Imports System.Data.SqlClient
Imports System.Text
Imports Itis.ApplicationBlocks.Data.SQLHelper

Public Class KameitenMasterDataAccess

    Private connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>アップロード管理を取得する</summary>
    ''' <returns>アップロード管理データテーブル</returns>
    Public Function SelInputKanri() As Data.DataTable

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

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
            .AppendLine("	file_kbn = 8 ")         'ファイル区分(8：加盟店情報一括取込用)
            .AppendLine(" ORDER BY ")
            .AppendLine("	syori_datetime DESC ")  '処理日時(降順)
        End With

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtInputKanri")

        Return dsReturn.Tables(0)

    End Function

    ''' <summary>アップロード管理の件数を取得する</summary>
    ''' <returns>アップロード管理の件数</returns>
    Public Function SelInputKanriCount() As Integer

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	COUNT(*) ")    '件数
            .AppendLine("FROM ")
            .AppendLine("	t_upload_kanri WITH(READCOMMITTED) ")           'アップロード管理テーブル
            .AppendLine("WHERE ")
            .AppendLine("	file_kbn = 8 ")             'ファイル区分
        End With

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtInputKanri")

        Return dsReturn.Tables(0).Rows(0).Item(0).ToString

    End Function
    ''' <summary>系列マスタより、データを取得する</summary>
    Public Function SelKeiretu(ByVal strCd As String, ByVal strKbn As String, ByRef mei As String) As Boolean

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet
        mei = ""
        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT DISTINCT ")
            .AppendLine("   keiretu_mei ")
            .AppendLine("FROM ")
            .AppendLine("   m_keiretu WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("   keiretu_cd = @cd")
            .AppendLine("   AND kbn = @kbn")
        End With

        'パラメータ作成
        paramList.Add(MakeParam("@cd", SqlDbType.VarChar, 5, strCd))

        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, strKbn))


        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtSyouhinCd", paramList.ToArray)

        '戻り値
        If dsReturn.Tables(0).Rows.Count > 0 Then
            mei = dsReturn.Tables(0).Rows(0).Item(0).ToString
            Return True

        Else
            mei = ""
            Return False
        End If

    End Function
    ''' <summary>営業所マスタより、データを取得する</summary>
    Public Function SelEigyousyo(ByVal strCd As String, ByRef mei As String) As Boolean

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet
        mei = ""
        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT DISTINCT ")
            .AppendLine("   eigyousyo_mei ")
            .AppendLine("FROM ")
            .AppendLine("   m_eigyousyo WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("   eigyousyo_cd = @cd")

        End With

        'パラメータ作成
        paramList.Add(MakeParam("@cd", SqlDbType.VarChar, 5, strCd))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtSyouhinCd", paramList.ToArray)

        '戻り値
        If dsReturn.Tables(0).Rows.Count > 0 Then
            mei = dsReturn.Tables(0).Rows(0).Item(0).ToString
            Return True
        Else
            mei = ""
            Return False
        End If

    End Function


    ''' <summary>商品コードを取得する</summary>
    Public Function SelTodoufuken(ByVal strCd As String, ByRef mei As String) As Boolean

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet
        mei = ""
        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT DISTINCT ")
            .AppendLine("	todouhuken_mei ")
            .AppendLine("FROM  ")
            .AppendLine("	m_todoufuken WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	todouhuken_cd = @todouhuken_cd ")
        End With

        'パラメータ作成
        paramList.Add(MakeParam("@todouhuken_cd", SqlDbType.VarChar, 2, strCd))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtSyouhinCd", paramList.ToArray)

        '戻り値
        If dsReturn.Tables(0).Rows.Count > 0 Then
            mei = dsReturn.Tables(0).Rows(0).Item(0).ToString
            Return True
        Else
            mei = ""
            Return False
        End If

    End Function
    ''' <summary>アカウントマスタより、データを取得する</summary>
    Public Function SelAccount(ByVal strCd As String, ByRef mei As String) As Boolean

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet
        mei = ""
        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT DISTINCT ")
            .AppendLine("   simei ")
            .AppendLine("FROM ")
            .AppendLine("   m_jiban_ninsyou WITH(READCOMMITTED) left join  m_account WITH(READCOMMITTED) on m_jiban_ninsyou.account_no=m_account.account_no where m_jiban_ninsyou.login_user_id=@cd")


        End With

        'パラメータ作成
        paramList.Add(MakeParam("@cd", SqlDbType.VarChar, 64, strCd))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtSyouhinCd", paramList.ToArray)

        '戻り値
        If dsReturn.Tables(0).Rows.Count > 0 Then
            mei = dsReturn.Tables(0).Rows(0).Item(0).ToString
            Return True
        Else
            mei = ""
            Return False
        End If

    End Function
    ''' <summary>調査会社マスタより、データを取得する</summary>
    Public Function SelTyousakaisya(ByVal strCd As String, ByVal strCd2 As String, Optional ByRef mei As String = "") As Boolean

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT DISTINCT ")
            .AppendLine("   tys_kaisya_mei ")
            .AppendLine("FROM ")
            .AppendLine("   m_tyousakaisya WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("   tys_kaisya_cd = @cd")
            .AppendLine("  AND jigyousyo_cd = @cd2")
        End With

        'パラメータ作成
        paramList.Add(MakeParam("@cd", SqlDbType.VarChar, 5, strCd))
        paramList.Add(MakeParam("@cd2", SqlDbType.VarChar, 2, strCd2))
        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtSyouhinCd", paramList.ToArray)

        '戻り値
        If dsReturn.Tables(0).Rows.Count > 0 Then
            mei = dsReturn.Tables(0).Rows(0).Item(0).ToString
            Return True
        Else
            mei = ""
            Return False
        End If

    End Function
    ''' <summary>加盟店コードを取得する</summary>
    Public Function SelKameitenCd(ByVal strKameitenCd As String, Optional ByVal strKbn As String = "", Optional ByRef mei As String = "") As Boolean

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT DISTINCT ")
            .AppendLine("   kameiten_mei1 ")
            .AppendLine("FROM ")
            .AppendLine("   m_kameiten WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("   kameiten_cd = @kameiten_cd")
            If Not String.IsNullOrEmpty(strKbn) Then
                .AppendLine("   AND kbn = @kbn")
            End If
        End With

        'パラメータ作成
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))
        If Not String.IsNullOrEmpty(strKbn) Then
            paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, strKbn))
        End If

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtSyouhinCd", paramList.ToArray)

        '戻り値
        If dsReturn.Tables(0).Rows.Count > 0 Then
            mei = dsReturn.Tables(0).Rows(0).Item(0).ToString
            Return True
        Else
            mei = ""
            Return False
        End If

    End Function

    ''' <summary>商品コードを取得する</summary>
    Public Function SelSyouhinCd(ByVal strSyouhinCd As String, ByRef mei As String) As Boolean

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet
        mei = ""
        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT DISTINCT ")
            .AppendLine("	syouhin_mei ")
            .AppendLine("FROM  ")
            .AppendLine("	m_syouhin WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	syouhin_cd = @syouhin_cd ")
        End With

        'パラメータ作成
        paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, strSyouhinCd))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtSyouhinCd", paramList.ToArray)

        '戻り値
        If dsReturn.Tables(0).Rows.Count > 0 Then
            mei = dsReturn.Tables(0).Rows(0).Item(0).ToString
            Return True
        Else
            mei = ""
            Return False
        End If

    End Function

    '==================2012/03/13 車龍 追加↓====================================
    ''' <summary>商品ﾏｽﾀ.souko_cd = "115'　に存在しない場合、falseです</summary>
    Public Function SelSoukoCheck(ByVal strSyouhinCd As String) As Boolean

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	COUNT(syouhin_cd) AS cnt ")
            .AppendLine("FROM  ")
            .AppendLine("	m_syouhin WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	syouhin_cd = @syouhin_cd ") '--商品コード
            .AppendLine("	and ")
            .AppendLine("	souko_cd = @souko_cd ") '--倉庫コード
        End With

        'パラメータ作成
        paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, strSyouhinCd))
        paramList.Add(MakeParam("@souko_cd", SqlDbType.VarChar, 3, "115"))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtSoukoCheck", paramList.ToArray)

        '戻り値
        If CInt(dsReturn.Tables(0).Rows(0).Item("cnt").ToString) > 0 Then
            Return True
        Else
            Return False
        End If

    End Function
    '==================2012/03/13 車龍 追加↑====================================

    ''' <summary>名称コードを取得する</summary>
    Public Function SelMeisyouCd(ByVal strMeisyouCd As String, ByRef mei As String) As Boolean

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet
        mei = ""
        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT DISTINCT ")
            .AppendLine("	meisyou ")
            .AppendLine("FROM  ")
            .AppendLine("	m_meisyou WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	code = @code ")
            .AppendLine("	AND meisyou_syubetu = '09' ")
        End With

        'パラメータ作成
        paramList.Add(MakeParam("@code", SqlDbType.VarChar, 8, strMeisyouCd))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtMeisyouCd", paramList.ToArray)

        '戻り値
        If dsReturn.Tables(0).Rows.Count > 0 Then
            mei = dsReturn.Tables(0).Rows(0).Item(0).ToString
            Return True
        Else
            mei = ""
            Return False
        End If

    End Function

    ''' <summary>名称コードを取得する</summary>
    Public Function SelTatouwaribikiSettei(ByVal strKameitenCd As String, ByVal strKubun As String) As Boolean

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT DISTINCT ")
            .AppendLine("	kameiten_cd ")
            .AppendLine("FROM  ")
            .AppendLine("	m_tatouwaribiki_settei WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	kameiten_cd = @kameiten_cd ")
            If strKubun <> "" Then
                .AppendLine(" AND toukubun = strKubun ")
            End If

        End With

        'パラメータ作成
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 8, strKameitenCd))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtTatouwaribikiSettei", paramList.ToArray)

        '戻り値
        If dsReturn.Tables(0).Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function
    ''' <summary>名称コードを取得する</summary>
    Public Function SelKameitenJyuusyo(ByVal strKameitenCd As String) As Boolean

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT DISTINCT ")
            .AppendLine("	kameiten_cd ")
            .AppendLine("FROM  ")
            .AppendLine("	m_kameiten_jyuusyo WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	kameiten_cd = @kameiten_cd ")
            .AppendLine(" AND jyuusyo_no = 1 ")
        End With

        'パラメータ作成
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 8, strKameitenCd))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtTatouwaribikiSettei", paramList.ToArray)

        '戻り値
        If dsReturn.Tables(0).Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function
    ''' <summary>エラー情報テーブルにエラー情報データを追加</summary>
    Public Function InstKameitenInfoIttukatuError(ByVal dtError As Data.DataTable) As Boolean

        '戻り値
        Dim InsCount As Integer = 0

        'SQLコメント
        Dim commandTextSb As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("INSERT INTO ")
            .AppendLine("	m_kameiten_info_ittukatu_error WITH(UPDLOCK) ")
            .AppendLine("	( ")
            .AppendLine("		edi_jouhou_sakusei_date")
            .AppendLine("		,gyou_no")
            .AppendLine("		,syori_datetime")
            .AppendLine("		,kbn")
            .AppendLine("		,kameiten_cd")
            .AppendLine("		,torikesi")
            .AppendLine("		,hattyuu_teisi_flg")
            .AppendLine("		,kameiten_mei1")
            .AppendLine("		,tenmei_kana1")
            .AppendLine("		,kameiten_mei2")
            .AppendLine("		,tenmei_kana2")
            .AppendLine("		,builder_no")
            .AppendLine("		,builder_mei")
            .AppendLine("		,keiretu_cd")
            .AppendLine("		,keiretu_mei")
            .AppendLine("		,eigyousyo_cd")
            .AppendLine("		,eigyousyo_mei")
            .AppendLine("		,kameiten_seisiki_mei")
            .AppendLine("		,kameiten_seisiki_mei_kana")
            .AppendLine("		,todouhuken_cd")
            .AppendLine("		,todouhuken_mei")
            '================↓2013/03/06 車龍 407584 追加↓========================
            .AppendLine("		,nenkan_tousuu ") '--年間棟数
            '================↑2013/03/06 車龍 407584 追加↑========================
            .AppendLine("		,fuho_syoumeisyo_flg")
            .AppendLine("		,fuho_syoumeisyo_kaisi_nengetu")
            .AppendLine("		,eigyou_tantousya_mei")
            .AppendLine("		,eigyou_tantousya_simei")
            .AppendLine("		,kyuu_eigyou_tantousya_mei")
            .AppendLine("		,kyuu_eigyou_tantousya_simei")
            '================↓2013/03/06 車龍 407584 追加↓========================
            .AppendLine("		,koj_uri_syubetsu ") '--工事売上種別
            .AppendLine("		,koj_uri_syubetsu_mei ") '--工事売上種別名
            .AppendLine("		,jiosaki_flg ") '--JIO先フラグ
            '================↑2013/03/06 車龍 407584 追加↑========================
            .AppendLine("		,kaiyaku_haraimodosi_kkk")
            .AppendLine("		,tou1_syouhin_cd")
            .AppendLine("		,tou1_syouhin_mei")
            .AppendLine("		,tou2_syouhin_cd")
            .AppendLine("		,tou2_syouhin_mei")
            .AppendLine("		,tou3_syouhin_cd")
            .AppendLine("		,tou3_syouhin_mei")
            .AppendLine("		,tys_seikyuu_saki_kbn")
            .AppendLine("		,tys_seikyuu_saki_cd")
            .AppendLine("		,tys_seikyuu_saki_brc")
            .AppendLine("		,tys_seikyuu_saki_mei")
            .AppendLine("		,koj_seikyuu_saki_kbn")
            .AppendLine("		,koj_seikyuu_saki_cd")
            .AppendLine("		,koj_seikyuu_saki_brc")
            .AppendLine("		,koj_seikyuu_saki_mei")
            .AppendLine("		,hansokuhin_seikyuu_saki_kbn")
            .AppendLine("		,hansokuhin_seikyuu_saki_cd")
            .AppendLine("		,hansokuhin_seikyuu_saki_brc")
            .AppendLine("		,hansokuhin_seikyuu_saki_mei")
            .AppendLine("		,tatemono_seikyuu_saki_kbn")
            .AppendLine("		,tatemono_seikyuu_saki_cd")
            .AppendLine("		,tatemono_seikyuu_saki_brc")
            .AppendLine("		,tatemono_seikyuu_saki_mei")
            .AppendLine("		,seikyuu_saki_kbn5")
            .AppendLine("		,seikyuu_saki_cd5")
            .AppendLine("		,seikyuu_saki_brc5")
            .AppendLine("		,seikyuu_saki_mei5")
            .AppendLine("		,seikyuu_saki_kbn6")
            .AppendLine("		,seikyuu_saki_cd6")
            .AppendLine("		,seikyuu_saki_brc6")
            .AppendLine("		,seikyuu_saki_mei6")
            .AppendLine("		,seikyuu_saki_kbn7")
            .AppendLine("		,seikyuu_saki_cd7")
            .AppendLine("		,seikyuu_saki_brc7")
            .AppendLine("		,seikyuu_saki_mei7")
            '================↓2013/03/06 車龍 407584 追加↓========================
            .AppendLine("		,hosyou_kikan ") '--保証期間
            .AppendLine("		,hosyousyo_hak_umu ") '--保証書発行有無
            '================↑2013/03/06 車龍 407584 追加↑========================
            .AppendLine("		,nyuukin_kakunin_jyouken")
            .AppendLine("		,nyuukin_kakunin_oboegaki")
            .AppendLine("		,tys_mitsyo_flg")
            .AppendLine("		,hattyuusyo_flg")
            .AppendLine("		,yuubin_no")
            .AppendLine("		,jyuusyo1")
            .AppendLine("		,jyuusyo2")
            .AppendLine("		,syozaichi_cd")
            .AppendLine("		,syozaichi_mei")
            .AppendLine("		,busyo_mei")
            .AppendLine("		,daihyousya_mei")
            .AppendLine("		,tel_no")
            .AppendLine("		,fax_no")
            .AppendLine("		,mail_address")
            .AppendLine("		,bikou1")
            .AppendLine("		,bikou2")
            '================↓2013/03/06 車龍 407584 追加↓========================
            .AppendLine("		,add_date ") '--登録日
            .AppendLine("		,seikyuu_umu ") '--請求有無
            .AppendLine("		,syouhin_cd ") '--商品コード
            .AppendLine("		,syouhin_mei ") '--商品名
            .AppendLine("		,uri_gaku ") '--売上金額
            .AppendLine("		,koumuten_seikyuu_gaku ") '--工務店請求金額
            .AppendLine("		,seikyuusyo_hak_date ") '--請求書発行日
            .AppendLine("		,uri_date ") '--売上年月日
            .AppendLine("		,bikou ") '--備考
            '================↑2013/03/06 車龍 407584 追加↑========================
            '==========2012/05/09 車龍 407553の対応 修正↓======================
            .AppendLine("		,kameiten_upd_datetime")
            .AppendLine("		,tatouwari_upd_datetime1")
            .AppendLine("		,tatouwari_upd_datetime2")
            .AppendLine("		,tatouwari_upd_datetime3")
            .AppendLine("		,kameiten_jyuusyo_upd_datetime")
            '==========2012/05/09 車龍 407553の対応 修正↑======================
            .AppendLine("		,bikou_syubetu1")
            .AppendLine("		,bikou_syubetu1_mei")
            .AppendLine("		,bikou_syubetu2")
            .AppendLine("		,bikou_syubetu2_mei")
            .AppendLine("		,bikou_syubetu3")
            .AppendLine("		,bikou_syubetu3_mei")
            .AppendLine("		,bikou_syubetu4")
            .AppendLine("		,bikou_syubetu4_mei")
            .AppendLine("		,bikou_syubetu5")
            .AppendLine("		,bikou_syubetu5_mei")
            .AppendLine("		,naiyou1")
            .AppendLine("		,naiyou2")
            .AppendLine("		,naiyou3")
            .AppendLine("		,naiyou4")
            .AppendLine("		,naiyou5")
            .AppendLine("		,add_login_user_id")
            .AppendLine("		,add_datetime")
            .AppendLine("		,upd_login_user_id")
            .AppendLine("		,upd_datetime")
            .AppendLine("	) ")
            .AppendLine("VALUES ")
            .AppendLine("	( ")
            .AppendLine("		@edi_jouhou_sakusei_date")
            .AppendLine("		,@gyou_no")
            .AppendLine("		,@syori_datetime")
            .AppendLine("		,@kbn")
            .AppendLine("		,@kameiten_cd")
            .AppendLine("		,@torikesi")
            .AppendLine("		,@hattyuu_teisi_flg")
            .AppendLine("		,@kameiten_mei1")
            .AppendLine("		,@tenmei_kana1")
            .AppendLine("		,@kameiten_mei2")
            .AppendLine("		,@tenmei_kana2")
            .AppendLine("		,@builder_no")
            .AppendLine("		,@builder_mei")
            .AppendLine("		,@keiretu_cd")
            .AppendLine("		,@keiretu_mei")
            .AppendLine("		,@eigyousyo_cd")
            .AppendLine("		,@eigyousyo_mei")
            .AppendLine("		,@kameiten_seisiki_mei")
            .AppendLine("		,@kameiten_seisiki_mei_kana")
            .AppendLine("		,@todouhuken_cd")
            .AppendLine("		,@todouhuken_mei")
            '================↓2013/03/06 車龍 407584 追加↓========================
            .AppendLine("		,@nenkan_tousuu ") '--年間棟数
            '================↑2013/03/06 車龍 407584 追加↑========================
            .AppendLine("		,@fuho_syoumeisyo_flg")
            .AppendLine("		,@fuho_syoumeisyo_kaisi_nengetu")
            .AppendLine("		,@eigyou_tantousya_mei")
            .AppendLine("		,@eigyou_tantousya_simei")
            .AppendLine("		,@kyuu_eigyou_tantousya_mei")
            .AppendLine("		,@kyuu_eigyou_tantousya_simei")
            '================↓2013/03/06 車龍 407584 追加↓========================
            .AppendLine("		,@koj_uri_syubetsu ") '--工事売上種別
            .AppendLine("		,@koj_uri_syubetsu_mei ") '--工事売上種別名
            .AppendLine("		,@jiosaki_flg ") '--JIO先フラグ
            '================↑2013/03/06 車龍 407584 追加↑========================
            .AppendLine("		,@kaiyaku_haraimodosi_kkk")
            .AppendLine("		,@syouhin_cd1")
            .AppendLine("		,@syouhin_mei1")
            .AppendLine("		,@syouhin_cd2")
            .AppendLine("		,@syouhin_mei2")
            .AppendLine("		,@syouhin_cd3")
            .AppendLine("		,@syouhin_mei3")
            .AppendLine("		,@tys_seikyuu_saki_kbn")
            .AppendLine("		,@tys_seikyuu_saki_cd")
            .AppendLine("		,@tys_seikyuu_saki_brc")
            .AppendLine("		,@tys_seikyuu_saki_mei")
            .AppendLine("		,@koj_seikyuu_saki_kbn")
            .AppendLine("		,@koj_seikyuu_saki_cd")
            .AppendLine("		,@koj_seikyuu_saki_brc")
            .AppendLine("		,@koj_seikyuu_saki_mei")
            .AppendLine("		,@hansokuhin_seikyuu_saki_kbn")
            .AppendLine("		,@hansokuhin_seikyuu_saki_cd")
            .AppendLine("		,@hansokuhin_seikyuu_saki_brc")
            .AppendLine("		,@hansokuhin_seikyuu_saki_mei")
            .AppendLine("		,@tatemono_seikyuu_saki_kbn")
            .AppendLine("		,@tatemono_seikyuu_saki_cd")
            .AppendLine("		,@tatemono_seikyuu_saki_brc")
            .AppendLine("		,@tatemono_seikyuu_saki_mei")
            .AppendLine("		,@seikyuu_saki_kbn5")
            .AppendLine("		,@seikyuu_saki_cd5")
            .AppendLine("		,@seikyuu_saki_brc5")
            .AppendLine("		,@seikyuu_saki_mei5")
            .AppendLine("		,@seikyuu_saki_kbn6")
            .AppendLine("		,@seikyuu_saki_cd6")
            .AppendLine("		,@seikyuu_saki_brc6")
            .AppendLine("		,@seikyuu_saki_mei6")
            .AppendLine("		,@seikyuu_saki_kbn7")
            .AppendLine("		,@seikyuu_saki_cd7")
            .AppendLine("		,@seikyuu_saki_brc7")
            .AppendLine("		,@seikyuu_saki_mei7")
            '================↓2013/03/06 車龍 407584 追加↓========================
            .AppendLine("		,@hosyou_kikan ") '--保証期間
            .AppendLine("		,@hosyousyo_hak_umu ") '--保証書発行有無
            '================↑2013/03/06 車龍 407584 追加↑========================
            .AppendLine("		,@nyuukin_kakunin_jyouken")
            .AppendLine("		,@nyuukin_kakunin_oboegaki")
            .AppendLine("		,@tys_mitsyo_flg")
            .AppendLine("		,@hattyuusyo_flg")
            .AppendLine("		,@yuubin_no")
            .AppendLine("		,@jyuusyo1")
            .AppendLine("		,@jyuusyo2")
            .AppendLine("		,@syozaichi_cd")
            .AppendLine("		,@syozaichi_mei")
            .AppendLine("		,@busyo_mei")
            .AppendLine("		,@daihyousya_mei")
            .AppendLine("		,@tel_no")
            .AppendLine("		,@fax_no")
            .AppendLine("		,@mail_address")
            .AppendLine("		,@bikou1")
            .AppendLine("		,@bikou2")
            '================↓2013/03/06 車龍 407584 追加↓========================
            .AppendLine("		,@add_date ") '--登録日
            .AppendLine("		,@seikyuu_umu ") '--請求有無
            .AppendLine("		,@syouhin_cd ") '--商品コード
            .AppendLine("		,@syouhin_mei ") '--商品名
            .AppendLine("		,@uri_gaku ") '--売上金額
            .AppendLine("		,@koumuten_seikyuu_gaku ") '--工務店請求金額
            .AppendLine("		,@seikyuusyo_hak_date ") '--請求書発行日
            .AppendLine("		,@uri_date ") '--売上年月日
            .AppendLine("		,@bikou ") '--備考
            '================↑2013/03/06 車龍 407584 追加↑========================
            '==========2012/05/09 車龍 407553の対応 修正↓======================
            .AppendLine("		,@kameiten_upd_datetime")
            .AppendLine("		,@tatouwari_upd_datetime1")
            .AppendLine("		,@tatouwari_upd_datetime2")
            .AppendLine("		,@tatouwari_upd_datetime3")
            .AppendLine("		,@kameiten_jyuusyo_upd_datetime")
            '==========2012/05/09 車龍 407553の対応 修正↑======================
            .AppendLine("		,@bikou_syubetu1")
            .AppendLine("		,@bikou_syubetu1_mei")
            .AppendLine("		,@bikou_syubetu2")
            .AppendLine("		,@bikou_syubetu2_mei")
            .AppendLine("		,@bikou_syubetu3")
            .AppendLine("		,@bikou_syubetu3_mei")
            .AppendLine("		,@bikou_syubetu4")
            .AppendLine("		,@bikou_syubetu4_mei")
            .AppendLine("		,@bikou_syubetu5")
            .AppendLine("		,@bikou_syubetu5_mei")
            .AppendLine("		,@naiyou1")
            .AppendLine("		,@naiyou2")
            .AppendLine("		,@naiyou3")
            .AppendLine("		,@naiyou4")
            .AppendLine("		,@naiyou5")
            .AppendLine("		,@add_login_user_id")
            .AppendLine("		,CONVERT(VARCHAR(10),GETDATE(),111) ")
            .AppendLine("		,NULL ")
            .AppendLine("		,NULL ")
            .AppendLine("	) ")
        End With

        For i As Integer = 0 To dtError.Rows.Count - 1
            'パラメータの設定
            paramList.Clear()
            paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, InsObj(dtError.Rows(i).Item("edi_jouhou_sakusei_date").ToString.Trim)))
            '==========2012/05/09 車龍 407553の対応 修正↓======================
            'paramList.Add(MakeParam("@gyou_no", SqlDbType.Int, 5, InsObj(dtError.Rows(i).Item("gyou_no").ToString.Trim)))
            'paramList.Add(MakeParam("@syori_datetime", SqlDbType.DateTime, 30, Convert.ToDateTime(CDate(dtError.Rows(i).Item("syori_datetime").ToString.Trim))))
            paramList.Add(MakeParam("@gyou_no", SqlDbType.Int, 5, InsObj(dtError.Rows(i).Item("gyou_no").ToString.Trim)))
            paramList.Add(MakeParam("@syori_datetime", SqlDbType.DateTime, 30, Convert.ToDateTime(CDate(dtError.Rows(i).Item("syori_datetime").ToString.Trim))))
            '==========2012/05/09 車龍 407553の対応 修正↑======================
            paramList.Add(MakeParam("@kbn", SqlDbType.VarChar, 1, InsObj(dtError.Rows(i).Item("kbn").ToString.Trim)))
            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, InsObj(dtError.Rows(i).Item("kameiten_cd").ToString.Trim)))
            paramList.Add(MakeParam("@torikesi", SqlDbType.VarChar, 1, InsObj(dtError.Rows(i).Item("torikesi").ToString.Trim)))
            paramList.Add(MakeParam("@hattyuu_teisi_flg", SqlDbType.VarChar, 10, InsObj(dtError.Rows(i).Item("hattyuu_teisi_flg").ToString.Trim)))
            paramList.Add(MakeParam("@kameiten_mei1", SqlDbType.VarChar, 40, InsObj(dtError.Rows(i).Item("kameiten_mei1").ToString.Trim)))
            paramList.Add(MakeParam("@tenmei_kana1", SqlDbType.VarChar, 20, InsObj(dtError.Rows(i).Item("tenmei_kana1").ToString.Trim)))
            paramList.Add(MakeParam("@kameiten_mei2", SqlDbType.VarChar, 40, InsObj(dtError.Rows(i).Item("kameiten_mei2").ToString.Trim)))
            paramList.Add(MakeParam("@tenmei_kana2", SqlDbType.VarChar, 20, InsObj(dtError.Rows(i).Item("tenmei_kana2").ToString.Trim)))
            paramList.Add(MakeParam("@builder_no", SqlDbType.VarChar, 9, InsObj(dtError.Rows(i).Item("builder_no").ToString.Trim)))
            paramList.Add(MakeParam("@builder_mei", SqlDbType.VarChar, 40, GetBuildMei(dtError.Rows(i).Item("builder_no").ToString.Trim)))
            paramList.Add(MakeParam("@keiretu_cd", SqlDbType.VarChar, 5, InsObj(dtError.Rows(i).Item("keiretu_cd").ToString.Trim)))
            paramList.Add(MakeParam("@keiretu_mei", SqlDbType.VarChar, 40, InsObj(dtError.Rows(i).Item("keiretu_mei").ToString.Trim)))
            paramList.Add(MakeParam("@eigyousyo_cd", SqlDbType.VarChar, 5, InsObj(dtError.Rows(i).Item("eigyousyo_cd").ToString.Trim)))
            paramList.Add(MakeParam("@eigyousyo_mei", SqlDbType.VarChar, 40, GetEigyousyoMei(dtError.Rows(i).Item("eigyousyo_cd").ToString.Trim)))
            paramList.Add(MakeParam("@kameiten_seisiki_mei", SqlDbType.VarChar, 80, InsObj(dtError.Rows(i).Item("kameiten_seisiki_mei").ToString.Trim)))
            paramList.Add(MakeParam("@kameiten_seisiki_mei_kana", SqlDbType.VarChar, 40, InsObj(dtError.Rows(i).Item("kameiten_seisiki_mei_kana").ToString.Trim)))
            paramList.Add(MakeParam("@todouhuken_cd", SqlDbType.VarChar, 2, InsObj(dtError.Rows(i).Item("todouhuken_cd").ToString.Trim)))
            paramList.Add(MakeParam("@todouhuken_mei", SqlDbType.VarChar, 10, GetTodouhukenMei(dtError.Rows(i).Item("todouhuken_cd").ToString.Trim)))
            '================↓2013/03/06 車龍 407584 追加↓========================
            paramList.Add(MakeParam("@nenkan_tousuu", SqlDbType.VarChar, 5, InsObj(dtError.Rows(i).Item("nenkan_tousuu").ToString.Trim)))
            '================↑2013/03/06 車龍 407584 追加↑========================
            paramList.Add(MakeParam("@fuho_syoumeisyo_flg", SqlDbType.VarChar, 1, InsObj(dtError.Rows(i).Item("fuho_syoumeisyo_flg").ToString.Trim)))
            paramList.Add(MakeParam("@fuho_syoumeisyo_kaisi_nengetu", SqlDbType.VarChar, 10, InsObj(dtError.Rows(i).Item("fuho_syoumeisyo_kaisi_nengetu").ToString.Trim)))
            paramList.Add(MakeParam("@eigyou_tantousya_mei", SqlDbType.VarChar, 16, InsObj(dtError.Rows(i).Item("eigyou_tantousya_mei").ToString.Trim)))
            paramList.Add(MakeParam("@eigyou_tantousya_simei", SqlDbType.VarChar, 30, GetSimei(dtError.Rows(i).Item("eigyou_tantousya_mei").ToString.Trim)))
            paramList.Add(MakeParam("@kyuu_eigyou_tantousya_mei", SqlDbType.VarChar, 16, InsObj(dtError.Rows(i).Item("kyuu_eigyou_tantousya_mei").ToString.Trim)))
            paramList.Add(MakeParam("@kyuu_eigyou_tantousya_simei", SqlDbType.VarChar, 30, GetSimei(dtError.Rows(i).Item("kyuu_eigyou_tantousya_mei").ToString.Trim)))
            '================↓2013/03/06 車龍 407584 追加↓========================
            paramList.Add(MakeParam("@koj_uri_syubetsu", SqlDbType.VarChar, 1, InsObj(dtError.Rows(i).Item("koj_uri_syubetsu").ToString.Trim)))
            paramList.Add(MakeParam("@koj_uri_syubetsu_mei", SqlDbType.VarChar, 40, Me.GetKojUriSyubetuMei(dtError.Rows(i).Item("koj_uri_syubetsu").ToString.Trim)))
            paramList.Add(MakeParam("@jiosaki_flg", SqlDbType.VarChar, 1, InsObj(dtError.Rows(i).Item("jiosaki_flg").ToString.Trim)))
            '================↑2013/03/06 車龍 407584 追加↑========================
            paramList.Add(MakeParam("@kaiyaku_haraimodosi_kkk", SqlDbType.VarChar, 10, InsObj(dtError.Rows(i).Item("kaiyaku_haraimodosi_kkk").ToString.Trim)))
            paramList.Add(MakeParam("@syouhin_cd1", SqlDbType.VarChar, 8, InsObj(dtError.Rows(i).Item("syouhin_cd1").ToString.Trim)))
            paramList.Add(MakeParam("@syouhin_mei1", SqlDbType.VarChar, 40, GetSyouhinMei(dtError.Rows(i).Item("syouhin_cd1").ToString.Trim)))
            paramList.Add(MakeParam("@syouhin_cd2", SqlDbType.VarChar, 8, InsObj(dtError.Rows(i).Item("syouhin_cd2").ToString.Trim)))
            paramList.Add(MakeParam("@syouhin_mei2", SqlDbType.VarChar, 40, GetSyouhinMei(dtError.Rows(i).Item("syouhin_cd2").ToString.Trim)))
            paramList.Add(MakeParam("@syouhin_cd3", SqlDbType.VarChar, 8, InsObj(dtError.Rows(i).Item("syouhin_cd3").ToString.Trim)))
            paramList.Add(MakeParam("@syouhin_mei3", SqlDbType.VarChar, 40, GetSyouhinMei(dtError.Rows(i).Item("syouhin_cd3").ToString.Trim)))
            paramList.Add(MakeParam("@tys_seikyuu_saki_kbn", SqlDbType.VarChar, 1, InsObj(dtError.Rows(i).Item("tys_seikyuu_saki_kbn").ToString.Trim)))
            paramList.Add(MakeParam("@tys_seikyuu_saki_cd", SqlDbType.VarChar, 5, InsObj(dtError.Rows(i).Item("tys_seikyuu_saki_cd").ToString.Trim)))
            paramList.Add(MakeParam("@tys_seikyuu_saki_brc", SqlDbType.VarChar, 2, InsObj(dtError.Rows(i).Item("tys_seikyuu_saki_brc").ToString.Trim)))
            paramList.Add(MakeParam("@tys_seikyuu_saki_mei", SqlDbType.VarChar, 40, InsObj(dtError.Rows(i).Item("tys_seikyuu_saki_mei").ToString.Trim)))
            paramList.Add(MakeParam("@koj_seikyuu_saki_kbn", SqlDbType.VarChar, 1, InsObj(dtError.Rows(i).Item("koj_seikyuu_saki_kbn").ToString.Trim)))
            paramList.Add(MakeParam("@koj_seikyuu_saki_cd", SqlDbType.VarChar, 5, InsObj(dtError.Rows(i).Item("koj_seikyuu_saki_cd").ToString.Trim)))
            paramList.Add(MakeParam("@koj_seikyuu_saki_brc", SqlDbType.VarChar, 2, InsObj(dtError.Rows(i).Item("koj_seikyuu_saki_brc").ToString.Trim)))
            paramList.Add(MakeParam("@koj_seikyuu_saki_mei", SqlDbType.VarChar, 40, InsObj(dtError.Rows(i).Item("koj_seikyuu_saki_mei").ToString.Trim)))
            paramList.Add(MakeParam("@hansokuhin_seikyuu_saki_kbn", SqlDbType.VarChar, 1, InsObj(dtError.Rows(i).Item("hansokuhin_seikyuu_saki_kbn").ToString.Trim)))
            paramList.Add(MakeParam("@hansokuhin_seikyuu_saki_cd", SqlDbType.VarChar, 5, InsObj(dtError.Rows(i).Item("hansokuhin_seikyuu_saki_cd").ToString.Trim)))
            paramList.Add(MakeParam("@hansokuhin_seikyuu_saki_brc", SqlDbType.VarChar, 2, InsObj(dtError.Rows(i).Item("hansokuhin_seikyuu_saki_brc").ToString.Trim)))
            paramList.Add(MakeParam("@hansokuhin_seikyuu_saki_mei", SqlDbType.VarChar, 40, InsObj(dtError.Rows(i).Item("hansokuhin_seikyuu_saki_mei").ToString.Trim)))
            paramList.Add(MakeParam("@tatemono_seikyuu_saki_kbn", SqlDbType.VarChar, 1, InsObj(dtError.Rows(i).Item("tatemono_seikyuu_saki_kbn").ToString.Trim)))
            paramList.Add(MakeParam("@tatemono_seikyuu_saki_cd", SqlDbType.VarChar, 5, InsObj(dtError.Rows(i).Item("tatemono_seikyuu_saki_cd").ToString.Trim)))
            paramList.Add(MakeParam("@tatemono_seikyuu_saki_brc", SqlDbType.VarChar, 2, InsObj(dtError.Rows(i).Item("tatemono_seikyuu_saki_brc").ToString.Trim)))
            paramList.Add(MakeParam("@tatemono_seikyuu_saki_mei", SqlDbType.VarChar, 40, InsObj(dtError.Rows(i).Item("tatemono_seikyuu_saki_mei").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_kbn5", SqlDbType.VarChar, 1, InsObj(dtError.Rows(i).Item("seikyuu_saki_kbn5").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_cd5", SqlDbType.VarChar, 5, InsObj(dtError.Rows(i).Item("seikyuu_saki_cd5").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_brc5", SqlDbType.VarChar, 2, InsObj(dtError.Rows(i).Item("seikyuu_saki_brc5").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_mei5", SqlDbType.VarChar, 40, InsObj(dtError.Rows(i).Item("seikyuu_saki_mei5").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_kbn6", SqlDbType.VarChar, 1, InsObj(dtError.Rows(i).Item("seikyuu_saki_kbn6").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_cd6", SqlDbType.VarChar, 5, InsObj(dtError.Rows(i).Item("seikyuu_saki_cd6").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_brc6", SqlDbType.VarChar, 2, InsObj(dtError.Rows(i).Item("seikyuu_saki_brc6").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_mei6", SqlDbType.VarChar, 40, InsObj(dtError.Rows(i).Item("seikyuu_saki_mei6").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_kbn7", SqlDbType.VarChar, 1, InsObj(dtError.Rows(i).Item("seikyuu_saki_kbn7").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_cd7", SqlDbType.VarChar, 5, InsObj(dtError.Rows(i).Item("seikyuu_saki_cd7").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_brc7", SqlDbType.VarChar, 2, InsObj(dtError.Rows(i).Item("seikyuu_saki_brc7").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_mei7", SqlDbType.VarChar, 40, InsObj(dtError.Rows(i).Item("seikyuu_saki_mei7").ToString.Trim)))
            '================↓2013/03/06 車龍 407584 追加↓========================
            paramList.Add(MakeParam("@hosyou_kikan", SqlDbType.VarChar, 2, InsObj(dtError.Rows(i).Item("hosyou_kikan").ToString.Trim)))
            paramList.Add(MakeParam("@hosyousyo_hak_umu", SqlDbType.VarChar, 1, InsObj(dtError.Rows(i).Item("hosyousyo_hak_umu").ToString.Trim)))
            '================↑2013/03/06 車龍 407584 追加↑========================
            paramList.Add(MakeParam("@nyuukin_kakunin_jyouken", SqlDbType.VarChar, 1, InsObj(dtError.Rows(i).Item("nyuukin_kakunin_jyouken").ToString.Trim)))
            paramList.Add(MakeParam("@nyuukin_kakunin_oboegaki", SqlDbType.VarChar, 10, InsObj(dtError.Rows(i).Item("nyuukin_kakunin_oboegaki").ToString.Trim)))
            paramList.Add(MakeParam("@tys_mitsyo_flg", SqlDbType.VarChar, 1, InsObj(dtError.Rows(i).Item("tys_mitsyo_flg").ToString.Trim)))
            paramList.Add(MakeParam("@hattyuusyo_flg", SqlDbType.VarChar, 1, InsObj(dtError.Rows(i).Item("hattyuusyo_flg").ToString.Trim)))
            paramList.Add(MakeParam("@yuubin_no", SqlDbType.VarChar, 8, InsObj(dtError.Rows(i).Item("yuubin_no").ToString.Trim)))
            paramList.Add(MakeParam("@jyuusyo1", SqlDbType.VarChar, 40, InsObj(dtError.Rows(i).Item("jyuusyo1").ToString.Trim)))
            paramList.Add(MakeParam("@jyuusyo2", SqlDbType.VarChar, 30, InsObj(dtError.Rows(i).Item("jyuusyo2").ToString.Trim)))
            paramList.Add(MakeParam("@syozaichi_cd", SqlDbType.VarChar, 2, InsObj(dtError.Rows(i).Item("jyuusyo_cd").ToString.Trim)))
            paramList.Add(MakeParam("@syozaichi_mei", SqlDbType.VarChar, 10, InsObj(dtError.Rows(i).Item("jyuusyo_mei").ToString.Trim)))
            paramList.Add(MakeParam("@busyo_mei", SqlDbType.VarChar, 50, InsObj(dtError.Rows(i).Item("busyo_mei").ToString.Trim)))
            paramList.Add(MakeParam("@daihyousya_mei", SqlDbType.VarChar, 20, InsObj(dtError.Rows(i).Item("daihyousya_mei").ToString.Trim)))
            paramList.Add(MakeParam("@tel_no", SqlDbType.VarChar, 16, InsObj(dtError.Rows(i).Item("tel_no").ToString.Trim)))
            paramList.Add(MakeParam("@fax_no", SqlDbType.VarChar, 16, InsObj(dtError.Rows(i).Item("fax_no").ToString.Trim)))
            paramList.Add(MakeParam("@mail_address", SqlDbType.VarChar, 64, InsObj(dtError.Rows(i).Item("mail_address").ToString.Trim)))
            paramList.Add(MakeParam("@bikou1", SqlDbType.VarChar, 30, InsObj(dtError.Rows(i).Item("bikou1").ToString.Trim)))
            paramList.Add(MakeParam("@bikou2", SqlDbType.VarChar, 30, InsObj(dtError.Rows(i).Item("bikou2").ToString.Trim)))
            '================↓2013/03/06 車龍 407584 追加↓========================
            paramList.Add(MakeParam("@add_date", SqlDbType.VarChar, 19, InsObj(dtError.Rows(i).Item("add_date").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_umu", SqlDbType.VarChar, 1, InsObj(dtError.Rows(i).Item("seikyuu_umu").ToString.Trim)))
            paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, InsObj(dtError.Rows(i).Item("syouhin_cd").ToString.Trim)))
            paramList.Add(MakeParam("@syouhin_mei", SqlDbType.VarChar, 40, Me.GetSyouhinMei(dtError.Rows(i).Item("syouhin_cd").ToString.Trim)))
            paramList.Add(MakeParam("@uri_gaku", SqlDbType.VarChar, 8, InsObj(dtError.Rows(i).Item("uri_gaku").ToString.Trim)))
            paramList.Add(MakeParam("@koumuten_seikyuu_gaku", SqlDbType.VarChar, 8, InsObj(dtError.Rows(i).Item("koumuten_seikyuu_gaku").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuusyo_hak_date", SqlDbType.VarChar, 19, InsObj(dtError.Rows(i).Item("seikyuusyo_hak_date").ToString.Trim)))
            paramList.Add(MakeParam("@uri_date", SqlDbType.VarChar, 19, InsObj(dtError.Rows(i).Item("uri_date").ToString.Trim)))
            paramList.Add(MakeParam("@bikou", SqlDbType.VarChar, 30, InsObj(dtError.Rows(i).Item("bikou").ToString.Trim)))
            '================↑2013/03/06 車龍 407584 追加↑========================
            '==========2012/05/09 車龍 407553の対応 修正↓======================
            paramList.Add(MakeParam("@kameiten_upd_datetime", SqlDbType.VarChar, 19, InsObj(dtError.Rows(i).Item("kameiten_upd_datetime").ToString.Trim)))
            paramList.Add(MakeParam("@tatouwari_upd_datetime1", SqlDbType.VarChar, 19, InsObj(dtError.Rows(i).Item("tatouwari_upd_datetime_1").ToString.Trim)))
            paramList.Add(MakeParam("@tatouwari_upd_datetime2", SqlDbType.VarChar, 19, InsObj(dtError.Rows(i).Item("tatouwari_upd_datetime_2").ToString.Trim)))
            paramList.Add(MakeParam("@tatouwari_upd_datetime3", SqlDbType.VarChar, 19, InsObj(dtError.Rows(i).Item("tatouwari_upd_datetime_3").ToString.Trim)))
            paramList.Add(MakeParam("@kameiten_jyuusyo_upd_datetime", SqlDbType.VarChar, 19, InsObj(dtError.Rows(i).Item("kameiten_jyuusyo_upd_datetime").ToString.Trim)))
            'paramList.Add(MakeParam("@bikou_syubetu1", SqlDbType.VarChar, 2, InsObj(dtError.Rows(i).Item("bikou_syubetu1").ToString.Trim)))
            'paramList.Add(MakeParam("@bikou_syubetu1_mei", SqlDbType.VarChar, 40, GetBikouMeisyou(dtError.Rows(i).Item("bikou_syubetu1_mei").ToString.Trim)))
            'paramList.Add(MakeParam("@bikou_syubetu2", SqlDbType.VarChar, 2, InsObj(dtError.Rows(i).Item("bikou_syubetu2").ToString.Trim)))
            'paramList.Add(MakeParam("@bikou_syubetu2_mei", SqlDbType.VarChar, 40, GetBikouMeisyou(dtError.Rows(i).Item("bikou_syubetu2_mei").ToString.Trim)))
            'paramList.Add(MakeParam("@bikou_syubetu3", SqlDbType.VarChar, 2, InsObj(dtError.Rows(i).Item("bikou_syubetu3").ToString.Trim)))
            'paramList.Add(MakeParam("@bikou_syubetu3_mei", SqlDbType.VarChar, 40, GetBikouMeisyou(dtError.Rows(i).Item("bikou_syubetu3_mei").ToString.Trim)))
            'paramList.Add(MakeParam("@bikou_syubetu4", SqlDbType.VarChar, 2, InsObj(dtError.Rows(i).Item("bikou_syubetu4").ToString.Trim)))
            'paramList.Add(MakeParam("@bikou_syubetu4_mei", SqlDbType.VarChar, 40, GetBikouMeisyou(dtError.Rows(i).Item("bikou_syubetu4_mei").ToString.Trim)))
            'paramList.Add(MakeParam("@bikou_syubetu5", SqlDbType.VarChar, 2, InsObj(dtError.Rows(i).Item("bikou_syubetu5").ToString.Trim)))
            'paramList.Add(MakeParam("@bikou_syubetu5_mei", SqlDbType.VarChar, 40, GetBikouMeisyou(dtError.Rows(i).Item("bikou_syubetu5_mei").ToString.Trim)))
            'paramList.Add(MakeParam("@naiyou1", SqlDbType.VarChar, 80, InsObj(dtError.Rows(i).Item("naiyou1").ToString.Trim)))
            'paramList.Add(MakeParam("@naiyou2", SqlDbType.VarChar, 80, InsObj(dtError.Rows(i).Item("naiyou2").ToString.Trim)))
            'paramList.Add(MakeParam("@naiyou3", SqlDbType.VarChar, 80, InsObj(dtError.Rows(i).Item("naiyou3").ToString.Trim)))
            'paramList.Add(MakeParam("@naiyou4", SqlDbType.VarChar, 80, InsObj(dtError.Rows(i).Item("naiyou4").ToString.Trim)))
            'paramList.Add(MakeParam("@naiyou5", SqlDbType.VarChar, 80, InsObj(dtError.Rows(i).Item("naiyou5").ToString.Trim)))
            'paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, InsObj(dtError.Rows(i).Item("add_login_user_id").ToString.Trim)))
            paramList.Add(MakeParam("@bikou_syubetu1", SqlDbType.VarChar, 2, InsObj(dtError.Rows(i).Item("bikou_syubetu1").ToString.Trim)))
            paramList.Add(MakeParam("@bikou_syubetu1_mei", SqlDbType.VarChar, 40, GetBikouMeisyou(dtError.Rows(i).Item("bikou_syubetu1").ToString.Trim)))
            paramList.Add(MakeParam("@bikou_syubetu2", SqlDbType.VarChar, 2, InsObj(dtError.Rows(i).Item("bikou_syubetu2").ToString.Trim)))
            paramList.Add(MakeParam("@bikou_syubetu2_mei", SqlDbType.VarChar, 40, GetBikouMeisyou(dtError.Rows(i).Item("bikou_syubetu2").ToString.Trim)))
            paramList.Add(MakeParam("@bikou_syubetu3", SqlDbType.VarChar, 2, InsObj(dtError.Rows(i).Item("bikou_syubetu3").ToString.Trim)))
            paramList.Add(MakeParam("@bikou_syubetu3_mei", SqlDbType.VarChar, 40, GetBikouMeisyou(dtError.Rows(i).Item("bikou_syubetu3").ToString.Trim)))
            paramList.Add(MakeParam("@bikou_syubetu4", SqlDbType.VarChar, 2, InsObj(dtError.Rows(i).Item("bikou_syubetu4").ToString.Trim)))
            paramList.Add(MakeParam("@bikou_syubetu4_mei", SqlDbType.VarChar, 40, GetBikouMeisyou(dtError.Rows(i).Item("bikou_syubetu4").ToString.Trim)))
            paramList.Add(MakeParam("@bikou_syubetu5", SqlDbType.VarChar, 2, InsObj(dtError.Rows(i).Item("bikou_syubetu5").ToString.Trim)))
            paramList.Add(MakeParam("@bikou_syubetu5_mei", SqlDbType.VarChar, 40, GetBikouMeisyou(dtError.Rows(i).Item("bikou_syubetu5").ToString.Trim)))
            paramList.Add(MakeParam("@naiyou1", SqlDbType.VarChar, 80, InsObj(dtError.Rows(i).Item("naiyou1").ToString.Trim)))
            paramList.Add(MakeParam("@naiyou2", SqlDbType.VarChar, 80, InsObj(dtError.Rows(i).Item("naiyou2").ToString.Trim)))
            paramList.Add(MakeParam("@naiyou3", SqlDbType.VarChar, 80, InsObj(dtError.Rows(i).Item("naiyou3").ToString.Trim)))
            paramList.Add(MakeParam("@naiyou4", SqlDbType.VarChar, 80, InsObj(dtError.Rows(i).Item("naiyou4").ToString.Trim)))
            paramList.Add(MakeParam("@naiyou5", SqlDbType.VarChar, 80, InsObj(dtError.Rows(i).Item("naiyou5").ToString.Trim)))
            paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, InsObj(dtError.Rows(i).Item("add_login_user_id").ToString.Trim)))

            '==========2012/05/09 車龍 407553の対応 修正↑======================


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

    ''' <summary>
    ''' 追加_備考種別名取得
    ''' </summary>
    ''' <param name="strBikouSyubetu">追加_備考種別</param>
    ''' <returns>追加_備考種別名</returns>
    Public Function GetBikouMeisyou(ByVal strBikouSyubetu As String) As Object
        Dim intCode As Integer
        Try
            intCode = CInt(strBikouSyubetu)
            Dim strSql As String = "SELECT meisyou FROM m_meisyou  WITH(READCOMMITTED) WHERE meisyou_syubetu='09' and  code='" & strBikouSyubetu & "'"
            Dim obj As Object = ExecuteScalar(connStr, CommandType.Text, strSql)

            If obj Is Nothing Then
                Return DBNull.Value
            Else
                Return obj.ToString
            End If
        Catch ex As Exception
            Return DBNull.Value
        End Try
    End Function

    ''' <summary>
    ''' ビルダー名取得
    ''' </summary>
    ''' <param name="strBuildNo">ビルダーNo</param>
    ''' <returns>ビルダー名</returns>
    Public Function GetBuildMei(ByVal strBuildNo As String) As Object
        Try
            Dim strSql As String = "SELECT kameiten_mei1 FROM m_kameiten  WITH(READCOMMITTED) WHERE kameiten_cd = '" & strBuildNo & "'"
            Dim obj As Object = ExecuteScalar(connStr, CommandType.Text, strSql)

            If obj Is Nothing Then
                Return DBNull.Value
            Else
                Return obj.ToString
            End If
        Catch ex As Exception
            Return DBNull.Value
        End Try
    End Function

    ''' <summary>
    ''' 営業所名取得
    ''' </summary>
    ''' <param name="strCode">営業所コード</param>
    ''' <returns>営業所名</returns>
    Public Function GetEigyousyoMei(ByVal strCode As String) As Object
        Try
            Dim strSql As String = "SELECT eigyousyo_mei FROM m_eigyousyo  WITH(READCOMMITTED) WHERE eigyousyo_cd = '" & strCode & "'"
            Dim obj As Object = ExecuteScalar(connStr, CommandType.Text, strSql)

            If obj Is Nothing Then
                Return DBNull.Value
            Else
                Return obj.ToString
            End If
        Catch ex As Exception
            Return DBNull.Value
        End Try
    End Function

    ''' <summary>
    ''' 都道府県名取得
    ''' </summary>
    ''' <param name="strCode">都道府県コード</param>
    ''' <returns>都道府県名</returns>
    Public Function GetTodouhukenMei(ByVal strCode As String) As Object
        Try
            Dim strSql As String = "SELECT todouhuken_mei FROM m_todoufuken  WITH(READCOMMITTED) WHERE todouhuken_cd = '" & strCode & "'"
            Dim obj As Object = ExecuteScalar(connStr, CommandType.Text, strSql)

            If obj Is Nothing Then
                Return DBNull.Value
            Else
                Return obj.ToString
            End If
        Catch ex As Exception
            Return DBNull.Value
        End Try
    End Function

    ''' <summary>
    ''' 営業担当者名取得
    ''' </summary>
    ''' <param name="strCode">コード</param>
    ''' <returns>営業担当者名</returns>
    Public Function GetSimei(ByVal strCode As String) As Object
        Try
            Dim strSql As String = "SELECT simei FROM m_account  WITH(READCOMMITTED) WHERE account= '" & strCode & "'"
            Dim obj As Object = ExecuteScalar(connStr, CommandType.Text, strSql)

            If obj Is Nothing Then
                Return DBNull.Value
            Else
                Return obj.ToString
            End If
        Catch ex As Exception
            Return DBNull.Value
        End Try
    End Function

    ''' <summary>
    ''' 商品名取得
    ''' </summary>
    ''' <param name="strCode">商品コード</param>
    ''' <returns>商品名</returns>
    Public Function GetSyouhinMei(ByVal strCode As String) As Object
        Try
            Dim strSql As String = "SELECT syouhin_mei FROM m_syouhin  WITH(READCOMMITTED) WHERE syouhin_cd= '" & strCode & "'"
            Dim obj As Object = ExecuteScalar(connStr, CommandType.Text, strSql)

            If obj Is Nothing Then
                Return DBNull.Value
            Else
                Return obj.ToString
            End If
        Catch ex As Exception
            Return DBNull.Value
        End Try
    End Function

    Function InsObj(ByVal str As String) As Object
        If String.IsNullOrEmpty(str) Then
            Return DBNull.Value
        Else
            Return str
        End If
    End Function

    ''' <summary>アップロード管理テーブルを登録する</summary>
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
            .AppendLine("	,8 ")
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

    ''' <summary>加盟店マスタにデータを更新</summary>
    ''' <history>2012/02/12　陳琳(大連情報システム部)　新規作成</history>
    Public Function UpdKameiten(ByVal dtOk As Data.DataTable) As Boolean
        '戻り値
        Dim InsUpdCount As Integer = 0

        '更新用sql文
        Dim strSqlUpd As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With strSqlUpd
            .AppendLine("UPDATE ")
            .AppendLine("	m_kameiten WITH(UPDLOCK) ")
            .AppendLine("SET ")
            .AppendLine("	kbn = @kbn")
            .AppendLine("	,torikesi = @torikesi")
            .AppendLine("	,hattyuu_teisi_flg = @hattyuu_teisi_flg")
            .AppendLine("	,kameiten_mei1 = @kameiten_mei1")
            .AppendLine("	,tenmei_kana1 = @tenmei_kana1")
            .AppendLine("	,kameiten_mei2 = @kameiten_mei2")
            .AppendLine("	,tenmei_kana2 = @tenmei_kana2")
            .AppendLine("	,kameiten_seisiki_mei = @kameiten_seisiki_mei")
            .AppendLine("	,kameiten_seisiki_mei_kana = @kameiten_seisiki_mei_kana")
            .AppendLine("	,eigyousyo_cd = @eigyousyo_cd")
            .AppendLine("	,keiretu_cd = @keiretu_cd")
            .AppendLine("	,tys_seikyuu_saki_cd = @tys_seikyuu_saki_cd")
            .AppendLine("	,tys_seikyuu_saki_brc = @tys_seikyuu_saki_brc")
            .AppendLine("	,tys_seikyuu_saki_kbn = @tys_seikyuu_saki_kbn")
            .AppendLine("	,koj_seikyuu_saki_cd = @koj_seikyuu_saki_cd")
            .AppendLine("	,koj_seikyuu_saki_brc = @koj_seikyuu_saki_brc")
            .AppendLine("	,koj_seikyuu_saki_kbn = @koj_seikyuu_saki_kbn")
            .AppendLine("	,hansokuhin_seikyuu_saki_cd = @hansokuhin_seikyuu_saki_cd")
            .AppendLine("	,hansokuhin_seikyuu_saki_brc = @hansokuhin_seikyuu_saki_brc")
            .AppendLine("	,hansokuhin_seikyuu_saki_kbn = @hansokuhin_seikyuu_saki_kbn")
            .AppendLine("	,tatemono_seikyuu_saki_cd = @tatemono_seikyuu_saki_cd")
            .AppendLine("	,tatemono_seikyuu_saki_brc = @tatemono_seikyuu_saki_brc")
            .AppendLine("	,tatemono_seikyuu_saki_kbn = @tatemono_seikyuu_saki_kbn")
            .AppendLine("	,seikyuu_saki_cd5 = @seikyuu_saki_cd5")
            .AppendLine("	,seikyuu_saki_brc5 = @seikyuu_saki_brc5")
            .AppendLine("	,seikyuu_saki_kbn5 = @seikyuu_saki_kbn5")
            .AppendLine("	,seikyuu_saki_cd6 = @seikyuu_saki_cd6")
            .AppendLine("	,seikyuu_saki_brc6 = @seikyuu_saki_brc6")
            .AppendLine("	,seikyuu_saki_kbn6 = @seikyuu_saki_kbn6")
            .AppendLine("	,seikyuu_saki_cd7 = @seikyuu_saki_cd7")
            .AppendLine("	,seikyuu_saki_brc7 = @seikyuu_saki_brc7")
            .AppendLine("	,seikyuu_saki_kbn7 = @seikyuu_saki_kbn7")
            .AppendLine("	,kaiyaku_haraimodosi_kkk = @kaiyaku_haraimodosi_kkk")
            .AppendLine("	,todouhuken_cd = @todouhuken_cd")
            '================↓2013/03/06 車龍 407584 追加↓========================
            .AppendLine("	,hosyou_kikan = @hosyou_kikan") '--保証期間
            .AppendLine("	,hosyousyo_hak_umu = @hosyousyo_hak_umu") '--保証書発行有無
            '================↑2013/03/06 車龍 407584 追加↑========================
            .AppendLine("	,builder_no = @builder_no")
            '================↓2013/03/06 車龍 407584 追加↓========================
            .AppendLine("	,nenkan_tousuu = @nenkan_tousuu") '--年間棟数
            '================↑2013/03/06 車龍 407584 追加↑========================
            .AppendLine("	,nyuukin_kakunin_jyouken = @nyuukin_kakunin_jyouken")
            .AppendLine("	,nyuukin_kakunin_oboegaki = @nyuukin_kakunin_oboegaki")
            .AppendLine("	,eigyou_tantousya_mei = @eigyou_tantousya_mei")
            .AppendLine("	,tys_mitsyo_flg = @tys_mitsyo_flg")
            .AppendLine("	,hattyuusyo_flg = @hattyuusyo_flg")
            .AppendLine("	,kyuu_eigyou_tantousya_mei = @kyuu_eigyou_tantousya_mei")
            .AppendLine("	,fuho_syoumeisyo_flg = @fuho_syoumeisyo_flg")
            .AppendLine("	,fuho_syoumeisyo_kaisi_nengetu = @fuho_syoumeisyo_kaisi_nengetu")
            '================↓2013/03/06 車龍 407584 追加↓========================
            .AppendLine("	,jiosaki_flg = @jiosaki_flg") '--JIO先FLG
            .AppendLine("	,koj_uri_syubetsu = @koj_uri_syubetsu") '--工事売上種別
            '================↑2013/03/06 車龍 407584 追加↑========================
            .AppendLine("	,upd_login_user_id = @upd_login_user_id")
            .AppendLine("	,upd_datetime = GETDATE()")


            .AppendLine(",ssgr_kkk=@ssgr_kkk") 'SSGR価格
            .AppendLine(",kaiseki_hosyou_kkk=@kaiseki_hosyou_kkk") '解析保証価格
            .AppendLine(",koj_mitiraisyo_soufu_fuyou=@koj_mitiraisyo_soufu_fuyou") '工事見積依頼書送付不要
            .AppendLine(",hikiwatasi_inji_umu=@hikiwatasi_inji_umu") '保証書引渡日印字有無
            .AppendLine(",hosyousyo_hassou_umu=@hosyousyo_hassou_umu") '保証書発送方法
            .AppendLine(",ekijyouka_tokuyaku_kakaku=@ekijyouka_tokuyaku_kakaku") '液状化特約費
            .AppendLine(",hosyousyo_hassou_umu_start_date=@hosyousyo_hassou_umu_start_date") '保証書発送方法_適用開始日
            .AppendLine(",taiou_syouhin_kbn=@taiou_syouhin_kbn") '対応商品区分
            .AppendLine(",taiou_syouhin_kbn_set_date=@taiou_syouhin_kbn_set_date") '対応商品区分設定日
            .AppendLine(",campaign_waribiki_flg=@campaign_waribiki_flg") 'キャンペーン割引FLG
            .AppendLine(",campaign_waribiki_set_date=@campaign_waribiki_set_date") 'キャンペーン割引設定日
            .AppendLine(",online_waribiki_flg=@online_waribiki_flg") 'オンライン割引FLG
            .AppendLine(",b_str_yuuryou_wide_flg=@b_str_yuuryou_wide_flg") 'B-STR有料ワイドFLG



            .AppendLine("WHERE ")
            .AppendLine("	kameiten_cd = @kameiten_cd ")
        End With

        Dim strKameiten As String
        Dim strUserId As String

        For i As Integer = 0 To dtOk.Rows.Count - 1
            'パラメータの設定
            paramList.Clear()
            paramList.Add(MakeParam("@kbn", SqlDbType.VarChar, 1, InsObj(dtOk.Rows(i).Item("kbn").ToString.Trim)))
            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, InsObj(dtOk.Rows(i).Item("kameiten_cd").ToString.Trim)))
            paramList.Add(MakeParam("@torikesi", SqlDbType.VarChar, 1, InsObj(dtOk.Rows(i).Item("torikesi").ToString.Trim)))
            paramList.Add(MakeParam("@hattyuu_teisi_flg", SqlDbType.VarChar, 10, InsObj(dtOk.Rows(i).Item("hattyuu_teisi_flg").ToString.Trim)))
            paramList.Add(MakeParam("@kameiten_mei1", SqlDbType.VarChar, 40, InsObj(dtOk.Rows(i).Item("kameiten_mei1").ToString.Trim)))
            paramList.Add(MakeParam("@tenmei_kana1", SqlDbType.VarChar, 20, InsObj(dtOk.Rows(i).Item("tenmei_kana1").ToString.Trim)))
            paramList.Add(MakeParam("@kameiten_mei2", SqlDbType.VarChar, 40, InsObj(dtOk.Rows(i).Item("kameiten_mei2").ToString.Trim)))
            paramList.Add(MakeParam("@tenmei_kana2", SqlDbType.VarChar, 20, InsObj(dtOk.Rows(i).Item("tenmei_kana2").ToString.Trim)))
            paramList.Add(MakeParam("@kameiten_seisiki_mei", SqlDbType.VarChar, 80, InsObj(dtOk.Rows(i).Item("kameiten_seisiki_mei").ToString.Trim)))
            paramList.Add(MakeParam("@kameiten_seisiki_mei_kana", SqlDbType.VarChar, 40, InsObj(dtOk.Rows(i).Item("kameiten_seisiki_mei_kana").ToString.Trim)))
            paramList.Add(MakeParam("@eigyousyo_cd", SqlDbType.VarChar, 5, InsObj(dtOk.Rows(i).Item("eigyousyo_cd").ToString.Trim)))
            paramList.Add(MakeParam("@keiretu_cd", SqlDbType.VarChar, 5, InsObj(dtOk.Rows(i).Item("keiretu_cd").ToString.Trim)))
            paramList.Add(MakeParam("@tys_seikyuu_saki_kbn", SqlDbType.VarChar, 1, InsObj(dtOk.Rows(i).Item("tys_seikyuu_saki_kbn").ToString.Trim)))
            paramList.Add(MakeParam("@tys_seikyuu_saki_cd", SqlDbType.VarChar, 5, InsObj(dtOk.Rows(i).Item("tys_seikyuu_saki_cd").ToString.Trim)))
            paramList.Add(MakeParam("@tys_seikyuu_saki_brc", SqlDbType.VarChar, 2, InsObj(dtOk.Rows(i).Item("tys_seikyuu_saki_brc").ToString.Trim)))
            paramList.Add(MakeParam("@koj_seikyuu_saki_kbn", SqlDbType.VarChar, 1, InsObj(dtOk.Rows(i).Item("koj_seikyuu_saki_kbn").ToString.Trim)))
            paramList.Add(MakeParam("@koj_seikyuu_saki_cd", SqlDbType.VarChar, 5, InsObj(dtOk.Rows(i).Item("koj_seikyuu_saki_cd").ToString.Trim)))
            paramList.Add(MakeParam("@koj_seikyuu_saki_brc", SqlDbType.VarChar, 2, InsObj(dtOk.Rows(i).Item("koj_seikyuu_saki_brc").ToString.Trim)))
            paramList.Add(MakeParam("@hansokuhin_seikyuu_saki_kbn", SqlDbType.VarChar, 1, InsObj(dtOk.Rows(i).Item("hansokuhin_seikyuu_saki_kbn").ToString.Trim)))
            paramList.Add(MakeParam("@hansokuhin_seikyuu_saki_cd", SqlDbType.VarChar, 5, InsObj(dtOk.Rows(i).Item("hansokuhin_seikyuu_saki_cd").ToString.Trim)))
            paramList.Add(MakeParam("@hansokuhin_seikyuu_saki_brc", SqlDbType.VarChar, 2, InsObj(dtOk.Rows(i).Item("hansokuhin_seikyuu_saki_brc").ToString.Trim)))
            paramList.Add(MakeParam("@tatemono_seikyuu_saki_kbn", SqlDbType.VarChar, 1, InsObj(dtOk.Rows(i).Item("tatemono_seikyuu_saki_kbn").ToString.Trim)))
            paramList.Add(MakeParam("@tatemono_seikyuu_saki_cd", SqlDbType.VarChar, 5, InsObj(dtOk.Rows(i).Item("tatemono_seikyuu_saki_cd").ToString.Trim)))
            paramList.Add(MakeParam("@tatemono_seikyuu_saki_brc", SqlDbType.VarChar, 2, InsObj(dtOk.Rows(i).Item("tatemono_seikyuu_saki_brc").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_kbn5", SqlDbType.VarChar, 1, InsObj(dtOk.Rows(i).Item("seikyuu_saki_kbn5").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_cd5", SqlDbType.VarChar, 5, InsObj(dtOk.Rows(i).Item("seikyuu_saki_cd5").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_brc5", SqlDbType.VarChar, 2, InsObj(dtOk.Rows(i).Item("seikyuu_saki_brc5").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_kbn6", SqlDbType.VarChar, 1, InsObj(dtOk.Rows(i).Item("seikyuu_saki_kbn6").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_cd6", SqlDbType.VarChar, 5, InsObj(dtOk.Rows(i).Item("seikyuu_saki_cd6").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_brc6", SqlDbType.VarChar, 2, InsObj(dtOk.Rows(i).Item("seikyuu_saki_brc6").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_kbn7", SqlDbType.VarChar, 1, InsObj(dtOk.Rows(i).Item("seikyuu_saki_kbn7").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_cd7", SqlDbType.VarChar, 5, InsObj(dtOk.Rows(i).Item("seikyuu_saki_cd7").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_brc7", SqlDbType.VarChar, 2, InsObj(dtOk.Rows(i).Item("seikyuu_saki_brc7").ToString.Trim)))
            paramList.Add(MakeParam("@kaiyaku_haraimodosi_kkk", SqlDbType.VarChar, 10, InsObj(dtOk.Rows(i).Item("kaiyaku_haraimodosi_kkk").ToString.Trim)))
            paramList.Add(MakeParam("@todouhuken_cd", SqlDbType.VarChar, 2, InsObj(dtOk.Rows(i).Item("todouhuken_cd").ToString.Trim)))
            '================↓2013/03/06 車龍 407584 追加↓========================
            paramList.Add(MakeParam("@hosyou_kikan", SqlDbType.Int, 10, InsObj(dtOk.Rows(i).Item("hosyou_kikan").ToString.Trim))) '--保証期間
            paramList.Add(MakeParam("@hosyousyo_hak_umu", SqlDbType.Int, 10, InsObj(dtOk.Rows(i).Item("hosyousyo_hak_umu").ToString.Trim))) '--保証書発行有無
            '================↑2013/03/06 車龍 407584 追加↑========================
            paramList.Add(MakeParam("@builder_no", SqlDbType.VarChar, 9, InsObj(dtOk.Rows(i).Item("builder_no").ToString.Trim)))
            '================↓2013/03/06 車龍 407584 追加↓========================
            paramList.Add(MakeParam("@nenkan_tousuu", SqlDbType.VarChar, 5, InsObj(dtOk.Rows(i).Item("nenkan_tousuu").ToString.Trim))) '--年間棟数
            '================↑2013/03/06 車龍 407584 追加↑========================
            paramList.Add(MakeParam("@nyuukin_kakunin_jyouken", SqlDbType.VarChar, 10, InsObj(dtOk.Rows(i).Item("nyuukin_kakunin_jyouken").ToString.Trim)))
            paramList.Add(MakeParam("@nyuukin_kakunin_oboegaki", SqlDbType.VarChar, 30, InsObj(dtOk.Rows(i).Item("nyuukin_kakunin_oboegaki").ToString.Trim)))
            paramList.Add(MakeParam("@eigyou_tantousya_mei", SqlDbType.VarChar, 16, InsObj(dtOk.Rows(i).Item("eigyou_tantousya_mei").ToString.Trim)))
            paramList.Add(MakeParam("@tys_mitsyo_flg", SqlDbType.VarChar, 10, InsObj(dtOk.Rows(i).Item("tys_mitsyo_flg").ToString.Trim)))
            paramList.Add(MakeParam("@hattyuusyo_flg", SqlDbType.VarChar, 10, InsObj(dtOk.Rows(i).Item("hattyuusyo_flg").ToString.Trim)))
            paramList.Add(MakeParam("@kyuu_eigyou_tantousya_mei", SqlDbType.VarChar, 16, InsObj(dtOk.Rows(i).Item("kyuu_eigyou_tantousya_mei").ToString.Trim)))
            paramList.Add(MakeParam("@fuho_syoumeisyo_flg", SqlDbType.VarChar, 1, InsObj(dtOk.Rows(i).Item("fuho_syoumeisyo_flg").ToString.Trim)))
            paramList.Add(MakeParam("@fuho_syoumeisyo_kaisi_nengetu", SqlDbType.VarChar, 30, InsObj(dtOk.Rows(i).Item("fuho_syoumeisyo_kaisi_nengetu").ToString.Trim)))
            '================↓2013/03/06 車龍 407584 追加↓========================
            paramList.Add(MakeParam("@jiosaki_flg", SqlDbType.Int, 10, InsObj(dtOk.Rows(i).Item("jiosaki_flg").ToString.Trim))) '--JIO先FLG
            paramList.Add(MakeParam("@koj_uri_syubetsu", SqlDbType.Int, 10, InsObj(dtOk.Rows(i).Item("koj_uri_syubetsu").ToString.Trim))) '--工事売上種別
            '================↑2013/03/06 車龍 407584 追加↑========================
            paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, InsObj(dtOk.Rows(i).Item("upd_login_user_id").ToString.Trim)))


            paramList.Add(MakeParam("@ssgr_kkk", SqlDbType.Int, 10, InsObj(dtOk.Rows(i).Item("ssgr_kkk").ToString.Trim))) 'SSGR価格
            paramList.Add(MakeParam("@kaiseki_hosyou_kkk", SqlDbType.Int, 10, InsObj(dtOk.Rows(i).Item("kaiseki_hosyou_kkk").ToString.Trim))) '解析保証価格
            paramList.Add(MakeParam("@koj_mitiraisyo_soufu_fuyou", SqlDbType.Int, 10, InsObj(dtOk.Rows(i).Item("koj_mitiraisyo_soufu_fuyou").ToString.Trim))) '工事見積依頼書送付不要
            paramList.Add(MakeParam("@hikiwatasi_inji_umu", SqlDbType.Int, 10, InsObj(dtOk.Rows(i).Item("hikiwatasi_inji_umu").ToString.Trim))) '保証書引渡日印字有無
            paramList.Add(MakeParam("@hosyousyo_hassou_umu", SqlDbType.Int, 10, InsObj(dtOk.Rows(i).Item("hosyousyo_hassou_umu").ToString.Trim))) '保証書発送方法
            paramList.Add(MakeParam("@ekijyouka_tokuyaku_kakaku", SqlDbType.Int, 10, InsObj(dtOk.Rows(i).Item("ekijyouka_tokuyaku_kakaku").ToString.Trim))) '液状化特約費
            paramList.Add(MakeParam("@hosyousyo_hassou_umu_start_date", SqlDbType.DateTime, 20, InsObj(dtOk.Rows(i).Item("hosyousyo_hassou_umu_start_date").ToString.Trim))) '保証書発送方法_適用開始日
            paramList.Add(MakeParam("@taiou_syouhin_kbn", SqlDbType.Int, 10, InsObj(dtOk.Rows(i).Item("taiou_syouhin_kbn").ToString.Trim))) '対応商品区分
            paramList.Add(MakeParam("@taiou_syouhin_kbn_set_date", SqlDbType.DateTime, 20, InsObj(dtOk.Rows(i).Item("taiou_syouhin_kbn_set_date").ToString.Trim))) '対応商品区分設定日
            paramList.Add(MakeParam("@campaign_waribiki_flg", SqlDbType.Int, 10, InsObj(dtOk.Rows(i).Item("campaign_waribiki_flg").ToString.Trim))) 'キャンペーン割引FLG
            paramList.Add(MakeParam("@campaign_waribiki_set_date", SqlDbType.DateTime, 20, InsObj(dtOk.Rows(i).Item("campaign_waribiki_set_date").ToString.Trim))) 'キャンペーン割引設定日
            paramList.Add(MakeParam("@online_waribiki_flg", SqlDbType.Int, 10, InsObj(dtOk.Rows(i).Item("online_waribiki_flg").ToString.Trim))) 'オンライン割引FLG
            paramList.Add(MakeParam("@b_str_yuuryou_wide_flg", SqlDbType.Int, 10, InsObj(dtOk.Rows(i).Item("b_str_yuuryou_wide_flg").ToString.Trim))) 'B-STR有料ワイドFLG



            Try

                '更新
                InsUpdCount = ExecuteNonQuery(connStr, CommandType.Text, strSqlUpd.ToString(), paramList.ToArray)

                If Not (InsUpdCount > 0) Then
                    Return False
                End If

                strKameiten = dtOk.Rows(i).Item("kameiten_cd").ToString.Trim
                strUserId = dtOk.Rows(i).Item("upd_login_user_id").ToString.Trim

                If Not InsUpdKameitenRenkei(strKameiten, strUserId) Then
                    Throw New ApplicationException
                End If

            Catch ex As Exception
                Return False
            End Try

        Next

        Return True
    End Function

    ''' <summary>加盟店マスタにデータを更新</summary>
    ''' <history>2012/02/12　陳琳(大連情報システム部)　新規作成</history>
    Public Function InsKameiten(ByVal dtOk As Data.DataTable) As Boolean
        '戻り値
        Dim InsUpdCount As Integer = 0

        '更新用sql文
        Dim strSqlIns As New System.Text.StringBuilder
        Dim strSqlUpd As New System.Text.StringBuilder
        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With strSqlIns
            .AppendLine("INSERT INTO ")
            .AppendLine("	m_kameiten WITH(UPDLOCK) ")
            .AppendLine("	( ")
            .AppendLine("	kbn ")
            .AppendLine("	,kameiten_cd ")
            .AppendLine("	,torikesi ")
            .AppendLine("	,hattyuu_teisi_flg ")
            .AppendLine("	,kameiten_mei1 ")
            .AppendLine("	,tenmei_kana1 ")
            .AppendLine("	,kameiten_mei2 ")
            .AppendLine("	,tenmei_kana2 ")
            .AppendLine("	,kameiten_seisiki_mei ")
            .AppendLine("	,kameiten_seisiki_mei_kana ")
            .AppendLine("	,eigyousyo_cd ")
            .AppendLine("	,keiretu_cd ")
            .AppendLine("	,tys_seikyuu_saki_cd ")
            .AppendLine("	,tys_seikyuu_saki_brc ")
            .AppendLine("	,tys_seikyuu_saki_kbn ")
            .AppendLine("	,koj_seikyuu_saki_cd ")
            .AppendLine("	,koj_seikyuu_saki_brc ")
            .AppendLine("	,koj_seikyuu_saki_kbn ")
            .AppendLine("	,hansokuhin_seikyuu_saki_cd ")
            .AppendLine("	,hansokuhin_seikyuu_saki_brc ")
            .AppendLine("	,hansokuhin_seikyuu_saki_kbn ")
            .AppendLine("	,tatemono_seikyuu_saki_cd ")
            .AppendLine("	,tatemono_seikyuu_saki_brc ")
            .AppendLine("	,tatemono_seikyuu_saki_kbn ")
            .AppendLine("	,seikyuu_saki_cd5 ")
            .AppendLine("	,seikyuu_saki_brc5 ")
            .AppendLine("	,seikyuu_saki_kbn5 ")
            .AppendLine("	,seikyuu_saki_cd6 ")
            .AppendLine("	,seikyuu_saki_brc6 ")
            .AppendLine("	,seikyuu_saki_kbn6 ")
            .AppendLine("	,seikyuu_saki_cd7 ")
            .AppendLine("	,seikyuu_saki_brc7 ")
            .AppendLine("	,seikyuu_saki_kbn7 ")
            .AppendLine("	,kaiyaku_haraimodosi_kkk ")
            .AppendLine("	,todouhuken_cd ")
            '================↓2013/03/06 車龍 407584 追加↓========================
            .AppendLine("	,hosyou_kikan ") '--保証期間
            .AppendLine("	,hosyousyo_hak_umu ") '--保証書発行有無
            '================↑2013/03/06 車龍 407584 追加↑========================
            .AppendLine("	,builder_no ")
            '================↓2013/03/06 車龍 407584 追加↓========================
            .AppendLine("	,nenkan_tousuu ") '--年間棟数
            '================↑2013/03/06 車龍 407584 追加↑========================
            .AppendLine("	,nyuukin_kakunin_jyouken ")
            .AppendLine("	,nyuukin_kakunin_oboegaki ")
            .AppendLine("	,eigyou_tantousya_mei ")
            .AppendLine("	,tys_mitsyo_flg ")
            .AppendLine("	,hattyuusyo_flg ")
            .AppendLine("	,kyuu_eigyou_tantousya_mei ")
            .AppendLine("	,fuho_syoumeisyo_flg ")
            .AppendLine("	,fuho_syoumeisyo_kaisi_nengetu ")
            '================↓2013/03/06 車龍 407584 追加↓========================
            .AppendLine("	,jiosaki_flg ") '--JIO先FLG
            .AppendLine("	,koj_uri_syubetsu ") '--工事売上種別
            '================↑2013/03/06 車龍 407584 追加↑========================
            .AppendLine("	,add_login_user_id ")
            .AppendLine("	,add_datetime ")

            .AppendLine(",ssgr_kkk")
            .AppendLine(",kaiseki_hosyou_kkk")
            .AppendLine(",koj_mitiraisyo_soufu_fuyou")
            .AppendLine(",hikiwatasi_inji_umu")
            .AppendLine(",hosyousyo_hassou_umu")
            .AppendLine(",ekijyouka_tokuyaku_kakaku")
            .AppendLine(",hosyousyo_hassou_umu_start_date")
            .AppendLine(",taiou_syouhin_kbn")
            .AppendLine(",taiou_syouhin_kbn_set_date")
            .AppendLine(",campaign_waribiki_flg")
            .AppendLine(",campaign_waribiki_set_date")
            .AppendLine(",online_waribiki_flg")
            .AppendLine(",b_str_yuuryou_wide_flg")

            .AppendLine("	) ")
            .AppendLine("VALUES ")
            .AppendLine("	( ")
            .AppendLine("	 @kbn")
            .AppendLine("	, @kameiten_cd")
            .AppendLine("	, @torikesi")
            .AppendLine("	, @hattyuu_teisi_flg")
            .AppendLine("	, @kameiten_mei1")
            .AppendLine("	, @tenmei_kana1")
            .AppendLine("	, @kameiten_mei2")
            .AppendLine("	, @tenmei_kana2")
            .AppendLine("	, @kameiten_seisiki_mei")
            .AppendLine("	, @kameiten_seisiki_mei_kana")
            .AppendLine("	, @eigyousyo_cd")
            .AppendLine("	, @keiretu_cd")
            .AppendLine("	, @tys_seikyuu_saki_cd")
            .AppendLine("	, @tys_seikyuu_saki_brc")
            .AppendLine("	, @tys_seikyuu_saki_kbn")
            .AppendLine("	, @koj_seikyuu_saki_cd")
            .AppendLine("	, @koj_seikyuu_saki_brc")
            .AppendLine("	, @koj_seikyuu_saki_kbn")
            .AppendLine("	, @hansokuhin_seikyuu_saki_cd")
            .AppendLine("	, @hansokuhin_seikyuu_saki_brc")
            .AppendLine("	, @hansokuhin_seikyuu_saki_kbn")
            .AppendLine("	, @tatemono_seikyuu_saki_cd")
            .AppendLine("	, @tatemono_seikyuu_saki_brc")
            .AppendLine("	, @tatemono_seikyuu_saki_kbn")
            .AppendLine("	, @seikyuu_saki_cd5")
            .AppendLine("	, @seikyuu_saki_brc5")
            .AppendLine("	, @seikyuu_saki_kbn5")
            .AppendLine("	, @seikyuu_saki_cd6")
            .AppendLine("	, @seikyuu_saki_brc6")
            .AppendLine("	, @seikyuu_saki_kbn6")
            .AppendLine("	, @seikyuu_saki_cd7")
            .AppendLine("	, @seikyuu_saki_brc7")
            .AppendLine("	, @seikyuu_saki_kbn7")
            .AppendLine("	, @kaiyaku_haraimodosi_kkk")
            .AppendLine("	, @todouhuken_cd")
            '================↓2013/03/06 車龍 407584 追加↓========================
            .AppendLine("	, @hosyou_kikan ") '--保証期間
            .AppendLine("	, @hosyousyo_hak_umu ") '--保証書発行有無
            '================↑2013/03/06 車龍 407584 追加↑========================
            .AppendLine("	, @builder_no")
            '================↓2013/03/06 車龍 407584 追加↓========================
            .AppendLine("	, @nenkan_tousuu ") '--年間棟数
            '================↑2013/03/06 車龍 407584 追加↑========================
            .AppendLine("	, @nyuukin_kakunin_jyouken")
            .AppendLine("	, @nyuukin_kakunin_oboegaki")
            .AppendLine("	, @eigyou_tantousya_mei")
            .AppendLine("	, @tys_mitsyo_flg")
            .AppendLine("	, @hattyuusyo_flg")
            .AppendLine("	, @kyuu_eigyou_tantousya_mei")
            .AppendLine("	, @fuho_syoumeisyo_flg")
            .AppendLine("	, @fuho_syoumeisyo_kaisi_nengetu")
            '================↓2013/03/06 車龍 407584 追加↓========================
            .AppendLine("	, @jiosaki_flg ") '--JIO先FLG
            .AppendLine("	, @koj_uri_syubetsu ") '--工事売上種別
            '================↑2013/03/06 車龍 407584 追加↑========================
            .AppendLine("	, @upd_login_user_id")
            .AppendLine("	 , GETDATE()")
            .AppendLine(",@ssgr_kkk")
            .AppendLine(",@kaiseki_hosyou_kkk")
            .AppendLine(",@koj_mitiraisyo_soufu_fuyou")
            .AppendLine(",@hikiwatasi_inji_umu")
            .AppendLine(",@hosyousyo_hassou_umu")
            .AppendLine(",@ekijyouka_tokuyaku_kakaku")
            .AppendLine(",@hosyousyo_hassou_umu_start_date")
            .AppendLine(",@taiou_syouhin_kbn")
            .AppendLine(",@taiou_syouhin_kbn_set_date")
            .AppendLine(",@campaign_waribiki_flg")
            .AppendLine(",@campaign_waribiki_set_date")
            .AppendLine(",@online_waribiki_flg")
            .AppendLine(",@b_str_yuuryou_wide_flg")
            .AppendLine("	) ")
        End With
        With strSqlUpd
            .AppendLine("UPDATE ")
            .AppendLine("	m_kameiten WITH(UPDLOCK) ")
            .AppendLine("SET ")
            .AppendLine("	kbn = @kbn")
            .AppendLine("	,torikesi = @torikesi")
            .AppendLine("	,hattyuu_teisi_flg = @hattyuu_teisi_flg")
            .AppendLine("	,kameiten_mei1 = @kameiten_mei1")
            .AppendLine("	,tenmei_kana1 = @tenmei_kana1")
            .AppendLine("	,kameiten_mei2 = @kameiten_mei2")
            .AppendLine("	,tenmei_kana2 = @tenmei_kana2")
            .AppendLine("	,kameiten_seisiki_mei = @kameiten_seisiki_mei")
            .AppendLine("	,kameiten_seisiki_mei_kana = @kameiten_seisiki_mei_kana")
            .AppendLine("	,eigyousyo_cd = @eigyousyo_cd")
            .AppendLine("	,keiretu_cd = @keiretu_cd")
            .AppendLine("	,tys_seikyuu_saki_cd = @tys_seikyuu_saki_cd")
            .AppendLine("	,tys_seikyuu_saki_brc = @tys_seikyuu_saki_brc")
            .AppendLine("	,tys_seikyuu_saki_kbn = @tys_seikyuu_saki_kbn")
            .AppendLine("	,koj_seikyuu_saki_cd = @koj_seikyuu_saki_cd")
            .AppendLine("	,koj_seikyuu_saki_brc = @koj_seikyuu_saki_brc")
            .AppendLine("	,koj_seikyuu_saki_kbn = @koj_seikyuu_saki_kbn")
            .AppendLine("	,hansokuhin_seikyuu_saki_cd = @hansokuhin_seikyuu_saki_cd")
            .AppendLine("	,hansokuhin_seikyuu_saki_brc = @hansokuhin_seikyuu_saki_brc")
            .AppendLine("	,hansokuhin_seikyuu_saki_kbn = @hansokuhin_seikyuu_saki_kbn")
            .AppendLine("	,tatemono_seikyuu_saki_cd = @tatemono_seikyuu_saki_cd")
            .AppendLine("	,tatemono_seikyuu_saki_brc = @tatemono_seikyuu_saki_brc")
            .AppendLine("	,tatemono_seikyuu_saki_kbn = @tatemono_seikyuu_saki_kbn")
            .AppendLine("	,seikyuu_saki_cd5 = @seikyuu_saki_cd5")
            .AppendLine("	,seikyuu_saki_brc5 = @seikyuu_saki_brc5")
            .AppendLine("	,seikyuu_saki_kbn5 = @seikyuu_saki_kbn5")
            .AppendLine("	,seikyuu_saki_cd6 = @seikyuu_saki_cd6")
            .AppendLine("	,seikyuu_saki_brc6 = @seikyuu_saki_brc6")
            .AppendLine("	,seikyuu_saki_kbn6 = @seikyuu_saki_kbn6")
            .AppendLine("	,seikyuu_saki_cd7 = @seikyuu_saki_cd7")
            .AppendLine("	,seikyuu_saki_brc7 = @seikyuu_saki_brc7")
            .AppendLine("	,seikyuu_saki_kbn7 = @seikyuu_saki_kbn7")
            .AppendLine("	,kaiyaku_haraimodosi_kkk = @kaiyaku_haraimodosi_kkk")
            .AppendLine("	,todouhuken_cd = @todouhuken_cd")
            '================↓2013/03/06 車龍 407584 追加↓========================
            .AppendLine("	,hosyou_kikan = @hosyou_kikan") '--保証期間
            .AppendLine("	,hosyousyo_hak_umu = @hosyousyo_hak_umu") '--保証書発行有無
            '================↑2013/03/06 車龍 407584 追加↑========================
            .AppendLine("	,builder_no = @builder_no")
            '================↓2013/03/06 車龍 407584 追加↓========================
            .AppendLine("	,nenkan_tousuu = @nenkan_tousuu ") '--年間棟数
            '================↑2013/03/06 車龍 407584 追加↑========================
            .AppendLine("	,nyuukin_kakunin_jyouken = @nyuukin_kakunin_jyouken")
            .AppendLine("	,nyuukin_kakunin_oboegaki = @nyuukin_kakunin_oboegaki")
            .AppendLine("	,eigyou_tantousya_mei = @eigyou_tantousya_mei")
            .AppendLine("	,tys_mitsyo_flg = @tys_mitsyo_flg")
            .AppendLine("	,hattyuusyo_flg = @hattyuusyo_flg")
            .AppendLine("	,kyuu_eigyou_tantousya_mei = @kyuu_eigyou_tantousya_mei")
            .AppendLine("	,fuho_syoumeisyo_flg = @fuho_syoumeisyo_flg")
            .AppendLine("	,fuho_syoumeisyo_kaisi_nengetu = @fuho_syoumeisyo_kaisi_nengetu")
            '================↓2013/03/06 車龍 407584 追加↓========================
            .AppendLine("	,jiosaki_flg = @jiosaki_flg ") '--JIO先FLG
            .AppendLine("	,koj_uri_syubetsu = @koj_uri_syubetsu ") '--工事売上種別
            '================↑2013/03/06 車龍 407584 追加↑========================
            .AppendLine("	,upd_login_user_id = @upd_login_user_id")
            .AppendLine("	,upd_datetime = GETDATE()")

            .AppendLine(",ssgr_kkk=@ssgr_kkk") 'SSGR価格
            .AppendLine(",kaiseki_hosyou_kkk=@kaiseki_hosyou_kkk") '解析保証価格
            .AppendLine(",koj_mitiraisyo_soufu_fuyou=@koj_mitiraisyo_soufu_fuyou") '工事見積依頼書送付不要
            .AppendLine(",hikiwatasi_inji_umu=@hikiwatasi_inji_umu") '保証書引渡日印字有無
            .AppendLine(",hosyousyo_hassou_umu=@hosyousyo_hassou_umu") '保証書発送方法
            .AppendLine(",ekijyouka_tokuyaku_kakaku=@ekijyouka_tokuyaku_kakaku") '液状化特約費
            .AppendLine(",hosyousyo_hassou_umu_start_date=@hosyousyo_hassou_umu_start_date") '保証書発送方法_適用開始日
            .AppendLine(",taiou_syouhin_kbn=@taiou_syouhin_kbn") '対応商品区分
            .AppendLine(",taiou_syouhin_kbn_set_date=@taiou_syouhin_kbn_set_date") '対応商品区分設定日
            .AppendLine(",campaign_waribiki_flg=@campaign_waribiki_flg") 'キャンペーン割引FLG
            .AppendLine(",campaign_waribiki_set_date=@campaign_waribiki_set_date") 'キャンペーン割引設定日
            .AppendLine(",online_waribiki_flg=@online_waribiki_flg") 'オンライン割引FLG
            .AppendLine(",b_str_yuuryou_wide_flg=@b_str_yuuryou_wide_flg") 'B-STR有料ワイドFLG


            .AppendLine("WHERE ")
            .AppendLine("	kameiten_cd = @kameiten_cd ")
        End With

        Dim strKameiten As String
        Dim strUserId As String

        For i As Integer = 0 To dtOk.Rows.Count - 1
            'パラメータの設定
            paramList.Clear()
            paramList.Add(MakeParam("@kbn", SqlDbType.VarChar, 1, InsObj(dtOk.Rows(i).Item("kbn").ToString.Trim)))
            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, InsObj(dtOk.Rows(i).Item("kameiten_cd").ToString.Trim)))
            paramList.Add(MakeParam("@torikesi", SqlDbType.VarChar, 1, InsObj(dtOk.Rows(i).Item("torikesi").ToString.Trim)))
            paramList.Add(MakeParam("@hattyuu_teisi_flg", SqlDbType.VarChar, 10, InsObj(dtOk.Rows(i).Item("hattyuu_teisi_flg").ToString.Trim)))
            paramList.Add(MakeParam("@kameiten_mei1", SqlDbType.VarChar, 40, InsObj(dtOk.Rows(i).Item("kameiten_mei1").ToString.Trim)))
            paramList.Add(MakeParam("@tenmei_kana1", SqlDbType.VarChar, 20, InsObj(dtOk.Rows(i).Item("tenmei_kana1").ToString.Trim)))
            paramList.Add(MakeParam("@kameiten_mei2", SqlDbType.VarChar, 40, InsObj(dtOk.Rows(i).Item("kameiten_mei2").ToString.Trim)))
            paramList.Add(MakeParam("@tenmei_kana2", SqlDbType.VarChar, 20, InsObj(dtOk.Rows(i).Item("tenmei_kana2").ToString.Trim)))
            paramList.Add(MakeParam("@kameiten_seisiki_mei", SqlDbType.VarChar, 80, InsObj(dtOk.Rows(i).Item("kameiten_seisiki_mei").ToString.Trim)))
            paramList.Add(MakeParam("@kameiten_seisiki_mei_kana", SqlDbType.VarChar, 40, InsObj(dtOk.Rows(i).Item("kameiten_seisiki_mei_kana").ToString.Trim)))
            paramList.Add(MakeParam("@eigyousyo_cd", SqlDbType.VarChar, 5, InsObj(dtOk.Rows(i).Item("eigyousyo_cd").ToString.Trim)))
            paramList.Add(MakeParam("@keiretu_cd", SqlDbType.VarChar, 5, InsObj(dtOk.Rows(i).Item("keiretu_cd").ToString.Trim)))
            paramList.Add(MakeParam("@tys_seikyuu_saki_kbn", SqlDbType.VarChar, 1, InsObj(dtOk.Rows(i).Item("tys_seikyuu_saki_kbn").ToString.Trim)))
            paramList.Add(MakeParam("@tys_seikyuu_saki_cd", SqlDbType.VarChar, 5, InsObj(dtOk.Rows(i).Item("tys_seikyuu_saki_cd").ToString.Trim)))
            paramList.Add(MakeParam("@tys_seikyuu_saki_brc", SqlDbType.VarChar, 2, InsObj(dtOk.Rows(i).Item("tys_seikyuu_saki_brc").ToString.Trim)))
            paramList.Add(MakeParam("@koj_seikyuu_saki_kbn", SqlDbType.VarChar, 1, InsObj(dtOk.Rows(i).Item("koj_seikyuu_saki_kbn").ToString.Trim)))
            paramList.Add(MakeParam("@koj_seikyuu_saki_cd", SqlDbType.VarChar, 5, InsObj(dtOk.Rows(i).Item("koj_seikyuu_saki_cd").ToString.Trim)))
            paramList.Add(MakeParam("@koj_seikyuu_saki_brc", SqlDbType.VarChar, 2, InsObj(dtOk.Rows(i).Item("koj_seikyuu_saki_brc").ToString.Trim)))
            paramList.Add(MakeParam("@hansokuhin_seikyuu_saki_kbn", SqlDbType.VarChar, 1, InsObj(dtOk.Rows(i).Item("hansokuhin_seikyuu_saki_kbn").ToString.Trim)))
            paramList.Add(MakeParam("@hansokuhin_seikyuu_saki_cd", SqlDbType.VarChar, 5, InsObj(dtOk.Rows(i).Item("hansokuhin_seikyuu_saki_cd").ToString.Trim)))
            paramList.Add(MakeParam("@hansokuhin_seikyuu_saki_brc", SqlDbType.VarChar, 2, InsObj(dtOk.Rows(i).Item("hansokuhin_seikyuu_saki_brc").ToString.Trim)))
            paramList.Add(MakeParam("@tatemono_seikyuu_saki_kbn", SqlDbType.VarChar, 1, InsObj(dtOk.Rows(i).Item("tatemono_seikyuu_saki_kbn").ToString.Trim)))
            paramList.Add(MakeParam("@tatemono_seikyuu_saki_cd", SqlDbType.VarChar, 5, InsObj(dtOk.Rows(i).Item("tatemono_seikyuu_saki_cd").ToString.Trim)))
            paramList.Add(MakeParam("@tatemono_seikyuu_saki_brc", SqlDbType.VarChar, 2, InsObj(dtOk.Rows(i).Item("tatemono_seikyuu_saki_brc").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_kbn5", SqlDbType.VarChar, 1, InsObj(dtOk.Rows(i).Item("seikyuu_saki_kbn5").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_cd5", SqlDbType.VarChar, 5, InsObj(dtOk.Rows(i).Item("seikyuu_saki_cd5").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_brc5", SqlDbType.VarChar, 2, InsObj(dtOk.Rows(i).Item("seikyuu_saki_brc5").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_kbn6", SqlDbType.VarChar, 1, InsObj(dtOk.Rows(i).Item("seikyuu_saki_kbn6").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_cd6", SqlDbType.VarChar, 5, InsObj(dtOk.Rows(i).Item("seikyuu_saki_cd6").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_brc6", SqlDbType.VarChar, 2, InsObj(dtOk.Rows(i).Item("seikyuu_saki_brc6").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_kbn7", SqlDbType.VarChar, 1, InsObj(dtOk.Rows(i).Item("seikyuu_saki_kbn7").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_cd7", SqlDbType.VarChar, 5, InsObj(dtOk.Rows(i).Item("seikyuu_saki_cd7").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_brc7", SqlDbType.VarChar, 2, InsObj(dtOk.Rows(i).Item("seikyuu_saki_brc7").ToString.Trim)))
            paramList.Add(MakeParam("@kaiyaku_haraimodosi_kkk", SqlDbType.VarChar, 10, InsObj(dtOk.Rows(i).Item("kaiyaku_haraimodosi_kkk").ToString.Trim)))
            paramList.Add(MakeParam("@todouhuken_cd", SqlDbType.VarChar, 2, InsObj(dtOk.Rows(i).Item("todouhuken_cd").ToString.Trim)))
            '================↓2013/03/06 車龍 407584 追加↓========================
            paramList.Add(MakeParam("@hosyou_kikan", SqlDbType.Int, 10, InsObj(dtOk.Rows(i).Item("hosyou_kikan").ToString.Trim))) '--保証期間
            paramList.Add(MakeParam("@hosyousyo_hak_umu", SqlDbType.Int, 10, InsObj(dtOk.Rows(i).Item("hosyousyo_hak_umu").ToString.Trim))) '--保証書発行有無
            '================↑2013/03/06 車龍 407584 追加↑========================
            paramList.Add(MakeParam("@builder_no", SqlDbType.VarChar, 9, InsObj(dtOk.Rows(i).Item("builder_no").ToString.Trim)))
            '================↓2013/03/06 車龍 407584 追加↓========================
            paramList.Add(MakeParam("@nenkan_tousuu", SqlDbType.VarChar, 5, InsObj(dtOk.Rows(i).Item("nenkan_tousuu").ToString.Trim))) '--年間棟数
            '================↑2013/03/06 車龍 407584 追加↑========================
            paramList.Add(MakeParam("@nyuukin_kakunin_jyouken", SqlDbType.VarChar, 10, InsObj(dtOk.Rows(i).Item("nyuukin_kakunin_jyouken").ToString.Trim)))
            paramList.Add(MakeParam("@nyuukin_kakunin_oboegaki", SqlDbType.VarChar, 30, InsObj(dtOk.Rows(i).Item("nyuukin_kakunin_oboegaki").ToString.Trim)))
            paramList.Add(MakeParam("@eigyou_tantousya_mei", SqlDbType.VarChar, 16, InsObj(dtOk.Rows(i).Item("eigyou_tantousya_mei").ToString.Trim)))
            paramList.Add(MakeParam("@tys_mitsyo_flg", SqlDbType.VarChar, 10, InsObj(dtOk.Rows(i).Item("tys_mitsyo_flg").ToString.Trim)))
            paramList.Add(MakeParam("@hattyuusyo_flg", SqlDbType.VarChar, 10, InsObj(dtOk.Rows(i).Item("hattyuusyo_flg").ToString.Trim)))
            paramList.Add(MakeParam("@kyuu_eigyou_tantousya_mei", SqlDbType.VarChar, 16, InsObj(dtOk.Rows(i).Item("kyuu_eigyou_tantousya_mei").ToString.Trim)))
            paramList.Add(MakeParam("@fuho_syoumeisyo_flg", SqlDbType.VarChar, 1, InsObj(dtOk.Rows(i).Item("fuho_syoumeisyo_flg").ToString.Trim)))
            paramList.Add(MakeParam("@fuho_syoumeisyo_kaisi_nengetu", SqlDbType.VarChar, 30, InsObj(dtOk.Rows(i).Item("fuho_syoumeisyo_kaisi_nengetu").ToString.Trim)))
            '================↓2013/03/06 車龍 407584 追加↓========================
            paramList.Add(MakeParam("@jiosaki_flg", SqlDbType.Int, 10, InsObj(dtOk.Rows(i).Item("jiosaki_flg").ToString.Trim))) '--JIO先FLG
            paramList.Add(MakeParam("@koj_uri_syubetsu", SqlDbType.Int, 10, InsObj(dtOk.Rows(i).Item("koj_uri_syubetsu").ToString.Trim))) '--工事売上種別
            '================↑2013/03/06 車龍 407584 追加↑========================
            paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, InsObj(dtOk.Rows(i).Item("upd_login_user_id").ToString.Trim)))



            paramList.Add(MakeParam("@ssgr_kkk", SqlDbType.Int, 10, InsObj(dtOk.Rows(i).Item("ssgr_kkk").ToString.Trim))) 'SSGR価格
            paramList.Add(MakeParam("@kaiseki_hosyou_kkk", SqlDbType.Int, 10, InsObj(dtOk.Rows(i).Item("kaiseki_hosyou_kkk").ToString.Trim))) '解析保証価格
            paramList.Add(MakeParam("@koj_mitiraisyo_soufu_fuyou", SqlDbType.Int, 10, InsObj(dtOk.Rows(i).Item("koj_mitiraisyo_soufu_fuyou").ToString.Trim))) '工事見積依頼書送付不要
            paramList.Add(MakeParam("@hikiwatasi_inji_umu", SqlDbType.Int, 10, InsObj(dtOk.Rows(i).Item("hikiwatasi_inji_umu").ToString.Trim))) '保証書引渡日印字有無
            paramList.Add(MakeParam("@hosyousyo_hassou_umu", SqlDbType.Int, 10, InsObj(dtOk.Rows(i).Item("hosyousyo_hassou_umu").ToString.Trim))) '保証書発送方法
            paramList.Add(MakeParam("@ekijyouka_tokuyaku_kakaku", SqlDbType.Int, 10, InsObj(dtOk.Rows(i).Item("ekijyouka_tokuyaku_kakaku").ToString.Trim))) '液状化特約費
            paramList.Add(MakeParam("@hosyousyo_hassou_umu_start_date", SqlDbType.DateTime, 20, InsObj(dtOk.Rows(i).Item("hosyousyo_hassou_umu_start_date").ToString.Trim))) '保証書発送方法_適用開始日
            paramList.Add(MakeParam("@taiou_syouhin_kbn", SqlDbType.Int, 10, InsObj(dtOk.Rows(i).Item("taiou_syouhin_kbn").ToString.Trim))) '対応商品区分
            paramList.Add(MakeParam("@taiou_syouhin_kbn_set_date", SqlDbType.DateTime, 20, InsObj(dtOk.Rows(i).Item("taiou_syouhin_kbn_set_date").ToString.Trim))) '対応商品区分設定日
            paramList.Add(MakeParam("@campaign_waribiki_flg", SqlDbType.Int, 10, InsObj(dtOk.Rows(i).Item("campaign_waribiki_flg").ToString.Trim))) 'キャンペーン割引FLG
            paramList.Add(MakeParam("@campaign_waribiki_set_date", SqlDbType.DateTime, 20, InsObj(dtOk.Rows(i).Item("campaign_waribiki_set_date").ToString.Trim))) 'キャンペーン割引設定日
            paramList.Add(MakeParam("@online_waribiki_flg", SqlDbType.Int, 10, InsObj(dtOk.Rows(i).Item("online_waribiki_flg").ToString.Trim))) 'オンライン割引FLG
            paramList.Add(MakeParam("@b_str_yuuryou_wide_flg", SqlDbType.Int, 10, InsObj(dtOk.Rows(i).Item("b_str_yuuryou_wide_flg").ToString.Trim))) 'B-STR有料ワイドFLG


            Try

                strKameiten = dtOk.Rows(i).Item("kameiten_cd").ToString.Trim
                strUserId = dtOk.Rows(i).Item("upd_login_user_id").ToString.Trim

                '更新
                If SelKameitenCd(dtOk.Rows(i).Item("kameiten_cd").ToString.Trim) Then
                    InsUpdCount = ExecuteNonQuery(connStr, CommandType.Text, strSqlUpd.ToString(), paramList.ToArray)

                    If Not InsUpdKameitenRenkei(strKameiten, strUserId) Then
                        Throw New ApplicationException
                    End If
                Else
                    InsUpdCount = ExecuteNonQuery(connStr, CommandType.Text, strSqlIns.ToString(), paramList.ToArray)

                    If Not InsUpdKameitenRenkei(strKameiten, strUserId) Then
                        Throw New ApplicationException
                    End If
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

    ''' <summary>加盟店住所マスタタにデータを更新</summary>
    ''' <history>2012/02/13　陳琳(大連情報システム部)　新規作成</history>
    Public Function UpdKameitenJyuusyo(ByVal dtOk As Data.DataTable) As Boolean
        '戻り値
        Dim InsUpdCount As Integer = 0

        '更新用sql文
        Dim strSqlUpd As New System.Text.StringBuilder
        Dim strSqlIns As New System.Text.StringBuilder
        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With strSqlUpd
            .AppendLine("UPDATE ")
            .AppendLine("	m_kameiten_jyuusyo WITH(UPDLOCK) ")
            .AppendLine("SET ")
            .AppendLine("	todouhuken_cd = @todouhuken_cd")
            .AppendLine("	,jyuusyo1 = @jyuusyo1")
            .AppendLine("	,jyuusyo2 = @jyuusyo2")
            .AppendLine("	,yuubin_no = @yuubin_no")
            .AppendLine("	,tel_no = @tel_no")
            .AppendLine("	,fax_no = @fax_no")
            .AppendLine("	,busyo_mei = @busyo_mei")
            .AppendLine("	,daihyousya_mei = @daihyousya_mei")
            .AppendLine("	,bikou1 = @bikou1")
            .AppendLine("	,bikou2 = @bikou2")
            .AppendLine("	,mail_address = @mail_address")
            .AppendLine("	,upd_login_user_id = @upd_login_user_id")
            .AppendLine("	,upd_datetime = GETDATE()")
            .AppendLine("WHERE ")
            .AppendLine("	kameiten_cd = @kameiten_cd ")
            .AppendLine("	AND jyuusyo_no = '1' ")
        End With
        With strSqlIns
            .AppendLine("INSERT INTO ")
            .AppendLine("	m_kameiten_jyuusyo WITH(UPDLOCK) ")
            .AppendLine("	( ")
            .AppendLine("kameiten_cd ")
            .AppendLine(",jyuusyo_no ")
            .AppendLine(",todouhuken_cd ")
            .AppendLine(",jyuusyo1 ")
            .AppendLine(", jyuusyo2 ")
            .AppendLine(", yuubin_no ")
            .AppendLine(", tel_no ")
            .AppendLine(", fax_no ")
            .AppendLine(", busyo_mei ")
            .AppendLine(", daihyousya_mei ")
            '==========↓2013/03/08 車龍 407584 追加↓======================
            .AppendLine(", add_nengetu ") '--登録年月
            .AppendLine(", seikyuusyo_flg ") '--請求書FLG
            .AppendLine(", hosyousyo_flg ") '--保証書FLG
            .AppendLine(", hkks_flg ") '--報告書FLG
            .AppendLine(", teiki_kankou_flg ") '--定期刊行FLG
            '==========↑2013/03/08 車龍 407584 追加↑======================
            .AppendLine(", bikou1 ")
            .AppendLine(", bikou2 ")
            .AppendLine(", mail_address ")
            '==========↓2013/03/08 車龍 407584 追加↓======================
            .AppendLine(", kasi_hosyousyo_flg ") '--瑕疵保証書FLG
            .AppendLine(", koj_hkks_flg ") '--工事報告書FLG
            .AppendLine(", kensa_hkks_flg ") '--検査報告書FLG
            '==========↑2013/03/08 車龍 407584 追加↑======================
            .AppendLine(", add_login_user_id ")
            .AppendLine(", add_datetime ")
            .AppendLine("	) ")
            .AppendLine("VALUES ")
            .AppendLine("	( ")
            .AppendLine(" @kameiten_cd ")
            .AppendLine(",1 ")
            .AppendLine(", @todouhuken_cd ")
            .AppendLine(", @jyuusyo1 ")
            .AppendLine(", @jyuusyo2 ")
            .AppendLine(", @yuubin_no ")
            .AppendLine(", @tel_no ")
            .AppendLine(", @fax_no ")
            .AppendLine(", @busyo_mei ")
            .AppendLine(", @daihyousya_mei ")
            '==========↓2013/03/08 車龍 407584 追加↓======================
            .AppendLine(", LEFT(CONVERT(VARCHAR(10),GETDATE(),111),7) ") '--登録年月
            .AppendLine(", '-1' ") '--請求書FLG
            .AppendLine(", '-1' ") '--保証書FLG
            .AppendLine(", '-1' ") '--報告書FLG
            .AppendLine(", '-1' ") '--定期刊行FLG
            '==========↑2013/03/08 車龍 407584 追加↑======================
            .AppendLine(", @bikou1 ")
            .AppendLine(", @bikou2 ")
            .AppendLine(", @mail_address ")
            '==========↓2013/03/08 車龍 407584 追加↓======================
            .AppendLine(", '-1' ") '--瑕疵保証書FLG
            .AppendLine(", '-1' ") '--工事報告書FLG
            .AppendLine(", '-1' ") '--検査報告書FLG
            '==========↑2013/03/08 車龍 407584 追加↑======================
            .AppendLine(", @upd_login_user_id")
            .AppendLine(", GETDATE()")
            .AppendLine("	) ")

        End With

        Dim strKameiten As String
        Dim strUserId As String

        For i As Integer = 0 To dtOk.Rows.Count - 1
            'パラメータの設定
            paramList.Clear()
            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, InsObj(dtOk.Rows(i).Item("kameiten_cd").ToString.Trim)))
            paramList.Add(MakeParam("@todouhuken_cd", SqlDbType.VarChar, 2, InsObj(dtOk.Rows(i).Item("todouhuken_cd").ToString.Trim)))
            paramList.Add(MakeParam("@jyuusyo1", SqlDbType.VarChar, 40, InsObj(dtOk.Rows(i).Item("jyuusyo1").ToString.Trim)))
            paramList.Add(MakeParam("@jyuusyo2", SqlDbType.VarChar, 30, InsObj(dtOk.Rows(i).Item("jyuusyo2").ToString.Trim)))
            paramList.Add(MakeParam("@yuubin_no", SqlDbType.VarChar, 8, InsObj(dtOk.Rows(i).Item("yuubin_no").ToString.Trim)))
            paramList.Add(MakeParam("@tel_no", SqlDbType.VarChar, 16, InsObj(dtOk.Rows(i).Item("tel_no").ToString.Trim)))
            paramList.Add(MakeParam("@fax_no", SqlDbType.VarChar, 16, InsObj(dtOk.Rows(i).Item("fax_no").ToString.Trim)))
            paramList.Add(MakeParam("@busyo_mei", SqlDbType.VarChar, 50, InsObj(dtOk.Rows(i).Item("busyo_mei").ToString.Trim)))
            paramList.Add(MakeParam("@daihyousya_mei", SqlDbType.VarChar, 20, InsObj(dtOk.Rows(i).Item("daihyousya_mei").ToString.Trim)))
            paramList.Add(MakeParam("@bikou1", SqlDbType.VarChar, 30, InsObj(dtOk.Rows(i).Item("bikou1").ToString.Trim)))
            paramList.Add(MakeParam("@bikou2", SqlDbType.VarChar, 30, InsObj(dtOk.Rows(i).Item("bikou2").ToString.Trim)))
            paramList.Add(MakeParam("@mail_address", SqlDbType.VarChar, 64, InsObj(dtOk.Rows(i).Item("mail_address").ToString.Trim)))
            paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, InsObj(dtOk.Rows(i).Item("upd_login_user_id").ToString.Trim)))

            strKameiten = dtOk.Rows(i).Item("kameiten_cd").ToString.Trim
            strUserId = dtOk.Rows(i).Item("upd_login_user_id").ToString.Trim

            Try
                If SelKameitenJyuusyo(dtOk.Rows(i).Item("kameiten_cd").ToString.Trim) Then
                    '更新
                    InsUpdCount = ExecuteNonQuery(connStr, CommandType.Text, strSqlUpd.ToString(), paramList.ToArray)

                    If Not InsUpdKameitenJyuusyoRenkei(strKameiten, strUserId) Then
                        Throw New ApplicationException
                    End If
                Else
                    'INS
                    InsUpdCount = ExecuteNonQuery(connStr, CommandType.Text, strSqlIns.ToString(), paramList.ToArray)

                    If Not InsUpdKameitenJyuusyoRenkei(strKameiten, strUserId) Then
                        Throw New ApplicationException
                    End If
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

    ''' <summary>加盟店住所マスタタにデータを更新</summary>
    ''' <history>2012/02/13　陳琳(大連情報システム部)　新規作成</history>
    Public Function InsKameitenJyuusyo(ByVal dtOk As Data.DataTable) As Boolean
        '戻り値
        Dim InsUpdCount As Integer = 0

        '更新用sql文
        Dim strSqlUpd As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With strSqlUpd
            .AppendLine("INSERT INTO ")
            .AppendLine("	m_kameiten_jyuusyo WITH(UPDLOCK) ")
            .AppendLine("	( ")
            .AppendLine("kameiten_cd ")
            .AppendLine(",jyuusyo_no ")
            .AppendLine(",todouhuken_cd ")
            .AppendLine(",jyuusyo1 ")
            .AppendLine(", jyuusyo2 ")
            .AppendLine(", yuubin_no ")
            .AppendLine(", tel_no ")
            .AppendLine(", fax_no ")
            .AppendLine(", busyo_mei ")
            .AppendLine(", daihyousya_mei ")
            .AppendLine(", bikou1 ")
            .AppendLine(", bikou2 ")
            .AppendLine(", mail_address ")
            .AppendLine(", add_login_user_id ")
            .AppendLine(", add_datetime ")
            .AppendLine("	) ")
            .AppendLine("VALUES ")
            .AppendLine("	( ")
            .AppendLine(" @kameiten_cd ")
            .AppendLine(",1 ")
            .AppendLine(", @todouhuken_cd ")
            .AppendLine(", @jyuusyo1 ")
            .AppendLine(", @jyuusyo2 ")
            .AppendLine(", @yuubin_no ")
            .AppendLine(", @tel_no ")
            .AppendLine(", @fax_no ")
            .AppendLine(", @busyo_mei ")
            .AppendLine(", @daihyousya_mei ")
            .AppendLine(", @bikou1 ")
            .AppendLine(", @bikou2 ")
            .AppendLine(", @mail_address ")

            .AppendLine("	, @upd_login_user_id")
            .AppendLine("	 , GETDATE()")
            .AppendLine("	) ")

        End With

        Dim strKameiten As String
        Dim strUserId As String

        For i As Integer = 0 To dtOk.Rows.Count - 1
            'パラメータの設定
            paramList.Clear()
            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, InsObj(dtOk.Rows(i).Item("kameiten_cd").ToString.Trim)))
            paramList.Add(MakeParam("@todouhuken_cd", SqlDbType.VarChar, 2, InsObj(dtOk.Rows(i).Item("todouhuken_cd").ToString.Trim)))
            paramList.Add(MakeParam("@jyuusyo1", SqlDbType.VarChar, 40, InsObj(dtOk.Rows(i).Item("jyuusyo1").ToString.Trim)))
            paramList.Add(MakeParam("@jyuusyo2", SqlDbType.VarChar, 30, InsObj(dtOk.Rows(i).Item("jyuusyo2").ToString.Trim)))
            paramList.Add(MakeParam("@yuubin_no", SqlDbType.VarChar, 8, InsObj(dtOk.Rows(i).Item("yuubin_no").ToString.Trim)))
            paramList.Add(MakeParam("@tel_no", SqlDbType.VarChar, 16, InsObj(dtOk.Rows(i).Item("tel_no").ToString.Trim)))
            paramList.Add(MakeParam("@fax_no", SqlDbType.VarChar, 16, InsObj(dtOk.Rows(i).Item("fax_no").ToString.Trim)))
            paramList.Add(MakeParam("@busyo_mei", SqlDbType.VarChar, 50, InsObj(dtOk.Rows(i).Item("busyo_mei").ToString.Trim)))
            paramList.Add(MakeParam("@daihyousya_mei", SqlDbType.VarChar, 20, InsObj(dtOk.Rows(i).Item("daihyousya_mei").ToString.Trim)))
            paramList.Add(MakeParam("@bikou1", SqlDbType.VarChar, 30, InsObj(dtOk.Rows(i).Item("bikou1").ToString.Trim)))
            paramList.Add(MakeParam("@bikou2", SqlDbType.VarChar, 30, InsObj(dtOk.Rows(i).Item("bikou2").ToString.Trim)))
            paramList.Add(MakeParam("@mail_address", SqlDbType.VarChar, 64, InsObj(dtOk.Rows(i).Item("mail_address").ToString.Trim)))
            paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, InsObj(dtOk.Rows(i).Item("upd_login_user_id").ToString.Trim)))
            Try

                strKameiten = dtOk.Rows(i).Item("kameiten_cd").ToString.Trim
                strUserId = dtOk.Rows(i).Item("upd_login_user_id").ToString.Trim

                '更新
                InsUpdCount = ExecuteNonQuery(connStr, CommandType.Text, strSqlUpd.ToString(), paramList.ToArray)

                If Not (InsUpdCount > 0) Then
                    Return False
                End If

                If Not InsUpdKameitenJyuusyoRenkei(strKameiten, strUserId) Then
                    Throw New ApplicationException
                End If

            Catch ex As Exception
                Return False
            End Try

        Next

        Return True
    End Function

    '=================2012/03/14 車龍 エラーの対応 追加↓===================================
    ''' <summary>多棟割引設定マスタにデータを存在チェックする</summary>
    ''' <history>2012/03/14 車龍(大連情報システム部)　新規作成</history>
    Public Function CheckTatouwaribikiSettei(ByVal strKameitenCd As String, ByVal strToukubun As String) As Boolean

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        '追加用SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	COUNT(kameiten_cd) AS cnt ")
            .AppendLine("FROM ")
            .AppendLine("	m_tatouwaribiki_settei WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	kameiten_cd = @kameiten_cd ")
            .AppendLine("	AND ")
            .AppendLine("	toukubun = @toukubun ")
        End With

        'パラメータの設定
        paramList.Clear()
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))
        paramList.Add(MakeParam("@toukubun", SqlDbType.Int, 1, strToukubun))
        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtTatouwaribikiSettei", paramList.ToArray())

        '戻り値
        If CInt(dsReturn.Tables(0).Rows(0).Item("cnt")) > 0 Then
            Return True
        Else
            Return False
        End If

    End Function
    '=================2012/03/14 車龍 エラーの対応 追加↑===================================

    ''' <summary>多棟割引設定マスタにデータを追加と更新</summary>
    ''' <history>2012/02/13　陳琳(大連情報システム部)　新規作成</history>
    Public Function InsUpdTatouwaribikiSettei(ByVal dtOk As Data.DataTable) As Boolean
        '戻り値
        Dim InsUpdCount As Integer = 0
        '追加用sql文
        Dim strSqlIns As New System.Text.StringBuilder
        '更新用sql文
        Dim strSqlUpd As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        '追加用SQL文
        With strSqlIns
            .AppendLine("INSERT INTO ")
            .AppendLine("	m_tatouwaribiki_settei WITH(UPDLOCK) ")
            .AppendLine("	( ")
            .AppendLine("		kameiten_cd ")
            .AppendLine("		,toukubun ")
            .AppendLine("		,syouhin_cd ")
            .AppendLine("		,add_login_user_id ")
            .AppendLine("		,add_datetime ")
            .AppendLine("		,upd_login_user_id ")
            .AppendLine("		,upd_datetime ")
            .AppendLine("	) ")
            .AppendLine("VALUES ")
            .AppendLine("	( ")
            .AppendLine("		@kameiten_cd ")
            .AppendLine("		,@toukubun ")
            .AppendLine("		,@syouhin_cd ")
            .AppendLine("		,@add_login_user_id ")
            .AppendLine("		,GETDATE() ")
            .AppendLine("		,NULL ")
            .AppendLine("		,NULL ")
            .AppendLine("	) ")
        End With

        With strSqlUpd
            .AppendLine("UPDATE ")
            .AppendLine("	m_tatouwaribiki_settei WITH(UPDLOCK) ")
            .AppendLine("SET ")
            .AppendLine("	syouhin_cd = @syouhin_cd ")
            .AppendLine("	,upd_login_user_id = @upd_login_user_id ")
            .AppendLine("	,upd_datetime = GETDATE() ")
            .AppendLine("WHERE ")
            .AppendLine("	kameiten_cd = @kameiten_cd ")
            .AppendLine("   AND ")
            .AppendLine("	toukubun = @toukubun ")
        End With

        Dim kameitenCd As String '加盟店コード
        Dim syouhinCd1 As String '棟区分1 商品コード
        Dim syouhinCd2 As String '棟区分2 商品コード
        Dim syouhinCd3 As String '棟区分3 商品コード

        Dim strUserId As String  'ユーザーID

        For i As Integer = 0 To dtOk.Rows.Count - 1

            Try
                kameitenCd = dtOk.Rows(i).Item("kameiten_cd").ToString.Trim
                syouhinCd1 = dtOk.Rows(i).Item("syouhin_cd1").ToString.Trim
                syouhinCd2 = dtOk.Rows(i).Item("syouhin_cd2").ToString.Trim
                syouhinCd3 = dtOk.Rows(i).Item("syouhin_cd3").ToString.Trim
                strUserId = dtOk.Rows(i).Item("add_login_user_id").ToString.Trim

                '棟区分1
                If Not String.IsNullOrEmpty(syouhinCd1) Then
                    'パラメータの設定
                    paramList.Clear()
                    paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, kameitenCd))
                    paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, InsObj(dtOk.Rows(i).Item("add_login_user_id").ToString.Trim)))
                    paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, InsObj(dtOk.Rows(i).Item("upd_login_user_id").ToString.Trim)))
                    paramList.Add(MakeParam("@toukubun", SqlDbType.Int, 1, 1))
                    paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, syouhinCd1))

                    If CheckTatouwaribikiSettei(kameitenCd, "1") Then
                        '存在する場合、更新
                        InsUpdCount = ExecuteNonQuery(connStr, CommandType.Text, strSqlUpd.ToString(), paramList.ToArray)
                        '多棟割引設定マスタ連携
                        If Not Me.InsUpdTatouwaribikiSetteiRenkei(kameitenCd, "1", strUserId) Then
                            Throw New ApplicationException
                        End If
                    Else
                        '存在しない場合、登録
                        InsUpdCount = ExecuteNonQuery(connStr, CommandType.Text, strSqlIns.ToString(), paramList.ToArray)
                        '多棟割引設定マスタ連携
                        If Not Me.InsUpdTatouwaribikiSetteiRenkei(kameitenCd, "1", strUserId) Then
                            Throw New ApplicationException
                        End If
                    End If
                End If

                '棟区分2
                If Not String.IsNullOrEmpty(syouhinCd2) Then
                    'パラメータの設定
                    paramList.Clear()
                    paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, kameitenCd))
                    paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, InsObj(dtOk.Rows(i).Item("add_login_user_id").ToString.Trim)))
                    paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, InsObj(dtOk.Rows(i).Item("upd_login_user_id").ToString.Trim)))
                    paramList.Add(MakeParam("@toukubun", SqlDbType.Int, 1, 2))
                    paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, syouhinCd2))

                    If CheckTatouwaribikiSettei(kameitenCd, "2") Then
                        '存在する場合、更新
                        InsUpdCount = ExecuteNonQuery(connStr, CommandType.Text, strSqlUpd.ToString(), paramList.ToArray)
                        '多棟割引設定マスタ連携
                        If Not Me.InsUpdTatouwaribikiSetteiRenkei(kameitenCd, "2", strUserId) Then
                            Throw New ApplicationException
                        End If
                    Else
                        '存在しない場合、登録
                        InsUpdCount = ExecuteNonQuery(connStr, CommandType.Text, strSqlIns.ToString(), paramList.ToArray)
                        '多棟割引設定マスタ連携
                        If Not Me.InsUpdTatouwaribikiSetteiRenkei(kameitenCd, "2", strUserId) Then
                            Throw New ApplicationException
                        End If
                    End If
                End If

                '棟区分3
                If Not String.IsNullOrEmpty(syouhinCd3) Then
                    'パラメータの設定
                    paramList.Clear()
                    paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, kameitenCd))
                    paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, InsObj(dtOk.Rows(i).Item("add_login_user_id").ToString.Trim)))
                    paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, InsObj(dtOk.Rows(i).Item("upd_login_user_id").ToString.Trim)))
                    paramList.Add(MakeParam("@toukubun", SqlDbType.Int, 1, 3))
                    paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, syouhinCd3))

                    If CheckTatouwaribikiSettei(kameitenCd, "3") Then
                        '存在する場合、更新
                        InsUpdCount = ExecuteNonQuery(connStr, CommandType.Text, strSqlUpd.ToString(), paramList.ToArray)
                        '多棟割引設定マスタ連携
                        If Not Me.InsUpdTatouwaribikiSetteiRenkei(kameitenCd, "3", strUserId) Then
                            Throw New ApplicationException
                        End If
                    Else
                        '存在しない場合、登録
                        InsUpdCount = ExecuteNonQuery(connStr, CommandType.Text, strSqlIns.ToString(), paramList.ToArray)
                        '多棟割引設定マスタ連携
                        If Not Me.InsUpdTatouwaribikiSetteiRenkei(kameitenCd, "3", strUserId) Then
                            Throw New ApplicationException
                        End If
                    End If
                End If

            Catch ex As Exception
                Return False
            End Try

        Next

        Return True
    End Function

    '=================2012/05/22 車龍 407553の対応 追加↓===================================

    ''' <summary>加盟店マスタ連携管理テーブルにデータを存在チェックする</summary>
    ''' <history>2012/05/22 車龍(大連情報システム部)　新規作成</history>
    Public Function CheckKameitenRenkei(ByVal strKameitenCd As String) As Boolean

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        '追加用SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	COUNT(kameiten_cd) AS cnt ")
            .AppendLine("FROM ")
            .AppendLine("	m_kameiten_renkei WITH(READCOMMITTED) ") '--加盟店マスタ連携管理テーブル
            .AppendLine("WHERE ")
            .AppendLine("	kameiten_cd = @kameiten_cd ") '--加盟店コード
        End With

        'パラメータの設定
        paramList.Clear()
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd)) '--加盟店コード
        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtKameitenRenkei", paramList.ToArray())

        '戻り値
        If CInt(dsReturn.Tables(0).Rows(0).Item("cnt")) > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>加盟店マスタ連携管理テーブルにデータを追加と更新</summary>
    ''' <history>2012/05/22 車龍(大連情報システム部)　新規作成</history>
    Public Function InsUpdKameitenRenkei(ByVal strKameitenCd As String, ByVal strUserId As String) As Boolean
        '戻り値
        Dim InsUpdCount As Integer = 0
        '追加用sql文
        Dim strSqlIns As New System.Text.StringBuilder
        '更新用sql文
        Dim strSqlUpd As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        '追加用SQL文
        With strSqlIns
            .AppendLine("INSERT INTO ")
            .AppendLine("	m_kameiten_renkei WITH(UPDLOCK) ")
            .AppendLine("	( ")
            .AppendLine("		kameiten_cd ")
            .AppendLine("		,renkei_siji_cd ")
            .AppendLine("		,sousin_jyky_cd ")
            .AppendLine("		,sousin_kanry_datetime ")
            .AppendLine("		,upd_login_user_id ")
            .AppendLine("		,upd_datetime ")
            .AppendLine("	) ")
            .AppendLine("VALUES ")
            .AppendLine("( ")
            .AppendLine("	@kameiten_cd ")
            .AppendLine("	,@renkei_siji_cd ")
            .AppendLine("	,@sousin_jyky_cd ")
            .AppendLine("	,NULL ")
            .AppendLine("	,@upd_login_user_id ")
            .AppendLine("	,GETDATE() ")
            .AppendLine(") ")
        End With

        With strSqlUpd
            .AppendLine("UPDATE ")
            .AppendLine("	m_kameiten_renkei WITH(UPDLOCK) ")
            .AppendLine("SET ")
            .AppendLine("	renkei_siji_cd = @renkei_siji_cd ")
            .AppendLine("	,sousin_jyky_cd = @sousin_jyky_cd ")
            .AppendLine("	,upd_login_user_id = @upd_login_user_id ")
            .AppendLine("	,upd_datetime = GETDATE() ")
            .AppendLine("WHERE ")
            .AppendLine("	kameiten_cd = @kameiten_cd ")
        End With

        'パラメータの設定
        paramList.Clear()
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))
        paramList.Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 10, "0")) '--送信状況コード
        paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, InsObj(strUserId)))

        Try
            If CheckKameitenRenkei(strKameitenCd) Then
                '存在する場合、更新
                paramList.Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 10, "2")) '--連携指示コード(変更)
                InsUpdCount = ExecuteNonQuery(connStr, CommandType.Text, strSqlUpd.ToString(), paramList.ToArray)
            Else
                '存在しない場合、登録
                paramList.Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 10, "1")) '--連携指示コード(新規)
                InsUpdCount = ExecuteNonQuery(connStr, CommandType.Text, strSqlIns.ToString(), paramList.ToArray)
            End If

        Catch ex As Exception
            Return False
        End Try

        Return True

    End Function

    ''' <summary>加盟店住所マスタ連携管理テーブルにデータを存在チェックする</summary>
    ''' <history>2012/05/22 車龍(大連情報システム部)　新規作成</history>
    Public Function CheckKameitenJyuusyoRenkei(ByVal strKameitenCd As String) As Boolean

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        '追加用SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	COUNT(kameiten_cd) AS cnt ")
            .AppendLine("FROM ")
            .AppendLine("	m_kameiten_jyuusyo_renkei WITH(READCOMMITTED) ") '--加盟店マスタ連携管理テーブル
            .AppendLine("WHERE ")
            .AppendLine("	kameiten_cd = @kameiten_cd ") '--加盟店コード
            .AppendLine("   AND ")
            .AppendLine("   jyuusyo_no = @jyuusyo_no ") '--住所NO
        End With

        'パラメータの設定
        paramList.Clear()
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd)) '--加盟店コード
        paramList.Add(MakeParam("@jyuusyo_no", SqlDbType.Int, 10, "1")) '--住所NO
        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtKameitenRenkei", paramList.ToArray())

        '戻り値
        If CInt(dsReturn.Tables(0).Rows(0).Item("cnt")) > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>加盟店住所マスタ連携管理テーブルにデータを追加と更新</summary>
    ''' <history>2012/05/22 車龍(大連情報システム部)　新規作成</history>
    Public Function InsUpdKameitenJyuusyoRenkei(ByVal strKameitenCd As String, ByVal strUserId As String) As Boolean
        '戻り値
        Dim InsUpdCount As Integer = 0
        '追加用sql文
        Dim strSqlIns As New System.Text.StringBuilder
        '更新用sql文
        Dim strSqlUpd As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        '追加用SQL文
        With strSqlIns
            .AppendLine("INSERT INTO ")
            .AppendLine("	m_kameiten_jyuusyo_renkei WITH(UPDLOCK) ")
            .AppendLine("	( ")
            .AppendLine("		kameiten_cd ")
            .AppendLine("		,jyuusyo_no ")
            .AppendLine("		,renkei_siji_cd ")
            .AppendLine("		,sousin_jyky_cd ")
            .AppendLine("		,sousin_kanry_datetime ")
            .AppendLine("		,upd_login_user_id ")
            .AppendLine("		,upd_datetime ")
            .AppendLine("	) ")
            .AppendLine("VALUES ")
            .AppendLine("( ")
            .AppendLine("	@kameiten_cd ")
            .AppendLine("	,@jyuusyo_no ")
            .AppendLine("	,@renkei_siji_cd ")
            .AppendLine("	,@sousin_jyky_cd ")
            .AppendLine("	,NULL ")
            .AppendLine("	,@upd_login_user_id ")
            .AppendLine("	,GETDATE() ")
            .AppendLine(") ")
        End With

        With strSqlUpd
            .AppendLine("UPDATE ")
            .AppendLine("	m_kameiten_jyuusyo_renkei WITH(UPDLOCK) ")
            .AppendLine("SET ")
            .AppendLine("	renkei_siji_cd = @renkei_siji_cd ")
            .AppendLine("	,sousin_jyky_cd = @sousin_jyky_cd ")
            .AppendLine("	,upd_login_user_id = @upd_login_user_id ")
            .AppendLine("	,upd_datetime = GETDATE() ")
            .AppendLine("WHERE ")
            .AppendLine("	kameiten_cd = @kameiten_cd ")
            .AppendLine("   AND ")
            .AppendLine("   jyuusyo_no = @jyuusyo_no ") '--住所NO
        End With

        'パラメータの設定
        paramList.Clear()
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))
        paramList.Add(MakeParam("@jyuusyo_no", SqlDbType.Int, 10, "1")) '--住所NO
        paramList.Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 10, "0")) '--送信状況コード
        paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, InsObj(strUserId)))

        Try
            If CheckKameitenJyuusyoRenkei(strKameitenCd) Then
                '存在する場合、更新
                paramList.Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 10, "2")) '--連携指示コード(変更)
                InsUpdCount = ExecuteNonQuery(connStr, CommandType.Text, strSqlUpd.ToString(), paramList.ToArray)
            Else
                '存在しない場合、登録
                paramList.Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 10, "1")) '--連携指示コード(新規)
                InsUpdCount = ExecuteNonQuery(connStr, CommandType.Text, strSqlIns.ToString(), paramList.ToArray)
            End If

        Catch ex As Exception
            Return False
        End Try

        Return True

    End Function

    ''' <summary>多棟割引設定マスタ連携管理テーブルにデータを存在チェックする</summary>
    ''' <history>2012/05/22 車龍(大連情報システム部)　新規作成</history>
    Public Function CheckTatouwaribikiSetteiRenkei(ByVal strKameitenCd As String, ByVal strToukubun As String) As Boolean

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        '追加用SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	COUNT(kameiten_cd) AS cnt ")
            .AppendLine("FROM ")
            .AppendLine("	m_tatouwaribiki_settei_renkei WITH(READCOMMITTED) ") '--多棟割引設定マスタ連携管理テーブル
            .AppendLine("WHERE ")
            .AppendLine("	kameiten_cd = @kameiten_cd ") '--加盟店コード
            .AppendLine("	AND ")
            .AppendLine("	toukubun = @toukubun ") '--棟区分
        End With

        'パラメータの設定
        paramList.Clear()
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd)) '--加盟店コード
        paramList.Add(MakeParam("@toukubun", SqlDbType.Int, 1, strToukubun)) '--棟区分
        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtTatouwaribikiSetteiRenkei", paramList.ToArray())

        '戻り値
        If CInt(dsReturn.Tables(0).Rows(0).Item("cnt")) > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>多棟割引設定マスタ連携管理テーブルにデータを追加と更新</summary>
    ''' <history>2012/05/22 車龍(大連情報システム部)　新規作成</history>
    Public Function InsUpdTatouwaribikiSetteiRenkei(ByVal strKameitenCd As String, ByVal strToukubun As String, ByVal strUserId As String) As Boolean
        '戻り値
        Dim InsUpdCount As Integer = 0
        '追加用sql文
        Dim strSqlIns As New System.Text.StringBuilder
        '更新用sql文
        Dim strSqlUpd As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        '追加用SQL文
        With strSqlIns
            .AppendLine("INSERT INTO ")
            .AppendLine("	m_tatouwaribiki_settei_renkei WITH(UPDLOCK) ")
            .AppendLine("	( ")
            .AppendLine("		kameiten_cd ")
            .AppendLine("		,toukubun ")
            .AppendLine("		,renkei_siji_cd ")
            .AppendLine("		,sousin_jyky_cd ")
            .AppendLine("		,sousin_kanry_datetime ")
            .AppendLine("		,upd_login_user_id ")
            .AppendLine("		,upd_datetime ")
            .AppendLine("	) ")
            .AppendLine("VALUES ")
            .AppendLine("( ")
            .AppendLine("	@kameiten_cd ")
            .AppendLine("	,@toukubun ")
            .AppendLine("	,@renkei_siji_cd ")
            .AppendLine("	,@sousin_jyky_cd ")
            .AppendLine("	,NULL ")
            .AppendLine("	,@upd_login_user_id ")
            .AppendLine("	,GETDATE() ")
            .AppendLine(") ")
        End With

        With strSqlUpd
            .AppendLine("UPDATE ")
            .AppendLine("	m_tatouwaribiki_settei_renkei WITH(UPDLOCK) ")
            .AppendLine("SET ")
            .AppendLine("	renkei_siji_cd = @renkei_siji_cd ")
            .AppendLine("	,sousin_jyky_cd = @sousin_jyky_cd ")
            .AppendLine("	,upd_login_user_id = @upd_login_user_id ")
            .AppendLine("	,upd_datetime = GETDATE() ")
            .AppendLine("WHERE ")
            .AppendLine("	kameiten_cd = @kameiten_cd ")
            .AppendLine("   AND ")
            .AppendLine("	toukubun = @toukubun ")
        End With

        'パラメータの設定
        paramList.Clear()
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))
        paramList.Add(MakeParam("@toukubun", SqlDbType.Int, 10, strToukubun))
        paramList.Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 10, "0")) '--送信状況コード
        paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, InsObj(strUserId)))

        Try
            If CheckTatouwaribikiSetteiRenkei(strKameitenCd, strToukubun) Then
                '存在する場合、更新
                paramList.Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 10, "2")) '--連携指示コード(変更)
                InsUpdCount = ExecuteNonQuery(connStr, CommandType.Text, strSqlUpd.ToString(), paramList.ToArray)
            Else
                '存在しない場合、登録
                paramList.Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 10, "1")) '--連携指示コード(新規)
                InsUpdCount = ExecuteNonQuery(connStr, CommandType.Text, strSqlIns.ToString(), paramList.ToArray)
            End If

        Catch ex As Exception
            Return False
        End Try

        Return True

    End Function

    ''' <summary>加盟店備考設定マスタ連携管理テーブルにデータを追加と更新</summary>
    ''' <history>2012/05/22 車龍(大連情報システム部)　新規作成</history>
    Public Function InsKameitenBikouSetteiRenkei(ByVal strKameitenCd As String, ByVal strNyuuryokuNo As String, ByVal strUserId As String) As Boolean
        '戻り値
        Dim InsUpdCount As Integer = 0
        '追加用sql文
        Dim strSqlIns As New System.Text.StringBuilder
        '更新用sql文
        Dim strSqlUpd As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        '追加用SQL文
        With strSqlIns
            .AppendLine("INSERT INTO ")
            .AppendLine("	m_kameiten_bikou_settei_renkei WITH(UPDLOCK) ")
            .AppendLine("	( ")
            .AppendLine("		kameiten_cd ")
            .AppendLine("		,nyuuryoku_no ")
            .AppendLine("		,renkei_siji_cd ")
            .AppendLine("		,sousin_jyky_cd ")
            .AppendLine("		,sousin_kanry_datetime ")
            .AppendLine("		,upd_login_user_id ")
            .AppendLine("		,upd_datetime ")
            .AppendLine("	) ")
            .AppendLine("VALUES ")
            .AppendLine("( ")
            .AppendLine("	@kameiten_cd ")
            .AppendLine("	,@nyuuryoku_no ")
            .AppendLine("	,@renkei_siji_cd ")
            .AppendLine("	,@sousin_jyky_cd ")
            .AppendLine("	,NULL ")
            .AppendLine("	,@upd_login_user_id ")
            .AppendLine("	,GETDATE() ")
            .AppendLine(") ")
        End With

        'パラメータの設定
        paramList.Clear()
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))
        paramList.Add(MakeParam("@nyuuryoku_no", SqlDbType.Int, 10, strNyuuryokuNo))
        paramList.Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 10, "1")) '--連携指示コード(新規)
        paramList.Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 10, "0")) '--送信状況コード
        paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, InsObj(strUserId)))

        Try
            '登録
            InsUpdCount = ExecuteNonQuery(connStr, CommandType.Text, strSqlIns.ToString(), paramList.ToArray)

        Catch ex As Exception
            Return False
        End Try

        Return True

    End Function
    '=================2012/05/22 車龍 407553の対応 追加↑===================================

    ''' <summary>加盟店備考設定マスタにデータを追加と更新</summary>
    ''' <history>2012/02/13　陳琳(大連情報システム部)　新規作成</history>
    Public Function InsKameitenBikouSettei(ByVal dtOk As Data.DataTable) As Boolean
        '戻り値
        Dim InsUpdCount As Integer = 0
        '追加用sql文
        Dim strSqlIns As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        '追加用SQL文
        With strSqlIns
            .AppendLine("INSERT INTO ")
            .AppendLine("	m_kameiten_bikou_settei WITH(UPDLOCK) ")
            .AppendLine("	( ")
            .AppendLine("		kameiten_cd ")
            .AppendLine("		,bikou_syubetu ")
            .AppendLine("		,nyuuryoku_no ")
            .AppendLine("		,naiyou ")
            .AppendLine("		,kousinsya ")
            .AppendLine("		,upd_datetime ")
            .AppendLine("		,add_login_user_id ")
            .AppendLine("		,add_datetime ")
            .AppendLine("		,upd_login_user_id ")
            .AppendLine("	) ")
            .AppendLine("VALUES ")
            .AppendLine("	( ")
            .AppendLine("		@kameiten_cd ")
            .AppendLine("		,@bikou_syubetu ")
            .AppendLine("		,@nyuuryoku_no ")
            .AppendLine("		,@naiyou ")
            .AppendLine("		,NULL ")
            .AppendLine("		,NULL")
            .AppendLine("		,@add_login_user_id ")
            .AppendLine("		,GETDATE() ")
            .AppendLine("		,NULL ")
            .AppendLine("	) ")
        End With

        Dim strKameiten As String
        Dim strUserId As String
        Dim strMaxNyuuryokuNo As String

        For i As Integer = 0 To dtOk.Rows.Count - 1

            Try
                Dim bikouSyubetu1 As String = dtOk.Rows(i).Item("bikou_syubetu1").ToString.Trim
                Dim bikouSyubetu2 As String = dtOk.Rows(i).Item("bikou_syubetu2").ToString.Trim
                Dim bikouSyubetu3 As String = dtOk.Rows(i).Item("bikou_syubetu3").ToString.Trim
                Dim bikouSyubetu4 As String = dtOk.Rows(i).Item("bikou_syubetu4").ToString.Trim
                Dim bikouSyubetu5 As String = dtOk.Rows(i).Item("bikou_syubetu5").ToString.Trim

                strKameiten = dtOk.Rows(i).Item("kameiten_cd").ToString.Trim
                strUserId = dtOk.Rows(i).Item("upd_login_user_id").ToString.Trim


                '追加
                If Not String.IsNullOrEmpty(bikouSyubetu1) Then
                    'パラメータの設定
                    paramList.Clear()
                    paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, InsObj(dtOk.Rows(i).Item("kameiten_cd").ToString.Trim)))
                    paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, InsObj(dtOk.Rows(i).Item("add_login_user_id").ToString.Trim)))
                    paramList.Add(MakeParam("@bikou_syubetu", SqlDbType.Int, 10, bikouSyubetu1))

                    '入力No（※加盟店で登録のある入力Noを参照し MAX値+1）
                    strMaxNyuuryokuNo = GetMaxNyuuryokuNo(dtOk.Rows(i).Item("kameiten_cd").ToString.Trim)
                    paramList.Add(MakeParam("@nyuuryoku_no", SqlDbType.Int, 10, strMaxNyuuryokuNo))

                    paramList.Add(MakeParam("@naiyou", SqlDbType.VarChar, 80, InsObj(dtOk.Rows(i).Item("naiyou1").ToString.Trim)))
                    InsUpdCount = ExecuteNonQuery(connStr, CommandType.Text, strSqlIns.ToString(), paramList.ToArray)

                    If Not Me.InsKameitenBikouSetteiRenkei(strKameiten, strMaxNyuuryokuNo, strUserId) Then
                        Throw New ApplicationException
                    End If
                End If

                If Not String.IsNullOrEmpty(bikouSyubetu2) Then
                    paramList.Clear()
                    paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, InsObj(dtOk.Rows(i).Item("kameiten_cd").ToString.Trim)))
                    paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, InsObj(dtOk.Rows(i).Item("add_login_user_id").ToString.Trim)))
                    paramList.Add(MakeParam("@bikou_syubetu", SqlDbType.Int, 10, bikouSyubetu2))

                    '入力No（※加盟店で登録のある入力Noを参照し MAX値+1）
                    strMaxNyuuryokuNo = GetMaxNyuuryokuNo(dtOk.Rows(i).Item("kameiten_cd").ToString.Trim)
                    paramList.Add(MakeParam("@nyuuryoku_no", SqlDbType.Int, 10, strMaxNyuuryokuNo))

                    paramList.Add(MakeParam("@naiyou", SqlDbType.VarChar, 80, InsObj(dtOk.Rows(i).Item("naiyou2").ToString.Trim)))
                    InsUpdCount = ExecuteNonQuery(connStr, CommandType.Text, strSqlIns.ToString(), paramList.ToArray)

                    If Not Me.InsKameitenBikouSetteiRenkei(strKameiten, strMaxNyuuryokuNo, strUserId) Then
                        Throw New ApplicationException
                    End If
                End If

                If Not String.IsNullOrEmpty(bikouSyubetu3) Then
                    paramList.Clear()
                    paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, InsObj(dtOk.Rows(i).Item("kameiten_cd").ToString.Trim)))
                    paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, InsObj(dtOk.Rows(i).Item("add_login_user_id").ToString.Trim)))
                    paramList.Add(MakeParam("@bikou_syubetu", SqlDbType.Int, 10, bikouSyubetu3))

                    '入力No（※加盟店で登録のある入力Noを参照し MAX値+1）
                    strMaxNyuuryokuNo = GetMaxNyuuryokuNo(dtOk.Rows(i).Item("kameiten_cd").ToString.Trim)
                    paramList.Add(MakeParam("@nyuuryoku_no", SqlDbType.Int, 10, strMaxNyuuryokuNo))

                    paramList.Add(MakeParam("@naiyou", SqlDbType.VarChar, 80, InsObj(dtOk.Rows(i).Item("naiyou3").ToString.Trim)))
                    InsUpdCount = ExecuteNonQuery(connStr, CommandType.Text, strSqlIns.ToString(), paramList.ToArray)

                    If Not Me.InsKameitenBikouSetteiRenkei(strKameiten, strMaxNyuuryokuNo, strUserId) Then
                        Throw New ApplicationException
                    End If
                End If

                If Not String.IsNullOrEmpty(bikouSyubetu4) Then
                    paramList.Clear()
                    paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, InsObj(dtOk.Rows(i).Item("kameiten_cd").ToString.Trim)))
                    paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, InsObj(dtOk.Rows(i).Item("add_login_user_id").ToString.Trim)))
                    paramList.Add(MakeParam("@bikou_syubetu", SqlDbType.Int, 10, bikouSyubetu4))

                    '入力No（※加盟店で登録のある入力Noを参照し MAX値+1）
                    strMaxNyuuryokuNo = GetMaxNyuuryokuNo(dtOk.Rows(i).Item("kameiten_cd").ToString.Trim)
                    paramList.Add(MakeParam("@nyuuryoku_no", SqlDbType.Int, 10, strMaxNyuuryokuNo))

                    paramList.Add(MakeParam("@naiyou", SqlDbType.VarChar, 80, InsObj(dtOk.Rows(i).Item("naiyou4").ToString.Trim)))
                    InsUpdCount = ExecuteNonQuery(connStr, CommandType.Text, strSqlIns.ToString(), paramList.ToArray)

                    If Not Me.InsKameitenBikouSetteiRenkei(strKameiten, strMaxNyuuryokuNo, strUserId) Then
                        Throw New ApplicationException
                    End If
                End If

                If Not String.IsNullOrEmpty(bikouSyubetu5) Then
                    paramList.Clear()
                    paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, InsObj(dtOk.Rows(i).Item("kameiten_cd").ToString.Trim)))
                    paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, InsObj(dtOk.Rows(i).Item("add_login_user_id").ToString.Trim)))
                    paramList.Add(MakeParam("@bikou_syubetu", SqlDbType.Int, 10, bikouSyubetu5))

                    '入力No（※加盟店で登録のある入力Noを参照し MAX値+1）
                    strMaxNyuuryokuNo = GetMaxNyuuryokuNo(dtOk.Rows(i).Item("kameiten_cd").ToString.Trim)
                    paramList.Add(MakeParam("@nyuuryoku_no", SqlDbType.Int, 10, strMaxNyuuryokuNo))

                    paramList.Add(MakeParam("@naiyou", SqlDbType.VarChar, 80, InsObj(dtOk.Rows(i).Item("naiyou5").ToString.Trim)))
                    InsUpdCount = ExecuteNonQuery(connStr, CommandType.Text, strSqlIns.ToString(), paramList.ToArray)

                    If Not Me.InsKameitenBikouSetteiRenkei(strKameiten, strMaxNyuuryokuNo, strUserId) Then
                        Throw New ApplicationException
                    End If
                End If

            Catch ex As Exception
                Return False
            End Try

        Next

        Return True
    End Function

    ''' <summary>
    ''' 入力No（※加盟店で登録のある入力Noを参照し MAX値+1）
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <returns>入力No</returns>
    ''' <remarks>2012/02/13　陳琳(大連情報システム部)　新規作成</remarks>
    Private Function GetMaxNyuuryokuNo(ByVal strKameitenCd As String) As Integer
        '戻り値
        Dim intRtn As Integer = 0
        Dim objMaxNo As Object
        'sql文
        Dim strSql As String = "SELECT MAX(nyuuryoku_no) FROM m_kameiten_bikou_settei  WITH(READCOMMITTED) WHERE kameiten_cd='" & strKameitenCd & "'"

        objMaxNo = ExecuteScalar(connStr, CommandType.Text, strSql)

        If objMaxNo Is DBNull.Value Then
            intRtn = 0
        Else
            intRtn = CInt(objMaxNo)
        End If

        Return intRtn + 1
    End Function

    ''' <summary>加盟店情報一括取込エラー情報を取得する</summary>
    ''' <param name="strEdidate">EDI情報作成日</param>
    ''' <param name="strSyoridate">処理日時</param>
    ''' <returns>加盟店情報一括取込エラーデータテーブル</returns>
    Public Function SelKameitenInfoIttukatuError(ByVal strEdidate As String, ByVal strSyoridate As String) As Data.DataTable
        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet
        'SQLコメント
        Dim commandTextSb As New StringBuilder
        ''パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        'SQL文
        With commandTextSb
            .AppendLine(" SELECT ")
            .AppendLine("   TOP 100 ")
            .AppendLine("		edi_jouhou_sakusei_date")
            .AppendLine("		,gyou_no")
            .AppendLine("		,syori_datetime")
            .AppendLine("		,kbn")
            .AppendLine("		,kameiten_cd")
            .AppendLine("		,torikesi")
            .AppendLine("		,hattyuu_teisi_flg")
            .AppendLine("		,kameiten_mei1")
            .AppendLine("		,tenmei_kana1")
            .AppendLine("		,kameiten_mei2")
            .AppendLine("		,tenmei_kana2")
            .AppendLine("		,builder_no")
            .AppendLine("		,builder_mei")
            .AppendLine("		,keiretu_cd")
            .AppendLine("		,keiretu_mei")
            .AppendLine("		,eigyousyo_cd")
            .AppendLine("		,eigyousyo_mei")
            .AppendLine("		,kameiten_seisiki_mei")
            .AppendLine("		,kameiten_seisiki_mei_kana")
            .AppendLine("		,todouhuken_cd")
            .AppendLine("		,todouhuken_mei")
            '================↓2013/03/06 車龍 407584 追加↓========================
            .AppendLine("		,nenkan_tousuu ") '--年間棟数
            '================↑2013/03/06 車龍 407584 追加↑========================
            .AppendLine("		,fuho_syoumeisyo_flg")
            .AppendLine("		,fuho_syoumeisyo_kaisi_nengetu")
            .AppendLine("		,eigyou_tantousya_mei")
            .AppendLine("		,eigyou_tantousya_simei")
            .AppendLine("		,kyuu_eigyou_tantousya_mei")
            .AppendLine("		,kyuu_eigyou_tantousya_simei")
            '================↓2013/03/06 車龍 407584 追加↓========================
            .AppendLine("		,koj_uri_syubetsu ") '--工事売上種別
            .AppendLine("		,koj_uri_syubetsu_mei ") '--工事売上種別名
            .AppendLine("		,jiosaki_flg ") '--JIO先フラグ
            '================↑2013/03/06 車龍 407584 追加↑========================
            .AppendLine("		,kaiyaku_haraimodosi_kkk")
            .AppendLine("		,tou1_syouhin_cd")
            .AppendLine("		,tou1_syouhin_mei")
            .AppendLine("		,tou2_syouhin_cd")
            .AppendLine("		,tou2_syouhin_mei")
            .AppendLine("		,tou3_syouhin_cd")
            .AppendLine("		,tou3_syouhin_mei")
            .AppendLine("		,tys_seikyuu_saki_kbn")
            .AppendLine("		,tys_seikyuu_saki_cd")
            .AppendLine("		,tys_seikyuu_saki_brc")
            .AppendLine("		,tys_seikyuu_saki_mei")
            .AppendLine("		,koj_seikyuu_saki_kbn")
            .AppendLine("		,koj_seikyuu_saki_cd")
            .AppendLine("		,koj_seikyuu_saki_brc")
            .AppendLine("		,koj_seikyuu_saki_mei")
            .AppendLine("		,hansokuhin_seikyuu_saki_kbn")
            .AppendLine("		,hansokuhin_seikyuu_saki_cd")
            .AppendLine("		,hansokuhin_seikyuu_saki_brc")
            .AppendLine("		,hansokuhin_seikyuu_saki_mei")
            .AppendLine("		,tatemono_seikyuu_saki_kbn")
            .AppendLine("		,tatemono_seikyuu_saki_cd")
            .AppendLine("		,tatemono_seikyuu_saki_brc")
            .AppendLine("		,tatemono_seikyuu_saki_mei")
            .AppendLine("		,seikyuu_saki_kbn5")
            .AppendLine("		,seikyuu_saki_cd5")
            .AppendLine("		,seikyuu_saki_brc5")
            .AppendLine("		,seikyuu_saki_mei5")
            .AppendLine("		,seikyuu_saki_kbn6")
            .AppendLine("		,seikyuu_saki_cd6")
            .AppendLine("		,seikyuu_saki_brc6")
            .AppendLine("		,seikyuu_saki_mei6")
            .AppendLine("		,seikyuu_saki_kbn7")
            .AppendLine("		,seikyuu_saki_cd7")
            .AppendLine("		,seikyuu_saki_brc7")
            .AppendLine("		,seikyuu_saki_mei7")
            '================↓2013/03/06 車龍 407584 追加↓========================
            .AppendLine("		,hosyou_kikan ") '--保証期間
            .AppendLine("		,hosyousyo_hak_umu ") '--保証書発行有無
            '================↑2013/03/06 車龍 407584 追加↑========================
            .AppendLine("		,nyuukin_kakunin_jyouken")
            .AppendLine("		,nyuukin_kakunin_oboegaki")
            .AppendLine("		,tys_mitsyo_flg")
            .AppendLine("		,hattyuusyo_flg")
            .AppendLine("		,yuubin_no")
            .AppendLine("		,jyuusyo1")
            .AppendLine("		,jyuusyo2")
            .AppendLine("		,syozaichi_cd")
            .AppendLine("		,syozaichi_mei")
            .AppendLine("		,busyo_mei")
            .AppendLine("		,daihyousya_mei")
            .AppendLine("		,tel_no")
            .AppendLine("		,fax_no")
            .AppendLine("		,mail_address")
            .AppendLine("		,bikou1")
            .AppendLine("		,bikou2")
            '================↓2013/03/06 車龍 407584 追加↓========================
            .AppendLine("		,add_date") '--登録日
            .AppendLine("		,seikyuu_umu") '--請求有無
            .AppendLine("		,syouhin_cd") '--商品コード
            .AppendLine("		,syouhin_mei") '--商品名
            .AppendLine("		,uri_gaku") '--売上金額
            .AppendLine("		,koumuten_seikyuu_gaku") '--工務店請求金額
            .AppendLine("		,seikyuusyo_hak_date") '--請求書発行日
            .AppendLine("		,uri_date") '--売上年月日
            .AppendLine("		,bikou") '--備考
            '================↑2013/03/06 車龍 407584 追加↑========================
            '==========2012/05/09 車龍 407553の対応 修正↓======================
            .AppendLine("		,kameiten_upd_datetime")
            .AppendLine("		,tatouwari_upd_datetime1")
            .AppendLine("		,tatouwari_upd_datetime2")
            .AppendLine("		,tatouwari_upd_datetime3")
            .AppendLine("		,kameiten_jyuusyo_upd_datetime")
            '==========2012/05/09 車龍 407553の対応 修正↑======================
            .AppendLine("		,bikou_syubetu1")
            .AppendLine("		,bikou_syubetu1_mei")
            .AppendLine("		,bikou_syubetu2")
            .AppendLine("		,bikou_syubetu2_mei")
            .AppendLine("		,bikou_syubetu3")
            .AppendLine("		,bikou_syubetu3_mei")
            .AppendLine("		,bikou_syubetu4")
            .AppendLine("		,bikou_syubetu4_mei")
            .AppendLine("		,bikou_syubetu5")
            .AppendLine("		,bikou_syubetu5_mei")
            .AppendLine("		,naiyou1")
            .AppendLine("		,naiyou2")
            .AppendLine("		,naiyou3")
            .AppendLine("		,naiyou4")
            .AppendLine("		,naiyou5")
            .AppendLine("		,add_login_user_id")
            .AppendLine("		,add_datetime")
            .AppendLine("		,upd_login_user_id")
            .AppendLine("		,upd_datetime")
            .AppendLine(" FROM  ")
            .AppendLine("    m_kameiten_info_ittukatu_error WITH(READCOMMITTED) ")  '加盟店情報一括取込エラー情報テーブル
            .AppendLine(" WHERE ")
            .AppendLine("    edi_jouhou_sakusei_date = @edi_jouhou_sakusei_date ")   'EDI情報作成日
            .AppendLine(" AND CONVERT(varchar(100),syori_datetime,112) + REPLACE(CONVERT(varchar(100),syori_datetime,114),':','') = @syori_datetime ")  '処理日時
            .AppendLine(" ORDER BY ")
            .AppendLine("    gyou_no ")
        End With
        'EDI情報作成日
        paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, strEdidate))
        '処理日時
        paramList.Add(MakeParam("@syori_datetime", SqlDbType.VarChar, 40, strSyoridate))
        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsReturn, "KameitenInfoIttukatuError", paramList.ToArray)

        Return dsReturn.Tables(0)

    End Function

    ''' <summary>加盟店情報一括取込エラー件数を取得する</summary>
    ''' <param name="strEdidate">EDI情報作成日</param>
    ''' <returns>加盟店情報一括取込エラー件数</returns>
    Public Function SelKameitenInfoIttukatuErrorCount(ByVal strEdidate As String, ByVal strSyoridate As String) As Integer
        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine(" SELECT ")
            .AppendLine("   COUNT(*) AS Maxcount ")
            .AppendLine(" FROM ")
            .AppendLine("   m_kameiten_info_ittukatu_error  WITH(READCOMMITTED) ")  '加盟店情報一括取込エラー情報テーブル
            .AppendLine(" WHERE ")
            .AppendLine("    edi_jouhou_sakusei_date = @edi_jouhou_sakusei_date ")  'EDI情報作成日
            .AppendLine(" AND CONVERT(varchar(100),syori_datetime,112) + REPLACE(CONVERT(varchar(100),syori_datetime,114),':','') = @syori_datetime ")  '処理日時
        End With

        'EDI情報作成日
        paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, strEdidate))
        '処理日時
        paramList.Add(MakeParam("@syori_datetime", SqlDbType.VarChar, 40, strSyoridate))
        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsReturn, "KameitenInfoIttukatuErrorCount", paramList.ToArray)

        Return dsReturn.Tables(0).Rows(0).Item("Maxcount")

    End Function

    ''' <summary>加盟店情報一括取込エラーCSVを取得</summary>
    ''' <returns>加盟店情報一括取込エラーCSVテーブル</returns>
    Public Function SelKameitenInfoIttukatuErrorCsv(ByVal strEdidate As String, ByVal strSyoridate As String) As Data.DataTable
        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet
        'SQLコメント
        Dim commandTextSb As New StringBuilder
        ''パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        'SQL文
        With commandTextSb
            .AppendLine(" SELECT ")
            .AppendLine("   TOP 5000")
            .AppendLine("		edi_jouhou_sakusei_date")
            .AppendLine("		,gyou_no")
            .AppendLine("		,syori_datetime")
            .AppendLine("		,kbn")
            .AppendLine("		,kameiten_cd")
            .AppendLine("		,torikesi")
            .AppendLine("		,hattyuu_teisi_flg")
            .AppendLine("		,kameiten_mei1")
            .AppendLine("		,tenmei_kana1")
            .AppendLine("		,kameiten_mei2")
            .AppendLine("		,tenmei_kana2")
            .AppendLine("		,builder_no")
            .AppendLine("		,builder_mei")
            .AppendLine("		,keiretu_cd")
            .AppendLine("		,keiretu_mei")
            .AppendLine("		,eigyousyo_cd")
            .AppendLine("		,eigyousyo_mei")
            .AppendLine("		,kameiten_seisiki_mei")
            .AppendLine("		,kameiten_seisiki_mei_kana")
            .AppendLine("		,todouhuken_cd")
            .AppendLine("		,todouhuken_mei")
            '================↓2013/03/06 車龍 407584 追加↓========================
            .AppendLine("		,nenkan_tousuu ") '--年間棟数
            '================↑2013/03/06 車龍 407584 追加↑========================
            .AppendLine("		,fuho_syoumeisyo_flg")
            .AppendLine("		,fuho_syoumeisyo_kaisi_nengetu")
            .AppendLine("		,eigyou_tantousya_mei")
            .AppendLine("		,eigyou_tantousya_simei")
            .AppendLine("		,kyuu_eigyou_tantousya_mei")
            .AppendLine("		,kyuu_eigyou_tantousya_simei")
            '================↓2013/03/06 車龍 407584 追加↓========================
            .AppendLine("		,koj_uri_syubetsu ") '--工事売上種別
            .AppendLine("		,koj_uri_syubetsu_mei ") '--工事売上種別名
            .AppendLine("		,jiosaki_flg ") '--JIO先フラグ
            '================↑2013/03/06 車龍 407584 追加↑========================
            .AppendLine("		,kaiyaku_haraimodosi_kkk")
            .AppendLine("		,tou1_syouhin_cd")
            .AppendLine("		,tou1_syouhin_mei")
            .AppendLine("		,tou2_syouhin_cd")
            .AppendLine("		,tou2_syouhin_mei")
            .AppendLine("		,tou3_syouhin_cd")
            .AppendLine("		,tou3_syouhin_mei")
            .AppendLine("		,tys_seikyuu_saki_kbn")
            .AppendLine("		,tys_seikyuu_saki_cd")
            .AppendLine("		,tys_seikyuu_saki_brc")
            .AppendLine("		,tys_seikyuu_saki_mei")
            .AppendLine("		,koj_seikyuu_saki_kbn")
            .AppendLine("		,koj_seikyuu_saki_cd")
            .AppendLine("		,koj_seikyuu_saki_brc")
            .AppendLine("		,koj_seikyuu_saki_mei")
            .AppendLine("		,hansokuhin_seikyuu_saki_kbn")
            .AppendLine("		,hansokuhin_seikyuu_saki_cd")
            .AppendLine("		,hansokuhin_seikyuu_saki_brc")
            .AppendLine("		,hansokuhin_seikyuu_saki_mei")
            .AppendLine("		,tatemono_seikyuu_saki_kbn")
            .AppendLine("		,tatemono_seikyuu_saki_cd")
            .AppendLine("		,tatemono_seikyuu_saki_brc")
            .AppendLine("		,tatemono_seikyuu_saki_mei")
            .AppendLine("		,seikyuu_saki_kbn5")
            .AppendLine("		,seikyuu_saki_cd5")
            .AppendLine("		,seikyuu_saki_brc5")
            .AppendLine("		,seikyuu_saki_mei5")
            .AppendLine("		,seikyuu_saki_kbn6")
            .AppendLine("		,seikyuu_saki_cd6")
            .AppendLine("		,seikyuu_saki_brc6")
            .AppendLine("		,seikyuu_saki_mei6")
            .AppendLine("		,seikyuu_saki_kbn7")
            .AppendLine("		,seikyuu_saki_cd7")
            .AppendLine("		,seikyuu_saki_brc7")
            .AppendLine("		,seikyuu_saki_mei7")
            '================↓2013/03/06 車龍 407584 追加↓========================
            .AppendLine("		,hosyou_kikan ") '--保証期間
            .AppendLine("		,hosyousyo_hak_umu ") '--保証書発行有無
            '================↑2013/03/06 車龍 407584 追加↑========================
            .AppendLine("		,nyuukin_kakunin_jyouken")
            .AppendLine("		,nyuukin_kakunin_oboegaki")
            .AppendLine("		,tys_mitsyo_flg")
            .AppendLine("		,hattyuusyo_flg")
            .AppendLine("		,yuubin_no")
            .AppendLine("		,jyuusyo1")
            .AppendLine("		,jyuusyo2")
            .AppendLine("		,syozaichi_cd")
            .AppendLine("		,syozaichi_mei")
            .AppendLine("		,busyo_mei")
            .AppendLine("		,daihyousya_mei")
            .AppendLine("		,tel_no")
            .AppendLine("		,fax_no")
            .AppendLine("		,mail_address")
            .AppendLine("		,bikou1")
            .AppendLine("		,bikou2")
            '================↓2013/03/06 車龍 407584 追加↓========================
            .AppendLine("		,add_date") '--登録日
            .AppendLine("		,seikyuu_umu") '--請求有無
            .AppendLine("		,syouhin_cd") '--商品コード
            .AppendLine("		,syouhin_mei") '--商品名
            .AppendLine("		,uri_gaku") '--売上金額
            .AppendLine("		,koumuten_seikyuu_gaku") '--工務店請求金額
            .AppendLine("		,seikyuusyo_hak_date") '--請求書発行日
            .AppendLine("		,uri_date") '--売上年月日
            .AppendLine("		,bikou") '--備考
            '================↑2013/03/06 車龍 407584 追加↑========================
            '==========2012/05/09 車龍 407553の対応 修正↓======================
            .AppendLine("		,kameiten_upd_datetime")
            .AppendLine("		,tatouwari_upd_datetime1")
            .AppendLine("		,tatouwari_upd_datetime2")
            .AppendLine("		,tatouwari_upd_datetime3")
            .AppendLine("		,kameiten_jyuusyo_upd_datetime")
            '==========2012/05/09 車龍 407553の対応 修正↑======================
            .AppendLine("		,bikou_syubetu1")
            .AppendLine("		,bikou_syubetu1_mei")
            .AppendLine("		,bikou_syubetu2")
            .AppendLine("		,bikou_syubetu2_mei")
            .AppendLine("		,bikou_syubetu3")
            .AppendLine("		,bikou_syubetu3_mei")
            .AppendLine("		,bikou_syubetu4")
            .AppendLine("		,bikou_syubetu4_mei")
            .AppendLine("		,bikou_syubetu5")
            .AppendLine("		,bikou_syubetu5_mei")
            .AppendLine("		,naiyou1")
            .AppendLine("		,naiyou2")
            .AppendLine("		,naiyou3")
            .AppendLine("		,naiyou4")
            .AppendLine("		,naiyou5")
            .AppendLine("		,add_login_user_id")
            .AppendLine("		,add_datetime")
            .AppendLine("		,upd_login_user_id")
            .AppendLine("		,upd_datetime")
            .AppendLine(" FROM  ")
            .AppendLine("    m_kameiten_info_ittukatu_error WITH(READCOMMITTED) ")  '加盟店情報一括取込エラー情報テーブル
            .AppendLine(" WHERE ")
            .AppendLine("    edi_jouhou_sakusei_date = @edi_jouhou_sakusei_date ")   'EDI情報作成日
            .AppendLine(" AND CONVERT(varchar(100),syori_datetime,112) + REPLACE(CONVERT(varchar(100),syori_datetime,114),':','') = @syori_datetime ")  '処理日時
            .AppendLine(" ORDER BY ")
            .AppendLine("    gyou_no ")
        End With
        'EDI情報作成日
        paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, strEdidate))
        '処理日時
        paramList.Add(MakeParam("@syori_datetime", SqlDbType.VarChar, 40, strSyoridate))
        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsReturn, "TokubetuTaiouError", paramList.ToArray)

        Return dsReturn.Tables(0)

    End Function

    ''' <summary>
    ''' 加盟店マスタの更新日時を取得する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2012/05/08 車龍 407553の対応 追加</history>
    Public Function SelKameitenUpdDate(ByVal strKameitenCd As String) As Data.DataTable

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet
        'SQLコメント
        Dim commandTextSb As New StringBuilder
        ''パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   CASE ")
            .AppendLine("       WHEN upd_datetime IS NOT NULL THEN ")
            .AppendLine("           REPLACE(CONVERT(VARCHAR(10),upd_datetime,112) + '_' + CONVERT(VARCHAR(10),upd_datetime,108),':','') ") '--更新日時
            .AppendLine("       ELSE ")
            .AppendLine("           REPLACE(CONVERT(VARCHAR(10),add_datetime,112) + '_' + CONVERT(VARCHAR(10),add_datetime,108),':','') ") '--登録日時
            .AppendLine("       END AS upd_datetime ")
            .AppendLine("FROM ")
            .AppendLine("	m_kameiten WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	kameiten_cd = @kameiten_cd ")
        End With

        'パラメータ
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))

        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsReturn, "dtKameitenUpdDate", paramList.ToArray)

        Return dsReturn.Tables("dtKameitenUpdDate")
    End Function

    ''' <summary>
    ''' 加盟店住所マスタの更新日時を取得する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2012/05/08 車龍 407553の対応 追加</history>
    Public Function SelKameitenJyuusyoUpdDate(ByVal strKameitenCd As String) As Data.DataTable

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet
        'SQLコメント
        Dim commandTextSb As New StringBuilder
        ''パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   CASE ")
            .AppendLine("       WHEN upd_datetime IS NOT NULL THEN ")
            .AppendLine("           REPLACE(CONVERT(VARCHAR(10),upd_datetime,112) + '_' + CONVERT(VARCHAR(10),upd_datetime,108),':','') ") '--更新日時
            .AppendLine("       ELSE ")
            .AppendLine("           REPLACE(CONVERT(VARCHAR(10),add_datetime,112) + '_' + CONVERT(VARCHAR(10),add_datetime,108),':','') ") '--登録日時
            .AppendLine("       END AS upd_datetime ")
            .AppendLine("FROM ")
            .AppendLine("	m_kameiten_jyuusyo WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	kameiten_cd = @kameiten_cd ")
            .AppendLine("	AND ")
            .AppendLine("	jyuusyo_no = @jyuusyo_no ")
        End With

        'パラメータ
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))
        paramList.Add(MakeParam("@jyuusyo_no", SqlDbType.VarChar, 1, "1"))

        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsReturn, "dtKameitenJyuusyoUpdDate", paramList.ToArray)

        Return dsReturn.Tables("dtKameitenJyuusyoUpdDate")
    End Function

    ''' <summary>
    ''' 多棟割引設定マスタの更新日時を取得する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2012/05/08 車龍 407553の対応 追加</history>
    Public Function SelTatouwaribikiSetteiUpdDate(ByVal strKameitenCd As String, ByVal strToukubun As String) As Data.DataTable

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet
        'SQLコメント
        Dim commandTextSb As New StringBuilder
        ''パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   CASE ")
            .AppendLine("       WHEN upd_datetime IS NOT NULL THEN ")
            .AppendLine("           REPLACE(CONVERT(VARCHAR(10),upd_datetime,112) + '_' + CONVERT(VARCHAR(10),upd_datetime,108),':','') ") '--更新日時
            .AppendLine("       ELSE ")
            .AppendLine("           REPLACE(CONVERT(VARCHAR(10),add_datetime,112) + '_' + CONVERT(VARCHAR(10),add_datetime,108),':','') ") '--登録日時
            .AppendLine("       END AS upd_datetime ")
            .AppendLine("FROM ")
            .AppendLine("	m_tatouwaribiki_settei WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	kameiten_cd = @kameiten_cd ")
            .AppendLine("	AND ")
            .AppendLine("	toukubun = @toukubun ")
        End With

        'パラメータ
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))
        paramList.Add(MakeParam("@toukubun", SqlDbType.Int, 10, strToukubun))

        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsReturn, "dtTatouwaribikiSetteiUpdDate", paramList.ToArray)

        Return dsReturn.Tables("dtTatouwaribikiSetteiUpdDate")
    End Function

    ''' <summary>
    ''' 商品の倉庫コードをチェックする
    ''' </summary>
    ''' <param name="strSyouhinCd">商品コード</param>
    ''' <param name="strSoukoCd">倉庫コード</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/03/08 車龍 407584 追加</history>
    Public Function SelSyouhinSoukoCdCheck(ByVal strSyouhinCd As String, ByVal strSoukoCd As String) As Boolean

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet
        'SQLコメント
        Dim commandTextSb As New StringBuilder
        ''パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	syouhin_cd ") '--商品コード ")
            .AppendLine("	,ISNULL(CONVERT(VARCHAR(10),souko_cd),'') AS souko_cd ") '--倉庫コード ")
            .AppendLine("FROM ")
            .AppendLine("	m_syouhin WITH(READCOMMITTED) ") '--商品マスタ ")
            .AppendLine("WHERE ")
            .AppendLine("	syouhin_cd = @syouhin_cd ") '--商品コード ")
        End With

        'パラメータ
        paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, strSyouhinCd))

        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsReturn, "dtSyouhinSoukoCdCheck", paramList.ToArray)

        If dsReturn.Tables(0).Rows.Count > 0 Then
            If dsReturn.Tables(0).Rows(0).Item("souko_cd").ToString.Trim.Equals(strSoukoCd) Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If

    End Function

    ''' <summary>
    ''' 請求先マスタを取得する
    ''' </summary>
    ''' <param name="strSeikyuuSakiCd">請求先コード</param>
    ''' <param name="strSeikyuuSakiBrc">請求先枝番</param>
    ''' <param name="strSeikyuuSakiKbn">請求先区分</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/03/08 車龍 407584 追加</history>
    Public Function SelSeikyuuSaki(ByVal strSeikyuuSakiCd As String, ByVal strSeikyuuSakiBrc As String, ByVal strSeikyuuSakiKbn As String) As Data.DataTable

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet
        'SQLコメント
        Dim commandTextSb As New StringBuilder
        ''パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	seikyuu_saki_cd ") '--請求先コード ")
            .AppendLine("	,seikyuu_saki_brc ") '--請求先枝番 ")
            .AppendLine("	,seikyuu_saki_kbn ") '--請求先区分 ")
            .AppendLine("	,torikesi ") '--取消 ")
            .AppendLine("FROM ")
            .AppendLine("	m_seikyuu_saki WITH(READCOMMITTED) ") '--請求先マスタ ")
            .AppendLine("WHERE ")
            .AppendLine("	seikyuu_saki_cd = @seikyuu_saki_cd ") '--請求先コード ")
            .AppendLine("	AND ")
            .AppendLine("	seikyuu_saki_brc = @seikyuu_saki_brc ") '--請求先枝番 ")
            .AppendLine("	AND ")
            .AppendLine("	seikyuu_saki_kbn = @seikyuu_saki_kbn ") '--請求先区分 ")
        End With

        'パラメータ
        paramList.Add(MakeParam("@seikyuu_saki_cd", SqlDbType.VarChar, 5, strSeikyuuSakiCd))
        paramList.Add(MakeParam("@seikyuu_saki_brc", SqlDbType.VarChar, 2, strSeikyuuSakiBrc))
        paramList.Add(MakeParam("@seikyuu_saki_kbn", SqlDbType.VarChar, 1, strSeikyuuSakiKbn))

        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsReturn, "dtSeikyuuSaki", paramList.ToArray)

        Return dsReturn.Tables("dtSeikyuuSaki")

    End Function

    ''' <summary>
    ''' 請求先マスタを登録、変更する
    ''' </summary>
    ''' <param name="dtOk">データテーブル</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/03/08 車龍 407584 追加</history>
    Public Function InsUpdSeikyuuSaki(ByVal dtOk As Data.DataTable) As Boolean

        '戻り値
        Dim InsUpdCount As Integer = 0

        Dim sqlIns As New System.Text.StringBuilder
        Dim sqlUpd As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With sqlIns
            .AppendLine("INSERT INTO  ")
            .AppendLine("	m_seikyuu_saki WITH(UPDLOCK) ") '--請求先マスタ ")
            .AppendLine("	( ")
            .AppendLine("		seikyuu_saki_cd ") '--請求先コード ")
            .AppendLine("		,seikyuu_saki_brc ") '--請求先枝番 ")
            .AppendLine("		,seikyuu_saki_kbn ") '--請求先区分 ")
            .AppendLine("		,torikesi ") '--取消 ")
            .AppendLine("		,skk_jigyousyo_cd ") '--新会計事業所コード ")
            .AppendLine("		,kyuu_seikyuu_saki_cd ") '--旧請求先コード ")
            .AppendLine("		,nayose_saki_cd ") '--名寄先コード ")
            .AppendLine("		,tantousya_mei ") '--担当者名 ")
            .AppendLine("		,seikyuusyo_inji_bukken_mei_flg ") '--請求書印字物件名フラグ ")
            .AppendLine("		,skysy_soufu_jyuusyo1 ") '--請求書送付先住所1 ")
            .AppendLine("		,skysy_soufu_jyuusyo2 ") '--請求書送付先住所2 ")
            .AppendLine("		,skysy_soufu_yuubin_no ") '--請求書送付先郵便番号 ")
            .AppendLine("		,skysy_soufu_tel_no ") '--請求書送付先電話番号 ")
            .AppendLine("		,skysy_soufu_fax_no ") '--請求書送付先FAX番号 ")
            .AppendLine("		,nyuukin_kouza_no ") '--入金口座番号 ")
            .AppendLine("		,seikyuu_sime_date ") '--請求締め日 ")
            .AppendLine("		,senpou_seikyuu_sime_date ") '--先方請求締め日 ")
            .AppendLine("		,kessanji_nidosime_flg ") '--決算時二度締めフラグ ")
            .AppendLine("		,tyk_koj_seikyuu_timing_flg ") '--直工事請求タイミングフラグ ")
            .AppendLine("		,sousai_flg ") '--相殺フラグ ")
            .AppendLine("		,kaisyuu_yotei_gessuu ") '--回収予定月数 ")
            .AppendLine("		,kaisyuu_yotei_date ") '--回収予定日 ")
            .AppendLine("		,seikyuusyo_hittyk_date ") '--請求書必着日 ")
            .AppendLine("		,kaisyuu1_syubetu1 ") '--回収1種別1 ")
            .AppendLine("		,kaisyuu1_wariai1 ") '--回収1割合1 ")
            .AppendLine("		,kaisyuu1_tegata_site_gessuu ") '--回収1手形サイト月数 ")
            .AppendLine("		,kaisyuu1_tegata_site_date ") '--回収1手形サイト日 ")
            .AppendLine("		,kaisyuu1_seikyuusyo_yousi ") '--回収1請求書用紙 ")
            .AppendLine("		,kaisyuu1_syubetu2 ") '--回収1種別2 ")
            .AppendLine("		,kaisyuu1_wariai2 ") '--回収1割合2 ")
            .AppendLine("		,kaisyuu1_syubetu3 ") '--回収1種別3 ")
            .AppendLine("		,kaisyuu1_wariai3 ") '--回収1割合3 ")
            .AppendLine("		,kaisyuu_kyoukaigaku ") '--回収境界額 ")
            .AppendLine("		,kaisyuu2_syubetu1 ") '--回収2種別1 ")
            .AppendLine("		,kaisyuu2_wariai1 ") '--回収2割合1 ")
            .AppendLine("		,kaisyuu2_tegata_site_gessuu ") '--回収2手形サイト月数 ")
            .AppendLine("		,kaisyuu2_tegata_site_date ") '--回収2手形サイト日 ")
            .AppendLine("		,kaisyuu2_seikyuusyo_yousi ") '--回収2請求書用紙 ")
            .AppendLine("		,kaisyuu2_syubetu2 ") '--回収2種別2 ")
            .AppendLine("		,kaisyuu2_wariai2 ") '--回収2割合2 ")
            .AppendLine("		,kaisyuu2_syubetu3 ") '--回収2種別3 ")
            .AppendLine("		,kaisyuu2_wariai3 ") '--回収2割合3 ")
            .AppendLine("		,koufuri_ok_flg ") '--口振OKフラグ ")
            .AppendLine("		,tougou_tokuisaki_cd ") '--統合会計得意先ｺｰﾄﾞ ")
            .AppendLine("		,anzen_kaihi_en ") '--安全協力会費_円 ")
            .AppendLine("		,anzen_kaihi_wari ") '--安全協力会費_割合 ")
            .AppendLine("		,bikou ") '--備考 ")
            .AppendLine("		,add_login_user_id ") '--登録ログインユーザーID ")
            .AppendLine("		,add_datetime ") '--登録日時 ")
            .AppendLine("		,upd_login_user_id ") '--更新ログインユーザーID ")
            .AppendLine("		,upd_datetime ") '--更新日時 ")
            '2013/5/29 請求書の銀行支店コード増加に伴うEarth改修 -----------------------↓
            .AppendLine("		,ginkou_siten_cd ") '--銀行支店コード ")
            '2013/5/29 請求書の銀行支店コード増加に伴うEarth改修 -----------------------↑
            .AppendLine("	) ")
            .AppendLine("SELECT ")
            .AppendLine("	@seikyuu_saki_cd ") '--請求先コード ")
            .AppendLine("	,@seikyuu_saki_brc ") '--請求先枝番 ")
            .AppendLine("	,@seikyuu_saki_kbn ") '--請求先区分 ")
            .AppendLine("	,@torikesi ") '--取消 ")
            .AppendLine("	,MSSTH.skk_jigyousyo_cd ") '--新会計事業所コード ")
            .AppendLine("	,NULL AS kyuu_seikyuu_saki_cd ") '--旧請求先コード ")
            .AppendLine("	,NULL AS nayose_saki_cd ") '--名寄先コード ")
            .AppendLine("	,MSSTH.tantousya_mei ") '--担当者名 ")
            .AppendLine("	,MSSTH.seikyuusyo_inji_bukken_mei_flg ") '--請求書印字物件名フラグ ")
            .AppendLine("	,NULL AS skysy_soufu_jyuusyo1 ") '--請求書送付先住所1 ")
            .AppendLine("	,NULL AS skysy_soufu_jyuusyo2 ") '--請求書送付先住所2 ")
            .AppendLine("	,NULL AS skysy_soufu_yuubin_no ") '--請求書送付先郵便番号 ")
            .AppendLine("	,NULL AS skysy_soufu_tel_no ") '--請求書送付先電話番号 ")
            .AppendLine("	,NULL AS skysy_soufu_fax_no ") '--請求書送付先FAX番号 ")
            .AppendLine("	,MSSTH.nyuukin_kouza_no ") '--入金口座番号 ")
            .AppendLine("	,MSSTH.seikyuu_sime_date ") '--請求締め日 ")
            .AppendLine("	,MSSTH.senpou_seikyuu_sime_date ") '--先方請求締め日 ")
            .AppendLine("	,NULL AS kessanji_nidosime_flg ") '--決算時二度締めフラグ ")
            .AppendLine("	,MSSTH.tyk_koj_seikyuu_timing_flg ") '--直工事請求タイミングフラグ ")
            .AppendLine("	,MSSTH.sousai_flg ") '--相殺フラグ ")
            .AppendLine("	,MSSTH.kaisyuu_yotei_gessuu ") '--回収予定月数 ")
            .AppendLine("	,MSSTH.kaisyuu_yotei_date ") '--回収予定日 ")
            .AppendLine("	,MSSTH.seikyuusyo_hittyk_date ") '--請求書必着日 ")
            .AppendLine("	,MSSTH.kaisyuu1_syubetu1 ") '--回収1種別1 ")
            .AppendLine("	,MSSTH.kaisyuu1_wariai1 ") '--回収1割合1 ")
            .AppendLine("	,MSSTH.kaisyuu1_tegata_site_gessuu ") '--回収1手形サイト月数 ")
            .AppendLine("	,MSSTH.kaisyuu1_tegata_site_date ") '--回収1手形サイト日 ")
            .AppendLine("	,MSSTH.kaisyuu1_seikyuusyo_yousi ") '--回収1請求書用紙 ")
            .AppendLine("	,MSSTH.kaisyuu1_syubetu2 ") '--回収1種別2 ")
            .AppendLine("	,MSSTH.kaisyuu1_wariai2 ") '--回収1割合2 ")
            .AppendLine("	,MSSTH.kaisyuu1_syubetu3 ") '--回収1種別3 ")
            .AppendLine("	,MSSTH.kaisyuu1_wariai3 ") '--回収1割合3 ")
            .AppendLine("	,MSSTH.kaisyuu_kyoukaigaku ") '--回収境界額 ")
            .AppendLine("	,MSSTH.kaisyuu2_syubetu1 ") '--回収2種別1 ")
            .AppendLine("	,MSSTH.kaisyuu2_wariai1 ") '--回収2割合1 ")
            .AppendLine("	,MSSTH.kaisyuu2_tegata_site_gessuu ") '--回収2手形サイト月数 ")
            .AppendLine("	,MSSTH.kaisyuu2_tegata_site_date ") '--回収2手形サイト日 ")
            .AppendLine("	,MSSTH.kaisyuu2_seikyuusyo_yousi ") '--回収2請求書用紙 ")
            .AppendLine("	,MSSTH.kaisyuu2_syubetu2 ") '--回収2種別2 ")
            .AppendLine("	,MSSTH.kaisyuu2_wariai2 ") '--回収2割合2 ")
            .AppendLine("	,MSSTH.kaisyuu2_syubetu3 ") '--回収2種別3 ")
            .AppendLine("	,MSSTH.kaisyuu2_wariai3 ") '--回収2割合3 ")
            .AppendLine("	,NULL AS koufuri_ok_flg ") '--口振OKフラグ ")
            .AppendLine("	,NULL AS tougou_tokuisaki_cd ") '--統合会計得意先ｺｰﾄﾞ ")
            .AppendLine("	,NULL AS anzen_kaihi_en ") '--安全協力会費_円 ")
            .AppendLine("	,NULL AS anzen_kaihi_wari ") '--安全協力会費_割合 ")
            .AppendLine("	,NULL AS bikou ") '--備考 ")
            .AppendLine("	,@add_login_user_id ") '--登録ログインユーザーID ")
            .AppendLine("	,GETDATE() AS add_datetime ") '--登録日時 ")
            .AppendLine("	,NULL AS upd_login_user_id ") '--更新ログインユーザーID ")
            .AppendLine("	,NULL AS upd_datetime ") '--更新日時 ")
            '2013/5/29 請求書の銀行支店コード増加に伴うEarth改修 -----------------------↓
            .AppendLine("	,MSSTH.ginkou_siten_cd ") '--銀行支店コード ")
            '2013/5/29 請求書の銀行支店コード増加に伴うEarth改修 -----------------------↑
            .AppendLine("FROM ")
            .AppendLine("	m_seikyuu_saki_touroku_hinagata AS MSSTH WITH(READCOMMITTED) ") '--請求先登録雛形マスタ ")
            .AppendLine("WHERE ")
            .AppendLine("	seikyuu_saki_brc = '00' ") '--請求先枝番 ")
            .AppendLine("	AND ")
            .AppendLine("	torikesi = '0' ") '--取消 ")
            .AppendLine("	AND ")
            .AppendLine("	kihon_flg = '1' ") '--基本ﾌﾗｸﾞ ")
        End With

        With sqlUpd
            .AppendLine("UPDATE ")
            .AppendLine("	m_seikyuu_saki WITH(UPDLOCK) ") '--請求先マスタ ")
            .AppendLine("SET ")
            .AppendLine("	torikesi = @torikesi ") '--取消 ")
            .AppendLine("WHERE ")
            .AppendLine("	seikyuu_saki_cd = @seikyuu_saki_cd ") '--請求先コード ")
            .AppendLine("	AND ")
            .AppendLine("	seikyuu_saki_brc = seikyuu_saki_brc ") '--請求先枝番 ")
            .AppendLine("	AND ")
            .AppendLine("	seikyuu_saki_kbn = seikyuu_saki_kbn ") '--請求先区分 ")
        End With

        '加盟店コード
        Dim strKameitenCd As String
        'ユーザーID
        Dim strUserId As String
        'テーブル
        Dim dtSeikyuuSaki As New Data.DataTable

        For i As Integer = 0 To dtOk.Rows.Count - 1
            '加盟店コード
            strKameitenCd = dtOk.Rows(i).Item("kameiten_cd").ToString.Trim
            'ユーザーID
            strUserId = dtOk.Rows(i).Item("add_login_user_id").ToString.Trim

            paramList.Clear()
            paramList.Add(MakeParam("@seikyuu_saki_cd", SqlDbType.VarChar, 5, strKameitenCd)) '請求先コード
            paramList.Add(MakeParam("@seikyuu_saki_brc", SqlDbType.VarChar, 2, "00")) '請求先枝番
            paramList.Add(MakeParam("@seikyuu_saki_kbn", SqlDbType.VarChar, 1, "0")) '請求先枝番
            paramList.Add(MakeParam("@torikesi", SqlDbType.VarChar, 1, "0")) '取消
            paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, strUserId)) '登録ログインユーザーID

            dtSeikyuuSaki = Me.SelSeikyuuSaki(strKameitenCd, "00", "0")
            Try
                If dtSeikyuuSaki.Rows.Count = 0 Then
                    'アップロード.加盟店ｺｰﾄﾞ　枝番　00　区分　0　が　請求先ﾏｽﾀ.請求先ｺｰﾄﾞ・請求先枝番・請求先区分に存在しない場合)	
                    '請求先登録雛形マスタの請求先枝番 = 00 、取消=0、基本ﾌﾗｸﾞ=1　から該当のレコードを取得し、セット
                    InsUpdCount = ExecuteNonQuery(connStr, CommandType.Text, sqlIns.ToString(), paramList.ToArray)
                Else
                    If dtSeikyuuSaki.Rows(0).Item("torikesi").ToString.Trim.Equals("1") Then
                        '請求マスタ.取消 = 1 で アップロード.加盟店ｺｰﾄﾞ　枝番　00　区分　0　が	
                        '請求先ﾏｽﾀ.請求先ｺｰﾄﾞ・請求先枝番・請求先区分に存在する時 は 取消 = 0 をセット
                        InsUpdCount = ExecuteNonQuery(connStr, CommandType.Text, sqlUpd.ToString(), paramList.ToArray)
                    End If
                End If
            Catch ex As Exception
                Return False
            End Try
        Next

        Return True
    End Function

    ''' <summary>
    ''' 店別初期請求テーブルの件数を取得する
    ''' </summary>
    ''' <param name="strMiseCd">請求先コード</param>
    ''' <param name="strBunruiCd">請求先枝番</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/03/09 車龍 407584 追加</history>
    Public Function SelTenbetuSyokiSeikyuuCount(ByVal strMiseCd As String, ByVal strBunruiCd As String) As Integer

        'SQLコメント
        Dim commandTextSb As New StringBuilder
        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        '件数
        Dim intCount As Integer = 0

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	COUNT(mise_cd) AS CNT ") '--件数 ")
            .AppendLine("FROM ")
            .AppendLine("	t_tenbetu_syoki_seikyuu WITH(READCOMMITTED) ") '--店別初期請求テーブル ")
            .AppendLine("WHERE ")
            .AppendLine("	mise_cd = @mise_cd ") '--店コード ")
            .AppendLine("	AND ")
            .AppendLine("	bunrui_cd = @bunrui_cd ") '--分類コード ")
        End With

        'パラメータ
        paramList.Add(MakeParam("@mise_cd", SqlDbType.VarChar, 5, strMiseCd))
        paramList.Add(MakeParam("@bunrui_cd", SqlDbType.VarChar, 3, strBunruiCd))

        '検索実行
        intCount = Convert.ToInt32(ExecuteScalar(connStr, CommandType.Text, commandTextSb.ToString, paramList.ToArray))

        '戻り値
        Return intCount

    End Function

    ''' <summary>
    ''' 店別初期請求テーブルを登録する
    ''' </summary>
    ''' <param name="dtOk">データテーブル</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/03/09 車龍 407584 追加</history>
    Public Function InsTenbetuSyokiSeikyuu(ByVal dtOk As Data.DataTable) As Boolean

        '戻り値
        Dim InsUpdCount As Integer = 0

        Dim sqlIns As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        Dim intInsCount As Integer = 0

        With sqlIns
            .AppendLine("INSERT INTO ")
            .AppendLine("	t_tenbetu_syoki_seikyuu  WITH(UPDLOCK) ") '--店別初期請求テーブル ")
            .AppendLine("	( ")
            .AppendLine("		mise_cd ") '--店コード ")
            .AppendLine("		,bunrui_cd ") '--分類コード ")
            .AppendLine("		,add_date ") '--登録日 ")
            .AppendLine("		,seikyuusyo_hak_date ") '--請求書発行日 ")
            .AppendLine("		,uri_date ") '--売上年月日 ")
            .AppendLine("		,denpyou_uri_date ") '--伝票売上年月日 ")
            .AppendLine("		,seikyuu_umu ") '--請求有無 ")
            .AppendLine("		,uri_keijyou_flg ") '--売上処理FLG(売上計上FLG) ")
            .AppendLine("		,uri_keijyou_date ") '--売上処理日(売上計上日) ")
            .AppendLine("		,syouhin_cd ") '--商品コード ")
            .AppendLine("		,uri_gaku ") '--売上金額 ")
            .AppendLine("		,zei_kbn ") '--税区分 ")
            .AppendLine("		,syouhizei_gaku ") '--消費税額 ")
            .AppendLine("		,bikou ") '--備考 ")
            .AppendLine("		,koumuten_seikyuu_gaku ") '--工務店請求金額 ")
            .AppendLine("		,add_login_user_id ") '--登録ログインユーザーID ")
            .AppendLine("		,add_datetime ") '--登録日時 ")
            .AppendLine("		,upd_login_user_id ") '--更新ログインユーザーID ")
            .AppendLine("		,upd_datetime ") '--更新日時 ")
            .AppendLine("	) ")
            .AppendLine("VALUES ")
            .AppendLine("( ")
            .AppendLine("	@mise_cd ") '--店コード ")
            .AppendLine("	,@bunrui_cd ") '--分類コード ")
            .AppendLine("	,@add_date ") '--登録日 ")
            .AppendLine("	,@seikyuusyo_hak_date ") '--請求書発行日 ")
            .AppendLine("	,CONVERT(DATETIME,CONVERT(VARCHAR(10),GETDATE(),111))") '--売上年月日 ")
            .AppendLine("	,CONVERT(DATETIME,CONVERT(VARCHAR(10),GETDATE(),111)) ") '--伝票売上年月日 ")
            .AppendLine("	,@seikyuu_umu ") '--請求有無 ")
            .AppendLine("	,@uri_keijyou_flg ") '--売上処理FLG(売上計上FLG) ")
            .AppendLine("	,NULL ") '--売上処理日(売上計上日) ")
            .AppendLine("	,@syouhin_cd ") '--商品コード ")
            .AppendLine("	,@uri_gaku ") '--売上金額 ")
            .AppendLine("	,@zei_kbn ") '--税区分 ")
            .AppendLine("	,@syouhizei_gaku ") '--消費税額 ")
            .AppendLine("	,@bikou ") '--備考 ")
            .AppendLine("	,@koumuten_seikyuu_gaku ") '--工務店請求金額 ")
            .AppendLine("	,@add_login_user_id ") '--登録ログインユーザーID ")
            .AppendLine("	,GETDATE() ") '--登録日時 ")
            .AppendLine("	,NULL ") '--更新ログインユーザーID ")
            .AppendLine("	,NULL ") '--更新日時 ")
            .AppendLine(") ")
        End With

        '店コード
        Dim strMiseCd As String = String.Empty
        '登録日
        Dim strAddDate As String = String.Empty
        Dim addDate As New DateTime
        '請求書発行日
        Dim strSeikyuusyoHakDate As String = String.Empty
        '請求有無
        Dim strSeikyuu_umu As String = String.Empty
        '商品コード
        Dim strSyouhinCd As String = String.Empty
        '税区分
        Dim strZeiKbn As String = String.Empty
        '売上金額
        Dim strUriGaku As String = String.Empty
        '消費税額
        Dim strSyouhizeiGaku As String = String.Empty
        '工務店請求金額
        Dim strKoumutenSeikyuuGaku As String = String.Empty

        '調査請求先コード
        Dim strTysSeikyuuSakiCd As String = String.Empty
        '調査請求先枝番
        Dim strTysSeikyuuSakiBrc As String = String.Empty
        '調査請求先区分
        Dim strTysSeikyuuSakiKbn As String = String.Empty

        '商品情報
        Dim dtSyouhinInfo As New Data.DataTable

        For i As Integer = 0 To dtOk.Rows.Count - 1

            '店コード
            strMiseCd = dtOk.Rows(i).Item("kameiten_cd").ToString.Trim

            If Me.SelTenbetuSyokiSeikyuuCount(strMiseCd, "200") > 0 Then
                '(アップロード.加盟店ｺｰﾄﾞ = 店別初期請求テーブル.店コード 且つ 店別初期請求テーブル. 分類コード=200　で存在する場合)　は	
                '※上書き更新は対応としない	
            Else
                '(アップロード.加盟店ｺｰﾄﾞ=店別初期請求テーブル.店コード 且つ 店別初期請求テーブル. 分類コード=200　で存在しない場合)

                '請求有無
                strSeikyuu_umu = dtOk.Rows(i).Item("seikyuu_umu").ToString.Trim
                If strSeikyuu_umu.Equals(String.Empty) Then
                    strSeikyuu_umu = "0"
                End If

                '登録日
                strAddDate = dtOk.Rows(i).Item("add_date").ToString.Trim
                If strAddDate.Equals(String.Empty) Then
                    addDate = Me.GetSysDate()
                Else
                    Try
                        strAddDate = Left(strAddDate, 4) & "/" & Mid(strAddDate, 5, 2) & "/" & Mid(strAddDate, 7, 2)
                        addDate = Convert.ToDateTime(strAddDate)
                    Catch ex As Exception
                        addDate = Me.GetSysDate()
                    End Try
                End If

                '調査請求先コード
                strTysSeikyuuSakiCd = dtOk.Rows(i).Item("tys_seikyuu_saki_cd").ToString.Trim
                '調査請求先枝番
                strTysSeikyuuSakiBrc = dtOk.Rows(i).Item("tys_seikyuu_saki_brc").ToString.Trim
                '調査請求先区分
                strTysSeikyuuSakiKbn = dtOk.Rows(i).Item("tys_seikyuu_saki_kbn").ToString.Trim

                If strSeikyuu_umu.Equals("0") Then
                    '請求有無 0　(請求無し選択時)

                    '商品コード
                    strSyouhinCd = "C0099"

                    '請求書発行日(更新対象としない)
                    strSeikyuusyoHakDate = String.Empty

                Else
                    '請求有無 1　(請求有り選択時)

                    '商品コード
                    strSyouhinCd = dtOk.Rows(i).Item("syouhin_cd").ToString.Trim

                    '請求書発行日(請求書発行日←システム年月＋請求先マスタ．請求締め日 )
                    strSeikyuusyoHakDate = Me.GetSeikyuusyoHakDate(strTysSeikyuuSakiCd, strTysSeikyuuSakiBrc, strTysSeikyuuSakiKbn)

                End If

                '商品情報
                dtSyouhinInfo = Me.SelSyouhinInfo(strSyouhinCd)

                '税区分
                If dtSyouhinInfo.Rows.Count > 0 Then
                    strZeiKbn = dtSyouhinInfo.Rows(0).Item("zei_kbn").ToString.Trim
                Else
                    strZeiKbn = String.Empty
                End If

                If strSeikyuu_umu.Equals("0") Then
                    '請求有無 0　(請求無し選択時)

                    '売上金額
                    strUriGaku = "0"

                    '消費税額
                    strSyouhizeiGaku = "0"

                    '工務店請求金額
                    strKoumutenSeikyuuGaku = "0"
                Else
                    '請求有無 1　(請求有り選択時)

                    '売上金額
                    strUriGaku = dtOk.Rows(i).Item("uri_gaku").ToString.Trim
                    If strUriGaku.Equals(String.Empty) Then
                        '売上金額が空白の場合、商品ﾏｽﾀ.標準価格
                        If dtSyouhinInfo.Rows.Count > 0 Then
                            strUriGaku = dtSyouhinInfo.Rows(0).Item("hyoujun_kkk").ToString.Trim
                        Else
                            strUriGaku = String.Empty
                        End If
                    End If

                    '消費税額
                    If dtSyouhinInfo.Rows.Count > 0 Then
                        If (Not dtSyouhinInfo.Rows(0).Item("zeiritu").ToString.Trim.Equals(String.Empty)) AndAlso (Not strUriGaku.Equals(String.Empty)) Then
                            '売上金額 × 消費税ﾏｽﾀ.掛率
                            strSyouhizeiGaku = Math.Round(Convert.ToDecimal(dtSyouhinInfo.Rows(0).Item("zeiritu")) * Convert.ToDecimal(strUriGaku), 0).ToString

                        Else
                            strSyouhizeiGaku = String.Empty
                        End If
                    Else
                        strSyouhizeiGaku = String.Empty
                    End If

                    '工務店請求金額
                    strKoumutenSeikyuuGaku = dtOk.Rows(i).Item("koumuten_seikyuu_gaku").ToString.Trim
                End If

                paramList.Clear()
                paramList.Add(MakeParam("@mise_cd", SqlDbType.VarChar, 5, strMiseCd)) '店コード
                paramList.Add(MakeParam("@bunrui_cd", SqlDbType.Char, 3, "200")) '分類コード
                paramList.Add(MakeParam("@add_date", SqlDbType.DateTime, 20, addDate.ToString("yyyy/MM/dd"))) '登録日
                paramList.Add(MakeParam("@seikyuusyo_hak_date", SqlDbType.DateTime, 20, InsObj(strSeikyuusyoHakDate))) '請求書発行日
                paramList.Add(MakeParam("@seikyuu_umu", SqlDbType.Int, 10, strSeikyuu_umu)) '請求有無
                paramList.Add(MakeParam("@uri_keijyou_flg", SqlDbType.Int, 10, "0")) '売上処理FLG(売上計上FLG)
                paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, strSyouhinCd)) '商品コード
                paramList.Add(MakeParam("@uri_gaku", SqlDbType.Int, 10, InsObj(strUriGaku))) '売上金額
                paramList.Add(MakeParam("@zei_kbn", SqlDbType.VarChar, 1, strZeiKbn)) '税区分
                paramList.Add(MakeParam("@syouhizei_gaku", SqlDbType.Int, 10, InsObj(strSyouhizeiGaku))) '消費税額
                paramList.Add(MakeParam("@bikou", SqlDbType.VarChar, 30, InsObj(dtOk.Rows(i).Item("bikou").ToString.Trim))) '備考
                paramList.Add(MakeParam("@koumuten_seikyuu_gaku", SqlDbType.Int, 10, InsObj(strKoumutenSeikyuuGaku))) '工務店請求金額
                paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, dtOk.Rows(i).Item("add_login_user_id").ToString.Trim)) '登録ログインユーザーID

                Try
                    '実行
                    intInsCount = ExecuteNonQuery(connStr, CommandType.Text, sqlIns.ToString(), paramList.ToArray)
                    If intInsCount = 0 Then
                        Return False
                    End If

                    '店別初期請求テーブル連携管理テーブル
                    If Not Me.InsTenbetuSyokiSeikyuuRenkei(strMiseCd, "200", dtOk.Rows(i).Item("add_login_user_id").ToString.Trim) Then
                        Throw New ApplicationException
                    End If
                Catch ex As Exception
                    Return False
                End Try

            End If
        Next

        Return True
    End Function

    ''' <summary>店別初期請求テーブル連携管理テーブルを存在チェックする</summary>
    ''' <history>2013/03/09 車龍(大連情報システム部)　新規作成</history>
    Public Function GetTenbetuSyokiSeikyuuRenkeiCount(ByVal strMiseCd As String, ByVal strBunruiCd As String) As Boolean

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        Dim intCount As Integer

        '追加用SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	COUNT(mise_cd) AS cnt ")
            .AppendLine("FROM ")
            .AppendLine("	t_tenbetu_syoki_seikyuu_renkei WITH(READCOMMITTED) ") '--店別初期請求テーブル連携管理テーブル
            .AppendLine("WHERE ")
            .AppendLine("	mise_cd = @mise_cd ") '--店コード
            .AppendLine("	AND ")
            .AppendLine("	bunrui_cd = @bunrui_cd ") '--分類コード
        End With

        'パラメータの設定
        paramList.Clear()
        paramList.Add(MakeParam("@mise_cd", SqlDbType.VarChar, 5, strMiseCd)) '--店コード
        paramList.Add(MakeParam("@bunrui_cd", SqlDbType.VarChar, 3, strBunruiCd)) '--分類コード
        ' 検索実行

        intCount = Convert.ToInt32(ExecuteScalar(connStr, CommandType.Text, commandTextSb.ToString, paramList.ToArray))

        '戻り値
        If intCount > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>
    ''' 店別初期請求テーブル連携管理テーブルを登録する
    ''' </summary>
    ''' <param name="strMiseCd">店コード</param>
    ''' <param name="strBunruiCd">分類コード</param>
    ''' <param name="strUserId">ユーザーID</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/03/09 車龍 407584 追加</history>
    Public Function InsTenbetuSyokiSeikyuuRenkei(ByVal strMiseCd As String, ByVal strBunruiCd As String, ByVal strUserId As String) As Boolean

        '戻り値
        Dim InsUpdCount As Integer = 0

        Dim sqlIns As New System.Text.StringBuilder
        Dim sqlUpd As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        Dim intInsUpdCount As Integer = 0

        With sqlIns
            .AppendLine("INSERT INTO ")
            .AppendLine("	t_tenbetu_syoki_seikyuu_renkei WITH(UPDLOCK) ") '--店別初期請求テーブル連携管理テーブル ")
            .AppendLine("	( ")
            .AppendLine("		mise_cd ") '--店コード ")
            .AppendLine("		,bunrui_cd ") '--分類コード ")
            .AppendLine("		,renkei_siji_cd ") '--連携指示コード ")
            .AppendLine("		,sousin_jyky_cd ") '--送信状況コード ")
            .AppendLine("		,upd_login_user_id ") '--更新ログインユーザーID ")
            .AppendLine("		,upd_datetime ") '--更新日時 ")
            .AppendLine("	) ")
            .AppendLine("VALUES ")
            .AppendLine("( ")
            .AppendLine("	@mise_cd ") '--店コード ")
            .AppendLine("	,@bunrui_cd ") '--分類コード ")
            .AppendLine("	,@renkei_siji_cd ") '--連携指示コード ")
            .AppendLine("	,@sousin_jyky_cd ") '--送信状況コード ")
            .AppendLine("	,@upd_login_user_id ") '--更新ログインユーザーID ")
            .AppendLine("	,GETDATE() ") '--更新日時 ")
            .AppendLine(") ")
        End With

        With sqlUpd
            .AppendLine("UPDATE ")
            .AppendLine("	t_tenbetu_syoki_seikyuu_renkei WITH(UPDLOCK) ") '--店別初期請求テーブル連携管理テーブル ")
            .AppendLine("SET ")
            .AppendLine("	renkei_siji_cd = @renkei_siji_cd ") '--連携指示コード ")
            .AppendLine("	,sousin_jyky_cd = @sousin_jyky_cd ") '--送信状況コード ")
            .AppendLine("	,upd_login_user_id = @upd_login_user_id ") '--更新ログインユーザーID ")
            .AppendLine("	,upd_datetime = GETDATE() ") '--更新日時 ")
            .AppendLine("WHERE ")
            .AppendLine("	mise_cd = @mise_cd ") '--店コード ")
            .AppendLine("	AND ")
            .AppendLine("	bunrui_cd = @bunrui_cd ") '--分類コード ")
        End With

        'パラメータの設定
        paramList.Clear()
        paramList.Add(MakeParam("@mise_cd", SqlDbType.VarChar, 5, strMiseCd)) '--店コード
        paramList.Add(MakeParam("@bunrui_cd", SqlDbType.VarChar, 3, strBunruiCd)) '--分類コード
        paramList.Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 10, "1")) '--連携指示コード
        paramList.Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 10, "0")) '--送信状況コード
        paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, strUserId)) '--更新ログインユーザーID

        Try
            If Me.GetTenbetuSyokiSeikyuuRenkeiCount(strMiseCd, strBunruiCd) Then
                '更新
                intInsUpdCount = ExecuteNonQuery(connStr, CommandType.Text, sqlUpd.ToString(), paramList.ToArray)
            Else
                '登録
                intInsUpdCount = ExecuteNonQuery(connStr, CommandType.Text, sqlIns.ToString(), paramList.ToArray)
            End If

            If intInsUpdCount = 0 Then
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function

    ''' <summary>
    ''' システム時間
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/03/09 車龍 407584 追加</history>
    Public Function GetSysDate() As DateTime

        ' SQL文の生成
        Dim sql As New StringBuilder

        ' パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        ' 加盟店マスト情報のSql 
        Sql.AppendLine("SELECT")
        Sql.AppendLine("    getdate()")

        Dim sysDate As New DateTime

        ' 検索実行
        sysDate = Convert.ToDateTime(ExecuteScalar(connStr, CommandType.Text, Sql.ToString(), paramList.ToArray))

        Return sysDate

    End Function

    ''' <summary>
    ''' 請求書発行日を取得する
    ''' </summary>
    ''' <param name="strTysSeikyuuSakiCd">調査請求先コード</param>
    ''' <param name="strTysSeikyuuSakiBrc">調査請求先枝番</param>
    ''' <param name="strTysSeikyuuSakiKbn">調査請求先区分</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/03/09 車龍 407584 追加</history>
    Public Function GetSeikyuusyoHakDate(ByVal strTysSeikyuuSakiCd As String, ByVal strTysSeikyuuSakiBrc As String, ByVal strTysSeikyuuSakiKbn As String) As DateTime

        '請求書発行日
        Dim seikyuusyoHakDate As New DateTime

        '請求締め日
        Dim strSeikyuuSimeDate As String = String.Empty

        Dim sysDate As DateTime = Me.GetSysDate()

        If strTysSeikyuuSakiCd.Equals(String.Empty) OrElse strTysSeikyuuSakiBrc.Equals(String.Empty) OrElse strTysSeikyuuSakiKbn.Equals(String.Empty) Then
            '請求締め日
            strSeikyuuSimeDate = String.Empty
        Else
            ' SQL文の生成
            Dim sql As New StringBuilder
            ' パラメータ格納
            Dim paramList As New List(Of SqlClient.SqlParameter)

            ' SQL文
            With sql
                .AppendLine("SELECT ")
                .AppendLine("	ISNULL(seikyuu_sime_date,'') AS seikyuu_sime_date ") '--請求締め日 ")
                .AppendLine("FROM ")
                .AppendLine("	m_seikyuu_saki WITH(READCOMMITTED) ") '--請求先マスタ ")
                .AppendLine("WHERE ")
                .AppendLine("	seikyuu_saki_cd = @seikyuu_saki_cd ") '--請求先コード ")
                .AppendLine("	AND ")
                .AppendLine("	seikyuu_saki_brc = @seikyuu_saki_brc ") '--請求先枝番 ")
                .AppendLine("	AND ")
                .AppendLine("	seikyuu_saki_kbn = @seikyuu_saki_kbn ") '--請求先区分 ")
            End With

            paramList.Add(MakeParam("@seikyuu_saki_cd", SqlDbType.VarChar, 5, strTysSeikyuuSakiCd))
            paramList.Add(MakeParam("@seikyuu_saki_brc", SqlDbType.VarChar, 2, strTysSeikyuuSakiBrc))
            paramList.Add(MakeParam("@seikyuu_saki_kbn", SqlDbType.VarChar, 40, strTysSeikyuuSakiKbn))

            ' 検索実行(請求締め日を取得する)
            strSeikyuuSimeDate = Convert.ToString(ExecuteScalar(connStr, CommandType.Text, sql.ToString(), paramList.ToArray))
        End If

        '（締め日が存在しない日の場合、システム年月の末日に設定する）
        If strSeikyuuSimeDate.Equals(String.Empty) Then
            strSeikyuuSimeDate = Right(StrDup(2, "0") & Convert.ToDateTime(sysDate.Year & "/" & sysDate.Month & "/" & "01").AddMonths(1).AddDays(-1).Day.ToString, 2)
        End If

        Try
            '請求書発行日 = システム年月＋請求締め日 
            seikyuusyoHakDate = Convert.ToDateTime(sysDate.Year & "/" & sysDate.Month & "/" & strSeikyuuSimeDate)
        Catch ex As Exception
            seikyuusyoHakDate = Convert.ToDateTime(sysDate.Year & "/" & sysDate.Month & "/" & "01").AddMonths(1).AddDays(-1)
        End Try

        '求めた請求書発行日＜システム日付の場合
        If DateTime.Compare(seikyuusyoHakDate, Convert.ToDateTime(sysDate.ToString("yyyy/MM/dd"))) < 0 Then
            '画面.請求書発行日←求めた請求書発行日＋1ヶ月
            seikyuusyoHakDate = seikyuusyoHakDate.AddMonths(1)
        End If

        '戻り値
        Return seikyuusyoHakDate

    End Function

    ''' <summary>
    ''' 商品情報を取得する
    ''' </summary>
    ''' <param name="strSyouhinCd">商品コード</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/03/09 車龍 407584 追加</history>
    Public Function SelSyouhinInfo(ByVal strSyouhinCd As String) As Data.DataTable

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet
        ' SQL文の生成
        Dim sql As New StringBuilder
        ' パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        '標準価格
        Dim strHyoujunKkk As String = String.Empty

        ' SQL文
        With sql
            .AppendLine("SELECT ")
            .AppendLine("	MSH.syouhin_cd ") '--商品コード ")
            .AppendLine("	,MSH.hyoujun_kkk ") '--標準価格 ")
            .AppendLine("	,MSH.zei_kbn ") '--税区分 ")
            .AppendLine("	,MSZ.zeiritu ") '--税率 ")
            .AppendLine("FROM ")
            .AppendLine("	m_syouhin AS MSH WITH(READCOMMITTED) ") '--商品マスタ ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_syouhizei AS MSZ WITH(READCOMMITTED) ") '--消費税マスタ ")
            .AppendLine("	ON ")
            .AppendLine("		MSH.zei_kbn = MSZ.zei_kbn ")
            .AppendLine("WHERE ")
            .AppendLine("	syouhin_cd  = @syouhin_cd ") '--商品コード ")

        End With

        paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, strSyouhinCd))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, sql.ToString(), dsReturn, "dtSyouhinInfo", paramList.ToArray)

        '戻り値
        Return dsReturn.Tables("dtSyouhinInfo")

    End Function

    ''' <summary>
    ''' 工事売上種別名取得
    ''' </summary>
    ''' <param name="strKojUriSyubetu">工事売上種別</param>
    ''' <returns>工事売上種別名</returns>
    ''' <history>2013/03/13 車龍 407584 追加</history>
    Public Function GetKojUriSyubetuMei(ByVal strKojUriSyubetu As String) As Object
        Dim intCode As Integer
        Try
            intCode = CInt(strKojUriSyubetu)
            Dim strSql As String = "SELECT meisyou FROM m_meisyou  WITH(READCOMMITTED) WHERE meisyou_syubetu='55' and  code='" & strKojUriSyubetu & "'"
            Dim obj As Object = ExecuteScalar(connStr, CommandType.Text, strSql)

            If obj Is Nothing Then
                Return DBNull.Value
            Else
                Return obj.ToString
            End If
        Catch ex As Exception
            Return DBNull.Value
        End Try
    End Function

End Class
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports System.Text

''' <summary>
''' 汎用売上データ取込画面DataAccess
''' </summary>
''' <remarks>2012/01/12 陳琳 新規作成</remarks>
Public Class HanyouUriageDataAccess

    Private connStr As String = ConnectionManager.Instance.ConnectionString

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
            .AppendLine("	CONVERT(varchar(100),syori_datetime,111) + ' ' + CONVERT(varchar(100),syori_datetime,108) AS syori_datetime ")              '処理日時
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
            .AppendLine("	file_kbn = 6 ")         'ファイル区分
            .AppendLine(" ORDER BY ")
            .AppendLine("	syori_datetime DESC ")  '処理日時(降順)
        End With

        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsHanbaiKakaku, "dtUploadKanri")

        Return dsHanbaiKakaku.Tables("dtUploadKanri")

    End Function
    ''' <summary>商品コードを取得する</summary>
    ''' <history>2011/03/02　車龍(大連情報システム部)　新規作成</history>
    Public Function SelSyouhinCdInput(ByVal strSyouhinCd As String) As DataTable

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT  ")
            .AppendLine("	syouhin_mei ")
            .AppendLine("FROM  ")
            .AppendLine("	m_syouhin WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	syouhin_cd = @syouhin_cd ")
        End With

        paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, strSyouhinCd))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtSyouhinCd", paramList.ToArray)

        '戻り値
        Return dsReturn.Tables(0)
   

    End Function
    ''' <summary>調査会社名を取得する</summary>
    ''' <returns>調査会社名データテーブル</returns>
    ''' <history>2011/02/24　車龍(大連情報システム部)　新規作成</history>
    Public Function SelSeikyuuSakiInput(ByVal strCd As String, ByVal strBrc As String, ByVal strKbn As String) As Boolean

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT  ")

            .AppendLine("      seikyuu_saki_cd AS cd ")
            .AppendLine(" FROM ")
            .AppendLine("      m_seikyuu_saki WITH(READCOMMITTED) ")
            .AppendLine(" WHERE  ")
            .AppendLine("  seikyuu_saki_cd = @seikyuu_saki_cd ")
            .AppendLine(" AND seikyuu_saki_brc = @seikyuu_saki_brc ")
            .AppendLine(" AND seikyuu_saki_kbn = @seikyuu_saki_kbn ")
        End With
        'パラメータの設定
        paramList.Add(MakeParam("@seikyuu_saki_cd", SqlDbType.VarChar, 5, strCd))
        paramList.Add(MakeParam("@seikyuu_saki_brc", SqlDbType.VarChar, 2, strBrc))
        paramList.Add(MakeParam("@seikyuu_saki_kbn", SqlDbType.VarChar, 1, strKbn))
        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtSeikyuuSaki", paramList.ToArray)

        '戻り値
        If dsReturn.Tables(0).Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If

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
            .AppendLine("	file_kbn =6 ")                          'ファイル区分
        End With

        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsHanbaiKakaku, "dtUploadKanri")

        Return CInt(dsHanbaiKakaku.Tables("dtUploadKanri").Rows(0).Item("count").ToString)

    End Function

    ''' <summary>汎用売上マスタ情報テーブルを登録する</summary>
    ''' <param name="dtHanyouUriageOk">マスタデータ情報</param>
    ''' <returns>成功失敗区分</returns>
    Public Function InsHanyouUriage(ByVal dtHanyouUriageOk As Data.DataTable, _
                                    ByVal strUserId As String) As Boolean

        '戻り値
        Dim InsCount As Integer = 0

        Dim commandTextSb As New System.Text.StringBuilder
        Dim HanyouSiireDataAccess As New HanyouSiireDataAccess
        Dim strLoginMei As String = HanyouSiireDataAccess.SelLoginMei(strUserId)
        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine(" INSERT INTO  ")
            .AppendLine("   t_hannyou_uriage WITH(UPDLOCK) ")
            .AppendLine("   ( ")
            .AppendLine("		torikesi ")                            '取消
            .AppendLine("		,tekiyou ")                             '摘要
            .AppendLine("		,uri_date ")                            '売上年月日
            .AppendLine("		,denpyou_uri_date ")                    '伝票売上年月日
            .AppendLine("		,seikyuu_date ")                        '請求年月日
            .AppendLine("		,seikyuu_saki_cd ")                     '請求先コード
            .AppendLine("		,seikyuu_saki_brc ")                    '請求先枝番
            .AppendLine("		,seikyuu_saki_kbn ")                    '請求先区分
            .AppendLine("		,syouhin_cd ")                          '商品コード
            .AppendLine("		,hin_mei ")                             '品名
            .AppendLine("		,suu ")                                 '数量
            .AppendLine("		,tanka ")                               '単価
            .AppendLine("		,syanai_genka ")                        '社内原価
            .AppendLine("		,zei_kbn ")                             '税区分
            .AppendLine("		,syouhizei_gaku ")                      '消費税額
            .AppendLine("		,uri_keijyou_flg ")                     '売上処理FLG(売上計上FLG)	
            .AppendLine("		,uri_keijyou_date ")                    '売上処理日(売上計上日)
            .AppendLine("		,kbn ")                                 '区分	
            .AppendLine("		,bangou ")                              '番号	
            .AppendLine("		,sesyu_mei ")                           '施主名
            '==========↓2015/11/19 高兵兵 add売上店区分,売上店ｺｰﾄﾞ  修正↓======================
            .AppendLine("		,uriage_ten_kbn ")                      '売上店区分
            .AppendLine("		,uriage_ten_cd ")                       '売上店ｺｰﾄﾞ
            '==========↑2015/11/19 高兵兵 add売上店区分,売上店ｺｰﾄﾞ  修正↑======================
            .AppendLine("		,add_login_user_id ")                   '登録ログインユーザID
            .AppendLine("		,add_login_user_name ")                 '登録ログインユーザ名
            .AppendLine("		,add_datetime ")                        '登録日時
            .AppendLine("		,upd_login_user_id ")                   '更新ログインユーザID
            .AppendLine("		,upd_login_user_name ")                 '更新ログインユーザ名
            .AppendLine("		,upd_datetime ")                        '更新日時
            .AppendLine("	) ")
            .AppendLine("VALUES ")
            .AppendLine("( ")
            .AppendLine("		@torikesi ")
            .AppendLine("		,@tekiyou ")
            .AppendLine("		,@uri_date ")
            .AppendLine("		,@denpyou_uri_date ")
            .AppendLine("		,@seikyuu_date ")
            .AppendLine("		,@seikyuu_saki_cd ")
            .AppendLine("		,@seikyuu_saki_brc ")
            .AppendLine("		,@seikyuu_saki_kbn ")
            .AppendLine("		,@syouhin_cd ")
            .AppendLine("		,@hin_mei ")
            .AppendLine("		,@suu ")
            .AppendLine("		,@tanka ")
            .AppendLine("		,@syanai_genka ")
            .AppendLine("		,@zei_kbn ")
            .AppendLine("		,@syouhizei_gaku ")
            .AppendLine("		,@uri_keijyou_flg ")
            .AppendLine("		,@uri_keijyou_date ")
            .AppendLine("		,@kbn ")
            .AppendLine("		,@bangou ")
            .AppendLine("		,@sesyu_mei ")
            '==========↓2015/11/19 高兵兵 add売上店区分,売上店ｺｰﾄﾞ  修正↓======================
            .AppendLine("		,@uriage_ten_kbn ")
            .AppendLine("		,@uriage_ten_cd ")
            '==========↑2015/11/19 高兵兵 add売上店区分,売上店ｺｰﾄﾞ  修正↑======================
            .AppendLine("		,@add_login_user_id ")
            .AppendLine("	,@add_login_user_name ")
            .AppendLine("		,GETDATE() ")
            .AppendLine("		,NULL ")
            .AppendLine("		,NULL ")
            .AppendLine("		,NULL ")
            .AppendLine(") ")
        End With


        For i As Integer = 0 To dtHanyouUriageOk.Rows.Count - 1

            'パラメータの設定
            paramList.Clear()
            If dtHanyouUriageOk.Rows(i).Item("torikesi").ToString.Trim = "" Then
                paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, "0"))
            Else
                paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, dtHanyouUriageOk.Rows(i).Item("torikesi").ToString.Trim))
            End If

            paramList.Add(MakeParam("@tekiyou", SqlDbType.VarChar, 512, InsObj(dtHanyouUriageOk.Rows(i).Item("tekiyou").ToString.Trim)))
            paramList.Add(MakeParam("@uri_date", SqlDbType.DateTime, 30, InsObj(dtHanyouUriageOk.Rows(i).Item("uri_date").ToString.Trim)))
            paramList.Add(MakeParam("@denpyou_uri_date", SqlDbType.DateTime, 30, InsObj(dtHanyouUriageOk.Rows(i).Item("denpyou_uri_date").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_date", SqlDbType.DateTime, 30, InsObj(dtHanyouUriageOk.Rows(i).Item("seikyuu_date").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_cd", SqlDbType.VarChar, 40, InsObj(dtHanyouUriageOk.Rows(i).Item("seikyuu_saki_cd").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_brc", SqlDbType.VarChar, 5, InsObj(dtHanyouUriageOk.Rows(i).Item("seikyuu_saki_brc").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_kbn", SqlDbType.Char, 5, InsObj(dtHanyouUriageOk.Rows(i).Item("seikyuu_saki_kbn").ToString.Trim)))
            paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, InsObj(dtHanyouUriageOk.Rows(i).Item("syouhin_cd").ToString.Trim)))
            paramList.Add(MakeParam("@hin_mei", SqlDbType.VarChar, 40, InsObj(dtHanyouUriageOk.Rows(i).Item("hin_mei").ToString.Trim)))
            paramList.Add(MakeParam("@suu", SqlDbType.Int, 10, InsObj(dtHanyouUriageOk.Rows(i).Item("suu").ToString.Trim)))
            paramList.Add(MakeParam("@tanka", SqlDbType.Int, 10, InsObj(dtHanyouUriageOk.Rows(i).Item("tanka").ToString.Trim)))
            paramList.Add(MakeParam("@syanai_genka", SqlDbType.Int, 10, InsObj(dtHanyouUriageOk.Rows(i).Item("syanai_genka").ToString.Trim)))
            paramList.Add(MakeParam("@zei_kbn", SqlDbType.VarChar, 1, InsObj(dtHanyouUriageOk.Rows(i).Item("zei_kbn").ToString.Trim)))
            paramList.Add(MakeParam("@syouhizei_gaku", SqlDbType.Int, 10, InsObj(dtHanyouUriageOk.Rows(i).Item("syouhizei_gaku").ToString.Trim)))
            paramList.Add(MakeParam("@uri_keijyou_flg", SqlDbType.Int, 10, InsObj(dtHanyouUriageOk.Rows(i).Item("uri_keijyou_flg").ToString.Trim)))
            paramList.Add(MakeParam("@uri_keijyou_date", SqlDbType.DateTime, 30, InsObj(dtHanyouUriageOk.Rows(i).Item("uri_keijyou_date").ToString.Trim)))
            paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, InsObj(dtHanyouUriageOk.Rows(i).Item("kbn").ToString.Trim)))
            paramList.Add(MakeParam("@bangou", SqlDbType.VarChar, 10, InsObj(dtHanyouUriageOk.Rows(i).Item("bangou").ToString.Trim)))
            paramList.Add(MakeParam("@sesyu_mei", SqlDbType.VarChar, 50, InsObj(dtHanyouUriageOk.Rows(i).Item("sesyu_mei").ToString.Trim)))
            '==========↓2015/11/19 高兵兵 add売上店区分,売上店ｺｰﾄﾞ  修正↓======================
            paramList.Add(MakeParam("@uriage_ten_kbn", SqlDbType.Char, 1, InsObj(dtHanyouUriageOk.Rows(i).Item("uriage_ten_kbn").ToString.Trim)))
            paramList.Add(MakeParam("@uriage_ten_cd", SqlDbType.VarChar, 7, InsObj(dtHanyouUriageOk.Rows(i).Item("uriage_ten_cd").ToString.Trim)))
            '==========↑2015/11/19 高兵兵 add売上店区分,売上店ｺｰﾄﾞ  修正↑======================
            paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, strUserId))
            paramList.Add(MakeParam("@add_login_user_name", SqlDbType.VarChar, 128, strLoginMei))
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


    ''' <summary>汎用売上エラー情報テーブルを登録する</summary>
    ''' <param name="dtHanyouUriageErr">エラーデータ情報</param>
    ''' <returns>成功失敗区分</returns>
    Public Function InsHanyouUriageErr(ByVal dtHanyouUriageErr As Data.DataTable, _
                                       ByVal strUploadDate As String, _
                                       ByVal strUserId As String) As Boolean

        '戻り値
        Dim InsCount As Integer = 0

        Dim commandTextSb As New System.Text.StringBuilder
        Dim HanyouSiireDataAccess As New HanyouSiireDataAccess
        Dim strLoginMei As String = HanyouSiireDataAccess.SelLoginMei(strUserId)
        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine(" INSERT INTO  ")
            .AppendLine("   t_hannyou_uriage_error WITH(UPDLOCK) ")
            .AppendLine("   ( ")
            .AppendLine("		edi_jouhou_sakusei_date ")              'EDI情報作成日
            .AppendLine("		,gyou_no ")                             '行NO
            .AppendLine("		,syori_datetime ")                      '処理日時
            .AppendLine("		,torikesi ")                            '取消
            .AppendLine("		,seikyuu_saki_kbn ")                    '請求先区分
            .AppendLine("		,seikyuu_saki_cd ")                     '請求先コード
            .AppendLine("		,seikyuu_saki_brc ")                    '請求先枝番
            .AppendLine("		,syouhin_cd ")                          '商品コード
            .AppendLine("		,hin_mei ")                             '品名
            .AppendLine("		,suu ")                                 '数量
            .AppendLine("		,tanka ")                               '単価
            .AppendLine("		,syanai_genka ")                        '社内原価
            .AppendLine("		,zei_kbn ")                             '税区分
            .AppendLine("		,syouhizei_gaku ")                      '消費税額
            .AppendLine("		,uri_date ")                            '売上年月日
            .AppendLine("		,seikyuu_date ")                        '請求年月日
            .AppendLine("		,denpyou_uri_date ")                    '伝票売上年月日
            .AppendLine("		,uri_keijyou_flg ")                     '売上処理FLG(売上計上FLG)	
            .AppendLine("		,uri_keijyou_date ")                    '売上処理日(売上計上日)
            .AppendLine("		,kbn ")                                 '区分	
            .AppendLine("		,bangou ")                              '番号	
            .AppendLine("		,sesyu_mei ")                           '施主名
            '==========↓2015/11/19 高兵兵 add売上店区分,売上店ｺｰﾄﾞ  修正↓======================
            .AppendLine("		,uriage_ten_kbn ")                      '売上店区分
            .AppendLine("		,uriage_ten_cd ")                       '売上店ｺｰﾄﾞ
            '==========↑2015/11/19 高兵兵 add売上店区分,売上店ｺｰﾄﾞ  修正↑======================
            .AppendLine("		,tekiyou ")                             '摘要
            .AppendLine("		,add_login_user_id ")                   '登録ログインユーザID
            .AppendLine("		,add_login_user_name ")                 '登録ログインユーザ名
            .AppendLine("		,add_datetime ")                        '登録日時
            .AppendLine("		,upd_login_user_id ")                   '更新ログインユーザID
            .AppendLine("		,upd_login_user_name ")                 '更新ログインユーザ名
            .AppendLine("		,upd_datetime ")                        '更新日時
            .AppendLine("	) ")
            .AppendLine("VALUES ")
            .AppendLine("( ")
            .AppendLine("		@edi_jouhou_sakusei_date ")
            .AppendLine("		,@gyou_no ")
            .AppendLine("		,@syori_datetime ")
            .AppendLine("		,@torikesi ")
            .AppendLine("		,@seikyuu_saki_kbn ")
            .AppendLine("		,@seikyuu_saki_cd ")
            .AppendLine("		,@seikyuu_saki_brc ")
            .AppendLine("		,@syouhin_cd ")
            .AppendLine("		,@hin_mei ")
            .AppendLine("		,@suu ")
            .AppendLine("		,@tanka ")
            .AppendLine("		,@syanai_genka ")
            .AppendLine("		,@zei_kbn ")
            .AppendLine("		,@syouhizei_gaku ")
            .AppendLine("		,@uri_date ")
            .AppendLine("		,@seikyuu_date ")
            .AppendLine("		,@denpyou_uri_date ")
            .AppendLine("		,@uri_keijyou_flg ")
            .AppendLine("		,@uri_keijyou_date")
            .AppendLine("		,@kbn ")
            .AppendLine("		,@bangou ")
            .AppendLine("		,@sesyu_mei ")
            '==========↓2015/11/19 高兵兵 add売上店区分,売上店ｺｰﾄﾞ  修正↓======================
            .AppendLine("		,@uriage_ten_kbn ")
            .AppendLine("		,@uriage_ten_cd ")
            '==========↑2015/11/19 高兵兵 add売上店区分,売上店ｺｰﾄﾞ  修正↑======================
            .AppendLine("		,@tekiyou ")
            .AppendLine("		,@add_login_user_id ")
            .AppendLine("	,@add_login_user_name ")
            .AppendLine("		,GETDATE() ")
            .AppendLine("		,NULL ")
            .AppendLine("		,NULL ")
            .AppendLine("		,NULL ")
            .AppendLine(") ")
        End With

        For i As Integer = 0 To dtHanyouUriageErr.Rows.Count - 1
            'パラメータの設定
            paramList.Clear()
            paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, dtHanyouUriageErr.Rows(i).Item("edi_jouhou_sakusei_date").ToString.Trim))
            paramList.Add(MakeParam("@gyou_no", SqlDbType.VarChar, 10, dtHanyouUriageErr.Rows(i).Item("gyou_no").ToString.Trim))
            paramList.Add(MakeParam("@syori_datetime", SqlDbType.VarChar, 30, strUploadDate))

            paramList.Add(MakeParam("@torikesi", SqlDbType.VarChar, 10, InsObj(dtHanyouUriageErr.Rows(i).Item("torikesi").ToString.Trim)))
            paramList.Add(MakeParam("@tekiyou", SqlDbType.VarChar, 512, InsObj(dtHanyouUriageErr.Rows(i).Item("tekiyou").ToString.Trim)))
            paramList.Add(MakeParam("@uri_date", SqlDbType.VarChar, 30, InsObj(dtHanyouUriageErr.Rows(i).Item("uri_date").ToString.Trim)))
            paramList.Add(MakeParam("@denpyou_uri_date", SqlDbType.VarChar, 30, InsObj(dtHanyouUriageErr.Rows(i).Item("denpyou_uri_date").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_date", SqlDbType.VarChar, 30, InsObj(dtHanyouUriageErr.Rows(i).Item("seikyuu_date").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_cd", SqlDbType.VarChar, 40, InsObj(dtHanyouUriageErr.Rows(i).Item("seikyuu_saki_cd").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_brc", SqlDbType.VarChar, 5, InsObj(dtHanyouUriageErr.Rows(i).Item("seikyuu_saki_brc").ToString.Trim)))
            paramList.Add(MakeParam("@seikyuu_saki_kbn", SqlDbType.Char, 5, InsObj(dtHanyouUriageErr.Rows(i).Item("seikyuu_saki_kbn").ToString.Trim)))
            paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, InsObj(dtHanyouUriageErr.Rows(i).Item("syouhin_cd").ToString.Trim)))
            paramList.Add(MakeParam("@hin_mei", SqlDbType.VarChar, 40, InsObj(dtHanyouUriageErr.Rows(i).Item("hin_mei").ToString.Trim)))
            paramList.Add(MakeParam("@suu", SqlDbType.VarChar, 10, InsObj(dtHanyouUriageErr.Rows(i).Item("suu").ToString.Trim)))
            paramList.Add(MakeParam("@tanka", SqlDbType.VarChar, 10, InsObj(dtHanyouUriageErr.Rows(i).Item("tanka").ToString.Trim)))
            paramList.Add(MakeParam("@syanai_genka", SqlDbType.VarChar, 10, InsObj(dtHanyouUriageErr.Rows(i).Item("syanai_genka").ToString.Trim)))
            paramList.Add(MakeParam("@zei_kbn", SqlDbType.VarChar, 1, InsObj(dtHanyouUriageErr.Rows(i).Item("zei_kbn").ToString.Trim)))
            paramList.Add(MakeParam("@syouhizei_gaku", SqlDbType.VarChar, 10, InsObj(dtHanyouUriageErr.Rows(i).Item("syouhizei_gaku").ToString.Trim)))
            paramList.Add(MakeParam("@uri_keijyou_flg", SqlDbType.VarChar, 10, InsObj(dtHanyouUriageErr.Rows(i).Item("uri_keijyou_flg").ToString.Trim)))
            paramList.Add(MakeParam("@uri_keijyou_date", SqlDbType.VarChar, 30, InsObj(dtHanyouUriageErr.Rows(i).Item("uri_keijyou_date").ToString.Trim)))
            paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, InsObj(dtHanyouUriageErr.Rows(i).Item("kbn").ToString.Trim)))
            paramList.Add(MakeParam("@bangou", SqlDbType.VarChar, 10, InsObj(dtHanyouUriageErr.Rows(i).Item("bangou").ToString.Trim)))
            paramList.Add(MakeParam("@sesyu_mei", SqlDbType.VarChar, 50, InsObj(dtHanyouUriageErr.Rows(i).Item("sesyu_mei").ToString.Trim)))
            '==========↓2015/11/19 高兵兵 add売上店区分,売上店ｺｰﾄﾞ  修正↓======================
            paramList.Add(MakeParam("@uriage_ten_kbn", SqlDbType.Char, 1, InsObj(dtHanyouUriageErr.Rows(i).Item("uriage_ten_kbn").ToString.Trim)))
            paramList.Add(MakeParam("@uriage_ten_cd", SqlDbType.VarChar, 7, InsObj(dtHanyouUriageErr.Rows(i).Item("uriage_ten_cd").ToString.Trim)))
            '==========↑2015/11/19 高兵兵 add売上店区分,売上店ｺｰﾄﾞ  修正↑======================
            paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, strUserId))
            paramList.Add(MakeParam("@add_login_user_name", SqlDbType.VarChar, 128, strLoginMei))
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
    Function InsObj(ByVal str As String) As Object
        If str = "" Then
            Return DBNull.Value
        Else
            Return str
        End If
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
            .AppendLine("	,6 ")
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

    ''' <summary>汎用売上エラー情報を取得する</summary>
    ''' <param name="strEdiJouhouSakuseiDate">EDI情報作成日</param>
    ''' <param name="strSyoriDate">処理日時</param>
    ''' <param name="intTopCount">取得の最大行数</param>
    ''' <returns>汎用売上エラーデータテーブル</returns>
    Public Function SelHanyouUriageErr(ByVal strEdiJouhouSakuseiDate As String, _
                                       ByVal strSyoridate As String, _
                                       ByVal intTopCount As Integer) As DataTable
        'DataSetインスタンスの生成
        Dim dsHanyouUriageErr As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine(" SELECT ")
            .AppendLine("   TOP " & intTopCount & "")
            .AppendLine("		edi_jouhou_sakusei_date AS edi_jouhou_sakusei_date ")           'EDI情報作成日
            .AppendLine("		,gyou_no AS gyou_no ")                                          '行NO
            .AppendLine("		,syori_datetime AS syori_datetime ")                            '処理日時
            .AppendLine("		,torikesi AS torikesi  ")                                        '取消
            '==========↓2015/11/19 高兵兵 add売上店区分,売上店ｺｰﾄﾞ  修正↓========================================
            .AppendLine("		,uriage_ten_kbn AS uriage_ten_kbn  ")                           '売上店区分
            .AppendLine("		,uriage_ten_cd AS uriage_ten_cd  ")                             '売上店コード
            '==========↑2015/11/19 高兵兵 add売上店区分,売上店ｺｰﾄﾞ  修正↑========================================
            .AppendLine("		,seikyuu_saki_kbn AS seikyuu_saki_kbn ")                        '請求先区分
            .AppendLine("		,seikyuu_saki_cd AS seikyuu_saki_cd ")                          '請求先コード
            .AppendLine("		,seikyuu_saki_brc AS seikyuu_saki_brc ")                        '請求先枝番
            .AppendLine("		,syouhin_cd AS syouhin_cd ")                                    '商品コード
            .AppendLine("		,hin_mei AS hin_mei ")                                          '品名
            .AppendLine("		,suu AS suu ")                                                  '数量
            .AppendLine("		,tanka AS tanka ")                                              '単価
            .AppendLine("		,syanai_genka AS syanai_genka ")                                '社内原価
            .AppendLine("		,zei_kbn AS zei_kbn ")                                          '税区分
            .AppendLine("		,syouhizei_gaku AS syouhizei_gaku ")                            '消費税額
            .AppendLine("		,uri_date AS uri_date ")                                        '売上年月日
            .AppendLine("		,seikyuu_date AS seikyuu_date ")                                '請求年月日
            .AppendLine("		,denpyou_uri_date AS denpyou_uri_date ")                        '伝票売上年月日
            .AppendLine("		,uri_keijyou_flg AS uri_keijyou_flg ")                          '売上処理FLG(売上計上FLG)	
            .AppendLine("		,uri_keijyou_date AS uri_keijyou_date ")                        '売上処理日(売上計上日)
            .AppendLine("		,kbn AS kbn ")                                                  '区分	
            .AppendLine("		,bangou AS bangou ")                                            '番号	
            .AppendLine("		,sesyu_mei AS sesyu_mei ")                                      '施主名
            .AppendLine("		,tekiyou AS tekiyou")                                           '摘要
            .AppendLine("		,add_login_user_id AS add_login_user_id ")                      '登録ログインユーザID
            .AppendLine("		,add_login_user_name AS add_login_user_name ")                  '登録ログインユーザ名
            .AppendLine("		,add_datetime AS add_datetime ")                                '登録日時
            .AppendLine("		,upd_login_user_id AS upd_login_user_id ")                      '更新ログインユーザID
            .AppendLine("		,upd_login_user_name AS upd_login_user_name ")                  '更新ログインユーザ名
            .AppendLine("		,upd_datetime AS upd_datetime ")                                '更新日時
            .AppendLine(" FROM ")
            .AppendLine("   t_hannyou_uriage_error AS THUE WITH(READCOMMITTED) ")
            .AppendLine(" WHERE ")
            .AppendLine("    ")

            'EDI情報作成日
            .AppendLine(" THUE.edi_jouhou_sakusei_date = @edi_jouhou_sakusei_date ")
            paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, strEdiJouhouSakuseiDate.Trim))

            '処理日時
            .AppendLine(" AND CONVERT(varchar(100),THUE.syori_datetime,112) + REPLACE(CONVERT(varchar(100),THUE.syori_datetime,114),':','') = @syori_datetime")

            paramList.Add(MakeParam("@syori_datetime", SqlDbType.VarChar, 40, strSyoridate))

            'ソート順
            .AppendLine(" ORDER BY ")
            .AppendLine("      THUE.edi_jouhou_sakusei_date, ")
            .AppendLine("      THUE.gyou_no ")
        End With

        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsHanyouUriageErr, _
                    "dsHanyouUriageErr", paramList.ToArray)

        Return dsHanyouUriageErr.Tables("dsHanyouUriageErr")

    End Function

    ''' <summary>汎用売上エラー件数を取得する</summary>
    ''' <param name="strEdiJouhouSakuseiDate">EDI情報作成日</param>
    ''' <param name="strSyoriDate">処理日時</param>
    ''' <returns>汎用売上エラー件数</returns>
    Public Function SelHanyouUriageErrCount(ByVal strEdiJouhouSakuseiDate As String, ByVal strSyoriDate As String) As Integer

        'DataSetインスタンスの生成
        Dim dsHanyouUriageErr As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine(" SELECT ")
            .AppendLine("   Count(edi_jouhou_sakusei_date) AS count ")
            .AppendLine(" FROM ")
            .AppendLine("   t_hannyou_uriage_error WITH(READCOMMITTED) ")   '汎用売上エラー情報T
            .AppendLine(" WHERE ")

            'EDI情報
            .AppendLine(" edi_jouhou_sakusei_date = @edi_jouhou_sakusei_date ")
            paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, strEdiJouhouSakuseiDate.Trim))

            '処理日時
            .AppendLine(" AND CONVERT(varchar(100),syori_datetime,112) + REPLACE(CONVERT(varchar(100),syori_datetime,114),':','') = @syori_datetime")
            '.AppendLine(" AND syori_datetime = @syori_datetime")
            paramList.Add(MakeParam("@syori_datetime", SqlDbType.VarChar, 40, strSyoriDate))

        End With
        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsHanyouUriageErr, _
                    "dsHanyouUriageErr", paramList.ToArray)

        Return dsHanyouUriageErr.Tables("dsHanyouUriageErr").Rows(0).Item("count")

    End Function

End Class

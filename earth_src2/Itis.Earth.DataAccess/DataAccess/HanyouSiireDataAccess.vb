Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports System.Text

''' <summary>
''' 汎用仕入データ取込画面DataAccess
''' </summary>
''' <remarks>2012/01/11 陳琳 新規作成</remarks>
Public Class HanyouSiireDataAccess

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
            .AppendLine("	file_kbn = 7 ")         'ファイル区分
            .AppendLine(" ORDER BY ")
            .AppendLine("	syori_datetime DESC ")  '処理日時(降順)
        End With

        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsHanbaiKakaku, "dtUploadKanri")

        Return dsHanbaiKakaku.Tables("dtUploadKanri")

    End Function
    Public Function SelSesyuMei(ByVal kbn As String, ByVal strHosyousyoNo As String) As String

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT  ")
            .AppendLine("      sesyu_mei ")
            .AppendLine(" FROM ")
            .AppendLine("      t_jiban WITH(READCOMMITTED) ")
            .AppendLine(" WHERE  ")
            .AppendLine("  kbn = @kbn ")
            .AppendLine(" AND hosyousyo_no = @hosyousyo_no ")
        End With
        'パラメータの設定
        paramList.Add(MakeParam("@kbn", SqlDbType.VarChar, 1, kbn))
        paramList.Add(MakeParam("@hosyousyo_no", SqlDbType.VarChar, 10, strHosyousyoNo))
        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtJiban", paramList.ToArray)

        '戻り値
        If dsReturn.Tables(0).Rows.Count = 0 Then
            Return ""
        Else
            Return dsReturn.Tables(0).Rows(0).Item(0).ToString
        End If


    End Function
    Public Function SelLoginMei(ByVal strUserId As String) As String

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT  ")

            .AppendLine("      DisplayName AS cd ")
            .AppendLine(" FROM ")
            .AppendLine("      m_jhs_mailbox WITH(READCOMMITTED) ")
            .AppendLine(" WHERE  ")
            .AppendLine("  PrimaryWindowsNTAccount = @PrimaryWindowsNTAccount ")

        End With
        'パラメータの設定
        paramList.Add(MakeParam("@PrimaryWindowsNTAccount", SqlDbType.VarChar, 64, strUserId))
        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtKameiten", paramList.ToArray)

        '戻り値
        Return dsReturn.Tables(0).Rows(0).Item(0).ToString

    End Function
    ''' <summary>調査会社名を取得する</summary>
    ''' <returns>調査会社名データテーブル</returns>
    ''' <history>2011/02/24　車龍(大連情報システム部)　新規作成</history>
    Public Function SelKameitenInput(ByVal strCd As String) As Boolean

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT  ")

            .AppendLine("      kameiten_cd AS cd ")
            .AppendLine(" FROM ")
            .AppendLine("      m_kameiten WITH(READCOMMITTED) ")
            .AppendLine(" WHERE  ")
            .AppendLine("  kameiten_cd = @kameiten_cd ")

        End With
        'パラメータの設定
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strCd))
        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtKameiten", paramList.ToArray)

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
            .AppendLine("	file_kbn =7 ")                          'ファイル区分
        End With

        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsHanbaiKakaku, "dtUploadKanri")

        Return CInt(dsHanbaiKakaku.Tables("dtUploadKanri").Rows(0).Item("count").ToString)

    End Function
    ''' <summary>アップロード管理の件数を取得する</summary>
    ''' <returns>アップロード管理の件数</returns>
    Public Function SelUploadKanri(ByVal strKbn As String, ByVal strFileName As String) As Integer

        'DataSetインスタンスの生成
        Dim dsHanbaiKakaku As New Data.DataSet
        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'SQL文
        With commandTextSb
            .AppendLine(" SELECT ")
            .AppendLine("	COUNT(*) AS count ")                    '件数
            .AppendLine(" FROM ")
            .AppendLine("	t_upload_kanri WITH(READCOMMITTED) ")   'アップロード管理テーブル
            .AppendLine(" WHERE ")
            .AppendLine("	file_kbn =@file_kbn AND error_umu=0 AND nyuuryoku_file_mei=@nyuuryoku_file_mei ")                          'ファイル区分
        End With
        'パラメータの設定
        paramList.Add(MakeParam("@file_kbn", SqlDbType.VarChar, 1, strKbn))
        paramList.Add(MakeParam("@nyuuryoku_file_mei", SqlDbType.VarChar, 128, strFileName))
        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsHanbaiKakaku, "dtUploadKanri", paramList.ToArray)

        Return CInt(dsHanbaiKakaku.Tables("dtUploadKanri").Rows(0).Item("count").ToString)

    End Function
    ''' <summary>汎用仕入マスタ情報テーブルを登録する</summary>
    ''' <param name="dtHanyouSiireOk">マスタデータ情報</param>
    ''' <returns>成功失敗区分</returns>
    Public Function InsHanyouSiire(ByVal dtHanyouSiireOk As Data.DataTable, _
                                   ByVal strUserId As String) As Boolean

        '戻り値
        Dim InsCount As Integer = 0
        Dim strLoginMei As String = SelLoginMei(strUserId)
        Dim commandTextSb As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine(" INSERT INTO  ")
            .AppendLine("   t_hannyou_siire WITH(UPDLOCK) ")
            .AppendLine("   ( ")
            .AppendLine("		torikesi ")                            '取消
            .AppendLine("		,tekiyou ")                             '摘要
            .AppendLine("		,siire_date ")                          '仕入年月日
            .AppendLine("		,denpyou_siire_date ")                  '伝票仕入年月日
            .AppendLine("		,tys_kaisya_cd ")                       '調査会社コード
            .AppendLine("		,tys_kaisya_jigyousyo_cd ")             '調査会社事業所コード
            .AppendLine("		,kameiten_cd ")                         '加盟店コード
            .AppendLine("		,syouhin_cd ")                          '商品コード
            .AppendLine("		,suu ")                                 '数量
            .AppendLine("		,tanka ")                               '単価
            .AppendLine("		,zei_kbn ")                             '税区分
            .AppendLine("		,syouhizei_gaku ")                      '消費税額
            .AppendLine("		,siire_keijyou_flg ")                   '仕入処理FLG	
            .AppendLine("		,siire_keijyou_date ")                  '仕入処理日
            .AppendLine("		,kbn ")                                 '区分	
            .AppendLine("		,bangou ")                              '番号	
            .AppendLine("		,sesyu_mei ")                           '施主名
            .AppendLine("		,add_login_user_id ")                   '登録ログインユーザID
            .AppendLine("		,add_login_user_name ")                 '登録ログインユーザ名
            .AppendLine("		,add_datetime ")                        '登録日時
            .AppendLine("		,upd_login_user_id ")                   '更新ログインユーザID
            .AppendLine("		,upd_login_user_name ")                 '更新ログインユーザ名
            .AppendLine("		,upd_datetime ")                        '更新日時
            .AppendLine("	) ")
            .AppendLine("VALUES ")
            .AppendLine("( ")
            .AppendLine("	@torikesi ")
            .AppendLine("	,@tekiyou ")
            .AppendLine("	,@siire_date ")
            .AppendLine("	,@denpyou_siire_date ")
            .AppendLine("	,@tys_kaisya_cd ")
            .AppendLine("	,@tys_kaisya_jigyousyo_cd ")
            .AppendLine("	,@kameiten_cd ")
            .AppendLine("	,@syouhin_cd ")
            .AppendLine("	,@suu ")
            .AppendLine("	,@tanka ")
            .AppendLine("	,@zei_kbn ")
            .AppendLine("	,@syouhizei_gaku ")
            .AppendLine("	,@siire_keijyou_flg ")
            .AppendLine("	,@siire_keijyou_date ")
            .AppendLine("	,@kbn ")
            .AppendLine("	,@bangou ")
            .AppendLine("	,@sesyu_mei ")
            .AppendLine("	,@add_login_user_id ")
            .AppendLine("	,@add_login_user_name ")
            .AppendLine("	,GETDATE() ")
            .AppendLine("	,NULL ")
            .AppendLine("	,NULL ")
            .AppendLine("	,NULL ")
            .AppendLine(") ")
        End With

 
        For i As Integer = 0 To dtHanyouSiireOk.Rows.Count - 1

            'パラメータの設定
            paramList.Clear()
            If dtHanyouSiireOk.Rows(i).Item("torikesi").ToString.Trim = "" Then
                paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, "0"))
            Else
                paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, dtHanyouSiireOk.Rows(i).Item("torikesi").ToString.Trim))
            End If

            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, InsObj(dtHanyouSiireOk.Rows(i).Item("kameiten_cd").ToString.Trim)))
            paramList.Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 5, InsObj(dtHanyouSiireOk.Rows(i).Item("tys_kaisya_cd").ToString.Trim)))
            paramList.Add(MakeParam("@tys_kaisya_jigyousyo_cd", SqlDbType.VarChar, 2, InsObj(dtHanyouSiireOk.Rows(i).Item("tys_kaisya_jigyousyo_cd").ToString.Trim)))
            paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, InsObj(dtHanyouSiireOk.Rows(i).Item("syouhin_cd").ToString.Trim)))
            paramList.Add(MakeParam("@suu", SqlDbType.Int, 10, InsObj(dtHanyouSiireOk.Rows(i).Item("suu").ToString.Trim)))
            paramList.Add(MakeParam("@tanka", SqlDbType.Int, 10, InsObj(dtHanyouSiireOk.Rows(i).Item("tanka").ToString.Trim)))
            paramList.Add(MakeParam("@zei_kbn", SqlDbType.VarChar, 1, InsObj(dtHanyouSiireOk.Rows(i).Item("zei_kbn").ToString.Trim)))
            paramList.Add(MakeParam("@syouhizei_gaku", SqlDbType.Int, 10, InsObj(dtHanyouSiireOk.Rows(i).Item("syouhizei_gaku").ToString.Trim)))
            paramList.Add(MakeParam("@siire_date", SqlDbType.DateTime, 30, InsObj(dtHanyouSiireOk.Rows(i).Item("siire_date").ToString.Trim)))
            paramList.Add(MakeParam("@denpyou_siire_date", SqlDbType.DateTime, 30, InsObj(dtHanyouSiireOk.Rows(i).Item("denpyou_siire_date").ToString.Trim)))
            paramList.Add(MakeParam("@siire_keijyou_flg", SqlDbType.Int, 10, InsObj(dtHanyouSiireOk.Rows(i).Item("siire_keijyou_flg").ToString.Trim)))
            paramList.Add(MakeParam("@siire_keijyou_date", SqlDbType.DateTime, 30, InsObj(dtHanyouSiireOk.Rows(i).Item("siire_keijyou_date").ToString.Trim)))
            paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, InsObj(dtHanyouSiireOk.Rows(i).Item("kbn").ToString.Trim)))
            paramList.Add(MakeParam("@bangou", SqlDbType.VarChar, 10, InsObj(dtHanyouSiireOk.Rows(i).Item("bangou").ToString.Trim)))
            paramList.Add(MakeParam("@sesyu_mei", SqlDbType.VarChar, 50, InsObj(dtHanyouSiireOk.Rows(i).Item("sesyu_mei").ToString.Trim)))
            paramList.Add(MakeParam("@tekiyou", SqlDbType.VarChar, 512, InsObj(dtHanyouSiireOk.Rows(i).Item("tekiyou").ToString.Trim)))
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


    ''' <summary>汎用仕入エラー情報テーブルを登録する</summary>
    ''' <param name="dtHanyouSiireErr">エラーデータ情報</param>
    ''' <returns>成功失敗区分</returns>
    Public Function InsHanyouSiireErr(ByVal dtHanyouSiireErr As Data.DataTable, _
                                      ByVal strUploadDate As String, _
                                      ByVal strUserId As String) As Boolean

        '戻り値
        Dim InsCount As Integer = 0

        Dim commandTextSb As New System.Text.StringBuilder
        Dim strLoginMei As String = SelLoginMei(strUserId)
        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine(" INSERT INTO  ")
            .AppendLine("   t_hannyou_siire_error WITH(UPDLOCK) ")
            .AppendLine("   ( ")
            .AppendLine("		edi_jouhou_sakusei_date ")              'EDI情報作成日
            .AppendLine("		,gyou_no ")                             '行NO
            .AppendLine("		,syori_datetime ")                      '処理日時
            .AppendLine("		,torikesi ")                            '取消
            .AppendLine("		,kameiten_cd ")                         '加盟店コード
            .AppendLine("		,tys_kaisya_cd ")                       '調査会社コード
            .AppendLine("		,tys_kaisya_jigyousyo_cd ")             '調査会社事業所コード
            .AppendLine("		,syouhin_cd ")                          '商品コード
            .AppendLine("		,suu ")                                 '数量
            .AppendLine("		,tanka ")                               '単価
            .AppendLine("		,zei_kbn ")                             '税区分	
            .AppendLine("		,syouhizei_gaku ")                      '消費税額
            .AppendLine("		,siire_date ")                          '仕入年月日	
            .AppendLine("		,denpyou_siire_date ")                  '伝票仕入年月日	
            .AppendLine("		,siire_keijyou_flg ")                   '仕入処理FLG	
            .AppendLine("		,siire_keijyou_date ")                  '仕入処理日	
            .AppendLine("		,kbn ")                                 '区分	
            .AppendLine("		,bangou ")                              '番号	
            .AppendLine("		,sesyu_mei ")                           '施主名	
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
            .AppendLine("	@edi_jouhou_sakusei_date ")
            .AppendLine("	,@gyou_no ")
            .AppendLine("	,@syori_datetime ")
            .AppendLine("	,@torikesi ")
            .AppendLine("	,@kameiten_cd ")
            .AppendLine("	,@tys_kaisya_cd ")
            .AppendLine("	,@tys_kaisya_jigyousyo_cd ")
            .AppendLine("	,@syouhin_cd ")
            .AppendLine("	,@suu ")
            .AppendLine("	,@tanka ")
            .AppendLine("	,@zei_kbn ")
            .AppendLine("	,@syouhizei_gaku ")
            .AppendLine("	,@siire_date ")
            .AppendLine("	,@denpyou_siire_date ")
            .AppendLine("	,@siire_keijyou_flg ")
            .AppendLine("	,@siire_keijyou_date")
            .AppendLine("	,@kbn ")
            .AppendLine("	,@bangou ")
            .AppendLine("	,@sesyu_mei ")
            .AppendLine("	,@tekiyou ")
            .AppendLine("	,@add_login_user_id ")
            .AppendLine("	,@add_login_user_name ")
            .AppendLine("	,GETDATE() ")
            .AppendLine("	,NULL ")
            .AppendLine("	,NULL ")
            .AppendLine("	,NULL ")
            .AppendLine(") ")
        End With

        For i As Integer = 0 To dtHanyouSiireErr.Rows.Count - 1

            'パラメータの設定
            paramList.Clear()
            paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, dtHanyouSiireErr.Rows(i).Item("edi_jouhou_sakusei_date").ToString.Trim))
            paramList.Add(MakeParam("@gyou_no", SqlDbType.VarChar, 10, dtHanyouSiireErr.Rows(i).Item("gyou_no").ToString.Trim))
            paramList.Add(MakeParam("@syori_datetime", SqlDbType.VarChar, 30, strUploadDate))
         
            paramList.Add(MakeParam("@torikesi", SqlDbType.VarChar, 10, InsObj(dtHanyouSiireErr.Rows(i).Item("torikesi").ToString.Trim)))


            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, InsObj(dtHanyouSiireErr.Rows(i).Item("kameiten_cd").ToString.Trim)))
            paramList.Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 5, InsObj(dtHanyouSiireErr.Rows(i).Item("tys_kaisya_cd").ToString.Trim)))
            paramList.Add(MakeParam("@tys_kaisya_jigyousyo_cd", SqlDbType.VarChar, 2, InsObj(dtHanyouSiireErr.Rows(i).Item("tys_kaisya_jigyousyo_cd").ToString.Trim)))
            paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, InsObj(dtHanyouSiireErr.Rows(i).Item("syouhin_cd").ToString.Trim)))
            paramList.Add(MakeParam("@suu", SqlDbType.VarChar, 10, InsObj(dtHanyouSiireErr.Rows(i).Item("suu").ToString.Trim)))
            paramList.Add(MakeParam("@tanka", SqlDbType.VarChar, 10, InsObj(dtHanyouSiireErr.Rows(i).Item("tanka").ToString.Trim)))
            paramList.Add(MakeParam("@zei_kbn", SqlDbType.VarChar, 1, InsObj(dtHanyouSiireErr.Rows(i).Item("zei_kbn").ToString.Trim)))
            paramList.Add(MakeParam("@syouhizei_gaku", SqlDbType.VarChar, 10, InsObj(dtHanyouSiireErr.Rows(i).Item("syouhizei_gaku").ToString.Trim)))
            paramList.Add(MakeParam("@siire_date", SqlDbType.VarChar, 30, InsObj(dtHanyouSiireErr.Rows(i).Item("siire_date").ToString.Trim)))
            paramList.Add(MakeParam("@denpyou_siire_date", SqlDbType.VarChar, 30, InsObj(dtHanyouSiireErr.Rows(i).Item("denpyou_siire_date").ToString.Trim)))
            paramList.Add(MakeParam("@siire_keijyou_flg", SqlDbType.VarChar, 10, InsObj(dtHanyouSiireErr.Rows(i).Item("siire_keijyou_flg").ToString.Trim)))
            paramList.Add(MakeParam("@siire_keijyou_date", SqlDbType.VarChar, 30, InsObj(dtHanyouSiireErr.Rows(i).Item("siire_keijyou_date").ToString.Trim)))
            paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, InsObj(dtHanyouSiireErr.Rows(i).Item("kbn").ToString.Trim)))
            paramList.Add(MakeParam("@bangou", SqlDbType.VarChar, 10, InsObj(dtHanyouSiireErr.Rows(i).Item("bangou").ToString.Trim)))
            paramList.Add(MakeParam("@sesyu_mei", SqlDbType.VarChar, 50, InsObj(dtHanyouSiireErr.Rows(i).Item("sesyu_mei").ToString.Trim)))
            paramList.Add(MakeParam("@tekiyou", SqlDbType.VarChar, 512, InsObj(dtHanyouSiireErr.Rows(i).Item("tekiyou").ToString.Trim)))
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
            .AppendLine("	,7 ")
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

    ''' <summary>汎用仕入エラー情報を取得する</summary>
    ''' <param name="strEdiJouhouSakuseiDate">EDI情報作成日</param>
    ''' <param name="strSyoriDate">処理日時</param>
    ''' <param name="intTopCount">取得の最大行数</param>
    ''' <returns>汎用仕入エラーデータテーブル</returns>
    Public Function SelHanyouSiireErr(ByVal strEdiJouhouSakuseiDate As String, _
                                      ByVal strSyoridate As String, _
                                      ByVal intTopCount As Integer) As DataTable
        'DataSetインスタンスの生成
        Dim dsHanyouSiireErr As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine(" SELECT ")
            .AppendLine("   TOP " & intTopCount & "")
            .AppendLine("   edi_jouhou_sakusei_date AS edi_jouhou_sakusei_date ")
            .AppendLine("   ,gyou_no AS gyou_no ")
            .AppendLine("   ,syori_datetime AS syori_datetime ")
            .AppendLine("   ,torikesi AS torikesi  ")
            .AppendLine("   ,kameiten_cd AS kameiten_cd ")
            .AppendLine("   ,tys_kaisya_cd AS tys_kaisya_cd ")
            .AppendLine("   ,tys_kaisya_jigyousyo_cd AS tys_kaisya_jigyousyo_cd ")
            .AppendLine("   ,syouhin_cd AS syouhin_cd ")
            .AppendLine("   ,suu AS suu ")
            .AppendLine("   ,tanka AS tanka ")
            .AppendLine("   ,zei_kbn AS zei_kbn ")
            .AppendLine("   ,syouhizei_gaku AS syouhizei_gaku ")
            .AppendLine("   ,siire_date AS siire_date ")
            .AppendLine("   ,denpyou_siire_date AS denpyou_siire_date ")
            .AppendLine("   ,'' AS siire_keijyou_flg ")
            .AppendLine("   ,'' AS siire_keijyou_date ")
            .AppendLine("   ,kbn AS kbn ")
            .AppendLine("   ,bangou AS bangou ")
            .AppendLine("   ,sesyu_mei AS sesyu_mei ")
            .AppendLine("   ,tekiyou AS tekiyou ")
            .AppendLine("   ,add_login_user_id AS add_login_user_id ")
            .AppendLine("   ,add_login_user_name AS add_login_user_name ")
            .AppendLine("   ,add_datetime AS add_datetime ")
            .AppendLine("   ,upd_login_user_id AS upd_login_user_id ")
            .AppendLine("   ,upd_login_user_name AS upd_login_user_name ")
            .AppendLine("   ,upd_datetime AS upd_datetime ")
            .AppendLine(" FROM ")
            .AppendLine("   t_hannyou_siire_error AS THSE WITH(READCOMMITTED) ")
            .AppendLine(" WHERE ")
            .AppendLine("    ")

            'EDI情報作成日
            .AppendLine(" THSE.edi_jouhou_sakusei_date = @edi_jouhou_sakusei_date ")
            paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, strEdiJouhouSakuseiDate.Trim))

            '処理日時
            .AppendLine(" AND CONVERT(varchar(100),THSE.syori_datetime,112) + REPLACE(CONVERT(varchar(100),THSE.syori_datetime,114),':','') = @syori_datetime")

            paramList.Add(MakeParam("@syori_datetime", SqlDbType.VarChar, 40, strSyoridate))

            'ソート順
            .AppendLine(" ORDER BY ")
            .AppendLine("      THSE.edi_jouhou_sakusei_date, ")
            .AppendLine("      THSE.gyou_no ")
        End With

        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsHanyouSiireErr, _
                    "dsHanyouSiireErr", paramList.ToArray)

        Return dsHanyouSiireErr.Tables("dsHanyouSiireErr")

    End Function

    ''' <summary>汎用仕入エラー件数を取得する</summary>
    ''' <param name="strEdiJouhouSakuseiDate">EDI情報作成日</param>
    ''' <param name="strSyoriDate">処理日時</param>
    ''' <returns>汎用仕入エラー件数</returns>
    Public Function SelHanyouSiireErrCount(ByVal strEdiJouhouSakuseiDate As String, ByVal strSyoriDate As String) As Integer

        'DataSetインスタンスの生成
        Dim dsHanyouSiireErr As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine(" SELECT ")
            .AppendLine("   Count(edi_jouhou_sakusei_date) AS count ")
            .AppendLine(" FROM ")
            .AppendLine("   t_hannyou_siire_error WITH(READCOMMITTED) ")   '汎用仕入エラー情報T
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
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsHanyouSiireErr, _
                    "dsHanyouSiireErr", paramList.ToArray)

        Return dsHanyouSiireErr.Tables("dsHanyouSiireErr").Rows(0).Item("count")



    End Function

End Class

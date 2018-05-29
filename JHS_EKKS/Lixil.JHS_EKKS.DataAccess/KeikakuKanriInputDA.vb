Option Explicit On
Option Strict On
Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Imports System.Data
Imports System.Text

''' <summary>
''' 計画管理 CSV取込
''' </summary>
''' <remarks></remarks>
Public Class KeikakuKanriInputDA

    ''' <summary>
    ''' 検索結果取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>																
    ''' <para>2012/11/23 P-44979 曹敬仁 新規作成 </para>																															
    ''' </history>
    Public Function SelInputKanri() As Data.DataTable
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name)

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'SQL文
        With commandTextSb
            .AppendLine(" SELECT TOP 100 ")
            .AppendLine("	CONVERT(varchar(100),syori_datetime,111) + ' ' + CONVERT(varchar(100),syori_datetime,108) AS syori_datetime ") '処理日時
            .AppendLine("	,CONVERT(varchar(100),syori_datetime,112) + REPLACE(CONVERT(varchar(100),syori_datetime,114),':','') AS torikomi_datetime") '取込日時
            .AppendLine("	,nyuuryoku_file_mei ")                      '入力ファイル名
            .AppendLine("	,CASE error_umu ")
            .AppendLine("    WHEN '1' THEN '有り' ")
            .AppendLine("    WHEN '0' THEN '無し' ")
            .AppendLine("    ELSE '' ")
            .AppendLine("    END AS error_umu ")                        'エラー有無
            .AppendLine("    ,edi_jouhou_sakusei_date ")                'EDI情報作成日
            .AppendLine(" FROM ")
            .AppendLine("	t_upload_kanri WITH(READCOMMITTED) ")       'アップロード管理テーブル
            .AppendLine(" WHERE ")
            .AppendLine("	file_kbn = 2 ")                             'ファイル区分(2：計画管理CSV取込用)
            .AppendLine(" ORDER BY ")
            .AppendLine("	syori_datetime DESC ")                      '処理日時(降順)
        End With

        ' 検索実行
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtInputKanri")

        Return dsReturn.Tables(0)

    End Function

    ''' <summary>
    ''' 全検索結果件数取得
    ''' </summary>
    ''' <returns>全検索結果件数</returns>
    ''' <remarks></remarks>
    ''' <history>																
    ''' <para>2012/11/23 P-44979 曹敬仁 新規作成 </para>																															
    ''' </history>
    Public Function SelInputKanriCount() As String
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name)

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	COUNT(syori_datetime) ")                    '件数
            .AppendLine("FROM ")
            .AppendLine("	t_upload_kanri WITH(READCOMMITTED) ")       'アップロード管理テーブル
            .AppendLine("WHERE ")
            .AppendLine("	file_kbn = 2 ")                             'ファイル区分
        End With

        ' 検索実行
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtInputKanri")

        Return dsReturn.Tables(0).Rows(0).Item(0).ToString

    End Function

    ''' <summary>
    ''' 計画管理_加盟店マスタから加盟店ｺｰﾄﾞを検索する
    ''' </summary>
    ''' <param name="strKeikakuNendo">計画年度</param>
    ''' <param name="strKameitenCd">加盟店ｺｰﾄﾞ</param>
    ''' <returns>検索結果</returns>
    ''' <remarks></remarks>
    ''' <history>																
    ''' <para>2012/12/19 P-44979 曹敬仁 新規作成 </para>																															
    ''' </history>
    Public Function SelKameitenCd(ByVal strKeikakuNendo As String, ByVal strKameitenCd As String) As Boolean
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo, strKameitenCd)

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	kameiten_cd ")                              '加盟店ｺｰﾄﾞ
            .AppendLine("FROM ")
            .AppendLine("	m_keikaku_kameiten WITH(READCOMMITTED) ")   '計画管理_加盟店ﾏｽﾀ
            .AppendLine("WHERE ")
            .AppendLine("	 keikaku_nendo = @keikaku_nendo ")
            .AppendLine("AND kameiten_cd = @kameiten_cd ")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@keikaku_nendo", SqlDbType.Char, 4, strKeikakuNendo))   '計画年度
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 8, strKameitenCd))    '加盟店ｺｰﾄﾞ

        ' 検索実行
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtKameitenCd", paramList.ToArray)

        If dsReturn.Tables("dtKameitenCd").Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    '''' <summary>
    '''' 計画管理用_商品マスタから計画管理_商品コードを検索する
    '''' </summary>
    '''' <param name="strKeikakuNendo">計画年度</param>
    '''' <param name="strKeikakuKanriSyouhinCd">計画管理_商品コード</param>
    '''' <returns>検索結果</returns>
    '''' <remarks></remarks>
    '''' <history>																
    '''' <para>2012/12/19 P-44979 曹敬仁 新規作成 </para>																															
    '''' </history>
    'Public Function SelKeikakuKanriSyouhinCd(ByVal strKeikakuNendo As String, ByVal strKeikakuKanriSyouhinCd As String) As Boolean
    '    'EMAB障害対応情報の格納処理
    '    EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo, strKeikakuKanriSyouhinCd)

    '    'DataSetインスタンスの生成
    '    Dim dsReturn As New Data.DataSet

    '    'SQL文の生成
    '    Dim commandTextSb As New StringBuilder

    '    'パラメータ格納
    '    Dim paramList As New List(Of SqlClient.SqlParameter)

    '    'SQL文
    '    With commandTextSb
    '        .AppendLine("SELECT ")
    '        .AppendLine("	keikaku_kanri_syouhin_cd ")                      '計画管理_商品コード
    '        .AppendLine("FROM ")
    '        .AppendLine("	m_keikaku_kanri_syouhin WITH(READCOMMITTED) ")   '計画管理用_商品マスタ
    '        .AppendLine("WHERE ")
    '        .AppendLine("	 keikaku_nendo = @keikaku_nendo ")
    '        .AppendLine("AND keikaku_kanri_syouhin_cd = @keikaku_kanri_syouhin_cd ")
    '        .AppendLine("AND torikesi = @torikesi ")
    '    End With

    '    'パラメータの設定
    '    paramList.Add(MakeParam("@keikaku_nendo", SqlDbType.Char, 4, strKeikakuNendo))                          '計画年度
    '    paramList.Add(MakeParam("@keikaku_kanri_syouhin_cd", SqlDbType.VarChar, 8, strKeikakuKanriSyouhinCd))   '計画管理_商品コード
    '    paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 1, 0))                                              '取消

    '    ' 検索実行
    '    FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtKeikakuKanriSyouhinCd", paramList.ToArray)

    '    If dsReturn.Tables("dtKeikakuKanriSyouhinCd").Rows.Count > 0 Then
    '        Return True
    '    Else
    '        Return False
    '    End If

    'End Function

    ''' <summary>
    ''' 計画管理テーブルから計画確定データを検索する
    ''' </summary>
    ''' <param name="strKeikakuNendo">計画年度</param>
    ''' <param name="strKameitenCd">加盟店ｺｰﾄﾞ</param>
    ''' <param name="strSyouhinCd">計画管理商品コード</param>
    ''' <returns>計画確定データ件数</returns>
    ''' <remarks></remarks>
    ''' <history>																
    ''' <para>2012/12/12 P-44979 曹敬仁 新規作成 </para>																															
    ''' </history>
    Public Function SelKeikakuKakuteiCount(ByVal strKeikakuNendo As String, _
                                           ByVal strKameitenCd As String, _
                                           ByVal strSyouhinCd As String) As Boolean
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo, strKameitenCd, strSyouhinCd)

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	COUNT(keikaku_kanri_syouhin_cd) ")          '件数
            .AppendLine("FROM ")
            .AppendLine("	t_keikaku_kanri WITH(READCOMMITTED) ")      '計画管理テーブル
            .AppendLine("WHERE ")
            .AppendLine("	  keikaku_nendo = @keikaku_nendo ")                         '計画_年度
            .AppendLine("AND  kameiten_cd = @kameiten_cd ")                             '加盟店ｺｰﾄﾞ
            .AppendLine("AND  keikaku_kanri_syouhin_cd = @keikaku_kanri_syouhin_cd ")   '計画管理_商品コード
            .AppendLine("AND  (keikaku_kakutei_flg = @keikaku_kakutei_flg ")            '計画確定FLG
            .AppendLine("OR  keikaku_huhen_flg = @keikaku_huhen_flg) ")                 '計画値不変FLG
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@keikaku_nendo", SqlDbType.Char, 4, strKeikakuNendo))               '計画年度(YYYY)
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 8, strKameitenCd))                '加盟店ｺｰﾄﾞ
        paramList.Add(MakeParam("@keikaku_kanri_syouhin_cd", SqlDbType.VarChar, 8, strSyouhinCd))    '計画管理_商品コード
        paramList.Add(MakeParam("@keikaku_kakutei_flg", SqlDbType.Int, 1, "1"))                      '計画確定FLG
        paramList.Add(MakeParam("@keikaku_huhen_flg", SqlDbType.Int, 1, "1"))                        '計画値不変FLG

        ' 検索実行
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtKeikakuKanri", paramList.ToArray)

        If Convert.ToInt32(dsReturn.Tables(0).Rows(0).Item(0)) > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>
    ''' FC用計画管理テーブルから計画確定データを検索する
    ''' </summary>
    ''' <param name="strKeikakuNendo">計画年度</param>
    ''' <param name="strBusyoCd">部署ｺｰﾄﾞ</param>
    ''' <param name="strSyouhinCd">計画管理_商品コード</param>
    ''' <returns>計画確定データ件数</returns>
    ''' <remarks></remarks>
    ''' <history>																
    ''' <para>2012/12/12 P-44979 曹敬仁 新規作成 </para>																															
    ''' </history>
    Public Function SelFCKeikakuKakuteiCount(ByVal strKeikakuNendo As String, _
                                             ByVal strBusyoCd As String, _
                                             ByVal strSyouhinCd As String) As Boolean
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo, strBusyoCd, strSyouhinCd)

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	COUNT(keikaku_kanri_syouhin_cd) ")              '件数
            .AppendLine("FROM ")
            .AppendLine("	t_fc_keikaku_kanri WITH(READCOMMITTED) ")       'FC用計画管理テーブル
            .AppendLine("WHERE ")
            .AppendLine("	  keikaku_nendo = @keikaku_nendo ")                         '計画_年度
            .AppendLine("AND  busyo_cd = @busyo_cd ")                                   '部署ｺｰﾄﾞ
            .AppendLine("AND  keikaku_kanri_syouhin_cd = @keikaku_kanri_syouhin_cd ")   '計画管理_商品コード
            .AppendLine("AND  (keikaku_kakutei_flg = @keikaku_kakutei_flg ")            '計画確定FLG
            .AppendLine("OR  keikaku_huhen_flg = @keikaku_huhen_flg) ")                 '計画値不変FLG
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@keikaku_nendo", SqlDbType.Char, 4, strKeikakuNendo))               '計画年度(YYYY)
        paramList.Add(MakeParam("@busyo_cd", SqlDbType.VarChar, 4, strBusyoCd))                      '部署ｺｰﾄﾞ
        paramList.Add(MakeParam("@keikaku_kanri_syouhin_cd", SqlDbType.VarChar, 8, strSyouhinCd))    '計画管理_商品コード
        paramList.Add(MakeParam("@keikaku_kakutei_flg", SqlDbType.Int, 1, "1"))                      '計画確定FLG
        paramList.Add(MakeParam("@keikaku_huhen_flg", SqlDbType.Int, 1, "1"))                        '計画値不変FLG

        '検索実行
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtFCKeikakuKanri", paramList.ToArray)

        If Convert.ToInt32(dsReturn.Tables(0).Rows(0).Item(0)) > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>
    ''' エラー内容を計画管理表_取込エラー情報テーブルに登録する
    ''' </summary>
    ''' <param name="strEdiJouhouSakuseiDate">EDI情報作成日</param>
    ''' <param name="strSyoriDatetime">処理日時</param>
    ''' <param name="drValue">登録データレコード</param>
    ''' <param name="strLoginUserId">ログインユーザーID</param>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    Public Function InsKeikakuTorikomiError(ByVal strEdiJouhouSakuseiDate As String, _
                                            ByVal strSyoriDatetime As String, _
                                            ByVal drValue As DataRow, _
                                            ByVal strLoginUserId As String) As Boolean
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                strEdiJouhouSakuseiDate, strSyoriDatetime, drValue, strLoginUserId)

        '戻り値
        Dim InsCount As Integer = 0

        Dim commandTextSb As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With commandTextSb
            .AppendLine("INSERT INTO  ")
            .AppendLine("	t_keikaku_torikomi_error WITH(UPDLOCK)")
            .AppendLine("	( ")
            .AppendLine("		edi_jouhou_sakusei_date ")          'EDI情報作成日
            .AppendLine("		,gyou_no ")                         '行NO
            .AppendLine("		,syori_datetime ")                  '処理日時
            .AppendLine("		,error_naiyou ")                    'エラー内容
            .AppendLine("		,add_login_user_id ")               '登録ログインユーザーID
            .AppendLine("		,add_datetime ")                    '登録日時
            .AppendLine("		,upd_login_user_id ")               '更新ログインユーザーID
            .AppendLine("		,upd_datetime ")                    '更新日時
            .AppendLine("	) ")
            .AppendLine("VALUES ")
            .AppendLine("( ")
            .AppendLine("	@edi_jouhou_sakusei_date ")
            .AppendLine("	,@gyou_no ")
            .AppendLine("	,@syori_datetime ")
            .AppendLine("	,@error_naiyou ")
            .AppendLine("	,@add_login_user_id ")
            .AppendLine("	,GETDATE() ")
            .AppendLine("	,@add_login_user_id ")
            .AppendLine("	,GETDATE() ")
            .AppendLine(") ")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, strEdiJouhouSakuseiDate))                'EDI情報作成日
        paramList.Add(MakeParam("@gyou_no", SqlDbType.BigInt, 12, drValue("gyou_no")))                                      '行NO
        paramList.Add(MakeParam("@syori_datetime", SqlDbType.DateTime, 30, Convert.ToDateTime(CDate(strSyoriDatetime))))    '処理日時
        paramList.Add(MakeParam("@error_naiyou", SqlDbType.VarChar, 255, drValue("error_naiyou")))                          'エラー内容
        paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, strLoginUserId))                               'ログインユーザーID

        '登録実行
        InsCount = ExecuteNonQuery(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

        If InsCount > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' 計画管理テーブルへのデータ登録
    ''' </summary>
    ''' <param name="drValue">登録データレコード</param>
    ''' <param name="strLoginUserId">ログインユーザーID</param>
    ''' <param name="strSyoriDatetime">処理日時</param>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    ''' <history>																
    ''' <para>2012/11/27 P-44979 曹敬仁 新規作成</para>																															
    ''' </history>
    Public Function InsKeikakuKanriData(ByVal drValue As DataRow, _
                                        ByVal strLoginUserId As String, _
                                        ByVal strSyoriDatetime As String) As Boolean
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, drValue, strLoginUserId, strSyoriDatetime)

        Dim sqlBuffer As New System.Text.StringBuilder
        Dim insCount As Integer

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With sqlBuffer
            .AppendLine(" INSERT INTO t_keikaku_kanri WITH(UPDLOCK) ( ")
            .AppendLine("     [keikaku_nendo], ")                             '計画_年度
            .AppendLine("     [add_datetime], ")                              '登録日時
            .AppendLine("     [kameiten_cd], ")                               '加盟店ｺｰﾄﾞ
            .AppendLine("     [keikaku_kanri_syouhin_cd], ")                  '計画管理_商品コード
            .AppendLine("     [kameiten_mei], ")                              '加盟店名
            .AppendLine("     [bunbetu_cd], ")                                '分別コード

            .AppendLine("     [4gatu_keisanyou_uri_heikin_tanka], ")          '4月_計算用__売上平均単価
            .AppendLine("     [4gatu_keisanyou_siire_heikin_tanka], ")        '4月_計算用__仕入平均単価

            .AppendLine("     [4gatu_keisanyou_koj_hantei_ritu], ")           '4月_計算用_工事判定率
            .AppendLine("     [4gatu_keisanyou_koj_jyuchuu_ritu], ")          '4月_計算用_工事受注率
            .AppendLine("     [4gatu_keisanyou_tyoku_koj_ritu], ")            '4月_計算用_直工事率
            .AppendLine("     [4gatu_keikaku_kensuu], ")                      '4月_計画件数
            .AppendLine("     [4gatu_keikaku_kingaku], ")                     '4月_計画金額
            .AppendLine("     [4gatu_keikaku_arari], ")                       '4月_計画粗利

            .AppendLine("     [5gatu_keisanyou_uri_heikin_tanka], ")          '5月_計算用__売上平均単価
            .AppendLine("     [5gatu_keisanyou_siire_heikin_tanka], ")        '5月_計算用__仕入平均単価

            .AppendLine("     [5gatu_keisanyou_koj_hantei_ritu], ")           '5月_計算用_工事判定率
            .AppendLine("     [5gatu_keisanyou_koj_jyuchuu_ritu], ")          '5月_計算用_工事受注率
            .AppendLine("     [5gatu_keisanyou_tyoku_koj_ritu], ")            '5月_計算用_直工事率
            .AppendLine("     [5gatu_keikaku_kensuu], ")                      '5月_計画件数
            .AppendLine("     [5gatu_keikaku_kingaku], ")                     '5月_計画金額
            .AppendLine("     [5gatu_keikaku_arari], ")                       '5月_計画粗利

            .AppendLine("     [6gatu_keisanyou_uri_heikin_tanka], ")          '6月_計算用__売上平均単価
            .AppendLine("     [6gatu_keisanyou_siire_heikin_tanka], ")        '6月_計算用__仕入平均単価

            .AppendLine("     [6gatu_keisanyou_koj_hantei_ritu], ")           '6月_計算用_工事判定率
            .AppendLine("     [6gatu_keisanyou_koj_jyuchuu_ritu], ")          '6月_計算用_工事受注率
            .AppendLine("     [6gatu_keisanyou_tyoku_koj_ritu], ")            '6月_計算用_直工事率
            .AppendLine("     [6gatu_keikaku_kensuu], ")                      '6月_計画件数
            .AppendLine("     [6gatu_keikaku_kingaku], ")                     '6月_計画金額
            .AppendLine("     [6gatu_keikaku_arari], ")                       '6月_計画粗利

            .AppendLine("     [7gatu_keisanyou_uri_heikin_tanka], ")          '7月_計算用__売上平均単価
            .AppendLine("     [7gatu_keisanyou_siire_heikin_tanka], ")        '7月_計算用__仕入平均単価

            .AppendLine("     [7gatu_keisanyou_koj_hantei_ritu], ")           '7月_計算用_工事判定率
            .AppendLine("     [7gatu_keisanyou_koj_jyuchuu_ritu], ")          '7月_計算用_工事受注率
            .AppendLine("     [7gatu_keisanyou_tyoku_koj_ritu], ")            '7月_計算用_直工事率
            .AppendLine("     [7gatu_keikaku_kensuu], ")                      '7月_計画件数
            .AppendLine("     [7gatu_keikaku_kingaku], ")                     '7月_計画金額
            .AppendLine("     [7gatu_keikaku_arari], ")                       '7月_計画粗利

            .AppendLine("     [8gatu_keisanyou_uri_heikin_tanka], ")          '8月_計算用__売上平均単価
            .AppendLine("     [8gatu_keisanyou_siire_heikin_tanka], ")        '8月_計算用__仕入平均単価

            .AppendLine("     [8gatu_keisanyou_koj_hantei_ritu], ")           '8月_計算用_工事判定率
            .AppendLine("     [8gatu_keisanyou_koj_jyuchuu_ritu], ")          '8月_計算用_工事受注率
            .AppendLine("     [8gatu_keisanyou_tyoku_koj_ritu], ")            '8月_計算用_直工事率
            .AppendLine("     [8gatu_keikaku_kensuu], ")                      '8月_計画件数
            .AppendLine("     [8gatu_keikaku_kingaku], ")                     '8月_計画金額
            .AppendLine("     [8gatu_keikaku_arari], ")                       '8月_計画粗利

            .AppendLine("     [9gatu_keisanyou_uri_heikin_tanka], ")          '9月_計算用__売上平均単価
            .AppendLine("     [9gatu_keisanyou_siire_heikin_tanka], ")        '9月_計算用__仕入平均単価

            .AppendLine("     [9gatu_keisanyou_koj_hantei_ritu], ")           '9月_計算用_工事判定率
            .AppendLine("     [9gatu_keisanyou_koj_jyuchuu_ritu], ")          '9月_計算用_工事受注率
            .AppendLine("     [9gatu_keisanyou_tyoku_koj_ritu], ")            '9月_計算用_直工事率
            .AppendLine("     [9gatu_keikaku_kensuu], ")                      '9月_計画件数
            .AppendLine("     [9gatu_keikaku_kingaku], ")                     '9月_計画金額
            .AppendLine("     [9gatu_keikaku_arari], ")                       '9月_計画粗利

            .AppendLine("     [10gatu_keisanyou_uri_heikin_tanka], ")          '10月_計算用__売上平均単価
            .AppendLine("     [10gatu_keisanyou_siire_heikin_tanka], ")        '10月_計算用__仕入平均単価

            .AppendLine("     [10gatu_keisanyou_koj_hantei_ritu], ")          '10月_計算用_工事判定率
            .AppendLine("     [10gatu_keisanyou_koj_jyuchuu_ritu], ")         '10月_計算用_工事受注率
            .AppendLine("     [10gatu_keisanyou_tyoku_koj_ritu], ")           '10月_計算用_直工事率
            .AppendLine("     [10gatu_keikaku_kensuu], ")                     '10月_計画件数
            .AppendLine("     [10gatu_keikaku_kingaku], ")                    '10月_計画金額
            .AppendLine("     [10gatu_keikaku_arari], ")                      '10月_計画粗利

            .AppendLine("     [11gatu_keisanyou_uri_heikin_tanka], ")          '11月_計算用__売上平均単価
            .AppendLine("     [11gatu_keisanyou_siire_heikin_tanka], ")        '11月_計算用__仕入平均単価

            .AppendLine("     [11gatu_keisanyou_koj_hantei_ritu], ")          '11月_計算用_工事判定率
            .AppendLine("     [11gatu_keisanyou_koj_jyuchuu_ritu], ")         '11月_計算用_工事受注率
            .AppendLine("     [11gatu_keisanyou_tyoku_koj_ritu], ")           '11月_計算用_直工事率
            .AppendLine("     [11gatu_keikaku_kensuu], ")                     '11月_計画件数
            .AppendLine("     [11gatu_keikaku_kingaku], ")                    '11月_計画金額
            .AppendLine("     [11gatu_keikaku_arari], ")                      '11月_計画粗利

            .AppendLine("     [12gatu_keisanyou_uri_heikin_tanka], ")          '12月_計算用__売上平均単価
            .AppendLine("     [12gatu_keisanyou_siire_heikin_tanka], ")        '12月_計算用__仕入平均単価

            .AppendLine("     [12gatu_keisanyou_koj_hantei_ritu], ")          '12月_計算用_工事判定率
            .AppendLine("     [12gatu_keisanyou_koj_jyuchuu_ritu], ")         '12月_計算用_工事受注率
            .AppendLine("     [12gatu_keisanyou_tyoku_koj_ritu], ")           '12月_計算用_直工事率
            .AppendLine("     [12gatu_keikaku_kensuu], ")                     '12月_計画件数
            .AppendLine("     [12gatu_keikaku_kingaku], ")                    '12月_計画金額
            .AppendLine("     [12gatu_keikaku_arari], ")                      '12月_計画粗利

            .AppendLine("     [1gatu_keisanyou_uri_heikin_tanka], ")          '1月_計算用__売上平均単価
            .AppendLine("     [1gatu_keisanyou_siire_heikin_tanka], ")        '1月_計算用__仕入平均単価

            .AppendLine("     [1gatu_keisanyou_koj_hantei_ritu], ")           '1月_計算用_工事判定率
            .AppendLine("     [1gatu_keisanyou_koj_jyuchuu_ritu], ")          '1月_計算用_工事受注率
            .AppendLine("     [1gatu_keisanyou_tyoku_koj_ritu], ")            '1月_計算用_直工事率
            .AppendLine("     [1gatu_keikaku_kensuu], ")                      '1月_計画件数
            .AppendLine("     [1gatu_keikaku_kingaku], ")                     '1月_計画金額
            .AppendLine("     [1gatu_keikaku_arari], ")                       '1月_計画粗利

            .AppendLine("     [2gatu_keisanyou_uri_heikin_tanka], ")          '2月_計算用__売上平均単価
            .AppendLine("     [2gatu_keisanyou_siire_heikin_tanka], ")        '2月_計算用__仕入平均単価

            .AppendLine("     [2gatu_keisanyou_koj_hantei_ritu], ")           '2月_計算用_工事判定率
            .AppendLine("     [2gatu_keisanyou_koj_jyuchuu_ritu], ")          '2月_計算用_工事受注率
            .AppendLine("     [2gatu_keisanyou_tyoku_koj_ritu], ")            '2月_計算用_直工事率
            .AppendLine("     [2gatu_keikaku_kensuu], ")                      '2月_計画件数
            .AppendLine("     [2gatu_keikaku_kingaku], ")                     '2月_計画金額
            .AppendLine("     [2gatu_keikaku_arari], ")                       '2月_計画粗利

            .AppendLine("     [3gatu_keisanyou_uri_heikin_tanka], ")          '3月_計算用__売上平均単価
            .AppendLine("     [3gatu_keisanyou_siire_heikin_tanka], ")        '3月_計算用__仕入平均単価

            .AppendLine("     [3gatu_keisanyou_koj_hantei_ritu], ")           '3月_計算用_工事判定率
            .AppendLine("     [3gatu_keisanyou_koj_jyuchuu_ritu], ")          '3月_計算用_工事受注率
            .AppendLine("     [3gatu_keisanyou_tyoku_koj_ritu], ")            '3月_計算用_直工事率
            .AppendLine("     [3gatu_keikaku_kensuu], ")                      '3月_計画件数
            .AppendLine("     [3gatu_keikaku_kingaku], ")                     '3月_計画金額
            .AppendLine("     [3gatu_keikaku_arari], ")                       '3月_計画粗利
            .AppendLine("     [keikaku_kakutei_flg], ")                       '計画確定FLG
            .AppendLine("     [keikaku_kakutei_id], ")                        '計画確定者ID
            .AppendLine("     [keikaku_kakutei_datetime], ")                  '計画確定日時
            .AppendLine("     [kakutei_minaosi_id], ")                        '確定見直し者ID
            .AppendLine("     [kakutei_minaosi_datetime], ")                  '確定見直し日時
            .AppendLine("     [keikaku_minaosi_flg], ")                       '計画見直しFLG
            .AppendLine("     [keikaku_huhen_flg], ")                         '計画値不変FLG
            '2013/10/14 李宇追加　↓
            .AppendLine("     UCCRPDEV,")                                     '報連相_顧客区分
            .AppendLine("     UCCRPSEQ,")                                     '報連相_顧客コードSEQ
            '2013/10/14 李宇追加　↑
            .AppendLine("     [add_login_user_id], ")                         '登録ログインユーザーID
            .AppendLine("     [upd_login_user_id], ")                         '更新ログインユーザーID
            .AppendLine("     [upd_datetime] ")                               '更新日時

            .AppendLine("    )VALUES( ")
            .AppendLine("     @keikaku_nendo, ")
            .AppendLine("     @add_datetime, ")
            .AppendLine("     @kameiten_cd, ")
            .AppendLine("     @keikaku_kanri_syouhin_cd, ")
            .AppendLine("     @kameiten_mei, ")
            .AppendLine("     @bunbetu_cd, ")

            .AppendLine("     @4gatu_keisanyou_uri_heikin_tanka, ")
            .AppendLine("     @4gatu_keisanyou_siire_heikin_tanka, ")

            .AppendLine("     @4gatu_keisanyou_koj_hantei_ritu, ")
            .AppendLine("     @4gatu_keisanyou_koj_jyuchuu_ritu, ")
            .AppendLine("     @4gatu_keisanyou_tyoku_koj_ritu, ")
            .AppendLine("     @4gatu_keikaku_kensuu, ")
            .AppendLine("     @4gatu_keikaku_kingaku, ")
            .AppendLine("     @4gatu_keikaku_arari, ")

            .AppendLine("     @5gatu_keisanyou_uri_heikin_tanka, ")
            .AppendLine("     @5gatu_keisanyou_siire_heikin_tanka, ")

            .AppendLine("     @5gatu_keisanyou_koj_hantei_ritu, ")
            .AppendLine("     @5gatu_keisanyou_koj_jyuchuu_ritu, ")
            .AppendLine("     @5gatu_keisanyou_tyoku_koj_ritu, ")
            .AppendLine("     @5gatu_keikaku_kensuu, ")
            .AppendLine("     @5gatu_keikaku_kingaku, ")
            .AppendLine("     @5gatu_keikaku_arari, ")

            .AppendLine("     @6gatu_keisanyou_uri_heikin_tanka, ")
            .AppendLine("     @6gatu_keisanyou_siire_heikin_tanka, ")

            .AppendLine("     @6gatu_keisanyou_koj_hantei_ritu, ")
            .AppendLine("     @6gatu_keisanyou_koj_jyuchuu_ritu, ")
            .AppendLine("     @6gatu_keisanyou_tyoku_koj_ritu, ")
            .AppendLine("     @6gatu_keikaku_kensuu, ")
            .AppendLine("     @6gatu_keikaku_kingaku, ")
            .AppendLine("     @6gatu_keikaku_arari, ")

            .AppendLine("     @7gatu_keisanyou_uri_heikin_tanka, ")
            .AppendLine("     @7gatu_keisanyou_siire_heikin_tanka, ")

            .AppendLine("     @7gatu_keisanyou_koj_hantei_ritu, ")
            .AppendLine("     @7gatu_keisanyou_koj_jyuchuu_ritu, ")
            .AppendLine("     @7gatu_keisanyou_tyoku_koj_ritu, ")
            .AppendLine("     @7gatu_keikaku_kensuu, ")
            .AppendLine("     @7gatu_keikaku_kingaku, ")
            .AppendLine("     @7gatu_keikaku_arari, ")

            .AppendLine("     @8gatu_keisanyou_uri_heikin_tanka, ")
            .AppendLine("     @8gatu_keisanyou_siire_heikin_tanka, ")

            .AppendLine("     @8gatu_keisanyou_koj_hantei_ritu, ")
            .AppendLine("     @8gatu_keisanyou_koj_jyuchuu_ritu, ")
            .AppendLine("     @8gatu_keisanyou_tyoku_koj_ritu, ")
            .AppendLine("     @8gatu_keikaku_kensuu, ")
            .AppendLine("     @8gatu_keikaku_kingaku, ")
            .AppendLine("     @8gatu_keikaku_arari, ")

            .AppendLine("     @9gatu_keisanyou_uri_heikin_tanka, ")
            .AppendLine("     @9gatu_keisanyou_siire_heikin_tanka, ")

            .AppendLine("     @9gatu_keisanyou_koj_hantei_ritu, ")
            .AppendLine("     @9gatu_keisanyou_koj_jyuchuu_ritu, ")
            .AppendLine("     @9gatu_keisanyou_tyoku_koj_ritu, ")
            .AppendLine("     @9gatu_keikaku_kensuu, ")
            .AppendLine("     @9gatu_keikaku_kingaku, ")
            .AppendLine("     @9gatu_keikaku_arari, ")

            .AppendLine("     @10gatu_keisanyou_uri_heikin_tanka, ")
            .AppendLine("     @10gatu_keisanyou_siire_heikin_tanka, ")

            .AppendLine("     @10gatu_keisanyou_koj_hantei_ritu, ")
            .AppendLine("     @10gatu_keisanyou_koj_jyuchuu_ritu, ")
            .AppendLine("     @10gatu_keisanyou_tyoku_koj_ritu, ")
            .AppendLine("     @10gatu_keikaku_kensuu, ")
            .AppendLine("     @10gatu_keikaku_kingaku, ")
            .AppendLine("     @10gatu_keikaku_arari, ")

            .AppendLine("     @11gatu_keisanyou_uri_heikin_tanka, ")
            .AppendLine("     @11gatu_keisanyou_siire_heikin_tanka, ")

            .AppendLine("     @11gatu_keisanyou_koj_hantei_ritu, ")
            .AppendLine("     @11gatu_keisanyou_koj_jyuchuu_ritu, ")
            .AppendLine("     @11gatu_keisanyou_tyoku_koj_ritu, ")
            .AppendLine("     @11gatu_keikaku_kensuu, ")
            .AppendLine("     @11gatu_keikaku_kingaku, ")
            .AppendLine("     @11gatu_keikaku_arari, ")

            .AppendLine("     @12gatu_keisanyou_uri_heikin_tanka, ")
            .AppendLine("     @12gatu_keisanyou_siire_heikin_tanka, ")

            .AppendLine("     @12gatu_keisanyou_koj_hantei_ritu, ")
            .AppendLine("     @12gatu_keisanyou_koj_jyuchuu_ritu, ")
            .AppendLine("     @12gatu_keisanyou_tyoku_koj_ritu, ")
            .AppendLine("     @12gatu_keikaku_kensuu, ")
            .AppendLine("     @12gatu_keikaku_kingaku, ")
            .AppendLine("     @12gatu_keikaku_arari, ")

            .AppendLine("     @1gatu_keisanyou_uri_heikin_tanka, ")
            .AppendLine("     @1gatu_keisanyou_siire_heikin_tanka, ")

            .AppendLine("     @1gatu_keisanyou_koj_hantei_ritu, ")
            .AppendLine("     @1gatu_keisanyou_koj_jyuchuu_ritu, ")
            .AppendLine("     @1gatu_keisanyou_tyoku_koj_ritu, ")
            .AppendLine("     @1gatu_keikaku_kensuu, ")
            .AppendLine("     @1gatu_keikaku_kingaku, ")
            .AppendLine("     @1gatu_keikaku_arari, ")

            .AppendLine("     @2gatu_keisanyou_uri_heikin_tanka, ")
            .AppendLine("     @2gatu_keisanyou_siire_heikin_tanka, ")

            .AppendLine("     @2gatu_keisanyou_koj_hantei_ritu, ")
            .AppendLine("     @2gatu_keisanyou_koj_jyuchuu_ritu, ")
            .AppendLine("     @2gatu_keisanyou_tyoku_koj_ritu, ")
            .AppendLine("     @2gatu_keikaku_kensuu, ")
            .AppendLine("     @2gatu_keikaku_kingaku, ")
            .AppendLine("     @2gatu_keikaku_arari, ")

            .AppendLine("     @3gatu_keisanyou_uri_heikin_tanka, ")
            .AppendLine("     @3gatu_keisanyou_siire_heikin_tanka, ")

            .AppendLine("     @3gatu_keisanyou_koj_hantei_ritu, ")
            .AppendLine("     @3gatu_keisanyou_koj_jyuchuu_ritu, ")
            .AppendLine("     @3gatu_keisanyou_tyoku_koj_ritu, ")
            .AppendLine("     @3gatu_keikaku_kensuu, ")
            .AppendLine("     @3gatu_keikaku_kingaku, ")
            .AppendLine("     @3gatu_keikaku_arari, ")
            .AppendLine("     NULL, ")
            .AppendLine("     NULL, ")
            .AppendLine("     NULL, ")
            .AppendLine("     NULL, ")
            .AppendLine("     NULL, ")
            .AppendLine("     NULL, ")
            .AppendLine("     NULL, ")
            '2013/10/14 李宇追加　↓
            .AppendLine("     @UCCRPDEV,")                                      '報連相_顧客区分
            .AppendLine("     @UCCRPSEQ,")                                      '報連相_顧客コードSEQ
            '2013/10/14 李宇追加　↑
            .AppendLine("     @add_login_user_id, ")
            .AppendLine("     @add_login_user_id, ")
            .AppendLine("     GETDATE() ")

            .AppendLine(" 	) ")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@keikaku_nendo", SqlDbType.Char, 4, drValue("keikaku_nendo")))                         '計画年度(YYYY)
        paramList.Add(MakeParam("@add_datetime", SqlDbType.DateTime, 30, Convert.ToDateTime(CDate(strSyoriDatetime))))  '登録日時
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 8, drValue("kameiten_cd")))                          '加盟店ｺｰﾄﾞ
        paramList.Add(MakeParam("@keikaku_kanri_syouhin_cd", SqlDbType.VarChar, 8, drValue("keikaku_kanri_syouhin_cd")))                        '計画管理_商品コード
        paramList.Add(MakeParam("@kameiten_mei", SqlDbType.VarChar, 40, drValue("kameiten_mei")))                       '加盟店名
        paramList.Add(MakeParam("@bunbetu_cd", SqlDbType.VarChar, 3, drValue("bunbetu_cd")))                            '分別コード

        paramList.Add(MakeParam("@4gatu_keisanyou_uri_heikin_tanka", SqlDbType.BigInt, 12, drValue("4gatu_keisanyou_uri_heikin_tanka")))        '4月_計算用__売上平均単価
        paramList.Add(MakeParam("@4gatu_keisanyou_siire_heikin_tanka", SqlDbType.BigInt, 12, drValue("4gatu_keisanyou_siire_heikin_tanka")))    '4月_計算用__仕入平均単価

        paramList.Add(MakeParam("@4gatu_keisanyou_koj_hantei_ritu", SqlDbType.Decimal, 4, drValue("4gatu_keisanyou_koj_hantei_ritu")))          '4月_計算用_工事判定率
        paramList.Add(MakeParam("@4gatu_keisanyou_koj_jyuchuu_ritu", SqlDbType.Decimal, 4, drValue("4gatu_keisanyou_koj_jyuchuu_ritu")))        '4月_計算用_工事受注率
        paramList.Add(MakeParam("@4gatu_keisanyou_tyoku_koj_ritu", SqlDbType.Decimal, 4, drValue("4gatu_keisanyou_tyoku_koj_ritu")))            '4月_計算用_直工事率
        paramList.Add(MakeParam("@4gatu_keikaku_kensuu", SqlDbType.BigInt, 12, drValue("4gatu_keikaku_kensuu")))        '4月_計画件数
        paramList.Add(MakeParam("@4gatu_keikaku_kingaku", SqlDbType.BigInt, 12, drValue("4gatu_keikaku_kingaku")))      '4月_計画金額
        paramList.Add(MakeParam("@4gatu_keikaku_arari", SqlDbType.BigInt, 12, drValue("4gatu_keikaku_arari")))          '4月_計画粗利

        paramList.Add(MakeParam("@5gatu_keisanyou_uri_heikin_tanka", SqlDbType.BigInt, 12, drValue("5gatu_keisanyou_uri_heikin_tanka")))        '5月_計算用__売上平均単価
        paramList.Add(MakeParam("@5gatu_keisanyou_siire_heikin_tanka", SqlDbType.BigInt, 12, drValue("5gatu_keisanyou_siire_heikin_tanka")))    '5月_計算用__仕入平均単価

        paramList.Add(MakeParam("@5gatu_keisanyou_koj_hantei_ritu", SqlDbType.Decimal, 4, drValue("5gatu_keisanyou_koj_hantei_ritu")))          '5月_計算用_工事判定率
        paramList.Add(MakeParam("@5gatu_keisanyou_koj_jyuchuu_ritu", SqlDbType.Decimal, 4, drValue("5gatu_keisanyou_koj_jyuchuu_ritu")))        '5月_計算用_工事受注率
        paramList.Add(MakeParam("@5gatu_keisanyou_tyoku_koj_ritu", SqlDbType.Decimal, 4, drValue("5gatu_keisanyou_tyoku_koj_ritu")))            '5月_計算用_直工事率
        paramList.Add(MakeParam("@5gatu_keikaku_kensuu", SqlDbType.BigInt, 12, drValue("5gatu_keikaku_kensuu")))        '5月_計画件数
        paramList.Add(MakeParam("@5gatu_keikaku_kingaku", SqlDbType.BigInt, 12, drValue("5gatu_keikaku_kingaku")))      '5月_計画金額
        paramList.Add(MakeParam("@5gatu_keikaku_arari", SqlDbType.BigInt, 12, drValue("5gatu_keikaku_arari")))          '5月_計画粗利

        paramList.Add(MakeParam("@6gatu_keisanyou_uri_heikin_tanka", SqlDbType.BigInt, 12, drValue("6gatu_keisanyou_uri_heikin_tanka")))        '6月_計算用__売上平均単価
        paramList.Add(MakeParam("@6gatu_keisanyou_siire_heikin_tanka", SqlDbType.BigInt, 12, drValue("6gatu_keisanyou_siire_heikin_tanka")))    '6月_計算用__仕入平均単価

        paramList.Add(MakeParam("@6gatu_keisanyou_koj_hantei_ritu", SqlDbType.Decimal, 4, drValue("6gatu_keisanyou_koj_hantei_ritu")))          '6月_計算用_工事判定率
        paramList.Add(MakeParam("@6gatu_keisanyou_koj_jyuchuu_ritu", SqlDbType.Decimal, 4, drValue("6gatu_keisanyou_koj_jyuchuu_ritu")))        '6月_計算用_工事受注率
        paramList.Add(MakeParam("@6gatu_keisanyou_tyoku_koj_ritu", SqlDbType.Decimal, 4, drValue("6gatu_keisanyou_tyoku_koj_ritu")))            '6月_計算用_直工事率
        paramList.Add(MakeParam("@6gatu_keikaku_kensuu", SqlDbType.BigInt, 12, drValue("6gatu_keikaku_kensuu")))        '6月_計画件数
        paramList.Add(MakeParam("@6gatu_keikaku_kingaku", SqlDbType.BigInt, 12, drValue("6gatu_keikaku_kingaku")))      '6月_計画金額
        paramList.Add(MakeParam("@6gatu_keikaku_arari", SqlDbType.BigInt, 12, drValue("6gatu_keikaku_arari")))          '6月_計画粗利

        paramList.Add(MakeParam("@7gatu_keisanyou_uri_heikin_tanka", SqlDbType.BigInt, 12, drValue("7gatu_keisanyou_uri_heikin_tanka")))        '7月_計算用__売上平均単価
        paramList.Add(MakeParam("@7gatu_keisanyou_siire_heikin_tanka", SqlDbType.BigInt, 12, drValue("7gatu_keisanyou_siire_heikin_tanka")))    '7月_計算用__仕入平均単価

        paramList.Add(MakeParam("@7gatu_keisanyou_koj_hantei_ritu", SqlDbType.Decimal, 4, drValue("7gatu_keisanyou_koj_hantei_ritu")))          '7月_計算用_工事判定率
        paramList.Add(MakeParam("@7gatu_keisanyou_koj_jyuchuu_ritu", SqlDbType.Decimal, 4, drValue("7gatu_keisanyou_koj_jyuchuu_ritu")))        '7月_計算用_工事受注率
        paramList.Add(MakeParam("@7gatu_keisanyou_tyoku_koj_ritu", SqlDbType.Decimal, 4, drValue("7gatu_keisanyou_tyoku_koj_ritu")))            '7月_計算用_直工事率
        paramList.Add(MakeParam("@7gatu_keikaku_kensuu", SqlDbType.BigInt, 12, drValue("7gatu_keikaku_kensuu")))        '7月_計画件数
        paramList.Add(MakeParam("@7gatu_keikaku_kingaku", SqlDbType.BigInt, 12, drValue("7gatu_keikaku_kingaku")))      '7月_計画金額
        paramList.Add(MakeParam("@7gatu_keikaku_arari", SqlDbType.BigInt, 12, drValue("7gatu_keikaku_arari")))          '7月_計画粗利

        paramList.Add(MakeParam("@8gatu_keisanyou_uri_heikin_tanka", SqlDbType.BigInt, 12, drValue("8gatu_keisanyou_uri_heikin_tanka")))        '8月_計算用__売上平均単価
        paramList.Add(MakeParam("@8gatu_keisanyou_siire_heikin_tanka", SqlDbType.BigInt, 12, drValue("8gatu_keisanyou_siire_heikin_tanka")))    '8月_計算用__仕入平均単価

        paramList.Add(MakeParam("@8gatu_keisanyou_koj_hantei_ritu", SqlDbType.Decimal, 4, drValue("8gatu_keisanyou_koj_hantei_ritu")))          '8月_計算用_工事判定率
        paramList.Add(MakeParam("@8gatu_keisanyou_koj_jyuchuu_ritu", SqlDbType.Decimal, 4, drValue("8gatu_keisanyou_koj_jyuchuu_ritu")))        '8月_計算用_工事受注率
        paramList.Add(MakeParam("@8gatu_keisanyou_tyoku_koj_ritu", SqlDbType.Decimal, 4, drValue("8gatu_keisanyou_tyoku_koj_ritu")))            '8月_計算用_直工事率
        paramList.Add(MakeParam("@8gatu_keikaku_kensuu", SqlDbType.BigInt, 12, drValue("8gatu_keikaku_kensuu")))        '8月_計画件数
        paramList.Add(MakeParam("@8gatu_keikaku_kingaku", SqlDbType.BigInt, 12, drValue("8gatu_keikaku_kingaku")))      '8月_計画金額
        paramList.Add(MakeParam("@8gatu_keikaku_arari", SqlDbType.BigInt, 12, drValue("8gatu_keikaku_arari")))          '8月_計画粗利

        paramList.Add(MakeParam("@9gatu_keisanyou_uri_heikin_tanka", SqlDbType.BigInt, 12, drValue("9gatu_keisanyou_uri_heikin_tanka")))        '9月_計算用__売上平均単価
        paramList.Add(MakeParam("@9gatu_keisanyou_siire_heikin_tanka", SqlDbType.BigInt, 12, drValue("9gatu_keisanyou_siire_heikin_tanka")))    '9月_計算用__仕入平均単価

        paramList.Add(MakeParam("@9gatu_keisanyou_koj_hantei_ritu", SqlDbType.Decimal, 4, drValue("9gatu_keisanyou_koj_hantei_ritu")))          '9月_計算用_工事判定率
        paramList.Add(MakeParam("@9gatu_keisanyou_koj_jyuchuu_ritu", SqlDbType.Decimal, 4, drValue("9gatu_keisanyou_koj_jyuchuu_ritu")))        '9月_計算用_工事受注率
        paramList.Add(MakeParam("@9gatu_keisanyou_tyoku_koj_ritu", SqlDbType.Decimal, 4, drValue("9gatu_keisanyou_tyoku_koj_ritu")))            '9月_計算用_直工事率
        paramList.Add(MakeParam("@9gatu_keikaku_kensuu", SqlDbType.BigInt, 12, drValue("9gatu_keikaku_kensuu")))        '9月_計画件数
        paramList.Add(MakeParam("@9gatu_keikaku_kingaku", SqlDbType.BigInt, 12, drValue("9gatu_keikaku_kingaku")))      '9月_計画金額
        paramList.Add(MakeParam("@9gatu_keikaku_arari", SqlDbType.BigInt, 12, drValue("9gatu_keikaku_arari")))          '9月_計画粗利

        paramList.Add(MakeParam("@10gatu_keisanyou_uri_heikin_tanka", SqlDbType.BigInt, 12, drValue("10gatu_keisanyou_uri_heikin_tanka")))        '10月_計算用__売上平均単価
        paramList.Add(MakeParam("@10gatu_keisanyou_siire_heikin_tanka", SqlDbType.BigInt, 12, drValue("10gatu_keisanyou_siire_heikin_tanka")))    '10月_計算用__仕入平均単価

        paramList.Add(MakeParam("@10gatu_keisanyou_koj_hantei_ritu", SqlDbType.Decimal, 4, drValue("10gatu_keisanyou_koj_hantei_ritu")))        '10月_計算用_工事判定率
        paramList.Add(MakeParam("@10gatu_keisanyou_koj_jyuchuu_ritu", SqlDbType.Decimal, 4, drValue("10gatu_keisanyou_koj_jyuchuu_ritu")))      '10月_計算用_工事受注率
        paramList.Add(MakeParam("@10gatu_keisanyou_tyoku_koj_ritu", SqlDbType.Decimal, 4, drValue("10gatu_keisanyou_tyoku_koj_ritu")))          '10月_計算用_直工事率
        paramList.Add(MakeParam("@10gatu_keikaku_kensuu", SqlDbType.BigInt, 12, drValue("10gatu_keikaku_kensuu")))      '10月_計画件数
        paramList.Add(MakeParam("@10gatu_keikaku_kingaku", SqlDbType.BigInt, 12, drValue("10gatu_keikaku_kingaku")))    '10月_計画金額
        paramList.Add(MakeParam("@10gatu_keikaku_arari", SqlDbType.BigInt, 12, drValue("10gatu_keikaku_arari")))        '10月_計画粗利

        paramList.Add(MakeParam("@11gatu_keisanyou_uri_heikin_tanka", SqlDbType.BigInt, 12, drValue("11gatu_keisanyou_uri_heikin_tanka")))        '11月_計算用__売上平均単価
        paramList.Add(MakeParam("@11gatu_keisanyou_siire_heikin_tanka", SqlDbType.BigInt, 12, drValue("11gatu_keisanyou_siire_heikin_tanka")))    '11月_計算用__仕入平均単価

        paramList.Add(MakeParam("@11gatu_keisanyou_koj_hantei_ritu", SqlDbType.Decimal, 4, drValue("11gatu_keisanyou_koj_hantei_ritu")))        '11月_計算用_工事判定率
        paramList.Add(MakeParam("@11gatu_keisanyou_koj_jyuchuu_ritu", SqlDbType.Decimal, 4, drValue("11gatu_keisanyou_koj_jyuchuu_ritu")))      '11月_計算用_工事受注率
        paramList.Add(MakeParam("@11gatu_keisanyou_tyoku_koj_ritu", SqlDbType.Decimal, 4, drValue("11gatu_keisanyou_tyoku_koj_ritu")))          '11月_計算用_直工事率
        paramList.Add(MakeParam("@11gatu_keikaku_kensuu", SqlDbType.BigInt, 12, drValue("11gatu_keikaku_kensuu")))      '11月_計画件数
        paramList.Add(MakeParam("@11gatu_keikaku_kingaku", SqlDbType.BigInt, 12, drValue("11gatu_keikaku_kingaku")))    '11月_計画金額
        paramList.Add(MakeParam("@11gatu_keikaku_arari", SqlDbType.BigInt, 12, drValue("11gatu_keikaku_arari")))        '11月_計画粗利

        paramList.Add(MakeParam("@12gatu_keisanyou_uri_heikin_tanka", SqlDbType.BigInt, 12, drValue("12gatu_keisanyou_uri_heikin_tanka")))        '12月_計算用__売上平均単価
        paramList.Add(MakeParam("@12gatu_keisanyou_siire_heikin_tanka", SqlDbType.BigInt, 12, drValue("12gatu_keisanyou_siire_heikin_tanka")))    '12月_計算用__仕入平均単価

        paramList.Add(MakeParam("@12gatu_keisanyou_koj_hantei_ritu", SqlDbType.Decimal, 4, drValue("12gatu_keisanyou_koj_hantei_ritu")))        '12月_計算用_工事判定率
        paramList.Add(MakeParam("@12gatu_keisanyou_koj_jyuchuu_ritu", SqlDbType.Decimal, 4, drValue("12gatu_keisanyou_koj_jyuchuu_ritu")))      '12月_計算用_工事受注率
        paramList.Add(MakeParam("@12gatu_keisanyou_tyoku_koj_ritu", SqlDbType.Decimal, 4, drValue("12gatu_keisanyou_tyoku_koj_ritu")))          '12月_計算用_直工事率
        paramList.Add(MakeParam("@12gatu_keikaku_kensuu", SqlDbType.BigInt, 12, drValue("12gatu_keikaku_kensuu")))      '12月_計画件数
        paramList.Add(MakeParam("@12gatu_keikaku_kingaku", SqlDbType.BigInt, 12, drValue("12gatu_keikaku_kingaku")))    '12月_計画金額
        paramList.Add(MakeParam("@12gatu_keikaku_arari", SqlDbType.BigInt, 12, drValue("12gatu_keikaku_arari")))        '12月_計画粗利

        paramList.Add(MakeParam("@1gatu_keisanyou_uri_heikin_tanka", SqlDbType.BigInt, 12, drValue("1gatu_keisanyou_uri_heikin_tanka")))        '1月_計算用__売上平均単価
        paramList.Add(MakeParam("@1gatu_keisanyou_siire_heikin_tanka", SqlDbType.BigInt, 12, drValue("1gatu_keisanyou_siire_heikin_tanka")))    '1月_計算用__仕入平均単価

        paramList.Add(MakeParam("@1gatu_keisanyou_koj_hantei_ritu", SqlDbType.Decimal, 4, drValue("1gatu_keisanyou_koj_hantei_ritu")))          '1月_計算用_工事判定率
        paramList.Add(MakeParam("@1gatu_keisanyou_koj_jyuchuu_ritu", SqlDbType.Decimal, 4, drValue("1gatu_keisanyou_koj_jyuchuu_ritu")))        '1月_計算用_工事受注率
        paramList.Add(MakeParam("@1gatu_keisanyou_tyoku_koj_ritu", SqlDbType.Decimal, 4, drValue("1gatu_keisanyou_tyoku_koj_ritu")))            '1月_計算用_直工事率
        paramList.Add(MakeParam("@1gatu_keikaku_kensuu", SqlDbType.BigInt, 12, drValue("1gatu_keikaku_kensuu")))        '1月_計画件数
        paramList.Add(MakeParam("@1gatu_keikaku_kingaku", SqlDbType.BigInt, 12, drValue("1gatu_keikaku_kingaku")))      '1月_計画金額
        paramList.Add(MakeParam("@1gatu_keikaku_arari", SqlDbType.BigInt, 12, drValue("1gatu_keikaku_arari")))          '1月_計画粗利

        paramList.Add(MakeParam("@2gatu_keisanyou_uri_heikin_tanka", SqlDbType.BigInt, 12, drValue("2gatu_keisanyou_uri_heikin_tanka")))        '2月_計算用__売上平均単価
        paramList.Add(MakeParam("@2gatu_keisanyou_siire_heikin_tanka", SqlDbType.BigInt, 12, drValue("2gatu_keisanyou_siire_heikin_tanka")))    '2月_計算用__仕入平均単価

        paramList.Add(MakeParam("@2gatu_keisanyou_koj_hantei_ritu", SqlDbType.Decimal, 4, drValue("2gatu_keisanyou_koj_hantei_ritu")))          '2月_計算用_工事判定率
        paramList.Add(MakeParam("@2gatu_keisanyou_koj_jyuchuu_ritu", SqlDbType.Decimal, 4, drValue("2gatu_keisanyou_koj_jyuchuu_ritu")))        '2月_計算用_工事受注率
        paramList.Add(MakeParam("@2gatu_keisanyou_tyoku_koj_ritu", SqlDbType.Decimal, 4, drValue("2gatu_keisanyou_tyoku_koj_ritu")))            '2月_計算用_直工事率
        paramList.Add(MakeParam("@2gatu_keikaku_kensuu", SqlDbType.BigInt, 12, drValue("2gatu_keikaku_kensuu")))        '2月_計画件数
        paramList.Add(MakeParam("@2gatu_keikaku_kingaku", SqlDbType.BigInt, 12, drValue("2gatu_keikaku_kingaku")))      '2月_計画金額
        paramList.Add(MakeParam("@2gatu_keikaku_arari", SqlDbType.BigInt, 12, drValue("2gatu_keikaku_arari")))          '2月_計画粗利

        paramList.Add(MakeParam("@3gatu_keisanyou_uri_heikin_tanka", SqlDbType.BigInt, 12, drValue("3gatu_keisanyou_uri_heikin_tanka")))        '3月_計算用__売上平均単価
        paramList.Add(MakeParam("@3gatu_keisanyou_siire_heikin_tanka", SqlDbType.BigInt, 12, drValue("3gatu_keisanyou_siire_heikin_tanka")))    '3月_計算用__仕入平均単価

        paramList.Add(MakeParam("@3gatu_keisanyou_koj_hantei_ritu", SqlDbType.Decimal, 4, drValue("3gatu_keisanyou_koj_hantei_ritu")))          '3月_計算用_工事判定率
        paramList.Add(MakeParam("@3gatu_keisanyou_koj_jyuchuu_ritu", SqlDbType.Decimal, 4, drValue("3gatu_keisanyou_koj_jyuchuu_ritu")))        '3月_計算用_工事受注率
        paramList.Add(MakeParam("@3gatu_keisanyou_tyoku_koj_ritu", SqlDbType.Decimal, 4, drValue("3gatu_keisanyou_tyoku_koj_ritu")))            '3月_計算用_直工事率
        paramList.Add(MakeParam("@3gatu_keikaku_kensuu", SqlDbType.BigInt, 12, drValue("3gatu_keikaku_kensuu")))        '3月_計画件数
        paramList.Add(MakeParam("@3gatu_keikaku_kingaku", SqlDbType.BigInt, 12, drValue("3gatu_keikaku_kingaku")))      '3月_計画金額
        paramList.Add(MakeParam("@3gatu_keikaku_arari", SqlDbType.BigInt, 12, drValue("3gatu_keikaku_arari")))          '3月_計画粗利
        '2013/10/14 李宇追加　↓
        paramList.Add(MakeParam("@UCCRPDEV", SqlDbType.VarChar, 2, drValue("UCCRPDEV")))      '報連相_顧客区分
        paramList.Add(MakeParam("@UCCRPSEQ", SqlDbType.Decimal, 10, drValue("UCCRPSEQ")))     '報連相_顧客コードSEQ
        '2013/10/14 李宇追加　↑
        paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, strLoginUserId))                           '登録ログインユーザーID

        '登録実行
        Call GetDebugSql(sqlBuffer.ToString, paramList)
        insCount = SQLHelper.ExecuteNonQuery(ManagerDA.Connection, CommandType.Text, sqlBuffer.ToString, paramList.ToArray())

        If insCount > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' 予定見込管理テーブルへのデータ登録
    ''' </summary>
    ''' <param name="drValue">登録データレコード</param>
    ''' <param name="strLoginUserId">ログインユーザーID</param>
    ''' <param name="strSyoriDatetime">処理日時</param>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    ''' <history>																
    ''' <para>2012/11/27 P-44979 曹敬仁 新規作成</para>																															
    ''' </history>
    Public Function InsYoteiMikomiKanriData(ByVal drValue As DataRow, _
                                            ByVal strLoginUserId As String, _
                                            ByVal strSyoriDatetime As String) As Boolean
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, drValue, strLoginUserId, strSyoriDatetime)

        Dim sqlBuffer As New System.Text.StringBuilder
        Dim insCount As Integer

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With sqlBuffer
            .AppendLine(" INSERT INTO t_yotei_mikomi_kanri WITH(UPDLOCK)( ")
            .AppendLine("     [keikaku_nendo], ")                            '計画_年度
            .AppendLine("     [add_datetime], ")                             '登録日時
            .AppendLine("     [kameiten_cd], ")                              '加盟店ｺｰﾄﾞ
            .AppendLine("     [keikaku_kanri_syouhin_cd], ")                 '計画管理_商品コード
            .AppendLine("     [kameiten_mei], ")                             '加盟店名
            .AppendLine("     [bunbetu_cd], ")                               '分別コード
            .AppendLine("     [4gatu_mikomi_kensuu], ")                      '4月_見込件数
            .AppendLine("     [4gatu_mikomi_kingaku], ")                     '4月_見込金額
            .AppendLine("     [4gatu_mikomi_arari], ")                       '4月_見込粗利
            .AppendLine("     [5gatu_mikomi_kensuu], ")                      '5月_見込件数
            .AppendLine("     [5gatu_mikomi_kingaku], ")                     '5月_見込金額
            .AppendLine("     [5gatu_mikomi_arari], ")                       '5月_見込粗利
            .AppendLine("     [6gatu_mikomi_kensuu], ")                      '6月_見込件数
            .AppendLine("     [6gatu_mikomi_kingaku], ")                     '6月_見込金額
            .AppendLine("     [6gatu_mikomi_arari], ")                       '6月_見込粗利
            .AppendLine("     [7gatu_mikomi_kensuu], ")                      '7月_見込件数
            .AppendLine("     [7gatu_mikomi_kingaku], ")                     '7月_見込金額
            .AppendLine("     [7gatu_mikomi_arari], ")                       '7月_見込粗利
            .AppendLine("     [8gatu_mikomi_kensuu], ")                      '8月_見込件数
            .AppendLine("     [8gatu_mikomi_kingaku], ")                     '8月_見込金額
            .AppendLine("     [8gatu_mikomi_arari], ")                       '8月_見込粗利
            .AppendLine("     [9gatu_mikomi_kensuu], ")                      '9月_見込件数
            .AppendLine("     [9gatu_mikomi_kingaku], ")                     '9月_見込金額
            .AppendLine("     [9gatu_mikomi_arari], ")                       '9月_見込粗利
            .AppendLine("     [10gatu_mikomi_kensuu], ")                     '10月_見込件数
            .AppendLine("     [10gatu_mikomi_kingaku], ")                    '10月_見込金額
            .AppendLine("     [10gatu_mikomi_arari], ")                      '10月_見込粗利
            .AppendLine("     [11gatu_mikomi_kensuu], ")                     '11月_見込件数
            .AppendLine("     [11gatu_mikomi_kingaku], ")                    '11月_見込金額
            .AppendLine("     [11gatu_mikomi_arari], ")                      '11月_見込粗利
            .AppendLine("     [12gatu_mikomi_kensuu], ")                     '12月_見込件数
            .AppendLine("     [12gatu_mikomi_kingaku], ")                    '12月_見込金額
            .AppendLine("     [12gatu_mikomi_arari], ")                      '12月_見込粗利
            .AppendLine("     [1gatu_mikomi_kensuu], ")                      '1月_見込件数
            .AppendLine("     [1gatu_mikomi_kingaku], ")                     '1月_見込金額
            .AppendLine("     [1gatu_mikomi_arari], ")                       '1月_見込粗利
            .AppendLine("     [2gatu_mikomi_kensuu], ")                      '2月_見込件数
            .AppendLine("     [2gatu_mikomi_kingaku], ")                     '2月_見込金額
            .AppendLine("     [2gatu_mikomi_arari], ")                       '2月_見込粗利
            .AppendLine("     [3gatu_mikomi_kensuu], ")                      '3月_見込件数
            .AppendLine("     [3gatu_mikomi_kingaku], ")                     '3月_見込金額
            .AppendLine("     [3gatu_mikomi_arari], ")                       '3月_見込粗利
            .AppendLine("     [add_login_user_id], ")                        '登録ログインユーザーID
            .AppendLine("     [upd_login_user_id], ")                        '更新ログインユーザーID
            .AppendLine("     [upd_datetime] ")                              '更新日時
            .AppendLine("    )VALUES( ")
            .AppendLine("     @keikaku_nendo, ")
            .AppendLine("     @add_datetime, ")
            .AppendLine("     @kameiten_cd, ")
            .AppendLine("     @keikaku_kanri_syouhin_cd, ")
            .AppendLine("     @kameiten_mei, ")
            .AppendLine("     @bunbetu_cd, ")
            .AppendLine("     @4gatu_mikomi_kensuu, ")
            .AppendLine("     @4gatu_mikomi_kingaku, ")
            .AppendLine("     @4gatu_mikomi_arari, ")
            .AppendLine("     @5gatu_mikomi_kensuu, ")
            .AppendLine("     @5gatu_mikomi_kingaku, ")
            .AppendLine("     @5gatu_mikomi_arari, ")
            .AppendLine("     @6gatu_mikomi_kensuu, ")
            .AppendLine("     @6gatu_mikomi_kingaku, ")
            .AppendLine("     @6gatu_mikomi_arari, ")
            .AppendLine("     @7gatu_mikomi_kensuu, ")
            .AppendLine("     @7gatu_mikomi_kingaku, ")
            .AppendLine("     @7gatu_mikomi_arari, ")
            .AppendLine("     @8gatu_mikomi_kensuu, ")
            .AppendLine("     @8gatu_mikomi_kingaku, ")
            .AppendLine("     @8gatu_mikomi_arari, ")
            .AppendLine("     @9gatu_mikomi_kensuu, ")
            .AppendLine("     @9gatu_mikomi_kingaku, ")
            .AppendLine("     @9gatu_mikomi_arari, ")
            .AppendLine("     @10gatu_mikomi_kensuu, ")
            .AppendLine("     @10gatu_mikomi_kingaku, ")
            .AppendLine("     @10gatu_mikomi_arari, ")
            .AppendLine("     @11gatu_mikomi_kensuu, ")
            .AppendLine("     @11gatu_mikomi_kingaku, ")
            .AppendLine("     @11gatu_mikomi_arari, ")
            .AppendLine("     @12gatu_mikomi_kensuu, ")
            .AppendLine("     @12gatu_mikomi_kingaku, ")
            .AppendLine("     @12gatu_mikomi_arari, ")
            .AppendLine("     @1gatu_mikomi_kensuu, ")
            .AppendLine("     @1gatu_mikomi_kingaku, ")
            .AppendLine("     @1gatu_mikomi_arari, ")
            .AppendLine("     @2gatu_mikomi_kensuu, ")
            .AppendLine("     @2gatu_mikomi_kingaku, ")
            .AppendLine("     @2gatu_mikomi_arari, ")
            .AppendLine("     @3gatu_mikomi_kensuu, ")
            .AppendLine("     @3gatu_mikomi_kingaku, ")
            .AppendLine("     @3gatu_mikomi_arari, ")
            .AppendLine("     @add_login_user_id, ")
            .AppendLine("     @add_login_user_id, ")
            .AppendLine("     GETDATE() ")
            .AppendLine(" 	) ")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@keikaku_nendo", SqlDbType.Char, 4, drValue("keikaku_nendo")))                       '計画年度(YYYY)
        paramList.Add(MakeParam("@add_datetime", SqlDbType.DateTime, 30, Convert.ToDateTime(CDate(strSyoriDatetime))))  '登録日時
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 8, drValue("kameiten_cd")))                        '加盟店ｺｰﾄﾞ
        paramList.Add(MakeParam("@keikaku_kanri_syouhin_cd", SqlDbType.VarChar, 8, drValue("keikaku_kanri_syouhin_cd")))       '計画管理_商品コード
        paramList.Add(MakeParam("@kameiten_mei", SqlDbType.VarChar, 40, drValue("kameiten_mei")))                     '加盟店名
        paramList.Add(MakeParam("@bunbetu_cd", SqlDbType.VarChar, 3, drValue("bunbetu_cd")))                          '分別コード
        paramList.Add(MakeParam("@4gatu_mikomi_kensuu", SqlDbType.BigInt, 12, drValue("4gatu_mikomi_kensuu")))        '4月_見込件数
        paramList.Add(MakeParam("@4gatu_mikomi_kingaku", SqlDbType.BigInt, 12, drValue("4gatu_mikomi_kingaku")))      '4月_見込金額
        paramList.Add(MakeParam("@4gatu_mikomi_arari", SqlDbType.BigInt, 12, drValue("4gatu_mikomi_arari")))          '4月_見込粗利
        paramList.Add(MakeParam("@5gatu_mikomi_kensuu", SqlDbType.BigInt, 12, drValue("5gatu_mikomi_kensuu")))        '5月_見込件数
        paramList.Add(MakeParam("@5gatu_mikomi_kingaku", SqlDbType.BigInt, 12, drValue("5gatu_mikomi_kingaku")))      '5月_見込金額
        paramList.Add(MakeParam("@5gatu_mikomi_arari", SqlDbType.BigInt, 12, drValue("5gatu_mikomi_arari")))          '5月_見込粗利
        paramList.Add(MakeParam("@6gatu_mikomi_kensuu", SqlDbType.BigInt, 12, drValue("6gatu_mikomi_kensuu")))        '6月_見込件数
        paramList.Add(MakeParam("@6gatu_mikomi_kingaku", SqlDbType.BigInt, 12, drValue("6gatu_mikomi_kingaku")))      '6月_見込金額
        paramList.Add(MakeParam("@6gatu_mikomi_arari", SqlDbType.BigInt, 12, drValue("6gatu_mikomi_arari")))          '6月_見込粗利
        paramList.Add(MakeParam("@7gatu_mikomi_kensuu", SqlDbType.BigInt, 12, drValue("7gatu_mikomi_kensuu")))        '7月_見込件数
        paramList.Add(MakeParam("@7gatu_mikomi_kingaku", SqlDbType.BigInt, 12, drValue("7gatu_mikomi_kingaku")))      '7月_見込金額
        paramList.Add(MakeParam("@7gatu_mikomi_arari", SqlDbType.BigInt, 12, drValue("7gatu_mikomi_arari")))          '7月_見込粗利
        paramList.Add(MakeParam("@8gatu_mikomi_kensuu", SqlDbType.BigInt, 12, drValue("8gatu_mikomi_kensuu")))        '8月_見込件数
        paramList.Add(MakeParam("@8gatu_mikomi_kingaku", SqlDbType.BigInt, 12, drValue("8gatu_mikomi_kingaku")))      '8月_見込金額
        paramList.Add(MakeParam("@8gatu_mikomi_arari", SqlDbType.BigInt, 12, drValue("8gatu_mikomi_arari")))          '8月_見込粗利
        paramList.Add(MakeParam("@9gatu_mikomi_kensuu", SqlDbType.BigInt, 12, drValue("9gatu_mikomi_kensuu")))        '9月_見込件数
        paramList.Add(MakeParam("@9gatu_mikomi_kingaku", SqlDbType.BigInt, 12, drValue("9gatu_mikomi_kingaku")))      '9月_見込金額
        paramList.Add(MakeParam("@9gatu_mikomi_arari", SqlDbType.BigInt, 12, drValue("9gatu_mikomi_arari")))          '9月_見込粗利
        paramList.Add(MakeParam("@10gatu_mikomi_kensuu", SqlDbType.BigInt, 12, drValue("10gatu_mikomi_kensuu")))      '10月_見込件数
        paramList.Add(MakeParam("@10gatu_mikomi_kingaku", SqlDbType.BigInt, 12, drValue("10gatu_mikomi_kingaku")))    '10月_見込金額
        paramList.Add(MakeParam("@10gatu_mikomi_arari", SqlDbType.BigInt, 12, drValue("10gatu_mikomi_arari")))        '10月_見込粗利
        paramList.Add(MakeParam("@11gatu_mikomi_kensuu", SqlDbType.BigInt, 12, drValue("11gatu_mikomi_kensuu")))      '11月_見込件数
        paramList.Add(MakeParam("@11gatu_mikomi_kingaku", SqlDbType.BigInt, 12, drValue("11gatu_mikomi_kingaku")))    '11月_見込金額
        paramList.Add(MakeParam("@11gatu_mikomi_arari", SqlDbType.BigInt, 12, drValue("11gatu_mikomi_arari")))        '11月_見込粗利
        paramList.Add(MakeParam("@12gatu_mikomi_kensuu", SqlDbType.BigInt, 12, drValue("12gatu_mikomi_kensuu")))      '12月_見込件数
        paramList.Add(MakeParam("@12gatu_mikomi_kingaku", SqlDbType.BigInt, 12, drValue("12gatu_mikomi_kingaku")))    '12月_見込金額
        paramList.Add(MakeParam("@12gatu_mikomi_arari", SqlDbType.BigInt, 12, drValue("12gatu_mikomi_arari")))        '12月_見込粗利
        paramList.Add(MakeParam("@1gatu_mikomi_kensuu", SqlDbType.BigInt, 12, drValue("1gatu_mikomi_kensuu")))        '1月_見込件数
        paramList.Add(MakeParam("@1gatu_mikomi_kingaku", SqlDbType.BigInt, 12, drValue("1gatu_mikomi_kingaku")))      '1月_見込金額
        paramList.Add(MakeParam("@1gatu_mikomi_arari", SqlDbType.BigInt, 12, drValue("1gatu_mikomi_arari")))          '1月_見込粗利
        paramList.Add(MakeParam("@2gatu_mikomi_kensuu", SqlDbType.BigInt, 12, drValue("2gatu_mikomi_kensuu")))        '2月_見込件数
        paramList.Add(MakeParam("@2gatu_mikomi_kingaku", SqlDbType.BigInt, 12, drValue("2gatu_mikomi_kingaku")))      '2月_見込金額
        paramList.Add(MakeParam("@2gatu_mikomi_arari", SqlDbType.BigInt, 12, drValue("2gatu_mikomi_arari")))          '2月_見込粗利
        paramList.Add(MakeParam("@3gatu_mikomi_kensuu", SqlDbType.BigInt, 12, drValue("3gatu_mikomi_kensuu")))        '3月_見込件数
        paramList.Add(MakeParam("@3gatu_mikomi_kingaku", SqlDbType.BigInt, 12, drValue("3gatu_mikomi_kingaku")))      '3月_見込金額
        paramList.Add(MakeParam("@3gatu_mikomi_arari", SqlDbType.BigInt, 12, drValue("3gatu_mikomi_arari")))          '3月_見込粗利
        paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, strLoginUserId))                         '登録ログインユーザーID

        '登録実行
        insCount = SQLHelper.ExecuteNonQuery(ManagerDA.Connection, CommandType.Text, sqlBuffer.ToString, paramList.ToArray())

        If insCount > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' FC用計画管理テーブルへのデータ登録
    ''' </summary>
    ''' <param name="drValue">登録データレコード</param>
    ''' <param name="strLoginUserId">ログインユーザーID</param>
    ''' <param name="strSyoriDatetime">処理日時</param>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    ''' <history>																
    ''' <para>2012/11/30 P-44979 曹敬仁 新規作成</para>																															
    ''' </history>
    Public Function InsFCKeikakuKanriData(ByVal drValue As DataRow, _
                                          ByVal strLoginUserId As String, _
                                          ByVal strSyoriDatetime As String) As Boolean
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, drValue, strLoginUserId, strSyoriDatetime)

        Dim sqlBuffer As New System.Text.StringBuilder
        Dim insCount As Integer

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With sqlBuffer
            .AppendLine("INSERT INTO  ")
            .AppendLine("	t_fc_keikaku_kanri WITH(UPDLOCK)")
            .AppendLine("	( ")
            .AppendLine("    [keikaku_nendo], ")                           '計画_年度
            .AppendLine("    [add_datetime], ")                            '登録日時
            .AppendLine("    [busyo_cd], ")                                '部署ｺｰﾄﾞ
            .AppendLine("    [keikaku_kanri_syouhin_cd], ")                '計画管理_商品コード
            .AppendLine("    [siten_mei], ")                               '支店名
            .AppendLine("    [bunbetu_cd], ")                              '分別コード

            .AppendLine("     [4gatu_keisanyou_uri_heikin_tanka], ")          '4月_計算用__売上平均単価
            .AppendLine("     [4gatu_keisanyou_siire_heikin_tanka], ")        '4月_計算用__仕入平均単価

            .AppendLine("    [4gatu_keisanyou_koj_hantei_ritu], ")         '4月_計算用_工事判定率
            .AppendLine("    [4gatu_keisanyou_koj_jyuchuu_ritu], ")        '4月_計算用_工事受注率
            .AppendLine("    [4gatu_keisanyou_tyoku_koj_ritu], ")          '4月_計算用_直工事率
            .AppendLine("    [4gatu_keikaku_kensuu], ")                    '4月_計画件数
            .AppendLine("    [4gatu_keikaku_kingaku], ")                   '4月_計画金額
            .AppendLine("    [4gatu_keikaku_arari], ")                     '4月_計画粗利

            .AppendLine("     [5gatu_keisanyou_uri_heikin_tanka], ")          '5月_計算用__売上平均単価
            .AppendLine("     [5gatu_keisanyou_siire_heikin_tanka], ")        '5月_計算用__仕入平均単価

            .AppendLine("    [5gatu_keisanyou_koj_hantei_ritu], ")         '5月_計算用_工事判定率
            .AppendLine("    [5gatu_keisanyou_koj_jyuchuu_ritu], ")        '5月_計算用_工事受注率
            .AppendLine("    [5gatu_keisanyou_tyoku_koj_ritu], ")          '5月_計算用_直工事率
            .AppendLine("    [5gatu_keikaku_kensuu], ")                    '5月_計画件数
            .AppendLine("    [5gatu_keikaku_kingaku], ")                   '5月_計画金額
            .AppendLine("    [5gatu_keikaku_arari], ")                     '5月_計画粗利

            .AppendLine("     [6gatu_keisanyou_uri_heikin_tanka], ")          '6月_計算用__売上平均単価
            .AppendLine("     [6gatu_keisanyou_siire_heikin_tanka], ")        '6月_計算用__仕入平均単価

            .AppendLine("    [6gatu_keisanyou_koj_hantei_ritu], ")         '6月_計算用_工事判定率
            .AppendLine("    [6gatu_keisanyou_koj_jyuchuu_ritu], ")        '6月_計算用_工事受注率
            .AppendLine("    [6gatu_keisanyou_tyoku_koj_ritu], ")          '6月_計算用_直工事率
            .AppendLine("    [6gatu_keikaku_kensuu], ")                    '6月_計画件数
            .AppendLine("    [6gatu_keikaku_kingaku], ")                   '6月_計画金額
            .AppendLine("    [6gatu_keikaku_arari], ")                     '6月_計画粗利

            .AppendLine("     [7gatu_keisanyou_uri_heikin_tanka], ")          '7月_計算用__売上平均単価
            .AppendLine("     [7gatu_keisanyou_siire_heikin_tanka], ")        '7月_計算用__仕入平均単価

            .AppendLine("    [7gatu_keisanyou_koj_hantei_ritu], ")         '7月_計算用_工事判定率
            .AppendLine("    [7gatu_keisanyou_koj_jyuchuu_ritu], ")        '7月_計算用_工事受注率
            .AppendLine("    [7gatu_keisanyou_tyoku_koj_ritu], ")          '7月_計算用_直工事率
            .AppendLine("    [7gatu_keikaku_kensuu], ")                    '7月_計画件数
            .AppendLine("    [7gatu_keikaku_kingaku], ")                   '7月_計画金額
            .AppendLine("    [7gatu_keikaku_arari], ")                     '7月_計画粗利

            .AppendLine("     [8gatu_keisanyou_uri_heikin_tanka], ")          '8月_計算用__売上平均単価
            .AppendLine("     [8gatu_keisanyou_siire_heikin_tanka], ")        '8月_計算用__仕入平均単価

            .AppendLine("    [8gatu_keisanyou_koj_hantei_ritu], ")         '8月_計算用_工事判定率
            .AppendLine("    [8gatu_keisanyou_koj_jyuchuu_ritu], ")        '8月_計算用_工事受注率
            .AppendLine("    [8gatu_keisanyou_tyoku_koj_ritu], ")          '8月_計算用_直工事率
            .AppendLine("    [8gatu_keikaku_kensuu], ")                    '8月_計画件数
            .AppendLine("    [8gatu_keikaku_kingaku], ")                   '8月_計画金額
            .AppendLine("    [8gatu_keikaku_arari], ")                     '8月_計画粗利

            .AppendLine("     [9gatu_keisanyou_uri_heikin_tanka], ")          '9月_計算用__売上平均単価
            .AppendLine("     [9gatu_keisanyou_siire_heikin_tanka], ")        '9月_計算用__仕入平均単価

            .AppendLine("    [9gatu_keisanyou_koj_hantei_ritu], ")         '9月_計算用_工事判定率
            .AppendLine("    [9gatu_keisanyou_koj_jyuchuu_ritu], ")        '9月_計算用_工事受注率
            .AppendLine("    [9gatu_keisanyou_tyoku_koj_ritu], ")          '9月_計算用_直工事率
            .AppendLine("    [9gatu_keikaku_kensuu], ")                    '9月_計画件数
            .AppendLine("    [9gatu_keikaku_kingaku], ")                   '9月_計画金額
            .AppendLine("    [9gatu_keikaku_arari], ")                     '9月_計画粗利

            .AppendLine("     [10gatu_keisanyou_uri_heikin_tanka], ")          '10月_計算用__売上平均単価
            .AppendLine("     [10gatu_keisanyou_siire_heikin_tanka], ")        '10月_計算用__仕入平均単価

            .AppendLine("    [10gatu_keisanyou_koj_hantei_ritu], ")        '10月_計算用_工事判定率
            .AppendLine("    [10gatu_keisanyou_koj_jyuchuu_ritu], ")       '10月_計算用_工事受注率
            .AppendLine("    [10gatu_keisanyou_tyoku_koj_ritu], ")         '10月_計算用_直工事率
            .AppendLine("    [10gatu_keikaku_kensuu], ")                   '10月_計画件数
            .AppendLine("    [10gatu_keikaku_kingaku], ")                  '10月_計画金額
            .AppendLine("    [10gatu_keikaku_arari], ")                    '10月_計画粗利

            .AppendLine("     [11gatu_keisanyou_uri_heikin_tanka], ")          '11月_計算用__売上平均単価
            .AppendLine("     [11gatu_keisanyou_siire_heikin_tanka], ")        '11月_計算用__仕入平均単価

            .AppendLine("    [11gatu_keisanyou_koj_hantei_ritu], ")        '11月_計算用_工事判定率
            .AppendLine("    [11gatu_keisanyou_koj_jyuchuu_ritu], ")       '11月_計算用_工事受注率
            .AppendLine("    [11gatu_keisanyou_tyoku_koj_ritu], ")         '11月_計算用_直工事率
            .AppendLine("    [11gatu_keikaku_kensuu], ")                   '11月_計画件数
            .AppendLine("    [11gatu_keikaku_kingaku], ")                  '11月_計画金額
            .AppendLine("    [11gatu_keikaku_arari], ")                    '11月_計画粗利

            .AppendLine("     [12gatu_keisanyou_uri_heikin_tanka], ")          '12月_計算用__売上平均単価
            .AppendLine("     [12gatu_keisanyou_siire_heikin_tanka], ")        '12月_計算用__仕入平均単価

            .AppendLine("    [12gatu_keisanyou_koj_hantei_ritu], ")        '12月_計算用_工事判定率
            .AppendLine("    [12gatu_keisanyou_koj_jyuchuu_ritu], ")       '12月_計算用_工事受注率
            .AppendLine("    [12gatu_keisanyou_tyoku_koj_ritu], ")         '12月_計算用_直工事率
            .AppendLine("    [12gatu_keikaku_kensuu], ")                   '12月_計画件数
            .AppendLine("    [12gatu_keikaku_kingaku], ")                  '12月_計画金額
            .AppendLine("    [12gatu_keikaku_arari], ")                    '12月_計画粗利

            .AppendLine("     [1gatu_keisanyou_uri_heikin_tanka], ")          '1月_計算用__売上平均単価
            .AppendLine("     [1gatu_keisanyou_siire_heikin_tanka], ")        '1月_計算用__仕入平均単価

            .AppendLine("    [1gatu_keisanyou_koj_hantei_ritu], ")         '1月_計算用_工事判定率
            .AppendLine("    [1gatu_keisanyou_koj_jyuchuu_ritu], ")        '1月_計算用_工事受注率
            .AppendLine("    [1gatu_keisanyou_tyoku_koj_ritu], ")          '1月_計算用_直工事率
            .AppendLine("    [1gatu_keikaku_kensuu], ")                    '1月_計画件数
            .AppendLine("    [1gatu_keikaku_kingaku], ")                   '1月_計画金額
            .AppendLine("    [1gatu_keikaku_arari], ")                     '1月_計画粗利

            .AppendLine("     [2gatu_keisanyou_uri_heikin_tanka], ")          '2月_計算用__売上平均単価
            .AppendLine("     [2gatu_keisanyou_siire_heikin_tanka], ")        '2月_計算用__仕入平均単価

            .AppendLine("    [2gatu_keisanyou_koj_hantei_ritu], ")         '2月_計算用_工事判定率
            .AppendLine("    [2gatu_keisanyou_koj_jyuchuu_ritu], ")        '2月_計算用_工事受注率
            .AppendLine("    [2gatu_keisanyou_tyoku_koj_ritu], ")          '2月_計算用_直工事率
            .AppendLine("    [2gatu_keikaku_kensuu], ")                    '2月_計画件数
            .AppendLine("    [2gatu_keikaku_kingaku], ")                   '2月_計画金額
            .AppendLine("    [2gatu_keikaku_arari], ")                     '2月_計画粗利

            .AppendLine("     [3gatu_keisanyou_uri_heikin_tanka], ")          '3月_計算用__売上平均単価
            .AppendLine("     [3gatu_keisanyou_siire_heikin_tanka], ")        '3月_計算用__仕入平均単価

            .AppendLine("    [3gatu_keisanyou_koj_hantei_ritu], ")         '3月_計算用_工事判定率
            .AppendLine("    [3gatu_keisanyou_koj_jyuchuu_ritu], ")        '3月_計算用_工事受注率
            .AppendLine("    [3gatu_keisanyou_tyoku_koj_ritu], ")          '3月_計算用_直工事率
            .AppendLine("    [3gatu_keikaku_kensuu], ")                    '3月_計画件数
            .AppendLine("    [3gatu_keikaku_kingaku], ")                   '3月_計画金額
            .AppendLine("    [3gatu_keikaku_arari], ")                     '3月_計画粗利
            .AppendLine("    [keikaku_kakutei_flg], ")                     '計画確定FLG
            .AppendLine("    [keikaku_kakutei_id], ")                      '計画確定者ID
            .AppendLine("    [keikaku_kakutei_datetime], ")                '計画確定日時
            .AppendLine("    [kakutei_minaosi_id], ")                      '確定見直し者ID
            .AppendLine("    [kakutei_minaosi_datetime], ")                '確定見直し日時
            .AppendLine("    [keikaku_minaosi_flg], ")                     '計画見直しFLG
            .AppendLine("    [keikaku_huhen_flg], ")                       '計画値不変FLG
            .AppendLine("    [add_login_user_id], ")                       '登録ログインユーザーID
            .AppendLine("    [upd_login_user_id], ")                       '更新ログインユーザーID
            .AppendLine("    [upd_datetime] ")                             '更新日時
            .AppendLine("    )VALUES( ")
            .AppendLine("    @keikaku_nendo, ")
            .AppendLine("    @add_datetime, ")
            .AppendLine("    @busyo_cd, ")
            .AppendLine("    @keikaku_kanri_syouhin_cd, ")
            .AppendLine("    @siten_mei, ")
            .AppendLine("    @bunbetu_cd, ")

            .AppendLine("     @4gatu_keisanyou_uri_heikin_tanka, ")
            .AppendLine("     @4gatu_keisanyou_siire_heikin_tanka, ")

            .AppendLine("    @4gatu_keisanyou_koj_hantei_ritu, ")
            .AppendLine("    @4gatu_keisanyou_koj_jyuchuu_ritu, ")
            .AppendLine("    @4gatu_keisanyou_tyoku_koj_ritu, ")
            .AppendLine("    @4gatu_keikaku_kensuu, ")
            .AppendLine("    @4gatu_keikaku_kingaku, ")
            .AppendLine("    @4gatu_keikaku_arari, ")

            .AppendLine("     @5gatu_keisanyou_uri_heikin_tanka, ")
            .AppendLine("     @5gatu_keisanyou_siire_heikin_tanka, ")

            .AppendLine("    @5gatu_keisanyou_koj_hantei_ritu, ")
            .AppendLine("    @5gatu_keisanyou_koj_jyuchuu_ritu, ")
            .AppendLine("    @5gatu_keisanyou_tyoku_koj_ritu, ")
            .AppendLine("    @5gatu_keikaku_kensuu, ")
            .AppendLine("    @5gatu_keikaku_kingaku, ")
            .AppendLine("    @5gatu_keikaku_arari, ")

            .AppendLine("     @6gatu_keisanyou_uri_heikin_tanka, ")
            .AppendLine("     @6gatu_keisanyou_siire_heikin_tanka, ")

            .AppendLine("    @6gatu_keisanyou_koj_hantei_ritu, ")
            .AppendLine("    @6gatu_keisanyou_koj_jyuchuu_ritu, ")
            .AppendLine("    @6gatu_keisanyou_tyoku_koj_ritu, ")
            .AppendLine("    @6gatu_keikaku_kensuu, ")
            .AppendLine("    @6gatu_keikaku_kingaku, ")
            .AppendLine("    @6gatu_keikaku_arari, ")

            .AppendLine("     @7gatu_keisanyou_uri_heikin_tanka, ")
            .AppendLine("     @7gatu_keisanyou_siire_heikin_tanka, ")

            .AppendLine("    @7gatu_keisanyou_koj_hantei_ritu, ")
            .AppendLine("    @7gatu_keisanyou_koj_jyuchuu_ritu, ")
            .AppendLine("    @7gatu_keisanyou_tyoku_koj_ritu, ")
            .AppendLine("    @7gatu_keikaku_kensuu, ")
            .AppendLine("    @7gatu_keikaku_kingaku, ")
            .AppendLine("    @7gatu_keikaku_arari, ")

            .AppendLine("     @8gatu_keisanyou_uri_heikin_tanka, ")
            .AppendLine("     @8gatu_keisanyou_siire_heikin_tanka, ")

            .AppendLine("    @8gatu_keisanyou_koj_hantei_ritu, ")
            .AppendLine("    @8gatu_keisanyou_koj_jyuchuu_ritu, ")
            .AppendLine("    @8gatu_keisanyou_tyoku_koj_ritu, ")
            .AppendLine("    @8gatu_keikaku_kensuu, ")
            .AppendLine("    @8gatu_keikaku_kingaku, ")
            .AppendLine("    @8gatu_keikaku_arari, ")

            .AppendLine("     @9gatu_keisanyou_uri_heikin_tanka, ")
            .AppendLine("     @9gatu_keisanyou_siire_heikin_tanka, ")

            .AppendLine("    @9gatu_keisanyou_koj_hantei_ritu, ")
            .AppendLine("    @9gatu_keisanyou_koj_jyuchuu_ritu, ")
            .AppendLine("    @9gatu_keisanyou_tyoku_koj_ritu, ")
            .AppendLine("    @9gatu_keikaku_kensuu, ")
            .AppendLine("    @9gatu_keikaku_kingaku, ")
            .AppendLine("    @9gatu_keikaku_arari, ")

            .AppendLine("     @10gatu_keisanyou_uri_heikin_tanka, ")
            .AppendLine("     @10gatu_keisanyou_siire_heikin_tanka, ")

            .AppendLine("    @10gatu_keisanyou_koj_hantei_ritu, ")
            .AppendLine("    @10gatu_keisanyou_koj_jyuchuu_ritu, ")
            .AppendLine("    @10gatu_keisanyou_tyoku_koj_ritu, ")
            .AppendLine("    @10gatu_keikaku_kensuu, ")
            .AppendLine("    @10gatu_keikaku_kingaku, ")
            .AppendLine("    @10gatu_keikaku_arari, ")

            .AppendLine("     @11gatu_keisanyou_uri_heikin_tanka, ")
            .AppendLine("     @11gatu_keisanyou_siire_heikin_tanka, ")

            .AppendLine("    @11gatu_keisanyou_koj_hantei_ritu, ")
            .AppendLine("    @11gatu_keisanyou_koj_jyuchuu_ritu, ")
            .AppendLine("    @11gatu_keisanyou_tyoku_koj_ritu, ")
            .AppendLine("    @11gatu_keikaku_kensuu, ")
            .AppendLine("    @11gatu_keikaku_kingaku, ")
            .AppendLine("    @11gatu_keikaku_arari, ")

            .AppendLine("     @12gatu_keisanyou_uri_heikin_tanka, ")
            .AppendLine("     @12gatu_keisanyou_siire_heikin_tanka, ")

            .AppendLine("    @12gatu_keisanyou_koj_hantei_ritu, ")
            .AppendLine("    @12gatu_keisanyou_koj_jyuchuu_ritu, ")
            .AppendLine("    @12gatu_keisanyou_tyoku_koj_ritu, ")
            .AppendLine("    @12gatu_keikaku_kensuu, ")
            .AppendLine("    @12gatu_keikaku_kingaku, ")
            .AppendLine("    @12gatu_keikaku_arari, ")

            .AppendLine("     @1gatu_keisanyou_uri_heikin_tanka, ")
            .AppendLine("     @1gatu_keisanyou_siire_heikin_tanka, ")

            .AppendLine("    @1gatu_keisanyou_koj_hantei_ritu, ")
            .AppendLine("    @1gatu_keisanyou_koj_jyuchuu_ritu, ")
            .AppendLine("    @1gatu_keisanyou_tyoku_koj_ritu, ")
            .AppendLine("    @1gatu_keikaku_kensuu, ")
            .AppendLine("    @1gatu_keikaku_kingaku, ")
            .AppendLine("    @1gatu_keikaku_arari, ")

            .AppendLine("     @2gatu_keisanyou_uri_heikin_tanka, ")
            .AppendLine("     @2gatu_keisanyou_siire_heikin_tanka, ")

            .AppendLine("    @2gatu_keisanyou_koj_hantei_ritu, ")
            .AppendLine("    @2gatu_keisanyou_koj_jyuchuu_ritu, ")
            .AppendLine("    @2gatu_keisanyou_tyoku_koj_ritu, ")
            .AppendLine("    @2gatu_keikaku_kensuu, ")
            .AppendLine("    @2gatu_keikaku_kingaku, ")
            .AppendLine("    @2gatu_keikaku_arari, ")

            .AppendLine("     @3gatu_keisanyou_uri_heikin_tanka, ")
            .AppendLine("     @3gatu_keisanyou_siire_heikin_tanka, ")

            .AppendLine("    @3gatu_keisanyou_koj_hantei_ritu, ")
            .AppendLine("    @3gatu_keisanyou_koj_jyuchuu_ritu, ")
            .AppendLine("    @3gatu_keisanyou_tyoku_koj_ritu, ")
            .AppendLine("    @3gatu_keikaku_kensuu, ")
            .AppendLine("    @3gatu_keikaku_kingaku, ")
            .AppendLine("    @3gatu_keikaku_arari, ")
            .AppendLine("    NULL, ")
            .AppendLine("    NULL, ")
            .AppendLine("    NULL, ")
            .AppendLine("    NULL, ")
            .AppendLine("    NULL, ")
            .AppendLine("    NULL, ")
            .AppendLine("    NULL, ")
            .AppendLine("    @add_login_user_id, ")
            .AppendLine("    @add_login_user_id, ")
            .AppendLine("    GETDATE() ")
            .AppendLine(" ) ")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@keikaku_nendo", SqlDbType.Char, 4, drValue("keikaku_nendo")))                                                 '計画_年度
        paramList.Add(MakeParam("@add_datetime", SqlDbType.DateTime, 30, Convert.ToDateTime(CDate(strSyoriDatetime))))                          '登録日時
        paramList.Add(MakeParam("@busyo_cd", SqlDbType.VarChar, 4, drValue("busyo_cd")))                                                        '部署ｺｰﾄﾞ
        paramList.Add(MakeParam("@keikaku_kanri_syouhin_cd", SqlDbType.VarChar, 8, drValue("keikaku_kanri_syouhin_cd")))                        '計画管理_商品コード
        paramList.Add(MakeParam("@siten_mei", SqlDbType.VarChar, 40, drValue("siten_mei")))                                                     '支店名
        paramList.Add(MakeParam("@bunbetu_cd", SqlDbType.VarChar, 3, drValue("bunbetu_cd")))                                                    '分別コード

        paramList.Add(MakeParam("@4gatu_keisanyou_uri_heikin_tanka", SqlDbType.BigInt, 12, drValue("4gatu_keisanyou_uri_heikin_tanka")))        '4月_計算用__売上平均単価
        paramList.Add(MakeParam("@4gatu_keisanyou_siire_heikin_tanka", SqlDbType.BigInt, 12, drValue("4gatu_keisanyou_siire_heikin_tanka")))    '4月_計算用__仕入平均単価

        paramList.Add(MakeParam("@4gatu_keisanyou_koj_hantei_ritu", SqlDbType.Decimal, 4, drValue("4gatu_keisanyou_koj_hantei_ritu")))          '4月_計算用_工事判定率
        paramList.Add(MakeParam("@4gatu_keisanyou_koj_jyuchuu_ritu", SqlDbType.Decimal, 4, drValue("4gatu_keisanyou_koj_jyuchuu_ritu")))        '4月_計算用_工事受注率
        paramList.Add(MakeParam("@4gatu_keisanyou_tyoku_koj_ritu", SqlDbType.Decimal, 4, drValue("4gatu_keisanyou_tyoku_koj_ritu")))            '4月_計算用_直工事率
        paramList.Add(MakeParam("@4gatu_keikaku_kensuu", SqlDbType.BigInt, 12, drValue("4gatu_keikaku_kensuu")))                                '4月_計画件数
        paramList.Add(MakeParam("@4gatu_keikaku_kingaku", SqlDbType.BigInt, 12, drValue("4gatu_keikaku_kingaku")))                              '4月_計画金額
        paramList.Add(MakeParam("@4gatu_keikaku_arari", SqlDbType.BigInt, 12, drValue("4gatu_keikaku_arari")))                                  '4月_計画粗利

        paramList.Add(MakeParam("@5gatu_keisanyou_uri_heikin_tanka", SqlDbType.BigInt, 12, drValue("5gatu_keisanyou_uri_heikin_tanka")))        '5月_計算用__売上平均単価
        paramList.Add(MakeParam("@5gatu_keisanyou_siire_heikin_tanka", SqlDbType.BigInt, 12, drValue("5gatu_keisanyou_siire_heikin_tanka")))    '5月_計算用__仕入平均単価

        paramList.Add(MakeParam("@5gatu_keisanyou_koj_hantei_ritu", SqlDbType.Decimal, 4, drValue("5gatu_keisanyou_koj_hantei_ritu")))          '5月_計算用_工事判定率
        paramList.Add(MakeParam("@5gatu_keisanyou_koj_jyuchuu_ritu", SqlDbType.Decimal, 4, drValue("5gatu_keisanyou_koj_jyuchuu_ritu")))        '5月_計算用_工事受注率
        paramList.Add(MakeParam("@5gatu_keisanyou_tyoku_koj_ritu", SqlDbType.Decimal, 4, drValue("5gatu_keisanyou_tyoku_koj_ritu")))            '5月_計算用_直工事率
        paramList.Add(MakeParam("@5gatu_keikaku_kensuu", SqlDbType.BigInt, 12, drValue("5gatu_keikaku_kensuu")))                                '5月_計画件数
        paramList.Add(MakeParam("@5gatu_keikaku_kingaku", SqlDbType.BigInt, 12, drValue("5gatu_keikaku_kingaku")))                              '5月_計画金額
        paramList.Add(MakeParam("@5gatu_keikaku_arari", SqlDbType.BigInt, 12, drValue("5gatu_keikaku_arari")))                                  '5月_計画粗利

        paramList.Add(MakeParam("@6gatu_keisanyou_uri_heikin_tanka", SqlDbType.BigInt, 12, drValue("6gatu_keisanyou_uri_heikin_tanka")))        '6月_計算用__売上平均単価
        paramList.Add(MakeParam("@6gatu_keisanyou_siire_heikin_tanka", SqlDbType.BigInt, 12, drValue("6gatu_keisanyou_siire_heikin_tanka")))    '6月_計算用__仕入平均単価

        paramList.Add(MakeParam("@6gatu_keisanyou_koj_hantei_ritu", SqlDbType.Decimal, 4, drValue("6gatu_keisanyou_koj_hantei_ritu")))          '6月_計算用_工事判定率
        paramList.Add(MakeParam("@6gatu_keisanyou_koj_jyuchuu_ritu", SqlDbType.Decimal, 4, drValue("6gatu_keisanyou_koj_jyuchuu_ritu")))        '6月_計算用_工事受注率
        paramList.Add(MakeParam("@6gatu_keisanyou_tyoku_koj_ritu", SqlDbType.Decimal, 4, drValue("6gatu_keisanyou_tyoku_koj_ritu")))            '6月_計算用_直工事率
        paramList.Add(MakeParam("@6gatu_keikaku_kensuu", SqlDbType.BigInt, 12, drValue("6gatu_keikaku_kensuu")))                                '6月_計画件数
        paramList.Add(MakeParam("@6gatu_keikaku_kingaku", SqlDbType.BigInt, 12, drValue("6gatu_keikaku_kingaku")))                              '6月_計画金額
        paramList.Add(MakeParam("@6gatu_keikaku_arari", SqlDbType.BigInt, 12, drValue("6gatu_keikaku_arari")))                                  '6月_計画粗利

        paramList.Add(MakeParam("@7gatu_keisanyou_uri_heikin_tanka", SqlDbType.BigInt, 12, drValue("7gatu_keisanyou_uri_heikin_tanka")))        '7月_計算用__売上平均単価
        paramList.Add(MakeParam("@7gatu_keisanyou_siire_heikin_tanka", SqlDbType.BigInt, 12, drValue("7gatu_keisanyou_siire_heikin_tanka")))    '7月_計算用__仕入平均単価

        paramList.Add(MakeParam("@7gatu_keisanyou_koj_hantei_ritu", SqlDbType.Decimal, 4, drValue("7gatu_keisanyou_koj_hantei_ritu")))          '7月_計算用_工事判定率
        paramList.Add(MakeParam("@7gatu_keisanyou_koj_jyuchuu_ritu", SqlDbType.Decimal, 4, drValue("7gatu_keisanyou_koj_jyuchuu_ritu")))        '7月_計算用_工事受注率
        paramList.Add(MakeParam("@7gatu_keisanyou_tyoku_koj_ritu", SqlDbType.Decimal, 4, drValue("7gatu_keisanyou_tyoku_koj_ritu")))            '7月_計算用_直工事率
        paramList.Add(MakeParam("@7gatu_keikaku_kensuu", SqlDbType.BigInt, 12, drValue("7gatu_keikaku_kensuu")))                                '7月_計画件数
        paramList.Add(MakeParam("@7gatu_keikaku_kingaku", SqlDbType.BigInt, 12, drValue("7gatu_keikaku_kingaku")))                              '7月_計画金額
        paramList.Add(MakeParam("@7gatu_keikaku_arari", SqlDbType.BigInt, 12, drValue("7gatu_keikaku_arari")))                                  '7月_計画粗利

        paramList.Add(MakeParam("@8gatu_keisanyou_uri_heikin_tanka", SqlDbType.BigInt, 12, drValue("8gatu_keisanyou_uri_heikin_tanka")))        '8月_計算用__売上平均単価
        paramList.Add(MakeParam("@8gatu_keisanyou_siire_heikin_tanka", SqlDbType.BigInt, 12, drValue("8gatu_keisanyou_siire_heikin_tanka")))    '8月_計算用__仕入平均単価

        paramList.Add(MakeParam("@8gatu_keisanyou_koj_hantei_ritu", SqlDbType.Decimal, 12, drValue("8gatu_keisanyou_koj_hantei_ritu")))         '8月_計算用_工事判定率
        paramList.Add(MakeParam("@8gatu_keisanyou_koj_jyuchuu_ritu", SqlDbType.Decimal, 12, drValue("8gatu_keisanyou_koj_jyuchuu_ritu")))       '8月_計算用_工事受注率
        paramList.Add(MakeParam("@8gatu_keisanyou_tyoku_koj_ritu", SqlDbType.Decimal, 12, drValue("8gatu_keisanyou_tyoku_koj_ritu")))           '8月_計算用_直工事率
        paramList.Add(MakeParam("@8gatu_keikaku_kensuu", SqlDbType.BigInt, 12, drValue("8gatu_keikaku_kensuu")))                                '8月_計画件数
        paramList.Add(MakeParam("@8gatu_keikaku_kingaku", SqlDbType.BigInt, 12, drValue("8gatu_keikaku_kingaku")))                              '8月_計画金額
        paramList.Add(MakeParam("@8gatu_keikaku_arari", SqlDbType.BigInt, 12, drValue("8gatu_keikaku_arari")))                                  '8月_計画粗利

        paramList.Add(MakeParam("@9gatu_keisanyou_uri_heikin_tanka", SqlDbType.BigInt, 12, drValue("9gatu_keisanyou_uri_heikin_tanka")))        '9月_計算用__売上平均単価
        paramList.Add(MakeParam("@9gatu_keisanyou_siire_heikin_tanka", SqlDbType.BigInt, 12, drValue("9gatu_keisanyou_siire_heikin_tanka")))    '9月_計算用__仕入平均単価

        paramList.Add(MakeParam("@9gatu_keisanyou_koj_hantei_ritu", SqlDbType.Decimal, 4, drValue("9gatu_keisanyou_koj_hantei_ritu")))          '9月_計算用_工事判定率
        paramList.Add(MakeParam("@9gatu_keisanyou_koj_jyuchuu_ritu", SqlDbType.Decimal, 4, drValue("9gatu_keisanyou_koj_jyuchuu_ritu")))        '9月_計算用_工事受注率
        paramList.Add(MakeParam("@9gatu_keisanyou_tyoku_koj_ritu", SqlDbType.Decimal, 4, drValue("9gatu_keisanyou_tyoku_koj_ritu")))            '9月_計算用_直工事率
        paramList.Add(MakeParam("@9gatu_keikaku_kensuu", SqlDbType.BigInt, 12, drValue("9gatu_keikaku_kensuu")))                                '9月_計画件数
        paramList.Add(MakeParam("@9gatu_keikaku_kingaku", SqlDbType.BigInt, 12, drValue("9gatu_keikaku_kingaku")))                              '9月_計画金額
        paramList.Add(MakeParam("@9gatu_keikaku_arari", SqlDbType.BigInt, 12, drValue("9gatu_keikaku_arari")))                                  '9月_計画粗利

        paramList.Add(MakeParam("@10gatu_keisanyou_uri_heikin_tanka", SqlDbType.BigInt, 12, drValue("10gatu_keisanyou_uri_heikin_tanka")))        '10月_計算用__売上平均単価
        paramList.Add(MakeParam("@10gatu_keisanyou_siire_heikin_tanka", SqlDbType.BigInt, 12, drValue("10gatu_keisanyou_siire_heikin_tanka")))    '10月_計算用__仕入平均単価

        paramList.Add(MakeParam("@10gatu_keisanyou_koj_hantei_ritu", SqlDbType.Decimal, 4, drValue("10gatu_keisanyou_koj_hantei_ritu")))        '10月_計算用_工事判定率
        paramList.Add(MakeParam("@10gatu_keisanyou_koj_jyuchuu_ritu", SqlDbType.Decimal, 4, drValue("10gatu_keisanyou_koj_jyuchuu_ritu")))      '10月_計算用_工事受注率
        paramList.Add(MakeParam("@10gatu_keisanyou_tyoku_koj_ritu", SqlDbType.Decimal, 4, drValue("10gatu_keisanyou_tyoku_koj_ritu")))          '10月_計算用_直工事率
        paramList.Add(MakeParam("@10gatu_keikaku_kensuu", SqlDbType.BigInt, 12, drValue("10gatu_keikaku_kensuu")))                              '10月_計画件数
        paramList.Add(MakeParam("@10gatu_keikaku_kingaku", SqlDbType.BigInt, 12, drValue("10gatu_keikaku_kingaku")))                            '10月_計画金額
        paramList.Add(MakeParam("@10gatu_keikaku_arari", SqlDbType.BigInt, 12, drValue("10gatu_keikaku_arari")))                                '10月_計画粗利

        paramList.Add(MakeParam("@11gatu_keisanyou_uri_heikin_tanka", SqlDbType.BigInt, 12, drValue("11gatu_keisanyou_uri_heikin_tanka")))        '11月_計算用__売上平均単価
        paramList.Add(MakeParam("@11gatu_keisanyou_siire_heikin_tanka", SqlDbType.BigInt, 12, drValue("11gatu_keisanyou_siire_heikin_tanka")))    '11月_計算用__仕入平均単価

        paramList.Add(MakeParam("@11gatu_keisanyou_koj_hantei_ritu", SqlDbType.Decimal, 4, drValue("11gatu_keisanyou_koj_hantei_ritu")))        '11月_計算用_工事判定率
        paramList.Add(MakeParam("@11gatu_keisanyou_koj_jyuchuu_ritu", SqlDbType.Decimal, 4, drValue("11gatu_keisanyou_koj_jyuchuu_ritu")))      '11月_計算用_工事受注率
        paramList.Add(MakeParam("@11gatu_keisanyou_tyoku_koj_ritu", SqlDbType.Decimal, 4, drValue("11gatu_keisanyou_tyoku_koj_ritu")))          '11月_計算用_直工事率
        paramList.Add(MakeParam("@11gatu_keikaku_kensuu", SqlDbType.BigInt, 8, drValue("11gatu_keikaku_kensuu")))                               '11月_計画件数
        paramList.Add(MakeParam("@11gatu_keikaku_kingaku", SqlDbType.BigInt, 12, drValue("11gatu_keikaku_kingaku")))                            '11月_計画金額
        paramList.Add(MakeParam("@11gatu_keikaku_arari", SqlDbType.BigInt, 12, drValue("11gatu_keikaku_arari")))                                '11月_計画粗利

        paramList.Add(MakeParam("@12gatu_keisanyou_uri_heikin_tanka", SqlDbType.BigInt, 12, drValue("12gatu_keisanyou_uri_heikin_tanka")))        '12月_計算用__売上平均単価
        paramList.Add(MakeParam("@12gatu_keisanyou_siire_heikin_tanka", SqlDbType.BigInt, 12, drValue("12gatu_keisanyou_siire_heikin_tanka")))    '12月_計算用__仕入平均単価

        paramList.Add(MakeParam("@12gatu_keisanyou_koj_hantei_ritu", SqlDbType.Decimal, 4, drValue("12gatu_keisanyou_koj_hantei_ritu")))        '12月_計算用_工事判定率
        paramList.Add(MakeParam("@12gatu_keisanyou_koj_jyuchuu_ritu", SqlDbType.Decimal, 4, drValue("12gatu_keisanyou_koj_jyuchuu_ritu")))      '12月_計算用_工事受注率
        paramList.Add(MakeParam("@12gatu_keisanyou_tyoku_koj_ritu", SqlDbType.Decimal, 4, drValue("12gatu_keisanyou_tyoku_koj_ritu")))          '12月_計算用_直工事率
        paramList.Add(MakeParam("@12gatu_keikaku_kensuu", SqlDbType.BigInt, 12, drValue("12gatu_keikaku_kensuu")))                              '12月_計画件数
        paramList.Add(MakeParam("@12gatu_keikaku_kingaku", SqlDbType.BigInt, 12, drValue("12gatu_keikaku_kingaku")))                            '12月_計画金額
        paramList.Add(MakeParam("@12gatu_keikaku_arari", SqlDbType.BigInt, 12, drValue("12gatu_keikaku_arari")))                                '12月_計画粗利

        paramList.Add(MakeParam("@1gatu_keisanyou_uri_heikin_tanka", SqlDbType.BigInt, 12, drValue("1gatu_keisanyou_uri_heikin_tanka")))        '1月_計算用__売上平均単価
        paramList.Add(MakeParam("@1gatu_keisanyou_siire_heikin_tanka", SqlDbType.BigInt, 12, drValue("1gatu_keisanyou_siire_heikin_tanka")))    '1月_計算用__仕入平均単価

        paramList.Add(MakeParam("@1gatu_keisanyou_koj_hantei_ritu", SqlDbType.Decimal, 4, drValue("1gatu_keisanyou_koj_hantei_ritu")))          '1月_計算用_工事判定率
        paramList.Add(MakeParam("@1gatu_keisanyou_koj_jyuchuu_ritu", SqlDbType.Decimal, 4, drValue("1gatu_keisanyou_koj_jyuchuu_ritu")))        '1月_計算用_工事受注率
        paramList.Add(MakeParam("@1gatu_keisanyou_tyoku_koj_ritu", SqlDbType.Decimal, 4, drValue("1gatu_keisanyou_tyoku_koj_ritu")))            '1月_計算用_直工事率
        paramList.Add(MakeParam("@1gatu_keikaku_kensuu", SqlDbType.BigInt, 12, drValue("1gatu_keikaku_kensuu")))                                '1月_計画件数
        paramList.Add(MakeParam("@1gatu_keikaku_kingaku", SqlDbType.BigInt, 12, drValue("1gatu_keikaku_kingaku")))                              '1月_計画金額
        paramList.Add(MakeParam("@1gatu_keikaku_arari", SqlDbType.BigInt, 12, drValue("1gatu_keikaku_arari")))                                  '1月_計画粗利

        paramList.Add(MakeParam("@2gatu_keisanyou_uri_heikin_tanka", SqlDbType.BigInt, 12, drValue("2gatu_keisanyou_uri_heikin_tanka")))        '2月_計算用__売上平均単価
        paramList.Add(MakeParam("@2gatu_keisanyou_siire_heikin_tanka", SqlDbType.BigInt, 12, drValue("2gatu_keisanyou_siire_heikin_tanka")))    '2月_計算用__仕入平均単価

        paramList.Add(MakeParam("@2gatu_keisanyou_koj_hantei_ritu", SqlDbType.Decimal, 4, drValue("2gatu_keisanyou_koj_hantei_ritu")))          '2月_計算用_工事判定率
        paramList.Add(MakeParam("@2gatu_keisanyou_koj_jyuchuu_ritu", SqlDbType.Decimal, 4, drValue("2gatu_keisanyou_koj_jyuchuu_ritu")))        '2月_計算用_工事受注率
        paramList.Add(MakeParam("@2gatu_keisanyou_tyoku_koj_ritu", SqlDbType.Decimal, 4, drValue("2gatu_keisanyou_tyoku_koj_ritu")))            '2月_計算用_直工事率
        paramList.Add(MakeParam("@2gatu_keikaku_kensuu", SqlDbType.BigInt, 12, drValue("2gatu_keikaku_kensuu")))                                '2月_計画件数
        paramList.Add(MakeParam("@2gatu_keikaku_kingaku", SqlDbType.BigInt, 12, drValue("2gatu_keikaku_kingaku")))                              '2月_計画金額
        paramList.Add(MakeParam("@2gatu_keikaku_arari", SqlDbType.BigInt, 12, drValue("2gatu_keikaku_arari")))                                  '2月_計画粗利

        paramList.Add(MakeParam("@3gatu_keisanyou_uri_heikin_tanka", SqlDbType.BigInt, 12, drValue("3gatu_keisanyou_uri_heikin_tanka")))        '3月_計算用__売上平均単価
        paramList.Add(MakeParam("@3gatu_keisanyou_siire_heikin_tanka", SqlDbType.BigInt, 12, drValue("3gatu_keisanyou_siire_heikin_tanka")))    '3月_計算用__仕入平均単価

        paramList.Add(MakeParam("@3gatu_keisanyou_koj_hantei_ritu", SqlDbType.Decimal, 4, drValue("3gatu_keisanyou_koj_hantei_ritu")))          '3月_計算用_工事判定率
        paramList.Add(MakeParam("@3gatu_keisanyou_koj_jyuchuu_ritu", SqlDbType.Decimal, 4, drValue("3gatu_keisanyou_koj_jyuchuu_ritu")))        '3月_計算用_工事受注率
        paramList.Add(MakeParam("@3gatu_keisanyou_tyoku_koj_ritu", SqlDbType.Decimal, 4, drValue("3gatu_keisanyou_tyoku_koj_ritu")))            '3月_計算用_直工事率
        paramList.Add(MakeParam("@3gatu_keikaku_kensuu", SqlDbType.BigInt, 12, drValue("3gatu_keikaku_kensuu")))                                '3月_計画件数
        paramList.Add(MakeParam("@3gatu_keikaku_kingaku", SqlDbType.BigInt, 12, drValue("3gatu_keikaku_kingaku")))                              '3月_計画金額
        paramList.Add(MakeParam("@3gatu_keikaku_arari", SqlDbType.BigInt, 12, drValue("3gatu_keikaku_arari")))                                  '3月_計画粗利
        paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, strLoginUserId))                                                   '登録ログインユーザーID

        '登録実行
        insCount = ExecuteNonQuery(ManagerDA.Connection, CommandType.Text, sqlBuffer.ToString(), paramList.ToArray)

        If insCount > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>
    ''' FC用予定見込管理テーブルへのデータ登録
    ''' </summary>
    ''' <param name="drValue">登録データレコード</param>
    ''' <param name="strLoginUserId">ログインユーザーID</param>
    ''' <param name="strSyoriDatetime">処理日時</param>
    ''' <remarks></remarks>
    ''' <history>																
    ''' <para>2012/11/27 P-44979 曹敬仁 新規作成</para>																															
    ''' </history>
    Public Function InsFCYoteiMikomiKanriData(ByVal drValue As DataRow, _
                                              ByVal strLoginUserId As String, _
                                              ByVal strSyoriDatetime As String) As Boolean
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, drValue, strLoginUserId, strSyoriDatetime)

        Dim sqlBuffer As New System.Text.StringBuilder
        Dim insCount As Integer

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With sqlBuffer
            .AppendLine(" INSERT INTO t_fc_yotei_mikomi_kanri WITH(UPDLOCK)( ")
            .AppendLine("     [keikaku_nendo], ")                            '計画_年度
            .AppendLine("     [add_datetime], ")                             '登録日時
            .AppendLine("     [busyo_cd], ")                                 '部署ｺｰﾄﾞ
            .AppendLine("     [keikaku_kanri_syouhin_cd], ")                 '計画管理_商品コード
            .AppendLine("     [siten_mei], ")                                '支店名
            .AppendLine("     [bunbetu_cd], ")                               '分別コード
            .AppendLine("     [4gatu_mikomi_kensuu], ")                      '4月_見込件数
            .AppendLine("     [4gatu_mikomi_kingaku], ")                     '4月_見込金額
            .AppendLine("     [4gatu_mikomi_arari], ")                       '4月_見込粗利
            .AppendLine("     [5gatu_mikomi_kensuu], ")                      '5月_見込件数
            .AppendLine("     [5gatu_mikomi_kingaku], ")                     '5月_見込金額
            .AppendLine("     [5gatu_mikomi_arari], ")                       '5月_見込粗利
            .AppendLine("     [6gatu_mikomi_kensuu], ")                      '6月_見込件数
            .AppendLine("     [6gatu_mikomi_kingaku], ")                     '6月_見込金額
            .AppendLine("     [6gatu_mikomi_arari], ")                       '6月_見込粗利
            .AppendLine("     [7gatu_mikomi_kensuu], ")                      '7月_見込件数
            .AppendLine("     [7gatu_mikomi_kingaku], ")                     '7月_見込金額
            .AppendLine("     [7gatu_mikomi_arari], ")                       '7月_見込粗利
            .AppendLine("     [8gatu_mikomi_kensuu], ")                      '8月_見込件数
            .AppendLine("     [8gatu_mikomi_kingaku], ")                     '8月_見込金額
            .AppendLine("     [8gatu_mikomi_arari], ")                       '8月_見込粗利
            .AppendLine("     [9gatu_mikomi_kensuu], ")                      '9月_見込件数
            .AppendLine("     [9gatu_mikomi_kingaku], ")                     '9月_見込金額
            .AppendLine("     [9gatu_mikomi_arari], ")                       '9月_見込粗利
            .AppendLine("     [10gatu_mikomi_kensuu], ")                     '10月_見込件数
            .AppendLine("     [10gatu_mikomi_kingaku], ")                    '10月_見込金額
            .AppendLine("     [10gatu_mikomi_arari], ")                      '10月_見込粗利
            .AppendLine("     [11gatu_mikomi_kensuu], ")                     '11月_見込件数
            .AppendLine("     [11gatu_mikomi_kingaku], ")                    '11月_見込金額
            .AppendLine("     [11gatu_mikomi_arari], ")                      '11月_見込粗利
            .AppendLine("     [12gatu_mikomi_kensuu], ")                     '12月_見込件数
            .AppendLine("     [12gatu_mikomi_kingaku], ")                    '12月_見込金額
            .AppendLine("     [12gatu_mikomi_arari], ")                      '12月_見込粗利
            .AppendLine("     [1gatu_mikomi_kensuu], ")                      '1月_見込件数
            .AppendLine("     [1gatu_mikomi_kingaku], ")                     '1月_見込金額
            .AppendLine("     [1gatu_mikomi_arari], ")                       '1月_見込粗利
            .AppendLine("     [2gatu_mikomi_kensuu], ")                      '2月_見込件数
            .AppendLine("     [2gatu_mikomi_kingaku], ")                     '2月_見込金額
            .AppendLine("     [2gatu_mikomi_arari], ")                       '2月_見込粗利
            .AppendLine("     [3gatu_mikomi_kensuu], ")                      '3月_見込件数
            .AppendLine("     [3gatu_mikomi_kingaku], ")                     '3月_見込金額
            .AppendLine("     [3gatu_mikomi_arari], ")                       '3月_見込粗利
            .AppendLine("     [add_login_user_id], ")                        '登録ログインユーザーID
            .AppendLine("     [upd_login_user_id], ")                        '更新ログインユーザーID
            .AppendLine("     [upd_datetime] ")                              '更新日時
            .AppendLine("    )VALUES( ")
            .AppendLine("     @keikaku_nendo, ")
            .AppendLine("     @add_datetime, ")
            .AppendLine("     @busyo_cd, ")
            .AppendLine("     @keikaku_kanri_syouhin_cd, ")
            .AppendLine("     @siten_mei, ")
            .AppendLine("     @bunbetu_cd, ")
            .AppendLine("     @4gatu_mikomi_kensuu, ")
            .AppendLine("     @4gatu_mikomi_kingaku, ")
            .AppendLine("     @4gatu_mikomi_arari, ")
            .AppendLine("     @5gatu_mikomi_kensuu, ")
            .AppendLine("     @5gatu_mikomi_kingaku, ")
            .AppendLine("     @5gatu_mikomi_arari, ")
            .AppendLine("     @6gatu_mikomi_kensuu, ")
            .AppendLine("     @6gatu_mikomi_kingaku, ")
            .AppendLine("     @6gatu_mikomi_arari, ")
            .AppendLine("     @7gatu_mikomi_kensuu, ")
            .AppendLine("     @7gatu_mikomi_kingaku, ")
            .AppendLine("     @7gatu_mikomi_arari, ")
            .AppendLine("     @8gatu_mikomi_kensuu, ")
            .AppendLine("     @8gatu_mikomi_kingaku, ")
            .AppendLine("     @8gatu_mikomi_arari, ")
            .AppendLine("     @9gatu_mikomi_kensuu, ")
            .AppendLine("     @9gatu_mikomi_kingaku, ")
            .AppendLine("     @9gatu_mikomi_arari, ")
            .AppendLine("     @10gatu_mikomi_kensuu, ")
            .AppendLine("     @10gatu_mikomi_kingaku, ")
            .AppendLine("     @10gatu_mikomi_arari, ")
            .AppendLine("     @11gatu_mikomi_kensuu, ")
            .AppendLine("     @11gatu_mikomi_kingaku, ")
            .AppendLine("     @11gatu_mikomi_arari, ")
            .AppendLine("     @12gatu_mikomi_kensuu, ")
            .AppendLine("     @12gatu_mikomi_kingaku, ")
            .AppendLine("     @12gatu_mikomi_arari, ")
            .AppendLine("     @1gatu_mikomi_kensuu, ")
            .AppendLine("     @1gatu_mikomi_kingaku, ")
            .AppendLine("     @1gatu_mikomi_arari, ")
            .AppendLine("     @2gatu_mikomi_kensuu, ")
            .AppendLine("     @2gatu_mikomi_kingaku, ")
            .AppendLine("     @2gatu_mikomi_arari, ")
            .AppendLine("     @3gatu_mikomi_kensuu, ")
            .AppendLine("     @3gatu_mikomi_kingaku, ")
            .AppendLine("     @3gatu_mikomi_arari, ")
            .AppendLine("     @add_login_user_id, ")
            .AppendLine("     @add_login_user_id, ")
            .AppendLine("     GETDATE() ")
            .AppendLine(" 	) ")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@keikaku_nendo", SqlDbType.Char, 4, drValue("keikaku_nendo")))                         '計画年度(YYYY)
        paramList.Add(MakeParam("@add_datetime", SqlDbType.DateTime, 30, Convert.ToDateTime(CDate(strSyoriDatetime))))  '登録日時
        paramList.Add(MakeParam("@busyo_cd", SqlDbType.VarChar, 4, drValue("busyo_cd")))                                '部署ｺｰﾄﾞ
        paramList.Add(MakeParam("@keikaku_kanri_syouhin_cd", SqlDbType.VarChar, 8, drValue("keikaku_kanri_syouhin_cd")))       '計画管理_商品コード
        paramList.Add(MakeParam("@siten_mei", SqlDbType.VarChar, 40, drValue("siten_mei")))                             '支店名
        paramList.Add(MakeParam("@bunbetu_cd", SqlDbType.VarChar, 3, drValue("bunbetu_cd")))                            '分別コード
        paramList.Add(MakeParam("@4gatu_mikomi_kensuu", SqlDbType.BigInt, 12, drValue("4gatu_mikomi_kensuu")))          '4月_見込件数
        paramList.Add(MakeParam("@4gatu_mikomi_kingaku", SqlDbType.BigInt, 12, drValue("4gatu_mikomi_kingaku")))        '4月_見込金額
        paramList.Add(MakeParam("@4gatu_mikomi_arari", SqlDbType.BigInt, 12, drValue("4gatu_mikomi_arari")))            '4月_見込粗利
        paramList.Add(MakeParam("@5gatu_mikomi_kensuu", SqlDbType.BigInt, 12, drValue("5gatu_mikomi_kensuu")))          '5月_見込件数
        paramList.Add(MakeParam("@5gatu_mikomi_kingaku", SqlDbType.BigInt, 12, drValue("5gatu_mikomi_kingaku")))        '5月_見込金額
        paramList.Add(MakeParam("@5gatu_mikomi_arari", SqlDbType.BigInt, 12, drValue("5gatu_mikomi_arari")))            '5月_見込粗利
        paramList.Add(MakeParam("@6gatu_mikomi_kensuu", SqlDbType.BigInt, 12, drValue("6gatu_mikomi_kensuu")))          '6月_見込件数
        paramList.Add(MakeParam("@6gatu_mikomi_kingaku", SqlDbType.BigInt, 12, drValue("6gatu_mikomi_kingaku")))        '6月_見込金額
        paramList.Add(MakeParam("@6gatu_mikomi_arari", SqlDbType.BigInt, 12, drValue("6gatu_mikomi_arari")))            '6月_見込粗利
        paramList.Add(MakeParam("@7gatu_mikomi_kensuu", SqlDbType.BigInt, 12, drValue("7gatu_mikomi_kensuu")))          '7月_見込件数
        paramList.Add(MakeParam("@7gatu_mikomi_kingaku", SqlDbType.BigInt, 12, drValue("7gatu_mikomi_kingaku")))        '7月_見込金額
        paramList.Add(MakeParam("@7gatu_mikomi_arari", SqlDbType.BigInt, 12, drValue("7gatu_mikomi_arari")))            '7月_見込粗利
        paramList.Add(MakeParam("@8gatu_mikomi_kensuu", SqlDbType.BigInt, 12, drValue("8gatu_mikomi_kensuu")))          '8月_見込件数
        paramList.Add(MakeParam("@8gatu_mikomi_kingaku", SqlDbType.BigInt, 12, drValue("8gatu_mikomi_kingaku")))        '8月_見込金額
        paramList.Add(MakeParam("@8gatu_mikomi_arari", SqlDbType.BigInt, 12, drValue("8gatu_mikomi_arari")))            '8月_見込粗利
        paramList.Add(MakeParam("@9gatu_mikomi_kensuu", SqlDbType.BigInt, 12, drValue("9gatu_mikomi_kensuu")))          '9月_見込件数
        paramList.Add(MakeParam("@9gatu_mikomi_kingaku", SqlDbType.BigInt, 12, drValue("9gatu_mikomi_kingaku")))        '9月_見込金額
        paramList.Add(MakeParam("@9gatu_mikomi_arari", SqlDbType.BigInt, 12, drValue("9gatu_mikomi_arari")))            '9月_見込粗利
        paramList.Add(MakeParam("@10gatu_mikomi_kensuu", SqlDbType.BigInt, 12, drValue("10gatu_mikomi_kensuu")))        '10月_見込件数
        paramList.Add(MakeParam("@10gatu_mikomi_kingaku", SqlDbType.BigInt, 12, drValue("10gatu_mikomi_kingaku")))      '10月_見込金額
        paramList.Add(MakeParam("@10gatu_mikomi_arari", SqlDbType.BigInt, 12, drValue("10gatu_mikomi_arari")))          '10月_見込粗利
        paramList.Add(MakeParam("@11gatu_mikomi_kensuu", SqlDbType.BigInt, 12, drValue("11gatu_mikomi_kensuu")))        '11月_見込件数
        paramList.Add(MakeParam("@11gatu_mikomi_kingaku", SqlDbType.BigInt, 12, drValue("11gatu_mikomi_kingaku")))      '11月_見込金額
        paramList.Add(MakeParam("@11gatu_mikomi_arari", SqlDbType.BigInt, 12, drValue("11gatu_mikomi_arari")))          '11月_見込粗利
        paramList.Add(MakeParam("@12gatu_mikomi_kensuu", SqlDbType.BigInt, 12, drValue("12gatu_mikomi_kensuu")))        '12月_見込件数
        paramList.Add(MakeParam("@12gatu_mikomi_kingaku", SqlDbType.BigInt, 12, drValue("12gatu_mikomi_kingaku")))      '12月_見込金額
        paramList.Add(MakeParam("@12gatu_mikomi_arari", SqlDbType.BigInt, 12, drValue("12gatu_mikomi_arari")))          '12月_見込粗利
        paramList.Add(MakeParam("@1gatu_mikomi_kensuu", SqlDbType.BigInt, 12, drValue("1gatu_mikomi_kensuu")))          '1月_見込件数
        paramList.Add(MakeParam("@1gatu_mikomi_kingaku", SqlDbType.BigInt, 12, drValue("1gatu_mikomi_kingaku")))        '1月_見込金額
        paramList.Add(MakeParam("@1gatu_mikomi_arari", SqlDbType.BigInt, 12, drValue("1gatu_mikomi_arari")))            '1月_見込粗利
        paramList.Add(MakeParam("@2gatu_mikomi_kensuu", SqlDbType.BigInt, 12, drValue("2gatu_mikomi_kensuu")))          '2月_見込件数
        paramList.Add(MakeParam("@2gatu_mikomi_kingaku", SqlDbType.BigInt, 12, drValue("2gatu_mikomi_kingaku")))        '2月_見込金額
        paramList.Add(MakeParam("@2gatu_mikomi_arari", SqlDbType.BigInt, 12, drValue("2gatu_mikomi_arari")))            '2月_見込粗利
        paramList.Add(MakeParam("@3gatu_mikomi_kensuu", SqlDbType.BigInt, 12, drValue("3gatu_mikomi_kensuu")))          '3月_見込件数
        paramList.Add(MakeParam("@3gatu_mikomi_kingaku", SqlDbType.BigInt, 12, drValue("3gatu_mikomi_kingaku")))        '3月_見込金額
        paramList.Add(MakeParam("@3gatu_mikomi_arari", SqlDbType.BigInt, 12, drValue("3gatu_mikomi_arari")))            '3月_見込粗利
        paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, strLoginUserId))                           '登録ログインユーザーID

        '登録実行
        insCount = SQLHelper.ExecuteNonQuery(ManagerDA.Connection, CommandType.Text, sqlBuffer.ToString, paramList.ToArray())

        If insCount > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' アップロード管理テーブルへのデータ登録
    ''' </summary>
    ''' <param name="strSyoriDatetime">処理日時</param>
    ''' <param name="strNyuuryokuFileMei">入力ファイル名</param>
    ''' <param name="strEdiJouhouSakuseiDate">EDI情報作成日</param>
    ''' <param name="strErrorUmu">エラー有無</param>
    ''' <param name="strLoginUserId">ログインユーザーID</param>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    Public Function InsInputKanri(ByVal strSyoriDatetime As String, _
                                  ByVal strNyuuryokuFileMei As String, _
                                  ByVal strEdiJouhouSakuseiDate As String, _
                                  ByVal strErrorUmu As Integer, _
                                  ByVal strLoginUserId As String) As Boolean
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                strSyoriDatetime, strNyuuryokuFileMei, strEdiJouhouSakuseiDate, strErrorUmu, strLoginUserId)

        '戻り値
        Dim InsCount As Integer = 0

        Dim commandTextSb As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With commandTextSb
            .AppendLine("INSERT INTO  ")
            .AppendLine("	t_upload_kanri WITH(UPDLOCK)")
            .AppendLine("	( ")
            .AppendLine("		syori_datetime ")                   '処理日時
            .AppendLine("		,nyuuryoku_file_mei ")              '入力ファイル名
            .AppendLine("		,edi_jouhou_sakusei_date ")         'EDI情報作成日
            .AppendLine("		,error_umu ")                       'エラー有無
            .AppendLine("		,file_kbn ")                        'ファイル区分
            .AppendLine("		,add_login_user_id ")               '登録ログインユーザーID
            .AppendLine("		,add_datetime ")                    '登録日時
            .AppendLine("		,upd_login_user_id ")               '更新ログインユーザーID
            .AppendLine("		,upd_datetime ")                    '更新日時
            .AppendLine("	) ")
            .AppendLine("VALUES ")
            .AppendLine("( ")
            .AppendLine("	@syori_datetime ")
            .AppendLine("	,@nyuuryoku_file_mei ")
            .AppendLine("	,@edi_jouhou_sakusei_date ")
            .AppendLine("	,@error_umu ")
            .AppendLine("	,@file_kbn ")
            .AppendLine("	,@add_login_user_id ")
            .AppendLine("	,GETDATE() ")
            .AppendLine("	,@add_login_user_id ")
            .AppendLine("	,GETDATE() ")
            .AppendLine(") ")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@syori_datetime", SqlDbType.DateTime, 30, Convert.ToDateTime(CDate(strSyoriDatetime))))    '処理日時
        paramList.Add(MakeParam("@nyuuryoku_file_mei", SqlDbType.VarChar, 128, strNyuuryokuFileMei))                        '入力ファイル名
        paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, strEdiJouhouSakuseiDate))                'EDI情報作成日
        paramList.Add(MakeParam("@error_umu", SqlDbType.Int, 1, strErrorUmu))                                               'エラー有無
        paramList.Add(MakeParam("@file_kbn", SqlDbType.Int, 2, 2))                                                          'ファイル区分
        paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, strLoginUserId))                               'ログインユーザーID

        '登録実行
        InsCount = ExecuteNonQuery(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

        If InsCount > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>
    ''' Debug用SQL文を取得
    ''' </summary>
    ''' <param name="strSql">生成されたSQL文</param>
    ''' <param name="ParamList">パラメータリスト</param>
    ''' <returns>Debug用SQL文</returns>
    ''' <remarks></remarks>
    Public Function GetDebugSql(ByVal strSql As String, ByVal ParamList As List(Of SqlClient.SqlParameter)) As String
        Dim i As Integer
        Dim result As String = String.Empty
        Dim objSqlParam As SqlClient.SqlParameter
        Dim strParamName As String
        Dim strParamValue As String

        ''パラメータの名前をソートする
        'Call ParamList.Sort()

        For i = 0 To ParamList.Count - 1
            objSqlParam = ParamList(i)
            strParamName = objSqlParam.ParameterName
            strParamValue = objSqlParam.Value.ToString
            strSql = strSql.Replace(strParamName, "'" & strParamValue & "'")
        Next

        result = strSql

        Return result
    End Function

    ''' <summary>
    ''' 報連送のデータを取得する
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/10/14　李宇(大連情報システム部)　新規作成</history>
    Public Function GetHorennSou(ByVal strKameitenCd As String) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                strKameitenCd)
        '戻りデータセット
        Dim dsReturn As New Data.DataSet

        'SQLコメント
        Dim commandTextSb As New StringBuilder

        'バラメタ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	uccrpdev")
            .AppendLine("	,uccrpseq")
            .AppendLine("FROM ")
            .AppendLine("	sfamt_usrcorp_mvr")
            .AppendLine("WHERE ")
            .AppendLine("	ucdelflg = '0'")
            .AppendLine("	AND len(ISNULL(@kameitenCd ,'')) = 8")
            .AppendLine("	AND uccrpcod = @kameitenCd")
        End With
        'バラメタ
        paramList.Add(MakeParam("@kameitenCd", SqlDbType.VarChar, 16, strKameitenCd))    '加盟店コード
        '検索実行
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "dtHorennSou", paramList.ToArray())

        Return dsReturn.Tables("dtHorennSou")

    End Function
End Class

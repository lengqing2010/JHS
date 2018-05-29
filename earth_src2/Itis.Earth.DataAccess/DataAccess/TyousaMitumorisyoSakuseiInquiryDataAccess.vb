Imports System.Data.SqlClient
Imports System.Text
Imports Itis.ApplicationBlocks.Data.SQLHelper

''' <summary>
''' 調査見積書作成指示
''' </summary>
''' <remarks></remarks>
''' <history>2013/11/13 李宇(大連情報システム部) 新規作成</history>
Public Class TyousaMitumorisyoSakuseiInquiryDataAccess

    ''' <summary>
    ''' 表示住所 選択
    ''' </summary>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/11/13 李宇(大連情報システム部) 新規作成</history>
    Public Function SelJyuusyoInfo() As Data.DataTable

        'DataSetインスタンスの生成
        Dim dsDataSet As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT")
            .AppendLine("   kanri_no")
            .AppendLine("  ,(convert(varchar,kanri_no) + '：' + shiten_mei) AS shiten_mei")
            .AppendLine("FROM  ")
            .AppendLine("   m_mitumori_hyoujijyuusyo_kanri WITH(READCOMMITTED)")
            .AppendLine("ORDER BY kanri_no ASC")
        End With

        ' 検索実行
        FillDataset(ConnectionManager.Instance.ConnectionString, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    "dsDataSet", paramList.ToArray)

        '戻る
        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>
    ''' 「見積書作成回数」を取得する
    ''' </summary>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strBultukennNo">見積書作成回数</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/11/13 李宇(大連情報システム部) 新規作成</history>
    Public Function SelSakuseiKaisuu(ByVal strKbn As String, ByVal strBultukennNo As String) As Data.DataTable

        'DataSetインスタンスの生成
        Dim dsDataSet As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT")
            .AppendLine("   mit_sakusei_kaisuu")
            .AppendLine("FROM  ")
            .AppendLine("   t_tyousa_mitumori_sakusei_kanri WITH(READCOMMITTED)")
            .AppendLine("WHERE")
            .AppendLine("   kbn = @kbn")
            .AppendLine("   AND hosyousyo_no = @hosyousyo_no")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, strKbn)) '区分
        paramList.Add(MakeParam("@hosyousyo_no", SqlDbType.VarChar, 10, strBultukennNo)) '保証書NO

        ' 検索実行
        FillDataset(ConnectionManager.Instance.ConnectionString, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    "dsDataSet", paramList.ToArray)

        '戻る
        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>
    ''' 承認者 選択
    ''' </summary>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/11/13 李宇(大連情報システム部) 新規作成</history>
    Public Function SelSyouninSyaInfo() As Data.DataTable

        'DataSetインスタンスの生成
        Dim dsDataSet As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT")
            .AppendLine("   syouninsya_id")
            .AppendLine("  ,syouninsya_mei")
            .AppendLine("FROM  ")
            .AppendLine("   m_syouninsya_syouninin_kanri WITH(READCOMMITTED)")
            .AppendLine("ORDER BY hyouji_jyun ASC")
        End With

        ' 検索実行
        FillDataset(ConnectionManager.Instance.ConnectionString, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    "dsDataSet", paramList.ToArray)

        '戻る
        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>
    ''' 見積書の存在を判斷する
    ''' </summary>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strBultukennNo">保証書NO</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/11/13 李宇(大連情報システム部) 新規作成</history>
    Public Function SelMitumoriCount(ByVal strKbn As String, ByVal strBultukennNo As String) As Data.DataTable

        'DataSetインスタンスの生成
        Dim dsDataSet As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT")
            .AppendLine("   COUNT(kbn)")
            .AppendLine("FROM  ")
            .AppendLine("   t_tyousa_mitumori_sakusei_kanri WITH(READCOMMITTED)")
            .AppendLine("WHERE")
            .AppendLine("   kbn = @kbn")
            .AppendLine("   AND hosyousyo_no = @hosyousyo_no")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, strKbn)) '区分
        paramList.Add(MakeParam("@hosyousyo_no", SqlDbType.VarChar, 10, strBultukennNo)) '保証書NO

        ' 検索実行
        FillDataset(ConnectionManager.Instance.ConnectionString, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    "dsDataSet", paramList.ToArray)

        '戻る
        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>
    ''' 見積書作成回数を更新する
    ''' </summary>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strBultukennNo">保証書NO</param>
    ''' <param name="inMitSakuseiKaisuu">見積書作成回数</param>
    ''' <param name="inSyouhizeiHyouji">消費税表示</param>
    ''' <param name="inMooruTenkaiFlg">モール展開FLG</param>
    ''' <param name="inHyoujiJyuusyoNo">表示住所_管理No</param>
    ''' <param name="strTourokuId">担当者ID</param>
    ''' <param name="strTantousyaMei">担当者名</param>
    ''' <param name="strSyouninsyaId">承認者ID</param>
    ''' <param name="strSyouninsyaMei">承認者名</param>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    ''' <history>2013/11/15 李宇(大連情報システム部) 新規作成</history>
    Public Function UpdMitumoriKaisu(ByVal strKbn As String, _
                                    ByVal strBultukennNo As String, _
                                    ByVal inMitSakuseiKaisuu As Integer, _
                                    ByVal inSyouhizeiHyouji As Integer, _
                                    ByVal inMooruTenkaiFlg As Integer, _
                                    ByVal inHyoujiJyuusyoNo As Integer, _
                                    ByVal strTourokuId As String, _
                                    ByVal strTantousyaMei As String, _
                                    ByVal strSyouninsyaId As String, _
                                    ByVal strSyouninsyaMei As String, _
                                    ByVal strSakuseiDate As String, _
                                    ByVal strIraiTantousyaMei As String) As Boolean

        '更新件数
        Dim updCount As Integer = 0

        Dim commandTextSb As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("UPDATE ")
            .AppendLine("  t_tyousa_mitumori_sakusei_kanri WITH(UPDLOCK)")
            .AppendLine("SET ")
            .AppendLine("  mit_sakusei_kaisuu = @mit_sakusei_kaisuu")
            .AppendLine("  ,syouhizei_hyouji = @syouhizei_hyouji")
            .AppendLine("  ,mooru_tenkai_flg = @mooru_tenkai_flg")
            .AppendLine("  ,hyouji_jyuusyo_no = @hyouji_jyuusyo_no")
            .AppendLine("  ,tantousya_id = @tantousya_id")
            .AppendLine("  ,tantousya_mei = @tantousya_mei")
            .AppendLine("  ,syouninsya_id = @syouninsya_id")
            .AppendLine("  ,syouninsya_mei = @syouninsya_mei")
            .AppendLine("  ,tys_mit_sakusei_date = @tys_mit_sakusei_date")
            .AppendLine("  ,tys_mit_irai_tantousya_mei = @tys_mit_irai_tantousya_mei")
            .AppendLine("  ,upd_login_user_id = @upd_login_user_id")
            .AppendLine("  ,upd_datetime = GetDate()")
            .AppendLine("WHERE")
            .AppendLine("  kbn = @kbn")
            .AppendLine("  AND hosyousyo_no = @hosyousyo_no")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, strKbn))                      '区分
        paramList.Add(MakeParam("@hosyousyo_no", SqlDbType.VarChar, 10, strBultukennNo)) '保証書NO
        paramList.Add(MakeParam("@mit_sakusei_kaisuu", SqlDbType.Int, 10, inMitSakuseiKaisuu))    '作成回数
        paramList.Add(MakeParam("@syouhizei_hyouji", SqlDbType.Int, 10, inSyouhizeiHyouji))    '消費税表示
        paramList.Add(MakeParam("@mooru_tenkai_flg", SqlDbType.Int, 10, inMooruTenkaiFlg))     'モール展開FLG
        paramList.Add(MakeParam("@hyouji_jyuusyo_no", SqlDbType.Int, 10, inHyoujiJyuusyoNo))   '表示住所_管理No
        paramList.Add(MakeParam("@tantousya_id", SqlDbType.VarChar, 30, strTourokuId))        '担当者ID
        paramList.Add(MakeParam("@tantousya_mei", SqlDbType.VarChar, 128, strTantousyaMei))   '担当者名
        paramList.Add(MakeParam("@syouninsya_id", SqlDbType.VarChar, 30, strSyouninsyaId))    '承認者ID
        paramList.Add(MakeParam("@syouninsya_mei", SqlDbType.VarChar, 128, strSyouninsyaMei)) '承認者名
        paramList.Add(MakeParam("@tys_mit_sakusei_date", SqlDbType.DateTime, 30, strSakuseiDate)) '調査見積書作成日
        paramList.Add(MakeParam("@tys_mit_irai_tantousya_mei", SqlDbType.VarChar, 20, IIf(strIraiTantousyaMei.Equals(String.Empty), DBNull.Value, strIraiTantousyaMei))) '調査見積書_依頼担当者
        paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, strTourokuId))  '更新者

        '実行
        updCount = ExecuteNonQuery(ConnectionManager.Instance.ConnectionString, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

        If Not updCount > 0 Then
            Return False
        End If
        Return True

    End Function

    ''' <summary>
    ''' 「調査見積書作成管理テーブル」に登録する
    ''' </summary>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strBultukennNo">保証書NO</param>
    ''' <param name="inMitSakuseiKaisuu">見積作成回数</param>
    ''' <param name="inSyouhizeiHyouji">消費税表示</param>
    ''' <param name="inMooruTenkaiFlg">モール展開FLG</param>
    ''' <param name="inHyoujiJyuusyoNo">表示住所_管理No</param>
    ''' <param name="strTourokuId">担当者ID</param>
    ''' <param name="strTantousyaMei">担当者名</param>
    ''' <param name="strSyouninsyaId">承認者ID</param>
    ''' <param name="strSyouninsyaMei">承認者名</param>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    ''' <history>2013/11/14 李宇(大連情報システム部) 新規作成</history>
    Public Function InsMitumoriKaisu(ByVal strKbn As String, _
                                    ByVal strBultukennNo As String, _
                                    ByVal inMitSakuseiKaisuu As Integer, _
                                    ByVal inSyouhizeiHyouji As Integer, _
                                    ByVal inMooruTenkaiFlg As Integer, _
                                    ByVal inHyoujiJyuusyoNo As Integer, _
                                    ByVal strTourokuId As String, _
                                    ByVal strTantousyaMei As String, _
                                    ByVal strSyouninsyaId As String, _
                                    ByVal strSyouninsyaMei As String, _
                                    ByVal strSakuseiDate As String, _
                                    ByVal strIraiTantousyaMei As String) As Boolean

        '更新件数
        Dim inCount As Integer = 0

        Dim commandTextSb As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("INSERT INTO t_tyousa_mitumori_sakusei_kanri WITH(UPDLOCK)")
            .AppendLine("           (kbn")
            .AppendLine("           ,hosyousyo_no")
            .AppendLine("           ,mit_sakusei_kaisuu")
            .AppendLine("           ,syouhizei_hyouji")
            .AppendLine("           ,mooru_tenkai_flg")
            .AppendLine("           ,hyouji_jyuusyo_no")
            .AppendLine("           ,tantousya_id")
            .AppendLine("           ,tantousya_mei")
            .AppendLine("           ,syouninsya_id")
            .AppendLine("           ,syouninsya_mei")
            .AppendLine("           ,tys_mit_sakusei_date")
            .AppendLine("           ,tys_mit_irai_tantousya_mei")
            .AppendLine("           ,add_login_user_id")
            .AppendLine("           ,add_datetime")
            .AppendLine("           ,upd_login_user_id")
            .AppendLine("           ,upd_datetime)")
            .AppendLine("     VALUES")
            .AppendLine("           (@kbn")
            .AppendLine("           ,@hosyousyo_no")
            .AppendLine("           ,@mit_sakusei_kaisuu")
            .AppendLine("           ,@syouhizei_hyouji")
            .AppendLine("           ,@mooru_tenkai_flg")
            .AppendLine("           ,@hyouji_jyuusyo_no")
            .AppendLine("           ,@tantousya_id")
            .AppendLine("           ,@tantousya_mei")
            .AppendLine("           ,@syouninsya_id")
            .AppendLine("           ,@syouninsya_mei")
            .AppendLine("           ,@tys_mit_sakusei_date")
            .AppendLine("           ,@tys_mit_irai_tantousya_mei")
            .AppendLine("           ,@add_login_user_id")
            .AppendLine("           ,GetDate()")
            .AppendLine("           ,NULL")
            .AppendLine("           ,NULL)")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, strKbn))                      '区分
        paramList.Add(MakeParam("@hosyousyo_no", SqlDbType.VarChar, 10, strBultukennNo)) '保証書NO
        paramList.Add(MakeParam("@mit_sakusei_kaisuu", SqlDbType.Int, 10, inMitSakuseiKaisuu))    '作成回数
        paramList.Add(MakeParam("@syouhizei_hyouji", SqlDbType.Int, 10, inSyouhizeiHyouji))    '消費税表示
        paramList.Add(MakeParam("@mooru_tenkai_flg", SqlDbType.Int, 10, inMooruTenkaiFlg))     'モール展開FLG
        paramList.Add(MakeParam("@hyouji_jyuusyo_no", SqlDbType.Int, 10, inHyoujiJyuusyoNo))   '表示住所_管理No
        paramList.Add(MakeParam("@tantousya_id", SqlDbType.VarChar, 30, strTourokuId))        '担当者ID
        paramList.Add(MakeParam("@tantousya_mei", SqlDbType.VarChar, 128, strTantousyaMei))   '担当者名
        paramList.Add(MakeParam("@syouninsya_id", SqlDbType.VarChar, 30, strSyouninsyaId))    '承認者ID
        paramList.Add(MakeParam("@syouninsya_mei", SqlDbType.VarChar, 128, strSyouninsyaMei)) '承認者名
        paramList.Add(MakeParam("@tys_mit_sakusei_date", SqlDbType.DateTime, 30, strSakuseiDate)) '調査見積書作成日
        paramList.Add(MakeParam("@tys_mit_irai_tantousya_mei", SqlDbType.VarChar, 20, IIf(strIraiTantousyaMei.Equals(String.Empty), DBNull.Value, strIraiTantousyaMei))) '調査見積書_依頼担当者
        paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, strTourokuId))   '登録ログインユーザID

        '実行
        inCount = ExecuteNonQuery(ConnectionManager.Instance.ConnectionString, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

        If Not inCount > 0 Then
            Return False
        End If
        Return True

    End Function

    ''' <summary>
    ''' 担当者IDの存在を判斷する
    ''' </summary>
    ''' <param name="strTantousyaId">担当者ID</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/11/14 李宇(大連情報システム部) 新規作成</history>
    Public Function SelSonzaiHandan(ByVal strTantousyaId As String) As Data.DataTable

        'DataSetインスタンスの生成
        Dim dsDataSet As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT")
            .AppendLine("   COUNT(tantousya_id)")
            .AppendLine("FROM  ")
            .AppendLine("   m_tantousya_syouninin_kanri WITH(READCOMMITTED)")
            .AppendLine("WHERE tantousya_id = @tantousya_id")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@tantousya_id", SqlDbType.VarChar, 30, strTantousyaId)) '担当者ID

        ' 検索実行
        FillDataset(ConnectionManager.Instance.ConnectionString, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    "dsDataSet", paramList.ToArray)

        '戻る
        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>
    ''' 承認者紐付け承認印管理マスタから【承認印】を取得する
    ''' </summary>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strBultukennNo">保証書NO</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/11/19 李宇(大連情報システム部) 新規作成</history>
    Public Function SelSyouninIn(ByVal strKbn As String, ByVal strBultukennNo As String) As Data.DataTable

        'DataSetインスタンスの生成
        Dim dsDataSet As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT")
            .AppendLine("  syou.syouninin_kakunousaki_pass")
            .AppendLine("FROM  ")
            .AppendLine("   t_tyousa_mitumori_sakusei_kanri ty WITH(READCOMMITTED)")
            .AppendLine("   inner join  m_syouninsya_syouninin_kanri syou")
            .AppendLine("   on ty.syouninsya_id =  syou.syouninsya_id")
            .AppendLine("WHERE")
            .AppendLine("   ty.kbn = @kbn and ty.hosyousyo_no=@hosyousyo_no")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, strKbn)) '区分
        paramList.Add(MakeParam("@hosyousyo_no", SqlDbType.VarChar, 10, strBultukennNo)) '保証書NO

        ' 検索実行
        FillDataset(ConnectionManager.Instance.ConnectionString, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    "dsDataSet", paramList.ToArray)

        '戻る
        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>
    ''' 担当者紐付け承認印管理マスタから【承認印】を取得する
    ''' </summary>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strBultukennNo">保証書NO</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/11/19 李宇(大連情報システム部) 新規作成</history>
    Public Function SelTantouIn(ByVal strKbn As String, ByVal strBultukennNo As String) As Data.DataTable

        'DataSetインスタンスの生成
        Dim dsDataSet As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT")
            .AppendLine("  tanto.tantousyain_kakunousaki_pass")
            .AppendLine("FROM  ")
            .AppendLine("   t_tyousa_mitumori_sakusei_kanri ty WITH(READCOMMITTED)")
            .AppendLine("   inner join  m_tantousya_syouninin_kanri tanto")
            .AppendLine("   on ty.tantousya_id =  tanto.tantousya_id")
            .AppendLine("WHERE")
            .AppendLine("   ty.kbn = @kbn and ty.hosyousyo_no=@hosyousyo_no")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, strKbn)) '区分
        paramList.Add(MakeParam("@hosyousyo_no", SqlDbType.VarChar, 10, strBultukennNo)) '保証書NO

        ' 検索実行
        FillDataset(ConnectionManager.Instance.ConnectionString, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    "dsDataSet", paramList.ToArray)

        '戻る
        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>
    ''' [Fixed Data Section]ONE
    ''' </summary>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strBultukennNo">保証書NO</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/11/19 李宇(大連情報システム部) 新規作成</history>
    Public Function SelKihonInfoOne(ByVal strKbn As String, ByVal strBultukennNo As String) As Data.DataTable

        'DataSetインスタンスの生成
        Dim dsDataSet As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT")
            .AppendLine("   CONVERT(varchar(100),ty.tys_mit_sakusei_date,111) AS hituke --調査見積書作成日")
            .AppendLine("   ,'〒' + jyu.yuubin_no AS yuubin_no --郵便番号")
            .AppendLine("   ,jyu.jyuusyo1 AS jyuusyo1 --住所1")
            .AppendLine("   ,jyu.jyuusyo2 AS jyuusyo2 --住所2")
            .AppendLine("   ,'Ｔｅｌ：' + jyu.tel_no + '  ' + 'Ｆａｘ：' + jyu.fax_no AS tel_fax --Tel")
            .AppendLine("   ,ty.tantousya_mei AS sousinsya --調査見積書作成者")
            .AppendLine("   ,ty.tys_mit_irai_tantousya_mei AS tys_mit_irai_tantousya_mei --調査見積書_依頼担当者")
            '==================2015/09/17 案件「430011」の対応 追加↓====================================
            .AppendLine("   ,jyu.hosoku  AS hosoku --補足")
            '==================2015/09/17 案件「430011」の対応 追加↑====================================
            .AppendLine("FROM  ")
            .AppendLine("   t_tyousa_mitumori_sakusei_kanri ty WITH(READCOMMITTED)")
            .AppendLine("   inner join m_mitumori_hyoujijyuusyo_kanri jyu")
            .AppendLine("   on ty.hyouji_jyuusyo_no=jyu.kanri_no")
            .AppendLine("WHERE")
            .AppendLine("   ty.kbn = @kbn and ty.hosyousyo_no=@hosyousyo_no")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, strKbn)) '区分
        paramList.Add(MakeParam("@hosyousyo_no", SqlDbType.VarChar, 10, strBultukennNo)) '保証書NO

        ' 検索実行
        FillDataset(ConnectionManager.Instance.ConnectionString, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    "dsDataSet", paramList.ToArray)

        '戻る
        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>
    ''' [Fixed Data Section]TWO
    ''' </summary>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strBultukennNo">保証書NO</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/11/19 李宇(大連情報システム部) 新規作成</history>
    Public Function SelKihonInfoTwo(ByVal strKbn As String, ByVal strBultukennNo As String) As Data.DataTable

        'DataSetインスタンスの生成
        Dim dsDataSet As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT")
            .AppendLine("   j.irai_tantousya_mei AS attn --依頼担当者")
            .AppendLine("   ,k.kameiten_mei1 AS made --加盟店名")
            .AppendLine("   ,j.kbn+j.hosyousyo_no AS bukken_no --物件番号")
            .AppendLine("   ,j.sesyu_mei AS bukken_mei --施主名")
            .AppendLine("   ,j.bukken_jyuusyo1+j.bukken_jyuusyo2+j.bukken_jyuusyo3 AS bukken_jyuusyo --納入場所")
            .AppendLine("FROM  ")
            .AppendLine("   t_jiban j WITH(READCOMMITTED)")
            .AppendLine("   inner join m_kameiten k")
            .AppendLine("   on j.kameiten_cd = k.kameiten_cd")
            .AppendLine("WHERE")
            .AppendLine("   j.kbn = @kbn and j.hosyousyo_no=@hosyousyo_no")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, strKbn)) '区分
        paramList.Add(MakeParam("@hosyousyo_no", SqlDbType.VarChar, 10, strBultukennNo)) '保証書NO

        ' 検索実行
        FillDataset(ConnectionManager.Instance.ConnectionString, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    "dsDataSet", paramList.ToArray)

        '戻る
        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>
    ''' 御見積書のデータ
    ''' </summary>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strBultukennNo">物件番号</param>
    ''' <param name="flg">税抜と税込の区分</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/11/19 李宇(大連情報システム部) 新規作成</history>
    Public Function SelTyouhyouDate(ByVal strKbn As String, _
                                    ByVal strBultukennNo As String, _
                                    ByVal flg As String) As Data.DataTable

        'DataSetインスタンスの生成
        Dim dsDataSet As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT")
            .AppendLine("	jts.gamen_hyouji_no 画面表示NO")
            .AppendLine("	,jts.syouhin_cd 商品コード")
            .AppendLine("	,CASE WHEN jts.a+jts.b IS NULL THEN")
            .AppendLine("			s.syouhin_mei")
            .AppendLine("		ELSE")
            .AppendLine("			(s.syouhin_mei)+'['+jts.tys_houhou_mei_ryaku+']'")
            .AppendLine("		END AS syouhin_mei --商品名")
            .AppendLine("	,'１件' AS suuryou --数量")
            If flg = "税抜" Then
                .AppendLine("	,jts.uri_gaku AS tanka --税抜単価")
                .AppendLine("	,1 * (jts.uri_gaku) AS kingaku --金額")
                .AppendLine("	,jts.syouhizei_gaku AS syouhizei --消費税額")
            ElseIf flg = "税込" Then
                .AppendLine("	,((jts.uri_gaku) + (jts.syouhizei_gaku)) AS tanka  --税込単価")
                .AppendLine("	,1 * ((jts.uri_gaku) + (jts.syouhizei_gaku)) AS kingaku --金額")
                .AppendLine("	,0 AS syouhizei --消費税額")
            End If
            .AppendLine("   ,'' bikou --備考")
            .AppendLine("FROM m_syouhin s  --商品マスタ")
            .AppendLine("INNER JOIN ")
            .AppendLine("	(SELECT")
            .AppendLine("		ts.gamen_hyouji_no")
            .AppendLine("		,ts.syouhin_cd")
            .AppendLine("		,ts.uri_gaku")
            .AppendLine("		,ts.syouhizei_gaku")
            .AppendLine("		,ts.bunrui_cd")
            .AppendLine("		,mt.tys_houhou_mei_ryaku")
            .AppendLine("		,km1.code as a")
            .AppendLine("		,km2.code as b")
            .AppendLine("	FROM t_jiban j --地盤テーブル")
            .AppendLine("	INNER JOIN t_teibetu_seikyuu ts		--邸別請求テーブル")
            .AppendLine("		ON j.kbn =  ts.kbn ")
            .AppendLine("		AND j.hosyousyo_no= ts.hosyousyo_no")
            .AppendLine("	LEFT JOIN m_tyousahouhou mt			--調査方法マスタ")
            .AppendLine("		ON j.tys_houhou = mt.tys_houhou_no")
            .AppendLine("	LEFT JOIN m_kakutyou_meisyou AS km1 --拡張名称マスタ")
            .AppendLine("		ON km1.code = ts.syouhin_cd")
            .AppendLine("		AND km1.meisyou_syubetu = 26")
            .AppendLine("	LEFT JOIN m_kakutyou_meisyou AS km2 --拡張名称マスタ")
            .AppendLine("		ON km2.code = j.tys_houhou")
            .AppendLine("		AND km2.meisyou_syubetu = 27")
            .AppendLine("	where")
            .AppendLine("		j.kbn = @kbn ")
            .AppendLine("		AND j.hosyousyo_no = @hosyousyo_no ")
            .AppendLine("		AND ts.bunrui_cd IN('100','110','115','120') ")
            .AppendLine("		AND ts.uri_gaku <> '0'") '売上金額は'0'ではない
            .AppendLine("       AND ts.seikyuu_umu = '1'") '請求有無で請求有りの商品

            ''税率を表示させたい(407662_消費税増税対応_Earth) 李宇追加 2014/02/18↓
            '.AppendLine("       AND ts.uri_date IS NULL")
            ''税率を表示させたい(407662_消費税増税対応_Earth) 李宇追加 2014/02/18↑

            .AppendLine("	) jts")
            .AppendLine("ON jts.syouhin_cd = s.syouhin_cd")
            .AppendLine("ORDER BY bunrui_cd ASC")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, strKbn))                      '区分
        paramList.Add(MakeParam("@hosyousyo_no", SqlDbType.VarChar, 10, strBultukennNo)) '保証書NO

        ' 検索実行
        FillDataset(ConnectionManager.Instance.ConnectionString, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    "dsDataSet", paramList.ToArray)

        '戻る
        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>
    ''' 「モール展開」のセットを判斷する
    ''' </summary>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strBultukennNo">物件番号</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/12/02 李宇(大連情報システム部) 新規作成</history>
    Public Function SelMoruHandan(ByVal strKbn As String, ByVal strBultukennNo As String) As Data.DataTable

        'DataSetインスタンスの生成
        Dim dsDataSet As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT")
            .AppendLine("   mooru_tenkai_flg")
            .AppendLine("FROM  ")
            .AppendLine("   t_tyousa_mitumori_sakusei_kanri WITH(READCOMMITTED)")
            .AppendLine("WHERE")
            .AppendLine("   kbn = @kbn")
            .AppendLine("   AND hosyousyo_no = @hosyousyo_no")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, strKbn)) '区分
        paramList.Add(MakeParam("@hosyousyo_no", SqlDbType.VarChar, 10, strBultukennNo)) '保証書NO

        ' 検索実行
        FillDataset(ConnectionManager.Instance.ConnectionString, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    "dsDataSet", paramList.ToArray)

        '戻る
        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>
    ''' 税率を追加する
    ''' </summary>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2014/02/17 李宇(大連情報システム部) 新規作成</history>
    Public Function SelZeiritu(ByVal strKbn As String) As Data.DataTable

        'DataSetインスタンスの生成
        Dim dsDataSet As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT")
            .AppendLine("   meisyou")
            .AppendLine("FROM m_kakutyou_meisyou WITH(READCOMMITTED)")
            .AppendLine("WHERE 1 = 1")
            If strKbn.Equals("2") Then
                '税込
                .AppendLine("AND code = '2'")
            Else
                '税抜
                .AppendLine("AND code = '1'")
            End If
            .AppendLine("AND meisyou_syubetu ='63'")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@strKbn", SqlDbType.Char, 1, strKbn)) '区分

        ' 検索実行
        FillDataset(ConnectionManager.Instance.ConnectionString, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    "dsDataSet", paramList.ToArray)

        '戻る
        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>
    ''' システム時間
    ''' </summary>
    ''' <returns></returns>
    Public Function SelSysTime() As Date
        ' DataSetインスタンスの生成
        Dim dsDataSet As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'SQL文
        With commandTextSb
            .AppendLine("SELECT   ")
            .AppendLine("   GETDATE() ")
        End With

        ' 検索実行
        FillDataset(ConnectionManager.Instance.ConnectionString, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    "dsDataSet")

        '戻る
        Return CDate(dsDataSet.Tables(0).Rows(0).Item(0))

    End Function

    ''' <summary>
    ''' 「調査見積書_依頼担当者」を取得する
    ''' </summary>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strBultukennNo">保証書NO</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Public Function SelIraiTantousya(ByVal strKbn As String, ByVal strBultukennNo As String) As Data.DataTable

        'DataSetインスタンスの生成
        Dim dsDataSet As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT")
            .AppendLine("   ISNULL(ty.tys_mit_irai_tantousya_mei, '') AS tys_mit_irai_tantousya_mei")
            .AppendLine("FROM  ")
            .AppendLine("   t_tyousa_mitumori_sakusei_kanri ty WITH(READCOMMITTED)")
            .AppendLine("WHERE")
            .AppendLine("   ty.kbn = @kbn and ty.hosyousyo_no=@hosyousyo_no")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, strKbn)) '区分
        paramList.Add(MakeParam("@hosyousyo_no", SqlDbType.VarChar, 10, strBultukennNo)) '保証書NO

        ' 検索実行
        FillDataset(ConnectionManager.Instance.ConnectionString, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    "dsDataSet", paramList.ToArray)

        '戻る
        Return dsDataSet.Tables(0)

    End Function
End Class

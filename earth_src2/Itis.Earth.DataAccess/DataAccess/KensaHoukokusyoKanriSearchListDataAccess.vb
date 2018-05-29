Imports System.Data.SqlClient
Imports System.Text
Imports Itis.ApplicationBlocks.Data.SQLHelper

''' <summary>検査報告書管理</summary>
''' <remarks>検査報告書管理用機能を提供する</remarks>
''' <history>
''' <para>2015/12/04　高兵兵(大連情報システム部)　新規作成</para>
''' </history>
Public Class KensaHoukokusyoKanriSearchListDataAccess

    Private connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>
    ''' 検査報告書管理テーブルを取得する
    ''' </summary>
    ''' <param name="strKakunouDateFrom">格納日From</param>
    ''' <param name="strKakunouDateTo">格納日To</param>
    ''' <param name="strSendDateFrom">発送日From</param>
    ''' <param name="strSendDateTo">発送日To</param>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strNoFrom">番号From</param>
    ''' <param name="strNoTo">番号To</param>
    ''' <param name="strKameitenCdFrom">加盟店CD</param>
    ''' <param name="strKensakuJyouken">検索上限件数</param>
    ''' <param name="blnKensakuTaisyouGai">取消は検索対象外</param>
    ''' <param name="blnSendDateTaisyouGai">発送日セット済みは対象外</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <para>2015/12/04　高兵兵(大連情報システム部)　新規作成</para>
    Public Function SelKensaHoukokusyoKanriSearch(ByVal strKakunouDateFrom As String, ByVal strKakunouDateTo As String, ByVal strSendDateFrom As String, _
                                                  ByVal strSendDateTo As String, ByVal strKbn As String, ByVal strNoFrom As String, _
                                                  ByVal strNoTo As String, ByVal strKameitenCdFrom As String, ByVal strKensakuJyouken As String, _
                                                  ByVal blnKensakuTaisyouGai As Boolean, ByVal blnSendDateTaisyouGai As Boolean) As Data.DataTable

        'DataSetインスタンスの生成
        Dim dsReturn As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            '検索上限件数
            If strKensakuJyouken.Trim.Equals("10") Then
                .AppendLine("   TOP 10 ")
            End If
            If strKensakuJyouken.Trim.Equals("100") Then
                .AppendLine("   TOP 100 ")
            End If
            .AppendLine("	kanri_no   ")
            .AppendLine("	,kbn   ")
            .AppendLine("	,hosyousyo_no   ")
            .AppendLine("	,kameiten_cd   ")
            .AppendLine("	,kakunou_date   ")
            .AppendLine("	,kameiten_mei   ")
            .AppendLine("	,sesyu_mei   ")
            .AppendLine("	,CASE torikesi WHEN '1' THEN '取消' ELSE '' end as torikesi   ")
            .AppendLine("	,kensa_hkks_busuu   ")
            .AppendLine("	,kensa_hkks_jyuusyo1   ")
            .AppendLine("	,kensa_hkks_jyuusyo2   ")
            .AppendLine("	,yuubin_no   ")
            .AppendLine("	,tel_no   ")
            .AppendLine("	,busyo_mei   ")
            .AppendLine("	,todouhuken_cd   ")
            .AppendLine("	,todouhuken_mei   ")
            .AppendLine("	,hassou_date   ")
            .AppendLine("	,hassou_date_in_flg   ")
            .AppendLine("	,souhu_tantousya   ")
            .AppendLine("	,bukken_jyuusyo1   ")
            .AppendLine("	,bukken_jyuusyo2   ")
            .AppendLine("	,bukken_jyuusyo3   ")
            .AppendLine("	,tatemono_kouzou   ")
            .AppendLine("	,tatemono_kaisu   ")
            .AppendLine("	,fc_nm   ")
            .AppendLine("	,kameiten_tanto   ")
            .AppendLine("	,tatemono_kameiten_cd   ")
            .AppendLine("	,kanrihyou_out_flg   ")
            .AppendLine("	,kanrihyou_out_date   ")
            .AppendLine("	,souhujyou_out_flg   ")
            .AppendLine("	,souhujyou_out_date   ")
            .AppendLine("	,kensa_hkks_out_flg   ")
            .AppendLine("	,kensa_hkks_out_date   ")
            .AppendLine("	,tooshi_no   ")
            .AppendLine("	,kensa_koutei_nm1   ")
            .AppendLine("	,kensa_koutei_nm2   ")
            .AppendLine("	,kensa_koutei_nm3   ")
            .AppendLine("	,kensa_koutei_nm4   ")
            .AppendLine("	,kensa_koutei_nm5   ")
            .AppendLine("	,kensa_koutei_nm6   ")
            .AppendLine("	,kensa_koutei_nm7   ")
            .AppendLine("	,kensa_koutei_nm8   ")
            .AppendLine("	,kensa_koutei_nm9   ")
            .AppendLine("	,kensa_koutei_nm10   ")
            .AppendLine("	,kensa_start_jissibi1   ")
            .AppendLine("	,kensa_start_jissibi2   ")
            .AppendLine("	,kensa_start_jissibi3   ")
            .AppendLine("	,kensa_start_jissibi4   ")
            .AppendLine("	,kensa_start_jissibi5   ")
            .AppendLine("	,kensa_start_jissibi6   ")
            .AppendLine("	,kensa_start_jissibi7   ")
            .AppendLine("	,kensa_start_jissibi8   ")
            .AppendLine("	,kensa_start_jissibi9   ")
            .AppendLine("	,kensa_start_jissibi10   ")
            .AppendLine("	,kensa_in_nm1   ")
            .AppendLine("	,kensa_in_nm2   ")
            .AppendLine("	,kensa_in_nm3   ")
            .AppendLine("	,kensa_in_nm4   ")
            .AppendLine("	,kensa_in_nm5   ")
            .AppendLine("	,kensa_in_nm6   ")
            .AppendLine("	,kensa_in_nm7   ")
            .AppendLine("	,kensa_in_nm8   ")
            .AppendLine("	,kensa_in_nm9   ")
            .AppendLine("	,kensa_in_nm10   ")
            .AppendLine("	,add_login_user_id   ")
            .AppendLine("	,add_datetime   ")
            .AppendLine("	,upd_login_user_id   ")
            .AppendLine("	,upd_datetime   ")
            .AppendLine("FROM  t_kensa_hkks_kanri WITH(READCOMMITTED) ")
            .AppendLine("WHERE 1 = 1 ")

            '格納日
            If (Not strKakunouDateFrom.Trim.Equals(String.Empty)) AndAlso (Not strKakunouDateTo.Trim.Equals(String.Empty)) Then
                '格納日FROM、格納日TO両方が入力されている場合
                .AppendLine(" AND convert(varchar, kakunou_date,111) BETWEEN @kakunou_date_from AND @kakunou_date_to ")   '格納日
            Else
                '格納日FROMのみあるいは、格納日TOが入力されている場合
                If Not strKakunouDateFrom.Trim.Equals(String.Empty) Then
                    .AppendLine(" AND convert(varchar, kakunou_date,111) = @kakunou_date_from ")   '格納日From
                Else
                    If Not strKakunouDateTo.Trim.Equals(String.Empty) Then
                        .AppendLine(" AND convert(varchar, kakunou_date,111) = @kakunou_date_to ")   '格納日To
                    End If
                End If
            End If

            '発送日
            If (Not strSendDateFrom.Trim.Equals(String.Empty)) AndAlso (Not strSendDateTo.Trim.Equals(String.Empty)) Then
                '発送日FROM、発送日TO両方が入力されている場合
                .AppendLine(" AND convert(varchar, hassou_date,111) BETWEEN @hassou_date_from AND @hassou_date_to ")   '発送日
            Else
                '格納日FROMのみあるいは、格納日TOが入力されている場合
                If Not strSendDateFrom.Trim.Equals(String.Empty) Then
                    .AppendLine(" AND convert(varchar, hassou_date,111) = @hassou_date_from ")   '発送日From
                Else
                    If Not strSendDateTo.Trim.Equals(String.Empty) Then
                        .AppendLine(" AND convert(varchar, hassou_date,111) = @hassou_date_to ")   '発送日To
                    End If
                End If
            End If

            '区分
            If Not strKbn.Trim.Equals(String.Empty) Then
                '区分=入力の場合
                .AppendLine(" AND kbn = @kbn ")   '区分
            End If

            '番号
            If (Not strNoFrom.Trim.Equals(String.Empty)) AndAlso (Not strNoTo.Trim.Equals(String.Empty)) Then
                '番号FROM、番号TO両方が入力されている場合
                .AppendLine(" AND hosyousyo_no BETWEEN @hosyousyo_no_from AND @hosyousyo_no_to ")   '番号
            Else
                '番号FROMのみあるいは、番号TOが入力されている場合
                If Not strNoFrom.Trim.Equals(String.Empty) Then
                    .AppendLine(" AND hosyousyo_no = @hosyousyo_no_from ")   '番号From
                Else
                    If Not strNoTo.Trim.Equals(String.Empty) Then
                        .AppendLine(" AND hosyousyo_no = @hosyousyo_no_to ")   '番号To
                    End If
                End If
            End If

            '加盟店CD
            If Not strKameitenCdFrom.Equals(String.Empty) Then
                '調査方法=入力の場合
                .AppendLine(" AND kameiten_cd = @kameiten_cd ")   '加盟店CD
            End If

            If blnKensakuTaisyouGai.Equals(True) Then
                '取消は検索対象外=チェックの場合
                .AppendLine(" AND torikesi = 0 ")
            End If
            If blnSendDateTaisyouGai.Equals(True) Then
                '発送日セット済みは対象外=チェックの場合
                .AppendLine(" AND hassou_date  IS NULL ")
                .AppendLine(" AND hassou_date_in_flg <> 1 ")
            End If
            .AppendLine("ORDER BY ")
            .AppendLine("	kanri_no ") '管理No
            .AppendLine("	,kbn ") '区分
            .AppendLine("	,hosyousyo_no ") '保証書NO
            .AppendLine("	,kameiten_cd ")  '加盟店コード        
        End With
        '格納日
        If Not strKakunouDateFrom.Equals(String.Empty) Then
            paramList.Add(MakeParam("@kakunou_date_from", SqlDbType.DateTime, 30, Convert.ToDateTime(CDate(strKakunouDateFrom))))
        End If
        If Not strKakunouDateTo.Equals(String.Empty) Then
            paramList.Add(MakeParam("@kakunou_date_to", SqlDbType.DateTime, 30, Convert.ToDateTime(CDate(strKakunouDateTo))))
        End If
        '発送日
        If Not strSendDateFrom.Equals(String.Empty) Then
            paramList.Add(MakeParam("@hassou_date_from", SqlDbType.DateTime, 30, Convert.ToDateTime(CDate(strSendDateFrom))))
        End If
        If Not strSendDateTo.Equals(String.Empty) Then
            paramList.Add(MakeParam("@hassou_date_to", SqlDbType.DateTime, 30, Convert.ToDateTime(CDate(strSendDateTo))))
        End If
        '区分
        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, strKbn))
        '番号
        paramList.Add(MakeParam("@hosyousyo_no_from", SqlDbType.VarChar, 10, strNoFrom))
        paramList.Add(MakeParam("@hosyousyo_no_to", SqlDbType.VarChar, 10, strNoTo))
        '加盟店
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCdFrom))

        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "KensaHoukokusyo", paramList.ToArray)

        Return dsReturn.Tables("KensaHoukokusyo")

    End Function

    ''' <summary>
    ''' 検査報告書管理件数を取得する
    ''' </summary>
    ''' <param name="strKakunouDateFrom">格納日From</param>
    ''' <param name="strKakunouDateTo">格納日To</param>
    ''' <param name="strSendDateFrom">発送日From</param>
    ''' <param name="strSendDateTo">発送日To</param>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strNoFrom">番号From</param>
    ''' <param name="strNoTo">番号To</param>
    ''' <param name="strKameitenCdFrom">加盟店CD</param>
    ''' <param name="strKensakuJyouken">検索上限件数</param>
    ''' <param name="blnKensakuTaisyouGai">取消は検索対象外</param>
    ''' <param name="blnSendDateTaisyouGai">発送日セット済みは対象外</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <para>2015/12/04　高兵兵(大連情報システム部)　新規作成</para>
    Public Function SelKensaHoukokusyoKanriSearchCount(ByVal strKakunouDateFrom As String, ByVal strKakunouDateTo As String, ByVal strSendDateFrom As String, _
                                                  ByVal strSendDateTo As String, ByVal strKbn As String, ByVal strNoFrom As String, _
                                                  ByVal strNoTo As String, ByVal strKameitenCdFrom As String, ByVal strKensakuJyouken As String, _
                                                  ByVal blnKensakuTaisyouGai As Boolean, ByVal blnSendDateTaisyouGai As Boolean) As Integer

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	COUNT(kanri_no) ")  '--件数
            .AppendLine("FROM ")
            .AppendLine("	t_kensa_hkks_kanri WITH(READCOMMITTED) ")    '--検査報告書管理T
            .AppendLine("WHERE 1 = 1 ")

            '格納日
            If (Not strKakunouDateFrom.Trim.Equals(String.Empty)) AndAlso (Not strKakunouDateTo.Trim.Equals(String.Empty)) Then
                '格納日FROM、格納日TO両方が入力されている場合
                .AppendLine(" AND convert(varchar, kakunou_date,111) BETWEEN @kakunou_date_from AND @kakunou_date_to ")   '格納日
            Else
                '格納日FROMのみあるいは、格納日TOが入力されている場合
                If Not strKakunouDateFrom.Trim.Equals(String.Empty) Then
                    .AppendLine(" AND convert(varchar, kakunou_date,111) = @kakunou_date_from ")   '格納日From
                Else
                    If Not strKakunouDateTo.Trim.Equals(String.Empty) Then
                        .AppendLine(" AND convert(varchar, kakunou_date,111) = @kakunou_date_to ")   '格納日To
                    End If
                End If
            End If

            '発送日
            If (Not strSendDateFrom.Trim.Equals(String.Empty)) AndAlso (Not strSendDateTo.Trim.Equals(String.Empty)) Then
                '発送日FROM、発送日TO両方が入力されている場合
                .AppendLine(" AND convert(varchar, hassou_date,111) BETWEEN @hassou_date_from AND @hassou_date_to ")   '発送日
            Else
                '格納日FROMのみあるいは、格納日TOが入力されている場合
                If Not strSendDateFrom.Trim.Equals(String.Empty) Then
                    .AppendLine(" AND convert(varchar, hassou_date,111) = @hassou_date_from ")   '発送日From
                Else
                    If Not strSendDateTo.Trim.Equals(String.Empty) Then
                        .AppendLine(" AND convert(varchar, hassou_date,111) = @hassou_date_to ")   '発送日To
                    End If
                End If
            End If

            '区分
            If Not strKbn.Trim.Equals(String.Empty) Then
                '区分=入力の場合
                .AppendLine(" AND kbn = @kbn ")   '区分
            End If

            '番号
            If (Not strNoFrom.Trim.Equals(String.Empty)) AndAlso (Not strNoTo.Trim.Equals(String.Empty)) Then
                '番号FROM、番号TO両方が入力されている場合
                .AppendLine(" AND hosyousyo_no BETWEEN @hosyousyo_no_from AND @hosyousyo_no_to ")   '番号
            Else
                '番号FROMのみあるいは、番号TOが入力されている場合
                If Not strNoFrom.Trim.Equals(String.Empty) Then
                    .AppendLine(" AND hosyousyo_no = @hosyousyo_no_from ")   '番号From
                Else
                    If Not strNoTo.Trim.Equals(String.Empty) Then
                        .AppendLine(" AND hosyousyo_no = @hosyousyo_no_to ")   '番号To
                    End If
                End If
            End If

            '加盟店CD
            If Not strKameitenCdFrom.Equals(String.Empty) Then
                '調査方法=入力の場合
                .AppendLine(" AND kameiten_cd = @kameiten_cd ")   '加盟店CD
            End If

            If blnKensakuTaisyouGai.Equals(True) Then
                '取消は検索対象外=チェックの場合
                .AppendLine(" AND torikesi = 0 ")
            End If
            If blnSendDateTaisyouGai.Equals(True) Then
                '発送日セット済みは対象外=チェックの場合
                .AppendLine(" AND hassou_date  IS NULL ")
                .AppendLine(" AND hassou_date_in_flg <> 1 ")
            End If

        End With
        '格納日
        If Not strKakunouDateFrom.Equals(String.Empty) Then
            paramList.Add(MakeParam("@kakunou_date_from", SqlDbType.DateTime, 30, Convert.ToDateTime(CDate(strKakunouDateFrom))))
        End If
        If Not strKakunouDateTo.Equals(String.Empty) Then
            paramList.Add(MakeParam("@kakunou_date_to", SqlDbType.DateTime, 30, Convert.ToDateTime(CDate(strKakunouDateTo))))
        End If
        '発送日
        If Not strSendDateFrom.Equals(String.Empty) Then
            paramList.Add(MakeParam("@hassou_date_from", SqlDbType.DateTime, 30, Convert.ToDateTime(CDate(strSendDateFrom))))
        End If
        If Not strSendDateTo.Equals(String.Empty) Then
            paramList.Add(MakeParam("@hassou_date_to", SqlDbType.DateTime, 30, Convert.ToDateTime(CDate(strSendDateTo))))
        End If
        '区分
        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, strKbn))
        '番号
        paramList.Add(MakeParam("@hosyousyo_no_from", SqlDbType.VarChar, 10, strNoFrom))
        paramList.Add(MakeParam("@hosyousyo_no_to", SqlDbType.VarChar, 10, strNoTo))
        '加盟店
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCdFrom))

        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "KensaHoukokusyo", paramList.ToArray)

        Return CInt(dsReturn.Tables(0).Rows(0).Item(0).ToString.Trim)

    End Function

    ''' <summary>
    ''' <summary>検査報告書管理を更新する</summary>
    ''' </summary>
    ''' <param name="strHassoudate">発送日</param>
    ''' <param name="strSouhutantousya">送付担当者</param>
    ''' <param name="strUserId">更新ログインユーザID</param>
    ''' <param name="dtKensa">検査報告書管理テーブル</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <para>2015/12/04　高兵兵(大連情報システム部)　新規作成</para>
    Public Function UpdKensahoukokusho(ByVal strHassoudate As String, ByVal strSouhutantousya As String, ByVal strUserId As String, ByVal dtKensa As DataTable) As Boolean

        '戻り値
        Dim UpdCount As Integer = 0

        Dim commandTextSb As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("UPDATE  ")
            .AppendLine("	t_kensa_hkks_kanri WITH(UPDLOCK) ")
            .AppendLine("SET  ")
            .AppendLine("	hassou_date = @hassou_date ")   '発送日
            .AppendLine("	,hassou_date_in_flg = '1' ")   '発送日入力フラグ
            .AppendLine("	,souhu_tantousya = @souhu_tantousya ")   '送付担当者
            .AppendLine("	,upd_login_user_id = @upd_login_user_id ")  '更新ログインユーザID
            .AppendLine("	,upd_datetime  = GETDATE() ")   '更新日時
            .AppendLine("WHERE  ")
            .AppendLine("	kanri_no = @kanri_no ")  '管理No
            .AppendLine("	AND ")
            .AppendLine("	kbn = @kbn ")  '区分
            .AppendLine("	AND ")
            .AppendLine("	hosyousyo_no = @hosyousyo_no ") '保証書NO
            .AppendLine("	AND ")
            .AppendLine("	kameiten_cd = @kameiten_cd ")  '加盟店コード

        End With

        For i As Integer = 0 To dtKensa.Rows.Count - 1
            paramList.Clear()
            'パラメータの設定
            paramList.Add(MakeParam("@hassou_date", SqlDbType.DateTime, 30, Convert.ToDateTime(CDate(strHassoudate))))
            paramList.Add(MakeParam("@souhu_tantousya", SqlDbType.VarChar, 128, strSouhutantousya)) '送付担当者	
            paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, strUserId)) '更新ログインユーザID
            paramList.Add(MakeParam("kanri_no", SqlDbType.Int, 1000, dtKensa.Rows(i).Item("kanri_no").ToString.Trim))  '管理No
            paramList.Add(MakeParam("kbn", SqlDbType.Char, 1, dtKensa.Rows(i).Item("kbn").ToString.Trim))  '区分
            paramList.Add(MakeParam("hosyousyo_no", SqlDbType.VarChar, 10, dtKensa.Rows(i).Item("hosyousyo_no").ToString.Trim))  '保証書NO
            paramList.Add(MakeParam("kameiten_cd", SqlDbType.VarChar, 5, dtKensa.Rows(i).Item("kameiten_cd").ToString.Trim))  '加盟店コード

            '更新されたデータセットを DB へ書き込み
            Try
                UpdCount = ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)
            Catch ex As Exception
                Return False
            End Try

            If Not (UpdCount > 0) Then
                Return False
            End If

        Next
        Return True
    End Function

    ''' <summary>
    ''' <summary>検査報告書管理を更新する</summary>
    ''' </summary>
    ''' <param name="strUserId">更新ログインユーザID</param>
    ''' <param name="dtKensa">検査報告書管理テーブル</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <para>2015/12/04　高兵兵(大連情報システム部)　新規作成</para>
    Public Function UpdKensahoukokushoTorikesi(ByVal strUserId As String, ByVal dtKensa As DataTable) As Boolean

        '戻り値
        Dim UpdCount As Integer = 0

        Dim commandTextSb As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("UPDATE  ")
            .AppendLine("	t_kensa_hkks_kanri WITH(UPDLOCK) ")
            .AppendLine("SET  ")
            .AppendLine("	torikesi = '1' ")   '取消
            .AppendLine("	,upd_login_user_id = @upd_login_user_id ")  '更新ログインユーザID
            .AppendLine("	,upd_datetime  = GETDATE() ")   '更新日時
            .AppendLine("WHERE  ")
            .AppendLine("	kanri_no = @kanri_no ")  '管理No
            .AppendLine("	AND ")
            .AppendLine("	kbn = @kbn ")  '区分
            .AppendLine("	AND ")
            .AppendLine("	hosyousyo_no = @hosyousyo_no ") '保証書NO
            .AppendLine("	AND ")
            .AppendLine("	kameiten_cd = @kameiten_cd ")  '加盟店コード

        End With

        For i As Integer = 0 To dtKensa.Rows.Count - 1
            paramList.Clear()
            paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, strUserId)) '更新ログインユーザID
            paramList.Add(MakeParam("kanri_no", SqlDbType.Int, 1000, dtKensa.Rows(i).Item("kanri_no").ToString.Trim))  '管理No
            paramList.Add(MakeParam("kbn", SqlDbType.Char, 1, dtKensa.Rows(i).Item("kbn").ToString.Trim))  '区分
            paramList.Add(MakeParam("hosyousyo_no", SqlDbType.VarChar, 10, dtKensa.Rows(i).Item("hosyousyo_no").ToString.Trim))  '保証書NO
            paramList.Add(MakeParam("kameiten_cd", SqlDbType.VarChar, 5, dtKensa.Rows(i).Item("kameiten_cd").ToString.Trim))  '加盟店コード

            '更新されたデータセットを DB へ書き込み
            Try
                UpdCount = ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)
            Catch ex As Exception
                Return False
            End Try

            If Not (UpdCount > 0) Then
                Return False
            End If
        Next

        Return True

    End Function

    ''' <summary>
    ''' 加盟店マスタを取得する
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店CD</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <para>2015/12/04　高兵兵(大連情報システム部)　新規作成</para>
    Public Function SelMkameiten(ByVal strKameitenCd As String) As Data.DataTable

        'DataSetインスタンスの生成
        Dim dsReturn As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("		SELECT  ")
            .AppendLine("			kameiten_mei1 ")  '--加盟店名1
            .AppendLine("		FROM  ")
            .AppendLine("			m_kameiten  WITH(READCOMMITTED) ")    '--加盟店マスタ
            .AppendLine("WHERE 1 = 1 ")

            '加盟店コード  
            If Not strKameitenCd.Trim.Equals(String.Empty) Then
                '加盟店コード  =入力の場合
                .AppendLine(" AND kameiten_cd = @kameiten_cd ")   '加盟店コード      
            End If
            .AppendLine("ORDER BY kameiten_cd ") '加盟店コード
        End With

        '加盟店CD
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))
        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "Mkameiten", paramList.ToArray)

        Return dsReturn.Tables("Mkameiten")

    End Function

End Class

Imports System.Data.SqlClient
Imports System.Text
Imports Itis.ApplicationBlocks.Data.SQLHelper

''' <summary>検査報告書_各帳票出力画面</summary>
''' <remarks>検査報告書_各帳票出力画面用機能を提供する</remarks>
''' <history>
''' <para>2015/12/15　高兵兵(大連情報システム部)　新規作成</para>
''' </history>
Public Class KensaHoukokusyoOutputDataAccess

    Private connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>
    ''' 検査報告書管理テーブルを取得する
    ''' </summary>
    ''' <param name="strSendDateFrom">発送日From</param>
    ''' <param name="strSendDateTo">発送日To</param>
    ''' <param name="strKbn">区分</param>
    ''' <param name="tbxNoTo">番号From</param>
    ''' <param name="strNoTo">番号To</param>
    ''' <param name="strKameitenCdFrom">加盟店CD</param>
    ''' <param name="strKensakuJyouken">検索上限件数</param>
    ''' <param name="blnKensakuTaisyouGai">取消は検索対象外</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <para>2015/12/15　高兵兵(大連情報システム部)　新規作成</para>
    Public Function SelKensaHoukokusyoKanriSearch(ByVal strSendDateFrom As String, ByVal strSendDateTo As String, ByVal strKbn As String, _
                                                  ByVal tbxNoTo As String, ByVal strNoTo As String, ByVal strKameitenCdFrom As String, _
                                                  ByVal strKensakuJyouken As String, ByVal blnKensakuTaisyouGai As Boolean) As Data.DataTable

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
            .AppendLine("	 kanri_no   ")
            .AppendLine("	,kbn   ")
            .AppendLine("	,hosyousyo_no   ")
            .AppendLine("	,kameiten_cd   ")
            .AppendLine("	,kameiten_mei   ")
            .AppendLine("	,sesyu_mei   ")
            .AppendLine("	,kensa_hkks_busuu  ")
            .AppendLine("	,convert(varchar,kakunou_date,111) as kakunou_date   ")
            .AppendLine("	,convert(varchar,hassou_date,111) as hassou_date   ")
            .AppendLine("	,kanrihyou_out_flg   ")
            .AppendLine("	,souhujyou_out_flg   ")
            .AppendLine("	,kensa_hkks_out_flg   ")
            .AppendLine("	,souhu_tantousya   ")
            .AppendLine("FROM  t_kensa_hkks_kanri WITH(READCOMMITTED) ")
            .AppendLine("WHERE 1 = 1 ")

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
            If (Not tbxNoTo.Trim.Equals(String.Empty)) AndAlso (Not strNoTo.Trim.Equals(String.Empty)) Then
                '番号FROM、番号TO両方が入力されている場合
                .AppendLine(" AND hosyousyo_no BETWEEN @hosyousyo_no_from AND @hosyousyo_no_to ")   '番号
            Else
                '番号FROMのみあるいは、番号TOが入力されている場合
                If Not tbxNoTo.Trim.Equals(String.Empty) Then
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
            .AppendLine("ORDER BY ")
            .AppendLine("	kanri_no ") '管理No
            .AppendLine("	,kbn ") '区分
            .AppendLine("	,hosyousyo_no ") '保証書NO
            .AppendLine("	,kameiten_cd ")  '加盟店コード        
        End With
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
        paramList.Add(MakeParam("@hosyousyo_no_from", SqlDbType.VarChar, 10, tbxNoTo))
        paramList.Add(MakeParam("@hosyousyo_no_to", SqlDbType.VarChar, 10, strNoTo))
        '加盟店
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCdFrom))

        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "KensaHoukokusyo", paramList.ToArray)

        Return dsReturn.Tables("KensaHoukokusyo")

    End Function

    ''' <summary>
    ''' 管理表EXCEL出力を取得する
    ''' </summary>
    ''' <param name="strKanriNo">管理No</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <para>2015/12/15　高兵兵(大連情報システム部)　新規作成</para>
    Public Function SelKanrihyouExcelInfo(ByVal strKanriNo As String) As Data.DataTable

        'DataSetインスタンスの生成
        Dim dsReturn As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	kensa_hkks_busuu ") '--検査報告書発行部数 ")
            .AppendLine("	,kensa_hkks_jyuusyo1 ") '--検査報告書送付先住所1 ")
            .AppendLine("	,kameiten_mei ") '--加盟店名 ")
            .AppendLine("	,'' AS check1 ") '--【チェック】空欄 ： 固定 ")
            .AppendLine("	,sesyu_mei ") '--施主名 ")
            .AppendLine("	,kbn ") '--区分 ")
            .AppendLine("	,hosyousyo_no ") '--保証書NO ")
            .AppendLine("	,CONVERT(VARCHAR(10),hassou_date,111) AS hassou_date ") '--発送日 ")
            .AppendLine("	,'' AS tyousa_gaiyou ") '--調査概要 ")
            .AppendLine("	,'' AS bikou ") '--備考 ")
            .AppendLine("	,'' AS technical_guide ") '--テクニカルガイド ")
            .AppendLine("	,'' AS check2 ") '--チェック ")
            .AppendLine("	,'' AS hassou_syuubetu ") '--発送種別 ")
            .AppendLine("	,'' AS hassou_hiyou ") '--発送費用 ")
            .AppendLine("	,kameiten_tanto ") '--依頼担当者名 ")
            .AppendLine("	,kameiten_cd ") '--加盟店コード ")
            .AppendLine("	,busyo_mei ") '--部署名 ")
            .AppendLine("	,yuubin_no ") '--郵便番号 ")
            .AppendLine("	,kensa_hkks_jyuusyo2 ") '--検査報告書送付先住所2 ")
            .AppendLine("	,tel_no ") '--電話番号 ")
            .AppendLine("	,kbn + kameiten_cd + '-' + kbn + hosyousyo_no + '.pdf' ") '--ファイル名(区分＆加盟店コード＆「-」&区分＆保証書番号＆「.pdf」) ")
            .AppendLine("	,tooshi_no ") '--通しNo ")
            .AppendLine("	,todouhuken_cd ") '--都道府県コード ")
            .AppendLine("	,todouhuken_mei ") '--都道府県名 ")
            .AppendLine("FROM ")
            .AppendLine("	t_kensa_hkks_kanri WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	kanri_no IN (" & strKanriNo & ") ")
            .AppendLine("ORDER BY ")
            .AppendLine("	hassou_date ASC ")
            .AppendLine("	,kameiten_cd ASC ")
            .AppendLine("	,hosyousyo_no ASC ")
        End With

        '検索実行
        Itis.ApplicationBlocks.Data.SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "KanrihyouExcelInfo", paramList.ToArray)

        Return dsReturn.Tables("KanrihyouExcelInfo")

    End Function

    ''' <summary>
    ''' 送付状PDF出力を取得する
    ''' </summary>
    ''' <param name="strKanriNo">管理No</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <para>2015/12/15　高兵兵(大連情報システム部)　新規作成</para>
    Public Function SelSyoufujyouPdfInfo(ByVal strKanriNo As String) As Data.DataTable

        'DataSetインスタンスの生成
        Dim dsReturn As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	TKHK.kanri_no  ") '--管理No	 ")
            .AppendLine("	,TKHK.kameiten_cd  ") '--加盟店コード ")
            .AppendLine("	,TKHK.tooshi_no  ") '--通しNo ")
            .AppendLine("	,TKHK.kameiten_mei  ") '--加盟店名 ")
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN ISNULL(TKHK.kensa_hkks_jyuusyo1,'') <> '' THEN ")
            .AppendLine("			TKHK.kensa_hkks_jyuusyo1 ")
            .AppendLine("		ELSE ")
            .AppendLine("			MKJ.jyuusyo1 ")
            .AppendLine("		END AS jyuusyo1  ") '--住所1 ")
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN ISNULL(TKHK.kensa_hkks_jyuusyo1,'') <> '' THEN ")
            .AppendLine("			TKHK.kensa_hkks_jyuusyo2 ")
            .AppendLine("		ELSE ")
            .AppendLine("			MKJ.jyuusyo2 ")
            .AppendLine("		END AS jyuusyo2  ") '--住所2 ")
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN ISNULL(TKHK.kensa_hkks_jyuusyo1,'') <> '' THEN ")
            .AppendLine("			TKHK.busyo_mei ")
            .AppendLine("		ELSE ")
            .AppendLine("			MKJ.busyo_mei ")
            .AppendLine("		END AS busyo_mei  ") '--部署名 ")
            .AppendLine("	,TKHK.yuubin_no  ") '--郵便番号 ")
            .AppendLine("	,TKHK.tel_no  ") '--電話番号 ")
            .AppendLine("	,TKHK.hassou_date  ") '--発送日 ")
            .AppendLine("	,TKHK.souhu_tantousya  ") '--送付担当者 ")
            .AppendLine("	,TKHK.kbn  ") '--区分 ")
            .AppendLine("	,TKHK.hosyousyo_no  ") '--保証書NO ")
            .AppendLine("	,TKHK.sesyu_mei  ") '--施主名 ")
            .AppendLine("	,TKHK.kensa_hkks_busuu  ") '--検査報告書発行部数 ")
            .AppendLine("FROM ")
            .AppendLine("	t_kensa_hkks_kanri as TKHK WITH(READCOMMITTED) ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_kameiten_jyuusyo AS MKJ WITH(READCOMMITTED)  ") '--加盟店住所マスタ ")
            .AppendLine("		ON ")
            .AppendLine("		MKJ.kameiten_cd = TKHK.kameiten_cd ")
            .AppendLine("		AND ")
            .AppendLine("		MKJ.jyuusyo_no = 1 ")
            .AppendLine("WHERE ")
            .AppendLine("	TKHK.kanri_no IN (" & strKanriNo & ") ")
            .AppendLine("ORDER BY ")
            .AppendLine("	TKHK.tooshi_no  ") '--通しNo ")
            .AppendLine("	,TKHK.hosyousyo_no  ") '--保証書NO ")
        End With

        '検索実行
        Itis.ApplicationBlocks.Data.SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "SyoufujyouPdfInfo", paramList.ToArray)

        Return dsReturn.Tables("SyoufujyouPdfInfo")

    End Function

    ''' <summary>
    ''' 報告書PDF出力を取得する
    ''' </summary>
    ''' <param name="strKanriNo">管理No</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <para>2015/12/15　高兵兵(大連情報システム部)　新規作成</para>
    Public Function SelHoukokusyoPdfInfo(ByVal strKanriNo As String) As Data.DataTable

        'DataSetインスタンスの生成
        Dim dsReturn As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	kanri_no  ") '--管理No ")
            .AppendLine("	,kbn  ") '--区分 ")
            .AppendLine("	,kameiten_cd  ") '--加盟店コード ")
            .AppendLine("	,hosyousyo_no  ") '--保証書No ")
            .AppendLine("	,sesyu_mei  ") '--施主名 ")
            .AppendLine("	,kameiten_mei  ") '--加盟店名 ")
            .AppendLine("	,bukken_jyuusyo1  ") '--物件住所1 ")
            .AppendLine("	,bukken_jyuusyo2  ") '--物件住所2 ")
            .AppendLine("	,bukken_jyuusyo3  ") '--物件住所3 ")
            .AppendLine("	,gaiyou_you  ") '--建物構造_概要用 ")
            .AppendLine("	,tatemono_kaisu  ") '--建物階数 ")
            .AppendLine("	,kensa_koutei_nm1  ") '--検査工程名1 ")
            .AppendLine("	,kensa_koutei_nm2  ") '--検査工程名2 ")
            .AppendLine("	,kensa_koutei_nm3  ") '--検査工程名3 ")
            .AppendLine("	,kensa_koutei_nm4  ") '--検査工程名4 ")
            .AppendLine("	,kensa_koutei_nm5  ") '--検査工程名5 ")
            .AppendLine("	,kensa_koutei_nm6  ") '--検査工程名6 ")
            .AppendLine("	,kensa_koutei_nm7  ") '--検査工程名7 ")
            .AppendLine("	,kensa_koutei_nm8  ") '--検査工程名8 ")
            .AppendLine("	,kensa_koutei_nm9  ") '--検査工程名9 ")
            .AppendLine("	,kensa_koutei_nm10  ") '--検査工程名10 ")
            .AppendLine("	,kensa_start_jissibi1  ") '--検査実施日1 ")
            .AppendLine("	,kensa_start_jissibi2  ") '--検査実施日2 ")
            .AppendLine("	,kensa_start_jissibi3  ") '--検査実施日3 ")
            .AppendLine("	,kensa_start_jissibi4  ") '--検査実施日4 ")
            .AppendLine("	,kensa_start_jissibi5  ") '--検査実施日5 ")
            .AppendLine("	,kensa_start_jissibi6  ") '--検査実施日6 ")
            .AppendLine("	,kensa_start_jissibi7  ") '--検査実施日7 ")
            .AppendLine("	,kensa_start_jissibi8  ") '--検査実施日8 ")
            .AppendLine("	,kensa_start_jissibi9  ") '--検査実施日9 ")
            .AppendLine("	,kensa_start_jissibi10  ") '--検査実施日10 ")
            .AppendLine("	,kensa_in_nm1  ") '--検査員名1 ")
            .AppendLine("	,kensa_in_nm2  ") '--検査員名2 ")
            .AppendLine("	,kensa_in_nm3  ") '--検査員名3 ")
            .AppendLine("	,kensa_in_nm4  ") '--検査員名4 ")
            .AppendLine("	,kensa_in_nm5  ") '--検査員名5 ")
            .AppendLine("	,kensa_in_nm6  ") '--検査員名6 ")
            .AppendLine("	,kensa_in_nm7  ") '--検査員名7 ")
            .AppendLine("	,kensa_in_nm8  ") '--検査員名8 ")
            .AppendLine("	,kensa_in_nm9  ") '--検査員名9 ")
            .AppendLine("	,kensa_in_nm10  ") '--検査員名10 ")
            .AppendLine("	,fc_nm  ") '--FC名 ")
            .AppendLine("FROM ")
            .AppendLine("	t_kensa_hkks_kanri WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	kanri_no = @kanri_no ")
        End With

        paramList.Add(Itis.ApplicationBlocks.Data.SQLHelper.MakeParam("@kanri_no", SqlDbType.Int, 10, strKanriNo))

        '検索実行
        Itis.ApplicationBlocks.Data.SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "HoukokusyoPdfInfo", paramList.ToArray)

        Return dsReturn.Tables("HoukokusyoPdfInfo")

    End Function

    ''' <summary>
    ''' 検査報告書管理件数を取得する
    ''' </summary>
    ''' <param name="strSendDateFrom">発送日From</param>
    ''' <param name="strSendDateTo">発送日To</param>
    ''' <param name="strKbn">区分</param>
    ''' <param name="tbxNoTo">番号From</param>
    ''' <param name="strNoTo">番号To</param>
    ''' <param name="strKameitenCdFrom">加盟店CD</param>
    ''' <param name="strKensakuJyouken">検索上限件数</param>
    ''' <param name="blnKensakuTaisyouGai">取消は検索対象外</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <para>2015/12/15　高兵兵(大連情報システム部)　新規作成</para>
    Public Function SelKensaHoukokusyoKanriSearchCount(ByVal strSendDateFrom As String, ByVal strSendDateTo As String, ByVal strKbn As String, _
                                                  ByVal tbxNoTo As String, ByVal strNoTo As String, ByVal strKameitenCdFrom As String, _
                                                  ByVal strKensakuJyouken As String, ByVal blnKensakuTaisyouGai As Boolean) As Integer

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
            If (Not tbxNoTo.Trim.Equals(String.Empty)) AndAlso (Not strNoTo.Trim.Equals(String.Empty)) Then
                '番号FROM、番号TO両方が入力されている場合
                .AppendLine(" AND hosyousyo_no BETWEEN @hosyousyo_no_from AND @hosyousyo_no_to ")   '番号
            Else
                '番号FROMのみあるいは、番号TOが入力されている場合
                If Not tbxNoTo.Trim.Equals(String.Empty) Then
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

        End With
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
        paramList.Add(MakeParam("@hosyousyo_no_from", SqlDbType.VarChar, 10, tbxNoTo))
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
    ''' <param name="strUserId">更新ログインユーザID</param>
    ''' <param name="dtKensa">検査報告書管理テーブル</param>
    ''' <param name="strFlg">ボタン区分(1:管理表EXCEL出力;2:送付状PDF出力;3:報告書PDF出力)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <para>2015/12/15　高兵兵(大連情報システム部)　新規作成</para>
    Public Function UpdKensahoukokusho(ByVal strUserId As String, ByVal dtKensa As DataTable, ByVal strFlg As String) As Boolean

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
            .AppendLine("	kensa_hkks_busuu = @kensa_hkks_busuu ")    '検査報告書発行部数
            If strFlg = "1" Then
                .AppendLine("	,kanrihyou_out_flg = '1' ")            '管理表出力フラグ
                .AppendLine("	,kanrihyou_out_date = GETDATE() ")     '管理表出力日
            ElseIf strFlg = "2" Then
                .AppendLine("	,souhujyou_out_flg = '1' ")            '送付状出力フラグ
                .AppendLine("	,souhujyou_out_date = GETDATE() ")     '送付状出力日
            Else
                .AppendLine("	,kensa_hkks_out_flg = '1' ")           '検査報告書出力フラグ
                .AppendLine("	,kensa_hkks_out_date = GETDATE() ")    '検査報告書出力日
            End If
            .AppendLine("	,tooshi_no = @tooshi_no ")                 '通しNo
            .AppendLine("	,upd_login_user_id = @upd_login_user_id ") '更新ログインユーザID
            .AppendLine("	,upd_datetime  = GETDATE() ")              '更新日時
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
            paramList.Add(MakeParam("@kensa_hkks_busuu", SqlDbType.Int, 1, dtKensa.Rows(i).Item("kensa_hkks_busuu").ToString.Trim)) '通しNo	
            paramList.Add(MakeParam("@tooshi_no", SqlDbType.Int, 1000, dtKensa.Rows(i).Item("tooshi_no").ToString.Trim)) '通しNo	
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
    ''' <para>2015/12/15　高兵兵(大連情報システム部)　新規作成</para>
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

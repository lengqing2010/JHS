Imports System.Data.SqlClient
Imports System.Text
Imports Itis.ApplicationBlocks.Data.SQLHelper

''' <summary>加盟店情報を新規登録する</summary>
''' <remarks>加盟店情報新規登録を提供する</remarks>
''' <history>
''' <para>2009/07/15　付龍(大連情報システム部)　新規作成</para>
''' </history>
Public Class KihonJyouhouInputDataAccess

    Private connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>加盟店コードの最大値を取得する</summary>
    ''' <param name="strKbn">パラメータ</param>
    ''' <returns>加盟店コード最大値データテーブル</returns>
    Public Function SelMaxKameitenCd(ByVal strKbn As String, _
                                    Optional ByVal strCdFrom As String = "", _
                                    Optional ByVal strCdTo As String = "") As KihonJyouhouInputDataSet.KameitenCdTableDataTable
        ' DataSetインスタンスの生成()
        Dim dsKihonJyouhouInputDataSet As New KihonJyouhouInputDataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   MAX(kameiten_cd) AS kameiten_cd ")
            .AppendLine("FROM ")
            .AppendLine("   m_kameiten WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("   kbn = @kbn ")
            .AppendLine("   AND UPPER(kameiten_cd) = LOWER(kameiten_cd) COLLATE Japanese_CS_AS_KS_WS ")
            If strCdFrom <> String.Empty Then
                .AppendLine("   AND kameiten_cd BETWEEN @kameiten_cd_from AND @kameiten_cd_to ")
                paramList.Add(MakeParam("@kameiten_cd_from", SqlDbType.VarChar, 5, strCdFrom))
                paramList.Add(MakeParam("@kameiten_cd_to", SqlDbType.VarChar, 5, strCdTo))
            End If
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, strKbn))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsKihonJyouhouInputDataSet, _
                    dsKihonJyouhouInputDataSet.KameitenCdTable.TableName, paramList.ToArray)

        Return dsKihonJyouhouInputDataSet.KameitenCdTable

    End Function

    ''' <summary>加盟店コードの最大値と採番設定の範囲を取得する</summary>
    ''' <param name="strKbn">区分</param>
    ''' <returns>加盟店コードの最大値と採番設定の範囲を取得する</returns>
    ''' <history>
    ''' <para>2012/11/19　車龍(大連情報システム部)　新規作成</para>
    ''' </history>
    Public Function SelMaxKameitenCd1(ByVal strKbn As String) As Data.DataTable

        ' DataSetインスタンスの生成()
        Dim dsKihonJyouhouInputDataSet As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	ISNULL(MK.kameiten_cd_max,'') AS kameiten_cd_max ")
            .AppendLine("	,ISNULL(MDK.kameiten_saiban_from,'') AS kameiten_saiban_from ")
            .AppendLine("	,ISNULL(MDK.kameiten_saiban_to,'') AS kameiten_saiban_to ")
            .AppendLine("FROM ")
            .AppendLine("	( ")
            .AppendLine("		SELECT ")
            .AppendLine("			@kbn AS kbn ")
            .AppendLine("			,( ")
            .AppendLine("				SELECT ")
            .AppendLine("					MAX(SUB_MK.kameiten_cd)  ")
            .AppendLine("				FROM ")
            .AppendLine("					m_kameiten AS SUB_MK WITH(READCOMMITTED) ")
            .AppendLine("					LEFT JOIN ")
            .AppendLine("					m_data_kbn AS SUB_MDK WITH(READCOMMITTED) ")
            .AppendLine("					ON ")
            .AppendLine("						SUB_MK.kbn = SUB_MDK.kbn ")
            .AppendLine("				WHERE ")
            .AppendLine("					SUB_MK.kbn = @kbn ")
            .AppendLine("					AND ")
            .AppendLine("					PATINDEX('%[^0-9]%',convert(VARCHAR(9),ISNULL(SUB_MK.kameiten_cd,''))) = 0 ")
            .AppendLine("					AND ")
            .AppendLine("					SUB_MK.kameiten_cd BETWEEN SUB_MDK.kameiten_saiban_from AND SUB_MDK.kameiten_saiban_to ")
            .AppendLine("				GROUP BY ")
            .AppendLine("					SUB_MK.kbn ")
            .AppendLine("			) AS kameiten_cd_max ")
            .AppendLine("	) AS MK ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_data_kbn AS MDK WITH(READCOMMITTED) ")
            .AppendLine("	ON ")
            .AppendLine("		MK.kbn = MDK.kbn ")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, strKbn))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsKihonJyouhouInputDataSet, _
                    "dtMaxKameitenCd", paramList.ToArray)

        Return dsKihonJyouhouInputDataSet.Tables("dtMaxKameitenCd")

    End Function

    ''' <summary>加盟店コードを取得する</summary>
    ''' <param name="strKbn">パラメータ</param>
    ''' <param name="strCd">パラメータ</param>
    ''' <returns>加盟店コードデータテーブル</returns>
    Public Function SelKameitenCd(ByVal strKbn As String, ByVal strCd As String) As KihonJyouhouInputDataSet.KameitenCdTableDataTable
        ' DataSetインスタンスの生成()
        Dim dsKihonJyouhouInputDataSet As New KihonJyouhouInputDataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   kameiten_cd ")
            .AppendLine("FROM ")
            .AppendLine("   m_kameiten WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("   kameiten_cd = @kameiten_cd ")
            If strKbn <> String.Empty Then
                .AppendLine("   AND kbn = @kbn ")
            End If
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strCd))
        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, strKbn))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsKihonJyouhouInputDataSet, _
                    dsKihonJyouhouInputDataSet.KameitenCdTable.TableName, paramList.ToArray)

        Return dsKihonJyouhouInputDataSet.KameitenCdTable

    End Function

    ''' <summary>加盟店マスタテーブルに登録する</summary>
    ''' <param name="dtParamKameitenInfo">パラメータ</param>
    ''' <returns>成否</returns>
    Public Function InsKameitenInfo(ByVal dtParamKameitenInfo As KihonJyouhouInputDataSet.Param_KameitenInfoDataTable) As Boolean
        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine(" INSERT INTO ")
            .AppendLine("   m_kameiten WITH(UPDLOCK) ")
            .AppendLine("   (kameiten_cd, ")
            .AppendLine("   kbn, ")
            .AppendLine("   torikesi, ")
            .AppendLine("   kameiten_mei1, ")
            .AppendLine("   tenmei_kana1, ")
            .AppendLine("   kameiten_mei2, ")
            .AppendLine("   tenmei_kana2, ")
            .AppendLine("   builder_no, ")
            .AppendLine("   keiretu_cd, ")
            .AppendLine("   eigyousyo_cd, ")
            .AppendLine("   th_kasi_cd, ")
            '==========================2012/03/28 車龍 405721案件の対応 追加↓==========================================
            commandTextSb.AppendLine(" torikesi_set_date, ")
            '==========================2012/03/28 車龍 405721案件の対応 追加↑==========================================
            .AppendLine("   add_login_user_id, ")
            .AppendLine("   add_datetime, ")
            .AppendLine("   upd_login_user_id, ")
            .AppendLine("   upd_datetime) ")
            .AppendLine(" VALUES( ")
            .AppendLine("   @kameiten_cd, ")
            .AppendLine("   @kbn, ")
            .AppendLine("   @torikesi, ")
            .AppendLine("   @kameiten_mei1, ")
            .AppendLine("   @tenmei_kana1, ")
            .AppendLine("   @kameiten_mei2, ")
            .AppendLine("   @tenmei_kana2, ")
            .AppendLine("   @builder_no, ")
            .AppendLine("   @keiretu_cd, ")
            .AppendLine("   @eigyousyo_cd, ")
            .AppendLine("   @th_kasi_cd, ")
            '==========================2012/03/28 車龍 405721案件の対応 追加↓==========================================
            If Not dtParamKameitenInfo(0).torikesi.ToString.Trim.Equals("0") Then
                commandTextSb.AppendLine(" GETDATE(), ")
            Else
                commandTextSb.AppendLine(" NULL, ")
            End If
            '==========================2012/03/28 車龍 405721案件の対応 追加↑==========================================
            .AppendLine("   @add_login_user_id, ")
            .AppendLine("   GETDATE(), ")
            .AppendLine("   @upd_login_user_id, ")
            .AppendLine("   GETDATE()) ")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).kameiten_cd))
        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, dtParamKameitenInfo(0).kbn))
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 1, dtParamKameitenInfo(0).torikesi))
        paramList.Add(MakeParam("@kameiten_mei1", SqlDbType.VarChar, 40, dtParamKameitenInfo(0).kameiten_mei1))
        paramList.Add(MakeParam("@tenmei_kana1", SqlDbType.VarChar, 20, dtParamKameitenInfo(0).tenmei_kana1))
        paramList.Add(MakeParam("@kameiten_mei2", SqlDbType.VarChar, 40, IIf(dtParamKameitenInfo(0).kameiten_mei2 = "", DBNull.Value, dtParamKameitenInfo(0).kameiten_mei2)))
        paramList.Add(MakeParam("@tenmei_kana2", SqlDbType.VarChar, 20, IIf(dtParamKameitenInfo(0).tenmei_kana2 = "", DBNull.Value, dtParamKameitenInfo(0).tenmei_kana2)))
        paramList.Add(MakeParam("@builder_no", SqlDbType.VarChar, 9, IIf(dtParamKameitenInfo(0).builder_no = "", DBNull.Value, dtParamKameitenInfo(0).builder_no)))
        paramList.Add(MakeParam("@keiretu_cd", SqlDbType.VarChar, 5, IIf(dtParamKameitenInfo(0).keiretu_cd = "", DBNull.Value, dtParamKameitenInfo(0).keiretu_cd)))
        paramList.Add(MakeParam("@eigyousyo_cd", SqlDbType.VarChar, 5, IIf(dtParamKameitenInfo(0).eigyousyo_cd = "", DBNull.Value, dtParamKameitenInfo(0).eigyousyo_cd)))
        paramList.Add(MakeParam("@th_kasi_cd", SqlDbType.VarChar, 7, IIf(dtParamKameitenInfo(0).th_kasi_cd = "", DBNull.Value, dtParamKameitenInfo(0).th_kasi_cd)))
        paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, dtParamKameitenInfo(0).add_login_user_id))
        paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, dtParamKameitenInfo(0).add_login_user_id))

        Try
            '更新されたデータセットを DB へ書き込み
            ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString, paramList.ToArray)
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function InsKameitenRenkeiInfo(ByVal dtParamKameitenInfo As KihonJyouhouInputDataSet.Param_KameitenInfoDataTable) As Boolean
        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine(" DELETE FROM ")
            .AppendLine("   m_kameiten_renkei ")
            .AppendLine(" WHERE ")
            .AppendLine("   kameiten_cd = @kameiten_cd ")
            .AppendLine(" INSERT INTO ")
            .AppendLine("   m_kameiten_renkei WITH(UPDLOCK) ")
            .AppendLine("   (kameiten_cd, ")
            .AppendLine("   renkei_siji_cd, ")
            .AppendLine("   sousin_jyky_cd, ")
            .AppendLine("   sousin_kanry_datetime, ")
            .AppendLine("   upd_login_user_id, ")
            .AppendLine("   upd_datetime) ")
            .AppendLine(" VALUES( ")
            .AppendLine("   @kameiten_cd, ")
            .AppendLine("   @renkei_siji_cd, ")
            .AppendLine("   @sousin_jyky_cd, ")
            .AppendLine("   @sousin_kanry_datetime, ")
            .AppendLine("   @upd_login_user_id, ")
            .AppendLine("   GETDATE()) ")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).kameiten_cd))
        paramList.Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 1, 1))
        paramList.Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 1, 0))
        paramList.Add(MakeParam("@sousin_kanry_datetime", SqlDbType.DateTime, 20, DBNull.Value))
        paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, dtParamKameitenInfo(0).add_login_user_id))

        Try
            '更新されたデータセットを DB へ書き込み
            ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString, paramList.ToArray)
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 「取消」ddlのデータを取得する
    ''' </summary>
    ''' <returns>「取消」ddlのデータテーブル</returns>
    ''' <remarks></remarks>
    ''' <history>2012/03/27 車龍 405721案件の対応 追加</history>
    Public Function SelTorikesiList(ByVal strCd As String) As Data.DataTable

        ' DataSetインスタンスの生成()
        Dim dsDataSet As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	code AS code  ")
            .AppendLine("	,(code + ':' + ISNULL(meisyou,'')) AS meisyou ")
            .AppendLine("FROM ")
            .AppendLine("	m_kakutyou_meisyou WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	meisyou_syubetu = @meisyou_syubetu ")
            If Not strCd.Equals(String.Empty) Then
                .AppendLine("	AND ")
                .AppendLine("	code = @code ")
            End If
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@meisyou_syubetu", SqlDbType.Int, 10, 56))
        paramList.Add(MakeParam("@code", SqlDbType.VarChar, 10, strCd))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dtTorikesi", paramList.ToArray)

        Return dsDataSet.Tables("dtTorikesi")

    End Function

End Class

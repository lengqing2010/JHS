Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Imports System.Data
Imports System.Text

''' <summary>
''' 計画管理用_加盟店備考情報
''' </summary>
''' <remarks></remarks>
Public Class KeikaikuKanriKameitenBikouInquiryDA

    ''' <summary>
    ''' 加盟店備考情報取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/02　車龍(大連情報システム部)　新規作成</history>
    Public Function SelBikouInfo(ByVal strKameitenCd As String) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                strKameitenCd)

        '戻りデータセット
        Dim dsReturn As New Data.DataSet

        'SQLコメント
        Dim commandTextSb As New StringBuilder

        'バラメタ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	MKKBS.bikou_syubetu ") '--備考種別 ")
            .AppendLine("	,ISNULL(MKM.meisyou,'') AS meisyou ") '--備考種別名 ")
            .AppendLine("	,MKKBS.nyuuryoku_no ") '--入力NO ")
            .AppendLine("	,ISNULL(MKKBS.naiyou,'') AS naiyou ") '--内容 ")
            .AppendLine("FROM ")
            .AppendLine("	m_keikaku_kameiten_bikou_settei AS MKKBS WITH(READCOMMITTED) ") '--計画管理_加盟店備考設定ﾏｽﾀ ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_keikakuyou_meisyou AS MKM WITH(READCOMMITTED) ") '--計画用_名称マスタ ")
            .AppendLine("	ON ")
            .AppendLine("		MKM.meisyou_syubetu = @meisyou_syubetu ")
            .AppendLine("		AND ")
            .AppendLine("		MKM.code = MKKBS.bikou_syubetu ")
            .AppendLine("WHERE ")
            .AppendLine("	MKKBS.kameiten_cd = @kameiten_cd ") '--加盟店コード ")
            .AppendLine("ORDER BY ")
            .AppendLine("	MKKBS.nyuuryoku_no ASC ") '--入力NO ")
        End With

        paramList.Add(MakeParam("@meisyou_syubetu", SqlDbType.VarChar, 2, "30"))
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))

        ' 検索実行
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtBikouInfo", paramList.ToArray)

        Return dsReturn.Tables("dtBikouInfo")

    End Function

    ''' <summary>
    ''' 加盟店備考更新日取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/02　車龍(大連情報システム部)　新規作成</history>
    Public Function SelKameitenBikouMaxUpdTime(ByVal strKameitenCd As String) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                strKameitenCd)

        '戻りデータセット
        Dim dsReturn As New Data.DataSet

        'SQLコメント
        Dim commandTextSb As New StringBuilder

        'バラメタ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT TOP 1 ")
            .AppendLine("	ISNULL(MAX(upd_datetime),MAX(add_datetime)) AS maxtime ")
            .AppendLine("	,ISNULL(upd_login_user_id,add_login_user_id) as theuser ")
            .AppendLine("FROM ")
            .AppendLine("	m_keikaku_kameiten_bikou_settei WITH (READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	kameiten_cd = @kameiten_cd ")
            .AppendLine("GROUP BY  ")
            .AppendLine("	upd_login_user_id ")
            .AppendLine("	,add_login_user_id ")
            .AppendLine("ORDER BY  ")
            .AppendLine("	maxtime DESC ")
        End With

        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))

        ' 検索実行
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtMaxUpdTime", paramList.ToArray)

        Return dsReturn.Tables("dtMaxUpdTime")

    End Function

    ''' <summary>
    ''' 加盟店種別取得
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Selkameitensyubetu(ByVal code As String) As String

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                code)

        ' DataSetインスタンスの生成
        Dim data As New DataSet

        ' SQL文の生成
        Dim sql As New StringBuilder

        ' パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        ' 加盟店マスト情報のSql 
        sql.AppendLine("SELECT          ")
        sql.AppendLine("code")
        sql.AppendLine(",meisyou")
        sql.AppendLine(" FROM            ")
        sql.AppendLine("  m_keikakuyou_meisyou WITH (READCOMMITTED)")

        sql.AppendLine("WHERE")
        sql.AppendLine(" meisyou_syubetu=@meisyou_syubetu ")
        sql.AppendLine(" and code = @code")
        paramList.Add(MakeParam("@meisyou_syubetu", SqlDbType.VarChar, 2, "30"))
        paramList.Add(MakeParam("@code", SqlDbType.Int, 4, code))


        ' 検索実行
        FillDataset(ManagerDA.Connection, CommandType.Text, sql.ToString(), data, "data", paramList.ToArray)

        If data.Tables(0).Rows.Count > 0 Then
            Return data.Tables(0).Rows(0).Item(1).ToString.Trim
        Else
            Return String.Empty
        End If

    End Function




    ''' <summary>
    ''' 備考追加
    ''' </summary>
    ''' <param name="dicPrm">追加データ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsBikou(ByVal dicPrm As Dictionary(Of String, String)) As Boolean

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                dicPrm)

        ' SQL文の生成
        Dim sql As New StringBuilder
        Dim intResult As Integer = 0
        ' パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With sql
            .AppendLine("DECLARE @nyuuryokuno int")
            .AppendLine("set @nyuuryokuno = (select isnull( (select max(nyuuryoku_no) from m_keikaku_kameiten_bikou_settei Where kameiten_cd = @kameiten_cd), 0)) ")

            .AppendLine("INSERT INTO ")
            .AppendLine("	m_keikaku_kameiten_bikou_settei WITH(UPDLOCK) ") '--計画管理_加盟店備考設定ﾏｽﾀ
            .AppendLine("	( ")
            .AppendLine("		kameiten_cd ")
            .AppendLine("		,bikou_syubetu ")
            .AppendLine("		,nyuuryoku_no ")
            .AppendLine("		,naiyou ")
            .AppendLine("		,kousinsya ")
            .AppendLine("		,add_login_user_id ")
            .AppendLine("		,add_datetime ")
            .AppendLine("		,upd_login_user_id ")
            .AppendLine("		,upd_datetime ")
            .AppendLine("	) ")
            .AppendLine("VALUES ")
            .AppendLine("( ")
            .AppendLine("	@kameiten_cd ")
            .AppendLine("	,@bikou_syubetu ")
            .AppendLine("	,@nyuuryokuno + 1 ")
            .AppendLine("	,@naiyou ")
            .AppendLine("	,@kousinsya ")
            .AppendLine("	,@add_login_user_id ")
            .AppendLine("	,GETDATE() ")
            .AppendLine("	,@add_login_user_id ")
            .AppendLine("	,GETDATE() ")
            .AppendLine(") ")
        End With

        'パラメータ
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, dicPrm("kameiten_cd")))
        paramList.Add(MakeParam("@bikou_syubetu", SqlDbType.VarChar, 5, dicPrm("bikou_syubetu")))
        paramList.Add(MakeParam("@naiyou", SqlDbType.VarChar, 80, dicPrm("naiyou")))
        paramList.Add(MakeParam("@kousinsya", SqlDbType.Int, 10, dicPrm("kousinsya")))
        paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, dicPrm("add_login_user_id")))

        ' クエリ実行
        Try
            intResult = ExecuteNonQuery(ManagerDA.Connection, CommandType.Text, sql.ToString(), paramList.ToArray)
        Catch ex As Exception
            Return False
        End Try

        If intResult = 0 Then
            Return False
        Else
            Return True
        End If

    End Function


    ''' <summary>
    ''' 備考更新
    ''' </summary>
    ''' <param name="dicPrm">追加データ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdBikou(ByVal dicPrm As Dictionary(Of String, String)) As Boolean

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                dicPrm)

        ' SQL文の生成
        Dim sql As New StringBuilder
        Dim intResult As Integer = 0
        ' パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With sql
            .AppendLine("UPDATE ")
            .AppendLine("	m_keikaku_kameiten_bikou_settei WITH(UPDLOCK) -- --計画管理_加盟店備考設定ﾏｽﾀ ")
            .AppendLine("SET ")
            .AppendLine("	bikou_syubetu = @bikou_syubetu ")
            .AppendLine("	,naiyou = @naiyou ")
            .AppendLine("	,kousinsya = @kousinsya ")
            .AppendLine("	,upd_login_user_id = @upd_login_user_id ")
            .AppendLine("	,upd_datetime = GETDATE() ")
            .AppendLine("WHERE ")
            .AppendLine("	kameiten_cd = @kameiten_cd ")
            .AppendLine("	AND ")
            .AppendLine("	nyuuryoku_no = @nyuuryoku_no ")
        End With

        'パラメータ
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, dicPrm("kameiten_cd")))
        paramList.Add(MakeParam("@bikou_syubetu", SqlDbType.VarChar, 5, dicPrm("bikou_syubetu")))
        paramList.Add(MakeParam("@nyuuryoku_no", SqlDbType.Int, 10, dicPrm("nyuuryoku_no")))
        paramList.Add(MakeParam("@naiyou", SqlDbType.VarChar, 80, dicPrm("naiyou")))
        paramList.Add(MakeParam("@kousinsya", SqlDbType.Int, 10, dicPrm("kousinsya")))
        paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, dicPrm("add_login_user_id")))

        ' クエリ実行
        Try
            intResult = ExecuteNonQuery(ManagerDA.Connection, CommandType.Text, sql.ToString(), paramList.ToArray)
        Catch ex As Exception
            Return False
        End Try

        If intResult = 0 Then
            Return False
        Else
            Return True
        End If

    End Function

    ''' <summary>
    ''' 備考削除
    ''' </summary>
    ''' <param name="dicPrm">追加データ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DelBikou(ByVal dicPrm As Dictionary(Of String, String)) As Boolean

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                dicPrm)

        ' SQL文の生成
        Dim sql As New StringBuilder
        Dim intResult As Integer = 0
        ' パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With sql
            .AppendLine("DELETE FROM ")
            .AppendLine("	m_keikaku_kameiten_bikou_settei WITH(UPDLOCK) ") '--計画管理_加盟店備考設定ﾏｽﾀ
            .AppendLine("WHERE ")
            .AppendLine("	kameiten_cd = @kameiten_cd ")
            .AppendLine("	AND ")
            .AppendLine("	nyuuryoku_no = @nyuuryoku_no ")
        End With

        'パラメータ
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, dicPrm("kameiten_cd")))
        paramList.Add(MakeParam("@nyuuryoku_no", SqlDbType.Int, 10, dicPrm("nyuuryoku_no")))

        ' クエリ実行
        Try
            intResult = ExecuteNonQuery(ManagerDA.Connection, CommandType.Text, sql.ToString(), paramList.ToArray)
        Catch ex As Exception
            Return False
        End Try

        If intResult = 0 Then
            Return False
        Else
            Return True
        End If

    End Function

End Class

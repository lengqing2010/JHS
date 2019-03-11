Imports System.Data.SqlClient
Imports System.Text
Imports Itis.ApplicationBlocks.Data.SQLHelper
Public Class KyoutuuJyouhouDataAccess
    Private connStr As String = ConnectionManager.Instance.ConnectionString
    ''' <summary>加盟店マスト情報を取得する</summary>
    Public Function SelKyoutuuJyouhouInfo(ByVal strKameitenCd As String) As KyoutuuJyouhouDataSet.KyoutuuJyouhouTableDataTable

        ' DataSetインスタンスの生成()
        Dim dsKyoutuuJyouhouDataSet As New KyoutuuJyouhouDataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine(" m_kameiten.kbn AS kbn, ")
        commandTextSb.AppendLine(" m_data_kbn.kbn_mei AS kbn_mei, ")
        commandTextSb.AppendLine(" m_kameiten.torikesi AS torikesi, ")
        commandTextSb.AppendLine(" m_kameiten.kameiten_cd AS kameiten_cd, ")
        commandTextSb.AppendLine(" m_kameiten.kameiten_mei1 AS kameiten_mei1, ")
        commandTextSb.AppendLine(" m_kameiten.tenmei_kana1 AS tenmei_kana1, ")
        commandTextSb.AppendLine(" m_kameiten.kameiten_mei2 AS kameiten_mei2, ")
        commandTextSb.AppendLine(" m_kameiten.tenmei_kana2 AS tenmei_kana2, ")
        commandTextSb.AppendLine(" m_kameiten.builder_no  AS builder_no,")
        commandTextSb.AppendLine(" m_kameiten2.kameiten_mei1  AS builder_mei,")
        commandTextSb.AppendLine(" m_kameiten.eigyousyo_cd  AS eigyousyo_cd,")
        commandTextSb.AppendLine(" m_eigyousyo.eigyousyo_mei  AS eigyousyo_mei,")
        commandTextSb.AppendLine(" m_kameiten.keiretu_cd  AS keiretu_cd,")
        commandTextSb.AppendLine(" m_keiretu.keiretu_mei  AS keiretu_mei,")
        commandTextSb.AppendLine(" m_kameiten.th_kasi_cd  AS th_kasi_cd,")
        commandTextSb.AppendLine(" m_jhs_mailbox.displayname  AS simei,")
        commandTextSb.AppendLine(" m_kameiten.upd_datetime  AS upd_datetime,")
        commandTextSb.AppendLine(" GETDATE()  AS sansyou_date")
        commandTextSb.AppendLine(" FROM  ")
        commandTextSb.AppendLine(" m_kameiten  WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" LEFT JOIN m_data_kbn  WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" ON  m_kameiten.kbn=m_data_kbn.kbn ")
        commandTextSb.AppendLine(" LEFT JOIN  ")
        commandTextSb.AppendLine(" (SELECT ")
        commandTextSb.AppendLine(" kameiten_cd, ")
        commandTextSb.AppendLine(" kameiten_mei1 ")
        commandTextSb.AppendLine(" FROM  ")
        commandTextSb.AppendLine(" m_kameiten  WITH (READCOMMITTED) ")
        'commandTextSb.AppendLine(" WHERE ")
        'commandTextSb.AppendLine(" kbn NOT IN  ")
        'commandTextSb.AppendLine(" (@kbn0,@kbn1) ")  
        commandTextSb.AppendLine(" ) AS m_kameiten2 ")
        commandTextSb.AppendLine(" ON  m_kameiten.builder_no=m_kameiten2.kameiten_cd ")
        commandTextSb.AppendLine(" LEFT JOIN m_eigyousyo  WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" ON  m_kameiten.eigyousyo_cd=m_eigyousyo.eigyousyo_cd ")
        commandTextSb.AppendLine(" LEFT JOIN m_keiretu  WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" ON  m_kameiten.keiretu_cd=m_keiretu.keiretu_cd ")
        commandTextSb.AppendLine(" LEFT JOIN m_jhs_mailbox  WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" ON  m_jhs_mailbox.PrimaryWindowsNTAccount=m_kameiten.upd_login_user_id ")
        commandTextSb.AppendLine(" WHERE m_kameiten.kameiten_cd=@kameiten_cd ")

        'パラメータの設定
        paramList.Add(MakeParam("@kbn0", SqlDbType.Char, 1, "T"))
        paramList.Add(MakeParam("@kbn1", SqlDbType.Char, 1, "J"))
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsKyoutuuJyouhouDataSet, _
                    dsKyoutuuJyouhouDataSet.KyoutuuJyouhouTable.TableName, paramList.ToArray)

        Return dsKyoutuuJyouhouDataSet.KyoutuuJyouhouTable


    End Function
    Public Function SelHosyousyo(ByVal strKameitenCd As String, ByVal strNengetu As String, ByVal strFlg As String) As DataTable
        ' DataSetインスタンスの生成()
        Dim dsDataSet As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文

        commandTextSb.AppendLine(" Select  t_jiban.kbn + t_jiban.hosyousyo_no ")
        commandTextSb.AppendLine(" ,t_jiban.sesyu_mei ")
        commandTextSb.AppendLine(" ,m_data_haki.haki_syubetu ")
        commandTextSb.AppendLine(" ,convert( varchar(10),t_jiban.hosyousyo_hak_date,111) ")
        commandTextSb.AppendLine(" ,convert( varchar(10),t_jiban.hosyou_kaisi_date ,111) ")
        commandTextSb.AppendLine(" ,t_jiban.fuho_syoumeisyo_flg ")
        commandTextSb.AppendLine(" ,t_jiban.fuho_syoumeisyo_hassou_date ")
        commandTextSb.AppendLine(" ,m_kameiten_bikou_settei.kameiten_cd AS bikou_syubetu ")
        commandTextSb.AppendLine(" FROM t_jiban  WITH (READCOMMITTED)  ")
        commandTextSb.AppendLine(" LEFT JOIN m_data_haki  WITH (READCOMMITTED) ON t_jiban.data_haki_syubetu=m_data_haki.data_haki_no ")
        commandTextSb.AppendLine(" LEFT JOIN (SELECT kameiten_cd FROM m_kameiten_bikou_settei  WITH (READCOMMITTED) WHERE bikou_syubetu=42 GROUP BY kameiten_cd ) AS m_kameiten_bikou_settei ON t_jiban.kameiten_cd=m_kameiten_bikou_settei.kameiten_cd  ")
        commandTextSb.AppendLine(" WHERE t_jiban.kameiten_cd=@strKameitenCd ")
        commandTextSb.AppendLine(" AND t_jiban.kbn=@strFlg ")
        commandTextSb.AppendLine(" AND  convert(varchar(4),year(t_jiban.hosyousyo_hak_date)) +'/'+RIGHT('0'+convert(varchar(2),month(t_jiban.hosyousyo_hak_date)),2)=@strNengetu ")
        commandTextSb.AppendLine(" ORDER BY convert(char(1),t_jiban.kbn)+t_jiban.hosyousyo_no ")

        'パラメータの設定
        paramList.Add(MakeParam("@strFlg", SqlDbType.VarChar, 1, strFlg))
        paramList.Add(MakeParam("@strNengetu", SqlDbType.VarChar, 7, strNengetu))
        paramList.Add(MakeParam("@strKameitenCd", SqlDbType.VarChar, 5, strKameitenCd))
        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    "dsDataSet", paramList.ToArray)

        Return dsDataSet.Tables(0)

    End Function


    Public Function SelKensuu(ByVal strKameitenCd As String, ByVal strKbn As String, ByVal strNengetu As String) As DataTable

        ' DataSetインスタンスの生成()
        Dim dsDataSet As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine(" kbn ")
        commandTextSb.AppendLine(" FROM t_jiban  WITH (READCOMMITTED)  ")
        commandTextSb.AppendLine("WHERE kbn=@kbn  ")
        commandTextSb.AppendLine("AND kameiten_cd=@kameiten_cd ")
        commandTextSb.AppendLine("AND CONVERT(varchar(4),year(hosyousyo_hak_date)) +'/'+RIGHT('0'+convert(varchar(2),month(hosyousyo_hak_date)),2)=@Nengetu ")
        'パラメータの設定
        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, strKbn))
        paramList.Add(MakeParam("@Nengetu", SqlDbType.VarChar, 7, strNengetu))
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))
        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    "dsDataSet", paramList.ToArray)

        Return dsDataSet.Tables(0)

    End Function
    ''' <summary>加盟店マスト情報を更新する</summary>
    Public Function UpdKyoutuuJyouhouInfo(ByVal dtKyoutuuJyouhouData As KyoutuuJyouhouDataSet.KyoutuuJyouhouTableDataTable) As Integer

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        commandTextSb.AppendLine(" UPDATE ")
        commandTextSb.AppendLine(" m_kameiten ")
        commandTextSb.AppendLine(" WITH(UPDLOCK) ")
        commandTextSb.AppendLine(" SET ")
        commandTextSb.AppendLine(" torikesi=@torikesi, ")
        commandTextSb.AppendLine(" kameiten_mei1=@kameiten_mei1, ")
        commandTextSb.AppendLine(" tenmei_kana1=@tenmei_kana1, ")
        commandTextSb.AppendLine(" kameiten_mei2=@kameiten_mei2, ")
        commandTextSb.AppendLine(" tenmei_kana2=@tenmei_kana2, ")
        commandTextSb.AppendLine(" builder_no=@builder_no, ")
        commandTextSb.AppendLine(" keiretu_cd=@keiretu_cd, ")
        commandTextSb.AppendLine(" eigyousyo_cd=@eigyousyo_cd, ")
        commandTextSb.AppendLine(" th_kasi_cd=@th_kasi_cd, ")
        commandTextSb.AppendLine(" upd_datetime=getdate(), ")
        commandTextSb.AppendLine(" upd_login_user_id=@upd_login_user_id, ")
        commandTextSb.AppendLine(" hattyuu_teisi_flg=@hattyuu_teisi_flg ")
        '==========================2012/03/28 車龍 405721案件の対応 追加↓==========================================
        If Not dtKyoutuuJyouhouData.Rows(0).Item("torikesi").ToString.Trim.Equals("0") Then
            commandTextSb.AppendLine(" ,torikesi_set_date=GETDATE() ")
        Else
            commandTextSb.AppendLine(" ,torikesi_set_date=NULL ")
        End If
        '==========================2012/03/28 車龍 405721案件の対応 追加↑==========================================
        commandTextSb.AppendLine(" WHERE m_kameiten.kameiten_cd=@kameiten_cd ")
        With dtKyoutuuJyouhouData.Rows(0)

            'パラメータの設定
            paramList.Add(MakeParam("@torikesi", SqlDbType.VarChar, 1, setParam(.Item("torikesi"))))
            paramList.Add(MakeParam("@kameiten_mei1", SqlDbType.VarChar, 40, setParam(.Item("kameiten_mei1"))))
            paramList.Add(MakeParam("@tenmei_kana1", SqlDbType.VarChar, 20, setParam(.Item("tenmei_kana1"))))
            paramList.Add(MakeParam("@kameiten_mei2", SqlDbType.VarChar, 40, setParam(.Item("kameiten_mei2"))))
            paramList.Add(MakeParam("@tenmei_kana2", SqlDbType.VarChar, 20, setParam(.Item("tenmei_kana2"))))
            paramList.Add(MakeParam("@builder_no", SqlDbType.VarChar, 9, setParam(.Item("builder_no"))))
            paramList.Add(MakeParam("@keiretu_cd", SqlDbType.VarChar, 5, setParam(.Item("keiretu_cd"))))
            paramList.Add(MakeParam("@eigyousyo_cd", SqlDbType.VarChar, 5, setParam(.Item("eigyousyo_cd"))))
            paramList.Add(MakeParam("@th_kasi_cd", SqlDbType.VarChar, 7, setParam(.Item("th_kasi_cd"))))

            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, setParam(.Item("kameiten_cd"))))
            paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, setParam(.Item("simei"))))

            paramList.Add(MakeParam("@hattyuu_teisi_flg", SqlDbType.VarChar, 10, setParam(.Item("hattyuu_teisi_flg"))))


        End With

        '更新されたデータセットを DB へ書き込み
        ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

        '終了処理
        commandTextSb = Nothing

        '戻り値をセット成功の場合
        UpdKyoutuuJyouhouInfo = True

    End Function
    Function setParam(ByVal strObj As Object) As Object
        If strObj.ToString.Trim = "" Then
            Return DBNull.Value
        Else
            Return strObj.ToString
        End If
    End Function
    Public Function UpdJiban(ByVal strKbn As String, ByVal strNo As String, ByVal strFlg As String, ByVal strUserId As String) As Boolean
        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)


        paramList.Add(MakeParam("@kbn", SqlDbType.VarChar, 1, strKbn))
        paramList.Add(MakeParam("@hosyousyo_no", SqlDbType.VarChar, 10, strNo))
        paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, strUserId))
        paramList.Add(MakeParam("@flg", SqlDbType.VarChar, 1, strFlg))
        'SQL文

        commandTextSb.AppendLine(" UPDATE ")
        commandTextSb.AppendLine(" t_jiban ")
        commandTextSb.AppendLine(" WITH(UPDLOCK) ")
        commandTextSb.AppendLine(" SET ")
        commandTextSb.AppendLine(" fuho_syoumeisyo_flg=@flg, ")
        commandTextSb.AppendLine(" kousinsya= left(@upd_login_user_id+'$'+right(convert(varchar(4),year(getdate())),2) +'/' +right('0'+convert(varchar(4),month(getdate())),2)+'/' +convert(varchar(4),day(getdate()))+' '+right('0'+convert(varchar(4),DATEPART(hour,getdate())),2)+':'+right('0'+convert(varchar(4),DATEPART(minute,getdate())),2),30), ")
        commandTextSb.AppendLine(" upd_datetime=getdate(), ")
        commandTextSb.AppendLine(" upd_login_user_id=@upd_login_user_id ")
        commandTextSb.AppendLine(" WHERE kbn=@kbn ")
        commandTextSb.AppendLine(" AND hosyousyo_no=@hosyousyo_no ")
        ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

        '終了処理
        commandTextSb = Nothing

        '戻り値をセット成功の場合
        UpdJiban = True
    End Function
    ''' <summary>地盤連携情報を更新する</summary>
    Public Function UpdJibanRenkei(ByVal strKbn As String, ByVal strNo As String, ByVal strUserId As String) As Boolean
        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        paramList.Add(MakeParam("@kbn", SqlDbType.VarChar, 1, strKbn))
        paramList.Add(MakeParam("@hosyousyo_no", SqlDbType.VarChar, 10, strNo))
        paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, strUserId))
        paramList.Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 4, "2"))
        paramList.Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 4, "0"))
            commandTextSb.AppendLine(" UPDATE ")
            commandTextSb.AppendLine(" t_jiban_renkei ")
            commandTextSb.AppendLine(" WITH(UPDLOCK) ")
            commandTextSb.AppendLine(" SET ")
            commandTextSb.AppendLine(" renkei_siji_cd=@renkei_siji_cd, ")
            commandTextSb.AppendLine(" sousin_jyky_cd=@sousin_jyky_cd, ")
            commandTextSb.AppendLine(" upd_datetime=getdate(), ")
            commandTextSb.AppendLine(" upd_login_user_id=@upd_login_user_id ")
            commandTextSb.AppendLine(" WHERE kbn=@kbn ")
            commandTextSb.AppendLine(" AND hosyousyo_no=@hosyousyo_no ")

            If ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray) = 0 Then
                commandTextSb.Remove(0, commandTextSb.Length)
                commandTextSb.AppendLine(" INSERT INTO ")
                commandTextSb.AppendLine("   t_jiban_renkei WITH(UPDLOCK) ")
                commandTextSb.AppendLine("   (kbn, ")
                commandTextSb.AppendLine("   hosyousyo_no, ")
                commandTextSb.AppendLine("   renkei_siji_cd, ")
                commandTextSb.AppendLine("   sousin_jyky_cd, ")
                commandTextSb.AppendLine("   upd_login_user_id, ")
                commandTextSb.AppendLine("   upd_datetime) ")
                commandTextSb.AppendLine(" VALUES( ")
                commandTextSb.AppendLine("   @kbn, ")
                commandTextSb.AppendLine("   @hosyousyo_no, ")
                commandTextSb.AppendLine("   @renkei_siji_cd, ")
                commandTextSb.AppendLine("   @sousin_jyky_cd, ")
                commandTextSb.AppendLine("   @upd_login_user_id, ")
                commandTextSb.AppendLine("   GETDATE()) ")
            ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)
            End If

            '終了処理
            commandTextSb = Nothing

            '戻り値をセット成功の場合
            UpdJibanRenkei = True
    End Function

    ''' <summary>加盟店連携情報を更新する</summary>
    Public Function UpdKyoutuuJyouhouRenkei(ByVal strKameitenCd As String, ByVal strUserId As String) As Integer

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        commandTextSb.AppendLine(" UPDATE ")
        commandTextSb.AppendLine(" m_kameiten_renkei ")
        commandTextSb.AppendLine(" WITH(UPDLOCK) ")
        commandTextSb.AppendLine(" SET ")
        commandTextSb.AppendLine(" renkei_siji_cd=@renkei_siji_cd, ")
        commandTextSb.AppendLine(" sousin_jyky_cd=@sousin_jyky_cd, ")
        commandTextSb.AppendLine(" upd_datetime=getdate(), ")
        commandTextSb.AppendLine(" upd_login_user_id=@upd_login_user_id ")
        commandTextSb.AppendLine(" WHERE m_kameiten_renkei.kameiten_cd=@kameiten_cd ")

        'パラメータの設定
        paramList.Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 4, "2"))
        paramList.Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 4, "0"))
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))
        paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, strUserId))


        '更新されたデータセットを DB へ書き込み
        If ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray) = 0 Then
            commandTextSb.Remove(0, commandTextSb.Length)
            paramList.Clear()

            commandTextSb.AppendLine(" INSERT INTO ")
            commandTextSb.AppendLine("   m_kameiten_renkei WITH(UPDLOCK) ")
            commandTextSb.AppendLine("   (kameiten_cd, ")
            commandTextSb.AppendLine("   renkei_siji_cd, ")
            commandTextSb.AppendLine("   sousin_jyky_cd, ")
            commandTextSb.AppendLine("   upd_login_user_id, ")
            commandTextSb.AppendLine("   upd_datetime) ")
            commandTextSb.AppendLine(" VALUES( ")
            commandTextSb.AppendLine("   @kameiten_cd, ")
            commandTextSb.AppendLine("   @renkei_siji_cd, ")
            commandTextSb.AppendLine("   @sousin_jyky_cd, ")
            commandTextSb.AppendLine("   @upd_login_user_id, ")
            commandTextSb.AppendLine("   GETDATE()) ")
            'パラメータの設定
            paramList.Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 4, "2"))
            paramList.Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 4, "0"))
            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))
            paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, strUserId))
            ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)
        End If

        '終了処理
        commandTextSb = Nothing

        '戻り値をセット成功の場合
        UpdKyoutuuJyouhouRenkei = True

    End Function


    ''' <summary>加盟店対応商品区分切替履歴テーブル</summary>
    Public Function UpdKameitenTaiouSyouhinKbnRireki(ByVal strKameitenCd As String, ByVal strUserId As String, ByVal taiou_syouhin_kbn As String) As Integer

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文

        With commandTextSb

            .AppendLine("Declare @kameiten_cd varchar(5)")
            .AppendLine("Declare @koushin_no int")
            .AppendLine("Declare @taiou_syouhin_kbn varchar(1)")
            .AppendLine("Declare @user_id varchar(30)")
            .AppendLine("")
            .AppendLine("Set @kameiten_cd='" & strKameitenCd & "'")
            .AppendLine("Set @taiou_syouhin_kbn='" & taiou_syouhin_kbn & "'")
            .AppendLine("Set @user_id='" & strUserId & "'")


            .AppendLine("SELECT @koushin_no = MAX(koushin_no)+1 FROM t_kameiten_taiou_syouhin_kbn_rireki ")
            .AppendLine("WHERE kameiten_cd=@kameiten_cd")
            .AppendLine("")
            .AppendLine("If @koushin_no Is Null ")
            .AppendLine("Begin")
            .AppendLine("	Set @koushin_no = 1")
            .AppendLine("End")
            .AppendLine("")
            .AppendLine("")
            .AppendLine("Insert into t_kameiten_taiou_syouhin_kbn_rireki(kameiten_cd,koushin_no,taiou_syouhin_kbn,taiou_syouhin_kbn_set_date,taiou_syouhin_kbn_end_date,add_login_user_id,add_datetime,upd_login_user_id,upd_datetime)")
            .AppendLine("SELECT @kameiten_cd,@koushin_no,@taiou_syouhin_kbn,getdate(),null,@user_id,getdate(),null,null")
            .AppendLine("")
            .AppendLine("Update t_kameiten_taiou_syouhin_kbn_rireki ")
            .AppendLine("Set taiou_syouhin_kbn_end_date = getdate(),upd_login_user_id=@user_id,upd_datetime=getdate()")
            .AppendLine("WHERE kameiten_cd=@kameiten_cd And koushin_no=@koushin_no-1")

        End With

        ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

        '終了処理
        commandTextSb = Nothing

        '戻り値をセット成功の場合
        Return True

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

    ''' <summary>
    ''' FC調査会社を取得する
    ''' </summary>
    ''' <returns>FC調査会社のデータテーブル</returns>
    ''' <remarks></remarks>
    ''' <history>2012/04/13 車龍 405738案件の対応 追加</history>
    Public Function SelFcTyousaKaisya(ByVal strEigyousyoCd As String) As Data.DataTable

        ' DataSetインスタンスの生成()
        Dim dsDataSet As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	'[' + MTK.tys_kaisya_cd + MTK.jigyousyo_cd + '] ' + ISNULL(MTK.tys_kaisya_mei,'') AS fc_tys_kaisya ")
            .AppendLine("FROM ")
            .AppendLine("	m_eigyousyo AS ME WITH(READCOMMITTED) ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_tyousakaisya AS MTK WITH(READCOMMITTED) ")
            .AppendLine("	ON ")
            .AppendLine("		ME.fc_tys_kaisya_cd = MTK.tys_kaisya_cd ")
            .AppendLine("		AND ")
            .AppendLine("		ME.fc_jigyousyo_cd = MTK.jigyousyo_cd ")
            .AppendLine("WHERE ")
            .AppendLine("	ME.eigyousyo_cd = @eigyousyo_cd ")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@eigyousyo_cd", SqlDbType.VarChar, 5, strEigyousyoCd))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dtFcTyousaKaisya", paramList.ToArray)

        Return dsDataSet.Tables("dtFcTyousaKaisya")

    End Function


End Class

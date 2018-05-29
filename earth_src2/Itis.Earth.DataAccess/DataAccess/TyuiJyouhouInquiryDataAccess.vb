Imports System.Data.SqlClient
Imports System.Text
Imports Itis.ApplicationBlocks.Data.SQLHelper
Public Class TyuiJyouhouInquiryDataAccess
    Private connStr As String = ConnectionManager.Instance.ConnectionString

    '''<summary>調査会社排他処理</summary>
    Public Function SelTyousaKaisyaHaita(ByVal dtTyousaKaisyaUPDData As TyuiJyouhouDataSet.TyousaKaisyaUPDTableDataTable, Optional ByVal blnChk As Boolean = False) As DataTable
        Dim commandTextSb As New System.Text.StringBuilder

        Dim dsReturn As New DataSet
        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine(" upd_login_user_id AS login_user_id, ")
        commandTextSb.AppendLine(" upd_datetime AS upd_datetime ")
        commandTextSb.AppendLine(" FROM  m_kameiten_tyousakaisya WITH (READCOMMITTED) ")
        commandTextSb.AppendLine("WHERE m_kameiten_tyousakaisya.kameiten_cd = @kameiten_cd  ")
        commandTextSb.AppendLine(" AND  m_kameiten_tyousakaisya.kaisya_kbn = @kaisya_kbn  ")

        'パラメータの設定
        With dtTyousaKaisyaUPDData.Rows(0)
            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, .Item("kameiten_cd")))
            paramList.Add(MakeParam("@kaisya_kbn", SqlDbType.Int, 4, .Item("kaisya_kbn")))
            'If blnCheck Then
            commandTextSb.AppendLine(" AND  m_kameiten_tyousakaisya.tys_kaisya_cd = @tys_kaisya_cd  ")
            commandTextSb.AppendLine(" AND  m_kameiten_tyousakaisya.jigyousyo_cd = @jigyousyo_cd  ")
            If blnChk Then
                paramList.Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 5, Split(.Item("tys_kaisya_cd"), ":")(1)))
                paramList.Add(MakeParam("@jigyousyo_cd", SqlDbType.VarChar, 2, Split(.Item("jigyousyo_cd"), ":")(1)))

            Else
                paramList.Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 5, Split(.Item("tys_kaisya_cd"), ":")(0)))
                paramList.Add(MakeParam("@jigyousyo_cd", SqlDbType.VarChar, 2, Split(.Item("jigyousyo_cd"), ":")(0)))

            End If
        End With
        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, _
                    "dsReturn", paramList.ToArray)

        Return dsReturn.Tables(0)

    End Function
    '''<summary>調査会社連携の更新処理</summary>
    Public Function UpdTyousaKaisyaRenkei(ByVal dtTyousaKaisyaUPDData As TyuiJyouhouDataSet.TyousaKaisyaUPDTableDataTable, ByVal strCd As String, Optional ByVal intRow As String = "") As Boolean
        '戻り値
        UpdTyousaKaisyaRenkei = False

        Dim commandTextSb As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With dtTyousaKaisyaUPDData.Rows(0)
            If strCd = "9" Then
                commandTextSb.AppendLine(" INSERT INTO ")
                commandTextSb.AppendLine(" m_kameiten_tyousakaisya_renkei ")
                commandTextSb.AppendLine(" WITH(UPDLOCK) ")
                commandTextSb.AppendLine("  (kameiten_cd,")
                commandTextSb.AppendLine("  kaisya_kbn, ")
                commandTextSb.AppendLine("  tys_kaisya_cd, ")
                commandTextSb.AppendLine("  jigyousyo_cd, ")
                commandTextSb.AppendLine("  renkei_siji_cd, ")
                commandTextSb.AppendLine("  sousin_jyky_cd, ")
                commandTextSb.AppendLine("  upd_login_user_id, ")
                commandTextSb.AppendLine("  upd_datetime) ")
                commandTextSb.AppendLine(" SELECT ")
                commandTextSb.AppendLine("  kameiten_cd ,")
                commandTextSb.AppendLine("  kaisya_kbn, ")
                commandTextSb.AppendLine("  tys_kaisya_cd, ")
                commandTextSb.AppendLine("  jigyousyo_cd, ")
                commandTextSb.AppendLine("  '5', ")
                commandTextSb.AppendLine("  @sousin_jyky_cd, ")
                commandTextSb.AppendLine("  @add_login_user_id, ")
                commandTextSb.AppendLine("  GETDATE() ")
                commandTextSb.AppendLine(" FROM m_kameiten_tyousakaisya  WITH (READCOMMITTED) ")
                commandTextSb.AppendLine(" WHERE kameiten_cd=@kameiten_cd ")
                commandTextSb.AppendLine(" AND kaisya_kbn=@kaisya_kbn ")
                commandTextSb.AppendLine(" AND kahi_kbn=@kahi_kbn ")
                commandTextSb.AppendLine(" AND tys_kaisya_cd+jigyousyo_cd NOT IN  ")
                commandTextSb.AppendLine(" (SELECT tys_kaisya_cd+jigyousyo_cd FROM m_kameiten_tyousakaisya_renkei  WITH (READCOMMITTED) ")
                commandTextSb.AppendLine(" WHERE kameiten_cd=@kameiten_cd ")
                commandTextSb.AppendLine(" AND kaisya_kbn=@kaisya_kbn )")
                paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, .Item("kameiten_cd")))
                paramList.Add(MakeParam("@kaisya_kbn", SqlDbType.Int, 4, .Item("kaisya_kbn")))
                paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, .Item("kousinsya")))
                paramList.Add(MakeParam("@kahi_kbn", SqlDbType.Int, 4, .Item("kahi_kbn")))
                paramList.Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 4, "0"))
                ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)
            End If
            paramList.Clear()
            commandTextSb.Remove(0, commandTextSb.Length)
            'パラメータの設定
            commandTextSb.AppendLine(" UPDATE ")
            commandTextSb.AppendLine(" m_kameiten_tyousakaisya_renkei ")
            commandTextSb.AppendLine(" WITH(UPDLOCK) ")
            commandTextSb.AppendLine(" SET ")
            commandTextSb.AppendLine(" sousin_jyky_cd=@sousin_jyky_cd, ")
            commandTextSb.AppendLine(" renkei_siji_cd=@renkei_siji_cd, ")
            commandTextSb.AppendLine(" kameiten_cd = @kameiten_cd, ")
            commandTextSb.AppendLine("   kaisya_kbn = @kaisya_kbn , ")
            commandTextSb.AppendLine("   tys_kaisya_cd = @tys_kaisya_cd , ")
            commandTextSb.AppendLine("   jigyousyo_cd = @jigyousyo_cd,  ")
            commandTextSb.AppendLine(" upd_login_user_id=@upd_login_user_id, ")
            commandTextSb.AppendLine(" upd_datetime=GETDATE() ")
            commandTextSb.AppendLine("WHERE kameiten_cd = @kameiten_cd  ")
            commandTextSb.AppendLine(" AND  kaisya_kbn = @kaisya_kbn  ")
            commandTextSb.AppendLine(" AND  tys_kaisya_cd = @tys_kaisya_cd1  ")
            commandTextSb.AppendLine(" AND  jigyousyo_cd = @jigyousyo_cd1  ")

            If strCd = "2" Then
                commandTextSb.Remove(0, commandTextSb.Length)

                commandTextSb.AppendLine(" DELETE FROM ")
                commandTextSb.AppendLine(" m_kameiten_tyousakaisya_renkei ")
                commandTextSb.AppendLine(" WITH(UPDLOCK) ")
                commandTextSb.AppendLine(" WHERE  kameiten_cd=@kameiten_cd ")
                commandTextSb.AppendLine(" AND  kaisya_kbn=@kaisya_kbn ")
                commandTextSb.AppendLine(" AND  ((tys_kaisya_cd=@tys_kaisya_cd ")
                commandTextSb.AppendLine(" AND  jigyousyo_cd=@jigyousyo_cd) ")
                commandTextSb.AppendLine(" OR  (tys_kaisya_cd=@tys_kaisya_cd1 ")
                commandTextSb.AppendLine(" AND  jigyousyo_cd=@jigyousyo_cd1 )) ")

                paramList.Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 5, Split(.Item("tys_kaisya_cd"), ":")(0)))
                paramList.Add(MakeParam("@jigyousyo_cd", SqlDbType.VarChar, 2, Split(.Item("jigyousyo_cd"), ":")(0)))
                paramList.Add(MakeParam("@tys_kaisya_cd1", SqlDbType.VarChar, 5, Split(.Item("tys_kaisya_cd"), ":")(1)))
                paramList.Add(MakeParam("@jigyousyo_cd1", SqlDbType.VarChar, 2, Split(.Item("jigyousyo_cd"), ":")(1)))
                paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, .Item("kameiten_cd")))
                paramList.Add(MakeParam("@kaisya_kbn", SqlDbType.Int, 4, .Item("kaisya_kbn")))
                'ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

                commandTextSb.AppendLine(" INSERT INTO ")
                commandTextSb.AppendLine(" m_kameiten_tyousakaisya_renkei ")
                commandTextSb.AppendLine(" WITH(UPDLOCK) ")
                commandTextSb.AppendLine("  (kameiten_cd,")
                commandTextSb.AppendLine("  kaisya_kbn, ")
                commandTextSb.AppendLine("  tys_kaisya_cd, ")
                commandTextSb.AppendLine("  jigyousyo_cd, ")
                commandTextSb.AppendLine("  renkei_siji_cd, ")
                commandTextSb.AppendLine("  sousin_jyky_cd, ")
                commandTextSb.AppendLine("  upd_login_user_id, ")
                commandTextSb.AppendLine("  upd_datetime) ")
                commandTextSb.AppendLine(" SELECT ")
                commandTextSb.AppendLine("  @kameiten_cd ,")
                commandTextSb.AppendLine("  @kaisya_kbn, ")
                commandTextSb.AppendLine("  @tys_kaisya_cd, ")
                commandTextSb.AppendLine("  @jigyousyo_cd, ")
                commandTextSb.AppendLine("  @renkei_siji_cd, ")
                commandTextSb.AppendLine("  @sousin_jyky_cd, ")
                commandTextSb.AppendLine("  @add_login_user_id, ")
                commandTextSb.AppendLine("  GETDATE() ")


                paramList.Add(MakeParam("@renkei_siji_cd1", SqlDbType.Int, 4, "9"))
                paramList.Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 4, "0"))
                paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, .Item("kousinsya")))
                If Split(.Item("tys_kaisya_cd"), ":")(0) = Split(.Item("tys_kaisya_cd"), ":")(1) And Split(.Item("jigyousyo_cd"), ":")(0) = Split(.Item("jigyousyo_cd"), ":")(1) Then
                    paramList.Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 4, "2"))
                Else
                    paramList.Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 4, "1"))
                End If
                If Not (Split(.Item("tys_kaisya_cd"), ":")(0) = Split(.Item("tys_kaisya_cd"), ":")(1) And Split(.Item("jigyousyo_cd"), ":")(0) = Split(.Item("jigyousyo_cd"), ":")(1)) Then
                    commandTextSb.AppendLine(" INSERT INTO ")
                    commandTextSb.AppendLine(" m_kameiten_tyousakaisya_renkei ")
                    commandTextSb.AppendLine(" WITH(UPDLOCK) ")
                    commandTextSb.AppendLine("  (kameiten_cd,")
                    commandTextSb.AppendLine("  kaisya_kbn, ")
                    commandTextSb.AppendLine("  tys_kaisya_cd, ")
                    commandTextSb.AppendLine("  jigyousyo_cd, ")
                    commandTextSb.AppendLine("  renkei_siji_cd, ")
                    commandTextSb.AppendLine("  sousin_jyky_cd, ")
                    commandTextSb.AppendLine("  upd_login_user_id, ")
                    commandTextSb.AppendLine("  upd_datetime) ")
                    commandTextSb.AppendLine(" SELECT ")
                    commandTextSb.AppendLine("  @kameiten_cd ,")
                    commandTextSb.AppendLine("  @kaisya_kbn, ")
                    commandTextSb.AppendLine("  @tys_kaisya_cd1, ")
                    commandTextSb.AppendLine("  @jigyousyo_cd1, ")
                    commandTextSb.AppendLine("  @renkei_siji_cd1, ")
                    commandTextSb.AppendLine("  @sousin_jyky_cd, ")
                    commandTextSb.AppendLine("  @add_login_user_id, ")
                    commandTextSb.AppendLine("  GETDATE() ")

                End If

                '更新されたデータセットを DB へ書き込み
                ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)



            Else
                paramList.Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 4, "0"))
                If strCd = "9" Then
                    paramList.Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 5, Split(.Item("tys_kaisya_cd"), ":")(1)))
                    paramList.Add(MakeParam("@jigyousyo_cd", SqlDbType.VarChar, 2, Split(.Item("jigyousyo_cd"), ":")(1)))

                Else
                    paramList.Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 5, Split(.Item("tys_kaisya_cd"), ":")(0)))
                    paramList.Add(MakeParam("@jigyousyo_cd", SqlDbType.VarChar, 2, Split(.Item("jigyousyo_cd"), ":")(0)))

                End If
                paramList.Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 4, strCd))
                paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, .Item("kousinsya")))
                paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, .Item("kameiten_cd")))
                paramList.Add(MakeParam("@kaisya_kbn", SqlDbType.Int, 4, .Item("kaisya_kbn")))
                paramList.Add(MakeParam("@tys_kaisya_cd1", SqlDbType.VarChar, 5, Split(.Item("tys_kaisya_cd"), ":")(1)))
                paramList.Add(MakeParam("@jigyousyo_cd1", SqlDbType.VarChar, 2, Split(.Item("jigyousyo_cd"), ":")(1)))

                '更新されたデータセットを DB へ書き込み
                If ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray) = 0 Then

                    commandTextSb.Remove(0, commandTextSb.Length)
                    commandTextSb.AppendLine(" INSERT INTO ")
                    commandTextSb.AppendLine(" m_kameiten_tyousakaisya_renkei ")
                    commandTextSb.AppendLine(" WITH(UPDLOCK) ")
                    commandTextSb.AppendLine("  (kameiten_cd,")
                    commandTextSb.AppendLine("  kaisya_kbn, ")
                    commandTextSb.AppendLine("  tys_kaisya_cd, ")
                    commandTextSb.AppendLine("  jigyousyo_cd, ")
                    commandTextSb.AppendLine("  renkei_siji_cd, ")
                    commandTextSb.AppendLine("  sousin_jyky_cd, ")
                    commandTextSb.AppendLine("  upd_login_user_id, ")
                    commandTextSb.AppendLine("  upd_datetime) ")
                    commandTextSb.AppendLine(" SELECT ")
                    commandTextSb.AppendLine("  @kameiten_cd ,")
                    commandTextSb.AppendLine("  @kaisya_kbn, ")
                    commandTextSb.AppendLine("  @tys_kaisya_cd, ")
                    commandTextSb.AppendLine("  @jigyousyo_cd, ")
                    commandTextSb.AppendLine("  @renkei_siji_cd, ")
                    commandTextSb.AppendLine("  @sousin_jyky_cd, ")
                    commandTextSb.AppendLine("  @add_login_user_id, ")
                    commandTextSb.AppendLine("  GETDATE() ")

                    paramList.Clear()
                    'パラメータの設定
                    With dtTyousaKaisyaUPDData.Rows(0)
                        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, .Item("kameiten_cd")))
                        paramList.Add(MakeParam("@kaisya_kbn", SqlDbType.Int, 4, .Item("kaisya_kbn")))
                        paramList.Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 5, Split(.Item("tys_kaisya_cd"), ":")(1)))
                        paramList.Add(MakeParam("@jigyousyo_cd", SqlDbType.VarChar, 2, Split(.Item("jigyousyo_cd"), ":")(1)))
                        paramList.Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 4, "9"))
                        paramList.Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 4, "0"))
                        paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, .Item("kousinsya")))


                    End With
                    '更新されたデータセットを DB へ書き込み
                    ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

                End If
                If strCd = "9" Then
                    commandTextSb.Remove(0, commandTextSb.Length)
                    commandTextSb.AppendLine(" UPDATE ")
                    commandTextSb.AppendLine(" m_kameiten_tyousakaisya_renkei ")
                    commandTextSb.AppendLine(" WITH(UPDLOCK) ")
                    commandTextSb.AppendLine(" SET ")
                    commandTextSb.AppendLine("  renkei_siji_cd=case renkei_siji_cd when '9' then '9' else '2' end ")
                    commandTextSb.AppendLine(" WHERE  ")
                    commandTextSb.AppendLine(" kameiten_cd=@kameiten_cd  ")
                    commandTextSb.AppendLine(" AND kaisya_kbn=@kaisya_kbn  ")

                    commandTextSb.AppendLine(" AND tys_kaisya_cd+jigyousyo_cd IN (  ")

                    commandTextSb.AppendLine(" SELECT  tys_kaisya_cd+jigyousyo_cd ")
                    commandTextSb.AppendLine(" FROM  ")
                    commandTextSb.AppendLine(" m_kameiten_tyousakaisya WITH (READCOMMITTED) ")
                    commandTextSb.AppendLine(" WHERE kameiten_cd=@kameiten_cd ")
                    commandTextSb.AppendLine(" AND kaisya_kbn=@kaisya_kbn ")
                    commandTextSb.AppendLine(" AND kahi_kbn=@kahi_kbn ")
                    commandTextSb.AppendLine(" AND nyuuryoku_no>=@nyuuryoku_no )")

                    commandTextSb.AppendLine(" DELETE FROM ")
                    commandTextSb.AppendLine(" m_kameiten_tyousakaisya_renkei ")
                    commandTextSb.AppendLine(" WITH(UPDLOCK) ")
                    commandTextSb.AppendLine(" WHERE  ")
                    commandTextSb.AppendLine(" kameiten_cd=@kameiten_cd  ")
                    commandTextSb.AppendLine(" AND kaisya_kbn=@kaisya_kbn  ")
                    commandTextSb.AppendLine(" AND renkei_siji_cd='5' ")
                    paramList.Add(MakeParam("@nyuuryoku_no", SqlDbType.Int, 4, intRow))
                    paramList.Add(MakeParam("@kahi_kbn", SqlDbType.Int, 4, .Item("kahi_kbn")))
                    ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)


                End If
            End If
                'SQL文
        End With
        '終了処理
        commandTextSb = Nothing

        '戻り値をセット成功の場合
        UpdTyousaKaisyaRenkei = True
    End Function
    ''' <summary>調査会社の更新処理</summary>
    Public Function UpdTyousaKaisya(ByVal dtTyousaKaisyaUPDData As TyuiJyouhouDataSet.TyousaKaisyaUPDTableDataTable) As Boolean
        '戻り値
        UpdTyousaKaisya = False

        Dim commandTextSb As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        commandTextSb.AppendLine(" UPDATE ")
        commandTextSb.AppendLine(" m_kameiten_tyousakaisya ")
        commandTextSb.AppendLine(" WITH(UPDLOCK) ")
        commandTextSb.AppendLine(" SET ")
        commandTextSb.AppendLine(" tys_kaisya_cd=@tys_kaisya_cd, ")
        commandTextSb.AppendLine(" jigyousyo_cd=@jigyousyo_cd, ")
        commandTextSb.AppendLine(" upd_login_user_id=@upd_login_user_id, ")
        commandTextSb.AppendLine(" upd_datetime=GETDATE() ")
        commandTextSb.AppendLine("WHERE kameiten_cd = @kameiten_cd  ")
        commandTextSb.AppendLine(" AND  kaisya_kbn = @kaisya_kbn  ")
        'commandTextSb.AppendLine(" AND  kahi_kbn = @kahi_kbn  ")
        commandTextSb.AppendLine(" AND  tys_kaisya_cd = @tys_kaisya_cd1  ")
        commandTextSb.AppendLine(" AND  jigyousyo_cd = @jigyousyo_cd1  ")
        'パラメータの設定
        With dtTyousaKaisyaUPDData.Rows(0)
            paramList.Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 5, Split(.Item("tys_kaisya_cd"), ":")(0)))
            paramList.Add(MakeParam("@jigyousyo_cd", SqlDbType.VarChar, 2, Split(.Item("jigyousyo_cd"), ":")(0)))
            paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, .Item("kousinsya")))
            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, .Item("kameiten_cd")))
            paramList.Add(MakeParam("@kaisya_kbn", SqlDbType.Int, 4, .Item("kaisya_kbn")))
            'paramList.Add(MakeParam("@kahi_kbn", SqlDbType.Int, 4, .Item("kahi_kbn")))
            paramList.Add(MakeParam("@tys_kaisya_cd1", SqlDbType.VarChar, 5, Split(.Item("tys_kaisya_cd"), ":")(1)))
            paramList.Add(MakeParam("@jigyousyo_cd1", SqlDbType.VarChar, 2, Split(.Item("jigyousyo_cd"), ":")(1)))
        End With
        '更新されたデータセットを DB へ書き込み
        ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

        '終了処理
        commandTextSb = Nothing

        '戻り値をセット成功の場合
        UpdTyousaKaisya = True
    End Function
    '''<summary>調査会社レコード行数を取得する</summary>
    Public Function SelTyousaKaisyaCount(ByVal dtTyousaKaisyaUPDData As TyuiJyouhouDataSet.TyousaKaisyaUPDTableDataTable) As Integer

        Dim commandTextSb As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        Dim intReturn As Integer = 0
        Dim dsReturn As New DataSet
        'SQL文

        commandTextSb.Remove(0, commandTextSb.Length)

        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine("  COUNT(kameiten_cd)")
        commandTextSb.AppendLine("  FROM  m_kameiten_tyousakaisya_renkei ")
        commandTextSb.AppendLine("  WHERE kameiten_cd = @kameiten_cd  ")
        commandTextSb.AppendLine("  AND kaisya_kbn=@kaisya_kbn ")
        commandTextSb.AppendLine("  AND tys_kaisya_cd=@tys_kaisya_cd ")
        commandTextSb.AppendLine("  AND jigyousyo_cd=@jigyousyo_cd ")

        'パラメータの設定
        With dtTyousaKaisyaUPDData.Rows(0)
            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, .Item("kameiten_cd")))
            paramList.Add(MakeParam("@kaisya_kbn", SqlDbType.Int, 4, .Item("kaisya_kbn")))
            paramList.Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 5, Split(.Item("tys_kaisya_cd"), ":")(0)))
            paramList.Add(MakeParam("@jigyousyo_cd", SqlDbType.VarChar, 2, Split(.Item("jigyousyo_cd"), ":")(0)))
        End With
        '更新されたデータセットを DB へ書き込み
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, _
                            "dsReturn", paramList.ToArray)

        '終了処理
        commandTextSb = Nothing

        '戻り値をセット成功の場合
        Return dsReturn.Tables(0).Rows(0).Item(0)
    End Function
    '''<summary>調査会社連携の新規処理</summary>
    Public Function InsTyousaKaisyaRenkei(ByVal dtTyousaKaisyaUPDData As TyuiJyouhouDataSet.TyousaKaisyaUPDTableDataTable) As Boolean
        '戻り値
        InsTyousaKaisyaRenkei = False

        Dim commandTextSb As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        Dim intReturn As Integer = 0
        Dim dsReturn As New DataSet
        'SQL文
      
        commandTextSb.Remove(0, commandTextSb.Length)
        commandTextSb.AppendLine(" INSERT INTO ")
        commandTextSb.AppendLine(" m_kameiten_tyousakaisya_renkei ")
        commandTextSb.AppendLine(" WITH(UPDLOCK) ")
        commandTextSb.AppendLine("  (kameiten_cd,")
        commandTextSb.AppendLine("  kaisya_kbn, ")
        commandTextSb.AppendLine("  tys_kaisya_cd, ")
        commandTextSb.AppendLine("  jigyousyo_cd, ")
        commandTextSb.AppendLine("  renkei_siji_cd, ")
        commandTextSb.AppendLine("  sousin_jyky_cd, ")
        commandTextSb.AppendLine("  upd_login_user_id, ")
        commandTextSb.AppendLine("  upd_datetime) ")
        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine("  kameiten_cd ,")
        commandTextSb.AppendLine("  kaisya_kbn, ")
        commandTextSb.AppendLine("  tys_kaisya_cd, ")
        commandTextSb.AppendLine("  jigyousyo_cd, ")
        commandTextSb.AppendLine("  @renkei_siji_cd, ")
        commandTextSb.AppendLine("  @sousin_jyky_cd, ")
        commandTextSb.AppendLine("  @add_login_user_id, ")
        commandTextSb.AppendLine("  GETDATE() ")
        commandTextSb.AppendLine("  FROM  m_kameiten_tyousakaisya ")
        commandTextSb.AppendLine("  WHERE kameiten_cd = @kameiten_cd  ")
        commandTextSb.AppendLine("  AND kaisya_kbn=@kaisya_kbn ")
        commandTextSb.AppendLine("  AND tys_kaisya_cd=@tys_kaisya_cd ")
        commandTextSb.AppendLine("  AND jigyousyo_cd=@jigyousyo_cd ")

        'パラメータの設定
        With dtTyousaKaisyaUPDData.Rows(0)
            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, .Item("kameiten_cd")))
            paramList.Add(MakeParam("@kaisya_kbn", SqlDbType.Int, 4, .Item("kaisya_kbn")))
            paramList.Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 5, Split(.Item("tys_kaisya_cd"), ":")(0)))
            paramList.Add(MakeParam("@jigyousyo_cd", SqlDbType.VarChar, 2, Split(.Item("jigyousyo_cd"), ":")(0)))
            paramList.Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 4, "1"))
            paramList.Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 4, "0"))
            paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, .Item("kousinsya")))


        End With
        '更新されたデータセットを DB へ書き込み
        ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

        '終了処理
        commandTextSb = Nothing

        '戻り値をセット成功の場合
        InsTyousaKaisyaRenkei = True
    End Function
    '''<summary>調査会社連携の新規処理</summary>
    Public Function InsTyousaKaisyaRenkei2(ByVal dtTyousaKaisyaUPDData As TyuiJyouhouDataSet.TyousaKaisyaUPDTableDataTable) As Boolean
        '戻り値
        InsTyousaKaisyaRenkei2 = False

        Dim commandTextSb As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        Dim intReturn As Integer = 0
        Dim dsReturn As New DataSet
        'SQL文

        commandTextSb.Remove(0, commandTextSb.Length)
        commandTextSb.AppendLine(" INSERT INTO ")
        commandTextSb.AppendLine(" m_kameiten_tyousakaisya_renkei ")
        commandTextSb.AppendLine(" WITH(UPDLOCK) ")
        commandTextSb.AppendLine("  (kameiten_cd,")
        commandTextSb.AppendLine("  kaisya_kbn, ")
        commandTextSb.AppendLine("  tys_kaisya_cd, ")
        commandTextSb.AppendLine("  jigyousyo_cd, ")
        commandTextSb.AppendLine("  renkei_siji_cd, ")
        commandTextSb.AppendLine("  sousin_jyky_cd, ")
        commandTextSb.AppendLine("  upd_login_user_id, ")
        commandTextSb.AppendLine("  upd_datetime) ")
        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine("  kameiten_cd ,")
        commandTextSb.AppendLine("  kaisya_kbn, ")
        commandTextSb.AppendLine("  tys_kaisya_cd, ")
        commandTextSb.AppendLine("  jigyousyo_cd, ")
        commandTextSb.AppendLine("  @renkei_siji_cd, ")
        commandTextSb.AppendLine("  @sousin_jyky_cd, ")
        commandTextSb.AppendLine("  @add_login_user_id, ")
        commandTextSb.AppendLine("  GETDATE() ")
        commandTextSb.AppendLine("  FROM  m_kameiten_tyousakaisya  WITH (READCOMMITTED) ")
        commandTextSb.AppendLine("  WHERE kameiten_cd = @kameiten_cd  ")
        commandTextSb.AppendLine("  AND CAST(kaisya_kbn AS varchar(1))+tys_kaisya_cd+jigyousyo_cd NOT IN ")
        commandTextSb.AppendLine("  ( SELECT CAST(kaisya_kbn AS varchar(1))+tys_kaisya_cd+jigyousyo_cd ")
        commandTextSb.AppendLine("  FROM m_kameiten_tyousakaisya_renkei  WITH (READCOMMITTED) ")
        commandTextSb.AppendLine("  WHERE kameiten_cd = @kameiten_cd  ")
        commandTextSb.AppendLine("  )")


        'パラメータの設定
        With dtTyousaKaisyaUPDData.Rows(0)
            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, .Item("kameiten_cd")))
            paramList.Add(MakeParam("@kaisya_kbn", SqlDbType.Int, 4, .Item("kaisya_kbn")))
            paramList.Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 5, Split(.Item("tys_kaisya_cd"), ":")(0)))
            paramList.Add(MakeParam("@jigyousyo_cd", SqlDbType.VarChar, 2, Split(.Item("jigyousyo_cd"), ":")(0)))
            paramList.Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 4, "2"))
            paramList.Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 4, "0"))
            paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, .Item("kousinsya")))


        End With
        '更新されたデータセットを DB へ書き込み
        ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

        '終了処理
        commandTextSb = Nothing

        '戻り値をセット成功の場合
        InsTyousaKaisyaRenkei2 = True
    End Function
    '''<summary>調査会社の新規処理</summary>
    Public Function InsTyousaKaisya(ByVal dtTyousaKaisyaUPDData As TyuiJyouhouDataSet.TyousaKaisyaUPDTableDataTable) As Boolean
        '戻り値
        InsTyousaKaisya = False

        Dim commandTextSb As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        Dim intReturn As Integer = 0
        Dim dsReturn As New DataSet
        'SQL文
        commandTextSb.AppendLine(" SELECT COUNT(kameiten_cd) ")
        commandTextSb.AppendLine("  FROM  m_kameiten_tyousakaisya WITH (READCOMMITTED)  ")
        commandTextSb.AppendLine("  WHERE kameiten_cd = @kameiten_cd  ")
        commandTextSb.AppendLine("  AND kaisya_kbn=@kaisya_kbn ")
        commandTextSb.AppendLine("  AND kahi_kbn=@kahi_kbn ")
        'パラメータの設定
        With dtTyousaKaisyaUPDData.Rows(0)
            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, .Item("kameiten_cd")))
            paramList.Add(MakeParam("@kaisya_kbn", SqlDbType.Int, 4, .Item("kaisya_kbn")))
            paramList.Add(MakeParam("@kahi_kbn", SqlDbType.Int, 4, .Item("kahi_kbn")))
        End With
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dsReturn", paramList.ToArray)
        intReturn = dsReturn.Tables(0).Rows(0).Item(0)
        dsReturn.Dispose()

        commandTextSb.Remove(0, commandTextSb.Length)
        commandTextSb.AppendLine(" INSERT INTO ")
        commandTextSb.AppendLine(" m_kameiten_tyousakaisya ")
        commandTextSb.AppendLine(" WITH(UPDLOCK) ")
        commandTextSb.AppendLine("  (kameiten_cd,")
        commandTextSb.AppendLine("  kaisya_kbn, ")
        commandTextSb.AppendLine("  kahi_kbn, ")
        commandTextSb.AppendLine("  tys_kaisya_cd, ")
        commandTextSb.AppendLine("  jigyousyo_cd, ")
        commandTextSb.AppendLine("  nyuuryoku_no, ")
        commandTextSb.AppendLine("  add_login_user_id, ")
        commandTextSb.AppendLine("  add_datetime, ")
        commandTextSb.AppendLine("  upd_login_user_id, ")
        commandTextSb.AppendLine("  upd_datetime) ")
        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine("  @kameiten_cd ,")
        commandTextSb.AppendLine("  @kaisya_kbn, ")
        commandTextSb.AppendLine("  @kahi_kbn, ")
        commandTextSb.AppendLine("  @tys_kaisya_cd, ")
        commandTextSb.AppendLine("  @jigyousyo_cd, ")
        commandTextSb.AppendLine("   ISNULL(MAX(nyuuryoku_no),0)+1, ")
        commandTextSb.AppendLine("  @add_login_user_id, ")
        commandTextSb.AppendLine("  GETDATE(), ")
        commandTextSb.AppendLine("  @add_login_user_id, ")
        commandTextSb.AppendLine("  GETDATE() ")
        commandTextSb.AppendLine("  FROM  m_kameiten_tyousakaisya ")
        commandTextSb.AppendLine("  WHERE kameiten_cd = @kameiten_cd  ")
        commandTextSb.AppendLine("  AND kaisya_kbn=@kaisya_kbn ")
        commandTextSb.AppendLine("  AND kahi_kbn=@kahi_kbn ")
        If intReturn <> 0 Then
            commandTextSb.AppendLine("  GROUP BY kameiten_cd,kaisya_kbn,kahi_kbn")
        End If

        'パラメータの設定
        With dtTyousaKaisyaUPDData.Rows(0)
            paramList.Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 5, Split(.Item("tys_kaisya_cd"), ":")(0)))
            paramList.Add(MakeParam("@jigyousyo_cd", SqlDbType.VarChar, 2, Split(.Item("jigyousyo_cd"), ":")(0)))
            paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, .Item("kousinsya")))
        End With
        '更新されたデータセットを DB へ書き込み
        ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

        '終了処理
        commandTextSb = Nothing

        '戻り値をセット成功の場合
        InsTyousaKaisya = True
    End Function
    ''' <summary>調査会社の削除処理</summary>
    Public Function DelTyousaKaisya(ByVal dtTyousaKaisyaUPDData As TyuiJyouhouDataSet.TyousaKaisyaUPDTableDataTable, ByVal intRow As Integer) As Boolean
        '戻り値
        DelTyousaKaisya = False

        Dim commandTextSb As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        commandTextSb.AppendLine(" DELETE ")
        commandTextSb.AppendLine(" FROM  m_kameiten_tyousakaisya ")
        commandTextSb.AppendLine(" WITH(UPDLOCK) ")
        commandTextSb.AppendLine(" WHERE kameiten_cd = @kameiten_cd  ")
        commandTextSb.AppendLine(" AND  kaisya_kbn = @kaisya_kbn  ")
        commandTextSb.AppendLine(" AND  tys_kaisya_cd = @tys_kaisya_cd  ")
        commandTextSb.AppendLine(" AND  jigyousyo_cd = @jigyousyo_cd  ")
        commandTextSb.AppendLine(" UPDATE ")
        commandTextSb.AppendLine(" m_kameiten_tyousakaisya ")
        commandTextSb.AppendLine(" WITH(UPDLOCK) ")
        commandTextSb.AppendLine(" SET nyuuryoku_no=nyuuryoku_no-1 ")
        commandTextSb.AppendLine(" WHERE kameiten_cd = @kameiten_cd  ")
        commandTextSb.AppendLine(" AND  kaisya_kbn = @kaisya_kbn  ")
        commandTextSb.AppendLine(" AND  nyuuryoku_no > @nyuuryoku_no  ")
        commandTextSb.AppendLine(" AND  kahi_kbn = @kahi_kbn  ")

        'パラメータの設定
        With dtTyousaKaisyaUPDData.Rows(0)
            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, .Item("kameiten_cd")))
            paramList.Add(MakeParam("@kaisya_kbn", SqlDbType.Int, 4, .Item("kaisya_kbn")))
            paramList.Add(MakeParam("@kahi_kbn", SqlDbType.Int, 4, .Item("kahi_kbn")))
            paramList.Add(MakeParam("@nyuuryoku_no", SqlDbType.Int, 4, intRow))
            paramList.Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 5, Split(.Item("tys_kaisya_cd"), ":")(1)))
            paramList.Add(MakeParam("@jigyousyo_cd", SqlDbType.VarChar, 2, Split(.Item("jigyousyo_cd"), ":")(1)))
        End With

        '更新されたデータセットを DB へ書き込み
        ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

        '終了処理
        commandTextSb = Nothing

        '戻り値をセット成功の場合
        DelTyousaKaisya = True

    End Function
    '''<summary>注意事項排他処理</summary>
    Public Function SelTyuuiJikouHaita(ByVal dtTyuiJyouhouUPDData As TyuiJyouhouDataSet.TyuuiJikouUPDTableDataTable) As DataTable
        Dim commandTextSb As New System.Text.StringBuilder

        Dim dsReturn As New DataSet
        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine(" upd_login_user_id AS login_user_id, ")
        commandTextSb.AppendLine(" upd_datetime AS upd_datetime ")
        commandTextSb.AppendLine(" FROM  m_kameiten_tyuuijikou WITH (READCOMMITTED) ")
        commandTextSb.AppendLine("WHERE kameiten_cd = @kameiten_cd  ")
        commandTextSb.AppendLine(" AND  nyuuryoku_no = @nyuuryoku_no  ")

        'パラメータの設定
        With dtTyuiJyouhouUPDData.Rows(0)
            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, .Item("kameiten_cd")))
            paramList.Add(MakeParam("@nyuuryoku_no", SqlDbType.Int, 4, .Item("nyuuryoku_no")))
        End With
        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, _
                    "dsReturn", paramList.ToArray)

        Return dsReturn.Tables(0)

    End Function
    ''' <summary>注意事項の新規処理</summary>
    Public Function UpdTyuuiJikouRenkei(ByVal dtTyuiJyouhouUPDData As TyuiJyouhouDataSet.TyuuiJikouUPDTableDataTable, ByVal strCd As String) As Boolean
        '戻り値
        UpdTyuuiJikouRenkei = False

        Dim commandTextSb As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        commandTextSb.AppendLine(" UPDATE ")
        commandTextSb.AppendLine(" m_kameiten_tyuuijikou_renkei ")
        commandTextSb.AppendLine(" WITH(UPDLOCK) ")
        commandTextSb.AppendLine(" SET ")
        commandTextSb.AppendLine(" renkei_siji_cd=@renkei_siji_cd, ")
        commandTextSb.AppendLine(" sousin_jyky_cd=@sousin_jyky_cd, ")
        commandTextSb.AppendLine(" upd_login_user_id=@kousinsya, ")
        commandTextSb.AppendLine(" upd_datetime=GETDATE() ")
        commandTextSb.AppendLine("WHERE kameiten_cd = @kameiten_cd  ")
        'パラメータの設定
        With dtTyuiJyouhouUPDData.Rows(0)
            If strCd = "9" Or strCd = "2" Then
                commandTextSb.AppendLine(" AND  nyuuryoku_no = @nyuuryoku_no  ")
                paramList.Add(MakeParam("@nyuuryoku_no", SqlDbType.Int, 4, .Item("nyuuryoku_no")))
                If strCd = "9" Then
                    Dim sql As New StringBuilder
                    'sql.AppendLine("DECLARE @nyuuryokuno int")
                    'sql.AppendLine("set @nyuuryokuno = (select isnull( (select max(nyuuryoku_no) from m_kameiten_tyuuijikou_renkei Where kameiten_cd = @kameiten_cd), 0))")

                    '新規追加
                    sql.AppendLine(" UPDATE  m_kameiten_tyuuijikou_renkei WITH(UPDLOCK) set ")
                    sql.AppendLine("     nyuuryoku_no = isnull( (select max(nyuuryoku_no) from m_kameiten_tyuuijikou_renkei WITH (READCOMMITTED) Where kameiten_cd = @kameiten_cd), 0)+1")
                    sql.AppendLine(",renkei_siji_cd=@renkei_siji_cd")
                    sql.AppendLine(",sousin_jyky_cd=@sousin_jyky_cd")
                    sql.AppendLine(",upd_login_user_id	=	@upd_login_user_id")
                    sql.AppendLine(",upd_datetime	=	getdate()")
                    sql.AppendLine("WHERE")
                    sql.AppendLine("    kameiten_cd = @kameiten_cd")
                    sql.AppendLine("    and nyuuryoku_no = @nyuuryoku_no;")
                    paramList.Clear()

                    paramList.Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 1, strCd))
                    paramList.Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 1, 0))
                    paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, .Item("kousinsya")))
                    paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, .Item("kameiten_cd")))
                    paramList.Add(MakeParam("@nyuuryoku_no", SqlDbType.Int, 8, .Item("nyuuryoku_no")))



                    'ExecuteNonQuery(connStr, _
                    '                            CommandType.Text, _
                    '                            sql.ToString(), _
                    '                            paramList.ToArray)

                    Dim sql1 As New StringBuilder
                    sql1.AppendLine(" UPDATE ")
                    sql1.AppendLine("  m_kameiten_tyuuijikou_renkei ")
                    sql1.AppendLine(" WITH(UPDLOCK) ")
                    sql1.AppendLine(" 	SET nyuuryoku_no = nyuuryoku_no-1")
                    sql1.AppendLine(",sousin_jyky_cd=@sousin_jyky_cd")
                    sql1.AppendLine(" ,renkei_siji_cd = case renkei_siji_cd when '9' then '9' else '2' end")
                    sql1.Append(",upd_login_user_id	=	@upd_login_user_id")
                    sql1.Append(",upd_datetime	=	getdate()")

                    sql1.AppendLine(" WHERE")
                    sql1.AppendLine(" 	kameiten_cd = @kameiten_cd")
                    sql1.AppendLine(" AND nyuuryoku_no>@nyuuryoku_no")
                    sql.AppendLine(sql1.ToString)
                    'ExecuteNonQuery(connStr, _
                    '                                   CommandType.Text, _
                    '                                   sql1.ToString(), _
                    '                                   paramList.ToArray)

                    sql1.Remove(0, sql1.Length)
                    sql1.AppendLine(" DELETE")
                    sql1.AppendLine(" FROM ")
                    sql1.AppendLine(" m_kameiten_tyuuijikou_renkei ")
                    sql1.AppendLine(" WITH(UPDLOCK) ")
                    sql1.AppendLine("WHERE")
                    sql1.AppendLine("    kameiten_cd = @kameiten_cd")
                    sql1.AppendLine("    and renkei_siji_cd = @renkei_siji_cd2")
                    sql.AppendLine(sql1.ToString)
                    paramList.Add(MakeParam("@renkei_siji_cd2", SqlDbType.Int, 1, 5))
                    ExecuteNonQuery(connStr, CommandType.Text, sql.ToString(), paramList.ToArray)

                End If
            Else
                commandTextSb.AppendLine("  AND nyuuryoku_no = (SELECT   ")
                commandTextSb.AppendLine("  MAX(nyuuryoku_no) ")
                commandTextSb.AppendLine("  FROM  m_kameiten_tyuuijikou WITH (READCOMMITTED)  ")
                commandTextSb.AppendLine("  WHERE kameiten_cd = @kameiten_cd ) ")

            End If

            paramList.Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 4, strCd))
            paramList.Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 4, "0"))
            paramList.Add(MakeParam("@kousinsya", SqlDbType.VarChar, 30, .Item("kousinsya")))
            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, .Item("kameiten_cd")))

        End With

        If strCd = "2" Then
            If ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray) = 0 Then
                commandTextSb.Remove(0, commandTextSb.Length)
                commandTextSb.AppendLine(" INSERT INTO ")
                commandTextSb.AppendLine(" m_kameiten_tyuuijikou_renkei ")
                commandTextSb.AppendLine(" WITH(UPDLOCK) ")
                commandTextSb.AppendLine("  (kameiten_cd,")
                commandTextSb.AppendLine("  nyuuryoku_no, ")
                commandTextSb.AppendLine("  renkei_siji_cd, ")
                commandTextSb.AppendLine("  sousin_jyky_cd, ")
                commandTextSb.AppendLine("  upd_login_user_id, ")
                commandTextSb.AppendLine("  upd_datetime) ")
                commandTextSb.AppendLine(" SELECT ")
                commandTextSb.AppendLine("  @kameiten_cd  ,")
                commandTextSb.AppendLine("  @nyuuryoku_no, ")
                commandTextSb.AppendLine("  @renkei_siji_cd, ")
                commandTextSb.AppendLine("  @sousin_jyky_cd, ")
                commandTextSb.AppendLine("  @upd_login_user_id, ")
                commandTextSb.AppendLine("  GETDATE() ")

                'パラメータの設定
                paramList.Clear()
                With dtTyuiJyouhouUPDData.Rows(0)
                    paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, .Item("kameiten_cd")))
                    paramList.Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 4, "2"))
                    paramList.Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 4, "0"))
                    paramList.Add(MakeParam("@nyuuryoku_no", SqlDbType.Int, 8, .Item("nyuuryoku_no")))
                    paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, .Item("kousinsya")))

                End With
                '更新されたデータセットを DB へ書き込み
                ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)
            End If
        ElseIf strCd = "1" Then
            ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)
        End If
        

        '終了処理
        commandTextSb = Nothing

        '戻り値をセット成功の場合
        UpdTyuuiJikouRenkei = True
    End Function
    ''' <summary>注意事項の更新処理</summary>
    Public Function UpdTyuuiJikou(ByVal dtTyuiJyouhouUPDData As TyuiJyouhouDataSet.TyuuiJikouUPDTableDataTable) As Boolean
        '戻り値
        UpdTyuuiJikou = False

        Dim commandTextSb As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        commandTextSb.AppendLine(" UPDATE ")
        commandTextSb.AppendLine(" m_kameiten_tyuuijikou ")
        commandTextSb.AppendLine(" WITH(UPDLOCK) ")
        commandTextSb.AppendLine(" SET ")
        commandTextSb.AppendLine(" tyuuijikou_syubetu=@tyuuijikou_syubetu, ")
        commandTextSb.AppendLine(" nyuuryoku_date=@nyuuryoku_date, ")
        commandTextSb.AppendLine(" uketukesya_mei=@uketukesya_mei, ")
        commandTextSb.AppendLine(" naiyou=@naiyou, ")
        commandTextSb.AppendLine(" upd_login_user_id=@kousinsya, ")
        commandTextSb.AppendLine(" upd_datetime=GETDATE() ")
        commandTextSb.AppendLine("WHERE kameiten_cd = @kameiten_cd  ")
        commandTextSb.AppendLine(" AND  nyuuryoku_no = @nyuuryoku_no  ")

        'パラメータの設定
        With dtTyuiJyouhouUPDData.Rows(0)
            paramList.Add(MakeParam("@tyuuijikou_syubetu", SqlDbType.Int, 4, .Item("tyuuijikou_syubetu")))
            paramList.Add(MakeParam("@nyuuryoku_date", SqlDbType.DateTime, 8, .Item("nyuuryoku_date")))
            paramList.Add(MakeParam("@uketukesya_mei", SqlDbType.VarChar, 20, .Item("uketukesya_mei")))
            paramList.Add(MakeParam("@naiyou", SqlDbType.VarChar, 80, .Item("naiyou")))
            paramList.Add(MakeParam("@kousinsya", SqlDbType.VarChar, 30, .Item("kousinsya")))
            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, .Item("kameiten_cd")))
            paramList.Add(MakeParam("@nyuuryoku_no", SqlDbType.Int, 4, .Item("nyuuryoku_no")))
        End With
        '更新されたデータセットを DB へ書き込み
        ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

        '終了処理
        commandTextSb = Nothing

        '戻り値をセット成功の場合
        UpdTyuuiJikou = True
    End Function

    ''' <summary>注意事項連携の更新処理</summary>
    Public Function InsTyuuiJikouRenkei(ByVal dtTyuiJyouhouUPDData As TyuiJyouhouDataSet.TyuuiJikouUPDTableDataTable) As Boolean
        '戻り値
        InsTyuuiJikouRenkei = False

        Dim commandTextSb As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        commandTextSb.AppendLine(" INSERT INTO ")
        commandTextSb.AppendLine(" m_kameiten_tyuuijikou_renkei ")
        commandTextSb.AppendLine(" WITH(UPDLOCK) ")
        commandTextSb.AppendLine("  (kameiten_cd,")
        commandTextSb.AppendLine("  nyuuryoku_no, ")
        commandTextSb.AppendLine("  renkei_siji_cd, ")
        commandTextSb.AppendLine("  sousin_jyky_cd, ")
        commandTextSb.AppendLine("  upd_login_user_id, ")
        commandTextSb.AppendLine("  upd_datetime) ")
        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine("  @kameiten_cd ,")
        commandTextSb.AppendLine("  ISNULL(MAX(nyuuryoku_no),0), ")
        commandTextSb.AppendLine("  @renkei_siji_cd, ")
        commandTextSb.AppendLine("  @sousin_jyky_cd, ")
        commandTextSb.AppendLine("  @upd_login_user_id, ")
        commandTextSb.AppendLine("  GETDATE() ")
        commandTextSb.AppendLine(" FROM  m_kameiten_tyuuijikou ")
        commandTextSb.AppendLine("WHERE kameiten_cd = @kameiten_cd  ")
        'パラメータの設定
        With dtTyuiJyouhouUPDData.Rows(0)
            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, .Item("kameiten_cd")))
            paramList.Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 4, "1"))
            paramList.Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 4, "0"))

            paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, .Item("kousinsya")))

        End With
        '更新されたデータセットを DB へ書き込み
        ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

        '終了処理
        commandTextSb = Nothing

        '戻り値をセット成功の場合
        InsTyuuiJikouRenkei = True
    End Function
    ''' <summary>注意事項連携の更新処理</summary>
    Public Function InsTyuuiJikouRenkei2(ByVal dtTyuiJyouhouUPDData As TyuiJyouhouDataSet.TyuuiJikouUPDTableDataTable, ByVal strNyuuryokuNo As String) As Boolean
        '戻り値
        InsTyuuiJikouRenkei2 = False

        Dim commandTextSb As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        commandTextSb.AppendLine(" INSERT INTO ")
        commandTextSb.AppendLine(" m_kameiten_tyuuijikou_renkei ")
        commandTextSb.AppendLine(" WITH(UPDLOCK) ")
        commandTextSb.AppendLine("  (kameiten_cd,")
        commandTextSb.AppendLine("  nyuuryoku_no, ")
        commandTextSb.AppendLine("  renkei_siji_cd, ")
        commandTextSb.AppendLine("  sousin_jyky_cd, ")
        commandTextSb.AppendLine("  upd_login_user_id, ")
        commandTextSb.AppendLine("  upd_datetime) ")
        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine("  kameiten_cd ,")
        commandTextSb.AppendLine("  nyuuryoku_no, ")
        commandTextSb.AppendLine("  @renkei_siji_cd, ")
        commandTextSb.AppendLine("  @sousin_jyky_cd, ")
        commandTextSb.AppendLine("  @upd_login_user_id, ")
        commandTextSb.AppendLine("  GETDATE() ")
        commandTextSb.AppendLine(" FROM  m_kameiten_tyuuijikou ")
        commandTextSb.AppendLine("  WITH (READCOMMITTED)  ")
        commandTextSb.AppendLine("WHERE kameiten_cd = @kameiten_cd  ")
        commandTextSb.AppendLine("AND  nyuuryoku_no NOT IN ( ")
        commandTextSb.AppendLine(" SELECT nyuuryoku_no FROM m_kameiten_tyuuijikou_renkei ")
        commandTextSb.AppendLine("WHERE kameiten_cd = @kameiten_cd  ")
        commandTextSb.AppendLine(" ) ")
        'パラメータの設定
        commandTextSb.AppendLine(" DELETE ")
        commandTextSb.AppendLine(" FROM  m_kameiten_tyuuijikou ")
        commandTextSb.AppendLine(" WITH(UPDLOCK) ")
        commandTextSb.AppendLine("WHERE kameiten_cd = @kameiten_cd  ")
        commandTextSb.AppendLine(" AND  nyuuryoku_no = @nyuuryoku_no  ;")


        Dim sql1 As New StringBuilder
        sql1.AppendLine(" UPDATE ")
        sql1.AppendLine("  m_kameiten_tyuuijikou ")
        sql1.AppendLine(" WITH(UPDLOCK) ")
        sql1.AppendLine(" 	SET nyuuryoku_no = nyuuryoku_no-1")
        sql1.Append(",upd_login_user_id	=	@upd_login_user_id")
        sql1.Append(",upd_datetime	=	getdate()")
        sql1.AppendLine(" WHERE")
        sql1.AppendLine(" 	kameiten_cd = @kameiten_cd")
        sql1.AppendLine(" AND nyuuryoku_no>@nyuuryoku_no")




        commandTextSb.Append(sql1.ToString)


        Dim Sql As New System.Text.StringBuilder
        Sql.AppendLine(" UPDATE  m_kameiten_tyuuijikou_renkei WITH(UPDLOCK) set ")
        Sql.AppendLine("     nyuuryoku_no = isnull( (select max(nyuuryoku_no) from m_kameiten_tyuuijikou_renkei WITH (READCOMMITTED) Where kameiten_cd = @kameiten_cd), 0)+1")
        Sql.AppendLine(",renkei_siji_cd=@renkei_siji_cd9")
        Sql.AppendLine(",sousin_jyky_cd=@sousin_jyky_cd")
        Sql.AppendLine(",upd_login_user_id	=	@upd_login_user_id")
        Sql.AppendLine(",upd_datetime	=	getdate()")
        Sql.AppendLine("WHERE")
        Sql.AppendLine("    kameiten_cd = @kameiten_cd")
        Sql.AppendLine("    and nyuuryoku_no = @nyuuryoku_no;")

        commandTextSb.Append(Sql.ToString)



  

        sql1.Remove(0, sql1.Length)
        sql1.AppendLine(" UPDATE ")
        sql1.AppendLine("  m_kameiten_tyuuijikou_renkei ")
        sql1.AppendLine(" WITH(UPDLOCK) ")
        sql1.AppendLine(" 	SET nyuuryoku_no = nyuuryoku_no-1")
        sql1.AppendLine(",sousin_jyky_cd=@sousin_jyky_cd")
        sql1.AppendLine(" ,renkei_siji_cd = case renkei_siji_cd when '9' then '9' else '2' end")
        sql1.Append(",upd_login_user_id	=	@upd_login_user_id")
        sql1.Append(",upd_datetime	=	getdate()")

        sql1.AppendLine(" WHERE")
        sql1.AppendLine(" 	kameiten_cd = @kameiten_cd")
        sql1.AppendLine(" AND nyuuryoku_no>@nyuuryoku_no")
        commandTextSb.AppendLine(sql1.ToString)


        sql1.Remove(0, sql1.Length)
        sql1.AppendLine(" DELETE")
        sql1.AppendLine(" FROM ")
        sql1.AppendLine(" m_kameiten_tyuuijikou_renkei ")
        sql1.AppendLine(" WITH(UPDLOCK) ")
        sql1.AppendLine("WHERE")
        sql1.AppendLine("    kameiten_cd = @kameiten_cd")
        sql1.AppendLine("    and renkei_siji_cd = @renkei_siji_cd2")
        commandTextSb.AppendLine(sql1.ToString)






        With dtTyuiJyouhouUPDData.Rows(0)
            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, .Item("kameiten_cd")))
            paramList.Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 4, "5"))
            paramList.Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 4, "0"))
            paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, .Item("kousinsya")))

            paramList.Add(MakeParam("@nyuuryoku_no", SqlDbType.Int, 4, strNyuuryokuNo))

            paramList.Add(MakeParam("@renkei_siji_cd9", SqlDbType.Int, 1, "9"))

            paramList.Add(MakeParam("@renkei_siji_cd2", SqlDbType.Int, 1, 5))

            paramList.Add(MakeParam("@nyuuryoku_no9", SqlDbType.Int, 8, .Item("nyuuryoku_no")))

        End With

        '更新されたデータセットを DB へ書き込み
        ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

        '終了処理
        commandTextSb = Nothing

        '戻り値をセット成功の場合
        InsTyuuiJikouRenkei2 = True
    End Function
    '''<summary>注意事項レコード行数を取得する</summary>
    Public Function SelTyuuiJikouCount(ByVal dtTyuiJyouhouUPDData As TyuiJyouhouDataSet.TyuuiJikouUPDTableDataTable) As Integer
        Dim commandTextSb As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        Dim intReturn As Integer = 0
        Dim dsReturn As New DataSet
        'SQL文

        commandTextSb.Remove(0, commandTextSb.Length)

        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine("  COUNT(kameiten_cd)")
        commandTextSb.AppendLine("  FROM  m_kameiten_tyuuijikou_renkei ")
        commandTextSb.AppendLine("  WITH (READCOMMITTED)  ")
        commandTextSb.AppendLine("  WHERE kameiten_cd = @kameiten_cd  ")
        commandTextSb.AppendLine("  AND nyuuryoku_no = (SELECT   ")
        commandTextSb.AppendLine("  MAX(nyuuryoku_no) ")
        commandTextSb.AppendLine("  FROM  m_kameiten_tyuuijikou WITH (READCOMMITTED)  ")
        commandTextSb.AppendLine("  WHERE kameiten_cd = @kameiten_cd ) ")
        'パラメータの設定
        With dtTyuiJyouhouUPDData.Rows(0)
            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, .Item("kameiten_cd")))
        End With
        '更新されたデータセットを DB へ書き込み
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, _
                            "dsReturn", paramList.ToArray)

        '終了処理
        commandTextSb = Nothing

        '戻り値をセット成功の場合
        Return dsReturn.Tables(0).Rows(0).Item(0)
    End Function
    ''' <summary>注意事項の新規処理</summary>
    Public Function InsTyuuiJikou(ByVal dtTyuiJyouhouUPDData As TyuiJyouhouDataSet.TyuuiJikouUPDTableDataTable) As Boolean
        '戻り値
        InsTyuuiJikou = False

        Dim commandTextSb As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        commandTextSb.AppendLine(" INSERT INTO ")
        commandTextSb.AppendLine(" m_kameiten_tyuuijikou ")
        commandTextSb.AppendLine(" WITH(UPDLOCK) ")
        commandTextSb.AppendLine("  (kameiten_cd,")
        commandTextSb.AppendLine("  tyuuijikou_syubetu, ")
        commandTextSb.AppendLine("  nyuuryoku_no, ")
        commandTextSb.AppendLine("  nyuuryoku_date, ")
        commandTextSb.AppendLine("  uketukesya_mei, ")
        commandTextSb.AppendLine("  naiyou, ")
        commandTextSb.AppendLine("  add_login_user_id, ")
        commandTextSb.AppendLine("  add_datetime, ")
        commandTextSb.AppendLine("  upd_login_user_id, ")
        commandTextSb.AppendLine("  upd_datetime) ")
        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine("  @kameiten_cd ,")
        commandTextSb.AppendLine("  @tyuuijikou_syubetu, ")
        commandTextSb.AppendLine("  ISNULL(MAX(nyuuryoku_no),0)+1, ")
        commandTextSb.AppendLine("  @nyuuryoku_date, ")
        commandTextSb.AppendLine("  @uketukesya_mei, ")
        commandTextSb.AppendLine("  @naiyou, ")
        commandTextSb.AppendLine("  @add_login_user_id, ")
        commandTextSb.AppendLine("  GETDATE(), ")
        commandTextSb.AppendLine("  @add_login_user_id, ")
        commandTextSb.AppendLine("  GETDATE() ")
        commandTextSb.AppendLine(" FROM  m_kameiten_tyuuijikou ")
        commandTextSb.AppendLine("WHERE kameiten_cd = @kameiten_cd  ")
        'パラメータの設定
        With dtTyuiJyouhouUPDData.Rows(0)
            paramList.Add(MakeParam("@tyuuijikou_syubetu", SqlDbType.Int, 4, .Item("tyuuijikou_syubetu")))
            paramList.Add(MakeParam("@nyuuryoku_date", SqlDbType.DateTime, 8, .Item("nyuuryoku_date")))
            paramList.Add(MakeParam("@uketukesya_mei", SqlDbType.VarChar, 20, .Item("uketukesya_mei")))
            paramList.Add(MakeParam("@naiyou", SqlDbType.VarChar, 80, .Item("naiyou")))
            paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, .Item("kousinsya")))
            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, .Item("kameiten_cd")))
        End With
        '更新されたデータセットを DB へ書き込み
        ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

        '終了処理
        commandTextSb = Nothing

        '戻り値をセット成功の場合
        InsTyuuiJikou = True
    End Function
    ''' <summary>注意事項の削除処理</summary>
    Public Function DelTyuuiJikou(ByVal strKameitenCd As String, ByVal strNyuuryokuNo As String, ByVal strUserId As String) As Boolean
        '戻り値
        DelTyuuiJikou = False

        Dim commandTextSb As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        commandTextSb.AppendLine(" DELETE ")
        commandTextSb.AppendLine(" FROM  m_kameiten_tyuuijikou ")
        commandTextSb.AppendLine(" WITH(UPDLOCK) ")
        commandTextSb.AppendLine("WHERE kameiten_cd = @kameiten_cd  ")
        commandTextSb.AppendLine(" AND  nyuuryoku_no = @nyuuryoku_no  ;")

     
        Dim sql1 As New StringBuilder
        sql1.AppendLine(" UPDATE ")
        sql1.AppendLine("  m_kameiten_tyuuijikou ")
        sql1.AppendLine(" WITH(UPDLOCK) ")
        sql1.AppendLine(" 	SET nyuuryoku_no = nyuuryoku_no-1")
        sql1.Append(",upd_login_user_id	=	@upd_login_user_id")
        sql1.Append(",upd_datetime	=	getdate()")
        sql1.AppendLine(" WHERE")
        sql1.AppendLine(" 	kameiten_cd = @kameiten_cd")
        sql1.AppendLine(" AND nyuuryoku_no>@nyuuryoku_no")

       
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))
        paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, strUserId))
        paramList.Add(MakeParam("@nyuuryoku_no", SqlDbType.Int, 4, strNyuuryokuNo))
        commandTextSb.Append(sql1.ToString)
        ExecuteNonQuery(connStr, _
                                           CommandType.Text, _
                                           commandTextSb.ToString(), _
                                           paramList.ToArray)




        '終了処理
        commandTextSb = Nothing

        '戻り値をセット成功の場合
        DelTyuuiJikou = True

    End Function
    ''' <summary>優先注意事項を取得する</summary>
    Public Function SelYuusenTyuuiJikouInfo(ByVal strKameitenCd As String, ByVal strUerId As String) As TyuiJyouhouDataSet.TyuuiJikouTableDataTable
        ' DataSetインスタンスの生成()
        Dim dsTyuiJyouhouDataSet As New TyuiJyouhouDataSet
        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        commandTextSb.AppendLine(" SELECT ")

        commandTextSb.AppendLine(" CONVERT(varchar(4), m_kameiten_tyuuijikou.tyuuijikou_syubetu)+':'+CONVERT(varchar(4), m_kameiten_tyuuijikou.nyuuryoku_no) AS tyuuijikou_syubetu, ")
        'commandTextSb.AppendLine(" m_meisyou.meisyou AS meisyou, ")
        commandTextSb.AppendLine(" m_kameiten_tyuuijikou.nyuuryoku_date AS nyuuryoku_date, ")
        commandTextSb.AppendLine(" m_kameiten_tyuuijikou.uketukesya_mei AS uketukesya_mei, ")
        commandTextSb.AppendLine(" m_kameiten_tyuuijikou.naiyou AS naiyou, ")
        commandTextSb.AppendLine(" m_kameiten_tyuuijikou.upd_datetime AS upd_datetime ")
        commandTextSb.AppendLine(" FROM m_jiban_ninsyou WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" LEFT JOIN m_tyuuijikou_yuusen  WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" ON m_jiban_ninsyou.gyoumu_kbn=m_tyuuijikou_yuusen.gyoumu_kbn ")
        commandTextSb.AppendLine(" INNER JOIN ")
        commandTextSb.AppendLine(" (SELECT tyuuijikou_syubetu, ")
        commandTextSb.AppendLine(" nyuuryoku_no, ")
        commandTextSb.AppendLine(" nyuuryoku_date, ")
        commandTextSb.AppendLine(" uketukesya_mei, ")
        commandTextSb.AppendLine(" upd_datetime, ")
        commandTextSb.AppendLine(" naiyou ")
        commandTextSb.AppendLine("FROM m_kameiten_tyuuijikou  WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" WHERE kameiten_cd=@kameiten_cd) AS m_kameiten_tyuuijikou")
        commandTextSb.AppendLine("ON m_kameiten_tyuuijikou.tyuuijikou_syubetu=m_tyuuijikou_yuusen.tyuuijikou_syubetu ")
        commandTextSb.AppendLine(" LEFT JOIN  ")
        commandTextSb.AppendLine(" (SELECT ")
        commandTextSb.AppendLine(" code, ")
        commandTextSb.AppendLine(" meisyou ")
        commandTextSb.AppendLine(" FROM  ")
        commandTextSb.AppendLine(" m_meisyou  WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" WHERE ")
        commandTextSb.AppendLine(" meisyou_syubetu=@meisyou_syubetu ")
        commandTextSb.AppendLine(" ) AS m_meisyou ")
        commandTextSb.AppendLine(" ON  m_kameiten_tyuuijikou.tyuuijikou_syubetu=m_meisyou.code ")

        commandTextSb.AppendLine(" WHERE  m_jiban_ninsyou.login_user_id=@login_user_id ")
        commandTextSb.AppendLine(" AND  m_jiban_ninsyou.torikesi=@torikesi ")
        '========2015/02/04 王莎莎 407679の対応 追加↓======================
        commandTextSb.AppendLine(" AND m_kameiten_tyuuijikou.tyuuijikou_syubetu not in ('90','91','97') ")
        '========2015/02/04 王莎莎 407679の対応 追加↑======================
        commandTextSb.AppendLine(" ORDER BY m_tyuuijikou_yuusen.hyouji_jyun ")
        'パラメータの設定
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, "0"))
        paramList.Add(MakeParam("@meisyou_syubetu", SqlDbType.Char, 2, "10"))
        paramList.Add(MakeParam("@login_user_id", SqlDbType.VarChar, 30, strUerId))
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsTyuiJyouhouDataSet, _
                    dsTyuiJyouhouDataSet.TyuuiJikouTable.TableName, paramList.ToArray)

        Return dsTyuiJyouhouDataSet.TyuuiJikouTable

    End Function
    ''' <summary>通常注意事項を取得する</summary>
    Public Function SelTuujyouTyuuiJikouInfo(ByVal strKameitenCd As String, ByVal strUerId As String) As TyuiJyouhouDataSet.TyuuiJikouTableDataTable
        ' DataSetインスタンスの生成()
        Dim dsTyuiJyouhouDataSet As New TyuiJyouhouDataSet
        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        commandTextSb.AppendLine("  SELECT  ")
        commandTextSb.AppendLine("  CONVERT(varchar(4), m_kameiten_tyuuijikou.tyuuijikou_syubetu)+':'+CONVERT(varchar(4), m_kameiten_tyuuijikou.nyuuryoku_no) AS tyuuijikou_syubetu,  ")
        'commandTextSb.AppendLine("  m_meisyou.meisyou AS meisyou,  ")
        commandTextSb.AppendLine("  m_kameiten_tyuuijikou.nyuuryoku_date AS nyuuryoku_date,  ")
        commandTextSb.AppendLine("  m_kameiten_tyuuijikou.uketukesya_mei AS uketukesya_mei,  ")
        commandTextSb.AppendLine("  m_kameiten_tyuuijikou.naiyou AS naiyou,  ")
        commandTextSb.AppendLine(" m_kameiten_tyuuijikou.upd_datetime AS upd_datetime ")
        commandTextSb.AppendLine(" FROM  ")
        commandTextSb.AppendLine(" (SELECT tyuuijikou_syubetu , ")
        commandTextSb.AppendLine("  nyuuryoku_no,  ")
        commandTextSb.AppendLine("  nyuuryoku_date,  ")
        commandTextSb.AppendLine("  uketukesya_mei,  ")
        commandTextSb.AppendLine("  upd_datetime,  ")
        commandTextSb.AppendLine("  naiyou  ")
        commandTextSb.AppendLine(" FROM m_kameiten_tyuuijikou  WITH (READCOMMITTED)  ")
        commandTextSb.AppendLine("  WHERE kameiten_cd=@kameiten_cd ) AS m_kameiten_tyuuijikou ")
        commandTextSb.AppendLine("  LEFT JOIN   ")
        commandTextSb.AppendLine("  (SELECT  ")
        commandTextSb.AppendLine("  code,  ")
        commandTextSb.AppendLine("  meisyou  ")
        commandTextSb.AppendLine("  FROM   ")
        commandTextSb.AppendLine("  m_meisyou  WITH (READCOMMITTED)  ")
        commandTextSb.AppendLine("  WHERE  ")
        commandTextSb.AppendLine("  meisyou_syubetu=@meisyou_syubetu ")
        commandTextSb.AppendLine("  ) AS m_meisyou  ")
        commandTextSb.AppendLine("  ON  m_kameiten_tyuuijikou.tyuuijikou_syubetu=m_meisyou.code  ")
        commandTextSb.AppendLine(" WHERE  ")
        commandTextSb.AppendLine(" m_kameiten_tyuuijikou.tyuuijikou_syubetu NOT IN ")
        commandTextSb.AppendLine(" (SELECT ISNULL(m_tyuuijikou_yuusen.tyuuijikou_syubetu,'') ")
        commandTextSb.AppendLine("  FROM m_jiban_ninsyou WITH (READCOMMITTED)  ")
        commandTextSb.AppendLine("  LEFT JOIN m_tyuuijikou_yuusen  WITH (READCOMMITTED)  ")
        commandTextSb.AppendLine("  ON m_jiban_ninsyou.gyoumu_kbn=m_tyuuijikou_yuusen.gyoumu_kbn  ")
        commandTextSb.AppendLine("  WHERE  m_jiban_ninsyou.login_user_id=@login_user_id ")
        commandTextSb.AppendLine("  AND  m_jiban_ninsyou.torikesi=@torikesi ) ")
        '========2015/02/04 王莎莎 407679の対応 追加↓======================
        commandTextSb.AppendLine(" AND m_kameiten_tyuuijikou.tyuuijikou_syubetu not in ('90','91','97') ")
        '========2015/02/04 王莎莎 407679の対応 追加↑======================
        commandTextSb.AppendLine("  ORDER BY nyuuryoku_no  ")
        'パラメータの設定
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, "0"))
        paramList.Add(MakeParam("@meisyou_syubetu", SqlDbType.Char, 2, "10"))
        paramList.Add(MakeParam("@login_user_id", SqlDbType.VarChar, 30, strUerId))
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsTyuiJyouhouDataSet, _
                    dsTyuiJyouhouDataSet.TyuuiJikouTable.TableName, paramList.ToArray)

        Return dsTyuiJyouhouDataSet.TyuuiJikouTable

    End Function


    ''' <summary>種別を取得する</summary>
    Public Function SelSyubetuInfo(ByVal flg As String) As TyuiJyouhouDataSet.MeisyouTableDataTable
        ' DataSetインスタンスの生成()
        Dim dsTyuiJyouhouDataSet As New TyuiJyouhouDataSet
        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine(" code, ")
        commandTextSb.AppendLine(" meisyou ")
        commandTextSb.AppendLine(" FROM  ")
        commandTextSb.AppendLine(" m_meisyou WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" WHERE meisyou_syubetu=@meisyou_syubetu ")

        '========2015/02/04 王莎莎 407679の対応 追加↓======================
        If flg = "TORA" Then
            commandTextSb.AppendLine(" AND code in ('90','91','97') ")
        Else
            commandTextSb.AppendLine(" AND code not in ('90','91','97') ")
        End If

        '========2015/02/04 王莎莎 407679の対応 追加↑======================

        commandTextSb.AppendLine(" ORDER BY hyouji_jyun ")
        'パラメータの設定
        paramList.Add(MakeParam("@meisyou_syubetu", SqlDbType.Char, 2, "10"))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsTyuiJyouhouDataSet, _
                    dsTyuiJyouhouDataSet.MeisyouTable.TableName, paramList.ToArray)

        Return dsTyuiJyouhouDataSet.MeisyouTable

    End Function
    ''' <summary>調査会社情報を取得する</summary>

    Public Function SelKaisyaJyouhouInfo(ByVal strKameitenCd As String, ByVal strKbn As String) As TyuiJyouhouDataSet.KaisyaTableDataTable
        ' DataSetインスタンスの生成()
        Dim dsTyuiJyouhouDataSet As New TyuiJyouhouDataSet
        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine(" m_kameiten_tyousakaisya.nyuuryoku_no, ")
        commandTextSb.AppendLine(" m_kameiten_tyousakaisya.tys_kaisya_cd+m_kameiten_tyousakaisya.jigyousyo_cd+':'+CONVERT(varchar(4), m_kameiten_tyousakaisya.nyuuryoku_no) AS tys_code, ")
        'commandTextSb.AppendLine(" m_meisyou.tys_mei AS tys_mei, ")
        commandTextSb.AppendLine(" m_kameiten_tyousakaisya.upd_datetime AS upd_datetime ")
        commandTextSb.AppendLine(" FROM  ")
        commandTextSb.AppendLine(" m_kameiten_tyousakaisya  WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" LEFT JOIN  ")
        commandTextSb.AppendLine(" (SELECT ")
        commandTextSb.AppendLine(" tys_kaisya_cd+jigyousyo_cd AS tys_code, ")
        commandTextSb.AppendLine(" tys_kaisya_mei AS tys_mei ")
        commandTextSb.AppendLine(" FROM  ")
        commandTextSb.AppendLine(" m_tyousakaisya  WITH (READCOMMITTED) ")
        'commandTextSb.AppendLine(" WHERE torikesi=@torikesi ")
        commandTextSb.AppendLine(" ) AS m_meisyou ")
        commandTextSb.AppendLine(" ON  m_kameiten_tyousakaisya.tys_kaisya_cd+m_kameiten_tyousakaisya.jigyousyo_cd=m_meisyou.tys_code ")

        commandTextSb.AppendLine(" WHERE m_kameiten_tyousakaisya.kaisya_kbn=@kaisya_kbn ")
        commandTextSb.AppendLine(" AND m_kameiten_tyousakaisya.kahi_kbn=@kahi_kbn ")
        commandTextSb.AppendLine(" AND m_kameiten_tyousakaisya.kameiten_cd=@kameiten_cd ")
        commandTextSb.AppendLine(" ORDER BY m_kameiten_tyousakaisya.nyuuryoku_no ")
        'パラメータの設定
        'paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, "0"))
        paramList.Add(MakeParam("@kaisya_kbn", SqlDbType.Int, 4, Left(strKbn, 1)))
        paramList.Add(MakeParam("@kahi_kbn", SqlDbType.Int, 4, Right(strKbn, 1)))
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))
        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsTyuiJyouhouDataSet, _
                    dsTyuiJyouhouDataSet.KaisyaTable.TableName, paramList.ToArray)

        Return dsTyuiJyouhouDataSet.KaisyaTable

    End Function
    ''' <summary>会社を取得する</summary>
    Public Function SelKaisyaInfo(Optional ByVal strKaisyaCd As String = "") As TyuiJyouhouDataSet.KaisyaTableDataTable
        ' DataSetインスタンスの生成()
        Dim dsTyuiJyouhouDataSet As New TyuiJyouhouDataSet
        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine(" tys_kaisya_cd+jigyousyo_cd AS tys_code ,")
        commandTextSb.AppendLine(" tys_kaisya_mei AS tys_mei ")
        commandTextSb.AppendLine(" FROM  ")
        commandTextSb.AppendLine(" m_tyousakaisya WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" WHERE torikesi=@torikesi ")
        If strKaisyaCd <> "" Then
            commandTextSb.AppendLine(" AND tys_kaisya_cd+jigyousyo_cd=@tys_kaisya_cd ")
            paramList.Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 7, strKaisyaCd))
        End If

        commandTextSb.AppendLine(" ORDER BY tys_kaisya_mei_kana ")
        'パラメータの設定
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, "0"))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsTyuiJyouhouDataSet, _
                    dsTyuiJyouhouDataSet.KaisyaTable.TableName, paramList.ToArray)

        Return dsTyuiJyouhouDataSet.KaisyaTable

    End Function

    ''' <summary>基礎仕様を取得する</summary>
    Public Function SelKisoSiyouInfo(Optional ByVal strKsno As String = "") As TyuiJyouhouDataSet.KisoSiyouTableDataTable
        ' DataSetインスタンスの生成()
        Dim dsTyuiJyouhouDataSet As New TyuiJyouhouDataSet
        'SQL文の生成
        Dim commandTextSb As New StringBuilder
        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        'SQL文
        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine(" ks_siyou_no AS ks_siyou_no ,")
        commandTextSb.AppendLine(" ks_siyou AS ks_siyou ")
        commandTextSb.AppendLine(" FROM  ")
        commandTextSb.AppendLine(" m_ks_siyou WITH (READCOMMITTED) ")
        If strKsno <> "" Then
            commandTextSb.AppendLine(" WHERE CONVERT(VARCHAR(11),ks_siyou_no)=@ks_siyou_no ")
            paramList.Add(MakeParam("@ks_siyou_no", SqlDbType.VarChar, 11, strKsno))
        End If

        'パラメータの設定

        commandTextSb.AppendLine(" ORDER BY ks_siyou_no ")


        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsTyuiJyouhouDataSet, _
                    dsTyuiJyouhouDataSet.KisoSiyouTable.TableName, paramList.ToArray)

        Return dsTyuiJyouhouDataSet.KisoSiyouTable

    End Function
    '''<summary>基礎仕様排他処理</summary>
    Public Function SelKisoSiyouHaita(ByVal strKameitenCd As String, ByVal strKsSiyouNo As String) As DataTable
        Dim commandTextSb As New System.Text.StringBuilder

        Dim dsReturn As New DataSet
        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine(" upd_login_user_id AS login_user_id, ")
        commandTextSb.AppendLine(" upd_datetime AS upd_datetime ")
        commandTextSb.AppendLine(" FROM  m_kameiten_ks_siyou_settei WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" WHERE kameiten_cd = @kameiten_cd  ")
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))
        'パラメータの設定


        commandTextSb.AppendLine(" AND  ks_siyou_no = @ks_siyou_no  ")
        paramList.Add(MakeParam("@ks_siyou_no", SqlDbType.Int, 4, strKsSiyouNo))



        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, _
                    "dsReturn", paramList.ToArray)

        Return dsReturn.Tables(0)

    End Function

    '''<summary>基礎仕様設定を取得する</summary>
    Public Function SelKisoSiyouSetteiInfo(ByVal strKameitenCd As String, ByVal strKahiKbn As String) As TyuiJyouhouDataSet.KisoSiyouTableDataTable
        ' DataSetインスタンスの生成()
        Dim dsTyuiJyouhouDataSet As New TyuiJyouhouDataSet
        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine(" m_kameiten_ks_siyou_settei.nyuuryoku_no, ")
        commandTextSb.AppendLine(" CONVERT(varchar(4), m_kameiten_ks_siyou_settei.ks_siyou_no)+':'+ CONVERT(varchar(4), m_kameiten_ks_siyou_settei.nyuuryoku_no) AS ks_siyou_no ,")
        'commandTextSb.AppendLine(" m_ks_siyou.ks_siyou AS ks_siyou, ")
        commandTextSb.AppendLine(" m_kameiten_ks_siyou_settei.upd_datetime AS upd_datetime ")
        commandTextSb.AppendLine(" FROM  ")
        commandTextSb.AppendLine(" m_kameiten_ks_siyou_settei WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" LEFT JOIN ")
        commandTextSb.AppendLine(" m_ks_siyou  WITH (READCOMMITTED)  ")
        commandTextSb.AppendLine(" ON m_kameiten_ks_siyou_settei.ks_siyou_no=m_ks_siyou.ks_siyou_no ")
        commandTextSb.AppendLine(" WHERE  ")
        commandTextSb.AppendLine(" m_kameiten_ks_siyou_settei.kameiten_cd=@kameiten_cd  ")
        commandTextSb.AppendLine(" AND m_kameiten_ks_siyou_settei.kahi_kbn=@kahi_kbn  ")
        commandTextSb.AppendLine(" ORDER BY  m_kameiten_ks_siyou_settei.nyuuryoku_no  ")
        'パラメータの設定
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))
        paramList.Add(MakeParam("@kahi_kbn", SqlDbType.Int, 4, strKahiKbn))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsTyuiJyouhouDataSet, _
                    dsTyuiJyouhouDataSet.KisoSiyouTable.TableName, paramList.ToArray)

        Return dsTyuiJyouhouDataSet.KisoSiyouTable

    End Function
    '''<summary>基礎仕様設定レコード行数を取得する</summary>
    Public Function SelKisoSiyouSetteiCount(ByVal strKameitenCd As String, ByVal strKsSiyouNo As String) As Integer

        Dim commandTextSb As New System.Text.StringBuilder
        Dim dtReturn As New DataSet
        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        commandTextSb.Remove(0, commandTextSb.Length)

        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine("  COUNT(kameiten_cd)")
        commandTextSb.AppendLine("  FROM  m_kameiten_ks_siyou_settei_renkei WITH (READCOMMITTED)  ")
        commandTextSb.AppendLine("  WHERE kameiten_cd = @kameiten_cd  ")
        commandTextSb.AppendLine("  AND ks_siyou_no=@ks_siyou_no ")

        'パラメータの設定
        paramList.Add(MakeParam("@ks_siyou_no", SqlDbType.Int, 4, strKsSiyouNo))
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))


        '更新されたデータセットを DB へ書き込み
        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dtReturn, _
                    "dtReturn", paramList.ToArray)


        '終了処理
        commandTextSb = Nothing

        '戻り値をセット成功の場合
        Return dtReturn.Tables(0).Rows(0).Item(0)
    End Function
    '''<summary>基礎仕様設定連携の新規処理</summary>
    Public Function InsKisoSiyouSetteiRenkei(ByVal strKameitenCd As String, ByVal strKsSiyouNo As String, ByVal strKousinsya As String) As Boolean
        '戻り値
        InsKisoSiyouSetteiRenkei = False

        Dim commandTextSb As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        commandTextSb.Remove(0, commandTextSb.Length)
        commandTextSb.AppendLine(" INSERT INTO ")
        commandTextSb.AppendLine(" m_kameiten_ks_siyou_settei_renkei ")
        commandTextSb.AppendLine(" WITH(UPDLOCK) ")
        commandTextSb.AppendLine("  (kameiten_cd,")
        commandTextSb.AppendLine("  ks_siyou_no, ")
        commandTextSb.AppendLine("  renkei_siji_cd, ")
        commandTextSb.AppendLine("  sousin_jyky_cd, ")
        commandTextSb.AppendLine("  upd_login_user_id, ")
        commandTextSb.AppendLine("  upd_datetime) ")
        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine("  kameiten_cd ,")
        commandTextSb.AppendLine("  ks_siyou_no, ")
        commandTextSb.AppendLine("  @renkei_siji_cd, ")
        commandTextSb.AppendLine("  @sousin_jyky_cd, ")
        commandTextSb.AppendLine("  @upd_login_user_id, ")
        commandTextSb.AppendLine("  GETDATE() ")
        commandTextSb.AppendLine("  FROM  m_kameiten_ks_siyou_settei WITH (READCOMMITTED)  ")
        commandTextSb.AppendLine("  WHERE kameiten_cd = @kameiten_cd  ")
        commandTextSb.AppendLine("  AND ks_siyou_no=@ks_siyou_no ")

        'パラメータの設定
        paramList.Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 4, "1"))
        paramList.Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 4, "0"))
        paramList.Add(MakeParam("@ks_siyou_no", SqlDbType.Int, 4, strKsSiyouNo))
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))
        paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, strKousinsya))

        '更新されたデータセットを DB へ書き込み
        ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

        '終了処理
        commandTextSb = Nothing

        '戻り値をセット成功の場合
        InsKisoSiyouSetteiRenkei = True
    End Function
    '''<summary>基礎仕様設定の新規処理</summary>
    Public Function InsKisoSiyouSettei(ByVal strKameitenCd As String, ByVal strKahiKbn As String, ByVal strKsSiyouNo As String, ByVal strKousinsya As String) As Boolean
        '戻り値
        InsKisoSiyouSettei = False

        Dim commandTextSb As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        Dim intReturn As Integer = 0
        Dim dsReturn As New DataSet
        'SQL文
        commandTextSb.AppendLine(" SELECT COUNT(kameiten_cd) ")
        commandTextSb.AppendLine("  FROM  m_kameiten_ks_siyou_settei WITH (READCOMMITTED)  ")
        commandTextSb.AppendLine("  WHERE kameiten_cd = @kameiten_cd  ")
        commandTextSb.AppendLine("  AND kahi_kbn=@kahi_kbn ")
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))
        paramList.Add(MakeParam("@kahi_kbn", SqlDbType.Int, 4, strKahiKbn))
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dsReturn", paramList.ToArray)
        intReturn = dsReturn.Tables(0).Rows(0).Item(0)
        dsReturn.Dispose()

        commandTextSb.Remove(0, commandTextSb.Length)
        commandTextSb.AppendLine(" INSERT INTO ")
        commandTextSb.AppendLine(" m_kameiten_ks_siyou_settei ")
        commandTextSb.AppendLine(" WITH(UPDLOCK) ")
        commandTextSb.AppendLine("  (kameiten_cd,")
        commandTextSb.AppendLine("  kahi_kbn, ")
        commandTextSb.AppendLine("  ks_siyou_no, ")
        commandTextSb.AppendLine("  nyuuryoku_no, ")
        commandTextSb.AppendLine("  add_login_user_id, ")
        commandTextSb.AppendLine("  add_datetime) ")
        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine("  @kameiten_cd ,")
        commandTextSb.AppendLine("  @kahi_kbn, ")
        commandTextSb.AppendLine("  @ks_siyou_no, ")

        commandTextSb.AppendLine("   ISNULL(MAX(nyuuryoku_no),0)+1, ")
        commandTextSb.AppendLine("  @kousinsya, ")
        commandTextSb.AppendLine("  GETDATE() ")
        commandTextSb.AppendLine("  FROM  m_kameiten_ks_siyou_settei WITH (READCOMMITTED)  ")
        commandTextSb.AppendLine("  WHERE kameiten_cd = @kameiten_cd  ")
        commandTextSb.AppendLine("  AND kahi_kbn=@kahi_kbn ")
        If intReturn <> 0 Then
            commandTextSb.AppendLine("  GROUP BY kameiten_cd,kahi_kbn")
        End If

        'パラメータの設定

        paramList.Add(MakeParam("@ks_siyou_no", SqlDbType.Int, 4, strKsSiyouNo))
        paramList.Add(MakeParam("@kousinsya", SqlDbType.VarChar, 30, strKousinsya))

        '更新されたデータセットを DB へ書き込み
        ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

        '終了処理
        commandTextSb = Nothing

        '戻り値をセット成功の場合
        InsKisoSiyouSettei = True
    End Function
    '''<summary>基礎仕様設定の新規処理</summary>
    Public Function InsKisoSiyouSettei2(ByVal strKameitenCd As String, ByVal strKousinsya As String) As Boolean
        '戻り値
        InsKisoSiyouSettei2 = False

        Dim commandTextSb As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        Dim intReturn As Integer = 0
        Dim dsReturn As New DataSet
        'SQL文
        commandTextSb.AppendLine(" INSERT INTO ")
        commandTextSb.AppendLine(" m_kameiten_ks_siyou_settei_renkei ")
        commandTextSb.AppendLine(" WITH(UPDLOCK) ")
        commandTextSb.AppendLine("  (kameiten_cd,")
        commandTextSb.AppendLine("  ks_siyou_no, ")
        commandTextSb.AppendLine("  renkei_siji_cd, ")
        commandTextSb.AppendLine("  sousin_jyky_cd, ")
        commandTextSb.AppendLine("  upd_login_user_id, ")
        commandTextSb.AppendLine("  upd_datetime) ")
        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine("  kameiten_cd ,")
        commandTextSb.AppendLine("  ks_siyou_no, ")
        commandTextSb.AppendLine("  @renkei_siji_cd, ")
        commandTextSb.AppendLine("  @sousin_jyky_cd, ")
        commandTextSb.AppendLine("  @upd_login_user_id, ")
        commandTextSb.AppendLine("  GETDATE() ")
        commandTextSb.AppendLine("  FROM  m_kameiten_ks_siyou_settei WITH (READCOMMITTED)  ")
        commandTextSb.AppendLine("  WHERE kameiten_cd = @kameiten_cd  ")
        commandTextSb.AppendLine("  AND ks_siyou_no NOT IN   ")
        commandTextSb.AppendLine("  (  ")
        commandTextSb.AppendLine("  SELECT ks_siyou_no ")
        commandTextSb.AppendLine("  FROM  m_kameiten_ks_siyou_settei_renkei WITH (READCOMMITTED)  ")
        commandTextSb.AppendLine("  WHERE kameiten_cd = @kameiten_cd  ")
        commandTextSb.AppendLine("  )  ")
        'パラメータの設定
        paramList.Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 4, "2"))
        paramList.Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 4, "0"))

        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))
        paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, strKousinsya))


        '更新されたデータセットを DB へ書き込み
        ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

        '終了処理
        commandTextSb = Nothing

        '戻り値をセット成功の場合
        InsKisoSiyouSettei2 = True
    End Function
    '''<summary>基礎仕様設定連携の更新処理</summary>
    Public Function UpdKisoSiyouSetteiRenkei(ByVal strKameitenCd As String, ByVal strKsSiyouNo As String, ByVal strUerId As String, ByVal strCd As String, Optional ByVal intRow As String = "", Optional ByVal strKahiKbn As String = "") As Boolean
        '戻り値
        UpdKisoSiyouSetteiRenkei = False

        Dim commandTextSb As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        If strCd = "9" Then

            commandTextSb.AppendLine(" INSERT INTO ")
            commandTextSb.AppendLine(" m_kameiten_ks_siyou_settei_renkei ")
            commandTextSb.AppendLine(" WITH(UPDLOCK) ")
            commandTextSb.AppendLine("  (kameiten_cd,")
            commandTextSb.AppendLine("  ks_siyou_no, ")
            commandTextSb.AppendLine("  renkei_siji_cd, ")
            commandTextSb.AppendLine("  sousin_jyky_cd, ")
            commandTextSb.AppendLine("  upd_login_user_id, ")
            commandTextSb.AppendLine("  upd_datetime) ")
            commandTextSb.AppendLine(" SELECT ")
            commandTextSb.AppendLine("  @kameiten_cd ,")
            commandTextSb.AppendLine("  ks_siyou_no, ")
            commandTextSb.AppendLine("  '5', ")
            commandTextSb.AppendLine("  @sousin_jyky_cd, ")
            commandTextSb.AppendLine("  @upd_login_user_id, ")
            commandTextSb.AppendLine("  GETDATE() FROM  m_kameiten_ks_siyou_settei  WITH (READCOMMITTED) ")
            commandTextSb.AppendLine("  WHERE kameiten_cd=@kameiten_cd ")
            commandTextSb.AppendLine("  AND kahi_kbn=@kahi_kbn ")
            commandTextSb.AppendLine("  AND ks_siyou_no NOT IN (")
            commandTextSb.AppendLine("  SELECT ks_siyou_no FROM  m_kameiten_ks_siyou_settei_renkei  WITH (READCOMMITTED) ")
            commandTextSb.AppendLine("  WHERE kameiten_cd=@kameiten_cd ")
            commandTextSb.AppendLine(")")
            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))
            paramList.Add(MakeParam("@ks_siyou_no", SqlDbType.Int, 4, strKsSiyouNo))
            paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, strUerId))
            paramList.Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 4, "0"))
            paramList.Add(MakeParam("@kahi_kbn", SqlDbType.Int, 4, strKahiKbn))
            ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)
            paramList.Clear()
            commandTextSb.Remove(0, commandTextSb.Length)
        End If
        'SQL文
        commandTextSb.AppendLine(" UPDATE ")
        commandTextSb.AppendLine(" m_kameiten_ks_siyou_settei_renkei ")
        commandTextSb.AppendLine(" WITH(UPDLOCK) ")
        commandTextSb.AppendLine(" SET ")
        commandTextSb.AppendLine(" sousin_jyky_cd=@sousin_jyky_cd, ")
        commandTextSb.AppendLine(" renkei_siji_cd=@renkei_siji_cd, ")
        commandTextSb.AppendLine("   ks_siyou_no = @ks_siyou_no,  ")
        commandTextSb.AppendLine(" upd_login_user_id=@upd_login_user_id, ")
        commandTextSb.AppendLine(" upd_datetime=GETDATE() ")
        commandTextSb.AppendLine("WHERE kameiten_cd = @kameiten_cd  ")
        commandTextSb.AppendLine(" AND  ks_siyou_no = @ks_siyou_no1  ")

        'パラメータの設定

        If strCd = "9" Or strCd = "1" Then
            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))
            paramList.Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 4, strCd))
            paramList.Add(MakeParam("@ks_siyou_no", SqlDbType.Int, 4, strKsSiyouNo))
            paramList.Add(MakeParam("@ks_siyou_no1", SqlDbType.Int, 4, strKsSiyouNo))
            paramList.Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 4, "0"))
            paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, strUerId))
            '更新されたデータセットを DB へ書き込み
            If ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray) = 0 And strCd = "9" Then
                commandTextSb.Remove(0, commandTextSb.Length)
                commandTextSb.AppendLine(" INSERT INTO ")
                commandTextSb.AppendLine(" m_kameiten_ks_siyou_settei_renkei ")
                commandTextSb.AppendLine(" WITH(UPDLOCK) ")
                commandTextSb.AppendLine("  (kameiten_cd,")
                commandTextSb.AppendLine("  ks_siyou_no, ")
                commandTextSb.AppendLine("  renkei_siji_cd, ")
                commandTextSb.AppendLine("  sousin_jyky_cd, ")
                commandTextSb.AppendLine("  upd_login_user_id, ")
                commandTextSb.AppendLine("  upd_datetime) ")
                commandTextSb.AppendLine(" SELECT ")
                commandTextSb.AppendLine("  @kameiten_cd ,")
                commandTextSb.AppendLine("  @ks_siyou_no, ")
                commandTextSb.AppendLine("  @renkei_siji_cd, ")
                commandTextSb.AppendLine("  @sousin_jyky_cd, ")
                commandTextSb.AppendLine("  @upd_login_user_id, ")
                commandTextSb.AppendLine("  GETDATE() ")

                paramList.Clear()
                'パラメータの設定
                paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))
                paramList.Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 4, strCd))
                paramList.Add(MakeParam("@ks_siyou_no", SqlDbType.Int, 4, strKsSiyouNo))
                paramList.Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 4, "0"))
                paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, strUerId))
                ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

            End If

            If strCd = "9" Then
                commandTextSb.Remove(0, commandTextSb.Length)
                commandTextSb.AppendLine(" UPDATE ")
                commandTextSb.AppendLine(" m_kameiten_ks_siyou_settei_renkei ")
                commandTextSb.AppendLine(" WITH(UPDLOCK) ")
                commandTextSb.AppendLine(" SET ")
                commandTextSb.AppendLine("  renkei_siji_cd=case renkei_siji_cd when '9' then '9' else '2' end ")
                commandTextSb.AppendLine(" WHERE kameiten_cd = @kameiten_cd ")
                commandTextSb.AppendLine(" AND ks_siyou_no IN  ")
                commandTextSb.AppendLine(" (SELECT  ks_siyou_no FROM  ")
                commandTextSb.AppendLine(" m_kameiten_ks_siyou_settei WITH (READCOMMITTED)  ")
                commandTextSb.AppendLine(" WHERE kameiten_cd = @kameiten_cd ")
                commandTextSb.AppendLine(" AND kahi_kbn=@kahi_kbn  ")
                commandTextSb.AppendLine(" AND nyuuryoku_no>=@nyuuryoku_no)  ")
                paramList.Add(MakeParam("@kahi_kbn", SqlDbType.Int, 4, strKahiKbn))
                paramList.Add(MakeParam("@nyuuryoku_no", SqlDbType.Int, 4, intRow))
                commandTextSb.AppendLine(" DELETE FROM  ")
                commandTextSb.AppendLine(" m_kameiten_ks_siyou_settei_renkei ")
                commandTextSb.AppendLine(" WITH(UPDLOCK) ")
                commandTextSb.AppendLine(" WHERE kameiten_cd = @kameiten_cd ")
                commandTextSb.AppendLine(" AND renkei_siji_cd='5'  ")



                ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

            End If


        Else
            commandTextSb.Remove(0, commandTextSb.Length)
            paramList.Clear()
            commandTextSb.AppendLine(" DELETE FROM ")
            commandTextSb.AppendLine(" m_kameiten_ks_siyou_settei_renkei ")
            commandTextSb.AppendLine(" WITH(UPDLOCK) ")
            commandTextSb.AppendLine("WHERE kameiten_cd = @kameiten_cd  ")
            commandTextSb.AppendLine(" AND  (ks_siyou_no = @ks_siyou_no  ")
            commandTextSb.AppendLine(" OR ks_siyou_no = @ks_siyou_no1) ")
            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))
            paramList.Add(MakeParam("@ks_siyou_no", SqlDbType.Int, 4, Split(strKsSiyouNo, ":")(0)))
            paramList.Add(MakeParam("@ks_siyou_no1", SqlDbType.Int, 4, Split(strKsSiyouNo, ":")(1)))
            ' ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)
            commandTextSb.AppendLine(" INSERT INTO ")
            commandTextSb.AppendLine(" m_kameiten_ks_siyou_settei_renkei ")
            commandTextSb.AppendLine(" WITH(UPDLOCK) ")
            commandTextSb.AppendLine("  (kameiten_cd,")
            commandTextSb.AppendLine("  ks_siyou_no, ")
            commandTextSb.AppendLine("  renkei_siji_cd, ")
            commandTextSb.AppendLine("  sousin_jyky_cd, ")
            commandTextSb.AppendLine("  upd_login_user_id, ")
            commandTextSb.AppendLine("  upd_datetime) ")
            commandTextSb.AppendLine(" SELECT ")
            commandTextSb.AppendLine("  @kameiten_cd ,")
            commandTextSb.AppendLine("  @ks_siyou_no, ")
            commandTextSb.AppendLine("  @renkei_siji_cd, ")
            commandTextSb.AppendLine("  @sousin_jyky_cd, ")
            commandTextSb.AppendLine("  @upd_login_user_id, ")
            commandTextSb.AppendLine("  GETDATE() ")

            paramList.Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 4, "0"))
            paramList.Add(MakeParam("@renkei_siji_cd1", SqlDbType.Int, 4, "9"))
            paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, strUerId))
            If Split(strKsSiyouNo, ":")(0) = Split(strKsSiyouNo, ":")(1) Then
                paramList.Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 4, "2"))
            Else
                paramList.Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 4, "1"))
            End If
            If Not Split(strKsSiyouNo, ":")(0) = Split(strKsSiyouNo, ":")(1) Then
                commandTextSb.AppendLine(" INSERT INTO ")
                commandTextSb.AppendLine(" m_kameiten_ks_siyou_settei_renkei ")
                commandTextSb.AppendLine(" WITH(UPDLOCK) ")
                commandTextSb.AppendLine("  (kameiten_cd,")
                commandTextSb.AppendLine("  ks_siyou_no, ")
                commandTextSb.AppendLine("  renkei_siji_cd, ")
                commandTextSb.AppendLine("  sousin_jyky_cd, ")
                commandTextSb.AppendLine("  upd_login_user_id, ")
                commandTextSb.AppendLine("  upd_datetime) ")
                commandTextSb.AppendLine(" SELECT ")
                commandTextSb.AppendLine("  @kameiten_cd ,")
                commandTextSb.AppendLine("  @ks_siyou_no1, ")
                commandTextSb.AppendLine("  @renkei_siji_cd1, ")
                commandTextSb.AppendLine("  @sousin_jyky_cd, ")
                commandTextSb.AppendLine("  @upd_login_user_id, ")
                commandTextSb.AppendLine("  GETDATE() ")

            End If
            ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

        End If


        '終了処理
        commandTextSb = Nothing

        '戻り値をセット成功の場合
        UpdKisoSiyouSetteiRenkei = True
    End Function
    '''<summary>基礎仕様設定の更新処理</summary>
    Public Function UpdKisoSiyouSettei(ByVal strKameitenCd As String, ByVal strKahiKbn As String, ByVal strKsSiyouNo As String, ByVal strUerId As String) As Boolean
        '戻り値
        UpdKisoSiyouSettei = False

        Dim commandTextSb As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        commandTextSb.AppendLine(" UPDATE ")
        commandTextSb.AppendLine(" m_kameiten_ks_siyou_settei ")
        commandTextSb.AppendLine(" WITH(UPDLOCK) ")
        commandTextSb.AppendLine(" SET ")
        'commandTextSb.AppendLine(" kahi_kbn=@kahi_kbn, ")
        commandTextSb.AppendLine(" ks_siyou_no=@ks_siyou_no, ")
        commandTextSb.AppendLine(" upd_login_user_id=@upd_login_user_id, ")
        commandTextSb.AppendLine(" upd_datetime=GETDATE() ")
        commandTextSb.AppendLine("WHERE kameiten_cd = @kameiten_cd  ")
        commandTextSb.AppendLine(" AND  ks_siyou_no = @ks_siyou_no1  ")

        'パラメータの設定

        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))
        'paramList.Add(MakeParam("@kahi_kbn", SqlDbType.Int, 4, strKahiKbn))
        paramList.Add(MakeParam("@ks_siyou_no", SqlDbType.Int, 4, Split(strKsSiyouNo, ":")(0)))
        paramList.Add(MakeParam("@ks_siyou_no1", SqlDbType.Int, 4, Split(strKsSiyouNo, ":")(1)))
        paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, strUerId))
        '更新されたデータセットを DB へ書き込み
        ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

        '終了処理
        commandTextSb = Nothing

        '戻り値をセット成功の場合
        UpdKisoSiyouSettei = True
    End Function
    '''<summary>基礎仕様設定の削除処理</summary>
    Public Function DelKisoSiyouSettei(ByVal strKameitenCd As String, ByVal strKsSiyouNo As String, ByVal strKahiKbn As String, ByVal intRow As Integer) As Boolean
        '戻り値
        DelKisoSiyouSettei = False

        Dim commandTextSb As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        commandTextSb.AppendLine(" DELETE ")
        commandTextSb.AppendLine(" FROM  m_kameiten_ks_siyou_settei  ")
        commandTextSb.AppendLine(" WITH(UPDLOCK) ")
        commandTextSb.AppendLine(" WHERE kameiten_cd = @kameiten_cd  ")
        commandTextSb.AppendLine(" AND  ks_siyou_no = @ks_siyou_no  ")
        commandTextSb.AppendLine(" UPDATE ")
        commandTextSb.AppendLine(" m_kameiten_ks_siyou_settei  ")
        commandTextSb.AppendLine(" WITH(UPDLOCK) ")
        commandTextSb.AppendLine(" SET nyuuryoku_no=nyuuryoku_no-1  ")
        commandTextSb.AppendLine(" WHERE kameiten_cd = @kameiten_cd  ")
        commandTextSb.AppendLine(" AND kahi_kbn = @kahi_kbn  ")
        commandTextSb.AppendLine(" AND nyuuryoku_no > @nyuuryoku_no  ")
        'パラメータの設定
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))
        paramList.Add(MakeParam("@ks_siyou_no", SqlDbType.Int, 4, strKsSiyouNo))
        paramList.Add(MakeParam("@kahi_kbn", SqlDbType.Int, 4, strKahiKbn))
        paramList.Add(MakeParam("@nyuuryoku_no", SqlDbType.Int, 4, intRow))
        '更新されたデータセットを DB へ書き込み
        ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

        '終了処理
        commandTextSb = Nothing

        '戻り値をセット成功の場合
        DelKisoSiyouSettei = True

    End Function

    ''' <summary>
    ''' 「取消」情報を取得する
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <returns>「取消」データ</returns>
    ''' <hidtory>2012/03/28 車龍 405721案件の対応 追加</hidtory>
    Public Function SelTorikesi(ByVal strKameitenCd As String) As Data.DataTable

        Dim dsReturn As New DataSet

        Dim commandTextSb As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	MK.torikesi ")
            .AppendLine("	,ISNULL(MKM.meisyou,'') AS meisyou ")
            .AppendLine("FROM ")
            .AppendLine("	m_kameiten AS MK WITH(READCOMMITTED) ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_kakutyou_meisyou AS MKM WITH(READCOMMITTED) ")
            .AppendLine("	ON ")
            .AppendLine("		MKM.meisyou_syubetu = 56 ")
            .AppendLine("		AND ")
            .AppendLine("		MK.torikesi = MKM.code ")
            .AppendLine("WHERE ")
            .AppendLine("	MK.kameiten_cd = @kameiten_cd ")
        End With
        
        'パラメータの設定
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtTorikesi", paramList.ToArray)

        Return dsReturn.Tables("dtTorikesi")

    End Function

    ''' <summary>
    ''' 「工事売上種別」情報を取得する
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <returns>「取消」データ</returns>
    ''' <hidtory>2012/05/15 車龍 405721案件の対応 追加</hidtory>
    Public Function SelKoujiUriageSyuubetu(ByVal strKameitenCd As String) As Data.DataTable

        Dim dsReturn As New DataSet

        Dim commandTextSb As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	ISNULL(MM.meisyou,'') AS meisyou ") '--名称 ")
            .AppendLine("FROM ")
            .AppendLine("	m_kameiten AS MK WITH(READCOMMITTED) ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_meisyou AS MM WITH(READCOMMITTED) ")
            .AppendLine("	ON ")
            .AppendLine("		MM.code = MK.koj_uri_syubetsu ") '--工事売上種別 ")
            .AppendLine("		AND ")
            .AppendLine("		MM.meisyou_syubetu = @meisyou_syubetu ") '--名称種別 ")
            .AppendLine("WHERE ")
            .AppendLine("	MK.kameiten_cd = @kameiten_cd ") '--加盟店コード ")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))
        paramList.Add(MakeParam("@meisyou_syubetu", SqlDbType.Int, 10, "55"))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtKoujiUriageSyuubetu", paramList.ToArray)

        Return dsReturn.Tables("dtKoujiUriageSyuubetu")

    End Function


    '========2015/02/04 王莎莎 407679の対応 追加↓======================
    ''' <summary>名称種別＝33の名称を取得する</summary>
    Public Function SelSyubetuInfo33() As Data.DataTable

        Dim dsReturn As New DataSet
        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine(" code, ")
        commandTextSb.AppendLine(" meisyou ")
        commandTextSb.AppendLine(" FROM  ")
        commandTextSb.AppendLine(" m_kakutyou_meisyou WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" WHERE meisyou_syubetu=@meisyou_syubetu ")
        commandTextSb.AppendLine(" ORDER BY hyouji_jyun ")
        'パラメータの設定
        paramList.Add(MakeParam("@meisyou_syubetu", SqlDbType.Char, 2, "33"))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, _
                    "dtMeisyou33", paramList.ToArray)

        Return dsReturn.Tables("dtMeisyou33")

    End Function

    ''' <summary>トラブル・クレーム情報を取得する</summary>
    Public Function SelTuujyouTyuuiJikouInfoTORA(ByVal strKameitenCd As String, ByVal strUerId As String) As TyuiJyouhouDataSet.TyuuiJikouTableDataTable
        ' DataSetインスタンスの生成()
        Dim dsTyuiJyouhouDataSet As New TyuiJyouhouDataSet
        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        commandTextSb.AppendLine("  SELECT  ")
        commandTextSb.AppendLine("  CONVERT(varchar(4), m_kameiten_tyuuijikou.tyuuijikou_syubetu)+':'+CONVERT(varchar(4), m_kameiten_tyuuijikou.nyuuryoku_no) AS tyuuijikou_syubetu,  ")
        'commandTextSb.AppendLine("  m_meisyou.meisyou AS meisyou,  ")
        commandTextSb.AppendLine("  m_kameiten_tyuuijikou.nyuuryoku_date AS nyuuryoku_date,  ")
        commandTextSb.AppendLine("  m_kameiten_tyuuijikou.uketukesya_mei AS uketukesya_mei,  ")
        commandTextSb.AppendLine("  m_kameiten_tyuuijikou.naiyou AS naiyou,  ")
        commandTextSb.AppendLine(" m_kameiten_tyuuijikou.upd_datetime AS upd_datetime ")
        commandTextSb.AppendLine(" FROM  ")
        commandTextSb.AppendLine(" (SELECT tyuuijikou_syubetu , ")
        commandTextSb.AppendLine("  nyuuryoku_no,  ")
        commandTextSb.AppendLine("  nyuuryoku_date,  ")
        commandTextSb.AppendLine("  uketukesya_mei,  ")
        commandTextSb.AppendLine("  upd_datetime,  ")
        commandTextSb.AppendLine("  naiyou  ")
        commandTextSb.AppendLine(" FROM m_kameiten_tyuuijikou  WITH (READCOMMITTED)  ")
        commandTextSb.AppendLine("  WHERE kameiten_cd=@kameiten_cd ) AS m_kameiten_tyuuijikou ")
        commandTextSb.AppendLine("  LEFT JOIN   ")
        commandTextSb.AppendLine("  (SELECT  ")
        commandTextSb.AppendLine("  code,  ")
        commandTextSb.AppendLine("  meisyou  ")
        commandTextSb.AppendLine("  FROM   ")
        commandTextSb.AppendLine("  m_meisyou  WITH (READCOMMITTED)  ")
        commandTextSb.AppendLine("  WHERE  ")
        commandTextSb.AppendLine("  meisyou_syubetu=@meisyou_syubetu ")
        commandTextSb.AppendLine("  ) AS m_meisyou  ")
        commandTextSb.AppendLine("  ON  m_kameiten_tyuuijikou.tyuuijikou_syubetu=m_meisyou.code  ")
        commandTextSb.AppendLine(" WHERE  ")
        commandTextSb.AppendLine(" m_kameiten_tyuuijikou.tyuuijikou_syubetu NOT IN ")
        commandTextSb.AppendLine(" (SELECT ISNULL(m_tyuuijikou_yuusen.tyuuijikou_syubetu,'') ")
        commandTextSb.AppendLine("  FROM m_jiban_ninsyou WITH (READCOMMITTED)  ")
        commandTextSb.AppendLine("  LEFT JOIN m_tyuuijikou_yuusen  WITH (READCOMMITTED)  ")
        commandTextSb.AppendLine("  ON m_jiban_ninsyou.gyoumu_kbn=m_tyuuijikou_yuusen.gyoumu_kbn  ")
        commandTextSb.AppendLine("  WHERE  m_jiban_ninsyou.login_user_id=@login_user_id ")
        commandTextSb.AppendLine("  AND  m_jiban_ninsyou.torikesi=@torikesi ) ")
        commandTextSb.AppendLine(" AND m_kameiten_tyuuijikou.tyuuijikou_syubetu in ('90','91','97') ")
        commandTextSb.AppendLine("  ORDER BY m_kameiten_tyuuijikou.nyuuryoku_date DESC  ")
        'パラメータの設定
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, "0"))
        paramList.Add(MakeParam("@meisyou_syubetu", SqlDbType.Char, 2, "10"))
        paramList.Add(MakeParam("@login_user_id", SqlDbType.VarChar, 30, strUerId))
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsTyuiJyouhouDataSet, _
                    dsTyuiJyouhouDataSet.TyuuiJikouTable.TableName, paramList.ToArray)

        Return dsTyuiJyouhouDataSet.Tables(0)

    End Function

    '========2015/02/04 王莎莎 407679の対応 追加↑======================


    ''' <summary>商品コードを取得する</summary>
    ''' <returns>商品コードデータテーブル</returns>
    ''' <history>2016/05/04　chel1　新規作成</history>
    Public Function SelSyouhinCd() As Data.DataTable

        '戻りデータセット
        Dim dsReturn As New Data.DataSet

        'SQLコメント
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	syouhin_cd ")                       '商品コード
            .AppendLine("	,(syouhin_cd + '：' + syouhin_mei) AS syouhin_mei ") '商品名（商品コード：商品名）
            .AppendLine("FROM ")
            .AppendLine("	m_syouhin WITH(READCOMMITTED) ")                        '商品マスタ
            .AppendLine("WHERE ")
            .AppendLine("	torikesi = 0 ")                     '取消
            .AppendLine("	AND ")
            .AppendLine("	souko_cd = '100' ")                 '倉庫コード
        End With

        'SQLコメント実行、戻りデータセット実装
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtSyouhinCd")

        '戻りデータテーブル
        Return dsReturn.Tables(0)

    End Function

    ''' <summary>調査方法を取得する</summary>
    ''' <returns>調査方法データテーブル</returns>
    ''' <history>2016/05/04　chel1　新規作成</history>
    Public Function SelTyousaHouhou() As Data.DataTable

        '戻りデータセット
        Dim dsReturn As New Data.DataSet

        'SQLコメント
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT  ")
            .AppendLine("	tys_houhou_no ")                                                            '調査方法NO
            .AppendLine("	,LTRIM(STR(tys_houhou_no) + '：' + tys_houhou_mei) AS tys_houhou_mei ")     '調査方法名称
            .AppendLine("FROM ")
            .AppendLine("	m_tyousahouhou WITH(READCOMMITTED) ")                                       '調査方法マスタ
        End With

        'SQLコメント実行、戻りデータセット実装
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtTyousahouhou")

        '戻りデータテーブル
        Return dsReturn.Tables(0)

    End Function


    '''<summary>基本商品の更新処理</summary>
    Public Function UpdKihonSyouhin(ByVal strKameitenCd As String, ByVal strKihonSyouhinCd As String, ByVal strKihonSyouhinTyuuibun As String) As Boolean

        Dim updCnt As Integer = 0

        Dim commandTextSb As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With commandTextSb
            .AppendLine("UPDATE ")
            .AppendLine("	m_kameiten WITH(UPDLOCK) ")
            .AppendLine("SET ")
            .AppendLine("	kihon_syouhin_cd = @kihon_syouhin_cd ")
            .AppendLine("	,kihon_syouhin_tyuuibun = @kihon_syouhin_tyuuibun ")
            .AppendLine("WHERE ")
            .AppendLine("	kameiten_cd = @kameiten_cd ")
        End With

        paramList.Add(MakeParam("@kihon_syouhin_cd", SqlDbType.VarChar, 8, IIf(strKihonSyouhinCd.Equals(String.Empty), DBNull.Value, strKihonSyouhinCd)))
        paramList.Add(MakeParam("@kihon_syouhin_tyuuibun", SqlDbType.VarChar, 80, IIf(strKihonSyouhinTyuuibun.Equals(String.Empty), DBNull.Value, strKihonSyouhinTyuuibun)))
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))

        updCnt = ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

        If updCnt = 0 Then
            Return False
        Else
            Return True
        End If

    End Function

    '''<summary>基本調査方法の更新処理</summary>
    Public Function UpdKihonTyousaHouhou(ByVal strKameitenCd As String, ByVal strKihonTyousaHouhouNo As String, ByVal strKihonTyousaHouhouTyuuibun As String) As Boolean

        Dim updCnt As Integer = 0

        Dim commandTextSb As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With commandTextSb
            .AppendLine("UPDATE ")
            .AppendLine("	m_kameiten WITH(UPDLOCK) ")
            .AppendLine("SET ")
            .AppendLine("	kihon_tyousahouhou_no = @kihon_tyousahouhou_no ")
            .AppendLine("	,kihon_tyousahouhou_tyuuibun = @kihon_tyousahouhou_tyuuibun ")
            .AppendLine("WHERE ")
            .AppendLine("	kameiten_cd = @kameiten_cd ")
        End With

        paramList.Add(MakeParam("@kihon_tyousahouhou_no", SqlDbType.Int, 10, IIf(strKihonTyousaHouhouNo.Equals(String.Empty), DBNull.Value, strKihonTyousaHouhouNo)))
        paramList.Add(MakeParam("@kihon_tyousahouhou_tyuuibun", SqlDbType.VarChar, 80, IIf(strKihonTyousaHouhouTyuuibun.Equals(String.Empty), DBNull.Value, strKihonTyousaHouhouTyuuibun)))
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))

        updCnt = ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

        If updCnt = 0 Then
            Return False
        Else
            Return True
        End If

    End Function


    ''' <summary>
    ''' 基本商品と調査方法を取得する
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <history>2016/05/04　chel1　新規作成</history>
    Public Function SelKihouSyouhinAndTyousaHouhou(ByVal strKameitenCd As String) As Data.DataTable

        Dim dsReturn As New DataSet

        Dim commandTextSb As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	kihon_syouhin_cd ")
            .AppendLine("	,kihon_tyousahouhou_no ")
            .AppendLine("	,kihon_syouhin_tyuuibun ")
            .AppendLine("	,kihon_tyousahouhou_tyuuibun ")
            .AppendLine("FROM ")
            .AppendLine("	m_kameiten WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	kameiten_cd = @kameiten_cd ") '--加盟店コード ")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtKihouSyouhinAndTyousaHouhou", paramList.ToArray)

        Return dsReturn.Tables("dtKihouSyouhinAndTyousaHouhou")

    End Function


End Class

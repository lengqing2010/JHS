Imports System.Data.SqlClient
Imports System.Text
Imports Itis.ApplicationBlocks.Data.SQLHelper
Public Class TyuiJyouhouInquiryDataAccess
    Private connStr As String = ConnectionManager.Instance.ConnectionString

    '''<summary>������Дr������</summary>
    Public Function SelTyousaKaisyaHaita(ByVal dtTyousaKaisyaUPDData As TyuiJyouhouDataSet.TyousaKaisyaUPDTableDataTable, Optional ByVal blnChk As Boolean = False) As DataTable
        Dim commandTextSb As New System.Text.StringBuilder

        Dim dsReturn As New DataSet
        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)
        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine(" upd_login_user_id AS login_user_id, ")
        commandTextSb.AppendLine(" upd_datetime AS upd_datetime ")
        commandTextSb.AppendLine(" FROM  m_kameiten_tyousakaisya WITH (READCOMMITTED) ")
        commandTextSb.AppendLine("WHERE m_kameiten_tyousakaisya.kameiten_cd = @kameiten_cd  ")
        commandTextSb.AppendLine(" AND  m_kameiten_tyousakaisya.kaisya_kbn = @kaisya_kbn  ")

        '�p�����[�^�̐ݒ�
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
        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, _
                    "dsReturn", paramList.ToArray)

        Return dsReturn.Tables(0)

    End Function
    '''<summary>������ИA�g�̍X�V����</summary>
    Public Function UpdTyousaKaisyaRenkei(ByVal dtTyousaKaisyaUPDData As TyuiJyouhouDataSet.TyousaKaisyaUPDTableDataTable, ByVal strCd As String, Optional ByVal intRow As String = "") As Boolean
        '�߂�l
        UpdTyousaKaisyaRenkei = False

        Dim commandTextSb As New System.Text.StringBuilder

        '�p�����[�^�i�[
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
            '�p�����[�^�̐ݒ�
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

                '�X�V���ꂽ�f�[�^�Z�b�g�� DB �֏�������
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

                '�X�V���ꂽ�f�[�^�Z�b�g�� DB �֏�������
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
                    '�p�����[�^�̐ݒ�
                    With dtTyousaKaisyaUPDData.Rows(0)
                        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, .Item("kameiten_cd")))
                        paramList.Add(MakeParam("@kaisya_kbn", SqlDbType.Int, 4, .Item("kaisya_kbn")))
                        paramList.Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 5, Split(.Item("tys_kaisya_cd"), ":")(1)))
                        paramList.Add(MakeParam("@jigyousyo_cd", SqlDbType.VarChar, 2, Split(.Item("jigyousyo_cd"), ":")(1)))
                        paramList.Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 4, "9"))
                        paramList.Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 4, "0"))
                        paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, .Item("kousinsya")))


                    End With
                    '�X�V���ꂽ�f�[�^�Z�b�g�� DB �֏�������
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
                'SQL��
        End With
        '�I������
        commandTextSb = Nothing

        '�߂�l���Z�b�g�����̏ꍇ
        UpdTyousaKaisyaRenkei = True
    End Function
    ''' <summary>������Ђ̍X�V����</summary>
    Public Function UpdTyousaKaisya(ByVal dtTyousaKaisyaUPDData As TyuiJyouhouDataSet.TyousaKaisyaUPDTableDataTable) As Boolean
        '�߂�l
        UpdTyousaKaisya = False

        Dim commandTextSb As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
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
        '�p�����[�^�̐ݒ�
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
        '�X�V���ꂽ�f�[�^�Z�b�g�� DB �֏�������
        ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

        '�I������
        commandTextSb = Nothing

        '�߂�l���Z�b�g�����̏ꍇ
        UpdTyousaKaisya = True
    End Function
    '''<summary>������Ѓ��R�[�h�s�����擾����</summary>
    Public Function SelTyousaKaisyaCount(ByVal dtTyousaKaisyaUPDData As TyuiJyouhouDataSet.TyousaKaisyaUPDTableDataTable) As Integer

        Dim commandTextSb As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        Dim intReturn As Integer = 0
        Dim dsReturn As New DataSet
        'SQL��

        commandTextSb.Remove(0, commandTextSb.Length)

        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine("  COUNT(kameiten_cd)")
        commandTextSb.AppendLine("  FROM  m_kameiten_tyousakaisya_renkei ")
        commandTextSb.AppendLine("  WHERE kameiten_cd = @kameiten_cd  ")
        commandTextSb.AppendLine("  AND kaisya_kbn=@kaisya_kbn ")
        commandTextSb.AppendLine("  AND tys_kaisya_cd=@tys_kaisya_cd ")
        commandTextSb.AppendLine("  AND jigyousyo_cd=@jigyousyo_cd ")

        '�p�����[�^�̐ݒ�
        With dtTyousaKaisyaUPDData.Rows(0)
            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, .Item("kameiten_cd")))
            paramList.Add(MakeParam("@kaisya_kbn", SqlDbType.Int, 4, .Item("kaisya_kbn")))
            paramList.Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 5, Split(.Item("tys_kaisya_cd"), ":")(0)))
            paramList.Add(MakeParam("@jigyousyo_cd", SqlDbType.VarChar, 2, Split(.Item("jigyousyo_cd"), ":")(0)))
        End With
        '�X�V���ꂽ�f�[�^�Z�b�g�� DB �֏�������
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, _
                            "dsReturn", paramList.ToArray)

        '�I������
        commandTextSb = Nothing

        '�߂�l���Z�b�g�����̏ꍇ
        Return dsReturn.Tables(0).Rows(0).Item(0)
    End Function
    '''<summary>������ИA�g�̐V�K����</summary>
    Public Function InsTyousaKaisyaRenkei(ByVal dtTyousaKaisyaUPDData As TyuiJyouhouDataSet.TyousaKaisyaUPDTableDataTable) As Boolean
        '�߂�l
        InsTyousaKaisyaRenkei = False

        Dim commandTextSb As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        Dim intReturn As Integer = 0
        Dim dsReturn As New DataSet
        'SQL��
      
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

        '�p�����[�^�̐ݒ�
        With dtTyousaKaisyaUPDData.Rows(0)
            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, .Item("kameiten_cd")))
            paramList.Add(MakeParam("@kaisya_kbn", SqlDbType.Int, 4, .Item("kaisya_kbn")))
            paramList.Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 5, Split(.Item("tys_kaisya_cd"), ":")(0)))
            paramList.Add(MakeParam("@jigyousyo_cd", SqlDbType.VarChar, 2, Split(.Item("jigyousyo_cd"), ":")(0)))
            paramList.Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 4, "1"))
            paramList.Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 4, "0"))
            paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, .Item("kousinsya")))


        End With
        '�X�V���ꂽ�f�[�^�Z�b�g�� DB �֏�������
        ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

        '�I������
        commandTextSb = Nothing

        '�߂�l���Z�b�g�����̏ꍇ
        InsTyousaKaisyaRenkei = True
    End Function
    '''<summary>������ИA�g�̐V�K����</summary>
    Public Function InsTyousaKaisyaRenkei2(ByVal dtTyousaKaisyaUPDData As TyuiJyouhouDataSet.TyousaKaisyaUPDTableDataTable) As Boolean
        '�߂�l
        InsTyousaKaisyaRenkei2 = False

        Dim commandTextSb As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        Dim intReturn As Integer = 0
        Dim dsReturn As New DataSet
        'SQL��

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


        '�p�����[�^�̐ݒ�
        With dtTyousaKaisyaUPDData.Rows(0)
            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, .Item("kameiten_cd")))
            paramList.Add(MakeParam("@kaisya_kbn", SqlDbType.Int, 4, .Item("kaisya_kbn")))
            paramList.Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 5, Split(.Item("tys_kaisya_cd"), ":")(0)))
            paramList.Add(MakeParam("@jigyousyo_cd", SqlDbType.VarChar, 2, Split(.Item("jigyousyo_cd"), ":")(0)))
            paramList.Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 4, "2"))
            paramList.Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 4, "0"))
            paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, .Item("kousinsya")))


        End With
        '�X�V���ꂽ�f�[�^�Z�b�g�� DB �֏�������
        ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

        '�I������
        commandTextSb = Nothing

        '�߂�l���Z�b�g�����̏ꍇ
        InsTyousaKaisyaRenkei2 = True
    End Function
    '''<summary>������Ђ̐V�K����</summary>
    Public Function InsTyousaKaisya(ByVal dtTyousaKaisyaUPDData As TyuiJyouhouDataSet.TyousaKaisyaUPDTableDataTable) As Boolean
        '�߂�l
        InsTyousaKaisya = False

        Dim commandTextSb As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        Dim intReturn As Integer = 0
        Dim dsReturn As New DataSet
        'SQL��
        commandTextSb.AppendLine(" SELECT COUNT(kameiten_cd) ")
        commandTextSb.AppendLine("  FROM  m_kameiten_tyousakaisya WITH (READCOMMITTED)  ")
        commandTextSb.AppendLine("  WHERE kameiten_cd = @kameiten_cd  ")
        commandTextSb.AppendLine("  AND kaisya_kbn=@kaisya_kbn ")
        commandTextSb.AppendLine("  AND kahi_kbn=@kahi_kbn ")
        '�p�����[�^�̐ݒ�
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

        '�p�����[�^�̐ݒ�
        With dtTyousaKaisyaUPDData.Rows(0)
            paramList.Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 5, Split(.Item("tys_kaisya_cd"), ":")(0)))
            paramList.Add(MakeParam("@jigyousyo_cd", SqlDbType.VarChar, 2, Split(.Item("jigyousyo_cd"), ":")(0)))
            paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, .Item("kousinsya")))
        End With
        '�X�V���ꂽ�f�[�^�Z�b�g�� DB �֏�������
        ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

        '�I������
        commandTextSb = Nothing

        '�߂�l���Z�b�g�����̏ꍇ
        InsTyousaKaisya = True
    End Function
    ''' <summary>������Ђ̍폜����</summary>
    Public Function DelTyousaKaisya(ByVal dtTyousaKaisyaUPDData As TyuiJyouhouDataSet.TyousaKaisyaUPDTableDataTable, ByVal intRow As Integer) As Boolean
        '�߂�l
        DelTyousaKaisya = False

        Dim commandTextSb As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
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

        '�p�����[�^�̐ݒ�
        With dtTyousaKaisyaUPDData.Rows(0)
            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, .Item("kameiten_cd")))
            paramList.Add(MakeParam("@kaisya_kbn", SqlDbType.Int, 4, .Item("kaisya_kbn")))
            paramList.Add(MakeParam("@kahi_kbn", SqlDbType.Int, 4, .Item("kahi_kbn")))
            paramList.Add(MakeParam("@nyuuryoku_no", SqlDbType.Int, 4, intRow))
            paramList.Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 5, Split(.Item("tys_kaisya_cd"), ":")(1)))
            paramList.Add(MakeParam("@jigyousyo_cd", SqlDbType.VarChar, 2, Split(.Item("jigyousyo_cd"), ":")(1)))
        End With

        '�X�V���ꂽ�f�[�^�Z�b�g�� DB �֏�������
        ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

        '�I������
        commandTextSb = Nothing

        '�߂�l���Z�b�g�����̏ꍇ
        DelTyousaKaisya = True

    End Function
    '''<summary>���ӎ����r������</summary>
    Public Function SelTyuuiJikouHaita(ByVal dtTyuiJyouhouUPDData As TyuiJyouhouDataSet.TyuuiJikouUPDTableDataTable) As DataTable
        Dim commandTextSb As New System.Text.StringBuilder

        Dim dsReturn As New DataSet
        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)
        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine(" upd_login_user_id AS login_user_id, ")
        commandTextSb.AppendLine(" upd_datetime AS upd_datetime ")
        commandTextSb.AppendLine(" FROM  m_kameiten_tyuuijikou WITH (READCOMMITTED) ")
        commandTextSb.AppendLine("WHERE kameiten_cd = @kameiten_cd  ")
        commandTextSb.AppendLine(" AND  nyuuryoku_no = @nyuuryoku_no  ")

        '�p�����[�^�̐ݒ�
        With dtTyuiJyouhouUPDData.Rows(0)
            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, .Item("kameiten_cd")))
            paramList.Add(MakeParam("@nyuuryoku_no", SqlDbType.Int, 4, .Item("nyuuryoku_no")))
        End With
        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, _
                    "dsReturn", paramList.ToArray)

        Return dsReturn.Tables(0)

    End Function
    ''' <summary>���ӎ����̐V�K����</summary>
    Public Function UpdTyuuiJikouRenkei(ByVal dtTyuiJyouhouUPDData As TyuiJyouhouDataSet.TyuuiJikouUPDTableDataTable, ByVal strCd As String) As Boolean
        '�߂�l
        UpdTyuuiJikouRenkei = False

        Dim commandTextSb As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        commandTextSb.AppendLine(" UPDATE ")
        commandTextSb.AppendLine(" m_kameiten_tyuuijikou_renkei ")
        commandTextSb.AppendLine(" WITH(UPDLOCK) ")
        commandTextSb.AppendLine(" SET ")
        commandTextSb.AppendLine(" renkei_siji_cd=@renkei_siji_cd, ")
        commandTextSb.AppendLine(" sousin_jyky_cd=@sousin_jyky_cd, ")
        commandTextSb.AppendLine(" upd_login_user_id=@kousinsya, ")
        commandTextSb.AppendLine(" upd_datetime=GETDATE() ")
        commandTextSb.AppendLine("WHERE kameiten_cd = @kameiten_cd  ")
        '�p�����[�^�̐ݒ�
        With dtTyuiJyouhouUPDData.Rows(0)
            If strCd = "9" Or strCd = "2" Then
                commandTextSb.AppendLine(" AND  nyuuryoku_no = @nyuuryoku_no  ")
                paramList.Add(MakeParam("@nyuuryoku_no", SqlDbType.Int, 4, .Item("nyuuryoku_no")))
                If strCd = "9" Then
                    Dim sql As New StringBuilder
                    'sql.AppendLine("DECLARE @nyuuryokuno int")
                    'sql.AppendLine("set @nyuuryokuno = (select isnull( (select max(nyuuryoku_no) from m_kameiten_tyuuijikou_renkei Where kameiten_cd = @kameiten_cd), 0))")

                    '�V�K�ǉ�
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

                '�p�����[�^�̐ݒ�
                paramList.Clear()
                With dtTyuiJyouhouUPDData.Rows(0)
                    paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, .Item("kameiten_cd")))
                    paramList.Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 4, "2"))
                    paramList.Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 4, "0"))
                    paramList.Add(MakeParam("@nyuuryoku_no", SqlDbType.Int, 8, .Item("nyuuryoku_no")))
                    paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, .Item("kousinsya")))

                End With
                '�X�V���ꂽ�f�[�^�Z�b�g�� DB �֏�������
                ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)
            End If
        ElseIf strCd = "1" Then
            ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)
        End If
        

        '�I������
        commandTextSb = Nothing

        '�߂�l���Z�b�g�����̏ꍇ
        UpdTyuuiJikouRenkei = True
    End Function
    ''' <summary>���ӎ����̍X�V����</summary>
    Public Function UpdTyuuiJikou(ByVal dtTyuiJyouhouUPDData As TyuiJyouhouDataSet.TyuuiJikouUPDTableDataTable) As Boolean
        '�߂�l
        UpdTyuuiJikou = False

        Dim commandTextSb As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
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

        '�p�����[�^�̐ݒ�
        With dtTyuiJyouhouUPDData.Rows(0)
            paramList.Add(MakeParam("@tyuuijikou_syubetu", SqlDbType.Int, 4, .Item("tyuuijikou_syubetu")))
            paramList.Add(MakeParam("@nyuuryoku_date", SqlDbType.DateTime, 8, .Item("nyuuryoku_date")))
            paramList.Add(MakeParam("@uketukesya_mei", SqlDbType.VarChar, 20, .Item("uketukesya_mei")))
            paramList.Add(MakeParam("@naiyou", SqlDbType.VarChar, 80, .Item("naiyou")))
            paramList.Add(MakeParam("@kousinsya", SqlDbType.VarChar, 30, .Item("kousinsya")))
            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, .Item("kameiten_cd")))
            paramList.Add(MakeParam("@nyuuryoku_no", SqlDbType.Int, 4, .Item("nyuuryoku_no")))
        End With
        '�X�V���ꂽ�f�[�^�Z�b�g�� DB �֏�������
        ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

        '�I������
        commandTextSb = Nothing

        '�߂�l���Z�b�g�����̏ꍇ
        UpdTyuuiJikou = True
    End Function

    ''' <summary>���ӎ����A�g�̍X�V����</summary>
    Public Function InsTyuuiJikouRenkei(ByVal dtTyuiJyouhouUPDData As TyuiJyouhouDataSet.TyuuiJikouUPDTableDataTable) As Boolean
        '�߂�l
        InsTyuuiJikouRenkei = False

        Dim commandTextSb As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
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
        '�p�����[�^�̐ݒ�
        With dtTyuiJyouhouUPDData.Rows(0)
            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, .Item("kameiten_cd")))
            paramList.Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 4, "1"))
            paramList.Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 4, "0"))

            paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, .Item("kousinsya")))

        End With
        '�X�V���ꂽ�f�[�^�Z�b�g�� DB �֏�������
        ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

        '�I������
        commandTextSb = Nothing

        '�߂�l���Z�b�g�����̏ꍇ
        InsTyuuiJikouRenkei = True
    End Function
    ''' <summary>���ӎ����A�g�̍X�V����</summary>
    Public Function InsTyuuiJikouRenkei2(ByVal dtTyuiJyouhouUPDData As TyuiJyouhouDataSet.TyuuiJikouUPDTableDataTable, ByVal strNyuuryokuNo As String) As Boolean
        '�߂�l
        InsTyuuiJikouRenkei2 = False

        Dim commandTextSb As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
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
        '�p�����[�^�̐ݒ�
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

        '�X�V���ꂽ�f�[�^�Z�b�g�� DB �֏�������
        ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

        '�I������
        commandTextSb = Nothing

        '�߂�l���Z�b�g�����̏ꍇ
        InsTyuuiJikouRenkei2 = True
    End Function
    '''<summary>���ӎ������R�[�h�s�����擾����</summary>
    Public Function SelTyuuiJikouCount(ByVal dtTyuiJyouhouUPDData As TyuiJyouhouDataSet.TyuuiJikouUPDTableDataTable) As Integer
        Dim commandTextSb As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        Dim intReturn As Integer = 0
        Dim dsReturn As New DataSet
        'SQL��

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
        '�p�����[�^�̐ݒ�
        With dtTyuiJyouhouUPDData.Rows(0)
            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, .Item("kameiten_cd")))
        End With
        '�X�V���ꂽ�f�[�^�Z�b�g�� DB �֏�������
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, _
                            "dsReturn", paramList.ToArray)

        '�I������
        commandTextSb = Nothing

        '�߂�l���Z�b�g�����̏ꍇ
        Return dsReturn.Tables(0).Rows(0).Item(0)
    End Function
    ''' <summary>���ӎ����̐V�K����</summary>
    Public Function InsTyuuiJikou(ByVal dtTyuiJyouhouUPDData As TyuiJyouhouDataSet.TyuuiJikouUPDTableDataTable) As Boolean
        '�߂�l
        InsTyuuiJikou = False

        Dim commandTextSb As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
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
        '�p�����[�^�̐ݒ�
        With dtTyuiJyouhouUPDData.Rows(0)
            paramList.Add(MakeParam("@tyuuijikou_syubetu", SqlDbType.Int, 4, .Item("tyuuijikou_syubetu")))
            paramList.Add(MakeParam("@nyuuryoku_date", SqlDbType.DateTime, 8, .Item("nyuuryoku_date")))
            paramList.Add(MakeParam("@uketukesya_mei", SqlDbType.VarChar, 20, .Item("uketukesya_mei")))
            paramList.Add(MakeParam("@naiyou", SqlDbType.VarChar, 80, .Item("naiyou")))
            paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, .Item("kousinsya")))
            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, .Item("kameiten_cd")))
        End With
        '�X�V���ꂽ�f�[�^�Z�b�g�� DB �֏�������
        ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

        '�I������
        commandTextSb = Nothing

        '�߂�l���Z�b�g�����̏ꍇ
        InsTyuuiJikou = True
    End Function
    ''' <summary>���ӎ����̍폜����</summary>
    Public Function DelTyuuiJikou(ByVal strKameitenCd As String, ByVal strNyuuryokuNo As String, ByVal strUserId As String) As Boolean
        '�߂�l
        DelTyuuiJikou = False

        Dim commandTextSb As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
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




        '�I������
        commandTextSb = Nothing

        '�߂�l���Z�b�g�����̏ꍇ
        DelTyuuiJikou = True

    End Function
    ''' <summary>�D�撍�ӎ������擾����</summary>
    Public Function SelYuusenTyuuiJikouInfo(ByVal strKameitenCd As String, ByVal strUerId As String) As TyuiJyouhouDataSet.TyuuiJikouTableDataTable
        ' DataSet�C���X�^���X�̐���()
        Dim dsTyuiJyouhouDataSet As New TyuiJyouhouDataSet
        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
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
        '========2015/02/04 ���� 407679�̑Ή� �ǉ���======================
        commandTextSb.AppendLine(" AND m_kameiten_tyuuijikou.tyuuijikou_syubetu not in ('90','91','97') ")
        '========2015/02/04 ���� 407679�̑Ή� �ǉ���======================
        commandTextSb.AppendLine(" ORDER BY m_tyuuijikou_yuusen.hyouji_jyun ")
        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, "0"))
        paramList.Add(MakeParam("@meisyou_syubetu", SqlDbType.Char, 2, "10"))
        paramList.Add(MakeParam("@login_user_id", SqlDbType.VarChar, 30, strUerId))
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsTyuiJyouhouDataSet, _
                    dsTyuiJyouhouDataSet.TyuuiJikouTable.TableName, paramList.ToArray)

        Return dsTyuiJyouhouDataSet.TyuuiJikouTable

    End Function
    ''' <summary>�ʏ풍�ӎ������擾����</summary>
    Public Function SelTuujyouTyuuiJikouInfo(ByVal strKameitenCd As String, ByVal strUerId As String) As TyuiJyouhouDataSet.TyuuiJikouTableDataTable
        ' DataSet�C���X�^���X�̐���()
        Dim dsTyuiJyouhouDataSet As New TyuiJyouhouDataSet
        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
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
        '========2015/02/04 ���� 407679�̑Ή� �ǉ���======================
        commandTextSb.AppendLine(" AND m_kameiten_tyuuijikou.tyuuijikou_syubetu not in ('90','91','97') ")
        '========2015/02/04 ���� 407679�̑Ή� �ǉ���======================
        commandTextSb.AppendLine("  ORDER BY nyuuryoku_no  ")
        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, "0"))
        paramList.Add(MakeParam("@meisyou_syubetu", SqlDbType.Char, 2, "10"))
        paramList.Add(MakeParam("@login_user_id", SqlDbType.VarChar, 30, strUerId))
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsTyuiJyouhouDataSet, _
                    dsTyuiJyouhouDataSet.TyuuiJikouTable.TableName, paramList.ToArray)

        Return dsTyuiJyouhouDataSet.TyuuiJikouTable

    End Function


    ''' <summary>��ʂ��擾����</summary>
    Public Function SelSyubetuInfo(ByVal flg As String) As TyuiJyouhouDataSet.MeisyouTableDataTable
        ' DataSet�C���X�^���X�̐���()
        Dim dsTyuiJyouhouDataSet As New TyuiJyouhouDataSet
        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine(" code, ")
        commandTextSb.AppendLine(" meisyou ")
        commandTextSb.AppendLine(" FROM  ")
        commandTextSb.AppendLine(" m_meisyou WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" WHERE meisyou_syubetu=@meisyou_syubetu ")

        '========2015/02/04 ���� 407679�̑Ή� �ǉ���======================
        If flg = "TORA" Then
            commandTextSb.AppendLine(" AND code in ('90','91','97') ")
        Else
            commandTextSb.AppendLine(" AND code not in ('90','91','97') ")
        End If

        '========2015/02/04 ���� 407679�̑Ή� �ǉ���======================

        commandTextSb.AppendLine(" ORDER BY hyouji_jyun ")
        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@meisyou_syubetu", SqlDbType.Char, 2, "10"))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsTyuiJyouhouDataSet, _
                    dsTyuiJyouhouDataSet.MeisyouTable.TableName, paramList.ToArray)

        Return dsTyuiJyouhouDataSet.MeisyouTable

    End Function
    ''' <summary>������Џ����擾����</summary>

    Public Function SelKaisyaJyouhouInfo(ByVal strKameitenCd As String, ByVal strKbn As String) As TyuiJyouhouDataSet.KaisyaTableDataTable
        ' DataSet�C���X�^���X�̐���()
        Dim dsTyuiJyouhouDataSet As New TyuiJyouhouDataSet
        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
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
        '�p�����[�^�̐ݒ�
        'paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, "0"))
        paramList.Add(MakeParam("@kaisya_kbn", SqlDbType.Int, 4, Left(strKbn, 1)))
        paramList.Add(MakeParam("@kahi_kbn", SqlDbType.Int, 4, Right(strKbn, 1)))
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))
        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsTyuiJyouhouDataSet, _
                    dsTyuiJyouhouDataSet.KaisyaTable.TableName, paramList.ToArray)

        Return dsTyuiJyouhouDataSet.KaisyaTable

    End Function
    ''' <summary>��Ђ��擾����</summary>
    Public Function SelKaisyaInfo(Optional ByVal strKaisyaCd As String = "") As TyuiJyouhouDataSet.KaisyaTableDataTable
        ' DataSet�C���X�^���X�̐���()
        Dim dsTyuiJyouhouDataSet As New TyuiJyouhouDataSet
        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
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
        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, "0"))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsTyuiJyouhouDataSet, _
                    dsTyuiJyouhouDataSet.KaisyaTable.TableName, paramList.ToArray)

        Return dsTyuiJyouhouDataSet.KaisyaTable

    End Function

    ''' <summary>��b�d�l���擾����</summary>
    Public Function SelKisoSiyouInfo(Optional ByVal strKsno As String = "") As TyuiJyouhouDataSet.KisoSiyouTableDataTable
        ' DataSet�C���X�^���X�̐���()
        Dim dsTyuiJyouhouDataSet As New TyuiJyouhouDataSet
        'SQL���̐���
        Dim commandTextSb As New StringBuilder
        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)
        'SQL��
        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine(" ks_siyou_no AS ks_siyou_no ,")
        commandTextSb.AppendLine(" ks_siyou AS ks_siyou ")
        commandTextSb.AppendLine(" FROM  ")
        commandTextSb.AppendLine(" m_ks_siyou WITH (READCOMMITTED) ")
        If strKsno <> "" Then
            commandTextSb.AppendLine(" WHERE CONVERT(VARCHAR(11),ks_siyou_no)=@ks_siyou_no ")
            paramList.Add(MakeParam("@ks_siyou_no", SqlDbType.VarChar, 11, strKsno))
        End If

        '�p�����[�^�̐ݒ�

        commandTextSb.AppendLine(" ORDER BY ks_siyou_no ")


        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsTyuiJyouhouDataSet, _
                    dsTyuiJyouhouDataSet.KisoSiyouTable.TableName, paramList.ToArray)

        Return dsTyuiJyouhouDataSet.KisoSiyouTable

    End Function
    '''<summary>��b�d�l�r������</summary>
    Public Function SelKisoSiyouHaita(ByVal strKameitenCd As String, ByVal strKsSiyouNo As String) As DataTable
        Dim commandTextSb As New System.Text.StringBuilder

        Dim dsReturn As New DataSet
        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)
        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine(" upd_login_user_id AS login_user_id, ")
        commandTextSb.AppendLine(" upd_datetime AS upd_datetime ")
        commandTextSb.AppendLine(" FROM  m_kameiten_ks_siyou_settei WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" WHERE kameiten_cd = @kameiten_cd  ")
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))
        '�p�����[�^�̐ݒ�


        commandTextSb.AppendLine(" AND  ks_siyou_no = @ks_siyou_no  ")
        paramList.Add(MakeParam("@ks_siyou_no", SqlDbType.Int, 4, strKsSiyouNo))



        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, _
                    "dsReturn", paramList.ToArray)

        Return dsReturn.Tables(0)

    End Function

    '''<summary>��b�d�l�ݒ���擾����</summary>
    Public Function SelKisoSiyouSetteiInfo(ByVal strKameitenCd As String, ByVal strKahiKbn As String) As TyuiJyouhouDataSet.KisoSiyouTableDataTable
        ' DataSet�C���X�^���X�̐���()
        Dim dsTyuiJyouhouDataSet As New TyuiJyouhouDataSet
        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
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
        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))
        paramList.Add(MakeParam("@kahi_kbn", SqlDbType.Int, 4, strKahiKbn))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsTyuiJyouhouDataSet, _
                    dsTyuiJyouhouDataSet.KisoSiyouTable.TableName, paramList.ToArray)

        Return dsTyuiJyouhouDataSet.KisoSiyouTable

    End Function
    '''<summary>��b�d�l�ݒ背�R�[�h�s�����擾����</summary>
    Public Function SelKisoSiyouSetteiCount(ByVal strKameitenCd As String, ByVal strKsSiyouNo As String) As Integer

        Dim commandTextSb As New System.Text.StringBuilder
        Dim dtReturn As New DataSet
        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        commandTextSb.Remove(0, commandTextSb.Length)

        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine("  COUNT(kameiten_cd)")
        commandTextSb.AppendLine("  FROM  m_kameiten_ks_siyou_settei_renkei WITH (READCOMMITTED)  ")
        commandTextSb.AppendLine("  WHERE kameiten_cd = @kameiten_cd  ")
        commandTextSb.AppendLine("  AND ks_siyou_no=@ks_siyou_no ")

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@ks_siyou_no", SqlDbType.Int, 4, strKsSiyouNo))
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))


        '�X�V���ꂽ�f�[�^�Z�b�g�� DB �֏�������
        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dtReturn, _
                    "dtReturn", paramList.ToArray)


        '�I������
        commandTextSb = Nothing

        '�߂�l���Z�b�g�����̏ꍇ
        Return dtReturn.Tables(0).Rows(0).Item(0)
    End Function
    '''<summary>��b�d�l�ݒ�A�g�̐V�K����</summary>
    Public Function InsKisoSiyouSetteiRenkei(ByVal strKameitenCd As String, ByVal strKsSiyouNo As String, ByVal strKousinsya As String) As Boolean
        '�߂�l
        InsKisoSiyouSetteiRenkei = False

        Dim commandTextSb As New System.Text.StringBuilder

        '�p�����[�^�i�[
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

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 4, "1"))
        paramList.Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 4, "0"))
        paramList.Add(MakeParam("@ks_siyou_no", SqlDbType.Int, 4, strKsSiyouNo))
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))
        paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, strKousinsya))

        '�X�V���ꂽ�f�[�^�Z�b�g�� DB �֏�������
        ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

        '�I������
        commandTextSb = Nothing

        '�߂�l���Z�b�g�����̏ꍇ
        InsKisoSiyouSetteiRenkei = True
    End Function
    '''<summary>��b�d�l�ݒ�̐V�K����</summary>
    Public Function InsKisoSiyouSettei(ByVal strKameitenCd As String, ByVal strKahiKbn As String, ByVal strKsSiyouNo As String, ByVal strKousinsya As String) As Boolean
        '�߂�l
        InsKisoSiyouSettei = False

        Dim commandTextSb As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        Dim intReturn As Integer = 0
        Dim dsReturn As New DataSet
        'SQL��
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

        '�p�����[�^�̐ݒ�

        paramList.Add(MakeParam("@ks_siyou_no", SqlDbType.Int, 4, strKsSiyouNo))
        paramList.Add(MakeParam("@kousinsya", SqlDbType.VarChar, 30, strKousinsya))

        '�X�V���ꂽ�f�[�^�Z�b�g�� DB �֏�������
        ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

        '�I������
        commandTextSb = Nothing

        '�߂�l���Z�b�g�����̏ꍇ
        InsKisoSiyouSettei = True
    End Function
    '''<summary>��b�d�l�ݒ�̐V�K����</summary>
    Public Function InsKisoSiyouSettei2(ByVal strKameitenCd As String, ByVal strKousinsya As String) As Boolean
        '�߂�l
        InsKisoSiyouSettei2 = False

        Dim commandTextSb As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        Dim intReturn As Integer = 0
        Dim dsReturn As New DataSet
        'SQL��
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
        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 4, "2"))
        paramList.Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 4, "0"))

        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))
        paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, strKousinsya))


        '�X�V���ꂽ�f�[�^�Z�b�g�� DB �֏�������
        ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

        '�I������
        commandTextSb = Nothing

        '�߂�l���Z�b�g�����̏ꍇ
        InsKisoSiyouSettei2 = True
    End Function
    '''<summary>��b�d�l�ݒ�A�g�̍X�V����</summary>
    Public Function UpdKisoSiyouSetteiRenkei(ByVal strKameitenCd As String, ByVal strKsSiyouNo As String, ByVal strUerId As String, ByVal strCd As String, Optional ByVal intRow As String = "", Optional ByVal strKahiKbn As String = "") As Boolean
        '�߂�l
        UpdKisoSiyouSetteiRenkei = False

        Dim commandTextSb As New System.Text.StringBuilder

        '�p�����[�^�i�[
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
        'SQL��
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

        '�p�����[�^�̐ݒ�

        If strCd = "9" Or strCd = "1" Then
            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))
            paramList.Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 4, strCd))
            paramList.Add(MakeParam("@ks_siyou_no", SqlDbType.Int, 4, strKsSiyouNo))
            paramList.Add(MakeParam("@ks_siyou_no1", SqlDbType.Int, 4, strKsSiyouNo))
            paramList.Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 4, "0"))
            paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, strUerId))
            '�X�V���ꂽ�f�[�^�Z�b�g�� DB �֏�������
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
                '�p�����[�^�̐ݒ�
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


        '�I������
        commandTextSb = Nothing

        '�߂�l���Z�b�g�����̏ꍇ
        UpdKisoSiyouSetteiRenkei = True
    End Function
    '''<summary>��b�d�l�ݒ�̍X�V����</summary>
    Public Function UpdKisoSiyouSettei(ByVal strKameitenCd As String, ByVal strKahiKbn As String, ByVal strKsSiyouNo As String, ByVal strUerId As String) As Boolean
        '�߂�l
        UpdKisoSiyouSettei = False

        Dim commandTextSb As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
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

        '�p�����[�^�̐ݒ�

        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))
        'paramList.Add(MakeParam("@kahi_kbn", SqlDbType.Int, 4, strKahiKbn))
        paramList.Add(MakeParam("@ks_siyou_no", SqlDbType.Int, 4, Split(strKsSiyouNo, ":")(0)))
        paramList.Add(MakeParam("@ks_siyou_no1", SqlDbType.Int, 4, Split(strKsSiyouNo, ":")(1)))
        paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, strUerId))
        '�X�V���ꂽ�f�[�^�Z�b�g�� DB �֏�������
        ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

        '�I������
        commandTextSb = Nothing

        '�߂�l���Z�b�g�����̏ꍇ
        UpdKisoSiyouSettei = True
    End Function
    '''<summary>��b�d�l�ݒ�̍폜����</summary>
    Public Function DelKisoSiyouSettei(ByVal strKameitenCd As String, ByVal strKsSiyouNo As String, ByVal strKahiKbn As String, ByVal intRow As Integer) As Boolean
        '�߂�l
        DelKisoSiyouSettei = False

        Dim commandTextSb As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
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
        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))
        paramList.Add(MakeParam("@ks_siyou_no", SqlDbType.Int, 4, strKsSiyouNo))
        paramList.Add(MakeParam("@kahi_kbn", SqlDbType.Int, 4, strKahiKbn))
        paramList.Add(MakeParam("@nyuuryoku_no", SqlDbType.Int, 4, intRow))
        '�X�V���ꂽ�f�[�^�Z�b�g�� DB �֏�������
        ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

        '�I������
        commandTextSb = Nothing

        '�߂�l���Z�b�g�����̏ꍇ
        DelKisoSiyouSettei = True

    End Function

    ''' <summary>
    ''' �u����v�����擾����
    ''' </summary>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <returns>�u����v�f�[�^</returns>
    ''' <hidtory>2012/03/28 �ԗ� 405721�Č��̑Ή� �ǉ�</hidtory>
    Public Function SelTorikesi(ByVal strKameitenCd As String) As Data.DataTable

        Dim dsReturn As New DataSet

        Dim commandTextSb As New System.Text.StringBuilder

        '�p�����[�^�i�[
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
        
        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtTorikesi", paramList.ToArray)

        Return dsReturn.Tables("dtTorikesi")

    End Function

    ''' <summary>
    ''' �u�H�������ʁv�����擾����
    ''' </summary>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <returns>�u����v�f�[�^</returns>
    ''' <hidtory>2012/05/15 �ԗ� 405721�Č��̑Ή� �ǉ�</hidtory>
    Public Function SelKoujiUriageSyuubetu(ByVal strKameitenCd As String) As Data.DataTable

        Dim dsReturn As New DataSet

        Dim commandTextSb As New System.Text.StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	ISNULL(MM.meisyou,'') AS meisyou ") '--���� ")
            .AppendLine("FROM ")
            .AppendLine("	m_kameiten AS MK WITH(READCOMMITTED) ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_meisyou AS MM WITH(READCOMMITTED) ")
            .AppendLine("	ON ")
            .AppendLine("		MM.code = MK.koj_uri_syubetsu ") '--�H�������� ")
            .AppendLine("		AND ")
            .AppendLine("		MM.meisyou_syubetu = @meisyou_syubetu ") '--���̎�� ")
            .AppendLine("WHERE ")
            .AppendLine("	MK.kameiten_cd = @kameiten_cd ") '--�����X�R�[�h ")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))
        paramList.Add(MakeParam("@meisyou_syubetu", SqlDbType.Int, 10, "55"))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtKoujiUriageSyuubetu", paramList.ToArray)

        Return dsReturn.Tables("dtKoujiUriageSyuubetu")

    End Function


    '========2015/02/04 ���� 407679�̑Ή� �ǉ���======================
    ''' <summary>���̎�ʁ�33�̖��̂��擾����</summary>
    Public Function SelSyubetuInfo33() As Data.DataTable

        Dim dsReturn As New DataSet
        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine(" code, ")
        commandTextSb.AppendLine(" meisyou ")
        commandTextSb.AppendLine(" FROM  ")
        commandTextSb.AppendLine(" m_kakutyou_meisyou WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" WHERE meisyou_syubetu=@meisyou_syubetu ")
        commandTextSb.AppendLine(" ORDER BY hyouji_jyun ")
        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@meisyou_syubetu", SqlDbType.Char, 2, "33"))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, _
                    "dtMeisyou33", paramList.ToArray)

        Return dsReturn.Tables("dtMeisyou33")

    End Function

    ''' <summary>�g���u���E�N���[�������擾����</summary>
    Public Function SelTuujyouTyuuiJikouInfoTORA(ByVal strKameitenCd As String, ByVal strUerId As String) As TyuiJyouhouDataSet.TyuuiJikouTableDataTable
        ' DataSet�C���X�^���X�̐���()
        Dim dsTyuiJyouhouDataSet As New TyuiJyouhouDataSet
        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
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
        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, "0"))
        paramList.Add(MakeParam("@meisyou_syubetu", SqlDbType.Char, 2, "10"))
        paramList.Add(MakeParam("@login_user_id", SqlDbType.VarChar, 30, strUerId))
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsTyuiJyouhouDataSet, _
                    dsTyuiJyouhouDataSet.TyuuiJikouTable.TableName, paramList.ToArray)

        Return dsTyuiJyouhouDataSet.Tables(0)

    End Function

    '========2015/02/04 ���� 407679�̑Ή� �ǉ���======================


    ''' <summary>���i�R�[�h���擾����</summary>
    ''' <returns>���i�R�[�h�f�[�^�e�[�u��</returns>
    ''' <history>2016/05/04�@chel1�@�V�K�쐬</history>
    Public Function SelSyouhinCd() As Data.DataTable

        '�߂�f�[�^�Z�b�g
        Dim dsReturn As New Data.DataSet

        'SQL�R�����g
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	syouhin_cd ")                       '���i�R�[�h
            .AppendLine("	,(syouhin_cd + '�F' + syouhin_mei) AS syouhin_mei ") '���i���i���i�R�[�h�F���i���j
            .AppendLine("FROM ")
            .AppendLine("	m_syouhin WITH(READCOMMITTED) ")                        '���i�}�X�^
            .AppendLine("WHERE ")
            .AppendLine("	torikesi = 0 ")                     '���
            .AppendLine("	AND ")
            .AppendLine("	souko_cd = '100' ")                 '�q�ɃR�[�h
        End With

        'SQL�R�����g���s�A�߂�f�[�^�Z�b�g����
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtSyouhinCd")

        '�߂�f�[�^�e�[�u��
        Return dsReturn.Tables(0)

    End Function

    ''' <summary>�������@���擾����</summary>
    ''' <returns>�������@�f�[�^�e�[�u��</returns>
    ''' <history>2016/05/04�@chel1�@�V�K�쐬</history>
    Public Function SelTyousaHouhou() As Data.DataTable

        '�߂�f�[�^�Z�b�g
        Dim dsReturn As New Data.DataSet

        'SQL�R�����g
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT  ")
            .AppendLine("	tys_houhou_no ")                                                            '�������@NO
            .AppendLine("	,LTRIM(STR(tys_houhou_no) + '�F' + tys_houhou_mei) AS tys_houhou_mei ")     '�������@����
            .AppendLine("FROM ")
            .AppendLine("	m_tyousahouhou WITH(READCOMMITTED) ")                                       '�������@�}�X�^
        End With

        'SQL�R�����g���s�A�߂�f�[�^�Z�b�g����
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtTyousahouhou")

        '�߂�f�[�^�e�[�u��
        Return dsReturn.Tables(0)

    End Function


    '''<summary>��{���i�̍X�V����</summary>
    Public Function UpdKihonSyouhin(ByVal strKameitenCd As String, ByVal strKihonSyouhinCd As String, ByVal strKihonSyouhinTyuuibun As String) As Boolean

        Dim updCnt As Integer = 0

        Dim commandTextSb As New System.Text.StringBuilder

        '�p�����[�^�i�[
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

    '''<summary>��{�������@�̍X�V����</summary>
    Public Function UpdKihonTyousaHouhou(ByVal strKameitenCd As String, ByVal strKihonTyousaHouhouNo As String, ByVal strKihonTyousaHouhouTyuuibun As String) As Boolean

        Dim updCnt As Integer = 0

        Dim commandTextSb As New System.Text.StringBuilder

        '�p�����[�^�i�[
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
    ''' ��{���i�ƒ������@���擾����
    ''' </summary>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <history>2016/05/04�@chel1�@�V�K�쐬</history>
    Public Function SelKihouSyouhinAndTyousaHouhou(ByVal strKameitenCd As String) As Data.DataTable

        Dim dsReturn As New DataSet

        Dim commandTextSb As New System.Text.StringBuilder

        '�p�����[�^�i�[
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
            .AppendLine("	kameiten_cd = @kameiten_cd ") '--�����X�R�[�h ")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtKihouSyouhinAndTyousaHouhou", paramList.ToArray)

        Return dsReturn.Tables("dtKihouSyouhinAndTyousaHouhou")

    End Function


End Class

Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Imports System.Data
Imports System.Text

''' <summary>
''' �v��Ǘ��p_�����X���l���
''' </summary>
''' <remarks></remarks>
Public Class KeikaikuKanriKameitenBikouInquiryDA

    ''' <summary>
    ''' �����X���l���擾
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/02�@�ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelBikouInfo(ByVal strKameitenCd As String) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                strKameitenCd)

        '�߂�f�[�^�Z�b�g
        Dim dsReturn As New Data.DataSet

        'SQL�R�����g
        Dim commandTextSb As New StringBuilder

        '�o�����^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	MKKBS.bikou_syubetu ") '--���l��� ")
            .AppendLine("	,ISNULL(MKM.meisyou,'') AS meisyou ") '--���l��ʖ� ")
            .AppendLine("	,MKKBS.nyuuryoku_no ") '--����NO ")
            .AppendLine("	,ISNULL(MKKBS.naiyou,'') AS naiyou ") '--���e ")
            .AppendLine("FROM ")
            .AppendLine("	m_keikaku_kameiten_bikou_settei AS MKKBS WITH(READCOMMITTED) ") '--�v��Ǘ�_�����X���l�ݒ�Ͻ� ")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	m_keikakuyou_meisyou AS MKM WITH(READCOMMITTED) ") '--�v��p_���̃}�X�^ ")
            .AppendLine("	ON ")
            .AppendLine("		MKM.meisyou_syubetu = @meisyou_syubetu ")
            .AppendLine("		AND ")
            .AppendLine("		MKM.code = MKKBS.bikou_syubetu ")
            .AppendLine("WHERE ")
            .AppendLine("	MKKBS.kameiten_cd = @kameiten_cd ") '--�����X�R�[�h ")
            .AppendLine("ORDER BY ")
            .AppendLine("	MKKBS.nyuuryoku_no ASC ") '--����NO ")
        End With

        paramList.Add(MakeParam("@meisyou_syubetu", SqlDbType.VarChar, 2, "30"))
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))

        ' �������s
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtBikouInfo", paramList.ToArray)

        Return dsReturn.Tables("dtBikouInfo")

    End Function

    ''' <summary>
    ''' �����X���l�X�V���擾
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/02�@�ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelKameitenBikouMaxUpdTime(ByVal strKameitenCd As String) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                strKameitenCd)

        '�߂�f�[�^�Z�b�g
        Dim dsReturn As New Data.DataSet

        'SQL�R�����g
        Dim commandTextSb As New StringBuilder

        '�o�����^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
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

        ' �������s
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtMaxUpdTime", paramList.ToArray)

        Return dsReturn.Tables("dtMaxUpdTime")

    End Function

    ''' <summary>
    ''' �����X��ʎ擾
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Selkameitensyubetu(ByVal code As String) As String

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                code)

        ' DataSet�C���X�^���X�̐���
        Dim data As New DataSet

        ' SQL���̐���
        Dim sql As New StringBuilder

        ' �p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        ' �����X�}�X�g����Sql 
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


        ' �������s
        FillDataset(ManagerDA.Connection, CommandType.Text, sql.ToString(), data, "data", paramList.ToArray)

        If data.Tables(0).Rows.Count > 0 Then
            Return data.Tables(0).Rows(0).Item(1).ToString.Trim
        Else
            Return String.Empty
        End If

    End Function




    ''' <summary>
    ''' ���l�ǉ�
    ''' </summary>
    ''' <param name="dicPrm">�ǉ��f�[�^</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsBikou(ByVal dicPrm As Dictionary(Of String, String)) As Boolean

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                dicPrm)

        ' SQL���̐���
        Dim sql As New StringBuilder
        Dim intResult As Integer = 0
        ' �p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With sql
            .AppendLine("DECLARE @nyuuryokuno int")
            .AppendLine("set @nyuuryokuno = (select isnull( (select max(nyuuryoku_no) from m_keikaku_kameiten_bikou_settei Where kameiten_cd = @kameiten_cd), 0)) ")

            .AppendLine("INSERT INTO ")
            .AppendLine("	m_keikaku_kameiten_bikou_settei WITH(UPDLOCK) ") '--�v��Ǘ�_�����X���l�ݒ�Ͻ�
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

        '�p�����[�^
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, dicPrm("kameiten_cd")))
        paramList.Add(MakeParam("@bikou_syubetu", SqlDbType.VarChar, 5, dicPrm("bikou_syubetu")))
        paramList.Add(MakeParam("@naiyou", SqlDbType.VarChar, 80, dicPrm("naiyou")))
        paramList.Add(MakeParam("@kousinsya", SqlDbType.Int, 10, dicPrm("kousinsya")))
        paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, dicPrm("add_login_user_id")))

        ' �N�G�����s
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
    ''' ���l�X�V
    ''' </summary>
    ''' <param name="dicPrm">�ǉ��f�[�^</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdBikou(ByVal dicPrm As Dictionary(Of String, String)) As Boolean

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                dicPrm)

        ' SQL���̐���
        Dim sql As New StringBuilder
        Dim intResult As Integer = 0
        ' �p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With sql
            .AppendLine("UPDATE ")
            .AppendLine("	m_keikaku_kameiten_bikou_settei WITH(UPDLOCK) -- --�v��Ǘ�_�����X���l�ݒ�Ͻ� ")
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

        '�p�����[�^
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, dicPrm("kameiten_cd")))
        paramList.Add(MakeParam("@bikou_syubetu", SqlDbType.VarChar, 5, dicPrm("bikou_syubetu")))
        paramList.Add(MakeParam("@nyuuryoku_no", SqlDbType.Int, 10, dicPrm("nyuuryoku_no")))
        paramList.Add(MakeParam("@naiyou", SqlDbType.VarChar, 80, dicPrm("naiyou")))
        paramList.Add(MakeParam("@kousinsya", SqlDbType.Int, 10, dicPrm("kousinsya")))
        paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, dicPrm("add_login_user_id")))

        ' �N�G�����s
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
    ''' ���l�폜
    ''' </summary>
    ''' <param name="dicPrm">�ǉ��f�[�^</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DelBikou(ByVal dicPrm As Dictionary(Of String, String)) As Boolean

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                dicPrm)

        ' SQL���̐���
        Dim sql As New StringBuilder
        Dim intResult As Integer = 0
        ' �p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With sql
            .AppendLine("DELETE FROM ")
            .AppendLine("	m_keikaku_kameiten_bikou_settei WITH(UPDLOCK) ") '--�v��Ǘ�_�����X���l�ݒ�Ͻ�
            .AppendLine("WHERE ")
            .AppendLine("	kameiten_cd = @kameiten_cd ")
            .AppendLine("	AND ")
            .AppendLine("	nyuuryoku_no = @nyuuryoku_no ")
        End With

        '�p�����[�^
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, dicPrm("kameiten_cd")))
        paramList.Add(MakeParam("@nyuuryoku_no", SqlDbType.Int, 10, dicPrm("nyuuryoku_no")))

        ' �N�G�����s
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

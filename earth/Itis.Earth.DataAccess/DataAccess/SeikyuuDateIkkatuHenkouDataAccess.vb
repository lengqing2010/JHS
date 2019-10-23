Imports System.Data.SqlClient
Imports System.Text

Public Class SeikyuuDateIkkatuHenkouDataAccess

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

#Region "�����o�ϐ�"
    Dim connStr As String = ConnectionManager.Instance.ConnectionString
    Dim cmnDtAcc As New CmnDataAccess
#End Region

#Region "SQL�p�����[�^"

    Private Const DBparamSeikyuuSakiKbn As String = "@SEIKYUUSAKIKBN"
    Private Const DBparamSeikyuuSakiCd As String = "@SEIKYUUSAKICD"
    Private Const DBparamSeikyuuSakiBrc As String = "@SEIKYUUSAKIBRC"
    Private Const DBparamSeikyuuDate As String = "@SEIKYUUDATE"
    Private Const DBparamUpdDateTime As String = "@UPDDATETIME"
    Private Const DBparamLoginUserId As String = "@LOGINUSERID"

    Private sqlParams() As SqlClient.SqlParameter

#End Region

#Region "�R���X�g���N�^"
    ''' <summary>
    ''' �X�V�p�R���X�g���N�^
    ''' </summary>
    ''' <param name="seiCd">������R�[�h</param>
    ''' <param name="seiBrc">������}��</param>
    ''' <param name="seiKbn">������敪</param>
    ''' <param name="seiDate">�����N����</param>
    ''' <param name="updDate">�X�V�N����</param>
    ''' <param name="updUser">�X�V���O�C�����[�U�[ID</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal seiCd As String, _
                   ByVal seiBrc As String, _
                   ByVal seiKbn As String, _
                   ByVal seiDate As Date, _
                   ByVal updDate As DateTime, _
                   ByVal updUser As String)

        'SQL�p�����[�^�֐ݒ�
        sqlParams = New SqlParameter() { _
                                        SQLHelper.MakeParam(DBparamSeikyuuSakiCd, SqlDbType.VarChar, 5, seiCd), _
                                        SQLHelper.MakeParam(DBparamSeikyuuSakiBrc, SqlDbType.VarChar, 2, seiBrc), _
                                        SQLHelper.MakeParam(DBparamSeikyuuSakiKbn, SqlDbType.Char, 1, seiKbn), _
                                        SQLHelper.MakeParam(DBparamSeikyuuDate, SqlDbType.DateTime, 1, seiDate.Date), _
                                        SQLHelper.MakeParam(DBparamUpdDateTime, SqlDbType.DateTime, 1, updDate), _
                                        SQLHelper.MakeParam(DBparamLoginUserId, SqlDbType.VarChar, 30, updUser) _
                                        }
    End Sub

    ''' <summary>
    ''' �����p�R���X�g���N�^
    ''' </summary>
    ''' <param name="seiCd">������R�[�h</param>
    ''' <param name="seiBrc">������}��</param>
    ''' <param name="seiKbn">������敪</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal seiCd As String, _
                   ByVal seiBrc As String, _
                   ByVal seiKbn As String)

        'SQL�p�����[�^�֐ݒ�
        sqlParams = New SqlParameter() { _
                                        SQLHelper.MakeParam(DBparamSeikyuuSakiCd, SqlDbType.VarChar, 5, seiCd), _
                                        SQLHelper.MakeParam(DBparamSeikyuuSakiBrc, SqlDbType.VarChar, 2, seiBrc), _
                                        SQLHelper.MakeParam(DBparamSeikyuuSakiKbn, SqlDbType.Char, 1, seiKbn) _
                                        }
    End Sub
#End Region


#Region "�����N�����ꊇ�ύX����"

    ''' <summary>
    ''' ���������s���ꊇ�X�V���@�ʐ����e�[�u��
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updSeikyuuDateTeibetuSeikyuu() As Integer

        Dim intResult As Integer = 0
        Dim sb As New StringBuilder()
        'TABLE��
        Dim strTeibetuSeikyuuTable As String = Me.GetTeibetuSeikyuuSqlTable()
        'WHERE��
        Dim strTeibetuSeikyuuWhere As String = Me.GetTeibetuSeikyuuSqlWhere()

        ' �X�V�pSQL�ݒ�
        sb.AppendLine("UPDATE jhs_sys.t_teibetu_seikyuu ")
        sb.AppendLine("SET")
        sb.AppendLine("    seikyuusyo_hak_date = " & DBparamSeikyuuDate & "")
        sb.AppendLine("    , upd_login_user_id = " & DBparamLoginUserId & "")
        sb.AppendLine("    , upd_datetime = " & DBparamUpdDateTime & " ")

        sb.AppendLine(" FROM ")
        sb.AppendLine("      jhs_sys.t_teibetu_seikyuu ts ")

        '****************
        '* WHERE����
        '****************
        sb.AppendLine(strTeibetuSeikyuuWhere)

        ' �N�G�����s
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    sb.ToString(), _
                                    sqlParams)
        Return intResult
    End Function

    ''' <summary>
    ''' ���������s���ꊇ�X�V���X�ʐ����e�[�u��
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updSeikyuuDateTenbetuSeikyuu() As Integer

        Dim intResult As Integer = 0
        Dim sb As New StringBuilder()
        'TABLE��
        Dim strTenbetuSeikyuuTable As String = Me.GetTenbetuSeikyuuSqlTable()
        'WHERE��
        Dim strTenbetuSeikyuuWhere As String = Me.GetTenbetuSeikyuuSqlWhere()

        sb.AppendLine("UPDATE jhs_sys.t_tenbetu_seikyuu ")
        sb.AppendLine("SET")
        sb.AppendLine("    seikyuusyo_hak_date = " & DBparamSeikyuuDate & "")
        sb.AppendLine("    , upd_login_user_id = " & DBparamLoginUserId & "")
        sb.AppendLine("    , upd_datetime = " & DBparamUpdDateTime & " ")

        sb.AppendLine(" FROM ")
        sb.AppendLine("      jhs_sys.t_tenbetu_seikyuu ts ")

        '****************
        '* WHERE����
        '****************
        sb.AppendLine(strTenbetuSeikyuuWhere)

        ' �N�G�����s
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    sb.ToString(), _
                                    sqlParams)
        Return intResult
    End Function

    ''' <summary>
    ''' ���������s���ꊇ�X�V���X�ʏ��������e�[�u��
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updSeikyuuDateTenbetuSyokiSeikyuu() As Integer

        Dim intResult As Integer = 0
        Dim sb As New StringBuilder()
        'TABLE��
        Dim strTenbetuSyokiSeikyuuTable As String = Me.GetTenbetuSyokiSeikyuuSqlTable()
        'WHERE��
        Dim strTenbetuSyokiSeikyuuWhere As String = Me.GetTenbetuSyokiSeikyuuSqlWhere()

        ' �X�V�pSQL�ݒ�
        sb.AppendLine("UPDATE jhs_sys.t_tenbetu_syoki_seikyuu ")
        sb.AppendLine("SET")
        sb.AppendLine("    seikyuusyo_hak_date = " & DBparamSeikyuuDate & "")
        sb.AppendLine("    , upd_login_user_id = " & DBparamLoginUserId & "")
        sb.AppendLine("    , upd_datetime = " & DBparamUpdDateTime & " ")

        sb.AppendLine(" FROM ")
        sb.AppendLine("      jhs_sys.t_tenbetu_syoki_seikyuu ts ")

        '****************
        '* WHERE����
        '****************
        sb.AppendLine(strTenbetuSyokiSeikyuuWhere)

        ' �N�G�����s
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    sb.ToString(), _
                                    sqlParams)
        Return intResult
    End Function

    ''' <summary>
    ''' ���������s���ꊇ�X�V���ėp����e�[�u��
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updSeikyuuDateHannyouUriage() As Integer

        Dim intResult As Integer = 0
        Dim sb As New StringBuilder()
        'TABLE��
        Dim strHannyouUriageTable As String = Me.GetHannyouUriageSqlTable()
        'WHERE��
        Dim strHannyouUriageWhere As String = Me.GetHannyouUriageSqlWhere()

        ' �X�V�pSQL�ݒ�
        sb.AppendLine("UPDATE jhs_sys.t_hannyou_uriage ")
        sb.AppendLine("SET")
        sb.AppendLine("    seikyuu_date = " & DBparamSeikyuuDate & "")
        sb.AppendLine("    , upd_login_user_id = " & DBparamLoginUserId & "")
        sb.AppendLine("    , upd_datetime = " & DBparamUpdDateTime & " ")

        '****************
        '* TABLE����
        '****************
        sb.AppendLine(strHannyouUriageTable)

        '****************
        '* WHERE����
        '****************
        sb.AppendLine(strHannyouUriageWhere)

        ' �N�G�����s
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    sb.ToString(), _
                                    sqlParams)
        Return intResult
    End Function

    ''' <summary>
    ''' ����f�[�^�e�[�u���̐����N������UPDATE(���e�[�u�����폜���ꂽ�}�C�i�X�`�[��Ώ�)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateUriDataSeikyuuDate() As Integer

        Dim intResult As Integer = 0
        Dim sb As New StringBuilder()
        'TABLE��
        Dim strUriageDataTable As String = Me.GetUriageDataSqlTable()
        'WHERE��
        Dim strUriageDataWhere As String = Me.GetUriageDataSqlWhere()

        'SQL������
        sb.AppendLine("UPDATE jhs_sys.t_uriage_data ")
        sb.AppendLine("SET")
        sb.AppendLine("    seikyuu_date = " & DBparamSeikyuuDate & "")
        sb.AppendLine("    , upd_login_user_id = " & DBparamLoginUserId & "")
        sb.AppendLine("    , upd_datetime = " & DBparamUpdDateTime & " ")

        '****************
        '* TABLE����
        '****************
        sb.AppendLine(strUriageDataTable)

        '****************
        '* WHERE����
        '****************
        sb.AppendLine(strUriageDataWhere)

        ' �N�G�����s
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    sb.ToString(), _
                                    sqlParams)

        Return intResult
    End Function

    ''' <summary>
    ''' ����f�[�^�e�[�u���̐����N������UPDATE(������`�[�f�[�^���A�����N�������擾����эX�V)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateTorikesiUriDataSeikyuuDate() As Integer

        Dim intResult As Integer = 0
        Dim sb As New StringBuilder()
        'TABLE��
        Dim strUriageDataTable As String = Me.GetUriageDataSeikyuuDateSqlTable()
        'WHERE��
        Dim strUriageDataWhere As String = Me.GetUriageDataSeikyuuDateSqlWhere()

        'SQL������
        sb.AppendLine("UPDATE jhs_sys.t_uriage_data ")
        sb.AppendLine("SET")
        sb.AppendLine("    seikyuu_date = ud2.seikyuu_date ")
        sb.AppendLine("    , upd_login_user_id = " & DBparamLoginUserId & "")
        sb.AppendLine("    , upd_datetime = " & DBparamUpdDateTime & " ")

        '****************
        '* TABLE����
        '****************
        sb.AppendLine(strUriageDataTable)

        '****************
        '* WHERE����
        '****************
        sb.AppendLine(strUriageDataWhere)

        ' �N�G�����s
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    sb.ToString(), _
                                    sqlParams)

        Return intResult
    End Function

#End Region

#Region "�ꊇ�ύX�Ώۃf�[�^�̎擾"

    ''' <summary>
    ''' �����N�����ꊇ�ύX�������s���/�@�ʐ����f�[�^���擾���܂�
    ''' </summary>
    ''' <returns>�@�ʐ����e�[�u��</returns>
    ''' <remarks></remarks>
    Public Function GetTeibetuSeikyuuTbl() As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTeibetuSeikyuuTbl")

        'SQL���̐���
        Dim sb As New StringBuilder

        'SELECT��
        Dim strTeibetuSeikyuuSelect As String = Me.GetTeibetuSeikyuuSqlSelect()
        'TABLE��
        Dim strTeibetuSeikyuuTable As String = Me.GetTeibetuSeikyuuSqlTable()
        'WHERE��
        Dim strTeibetuSeikyuuWhere As String = Me.GetTeibetuSeikyuuSqlWhere()
        'ORDER BY��
        Dim strTeibetuSeikyuuOrderBy As String = Me.GetTeibetuSeikyuuSqlOrderBy()

        '****************
        '* SELECT����
        '****************
        sb.AppendLine(strTeibetuSeikyuuSelect)

        '****************
        '* TABLE����
        '****************
        sb.AppendLine(strTeibetuSeikyuuTable)

        '****************
        '* WHERE����
        '****************
        sb.AppendLine(strTeibetuSeikyuuWhere)

        '***********************************************************************
        ' ORDER BY��i�敪���ۏ؏�NO�����ރR�[�h����ʕ\��NO�j
        '***********************************************************************
        sb.Append(strTeibetuSeikyuuOrderBy)

        '�f�[�^�擾���ԋp
        Return cmnDtAcc.getDataTable(sb.ToString, sqlParams)
    End Function

    ''' <summary>
    ''' �����N�����ꊇ�ύX�������s���/�X�ʐ����f�[�^���擾���܂�
    ''' </summary>
    ''' <returns>�X�ʐ����e�[�u��</returns>
    ''' <remarks></remarks>
    Public Function GetTenbetuSeikyuuTbl() As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTenbetuSeikyuuTbl")

        'SQL���̐���
        Dim sb As New StringBuilder

        'SELECT��
        Dim strTenbetuSeikyuuSelect As String = Me.GetTenbetuSeikyuuSqlSelect()
        'TABLE��
        Dim strTenbetuSeikyuuTable As String = Me.GetTenbetuSeikyuuSqlTable()
        'WHERE��
        Dim strTenbetuSeikyuuWhere As String = Me.GetTenbetuSeikyuuSqlWhere()
        'ORDER BY��
        Dim strTenbetuSeikyuuOrderBy As String = Me.GetTenbetuSeikyuuSqlOrderBy()

        '****************
        '* SELECT����
        '****************
        sb.AppendLine(strTenbetuSeikyuuSelect)

        '****************
        '* TABLE����
        '****************
        sb.AppendLine(strTenbetuSeikyuuTable)

        '****************
        '* WHERE����
        '****************
        sb.AppendLine(strTenbetuSeikyuuWhere)

        '***********************************************************************
        ' ORDER BY��i���ރR�[�h���X�R�[�h�����͓������͓�NO�j
        '***********************************************************************
        sb.Append(strTenbetuSeikyuuOrderBy)

        '�f�[�^�擾���ԋp
        Return cmnDtAcc.getDataTable(sb.ToString, sqlParams)
    End Function

    ''' <summary>
    ''' �����N�����ꊇ�ύX�������s���/�X�ʏ��������f�[�^���擾���܂�
    ''' </summary>
    ''' <returns>�X�ʏ��������e�[�u��</returns>
    ''' <remarks></remarks>
    Public Function GetTenbetuSyokiSeikyuuTbl() As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTenbetuSyokiSeikyuuTbl")

        'SQL���̐���
        Dim sb As New StringBuilder

        'SELECT��
        Dim strTenbetuSyokiSeikyuuSelect As String = Me.GetTenbetuSyokiSeikyuuSqlSelect()
        'TABLE��
        Dim strTenbetuSyokiSeikyuuTable As String = Me.GetTenbetuSyokiSeikyuuSqlTable()
        'WHERE��
        Dim strTenbetuSyokiSeikyuuWhere As String = Me.GetTenbetuSyokiSeikyuuSqlWhere()
        'ORDER BY��
        Dim strTenbetuSyokiSeikyuuOrderBy As String = Me.GetTenbetuSyokiSeikyuuSqlOrderBy()

        '****************
        '* SELECT����
        '****************
        sb.AppendLine(strTenbetuSyokiSeikyuuSelect)

        '****************
        '* TABLE����
        '****************
        sb.AppendLine(strTenbetuSyokiSeikyuuTable)

        '****************
        '* WHERE����
        '****************
        sb.AppendLine(strTenbetuSyokiSeikyuuWhere)

        '***********************************************************************
        ' ORDER BY��i�X�R�[�h�A���ރR�[�h�j
        '***********************************************************************
        sb.Append(strTenbetuSyokiSeikyuuOrderBy)

        '�f�[�^�擾���ԋp
        Return cmnDtAcc.getDataTable(sb.ToString, sqlParams)
    End Function

    ''' <summary>
    ''' �����N�����ꊇ�ύX�������s���/�ėp����f�[�^���擾���܂�
    ''' </summary>
    ''' <returns>�ėp����e�[�u��</returns>
    ''' <remarks></remarks>
    Public Function GetHannyouUriageTbl() As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetHannyouUriageTbl")

        'SQL���̐���
        Dim sb As New StringBuilder

        'SELECT��
        Dim strHannyouUriageSelect As String = Me.GetHannyouUriageSqlSelect()
        'TABLE��
        Dim strHannyouUriageTable As String = Me.GetHannyouUriageSqlTable()
        'WHERE��
        Dim strHannyouUriageWhere As String = Me.GetHannyouUriageSqlWhere()
        'ORDER BY��
        Dim strHannyouUriageOrderBy As String = Me.GetHannyouUriageSqlOrderBy()

        '****************
        '* SELECT����
        '****************
        sb.AppendLine(strHannyouUriageSelect)

        '****************
        '* TABLE����
        '****************
        sb.AppendLine(strHannyouUriageTable)

        '****************
        '* WHERE����
        '****************
        sb.AppendLine(strHannyouUriageWhere)

        '***********************************************************************
        ' ORDER BY��i�X�R�[�h�A���ރR�[�h�j
        '***********************************************************************
        sb.Append(strHannyouUriageOrderBy)

        '�f�[�^�擾���ԋp
        Return cmnDtAcc.getDataTable(sb.ToString, sqlParams)
    End Function

    ''' <summary>
    ''' �����N�����ꊇ�ύX�������s���/����f�[�^���擾���܂�
    ''' </summary>
    ''' <returns>����e�[�u��</returns>
    ''' <remarks></remarks>
    Public Function GetUriageDataTbl() As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetUriageDataTbl")

        'SQL���̐���
        Dim sb As New StringBuilder

        'SELECT��
        Dim strUriageDataSelect As String = Me.GetUriageDataSqlSelect()
        'TABLE��
        Dim strUriageDataTable As String = Me.GetUriageDataSqlTable()
        'WHERE��
        Dim strUriageDataWhere As String = Me.GetUriageDataSqlWhere()
        'ORDER BY��
        Dim strUriageDataOrderBy As String = Me.GetUriageDataSqlOrderBy()

        '****************
        '* SELECT����
        '****************
        sb.AppendLine(strUriageDataSelect)

        '****************
        '* TABLE����
        '****************
        sb.AppendLine(strUriageDataTable)

        '****************
        '* WHERE����
        '****************
        sb.AppendLine(strUriageDataWhere)

        '***********************************************************************
        ' ORDER BY��i�X�R�[�h�A���ރR�[�h�j
        '***********************************************************************
        sb.Append(strUriageDataOrderBy)

        '�f�[�^�擾���ԋp
        Return cmnDtAcc.getDataTable(sb.ToString, sqlParams)
    End Function

    ''' <summary>
    ''' �����N�����ꊇ�ύX�������s���/����f�[�^���擾���܂�
    ''' </summary>
    ''' <returns>����e�[�u��</returns>
    ''' <remarks></remarks>
    Public Function GetUriageDataSeikyuuDateTbl() As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetUriageDataSeikyuuDateTbl")

        'SQL���̐���
        Dim sb As New StringBuilder

        'SELECT��
        Dim strUriageDataSelect As String = Me.GetUriageDataSqlSelect()
        'TABLE��
        Dim strUriageDataTable As String = Me.GetUriageDataSeikyuuDateSqlTable()
        'WHERE��
        Dim strUriageDataWhere As String = Me.GetUriageDataSeikyuuDateSqlWhere()
        'ORDER BY��
        Dim strUriageDataOrderBy As String = Me.GetUriageDataSqlOrderBy()

        '****************
        '* SELECT����
        '****************
        sb.AppendLine(strUriageDataSelect)

        '****************
        '* TABLE����
        '****************
        sb.AppendLine(strUriageDataTable)

        '****************
        '* WHERE����
        '****************
        sb.AppendLine(strUriageDataWhere)

        '***********************************************************************
        ' ORDER BY��i�X�R�[�h�A���ރR�[�h�j
        '***********************************************************************
        sb.Append(strUriageDataOrderBy)

        '�f�[�^�擾���ԋp
        Return cmnDtAcc.getDataTable(sb.ToString, sqlParams)
    End Function

#End Region

#Region "���ʃN�G��"

#Region "SELECT��"
    ''' <summary>
    ''' �@�ʐ����f�[�^�擾�p�̋���SELECT�N�G�����擾
    ''' </summary>
    ''' <returns>�@�ʐ����f�[�^�擾�p�̋���SELECT�N�G�����擾</returns>
    ''' <remarks></remarks>
    Private Function GetTeibetuSeikyuuSqlSelect() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTeibetuSeikyuuSqlSelect")

        'SQL���̐���
        Dim sb As New StringBuilder

        '****************
        '* SELECT����
        '****************
        sb.AppendLine(" SELECT ")
        sb.AppendLine("      ts.kbn ")
        sb.AppendLine("    , ts.hosyousyo_no AS bangou ")
        sb.AppendLine("    , j.sesyu_mei ")
        sb.AppendLine("    , j.kameiten_cd ")
        sb.AppendLine("    , ts.syouhin_cd ")
        sb.AppendLine("    , ms.syouhin_mei ")
        sb.AppendLine("    , NULL AS suu ")
        sb.AppendLine("    , NULL AS tanka ")
        sb.AppendLine("    , ts.uri_gaku ")
        sb.AppendLine("    , ts.seikyuusyo_hak_date ")
        sb.AppendLine("    , ts.uri_date ")
        sb.AppendLine("    , ts.denpyou_uri_date ")
        sb.AppendLine("    , ISNULL(ts.upd_login_user_id, ts.add_login_user_id) AS upd_login_user_id ")
        sb.AppendLine("    , ISNULL(ts.upd_datetime, ts.add_datetime) AS upd_datetime ")

        Return sb.ToString
    End Function

    ''' <summary>
    ''' �X�ʐ����f�[�^�擾�p�̋���SELECT�N�G�����擾
    ''' </summary>
    ''' <returns>�X�ʐ����f�[�^�擾�p�̋���SELECT�N�G�����擾</returns>
    ''' <remarks></remarks>
    Private Function GetTenbetuSeikyuuSqlSelect() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTenbetuSeikyuuSqlSelect")

        'SQL���̐���
        Dim sb As New StringBuilder

        '****************
        '* SELECT����
        '****************
        sb.AppendLine(" SELECT ")
        sb.AppendLine("      NULL AS kbn ")
        sb.AppendLine("    , NULL AS bangou ")
        sb.AppendLine("    , NULL AS sesyu_mei ")
        sb.AppendLine("    , ts.mise_cd AS kameiten_cd ")
        sb.AppendLine("    , ts.syouhin_cd ")
        sb.AppendLine("    , ms.syouhin_mei ")
        sb.AppendLine("    , ts.suu ")
        sb.AppendLine("    , ts.tanka ")
        sb.AppendLine("    , NULL AS uri_gaku ")
        sb.AppendLine("    , ts.seikyuusyo_hak_date ")
        sb.AppendLine("    , ts.uri_date ")
        sb.AppendLine("    , ts.denpyou_uri_date ")
        sb.AppendLine("    , ISNULL(ts.upd_login_user_id, ts.add_login_user_id) AS upd_login_user_id ")
        sb.AppendLine("    , ISNULL(ts.upd_datetime, ts.add_datetime) AS upd_datetime ")

        Return sb.ToString
    End Function

    ''' <summary>
    ''' �X�ʏ��������f�[�^�擾�p�̋���SELECT�N�G�����擾
    ''' </summary>
    ''' <returns>�X�ʏ��������f�[�^�擾�p�̋���SELECT�N�G�����擾</returns>
    ''' <remarks></remarks>
    Private Function GetTenbetuSyokiSeikyuuSqlSelect() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTenbetuSyokiSeikyuuSqlSelect")

        'SQL���̐���
        Dim sb As New StringBuilder

        '****************
        '* SELECT����
        '****************
        sb.AppendLine(" SELECT ")
        sb.AppendLine("      NULL AS kbn ")
        sb.AppendLine("    , NULL AS bangou ")
        sb.AppendLine("    , NULL AS sesyu_mei ")
        sb.AppendLine("    , ts.mise_cd AS kameiten_cd ")
        sb.AppendLine("    , ts.syouhin_cd ")
        sb.AppendLine("    , ms.syouhin_mei ")
        sb.AppendLine("    , NULL AS suu ")
        sb.AppendLine("    , NULL AS tanka ")
        sb.AppendLine("    , ts.uri_gaku ")
        sb.AppendLine("    , ts.seikyuusyo_hak_date ")
        sb.AppendLine("    , ts.uri_date ")
        sb.AppendLine("    , ts.denpyou_uri_date ")
        sb.AppendLine("    , ISNULL(ts.upd_login_user_id, ts.add_login_user_id) AS upd_login_user_id ")
        sb.AppendLine("    , ISNULL(ts.upd_datetime, ts.add_datetime) AS upd_datetime ")

        Return sb.ToString
    End Function

    ''' <summary>
    ''' �ėp����f�[�^�擾�p�̋���SELECT�N�G�����擾
    ''' </summary>
    ''' <returns>�ėp����f�[�^�擾�p�̋���SELECT�N�G�����擾</returns>
    ''' <remarks></remarks>
    Private Function GetHannyouUriageSqlSelect() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetHannyouUriageSqlSelect")

        'SQL���̐���
        Dim sb As New StringBuilder

        '****************
        '* SELECT����
        '****************
        sb.AppendLine(" SELECT ")
        sb.AppendLine("      hu.kbn ")
        sb.AppendLine("    , hu.bangou ")
        sb.AppendLine("    , hu.sesyu_mei ")
        sb.AppendLine("    , NULL AS kameiten_cd ")
        sb.AppendLine("    , hu.syouhin_cd ")
        sb.AppendLine("    , hu.hin_mei AS syouhin_mei ")
        sb.AppendLine("    , hu.suu ")
        sb.AppendLine("    , hu.tanka ")
        sb.AppendLine("    , NULL AS uri_gaku ")
        sb.AppendLine("    , hu.seikyuu_date AS seikyuusyo_hak_date ")
        sb.AppendLine("    , hu.uri_date ")
        sb.AppendLine("    , hu.denpyou_uri_date ")
        sb.AppendLine("    , ISNULL(hu.upd_login_user_id, hu.add_login_user_id) AS upd_login_user_id ")
        sb.AppendLine("    , ISNULL(hu.upd_datetime, hu.add_datetime) AS upd_datetime ")

        Return sb.ToString
    End Function

    ''' <summary>
    ''' ����f�[�^�擾�p�̋���SELECT�N�G�����擾
    ''' </summary>
    ''' <returns>����f�[�^�擾�p�̋���SELECT�N�G�����擾</returns>
    ''' <remarks></remarks>
    Private Function GetUriageDataSqlSelect() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetUriageDataSqlSelect")

        'SQL���̐���
        Dim sb As New StringBuilder

        '****************
        '* SELECT����
        '****************
        sb.AppendLine(" SELECT ")
        sb.AppendLine("      ud.kbn ")
        sb.AppendLine("    , ud.bangou ")
        sb.AppendLine("    , ISNULL(j.sesyu_mei, hu.sesyu_mei) AS sesyu_mei ")
        sb.AppendLine("    , ud.kameiten_cd ")
        sb.AppendLine("    , ud.syouhin_cd ")
        sb.AppendLine("    , ud.hinmei AS syouhin_mei ")
        sb.AppendLine("    , ud.suu ")
        sb.AppendLine("    , NULL AS tanka ")
        sb.AppendLine("    , ud.uri_gaku ")
        sb.AppendLine("    , ud.seikyuu_date AS seikyuusyo_hak_date ")
        sb.AppendLine("    , ud.uri_date ")
        sb.AppendLine("    , ud.denpyou_uri_date ")
        sb.AppendLine("    , ISNULL(ud.upd_login_user_id, ud.add_login_user_id) AS upd_login_user_id ")
        sb.AppendLine("    , ISNULL(ud.upd_datetime, ud.add_datetime) AS upd_datetime ")

        Return sb.ToString
    End Function

#End Region

#Region "TABLE��"
    ''' <summary>
    ''' �@�ʐ����f�[�^�擾�p�̋���TABLE�N�G�����擾
    ''' </summary>
    ''' <returns>�����f�[�^�擾�p�̋���TABLE�N�G�����擾</returns>
    ''' <remarks></remarks>
    Private Function GetTeibetuSeikyuuSqlTable() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTeibetuSeikyuuSqlTable")

        'SQL���̐���
        Dim sb As New StringBuilder

        '****************
        '* TABLE����(���C��)
        '****************
        sb.AppendLine(" FROM ")
        sb.AppendLine("      jhs_sys.t_teibetu_seikyuu ts ")
        sb.AppendLine("           LEFT OUTER JOIN t_jiban j ")
        sb.AppendLine("             ON ts.kbn = j.kbn ")
        sb.AppendLine("            AND ts.hosyousyo_no = j.hosyousyo_no ")
        sb.AppendLine("           LEFT OUTER JOIN m_syouhin ms ")
        sb.AppendLine("             ON ts.syouhin_cd = ms.syouhin_cd ")

        Return sb.ToString
    End Function

    ''' <summary>
    ''' �X�ʐ����f�[�^�擾�p�̋���TABLE�N�G�����擾
    ''' </summary>
    ''' <returns>�X�ʐ����f�[�^�擾�p�̋���TABLE�N�G�����擾</returns>
    ''' <remarks></remarks>
    Private Function GetTenbetuSeikyuuSqlTable() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTenbetuSeikyuuSqlTable")

        'SQL���̐���
        Dim sb As New StringBuilder

        '****************
        '* TABLE����(���C��)
        '****************
        sb.AppendLine(" FROM ")
        sb.AppendLine("      jhs_sys.t_tenbetu_seikyuu ts ")
        sb.AppendLine("           LEFT OUTER JOIN m_syouhin ms ")
        sb.AppendLine("             ON ts.syouhin_cd = ms.syouhin_cd ")

        Return sb.ToString
    End Function

    ''' <summary>
    ''' �X�ʏ��������f�[�^�擾�p�̋���TABLE�N�G�����擾
    ''' </summary>
    ''' <returns>�X�ʏ��������f�[�^�擾�p�̋���TABLE�N�G�����擾</returns>
    ''' <remarks></remarks>
    Private Function GetTenbetuSyokiSeikyuuSqlTable() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTenbetuSyokiSeikyuuSqlTable")

        'SQL���̐���
        Dim sb As New StringBuilder

        '****************
        '* TABLE����(���C��)
        '****************
        sb.AppendLine(" FROM ")
        sb.AppendLine("      jhs_sys.t_tenbetu_syoki_seikyuu ts ")
        sb.AppendLine("           LEFT OUTER JOIN m_syouhin ms ")
        sb.AppendLine("             ON ts.syouhin_cd = ms.syouhin_cd ")

        Return sb.ToString
    End Function

    ''' <summary>
    ''' �ėp����f�[�^�擾�p�̋���TABLE�N�G�����擾
    ''' </summary>
    ''' <returns>�ėp����f�[�^�擾�p�̋���TABLE�N�G�����擾</returns>
    ''' <remarks></remarks>
    Private Function GetHannyouUriageSqlTable() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetHannyouUriageSqlTable")

        'SQL���̐���
        Dim sb As New StringBuilder

        '****************
        '* TABLE����(���C��)
        '****************
        sb.AppendLine(" FROM ")
        sb.AppendLine("      jhs_sys.t_hannyou_uriage hu ")

        Return sb.ToString
    End Function

    ''' <summary>
    ''' ����f�[�^�擾�p�̋���TABLE�N�G�����擾
    ''' </summary>
    ''' <returns>����f�[�^�擾�p�̋���TABLE�N�G�����擾</returns>
    ''' <remarks></remarks>
    Private Function GetUriageDataSqlTable() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetUriageDataSqlTable")

        'SQL���̐���
        Dim sb As New StringBuilder

        '****************
        '* TABLE����(���C��)
        '****************
        sb.AppendLine(" FROM ")
        sb.AppendLine("      jhs_sys.t_uriage_data ud ")
        sb.AppendLine("           LEFT OUTER JOIN t_jiban j ")
        sb.AppendLine("             ON ud.kbn = j.kbn ")
        sb.AppendLine("            AND ud.bangou = j.hosyousyo_no ")
        sb.AppendLine("           LEFT OUTER JOIN t_hannyou_uriage hu ")
        sb.AppendLine("             ON ud.himoduke_cd = CAST(hu.han_uri_unique_no AS VARCHAR) ")

        Return sb.ToString
    End Function

    ''' <summary>
    ''' ����f�[�^�擾�p�̋���TABLE�N�G�����擾
    ''' </summary>
    ''' <returns>����f�[�^�擾�p�̋���TABLE�N�G�����擾</returns>
    ''' <remarks></remarks>
    Private Function GetUriageDataSeikyuuDateSqlTable() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetUriageDataSeikyuuDateSqlTable")

        'SQL���̐���
        Dim sb As New StringBuilder

        '****************
        '* TABLE����(���C��)
        '****************
        sb.AppendLine(" FROM ")
        sb.AppendLine("      jhs_sys.t_uriage_data ud ")
        sb.AppendLine("           LEFT OUTER JOIN ")
        sb.AppendLine("               (SELECT ")
        sb.AppendLine("                     * ")
        sb.AppendLine("                FROM ")
        sb.AppendLine("                     jhs_sys.t_uriage_data ")
        sb.AppendLine("               ) ")
        sb.AppendLine("                ud2 ")
        sb.AppendLine("             ON ud.torikesi_moto_denpyou_unique_no = ud2.denpyou_unique_no ")

        Return sb.ToString

    End Function
#End Region

#Region "WHERE��"
    ''' <summary>
    ''' �@�ʐ����f�[�^�擾�p�̋���WHERE�N�G�����擾
    ''' </summary>
    ''' <returns>�@�ʐ����f�[�^�擾�p�̋���WHERE�N�G�����擾</returns>
    ''' <remarks></remarks>
    Private Function GetTeibetuSeikyuuSqlWhere() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTeibetuSeikyuuSqlWhere")

        'SQL���̐���
        Dim sb As New StringBuilder()

        '****************
        '* WHERE����
        '****************
        sb.AppendLine(" WHERE ")
        sb.AppendLine("      EXISTS ")
        sb.AppendLine("     (SELECT ")
        sb.AppendLine("           td.* ")
        sb.AppendLine("      FROM ")
        sb.AppendLine("          (SELECT ")
        sb.AppendLine("                dd.* ")
        sb.AppendLine("           FROM ")
        sb.AppendLine("                jhs_sys.v_seikyuusyo_mihakkou_uri_data dd ")
        sb.AppendLine("           WHERE ")
        sb.AppendLine("                --�}�C�i�X�`�[�̗L��v���X�`�[�����O ")
        sb.AppendLine("                dd.denpyou_syubetu = 'UN' ")
        sb.AppendLine("            AND NOT EXISTS ")
        sb.AppendLine("               (SELECT ")
        sb.AppendLine("                     * ")
        sb.AppendLine("                FROM ")
        sb.AppendLine("                     jhs_sys.v_seikyuusyo_mihakkou_uri_data dr ")
        sb.AppendLine("                WHERE ")
        sb.AppendLine("                     dr.denpyou_syubetu = 'UR' ")
        sb.AppendLine("                 AND dr.torikesi_moto_denpyou_unique_no = dd.denpyou_unique_no ")
        sb.AppendLine("               ) ")
        sb.AppendLine("          ) ")
        sb.AppendLine("           tt ")
        sb.AppendLine("                INNER JOIN jhs_sys.t_teibetu_seikyuu td ")
        sb.AppendLine("                  ON (tt.himoduke_table_type = 1 ")
        sb.AppendLine("                 AND substring(tt.himoduke_cd, 1, 1) = td.kbn ")
        sb.AppendLine("                 AND substring(tt.himoduke_cd, 5, 10) = td.hosyousyo_no ")
        sb.AppendLine("                 AND substring(tt.himoduke_cd, 18, 2) = SUBSTRING(td.bunrui_cd, 1, 2) ")
        sb.AppendLine("                 AND substring(tt.himoduke_cd, 24, LEN(tt.himoduke_cd)) = CAST(td.gamen_hyouji_no AS VARCHAR)) ")
        sb.AppendLine("      WHERE ")
        sb.AppendLine("           tt.seikyuu_date IS NOT NULL ")
        sb.AppendLine("       AND td.seikyuusyo_hak_date IS NOT NULL ")
        sb.AppendLine("       AND td.kbn = ts.kbn ")
        sb.AppendLine("       AND td.hosyousyo_no = ts.hosyousyo_no ")
        sb.AppendLine("       AND td.bunrui_cd = ts.bunrui_cd ")
        sb.AppendLine("       AND td.gamen_hyouji_no = ts.gamen_hyouji_no ")
        sb.AppendLine("       AND tt.seikyuu_saki_cd = " & DBparamSeikyuuSakiCd & " ")
        sb.AppendLine("       AND tt.seikyuu_saki_brc = " & DBparamSeikyuuSakiBrc & " ")
        sb.AppendLine("       AND tt.seikyuu_saki_kbn = " & DBparamSeikyuuSakiKbn & " ")
        sb.AppendLine("     ) ")

        Return sb.ToString
    End Function

    ''' <summary>
    ''' �X�ʐ����f�[�^�擾�p�̋���WHERE�N�G�����擾
    ''' </summary>
    ''' <returns>�X�ʐ����f�[�^�擾�p�̋���WHERE�N�G�����擾</returns>
    ''' <remarks></remarks>
    Private Function GetTenbetuSeikyuuSqlWhere() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTenbetuSeikyuuSqlWhere")

        'SQL���̐���
        Dim sb As New StringBuilder()

        '****************
        '* WHERE����
        '****************
        sb.AppendLine(" WHERE ")
        sb.AppendLine("      EXISTS ")
        sb.AppendLine("     (SELECT ")
        sb.AppendLine("           td.* ")
        sb.AppendLine("      FROM ")
        sb.AppendLine("          (SELECT ")
        sb.AppendLine("                dd.* ")
        sb.AppendLine("           FROM ")
        sb.AppendLine("                jhs_sys.v_seikyuusyo_mihakkou_uri_data dd ")
        sb.AppendLine("           WHERE ")
        sb.AppendLine("                --�}�C�i�X�`�[�̗L��v���X�`�[�����O ")
        sb.AppendLine("                dd.denpyou_syubetu = 'UN' ")
        sb.AppendLine("            AND NOT EXISTS ")
        sb.AppendLine("               (SELECT ")
        sb.AppendLine("                     * ")
        sb.AppendLine("                FROM ")
        sb.AppendLine("                     jhs_sys.v_seikyuusyo_mihakkou_uri_data dr ")
        sb.AppendLine("                WHERE ")
        sb.AppendLine("                     dr.denpyou_syubetu = 'UR' ")
        sb.AppendLine("                 AND dr.torikesi_moto_denpyou_unique_no = dd.denpyou_unique_no ")
        sb.AppendLine("               ) ")
        sb.AppendLine("          ) ")
        sb.AppendLine("           tt ")
        sb.AppendLine("                INNER JOIN jhs_sys.t_tenbetu_seikyuu td ")
        sb.AppendLine("                  ON (tt.himoduke_table_type = 2 ")
        sb.AppendLine("                 AND substring(tt.himoduke_cd, 1, 5) = td.mise_cd ")
        sb.AppendLine("                 AND substring(tt.himoduke_cd, 9, 3) = td.bunrui_cd ")
        sb.AppendLine("                 AND substring(tt.himoduke_cd, 15, 8) = CONVERT(VARCHAR, td.nyuuryoku_date, 112) ")
        sb.AppendLine("                 AND substring(tt.himoduke_cd, 26, LEN(tt.himoduke_cd)) = CAST(td.nyuuryoku_date_no AS VARCHAR)) ")
        sb.AppendLine("      WHERE ")
        sb.AppendLine("           tt.seikyuu_date IS NOT NULL ")
        sb.AppendLine("       AND td.seikyuusyo_hak_date IS NOT NULL ")
        sb.AppendLine("       AND td.mise_cd = ts.mise_cd ")
        sb.AppendLine("       AND td.bunrui_cd = ts.bunrui_cd ")
        sb.AppendLine("       AND td.nyuuryoku_date = ts.nyuuryoku_date ")
        sb.AppendLine("       AND td.nyuuryoku_date_no = ts.nyuuryoku_date_no ")
        sb.AppendLine("       AND tt.seikyuu_saki_cd = " & DBparamSeikyuuSakiCd & " ")
        sb.AppendLine("       AND tt.seikyuu_saki_brc = " & DBparamSeikyuuSakiBrc & " ")
        sb.AppendLine("       AND tt.seikyuu_saki_kbn = " & DBparamSeikyuuSakiKbn & " ")
        sb.AppendLine("     ) ")

        Return sb.ToString
    End Function

    ''' <summary>
    ''' �X�ʏ��������f�[�^�擾�p�̋���WHERE�N�G�����擾
    ''' </summary>
    ''' <returns>�X�ʏ��������f�[�^�擾�p�̋���WHERE�N�G�����擾</returns>
    ''' <remarks></remarks>
    Private Function GetTenbetuSyokiSeikyuuSqlWhere() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTenbetuSyokiSeikyuuSqlWhere")

        'SQL���̐���
        Dim sb As New StringBuilder()

        '****************
        '* WHERE����
        '****************
        sb.AppendLine(" WHERE ")
        sb.AppendLine("      EXISTS ")
        sb.AppendLine("     (SELECT ")
        sb.AppendLine("           td.* ")
        sb.AppendLine("      FROM ")
        sb.AppendLine("          (SELECT ")
        sb.AppendLine("                dd.* ")
        sb.AppendLine("           FROM ")
        sb.AppendLine("                jhs_sys.v_seikyuusyo_mihakkou_uri_data dd ")
        sb.AppendLine("           WHERE ")
        sb.AppendLine("                --�}�C�i�X�`�[�̗L��v���X�`�[�����O ")
        sb.AppendLine("                dd.denpyou_syubetu = 'UN' ")
        sb.AppendLine("            AND NOT EXISTS ")
        sb.AppendLine("               (SELECT ")
        sb.AppendLine("                     * ")
        sb.AppendLine("                FROM ")
        sb.AppendLine("                     jhs_sys.v_seikyuusyo_mihakkou_uri_data dr ")
        sb.AppendLine("                WHERE ")
        sb.AppendLine("                     dr.denpyou_syubetu = 'UR' ")
        sb.AppendLine("                 AND dr.torikesi_moto_denpyou_unique_no = dd.denpyou_unique_no ")
        sb.AppendLine("               ) ")
        sb.AppendLine("          ) ")
        sb.AppendLine("           tt ")
        sb.AppendLine("                INNER JOIN jhs_sys.t_tenbetu_syoki_seikyuu td ")
        sb.AppendLine("                  ON (tt.himoduke_table_type = 3 ")
        sb.AppendLine("                 AND substring(tt.himoduke_cd, 1, 5) = td.mise_cd ")
        sb.AppendLine("                 AND substring(tt.himoduke_cd, 9, LEN(tt.himoduke_cd)) = td.bunrui_cd) ")
        sb.AppendLine("      WHERE ")
        sb.AppendLine("           tt.seikyuu_date IS NOT NULL ")
        sb.AppendLine("       AND td.seikyuusyo_hak_date IS NOT NULL ")
        sb.AppendLine("       AND td.mise_cd = ts.mise_cd ")
        sb.AppendLine("       AND td.bunrui_cd = ts.bunrui_cd ")
        sb.AppendLine("       AND tt.seikyuu_saki_cd = " & DBparamSeikyuuSakiCd & " ")
        sb.AppendLine("       AND tt.seikyuu_saki_brc = " & DBparamSeikyuuSakiBrc & " ")
        sb.AppendLine("       AND tt.seikyuu_saki_kbn = " & DBparamSeikyuuSakiKbn & " ")
        sb.AppendLine("     ) ")

        Return sb.ToString
    End Function

    ''' <summary>
    ''' �ėp����f�[�^�擾�p�̋���WHERE�N�G�����擾
    ''' </summary>
    ''' <returns>�ėp����f�[�^�擾�p�̋���WHERE�N�G�����擾</returns>
    ''' <remarks></remarks>
    Private Function GetHannyouUriageSqlWhere() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetHannyouUriageSqlWhere")

        'SQL���̐���
        Dim sb As New StringBuilder()

        '****************
        '* WHERE����
        '****************
        sb.AppendLine(" WHERE ")
        sb.AppendLine("      EXISTS ")
        sb.AppendLine("     (SELECT ")
        sb.AppendLine("           td.* ")
        sb.AppendLine("      FROM ")
        sb.AppendLine("          (SELECT ")
        sb.AppendLine("                dd.* ")
        sb.AppendLine("           FROM ")
        sb.AppendLine("                jhs_sys.v_seikyuusyo_mihakkou_uri_data dd ")
        sb.AppendLine("           WHERE ")
        sb.AppendLine("                --�}�C�i�X�`�[�̗L��v���X�`�[�����O ")
        sb.AppendLine("                dd.denpyou_syubetu = 'UN' ")
        sb.AppendLine("            AND NOT EXISTS ")
        sb.AppendLine("               (SELECT ")
        sb.AppendLine("                     * ")
        sb.AppendLine("                FROM ")
        sb.AppendLine("                     jhs_sys.v_seikyuusyo_mihakkou_uri_data dr ")
        sb.AppendLine("                WHERE ")
        sb.AppendLine("                     dr.denpyou_syubetu = 'UR' ")
        sb.AppendLine("                 AND dr.torikesi_moto_denpyou_unique_no = dd.denpyou_unique_no ")
        sb.AppendLine("               ) ")
        sb.AppendLine("          ) ")
        sb.AppendLine("           tt ")
        sb.AppendLine("                INNER JOIN jhs_sys.t_hannyou_uriage td ")
        sb.AppendLine("                  ON (tt.himoduke_table_type = 9 ")
        sb.AppendLine("                 AND tt.himoduke_cd = td.han_uri_unique_no) ")
        sb.AppendLine("      WHERE ")
        sb.AppendLine("           tt.seikyuu_date IS NOT NULL ")
        sb.AppendLine("       AND td.seikyuu_date IS NOT NULL ")
        sb.AppendLine("       AND td.han_uri_unique_no = hu.han_uri_unique_no ")
        sb.AppendLine("       AND tt.seikyuu_saki_cd = " & DBparamSeikyuuSakiCd & " ")
        sb.AppendLine("       AND tt.seikyuu_saki_brc = " & DBparamSeikyuuSakiBrc & " ")
        sb.AppendLine("       AND tt.seikyuu_saki_kbn = " & DBparamSeikyuuSakiKbn & " ")
        sb.AppendLine("     ) ")

        Return sb.ToString
    End Function

    ''' <summary>
    ''' ����f�[�^�擾�p�̋���WHERE�N�G�����擾(���e�[�u�����폜���ꂽ�}�C�i�X�`�[��Ώ�)
    ''' </summary>
    ''' <returns>����f�[�^�擾�p�̋���WHERE�N�G�����擾(���e�[�u�����폜���ꂽ�}�C�i�X�`�[��Ώ�)</returns>
    ''' <remarks></remarks>
    Private Function GetUriageDataSqlWhere() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetUriageDataSqlWhere")

        'SQL���̐���
        Dim sb As New StringBuilder()

        '****************
        '* WHERE����
        '****************
        sb.AppendLine(" WHERE ")
        sb.AppendLine("      EXISTS ")
        sb.AppendLine("     (SELECT ")
        sb.AppendLine("           * ")
        sb.AppendLine("      FROM ")
        sb.AppendLine("           jhs_sys.v_seikyuusyo_mihakkou_uri_data tt ")
        sb.AppendLine("      WHERE ")
        sb.AppendLine("           tt.denpyou_unique_no = ud.denpyou_unique_no ")
        sb.AppendLine("       AND tt.seikyuu_saki_cd = " & DBparamSeikyuuSakiCd & " ")
        sb.AppendLine("       AND tt.seikyuu_saki_brc = " & DBparamSeikyuuSakiBrc & " ")
        sb.AppendLine("       AND tt.seikyuu_saki_kbn = " & DBparamSeikyuuSakiKbn & " ")
        sb.AppendLine("       AND tt.denpyou_syubetu = 'UR' ")
        sb.AppendLine("       AND tt.uri_keijyou_flg = 1 ")
        sb.AppendLine("       AND (( ")
        sb.AppendLine("           --�@�ʐ����ɑ��݂��Ȃ�����f�[�^ ")
        sb.AppendLine("           tt.himoduke_table_type = 1 ")
        sb.AppendLine("       AND NOT EXISTS ")
        sb.AppendLine("               (SELECT ")
        sb.AppendLine("                     * ")
        sb.AppendLine("                FROM ")
        sb.AppendLine("                     jhs_sys.t_teibetu_seikyuu td ")
        sb.AppendLine("                WHERE ")
        sb.AppendLine("                     substring(tt.himoduke_cd, 1, 1) = td.kbn ")
        sb.AppendLine("                 AND substring(tt.himoduke_cd, 5, 10) = td.hosyousyo_no ")
        sb.AppendLine("                 AND substring(tt.himoduke_cd, 18, 2) = SUBSTRING(td.bunrui_cd, 1, 2) ")
        sb.AppendLine("                 AND substring(tt.himoduke_cd, 24, LEN(tt.himoduke_cd)) = CAST(td.gamen_hyouji_no AS VARCHAR) ")
        sb.AppendLine("               ) ")
        sb.AppendLine("          ) ")
        sb.AppendLine("        OR ( ")
        sb.AppendLine("           --�X�ʐ����ɑ��݂��Ȃ�����f�[�^ ")
        sb.AppendLine("           tt.himoduke_table_type = 2 ")
        sb.AppendLine("       AND NOT EXISTS ")
        sb.AppendLine("               (SELECT ")
        sb.AppendLine("                     * ")
        sb.AppendLine("                FROM ")
        sb.AppendLine("                     jhs_sys.t_tenbetu_seikyuu td ")
        sb.AppendLine("                WHERE ")
        sb.AppendLine("                     substring(tt.himoduke_cd, 1, 5) = td.mise_cd ")
        sb.AppendLine("                 AND substring(tt.himoduke_cd, 9, 3) = td.bunrui_cd ")
        sb.AppendLine("                 AND substring(tt.himoduke_cd, 15, 8) = CONVERT(VARCHAR, td.nyuuryoku_date, 112) ")
        sb.AppendLine("                 AND substring(tt.himoduke_cd, 26, LEN(tt.himoduke_cd)) = CAST(td.nyuuryoku_date_no AS VARCHAR) ")
        sb.AppendLine("               ) ")
        sb.AppendLine("          ) ")
        sb.AppendLine("        OR ( ")
        sb.AppendLine("           --�X�ʏ��������ɑ��݂��Ȃ�����f�[�^ ")
        sb.AppendLine("           tt.himoduke_table_type = 3 ")
        sb.AppendLine("       AND NOT EXISTS ")
        sb.AppendLine("               (SELECT ")
        sb.AppendLine("                     * ")
        sb.AppendLine("                FROM ")
        sb.AppendLine("                     jhs_sys.t_tenbetu_syoki_seikyuu td ")
        sb.AppendLine("                WHERE ")
        sb.AppendLine("                     substring(tt.himoduke_cd, 1, 5) = td.mise_cd ")
        sb.AppendLine("                 AND substring(tt.himoduke_cd, 9, LEN(tt.himoduke_cd)) = td.bunrui_cd ")
        sb.AppendLine("               ) ")
        sb.AppendLine("          ) ")
        sb.AppendLine("        OR ( ")
        sb.AppendLine("           --�ėp����ɑ��݂��Ȃ�����f�[�^ ")
        sb.AppendLine("           tt.himoduke_table_type = 9 ")
        sb.AppendLine("       AND NOT EXISTS ")
        sb.AppendLine("               (SELECT ")
        sb.AppendLine("                     * ")
        sb.AppendLine("                FROM ")
        sb.AppendLine("                     jhs_sys.t_hannyou_uriage td ")
        sb.AppendLine("                WHERE ")
        sb.AppendLine("                     tt.himoduke_cd = td.han_uri_unique_no ")
        sb.AppendLine("                    )")
        sb.AppendLine("                )")
        sb.AppendLine("            )")
        sb.AppendLine("    ) ")

        Return sb.ToString
    End Function

    ''' <summary>
    ''' ����f�[�^�擾�p�̋���WHERE�N�G�����擾(�v��ςł���/�ԓ`�[�Ő����N�������قȂ鐿�������쐬�̃}�C�i�X�`�[��Ώ�)
    ''' </summary>
    ''' <returns>����f�[�^�擾�p�̋���WHERE�N�G�����擾(�v��ςł���/�ԓ`�[�Ő����N�������قȂ鐿�������쐬�̃}�C�i�X�`�[��Ώ�)</returns>
    ''' <remarks></remarks>
    Private Function GetUriageDataSeikyuuDateSqlWhere() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetUriageDataSeikyuuDateSqlWhere")

        'SQL���̐���
        Dim sb As New StringBuilder()

        '****************
        '* WHERE����
        '****************
        sb.Append(" WHERE ")
        sb.Append("      ud.seikyuu_saki_cd = " & DBparamSeikyuuSakiCd & " ")
        sb.Append("  AND ud.seikyuu_saki_brc = " & DBparamSeikyuuSakiBrc & " ")
        sb.Append("  AND ud.seikyuu_saki_kbn = " & DBparamSeikyuuSakiKbn & " ")
        sb.Append("  AND ud.uri_keijyou_flg = '1' ")
        sb.Append("  AND CONVERT(VARCHAR, ud.seikyuu_date, 111) <> CONVERT(VARCHAR, ud2.seikyuu_date, 111) ")
        sb.Append("  AND EXISTS ")
        sb.Append("     (SELECT ")
        sb.Append("           * ")
        sb.Append("      FROM ")
        sb.Append("           jhs_sys.v_seikyuusyo_mihakkou_uri_data tt ")
        sb.Append("      WHERE ")
        sb.Append("           tt.denpyou_unique_no = ud.denpyou_unique_no ")
        sb.Append("       AND tt.seikyuu_saki_cd = ud.seikyuu_saki_cd ")
        sb.Append("       AND tt.seikyuu_saki_brc = ud.seikyuu_saki_brc ")
        sb.Append("       AND tt.seikyuu_saki_kbn = ud.seikyuu_saki_kbn ")
        sb.Append("       AND tt.denpyou_syubetu = 'UR' ")
        sb.Append("     ) ")
        sb.Append("  ")

        Return sb.ToString
    End Function

#End Region

#Region "ORDER BY��"

    ''' <summary>
    ''' �@�ʐ����f�[�^�擾�p�̋���ORDER BY�N�G�����擾
    ''' </summary>
    ''' <returns>�@�ʐ����f�[�^�擾�p�̋���ORDER BY�N�G�����擾</returns>
    ''' <remarks></remarks>
    Private Function GetTeibetuSeikyuuSqlOrderBy() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTeibetuSeikyuuSqlOrderBy")

        'SQL���̐���
        Dim sb As New StringBuilder

        sb.AppendLine(" ORDER BY ")
        sb.AppendLine("      ts.kbn ")
        sb.AppendLine("    , ts.hosyousyo_no ")
        sb.AppendLine("    , ts.bunrui_cd ")
        sb.AppendLine("    , ts.gamen_hyouji_no ")
        sb.AppendLine("")

        Return sb.ToString
    End Function

    ''' <summary>
    ''' �X�ʐ����f�[�^�擾�p�̋���ORDER BY�N�G�����擾
    ''' </summary>
    ''' <returns>�X�ʐ����f�[�^�擾�p�̋���ORDER BY�N�G�����擾</returns>
    ''' <remarks></remarks>
    Private Function GetTenbetuSeikyuuSqlOrderBy() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTenbetuSeikyuuSqlOrderBy")

        'SQL���̐���
        Dim sb As New StringBuilder

        sb.AppendLine(" ORDER BY ")
        sb.AppendLine("      ts.bunrui_cd ")
        sb.AppendLine("    , ts.mise_cd ")
        sb.AppendLine("    , ts.nyuuryoku_date ")
        sb.AppendLine("    , ts.nyuuryoku_date_no ")
        sb.AppendLine("")

        Return sb.ToString
    End Function

    ''' <summary>
    ''' �X�ʏ��������f�[�^�擾�p�̋���ORDER BY�N�G�����擾
    ''' </summary>
    ''' <returns>�X�ʏ��������f�[�^�擾�p�̋���ORDER BY�N�G�����擾</returns>
    ''' <remarks></remarks>
    Private Function GetTenbetuSyokiSeikyuuSqlOrderBy() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTenbetuSyokiSeikyuuSqlOrderBy")

        'SQL���̐���
        Dim sb As New StringBuilder

        sb.AppendLine(" ORDER BY ")
        sb.AppendLine("      ts.mise_cd ")
        sb.AppendLine("    , ts.bunrui_cd ")
        sb.AppendLine("")

        Return sb.ToString
    End Function

    ''' <summary>
    ''' �ėp����f�[�^�擾�p�̋���ORDER BY�N�G�����擾
    ''' </summary>
    ''' <returns>�ėp����f�[�^�擾�p�̋���ORDER BY�N�G�����擾</returns>
    ''' <remarks></remarks>
    Private Function GetHannyouUriageSqlOrderBy() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetHannyouUriageSqlOrderBy")

        'SQL���̐���
        Dim sb As New StringBuilder

        sb.AppendLine(" ORDER BY ")
        sb.AppendLine("      hu.kbn ")
        sb.AppendLine("    , hu.bangou ")
        sb.AppendLine("    , hu.syouhin_cd ")
        sb.AppendLine("    , hu.seikyuu_date ")
        sb.AppendLine("    , hu.han_uri_unique_no ")
        sb.AppendLine("")

        Return sb.ToString
    End Function

    ''' <summary>
    ''' ����f�[�^�擾�p�̋���ORDER BY�N�G�����擾
    ''' </summary>
    ''' <returns>����f�[�^�擾�p�̋���ORDER BY�N�G�����擾</returns>
    ''' <remarks></remarks>
    Private Function GetUriageDataSqlOrderBy() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetUriageDataSqlOrderBy")

        'SQL���̐���
        Dim sb As New StringBuilder

        sb.AppendLine(" ORDER BY ")
        sb.AppendLine("      ud.kbn ")
        sb.AppendLine("    , ud.bangou ")
        sb.AppendLine("    , ud.syouhin_cd ")
        sb.AppendLine("    , ud.seikyuu_date ")
        sb.AppendLine("    , ud.denpyou_unique_no ")
        sb.AppendLine("")

        Return sb.ToString
    End Function

#End Region

#End Region

End Class

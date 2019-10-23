Imports System
Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' �����f�[�^�̎擾�N���X
''' </summary>
''' <remarks></remarks>
Public Class SeikyuuMiinsatuDataAccess

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

#Region "�����o�ϐ�"
    Dim connStr As String = ConnectionManager.Instance.ConnectionString
    Dim SLogic As New StringLogic
    Dim cmnDtAcc As New CmnDataAccess
#End Region

#Region "�������f�[�^�̎擾"
    ''' <summary>
    ''' ������������ꗗ���/�����f�[�^���擾���܂�
    ''' </summary>
    ''' <param name="dtRec">�����f�[�^���R�[�h�N���X</param>
    ''' <returns>�����f�[�^�e�[�u��</returns>
    ''' <remarks></remarks>
    Public Function GetSearchSeikyuusyoTbl(ByVal dtRec As SeikyuuDataRecord) As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSearchSeikyuusyoTbl", dtRec)

        'SQL���̐���
        Dim cmdTextSb As New StringBuilder

        'SELECT��
        Dim strCmnSelect As String = Me.GetSearchSeikyuusyoSqlSelect()
        'TABLE��
        Dim strCmnTable As String = Me.GetCmnSqlTable()
        'WHERE��
        Dim strCmnWhere As String = Me.GetCmnSqlWhere(dtRec)
        'ORDER BY��
        Dim strCmnOrderBy As String = Me.GetCmnSqlOrderBy()

        '****************
        '* SELECT����
        '****************
        cmdTextSb.Append(strCmnSelect)

        '****************
        '* TABLE����
        '****************
        cmdTextSb.Append(strCmnTable)

        '****************
        '* WHERE��
        '****************
        cmdTextSb.Append(strCmnWhere)

        '***********************************************************************
        ' ORDER BY��i���������s��DESC��������i�敪�E�R�[�h�E�}�ԁj�j
        '***********************************************************************
        cmdTextSb.Append(strCmnOrderBy)

        '�f�[�^�擾���ԋp
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString)
    End Function

#End Region

#Region "SELECT��"
    ''' <summary>
    ''' ����������f�[�^�擾�p��SELECT�N�G�����擾
    ''' </summary>
    ''' <returns>����������f�[�^�擾�p�̋���SELECT�N�G�����擾</returns>
    ''' <remarks></remarks>
    Private Function GetSearchSeikyuusyoSqlSelect() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSearchSeikyuusyoSqlSelect")

        'SQL���̐���
        Dim cmdTextSb As New StringBuilder

        '****************
        '* SELECT����
        '****************
        cmdTextSb.Append(" SELECT ")
        cmdTextSb.Append("      SK.seikyuu_saki_cd ")
        cmdTextSb.Append("    , SK.seikyuu_saki_brc ")
        cmdTextSb.Append("    , SK.seikyuu_saki_kbn ")
        cmdTextSb.Append("    , SK.seikyuu_saki_mei ")
        cmdTextSb.Append("    , SK.seikyuusyo_hak_date ")
        cmdTextSb.Append("    , SK.seikyuu_sime_date ")
        cmdTextSb.Append("    , ISNULL(MKM.meisyou, '') AS mst_meisyou ")

        Return cmdTextSb.ToString
    End Function

#End Region

#Region "TABLE��"
    ''' <summary>
    ''' �����f�[�^�擾�p��TABLE�N�G�����擾
    ''' </summary>
    ''' <returns>�����f�[�^�擾�p�̋���TABLE�N�G�����擾</returns>
    ''' <remarks></remarks>
    Private Function GetCmnSqlTable() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetCmnSqlTable")

        'SQL���̐���
        Dim cmdTextSb As New StringBuilder

        '****************
        '* TABLE����(���C��)
        '****************
        cmdTextSb.Append(" FROM ")
        cmdTextSb.Append("    t_seikyuu_kagami SK WITH (READCOMMITTED) ")

        '***********************************************************************
        ' �g�����̃}�X�^�F����(�O������)
        '***********************************************************************
        cmdTextSb.Append(" LEFT OUTER JOIN m_kakutyou_meisyou MKM WITH (READCOMMITTED) ")
        cmdTextSb.Append("   ON MKM.meisyou_syubetu = 3 ")
        cmdTextSb.Append("  AND SK.kaisyuu_seikyuusyo_yousi = MKM.code ")

        Return cmdTextSb.ToString
    End Function

#End Region

#Region "WHERE��"
    ''' <summary>
    ''' �����f�[�^�擾�p��WHERE�N�G�����擾
    ''' </summary>
    ''' <param name="dtRec">�����f�[�^���R�[�h�N���X</param>
    ''' <returns>�����f�[�^�擾�p�̋���WHERE�N�G�����擾</returns>
    ''' <remarks></remarks>
    Private Function GetCmnSqlWhere(ByVal dtRec As SeikyuuDataRecord) As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetCmnSqlWhere", dtRec)

        'SQL���̐���
        Dim cmdTextSb As New StringBuilder

        cmdTextSb.Append(" WHERE 1 = 1 ")
        cmdTextSb.Append("  AND SK.seikyuusyo_insatu_date IS NULL ")
        cmdTextSb.Append("  AND SK.torikesi = 0 ")

        Return cmdTextSb.ToString
    End Function

#End Region

#Region "ORDER BY��"
    ''' <summary>
    ''' �����f�[�^�擾�p��ORDER BY�N�G�����擾
    ''' </summary>
    ''' <returns>�����f�[�^�擾�p�̋���ORDER BY�N�G�����擾</returns>
    ''' <remarks></remarks>
    Private Function GetCmnSqlOrderBy() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetCmnSqlOrderBy")

        'SQL���̐���
        Dim cmdTextSb As New StringBuilder

        cmdTextSb.Append(" ORDER BY ")
        cmdTextSb.Append("      SK.seikyuu_saki_cd ")
        cmdTextSb.Append("    , SK.seikyuu_saki_brc ")
        cmdTextSb.Append("    , SK.seikyuu_saki_kbn ")
        cmdTextSb.Append("    , SK.seikyuusyo_hak_date ")

        Return cmdTextSb.ToString
    End Function
#End Region

End Class

Imports System.text
Imports System.Data.SqlClient

Public Class KoujiSyouhinDataAccess
    Inherits AbsDataAccess

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

#Region "�������[�h"
    ''' <summary>
    ''' �R���g���[���̌������[�h
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SearchMode
        ''' <summary>
        ''' ���ǍH��
        ''' </summary>
        ''' <remarks></remarks>
        KAIRYOU = 130
        ''' <summary>
        ''' �ǉ��H��
        ''' </summary>
        ''' <remarks></remarks>
        TUIKA = 140
    End Enum
#End Region

    ''' <summary>
    ''' �����H�����
    ''' </summary>
    ''' <remarks></remarks>
    Private searchKoujiMode As Integer

    ''' <summary>
    ''' �R�l�N�V�����X�g�����O
    ''' </summary>
    ''' <remarks></remarks>
    Private connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>
    ''' �R���X�g���N�^
    ''' </summary>
    ''' <param name="mode"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal mode As SearchMode)
        searchKoujiMode = mode
    End Sub

#Region "�H����Џ��i�R���{�f�[�^�쐬"
    ''' <summary>
    ''' �R���{�{�b�N�X�ݒ�p�̗L���ȏ��i���R�[�h��S�Ď擾���܂�
    ''' </summary>
    ''' <param name="dt" ></param>
    ''' <param name="withSpaceRow" >�擪�ɋ󔒍s���Z�b�g����ꍇ:true</param>
    ''' <param name="withCode" >�i�C�ӁjValue��Key���ڂ�t������ꍇ:true " 1:aaaa "</param>
    ''' <remarks></remarks>
    Public Overrides Sub GetDropdownData(ByRef dt As DataTable, ByVal withSpaceRow As Boolean, Optional ByVal withCode As Boolean = True, Optional ByVal blnTorikesi As Boolean = True)
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetDropdownData", dt, withSpaceRow, withCode)

        Dim connStr As String = ConnectionManager.Instance.ConnectionString
        Dim commandTextSb As New StringBuilder()
        Dim row As SyouhinDataSet.SyouhinTableRow

        commandTextSb.Append(" SELECT ")
        commandTextSb.Append("    syouhin_cd ")
        commandTextSb.Append("    ,syouhin_mei ")
        commandTextSb.Append(" FROM ")
        commandTextSb.Append("    jhs_sys.m_syouhin WITH (READCOMMITTED) ")
        commandTextSb.Append(" WHERE ")
        commandTextSb.Append(String.Format(" souko_cd = '{0}' ", IIf(searchKoujiMode = SearchMode.KAIRYOU, "130", "140")))
        commandTextSb.Append(" ORDER BY ")
        commandTextSb.Append("    syouhin_cd")

        Dim test As String = commandTextSb.ToString()

        ' �f�[�^�̎擾
        Dim dsSyouhin As New SyouhinDataSet()
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            dsSyouhin, dsSyouhin.SyouhinTable.TableName)

        Dim syouhinDataTable As SyouhinDataSet.SyouhinTableDataTable = _
                    dsSyouhin.SyouhinTable

        If syouhinDataTable.Count <> 0 Then

            ' �󔒍s�f�[�^�̐ݒ�
            If withSpaceRow = True Then
                dt.Rows.Add(CreateRow("", "", dt))
            End If

            ' �擾�f�[�^���DataRow���쐬��DataTable�ɃZ�b�g����
            For Each row In syouhinDataTable
                If withCode = True Then
                    dt.Rows.Add(CreateRow(row.syouhin_cd + ":" + row.syouhin_mei, row.syouhin_cd, dt))
                Else
                    dt.Rows.Add(CreateRow(row.syouhin_mei, row.syouhin_cd, dt))
                End If
            Next

        End If

    End Sub
#End Region

End Class

Imports System.text
Imports System.Data.SqlClient

Public Class KameitenBikouSetteiDataAccess

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ''' <summary>
    ''' �R�l�N�V�����X�g�����O
    ''' </summary>
    ''' <remarks></remarks>
    Private connStr As String = ConnectionManager.Instance.ConnectionString

#Region "���l��ʂ̑��݃`�F�b�N"
    ''' <summary>
    ''' ���l��ʂ̑��݃`�F�b�N�����܂�
    ''' </summary>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <param name="intBikouSyubetu">���l���</param>
    ''' <param name="blnDelete">True:����f�[�^���擾�Ώۂ��珜�O False:����f�[�^��Ώ�</param>
    ''' <returns>�Y���f�[�^����</returns>
    ''' <remarks></remarks>
    Public Function ChkBikouSyubetu( _
                                      ByVal strKameitenCd As String, _
                                      ByVal intBikouSyubetu As Integer, _
                                      Optional ByVal blnDelete As Boolean = True _
                                      ) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".ChkBikouSyubetu", _
                                                    strKameitenCd, _
                                                    intBikouSyubetu, _
                                                    blnDelete _
                                                    )
        '�����̓`�F�b�N
        If strKameitenCd = "" Or IsNumeric(intBikouSyubetu) = False Then
            Return 0
        End If

        ' �p�����[�^
        Const strParamKameitenCd As String = "@KAMEITENCD"

        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append(" SELECT ")
        commandTextSb.Append("    COUNT(bs.kameiten_cd) ")
        commandTextSb.Append(" FROM ")
        commandTextSb.Append("    m_kameiten_bikou_settei bs WITH (READCOMMITTED) ")
        commandTextSb.Append(" WHERE  ")
        commandTextSb.Append("    bs.kameiten_cd = " & strParamKameitenCd)
        commandTextSb.Append(" AND  ")
        commandTextSb.Append("    bs.bikou_syubetu = " & intBikouSyubetu)

        ' �p�����[�^�֐ݒ�
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamKameitenCd, SqlDbType.VarChar, 5, strKameitenCd)}

        ' �f�[�^�̎擾
        Dim data As Object = Nothing

        ' �������s
        data = SQLHelper.ExecuteScalar(connStr, _
                                       CommandType.Text, _
                                       commandTextSb.ToString(), _
                                       commandParameters)

        ' �擾�o���Ȃ��ꍇ�A�󔒂�ԋp
        If data Is Nothing OrElse IsDBNull(data) Then
            Return 0
        End If

        Return data

    End Function
#End Region

End Class

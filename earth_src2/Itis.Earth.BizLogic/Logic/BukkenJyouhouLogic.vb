Imports Itis.Earth.DataAccess
''' <summary>������񌟍�����</summary>
''' <remarks>������񌟍��@�\��񋟂���</remarks>
''' <history>
''' <para>2009/10/27�@��(��A���V�X�e����)�@�V�K�쐬</para>
''' </history>
Public Class BukkenJyouhouLogic

    ''' <summary>�����X�������Ɖ�N���X�̃C���X�^���X���� </summary>
    Private bukkenJyouhouDataAccess As New BukkenJyouhouDataAccess

    ''' <summary> �������擾</summary>
    ''' <param name="dtParam">Param�f�[�^�Z�[�g</param>
    ''' <returns>�������̃f�[�^</returns>
    Public Function GetBukkenJyouhouInfo(ByVal dtParam As DataTable) As BukkenJyouhouDataSet.BukkenJyouhouTableDataTable
        '�f�[�^�擾
        Return bukkenJyouhouDataAccess.SelBukkenJyouhouInfo(dtParam)
    End Function

    ''' <summary>�����X���f�[�^�����擾����</summary>
    ''' <param name="dtParam">Param�f�[�^�Z�[�g</param>
    ''' <returns>�����X���f�[�^��</returns>
    Public Function GetBukkenJyouhouInfoCount(ByVal dtParam As DataTable) As Integer
        '�f�[�^�擾
        Return bukkenJyouhouDataAccess.SelBukkenJyouhouInfoCount(dtParam)
    End Function

    ''' <summary> CSV���擾</summary>
    ''' <param name="dtParam">Param�f�[�^�Z�[�g</param>
    ''' <returns>CSV���̃f�[�^</returns>
    Public Function GetBukkenJyouhouInfoCSV(ByVal dtParam As DataTable) As BukkenJyouhouDataSet.BukkenJyouhouCSVTableDataTable
        '�f�[�^�擾
        Return bukkenJyouhouDataAccess.SelBukkenJyouhouInfoCSV(dtParam)
    End Function
End Class

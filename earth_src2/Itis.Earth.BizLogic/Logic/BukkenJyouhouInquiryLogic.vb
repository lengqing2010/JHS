Imports Itis.Earth.DataAccess
''' <summary>�����X�������Ɖ��</summary>
''' <remarks>�����X�������Ɖ�@�\��񋟂���</remarks>
''' <history>
''' <para>2009/07/15�@�n���R(��A���V�X�e����)�@�V�K�쐬</para>
''' </history>
Public Class BukkenJyouhouInquiryLogic

    ''' <summary>�����X�������Ɖ�N���X�̃C���X�^���X���� </summary>
    Private bukkenJyouhouInquiryDataAccess As New BukkenJyouhouInquiryDataAccess

    ''' <summary> �������擾</summary>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <returns>�������̃f�[�^</returns>
    Public Function GetBukkenJyouhouInfo(ByVal strKameitenCd As String) As BukkenJyouhouInquiryDataSet.BukkenJyouhouInquiryDataTableDataTable
        '�f�[�^�擾
        Return bukkenJyouhouInquiryDataAccess.SelBukkenJyouhouInfo(strKameitenCd)
    End Function

    ''' <summary>
    ''' �u����v�����擾����
    ''' </summary>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <returns>�u����v�f�[�^</returns>
    ''' <hidtory>2012/03/28 �ԗ� 405721�Č��̑Ή� �ǉ�</hidtory>
    Public Function GetTorikesi(ByVal strKameitenCd As String) As Data.DataTable

        '�߂�l
        Return bukkenJyouhouInquiryDataAccess.SelTorikesi(strKameitenCd)

    End Function

End Class

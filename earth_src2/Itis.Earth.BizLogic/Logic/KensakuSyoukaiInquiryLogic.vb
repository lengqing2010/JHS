Imports Itis.Earth.DataAccess

''' <summary>�����X���������Ɖ��</summary>
''' <remarks>�����X�����Ɖ�@�\��񋟂���</remarks>
''' <history>
''' <para>2009/07/15�@�t��(��A���V�X�e����)�@�V�K�쐬</para>
''' </history>
Public Class KensakuSyoukaiInquiryLogic
    ''' <summary>�����X��񌟍��Ɖ�N���X�̃C���X�^���X���� </summary>
    Private kensakuSyoukaiInquiryDA As New KensakuSyoukaiInquiryDataAccess

    ''' <summary>�����X���f�[�^���擾����</summary>
    ''' <param name="dtParamKameitenInfo">�p�����[�^</param>
    ''' <returns>�����X���f�[�^�e�[�u��</returns>
    Public Function GetKameitenInfo(ByVal dtParamKameitenInfo As KensakuSyoukaiInquiryDataSet.Param_KameitenInfoDataTable) _
            As KensakuSyoukaiInquiryDataSet.KameitenInfoTableDataTable
        Return kensakuSyoukaiInquiryDA.SelKameitenInfo(dtParamKameitenInfo)
    End Function

    ''' <summary>�����X���f�[�^�����擾����</summary>
    ''' <param name="dtParamKameitenInfo">�p�����[�^</param>
    ''' <returns>�����X���f�[�^��</returns>
    Public Function GetKameitenInfoCount(ByVal dtParamKameitenInfo As KensakuSyoukaiInquiryDataSet.Param_KameitenInfoDataTable) _
           As Integer
        Return kensakuSyoukaiInquiryDA.SelKameitenInfoCount(dtParamKameitenInfo)
    End Function

    ''' <summary>�����X��{���CSV�f�[�^���擾����</summary>
    ''' <param name="dtParamKameitenInfo">�p�����[�^</param>
    ''' <returns>�����X��{���CSV�f�[�^�e�[�u��</returns>
    Public Function GetKihonJyouhouCsvInfo(ByVal dtParamKameitenInfo As KensakuSyoukaiInquiryDataSet.Param_KameitenInfoDataTable) _
           As Data.DataSet
        Return kensakuSyoukaiInquiryDA.SelKihonJyouhouCsvInfo(dtParamKameitenInfo)
    End Function

    ''' <summary>�����X�Z�����CSV�f�[�^���擾����</summary>
    ''' <param name="dtParamKameitenInfo">�p�����[�^</param>
    ''' <returns>�����X�Z�����CSV�f�[�^�e�[�u��</returns>
    Public Function GetJyusyoJyouhouCsvInfo(ByVal dtParamKameitenInfo As KensakuSyoukaiInquiryDataSet.Param_KameitenInfoDataTable) _
           As Data.DataSet
        Return kensakuSyoukaiInquiryDA.SelJyusyoJyouhouCsvInfo(dtParamKameitenInfo)
    End Function

    ''' <summary>�����X���ꊇ�捞���CSV�f�[�^���擾����</summary>
    ''' <returns>�����X���ꊇ�捞���CSV�f�[�^�e�[�u��</returns>
    Public Function GetKameitenJyuusyoCsvInfo(ByVal dtParamKameitenInfo As KensakuSyoukaiInquiryDataSet.Param_KameitenInfoDataTable) As Data.DataTable
        Return kensakuSyoukaiInquiryDA.SelKameitenJyuusyoCsvInfo(dtParamKameitenInfo)
    End Function

    ''' <summary>
    ''' ��������i�[��Ǘ��}�X�^���A�i�[��t�@�C���p�X���擾����
    ''' </summary>
    ''' <param name="strKbn">�敪</param>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>���F ��A���V�X�e���� 2013.03.22</history>
    Public Function GetKakunousakiFilePassJ(ByVal strKbn As String, ByVal strKameitenCd As String) As Data.DataTable
        Return kensakuSyoukaiInquiryDA.SelKakunousakiFilePassJ(strKbn, strKameitenCd)
    End Function

    ''' <summary>
    ''' �����J�[�h�i�[��Ǘ��}�X�^���A�i�[��t�@�C���p�X���擾����
    ''' </summary>
    ''' <param name="strKbn">�敪</param>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>���F ��A���V�X�e���� 2013.03.22</history>
    Public Function GetKakunousakiFilePassC(ByVal strKbn As String, ByVal strKameitenCd As String) As Data.DataTable
        Return kensakuSyoukaiInquiryDA.SelKakunousakiFilePassC(strKbn, strKameitenCd)
    End Function

End Class

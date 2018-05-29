Imports Itis.Earth.DataAccess

''' <summary>�����X�c�Ə�����������</summary>
''' <remarks>�����X�c�Ə�񌟍��@�\��񋟂���</remarks>
''' <history>
''' <para>2009/07/16�@����N(��A���V�X�e����)�@�V�K�쐬</para>
''' </history>
Public Class EigyouJyouhouInquiryLogic

    ''' <summary> �����X�c�Ə��N���X�̃C���X�^���X���� </summary>
    Private EigyouJyouhouDataAccess As New EigyouJyouhouInquiryDataAccess

    ''' <summary>
    ''' �g�D���x�����擾����B
    ''' </summary>
    ''' <returns>�g�D���x���f�[�^�e�[�u��</returns>
    Public Function GetSosikiLabelInfo() As EigyouJyouhouDataSet.sosikiLabelDataTable
        Return EigyouJyouhouDataAccess.selSosikiLevel()
    End Function
    Public Function GetSosikiLabelInfo2(ByVal strSosiki As String, ByVal strSosiki2 As String, ByVal strBusyoCd As String, ByVal strBusyoCd2 As String, ByVal strKbn As String) As EigyouJyouhouDataSet.sosikiLabelDataTable
        Return EigyouJyouhouDataAccess.selSosikiLevel2(strSosiki, strSosiki2, strBusyoCd, strBusyoCd2, strKbn)
    End Function
    ''' <summary>
    ''' �����R�[�h�Ɩ��̂��擾����B
    ''' </summary>
    ''' <param name="strSosikiLevel">�g�D���x��</param>
    ''' <returns>�������f�[�^�e�[�u��</returns>
    Public Function GetbusyoCdInfo(ByVal strSosikiLevel As String) As EigyouJyouhouDataSet.busyoCdDataTable
        Return EigyouJyouhouDataAccess.selBusyoCd(strSosikiLevel)
    End Function
    Public Function GetbusyoCdInfo2(ByVal strSosikiLevel As String, ByVal strBusyoCd As String, ByVal strSansyouBusyoCd As String) As EigyouJyouhouDataSet.busyoCdDataTable
        Return EigyouJyouhouDataAccess.selBusyoCd2(strSosikiLevel, strBusyoCd, strSansyouBusyoCd)
    End Function

    ''' <summary>
    ''' ���O�C�����[�U�[�̉c�ƃ}���敪���擾����B
    ''' </summary>
    ''' <param name="strUserId">���O�C�����[�U�[ID</param>
    ''' <returns>���O�C�����[�U�[�̉c�ƃ}���敪�f�[�^�e�[�u��</returns>
    Public Function GetEigyouManKbnInfo(ByVal strUserId As String) As EigyouJyouhouDataSet.eigyouManKbnDataTable
        Return EigyouJyouhouDataAccess.selEigyouManKbn(strUserId)
    End Function

    ''' <summary>
    ''' �����X�c�Ə��f�[�^�������擾����B
    ''' </summary>
    ''' <param name="dtParamEigyouInfo">���������e�[�u��</param>
    ''' <returns>�����X�c�Ə��f�[�^����</returns>
    Public Function GetEigyouJyouhouCountInfo(ByVal dtParamEigyouInfo As EigyouJyouhouDataSet.paramEigyouJyouhouDataTable _
                                 , ByVal chkBusyoCd As Boolean) As Integer
        Return EigyouJyouhouDataAccess.selEigyouJyouhouCount(dtParamEigyouInfo, chkBusyoCd)
    End Function

    ''' <summary>
    ''' �����X�c�Ə����擾����B
    ''' </summary>
    ''' <param name="dtParamEigyouInfo">���������e�[�u��</param>
    ''' <returns>�����X�c�Ə��f�[�^�e�[�u��</returns>
    Public Function GetEigyouJyouhouInfo(ByVal dtParamEigyouInfo As EigyouJyouhouDataSet.paramEigyouJyouhouDataTable _
                                    , ByVal chkBusyoCd As Boolean) As EigyouJyouhouDataSet.eigyouJyouhouDataTable
        Return EigyouJyouhouDataAccess.selEigyouJyouhou(dtParamEigyouInfo, chkBusyoCd)
    End Function



End Class

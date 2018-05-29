Imports Itis.Earth.DataAccess

Public Class SeikyusyoFcwOutputLogic

    ''' <summary>
    ''' ���������[�o�̓f�[�^��������
    ''' </summary>
    ''' <param name="strSeikyusyo_no">������R�[�h</param>
    ''' <returns>���������[�o�̓f�[�^</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 2010/07/19�@������(��A)�@�V�K�쐬
    ''' </history>
    Public Function GetSeikyusyoData(ByVal strSeikyusyo_no As String) As Data.DataTable

        '�f�[�^�擾
        Return (New SeikyusyoFcwOutputDataAccess).SelSeikyusyoFcwOutputData(strSeikyusyo_no)

    End Function

    ''' <summary>
    ''' �����揑No�f�[�^���擾����
    ''' </summary>
    ''' <param name="strSeikyusyo_no">�����揑No</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 2010/07/19�@������(��A)�@�V�K�쐬
    ''' </history>
    Public Function GetSeikyusyoNoData(ByVal strSeikyusyo_no As String) As Data.DataTable

        '�f�[�^�擾
        Return (New SeikyusyoFcwOutputDataAccess).SelSeikyusyoNoData(strSeikyusyo_no)

    End Function

    ''' <summary>
    ''' ���O�C�����[�U�[�����擾����B
    ''' </summary>
    ''' <param name="loginID">���O�C�����[�U�[�R�[�h</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 2010/07/19�@�^����(��A)�@�V�K�쐬
    ''' </history>
    Public Function GetLoginUserName(ByVal loginID As String) As String

        '�f�[�^�擾
        Return (New SeikyusyoFcwOutputDataAccess).SelLoginUserName(loginID)

    End Function

    ''' <summary>
    ''' ��ވ�̓X�����擾����
    ''' </summary>
    ''' <param name="UA_kbn">�敪</param>
    ''' <param name="UA_bangou">�ԍ�</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 2010/07/19�@�^����(��A)�@�V�K�쐬
    ''' </history>
    Public Function GetHimodekeSyurui_1_tenmei(ByVal UA_kbn As String, ByVal UA_bangou As String) _
As Data.DataTable

        '�f�[�^�擾
        Return (New SeikyusyoFcwOutputDataAccess).SelHimodekeSyurui_1_tenmei(UA_kbn, UA_bangou)

    End Function

    ''' <summary>
    ''' ��ޓ�̓X�����擾����
    ''' </summary>
    ''' <param name="UA_himodukecd_ten_cd">�X�R�[�h</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 2010/07/19�@�^����(��A)�@�V�K�쐬
    ''' </history>
    Public Function GetHimodekeSyurui_2_tenmei(ByVal UA_himodukecd_ten_cd As String) _
As Data.DataTable

        '�f�[�^�擾
        Return (New SeikyusyoFcwOutputDataAccess).SelHimodekeSyurui_2_tenmei(UA_himodukecd_ten_cd)

    End Function

    ''' <summary>
    ''' ��ގO�̓X�����擾����
    ''' </summary>
    ''' <param name="UA_himodukecd_ten_cd">�X�R�[�h</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 2010/07/19�@�^����(��A)�@�V�K�쐬
    ''' </history>
    Public Function GetHimodekeSyurui_3_tenmei(ByVal UA_himodukecd_ten_cd As String) _
As Data.DataTable

        '�f�[�^�擾
        Return (New SeikyusyoFcwOutputDataAccess).SelHimodekeSyurui_3_tenmei(UA_himodukecd_ten_cd)

    End Function


End Class

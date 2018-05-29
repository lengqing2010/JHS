Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Microsoft.VisualBasic
Imports System.Web
Imports system.Web.HttpContext
Imports System.Configuration
Imports Lixil.JHS_EKKS.DataAccess

''' <summary>
''' �e��W�v
''' </summary>
''' <remarks></remarks>
Public Class KakusyuSyukeiInquiryBC

    Private kakusyuSyukeiInquiryDA As New DataAccess.KakusyuSyukeiInquiryDA

    ''' <summary>
    ''' ����ToList���擾����
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2012/12/04�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetTukinamiListData() As DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name)

        Return kakusyuSyukeiInquiryDA.SelTukinamiListData()

    End Function

    ''' <summary>
    ''' ��ʖ��ׂ��擾����
    ''' </summary>
    ''' <param name="strTodouhukenCd">�s���{���R�[�h</param>
    ''' <param name="strSitenCd">�x�X�R�[�h</param>
    ''' <param name="strEigyousyoBusyoCd">�c�Ə��R�[�h</param>
    ''' <param name="strKeiretuCd">�n��R�[�h</param>
    ''' <param name="strEigyouTantousyaId">�c�ƃ}���R�[�h</param>
    ''' <param name="strKameitenCd">�o�^���Ǝ҃R�[�h</param>
    ''' <param name="strNendo">�N�x</param>
    ''' <param name="intBegin">����</param>
    ''' <param name="intEnd">�I���</param>
    ''' <returns>Data.DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/12/27�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetKakusyuSyukeiData(ByVal strSitenCd As String, _
                                         ByVal strTodouhukenCd As String, _
                                         ByVal strEigyousyoBusyoCd As String, _
                                         ByVal strKeiretuCd As String, _
                                         ByVal strEigyouTantousyaId As String, _
                                         ByVal strKameitenCd As String, _
                                         ByVal strNendo As String, _
                                         ByVal intBegin As Integer, _
                                         ByVal intEnd As Integer, _
                                         ByVal strEigyouKbn As String) As DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                            strSitenCd, _
                                                                                            strTodouhukenCd, _
                                                                                            strEigyousyoBusyoCd, _
                                                                                            strKeiretuCd, _
                                                                                            strEigyouTantousyaId, _
                                                                                            strKameitenCd, _
                                                                                            strNendo, _
                                                                                            intBegin, _
                                                                                            intEnd, _
                                                                                            strEigyouKbn)


        Return kakusyuSyukeiInquiryDA.SelKakusyuSyukeiData(strSitenCd, _
                                                           strTodouhukenCd, _
                                                           strEigyousyoBusyoCd, _
                                                           strKeiretuCd, _
                                                           strEigyouTantousyaId, _
                                                           strKameitenCd, _
                                                           strNendo, _
                                                           intBegin, _
                                                           intEnd, _
                                                           strEigyouKbn)

    End Function

    ''' <summary>
    ''' ��ʖ��ׂ��擾����
    ''' </summary>
    ''' <param name="strSitenCd">�x�X�R�[�h</param>
    ''' <param name="strNendo">�N�x</param>
    ''' <param name="intBegin">����</param>
    ''' <param name="intEnd">�I���</param>
    ''' <returns>Data.DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/12/27�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetKakusyuSyukeiFCData(ByVal strSitenCd As String, _
                                           ByVal strNendo As String, _
                                           ByVal intBegin As Integer, _
                                           ByVal intEnd As Integer) As DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                            strSitenCd, _
                                                                                            strNendo, _
                                                                                            intBegin, _
                                                                                            intEnd)


        Return kakusyuSyukeiInquiryDA.SelKakusyuSyukeiFCData(strSitenCd, _
                                                             strNendo, _
                                                             intBegin, _
                                                             intEnd)

    End Function


    Public Function GetKakusyuSyukeiSubeteData(ByVal strSitenCd As String, _
                                               ByVal strNendo As String, _
                                               ByVal intBegin As Integer, _
                                               ByVal intEnd As Integer, _
                                               ByVal strEigyouKbn As String) As Data.DataTable


        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                            strSitenCd, _
                                                                                            strNendo, _
                                                                                            intBegin, _
                                                                                            intEnd, _
                                                                                            strEigyouKbn)

        Return kakusyuSyukeiInquiryDA.SelKakusyuSyukeiSubeteData(strSitenCd, _
                                                                 strNendo, _
                                                                 intBegin, _
                                                                 intEnd, _
                                                                 strEigyouKbn)

    End Function

End Class

Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports System.Transactions
Imports Lixil.JHS_EKKS.DataAccess

''' <summary>
''' �n��R�[�h����POPUP
''' </summary>
''' <remarks></remarks>
Public Class EigyousyoSearchBC

    Private eigyousyoSearchDA As New DataAccess.EigyousyoSearchDA

    ''' <summary>
    ''' �u�����Ǘ��}�X�^�v�e�[�v�����A�c�Ə������擾����
    ''' </summary>
    ''' <param name="strRows">�����������</param>
    ''' <param name="strEigyousyoMei">�c�Ə���</param>
    ''' <param name="blnTorikesi">���</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/11/17�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetEigyousyoMei(ByVal strRows As String, _
                                     ByVal strEigyousyoMei As String, _
                                     ByVal blnTorikesi As Boolean, Optional ByVal blnAimai As Boolean = True) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strRows, _
                                                                                          strEigyousyoMei, _
                                                                                          blnTorikesi)

        Return eigyousyoSearchDA.SelEigyousyoMei(strRows, strEigyousyoMei, blnTorikesi, blnAimai)

    End Function

    ''' <summary>
    ''' ���������f�[�^�������擾����
    ''' </summary>
    ''' <param name="strEigyousyoMei">�c�Ə���</param>
    ''' <param name="blnTorikesi">���</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/11/17�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetEigyousyoMeiCount(ByVal strEigyousyoMei As String, ByVal blnTorikesi As Boolean) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strEigyousyoMei, _
                                                                                          blnTorikesi)

        Return eigyousyoSearchDA.SelEigyousyoMeiCount(strEigyousyoMei, blnTorikesi)

    End Function

End Class

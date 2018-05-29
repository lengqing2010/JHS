Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports System.Transactions
Imports Lixil.JHS_EKKS.DataAccess
Public Class EigyouManSearchBC
    Private egyouManSearchDA As New DataAccess.EigyouManSearchDA

    ''' <summary>
    ''' ���[�U�[�f�[�^���擾����
    ''' </summary>
    ''' <param name="strRows">�����������</param>
    ''' <param name="strUserId">���[�U�[ID</param>
    ''' <param name="strUserName">���[�U�[��</param>
    ''' <param name="blnTorikesi" >���</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/11/15�@��~��(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetUserInfo(ByVal strRows As String, _
                                     ByVal strUserId As String, _
                                     ByVal strUserName As String, _
                                     ByVal blnTorikesi As Boolean, Optional ByVal blnAimai As Boolean = True) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strRows, _
                                                                                          strUserId, _
                                                                                          strUserName, blnAimai)

        Return egyouManSearchDA.SelUserInfo(strRows, strUserId, strUserName, blnTorikesi, blnAimai)

    End Function

    ''' <summary>
    ''' ���������f�[�^�������擾����
    ''' </summary>
    ''' <param name="strUserId">���[�U�[ID</param>
    ''' <param name="strUserName">���[�U�[��</param>
    ''' <param name="blnTorikesi" >���</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/11/16�@��~��(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetUserCount(ByVal strUserId As String, _
                                       ByVal strUserName As String, ByVal blnTorikesi As Boolean) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strUserId, _
                                                                                          strUserName)

        Return egyouManSearchDA.SelUserCount(strUserId, strUserName, blnTorikesi)

    End Function

End Class

Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports System.Transactions
Imports Lixil.JHS_EKKS.DataAccess

''' <summary>
''' �n��R�[�h����POPUP
''' </summary>
''' <remarks></remarks>
Public Class KeiretuSearchBC

    Private keiretuSearchDA As New DataAccess.KeiretuSearchDA

    ''' <summary>
    ''' �u�n��}�X�^�v�e�[�v�����A�n������擾����
    ''' </summary>
    ''' <param name="strRows">�����������</param>
    ''' <param name="strKeiretuCd">�n��R�[�h</param>
    ''' <param name="strKeiretuMei">�n��</param>
    ''' <param name="blnTorikesi">���</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/11/15�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetKiretuJyouhou(ByVal strRows As String, _
                                     ByVal strKeiretuCd As String, _
                                     ByVal strKeiretuMei As String, _
                                     ByVal blnTorikesi As Boolean, Optional ByVal blnAimai As Boolean = True) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strRows, _
                                                                                          strKeiretuCd, _
                                                                                          strKeiretuMei, _
                                                                                          blnTorikesi)

        Return keiretuSearchDA.SelKiretuJyouhou(strRows, strKeiretuCd, strKeiretuMei, blnTorikesi, blnAimai)

    End Function

    ''' <summary>
    ''' ���������f�[�^�������擾����
    ''' </summary>
    ''' <param name="strKeiretuCd">�n��R�[�h</param>
    ''' <param name="strKeiretuMei">�n��</param>
    ''' <param name="blnTorikesi">���</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/11/16�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetKiretuJyouhouCount(ByVal strKeiretuCd As String, _
                                          ByVal strKeiretuMei As String, _
                                          ByVal blnTorikesi As Boolean) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strKeiretuCd, _
                                                                                          strKeiretuMei, _
                                                                                          blnTorikesi)

        Return keiretuSearchDA.SelKiretuJyouhouCount(strKeiretuCd, strKeiretuMei, blnTorikesi)

    End Function

End Class

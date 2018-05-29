Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports System.Transactions
Imports Lixil.JHS_EKKS.DataAccess
Public Class SitenSearchBC
    Private sitenSearchDA As New DataAccess.SitenSearchDA

    ''' <summary>
    ''' �����Ǘ��}�X�^�̃f�[�^���擾����
    ''' </summary>
    ''' <param name="strRows">�f�[�^��</param>
    ''' <param name="strBusyoMei">������</param>
    ''' <param name="blnTorikesi" >���</param> 
    ''' <returns>�����Ǘ��}�X�^�f�[�^</returns>
    ''' <history>2012/11/17�@��~��(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetBusyoKanri(ByVal strRows As String, _
                                     ByVal strBusyoMei As String, ByVal blnTorikesi As Boolean, Optional ByVal blnAimai As Boolean = True, Optional ByVal strBusyoCD As String = "") As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strRows, _
                                                                                          strBusyoMei, _
                                                                                          blnTorikesi, _
                                                                                          blnAimai)

        Return sitenSearchDA.SelBusyoKanri(strRows, strBusyoMei, blnTorikesi, blnAimai, strBusyoCD)

    End Function

    ''' <summary>
    '''�����Ǘ��}�X�^�̃f�[�^�������擾���� 
    ''' </summary>
    ''' <param name="strBusyoMei">������</param>
    ''' <param name="blnTorikesi" >���</param>
    ''' <returns>�����Ǘ��}�X�^�f�[�^</returns>
    ''' <history>2012/11/17�@��~��(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetDataCount(ByVal strBusyoMei As String, ByVal blnTorikesi As Boolean) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strBusyoMei)

        Return sitenSearchDA.SelDataCount(strBusyoMei, blnTorikesi)

    End Function
End Class

Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports System.Transactions
Imports Lixil.JHS_EKKS.DataAccess

''' <summary>
''' ��ʌ���
''' </summary>
''' <remarks></remarks>
Public Class SyubetuSearchBC

    Private syubetuSearchDA As New DataAccess.SyubetuSearchDA

    ''' <summary>
    ''' ��ʏ�����������
    ''' </summary>
    ''' <param name="intRows">�����������</param>
    ''' <param name="code">��ʃR�[�h</param>
    ''' <param name="mei">��ʖ�</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/03�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelSyubetu(ByVal intRows As String, _
                               ByVal code As String, _
                               ByVal mei As String) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          intRows, _
                                                                                          code, _
                                                                                          mei)

        Return syubetuSearchDA.SelSyubetu(intRows, code, mei)

    End Function

    ''' <summary>
    ''' ���������f�[�^�������擾����
    ''' </summary>
    ''' <param name="code">��ʃR�[�h</param>
    ''' <param name="mei">��ʖ�</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/03�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelSyubetuCount(ByVal code As String, _
                                    ByVal mei As String) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          code, _
                                                                                          mei)

        Return syubetuSearchDA.SelSyubetuCount(code, mei)

    End Function

End Class

Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports System.Transactions
Imports Lixil.JHS_EKKS.DataAccess

''' <summary>
''' �s���{������POPUP
''' </summary>
''' <remarks></remarks>
Public Class TodoufukenSearchBC

    Private todoufukenSearchDA As New DataAccess.TodoufukenSearchDA

    ''' <summary>
    ''' �u�s���{�����v�̌�����������
    ''' </summary>
    ''' <param name="strRows">�����������</param>
    ''' <param name="strTodoufukenMei">�s���{����</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    '''  <history>2012/11/19�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetTodoufukenMei(ByVal strRows As String, _
                                     ByVal strTodoufukenMei As String, _
                                     Optional ByVal blnAimai As Boolean = True) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strRows, _
                                                                                          strTodoufukenMei)

        Return todoufukenSearchDA.SelTodoufukenMei(strRows, strTodoufukenMei)

    End Function

    ''' <summary>
    ''' ���������f�[�^�������擾����
    ''' </summary>
    ''' <param name="strTodoufukenMei">�s���{����</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/11/19�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetKiretuJyouhouCount(ByVal strTodoufukenMei As String) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strTodoufukenMei)

        Return todoufukenSearchDA.SelTodoufukenMeiCount(strTodoufukenMei)

    End Function

End Class

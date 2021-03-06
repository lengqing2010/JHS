Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports System.Transactions
Imports Lixil.JHS_EKKS.DataAccess

''' <summary>
''' s¹{§õPOPUP
''' </summary>
''' <remarks></remarks>
Public Class TodoufukenSearchBC

    Private todoufukenSearchDA As New DataAccess.TodoufukenSearchDA

    ''' <summary>
    ''' us¹{§¼vÌõ·é
    ''' </summary>
    ''' <param name="strRows">õãÀ</param>
    ''' <param name="strTodoufukenMei">s¹{§¼</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    '''  <history>2012/11/19@F(åAîñVXe)@VKì¬</history>
    Public Function GetTodoufukenMei(ByVal strRows As String, _
                                     ByVal strTodoufukenMei As String, _
                                     Optional ByVal blnAimai As Boolean = True) As Data.DataTable

        'EMABáQÎîñÌi[
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strRows, _
                                                                                          strTodoufukenMei)

        Return todoufukenSearchDA.SelTodoufukenMei(strRows, strTodoufukenMei)

    End Function

    ''' <summary>
    ''' õµ½f[^ðæ¾·é
    ''' </summary>
    ''' <param name="strTodoufukenMei">s¹{§¼</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/11/19@F(åAîñVXe)@VKì¬</history>
    Public Function GetKiretuJyouhouCount(ByVal strTodoufukenMei As String) As Data.DataTable

        'EMABáQÎîñÌi[
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strTodoufukenMei)

        Return todoufukenSearchDA.SelTodoufukenMeiCount(strTodoufukenMei)

    End Function

End Class

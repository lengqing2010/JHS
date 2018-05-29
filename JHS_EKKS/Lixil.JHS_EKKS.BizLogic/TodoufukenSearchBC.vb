Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports System.Transactions
Imports Lixil.JHS_EKKS.DataAccess

''' <summary>
''' “s“¹•{Œ§ŒŸõPOPUP
''' </summary>
''' <remarks></remarks>
Public Class TodoufukenSearchBC

    Private todoufukenSearchDA As New DataAccess.TodoufukenSearchDA

    ''' <summary>
    ''' u“s“¹•{Œ§–¼v‚ÌŒŸõˆ—‚·‚é
    ''' </summary>
    ''' <param name="strRows">ŒŸõãŒÀŒ”</param>
    ''' <param name="strTodoufukenMei">“s“¹•{Œ§–¼</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    '''  <history>2012/11/19@—›‰F(‘å˜Aî•ñƒVƒXƒeƒ€•”)@V‹Kì¬</history>
    Public Function GetTodoufukenMei(ByVal strRows As String, _
                                     ByVal strTodoufukenMei As String, _
                                     Optional ByVal blnAimai As Boolean = True) As Data.DataTable

        'EMABáŠQ‘Î‰î•ñ‚ÌŠi”[ˆ—
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strRows, _
                                                                                          strTodoufukenMei)

        Return todoufukenSearchDA.SelTodoufukenMei(strRows, strTodoufukenMei)

    End Function

    ''' <summary>
    ''' ŒŸõ‚µ‚½ƒf[ƒ^Œ”‚ğæ“¾‚·‚é
    ''' </summary>
    ''' <param name="strTodoufukenMei">“s“¹•{Œ§–¼</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/11/19@—›‰F(‘å˜Aî•ñƒVƒXƒeƒ€•”)@V‹Kì¬</history>
    Public Function GetKiretuJyouhouCount(ByVal strTodoufukenMei As String) As Data.DataTable

        'EMABáŠQ‘Î‰î•ñ‚ÌŠi”[ˆ—
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strTodoufukenMei)

        Return todoufukenSearchDA.SelTodoufukenMeiCount(strTodoufukenMei)

    End Function

End Class

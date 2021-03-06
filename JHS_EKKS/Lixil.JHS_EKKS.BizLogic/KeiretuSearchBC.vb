Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports System.Transactions
Imports Lixil.JHS_EKKS.DataAccess

''' <summary>
''' nñR[hõPOPUP
''' </summary>
''' <remarks></remarks>
Public Class KeiretuSearchBC

    Private keiretuSearchDA As New DataAccess.KeiretuSearchDA

    ''' <summary>
    ''' unñ}X^ve[væèAnñîñðæ¾·é
    ''' </summary>
    ''' <param name="strRows">õãÀ</param>
    ''' <param name="strKeiretuCd">nñR[h</param>
    ''' <param name="strKeiretuMei">nñ¼</param>
    ''' <param name="blnTorikesi">æÁ</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/11/15@F(åAîñVXe)@VKì¬</history>
    Public Function GetKiretuJyouhou(ByVal strRows As String, _
                                     ByVal strKeiretuCd As String, _
                                     ByVal strKeiretuMei As String, _
                                     ByVal blnTorikesi As Boolean, Optional ByVal blnAimai As Boolean = True) As Data.DataTable

        'EMABáQÎîñÌi[
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strRows, _
                                                                                          strKeiretuCd, _
                                                                                          strKeiretuMei, _
                                                                                          blnTorikesi)

        Return keiretuSearchDA.SelKiretuJyouhou(strRows, strKeiretuCd, strKeiretuMei, blnTorikesi, blnAimai)

    End Function

    ''' <summary>
    ''' õµ½f[^ðæ¾·é
    ''' </summary>
    ''' <param name="strKeiretuCd">nñR[h</param>
    ''' <param name="strKeiretuMei">nñ¼</param>
    ''' <param name="blnTorikesi">æÁ</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/11/16@F(åAîñVXe)@VKì¬</history>
    Public Function GetKiretuJyouhouCount(ByVal strKeiretuCd As String, _
                                          ByVal strKeiretuMei As String, _
                                          ByVal blnTorikesi As Boolean) As Data.DataTable

        'EMABáQÎîñÌi[
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strKeiretuCd, _
                                                                                          strKeiretuMei, _
                                                                                          blnTorikesi)

        Return keiretuSearchDA.SelKiretuJyouhouCount(strKeiretuCd, strKeiretuMei, blnTorikesi)

    End Function

End Class

Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports System.Transactions
Imports Lixil.JHS_EKKS.DataAccess


''' <summary>
''' væÇ_Á¿X@õPOPUP
''' </summary>
''' <remarks></remarks>
Public Class KeikakuKanriKameitenSearchBC

    Private KeikakuKanriKameitenSearchDA As New DataAccess.KeikakuKanriKameitenSearchDA

    ''' <summary>
    ''' uÁ¿XR[hvAuÁ¿X¼vAus¹{§¼vÌõ·é
    ''' </summary>
    ''' <param name="strRows">õãÀ</param>
    ''' <param name="strYear">Nx</param>
    ''' <param name="strKameitenCd">Á¿XR[h</param>
    ''' <param name="strKameitenMei">Á¿X¼</param>
    ''' <param name="blnTorikesi">æÁ</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/11/19@F(åAîñVXe)@VKì¬</history>
    Public Function GetKeikakuKanriKameiten(ByVal strRows As String, _
                                            ByVal strYear As String, _
                                            ByVal strKameitenCd As String, _
                                            ByVal strKameitenMei As String, _
                                            ByVal blnTorikesi As Boolean, Optional ByVal blnAimai As Boolean = True) As Data.DataTable

        'EMABáQÎîñÌi[
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strRows, _
                                                                                          strYear, _
                                                                                          strKameitenCd, _
                                                                                          strKameitenMei, _
                                                                                          blnTorikesi)

        Return KeikakuKanriKameitenSearchDA.SelKeikakuKanriKameiten(strRows, strYear, strKameitenCd, strKameitenMei, blnTorikesi, blnAimai)

    End Function

    ''' <summary>
    ''' õµ½f[^ðæ¾·é
    ''' </summary>
    ''' <param name="strYear">Nx</param>
    ''' <param name="strKameitenCd">Á¿XR[h</param>
    ''' <param name="strKameitenMei">Á¿X¼</param>
    ''' <param name="blnTorikesi">æÁ</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/11/19@F(åAîñVXe)@VKì¬</history>
    Public Function GetKeikakuKanriKameitenCount(ByVal strYear As String, _
                                                 ByVal strKameitenCd As String, _
                                                 ByVal strKameitenMei As String, _
                                                 ByVal blnTorikesi As Boolean) As Data.DataTable

        'EMABáQÎîñÌi[
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strYear, _
                                                                                          strKameitenCd, _
                                                                                          strKameitenMei, _
                                                                                          blnTorikesi)

        Return KeikakuKanriKameitenSearchDA.SelKeikakuKanriKameitenCount(strYear, strKameitenCd, strKameitenMei, blnTorikesi)

    End Function

End Class

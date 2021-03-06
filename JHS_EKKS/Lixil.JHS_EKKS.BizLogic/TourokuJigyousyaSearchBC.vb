Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports System.Transactions
Imports Lixil.JHS_EKKS.DataAccess

''' <summary>
''' s¹{§õPOPUP
''' </summary>
''' <remarks></remarks>
Public Class TourokuJigyousyaSearchBC

    Private tourokuJigyousyaSearchDA As New DataAccess.TourokuJigyousyaSearchDA

    ''' <summary>
    ''' uÁ¿XR[hvAuÁ¿X¼vAus¹{§¼vÆuÁ¿XJi¼vÌõ·é
    ''' </summary>
    ''' <param name="strRows">õãÀ</param>
    ''' <param name="strKameitenCd">Á¿XR[h</param>
    ''' <param name="strKameitenMei">Á¿X¼</param>
    ''' <param name="blnTorikesi">æÁ</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/11/19@F(åAîñVXe)@VKì¬</history>
    Public Function GetTourokuJigyousya(ByVal strRows As String, _
                                     ByVal strKameitenCd As String, _
                                     ByVal strKameitenMei As String, _
                                     ByVal blnTorikesi As Boolean, Optional ByVal blnAimai As Boolean = True) As Data.DataTable

        'EMABáQÎîñÌi[
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strRows, _
                                                                                          strKameitenCd, _
                                                                                          strKameitenMei, _
                                                                                          blnTorikesi)

        Return tourokuJigyousyaSearchDA.SelTourokuJigyousya(strRows, strKameitenCd, strKameitenMei, blnTorikesi, blnAimai)

    End Function

    ''' <summary>
    ''' õµ½f[^ðæ¾·é
    ''' </summary>
    ''' <param name="strKameitenCd">Á¿XR[h</param>
    ''' <param name="strKameitenMei">Á¿X¼</param>
    ''' <param name="blnTorikesi">æÁ</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/11/19@F(åAîñVXe)@VKì¬</history>
    Public Function GetTourokuJigyousyaCount(ByVal strKameitenCd As String, _
                                          ByVal strKameitenMei As String, _
                                          ByVal blnTorikesi As Boolean) As Data.DataTable

        'EMABáQÎîñÌi[
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strKameitenCd, _
                                                                                          strKameitenMei, _
                                                                                          blnTorikesi)

        Return tourokuJigyousyaSearchDA.SelTourokuJigyousyaCount(strKameitenCd, strKameitenMei, blnTorikesi)

    End Function

End Class

Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports System.Transactions
Imports Lixil.JHS_EKKS.DataAccess

Public Class KeikaikuKanriKameitenBikouInquiryBC

    Private keikaikuKanriKameitenBikouInquiryDA As New DataAccess.KeikaikuKanriKameitenBikouInquiryDA

    ''' <summary>
    ''' Á¿Xõlîñæ¾
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/02@Ô´(åAîñVXe)@VKì¬</history>
    Public Function GetBikouInfo(ByVal strKameitenCd As String) As Data.DataTable

        'EMABáQÎîñÌi[
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                strKameitenCd)

        Return keikaikuKanriKameitenBikouInquiryDA.SelBikouInfo(strKameitenCd)

    End Function

    ''' <summary>
    ''' Á¿XõlXVúæ¾
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/02@Ô´(åAîñVXe)@VKì¬</history>
    Public Function GetKameitenBikouMaxUpdTime(ByVal strKameitenCd As String) As Data.DataTable

        'EMABáQÎîñÌi[
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                strKameitenCd)

        Return keikaikuKanriKameitenBikouInquiryDA.SelKameitenBikouMaxUpdTime(strKameitenCd)

    End Function
        ''' <summary>
        ''' Á¿XíÊæ¾
        ''' </summary>
        ''' <param name="code"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
    Public Function Getkameitensyubetu(ByVal code As String) As String

        'EMABáQÎîñÌi[
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                code)

        Return keikaikuKanriKameitenBikouInquiryDA.Selkameitensyubetu(code)

    End Function

    ''' <summary>
    ''' õlÇÁ
    ''' </summary>
    ''' <param name="dicPrm">ÇÁf[^</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetInsBikou(ByVal dicPrm As Dictionary(Of String, String)) As Boolean

        'EMABáQÎîñÌi[
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                dicPrm)

        Return keikaikuKanriKameitenBikouInquiryDA.InsBikou(dicPrm)

    End Function

    ''' <summary>
    ''' õlXV
    ''' </summary>
    ''' <param name="dicPrm">ÇÁf[^</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetUpdBikou(ByVal dicPrm As Dictionary(Of String, String)) As Boolean

        'EMABáQÎîñÌi[
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                dicPrm)

        Return keikaikuKanriKameitenBikouInquiryDA.UpdBikou(dicPrm)

    End Function

    ''' <summary>
    ''' õlí
    ''' </summary>
    ''' <param name="dicPrm">ÇÁf[^</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetDelBikou(ByVal dicPrm As Dictionary(Of String, String)) As Boolean

        'EMABáQÎîñÌi[
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                dicPrm)

        Return keikaikuKanriKameitenBikouInquiryDA.DelBikou(dicPrm)

    End Function

End Class

Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports System.Transactions
Imports Lixil.JHS_EKKS.DataAccess
Imports Lixil.JHS_EKKS.Utilities
Public Class KeikakuKanriSearchListBC
    Private KeikakuKanriSearchListDA As New KeikakuKanriSearchListDA
    Public Function GetKeikakuKanriData(ByVal KeikakuKanriRecord As KeikakuKanriRecord, ByVal selectKbn As KeikakuKanriRecord.selectKbn, Optional ByVal blnCsv As Boolean = False) As DataTable
        'EMABáŠQ‘Î‰î•ñ‚ÌŠi”[ˆ—
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          KeikakuKanriRecord, blnCsv)
        Return KeikakuKanriSearchListDA.SelKeikakuKanriData(KeikakuKanriRecord, selectKbn, blnCsv)
    End Function
    Public Function GetSitenbetuTukiData(ByVal KeikakuKanriRecord As KeikakuKanriRecord) As DataTable
        'EMABáŠQ‘Î‰î•ñ‚ÌŠi”[ˆ—
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          KeikakuKanriRecord)
        Return KeikakuKanriSearchListDA.SelSitenbetuTukiData(KeikakuKanriRecord)
    End Function
    Public Function SetKeikakuKanri(ByVal strKousin As String, ByVal strKousinId As String, ByVal blnKakutei As Boolean) As Integer
        'EMABáŠQ‘Î‰î•ñ‚ÌŠi”[ˆ—
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strKousin, strKousinId, blnKakutei)
        Return KeikakuKanriSearchListDA.UpdKeikakuKanri(strKousin, strKousinId, blnKakutei)
    End Function
    Public Function GetCount() As Integer
        'EMABáŠQ‘Î‰î•ñ‚ÌŠi”[ˆ—
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name)
        Return KeikakuKanriSearchListDA.SelCount()
    End Function
 
End Class

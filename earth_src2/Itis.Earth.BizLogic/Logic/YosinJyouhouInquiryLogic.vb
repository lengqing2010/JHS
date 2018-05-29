Imports Itis.Earth.DataAccess
Public Class YosinJyouhouInquiryLogic
    ''' <summary>
    ''' 加盟店与信情報を取得する
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 2009/07/11　王穎(大連開発)　新規作成　P-*****
    ''' </history>
    Public Function GetYosinJyouhou(ByVal strKameitenCd As String) As DataAccess.YosinJyouhouDataSet. _
                                                                            YosinJyouhouTableDataTable
        Dim YosinJyouhouDA As New DataAccess.YosinJyouhouDataAccess
        Return YosinJyouhouDA.SelYosinJyouhou(strKameitenCd)

    End Function
End Class

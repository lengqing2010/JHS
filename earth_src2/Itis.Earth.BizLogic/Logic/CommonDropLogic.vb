Imports Itis.Earth.DataAccess

Public Class CommonDropLogic

    ''' <summary> メインメニュークラスのインスタンス生成 </summary>
    Private CommonDropDA As New CommonDropDataAccess
    ''' <summary> 共通データを取得する</summary>
    Public Function GetCommonDropInfo(ByVal kbn As Integer) As CommonDropDataSet.DropTableDataTable
        Return CommonDropDA.SelCommonInfo(kbn)
    End Function

End Class

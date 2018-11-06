Imports Itis.Earth.DataAccess

Public Class SalesforceLogic

    ''' <summary> メインメニュークラスのインスタンス生成 </summary>
    Private SalesforceDataAccess As New SalesforceDataAccess
    ''' <summary> 共通データを取得する</summary>
    Public Function GetSalesforceHikasseiFlg(ByVal strKameitenCd As String) As String
        Return SalesforceDataAccess.GetSalesforceHikasseiFlg(strKameitenCd)
    End Function

    Public Function GetSalesforceHikasseiFlgByKbn(ByVal kbn As String) As String
        Return SalesforceDataAccess.GetSalesforceHikasseiFlgByKbn(kbn)
    End Function


End Class

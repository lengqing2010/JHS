Imports Itis.Earth.BizLogic

Public Class Salesforce

    Private SalesforceLogic As New SalesforceLogic
    Public Function GetSalesforceHikasseiFlg(ByVal strKameitenCd As String)
        Return SalesforceLogic.GetSalesforceHikasseiFlg(strKameitenCd)
    End Function

    Public Function GetSalesforceHikasseiFlgByKbn(ByVal kbn As String)
        Return SalesforceLogic.GetSalesforceHikasseiFlgByKbn(kbn)
    End Function

End Class

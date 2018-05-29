Imports Itis.Earth.DataAccess
Imports Itis.Earth.BizLogic
Public Class HanteiKojiSyubetuLogic
    Private HanteiKojiSyubetuDA As New HanteiKojiSyubetuDataAccess

    Public Function SelHanteiKojiSyubetuInfo(ByVal strKsSiyouNo As String, ByVal strKairyKojSyubetuNo As String) As DataTable
        Return HanteiKojiSyubetuDA.SelHanteiKojiSyubetuInfo(strKsSiyouNo, strKairyKojSyubetuNo)
    End Function
    Public Function SelJyuufuku(ByVal strKsSiyouNo As String, ByVal strKairyKojSyubetuNo As String) As Boolean

        If HanteiKojiSyubetuDA.SelJyuufuku(strKsSiyouNo, strKairyKojSyubetuNo).Rows.Count = 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function InsSeikyuuSaki(ByVal strKsSiyouNo As String, ByVal strKairyKojSyubetuNo As String, _
                               ByVal strUserId As String) As Integer

        Using scope As Transactions.TransactionScope = New Transactions.TransactionScope(Transactions.TransactionScopeOption.Required)
            Try
                HanteiKojiSyubetuDA.InsHanteiKojiSyubetu(strKsSiyouNo, _
                                    strKairyKojSyubetuNo, _
                                    strUserId)
                scope.Complete()
                Return True
            Catch ex As Exception
                scope.Dispose()
                Return False
            End Try

        End Using

    End Function
    Public Function DelHanteiKojiSyubetu(ByVal strKsSiyouNo As String, ByVal strKairyKojSyubetuNo As String) As Boolean

        Using scope As Transactions.TransactionScope = New Transactions.TransactionScope(Transactions.TransactionScopeOption.Required)
            Try

                HanteiKojiSyubetuDA.DelHanteiKojiSyubetu(strKsSiyouNo, strKairyKojSyubetuNo)
                scope.Complete()
                Return True

            Catch ex As Exception
                scope.Dispose()
                Return False
            End Try

        End Using

    End Function
    Public Function SelHaita(ByVal strKsSiyouNo As String, ByVal strKairyKojSyubetuNo As String, _
                               ByVal strKousin As String) As DataTable

        Return HanteiKojiSyubetuDA.SelHaita(strKsSiyouNo, strKairyKojSyubetuNo, strKousin)
    End Function
End Class

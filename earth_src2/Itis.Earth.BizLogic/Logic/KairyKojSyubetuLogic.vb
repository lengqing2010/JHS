Imports Itis.Earth.DataAccess
Imports Itis.Earth.BizLogic
Public Class KairyKojSyubetuLogic
    Private KojSyubetuDA As New KairyKojSyubetuDataAccess
    Public Function SelKojSyubetuInfo(ByVal strSyubetuNo As String) As DataTable
        Return KojSyubetuDA.SelKojSyubetuInfo(strSyubetuNo)
    End Function

    Public Function SelHaita(ByVal strSyubetuNo As String, ByVal strKousin As String) As DataTable

        Return KojSyubetuDA.SelHaita(strSyubetuNo, strKousin)
    End Function
    Public Function InsKojSyubetu(ByVal strSyubetuNo As String, _
                                 ByVal strSyubetu As String, _
                                 ByVal strUserId As String) As Integer

        Using scope As Transactions.TransactionScope = New Transactions.TransactionScope(Transactions.TransactionScopeOption.Required)
            Try
                KojSyubetuDA.InsKojSyubetu(strSyubetuNo, _
                                    strSyubetu, _
                                    strUserId)
                scope.Complete()
                Return True
            Catch ex As Exception
                scope.Dispose()
                Return False
            End Try

        End Using

    End Function

    Public Function UpdKojSyubetu(ByVal strSyubetuNo As String, _
                                 ByVal strSyubetu As String, _
                                 ByVal strUserId As String) As Integer

        Using scope As Transactions.TransactionScope = New Transactions.TransactionScope(Transactions.TransactionScopeOption.Required)
            Try
                If KojSyubetuDA.SelKojSyubetuInfo(strSyubetuNo).Rows.Count = 0 Then
                    scope.Dispose()
                    Return 2
                Else

                    KojSyubetuDA.UpdKojSyubetu(strSyubetuNo, _
                                    strSyubetu, _
                                    strUserId)
                    scope.Complete()
                    Return 1
                End If

            Catch ex As Exception
                scope.Dispose()
                Return 0
            End Try

        End Using

    End Function

    Public Function DelKojSyubetu(ByVal strSyubetuNo As String) As Boolean

        Using scope As Transactions.TransactionScope = New Transactions.TransactionScope(Transactions.TransactionScopeOption.Required)
            Try

                KojSyubetuDA.DelKojSyubetu(strSyubetuNo)
                scope.Complete()
                Return True

            Catch ex As Exception
                scope.Dispose()
                Return False
            End Try

        End Using

    End Function
End Class

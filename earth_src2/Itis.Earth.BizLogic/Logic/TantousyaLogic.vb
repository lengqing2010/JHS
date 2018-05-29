Imports Itis.Earth.DataAccess
Imports Itis.Earth.BizLogic
Public Class TantousyaLogic
    ''' <summary> メインメニュークラスのインスタンス生成 </summary>
    Private TantousyaDA As New TantousyaMasterDataAccess
    Public Function SelSimeiInfo(ByVal strNO As String) As DataTable

        Return TantousyaDA.SelSimeiInfo(strNO)
    End Function
    Public Function SelTantouInfo(ByVal strCd As String, Optional ByVal strMei As String = "", Optional ByVal strKBN As String = "", Optional ByVal strKen As String = "") As DataTable
        Return TantousyaDA.SelTantouInfo(strCd, strMei, strKBN, strKen)
    End Function

    Public Function SelJyuufuku(ByVal strCd As String) As Boolean
        If TantousyaDA.SelJyuufuku(strCd).Rows.Count = 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function SelHaita(ByVal strCd As String, ByVal strKousin As String) As DataTable
        Return TantousyaDA.SelHaita(strCd, strKousin)
    End Function
    Public Function InsTantousya(ByVal strCd As String, ByVal strMei As String, ByVal strKBN As String, ByVal strUserId As String) As Integer

        Using scope As Transactions.TransactionScope = New Transactions.TransactionScope(Transactions.TransactionScopeOption.Required)
            Try
                TantousyaDA.InsTantousya(strCd, _
                                    strMei, _
                                    strKBN, _
                                    strUserId)
                scope.Complete()
                Return True
            Catch ex As Exception
                scope.Dispose()
                Return False
            End Try

        End Using

    End Function


    Public Function UpdTantou(ByVal strCd As String, ByVal strMei As String, ByVal strKBN As String, ByVal strUserId As String) As Integer

        Using scope As Transactions.TransactionScope = New Transactions.TransactionScope(Transactions.TransactionScopeOption.Required)
            Try
                If TantousyaDA.SelTantouInfo(strCd).Rows.Count = 0 Then
                    scope.Dispose()
                    Return 2
                Else

                    TantousyaDA.UpdTantou(strCd, _
                                                  strMei, _
                                                  strKBN, _
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

    Public Function DelTantou(ByVal strCd As String) As Boolean

        Using scope As Transactions.TransactionScope = New Transactions.TransactionScope(Transactions.TransactionScopeOption.Required)
            Try

                TantousyaDA.DelTantou(strCd)
                scope.Complete()
                Return True

            Catch ex As Exception
                scope.Dispose()
                Return False
            End Try

        End Using

    End Function

End Class

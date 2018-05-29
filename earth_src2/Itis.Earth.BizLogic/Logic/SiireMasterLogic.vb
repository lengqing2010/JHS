Imports Itis.Earth.DataAccess
Imports Itis.Earth.BizLogic
Public Class SiireMasterLogic
    '''' <summary> メインメニュークラスのインスタンス生成 </summary>
    Private SiireDA As New SiireMasterDataAccess
    Public Function SelJyuufuku(ByVal strKaisya As String, ByVal strKameitenCd As String) As Boolean

        If SiireDA.SelJyuufuku(strKaisya, strKameitenCd).Rows.Count = 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function SelSiireInfo2(ByVal strKaisya As String, ByVal strKameitenCd As String) As DataTable
        Return SiireDA.SelSiireInfo2(strKaisya, strKameitenCd)
    End Function
    Public Function SelSiireInfo(ByVal strKaisya As String, ByVal strKameitenCd As String) As DataTable
        Return SiireDA.SelSiireInfo(strKaisya, strKameitenCd)
    End Function
    Public Function SelTyousaInfo(ByVal strCd As String, _
                               ByVal blnDelete As Boolean) As DataTable
        Return SiireDA.SelTyousaInfo(strCd, blnDelete)
    End Function
    Public Function SelHaita(ByVal strKaisya As String, _
                           ByVal strKameitenCd As String, _
                           ByVal strKousin As String) As DataTable

        Return SiireDA.SelHaita(strKaisya, strKameitenCd, strKousin)
    End Function
    Public Function DelKeiretu(ByVal strKaisya As String, _
                         ByVal strKameitenCd As String) As Boolean
        Using scope As Transactions.TransactionScope = New Transactions.TransactionScope(Transactions.TransactionScopeOption.Required)
            Try
                SiireDA.DelSyouhin(strKaisya, strKameitenCd)
                scope.Complete()
                Return True
            Catch ex As Exception
                scope.Dispose()
                Return False
            End Try

        End Using

    End Function
    Public Function InsSiire(ByVal strKaisya As String, _
                               ByVal strJigyousyo As String, _
                               ByVal strKameiten As String, _
                               ByVal strk1 As String, _
                               ByVal strk2 As String, _
                               ByVal strk3 As String, _
                               ByVal strUmu As String, _
                               ByVal strUserId As String) As Integer
        Using scope As Transactions.TransactionScope = New Transactions.TransactionScope(Transactions.TransactionScopeOption.Required)
            Try
                SiireDA.InsSiire(strKaisya, _
                                    strJigyousyo, _
                                    strKameiten, _
                                    strk1, _
                                    strk2, _
                                    strk3, _
                                    strUmu, _
                                    strUserId)
                scope.Complete()
                Return True
            Catch ex As Exception
                scope.Dispose()
                Return False
            End Try

        End Using

    End Function
    Public Function UpdSiire(ByVal strKaisya As String, _
                                ByVal strJigyousyo As String, _
                                ByVal strKameiten As String, _
                                ByVal strk1 As String, _
                                ByVal strk2 As String, _
                                ByVal strk3 As String, _
                                ByVal strUmu As String, _
                                ByVal strUserId As String) As Integer
        Using scope As Transactions.TransactionScope = New Transactions.TransactionScope(Transactions.TransactionScopeOption.Required)
            Try
                If SiireDA.SelSiireInfo(strKaisya & strJigyousyo, strKameiten).Rows.Count = 0 Then
                    scope.Dispose()
                    Return 2
                Else

                    SiireDA.UpdSiire(strKaisya, _
                                                  strJigyousyo, _
                                                  strKameiten, _
                                                  strk1, _
                                                  strk2, _
                                                  strk3, _
                                                  strUmu, _
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
End Class

Imports Itis.Earth.DataAccess
Imports Itis.Earth.BizLogic

Public Class SeikyuuSakiHenkouLogic
    '''' <summary> メインメニュークラスのインスタンス生成 </summary>
    Private SeikyuuDA As New SeikyuuSakiHenkouMasterDataAccess
    Public Function SelSeikyuuInfo(ByVal strKameitenCd As String, ByVal strSyouhinKBN As String) As DataTable
        Return SeikyuuDA.SelSeikyuuInfo(strKameitenCd, strSyouhinKBN)
    End Function
    Public Function SelSeikyuuSakiMei(ByVal strCd As String, ByVal strBrc As String, ByVal strKBN As String, ByVal strTorikesi As String) As DataTable
        Return SeikyuuDA.SelSeikyuuSakiMei(strCd, strBrc, strKBN, strTorikesi)
    End Function
    Public Function SelJyuufuku(ByVal strKameiten As String, ByVal strSyouhinKBN As String, Optional ByVal strMae As String = "") As Boolean

        If SeikyuuDA.SelJyuufuku(strKameiten, strSyouhinKBN).Rows.Count = 0 Then
            Return True
        Else
            If strSyouhinKBN = strMae Then
                Return True
            Else
                Return False
            End If

        End If
    End Function
    Public Function SelHaita(ByVal strKameitenCd As String, _
                          ByVal strSyouhinKBN As String, _
                          ByVal strKousin As String) As DataTable

        Return SeikyuuDA.SelHaita(strKameitenCd, strSyouhinKBN, strKousin)
    End Function
    Public Function InsSeikyuuSaki(ByVal strKameiten As String, _
                       ByVal strSyouhinKBN As String, _
                       ByVal strHenkouSaki As String, _
                       ByVal strBrc As String, _
                       ByVal strSakiKBN As String, _
                       ByVal strUserId As String) As Integer

        Using scope As Transactions.TransactionScope = New Transactions.TransactionScope(Transactions.TransactionScopeOption.Required)
            Try
                SeikyuuDA.InsSeikyuuSaki(strKameiten, _
                                    strSyouhinKBN, _
                                    strHenkouSaki, _
                                    strBrc, _
                                    strSakiKBN, _
                                    strUserId)
                scope.Complete()
                Return True
            Catch ex As Exception
                scope.Dispose()
                Return False
            End Try

        End Using

    End Function

    Public Function UpdSeikyuuSaki(ByVal strKameiten As String, _
                           ByVal strSyouhinKBN As String, _
                           ByVal strHenkouSaki As String, _
                           ByVal strBrc As String, _
                           ByVal strSakiKBN As String, _
                           ByVal strUserId As String, ByVal strSyouhinmae As String) As Integer

        Using scope As Transactions.TransactionScope = New Transactions.TransactionScope(Transactions.TransactionScopeOption.Required)
            Try
                If SeikyuuDA.SelSeikyuuInfo(strKameiten, strSyouhinmae).Rows.Count = 0 Then
                    scope.Dispose()
                    Return 2
                Else

                    SeikyuuDA.UpdSeikyuuSaki(strKameiten, _
                                                  strSyouhinKBN, _
                                                  strHenkouSaki, _
                                                  strBrc, _
                                                  strSakiKBN, _
                                                  strUserId, strSyouhinmae)
                    scope.Complete()
                    Return 1
                End If

            Catch ex As Exception
                scope.Dispose()
                Return 0
            End Try

        End Using

    End Function

    Public Function DelSeikyuuSaki(ByVal strKameitenCd As String, ByVal strSyouhinKBN As String)  As Boolean

        Using scope As Transactions.TransactionScope = New Transactions.TransactionScope(Transactions.TransactionScopeOption.Required)
            Try
                
                SeikyuuDA.DelSeikyuuSaki(strKameitenCd, strSyouhinKBN)
                scope.Complete()
                Return True

            Catch ex As Exception
                scope.Dispose()
                Return False
            End Try

        End Using

    End Function
End Class

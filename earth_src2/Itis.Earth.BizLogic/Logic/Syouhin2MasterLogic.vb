Imports Itis.Earth.DataAccess
Imports Itis.Earth.BizLogic
Public Class Syouhin2MasterLogic
    '''' <summary> メインメニュークラスのインスタンス生成 </summary>
    Private SyouhinDA As New Syouhin2MasterDataAccess
    Public Function SelSyouhinInfo(ByVal strKameitenCd As String, ByVal strSyouhinCd As String) As DataTable
        Return SyouhinDA.SelSyouhinInfo(strKameitenCd, strSyouhinCd)
    End Function

    Public Function SelJyuufuku(ByVal strKameitenCd As String, ByVal strSyouhinCd As String) As Boolean

        If SyouhinDA.SelJyuufuku(strKameitenCd, strSyouhinCd).Rows.Count = 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function SelDate(ByVal strKameitenCd As String, ByVal strSyouhinCd As String) As String

        Return SyouhinDA.SelJyuufuku(strKameitenCd, strSyouhinCd).Rows(0).Item(1).ToString
    End Function
    Public Function InsKeiretu(ByVal strKameitenCd As String, _
                             ByVal strSyouhinCd As String, _
                             ByVal strUserId As String) As Integer
        Using scope As Transactions.TransactionScope = New Transactions.TransactionScope(Transactions.TransactionScopeOption.Required)
            Try
                SyouhinDA.InsSyouhin(strKameitenCd, strSyouhinCd, strUserId)
                scope.Complete()
                Return True
            Catch ex As Exception
                scope.Dispose()
                Return False
            End Try

        End Using

    End Function
    '''<summary>商品データを取得する</summary>
    Public Function SelSyouhin(ByVal strSyouhinCd As String, _
                                    ByVal strSyouhinMei As String, _
                                     ByVal soukoCd As String) As CommonSearchDataSet.SyouhinTableDataTable
        Return SyouhinDA.SelSyouhin(strSyouhinCd, strSyouhinMei, soukoCd)
    End Function
    '''<summary>加盟店データを取得する</summary>
    Public Function GetKameitenKensakuInfo(ByVal strKameitenCd As String, ByVal strTorikesi As String) As CommonSearchDataSet.KameitenSearchTableDataTable
        Return SyouhinDA.SelKameitenKensakuInfo(strKameitenCd, strTorikesi)

    End Function
    Public Function DelKeiretu(ByVal strKameitenCd As String, _
                             ByVal strSyouhinCd As String) As Boolean
        Using scope As Transactions.TransactionScope = New Transactions.TransactionScope(Transactions.TransactionScopeOption.Required)
            Try
                SyouhinDA.DelSyouhin(strKameitenCd, strSyouhinCd)
                scope.Complete()
                Return True
            Catch ex As Exception
                scope.Dispose()
                Return False
            End Try

        End Using

    End Function
    Public Function SelHaita(ByVal strKameitenCd As String, _
                             ByVal strSyouhinCd As String, _
                             ByVal strKousin As String) As DataTable

        Return SyouhinDA.SelHaita(strKameitenCd, strSyouhinCd, strKousin)
    End Function
End Class

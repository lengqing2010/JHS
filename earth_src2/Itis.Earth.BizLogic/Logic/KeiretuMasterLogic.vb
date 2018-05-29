Imports Itis.Earth.DataAccess
Imports Itis.Earth.BizLogic
Public Class KeiretuMasterLogic
    ''' <summary> メインメニュークラスのインスタンス生成 </summary>
    Private KeiretuDA As New KeiretuMasterDataAccess
    '''<summary>系列レコード行数を取得する</summary>
    Public Function SelKeiretuInfo(ByVal strKbn As String, ByVal strTorikesi As String) As DataTable

        Return KeiretuDA.SelKeiretuInfo(strKbn, strTorikesi)
    End Function

    Public Function SelJyuufuku(ByVal strKbn As String, ByVal strKeiretuCd As String) As Boolean

        If KeiretuDA.SelJyuufuku(strKbn, strKeiretuCd).Rows.Count = 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function SelDate(ByVal strKbn As String, ByVal strKeiretuCd As String) As String

        Return KeiretuDA.SelJyuufuku(strKbn, strKeiretuCd).Rows(0).Item(1).ToString
    End Function
    Public Function SelHaita(ByVal strKbn As String, ByVal strKeiretuCd As String, ByVal strKousin As String) As DataTable

        Return KeiretuDA.SelHaita(strKbn, strKeiretuCd, strKousin)
    End Function

    Public Function InsKeiretu(ByVal strKbn As String, _
                                ByVal strTorikesi As String, _
                                ByVal strKeiretuCd As String, _
                                ByVal strKeiretuMei As String, _
                                ByVal strUserId As String) As Integer
        Using scope As Transactions.TransactionScope = New Transactions.TransactionScope(Transactions.TransactionScopeOption.Required)
            Try
                KeiretuDA.InsKeiretu(strKbn, strTorikesi, strKeiretuCd, strKeiretuMei, strUserId)
                scope.Complete()
                Return True
            Catch ex As Exception
                scope.Dispose()
                Return False
            End Try

        End Using

    End Function
    Public Function UpdKeiretu(ByVal strKbn As String, _
                               ByVal strTorikesi As String, _
                               ByVal strKeiretuCd As String, _
                               ByVal strKeiretuMei As String, _
                               ByVal strUserId As String) As Integer
        Using scope As Transactions.TransactionScope = New Transactions.TransactionScope(Transactions.TransactionScopeOption.Required)
            Try
                KeiretuDA.UpdKeiretu(strKbn, strTorikesi, strKeiretuCd, strKeiretuMei, strUserId)
                scope.Complete()
                Return True
            Catch ex As Exception
                scope.Dispose()
                Return False
            End Try

        End Using

    End Function
End Class

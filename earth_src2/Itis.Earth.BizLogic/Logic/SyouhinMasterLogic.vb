Imports Itis.Earth.DataAccess
Imports Itis.Earth.BizLogic
Public Class SyouhinMasterLogic
    ''' <summary> メインメニュークラスのインスタンス生成 </summary>
    Private SyouhinDA As New SyouhinMasterDataAccess
    '''<summary>消費税を取得する</summary>
    Public Function SelZeiKBNInfo() As DataTable

        Return SyouhinDA.SelZeiKBNInfo()
    End Function
    '''<summary>商品データを取得する</summary>
    Public Function GetSyouhinInfo(ByVal strSyouhinCd As String) As SyouhinDataSet.m_syouhinDataTable
        Return SyouhinDA.SelSyouhinInfo(strSyouhinCd)
    End Function
    '''<summary>拡張名称を取得する</summary>
    Public Function SelKakutyouInfo(ByVal strSyubetu As String, Optional ByVal strTable As String = "") As DataTable

        Return SyouhinDA.SelKakutyouInfo(strSyubetu, strTable)
    End Function

    Public Function SelSyouhinInfo(ByVal strSyouhinCd As String) As SyouhinDataSet.m_syouhinDataTable
        Return SyouhinDA.SelSyouhinInfo(strSyouhinCd)
    End Function

    Public Function InsSyouhin(ByVal dtSyouhin As SyouhinDataSet.m_syouhinDataTable) As Boolean
        Using scope As Transactions.TransactionScope = New Transactions.TransactionScope(Transactions.TransactionScopeOption.Required)
            Try
                SyouhinDA.InsSyouhin(dtSyouhin)
                scope.Complete()
                Return True
            Catch ex As Exception
                scope.Dispose()
                Return False
            End Try

        End Using

    End Function
    Public Function SelHaita(ByVal strKeiretuCd As String, ByVal strKousin As String) As DataTable

        Return SyouhinDA.SelHaita(strKeiretuCd, strKousin)
    End Function
    Public Function UpdSyouhin(ByVal dtSyouhin As SyouhinDataSet.m_syouhinDataTable) As String
        Dim dtHaita As New DataTable
        Using scope As Transactions.TransactionScope = New Transactions.TransactionScope(Transactions.TransactionScopeOption.Required)
            Try

                If SelSyouhinInfo(dtSyouhin(0).syouhin_cd).Rows.Count = 0 Then
                    scope.Dispose()
                    Return "2"
                Else
                    dtHaita = SelHaita(dtSyouhin(0).syouhin_cd, dtSyouhin(0).upd_datetime)
                    If dtHaita.Rows.Count <> 0 Then
                        scope.Dispose()
                        Return String.Format(Messages.Instance.MSG2033E, dtHaita.Rows(0).Item("upd_login_user_id"), dtHaita.Rows(0).Item("upd_datetime"), "商品マスタ").ToString()
                    Else
                        SyouhinDA.UpdSyouhin(dtSyouhin)
                        scope.Complete()
                        Return "0"
                    End If
                End If

            Catch ex As Exception
                scope.Dispose()
                Return "1"
            End Try

        End Using

    End Function
End Class

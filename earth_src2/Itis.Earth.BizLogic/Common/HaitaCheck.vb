Imports Itis.Earth.DataAccess
Public Class HaitaCheck
    ''' <summary> メインメニュークラスのインスタンス生成 </summary>
    Private CommonSearchDA As New CommonSearchDataAccess
    ''' <summary>
    ''' 排他チェック
    ''' </summary>
    ''' <param name="strKameitenCd"></param>
    ''' <param name="strTableName"></param>
    ''' <param name="gameiDate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CheckHaita(ByVal strKameitenCd As String, ByVal strTableName As String, ByVal gameiDate As Date) As String
        Dim strReturn As String = ""
        strReturn = CommonSearchDA.SelHaita(strKameitenCd, strTableName)
        If strReturn = "" Then
            Return Messages.Instance.MSG2009E
        Else
            If strReturn.Split(",")(1).Trim <> "" Then
                If CType(strReturn.Split(",")(1), Date) > gameiDate Then
                    Return String.Format(Messages.Instance.MSG003E, Split(strReturn, ",")(0), Split(strReturn, ",")(1))

                Else
                    Return ""
                End If
            Else
                Return ""
            End If

        End If

    End Function
End Class


''' <summary>
''' 区分データのビジネスロジッククラスです
''' </summary>
''' <remarks></remarks>
Public Class KubunLogic
    Inherits DropDownLogicHelper

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ''' <summary>
    ''' 区分を引数に区分レコードを１件取得します
    ''' </summary>
    ''' <param name="kubun">区分</param>
    ''' <returns>区分レコード</returns>
    ''' <remarks></remarks>
    Public Function GetInfo(ByVal kubun As String) As KubunRecord

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetInfo", _
                                            kubun)

        Dim kubunRec As New KubunRecord
        Dim dataAccess As KubunDataAccess = New KubunDataAccess

        Dim torikeshi As Integer
        Dim kubunMei As String = ""

        If dataAccess.GetDataBy(kubun, torikeshi, kubunMei) = False Then
            Debug.WriteLine("取得出来ませんでした")
            Return Nothing
        Else
            kubunRec.Kubun = kubun
            kubunRec.Torikeshi = torikeshi
            kubunRec.KubunMei = kubunMei

            Debug.WriteLine("kubun_rec.Kubun" + kubunRec.Kubun)
            Debug.WriteLine("kubun_rec.Torikeshi" + kubunRec.Torikeshi.ToString)
            Debug.WriteLine("kubun_rec.KubunMei" + kubunRec.KubunMei)

        End If

        Return kubunRec

    End Function

End Class

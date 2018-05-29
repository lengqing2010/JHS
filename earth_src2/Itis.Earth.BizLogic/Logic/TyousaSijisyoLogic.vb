Imports Itis.Earth.DataAccess

Public Class TyousaSijisyoLogic

    ''' <summary>加盟店物件情報照会クラスのインスタンス生成 </summary>
    Private TyousaSijisyoDataAccess As New TyousaSijisyoDataAccess

    ''' <summary>調査指示情報</summary>
    ''' <returns>調査指示情報</returns>
    ''' <history>2016/11/24　李松涛(大連情報システム部)　新規作成</history>
    Public Function GetTyousaSijisyo(ByVal kbn As String _
                                   , ByVal hosyousyo_no As String) As Data.DataTable

        Return TyousaSijisyoDataAccess.SelTyousaSijisyo(kbn, hosyousyo_no)


    End Function
End Class

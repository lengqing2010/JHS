Imports Itis.Earth.DataAccess

''' <summary>与信管理情報を取得する</summary>
''' <remarks>与信管理情報照会を提供する</remarks>
''' <history>
''' <para>2009/07/16　呉営(大連情報システム部)　新規作成</para>
''' </history>
Public Class YosinJyouhouInputLogic
    ''' <summary> 与信管理情報クラスのインスタンス生成 </summary>
    Private YosinJyouhouInputDA As New YosinJyouhouInputDataAccess
    ''' <summary>
    ''' 与信管理情報を取得する
    ''' </summary>
    ''' <param name="nayose_cd">名寄先コード</param>
    ''' <returns>与信管理情報データテーブル</returns>
    Public Function GetYosinKanriInfo(ByVal nayose_cd As String) As YosinJyouhouInputDataSet.YosinKanriInfoTableDataTable
        Return YosinJyouhouInputDA.GetYosinKanriInfo(nayose_cd)
    End Function
    Public Function GetYosinMeisai(ByVal nayose_cd As String, ByVal tyousa As Boolean, ByVal kouji As Boolean, ByVal sonota As Boolean, ByVal yotei As Boolean, ByVal jisseki As Boolean) As DataSet
        Return YosinJyouhouInputDA.GetYosinMeisai(nayose_cd, tyousa, kouji, sonota, yotei, jisseki)
    End Function
    Public Function GetNayoseInfo(ByVal kameiten_cd As String) As DataTable
        Return YosinJyouhouInputDA.GetNayoseInfo(kameiten_cd)
    End Function
    ''' <summary>
    ''' 入金予定情報を取得する
    ''' </summary>
    ''' <param name="nayose_cd">名寄先コード</param>
    ''' <returns>入金予定情報データテーブル</returns>
    Public Function GetNyuukinYoteiInfo(ByVal nayose_cd As String, _
                                        ByVal ruiseki_nyuukin_set_date As DateTime) As YosinJyouhouInputDataSet.NyuukinYoteiInfoTableDataTable
        Return YosinJyouhouInputDA.GetNyuukinYoteiInfo(nayose_cd, ruiseki_nyuukin_set_date)
    End Function
End Class

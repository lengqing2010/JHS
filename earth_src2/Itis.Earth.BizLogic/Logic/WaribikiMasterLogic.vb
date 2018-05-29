Imports Itis.Earth.DataAccess
Public Class WaribikiMasterLogic
    ''' <summary>
    ''' 多棟割引データを取得
    ''' </summary>
    ''' <param name="strKameitenCdFrom">加盟店コード（FROM）</param>
    ''' <param name="strKameitenCdTo">加盟店コード（TO）</param>
    ''' <param name="strKameitenMei">加盟店名</param>
    ''' <param name="strKameitenKana">加盟店カナ</param>
    ''' <param name="strKeiretuCd">系列コード</param>
    ''' <history>
    ''' <para>2009/07/15　王穎(大連開発)　新規作成　P-*****</para>
    ''' </history>
    ''' <returns>多棟割引データ情報</returns>
    ''' <remarks></remarks>
    Public Function GetWaribiki(ByVal strKameitenCdFrom As String, _
                                     ByVal strKameitenCdTo As String, _
                                     ByVal strKameitenMei As String, _
                                     ByVal strKameitenKana As String, _
                                     ByVal strKeiretuCd As String, _
                                     ByVal strSyouhin As String, _
                                     ByVal strSearchCount As String) _
                                     As DataAccess.WaribikiDataSet.WaribikiTableDataTable
        Dim WaribikiDA As New DataAccess.WaribikiMasterDataAccess
        Return WaribikiDA.SelWaribiki(strKameitenCdFrom, _
                                      strKameitenCdTo, _
                                      strKameitenMei, _
                                      strKameitenKana, _
                                      strKeiretuCd, _
                                      strSyouhin, _
                                      strSearchCount)

    End Function
    ''' <summary>
    ''' 多棟割引データ件数を取得
    ''' </summary>
    ''' <param name="strKameitenCdFrom">加盟店コード（FROM）</param>
    ''' <param name="strKameitenCdTo">加盟店コード（TO）</param>
    ''' <param name="strKameitenMei">加盟店名</param>
    ''' <param name="strKameitenKana">加盟店カナ</param>
    ''' <param name="strKeiretuCd">系列コード</param>
    ''' <history>
    ''' <para>2009/07/15　王穎(大連開発)　新規作成　P-*****</para>
    ''' </history>
    ''' <returns>多棟割引データ件数</returns>
    ''' <remarks></remarks>
    Public Function GetWaribikiCount(ByVal strKameitenCdFrom As String, _
                                     ByVal strKameitenCdTo As String, _
                                     ByVal strKameitenMei As String, _
                                     ByVal strKameitenKana As String, _
                                     ByVal strKeiretuCd As String, _
                                     ByVal strSyouhin As String) As Integer

        Dim WaribikiDA As New DataAccess.WaribikiMasterDataAccess
        Return WaribikiDA.SelWaribikiCount(strKameitenCdFrom, _
                                      strKameitenCdTo, _
                                      strKameitenMei, _
                                      strKameitenKana, _
                                      strKeiretuCd, _
                                      strSyouhin)

    End Function
End Class

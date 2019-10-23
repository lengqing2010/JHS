''' <summary>
''' 系列インターフェース
''' </summary>
''' <remarks>系列オブジェクトの基底</remarks>
Public Interface IKeiretuDataAccess

    ''' <summary>
    ''' 請求金額を取得します
    ''' </summary>
    ''' <param name="intMode">取得モード</param>
    ''' <param name="strSyouhinCd">商品コード</param>
    ''' <param name="intKingaku">TH請求用価格マスタのKEY,掛率計算時の金額</param>
    ''' <param name="intReturnKingaku">取得金額（戻り値）</param>
    ''' <returns>1:取得OK,0:経理要連絡,-1:取得NG</returns>
    ''' <remarks></remarks>
    Function getSeikyuKingaku(ByVal intMode As Integer, _
                        ByVal strSyouhinCd As String, _
                        ByVal intKingaku As Integer, _
                        ByRef intReturnKingaku As Integer) As Integer
End Interface

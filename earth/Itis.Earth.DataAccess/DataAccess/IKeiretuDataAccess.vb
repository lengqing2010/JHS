''' <summary>
''' nρC^[tF[X
''' </summary>
''' <remarks>nρIuWFNgΜξκ</remarks>
Public Interface IKeiretuDataAccess

    ''' <summary>
    ''' ΏΰzπζΎ΅ά·
    ''' </summary>
    ''' <param name="intMode">ζΎ[h</param>
    ''' <param name="strSyouhinCd">€iR[h</param>
    ''' <param name="intKingaku">THΏpΏi}X^ΜKEY,|¦vZΜΰz</param>
    ''' <param name="intReturnKingaku">ζΎΰziίθlj</param>
    ''' <returns>1:ζΎOK,0:ovA,-1:ζΎNG</returns>
    ''' <remarks></remarks>
    Function getSeikyuKingaku(ByVal intMode As Integer, _
                        ByVal strSyouhinCd As String, _
                        ByVal intKingaku As Integer, _
                        ByRef intReturnKingaku As Integer) As Integer
End Interface

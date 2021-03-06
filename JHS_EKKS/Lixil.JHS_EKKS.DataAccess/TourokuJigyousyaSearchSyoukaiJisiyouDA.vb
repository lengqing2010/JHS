Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Imports System.Data
Imports System.Text

''' <summary>
''' o^ÆÒõ_væÇ_Á¿XõÆïw¦pPOPUP
''' </summary>
''' <remarks></remarks>
Public Class TourokuJigyousyaSearchSyoukaiJisiyouDA

    ''' <summary>
    ''' uÁ¿XR[hvAuÁ¿X¼vAus¹{§¼vÆuÁ¿XJi¼vÌõ·é
    ''' </summary>
    ''' <param name="strRows">õãÀ</param>
    ''' <param name="strKameitenCd">Á¿XR[h</param>
    ''' <param name="strKameitenMei">Á¿X¼</param>
    ''' <param name="blnTorikesi">æÁ</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/7/29@F(åAîñVXe)@VKì¬</history>
    Public Function SelTourokuJigyousya(ByVal kbn As String, _
                                        ByVal strRows As String, _
                                        ByVal strKameitenCd As String, _
                                        ByVal strKameitenMei As String, _
                                        ByVal blnTorikesi As Boolean) As Data.DataTable

        'EMABáQÎîñÌi[
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strRows, _
                                                                                          strKameitenCd, _
                                                                                          strKameitenMei, _
                                                                                          blnTorikesi)

        'ßèf[^Zbg
        Dim dsReturn As New Data.DataSet

        'SQLRg
        Dim commandTextSb As New StringBuilder

        'o^i[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL¶
        With commandTextSb
            .AppendLine("SELECT")
            If strRows = "0" Then
                .AppendLine("   TOP 100")
            End If
            .AppendLine("    MKKI.kameiten_cd")                   'Á¿XR[h
            .AppendLine("   ,MKKI.keikakuyou_kameitenmei")                 'Á¿X¼
            .AppendLine("   ,MT.todouhuken_mei")
            .AppendLine("   ,MKKI.tenmei_kana1")
            '.AppendLine("   ,CASE WHEN MK.torikesi = '0' THEN")
            '.AppendLine("         ''")
            '.AppendLine("         ELSE 'æÁ'")
            '.AppendLine("         END AS Torikesi")         'æÁ
            .AppendLine("FROM")
            .AppendLine("   m_keikaku_kameiten_info AS MKKI WITH(READCOMMITTED)")  'Á¿X}X^
            .AppendLine("   LEFT JOIN")
            .AppendLine("       m_todoufuken AS MT WITH(READCOMMITTED)") 's¹{§}X^
            .AppendLine("       ON MKKI.todouhuken_cd = MT.todouhuken_cd")
            .AppendLine("WHERE")
            .AppendLine("   1 = 1")
            .AppendLine("   AND MKKI.kbn = @kbn")
            If strKameitenCd <> "" Then
                .AppendLine("   AND MKKI.kameiten_cd LIKE @kameiten_cd")
            End If
            If strKameitenMei <> "" Then
                .AppendLine("   AND MKKI.keikakuyou_kameitenmei LIKE @kameiten_mei")
            End If
            If blnTorikesi = True Then
                .AppendLine("   AND MKKI.torikesi = @torikesi ")
            End If
            .AppendLine("ORDER BY")
            .AppendLine("   MKKI.kameiten_cd")
        End With

        'o^
        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, kbn & "%")) 'æª
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 4, strKameitenCd)) 'Á¿XR[h
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 10, "0"))        'æÁ
        paramList.Add(MakeParam("@kameiten_mei", SqlDbType.VarChar, 80, "%" & strKameitenMei & "%")) 'Á¿X¼
        'If blnAimai Then
        '    paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd & "%")) 'Á¿XR[h
        '    paramList.Add(MakeParam("@kameiten_mei", SqlDbType.VarChar, 40, "%" & strKameitenMei & "%")) 'Á¿X¼
        'Else
        '    paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 4, strKameitenCd)) 'Á¿XR[h
        '    paramList.Add(MakeParam("@kameiten_mei", SqlDbType.VarChar, 38, strKameitenMei)) 'Á¿X¼
        'End If
        'paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 10, "0"))                              'æÁ

        'õÀs
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "dtTourokuJigyousya", paramList.ToArray())

        Return dsReturn.Tables("dtTourokuJigyousya")

    End Function

    ''' <summary>
    ''' õµ½f[^ðæ¾·é
    ''' </summary>
    ''' <param name="strKameitenCd">Á¿XR[h</param>
    ''' <param name="strKameitenMei">Á¿X¼</param>
    ''' <param name="blnTorikesi">æÁ</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/7/29@F(åAîñVXe)@VKì¬</history>
    Public Function SelTourokuJigyousyaCount(ByVal kbn As String, _
                                             ByVal strKameitenCd As String, _
                                             ByVal strKameitenMei As String, _
                                             ByVal blnTorikesi As Boolean) As Data.DataTable

        'EMABáQÎîñÌi[
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strKameitenCd, _
                                                                                          strKameitenMei, _
                                                                                          blnTorikesi)

        'ßèf[^Zbg
        Dim dsReturn As New Data.DataSet

        'SQLRg
        Dim commandTextSb As New StringBuilder

        'o^i[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL¶
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("    COUNT(kameiten_cd)")
            .AppendLine("FROM")
            .AppendLine("    m_keikaku_kameiten_info WITH(READCOMMITTED)")
            .AppendLine("WHERE")
            .AppendLine("   1 = 1")
            .AppendLine("   AND MKKI.kbn = @kbn")
            If strKameitenCd <> "" Then
                .AppendLine("   AND kameiten_cd LIKE @kameiten_cd")
            End If
            If strKameitenMei <> "" Then
                .AppendLine("   AND keikakuyou_kameitenmei LIKE @kameiten_mei")
            End If
            If blnTorikesi = True Then
                .AppendLine("   AND torikesi = @torikesi ")
            End If
            .AppendLine("ORDER BY")
            .AppendLine("   kameiten_cd")
        End With

        'o^
        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, kbn & "%")) 'æª
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 6, strKameitenCd & "%")) 'Á¿XR[h
        paramList.Add(MakeParam("@kameiten_mei", SqlDbType.VarChar, 80, "%" & strKameitenMei & "%")) 'Á¿X¼
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 10, "0"))                              'æÁ

        'õÀs
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "dtTourokuJigyousyaCount", paramList.ToArray())

        Return dsReturn.Tables("dtTourokuJigyousyaCount")

    End Function

End Class

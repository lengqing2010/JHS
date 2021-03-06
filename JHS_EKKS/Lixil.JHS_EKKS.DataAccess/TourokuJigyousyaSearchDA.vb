Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Imports System.Data
Imports System.Text

''' <summary>
''' o^ÆÒPOPUP
''' </summary>
''' <remarks></remarks>
Public Class TourokuJigyousyaSearchDA

    ''' <summary>
    ''' uÁ¿XR[hvAuÁ¿X¼vAus¹{§¼vÆuÁ¿XJi¼vÌõ·é
    ''' </summary>
    ''' <param name="strRows">õãÀ</param>
    ''' <param name="strKameitenCd">Á¿XR[h</param>
    ''' <param name="strKameitenMei">Á¿X¼</param>
    ''' <param name="blnTorikesi">æÁ</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/11/19@F(åAîñVXe)@VKì¬</history>
    Public Function SelTourokuJigyousya(ByVal strRows As String, _
                                        ByVal strKameitenCd As String, _
                                        ByVal strKameitenMei As String, _
                                        ByVal blnTorikesi As Boolean, ByVal blnAimai As Boolean) As Data.DataTable

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
            .AppendLine("    MK.kameiten_cd")                   'Á¿XR[h
            .AppendLine("   ,MK.kameiten_mei1")                 'Á¿X¼
            .AppendLine("   ,MT.todouhuken_mei")
            .AppendLine("   ,MK.tenmei_kana1")
            .AppendLine("   ,CASE WHEN MK.torikesi = '0' THEN")
            .AppendLine("         ''")
            .AppendLine("         ELSE 'æÁ'")
            .AppendLine("         END AS Torikesi")         'æÁ
            .AppendLine("FROM")
            .AppendLine("   m_kameiten AS MK WITH(READCOMMITTED)")  'Á¿X}X^
            .AppendLine("   LEFT JOIN")
            .AppendLine("       m_todoufuken AS MT WITH(READCOMMITTED)") 's¹{§}X^
            .AppendLine("       ON MK.todouhuken_cd = MT.todouhuken_cd")
            .AppendLine("WHERE")
            If strKameitenCd <> "" Then
                .AppendLine("   MK.kameiten_cd LIKE @kameiten_cd")
            Else
                .AppendLine("   1 = 1")
            End If
            If strKameitenMei <> "" Then
                .AppendLine("   AND (MK.kameiten_mei1 LIKE @kameiten_mei")
                .AppendLine("   OR MK.kameiten_mei2 LIKE @kameiten_mei)")
            End If
            If blnTorikesi = True Then
                .AppendLine("   AND MK.torikesi = @torikesi ")
            End If
            .AppendLine("ORDER BY")
            .AppendLine("   MK.kameiten_cd")
        End With

        'o^
        If blnAimai Then
            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd & "%")) 'Á¿XR[h
            paramList.Add(MakeParam("@kameiten_mei", SqlDbType.VarChar, 40, "%" & strKameitenMei & "%")) 'Á¿X¼
        Else
            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 4, strKameitenCd)) 'Á¿XR[h
            paramList.Add(MakeParam("@kameiten_mei", SqlDbType.VarChar, 38, strKameitenMei)) 'Á¿X¼
        End If
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 10, "0"))                              'æÁ

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
    ''' <history>2012/11/19@F(åAîñVXe)@VKì¬</history>
    Public Function SelTourokuJigyousyaCount(ByVal strKameitenCd As String, _
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
            .AppendLine("    m_kameiten WITH(READCOMMITTED)")
            .AppendLine("WHERE")
            If strKameitenCd <> "" Then
                .AppendLine("   kameiten_cd LIKE @kameiten_cd")
            Else
                .AppendLine("   1 = 1")
            End If
            If strKameitenMei <> "" Then
                .AppendLine("   AND (kameiten_mei1 LIKE @kameiten_mei")
                .AppendLine("   OR kameiten_mei2 LIKE @kameiten_mei)")
            End If
            If blnTorikesi = True Then
                .AppendLine("   AND torikesi = @torikesi ")
            End If
        End With

        'o^
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 6, strKameitenCd & "%")) 'Á¿XR[h
        paramList.Add(MakeParam("@kameiten_mei", SqlDbType.VarChar, 42, "%" & strKameitenMei & "%")) 'Á¿X¼
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 10, "0"))                              'æÁ

        'õÀs
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "dtTourokuJigyousyaCount", paramList.ToArray())

        Return dsReturn.Tables("dtTourokuJigyousyaCount")

    End Function

End Class

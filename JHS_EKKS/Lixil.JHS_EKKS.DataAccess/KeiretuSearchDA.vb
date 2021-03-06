Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Imports System.Data
Imports System.Text

''' <summary>
''' nñR[hõPOPUP
''' </summary>
''' <remarks></remarks>
Public Class KeiretuSearchDA

    ''' <summary>
    ''' unñ}X^ve[væèAnñîñðæ¾·é
    ''' </summary>
    ''' <param name="strRows">õãÀ</param>
    ''' <param name="strKeiretuCd">nñR[h</param>
    ''' <param name="strKeiretuMei">nñ¼</param>
    ''' <param name="blnTorikesi">æÁ</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/11/15@F(åAîñVXe)@VKì¬</history>
    Public Function SelKiretuJyouhou(ByVal strRows As String, _
                                     ByVal strKeiretuCd As String, _
                                     ByVal strKeiretuMei As String, _
                                     ByVal blnTorikesi As Boolean, ByVal blnAimai As Boolean) As Data.DataTable

        'EMABáQÎîñÌi[
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strRows, _
                                                                                          strKeiretuCd, _
                                                                                          strKeiretuMei, _
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
            .AppendLine("    keiretu_cd")                   'nñR[h
            .AppendLine("   ,keiretu_mei")                  'nñ¼
            .AppendLine("   ,CASE WHEN torikesi = '0' THEN")
            .AppendLine("         ''")
            .AppendLine("         ELSE 'æÁ'")
            .AppendLine("         END AS Torikesi")         'æÁ
            .AppendLine("FROM")
            .AppendLine("   m_keiretu WITH(READCOMMITTED)") 'nñ}X^
            .AppendLine("WHERE")
            If strKeiretuCd <> "" Then
                .AppendLine("   keiretu_cd LIKE @keiretu_cd")
            Else
                .AppendLine("1 = 1")
            End If
            If strKeiretuMei <> "" Then
                .AppendLine("   AND keiretu_mei LIKE @keiretu_mei")
            End If
            If blnTorikesi = True Then
                .AppendLine("   AND torikesi = @torikesi ")
            End If
            .AppendLine("ORDER BY")
            .AppendLine("   keiretu_cd")
        End With

        'o^
        If blnAimai Then
            paramList.Add(MakeParam("@keiretu_cd", SqlDbType.VarChar, 6, strKeiretuCd & "%"))    'nñR[h
            paramList.Add(MakeParam("@keiretu_mei", SqlDbType.VarChar, 42, "%" & strKeiretuMei & "%")) 'nñ¼
        Else
            paramList.Add(MakeParam("@keiretu_cd", SqlDbType.VarChar, 5, strKeiretuCd))    'nñR[h
            paramList.Add(MakeParam("@keiretu_mei", SqlDbType.VarChar, 40, strKeiretuMei)) 'nñ¼
        End If
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 10, "0"))                              'æÁ

        'õÀs
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "dtKiretuJyouhou", paramList.ToArray())

        Return dsReturn.Tables("dtKiretuJyouhou")

    End Function

    ''' <summary>
    ''' õµ½f[^ðæ¾·é
    ''' </summary>
    ''' <param name="strKeiretuCd">nñR[h</param>
    ''' <param name="strKeiretuMei">nñ¼</param>
    ''' <param name="blnTorikesi">æÁ</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/11/16@F(åAîñVXe)@VKì¬</history>
    Public Function SelKiretuJyouhouCount(ByVal strKeiretuCd As String, _
                                          ByVal strKeiretuMei As String, _
                                          ByVal blnTorikesi As Boolean) As Data.DataTable

        'EMABáQÎîñÌi[
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strKeiretuCd, _
                                                                                          strKeiretuMei, _
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
            .AppendLine("    COUNT(keiretu_cd)")
            .AppendLine("FROM")
            .AppendLine("    m_keiretu WITH(READCOMMITTED)")
            .AppendLine("WHERE")
            If strKeiretuCd <> "" Then
                .AppendLine("     keiretu_cd LIKE @keiretu_cd")
            Else
                .AppendLine("     1 = 1")
            End If
            If strKeiretuMei <> "" Then
                .AppendLine("    AND keiretu_mei LIKE @keiretu_mei")
            End If
            If blnTorikesi = True Then
                .AppendLine("   AND torikesi = @torikesi")
            End If
        End With

        'o^
        paramList.Add(MakeParam("@keiretu_cd", SqlDbType.VarChar, 6, strKeiretuCd & "%"))          'nñR[h
        paramList.Add(MakeParam("@keiretu_mei", SqlDbType.VarChar, 42, "%" & strKeiretuMei & "%")) 'nñ¼
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 10, "0"))                              'æÁ

        'õÀs
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "dtKiretuJyouhouCount", paramList.ToArray())

        Return dsReturn.Tables("dtKiretuJyouhouCount")

    End Function

End Class

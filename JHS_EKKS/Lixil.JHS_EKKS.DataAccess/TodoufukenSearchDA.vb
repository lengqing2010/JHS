Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Imports System.Data
Imports System.Text

''' <summary>
''' s¹{§õPOPUP
''' </summary>
''' <remarks></remarks>
Public Class TodoufukenSearchDA

    ''' <summary>
    ''' us¹{§¼vÌõ·é
    ''' </summary>
    ''' <param name="strRows">õãÀ</param>
    ''' <param name="strTodoufukenMei">s¹{§¼</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    '''  <history>2012/11/19@F(åAîñVXe)@VKì¬</history>
    Public Function SelTodoufukenMei(ByVal strRows As String, _
                                     ByVal strTodoufukenMei As String) As Data.DataTable

        'EMABáQÎîñÌi[
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strRows, _
                                                                                          strTodoufukenMei)

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
            .AppendLine("    todouhuken_cd")                   's¹{§R[h
            .AppendLine("   ,todouhuken_mei")                  's¹{§¼
            .AppendLine("FROM")
            .AppendLine("   m_todoufuken WITH(READCOMMITTED)")  's¹{§}X^
            .AppendLine("WHERE")
            .AppendLine("1 = 1")

            If strTodoufukenMei <> "" Then
                .AppendLine("   AND todouhuken_mei LIKE @todoufuken_mei")
            End If
            .AppendLine("ORDER BY")
            .AppendLine("   todouhuken_mei")
        End With

        'o^
        paramList.Add(MakeParam("@todoufuken_mei", SqlDbType.VarChar, 42, "%" & strTodoufukenMei & "%")) 's¹{§¼

        'õÀs
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "dtTodoufukenMei", paramList.ToArray())

        Return dsReturn.Tables("dtTodoufukenMei")

    End Function

    ''' <summary>
    ''' õµ½f[^ðæ¾·é
    ''' </summary>
    ''' <param name="strTodoufukenMei">s¹{§¼</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/11/19@F(åAîñVXe)@VKì¬</history>
    Public Function SelTodoufukenMeiCount(ByVal strTodoufukenMei As String) As Data.DataTable

        'EMABáQÎîñÌi[
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strTodoufukenMei)

        'ßèf[^Zbg
        Dim dsReturn As New Data.DataSet

        'SQLRg
        Dim commandTextSb As New StringBuilder

        'o^i[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL¶
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("    COUNT(todouhuken_mei)")
            .AppendLine("FROM")
            .AppendLine("    m_todoufuken WITH(READCOMMITTED)")
            .AppendLine("WHERE")
            .AppendLine("     1 = 1")
            If strTodoufukenMei <> "" Then
                .AppendLine("    AND todouhuken_mei LIKE @todoufuken_mei")
            End If
        End With

        'o^
        paramList.Add(MakeParam("@todoufuken_mei", SqlDbType.VarChar, 42, "%" & strTodoufukenMei & "%")) 's¹{§¼

        'õÀs
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "dtTodoufukenMeiCount", paramList.ToArray())

        Return dsReturn.Tables("dtTodoufukenMeiCount")

    End Function

End Class

Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Imports System.Data
Imports System.Text

''' <summary>
''' væÇ_Á¿X@õPOPUP
''' </summary>
''' <remarks></remarks>
Public Class KeikakuKanriKameitenSearchDA

    ''' <summary>
    ''' uÁ¿XR[hvAuÁ¿X¼vAus¹{§¼vÌõ·é
    ''' </summary>
    ''' <param name="strRows">õãÀ</param>
    ''' <param name="strYear">Nx</param>
    ''' <param name="strKameitenCd">Á¿XR[h</param>
    ''' <param name="strKameitenMei">Á¿X¼</param>
    ''' <param name="blnTorikesi">æÁ</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/11/19@F(åAîñVXe)@VKì¬</history>
    Public Function SelKeikakuKanriKameiten(ByVal strRows As String, _
                                            ByVal strYear As String, _
                                            ByVal strKameitenCd As String, _
                                            ByVal strKameitenMei As String, _
                                            ByVal blnTorikesi As Boolean, ByVal blnAimai As Boolean) As Data.DataTable

        'EMABáQÎîñÌi[
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strRows, _
                                                                                          strYear, _
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
            .AppendLine("	kameiten_cd ") '--Á¿XR[h ")
            .AppendLine("	,kameiten_mei ") '--Á¿X¼ ")
            .AppendLine("	,todouhuken_mei ") '--s¹{§¼ ")
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN torikesi = '0' THEN ")
            .AppendLine("			'' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'æÁ' ")
            .AppendLine("		END AS torikesi ") '--æÁ ")
            .AppendLine("FROM ")
            .AppendLine("	m_keikaku_kameiten WITH(READCOMMITTED) ") '--væÇ_Á¿X}X^ ")
            .AppendLine("WHERE ")
            .AppendLine("	keikaku_nendo = @keikaku_nendo ") '--væNx ")
            If strKameitenCd <> "" Then
                .AppendLine("	AND ")
                .AppendLine("   kameiten_cd LIKE @kameiten_cd") '--Á¿XR[h ")
            End If
            If strKameitenMei <> "" Then
                .AppendLine("	AND ")
                .AppendLine("	kameiten_mei LIKE @kameiten_mei ") '--Á¿X¼ ")
            End If
            If blnTorikesi = True Then
                .AppendLine("	AND ")
                .AppendLine("	torikesi = @torikesi ") '--æÁ ")
            End If
            .AppendLine("ORDER BY")
            .AppendLine("   kameiten_cd ASC ")
        End With

        'o^
        paramList.Add(MakeParam("@keikaku_nendo", SqlDbType.VarChar, 4, strYear)) 'Á¿XR[h
        If blnAimai Then
            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 17, strKameitenCd & "%")) 'Á¿XR[h
            paramList.Add(MakeParam("@kameiten_mei", SqlDbType.VarChar, 42, "%" & strKameitenMei & "%")) 'Á¿X¼
        Else
            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 16, strKameitenCd)) 'Á¿XR[h
            paramList.Add(MakeParam("@kameiten_mei", SqlDbType.VarChar, 40, strKameitenMei)) 'Á¿X¼
        End If
        
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 10, "0"))                              'æÁ

        'õÀs
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "dtKeikakuKanriKameiten", paramList.ToArray())

        Return dsReturn.Tables("dtKeikakuKanriKameiten")

    End Function

    ''' <summary>
    ''' õµ½f[^ðæ¾·é
    ''' </summary>
    ''' <param name="strYear">Nx</param>
    ''' <param name="strKameitenCd">Á¿XR[h</param>
    ''' <param name="strKameitenMei">Á¿X¼</param>
    ''' <param name="blnTorikesi">æÁ</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/11/19@F(åAîñVXe)@VKì¬</history>
    Public Function SelKeikakuKanriKameitenCount(ByVal strYear As String, _
                                                 ByVal strKameitenCd As String, _
                                                 ByVal strKameitenMei As String, _
                                                 ByVal blnTorikesi As Boolean) As Data.DataTable

        'EMABáQÎîñÌi[
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strYear, _
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
            .AppendLine("FROM ")
            .AppendLine("	m_keikaku_kameiten WITH(READCOMMITTED) ") '--væÇ_Á¿X}X^ ")
            .AppendLine("WHERE ")
            .AppendLine("	keikaku_nendo = @keikaku_nendo ") '--væNx ")
            If strKameitenCd <> "" Then
                .AppendLine("	AND ")
                .AppendLine("   kameiten_cd LIKE @kameiten_cd") '--Á¿XR[h ")
            End If
            If strKameitenMei <> "" Then
                .AppendLine("	AND ")
                .AppendLine("	kameiten_mei LIKE @kameiten_mei ") '--Á¿X¼ ")
            End If
            If blnTorikesi = True Then
                .AppendLine("	AND ")
                .AppendLine("	torikesi = @torikesi ") '--æÁ ")
            End If
        End With

        'o^
        paramList.Add(MakeParam("@keikaku_nendo", SqlDbType.VarChar, 4, strYear)) 'Á¿XR[h
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 17, strKameitenCd & "%")) 'Á¿XR[h
        paramList.Add(MakeParam("@kameiten_mei", SqlDbType.VarChar, 42, "%" & strKameitenMei & "%")) 'Á¿X¼
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 10, "0"))                               'æÁ

        'õÀs
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "dtKeikakuKanriKameitenCount", paramList.ToArray())

        Return dsReturn.Tables("dtKeikakuKanriKameitenCount")

    End Function

End Class

Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Imports System.Data
Imports System.Text

''' <summary>
''' 計画管理_加盟店　検索POPUP
''' </summary>
''' <remarks></remarks>
Public Class KeikakuKanriKameitenSearchDA

    ''' <summary>
    ''' 「加盟店コード」、「加盟店名」、「都道府県名」の検索処理する
    ''' </summary>
    ''' <param name="strRows">検索上限件数</param>
    ''' <param name="strYear">年度</param>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="strKameitenMei">加盟店名</param>
    ''' <param name="blnTorikesi">取消</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/11/19　李宇(大連情報システム部)　新規作成</history>
    Public Function SelKeikakuKanriKameiten(ByVal strRows As String, _
                                            ByVal strYear As String, _
                                            ByVal strKameitenCd As String, _
                                            ByVal strKameitenMei As String, _
                                            ByVal blnTorikesi As Boolean, ByVal blnAimai As Boolean) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strRows, _
                                                                                          strYear, _
                                                                                          strKameitenCd, _
                                                                                          strKameitenMei, _
                                                                                          blnTorikesi)

        '戻りデータセット
        Dim dsReturn As New Data.DataSet

        'SQLコメント
        Dim commandTextSb As New StringBuilder

        'バラメタ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT")
            If strRows = "0" Then
                .AppendLine("   TOP 100")
            End If
            .AppendLine("	kameiten_cd ") '--加盟店コード ")
            .AppendLine("	,kameiten_mei ") '--加盟店名 ")
            .AppendLine("	,todouhuken_mei ") '--都道府県名 ")
            .AppendLine("	,CASE ")
            .AppendLine("		WHEN torikesi = '0' THEN ")
            .AppendLine("			'' ")
            .AppendLine("		ELSE ")
            .AppendLine("			'取消' ")
            .AppendLine("		END AS torikesi ") '--取消 ")
            .AppendLine("FROM ")
            .AppendLine("	m_keikaku_kameiten WITH(READCOMMITTED) ") '--計画管理_加盟店マスタ ")
            .AppendLine("WHERE ")
            .AppendLine("	keikaku_nendo = @keikaku_nendo ") '--計画年度 ")
            If strKameitenCd <> "" Then
                .AppendLine("	AND ")
                .AppendLine("   kameiten_cd LIKE @kameiten_cd") '--加盟店コード ")
            End If
            If strKameitenMei <> "" Then
                .AppendLine("	AND ")
                .AppendLine("	kameiten_mei LIKE @kameiten_mei ") '--加盟店名 ")
            End If
            If blnTorikesi = True Then
                .AppendLine("	AND ")
                .AppendLine("	torikesi = @torikesi ") '--取消 ")
            End If
            .AppendLine("ORDER BY")
            .AppendLine("   kameiten_cd ASC ")
        End With

        'バラメタ
        paramList.Add(MakeParam("@keikaku_nendo", SqlDbType.VarChar, 4, strYear)) '加盟店コード
        If blnAimai Then
            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 17, strKameitenCd & "%")) '加盟店コード
            paramList.Add(MakeParam("@kameiten_mei", SqlDbType.VarChar, 42, "%" & strKameitenMei & "%")) '加盟店名
        Else
            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 16, strKameitenCd)) '加盟店コード
            paramList.Add(MakeParam("@kameiten_mei", SqlDbType.VarChar, 40, strKameitenMei)) '加盟店名
        End If
        
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 10, "0"))                              '取消

        '検索実行
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "dtKeikakuKanriKameiten", paramList.ToArray())

        Return dsReturn.Tables("dtKeikakuKanriKameiten")

    End Function

    ''' <summary>
    ''' 検索したデータ件数を取得する
    ''' </summary>
    ''' <param name="strYear">年度</param>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="strKameitenMei">加盟店名</param>
    ''' <param name="blnTorikesi">取消</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/11/19　李宇(大連情報システム部)　新規作成</history>
    Public Function SelKeikakuKanriKameitenCount(ByVal strYear As String, _
                                                 ByVal strKameitenCd As String, _
                                                 ByVal strKameitenMei As String, _
                                                 ByVal blnTorikesi As Boolean) As Data.DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strYear, _
                                                                                          strKameitenCd, _
                                                                                          strKameitenMei, _
                                                                                          blnTorikesi)

        '戻りデータセット
        Dim dsReturn As New Data.DataSet

        'SQLコメント
        Dim commandTextSb As New StringBuilder

        'バラメタ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("    COUNT(kameiten_cd)")
            .AppendLine("FROM ")
            .AppendLine("	m_keikaku_kameiten WITH(READCOMMITTED) ") '--計画管理_加盟店マスタ ")
            .AppendLine("WHERE ")
            .AppendLine("	keikaku_nendo = @keikaku_nendo ") '--計画年度 ")
            If strKameitenCd <> "" Then
                .AppendLine("	AND ")
                .AppendLine("   kameiten_cd LIKE @kameiten_cd") '--加盟店コード ")
            End If
            If strKameitenMei <> "" Then
                .AppendLine("	AND ")
                .AppendLine("	kameiten_mei LIKE @kameiten_mei ") '--加盟店名 ")
            End If
            If blnTorikesi = True Then
                .AppendLine("	AND ")
                .AppendLine("	torikesi = @torikesi ") '--取消 ")
            End If
        End With

        'バラメタ
        paramList.Add(MakeParam("@keikaku_nendo", SqlDbType.VarChar, 4, strYear)) '加盟店コード
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 17, strKameitenCd & "%")) '加盟店コード
        paramList.Add(MakeParam("@kameiten_mei", SqlDbType.VarChar, 42, "%" & strKameitenMei & "%")) '加盟店名
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 10, "0"))                               '取消

        '検索実行
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "dtKeikakuKanriKameitenCount", paramList.ToArray())

        Return dsReturn.Tables("dtKeikakuKanriKameitenCount")

    End Function

End Class

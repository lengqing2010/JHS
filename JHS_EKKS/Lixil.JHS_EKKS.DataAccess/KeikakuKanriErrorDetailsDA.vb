Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Imports System.Data
Imports System.Text

Public Class KeikakuKanriErrorDetailsDA

    ''' <summary>
    '''計画管理表_取込エラー情報取得
    ''' </summary>
    ''' <param name="strEdiJouhouSakuseiDate">EDI情報作成日</param>
    ''' <param name="strSyoriDatetime">処理日時</param>
    ''' <returns>計画管理表_取込エラー情報テーブル</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2012/11/26　楊双(大連情報システム部)　新規作成</para>
    ''' </history>
    Public Function SelKeikakuTorikomiError(ByVal strEdiJouhouSakuseiDate As String, ByVal strSyoriDatetime As String) As DataTable

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name _
                                                                                          , strEdiJouhouSakuseiDate _
                                                                                          , strSyoriDatetime)
        '戻りデータセット
        Dim dsReturn As New Data.DataSet

        'SQLコメント
        Dim commandTextSb As New StringBuilder

        'バラメタ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   TOP 100 ")
            .AppendLine("	edi_jouhou_sakusei_date ")
            .AppendLine("	,gyou_no ")
            .AppendLine("	,syori_datetime ")
            .AppendLine("	,error_naiyou ")
            .AppendLine("FROM ")
            .AppendLine("	t_keikaku_torikomi_error WITH (READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	edi_jouhou_sakusei_date = @edi_jouhou_sakusei_date ")
            .AppendLine("	AND CONVERT(varchar(100), syori_datetime, 112) + REPLACE(CONVERT(varchar(100), syori_datetime, 114), ':', '') = @syori_datetime ")
            .AppendLine("ORDER BY ")
            .AppendLine("	gyou_no ")
        End With

        paramList.Add(SQLHelper.MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, strEdiJouhouSakuseiDate))
        paramList.Add(SQLHelper.MakeParam("@syori_datetime", SqlDbType.VarChar, 20, strSyoriDatetime))

        '実行
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "KeikakuTorikomiError", paramList.ToArray)

        '戻る値
        Return dsReturn.Tables("KeikakuTorikomiError")

    End Function

    ''' <summary>
    '''計画管理表_取込エラー情報件数取得
    ''' </summary>
    ''' <param name="strEdiJouhouSakuseiDate">EDI情報作成日</param>
    ''' <param name="strSyoriDatetime">処理日時</param>
    ''' <returns>計画管理表_取込エラー情報テーブル</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2012/11/26　楊双(大連情報システム部)　新規作成</para>
    ''' </history>
    Public Function SelKeikakuTorikomiErrorCount(ByVal strEdiJouhouSakuseiDate As String, ByVal strSyoriDatetime As String) As String

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name _
                                                                                          , strEdiJouhouSakuseiDate _
                                                                                          , strSyoriDatetime)
        '戻りデータセット
        Dim dsReturn As New Data.DataSet

        'SQLコメント
        Dim commandTextSb As New StringBuilder

        'バラメタ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	Count(edi_jouhou_sakusei_date) AS count ")
            .AppendLine("FROM ")
            .AppendLine("	t_keikaku_torikomi_error WITH (READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	edi_jouhou_sakusei_date = @edi_jouhou_sakusei_date ")
            .AppendLine("	AND CONVERT(varchar(100), syori_datetime, 112) + REPLACE(CONVERT(varchar(100), syori_datetime, 114), ':', '') = @syori_datetime ")
        End With

        paramList.Add(SQLHelper.MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, strEdiJouhouSakuseiDate))
        paramList.Add(SQLHelper.MakeParam("@syori_datetime", SqlDbType.VarChar, 20, strSyoriDatetime))

        '実行
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "KeikakuTorikomiErrorCount", paramList.ToArray)

        '戻る値
        Return dsReturn.Tables("KeikakuTorikomiErrorCount").Rows(0).Item("count").ToString

    End Function

End Class

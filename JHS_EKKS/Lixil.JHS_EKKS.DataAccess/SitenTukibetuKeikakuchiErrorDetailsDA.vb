Option Explicit On
Option Strict On
Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Imports System.Data
Imports System.Text

''' <summary>
''' 支店 月別計画値 EXCEL取込エラー
''' </summary>
''' <remarks></remarks>
Public Class SitenTukibetuKeikakuchiErrorDetailsDA

    ''' <summary>
    ''' エラー内容を取得する
    ''' </summary>
    ''' <param name="strSyoriDatetime">処理日時</param>
    ''' <param name="strEdiJouhouSakuseiDate">EDI情報作成日</param>
    ''' <returns>Data.DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/11/27　李宇(大連情報システム部)　新規作成</history>
    Public Function SelErrorJyouhou(ByVal strSyoriDatetime As String, ByVal strEdiJouhouSakuseiDate As String) As Data.DataTable

        '戻りデータセット
        Dim dsReturn As New Data.DataSet

        'SQLコメント
        Dim commandTextSb As New StringBuilder

        'バラメタ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT")
            .AppendLine("   TOP 100")
            .AppendLine("    gyou_no")                   '行NO
            .AppendLine("   ,error_naiyou")              'エラー内容
            .AppendLine("FROM")
            .AppendLine("   t_siten_tukibetu_torikomi_error WITH(READCOMMITTED)")  '支店別月別計画管理表_取込エラー情報テーブル
            .AppendLine("WHERE")
            '.AppendLine("   syori_datetime = @syori_datetime")
            .AppendLine("       CONVERT(varchar(100),syori_datetime,112) + REPLACE(CONVERT(varchar(100),syori_datetime,114),':','') = @syori_datetime")
            .AppendLine("   AND edi_jouhou_sakusei_date = @edi_jouhou_sakusei_date")
            .AppendLine("ORDER BY")
            .AppendLine("   gyou_no")
        End With

        'バラメタ
        paramList.Add(MakeParam("@syori_datetime", SqlDbType.VarChar, 40, strSyoriDatetime)) '処理日時
        paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, strEdiJouhouSakuseiDate)) 'EDI情報作成日

        '検索実行
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "dtErrorJyouhouCount", paramList.ToArray())

        Return dsReturn.Tables("dtErrorJyouhouCount")

    End Function

    ''' <summary>
    ''' エラー内容のデータすうを取得する
    ''' </summary>
    ''' <param name="strSyoriDatetime">処理日時</param>
    ''' <param name="strEdiJouhouSakuseiDate">EDI情報作成日</param>
    ''' <returns>Data.DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/11/27　李宇(大連情報システム部)　新規作成</history>
    Public Function SelErrorJyouhouCount(ByVal strSyoriDatetime As String, ByVal strEdiJouhouSakuseiDate As String) As Data.DataTable

        '戻りデータセット
        Dim dsReturn As New Data.DataSet

        'SQLコメント
        Dim commandTextSb As New StringBuilder

        'バラメタ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT")
            .AppendLine("   COUNT(*)")              'エラー内容
            .AppendLine("FROM")
            .AppendLine("   t_siten_tukibetu_torikomi_error WITH(READCOMMITTED)")  '支店別月別計画管理表_取込エラー情報テーブル
            .AppendLine("WHERE")
            '.AppendLine("   syori_datetime = @syori_datetime")
            .AppendLine("       CONVERT(varchar(100),syori_datetime,112) + REPLACE(CONVERT(varchar(100),syori_datetime,114),':','') = @syori_datetime")
            .AppendLine("   AND edi_jouhou_sakusei_date = @edi_jouhou_sakusei_date")
        End With

        'バラメタ
        paramList.Add(MakeParam("@syori_datetime", SqlDbType.VarChar, 40, strSyoriDatetime)) '処理日時
        paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, strEdiJouhouSakuseiDate)) 'EDI情報作成日

        '検索実行
        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "dtErrorJyouhouCount", paramList.ToArray())

        Return dsReturn.Tables("dtErrorJyouhouCount")

    End Function

End Class

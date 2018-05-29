Imports System.Data.SqlClient
Imports System.Data
Imports System.Text
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Public Class YosinKanriSearchDataAccess
    Public Function SelYosinKanri() _
                                    As DataAccess.YosinKanriDataSet.YosinKanriTableDataTable

        '---  接続文字列  ---

        Dim sbSql As New StringBuilder
        Dim lstParam As New List(Of SqlClient.SqlParameter)
        Dim strCon As String = System.Configuration.ConfigurationManager.ConnectionStrings("EarthConnectionString").ConnectionString

        '---  返却値格納用データセット  ---
        Dim dsYoshinKanri As New DataAccess.YosinKanriDataSet

        With sbSql
            '---  Sql文の生成  ---
            .AppendLine("SELECT ")
            .AppendLine("   TOP 100")
            .AppendLine("   nayose_cd")
            .AppendLine(",	nayose_mei1")
            .AppendLine(",	nayose_yosin_gaku")
            .AppendLine(",	keikoku_jyoukyou")
            .AppendLine("  FROM m_yosin_kanri ")
            .AppendLine("  WITH (READCOMMITTED) ")
            '.AppendLine("  WHERE ")
            '.AppendLine("     A.kameiten_cd = @kameiten_cd) AS hsh")
            .AppendLine("ORDER BY nayose_cd")
        End With

        'パラメータ 
        'lstParam.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))

        SQLHelper.FillDataset(strCon, CommandType.Text, _
        sbSql.ToString, _
        dsYoshinKanri, dsYoshinKanri.YosinKanriTable.TableName)
        'dsYoshinKanri, dsYoshinKanri.YosinKanriTable.TableName, lstParam.ToArray)

        lstParam.Clear()

        Return dsYoshinKanri.YosinKanriTable

    End Function
    Public Function SelYosinKanriCount() As Integer

        '---  接続文字列  ---

        Dim sbSql As New StringBuilder
        Dim lstParam As New List(Of SqlClient.SqlParameter)
        Dim strCon As String = System.Configuration.ConfigurationManager.ConnectionStrings("EarthConnectionString").ConnectionString

        '---  返却値格納用データセット  ---
        Dim dsYoshinKanri As New DataAccess.YosinKanriDataSet
        Dim intYoshinKanri As Integer
        intYoshinKanri = 0

        With sbSql
            '---  Sql文の生成  ---
            .AppendLine("SELECT COUNT(nayose_cd) AS num")
            .AppendLine("  FROM m_yosin_kanri ")
            .AppendLine("  WITH (READCOMMITTED) ")
            '.AppendLine("  WHERE ")
            '.AppendLine("     A.kameiten_cd = @kameiten_cd) AS hsh")
        End With

        'パラメータ 
        'lstParam.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))

        SQLHelper.FillDataset(strCon, CommandType.Text, _
        sbSql.ToString, _
        dsYoshinKanri, dsYoshinKanri.YosinKanriConutTable.TableName)
        'dsYoshinKanri, dsYoshinKanri.YosinKanriTable.TableName, lstParam.ToArray)

        lstParam.Clear()
        intYoshinKanri = Convert.ToInt32(dsYoshinKanri.YosinKanriConutTable.Rows(0).Item("num"))
        Return intYoshinKanri

    End Function
End Class

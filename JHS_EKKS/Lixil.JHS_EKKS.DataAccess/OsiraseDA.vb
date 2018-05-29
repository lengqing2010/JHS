Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Imports System.Data

Public Class OsiraseDA

    ''' <summary>
    ''' お知らせレコードを取得します
    ''' </summary>
    ''' <returns>お知らせデータテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetOsiraseData() As DataTable
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name)

        'SQLコメント
        Dim sqlBuffer As New System.Text.StringBuilder

        '戻りデータセット
        Dim dsInfo As New Data.DataSet

        'バラメタ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        Dim commandText As String = " SELECT " & _
                                    "   nengappi, " & _
                                    "   nyuuryoku_busyo, " & _
                                    "   nyuuryoku_mei, " & _
                                    "   hyouji_naiyou, " & _
                                    "   link_saki" & _
                                    " FROM " & _
                                    "    t_osirase WITH (READCOMMITTED) " & _
                                    " WHERE " & _
                                    "    torikesi = 0  " & _
                                    " ORDER BY nengappi desc"

      

        FillDataset(ManagerDA.Connection, CommandType.Text, commandText, _
            dsInfo, "dsInfo")



        Return dsInfo.Tables("dsInfo")

    End Function

End Class

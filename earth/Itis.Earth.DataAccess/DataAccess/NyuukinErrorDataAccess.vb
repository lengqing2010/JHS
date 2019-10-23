Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' 入金エラー確認に関する処理を行うデータアクセスクラスです
''' </summary>
''' <remarks></remarks>
Public Class NyuukinErrorDataAccess
    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

#Region "メンバ変数"
    Dim connStr As String = ConnectionManager.Instance.ConnectionString
#End Region

#Region "初期処理"
    ''' <summary>
    ''' 入金エラー情報を取得します
    ''' </summary>
    ''' <param name="strEdiJouhou">EDI情報作成日</param>
    ''' <returns>入金エラー情報を格納したデータテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetNyuukinErrData(ByVal intFileKbn As Integer, ByVal strEdiJouhou As String) As NyuukinErrorDataSet.ErrorJouhouDataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetNyuukinErrData" _
                                                    , intFileKbn _
                                                    , strEdiJouhou)
        Dim nyuukinErrDataSet As New NyuukinErrorDataSet
        Dim nyuukinErrTable As NyuukinErrorDataSet.ErrorJouhouDataTable
        Dim commandTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        commandTextSb.Append(" SELECT ")
        commandTextSb.Append("      edi_jouhou_sakusei_date ")
        commandTextSb.Append("     ,gyou_no ")
        commandTextSb.Append("     ,syori_datetime ")
        commandTextSb.Append("     ,group_cd ")
        commandTextSb.Append("     ,kokyaku_cd ")
        commandTextSb.Append("     ,tekiyou ")
        commandTextSb.Append("     ,nyuukin_gaku ")
        commandTextSb.Append("     ,syouhin_cd")
        commandTextSb.Append("   FROM ")
        commandTextSb.Append("      t_nyuukin_error ")
        commandTextSb.Append("  WHERE ")
        If intFileKbn = 0 Then
            commandTextSb.Append("     file_kbn IS NULL ")
        Else
            commandTextSb.Append("     file_kbn = @FILEKBN ")
        End If
        commandTextSb.Append("    AND ")
        commandTextSb.Append("     edi_jouhou_sakusei_date = @EDIJOUHOUSAKUSEIDATE ")
        If intFileKbn = 0 Then
            commandTextSb.Append("  ORDER BY group_cd ")
            commandTextSb.Append("          ,kokyaku_cd ")
            commandTextSb.Append("          ,tekiyou ")
            commandTextSb.Append("          ,gyou_no ")
        Else
            commandTextSb.Append("  ORDER BY gyou_no ")
        End If

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
                                        SQLHelper.MakeParam("@FILEKBN", SqlDbType.Int, 4, intFileKbn) _
                                        , SQLHelper.MakeParam("@EDIJOUHOUSAKUSEIDATE", SqlDbType.VarChar, 40, strEdiJouhou)}
        ' 検索実行
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            nyuukinErrDataSet, nyuukinErrDataSet.ErrorJouhou.TableName, cmdParams)
        nyuukinErrTable = nyuukinErrDataSet.ErrorJouhou

        Return nyuukinErrTable
    End Function
#End Region
End Class

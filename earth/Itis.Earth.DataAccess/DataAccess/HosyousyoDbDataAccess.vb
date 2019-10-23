Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' 保証書DB格納先管理マスタへの接続クラス
''' </summary>
''' <remarks></remarks>
Public Class HosyousyoDbDataAccess

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ''' <summary>
    ''' 保証書DB情報を取得
    ''' </summary>
    ''' <param name="kubun"></param>
    ''' <param name="ym"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetHosyousyoDbInfo(ByVal kubun As String, ByVal ym As String) As DataTable
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetHosyousyoDbInfo", kubun, ym)

        Dim cmnDtAcc As New CmnDataAccess
        Dim cmdTextSb As New StringBuilder()

        'パラメータ
        Const strParamKbn As String = "@KUBUN"
        Const strParamDate As String = "@DATE"

        cmdTextSb.AppendLine(" SELECT ")
        cmdTextSb.AppendLine("      kbn ")
        cmdTextSb.AppendLine("    , date_from ")
        cmdTextSb.AppendLine("    , date_to ")
        cmdTextSb.AppendLine("    , kakunousaki_file_pass ")
        cmdTextSb.AppendLine("    , add_login_user_id ")
        cmdTextSb.AppendLine("    , add_datetime ")
        cmdTextSb.AppendLine("    , upd_login_user_id ")
        cmdTextSb.AppendLine("    , upd_datetime ")
        cmdTextSb.AppendLine(" FROM ")
        cmdTextSb.AppendLine("      m_hosyousyo_db_kanri ")
        cmdTextSb.AppendLine(" WHERE ")
        cmdTextSb.AppendLine("      kbn = " & strParamKbn)
        cmdTextSb.AppendLine("  AND date_from <= " & strParamDate)
        cmdTextSb.AppendLine("  AND date_to >= " & strParamDate)

        ' パラメータへ設定
        Dim sqlParams() As SqlParameter = {SQLHelper.MakeParam(strParamKbn, SqlDbType.Char, 1, kubun), _
                                           SQLHelper.MakeParam(strParamDate, SqlDbType.VarChar, 6, ym)}

        'データ取得＆返却
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, sqlParams)
    End Function

End Class

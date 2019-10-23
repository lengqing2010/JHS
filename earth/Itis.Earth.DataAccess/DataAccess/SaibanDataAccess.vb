Imports System.text
Imports System.Data.SqlClient

Public Class SaibanDataAccess
    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName
    Dim connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>
    ''' 指定した採番種別の最終NOを取得する
    ''' </summary>
    ''' <param name="strSyubetu">採番種別</param>
    ''' <returns>最終NO</returns>
    ''' <remarks></remarks>
    Public Function getSaibanRecord(ByVal strSyubetu As String) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getSaibanRecord", strSyubetu)

        'SQL文の生成
        Dim cmdTextSb As New StringBuilder
        Dim cmdParams() As SqlParameter
        Dim objResult As Object
        Dim intResult As Integer

        cmdTextSb.Append(" SELECT")
        cmdTextSb.Append("      saisyuu_no")
        cmdTextSb.Append(" FROM")
        cmdTextSb.Append("      m_saiban")
        cmdTextSb.Append(" WHERE")
        cmdTextSb.Append("      saiban_syubetu = @SAIBANSYUBETU")

        cmdParams = New SqlParameter() {SQLHelper.MakeParam("@SAIBANSYUBETU", SqlDbType.VarChar, 2, strSyubetu)}

        'データの取得
        objResult = ExecuteScalar(connStr _
                                , CommandType.Text _
                                , cmdTextSb.ToString _
                                , cmdParams)
        If objResult Is Nothing Then
            intResult = Integer.MinValue
        Else
            intResult = Integer.Parse(objResult)
        End If

        'データ返却
        Return intResult

    End Function

    ''' <summary>
    ''' 最終NOをカウントアップする
    ''' </summary>
    ''' <param name="strSyubetu">採番種別</param>
    ''' <param name="strLoginUserId">更新ユーザーID</param>
    ''' <returns>更新の成否(True：成功／False：失敗)</returns>
    ''' <remarks></remarks>
    Public Function updCntUpLastNo(ByVal strSyubetu As String, ByVal strLoginUserId As String) As Boolean
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".updCntUpLastNo", strSyubetu)
        'SQL文の生成
        Dim cmdTextSb As New StringBuilder
        Dim cmdParams() As SqlParameter
        Dim intResult As Integer

        cmdTextSb.Append(" UPDATE")
        cmdTextSb.Append("      m_saiban")
        cmdTextSb.Append(" SET")
        cmdTextSb.Append("      saisyuu_no =")
        cmdTextSb.Append("     (SELECT")
        cmdTextSb.Append("           isnull(max(saisyuu_no), 0) +1")
        cmdTextSb.Append("      FROM")
        cmdTextSb.Append("           m_saiban")
        cmdTextSb.Append("      WHERE")
        cmdTextSb.Append("           saiban_syubetu = @SAIBANSYUBETU")
        cmdTextSb.Append("     )")
        cmdTextSb.Append("    , upd_login_user_id = @LOGINUSERID")
        cmdTextSb.Append("    , upd_datetime = @UPDDATETIME")
        cmdTextSb.Append(" WHERE")
        cmdTextSb.Append("      saiban_syubetu = @SAIBANSYUBETU")

        cmdParams = New SqlParameter() {SQLHelper.MakeParam("@SAIBANSYUBETU", SqlDbType.VarChar, 2, strSyubetu) _
                                        , SQLHelper.MakeParam("@LOGINUSERID", SqlDbType.VarChar, 30, strLoginUserId) _
                                        , SQLHelper.MakeParam("@UPDDATETIME", SqlDbType.DateTime, 16, DateTime.Now)}

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    cmdTextSb.ToString(), _
                                    cmdParams)

        ' 更新に失敗した場合、False
        If intResult < 1 Then
            Return False
        End If

        Return True

    End Function

End Class

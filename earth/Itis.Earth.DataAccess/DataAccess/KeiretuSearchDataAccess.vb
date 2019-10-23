Imports System.text
Imports System.Data.SqlClient

Public Class KeiretuSearchDataAccess
    ''' <summary>
    ''' コネクションストリング
    ''' </summary>
    ''' <remarks></remarks>
    Private connStr As String = ConnectionManager.Instance.ConnectionString

#Region "系列マスタ検索"
    ''' <summary>
    ''' 系列マスタの検索を行う
    ''' </summary>
    ''' <param name="strKubun">区分</param>
    ''' <param name="strKeiretuCd">系列ｺｰﾄﾞ</param>
    ''' <param name="strKeiretuNm">系列名</param>
    ''' <param name="blnDelete">取消対象フラグ</param>
    ''' <returns>KeiretuSearchTableDataTable</returns>
    ''' <remarks></remarks>
    Public Function getKeiretuKensakuData(ByVal strKubun As String, _
                                      ByVal strKeiretuCd As String, _
                                      ByVal strKeiretuNm As String, _
                                      ByVal blnDelete As Boolean) As KeiretuSearchDataSet.KeiretuSearchTableDataTable

        ' パラメータ
        Const strParamKeiretuCd As String = "@KEIRETUCD"
        Const strParamKeiretuNm As String = "@KEIRETUNM"
        Const strParamKubun1 As String = "@KUBUN1"
        Const strParamKubun2 As String = "@KUBUN2"
        Const strParamKubun3 As String = "@KUBUN3"

        Dim tmpKbn1 As String = ""
        Dim tmpKbn2 As String = ""
        Dim tmpKbn3 As String = ""
        Dim kbnCount As Integer = 1


        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append("SELECT " & _
                                "  kbn, " & _
                                "  torikesi, " & _
                                "  keiretu_cd, " & _
                                "  keiretu_mei ")
        commandTextSb.Append("FROM m_keiretu ")
        commandTextSb.Append("WHERE ")
        If blnDelete = True Then
            commandTextSb.Append(" torikesi = 0 ")
        Else
            commandTextSb.Append(" 0 = 0 ")
        End If
        If strKubun <> "" Then
            commandTextSb.Append(" AND kbn IN ( ")
            Dim arrKbn As Array = strKubun.Split(",")
            For Each tmpKbn As String In arrKbn
                If kbnCount = 1 Then
                    tmpKbn1 = arrKbn(0)
                    commandTextSb.Append(strParamKubun1)
                End If
                If kbnCount = 2 Then
                    tmpKbn2 = arrKbn(1)
                    commandTextSb.Append("," & strParamKubun2)
                End If
                If kbnCount = 3 Then
                    tmpKbn3 = arrKbn(2)
                    commandTextSb.Append("," & strParamKubun3)
                End If
                kbnCount += 1
            Next
            commandTextSb.Append(" ) ")
        End If
        If strKeiretuCd <> "" Then
            commandTextSb.Append(" AND keiretu_cd Like " & strParamKeiretuCd)
        End If
        If strKeiretuNm <> "" Then
            commandTextSb.Append(" AND keiretu_mei Like " & strParamKeiretuNm)
        End If
        commandTextSb.Append(" ORDER BY keiretu_cd ")

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamKeiretuCd, SqlDbType.VarChar, 5, strKeiretuCd & Chr(37)), _
             SQLHelper.MakeParam(strParamKeiretuNm, SqlDbType.VarChar, 42, strKeiretuNm & Chr(37)), _
             SQLHelper.MakeParam(strParamKubun1, SqlDbType.Char, 1, tmpKbn1), _
             SQLHelper.MakeParam(strParamKubun2, SqlDbType.Char, 1, tmpKbn2), _
             SQLHelper.MakeParam(strParamKubun3, SqlDbType.Char, 1, tmpKbn3) _
            }

        ' データの取得
        Dim tmpDataSet As New KeiretuSearchDataSet()

        ' 検索実行
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            tmpDataSet, tmpDataSet.KeiretuSearchTable.TableName, commandParameters)

        Dim KeiretuTable As KeiretuSearchDataSet.KeiretuSearchTableDataTable = _
                    tmpDataSet.KeiretuSearchTable

        Return KeiretuTable

    End Function
#End Region

End Class

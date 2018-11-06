Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Imports Itis.Earth.DataAccess
Imports System.text
Imports System.Data.SqlClient

Public Class SalesforceDataAccess
    Dim connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>
    '''salesforce項目_編集非活性フラグを取得します
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetSalesforceHikasseiFlg(ByVal strKameitenCd As String) As String



        ' DataSetインスタンスの生成()
        Dim dsCommonSearch As New DataSet

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        Dim sb As New System.Text.StringBuilder
        sb.Append("select isnull(salesforce_hikassei_flg,'') from m_kameiten a " & vbCrLf)
        sb.Append("left join m_data_kbn b " & vbCrLf)
        sb.Append("on a.kbn = b.kbn " & vbCrLf)
        sb.Append("where kameiten_cd = @kameiten_cd")

        'パラメータの設定
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 6, strKameitenCd))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, sb.ToString(), dsCommonSearch, _
                    "tmp", paramList.ToArray)

        If dsCommonSearch.Tables(0).Rows.Count = 0 Then
            Return ""
        Else
            Return dsCommonSearch.Tables(0).Rows(0).Item(0).ToString
        End If

    End Function


    ''' <summary>
    '''salesforce項目_編集非活性フラグを取得します
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetSalesforceHikasseiFlgByKbn(ByVal kbn As String) As String

        ' DataSetインスタンスの生成()
        Dim dsCommonSearch As New DataSet

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        Dim sb As New System.Text.StringBuilder
        sb.Append("select isnull(salesforce_hikassei_flg,'') from m_data_kbn a " & vbCrLf)
        sb.Append("where kbn = @kbn")

        'パラメータの設定
        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, kbn))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, sb.ToString(), dsCommonSearch, _
                    "tmp", paramList.ToArray)

        If dsCommonSearch.Tables(0).Rows.Count = 0 Then
            Return ""
        Else
            Return dsCommonSearch.Tables(0).Rows(0).Item(0).ToString
        End If

    End Function
End Class

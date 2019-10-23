Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' ビルダー情報の取得クラスです
''' </summary>
''' <remarks></remarks>
Public Class BuilderDataAccess

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    Dim connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>
    ''' ビルダー情報を取得します
    ''' </summary>
    ''' <param name="kameitenCd">加盟店コード</param>
    ''' <returns>ビルダー情報テーブル</returns>
    ''' <remarks></remarks>
    Public Function GetBuilderData(ByVal kameitenCd As String) As BuilderDataSet.BuilderTableDataTable

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetBuilderData", _
                                            kameitenCd)

        ' パラメータ
        Const strParamKameitenCd As String = "@KAMEITENCD"

        Dim commandText As String = " SELECT " & _
                                    "     k.kameiten_cd, " & _
                                    "     k.nyuuryoku_no, " & _
                                    "     m.meisyou As tyuuijikou_syubetu, " & _
                                    "     k.nyuuryoku_date, " & _
                                    "     k.uketukesya_mei, " & _
                                    "     k.naiyou " & _
                                    " FROM " & _
                                    "     m_kameiten_tyuuijikou k WITH (READCOMMITTED)" & _
                                    " LEFT OUTER JOIN  " & _
                                    "     m_meisyou m WITH (READCOMMITTED) ON m.code = k.tyuuijikou_syubetu   " & _
                                    "                 AND m.meisyou_syubetu = '10' " & _
                                    " WHERE " & _
                                    "     k.kameiten_cd = " & strParamKameitenCd & _
                                    " ORDER BY k.tyuuijikou_syubetu, k.nyuuryoku_date desc, k.nyuuryoku_no desc "

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamKameitenCd, SqlDbType.VarChar, 5, kameitenCd)}

        ' データの取得
        Dim BuilderDataSet As New BuilderDataSet()

        SQLHelper.FillDataset(connStr, CommandType.Text, commandText, _
            BuilderDataSet, BuilderDataSet.BuilderTable.TableName, commandParameters)

        Dim BuilderTable As BuilderDataSet.BuilderTableDataTable = BuilderDataSet.BuilderTable

        Return BuilderTable

    End Function

    ''' <summary>
    ''' 加盟店コードの注意事項に13が存在しない場合、エラーを返す。
    ''' </summary>
    ''' <param name="kameitenCd">加盟店コード</param>
    ''' <returns>True or False</returns>
    ''' <remarks>※加盟店コード=未入力時、スルー</remarks>
    Public Function ChkBuilderData13(ByVal kameitenCd As String) As Boolean

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".ChkBuilderData", _
                                            kameitenCd)

        '加盟店コード=未入力時、スルー
        If kameitenCd.Trim = String.Empty Then
            Return True
        End If

        ' パラメータ
        Const strParamKameitenCd As String = "@KAMEITENCD"

        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append(" SELECT ")
        commandTextSb.Append("     COUNT(k.kameiten_cd) ")
        commandTextSb.Append(" FROM ")
        commandTextSb.Append("     m_kameiten_tyuuijikou k WITH (READCOMMITTED)")
        commandTextSb.Append(" WHERE ")
        commandTextSb.Append("     k.kameiten_cd = " & strParamKameitenCd)
        commandTextSb.Append(" AND ")
        commandTextSb.Append("     k.tyuuijikou_syubetu = '13'")

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamKameitenCd, SqlDbType.VarChar, 5, kameitenCd)}

        ' データの取得
        Dim data As Object = Nothing

        ' 検索実行
        data = SQLHelper.ExecuteScalar(connStr, _
                                       CommandType.Text, _
                                       commandTextSb.ToString(), _
                                       commandParameters)

        ' 取得出来ない場合、空白を返却
        If data Is Nothing OrElse IsDBNull(data) Then
            Return False
        End If
        '該当データなし
        If data = 0 Then
            Return False
        End If
        '該当データあり
        If data > 0 Then
            Return True
        End If
        Return False

    End Function

    ''' <summary>
    ''' 加盟店コードの注意事項に 55 が存在する場合、TRUE を返す。
    ''' </summary>
    ''' <param name="kameitenCd">加盟店コード</param>
    ''' <returns>True or False</returns>
    ''' <remarks>※加盟店コード=未入力時、False </remarks>
    Public Function ChkBuilderData55(ByVal kameitenCd As String) As Boolean

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".ChkBuilderData", _
                                            kameitenCd)

        '加盟店コード=未入力時、スルー
        If kameitenCd.Trim = String.Empty Then
            Return False
        End If

        ' パラメータ
        Const strParamKameitenCd As String = "@KAMEITENCD"

        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append(" SELECT ")
        commandTextSb.Append("     COUNT(k.kameiten_cd) ")
        commandTextSb.Append(" FROM ")
        commandTextSb.Append("     m_kameiten_tyuuijikou k WITH (READCOMMITTED)")
        commandTextSb.Append(" WHERE ")
        commandTextSb.Append("     k.kameiten_cd = " & strParamKameitenCd)
        commandTextSb.Append(" AND ")
        commandTextSb.Append("     k.tyuuijikou_syubetu = 55")

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamKameitenCd, SqlDbType.VarChar, 5, kameitenCd)}

        ' データの取得
        Dim data As Object = Nothing

        ' 検索実行
        data = SQLHelper.ExecuteScalar(connStr, _
                                       CommandType.Text, _
                                       commandTextSb.ToString(), _
                                       commandParameters)

        ' 取得出来ない場合、空白を返却
        If data Is Nothing OrElse IsDBNull(data) Then
            Return False
        End If
        '該当データなし
        If data = 0 Then
            Return False
        End If
        '該当データあり
        If data > 0 Then
            Return True
        End If
        Return False

    End Function
End Class

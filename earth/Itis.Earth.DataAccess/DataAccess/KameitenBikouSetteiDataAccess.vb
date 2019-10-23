Imports System.text
Imports System.Data.SqlClient

Public Class KameitenBikouSetteiDataAccess

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ''' <summary>
    ''' コネクションストリング
    ''' </summary>
    ''' <remarks></remarks>
    Private connStr As String = ConnectionManager.Instance.ConnectionString

#Region "備考種別の存在チェック"
    ''' <summary>
    ''' 備考種別の存在チェックをします
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="intBikouSyubetu">備考種別</param>
    ''' <param name="blnDelete">True:取消データを取得対象から除外 False:取消データを対象</param>
    ''' <returns>該当データ件数</returns>
    ''' <remarks></remarks>
    Public Function ChkBikouSyubetu( _
                                      ByVal strKameitenCd As String, _
                                      ByVal intBikouSyubetu As Integer, _
                                      Optional ByVal blnDelete As Boolean = True _
                                      ) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".ChkBikouSyubetu", _
                                                    strKameitenCd, _
                                                    intBikouSyubetu, _
                                                    blnDelete _
                                                    )
        '未入力チェック
        If strKameitenCd = "" Or IsNumeric(intBikouSyubetu) = False Then
            Return 0
        End If

        ' パラメータ
        Const strParamKameitenCd As String = "@KAMEITENCD"

        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append(" SELECT ")
        commandTextSb.Append("    COUNT(bs.kameiten_cd) ")
        commandTextSb.Append(" FROM ")
        commandTextSb.Append("    m_kameiten_bikou_settei bs WITH (READCOMMITTED) ")
        commandTextSb.Append(" WHERE  ")
        commandTextSb.Append("    bs.kameiten_cd = " & strParamKameitenCd)
        commandTextSb.Append(" AND  ")
        commandTextSb.Append("    bs.bikou_syubetu = " & intBikouSyubetu)

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamKameitenCd, SqlDbType.VarChar, 5, strKameitenCd)}

        ' データの取得
        Dim data As Object = Nothing

        ' 検索実行
        data = SQLHelper.ExecuteScalar(connStr, _
                                       CommandType.Text, _
                                       commandTextSb.ToString(), _
                                       commandParameters)

        ' 取得出来ない場合、空白を返却
        If data Is Nothing OrElse IsDBNull(data) Then
            Return 0
        End If

        Return data

    End Function
#End Region

End Class

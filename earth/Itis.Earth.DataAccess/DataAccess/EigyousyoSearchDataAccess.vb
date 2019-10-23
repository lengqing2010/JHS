Imports System.text
Imports System.Data.SqlClient

Public Class EigyousyoSearchDataAccess

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ''' <summary>
    ''' コネクションストリング
    ''' </summary>
    ''' <remarks></remarks>
    Private connStr As String = ConnectionManager.Instance.ConnectionString

#Region "営業所マスタ検索"
    ''' <summary>
    ''' 営業所マスタの検索を行う
    ''' </summary>
    ''' <param name="strEigyousyoCd">営業所コード</param>
    ''' <param name="strEigyousyoKana">営業所カナ</param>
    ''' <param name="blnDelete">取消対象フラグ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEigyosyoKensakuData(ByVal strEigyousyoCd As String, _
                                           ByVal strEigyousyoKana As String, _
                                           ByVal blnDelete As Boolean) As EigyousyoSearchDataSet.EigyousyoSearchTableDataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetEigyosyoKensakuData", _
                                            strEigyousyoCd, _
                                            strEigyousyoKana, _
                                            blnDelete)

        ' パラメータ
        Const strParamEigyousyoCd As String = "@EIGYOUSYOCD"
        Const strParamEigyousyoKana As String = "@EIGYOUSYOKANA"

        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append("SELECT ")
        commandTextSb.Append("  eigyousyo_cd ")
        commandTextSb.Append("  ,torikesi ")
        commandTextSb.Append("  ,eigyousyo_mei ")
        commandTextSb.Append("  ,eigyousyo_kana ")
        commandTextSb.Append("  ,seikyuu_saki_cd ")
        commandTextSb.Append("  ,seikyuu_saki_brc ")
        commandTextSb.Append("  ,seikyuu_saki_kbn ")
        commandTextSb.Append("  ,seikyuu_saki_mei ")
        commandTextSb.Append("  ,seikyuu_saki_kana ")
        commandTextSb.Append(" FROM m_eigyousyo WITH (READCOMMITTED) ")
        commandTextSb.Append(" WHERE 0 = 0 ")
        If strEigyousyoCd <> "" Then
            commandTextSb.Append(" AND eigyousyo_cd like " & strParamEigyousyoCd)
        End If
        If strEigyousyoKana <> "" Then
            commandTextSb.Append(" AND eigyousyo_kana Like " & strParamEigyousyoKana)
        End If
        If blnDelete Then
            commandTextSb.Append(" AND torikesi = 0 ")
        End If

        '' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamEigyousyoCd, SqlDbType.VarChar, 6, strEigyousyoCd & Chr(37)), _
             SQLHelper.MakeParam(strParamEigyousyoKana, SqlDbType.VarChar, 22, Chr(37) & strEigyousyoKana & Chr(37)) _
             }

        '' データの取得
        Dim EigyousyoDataSet As New EigyousyoSearchDataSet()

        '' 検索実行
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            EigyousyoDataSet, EigyousyoDataSet.EigyousyoSearchTable.TableName, commandParameters)

        Dim eigyousyoTable As EigyousyoSearchDataSet.EigyousyoSearchTableDataTable = _
                    EigyousyoDataSet.EigyousyoSearchTable

        Return eigyousyoTable

    End Function
#End Region


End Class

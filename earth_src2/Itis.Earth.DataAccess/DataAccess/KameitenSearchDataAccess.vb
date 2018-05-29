Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Imports System.text
Imports System.Data.SqlClient

Public Class KameitenSearchDataAccess
    ''' <summary>
    ''' コネクションストリング
    ''' </summary>
    ''' <remarks></remarks>
    Private connStr As String = ConnectionManager.Instance.ConnectionString

#Region "加盟店マスタ検索"
    ''' <summary>
    ''' 加盟店マスタの検索を行う
    ''' </summary>
    ''' <param name="strKubun">区分</param>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="strKameitenNm">加盟店名</param>
    ''' <param name="strKameitenKana">加盟店カナ</param>
    ''' <param name="blnDelete">取消対象フラグ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getKameitenKensakuData(ByVal strKubun As String, _
                                      ByVal strKameitenCd As String, _
                                      ByVal strKameitenNm As String, _
                                      ByVal strKameitenKana As String, _
                                      ByVal blnDelete As Boolean) As KameitenSearchDataSet.KameitenSearchTableDataTable

        ' パラメータ
        Const strParamKameitenCd As String = "@KAMEITENCD"
        Const strParamKameitenKana As String = "@KAMEITENKANA"
        Const strParamKubun1 As String = "@KUBUN1"
        Const strParamKubun2 As String = "@KUBUN2"
        Const strParamKubun3 As String = "@KUBUN3"

        Dim tmpKbn1 As String = ""
        Dim tmpKbn2 As String = ""
        Dim tmpKbn3 As String = ""
        Dim kbnCount As Integer = 1

        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append("SELECT k.kbn, k.kameiten_cd, k.torikesi, k.kameiten_mei1, k.tenmei_kana1, k.tenmei_kana2, t.todouhuken_mei")
        commandTextSb.Append("  FROM m_kameiten k ")
        commandTextSb.Append("  LEFT OUTER JOIN m_todoufuken t ON t.todouhuken_cd = k.todouhuken_cd ")
        commandTextSb.Append("  WHERE ")
        If blnDelete = True Then
            commandTextSb.Append(" k.torikesi = 0 ")
        Else
            commandTextSb.Append(" 0 = 0 ")
        End If
        If strKubun <> "" Then
            commandTextSb.Append(" AND k.kbn IN ( ")
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
        If strKameitenCd <> "" Then
            commandTextSb.Append(" AND k.kameiten_cd = " & strParamKameitenCd)
        End If
        If strKameitenKana <> "" Then
            commandTextSb.Append(" AND (k.tenmei_kana1 Like " & strParamKameitenKana)
            commandTextSb.Append(" OR k.tenmei_kana2 Like " & strParamKameitenKana & " )")
        End If
        If strKubun = "E" Then
            commandTextSb.Append(" ORDER BY k.kameiten_cd ")
        Else
            commandTextSb.Append(" ORDER BY k.tenmei_kana1 ")
        End If

        ' パラメータへ設定

        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamKameitenCd, SqlDbType.VarChar, 5, strKameitenCd), _
             SQLHelper.MakeParam(strParamKameitenKana, SqlDbType.VarChar, 22, strKameitenKana & Chr(37)), _
             SQLHelper.MakeParam(strParamKubun1, SqlDbType.Char, 1, tmpKbn1), _
             SQLHelper.MakeParam(strParamKubun2, SqlDbType.Char, 1, tmpKbn2), _
             SQLHelper.MakeParam(strParamKubun3, SqlDbType.Char, 1, tmpKbn3) _
             }

        ' データの取得

        Dim kameitenDataSet As New KameitenSearchDataSet()

        ' 検索実行

        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            kameitenDataSet, kameitenDataSet.KameitenSearchTable.TableName, commandParameters)

        Dim kameitenTable As KameitenSearchDataSet.KameitenSearchTableDataTable = _
                    kameitenDataSet.KameitenSearchTable

        Return kameitenTable

    End Function
#End Region

#Region "加盟店マスタ検索（依頼画面表示用）"
    ''' <summary>
    ''' 依頼画面表示用に加盟店マスタの検索を行う
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="strTyousakaisyaCd">調査会社コード</param>
    ''' <param name="blnDelete">(任意)取消対象フラグ false:取消も取得する(Default:True)</param>
    ''' <returns></returns>
    ''' <remarks>単一行の検索なのでDefaultで取消データを取得するようにしております</remarks>
    Public Function getKameitenKensakuData(ByVal strKubun As String, _
                                           ByVal strKameitenCd As String, _
                                           ByVal strTyousakaisyaCd As String, _
                                           Optional ByVal blnDelete As Boolean = True) _
                                           As KameitenSearchDataSet.KameitenDispSearchTableDataTable

        ' パラメータ
        Const strParamKubun As String = "@KUBUN"
        Const strParamKameitenCd As String = "@KAMEITENCD"
        Const strParamTyousakaisyaCd As String = "@TYOUSAKAISYACD"

        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append(" SELECT ")
        commandTextSb.Append("    k.kbn, ")
        commandTextSb.Append("    k.torikesi, ")
        commandTextSb.Append("    k.kameiten_cd, ")
        commandTextSb.Append("    k.kameiten_mei1, ")
        commandTextSb.Append("    k.tenmei_kana1, ")
        commandTextSb.Append("    j.tel_no, ")
        commandTextSb.Append("    j.fax_no, ")
        commandTextSb.Append("    j.mail_address, ")
        commandTextSb.Append("    k.builder_no, ")
        commandTextSb.Append("    b.builder_count, ")
        commandTextSb.Append("    r.keiretu_mei, ")
        commandTextSb.Append("    k.kameiten_mei2 As eigyousyo_mei, ")
        commandTextSb.Append("    k.tenmei_kana2, ")
        commandTextSb.Append("    k.koj_tantou_flg, ")
        commandTextSb.Append("    k.tys_mitsyo_flg, ")
        commandTextSb.Append("    m1.meisyou As tys_mitsyo_msg, ")
        commandTextSb.Append("    k.hattyuusyo_flg, ")
        commandTextSb.Append("    m2.meisyou As hattyuusyo_msg, ")
        If strTyousakaisyaCd <> "" Then
            commandTextSb.Append("    t.kahi_kbn, ")
        Else
            commandTextSb.Append("    5 As kahi_kbn, ")
        End If
        commandTextSb.Append("    k.keiretu_cd, ")
        commandTextSb.Append("    k.tys_seikyuu_saki ")
        commandTextSb.Append(" FROM ")
        commandTextSb.Append("    m_kameiten k ")
        commandTextSb.Append("    LEFT OUTER JOIN m_kameiten_jyuusyo j ON  j.kameiten_cd = k.kameiten_cd ")
        commandTextSb.Append("                                         AND j.jyuusyo_no = 1 ")
        commandTextSb.Append("    LEFT OUTER JOIN m_keiretu r ON r.kbn = k.kbn AND r.keiretu_cd = k.keiretu_cd ")
        If strTyousakaisyaCd <> "" Then
            commandTextSb.Append("    LEFT OUTER JOIN m_kameiten_tyousakaisya t ON  t.kameiten_cd = k.kameiten_cd AND kaisya_kbn = 1 ")
            commandTextSb.Append("                                              AND (RTRIM(t.tys_kaisya_cd) + RTRIM(t.jigyousyo_cd)) = " & strParamTyousakaisyaCd)
        End If
        commandTextSb.Append("    LEFT OUTER JOIN m_meisyou m1 ON  m1.meisyou_syubetu = '08'  ")
        commandTextSb.Append("                                  AND m1.code = k.tys_mitsyo_flg ")
        commandTextSb.Append("                                  AND k.tys_mitsyo_flg <> 0 ")
        commandTextSb.Append("    LEFT OUTER JOIN m_meisyou m2 ON  m2.meisyou_syubetu = '07'  ")
        commandTextSb.Append("                                  AND m2.code = k.hattyuusyo_flg ")
        commandTextSb.Append("                                  AND k.hattyuusyo_flg <> 0 ")
        commandTextSb.Append("    LEFT OUTER JOIN (SELECT ")
        commandTextSb.Append("                         kameiten_cd, ")
        commandTextSb.Append("                         COUNT(*) AS builder_count ")
        commandTextSb.Append("                     FROM ")
        commandTextSb.Append("                         m_kameiten_tyuuijikou ")
        commandTextSb.Append("                     GROUP BY ")
        commandTextSb.Append("                         kameiten_cd ")
        commandTextSb.Append("                    ) b ON b.kameiten_cd = k.kameiten_cd  ")
        commandTextSb.Append(" WHERE  ")
        commandTextSb.Append("    k.kbn = " & strParamKubun)
        commandTextSb.Append("    AND k.kameiten_cd = " & strParamKameitenCd)

        ' 取消しを除外する

        If blnDelete = True Then
            commandTextSb.Append(" AND k.torikesi = 0 ")
        End If

        ' パラメータへ設定

        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamKubun, SqlDbType.Char, 1, strKubun), _
             SQLHelper.MakeParam(strParamKameitenCd, SqlDbType.VarChar, 5, strKameitenCd)}


        If strTyousakaisyaCd <> "" Then
            commandParameters = New SqlParameter() {SQLHelper.MakeParam(strParamKubun, SqlDbType.Char, 1, strKubun), _
                                                    SQLHelper.MakeParam(strParamKameitenCd, SqlDbType.VarChar, 5, strKameitenCd), _
                                                    SQLHelper.MakeParam(strParamTyousakaisyaCd, SqlDbType.VarChar, 7, strTyousakaisyaCd)}
        End If

        ' データの取得

        Dim kameitenDataSet As New KameitenSearchDataSet()

        ' 検索実行

        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            kameitenDataSet, kameitenDataSet.KameitenDispSearchTable.TableName, commandParameters)

        Dim kameitendispTable As KameitenSearchDataSet.KameitenDispSearchTableDataTable = _
                    kameitenDataSet.KameitenDispSearchTable

        Return kameitendispTable

    End Function
#End Region

#Region "請求締め日取得"
    ''' <summary>
    ''' 加盟店マスタより請求締め日を取得する
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="strKbn">区分</param>
    ''' <returns>請求締め日</returns>
    ''' <remarks></remarks>
    Public Function getSeikyuuSimeDate(ByVal strKameitenCd As String, _
                                       ByVal strKbn As String) As String

        ' パラメータ
        Const strParamKameitenCd As String = "@KAMEITENCD"
        Const strParamKbn As String = "@TYOUSAKAISYACD"

        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append(" SELECT ")
        commandTextSb.Append("    tys_seikyuu_sime_date ")
        commandTextSb.Append(" FROM ")
        commandTextSb.Append("    m_kameiten k ")
        commandTextSb.Append(" WHERE  ")
        commandTextSb.Append("    k.kameiten_cd = " & strParamKameitenCd)
        commandTextSb.Append(" AND  ")
        commandTextSb.Append("    k.kbn = " & strParamKbn)

        ' パラメータへ設定

        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamKameitenCd, SqlDbType.VarChar, 5, strKameitenCd), _
             SQLHelper.MakeParam(strParamKbn, SqlDbType.Char, 1, strKbn)}

        ' データの取得

        Dim data As Object

        ' 検索実行

        data = SQLHelper.ExecuteScalar(connStr, _
                                       CommandType.Text, _
                                       commandTextSb.ToString(), _
                                       commandParameters)

        ' 取得出来ない場合、空白を返却
        If data Is Nothing Then
            Return ""
        End If

        Return data

    End Function
#End Region


End Class

Imports System.text
Imports System.Data.SqlClient

Public Class KameitenSearchDataAccess

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ''' <summary>
    ''' コネクションストリング
    ''' </summary>
    ''' <remarks></remarks>
    Private connStr As String = ConnectionManager.Instance.ConnectionString

    Dim cmnDtAcc As New CmnDataAccess

#Region "加盟店マスタ検索"
    ''' <summary>
    ''' 加盟店マスタの検索を行う
    ''' </summary>
    ''' <param name="strKubun">区分</param>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="strKameitenNm">加盟店名</param>
    ''' <param name="strKameitenKana">加盟店カナ</param>
    ''' <param name="strKameitenTourokuJuusyo">加盟店登録住所</param>
    ''' <param name="strKameitenTelNo">加盟店電話番号</param>
    ''' <param name="blnDelete">取消対象フラグ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetKameitenKensakuData(ByVal strKubun As String, _
                                           ByVal strKameitenCd As String, _
                                           ByVal strKameitenNm As String, _
                                           ByVal strKameitenKana As String, _
                                           ByVal strKameitenTourokuJuusyo As String, _
                                           ByVal strKameitenTelNo As String, _
                                           ByVal blnDelete As Boolean) As DataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKameitenKensakuData", _
                                            strKubun, _
                                            strKameitenCd, _
                                            strKameitenNm, _
                                            strKameitenKana, _
                                            blnDelete)

        '加盟店住所マスタ結合要非チェック
        Dim flgKameitenJuusyoMaster As Boolean = False
        If strKameitenTourokuJuusyo <> String.Empty OrElse strKameitenTelNo <> String.Empty Then
            flgKameitenJuusyoMaster = True
        End If

        'パラメータ
        Const strParamKubun1 As String = "@KUBUN1"
        Const strParamKubun2 As String = "@KUBUN2"
        Const strParamKubun3 As String = "@KUBUN3"
        Const strParamKameitenCd As String = "@KAMEITENCD"
        Const strParamKameitenKana As String = "@KAMEITENKANA"
        Const strParamKameitenTourokuJuusyo As String = "@KAMEITENTOUROKUJUUSYO"
        Const strParamKameitenTelNo As String = "@KAMEITENTELNO"

        '区分条件テンポラリ
        Dim tmpKbn1 As String = ""
        Dim tmpKbn2 As String = ""
        Dim tmpKbn3 As String = ""
        Dim kbnCount As Integer = 1

        'SQL文生成
        Dim cmdTextSb As New StringBuilder()

        cmdTextSb.AppendLine("SELECT ")
        cmdTextSb.AppendLine("     k.kbn ")
        cmdTextSb.AppendLine("   , k.kameiten_cd ")
        cmdTextSb.AppendLine("   , k.torikesi ")
        cmdTextSb.AppendLine("   , km.meisyou AS kt_torikesi_riyuu ")
        cmdTextSb.AppendLine("   , k.kameiten_mei1 ")
        cmdTextSb.AppendLine("   , k.tenmei_kana1 ")
        cmdTextSb.AppendLine("   , k.tenmei_kana2 ")
        cmdTextSb.AppendLine("   , k.hattyuu_teisi_flg ")
        cmdTextSb.AppendLine("   , t.todouhuken_mei ")
        cmdTextSb.AppendLine("   , k.keiretu_cd ")
        cmdTextSb.AppendLine("   , k.ssgr_kkk ")
        cmdTextSb.AppendLine("   , k.kaiseki_hosyou_kkk ")
        cmdTextSb.AppendLine("   , k.sds_jidoou_set_info ")
        cmdTextSb.AppendLine("FROM ")
        cmdTextSb.AppendLine("     m_kameiten k WITH (READCOMMITTED) ")
        cmdTextSb.AppendLine("          LEFT OUTER JOIN m_todoufuken t WITH (READCOMMITTED) ")
        cmdTextSb.AppendLine("            ON k.todouhuken_cd = t.todouhuken_cd ")
        If flgKameitenJuusyoMaster Then
            cmdTextSb.AppendLine("   LEFT OUTER JOIN m_kameiten_jyuusyo kj WITH (READCOMMITTED)  ")
            cmdTextSb.AppendLine("     ON k.kameiten_cd = kj.kameiten_cd  ")
            cmdTextSb.AppendLine("     AND 1 = kj.jyuusyo_no  ")
        End If
        cmdTextSb.AppendLine("          LEFT OUTER JOIN m_kakutyou_meisyou km ")
        cmdTextSb.AppendLine("            ON km.meisyou_syubetu = 56 ")
        cmdTextSb.AppendLine("           AND CAST(k.torikesi AS VARCHAR(10)) = km.code ")
        cmdTextSb.AppendLine("WHERE ")
        cmdTextSb.AppendLine("     0 = 0 ")
        If blnDelete = True Then
            cmdTextSb.AppendLine(" AND k.torikesi = 0 ")
        End If
        If strKubun <> "" Then
            cmdTextSb.Append(" AND k.kbn IN ( ")
            Dim arrKbn As Array = strKubun.Split(",")
            For Each tmpKbn As String In arrKbn
                If kbnCount = 1 Then
                    tmpKbn1 = arrKbn(0)
                    cmdTextSb.Append(strParamKubun1)
                End If
                If kbnCount = 2 Then
                    tmpKbn2 = arrKbn(1)
                    cmdTextSb.Append("," & strParamKubun2)
                End If
                If kbnCount = 3 Then
                    tmpKbn3 = arrKbn(2)
                    cmdTextSb.Append("," & strParamKubun3)
                End If
                kbnCount += 1
            Next
            cmdTextSb.AppendLine(" ) ")
        End If
        If strKameitenCd <> "" Then
            cmdTextSb.AppendLine(" AND k.kameiten_cd LIKE " & strParamKameitenCd)
        End If
        If strKameitenKana <> "" Then
            cmdTextSb.AppendLine(" AND (k.tenmei_kana1 LIKE " & strParamKameitenKana)
            cmdTextSb.AppendLine(" OR k.tenmei_kana2 LIKE " & strParamKameitenKana & " )")
        End If
        If strKameitenTourokuJuusyo <> String.Empty Then
            cmdTextSb.AppendLine("   AND kj.jyuusyo1 + kj.jyuusyo2 LIKE " & strParamKameitenTourokuJuusyo)
        End If
        If strKameitenTelNo <> String.Empty Then
            cmdTextSb.AppendLine("   AND REPLACE(kj.tel_no,'-','') LIKE REPLACE(" & strParamKameitenTelNo & ",'-','')")
        End If
        If strKubun = "E" Then
            cmdTextSb.AppendLine(" ORDER BY k.kameiten_cd ")
        Else
            cmdTextSb.AppendLine(" ORDER BY k.tenmei_kana1 ")
        End If


        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = { _
                        SQLHelper.MakeParam(strParamKubun1, SqlDbType.Char, 1, tmpKbn1), _
                        SQLHelper.MakeParam(strParamKubun2, SqlDbType.Char, 1, tmpKbn2), _
                        SQLHelper.MakeParam(strParamKubun3, SqlDbType.Char, 1, tmpKbn3), _
                        SQLHelper.MakeParam(strParamKameitenCd, SqlDbType.VarChar, 5, strKameitenCd & Chr(37)), _
                        SQLHelper.MakeParam(strParamKameitenKana, SqlDbType.VarChar, 22, strKameitenKana & Chr(37)), _
                        SQLHelper.MakeParam(strParamKameitenTourokuJuusyo, SqlDbType.VarChar, 71, strKameitenTourokuJuusyo & Chr(37)), _
                        SQLHelper.MakeParam(strParamKameitenTelNo, SqlDbType.VarChar, 17, strKameitenTelNo & Chr(37)) _
                        }

        Return cmnDtAcc.getDataTable(cmdTextSb.ToString(), commandParameters)

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
    Public Function GetKameitenKensakuData(ByVal strKubun As String, _
                                           ByVal strKameitenCd As String, _
                                           ByVal strTyousakaisyaCd As String, _
                                           Optional ByVal blnDelete As Boolean = True) _
                                           As KameitenSearchDataSet.KameitenDispSearchTableDataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKameitenKensakuData", _
                                    strKubun, _
                                    strKameitenCd, _
                                    strTyousakaisyaCd, _
                                    blnDelete)

        ' パラメータ
        Const strParamKubun As String = "@KUBUN"
        Const strParamKameitenCd As String = "@KAMEITENCD"
        Const strParamTyousakaisyaCd As String = "@TYOUSAKAISYACD"

        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append(" SELECT ")
        commandTextSb.Append("    k.kbn, ")
        commandTextSb.Append("    k.torikesi, ")
        commandTextSb.Append("    km.meisyou AS kt_torikesi_riyuu, ")
        commandTextSb.Append("    k.kameiten_cd, ")
        commandTextSb.Append("    k.kameiten_mei1, ")
        commandTextSb.Append("    k.tenmei_kana1, ")
        commandTextSb.Append("    k.eigyousyo_cd, ")
        commandTextSb.Append("    j.tel_no, ")
        commandTextSb.Append("    j.fax_no, ")
        commandTextSb.Append("    j.mail_address, ")
        commandTextSb.Append("    k.builder_no, ")
        commandTextSb.Append("    b.builder_count, ")
        commandTextSb.Append("    r.keiretu_mei, ")
        commandTextSb.Append("    ei.eigyousyo_mei As eigyousyo_mei, ")
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
        commandTextSb.Append("    k.tys_seikyuu_saki, ")
        commandTextSb.Append("    k.hosyousyo_hak_umu, ")
        commandTextSb.Append("    k.hosyou_kikan, ")
        commandTextSb.Append("    k.koj_gaisya_seikyuu_umu, ")
        commandTextSb.Append("    k.koj_seikyuusaki, ")
        commandTextSb.Append("    k.hansokuhin_seikyuusaki, ")
        commandTextSb.Append("    ISNULL(j.jyuusyo1,'') + ISNULL(j.jyuusyo2,'') As jyuusyo, ")
        commandTextSb.Append("    k.jiosaki_flg, ")
        commandTextSb.Append("    k.tys_seikyuu_saki_cd, ")
        commandTextSb.Append("    k.tys_seikyuu_saki_brc, ")
        commandTextSb.Append("    k.tys_seikyuu_saki_kbn, ")
        commandTextSb.Append("    k.tys_seikyuu_sime_date, ")
        commandTextSb.Append("    k.koj_seikyuu_saki_cd, ")
        commandTextSb.Append("    k.koj_seikyuu_saki_brc, ")
        commandTextSb.Append("    k.koj_seikyuu_saki_kbn, ")
        commandTextSb.Append("    k.koj_seikyuu_sime_date, ")
        commandTextSb.Append("    k.hansokuhin_seikyuu_saki_cd, ")
        commandTextSb.Append("    k.hansokuhin_seikyuu_saki_brc, ")
        commandTextSb.Append("    k.hansokuhin_seikyuu_saki_kbn, ")
        commandTextSb.Append("    k.hansokuhin_seikyuu_sime_date, ")
        commandTextSb.Append("    k.kameiten_seisiki_mei, ")
        commandTextSb.Append("    k.kameiten_seisiki_mei_kana, ")
        commandTextSb.Append("    ei.fc_tys_kaisya_cd, ")
        commandTextSb.Append("    ei.fc_jigyousyo_cd, ")
        commandTextSb.Append("    tk.tys_kaisya_mei AS fc_ten_mei ")

        commandTextSb.Append(" FROM ")
        commandTextSb.Append("    m_kameiten k WITH (READCOMMITTED) ")
        commandTextSb.Append("    LEFT OUTER JOIN m_kameiten_jyuusyo j WITH (READCOMMITTED) ON  j.kameiten_cd = k.kameiten_cd ")
        commandTextSb.Append("                                         AND j.jyuusyo_no = 1 ")
        commandTextSb.Append("    LEFT OUTER JOIN m_keiretu r WITH (READCOMMITTED) ON r.kbn = k.kbn AND r.keiretu_cd = k.keiretu_cd ")
        If strTyousakaisyaCd <> "" Then
            commandTextSb.Append("    LEFT OUTER JOIN m_kameiten_tyousakaisya t WITH (READCOMMITTED) ON  t.kameiten_cd = k.kameiten_cd AND kaisya_kbn = 1 ")
            commandTextSb.Append("                                              AND (RTRIM(t.tys_kaisya_cd) + RTRIM(t.jigyousyo_cd)) = " & strParamTyousakaisyaCd)
        End If
        commandTextSb.Append("    LEFT OUTER JOIN m_meisyou m1 WITH (READCOMMITTED) ON  m1.meisyou_syubetu = '08'  ")
        commandTextSb.Append("                                  AND m1.code = k.tys_mitsyo_flg ")
        commandTextSb.Append("                                  AND k.tys_mitsyo_flg <> 0 ")
        commandTextSb.Append("    LEFT OUTER JOIN m_meisyou m2 WITH (READCOMMITTED) ON  m2.meisyou_syubetu = '07'  ")
        commandTextSb.Append("                                  AND m2.code = k.hattyuusyo_flg ")
        commandTextSb.Append("                                  AND k.hattyuusyo_flg <> 0 ")
        commandTextSb.Append("    LEFT OUTER JOIN (SELECT ")
        commandTextSb.Append("                         kameiten_cd, ")
        commandTextSb.Append("                         COUNT(*) AS builder_count ")
        commandTextSb.Append("                     FROM ")
        commandTextSb.Append("                         m_kameiten_tyuuijikou WITH (READCOMMITTED) ")
        commandTextSb.Append("                     GROUP BY ")
        commandTextSb.Append("                         kameiten_cd ")
        commandTextSb.Append("                    ) b ON b.kameiten_cd = k.kameiten_cd  ")
        commandTextSb.Append("    LEFT OUTER JOIN m_eigyousyo ei WITH (READCOMMITTED) ON  k.eigyousyo_cd = ei.eigyousyo_cd  ")
        commandTextSb.Append("    LEFT OUTER JOIN m_kakutyou_meisyou km ")
        commandTextSb.Append("      ON km.meisyou_syubetu = 56 ")
        commandTextSb.Append("      AND CAST(k.torikesi AS VARCHAR(10)) = km.code ")
        commandTextSb.Append("    LEFT OUTER JOIN m_tyousakaisya tk WITH (READCOMMITTED) ")
        commandTextSb.Append("     ON ei.fc_tys_kaisya_cd = tk.tys_kaisya_cd ")
        commandTextSb.Append("     AND ei.fc_jigyousyo_cd = tk.jigyousyo_cd ")

        commandTextSb.Append(" WHERE  ")
        commandTextSb.Append("     k.kameiten_cd = " & strParamKameitenCd)
        If strKubun <> "" Then '区分指定あり
            commandTextSb.Append(" AND k.kbn = " & strParamKubun)
        End If

        ' 取消しを除外する
        If blnDelete = True Then
            commandTextSb.Append(" AND k.torikesi = 0 ")
        End If

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
                            {SQLHelper.MakeParam(strParamKubun, SqlDbType.Char, 1, strKubun), _
                            SQLHelper.MakeParam(strParamKameitenCd, SqlDbType.VarChar, 5, strKameitenCd), _
                            SQLHelper.MakeParam(strParamTyousakaisyaCd, SqlDbType.VarChar, 7, strTyousakaisyaCd)}

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

#Region "調査請求締め日取得"
    ''' <summary>
    ''' 加盟店マスタより調査請求締め日を取得する
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="strKbn">区分</param>
    ''' <param name="blnTorikesi">取消フラグ使用有無（True：使用有り　False：使用無し）</param>
    ''' <returns>請求締め日</returns>
    ''' <remarks></remarks>
    Public Function GetTyousaSeikyuuSimeDate(ByVal strKameitenCd As String, _
                                       ByVal strKbn As String, _
                                       Optional ByVal blnTorikesi As Boolean = False) As String

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSeikyuuSimeDate", _
                            strKameitenCd, _
                            strKbn, _
                            blnTorikesi)
        '未入力チェック
        If strKameitenCd = "" Or strKbn = "" Then
            Return ""
        End If

        ' パラメータ
        Const strParamKameitenCd As String = "@KAMEITENCD"
        Const strParamKbn As String = "@TYOUSAKAISYACD"

        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append(" SELECT ")
        commandTextSb.Append("    tys_seikyuu_sime_date ")
        commandTextSb.Append(" FROM ")
        commandTextSb.Append("    m_kameiten k WITH (READCOMMITTED) ")
        commandTextSb.Append(" WHERE  ")
        commandTextSb.Append("    k.kameiten_cd = " & strParamKameitenCd)
        commandTextSb.Append(" AND  ")
        commandTextSb.Append("    k.kbn = " & strParamKbn)
        If blnTorikesi Then
            commandTextSb.Append(" AND  ")
            commandTextSb.Append("    k.torikesi = 0")
        End If

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamKameitenCd, SqlDbType.VarChar, 5, strKameitenCd), _
             SQLHelper.MakeParam(strParamKbn, SqlDbType.Char, 1, strKbn)}

        ' データの取得
        Dim data As Object = Nothing

        ' 検索実行
        data = SQLHelper.ExecuteScalar(connStr, _
                                       CommandType.Text, _
                                       commandTextSb.ToString(), _
                                       commandParameters)

        ' 取得出来ない場合、空白を返却
        If data Is Nothing OrElse IsDBNull(data) Then
            Return ""
        End If

        Return data

    End Function
#End Region

#Region "工事請求締め日取得"
    ''' <summary>
    ''' 加盟店マスタより工事請求締め日を取得する
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="strKbn">区分</param>
    ''' <returns>工事請求締め日</returns>
    ''' <remarks></remarks>
    Public Function GetKoujiSeikyuuSimeDate(ByVal strKameitenCd As String, _
                                       ByVal strKbn As String) As String

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKoujiSeikyuuSimeDate", _
                            strKameitenCd, _
                            strKbn)

        '未入力チェック
        If strKameitenCd = "" Or strKbn = "" Then
            Return ""
        End If

        ' パラメータ
        Const strParamKameitenCd As String = "@KAMEITENCD"
        Const strParamKbn As String = "@TYOUSAKAISYACD"

        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append(" SELECT ")
        commandTextSb.Append("    k.koj_seikyuu_sime_date ")
        commandTextSb.Append(" FROM ")
        commandTextSb.Append("    m_kameiten k WITH (READCOMMITTED) ")
        commandTextSb.Append(" WHERE  ")
        commandTextSb.Append("    k.kameiten_cd = " & strParamKameitenCd)
        commandTextSb.Append(" AND  ")
        commandTextSb.Append("    k.kbn = " & strParamKbn)
        commandTextSb.Append(" AND  ")
        commandTextSb.Append("    k.torikesi = 0")

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamKameitenCd, SqlDbType.VarChar, 5, strKameitenCd), _
             SQLHelper.MakeParam(strParamKbn, SqlDbType.Char, 1, strKbn)}

        ' データの取得
        Dim data As Object = Nothing

        ' 検索実行
        data = SQLHelper.ExecuteScalar(connStr, _
                                       CommandType.Text, _
                                       commandTextSb.ToString(), _
                                       commandParameters)

        ' 取得出来ない場合、空白を返却
        If data Is Nothing OrElse IsDBNull(data) Then
            Return ""
        End If

        Return data

    End Function
#End Region

#Region "販促品請求締め日取得"
    ''' <summary>
    ''' 加盟店マスタより販促品請求締め日を取得する
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="strKbn">区分</param>
    ''' <returns>販促品請求締め日</returns>
    ''' <remarks></remarks>
    Public Function GetHansokuhinSeikyuuSimeDate(ByVal strKameitenCd As String, _
                                       ByVal strKbn As String) As String

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetHansokuhinSeikyuuSimeDate", _
                            strKameitenCd, _
                            strKbn)
        '未入力チェック
        If strKameitenCd = "" Or strKbn = "" Then
            Return ""
        End If

        ' パラメータ
        Const strParamKameitenCd As String = "@KAMEITENCD"
        Const strParamKbn As String = "@TYOUSAKAISYACD"

        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append(" SELECT ")
        commandTextSb.Append("    k.hansokuhin_seikyuu_sime_date ")
        commandTextSb.Append(" FROM ")
        commandTextSb.Append("    m_kameiten k WITH (READCOMMITTED) ")
        commandTextSb.Append(" WHERE  ")
        commandTextSb.Append("    k.kameiten_cd = " & strParamKameitenCd)
        commandTextSb.Append(" AND  ")
        commandTextSb.Append("    k.kbn = " & strParamKbn)
        commandTextSb.Append(" AND  ")
        commandTextSb.Append("    k.torikesi = 0")

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamKameitenCd, SqlDbType.VarChar, 5, strKameitenCd), _
             SQLHelper.MakeParam(strParamKbn, SqlDbType.Char, 1, strKbn)}

        ' データの取得
        Dim data As Object = Nothing

        ' 検索実行
        data = SQLHelper.ExecuteScalar(connStr, _
                                       CommandType.Text, _
                                       commandTextSb.ToString(), _
                                       commandParameters)

        ' 取得出来ない場合、空白を返却
        If data Is Nothing OrElse IsDBNull(data) Then
            Return ""
        End If

        Return data

    End Function
#End Region

#Region "工事会社NG情報取得"
    ''' <summary>
    ''' 工事会社NG情報取得を行う。
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="strKoujikaisyaCd">工事会社コード</param>
    ''' <param name="blnDelete">(任意)取消対象フラグ false:取消も取得する(Default:True)</param>
    ''' <returns></returns>
    ''' <remarks>単一行の検索なのでDefaultで取消データを取得するようにしております</remarks>
    Public Function GetKoujiKaisyaNGData(ByVal strKubun As String, _
                                           ByVal strKameitenCd As String, _
                                           ByVal strKoujikaisyaCd As String, _
                                           Optional ByVal blnDelete As Boolean = True _
                                           ) _
                                           As KameitenSearchDataSet.KameitenKoujiKaisyaNGTableDataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKoujiKaisyaNGData", _
                                    strKubun, _
                                    strKameitenCd, _
                                    strKoujikaisyaCd, _
                                    blnDelete)

        ' パラメータ
        Const strParamKubun As String = "@KUBUN"
        Const strParamKameitenCd As String = "@KAMEITENCD"
        Const strParamKoujikaisyaCd As String = "@KOUJIKAISYACD"

        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append(" SELECT ")
        commandTextSb.Append("    k.kbn, ")
        commandTextSb.Append("    k.torikesi, ")
        commandTextSb.Append("    k.kameiten_cd, ")
        commandTextSb.Append("    ISNULL(t.kahi_kbn,5) AS kahi_kbn ")
        commandTextSb.Append(" FROM ")
        commandTextSb.Append("    m_kameiten k WITH (READCOMMITTED) ")
        commandTextSb.Append("    LEFT OUTER JOIN m_kameiten_tyousakaisya t WITH (READCOMMITTED) ON  t.kameiten_cd = k.kameiten_cd AND kaisya_kbn = 2 ")
        commandTextSb.Append("                                              AND (RTRIM(t.tys_kaisya_cd) + RTRIM(t.jigyousyo_cd)) = " & strParamKoujikaisyaCd)
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
                                                SQLHelper.MakeParam(strParamKameitenCd, SqlDbType.VarChar, 5, strKameitenCd), _
                                                SQLHelper.MakeParam(strParamKoujikaisyaCd, SqlDbType.VarChar, 7, strKoujikaisyaCd)}

        ' データの取得
        Dim kameitenDataSet As New KameitenSearchDataSet()

        ' 検索実行
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            kameitenDataSet, kameitenDataSet.KameitenKoujiKaisyaNGTable.TableName, commandParameters)

        Dim kameitendispTable As KameitenSearchDataSet.KameitenKoujiKaisyaNGTableDataTable = _
                    kameitenDataSet.KameitenKoujiKaisyaNGTable

        Return kameitendispTable

    End Function
#End Region

#Region "解約払戻し価格取得"
    ''' <summary>
    ''' 加盟店マスタより解約払戻し価格取得を取得する
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="strKbn">区分</param>
    ''' <remarks></remarks>
    Public Function GetKaiyakuKakaku(ByVal strKameitenCd As String, _
                                     ByVal strKbn As String) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKaiyakuKakaku", _
                            strKameitenCd, _
                            strKbn)

        '未入力チェック
        If strKameitenCd = "" Or strKbn = "" Then
            Return 0
        End If

        ' パラメータ
        Const strParamKameitenCd As String = "@KAMEITENCD"
        Const strParamKbn As String = "@KBN"

        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append(" SELECT ")
        commandTextSb.Append("    ISNULL (kaiyaku_haraimodosi_kkk,0) As kaiyaku_haraimodosi_kkk ")
        commandTextSb.Append(" FROM ")
        commandTextSb.Append("    m_kameiten k WITH (READCOMMITTED) ")
        commandTextSb.Append(" WHERE  ")
        commandTextSb.Append("    k.kameiten_cd = " & strParamKameitenCd)
        commandTextSb.Append(" AND  ")
        commandTextSb.Append("    k.kbn = " & strParamKbn)

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamKameitenCd, SqlDbType.VarChar, 5, strKameitenCd), _
             SQLHelper.MakeParam(strParamKbn, SqlDbType.Char, 1, strKbn)}

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

#Region "加盟店マスタ.入金確認条件NO取得"
    ''' <summary>
    ''' 加盟店マスタより入金確認条件NOを取得する
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="strKbn">区分</param>
    ''' <remarks></remarks>
    Public Function GetNyuukinKakuninJoukenNo(ByVal strKameitenCd As String, _
                                     ByVal strKbn As String) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetNyuukinKakuninJoukenNo", _
                            strKameitenCd, _
                            strKbn)

        '未入力チェック
        If strKameitenCd = "" Or strKbn = "" Then
            Return -1
        End If

        ' パラメータ
        Const strParamKameitenCd As String = "@KAMEITENCD"
        Const strParamKbn As String = "@KBN"

        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append(" SELECT ")
        commandTextSb.Append("    ISNULL (nyuukin_kakunin_jyouken,0) As nyuukin_kakunin_jyouken ")
        commandTextSb.Append(" FROM ")
        commandTextSb.Append("    m_kameiten k WITH (READCOMMITTED) ")
        commandTextSb.Append(" WHERE  ")
        commandTextSb.Append("    k.kameiten_cd = " & strParamKameitenCd)
        commandTextSb.Append(" AND  ")
        commandTextSb.Append("    k.kbn = " & strParamKbn)

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamKameitenCd, SqlDbType.VarChar, 5, strKameitenCd), _
             SQLHelper.MakeParam(strParamKbn, SqlDbType.Char, 1, strKbn)}

        ' データの取得
        Dim data As Object = Nothing

        ' 検索実行
        data = SQLHelper.ExecuteScalar(connStr, _
                                       CommandType.Text, _
                                       commandTextSb.ToString(), _
                                       commandParameters)

        ' 取得出来ない場合、空白を返却
        If data Is Nothing OrElse IsDBNull(data) Then
            Return -1
        End If

        Return data

    End Function
#End Region

#Region "付保証明書情報取得"
    ''' <summary>
    ''' 付保証明書情報取得を行う。
    ''' </summary>
    ''' <param name="strKubun">区分</param>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="blnDelete">(任意)取消対象フラグ false:取消も取得する(Default:True)</param>
    ''' <returns></returns>
    ''' <remarks>単一行の検索なのでDefaultで取消データを取得するようにしております</remarks>
    Public Function GetFuhoSyoumeisyoData(ByVal strKubun As String, _
                                           ByVal strKameitenCd As String, _
                                           Optional ByVal blnDelete As Boolean = True _
                                           ) _
                                           As KameitenSearchDataSet.KameitenFuhoTableDataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetFuhoSyoumeisyoData", _
                                    strKubun, _
                                    strKameitenCd, _
                                    blnDelete)

        ' パラメータ
        Const strParamKubun As String = "@KUBUN"
        Const strParamKameitenCd As String = "@KAMEITENCD"

        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append(" SELECT ")
        commandTextSb.Append("    k.kbn, ")
        commandTextSb.Append("    k.torikesi, ")
        commandTextSb.Append("    k.kameiten_cd, ")
        commandTextSb.Append("    k.fuho_syoumeisyo_flg, ")
        commandTextSb.Append("    k.fuho_syoumeisyo_kaisi_nengetu ")
        commandTextSb.Append(" FROM ")
        commandTextSb.Append("    m_kameiten k WITH (READCOMMITTED) ")
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

        ' データの取得
        Dim kameitenDataSet As New KameitenSearchDataSet()

        ' 検索実行
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            kameitenDataSet, kameitenDataSet.KameitenFuhoTable.TableName, commandParameters)

        Dim kameitendispTable As KameitenSearchDataSet.KameitenFuhoTableDataTable = _
                    kameitenDataSet.KameitenFuhoTable

        Return kameitendispTable

    End Function
#End Region

#Region "営業所・系列コード取得処理"
    ''' <summary>
    ''' 営業所コード・系列コードを取得する
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEigyousyoKeiretuCd(ByVal strKameitenCd As String) As DataTable
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetEigyousyoKeiretuCd", strKameitenCd)

        'SQL文の生成
        Dim cmdTextSb As New StringBuilder
        ' パラメータ
        Dim cmdParams() As SqlClient.SqlParameter
        Const strParamKameitenCd As String = "@KAMEITENCD"

        cmdTextSb.AppendLine(" SELECT ")
        cmdTextSb.AppendLine("      k.kameiten_cd ")
        cmdTextSb.AppendLine("    , k.eigyousyo_cd ")
        cmdTextSb.AppendLine("    , k.keiretu_cd ")
        cmdTextSb.AppendLine(" FROM ")
        cmdTextSb.AppendLine("      m_kameiten k ")
        cmdTextSb.AppendLine(" WHERE ")
        cmdTextSb.AppendLine("      k.kameiten_cd = " & strParamKameitenCd)

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
                SQLHelper.MakeParam(strParamKameitenCd, SqlDbType.VarChar, 5, strKameitenCd) _
        }

        'データ取得＆返却
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)
    End Function
#End Region

End Class

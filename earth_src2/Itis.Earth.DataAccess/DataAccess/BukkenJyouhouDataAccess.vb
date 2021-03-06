Imports System.Data.SqlClient
Imports System.Text
Imports Itis.ApplicationBlocks.Data.SQLHelper
''' <summary>¨îñõ·é</summary>
''' <remarks>¨îñõ@\ðñ·é</remarks>
''' <history>
''' <para>2009/10/27@(åAîñVXe)@VKì¬</para>
''' </history>
Public Class BukkenJyouhouDataAccess

    Private connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary> ¨îñæ¾</summary>
    ''' <param name="dtParam">Paramf[^Z[g</param>
    ''' <returns>¨îñÌf[^</returns>
    Public Function SelBukkenJyouhouInfo(ByVal dtParam As DataTable) As BukkenJyouhouDataSet.BukkenJyouhouTableDataTable

        ' DataSetCX^XÌ¶¬()
        Dim dsBukkenJyouhouDataSet As New BukkenJyouhouDataSet

        'SQL¶Ì¶¬
        Dim commandTextSb As New StringBuilder

        'p[^i[
        Dim paramList As New List(Of SqlClient.SqlParameter)
        Dim strFrom As String = ""
        Dim strTo As String = ""
        Dim arrKbn() As String
        'SQL¶
        With commandTextSb
            .AppendLine(" SELECT ")

            .AppendLine(" TOP " & dtParam.Rows(0).Item("kennsuu").ToString)

            .AppendLine(" t_jiban.kbn + t_jiban.hosyousyo_no as hosyousyo_no, ")
            .AppendLine(" m_data_haki.haki_syubetu, ")
            .AppendLine(" t_jiban.irai_date, ")
            .AppendLine(" t_jiban.tys_kibou_date, ")
            .AppendLine(" m_tyousahouhou.tys_houhou_mei_ryaku, ")
            .AppendLine(" t_jiban.sesyu_mei, ")
            .AppendLine(" t_jiban.kameiten_cd, ")
            .AppendLine(" m_kameiten.kameiten_mei1, ")
            .AppendLine(" t_jiban.irai_tantousya_mei, ")
            .AppendLine(" t_jiban.hantei_cd2, ")
            .AppendLine(" m_ks_siyou.ks_siyou, ")
            .AppendLine(" m_kairy_koj_syubetu.kairy_koj_syubetu, ")
            .AppendLine(" t_jiban.hosyousyo_hak_date, ")
            .AppendLine(" t_jiban.syoudakusyo_tys_date, ")
            .AppendLine(" t_jiban.tys_jissi_date, ")
            .AppendLine(" tyousa.uri_date, ")
            .AppendLine(" t_jiban.kairy_koj_kanry_yotei_date, ")
            .AppendLine(" t_jiban.kairy_koj_date, ")
            .AppendLine(" kouji.uri_date as uri_date2,")
            .AppendLine(" ReportIF.status, ")

            '20100318 nR Î
            .AppendLine(" t_jiban.keiyu ")
            '==================2012/03/29 Ô´ 405721ÄÌÎ ÇÁ«=================================
            .AppendLine(" ,m_kameiten.torikesi")
            .AppendLine(" ,CASE ")
            .AppendLine("    WHEN m_kameiten.torikesi = 0 THEN ")
            .AppendLine("        '' ")
            .AppendLine("    ELSE ")
            .AppendLine("        CONVERT(VARCHAR(10),m_kameiten.torikesi) + ':' + ISNULL(MKM.meisyou,'') ")
            .AppendLine("    END AS torikesi_txt ")
            '==================2012/03/29 Ô´ 405721ÄÌÎ ÇÁª=================================

            .AppendLine(" FROM t_jiban WITH (READCOMMITTED) ")
            .AppendLine(" LEFT JOIN m_kameiten  WITH (READCOMMITTED) ")
            .AppendLine(" ON T_jiban.kameiten_cd=m_kameiten.kameiten_cd ")
            .AppendLine(" LEFT JOIN m_todoufuken  WITH (READCOMMITTED) ")
            .AppendLine(" ON m_todoufuken.todouhuken_cd=m_kameiten.todouhuken_cd ")

            .AppendLine(" LEFT JOIN m_data_haki  WITH (READCOMMITTED) ")
            .AppendLine(" ON t_jiban.data_haki_syubetu=m_data_haki.data_haki_no ")
            .AppendLine(" LEFT JOIN m_tyousahouhou  WITH (READCOMMITTED) ")
            .AppendLine(" ON t_jiban.tys_houhou=m_tyousahouhou.tys_houhou_no ")
            '.AppendLine(" LEFT JOIN ReportIF  WITH (READCOMMITTED) ")
            '.AppendLine(" ON t_jiban.kbn=SUBSTRING(ReportIF.kokyaku_no,1,1)  ")
            '.AppendLine("  AND t_jiban.hosyousyo_no=SUBSTRING(ReportIF.kokyaku_no,2,10)  ")

            .AppendLine(" LEFT JOIN ReportIF  WITH (READCOMMITTED) ")
            .AppendLine(" ON t_jiban.kbn  + t_jiban.hosyousyo_no  = ReportIF.kokyaku_no  ")


            .AppendLine("  LEFT JOIN t_teibetu_seikyuu AS tyousa  WITH (READCOMMITTED) ")
            .AppendLine("  ON t_jiban.kbn=tyousa.kbn  ")
            .AppendLine("  AND t_jiban.hosyousyo_no=tyousa.hosyousyo_no ")
            .AppendLine(" AND tyousa.bunrui_cd='100' ")
            .AppendLine(" LEFT JOIN t_teibetu_seikyuu AS kouji  WITH (READCOMMITTED) ")
            .AppendLine(" ON t_jiban.kbn=kouji.kbn  ")
            .AppendLine(" AND t_jiban.hosyousyo_no=kouji.hosyousyo_no ")
            .AppendLine(" AND (KOUJI.bunrui_cd='130' ) ")
            .AppendLine(" LEFT JOIN t_teibetu_seikyuu AS kouji2  WITH (READCOMMITTED) ")
            .AppendLine(" ON t_jiban.kbn=kouji2.kbn  ")
            .AppendLine(" AND t_jiban.hosyousyo_no=kouji2.hosyousyo_no ")
            .AppendLine(" AND (kouji2.bunrui_cd='140' ) ")
            .AppendLine(" LEFT JOIN m_ks_siyou   WITH (READCOMMITTED) ")
            .AppendLine(" ON t_jiban.hantei_cd1=m_ks_siyou.ks_siyou_no ")
            .AppendLine(" LEFT JOIN m_kairy_koj_syubetu ")
            .AppendLine(" ON t_jiban.kairy_koj_syubetu=m_kairy_koj_syubetu.kairy_koj_syubetu_no ")
            .AppendLine(" LEFT JOIN m_busyo_kanri   WITH (READCOMMITTED)  ")
            .AppendLine(" ON m_busyo_kanri.busyo_cd=m_todoufuken.busyo_cd ")
            '==================2012/03/29 Ô´ 405721ÄÌÎ ÇÁ«=================================
            .AppendLine(" LEFT JOIN m_kakutyou_meisyou AS MKM WITH (READCOMMITTED)  ")
            .AppendLine(" ON MKM.meisyou_syubetu=56 ")
            .AppendLine(" AND MKM.code=m_kameiten.torikesi ")
            '==================2012/03/29 Ô´ 405721ÄÌÎ ÇÁª=================================


            .AppendLine(" WHERE t_jiban.kbn IS NOT NULL  ")

            '========================================================

            If dtParam.Rows(0).Item("SosikiLevel").ToString <> String.Empty Then
                If dtParam.Rows(0).Item("eigyouManKbn").ToString = "1" Then
                    .AppendLine(" AND m_busyo_kanri.sosiki_level = @sosiki_level2 ")
                    paramList.Add(MakeParam("@sosiki_level2", SqlDbType.VarChar, 2, dtParam.Rows(0).Item("SosikiLevel")))
                Else
                    .AppendLine(" AND m_busyo_kanri.sosiki_level >= @sosiki_level2 ")
                    paramList.Add(MakeParam("@sosiki_level2", SqlDbType.VarChar, 2, dtParam.Rows(0).Item("SosikiLevel")))

                End If
            End If
            If dtParam.Rows(0).Item("eigyouManKbn").ToString = "1" Then
                .AppendLine(" AND  m_todoufuken.busyo_cd IN ( ")
                .AppendLine(" @busyo_cd2 ")
                If dtParam.Rows(0).Item("SosikiLevel").ToString = String.Empty Then
                    paramList.Add(MakeParam("@busyo_cd2", SqlDbType.VarChar, 4, dtParam.Rows(0).Item("busyo_cd")))
                Else
                    paramList.Add(MakeParam("@busyo_cd2", SqlDbType.VarChar, 4, dtParam.Rows(0).Item("BusyoCd")))
                End If
                .AppendLine(" ) ")
            Else
                If dtParam.Rows(0).Item("BusyoCd").ToString = "0000" Then
                    If dtParam.Rows(0).Item("CHKBusyoCd") Then
                        .AppendLine(" AND  m_todoufuken.busyo_cd IN ( ")
                        .AppendLine(" @busyo_cd2 ")
                        .AppendLine(" ) ")
                        paramList.Add(MakeParam("@busyo_cd2", SqlDbType.VarChar, 4, dtParam.Rows(0).Item("BusyoCd")))
                    End If
                Else
                    If dtParam.Rows(0).Item("BusyoCd").ToString <> String.Empty Then
                        .AppendLine(" AND  m_todoufuken.busyo_cd IN ( ")
                        .AppendLine("SELECT busyo_cd FROM  ")
                        .AppendLine("(SELECT a6.busyo_cd, ")
                        .AppendLine("ISNULL(a1.busyo_cd,'0000') ")
                        .AppendLine("+ISNULL(a2.busyo_cd,'0000') ")
                        .AppendLine("+ISNULL(a3.busyo_cd,'0000') ")
                        .AppendLine("+ISNULL(a4.busyo_cd,'0000') ")
                        .AppendLine("+ISNULL(a5.busyo_cd,'0000') ")
                        .AppendLine("+ISNULL(a6.busyo_cd,'0000') AS cd ")
                        .AppendLine("FROM m_busyo_kanri a6 ")
                        .AppendLine("LEFT JOIN m_busyo_kanri a5 ")
                        .AppendLine("ON a6.joui_soiki = a5.busyo_cd ")
                        .AppendLine("LEFT JOIN m_busyo_kanri a4 ")
                        .AppendLine("ON a5.joui_soiki = a4.busyo_cd ")
                        .AppendLine("LEFT JOIN m_busyo_kanri a3 ")
                        .AppendLine("ON a4.joui_soiki = a3.busyo_cd ")
                        .AppendLine("LEFT JOIN m_busyo_kanri a2 ")
                        .AppendLine("ON a3.joui_soiki = a2.busyo_cd ")
                        .AppendLine("LEFT JOIN m_busyo_kanri a1 ")
                        .AppendLine("ON a2.joui_soiki = a1.busyo_cd) AS TB ")
                        .AppendLine("WHERE   (")
                        If dtParam.Rows(0).Item("CHKBusyoCd") Then

                            .AppendLine("  m_todoufuken.busyo_cd = @busyo_cd ")
                            paramList.Add(MakeParam("@busyo_cd", SqlDbType.VarChar, 4, dtParam.Rows(0).Item("BusyoCd")))
                        Else
                            .AppendLine("SUBSTRING(cd,1,4)=@busyo_cd2 OR  ")
                            .AppendLine("SUBSTRING(cd,5,4)=@busyo_cd2 OR  ")
                            .AppendLine("SUBSTRING(cd,9,4)=@busyo_cd2 OR  ")
                            .AppendLine("SUBSTRING(cd,13,4)=@busyo_cd2 OR  ")
                            .AppendLine("SUBSTRING(cd,17,4)=@busyo_cd2 OR  ")
                            .AppendLine("SUBSTRING(cd,21,4)=@busyo_cd2   ")
                            paramList.Add(MakeParam("@busyo_cd2", SqlDbType.VarChar, 4, dtParam.Rows(0).Item("BusyoCd")))
                        End If
                        .AppendLine(" ) ")
                        .AppendLine(" ) ")
                    Else
                        If dtParam.Rows(0).Item("busyo_cd").ToString = "0000" Or dtParam.Rows(0).Item("t_sansyou_busyo_cd").ToString = "0000" Then
                        Else
                            .AppendLine(" AND  m_todoufuken.busyo_cd IN ( ")
                            .AppendLine("SELECT busyo_cd FROM  ")
                            .AppendLine("(SELECT a6.busyo_cd, ")
                            .AppendLine("ISNULL(a1.busyo_cd,'0000') ")
                            .AppendLine("+ISNULL(a2.busyo_cd,'0000') ")
                            .AppendLine("+ISNULL(a3.busyo_cd,'0000') ")
                            .AppendLine("+ISNULL(a4.busyo_cd,'0000') ")
                            .AppendLine("+ISNULL(a5.busyo_cd,'0000') ")
                            .AppendLine("+ISNULL(a6.busyo_cd,'0000') AS cd ")
                            .AppendLine("FROM m_busyo_kanri a6 ")
                            .AppendLine("LEFT JOIN m_busyo_kanri a5 ")
                            .AppendLine("ON a6.joui_soiki = a5.busyo_cd ")
                            .AppendLine("LEFT JOIN m_busyo_kanri a4 ")
                            .AppendLine("ON a5.joui_soiki = a4.busyo_cd ")
                            .AppendLine("LEFT JOIN m_busyo_kanri a3 ")
                            .AppendLine("ON a4.joui_soiki = a3.busyo_cd ")
                            .AppendLine("LEFT JOIN m_busyo_kanri a2 ")
                            .AppendLine("ON a3.joui_soiki = a2.busyo_cd ")
                            .AppendLine("LEFT JOIN m_busyo_kanri a1 ")
                            .AppendLine("ON a2.joui_soiki = a1.busyo_cd) AS TB ")
                            .AppendLine("WHERE   (")
                            Dim intCHK As Integer = 0
                            If dtParam.Rows(0).Item("busyo_cd").ToString <> String.Empty Then
                                .AppendLine("SUBSTRING(cd,1,4)=@busyo_cd2 OR  ")
                                .AppendLine("SUBSTRING(cd,5,4)=@busyo_cd2 OR  ")
                                .AppendLine("SUBSTRING(cd,9,4)=@busyo_cd2 OR  ")
                                .AppendLine("SUBSTRING(cd,13,4)=@busyo_cd2 OR  ")
                                .AppendLine("SUBSTRING(cd,17,4)=@busyo_cd2 OR  ")
                                .AppendLine("SUBSTRING(cd,21,4)=@busyo_cd2   ")
                                paramList.Add(MakeParam("@busyo_cd2", SqlDbType.VarChar, 4, dtParam.Rows(0).Item("busyo_cd")))
                                intCHK = intCHK + 1
                            End If
                            If dtParam.Rows(0).Item("t_sansyou_busyo_cd").ToString <> String.Empty Then
                                If intCHK = 1 Then
                                    .AppendLine(" OR ")
                                End If
                                .AppendLine(" SUBSTRING(cd,1,4)=@busyo_cd3 OR  ")
                                .AppendLine("SUBSTRING(cd,5,4)=@busyo_cd3 OR  ")
                                .AppendLine("SUBSTRING(cd,9,4)=@busyo_cd3 OR  ")
                                .AppendLine("SUBSTRING(cd,13,4)=@busyo_cd3 OR  ")
                                .AppendLine("SUBSTRING(cd,17,4)=@busyo_cd3 OR  ")
                                .AppendLine("SUBSTRING(cd,21,4)=@busyo_cd3   ")
                                paramList.Add(MakeParam("@busyo_cd3", SqlDbType.VarChar, 4, dtParam.Rows(0).Item("t_sansyou_busyo_cd")))
                            End If
                            .AppendLine(" ) ")
                            .AppendLine(" ) ")
                        End If


                    End If

                End If
            End If

            'æª
            If dtParam.Rows(0).Item("kbn").ToString <> String.Empty Then
                arrKbn = dtParam.Rows(0).Item("kbn").ToString.Split(",")
                .AppendLine(" AND t_jiban.kbn IN ( ")
                For i As Integer = 0 To arrKbn.Length - 1
                    If i = 0 Then
                        .AppendLine("     @kbn" & i & "   ")
                    Else
                        .AppendLine("     ,@kbn" & i & "   ")
                    End If
                    paramList.Add(MakeParam("@kbn" & i, SqlDbType.Char, 1, arrKbn(i)))
                Next
                .AppendLine("       ) ")
            End If

            'Ô
            If dtParam.Rows(0).Item("Bangou").ToString <> String.Empty Then
                strFrom = ""
                strTo = ""
                strFrom = Split(dtParam.Rows(0).Item("Bangou").ToString, ",")(0)
                strTo = Split(dtParam.Rows(0).Item("Bangou").ToString, ",")(1)
                If strFrom <> String.Empty And strTo <> String.Empty Then
                    .AppendLine(" AND t_jiban.hosyousyo_no BETWEEN @hosyousyo_no_from AND @hosyousyo_no_to ")
                    paramList.Add(MakeParam("@hosyousyo_no_from", SqlDbType.VarChar, 10, strFrom))
                    paramList.Add(MakeParam("@hosyousyo_no_to", SqlDbType.VarChar, 10, strTo))
                ElseIf strFrom <> String.Empty And strTo = String.Empty Then
                    .AppendLine(" AND t_jiban.hosyousyo_no = @hosyousyo_no_from ")
                    paramList.Add(MakeParam("@hosyousyo_no_from", SqlDbType.VarChar, 10, strFrom))
                End If
            End If

            'Ëú
            If dtParam.Rows(0).Item("Irai").ToString <> String.Empty Then
                strFrom = ""
                strTo = ""
                strFrom = Split(dtParam.Rows(0).Item("Irai").ToString, ",")(0)
                strTo = Split(dtParam.Rows(0).Item("Irai").ToString, ",")(1)
                If strFrom <> String.Empty And strTo <> String.Empty Then
                    .AppendLine(" AND t_jiban.irai_date BETWEEN @Irai_from AND @Irai_to ")
                    paramList.Add(MakeParam("@Irai_from", SqlDbType.VarChar, 10, strFrom))
                    paramList.Add(MakeParam("@Irai_to", SqlDbType.VarChar, 10, strTo))
                ElseIf strFrom <> String.Empty And strTo = String.Empty Then
                    .AppendLine(" AND t_jiban.irai_date = @Irai_from ")
                    paramList.Add(MakeParam("@Irai_from", SqlDbType.VarChar, 10, strFrom))
                End If
            End If
            'væ
            If dtParam.Rows(0).Item("Keikakusyo").ToString <> String.Empty Then
                strFrom = ""
                strTo = ""
                strFrom = Split(dtParam.Rows(0).Item("Keikakusyo").ToString, ",")(0)
                strTo = Split(dtParam.Rows(0).Item("Keikakusyo").ToString, ",")(1)
                If strFrom <> String.Empty And strTo <> String.Empty Then
                    .AppendLine(" AND t_jiban.keikakusyo_sakusei_date BETWEEN @Keikakusyo_from AND @Keikakusyo_to ")
                    paramList.Add(MakeParam("@Keikakusyo_from", SqlDbType.VarChar, 10, strFrom))
                    paramList.Add(MakeParam("@Keikakusyo_to", SqlDbType.VarChar, 10, strTo))
                ElseIf strFrom <> String.Empty And strTo = String.Empty Then
                    .AppendLine(" AND t_jiban.keikakusyo_sakusei_date = @Keikakusyo_from ")
                    paramList.Add(MakeParam("@Keikakusyo_from", SqlDbType.VarChar, 10, strFrom))
                End If
            End If
            '²¸\è
            If dtParam.Rows(0).Item("TyousaYotei").ToString <> String.Empty Then
                strFrom = ""
                strTo = ""
                strFrom = Split(dtParam.Rows(0).Item("TyousaYotei").ToString, ",")(0)
                strTo = Split(dtParam.Rows(0).Item("TyousaYotei").ToString, ",")(1)
                If strFrom <> String.Empty And strTo <> String.Empty Then
                    .AppendLine(" AND t_jiban.syoudakusyo_tys_date BETWEEN @TyousaYotei_from AND @TyousaYotei_to ")
                    paramList.Add(MakeParam("@TyousaYotei_from", SqlDbType.VarChar, 10, strFrom))
                    paramList.Add(MakeParam("@TyousaYotei_to", SqlDbType.VarChar, 10, strTo))
                ElseIf strFrom <> String.Empty And strTo = String.Empty Then
                    .AppendLine(" AND t_jiban.syoudakusyo_tys_date = @TyousaYotei_from ")
                    paramList.Add(MakeParam("@TyousaYotei_from", SqlDbType.VarChar, 10, strFrom))
                End If
            End If
            '²¸À{
            If dtParam.Rows(0).Item("TyousaJissi").ToString <> String.Empty Then
                strFrom = ""
                strTo = ""
                strFrom = Split(dtParam.Rows(0).Item("TyousaJissi").ToString, ",")(0)
                strTo = Split(dtParam.Rows(0).Item("TyousaJissi").ToString, ",")(1)
                If strFrom <> String.Empty And strTo <> String.Empty Then
                    .AppendLine(" AND t_jiban.tys_jissi_date BETWEEN @TyousaJissi_from AND @TyousaJissi_to ")
                    paramList.Add(MakeParam("@TyousaJissi_from", SqlDbType.VarChar, 10, strFrom))
                    paramList.Add(MakeParam("@TyousaJissi_to", SqlDbType.VarChar, 10, strTo))
                ElseIf strFrom <> String.Empty And strTo = String.Empty Then
                    .AppendLine(" AND t_jiban.tys_jissi_date = @TyousaJissi_from ")
                    paramList.Add(MakeParam("@TyousaJissi_from", SqlDbType.VarChar, 10, strFrom))
                End If
            End If
            '²¸ã

            If dtParam.Rows(0).Item("TyousaUriage").ToString <> String.Empty Then
                strFrom = ""
                strTo = ""
                strFrom = Split(dtParam.Rows(0).Item("TyousaUriage").ToString, ",")(0)
                strTo = Split(dtParam.Rows(0).Item("TyousaUriage").ToString, ",")(1)
                If strFrom <> String.Empty And strTo <> String.Empty Then
                    .AppendLine(" AND tyousa.uri_date BETWEEN @TyousaUriage_from AND @TyousaUriage_to ")
                    paramList.Add(MakeParam("@TyousaUriage_from", SqlDbType.VarChar, 10, strFrom))
                    paramList.Add(MakeParam("@TyousaUriage_to", SqlDbType.VarChar, 10, strTo))
                ElseIf strFrom <> String.Empty And strTo = String.Empty Then
                    .AppendLine(" AND tyousa.uri_date = @TyousaUriage_from ")
                    paramList.Add(MakeParam("@TyousaUriage_from", SqlDbType.VarChar, 10, strFrom))
                End If
            End If
            'H\è
            If dtParam.Rows(0).Item("KoujiYotei").ToString <> String.Empty Then
                strFrom = ""
                strTo = ""
                strFrom = Split(dtParam.Rows(0).Item("KoujiYotei").ToString, ",")(0)
                strTo = Split(dtParam.Rows(0).Item("KoujiYotei").ToString, ",")(1)
                If strFrom <> String.Empty And strTo <> String.Empty Then
                    .AppendLine(" AND (t_jiban.kairy_koj_kanry_yotei_date BETWEEN @KoujiYotei_from AND @KoujiYotei_to ")
                    .AppendLine(" OR t_jiban.t_koj_kanry_yotei_date BETWEEN @KoujiYotei_from AND @KoujiYotei_to ) ")
                    paramList.Add(MakeParam("@KoujiYotei_from", SqlDbType.VarChar, 10, strFrom))
                    paramList.Add(MakeParam("@KoujiYotei_to", SqlDbType.VarChar, 10, strTo))
                ElseIf strFrom <> String.Empty And strTo = String.Empty Then
                    .AppendLine(" AND (t_jiban.kairy_koj_kanry_yotei_date = @KoujiYotei_from OR t_jiban.t_koj_kanry_yotei_date = @KoujiYotei_from)")
                    paramList.Add(MakeParam("@KoujiYotei_from", SqlDbType.VarChar, 10, strFrom))
                End If
            End If

            'HÀ{
            If dtParam.Rows(0).Item("KoujiJissi").ToString <> String.Empty Then
                strFrom = ""
                strTo = ""
                strFrom = Split(dtParam.Rows(0).Item("KoujiJissi").ToString, ",")(0)
                strTo = Split(dtParam.Rows(0).Item("KoujiJissi").ToString, ",")(1)
                If strFrom <> String.Empty And strTo <> String.Empty Then
                    .AppendLine(" AND (t_jiban.kairy_koj_date BETWEEN @KoujiJissi_from AND @KoujiJissi_to ")
                    .AppendLine(" OR t_jiban.t_koj_date BETWEEN @KoujiJissi_from AND @KoujiJissi_to ) ")
                    paramList.Add(MakeParam("@KoujiJissi_from", SqlDbType.VarChar, 10, strFrom))
                    paramList.Add(MakeParam("@KoujiJissi_to", SqlDbType.VarChar, 10, strTo))
                ElseIf strFrom <> String.Empty And strTo = String.Empty Then
                    .AppendLine(" AND (t_jiban.kairy_koj_date = @KoujiJissi_from OR t_jiban.t_koj_date = @KoujiJissi_from)")
                    paramList.Add(MakeParam("@KoujiJissi_from", SqlDbType.VarChar, 10, strFrom))
                End If
            End If
            'Hã
            If dtParam.Rows(0).Item("KoujiUriage").ToString <> String.Empty Then
                strFrom = ""
                strTo = ""
                strFrom = Split(dtParam.Rows(0).Item("KoujiUriage").ToString, ",")(0)
                strTo = Split(dtParam.Rows(0).Item("KoujiUriage").ToString, ",")(1)
                If dtParam.Rows(0).Item("CHKKouji") Then
                    If strFrom <> String.Empty And strTo <> String.Empty Then
                        .AppendLine(" AND ((kouji.uri_date BETWEEN @KoujiUriage_from AND @KoujiUriage_to AND (t_jiban.koj_gaisya_seikyuu_umu=@koj_gaisya_seikyuu_umu OR t_jiban.koj_gaisya_seikyuu_umu IS NULL)) ")
                        .AppendLine(" OR (kouji2.uri_date BETWEEN @KoujiUriage_from AND @KoujiUriage_to AND (t_jiban.t_koj_kaisya_seikyuu_umu=@koj_gaisya_seikyuu_umu OR t_jiban.t_koj_kaisya_seikyuu_umu IS NULL)))  ")
                        paramList.Add(MakeParam("@KoujiUriage_from", SqlDbType.VarChar, 10, strFrom))
                        paramList.Add(MakeParam("@KoujiUriage_to", SqlDbType.VarChar, 10, strTo))
                        paramList.Add(MakeParam("@koj_gaisya_seikyuu_umu", SqlDbType.Int, 4, "0"))
                    ElseIf strFrom <> String.Empty And strTo = String.Empty Then
                        .AppendLine(" AND ((kouji.uri_date = @KoujiUriage_from AND (t_jiban.koj_gaisya_seikyuu_umu=@koj_gaisya_seikyuu_umu OR t_jiban.koj_gaisya_seikyuu_umu IS NULL)) OR (kouji2.uri_date = @KoujiUriage_from AND (t_jiban.t_koj_kaisya_seikyuu_umu=@koj_gaisya_seikyuu_umu OR t_jiban.t_koj_kaisya_seikyuu_umu IS NULL)))")
                        paramList.Add(MakeParam("@KoujiUriage_from", SqlDbType.VarChar, 10, strFrom))
                        paramList.Add(MakeParam("@koj_gaisya_seikyuu_umu", SqlDbType.Int, 4, "0"))
                    End If
                Else
                    If strFrom <> String.Empty And strTo <> String.Empty Then
                        .AppendLine(" AND (kouji.uri_date BETWEEN @KoujiUriage_from AND @KoujiUriage_to ")
                        .AppendLine(" OR kouji2.uri_date BETWEEN @KoujiUriage_from AND @KoujiUriage_to ) ")
                        paramList.Add(MakeParam("@KoujiUriage_from", SqlDbType.VarChar, 10, strFrom))
                        paramList.Add(MakeParam("@KoujiUriage_to", SqlDbType.VarChar, 10, strTo))
                    ElseIf strFrom <> String.Empty And strTo = String.Empty Then
                        .AppendLine(" AND (kouji.uri_date = @KoujiUriage_from OR kouji2.uri_date = @KoujiUriage_from)")
                        paramList.Add(MakeParam("@KoujiUriage_from", SqlDbType.VarChar, 10, strFrom))
                    End If
                End If
                .AppendLine(" AND (kouji.kbn is not null OR kouji2.kbn is not null )")
            End If
            'Á¿XR[h

            If dtParam.Rows(0).Item("KameitenCd").ToString <> String.Empty Then
                .AppendLine(" AND m_kameiten.kameiten_cd = @KameitenCd  ")
                paramList.Add(MakeParam("@KameitenCd", SqlDbType.VarChar, 5, dtParam.Rows(0).Item("KameitenCd")))
            End If
            's¹{§

            If dtParam.Rows(0).Item("todoufuken").ToString <> String.Empty Then
                arrKbn = dtParam.Rows(0).Item("todoufuken").ToString.Split(",")
                .AppendLine(" AND m_todoufuken.todouhuken_cd IN ( ")
                For i As Integer = 0 To arrKbn.Length - 1
                    If i = 0 Then
                        .AppendLine("     @todoufuken" & i & "   ")
                    Else
                        .AppendLine("     ,@todoufuken" & i & "   ")
                    End If
                    paramList.Add(MakeParam("@todoufuken" & i, SqlDbType.VarChar, 2, arrKbn(i)))
                Next
                .AppendLine("       ) ")
            End If

            'nñR[h

            If dtParam.Rows(0).Item("KeiretuCd").ToString <> String.Empty Then
                .AppendLine(" AND m_kameiten.keiretu_cd = @KeiretuCd  ")
                paramList.Add(MakeParam("@KeiretuCd", SqlDbType.VarChar, 5, dtParam.Rows(0).Item("KeiretuCd")))
            End If
            'cÆR[h
            If dtParam.Rows(0).Item("EigyousyoCd").ToString <> String.Empty Then
                .AppendLine(" AND m_kameiten.eigyousyo_cd = @EigyousyoCd  ")
                paramList.Add(MakeParam("@EigyousyoCd", SqlDbType.VarChar, 5, dtParam.Rows(0).Item("EigyousyoCd")))
            End If


            'ScÆID
            If dtParam.Rows(0).Item("TantouEigyouID").ToString <> String.Empty Then
                .AppendLine(" AND m_kameiten.eigyou_tantousya_mei = @TantouEigyouID  ")
                paramList.Add(MakeParam("@TantouEigyouID", SqlDbType.VarChar, 64, dtParam.Rows(0).Item("TantouEigyouID")))
            End If

            .AppendLine(" ORDER BY  t_jiban.kbn, t_jiban.hosyousyo_no  ")

        End With


        'õÀs()
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsBukkenJyouhouDataSet, _
                    dsBukkenJyouhouDataSet.BukkenJyouhouTable.TableName, paramList.ToArray)

        Return dsBukkenJyouhouDataSet.BukkenJyouhouTable

    End Function

    ''' <summary>Á¿Xîñf[^Âðæ¾·é</summary>
    ''' <param name="dtParam">Paramf[^Z[g</param>
    ''' <returns>Á¿Xîñf[^Â</returns>
    Public Function SelBukkenJyouhouInfoCount(ByVal dtParam As DataTable) As Integer

        ' DataSetCX^XÌ¶¬()
        Dim dsBukkenJyouhouDataSet As New BukkenJyouhouDataSet

        'SQL¶Ì¶¬
        Dim commandTextSb As New StringBuilder

        'p[^i[
        Dim paramList As New List(Of SqlClient.SqlParameter)
        Dim strFrom As String = ""
        Dim strTo As String = ""
        Dim arrKbn() As String
        'SQL¶
        With commandTextSb
            .AppendLine(" SELECT ")
            .AppendLine(" COUNT(t_jiban.kbn + t_jiban.hosyousyo_no) AS hosyousyo_no_count")
            .AppendLine(" FROM t_jiban WITH (READCOMMITTED) ")
            .AppendLine(" LEFT JOIN m_kameiten  WITH (READCOMMITTED) ")
            .AppendLine(" ON T_jiban.kameiten_cd=m_kameiten.kameiten_cd ")
            .AppendLine(" LEFT JOIN m_todoufuken  WITH (READCOMMITTED) ")
            .AppendLine(" ON m_todoufuken.todouhuken_cd=m_kameiten.todouhuken_cd ")

            .AppendLine(" LEFT JOIN m_data_haki  WITH (READCOMMITTED) ")
            .AppendLine(" ON t_jiban.data_haki_syubetu=m_data_haki.data_haki_no ")
            .AppendLine(" LEFT JOIN m_tyousahouhou  WITH (READCOMMITTED) ")
            .AppendLine(" ON t_jiban.tys_houhou=m_tyousahouhou.tys_houhou_no ")
            '.AppendLine(" LEFT JOIN ReportIF  WITH (READCOMMITTED) ")
            '.AppendLine(" ON t_jiban.kbn=SUBSTRING(ReportIF.kokyaku_no,1,1)  ")
            '.AppendLine("  AND t_jiban.hosyousyo_no=SUBSTRING(ReportIF.kokyaku_no,2,10)  ")

            .AppendLine(" LEFT JOIN ReportIF  WITH (READCOMMITTED) ")
            .AppendLine(" ON t_jiban.kbn  + t_jiban.hosyousyo_no  = ReportIF.kokyaku_no  ")

            .AppendLine("  LEFT JOIN t_teibetu_seikyuu AS tyousa  WITH (READCOMMITTED) ")
            .AppendLine("  ON t_jiban.kbn=tyousa.kbn  ")
            .AppendLine("  AND t_jiban.hosyousyo_no=tyousa.hosyousyo_no ")
            .AppendLine(" AND tyousa.bunrui_cd='100' ")
            .AppendLine(" LEFT JOIN t_teibetu_seikyuu AS kouji  WITH (READCOMMITTED) ")
            .AppendLine(" ON t_jiban.kbn=kouji.kbn  ")
            .AppendLine(" AND t_jiban.hosyousyo_no=kouji.hosyousyo_no ")
            .AppendLine(" AND (KOUJI.bunrui_cd='130' ) ")
            .AppendLine(" LEFT JOIN t_teibetu_seikyuu AS kouji2  WITH (READCOMMITTED) ")
            .AppendLine(" ON t_jiban.kbn=kouji2.kbn  ")
            .AppendLine(" AND t_jiban.hosyousyo_no=kouji2.hosyousyo_no ")
            .AppendLine(" AND (kouji2.bunrui_cd='140' ) ")
            .AppendLine(" LEFT JOIN m_ks_siyou   WITH (READCOMMITTED) ")
            .AppendLine(" ON t_jiban.hantei_cd1=m_ks_siyou.ks_siyou_no ")
            .AppendLine(" LEFT JOIN m_kairy_koj_syubetu ")
            .AppendLine(" ON t_jiban.kairy_koj_syubetu=m_kairy_koj_syubetu.kairy_koj_syubetu_no ")
            .AppendLine(" LEFT JOIN m_busyo_kanri   WITH (READCOMMITTED)  ")
            .AppendLine(" ON m_busyo_kanri.busyo_cd=m_todoufuken.busyo_cd ")

            .AppendLine(" WHERE t_jiban.kbn IS NOT NULL  ")

            '========================================================
            '.AppendLine(" and ReportIF.status>=699  ")
            '.AppendLine(" and m_data_haki.haki_syubetu  IS NOT NULL ")
            '.AppendLine(" and t_jiban.irai_date  IS NOT NULL ")
            '.AppendLine(" and t_jiban.tys_kibou_date IS NOT NULL ")
            '.AppendLine(" and m_tyousahouhou.tys_houhou_mei_ryaku IS NOT NULL ")
            '.AppendLine(" and t_jiban.sesyu_mei IS NOT NULL ")
            '.AppendLine(" and t_jiban.kameiten_cd IS NOT NULL ")
            '.AppendLine(" and m_kameiten.kameiten_mei1 IS NOT NULL ")

            '.AppendLine(" and t_jiban.irai_tantousya_mei IS NOT NULL ")
            '.AppendLine(" and m_ks_siyou.ks_siyou IS NOT NULL ")
            '.AppendLine(" and m_kairy_koj_syubetu.kairy_koj_syubetu IS NOT NULL ")
            '.AppendLine(" and t_jiban.hosyousyo_hak_date IS NOT NULL ")

            '.AppendLine(" and t_jiban.syoudakusyo_tys_date IS NOT NULL ")
            '.AppendLine(" and t_jiban.tys_jissi_date IS NOT NULL ")
            '.AppendLine(" and tyousa.uri_date IS NOT NULL ")
            '.AppendLine(" and t_jiban.kairy_koj_kanry_yotei_date IS NOT NULL ")
            '.AppendLine(" and t_jiban.kairy_koj_date IS NOT NULL ")

            '=============================================================
            If dtParam.Rows(0).Item("SosikiLevel").ToString <> String.Empty Then
                If dtParam.Rows(0).Item("eigyouManKbn").ToString = "1" Then
                    .AppendLine(" AND m_busyo_kanri.sosiki_level = @sosiki_level2 ")
                    paramList.Add(MakeParam("@sosiki_level2", SqlDbType.VarChar, 2, dtParam.Rows(0).Item("SosikiLevel")))
                Else
                    .AppendLine(" AND m_busyo_kanri.sosiki_level >= @sosiki_level2 ")
                    paramList.Add(MakeParam("@sosiki_level2", SqlDbType.VarChar, 2, dtParam.Rows(0).Item("SosikiLevel")))

                End If
            End If
            If dtParam.Rows(0).Item("eigyouManKbn").ToString = "1" Then
                .AppendLine(" AND  m_todoufuken.busyo_cd IN ( ")
                .AppendLine(" @busyo_cd2 ")
                If dtParam.Rows(0).Item("SosikiLevel").ToString = String.Empty Then
                    paramList.Add(MakeParam("@busyo_cd2", SqlDbType.VarChar, 4, dtParam.Rows(0).Item("busyo_cd")))
                Else
                    paramList.Add(MakeParam("@busyo_cd2", SqlDbType.VarChar, 4, dtParam.Rows(0).Item("BusyoCd")))
                End If
                .AppendLine(" ) ")
            Else
                If dtParam.Rows(0).Item("BusyoCd").ToString = "0000" Then
                    If dtParam.Rows(0).Item("CHKBusyoCd") Then
                        .AppendLine(" AND  m_todoufuken.busyo_cd IN ( ")
                        .AppendLine(" @busyo_cd2 ")
                        .AppendLine(" ) ")
                        paramList.Add(MakeParam("@busyo_cd2", SqlDbType.VarChar, 4, dtParam.Rows(0).Item("BusyoCd")))
                    End If
                Else
                    If dtParam.Rows(0).Item("BusyoCd").ToString <> String.Empty Then
                        .AppendLine(" AND  m_todoufuken.busyo_cd IN ( ")
                        .AppendLine("SELECT busyo_cd FROM  ")
                        .AppendLine("(SELECT a6.busyo_cd, ")
                        .AppendLine("ISNULL(a1.busyo_cd,'0000') ")
                        .AppendLine("+ISNULL(a2.busyo_cd,'0000') ")
                        .AppendLine("+ISNULL(a3.busyo_cd,'0000') ")
                        .AppendLine("+ISNULL(a4.busyo_cd,'0000') ")
                        .AppendLine("+ISNULL(a5.busyo_cd,'0000') ")
                        .AppendLine("+ISNULL(a6.busyo_cd,'0000') AS cd ")
                        .AppendLine("FROM m_busyo_kanri a6 ")
                        .AppendLine("LEFT JOIN m_busyo_kanri a5 ")
                        .AppendLine("ON a6.joui_soiki = a5.busyo_cd ")
                        .AppendLine("LEFT JOIN m_busyo_kanri a4 ")
                        .AppendLine("ON a5.joui_soiki = a4.busyo_cd ")
                        .AppendLine("LEFT JOIN m_busyo_kanri a3 ")
                        .AppendLine("ON a4.joui_soiki = a3.busyo_cd ")
                        .AppendLine("LEFT JOIN m_busyo_kanri a2 ")
                        .AppendLine("ON a3.joui_soiki = a2.busyo_cd ")
                        .AppendLine("LEFT JOIN m_busyo_kanri a1 ")
                        .AppendLine("ON a2.joui_soiki = a1.busyo_cd) AS TB ")
                        .AppendLine("WHERE   (")
                        If dtParam.Rows(0).Item("CHKBusyoCd") Then

                            .AppendLine("  m_todoufuken.busyo_cd = @busyo_cd ")
                            paramList.Add(MakeParam("@busyo_cd", SqlDbType.VarChar, 4, dtParam.Rows(0).Item("BusyoCd")))
                        Else
                            .AppendLine("SUBSTRING(cd,1,4)=@busyo_cd2 OR  ")
                            .AppendLine("SUBSTRING(cd,5,4)=@busyo_cd2 OR  ")
                            .AppendLine("SUBSTRING(cd,9,4)=@busyo_cd2 OR  ")
                            .AppendLine("SUBSTRING(cd,13,4)=@busyo_cd2 OR  ")
                            .AppendLine("SUBSTRING(cd,17,4)=@busyo_cd2 OR  ")
                            .AppendLine("SUBSTRING(cd,21,4)=@busyo_cd2   ")
                            paramList.Add(MakeParam("@busyo_cd2", SqlDbType.VarChar, 4, dtParam.Rows(0).Item("BusyoCd")))
                        End If
                        .AppendLine(" ) ")
                        .AppendLine(" ) ")
                    Else
                        If dtParam.Rows(0).Item("busyo_cd").ToString = "0000" Or dtParam.Rows(0).Item("t_sansyou_busyo_cd").ToString = "0000" Then
                        Else
                            .AppendLine(" AND  m_todoufuken.busyo_cd IN ( ")
                            .AppendLine("SELECT busyo_cd FROM  ")
                            .AppendLine("(SELECT a6.busyo_cd, ")
                            .AppendLine("ISNULL(a1.busyo_cd,'0000') ")
                            .AppendLine("+ISNULL(a2.busyo_cd,'0000') ")
                            .AppendLine("+ISNULL(a3.busyo_cd,'0000') ")
                            .AppendLine("+ISNULL(a4.busyo_cd,'0000') ")
                            .AppendLine("+ISNULL(a5.busyo_cd,'0000') ")
                            .AppendLine("+ISNULL(a6.busyo_cd,'0000') AS cd ")
                            .AppendLine("FROM m_busyo_kanri a6 ")
                            .AppendLine("LEFT JOIN m_busyo_kanri a5 ")
                            .AppendLine("ON a6.joui_soiki = a5.busyo_cd ")
                            .AppendLine("LEFT JOIN m_busyo_kanri a4 ")
                            .AppendLine("ON a5.joui_soiki = a4.busyo_cd ")
                            .AppendLine("LEFT JOIN m_busyo_kanri a3 ")
                            .AppendLine("ON a4.joui_soiki = a3.busyo_cd ")
                            .AppendLine("LEFT JOIN m_busyo_kanri a2 ")
                            .AppendLine("ON a3.joui_soiki = a2.busyo_cd ")
                            .AppendLine("LEFT JOIN m_busyo_kanri a1 ")
                            .AppendLine("ON a2.joui_soiki = a1.busyo_cd) AS TB ")
                            .AppendLine("WHERE   (")
                            Dim intCHK As Integer = 0
                            If dtParam.Rows(0).Item("busyo_cd").ToString <> String.Empty Then
                                .AppendLine("SUBSTRING(cd,1,4)=@busyo_cd2 OR  ")
                                .AppendLine("SUBSTRING(cd,5,4)=@busyo_cd2 OR  ")
                                .AppendLine("SUBSTRING(cd,9,4)=@busyo_cd2 OR  ")
                                .AppendLine("SUBSTRING(cd,13,4)=@busyo_cd2 OR  ")
                                .AppendLine("SUBSTRING(cd,17,4)=@busyo_cd2 OR  ")
                                .AppendLine("SUBSTRING(cd,21,4)=@busyo_cd2   ")
                                paramList.Add(MakeParam("@busyo_cd2", SqlDbType.VarChar, 4, dtParam.Rows(0).Item("busyo_cd")))
                                intCHK = intCHK + 1
                            End If
                            If dtParam.Rows(0).Item("t_sansyou_busyo_cd").ToString <> String.Empty Then
                                If intCHK = 1 Then
                                    .AppendLine(" OR ")
                                End If
                                .AppendLine(" SUBSTRING(cd,1,4)=@busyo_cd3 OR  ")
                                .AppendLine("SUBSTRING(cd,5,4)=@busyo_cd3 OR  ")
                                .AppendLine("SUBSTRING(cd,9,4)=@busyo_cd3 OR  ")
                                .AppendLine("SUBSTRING(cd,13,4)=@busyo_cd3 OR  ")
                                .AppendLine("SUBSTRING(cd,17,4)=@busyo_cd3 OR  ")
                                .AppendLine("SUBSTRING(cd,21,4)=@busyo_cd3   ")
                                paramList.Add(MakeParam("@busyo_cd3", SqlDbType.VarChar, 4, dtParam.Rows(0).Item("t_sansyou_busyo_cd")))
                            End If
                            .AppendLine(" ) ")
                            .AppendLine(" ) ")
                        End If


                    End If

                End If
            End If

            'æª
            If dtParam.Rows(0).Item("kbn").ToString <> String.Empty Then
                arrKbn = dtParam.Rows(0).Item("kbn").ToString.Split(",")
                .AppendLine(" AND t_jiban.kbn IN ( ")
                For i As Integer = 0 To arrKbn.Length - 1
                    If i = 0 Then
                        .AppendLine("     @kbn" & i & "   ")
                    Else
                        .AppendLine("     ,@kbn" & i & "   ")
                    End If
                    paramList.Add(MakeParam("@kbn" & i, SqlDbType.Char, 1, arrKbn(i)))
                Next
                .AppendLine("       ) ")
            End If

            'Ô
            If dtParam.Rows(0).Item("Bangou").ToString <> String.Empty Then
                strFrom = ""
                strTo = ""
                strFrom = Split(dtParam.Rows(0).Item("Bangou").ToString, ",")(0)
                strTo = Split(dtParam.Rows(0).Item("Bangou").ToString, ",")(1)
                If strFrom <> String.Empty And strTo <> String.Empty Then
                    .AppendLine(" AND t_jiban.hosyousyo_no BETWEEN @hosyousyo_no_from AND @hosyousyo_no_to ")
                    paramList.Add(MakeParam("@hosyousyo_no_from", SqlDbType.VarChar, 10, strFrom))
                    paramList.Add(MakeParam("@hosyousyo_no_to", SqlDbType.VarChar, 10, strTo))
                ElseIf strFrom <> String.Empty And strTo = String.Empty Then
                    .AppendLine(" AND t_jiban.hosyousyo_no = @hosyousyo_no_from ")
                    paramList.Add(MakeParam("@hosyousyo_no_from", SqlDbType.VarChar, 10, strFrom))
                End If
            End If

            'Ëú
            If dtParam.Rows(0).Item("Irai").ToString <> String.Empty Then
                strFrom = ""
                strTo = ""
                strFrom = Split(dtParam.Rows(0).Item("Irai").ToString, ",")(0)
                strTo = Split(dtParam.Rows(0).Item("Irai").ToString, ",")(1)
                If strFrom <> String.Empty And strTo <> String.Empty Then
                    .AppendLine(" AND t_jiban.irai_date BETWEEN @Irai_from AND @Irai_to ")
                    paramList.Add(MakeParam("@Irai_from", SqlDbType.VarChar, 10, strFrom))
                    paramList.Add(MakeParam("@Irai_to", SqlDbType.VarChar, 10, strTo))
                ElseIf strFrom <> String.Empty And strTo = String.Empty Then
                    .AppendLine(" AND t_jiban.irai_date = @Irai_from ")
                    paramList.Add(MakeParam("@Irai_from", SqlDbType.VarChar, 10, strFrom))
                End If
            End If
            'væ
            If dtParam.Rows(0).Item("Keikakusyo").ToString <> String.Empty Then
                strFrom = ""
                strTo = ""
                strFrom = Split(dtParam.Rows(0).Item("Keikakusyo").ToString, ",")(0)
                strTo = Split(dtParam.Rows(0).Item("Keikakusyo").ToString, ",")(1)
                If strFrom <> String.Empty And strTo <> String.Empty Then
                    .AppendLine(" AND t_jiban.keikakusyo_sakusei_date BETWEEN @Keikakusyo_from AND @Keikakusyo_to ")
                    paramList.Add(MakeParam("@Keikakusyo_from", SqlDbType.VarChar, 10, strFrom))
                    paramList.Add(MakeParam("@Keikakusyo_to", SqlDbType.VarChar, 10, strTo))
                ElseIf strFrom <> String.Empty And strTo = String.Empty Then
                    .AppendLine(" AND t_jiban.keikakusyo_sakusei_date = @Keikakusyo_from ")
                    paramList.Add(MakeParam("@Keikakusyo_from", SqlDbType.VarChar, 10, strFrom))
                End If
            End If
            '²¸\è
            If dtParam.Rows(0).Item("TyousaYotei").ToString <> String.Empty Then
                strFrom = ""
                strTo = ""
                strFrom = Split(dtParam.Rows(0).Item("TyousaYotei").ToString, ",")(0)
                strTo = Split(dtParam.Rows(0).Item("TyousaYotei").ToString, ",")(1)
                If strFrom <> String.Empty And strTo <> String.Empty Then
                    .AppendLine(" AND t_jiban.syoudakusyo_tys_date BETWEEN @TyousaYotei_from AND @TyousaYotei_to ")
                    paramList.Add(MakeParam("@TyousaYotei_from", SqlDbType.VarChar, 10, strFrom))
                    paramList.Add(MakeParam("@TyousaYotei_to", SqlDbType.VarChar, 10, strTo))
                ElseIf strFrom <> String.Empty And strTo = String.Empty Then
                    .AppendLine(" AND t_jiban.syoudakusyo_tys_date = @TyousaYotei_from ")
                    paramList.Add(MakeParam("@TyousaYotei_from", SqlDbType.VarChar, 10, strFrom))
                End If
            End If
            '²¸À{
            If dtParam.Rows(0).Item("TyousaJissi").ToString <> String.Empty Then
                strFrom = ""
                strTo = ""
                strFrom = Split(dtParam.Rows(0).Item("TyousaJissi").ToString, ",")(0)
                strTo = Split(dtParam.Rows(0).Item("TyousaJissi").ToString, ",")(1)
                If strFrom <> String.Empty And strTo <> String.Empty Then
                    .AppendLine(" AND t_jiban.tys_jissi_date BETWEEN @TyousaJissi_from AND @TyousaJissi_to ")
                    paramList.Add(MakeParam("@TyousaJissi_from", SqlDbType.VarChar, 10, strFrom))
                    paramList.Add(MakeParam("@TyousaJissi_to", SqlDbType.VarChar, 10, strTo))
                ElseIf strFrom <> String.Empty And strTo = String.Empty Then
                    .AppendLine(" AND t_jiban.tys_jissi_date = @TyousaJissi_from ")
                    paramList.Add(MakeParam("@TyousaJissi_from", SqlDbType.VarChar, 10, strFrom))
                End If
            End If
            '²¸ã

            If dtParam.Rows(0).Item("TyousaUriage").ToString <> String.Empty Then
                strFrom = ""
                strTo = ""
                strFrom = Split(dtParam.Rows(0).Item("TyousaUriage").ToString, ",")(0)
                strTo = Split(dtParam.Rows(0).Item("TyousaUriage").ToString, ",")(1)
                If strFrom <> String.Empty And strTo <> String.Empty Then
                    .AppendLine(" AND tyousa.uri_date BETWEEN @TyousaUriage_from AND @TyousaUriage_to ")
                    paramList.Add(MakeParam("@TyousaUriage_from", SqlDbType.VarChar, 10, strFrom))
                    paramList.Add(MakeParam("@TyousaUriage_to", SqlDbType.VarChar, 10, strTo))
                ElseIf strFrom <> String.Empty And strTo = String.Empty Then
                    .AppendLine(" AND tyousa.uri_date = @TyousaUriage_from ")
                    paramList.Add(MakeParam("@TyousaUriage_from", SqlDbType.VarChar, 10, strFrom))
                End If
            End If
            'H\è
            If dtParam.Rows(0).Item("KoujiYotei").ToString <> String.Empty Then
                strFrom = ""
                strTo = ""
                strFrom = Split(dtParam.Rows(0).Item("KoujiYotei").ToString, ",")(0)
                strTo = Split(dtParam.Rows(0).Item("KoujiYotei").ToString, ",")(1)
                If strFrom <> String.Empty And strTo <> String.Empty Then
                    .AppendLine(" AND (t_jiban.kairy_koj_kanry_yotei_date BETWEEN @KoujiYotei_from AND @KoujiYotei_to ")
                    .AppendLine(" OR t_jiban.t_koj_kanry_yotei_date BETWEEN @KoujiYotei_from AND @KoujiYotei_to ) ")
                    paramList.Add(MakeParam("@KoujiYotei_from", SqlDbType.VarChar, 10, strFrom))
                    paramList.Add(MakeParam("@KoujiYotei_to", SqlDbType.VarChar, 10, strTo))
                ElseIf strFrom <> String.Empty And strTo = String.Empty Then
                    .AppendLine(" AND (t_jiban.kairy_koj_kanry_yotei_date = @KoujiYotei_from OR t_jiban.t_koj_kanry_yotei_date = @KoujiYotei_from)")
                    paramList.Add(MakeParam("@KoujiYotei_from", SqlDbType.VarChar, 10, strFrom))
                End If
            End If

            'HÀ{
            If dtParam.Rows(0).Item("KoujiJissi").ToString <> String.Empty Then
                strFrom = ""
                strTo = ""
                strFrom = Split(dtParam.Rows(0).Item("KoujiJissi").ToString, ",")(0)
                strTo = Split(dtParam.Rows(0).Item("KoujiJissi").ToString, ",")(1)
                If strFrom <> String.Empty And strTo <> String.Empty Then
                    .AppendLine(" AND (t_jiban.kairy_koj_date BETWEEN @KoujiJissi_from AND @KoujiJissi_to ")
                    .AppendLine(" OR t_jiban.t_koj_date BETWEEN @KoujiJissi_from AND @KoujiJissi_to ) ")
                    paramList.Add(MakeParam("@KoujiJissi_from", SqlDbType.VarChar, 10, strFrom))
                    paramList.Add(MakeParam("@KoujiJissi_to", SqlDbType.VarChar, 10, strTo))
                ElseIf strFrom <> String.Empty And strTo = String.Empty Then
                    .AppendLine(" AND (t_jiban.kairy_koj_date = @KoujiJissi_from OR t_jiban.t_koj_date = @KoujiJissi_from)")
                    paramList.Add(MakeParam("@KoujiJissi_from", SqlDbType.VarChar, 10, strFrom))
                End If
            End If
            'Hã
            If dtParam.Rows(0).Item("KoujiUriage").ToString <> String.Empty Then
                strFrom = ""
                strTo = ""
                strFrom = Split(dtParam.Rows(0).Item("KoujiUriage").ToString, ",")(0)
                strTo = Split(dtParam.Rows(0).Item("KoujiUriage").ToString, ",")(1)
                If dtParam.Rows(0).Item("CHKKouji") Then
                    If strFrom <> String.Empty And strTo <> String.Empty Then
                        .AppendLine(" AND ((kouji.uri_date BETWEEN @KoujiUriage_from AND @KoujiUriage_to AND (t_jiban.koj_gaisya_seikyuu_umu=@koj_gaisya_seikyuu_umu OR t_jiban.koj_gaisya_seikyuu_umu IS NULL)) ")
                        .AppendLine(" OR (kouji2.uri_date BETWEEN @KoujiUriage_from AND @KoujiUriage_to AND (t_jiban.t_koj_kaisya_seikyuu_umu=@koj_gaisya_seikyuu_umu OR t_jiban.t_koj_kaisya_seikyuu_umu IS NULL)))  ")
                        paramList.Add(MakeParam("@KoujiUriage_from", SqlDbType.VarChar, 10, strFrom))
                        paramList.Add(MakeParam("@KoujiUriage_to", SqlDbType.VarChar, 10, strTo))
                        paramList.Add(MakeParam("@koj_gaisya_seikyuu_umu", SqlDbType.Int, 4, "0"))
                    ElseIf strFrom <> String.Empty And strTo = String.Empty Then
                        .AppendLine(" AND ((kouji.uri_date = @KoujiUriage_from AND (t_jiban.koj_gaisya_seikyuu_umu=@koj_gaisya_seikyuu_umu OR t_jiban.koj_gaisya_seikyuu_umu IS NULL)) OR (kouji2.uri_date = @KoujiUriage_from AND (t_jiban.t_koj_kaisya_seikyuu_umu=@koj_gaisya_seikyuu_umu OR t_jiban.t_koj_kaisya_seikyuu_umu IS NULL)))")
                        paramList.Add(MakeParam("@KoujiUriage_from", SqlDbType.VarChar, 10, strFrom))
                        paramList.Add(MakeParam("@koj_gaisya_seikyuu_umu", SqlDbType.Int, 4, "0"))
                    End If
                Else
                    If strFrom <> String.Empty And strTo <> String.Empty Then
                        .AppendLine(" AND (kouji.uri_date BETWEEN @KoujiUriage_from AND @KoujiUriage_to ")
                        .AppendLine(" OR kouji2.uri_date BETWEEN @KoujiUriage_from AND @KoujiUriage_to ) ")
                        paramList.Add(MakeParam("@KoujiUriage_from", SqlDbType.VarChar, 10, strFrom))
                        paramList.Add(MakeParam("@KoujiUriage_to", SqlDbType.VarChar, 10, strTo))
                    ElseIf strFrom <> String.Empty And strTo = String.Empty Then
                        .AppendLine(" AND (kouji.uri_date = @KoujiUriage_from OR kouji2.uri_date = @KoujiUriage_from)")
                        paramList.Add(MakeParam("@KoujiUriage_from", SqlDbType.VarChar, 10, strFrom))
                    End If
                End If
                .AppendLine(" AND (kouji.kbn is not null OR kouji2.kbn is not null )")
            End If
            'Á¿XR[h

            If dtParam.Rows(0).Item("KameitenCd").ToString <> String.Empty Then
                .AppendLine(" AND m_kameiten.kameiten_cd = @KameitenCd  ")
                paramList.Add(MakeParam("@KameitenCd", SqlDbType.VarChar, 5, dtParam.Rows(0).Item("KameitenCd")))
            End If
            's¹{§

            If dtParam.Rows(0).Item("todoufuken").ToString <> String.Empty Then
                arrKbn = dtParam.Rows(0).Item("todoufuken").ToString.Split(",")
                .AppendLine(" AND m_todoufuken.todouhuken_cd IN ( ")
                For i As Integer = 0 To arrKbn.Length - 1
                    If i = 0 Then
                        .AppendLine("     @todoufuken" & i & "   ")
                    Else
                        .AppendLine("     ,@todoufuken" & i & "   ")
                    End If
                    paramList.Add(MakeParam("@todoufuken" & i, SqlDbType.VarChar, 2, arrKbn(i)))
                Next
                .AppendLine("       ) ")
            End If

            'nñR[h

            If dtParam.Rows(0).Item("KeiretuCd").ToString <> String.Empty Then
                .AppendLine(" AND m_kameiten.keiretu_cd = @KeiretuCd  ")
                paramList.Add(MakeParam("@KeiretuCd", SqlDbType.VarChar, 5, dtParam.Rows(0).Item("KeiretuCd")))
            End If
            'cÆR[h
            If dtParam.Rows(0).Item("EigyousyoCd").ToString <> String.Empty Then
                .AppendLine(" AND m_kameiten.eigyousyo_cd = @EigyousyoCd  ")
                paramList.Add(MakeParam("@EigyousyoCd", SqlDbType.VarChar, 5, dtParam.Rows(0).Item("EigyousyoCd")))
            End If


            'ScÆID
            If dtParam.Rows(0).Item("TantouEigyouID").ToString <> String.Empty Then
                .AppendLine(" AND m_kameiten.eigyou_tantousya_mei = @TantouEigyouID  ")
                paramList.Add(MakeParam("@TantouEigyouID", SqlDbType.VarChar, 64, dtParam.Rows(0).Item("TantouEigyouID")))
            End If

        End With


        'õÀs()
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsBukkenJyouhouDataSet, _
                    dsBukkenJyouhouDataSet.BukkenJyouhouCountTable.TableName, paramList.ToArray)

        Return dsBukkenJyouhouDataSet.BukkenJyouhouCountTable(0).hosyousyo_no_count

    End Function

    ''' <summary> CSVîñæ¾</summary>
    ''' <param name="dtParam">Paramf[^Z[g</param>
    ''' <returns>CSVîñÌf[^</returns>
    Public Function SelBukkenJyouhouInfoCSV(ByVal dtParam As DataTable) As BukkenJyouhouDataSet.BukkenJyouhouCSVTableDataTable

        ' DataSetCX^XÌ¶¬()
        Dim dsBukkenJyouhouDataSet As New BukkenJyouhouDataSet

        'SQL¶Ì¶¬
        Dim commandTextSb1 As New StringBuilder
        Dim commandTextSb2 As New StringBuilder
        Dim commandTextSbMAIN As New StringBuilder
        Dim commandTextSb As New StringBuilder
        'p[^i[
        Dim paramList As New List(Of SqlClient.SqlParameter)
        Dim strFrom As String = ""
        Dim strTo As String = ""
        Dim arrKbn() As String
        'SQL¶
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	MDH.haki_syubetu, ") 'jüíÊ
            .AppendLine("	TJ.kbn, ") 'æª
            .AppendLine("	TJ.hosyousyo_no, ") 'Ô
            .AppendLine("	TJ.sesyu_mei, ") '{å¼
            .AppendLine("	TJ.irai_date, ") 'Ëú
            .AppendLine("	TJ.kameiten_cd, ") 'Á¿XR[h
            .AppendLine("	MK.kameiten_mei1, ") 'Á¿X¼
            '========================================405672 ADD
            .AppendLine("	TJ.tys_renrakusaki_atesaki_mei, ") '²¸Aæ
            .AppendLine("	TJ.bukken_nayose_cd, ") '¨¼ñR[h
            '========================================405672 ADD
            .AppendLine("	TJ.irai_tantousya_mei, ") 'ËSÒ

            '========================================405672 DEL
            '.AppendLine("	MK.todouhuken_cd, ") 's¹{§R[h
            '========================================405672 DEL
            .AppendLine("	MT.todouhuken_mei, ") 's¹{§¼
            .AppendLine("	MK.eigyou_tantousya_mei, ") 'ScÆID
            .AppendLine("	TJ.bukken_jyuusyo1, ") '¨Z1
            .AppendLine("	TJ.bukken_jyuusyo2, ") '¨Z2
            .AppendLine("	TJ.bukken_jyuusyo3, ") '¨Z3
            .AppendLine("	TJ.bikou, ") 'õl
            '========================================405672 ADD
            .AppendLine("	TJ.tys_kaisya_cd, ") '²¸ïÐR[h
            .AppendLine("	TJ.tys_kaisya_jigyousyo_cd, ") '²¸ïÐÆR[h
            '========================================405672 ADD
            .AppendLine("	MTK.tys_kaisya_mei, ") '²¸ïÐ¼
            .AppendLine("	MTH.tys_houhou_mei, ") '²¸û@¼
            .AppendLine("	TJ.tys_kibou_date, ") '²¸ó]ú
            .AppendLine("	TJ.tys_kibou_jikan, ") '²¸ó]Ô
            .AppendLine("	TJ.syoudakusyo_tys_date, ") '³ø²¸ú
            .AppendLine("	TJ.tys_jissi_date, ") '²¸À{ú
            .AppendLine("	ks_siyou = ")
            .AppendLine("	CASE WHEN (RI.status >= '700') OR (TJ.keiyu = '9') THEN ")
            .AppendLine("		CASE WHEN TJ.hantei_cd2 IS NOT NULL THEN MKS.ks_siyou + '¼' ")
            .AppendLine("		ELSE MKS.ks_siyou ")
            .AppendLine("		END ")
            .AppendLine("	ELSE NULL ")
            .AppendLine("	END, ") 'îbdl
            .AppendLine("	TJ.keikakusyo_sakusei_date, ") 'væì¬ú
            '========================================405672 ADD@«
            .AppendLine("	TJ.hosyousyo_hak_iraisyo_tyk_date, ") 'ÛØ­sËú
            .AppendLine("	MHHJ.hosyousyo_hak_jyky, ") 'ÛØ­sóµ
            '========================================405672 ADD@ª

            .AppendLine("	TJ.hosyousyo_hak_date, ") 'ÛØ­sú

            '========================================405672 ADD@«
            .AppendLine("	TJ.koj_gaisya_cd, ") 'HïÐR[h
            .AppendLine("	TJ.koj_gaisya_jigyousyo_cd, ") 'HïÐÆR[h
            '========================================405672 ADD@ª

            .AppendLine("	MTK2.tys_kaisya_mei AS koj_gaisya_mei, ") 'HïÐ¼

            '========================================405672 ADD «
            .AppendLine("	TJ.t_koj_kaisya_cd, ") 'ÇÁHïÐR[h
            .AppendLine("	TJ.t_koj_kaisya_jigyousyo_cd, ") 'ÇÁHïÐÆR[h
            .AppendLine("	MTK3.tys_kaisya_mei AS koj_tys_kaisya_mei, ") 'ÇÁHïÐ¼
            .AppendLine("	TJ.koj_siyou_kakunin, ") 'dlmFmFL³
            .AppendLine("	TJ.koj_siyou_kakunin_date, ") 'HdlmFú
            '========================================405672 ADD@ª

            .AppendLine("	TJ.kairy_koj_kanry_yotei_date, ") 'üÇH®¹\èú
            .AppendLine("	TJ.kairy_koj_date, ") 'üÇHú
            .AppendLine("	TJ.t_koj_kanry_yotei_date, ") 'ÇÁH\èú
            .AppendLine("	TJ.t_koj_date, ") 'ÇÁHÀ{ú
            .AppendLine("	TTS.uri_date, ") '²¸ãNú
            .AppendLine("	TBL2.uri_gaku, ") '²¸ãàzi1+2-ðñj
            .AppendLine("	TTS.seikyuusyo_hak_date, ") '²¸¿­sú
            '========================================405672 ADD «
            '.AppendLine("	TBL3.nyuukin_gaku, ") '²¸üààz
            .AppendLine("	TBL6.nyuukin_gaku, ") '²¸üààz
            .AppendLine("	TBL2.hattyuusyo_gaku, ") '²¸­àz
            .AppendLine("	TTS.hattyuusyo_kakunin_date, ") '²¸­mFú
            '========================================405672 ADD@ª
            .AppendLine("	TTS2.uri_date AS kairyou_uri_date, ") 'üÇHãNú
            .AppendLine("	TTS2.uri_gaku AS kairyou_uri_gaku, ") 'üÇHãàz
            .AppendLine("	TTS2.seikyuusyo_hak_date AS kairyou_seikyuusyo_hak_date, ") 'üÇH¿ú
            .AppendLine("	TBL4.koj_nyuukin_gaku, ") 'Hüààz
            '========================================405672 ADD «
            .AppendLine("	TTS2.hattyuusyo_gaku AS hattyuusyo_gaku1 , ") 'H­àz
            .AppendLine("	TTS2.hattyuusyo_kakunin_date AS hattyuusyo_kakunin_date1 , ") 'H­mFú
            '========================================405672 ADD@ª
            .AppendLine("	TTS3.uri_date AS tuika_uri_date, ") 'ÇÁHãNú
            .AppendLine("	TTS3.uri_gaku AS tuika_uri_gaku, ") 'ÇÁHãàz
            .AppendLine("	TTS3.seikyuusyo_hak_date AS tuika_seikyuusyo_hak_date, ") 'ÇÁH¿ú
            .AppendLine("	TBL5.tuika_koj_nyuukin_gaku, ") 'ÇÁHüààz
            '========================================405672 ADD «
            .AppendLine("	TTS3.hattyuusyo_gaku AS hattyuusyo_gaku2, ") 'ÇÁHüààz
            .AppendLine("	TTS3.hattyuusyo_kakunin_date AS hattyuusyo_kakunin_date2 ") 'ÇÁHüààz
            '========================================405672 ADD@ª
            .AppendLine("FROM ")
            .AppendLine("	t_jiban TJ WITH (READCOMMITTED) ") 'nÕ}X^
            .AppendLine("LEFT JOIN ")
            .AppendLine("	m_data_haki MDH WITH (READCOMMITTED) ") 'f[^jü}X^
            .AppendLine("ON ")
            .AppendLine("	TJ.data_haki_syubetu = MDH.data_haki_no ")
            .AppendLine("LEFT JOIN ")
            .AppendLine("	m_kameiten MK WITH (READCOMMITTED) ") 'Á¿X}X^
            .AppendLine("ON ")
            .AppendLine("	TJ.kameiten_cd = MK.kameiten_cd ")
            .AppendLine("LEFT JOIN ")
            .AppendLine("	m_todoufuken MT WITH (READCOMMITTED)  ") 's¹{§}X^
            .AppendLine("ON ")
            .AppendLine("	MK.todouhuken_cd = MT.todouhuken_cd ")
            .AppendLine("LEFT JOIN ")
            .AppendLine("	m_tyousakaisya MTK WITH (READCOMMITTED) ") '²¸ïÐ}X^
            .AppendLine("ON ")
            .AppendLine("	TJ.tys_kaisya_cd = MTK.tys_kaisya_cd ")
            .AppendLine("AND ")
            .AppendLine("	TJ.tys_kaisya_jigyousyo_cd = MTK.jigyousyo_cd ")
            .AppendLine("LEFT JOIN ")
            .AppendLine("	m_tyousakaisya MTK2 WITH (READCOMMITTED) ") '²¸ïÐ}X^
            .AppendLine("ON ")
            .AppendLine("	TJ.koj_gaisya_cd = MTK2.tys_kaisya_cd ")
            .AppendLine("AND ")
            .AppendLine("	TJ.koj_gaisya_jigyousyo_cd = MTK2.jigyousyo_cd ")
            .AppendLine("LEFT JOIN ")
            .AppendLine("	m_tyousahouhou MTH WITH (READCOMMITTED) ") '²¸û@}X^
            .AppendLine("ON ")
            .AppendLine("	TJ.tys_houhou = MTH.tys_houhou_no ")
            .AppendLine("LEFT JOIN ")
            .AppendLine("	m_ks_siyou MKS WITH (READCOMMITTED) ") 'îbdl}X^
            .AppendLine("ON ")
            .AppendLine("	TJ.hantei_cd1 = MKS.ks_siyou_no ")
            .AppendLine("LEFT JOIN ")
            .AppendLine("	ReportIF RI WITH (READCOMMITTED) ") 'i»f[^
            .AppendLine("ON ")
            '.AppendLine("	TJ.kbn = SUBSTRING(RI.kokyaku_no,1,1) ")
            '.AppendLine("AND ")
            '.AppendLine("	TJ.hosyousyo_no = SUBSTRING(RI.kokyaku_no,2,10) ")

            .AppendLine("	TJ.kbn+TJ.hosyousyo_no = RI.kokyaku_no ")

            .AppendLine("LEFT JOIN ")
            .AppendLine("   t_teibetu_seikyuu TTS WITH (READCOMMITTED) ") '@Ê¿e[u
            .AppendLine("ON ")
            .AppendLine("   TJ.hosyousyo_no = TTS.hosyousyo_no")
            .AppendLine("AND ")
            .AppendLine("   TJ.kbn = TTS.kbn ")
            .AppendLine("AND ")
            .AppendLine("   TTS.bunrui_cd = '100'")

            .AppendLine("LEFT JOIN ")
            .AppendLine("	(SELECT ")
            .AppendLine("		TTS1.kbn, ")
            .AppendLine("		TTS1.hosyousyo_no, ")
            .AppendLine("		FLOOR(SUM(ISNULL(TTS1.uri_gaku,0))) AS uri_gaku, ") '²¸ãàzi1+2-ðñj
            .AppendLine("		FLOOR(SUM(ISNULL(TTS1.hattyuusyo_gaku,0))) AS hattyuusyo_gaku ") '²¸­àz
            .AppendLine("	FROM ")
            '.AppendLine("		t_jiban TJ2 WITH (READCOMMITTED) ") 'nÕe[u
            '.AppendLine("	LEFT JOIN ")
            .AppendLine("		t_teibetu_seikyuu TTS1 WITH (READCOMMITTED) ") '@Ê¿e[u
            '.AppendLine("	ON ")
            '.AppendLine("		TJ2.hosyousyo_no = TTS1.hosyousyo_no ")
            '.AppendLine("	AND ")
            '.AppendLine("		TJ2.kbn = TTS1.kbn ")
            .AppendLine("	WHERE ")
            .AppendLine("		(TTS1.bunrui_cd = '100' OR TTS1.bunrui_cd = '110'  OR TTS1.bunrui_cd = '115'  OR TTS1.bunrui_cd = '180') ")
            .AppendLine("	GROUP BY ")
            .AppendLine("		TTS1.kbn, ")
            .AppendLine("		TTS1.hosyousyo_no) TBL2 ")
            .AppendLine("ON  ")
            .AppendLine("	TJ.hosyousyo_no = TBL2.hosyousyo_no ")
            .AppendLine("AND ")
            .AppendLine("	TJ.kbn = TBL2.kbn ")
            '.AppendLine("LEFT JOIN ")
            '.AppendLine("	(SELECT ")
            '.AppendLine("		TJ3.kbn, ")
            '.AppendLine("		TJ3.hosyousyo_no, ")
            '.AppendLine("		FLOOR(ISNULL(TTN.zeikomi_nyuukin_gaku,0)-ISNULL(TTN.zeikomi_henkin_gaku,0)) AS nyuukin_gaku ") '²¸üààz
            '.AppendLine("	FROM ")
            '.AppendLine("		t_jiban TJ3 WITH (READCOMMITTED) ") 'nÕe[u
            '.AppendLine("	LEFT JOIN ")
            '.AppendLine("		t_teibetu_nyuukin TTN WITH (READCOMMITTED) ") '@Êüàe[u
            '.AppendLine("	ON ")
            '.AppendLine("		TJ3.hosyousyo_no = TTN.hosyousyo_no ")
            '.AppendLine("	AND ")
            '.AppendLine("		TJ3.kbn = TTN.kbn ")
            '.AppendLine("	WHERE ")
            '.AppendLine("		TTN.bunrui_cd = '100') TBL3 ")
            '.AppendLine("ON  ")
            '.AppendLine("	TJ.hosyousyo_no = TBL3.hosyousyo_no ")
            '.AppendLine("AND ")
            '.AppendLine("	TJ.kbn = TBL3.kbn ")
            .AppendLine("LEFT JOIN ")
            .AppendLine("	(SELECT ")
            .AppendLine("		TTN1.kbn, ")
            .AppendLine("		TTN1.hosyousyo_no, ")
            .AppendLine("		FLOOR(ISNULL(TTN1.zeikomi_nyuukin_gaku,0)-ISNULL(TTN1.zeikomi_henkin_gaku,0)) AS koj_nyuukin_gaku ") 'Hüààz
            .AppendLine("	FROM ")
            '.AppendLine("		t_jiban TJ4 WITH (READCOMMITTED) ") 'nÕe[u
            '.AppendLine("	LEFT JOIN ")
            .AppendLine("		t_teibetu_nyuukin TTN1 WITH (READCOMMITTED) ") '@Êüàe[u
            '.AppendLine("	ON ")
            '.AppendLine("		TJ4.hosyousyo_no = TTN1.hosyousyo_no ")
            '.AppendLine("	AND ")
            '.AppendLine("		TJ4.kbn = TTN1.kbn ")
            .AppendLine("	WHERE ")
            .AppendLine("		TTN1.bunrui_cd = '130') TBL4 ")
            .AppendLine("ON  ")
            .AppendLine("	TJ.hosyousyo_no = TBL4.hosyousyo_no ")
            .AppendLine("AND ")
            .AppendLine("	TJ.kbn = TBL4.kbn ")
            .AppendLine("LEFT JOIN ")
            .AppendLine("	(SELECT ")
            .AppendLine("		TTN2.kbn, ")
            .AppendLine("		TTN2.hosyousyo_no, ")
            .AppendLine("		FLOOR(ISNULL(TTN2.zeikomi_nyuukin_gaku,0)-ISNULL(TTN2.zeikomi_henkin_gaku,0)) AS tuika_koj_nyuukin_gaku ") 'ÇÁHüààz
            .AppendLine("	FROM ")
            '.AppendLine("		t_jiban TJ5 WITH (READCOMMITTED) ") 'nÕe[u
            '.AppendLine("	LEFT JOIN ")
            .AppendLine("		t_teibetu_nyuukin TTN2 WITH (READCOMMITTED) ") '@Êüàe[u
            '.AppendLine("	ON ")
            '.AppendLine("		TJ5.hosyousyo_no = TTN2.hosyousyo_no ")
            '.AppendLine("	AND ")
            '.AppendLine("		TJ5.kbn = TTN2.kbn ")
            .AppendLine("	WHERE ")
            .AppendLine("		TTN2.bunrui_cd = '140') TBL5 ")
            .AppendLine("ON  ")
            .AppendLine("	TJ.hosyousyo_no = TBL5.hosyousyo_no ")
            .AppendLine("AND ")
            .AppendLine("	TJ.kbn = TBL5.kbn ")
            .AppendLine("LEFT JOIN ")
            .AppendLine("   t_teibetu_seikyuu TTS2 WITH (READCOMMITTED) ") '@Ê¿e[u
            .AppendLine("ON ")
            .AppendLine("   TJ.hosyousyo_no = TTS2.hosyousyo_no ")
            .AppendLine("AND ")
            .AppendLine("   TJ.kbn = TTS2.kbn ")
            .AppendLine("AND ")
            .AppendLine("  TTS2.bunrui_cd = '130'")

            .AppendLine("LEFT JOIN ")
            .AppendLine("   t_teibetu_seikyuu TTS3 WITH (READCOMMITTED) ") '@Ê¿e[u
            .AppendLine("ON ")
            .AppendLine("   TJ.hosyousyo_no = TTS3.hosyousyo_no ")
            .AppendLine("AND ")
            .AppendLine("   TJ.kbn = TTS3.kbn ")
            .AppendLine("AND ")
            .AppendLine("  TTS3.bunrui_cd = '140'")

            .AppendLine("LEFT JOIN  ")
            .AppendLine("	m_busyo_kanri MBK WITH (READCOMMITTED) ") 'Ç}X^
            .AppendLine("ON ")
            .AppendLine("	MBK.busyo_cd=MT.busyo_cd ")

            .AppendLine("LEFT JOIN  ")
            .AppendLine("	m_hosyousyo_hak_jyky MHHJ WITH (READCOMMITTED) ")
            .AppendLine("ON ")
            .AppendLine("	MHHJ.hosyousyo_hak_jyky_no=TJ.hosyousyo_hak_jyky ")

            .AppendLine("LEFT JOIN ")
            .AppendLine("	m_tyousakaisya MTK3 WITH (READCOMMITTED) ") '²¸ïÐ}X^
            .AppendLine("ON ")
            .AppendLine("	TJ.t_koj_kaisya_cd = MTK3.tys_kaisya_cd ")
            .AppendLine("AND ")
            .AppendLine("	TJ.t_koj_kaisya_jigyousyo_cd = MTK3.jigyousyo_cd ")

            .AppendLine("LEFT JOIN ")
            .AppendLine("	(SELECT ")
            .AppendLine("		TTN3.kbn, ")
            .AppendLine("		TTN3.hosyousyo_no, ")
            .AppendLine("		FLOOR(SUM(ISNULL(TTN3.zeikomi_nyuukin_gaku,0)-ISNULL(TTN3.zeikomi_henkin_gaku,0))) AS nyuukin_gaku ") '²¸üààz
            .AppendLine("	FROM ")
            '.AppendLine("		t_jiban TJ6 WITH (READCOMMITTED) ") 'nÕe[u
            '.AppendLine("	LEFT JOIN ")
            .AppendLine("		t_teibetu_nyuukin TTN3 WITH (READCOMMITTED) ") '@Êüàe[u
            '.AppendLine("	ON ")
            '.AppendLine("		TJ6.hosyousyo_no = TTN3.hosyousyo_no ")
            '.AppendLine("	AND ")
            '.AppendLine("		TJ6.kbn = TTN3.kbn ")
            .AppendLine("	WHERE ")
            .AppendLine("		(TTN3.bunrui_cd = '100' OR TTN3.bunrui_cd = '110'  OR TTN3.bunrui_cd = '115'  OR TTN3.bunrui_cd = '180') ")
            .AppendLine("	GROUP BY ")
            .AppendLine("		TTN3.kbn, ")
            .AppendLine("		TTN3.hosyousyo_no) TBL6 ")
            .AppendLine("ON  ")
            .AppendLine("	TJ.hosyousyo_no = TBL6.hosyousyo_no ")
            .AppendLine("AND ")
            .AppendLine("	TJ.kbn = TBL6.kbn ")

            .AppendLine(" WHERE TJ.kbn IS NOT NULL ")
            If dtParam.Rows(0).Item("SosikiLevel").ToString <> String.Empty Then
                If dtParam.Rows(0).Item("eigyouManKbn").ToString = "1" Then
                    .AppendLine(" AND MBK.sosiki_level = @sosiki_level2 ")
                    paramList.Add(MakeParam("@sosiki_level2", SqlDbType.VarChar, 2, dtParam.Rows(0).Item("SosikiLevel")))
                Else
                    .AppendLine(" AND MBK.sosiki_level >= @sosiki_level2 ")
                    paramList.Add(MakeParam("@sosiki_level2", SqlDbType.VarChar, 2, dtParam.Rows(0).Item("SosikiLevel")))
                End If
            End If
            If dtParam.Rows(0).Item("eigyouManKbn").ToString = "1" Then
                .AppendLine(" AND  MT.busyo_cd IN ( ")
                .AppendLine(" @busyo_cd2 ")
                If dtParam.Rows(0).Item("SosikiLevel").ToString = String.Empty Then
                    paramList.Add(MakeParam("@busyo_cd2", SqlDbType.VarChar, 4, dtParam.Rows(0).Item("busyo_cd")))
                Else
                    paramList.Add(MakeParam("@busyo_cd2", SqlDbType.VarChar, 4, dtParam.Rows(0).Item("BusyoCd")))
                End If
                .AppendLine(" ) ")
            Else
                If dtParam.Rows(0).Item("BusyoCd").ToString = "0000" Then
                    If dtParam.Rows(0).Item("CHKBusyoCd") Then
                        .AppendLine(" AND  MT.busyo_cd IN ( ")
                        .AppendLine(" @busyo_cd2 ")
                        .AppendLine(" ) ")
                        paramList.Add(MakeParam("@busyo_cd2", SqlDbType.VarChar, 4, dtParam.Rows(0).Item("BusyoCd")))
                    End If
                Else
                    If dtParam.Rows(0).Item("BusyoCd").ToString <> String.Empty Then
                        .AppendLine(" AND  MT.busyo_cd IN ( ")
                        .AppendLine("SELECT busyo_cd FROM  ")
                        .AppendLine("(SELECT a6.busyo_cd, ")
                        .AppendLine("ISNULL(a1.busyo_cd,'0000') ")
                        .AppendLine("+ISNULL(a2.busyo_cd,'0000') ")
                        .AppendLine("+ISNULL(a3.busyo_cd,'0000') ")
                        .AppendLine("+ISNULL(a4.busyo_cd,'0000') ")
                        .AppendLine("+ISNULL(a5.busyo_cd,'0000') ")
                        .AppendLine("+ISNULL(a6.busyo_cd,'0000') AS cd ")
                        .AppendLine("FROM m_busyo_kanri a6 ")
                        .AppendLine("LEFT JOIN m_busyo_kanri a5 ")
                        .AppendLine("ON a6.joui_soiki = a5.busyo_cd ")
                        .AppendLine("LEFT JOIN m_busyo_kanri a4 ")
                        .AppendLine("ON a5.joui_soiki = a4.busyo_cd ")
                        .AppendLine("LEFT JOIN m_busyo_kanri a3 ")
                        .AppendLine("ON a4.joui_soiki = a3.busyo_cd ")
                        .AppendLine("LEFT JOIN m_busyo_kanri a2 ")
                        .AppendLine("ON a3.joui_soiki = a2.busyo_cd ")
                        .AppendLine("LEFT JOIN m_busyo_kanri a1 ")
                        .AppendLine("ON a2.joui_soiki = a1.busyo_cd) AS TB ")
                        .AppendLine("WHERE   (")
                        If dtParam.Rows(0).Item("CHKBusyoCd") Then

                            .AppendLine("  MT.busyo_cd = @busyo_cd ")
                            paramList.Add(MakeParam("@busyo_cd", SqlDbType.VarChar, 4, dtParam.Rows(0).Item("BusyoCd")))
                        Else
                            .AppendLine("SUBSTRING(cd,1,4)=@busyo_cd2 OR  ")
                            .AppendLine("SUBSTRING(cd,5,4)=@busyo_cd2 OR  ")
                            .AppendLine("SUBSTRING(cd,9,4)=@busyo_cd2 OR  ")
                            .AppendLine("SUBSTRING(cd,13,4)=@busyo_cd2 OR  ")
                            .AppendLine("SUBSTRING(cd,17,4)=@busyo_cd2 OR  ")
                            .AppendLine("SUBSTRING(cd,21,4)=@busyo_cd2   ")
                            paramList.Add(MakeParam("@busyo_cd2", SqlDbType.VarChar, 4, dtParam.Rows(0).Item("BusyoCd")))
                        End If
                        .AppendLine(" ) ")
                        .AppendLine(" ) ")
                    Else
                        If dtParam.Rows(0).Item("busyo_cd").ToString = "0000" Or dtParam.Rows(0).Item("t_sansyou_busyo_cd").ToString = "0000" Then
                        Else
                            .AppendLine(" AND  MT.busyo_cd IN ( ")
                            .AppendLine("SELECT busyo_cd FROM  ")
                            .AppendLine("(SELECT a6.busyo_cd, ")
                            .AppendLine("ISNULL(a1.busyo_cd,'0000') ")
                            .AppendLine("+ISNULL(a2.busyo_cd,'0000') ")
                            .AppendLine("+ISNULL(a3.busyo_cd,'0000') ")
                            .AppendLine("+ISNULL(a4.busyo_cd,'0000') ")
                            .AppendLine("+ISNULL(a5.busyo_cd,'0000') ")
                            .AppendLine("+ISNULL(a6.busyo_cd,'0000') AS cd ")
                            .AppendLine("FROM m_busyo_kanri a6 ")
                            .AppendLine("LEFT JOIN m_busyo_kanri a5 ")
                            .AppendLine("ON a6.joui_soiki = a5.busyo_cd ")
                            .AppendLine("LEFT JOIN m_busyo_kanri a4 ")
                            .AppendLine("ON a5.joui_soiki = a4.busyo_cd ")
                            .AppendLine("LEFT JOIN m_busyo_kanri a3 ")
                            .AppendLine("ON a4.joui_soiki = a3.busyo_cd ")
                            .AppendLine("LEFT JOIN m_busyo_kanri a2 ")
                            .AppendLine("ON a3.joui_soiki = a2.busyo_cd ")
                            .AppendLine("LEFT JOIN m_busyo_kanri a1 ")
                            .AppendLine("ON a2.joui_soiki = a1.busyo_cd) AS TB ")
                            .AppendLine("WHERE   (")
                            Dim intCHK As Integer = 0
                            If dtParam.Rows(0).Item("busyo_cd").ToString <> "" Then
                                .AppendLine("SUBSTRING(cd,1,4)=@busyo_cd2 OR  ")
                                .AppendLine("SUBSTRING(cd,5,4)=@busyo_cd2 OR  ")
                                .AppendLine("SUBSTRING(cd,9,4)=@busyo_cd2 OR  ")
                                .AppendLine("SUBSTRING(cd,13,4)=@busyo_cd2 OR  ")
                                .AppendLine("SUBSTRING(cd,17,4)=@busyo_cd2 OR  ")
                                .AppendLine("SUBSTRING(cd,21,4)=@busyo_cd2   ")
                                paramList.Add(MakeParam("@busyo_cd2", SqlDbType.VarChar, 4, dtParam.Rows(0).Item("busyo_cd")))
                                intCHK = intCHK + 1
                            End If
                            If dtParam.Rows(0).Item("t_sansyou_busyo_cd").ToString <> "" Then
                                If intCHK = 1 Then
                                    .AppendLine(" OR ")
                                End If
                                .AppendLine(" SUBSTRING(cd,1,4)=@busyo_cd3 OR  ")
                                .AppendLine("SUBSTRING(cd,5,4)=@busyo_cd3 OR  ")
                                .AppendLine("SUBSTRING(cd,9,4)=@busyo_cd3 OR  ")
                                .AppendLine("SUBSTRING(cd,13,4)=@busyo_cd3 OR  ")
                                .AppendLine("SUBSTRING(cd,17,4)=@busyo_cd3 OR  ")
                                .AppendLine("SUBSTRING(cd,21,4)=@busyo_cd3   ")
                                paramList.Add(MakeParam("@busyo_cd3", SqlDbType.VarChar, 4, dtParam.Rows(0).Item("t_sansyou_busyo_cd")))
                            End If
                            .AppendLine(" ) ")
                            .AppendLine(" ) ")
                        End If
                    End If
                End If
            End If

            'æª
            If dtParam.Rows(0).Item("kbn").ToString <> String.Empty Then
                arrKbn = dtParam.Rows(0).Item("kbn").ToString.Split(",")
                .AppendLine(" AND TJ.kbn IN ( ")
                For i As Integer = 0 To arrKbn.Length - 1
                    If i = 0 Then
                        .AppendLine("     @kbn" & i & "   ")
                    Else
                        .AppendLine("     ,@kbn" & i & "   ")
                    End If
                    paramList.Add(MakeParam("@kbn" & i, SqlDbType.Char, 1, arrKbn(i)))
                Next
                .AppendLine("       ) ")
            End If

            'Ô
            If dtParam.Rows(0).Item("Bangou").ToString <> String.Empty Then
                strFrom = ""
                strTo = ""
                strFrom = Split(dtParam.Rows(0).Item("Bangou").ToString, ",")(0)
                strTo = Split(dtParam.Rows(0).Item("Bangou").ToString, ",")(1)
                If strFrom <> String.Empty And strTo <> String.Empty Then
                    .AppendLine(" AND TJ.hosyousyo_no BETWEEN @hosyousyo_no_from AND @hosyousyo_no_to ")
                    paramList.Add(MakeParam("@hosyousyo_no_from", SqlDbType.VarChar, 10, strFrom))
                    paramList.Add(MakeParam("@hosyousyo_no_to", SqlDbType.VarChar, 10, strTo))
                ElseIf strFrom <> String.Empty And strTo = String.Empty Then
                    .AppendLine(" AND TJ.hosyousyo_no = @hosyousyo_no_from ")
                    paramList.Add(MakeParam("@hosyousyo_no_from", SqlDbType.VarChar, 10, strFrom))
                End If
            End If

            'Ëú
            If dtParam.Rows(0).Item("Irai").ToString <> String.Empty Then
                strFrom = ""
                strTo = ""
                strFrom = Split(dtParam.Rows(0).Item("Irai").ToString, ",")(0)
                strTo = Split(dtParam.Rows(0).Item("Irai").ToString, ",")(1)
                If strFrom <> String.Empty And strTo <> String.Empty Then
                    .AppendLine(" AND TJ.irai_date BETWEEN @Irai_from AND @Irai_to ")
                    paramList.Add(MakeParam("@Irai_from", SqlDbType.VarChar, 10, strFrom))
                    paramList.Add(MakeParam("@Irai_to", SqlDbType.VarChar, 10, strTo))
                ElseIf strFrom <> String.Empty And strTo = String.Empty Then
                    .AppendLine(" AND TJ.irai_date = @Irai_from ")
                    paramList.Add(MakeParam("@Irai_from", SqlDbType.VarChar, 10, strFrom))
                End If
            End If

            'væ
            If dtParam.Rows(0).Item("Keikakusyo").ToString <> String.Empty Then
                strFrom = ""
                strTo = ""
                strFrom = Split(dtParam.Rows(0).Item("Keikakusyo").ToString, ",")(0)
                strTo = Split(dtParam.Rows(0).Item("Keikakusyo").ToString, ",")(1)
                If strFrom <> String.Empty And strTo <> String.Empty Then
                    .AppendLine(" AND TJ.keikakusyo_sakusei_date BETWEEN @Keikakusyo_from AND @Keikakusyo_to ")
                    paramList.Add(MakeParam("@Keikakusyo_from", SqlDbType.VarChar, 10, strFrom))
                    paramList.Add(MakeParam("@Keikakusyo_to", SqlDbType.VarChar, 10, strTo))
                ElseIf strFrom <> String.Empty And strTo = String.Empty Then
                    .AppendLine(" AND TJ.keikakusyo_sakusei_date = @Keikakusyo_from ")
                    paramList.Add(MakeParam("@Keikakusyo_from", SqlDbType.VarChar, 10, strFrom))
                End If
            End If

            '²¸\è
            If dtParam.Rows(0).Item("TyousaYotei").ToString <> String.Empty Then
                strFrom = ""
                strTo = ""
                strFrom = Split(dtParam.Rows(0).Item("TyousaYotei").ToString, ",")(0)
                strTo = Split(dtParam.Rows(0).Item("TyousaYotei").ToString, ",")(1)
                If strFrom <> String.Empty And strTo <> String.Empty Then
                    .AppendLine(" AND TJ.syoudakusyo_tys_date BETWEEN @TyousaYotei_from AND @TyousaYotei_to ")
                    paramList.Add(MakeParam("@TyousaYotei_from", SqlDbType.VarChar, 10, strFrom))
                    paramList.Add(MakeParam("@TyousaYotei_to", SqlDbType.VarChar, 10, strTo))
                ElseIf strFrom <> String.Empty And strTo = String.Empty Then
                    .AppendLine(" AND TJ.syoudakusyo_tys_date = @TyousaYotei_from ")
                    paramList.Add(MakeParam("@TyousaYotei_from", SqlDbType.VarChar, 10, strFrom))
                End If
            End If

            '²¸À{
            If dtParam.Rows(0).Item("TyousaJissi").ToString <> String.Empty Then
                strFrom = ""
                strTo = ""
                strFrom = Split(dtParam.Rows(0).Item("TyousaJissi").ToString, ",")(0)
                strTo = Split(dtParam.Rows(0).Item("TyousaJissi").ToString, ",")(1)
                If strFrom <> String.Empty And strTo <> String.Empty Then
                    .AppendLine(" AND TJ.tys_jissi_date BETWEEN @TyousaJissi_from AND @TyousaJissi_to ")
                    paramList.Add(MakeParam("@TyousaJissi_from", SqlDbType.VarChar, 10, strFrom))
                    paramList.Add(MakeParam("@TyousaJissi_to", SqlDbType.VarChar, 10, strTo))
                ElseIf strFrom <> String.Empty And strTo = String.Empty Then
                    .AppendLine(" AND TJ.tys_jissi_date = @TyousaJissi_from ")
                    paramList.Add(MakeParam("@TyousaJissi_from", SqlDbType.VarChar, 10, strFrom))
                End If
            End If

            '²¸ã
            If dtParam.Rows(0).Item("TyousaUriage").ToString <> String.Empty Then
                strFrom = ""
                strTo = ""
                strFrom = Split(dtParam.Rows(0).Item("TyousaUriage").ToString, ",")(0)
                strTo = Split(dtParam.Rows(0).Item("TyousaUriage").ToString, ",")(1)
                If strFrom <> String.Empty And strTo <> String.Empty Then
                    .AppendLine(" AND TTS.uri_date BETWEEN @TyousaUriage_from AND @TyousaUriage_to ")
                    paramList.Add(MakeParam("@TyousaUriage_from", SqlDbType.VarChar, 10, strFrom))
                    paramList.Add(MakeParam("@TyousaUriage_to", SqlDbType.VarChar, 10, strTo))
                ElseIf strFrom <> String.Empty And strTo = String.Empty Then
                    .AppendLine(" AND TTS.uri_date = @TyousaUriage_from ")
                    paramList.Add(MakeParam("@TyousaUriage_from", SqlDbType.VarChar, 10, strFrom))
                End If
            End If

            'H\è
            If dtParam.Rows(0).Item("KoujiYotei").ToString <> String.Empty Then
                strFrom = ""
                strTo = ""
                strFrom = Split(dtParam.Rows(0).Item("KoujiYotei").ToString, ",")(0)
                strTo = Split(dtParam.Rows(0).Item("KoujiYotei").ToString, ",")(1)
                If strFrom <> String.Empty And strTo <> String.Empty Then
                    .AppendLine(" AND (TJ.kairy_koj_kanry_yotei_date BETWEEN @KoujiYotei_from AND @KoujiYotei_to ")
                    .AppendLine(" OR TJ.t_koj_kanry_yotei_date BETWEEN @KoujiYotei_from AND @KoujiYotei_to ) ")
                    paramList.Add(MakeParam("@KoujiYotei_from", SqlDbType.VarChar, 10, strFrom))
                    paramList.Add(MakeParam("@KoujiYotei_to", SqlDbType.VarChar, 10, strTo))
                ElseIf strFrom <> String.Empty And strTo = String.Empty Then
                    .AppendLine(" AND (TJ.kairy_koj_kanry_yotei_date = @KoujiYotei_from OR TJ.t_koj_kanry_yotei_date = @KoujiYotei_from)")
                    paramList.Add(MakeParam("@KoujiYotei_from", SqlDbType.VarChar, 10, strFrom))
                End If
            End If

            'HÀ{
            If dtParam.Rows(0).Item("KoujiJissi").ToString <> String.Empty Then
                strFrom = ""
                strTo = ""
                strFrom = Split(dtParam.Rows(0).Item("KoujiJissi").ToString, ",")(0)
                strTo = Split(dtParam.Rows(0).Item("KoujiJissi").ToString, ",")(1)
                If strFrom <> String.Empty And strTo <> String.Empty Then
                    .AppendLine(" AND (TJ.kairy_koj_date BETWEEN @KoujiJissi_from AND @KoujiJissi_to ")
                    .AppendLine(" OR TJ.t_koj_date BETWEEN @KoujiJissi_from AND @KoujiJissi_to ) ")
                    paramList.Add(MakeParam("@KoujiJissi_from", SqlDbType.VarChar, 10, strFrom))
                    paramList.Add(MakeParam("@KoujiJissi_to", SqlDbType.VarChar, 10, strTo))
                ElseIf strFrom <> String.Empty And strTo = String.Empty Then
                    .AppendLine(" AND (TJ.kairy_koj_date = @KoujiJissi_from OR TJ.t_koj_date = @KoujiJissi_from)")
                    paramList.Add(MakeParam("@KoujiJissi_from", SqlDbType.VarChar, 10, strFrom))
                End If
            End If



            'Á¿XR[h
            If dtParam.Rows(0).Item("KameitenCd").ToString <> String.Empty Then
                .AppendLine(" AND MK.kameiten_cd = @KameitenCd  ")
                paramList.Add(MakeParam("@KameitenCd", SqlDbType.VarChar, 5, dtParam.Rows(0).Item("KameitenCd")))
            End If

            's¹{§
            If dtParam.Rows(0).Item("todoufuken").ToString <> String.Empty Then
                arrKbn = dtParam.Rows(0).Item("todoufuken").ToString.Split(",")
                .AppendLine(" AND MT.todouhuken_cd IN ( ")
                For i As Integer = 0 To arrKbn.Length - 1
                    If i = 0 Then
                        .AppendLine("     @todoufuken" & i & "   ")
                    Else
                        .AppendLine("     ,@todoufuken" & i & "   ")
                    End If
                    paramList.Add(MakeParam("@todoufuken" & i, SqlDbType.VarChar, 2, arrKbn(i)))
                Next
                .AppendLine("       ) ")
            End If

            'nñR[h
            If dtParam.Rows(0).Item("KeiretuCd").ToString <> String.Empty Then
                .AppendLine(" AND MK.keiretu_cd = @KeiretuCd  ")
                paramList.Add(MakeParam("@KeiretuCd", SqlDbType.VarChar, 5, dtParam.Rows(0).Item("KeiretuCd")))
            End If

            'cÆR[h
            If dtParam.Rows(0).Item("EigyousyoCd").ToString <> String.Empty Then
                .AppendLine(" AND MK.eigyousyo_cd = @EigyousyoCd  ")
                paramList.Add(MakeParam("@EigyousyoCd", SqlDbType.VarChar, 5, dtParam.Rows(0).Item("EigyousyoCd")))
            End If


            'ScÆID
            If dtParam.Rows(0).Item("TantouEigyouID").ToString <> String.Empty Then
                .AppendLine(" AND MK.eigyou_tantousya_mei = @TantouEigyouID  ")
                paramList.Add(MakeParam("@TantouEigyouID", SqlDbType.VarChar, 64, dtParam.Rows(0).Item("TantouEigyouID")))
            End If

            ' .AppendLine(" ORDER BY  TJ.kbn + TJ.hosyousyo_no  ")
      
            'Hã
            If dtParam.Rows(0).Item("KoujiUriage").ToString <> String.Empty Then
                strFrom = ""
                strTo = ""
                strFrom = Split(dtParam.Rows(0).Item("KoujiUriage").ToString, ",")(0)
                strTo = Split(dtParam.Rows(0).Item("KoujiUriage").ToString, ",")(1)
                If dtParam.Rows(0).Item("CHKKouji").ToString Then
                    If strFrom <> String.Empty And strTo <> String.Empty Then
                        commandTextSb1.AppendLine(" AND TTS2.kbn is not null ")
                        commandTextSb1.AppendLine(" AND (TTS2.uri_date BETWEEN @KoujiUriage_from AND @KoujiUriage_to AND (TJ.koj_gaisya_seikyuu_umu=@koj_gaisya_seikyuu_umu OR TJ.koj_gaisya_seikyuu_umu IS NULL))")
                        commandTextSb1.AppendLine(" union ")
                        commandTextSbMAIN.AppendLine(commandTextSb.ToString)
                        commandTextSbMAIN.AppendLine(commandTextSb1.ToString)

                        commandTextSb2.AppendLine(" AND TTS3.kbn is not null ")
                        commandTextSb2.AppendLine(" AND (TTS3.uri_date BETWEEN @KoujiUriage_from AND @KoujiUriage_to AND (TJ.t_koj_kaisya_seikyuu_umu=@koj_gaisya_seikyuu_umu OR TJ.t_koj_kaisya_seikyuu_umu IS NULL))  ")
                        commandTextSbMAIN.AppendLine(commandTextSb.ToString)
                        commandTextSbMAIN.AppendLine(commandTextSb2.ToString)

                        paramList.Add(MakeParam("@KoujiUriage_from", SqlDbType.VarChar, 10, strFrom))
                        paramList.Add(MakeParam("@KoujiUriage_to", SqlDbType.VarChar, 10, strTo))
                        paramList.Add(MakeParam("@koj_gaisya_seikyuu_umu", SqlDbType.Int, 4, "0"))
                    ElseIf strFrom <> String.Empty And strTo = String.Empty Then

                        commandTextSb1.AppendLine(" AND TTS2.kbn is not null ")
                        commandTextSb1.AppendLine(" AND (TTS2.uri_date = @KoujiUriage_from AND (TJ.koj_gaisya_seikyuu_umu=@koj_gaisya_seikyuu_umu OR TJ.koj_gaisya_seikyuu_umu IS NULL))")
                        commandTextSb1.AppendLine(" union ")
                        commandTextSbMAIN.AppendLine(commandTextSb.ToString)
                        commandTextSbMAIN.AppendLine(commandTextSb1.ToString)
                        commandTextSb2.AppendLine(" AND TTS3.kbn is not null ")
                        commandTextSb2.AppendLine(" AND (TTS3.uri_date= @KoujiUriage_from AND (TJ.t_koj_kaisya_seikyuu_umu=@koj_gaisya_seikyuu_umu OR TJ.t_koj_kaisya_seikyuu_umu IS NULL))  ")
                        commandTextSbMAIN.AppendLine(commandTextSb.ToString)
                        commandTextSbMAIN.AppendLine(commandTextSb2.ToString)

                        '.AppendLine(" AND ((TTS2.uri_date = @KoujiUriage_from AND (TJ.koj_gaisya_seikyuu_umu=@koj_gaisya_seikyuu_umu OR TJ.koj_gaisya_seikyuu_umu IS NULL)) OR ")
                        '.AppendLine("( TTS3.uri_date= @KoujiUriage_from AND (TJ.t_koj_kaisya_seikyuu_umu=@koj_gaisya_seikyuu_umu OR TJ.t_koj_kaisya_seikyuu_umu IS NULL)))")
                        paramList.Add(MakeParam("@KoujiUriage_from", SqlDbType.VarChar, 10, strFrom))
                        paramList.Add(MakeParam("@koj_gaisya_seikyuu_umu", SqlDbType.Int, 4, "0"))
                    End If
                Else
                    If strFrom <> String.Empty And strTo <> String.Empty Then
                        .AppendLine(" AND (TTS2.uri_date BETWEEN @KoujiUriage_from AND @KoujiUriage_to ")
                        .AppendLine(" OR TTS3.uri_date BETWEEN @KoujiUriage_from AND @KoujiUriage_to ) ")
                        paramList.Add(MakeParam("@KoujiUriage_from", SqlDbType.VarChar, 10, strFrom))
                        paramList.Add(MakeParam("@KoujiUriage_to", SqlDbType.VarChar, 10, strTo))
                    ElseIf strFrom <> String.Empty And strTo = String.Empty Then
                        .AppendLine(" AND (TTS2.uri_date = @KoujiUriage_from OR TTS3.uri_date = @KoujiUriage_from)")
                        paramList.Add(MakeParam("@KoujiUriage_from", SqlDbType.VarChar, 10, strFrom))
                    End If
                    .AppendLine(" AND (TTS2.kbn is not null or TTS3.kbn is not null)")
                    commandTextSbMAIN.AppendLine(commandTextSb.ToString)
                End If
            Else
                commandTextSbMAIN.AppendLine(commandTextSb.ToString)
            End If
        End With
        


        'õÀs()
        FillDataset(connStr, CommandType.Text, commandTextSbMAIN.ToString(), dsBukkenJyouhouDataSet, _
                    dsBukkenJyouhouDataSet.BukkenJyouhouCSVTable.TableName, paramList.ToArray)

        Return dsBukkenJyouhouDataSet.BukkenJyouhouCSVTable

    End Function
End Class

Imports System.Data.SqlClient
Imports System.Text
Imports Itis.ApplicationBlocks.Data.SQLHelper

''' <summary>加盟店情報を検索照会する</summary>
''' <remarks>加盟店検索照会機能を提供する</remarks>
''' <history>
''' <para>2009/07/15　付龍(大連情報システム部)　新規作成</para>
''' </history>
Public Class KensakuSyoukaiInquiryDataAccess

    Private connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>加盟店情報データを取得する</summary>
    ''' <param name="dtParamKameitenInfo">パラメータ</param>
    ''' <returns>加盟店情報データテーブル</returns>
    Public Function SelKameitenInfo(ByVal dtParamKameitenInfo As KensakuSyoukaiInquiryDataSet.Param_KameitenInfoDataTable) _
           As KensakuSyoukaiInquiryDataSet.KameitenInfoTableDataTable

        'DataSetインスタンスの生成
        Dim dsKensakuSyoukaiInquiry As New KensakuSyoukaiInquiryDataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine(" SELECT ")
            If dtParamKameitenInfo(0).kensaku_count = "100" Then
                .AppendLine("      TOP 100 ")
            End If
            .AppendLine("      MKK.torikesi, ")
            .AppendLine("      MKK.kbn, ")
            .AppendLine("      MKK.kameiten_cd, ")
            .AppendLine("      MKK.kameiten_mei1, ")
            .AppendLine("      MKK.tenmei_kana1, ")
            .AppendLine("      MKK.kameiten_mei2, ")
            .AppendLine("      ISNULL(MKJ0.jyuusyo1,'') + ISNULL(MKJ0.jyuusyo2,'') AS jyuusyo1, ")
            .AppendLine("      (CASE WHEN MKK.todouhuken_mei = '：' THEN '' ELSE MKK.todouhuken_mei END) AS todouhuken_mei, ")
            .AppendLine("      MKK.keiretu_cd, ")
            .AppendLine("      MKK.eigyousyo_cd, ")
            .AppendLine("      MKK.builder_no, ")
            .AppendLine("      MKJ0.daihyousya_mei, ")
            .AppendLine("      MKJ0.tel_no ")
            .AppendLine(" FROM ")
            .AppendLine(" (SELECT DISTINCT ")
            '================2012/03/28 車龍 405721案件の対応 修正↓==================================
            '.AppendLine("      MK.torikesi, ")
            .AppendLine("      CASE ")
            .AppendLine("          WHEN MK.torikesi = 0 THEN ")
            .AppendLine("              '0' ")
            .AppendLine("          ELSE ")
            .AppendLine("              CONVERT(VARCHAR(10),MK.torikesi) + ':' + ISNULL(MKM.meisyou,'') ")
            .AppendLine("          END AS torikesi, ")
            '================2012/03/28 車龍 405721案件の対応 修正↑==================================
            .AppendLine("      MK.kbn, ")
            .AppendLine("      MK.kameiten_cd, ")
            .AppendLine("      MK.kameiten_mei1, ")
            .AppendLine("      MK.tenmei_kana1, ")
            .AppendLine("      (CASE WHEN MK.kameiten_mei2 <> '' THEN MK.kameiten_mei2 ELSE tenmei_kana2 END) AS kameiten_mei2, ")
            .AppendLine("      ISNULL(MK.todouhuken_cd,'') + '：' + ISNULL(MT.todouhuken_mei,'') AS todouhuken_mei, ")
            .AppendLine("      MK.keiretu_cd, ")
            .AppendLine("      MK.eigyousyo_cd, ")
            .AppendLine("      MK.builder_no ")
            .AppendLine(" FROM ")
            .AppendLine("      m_kameiten AS MK ")
            .AppendLine(" LEFT OUTER JOIN m_kameiten_jyuusyo AS MKJ ")
            .AppendLine(" ON MK.kameiten_cd = MKJ.kameiten_cd ")
            .AppendLine(" LEFT OUTER JOIN m_todoufuken AS MT ")
            .AppendLine(" ON MK.todouhuken_cd = MT.todouhuken_cd ")
            .AppendLine(" LEFT OUTER JOIN ")
            .AppendLine("   (SELECT kameiten_cd, daihyousya_mei, tel_no ")
            .AppendLine("   FROM m_kameiten_jyuusyo WITH(READCOMMITTED) ")
            .AppendLine("   WHERE jyuusyo_no = 1) AS MKJ1 ")
            .AppendLine(" ON MK.kameiten_cd = MKJ1.kameiten_cd ")
            '================2012/03/28 車龍 405721案件の対応 追加↓==================================
            .AppendLine(" LEFT OUTER JOIN m_kakutyou_meisyou AS MKM ") '--拡張名称マスタ
            .AppendLine(" ON MK.torikesi = MKM.code ")
            .AppendLine(" AND MKM.meisyou_syubetu = 56 ")
            '================2012/03/28 車龍 405721案件の対応 追加↑==================================
            .AppendLine(" WHERE ")
            .AppendLine("       1 = 1 ")
            '区分
            If dtParamKameitenInfo(0).kbn <> String.Empty Then
                Dim arrKbn() As String = dtParamKameitenInfo(0).kbn.Split(",")
                .AppendLine(" AND MK.kbn IN ( ")
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
            '退会した加盟店
            If dtParamKameitenInfo(0).taikai <> "1" Then
                .AppendLine(" AND MK.torikesi = 0 ")
            End If
            '加盟店名
            If dtParamKameitenInfo(0).kameiten_mei <> String.Empty Then
                .AppendLine(" AND (MK.kameiten_mei1 LIKE @kameiten_mei1 ")
                .AppendLine("      OR MK.kameiten_mei2 LIKE @kameiten_mei2) ")
                paramList.Add(MakeParam("@kameiten_mei1", SqlDbType.VarChar, 42, "%" & dtParamKameitenInfo(0).kameiten_mei & "%"))
                paramList.Add(MakeParam("@kameiten_mei2", SqlDbType.VarChar, 42, "%" & dtParamKameitenInfo(0).kameiten_mei & "%"))
            End If
            '加盟店コード
            If dtParamKameitenInfo(0).kameiten_cd_from <> String.Empty And dtParamKameitenInfo(0).kameiten_cd_to <> String.Empty Then
                .AppendLine(" AND MK.kameiten_cd BETWEEN @kameiten_cd_from AND @kameiten_cd_to ")
                paramList.Add(MakeParam("@kameiten_cd_from", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).kameiten_cd_from))
                paramList.Add(MakeParam("@kameiten_cd_to", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).kameiten_cd_to))
            ElseIf dtParamKameitenInfo(0).kameiten_cd_from <> String.Empty And dtParamKameitenInfo(0).kameiten_cd_to = String.Empty Then
                .AppendLine(" AND MK.kameiten_cd = @kameiten_cd_from ")
                paramList.Add(MakeParam("@kameiten_cd_from", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).kameiten_cd_from))
            End If
            '加盟店名カナ
            If dtParamKameitenInfo(0).kameiten_kana <> String.Empty Then
                .AppendLine(" AND (MK.tenmei_kana1 LIKE @kameiten_kana1 ")
                .AppendLine("      OR MK.tenmei_kana2 LIKE @kameiten_kana2) ")
                paramList.Add(MakeParam("@kameiten_kana1", SqlDbType.VarChar, 22, "%" & dtParamKameitenInfo(0).kameiten_kana & "%"))
                paramList.Add(MakeParam("@kameiten_kana2", SqlDbType.VarChar, 22, "%" & dtParamKameitenInfo(0).kameiten_kana & "%"))
            End If
            '営業所コード
            If dtParamKameitenInfo(0).eigyousyo_cd_from <> String.Empty And dtParamKameitenInfo(0).eigyousyo_cd_to <> String.Empty Then
                .AppendLine(" AND MK.eigyousyo_cd BETWEEN @eigyousyo_cd_from AND @eigyousyo_cd_to ")
                paramList.Add(MakeParam("@eigyousyo_cd_from", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).eigyousyo_cd_from))
                paramList.Add(MakeParam("@eigyousyo_cd_to", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).eigyousyo_cd_to))
            ElseIf dtParamKameitenInfo(0).eigyousyo_cd_from <> String.Empty And dtParamKameitenInfo(0).eigyousyo_cd_to = String.Empty Then
                .AppendLine(" AND MK.eigyousyo_cd = @eigyousyo_cd_from ")
                paramList.Add(MakeParam("@eigyousyo_cd_from", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).eigyousyo_cd_from))
            End If
            '電話番号
            If dtParamKameitenInfo(0).tel_no <> String.Empty Then
                .AppendLine(" AND REPLACE(MKJ.tel_no,'-','') LIKE @tel_no ")
                paramList.Add(MakeParam("@tel_no", SqlDbType.VarChar, 17, dtParamKameitenInfo(0).tel_no & "%"))
            End If
            '登録年月
            If dtParamKameitenInfo(0).touroku_nengetu_from <> String.Empty And dtParamKameitenInfo(0).touroku_nengetu_to <> String.Empty Then
                .AppendLine(" AND MKJ.add_nengetu BETWEEN @touroku_nengetu_from AND @touroku_nengetu_to ")
                paramList.Add(MakeParam("@touroku_nengetu_from", SqlDbType.VarChar, 7, dtParamKameitenInfo(0).touroku_nengetu_from))
                paramList.Add(MakeParam("@touroku_nengetu_to", SqlDbType.VarChar, 7, dtParamKameitenInfo(0).touroku_nengetu_to))
            ElseIf dtParamKameitenInfo(0).touroku_nengetu_from <> String.Empty And dtParamKameitenInfo(0).touroku_nengetu_to = String.Empty Then
                .AppendLine(" AND MKJ.add_nengetu = @touroku_nengetu_from ")
                paramList.Add(MakeParam("@touroku_nengetu_from", SqlDbType.VarChar, 7, dtParamKameitenInfo(0).touroku_nengetu_from))
            End If
            '都道府県
            If dtParamKameitenInfo(0).todouhuken_cd <> String.Empty Then
                .AppendLine(" AND MK.todouhuken_cd = @todouhuken_cd ")
                paramList.Add(MakeParam("@todouhuken_cd", SqlDbType.VarChar, 2, dtParamKameitenInfo(0).todouhuken_cd))
            End If
            '系列コード
            If dtParamKameitenInfo(0).keiretu_cd <> String.Empty Then
                .AppendLine(" AND MK.keiretu_cd = @keiretu_cd ")
                paramList.Add(MakeParam("@keiretu_cd", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).keiretu_cd))
            End If
            '送付先選択
            If dtParamKameitenInfo(0).soufusaki_kbn = String.Empty Then
                Dim soufusakiSb As New StringBuilder
                .AppendLine(" AND (")
                If dtParamKameitenInfo(0).jyuusyo_no <> String.Empty Then
                    soufusakiSb.Append(" MKJ.jyuusyo_no = 1 OR")
                End If
                If dtParamKameitenInfo(0).seikyuusyo_flg <> String.Empty Then
                    soufusakiSb.Append(" MKJ.seikyuusyo_flg = -1 OR")
                End If
                If dtParamKameitenInfo(0).hosyousyo_flg <> String.Empty Then
                    soufusakiSb.Append(" MKJ.hosyousyo_flg = -1 OR")
                End If
                If dtParamKameitenInfo(0).kasi_hosyousyo_flg <> String.Empty Then
                    soufusakiSb.Append(" MKJ.kasi_hosyousyo_flg = -1 OR")
                End If
                If dtParamKameitenInfo(0).teiki_kankou_flg <> String.Empty Then
                    soufusakiSb.Append(" MKJ.teiki_kankou_flg = -1 OR")
                End If
                If dtParamKameitenInfo(0).hkks_flg <> String.Empty Then
                    soufusakiSb.Append(" MKJ.hkks_flg = -1 OR")
                End If
                If dtParamKameitenInfo(0).koj_hkks_flg <> String.Empty Then
                    soufusakiSb.Append(" MKJ.koj_hkks_flg = -1 OR")
                End If
                If dtParamKameitenInfo(0).kensa_hkks_flg <> String.Empty Then
                    soufusakiSb.Append(" MKJ.kensa_hkks_flg = -1 OR")
                End If
                .AppendLine(soufusakiSb.ToString.Substring(0, soufusakiSb.ToString.Length - 2))
                .AppendLine(" ) ")
            End If
            .AppendLine(" ) AS MKK ")
            .AppendLine(" LEFT OUTER JOIN m_kameiten_jyuusyo AS MKJ0 ")
            .AppendLine(" ON MKK.kameiten_cd = MKJ0.kameiten_cd ")
            .AppendLine(" AND MKJ0.jyuusyo_no = 1 ")
            .AppendLine(" ORDER BY ")
            .AppendLine("      MKK.kameiten_cd ")
        End With

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsKensakuSyoukaiInquiry, _
                    dsKensakuSyoukaiInquiry.KameitenInfoTable.TableName, paramList.ToArray)

        Return dsKensakuSyoukaiInquiry.KameitenInfoTable

    End Function

    ''' <summary>加盟店情報データ個数を取得する</summary>
    ''' <param name="dtParamKameitenInfo">パラメータ</param>
    ''' <returns>加盟店情報データ個数</returns>
    Public Function SelKameitenInfoCount(ByVal dtParamKameitenInfo As KensakuSyoukaiInquiryDataSet.Param_KameitenInfoDataTable) _
            As Integer
        'DataSetインスタンスの生成
        Dim dsKensakuSyoukaiInquiry As New KensakuSyoukaiInquiryDataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine(" SELECT ")
            .AppendLine("      COUNT(MKK.kameiten_cd) AS kameiten_cd_count ")
            .AppendLine(" FROM ")
            .AppendLine(" (SELECT DISTINCT ")
            .AppendLine("      MK.torikesi, ")
            .AppendLine("      MK.kbn, ")
            .AppendLine("      MK.kameiten_cd, ")
            .AppendLine("      MK.kameiten_mei1, ")
            .AppendLine("      MK.tenmei_kana1, ")
            .AppendLine("      (CASE WHEN MK.kameiten_mei2 <> '' THEN MK.kameiten_mei2 ELSE tenmei_kana2 END) AS kameiten_mei2, ")
            .AppendLine("      MT.todouhuken_mei, ")
            .AppendLine("      MK.keiretu_cd, ")
            .AppendLine("      MK.eigyousyo_cd, ")
            .AppendLine("      MK.builder_no ")
            .AppendLine(" FROM ")
            .AppendLine("      m_kameiten AS MK ")
            .AppendLine(" LEFT OUTER JOIN m_kameiten_jyuusyo AS MKJ ")
            .AppendLine(" ON MK.kameiten_cd = MKJ.kameiten_cd ")
            .AppendLine(" LEFT OUTER JOIN m_todoufuken AS MT ")
            .AppendLine(" ON MK.todouhuken_cd = MT.todouhuken_cd ")
            .AppendLine(" LEFT OUTER JOIN ")
            .AppendLine("   (SELECT kameiten_cd, daihyousya_mei, tel_no ")
            .AppendLine("   FROM m_kameiten_jyuusyo WITH(READCOMMITTED) ")
            .AppendLine("   WHERE jyuusyo_no = 1) AS MKJ1 ")
            .AppendLine(" ON MK.kameiten_cd = MKJ1.kameiten_cd ")
            .AppendLine(" WHERE ")
            .AppendLine("       1 = 1 ")
            '区分
            If dtParamKameitenInfo(0).kbn <> String.Empty Then
                Dim arrKbn() As String = dtParamKameitenInfo(0).kbn.Split(",")
                .AppendLine(" AND MK.kbn IN ( ")
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
            '退会した加盟店
            If dtParamKameitenInfo(0).taikai <> "1" Then
                .AppendLine(" AND MK.torikesi = 0 ")
            End If
            '加盟店名
            If dtParamKameitenInfo(0).kameiten_mei <> String.Empty Then
                .AppendLine(" AND (MK.kameiten_mei1 LIKE @kameiten_mei1 ")
                .AppendLine("      OR MK.kameiten_mei2 LIKE @kameiten_mei2) ")
                paramList.Add(MakeParam("@kameiten_mei1", SqlDbType.VarChar, 42, "%" & dtParamKameitenInfo(0).kameiten_mei & "%"))
                paramList.Add(MakeParam("@kameiten_mei2", SqlDbType.VarChar, 42, "%" & dtParamKameitenInfo(0).kameiten_mei & "%"))
            End If
            '加盟店コード
            If dtParamKameitenInfo(0).kameiten_cd_from <> String.Empty And dtParamKameitenInfo(0).kameiten_cd_to <> String.Empty Then
                .AppendLine(" AND MK.kameiten_cd BETWEEN @kameiten_cd_from AND @kameiten_cd_to ")
                paramList.Add(MakeParam("@kameiten_cd_from", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).kameiten_cd_from))
                paramList.Add(MakeParam("@kameiten_cd_to", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).kameiten_cd_to))
            ElseIf dtParamKameitenInfo(0).kameiten_cd_from <> String.Empty And dtParamKameitenInfo(0).kameiten_cd_to = String.Empty Then
                .AppendLine(" AND MK.kameiten_cd = @kameiten_cd_from ")
                paramList.Add(MakeParam("@kameiten_cd_from", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).kameiten_cd_from))
            End If
            '加盟店名カナ
            If dtParamKameitenInfo(0).kameiten_kana <> String.Empty Then
                .AppendLine(" AND (MK.tenmei_kana1 LIKE @kameiten_kana1 ")
                .AppendLine("      OR MK.tenmei_kana2 LIKE @kameiten_kana2) ")
                paramList.Add(MakeParam("@kameiten_kana1", SqlDbType.VarChar, 22, "%" & dtParamKameitenInfo(0).kameiten_kana & "%"))
                paramList.Add(MakeParam("@kameiten_kana2", SqlDbType.VarChar, 22, "%" & dtParamKameitenInfo(0).kameiten_kana & "%"))
            End If
            '営業所コード
            If dtParamKameitenInfo(0).eigyousyo_cd_from <> String.Empty And dtParamKameitenInfo(0).eigyousyo_cd_to <> String.Empty Then
                .AppendLine(" AND MK.eigyousyo_cd BETWEEN @eigyousyo_cd_from AND @eigyousyo_cd_to ")
                paramList.Add(MakeParam("@eigyousyo_cd_from", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).eigyousyo_cd_from))
                paramList.Add(MakeParam("@eigyousyo_cd_to", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).eigyousyo_cd_to))
            ElseIf dtParamKameitenInfo(0).eigyousyo_cd_from <> String.Empty And dtParamKameitenInfo(0).eigyousyo_cd_to = String.Empty Then
                .AppendLine(" AND MK.eigyousyo_cd = @eigyousyo_cd_from ")
                paramList.Add(MakeParam("@eigyousyo_cd_from", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).eigyousyo_cd_from))
            End If
            '電話番号
            If dtParamKameitenInfo(0).tel_no <> String.Empty Then
                .AppendLine(" AND REPLACE(MKJ.tel_no,'-','') LIKE @tel_no ")
                paramList.Add(MakeParam("@tel_no", SqlDbType.VarChar, 17, dtParamKameitenInfo(0).tel_no & "%"))
            End If
            '登録年月
            If dtParamKameitenInfo(0).touroku_nengetu_from <> String.Empty And dtParamKameitenInfo(0).touroku_nengetu_to <> String.Empty Then
                .AppendLine(" AND MKJ.add_nengetu BETWEEN @touroku_nengetu_from AND @touroku_nengetu_to ")
                paramList.Add(MakeParam("@touroku_nengetu_from", SqlDbType.VarChar, 7, dtParamKameitenInfo(0).touroku_nengetu_from))
                paramList.Add(MakeParam("@touroku_nengetu_to", SqlDbType.VarChar, 7, dtParamKameitenInfo(0).touroku_nengetu_to))
            ElseIf dtParamKameitenInfo(0).touroku_nengetu_from <> String.Empty And dtParamKameitenInfo(0).touroku_nengetu_to = String.Empty Then
                .AppendLine(" AND MKJ.add_nengetu = @touroku_nengetu_from ")
                paramList.Add(MakeParam("@touroku_nengetu_from", SqlDbType.VarChar, 7, dtParamKameitenInfo(0).touroku_nengetu_from))
            End If
            '都道府県
            If dtParamKameitenInfo(0).todouhuken_cd <> String.Empty Then
                .AppendLine(" AND MK.todouhuken_cd = @todouhuken_cd ")
                paramList.Add(MakeParam("@todouhuken_cd", SqlDbType.VarChar, 2, dtParamKameitenInfo(0).todouhuken_cd))
            End If
            '系列コード
            If dtParamKameitenInfo(0).keiretu_cd <> String.Empty Then
                .AppendLine(" AND MK.keiretu_cd = @keiretu_cd ")
                paramList.Add(MakeParam("@keiretu_cd", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).keiretu_cd))
            End If
            '送付先選択
            If dtParamKameitenInfo(0).soufusaki_kbn = String.Empty Then
                Dim soufusakiSb As New StringBuilder
                .AppendLine(" AND (")
                If dtParamKameitenInfo(0).jyuusyo_no <> String.Empty Then
                    soufusakiSb.Append(" MKJ.jyuusyo_no = 1 OR")
                End If
                If dtParamKameitenInfo(0).seikyuusyo_flg <> String.Empty Then
                    soufusakiSb.Append(" MKJ.seikyuusyo_flg = -1 OR")
                End If
                If dtParamKameitenInfo(0).hosyousyo_flg <> String.Empty Then
                    soufusakiSb.Append(" MKJ.hosyousyo_flg = -1 OR")
                End If
                If dtParamKameitenInfo(0).kasi_hosyousyo_flg <> String.Empty Then
                    soufusakiSb.Append(" MKJ.kasi_hosyousyo_flg = -1 OR")
                End If
                If dtParamKameitenInfo(0).teiki_kankou_flg <> String.Empty Then
                    soufusakiSb.Append(" MKJ.teiki_kankou_flg = -1 OR")
                End If
                If dtParamKameitenInfo(0).hkks_flg <> String.Empty Then
                    soufusakiSb.Append(" MKJ.hkks_flg = -1 OR")
                End If
                If dtParamKameitenInfo(0).koj_hkks_flg <> String.Empty Then
                    soufusakiSb.Append(" MKJ.koj_hkks_flg = -1 OR")
                End If
                If dtParamKameitenInfo(0).kensa_hkks_flg <> String.Empty Then
                    soufusakiSb.Append(" MKJ.kensa_hkks_flg = -1 OR")
                End If
                .AppendLine(soufusakiSb.ToString.Substring(0, soufusakiSb.ToString.Length - 2))
                .AppendLine(" ) ")
            End If
            .AppendLine(" ) AS MKK ")
        End With

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsKensakuSyoukaiInquiry, _
                    dsKensakuSyoukaiInquiry.KameitenInfoCountTable.TableName, paramList.ToArray)

        Return dsKensakuSyoukaiInquiry.KameitenInfoCountTable(0).kameiten_cd_count

    End Function

    ''' <summary>加盟店基本情報CSVデータを取得する</summary>
    ''' <param name="dtParamKameitenInfo">パラメータ</param>
    ''' <returns>加盟店基本情報CSVデータテーブル</returns>
    Public Function SelKihonJyouhouCsvInfo(ByVal dtParamKameitenInfo As KensakuSyoukaiInquiryDataSet.Param_KameitenInfoDataTable) _
            As Data.DataSet

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine(" SELECT DISTINCT ")
            .AppendLine("	MK.kbn, ") '--区分(加盟店マスタ.区分) ")
            .AppendLine("	MK.kameiten_cd, ") '--加盟店ｺｰﾄﾞ(加盟店マスタ.加盟店ｺｰﾄﾞ) ")
            .AppendLine("	MK.torikesi, ") '--取消(加盟店マスタ.取消) ")
            .AppendLine("	MK.kameiten_mei1, ") '--加盟店名1(加盟店マスタ.加盟店名1) ")
            .AppendLine("	MK.tenmei_kana1, ") '--店名ｶﾅ1(加盟店マスタ.店名ｶﾅ1) ")
            .AppendLine("	MK.kameiten_mei2, ") '--加盟店名2(加盟店マスタ.加盟店名2) ")
            .AppendLine("	MK.tenmei_kana2, ") '--店名ｶﾅ2(加盟店マスタ.店名ｶﾅ2) ")
            .AppendLine("	MK.eigyousyo_cd, ") '--営業所ｺｰﾄﾞ(加盟店マスタ.営業所ｺｰﾄﾞ) ")
            .AppendLine("	MK.keiretu_cd, ") '--系列ｺｰﾄﾞ(加盟店マスタ.系列ｺｰﾄﾞ) ")
            .AppendLine("	MK.th_kasi_cd, ") '--TH瑕疵ｺｰﾄﾞ(加盟店マスタ.TH瑕疵ｺｰﾄﾞ) ")
            .AppendLine("	MK.builder_no, ") '--ﾋﾞﾙﾀﾞｰNO(加盟店マスタ.ﾋﾞﾙﾀﾞｰNO) ")
            .AppendLine("	MK.hattyuu_teisi_flg, ") '--発注停止フラグ(加盟店マスタ.発注停止フラグ) ")
            .AppendLine("	MK.todouhuken_cd, ") '--都道府県ｺｰﾄﾞ(加盟店マスタ.都道府県ｺｰﾄﾞ) ")
            .AppendLine("	MK.nenkan_tousuu, ") '--年間棟数(加盟店マスタ.年間棟数) ")
            .AppendLine("	MK.eigyou_tantousya_mei, ") '--営業担当者(加盟店マスタ.営業担当者) ")
            .AppendLine("	MK.hikitugi_kanryou_date, ") '--引継完了日(加盟店マスタ.引継完了日) ")
            .AppendLine("	MK.kyuu_eigyou_tantousya_mei, ") '--旧営業担当者(加盟店マスタ.旧営業担当者) ")
            .AppendLine("	MK.jisin_hosyou_flg, ") '--地震補償FLG(加盟店マスタ.地震補償FLG) ")
            .AppendLine("	MK.jisin_hosyou_add_date, ") '--地震補償登録日(加盟店マスタ.地震補償登録日) ")
            .AppendLine("	MK.fuho_syoumeisyo_flg, ") '--付保証明FLG(加盟店マスタ.付保証明書FLG) ")
            .AppendLine("	MK.fuho_syoumeisyo_kaisi_nengetu, ") '--付保証明書開始年月(加盟店マスタ.付保証明書開始年月) ")
            .AppendLine("	MK.koj_uri_syubetsu, ") '--工事売上種別(加盟店マスタ.工事売上種別) ")
            .AppendLine("	MK.koj_support_system, ") '--工事サポートシステム(加盟店マスタ.工事サポートシステム) ")
            .AppendLine("	MK.jiosaki_flg, ") '--JIO先FLG(加盟店マスタ.JIO先FLG) ")
            .AppendLine("	MK.kameiten_seisiki_mei, ") '--正式名称(加盟店マスタ.加盟店正式名) ")
            .AppendLine("	MK.kameiten_seisiki_mei_kana, ") '--正式名称2(加盟店マスタ.加盟店正式名2) ")
            .AppendLine("	MK.kaiyaku_haraimodosi_kkk, ") '--解約払戻価格(加盟店マスタ.解約払戻価格) ")
            .AppendLine("	SYOUHIN1.syouhin_cd AS syouhin_cd1, ") '--棟区分1商品ｺｰﾄﾞ(多棟割引マスタ.商品コード(多棟割引マスタ.棟区分=1)) ")
            .AppendLine("	SYOUHIN1.syouhin_mei AS syouhin_mei1, ") '--棟区分1商品名(商品マスタ.商品名) ")
            .AppendLine("	SYOUHIN2.syouhin_cd AS syouhin_cd2, ") '--棟区分2商品ｺｰﾄﾞ(多棟割引マスタ.商品コード(多棟割引マスタ.棟区分=2)) ")
            .AppendLine("	SYOUHIN2.syouhin_mei AS syouhin_mei2, ") '--棟区分2商品名(商品マスタ.商品名) ")
            .AppendLine("	SYOUHIN3.syouhin_cd AS syouhin_cd3, ") '--棟区分3商品ｺｰﾄﾞ(多棟割引マスタ.商品コード(多棟割引マスタ.棟区分=3)) ")
            .AppendLine("	SYOUHIN3.syouhin_mei AS syouhin_mei3, ") '--棟区分3商品名(商品マスタ.商品名) ")
            .AppendLine("	MK.tys_seikyuu_saki_kbn, ") '--調査請求先 区分(加盟店マスタ.調査請求先区分) ")
            .AppendLine("	MK.tys_seikyuu_saki_cd, ") '--調査請求先コード(加盟店マスタ.調査請求先コード) ")
            .AppendLine("	MK.tys_seikyuu_saki_brc, ") '--調査請求先枝番(加盟店マスタ.調査請求先枝番) ")
            .AppendLine("	(SELECT seikyuu_saki_mei FROM v_seikyuu_saki_info WHERE ")
            .AppendLine("		seikyuu_saki_cd=MK.tys_seikyuu_saki_cd AND ")
            .AppendLine("		seikyuu_saki_brc=MK.tys_seikyuu_saki_brc AND  ")
            .AppendLine("		seikyuu_saki_kbn=MK.tys_seikyuu_saki_kbn ")
            .AppendLine("	) AS tys_seikyuu_saki_mei, ") '--調査請求先名(加盟店マスタ.加盟店名、調査会社マスタ.調査会社名、営業所マスタ.営業所名 (区分・コードにより分別) ) ")
            .AppendLine("	MK.tys_seikyuu_sime_date, ") '--調査請求締め日(加盟店マスタ.調査請求締め日) ")
            .AppendLine("	MK.koj_seikyuu_saki_kbn, ") '--工事請求先 区分(加盟店マスタ.工事請求先区分) ")
            .AppendLine("	MK.koj_seikyuu_saki_cd, ") '--工事請求先コード(加盟店マスタ.工事請求先コード) ")
            .AppendLine("	MK.koj_seikyuu_saki_brc, ") '--工事請求先枝番(加盟店マスタ.工事請求先枝番) ")
            .AppendLine("	(SELECT seikyuu_saki_mei FROM v_seikyuu_saki_info WHERE ")
            .AppendLine("		seikyuu_saki_cd=MK.koj_seikyuu_saki_cd AND ")
            .AppendLine("		seikyuu_saki_brc=MK.koj_seikyuu_saki_brc AND  ")
            .AppendLine("		seikyuu_saki_kbn=MK.koj_seikyuu_saki_kbn ")
            .AppendLine("	) AS koj_seikyuu_saki_mei, ") '--工事請求先名(加盟店マスタ.加盟店名、調査会社マスタ.調査会社名、営業所マスタ.営業所名 (区分・コードにより分別) ) ")
            .AppendLine("	MK.koj_seikyuu_sime_date, ") '--工事請求締め日(加盟店マスタ.工事請求締め日) ")
            .AppendLine("	MK.hansokuhin_seikyuu_saki_kbn, ") '--販促品請求先 区分(加盟店マスタ.販促品請求先区分) ")
            .AppendLine("	MK.hansokuhin_seikyuu_saki_cd, ") '--販促品請求先コード(加盟店マスタ.販促品請求先コード) ")
            .AppendLine("	MK.hansokuhin_seikyuu_saki_brc, ") '--販促品請求先枝番(加盟店マスタ.販促品請求先枝番) ")
            .AppendLine("	(SELECT seikyuu_saki_mei FROM v_seikyuu_saki_info WHERE ")
            .AppendLine("		seikyuu_saki_cd=MK.hansokuhin_seikyuu_saki_cd AND ")
            .AppendLine("		seikyuu_saki_brc=MK.hansokuhin_seikyuu_saki_brc AND  ")
            .AppendLine("		seikyuu_saki_kbn=MK.hansokuhin_seikyuu_saki_kbn ")
            .AppendLine("	) AS hansokuhin_seikyuu_saki_mei, ") '--販促品請求先名(加盟店マスタ.加盟店名、調査会社マスタ.調査会社名、営業所マスタ.営業所名 (区分・コードにより分別) ) ")
            .AppendLine("	MK.hansokuhin_seikyuu_sime_date, ") '--販促品請求締め日(加盟店マスタ.販促品請求締め日) ")
            .AppendLine("	MK.tatemono_seikyuu_saki_kbn, ") '--建物検査請求先 区分(加盟店マスタ.建物請求先区分) ")
            .AppendLine("	MK.tatemono_seikyuu_saki_cd, ") '--建物検査請求先コード(加盟店マスタ.建物請求先コード) ")
            .AppendLine("	MK.tatemono_seikyuu_saki_brc, ") '--建物検査請求先枝番(加盟店マスタ.建物請求先枝番) ")
            .AppendLine("	(SELECT seikyuu_saki_mei FROM v_seikyuu_saki_info WHERE ")
            .AppendLine("		seikyuu_saki_cd=MK.tatemono_seikyuu_saki_cd AND ")
            .AppendLine("		seikyuu_saki_brc=MK.tatemono_seikyuu_saki_brc AND  ")
            .AppendLine("		seikyuu_saki_kbn=MK.tatemono_seikyuu_saki_kbn ")
            .AppendLine("	) AS tatemono_seikyuu_saki_mei, ") '--建物検査請求先名(加盟店マスタ.加盟店名、調査会社マスタ.調査会社名、営業所マスタ.営業所名 (区分・コードにより分別) ) ")
            .AppendLine("	MK.hansokuhin_seikyuu_sime_date, ") '--建物検査請求締め日(加盟店マスタ.販促品請求締め日) ")
            .AppendLine("	MK.seikyuu_saki_kbn5, ") '--請求先区分5(加盟店マスタ.請求先区分5) ")
            .AppendLine("	MK.seikyuu_saki_cd5, ") '--請求先コード5(加盟店マスタ.請求先コード5) ")
            .AppendLine("	MK.seikyuu_saki_brc5, ") '--請求先枝番5(加盟店マスタ.請求先枝番5) ")
            .AppendLine("	(SELECT seikyuu_saki_mei FROM v_seikyuu_saki_info WHERE ")
            .AppendLine("		seikyuu_saki_cd=MK.seikyuu_saki_cd5 AND ")
            .AppendLine("		seikyuu_saki_brc=MK.seikyuu_saki_brc5 AND  ")
            .AppendLine("		seikyuu_saki_kbn=MK.seikyuu_saki_kbn5 ")
            .AppendLine("	) AS seikyuu_saki5_mei, ") '--請求先5名(加盟店マスタ.加盟店名、調査会社マスタ.調査会社名、営業所マスタ.営業所名 (区分・コードにより分別) ) ")
            .AppendLine("	MK.hansokuhin_seikyuu_sime_date, ") '--請求先5締め日(加盟店マスタ.販促品請求締め日) ")
            .AppendLine("	MK.seikyuu_saki_kbn6, ") '--請求先区分6(加盟店マスタ.請求先区分6) ")
            .AppendLine("	MK.seikyuu_saki_cd6, ") '--請求先コード6(加盟店マスタ.請求先コード6) ")
            .AppendLine("	MK.seikyuu_saki_brc6, ") '--請求先枝番6(加盟店マスタ.請求先枝番6) ")
            .AppendLine("	(SELECT seikyuu_saki_mei FROM v_seikyuu_saki_info WHERE ")
            .AppendLine("		seikyuu_saki_cd=MK.seikyuu_saki_cd6 AND ")
            .AppendLine("		seikyuu_saki_brc=MK.seikyuu_saki_brc6 AND  ")
            .AppendLine("		seikyuu_saki_kbn=MK.seikyuu_saki_kbn6 ")
            .AppendLine("	) AS seikyuu_saki6_mei, ") '--請求先6名(加盟店マスタ.加盟店名、調査会社マスタ.調査会社名、営業所マスタ.営業所名 (区分・コードにより分別) ) ")
            .AppendLine("	MK.hansokuhin_seikyuu_sime_date, ") '--請求先6締め日(加盟店マスタ.販促品請求締め日) ")
            .AppendLine("	MK.seikyuu_saki_kbn7, ") '--請求先区分7(加盟店マスタ.請求先区分7) ")
            .AppendLine("	MK.seikyuu_saki_cd7, ") '--請求先コード7(加盟店マスタ.請求先コード7) ")
            .AppendLine("	MK.seikyuu_saki_brc7, ") '--請求先枝番7(加盟店マスタ.請求先枝番7) ")
            .AppendLine("	(SELECT seikyuu_saki_mei FROM v_seikyuu_saki_info WHERE ")
            .AppendLine("		seikyuu_saki_cd=MK.seikyuu_saki_cd7 AND ")
            .AppendLine("		seikyuu_saki_brc=MK.seikyuu_saki_brc7 AND  ")
            .AppendLine("		seikyuu_saki_kbn=MK.seikyuu_saki_kbn7 ")
            .AppendLine("	) AS seikyuu_saki7_mei, ") '--請求先7名(加盟店マスタ.加盟店名、調査会社マスタ.調査会社名、営業所マスタ.営業所名 (区分・コードにより分別) ) ")
            .AppendLine("	MK.hansokuhin_seikyuu_sime_date, ") '--請求先7締め日(加盟店マスタ.販促品請求締め日) ")
            .AppendLine("	MK.hosyou_kikan, ") '--保証期間(加盟店マスタ.保証期間) ")
            .AppendLine("	MK.hosyousyo_hak_umu, ") '--保証書発行有無(加盟店マスタ.保証書発行有無) ")
            .AppendLine("	MK.nyuukin_kakunin_jyouken, ") '--入金確認条件(加盟店マスタ.入金確認条件) ")
            .AppendLine("	MM.meisyou, ") '--入金確認条件名(名称マスタ.名称（名称マスタ.名称種別 = "05"　で　加盟店マスタ.入金確認条件=名称マスタ.ｺｰﾄﾞ）) ")
            .AppendLine("	MK.nyuukin_kakunin_oboegaki, ") '--入金確認覚書(加盟店マスタ.入金確認覚書) ")
            .AppendLine("	MK.koj_gaisya_seikyuu_umu, ") '--工事会社請求有無(加盟店マスタ.工事会社請求有無) ")
            .AppendLine("	MK.koj_tantou_flg, ") '--工事担当FLG(加盟店マスタ.工事担当FLG) ")
            .AppendLine("	MK.tys_mitsyo_flg, ") '--調査見積書FLG(加盟店マスタ.調査見積書FLG) ")
            .AppendLine("	MK.hattyuusyo_flg, ") '--発注書FLG(加盟店マスタ.発注書FLG) ")
            .AppendLine("	MK.mitsyo_file_mei, ") '--見積書ﾌｧｲﾙ名(加盟店マスタ.見積書ﾌｧｲﾙ名) ")
            .AppendLine("	MKTJ.tys_mitsyo_flg AS tys_mitsyo, ") '--調査見積書(加盟店取引情報マスタ.調査見積書FLG) ")
            .AppendLine("	MKTJ.ks_danmenzu_flg, ") '--基礎断面図(加盟店取引情報マスタ.基礎断面図FLG) ")
            .AppendLine("	MKTJ.tatou_waribiki_flg, ") '--多棟割引区分(加盟店取引情報マスタ.多棟割引FLG) ")
            .AppendLine("	MKTJ.tatou_waribiki_bikou, ") '--多棟割引備考(加盟店取引情報マスタ.多棟割引備考) ")
            .AppendLine("	MKTJ.tokka_sinsei_flg, ") '--特価申請(加盟店取引情報マスタ.特価申請FLG) ")
            .AppendLine("	MKTJ.zando_syobunhi_umu, ") '--残土処分費(加盟店取引情報マスタ.残土処分費有無) ")
            .AppendLine("	MKTJ.kyuusuisyadai_umu, ") '--給水車代(加盟店取引情報マスタ.給水車代有無) ")
            .AppendLine("	MKTJ.jinawa_taiou_umu, ") '--地縄はり(加盟店取引情報マスタ.地縄対応有無) ")
            .AppendLine("	MKTJ.kousin_taiou_umu, ") '--杭芯出し(加盟店取引情報マスタ.杭芯対応有無) ")
            .AppendLine("	MKTJ.tys_kojkan_heikin_nissuu, ") '--平均日数(加盟店取引情報マスタ.調査工事間平均日数) ")
            .AppendLine("	MKTJ.hyoujun_ks, ") '--標準基礎(加盟店取引情報マスタ.標準基礎) ")
            .AppendLine("	MKTJ.js_igai_koj_flg, ") '--JHS以外工事(加盟店取引情報マスタ.JS以外工事FLG) ")
            .AppendLine("	MKTJ.tys_hkks_busuu, ") '--調査報告書部数(加盟店取引情報マスタ.調査報告書部数) ")
            .AppendLine("	MKTJ.koj_hkks_busuu, ") '--工事報告書部数(加盟店取引情報マスタ.工事報告書部数) ")
            .AppendLine("	MKTJ.kensa_hkks_busuu, ") '--検査報告書部数(加盟店取引情報マスタ.検査報告書部数) ")
            .AppendLine("	MKTJ.tys_hkks_douhuu_umu, ") '--調査報告書同封(加盟店取引情報マスタ.調査報告書同封有無) ")
            .AppendLine("	MKTJ.koj_hkks_douhuu_umu, ") '--工事報告書同封(加盟店取引情報マスタ.工事報告書同封有無) ")
            .AppendLine("	MKTJ.kensa_hkks_douhuu_umu, ") '--検査報告書同封(加盟店取引情報マスタ.検査報告書同封有無) ")
            .AppendLine("	MKTJ.nyuukin_mae_hosyousyo_hak, ") '--入金前保証書発行(加盟店取引情報マスタ.入金前保証書発行) ")
            .AppendLine("	MKTJ.hikiwatasi_file_umu, ") '--引渡ファイル(加盟店取引情報マスタ.引渡ﾌｧｲﾙ有無) ")
            .AppendLine("	MKTJ.sime_date, ") '--回収締め日(加盟店取引情報マスタ.締め日) ")
            .AppendLine("	MKTJ.seikyuusyo_hittyk_date, ") '--請求書必着日(加盟店取引情報マスタ.請求書必着日) ")
            .AppendLine("	MKTJ.siharai_yotei_tuki, ") '--支払予定月(加盟店取引情報マスタ.支払予定月) ")
            .AppendLine("	MKTJ.siharai_yotei_date, ") '--支払予定日(加盟店取引情報マスタ.支払予定日) ")
            .AppendLine("	MKTJ.siharai_genkin_wariai, ") '--現金割合(加盟店取引情報マスタ.支払現金割合) ")
            .AppendLine("	MKTJ.siharai_houhou_flg, ") '--支払方法(加盟店取引情報マスタ.支払方法FLG) ")
            .AppendLine("	MKTJ.siharai_tegata_wariai, ") '--手形割合(加盟店取引情報マスタ.支払手形割合) ")
            .AppendLine("	MKTJ.siharai_site, ") '--支払サイト(加盟店取引情報マスタ.支払ｻｲﾄ) ")
            .AppendLine("	MKTJ.tegata_hiritu, ") '--手形比率(加盟店取引情報マスタ.手形比率) ")
            .AppendLine("	MKTJ.tys_hattyuusyo_umu, ") '--調査発注書有無(加盟店取引情報マスタ.調査発注書有無) ")
            .AppendLine("	MKTJ.koj_hattyuusyo_umu, ") '--工事発注書有無(加盟店取引情報マスタ.工事発注書有無) ")
            .AppendLine("	MKTJ.kensa_hattyuusyo_umu, ") '--検査発注書有無(加盟店取引情報マスタ.検査発注書有無) ")
            .AppendLine("	MKTJ.senpou_sitei_seikyuusyo, ") '--先方指定請求書(加盟店取引情報マスタ.先方指定請求書) ")
            .AppendLine("	MKTJ.flow_kakunin_date, ") '--フロー確認日(加盟店取引情報マスタ.フロー確認日) ")
            .AppendLine("	MKTJ.kyouryoku_kaihi_umu, ") '--協力会費(加盟店取引情報マスタ.協力会費有無) ")
            .AppendLine("	MKTJ.kyouryoku_kaihi_hiritu, ") '--協力会費比率(加盟店取引情報マスタ.協力会費比率) ")
            .AppendLine("	MK.danmenzu1, ") '--断面図1(加盟店マスタ.断面図1) ")
            .AppendLine("	MK.danmenzu2, ") '--断面図2(加盟店マスタ.断面図2) ")
            .AppendLine("	MK.danmenzu3, ") '--断面図3(加盟店マスタ.断面図3) ")
            .AppendLine("	MK.danmenzu4, ") '--断面図4(加盟店マスタ.断面図4) ")
            .AppendLine("	MK.danmenzu5, ") '--断面図5(加盟店マスタ.断面図5) ")
            .AppendLine("	MK.danmenzu6, ") '--断面図6(加盟店マスタ.断面図6) ")
            .AppendLine("	MK.danmenzu7 ") '--断面図7(加盟店マスタ.断面図7) ")
            .AppendLine(" FROM ")
            .AppendLine("       m_kameiten AS MK WITH(READCOMMITTED) ")
            .AppendLine(" LEFT OUTER JOIN ")
            .AppendLine("       (SELECT MTS.kameiten_cd, MTS.toukubun, MTS.syouhin_cd, MS.syouhin_mei  ")
            .AppendLine("       FROM m_tatouwaribiki_settei AS MTS WITH(READCOMMITTED) ")
            .AppendLine("       LEFT OUTER JOIN  ")
            .AppendLine("       m_syouhin AS MS WITH(READCOMMITTED) ")
            .AppendLine("       ON MTS.syouhin_cd = MS.syouhin_cd ")
            .AppendLine("       WHERE MTS.toukubun = 1) AS SYOUHIN1 ")
            .AppendLine("       ON MK.kameiten_cd = SYOUHIN1.kameiten_cd ")
            .AppendLine(" LEFT OUTER JOIN  ")
            .AppendLine("       (SELECT MTS.kameiten_cd, MTS.toukubun, MTS.syouhin_cd, MS.syouhin_mei  ")
            .AppendLine("       FROM m_tatouwaribiki_settei AS MTS WITH(READCOMMITTED) ")
            .AppendLine("       LEFT OUTER JOIN  ")
            .AppendLine("       m_syouhin AS MS WITH(READCOMMITTED) ")
            .AppendLine("       ON MTS.syouhin_cd = MS.syouhin_cd ")
            .AppendLine("       WHERE MTS.toukubun = 2) AS SYOUHIN2 ")
            .AppendLine("       ON MK.kameiten_cd = SYOUHIN2.kameiten_cd ")
            .AppendLine(" LEFT OUTER JOIN  ")
            .AppendLine("      	(SELECT MTS.kameiten_cd, MTS.toukubun, MTS.syouhin_cd, MS.syouhin_mei  ")
            .AppendLine("      	FROM m_tatouwaribiki_settei AS MTS WITH(READCOMMITTED) ")
            .AppendLine("      	LEFT OUTER JOIN ")
            .AppendLine("      	m_syouhin AS MS WITH(READCOMMITTED) ")
            .AppendLine("      	ON MTS.syouhin_cd = MS.syouhin_cd ")
            .AppendLine("      	WHERE MTS.toukubun = 3) AS SYOUHIN3 ")
            .AppendLine("      	ON MK.kameiten_cd = SYOUHIN3.kameiten_cd ")
            .AppendLine(" LEFT OUTER JOIN ")
            .AppendLine("      	m_kameiten_torihiki_jouhou AS MKTJ WITH(READCOMMITTED) ")
            .AppendLine("      	ON MK.kameiten_cd = MKTJ.kameiten_cd ")
            .AppendLine(" LEFT OUTER JOIN ")
            .AppendLine("      	m_meisyou AS MM WITH(READCOMMITTED) ")
            .AppendLine("      	ON MK.nyuukin_kakunin_jyouken = MM.code ")
            .AppendLine("      	AND MM.meisyou_syubetu = '05' ")
            .AppendLine(" LEFT OUTER JOIN ")
            .AppendLine("      	m_kameiten_jyuusyo AS MKJ WITH(READCOMMITTED) ")
            .AppendLine("      	ON MK.kameiten_cd = MKJ.kameiten_cd ")
            .AppendLine(" WHERE ")
            .AppendLine("       1 = 1 ")
            '区分
            If dtParamKameitenInfo(0).kbn <> String.Empty Then
                Dim arrKbn() As String = dtParamKameitenInfo(0).kbn.Split(",")
                .AppendLine(" AND MK.kbn IN ( ")
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
            '退会した加盟店
            If dtParamKameitenInfo(0).taikai <> "1" Then
                .AppendLine(" AND MK.torikesi = 0 ")
            End If
            '加盟店名
            If dtParamKameitenInfo(0).kameiten_mei <> String.Empty Then
                .AppendLine(" AND (MK.kameiten_mei1 LIKE @kameiten_mei1 ")
                .AppendLine("      OR MK.kameiten_mei2 LIKE @kameiten_mei2) ")
                paramList.Add(MakeParam("@kameiten_mei1", SqlDbType.VarChar, 42, "%" & dtParamKameitenInfo(0).kameiten_mei & "%"))
                paramList.Add(MakeParam("@kameiten_mei2", SqlDbType.VarChar, 42, "%" & dtParamKameitenInfo(0).kameiten_mei & "%"))
            End If
            '加盟店コード
            If dtParamKameitenInfo(0).kameiten_cd_from <> String.Empty And dtParamKameitenInfo(0).kameiten_cd_to <> String.Empty Then
                .AppendLine(" AND MK.kameiten_cd BETWEEN @kameiten_cd_from AND @kameiten_cd_to ")
                paramList.Add(MakeParam("@kameiten_cd_from", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).kameiten_cd_from))
                paramList.Add(MakeParam("@kameiten_cd_to", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).kameiten_cd_to))
            ElseIf dtParamKameitenInfo(0).kameiten_cd_from <> String.Empty And dtParamKameitenInfo(0).kameiten_cd_to = String.Empty Then
                .AppendLine(" AND MK.kameiten_cd = @kameiten_cd_from ")
                paramList.Add(MakeParam("@kameiten_cd_from", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).kameiten_cd_from))
            End If
            '加盟店名カナ
            If dtParamKameitenInfo(0).kameiten_kana <> String.Empty Then
                .AppendLine(" AND (MK.tenmei_kana1 LIKE @kameiten_kana1 ")
                .AppendLine("      OR MK.tenmei_kana2 LIKE @kameiten_kana2) ")
                paramList.Add(MakeParam("@kameiten_kana1", SqlDbType.VarChar, 22, "%" & dtParamKameitenInfo(0).kameiten_kana & "%"))
                paramList.Add(MakeParam("@kameiten_kana2", SqlDbType.VarChar, 22, "%" & dtParamKameitenInfo(0).kameiten_kana & "%"))
            End If
            '営業所コード
            If dtParamKameitenInfo(0).eigyousyo_cd_from <> String.Empty And dtParamKameitenInfo(0).eigyousyo_cd_to <> String.Empty Then
                .AppendLine(" AND MK.eigyousyo_cd BETWEEN @eigyousyo_cd_from AND @eigyousyo_cd_to ")
                paramList.Add(MakeParam("@eigyousyo_cd_from", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).eigyousyo_cd_from))
                paramList.Add(MakeParam("@eigyousyo_cd_to", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).eigyousyo_cd_to))
            ElseIf dtParamKameitenInfo(0).eigyousyo_cd_from <> String.Empty And dtParamKameitenInfo(0).eigyousyo_cd_to = String.Empty Then
                .AppendLine(" AND MK.eigyousyo_cd = @eigyousyo_cd_from ")
                paramList.Add(MakeParam("@eigyousyo_cd_from", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).eigyousyo_cd_from))
            End If
            '電話番号
            If dtParamKameitenInfo(0).tel_no <> String.Empty Then
                .AppendLine(" AND REPLACE(MKJ.tel_no,'-','') LIKE @tel_no ")
                paramList.Add(MakeParam("@tel_no", SqlDbType.VarChar, 17, dtParamKameitenInfo(0).tel_no & "%"))
            End If
            '登録年月
            If dtParamKameitenInfo(0).touroku_nengetu_from <> String.Empty And dtParamKameitenInfo(0).touroku_nengetu_to <> String.Empty Then
                .AppendLine(" AND MKJ.add_nengetu BETWEEN @touroku_nengetu_from AND @touroku_nengetu_to ")
                paramList.Add(MakeParam("@touroku_nengetu_from", SqlDbType.VarChar, 7, dtParamKameitenInfo(0).touroku_nengetu_from))
                paramList.Add(MakeParam("@touroku_nengetu_to", SqlDbType.VarChar, 7, dtParamKameitenInfo(0).touroku_nengetu_to))
            ElseIf dtParamKameitenInfo(0).touroku_nengetu_from <> String.Empty And dtParamKameitenInfo(0).touroku_nengetu_to = String.Empty Then
                .AppendLine(" AND MKJ.add_nengetu = @touroku_nengetu_from ")
                paramList.Add(MakeParam("@touroku_nengetu_from", SqlDbType.VarChar, 7, dtParamKameitenInfo(0).touroku_nengetu_from))
            End If
            '都道府県
            If dtParamKameitenInfo(0).todouhuken_cd <> String.Empty Then
                .AppendLine(" AND MK.todouhuken_cd = @todouhuken_cd ")
                paramList.Add(MakeParam("@todouhuken_cd", SqlDbType.VarChar, 2, dtParamKameitenInfo(0).todouhuken_cd))
            End If
            '系列コード
            If dtParamKameitenInfo(0).keiretu_cd <> String.Empty Then
                .AppendLine(" AND MK.keiretu_cd = @keiretu_cd ")
                paramList.Add(MakeParam("@keiretu_cd", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).keiretu_cd))
            End If
            '送付先選択
            If dtParamKameitenInfo(0).soufusaki_kbn = String.Empty Then
                Dim soufusakiSb As New StringBuilder
                .AppendLine(" AND (")
                If dtParamKameitenInfo(0).jyuusyo_no <> String.Empty Then
                    soufusakiSb.Append(" MKJ.jyuusyo_no = 1 OR")
                End If
                If dtParamKameitenInfo(0).seikyuusyo_flg <> String.Empty Then
                    soufusakiSb.Append(" MKJ.seikyuusyo_flg = -1 OR")
                End If
                If dtParamKameitenInfo(0).hosyousyo_flg <> String.Empty Then
                    soufusakiSb.Append(" MKJ.hosyousyo_flg = -1 OR")
                End If
                If dtParamKameitenInfo(0).kasi_hosyousyo_flg <> String.Empty Then
                    soufusakiSb.Append(" MKJ.kasi_hosyousyo_flg = -1 OR")
                End If
                If dtParamKameitenInfo(0).teiki_kankou_flg <> String.Empty Then
                    soufusakiSb.Append(" MKJ.teiki_kankou_flg = -1 OR")
                End If
                If dtParamKameitenInfo(0).hkks_flg <> String.Empty Then
                    soufusakiSb.Append(" MKJ.hkks_flg = -1 OR")
                End If
                If dtParamKameitenInfo(0).koj_hkks_flg <> String.Empty Then
                    soufusakiSb.Append(" MKJ.koj_hkks_flg = -1 OR")
                End If
                If dtParamKameitenInfo(0).kensa_hkks_flg <> String.Empty Then
                    soufusakiSb.Append(" MKJ.kensa_hkks_flg = -1 OR")
                End If
                .AppendLine(soufusakiSb.ToString.Substring(0, soufusakiSb.ToString.Length - 2))
                .AppendLine(" ) ")
            End If
            .AppendLine(" ORDER BY ")
            .AppendLine("      MK.kameiten_cd ")
        End With

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, _
                    "KihonJyouhouCsv", paramList.ToArray)

        Return dsReturn

    End Function

    ''' <summary>加盟店住所情報CSVデータを取得する</summary>
    ''' <param name="dtParamKameitenInfo">パラメータ</param>
    ''' <returns>加盟店住所情報CSVデータテーブル</returns>
    Public Function SelJyusyoJyouhouCsvInfo(ByVal dtParamKameitenInfo As KensakuSyoukaiInquiryDataSet.Param_KameitenInfoDataTable) _
           As Data.DataSet

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine(" SELECT DISTINCT ")
            .AppendLine(" 	MK.torikesi, ")
            .AppendLine(" 	MK.kbn, ")
            .AppendLine(" 	MK.kameiten_cd, ")
            .AppendLine(" 	MK.kameiten_mei1, ")
            .AppendLine(" 	MK.tenmei_kana1, ")
            .AppendLine(" 	MK.kameiten_mei2, ")
            .AppendLine(" 	MK.tenmei_kana2, ")
            .AppendLine(" 	MK.todouhuken_cd, ")
            .AppendLine(" 	MT.todouhuken_mei, ")
            .AppendLine(" 	MKJ1.daihyousya_mei, ")
            .AppendLine(" 	MKJ1.add_nengetu, ")
            .AppendLine(" 	MK.builder_no, ")
            .AppendLine(" 	MK.eigyousyo_cd, ")
            .AppendLine(" 	ME.eigyousyo_mei, ")
            .AppendLine(" 	MKJ1.yuubin_no1, ")
            .AppendLine(" 	MKJ1.jyuusyo11, ")
            .AppendLine(" 	MKJ1.jyuusyo21, ")
            .AppendLine(" 	MKJ1.tel_no1, ")
            .AppendLine(" 	MKJ1.fax_no1, ")
            .AppendLine(" 	MKJ1.busyo_mei1, ")
            .AppendLine(" 	MKJ1.upd_datetime1, ")
            .AppendLine(" 	MKJ1.bikou11, ")
            .AppendLine(" 	MKJ1.bikou21, ")
            .AppendLine(" 	MKJ2.seikyuusyo_flg2, ")
            .AppendLine(" 	MKJ2.hosyousyo_flg2, ")
            .AppendLine(" 	MKJ2.kasi_hosyousyo_flg2, ")
            .AppendLine(" 	MKJ2.teiki_kankou_flg2, ")
            .AppendLine(" 	MKJ2.hkks_flg2, ")
            .AppendLine(" 	MKJ2.koj_hkks_flg2, ")
            .AppendLine(" 	MKJ2.kensa_hkks_flg2, ")
            .AppendLine(" 	MKJ2.yuubin_no2, ")
            .AppendLine(" 	MKJ2.jyuusyo12, ")
            .AppendLine(" 	MKJ2.jyuusyo22, ")
            .AppendLine(" 	MKJ2.tel_no2, ")
            .AppendLine(" 	MKJ2.fax_no2, ")
            .AppendLine(" 	MKJ2.busyo_mei2, ")
            .AppendLine(" 	MKJ2.daihyousya_mei AS daihyousya_mei1, ") '--送付先1：代表者名(加盟店住所マスタ.代表者名(加盟店住所マスタ.住所NO=2))
            .AppendLine(" 	MKJ2.upd_datetime2, ")
            .AppendLine(" 	MKJ2.bikou12, ")
            .AppendLine(" 	MKJ3.seikyuusyo_flg3, ")
            .AppendLine(" 	MKJ3.hosyousyo_flg3, ")
            .AppendLine(" 	MKJ3.kasi_hosyousyo_flg3, ")
            .AppendLine(" 	MKJ3.teiki_kankou_flg3, ")
            .AppendLine(" 	MKJ3.hkks_flg3, ")
            .AppendLine(" 	MKJ3.koj_hkks_flg3, ")
            .AppendLine(" 	MKJ3.kensa_hkks_flg3, ")
            .AppendLine(" 	MKJ3.yuubin_no3, ")
            .AppendLine(" 	MKJ3.jyuusyo13, ")
            .AppendLine(" 	MKJ3.jyuusyo23, ")
            .AppendLine(" 	MKJ3.tel_no3, ")
            .AppendLine(" 	MKJ3.fax_no3, ")
            .AppendLine(" 	MKJ3.busyo_mei3, ")
            .AppendLine(" 	MKJ3.daihyousya_mei AS daihyousya_mei2, ") '--送付先1：代表者名(加盟店住所マスタ.代表者名(加盟店住所マスタ.住所NO=3))
            .AppendLine(" 	MKJ3.upd_datetime3, ")
            .AppendLine(" 	MKJ3.bikou13, ")
            .AppendLine(" 	MKJ4.seikyuusyo_flg4, ")
            .AppendLine(" 	MKJ4.hosyousyo_flg4, ")
            .AppendLine(" 	MKJ4.kasi_hosyousyo_flg4, ")
            .AppendLine(" 	MKJ4.teiki_kankou_flg4, ")
            .AppendLine(" 	MKJ4.hkks_flg4, ")
            .AppendLine(" 	MKJ4.koj_hkks_flg4, ")
            .AppendLine(" 	MKJ4.kensa_hkks_flg4, ")
            .AppendLine(" 	MKJ4.yuubin_no4, ")
            .AppendLine(" 	MKJ4.jyuusyo14, ")
            .AppendLine(" 	MKJ4.jyuusyo24, ")
            .AppendLine(" 	MKJ4.tel_no4, ")
            .AppendLine(" 	MKJ4.fax_no4, ")
            .AppendLine(" 	MKJ4.busyo_mei4, ")
            .AppendLine(" 	MKJ4.daihyousya_mei AS daihyousya_mei3, ") '--送付先1：代表者名(加盟店住所マスタ.代表者名(加盟店住所マスタ.住所NO=4))
            .AppendLine(" 	MKJ4.upd_datetime4, ")
            .AppendLine(" 	MKJ4.bikou14, ")
            .AppendLine(" 	MKJ5.seikyuusyo_flg5, ")
            .AppendLine(" 	MKJ5.hosyousyo_flg5, ")
            .AppendLine(" 	MKJ5.kasi_hosyousyo_flg5, ")
            .AppendLine(" 	MKJ5.teiki_kankou_flg5, ")
            .AppendLine(" 	MKJ5.hkks_flg5, ")
            .AppendLine(" 	MKJ5.koj_hkks_flg5, ")
            .AppendLine(" 	MKJ5.kensa_hkks_flg5, ")
            .AppendLine(" 	MKJ5.yuubin_no5, ")
            .AppendLine(" 	MKJ5.jyuusyo15, ")
            .AppendLine(" 	MKJ5.jyuusyo25, ")
            .AppendLine(" 	MKJ5.tel_no5, ")
            .AppendLine(" 	MKJ5.fax_no5, ")
            .AppendLine(" 	MKJ5.busyo_mei5, ")
            .AppendLine(" 	MKJ5.daihyousya_mei AS daihyousya_mei4, ") '--送付先1：代表者名(加盟店住所マスタ.代表者名(加盟店住所マスタ.住所NO=5))
            .AppendLine(" 	MKJ5.upd_datetime5, ")
            .AppendLine(" 	MKJ5.bikou15 ")
            .AppendLine(" FROM m_kameiten AS MK WITH(READCOMMITTED) ")
            .AppendLine(" LEFT OUTER JOIN ")
            .AppendLine(" 	(SELECT  ")
            .AppendLine(" 	kameiten_cd, ")
            .AppendLine(" 	jyuusyo_no, ")
            .AppendLine(" 	daihyousya_mei, ")
            .AppendLine(" 	add_nengetu, ")
            .AppendLine(" 	yuubin_no AS yuubin_no1, ")
            .AppendLine(" 	jyuusyo1 AS jyuusyo11, ")
            .AppendLine(" 	jyuusyo2 AS jyuusyo21, ")
            .AppendLine(" 	tel_no AS tel_no1, ")
            .AppendLine(" 	fax_no AS fax_no1, ")
            .AppendLine(" 	busyo_mei AS busyo_mei1, ")
            .AppendLine(" 	upd_datetime AS upd_datetime1, ")
            .AppendLine(" 	bikou1 AS bikou11, ")
            .AppendLine(" 	bikou2 AS bikou21 ")
            .AppendLine(" 	FROM m_kameiten_jyuusyo WITH(READCOMMITTED) ")
            .AppendLine(" 	WHERE jyuusyo_no = 1) AS MKJ1 ")
            .AppendLine(" ON MK.kameiten_cd = MKJ1.kameiten_cd ")
            .AppendLine(" LEFT OUTER JOIN ")
            .AppendLine(" 	(SELECT  ")
            .AppendLine(" 	kameiten_cd, ")
            .AppendLine(" 	jyuusyo_no, ")
            .AppendLine(" 	daihyousya_mei, ")
            .AppendLine(" 	(CASE WHEN seikyuusyo_flg = -1 THEN '○' ELSE '' END) AS seikyuusyo_flg2, ")
            .AppendLine(" 	(CASE WHEN hosyousyo_flg = -1 THEN '○' ELSE '' END) AS hosyousyo_flg2, ")
            .AppendLine(" 	(CASE WHEN kasi_hosyousyo_flg = -1 THEN '○' ELSE '' END) AS kasi_hosyousyo_flg2, ")
            .AppendLine(" 	(CASE WHEN teiki_kankou_flg = -1 THEN '○' ELSE '' END) AS teiki_kankou_flg2, ")
            .AppendLine(" 	(CASE WHEN hkks_flg = -1 THEN '○' ELSE '' END) AS hkks_flg2, ")
            .AppendLine(" 	(CASE WHEN koj_hkks_flg = -1 THEN '○' ELSE '' END) AS koj_hkks_flg2, ")
            .AppendLine(" 	(CASE WHEN kensa_hkks_flg = -1 THEN '○' ELSE '' END) AS kensa_hkks_flg2, ")
            .AppendLine(" 	yuubin_no AS yuubin_no2, ")
            .AppendLine(" 	jyuusyo1 AS jyuusyo12, ")
            .AppendLine(" 	jyuusyo2 AS jyuusyo22, ")
            .AppendLine(" 	tel_no AS tel_no2, ")
            .AppendLine(" 	fax_no AS fax_no2, ")
            .AppendLine(" 	busyo_mei AS busyo_mei2, ")
            .AppendLine(" 	upd_datetime AS upd_datetime2, ")
            .AppendLine(" 	bikou1 AS bikou12 ")
            .AppendLine(" 	FROM m_kameiten_jyuusyo WITH(READCOMMITTED) ")
            .AppendLine(" 	WHERE jyuusyo_no = 2) AS MKJ2 ")
            .AppendLine(" ON MK.kameiten_cd = MKJ2.kameiten_cd ")
            .AppendLine(" LEFT OUTER JOIN ")
            .AppendLine(" 	(SELECT  ")
            .AppendLine(" 	kameiten_cd, ")
            .AppendLine(" 	jyuusyo_no, ")
            .AppendLine(" 	daihyousya_mei, ")
            .AppendLine(" 	(CASE WHEN seikyuusyo_flg = -1 THEN '○' ELSE '' END) AS seikyuusyo_flg3, ")
            .AppendLine(" 	(CASE WHEN hosyousyo_flg = -1 THEN '○' ELSE '' END) AS hosyousyo_flg3, ")
            .AppendLine(" 	(CASE WHEN kasi_hosyousyo_flg = -1 THEN '○' ELSE '' END) AS kasi_hosyousyo_flg3, ")
            .AppendLine(" 	(CASE WHEN teiki_kankou_flg = -1 THEN '○' ELSE '' END) AS teiki_kankou_flg3, ")
            .AppendLine(" 	(CASE WHEN hkks_flg = -1 THEN '○' ELSE '' END) AS hkks_flg3, ")
            .AppendLine(" 	(CASE WHEN koj_hkks_flg = -1 THEN '○' ELSE '' END) AS koj_hkks_flg3, ")
            .AppendLine(" 	(CASE WHEN kensa_hkks_flg = -1 THEN '○' ELSE '' END) AS kensa_hkks_flg3, ")
            .AppendLine(" 	yuubin_no AS yuubin_no3, ")
            .AppendLine(" 	jyuusyo1 AS jyuusyo13, ")
            .AppendLine(" 	jyuusyo2 AS jyuusyo23, ")
            .AppendLine(" 	tel_no AS tel_no3, ")
            .AppendLine(" 	fax_no AS fax_no3, ")
            .AppendLine(" 	busyo_mei AS busyo_mei3, ")
            .AppendLine(" 	upd_datetime AS upd_datetime3, ")
            .AppendLine(" 	bikou1 AS bikou13 ")
            .AppendLine(" 	FROM m_kameiten_jyuusyo WITH(READCOMMITTED) ")
            .AppendLine(" 	WHERE jyuusyo_no = 3) AS MKJ3 ")
            .AppendLine(" ON MK.kameiten_cd = MKJ3.kameiten_cd ")
            .AppendLine(" LEFT OUTER JOIN ")
            .AppendLine(" 	(SELECT  ")
            .AppendLine(" 	kameiten_cd, ")
            .AppendLine(" 	jyuusyo_no, ")
            .AppendLine(" 	daihyousya_mei, ")
            .AppendLine(" 	(CASE WHEN seikyuusyo_flg = -1 THEN '○' ELSE '' END) AS seikyuusyo_flg4, ")
            .AppendLine(" 	(CASE WHEN hosyousyo_flg = -1 THEN '○' ELSE '' END) AS hosyousyo_flg4, ")
            .AppendLine(" 	(CASE WHEN kasi_hosyousyo_flg = -1 THEN '○' ELSE '' END) AS kasi_hosyousyo_flg4, ")
            .AppendLine(" 	(CASE WHEN teiki_kankou_flg = -1 THEN '○' ELSE '' END) AS teiki_kankou_flg4, ")
            .AppendLine(" 	(CASE WHEN hkks_flg = -1 THEN '○' ELSE '' END) AS hkks_flg4, ")
            .AppendLine(" 	(CASE WHEN koj_hkks_flg = -1 THEN '○' ELSE '' END) AS koj_hkks_flg4, ")
            .AppendLine(" 	(CASE WHEN kensa_hkks_flg = -1 THEN '○' ELSE '' END) AS kensa_hkks_flg4, ")
            .AppendLine(" 	yuubin_no AS yuubin_no4, ")
            .AppendLine(" 	jyuusyo1 AS jyuusyo14, ")
            .AppendLine(" 	jyuusyo2 AS jyuusyo24, ")
            .AppendLine(" 	tel_no AS tel_no4, ")
            .AppendLine(" 	fax_no AS fax_no4, ")
            .AppendLine(" 	busyo_mei AS busyo_mei4, ")
            .AppendLine(" 	upd_datetime AS upd_datetime4, ")
            .AppendLine(" 	bikou1 AS bikou14 ")
            .AppendLine(" 	FROM m_kameiten_jyuusyo WITH(READCOMMITTED) ")
            .AppendLine(" 	WHERE jyuusyo_no = 4) AS MKJ4 ")
            .AppendLine(" ON MK.kameiten_cd = MKJ4.kameiten_cd ")
            .AppendLine(" LEFT OUTER JOIN ")
            .AppendLine(" 	(SELECT  ")
            .AppendLine(" 	kameiten_cd, ")
            .AppendLine(" 	jyuusyo_no, ")
            .AppendLine(" 	daihyousya_mei, ")
            .AppendLine(" 	(CASE WHEN seikyuusyo_flg = -1 THEN '○' ELSE '' END) AS seikyuusyo_flg5, ")
            .AppendLine(" 	(CASE WHEN hosyousyo_flg = -1 THEN '○' ELSE '' END) AS hosyousyo_flg5, ")
            .AppendLine(" 	(CASE WHEN kasi_hosyousyo_flg = -1 THEN '○' ELSE '' END) AS kasi_hosyousyo_flg5, ")
            .AppendLine(" 	(CASE WHEN teiki_kankou_flg = -1 THEN '○' ELSE '' END) AS teiki_kankou_flg5, ")
            .AppendLine(" 	(CASE WHEN hkks_flg = -1 THEN '○' ELSE '' END) AS hkks_flg5, ")
            .AppendLine(" 	(CASE WHEN koj_hkks_flg = -1 THEN '○' ELSE '' END) AS koj_hkks_flg5, ")
            .AppendLine(" 	(CASE WHEN kensa_hkks_flg = -1 THEN '○' ELSE '' END) AS kensa_hkks_flg5, ")
            .AppendLine(" 	yuubin_no AS yuubin_no5, ")
            .AppendLine(" 	jyuusyo1 AS jyuusyo15, ")
            .AppendLine(" 	jyuusyo2 AS jyuusyo25, ")
            .AppendLine(" 	tel_no AS tel_no5, ")
            .AppendLine(" 	fax_no AS fax_no5, ")
            .AppendLine(" 	busyo_mei AS busyo_mei5, ")
            .AppendLine(" 	upd_datetime AS upd_datetime5, ")
            .AppendLine(" 	bikou1 AS bikou15 ")
            .AppendLine(" 	FROM m_kameiten_jyuusyo WITH(READCOMMITTED) ")
            .AppendLine(" 	WHERE jyuusyo_no = 5) AS MKJ5 ")
            .AppendLine(" ON MK.kameiten_cd = MKJ5.kameiten_cd ")
            .AppendLine(" LEFT OUTER JOIN m_todoufuken AS MT WITH(READCOMMITTED) ")
            .AppendLine(" ON MK.todouhuken_cd = MT.todouhuken_cd ")
            .AppendLine(" LEFT OUTER JOIN m_eigyousyo AS ME WITH(READCOMMITTED) ")
            .AppendLine(" ON MK.eigyousyo_cd = ME.eigyousyo_cd ")
            .AppendLine(" LEFT OUTER JOIN m_kameiten_jyuusyo AS MKJ WITH(READCOMMITTED) ")
            .AppendLine(" ON MK.kameiten_cd = MKJ.kameiten_cd ")
            .AppendLine(" WHERE ")
            .AppendLine("       1 = 1 ")
            '区分
            If dtParamKameitenInfo(0).kbn <> String.Empty Then
                Dim arrKbn() As String = dtParamKameitenInfo(0).kbn.Split(",")
                .AppendLine(" AND MK.kbn IN ( ")
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
            '退会した加盟店
            If dtParamKameitenInfo(0).taikai <> "1" Then
                .AppendLine(" AND MK.torikesi = 0 ")
            End If
            '加盟店名
            If dtParamKameitenInfo(0).kameiten_mei <> String.Empty Then
                .AppendLine(" AND (MK.kameiten_mei1 LIKE @kameiten_mei1 ")
                .AppendLine("      OR MK.kameiten_mei2 LIKE @kameiten_mei2) ")
                paramList.Add(MakeParam("@kameiten_mei1", SqlDbType.VarChar, 42, "%" & dtParamKameitenInfo(0).kameiten_mei & "%"))
                paramList.Add(MakeParam("@kameiten_mei2", SqlDbType.VarChar, 42, "%" & dtParamKameitenInfo(0).kameiten_mei & "%"))
            End If
            '加盟店コード
            If dtParamKameitenInfo(0).kameiten_cd_from <> String.Empty And dtParamKameitenInfo(0).kameiten_cd_to <> String.Empty Then
                .AppendLine(" AND MK.kameiten_cd BETWEEN @kameiten_cd_from AND @kameiten_cd_to ")
                paramList.Add(MakeParam("@kameiten_cd_from", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).kameiten_cd_from))
                paramList.Add(MakeParam("@kameiten_cd_to", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).kameiten_cd_to))
            ElseIf dtParamKameitenInfo(0).kameiten_cd_from <> String.Empty And dtParamKameitenInfo(0).kameiten_cd_to = String.Empty Then
                .AppendLine(" AND MK.kameiten_cd = @kameiten_cd_from ")
                paramList.Add(MakeParam("@kameiten_cd_from", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).kameiten_cd_from))
            End If
            '加盟店名カナ
            If dtParamKameitenInfo(0).kameiten_kana <> String.Empty Then
                .AppendLine(" AND (MK.tenmei_kana1 LIKE @kameiten_kana1 ")
                .AppendLine("      OR MK.tenmei_kana2 LIKE @kameiten_kana2) ")
                paramList.Add(MakeParam("@kameiten_kana1", SqlDbType.VarChar, 22, "%" & dtParamKameitenInfo(0).kameiten_kana & "%"))
                paramList.Add(MakeParam("@kameiten_kana2", SqlDbType.VarChar, 22, "%" & dtParamKameitenInfo(0).kameiten_kana & "%"))
            End If
            '営業所コード
            If dtParamKameitenInfo(0).eigyousyo_cd_from <> String.Empty And dtParamKameitenInfo(0).eigyousyo_cd_to <> String.Empty Then
                .AppendLine(" AND MK.eigyousyo_cd BETWEEN @eigyousyo_cd_from AND @eigyousyo_cd_to ")
                paramList.Add(MakeParam("@eigyousyo_cd_from", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).eigyousyo_cd_from))
                paramList.Add(MakeParam("@eigyousyo_cd_to", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).eigyousyo_cd_to))
            ElseIf dtParamKameitenInfo(0).eigyousyo_cd_from <> String.Empty And dtParamKameitenInfo(0).eigyousyo_cd_to = String.Empty Then
                .AppendLine(" AND MK.eigyousyo_cd = @eigyousyo_cd_from ")
                paramList.Add(MakeParam("@eigyousyo_cd_from", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).eigyousyo_cd_from))
            End If
            '電話番号
            If dtParamKameitenInfo(0).tel_no <> String.Empty Then
                .AppendLine(" AND REPLACE(MKJ.tel_no,'-','') LIKE @tel_no ")
                paramList.Add(MakeParam("@tel_no", SqlDbType.VarChar, 17, dtParamKameitenInfo(0).tel_no & "%"))
            End If
            '登録年月
            If dtParamKameitenInfo(0).touroku_nengetu_from <> String.Empty And dtParamKameitenInfo(0).touroku_nengetu_to <> String.Empty Then
                .AppendLine(" AND MKJ.add_nengetu BETWEEN @touroku_nengetu_from AND @touroku_nengetu_to ")
                paramList.Add(MakeParam("@touroku_nengetu_from", SqlDbType.VarChar, 7, dtParamKameitenInfo(0).touroku_nengetu_from))
                paramList.Add(MakeParam("@touroku_nengetu_to", SqlDbType.VarChar, 7, dtParamKameitenInfo(0).touroku_nengetu_to))
            ElseIf dtParamKameitenInfo(0).touroku_nengetu_from <> String.Empty And dtParamKameitenInfo(0).touroku_nengetu_to = String.Empty Then
                .AppendLine(" AND MKJ.add_nengetu = @touroku_nengetu_from ")
                paramList.Add(MakeParam("@touroku_nengetu_from", SqlDbType.VarChar, 7, dtParamKameitenInfo(0).touroku_nengetu_from))
            End If
            '都道府県
            If dtParamKameitenInfo(0).todouhuken_cd <> String.Empty Then
                .AppendLine(" AND MK.todouhuken_cd = @todouhuken_cd ")
                paramList.Add(MakeParam("@todouhuken_cd", SqlDbType.VarChar, 2, dtParamKameitenInfo(0).todouhuken_cd))
            End If
            '系列コード
            If dtParamKameitenInfo(0).keiretu_cd <> String.Empty Then
                .AppendLine(" AND MK.keiretu_cd = @keiretu_cd ")
                paramList.Add(MakeParam("@keiretu_cd", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).keiretu_cd))
            End If
            '送付先選択
            If dtParamKameitenInfo(0).soufusaki_kbn = String.Empty Then
                Dim soufusakiSb As New StringBuilder
                .AppendLine(" AND (")
                If dtParamKameitenInfo(0).jyuusyo_no <> String.Empty Then
                    soufusakiSb.Append(" MKJ.jyuusyo_no = 1 OR")
                End If
                If dtParamKameitenInfo(0).seikyuusyo_flg <> String.Empty Then
                    soufusakiSb.Append(" MKJ.seikyuusyo_flg = -1 OR")
                End If
                If dtParamKameitenInfo(0).hosyousyo_flg <> String.Empty Then
                    soufusakiSb.Append(" MKJ.hosyousyo_flg = -1 OR")
                End If
                If dtParamKameitenInfo(0).kasi_hosyousyo_flg <> String.Empty Then
                    soufusakiSb.Append(" MKJ.kasi_hosyousyo_flg = -1 OR")
                End If
                If dtParamKameitenInfo(0).teiki_kankou_flg <> String.Empty Then
                    soufusakiSb.Append(" MKJ.teiki_kankou_flg = -1 OR")
                End If
                If dtParamKameitenInfo(0).hkks_flg <> String.Empty Then
                    soufusakiSb.Append(" MKJ.hkks_flg = -1 OR")
                End If
                If dtParamKameitenInfo(0).koj_hkks_flg <> String.Empty Then
                    soufusakiSb.Append(" MKJ.koj_hkks_flg = -1 OR")
                End If
                If dtParamKameitenInfo(0).kensa_hkks_flg <> String.Empty Then
                    soufusakiSb.Append(" MKJ.kensa_hkks_flg = -1 OR")
                End If
                .AppendLine(soufusakiSb.ToString.Substring(0, soufusakiSb.ToString.Length - 2))
                .AppendLine(" ) ")
            End If
            .AppendLine(" ORDER BY ")
            .AppendLine("      MK.kameiten_cd ")
        End With

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, _
                    "JyusyoJyouhouCsv", paramList.ToArray)

        Return dsReturn

    End Function

    ''' <summary>加盟店情報一括取込情報CSVデータを取得する</summary>
    ''' <returns>加盟店情報一括取込情報CSVデータテーブル</returns>
    Public Function SelKameitenJyuusyoCsvInfo(ByVal dtParamKameitenInfo As KensakuSyoukaiInquiryDataSet.Param_KameitenInfoDataTable) As Data.DataTable
        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet
        'SQLコメント
        Dim commandTextSb As New StringBuilder
        ''パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine(" SELECT DISTINCT ")
            .AppendLine("   RIGHT(CONVERT(varchar(10),GETDATE(),112),4)+REPLACE(CONVERT(varchar(8),GETDATE(),108),':','') AS edi_jouhou_sakusei_dat, ")
            .AppendLine("   MK.kbn, ")
            .AppendLine("   MK.kameiten_cd, ")
            .AppendLine("   MK.torikesi, ")
            .AppendLine("   MK.hattyuu_teisi_flg, ")
            .AppendLine("   MK.kameiten_mei1, ")
            .AppendLine("   MK.tenmei_kana1, ")
            .AppendLine("   MK.kameiten_mei2, ")
            .AppendLine("   MK.tenmei_kana2, ")
            .AppendLine("   MK.builder_no, ")
            .AppendLine("   MK.kameiten_mei1 AS builder_mei, ")
            .AppendLine("   MK.keiretu_cd, ")
            .AppendLine("   MKR.keiretu_mei, ")
            .AppendLine("   MK.eigyousyo_cd, ")
            .AppendLine("   ME.eigyousyo_mei, ")
            .AppendLine("   MK.kameiten_seisiki_mei, ")
            .AppendLine("   MK.kameiten_seisiki_mei_kana, ")
            .AppendLine("   MK.todouhuken_cd, ")
            .AppendLine("   MT.todouhuken_mei, ")
            '================↓2013/03/06 車龍 407584 追加↓========================
            .AppendLine("   MK.nenkan_tousuu, ") '--年間棟数
            '================↑2013/03/06 車龍 407584 追加↑========================
            .AppendLine("   MK.fuho_syoumeisyo_flg, ")
            .AppendLine("   MK.fuho_syoumeisyo_kaisi_nengetu, ")
            .AppendLine("   MK.eigyou_tantousya_mei, ")
            '--営業担当者名 アカウントマスタ.氏名　を表示
            .AppendLine("   (select  simei from m_jiban_ninsyou left join  m_account on m_jiban_ninsyou.account_no=m_account.account_no where m_jiban_ninsyou.login_user_id=MK.eigyou_tantousya_mei) AS eigyou_tantousya_simei, ")
            .AppendLine("   MK.kyuu_eigyou_tantousya_mei, ")
            '--旧営業担当者名 アカウントマスタ.氏名　を表示	
            .AppendLine("   (select  simei from m_jiban_ninsyou left join  m_account on m_jiban_ninsyou.account_no=m_account.account_no where m_jiban_ninsyou.login_user_id=MK.kyuu_eigyou_tantousya_mei) AS kyuu_eigyou_tantousya_simei, ")
            '================↓2013/03/06 車龍 407584 追加↓========================
            .AppendLine("   MK.koj_uri_syubetsu, ") '--工事売上種別
            .AppendLine("   MM.meisyou AS koj_uri_syubetsu_mei, ") '--工事売上種別名
            .AppendLine("   MK.jiosaki_flg, ") '--JIO先フラグ
            '================↑2013/03/06 車龍 407584 追加↑========================
            .AppendLine("   MK.kaiyaku_haraimodosi_kkk, ")
            .AppendLine("   SYOUHIN1.syouhin_cd AS syouhin_cd1, ")
            .AppendLine("   SYOUHIN1.syouhin_mei AS syouhin_mei1, ")
            .AppendLine("   SYOUHIN2.syouhin_cd AS syouhin_cd2, ")
            .AppendLine("   SYOUHIN2.syouhin_mei AS syouhin_mei2, ")
            .AppendLine("   SYOUHIN3.syouhin_cd AS syouhin_cd3, ")
            .AppendLine("   SYOUHIN3.syouhin_mei AS syouhin_mei3, ")
            .AppendLine("   MK.tys_seikyuu_saki_kbn, ")
            .AppendLine("   MK.tys_seikyuu_saki_cd, ")
            .AppendLine("   MK.tys_seikyuu_saki_brc, ")
            .AppendLine("   (SELECT seikyuu_saki_mei FROM v_seikyuu_saki_info WHERE ")
            .AppendLine("           seikyuu_saki_cd=MK.tys_seikyuu_saki_cd AND ")
            .AppendLine("           seikyuu_saki_brc=MK.tys_seikyuu_saki_brc AND  ")
            .AppendLine("           seikyuu_saki_kbn=MK.tys_seikyuu_saki_kbn ")
            .AppendLine("   ) AS tys_seikyuu_saki_mei, ")
            .AppendLine("   MK.koj_seikyuu_saki_kbn, ")
            .AppendLine("   MK.koj_seikyuu_saki_cd, ")
            .AppendLine("   MK.koj_seikyuu_saki_brc, ")
            .AppendLine("   (SELECT seikyuu_saki_mei FROM v_seikyuu_saki_info WHERE ")
            .AppendLine("           seikyuu_saki_cd=MK.koj_seikyuu_saki_cd AND ")
            .AppendLine("           seikyuu_saki_brc=MK.koj_seikyuu_saki_brc AND  ")
            .AppendLine("           seikyuu_saki_kbn=MK.koj_seikyuu_saki_kbn ")
            .AppendLine("   ) AS koj_seikyuu_saki_mei, ")
            .AppendLine("   MK.hansokuhin_seikyuu_saki_kbn, ")
            .AppendLine("   MK.hansokuhin_seikyuu_saki_cd, ")
            .AppendLine("   MK.hansokuhin_seikyuu_saki_brc, ")
            .AppendLine("   (SELECT seikyuu_saki_mei FROM v_seikyuu_saki_info WHERE ")
            .AppendLine("           seikyuu_saki_cd=MK.hansokuhin_seikyuu_saki_cd AND ")
            .AppendLine("           seikyuu_saki_brc=MK.hansokuhin_seikyuu_saki_brc AND  ")
            .AppendLine("           seikyuu_saki_kbn=MK.hansokuhin_seikyuu_saki_kbn ")
            .AppendLine("   ) AS hansokuhin_seikyuu_saki_mei, ")
            .AppendLine("   MK.tatemono_seikyuu_saki_kbn, ")
            .AppendLine("   MK.tatemono_seikyuu_saki_cd, ")
            .AppendLine("   MK.tatemono_seikyuu_saki_brc, ")
            .AppendLine("   (SELECT seikyuu_saki_mei FROM v_seikyuu_saki_info WHERE ")
            .AppendLine("           seikyuu_saki_cd=MK.tatemono_seikyuu_saki_cd AND ")
            .AppendLine("           seikyuu_saki_brc=MK.tatemono_seikyuu_saki_brc AND  ")
            .AppendLine("           seikyuu_saki_kbn=MK.tatemono_seikyuu_saki_kbn ")
            .AppendLine("   ) AS tatemono_seikyuu_saki_mei, ")
            .AppendLine("   MK.seikyuu_saki_kbn5, ")
            .AppendLine("   MK.seikyuu_saki_cd5, ")
            .AppendLine("   MK.seikyuu_saki_brc5, ")
            .AppendLine("   (SELECT seikyuu_saki_mei FROM v_seikyuu_saki_info WHERE ")
            .AppendLine("           seikyuu_saki_cd=MK.seikyuu_saki_cd5 AND ")
            .AppendLine("           seikyuu_saki_brc=MK.seikyuu_saki_brc5 AND  ")
            .AppendLine("           seikyuu_saki_kbn=MK.seikyuu_saki_kbn5 ")
            .AppendLine("   ) AS seikyuu_saki5_mei, ")
            .AppendLine("   MK.seikyuu_saki_kbn6, ")
            .AppendLine("   MK.seikyuu_saki_cd6, ")
            .AppendLine("   MK.seikyuu_saki_brc6, ")
            .AppendLine("   (SELECT seikyuu_saki_mei FROM v_seikyuu_saki_info WHERE ")
            .AppendLine("           seikyuu_saki_cd=MK.seikyuu_saki_cd6 AND ")
            .AppendLine("           seikyuu_saki_brc=MK.seikyuu_saki_brc6 AND  ")
            .AppendLine("           seikyuu_saki_kbn=MK.seikyuu_saki_kbn6 ")
            .AppendLine("   ) AS seikyuu_saki6_mei, ")
            .AppendLine("   MK.seikyuu_saki_kbn7, ")
            .AppendLine("   MK.seikyuu_saki_cd7, ")
            .AppendLine("   MK.seikyuu_saki_brc7, ")
            .AppendLine("   (SELECT seikyuu_saki_mei FROM v_seikyuu_saki_info WHERE ")
            .AppendLine("           seikyuu_saki_cd=MK.seikyuu_saki_cd7 AND ")
            .AppendLine("           seikyuu_saki_brc=MK.seikyuu_saki_brc7 AND  ")
            .AppendLine("           seikyuu_saki_kbn=MK.seikyuu_saki_kbn7 ")
            .AppendLine("   ) AS seikyuu_saki7_mei, ")
            '================↓2013/03/06 車龍 407584 追加↓========================
            .AppendLine("   MK.hosyou_kikan, ") '--保証期間
            .AppendLine("   MK.hosyousyo_hak_umu, ") '--保証書発行有無
            '================↑2013/03/06 車龍 407584 追加↑========================
            .AppendLine("   MK.nyuukin_kakunin_jyouken, ")
            .AppendLine("   MK.nyuukin_kakunin_oboegaki, ")
            .AppendLine("   MK.tys_mitsyo_flg, ")
            .AppendLine("   MK.hattyuusyo_flg, ")
            .AppendLine("   MKJ.yuubin_no, ")
            .AppendLine("   MKJ.jyuusyo1, ")
            .AppendLine("   MKJ.jyuusyo2, ")
            .AppendLine("   MKJ.todouhuken_cd AS jyuusyo_cd, ")
            .AppendLine("   (SELECT todouhuken_mei FROM m_todoufuken WHERE ")
            .AppendLine("           todouhuken_cd=MKJ.todouhuken_cd ")
            .AppendLine("   ) AS jyuusyo_mei, ")
            .AppendLine("   MKJ.busyo_mei, ")
            .AppendLine("   MKJ.daihyousya_mei, ")
            .AppendLine("   MKJ.tel_no, ")
            .AppendLine("   MKJ.fax_no, ")
            '--申込担当者 加盟店住所マスタ.申込担当者
            .AppendLine("   MKJ.mail_address AS mail_address, ")
            .AppendLine("   MKJ.bikou1, ")
            .AppendLine("   MKJ.bikou2 ")
            '================↓2013/03/06 車龍 407584 追加↓========================
            .AppendLine("   ,REPLACE(ISNULL(CONVERT(VARCHAR(10),TTSS.add_date,112) + '_' + CONVERT(VARCHAR(10),TTSS.add_date,108),''),':','') AS add_date ") '--登録日
            .AppendLine("   ,TTSS.seikyuu_umu ") '--請求有無
            .AppendLine("   ,TTSS.syouhin_cd ") '--商品コード
            .AppendLine("   ,SUB_MS.syouhin_mei ") '--商品名
            .AppendLine("   ,TTSS.uri_gaku ") '--売上金額
            .AppendLine("   ,TTSS.koumuten_seikyuu_gaku ") '--工務店請求金額
            .AppendLine("   ,REPLACE(ISNULL(CONVERT(VARCHAR(10),TTSS.seikyuusyo_hak_date,112) + '_' + CONVERT(VARCHAR(10),TTSS.seikyuusyo_hak_date,108),''),':','') AS seikyuusyo_hak_date ") '--請求書発行日
            .AppendLine("   ,REPLACE(ISNULL(CONVERT(VARCHAR(10),TTSS.uri_date,112) + '_' + CONVERT(VARCHAR(10),TTSS.uri_date,108),''),':','') AS uri_date ") '--売上年月日
            .AppendLine("   ,TTSS.bikou ") '--備考
            '================↑2013/03/06 車龍 407584 追加↑========================
            '============2012/05/08 車龍 407553の対応 追加↓======================
            .AppendLine("   ,CASE ")
            .AppendLine("       WHEN  MK.upd_datetime IS NOT NULL THEN ")
            .AppendLine("           REPLACE(ISNULL(CONVERT(VARCHAR(10),MK.upd_datetime,112) + '_' + CONVERT(VARCHAR(10),MK.upd_datetime,108),''),':','') ") '更新日時
            .AppendLine("       ELSE ")
            .AppendLine("           REPLACE(ISNULL(CONVERT(VARCHAR(10),MK.add_datetime,112) + '_' + CONVERT(VARCHAR(10),MK.add_datetime,108),''),':','') ") '登録日時
            .AppendLine("       END AS kameiten_upd_datetime ") '--加盟店_更新日時
            .AppendLine("   ,REPLACE(ISNULL(CONVERT(VARCHAR(10),SYOUHIN1.upd_datetime,112) + '_' + CONVERT(VARCHAR(10),SYOUHIN1.upd_datetime,108),''),':','') AS tatouwari_upd_datetime_1 ") '--多棟割引_更新日時1
            .AppendLine("   ,REPLACE(ISNULL(CONVERT(VARCHAR(10),SYOUHIN2.upd_datetime,112) + '_' + CONVERT(VARCHAR(10),SYOUHIN2.upd_datetime,108),''),':','') AS tatouwari_upd_datetime_2 ") '--多棟割引_更新日時2
            .AppendLine("   ,REPLACE(ISNULL(CONVERT(VARCHAR(10),SYOUHIN3.upd_datetime,112) + '_' + CONVERT(VARCHAR(10),SYOUHIN3.upd_datetime,108),''),':','') AS tatouwari_upd_datetime_3 ") '--多棟割引_更新日時3
            .AppendLine("   ,CASE ")
            .AppendLine("       WHEN  MKJ.upd_datetime IS NOT NULL THEN ")
            .AppendLine("           REPLACE(ISNULL(CONVERT(VARCHAR(10),MKJ.upd_datetime,112) + '_' + CONVERT(VARCHAR(10),MKJ.upd_datetime,108),''),':','') ") '更新日時
            .AppendLine("       ELSE ")
            .AppendLine("           REPLACE(ISNULL(CONVERT(VARCHAR(10),MKJ.add_datetime,112) + '_' + CONVERT(VARCHAR(10),MKJ.add_datetime,108),''),':','') ") '登録日時
            .AppendLine("       END AS kameiten_jyuusyo_upd_datetime ") '--加盟店住所_更新日時
            '============2012/05/08 車龍 407553の対応 追加↑======================
            .AppendLine(" FROM  ")
            .AppendLine("    m_kameiten AS MK WITH(READCOMMITTED) ")
            .AppendLine(" LEFT JOIN  ")
            .AppendLine("    m_keiretu AS MKR WITH(READCOMMITTED) ")
            .AppendLine(" ON ")
            .AppendLine("    MKR.keiretu_cd = MK.keiretu_cd ")
            .AppendLine("    AND MKR.kbn = MK.kbn ")
            .AppendLine(" LEFT JOIN  ")
            .AppendLine("    m_eigyousyo AS ME WITH(READCOMMITTED) ")
            .AppendLine(" ON ")
            .AppendLine("    ME.eigyousyo_cd = MK.eigyousyo_cd ")
            .AppendLine(" LEFT JOIN  ")
            .AppendLine("    m_todoufuken AS MT WITH(READCOMMITTED) ")
            .AppendLine(" ON ")
            .AppendLine("    MT.todouhuken_cd = MK.todouhuken_cd ")
            .AppendLine(" LEFT OUTER JOIN ")
            .AppendLine("       (SELECT MTS.kameiten_cd, MTS.toukubun, MTS.syouhin_cd, MS.syouhin_mei  ")
            '============2012/05/08 車龍 407553の対応 追加↓======================
            .AppendLine("           ,CASE ")
            .AppendLine("               WHEN MTS.upd_datetime IS NOT NULL THEN ")
            .AppendLine("                   MTS.upd_datetime ") '--更新日時
            .AppendLine("               ELSE ")
            .AppendLine("                   MTS.add_datetime ") '--登録日時
            .AppendLine("               END AS upd_datetime ")
            '============2012/05/08 車龍 407553の対応 追加↑======================
            .AppendLine("       FROM m_tatouwaribiki_settei AS MTS WITH(READCOMMITTED) ")
            .AppendLine("       LEFT OUTER JOIN  ")
            .AppendLine("       m_syouhin AS MS WITH(READCOMMITTED) ")
            .AppendLine("       ON MTS.syouhin_cd = MS.syouhin_cd ")
            .AppendLine("       WHERE MTS.toukubun = 1) AS SYOUHIN1 ")
            .AppendLine("       ON MK.kameiten_cd = SYOUHIN1.kameiten_cd ")
            .AppendLine(" LEFT OUTER JOIN  ")
            .AppendLine("       (SELECT MTS.kameiten_cd, MTS.toukubun, MTS.syouhin_cd, MS.syouhin_mei  ")
            '============2012/05/08 車龍 407553の対応 追加↓======================
            .AppendLine("           ,CASE ")
            .AppendLine("               WHEN MTS.upd_datetime IS NOT NULL THEN ")
            .AppendLine("                   MTS.upd_datetime ") '--更新日時
            .AppendLine("               ELSE ")
            .AppendLine("                   MTS.add_datetime ") '--登録日時
            .AppendLine("               END AS upd_datetime ")
            '============2012/05/08 車龍 407553の対応 追加↑======================
            .AppendLine("       FROM m_tatouwaribiki_settei AS MTS WITH(READCOMMITTED) ")
            .AppendLine("       LEFT OUTER JOIN  ")
            .AppendLine("       m_syouhin AS MS WITH(READCOMMITTED) ")
            .AppendLine("       ON MTS.syouhin_cd = MS.syouhin_cd ")
            .AppendLine("       WHERE MTS.toukubun = 2) AS SYOUHIN2 ")
            .AppendLine("       ON MK.kameiten_cd = SYOUHIN2.kameiten_cd ")
            .AppendLine(" LEFT OUTER JOIN  ")
            .AppendLine("      	(SELECT MTS.kameiten_cd, MTS.toukubun, MTS.syouhin_cd, MS.syouhin_mei  ")
            '============2012/05/08 車龍 407553の対応 追加↓======================
            .AppendLine("           ,CASE ")
            .AppendLine("               WHEN MTS.upd_datetime IS NOT NULL THEN ")
            .AppendLine("                   MTS.upd_datetime ") '--更新日時
            .AppendLine("               ELSE ")
            .AppendLine("                   MTS.add_datetime ") '--登録日時
            .AppendLine("               END AS upd_datetime ")
            '============2012/05/08 車龍 407553の対応 追加↑======================
            .AppendLine("      	FROM m_tatouwaribiki_settei AS MTS WITH(READCOMMITTED) ")
            .AppendLine("      	LEFT OUTER JOIN ")
            .AppendLine("      	m_syouhin AS MS WITH(READCOMMITTED) ")
            .AppendLine("      	ON MTS.syouhin_cd = MS.syouhin_cd ")
            .AppendLine("      	WHERE MTS.toukubun = 3) AS SYOUHIN3 ")
            .AppendLine("      	ON MK.kameiten_cd = SYOUHIN3.kameiten_cd ")
            .AppendLine(" LEFT JOIN  ")
            .AppendLine("    m_kameiten_jyuusyo AS MKJ WITH(READCOMMITTED) ")
            .AppendLine(" ON ")
            .AppendLine("    MKJ.kameiten_cd = MK.kameiten_cd ")
            .AppendLine("    AND MKJ.jyuusyo_no = '1' ")
            '================↓2013/03/06 車龍 407584 追加↓========================
            .AppendLine(" LEFT JOIN ")
            .AppendLine("    m_meisyou AS MM ") '--名称マスタ ")
            .AppendLine(" ON ")
            .AppendLine("    MM.meisyou_syubetu = '55' ") '--工事売上種別 ")
            .AppendLine("    AND ")
            .AppendLine("    MM.code = MK.koj_uri_syubetsu ")
            .AppendLine(" LEFT JOIN ")
            .AppendLine("    t_tenbetu_syoki_seikyuu AS TTSS ") '--店別初期請求テーブル ")
            .AppendLine(" ON ")
            .AppendLine("    TTSS.mise_cd = MK.kameiten_cd  ")
            .AppendLine("    AND ")
            .AppendLine("    TTSS.bunrui_cd = '200' ")
            .AppendLine(" LEFT JOIN ")
            .AppendLine("    m_syouhin AS SUB_MS ") '--商品マスタ ")
            .AppendLine(" ON ")
            .AppendLine("    SUB_MS.syouhin_cd = TTSS.syouhin_cd ")
            '================↑2013/03/06 車龍 407584 追加↑========================
            .AppendLine(" WHERE ")
            .AppendLine("   1=1 ")
            '.AppendLine("   AND MK.kameiten_cd='00006'")
            '区分
            If dtParamKameitenInfo(0).kbn <> String.Empty Then
                Dim arrKbn() As String = dtParamKameitenInfo(0).kbn.Split(",")
                .AppendLine(" AND MK.kbn IN ( ")
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
            '退会した加盟店
            If dtParamKameitenInfo(0).taikai <> "1" Then
                .AppendLine(" AND MK.torikesi = 0 ")
            End If
            '加盟店名
            If dtParamKameitenInfo(0).kameiten_mei <> String.Empty Then
                .AppendLine(" AND (MK.kameiten_mei1 LIKE @kameiten_mei1 ")
                .AppendLine("      OR MK.kameiten_mei2 LIKE @kameiten_mei2) ")
                paramList.Add(MakeParam("@kameiten_mei1", SqlDbType.VarChar, 42, "%" & dtParamKameitenInfo(0).kameiten_mei & "%"))
                paramList.Add(MakeParam("@kameiten_mei2", SqlDbType.VarChar, 42, "%" & dtParamKameitenInfo(0).kameiten_mei & "%"))
            End If
            '加盟店コード
            If dtParamKameitenInfo(0).kameiten_cd_from <> String.Empty And dtParamKameitenInfo(0).kameiten_cd_to <> String.Empty Then
                .AppendLine(" AND MK.kameiten_cd BETWEEN @kameiten_cd_from AND @kameiten_cd_to ")
                paramList.Add(MakeParam("@kameiten_cd_from", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).kameiten_cd_from))
                paramList.Add(MakeParam("@kameiten_cd_to", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).kameiten_cd_to))
            ElseIf dtParamKameitenInfo(0).kameiten_cd_from <> String.Empty And dtParamKameitenInfo(0).kameiten_cd_to = String.Empty Then
                .AppendLine(" AND MK.kameiten_cd = @kameiten_cd_from ")
                paramList.Add(MakeParam("@kameiten_cd_from", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).kameiten_cd_from))
            End If
            '加盟店名カナ
            If dtParamKameitenInfo(0).kameiten_kana <> String.Empty Then
                .AppendLine(" AND (MK.tenmei_kana1 LIKE @kameiten_kana1 ")
                .AppendLine("      OR MK.tenmei_kana2 LIKE @kameiten_kana2) ")
                paramList.Add(MakeParam("@kameiten_kana1", SqlDbType.VarChar, 22, "%" & dtParamKameitenInfo(0).kameiten_kana & "%"))
                paramList.Add(MakeParam("@kameiten_kana2", SqlDbType.VarChar, 22, "%" & dtParamKameitenInfo(0).kameiten_kana & "%"))
            End If
            '営業所コード
            If dtParamKameitenInfo(0).eigyousyo_cd_from <> String.Empty And dtParamKameitenInfo(0).eigyousyo_cd_to <> String.Empty Then
                .AppendLine(" AND MK.eigyousyo_cd BETWEEN @eigyousyo_cd_from AND @eigyousyo_cd_to ")
                paramList.Add(MakeParam("@eigyousyo_cd_from", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).eigyousyo_cd_from))
                paramList.Add(MakeParam("@eigyousyo_cd_to", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).eigyousyo_cd_to))
            ElseIf dtParamKameitenInfo(0).eigyousyo_cd_from <> String.Empty And dtParamKameitenInfo(0).eigyousyo_cd_to = String.Empty Then
                .AppendLine(" AND MK.eigyousyo_cd = @eigyousyo_cd_from ")
                paramList.Add(MakeParam("@eigyousyo_cd_from", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).eigyousyo_cd_from))
            End If
            '電話番号
            If dtParamKameitenInfo(0).tel_no <> String.Empty Then
                .AppendLine(" AND REPLACE(MKJ.tel_no,'-','') LIKE @tel_no ")
                paramList.Add(MakeParam("@tel_no", SqlDbType.VarChar, 17, dtParamKameitenInfo(0).tel_no & "%"))
            End If
            '登録年月
            If dtParamKameitenInfo(0).touroku_nengetu_from <> String.Empty And dtParamKameitenInfo(0).touroku_nengetu_to <> String.Empty Then
                .AppendLine(" AND MKJ.add_nengetu BETWEEN @touroku_nengetu_from AND @touroku_nengetu_to ")
                paramList.Add(MakeParam("@touroku_nengetu_from", SqlDbType.VarChar, 7, dtParamKameitenInfo(0).touroku_nengetu_from))
                paramList.Add(MakeParam("@touroku_nengetu_to", SqlDbType.VarChar, 7, dtParamKameitenInfo(0).touroku_nengetu_to))
            ElseIf dtParamKameitenInfo(0).touroku_nengetu_from <> String.Empty And dtParamKameitenInfo(0).touroku_nengetu_to = String.Empty Then
                .AppendLine(" AND MKJ.add_nengetu = @touroku_nengetu_from ")
                paramList.Add(MakeParam("@touroku_nengetu_from", SqlDbType.VarChar, 7, dtParamKameitenInfo(0).touroku_nengetu_from))
            End If
            '都道府県
            If dtParamKameitenInfo(0).todouhuken_cd <> String.Empty Then
                .AppendLine(" AND MK.todouhuken_cd = @todouhuken_cd ")
                paramList.Add(MakeParam("@todouhuken_cd", SqlDbType.VarChar, 2, dtParamKameitenInfo(0).todouhuken_cd))
            End If
            '系列コード
            If dtParamKameitenInfo(0).keiretu_cd <> String.Empty Then
                .AppendLine(" AND MK.keiretu_cd = @keiretu_cd ")
                paramList.Add(MakeParam("@keiretu_cd", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).keiretu_cd))
            End If
            '送付先選択
            If dtParamKameitenInfo(0).soufusaki_kbn = String.Empty Then
                Dim soufusakiSb As New StringBuilder
                .AppendLine(" AND (")
                If dtParamKameitenInfo(0).jyuusyo_no <> String.Empty Then
                    soufusakiSb.Append(" MKJ.jyuusyo_no = 1 OR")
                End If
                If dtParamKameitenInfo(0).seikyuusyo_flg <> String.Empty Then
                    soufusakiSb.Append(" MKJ.seikyuusyo_flg = -1 OR")
                End If
                If dtParamKameitenInfo(0).hosyousyo_flg <> String.Empty Then
                    soufusakiSb.Append(" MKJ.hosyousyo_flg = -1 OR")
                End If
                If dtParamKameitenInfo(0).kasi_hosyousyo_flg <> String.Empty Then
                    soufusakiSb.Append(" MKJ.kasi_hosyousyo_flg = -1 OR")
                End If
                If dtParamKameitenInfo(0).teiki_kankou_flg <> String.Empty Then
                    soufusakiSb.Append(" MKJ.teiki_kankou_flg = -1 OR")
                End If
                If dtParamKameitenInfo(0).hkks_flg <> String.Empty Then
                    soufusakiSb.Append(" MKJ.hkks_flg = -1 OR")
                End If
                If dtParamKameitenInfo(0).koj_hkks_flg <> String.Empty Then
                    soufusakiSb.Append(" MKJ.koj_hkks_flg = -1 OR")
                End If
                If dtParamKameitenInfo(0).kensa_hkks_flg <> String.Empty Then
                    soufusakiSb.Append(" MKJ.kensa_hkks_flg = -1 OR")
                End If
                .AppendLine(soufusakiSb.ToString.Substring(0, soufusakiSb.ToString.Length - 2))
                .AppendLine(" ) ")
            End If
            .AppendLine(" ORDER BY ")
            .AppendLine("      MK.kameiten_cd ")
        End With

        'EDI情報作成日
        'paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, ""))

        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsReturn, "KameitenJyuusyo", paramList.ToArray)

        Return dsReturn.Tables(0)
    End Function

    ''' <summary>
    ''' 取引条件格納先管理マスタより、格納先ファイルパスを取得する
    ''' </summary>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>李宇 大連情報システム部 2013.03.22</history>
    Public Function SelKakunousakiFilePassJ(ByVal strKbn As String, ByVal strKameitenCd As String) As Data.DataTable

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT")
            .AppendLine("   MTJDK.kakunousaki_file_pass AS kakunousaki_file_pass")
            .AppendLine("FROM")
            .AppendLine("   m_torihiki_jyouken_db_kanri AS MTJDK WITH(READCOMMITTED)")
            .AppendLine("WHERE")
            .AppendLine("   MTJDK.kbn = @kbn")
            .AppendLine("   AND MTJDK.kameiten_cd_from <= @kameiten_cd")
            .AppendLine("   AND MTJDK.kameiten_cd_to >= @kameiten_cd")
        End With

        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, strKbn))
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))

        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsReturn, "KakunousakiFilePassJ", paramList.ToArray)

        Return dsReturn.Tables(0)
    End Function

    ''' <summary>
    ''' 調査カード格納先管理マスタより、格納先ファイルパスを取得する
    ''' </summary>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>李宇 大連情報システム部 2013.03.22</history>
    Public Function SelKakunousakiFilePassC(ByVal strKbn As String, ByVal strKameitenCd As String) As Data.DataTable

        'DataSetインスタンスの生成
        Dim dsReturn As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT")
            .AppendLine("   MTCDK.kakunousaki_file_pass AS kakunousaki_file_pass")
            .AppendLine("FROM")
            .AppendLine("   m_tyousa_card_db_kanri AS MTCDK WITH(READCOMMITTED)")
            .AppendLine("WHERE")
            .AppendLine("   MTCDK.kbn = @kbn")
            .AppendLine("   AND MTCDK.kameiten_cd_from <= @kameiten_cd")
            .AppendLine("   AND MTCDK.kameiten_cd_to >= @kameiten_cd")
        End With

        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, strKbn))
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))

        '検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsReturn, "KakunousakiFilePassC", paramList.ToArray)

        Return dsReturn.Tables(0)
    End Function

End Class

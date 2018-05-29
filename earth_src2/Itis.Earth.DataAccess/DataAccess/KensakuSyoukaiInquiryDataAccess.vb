Imports System.Data.SqlClient
Imports System.Text
Imports Itis.ApplicationBlocks.Data.SQLHelper

''' <summary>�����X���������Ɖ��</summary>
''' <remarks>�����X�����Ɖ�@�\��񋟂���</remarks>
''' <history>
''' <para>2009/07/15�@�t��(��A���V�X�e����)�@�V�K�쐬</para>
''' </history>
Public Class KensakuSyoukaiInquiryDataAccess

    Private connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>�����X���f�[�^���擾����</summary>
    ''' <param name="dtParamKameitenInfo">�p�����[�^</param>
    ''' <returns>�����X���f�[�^�e�[�u��</returns>
    Public Function SelKameitenInfo(ByVal dtParamKameitenInfo As KensakuSyoukaiInquiryDataSet.Param_KameitenInfoDataTable) _
           As KensakuSyoukaiInquiryDataSet.KameitenInfoTableDataTable

        'DataSet�C���X�^���X�̐���
        Dim dsKensakuSyoukaiInquiry As New KensakuSyoukaiInquiryDataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
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
            .AppendLine("      (CASE WHEN MKK.todouhuken_mei = '�F' THEN '' ELSE MKK.todouhuken_mei END) AS todouhuken_mei, ")
            .AppendLine("      MKK.keiretu_cd, ")
            .AppendLine("      MKK.eigyousyo_cd, ")
            .AppendLine("      MKK.builder_no, ")
            .AppendLine("      MKJ0.daihyousya_mei, ")
            .AppendLine("      MKJ0.tel_no ")
            .AppendLine(" FROM ")
            .AppendLine(" (SELECT DISTINCT ")
            '================2012/03/28 �ԗ� 405721�Č��̑Ή� �C����==================================
            '.AppendLine("      MK.torikesi, ")
            .AppendLine("      CASE ")
            .AppendLine("          WHEN MK.torikesi = 0 THEN ")
            .AppendLine("              '0' ")
            .AppendLine("          ELSE ")
            .AppendLine("              CONVERT(VARCHAR(10),MK.torikesi) + ':' + ISNULL(MKM.meisyou,'') ")
            .AppendLine("          END AS torikesi, ")
            '================2012/03/28 �ԗ� 405721�Č��̑Ή� �C����==================================
            .AppendLine("      MK.kbn, ")
            .AppendLine("      MK.kameiten_cd, ")
            .AppendLine("      MK.kameiten_mei1, ")
            .AppendLine("      MK.tenmei_kana1, ")
            .AppendLine("      (CASE WHEN MK.kameiten_mei2 <> '' THEN MK.kameiten_mei2 ELSE tenmei_kana2 END) AS kameiten_mei2, ")
            .AppendLine("      ISNULL(MK.todouhuken_cd,'') + '�F' + ISNULL(MT.todouhuken_mei,'') AS todouhuken_mei, ")
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
            '================2012/03/28 �ԗ� 405721�Č��̑Ή� �ǉ���==================================
            .AppendLine(" LEFT OUTER JOIN m_kakutyou_meisyou AS MKM ") '--�g�����̃}�X�^
            .AppendLine(" ON MK.torikesi = MKM.code ")
            .AppendLine(" AND MKM.meisyou_syubetu = 56 ")
            '================2012/03/28 �ԗ� 405721�Č��̑Ή� �ǉ���==================================
            .AppendLine(" WHERE ")
            .AppendLine("       1 = 1 ")
            '�敪
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
            '�މ�������X
            If dtParamKameitenInfo(0).taikai <> "1" Then
                .AppendLine(" AND MK.torikesi = 0 ")
            End If
            '�����X��
            If dtParamKameitenInfo(0).kameiten_mei <> String.Empty Then
                .AppendLine(" AND (MK.kameiten_mei1 LIKE @kameiten_mei1 ")
                .AppendLine("      OR MK.kameiten_mei2 LIKE @kameiten_mei2) ")
                paramList.Add(MakeParam("@kameiten_mei1", SqlDbType.VarChar, 42, "%" & dtParamKameitenInfo(0).kameiten_mei & "%"))
                paramList.Add(MakeParam("@kameiten_mei2", SqlDbType.VarChar, 42, "%" & dtParamKameitenInfo(0).kameiten_mei & "%"))
            End If
            '�����X�R�[�h
            If dtParamKameitenInfo(0).kameiten_cd_from <> String.Empty And dtParamKameitenInfo(0).kameiten_cd_to <> String.Empty Then
                .AppendLine(" AND MK.kameiten_cd BETWEEN @kameiten_cd_from AND @kameiten_cd_to ")
                paramList.Add(MakeParam("@kameiten_cd_from", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).kameiten_cd_from))
                paramList.Add(MakeParam("@kameiten_cd_to", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).kameiten_cd_to))
            ElseIf dtParamKameitenInfo(0).kameiten_cd_from <> String.Empty And dtParamKameitenInfo(0).kameiten_cd_to = String.Empty Then
                .AppendLine(" AND MK.kameiten_cd = @kameiten_cd_from ")
                paramList.Add(MakeParam("@kameiten_cd_from", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).kameiten_cd_from))
            End If
            '�����X���J�i
            If dtParamKameitenInfo(0).kameiten_kana <> String.Empty Then
                .AppendLine(" AND (MK.tenmei_kana1 LIKE @kameiten_kana1 ")
                .AppendLine("      OR MK.tenmei_kana2 LIKE @kameiten_kana2) ")
                paramList.Add(MakeParam("@kameiten_kana1", SqlDbType.VarChar, 22, "%" & dtParamKameitenInfo(0).kameiten_kana & "%"))
                paramList.Add(MakeParam("@kameiten_kana2", SqlDbType.VarChar, 22, "%" & dtParamKameitenInfo(0).kameiten_kana & "%"))
            End If
            '�c�Ə��R�[�h
            If dtParamKameitenInfo(0).eigyousyo_cd_from <> String.Empty And dtParamKameitenInfo(0).eigyousyo_cd_to <> String.Empty Then
                .AppendLine(" AND MK.eigyousyo_cd BETWEEN @eigyousyo_cd_from AND @eigyousyo_cd_to ")
                paramList.Add(MakeParam("@eigyousyo_cd_from", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).eigyousyo_cd_from))
                paramList.Add(MakeParam("@eigyousyo_cd_to", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).eigyousyo_cd_to))
            ElseIf dtParamKameitenInfo(0).eigyousyo_cd_from <> String.Empty And dtParamKameitenInfo(0).eigyousyo_cd_to = String.Empty Then
                .AppendLine(" AND MK.eigyousyo_cd = @eigyousyo_cd_from ")
                paramList.Add(MakeParam("@eigyousyo_cd_from", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).eigyousyo_cd_from))
            End If
            '�d�b�ԍ�
            If dtParamKameitenInfo(0).tel_no <> String.Empty Then
                .AppendLine(" AND REPLACE(MKJ.tel_no,'-','') LIKE @tel_no ")
                paramList.Add(MakeParam("@tel_no", SqlDbType.VarChar, 17, dtParamKameitenInfo(0).tel_no & "%"))
            End If
            '�o�^�N��
            If dtParamKameitenInfo(0).touroku_nengetu_from <> String.Empty And dtParamKameitenInfo(0).touroku_nengetu_to <> String.Empty Then
                .AppendLine(" AND MKJ.add_nengetu BETWEEN @touroku_nengetu_from AND @touroku_nengetu_to ")
                paramList.Add(MakeParam("@touroku_nengetu_from", SqlDbType.VarChar, 7, dtParamKameitenInfo(0).touroku_nengetu_from))
                paramList.Add(MakeParam("@touroku_nengetu_to", SqlDbType.VarChar, 7, dtParamKameitenInfo(0).touroku_nengetu_to))
            ElseIf dtParamKameitenInfo(0).touroku_nengetu_from <> String.Empty And dtParamKameitenInfo(0).touroku_nengetu_to = String.Empty Then
                .AppendLine(" AND MKJ.add_nengetu = @touroku_nengetu_from ")
                paramList.Add(MakeParam("@touroku_nengetu_from", SqlDbType.VarChar, 7, dtParamKameitenInfo(0).touroku_nengetu_from))
            End If
            '�s���{��
            If dtParamKameitenInfo(0).todouhuken_cd <> String.Empty Then
                .AppendLine(" AND MK.todouhuken_cd = @todouhuken_cd ")
                paramList.Add(MakeParam("@todouhuken_cd", SqlDbType.VarChar, 2, dtParamKameitenInfo(0).todouhuken_cd))
            End If
            '�n��R�[�h
            If dtParamKameitenInfo(0).keiretu_cd <> String.Empty Then
                .AppendLine(" AND MK.keiretu_cd = @keiretu_cd ")
                paramList.Add(MakeParam("@keiretu_cd", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).keiretu_cd))
            End If
            '���t��I��
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

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsKensakuSyoukaiInquiry, _
                    dsKensakuSyoukaiInquiry.KameitenInfoTable.TableName, paramList.ToArray)

        Return dsKensakuSyoukaiInquiry.KameitenInfoTable

    End Function

    ''' <summary>�����X���f�[�^�����擾����</summary>
    ''' <param name="dtParamKameitenInfo">�p�����[�^</param>
    ''' <returns>�����X���f�[�^��</returns>
    Public Function SelKameitenInfoCount(ByVal dtParamKameitenInfo As KensakuSyoukaiInquiryDataSet.Param_KameitenInfoDataTable) _
            As Integer
        'DataSet�C���X�^���X�̐���
        Dim dsKensakuSyoukaiInquiry As New KensakuSyoukaiInquiryDataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
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
            '�敪
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
            '�މ�������X
            If dtParamKameitenInfo(0).taikai <> "1" Then
                .AppendLine(" AND MK.torikesi = 0 ")
            End If
            '�����X��
            If dtParamKameitenInfo(0).kameiten_mei <> String.Empty Then
                .AppendLine(" AND (MK.kameiten_mei1 LIKE @kameiten_mei1 ")
                .AppendLine("      OR MK.kameiten_mei2 LIKE @kameiten_mei2) ")
                paramList.Add(MakeParam("@kameiten_mei1", SqlDbType.VarChar, 42, "%" & dtParamKameitenInfo(0).kameiten_mei & "%"))
                paramList.Add(MakeParam("@kameiten_mei2", SqlDbType.VarChar, 42, "%" & dtParamKameitenInfo(0).kameiten_mei & "%"))
            End If
            '�����X�R�[�h
            If dtParamKameitenInfo(0).kameiten_cd_from <> String.Empty And dtParamKameitenInfo(0).kameiten_cd_to <> String.Empty Then
                .AppendLine(" AND MK.kameiten_cd BETWEEN @kameiten_cd_from AND @kameiten_cd_to ")
                paramList.Add(MakeParam("@kameiten_cd_from", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).kameiten_cd_from))
                paramList.Add(MakeParam("@kameiten_cd_to", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).kameiten_cd_to))
            ElseIf dtParamKameitenInfo(0).kameiten_cd_from <> String.Empty And dtParamKameitenInfo(0).kameiten_cd_to = String.Empty Then
                .AppendLine(" AND MK.kameiten_cd = @kameiten_cd_from ")
                paramList.Add(MakeParam("@kameiten_cd_from", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).kameiten_cd_from))
            End If
            '�����X���J�i
            If dtParamKameitenInfo(0).kameiten_kana <> String.Empty Then
                .AppendLine(" AND (MK.tenmei_kana1 LIKE @kameiten_kana1 ")
                .AppendLine("      OR MK.tenmei_kana2 LIKE @kameiten_kana2) ")
                paramList.Add(MakeParam("@kameiten_kana1", SqlDbType.VarChar, 22, "%" & dtParamKameitenInfo(0).kameiten_kana & "%"))
                paramList.Add(MakeParam("@kameiten_kana2", SqlDbType.VarChar, 22, "%" & dtParamKameitenInfo(0).kameiten_kana & "%"))
            End If
            '�c�Ə��R�[�h
            If dtParamKameitenInfo(0).eigyousyo_cd_from <> String.Empty And dtParamKameitenInfo(0).eigyousyo_cd_to <> String.Empty Then
                .AppendLine(" AND MK.eigyousyo_cd BETWEEN @eigyousyo_cd_from AND @eigyousyo_cd_to ")
                paramList.Add(MakeParam("@eigyousyo_cd_from", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).eigyousyo_cd_from))
                paramList.Add(MakeParam("@eigyousyo_cd_to", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).eigyousyo_cd_to))
            ElseIf dtParamKameitenInfo(0).eigyousyo_cd_from <> String.Empty And dtParamKameitenInfo(0).eigyousyo_cd_to = String.Empty Then
                .AppendLine(" AND MK.eigyousyo_cd = @eigyousyo_cd_from ")
                paramList.Add(MakeParam("@eigyousyo_cd_from", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).eigyousyo_cd_from))
            End If
            '�d�b�ԍ�
            If dtParamKameitenInfo(0).tel_no <> String.Empty Then
                .AppendLine(" AND REPLACE(MKJ.tel_no,'-','') LIKE @tel_no ")
                paramList.Add(MakeParam("@tel_no", SqlDbType.VarChar, 17, dtParamKameitenInfo(0).tel_no & "%"))
            End If
            '�o�^�N��
            If dtParamKameitenInfo(0).touroku_nengetu_from <> String.Empty And dtParamKameitenInfo(0).touroku_nengetu_to <> String.Empty Then
                .AppendLine(" AND MKJ.add_nengetu BETWEEN @touroku_nengetu_from AND @touroku_nengetu_to ")
                paramList.Add(MakeParam("@touroku_nengetu_from", SqlDbType.VarChar, 7, dtParamKameitenInfo(0).touroku_nengetu_from))
                paramList.Add(MakeParam("@touroku_nengetu_to", SqlDbType.VarChar, 7, dtParamKameitenInfo(0).touroku_nengetu_to))
            ElseIf dtParamKameitenInfo(0).touroku_nengetu_from <> String.Empty And dtParamKameitenInfo(0).touroku_nengetu_to = String.Empty Then
                .AppendLine(" AND MKJ.add_nengetu = @touroku_nengetu_from ")
                paramList.Add(MakeParam("@touroku_nengetu_from", SqlDbType.VarChar, 7, dtParamKameitenInfo(0).touroku_nengetu_from))
            End If
            '�s���{��
            If dtParamKameitenInfo(0).todouhuken_cd <> String.Empty Then
                .AppendLine(" AND MK.todouhuken_cd = @todouhuken_cd ")
                paramList.Add(MakeParam("@todouhuken_cd", SqlDbType.VarChar, 2, dtParamKameitenInfo(0).todouhuken_cd))
            End If
            '�n��R�[�h
            If dtParamKameitenInfo(0).keiretu_cd <> String.Empty Then
                .AppendLine(" AND MK.keiretu_cd = @keiretu_cd ")
                paramList.Add(MakeParam("@keiretu_cd", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).keiretu_cd))
            End If
            '���t��I��
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

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsKensakuSyoukaiInquiry, _
                    dsKensakuSyoukaiInquiry.KameitenInfoCountTable.TableName, paramList.ToArray)

        Return dsKensakuSyoukaiInquiry.KameitenInfoCountTable(0).kameiten_cd_count

    End Function

    ''' <summary>�����X��{���CSV�f�[�^���擾����</summary>
    ''' <param name="dtParamKameitenInfo">�p�����[�^</param>
    ''' <returns>�����X��{���CSV�f�[�^�e�[�u��</returns>
    Public Function SelKihonJyouhouCsvInfo(ByVal dtParamKameitenInfo As KensakuSyoukaiInquiryDataSet.Param_KameitenInfoDataTable) _
            As Data.DataSet

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine(" SELECT DISTINCT ")
            .AppendLine("	MK.kbn, ") '--�敪(�����X�}�X�^.�敪) ")
            .AppendLine("	MK.kameiten_cd, ") '--�����X����(�����X�}�X�^.�����X����) ")
            .AppendLine("	MK.torikesi, ") '--���(�����X�}�X�^.���) ")
            .AppendLine("	MK.kameiten_mei1, ") '--�����X��1(�����X�}�X�^.�����X��1) ")
            .AppendLine("	MK.tenmei_kana1, ") '--�X����1(�����X�}�X�^.�X����1) ")
            .AppendLine("	MK.kameiten_mei2, ") '--�����X��2(�����X�}�X�^.�����X��2) ")
            .AppendLine("	MK.tenmei_kana2, ") '--�X����2(�����X�}�X�^.�X����2) ")
            .AppendLine("	MK.eigyousyo_cd, ") '--�c�Ə�����(�����X�}�X�^.�c�Ə�����) ")
            .AppendLine("	MK.keiretu_cd, ") '--�n����(�����X�}�X�^.�n����) ")
            .AppendLine("	MK.th_kasi_cd, ") '--TH���r����(�����X�}�X�^.TH���r����) ")
            .AppendLine("	MK.builder_no, ") '--����ްNO(�����X�}�X�^.����ްNO) ")
            .AppendLine("	MK.hattyuu_teisi_flg, ") '--������~�t���O(�����X�}�X�^.������~�t���O) ")
            .AppendLine("	MK.todouhuken_cd, ") '--�s���{������(�����X�}�X�^.�s���{������) ")
            .AppendLine("	MK.nenkan_tousuu, ") '--�N�ԓ���(�����X�}�X�^.�N�ԓ���) ")
            .AppendLine("	MK.eigyou_tantousya_mei, ") '--�c�ƒS����(�����X�}�X�^.�c�ƒS����) ")
            .AppendLine("	MK.hikitugi_kanryou_date, ") '--���p������(�����X�}�X�^.���p������) ")
            .AppendLine("	MK.kyuu_eigyou_tantousya_mei, ") '--���c�ƒS����(�����X�}�X�^.���c�ƒS����) ")
            .AppendLine("	MK.jisin_hosyou_flg, ") '--�n�k�⏞FLG(�����X�}�X�^.�n�k�⏞FLG) ")
            .AppendLine("	MK.jisin_hosyou_add_date, ") '--�n�k�⏞�o�^��(�����X�}�X�^.�n�k�⏞�o�^��) ")
            .AppendLine("	MK.fuho_syoumeisyo_flg, ") '--�t�ۏؖ�FLG(�����X�}�X�^.�t�ۏؖ���FLG) ")
            .AppendLine("	MK.fuho_syoumeisyo_kaisi_nengetu, ") '--�t�ۏؖ����J�n�N��(�����X�}�X�^.�t�ۏؖ����J�n�N��) ")
            .AppendLine("	MK.koj_uri_syubetsu, ") '--�H��������(�����X�}�X�^.�H��������) ")
            .AppendLine("	MK.koj_support_system, ") '--�H���T�|�[�g�V�X�e��(�����X�}�X�^.�H���T�|�[�g�V�X�e��) ")
            .AppendLine("	MK.jiosaki_flg, ") '--JIO��FLG(�����X�}�X�^.JIO��FLG) ")
            .AppendLine("	MK.kameiten_seisiki_mei, ") '--��������(�����X�}�X�^.�����X������) ")
            .AppendLine("	MK.kameiten_seisiki_mei_kana, ") '--��������2(�����X�}�X�^.�����X������2) ")
            .AppendLine("	MK.kaiyaku_haraimodosi_kkk, ") '--��񕥖߉��i(�����X�}�X�^.��񕥖߉��i) ")
            .AppendLine("	SYOUHIN1.syouhin_cd AS syouhin_cd1, ") '--���敪1���i����(���������}�X�^.���i�R�[�h(���������}�X�^.���敪=1)) ")
            .AppendLine("	SYOUHIN1.syouhin_mei AS syouhin_mei1, ") '--���敪1���i��(���i�}�X�^.���i��) ")
            .AppendLine("	SYOUHIN2.syouhin_cd AS syouhin_cd2, ") '--���敪2���i����(���������}�X�^.���i�R�[�h(���������}�X�^.���敪=2)) ")
            .AppendLine("	SYOUHIN2.syouhin_mei AS syouhin_mei2, ") '--���敪2���i��(���i�}�X�^.���i��) ")
            .AppendLine("	SYOUHIN3.syouhin_cd AS syouhin_cd3, ") '--���敪3���i����(���������}�X�^.���i�R�[�h(���������}�X�^.���敪=3)) ")
            .AppendLine("	SYOUHIN3.syouhin_mei AS syouhin_mei3, ") '--���敪3���i��(���i�}�X�^.���i��) ")
            .AppendLine("	MK.tys_seikyuu_saki_kbn, ") '--���������� �敪(�����X�}�X�^.����������敪) ")
            .AppendLine("	MK.tys_seikyuu_saki_cd, ") '--����������R�[�h(�����X�}�X�^.����������R�[�h) ")
            .AppendLine("	MK.tys_seikyuu_saki_brc, ") '--����������}��(�����X�}�X�^.����������}��) ")
            .AppendLine("	(SELECT seikyuu_saki_mei FROM v_seikyuu_saki_info WHERE ")
            .AppendLine("		seikyuu_saki_cd=MK.tys_seikyuu_saki_cd AND ")
            .AppendLine("		seikyuu_saki_brc=MK.tys_seikyuu_saki_brc AND  ")
            .AppendLine("		seikyuu_saki_kbn=MK.tys_seikyuu_saki_kbn ")
            .AppendLine("	) AS tys_seikyuu_saki_mei, ") '--���������於(�����X�}�X�^.�����X���A������Ѓ}�X�^.������Ж��A�c�Ə��}�X�^.�c�Ə��� (�敪�E�R�[�h�ɂ�蕪��) ) ")
            .AppendLine("	MK.tys_seikyuu_sime_date, ") '--�����������ߓ�(�����X�}�X�^.�����������ߓ�) ")
            .AppendLine("	MK.koj_seikyuu_saki_kbn, ") '--�H�������� �敪(�����X�}�X�^.�H��������敪) ")
            .AppendLine("	MK.koj_seikyuu_saki_cd, ") '--�H��������R�[�h(�����X�}�X�^.�H��������R�[�h) ")
            .AppendLine("	MK.koj_seikyuu_saki_brc, ") '--�H��������}��(�����X�}�X�^.�H��������}��) ")
            .AppendLine("	(SELECT seikyuu_saki_mei FROM v_seikyuu_saki_info WHERE ")
            .AppendLine("		seikyuu_saki_cd=MK.koj_seikyuu_saki_cd AND ")
            .AppendLine("		seikyuu_saki_brc=MK.koj_seikyuu_saki_brc AND  ")
            .AppendLine("		seikyuu_saki_kbn=MK.koj_seikyuu_saki_kbn ")
            .AppendLine("	) AS koj_seikyuu_saki_mei, ") '--�H�������於(�����X�}�X�^.�����X���A������Ѓ}�X�^.������Ж��A�c�Ə��}�X�^.�c�Ə��� (�敪�E�R�[�h�ɂ�蕪��) ) ")
            .AppendLine("	MK.koj_seikyuu_sime_date, ") '--�H���������ߓ�(�����X�}�X�^.�H���������ߓ�) ")
            .AppendLine("	MK.hansokuhin_seikyuu_saki_kbn, ") '--�̑��i������ �敪(�����X�}�X�^.�̑��i������敪) ")
            .AppendLine("	MK.hansokuhin_seikyuu_saki_cd, ") '--�̑��i������R�[�h(�����X�}�X�^.�̑��i������R�[�h) ")
            .AppendLine("	MK.hansokuhin_seikyuu_saki_brc, ") '--�̑��i������}��(�����X�}�X�^.�̑��i������}��) ")
            .AppendLine("	(SELECT seikyuu_saki_mei FROM v_seikyuu_saki_info WHERE ")
            .AppendLine("		seikyuu_saki_cd=MK.hansokuhin_seikyuu_saki_cd AND ")
            .AppendLine("		seikyuu_saki_brc=MK.hansokuhin_seikyuu_saki_brc AND  ")
            .AppendLine("		seikyuu_saki_kbn=MK.hansokuhin_seikyuu_saki_kbn ")
            .AppendLine("	) AS hansokuhin_seikyuu_saki_mei, ") '--�̑��i�����於(�����X�}�X�^.�����X���A������Ѓ}�X�^.������Ж��A�c�Ə��}�X�^.�c�Ə��� (�敪�E�R�[�h�ɂ�蕪��) ) ")
            .AppendLine("	MK.hansokuhin_seikyuu_sime_date, ") '--�̑��i�������ߓ�(�����X�}�X�^.�̑��i�������ߓ�) ")
            .AppendLine("	MK.tatemono_seikyuu_saki_kbn, ") '--�������������� �敪(�����X�}�X�^.����������敪) ")
            .AppendLine("	MK.tatemono_seikyuu_saki_cd, ") '--��������������R�[�h(�����X�}�X�^.����������R�[�h) ")
            .AppendLine("	MK.tatemono_seikyuu_saki_brc, ") '--��������������}��(�����X�}�X�^.����������}��) ")
            .AppendLine("	(SELECT seikyuu_saki_mei FROM v_seikyuu_saki_info WHERE ")
            .AppendLine("		seikyuu_saki_cd=MK.tatemono_seikyuu_saki_cd AND ")
            .AppendLine("		seikyuu_saki_brc=MK.tatemono_seikyuu_saki_brc AND  ")
            .AppendLine("		seikyuu_saki_kbn=MK.tatemono_seikyuu_saki_kbn ")
            .AppendLine("	) AS tatemono_seikyuu_saki_mei, ") '--�������������於(�����X�}�X�^.�����X���A������Ѓ}�X�^.������Ж��A�c�Ə��}�X�^.�c�Ə��� (�敪�E�R�[�h�ɂ�蕪��) ) ")
            .AppendLine("	MK.hansokuhin_seikyuu_sime_date, ") '--���������������ߓ�(�����X�}�X�^.�̑��i�������ߓ�) ")
            .AppendLine("	MK.seikyuu_saki_kbn5, ") '--������敪5(�����X�}�X�^.������敪5) ")
            .AppendLine("	MK.seikyuu_saki_cd5, ") '--������R�[�h5(�����X�}�X�^.������R�[�h5) ")
            .AppendLine("	MK.seikyuu_saki_brc5, ") '--������}��5(�����X�}�X�^.������}��5) ")
            .AppendLine("	(SELECT seikyuu_saki_mei FROM v_seikyuu_saki_info WHERE ")
            .AppendLine("		seikyuu_saki_cd=MK.seikyuu_saki_cd5 AND ")
            .AppendLine("		seikyuu_saki_brc=MK.seikyuu_saki_brc5 AND  ")
            .AppendLine("		seikyuu_saki_kbn=MK.seikyuu_saki_kbn5 ")
            .AppendLine("	) AS seikyuu_saki5_mei, ") '--������5��(�����X�}�X�^.�����X���A������Ѓ}�X�^.������Ж��A�c�Ə��}�X�^.�c�Ə��� (�敪�E�R�[�h�ɂ�蕪��) ) ")
            .AppendLine("	MK.hansokuhin_seikyuu_sime_date, ") '--������5���ߓ�(�����X�}�X�^.�̑��i�������ߓ�) ")
            .AppendLine("	MK.seikyuu_saki_kbn6, ") '--������敪6(�����X�}�X�^.������敪6) ")
            .AppendLine("	MK.seikyuu_saki_cd6, ") '--������R�[�h6(�����X�}�X�^.������R�[�h6) ")
            .AppendLine("	MK.seikyuu_saki_brc6, ") '--������}��6(�����X�}�X�^.������}��6) ")
            .AppendLine("	(SELECT seikyuu_saki_mei FROM v_seikyuu_saki_info WHERE ")
            .AppendLine("		seikyuu_saki_cd=MK.seikyuu_saki_cd6 AND ")
            .AppendLine("		seikyuu_saki_brc=MK.seikyuu_saki_brc6 AND  ")
            .AppendLine("		seikyuu_saki_kbn=MK.seikyuu_saki_kbn6 ")
            .AppendLine("	) AS seikyuu_saki6_mei, ") '--������6��(�����X�}�X�^.�����X���A������Ѓ}�X�^.������Ж��A�c�Ə��}�X�^.�c�Ə��� (�敪�E�R�[�h�ɂ�蕪��) ) ")
            .AppendLine("	MK.hansokuhin_seikyuu_sime_date, ") '--������6���ߓ�(�����X�}�X�^.�̑��i�������ߓ�) ")
            .AppendLine("	MK.seikyuu_saki_kbn7, ") '--������敪7(�����X�}�X�^.������敪7) ")
            .AppendLine("	MK.seikyuu_saki_cd7, ") '--������R�[�h7(�����X�}�X�^.������R�[�h7) ")
            .AppendLine("	MK.seikyuu_saki_brc7, ") '--������}��7(�����X�}�X�^.������}��7) ")
            .AppendLine("	(SELECT seikyuu_saki_mei FROM v_seikyuu_saki_info WHERE ")
            .AppendLine("		seikyuu_saki_cd=MK.seikyuu_saki_cd7 AND ")
            .AppendLine("		seikyuu_saki_brc=MK.seikyuu_saki_brc7 AND  ")
            .AppendLine("		seikyuu_saki_kbn=MK.seikyuu_saki_kbn7 ")
            .AppendLine("	) AS seikyuu_saki7_mei, ") '--������7��(�����X�}�X�^.�����X���A������Ѓ}�X�^.������Ж��A�c�Ə��}�X�^.�c�Ə��� (�敪�E�R�[�h�ɂ�蕪��) ) ")
            .AppendLine("	MK.hansokuhin_seikyuu_sime_date, ") '--������7���ߓ�(�����X�}�X�^.�̑��i�������ߓ�) ")
            .AppendLine("	MK.hosyou_kikan, ") '--�ۏ؊���(�����X�}�X�^.�ۏ؊���) ")
            .AppendLine("	MK.hosyousyo_hak_umu, ") '--�ۏ؏����s�L��(�����X�}�X�^.�ۏ؏����s�L��) ")
            .AppendLine("	MK.nyuukin_kakunin_jyouken, ") '--�����m�F����(�����X�}�X�^.�����m�F����) ")
            .AppendLine("	MM.meisyou, ") '--�����m�F������(���̃}�X�^.���́i���̃}�X�^.���̎�� = "05"�@�Ł@�����X�}�X�^.�����m�F����=���̃}�X�^.���ށj) ")
            .AppendLine("	MK.nyuukin_kakunin_oboegaki, ") '--�����m�F�o��(�����X�}�X�^.�����m�F�o��) ")
            .AppendLine("	MK.koj_gaisya_seikyuu_umu, ") '--�H����А����L��(�����X�}�X�^.�H����А����L��) ")
            .AppendLine("	MK.koj_tantou_flg, ") '--�H���S��FLG(�����X�}�X�^.�H���S��FLG) ")
            .AppendLine("	MK.tys_mitsyo_flg, ") '--�������Ϗ�FLG(�����X�}�X�^.�������Ϗ�FLG) ")
            .AppendLine("	MK.hattyuusyo_flg, ") '--������FLG(�����X�}�X�^.������FLG) ")
            .AppendLine("	MK.mitsyo_file_mei, ") '--���Ϗ�̧�ٖ�(�����X�}�X�^.���Ϗ�̧�ٖ�) ")
            .AppendLine("	MKTJ.tys_mitsyo_flg AS tys_mitsyo, ") '--�������Ϗ�(�����X������}�X�^.�������Ϗ�FLG) ")
            .AppendLine("	MKTJ.ks_danmenzu_flg, ") '--��b�f�ʐ}(�����X������}�X�^.��b�f�ʐ}FLG) ")
            .AppendLine("	MKTJ.tatou_waribiki_flg, ") '--���������敪(�����X������}�X�^.��������FLG) ")
            .AppendLine("	MKTJ.tatou_waribiki_bikou, ") '--�����������l(�����X������}�X�^.�����������l) ")
            .AppendLine("	MKTJ.tokka_sinsei_flg, ") '--�����\��(�����X������}�X�^.�����\��FLG) ")
            .AppendLine("	MKTJ.zando_syobunhi_umu, ") '--�c�y������(�����X������}�X�^.�c�y������L��) ")
            .AppendLine("	MKTJ.kyuusuisyadai_umu, ") '--�����ԑ�(�����X������}�X�^.�����ԑ�L��) ")
            .AppendLine("	MKTJ.jinawa_taiou_umu, ") '--�n��͂�(�����X������}�X�^.�n��Ή��L��) ")
            .AppendLine("	MKTJ.kousin_taiou_umu, ") '--�Y�c�o��(�����X������}�X�^.�Y�c�Ή��L��) ")
            .AppendLine("	MKTJ.tys_kojkan_heikin_nissuu, ") '--���ϓ���(�����X������}�X�^.�����H���ԕ��ϓ���) ")
            .AppendLine("	MKTJ.hyoujun_ks, ") '--�W����b(�����X������}�X�^.�W����b) ")
            .AppendLine("	MKTJ.js_igai_koj_flg, ") '--JHS�ȊO�H��(�����X������}�X�^.JS�ȊO�H��FLG) ")
            .AppendLine("	MKTJ.tys_hkks_busuu, ") '--�����񍐏�����(�����X������}�X�^.�����񍐏�����) ")
            .AppendLine("	MKTJ.koj_hkks_busuu, ") '--�H���񍐏�����(�����X������}�X�^.�H���񍐏�����) ")
            .AppendLine("	MKTJ.kensa_hkks_busuu, ") '--�����񍐏�����(�����X������}�X�^.�����񍐏�����) ")
            .AppendLine("	MKTJ.tys_hkks_douhuu_umu, ") '--�����񍐏�����(�����X������}�X�^.�����񍐏������L��) ")
            .AppendLine("	MKTJ.koj_hkks_douhuu_umu, ") '--�H���񍐏�����(�����X������}�X�^.�H���񍐏������L��) ")
            .AppendLine("	MKTJ.kensa_hkks_douhuu_umu, ") '--�����񍐏�����(�����X������}�X�^.�����񍐏������L��) ")
            .AppendLine("	MKTJ.nyuukin_mae_hosyousyo_hak, ") '--�����O�ۏ؏����s(�����X������}�X�^.�����O�ۏ؏����s) ")
            .AppendLine("	MKTJ.hikiwatasi_file_umu, ") '--���n�t�@�C��(�����X������}�X�^.���ņ�ٗL��) ")
            .AppendLine("	MKTJ.sime_date, ") '--������ߓ�(�����X������}�X�^.���ߓ�) ")
            .AppendLine("	MKTJ.seikyuusyo_hittyk_date, ") '--�������K����(�����X������}�X�^.�������K����) ")
            .AppendLine("	MKTJ.siharai_yotei_tuki, ") '--�x���\�茎(�����X������}�X�^.�x���\�茎) ")
            .AppendLine("	MKTJ.siharai_yotei_date, ") '--�x���\���(�����X������}�X�^.�x���\���) ")
            .AppendLine("	MKTJ.siharai_genkin_wariai, ") '--��������(�����X������}�X�^.�x����������) ")
            .AppendLine("	MKTJ.siharai_houhou_flg, ") '--�x�����@(�����X������}�X�^.�x�����@FLG) ")
            .AppendLine("	MKTJ.siharai_tegata_wariai, ") '--��`����(�����X������}�X�^.�x����`����) ")
            .AppendLine("	MKTJ.siharai_site, ") '--�x���T�C�g(�����X������}�X�^.�x�����) ")
            .AppendLine("	MKTJ.tegata_hiritu, ") '--��`�䗦(�����X������}�X�^.��`�䗦) ")
            .AppendLine("	MKTJ.tys_hattyuusyo_umu, ") '--�����������L��(�����X������}�X�^.�����������L��) ")
            .AppendLine("	MKTJ.koj_hattyuusyo_umu, ") '--�H���������L��(�����X������}�X�^.�H���������L��) ")
            .AppendLine("	MKTJ.kensa_hattyuusyo_umu, ") '--�����������L��(�����X������}�X�^.�����������L��) ")
            .AppendLine("	MKTJ.senpou_sitei_seikyuusyo, ") '--����w�萿����(�����X������}�X�^.����w�萿����) ")
            .AppendLine("	MKTJ.flow_kakunin_date, ") '--�t���[�m�F��(�����X������}�X�^.�t���[�m�F��) ")
            .AppendLine("	MKTJ.kyouryoku_kaihi_umu, ") '--���͉��(�����X������}�X�^.���͉��L��) ")
            .AppendLine("	MKTJ.kyouryoku_kaihi_hiritu, ") '--���͉��䗦(�����X������}�X�^.���͉��䗦) ")
            .AppendLine("	MK.danmenzu1, ") '--�f�ʐ}1(�����X�}�X�^.�f�ʐ}1) ")
            .AppendLine("	MK.danmenzu2, ") '--�f�ʐ}2(�����X�}�X�^.�f�ʐ}2) ")
            .AppendLine("	MK.danmenzu3, ") '--�f�ʐ}3(�����X�}�X�^.�f�ʐ}3) ")
            .AppendLine("	MK.danmenzu4, ") '--�f�ʐ}4(�����X�}�X�^.�f�ʐ}4) ")
            .AppendLine("	MK.danmenzu5, ") '--�f�ʐ}5(�����X�}�X�^.�f�ʐ}5) ")
            .AppendLine("	MK.danmenzu6, ") '--�f�ʐ}6(�����X�}�X�^.�f�ʐ}6) ")
            .AppendLine("	MK.danmenzu7 ") '--�f�ʐ}7(�����X�}�X�^.�f�ʐ}7) ")
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
            '�敪
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
            '�މ�������X
            If dtParamKameitenInfo(0).taikai <> "1" Then
                .AppendLine(" AND MK.torikesi = 0 ")
            End If
            '�����X��
            If dtParamKameitenInfo(0).kameiten_mei <> String.Empty Then
                .AppendLine(" AND (MK.kameiten_mei1 LIKE @kameiten_mei1 ")
                .AppendLine("      OR MK.kameiten_mei2 LIKE @kameiten_mei2) ")
                paramList.Add(MakeParam("@kameiten_mei1", SqlDbType.VarChar, 42, "%" & dtParamKameitenInfo(0).kameiten_mei & "%"))
                paramList.Add(MakeParam("@kameiten_mei2", SqlDbType.VarChar, 42, "%" & dtParamKameitenInfo(0).kameiten_mei & "%"))
            End If
            '�����X�R�[�h
            If dtParamKameitenInfo(0).kameiten_cd_from <> String.Empty And dtParamKameitenInfo(0).kameiten_cd_to <> String.Empty Then
                .AppendLine(" AND MK.kameiten_cd BETWEEN @kameiten_cd_from AND @kameiten_cd_to ")
                paramList.Add(MakeParam("@kameiten_cd_from", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).kameiten_cd_from))
                paramList.Add(MakeParam("@kameiten_cd_to", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).kameiten_cd_to))
            ElseIf dtParamKameitenInfo(0).kameiten_cd_from <> String.Empty And dtParamKameitenInfo(0).kameiten_cd_to = String.Empty Then
                .AppendLine(" AND MK.kameiten_cd = @kameiten_cd_from ")
                paramList.Add(MakeParam("@kameiten_cd_from", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).kameiten_cd_from))
            End If
            '�����X���J�i
            If dtParamKameitenInfo(0).kameiten_kana <> String.Empty Then
                .AppendLine(" AND (MK.tenmei_kana1 LIKE @kameiten_kana1 ")
                .AppendLine("      OR MK.tenmei_kana2 LIKE @kameiten_kana2) ")
                paramList.Add(MakeParam("@kameiten_kana1", SqlDbType.VarChar, 22, "%" & dtParamKameitenInfo(0).kameiten_kana & "%"))
                paramList.Add(MakeParam("@kameiten_kana2", SqlDbType.VarChar, 22, "%" & dtParamKameitenInfo(0).kameiten_kana & "%"))
            End If
            '�c�Ə��R�[�h
            If dtParamKameitenInfo(0).eigyousyo_cd_from <> String.Empty And dtParamKameitenInfo(0).eigyousyo_cd_to <> String.Empty Then
                .AppendLine(" AND MK.eigyousyo_cd BETWEEN @eigyousyo_cd_from AND @eigyousyo_cd_to ")
                paramList.Add(MakeParam("@eigyousyo_cd_from", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).eigyousyo_cd_from))
                paramList.Add(MakeParam("@eigyousyo_cd_to", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).eigyousyo_cd_to))
            ElseIf dtParamKameitenInfo(0).eigyousyo_cd_from <> String.Empty And dtParamKameitenInfo(0).eigyousyo_cd_to = String.Empty Then
                .AppendLine(" AND MK.eigyousyo_cd = @eigyousyo_cd_from ")
                paramList.Add(MakeParam("@eigyousyo_cd_from", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).eigyousyo_cd_from))
            End If
            '�d�b�ԍ�
            If dtParamKameitenInfo(0).tel_no <> String.Empty Then
                .AppendLine(" AND REPLACE(MKJ.tel_no,'-','') LIKE @tel_no ")
                paramList.Add(MakeParam("@tel_no", SqlDbType.VarChar, 17, dtParamKameitenInfo(0).tel_no & "%"))
            End If
            '�o�^�N��
            If dtParamKameitenInfo(0).touroku_nengetu_from <> String.Empty And dtParamKameitenInfo(0).touroku_nengetu_to <> String.Empty Then
                .AppendLine(" AND MKJ.add_nengetu BETWEEN @touroku_nengetu_from AND @touroku_nengetu_to ")
                paramList.Add(MakeParam("@touroku_nengetu_from", SqlDbType.VarChar, 7, dtParamKameitenInfo(0).touroku_nengetu_from))
                paramList.Add(MakeParam("@touroku_nengetu_to", SqlDbType.VarChar, 7, dtParamKameitenInfo(0).touroku_nengetu_to))
            ElseIf dtParamKameitenInfo(0).touroku_nengetu_from <> String.Empty And dtParamKameitenInfo(0).touroku_nengetu_to = String.Empty Then
                .AppendLine(" AND MKJ.add_nengetu = @touroku_nengetu_from ")
                paramList.Add(MakeParam("@touroku_nengetu_from", SqlDbType.VarChar, 7, dtParamKameitenInfo(0).touroku_nengetu_from))
            End If
            '�s���{��
            If dtParamKameitenInfo(0).todouhuken_cd <> String.Empty Then
                .AppendLine(" AND MK.todouhuken_cd = @todouhuken_cd ")
                paramList.Add(MakeParam("@todouhuken_cd", SqlDbType.VarChar, 2, dtParamKameitenInfo(0).todouhuken_cd))
            End If
            '�n��R�[�h
            If dtParamKameitenInfo(0).keiretu_cd <> String.Empty Then
                .AppendLine(" AND MK.keiretu_cd = @keiretu_cd ")
                paramList.Add(MakeParam("@keiretu_cd", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).keiretu_cd))
            End If
            '���t��I��
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

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, _
                    "KihonJyouhouCsv", paramList.ToArray)

        Return dsReturn

    End Function

    ''' <summary>�����X�Z�����CSV�f�[�^���擾����</summary>
    ''' <param name="dtParamKameitenInfo">�p�����[�^</param>
    ''' <returns>�����X�Z�����CSV�f�[�^�e�[�u��</returns>
    Public Function SelJyusyoJyouhouCsvInfo(ByVal dtParamKameitenInfo As KensakuSyoukaiInquiryDataSet.Param_KameitenInfoDataTable) _
           As Data.DataSet

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
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
            .AppendLine(" 	MKJ2.daihyousya_mei AS daihyousya_mei1, ") '--���t��1�F��\�Җ�(�����X�Z���}�X�^.��\�Җ�(�����X�Z���}�X�^.�Z��NO=2))
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
            .AppendLine(" 	MKJ3.daihyousya_mei AS daihyousya_mei2, ") '--���t��1�F��\�Җ�(�����X�Z���}�X�^.��\�Җ�(�����X�Z���}�X�^.�Z��NO=3))
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
            .AppendLine(" 	MKJ4.daihyousya_mei AS daihyousya_mei3, ") '--���t��1�F��\�Җ�(�����X�Z���}�X�^.��\�Җ�(�����X�Z���}�X�^.�Z��NO=4))
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
            .AppendLine(" 	MKJ5.daihyousya_mei AS daihyousya_mei4, ") '--���t��1�F��\�Җ�(�����X�Z���}�X�^.��\�Җ�(�����X�Z���}�X�^.�Z��NO=5))
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
            .AppendLine(" 	(CASE WHEN seikyuusyo_flg = -1 THEN '��' ELSE '' END) AS seikyuusyo_flg2, ")
            .AppendLine(" 	(CASE WHEN hosyousyo_flg = -1 THEN '��' ELSE '' END) AS hosyousyo_flg2, ")
            .AppendLine(" 	(CASE WHEN kasi_hosyousyo_flg = -1 THEN '��' ELSE '' END) AS kasi_hosyousyo_flg2, ")
            .AppendLine(" 	(CASE WHEN teiki_kankou_flg = -1 THEN '��' ELSE '' END) AS teiki_kankou_flg2, ")
            .AppendLine(" 	(CASE WHEN hkks_flg = -1 THEN '��' ELSE '' END) AS hkks_flg2, ")
            .AppendLine(" 	(CASE WHEN koj_hkks_flg = -1 THEN '��' ELSE '' END) AS koj_hkks_flg2, ")
            .AppendLine(" 	(CASE WHEN kensa_hkks_flg = -1 THEN '��' ELSE '' END) AS kensa_hkks_flg2, ")
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
            .AppendLine(" 	(CASE WHEN seikyuusyo_flg = -1 THEN '��' ELSE '' END) AS seikyuusyo_flg3, ")
            .AppendLine(" 	(CASE WHEN hosyousyo_flg = -1 THEN '��' ELSE '' END) AS hosyousyo_flg3, ")
            .AppendLine(" 	(CASE WHEN kasi_hosyousyo_flg = -1 THEN '��' ELSE '' END) AS kasi_hosyousyo_flg3, ")
            .AppendLine(" 	(CASE WHEN teiki_kankou_flg = -1 THEN '��' ELSE '' END) AS teiki_kankou_flg3, ")
            .AppendLine(" 	(CASE WHEN hkks_flg = -1 THEN '��' ELSE '' END) AS hkks_flg3, ")
            .AppendLine(" 	(CASE WHEN koj_hkks_flg = -1 THEN '��' ELSE '' END) AS koj_hkks_flg3, ")
            .AppendLine(" 	(CASE WHEN kensa_hkks_flg = -1 THEN '��' ELSE '' END) AS kensa_hkks_flg3, ")
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
            .AppendLine(" 	(CASE WHEN seikyuusyo_flg = -1 THEN '��' ELSE '' END) AS seikyuusyo_flg4, ")
            .AppendLine(" 	(CASE WHEN hosyousyo_flg = -1 THEN '��' ELSE '' END) AS hosyousyo_flg4, ")
            .AppendLine(" 	(CASE WHEN kasi_hosyousyo_flg = -1 THEN '��' ELSE '' END) AS kasi_hosyousyo_flg4, ")
            .AppendLine(" 	(CASE WHEN teiki_kankou_flg = -1 THEN '��' ELSE '' END) AS teiki_kankou_flg4, ")
            .AppendLine(" 	(CASE WHEN hkks_flg = -1 THEN '��' ELSE '' END) AS hkks_flg4, ")
            .AppendLine(" 	(CASE WHEN koj_hkks_flg = -1 THEN '��' ELSE '' END) AS koj_hkks_flg4, ")
            .AppendLine(" 	(CASE WHEN kensa_hkks_flg = -1 THEN '��' ELSE '' END) AS kensa_hkks_flg4, ")
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
            .AppendLine(" 	(CASE WHEN seikyuusyo_flg = -1 THEN '��' ELSE '' END) AS seikyuusyo_flg5, ")
            .AppendLine(" 	(CASE WHEN hosyousyo_flg = -1 THEN '��' ELSE '' END) AS hosyousyo_flg5, ")
            .AppendLine(" 	(CASE WHEN kasi_hosyousyo_flg = -1 THEN '��' ELSE '' END) AS kasi_hosyousyo_flg5, ")
            .AppendLine(" 	(CASE WHEN teiki_kankou_flg = -1 THEN '��' ELSE '' END) AS teiki_kankou_flg5, ")
            .AppendLine(" 	(CASE WHEN hkks_flg = -1 THEN '��' ELSE '' END) AS hkks_flg5, ")
            .AppendLine(" 	(CASE WHEN koj_hkks_flg = -1 THEN '��' ELSE '' END) AS koj_hkks_flg5, ")
            .AppendLine(" 	(CASE WHEN kensa_hkks_flg = -1 THEN '��' ELSE '' END) AS kensa_hkks_flg5, ")
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
            '�敪
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
            '�މ�������X
            If dtParamKameitenInfo(0).taikai <> "1" Then
                .AppendLine(" AND MK.torikesi = 0 ")
            End If
            '�����X��
            If dtParamKameitenInfo(0).kameiten_mei <> String.Empty Then
                .AppendLine(" AND (MK.kameiten_mei1 LIKE @kameiten_mei1 ")
                .AppendLine("      OR MK.kameiten_mei2 LIKE @kameiten_mei2) ")
                paramList.Add(MakeParam("@kameiten_mei1", SqlDbType.VarChar, 42, "%" & dtParamKameitenInfo(0).kameiten_mei & "%"))
                paramList.Add(MakeParam("@kameiten_mei2", SqlDbType.VarChar, 42, "%" & dtParamKameitenInfo(0).kameiten_mei & "%"))
            End If
            '�����X�R�[�h
            If dtParamKameitenInfo(0).kameiten_cd_from <> String.Empty And dtParamKameitenInfo(0).kameiten_cd_to <> String.Empty Then
                .AppendLine(" AND MK.kameiten_cd BETWEEN @kameiten_cd_from AND @kameiten_cd_to ")
                paramList.Add(MakeParam("@kameiten_cd_from", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).kameiten_cd_from))
                paramList.Add(MakeParam("@kameiten_cd_to", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).kameiten_cd_to))
            ElseIf dtParamKameitenInfo(0).kameiten_cd_from <> String.Empty And dtParamKameitenInfo(0).kameiten_cd_to = String.Empty Then
                .AppendLine(" AND MK.kameiten_cd = @kameiten_cd_from ")
                paramList.Add(MakeParam("@kameiten_cd_from", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).kameiten_cd_from))
            End If
            '�����X���J�i
            If dtParamKameitenInfo(0).kameiten_kana <> String.Empty Then
                .AppendLine(" AND (MK.tenmei_kana1 LIKE @kameiten_kana1 ")
                .AppendLine("      OR MK.tenmei_kana2 LIKE @kameiten_kana2) ")
                paramList.Add(MakeParam("@kameiten_kana1", SqlDbType.VarChar, 22, "%" & dtParamKameitenInfo(0).kameiten_kana & "%"))
                paramList.Add(MakeParam("@kameiten_kana2", SqlDbType.VarChar, 22, "%" & dtParamKameitenInfo(0).kameiten_kana & "%"))
            End If
            '�c�Ə��R�[�h
            If dtParamKameitenInfo(0).eigyousyo_cd_from <> String.Empty And dtParamKameitenInfo(0).eigyousyo_cd_to <> String.Empty Then
                .AppendLine(" AND MK.eigyousyo_cd BETWEEN @eigyousyo_cd_from AND @eigyousyo_cd_to ")
                paramList.Add(MakeParam("@eigyousyo_cd_from", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).eigyousyo_cd_from))
                paramList.Add(MakeParam("@eigyousyo_cd_to", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).eigyousyo_cd_to))
            ElseIf dtParamKameitenInfo(0).eigyousyo_cd_from <> String.Empty And dtParamKameitenInfo(0).eigyousyo_cd_to = String.Empty Then
                .AppendLine(" AND MK.eigyousyo_cd = @eigyousyo_cd_from ")
                paramList.Add(MakeParam("@eigyousyo_cd_from", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).eigyousyo_cd_from))
            End If
            '�d�b�ԍ�
            If dtParamKameitenInfo(0).tel_no <> String.Empty Then
                .AppendLine(" AND REPLACE(MKJ.tel_no,'-','') LIKE @tel_no ")
                paramList.Add(MakeParam("@tel_no", SqlDbType.VarChar, 17, dtParamKameitenInfo(0).tel_no & "%"))
            End If
            '�o�^�N��
            If dtParamKameitenInfo(0).touroku_nengetu_from <> String.Empty And dtParamKameitenInfo(0).touroku_nengetu_to <> String.Empty Then
                .AppendLine(" AND MKJ.add_nengetu BETWEEN @touroku_nengetu_from AND @touroku_nengetu_to ")
                paramList.Add(MakeParam("@touroku_nengetu_from", SqlDbType.VarChar, 7, dtParamKameitenInfo(0).touroku_nengetu_from))
                paramList.Add(MakeParam("@touroku_nengetu_to", SqlDbType.VarChar, 7, dtParamKameitenInfo(0).touroku_nengetu_to))
            ElseIf dtParamKameitenInfo(0).touroku_nengetu_from <> String.Empty And dtParamKameitenInfo(0).touroku_nengetu_to = String.Empty Then
                .AppendLine(" AND MKJ.add_nengetu = @touroku_nengetu_from ")
                paramList.Add(MakeParam("@touroku_nengetu_from", SqlDbType.VarChar, 7, dtParamKameitenInfo(0).touroku_nengetu_from))
            End If
            '�s���{��
            If dtParamKameitenInfo(0).todouhuken_cd <> String.Empty Then
                .AppendLine(" AND MK.todouhuken_cd = @todouhuken_cd ")
                paramList.Add(MakeParam("@todouhuken_cd", SqlDbType.VarChar, 2, dtParamKameitenInfo(0).todouhuken_cd))
            End If
            '�n��R�[�h
            If dtParamKameitenInfo(0).keiretu_cd <> String.Empty Then
                .AppendLine(" AND MK.keiretu_cd = @keiretu_cd ")
                paramList.Add(MakeParam("@keiretu_cd", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).keiretu_cd))
            End If
            '���t��I��
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

        ' �������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, _
                    "JyusyoJyouhouCsv", paramList.ToArray)

        Return dsReturn

    End Function

    ''' <summary>�����X���ꊇ�捞���CSV�f�[�^���擾����</summary>
    ''' <returns>�����X���ꊇ�捞���CSV�f�[�^�e�[�u��</returns>
    Public Function SelKameitenJyuusyoCsvInfo(ByVal dtParamKameitenInfo As KensakuSyoukaiInquiryDataSet.Param_KameitenInfoDataTable) As Data.DataTable
        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet
        'SQL�R�����g
        Dim commandTextSb As New StringBuilder
        ''�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
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
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            .AppendLine("   MK.nenkan_tousuu, ") '--�N�ԓ���
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            .AppendLine("   MK.fuho_syoumeisyo_flg, ")
            .AppendLine("   MK.fuho_syoumeisyo_kaisi_nengetu, ")
            .AppendLine("   MK.eigyou_tantousya_mei, ")
            '--�c�ƒS���Җ� �A�J�E���g�}�X�^.�����@��\��
            .AppendLine("   (select  simei from m_jiban_ninsyou left join  m_account on m_jiban_ninsyou.account_no=m_account.account_no where m_jiban_ninsyou.login_user_id=MK.eigyou_tantousya_mei) AS eigyou_tantousya_simei, ")
            .AppendLine("   MK.kyuu_eigyou_tantousya_mei, ")
            '--���c�ƒS���Җ� �A�J�E���g�}�X�^.�����@��\��	
            .AppendLine("   (select  simei from m_jiban_ninsyou left join  m_account on m_jiban_ninsyou.account_no=m_account.account_no where m_jiban_ninsyou.login_user_id=MK.kyuu_eigyou_tantousya_mei) AS kyuu_eigyou_tantousya_simei, ")
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            .AppendLine("   MK.koj_uri_syubetsu, ") '--�H��������
            .AppendLine("   MM.meisyou AS koj_uri_syubetsu_mei, ") '--�H�������ʖ�
            .AppendLine("   MK.jiosaki_flg, ") '--JIO��t���O
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
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
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            .AppendLine("   MK.hosyou_kikan, ") '--�ۏ؊���
            .AppendLine("   MK.hosyousyo_hak_umu, ") '--�ۏ؏����s�L��
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
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
            '--�\���S���� �����X�Z���}�X�^.�\���S����
            .AppendLine("   MKJ.mail_address AS mail_address, ")
            .AppendLine("   MKJ.bikou1, ")
            .AppendLine("   MKJ.bikou2 ")
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            .AppendLine("   ,REPLACE(ISNULL(CONVERT(VARCHAR(10),TTSS.add_date,112) + '_' + CONVERT(VARCHAR(10),TTSS.add_date,108),''),':','') AS add_date ") '--�o�^��
            .AppendLine("   ,TTSS.seikyuu_umu ") '--�����L��
            .AppendLine("   ,TTSS.syouhin_cd ") '--���i�R�[�h
            .AppendLine("   ,SUB_MS.syouhin_mei ") '--���i��
            .AppendLine("   ,TTSS.uri_gaku ") '--������z
            .AppendLine("   ,TTSS.koumuten_seikyuu_gaku ") '--�H���X�������z
            .AppendLine("   ,REPLACE(ISNULL(CONVERT(VARCHAR(10),TTSS.seikyuusyo_hak_date,112) + '_' + CONVERT(VARCHAR(10),TTSS.seikyuusyo_hak_date,108),''),':','') AS seikyuusyo_hak_date ") '--���������s��
            .AppendLine("   ,REPLACE(ISNULL(CONVERT(VARCHAR(10),TTSS.uri_date,112) + '_' + CONVERT(VARCHAR(10),TTSS.uri_date,108),''),':','') AS uri_date ") '--����N����
            .AppendLine("   ,TTSS.bikou ") '--���l
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            '============2012/05/08 �ԗ� 407553�̑Ή� �ǉ���======================
            .AppendLine("   ,CASE ")
            .AppendLine("       WHEN  MK.upd_datetime IS NOT NULL THEN ")
            .AppendLine("           REPLACE(ISNULL(CONVERT(VARCHAR(10),MK.upd_datetime,112) + '_' + CONVERT(VARCHAR(10),MK.upd_datetime,108),''),':','') ") '�X�V����
            .AppendLine("       ELSE ")
            .AppendLine("           REPLACE(ISNULL(CONVERT(VARCHAR(10),MK.add_datetime,112) + '_' + CONVERT(VARCHAR(10),MK.add_datetime,108),''),':','') ") '�o�^����
            .AppendLine("       END AS kameiten_upd_datetime ") '--�����X_�X�V����
            .AppendLine("   ,REPLACE(ISNULL(CONVERT(VARCHAR(10),SYOUHIN1.upd_datetime,112) + '_' + CONVERT(VARCHAR(10),SYOUHIN1.upd_datetime,108),''),':','') AS tatouwari_upd_datetime_1 ") '--��������_�X�V����1
            .AppendLine("   ,REPLACE(ISNULL(CONVERT(VARCHAR(10),SYOUHIN2.upd_datetime,112) + '_' + CONVERT(VARCHAR(10),SYOUHIN2.upd_datetime,108),''),':','') AS tatouwari_upd_datetime_2 ") '--��������_�X�V����2
            .AppendLine("   ,REPLACE(ISNULL(CONVERT(VARCHAR(10),SYOUHIN3.upd_datetime,112) + '_' + CONVERT(VARCHAR(10),SYOUHIN3.upd_datetime,108),''),':','') AS tatouwari_upd_datetime_3 ") '--��������_�X�V����3
            .AppendLine("   ,CASE ")
            .AppendLine("       WHEN  MKJ.upd_datetime IS NOT NULL THEN ")
            .AppendLine("           REPLACE(ISNULL(CONVERT(VARCHAR(10),MKJ.upd_datetime,112) + '_' + CONVERT(VARCHAR(10),MKJ.upd_datetime,108),''),':','') ") '�X�V����
            .AppendLine("       ELSE ")
            .AppendLine("           REPLACE(ISNULL(CONVERT(VARCHAR(10),MKJ.add_datetime,112) + '_' + CONVERT(VARCHAR(10),MKJ.add_datetime,108),''),':','') ") '�o�^����
            .AppendLine("       END AS kameiten_jyuusyo_upd_datetime ") '--�����X�Z��_�X�V����
            '============2012/05/08 �ԗ� 407553�̑Ή� �ǉ���======================
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
            '============2012/05/08 �ԗ� 407553�̑Ή� �ǉ���======================
            .AppendLine("           ,CASE ")
            .AppendLine("               WHEN MTS.upd_datetime IS NOT NULL THEN ")
            .AppendLine("                   MTS.upd_datetime ") '--�X�V����
            .AppendLine("               ELSE ")
            .AppendLine("                   MTS.add_datetime ") '--�o�^����
            .AppendLine("               END AS upd_datetime ")
            '============2012/05/08 �ԗ� 407553�̑Ή� �ǉ���======================
            .AppendLine("       FROM m_tatouwaribiki_settei AS MTS WITH(READCOMMITTED) ")
            .AppendLine("       LEFT OUTER JOIN  ")
            .AppendLine("       m_syouhin AS MS WITH(READCOMMITTED) ")
            .AppendLine("       ON MTS.syouhin_cd = MS.syouhin_cd ")
            .AppendLine("       WHERE MTS.toukubun = 1) AS SYOUHIN1 ")
            .AppendLine("       ON MK.kameiten_cd = SYOUHIN1.kameiten_cd ")
            .AppendLine(" LEFT OUTER JOIN  ")
            .AppendLine("       (SELECT MTS.kameiten_cd, MTS.toukubun, MTS.syouhin_cd, MS.syouhin_mei  ")
            '============2012/05/08 �ԗ� 407553�̑Ή� �ǉ���======================
            .AppendLine("           ,CASE ")
            .AppendLine("               WHEN MTS.upd_datetime IS NOT NULL THEN ")
            .AppendLine("                   MTS.upd_datetime ") '--�X�V����
            .AppendLine("               ELSE ")
            .AppendLine("                   MTS.add_datetime ") '--�o�^����
            .AppendLine("               END AS upd_datetime ")
            '============2012/05/08 �ԗ� 407553�̑Ή� �ǉ���======================
            .AppendLine("       FROM m_tatouwaribiki_settei AS MTS WITH(READCOMMITTED) ")
            .AppendLine("       LEFT OUTER JOIN  ")
            .AppendLine("       m_syouhin AS MS WITH(READCOMMITTED) ")
            .AppendLine("       ON MTS.syouhin_cd = MS.syouhin_cd ")
            .AppendLine("       WHERE MTS.toukubun = 2) AS SYOUHIN2 ")
            .AppendLine("       ON MK.kameiten_cd = SYOUHIN2.kameiten_cd ")
            .AppendLine(" LEFT OUTER JOIN  ")
            .AppendLine("      	(SELECT MTS.kameiten_cd, MTS.toukubun, MTS.syouhin_cd, MS.syouhin_mei  ")
            '============2012/05/08 �ԗ� 407553�̑Ή� �ǉ���======================
            .AppendLine("           ,CASE ")
            .AppendLine("               WHEN MTS.upd_datetime IS NOT NULL THEN ")
            .AppendLine("                   MTS.upd_datetime ") '--�X�V����
            .AppendLine("               ELSE ")
            .AppendLine("                   MTS.add_datetime ") '--�o�^����
            .AppendLine("               END AS upd_datetime ")
            '============2012/05/08 �ԗ� 407553�̑Ή� �ǉ���======================
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
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            .AppendLine(" LEFT JOIN ")
            .AppendLine("    m_meisyou AS MM ") '--���̃}�X�^ ")
            .AppendLine(" ON ")
            .AppendLine("    MM.meisyou_syubetu = '55' ") '--�H�������� ")
            .AppendLine("    AND ")
            .AppendLine("    MM.code = MK.koj_uri_syubetsu ")
            .AppendLine(" LEFT JOIN ")
            .AppendLine("    t_tenbetu_syoki_seikyuu AS TTSS ") '--�X�ʏ��������e�[�u�� ")
            .AppendLine(" ON ")
            .AppendLine("    TTSS.mise_cd = MK.kameiten_cd  ")
            .AppendLine("    AND ")
            .AppendLine("    TTSS.bunrui_cd = '200' ")
            .AppendLine(" LEFT JOIN ")
            .AppendLine("    m_syouhin AS SUB_MS ") '--���i�}�X�^ ")
            .AppendLine(" ON ")
            .AppendLine("    SUB_MS.syouhin_cd = TTSS.syouhin_cd ")
            '================��2013/03/06 �ԗ� 407584 �ǉ���========================
            .AppendLine(" WHERE ")
            .AppendLine("   1=1 ")
            '.AppendLine("   AND MK.kameiten_cd='00006'")
            '�敪
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
            '�މ�������X
            If dtParamKameitenInfo(0).taikai <> "1" Then
                .AppendLine(" AND MK.torikesi = 0 ")
            End If
            '�����X��
            If dtParamKameitenInfo(0).kameiten_mei <> String.Empty Then
                .AppendLine(" AND (MK.kameiten_mei1 LIKE @kameiten_mei1 ")
                .AppendLine("      OR MK.kameiten_mei2 LIKE @kameiten_mei2) ")
                paramList.Add(MakeParam("@kameiten_mei1", SqlDbType.VarChar, 42, "%" & dtParamKameitenInfo(0).kameiten_mei & "%"))
                paramList.Add(MakeParam("@kameiten_mei2", SqlDbType.VarChar, 42, "%" & dtParamKameitenInfo(0).kameiten_mei & "%"))
            End If
            '�����X�R�[�h
            If dtParamKameitenInfo(0).kameiten_cd_from <> String.Empty And dtParamKameitenInfo(0).kameiten_cd_to <> String.Empty Then
                .AppendLine(" AND MK.kameiten_cd BETWEEN @kameiten_cd_from AND @kameiten_cd_to ")
                paramList.Add(MakeParam("@kameiten_cd_from", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).kameiten_cd_from))
                paramList.Add(MakeParam("@kameiten_cd_to", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).kameiten_cd_to))
            ElseIf dtParamKameitenInfo(0).kameiten_cd_from <> String.Empty And dtParamKameitenInfo(0).kameiten_cd_to = String.Empty Then
                .AppendLine(" AND MK.kameiten_cd = @kameiten_cd_from ")
                paramList.Add(MakeParam("@kameiten_cd_from", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).kameiten_cd_from))
            End If
            '�����X���J�i
            If dtParamKameitenInfo(0).kameiten_kana <> String.Empty Then
                .AppendLine(" AND (MK.tenmei_kana1 LIKE @kameiten_kana1 ")
                .AppendLine("      OR MK.tenmei_kana2 LIKE @kameiten_kana2) ")
                paramList.Add(MakeParam("@kameiten_kana1", SqlDbType.VarChar, 22, "%" & dtParamKameitenInfo(0).kameiten_kana & "%"))
                paramList.Add(MakeParam("@kameiten_kana2", SqlDbType.VarChar, 22, "%" & dtParamKameitenInfo(0).kameiten_kana & "%"))
            End If
            '�c�Ə��R�[�h
            If dtParamKameitenInfo(0).eigyousyo_cd_from <> String.Empty And dtParamKameitenInfo(0).eigyousyo_cd_to <> String.Empty Then
                .AppendLine(" AND MK.eigyousyo_cd BETWEEN @eigyousyo_cd_from AND @eigyousyo_cd_to ")
                paramList.Add(MakeParam("@eigyousyo_cd_from", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).eigyousyo_cd_from))
                paramList.Add(MakeParam("@eigyousyo_cd_to", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).eigyousyo_cd_to))
            ElseIf dtParamKameitenInfo(0).eigyousyo_cd_from <> String.Empty And dtParamKameitenInfo(0).eigyousyo_cd_to = String.Empty Then
                .AppendLine(" AND MK.eigyousyo_cd = @eigyousyo_cd_from ")
                paramList.Add(MakeParam("@eigyousyo_cd_from", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).eigyousyo_cd_from))
            End If
            '�d�b�ԍ�
            If dtParamKameitenInfo(0).tel_no <> String.Empty Then
                .AppendLine(" AND REPLACE(MKJ.tel_no,'-','') LIKE @tel_no ")
                paramList.Add(MakeParam("@tel_no", SqlDbType.VarChar, 17, dtParamKameitenInfo(0).tel_no & "%"))
            End If
            '�o�^�N��
            If dtParamKameitenInfo(0).touroku_nengetu_from <> String.Empty And dtParamKameitenInfo(0).touroku_nengetu_to <> String.Empty Then
                .AppendLine(" AND MKJ.add_nengetu BETWEEN @touroku_nengetu_from AND @touroku_nengetu_to ")
                paramList.Add(MakeParam("@touroku_nengetu_from", SqlDbType.VarChar, 7, dtParamKameitenInfo(0).touroku_nengetu_from))
                paramList.Add(MakeParam("@touroku_nengetu_to", SqlDbType.VarChar, 7, dtParamKameitenInfo(0).touroku_nengetu_to))
            ElseIf dtParamKameitenInfo(0).touroku_nengetu_from <> String.Empty And dtParamKameitenInfo(0).touroku_nengetu_to = String.Empty Then
                .AppendLine(" AND MKJ.add_nengetu = @touroku_nengetu_from ")
                paramList.Add(MakeParam("@touroku_nengetu_from", SqlDbType.VarChar, 7, dtParamKameitenInfo(0).touroku_nengetu_from))
            End If
            '�s���{��
            If dtParamKameitenInfo(0).todouhuken_cd <> String.Empty Then
                .AppendLine(" AND MK.todouhuken_cd = @todouhuken_cd ")
                paramList.Add(MakeParam("@todouhuken_cd", SqlDbType.VarChar, 2, dtParamKameitenInfo(0).todouhuken_cd))
            End If
            '�n��R�[�h
            If dtParamKameitenInfo(0).keiretu_cd <> String.Empty Then
                .AppendLine(" AND MK.keiretu_cd = @keiretu_cd ")
                paramList.Add(MakeParam("@keiretu_cd", SqlDbType.VarChar, 5, dtParamKameitenInfo(0).keiretu_cd))
            End If
            '���t��I��
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

        'EDI���쐬��
        'paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, ""))

        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsReturn, "KameitenJyuusyo", paramList.ToArray)

        Return dsReturn.Tables(0)
    End Function

    ''' <summary>
    ''' ��������i�[��Ǘ��}�X�^���A�i�[��t�@�C���p�X���擾����
    ''' </summary>
    ''' <param name="strKbn">�敪</param>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>���F ��A���V�X�e���� 2013.03.22</history>
    Public Function SelKakunousakiFilePassJ(ByVal strKbn As String, ByVal strKameitenCd As String) As Data.DataTable

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
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

        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsReturn, "KakunousakiFilePassJ", paramList.ToArray)

        Return dsReturn.Tables(0)
    End Function

    ''' <summary>
    ''' �����J�[�h�i�[��Ǘ��}�X�^���A�i�[��t�@�C���p�X���擾����
    ''' </summary>
    ''' <param name="strKbn">�敪</param>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>���F ��A���V�X�e���� 2013.03.22</history>
    Public Function SelKakunousakiFilePassC(ByVal strKbn As String, ByVal strKameitenCd As String) As Data.DataTable

        'DataSet�C���X�^���X�̐���
        Dim dsReturn As New Data.DataSet

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
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

        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString, dsReturn, "KakunousakiFilePassC", paramList.ToArray)

        Return dsReturn.Tables(0)
    End Function

End Class

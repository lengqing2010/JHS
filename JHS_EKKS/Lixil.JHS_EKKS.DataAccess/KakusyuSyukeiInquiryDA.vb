Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Imports System.Data
Imports System.Text

''' <summary>
''' �e��W�v
''' </summary>
''' <remarks></remarks>
Public Class KakusyuSyukeiInquiryDA

    ''' <summary>
    ''' ����ToList���擾����
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2012/12/04�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelTukinamiListData() As DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name)

        '�߂�f�[�^�Z�b�g
        Dim dsReturn As New Data.DataSet

        'SQL�R�����g
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With commandTextSb
            .AppendLine(" SELECT ")
            .AppendLine("         code ")
            .AppendLine("         ,meisyou ")
            .AppendLine(" FROM ")
            .AppendLine("         m_keikakuyou_meisyou WITH(READCOMMITTED)  ")
            .AppendLine(" WHERE ")
            .AppendLine("         meisyou_syubetu = @meisyou_syubetu ")
            .AppendLine(" ORDER BY ")
            .AppendLine("         hyouji_jyun ASC ")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@meisyou_syubetu", SqlDbType.Char, 2, "02"))     '���̎��

        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "dsTukinamiListData", paramList.ToArray())

        Return dsReturn.Tables("dsTukinamiListData")

    End Function

    ''' <summary>
    ''' ��ʃf�[�^���擾����(�c�Ƌ敪��FC��I�����Ȃ�)
    ''' </summary>
    ''' <param name="strTodouhukenCd">�s���{���R�[�h</param>
    ''' <param name="strSitenCd">�x�X�R�[�h</param>
    ''' <param name="strEigyousyoBusyoCd">�c�Ə��R�[�h</param>
    ''' <param name="strKeiretuCd">�n��R�[�h</param>
    ''' <param name="strEigyouTantousyaId">�c�ƃ}���R�[�h</param>
    ''' <param name="strKameitenCd">�o�^���Ǝ҃R�[�h</param>
    ''' <param name="strNendo">�N�x</param>
    ''' <param name="intBegin">����</param>
    ''' <param name="intEnd">�I���</param>
    ''' <param name="strEigyouKbn">�c�Ƌ敪</param>
    ''' <returns>Data.DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/12/27�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelKakusyuSyukeiData(ByVal strSitenCd As String, _
                                         ByVal strTodouhukenCd As String, _
                                         ByVal strEigyousyoBusyoCd As String, _
                                         ByVal strKeiretuCd As String, _
                                         ByVal strEigyouTantousyaId As String, _
                                         ByVal strKameitenCd As String, _
                                         ByVal strNendo As String, _
                                         ByVal intBegin As Integer, _
                                         ByVal intEnd As Integer, _
                                         ByVal strEigyouKbn As String) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                            strSitenCd, _
                                                                                            strTodouhukenCd, _
                                                                                            strEigyousyoBusyoCd, _
                                                                                            strKeiretuCd, _
                                                                                            strEigyouTantousyaId, _
                                                                                            strKameitenCd, _
                                                                                            strNendo, _
                                                                                            intBegin, _
                                                                                            intEnd, _
                                                                                            strEigyouKbn)

        '�߂�f�[�^�Z�b�g
        Dim dsReturn As New Data.DataSet

        'SQL�R�����g
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        If Left(strEigyouKbn, 1).Equals(",") Then
            strEigyouKbn = Mid(strEigyouKbn, 2, strEigyouKbn.Length)
            strEigyouKbn = "('" & strEigyouKbn.Replace(",", "','") & "')"
        End If

        With commandTextSb
            .AppendLine("SELECT")
            .AppendLine("    SUB_DATA.busyo_cd")
            .AppendLine("   ,SUB_DATA.Siten")
            .AppendLine("   ,SUB_HIRITU.koujiHanteiritu")
            .AppendLine("   ,SUB_HIRITU.koujiJyutyuuritu")
            .AppendLine("   ,SUB_HIRITU.tyokuKoujiritu")
            .AppendLine("   ,SUB_DATA.meisyou")
            .AppendLine("   ,SUB_DATA.syouhin_mei")
            .AppendLine("   ,SUB_DATA.zennenHeikentanka")
            .AppendLine("   ,SUB_DATA.keikakuKensuSyukei")     '�v�挏���W�v
            .AppendLine("   ,SUB_DATA.keikakuKingakuSyukei")   '�v����z�W�v
            .AppendLine("   ,SUB_DATA.keikakuSoriSyukei ")     '�v��e���W�v
            .AppendLine("   ,SUB_DATA.jiltusekiKensuSyukei ")  '���ь����W�v
            .AppendLine("   ,SUB_DATA.jiltusekiKingakuSyukei") '���ы��z�W�v
            .AppendLine("   ,SUB_DATA.jiltusekiSoriSyukei")    '���ёe���W�v
            .AppendLine("FROM")
            .AppendLine("(SELECT")
            If Not strSitenCd.Equals(String.Empty) Then
                .AppendLine("    MBK.busyo_mei AS Siten")
                .AppendLine("   ,MBK.busyo_cd AS busyo_cd")
            ElseIf (Not strTodouhukenCd.Equals(String.Empty)) Then
                .AppendLine("    MT.todouhuken_mei AS Siten")
                .AppendLine("   ,MT.todouhuken_cd AS busyo_cd")
            ElseIf (Not strEigyousyoBusyoCd.Equals(String.Empty)) Then
                .AppendLine("    MBK1.busyo_mei AS Siten")
                .AppendLine("   ,MBK1.busyo_cd AS busyo_cd")
            ElseIf (Not strKeiretuCd.Equals(String.Empty)) Then
                .AppendLine("    MK.keiretu_mei AS Siten")
                .AppendLine("   ,MK.keiretu_cd AS busyo_cd")
            ElseIf (Not strEigyouTantousyaId.Equals(String.Empty)) Then
                .AppendLine("    SUB_MAN.DisplayName AS Siten")
                .AppendLine("   ,SUB_MAN.login_user_id AS busyo_cd")
            ElseIf (Not strKameitenCd.Equals(String.Empty)) Then
                .AppendLine("    MK1.kameiten_mei1 AS Siten")
                .AppendLine("   ,MK1.kameiten_cd AS busyo_cd")
            End If
            .AppendLine("   ,MKKS.bunbetu_cd")
            .AppendLine("   ,MKM.meisyou AS meisyou")                                '--����
            .AppendLine("   ,MKM.hyouji_jyun")
            .AppendLine("   ,SUB_T.keikaku_kanri_syouhin_cd")
            .AppendLine("   ,MKKS.keikaku_kanri_syouhin_mei AS syouhin_mei")         '--���i��
            .AppendLine("   ,CONVERT(DECIMAL(15,0),ROUND(AVG(ISNULL(CONVERT(DECIMAL,TJK.zennen_heikin_tanka),0)),0)) AS zennenHeikentanka") '--�O�N_���ϒP��
            '.AppendLine("   ,AVG(TJK.zennen_heikin_tanka) AS zennenHeikentanka")     '--�O�N_���ϒP��

            '�v�挏���W�v
            .AppendLine("   ,(")
            .AppendLine("    0")
            If intEnd <> 0 Then
                For i As Integer = intBegin To intEnd
                    .AppendLine(" + ISNULL(SUM([" & IIf(i > 12, i - 12, i).ToString & "gatu_keikaku_kensuu]),0)")
                Next
            Else
                .AppendLine("     + ISNULL(SUM([" & IIf(intBegin > 12, intBegin - 12, intBegin).ToString & "gatu_keikaku_kensuu]),0)")
            End If
            .AppendLine(" ) AS keikakuKensuSyukei") '�v�挏���W�v

            '�v����z�W�v
            .AppendLine("   ,(")
            .AppendLine("    0")
            If intEnd <> 0 Then
                For i As Integer = intBegin To intEnd
                    .AppendLine(" + ISNULL(SUM([" & IIf(i > 12, i - 12, i).ToString & "gatu_keikaku_kingaku]),0)")
                Next
            Else
                .AppendLine("     + ISNULL(SUM([" & IIf(intBegin > 12, intBegin - 12, intBegin).ToString & "gatu_keikaku_kingaku]),0)")
            End If
            .AppendLine(" ) AS keikakuKingakuSyukei") '�v����z�W�v

            '�v��e���W�v
            .AppendLine("   ,(")
            .AppendLine("    0")
            If intEnd <> 0 Then
                For i As Integer = intBegin To intEnd
                    .AppendLine(" + ISNULL(SUM([" & IIf(i > 12, i - 12, i).ToString & "gatu_keikaku_arari]),0)")
                Next
            Else
                .AppendLine("     + ISNULL(SUM([" & IIf(intBegin > 12, intBegin - 12, intBegin).ToString & "gatu_keikaku_arari]),0)")
            End If
            .AppendLine(" ) AS keikakuSoriSyukei") '�v��e���W�v

            '���ь����W�v
            .AppendLine("   ,(")
            .AppendLine("    0")
            If intEnd <> 0 Then
                For i As Integer = intBegin To intEnd
                    .AppendLine(" + ISNULL(SUM([" & IIf(i > 12, i - 12, i).ToString & "gatu_jisseki_kensuu]),0)")
                Next
            Else
                .AppendLine("     + ISNULL(SUM([" & IIf(intBegin > 12, intBegin - 12, intBegin).ToString & "gatu_jisseki_kensuu]),0)")
            End If
            .AppendLine(" ) AS jiltusekiKensuSyukei") '���ь����W�v

            '���ы��z�W�v
            .AppendLine("   ,(")
            .AppendLine("    0")
            If intEnd <> 0 Then
                For i As Integer = intBegin To intEnd
                    .AppendLine(" + ISNULL(SUM([" & IIf(i > 12, i - 12, i).ToString & "gatu_jisseki_kingaku]),0)")
                Next
            Else
                .AppendLine("     + ISNULL(SUM([" & IIf(intBegin > 12, intBegin - 12, intBegin).ToString & "gatu_jisseki_kingaku]),0)")
            End If
            .AppendLine(" ) AS jiltusekiKingakuSyukei") '���ы��z�W�v

            '���ёe���W�v
            .AppendLine("   ,(")
            .AppendLine("    0")
            If intEnd <> 0 Then
                For i As Integer = intBegin To intEnd
                    .AppendLine(" + ISNULL(SUM([" & IIf(i > 12, i - 12, i).ToString & "gatu_jisseki_arari]),0)")
                Next
            Else
                .AppendLine("     + ISNULL(SUM([" & IIf(intBegin > 12, intBegin - 12, intBegin).ToString & "gatu_jisseki_arari]),0)")
            End If
            .AppendLine(" ) AS jiltusekiSoriSyukei") '���ёe���W�v

            .AppendLine("FROM m_keikaku_kameiten AS MKK WITH(READCOMMITTED)")            '--�v��Ǘ�_�����XϽ�
            .AppendLine("   LEFT JOIN")
            .AppendLine("       (SELECT")
            .AppendLine("           kameiten_cd")
            .AppendLine("           ,keikaku_kanri_syouhin_cd")
            .AppendLine("           ,keikaku_nendo")
            .AppendLine("       FROM")
            .AppendLine("           t_keikaku_kanri WITH(READCOMMITTED)")
            .AppendLine("       WHERE")
            .AppendLine("           keikaku_nendo = @strNendo")
            .AppendLine("       GROUP BY")
            .AppendLine("           kameiten_cd")
            .AppendLine("           ,keikaku_kanri_syouhin_cd")
            .AppendLine("           ,keikaku_nendo")
            .AppendLine("       UNION")
            .AppendLine("       SELECT")
            .AppendLine("           kameiten_cd")
            .AppendLine("           ,keikaku_kanri_syouhin_cd")
            .AppendLine("           ,keikaku_nendo")
            .AppendLine("       FROM")
            .AppendLine("           t_jisseki_kanri WITH(READCOMMITTED)")
            .AppendLine("       WHERE")
            .AppendLine("           keikaku_nendo = @strNendo) AS SUB_T")
            .AppendLine("   ON MKK.kameiten_cd = SUB_T.kameiten_cd")
            .AppendLine("   AND MKK.keikaku_nendo = SUB_T.keikaku_nendo")
            .AppendLine("   AND MKK.eigyou_kbn IN ").Append(strEigyouKbn)

            .AppendLine("   LEFT JOIN")
            .AppendLine("       t_jisseki_kanri AS TJK WITH(READCOMMITTED)")                      '--���ъǗ��e�[�u��
            .AppendLine("       ON SUB_T.kameiten_cd = TJK.kameiten_cd")
            .AppendLine("       AND SUB_T.keikaku_nendo = TJK.keikaku_nendo")
            .AppendLine("       AND SUB_T.keikaku_kanri_syouhin_cd = TJK.keikaku_kanri_syouhin_cd")

            .AppendLine("   LEFT JOIN")
            .AppendLine("       (SELECT ")
            .AppendLine("            TKK.kameiten_cd")
            .AppendLine("           ,TKK.keikaku_nendo")
            .AppendLine("           ,TKK.bunbetu_cd")
            .AppendLine("           ,TKK.keikaku_kanri_syouhin_cd")
            .AppendLine("           ,SUM(TKK.[4gatu_keikaku_kensuu] ) AS [4gatu_keikaku_kensuu] ")     '4��_�v�挏��
            .AppendLine("           ,SUM(TKK.[4gatu_keikaku_kingaku] ) AS [4gatu_keikaku_kingaku] ")   '4��_�v����z
            .AppendLine("           ,SUM(TKK.[4gatu_keikaku_arari] ) AS [4gatu_keikaku_arari] ")       '4��_�v��e��
            .AppendLine("           ,SUM(TKK.[5gatu_keikaku_kensuu] ) AS [5gatu_keikaku_kensuu] ")     '5��_�v�挏��
            .AppendLine("           ,SUM(TKK.[5gatu_keikaku_kingaku] ) AS [5gatu_keikaku_kingaku] ")   '5��_�v����z
            .AppendLine("           ,SUM(TKK.[5gatu_keikaku_arari] ) AS [5gatu_keikaku_arari] ")       '5��_�v��e��
            .AppendLine("           ,SUM(TKK.[6gatu_keikaku_kensuu] ) AS [6gatu_keikaku_kensuu] ")     '6��_�v�挏��
            .AppendLine("           ,SUM(TKK.[6gatu_keikaku_kingaku] ) AS [6gatu_keikaku_kingaku] ")   '6��_�v����z
            .AppendLine("           ,SUM(TKK.[6gatu_keikaku_arari] ) AS [6gatu_keikaku_arari] ")       '6��_�v��e��
            .AppendLine("           ,SUM(TKK.[7gatu_keikaku_kensuu] ) AS [7gatu_keikaku_kensuu] ")     '7��_�v�挏��
            .AppendLine("           ,SUM(TKK.[7gatu_keikaku_kingaku] ) AS [7gatu_keikaku_kingaku] ")   '7��_�v����z
            .AppendLine("           ,SUM(TKK.[7gatu_keikaku_arari] ) AS [7gatu_keikaku_arari] ")       '7��_�v��e��
            .AppendLine("           ,SUM(TKK.[8gatu_keikaku_kensuu] ) AS [8gatu_keikaku_kensuu] ")     '8��_�v�挏��
            .AppendLine("           ,SUM(TKK.[8gatu_keikaku_kingaku] ) AS [8gatu_keikaku_kingaku] ")   '8��_�v����z
            .AppendLine("           ,SUM(TKK.[8gatu_keikaku_arari] ) AS [8gatu_keikaku_arari] ")       '8��_�v��e��
            .AppendLine("           ,SUM(TKK.[9gatu_keikaku_kensuu] ) AS [9gatu_keikaku_kensuu] ")     '9��_�v�挏��
            .AppendLine("           ,SUM(TKK.[9gatu_keikaku_kingaku] ) AS [9gatu_keikaku_kingaku] ")   '9��_�v����z
            .AppendLine("           ,SUM(TKK.[9gatu_keikaku_arari] ) AS [9gatu_keikaku_arari] ")       '9��_�v��e��
            .AppendLine("           ,SUM(TKK.[10gatu_keikaku_kensuu] ) AS [10gatu_keikaku_kensuu] ")   '10��_�v�挏��
            .AppendLine("           ,SUM(TKK.[10gatu_keikaku_kingaku] ) AS [10gatu_keikaku_kingaku] ") '10��_�v����z
            .AppendLine("           ,SUM(TKK.[10gatu_keikaku_arari] ) AS [10gatu_keikaku_arari] ")     '10��_�v��e��
            .AppendLine("           ,SUM(TKK.[11gatu_keikaku_kensuu] ) AS [11gatu_keikaku_kensuu] ")   '11��_�v�挏��
            .AppendLine("           ,SUM(TKK.[11gatu_keikaku_kingaku] ) AS [11gatu_keikaku_kingaku] ") '11��_�v����z
            .AppendLine("           ,SUM(TKK.[11gatu_keikaku_arari] ) AS [11gatu_keikaku_arari] ")     '11��_�v��e��
            .AppendLine("           ,SUM(TKK.[12gatu_keikaku_kensuu] ) AS [12gatu_keikaku_kensuu] ")   '12��_�v�挏��
            .AppendLine("           ,SUM(TKK.[12gatu_keikaku_kingaku] ) AS [12gatu_keikaku_kingaku] ") '12��_�v����z
            .AppendLine("           ,SUM(TKK.[12gatu_keikaku_arari] ) AS [12gatu_keikaku_arari] ")     '12��_�v��e��
            .AppendLine("           ,SUM(TKK.[1gatu_keikaku_kensuu] ) AS [1gatu_keikaku_kensuu] ")     '1��_�v�挏��
            .AppendLine("           ,SUM(TKK.[1gatu_keikaku_kingaku] ) AS [1gatu_keikaku_kingaku] ")   '1��_�v����z
            .AppendLine("           ,SUM(TKK.[1gatu_keikaku_arari] ) AS [1gatu_keikaku_arari] ")       '1��_�v��e��
            .AppendLine("           ,SUM(TKK.[2gatu_keikaku_kensuu] ) AS [2gatu_keikaku_kensuu] ")     '2��_�v�挏��
            .AppendLine("           ,SUM(TKK.[2gatu_keikaku_kingaku] ) AS [2gatu_keikaku_kingaku] ")   '2��_�v����z
            .AppendLine("           ,SUM(TKK.[2gatu_keikaku_arari] ) AS [2gatu_keikaku_arari] ")       '2��_�v��e��
            .AppendLine("           ,SUM(TKK.[3gatu_keikaku_kensuu] ) AS [3gatu_keikaku_kensuu] ")     '3��_�v�挏��
            .AppendLine("           ,SUM(TKK.[3gatu_keikaku_kingaku] ) AS [3gatu_keikaku_kingaku] ")   '3��_�v����z
            .AppendLine("           ,SUM(TKK.[3gatu_keikaku_arari] ) AS [3gatu_keikaku_arari] ")       '3��_�v��e��
            .AppendLine("       FROM t_keikaku_kanri AS TKK WITH(READCOMMITTED)")                '--�v��Ǘ��e�[�u��
            .AppendLine("       INNER JOIN(")
            .AppendLine("           SELECT kameiten_cd")
            .AppendLine("               ,keikaku_nendo")
            .AppendLine("               ,keikaku_kanri_syouhin_cd")
            .AppendLine("               ,MAX(CASE WHEN ISNULL(CONVERT(VARCHAR,keikaku_kakutei_flg),'0') <> '1' THEN '0' ELSE '1' END")
            .AppendLine("                + CONVERT(VARCHAR,add_datetime,121)) AS add_datetime")
            .AppendLine("           FROM t_keikaku_kanri WITH(READCOMMITTED)")
            .AppendLine("           WHERE keikaku_nendo = @strNendo")
            .AppendLine("           GROUP BY kameiten_cd")
            .AppendLine("                   ,keikaku_nendo")
            .AppendLine("                   ,keikaku_kanri_syouhin_cd)AS TKK_KEY ")
            .AppendLine("        ON TKK.kameiten_cd = TKK_KEY.kameiten_cd")
            .AppendLine("        AND TKK_KEY.keikaku_nendo = TKK.keikaku_nendo ")
            .AppendLine("        AND TKK_KEY.keikaku_kanri_syouhin_cd = TKK.keikaku_kanri_syouhin_cd")
            .AppendLine("        AND TKK_KEY.add_datetime = CASE WHEN ISNULL(CONVERT(VARCHAR,TKK.keikaku_kakutei_flg),'0') <> '1' THEN '0' ELSE '1' END")
            .AppendLine("                                    + CONVERT(VARCHAR,TKK.add_datetime,121) ")
            .AppendLine("       INNER JOIN m_keikaku_kameiten AS MKK WITH(READCOMMITTED) ")
            .AppendLine("           ON TKK.kameiten_cd = MKK.kameiten_cd ")
            .AppendLine("           AND TKK.keikaku_nendo = MKK.keikaku_nendo ")

            .AppendLine("       GROUP BY ")
            .AppendLine("           TKK.kameiten_cd")
            .AppendLine("          ,TKK.keikaku_nendo")
            .AppendLine("          ,TKK.bunbetu_cd")
            .AppendLine("          ,TKK.keikaku_kanri_syouhin_cd) AS SUB_T4")
            .AppendLine("    ON SUB_T.kameiten_cd = SUB_T4.kameiten_cd")
            .AppendLine("    AND SUB_T.keikaku_nendo = SUB_T4.keikaku_nendo ")
            .AppendLine("    AND SUB_T.keikaku_kanri_syouhin_cd = SUB_T4.keikaku_kanri_syouhin_cd")

            .AppendLine("   INNER JOIN")
            .AppendLine("       m_keikaku_kanri_syouhin AS MKKS WITH(READCOMMITTED)")             '--�v��Ǘ��p_���i�}�X�^
            .AppendLine("       ON SUB_T.keikaku_nendo = MKKS.keikaku_nendo")
            .AppendLine("       AND SUB_T.keikaku_kanri_syouhin_cd = MKKS.keikaku_kanri_syouhin_cd")

            .AppendLine("   INNER JOIN ")
            .AppendLine("       m_keikakuyou_meisyou AS MKM WITH(READCOMMITTED)")                 '--�v��p_���̃}�X�^
            .AppendLine("       ON MKKS.bunbetu_cd = CONVERT(VARCHAR(10),MKM.code)")
            .AppendLine("       AND MKM.meisyou_syubetu='15'")

            If (Not strSitenCd.Equals(String.Empty)) Then
                .AppendLine("   INNER JOIN")
                .AppendLine("       m_busyo_kanri AS MBK WITH(READCOMMITTED)")           '--�����Ǘ��}�X�^
                .AppendLine("       ON MKK.busyo_cd = MBK.busyo_cd")
                .AppendLine("       AND MBK.sosiki_level = '4'")
            End If
            If (Not strTodouhukenCd.Equals(String.Empty)) Then
                .AppendLine("   INNER JOIN")
                .AppendLine("       m_todoufuken AS MT WITH(READCOMMITTED)")             '--�s���{���}�X�^
                .AppendLine("       ON MKK.todouhuken_cd = MT.todouhuken_cd")
            ElseIf (Not strEigyousyoBusyoCd.Equals(String.Empty)) Then
                .AppendLine("   INNER JOIN")
                .AppendLine("       m_busyo_kanri AS MBK1 WITH(READCOMMITTED)")          '--�����Ǘ��}�X�^
                .AppendLine("       ON MKK.eigyousyo_busyo_cd = MBK1.busyo_cd")
                .AppendLine("       AND MBK1.sosiki_level = '5'")
            ElseIf (Not strKeiretuCd.Equals(String.Empty)) Then
                .AppendLine("   INNER JOIN")
                .AppendLine("       (SELECT DISTINCT")
                .AppendLine("            keiretu_cd")
                .AppendLine("           ,keiretu_mei")
                .AppendLine("       FROM")
                .AppendLine("           m_keiretu WITH(READCOMMITTED)")
                .AppendLine("       WHERE")
                .AppendLine("               1 = 1")
                If Not strKeiretuCd.Equals("ALL") Then
                    .AppendLine("       AND keiretu_cd = @strKeiretuCd")                   '--���hid�D�n����
                End If
                .AppendLine("       ) AS MK")
                .AppendLine("       ON MKK.keiretu_cd = MK.keiretu_cd")
            ElseIf (Not strEigyouTantousyaId.Equals(String.Empty)) Then
                .AppendLine("   INNER JOIN")
                .AppendLine("       (SELECT  ")
                .AppendLine("           login_user_id ")
                .AppendLine("           ,DisplayName")
                .AppendLine("       FROM")
                .AppendLine("           m_jiban_ninsyou msyou  WITH(READCOMMITTED)")              '�n�ՔF�؃}�X�^
                .AppendLine("       INNER JOIN ")
                .AppendLine("           m_jhs_mailbox  mbox WITH(READCOMMITTED)")            '�Ј��A�J�E���g���}�X�^
                .AppendLine("       ON")
                .AppendLine("           msyou.login_user_id=mbox.PrimaryWindowsNTAccount) AS SUB_MAN ")
                .AppendLine("       ON MKK.eigyou_tantousya_id = SUB_MAN.login_user_id")
            ElseIf (Not strKameitenCd.Equals(String.Empty)) Then
                .AppendLine("   INNER JOIN")
                .AppendLine("       m_kameiten AS MK1 WITH(READCOMMITTED)")             '--�����X�}�X�^
                .AppendLine("       ON MKK.kameiten_cd = MK1.kameiten_cd")
            End If

            .AppendLine("WHERE")
            .AppendLine("   MKK.keikaku_nendo = @strNendo")                    '--�v��_�N�x
            If (Not strSitenCd.Equals(String.Empty)) AndAlso (Not strSitenCd.Equals("ALL")) Then
                .AppendLine("AND MKK.busyo_cd = @strSitenCd")                       '--���hid�D�x�X�R�[�h
            ElseIf (Not strTodouhukenCd.Equals(String.Empty)) AndAlso (Not strTodouhukenCd.Equals("ALL")) Then
                .AppendLine("AND MKK.todouhuken_cd = @strTodouhukenCd")             '--���hid�D�s���{���R�[�h
            ElseIf (Not strEigyousyoBusyoCd.Equals(String.Empty)) AndAlso (Not strEigyousyoBusyoCd.Equals("ALL")) Then
                .AppendLine("AND MKK.eigyousyo_busyo_cd = @strEigyousyoBusyoCd")    '--���hid�D�c�Ə��R�[�h
            ElseIf (Not strKeiretuCd.Equals(String.Empty)) AndAlso (Not strKeiretuCd.Equals("ALL")) Then
                .AppendLine("AND MKK.keiretu_cd = @strKeiretuCd")                   '--���hid�D�n����
            ElseIf (Not strEigyouTantousyaId.Equals(String.Empty)) AndAlso (Not strEigyouTantousyaId.Equals("ALL")) Then
                .AppendLine("AND MKK.eigyou_tantousya_id = @strEigyouTantousyaId")  '--���hid�D�c�ƃ}���R�[�h
            ElseIf (Not strKameitenCd.Equals(String.Empty)) AndAlso (Not strKameitenCd.Equals("ALL")) Then
                .AppendLine("AND MKK.kameiten_cd = @strKameitenCd")                 '--���hid�D�o�^���Ǝ҃R�[�h
            End If

            .AppendLine("   AND MKK.eigyou_kbn IN ").Append(strEigyouKbn)

            .AppendLine("GROUP BY ")
            If (Not strSitenCd.Equals(String.Empty)) Then
                .AppendLine("    MBK.busyo_mei")
                .AppendLine("   ,MBK.busyo_cd")
            ElseIf (Not strTodouhukenCd.Equals(String.Empty)) Then
                .AppendLine("    MT.todouhuken_mei")
                .AppendLine("   ,MT.todouhuken_cd")
            ElseIf (Not strEigyousyoBusyoCd.Equals(String.Empty)) Then
                .AppendLine("    MBK1.busyo_mei")
                .AppendLine("   ,MBK1.busyo_cd")
            ElseIf (Not strKeiretuCd.Equals(String.Empty)) Then
                .AppendLine("    MK.keiretu_mei")
                .AppendLine("   ,MK.keiretu_cd")
            ElseIf (Not strEigyouTantousyaId.Equals(String.Empty)) Then
                .AppendLine("    SUB_MAN.DisplayName")
                .AppendLine("   ,SUB_MAN.login_user_id")
            ElseIf (Not strKameitenCd.Equals(String.Empty)) Then
                .AppendLine("    MK1.kameiten_mei1")
                .AppendLine("   ,MK1.kameiten_cd")
            End If
            .AppendLine("   ,MKKS.bunbetu_cd")
            .AppendLine("   ,MKM.meisyou")
            .AppendLine("   ,MKM.hyouji_jyun")
            .AppendLine("   ,SUB_T.keikaku_kanri_syouhin_cd")
            .AppendLine("   ,MKKS.keikaku_kanri_syouhin_mei")

            .AppendLine(") AS SUB_DATA")

            .AppendLine("LEFT JOIN")
            .AppendLine("(")
            .AppendLine("   SELECT")
            If (Not strSitenCd.Equals(String.Empty)) Then
                .AppendLine("MKK.busyo_cd")                       '--���hid�D�x�X�R�[�h
            ElseIf (Not strTodouhukenCd.Equals(String.Empty)) Then
                .AppendLine("MKK.todouhuken_cd")             '--���hid�D�s���{���R�[�h
            ElseIf (Not strEigyousyoBusyoCd.Equals(String.Empty)) Then
                .AppendLine("MKK.eigyousyo_busyo_cd")    '--���hid�D�c�Ə��R�[�h
            ElseIf (Not strKeiretuCd.Equals(String.Empty)) Then
                .AppendLine("MKK.keiretu_cd")                   '--���hid�D�n����
            ElseIf (Not strEigyouTantousyaId.Equals(String.Empty)) Then
                .AppendLine("MKK.eigyou_tantousya_id")  '--���hid�D�c�ƃ}���R�[�h
            ElseIf (Not strKameitenCd.Equals(String.Empty)) Then
                .AppendLine("MKK.kameiten_cd")                 '--���hid�D�o�^���Ǝ҃R�[�h
            End If
            .AppendLine("       ,CONVERT(VARCHAR(20),CAST(AVG(ISNULL(TNHK.koj_hantei_ritu,0)) AS NUMERIC(38,1))) + '%' AS koujiHanteiritu")
            .AppendLine("       ,CONVERT(VARCHAR(20),CAST(AVG(ISNULL(TNHK.koj_jyuchuu_ritu,0)) AS NUMERIC(38,1))) + '%' AS koujiJyutyuuritu")
            .AppendLine("       ,CONVERT(VARCHAR(20),CAST(AVG(ISNULL(TNHK.tyoku_koj_ritu,0)) AS NUMERIC(38,1))) + '%' AS tyokuKoujiritu")
            .AppendLine("   FROM")
            .AppendLine("       t_nendo_hiritu_kanri AS TNHK WITH(READCOMMITTED)")
            .AppendLine("       INNER JOIN")
            .AppendLine("       m_keikaku_kameiten AS MKK WITH(READCOMMITTED)")
            .AppendLine("       ON ")
            .AppendLine("           MKK.kameiten_cd = TNHK.kameiten_cd")
            .AppendLine("           AND ")
            .AppendLine("           MKK.keikaku_nendo = TNHK.keikaku_nendo")
            .AppendLine("   WHERE")
            .AppendLine("       TNHK.keikaku_nendo = @strNendo")
            If (Not strSitenCd.Equals(String.Empty)) AndAlso (Not strSitenCd.Equals("ALL")) Then
                .AppendLine(" AND MKK.busyo_cd = @strSitenCd")                       '--���hid�D�x�X�R�[�h
            ElseIf (Not strTodouhukenCd.Equals(String.Empty)) AndAlso (Not strTodouhukenCd.Equals("ALL")) Then
                .AppendLine("AND MKK.todouhuken_cd = @strTodouhukenCd")             '--���hid�D�s���{���R�[�h
            ElseIf (Not strEigyousyoBusyoCd.Equals(String.Empty)) AndAlso (Not strEigyousyoBusyoCd.Equals("ALL")) Then
                .AppendLine("AND MKK.eigyousyo_busyo_cd = @strEigyousyoBusyoCd")    '--���hid�D�c�Ə��R�[�h
            ElseIf (Not strKeiretuCd.Equals(String.Empty)) AndAlso (Not strKeiretuCd.Equals("ALL")) Then
                .AppendLine("AND MKK.keiretu_cd = @strKeiretuCd")                   '--���hid�D�n����
            ElseIf (Not strEigyouTantousyaId.Equals(String.Empty)) AndAlso (Not strEigyouTantousyaId.Equals("ALL")) Then
                .AppendLine("AND MKK.eigyou_tantousya_id = @strEigyouTantousyaId")  '--���hid�D�c�ƃ}���R�[�h
            ElseIf (Not strKameitenCd.Equals(String.Empty)) AndAlso (Not strKameitenCd.Equals("ALL")) Then
                .AppendLine("AND MKK.kameiten_cd = @strKameitenCd")                 '--���hid�D�o�^���Ǝ҃R�[�h
            End If
            .AppendLine("   AND MKK.eigyou_kbn IN ").Append(strEigyouKbn)

            If (Not strSitenCd.Equals(String.Empty)) Then
                .AppendLine("GROUP BY MKK.busyo_cd")                       '--���hid�D�x�X�R�[�h
            ElseIf (Not strTodouhukenCd.Equals(String.Empty)) Then
                .AppendLine("GROUP BY MKK.todouhuken_cd")                  '--���hid�D�s���{���R�[�h
            ElseIf (Not strEigyousyoBusyoCd.Equals(String.Empty)) Then
                .AppendLine("GROUP BY MKK.eigyousyo_busyo_cd")             '--���hid�D�c�Ə��R�[�h
            ElseIf (Not strKeiretuCd.Equals(String.Empty)) Then
                .AppendLine("GROUP BY MKK.keiretu_cd")                     '--���hid�D�n����
            ElseIf (Not strEigyouTantousyaId.Equals(String.Empty)) Then
                .AppendLine("GROUP BY MKK.eigyou_tantousya_id")            '--���hid�D�c�ƃ}���R�[�h
            ElseIf (Not strKameitenCd.Equals(String.Empty)) Then
                .AppendLine("GROUP BY MKK.kameiten_cd")                    '--���hid�D�o�^���Ǝ҃R�[�h
            End If

            .AppendLine(") AS SUB_HIRITU")
            .AppendLine("ON 1=1")
            If (Not strSitenCd.Equals(String.Empty)) Then
                .AppendLine("AND SUB_DATA.busyo_cd = SUB_HIRITU.busyo_cd")                       '--���hid�D�x�X�R�[�h
            ElseIf (Not strTodouhukenCd.Equals(String.Empty)) Then
                .AppendLine("AND SUB_DATA.busyo_cd = SUB_HIRITU.todouhuken_cd")                  '--���hid�D�s���{���R�[�h
            ElseIf (Not strEigyousyoBusyoCd.Equals(String.Empty)) Then
                .AppendLine("AND SUB_DATA.busyo_cd = SUB_HIRITU.eigyousyo_busyo_cd")             '--���hid�D�c�Ə��R�[�h
            ElseIf (Not strKeiretuCd.Equals(String.Empty)) Then
                .AppendLine("AND SUB_DATA.busyo_cd = SUB_HIRITU.keiretu_cd")                    '--���hid�D�n����
            ElseIf (Not strEigyouTantousyaId.Equals(String.Empty)) Then
                .AppendLine("AND SUB_DATA.busyo_cd = SUB_HIRITU.eigyou_tantousya_id")            '--���hid�D�c�ƃ}���R�[�h
            ElseIf (Not strKameitenCd.Equals(String.Empty)) Then
                .AppendLine("AND SUB_DATA.busyo_cd = SUB_HIRITU.kameiten_cd")                    '--���hid�D�o�^���Ǝ҃R�[�h
            End If

            .AppendLine("ORDER BY")
            .AppendLine("	 SUB_DATA.busyo_cd")
            .AppendLine("   ,SUB_DATA.hyouji_jyun")

        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@strSitenCd", SqlDbType.VarChar, 4, strSitenCd))                       '�x�X�R�[�h
        paramList.Add(MakeParam("@strTodouhukenCd", SqlDbType.VarChar, 2, strTodouhukenCd))             '�s���{���R�[�h
        paramList.Add(MakeParam("@strEigyousyoBusyoCd", SqlDbType.VarChar, 4, strEigyousyoBusyoCd))     '�c�Ə������R�[�h
        paramList.Add(MakeParam("@strKeiretuCd", SqlDbType.VarChar, 5, strKeiretuCd))                   '�n��R�[�h
        paramList.Add(MakeParam("@strEigyouTantousyaId", SqlDbType.VarChar, 30, strEigyouTantousyaId))  '�c�ƃ}���R�[�h
        paramList.Add(MakeParam("@strKameitenCd", SqlDbType.VarChar, 8, strKameitenCd))                 '�o�^���Ǝ҃R�[�h
        paramList.Add(MakeParam("@strNendo", SqlDbType.Char, 4, strNendo))                              '�v��_�N�x

        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "dsTukinamiListData", paramList.ToArray())
        Return dsReturn.Tables("dsTukinamiListData")

    End Function

    ''' <summary>
    '''�@��ʃf�[�^���擾����(�c�Ƌ敪��FC��I������)
    ''' </summary>
    ''' <param name="strSitenCd">�x�X�R�[�h</param>
    ''' <param name="strNendo">�N�x</param>
    ''' <param name="intBegin">����</param>
    ''' <param name="intEnd">�I���</param>
    ''' <returns>Data.DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/12/27�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelKakusyuSyukeiFCData(ByVal strSitenCd As String, _
                                           ByVal strNendo As String, _
                                           ByVal intBegin As Integer, _
                                           ByVal intEnd As Integer) As DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                            strSitenCd, _
                                                                                            strNendo, _
                                                                                            intBegin, _
                                                                                            intEnd)

        '�߂�f�[�^�Z�b�g
        Dim dsReturn As New Data.DataSet

        'SQL�R�����g
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With commandTextSb
            .AppendLine("SELECT")
            .AppendLine("    SUB_DATA.Siten")
            .AppendLine("   ,SUB_DATA.busyo_cd")
            .AppendLine("	,SUB_HIRITU.koujiHanteiritu")
            .AppendLine("	,SUB_HIRITU.koujiJyutyuuritu")
            .AppendLine("	,SUB_HIRITU.tyokuKoujiritu")
            .AppendLine("	,SUB_DATA.meisyou")
            .AppendLine("	,SUB_DATA.syouhin_mei")
            .AppendLine("	,SUB_DATA.zennenHeikentanka")
            .AppendLine("	,SUB_DATA.keikakuKensuSyukei")     '�v�挏���W�v
            .AppendLine("	,SUB_DATA.keikakuKingakuSyukei")   '�v����z�W�v
            .AppendLine("	,SUB_DATA.keikakuSoriSyukei ")     '�v��e���W�v
            .AppendLine("	,SUB_DATA.jiltusekiKensuSyukei ")  '���ь����W�v
            .AppendLine("	,SUB_DATA.jiltusekiKingakuSyukei") '���ы��z�W�v
            .AppendLine("	,SUB_DATA.jiltusekiSoriSyukei")    '���ёe���W�v
            .AppendLine("FROM")
            .AppendLine("(SELECT")
            .AppendLine("    MBK.busyo_mei AS Siten")
            .AppendLine("   ,MBK.busyo_cd AS busyo_cd")
            .AppendLine("   ,MKKS.bunbetu_cd")
            .AppendLine("   ,MKM.meisyou AS meisyou")
            .AppendLine("   ,MKM.hyouji_jyun")
            .AppendLine("   ,SUB_T.keikaku_kanri_syouhin_cd")
            .AppendLine("   ,MKKS.keikaku_kanri_syouhin_mei AS syouhin_mei")
            .AppendLine("   ,SUB_T3.zennenHeikentanka")

            '�v�挏���W�v
            .AppendLine("   ,(")
            .AppendLine("    0")
            If intEnd <> 0 Then
                For i As Integer = intBegin To intEnd
                    .AppendLine(" + ISNULL(SUM([" & IIf(i > 12, i - 12, i).ToString & "gatu_keikaku_kensuu]),0)")
                Next
            Else
                .AppendLine("     + ISNULL(SUM([" & IIf(intBegin > 12, intBegin - 12, intBegin).ToString & "gatu_keikaku_kensuu]),0)")
            End If
            .AppendLine(" ) AS keikakuKensuSyukei") '�v�挏���W�v

            '�v����z�W�v
            .AppendLine("   ,(")
            .AppendLine("    0")
            If intEnd <> 0 Then
                For i As Integer = intBegin To intEnd
                    .AppendLine(" + ISNULL(SUM([" & IIf(i > 12, i - 12, i).ToString & "gatu_keikaku_kingaku]),0)")
                Next
            Else
                .AppendLine("     + ISNULL(SUM([" & IIf(intBegin > 12, intBegin - 12, intBegin).ToString & "gatu_keikaku_kingaku]),0)")
            End If
            .AppendLine(" ) AS keikakuKingakuSyukei") '�v����z�W�v

            '�v��e���W�v
            .AppendLine("   ,(")
            .AppendLine("    0")
            If intEnd <> 0 Then
                For i As Integer = intBegin To intEnd
                    .AppendLine(" + ISNULL(SUM([" & IIf(i > 12, i - 12, i).ToString & "gatu_keikaku_arari]),0)")
                Next
            Else
                .AppendLine("     + ISNULL(SUM([" & IIf(intBegin > 12, intBegin - 12, intBegin).ToString & "gatu_keikaku_arari]),0)")
            End If
            .AppendLine(" ) AS keikakuSoriSyukei") '�v��e���W�v

            '���ь����W�v
            .AppendLine("   ,(")
            .AppendLine("    0")
            If intEnd <> 0 Then
                For i As Integer = intBegin To intEnd
                    .AppendLine(" + ISNULL(SUM([" & IIf(i > 12, i - 12, i).ToString & "gatu_jisseki_kensuu]),0)")
                Next
            Else
                .AppendLine("     + ISNULL(SUM([" & IIf(intBegin > 12, intBegin - 12, intBegin).ToString & "gatu_jisseki_kensuu]),0)")
            End If
            .AppendLine(" ) AS jiltusekiKensuSyukei") '���ь����W�v

            '���ы��z�W�v
            .AppendLine("   ,(")
            .AppendLine("    0")
            If intEnd <> 0 Then
                For i As Integer = intBegin To intEnd
                    .AppendLine(" + ISNULL(SUM([" & IIf(i > 12, i - 12, i).ToString & "gatu_jisseki_kingaku]),0)")
                Next
            Else
                .AppendLine("     + ISNULL(SUM([" & IIf(intBegin > 12, intBegin - 12, intBegin).ToString & "gatu_jisseki_kingaku]),0)")
            End If
            .AppendLine(" ) AS jiltusekiKingakuSyukei") '���ы��z�W�v

            '���ёe���W�v
            .AppendLine("   ,(")
            .AppendLine("    0")
            If intEnd <> 0 Then
                For i As Integer = intBegin To intEnd
                    .AppendLine(" + ISNULL(SUM([" & IIf(i > 12, i - 12, i).ToString & "gatu_jisseki_arari]),0)")
                Next
            Else
                .AppendLine("     + ISNULL(SUM([" & IIf(intBegin > 12, intBegin - 12, intBegin).ToString & "gatu_jisseki_arari]),0)")
            End If
            .AppendLine(" ) AS jiltusekiSoriSyukei") '���ёe���W�v
            'End If
            .AppendLine("FROM ")
            .AppendLine("       (SELECT")
            .AppendLine("           busyo_cd")
            .AppendLine("           ,keikaku_kanri_syouhin_cd")
            .AppendLine("           ,keikaku_nendo")
            .AppendLine("       FROM")
            .AppendLine("           t_fc_keikaku_kanri WITH(READCOMMITTED)")
            .AppendLine("       WHERE")
            .AppendLine("           keikaku_nendo = @strNendo")
            If (Not strSitenCd.Equals(String.Empty)) AndAlso (Not strSitenCd.Equals("ALL")) Then
                .AppendLine("       AND busyo_cd = @strSitenCd")
            End If
            .AppendLine("       GROUP BY")
            .AppendLine("           busyo_cd")
            .AppendLine("           ,keikaku_kanri_syouhin_cd")
            .AppendLine("           ,keikaku_nendo")
            .AppendLine("       UNION")
            .AppendLine("       SELECT")
            .AppendLine("           MKK.busyo_cd")
            .AppendLine("           ,TJK.keikaku_kanri_syouhin_cd")
            .AppendLine("           ,TJK.keikaku_nendo")
            .AppendLine("       FROM")
            .AppendLine("           t_jisseki_kanri AS TJK WITH(READCOMMITTED)")
            .AppendLine("       INNER JOIN m_keikaku_kameiten AS MKK")
            .AppendLine("           ON TJK.kameiten_cd = MKK.kameiten_cd")
            .AppendLine("          AND TJK.keikaku_nendo = MKK.keikaku_nendo")
            .AppendLine("       WHERE")
            .AppendLine("           TJK.keikaku_nendo = @strNendo")
            .AppendLine("       AND MKK.eigyou_kbn = '4'")
            If (Not strSitenCd.Equals(String.Empty)) AndAlso (Not strSitenCd.Equals("ALL")) Then
                .AppendLine("       AND busyo_cd = @strSitenCd")
            End If
            .AppendLine("       GROUP BY")
            .AppendLine("           MKK.busyo_cd")
            .AppendLine("           ,TJK.keikaku_kanri_syouhin_cd")
            .AppendLine("           ,TJK.keikaku_nendo) AS SUB_T")
            .AppendLine("   LEFT JOIN")
            .AppendLine("       (SELECT")
            .AppendLine("             MKK.busyo_cd")
            .AppendLine("            ,TJK.keikaku_kanri_syouhin_cd")
            .AppendLine("            ,TJK.keikaku_nendo")
            .AppendLine("            ,CONVERT(DECIMAL(15,0),ROUND(AVG(ISNULL(CONVERT(DECIMAL,TJK.zennen_heikin_tanka),0)),0)) AS zennenHeikentanka") '--�O�N_���ϒP��
            '.AppendLine("            ,AVG(TJK.zennen_heikin_tanka) AS zennenHeikentanka")
            .AppendLine("            ,SUM([4gatu_jisseki_kensuu] ) AS [4gatu_jisseki_kensuu]")
            .AppendLine("            ,SUM([4gatu_jisseki_kingaku] ) AS [4gatu_jisseki_kingaku] ")
            .AppendLine("            ,SUM([4gatu_jisseki_arari] ) AS [4gatu_jisseki_arari] ")
            .AppendLine("            ,SUM([5gatu_jisseki_kensuu] ) AS [5gatu_jisseki_kensuu] ")
            .AppendLine("            ,SUM([5gatu_jisseki_kingaku] ) AS [5gatu_jisseki_kingaku] ")
            .AppendLine("            ,SUM([5gatu_jisseki_arari] ) AS [5gatu_jisseki_arari] ")
            .AppendLine("            ,SUM([6gatu_jisseki_kensuu] ) AS [6gatu_jisseki_kensuu] ")
            .AppendLine("            ,SUM([6gatu_jisseki_kingaku] ) AS [6gatu_jisseki_kingaku] ")
            .AppendLine("            ,SUM([6gatu_jisseki_arari] ) AS [6gatu_jisseki_arari] ")
            .AppendLine("            ,SUM([7gatu_jisseki_kensuu] ) AS [7gatu_jisseki_kensuu] ")
            .AppendLine("            ,SUM([7gatu_jisseki_kingaku] ) AS [7gatu_jisseki_kingaku] ")
            .AppendLine("            ,SUM([7gatu_jisseki_arari] ) AS [7gatu_jisseki_arari] ")
            .AppendLine("            ,SUM([8gatu_jisseki_kensuu] ) AS [8gatu_jisseki_kensuu] ")
            .AppendLine("            ,SUM([8gatu_jisseki_kingaku] ) AS [8gatu_jisseki_kingaku] ")
            .AppendLine("            ,SUM([8gatu_jisseki_arari] ) AS [8gatu_jisseki_arari] ")
            .AppendLine("            ,SUM([9gatu_jisseki_kensuu] ) AS [9gatu_jisseki_kensuu] ")
            .AppendLine("            ,SUM([9gatu_jisseki_kingaku] ) AS [9gatu_jisseki_kingaku] ")
            .AppendLine("            ,SUM([9gatu_jisseki_arari] ) AS [9gatu_jisseki_arari] ")
            .AppendLine("            ,SUM([10gatu_jisseki_kensuu] ) AS [10gatu_jisseki_kensuu] ")
            .AppendLine("            ,SUM([10gatu_jisseki_kingaku] ) AS [10gatu_jisseki_kingaku] ")
            .AppendLine("            ,SUM([10gatu_jisseki_arari] ) AS [10gatu_jisseki_arari] ")
            .AppendLine("            ,SUM([11gatu_jisseki_kensuu] ) AS [11gatu_jisseki_kensuu] ")
            .AppendLine("            ,SUM([11gatu_jisseki_kingaku] ) AS [11gatu_jisseki_kingaku] ")
            .AppendLine("            ,SUM([11gatu_jisseki_arari] ) AS [11gatu_jisseki_arari] ")
            .AppendLine("            ,SUM([12gatu_jisseki_kensuu] ) AS [12gatu_jisseki_kensuu] ")
            .AppendLine("            ,SUM([12gatu_jisseki_kingaku] ) AS [12gatu_jisseki_kingaku] ")
            .AppendLine("            ,SUM([12gatu_jisseki_arari] ) AS [12gatu_jisseki_arari] ")
            .AppendLine("            ,SUM([1gatu_jisseki_kensuu] ) AS [1gatu_jisseki_kensuu] ")
            .AppendLine("            ,SUM([1gatu_jisseki_kingaku] ) AS [1gatu_jisseki_kingaku] ")
            .AppendLine("            ,SUM([1gatu_jisseki_arari] ) AS [1gatu_jisseki_arari] ")
            .AppendLine("            ,SUM([2gatu_jisseki_kensuu] ) AS [2gatu_jisseki_kensuu] ")
            .AppendLine("            ,SUM([2gatu_jisseki_kingaku] ) AS [2gatu_jisseki_kingaku] ")
            .AppendLine("            ,SUM([2gatu_jisseki_arari] ) AS [2gatu_jisseki_arari] ")
            .AppendLine("            ,SUM([3gatu_jisseki_kensuu] ) AS [3gatu_jisseki_kensuu] ")
            .AppendLine("            ,SUM([3gatu_jisseki_kingaku] ) AS [3gatu_jisseki_kingaku] ")
            .AppendLine("            ,SUM([3gatu_jisseki_arari] ) AS [3gatu_jisseki_arari] ")
            .AppendLine("    FROM")
            .AppendLine("         t_jisseki_kanri AS TJK WITH(READCOMMITTED)")
            .AppendLine("       INNER JOIN m_keikaku_kameiten AS MKK")
            .AppendLine("            ON TJK.kameiten_cd = MKK.kameiten_cd")
            .AppendLine("           AND TJK.keikaku_nendo = MKK.keikaku_nendo")
            .AppendLine("    WHERE")
            .AppendLine("           TJK.keikaku_nendo = @strNendo")
            .AppendLine("       AND MKK.eigyou_kbn = '4'")
            .AppendLine("    GROUP BY")
            .AppendLine("         MKK.busyo_cd")
            .AppendLine("        ,TJK.keikaku_kanri_syouhin_cd")
            .AppendLine("        ,TJK.keikaku_nendo) AS SUB_T3")
            .AppendLine("    ON SUB_T.busyo_cd = SUB_T3.busyo_cd")
            .AppendLine("    AND SUB_T.keikaku_nendo = SUB_T3.keikaku_nendo")
            .AppendLine("    AND SUB_T.keikaku_kanri_syouhin_cd = SUB_T3.keikaku_kanri_syouhin_cd")
            .AppendLine(" LEFT JOIN")
            .AppendLine("    (SELECT ")
            .AppendLine("         TFKK.busyo_cd")
            .AppendLine("         ,TFKK.keikaku_nendo")
            .AppendLine("         ,TFKK.bunbetu_cd")
            .AppendLine("         ,TFKK.keikaku_kanri_syouhin_cd")
            .AppendLine("         ,SUM(TFKK.[4gatu_keikaku_kensuu] ) AS [4gatu_keikaku_kensuu] ")
            .AppendLine("         ,SUM(TFKK.[4gatu_keikaku_kingaku] ) AS [4gatu_keikaku_kingaku] ")
            .AppendLine("         ,SUM(TFKK.[4gatu_keikaku_arari] ) AS [4gatu_keikaku_arari] ")
            .AppendLine("         ,SUM(TFKK.[5gatu_keikaku_kensuu] ) AS [5gatu_keikaku_kensuu] ")
            .AppendLine("         ,SUM(TFKK.[5gatu_keikaku_kingaku] ) AS [5gatu_keikaku_kingaku] ")
            .AppendLine("         ,SUM(TFKK.[5gatu_keikaku_arari] ) AS [5gatu_keikaku_arari] ")
            .AppendLine("         ,SUM(TFKK.[6gatu_keikaku_kensuu] ) AS [6gatu_keikaku_kensuu] ")
            .AppendLine("         ,SUM(TFKK.[6gatu_keikaku_kingaku] ) AS [6gatu_keikaku_kingaku] ")
            .AppendLine("         ,SUM(TFKK.[6gatu_keikaku_arari] ) AS [6gatu_keikaku_arari] ")
            .AppendLine("         ,SUM(TFKK.[7gatu_keikaku_kensuu] ) AS [7gatu_keikaku_kensuu] ")
            .AppendLine("         ,SUM(TFKK.[7gatu_keikaku_kingaku] ) AS [7gatu_keikaku_kingaku] ")
            .AppendLine("         ,SUM(TFKK.[7gatu_keikaku_arari] ) AS [7gatu_keikaku_arari] ")
            .AppendLine("         ,SUM(TFKK.[8gatu_keikaku_kensuu] ) AS [8gatu_keikaku_kensuu] ")
            .AppendLine("         ,SUM(TFKK.[8gatu_keikaku_kingaku] ) AS [8gatu_keikaku_kingaku] ")
            .AppendLine("         ,SUM(TFKK.[8gatu_keikaku_arari] ) AS [8gatu_keikaku_arari] ")
            .AppendLine("         ,SUM(TFKK.[9gatu_keikaku_kensuu] ) AS [9gatu_keikaku_kensuu] ")
            .AppendLine("         ,SUM(TFKK.[9gatu_keikaku_kingaku] ) AS [9gatu_keikaku_kingaku] ")
            .AppendLine("         ,SUM(TFKK.[9gatu_keikaku_arari] ) AS [9gatu_keikaku_arari] ")
            .AppendLine("         ,SUM(TFKK.[10gatu_keikaku_kensuu] ) AS [10gatu_keikaku_kensuu] ")
            .AppendLine("         ,SUM(TFKK.[10gatu_keikaku_kingaku] ) AS [10gatu_keikaku_kingaku] ")
            .AppendLine("         ,SUM(TFKK.[10gatu_keikaku_arari] ) AS [10gatu_keikaku_arari] ")
            .AppendLine("         ,SUM(TFKK.[11gatu_keikaku_kensuu] ) AS [11gatu_keikaku_kensuu] ")
            .AppendLine("         ,SUM(TFKK.[11gatu_keikaku_kingaku] ) AS [11gatu_keikaku_kingaku] ")
            .AppendLine("         ,SUM(TFKK.[11gatu_keikaku_arari] ) AS [11gatu_keikaku_arari] ")
            .AppendLine("         ,SUM(TFKK.[12gatu_keikaku_kensuu] ) AS [12gatu_keikaku_kensuu] ")
            .AppendLine("         ,SUM(TFKK.[12gatu_keikaku_kingaku] ) AS [12gatu_keikaku_kingaku] ")
            .AppendLine("         ,SUM(TFKK.[12gatu_keikaku_arari] ) AS [12gatu_keikaku_arari] ")
            .AppendLine("         ,SUM(TFKK.[1gatu_keikaku_kensuu] ) AS [1gatu_keikaku_kensuu] ")
            .AppendLine("         ,SUM(TFKK.[1gatu_keikaku_kingaku] ) AS [1gatu_keikaku_kingaku] ")
            .AppendLine("         ,SUM(TFKK.[1gatu_keikaku_arari] ) AS [1gatu_keikaku_arari] ")
            .AppendLine("         ,SUM(TFKK.[2gatu_keikaku_kensuu] ) AS [2gatu_keikaku_kensuu] ")
            .AppendLine("         ,SUM(TFKK.[2gatu_keikaku_kingaku] ) AS [2gatu_keikaku_kingaku] ")
            .AppendLine("         ,SUM(TFKK.[2gatu_keikaku_arari] ) AS [2gatu_keikaku_arari] ")
            .AppendLine("         ,SUM(TFKK.[3gatu_keikaku_kensuu] ) AS [3gatu_keikaku_kensuu] ")
            .AppendLine("         ,SUM(TFKK.[3gatu_keikaku_kingaku] ) AS [3gatu_keikaku_kingaku] ")
            .AppendLine("         ,SUM(TFKK.[3gatu_keikaku_arari] ) AS [3gatu_keikaku_arari] ")
            .AppendLine("     FROM t_fc_keikaku_kanri AS TFKK WITH(READCOMMITTED)")
            .AppendLine("     INNER JOIN(")
            .AppendLine("        SELECT busyo_cd")
            .AppendLine("               ,keikaku_nendo")
            .AppendLine("               ,keikaku_kanri_syouhin_cd")
            .AppendLine("               ,MAX(CASE WHEN ISNULL(CONVERT(VARCHAR,keikaku_kakutei_flg),'0') <> '1' THEN '0' ELSE '1' END")
            .AppendLine("               + CONVERT(VARCHAR,add_datetime,121)) AS add_datetime")
            .AppendLine("        FROM t_fc_keikaku_kanri WITH(READCOMMITTED)")
            .AppendLine("        WHERE keikaku_nendo = @strNendo")
            .AppendLine("        GROUP BY busyo_cd")
            .AppendLine("                ,keikaku_nendo")
            .AppendLine("                ,keikaku_kanri_syouhin_cd)AS TFKK_KEY")
            .AppendLine("      ON TFKK.busyo_cd = TFKK_KEY.busyo_cd")
            .AppendLine("      AND TFKK_KEY.keikaku_kanri_syouhin_cd = TFKK.keikaku_kanri_syouhin_cd")
            .AppendLine("      AND TFKK_KEY.keikaku_nendo = TFKK.keikaku_nendo ")
            .AppendLine("      AND TFKK_KEY.add_datetime = CASE WHEN ISNULL(CONVERT(VARCHAR,TFKK.keikaku_kakutei_flg),'0') <> '1' THEN '0' ELSE '1' END")
            .AppendLine("                                 + CONVERT(VARCHAR,TFKK.add_datetime,121) ")
            .AppendLine("    GROUP BY ")
            .AppendLine("        TFKK.busyo_cd")
            .AppendLine("       ,TFKK.keikaku_nendo")
            .AppendLine("       ,TFKK.bunbetu_cd")
            .AppendLine("       ,TFKK.keikaku_kanri_syouhin_cd) AS SUB_T4")
            .AppendLine(" ON SUB_T.busyo_cd = SUB_T4.busyo_cd")
            .AppendLine(" AND SUB_T.keikaku_nendo = SUB_T4.keikaku_nendo ")
            .AppendLine(" AND SUB_T.keikaku_kanri_syouhin_cd = SUB_T4.keikaku_kanri_syouhin_cd ")
            .AppendLine("INNER JOIN")
            .AppendLine("     m_keikaku_kanri_syouhin AS MKKS WITH(READCOMMITTED)")
            .AppendLine("     ON SUB_T.keikaku_nendo = MKKS.keikaku_nendo")
            .AppendLine("     AND SUB_T.keikaku_kanri_syouhin_cd = MKKS.keikaku_kanri_syouhin_cd")
            .AppendLine("INNER JOIN ")
            .AppendLine("     m_keikakuyou_meisyou AS MKM WITH(READCOMMITTED)")
            .AppendLine("     ON MKKS.bunbetu_cd = CONVERT(VARCHAR(10),MKM.code)")
            .AppendLine("    AND MKM.meisyou_syubetu='15'")
            .AppendLine("INNER JOIN")
            .AppendLine("     m_busyo_kanri AS MBK WITH(READCOMMITTED)")
            .AppendLine("     ON SUB_T.busyo_cd = MBK.busyo_cd")
            .AppendLine("     AND MBK.sosiki_level = '4'")

            .AppendLine("GROUP BY ")
            .AppendLine("    MBK.busyo_mei")
            .AppendLine("   ,MBK.busyo_cd")
            .AppendLine("   ,MKKS.bunbetu_cd")
            .AppendLine("   ,MKM.meisyou")
            .AppendLine("   ,MKM.hyouji_jyun")
            .AppendLine("   ,SUB_T.keikaku_kanri_syouhin_cd")
            .AppendLine("   ,SUB_T3.zennenHeikentanka")
            .AppendLine("   ,MKKS.keikaku_kanri_syouhin_mei")
            .AppendLine(") AS SUB_DATA")
            .AppendLine("LEFT JOIN")
            .AppendLine("(")
            .AppendLine("   SELECT")
            .AppendLine("       MKK.busyo_cd")
            .AppendLine("      ,CONVERT(VARCHAR(20),CAST(AVG(ISNULL(TNHK.koj_hantei_ritu,0)) AS NUMERIC(38,1))) + '%' AS koujiHanteiritu")
            .AppendLine("      ,CONVERT(VARCHAR(20),CAST(AVG(ISNULL(TNHK.koj_jyuchuu_ritu,0)) AS NUMERIC(38,1))) + '%' AS koujiJyutyuuritu")
            .AppendLine("      ,CONVERT(VARCHAR(20),CAST(AVG(ISNULL(TNHK.tyoku_koj_ritu,0)) AS NUMERIC(38,1))) + '%' AS tyokuKoujiritu")
            .AppendLine("  FROM")
            .AppendLine("      t_nendo_hiritu_kanri AS TNHK WITH(READCOMMITTED)")
            .AppendLine("       INNER JOIN")
            .AppendLine("       m_keikaku_kameiten AS MKK WITH(READCOMMITTED)")
            .AppendLine("       ON ")
            .AppendLine("       MKK.kameiten_cd = TNHK.kameiten_cd")
            .AppendLine("       AND ")
            .AppendLine("       MKK.keikaku_nendo = TNHK.keikaku_nendo")
            .AppendLine("  WHERE")
            .AppendLine("     TNHK.keikaku_nendo = @strNendo")
            If (Not strSitenCd.Equals(String.Empty)) AndAlso (Not strSitenCd.Equals("ALL")) Then
                .AppendLine("     AND MKK.busyo_cd = @strSitenCd")
            End If
            .AppendLine("     AND MKK.eigyou_kbn = '4'")
            .AppendLine(" GROUP BY ")
            .AppendLine("      MKK.busyo_cd")
            .AppendLine(") AS SUB_HIRITU")
            .AppendLine("ON")
            .AppendLine("SUB_DATA.busyo_cd = SUB_HIRITU.busyo_cd")
            .AppendLine("ORDER BY")
            .AppendLine("	 SUB_DATA.busyo_cd")
            .AppendLine("   ,SUB_DATA.hyouji_jyun")

        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@strSitenCd", SqlDbType.VarChar, 4, strSitenCd))                       '�x�X�R�[�h
        paramList.Add(MakeParam("@strNendo", SqlDbType.Char, 4, strNendo))                              '�v��_�N�x

        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "dsTukinamiListFCData", paramList.ToArray())

        Return dsReturn.Tables("dsTukinamiListFCData")

    End Function

    ''' <summary>
    ''' ��ʂ��f�[�^���擾����(�c�Ƌ敪��S�đI������)
    ''' </summary>
    ''' <param name="strSitenCd">�x�X�R�[�h</param>
    ''' <param name="strNendo">�N�x</param>
    ''' <param name="intBegin">����</param>
    ''' <param name="intEnd">�I���</param>
    ''' <param name="strEigyouJisseki">�c�Ƌ敪</param>
    ''' <returns>Data.DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/01/09�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function SelKakusyuSyukeiSubeteData(ByVal strSitenCd As String, _
                                               ByVal strNendo As String, _
                                               ByVal intBegin As Integer, _
                                               ByVal intEnd As Integer, _
                                               ByVal strEigyouJisseki As String) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                            strSitenCd, _
                                                                                            strNendo, _
                                                                                            intBegin, _
                                                                                            intEnd, _
                                                                                            strEigyouJisseki)

        '�߂�f�[�^�Z�b�g
        Dim dsReturn As New Data.DataSet

        'SQL�R�����g
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        Dim strEigyouKbnFc As String
        Dim strEigyouKbnNotFc As String

        strEigyouJisseki = Mid(strEigyouJisseki, 2, strEigyouJisseki.Length)
        strEigyouKbnFc = "('" & strEigyouJisseki.Replace(",", "','") & "')"
        strEigyouKbnNotFc = "('" & strEigyouJisseki.Replace(",4", String.Empty).Replace(",", "','") & "')"

        With commandTextSb
            .AppendLine("SELECT")
            .AppendLine("	SUB_DATA.Siten")
            .AppendLine("	,SUB_DATA.busyo_cd")
            .AppendLine("	,SUB_HIRITU.koujiHanteiritu")
            .AppendLine("	,SUB_HIRITU.koujiJyutyuuritu")
            .AppendLine("	,SUB_HIRITU.tyokuKoujiritu")
            .AppendLine("	,SUB_DATA.meisyou")
            .AppendLine("	,SUB_DATA.syouhin_mei")
            .AppendLine("	,SUB_DATA.zennenHeikentanka")
            .AppendLine("	,SUB_DATA.keikakuKensuSyukei")     '�v�挏���W�v
            .AppendLine("	,SUB_DATA.keikakuKingakuSyukei")   '�v����z�W�v
            .AppendLine("	,SUB_DATA.keikakuSoriSyukei ")     '�v��e���W�v
            .AppendLine("	,SUB_DATA.jiltusekiKensuSyukei ")  '���ь����W�v
            .AppendLine("	,SUB_DATA.jiltusekiKingakuSyukei") '���ы��z�W�v
            .AppendLine("	,SUB_DATA.jiltusekiSoriSyukei")    '���ёe���W�v
            .AppendLine("FROM")
            .AppendLine("	(")
            .AppendLine("	SELECT")
            .AppendLine("	    MBK.busyo_mei AS Siten")
            .AppendLine("	   ,MBK.busyo_cd AS busyo_cd")
            .AppendLine("      ,MKKS.bunbetu_cd")
            .AppendLine("	   ,MKM.meisyou AS meisyou")
            .AppendLine("      ,MKM.hyouji_jyun")
            .AppendLine("      ,SUB_T.keikaku_kanri_syouhin_cd")
            .AppendLine("	   ,MKKS.keikaku_kanri_syouhin_mei AS syouhin_mei")
            .AppendLine("	   ,SUB_T3.zennenHeikentanka")
            '�v�挏���W�v
            .AppendLine("   ,(")
            .AppendLine("    0")
            If intEnd <> 0 Then
                For i As Integer = intBegin To intEnd
                    .AppendLine(" + ISNULL(SUM(SUB_T1.[" & IIf(i > 12, i - 12, i).ToString & "gatu_keikaku_kensuu]),0)")
                    .AppendLine(" + ISNULL(SUM(SUB_T4.[" & IIf(i > 12, i - 12, i).ToString & "gatu_keikaku_kensuu]),0)")
                Next
            Else
                .AppendLine("     + ISNULL(SUM(SUB_T1.[" & IIf(intBegin > 12, intBegin - 12, intBegin).ToString & "gatu_keikaku_kensuu]),0)")
                .AppendLine("     + ISNULL(SUM(SUB_T4.[" & IIf(intBegin > 12, intBegin - 12, intBegin).ToString & "gatu_keikaku_kensuu]),0)")
            End If
            .AppendLine(" ) AS keikakuKensuSyukei") '�v�挏���W�v

            '�v����z�W�v
            .AppendLine("   ,(")
            .AppendLine("    0")
            If intEnd <> 0 Then
                For i As Integer = intBegin To intEnd
                    .AppendLine(" + ISNULL(SUM(SUB_T1.[" & IIf(i > 12, i - 12, i).ToString & "gatu_keikaku_kingaku]),0)")
                    .AppendLine(" + ISNULL(SUM(SUB_T4.[" & IIf(i > 12, i - 12, i).ToString & "gatu_keikaku_kingaku]),0)")
                Next
            Else
                .AppendLine("     + ISNULL(SUM(SUB_T1.[" & IIf(intBegin > 12, intBegin - 12, intBegin).ToString & "gatu_keikaku_kingaku]),0)")
                .AppendLine("     + ISNULL(SUM(SUB_T4.[" & IIf(intBegin > 12, intBegin - 12, intBegin).ToString & "gatu_keikaku_kingaku]),0)")
            End If
            .AppendLine(" ) AS keikakuKingakuSyukei") '�v����z�W�v

            '�v��e���W�v
            .AppendLine("   ,(")
            .AppendLine("    0")
            If intEnd <> 0 Then
                For i As Integer = intBegin To intEnd
                    .AppendLine(" + ISNULL(SUM(SUB_T1.[" & IIf(i > 12, i - 12, i).ToString & "gatu_keikaku_arari]),0)")
                    .AppendLine(" + ISNULL(SUM(SUB_T4.[" & IIf(i > 12, i - 12, i).ToString & "gatu_keikaku_arari]),0)")
                Next
            Else
                .AppendLine("     + ISNULL(SUM(SUB_T1.[" & IIf(intBegin > 12, intBegin - 12, intBegin).ToString & "gatu_keikaku_arari]),0)")
                .AppendLine("     + ISNULL(SUM(SUB_T4.[" & IIf(intBegin > 12, intBegin - 12, intBegin).ToString & "gatu_keikaku_arari]),0)")
            End If
            .AppendLine(" ) AS keikakuSoriSyukei") '�v��e���W�v

            '���ь����W�v
            .AppendLine("   ,(")
            .AppendLine("    0")
            If intEnd <> 0 Then
                For i As Integer = intBegin To intEnd
                    .AppendLine(" + ISNULL(SUM(SUB_T3.[" & IIf(i > 12, i - 12, i).ToString & "gatu_jisseki_kensuu]),0)")
                Next
            Else
                .AppendLine("     + ISNULL(SUM(SUB_T3.[" & IIf(intBegin > 12, intBegin - 12, intBegin).ToString & "gatu_jisseki_kensuu]),0)")
            End If
            .AppendLine(" ) AS jiltusekiKensuSyukei") '���ь����W�v

            '���ы��z�W�v
            .AppendLine("   ,(")
            .AppendLine("    0")
            If intEnd <> 0 Then
                For i As Integer = intBegin To intEnd
                    .AppendLine(" + ISNULL(SUM(SUB_T3.[" & IIf(i > 12, i - 12, i).ToString & "gatu_jisseki_kingaku]),0)")
                Next
            Else
                .AppendLine("     + ISNULL(SUM(SUB_T3.[" & IIf(intBegin > 12, intBegin - 12, intBegin).ToString & "gatu_jisseki_kingaku]),0)")
            End If
            .AppendLine(" ) AS jiltusekiKingakuSyukei") '���ы��z�W�v

            '���ёe���W�v
            .AppendLine("   ,(")
            .AppendLine("    0")
            If intEnd <> 0 Then
                For i As Integer = intBegin To intEnd
                    .AppendLine(" + ISNULL(SUM(SUB_T3.[" & IIf(i > 12, i - 12, i).ToString & "gatu_jisseki_arari]),0)")
                Next
            Else
                .AppendLine("     + ISNULL(SUM(SUB_T3.[" & IIf(intBegin > 12, intBegin - 12, intBegin).ToString & "gatu_jisseki_arari]),0)")
            End If
            .AppendLine(" ) AS jiltusekiSoriSyukei") '���ёe���W�v
            .AppendLine("	FROM ")
            .AppendLine("	(")
            .AppendLine("		 SELECT ")
            .AppendLine("			MKK.busyo_cd")
            .AppendLine("			,MKK.keikaku_nendo")
            .AppendLine("			,TKK.keikaku_kanri_syouhin_cd")
            .AppendLine("		 FROM")
            .AppendLine("			t_keikaku_kanri AS TKK WITH(READCOMMITTED)")
            .AppendLine("			INNER JOIN 	m_keikaku_kameiten AS MKK WITH(READCOMMITTED)")
            .AppendLine("			ON MKK.kameiten_cd = TKK.kameiten_cd")
            .AppendLine("			AND MKK.keikaku_nendo = TKK.keikaku_nendo")
            .AppendLine("		 WHERE")
            .AppendLine("				MKK.keikaku_nendo = @strNendo")
            If (Not strSitenCd.Equals(String.Empty)) AndAlso (Not strSitenCd.Equals("ALL")) Then
                .AppendLine("			AND MKK.busyo_cd = @strSitenCd")
            End If
            .Append("			AND MKK.eigyou_kbn IN ").AppendLine(strEigyouKbnNotFc)
            .AppendLine("		 GROUP BY")
            .AppendLine("			  MKK.busyo_cd")
            .AppendLine("			 ,MKK.keikaku_nendo")
            .AppendLine("			 ,TKK.keikaku_kanri_syouhin_cd")
            .AppendLine("		 UNION")
            .AppendLine("		 SELECT")
            .AppendLine("			 MKK.busyo_cd")
            .AppendLine("			,TJK.keikaku_nendo")
            .AppendLine("			,TJK.keikaku_kanri_syouhin_cd")
            .AppendLine("		 FROM ")
            .AppendLine("			t_jisseki_kanri AS TJK WITH(READCOMMITTED)")
            .AppendLine("			INNER JOIN m_keikaku_kameiten AS MKK WITH(READCOMMITTED)")
            .AppendLine("			ON MKK.kameiten_cd = TJK.kameiten_cd")
            .AppendLine("			AND MKK.keikaku_nendo = TJK.keikaku_nendo")
            .AppendLine("		 WHERE")
            .AppendLine("				MKK.keikaku_nendo = @strNendo")
            If (Not strSitenCd.Equals(String.Empty)) AndAlso (Not strSitenCd.Equals("ALL")) Then
                .AppendLine("			AND MKK.busyo_cd = @strSitenCd")
            End If
            .Append("			    AND MKK.eigyou_kbn IN ").AppendLine(strEigyouKbnFc)
            .AppendLine("		 GROUP BY")
            .AppendLine("			 MKK.busyo_cd")
            .AppendLine("			,TJK.keikaku_nendo")
            .AppendLine("			,TJK.keikaku_kanri_syouhin_cd	")
            .AppendLine("		UNION")
            .AppendLine("		SELECT ")
            .AppendLine("			TFKK.busyo_cd ")
            .AppendLine("			,TFKK.keikaku_nendo ")
            .AppendLine("			,TFKK.keikaku_kanri_syouhin_cd")
            .AppendLine("		FROM t_fc_keikaku_kanri AS TFKK WITH(READCOMMITTED) ")
            .AppendLine("		WHERE TFKK.keikaku_nendo = @strNendo")
            If (Not strSitenCd.Equals(String.Empty)) AndAlso (Not strSitenCd.Equals("ALL")) Then
                .AppendLine("			AND TFKK.busyo_cd = @strSitenCd")
            End If
            .AppendLine("		GROUP BY ")
            .AppendLine("			 TFKK.busyo_cd ")
            .AppendLine("			,TFKK.keikaku_nendo ")
            .AppendLine("			,TFKK.keikaku_kanri_syouhin_cd")
            .AppendLine("	) AS SUB_T")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("	   (SELECT  ")
            .AppendLine("			MKK.busyo_cd")
            .AppendLine("		   ,TKK.keikaku_nendo ")
            .AppendLine("		   ,TKK.keikaku_kanri_syouhin_cd ")
            .AppendLine("		   ,SUM(TKK.[4gatu_keikaku_kensuu] ) AS [4gatu_keikaku_kensuu]    ")
            .AppendLine("		   ,SUM(TKK.[4gatu_keikaku_kingaku] ) AS [4gatu_keikaku_kingaku]  ")
            .AppendLine("		   ,SUM(TKK.[4gatu_keikaku_arari] ) AS [4gatu_keikaku_arari]     ")
            .AppendLine("		   ,SUM(TKK.[5gatu_keikaku_kensuu] ) AS [5gatu_keikaku_kensuu]   ")
            .AppendLine("		   ,SUM(TKK.[5gatu_keikaku_kingaku] ) AS [5gatu_keikaku_kingaku]")
            .AppendLine("		   ,SUM(TKK.[5gatu_keikaku_arari] ) AS [5gatu_keikaku_arari]    ")
            .AppendLine("		   ,SUM(TKK.[6gatu_keikaku_kensuu] ) AS [6gatu_keikaku_kensuu]   ")
            .AppendLine("		   ,SUM(TKK.[6gatu_keikaku_kingaku] ) AS [6gatu_keikaku_kingaku]  ")
            .AppendLine("		   ,SUM(TKK.[6gatu_keikaku_arari] ) AS [6gatu_keikaku_arari]      ")
            .AppendLine("		   ,SUM(TKK.[7gatu_keikaku_kensuu] ) AS [7gatu_keikaku_kensuu]    ")
            .AppendLine("		   ,SUM(TKK.[7gatu_keikaku_kingaku] ) AS [7gatu_keikaku_kingaku]  ")
            .AppendLine("		   ,SUM(TKK.[7gatu_keikaku_arari] ) AS [7gatu_keikaku_arari]    ")
            .AppendLine("		   ,SUM(TKK.[8gatu_keikaku_kensuu] ) AS [8gatu_keikaku_kensuu]  ")
            .AppendLine("		   ,SUM(TKK.[8gatu_keikaku_kingaku] ) AS [8gatu_keikaku_kingaku] ")
            .AppendLine("		   ,SUM(TKK.[8gatu_keikaku_arari] ) AS [8gatu_keikaku_arari]   ")
            .AppendLine("		   ,SUM(TKK.[9gatu_keikaku_kensuu] ) AS [9gatu_keikaku_kensuu]  ")
            .AppendLine("		   ,SUM(TKK.[9gatu_keikaku_kingaku] ) AS [9gatu_keikaku_kingaku]  ")
            .AppendLine("		   ,SUM(TKK.[9gatu_keikaku_arari] ) AS [9gatu_keikaku_arari]       ")
            .AppendLine("		   ,SUM(TKK.[10gatu_keikaku_kensuu] ) AS [10gatu_keikaku_kensuu]  ")
            .AppendLine("		   ,SUM(TKK.[10gatu_keikaku_kingaku] ) AS [10gatu_keikaku_kingaku]  ")
            .AppendLine("		   ,SUM(TKK.[10gatu_keikaku_arari] ) AS [10gatu_keikaku_arari]     ")
            .AppendLine("		   ,SUM(TKK.[11gatu_keikaku_kensuu] ) AS [11gatu_keikaku_kensuu]    ")
            .AppendLine("		   ,SUM(TKK.[11gatu_keikaku_kingaku] ) AS [11gatu_keikaku_kingaku] ")
            .AppendLine("		   ,SUM(TKK.[11gatu_keikaku_arari] ) AS [11gatu_keikaku_arari]     ")
            .AppendLine("		   ,SUM(TKK.[12gatu_keikaku_kensuu] ) AS [12gatu_keikaku_kensuu]   ")
            .AppendLine("		   ,SUM(TKK.[12gatu_keikaku_kingaku] ) AS [12gatu_keikaku_kingaku]  ")
            .AppendLine("		   ,SUM(TKK.[12gatu_keikaku_arari] ) AS [12gatu_keikaku_arari]     ")
            .AppendLine("		   ,SUM(TKK.[1gatu_keikaku_kensuu] ) AS [1gatu_keikaku_kensuu]    ")
            .AppendLine("		   ,SUM(TKK.[1gatu_keikaku_kingaku] ) AS [1gatu_keikaku_kingaku]   ")
            .AppendLine("		   ,SUM(TKK.[1gatu_keikaku_arari] ) AS [1gatu_keikaku_arari]     ")
            .AppendLine("		   ,SUM(TKK.[2gatu_keikaku_kensuu] ) AS [2gatu_keikaku_kensuu]    ")
            .AppendLine("		   ,SUM(TKK.[2gatu_keikaku_kingaku] ) AS [2gatu_keikaku_kingaku]  ")
            .AppendLine("		   ,SUM(TKK.[2gatu_keikaku_arari] ) AS [2gatu_keikaku_arari]   ")
            .AppendLine("		   ,SUM(TKK.[3gatu_keikaku_kensuu] ) AS [3gatu_keikaku_kensuu]  ")
            .AppendLine("		   ,SUM(TKK.[3gatu_keikaku_kingaku] ) AS [3gatu_keikaku_kingaku] ")
            .AppendLine("		   ,SUM(TKK.[3gatu_keikaku_arari] ) AS [3gatu_keikaku_arari]   ")
            .AppendLine("	   FROM t_keikaku_kanri AS TKK WITH(READCOMMITTED)     ")
            .AppendLine("	   INNER JOIN( ")
            .AppendLine("		       SELECT ")
            .AppendLine("			    	  kameiten_cd ")
            .AppendLine("			    	 ,keikaku_nendo ")
            .AppendLine("			    	 ,keikaku_kanri_syouhin_cd")
            .AppendLine("			    	 ,MAX(CASE WHEN ISNULL(CONVERT(VARCHAR,keikaku_kakutei_flg),'0') <> '1' THEN '0' ELSE '1' END ")
            .AppendLine("			    	  + CONVERT(VARCHAR,add_datetime,121)) AS add_datetime ")
            .AppendLine("		       FROM t_keikaku_kanri WITH(READCOMMITTED) ")
            .AppendLine("		       WHERE keikaku_nendo = @strNendo")
            .AppendLine("		       GROUP BY ")
            .AppendLine("			    		 kameiten_cd ")
            .AppendLine("			    	    ,keikaku_nendo")
            .AppendLine("			    	    ,keikaku_kanri_syouhin_cd)AS TKK_KEY ")
            .AppendLine("		ON TKK.kameiten_cd = TKK_KEY.kameiten_cd ")
            .AppendLine("		AND TKK_KEY.keikaku_nendo = TKK.keikaku_nendo  ")
            .AppendLine("		AND TKK_KEY.keikaku_kanri_syouhin_cd = TKK.keikaku_kanri_syouhin_cd  ")
            .AppendLine("		AND TKK_KEY.add_datetime = CASE WHEN ISNULL(CONVERT(VARCHAR,TKK.keikaku_kakutei_flg),'0') <> '1' THEN '0' ELSE '1' END ")
            .AppendLine("								   + CONVERT(VARCHAR,TKK.add_datetime,121) ")
            .AppendLine("		INNER JOIN m_keikaku_kameiten AS MKK ")
            .AppendLine("			ON TKK.kameiten_cd = MKK.kameiten_cd ")
            .AppendLine("			AND TKK.keikaku_nendo = MKK.keikaku_nendo ")
            .AppendLine("			AND MKK.eigyou_kbn IN ").Append(strEigyouKbnNotFc)
            .AppendLine("		WHERE ")
            .AppendLine("			    TKK.keikaku_nendo = @strNendo ")
            If (Not strSitenCd.Equals(String.Empty)) AndAlso (Not strSitenCd.Equals("ALL")) Then
                .AppendLine("		    AND MKK.busyo_cd = @strSitenCd ")
            End If
            .AppendLine("	    GROUP BY  ")
            .AppendLine("			MKK.busyo_cd")
            .AppendLine("		   ,TKK.keikaku_nendo")
            .AppendLine("		   ,TKK.keikaku_kanri_syouhin_cd) AS SUB_T1")
            .AppendLine("	ON SUB_T.busyo_cd = SUB_T1.busyo_cd ")
            .AppendLine("	AND SUB_T.keikaku_nendo = SUB_T1.keikaku_nendo")
            .AppendLine("	AND SUB_T.keikaku_kanri_syouhin_cd = SUB_T1.keikaku_kanri_syouhin_cd")
            .AppendLine("	LEFT JOIN ")
            .AppendLine("		(SELECT ")
            .AppendLine("		      MKK.busyo_cd")
            .AppendLine("			 ,TJK.keikaku_kanri_syouhin_cd ")
            .AppendLine("			 ,TJK.keikaku_nendo ")
            .AppendLine("            ,CONVERT(DECIMAL(15,0),ROUND(AVG(ISNULL(CONVERT(DECIMAL,TJK.zennen_heikin_tanka),0)),0)) AS zennenHeikentanka") '--�O�N_���ϒP��
            '.AppendLine("			 ,AVG(TJK.zennen_heikin_tanka) AS zennenHeikentanka ")
            .AppendLine("			 ,SUM(TJK.[4gatu_jisseki_kensuu] ) AS [4gatu_jisseki_kensuu] ")
            .AppendLine("			 ,SUM(TJK.[4gatu_jisseki_kingaku] ) AS [4gatu_jisseki_kingaku]  ")
            .AppendLine("			 ,SUM(TJK.[4gatu_jisseki_arari] ) AS [4gatu_jisseki_arari]  ")
            .AppendLine("			 ,SUM(TJK.[5gatu_jisseki_kensuu] ) AS [5gatu_jisseki_kensuu]  ")
            .AppendLine("			 ,SUM(TJK.[5gatu_jisseki_kingaku] ) AS [5gatu_jisseki_kingaku]  ")
            .AppendLine("			 ,SUM(TJK.[5gatu_jisseki_arari] ) AS [5gatu_jisseki_arari]  ")
            .AppendLine("			 ,SUM(TJK.[6gatu_jisseki_kensuu] ) AS [6gatu_jisseki_kensuu]  ")
            .AppendLine("			 ,SUM(TJK.[6gatu_jisseki_kingaku] ) AS [6gatu_jisseki_kingaku]  ")
            .AppendLine("			 ,SUM(TJK.[6gatu_jisseki_arari] ) AS [6gatu_jisseki_arari]  ")
            .AppendLine("			 ,SUM(TJK.[7gatu_jisseki_kensuu] ) AS [7gatu_jisseki_kensuu]  ")
            .AppendLine("			 ,SUM(TJK.[7gatu_jisseki_kingaku] ) AS [7gatu_jisseki_kingaku]  ")
            .AppendLine("			 ,SUM(TJK.[7gatu_jisseki_arari] ) AS [7gatu_jisseki_arari]  ")
            .AppendLine("			 ,SUM(TJK.[8gatu_jisseki_kensuu] ) AS [8gatu_jisseki_kensuu]  ")
            .AppendLine("			 ,SUM(TJK.[8gatu_jisseki_kingaku] ) AS [8gatu_jisseki_kingaku]  ")
            .AppendLine("			 ,SUM(TJK.[8gatu_jisseki_arari] ) AS [8gatu_jisseki_arari]  ")
            .AppendLine("			 ,SUM(TJK.[9gatu_jisseki_kensuu] ) AS [9gatu_jisseki_kensuu]  ")
            .AppendLine("			 ,SUM(TJK.[9gatu_jisseki_kingaku] ) AS [9gatu_jisseki_kingaku]  ")
            .AppendLine("			 ,SUM(TJK.[9gatu_jisseki_arari] ) AS [9gatu_jisseki_arari]  ")
            .AppendLine("			 ,SUM(TJK.[10gatu_jisseki_kensuu] ) AS [10gatu_jisseki_kensuu]  ")
            .AppendLine("			 ,SUM(TJK.[10gatu_jisseki_kingaku] ) AS [10gatu_jisseki_kingaku]  ")
            .AppendLine("			 ,SUM(TJK.[10gatu_jisseki_arari] ) AS [10gatu_jisseki_arari]  ")
            .AppendLine("			 ,SUM(TJK.[11gatu_jisseki_kensuu] ) AS [11gatu_jisseki_kensuu]  ")
            .AppendLine("			 ,SUM(TJK.[11gatu_jisseki_kingaku] ) AS [11gatu_jisseki_kingaku]  ")
            .AppendLine("			 ,SUM(TJK.[11gatu_jisseki_arari] ) AS [11gatu_jisseki_arari]  ")
            .AppendLine("			 ,SUM(TJK.[12gatu_jisseki_kensuu] ) AS [12gatu_jisseki_kensuu]  ")
            .AppendLine("			 ,SUM(TJK.[12gatu_jisseki_kingaku] ) AS [12gatu_jisseki_kingaku]  ")
            .AppendLine("			 ,SUM(TJK.[12gatu_jisseki_arari] ) AS [12gatu_jisseki_arari]  ")
            .AppendLine("			 ,SUM(TJK.[1gatu_jisseki_kensuu] ) AS [1gatu_jisseki_kensuu]  ")
            .AppendLine("			 ,SUM(TJK.[1gatu_jisseki_kingaku] ) AS [1gatu_jisseki_kingaku]  ")
            .AppendLine("			 ,SUM(TJK.[1gatu_jisseki_arari] ) AS [1gatu_jisseki_arari]  ")
            .AppendLine("			 ,SUM(TJK.[2gatu_jisseki_kensuu] ) AS [2gatu_jisseki_kensuu]  ")
            .AppendLine("			 ,SUM(TJK.[2gatu_jisseki_kingaku] ) AS [2gatu_jisseki_kingaku]  ")
            .AppendLine("			 ,SUM(TJK.[2gatu_jisseki_arari] ) AS [2gatu_jisseki_arari]  ")
            .AppendLine("			 ,SUM(TJK.[3gatu_jisseki_kensuu] ) AS [3gatu_jisseki_kensuu]  ")
            .AppendLine("			 ,SUM(TJK.[3gatu_jisseki_kingaku] ) AS [3gatu_jisseki_kingaku]  ")
            .AppendLine("			 ,SUM(TJK.[3gatu_jisseki_arari] ) AS [3gatu_jisseki_arari]  ")
            .AppendLine("		FROM ")
            .AppendLine("		  t_jisseki_kanri AS TJK WITH(READCOMMITTED) ")
            .AppendLine("		INNER JOIN m_keikaku_kameiten AS MKK ")
            .AppendLine("			ON TJK.kameiten_cd = MKK.kameiten_cd ")
            .AppendLine("			AND TJK.keikaku_nendo = MKK.keikaku_nendo ")
            .AppendLine("			AND MKK.eigyou_kbn IN ").Append(strEigyouKbnFc)
            .AppendLine("		WHERE ")
            .AppendLine("			TJK.keikaku_nendo = @strNendo ")
            If (Not strSitenCd.Equals(String.Empty)) AndAlso (Not strSitenCd.Equals("ALL")) Then
                .AppendLine("		 AND MKK.busyo_cd = @strSitenCd ")
            End If
            .AppendLine("		GROUP BY ")
            .AppendLine("			 MKK.busyo_cd")
            .AppendLine("			 ,TJK.keikaku_kanri_syouhin_cd ")
            .AppendLine("			 ,TJK.keikaku_nendo ) AS SUB_T3 ")
            .AppendLine("	ON SUB_T.busyo_cd = SUB_T3.busyo_cd ")
            .AppendLine("	AND SUB_T.keikaku_nendo = SUB_T3.keikaku_nendo ")
            .AppendLine("	AND SUB_T.keikaku_kanri_syouhin_cd = SUB_T3.keikaku_kanri_syouhin_cd ")

            .AppendLine("	LEFT JOIN ")
            .AppendLine("		(SELECT  ")
            .AppendLine("			 TFKK.busyo_cd ")
            .AppendLine("			 ,TFKK.keikaku_nendo ")
            .AppendLine("			 ,TFKK.bunbetu_cd ")
            .AppendLine("			 ,TFKK.keikaku_kanri_syouhin_cd ")
            .AppendLine("			 ,SUM(TFKK.[4gatu_keikaku_kensuu] ) AS [4gatu_keikaku_kensuu]  ")
            .AppendLine("			 ,SUM(TFKK.[4gatu_keikaku_kingaku] ) AS [4gatu_keikaku_kingaku]  ")
            .AppendLine("			 ,SUM(TFKK.[4gatu_keikaku_arari] ) AS [4gatu_keikaku_arari]  ")
            .AppendLine("			 ,SUM(TFKK.[5gatu_keikaku_kensuu] ) AS [5gatu_keikaku_kensuu]  ")
            .AppendLine("			 ,SUM(TFKK.[5gatu_keikaku_kingaku] ) AS [5gatu_keikaku_kingaku]  ")
            .AppendLine("			 ,SUM(TFKK.[5gatu_keikaku_arari] ) AS [5gatu_keikaku_arari]  ")
            .AppendLine("			 ,SUM(TFKK.[6gatu_keikaku_kensuu] ) AS [6gatu_keikaku_kensuu]  ")
            .AppendLine("			 ,SUM(TFKK.[6gatu_keikaku_kingaku] ) AS [6gatu_keikaku_kingaku]  ")
            .AppendLine("			 ,SUM(TFKK.[6gatu_keikaku_arari] ) AS [6gatu_keikaku_arari]  ")
            .AppendLine("			 ,SUM(TFKK.[7gatu_keikaku_kensuu] ) AS [7gatu_keikaku_kensuu]  ")
            .AppendLine("			 ,SUM(TFKK.[7gatu_keikaku_kingaku] ) AS [7gatu_keikaku_kingaku]  ")
            .AppendLine("			 ,SUM(TFKK.[7gatu_keikaku_arari] ) AS [7gatu_keikaku_arari]  ")
            .AppendLine("			 ,SUM(TFKK.[8gatu_keikaku_kensuu] ) AS [8gatu_keikaku_kensuu]  ")
            .AppendLine("			 ,SUM(TFKK.[8gatu_keikaku_kingaku] ) AS [8gatu_keikaku_kingaku]  ")
            .AppendLine("			 ,SUM(TFKK.[8gatu_keikaku_arari] ) AS [8gatu_keikaku_arari]  ")
            .AppendLine("			 ,SUM(TFKK.[9gatu_keikaku_kensuu] ) AS [9gatu_keikaku_kensuu]  ")
            .AppendLine("			 ,SUM(TFKK.[9gatu_keikaku_kingaku] ) AS [9gatu_keikaku_kingaku]  ")
            .AppendLine("			 ,SUM(TFKK.[9gatu_keikaku_arari] ) AS [9gatu_keikaku_arari]  ")
            .AppendLine("			 ,SUM(TFKK.[10gatu_keikaku_kensuu] ) AS [10gatu_keikaku_kensuu]  ")
            .AppendLine("			 ,SUM(TFKK.[10gatu_keikaku_kingaku] ) AS [10gatu_keikaku_kingaku]  ")
            .AppendLine("			 ,SUM(TFKK.[10gatu_keikaku_arari] ) AS [10gatu_keikaku_arari]  ")
            .AppendLine("			 ,SUM(TFKK.[11gatu_keikaku_kensuu] ) AS [11gatu_keikaku_kensuu]  ")
            .AppendLine("			 ,SUM(TFKK.[11gatu_keikaku_kingaku] ) AS [11gatu_keikaku_kingaku]  ")
            .AppendLine("			 ,SUM(TFKK.[11gatu_keikaku_arari] ) AS [11gatu_keikaku_arari]  ")
            .AppendLine("			 ,SUM(TFKK.[12gatu_keikaku_kensuu] ) AS [12gatu_keikaku_kensuu]  ")
            .AppendLine("			 ,SUM(TFKK.[12gatu_keikaku_kingaku] ) AS [12gatu_keikaku_kingaku]  ")
            .AppendLine("			 ,SUM(TFKK.[12gatu_keikaku_arari] ) AS [12gatu_keikaku_arari]  ")
            .AppendLine("			 ,SUM(TFKK.[1gatu_keikaku_kensuu] ) AS [1gatu_keikaku_kensuu]  ")
            .AppendLine("			 ,SUM(TFKK.[1gatu_keikaku_kingaku] ) AS [1gatu_keikaku_kingaku]  ")
            .AppendLine("			 ,SUM(TFKK.[1gatu_keikaku_arari] ) AS [1gatu_keikaku_arari]  ")
            .AppendLine("			 ,SUM(TFKK.[2gatu_keikaku_kensuu] ) AS [2gatu_keikaku_kensuu]  ")
            .AppendLine("			 ,SUM(TFKK.[2gatu_keikaku_kingaku] ) AS [2gatu_keikaku_kingaku]  ")
            .AppendLine("			 ,SUM(TFKK.[2gatu_keikaku_arari] ) AS [2gatu_keikaku_arari]  ")
            .AppendLine("			 ,SUM(TFKK.[3gatu_keikaku_kensuu] ) AS [3gatu_keikaku_kensuu]  ")
            .AppendLine("			 ,SUM(TFKK.[3gatu_keikaku_kingaku] ) AS [3gatu_keikaku_kingaku]  ")
            .AppendLine("			 ,SUM(TFKK.[3gatu_keikaku_arari] ) AS [3gatu_keikaku_arari]  ")
            .AppendLine("		 FROM t_fc_keikaku_kanri AS TFKK WITH(READCOMMITTED) ")
            .AppendLine("		 INNER JOIN( ")
            .AppendLine("			SELECT busyo_cd ")
            .AppendLine("				   ,keikaku_nendo ")
            .AppendLine("                  ,keikaku_kanri_syouhin_cd")
            .AppendLine("				   ,MAX(CASE WHEN ISNULL(CONVERT(VARCHAR,keikaku_kakutei_flg),'0') <> '1' THEN '0' ELSE '1' END ")
            .AppendLine("				   + CONVERT(VARCHAR,add_datetime,121)) AS add_datetime ")
            .AppendLine("			FROM t_fc_keikaku_kanri WITH(READCOMMITTED) ")
            .AppendLine("			WHERE keikaku_nendo = @strNendo ")
            .AppendLine("			GROUP BY busyo_cd ")
            .AppendLine("					,keikaku_nendo ")
            .AppendLine("                   ,keikaku_kanri_syouhin_cd)AS TFKK_KEY")
            .AppendLine("		  ON TFKK.busyo_cd = TFKK_KEY.busyo_cd ")
            .AppendLine("		  AND TFKK_KEY.keikaku_kanri_syouhin_cd = TFKK.keikaku_kanri_syouhin_cd  ")
            .AppendLine("		  AND TFKK_KEY.keikaku_nendo = TFKK.keikaku_nendo  ")
            .AppendLine("		  AND TFKK_KEY.add_datetime = CASE WHEN ISNULL(CONVERT(VARCHAR,TFKK.keikaku_kakutei_flg),'0') <> '1' THEN '0' ELSE '1' END ")
            .AppendLine("									 + CONVERT(VARCHAR,TFKK.add_datetime,121)  ")
            .AppendLine("		GROUP BY  ")
            .AppendLine("			TFKK.busyo_cd ")
            .AppendLine("		   ,TFKK.keikaku_nendo ")
            .AppendLine("		   ,TFKK.bunbetu_cd ")
            .AppendLine("		   ,TFKK.keikaku_kanri_syouhin_cd) AS SUB_T4 ")
            .AppendLine("	 ON SUB_T.busyo_cd = SUB_T4.busyo_cd ")
            .AppendLine("	 AND SUB_T.keikaku_nendo = SUB_T4.keikaku_nendo ")
            .AppendLine("	 AND SUB_T.keikaku_kanri_syouhin_cd = SUB_T4.keikaku_kanri_syouhin_cd ")
            .AppendLine("	INNER JOIN ")
            .AppendLine("		m_keikaku_kanri_syouhin AS MKKS WITH(READCOMMITTED)")
            .AppendLine("		ON SUB_T.keikaku_nendo = MKKS.keikaku_nendo ")
            .AppendLine("		AND SUB_T.keikaku_kanri_syouhin_cd = MKKS.keikaku_kanri_syouhin_cd ")
            .AppendLine("	INNER JOIN  ")
            .AppendLine("		m_keikakuyou_meisyou AS MKM WITH(READCOMMITTED) ")
            .AppendLine("		ON MKKS.bunbetu_cd = CONVERT(VARCHAR(10),MKM.code) ")
            .AppendLine("		AND MKM.meisyou_syubetu='15' ")
            .AppendLine("	INNER JOIN ")
            .AppendLine("		m_busyo_kanri AS MBK WITH(READCOMMITTED)")
            .AppendLine("		ON SUB_T.busyo_cd = MBK.busyo_cd ")
            .AppendLine("		AND MBK.sosiki_level = '4' ")
            .AppendLine("   GROUP BY")
            .AppendLine("       MBK.busyo_cd")
            .AppendLine("	   ,MBK.busyo_mei")
            .AppendLine("      ,MKKS.bunbetu_cd")
            .AppendLine("	   ,MKM.meisyou")
            .AppendLine("      ,MKM.hyouji_jyun")
            .AppendLine("      ,SUB_T.keikaku_kanri_syouhin_cd")
            .AppendLine("	   ,MKKS.keikaku_kanri_syouhin_mei")
            .AppendLine("      ,SUB_T3.zennenHeikentanka")
            .AppendLine(") AS SUB_DATA")

            .AppendLine(" LEFT JOIN ")
            .AppendLine("( ")
            .AppendLine("	SELECT ")
            .AppendLine("	    MKK.busyo_cd ")
            .AppendLine("	   ,CONVERT(VARCHAR(20),CAST(AVG(ISNULL(TNHK.koj_hantei_ritu,0)) AS NUMERIC(38,1))) + '%' AS koujiHanteiritu ")
            .AppendLine("	   ,CONVERT(VARCHAR(20),CAST(AVG(ISNULL(TNHK.koj_jyuchuu_ritu,0)) AS NUMERIC(38,1))) + '%' AS koujiJyutyuuritu ")
            .AppendLine("	   ,CONVERT(VARCHAR(20),CAST(AVG(ISNULL(TNHK.tyoku_koj_ritu,0)) AS NUMERIC(38,1))) + '%' AS tyokuKoujiritu ")
            .AppendLine("	FROM ")
            .AppendLine("	   t_nendo_hiritu_kanri AS TNHK WITH(READCOMMITTED) ")
            .AppendLine("	    INNER JOIN ")
            .AppendLine("	    m_keikaku_kameiten AS MKK WITH(READCOMMITTED) ")
            .AppendLine("	    ON  ")
            .AppendLine("	    MKK.kameiten_cd = TNHK.kameiten_cd ")
            .AppendLine("	    AND  ")
            .AppendLine("	    MKK.keikaku_nendo = TNHK.keikaku_nendo ")
            .AppendLine("	WHERE ")
            .AppendLine("	  TNHK.keikaku_nendo = @strNendo")
            If (Not strSitenCd.Equals(String.Empty)) AndAlso (Not strSitenCd.Equals("ALL")) Then
                .AppendLine("	  AND MKK.busyo_cd = @strSitenCd ")
            End If
            .AppendLine("         AND MKK.eigyou_kbn IN ").Append(strEigyouKbnFc)
            .AppendLine("	GROUP BY  ")
            .AppendLine("	   MKK.busyo_cd ")
            .AppendLine(") AS SUB_HIRITU ")
            .AppendLine("ON ")
            .AppendLine("SUB_DATA.busyo_cd = SUB_HIRITU.busyo_cd")
            .AppendLine("ORDER BY")
            .AppendLine("	 SUB_DATA.busyo_cd")
            .AppendLine("   ,SUB_DATA.hyouji_jyun")
        End With

        '�p�����[�^�̐ݒ�
        paramList.Add(MakeParam("@strSitenCd", SqlDbType.VarChar, 4, strSitenCd))                       '�x�X�R�[�h
        paramList.Add(MakeParam("@strNendo", SqlDbType.Char, 4, strNendo))                              '�v��_�N�x

        FillDataset(ManagerDA.Connection, CommandType.Text, commandTextSb.ToString, dsReturn, "dsTukinamiListSubeteData", paramList.ToArray())

        Return dsReturn.Tables("dsTukinamiListSubeteData")

    End Function
End Class

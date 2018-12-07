Imports System.Data.SqlClient
Imports System.Text
Imports Itis.ApplicationBlocks.Data.SQLHelper

Public Class SeikyusyoFcwOutputDataAccess

    Private connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>
    ''' ���������[�o�̓f�[�^��������
    ''' </summary>
    ''' <param name="strSeikyusyo_no">������R�[�h</param>
    ''' <returns>���������[�o�̓f�[�^</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 2010/07/19�@������(��A)�@�V�K�쐬
    ''' </history>
    Public Function SelSeikyusyoFcwOutputData(ByVal strSeikyusyo_no As String) As Data.DataTable

        ' DataSet�C���X�^���X�̐���()
        Dim dsRetrun As New Data.DataSet
        dsRetrun.Tables.Add()

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT    tk.seikyuu_saki_cd + '-' + tk.seikyuu_saki_brc + ")
            '2011/02/14 �u�����溰�ށv�́y   T�z�󎚂̔���ύX �t���ǉ� ��
            '.AppendLine("       CASE WHEN LEFT(km.hannyou_cd,1) = '1' THEN '   T' ELSE '' END AS seikyuu --������R�[�h-������}��")
            .AppendLine("       CASE WHEN LEFT(tk.kaisyuu_seikyuusyo_yousi_hannyou_cd,1) = '1' THEN '   T' ELSE '' END AS seikyuu --������R�[�h-������}��")
            '2011/02/14 �u�����溰�ށv�́y   T�z�󎚂̔���ύX �t���ǉ� ��
            .AppendLine("             ,tk.seikyuusyo_hak_date                        --���������s��")
            .AppendLine("             ,tk.yuubin_no                                  --�X�֔ԍ�")
            .AppendLine("             ,tk.jyuusyo1                                   --�Z��1")
            .AppendLine("             ,tk.jyuusyo2                                   --�Z��2")
            .AppendLine("             ,tk.seikyuu_saki_mei                           --�����於")
            .AppendLine("             ,tk.seikyuu_saki_mei2                          --�����於2")
            .AppendLine("             ,isnull(tk.tantousya_mei,'��S����') ")
            .AppendLine("                                         AS tantousya_mei   --�S���Җ�")
            '===============2011/05/31 405693_EARTH�o���Q���v�]�Ή� �ԗ� �C�� �J�n��===========================
            ''2010/10/22 �ėp����e�[�u���ΏۂƂ��� �t���ǉ� ��
            ''.AppendLine("             ,ISNULL(tu.kbn,'') + ISNULL(tu.bangou,'') as bukken_no --�����ԍ�")
            '.AppendLine("             ,CASE ")
            '.AppendLine("	            WHEN TU.himoduke_table_type =  '1' ") '--����f�[�^�e�[�u��.�R�t���e�[�u���^�C�v=1�̎�")
            '.AppendLine("	            THEN ISNULL(TU.kbn,'') +ISNULL(TU.bangou,'') ") '--����f�[�^�e�[�u��.�敪�{�ԍ�")
            '.AppendLine("	            WHEN TU.himoduke_table_type =  '9' ") '--����f�[�^�e�[�u��.�R�t���e�[�u���^�C�v=9�̎�")
            '.AppendLine("	            THEN ISNULL(THU.kbn,'') +ISNULL(THU.bangou,'') ") '--�ėp����e�[�u��.�敪�{�ԍ�")
            '.AppendLine("	            ELSE '' ")
            '.AppendLine("	            END AS bukken_no ") '--�����ԍ�")
            ''2010/10/22 �ėp����e�[�u���ΏۂƂ��� �t���ǉ� ��
            ''2010/10/22 �ėp����e�[�u���ΏۂƂ��� �t���ǉ� ��
            ''.AppendLine("             ,CASE WHEN tk.seikyuusyo_inji_bukken_mei_flg = '0' THEN")
            ''.AppendLine("                      tj.sesyu_mei                          --�{�喼")
            ''.AppendLine("                     WHEN tk.seikyuusyo_inji_bukken_mei_flg = '1' THEN")
            ''.AppendLine("                      tj.jutyuu_bukken_mei                  --�󒍕�����")
            ''.AppendLine("              END AS bukken_mei")
            '.AppendLine("              ,CASE ")
            '.AppendLine("	            WHEN TU.himoduke_table_type =  '1' ") '--����f�[�^�e�[�u��.�R�t���e�[�u���^�C�v=1�̎�")
            '.AppendLine("	            THEN ")
            '.AppendLine("                   CASE WHEN tk.seikyuusyo_inji_bukken_mei_flg = '0' THEN")
            '.AppendLine("                            tj.sesyu_mei                          --�{�喼")
            '.AppendLine("                         WHEN tk.seikyuusyo_inji_bukken_mei_flg = '1' THEN")
            '.AppendLine("                            tj.jutyuu_bukken_mei                  --�󒍕�����")
            '.AppendLine("                    END ")
            '.AppendLine("	            WHEN TU.himoduke_table_type =  '9' ") '--����f�[�^�e�[�u��.�R�t���e�[�u���^�C�v=9�̎�")
            '.AppendLine("	            THEN THU.sesyu_mei ") '--�ėp����e�[�u��.�{�喼")
            '.AppendLine("	            ELSE '' ")
            '.AppendLine("	            END AS bukken_mei ") '--�E�v��(�{�喼)")
            ''2010/10/22 �ėp����e�[�u���ΏۂƂ��� �t���ǉ� ��
            .AppendLine("               ,ISNULL(tu.kbn,'') + ISNULL(tu.bangou,'') as bukken_no ") '--�����ԍ�(����f�[�^�e�[�u��.�敪�{�ԍ�)
            .AppendLine("               ,CASE ")
            .AppendLine("                   WHEN tj.kbn IS NOT NULL THEN ") '--�n�Ճe�[�u�������݂��鎞
            .AppendLine("                       CASE ")
            .AppendLine("                           WHEN tk.seikyuusyo_inji_bukken_mei_flg = '0' THEN")
            .AppendLine("                               tj.sesyu_mei                          --�{�喼")
            .AppendLine("                           WHEN tk.seikyuusyo_inji_bukken_mei_flg = '1' THEN")
            .AppendLine("                               tj.jutyuu_bukken_mei                  --�󒍕�����")
            .AppendLine("                           END ")
            .AppendLine("                   WHEN TU.himoduke_table_type =  '9' ") '--����f�[�^�e�[�u��.�R�t���e�[�u���^�C�v=9�̎�")
            .AppendLine("                       AND tj.kbn IS NULL ")   '--���� �n�Ճe�[�u�������݂��Ȃ���
            .AppendLine("                   THEN THU.sesyu_mei ") '--�ėp����e�[�u��.�{�喼")
            .AppendLine("                   ELSE '' ")
            .AppendLine("                   END AS bukken_mei ") '--�E�v��(�{�喼)")
            '===============2011/05/31 405693_EARTH�o���Q���v�]�Ή� �ԗ� �C�� �I����===========================
            .AppendLine("             ,tk.konkai_kaisyuu_yotei_date                  --�����\���")
            .AppendLine("             ,LEFT(ISNULL(tk.kaisyuu_seikyuusyo_yousi,''),1) AS furikomi_flg --�U����\�����e�t���O")
            .AppendLine("             ,SUBSTRING(ISNULL(tk.kaisyuu_seikyuusyo_yousi,''),2,1) AS yousi_flg --�p���t���O")
            .AppendLine("             ,CASE WHEN LEFT(tk.kaisyuu_seikyuusyo_yousi,1) = '1' THEN")
            .AppendLine("                     nyuukin_kouza_no                       --���������ԍ�")
            .AppendLine("              END nyuukin_kouza_no")
            .AppendLine("             ,tk.kurikosi_gaku                              --�O��J�z�c��")
            .AppendLine("             ,tk.gonyuukin_gaku                             --������z")
            .AppendLine("             ,tk.sousai_gaku                                --���E")
            .AppendLine("             ,tk.tyousei_gaku                               --�����z")
            .AppendLine("             ,tk.konkai_goseikyuu_gaku                      --����䐿���z")
            .AppendLine("             ,tk.konkai_kurikosi_gaku                       --�J�z�c��")
            .AppendLine("             ,tu.himoduke_cd                                --�R�t���R�[�h")
            .AppendLine("             ,tu.himoduke_table_type                        --�R�t�����e�[�u�����")
            .AppendLine("             ,tu.kbn  AS uri_kbn                            --(����)�敪")
            .AppendLine("             ,tu.bangou  AS uri_bangou                      --(����)�ԍ�")
            .AppendLine("             ,tu.seikyuu_saki_cd  AS ten_cd                 --(������)�X�R�[�h")
            '.AppendLine("             ,tu.seikyuu_saki_mei  AS ten_mei               --(������)�X��")
            .AppendLine("             ,ISNULL(tu.seikyuu_saki_mei,'')  AS ten_mei    --(������)�X��")
            .AppendLine("             ,tu.denpyou_uri_date                           --����N����")
            .AppendLine("             ,tu.syouhin_cd                                 --���icd")
       
            .AppendLine("             ,tu.hinmei                                     --���i��")
            .AppendLine(" ,CASE ")
            .AppendLine(" WHEN km1.code+km2.code IS NULL THEN ")
            .AppendLine("            tu.hinmei ")
            .AppendLine(" ELSE ")
            .AppendLine(" RTRIM(tu.hinmei)  +'['+m_tyousahouhou.tys_houhou_mei_ryaku+']' ")
            .AppendLine(" END AS hinmei--���i�� ")

            ''2018/12/05 ������ JHS0003_Earth���������[�̍��� �ǉ� �˗��S���Җ���
            '.AppendLine(" ,CASE ")
            '.AppendLine("       WHEN km1.code+km2.code IS NULL THEN ")
            '.AppendLine("            tu.hinmei ")
            '.AppendLine("  ELSE ")
            '.AppendLine("       RTRIM(tu.hinmei)  +'['+m_tyousahouhou.tys_houhou_mei_ryaku+']' ")
            '.AppendLine("  END + ' ' + tj.irai_tantousya_mei + ' �l' AS hinmei --���i�� ")
            ''2018/12/05 ������ JHS0003_Earth���������[�̍��ڒǉ� �˗��S���Җ���


            .AppendLine("             ,tu.suu                                        --����")
            .AppendLine("             ,tu.tanka                                      --�P��")
            .AppendLine("             ,tu.uri_gaku                                   --�ŕʋ��z")
            .AppendLine("             ,tu.sotozei_gaku                               --�O�Ŋz")
            .AppendLine("             ,ISNULL(tu.uri_gaku,0) + ISNULL(tu.sotozei_gaku,0) as zeikomi_gaku --�ō����z")
            '2013/5/29 �������̋�s�x�X�R�[�h�����ɔ���Earth���C -----------------------��
            .AppendLine("             ,ISNULL(tk.ginkou_siten_mei,'') + '(' + ISNULL(tk.ginkou_siten_cd,'') + ')' as ginkou_siten_cd  --��s�x�X�R�[�h")
            '2013/5/29 �������̋�s�x�X�R�[�h�����ɔ���Earth���C -----------------------��
            '===============��2014/02/06 407662_����ő��őΉ�_Earth �ԗ� �ǉ� �J�n��===========================
            .AppendLine(",MSZ.zeiritu ") '--�ŗ�
            '===============��2014/02/06 407662_����ő��őΉ�_Earth �ԗ� �ǉ� �I����===========================

            '2018/12/05 ������ JHS0003_Earth���������[�̍��ڒǉ� ��
            '�˗��S���Җ�
            .AppendLine(",ISNULL(tj.irai_tantousya_mei,'') as irai_tantousya_mei  --�˗��S���Җ�")
            '�_��No
            .AppendLine(",ISNULL(tj.keiyaku_no,'') as keiyaku_no  --�_��No")

            .AppendLine(",SUBSTRING(ISNULL(tk.kaisyuu_seikyuusyo_yousi,''),3,1) AS koumoku_hyouji_flg --���ڕ\���t���O")
            '2018/12/05 ������ JHS0003_Earth���������[�̍��ڒǉ� ��

            .AppendLine("")
            .AppendLine("FROM      t_seikyuu_kagami AS tk                            --�����Ӄe�[�u��")
            .AppendLine("LEFT JOIN  t_seikyuu_meisai AS tm                           --�������׃e�[�u��")
            .AppendLine("ON         tk.seikyuusyo_no = tm.seikyuusyo_no              --������NO")
            .AppendLine("AND        tm.inji_taisyo_flg = '1'")
            .AppendLine("LEFT JOIN  t_uriage_data      AS tu                         --����f�[�^�e�[�u��")
            .AppendLine("ON         tm.denpyou_unique_no = tu.denpyou_unique_no      --�`�[���j�[�NNO")
            .AppendLine("LEFT JOIN  t_jiban               AS tj                      --�n�Ճe�[�u��")
            .AppendLine("ON         tj.kbn = tu.kbn                                  --�敪")
            .AppendLine("AND        tj.hosyousyo_no = tu.bangou                      --�n�Ճe�[�u��.�ۏ؏�NO=����f�[�^�e�[�u��.�ԍ�")
            '2010/10/22 �ėp����e�[�u���ΏۂƂ��� �t���ǉ� ��
            .AppendLine("LEFT JOIN  t_hannyou_uriage      AS THU                     --�ėp����e�[�u��")
            .AppendLine("ON         TU.himoduke_cd = CONVERT(VARCHAR,THU.han_uri_unique_no) --�ėp���テ�j�[�NNO")
            '2010/10/22 �ėp����e�[�u���ΏۂƂ��� �t���ǉ� ��
            .AppendLine("LEFT JOIN  m_kakutyou_meisyou    AS km                      --�g�����̃}�X�^")
            .AppendLine("ON         km.meisyou_syubetu = '3'                         --�敪")
            .AppendLine("AND        km.code = tk.kaisyuu_seikyuusyo_yousi            --�R�[�h")
            .AppendLine(" LEFT JOIN m_tyousahouhou ")
            .AppendLine(" ON tj.tys_houhou = m_tyousahouhou.tys_houhou_no ")
            .AppendLine(" LEFT JOIN m_kakutyou_meisyou AS km1 ")
            .AppendLine(" ON tu.syouhin_cd = km1.code ")
            .AppendLine(" AND km1.meisyou_syubetu = 26 ")
            .AppendLine(" LEFT JOIN m_kakutyou_meisyou AS km2 ")
            .AppendLine(" ON tj.tys_houhou = km2.code ")
            .AppendLine(" AND km2.meisyou_syubetu = 27 ")
            '===============��2014/02/06 407662_����ő��őΉ�_Earth �ԗ� �ǉ� �J�n��===========================
            .AppendLine("LEFT JOIN  ")
            .AppendLine("m_syouhizei AS MSZ ") '--����Ń}�X�^
            .AppendLine("   ON ") '--�ŋ敪
            .AppendLine("       MSZ.zei_kbn = tu.zei_kbn ") '--�ŋ敪
            '===============��2014/02/06 407662_����ő��őΉ�_Earth �ԗ� �ǉ� �I����===========================
            .AppendLine("WHERE    tk.seikyuusyo_no =  '" & strSeikyusyo_no & "'")
            '===============��2014/02/06 407662_����ő��őΉ�_Earth �ԗ� �C�� �J�n��===========================
            '.AppendLine("ORDER BY tk.seikyuusyo_no,tu.kbn + tu.bangou,tu.syouhin_cd,tu.denpyou_uri_date")
            .AppendLine("ORDER BY tk.seikyuusyo_no,tu.kbn + tu.bangou,tu.syouhin_cd,tu.denpyou_uri_date,MSZ.zeiritu")
            '===============��2014/02/06 407662_����ő��őΉ�_Earth �ԗ� �C�� �I����===========================

        End With

        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsRetrun, _
                    dsRetrun.Tables(0).TableName, paramList.ToArray)

        Return dsRetrun.Tables(0)

    End Function

    ''' <summary>
    ''' �����揑No�f�[�^���擾����
    ''' </summary>
    ''' <param name="strSeikyusyo_no">�����揑No</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 2010/07/19�@������(��A)�@�V�K�쐬
    ''' </history>
    Public Function SelSeikyusyoNoData(ByVal strSeikyusyo_no As String) As Data.DataTable

        'DataSet�C���X�^���X�̐���()
        Dim dsRetrun As New Data.DataSet
        dsRetrun.Tables.Add()

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT  DISTINCT  tk.seikyuusyo_no    --�����揑No")
            .AppendLine("FROM      t_seikyuu_kagami AS tk      --�����Ӄe�[�u��")
            .AppendLine("WHERE    tk.seikyuusyo_no IN (" & strSeikyusyo_no & ")")
        End With

        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsRetrun, _
                    dsRetrun.Tables(0).TableName, paramList.ToArray)

        Return dsRetrun.Tables(0)

    End Function

    ''' <summary>
    ''' ���O�C�����[�U�[�����擾����B
    ''' </summary>
    ''' <param name="loginID">���O�C�����[�U�[�R�[�h</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 2010/07/19�@�^����(��A)�@�V�K�쐬
    ''' </history>
    Public Function SelLoginUserName(ByVal loginID As String) As String

        'DataSet�C���X�^���X�̐���()
        Dim dsRetrun As New Data.DataSet
        dsRetrun.Tables.Add()

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT DisplayName ")
            .AppendLine("FROM m_jhs_mailbox ")
            .AppendLine("WHERE DirectoryName = '" & loginID & "'")
        End With

        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsRetrun, _
                    dsRetrun.Tables(0).TableName, paramList.ToArray)

        If dsRetrun.Tables(0).Rows.Count = 0 Then
            Return "[NO DATA FOUND]"
        Else
            Return dsRetrun.Tables(0).Rows(0).Item("DisplayName").ToString
        End If

    End Function

    ''' <summary>
    ''' ��ވ�̓X�����擾����
    ''' </summary>
    ''' <param name="UA_kbn">�敪</param>
    ''' <param name="UA_bangou">�ԍ�</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 2010/07/19�@�^����(��A)�@�V�K�쐬
    ''' </history>
    Public Function SelHimodekeSyurui_1_tenmei(ByVal UA_kbn As String, ByVal UA_bangou As String) _
    As Data.DataTable

        'DataSet�C���X�^���X�̐���()
        Dim dsRetrun As New Data.DataSet
        dsRetrun.Tables.Add()

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT KM.kameiten_cd AS ten_cd,KM.kameiten_seisiki_mei AS ten_mei ")
            .AppendLine("FROM t_jiban AS JB ")
            .AppendLine("LEFT JOIN m_kameiten AS KM ")
            .AppendLine("ON JB.kameiten_cd = KM.kameiten_cd")
            .AppendLine("WHERE JB.kbn = '" & UA_kbn & "'")
            .AppendLine("AND JB.hosyousyo_no = '" & UA_bangou & "'")
        End With

        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsRetrun, _
                    dsRetrun.Tables(0).TableName, paramList.ToArray)

        Return dsRetrun.Tables(0)

    End Function

    ''' <summary>
    ''' ��ޓ�̓X�����擾����
    ''' </summary>
    ''' <param name="UA_himodukecd_ten_cd">�X�R�[�h</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 2010/07/19�@�^����(��A)�@�V�K�쐬
    ''' </history>
    Public Function SelHimodekeSyurui_2_tenmei(ByVal UA_himodukecd_ten_cd As String) _
    As Data.DataTable

        'DataSet�C���X�^���X�̐���()
        Dim dsRetrun As New Data.DataSet
        dsRetrun.Tables.Add()

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT KM.kameiten_cd AS ten_cd,KM.kameiten_seisiki_mei AS ten_mei ")
            .AppendLine("FROM t_tenbetu_seikyuu AS TBSK ")
            .AppendLine("LEFT JOIN m_kameiten AS KM ")
            .AppendLine("ON TBSK.mise_cd = KM.kameiten_cd")
            .AppendLine("WHERE TBSK.mise_cd = '" & UA_himodukecd_ten_cd & "'")
        End With

        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsRetrun, _
                    dsRetrun.Tables(0).TableName, paramList.ToArray)


        If dsRetrun.Tables(0).Rows.Count > 0 Then
            Return dsRetrun.Tables(0)
        End If

        'SQL��
        commandTextSb = New StringBuilder

        With commandTextSb
            .AppendLine("SELECT ME.eigyousyo_cd AS ten_cd,ME.eigyousyo_mei AS ten_mei ")
            .AppendLine("FROM t_tenbetu_seikyuu AS TBSK ")
            .AppendLine("LEFT JOIN m_eigyousyo AS ME ")
            .AppendLine("ON TBSK.mise_cd = ME.eigyousyo_cd")
            .AppendLine("WHERE TBSK.mise_cd = '" & UA_himodukecd_ten_cd & "'")
        End With

        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsRetrun, _
                    dsRetrun.Tables(0).TableName, paramList.ToArray)

        Return dsRetrun.Tables(0)

    End Function

    ''' <summary>
    ''' ��ގO�̓X�����擾����
    ''' </summary>
    ''' <param name="UA_himodukecd_ten_cd">�X�R�[�h</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 2010/07/19�@�^����(��A)�@�V�K�쐬
    ''' </history>
    Public Function SelHimodekeSyurui_3_tenmei(ByVal UA_himodukecd_ten_cd As String) _
   As Data.DataTable

        'DataSet�C���X�^���X�̐���()
        Dim dsRetrun As New Data.DataSet
        dsRetrun.Tables.Add()

        'SQL���̐���
        Dim commandTextSb As New StringBuilder

        '�p�����[�^�i�[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL��
        With commandTextSb
            .AppendLine("SELECT KM.kameiten_cd AS ten_cd,KM.kameiten_seisiki_mei AS ten_mei ")
            .AppendLine("FROM t_tenbetu_syoki_seikyuu AS TBSK ")
            .AppendLine("LEFT JOIN m_kameiten AS KM ")
            .AppendLine("ON TBSK.mise_cd = KM.kameiten_cd")
            .AppendLine("WHERE TBSK.mise_cd = '" & UA_himodukecd_ten_cd & "'")
        End With

        '�������s
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsRetrun, _
                    dsRetrun.Tables(0).TableName, paramList.ToArray)

        Return dsRetrun.Tables(0)

    End Function

End Class

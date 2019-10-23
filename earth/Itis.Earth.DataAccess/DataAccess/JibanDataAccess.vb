Imports System
Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' �n�Ճf�[�^�̎擾�N���X�ł�
''' </summary>
''' <remarks></remarks>
Public Class JibanDataAccess

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

#Region "�����o�ϐ�"
    Dim connStr As String = ConnectionManager.Instance.ConnectionString
    Dim strLogic As New StringLogic
    Dim cmnDtAcc As New CmnDataAccess
#End Region

#Region "�񋓌^"
    ''' <summary>
    ''' �X�V�ΏۃA�C�e����
    ''' </summary>
    ''' <remarks></remarks>
    Enum enumItemName
        ''' <summary>
        ''' �{�喼
        ''' </summary>
        ''' <remarks></remarks>
        SesyuMei = 1
        ''' <summary>
        ''' �Z��
        ''' </summary>
        ''' <remarks></remarks>
        Juusyo = 2
        ''' <summary>
        ''' �����X�R�[�h
        ''' </summary>
        ''' <remarks></remarks>
        KameitenCd = 3
        ''' <summary>
        ''' �������
        ''' </summary>
        ''' <remarks></remarks>
        TyousaKaisya = 4
        ''' <summary>
        ''' ���l
        ''' </summary>
        ''' <remarks></remarks>
        Bikou = 5
        ''' <summary>
        ''' ���l2
        ''' </summary>
        ''' <remarks></remarks>
        Bikou2 = 6
    End Enum

#End Region

#Region "�n�Ճf�[�^�̎擾"
    ''' <summary>
    ''' �n�Ճ��R�[�h���擾���܂�
    ''' </summary>
    ''' <param name="kbn">�敪</param>
    ''' <param name="hosyousyoNo">�ۏ؏�NO</param>   
    ''' <param name="isNotDataHaki">�f�[�^�j����ʔ��f�t���O</param>
    ''' <returns>�n�Ճf�[�^�e�[�u��</returns>
    ''' <remarks></remarks>
    Public Function GetJibanData(ByVal kbn As String, _
                                 ByVal hosyousyoNo As String, _
                        Optional ByVal isNotDataHaki As Boolean = False) As JibanDataSet.JibanTableDataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetJibanData", _
                                            kbn, _
                                            hosyousyoNo, _
                                            isNotDataHaki)

        ' �p�����[�^
        Const strParamKbn As String = "@KBN"
        Const strParamHosyousyoNo As String = "@HOSYOUSYONO"

        ' �p�����[�^�֐ݒ�
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamKbn, SqlDbType.Char, 1, kbn), _
             SQLHelper.MakeParam(strParamHosyousyoNo, SqlDbType.VarChar, 10, hosyousyoNo)}

        Dim commandTextSb As New StringBuilder

        commandTextSb.Append(" SELECT ")
        commandTextSb.Append("   j.kbn, ")
        commandTextSb.Append("   j.hosyousyo_no, ")
        commandTextSb.Append("   j.data_haki_syubetu, ")
        commandTextSb.Append("   j.data_haki_date, ")
        commandTextSb.Append("   j.sesyu_mei, ")
        commandTextSb.Append("   j.jutyuu_bukken_mei, ")
        commandTextSb.Append("   j.bukken_jyuusyo1, ")
        commandTextSb.Append("   j.bukken_jyuusyo2, ")
        commandTextSb.Append("   j.bukken_jyuusyo3, ")
        commandTextSb.Append("   j.bunjou_cd, ")
        commandTextSb.Append("   j.bukken_nayose_cd, ")
        commandTextSb.Append("   j.kameiten_cd, ")
        commandTextSb.Append("   j.syouhin_kbn, ")
        commandTextSb.Append("   j.bikou, ")
        commandTextSb.Append("   j.irai_date, ")
        commandTextSb.Append("   j.irai_tantousya_mei, ")
        commandTextSb.Append("   j.keiyaku_no, ")
        commandTextSb.Append("   j.th_kasi_umu, ")
        commandTextSb.Append("   j.kaisou, ")
        commandTextSb.Append("   j.sintiku_tatekae, ")
        commandTextSb.Append("   j.kouzou, ")
        commandTextSb.Append("   j.kouzou_memo, ")
        commandTextSb.Append("   j.syako, ")
        commandTextSb.Append("   j.negiri_hukasa, ")
        commandTextSb.Append("   j.yotei_morituti_atusa, ")
        commandTextSb.Append("   j.yotei_ks, ")
        commandTextSb.Append("   j.yotei_ks_memo, ")
        commandTextSb.Append("   j.tys_kaisya_cd, ")
        commandTextSb.Append("   j.tys_kaisya_jigyousyo_cd, ")
        commandTextSb.Append("   j.tys_houhou, ")
        commandTextSb.Append("   j.tys_gaiyou, ")
        commandTextSb.Append("   j.fc_builder_hanbai_gaku, ")
        commandTextSb.Append("   j.tys_kibou_date, ")
        commandTextSb.Append("   j.tys_kibou_jikan, ")
        commandTextSb.Append("   j.tatiai_umu, ")
        commandTextSb.Append("   j.tatiaisya_cd, ")
        commandTextSb.Append("   j.tenpu_heimenzu, ")
        commandTextSb.Append("   j.tenpu_ritumenzu, ")
        commandTextSb.Append("   j.tenpu_ks_husezu, ")
        commandTextSb.Append("   j.tenpu_danmenzu, ")
        commandTextSb.Append("   j.tenpu_kukeizu, ")
        commandTextSb.Append("   j.syoudakusyo_tys_date, ")
        commandTextSb.Append("   j.tys_jissi_date, ")
        commandTextSb.Append("   j.dositu, ")
        commandTextSb.Append("   j.kyoyou_sijiryoku, ")
        commandTextSb.Append("   j.hantei_cd1, ")
        commandTextSb.Append("   j.hantei_cd2, ")
        commandTextSb.Append("   j.hantei_setuzoku_moji, ")
        commandTextSb.Append("   j.tantousya_cd, ")
        commandTextSb.Append("   t1.tantousya_mei As tantousya_mei, ")
        commandTextSb.Append("   j.syouninsya_cd, ")
        commandTextSb.Append("   t2.tantousya_mei As syouninsya_mei, ")
        commandTextSb.Append("   j.keikakusyo_sakusei_date, ")
        commandTextSb.Append("   j.ks_danmen_cd, ")
        commandTextSb.Append("   j.danmenzu_setumei, ")
        commandTextSb.Append("   j.kousatu, ")
        commandTextSb.Append("   j.ks_hkks_umu, ")
        commandTextSb.Append("   j.ks_koj_kanry_hkks_tyk_date, ")
        commandTextSb.Append("   j.hosyousyo_hak_jyky, ")
        commandTextSb.Append("   j.hosyousyo_hak_jyky_settei_date, ")
        commandTextSb.Append("   j.hosyousyo_hak_date, ")
        commandTextSb.Append("   j.insatu_hosyousyo_hak_date, ")
        commandTextSb.Append("   j.hosyou_umu, ")
        commandTextSb.Append("   j.hosyou_kaisi_date, ")
        commandTextSb.Append("   j.hosyou_kikan, ")
        commandTextSb.Append("   j.hosyou_nasi_riyuu, ")
        commandTextSb.Append("   j.hosyou_syouhin_umu, ")
        commandTextSb.Append("   j.hoken_kaisya, ")
        commandTextSb.Append("   j.hoken_sinsei_tuki, ")
        commandTextSb.Append("   j.hosyousyo_saihak_date, ")
        commandTextSb.Append("   j.tys_hkks_umu, ")
        commandTextSb.Append("   j.tys_hkks_jyuri_syousai, ")
        commandTextSb.Append("   j.tys_hkks_jyuri_date, ")
        commandTextSb.Append("   j.tys_hkks_hak_date, ")
        commandTextSb.Append("   j.tys_hkks_saihak_date, ")
        commandTextSb.Append("   j.koj_hkks_umu, ")
        commandTextSb.Append("   j.koj_hkks_juri_syousai, ")
        commandTextSb.Append("   j.koj_hkks_juri_date, ")
        commandTextSb.Append("   j.koj_hkks_hassou_date, ")
        commandTextSb.Append("   j.koj_hkks_saihak_date, ")
        commandTextSb.Append("   j.koj_gaisya_cd, ")
        commandTextSb.Append("   j.koj_gaisya_jigyousyo_cd, ")
        commandTextSb.Append("   j.kairy_koj_syubetu, ")
        commandTextSb.Append("   j.kairy_koj_kanry_yotei_date, ")
        commandTextSb.Append("   j.kairy_koj_date, ")
        commandTextSb.Append("   j.kairy_koj_sokuhou_tyk_date, ")
        commandTextSb.Append("   j.t_koj_kaisya_cd, ")
        commandTextSb.Append("   j.t_koj_kaisya_jigyousyo_cd, ")
        commandTextSb.Append("   j.t_koj_syubetu, ")
        commandTextSb.Append("   j.t_koj_kanry_yotei_date, ")
        commandTextSb.Append("   j.t_koj_date, ")
        commandTextSb.Append("   j.t_koj_sokuhou_tyk_date, ")
        commandTextSb.Append("   j.tys_kekka_add_datetime, ")
        commandTextSb.Append("   j.tys_kekka_upd_datetime, ")
        commandTextSb.Append("   j.douji_irai_tousuu, ")
        commandTextSb.Append("   j.hoken_sinsei_kbn, ")
        commandTextSb.Append("   j.kasi_umu, ")
        commandTextSb.Append("   j.koj_gaisya_seikyuu_umu, ")
        commandTextSb.Append("   j.henkin_syori_flg, ")
        commandTextSb.Append("   j.henkin_syori_date, ")
        commandTextSb.Append("   j.koj_tantousya_mei, ")
        commandTextSb.Append("   j.keiyu, ")
        commandTextSb.Append("   j.koj_siyou_kakunin, ")
        commandTextSb.Append("   j.koj_siyou_kakunin_date, ")
        commandTextSb.Append("   j.hosyousyo_hak_iraisyo_umu, ")
        commandTextSb.Append("   j.hosyousyo_hak_iraisyo_tyk_date, ")
        commandTextSb.Append("   j.sekkei_kyoyou_sijiryoku, ")
        commandTextSb.Append("   j.irai_yotei_tousuu, ")
        commandTextSb.Append("   j.tatemono_youto_no, ")
        commandTextSb.Append("   j.kosuu, ")
        commandTextSb.Append("   j.kousinsya, ")
        commandTextSb.Append("   j.tys_renrakusaki_atesaki_mei, ")
        commandTextSb.Append("   j.tys_renrakusaki_tel, ")
        commandTextSb.Append("   j.tys_renrakusaki_fax, ")
        commandTextSb.Append("   j.tys_renrakusaki_mail, ")
        commandTextSb.Append("   j.tys_renrakusaki_tantou_mei, ")
        commandTextSb.Append("   j.add_login_user_id, ")
        commandTextSb.Append("   j.add_datetime, ")
        commandTextSb.Append("   ISNULL(j.upd_login_user_id,j.add_login_user_id) AS upd_login_user_id , ")
        commandTextSb.Append("   ISNULL(j.upd_datetime,j.add_datetime) AS upd_datetime , ")
        commandTextSb.Append("   j.t_koj_kaisya_seikyuu_umu, ")
        commandTextSb.Append("   j.bikou2, ")
        commandTextSb.Append("   j.syasin_jyuri, ")
        commandTextSb.Append("   j.syasin_comment, ")
        commandTextSb.Append("   r.t_houkoku_syounin  AS t_houkoku_syounin,")
        commandTextSb.Append("   r.status  AS status,")
        commandTextSb.Append("   hs.hosyousyo_hak_date  AS hs_hosyousyo_hak_date,")
        commandTextSb.Append("   hs.hkks_hassou_date  AS hs_hkks_hassou_date, ")
        commandTextSb.Append("   m.meisyou As nyuukin_kakunin_jyouken_mei, ")
        commandTextSb.Append("   hj.hkks_jyuri_jyky,  ")
        commandTextSb.Append("   ks.kairy_koj_syubetu As kairy_koj_syubetu_mei, ")
        commandTextSb.Append("   k.hansokuhin_seikyuusaki, ")
        commandTextSb.Append("   k.tys_seikyuu_saki, ")
        commandTextSb.Append("   j.fuho_syoumeisyo_flg, ")
        commandTextSb.Append("   j.fuho_syoumeisyo_hassou_date, ")
        commandTextSb.Append("   j.yoyaku_zumi_flg, ")
        commandTextSb.Append("   j.annaizu, ")
        commandTextSb.Append("   j.haitizu, ")
        commandTextSb.Append("   j.kakukai_heimenzu, ")
        commandTextSb.Append("   j.ks_husezu, ")
        commandTextSb.Append("   j.ks_danmenzu, ")
        commandTextSb.Append("   j.zousei_keikakuzu, ")
        commandTextSb.Append("   j.ks_tyakkou_yotei_from_date, ")
        commandTextSb.Append("   j.ks_tyakkou_yotei_to_date, ")
        commandTextSb.Append("   j.sinki_touroku_moto_kbn, ")
        commandTextSb.Append("   j.koj_hantei_kekka_flg, ")
        commandTextSb.Append("   k.keiretu_cd, ")
        commandTextSb.Append("   k.eigyousyo_cd, ")
        commandTextSb.Append("   j.sesyu_mei_umu, ")
        commandTextSb.Append("   j.henkou_yotei_kameiten_cd, ")
        commandTextSb.Append("   j.hosyou_nasi_riyuu_cd, ")
        commandTextSb.Append("   j.hak_irai_uke_datetime, ")
        commandTextSb.Append("   j.hak_irai_can_datetime, ")
        commandTextSb.Append("   j.hosyousyo_hassou_date, ")
        commandTextSb.Append("   j.hosyousyo_hak_houhou ")
        commandTextSb.Append(" FROM ")
        commandTextSb.Append("    t_jiban j  WITH (READCOMMITTED) ")
        commandTextSb.Append(" LEFT OUTER JOIN  ")
        commandTextSb.Append("    ReportIF r  WITH (READCOMMITTED) ")
        commandTextSb.Append(" ON  ")
        commandTextSb.Append("    j.kbn + j.hosyousyo_no = r.kokyaku_no ")
        commandTextSb.Append(" LEFT OUTER JOIN  ")
        commandTextSb.Append("    m_kameiten k  WITH (READCOMMITTED) ")
        commandTextSb.Append(" ON ")
        commandTextSb.Append("    j.kameiten_cd = k.kameiten_cd ")
        'commandTextSb.Append(" AND ")
        'commandTextSb.Append("    k.torikesi = 0 ")
        commandTextSb.Append(" LEFT OUTER JOIN ")
        commandTextSb.Append("    m_meisyou m  WITH (READCOMMITTED) ")
        commandTextSb.Append(" ON ")
        commandTextSb.Append("    m.meisyou_syubetu = '05' ")
        commandTextSb.Append(" AND ")
        commandTextSb.Append("    m.code = k.nyuukin_kakunin_jyouken ")
        commandTextSb.Append(" LEFT OUTER JOIN  ")
        commandTextSb.Append("    m_tantousya t1  WITH (READCOMMITTED) ")
        commandTextSb.Append(" ON  ")
        commandTextSb.Append("    j.tantousya_cd = t1.tantousya_cd ")
        commandTextSb.Append(" LEFT OUTER JOIN  ")
        commandTextSb.Append("    m_tantousya t2  WITH (READCOMMITTED) ")
        commandTextSb.Append(" ON  ")
        commandTextSb.Append("    j.syouninsya_cd = t2.tantousya_cd ")
        commandTextSb.Append(" LEFT OUTER JOIN  ")
        commandTextSb.Append("    m_hiduke_save hs  WITH (READCOMMITTED) ")
        commandTextSb.Append(" ON  ")
        commandTextSb.Append("    j.kbn = hs.kbn ")
        commandTextSb.Append(" LEFT OUTER JOIN  ")
        commandTextSb.Append("    m_hkks_juri hj  WITH (READCOMMITTED) ")
        commandTextSb.Append(" ON  ")
        commandTextSb.Append("    hj.hkks_jyuri_no = j.koj_hkks_umu ")
        commandTextSb.Append(" LEFT OUTER JOIN  ")
        commandTextSb.Append("    m_kairy_koj_syubetu ks  WITH (READCOMMITTED) ")
        commandTextSb.Append(" ON  ")
        commandTextSb.Append("    ks.kairy_koj_syubetu_no = j.kairy_koj_syubetu ")
        commandTextSb.Append(" WHERE ")
        commandTextSb.Append("    j.kbn = " & strParamKbn)
        commandTextSb.Append(" AND ")
        commandTextSb.Append("    j.hosyousyo_no = " & strParamHosyousyoNo)
        If isNotDataHaki Then
            '�f�[�^�j����ʂ��ݒ肳��Ă��Ȃ����̂����擾����ꍇ
            commandTextSb.Append(" AND")
            commandTextSb.Append("    j.data_haki_syubetu = 0")
        End If
        commandTextSb.Append(" ORDER BY r.kokyaku_brc DESC ")

        ' �f�[�^�̎擾
        Dim JibanDataSet As New JibanDataSet()

        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString, _
            JibanDataSet, JibanDataSet.JibanTable.TableName, commandParameters)

        Dim JibanTable As JibanDataSet.JibanTableDataTable = JibanDataSet.JibanTable

        Return JibanTable

    End Function

#Region "���������p�f�[�^�̎擾"

    ''' <summary>
    ''' �n�Ճ��R�[�h���擾���܂�(���������p)
    ''' </summary>
    ''' <param name="keyRecAcc">�n�Ճf�[�^���������N���X</param>
    ''' <returns>�n�Ռ������ʃf�[�^�e�[�u��</returns>
    ''' <remarks>���������ɂ��������̂ݒn�Ճf�[�^���������N���X�ɐݒ肵�A�n���ĉ�����</remarks>
    Public Function GetJibanSearchData(ByVal keyRecAcc As JibanSearchKeyRecord) As JibanDataSet.JibanSearchTableDataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetJibanData", _
                                            keyRecAcc)

        ' �p�����[�^�ݒ�p
        Dim commandParameters() As SqlParameter = Nothing

        ' SQL��
        Dim commandText As String = getSqlCondition(commandParameters, keyRecAcc)

        ' �f�[�^�̎擾
        Dim JibanDataSet As New JibanDataSet()

        ' �p�����[�^�̗L���ɂ����s
        If commandParameters.Length > 0 Then
            SQLHelper.FillDataset(connStr, CommandType.Text, commandText, _
                JibanDataSet, JibanDataSet.JibanSearchTable.TableName, commandParameters)
        Else
            SQLHelper.FillDataset(connStr, CommandType.Text, commandText, _
                JibanDataSet, JibanDataSet.JibanSearchTable.TableName)
        End If

        Dim JibanSearchTable As JibanDataSet.JibanSearchTableDataTable = JibanDataSet.JibanSearchTable

        Return JibanSearchTable

    End Function

    ''' <summary>
    ''' �n�Ճ��R�[�h�擾�p��SQL���𐶐����܂�(���������p)
    ''' </summary>
    ''' <param name="sql_params">SQL�p�����[�^�z��</param> 
    ''' <param name="keyRecAcc">�n�Ճf�[�^���������N���X</param>
    ''' <returns>�����p��SQL��</returns>
    ''' <remarks></remarks>
    Public Function getSqlCondition(ByRef sql_params As SqlParameter(), ByVal keyRecAcc As JibanSearchKeyRecord) As String

        Dim listParams As New List(Of ParamRecord)

        Dim sql As String = ""
        Dim sqlCondition As New StringBuilder
        Dim strKoujiSQL As String = "@KOUJISQL"

        ' �����̊�{�ƂȂ�SQL��

        Dim baseSql As String = " SELECT " & _
                                "   j.kbn " & _
                                "   , j.hosyousyo_no " & _
                                "   , h.haki_syubetu As data_haki_syubetu " & _
                                "   , j.sesyu_mei " & _
                                "   , j.bukken_jyuusyo1 " & _
                                "   , j.bukken_jyuusyo2 " & _
                                "   , j.bukken_jyuusyo3 " & _
                                "   , j.kameiten_cd " & _
                                "   , k.torikesi AS kt_torikesi " & _
                                "   , km.meisyou AS kt_torikesi_riyuu " & _
                                "   , k.kameiten_mei1 " & _
                                "   , j.tys_jissi_date " & _
                                "   , j.irai_tantousya_mei " & _
                                "   , j.irai_date " & _
                                "   , j.tys_kibou_date " & _
                                "   , j.yoyaku_zumi_flg " & _
                                "   , j.tys_kaisya_cd " & _
                                "   , j.tys_kaisya_jigyousyo_cd " & _
                                "   , tm.tys_kaisya_mei " & _
                                "   , thm.tys_houhou_mei " & _
                                "   , j.syoudakusyo_tys_date " & _
                                "   , t.koumuten_seikyuu_gaku " & _
                                "   , t.uri_gaku " & _
                                "   , t.siire_gaku " & _
                                "   , ttmi.tantousya_mei As tantousya_mei " & _
                                "   , ttmk.tantousya_mei As syouninsya_mei " & _
                                "   , ksm1.ks_siyou As hantei1 " & _
                                "   , kss.ks_siyou_setuzokusi As hantei_setuzoku_moji " & _
                                "   , ksm2.ks_siyou As hantei2 " & _
                                "   , j.keikakusyo_sakusei_date " & _
                                "   , j.hosyousyo_hak_date " & _
                                "   , j.bikou " & _
                                "   , jmm.DisplayName As eigyou_tantousya_mei " & _
                                "   , tk.uri_date AS koj_uri_date " & _
                                "   , j.bunjou_cd " & _
                                "   , j.bukken_nayose_cd " & _
                                "   , j.keiyaku_no " & _
                                " FROM " & _
                                "   t_jiban j  " & _
                                "   LEFT OUTER JOIN m_data_haki h  " & _
                                "     ON j.data_haki_syubetu = h.data_haki_no  " & _
                                "   LEFT OUTER JOIN m_kameiten k  " & _
                                "     ON j.kameiten_cd = k.kameiten_cd  " & _
                                "   LEFT OUTER JOIN m_jhs_mailbox jmm  " & _
                                "     ON k.eigyou_tantousya_mei = jmm.PrimaryWindowsNTAccount  " & _
                                "   LEFT OUTER JOIN m_tyousakaisya tm  " & _
                                "     ON j.tys_kaisya_cd = tm.tys_kaisya_cd  " & _
                                "     AND j.tys_kaisya_jigyousyo_cd = tm.jigyousyo_cd  " & _
                                "   LEFT OUTER JOIN m_tyousahouhou thm  " & _
                                "     ON j.tys_houhou = thm.tys_houhou_no  " & _
                                "   LEFT OUTER JOIN m_tantousya ttmi  " & _
                                "     ON j.tantousya_cd = ttmi.tantousya_cd  " & _
                                "   LEFT OUTER JOIN m_tantousya ttmk  " & _
                                "     ON j.syouninsya_cd = ttmk.tantousya_cd  " & _
                                "   LEFT OUTER JOIN m_ks_siyou ksm1  " & _
                                "     ON j.hantei_cd1 = ksm1.ks_siyou_no  " & _
                                "   LEFT OUTER JOIN m_ks_siyou ksm2  " & _
                                "     ON j.hantei_cd2 = ksm2.ks_siyou_no  " & _
                                "   LEFT OUTER JOIN m_ks_siyou_setuzokusi kss  " & _
                                "     ON j.hantei_setuzoku_moji = kss.ks_siyou_setuzokusi_no  " & _
                                "   LEFT OUTER JOIN (  " & _
                                "     SELECT " & _
                                "       kbn " & _
                                "       , hosyousyo_no " & _
                                "       , SUM(koumuten_seikyuu_gaku) As koumuten_seikyuu_gaku " & _
                                "       , SUM(uri_gaku) As uri_gaku " & _
                                "       , SUM(siire_gaku) As siire_gaku  " & _
                                "     FROM " & _
                                "       t_teibetu_seikyuu  " & _
                                "     WHERE " & _
                                "       bunrui_cd IN ('100', '110', '115', '120')  " & _
                                "       @TSEIKYUU1JOUKEN" & _
                                "     GROUP BY " & _
                                "       kbn " & _
                                "       , hosyousyo_no " & _
                                "   ) t  " & _
                                "     ON j.kbn = t.kbn  " & _
                                "     AND j.hosyousyo_no = t.hosyousyo_no  " & _
                                "   LEFT OUTER JOIN t_teibetu_seikyuu tk  " & _
                                "     ON j.kbn = tk.kbn  " & _
                                "     AND j.hosyousyo_no = tk.hosyousyo_no  " & _
                                "     AND tk.bunrui_cd = '130'  " & _
                                "   LEFT OUTER JOIN m_kakutyou_meisyou AS km  " & _
                                "     ON km.meisyou_syubetu = 56 " & _
                                "     AND km.code = CAST(k.torikesi AS VARCHAR(10))  " & _
                                "   LEFT OUTER JOIN m_todoufuken AS tf  " & _
                                "     ON tf.todouhuken_cd = k.todouhuken_cd " & _
                                " WHERE 0 = 0 " & _
                                " {0} " & _
                                " ORDER BY " & _
                                "   j.kbn " & _
                                "   , j.hosyousyo_no " & _
                                "   , h.haki_syubetu " & _
                                "   , k.kameiten_mei1 " & _
                                "   , j.sesyu_mei "

        '***********************************************************************
        ' �敪�����̐ݒ�
        '***********************************************************************
        '�敪����SQL�i�[�p
        Dim tmpKbnNoJouken As String = String.Empty
        '�敪�w������i�[���X�g
        Dim listKbn As New List(Of String)

        ' �敪_1�̔���
        If IIf(keyRecAcc.Kbn1 Is Nothing, String.Empty, keyRecAcc.Kbn1) IsNot String.Empty Then
            listKbn.Add(keyRecAcc.Kbn1)
        End If
        ' �敪_2�̔���
        If IIf(keyRecAcc.Kbn2 Is Nothing, String.Empty, keyRecAcc.Kbn2) IsNot String.Empty Then
            listKbn.Add(keyRecAcc.Kbn2)
        End If
        ' �敪_3�̔���
        If IIf(keyRecAcc.Kbn3 Is Nothing, String.Empty, keyRecAcc.Kbn3) IsNot String.Empty Then
            listKbn.Add(keyRecAcc.Kbn3)
        End If

        'SQL������
        tmpKbnNoJouken &= (" AND ")
        tmpKbnNoJouken &= (" j.kbn IN ( ")

        If listKbn.Count = 0 Then
            '�������ݒ肳��Ă��Ȃ��ꍇ�A�S�敪��Ώ�
            tmpKbnNoJouken &= (" SELECT kbn FROM m_data_kbn ")
        ElseIf listKbn.Count >= 1 Then
            '�������ЂƂȏ�ݒ肳��Ă���ꍇ
            If listKbn.Count = 1 Then
                '�������ЂƂ����̏ꍇ�A���̂��p�t�H�[�}���X���ቺ����̂ŁA
                '�K���I�}�P�̏����Ƃ��ē����l��ǉ�����
                listKbn.Add(listKbn(0))
            End If

            Dim strParamKbn As String = "@KUBUN"
            Dim ki As Integer = 0
            '�����̐������p�����[�^�ݒ聕SQL�����̃��[�v
            For ki = 0 To listKbn.Count - 1
                strParamKbn &= (ki + 1)
                tmpKbnNoJouken &= (strParamKbn)
                listParams.Add(getParamRecord(strParamKbn, SqlDbType.Char, 1, listKbn(ki)))

                If ki + 1 < listKbn.Count Then
                    '�������Ō�̈�ł͂Ȃ��ꍇ�A���������u,�v��ǉ�
                    tmpKbnNoJouken &= (",")
                End If
            Next
        End If

        tmpKbnNoJouken &= (" ) ")

        '***********************************************************************
        ' ��_�����{�t���O�̐ݒ�
        '***********************************************************************
        ' ��_�����{�t���O�̔���
        If Not IIf(keyRecAcc.TouzaiFlg Is Nothing, String.Empty, keyRecAcc.TouzaiFlg) Is String.Empty Then

            sqlCondition.Append(" AND ")

            listParams.Add(getParamRecord("@TOUZAIFLG", SqlDbType.VarChar, 1, keyRecAcc.TouzaiFlg))
            sqlCondition.Append(" tf.touzai_flg = @TOUZAIFLG ")
        End If

        Dim irai_date_flg As Boolean = False

        '***********************************************************************
        ' �ۏ؏��Ώ۔͈͏����i�ߋ��Q�N�����j
        '***********************************************************************
        ' �ۏ؏��Ώ۔͈͂̔���
        If keyRecAcc.HosyousyoNoHani = 1 And _
          (keyRecAcc.HosyousyoNoFrom Is String.Empty And keyRecAcc.HosyousyoNoTo Is String.Empty) Then

            ' ���ݓ����̂Q�N�O��Key�Ƃ���
            Dim key As String = Date.Now.AddYears(-2).Year.ToString("0000") & "010000"

            tmpKbnNoJouken &= (" AND ")

            listParams.Add(getParamRecord("@HOSYOUSYONOHANI", SqlDbType.VarChar, 10, key))
            tmpKbnNoJouken &= (" j.hosyousyo_no >= @HOSYOUSYONOHANI ")
            tmpKbnNoJouken &= (" AND LEN(j.hosyousyo_no) = 10 ")
            irai_date_flg = True
        End If

        '***********************************************************************
        ' �ۏ؏��͈͏����̐ݒ�
        '***********************************************************************
        ' ��ł��ݒ肳��Ă���ꍇ�A�������쐬
        If Not IIf(keyRecAcc.HosyousyoNoFrom Is Nothing, String.Empty, keyRecAcc.HosyousyoNoFrom) Is String.Empty Or _
           Not IIf(keyRecAcc.HosyousyoNoTo Is Nothing, String.Empty, keyRecAcc.HosyousyoNoTo) Is String.Empty Then

            tmpKbnNoJouken &= (" AND ")

            If Not keyRecAcc.HosyousyoNoFrom Is String.Empty And _
               Not keyRecAcc.HosyousyoNoTo Is String.Empty Then
                ' �����w��L���BETWEEN
                tmpKbnNoJouken &= (" j.hosyousyo_no BETWEEN @HOSYOUSYONOFROM AND @HOSYOUSYONOTO")
                listParams.Add(getParamRecord("@HOSYOUSYONOFROM", SqlDbType.VarChar, 10, keyRecAcc.HosyousyoNoFrom))
                listParams.Add(getParamRecord("@HOSYOUSYONOTO", SqlDbType.VarChar, 10, keyRecAcc.HosyousyoNoTo))
            Else
                If Not keyRecAcc.HosyousyoNoFrom Is String.Empty Then
                    ' �ۏ؏�From�̂�
                    tmpKbnNoJouken &= (" j.hosyousyo_no >= @HOSYOUSYONOFROM ")
                    listParams.Add(getParamRecord("@HOSYOUSYONOFROM", SqlDbType.VarChar, 10, keyRecAcc.HosyousyoNoFrom))
                Else
                    ' �ۏ؏�To�̂�
                    tmpKbnNoJouken &= (" j.hosyousyo_no <= @HOSYOUSYONOTO ")
                    listParams.Add(getParamRecord("@HOSYOUSYONOTO", SqlDbType.VarChar, 10, keyRecAcc.HosyousyoNoTo))
                End If
            End If

            If irai_date_flg = False Then
                tmpKbnNoJouken &= (" AND LEN(j.hosyousyo_no) = 10 ")
                'sql_condition.Append(" AND j.irai_date IS NOT NULL ")
            End If
        End If

        '�敪�Ɣԍ���WHERE�������Z�b�g
        sqlCondition.Append(tmpKbnNoJouken)
        '�敪�Ɣԍ���WHERE�������A�@�ʐ����e�[�u���̏����ɂ��Z�b�g
        baseSql = baseSql.Replace("@TSEIKYUU1JOUKEN", tmpKbnNoJouken.Replace("j.", ""))

        '***********************************************************************
        ' �����X�R�[�h�̐ݒ�
        '***********************************************************************
        ' �����X�R�[�h�̔���
        If Not IIf(keyRecAcc.KameitenCd Is Nothing, String.Empty, keyRecAcc.KameitenCd) Is String.Empty Then

            sqlCondition.Append(" AND ")

            listParams.Add(getParamRecord("@KAMEITENCD", SqlDbType.VarChar, 5, keyRecAcc.KameitenCd))
            sqlCondition.Append(" j.kameiten_cd = @KAMEITENCD ")
        End If

        '***********************************************************************
        ' �����X�J�i�P�̐ݒ�
        '***********************************************************************
        ' �����X�J�i�P�̔���
        If Not IIf(keyRecAcc.TenmeiKana1 Is Nothing, String.Empty, keyRecAcc.TenmeiKana1) Is String.Empty Then

            sqlCondition.Append(" AND ")

            listParams.Add(getParamRecord("@TENMEIKANA1", SqlDbType.VarChar, 20, keyRecAcc.TenmeiKana1 & "%"))
            sqlCondition.Append(" (k.tenmei_kana1 like @TENMEIKANA1 OR k.tenmei_kana2 like @TENMEIKANA1)")
        End If

        '***********************************************************************
        ' �n��R�[�h�̐ݒ�
        '***********************************************************************
        ' �n��R�[�h�̔���
        If Not IIf(keyRecAcc.KeiretuCd Is Nothing, String.Empty, keyRecAcc.KeiretuCd) Is String.Empty Then

            sqlCondition.Append(" AND ")

            listParams.Add(getParamRecord("@KEIRETU", SqlDbType.VarChar, 5, keyRecAcc.KeiretuCd))
            sqlCondition.Append(" k.keiretu_cd = @KEIRETU ")
        End If

        '***********************************************************************
        ' �c�Ə��R�[�h�̐ݒ�
        '***********************************************************************
        ' �c�Ə��R�[�h�̔���
        If Not IIf(keyRecAcc.EigyousyoCd Is Nothing, String.Empty, keyRecAcc.EigyousyoCd) Is String.Empty Then

            sqlCondition.Append(" AND ")

            listParams.Add(getParamRecord("@EIGYOUSYO", SqlDbType.VarChar, 5, keyRecAcc.EigyousyoCd))
            sqlCondition.Append(" k.eigyousyo_cd = @EIGYOUSYO ")
        End If

        '***********************************************************************
        ' ������ЃR�[�h�̐ݒ�
        '***********************************************************************
        ' ������ЃR�[�h�̔���
        If Not IIf(keyRecAcc.TysKaisyaCd Is Nothing, String.Empty, keyRecAcc.TysKaisyaCd) Is String.Empty Then

            sqlCondition.Append(" AND ")

            listParams.Add(getParamRecord("@TYSKAISYACD", SqlDbType.VarChar, 7, keyRecAcc.TysKaisyaCd))
            sqlCondition.Append(" j.tys_kaisya_cd + j.tys_kaisya_jigyousyo_cd = @TYSKAISYACD ")
        End If

        '***********************************************************************
        ' �H����ЃR�[�h�̐ݒ�
        '***********************************************************************
        ' �H����ЃR�[�h�̔���
        If Not IIf(keyRecAcc.KojGaisyaCd Is Nothing, String.Empty, keyRecAcc.KojGaisyaCd) Is String.Empty Then

            sqlCondition.Append(" AND ")

            listParams.Add(getParamRecord("@KOUJIGAISYACD", SqlDbType.VarChar, 7, keyRecAcc.KojGaisyaCd))
            sqlCondition.Append(" j.koj_gaisya_cd + j.koj_gaisya_jigyousyo_cd = @KOUJIGAISYACD ")
        End If

        '***********************************************************************
        ' �H������N�����͈͏����̐ݒ�
        '***********************************************************************
        createDateHanniSql(sqlCondition, listParams, _
                           keyRecAcc.KojUriDateFrom, keyRecAcc.KojUriDateTo, _
                           "tk.uri_date", "@KOJURIDATE")

        '***********************************************************************
        ' �H�������\����͈͏����̐ݒ�
        '***********************************************************************
        createDateHanniSql(sqlCondition, listParams, _
                           keyRecAcc.KairyKojKanryYoteiDateFrom, keyRecAcc.KairyKojKanryYoteiDateTo, _
                           "j.kairy_koj_kanry_yotei_date", "@KOJKANRYOUYOTEIDATE")

        '***********************************************************************
        ' �{�喼�̐ݒ�
        '***********************************************************************
        ' �{�喼�̔���
        If Not IIf(keyRecAcc.SesyuMei Is Nothing, String.Empty, keyRecAcc.SesyuMei) Is String.Empty Then

            sqlCondition.Append(" AND ")

            listParams.Add(getParamRecord("@SESYUMEI", SqlDbType.VarChar, 50 + 1, keyRecAcc.SesyuMei & "%"))
            sqlCondition.Append(" REPLACE(j.sesyu_mei, ' ', '') like REPLACE(@SESYUMEI, ' ', '') ")
        End If

        '***********************************************************************
        ' �����Z��1+2�̐ݒ�
        '***********************************************************************
        ' �����Z��1+2�̔���
        If Not IIf(keyRecAcc.BukkenJyuusyo12 Is Nothing, String.Empty, keyRecAcc.BukkenJyuusyo12) Is String.Empty Then

            sqlCondition.Append(" AND ")

            listParams.Add(getParamRecord("@BUKKENJYUUSYO12", SqlDbType.VarChar, 64 + 1, keyRecAcc.BukkenJyuusyo12 & "%"))
            sqlCondition.Append(" isnull(j.bukken_jyuusyo1, '') + isnull(j.bukken_jyuusyo2, '') like @BUKKENJYUUSYO12 ")
        End If

        '***********************************************************************
        ' ���l�̐ݒ�
        '***********************************************************************
        ' ���l�̔���
        If Not IIf(keyRecAcc.Bikou Is Nothing, String.Empty, keyRecAcc.Bikou) Is String.Empty Then

            sqlCondition.Append(" AND ")

            listParams.Add(getParamRecord("@BIKOU", SqlDbType.VarChar, 256 + 1, keyRecAcc.Bikou & "%"))
            sqlCondition.Append(" j.bikou like @BIKOU ")
        End If

        '***********************************************************************
        ' �˗����͈͏����̐ݒ�
        '***********************************************************************
        createDateHanniSql(sqlCondition, listParams, _
                           keyRecAcc.IraiDateFrom, keyRecAcc.IraiDateTo, _
                           "j.irai_date", "@IRAIDATE")

        '***********************************************************************
        ' ������]���͈͏����̐ݒ�
        '***********************************************************************
        createDateHanniSql(sqlCondition, listParams, _
                           keyRecAcc.TyousaKibouDateFrom, keyRecAcc.TyousaKibouDateTo, _
                           "j.tys_kibou_date", "@TYOUSAKIBOUDATE")

        '***********************************************************************
        ' �������{���͈͏����̐ݒ�
        '***********************************************************************
        createDateHanniSql(sqlCondition, listParams, _
                           keyRecAcc.TyousaJissiDateFrom, keyRecAcc.TyousaJissiDateTo, _
                           "j.tys_jissi_date", "@TYOUSAJISSIDATE")

        '***********************************************************************
        ' �ۏ؏����s���͈͏����̐ݒ�
        '***********************************************************************
        createDateHanniSql(sqlCondition, listParams, _
                           keyRecAcc.HosyousyoHakkouDateFrom, keyRecAcc.HosyousyoHakkouDateTo, _
                           "j.hosyousyo_hak_date", "@HOSYOUSYOHAKKOUDATE")

        '***********************************************************************
        ' �������������͈͏����̐ݒ�
        '***********************************************************************
        createDateHanniSql(sqlCondition, listParams, _
                           keyRecAcc.SyoudakusyoTyousaDateFrom, keyRecAcc.SyoudakusyoTyousaDateTo, _
                           "j.syoudakusyo_tys_date", "@SYOUDAKUSYOTYOUSADATE")

        '***********************************************************************
        ' �v�揑�쐬���͈͏����̐ݒ�
        '***********************************************************************
        createDateHanniSql(sqlCondition, listParams, _
                           keyRecAcc.KeikakusyoSakuseiDateFrom, keyRecAcc.KeikakusyoSakuseiDateTo, _
                           "j.keikakusyo_sakusei_date", "@KEIKAKUSYOSAKUSEIDATE")

        '***********************************************************************
        ' �ۏ؏����s�˗��������͈͏����̐ݒ�
        '***********************************************************************
        createDateHanniSql(sqlCondition, listParams, _
                           keyRecAcc.HosyousyoHakkouIraisyoTyakuDateFrom, keyRecAcc.HosyousyoHakkouIraisyoTyakuDateTo, _
                           "j.hosyousyo_hak_iraisyo_tyk_date", "@HOSYOUSYOHAKKOUIRAISYOTYAKUDATE")

        '***********************************************************************
        ' �f�[�^�j�����
        '***********************************************************************
        ' �f�[�^�j����ʂ̔���
        If keyRecAcc.DataHakiSyubetu = 0 Then

            sqlCondition.Append(" AND ")

            listParams.Add(getParamRecord("@DATAHAKISYUBETU", SqlDbType.Int, 10, keyRecAcc.DataHakiSyubetu))
            sqlCondition.Append(" ISNULL(j.data_haki_syubetu,0) = @DATAHAKISYUBETU ")

        End If

        '***********************************************************************
        ' �\���FLG
        '***********************************************************************
        ' �\���FLG�̔���
        If keyRecAcc.YoyakuZumiFlg = 1 Then

            sqlCondition.Append(" AND ")

            listParams.Add(getParamRecord("@YOYAKUZUMI", SqlDbType.Int, 10, keyRecAcc.YoyakuZumiFlg))
            sqlCondition.Append(" ISNULL(j.yoyaku_zumi_flg,0) = @YOYAKUZUMI ")

        End If

        '***********************************************************************
        ' �����R�[�h
        '***********************************************************************
        ' �����R�[�h�̔���
        If keyRecAcc.BunjouCd <> Integer.MinValue Then

            sqlCondition.Append(" AND ")

            listParams.Add(getParamRecord("@BUNJOUCD", SqlDbType.Int, 4, keyRecAcc.BunjouCd))
            sqlCondition.Append(" ISNULL(j.bunjou_cd, 0) = @BUNJOUCD")

        End If

        '***********************************************************************
        ' ��������R�[�h
        '***********************************************************************
        ' ��������R�[�h�̔���
        If keyRecAcc.BukkenNayoseCd IsNot Nothing AndAlso keyRecAcc.BukkenNayoseCd <> String.Empty Then

            sqlCondition.Append(" AND ")

            listParams.Add(getParamRecord("@BUKKENNNAYOSECD", SqlDbType.VarChar, 12, keyRecAcc.BukkenNayoseCd & "%"))
            sqlCondition.Append(" ISNULL(j.bukken_nayose_cd, 0) like @BUKKENNNAYOSECD")

        End If

        '***********************************************************************
        ' �_��NO
        '***********************************************************************
        ' �_��NO�̔���
        If keyRecAcc.KeiyakuNo IsNot Nothing AndAlso keyRecAcc.KeiyakuNo <> String.Empty Then

            sqlCondition.Append(" AND ")

            listParams.Add(getParamRecord("@KEIYAKUNO", SqlDbType.VarChar, 20, keyRecAcc.KeiyakuNo & "%"))
            sqlCondition.Append(" ISNULL(j.keiyaku_no, 0) like @KEIYAKUNO")

        End If

        ' �p�����[�^�̍쐬
        Dim i As Integer
        Dim cmdParams(listParams.Count - 1) As SqlParameter
        For i = 0 To listParams.Count - 1
            Dim rec As ParamRecord = listParams(i)
            ' �K�v�ȏ����Z�b�g
            cmdParams(i) = SQLHelper.MakeParam(rec.Param, rec.DbType, rec.ParamLength, rec.SetData)
        Next

        ' �ԋp�p�p�����[�^�̃Z�b�g
        sql_params = cmdParams

        ' �ŏI�I��SQL���̐���
        sql = String.Format(baseSql, sqlCondition.ToString())

        Return sql

    End Function

    ''' <summary>
    ''' �p�����[�^���R�[�h���쐬���܂�(���������p)
    ''' </summary>
    ''' <param name="paramName">�p�����[�^��</param>
    ''' <param name="dbType">DB����</param>
    ''' <param name="length">����</param>
    ''' <param name="data">�ݒ肷��f�[�^</param>
    ''' <returns>�p�����[�^���R�[�h</returns>
    ''' <remarks></remarks>
    Private Function getParamRecord(ByVal paramName As String, _
                                    ByVal dbType As SqlDbType, _
                                    ByVal length As Integer, _
                                    ByVal data As Object) As ParamRecord
        Dim paramRec As New ParamRecord

        paramRec.Param = paramName
        paramRec.DbType = dbType
        paramRec.ParamLength = length
        paramRec.SetData = data

        Return paramRec
    End Function

    ''' <summary>
    ''' ���t�͈͌�������SQL������(���������p)
    ''' </summary>
    ''' <param name="sqlCondition">SQL����~�ς���StringBuilder</param>
    ''' <param name="arrParams">DB�ւ̃p�����[�^��~�ς���List</param>
    ''' <param name="dateFrom">FROM���t</param>
    ''' <param name="dateTo">TO���t</param>
    ''' <param name="strColumn">�����ΏۃJ����������</param>
    ''' <param name="strTarget">�p�����[�^�u���p������</param>
    ''' <remarks></remarks>
    Private Sub createDateHanniSql(ByRef sqlCondition As StringBuilder, _
                                   ByRef arrParams As List(Of ParamRecord), _
                                   ByVal dateFrom As Date, _
                                   ByVal dateTo As Date, _
                                   ByVal strColumn As String, _
                                   ByVal strTarget As String)

        ' ���t����ł��ݒ肳��Ă���ꍇ�A�������쐬
        If dateFrom <> DateTime.MinValue Or _
           dateTo <> DateTime.MinValue Then

            Dim strTargetFrom As String = strTarget & "FROM"
            Dim strTargetTo As String = strTarget & "TO"

            sqlCondition.Append(" AND ")

            If dateFrom <> DateTime.MinValue And _
               dateTo <> DateTime.MinValue Then
                ' �����w��L���BETWEEN
                sqlCondition.Append(String.Format(" {0} BETWEEN {1} AND {2} ", strColumn, strTargetFrom, strTargetTo))
                arrParams.Add(getParamRecord(strTargetFrom, SqlDbType.DateTime, 16, dateFrom))
                arrParams.Add(getParamRecord(strTargetTo, SqlDbType.DateTime, 16, dateTo))
            Else
                If dateFrom <> DateTime.MinValue Then
                    ' From�̂�
                    sqlCondition.Append(String.Format(" {0} >= {1} ", strColumn, strTargetFrom))
                    arrParams.Add(getParamRecord(strTargetFrom, SqlDbType.DateTime, 16, dateFrom))
                Else
                    ' To�̂�
                    sqlCondition.Append(String.Format(" {0} <= {1} ", strColumn, strTargetTo))
                    arrParams.Add(getParamRecord(strTargetTo, SqlDbType.DateTime, 16, dateTo))
                End If
            End If

        End If
    End Sub

#End Region

    ''' <summary>
    ''' �n�Ճ��R�[�h�������������o�^�̔�����s���܂�
    ''' </summary>
    ''' <param name="kbn">�敪</param>
    ''' <param name="hosyousyoNo">�ۏ؏�NO</param>   
    ''' <returns>True:�V�K�o�^�i�����o�^��ԁjFalse:�o�^��</returns>
    ''' <remarks>�V�K�̔ԍςł��鎖��O��Ƃ��A�X�V�̗L�����`�F�b�N���܂�</remarks>
    Public Function ChkJibanNew(ByVal kbn As String, _
                                ByVal hosyousyoNo As String) As Boolean

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".ChkJibanNew", _
                                    kbn, _
                                    hosyousyoNo)

        ' �p�����[�^
        Const strParamKbn As String = "@KBN"
        Const strParamHosyousyoNo As String = "@HOSYOUSYONO"

        ' �p�����[�^�֐ݒ�
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamKbn, SqlDbType.Char, 1, kbn), _
             SQLHelper.MakeParam(strParamHosyousyoNo, SqlDbType.VarChar, 10, hosyousyoNo)}

        Dim commandText As String = " SELECT " & _
                                    "   upd_datetime " & _
                                    " FROM " & _
                                    "    t_jiban WITH (READCOMMITTED) " & _
                                    " WHERE " & _
                                    "    kbn = " & strParamKbn & _
                                    " AND " & _
                                    "    hosyousyo_no = " & strParamHosyousyoNo

        ' �f�[�^�̎擾
        Dim ret As Object = SQLHelper.ExecuteScalar(connStr, _
                                                    CommandType.Text, _
                                                    commandText, _
                                                    commandParameters)

        If ret Is System.DBNull.Value Then
            Return True
        End If

        Return False

    End Function

    ''' <summary>
    ''' �n�Ճ��R�[�h�����������݃`�F�b�N���s���܂�
    ''' </summary>
    ''' <param name="kbn">�敪</param>
    ''' <param name="hosyousyoNo">�ۏ؏�NO</param>   
    ''' <returns>True:���݂��� False:���݂��Ȃ�</returns>
    ''' <remarks>
    ''' ���p�Ȍ����������ׁA�p�����[�^�̗L���`�F�b�N��<br/>
    ''' ���W�b�N�N���X�Ɏ������Ă�������(��,Nothing�̊m�F)</remarks>
    Public Function IsJibanData(ByVal kbn As String, _
                                ByVal hosyousyoNo As String) As Boolean

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".ChkJibanNew", _
                                    kbn, _
                                    hosyousyoNo)

        ' �p�����[�^
        Const strParamKbn As String = "@KBN"
        Const strParamHosyousyoNo As String = "@HOSYOUSYONO"

        ' �p�����[�^�֐ݒ�
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamKbn, SqlDbType.Char, 1, kbn), _
             SQLHelper.MakeParam(strParamHosyousyoNo, SqlDbType.VarChar, 10, hosyousyoNo)}

        Dim commandText As String = " SELECT " & _
                                    "   kbn " & _
                                    " FROM " & _
                                    "    t_jiban WITH (READCOMMITTED) " & _
                                    " WHERE " & _
                                    "    kbn = " & strParamKbn & _
                                    " AND " & _
                                    "    hosyousyo_no = " & strParamHosyousyoNo

        ' �f�[�^�̎擾
        Dim ret As Object = SQLHelper.ExecuteScalar(connStr, _
                                                    CommandType.Text, _
                                                    commandText, _
                                                    commandParameters)

        If ret Is Nothing Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' �n�Ճ��R�[�h���������X�V�������擾���܂�
    ''' </summary>
    ''' <param name="kbn">�敪</param>
    ''' <param name="hosyousyoNo">�ۏ؏�NO</param>   
    ''' <returns>�X�V����</returns>
    ''' <remarks></remarks>
    Public Function GetJibanUpdDateTime(ByVal kbn As String, _
                                        ByVal hosyousyoNo As String) As DateTime

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".ChkJibanNew", _
                                    kbn, _
                                    hosyousyoNo)

        ' �p�����[�^
        Const strParamKbn As String = "@KBN"
        Const strParamHosyousyoNo As String = "@HOSYOUSYONO"

        ' �p�����[�^�֐ݒ�
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamKbn, SqlDbType.Char, 1, kbn), _
             SQLHelper.MakeParam(strParamHosyousyoNo, SqlDbType.VarChar, 10, hosyousyoNo)}

        Dim commandText As String = " SELECT " & _
                                    "   upd_datetime " & _
                                    " FROM " & _
                                    "    t_jiban WITH (READCOMMITTED) " & _
                                    " WHERE " & _
                                    "    kbn = " & strParamKbn & _
                                    " AND " & _
                                    "    hosyousyo_no = " & strParamHosyousyoNo

        ' �f�[�^�̎擾
        Dim ret As Object = SQLHelper.ExecuteScalar(connStr, _
                                                    CommandType.Text, _
                                                    commandText, _
                                                    commandParameters)

        If ret Is System.DBNull.Value Then
            Return DateTime.MinValue
        End If

        Return ret

    End Function

    ''' <summary>
    ''' �n�Ճf�[�^�ɐݒ肳��Ă��镪���R�[�h���擾���܂�
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetBunjouCd(ByVal strBunjouCd As String) As Boolean

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetBunjouCd", strBunjouCd)

        Dim blnRes As Boolean = False

        Dim cmdParams() As SqlParameter = {}
        Dim commandTextSb As New StringBuilder
        Dim dTblRes As New DataTable

        'SQL����
        commandTextSb.Append(" SELECT ")
        commandTextSb.Append("   j.bunjou_cd ")
        commandTextSb.Append(" FROM ")
        commandTextSb.Append("    t_jiban j  WITH (READCOMMITTED) ")
        commandTextSb.Append(" WHERE 1=1 ")
        '�f�[�^�j����ʂ��ݒ肳��Ă��Ȃ����̂����擾
        commandTextSb.Append(" AND")
        commandTextSb.Append("    j.data_haki_syubetu = 0")
        commandTextSb.Append(" AND ")
        commandTextSb.Append("    j.bunjou_cd IS NOT NULL ")
        commandTextSb.Append(" AND ")
        commandTextSb.Append("    j.bunjou_cd = " & strBunjouCd)

        '�f�[�^�擾���ԋp
        dTblRes = cmnDtAcc.getDataTable(commandTextSb.ToString, cmdParams)
        If Not dTblRes Is Nothing AndAlso dTblRes.Rows.Count > 0 Then
            blnRes = True
        End If

        Return blnRes
    End Function

    ''' <summary>
    ''' �n�Ճ��R�[�h�𑖍����Y�����������������疼�񂳂�Ă��邩�̃`�F�b�N���s���܂�
    ''' </summary>
    ''' <param name="kbn">�敪</param>
    ''' <param name="hosyousyoNo">�ۏ؏�NO</param>
    ''' <param name="nayoseKbn">�����.�敪</param>
    ''' <param name="nayoseHosyousyoNo">�����.�ۏ؏�NO</param>
    ''' <returns>True:���݂��� False:���݂��Ȃ�</returns>
    ''' <remarks></remarks>
    Public Function ChkJibanDataNayoseNotChildren( _
                                                    ByVal kbn As String, _
                                                    ByVal hosyousyoNo As String, _
                                                    ByVal nayoseKbn As String, _
                                                    ByVal nayoseHosyousyoNo As String _
                                                ) As Boolean

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".ChkJibanDataNayoseNotChildren", _
                                    kbn, hosyousyoNo, nayoseKbn, nayoseHosyousyoNo)

        ' �p�����[�^
        Const strPrmKbn1 As String = "@KBN1" '�敪
        Const strPrmHosyousyoNo1 As String = "@HOSYOUSYONO1" '�ԍ�
        Const strPrmBukkenNo1 As String = "@BUKKENNO1" '����NO
        ' �p�����[�^(�����)
        Const strPrmKbn2 As String = "@KBN2" '�敪
        Const strPrmHosyousyoNo2 As String = "@HOSYOUSYONO2" '�ԍ�
        Const strPrmBukkenNo2 As String = "@BUKKENNO2" '����NO

        If kbn = nayoseKbn And hosyousyoNo = nayoseHosyousyoNo Then '�������ɖ��񂷂�ꍇ�A�f�[�^�`�F�b�N�s�v
            Return True
        End If

        Dim cmdTextSb As New StringBuilder

        '���o�N�G������
        '������NO��(�������ȊO��)���������疼�񂳂�Ă��Ȃ��ꍇOK
        cmdTextSb.Append(" SELECT ")
        cmdTextSb.Append("      TJ3.kbn ")
        cmdTextSb.Append("  FROM ")
        cmdTextSb.Append("      t_jiban TJ3 ")
        cmdTextSb.Append("  WHERE ")
        cmdTextSb.Append("      1=1 ")
        cmdTextSb.Append("  AND NOT ( ")
        cmdTextSb.Append("      TJ3.kbn = " & strPrmKbn1)
        cmdTextSb.Append("      AND TJ3.hosyousyo_no = " & strPrmHosyousyoNo1)
        cmdTextSb.Append("      ) ")
        cmdTextSb.Append("  AND TJ3.bukken_nayose_cd IS NOT NULL ")
        cmdTextSb.Append("  AND TJ3.bukken_nayose_cd = " & strPrmBukkenNo1)

        ' �p�����[�^�֐ݒ�
        Dim cmdPrms() As SqlParameter = _
            {SQLHelper.MakeParam(strPrmKbn1, SqlDbType.Char, 1, kbn), _
             SQLHelper.MakeParam(strPrmHosyousyoNo1, SqlDbType.VarChar, 10, hosyousyoNo), _
             SQLHelper.MakeParam(strPrmBukkenNo1, SqlDbType.VarChar, 11, kbn & hosyousyoNo), _
             SQLHelper.MakeParam(strPrmKbn2, SqlDbType.Char, 1, nayoseKbn), _
             SQLHelper.MakeParam(strPrmHosyousyoNo2, SqlDbType.VarChar, 10, nayoseHosyousyoNo), _
             SQLHelper.MakeParam(strPrmBukkenNo2, SqlDbType.VarChar, 11, nayoseKbn & nayoseHosyousyoNo)}

        ' �f�[�^�̎擾
        Dim ret As Object = SQLHelper.ExecuteScalar(connStr, _
                                                    CommandType.Text, _
                                                    cmdTextSb.ToString, _
                                                    cmdPrms)
        '�l���Ƃ��ꍇ�A�G���[
        If ret Is Nothing Then
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' �n�Ճ��R�[�h�𑖍����A�Y���������e�����ł��邩�ǂ����̔�����s�Ȃ�
    ''' </summary>
    ''' <param name="kbn">�敪</param>
    ''' <param name="hosyousyoNo">�ۏ؏�NO</param>
    ''' <returns>True:���݂��� False:���݂��Ȃ�</returns>
    ''' <remarks></remarks>
    Public Function ChkJibanDataOyaBukken( _
                                            ByVal kbn As String, _
                                            ByVal hosyousyoNo As String, _
                                            ByVal nayoseKbn As String, _
                                            ByVal nayoseHosyousyoNo As String _
                                                ) As Boolean

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".ChkJibanDataOyaBukken", _
                                    kbn, hosyousyoNo, nayoseKbn, nayoseHosyousyoNo)

        ' �p�����[�^
        Const strPrmKbn1 As String = "@KBN1" '�敪
        Const strPrmHosyousyoNo1 As String = "@HOSYOUSYONO1" '�ԍ�
        Const strPrmBukkenNo1 As String = "@BUKKENNO1" '����NO

        If kbn = nayoseKbn And hosyousyoNo = nayoseHosyousyoNo Then '�������ɖ��񂷂�ꍇ�A�f�[�^�`�F�b�N�s�v
            Return True
        End If

        Dim cmdTextSb As New StringBuilder

        '���o�N�G������
        '�Y�������͐e����
        cmdTextSb.Append(" SELECT ")
        cmdTextSb.Append("      TJ3.kbn ")
        cmdTextSb.Append("  FROM ")
        cmdTextSb.Append("      t_jiban TJ3 ")
        cmdTextSb.Append("  WHERE ")
        cmdTextSb.Append("      1=1 ")
        cmdTextSb.Append("  AND TJ3.kbn = " & strPrmKbn1)
        cmdTextSb.Append("  AND TJ3.hosyousyo_no = " & strPrmHosyousyoNo1)
        cmdTextSb.Append("  AND TJ3.bukken_nayose_cd IS NOT NULL ")
        cmdTextSb.Append("  AND TJ3.bukken_nayose_cd = " & strPrmBukkenNo1)

        ' �p�����[�^�֐ݒ�
        Dim cmdPrms() As SqlParameter = _
            {SQLHelper.MakeParam(strPrmKbn1, SqlDbType.Char, 1, nayoseKbn), _
             SQLHelper.MakeParam(strPrmHosyousyoNo1, SqlDbType.VarChar, 10, nayoseHosyousyoNo), _
             SQLHelper.MakeParam(strPrmBukkenNo1, SqlDbType.VarChar, 11, nayoseKbn & nayoseHosyousyoNo) _
             }

        ' �f�[�^�̎擾
        Dim ret As Object = SQLHelper.ExecuteScalar(connStr, _
                                                    CommandType.Text, _
                                                    cmdTextSb.ToString, _
                                                    cmdPrms)

        '�l���Ƃ�Ȃ��ꍇ�A�G���[
        If ret Is Nothing Then
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' �n�Ճ��R�[�h�𑖍����ŐV�̕�������󋵂̃`�F�b�N���s���܂�
    ''' </summary>
    ''' <param name="kbn">�敪</param>
    ''' <param name="hosyousyoNo">�ۏ؏�NO</param>   
    ''' <returns>True:���݂��� False:���݂��Ȃ�</returns>
    ''' <remarks></remarks>
    Public Function ChkLatestJibanDataNayoseJyky( _
                                                    ByVal kbn As String, _
                                                    ByVal hosyousyoNo As String _
                                                ) As Boolean

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".ChkLatestJibanDataNayoseJyky", _
                                    kbn, _
                                    hosyousyoNo)

        ' �p�����[�^
        Const strPrmKbn1 As String = "@KBN1" '�敪
        Const strPrmHosyousyoNo1 As String = "@HOSYOUSYONO1" '�ԍ�
        Const strPrmBukkenNo1 As String = "@BUKKENNO1" '����NO

        Dim cmdTextSb As New StringBuilder

        '���o�N�G������
        cmdTextSb.Append(" SELECT ")
        cmdTextSb.Append("      TJ1.kbn ")
        cmdTextSb.Append("    , TJ1.hosyousyo_no ")
        cmdTextSb.Append(" FROM ")
        cmdTextSb.Append("      t_jiban TJ1 ")
        cmdTextSb.Append(" WHERE ")
        cmdTextSb.Append("      1=1 ")
        cmdTextSb.Append("  AND TJ1.kbn = " & strPrmKbn1)
        cmdTextSb.Append("  AND TJ1.hosyousyo_no = " & strPrmHosyousyoNo1)
        cmdTextSb.Append("  AND EXISTS ")
        cmdTextSb.Append("     (SELECT ")
        cmdTextSb.Append("           * ")
        cmdTextSb.Append("      FROM ")
        cmdTextSb.Append("           t_jiban TJ3 ")
        cmdTextSb.Append("      WHERE ")
        cmdTextSb.Append("           1=1 ")
        cmdTextSb.Append("       AND NOT (TJ3.kbn = " & strPrmKbn1)
        cmdTextSb.Append("           AND TJ3.hosyousyo_no = " & strPrmHosyousyoNo1 & ")")
        cmdTextSb.Append("       AND TJ3.bukken_nayose_cd IS NOT NULL ")
        cmdTextSb.Append("       AND TJ3.bukken_nayose_cd = " & strPrmBukkenNo1)
        cmdTextSb.Append("     ) ")

        ' �p�����[�^�֐ݒ�
        Dim cmdPrms() As SqlParameter = _
            {SQLHelper.MakeParam(strPrmKbn1, SqlDbType.Char, 1, kbn), _
             SQLHelper.MakeParam(strPrmHosyousyoNo1, SqlDbType.VarChar, 10, hosyousyoNo), _
             SQLHelper.MakeParam(strPrmBukkenNo1, SqlDbType.VarChar, 11, kbn & hosyousyoNo)}

        ' �f�[�^�̎擾
        Dim ret As Object = SQLHelper.ExecuteScalar(connStr, _
                                                    CommandType.Text, _
                                                    cmdTextSb.ToString, _
                                                    cmdPrms)

        '���݂��Ȃ����OK
        If ret Is Nothing Then
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="kbn"></param>
    ''' <param name="bangou"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBukkenNayoseNotEqualList( _
                                                ByVal kbn As String, _
                                                ByVal bangou As String _
                                            ) As String

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetBukkenNayoseNotEqualList", _
                                    kbn, bangou)

        ' �p�����[�^
        Const strPrmKbn1 As String = "@KBN1" '�敪
        Const strPrmHosyousyoNo1 As String = "@HOSYOUSYONO1" '�ԍ�
        Const strPrmBukkenNo1 As String = "@BUKKENNO1" '����NO

        Dim dTblRes As New DataTable
        Dim strRetBukkenNo As String = String.Empty
        Dim cmdTextSb As New StringBuilder

        '���o�N�G������
        cmdTextSb.Append(" SELECT ")
        cmdTextSb.Append("      TJ1.kbn AS kbn ")
        cmdTextSb.Append("    , TJ1.hosyousyo_no AS bangou ")
        cmdTextSb.Append("    , TJ1.bukken_nayose_cd AS bukken_nayose_cd ")
        cmdTextSb.Append(" FROM ")
        cmdTextSb.Append("      t_jiban TJ1 ")
        cmdTextSb.Append(" WHERE ")
        cmdTextSb.Append("      1=1 ")
        cmdTextSb.Append("  AND TJ1.kbn = " & strPrmKbn1)
        cmdTextSb.Append("  AND TJ1.hosyousyo_no = " & strPrmHosyousyoNo1)
        cmdTextSb.Append("  AND TJ1.bukken_nayose_cd IS NOT NULL ")
        cmdTextSb.Append("  AND TJ1.bukken_nayose_cd <> " & strPrmBukkenNo1)

        ' �p�����[�^�֐ݒ�
        Dim cmdPrms() As SqlParameter = _
            {SQLHelper.MakeParam(strPrmKbn1, SqlDbType.Char, 1, kbn), _
             SQLHelper.MakeParam(strPrmHosyousyoNo1, SqlDbType.VarChar, 10, bangou), _
             SQLHelper.MakeParam(strPrmBukkenNo1, SqlDbType.VarChar, 11, kbn & bangou) _
        }

        '�f�[�^�擾���ԋp
        dTblRes = cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdPrms)
        If dTblRes.Rows.Count > 0 Then
            strRetBukkenNo = dTblRes.Rows(0)("kbn").ToString & dTblRes.Rows(0)("bangou").ToString
        End If
        Return strRetBukkenNo
    End Function

    ''' <summary>
    ''' �i���ۏ؏��󋵌����p�f�[�^���擾���܂�
    ''' </summary>
    ''' <param name="keyRec">�i��Key���R�[�h</param>
    ''' <returns>�f�[�^�e�[�u��</returns>
    ''' <remarks></remarks>
    Public Function GetJibanDataHinsitu(ByVal keyRec As HinsituHosyousyoJyoukyouRecord) As DataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetJibanDataHinsitu", _
                                                    keyRec)
        'SQL���̐���
        Dim sql As String = String.Empty
        Dim cmdParams() As SqlParameter = GetJibanDataHinsituSqlCmnParams(keyRec, sql)

        Dim sqlSb As New StringBuilder()
        sqlSb.AppendLine(" SELECT ")
        sqlSb.AppendLine("    j.kbn")
        sqlSb.AppendLine("    , j.hosyousyo_no ")
        sqlSb.AppendLine("    , h.haki_syubetu As data_haki_syubetu ")
        sqlSb.AppendLine("    , j.sesyu_mei ")
        sqlSb.AppendLine("    , j.bukken_jyuusyo1 ")
        sqlSb.AppendLine("    , j.bukken_jyuusyo2 ")
        sqlSb.AppendLine("    , j.bukken_jyuusyo3 ")
        sqlSb.AppendLine("    , j.kameiten_cd ")
        sqlSb.AppendLine("    , k.torikesi AS kt_torikesi ")
        sqlSb.AppendLine("    , km.meisyou AS kt_torikesi_riyuu ")
        sqlSb.AppendLine("    , k.kameiten_mei1 ")
        sqlSb.AppendLine("    , ri.hak_irai_time ")
        sqlSb.AppendLine("    , j.hosyousyo_hak_iraisyo_tyk_date ")
        sqlSb.AppendLine("    , j.hosyousyo_hak_date ")
        sqlSb.AppendLine("    , j.hosyou_kaisi_date ")
        sqlSb.AppendLine("    , ksm1.ks_siyou As ks_siyou_1 ")
        sqlSb.AppendLine("    , ksm2.ks_siyou As ks_siyou_2 ")
        sqlSb.AppendLine("    , kss.ks_siyou_setuzokusi ")
        sqlSb.AppendLine("    , j.koj_hkks_juri_date ")
        sqlSb.AppendLine("    , j.hosyou_nasi_riyuu ")
        sqlSb.AppendLine("    , jmm.DisplayName ")
        sqlSb.AppendLine("    , j.hosyousyo_hak_houhou AS hosyousyo_hak_umu")
        sqlSb.AppendLine("    , k.hosyou_kikan As hosyou_kikan_MK ")
        sqlSb.AppendLine("    , j.hosyou_kikan As hosyou_kikan_TJ ")
        sqlSb.AppendLine("    , j.irai_date ")
        sqlSb.AppendLine("    , j.bikou ")
        '�ȉ��`�F�b�N���Z�b�g�p
        sqlSb.AppendLine("    , j.upd_datetime ")
        sqlSb.AppendLine("    , ktm.tyuuijikou_syubetu ")
        sqlSb.AppendLine("    , ri.hak_irai_hw_date ")
        sqlSb.AppendLine("    , j.hosyousyo_saihak_date ")
        sqlSb.AppendLine("    , ri.hak_irai_bkn_name ")
        sqlSb.AppendLine("    , ri.hak_irai_bkn_adr1 ")
        sqlSb.AppendLine("    , ri.hak_irai_bkn_adr2 ")
        sqlSb.AppendLine("    , ri.hak_irai_bkn_adr3 ")
        sqlSb.AppendLine("    , ri.keiyu ")
        sqlSb.AppendLine("    , tk.hosyousyo_no AS hosyousyo_no_TK ")
        sqlSb.AppendLine("    , hkt.bukken_jyky ")
        sqlSb.AppendLine("    , tk.bikou As bikou_TK ")
        sqlSb.AppendLine("    , j.hosyousyo_hak_iraisyo_umu ")
        sqlSb.AppendLine("    , ri.hak_irai_uke_datetime ")
        sqlSb.AppendLine("    , ri.hak_irai_can_datetime ")

        sqlSb.AppendLine(sql)

        '�f�[�^�擾���ԋp
        Return cmnDtAcc.getDataTable(sqlSb.ToString, cmdParams)
    End Function

    ''' <summary>
    ''' �i���ۏ؏��󋵌����f�[�^[CSV�o�͗p]���擾���܂�
    ''' </summary>
    ''' <param name="keyRec">�i��Key���R�[�h</param>
    ''' <returns>�f�[�^�e�[�u��</returns>
    ''' <remarks></remarks>
    Public Function GetJibanDataHinsituCsv(ByVal keyRec As HinsituHosyousyoJyoukyouRecord) As DataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetJibanDataHinsituCsv", keyRec)

        'SQL���̐���
        Dim sql As String = String.Empty
        Dim cmdParams() As SqlParameter = GetJibanDataHinsituSqlCmnParams(keyRec, sql)

        Dim sqlSb As New StringBuilder
        sqlSb.AppendLine(" SELECT ")
        sqlSb.AppendLine("     CONVERT(VARCHAR,j.data_haki_syubetu) + '�F' + h.haki_syubetu '�j�����' ")
        sqlSb.AppendLine("    , j.kbn '�敪' ")
        sqlSb.AppendLine("    , j.hosyousyo_no '�ԍ�' ")
        sqlSb.AppendLine("    , j.sesyu_mei '�{�喼' ")
        sqlSb.AppendLine("    , j.bukken_jyuusyo1 + j.bukken_jyuusyo2 + j.bukken_jyuusyo3 '�����Z��' ")
        sqlSb.AppendLine("    , j.kameiten_cd '�����X�R�[�h' ")
        sqlSb.AppendLine("    , CONVERT(VARCHAR,k.torikesi) + '�F' + km.meisyou '�����X���' ")
        sqlSb.AppendLine("    , k.kameiten_mei1 '�����X��' ")
        sqlSb.AppendLine("    , CONVERT(VARCHAR, ri.hak_irai_time, 111) '�˗�����' ")
        sqlSb.AppendLine("    , CONVERT(VARCHAR, j.hosyousyo_hak_iraisyo_tyk_date, 111) '�˗�������' ")
        sqlSb.AppendLine("    , CONVERT(VARCHAR, j.hosyousyo_hak_date, 111) '���s��' ")
        sqlSb.AppendLine("    , CONVERT(VARCHAR, j.hosyou_kaisi_date, 111) '�ۏ؊J�n��' ")
        sqlSb.AppendLine("    , ksm1.ks_siyou + kss.ks_siyou_setuzokusi + ksm2.ks_siyou '����' ")
        sqlSb.AppendLine("    , CONVERT(VARCHAR, j.koj_hkks_juri_date, 111) '�H��󗝓�' ")
        sqlSb.AppendLine("    , j.hosyou_nasi_riyuu '�ۏ؂Ȃ����R' ")
        sqlSb.AppendLine("    , jmm.DisplayName '�c�ƒS����' ")
        sqlSb.AppendLine("    , CASE WHEN j.hosyousyo_hak_houhou = '0' THEN '�˗���' WHEN j.hosyousyo_hak_houhou = '1' THEN '�������s' WHEN j.hosyousyo_hak_houhou = '2' THEN '�n�Ճ��[��' ELSE '' END '���񔭍s���@' ")
        sqlSb.AppendLine("    , k.hosyou_kikan '���^�ۏ؊���' ")
        sqlSb.AppendLine("    , j.hosyou_kikan '���^�ۏ؊���' ")
        sqlSb.AppendLine("    , CONVERT(VARCHAR, j.irai_date, 111) '�����˗���' ")
        sqlSb.AppendLine("    , j.bikou '���l' ")
        sqlSb.AppendLine(sql)

        '�f�[�^�擾���ԋp
        Return cmnDtAcc.getDataTable(sqlSb.ToString, cmdParams)
    End Function

    ''' <summary>
    ''' �i���ۏ؏��󋵌����f�[�^�擾�p�̋���SQL�N�G���E�p�����[�^���擾
    ''' </summary>
    ''' <param name="keyRec">�i��Key���R�[�h</param>
    ''' <param name="sql">SQL�N�G��</param>
    ''' <returns>���ʕ�����SQL</returns>
    ''' <remarks></remarks>
    Private Function GetJibanDataHinsituSqlCmnParams(ByVal keyRec As HinsituHosyousyoJyoukyouRecord, _
                                                     ByRef sql As String) As SqlParameter()
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetJibanDataHinsituSqlCmnParams", keyRec, sql)

        '�p�����[�^�̐���
        Dim listParams As New List(Of ParamRecord)
        'SQL���̐���
        Dim sqlCondition As New StringBuilder

        Dim sqlSb As New StringBuilder()
        sqlSb.AppendLine("FROM")
        sqlSb.AppendLine("    jhs_sys.t_jiban j WITH (READCOMMITTED) ")
        '�����XM
        sqlSb.AppendLine("    LEFT OUTER JOIN jhs_sys.m_kameiten k WITH (READCOMMITTED) ")
        sqlSb.AppendLine("        ON j.kameiten_cd = k.kameiten_cd ")
        '�g������M
        sqlSb.AppendLine("    LEFT OUTER JOIN jhs_sys.m_kakutyou_meisyou AS km WITH (READCOMMITTED) ")
        sqlSb.AppendLine("        ON km.meisyou_syubetu = 56 ")
        sqlSb.AppendLine("        AND km.code = CAST(k.torikesi AS VARCHAR(10)) ")
        '�f�[�^�j��M
        sqlSb.AppendLine("    LEFT OUTER JOIN jhs_sys.m_data_haki h WITH (READCOMMITTED) ")
        sqlSb.AppendLine("        ON j.data_haki_syubetu = h.data_haki_no ")
        '�Ј��A�J�E���g���M
        sqlSb.AppendLine("    LEFT OUTER JOIN jhs_sys.m_jhs_mailbox jmm WITH (READCOMMITTED) ")
        sqlSb.AppendLine("        ON k.eigyou_tantousya_mei = jmm.PrimaryWindowsNTAccount ")
        '�S����M
        sqlSb.AppendLine("    LEFT OUTER JOIN jhs_sys.m_tantousya ttmi WITH (READCOMMITTED) ")
        sqlSb.AppendLine("        ON j.tantousya_cd = ttmi.tantousya_cd ")
        '�������M
        sqlSb.AppendLine("    LEFT OUTER JOIN jhs_sys.m_tyousakaisya tm WITH (READCOMMITTED) ")
        sqlSb.AppendLine("        ON j.tys_kaisya_cd = tm.tys_kaisya_cd ")
        sqlSb.AppendLine("        AND j.tys_kaisya_jigyousyo_cd = tm.jigyousyo_cd ")
        '��b�d�lM1
        sqlSb.AppendLine("    LEFT OUTER JOIN jhs_sys.m_ks_siyou ksm1 WITH (READCOMMITTED) ")
        sqlSb.AppendLine("        ON j.hantei_cd1 = ksm1.ks_siyou_no ")
        '��b�d�lM2
        sqlSb.AppendLine("    LEFT OUTER JOIN jhs_sys.m_ks_siyou ksm2 WITH (READCOMMITTED) ")
        sqlSb.AppendLine("        ON j.hantei_cd2 = ksm2.ks_siyou_no ")
        '��b�d�l�ڑ���M
        sqlSb.AppendLine("    LEFT OUTER JOIN jhs_sys.m_ks_siyou_setuzokusi kss WITH (READCOMMITTED) ")
        sqlSb.AppendLine("        ON j.hantei_setuzoku_moji = kss.ks_siyou_setuzokusi_no ")
        '�i��
        sqlSb.AppendLine("    LEFT OUTER JOIN ReportIF ri WITH (READCOMMITTED) ")
        sqlSb.AppendLine("        ON j.kbn + j.hosyousyo_no = ri.kokyaku_no ")
        sqlSb.AppendLine("        AND ri.kokyaku_brc = '0'                  ")
        '�ۏ؏��Ǘ�T
        sqlSb.AppendLine("    LEFT OUTER JOIN jhs_sys.t_hosyousyo_kanri hkt WITH (READCOMMITTED) ")
        sqlSb.AppendLine("        ON j.kbn = hkt.kbn ")
        sqlSb.AppendLine("        AND j.hosyousyo_no = hkt.hosyousyo_no ")
        '�ȉ��`�F�b�N���Z�b�g�p
        '�����X���ӎ����ݒ�M
        sqlSb.AppendLine("        LEFT OUTER JOIN (")
        sqlSb.AppendLine("         SELECT mkt.kameiten_cd, mkt.tyuuijikou_syubetu")
        sqlSb.AppendLine("              , MAX(mkt.nyuuryoku_no) max_nyuuryoku_no ")
        sqlSb.AppendLine("           FROM m_kameiten_tyuuijikou mkt WITH (READCOMMITTED)  ")
        sqlSb.AppendLine("        GROUP BY mkt.kameiten_cd, mkt.tyuuijikou_syubetu ")
        sqlSb.AppendLine("        ) AS ktm ")
        sqlSb.AppendLine("        ON j.kameiten_cd = ktm.kameiten_cd")
        sqlSb.AppendLine("       AND ktm.tyuuijikou_syubetu = '55'  ")
        '�@�ʐ���T
        sqlSb.AppendLine("    LEFT OUTER JOIN jhs_sys.t_teibetu_seikyuu AS tk WITH (READCOMMITTED) ")
        sqlSb.AppendLine("        ON j.kbn = tk.kbn ")
        sqlSb.AppendLine("        AND j.hosyousyo_no = tk.hosyousyo_no ")
        sqlSb.AppendLine("        AND tk.bunrui_cd = '170' ")

        sqlSb.AppendLine(" WHERE ")
        sqlSb.AppendLine("    0 = 0 ")
        sqlSb.AppendLine("    {0} ")
        sqlSb.AppendLine(" ORDER BY ")
        sqlSb.AppendLine("    j.kbn ")
        sqlSb.AppendLine("    , j.hosyousyo_no ")
        sqlSb.AppendLine("")

        '***********************************************************************
        ' �敪�����̐ݒ�
        '***********************************************************************
        '�敪����SQL�i�[�p
        Dim tmpKbnNoJouken As String = String.Empty
        '�敪�w������i�[���X�g
        Dim listKbn As New List(Of String)

        ' �敪_1�̔���
        If IIf(keyRec.Kbn1 Is Nothing, String.Empty, keyRec.Kbn1) IsNot String.Empty Then
            listKbn.Add(keyRec.Kbn1)
        End If

        'SQL������
        tmpKbnNoJouken &= (" AND ")
        tmpKbnNoJouken &= (" j.kbn IN ( ")

        If listKbn.Count = 0 Then
            '�������ݒ肳��Ă��Ȃ��ꍇ�A�S�敪��Ώ�
            tmpKbnNoJouken &= (" SELECT kbn FROM m_data_kbn ")
        ElseIf listKbn.Count >= 1 Then
            Dim strParamKbn As String = "@KUBUN"
            Dim ki As Integer = 0
            tmpKbnNoJouken &= (strParamKbn)
            listParams.Add(getParamRecord(strParamKbn, SqlDbType.Char, 1, listKbn(ki)))
        End If

        tmpKbnNoJouken &= (" ) ")

        '***********************************************************************
        ' �ۏ؏��͈͏����̐ݒ�
        '***********************************************************************
        ' ��ł��ݒ肳��Ă���ꍇ�A�������쐬
        If Not IIf(keyRec.HosyousyoNoFrom Is Nothing, String.Empty, keyRec.HosyousyoNoFrom) Is String.Empty Or _
           Not IIf(keyRec.HosyousyoNoTo Is Nothing, String.Empty, keyRec.HosyousyoNoTo) Is String.Empty Then

            tmpKbnNoJouken &= (" AND ")

            If Not keyRec.HosyousyoNoFrom Is String.Empty And _
               Not keyRec.HosyousyoNoTo Is String.Empty Then
                ' �����w��L���BETWEEN
                tmpKbnNoJouken &= (" j.hosyousyo_no BETWEEN @HOSYOUSYONOFROM AND @HOSYOUSYONOTO")
                listParams.Add(getParamRecord("@HOSYOUSYONOFROM", SqlDbType.VarChar, 10, keyRec.HosyousyoNoFrom))
                listParams.Add(getParamRecord("@HOSYOUSYONOTO", SqlDbType.VarChar, 10, keyRec.HosyousyoNoTo))
            Else
                If Not keyRec.HosyousyoNoFrom Is String.Empty Then
                    ' �ۏ؏�From�̂�
                    tmpKbnNoJouken &= (" j.hosyousyo_no >= @HOSYOUSYONOFROM ")
                    listParams.Add(getParamRecord("@HOSYOUSYONOFROM", SqlDbType.VarChar, 10, keyRec.HosyousyoNoFrom))
                Else
                    ' �ۏ؏�To�̂�
                    tmpKbnNoJouken &= (" j.hosyousyo_no <= @HOSYOUSYONOTO ")
                    listParams.Add(getParamRecord("@HOSYOUSYONOTO", SqlDbType.VarChar, 10, keyRec.HosyousyoNoTo))
                End If
            End If

        End If

        '�敪�Ɣԍ���WHERE�������Z�b�g
        sqlCondition.Append(tmpKbnNoJouken)

        '***********************************************************************
        ' �_��NO
        '***********************************************************************
        ' �_��NO�̔���
        If keyRec.KeiyakuNo IsNot Nothing AndAlso keyRec.KeiyakuNo <> String.Empty Then

            sqlCondition.Append(" AND ")

            listParams.Add(getParamRecord("@KEIYAKUNO", SqlDbType.VarChar, 20, keyRec.KeiyakuNo & "%"))
            sqlCondition.Append(" ISNULL(j.keiyaku_no, 0) like @KEIYAKUNO")

        End If

        '***********************************************************************
        ' �{�喼�̐ݒ�
        '***********************************************************************
        ' �{�喼�̔���
        If Not IIf(keyRec.SesyuMei Is Nothing, String.Empty, keyRec.SesyuMei) Is String.Empty Then

            sqlCondition.Append(" AND ")

            listParams.Add(getParamRecord("@SESYUMEI", SqlDbType.VarChar, 50 + 1, keyRec.SesyuMei & "%"))
            sqlCondition.Append(" REPLACE(j.sesyu_mei, ' ', '') like REPLACE (@SESYUMEI, ' ', '') ")
        End If

        '***********************************************************************
        ' �����Z��1+2�̐ݒ�
        '***********************************************************************
        ' �����Z��1+2�̔���
        If Not IIf(keyRec.BukkenJyuusyo12 Is Nothing, String.Empty, keyRec.BukkenJyuusyo12) Is String.Empty Then

            sqlCondition.Append(" AND ")

            listParams.Add(getParamRecord("@BUKKENJYUUSYO12", SqlDbType.VarChar, 64 + 1, keyRec.BukkenJyuusyo12 & "%"))
            sqlCondition.Append(" isnull(j.bukken_jyuusyo1, '') + isnull(j.bukken_jyuusyo2, '') + isnull(j.bukken_jyuusyo3, '') like @BUKKENJYUUSYO12 ")
        End If

        '***********************************************************************
        ' ���l�̐ݒ�
        '***********************************************************************
        ' ���l�̔���
        If Not IIf(keyRec.Bikou Is Nothing, String.Empty, keyRec.Bikou) Is String.Empty Then

            sqlCondition.Append(" AND ")

            listParams.Add(getParamRecord("@BIKOU", SqlDbType.VarChar, 256 + 1, keyRec.Bikou & "%"))
            sqlCondition.Append(" j.bikou like @BIKOU ")
        End If

        '***********************************************************************
        ' ������ЃR�[�h�̐ݒ�
        '***********************************************************************
        ' ������ЃR�[�h�̔���
        If Not IIf(keyRec.TysKaisyaCd Is Nothing, String.Empty, keyRec.TysKaisyaCd) Is String.Empty Then

            sqlCondition.Append(" AND ")

            listParams.Add(getParamRecord("@TYSKAISYACD", SqlDbType.VarChar, 7, keyRec.TysKaisyaCd))
            sqlCondition.Append(" j.tys_kaisya_cd + j.tys_kaisya_jigyousyo_cd = @TYSKAISYACD ")
        End If

        '***********************************************************************
        ' ���s�i���󋵂̐ݒ�
        '***********************************************************************
        'SQL�i�[�p
        Dim tmpHakkouStatusSql As String = String.Empty

        ' ���s�i����1�̔���
        If keyRec.HakkouStatus1 = 1 Then
            tmpHakkouStatusSql &= (" AND ( ")
            tmpHakkouStatusSql &= (" ( hkt.bukken_jyky = '0' ) ")
        End If

        ' ���s�i����2�̔���i���s�s�j
        If keyRec.HakkouStatus2 = 1 Then
            If tmpHakkouStatusSql = String.Empty Then
                tmpHakkouStatusSql &= (" AND ( ")
            Else
                tmpHakkouStatusSql &= (" OR ")
            End If
            tmpHakkouStatusSql &= (" ( hkt.bukken_jyky = '2' ) ")
        End If

        ' ���s�i����3�̔���i���s���˗��j
        If keyRec.HakkouStatus3 = 1 Then
            If tmpHakkouStatusSql = String.Empty Then
                tmpHakkouStatusSql &= (" AND ( ")
            Else
                tmpHakkouStatusSql &= (" OR ")
            End If
            tmpHakkouStatusSql &= (" ( hkt.bukken_jyky = '1' ")
            tmpHakkouStatusSql &= (" AND ISNULL(j.hosyousyo_hak_iraisyo_umu, 0) = 0 ")
            tmpHakkouStatusSql &= (" AND ( ISNULL(ri.hak_irai_time, '') = '' OR ISNULL(ri.hak_irai_time, '') < ISNULL(ri.hak_irai_can_datetime, '') )) ")
        End If

        ' ���s�i����4�̔���i���[���˗��i�Ĕ��s�܁j�ρE����t�j
        If keyRec.HakkouStatus4 = 1 Then
            If tmpHakkouStatusSql = String.Empty Then
                tmpHakkouStatusSql &= (" AND ( ")
            Else
                tmpHakkouStatusSql &= (" OR ")
            End If
            tmpHakkouStatusSql &= (" ( ISNULL(ri.hak_irai_time, '') <> '' ")
            tmpHakkouStatusSql &= (" AND ( ISNULL(ri.hak_irai_uke_datetime, '') = '' OR ISNULL(ri.hak_irai_time, '') > ISNULL(ri.hak_irai_uke_datetime, '') ) ")
            tmpHakkouStatusSql &= (" AND ( ISNULL(ri.hak_irai_can_datetime, '') = '' OR ISNULL(ri.hak_irai_time, '') > ISNULL(ri.hak_irai_can_datetime, '') )) ")
        End If

        ' ���s�i����5�̔���i���[���˗��i�Ĕ��s�܁j�����ρj
        If keyRec.HakkouStatus5 = 1 Then
            If tmpHakkouStatusSql = String.Empty Then
                tmpHakkouStatusSql &= (" AND ( ")
            Else
                tmpHakkouStatusSql &= (" OR ")
            End If
            tmpHakkouStatusSql &= (" ( ISNULL(ri.hak_irai_time, '') < ISNULL(ri.hak_irai_uke_datetime, '') OR ISNULL(ri.hak_irai_time, '') < ISNULL(ri.hak_irai_can_datetime, '') ) ")
        End If

        ' ���s�i����6�̔���i���񔭍s�ρj
        If keyRec.HakkouStatus6 = 1 Then
            If tmpHakkouStatusSql = String.Empty Then
                tmpHakkouStatusSql &= (" AND ( ")
            Else
                tmpHakkouStatusSql &= (" OR ")
            End If
            tmpHakkouStatusSql &= (" ( hkt.bukken_jyky = '3' ) ")
        End If

        ' �I���̊���
        If tmpHakkouStatusSql <> String.Empty Then
            tmpHakkouStatusSql &= (" ) ")
        End If

        '���s�i���󋵂�WHERE�������Z�b�g
        sqlCondition.Append(tmpHakkouStatusSql)

        '***********************************************************************
        ' �����X�R�[�h�̐ݒ�
        '***********************************************************************
        ' �����X�R�[�h�̔���
        If Not IIf(keyRec.KameitenCd Is Nothing, String.Empty, keyRec.KameitenCd) Is String.Empty Then

            sqlCondition.Append(" AND ")

            listParams.Add(getParamRecord("@KAMEITENCD", SqlDbType.VarChar, 5, keyRec.KameitenCd))
            sqlCondition.Append(" j.kameiten_cd = @KAMEITENCD ")
        End If

        '***********************************************************************
        ' �����X�J�i�P�̐ݒ�
        '***********************************************************************
        ' �����X�J�i�P�̔���
        If Not IIf(keyRec.TenmeiKana1 Is Nothing, String.Empty, keyRec.TenmeiKana1) Is String.Empty Then

            sqlCondition.Append(" AND ")

            listParams.Add(getParamRecord("@TENMEIKANA1", SqlDbType.VarChar, 20, keyRec.TenmeiKana1 & "%"))
            sqlCondition.Append(" (k.tenmei_kana1 like @TENMEIKANA1 OR k.tenmei_kana2 like @TENMEIKANA1)")
        End If

        '***********************************************************************
        ' ���񔭍s���@�i���s�^�C�~���O�j�̐ݒ�
        '***********************************************************************
        ' 
        If keyRec.HakkouTiming = 0 Then
            ' �˗����I��
            sqlCondition.Append(" AND ")
            sqlCondition.Append(" j.hosyousyo_hak_houhou = 0 ")
        ElseIf keyRec.HakkouTiming = 1 Then
            ' �������s�I��
            sqlCondition.Append(" AND ")
            sqlCondition.Append(" j.hosyousyo_hak_houhou = 1 ")
        ElseIf keyRec.HakkouTiming = 2 Then
            ' �n�Ճ��[���I��
            sqlCondition.Append(" AND ")
            sqlCondition.Append(" j.hosyousyo_hak_houhou = 2 ")
        End If

        '***********************************************************************
        ' �˗��������͈͏����̐ݒ�
        '***********************************************************************
        ' ��`�F�b�N��
        If keyRec.HosyousyoHakkouIraisyoTyakuDateChk = 1 Then
            sqlCondition.Append(" AND ")
            sqlCondition.Append(" j.hosyousyo_hak_iraisyo_tyk_date IS NULL ")
        Else
            '�͈͐ݒ�
            createDateHanniSql(sqlCondition, listParams, _
                   keyRec.HosyousyoHakkouIraisyoTyakuDateFrom, keyRec.HosyousyoHakkouIraisyoTyakuDateTo, _
                   "j.hosyousyo_hak_iraisyo_tyk_date", "@HOSYOUSYOHAKKOUIRAISYOTYAKUDATE")
        End If

        '***********************************************************************
        ' ���s���͈͏����̐ݒ�
        '***********************************************************************
        ' ��`�F�b�N��
        If keyRec.HosyousyoHakkouDateChk = 1 Then
            sqlCondition.Append(" AND ")
            sqlCondition.Append(" j.hosyousyo_hak_date IS NULL ")
        Else
            '�͈͐ݒ�
            createDateHanniSql(sqlCondition, listParams, _
                   keyRec.HosyousyoHakkouDateFrom, keyRec.HosyousyoHakkouDateTo, _
                   "j.hosyousyo_hak_date", "@HOSYOUSYOHAKKOUDATE")
        End If

        '***********************************************************************
        ' �Ĕ��s���͈͏����̐ݒ�
        '***********************************************************************
        createDateHanniSql(sqlCondition, listParams, _
                           keyRec.HosyousyoSaihakDateFrom, keyRec.HosyousyoSaihakDateTo, _
                           "j.hosyousyo_saihak_date", "@HOSYOUSYOSAIHAKKOUDATE")

        '***********************************************************************
        ' ���s�˗����͈͏����̐ݒ�
        '***********************************************************************
        ' ��`�F�b�N��
        If keyRec.HakIraiTimeChk = 1 Then
            sqlCondition.Append(" AND ")
            sqlCondition.Append(" ri.hak_irai_time IS NULL ")
        Else
            '�͈͐ݒ�
            createDateHanniSql(sqlCondition, listParams, _
                   keyRec.HakIraiTimeFrom, keyRec.HakIraiTimeTo, _
                   "ri.hak_irai_time", "@HAKKOUIRAITIME")
        End If

        '***********************************************************************
        ' �����˗����͈͏����̐ݒ�
        '***********************************************************************
        createDateHanniSql(sqlCondition, listParams, _
                           keyRec.IraiDateFrom, keyRec.IraiDateTo, _
                           "j.irai_date", "@IRAIDATE")

        '***********************************************************************
        ' �ۏ؊���
        '***********************************************************************
        ' �ۏ؊��Ԃ̔���
        If keyRec.HosyouKikanMK <> Integer.MinValue OrElse keyRec.HosyouKikanTJ <> Integer.MinValue Then
            '�����X�E�������͎�
            If keyRec.HosyouKikanMK <> Integer.MinValue AndAlso keyRec.HosyouKikanTJ <> Integer.MinValue Then
                sqlCondition.Append(" AND ")

                listParams.Add(getParamRecord("@HOSYOUKIKANMK", SqlDbType.Int, 10, keyRec.HosyouKikanMK))
                listParams.Add(getParamRecord("@HOSYOUKIKANTJ", SqlDbType.Int, 10, keyRec.HosyouKikanTJ))
                sqlCondition.Append(" k.hosyou_kikan = @HOSYOUKIKANMK AND j.hosyou_kikan = @HOSYOUKIKANTJ ")

                ' �����X�̂ݓ��͎�
            ElseIf keyRec.HosyouKikanMK <> Integer.MinValue OrElse keyRec.HosyouKikanTJ = Integer.MinValue Then
                sqlCondition.Append(" AND ")

                listParams.Add(getParamRecord("@HOSYOUKIKANMK", SqlDbType.Int, 10, keyRec.HosyouKikanMK))
                sqlCondition.Append(" k.hosyou_kikan = @HOSYOUKIKANMK ")

                ' �����̂ݓ��͎�
            ElseIf keyRec.HosyouKikanMK = Integer.MinValue OrElse keyRec.HosyouKikanTJ <> Integer.MinValue Then
                sqlCondition.Append(" AND ")

                listParams.Add(getParamRecord("@HOSYOUKIKANTJ", SqlDbType.Int, 10, keyRec.HosyouKikanTJ))
                sqlCondition.Append(" j.hosyou_kikan = @HOSYOUKIKANTJ ")
            End If
        End If

        '***********************************************************************
        ' �f�[�^�j�����
        '***********************************************************************
        ' �f�[�^�j����ʂ̔���
        If keyRec.DataHakiSyubetu = 0 Then

            sqlCondition.Append(" AND ")

            listParams.Add(getParamRecord("@DATAHAKISYUBETU", SqlDbType.Int, 10, keyRec.DataHakiSyubetu))
            sqlCondition.Append(" ISNULL(j.data_haki_syubetu,0) = @DATAHAKISYUBETU ")

        End If

        '***********************************************************************
        ' �p�����[�^�̍쐬
        '***********************************************************************
        ' �p�����[�^�̍쐬
        Dim i As Integer
        Dim cmdParams(listParams.Count - 1) As SqlParameter
        For i = 0 To listParams.Count - 1
            Dim rec As ParamRecord = listParams(i)
            ' �K�v�ȏ����Z�b�g
            cmdParams(i) = SQLHelper.MakeParam(rec.Param, rec.DbType, rec.ParamLength, rec.SetData)
        Next

        ' �ŏI�I��SQL���̐���
        sql = String.Format(sqlSb.ToString, sqlCondition.ToString)

        Return cmdParams
    End Function

#End Region

#Region "�@�ʐ����f�[�^�̎擾"
    ''' <summary>
    ''' �@�ʐ������R�[�h���擾���܂��i�敪�E�ۏ؏�NO�P�ʂɑS�āj
    ''' </summary>
    ''' <param name="kbn">�敪</param>
    ''' <param name="hosyousyoNo">�ۏ؏�NO</param>
    ''' <param name="bunruiCd">���ރR�[�h�i�C�Ӂj</param>
    ''' <returns>�@�ʐ����f�[�^�e�[�u��</returns>
    ''' <remarks></remarks>
    Public Function GetTeibetuSeikyuuData(ByVal kbn As String, _
                                          ByVal hosyousyoNo As String, _
                                          Optional ByVal bunruiCd As String = "") As DataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTeibetuSeikyuuData", _
                                                    kbn, _
                                                    hosyousyoNo, _
                                                    bunruiCd)

        ' �p�����[�^
        Const strParamKbn As String = "@KBN"
        Const strParamHosyousyoNo As String = "@HOSYOUSYONO"
        Const strParamBunruiCd As String = "@BUNRUICD"

        Dim sqlSb As New StringBuilder()
        sqlSb.AppendLine("SELECT")
        sqlSb.AppendLine("    t.kbn")
        sqlSb.AppendLine("    , t.hosyousyo_no")
        sqlSb.AppendLine("    , t.bunrui_cd")
        sqlSb.AppendLine("    , t.gamen_hyouji_no")
        sqlSb.AppendLine("    , t.syouhin_cd")
        sqlSb.AppendLine("    , s.syouhin_mei")
        sqlSb.AppendLine("    , t.uri_gaku")
        sqlSb.AppendLine("    , t.siire_gaku")
        sqlSb.AppendLine("    , t.zei_kbn")
        sqlSb.AppendLine("    , t.syouhizei_gaku")
        sqlSb.AppendLine("    , t.siire_syouhizei_gaku")
        sqlSb.AppendLine("    , z.zeiritu")
        sqlSb.AppendLine("    , t.seikyuusyo_hak_date")
        sqlSb.AppendLine("    , t.uri_date")
        sqlSb.AppendLine("    , t.denpyou_uri_date")
        sqlSb.AppendLine("    , t.denpyou_siire_date")
        sqlSb.AppendLine("    , t.seikyuu_umu")
        sqlSb.AppendLine("    , t.kakutei_kbn")
        sqlSb.AppendLine("    , t.uri_keijyou_flg")
        sqlSb.AppendLine("    , t.uri_keijyou_date")
        sqlSb.AppendLine("    , t.bikou")
        sqlSb.AppendLine("    , t.koumuten_seikyuu_gaku")
        sqlSb.AppendLine("    , t.hattyuusyo_gaku")
        sqlSb.AppendLine("    , t.hattyuusyo_kakunin_date")
        sqlSb.AppendLine("    , t.ikkatu_nyuukin_flg")
        sqlSb.AppendLine("    , t.tys_mitsyo_sakusei_date")
        sqlSb.AppendLine("    , t.hattyuusyo_kakutei_flg")
        sqlSb.AppendLine("    , t.seikyuu_saki_cd")
        sqlSb.AppendLine("    , t.seikyuu_saki_brc")
        sqlSb.AppendLine("    , t.seikyuu_saki_kbn")
        sqlSb.AppendLine("    , t.tys_kaisya_cd")
        sqlSb.AppendLine("    , t.tys_kaisya_jigyousyo_cd")
        sqlSb.AppendLine("    , t.add_login_user_id")
        sqlSb.AppendLine("    , t.add_datetime")
        sqlSb.AppendLine("    , ISNULL(t.upd_login_user_id,t.add_login_user_id) AS upd_login_user_id")
        sqlSb.AppendLine("    , ISNULL(t.upd_datetime,t.add_datetime) AS upd_datetime ")
        sqlSb.AppendLine("FROM")
        sqlSb.AppendLine("    jhs_sys.t_teibetu_seikyuu t WITH (READCOMMITTED) ")
        sqlSb.AppendLine("    LEFT OUTER JOIN jhs_sys.m_syouhin s WITH (READCOMMITTED) ")
        sqlSb.AppendLine("        ON s.syouhin_cd = t.syouhin_cd ")
        sqlSb.AppendLine("    LEFT OUTER JOIN jhs_sys.m_syouhizei z WITH (READCOMMITTED) ")
        sqlSb.AppendLine("        ON z.zei_kbn = t.zei_kbn ")
        sqlSb.AppendLine("WHERE")
        sqlSb.AppendLine("    t.kbn = " & strParamKbn)
        sqlSb.AppendLine("    AND t.hosyousyo_no = " & strParamHosyousyoNo)
        ' ���ރR�[�h�������ɑ��݂���ꍇ�A�����ɒǉ�����
        If bunruiCd <> "" Then
            sqlSb.AppendLine("    AND t.bunrui_cd = " & strParamBunruiCd)
        End If
        sqlSb.AppendLine("ORDER BY")
        sqlSb.AppendLine("    t.bunrui_cd")
        sqlSb.AppendLine("    , t.gamen_hyouji_no")
        sqlSb.AppendLine("")

        ' �p�����[�^�֐ݒ�
        Dim commandParameters() As SqlParameter = _
                    {SQLHelper.MakeParam(strParamKbn, SqlDbType.Char, 1, kbn), _
                     SQLHelper.MakeParam(strParamHosyousyoNo, SqlDbType.VarChar, 10, hosyousyoNo), _
                     SQLHelper.MakeParam(strParamBunruiCd, SqlDbType.VarChar, 3, bunruiCd)}

        '�f�[�^�擾���ԋp
        Return cmnDtAcc.getDataTable(sqlSb.ToString, commandParameters)

    End Function

    ''' <summary>
    ''' �@�ʐ������R�[�h���擾���܂�(�P���̂ݎ擾)
    ''' </summary>
    ''' <param name="kbn">�敪</param>
    ''' <param name="hosyousyoNo">�ۏ؏�NO</param>
    ''' <param name="bunruiCd">���ރR�[�h</param>
    ''' <param name="gamenHyoujiNo">��ʕ\��NO</param>
    ''' <returns>�@�ʐ����f�[�^�e�[�u��</returns>
    ''' <remarks></remarks>
    Public Function GetTeibetuSeikyuuData(ByVal kbn As String, _
                                          ByVal hosyousyoNo As String, _
                                          ByVal bunruiCd As String, _
                                          ByVal gamenHyoujiNo As Integer) As DataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTeibetuSeikyuuData", _
                                            kbn, _
                                            hosyousyoNo, _
                                            bunruiCd, _
                                            gamenHyoujiNo)

        ' �p�����[�^
        Const strParamKbn As String = "@KBN"
        Const strParamHosyousyoNo As String = "@HOSYOUSYONO"
        Const strParamBunruiCd As String = "@BUNRUICD"
        Const strParamGamenHyoujiNo As String = "@GAMENHYOUJINO"

        Dim sqlSb As New StringBuilder()
        sqlSb.AppendLine("SELECT")
        sqlSb.AppendLine("    * ")
        sqlSb.AppendLine("FROM")
        sqlSb.AppendLine("    jhs_sys.t_teibetu_seikyuu ")
        sqlSb.AppendLine("WHERE")
        sqlSb.AppendLine("    kbn = " & strParamKbn)
        sqlSb.AppendLine("    AND hosyousyo_no = " & strParamHosyousyoNo)
        If bunruiCd = "110" Or bunruiCd = "115" Then
            sqlSb.AppendLine("    AND bunrui_cd IN ('110','115') ")
        Else
            sqlSb.AppendLine("    AND bunrui_cd = " & strParamBunruiCd)
        End If
        sqlSb.AppendLine("    AND gamen_hyouji_no =" & strParamGamenHyoujiNo)
        sqlSb.AppendLine("")

        ' �p�����[�^�֐ݒ�
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamKbn, SqlDbType.Char, 1, kbn), _
             SQLHelper.MakeParam(strParamHosyousyoNo, SqlDbType.VarChar, 10, hosyousyoNo), _
             SQLHelper.MakeParam(strParamBunruiCd, SqlDbType.VarChar, 3, bunruiCd), _
             SQLHelper.MakeParam(strParamGamenHyoujiNo, SqlDbType.Int, 1, gamenHyoujiNo)}

        '�f�[�^�擾���ԋp
        Return cmnDtAcc.getDataTable(sqlSb.ToString, commandParameters)

    End Function

    ''' <summary>
    ''' �@�ʐ������R�[�h��Key���擾���܂��i�敪�E�ۏ؏�NO�P�ʂɑS�āj
    ''' </summary>
    ''' <param name="kbn">�敪</param>
    ''' <param name="hosyousyoNo">�ۏ؏�NO</param>
    ''' <returns>�@�ʐ����f�[�^�e�[�u��</returns>
    ''' <remarks></remarks>
    Public Function GetTeibetuSeikyuuDataKey(ByVal kbn As String, _
                                             ByVal hosyousyoNo As String) As JibanDataSet.TeibetuSeikyuuKeyTableDataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTeibetuSeikyuuDataKey", _
                                                    kbn, _
                                                    hosyousyoNo)

        ' �p�����[�^
        Const strParamKbn As String = "@KBN"
        Const strParamHosyousyoNo As String = "@HOSYOUSYONO"

        Dim commandTextSb As New StringBuilder()
        commandTextSb.Append(" SELECT " & _
                                    "   t.kbn, " & _
                                    "   t.hosyousyo_no, " & _
                                    "   t.bunrui_cd, " & _
                                    "   t.gamen_hyouji_no " & _
                                    " FROM " & _
                                    "    t_teibetu_seikyuu t WITH (UPDLOCK) " & _
                                    " WHERE " & _
                                    "    t.kbn = " & strParamKbn & _
                                    " AND " & _
                                    "    t.hosyousyo_no = " & strParamHosyousyoNo)

        ' �p�����[�^�֐ݒ�
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamKbn, SqlDbType.Char, 1, kbn), _
             SQLHelper.MakeParam(strParamHosyousyoNo, SqlDbType.VarChar, 10, hosyousyoNo)}

        commandTextSb.Append(" ORDER BY t.bunrui_cd,t.gamen_hyouji_no ")

        ' �f�[�^�̎擾
        Dim JibanDataSet As New JibanDataSet()

        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            JibanDataSet, JibanDataSet.TeibetuSeikyuuKeyTable.TableName, commandParameters)

        Dim teibetuTable As JibanDataSet.TeibetuSeikyuuKeyTableDataTable = JibanDataSet.TeibetuSeikyuuKeyTable

        Return teibetuTable

    End Function

    ''' <summary>
    ''' �@�ʐ������R�[�h�̉�ʕ\��NO�̍ő�l���擾���܂�
    ''' </summary>
    ''' <param name="kbn">�敪</param>
    ''' <param name="hosyousyoNo">�ۏ؏�NO</param>
    ''' <param name="bunruiCd">���ރR�[�h</param>
    ''' <returns>�@�ʐ����f�[�^�e�[�u��</returns>
    ''' <remarks></remarks>
    Public Function GetTeibetuSeikyuuMaxNo(ByVal kbn As String, _
                                           ByVal hosyousyoNo As String, _
                                           ByVal bunruiCd As String) As Object

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTeibetuSeikyuuMaxNo", _
                                                    kbn, _
                                                    hosyousyoNo, _
                                                    bunruiCd)

        ' �p�����[�^
        Const strParamKbn As String = "@KBN"
        Const strParamHosyousyoNo As String = "@HOSYOUSYONO"
        Const strParamBunruiCd As String = "@BUNRUICD"

        Dim commandTextSb As New StringBuilder()
        commandTextSb.Append(" SELECT " & _
                                    "   MAX(t.gamen_hyouji_no) " & _
                                    " FROM " & _
                                    "    t_teibetu_seikyuu t WITH (UPDLOCK) " & _
                                    " WHERE " & _
                                    "    t.kbn = " & strParamKbn & _
                                    " AND " & _
                                    "    t.hosyousyo_no = " & strParamHosyousyoNo & _
                                    " AND " & _
                                    "    t.bunrui_cd = " & strParamBunruiCd)

        ' �p�����[�^�֐ݒ�
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamKbn, SqlDbType.Char, 1, kbn), _
             SQLHelper.MakeParam(strParamHosyousyoNo, SqlDbType.VarChar, 10, hosyousyoNo), _
             SQLHelper.MakeParam(strParamBunruiCd, SqlDbType.VarChar, 3, bunruiCd)}

        '�f�[�^�̎擾
        Dim retValue As Object = SQLHelper.ExecuteScalar(connStr, CommandType.Text, commandTextSb.ToString(), commandParameters)

        If retValue Is DBNull.Value Then
            Return 0
        End If

        Return retValue

    End Function

    ''' <summary>
    ''' �Y���������Ɋ֘A����@�ʐ����f�[�^(���i1�`3)�̕ۏؗL�����擾���A�����ꂩ���ۏؗL�̏ꍇ1���A�ȊO�̏ꍇ0��Ԃ��B
    ''' ���f�[�^�����݂��Ȃ��ꍇ�A""(��)��Ԃ��B
    ''' </summary>
    ''' <param name="strKbn">�敪</param>
    ''' <param name="strBangou">�ԍ�</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetHosyouSyouhinUmu( _
                                    ByVal strKbn As String, _
                                    ByVal strBangou As String _
                                ) As String

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetHosyouSyouhinUmu", strKbn, strBangou)

        Dim blnRes As Boolean = False

        Dim dTblRes As New DataTable
        Dim strRetHosyouUmu As String = String.Empty

        ' �p�����[�^
        Const strPrmKbn As String = "@KBN" '�敪
        Const strPrmBangou As String = "@BANGOU" '�ԍ�

        Dim cmdTextSb As New StringBuilder

        'SQL����
        cmdTextSb.Append(" SELECT ")
        cmdTextSb.Append("      TS.kbn ")
        cmdTextSb.Append("    , TS.hosyousyo_no ")
        cmdTextSb.Append("    , MAX( ")
        cmdTextSb.Append("           CASE ")
        cmdTextSb.Append("                WHEN ISNULL(MS.hosyou_umu, '') = '' ")
        cmdTextSb.Append("                THEN '0' ")
        cmdTextSb.Append("                WHEN MS.hosyou_umu = '0' ")
        cmdTextSb.Append("                THEN '0' ")
        cmdTextSb.Append("                WHEN MS.hosyou_umu = '1' ")
        cmdTextSb.Append("                THEN '1' ")
        cmdTextSb.Append("                ELSE '0' ")
        cmdTextSb.Append("           END) AS teibetu_hosyou_syouhin_umu ")
        cmdTextSb.Append(" FROM ")
        cmdTextSb.Append("      t_teibetu_seikyuu TS ")
        cmdTextSb.Append("           INNER JOIN m_syouhin MS ")
        cmdTextSb.Append("             ON TS.syouhin_cd = MS.syouhin_cd ")
        cmdTextSb.Append(" WHERE ")
        cmdTextSb.Append("      1=1 ")
        cmdTextSb.Append("  AND TS.kbn = " & strPrmKbn)
        cmdTextSb.Append("  AND TS.hosyousyo_no = " & strPrmBangou)
        cmdTextSb.Append("  AND TS.bunrui_cd IN('100', '110', '115', '120') ")
        cmdTextSb.Append(" GROUP BY ")
        cmdTextSb.Append("      TS.kbn ")
        cmdTextSb.Append("    , TS.hosyousyo_no ")

        ' �p�����[�^�֐ݒ�
        Dim cmdPrms() As SqlParameter = _
            {SQLHelper.MakeParam(strPrmKbn, SqlDbType.Char, 1, strKbn), _
             SQLHelper.MakeParam(strPrmBangou, SqlDbType.VarChar, 10, strBangou)}

        '�f�[�^�擾���ԋp
        dTblRes = cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdPrms)
        If dTblRes.Rows.Count > 0 Then
            strRetHosyouUmu = dTblRes.Rows(0)("teibetu_hosyou_syouhin_umu").ToString
        End If

        Return strRetHosyouUmu
    End Function

#End Region

#Region "�@�ʓ����f�[�^�̎擾"

    ''' <summary>
    ''' �@�ʓ������R�[�h���擾���܂�
    ''' </summary>
    ''' <param name="strKbn">�敪</param>
    ''' <param name="strHosyousyoNo">�ۏ؏�NO</param>
    ''' <returns>�f�[�^�e�[�u��</returns>
    ''' <remarks></remarks>
    Public Function GetTeibetuNyuukinDataKey(ByVal strKbn As String _
                                            , ByVal strHosyousyoNo As String) As DataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTeibetuNyuukinData", _
                                            strKbn, _
                                            strHosyousyoNo)

        '�p�����[�^
        Const strParamKbn As String = "@KBN"
        Const strParamHosyousyoNo As String = "@HOSYOUSYONO"

        Dim cmdTextSb As New StringBuilder()

        cmdTextSb.Append(" SELECT")
        cmdTextSb.Append("      kbn")
        cmdTextSb.Append("    , hosyousyo_no")
        cmdTextSb.Append("    , bunrui_cd")
        cmdTextSb.Append("    , gamen_hyouji_no")
        cmdTextSb.Append("    , ISNULL(zeikomi_nyuukin_gaku, 0) - ISNULL(zeikomi_henkin_gaku, 0) AS nyuukin_gaku")
        cmdTextSb.Append("    , zeikomi_nyuukin_gaku")
        cmdTextSb.Append("    , zeikomi_henkin_gaku")
        cmdTextSb.Append("    , add_login_user_id")
        cmdTextSb.Append("    , add_datetime")
        cmdTextSb.Append("    , upd_login_user_id")
        cmdTextSb.Append("    , upd_datetime")
        cmdTextSb.Append(" FROM")
        cmdTextSb.Append("      t_teibetu_nyuukin")
        cmdTextSb.Append("      WITH (READCOMMITTED)")
        cmdTextSb.Append(" WHERE")
        cmdTextSb.Append("      kbn = " & strParamKbn)
        cmdTextSb.Append("  AND hosyousyo_no = " & strParamHosyousyoNo)

        ' �p�����[�^�֐ݒ�
        Dim cmdParams() As SqlParameter = _
            {SQLHelper.MakeParam(strParamKbn, SqlDbType.Char, 1, strKbn), _
             SQLHelper.MakeParam(strParamHosyousyoNo, SqlDbType.VarChar, 10, strHosyousyoNo)}

        cmdTextSb.Append(" ORDER BY bunrui_cd")
        cmdTextSb.Append("        , gamen_hyouji_no")

        Dim cmnDtAcc As New CmnDataAccess

        '�f�[�^�擾���ԋp 
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)

    End Function

    ''' <summary>
    ''' �@�ʓ������R�[�h���擾���܂�
    ''' </summary>
    ''' <param name="strKbn">�敪</param>
    ''' <param name="strHosyousyoNo">�ۏ؏�NO</param>
    ''' <param name="strBunruiCd">���ރR�[�h�i�C�Ӂj</param>
    ''' <param name="intGamenHyoujiNo">��ʕ\��NO�i�C�Ӂj</param>
    ''' <returns>�f�[�^�e�[�u��</returns>
    ''' <remarks></remarks>
    Public Function GetTeibetuNyuukinDataKey(ByVal strKbn As String _
                                            , ByVal strHosyousyoNo As String _
                                            , ByVal strBunruiCd As String _
                                            , ByVal intGamenHyoujiNo As Integer) As DataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTeibetuNyuukinData", _
                                            strKbn, _
                                            strHosyousyoNo, _
                                            strBunruiCd, _
                                            intGamenHyoujiNo)

        '�p�����[�^
        Const strParamKbn As String = "@KBN"
        Const strParamHosyousyoNo As String = "@HOSYOUSYONO"
        Const strParamBunruiCd As String = "@BUNRUICD"
        Const strParamGamenHyoujiNo As String = "@GAMENHYOUJINO"

        Dim cmdTextSb As New StringBuilder()

        cmdTextSb.Append(" SELECT")
        cmdTextSb.Append("      kbn")
        cmdTextSb.Append("    , hosyousyo_no")
        cmdTextSb.Append("    , bunrui_cd")
        cmdTextSb.Append("    , gamen_hyouji_no")
        cmdTextSb.Append("    , ISNULL(zeikomi_nyuukin_gaku, 0) - ISNULL(zeikomi_henkin_gaku, 0) AS nyuukin_gaku")
        cmdTextSb.Append("    , zeikomi_nyuukin_gaku")
        cmdTextSb.Append("    , zeikomi_henkin_gaku")
        cmdTextSb.Append("    , add_login_user_id")
        cmdTextSb.Append("    , add_datetime")
        cmdTextSb.Append("    , upd_login_user_id")
        cmdTextSb.Append("    , upd_datetime")
        cmdTextSb.Append(" FROM")
        cmdTextSb.Append("      t_teibetu_nyuukin")
        cmdTextSb.Append("      WITH (READCOMMITTED)")
        cmdTextSb.Append(" WHERE")
        cmdTextSb.Append("      kbn = " & strParamKbn)
        cmdTextSb.Append("  AND hosyousyo_no = " & strParamHosyousyoNo)
        cmdTextSb.Append(" AND LEFT(bunrui_cd, 2) = LEFT(" & strParamBunruiCd & ", 2)")
        cmdTextSb.Append(" AND gamen_hyouji_no = " & strParamGamenHyoujiNo)

        cmdTextSb.Append(" ORDER BY bunrui_cd")
        cmdTextSb.Append("        , gamen_hyouji_no")

        Dim cmdParams() As SqlParameter = _
        {SQLHelper.MakeParam(strParamKbn, SqlDbType.Char, 1, strKbn), _
         SQLHelper.MakeParam(strParamHosyousyoNo, SqlDbType.VarChar, 10, strHosyousyoNo), _
         SQLHelper.MakeParam(strParamBunruiCd, SqlDbType.VarChar, 3, strBunruiCd), _
         SQLHelper.MakeParam(strParamGamenHyoujiNo, SqlDbType.Int, 4, intGamenHyoujiNo)}

        Dim cmnDtAcc As New CmnDataAccess

        '�f�[�^�擾���ԋp 
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)

    End Function


    ''' <summary>
    ''' �@�ʓ������R�[�h�̕��ނ��Ƃ̓����z���擾���܂�
    ''' </summary>
    ''' <param name="strKbn">�敪</param>
    ''' <param name="strHosyousyoNo">�ۏ؏�NO</param>
    ''' <returns>�f�[�^�e�[�u��</returns>
    ''' <remarks></remarks>
    Public Function GetTeibetuNyuukinData(ByVal strKbn As String _
                                        , ByVal strHosyousyoNo As String) As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTeibetuNyuukinData", _
                                                   strKbn, _
                                                   strHosyousyoNo)
        ' �p�����[�^
        Const strParamKbn As String = "@KBN"
        Const strParamHosyousyoNo As String = "@HOSYOUSYONO"
        Dim cmdTextSb As New StringBuilder()

        cmdTextSb.Append(" SELECT")
        cmdTextSb.Append("      kbn")
        cmdTextSb.Append("    , hosyousyo_no")
        cmdTextSb.Append("    , bunrui_cd")
        cmdTextSb.Append("    , SUM(ISNULL(zeikomi_nyuukin_gaku, 0) - ISNULL(zeikomi_henkin_gaku, 0)) nyuukin_gaku")
        cmdTextSb.Append(" FROM")
        cmdTextSb.Append("     (SELECT")
        cmdTextSb.Append("           kbn")
        cmdTextSb.Append("         , hosyousyo_no")
        cmdTextSb.Append("         ,")
        cmdTextSb.Append("           CASE")
        cmdTextSb.Append("                WHEN bunrui_cd in('100', '110', '115', '180')")
        cmdTextSb.Append("                THEN '100'")
        cmdTextSb.Append("                ELSE bunrui_cd")
        cmdTextSb.Append("           END bunrui_cd")
        cmdTextSb.Append("         , zeikomi_nyuukin_gaku")
        cmdTextSb.Append("         , zeikomi_henkin_gaku")
        cmdTextSb.Append("         , add_login_user_id")
        cmdTextSb.Append("         , add_datetime")
        cmdTextSb.Append("         , upd_login_user_id")
        cmdTextSb.Append("         , upd_datetime")
        cmdTextSb.Append("      FROM")
        cmdTextSb.Append("           t_teibetu_nyuukin WITH (READCOMMITTED)")
        cmdTextSb.Append("      WHERE")
        cmdTextSb.Append("           kbn = " & strParamKbn)
        cmdTextSb.Append("       AND hosyousyo_no = " & strParamHosyousyoNo)
        cmdTextSb.Append("     ) T")
        cmdTextSb.Append(" GROUP BY")
        cmdTextSb.Append("      kbn")
        cmdTextSb.Append("    , hosyousyo_no")
        cmdTextSb.Append("    , bunrui_cd")

        ' �p�����[�^�֐ݒ�
        Dim cmdParams() As SqlParameter = _
            {SQLHelper.MakeParam(strParamKbn, SqlDbType.Char, 1, strKbn), _
             SQLHelper.MakeParam(strParamHosyousyoNo, SqlDbType.VarChar, 10, strHosyousyoNo)}

        Dim cmnDtAcc As New CmnDataAccess

        '�f�[�^�擾���ԋp 
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)
    End Function

    ''' <summary>
    ''' �n�ՂƓ@�ʐ����ɕR�Â��@�ʓ����f�[�^���擾�i�n�Ղɂ����ē@�ʐ����ɂȂ��@�ʓ����f�[�^���擾�j
    ''' </summary>
    ''' <param name="strKbn">�敪</param>
    ''' <param name="strHosyousyoNo">�ۏ؏�NO</param>
    ''' <returns>�f�[�^�e�[�u��</returns>
    ''' <remarks></remarks>
    Public Function GetTeibetuSeikyuuNyuukinData(ByVal strKbn As String _
                                                , ByVal strHosyousyoNo As String) As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTeibetuSeikyuuNyuukinData", _
                                                   strKbn, _
                                                   strHosyousyoNo)
        ' �p�����[�^
        Const strParamKbn As String = "@KBN"
        Const strParamHosyousyoNo As String = "@HOSYOUSYONO"
        Dim cmdTextSb As New StringBuilder()

        cmdTextSb.Append(" SELECT")
        cmdTextSb.Append("      *")
        cmdTextSb.Append(" FROM")
        cmdTextSb.Append("     (SELECT")
        cmdTextSb.Append("           TJ.kbn")
        cmdTextSb.Append("         , TJ.hosyousyo_no")
        cmdTextSb.Append("         , ISNULL(TS.bunrui_cd, TN.bunrui_cd) bunrui_cd")
        cmdTextSb.Append("         , ISNULL(TS.gamen_hyouji_no, TN.bunrui_cd) gamen_hyouji_no")
        cmdTextSb.Append("         , ISNULL(TN.zeikomi_nyuukin_gaku, 0) - ISNULL(TN.zeikomi_henkin_gaku, 0) AS nyuukin_gaku")
        cmdTextSb.Append("         , TN.zeikomi_nyuukin_gaku")
        cmdTextSb.Append("         , TN.zeikomi_henkin_gaku")
        cmdTextSb.Append("         , TN.add_login_user_id")
        cmdTextSb.Append("         , TN.add_datetime")
        cmdTextSb.Append("         , TN.upd_login_user_id")
        cmdTextSb.Append("         , TN.upd_datetime")
        cmdTextSb.Append("      FROM")
        cmdTextSb.Append("           t_jiban TJ")
        cmdTextSb.Append("                LEFT OUTER JOIN t_teibetu_seikyuu TS")
        cmdTextSb.Append("                  ON TJ.kbn = TS.kbn")
        cmdTextSb.Append("                 AND TJ.hosyousyo_no = TS.hosyousyo_no")
        cmdTextSb.Append("                LEFT OUTER JOIN t_teibetu_nyuukin TN")
        cmdTextSb.Append("                  ON TJ.kbn = TN.kbn")
        cmdTextSb.Append("                 AND TJ.hosyousyo_no = TN.hosyousyo_no")
        cmdTextSb.Append("                 AND LEFT (TS.bunrui_cd, 2) = LEFT (TN.bunrui_cd, 2)")
        cmdTextSb.Append("                 AND TS.gamen_hyouji_no = TN.gamen_hyouji_no")
        cmdTextSb.Append("      WHERE")
        cmdTextSb.Append("            TJ.kbn = " & strParamKbn)
        cmdTextSb.Append("        AND TJ.hosyousyo_no = " & strParamHosyousyoNo)
        cmdTextSb.Append("      UNION")
        cmdTextSb.Append("      SELECT")
        cmdTextSb.Append("           TJ.kbn")
        cmdTextSb.Append("         , TJ.hosyousyo_no")
        cmdTextSb.Append("         , ISNULL(TS.bunrui_cd, TN.bunrui_cd) bunrui_cd")
        cmdTextSb.Append("         , ISNULL(TS.gamen_hyouji_no, TN.gamen_hyouji_no) gamen_hyouji_no")
        cmdTextSb.Append("         , ISNULL(TN.zeikomi_nyuukin_gaku, 0) - ISNULL(TN.zeikomi_henkin_gaku, 0) AS nyuukin_gaku")
        cmdTextSb.Append("         , TN.zeikomi_nyuukin_gaku")
        cmdTextSb.Append("         , TN.zeikomi_henkin_gaku")
        cmdTextSb.Append("         , TN.add_login_user_id")
        cmdTextSb.Append("         , TN.add_datetime")
        cmdTextSb.Append("         , TN.upd_login_user_id")
        cmdTextSb.Append("         , TN.upd_datetime")
        cmdTextSb.Append("      FROM")
        cmdTextSb.Append("           t_jiban TJ")
        cmdTextSb.Append("                LEFT OUTER JOIN t_teibetu_nyuukin TN")
        cmdTextSb.Append("                  ON TJ.kbn = TN.kbn")
        cmdTextSb.Append("                 AND TJ.hosyousyo_no = TN.hosyousyo_no")
        cmdTextSb.Append("                LEFT OUTER JOIN t_teibetu_seikyuu TS")
        cmdTextSb.Append("                  ON TJ.kbn = TS.kbn")
        cmdTextSb.Append("                 AND TJ.hosyousyo_no = TS.hosyousyo_no")
        cmdTextSb.Append("                 AND LEFT (TN.bunrui_cd, 2) = LEFT (TS.bunrui_cd, 2)")
        cmdTextSb.Append("                 AND TN.gamen_hyouji_no = TS.gamen_hyouji_no")
        cmdTextSb.Append("      WHERE")
        cmdTextSb.Append("            TJ.kbn = " & strParamKbn)
        cmdTextSb.Append("        AND TJ.hosyousyo_no = " & strParamHosyousyoNo)
        cmdTextSb.Append("     )")
        cmdTextSb.Append("      WK")
        cmdTextSb.Append(" ORDER BY")
        cmdTextSb.Append("      kbn")
        cmdTextSb.Append("    , hosyousyo_no")
        cmdTextSb.Append("    , LEFT (bunrui_cd, 2)")
        cmdTextSb.Append("    , gamen_hyouji_no")
        ' �p�����[�^�֐ݒ�
        Dim cmdParams() As SqlParameter = _
            {SQLHelper.MakeParam(strParamKbn, SqlDbType.Char, 1, strKbn), _
             SQLHelper.MakeParam(strParamHosyousyoNo, SqlDbType.VarChar, 10, strHosyousyoNo)}

        Dim cmnDtAcc As New CmnDataAccess

        '�f�[�^�擾���ԋp 
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)

    End Function
#End Region

#Region "�n�Ճf�[�^�o�^"
    ''' <summary>
    ''' �n�Ճe�[�u���փf�[�^�𔽉f���܂�
    ''' </summary>
    ''' <param name="kbn">�敪</param>
    ''' <param name="hosyousyoNo">�ۏ؏�NO</param>
    ''' <param name="addLoginUserId">�o�^��ID</param>
    ''' <returns>�o�^����</returns>
    ''' <remarks></remarks>
    Public Function InsertJibanData(ByVal kbn As String, _
                                    ByVal hosyousyoNo As String, _
                                    ByVal addLoginUserId As String, _
                                    ByVal sinkiTourokuMotoKbnType As EarthEnum.EnumSinkiTourokuMotoKbnType _
                                    ) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".InsertJibanData", _
                                    kbn, _
                                    hosyousyoNo, _
                                    addLoginUserId, _
                                    sinkiTourokuMotoKbnType)

        Dim intResult As Integer = 0
        Dim commandTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        Dim strKousinsya As String = strLogic.GetKousinsya(addLoginUserId, DateTime.Now) '�X�V��

        ' �����ݒ�
        commandTextSb.Append(" INSERT INTO t_jiban ")
        commandTextSb.Append(" (   kbn, ")
        commandTextSb.Append("     hosyousyo_no, ")
        commandTextSb.Append("     data_haki_syubetu, ")
        commandTextSb.Append("     syouhin_kbn, ")
        commandTextSb.Append("     tys_gaiyou, ")
        commandTextSb.Append("     tenpu_heimenzu, ")
        commandTextSb.Append("     tenpu_ritumenzu, ")
        commandTextSb.Append("     tenpu_ks_husezu, ")
        commandTextSb.Append("     tenpu_danmenzu, ")
        commandTextSb.Append("     tenpu_kukeizu, ")
        commandTextSb.Append("     douji_irai_tousuu, ")
        commandTextSb.Append("     kasi_umu, ")
        commandTextSb.Append("     henkin_syori_flg, ")
        commandTextSb.Append("     keiyu, ")
        commandTextSb.Append("     add_login_user_id, ")
        commandTextSb.Append("     add_datetime, ")
        commandTextSb.Append("     kousinsya, ")
        commandTextSb.Append("     sinki_touroku_moto_kbn, ")
        commandTextSb.Append("     koj_hantei_kekka_flg ")
        commandTextSb.Append(" )VALUES( ")
        commandTextSb.Append("     @KBN, ")
        commandTextSb.Append("     @HOSYOUSYONO, ")
        commandTextSb.Append("     0, ")
        commandTextSb.Append("     9, ")
        commandTextSb.Append("     9, ")
        commandTextSb.Append("     0, ")
        commandTextSb.Append("     0, ")
        commandTextSb.Append("     0, ")
        commandTextSb.Append("     0, ")
        commandTextSb.Append("     0, ")
        commandTextSb.Append("     0, ")
        commandTextSb.Append("     0, ")
        commandTextSb.Append("     0, ")
        commandTextSb.Append("     0, ")
        commandTextSb.Append("     @ADDLOGINUSERID, ")
        commandTextSb.Append("     @ADDDATETIME, ")
        commandTextSb.Append("     @KOUSINSYA, ")
        commandTextSb.Append("     @TOUROKUMOTO, ")
        commandTextSb.Append("     0 ")
        commandTextSb.Append(" ) ")

        cmdParams = New SqlParameter() { _
                SQLHelper.MakeParam("@KBN", SqlDbType.Char, 1, kbn), _
                SQLHelper.MakeParam("@HOSYOUSYONO", SqlDbType.VarChar, 10, hosyousyoNo), _
                SQLHelper.MakeParam("@ADDLOGINUSERID", SqlDbType.VarChar, 30, addLoginUserId), _
                SQLHelper.MakeParam("@ADDDATETIME", SqlDbType.DateTime, 16, DateTime.Now), _
                SQLHelper.MakeParam("@KOUSINSYA", SqlDbType.VarChar, 30, strKousinsya), _
                SQLHelper.MakeParam("@TOUROKUMOTO", SqlDbType.Int, 10, sinkiTourokuMotoKbnType) _
                }

        ' �N�G�����s
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    commandTextSb.ToString(), _
                                    cmdParams)

        Return intResult

    End Function
#End Region

#Region "�n�Ճf�[�^�X�V"
    ''' <summary>
    ''' �n�Ճe�[�u���փf�[�^�𔽉f���܂�
    ''' </summary>
    ''' <param name="sql">�X�VSQL</param>
    ''' <returns>�X�V�ɕK�v�ȃp�����[�^���</returns>
    ''' <remarks>���{�����͌ŗL�̃e�[�u���Ɉˑ����Ȃ��ׁA�e��e�[�u���̍X�V�Ɏg�p��</remarks>
    Public Function UpdateJibanData(ByVal sql As String, _
                                    ByVal paramList As List(Of ParamRecord)) As Boolean

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".UpdateJibanData", _
                            sql, _
                            paramList)

        Dim intResult As Integer = 0
        Dim cmdParams(paramList.Count - 1) As SqlClient.SqlParameter
        Dim i As Integer

        For i = 0 To paramList.Count - 1
            Dim rec As ParamRecord = paramList(i)
            ' �K�v�ȏ����Z�b�g
            cmdParams(i) = SQLHelper.MakeParam(rec.Param, rec.DbType, rec.ParamLength, rec.SetData)
        Next

        ' �N�G�����s
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    sql, _
                                    cmdParams)

        ' �X�V�Ɏ��s�����ꍇ�AFalse
        If intResult < 1 Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' �n�Ճe�[�u���̔r���`�F�b�N���s���܂�
    ''' </summary>
    ''' <param name="sql">SQL</param>
    ''' <param name="paramList">�p�����[�^�̃��X�g</param>
    ''' <returns>�n�Ճf�[�^�r�����R�[�h�i�X�V���t����L��̏ꍇ�擾����܂��j</returns>
    ''' <remarks></remarks>
    Public Function CheckHaita(ByVal sql As String, _
                               ByVal paramList As List(Of ParamRecord)) As JibanDataSet.JibanHaitaTableDataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".CheckHaita", _
                                                    sql, _
                                                    paramList)

        Dim intResult As Integer = 0
        Dim cmdParams(paramList.Count - 1) As SqlClient.SqlParameter
        Dim i As Integer

        For i = 0 To paramList.Count - 1
            Dim rec As ParamRecord = paramList(i)
            ' �K�v�ȏ����Z�b�g
            cmdParams(i) = SQLHelper.MakeParam(rec.Param, rec.DbType, rec.ParamLength, rec.SetData)
        Next

        ' �f�[�^�̎擾
        Dim JibanDataSet As New JibanDataSet()

        SQLHelper.FillDataset(connStr, CommandType.Text, sql, _
            JibanDataSet, JibanDataSet.JibanHaitaTable.TableName, cmdParams)

        Dim haitaTable As JibanDataSet.JibanHaitaTableDataTable = JibanDataSet.JibanHaitaTable

        Return haitaTable

    End Function

    ''' <summary>
    ''' �o�R���X�V���܂�
    ''' </summary>
    ''' <param name="kbn">�敪</param>
    ''' <param name="hosyousyoNo">�ۏ؏�NO</param>
    ''' <param name="addLoginUserId">���O�C�����[�U�[ID</param>
    ''' <param name="keiyu">�o�R</param>
    ''' <returns>�X�V����</returns>
    ''' <remarks></remarks>
    Public Function UpdateJibanKeiyu(ByVal kbn As String, _
                                   ByVal hosyousyoNo As String, _
                                   ByVal addLoginUserId As String, _
                                   ByVal keiyu As Integer, _
                                   Optional ByVal updateDatetime As DateTime = Nothing) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".UpdateJibanKeiyu", _
                                            kbn, _
                                            hosyousyoNo, _
                                            addLoginUserId, _
                                            keiyu)

        Dim intResult As Integer = 0
        Dim commandTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        ' �����ݒ�
        commandTextSb.Append(" UPDATE t_jiban ")
        commandTextSb.Append(" SET keiyu = @KEIYU, ")
        commandTextSb.Append("     upd_login_user_id = @UPDLOGINUSERID, ")
        commandTextSb.Append("     upd_datetime     = @UPDDATETIME ")
        commandTextSb.Append(" WHERE kbn             = @KBN ")
        commandTextSb.Append(" AND   hosyousyo_no    = @HOSYOUSYONO ")

        If updateDatetime = Nothing Then
            updateDatetime = DateTime.Now
        End If

        cmdParams = New SqlParameter() { _
                SQLHelper.MakeParam("@KBN", SqlDbType.Char, 1, kbn), _
                SQLHelper.MakeParam("@HOSYOUSYONO", SqlDbType.VarChar, 10, hosyousyoNo), _
                SQLHelper.MakeParam("@KEIYU", SqlDbType.Int, 4, keiyu), _
                SQLHelper.MakeParam("@UPDLOGINUSERID", SqlDbType.VarChar, 30, addLoginUserId), _
                SQLHelper.MakeParam("@UPDDATETIME", SqlDbType.DateTime, 16, updateDatetime)}

        ' �N�G�����s
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    commandTextSb.ToString(), _
                                    cmdParams)

        Return intResult

    End Function

    ''' <summary>
    ''' ���s�˗��L�����Z���������X�V���܂�
    ''' </summary>
    ''' <param name="kbn">�敪</param>
    ''' <param name="hosyousyoNo">�ۏ؏�NO</param>
    ''' <param name="addLoginUserId">���O�C�����[�U�[ID</param>
    ''' <param name="cancelDatetime">���s�˗��L�����Z������</param>
    ''' <returns>�X�V����</returns>
    ''' <remarks></remarks>
    Public Function UpdateJibanIraiCancel(ByVal kbn As String, _
                                   ByVal hosyousyoNo As String, _
                                   ByVal addLoginUserId As String, _
                                   ByVal cancelDatetime As DateTime, _
                                   Optional ByVal updateDatetime As DateTime = Nothing) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".UpdateJibancancelDatetime", _
                                            kbn, _
                                            hosyousyoNo, _
                                            addLoginUserId, _
                                            cancelDatetime)

        Dim intResult As Integer = 0
        Dim commandTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        ' �����ݒ�
        commandTextSb.Append(" UPDATE t_jiban ")
        commandTextSb.Append(" SET hak_irai_can_datetime = @HAKIRAICANDATETIME, ")
        commandTextSb.Append("     upd_login_user_id     = @UPDLOGINUSERID, ")
        commandTextSb.Append("     upd_datetime          = @UPDDATETIME ")
        commandTextSb.Append(" WHERE kbn          = @KBN ")
        commandTextSb.Append(" AND   hosyousyo_no = @HOSYOUSYONO ")

        If updateDatetime = Nothing Then
            updateDatetime = DateTime.Now
        End If

        cmdParams = New SqlParameter() { _
                SQLHelper.MakeParam("@KBN", SqlDbType.Char, 1, kbn), _
                SQLHelper.MakeParam("@HOSYOUSYONO", SqlDbType.VarChar, 10, hosyousyoNo), _
                SQLHelper.MakeParam("@HAKIRAICANDATETIME", SqlDbType.DateTime, 16, cancelDatetime), _
                SQLHelper.MakeParam("@UPDLOGINUSERID", SqlDbType.VarChar, 30, addLoginUserId), _
                SQLHelper.MakeParam("@UPDDATETIME", SqlDbType.DateTime, 16, updateDatetime)}

        ' �N�G�����s
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    commandTextSb.ToString(), _
                                    cmdParams)

        Return intResult

    End Function
#End Region

#Region "�n�Ճf�[�^�E�@�ʃf�[�^�폜"
    ''' <summary>
    ''' �n�Ճe�[�u�����폜���܂�
    ''' </summary>
    ''' <param name="kbn">�敪</param>
    ''' <param name="hosyousyoNo">�ۏ؏�NO</param>
    ''' <param name="deleteTeibetu">�@�ʐ������R�[�h���폜����ꍇ�FTrue</param>
    ''' <param name="loginUserId">�X�V���O�C�����[�U�[ID(�폜�����g���K�p)</param>
    ''' <returns>�폜���� True:����-�R�~�b�g���Ă������� False:���s</returns>
    ''' <remarks>�n�Ճf�[�^�Ɠ@�ʐ����̓�����ۂ��߁A�Ăяo�����Ńg�����U�N�V����������s���ĉ�����</remarks>
    Public Function DeleteJibanData(ByVal kbn As String, _
                                    ByVal hosyousyoNo As String, _
                                    ByVal deleteTeibetu As Boolean, _
                                    ByVal loginUserId As String) As Boolean

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".DeleteJibanData", _
                                    kbn, _
                                    hosyousyoNo, _
                                    deleteTeibetu)

        Dim intResult As Integer = 0
        Dim commandTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter = { _
            SQLHelper.MakeParam("@KBN", SqlDbType.Char, 1, kbn), _
            SQLHelper.MakeParam("@HOSYOUSYONO", SqlDbType.VarChar, 10, hosyousyoNo)}

        '*****************************************************************
        ' �n�Ճe�[�u���̍폜
        '*****************************************************************
        commandTextSb.Append(" DELETE FROM t_jiban ")
        commandTextSb.Append(" WHERE kbn = @KBN AND hosyousyo_no = @HOSYOUSYONO ")

        ' �N�G�����s
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    commandTextSb.ToString(), _
                                    cmdParams)

        ' �������R�[�h����0�ȉ��̏ꍇ�AFalse�ō폜�����I��
        If intResult <= 0 Then
            Return False
        End If

        ' �@�ʐ����e�[�u���𓯎��폜����ꍇ
        If deleteTeibetu = True Then
            '*****************************************************************
            ' �@�ʐ����e�[�u���̍폜
            '*****************************************************************
            commandTextSb = New StringBuilder()
            commandTextSb.Append(CreateUserInfoTempTableSQL(loginUserId))   '�폜�g���K�p���[�J���ꎞ�e�[�u������SQL��ǉ�
            commandTextSb.Append(" DELETE FROM t_teibetu_seikyuu ")
            commandTextSb.Append(" WHERE kbn = @KBN AND hosyousyo_no = @HOSYOUSYONO ")

            ' �N�G�����s
            intResult = ExecuteNonQuery(connStr, _
                                        CommandType.Text, _
                                        commandTextSb.ToString(), _
                                        cmdParams)

            ' �������R�[�h����0�ȉ��̏ꍇ�AFalse�ō폜�����I��
            If intResult <= 0 Then
                Return False
            End If

        End If

        Return True

    End Function
#End Region

#Region "�@�ʐ�����M�f�[�^�폜"
    ''' <summary>
    ''' �@�ʐ����e�[�u�����폜���܂��i�敪�E�ۏ؏�NO�őS�āj
    ''' </summary>
    ''' <param name="kbn">�敪</param>
    ''' <param name="hosyousyoNo">�ۏ؏�NO</param>
    ''' <returns>�폜���� True:���� False:���s</returns>
    ''' <remarks></remarks>
    Public Function DeleteteibetuSeikyuuJyusinData(ByVal kbn As String, _
                                                   ByVal hosyousyoNo As String) As Boolean

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".DeleteteibetuSeikyuuJyusinData", _
                                            kbn, _
                                            hosyousyoNo)

        Dim intResult As Integer = 0
        Dim commandTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter = { _
            SQLHelper.MakeParam("@KBN", SqlDbType.Char, 1, kbn), _
            SQLHelper.MakeParam("@HOSYOUSYONO", SqlDbType.VarChar, 10, hosyousyoNo) _
            }

        '*****************************************************************
        ' �@�ʐ����e�[�u���̍폜
        '*****************************************************************
        commandTextSb = New StringBuilder()
        commandTextSb.Append(" DELETE FROM t_teibetu_seikyuu_jyusin ")
        commandTextSb.Append(" WHERE kbn = @KBN  ")
        commandTextSb.Append(" AND   hosyousyo_no = @HOSYOUSYONO ")

        ' �N�G�����s
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    commandTextSb.ToString(), _
                                    cmdParams)
        Return True

    End Function
#End Region

#Region "�A�g�f�[�^�o�^�E�X�V"
#Region "�n�՘A�g"
    ''' <summary>
    ''' �n�՘A�g�e�[�u���̓��e���m�F��f�[�^�𔽉f���܂�
    ''' </summary>
    ''' <param name="jibanRrec">�n�՘A�g���R�[�h</param>
    ''' <param name="isUpdate">�n�Ճe�[�u�����X�V�����ꍇ��True</param>
    ''' <returns>���f����</returns>
    ''' <remarks></remarks>
    Public Function EditJibanRenkeiData(ByVal jibanRrec As JibanRenkeiRecord, _
                                        Optional ByVal isUpdate As Boolean = True) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".EditJibanRenkeiData", _
                                                    jibanRrec, _
                                                    isUpdate)

        ' �n�՘A�g�e�[�u���̑��݃`�F�b�N
        ' �p�����[�^
        Const paramKubun As String = "@KUBUN"
        Const paramHosyousyoNo As String = "@HOSYOUSYONO"

        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append(" SELECT sousin_jyky_cd ")
        commandTextSb.Append(" FROM t_jiban_renkei WITH(UPDLOCK) ")
        commandTextSb.Append(" WHERE kbn = " & paramKubun)
        commandTextSb.Append(" AND   hosyousyo_no = " & paramHosyousyoNo)

        ' �p�����[�^�֐ݒ�
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(paramKubun, SqlDbType.Char, 1, jibanRrec.Kbn), _
             SQLHelper.MakeParam(paramHosyousyoNo, SqlDbType.VarChar, 10, jibanRrec.HosyousyoNo)}

        ' ���M�󋵃R�[�h
        'Dim sousin_jyky_cd As Integer
        Dim ret_value As Object = SQLHelper.ExecuteScalar(connStr, _
                                                          CommandType.Text, _
                                                          commandTextSb.ToString(), _
                                                          commandParameters)

        If ret_value Is Nothing Then
            ' �l���擾�ł��Ȃ��ꍇ�͐V�K��Insert����(�V�K�͒���Insert���\�b�h�����s��)
            jibanRrec.SousinJykyCd = 0
            '�폜�̏ꍇ�͑��M�󋵃R�[�h���폜��Insert
            If jibanRrec.RenkeiSijiCd <> 9 Then
                If isUpdate = True Then
                    '�����n�Ճf�[�^�̍X�V�Ȃ̂� 2
                    jibanRrec.RenkeiSijiCd = 2
                Else
                    jibanRrec.RenkeiSijiCd = 1
                End If
            End If
            Return InsertJibanRenkeiData(jibanRrec)

        ElseIf ret_value = 0 Then
            ' ���M�󋵃R�[�h�������M�̏ꍇ�A���M�R�[�h�A�A�g�w���R�[�h��ύX����UPDATE
            Return UpdateJibanRenkeiData(jibanRrec, False)
        Else
            ' ���M�󋵃R�[�h�����M�ς̏ꍇ�A���M�R�[�h�F�����M�A�A�g�w���R�[�h�F�X�V��UPDATE
            jibanRrec.SousinJykyCd = 0
            jibanRrec.RenkeiSijiCd = 2
            Return UpdateJibanRenkeiData(jibanRrec, True)
        End If

    End Function

    ''' <summary>
    ''' �n�՘A�g�e�[�u���փf�[�^��o�^���܂�
    ''' </summary>
    ''' <param name="jibanRec">�n�՘A�g���R�[�h</param>
    ''' <returns>�o�^����</returns>
    ''' <remarks></remarks>
    Public Function InsertJibanRenkeiData(ByVal jibanRec As JibanRenkeiRecord) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".InsertJibanRenkeiData", _
                                                    jibanRec)

        Dim intResult As Integer = 0
        Dim commandTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        ' �o�^�pSQL�ݒ�
        commandTextSb.Append(" INSERT INTO t_jiban_renkei ( ")
        commandTextSb.Append("     kbn, ")
        commandTextSb.Append("     hosyousyo_no, ")
        commandTextSb.Append("     renkei_siji_cd, ")
        commandTextSb.Append("     sousin_jyky_cd, ")
        commandTextSb.Append("     upd_login_user_id, ")
        commandTextSb.Append("     upd_datetime ")
        commandTextSb.Append(" ) VALUES ( ")
        commandTextSb.Append("     @KBN, ")
        commandTextSb.Append("     @HOSYOUSYONO, ")
        commandTextSb.Append("     @RENKEISIJICD, ")
        commandTextSb.Append("     @SOUSINJYKYCD, ")
        commandTextSb.Append("     @UPDLOGINUSERID, ")
        commandTextSb.Append("     @ADDDATETIME )")

        cmdParams = New SqlParameter() { _
                SQLHelper.MakeParam("@KBN", SqlDbType.Char, 1, jibanRec.Kbn), _
                SQLHelper.MakeParam("@HOSYOUSYONO", SqlDbType.VarChar, 10, jibanRec.HosyousyoNo), _
                SQLHelper.MakeParam("@RENKEISIJICD", SqlDbType.Int, 4, jibanRec.RenkeiSijiCd), _
                SQLHelper.MakeParam("@SOUSINJYKYCD", SqlDbType.Int, 4, jibanRec.SousinJykyCd), _
                SQLHelper.MakeParam("@UPDLOGINUSERID", SqlDbType.VarChar, 30, jibanRec.UpdLoginUserId), _
                SQLHelper.MakeParam("@ADDDATETIME", SqlDbType.DateTime, 16, DateTime.Now)}

        ' �N�G�����s
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    commandTextSb.ToString(), _
                                    cmdParams)

        Return intResult

    End Function

    ''' <summary>
    ''' �n�՘A�g�e�[�u���̃f�[�^���X�V���܂�
    ''' </summary>
    ''' <param name="jibanRec">�n�՘A�g���R�[�h</param>
    ''' <param name="isAll">True:���M�R�[�h,�A�g�w���R�[�h���X�V����</param>
    ''' <returns>�X�V����</returns>
    ''' <remarks></remarks>
    Public Function UpdateJibanRenkeiData(ByVal jibanRec As JibanRenkeiRecord, _
                                          ByVal isAll As Boolean) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".UpdateJibanRenkeiData", _
                                            jibanRec, _
                                            isAll)

        Dim intResult As Integer = 0
        Dim commandTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        ' �o�^�pSQL�ݒ�
        commandTextSb.Append(" UPDATE t_jiban_renkei ")
        commandTextSb.Append(" SET ")
        If isAll = True Then
            commandTextSb.Append("     renkei_siji_cd    = @RENKEISIJICD, ")
            commandTextSb.Append("     sousin_jyky_cd    = @SOUSINJYKYCD, ")
        End If
        commandTextSb.Append("     upd_login_user_id = @UPDLOGINUSERID, ")
        commandTextSb.Append("     upd_datetime     = @UPDDATETIME ")
        commandTextSb.Append(" WHERE kbn             = @KBN ")
        commandTextSb.Append(" AND   hosyousyo_no    = @HOSYOUSYONO ")

        cmdParams = New SqlParameter() { _
                SQLHelper.MakeParam("@KBN", SqlDbType.Char, 1, jibanRec.Kbn), _
                SQLHelper.MakeParam("@HOSYOUSYONO", SqlDbType.VarChar, 10, jibanRec.HosyousyoNo), _
                SQLHelper.MakeParam("@RENKEISIJICD", SqlDbType.Int, 4, jibanRec.RenkeiSijiCd), _
                SQLHelper.MakeParam("@SOUSINJYKYCD", SqlDbType.Int, 4, jibanRec.SousinJykyCd), _
                SQLHelper.MakeParam("@UPDLOGINUSERID", SqlDbType.VarChar, 30, jibanRec.UpdLoginUserId), _
                SQLHelper.MakeParam("@UPDDATETIME", SqlDbType.DateTime, 16, DateTime.Now)}

        ' �N�G�����s
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    commandTextSb.ToString(), _
                                    cmdParams)

        Return intResult

    End Function

#End Region
#Region "�@�ʐ����A�g"
    ''' <summary>
    ''' �@�ʐ����A�g�e�[�u���̓��e���m�F��f�[�^�𔽉f���܂�
    ''' </summary>
    ''' <param name="teibetuRec">�@�ʐ����A�g���R�[�h</param>
    ''' <returns>���f����</returns>
    ''' <remarks></remarks>
    Public Function EditTeibetuRenkeiData(ByVal teibetuRec As TeibetuSeikyuuRenkeiRecord) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".EditTeibetuRenkeiData", _
                                                    teibetuRec)

        ' �@�ʐ����A�g�e�[�u���̑��݃`�F�b�N
        ' �p�����[�^
        Const paramKubun As String = "@KUBUN"
        Const paramHosyousyoNo As String = "@HOSYOUSYONO"
        Const paramBunruiCd As String = "@BUNRUICD"
        Const paramGamenHyoujiNo As String = "@GAMENHYOUJINO"

        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append(" SELECT sousin_jyky_cd,renkei_siji_cd ")
        commandTextSb.Append(" FROM t_teibetu_seikyuu_renkei WITH(UPDLOCK) ")
        commandTextSb.Append(" WHERE kbn = " & paramKubun)
        commandTextSb.Append(" AND   hosyousyo_no = " & paramHosyousyoNo)
        commandTextSb.Append(" AND   SUBSTRING(bunrui_cd,1,2)         = SUBSTRING(" & paramBunruiCd & ",1,2)")
        commandTextSb.Append(" AND   gamen_hyouji_no   = " & paramGamenHyoujiNo)

        ' �p�����[�^�֐ݒ�
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(paramKubun, SqlDbType.Char, 1, teibetuRec.Kbn), _
             SQLHelper.MakeParam(paramHosyousyoNo, SqlDbType.VarChar, 10, teibetuRec.HosyousyoNo), _
             SQLHelper.MakeParam(paramBunruiCd, SqlDbType.VarChar, 3, teibetuRec.BunruiCd), _
             SQLHelper.MakeParam(paramGamenHyoujiNo, SqlDbType.Int, 4, teibetuRec.GamenHyoujiNo)}

        ' �f�[�^�̎擾
        Dim TeibetuRenkeiDataSet As New TeibetuRenkeiDataSet()

        ' ���M�󋵃R�[�h
        SQLHelper.FillDataset(connStr, _
                              CommandType.Text, _
                              commandTextSb.ToString(), _
                              TeibetuRenkeiDataSet, _
                              TeibetuRenkeiDataSet.RenkeiTable.TableName, _
                              commandParameters)

        Dim renkeiTable As TeibetuRenkeiDataSet.RenkeiTableDataTable = TeibetuRenkeiDataSet.RenkeiTable


        If renkeiTable.Rows.Count < 1 Then
            ' �l���擾�ł��Ȃ��ꍇ�͐V�K��Insert����(�n�Ղ���͍X�V�ׁ̈A�@�ʐ����͐V�K�ł��X�V)
            teibetuRec.SousinJykyCd = 0
            '�폜�̏ꍇ�͑��M�󋵃R�[�h���폜��Insert
            If teibetuRec.RenkeiSijiCd <> 9 Then
                If teibetuRec.IsUpdate = True Then
                    ' �����f�[�^�̍X�V����2
                    teibetuRec.RenkeiSijiCd = 2
                Else
                    teibetuRec.RenkeiSijiCd = 1
                End If
            End If

            Return InsertTeibetuRenkeiData(teibetuRec)

        Else
            Dim row As TeibetuRenkeiDataSet.RenkeiTableRow = renkeiTable.Rows(0)

            ' �V�K�f�[�^�������M������폜�ȊO�̏ꍇ�A�V�K�E�����M�̂܂܂Ƃ���
            If row.sousin_jyky_cd = 0 And _
               row.renkei_siji_cd = 1 And _
               teibetuRec.RenkeiSijiCd <> 9 Then
                teibetuRec.SousinJykyCd = 0
                teibetuRec.RenkeiSijiCd = 1
            End If

            ' �폜�f�[�^�������M�ō���V�K�̏ꍇ�A�X�V�E�����M�ɕύX����
            If row.sousin_jyky_cd = 0 And _
               row.renkei_siji_cd = 9 And _
               teibetuRec.RenkeiSijiCd = 1 Then
                teibetuRec.SousinJykyCd = 0
                teibetuRec.RenkeiSijiCd = 2
            End If

            ' ���M�󋵃R�[�h�����M�ς̏ꍇ�A���M�R�[�h�F�����M�A�A�g�w���R�[�h�F�X�V��UPDATE
            Return UpdateTeibetuRenkeiData(teibetuRec, True)
        End If

    End Function

    ''' <summary>
    ''' �@�ʐ����A�g�e�[�u���փf�[�^��o�^���܂�
    ''' </summary>
    ''' <param name="teibetuRec">�@�ʐ����A�g���R�[�h</param>
    ''' <returns>�o�^����</returns>
    ''' <remarks></remarks>
    Public Function InsertTeibetuRenkeiData(ByVal teibetuRec As TeibetuSeikyuuRenkeiRecord) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".InsertTeibetuRenkeiData", _
                                                    teibetuRec)

        Dim intResult As Integer = 0
        Dim commandTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        ' �o�^�pSQL�ݒ�
        commandTextSb.Append(" INSERT INTO t_teibetu_seikyuu_renkei ( ")
        commandTextSb.Append("     kbn, ")
        commandTextSb.Append("     hosyousyo_no, ")
        commandTextSb.Append("     bunrui_cd, ")
        commandTextSb.Append("     gamen_hyouji_no, ")
        commandTextSb.Append("     renkei_siji_cd, ")
        commandTextSb.Append("     sousin_jyky_cd, ")
        commandTextSb.Append("     upd_login_user_id, ")
        commandTextSb.Append("     upd_datetime ")
        commandTextSb.Append(" ) VALUES ( ")
        commandTextSb.Append("     @KBN, ")
        commandTextSb.Append("     @HOSYOUSYONO, ")
        commandTextSb.Append("     @BUNRUICD, ")
        commandTextSb.Append("     @GAMENHYOUJINO, ")
        commandTextSb.Append("     @RENKEISIJICD, ")
        commandTextSb.Append("     @SOUSINJYKYCD, ")
        commandTextSb.Append("     @UPDLOGINUSERID, ")
        commandTextSb.Append("     @ADDDATETIME )")

        cmdParams = New SqlParameter() { _
                SQLHelper.MakeParam("@KBN", SqlDbType.Char, 1, teibetuRec.Kbn), _
                SQLHelper.MakeParam("@HOSYOUSYONO", SqlDbType.VarChar, 10, teibetuRec.HosyousyoNo), _
                SQLHelper.MakeParam("@BUNRUICD", SqlDbType.VarChar, 3, teibetuRec.BunruiCd), _
                SQLHelper.MakeParam("@GAMENHYOUJINO", SqlDbType.Int, 4, teibetuRec.GamenHyoujiNo), _
                SQLHelper.MakeParam("@RENKEISIJICD", SqlDbType.Int, 4, teibetuRec.RenkeiSijiCd), _
                SQLHelper.MakeParam("@SOUSINJYKYCD", SqlDbType.Int, 4, teibetuRec.SousinJykyCd), _
                SQLHelper.MakeParam("@UPDLOGINUSERID", SqlDbType.VarChar, 30, teibetuRec.UpdLoginUserId), _
                SQLHelper.MakeParam("@ADDDATETIME", SqlDbType.DateTime, 16, DateTime.Now)}

        ' �N�G�����s
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    commandTextSb.ToString(), _
                                    cmdParams)

        Return intResult

    End Function

    ''' <summary>
    ''' �@�ʐ����A�g�e�[�u���̃f�[�^���X�V���܂�
    ''' </summary>
    ''' <param name="teibetuRec">�@�ʐ����A�g���R�[�h</param>
    ''' <param name="isAll">True:���M�R�[�h,�A�g�w���R�[�h���X�V����</param>
    ''' <returns>�X�V����</returns>
    ''' <remarks></remarks>
    Public Function UpdateTeibetuRenkeiData(ByVal teibetuRec As TeibetuSeikyuuRenkeiRecord, _
                                            ByVal isAll As Boolean) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".UpdateTeibetuRenkeiData", _
                                                    teibetuRec, _
                                                    isAll)

        Dim intResult As Integer = 0
        Dim commandTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        ' �o�^�pSQL�ݒ�
        commandTextSb.Append(" UPDATE t_teibetu_seikyuu_renkei ")
        commandTextSb.Append(" SET ")
        commandTextSb.Append("        bunrui_cd = @BUNRUICD, ")
        If isAll = True Then
            commandTextSb.Append("    renkei_siji_cd    = @RENKEISIJICD, ")
            commandTextSb.Append("    sousin_jyky_cd    = @SOUSINJYKYCD, ")
        End If
        commandTextSb.Append("        upd_login_user_id = @UPDLOGINUSERID, ")
        commandTextSb.Append("        upd_datetime     = @UPDDATETIME ")
        commandTextSb.Append(" WHERE  kbn               = @KBN ")
        commandTextSb.Append(" AND    hosyousyo_no      = @HOSYOUSYONO ")
        commandTextSb.Append(" AND    SUBSTRING(bunrui_cd,1,2)         = SUBSTRING(@BUNRUICD,1,2)")
        commandTextSb.Append(" AND    gamen_hyouji_no   = @GAMENHYOUJINO ")

        cmdParams = New SqlParameter() { _
                SQLHelper.MakeParam("@KBN", SqlDbType.Char, 1, teibetuRec.Kbn), _
                SQLHelper.MakeParam("@HOSYOUSYONO", SqlDbType.VarChar, 10, teibetuRec.HosyousyoNo), _
                SQLHelper.MakeParam("@BUNRUICD", SqlDbType.VarChar, 3, teibetuRec.BunruiCd), _
                SQLHelper.MakeParam("@GAMENHYOUJINO", SqlDbType.Int, 4, teibetuRec.GamenHyoujiNo), _
                SQLHelper.MakeParam("@RENKEISIJICD", SqlDbType.Int, 4, teibetuRec.RenkeiSijiCd), _
                SQLHelper.MakeParam("@SOUSINJYKYCD", SqlDbType.Int, 4, teibetuRec.SousinJykyCd), _
                SQLHelper.MakeParam("@UPDLOGINUSERID", SqlDbType.VarChar, 30, teibetuRec.UpdLoginUserId), _
                SQLHelper.MakeParam("@UPDDATETIME", SqlDbType.DateTime, 16, DateTime.Now)}

        Dim debug As String = commandTextSb.ToString()

        ' �N�G�����s
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    commandTextSb.ToString(), _
                                    cmdParams)

        Return intResult

    End Function

#End Region
#Region "�X�V����A�g"
    ''' <summary>
    ''' �X�V����A�g�e�[�u���̓��e���m�F��f�[�^�𔽉f���܂�
    ''' </summary>
    ''' <param name="kousinRirekiRec">�X�V����A�g���R�[�h</param>
    ''' <param name="isUpdate">�@�ʃe�[�u�����X�V�����ꍇ��True</param>
    ''' <returns>���f����</returns>
    ''' <remarks></remarks>
    Public Function EditKousinRirekiRenkeiData(ByVal kousinRirekiRec As KousinRirekiRenkeiRecord, _
                                               Optional ByVal isUpdate As Boolean = True) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".EditKousinRirekiRenkeiData", _
                                                    kousinRirekiRec, _
                                                    isUpdate)

        ' �X�V����A�g�e�[�u���̑��݃`�F�b�N
        ' �p�����[�^
        Const paramUpdDateTime As String = "@UPDDATETIME"
        Const paramKubun As String = "@KUBUN"
        Const paramHosyousyoNo As String = "@HOSYOUSYONO"
        Const paramUpdKoumoku As String = "@UPDKOUMOKU"

        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append(" SELECT sousin_jyky_cd ")
        commandTextSb.Append(" FROM t_kousin_rireki_renkei WITH(UPDLOCK) ")
        commandTextSb.Append(" WHERE kbn = " & paramKubun)
        commandTextSb.Append(" AND   hosyousyo_no = " & paramHosyousyoNo)
        commandTextSb.Append(" AND   upd_koumoku  = " & paramUpdKoumoku)
        commandTextSb.Append(" AND   upd_datetime_rireki   = " & paramUpdDateTime)

        ' �p�����[�^�֐ݒ�
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(paramKubun, SqlDbType.Char, 1, kousinRirekiRec.Kbn), _
             SQLHelper.MakeParam(paramHosyousyoNo, SqlDbType.VarChar, 10, kousinRirekiRec.HosyousyoNo), _
             SQLHelper.MakeParam(paramUpdDateTime, SqlDbType.DateTime, 8, kousinRirekiRec.UpdDatetime), _
             SQLHelper.MakeParam(paramUpdKoumoku, SqlDbType.VarChar, 30, kousinRirekiRec.UpdKoumoku)}

        ' ���M�󋵃R�[�h
        Dim ret_value As Object = SQLHelper.ExecuteScalar(connStr, _
                                                          CommandType.Text, _
                                                          commandTextSb.ToString(), _
                                                          commandParameters)

        If ret_value Is Nothing Then
            ' �l���擾�ł��Ȃ��ꍇ�͐V�K��Insert����(�V�K�͒���Insert���\�b�h�����s��)
            kousinRirekiRec.SousinJykyCd = 0
            kousinRirekiRec.RenkeiSijiCd = 1
            Return InsertKousinRirekiRenkeiData(kousinRirekiRec)
        Else
            ' �X�V�����e�[�u����INSERT�ׁ݂̂̈A���݂��鎖�͗L�蓾�Ȃ����O�̈׍X�V���W�b�N��p��
            Return UpdateKousinRirekiRenkeiData(kousinRirekiRec, False)
        End If

    End Function

    ''' <summary>
    ''' �X�V����A�g�e�[�u���փf�[�^��o�^���܂�
    ''' </summary>
    ''' <param name="kousinRirekiRec">�X�V����A�g���R�[�h</param>
    ''' <returns>�o�^����</returns>
    ''' <remarks></remarks>
    Public Function InsertKousinRirekiRenkeiData(ByVal kousinRirekiRec As KousinRirekiRenkeiRecord) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".InsertKousinRirekiRenkeiData", _
                                                    kousinRirekiRec)

        Dim intResult As Integer = 0
        Dim commandTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        ' �o�^�pSQL�ݒ�
        commandTextSb.Append(" INSERT INTO t_kousin_rireki_renkei ( ")
        commandTextSb.Append("     upd_datetime_rireki, ")
        commandTextSb.Append("     kbn, ")
        commandTextSb.Append("     hosyousyo_no, ")
        commandTextSb.Append("     upd_koumoku, ")
        commandTextSb.Append("     renkei_siji_cd, ")
        commandTextSb.Append("     sousin_jyky_cd, ")
        commandTextSb.Append("     upd_login_user_id, ")
        commandTextSb.Append("     upd_datetime ")
        commandTextSb.Append(" ) VALUES ( ")
        commandTextSb.Append("     @UPDDATETIME, ")
        commandTextSb.Append("     @KBN, ")
        commandTextSb.Append("     @HOSYOUSYONO, ")
        commandTextSb.Append("     @UPDKOUMOKU, ")
        commandTextSb.Append("     @RENKEISIJICD, ")
        commandTextSb.Append("     @SOUSINJYKYCD, ")
        commandTextSb.Append("     @UPDLOGINUSERID, ")
        commandTextSb.Append("     @ADDDATETIME )")

        cmdParams = New SqlParameter() { _
                SQLHelper.MakeParam("@UPDDATETIME", SqlDbType.DateTime, 8, kousinRirekiRec.UpdDatetime), _
                SQLHelper.MakeParam("@KBN", SqlDbType.Char, 1, kousinRirekiRec.Kbn), _
                SQLHelper.MakeParam("@HOSYOUSYONO", SqlDbType.VarChar, 10, kousinRirekiRec.HosyousyoNo), _
                SQLHelper.MakeParam("@UPDKOUMOKU", SqlDbType.VarChar, 30, kousinRirekiRec.UpdKoumoku), _
                SQLHelper.MakeParam("@RENKEISIJICD", SqlDbType.Int, 4, kousinRirekiRec.RenkeiSijiCd), _
                SQLHelper.MakeParam("@SOUSINJYKYCD", SqlDbType.Int, 4, kousinRirekiRec.SousinJykyCd), _
                SQLHelper.MakeParam("@UPDLOGINUSERID", SqlDbType.VarChar, 30, kousinRirekiRec.UpdLoginUserId), _
                SQLHelper.MakeParam("@ADDDATETIME", SqlDbType.DateTime, 16, DateTime.Now)}

        ' �N�G�����s
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    commandTextSb.ToString(), _
                                    cmdParams)

        Return intResult

    End Function

    ''' <summary>
    ''' �X�V����A�g�e�[�u���̃f�[�^���X�V���܂�
    ''' </summary>
    ''' <param name="kousinRirekiRec">�X�V����A�g���R�[�h</param>
    ''' <param name="isAll">True:���M�R�[�h,�A�g�w���R�[�h���X�V����</param>
    ''' <returns>�X�V����</returns>
    ''' <remarks></remarks>
    Public Function UpdateKousinRirekiRenkeiData(ByVal kousinRirekiRec As KousinRirekiRenkeiRecord, _
                                                 ByVal isAll As Boolean) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".UpdateKousinRirekiRenkeiData", _
                                            kousinRirekiRec, _
                                            isAll)

        Dim intResult As Integer = 0
        Dim commandTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        ' �o�^�pSQL�ݒ�
        commandTextSb.Append(" UPDATE t_kousin_rireki_renkei  ")
        commandTextSb.Append(" SET ")
        If isAll = True Then
            commandTextSb.Append("     renkei_siji_cd = @RENKEISIJICD, ")
            commandTextSb.Append("     sousin_jyky_cd = @SOUSINJYKYCD, ")
        End If
        commandTextSb.Append("     upd_login_user_id = @UPDLOGINUSERID, ")
        commandTextSb.Append("     upd_datetime  = @UPDDATETIME ")

        commandTextSb.Append(" WHERE upd_datetime_rireki = @UPDDATETIME ")
        commandTextSb.Append(" AND   kbn          = @KBN  ")
        commandTextSb.Append(" AND   hosyousyo_no = @HOSYOUSYONO ")
        commandTextSb.Append(" AND   upd_koumoku  = @UPDKOUMOKU ")

        cmdParams = New SqlParameter() { _
                SQLHelper.MakeParam("@UPDDATETIME", SqlDbType.DateTime, 8, kousinRirekiRec.UpdDatetime), _
                SQLHelper.MakeParam("@KBN", SqlDbType.Char, 1, kousinRirekiRec.Kbn), _
                SQLHelper.MakeParam("@HOSYOUSYONO", SqlDbType.VarChar, 10, kousinRirekiRec.HosyousyoNo), _
                SQLHelper.MakeParam("@UPDKOUMOKU", SqlDbType.VarChar, 30, kousinRirekiRec.UpdKoumoku), _
                SQLHelper.MakeParam("@RENKEISIJICD", SqlDbType.Int, 4, kousinRirekiRec.RenkeiSijiCd), _
                SQLHelper.MakeParam("@SOUSINJYKYCD", SqlDbType.Int, 4, kousinRirekiRec.SousinJykyCd), _
                SQLHelper.MakeParam("@UPDLOGINUSERID", SqlDbType.VarChar, 30, kousinRirekiRec.UpdLoginUserId), _
                SQLHelper.MakeParam("@UPDDATETIME", SqlDbType.DateTime, 16, DateTime.Now)}

        ' �N�G�����s
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    commandTextSb.ToString(), _
                                    cmdParams)

        Return intResult

    End Function

#End Region
#End Region

#Region "�X�V�����e�[�u���o�^"
    ''' <summary>
    ''' �X�V�����e�[�u���ւ̓o�^���s���܂�
    ''' </summary>
    ''' <param name="kbn">�敪</param>
    ''' <param name="hosyousyoNo">�ۏ؏�NO</param>
    ''' <param name="chkTaisyou">�`�F�b�N���鍀�� 1:�{�喼 2:�����Z�� 3:������� 4:���l</param>
    ''' <param name="chkTaisyouNm">�`�F�b�N�Ώۂ̓��{�ꖼ</param>
    ''' <param name="chkData">�`�F�b�N����f�[�^</param>
    ''' <param name="loginUserId">���O�C�����[�U�[ID</param>
    ''' <remarks>�`�F�b�N�Ώۂ̍��ڂ��قȂ�ꍇ�̂݁A�X�V�����e�[�u���֓o�^���܂�</remarks>
    Public Function AddKoushinRireki(ByVal kbn As String, _
                                     ByVal hosyousyoNo As String, _
                                     ByVal chkTaisyou As Integer, _
                                     ByVal chkTaisyouNm As String, _
                                     ByVal chkData As String, _
                                     ByVal loginUserId As String) As Boolean

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".AddKoushinRireki", _
                                    kbn, _
                                    hosyousyoNo, _
                                    chkTaisyou, _
                                    chkTaisyouNm, _
                                    chkData, _
                                    loginUserId)

        Dim intResult As Integer = 0

        ' �A�g�e�[�u���Ɠ��������ׁA�X�V���t�����O�Ɏ擾����
        Dim nowDate As DateTime = DateTime.Now

        Dim commandTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        Dim updateItemName As String = ""
        Dim updateTableName As String = ""

        Select Case chkTaisyou
            Case enumItemName.SesyuMei
                updateItemName = "ISNULL(sesyu_mei,'')"
            Case enumItemName.Juusyo
                updateItemName = "ISNULL(bukken_jyuusyo1,'') + ISNULL(bukken_jyuusyo2,'')"
            Case enumItemName.KameitenCd
                updateItemName = "ISNULL(kameiten_cd,'')"
            Case enumItemName.TyousaKaisya
                updateItemName = "ISNULL(tys_kaisya_cd,'') + ISNULL(tys_kaisya_jigyousyo_cd,'')"
            Case enumItemName.Bikou
                updateItemName = "ISNULL(bikou,'')"
            Case enumItemName.Bikou2
                updateItemName = "ISNULL(bikou2,'')"
            Case Else
                Exit Function
        End Select

        commandTextSb.Append(" INSERT INTO t_kousin_rireki ")
        commandTextSb.Append(" SELECT ")
        commandTextSb.Append("    @UPDDATETIME As upd_datetime, ")
        commandTextSb.Append("    kbn, ")
        commandTextSb.Append("    hosyousyo_no, ")
        commandTextSb.Append("    @UPDKOUMOKU As upd_koumoku, ")
        commandTextSb.Append("    {0} As upd_mae_atai, ")
        commandTextSb.Append("    @CHKDATA As upd_go_atai, ")
        commandTextSb.Append("    @USERID As kousinsya ")
        commandTextSb.Append(" FROM ")
        commandTextSb.Append("     t_jiban WITH (READCOMMITTED) ")
        commandTextSb.Append(" WHERE ")
        commandTextSb.Append("     kbn = @KBN AND ")
        commandTextSb.Append("     hosyousyo_no = @HOSYOUSYONO AND ")
        commandTextSb.Append("     upd_datetime IS NOT NULL AND ")
        commandTextSb.Append("     {1} COLLATE Japanese_CS_AS_KS_WS  <> @CHKDATA COLLATE Japanese_CS_AS_KS_WS ")

        cmdParams = New SqlParameter() { _
                        SQLHelper.MakeParam("@UPDDATETIME", SqlDbType.DateTime, 8, nowDate), _
                        SQLHelper.MakeParam("@KBN", SqlDbType.Char, 1, kbn), _
                        SQLHelper.MakeParam("@HOSYOUSYONO", SqlDbType.VarChar, 10, hosyousyoNo), _
                        SQLHelper.MakeParam("@UPDKOUMOKU", SqlDbType.VarChar, 30, chkTaisyouNm), _
                        SQLHelper.MakeParam("@CHKDATA", SqlDbType.VarChar, 512, chkData), _
                        SQLHelper.MakeParam("@USERID", SqlDbType.VarChar, 30, loginUserId)}

        Dim sql As String = commandTextSb.ToString()

        ' �N�G�����s
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    String.Format(commandTextSb.ToString(), updateItemName, updateItemName), _
                                    cmdParams)

        If intResult >= 1 Then
            ' �ύX���������̂ŁA�A�g�e�[�u���ɂ��o�^����
            Dim kousinRirekiRec As New KousinRirekiRenkeiRecord

            kousinRirekiRec.UpdDatetime = nowDate
            kousinRirekiRec.Kbn = kbn
            kousinRirekiRec.HosyousyoNo = hosyousyoNo
            kousinRirekiRec.UpdKoumoku = chkTaisyouNm
            kousinRirekiRec.RenkeiSijiCd = 1
            kousinRirekiRec.SousinJykyCd = 0
            kousinRirekiRec.UpdDatetime1 = nowDate
            kousinRirekiRec.UpdLoginUserId = loginUserId

            intResult = EditKousinRirekiRenkeiData(kousinRirekiRec)

            ' �o�^�Ɏ��s�����ꍇFalse
            If intResult <= 0 Then
                Return False
            End If
        End If

        Return True

    End Function

#End Region

#Region "�����p�r���Z�z�擾"
    ''' <summary>
    ''' �����p�r���Z�z���擾���܂�
    ''' </summary>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <param name="intTatemonoYouto">�����p�r�R�[�h</param>
    ''' <returns>�����p�r���Z�z</returns>
    ''' <remarks>�����p�r�Q�`�X�܂ł̉��Z�z���擾���܂��B�擾�����͌Ăяo�����ōs���Ă��������B<br/>
    '''          �i���i�R�[�h�ɂ����Z���f���j</remarks>
    Public Function GetTatemonoYoutoKasangaku(ByVal strKameitenCd As String, _
                                              ByVal intTatemonoYouto As Integer) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTatemonoYoutoKasangaku", _
                                                    strKameitenCd, _
                                                    intTatemonoYouto)

        ' �n�՘A�g�e�[�u���̑��݃`�F�b�N
        ' �p�����[�^
        Const paramKameitenCd As String = "@KAMEITENCD"

        ' �����p�r��2�`9�̏ꍇ�̂݁A���Z�z���擾����
        If intTatemonoYouto < 2 Or intTatemonoYouto > 9 Then
            Return 0
        End If

        Dim commandTextSb As New StringBuilder()
        commandTextSb.Append(" SELECT ISNULL(kasangaku" & intTatemonoYouto.ToString() & ",0) As kasangaku ")
        commandTextSb.Append(" FROM m_tatemono_youto_kasangaku WITH (READCOMMITTED) ")
        commandTextSb.Append(" WHERE kameiten_cd = " & strKameitenCd)

        ' �p�����[�^�֐ݒ�
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(paramKameitenCd, SqlDbType.VarChar, 5, strKameitenCd)}

        ' �����p�r���Z�z
        Dim retValue As Object = SQLHelper.ExecuteScalar(connStr, _
                                                          CommandType.Text, _
                                                          commandTextSb.ToString(), _
                                                          commandParameters)

        If retValue Is Nothing Then
            Return 0
        End If

        Return retValue

    End Function
#End Region

#Region "�H���R�s�[�����`�F�b�N�p"
    ''' <summary>
    ''' �H���R�s�[�����`�F�b�N�p
    ''' </summary>
    ''' <param name="kbn">�敪</param>
    ''' <param name="hosyousyoNo">�ۏ؏�NO</param>   
    ''' <returns>�H���R�s�[�����`�F�b�N�p�f�[�^�e�[�u��</returns>
    ''' <remarks></remarks>
    Public Function GetKoujiCopyCheckData(ByVal kbn As String, _
                                 ByVal hosyousyoNo As String) As JibanDataSet.KoujiCopyCheckTableDataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetJibanData", _
                                            kbn, _
                                            hosyousyoNo)

        ' �p�����[�^
        Const strParamKbn As String = "@KBN"
        Const strParamHosyousyoNo As String = "@HOSYOUSYONO"

        ' �p�����[�^�֐ݒ�
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamKbn, SqlDbType.Char, 1, kbn), _
             SQLHelper.MakeParam(strParamHosyousyoNo, SqlDbType.VarChar, 10, hosyousyoNo)}

        Dim commandText As String = " select " & _
                                    "   j.kbn " & _
                                    "   , j.hosyousyo_no " & _
                                    "   , syouninsya_cd " & _
                                    "   , koj_siyou_kakunin " & _
                                    "   , koj_siyou_kakunin_date " & _
                                    "   , koj_gaisya_cd " & _
                                    "   , koj_gaisya_seikyuu_umu " & _
                                    "   , kairy_koj_syubetu " & _
                                    "   , kairy_koj_kanry_yotei_date " & _
                                    "   , kairy_koj_sokuhou_tyk_date " & _
                                    "   , t.syouhin_cd " & _
                                    "   , t.uri_gaku " & _
                                    "   , t.siire_gaku  " & _
                                    "   , t.seikyuu_umu " & _
                                    "   , t.uri_keijyou_date  " & _
                                    "   , t.hattyuusyo_gaku  " & _
                                    " from " & _
                                    "   t_jiban j  " & _
                                    "   left outer join t_teibetu_seikyuu t  " & _
                                    "     on t.kbn = j.kbn  " & _
                                    "     and t.hosyousyo_no = j.hosyousyo_no  " & _
                                    "     and t.bunrui_cd = '" & EarthConst.SOUKO_CD_KAIRYOU_KOUJI & "'  " & _
                                    " where " & _
                                    "   j.kbn = " & strParamKbn & _
                                    "   and j.hosyousyo_no = " & strParamHosyousyoNo

        ' �f�[�^�̎擾
        Dim JibanDataSet As New JibanDataSet()

        SQLHelper.FillDataset(connStr, CommandType.Text, commandText, _
            JibanDataSet, JibanDataSet.KoujiCopyCheckTable.TableName, commandParameters)

        Dim KoujiCopyCheckTable As JibanDataSet.KoujiCopyCheckTableDataTable = JibanDataSet.KoujiCopyCheckTable

        Return KoujiCopyCheckTable

    End Function
#End Region

    ''' <summary>
    ''' ���`�[���s�Ή���
    ''' �폜�����g���K�[�p�Ƀ��[�U�[ID�����[�J���ꎞ�e�[�u���Ɋi�[���邽�߂�SQL���𐶐�
    ''' </summary>
    ''' <param name="strUserId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CreateUserInfoTempTableSQL(ByVal strUserId As String) As String

        Dim commandTextSb As New StringBuilder()

        Const strParamTableName As String = "#TEMP_USER_INFO_FOR_TRIGGER"

        '�e���|�����e�[�u���̍쐬
        commandTextSb.Append(" IF object_id('tempdb.." & strParamTableName & "') is null ")
        commandTextSb.Append(" BEGIN ")

        commandTextSb.Append("    CREATE TABLE ")
        commandTextSb.Append("           " & strParamTableName & " ")
        commandTextSb.Append("          (login_user_id  VARCHAR(30) ")
        commandTextSb.Append("          ,syori_datetime DATETIME ) ")

        commandTextSb.Append("    INSERT INTO " & strParamTableName & " ")
        commandTextSb.Append("          (login_user_id ")
        commandTextSb.Append("          ,syori_datetime ) ")
        commandTextSb.Append("    VALUES ")
        commandTextSb.Append("          ( '" & strUserId & "'")
        commandTextSb.Append("          ,GETDATE() ) ")

        commandTextSb.Append(" END ")

        Return commandTextSb.ToString()

    End Function

End Class

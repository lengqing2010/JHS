Imports System.Data.SqlClient
Imports System.Text
Imports Itis.ApplicationBlocks.Data.SQLHelper
''' <summary>
''' ¿æ}X^
''' </summary>
''' <history>
''' <para>2010/05/24@nR(åA)@VKì¬</para>
''' </history>
Public Class SeikyuuSakiMasterDataAccess

    Private connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>
    ''' ¿æo^`}X^
    ''' </summary>
    Public Function SelSeikyuuSakiTourokuHinagataInfo() As DataTable

        ' DataSetCX^XÌ¶¬
        Dim dsDataSet As New DataSet

        'SQL¶Ì¶¬
        Dim commandTextSb As New StringBuilder

        'SQL¶
        With commandTextSb
            .AppendLine(" SELECT   ")
            .AppendLine("   seikyuu_saki_brc   ")
            .AppendLine("   ,hyouji_naiyou   ")
            .AppendLine(" FROM  ")
            .AppendLine("   m_seikyuu_saki_touroku_hinagata WITH (READCOMMITTED)  ")
            .AppendLine(" ORDER BY seikyuu_saki_brc ")
        End With

        ' õÀs
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dsDataSet")

        'ßé
        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>
    ''' g£¼Ì}X^
    ''' </summary>
    ''' <param name="strSyubetu">¼ÌíÊ</param>
    Public Function SelKakutyouInfo(ByVal strSyubetu As String) As DataTable

        ' DataSetCX^XÌ¶¬
        Dim dsDataSet As New DataSet

        'SQL¶Ì¶¬
        Dim commandTextSb As New StringBuilder

        'p[^i[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL¶
        With commandTextSb
            .AppendLine(" SELECT   ")
            .AppendLine("   code   ")
            .AppendLine("   ,meisyou   ")
            .AppendLine(" FROM  ")
            .AppendLine("   m_kakutyou_meisyou WITH (READCOMMITTED)  ")
            .AppendLine(" WHERE  meisyou_syubetu=@meisyou_syubetu  ")
            .AppendLine(" ORDER BY hyouji_jyun ")
        End With

        'p[^ÌÝè
        paramList.Add(MakeParam("@meisyou_syubetu", SqlDbType.VarChar, 3, strSyubetu))

        ' õÀs
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    "dsDataSet", paramList.ToArray)

        'ßé
        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>
    ''' ¿æîñÌæ¾
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelSeikyuuSakiInfo(ByVal strSeikyuuSakiCd As String, ByVal strSeikyuuSakiBrc As String, ByVal SeikyuuSakiKbn As String) As SeikyuuSakiDataSet.m_seikyuu_sakiDataTable

        ' DataSetCX^XÌ¶¬
        Dim dsDataSet As New SeikyuuSakiDataSet

        'SQL¶Ì¶¬
        Dim commandTextSb As New StringBuilder

        'p[^i[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL¶
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   MSS.seikyuu_saki_cd, ")     '¿æR[h
            .AppendLine("   MSS.seikyuu_saki_brc, ")        '¿æ}Ô
            .AppendLine("   MSS.seikyuu_saki_kbn, ")        '¿ææª
            .AppendLine("   MSS.torikesi, ")        'æÁ
            .AppendLine("   MSS.skk_jigyousyo_cd, ")        'VïvÆR[h
            .AppendLine("   MSS.kyuu_seikyuu_saki_cd, ")        '¿æR[h

            .AppendLine("   MSS.skysy_soufu_jyuusyo1, ")        '¿tæZ1
            .AppendLine("   MSS.skysy_soufu_jyuusyo2, ")        '¿tæZ2
            .AppendLine("   MSS.skysy_soufu_yuubin_no, ")        '¿tæXÖÔ
            .AppendLine("   MSS.skysy_soufu_tel_no, ")        '¿tædbÔ
            .AppendLine("   MSS.skysy_soufu_fax_no, ")        '¿tæFAXÔ

            .AppendLine("   MSS.tantousya_mei, ")       'SÒ¼
            .AppendLine("   MSS.seikyuusyo_inji_bukken_mei_flg, ")      '¿ó¨¼tO
            .AppendLine("   MSS.nyuukin_kouza_no, ")        'üàûÀÔ
            .AppendLine("   MSS.seikyuu_sime_date, ")       '¿÷ßú
            .AppendLine("   MSS.senpou_seikyuu_sime_date, ")        'æû¿÷ßú
            .AppendLine("   MSS.tyk_koj_seikyuu_timing_flg, ")      '¼H¿^C~OtO
            .AppendLine("   MSS.sousai_flg, ")      'EtO
            .AppendLine("   MSS.kaisyuu_yotei_gessuu, ")        'ñû\è
            .AppendLine("   MSS.kaisyuu_yotei_date, ")      'ñû\èú
            .AppendLine("   MSS.seikyuusyo_hittyk_date, ")      '¿Kú
            .AppendLine("   MSS.kaisyuu1_syubetu1, ")       'ñû1íÊ1
            .AppendLine("   MSS.kaisyuu1_wariai1, ")        'ñû11
            .AppendLine("   MSS.kaisyuu1_tegata_site_gessuu, ")     'ñû1è`TCg
            .AppendLine("   MSS.kaisyuu1_tegata_site_date, ")       'ñû1è`TCgú
            .AppendLine("   MSS.kaisyuu1_seikyuusyo_yousi, ")       'ñû1¿p
            .AppendLine("   MSS.kaisyuu1_syubetu2, ")       'ñû1íÊ2
            .AppendLine("   MSS.kaisyuu1_wariai2, ")        'ñû12
            .AppendLine("   MSS.kaisyuu1_syubetu3, ")       'ñû1íÊ3
            .AppendLine("   MSS.kaisyuu1_wariai3, ")        'ñû13
            .AppendLine("   MSS.kaisyuu_kyoukaigaku, ")     'ñû«Ez
            .AppendLine("   MSS.kaisyuu2_syubetu1, ")       'ñû2íÊ1
            .AppendLine("   MSS.kaisyuu2_wariai1, ")        'ñû21
            .AppendLine("   MSS.kaisyuu2_tegata_site_gessuu, ")     'ñû2è`TCg
            .AppendLine("   MSS.kaisyuu2_tegata_site_date, ")       'ñû2è`TCgú
            .AppendLine("   MSS.kaisyuu2_seikyuusyo_yousi, ")       'ñû2¿p
            .AppendLine("   MSS.kaisyuu2_syubetu2, ")       'ñû2íÊ2
            .AppendLine("   MSS.kaisyuu2_wariai2, ")        'ñû22
            .AppendLine("   MSS.kaisyuu2_syubetu3, ")       'ñû2íÊ3
            .AppendLine("   MSS.kaisyuu2_wariai3, ")        'ñû23
            .AppendLine("   MSS.add_login_user_id, ")       'o^OC[U[ID
            .AppendLine("   MSS.add_datetime, ")        'o^ú
            .AppendLine("   MSS.upd_login_user_id, ")       'XVOC[U[ID
            .AppendLine("   MSS.upd_datetime, ")        'XVú
            .AppendLine("   MSJ.skk_jigyousyo_mei, ")        'Æ¼
            .AppendLine("   VSSI.seikyuu_saki_mei ")        '¿æ¼

            '20100925@¼ñæR[hA¼ñæ¼@ÇÁ@nR«««
            .AppendLine("   ,MSS.nayose_saki_cd ")        '¼ñæR[h
            .AppendLine("   ,MY.nayose_saki_name1 ")        '¼ñæ¼
            '20100925@¼ñæR[hA¼ñæ¼@ÇÁ@nRªªª

            '==================2011/06/16 Ô´  ÇÁ Jn«========================== 
            .AppendLine("   ,MSS.kessanji_nidosime_flg ")   'Zñx÷ßtO
            '==================2011/06/16 Ô´  ÇÁ I¹ª==========================

            '==================2012/05/17 Ô´ 407553ÌÎ ÇÁ«========================== 
            .AppendLine("   ,MSS.koufuri_ok_flg ") 'ûUOKtO
            .AppendLine("   ,MSS.tougou_tokuisaki_cd ") 'ïv¾Óæº°ÄÞ
            .AppendLine("   ,MSS.anzen_kaihi_en ") 'ÀS¦Íïï_~
            .AppendLine("   ,MSS.anzen_kaihi_wari ") 'ÀS¦Íïï_
            .AppendLine("   ,MSS.bikou ") 'õl
            '==================2012/05/17 Ô´ 407553ÌÎ ÇÁª==========================
            '2013/5/29 ¿ÌâsxXR[hÁÉº¤EarthüC -----------------------«
            .AppendLine("   ,MSS.ginkou_siten_cd ") 'âsxXR[h
            '2013/5/29 ¿ÌâsxXR[hÁÉº¤EarthüC -----------------------ª
            .AppendLine("   ,ISNULL(MSS.kyouryoku_kaihi_taisyou,'') as kyouryoku_kaihi_taisyou ")
            .AppendLine("FROM ")
            .AppendLine("   m_seikyuu_saki MSS WITH (READCOMMITTED) ")      '¿æ}X^
            .AppendLine("LEFT JOIN ")
            .AppendLine("   m_sinkaikei_jigyousyo MSJ WITH (READCOMMITTED) ")   'VïvÆ}X^
            .AppendLine("ON ")
            .AppendLine("   MSS.skk_jigyousyo_cd = MSJ.skk_jigyousyo_cd ")
            .AppendLine("LEFT JOIN ")
            .AppendLine("   v_seikyuu_saki_info VSSI WITH (READCOMMITTED) ")   '¿æîñ
            .AppendLine("ON ")
            .AppendLine("   MSS.seikyuu_saki_cd = VSSI.seikyuu_saki_cd ")
            .AppendLine("AND ")
            .AppendLine("   MSS.seikyuu_saki_brc = VSSI.seikyuu_saki_brc ")
            .AppendLine("AND ")
            .AppendLine("   MSS.seikyuu_saki_kbn = VSSI.seikyuu_saki_kbn ")

            '20100925@¼ñæR[hA¼ñæ¼@ÇÁ@nR«««
            .AppendLine("LEFT JOIN ")
            .AppendLine("   m_yosinkanri MY WITH (READCOMMITTED) ")   '^MÇ}X^
            .AppendLine("ON ")
            .AppendLine("   MSS.nayose_saki_cd = MY.nayose_saki_cd ")
            '20100925@¼ñæR[hA¼ñæ¼@ÇÁ@nRªªª

            .AppendLine("WHERE ")
            .AppendLine("   MSS.seikyuu_saki_cd = @seikyuu_saki_cd ")
            .AppendLine("AND ")
            .AppendLine("   MSS.seikyuu_saki_brc = @seikyuu_saki_brc ")
            .AppendLine("AND ")
            .AppendLine("   MSS.seikyuu_saki_kbn = @seikyuu_saki_kbn ")
        End With

        'p[^ÌÝè
        paramList.Add(MakeParam("@seikyuu_saki_cd", SqlDbType.VarChar, 5, strSeikyuuSakiCd))
        paramList.Add(MakeParam("@seikyuu_saki_brc", SqlDbType.VarChar, 2, strSeikyuuSakiBrc))
        paramList.Add(MakeParam("@seikyuu_saki_kbn", SqlDbType.VarChar, 1, SeikyuuSakiKbn))

        ' õÀs
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet,dsDataSet.m_seikyuu_saki.TableName, paramList.ToArray)

        'ßé
        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>
    ''' ¿æ}X^o^
    ''' </summary>
    ''' <param name="dtSeikyuuSaki">o^f[^</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsSeikyuuSaki(ByVal dtSeikyuuSaki As SeikyuuSakiDataSet.m_seikyuu_sakiDataTable) As Boolean

        'SQL¶Ì¶¬
        Dim commandTextSb As New StringBuilder

        'p[^i[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL¶
        With commandTextSb
            .AppendLine("INSERT INTO ")
            .AppendLine("   m_seikyuu_saki WITH (UPDLOCK) ")
            .AppendLine("( ")
            .AppendLine("   seikyuu_saki_cd, ") '¿æR[h
            .AppendLine("   seikyuu_saki_brc, ")    '¿æ}Ô
            .AppendLine("   seikyuu_saki_kbn, ")    '¿ææª
            .AppendLine("   torikesi, ")    'æÁ
            .AppendLine("   skk_jigyousyo_cd, ")    'VïvÆR[h

            .AppendLine("   kyuu_seikyuu_saki_cd, ")    '¿æR[h

            .AppendLine("   skysy_soufu_jyuusyo1, ")    '¿tæZ1
            .AppendLine("   skysy_soufu_jyuusyo2, ")    '¿tæZ2
            .AppendLine("   skysy_soufu_yuubin_no, ")    '¿tæXÖÔ
            .AppendLine("   skysy_soufu_tel_no, ")    '¿tædbÔ
            .AppendLine("   skysy_soufu_fax_no, ")    '¿tæFAXÔ

            .AppendLine("   tantousya_mei, ")   'SÒ¼
            .AppendLine("   seikyuusyo_inji_bukken_mei_flg, ")  '¿ó¨¼tO
            .AppendLine("   nyuukin_kouza_no, ")    'üàûÀÔ
            .AppendLine("   seikyuu_sime_date, ")   '¿÷ßú
            .AppendLine("   senpou_seikyuu_sime_date, ")    'æû¿÷ßú
            .AppendLine("   tyk_koj_seikyuu_timing_flg, ")  '¼H¿^C~OtO
            .AppendLine("   sousai_flg, ")  'EtO
            .AppendLine("   kaisyuu_yotei_gessuu, ")    'ñû\è
            .AppendLine("   kaisyuu_yotei_date, ")  'ñû\èú
            .AppendLine("   seikyuusyo_hittyk_date, ")  '¿Kú
            .AppendLine("   kaisyuu1_syubetu1, ")   'ñû1íÊ1
            .AppendLine("   kaisyuu1_wariai1, ")    'ñû11
            .AppendLine("   kaisyuu1_tegata_site_gessuu, ") 'ñû1è`TCg
            .AppendLine("   kaisyuu1_tegata_site_date, ")   'ñû1è`TCgú
            .AppendLine("   kaisyuu1_seikyuusyo_yousi, ")   'ñû1¿p
            .AppendLine("   kaisyuu1_syubetu2, ")   'ñû1íÊ2
            .AppendLine("   kaisyuu1_wariai2, ")    'ñû12
            .AppendLine("   kaisyuu1_syubetu3, ")   'ñû1íÊ3
            .AppendLine("   kaisyuu1_wariai3, ")    'ñû13
            .AppendLine("   kaisyuu_kyoukaigaku, ") 'ñû«Ez
            .AppendLine("   kaisyuu2_syubetu1, ")   'ñû2íÊ1
            .AppendLine("   kaisyuu2_wariai1, ")    'ñû21
            .AppendLine("   kaisyuu2_tegata_site_gessuu, ") 'ñû2è`TCg
            .AppendLine("   kaisyuu2_tegata_site_date, ")   'ñû2è`TCgú
            .AppendLine("   kaisyuu2_seikyuusyo_yousi, ")   'ñû2¿p
            .AppendLine("   kaisyuu2_syubetu2, ")   'ñû2íÊ2
            .AppendLine("   kaisyuu2_wariai2, ")    'ñû22
            .AppendLine("   kaisyuu2_syubetu3, ")   'ñû2íÊ3
            .AppendLine("   kaisyuu2_wariai3, ")    'ñû23

            .AppendLine("   add_login_user_id, ")   'o^OC[U[ID
            .AppendLine("   add_datetime ")    'o^ú

            '20100925@¼ñæR[hA¼ñæ¼@ÇÁ@nR«««
            .AppendLine("   ,nayose_saki_cd ")    '¼ñæR[h
            '20100925@¼ñæR[hA¼ñæ¼@ÇÁ@nRªªª

            '==================2011/06/16 Ô´  ÇÁ Jn«========================== 
            .AppendLine("   ,kessanji_nidosime_flg ")   'Zñx÷ßtO
            '==================2011/06/16 Ô´  ÇÁ I¹ª==========================

            '==================2012/05/17 Ô´ 407553ÌÎ ÇÁ«========================== 
            .AppendLine("   ,koufuri_ok_flg ") 'ûUOKtO
            .AppendLine("   ,tougou_tokuisaki_cd ") 'ïv¾Óæº°ÄÞ
            .AppendLine("   ,anzen_kaihi_en ") 'ÀS¦Íïï_~
            .AppendLine("   ,anzen_kaihi_wari ") 'ÀS¦Íïï_
            .AppendLine("   ,bikou ") 'õl
            '==================2012/05/17 Ô´ 407553ÌÎ ÇÁª==========================

            '2013/5/29 ¿ÌâsxXR[hÁÉº¤EarthüC -----------------------«
            .AppendLine("   ,ginkou_siten_cd ")           'âsxXR[h
            '2013/5/29 ¿ÌâsxXR[hÁÉº¤EarthüC -----------------------ª
            .AppendLine("   ,kyouryoku_kaihi_taisyou ")
            .AppendLine(") ")
            .AppendLine("SELECT ")
            .AppendLine("   @seikyuu_saki_cd, ")    '¿æR[h
            .AppendLine("   @seikyuu_saki_brc, ")   '¿æ}Ô
            .AppendLine("   @seikyuu_saki_kbn, ")   '¿ææª
            .AppendLine("   @torikesi, ")   'æÁ
            .AppendLine("   @skk_jigyousyo_cd, ")   'VïvÆR[h

            .AppendLine("   @kyuu_seikyuu_saki_cd, ")   '¿æR[h

            .AppendLine("   @skysy_soufu_jyuusyo1, ")   '¿tæZ
            .AppendLine("   @skysy_soufu_jyuusyo2, ")   '¿tæZ
            .AppendLine("   @skysy_soufu_yuubin_no, ")   '¿tæXÖÔ
            .AppendLine("   @skysy_soufu_tel_no, ")   '¿tædbÔ
            .AppendLine("   @skysy_soufu_fax_no, ")   '¿tæFAXÔ

            .AppendLine("   @tantousya_mei, ")  'SÒ¼
            .AppendLine("   @seikyuusyo_inji_bukken_mei_flg, ") '¿ó¨¼tO
            .AppendLine("   @nyuukin_kouza_no, ")   'üàûÀÔ
            .AppendLine("   @seikyuu_sime_date, ")  '¿÷ßú
            .AppendLine("   @senpou_seikyuu_sime_date, ")   'æû¿÷ßú
            .AppendLine("   @tyk_koj_seikyuu_timing_flg, ") '¼H¿^C~OtO
            .AppendLine("   @sousai_flg, ") 'EtO
            .AppendLine("   @kaisyuu_yotei_gessuu, ")   'ñû\è
            .AppendLine("   @kaisyuu_yotei_date, ") 'ñû\èú
            .AppendLine("   @seikyuusyo_hittyk_date, ") '¿Kú
            .AppendLine("   @kaisyuu1_syubetu1, ")  'ñû1íÊ1
            .AppendLine("   @kaisyuu1_wariai1, ")   'ñû11
            .AppendLine("   @kaisyuu1_tegata_site_gessuu, ")    'ñû1è`TCg
            .AppendLine("   @kaisyuu1_tegata_site_date, ")  'ñû1è`TCgú
            .AppendLine("   @kaisyuu1_seikyuusyo_yousi, ")  'ñû1¿p
            .AppendLine("   @kaisyuu1_syubetu2, ")  'ñû1íÊ2
            .AppendLine("   @kaisyuu1_wariai2, ")   'ñû12
            .AppendLine("   @kaisyuu1_syubetu3, ")  'ñû1íÊ3
            .AppendLine("   @kaisyuu1_wariai3, ")   'ñû13
            .AppendLine("   @kaisyuu_kyoukaigaku, ")    'ñû«Ez
            .AppendLine("   @kaisyuu2_syubetu1, ")  'ñû2íÊ1
            .AppendLine("   @kaisyuu2_wariai1, ")   'ñû21
            .AppendLine("   @kaisyuu2_tegata_site_gessuu, ")    'ñû2è`TCg
            .AppendLine("   @kaisyuu2_tegata_site_date, ")  'ñû2è`TCgú
            .AppendLine("   @kaisyuu2_seikyuusyo_yousi, ")  'ñû2¿p
            .AppendLine("   @kaisyuu2_syubetu2, ")  'ñû2íÊ2
            .AppendLine("   @kaisyuu2_wariai2, ")   'ñû22
            .AppendLine("   @kaisyuu2_syubetu3, ")  'ñû2íÊ3
            .AppendLine("   @kaisyuu2_wariai3, ")   'ñû23
            .AppendLine("   @add_login_user_id, ")  'o^OC[U[ID
            .AppendLine("   GETDATE() ")   'o^ú
            '20100925@¼ñæR[hA¼ñæ¼@ÇÁ@nR«««
            .AppendLine("   ,@nayose_saki_cd ")    '¼ñæR[h
            '20100925@¼ñæR[hA¼ñæ¼@ÇÁ@nRªªª

            '==================2011/06/16 Ô´  ÇÁ Jn«========================== 
            .AppendLine("   ,@kessanji_nidosime_flg ")   'Zñx÷ßtO
            '==================2011/06/16 Ô´  ÇÁ I¹ª==========================

            '==================2012/05/17 Ô´ 407553ÌÎ ÇÁ«========================== 
            .AppendLine("   ,@koufuri_ok_flg ") 'ûUOKtO
            .AppendLine("   ,@tougou_tokuisaki_cd ") 'ïv¾Óæº°ÄÞ
            .AppendLine("   ,@anzen_kaihi_en ") 'ÀS¦Íïï_~
            .AppendLine("   ,@anzen_kaihi_wari ") 'ÀS¦Íïï_
            .AppendLine("   ,@bikou ") 'õl
            '==================2012/05/17 Ô´ 407553ÌÎ ÇÁª==========================
            '2013/5/29 ¿ÌâsxXR[hÁÉº¤EarthüC -----------------------«
            .AppendLine("   ,@ginkou_siten_cd ")           'âsxXR[h
            '2013/5/29 ¿ÌâsxXR[hÁÉº¤EarthüC -----------------------ª
            .AppendLine("   ,@kyouryoku_kaihi_taisyou ")
        End With

        'p[^ÌÝè
        With paramList
            '¿æR[h
            .Add(MakeParam("@seikyuu_saki_cd", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).seikyuu_saki_cd = "", DBNull.Value, dtSeikyuuSaki(0).seikyuu_saki_cd)))
            '¿æ}Ô
            .Add(MakeParam("@seikyuu_saki_brc", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).seikyuu_saki_brc = "", DBNull.Value, dtSeikyuuSaki(0).seikyuu_saki_brc)))
            '¿ææª
            .Add(MakeParam("@seikyuu_saki_kbn", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).seikyuu_saki_kbn = "", DBNull.Value, dtSeikyuuSaki(0).seikyuu_saki_kbn)))
            'æÁ
            .Add(MakeParam("@torikesi", SqlDbType.VarChar, 255, dtSeikyuuSaki(0).torikesi))
            'VïvÆR[h
            .Add(MakeParam("@skk_jigyousyo_cd", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).skk_jigyousyo_cd = "", DBNull.Value, dtSeikyuuSaki(0).skk_jigyousyo_cd)))

            '¿æR[h
            .Add(MakeParam("@kyuu_seikyuu_saki_cd", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kyuu_seikyuu_saki_cd = "", DBNull.Value, dtSeikyuuSaki(0).kyuu_seikyuu_saki_cd)))

            '¿tæZ1
            .Add(MakeParam("@skysy_soufu_jyuusyo1", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).skysy_soufu_jyuusyo1 = "", DBNull.Value, dtSeikyuuSaki(0).skysy_soufu_jyuusyo1)))
            '¿tæZ2
            .Add(MakeParam("@skysy_soufu_jyuusyo2", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).skysy_soufu_jyuusyo2 = "", DBNull.Value, dtSeikyuuSaki(0).skysy_soufu_jyuusyo2)))
            '¿tæXÖÔ
            .Add(MakeParam("@skysy_soufu_yuubin_no", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).skysy_soufu_yuubin_no = "", DBNull.Value, dtSeikyuuSaki(0).skysy_soufu_yuubin_no)))
            '¿tædbÔ
            .Add(MakeParam("@skysy_soufu_tel_no", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).skysy_soufu_tel_no = "", DBNull.Value, dtSeikyuuSaki(0).skysy_soufu_tel_no)))
            '¿tæFAXÔ
            .Add(MakeParam("@skysy_soufu_fax_no", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).skysy_soufu_fax_no = "", DBNull.Value, dtSeikyuuSaki(0).skysy_soufu_fax_no)))

            'SÒ¼
            .Add(MakeParam("@tantousya_mei", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).tantousya_mei = "", DBNull.Value, dtSeikyuuSaki(0).tantousya_mei)))
            '¿ó¨¼tO
            .Add(MakeParam("@seikyuusyo_inji_bukken_mei_flg", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).seikyuusyo_inji_bukken_mei_flg = "", DBNull.Value, dtSeikyuuSaki(0).seikyuusyo_inji_bukken_mei_flg)))
            'üàûÀÔ
            .Add(MakeParam("@nyuukin_kouza_no", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).nyuukin_kouza_no = "", DBNull.Value, dtSeikyuuSaki(0).nyuukin_kouza_no)))
            '¿÷ßú
            .Add(MakeParam("@seikyuu_sime_date", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).seikyuu_sime_date = "", DBNull.Value, dtSeikyuuSaki(0).seikyuu_sime_date)))
            'æû¿÷ßú
            .Add(MakeParam("@senpou_seikyuu_sime_date", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).senpou_seikyuu_sime_date = "", DBNull.Value, dtSeikyuuSaki(0).senpou_seikyuu_sime_date)))
            '¼H¿^C~OtO
            .Add(MakeParam("@tyk_koj_seikyuu_timing_flg", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).tyk_koj_seikyuu_timing_flg = "", DBNull.Value, dtSeikyuuSaki(0).tyk_koj_seikyuu_timing_flg)))
            'EtO
            .Add(MakeParam("@sousai_flg", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).sousai_flg = "", DBNull.Value, dtSeikyuuSaki(0).sousai_flg)))
            'ñû\è
            .Add(MakeParam("@kaisyuu_yotei_gessuu", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu_yotei_gessuu = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu_yotei_gessuu)))
            'ñû\èú
            .Add(MakeParam("@kaisyuu_yotei_date", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu_yotei_date = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu_yotei_date)))
            '¿Kú
            .Add(MakeParam("@seikyuusyo_hittyk_date", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).seikyuusyo_hittyk_date = "", DBNull.Value, dtSeikyuuSaki(0).seikyuusyo_hittyk_date)))
            'ñû1íÊ1
            .Add(MakeParam("@kaisyuu1_syubetu1", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu1_syubetu1 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu1_syubetu1)))
            'ñû11
            .Add(MakeParam("@kaisyuu1_wariai1", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu1_wariai1 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu1_wariai1)))
            'ñû1è`TCg
            .Add(MakeParam("@kaisyuu1_tegata_site_gessuu", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu1_tegata_site_gessuu = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu1_tegata_site_gessuu)))
            'ñû1è`TCgú
            .Add(MakeParam("@kaisyuu1_tegata_site_date", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu1_tegata_site_date = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu1_tegata_site_date)))
            'ñû1¿p
            .Add(MakeParam("@kaisyuu1_seikyuusyo_yousi", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu1_seikyuusyo_yousi = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu1_seikyuusyo_yousi)))
            'ñû1íÊ2
            .Add(MakeParam("@kaisyuu1_syubetu2", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu1_syubetu2 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu1_syubetu2)))
            'ñû12
            .Add(MakeParam("@kaisyuu1_wariai2", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu1_wariai2 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu1_wariai2)))
            'ñû1íÊ3
            .Add(MakeParam("@kaisyuu1_syubetu3", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu1_syubetu3 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu1_syubetu3)))
            'ñû13
            .Add(MakeParam("@kaisyuu1_wariai3", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu1_wariai3 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu1_wariai3)))
            'ñû«Ez
            .Add(MakeParam("@kaisyuu_kyoukaigaku", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu_kyoukaigaku = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu_kyoukaigaku)))
            'ñû2íÊ1
            .Add(MakeParam("@kaisyuu2_syubetu1", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu2_syubetu1 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu2_syubetu1)))
            'ñû21
            .Add(MakeParam("@kaisyuu2_wariai1", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu2_wariai1 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu2_wariai1)))
            'ñû2è`TCg
            .Add(MakeParam("@kaisyuu2_tegata_site_gessuu", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu2_tegata_site_gessuu = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu2_tegata_site_gessuu)))
            'ñû2è`TCgú
            .Add(MakeParam("@kaisyuu2_tegata_site_date", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu2_tegata_site_date = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu2_tegata_site_date)))
            'ñû2¿p
            .Add(MakeParam("@kaisyuu2_seikyuusyo_yousi", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu2_seikyuusyo_yousi = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu2_seikyuusyo_yousi)))
            'ñû2íÊ2
            .Add(MakeParam("@kaisyuu2_syubetu2", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu2_syubetu2 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu2_syubetu2)))
            'ñû22
            .Add(MakeParam("@kaisyuu2_wariai2", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu2_wariai2 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu2_wariai2)))
            'ñû2íÊ3
            .Add(MakeParam("@kaisyuu2_syubetu3", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu2_syubetu3 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu2_syubetu3)))
            'ñû23
            .Add(MakeParam("@kaisyuu2_wariai3", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu2_wariai3 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu2_wariai3)))
            'o^OC[U[ID
            .Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).add_login_user_id = "", DBNull.Value, dtSeikyuuSaki(0).add_login_user_id)))

            '20100925@¼ñæR[hA¼ñæ¼@ÇÁ@nR«««
            .Add(MakeParam("@nayose_saki_cd", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).nayose_saki_cd = "", DBNull.Value, dtSeikyuuSaki(0).nayose_saki_cd)))
            '20100925@¼ñæR[hA¼ñæ¼@ÇÁ@nRªªª

            '==================2011/06/16 Ô´  ÇÁ Jn«========================== 
            .Add(MakeParam("@kessanji_nidosime_flg", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kessanji_nidosime_flg = "", DBNull.Value, dtSeikyuuSaki(0).kessanji_nidosime_flg)))    'Zñx÷ßtO
            '==================2011/06/16 Ô´  ÇÁ I¹ª==========================

            '==================2012/05/17 Ô´ 407553ÌÎ ÇÁ«========================== 
            'ûUOKtO
            .Add(MakeParam("@koufuri_ok_flg", SqlDbType.Int, 10, IIf(dtSeikyuuSaki(0).koufuri_ok_flg = "", DBNull.Value, dtSeikyuuSaki(0).koufuri_ok_flg)))
            'ïv¾Óæº°ÄÞ
            .Add(MakeParam("@tougou_tokuisaki_cd", SqlDbType.VarChar, 10, IIf(dtSeikyuuSaki(0).tougou_tokuisaki_cd = "", DBNull.Value, dtSeikyuuSaki(0).tougou_tokuisaki_cd)))
            'ÀS¦Íïï_~
            .Add(MakeParam("@anzen_kaihi_en", SqlDbType.Int, 10, IIf(dtSeikyuuSaki(0).anzen_kaihi_en = "", DBNull.Value, dtSeikyuuSaki(0).anzen_kaihi_en)))
            'ÀS¦Íïï_
            .Add(MakeParam("@anzen_kaihi_wari", SqlDbType.Decimal, 10, IIf(dtSeikyuuSaki(0).anzen_kaihi_wari = "", DBNull.Value, dtSeikyuuSaki(0).anzen_kaihi_wari)))
            'õl
            .Add(MakeParam("@bikou", SqlDbType.VarChar, 40, IIf(dtSeikyuuSaki(0).bikou = "", DBNull.Value, dtSeikyuuSaki(0).bikou)))
            '==================2012/05/17 Ô´ 407553ÌÎ ÇÁª==========================
            '2013/5/29 ¿ÌâsxXR[hÁÉº¤EarthüC -----------------------«
            .Add(MakeParam("@ginkou_siten_cd", SqlDbType.VarChar, 3, IIf(dtSeikyuuSaki(0).ginkou_siten_cd = "", DBNull.Value, dtSeikyuuSaki(0).ginkou_siten_cd))) 'âsxXR[h
            '2013/5/29 ¿ÌâsxXR[hÁÉº¤EarthüC -----------------------ª
            .Add(MakeParam("@kyouryoku_kaihi_taisyou", SqlDbType.VarChar, 10, IIf(dtSeikyuuSaki(0).kyouryoku_kaihi_taisyou = "", DBNull.Value, dtSeikyuuSaki(0).kyouryoku_kaihi_taisyou))) 'âsxXR[h


        End With

        ' NGÀs
        Return ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

    End Function

    ''' <summary>
    ''' r¼`FbNp
    ''' </summary>
    ''' <param name="strSeikyuuSakiCd">¿æR[h</param>
    ''' <param name="strSeikyuuSakiBrc">¿æ</param>
    ''' <param name="strKousinDate">XVÔ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelHaita(ByVal strSeikyuuSakiCd As String, ByVal strSeikyuuSakiBrc As String, ByVal SeikyuuSakiKbn As String, ByVal strKousinDate As String) As DataTable

        ' DataSetCX^XÌ¶¬()
        Dim dsDataSet As New DataSet

        'SQL¶Ì¶¬
        Dim commandTextSb As New StringBuilder

        'p[^i[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL¶
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("  upd_login_user_id ")
            .AppendLine("  ,upd_datetime ")
            .AppendLine("FROM ")
            .AppendLine("  m_seikyuu_saki WITH (READCOMMITTED)  ")
            .AppendLine("WHERE ")
            .AppendLine("  seikyuu_saki_cd = @seikyuu_saki_cd  ")
            .AppendLine("AND ")
            .AppendLine("  seikyuu_saki_brc = @seikyuu_saki_brc  ")
            .AppendLine("AND ")
            .AppendLine("  seikyuu_saki_kbn = @seikyuu_saki_kbn  ")
            .AppendLine("AND ")
            .AppendLine("   CONVERT(varchar(19),CONVERT(datetime,upd_datetime,21),21)<>CONVERT(varchar(19),CONVERT(datetime,@upd_datetime,21),21) ")
            .AppendLine("AND ")
            .AppendLine("   upd_datetime IS NOT NULL  ")
        End With

        'p[^ÌÝè
        paramList.Add(MakeParam("@seikyuu_saki_cd", SqlDbType.VarChar, 5, strSeikyuuSakiCd))
        paramList.Add(MakeParam("@seikyuu_saki_brc", SqlDbType.VarChar, 2, strSeikyuuSakiBrc))
        paramList.Add(MakeParam("@seikyuu_saki_kbn", SqlDbType.VarChar, 1, SeikyuuSakiKbn))
        paramList.Add(MakeParam("@upd_datetime", SqlDbType.VarChar, 19, strKousinDate))

        ' õÀs
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dsDataSet", paramList.ToArray)

        Return dsDataSet.Tables(0)
    End Function

    ''' <summary>
    ''' ¿æ}X^e[uÌC³
    ''' </summary>
    ''' <param name="dtSeikyuuSaki">C³Ìf[^</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdSeikyuuSaki(ByVal dtSeikyuuSaki As SeikyuuSakiDataSet.m_seikyuu_sakiDataTable) As Integer

        'SQL¶Ì¶¬
        Dim commandTextSb As New StringBuilder

        'p[^i[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL¶
        With commandTextSb
            .AppendLine("UPDATE ")
            .AppendLine("   m_seikyuu_saki WITH (UPDLOCK) ")
            .AppendLine("SET ")
            .AppendLine("   torikesi = @torikesi, ") 'æÁ
            .AppendLine("   skk_jigyousyo_cd = @skk_jigyousyo_cd, ") 'VïvÆR[h

            '==================2011/06/16 Ô´  í Jn«==========================
            '.AppendLine("   kyuu_seikyuu_saki_cd = @kyuu_seikyuu_saki_cd, ") '¿æR[h
            '==================2011/06/16 Ô´  í I¹ª==========================

            .AppendLine("   skysy_soufu_jyuusyo1 = @skysy_soufu_jyuusyo1, ")    '¿tæZ1
            .AppendLine("   skysy_soufu_jyuusyo2 = @skysy_soufu_jyuusyo2, ")    '¿tæZ2
            .AppendLine("   skysy_soufu_yuubin_no = @skysy_soufu_yuubin_no, ")    '¿tæXÖÔ
            .AppendLine("   skysy_soufu_tel_no = @skysy_soufu_tel_no, ")    '¿tædbÔ
            .AppendLine("   skysy_soufu_fax_no = @skysy_soufu_fax_no, ")    '¿tæFAXÔ

            .AppendLine("   tantousya_mei = @tantousya_mei, ") 'SÒ¼
            .AppendLine("   seikyuusyo_inji_bukken_mei_flg = @seikyuusyo_inji_bukken_mei_flg, ") '¿ó¨¼tO
            .AppendLine("   nyuukin_kouza_no = @nyuukin_kouza_no, ") 'üàûÀÔ
            .AppendLine("   seikyuu_sime_date = @seikyuu_sime_date, ") '¿÷ßú
            .AppendLine("   senpou_seikyuu_sime_date = @senpou_seikyuu_sime_date, ") 'æû¿÷ßú
            .AppendLine("   tyk_koj_seikyuu_timing_flg = @tyk_koj_seikyuu_timing_flg, ") '¼H¿^C~OtO
            .AppendLine("   sousai_flg = @sousai_flg, ") 'EtO
            .AppendLine("   kaisyuu_yotei_gessuu = @kaisyuu_yotei_gessuu, ") 'ñû\è
            .AppendLine("   kaisyuu_yotei_date = @kaisyuu_yotei_date, ") 'ñû\èú
            .AppendLine("   seikyuusyo_hittyk_date = @seikyuusyo_hittyk_date, ") '¿Kú
            .AppendLine("   kaisyuu1_syubetu1 = @kaisyuu1_syubetu1, ") 'ñû1íÊ1
            .AppendLine("   kaisyuu1_wariai1 = @kaisyuu1_wariai1, ") 'ñû11
            .AppendLine("   kaisyuu1_tegata_site_gessuu = @kaisyuu1_tegata_site_gessuu, ") 'ñû1è`TCg
            .AppendLine("   kaisyuu1_tegata_site_date = @kaisyuu1_tegata_site_date, ") 'ñû1è`TCgú
            .AppendLine("   kaisyuu1_seikyuusyo_yousi = @kaisyuu1_seikyuusyo_yousi, ") 'ñû1¿p
            .AppendLine("   kaisyuu1_syubetu2 = @kaisyuu1_syubetu2, ") 'ñû1íÊ2
            .AppendLine("   kaisyuu1_wariai2 = @kaisyuu1_wariai2, ") 'ñû12
            .AppendLine("   kaisyuu1_syubetu3 = @kaisyuu1_syubetu3, ") 'ñû1íÊ3
            .AppendLine("   kaisyuu1_wariai3 = @kaisyuu1_wariai3, ") 'ñû13
            .AppendLine("   kaisyuu_kyoukaigaku = @kaisyuu_kyoukaigaku, ") 'ñû«Ez
            .AppendLine("   kaisyuu2_syubetu1 = @kaisyuu2_syubetu1, ") 'ñû2íÊ1
            .AppendLine("   kaisyuu2_wariai1 = @kaisyuu2_wariai1, ") 'ñû21
            .AppendLine("   kaisyuu2_tegata_site_gessuu = @kaisyuu2_tegata_site_gessuu, ") 'ñû2è`TCg
            .AppendLine("   kaisyuu2_tegata_site_date = @kaisyuu2_tegata_site_date, ") 'ñû2è`TCgú
            .AppendLine("   kaisyuu2_seikyuusyo_yousi = @kaisyuu2_seikyuusyo_yousi, ") 'ñû2¿p
            .AppendLine("   kaisyuu2_syubetu2 = @kaisyuu2_syubetu2, ") 'ñû2íÊ2
            .AppendLine("   kaisyuu2_wariai2 = @kaisyuu2_wariai2, ") 'ñû22
            .AppendLine("   kaisyuu2_syubetu3 = @kaisyuu2_syubetu3, ") 'ñû2íÊ3
            .AppendLine("   kaisyuu2_wariai3 = @kaisyuu2_wariai3, ") 'ñû23
            .AppendLine("   upd_login_user_id = @upd_login_user_id, ") 'XVOC[U[ID
            .AppendLine("   upd_datetime = GETDATE() ") 'XVú
            '20100925@¼ñæR[hA¼ñæ¼@ÇÁ@nR«««
            .AppendLine("   ,nayose_saki_cd = @nayose_saki_cd ")    '¼ñæR[h
            '20100925@¼ñæR[hA¼ñæ¼@ÇÁ@nRªªª

            '==================2011/06/16 Ô´  ÇÁ Jn«========================== 
            .AppendLine("   ,kessanji_nidosime_flg = @kessanji_nidosime_flg ")   'Zñx÷ßtO
            '==================2011/06/16 Ô´  ÇÁ I¹ª==========================

            '==================2012/05/17 Ô´ 407553ÌÎ ÇÁ«========================== 
            .AppendLine("   ,koufuri_ok_flg = @koufuri_ok_flg ") 'ûUOKtO
            .AppendLine("   ,tougou_tokuisaki_cd = @tougou_tokuisaki_cd ") 'ïv¾Óæº°ÄÞ
            .AppendLine("   ,anzen_kaihi_en = @anzen_kaihi_en ") 'ÀS¦Íïï_~
            .AppendLine("   ,anzen_kaihi_wari = @anzen_kaihi_wari ") 'ÀS¦Íïï_
            .AppendLine("   ,bikou = @bikou ") 'õl
            '==================2012/05/17 Ô´ 407553ÌÎ ÇÁª==========================
            '2013/5/29 ¿ÌâsxXR[hÁÉº¤EarthüC -----------------------«
            .AppendLine("   ,ginkou_siten_cd = @ginkou_siten_cd ") 'âsxXR[h
            '2013/5/29 ¿ÌâsxXR[hÁÉº¤EarthüC -----------------------ª
            .AppendLine("   ,kyouryoku_kaihi_taisyou = @kyouryoku_kaihi_taisyou ")
            .AppendLine("WHERE ")
            .AppendLine("  seikyuu_saki_cd = @seikyuu_saki_cd  ")
            .AppendLine("AND ")
            .AppendLine("  seikyuu_saki_brc = @seikyuu_saki_brc  ")
            .AppendLine("AND ")
            .AppendLine("  seikyuu_saki_kbn = @seikyuu_saki_kbn  ")
        End With

        'p[^ÌÝè
        With paramList
            '¿æR[h
            .Add(MakeParam("@seikyuu_saki_cd", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).seikyuu_saki_cd = "", DBNull.Value, dtSeikyuuSaki(0).seikyuu_saki_cd)))
            '¿æ}Ô
            .Add(MakeParam("@seikyuu_saki_brc", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).seikyuu_saki_brc = "", DBNull.Value, dtSeikyuuSaki(0).seikyuu_saki_brc)))
            '¿ææª
            .Add(MakeParam("@seikyuu_saki_kbn", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).seikyuu_saki_kbn = "", DBNull.Value, dtSeikyuuSaki(0).seikyuu_saki_kbn)))
            'æÁ
            .Add(MakeParam("@torikesi", SqlDbType.VarChar, 255, dtSeikyuuSaki(0).torikesi))
            'VïvÆR[h
            .Add(MakeParam("@skk_jigyousyo_cd", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).skk_jigyousyo_cd = "", DBNull.Value, dtSeikyuuSaki(0).skk_jigyousyo_cd)))

            '==================2011/06/16 Ô´  í Jn«==========================
            ''¿æR[h
            '.Add(MakeParam("@kyuu_seikyuu_saki_cd", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kyuu_seikyuu_saki_cd = "", DBNull.Value, dtSeikyuuSaki(0).kyuu_seikyuu_saki_cd)))
            '==================2011/06/16 Ô´  í I¹ª==========================

            '¿tæZ1
            .Add(MakeParam("@skysy_soufu_jyuusyo1", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).skysy_soufu_jyuusyo1 = "", DBNull.Value, dtSeikyuuSaki(0).skysy_soufu_jyuusyo1)))
            '¿tæZ2
            .Add(MakeParam("@skysy_soufu_jyuusyo2", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).skysy_soufu_jyuusyo2 = "", DBNull.Value, dtSeikyuuSaki(0).skysy_soufu_jyuusyo2)))
            '¿tæXÖÔ
            .Add(MakeParam("@skysy_soufu_yuubin_no", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).skysy_soufu_yuubin_no = "", DBNull.Value, dtSeikyuuSaki(0).skysy_soufu_yuubin_no)))
            '¿tædbÔ
            .Add(MakeParam("@skysy_soufu_tel_no", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).skysy_soufu_tel_no = "", DBNull.Value, dtSeikyuuSaki(0).skysy_soufu_tel_no)))
            '¿tæFAXÔ
            .Add(MakeParam("@skysy_soufu_fax_no", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).skysy_soufu_fax_no = "", DBNull.Value, dtSeikyuuSaki(0).skysy_soufu_fax_no)))

            'SÒ¼
            .Add(MakeParam("@tantousya_mei", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).tantousya_mei = "", DBNull.Value, dtSeikyuuSaki(0).tantousya_mei)))
            '¿ó¨¼tO
            .Add(MakeParam("@seikyuusyo_inji_bukken_mei_flg", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).seikyuusyo_inji_bukken_mei_flg = "", DBNull.Value, dtSeikyuuSaki(0).seikyuusyo_inji_bukken_mei_flg)))
            'üàûÀÔ
            .Add(MakeParam("@nyuukin_kouza_no", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).nyuukin_kouza_no = "", DBNull.Value, dtSeikyuuSaki(0).nyuukin_kouza_no)))
            '¿÷ßú
            .Add(MakeParam("@seikyuu_sime_date", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).seikyuu_sime_date = "", DBNull.Value, dtSeikyuuSaki(0).seikyuu_sime_date)))
            'æû¿÷ßú
            .Add(MakeParam("@senpou_seikyuu_sime_date", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).senpou_seikyuu_sime_date = "", DBNull.Value, dtSeikyuuSaki(0).senpou_seikyuu_sime_date)))
            '¼H¿^C~OtO
            .Add(MakeParam("@tyk_koj_seikyuu_timing_flg", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).tyk_koj_seikyuu_timing_flg = "", DBNull.Value, dtSeikyuuSaki(0).tyk_koj_seikyuu_timing_flg)))
            'EtO
            .Add(MakeParam("@sousai_flg", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).sousai_flg = "", DBNull.Value, dtSeikyuuSaki(0).sousai_flg)))
            'ñû\è
            .Add(MakeParam("@kaisyuu_yotei_gessuu", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu_yotei_gessuu = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu_yotei_gessuu)))
            'ñû\èú
            .Add(MakeParam("@kaisyuu_yotei_date", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu_yotei_date = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu_yotei_date)))
            '¿Kú
            .Add(MakeParam("@seikyuusyo_hittyk_date", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).seikyuusyo_hittyk_date = "", DBNull.Value, dtSeikyuuSaki(0).seikyuusyo_hittyk_date)))
            'ñû1íÊ1
            .Add(MakeParam("@kaisyuu1_syubetu1", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu1_syubetu1 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu1_syubetu1)))
            'ñû11
            .Add(MakeParam("@kaisyuu1_wariai1", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu1_wariai1 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu1_wariai1)))
            'ñû1è`TCg
            .Add(MakeParam("@kaisyuu1_tegata_site_gessuu", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu1_tegata_site_gessuu = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu1_tegata_site_gessuu)))
            'ñû1è`TCgú
            .Add(MakeParam("@kaisyuu1_tegata_site_date", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu1_tegata_site_date = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu1_tegata_site_date)))
            'ñû1¿p
            .Add(MakeParam("@kaisyuu1_seikyuusyo_yousi", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu1_seikyuusyo_yousi = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu1_seikyuusyo_yousi)))
            'ñû1íÊ2
            .Add(MakeParam("@kaisyuu1_syubetu2", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu1_syubetu2 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu1_syubetu2)))
            'ñû12
            .Add(MakeParam("@kaisyuu1_wariai2", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu1_wariai2 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu1_wariai2)))
            'ñû1íÊ3
            .Add(MakeParam("@kaisyuu1_syubetu3", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu1_syubetu3 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu1_syubetu3)))
            'ñû13
            .Add(MakeParam("@kaisyuu1_wariai3", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu1_wariai3 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu1_wariai3)))
            'ñû«Ez
            .Add(MakeParam("@kaisyuu_kyoukaigaku", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu_kyoukaigaku = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu_kyoukaigaku)))
            'ñû2íÊ1
            .Add(MakeParam("@kaisyuu2_syubetu1", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu2_syubetu1 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu2_syubetu1)))
            'ñû21
            .Add(MakeParam("@kaisyuu2_wariai1", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu2_wariai1 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu2_wariai1)))
            'ñû2è`TCg
            .Add(MakeParam("@kaisyuu2_tegata_site_gessuu", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu2_tegata_site_gessuu = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu2_tegata_site_gessuu)))
            'ñû2è`TCgú
            .Add(MakeParam("@kaisyuu2_tegata_site_date", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu2_tegata_site_date = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu2_tegata_site_date)))
            'ñû2¿p
            .Add(MakeParam("@kaisyuu2_seikyuusyo_yousi", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu2_seikyuusyo_yousi = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu2_seikyuusyo_yousi)))
            'ñû2íÊ2
            .Add(MakeParam("@kaisyuu2_syubetu2", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu2_syubetu2 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu2_syubetu2)))
            'ñû22
            .Add(MakeParam("@kaisyuu2_wariai2", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu2_wariai2 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu2_wariai2)))
            'ñû2íÊ3
            .Add(MakeParam("@kaisyuu2_syubetu3", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu2_syubetu3 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu2_syubetu3)))
            'ñû23
            .Add(MakeParam("@kaisyuu2_wariai3", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kaisyuu2_wariai3 = "", DBNull.Value, dtSeikyuuSaki(0).kaisyuu2_wariai3)))
            'XVOC[U[ID
            .Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).upd_login_user_id = "", DBNull.Value, dtSeikyuuSaki(0).upd_login_user_id)))

            '20100925@¼ñæR[hA¼ñæ¼@ÇÁ@nR«««
            .Add(MakeParam("@nayose_saki_cd", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).nayose_saki_cd = "", DBNull.Value, dtSeikyuuSaki(0).nayose_saki_cd)))
            '20100925@¼ñæR[hA¼ñæ¼@ÇÁ@nRªªª

            '==================2011/06/16 Ô´  ÇÁ Jn«========================== 
            .Add(MakeParam("@kessanji_nidosime_flg", SqlDbType.VarChar, 255, IIf(dtSeikyuuSaki(0).kessanji_nidosime_flg = "", DBNull.Value, dtSeikyuuSaki(0).kessanji_nidosime_flg)))    'Zñx÷ßtO
            '==================2011/06/16 Ô´  ÇÁ I¹ª==========================

            '==================2012/05/17 Ô´ 407553ÌÎ ÇÁ«========================== 
            'ûUOKtO
            .Add(MakeParam("@koufuri_ok_flg", SqlDbType.Int, 10, IIf(dtSeikyuuSaki(0).koufuri_ok_flg = "", DBNull.Value, dtSeikyuuSaki(0).koufuri_ok_flg)))
            'ïv¾Óæº°ÄÞ
            .Add(MakeParam("@tougou_tokuisaki_cd", SqlDbType.VarChar, 10, IIf(dtSeikyuuSaki(0).tougou_tokuisaki_cd = "", DBNull.Value, dtSeikyuuSaki(0).tougou_tokuisaki_cd)))
            'ÀS¦Íïï_~
            .Add(MakeParam("@anzen_kaihi_en", SqlDbType.Int, 10, IIf(dtSeikyuuSaki(0).anzen_kaihi_en = "", DBNull.Value, dtSeikyuuSaki(0).anzen_kaihi_en)))
            'ÀS¦Íïï_
            .Add(MakeParam("@anzen_kaihi_wari", SqlDbType.Decimal, 10, IIf(dtSeikyuuSaki(0).anzen_kaihi_wari = "", DBNull.Value, dtSeikyuuSaki(0).anzen_kaihi_wari)))
            'õl
            .Add(MakeParam("@bikou", SqlDbType.VarChar, 40, IIf(dtSeikyuuSaki(0).bikou = "", DBNull.Value, dtSeikyuuSaki(0).bikou)))
            '==================2012/05/17 Ô´ 407553ÌÎ ÇÁª==========================
            '2013/5/29 ¿ÌâsxXR[hÁÉº¤EarthüC -----------------------«
            .Add(MakeParam("@ginkou_siten_cd", SqlDbType.VarChar, 3, IIf(dtSeikyuuSaki(0).ginkou_siten_cd = "", DBNull.Value, dtSeikyuuSaki(0).ginkou_siten_cd))) 'âsxXR[h
            '2013/5/29 ¿ÌâsxXR[hÁÉº¤EarthüC -----------------------ª
            .Add(MakeParam("@kyouryoku_kaihi_taisyou", SqlDbType.VarChar, 10, IIf(dtSeikyuuSaki(0).kyouryoku_kaihi_taisyou = "", DBNull.Value, dtSeikyuuSaki(0).kyouryoku_kaihi_taisyou))) 'âsxXR[h


        End With

        ' NGÀs
        Return ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

    End Function

    ''' <summary>
    ''' ¿æo^`}X^îñÌæ¾
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelSeikyuuSakiHinagataInfo(ByVal strSeikyuuSakiBrc As String) As SeikyuuSakiHinagataDataSet.m_seikyuu_saki_touroku_hinagataDataTable

        ' DataSetCX^XÌ¶¬
        Dim dsDataSet As New SeikyuuSakiHinagataDataSet

        'SQL¶Ì¶¬
        Dim commandTextSb As New StringBuilder

        'p[^i[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL¶
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   skk_jigyousyo_cd, ") 'VïvÆR[h
            .AppendLine("   tantousya_mei, ") 'SÒ¼
            .AppendLine("   seikyuusyo_inji_bukken_mei_flg, ") '¿ó¨¼tO
            .AppendLine("   nyuukin_kouza_no, ") 'üàûÀÔ
            .AppendLine("   seikyuu_sime_date, ") '¿÷ßú
            .AppendLine("   senpou_seikyuu_sime_date, ") 'æû¿÷ßú
            .AppendLine("   sousai_flg, ") 'EtO

            .AppendLine("   tyk_koj_seikyuu_timing_flg, ")  '¼H¿^C~OtO

            .AppendLine("   kaisyuu_yotei_gessuu, ") 'ñû\è
            .AppendLine("   kaisyuu_yotei_date, ") 'ñû\èú
            .AppendLine("   seikyuusyo_hittyk_date, ") '¿Kú
            .AppendLine("   kaisyuu1_syubetu1, ") 'ñû1íÊ1
            .AppendLine("   kaisyuu1_wariai1, ") 'ñû11
            .AppendLine("   kaisyuu1_tegata_site_gessuu, ") 'ñû1è`TCg
            .AppendLine("   kaisyuu1_tegata_site_date, ") 'ñû1è`TCgú
            .AppendLine("   kaisyuu1_seikyuusyo_yousi, ") 'ñû1¿p
            .AppendLine("   kaisyuu1_syubetu2, ") 'ñû1íÊ2
            .AppendLine("   kaisyuu1_wariai2, ") 'ñû12
            .AppendLine("   kaisyuu1_syubetu3, ") 'ñû1íÊ3
            .AppendLine("   kaisyuu1_wariai3, ") 'ñû13
            .AppendLine("   kaisyuu_kyoukaigaku, ") 'ñû«Ez
            .AppendLine("   kaisyuu2_syubetu1, ") 'ñû2íÊ1
            .AppendLine("   kaisyuu2_wariai1, ") 'ñû21
            .AppendLine("   kaisyuu2_tegata_site_gessuu, ") 'ñû2è`TCg
            .AppendLine("   kaisyuu2_tegata_site_date, ") 'ñû2è`TCgú
            .AppendLine("   kaisyuu2_seikyuusyo_yousi, ") 'ñû2¿p
            .AppendLine("   kaisyuu2_syubetu2, ") 'ñû2íÊ2
            .AppendLine("   kaisyuu2_wariai2, ") 'ñû22
            .AppendLine("   kaisyuu2_syubetu3, ") 'ñû2íÊ3
            .AppendLine("   kaisyuu2_wariai3 ") 'ñû23
            '2013/5/29 ¿ÌâsxXR[hÁÉº¤EarthüC -----------------------«
            .AppendLine("   ,ginkou_siten_cd ") 'âsxXR[h
            '2013/5/29 ¿ÌâsxXR[hÁÉº¤EarthüC -----------------------ª
            .AppendLine("FROM ")
            .AppendLine("   m_seikyuu_saki_touroku_hinagata WITH (READCOMMITTED) ")      '¿æo^`}X^
            .AppendLine("WHERE ")
            .AppendLine("   seikyuu_saki_brc = @seikyuu_saki_brc ")

        End With

        'p[^ÌÝè
        paramList.Add(MakeParam("@seikyuu_saki_brc", SqlDbType.VarChar, 2, strSeikyuuSakiBrc))

        ' õÀs
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, dsDataSet.m_seikyuu_saki_touroku_hinagata.TableName, paramList.ToArray)

        'ßé
        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>
    ''' VïvÆ}X^
    ''' </summary>
    Public Function SelSinkaikeiJigyousyoInfo(ByVal strSkkJigyousyoCd As String) As DataTable

        ' DataSetCX^XÌ¶¬
        Dim dsDataSet As New DataSet

        'SQL¶Ì¶¬
        Dim commandTextSb As New StringBuilder

        'p[^i[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL¶
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   skk_jigyousyo_cd, ")
            .AppendLine("   skk_jigyousyo_mei ")
            .AppendLine("FROM ")
            .AppendLine("   m_sinkaikei_jigyousyo WITH (READCOMMITTED)  ")
            .AppendLine("WHERE ")
            .AppendLine("   skk_jigyousyo_cd = @skk_jigyousyo_cd ")
        End With

        'p[^ÌÝè
        paramList.Add(MakeParam("@skk_jigyousyo_cd", SqlDbType.VarChar, 10, strSkkJigyousyoCd))

        ' õÀs
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dsDataSet", paramList.ToArray)

        'ßé
        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>
    ''' ¼ñæR[hi^MÇ}X^j
    ''' </summary>
    ''' <history>20100925@¼ñæR[hA¼ñæ¼@ÇÁ@nR</history>
    Public Function SelNayoseSakiInfo(ByVal strNayoseSakiCd As String) As DataTable

        ' DataSetCX^XÌ¶¬
        Dim dsDataSet As New DataSet

        'SQL¶Ì¶¬
        Dim commandTextSb As New StringBuilder

        'p[^i[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL¶
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   nayose_saki_cd, ")
            .AppendLine("   nayose_saki_name1 ")
            .AppendLine("FROM ")
            .AppendLine("   m_yosinkanri WITH (READCOMMITTED)  ")
            .AppendLine("WHERE ")
            .AppendLine("   nayose_saki_cd = @nayose_saki_cd ")
        End With

        'p[^ÌÝè
        paramList.Add(MakeParam("@nayose_saki_cd", SqlDbType.VarChar, 5, strNayoseSakiCd))

        ' õÀs
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dsDataSet", paramList.ToArray)

        'ßé
        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>
    ''' ¿æ}X^r[
    ''' </summary>
    Public Function SelVSeikyuuSakiInfo(ByVal strKbn As String, ByVal strCd As String, ByVal strBrc As String, ByVal blnDelete As Boolean) As CommonSearchDataSet.SeikyuuSakiTableDataTable

        ' DataSetCX^XÌ¶¬
        Dim dsDataSet As New CommonSearchDataSet

        'SQL¶Ì¶¬
        Dim commandTextSb As New StringBuilder

        'p[^i[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL¶
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   seikyuu_saki_kbn, ")
            .AppendLine("   RTRIM(seikyuu_saki_cd) AS seikyuu_saki_cd, ")
            .AppendLine("   seikyuu_saki_brc, ")
            .AppendLine("   seikyuu_saki_mei ")
            .AppendLine("FROM ")
            .AppendLine("   v_seikyuu_saki_info WITH(READCOMMITTED)  ")
            .AppendLine(" WHERE ")
        End With

        If blnDelete = True Then
            commandTextSb.Append(" torikesi = @torikesi ")
            paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 1, "0"))
        Else
            commandTextSb.Append("  0 = 0 ")
        End If

        If strCd.Trim <> "" Then
            commandTextSb.AppendLine(" AND seikyuu_saki_cd = @seikyuu_saki_cd ")
        End If
        If strBrc.Trim <> "" Then
            commandTextSb.AppendLine(" AND seikyuu_saki_brc = @seikyuu_saki_brc ")
        End If
        If strKbn.Trim <> "" Then
            commandTextSb.AppendLine(" AND seikyuu_saki_kbn = @seikyuu_saki_kbn ")
        End If

        'p[^ÌÝè
        'p[^ÌÝè
        paramList.Add(MakeParam("@seikyuu_saki_kbn", SqlDbType.VarChar, 255, strKbn))
        paramList.Add(MakeParam("@seikyuu_saki_cd", SqlDbType.VarChar, 255, strCd))
        paramList.Add(MakeParam("@seikyuu_saki_brc", SqlDbType.VarChar, 255, strBrc))

        ' õÀs
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, dsDataSet.SeikyuuSakiTable.TableName, paramList.ToArray)

        'ßé
        Return dsDataSet.SeikyuuSakiTable

    End Function

    ''' <summary>
    ''' »Ì¼}X^¶Ý`FbN
    ''' </summary>
    Public Function SelSonzaiChk(ByVal strKbn As String, ByVal strCd As String, ByVal strBrc As String) As DataTable

        ' DataSetCX^XÌ¶¬
        Dim dsDataSet As New DataSet

        'SQL¶Ì¶¬
        Dim commandTextSb As New StringBuilder

        'p[^i[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL¶
        With commandTextSb
            Select Case strKbn
                Case "0"
                    .AppendLine("SELECT ")
                    .AppendLine("   kameiten_cd ")
                    .AppendLine("FROM ")
                    .AppendLine("   m_kameiten WITH (READCOMMITTED)  ")
                    .AppendLine("WHERE ")
                    .AppendLine("   kameiten_cd = @kameiten_cd ")
                    'p[^ÌÝè
                    paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strCd))
                Case "1"
                    .AppendLine("SELECT ")
                    .AppendLine("   tys_kaisya_cd ")
                    .AppendLine("FROM ")
                    .AppendLine("   m_tyousakaisya WITH (READCOMMITTED)  ")
                    .AppendLine("WHERE ")
                    .AppendLine("   tys_kaisya_cd = @tys_kaisya_cd ")
                    .AppendLine("AND ")
                    .AppendLine("   jigyousyo_cd = @jigyousyo_cd ")
                    'p[^ÌÝè
                    paramList.Add(MakeParam("@tys_kaisya_cd", SqlDbType.VarChar, 5, strCd))
                    paramList.Add(MakeParam("@jigyousyo_cd", SqlDbType.VarChar, 2, strBrc))
                Case "2"
                    .AppendLine("SELECT ")
                    .AppendLine("   eigyousyo_cd ")
                    .AppendLine("FROM ")
                    .AppendLine("   m_eigyousyo WITH (READCOMMITTED)  ")
                    .AppendLine("WHERE ")
                    .AppendLine("   eigyousyo_cd = @eigyousyo_cd ")
                    'p[^ÌÝè
                    paramList.Add(MakeParam("@eigyousyo_cd", SqlDbType.VarChar, 5, strCd))
            End Select
        End With

        ' õÀs
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dsDataSet", paramList.ToArray)

        'ßé
        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>
    ''' MailAddress@æ¾
    ''' </summary>
    ''' <param name="yuubin_no">XÖNo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMailAddress(ByVal yuubin_no As String) As DataSet

        ' DataSetCX^XÌ¶¬
        Dim data As New DataSet

        ' SQL¶Ì¶¬
        Dim sql As New StringBuilder

        ' p[^i[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        ' Á¿X}XgîñÌSql 
        sql.AppendLine("SELECT (yuubin_no + ',' + isnull(todoufuken_mei,'')+     isnull(sikutyouson_mei,'')+      isnull(tiiki_mei,'')) as mei")
        sql.AppendLine("    from ")
        sql.AppendLine("    m_yuubin ")
        sql.AppendLine("    where ")
        sql.AppendLine("    yuubin_no like @yuubin_no order by yuubin_no")

        paramList.Add(MakeParam("@yuubin_no", SqlDbType.VarChar, 8, yuubin_no & "%"))
        ' õÀs
        FillDataset(connStr, CommandType.Text, sql.ToString(), data, _
                   "mei", paramList.ToArray)

        Return data

    End Function

    ''' <summary>
    ''' XÖÔ¶Ý`FbN
    ''' </summary>
    Public Function SelYuubinInfo(ByVal strBangou As String) As DataTable

        ' DataSetCX^XÌ¶¬
        Dim dsDataSet As New DataSet

        'SQL¶Ì¶¬
        Dim commandTextSb As New StringBuilder

        'p[^i[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL¶
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("   yuubin_no ")
            .AppendLine("FROM ")
            .AppendLine("   m_yuubin WITH (READCOMMITTED)  ")
            .AppendLine("WHERE ")
            .AppendLine("   yuubin_no = @yuubin_no ")
        End With

        'p[^ÌÝè
        paramList.Add(MakeParam("@yuubin_no", SqlDbType.VarChar, 7, strBangou))

        ' õÀs
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dsDataSet", paramList.ToArray)

        'ßé
        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>
    ''' ûUnjtOðæ¾·é
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2012/05/15 Ô´ 407553ÌÎ ÇÁ</history>
    Public Function SelKutiburiOkFlg() As Data.DataTable

        ' DataSetCX^XÌ¶¬
        Dim dsDataSet As New DataSet

        'SQL¶Ì¶¬
        Dim commandTextSb As New StringBuilder

        'p[^i[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL¶
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	code AS code ") '--R[h ")
            .AppendLine("	,ISNULL(meisyou,'') AS meisyou ") '--¼Ì ")
            .AppendLine("FROM ")
            .AppendLine("	m_kakutyou_meisyou WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	meisyou_syubetu = @meisyou_syubetu ") '--¼ÌíÊ ")
            .AppendLine("ORDER BY ")
            .AppendLine("	hyouji_jyun ASC ") '--\¦ ")
        End With

        'p[^ÌÝè
        paramList.Add(MakeParam("@meisyou_syubetu", SqlDbType.Int, 10, "47"))

        ' õÀs
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dsKutiburiOkFlg", paramList.ToArray)

        'ßé
        Return dsDataSet.Tables("dsKutiburiOkFlg")

    End Function

    ''' <summary>
    ''' âsxXR[hðæ¾·é
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/05/29 ko ¿ÌâsxXR[hÁÉº¤EarthüC</history>
    Public Function SelGinkouSitenCd() As Data.DataTable

        ' DataSetCX^XÌ¶¬
        Dim dsDataSet As New DataSet

        'SQL¶Ì¶¬
        Dim commandTextSb As New StringBuilder

        'p[^i[
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL¶
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	code AS code ") '--R[h ")
            .AppendLine("	,code + ':' + ISNULL(meisyou,'') AS meisyou ") '--¼Ì ")
            .AppendLine("FROM ")
            .AppendLine("	m_kakutyou_meisyou WITH(READCOMMITTED) ")
            .AppendLine("WHERE ")
            .AppendLine("	meisyou_syubetu = @meisyou_syubetu ") '--¼ÌíÊ ")
            .AppendLine("ORDER BY ")
            .AppendLine("	hyouji_jyun ASC ") '--\¦ ")
        End With

        'p[^ÌÝè
        paramList.Add(MakeParam("@meisyou_syubetu", SqlDbType.Int, 10, "48"))

        ' õÀs
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dsGinkouSitenCd", paramList.ToArray)

        'ßé
        Return dsDataSet.Tables("dsGinkouSitenCd")

    End Function

    ''' <summary>
    ''' MaxûUnjtOðæ¾·é
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2012/05/15 Ô´ 407553ÌÎ ÇÁ</history>
    Public Function SelMaxKutiburiOkFlg() As Data.DataTable

        ' DataSetCX^XÌ¶¬
        Dim dsDataSet As New DataSet

        'SQL¶Ì¶¬
        Dim commandTextSb As New StringBuilder

        'SQL¶
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("	MAX(CONVERT(INT,tougou_tokuisaki_cd)) AS tougou_tokuisaki_cd_max ") '--ïv¾Óæº°ÄÞ ")
            .AppendLine("FROM ")
            .AppendLine("	m_seikyuu_saki ") '--¿æ}X^ ")
        End With

        ' õÀs
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, "dsMaxKutiburiOkFlg")

        'ßé
        Return dsDataSet.Tables("dsMaxKutiburiOkFlg")

    End Function

End Class

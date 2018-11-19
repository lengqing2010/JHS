Imports System.Text
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Imports Itis.Earth.DataAccess
Imports System.Data.SqlClient

Public Class TorihikiJyouhouDataAccess
    Private CommonAbs As New AbsDataAccess()

    Private connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>
    ''' [取引情報]用　加盟店取引マスタ検索
    ''' </summary>
    ''' <param name="KametenCd">加盟店コード</param>
    ''' <returns>加盟店取引テーブル</returns>
    ''' <remarks></remarks>
    Public Function SelTorihiki(ByVal KametenCd As String) As KameitenTorihikiJyouhouDataSet.m_kameiten_torihiki_jouhouDataTable

        Dim commandTextSb As New System.Text.StringBuilder

        Dim dsReturn As New KameitenTorihikiJyouhouDataSet
        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine(" kameiten_cd ")
        commandTextSb.AppendLine(" ,sime_date ")
        commandTextSb.AppendLine(" ,seikyuusyo_hittyk_date ")
        commandTextSb.AppendLine(" ,siharai_yotei_tuki ")
        commandTextSb.AppendLine(" ,siharai_yotei_date ")
        commandTextSb.AppendLine(" ,siharai_houhou_flg ")
        commandTextSb.AppendLine(" ,siharai_genkin_wariai ")
        commandTextSb.AppendLine(" ,siharai_tegata_wariai ")
        commandTextSb.AppendLine(" ,siharai_site ")
        commandTextSb.AppendLine(" ,tegata_hiritu ")
        commandTextSb.AppendLine(" ,tys_hattyuusyo_umu ")
        commandTextSb.AppendLine(" ,koj_hattyuusyo_umu ")
        commandTextSb.AppendLine(" ,kensa_hattyuusyo_umu ")
        commandTextSb.AppendLine(" ,senpou_sitei_seikyuusyo ")
        commandTextSb.AppendLine(" ,flow_kakunin_date ")
        commandTextSb.AppendLine(" ,kyouryoku_kaihi_umu ")
        commandTextSb.AppendLine(" ,kyouryoku_kaihi_hiritu ")
        commandTextSb.AppendLine(" ,tys_mitsyo_flg ")
        commandTextSb.AppendLine(" ,ks_danmenzu_flg ")
        commandTextSb.AppendLine(" ,tatou_waribiki_flg ")
        commandTextSb.AppendLine(" ,tatou_waribiki_bikou ")
        commandTextSb.AppendLine(" ,tokka_sinsei_flg ")
        commandTextSb.AppendLine(" ,jinawa_taiou_umu ")
        commandTextSb.AppendLine(" ,kousin_taiou_umu ")
        commandTextSb.AppendLine(" ,zando_syobunhi_umu ")
        commandTextSb.AppendLine(" ,kyuusuisyadai_umu ")
        commandTextSb.AppendLine(" ,tys_kojkan_heikin_nissuu ")
        commandTextSb.AppendLine(" ,hyoujun_ks ")
        commandTextSb.AppendLine(" ,js_igai_koj_flg ")
        commandTextSb.AppendLine(" ,tys_hkks_busuu ")
        commandTextSb.AppendLine(" ,koj_hkks_busuu ")
        commandTextSb.AppendLine(" ,kensa_hkks_busuu ")
        commandTextSb.AppendLine(" ,nyuukin_mae_hosyousyo_hak ")
        commandTextSb.AppendLine(" ,hikiwatasi_file_umu ")
        commandTextSb.AppendLine(" ,tys_hkks_douhuu_umu ")
        commandTextSb.AppendLine(" ,koj_hkks_douhuu_umu ")
        commandTextSb.AppendLine(" ,kensa_hkks_douhuu_umu ")

        commandTextSb.AppendLine(" ,add_login_user_id ")
        commandTextSb.AppendLine(" ,add_datetime ")
        commandTextSb.AppendLine(" ,upd_login_user_id ")
        commandTextSb.AppendLine(" ,upd_datetime ")

        commandTextSb.AppendLine(" FROM ")
        commandTextSb.AppendLine(" m_kameiten_torihiki_jouhou WITH (READCOMMITTED)")
        commandTextSb.AppendLine(" WHERE ")
        commandTextSb.AppendLine(" kameiten_cd = @kameiten_cd  ")

        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, KametenCd))

        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "m_kameiten_torihiki_jouhou", paramList.ToArray)

        Return dsReturn.m_kameiten_torihiki_jouhou

    End Function

    ''' <summary>
    ''' [取引情報]用　加盟店取引マスタ検索、重複チェック
    ''' </summary>
    ''' <param name="kameitenCd">加盟店コード</param>
    ''' <returns>TRUE:存在,FALSE:不存在</returns>
    ''' <remarks></remarks>
    Public Function SelJyufukuData(ByVal kameitenCd As String) As Boolean

        Dim commandTextSb As New System.Text.StringBuilder

        Dim dsReturn As New DataSet
        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine("  COUNT (*) AS cnt ")
        commandTextSb.AppendLine(" FROM ")
        commandTextSb.AppendLine(" m_kameiten_torihiki_jouhou WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" WHERE ")
        commandTextSb.AppendLine(" kameiten_cd = @kameiten_cd ")

        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, kameitenCd))

        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsReturn, "dtReturn", paramList.ToArray)

        Return dsReturn.Tables(0).Rows(0).Item("cnt")

    End Function

    ''' <summary>
    ''' [取引情報_業務]用　取引マスタ、挿入また更新
    ''' </summary>
    ''' <param name="dtTourihikiGyoumu">加盟店取引業務テーブル</param>
    ''' <param name="kousinFlg">更新フラグ</param>
    ''' <returns>TRUE:成功,FALSE:失敗</returns>
    ''' <remarks></remarks>
    Public Function InsUpdTorihikiGyoumu(ByVal dtTourihikiGyoumu As KameitenTorihikiJyouhouDataSet.m_kameiten_torihiki_jouhouDataTable, ByVal kousinFlg As String) As Boolean

        '戻り値
        InsUpdTorihikiGyoumu = False

        Dim commandTextSb As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        If kousinFlg = "ins" Then

            commandTextSb.AppendLine(" INSERT INTO ")
            commandTextSb.AppendLine(" m_kameiten_torihiki_jouhou ")
            commandTextSb.AppendLine(" WITH(UPDLOCK) ")
            commandTextSb.AppendLine(" (kameiten_cd  ")
            commandTextSb.AppendLine(" ,tys_mitsyo_flg  ")
            commandTextSb.AppendLine(" ,ks_danmenzu_flg  ")
            commandTextSb.AppendLine(" ,tatou_waribiki_flg  ")
            commandTextSb.AppendLine(" ,tatou_waribiki_bikou  ")
            commandTextSb.AppendLine(" ,tokka_sinsei_flg  ")
            commandTextSb.AppendLine(" ,jinawa_taiou_umu  ")
            commandTextSb.AppendLine(" ,kousin_taiou_umu  ")
            commandTextSb.AppendLine(" ,zando_syobunhi_umu  ")
            commandTextSb.AppendLine(" ,kyuusuisyadai_umu  ")
            commandTextSb.AppendLine(" ,tys_kojkan_heikin_nissuu  ")
            commandTextSb.AppendLine(" ,hyoujun_ks  ")
            commandTextSb.AppendLine(" ,js_igai_koj_flg  ")
            commandTextSb.AppendLine(" ,tys_hkks_busuu  ")
            commandTextSb.AppendLine(" ,koj_hkks_busuu  ")
            commandTextSb.AppendLine(" ,kensa_hkks_busuu  ")
            commandTextSb.AppendLine(" ,nyuukin_mae_hosyousyo_hak  ")
            commandTextSb.AppendLine(" ,hikiwatasi_file_umu  ")
            commandTextSb.AppendLine(" ,tys_hkks_douhuu_umu  ")
            commandTextSb.AppendLine(" ,koj_hkks_douhuu_umu  ")
            commandTextSb.AppendLine(" ,kensa_hkks_douhuu_umu  ")
            commandTextSb.AppendLine(" ,add_login_user_id  ")
            commandTextSb.AppendLine(" ,add_datetime  ")
            commandTextSb.AppendLine(" ,upd_login_user_id ")
            commandTextSb.AppendLine(" ,upd_datetime) ")
            commandTextSb.AppendLine(" SELECT ")
            commandTextSb.AppendLine(" @kameiten_cd  ")
            commandTextSb.AppendLine(" ,@tys_mitsyo_flg  ")
            commandTextSb.AppendLine(" ,@ks_danmenzu_flg  ")
            commandTextSb.AppendLine(" ,@tatou_waribiki_flg  ")
            commandTextSb.AppendLine(" ,@tatou_waribiki_bikou  ")
            commandTextSb.AppendLine(" ,@tokka_sinsei_flg  ")
            commandTextSb.AppendLine(" ,@jinawa_taiou_umu  ")
            commandTextSb.AppendLine(" ,@kousin_taiou_umu  ")
            commandTextSb.AppendLine(" ,@zando_syobunhi_umu  ")
            commandTextSb.AppendLine(" ,@kyuusuisyadai_umu  ")
            commandTextSb.AppendLine(" ,@tys_kojkan_heikin_nissuu  ")
            commandTextSb.AppendLine(" ,@hyoujun_ks  ")
            commandTextSb.AppendLine(" ,@js_igai_koj_flg  ")
            commandTextSb.AppendLine(" ,@tys_hkks_busuu  ")
            commandTextSb.AppendLine(" ,@koj_hkks_busuu  ")
            commandTextSb.AppendLine(" ,@kensa_hkks_busuu  ")
            commandTextSb.AppendLine(" ,@nyuukin_mae_hosyousyo_hak  ")
            commandTextSb.AppendLine(" ,@hikiwatasi_file_umu  ")
            commandTextSb.AppendLine(" ,@tys_hkks_douhuu_umu  ")
            commandTextSb.AppendLine(" ,@koj_hkks_douhuu_umu  ")
            commandTextSb.AppendLine(" ,@kensa_hkks_douhuu_umu  ")
            commandTextSb.AppendLine(" ,@add_login_user_id  ")
            commandTextSb.AppendLine(" ,getdate() ")
            commandTextSb.AppendLine(" ,@add_login_user_id  ")
            commandTextSb.AppendLine(" ,getdate() ")

            commandTextSb.AppendLine(" DELETE FROM ")
            commandTextSb.AppendLine(" m_kameiten_torihiki_jouhou_renkei ")
            commandTextSb.AppendLine(" WITH(UPDLOCK) ")
            commandTextSb.AppendLine(" WHERE ")
            commandTextSb.AppendLine(" kameiten_cd = @kameiten_cd;")

            commandTextSb.AppendLine(" INSERT INTO ")
            commandTextSb.AppendLine(" m_kameiten_torihiki_jouhou_renkei ")
            commandTextSb.AppendLine(" WITH(UPDLOCK) ")
            commandTextSb.AppendLine(" (kameiten_cd,")
            commandTextSb.AppendLine(" renkei_siji_cd, ")
            commandTextSb.AppendLine(" sousin_jyky_cd, ")
            commandTextSb.AppendLine(" upd_login_user_id, ")
            commandTextSb.AppendLine(" upd_datetime) ")
            commandTextSb.AppendLine(" SELECT")
            commandTextSb.AppendLine(" @kameiten_cd, ")
            commandTextSb.AppendLine(" @renkei_siji_cd_ins, ")
            commandTextSb.AppendLine(" @sousin_jyky_cd, ")
            commandTextSb.AppendLine(" @add_login_user_id, ")
            commandTextSb.AppendLine(" getdate() ;")

        ElseIf kousinFlg = "upd" Then

            commandTextSb.AppendLine(" UPDATE ")
            commandTextSb.AppendLine(" m_kameiten_torihiki_jouhou ")
            commandTextSb.AppendLine(" WITH(UPDLOCK) ")
            commandTextSb.AppendLine(" SET")
            commandTextSb.AppendLine(" tys_mitsyo_flg = @tys_mitsyo_flg  ")
            commandTextSb.AppendLine(" ,ks_danmenzu_flg = @ks_danmenzu_flg   ")
            commandTextSb.AppendLine(" ,tatou_waribiki_flg = @tatou_waribiki_flg   ")
            commandTextSb.AppendLine(" ,tatou_waribiki_bikou = @tatou_waribiki_bikou   ")
            commandTextSb.AppendLine(" ,tokka_sinsei_flg = @tokka_sinsei_flg   ")
            commandTextSb.AppendLine(" ,jinawa_taiou_umu = @jinawa_taiou_umu   ")
            commandTextSb.AppendLine(" ,kousin_taiou_umu = @kousin_taiou_umu   ")
            commandTextSb.AppendLine(" ,zando_syobunhi_umu = @zando_syobunhi_umu   ")
            commandTextSb.AppendLine(" ,kyuusuisyadai_umu = @kyuusuisyadai_umu   ")
            commandTextSb.AppendLine(" ,tys_kojkan_heikin_nissuu = @tys_kojkan_heikin_nissuu   ")
            commandTextSb.AppendLine(" ,hyoujun_ks = @hyoujun_ks   ")
            commandTextSb.AppendLine(" ,js_igai_koj_flg = @js_igai_koj_flg   ")
            commandTextSb.AppendLine(" ,tys_hkks_busuu = @tys_hkks_busuu   ")
            commandTextSb.AppendLine(" ,koj_hkks_busuu = @koj_hkks_busuu   ")
            commandTextSb.AppendLine(" ,kensa_hkks_busuu = @kensa_hkks_busuu   ")
            commandTextSb.AppendLine(" ,nyuukin_mae_hosyousyo_hak = @nyuukin_mae_hosyousyo_hak   ")
            commandTextSb.AppendLine(" ,hikiwatasi_file_umu = @hikiwatasi_file_umu   ")
            commandTextSb.AppendLine(" ,tys_hkks_douhuu_umu = @tys_hkks_douhuu_umu   ")
            commandTextSb.AppendLine(" ,koj_hkks_douhuu_umu = @koj_hkks_douhuu_umu   ")
            commandTextSb.AppendLine(" ,kensa_hkks_douhuu_umu = @kensa_hkks_douhuu_umu   ")
            commandTextSb.AppendLine(" ,upd_login_user_id = @upd_login_user_id   ")
            commandTextSb.AppendLine(" ,upd_datetime = getdate() ")
            commandTextSb.AppendLine(" WHERE ")
            commandTextSb.AppendLine(" kameiten_cd = @kameiten_cd ")

            commandTextSb.AppendLine(" DELETE FROM ")
            commandTextSb.AppendLine(" m_kameiten_torihiki_jouhou_renkei ")
            commandTextSb.AppendLine(" WITH(UPDLOCK) ")
            commandTextSb.AppendLine(" WHERE ")
            commandTextSb.AppendLine(" kameiten_cd = @kameiten_cd;")

            commandTextSb.AppendLine(" INSERT INTO ")
            commandTextSb.AppendLine(" m_kameiten_torihiki_jouhou_renkei ")
            commandTextSb.AppendLine(" WITH(UPDLOCK) ")
            commandTextSb.AppendLine(" (kameiten_cd,")
            commandTextSb.AppendLine(" renkei_siji_cd, ")
            commandTextSb.AppendLine(" sousin_jyky_cd, ")
            commandTextSb.AppendLine(" upd_login_user_id, ")
            commandTextSb.AppendLine(" upd_datetime) ")
            commandTextSb.AppendLine(" SELECT")
            commandTextSb.AppendLine(" @kameiten_cd, ")
            commandTextSb.AppendLine(" @renkei_siji_cd_upd, ")
            commandTextSb.AppendLine(" @sousin_jyky_cd, ")
            commandTextSb.AppendLine(" @upd_login_user_id, ")
            commandTextSb.AppendLine(" getdate() ;")

        End If

        'パラメータの設定
        paramList.Add(MakeParam("@tys_mitsyo_flg", SqlDbType.Int, 1, dtTourihikiGyoumu.Rows(0).Item("tys_mitsyo_flg")))
        paramList.Add(MakeParam("@ks_danmenzu_flg", SqlDbType.Int, 1, dtTourihikiGyoumu.Rows(0).Item("ks_danmenzu_flg")))
        paramList.Add(MakeParam("@tatou_waribiki_flg", SqlDbType.Int, 1, dtTourihikiGyoumu.Rows(0).Item("tatou_waribiki_flg")))
        paramList.Add(MakeParam("@tatou_waribiki_bikou", SqlDbType.VarChar, 40, dtTourihikiGyoumu.Rows(0).Item("tatou_waribiki_bikou")))
        paramList.Add(MakeParam("@tokka_sinsei_flg", SqlDbType.Int, 1, dtTourihikiGyoumu.Rows(0).Item("tokka_sinsei_flg")))
        paramList.Add(MakeParam("@jinawa_taiou_umu", SqlDbType.Int, 1, dtTourihikiGyoumu.Rows(0).Item("jinawa_taiou_umu")))
        paramList.Add(MakeParam("@kousin_taiou_umu", SqlDbType.Int, 1, dtTourihikiGyoumu.Rows(0).Item("kousin_taiou_umu")))
        paramList.Add(MakeParam("@zando_syobunhi_umu", SqlDbType.Int, 1, dtTourihikiGyoumu.Rows(0).Item("zando_syobunhi_umu")))
        paramList.Add(MakeParam("@kyuusuisyadai_umu", SqlDbType.Int, 1, dtTourihikiGyoumu.Rows(0).Item("kyuusuisyadai_umu")))
        paramList.Add(MakeParam("@tys_kojkan_heikin_nissuu", SqlDbType.Int, 3, dtTourihikiGyoumu.Rows(0).Item("tys_kojkan_heikin_nissuu")))
        paramList.Add(MakeParam("@hyoujun_ks", SqlDbType.VarChar, 40, dtTourihikiGyoumu.Rows(0).Item("hyoujun_ks")))
        paramList.Add(MakeParam("@js_igai_koj_flg", SqlDbType.Int, 1, dtTourihikiGyoumu.Rows(0).Item("js_igai_koj_flg")))
        paramList.Add(MakeParam("@tys_hkks_busuu", SqlDbType.Int, 2, dtTourihikiGyoumu.Rows(0).Item("tys_hkks_busuu")))
        paramList.Add(MakeParam("@koj_hkks_busuu", SqlDbType.Int, 2, dtTourihikiGyoumu.Rows(0).Item("koj_hkks_busuu")))
        paramList.Add(MakeParam("@kensa_hkks_busuu", SqlDbType.Int, 2, dtTourihikiGyoumu.Rows(0).Item("kensa_hkks_busuu")))
        paramList.Add(MakeParam("@nyuukin_mae_hosyousyo_hak", SqlDbType.VarChar, 40, dtTourihikiGyoumu.Rows(0).Item("nyuukin_mae_hosyousyo_hak")))
        paramList.Add(MakeParam("@hikiwatasi_file_umu", SqlDbType.Int, 1, dtTourihikiGyoumu.Rows(0).Item("hikiwatasi_file_umu")))
        paramList.Add(MakeParam("@tys_hkks_douhuu_umu", SqlDbType.Int, 1, dtTourihikiGyoumu.Rows(0).Item("tys_hkks_douhuu_umu")))
        paramList.Add(MakeParam("@koj_hkks_douhuu_umu", SqlDbType.Int, 1, dtTourihikiGyoumu.Rows(0).Item("koj_hkks_douhuu_umu")))
        paramList.Add(MakeParam("@kensa_hkks_douhuu_umu", SqlDbType.Int, 1, dtTourihikiGyoumu.Rows(0).Item("kensa_hkks_douhuu_umu")))
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, dtTourihikiGyoumu.Rows(0).Item("kameiten_cd")))

        paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, dtTourihikiGyoumu.Rows(0).Item("add_login_user_id")))
        paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, dtTourihikiGyoumu.Rows(0).Item("upd_login_user_id")))

        paramList.Add(MakeParam("@renkei_siji_cd_ins", SqlDbType.Int, 4, 1))
        paramList.Add(MakeParam("@renkei_siji_cd_upd", SqlDbType.Int, 4, 2))
        paramList.Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 4, 0))

        '更新されたデータセットを DB へ書き込み
        ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

        '終了処理
        commandTextSb = Nothing

        '戻り値をセット成功の場合
        InsUpdTorihikiGyoumu = True
    End Function

    ''' <summary>
    ''' [取引情報_経理]用 　多棟割引マスタ、挿入また更新
    ''' </summary>
    ''' <param name="dtTourihikiKeiri">加盟店取引経理テーブル</param>
    ''' <param name="kousinFlg">更新フラグ</param>
    ''' <returns>TRUE:成功,FALSE:失敗</returns>
    ''' <remarks></remarks>
    Public Function InsUpdTorihikiKeiri(ByVal dtTourihikiKeiri As KameitenTorihikiJyouhouDataSet.m_kameiten_torihiki_jouhouDataTable, ByVal kousinFlg As String) As Boolean

        '戻り値
        InsUpdTorihikiKeiri = False

        Dim commandTextSb As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        If kousinFlg = "ins" Then

            commandTextSb.AppendLine(" INSERT INTO ")
            commandTextSb.AppendLine(" m_kameiten_torihiki_jouhou ")
            commandTextSb.AppendLine(" WITH(UPDLOCK) ")
            commandTextSb.AppendLine(" (kameiten_cd , ")
            commandTextSb.AppendLine(" sime_date ,")
            commandTextSb.AppendLine(" seikyuusyo_hittyk_date ,")
            commandTextSb.AppendLine(" siharai_yotei_tuki ,")
            commandTextSb.AppendLine(" siharai_yotei_date ,")
            commandTextSb.AppendLine(" siharai_houhou_flg ,")
            commandTextSb.AppendLine(" siharai_genkin_wariai ,")
            commandTextSb.AppendLine(" siharai_tegata_wariai ,")
            commandTextSb.AppendLine(" siharai_site ,")
            commandTextSb.AppendLine(" tegata_hiritu ,")
            commandTextSb.AppendLine(" tys_hattyuusyo_umu ,")
            commandTextSb.AppendLine(" koj_hattyuusyo_umu ,")
            commandTextSb.AppendLine(" kensa_hattyuusyo_umu ,")
            commandTextSb.AppendLine(" senpou_sitei_seikyuusyo ,")
            commandTextSb.AppendLine(" flow_kakunin_date ,")
            commandTextSb.AppendLine(" kyouryoku_kaihi_umu ,")
            commandTextSb.AppendLine(" kyouryoku_kaihi_hiritu,")
            commandTextSb.AppendLine(" add_login_user_id, ")
            commandTextSb.AppendLine(" add_datetime,  ")
            commandTextSb.AppendLine(" upd_login_user_id, ")
            commandTextSb.AppendLine(" upd_datetime) ")
            commandTextSb.AppendLine(" SELECT ")
            commandTextSb.AppendLine(" @kameiten_cd ,")
            commandTextSb.AppendLine(" @sime_date ,")
            commandTextSb.AppendLine(" @seikyuusyo_hittyk_date ,")
            commandTextSb.AppendLine(" @siharai_yotei_tuki ,")
            commandTextSb.AppendLine(" @siharai_yotei_date ,")
            commandTextSb.AppendLine(" @siharai_houhou_flg ,")
            commandTextSb.AppendLine(" @siharai_genkin_wariai ,")
            commandTextSb.AppendLine(" @siharai_tegata_wariai ,")
            commandTextSb.AppendLine(" @siharai_site ,")
            commandTextSb.AppendLine(" @tegata_hiritu ,")
            commandTextSb.AppendLine(" @tys_hattyuusyo_umu ,")
            commandTextSb.AppendLine(" @koj_hattyuusyo_umu ,")
            commandTextSb.AppendLine(" @kensa_hattyuusyo_umu ,")
            commandTextSb.AppendLine(" @senpou_sitei_seikyuusyo ,")
            commandTextSb.AppendLine(" @flow_kakunin_date ,")
            commandTextSb.AppendLine(" @kyouryoku_kaihi_umu ,")
            commandTextSb.AppendLine(" @kyouryoku_kaihi_hiritu,")
            commandTextSb.AppendLine(" @add_login_user_id,  ")
            commandTextSb.AppendLine(" getdate() ,")
            commandTextSb.AppendLine(" @add_login_user_id,  ")
            commandTextSb.AppendLine(" getdate() ;")

            commandTextSb.AppendLine(" DELETE FROM ")
            commandTextSb.AppendLine(" m_kameiten_torihiki_jouhou_renkei ")
            commandTextSb.AppendLine(" WITH(UPDLOCK) ")
            commandTextSb.AppendLine(" WHERE ")
            commandTextSb.AppendLine(" kameiten_cd = @kameiten_cd;")

            commandTextSb.AppendLine(" INSERT INTO ")
            commandTextSb.AppendLine(" m_kameiten_torihiki_jouhou_renkei ")
            commandTextSb.AppendLine(" WITH(UPDLOCK) ")
            commandTextSb.AppendLine(" (kameiten_cd,")
            commandTextSb.AppendLine(" renkei_siji_cd, ")
            commandTextSb.AppendLine(" sousin_jyky_cd, ")
            commandTextSb.AppendLine(" upd_login_user_id, ")
            commandTextSb.AppendLine(" upd_datetime) ")
            commandTextSb.AppendLine(" SELECT")
            commandTextSb.AppendLine(" @kameiten_cd, ")
            commandTextSb.AppendLine(" @renkei_siji_cd_ins, ")
            commandTextSb.AppendLine(" @sousin_jyky_cd, ")
            commandTextSb.AppendLine(" @add_login_user_id, ")
            commandTextSb.AppendLine(" getdate() ;")

        ElseIf kousinFlg = "upd" Then

            commandTextSb.AppendLine(" UPDATE ")
            commandTextSb.AppendLine(" m_kameiten_torihiki_jouhou ")
            commandTextSb.AppendLine(" WITH(UPDLOCK) ")
            commandTextSb.AppendLine(" SET")
            commandTextSb.AppendLine(" sime_date = @sime_date  ")
            commandTextSb.AppendLine(" ,seikyuusyo_hittyk_date = @seikyuusyo_hittyk_date   ")
            commandTextSb.AppendLine(" ,siharai_yotei_tuki = @siharai_yotei_tuki   ")
            commandTextSb.AppendLine(" ,siharai_yotei_date = @siharai_yotei_date   ")
            commandTextSb.AppendLine(" ,siharai_houhou_flg = @siharai_houhou_flg   ")
            commandTextSb.AppendLine(" ,siharai_genkin_wariai = @siharai_genkin_wariai   ")
            commandTextSb.AppendLine(" ,siharai_tegata_wariai = @siharai_tegata_wariai   ")
            commandTextSb.AppendLine(" ,siharai_site = @siharai_site   ")
            commandTextSb.AppendLine(" ,tegata_hiritu = @tegata_hiritu   ")
            commandTextSb.AppendLine(" ,tys_hattyuusyo_umu = @tys_hattyuusyo_umu   ")
            commandTextSb.AppendLine(" ,koj_hattyuusyo_umu = @koj_hattyuusyo_umu   ")
            commandTextSb.AppendLine(" ,kensa_hattyuusyo_umu = @kensa_hattyuusyo_umu   ")
            commandTextSb.AppendLine(" ,senpou_sitei_seikyuusyo = @senpou_sitei_seikyuusyo   ")
            commandTextSb.AppendLine(" ,flow_kakunin_date = @flow_kakunin_date   ")
            commandTextSb.AppendLine(" ,kyouryoku_kaihi_umu = @kyouryoku_kaihi_umu   ")
            commandTextSb.AppendLine(" ,kyouryoku_kaihi_hiritu = @kyouryoku_kaihi_hiritu   ")
            commandTextSb.AppendLine(" ,upd_login_user_id = @upd_login_user_id   ")
            commandTextSb.AppendLine(" ,upd_datetime = getdate() ")
            commandTextSb.AppendLine(" WHERE ")
            commandTextSb.AppendLine(" kameiten_cd = @kameiten_cd ;")

            commandTextSb.AppendLine(" DELETE FROM ")
            commandTextSb.AppendLine(" m_kameiten_torihiki_jouhou_renkei ")
            commandTextSb.AppendLine(" WITH(UPDLOCK) ")
            commandTextSb.AppendLine(" WHERE ")
            commandTextSb.AppendLine(" kameiten_cd = @kameiten_cd;")

            commandTextSb.AppendLine(" INSERT INTO ")
            commandTextSb.AppendLine(" m_kameiten_torihiki_jouhou_renkei ")
            commandTextSb.AppendLine(" WITH(UPDLOCK) ")
            commandTextSb.AppendLine(" (kameiten_cd,")
            commandTextSb.AppendLine(" renkei_siji_cd, ")
            commandTextSb.AppendLine(" sousin_jyky_cd, ")
            commandTextSb.AppendLine(" upd_login_user_id, ")
            commandTextSb.AppendLine(" upd_datetime) ")
            commandTextSb.AppendLine(" SELECT")
            commandTextSb.AppendLine(" @kameiten_cd, ")
            commandTextSb.AppendLine(" @renkei_siji_cd_upd, ")
            commandTextSb.AppendLine(" @sousin_jyky_cd, ")
            commandTextSb.AppendLine(" @upd_login_user_id, ")
            commandTextSb.AppendLine(" getdate() ;")

        End If

        'パラメータの設定
        paramList.Add(MakeParam("@sime_date", SqlDbType.VarChar, 2, dtTourihikiKeiri.Rows(0).Item("sime_date")))
        paramList.Add(MakeParam("@seikyuusyo_hittyk_date", SqlDbType.VarChar, 2, dtTourihikiKeiri.Rows(0).Item("seikyuusyo_hittyk_date")))
        paramList.Add(MakeParam("@siharai_yotei_tuki", SqlDbType.VarChar, 2, dtTourihikiKeiri.Rows(0).Item("siharai_yotei_tuki")))
        paramList.Add(MakeParam("@siharai_yotei_date", SqlDbType.VarChar, 2, dtTourihikiKeiri.Rows(0).Item("siharai_yotei_date")))

        paramList.Add(MakeParam("@siharai_houhou_flg", SqlDbType.Int, 1, dtTourihikiKeiri.Rows(0).Item("siharai_houhou_flg")))

        paramList.Add(MakeParam("@siharai_genkin_wariai", SqlDbType.Int, 3, dtTourihikiKeiri.Rows(0).Item("siharai_genkin_wariai")))
        paramList.Add(MakeParam("@siharai_tegata_wariai", SqlDbType.Int, 3, dtTourihikiKeiri.Rows(0).Item("siharai_tegata_wariai")))

        paramList.Add(MakeParam("@siharai_site", SqlDbType.Int, 2, dtTourihikiKeiri.Rows(0).Item("siharai_site")))
        paramList.Add(MakeParam("@tegata_hiritu", SqlDbType.VarChar, 128, dtTourihikiKeiri.Rows(0).Item("tegata_hiritu")))
        paramList.Add(MakeParam("@tys_hattyuusyo_umu", SqlDbType.Int, 1, dtTourihikiKeiri.Rows(0).Item("tys_hattyuusyo_umu")))
        paramList.Add(MakeParam("@koj_hattyuusyo_umu", SqlDbType.Int, 1, dtTourihikiKeiri.Rows(0).Item("koj_hattyuusyo_umu")))
        paramList.Add(MakeParam("@kensa_hattyuusyo_umu", SqlDbType.Int, 1, dtTourihikiKeiri.Rows(0).Item("kensa_hattyuusyo_umu")))
        paramList.Add(MakeParam("@senpou_sitei_seikyuusyo", SqlDbType.Int, 1, dtTourihikiKeiri.Rows(0).Item("senpou_sitei_seikyuusyo")))
        paramList.Add(MakeParam("@flow_kakunin_date", SqlDbType.DateTime, 8, dtTourihikiKeiri.Rows(0).Item("flow_kakunin_date")))
        paramList.Add(MakeParam("@kyouryoku_kaihi_umu", SqlDbType.Int, 1, dtTourihikiKeiri.Rows(0).Item("kyouryoku_kaihi_umu")))
        paramList.Add(MakeParam("@kyouryoku_kaihi_hiritu", SqlDbType.VarChar, 128, dtTourihikiKeiri.Rows(0).Item("kyouryoku_kaihi_hiritu")))
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, dtTourihikiKeiri.Rows(0).Item("kameiten_cd")))

        paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, dtTourihikiKeiri.Rows(0).Item("add_login_user_id")))
        paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, dtTourihikiKeiri.Rows(0).Item("upd_login_user_id")))

        paramList.Add(MakeParam("@renkei_siji_cd_ins", SqlDbType.Int, 4, 1))
        paramList.Add(MakeParam("@renkei_siji_cd_upd", SqlDbType.Int, 4, 2))
        paramList.Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 4, 0))

        '更新されたデータセットを DB へ書き込み
        ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

        '終了処理
        commandTextSb = Nothing

        '戻り値をセット成功の場合
        InsUpdTorihikiKeiri = True
    End Function

    ''' <summary>
    ''' [取引情報]用　加盟店マスタ、更新
    ''' </summary>
    ''' <param name="dtKameiten">加盟店テーブル</param>
    ''' <returns>TRUE:成功,FALSE:失敗</returns>
    ''' <remarks></remarks>
    Public Function UpdKameiten(ByVal dtKameiten As KameitenDataSet.m_kameitenTableDataTable) As Boolean

        '戻り値
        UpdKameiten = False

        Dim commandTextSb As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        commandTextSb.AppendLine(" UPDATE m_kameiten ")
        commandTextSb.AppendLine(" WITH(UPDLOCK) ")
        commandTextSb.AppendLine(" SET ")
        commandTextSb.AppendLine(" hosyou_kikan = @hosyou_kikan ")
        commandTextSb.AppendLine(" ,hosyousyo_hak_umu = @hosyousyo_hak_umu ")
        commandTextSb.AppendLine(" ,koj_gaisya_seikyuu_umu = @koj_gaisya_seikyuu_umu ")
        commandTextSb.AppendLine(" ,koj_tantou_flg = @koj_tantou_flg ")
        commandTextSb.AppendLine(" ,nyuukin_kakunin_jyouken = @nyuukin_kakunin_jyouken ")
        commandTextSb.AppendLine(" ,nyuukin_kakunin_oboegaki = @nyuukin_kakunin_oboegaki ")
        commandTextSb.AppendLine(" ,tys_mitsyo_flg = @tys_mitsyo_flg ")
        commandTextSb.AppendLine(" ,hattyuusyo_flg = @hattyuusyo_flg ")
        commandTextSb.AppendLine(" ,mitsyo_file_mei = @mitsyo_file_mei ")
        commandTextSb.AppendLine(" ,upd_login_user_id = @upd_login_user_id ")
        commandTextSb.AppendLine(" ,upd_datetime = getdate() ")

        '==================2017/01/01 李松涛 追加 液状化特約管理 新特約切替日↓==========================
        commandTextSb.AppendLine(" ,ekijyouka_tokuyaku_kanri = @ekijyouka_tokuyaku_kanri ")
        commandTextSb.AppendLine(" ,shintokuyaku_kirikaedate = @shintokuyaku_kirikaedate ")
        '==================2017/01/01 李松涛 追加 液状化特約管理 新特約切替日↑==========================

        commandTextSb.AppendLine(" ,web_moushikomi_saiban_hanbetu_flg = @web_moushikomi_saiban_hanbetu_flg ")
        commandTextSb.AppendLine(" ,hattyuusyo_michaku_renkei_taisyougai_flg = @hattyuusyo_michaku_renkei_taisyougai_flg ")

        commandTextSb.AppendLine(" ,shitei_seikyuusyo_umu = @shitei_seikyuusyo_umu ")
        commandTextSb.AppendLine(" ,shiroari_kensa_hyouji = @shiroari_kensa_hyouji ")

        commandTextSb.AppendLine(" ,hosyousyo_hak_kakuninsya = @hosyousyo_hak_kakuninsya")
        commandTextSb.AppendLine(" ,hosyousyo_hak_kakunin_date = @hosyousyo_hak_kakunin_date")
        commandTextSb.AppendLine(" ,hikiwatasi_inji_umu = @hikiwatasi_inji_umu")
        commandTextSb.AppendLine(" ,hosyou_kikan_kakuninsya = @hosyou_kikan_kakuninsya")
        commandTextSb.AppendLine(" ,hosyou_kikan_start_date = @hosyou_kikan_start_date")
        commandTextSb.AppendLine(" ,hosyousyo_hassou_umu = @hosyousyo_hassou_umu")
        commandTextSb.AppendLine(" ,fuho_fax_kakuninsya = @fuho_fax_kakuninsya")
        commandTextSb.AppendLine(" ,fuho_fax_kakunin_date = @fuho_fax_kakunin_date")
        commandTextSb.AppendLine(" ,fuho_fax_umu = @fuho_fax_umu")
        commandTextSb.AppendLine(" ,hosyousyo_hassou_umu_start_date = @hosyousyo_hassou_umu_start_date")


        commandTextSb.AppendLine(" WHERE ")
        commandTextSb.AppendLine(" kameiten_cd = @kameiten_cd ;")

        commandTextSb.AppendLine(" DELETE FROM ")
        commandTextSb.AppendLine(" m_kameiten_renkei ")
        commandTextSb.AppendLine(" WITH(UPDLOCK) ")
        commandTextSb.AppendLine(" WHERE ")
        commandTextSb.AppendLine(" kameiten_cd = @kameiten_cd;")

        commandTextSb.AppendLine(" INSERT INTO ")
        commandTextSb.AppendLine(" m_kameiten_renkei ")
        commandTextSb.AppendLine(" WITH(UPDLOCK) ")
        commandTextSb.AppendLine(" (kameiten_cd,")
        commandTextSb.AppendLine(" renkei_siji_cd, ")
        commandTextSb.AppendLine(" sousin_jyky_cd, ")
        commandTextSb.AppendLine(" upd_login_user_id, ")
        commandTextSb.AppendLine(" upd_datetime) ")
        commandTextSb.AppendLine(" SELECT")
        commandTextSb.AppendLine(" @kameiten_cd, ")
        commandTextSb.AppendLine(" @renkei_siji_cd, ")
        commandTextSb.AppendLine(" @sousin_jyky_cd, ")
        commandTextSb.AppendLine(" @upd_login_user_id, ")
        commandTextSb.AppendLine(" getdate() ;")

        'パラメータの設定
        paramList.Add(MakeParam("@hosyou_kikan", SqlDbType.Int, 4, dtKameiten.Rows(0).Item("hosyou_kikan")))
        paramList.Add(MakeParam("@hosyousyo_hak_umu", SqlDbType.Int, 4, dtKameiten.Rows(0).Item("hosyousyo_hak_umu")))
        paramList.Add(MakeParam("@koj_gaisya_seikyuu_umu", SqlDbType.Int, 4, dtKameiten.Rows(0).Item("koj_gaisya_seikyuu_umu")))
        paramList.Add(MakeParam("@koj_tantou_flg", SqlDbType.Int, 4, dtKameiten.Rows(0).Item("koj_tantou_flg")))
        paramList.Add(MakeParam("@nyuukin_kakunin_jyouken", SqlDbType.Int, 4, dtKameiten.Rows(0).Item("nyuukin_kakunin_jyouken")))
        paramList.Add(MakeParam("@nyuukin_kakunin_oboegaki", SqlDbType.DateTime, 8, dtKameiten.Rows(0).Item("nyuukin_kakunin_oboegaki")))
        paramList.Add(MakeParam("@tys_mitsyo_flg", SqlDbType.Int, 4, dtKameiten.Rows(0).Item("tys_mitsyo_flg")))
        paramList.Add(MakeParam("@hattyuusyo_flg", SqlDbType.VarChar, 5, dtKameiten.Rows(0).Item("hattyuusyo_flg")))
        paramList.Add(MakeParam("@mitsyo_file_mei", SqlDbType.VarChar, 5, dtKameiten.Rows(0).Item("mitsyo_file_mei")))

        paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, dtKameiten.Rows(0).Item("upd_login_user_id")))

        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, dtKameiten.Rows(0).Item("kameiten_cd")))

        paramList.Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 4, 2))
        paramList.Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 4, 0))

        '==================2017/01/01 李松涛 追加 液状化特約管理 新特約切替日↓==========================
        paramList.Add(MakeParam("@ekijyouka_tokuyaku_kanri", SqlDbType.VarChar, 10, dtKameiten.Rows(0).Item("ekijyouka_tokuyaku_kanri")))
        paramList.Add(MakeParam("@shintokuyaku_kirikaedate", SqlDbType.DateTime, 20, EmptyToNULL(dtKameiten.Rows(0).Item("shintokuyaku_kirikaedate"))))
        '==================2017/01/01 李松涛 追加 液状化特約管理 新特約切替日↑==========================

        paramList.Add(MakeParam("@web_moushikomi_saiban_hanbetu_flg", SqlDbType.Int, 4, dtKameiten.Rows(0).Item("web_moushikomi_saiban_hanbetu_flg")))
        paramList.Add(MakeParam("@hattyuusyo_michaku_renkei_taisyougai_flg", SqlDbType.Int, 4, dtKameiten.Rows(0).Item("hattyuusyo_michaku_renkei_taisyougai_flg")))

        If dtKameiten.Rows(0).Item("shitei_seikyuusyo_umu") = "" Then
            paramList.Add(MakeParam("@shitei_seikyuusyo_umu", SqlDbType.Int, 5, DBNull.Value))

        Else
            paramList.Add(MakeParam("@shitei_seikyuusyo_umu", SqlDbType.Int, 5, CInt(dtKameiten.Rows(0).Item("shitei_seikyuusyo_umu"))))

        End If
         paramList.Add(MakeParam("@shiroari_kensa_hyouji", SqlDbType.Int, 4, dtKameiten.Rows(0).Item("shiroari_kensa_hyouji")))

        paramList.Add(MakeParam("@hosyousyo_hak_kakuninsya", SqlDbType.VarChar, 50, dtKameiten.Rows(0).Item("hosyousyo_hak_kakuninsya")))
        paramList.Add(MakeParam("@hosyousyo_hak_kakunin_date", SqlDbType.DateTime, 20, EmptyToNULL(dtKameiten.Rows(0).Item("hosyousyo_hak_kakunin_date"))))
        paramList.Add(MakeParam("@hikiwatasi_inji_umu", SqlDbType.Int, 4, EmptyToNULL(dtKameiten.Rows(0).Item("hikiwatasi_inji_umu"))))

        paramList.Add(MakeParam("@hosyou_kikan_kakuninsya", SqlDbType.VarChar, 50, dtKameiten.Rows(0).Item("hosyou_kikan_kakuninsya")))
        paramList.Add(MakeParam("@hosyou_kikan_start_date", SqlDbType.DateTime, 20, EmptyToNULL(dtKameiten.Rows(0).Item("hosyou_kikan_start_date"))))
        paramList.Add(MakeParam("@hosyousyo_hassou_umu", SqlDbType.Int, 4, EmptyToNULL(dtKameiten.Rows(0).Item("hosyousyo_hassou_umu"))))

        paramList.Add(MakeParam("@fuho_fax_kakuninsya", SqlDbType.VarChar, 50, dtKameiten.Rows(0).Item("fuho_fax_kakuninsya")))
        paramList.Add(MakeParam("@fuho_fax_kakunin_date", SqlDbType.DateTime, 20, EmptyToNULL(dtKameiten.Rows(0).Item("fuho_fax_kakunin_date"))))
        paramList.Add(MakeParam("@fuho_fax_umu", SqlDbType.Int, 4, EmptyToNULL(dtKameiten.Rows(0).Item("fuho_fax_umu"))))

        '保証書発送有無_適用開始日
        paramList.Add(MakeParam("@hosyousyo_hassou_umu_start_date", SqlDbType.DateTime, 20, EmptyToNULL(dtKameiten.Rows(0).Item("hosyousyo_hassou_umu_start_date"))))

        '更新されたデータセットを DB へ書き込み
        ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

        '終了処理
        commandTextSb = Nothing

        '戻り値をセット成功の場合
        UpdKameiten = True

    End Function

    Function EmptyToNULL(ByVal v As Object) As Object
        If v.ToString = "" Then
            Return DBNull.Value
        Else
            Return v.ToString
        End If
    End Function

    ''' <summary>
    ''' コンボボックス設定用の有効な名称テーブルレコードを全て取得します<br/>
    ''' 本メソッドを直接実行した場合、コード９以外の全てのレコードが取得されます
    ''' </summary>
    ''' <param name="dt" >データテーブル</param>
    ''' <param name="meisyou_syubetu" >名称種別</param>
    ''' <param name="withSpaceRow" >先頭に空白行をセットする場合:true</param>
    ''' <param name="withCode" >（任意）ValueにKey項目を付加する場合:true " 1:aaaa "</param>
    ''' <remarks></remarks>
    Public Sub GetDropdownData(ByRef dt As DataTable, _
                                         ByVal meisyou_syubetu As String, _
                                         ByVal withSpaceRow As Boolean, _
                                         Optional ByVal withCode As Boolean = True)

        Dim connStr As String = ConnectionManager.Instance.ConnectionString
        Dim commandTextSb As New StringBuilder()
        Dim row As MeisyouDataSet.MeisyouTableRow

        Const paramSyubetu As String = "@SYUBETU"

        commandTextSb.Append("SELECT meisyou_syubetu,code,meisyou ")
        commandTextSb.Append("  FROM m_meisyou ")
        commandTextSb.Append("  WHERE meisyou_syubetu = " & paramSyubetu)
        commandTextSb.Append("  AND   code     <> 9 ")
        commandTextSb.Append("  ORDER BY hyouji_jyun ")

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(paramSyubetu, SqlDbType.Char, 2, meisyou_syubetu)}

        ' データの取得
        Dim MeisyouDataSet As New MeisyouDataSet()
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            MeisyouDataSet, MeisyouDataSet.MeisyouTable.TableName, commandParameters)

        Dim MeisyouDataTable As MeisyouDataSet.MeisyouTableDataTable = _
                    MeisyouDataSet.MeisyouTable

        If MeisyouDataTable.Count <> 0 Then

            ' 空白行データの設定
            If withSpaceRow = True Then
                dt.Rows.Add(CommonAbs.CreateRow("", "", dt))
            End If

            ' 取得データよりDataRowを作成しDataTableにセットする
            For Each row In MeisyouDataTable
                If withCode = True Then
                    dt.Rows.Add(CommonAbs.CreateRow(row.code.ToString + ":" + row.meisyou, row.code, dt))
                Else
                    dt.Rows.Add(CommonAbs.CreateRow(row.meisyou, row.code, dt))
                End If
            Next

        End If

    End Sub





    ''' <summary>
    ''' コンボボックス設定用の有効な名称テーブルレコードを全て取得します<br/>
    ''' 本メソッドを直接実行した場合、コード９以外の全てのレコードが取得されます
    ''' </summary>
    ''' <param name="dt" >データテーブル</param>
    ''' <param name="meisyou_syubetu" >名称種別</param>
    ''' <param name="withSpaceRow" >先頭に空白行をセットする場合:true</param>
    ''' <param name="withCode" >（任意）ValueにKey項目を付加する場合:true " 1:aaaa "</param>
    ''' <remarks></remarks>
    Public Sub GetDropdownData6869(ByRef dt As DataTable, _
                                         ByVal meisyou_syubetu As String, _
                                         ByVal withSpaceRow As Boolean, _
                                         Optional ByVal withCode As Boolean = True)

        Dim connStr As String = ConnectionManager.Instance.ConnectionString
        Dim commandTextSb As New StringBuilder()
        Dim row As MeisyouDataSet.MeisyouTableRow

        Const paramSyubetu As String = "@SYUBETU"

        commandTextSb.Append("SELECT meisyou_syubetu,code,meisyou ")
        commandTextSb.Append("  FROM m_kakutyou_meisyou ")
        'commandTextSb.Append("  WHERE meisyou_syubetu = " & paramSyubetu)
        commandTextSb.Append("  WHERE meisyou_syubetu = '" & meisyou_syubetu & "'")
        '       commandTextSb.Append("  AND   code     <> 9 ")
        commandTextSb.Append("  ORDER BY hyouji_jyun ")

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(paramSyubetu, SqlDbType.Char, 2, meisyou_syubetu)}

        ' データの取得
        Dim MeisyouDataSet As New MeisyouDataSet()
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            MeisyouDataSet, MeisyouDataSet.MeisyouTable.TableName)

        Dim MeisyouDataTable As MeisyouDataSet.MeisyouTableDataTable = _
                    MeisyouDataSet.MeisyouTable

        If MeisyouDataTable.Count <> 0 Then

            ' 空白行データの設定
            If withSpaceRow = True Then
                dt.Rows.Add(CommonAbs.CreateRow("", "", dt))
            End If

            ' 取得データよりDataRowを作成しDataTableにセットする
            For Each row In MeisyouDataTable
                If withCode = True Then
                    dt.Rows.Add(CommonAbs.CreateRow(row.code.ToString + ":" + row.meisyou, row.code, dt))
                Else
                    dt.Rows.Add(CommonAbs.CreateRow(row.meisyou, row.code, dt))
                End If
            Next

        End If

    End Sub

End Class

Imports System.Data.SqlClient
Imports System.Text
Imports Itis.ApplicationBlocks.Data.SQLHelper

''' <summary>ユーザー管理情報を照会登録する</summary>
''' <remarks>ユーザー管理情報を照会登録機能を提供する</remarks>
''' <history>
''' <para>2009/07/17　高雅娟(大連情報システム部)　新規作成</para>
''' </history>
Public Class KanrisyaMenuInquiryInputDataAccess
    Private connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>
    ''' 業務区分情報を取得する。
    ''' </summary>
    ''' <returns>業務区分情報データセット</returns>
    Public Function selGyoumuKubun() As KanrisyaMenuInquiryInputDataSet.gyoumuKubunDataTable

        Dim commandTextSb As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        Dim dsGyoumu As New KanrisyaMenuInquiryInputDataSet

        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine(" code, ")
        commandTextSb.AppendLine(" meisyou ")
        commandTextSb.AppendLine(" FROM m_meisyou WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" WHERE  meisyou_syubetu= @meisyou_syubetu ")

        'パラメータの設定
        paramList.Add(MakeParam("@meisyou_syubetu", SqlDbType.VarChar, 2, "53"))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsGyoumu, _
                    dsGyoumu.gyoumuKubun.TableName, paramList.ToArray)

        '終了処理
        commandTextSb = Nothing

        Return dsGyoumu.gyoumuKubun
    End Function

    ''' <summary>
    ''' ログインユーザーの参照権限管理者FLGを取得する。
    ''' </summary>
    ''' <param name="strUserId">ログインユーザーID</param>
    ''' <returns>参照権限管理者FLGデータテーブル</returns>
    Public Function selUserKengenKanriFlg(ByVal strUserId As String) _
            As KanrisyaMenuInquiryInputDataSet.userKengenKanriFlgDataTable

        Dim commandTextSb As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        Dim dsUserKengenKanriFlg As New KanrisyaMenuInquiryInputDataSet

        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine(" sansyou_kengen_kanri_flg ")
        commandTextSb.AppendLine(" FROM m_jiban_ninsyou WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" WHERE  login_user_id= @login_user_id ")

        'パラメータの設定
        paramList.Add(MakeParam("@login_user_id", SqlDbType.VarChar, 30, strUserId))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsUserKengenKanriFlg, _
                    dsUserKengenKanriFlg.userKengenKanriFlg.TableName, paramList.ToArray)

        '終了処理
        commandTextSb = Nothing

        Return dsUserKengenKanriFlg.userKengenKanriFlg

    End Function

    ''' <summary>
    ''' 所属部署変更日情報を取得する。
    ''' </summary>
    ''' <param name="strUserId">画面に入力したユーザーID</param>
    ''' <returns>所属部署変更日情報データセット</returns>
    Public Function selSyozokuHenkouDate(ByVal strUserId As String) _
            As KanrisyaMenuInquiryInputDataSet.syozokuHenkouDateDataTable

        Dim commandTextSb As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        Dim dsSyozokuHenkouDate As New KanrisyaMenuInquiryInputDataSet

        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine(" login_user_id, ")
        commandTextSb.AppendLine(" syozoku_henkou_date ")
        commandTextSb.AppendLine(" FROM m_jiban_ninsyou WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" WHERE  login_user_id= @login_user_id ")

        'パラメータの設定
        paramList.Add(MakeParam("@login_user_id", SqlDbType.VarChar, 30, strUserId.ToString))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsSyozokuHenkouDate, _
                    dsSyozokuHenkouDate.syozokuHenkouDate.TableName, paramList.ToArray)

        '終了処理
        commandTextSb = Nothing

        Return dsSyozokuHenkouDate.syozokuHenkouDate
    End Function

    ''' <summary>
    ''' ユーザー情報を取得する。
    ''' </summary>
    ''' <param name="strUserId">ユーザーID</param>
    ''' <param name="strSyozokuHenkouDate">所属部署変更日</param>
    ''' <returns>ユーザー情報データセット</returns>
    Public Function selUserInfo(ByVal strUserId As String, ByVal strSyozokuHenkouDate As String) _
                                        As KanrisyaMenuInquiryInputDataSet.kanrisyaJyouhouDataTable

        Dim commandTextSb As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        Dim dsUserInfo As New KanrisyaMenuInquiryInputDataSet

        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine(" A.login_user_id, ")
        commandTextSb.AppendLine(" B.DisplayName, ")
        commandTextSb.AppendLine(" C.busyo_mei, ")
        commandTextSb.AppendLine(" D.meisyou AS sosikiMei, ")
        commandTextSb.AppendLine(" E.yakusyoku, ")
        commandTextSb.AppendLine(" A.sansyou_kengen_kanri_flg, ")
        commandTextSb.AppendLine(" A.eigyou_man_kbn, ")
        commandTextSb.AppendLine(" A.gyoumu_kbn, ")
        commandTextSb.AppendLine(" A.t_sansyou_busyo_cd, ")
        commandTextSb.AppendLine(" C.sosiki_level, ")
        commandTextSb.AppendLine(" C.busyo_cd, ")
        commandTextSb.AppendLine(" F.irai_gyoumu_kengen, ")
        commandTextSb.AppendLine(" F.sinki_nyuuryoku_kengen, ")
        commandTextSb.AppendLine(" F.data_haki_kengen, ")
        commandTextSb.AppendLine(" F.kekka_gyoumu_kengen, ")
        commandTextSb.AppendLine(" F.hosyou_gyoumu_kengen, ")
        commandTextSb.AppendLine(" F.hkks_gyoumu_kengen, ")
        commandTextSb.AppendLine(" F.koj_gyoumu_kengen, ")
        commandTextSb.AppendLine(" F.keiri_gyoumu_kengen, ")
        commandTextSb.AppendLine(" F.hansoku_uri_kengen, ")
        commandTextSb.AppendLine(" F.hattyuusyo_kanri_kengen, ")
        commandTextSb.AppendLine(" F.kaiseki_master_kanri_kengen, ")
        commandTextSb.AppendLine(" F.eigyou_master_kanri_kengen, ")
        commandTextSb.AppendLine(" F.kkk_master_kanri_kengen, ")
        commandTextSb.AppendLine(" F.system_kanrisya_kengen, ")

        commandTextSb.AppendLine(" F.tyousaka_kanrisya_kengen, ")
        commandTextSb.AppendLine(" F.kensa_gyoumu_kengen, ")
        commandTextSb.AppendLine(" F.hanyou1_gyoumu_kengen, ")
        commandTextSb.AppendLine(" F.hanyou2_gyoumu_kengen, ")
        commandTextSb.AppendLine(" F.hanyou3_gyoumu_kengen, ")
        commandTextSb.AppendLine(" A.account_no, ")

        commandTextSb.AppendLine(" ISNULL(A.upd_datetime,'') AS upd_datetime ")
        commandTextSb.AppendLine(" FROM m_jiban_ninsyou  A WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" LEFT OUTER JOIN m_jhs_mailbox B ")
        commandTextSb.AppendLine(" ON A.login_user_id=B.PrimaryWindowsNTAccount ")
        commandTextSb.AppendLine(" LEFT OUTER JOIN m_busyo_kanri C ")
        If strSyozokuHenkouDate.ToString <> "" Then
            If strSyozokuHenkouDate.ToString.Substring(0, 10) > Date.Today.ToString.Substring(0, 10) Then
                commandTextSb.AppendLine(" ON A.kyuu_syozoku_busyo_cd=C.busyo_cd ")
            Else
                commandTextSb.AppendLine(" ON A.busyo_cd=C.busyo_cd ")
            End If
        Else
            commandTextSb.AppendLine(" ON A.busyo_cd=C.busyo_cd ")
        End If

        commandTextSb.AppendLine(" LEFT OUTER JOIN m_meisyou D ")
        commandTextSb.AppendLine(" ON D.meisyou_syubetu=@meisyou_syubetu1 ")
        commandTextSb.AppendLine("    AND	C.sosiki_level=D.code ")
        commandTextSb.AppendLine(" LEFT OUTER JOIN ")
        commandTextSb.AppendLine(" (SELECT ")
        commandTextSb.AppendLine(" C.login_user_id, ")
        commandTextSb.AppendLine(" D.meisyou AS yakusyoku ")
        commandTextSb.AppendLine(" FROM ")
        commandTextSb.AppendLine("  (SELECT ")
        commandTextSb.AppendLine("  TOP 1 A.yakusyoku_cd, ")
        commandTextSb.AppendLine("  B.sosiki_level, ")
        commandTextSb.AppendLine("  A.login_user_id ")
        commandTextSb.AppendLine("  FROM m_kanrityou A ")
        commandTextSb.AppendLine("  LEFT OUTER JOIN m_busyo_kanri B ")
        commandTextSb.AppendLine("  ON  A.busyo_cd=B.busyo_cd ")
        commandTextSb.AppendLine("  WHERE A.login_user_id=@login_user_id ")
        commandTextSb.AppendLine("  ORDER BY   B.sosiki_level ASC) AS C ")
        commandTextSb.AppendLine("  INNER JOIN m_meisyou D ")
        commandTextSb.AppendLine("  ON C.yakusyoku_cd=D.code AND D.meisyou_syubetu=@meisyou_syubetu2) AS E ")
        commandTextSb.AppendLine(" ON A.login_user_id=E.login_user_id ")
        commandTextSb.AppendLine(" LEFT OUTER JOIN m_account F ")
        commandTextSb.AppendLine(" ON A.account_no=F.account_no ")
        commandTextSb.AppendLine(" WHERE  ")
        commandTextSb.AppendLine(" A.login_user_id=@login_user_id ")

        'パラメータの設定
        paramList.Add(MakeParam("@meisyou_syubetu1", SqlDbType.VarChar, 2, "50"))
        paramList.Add(MakeParam("@meisyou_syubetu2", SqlDbType.VarChar, 2, "51"))
        paramList.Add(MakeParam("@login_user_id", SqlDbType.VarChar, 30, strUserId))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsUserInfo, _
                    dsUserInfo.kanrisyaJyouhou.TableName, paramList.ToArray)

        '終了処理
        commandTextSb = Nothing

        Return dsUserInfo.kanrisyaJyouhou
    End Function

    ''' <summary>
    ''' 追加参照部署コードによって取得した組織レベル
    ''' </summary>
    ''' <param name="strBusyoCd">追加参照部署コード</param>
    ''' <returns>組織レベルデータテーブル</returns>
    Public Function selLevel(ByVal strBusyoCd As String) As KanrisyaMenuInquiryInputDataSet.sansyouLevelDataTable

        Dim commandTextSb As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        Dim dsLevel As New KanrisyaMenuInquiryInputDataSet

        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine(" sosiki_level ")
        commandTextSb.AppendLine(" FROM m_busyo_kanri WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" WHERE  busyo_cd= @busyo_cd ")

        'パラメータの設定
        paramList.Add(MakeParam("@busyo_cd", SqlDbType.VarChar, 4, strBusyoCd.ToString))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsLevel, _
                    dsLevel.sansyouLevel.TableName, paramList.ToArray)

        '終了処理
        commandTextSb = Nothing

        Return dsLevel.sansyouLevel

    End Function


    ''' <summary>
    ''' 追加参照部署コードによって取得した組織レベル
    ''' </summary>
    ''' <returns>組織レベルデータテーブル</returns>
    Public Function SelBusyoList() As DataTable

        Dim commandTextSb As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        Dim ds As New DataSet

        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine(" busyo_cd,busyo_mei ")
        commandTextSb.AppendLine(" FROM m_busyo_kanri WITH (READCOMMITTED) ")


        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), ds, _
                    "tmp")

        '終了処理
        commandTextSb = Nothing

        Return ds.Tables(0)

    End Function





    ''' <summary>
    ''' 登録したユーザーが地盤認証マスタに存在チェック
    ''' </summary>
    ''' <param name="strUserId">ユーザーID</param>
    ''' <returns>ユーザー情報テーブル</returns>
    Public Function selJibanNinsyouHaita(ByVal strUserId As String) As KanrisyaMenuInquiryInputDataSet.jibanNinsyouDataTable

        Dim commandTextSb As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        Dim dsJibanNinsyou As New KanrisyaMenuInquiryInputDataSet

        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine(" login_user_id ,")
        commandTextSb.AppendLine(" upd_datetime ")
        commandTextSb.AppendLine(" FROM m_jiban_ninsyou WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" WHERE  login_user_id= @login_user_id ")

        'パラメータの設定
        paramList.Add(MakeParam("@login_user_id", SqlDbType.VarChar, 30, strUserId.ToString))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsJibanNinsyou, _
                    dsJibanNinsyou.jibanNinsyou.TableName, paramList.ToArray)

        '終了処理
        commandTextSb = Nothing

        Return dsJibanNinsyou.jibanNinsyou

    End Function

    ''' <summary>
    ''' 地盤認証マスタ.参照権限管理者FLG 取得する
    ''' </summary>
    ''' <param name="strUserId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelSansyouKengenKanriFlg(ByVal strUserId As String) As String

        Dim commandTextSb As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        Dim ds As New Data.DataSet

        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine(" isnull(sansyou_kengen_kanri_flg,'') ")
        commandTextSb.AppendLine(" FROM m_jiban_ninsyou WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" WHERE  login_user_id= @login_user_id ")

        'パラメータの設定
        paramList.Add(MakeParam("@login_user_id", SqlDbType.VarChar, 30, strUserId.ToString))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), ds, _
                    "tmp", paramList.ToArray)

        '終了処理
        commandTextSb = Nothing

        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item(0).ToString
        Else
            Return ""
        End If
    End Function


    ''' <summary>
    ''' 登録したユーザーが地盤認証マスタ連携管理テーブルに存在チェック
    ''' </summary>
    ''' <param name="strUserId">ユーザーID</param>
    ''' <returns>ユーザー情報テーブル</returns>
    Public Function selJibanNinsyouRenkeiHaita(ByVal strUserId As String) As KanrisyaMenuInquiryInputDataSet.jibanNinsyouDataTable

        Dim commandTextSb As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        Dim dsJibanNinsyou As New KanrisyaMenuInquiryInputDataSet

        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine(" login_user_id ")
        commandTextSb.AppendLine(" FROM m_jiban_ninsyou_renkei WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" WHERE  login_user_id= @login_user_id ")

        'パラメータの設定
        paramList.Add(MakeParam("@login_user_id", SqlDbType.VarChar, 30, strUserId.ToString))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsJibanNinsyou, _
                    dsJibanNinsyou.jibanNinsyou.TableName, paramList.ToArray)

        '終了処理
        commandTextSb = Nothing

        Return dsJibanNinsyou.jibanNinsyou

    End Function

    ''' <summary>
    ''' 地盤認証マスタを更新する。
    ''' </summary>
    ''' <param name="dtUPDData">更新項目のテーブル</param>
    ''' <returns>true or false</returns>
    Public Function UpdJibanNinsyou(ByVal account_no As String, ByVal dtUPDData As KanrisyaMenuInquiryInputDataSet.updJibanNinsyouBusyoDataTable) As Boolean
        '戻り値
        UpdJibanNinsyou = False

        Dim commandTextSb As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        commandTextSb.AppendLine(" UPDATE ")
        commandTextSb.AppendLine(" m_jiban_ninsyou ")
        commandTextSb.AppendLine(" WITH(UPDLOCK) ")
        commandTextSb.AppendLine(" SET ")
        commandTextSb.AppendLine(" eigyou_man_kbn=@eigyou_man_kbn, ")
        commandTextSb.AppendLine(" gyoumu_kbn=@gyoumu_kbn, ")
        commandTextSb.AppendLine(" busyo_cd=@ss_busyo_cd, ")
        commandTextSb.AppendLine(" t_sansyou_busyo_cd=@busyo_cd, ")
        commandTextSb.AppendLine(" upd_login_user_id=@upd_login_user_id, ")
        commandTextSb.AppendLine(" upd_datetime=GETDATE() ")
        commandTextSb.AppendLine("WHERE login_user_id = @login_user_id  ")



        commandTextSb.AppendLine(" UPDATE ")
        commandTextSb.AppendLine(" m_account ")
        commandTextSb.AppendLine(" SET ")

        commandTextSb.AppendLine(" irai_gyoumu_kengen='" & dtUPDData.Rows(0).Item("irai_gyoumu_kengen") & "', ")
        commandTextSb.AppendLine(" sinki_nyuuryoku_kengen='" & dtUPDData.Rows(0).Item("sinki_nyuuryoku_kengen") & "', ")
        commandTextSb.AppendLine(" data_haki_kengen='" & dtUPDData.Rows(0).Item("data_haki_kengen") & "', ")
        commandTextSb.AppendLine(" kekka_gyoumu_kengen='" & dtUPDData.Rows(0).Item("kekka_gyoumu_kengen") & "', ")
        commandTextSb.AppendLine(" hosyou_gyoumu_kengen='" & dtUPDData.Rows(0).Item("hosyou_gyoumu_kengen") & "', ")
        commandTextSb.AppendLine(" hkks_gyoumu_kengen='" & dtUPDData.Rows(0).Item("hkks_gyoumu_kengen") & "', ")
        commandTextSb.AppendLine(" koj_gyoumu_kengen='" & dtUPDData.Rows(0).Item("koj_gyoumu_kengen") & "', ")
        commandTextSb.AppendLine(" keiri_gyoumu_kengen='" & dtUPDData.Rows(0).Item("keiri_gyoumu_kengen") & "', ")
        commandTextSb.AppendLine(" hansoku_uri_kengen='" & dtUPDData.Rows(0).Item("hansoku_uri_kengen") & "', ")
        commandTextSb.AppendLine(" hattyuusyo_kanri_kengen='" & dtUPDData.Rows(0).Item("hattyuusyo_kanri_kengen") & "', ")
        commandTextSb.AppendLine(" kaiseki_master_kanri_kengen='" & dtUPDData.Rows(0).Item("kaiseki_master_kanri_kengen") & "', ")
        commandTextSb.AppendLine(" eigyou_master_kanri_kengen='" & dtUPDData.Rows(0).Item("eigyou_master_kanri_kengen") & "', ")
        commandTextSb.AppendLine(" kkk_master_kanri_kengen='" & dtUPDData.Rows(0).Item("kkk_master_kanri_kengen") & "', ")
        commandTextSb.AppendLine(" tyousaka_kanrisya_kengen='" & dtUPDData.Rows(0).Item("tyousaka_kanrisya_kengen") & "', ")
        commandTextSb.AppendLine(" kensa_gyoumu_kengen='" & dtUPDData.Rows(0).Item("kensa_gyoumu_kengen") & "', ")
        commandTextSb.AppendLine(" hanyou1_gyoumu_kengen='" & dtUPDData.Rows(0).Item("hanyou1_gyoumu_kengen") & "', ")
        commandTextSb.AppendLine(" hanyou2_gyoumu_kengen='" & dtUPDData.Rows(0).Item("hanyou2_gyoumu_kengen") & "', ")
        commandTextSb.AppendLine(" hanyou3_gyoumu_kengen='" & dtUPDData.Rows(0).Item("hanyou3_gyoumu_kengen") & "', ")
        commandTextSb.AppendLine(" system_kanrisya_kengen='" & dtUPDData.Rows(0).Item("system_kanrisya_kengen") & "', ")
        commandTextSb.AppendLine(" upd_login_user_id=@upd_login_user_id, ")
        commandTextSb.AppendLine(" upd_datetime=GETDATE() ")

        commandTextSb.AppendLine("WHERE account_no = " & account_no & "  ")





        'パラメータの設定
        With dtUPDData.Rows(0)
            If dtUPDData.Rows(0).Item("eigyou_man_kbn") <> "" Then
                paramList.Add(MakeParam("@eigyou_man_kbn", SqlDbType.Int, 4, .Item("eigyou_man_kbn")))
            Else
                paramList.Add(MakeParam("@eigyou_man_kbn", SqlDbType.Int, 4, DBNull.Value))
            End If
            If dtUPDData.Rows(0).Item("gyoumu_kbn") <> "" Then
                paramList.Add(MakeParam("@gyoumu_kbn", SqlDbType.Int, 4, .Item("gyoumu_kbn")))
            Else
                paramList.Add(MakeParam("@gyoumu_kbn", SqlDbType.Int, 4, DBNull.Value))
            End If
            paramList.Add(MakeParam("@busyo_cd", SqlDbType.VarChar, 4, .Item("busyo_cd")))
            paramList.Add(MakeParam("@ss_busyo_cd", SqlDbType.VarChar, 4, .Item("ss_busyo_cd")))
            paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, .Item("upd_login_user_id")))
            paramList.Add(MakeParam("@login_user_id", SqlDbType.VarChar, 30, .Item("user_id")))

        End With
        '更新されたデータセットを DB へ書き込み
        ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

        '終了処理
        commandTextSb = Nothing

        '戻り値をセット成功の場合
        UpdJibanNinsyou = True

    End Function

    ''' <summary>
    ''' 地盤認証マスタ連携管理テーブルを登録する。
    ''' </summary>
    ''' <param name="dtUPDData">登録項目のテーブル</param>
    ''' <returns>true or false</returns>
    Public Function InsJibanNinsyouRenkei(ByVal dtUPDData As KanrisyaMenuInquiryInputDataSet.updJibanNinsyouBusyoDataTable) As Boolean
        '戻り値
        InsJibanNinsyouRenkei = False

        Dim commandTextSb As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        commandTextSb.AppendLine(" INSERT INTO ")
        commandTextSb.AppendLine(" m_jiban_ninsyou_renkei ")
        commandTextSb.AppendLine(" WITH(UPDLOCK) ")
        commandTextSb.AppendLine(" (")
        commandTextSb.AppendLine("  login_user_id,")
        commandTextSb.AppendLine("  renkei_siji_cd,")
        commandTextSb.AppendLine("  sousin_jyky_cd,")
        commandTextSb.AppendLine("  sousin_kanry_datetime,")
        commandTextSb.AppendLine("  upd_login_user_id,")
        commandTextSb.AppendLine("  upd_datetime")
        commandTextSb.AppendLine(" ) ")
        commandTextSb.AppendLine(" VALUES( ")
        commandTextSb.AppendLine(" @login_user_id, ")
        commandTextSb.AppendLine(" @renkei_siji_cd, ")
        commandTextSb.AppendLine(" @sousin_jyky_cd, ")
        commandTextSb.AppendLine(" NULL, ")
        commandTextSb.AppendLine(" @upd_login_user_id, ")
        commandTextSb.AppendLine(" GETDATE() ")
        commandTextSb.AppendLine(" ) ")

        'パラメータの設定
        With dtUPDData.Rows(0)
            paramList.Add(MakeParam("@login_user_id", SqlDbType.VarChar, 30, .Item("user_id")))
            paramList.Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 4, .Item("renkei_siji_cd")))
            paramList.Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 4, .Item("sousin_jyky_cd")))
            paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, .Item("upd_login_user_id")))
        End With
        '更新されたデータセットを DB へ書き込み
        ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

        '終了処理
        commandTextSb = Nothing

        '戻り値をセット成功の場合
        InsJibanNinsyouRenkei = True

    End Function

    ''' <summary>
    ''' 地盤認証マスタ連携管理テーブルを更新する。
    ''' </summary>
    ''' <param name="dtUPDData">更新項目のテーブル</param>
    ''' <returns>true or false</returns>
    Public Function UpdJibanNinsyouRenkei(ByVal dtUPDData As KanrisyaMenuInquiryInputDataSet.updJibanNinsyouBusyoDataTable) As Boolean
        '戻り値
        UpdJibanNinsyouRenkei = False

        Dim commandTextSb As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        commandTextSb.AppendLine(" UPDATE ")
        commandTextSb.AppendLine(" m_jiban_ninsyou_renkei ")
        commandTextSb.AppendLine(" WITH(UPDLOCK) ")
        commandTextSb.AppendLine(" SET ")
        commandTextSb.AppendLine(" renkei_siji_cd=@renkei_siji_cd, ")
        commandTextSb.AppendLine(" sousin_jyky_cd=@sousin_jyky_cd, ")
        commandTextSb.AppendLine(" upd_login_user_id=@upd_login_user_id, ")
        commandTextSb.AppendLine(" upd_datetime=GETDATE() ")
        commandTextSb.AppendLine("WHERE login_user_id = @login_user_id  ")

        'パラメータの設定
        With dtUPDData.Rows(0)
            paramList.Add(MakeParam("@renkei_siji_cd", SqlDbType.Int, 4, .Item("renkei_siji_cd")))
            paramList.Add(MakeParam("@sousin_jyky_cd", SqlDbType.Int, 4, .Item("sousin_jyky_cd")))
            paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, .Item("upd_login_user_id")))
            paramList.Add(MakeParam("@login_user_id", SqlDbType.VarChar, 30, .Item("user_id")))

        End With
        '更新されたデータセットを DB へ書き込み
        ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

        '終了処理
        commandTextSb = Nothing

        '戻り値をセット成功の場合
        UpdJibanNinsyouRenkei = True

    End Function

End Class

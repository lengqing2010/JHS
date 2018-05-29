Imports System.Data.SqlClient
Imports System.Text
Imports Itis.ApplicationBlocks.Data.SQLHelper
Public Class KihonJyouhouDataAccess
    Private connStr As String = ConnectionManager.Instance.ConnectionString
    ''' <summary>基本情報を取得する</summary>
    Public Function SelKihonJyouhouInfo(ByVal strKameitenCd As String) As KihonJyouhouDataSet.KihonJyouhouTableDataTable
        Dim commandTextSb As New System.Text.StringBuilder


        Dim dsKihonJyouhou As New KihonJyouhouDataSet
        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine(" todouhuken_cd AS todouhuken_cd, ")
        commandTextSb.AppendLine(" nenkan_tousuu AS nenkan_tousuu, ")
        commandTextSb.AppendLine(" m_jhs_mailbox1.DisplayName AS eigyou_tantousya_mei, ")
        commandTextSb.AppendLine(" hikitugi_kanryou_date AS hikitugi_kanryou_date, ")
        commandTextSb.AppendLine(" m_jhs_mailbox2.DisplayName AS kyuu_eigyou_tantousya_mei, ")
        commandTextSb.AppendLine(" jisin_hosyou_flg AS jisin_hosyou_flg, ")
        commandTextSb.AppendLine(" jisin_hosyou_add_date AS jisin_hosyou_add_date, ")
        commandTextSb.AppendLine(" eigyou_tantousya_mei AS eigyou_tantousya_cd, ")
        commandTextSb.AppendLine(" kyuu_eigyou_tantousya_mei AS kyuu_eigyou_tantousya_cd, ")
        commandTextSb.AppendLine(" fuho_syoumeisyo_flg AS fuho_syoumeisyo_flg, ")
        commandTextSb.AppendLine(" fuho_syoumeisyo_kaisi_nengetu AS fuho_syoumeisyo_kaisi_nengetu, ")
        commandTextSb.AppendLine(" upd_datetime, ")
        commandTextSb.AppendLine(" null as upd_login_user_id, ")
        commandTextSb.AppendLine(" jiosaki_flg, ")
        commandTextSb.AppendLine(" koj_uri_syubetsu, ")
        commandTextSb.AppendLine(" koj_support_system, ")
        commandTextSb.AppendLine(" kameiten_seisiki_mei, ")
        commandTextSb.AppendLine(" kameiten_seisiki_mei_kana, ")
        '2013/11/01 李宇追加 ↓
        commandTextSb.AppendLine(" sds_jidoou_set_info, ") 'SDS自動設定情報
        '2013/11/01 李宇追加 ↑

        '==================2017/01/01 李松涛 追加 新築住宅引渡し（販売）件数 不動産売買件数 リフォーム前年度請負金額↓==========================
        commandTextSb.AppendLine(" shinchiku_jyuutaku_hikiwatashi_kensuu, ")
        commandTextSb.AppendLine(" fudousan_baibai_kensuu, ")
        commandTextSb.AppendLine(" reform_zennendo_ukeoi_kingaku ")
        '==================2017/01/01 李松涛 追加 新築住宅引渡し（販売）件数 不動産売買件数 リフォーム前年度請負金額↑==========================


        commandTextSb.AppendLine(" FROM  m_kameiten WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" LEFT JOIN  m_jhs_mailbox  AS  m_jhs_mailbox1  WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" ON m_jhs_mailbox1.PrimaryWindowsNTAccount=m_kameiten.eigyou_tantousya_mei ")
        commandTextSb.AppendLine(" LEFT JOIN  m_jhs_mailbox  AS  m_jhs_mailbox2  WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" ON m_jhs_mailbox2.PrimaryWindowsNTAccount=m_kameiten.kyuu_eigyou_tantousya_mei ")

        commandTextSb.AppendLine("WHERE kameiten_cd = @kameiten_cd  ")

        'パラメータの設定
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))


        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsKihonJyouhou, _
                    dsKihonJyouhou.KihonJyouhouTable.TableName, paramList.ToArray)

        Return dsKihonJyouhou.KihonJyouhouTable

    End Function
    ''' <summary>基本情報を更新する</summary>
    Public Function UpdKihonJyouhouInfo(ByVal dtKihonJyouhouData As KihonJyouhouDataSet.KihonJyouhouTableDataTable) As Boolean

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        commandTextSb.AppendLine(" UPDATE ")
        commandTextSb.AppendLine(" m_kameiten ")
        commandTextSb.AppendLine(" WITH(UPDLOCK) ")
        commandTextSb.AppendLine(" SET ")
        commandTextSb.AppendLine(" todouhuken_cd=@todouhuken_cd, ")
        commandTextSb.AppendLine(" nenkan_tousuu=@nenkan_tousuu, ")
        commandTextSb.AppendLine(" eigyou_tantousya_mei=@eigyou_tantousya_mei, ")
        commandTextSb.AppendLine(" hikitugi_kanryou_date=@hikitugi_kanryou_date, ")
        commandTextSb.AppendLine(" kyuu_eigyou_tantousya_mei=@kyuu_eigyou_tantousya_mei, ")
        commandTextSb.AppendLine(" jisin_hosyou_flg=@jisin_hosyou_flg, ")
        commandTextSb.AppendLine(" jisin_hosyou_add_date=@jisin_hosyou_add_date, ")
        commandTextSb.AppendLine(" upd_login_user_id=@upd_login_user_id, ")
        commandTextSb.AppendLine(" fuho_syoumeisyo_flg=@fuho_syoumeisyo_flg, ")
        commandTextSb.AppendLine(" jiosaki_flg=@jiosaki_flg, ")
        commandTextSb.AppendLine(" koj_uri_syubetsu=@koj_uri_syubetsu, ")
        commandTextSb.AppendLine(" koj_support_system=@koj_support_system, ")
        commandTextSb.AppendLine(" kameiten_seisiki_mei=@kameiten_seisiki_mei, ")
        commandTextSb.AppendLine(" kameiten_seisiki_mei_kana=@kameiten_seisiki_mei_kana, ")
        '2013/11/01 李宇追加 ↓
        'SDS自動設定情報
        commandTextSb.AppendLine(" sds_jidoou_set_info=@sds_jidoou_set_info, ")
        '2013/11/01 李宇追加 ↑




        If dtKihonJyouhouData.Rows(0).Item("fuho_syoumeisyo_kaisi_nengetu") <> "" Then
            commandTextSb.AppendLine(" fuho_syoumeisyo_kaisi_nengetu=@fuho_syoumeisyo_kaisi_nengetu, ")
            paramList.Add(MakeParam("@fuho_syoumeisyo_kaisi_nengetu", SqlDbType.VarChar, 10, dtKihonJyouhouData.Rows(0).Item("fuho_syoumeisyo_kaisi_nengetu") & "/01"))
        Else
            commandTextSb.AppendLine(" fuho_syoumeisyo_kaisi_nengetu=null, ")
        End If

        '==================2017/01/01 李松涛 追加 新築住宅引渡し（販売）件数 不動産売買件数 リフォーム前年度請負金額↓==========================
        commandTextSb.AppendLine(" shinchiku_jyuutaku_hikiwatashi_kensuu=@shinchiku_jyuutaku_hikiwatashi_kensuu, ")
        commandTextSb.AppendLine(" fudousan_baibai_kensuu=@fudousan_baibai_kensuu, ")
        commandTextSb.AppendLine(" reform_zennendo_ukeoi_kingaku=@reform_zennendo_ukeoi_kingaku, ")
        '==================2017/01/01 李松涛 追加 新築住宅引渡し（販売）件数 不動産売買件数 リフォーム前年度請負金額↑==========================



        commandTextSb.AppendLine(" upd_datetime=GETDATE() ")
        commandTextSb.AppendLine(" WHERE m_kameiten.kameiten_cd=@kameiten_cd ")
        With dtKihonJyouhouData.Rows(0)

            'パラメータの設定
            paramList.Add(MakeParam("@todouhuken_cd", SqlDbType.VarChar, 2, .Item("todouhuken_cd")))
            paramList.Add(MakeParam("@nenkan_tousuu", SqlDbType.VarChar, 5, .Item("nenkan_tousuu")))
            paramList.Add(MakeParam("@eigyou_tantousya_mei", SqlDbType.VarChar, 64, .Item("eigyou_tantousya_mei")))
            If .Item("hikitugi_kanryou_date").ToString.Trim = "" Then
                paramList.Add(MakeParam("@hikitugi_kanryou_date", SqlDbType.VarChar, 16, DBNull.Value))

            Else
                paramList.Add(MakeParam("@hikitugi_kanryou_date", SqlDbType.VarChar, 16, .Item("hikitugi_kanryou_date")))

            End If
            paramList.Add(MakeParam("@kyuu_eigyou_tantousya_mei", SqlDbType.VarChar, 64, .Item("kyuu_eigyou_tantousya_mei")))
            If .Item("jisin_hosyou_flg") = "0" Then
                paramList.Add(MakeParam("@jisin_hosyou_flg", SqlDbType.Int, 4, DBNull.Value))
            Else
                paramList.Add(MakeParam("@jisin_hosyou_flg", SqlDbType.Int, 4, .Item("jisin_hosyou_flg")))
            End If
            If .Item("jisin_hosyou_add_date").ToString.Trim = "" Then
                paramList.Add(MakeParam("@jisin_hosyou_add_date", SqlDbType.VarChar, 16, DBNull.Value))

            Else
                paramList.Add(MakeParam("@jisin_hosyou_add_date", SqlDbType.VarChar, 16, .Item("jisin_hosyou_add_date")))

            End If
            paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, .Item("upd_login_user_id")))
            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, .Item("kameiten_cd")))

            paramList.Add(MakeParam("@fuho_syoumeisyo_flg", SqlDbType.VarChar, 2, .Item("fuho_syoumeisyo_flg")))
            If .Item("jiosaki_flg").ToString.Trim = "" Then
                paramList.Add(MakeParam("@jiosaki_flg", SqlDbType.VarChar, 16, DBNull.Value))
            Else
                paramList.Add(MakeParam("@jiosaki_flg", SqlDbType.VarChar, 16, .Item("jiosaki_flg")))
            End If
            If .Item("koj_uri_syubetsu").ToString.Trim = "" Then
                paramList.Add(MakeParam("@koj_uri_syubetsu", SqlDbType.VarChar, 16, DBNull.Value))
            Else
                paramList.Add(MakeParam("@koj_uri_syubetsu", SqlDbType.VarChar, 16, .Item("koj_uri_syubetsu")))
            End If
            If .Item("koj_support_system").ToString.Trim = "" Then
                paramList.Add(MakeParam("@koj_support_system", SqlDbType.VarChar, 16, DBNull.Value))
            Else
                paramList.Add(MakeParam("@koj_support_system", SqlDbType.VarChar, 16, .Item("koj_support_system")))
            End If
            If .Item("kameiten_seisiki_mei").ToString.Trim = "" Then
                paramList.Add(MakeParam("@kameiten_seisiki_mei", SqlDbType.VarChar, 80, DBNull.Value))

            Else
                paramList.Add(MakeParam("@kameiten_seisiki_mei", SqlDbType.VarChar, 80, .Item("kameiten_seisiki_mei")))

            End If
            If .Item("kameiten_seisiki_mei_kana").ToString.Trim = "" Then
                paramList.Add(MakeParam("@kameiten_seisiki_mei_kana", SqlDbType.VarChar, 40, DBNull.Value))

            Else
                paramList.Add(MakeParam("@kameiten_seisiki_mei_kana", SqlDbType.VarChar, 40, .Item("kameiten_seisiki_mei_kana")))

            End If
            '2013/11/01 李宇追加 ↓
            'SDS自動設定情報
            If .Item("sds_jidoou_set_info").ToString.Trim = "" Then
                paramList.Add(MakeParam("@sds_jidoou_set_info", SqlDbType.Int, 10, DBNull.Value))
            Else
                paramList.Add(MakeParam("@sds_jidoou_set_info", SqlDbType.Int, 10, .Item("sds_jidoou_set_info")))
            End If
            '2013/11/01 李宇追加 ↑

            '==================2017/01/01 李松涛 追加 新築住宅引渡し（販売）件数 不動産売買件数 リフォーム前年度請負金額↓==========================
            paramList.Add(MakeParam("@shinchiku_jyuutaku_hikiwatashi_kensuu", SqlDbType.VarChar, 10, .Item("shinchiku_jyuutaku_hikiwatashi_kensuu")))
            paramList.Add(MakeParam("@fudousan_baibai_kensuu", SqlDbType.VarChar, 8, .Item("fudousan_baibai_kensuu")))
            paramList.Add(MakeParam("@reform_zennendo_ukeoi_kingaku", SqlDbType.VarChar, 20, .Item("reform_zennendo_ukeoi_kingaku")))
            '==================2017/01/01 李松涛 追加 新築住宅引渡し（販売）件数 不動産売買件数 リフォーム前年度請負金額↑==========================


        End With

        '更新されたデータセットを DB へ書き込み
        ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

        '終了処理
        commandTextSb = Nothing

        '戻り値をセット成功の場合
        UpdKihonJyouhouInfo = True

    End Function
End Class

Imports System
Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' FC申込データの取得クラス
''' </summary>
''' <remarks></remarks>
Public Class FcMousikomiDataAccess

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

#Region "メンバ変数"
    Dim connStr As String = ConnectionManager.Instance.ConnectionString
    Dim SLogic As New StringLogic
    Dim cmnDtAcc As New CmnDataAccess
#End Region

#Region "FC申込データの取得"
    ''' <summary>
    ''' FC申込検索画面/FC申込データを取得します
    ''' </summary>
    ''' <param name="keyRec">検索KEYレコードクラス</param>
    ''' <returns>FC申込データテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetSearchMousikomiIf(ByVal keyRec As FcMousikomiKeyRecord) As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSearchMousikomiIf", keyRec)

        'SQL文の生成
        Dim cmdTextSb As New StringBuilder
        'パラメータ
        Dim cmdParams() As SqlParameter = Me.GetMousikomiSqlCmnParams(keyRec)

        'SELECT句
        Dim strCmnSelect As String = Me.GetCmnSqlSelect()
        'TABLE句
        Dim strCmnTable As String = Me.GetCmnSqlTable()
        'WHERE句
        Dim strCmnWhere As String = Me.GetCmnSqlWhere(keyRec)
        'ORDER BY句
        Dim strCmnOrderBy As String = Me.GetCmnSqlOrderBy()

        '****************
        '* SELECT項目
        '****************
        cmdTextSb.Append(strCmnSelect)

        '****************
        '* TABLE項目
        '****************
        cmdTextSb.Append(strCmnTable)

        '****************
        '* WHERE句
        '****************
        cmdTextSb.Append(strCmnWhere)

        '***********************************************************************
        ' ORDER BY句（登録日時）
        '***********************************************************************
        cmdTextSb.Append(strCmnOrderBy)

        'データ取得＆返却
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)
    End Function

    ''' <summary>
    ''' FC申込修正画面/FC申込データをPKで取得します
    ''' </summary>
    ''' <param name="strMousikomiNo">主キー項目値</param>
    ''' <returns>データテーブル</returns>
    ''' <remarks>検索キーレコードクラスをKEYにして取得</remarks>
    Public Function GetMousikomiIfRec(ByVal strMousikomiNo As String) As DataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetMousikomiIfRec" _
                                                    , strMousikomiNo _
                                                    )

        'SQLクエリ生成
        Dim cmdTextSb As New StringBuilder()
        'パラメータ
        Dim cmdParams() As SqlClient.SqlParameter

        'SELECT句
        Dim strCmnSelect As String = Me.GetCmnSqlSelect()
        'TABLE句
        Dim strCmnTable As String = Me.GetCmnSqlTable()

        '****************
        '* SELECT項目
        '****************
        cmdTextSb.Append(strCmnSelect)

        '****************
        '* TABLE項目
        '****************
        cmdTextSb.Append(strCmnTable)

        '****************
        '* WHERE項目
        '****************
        cmdTextSb.Append("  WHERE mi.mousikomi_no = " & DBprmMouskomiNo)

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam(DBprmMouskomiNo, SqlDbType.BigInt, 8, strMousikomiNo) _
        }

        'データ取得＆返却
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)

    End Function
#End Region

#Region "FC申込データの更新"
    ''' <summary>
    ''' FC申込テーブルへデータを反映します
    ''' </summary>
    ''' <param name="sql">更新SQL</param>
    ''' <returns>更新に必要なパラメータ情報</returns>
    ''' <remarks>★本処理は固有のテーブルに依存しない為、各種テーブルの更新に使用可</remarks>
    Public Function UpdateMousikomiData(ByVal sql As String, _
                                    ByVal paramList As List(Of ParamRecord)) As Boolean

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".UpdateMousikomiData", _
                            sql, _
                            paramList)

        Dim intResult As Integer = 0
        Dim cmdParams(paramList.Count - 1) As SqlClient.SqlParameter
        Dim i As Integer

        For i = 0 To paramList.Count - 1
            Dim rec As ParamRecord = paramList(i)
            ' 必要な情報をセット
            cmdParams(i) = SQLHelper.MakeParam(rec.Param, rec.DbType, rec.ParamLength, rec.SetData)
        Next

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    sql, _
                                    cmdParams)

        ' 更新に失敗した場合、False
        If intResult < 1 Then
            Return False
        End If

        Return True

    End Function
#End Region

#Region "FC申込データの新規受注"
    ''' <summary>
    ''' FC申込データを地盤データと紐付けします
    ''' </summary>
    ''' <param name="miRec">FC申込レコード</param>
    ''' <returns>更新件数</returns>
    ''' <remarks></remarks>
    Public Function UpdMousikomiJibanLink(ByVal miRec As FcMousikomiRecord) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".UpdMousikomiData", miRec)

        Dim intResult As Integer
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        'クエリ生成
        cmdTextSb.Append(" UPDATE ")
        cmdTextSb.Append("      [dbo].[FcMousikomiIF] ")
        cmdTextSb.Append(" SET ")
        cmdTextSb.Append("     status = " & DBprmStatus)
        cmdTextSb.Append("    , kousinsya = " & DBprmKousinsya)
        cmdTextSb.Append("    , upd_login_user_id = " & DBprmUpdUserID)
        cmdTextSb.Append("    , upd_datetime = " & DBprmUpdDatetime)
        cmdTextSb.Append(" WHERE ")
        cmdTextSb.Append("      mousikomi_no = " & DBprmMouskomiNo)

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam(DBprmMouskomiNo, SqlDbType.BigInt, 8, miRec.MousikomiNo), _
            SQLHelper.MakeParam(DBprmStatus, SqlDbType.Char, 3, miRec.Status), _
            SQLHelper.MakeParam(DBprmKousinsya, SqlDbType.VarChar, 30, miRec.Kousinsya), _
            SQLHelper.MakeParam(DBprmUpdUserID, SqlDbType.VarChar, 30, miRec.UpdLoginUserId), _
            SQLHelper.MakeParam(DBprmUpdDatetime, SqlDbType.DateTime, 16, DateTime.Now)}

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    cmdTextSb.ToString(), _
                                    cmdParams)
        Return intResult
    End Function

    ''' <summary>
    ''' 地盤項目と同等の項目については、FC申込データを地盤データに移送します
    ''' ※ステータスも同時更新
    ''' </summary>
    ''' <param name="miRec">FC申込レコード</param>
    ''' <returns>更新件数</returns>
    ''' <remarks></remarks>
    Public Function UpdMousikomiData(ByVal miRec As FcMousikomiRecord) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".UpdMousikomiData", miRec)

        Dim intResult As Integer
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        'クエリ生成
        cmdTextSb.Append(" UPDATE ")
        cmdTextSb.Append("      [jhs_sys].[t_jiban] ")
        cmdTextSb.Append(" SET ")
        cmdTextSb.Append("     kameiten_cd = MI.kameiten_cd ")
        cmdTextSb.Append("    , irai_date = CONVERT(DATETIME, CONVERT(VARCHAR, MI.irai_date, 111), 111) ")
        cmdTextSb.Append("    , keiyu = MI.keiyu ")
        cmdTextSb.Append("    , keiyaku_no = MI.keiyaku_no ")
        cmdTextSb.Append("    , tys_renrakusaki_atesaki_mei = MI.tys_renrakusaki_atesaki_mei ")
        cmdTextSb.Append("    , irai_tantousya_mei = MI.tys_renrakusaki_tantou_mei ")
        cmdTextSb.Append("    , tys_renrakusaki_tel = MI.tys_renrakusaki_tel ")
        cmdTextSb.Append("    , tys_renrakusaki_fax = MI.tys_renrakusaki_fax ")
        cmdTextSb.Append("    , tys_renrakusaki_mail = MI.tys_renrakusaki_mail ")
        cmdTextSb.Append("    , sesyu_mei = MI.sesyu_mei ")
        cmdTextSb.Append("    , jutyuu_bukken_mei = MI.sesyu_mei ")
        cmdTextSb.Append("    , bukken_jyuusyo1 = MI.bukken_jyuusyo1 ")
        cmdTextSb.Append("    , bukken_jyuusyo2=MI.bukken_jyuusyo2 ")
        cmdTextSb.Append("    , bukken_jyuusyo3=MI.bukken_jyuusyo3 ")
        cmdTextSb.Append("    , tys_kibou_date=MI.tys_kibou_date ")
        cmdTextSb.Append("    , tys_kibou_jikan=MI.tys_kibou_kbn + ' ' + MI.tys_kaisi_kibou_jikan ")
        cmdTextSb.Append("    , ks_tyakkou_yotei_from_date=MI.ks_tyakkou_yotei_from_date ")
        cmdTextSb.Append("    , ks_tyakkou_yotei_to_date=MI.ks_tyakkou_yotei_to_date ")
        cmdTextSb.Append("    , tatiai_umu=MI.tatiai_umu ")
        cmdTextSb.Append("    , tatiaisya_cd=MI.tatiaisya_cd ")
        cmdTextSb.Append("    , kouzou = MI.kouzou ")
        cmdTextSb.Append("    , kouzou_memo = MI.kouzou_memo ")
        cmdTextSb.Append("    , sintiku_tatekae = MI.sintiku_tatekae ")
        cmdTextSb.Append("    , kaisou = MI.kaisou_tijyou ")
        cmdTextSb.Append("    , tatemono_youto_no = MI.tatemono_youto_no ")
        cmdTextSb.Append("    , syako = MI.syako ")
        cmdTextSb.Append("    , sekkei_kyoyou_sijiryoku = MI.sekkei_kyoyou_sijiryoku ")
        cmdTextSb.Append("    , negiri_hukasa = MI.negiri_hukasa ")
        cmdTextSb.Append("    , yotei_ks = MI.yotei_ks ")
        cmdTextSb.Append("    , yotei_ks_memo = MI.yotei_ks_memo ")
        cmdTextSb.Append("    , bikou = MI.bikou ")
        cmdTextSb.Append("    , douji_irai_tousuu = MI.douji_irai_tousuu ")
        cmdTextSb.Append("    , yotei_morituti_atusa = MI.yotei_morituti_atusa ")
        cmdTextSb.Append("    , tys_kaisya_cd = MI.tantou_tys_kaisya_cd ")
        cmdTextSb.Append("    , tys_kaisya_jigyousyo_cd = MI.tantou_tys_kaisya_jigyousyo_cd ")
        cmdTextSb.Append("    , tys_houhou = MI.tys_houhou ")
        cmdTextSb.Append("    , sesyu_mei_umu = MI.sesyu_mei_umu ")
        cmdTextSb.Append("    , bukken_nayose_cd = ( ")
        cmdTextSb.Append("          CASE WHEN MI.bukken_nayose_cd IS NULL OR MI.bukken_nayose_cd = '' THEN MI.kbn + MI.hosyousyo_no ")
        cmdTextSb.Append("          ELSE MI.bukken_nayose_cd ")
        cmdTextSb.Append("      END) ")
        cmdTextSb.Append("    , kousinsya = MI.kousinsya ")
        cmdTextSb.Append("    , upd_login_user_id = MI.upd_login_user_id ")
        cmdTextSb.Append("    , upd_datetime = MI.upd_datetime ")

        cmdTextSb.Append(" FROM ")
        cmdTextSb.Append("      [jhs_sys].[t_jiban] AS TJ ")
        cmdTextSb.Append("           INNER JOIN [dbo].[FcMousikomiIF] AS MI ")
        cmdTextSb.Append("             ON TJ.kbn = MI.kbn ")
        cmdTextSb.Append("            AND TJ.hosyousyo_no = MI.hosyousyo_no ")

        cmdTextSb.Append(" WHERE ")
        cmdTextSb.Append("      TJ.kbn = " & DBprmKbn)
        cmdTextSb.Append("  AND TJ.hosyousyo_no = " & DBprmBangou)
        cmdTextSb.Append("  AND MI.mousikomi_no = " & DBprmMouskomiNo)

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam(DBprmKbn, SqlDbType.Char, 1, miRec.Kbn), _
            SQLHelper.MakeParam(DBprmMouskomiNo, SqlDbType.BigInt, 8, miRec.MousikomiNo), _
            SQLHelper.MakeParam(DBprmBangou, SqlDbType.VarChar, 10, miRec.HosyousyoNo), _
            SQLHelper.MakeParam(DBprmStatus, SqlDbType.Char, 3, miRec.Status), _
            SQLHelper.MakeParam(DBprmKousinsya, SqlDbType.VarChar, 30, miRec.Kousinsya), _
            SQLHelper.MakeParam(DBprmUpdUserID, SqlDbType.VarChar, 30, miRec.UpdLoginUserId), _
            SQLHelper.MakeParam(DBprmUpdDatetime, SqlDbType.DateTime, 16, DateTime.Now)}

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    cmdTextSb.ToString(), _
                                    cmdParams)
        Return intResult
    End Function

#End Region

#Region "共通クエリ"

#Region "SELECT句"
    ''' <summary>
    ''' 申込データ取得用の共通SELECTクエリを取得
    ''' </summary>
    ''' <returns>申込データ取得用の共通SELECTクエリを取得</returns>
    ''' <remarks></remarks>
    Private Function GetCmnSqlSelect() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetCmnSqlSelect")

        'SQL文の生成
        Dim cmdTextSb As New StringBuilder

        '****************
        '* SELECT項目
        '****************
        cmdTextSb.Append(" SELECT ")
        cmdTextSb.Append("      mi.mousikomi_no ")
        cmdTextSb.Append("    , mi.status ")
        cmdTextSb.Append("    , mi.report_send_code ")
        cmdTextSb.Append("    , mi.report_recv_code ")
        cmdTextSb.Append("    , mi.send_sts ")
        cmdTextSb.Append("    , mi.recv_sts ")
        cmdTextSb.Append("    , mi.kbn ")
        cmdTextSb.Append("    , mi.hosyousyo_no ")
        cmdTextSb.Append("    , case mi.status ")
        cmdTextSb.Append("          when " & EarthConst.MOUSIKOMI_STATUS_ZUMI_JUTYUU & " then j.kameiten_cd ")
        cmdTextSb.Append("          else mi.kameiten_cd ")
        cmdTextSb.Append("      end AS kameiten_cd ")
        cmdTextSb.Append("    , k.kameiten_mei1 ")
        cmdTextSb.Append("    , mi.syouhin_cd ")
        cmdTextSb.Append("    , s.syouhin_mei ")
        cmdTextSb.Append("    , mi.irai_date ")
        cmdTextSb.Append("    , case mi.status ")
        cmdTextSb.Append("        when " & EarthConst.MOUSIKOMI_STATUS_ZUMI_JUTYUU & " then j.keiyu ")
        cmdTextSb.Append("        else mi.keiyu ")
        cmdTextSb.Append("      end AS keiyu ")
        cmdTextSb.Append("    , case mi.status ")
        cmdTextSb.Append("        when " & EarthConst.MOUSIKOMI_STATUS_ZUMI_JUTYUU & " then j.keiyaku_no ")
        cmdTextSb.Append("        else mi.keiyaku_no ")
        cmdTextSb.Append("      end AS keiyaku_no ")
        cmdTextSb.Append("    , case mi.status ")
        cmdTextSb.Append("        when " & EarthConst.MOUSIKOMI_STATUS_ZUMI_JUTYUU & " then j.tys_renrakusaki_tantou_mei ")
        cmdTextSb.Append("        else mi.tys_renrakusaki_tantou_mei ")
        cmdTextSb.Append("      end AS tys_renrakusaki_tantou_mei ")
        cmdTextSb.Append("    , mi.tantousya_renrakusaki_tel ")
        cmdTextSb.Append("    , mi.tys_renrakusaki_atesaki_mei ")
        cmdTextSb.Append("    , case mi.status ")
        cmdTextSb.Append("        when " & EarthConst.MOUSIKOMI_STATUS_ZUMI_JUTYUU & " then j.tys_renrakusaki_tel ")
        cmdTextSb.Append("        else mi.tys_renrakusaki_tel ")
        cmdTextSb.Append("      end AS tys_renrakusaki_tel ")
        cmdTextSb.Append("    , case mi.status ")
        cmdTextSb.Append("        when " & EarthConst.MOUSIKOMI_STATUS_ZUMI_JUTYUU & " then j.tys_renrakusaki_fax ")
        cmdTextSb.Append("        else mi.tys_renrakusaki_fax ")
        cmdTextSb.Append("      end AS tys_renrakusaki_fax ")
        cmdTextSb.Append("    , case mi.status ")
        cmdTextSb.Append("        when " & EarthConst.MOUSIKOMI_STATUS_ZUMI_JUTYUU & " then j.tys_renrakusaki_mail ")
        cmdTextSb.Append("        else mi.tys_renrakusaki_mail ")
        cmdTextSb.Append("      end AS tys_renrakusaki_mail ")
        cmdTextSb.Append("    , mi.bukken_mei_kana ")
        cmdTextSb.Append("    , case mi.status ")
        cmdTextSb.Append("        when " & EarthConst.MOUSIKOMI_STATUS_ZUMI_JUTYUU & " then j.sesyu_mei ")
        cmdTextSb.Append("        else mi.sesyu_mei ")
        cmdTextSb.Append("      end AS sesyu_mei ")
        cmdTextSb.Append("    , mi.tys_basyo_yuubin_1 ")
        cmdTextSb.Append("    , mi.tys_basyo_yuubin_2 ")
        cmdTextSb.Append("    , mi.tys_basyo_todoufuken ")
        cmdTextSb.Append("    , case mi.status ")
        cmdTextSb.Append("        when " & EarthConst.MOUSIKOMI_STATUS_ZUMI_JUTYUU & " then j.bukken_jyuusyo1 ")
        cmdTextSb.Append("        else mi.bukken_jyuusyo1 ")
        cmdTextSb.Append("      end AS bukken_jyuusyo1 ")
        cmdTextSb.Append("    , case mi.status ")
        cmdTextSb.Append("        when " & EarthConst.MOUSIKOMI_STATUS_ZUMI_JUTYUU & " then j.bukken_jyuusyo2 ")
        cmdTextSb.Append("        else mi.bukken_jyuusyo2 ")
        cmdTextSb.Append("      end AS bukken_jyuusyo2 ")
        cmdTextSb.Append("    , case mi.status ")
        cmdTextSb.Append("        when " & EarthConst.MOUSIKOMI_STATUS_ZUMI_JUTYUU & " then j.bukken_jyuusyo3 ")
        cmdTextSb.Append("        else mi.bukken_jyuusyo3 ")
        cmdTextSb.Append("      end AS bukken_jyuusyo3 ")
        cmdTextSb.Append("    , case mi.status ")
        cmdTextSb.Append("        when " & EarthConst.MOUSIKOMI_STATUS_ZUMI_JUTYUU & " then j.tys_kibou_date ")
        cmdTextSb.Append("        else mi.tys_kibou_date ")
        cmdTextSb.Append("      end AS tys_kibou_date ")
        cmdTextSb.Append("    , mi.tys_kibou_kbn ")
        cmdTextSb.Append("    , mi.tys_kaisi_kibou_jikan ")
        cmdTextSb.Append("    , case mi.status ")
        cmdTextSb.Append("        when " & EarthConst.MOUSIKOMI_STATUS_ZUMI_JUTYUU & " then j.ks_tyakkou_yotei_from_date ")
        cmdTextSb.Append("        else mi.ks_tyakkou_yotei_from_date ")
        cmdTextSb.Append("      end AS ks_tyakkou_yotei_from_date ")
        cmdTextSb.Append("    , case mi.status ")
        cmdTextSb.Append("        when " & EarthConst.MOUSIKOMI_STATUS_ZUMI_JUTYUU & " then j.ks_tyakkou_yotei_to_date ")
        cmdTextSb.Append("        else mi.ks_tyakkou_yotei_to_date ")
        cmdTextSb.Append("      end AS ks_tyakkou_yotei_to_date ")
        cmdTextSb.Append("    , case mi.status ")
        cmdTextSb.Append("        when " & EarthConst.MOUSIKOMI_STATUS_ZUMI_JUTYUU & " then j.tatiai_umu ")
        cmdTextSb.Append("        else mi.tatiai_umu ")
        cmdTextSb.Append("      end AS tatiai_umu ")
        cmdTextSb.Append("    , mi.tatiaisya_cd ")
        cmdTextSb.Append("    , mi.tatiaisya_sonota_hosoku ")
        cmdTextSb.Append("    , mi.sds_kibou ")
        cmdTextSb.Append("    , case mi.status ")
        cmdTextSb.Append("        when " & EarthConst.MOUSIKOMI_STATUS_ZUMI_JUTYUU & " then j.kouzou ")
        cmdTextSb.Append("        else mi.kouzou ")
        cmdTextSb.Append("      end AS kouzou ")
        cmdTextSb.Append("    , case mi.status ")
        cmdTextSb.Append("        when " & EarthConst.MOUSIKOMI_STATUS_ZUMI_JUTYUU & " then j.kouzou_memo ")
        cmdTextSb.Append("        else mi.kouzou_memo ")
        cmdTextSb.Append("      end AS kouzou_memo ")
        cmdTextSb.Append("    , case mi.status ")
        cmdTextSb.Append("        when " & EarthConst.MOUSIKOMI_STATUS_ZUMI_JUTYUU & " then j.sintiku_tatekae ")
        cmdTextSb.Append("        else mi.sintiku_tatekae ")
        cmdTextSb.Append("      end AS sintiku_tatekae ")
        cmdTextSb.Append("    , mi.nobeyuka_menseki ")
        cmdTextSb.Append("    , mi.kentiku_menseki ")
        cmdTextSb.Append("    , mi.kaisou_tijyou ")
        cmdTextSb.Append("    , mi.kaisou_tika ")
        cmdTextSb.Append("    , case mi.status ")
        cmdTextSb.Append("        when " & EarthConst.MOUSIKOMI_STATUS_ZUMI_JUTYUU & " then j.syako ")
        cmdTextSb.Append("        else mi.syako ")
        cmdTextSb.Append("      end AS syako ")
        cmdTextSb.Append("    , mi.tatemono_youto_no ")
        cmdTextSb.Append("    , mi.tmyt_no_tenpo_youto ")
        cmdTextSb.Append("    , mi.tmyt_no_sonota_youto ")
        cmdTextSb.Append("    , mi.tiiki_tokusei ")
        cmdTextSb.Append("    , case mi.status ")
        cmdTextSb.Append("        when " & EarthConst.MOUSIKOMI_STATUS_ZUMI_JUTYUU & " then j.sekkei_kyoyou_sijiryoku ")
        cmdTextSb.Append("        else mi.sekkei_kyoyou_sijiryoku ")
        cmdTextSb.Append("      end AS sekkei_kyoyou_sijiryoku ")
        cmdTextSb.Append("    , case mi.status ")
        cmdTextSb.Append("        when " & EarthConst.MOUSIKOMI_STATUS_ZUMI_JUTYUU & " then j.negiri_hukasa ")
        cmdTextSb.Append("        else mi.negiri_hukasa ")
        cmdTextSb.Append("      end AS negiri_hukasa ")
        cmdTextSb.Append("    , case mi.status ")
        cmdTextSb.Append("        when " & EarthConst.MOUSIKOMI_STATUS_ZUMI_JUTYUU & " then j.yotei_ks ")
        cmdTextSb.Append("        else mi.yotei_ks ")
        cmdTextSb.Append("      end AS yotei_ks ")
        cmdTextSb.Append("    , mi.nuno_ks_base_w ")
        cmdTextSb.Append("    , case mi.status ")
        cmdTextSb.Append("        when " & EarthConst.MOUSIKOMI_STATUS_ZUMI_JUTYUU & " then j.yotei_ks_memo ")
        cmdTextSb.Append("        else mi.yotei_ks_memo ")
        cmdTextSb.Append("      end AS yotei_ks_memo ")
        cmdTextSb.Append("    , mi.yotei_ks_tatiagari_takasa ")
        cmdTextSb.Append("    , mi.skt_douro_haba ")
        cmdTextSb.Append("    , mi.tuukou_fuka_syaryou_flg ")
        cmdTextSb.Append("    , mi.douro_kisei_umu ")
        cmdTextSb.Append("    , mi.takasa_syougai_umu ")
        cmdTextSb.Append("    , mi.densen_umu ")
        cmdTextSb.Append("    , mi.tonneru_umu ")
        cmdTextSb.Append("    , mi.sktn_kouteisa ")
        cmdTextSb.Append("    , mi.sktn_kouteisa_hosoku ")
        cmdTextSb.Append("    , mi.slope_umu ")
        cmdTextSb.Append("    , mi.slope_hosoku ")
        cmdTextSb.Append("    , mi.hannyuu_jykn_sonota ")
        cmdTextSb.Append("    , mi.skt_zenreki_takuti ")
        cmdTextSb.Append("    , mi.skt_zenreki_ta ")
        cmdTextSb.Append("    , mi.skt_zenreki_hatake ")
        cmdTextSb.Append("    , mi.skt_zenreki_syokuju ")
        cmdTextSb.Append("    , mi.skt_zenreki_zouki ")
        cmdTextSb.Append("    , mi.skt_zenreki_tyuusyajyou ")
        cmdTextSb.Append("    , mi.skt_zenreki_kantakuti ")
        cmdTextSb.Append("    , mi.skt_zenreki_koujyouato ")
        cmdTextSb.Append("    , mi.skt_zenreki_sonota ")
        cmdTextSb.Append("    , mi.skt_zenreki_sonota_hosoku ")
        cmdTextSb.Append("    , mi.takuti_zousei_kikan ")
        cmdTextSb.Append("    , mi.zousei_gessuu ")
        cmdTextSb.Append("    , mi.kiri_mori_kbn ")
        cmdTextSb.Append("    , mi.kison_tatemono_umu ")
        cmdTextSb.Append("    , mi.ido_umu ")
        cmdTextSb.Append("    , mi.jyouka_genkyou_umu ")
        cmdTextSb.Append("    , mi.jyouka_yotei_umu ")
        cmdTextSb.Append("    , mi.jinawa ")
        cmdTextSb.Append("    , mi.kyoukai_kui ")
        cmdTextSb.Append("    , mi.skt_genkyou_sarati ")
        cmdTextSb.Append("    , mi.skt_genkyou_tyuusyajyou ")
        cmdTextSb.Append("    , mi.skt_genkyou_noukouti ")
        cmdTextSb.Append("    , mi.skt_genkyou_sonota ")
        cmdTextSb.Append("    , mi.skt_genkyou_sonota_hosoku ")
        cmdTextSb.Append("    , case mi.status ")
        cmdTextSb.Append("        when " & EarthConst.MOUSIKOMI_STATUS_ZUMI_JUTYUU & " then j.bikou ")
        cmdTextSb.Append("        else mi.bikou ")
        cmdTextSb.Append("      end AS bikou ")
        cmdTextSb.Append("    , case mi.status ")
        cmdTextSb.Append("        when " & EarthConst.MOUSIKOMI_STATUS_ZUMI_JUTYUU & " then j.douji_irai_tousuu ")
        cmdTextSb.Append("        else mi.douji_irai_tousuu ")
        cmdTextSb.Append("      end AS douji_irai_tousuu ")
        cmdTextSb.Append("    , mi.mrtt_tys_mae_jissi_zumi_morituti_atusa ")
        cmdTextSb.Append("    , mi.yotei_morituti_atusa ")
        cmdTextSb.Append("    , mi.yhk_l_gata ")
        cmdTextSb.Append("    , mi.yhk_gyaku_l_gata ")
        cmdTextSb.Append("    , mi.yhk_kanti_block_jyuuryoku_siki ")
        cmdTextSb.Append("    , mi.yhk_cb ")
        cmdTextSb.Append("    , mi.yhk_kisetu_zumi ")
        cmdTextSb.Append("    , mi.yhk_sinsetu_yotei ")
        cmdTextSb.Append("    , mi.yhk_setti_keika_nensuu ")
        cmdTextSb.Append("    , mi.yhk_takasa ")
        cmdTextSb.Append("    , mi.yhk_keikaku_takasa ")
        cmdTextSb.Append("    , mi.yhk_yakusyo_kakunin ")
        cmdTextSb.Append("    , mi.haturi_youhi ")
        cmdTextSb.Append("    , mi.tantou_tys_kaisya_cd ")
        cmdTextSb.Append("    , mi.tantou_tys_kaisya_jigyousyo_cd ")
        cmdTextSb.Append("    , tysK1.tys_kaisya_mei AS tantou_tys_kaisya_mei ")
        cmdTextSb.Append("    , mi.mousikomi_tys_kaisya_cd ")
        cmdTextSb.Append("    , mi.mousikomi_tys_kaisya_jigyousyo_cd ")
        cmdTextSb.Append("    , tysK2.tys_kaisya_mei AS mousikomi_tys_kaisya_mei ")
        cmdTextSb.Append("    , mi.tys_kaisya_cd_tantousya ")
        cmdTextSb.Append("    , mi.you_tyuui_jouhou ")
        cmdTextSb.Append("    , mi.tys_houhou ")
        cmdTextSb.Append("    , th.tys_houhou_mei ")
        cmdTextSb.Append("    , mi.sesyu_mei_umu ")
        cmdTextSb.Append("    , mi.bukken_nayose_cd ")
        cmdTextSb.Append("    , mi.kousinsya ")
        cmdTextSb.Append("    , ISNULL(mi.upd_datetime, mi.add_datetime) AS upd_datetime ")
        cmdTextSb.Append("    , ISNULL(mi.upd_login_user_id, mi.add_login_user_id) AS upd_login_user_id ")

        Return cmdTextSb.ToString
    End Function
#End Region

#Region "TABLE句"
    ''' <summary>
    ''' 請求データ取得用の共通TABLEクエリを取得
    ''' </summary>
    ''' <returns>申込データ取得用の共通TABLEクエリを取得</returns>
    ''' <remarks></remarks>
    Private Function GetCmnSqlTable() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetCmnSqlTable")

        'SQL文の生成
        Dim cmdTextSb As New StringBuilder

        '****************
        '* TABLE項目
        '****************
        cmdTextSb.Append(" FROM ")
        cmdTextSb.Append("      dbo.FcMousikomiIF mi WITH (READCOMMITTED) ")
        cmdTextSb.Append("           LEFT OUTER JOIN t_jiban j WITH (READCOMMITTED) ")
        cmdTextSb.Append("             ON mi.kbn = j.kbn ")
        cmdTextSb.Append("            AND mi.hosyousyo_no = j.hosyousyo_no ")
        cmdTextSb.Append("           LEFT OUTER JOIN m_kameiten k WITH (READCOMMITTED) ")
        cmdTextSb.Append("             ON ISNULL(j.kameiten_cd, mi.kameiten_cd) = k.kameiten_cd ")
        cmdTextSb.Append("           LEFT OUTER JOIN m_syouhin s WITH (READCOMMITTED) ")
        cmdTextSb.Append("             ON mi.syouhin_cd = s.syouhin_cd ")
        cmdTextSb.Append("           LEFT OUTER JOIN m_tyousahouhou th WITH (READCOMMITTED) ")
        cmdTextSb.Append("             ON mi.tys_houhou = th.tys_houhou_no ")
        cmdTextSb.Append("           LEFT OUTER JOIN m_tyousakaisya tysK1 WITH (READCOMMITTED) ")
        cmdTextSb.Append("             ON mi.tantou_tys_kaisya_cd = tysK1.tys_kaisya_cd ")
        cmdTextSb.Append("            AND mi.tantou_tys_kaisya_jigyousyo_cd = tysK1.jigyousyo_cd ")
        cmdTextSb.Append("           LEFT OUTER JOIN m_tyousakaisya tysK2 WITH (READCOMMITTED) ")
        cmdTextSb.Append("             ON mi.mousikomi_tys_kaisya_cd = tysK2.tys_kaisya_cd ")
        cmdTextSb.Append("            AND mi.mousikomi_tys_kaisya_jigyousyo_cd = tysK2.jigyousyo_cd ")

        Return cmdTextSb.ToString
    End Function

#End Region

#Region "WHERE句"
    ''' <summary>
    ''' 申込データ取得用の共通WHEREクエリを取得
    ''' </summary>
    ''' <param name="keyRec">申込データKEYレコードクラス</param>
    ''' <returns>申込データ取得用の共通WHEREクエリを取得</returns>
    ''' <remarks></remarks>
    Private Function GetCmnSqlWhere(ByVal keyRec As FcMousikomiKeyRecord) As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetCmnSqlWhere", keyRec)

        'SQL文の生成
        Dim cmdTextSb As New StringBuilder

        cmdTextSb.Append(" WHERE 1 = 1 ")

        '***********************************************************************
        ' 申込NO
        '***********************************************************************
        If keyRec.MousikomiNoFrom <> Long.MinValue Or _
            keyRec.MousikomiNoTo <> Long.MinValue Then

            cmdTextSb.Append(" AND ")

            If keyRec.MousikomiNoFrom <> Long.MinValue And _
                keyRec.MousikomiNoTo <> Long.MinValue Then
                '両方指定ありはBETWEEN
                cmdTextSb.Append("  mi.mousikomi_no BETWEEN " & DBprmMouskomiNoFrom)
                cmdTextSb.Append("  AND ")
                cmdTextSb.Append(DBprmMouskomiNoTo)
            Else
                If keyRec.MousikomiNoFrom <> Long.MinValue Then
                    'Fromのみ
                    cmdTextSb.Append("  mi.mousikomi_no >= " & DBprmMouskomiNoFrom)
                Else
                    'Toのみ
                    cmdTextSb.Append("  mi.mousikomi_no <= " & DBprmMouskomiNoTo)
                End If
            End If
        End If

        '***********************************************************************
        ' 登録日時
        '***********************************************************************
        If keyRec.AddDatetimeFrom <> DateTime.MinValue Or _
            keyRec.AddDatetimeTo <> DateTime.MinValue Then

            cmdTextSb.Append(" AND ")

            If keyRec.AddDatetimeFrom <> DateTime.MinValue And _
                keyRec.AddDatetimeTo <> DateTime.MinValue Then
                '両方指定ありはBETWEEN
                cmdTextSb.Append("  CONVERT(VARCHAR, mi.add_datetime, 111) BETWEEN " & DBprmAddDatetimeFrom)
                cmdTextSb.Append("  AND ")
                cmdTextSb.Append(DBprmAddDatetimeTo)
            Else
                If keyRec.AddDatetimeFrom <> DateTime.MinValue Then
                    'Fromのみ
                    cmdTextSb.Append("  CONVERT(VARCHAR, mi.add_datetime, 111) >= " & DBprmAddDatetimeFrom)
                Else
                    'Toのみ
                    cmdTextSb.Append("  CONVERT(VARCHAR, mi.add_datetime, 111) <= " & DBprmAddDatetimeTo)
                End If
            End If
        End If

        '***********************************************************************
        ' 加盟店コード
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.KameitenCd) Then
            cmdTextSb.Append(" AND mi.kameiten_cd = " & DBprmKameitenCd)
        End If

        '***********************************************************************
        ' 依頼日
        '***********************************************************************
        If keyRec.IraiDateFrom <> DateTime.MinValue Or _
            keyRec.IraiDateTo <> DateTime.MinValue Then

            cmdTextSb.Append(" AND ")

            If keyRec.IraiDateFrom <> DateTime.MinValue And _
                keyRec.IraiDateTo <> DateTime.MinValue Then
                '両方指定ありはBETWEEN
                cmdTextSb.Append("  CONVERT(VARCHAR, mi.irai_date, 111) BETWEEN " & DBprmIraiDateFrom)
                cmdTextSb.Append("  AND ")
                cmdTextSb.Append(DBprmIraiDateTo)
            Else
                If keyRec.IraiDateFrom <> DateTime.MinValue Then
                    'Fromのみ
                    cmdTextSb.Append("  CONVERT(VARCHAR, mi.irai_date, 111) >= " & DBprmIraiDateFrom)
                Else
                    'Toのみ
                    cmdTextSb.Append("  CONVERT(VARCHAR, mi.irai_date, 111) <= " & DBprmIraiDateTo)
                End If
            End If
        End If

        '***********************************************************************
        ' 物件名称
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.SesyuMei) Then
            cmdTextSb.Append("  AND REPLACE(mi.sesyu_mei, ' ', '') LIKE REPLACE(" & DBprmSesyuMei & ", ' ', '')")
        End If

        '***********************************************************************
        ' 同時依頼棟数
        '***********************************************************************
        If keyRec.DoujiIraiTousuuFrom <> Integer.MinValue Or _
            keyRec.DoujiIraiTousuuTo <> Integer.MinValue Then

            cmdTextSb.Append(" AND ")

            If keyRec.DoujiIraiTousuuFrom <> Integer.MinValue And _
                keyRec.DoujiIraiTousuuTo <> Integer.MinValue Then
                '両方指定ありはBETWEEN
                cmdTextSb.Append("  mi.douji_irai_tousuu BETWEEN " & DBprmDoujiIraiTousuuFrom)
                cmdTextSb.Append("  AND ")
                cmdTextSb.Append(DBprmDoujiIraiTousuuTo)
            Else
                If keyRec.DoujiIraiTousuuFrom <> Integer.MinValue Then
                    'Fromのみ
                    cmdTextSb.Append("  mi.douji_irai_tousuu >= " & DBprmDoujiIraiTousuuFrom)
                Else
                    'Toのみ
                    cmdTextSb.Append("  mi.douji_irai_tousuu <= " & DBprmDoujiIraiTousuuTo)
                End If
            End If
        End If

        '***********************************************************************
        ' 区分
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.Kbn) Then
            cmdTextSb.Append("  AND mi.kbn = " & DBprmKbn)
        End If

        '***********************************************************************
        ' 番号
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.HosyousyoNoFrom) Or Not String.IsNullOrEmpty(keyRec.HosyousyoNoTo) Then
            cmdTextSb.Append(" AND ")

            If Not (keyRec.HosyousyoNoFrom Is String.Empty) And Not (keyRec.HosyousyoNoTo Is String.Empty) Then
                ' 両方指定有りはBETWEEN
                cmdTextSb.Append("  mi.hosyousyo_no BETWEEN " & DBprmHosyousyoNoFrom)
                cmdTextSb.Append("  AND ")
                cmdTextSb.Append(DBprmHosyousyoNoTo)
            Else
                If Not keyRec.HosyousyoNoFrom Is String.Empty Then
                    ' 番号Fromのみ
                    cmdTextSb.Append(" mi.hosyousyo_no >= " & DBprmHosyousyoNoFrom)
                Else
                    ' 番号Toのみ
                    cmdTextSb.Append(" mi.hosyousyo_no <= " & DBprmHosyousyoNoTo)
                End If
            End If
        End If

        '***********************************************************************
        ' ステータス
        '***********************************************************************
        cmdTextSb.Append("  AND mi.status = " & DBprmStatus)

        '***********************************************************************
        ' 調査会社
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.TysKaisyaCd) Then
            cmdTextSb.Append(" AND (")
            cmdTextSb.Append(" (mi.tantou_tys_kaisya_cd + mi.tantou_tys_kaisya_jigyousyo_cd = " & DBprmTysKaisyaCd)
            cmdTextSb.Append(" ) OR ")
            cmdTextSb.Append(" (mi.mousikomi_tys_kaisya_cd + mi.mousikomi_tys_kaisya_jigyousyo_cd = " & DBprmTysKaisyaCd)
            cmdTextSb.Append(" )) ")
        End If

        '***********************************************************************
        ' 要注意有無
        '***********************************************************************
        'チェックありの場合
        If keyRec.YouTyuuiUmuSearchTaisyou = 0 Then

            cmdTextSb.Append("  AND (")
            cmdTextSb.Append("  (mi.you_tyuui_jouhou IS NOT NULL) ")
            cmdTextSb.Append("  AND ")
            cmdTextSb.Append("  (mi.you_tyuui_jouhou <> '') ")
            cmdTextSb.Append("  ) ")

        End If

        Return cmdTextSb.ToString
    End Function
#End Region

#Region "ORDER BY句"
    ''' <summary>
    ''' 申込データ取得用の共通ORDER BYクエリを取得
    ''' </summary>
    ''' <returns>申込データ取得用の共通ORDER BYクエリを取得</returns>
    ''' <remarks></remarks>
    Private Function GetCmnSqlOrderBy() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetCmnSqlOrderBy")

        'SQL文の生成
        Dim cmdTextSb As New StringBuilder

        cmdTextSb.Append(" ORDER BY ")
        cmdTextSb.Append("      mi.add_datetime ")

        Return cmdTextSb.ToString
    End Function
#End Region

#Region "SQLパラメータ"

#Region "パラメータ定義"
    '申込NO
    Private Const DBprmMouskomiNo As String = "@MOUSIKOMI_NO"
    '申込NO FROM,TO
    Private Const DBprmMouskomiNoFrom As String = "@MOUSIKOMI_NO_FROM"
    Private Const DBprmMouskomiNoTo As String = "@MOUSIKOMI_NO_TO"
    '登録日時FROM,TO
    Private Const DBprmAddDatetimeFrom As String = "@ADD_DATETIME_FROM"
    Private Const DBprmAddDatetimeTo As String = "@ADD_DATETIME_TO"
    '加盟店コード
    Private Const DBprmKameitenCd As String = "@KAMEITEN_CD"
    '依頼日FROM,TO
    Private Const DBprmIraiDateFrom As String = "@IRAI_DATE_FROM"
    Private Const DBprmIraiDateTo As String = "@IRAI_DATE_TO"
    '物件名称
    Private Const DBprmSesyuMei As String = "@SESYU_MEI"
    '同時依頼棟数FROM,TO
    Private Const DBprmDoujiIraiTousuuFrom As String = "@DOUJI_IRAI_TOUSUU_FROM"
    Private Const DBprmDoujiIraiTousuuTo As String = "@DOUJI_IRAI_TOUSUU_TO"
    '区分
    Private Const DBprmKbn As String = "@KBN"
    '番号FROM,TO
    Private Const DBprmHosyousyoNoFrom As String = "@HOSYOUSYO_NO_FROM"
    Private Const DBprmHosyousyoNoTo As String = "@HOSYOUSYO_NO_TO"
    'ステータス
    Private Const DBprmStatus As String = "@STATUS"
    '調査会社
    Private Const DBprmTysKaisyaCd As String = "@TYS_KAISYA_CD"

#Region "更新用"
    '更新者
    Private Const DBprmKousinsya As String = "@KOUSINSYA"
    '更新ログインユーザID
    Private Const DBprmUpdUserID As String = "@UPDLOGINUSERID"
    '更新日時
    Private Const DBprmUpdDatetime As String = "@UPDDATETIME"
    '番号(保証書NO)
    Private Const DBprmBangou As String = "@HOSYOUSYO_NO"
#End Region

#End Region

    ''' <summary>
    ''' 申込データ取得用の共通SQLパラメータを取得
    ''' </summary>
    ''' <param name="keyRec">申込データKEYレコードクラス</param>
    ''' <returns>申込データ取得用の共通SQLクエリ</returns>
    ''' <remarks></remarks>
    Private Function GetMousikomiSqlCmnParams(ByVal keyRec As FcMousikomiKeyRecord) As SqlParameter()
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetMousikomiSqlCmnParams", keyRec)

        '申込NO From,To
        Dim objMousikomiNoFrom As Object = IIf(keyRec.MousikomiNoFrom = Long.MinValue, DBNull.Value, keyRec.MousikomiNoFrom)
        Dim objMousikomiNoTo As Object = IIf(keyRec.MousikomiNoTo = Long.MinValue, DBNull.Value, keyRec.MousikomiNoTo)
        '登録日時FROM,TO
        Dim objAddDatetimeFrom As Object = IIf(keyRec.AddDatetimeFrom = DateTime.MinValue, DBNull.Value, keyRec.AddDatetimeFrom)
        Dim objAddDatetimeTo As Object = IIf(keyRec.AddDatetimeTo = DateTime.MinValue, DBNull.Value, keyRec.AddDatetimeTo)
        '加盟店コード
        Dim objKameitenCd As Object = IIf(keyRec.KameitenCd = String.Empty, DBNull.Value, keyRec.KameitenCd)
        '依頼日FROM,TO
        Dim objIraiDateFrom As Object = IIf(keyRec.IraiDateFrom = DateTime.MinValue, DBNull.Value, keyRec.IraiDateFrom)
        Dim objIraiDateTo As Object = IIf(keyRec.IraiDateTo = DateTime.MinValue, DBNull.Value, keyRec.IraiDateTo)
        '物件名称
        Dim objSesyuMei As Object = IIf(keyRec.SesyuMei = String.Empty, DBNull.Value, keyRec.SesyuMei)
        '同時依頼棟数FROM,TO
        Dim objDoujiIraiTousuuFrom As Object = IIf(keyRec.DoujiIraiTousuuFrom = Integer.MinValue, DBNull.Value, keyRec.DoujiIraiTousuuFrom)
        Dim objDoujiIraiTousuuTo As Object = IIf(keyRec.DoujiIraiTousuuTo = Integer.MinValue, DBNull.Value, keyRec.DoujiIraiTousuuTo)
        '区分
        Dim objKbn As Object = IIf(keyRec.Kbn = String.Empty, DBNull.Value, keyRec.Kbn)
        '番号From,To
        Dim objHosyousyoNoFrom As Object = IIf(keyRec.HosyousyoNoFrom = String.Empty, DBNull.Value, keyRec.HosyousyoNoFrom)
        Dim objHosyousyoNoTo As Object = IIf(keyRec.HosyousyoNoTo = String.Empty, DBNull.Value, keyRec.HosyousyoNoTo)
        'ステータス
        Dim objStatus As Object = IIf(keyRec.Status = String.Empty, DBNull.Value, keyRec.Status)
        '調査会社
        Dim objTysKaisyaCd As Object = IIf(keyRec.TysKaisyaCd = String.Empty, DBNull.Value, keyRec.TysKaisyaCd)

        'パラメータへ設定
        Dim cmdParams() As SqlParameter = { _
                 SQLHelper.MakeParam(DBprmMouskomiNoFrom, SqlDbType.BigInt, 8, objMousikomiNoFrom), _
                 SQLHelper.MakeParam(DBprmMouskomiNoTo, SqlDbType.BigInt, 8, objMousikomiNoTo), _
                 SQLHelper.MakeParam(DBprmAddDatetimeFrom, SqlDbType.DateTime, 16, objAddDatetimeFrom), _
                 SQLHelper.MakeParam(DBprmAddDatetimeTo, SqlDbType.DateTime, 16, objAddDatetimeTo), _
                 SQLHelper.MakeParam(DBprmKameitenCd, SqlDbType.VarChar, 5, objKameitenCd), _
                 SQLHelper.MakeParam(DBprmIraiDateFrom, SqlDbType.DateTime, 16, objIraiDateFrom), _
                 SQLHelper.MakeParam(DBprmIraiDateTo, SqlDbType.DateTime, 16, objIraiDateTo), _
                 SQLHelper.MakeParam(DBprmSesyuMei, SqlDbType.VarChar, 50, objSesyuMei & Chr(37)), _
                 SQLHelper.MakeParam(DBprmDoujiIraiTousuuFrom, SqlDbType.Int, 4, objDoujiIraiTousuuFrom), _
                 SQLHelper.MakeParam(DBprmDoujiIraiTousuuTo, SqlDbType.Int, 4, objDoujiIraiTousuuTo), _
                 SQLHelper.MakeParam(DBprmKbn, SqlDbType.Char, 1, objKbn), _
                 SQLHelper.MakeParam(DBprmHosyousyoNoFrom, SqlDbType.VarChar, 10, objHosyousyoNoFrom), _
                 SQLHelper.MakeParam(DBprmHosyousyoNoTo, SqlDbType.VarChar, 10, objHosyousyoNoTo), _
                 SQLHelper.MakeParam(DBprmStatus, SqlDbType.Char, 3, objStatus), _
                 SQLHelper.MakeParam(DBprmTysKaisyaCd, SqlDbType.VarChar, 7, objTysKaisyaCd) _
        }

        Return cmdParams
    End Function
#End Region

#End Region
End Class

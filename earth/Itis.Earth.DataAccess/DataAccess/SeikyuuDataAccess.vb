Imports System
Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' 請求データの取得クラス
''' </summary>
''' <remarks></remarks>
Public Class SeikyuuDataAccess

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

#Region "メンバ変数"
    Dim connStr As String = ConnectionManager.Instance.ConnectionString
    Dim SLogic As New StringLogic
    Dim cmnDtAcc As New CmnDataAccess
#End Region

#Region "請求書データの取得"

    ''' <summary>
    ''' (過去)請求書一覧画面/請求データを取得します
    ''' </summary>
    ''' <param name="keyRec">検索KEYレコードクラス</param>
    ''' <param name="emType">請求データの検索タイプ</param>
    ''' <returns>請求データテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetSearchSeikyuusyoTbl(ByVal keyRec As SeikyuuDataKeyRecord, ByVal emType As EarthEnum.emSeikyuuSearchType) As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSearchSeikyuusyoTbl", keyRec, emType)

        'SQL文の生成
        Dim cmdTextSb As New StringBuilder
        'パラメータ
        Dim cmdParams() As SqlParameter = Me.GetSeikyuuSqlCmnParams(keyRec)

        'SELECT句
        Dim strCmnSelect As String = Me.GetSearchSeikyuusyoSqlSelect()
        'TABLE句
        Dim strCmnTable As String = Me.GetCmnSqlTable(keyRec, emType)
        'WHERE句
        Dim strCmnWhere As String = Me.GetCmnSqlWhere(keyRec, emType)
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
        ' ORDER BY句（請求書発行日→請求書NO）
        '***********************************************************************
        cmdTextSb.Append(strCmnOrderBy)

        'データ取得＆返却
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)
    End Function

    ''' <summary>
    ''' (過去)請求書一覧画面/請求データ[CSV出力用]を取得します
    ''' </summary>
    ''' <param name="keyRec">請求データKeyレコード</param>
    ''' <param name="emType">請求データの検索タイプ</param>
    ''' <returns>データテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetSearchSeikyuuDataCsv(ByVal keyRec As SeikyuuDataKeyRecord, ByVal emType As EarthEnum.emSeikyuuSearchType) As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSearchSeikyuuDataCsv", keyRec, emType)

        'SQL文の生成
        Dim cmdTextSb As New StringBuilder

        Dim cmdParams() As SqlParameter = Me.GetSeikyuuSqlCmnParams(keyRec)

        'SELECT句
        Dim strCmnSelect As String = Me.GetCmnSqlSelectCsv()
        'TABLE句
        Dim strCmnTable As String = Me.GetCmnSqlTableCsv(keyRec, emType)
        'WHERE句
        Dim strCmnWhere As String = Me.GetCmnSqlWhere(keyRec, emType)
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
        cmdTextSb.Append(" AND SK.seikyuusyo_no IN ('" & keyRec.ArrSeikyuuSakiNo.Replace(EarthConst.SEP_STRING, "','") & "') ") '指定の請求書NOのみを抽出

        '***********************************************************************
        ' ORDER BY句（請求書発行日→請求書NO）
        '***********************************************************************
        cmdTextSb.Append(strCmnOrderBy)

        'データ取得＆返却
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)

    End Function

    ''' <summary>
    ''' 請求書印字内容編集画面/請求データをPKで取得します
    ''' </summary>
    ''' <param name="strSeikyuusyoNo">主キー項目値</param>
    ''' <returns>データテーブル</returns>
    ''' <remarks>検索キーレコードクラスをKEYにして取得</remarks>
    Public Function GetSeikyuusyoSyuuseiRec(ByVal strSeikyuusyoNo As String) As DataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSeikyuusyoSyuuseiRec" _
                                                    , strSeikyuusyoNo _
                                                    )
        ' パラメータ
        Dim cmdParams() As SqlClient.SqlParameter

        'SELECT句
        Dim strCmnSelect As String = Me.GetCmnSqlSelectSeikyuusyoSyuusei()

        'SQLクエリ生成
        Dim cmdTextSb As New StringBuilder()

        '****************
        '* SELECT項目
        '****************
        cmdTextSb.Append(strCmnSelect)

        '****************
        '* TABLE項目
        '****************
        cmdTextSb.Append(" FROM ")
        cmdTextSb.Append("   t_seikyuu_kagami SK WITH (READCOMMITTED) ")
        '***********************************************************************
        ' VIEW請求先：請求先名(外部結合)
        '***********************************************************************
        cmdTextSb.Append("      LEFT OUTER JOIN v_seikyuu_saki_info VIW WITH (READCOMMITTED) ")
        cmdTextSb.Append("          ON SK.seikyuu_saki_cd = VIW.seikyuu_saki_cd ")
        cmdTextSb.Append("          AND SK.seikyuu_saki_brc = VIW.seikyuu_saki_brc ")
        cmdTextSb.Append("          AND SK.seikyuu_saki_kbn = VIW.seikyuu_saki_kbn ")

        '***********************************************************************
        ' 請求先マスタ：請求締め日(外部結合)
        '***********************************************************************
        cmdTextSb.Append("      LEFT OUTER JOIN m_seikyuu_saki MSM WITH (READCOMMITTED) ")
        cmdTextSb.Append("          ON SK.seikyuu_saki_cd = MSM.seikyuu_saki_cd ")
        cmdTextSb.Append("          AND SK.seikyuu_saki_brc = MSM.seikyuu_saki_brc ")
        cmdTextSb.Append("          AND SK.seikyuu_saki_kbn = MSM.seikyuu_saki_kbn ")

        '***********************************************************************
        ' 請求鑑毎の明細件数(外部結合)
        '***********************************************************************
        cmdTextSb.Append("      LEFT OUTER JOIN ")
        cmdTextSb.Append("          ( SELECT  ")
        cmdTextSb.Append("              SM.seikyuusyo_no ")
        cmdTextSb.Append("              ,COUNT(SM.seikyuusyo_no) AS meisai_kensuu ")
        cmdTextSb.Append("          FROM ")
        cmdTextSb.Append("              t_seikyuu_meisai SM WITH (READCOMMITTED) ")
        cmdTextSb.Append("          WHERE ")
        cmdTextSb.Append("              SM.inji_taisyo_flg = 1 ")
        cmdTextSb.Append("          GROUP BY SM.seikyuusyo_no ")
        cmdTextSb.Append("          ) AS SUB ")
        cmdTextSb.Append("          ON SK.seikyuusyo_no = SUB.seikyuusyo_no ")

        '****************
        '* WHERE項目
        '****************
        cmdTextSb.Append("  WHERE SK.seikyuusyo_no = " & DBprmSeikyuusyoNo)

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam(DBprmSeikyuusyoNo, SqlDbType.VarChar, 15, strSeikyuusyoNo) _
        }

        'データ取得＆返却
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)
    End Function

    ''' <summary>
    ''' 請求書印字内容編集画面/請求明細の重複データを取得します
    ''' </summary>
    ''' <param name="strSeikyuusyoNo">請求書No</param>
    ''' <returns>データテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetDenpyouExistsCnt(ByVal strSeikyuusyoNo As String) As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetDenpyouExistsCnt" _
                                                    , strSeikyuusyoNo)
        ' パラメータ
        Dim cmdParams() As SqlClient.SqlParameter

        'SQLクエリ生成
        Dim cmdTextSb As New StringBuilder

        cmdTextSb.Append(" SELECT ")
        cmdTextSb.Append("      * ")
        cmdTextSb.Append(" FROM ")
        cmdTextSb.Append("      t_seikyuu_meisai SM ")
        cmdTextSb.Append("           INNER JOIN t_seikyuu_kagami SK ")
        cmdTextSb.Append("             ON SM.seikyuusyo_no = SK.seikyuusyo_no ")
        cmdTextSb.Append(" WHERE ")
        cmdTextSb.Append("      EXISTS ")
        cmdTextSb.Append("     (SELECT ")
        cmdTextSb.Append("           * ")
        cmdTextSb.Append("      FROM ")
        cmdTextSb.Append("           t_seikyuu_meisai TSM ")
        cmdTextSb.Append("      WHERE ")
        cmdTextSb.Append("           SM.denpyou_unique_no = TSM.denpyou_unique_no ")
        cmdTextSb.Append("       AND TSM.seikyuusyo_no = " & DBprmSeikyuusyoNo)
        cmdTextSb.Append("     ) ")
        cmdTextSb.Append("  AND SK.torikesi = '0' ")

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam(DBprmSeikyuusyoNo, SqlDbType.VarChar, 15, strSeikyuusyoNo) _
        }

        'データ取得＆返却
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)

    End Function

    ''' <summary>
    ''' 請求データ[未発行件数用]を取得します
    ''' </summary>
    ''' <returns>データテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetMihakkouCnt() As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetMihakkouCnt")

        'SQL文の生成
        Dim cmdTextSb As New StringBuilder
        Dim cmdParams() As SqlParameter = {}

        cmdTextSb.Append(" SELECT ")
        cmdTextSb.Append("      COUNT(SK.seikyuusyo_no) AS mihakkou_cnt ")
        cmdTextSb.Append(" FROM ")
        cmdTextSb.Append("      t_seikyuu_kagami SK ")
        cmdTextSb.Append("           INNER JOIN ")
        cmdTextSb.Append("               (SELECT ")
        cmdTextSb.Append("                     TSM.seikyuusyo_no ")
        cmdTextSb.Append("                FROM ")
        cmdTextSb.Append("                     t_seikyuu_meisai TSM ")
        cmdTextSb.Append("                WHERE ")
        cmdTextSb.Append("                     TSM.inji_taisyo_flg = '1' ")
        cmdTextSb.Append("                GROUP BY ")
        cmdTextSb.Append("                     TSM.seikyuusyo_no ")
        cmdTextSb.Append("               ) ")
        cmdTextSb.Append("                AS SUB ")
        cmdTextSb.Append("             ON SK.seikyuusyo_no = SUB.seikyuusyo_no ")
        cmdTextSb.Append(" WHERE ")
        cmdTextSb.Append("      SK.seikyuusyo_insatu_date IS NULL ")
        cmdTextSb.Append("  AND SK.torikesi = 0 ")

        'データ取得＆返却
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)

    End Function

    ''' <summary>
    ''' 請求明細の伝票ユニークNOに紐付く、最新の請求鑑レコードを取得します
    ''' </summary>
    ''' <param name="strDenUnqNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSeikyuusyoMaxRec(ByVal strDenUnqNo As String) As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSeikyuusyoMaxRec", strDenUnqNo)

        ' パラメータ
        Dim cmdParams() As SqlClient.SqlParameter
        'SQLクエリ生成
        Dim cmdTextSb As New StringBuilder()

        cmdTextSb.AppendLine("SELECT")
        cmdTextSb.AppendLine("     MAX(TSK.seikyuusyo_no) seikyuusyo_no")
        cmdTextSb.AppendLine("FROM")
        cmdTextSb.AppendLine("     t_seikyuu_kagami TSK")
        cmdTextSb.AppendLine("          INNER JOIN t_seikyuu_meisai TSM")
        cmdTextSb.AppendLine("            ON TSK.seikyuusyo_no = TSM.seikyuusyo_no")
        cmdTextSb.AppendLine("WHERE")
        cmdTextSb.AppendLine("     TSK.torikesi = '0'")
        cmdTextSb.AppendLine(" AND TSM.denpyou_unique_no = " & DBprmMeisaiDenUnqNo)

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam(DBprmMeisaiDenUnqNo, SqlDbType.Int, 4, strDenUnqNo) _
        }

        'データ取得＆返却
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)
    End Function

#End Region

#Region "請求書データの更新"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="strSeikyuusyoNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdKagamiTorikesi(ByVal strSeikyuusyoNo As String, ByVal strLoginUserId As String) As Boolean
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".UpdKagamiTorikesi", strSeikyuusyoNo, strLoginUserId)

        ' SQLコマンドパラメータ設定用
        Dim cmdParams() As SqlClient.SqlParameter
        ' SQLクエリ生成
        Dim cmdTextSb As New StringBuilder()
        ' 更新結果件数
        Dim intResult As Integer = 0

        cmdTextSb.AppendLine("UPDATE")
        cmdTextSb.AppendLine("     t_seikyuu_kagami")
        cmdTextSb.AppendLine("SET")
        cmdTextSb.AppendLine("     torikesi = 1")
        cmdTextSb.AppendLine("   , upd_login_user_id = " & DBparamAddLoginUserId)
        cmdTextSb.AppendLine("   , upd_datetime = " & DBparamAddDateTime)
        cmdTextSb.AppendLine("WHERE")
        cmdTextSb.AppendLine("     seikyuusyo_no = " & DBprmSeikyuusyoNo)

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam(DBprmSeikyuusyoNo, SqlDbType.VarChar, 15, strSeikyuusyoNo), _
            SQLHelper.MakeParam(DBparamAddLoginUserId, SqlDbType.VarChar, 30, strLoginUserId), _
            SQLHelper.MakeParam(DBparamAddDateTime, SqlDbType.DateTime, 16, DateTime.Now)}

        intResult = ExecuteNonQuery(connStr, CommandType.Text, cmdTextSb.ToString, cmdParams)

        If intResult < 1 Then
            Return False
        End If

        Return True
    End Function

#End Region

#Region "共通クエリ"

#Region "SELECT句"
    ''' <summary>
    ''' 請求データ取得用のSELECTクエリを取得
    ''' </summary>
    ''' <returns>請求データ取得用の共通SELECTクエリを取得</returns>
    ''' <remarks></remarks>
    Private Function GetSearchSeikyuusyoSqlSelect() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSearchSeikyuusyoSqlSelect")

        'SQL文の生成
        Dim cmdTextSb As New StringBuilder
        'SELECT句
        Dim strCmnSelect As String = Me.GetCmnSqlSelect()

        '****************
        '* SELECT項目
        '****************
        cmdTextSb.Append(strCmnSelect)
        cmdTextSb.Append("  ,ISNULL(WK.seikyuu_date_sai_flg, 0) AS seikyuu_date_sai_flg ")
        cmdTextSb.Append("  ,ISNULL(SUB.meisai_kensuu, 0) AS meisai_kensuu ")

        Return cmdTextSb.ToString
    End Function

    ''' <summary>
    ''' 請求データ取得用の共通SELECTクエリを取得[CSV]
    ''' </summary>
    ''' <returns>請求データ取得用の共通SELECTクエリを取得[CSV]</returns>
    ''' <remarks></remarks>
    Private Function GetCmnSqlSelectCsv() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetCmnSqlSelectCsv")

        'SQL文の生成
        Dim cmdTextSb As New StringBuilder

        '****************
        '* SELECT項目
        '****************
        cmdTextSb.Append(" SELECT ")
        cmdTextSb.Append("    SK.seikyuusyo_no '請求書NO' ")
        cmdTextSb.Append("    , SK.torikesi '取消' ")
        cmdTextSb.Append("    , SK.seikyuu_saki_cd '請求先コード' ")
        cmdTextSb.Append("    , SK.seikyuu_saki_brc '請求先枝番' ")
        cmdTextSb.Append("    , SK.seikyuu_saki_kbn '請求先区分' ")
        cmdTextSb.Append("    , SK.seikyuu_saki_mei '請求先名' ")
        cmdTextSb.Append("    , SK.seikyuu_saki_mei2 '請求先名２' ")
        cmdTextSb.Append("    , SK.yuubin_no '郵便番号' ")
        cmdTextSb.Append("    , SK.jyuusyo1 '住所1' ")
        cmdTextSb.Append("    , SK.jyuusyo2 '住所2' ")
        cmdTextSb.Append("    , SK.tel_no '電話番号' ")
        cmdTextSb.Append("    , SK.zenkai_goseikyuu_gaku '前回御請求額' ")
        cmdTextSb.Append("    , SK.gonyuukin_gaku '御入金額' ")
        cmdTextSb.Append("    , SK.sousai_gaku '相殺額' ")
        cmdTextSb.Append("    , SK.tyousei_gaku '調整額' ")
        cmdTextSb.Append("    , SK.kurikosi_gaku '前回繰越残高' ")
        cmdTextSb.Append("    , SK.konkai_goseikyuu_gaku '今回御請求額' ")
        cmdTextSb.Append("    , SK.konkai_kurikosi_gaku '繰越残高' ")
        cmdTextSb.Append("    , CONVERT(VARCHAR, SK.konkai_kaisyuu_yotei_date, 111) '入金予定日' ")
        cmdTextSb.Append("    , SK.seikyuusyo_insatu_date '請求書印刷日' ")
        cmdTextSb.Append("    , CONVERT(VARCHAR, SK.seikyuusyo_hak_date, 111) '請求書発行日' ")
        cmdTextSb.Append("    , SK.tantousya_mei '担当者名' ")
        cmdTextSb.Append("    , SK.seikyuusyo_inji_bukken_mei_flg '請求書印字物件名フラグ' ")
        cmdTextSb.Append("    , SK.nyuukin_kouza_no '入金口座番号' ")
        cmdTextSb.Append("    , SK.seikyuu_sime_date '請求締め日' ")
        cmdTextSb.Append("    , SK.senpou_seikyuu_sime_date '先方請求締め日' ")
        cmdTextSb.Append("    , SK.sousai_flg '相殺フラグ' ")
        cmdTextSb.Append("    , SK.kaisyuu_yotei_gessuu '回収予定月数' ")
        cmdTextSb.Append("    , SK.kaisyuu_yotei_date '回収予定日' ")
        cmdTextSb.Append("    , SK.seikyuusyo_hittyk_date '請求書必着日' ")
        cmdTextSb.Append("    , SK.kaisyuu_syubetu1 '種別1' ")
        cmdTextSb.Append("    , SK.kaisyuu_wariai1 '割合1' ")
        cmdTextSb.Append("    , SK.kaisyuu_tegata_site_gessuu '手形サイト月数' ")
        cmdTextSb.Append("    , SK.kaisyuu_tegata_site_date '手形サイト日' ")
        cmdTextSb.Append("    , SK.kaisyuu_seikyuusyo_yousi '請求書用紙' ")
        cmdTextSb.Append("    , SK.kaisyuu_seikyuusyo_yousi_hannyou_cd '請求書用紙汎用コード' ")
        cmdTextSb.Append("    , SK.kaisyuu_syubetu2 '種別2' ")
        cmdTextSb.Append("    , SK.kaisyuu_wariai2 '割合2' ")
        cmdTextSb.Append("    , SK.kaisyuu_syubetu3 '種別3' ")
        cmdTextSb.Append("    , SK.kaisyuu_wariai3 '割合3' ")
        cmdTextSb.Append("    , URI.denpyou_unique_no '伝票ユニークNO' ")
        cmdTextSb.Append("    , URI.denpyou_no '伝票番号' ")
        cmdTextSb.Append("    , URI.denpyou_syubetu '伝票種別' ")
        cmdTextSb.Append("    , URI.torikesi_moto_denpyou_unique_no '取消元伝票ユニークNO' ")
        cmdTextSb.Append("    , URI.kbn '区分' ")
        cmdTextSb.Append("    , URI.bangou '番号' ")
        cmdTextSb.Append("    , URI.himoduke_cd '紐付けコード' ")
        cmdTextSb.Append("    , URI.himoduke_table_type '紐付け元テーブル種別' ")
        cmdTextSb.Append("    , CONVERT(VARCHAR, URI.uri_date, 111) '売上年月日' ")
        cmdTextSb.Append("    , CONVERT(VARCHAR, URI.denpyou_uri_date, 111) '伝票売上年月日' ")
        cmdTextSb.Append("    , CONVERT(VARCHAR, URI.seikyuu_date, 111) '請求年月日' ")
        cmdTextSb.Append("    , URI.seikyuu_saki_mei '売上_請求先名' ")
        cmdTextSb.Append("    , URI.syouhin_cd '商品コード' ")
        cmdTextSb.Append("    , URI.hinmei '品名' ")
        cmdTextSb.Append("    , URI.suu '数量' ")
        cmdTextSb.Append("    , URI.tani '単位' ")
        cmdTextSb.Append("    , URI.tanka '単価' ")
        cmdTextSb.Append("    , URI.syanai_genka '社内原価' ")
        cmdTextSb.Append("    , URI.uri_gaku '売上金額' ")
        cmdTextSb.Append("    , URI.sotozei_gaku '外税額' ")
        cmdTextSb.Append("    , URI.zei_kbn '税区分' ")
        cmdTextSb.Append("    , URI.add_login_user_id '登録ログインユーザーID' ")
        cmdTextSb.Append("    , URI.add_login_user_name '登録ログインユーザー名' ")
        cmdTextSb.Append("    , URI.add_datetime '登録日時' ")
        cmdTextSb.Append("    , URI.upd_login_user_id '更新ログインユーザーID' ")
        cmdTextSb.Append("    , URI.upd_datetime '更新日時' ")

        Return cmdTextSb.ToString
    End Function

    ''' <summary>
    ''' 請求データ取得用のSELECTクエリを取得
    ''' </summary>
    ''' <returns>請求データ取得用の共通SELECTクエリを取得</returns>
    ''' <remarks></remarks>
    Private Function GetCmnSqlSelectSeikyuusyoSyuusei() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetCmnSqlSelectSeikyuusyoSyuusei")

        'SQL文の生成
        Dim cmdTextSb As New StringBuilder

        'SELECT句
        Dim strCmnSelect As String = Me.GetCmnSqlSelect()

        '****************
        '* SELECT項目
        '****************
        cmdTextSb.Append(strCmnSelect)
        cmdTextSb.Append("  ,VIW.seikyuu_saki_mei AS view_seikyuu_saki_mei ")
        cmdTextSb.Append("  ,SUB.meisai_kensuu AS meisai_kensuu ")

        Return cmdTextSb.ToString
    End Function

    ''' <summary>
    ''' 請求データ取得用の共通SELECTクエリを取得
    ''' </summary>
    ''' <returns>請求データ取得用の共通SELECTクエリを取得</returns>
    ''' <remarks></remarks>
    Private Function GetCmnSqlSelect() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetCmnSqlSelect")

        'SQL文の生成
        Dim cmdTextSb As New StringBuilder

        '****************
        '* SELECT項目
        '****************
        cmdTextSb.Append("SELECT")
        cmdTextSb.Append("  SK.seikyuusyo_no")
        cmdTextSb.Append("  ,SK.torikesi")
        cmdTextSb.Append("  ,SK.seikyuu_saki_cd")
        cmdTextSb.Append("  ,SK.seikyuu_saki_brc")
        cmdTextSb.Append("  ,SK.seikyuu_saki_kbn")
        cmdTextSb.Append("  ,SK.seikyuu_saki_mei")
        cmdTextSb.Append("  ,SK.seikyuu_saki_mei2")
        cmdTextSb.Append("  ,SK.yuubin_no")
        cmdTextSb.Append("  ,SK.jyuusyo1")
        cmdTextSb.Append("  ,SK.jyuusyo2")
        cmdTextSb.Append("  ,SK.tel_no")
        cmdTextSb.Append("  ,SK.zenkai_goseikyuu_gaku")
        cmdTextSb.Append("  ,SK.gonyuukin_gaku")
        cmdTextSb.Append("  ,SK.sousai_gaku")
        cmdTextSb.Append("  ,SK.tyousei_gaku")
        cmdTextSb.Append("  ,SK.kurikosi_gaku")
        cmdTextSb.Append("  ,SK.konkai_goseikyuu_gaku")
        cmdTextSb.Append("  ,SK.konkai_kurikosi_gaku")
        cmdTextSb.Append("  ,SK.konkai_kaisyuu_yotei_date")
        cmdTextSb.Append("  ,SK.seikyuusyo_insatu_date")
        cmdTextSb.Append("  ,SK.seikyuusyo_hak_date")
        cmdTextSb.Append("  ,SK.tantousya_mei")
        cmdTextSb.Append("  ,SK.seikyuusyo_inji_bukken_mei_flg")
        cmdTextSb.Append("  ,SK.nyuukin_kouza_no")
        cmdTextSb.Append("  ,SK.seikyuu_sime_date")
        cmdTextSb.Append("  ,SK.senpou_seikyuu_sime_date")
        cmdTextSb.Append("  ,SK.sousai_flg")
        cmdTextSb.Append("  ,SK.kaisyuu_yotei_gessuu")
        cmdTextSb.Append("  ,SK.kaisyuu_yotei_date")
        cmdTextSb.Append("  ,SK.seikyuusyo_hittyk_date")
        cmdTextSb.Append("  ,SK.kaisyuu_syubetu1")
        cmdTextSb.Append("  ,SK.kaisyuu_wariai1")
        cmdTextSb.Append("  ,SK.kaisyuu_tegata_site_gessuu")
        cmdTextSb.Append("  ,SK.kaisyuu_tegata_site_date")
        cmdTextSb.Append("  ,SK.kaisyuu_seikyuusyo_yousi")
        cmdTextSb.Append("  ,ISNULL(SK.kaisyuu_seikyuusyo_yousi_hannyou_cd,'') AS kaisyuu_seikyuusyo_yousi_hannyou_cd")
        cmdTextSb.Append("  ,CASE ")
        cmdTextSb.Append("      WHEN SK.kaisyuu_seikyuusyo_yousi_hannyou_cd IS NULL THEN 0 ")
        cmdTextSb.Append("      WHEN SUBSTRING(SK.kaisyuu_seikyuusyo_yousi_hannyou_cd, 1, 1) = '9' THEN 1 ")
        cmdTextSb.Append("      ELSE 0 ")
        cmdTextSb.Append("      END AS print_taigyougai_flg")
        cmdTextSb.Append("  ,SK.kaisyuu_syubetu2")
        cmdTextSb.Append("  ,SK.kaisyuu_wariai2")
        cmdTextSb.Append("  ,SK.kaisyuu_syubetu3")
        cmdTextSb.Append("  ,SK.kaisyuu_wariai3")
        cmdTextSb.Append("  ,SK.add_login_user_id")
        cmdTextSb.Append("  ,SK.add_datetime")
        cmdTextSb.Append("  ,ISNULL(SK.upd_login_user_id, SK.add_login_user_id) AS upd_login_user_id")
        cmdTextSb.Append("  ,ISNULL(SK.upd_datetime, SK.add_datetime) AS upd_datetime")
        cmdTextSb.Append("  ,MSM.seikyuu_sime_date AS mst_seikyuu_sime_date")

        Return cmdTextSb.ToString
    End Function
#End Region

#Region "TABLE句"
    ''' <summary>
    ''' 請求データ取得用の共通TABLEクエリを取得
    ''' </summary>
    ''' <param name="keyRec">請求データKEYレコードクラス</param>
    ''' <param name="emType">請求データの検索タイプ</param>
    ''' <returns>請求データ取得用の共通TABLEクエリを取得</returns>
    ''' <remarks></remarks>
    Private Function GetCmnSqlTable(ByVal keyRec As SeikyuuDataKeyRecord, ByVal emType As EarthEnum.emSeikyuuSearchType) As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetCmnSqlTable", keyRec, emType)

        'SQL文の生成
        Dim cmdTextSb As New StringBuilder

        '****************
        '* TABLE項目(メイン)
        '****************
        cmdTextSb.Append(" FROM ")
        cmdTextSb.Append("   t_seikyuu_kagami SK WITH (READCOMMITTED) ")

        '***********************************************************************
        ' 売上伝票の請求年月日と請求書発行日の比較用：請求書発行日差異フラグ(外部結合)
        '***********************************************************************
        cmdTextSb.Append("           LEFT OUTER JOIN ")
        cmdTextSb.Append("               (SELECT ")
        cmdTextSb.Append("                     WK.seikyuusyo_no ")
        cmdTextSb.Append("                   , MAX( ")
        cmdTextSb.Append("                          CASE ")
        cmdTextSb.Append("                               WHEN ISNULL(WK.seikyuusyo_hak_date, '') <> ISNULL(TU.seikyuu_date, '') ")
        cmdTextSb.Append("                               THEN 1 ")
        cmdTextSb.Append("                               ELSE 0 ")
        cmdTextSb.Append("                          END) seikyuu_date_sai_flg ")
        cmdTextSb.Append("                FROM ")
        cmdTextSb.Append("                     t_uriage_data TU WITH (READCOMMITTED) ")
        cmdTextSb.Append("                          INNER JOIN ")
        cmdTextSb.Append("                              (SELECT ")
        cmdTextSb.Append("                                    SM.seikyuusyo_no ")
        cmdTextSb.Append("                                  , SK.seikyuusyo_hak_date ")
        cmdTextSb.Append("                                  , SM.denpyou_unique_no ")
        cmdTextSb.Append("                               FROM ")
        cmdTextSb.Append("                                    t_seikyuu_kagami SK WITH (READCOMMITTED) ")
        cmdTextSb.Append("                                         INNER JOIN t_seikyuu_meisai SM ")
        cmdTextSb.Append("                                           ON SK.seikyuusyo_no = SM.seikyuusyo_no ")
        cmdTextSb.Append("                                          AND SM.inji_taisyo_flg = 1 ")
        cmdTextSb.Append("                              ) WK ")
        cmdTextSb.Append("                            ON TU.denpyou_unique_no = WK.denpyou_unique_no ")
        cmdTextSb.Append("                GROUP BY ")
        cmdTextSb.Append("                     WK.seikyuusyo_no ")
        cmdTextSb.Append("               ) AS WK ")
        cmdTextSb.Append("             ON SK.seikyuusyo_no = WK.seikyuusyo_no ")

        '***********************************************************************
        ' 請求先情報取得ビュー：請求先名カナ(外部結合)
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.SeikyuuSakiMeiKana) Then
            cmdTextSb.Append(" LEFT OUTER JOIN v_seikyuu_saki_info VIW WITH (READCOMMITTED) ")
            cmdTextSb.Append("    ON SK.seikyuu_saki_cd = VIW.seikyuu_saki_cd ")
            cmdTextSb.Append("   AND SK.seikyuu_saki_brc = VIW.seikyuu_saki_brc ")
            cmdTextSb.Append("   AND SK.seikyuu_saki_kbn = VIW.seikyuu_saki_kbn ")
        End If

        '***********************************************************************
        ' 請求明細テーブル：明細件数(外部結合)●
        '***********************************************************************
        cmdTextSb.Append(" LEFT OUTER JOIN ")
        cmdTextSb.Append(" ( SELECT  ")
        cmdTextSb.Append("      SM.seikyuusyo_no ")
        cmdTextSb.Append("      ,COUNT(SM.seikyuusyo_no) AS meisai_kensuu ")
        cmdTextSb.Append("  FROM ")
        cmdTextSb.Append("      t_seikyuu_meisai SM WITH (READCOMMITTED) ")
        cmdTextSb.Append("  WHERE ")
        cmdTextSb.Append("      SM.inji_taisyo_flg = 1 ")
        cmdTextSb.Append("  GROUP BY SM.seikyuusyo_no ")
        cmdTextSb.Append("  ) AS SUB ")
        cmdTextSb.Append("  ON SK.seikyuusyo_no = SUB.seikyuusyo_no ")

        '***********************************************************************
        ' 請求先マスタ：請求締め日(外部結合)
        '***********************************************************************
        cmdTextSb.Append(" LEFT OUTER JOIN m_seikyuu_saki MSM WITH (READCOMMITTED) ")
        cmdTextSb.Append("    ON SK.seikyuu_saki_cd = MSM.seikyuu_saki_cd ")
        cmdTextSb.Append("   AND SK.seikyuu_saki_brc = MSM.seikyuu_saki_brc ")
        cmdTextSb.Append("   AND SK.seikyuu_saki_kbn = MSM.seikyuu_saki_kbn ")

        Return cmdTextSb.ToString
    End Function

    ''' <summary>
    ''' 請求データ取得用の共通TABLEクエリを取得[CSV]
    ''' </summary>
    ''' <param name="keyRec">請求データKEYレコードクラス</param>
    ''' <param name="emType">請求データの検索タイプ</param>
    ''' <returns>請求データ取得用の共通TABLEクエリを取得[CSV]</returns>
    ''' <remarks></remarks>
    Private Function GetCmnSqlTableCsv(ByVal keyRec As SeikyuuDataKeyRecord, ByVal emType As EarthEnum.emSeikyuuSearchType) As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetCmnSqlTableCsv", keyRec, emType)

        'SQL文の生成
        Dim cmdTextSb As New StringBuilder

        cmdTextSb.Append(" FROM ")
        cmdTextSb.Append("   t_seikyuu_kagami SK WITH (READCOMMITTED) ")

        '***********************************************************************
        ' 請求明細T(外部結合)
        '***********************************************************************
        cmdTextSb.Append(" INNER JOIN t_seikyuu_meisai SM WITH (READCOMMITTED) ")
        cmdTextSb.Append("    ON SK.seikyuusyo_no = SM.seikyuusyo_no ")
        cmdTextSb.Append("    AND SM.inji_taisyo_flg = 1 ")

        '***********************************************************************
        ' 売上データT(内部結合)
        '***********************************************************************
        cmdTextSb.Append(" INNER JOIN t_uriage_data URI WITH (READCOMMITTED) ")
        cmdTextSb.Append("    ON SM.denpyou_unique_no = URI.denpyou_unique_no ")

        '***********************************************************************
        ' 請求先情報取得ビュー：請求先名カナ(外部結合)
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.SeikyuuSakiMeiKana) Then
            cmdTextSb.Append(" LEFT OUTER JOIN v_seikyuu_saki_info VIW WITH (READCOMMITTED) ")
            cmdTextSb.Append("    ON SK.seikyuu_saki_cd = VIW.seikyuu_saki_cd ")
            cmdTextSb.Append("   AND SK.seikyuu_saki_brc = VIW.seikyuu_saki_brc ")
            cmdTextSb.Append("   AND SK.seikyuu_saki_kbn = VIW.seikyuu_saki_kbn ")
        End If

        '***********************************************************************
        ' 請求明細テーブル：明細件数(サブクエリ)●
        '***********************************************************************
        If emType = EarthEnum.emSeikyuuSearchType.SearchSeikyuusyo Then '請求書一覧画面
            If Not (keyRec.MeisaiKensuuFrom = Integer.MinValue) Or Not (keyRec.MeisaiKensuuTo = Integer.MinValue) Then
                cmdTextSb.Append(" INNER JOIN ")
                cmdTextSb.Append(" ( SELECT  ")
                cmdTextSb.Append("      SM.seikyuusyo_no ")
                cmdTextSb.Append("      ,COUNT(SM.seikyuusyo_no) AS meisai_kensuu ")
                cmdTextSb.Append("  FROM ")
                cmdTextSb.Append("      t_seikyuu_meisai SM WITH (READCOMMITTED) ")
                cmdTextSb.Append("  WHERE ")
                cmdTextSb.Append("      SM.inji_taisyo_flg = 1 ")
                cmdTextSb.Append("  GROUP BY SM.seikyuusyo_no ")
                cmdTextSb.Append("  ) AS SUB ")
                cmdTextSb.Append("  ON SK.seikyuusyo_no = SUB.seikyuusyo_no ")
            End If
        End If

        '***********************************************************************
        ' 請求先マスタ：請求締め日(外部結合)
        '***********************************************************************
        cmdTextSb.Append(" LEFT OUTER JOIN m_seikyuu_saki MSM WITH (READCOMMITTED) ")
        cmdTextSb.Append("    ON SK.seikyuu_saki_cd = MSM.seikyuu_saki_cd ")
        cmdTextSb.Append("   AND SK.seikyuu_saki_brc = MSM.seikyuu_saki_brc ")
        cmdTextSb.Append("   AND SK.seikyuu_saki_kbn = MSM.seikyuu_saki_kbn ")

        Return cmdTextSb.ToString
    End Function

#End Region

#Region "WHERE句"
    ''' <summary>
    ''' 請求データ取得用の共通WHEREクエリを取得
    ''' </summary>
    ''' <param name="keyRec">請求データKEYレコードクラス</param>
    ''' <param name="emType">請求データの検索タイプ</param>
    ''' <returns>請求データ取得用の共通WHEREクエリを取得</returns>
    ''' <remarks></remarks>
    Private Function GetCmnSqlWhere(ByVal keyRec As SeikyuuDataKeyRecord, ByVal emType As EarthEnum.emSeikyuuSearchType) As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetCmnSqlWhere", keyRec, emType)

        'SQL文の生成
        Dim cmdTextSb As New StringBuilder

        cmdTextSb.Append(" WHERE 1 = 1 ")

        '***********************************************************************
        ' 請求先名カナ
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.SeikyuuSakiMeiKana) Then
            cmdTextSb.Append("   AND VIW.seikyuu_saki_kana like " & DBprmSeikyuuSakiMeiKana)
        End If

        '***********************************************************************
        ' 請求先区分
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.SeikyuuSakiKbn) Then
            cmdTextSb.Append(" AND SK.seikyuu_saki_kbn = " & DBprmSeikyuuSakiKbn)
        End If

        '***********************************************************************
        ' 請求先コード
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.SeikyuuSakiCd) Then
            cmdTextSb.Append(" AND SK.seikyuu_saki_cd = " & DBprmSeikyuuSakiCd)
        End If

        '***********************************************************************
        ' 請求先枝番
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.SeikyuuSakiBrc) Then
            cmdTextSb.Append(" AND SK.seikyuu_saki_brc = " & DBprmSeikyuuSakiBrc)
        End If

        '***********************************************************************
        ' 請求書発行日
        '***********************************************************************
        If keyRec.SeikyuusyoHakDateFrom <> DateTime.MinValue Or _
            keyRec.SeikyuusyoHakDateTo <> DateTime.MinValue Then

            cmdTextSb.Append(" AND ")

            If keyRec.SeikyuusyoHakDateFrom <> DateTime.MinValue And _
                keyRec.SeikyuusyoHakDateTo <> DateTime.MinValue Then
                '両方指定ありはBETWEEN
                cmdTextSb.Append(" CONVERT(VARCHAR, SK.seikyuusyo_hak_date ,111) BETWEEN " & DBprmSeikyuusyoHakDateFrom)
                cmdTextSb.Append(" AND ")
                cmdTextSb.Append(DBprmSeikyuusyoHakDateTo)
            Else
                If keyRec.SeikyuusyoHakDateFrom <> DateTime.MinValue Then
                    'Fromのみ
                    cmdTextSb.Append(" CONVERT(VARCHAR, SK.seikyuusyo_hak_date ,111) >= " & DBprmSeikyuusyoHakDateFrom)
                Else
                    'Toのみ
                    cmdTextSb.Append(" CONVERT(VARCHAR, SK.seikyuusyo_hak_date ,111) <= " & DBprmSeikyuusyoHakDateTo)
                End If
            End If
        End If



        '***********************************************************************
        ' 請求締め日●請求書一覧画面,過去請求書一覧画面
        '***********************************************************************
        If keyRec.SeikyuuSimeDate <> String.Empty Then
            cmdTextSb.Append(" AND RIGHT ('00' + MSM.seikyuu_sime_date, 2) = " & DBprmSeikyuuSimeDate)
        End If

        '***********************************************************************
        ' 請求書用紙●請求書一覧画面,過去請求書一覧画面
        '***********************************************************************
        If keyRec.SeikyuuSyosiki <> String.Empty Then
            If keyRec.SeikyuuSyosiki = EarthConst.ISNULL Then
                cmdTextSb.Append(" AND SK.kaisyuu_seikyuusyo_yousi IS NULL ")
            Else
                cmdTextSb.Append(" AND SK.kaisyuu_seikyuusyo_yousi = " & DBprmSeikyuuSyosiki)
            End If

        End If

        If emType = EarthEnum.emSeikyuuSearchType.SearchSeikyuusyo Then '1.請求書一覧画面

            '***********************************************************************
            ' 明細件数(サブクエリ)●1.請求書一覧画面
            '***********************************************************************
            If Not (keyRec.MeisaiKensuuFrom = Integer.MinValue) Or Not (keyRec.MeisaiKensuuTo = Integer.MinValue) Then

                cmdTextSb.Append(" AND ")

                If Not (keyRec.MeisaiKensuuFrom = Integer.MinValue) And Not (keyRec.MeisaiKensuuTo = Integer.MinValue) Then
                    ' 両方指定有りはBETWEEN
                    cmdTextSb.Append(" SUB.meisai_kensuu BETWEEN " & DBprmMeisaiKensuuFrom)
                    cmdTextSb.Append(" AND ")
                    cmdTextSb.Append(DBprmMeisaiKensuuTo)
                Else
                    If Not keyRec.MeisaiKensuuFrom = Integer.MinValue Then
                        ' Fromのみ
                        cmdTextSb.Append(" SUB.meisai_kensuu >= " & DBprmMeisaiKensuuFrom)
                    Else
                        ' Toのみ
                        cmdTextSb.Append(" SUB.meisai_kensuu <= " & DBprmMeisaiKensuuTo)
                    End If
                End If
            End If

        End If

        '***********************************************************************
        ' 取消
        '***********************************************************************
        If keyRec.Torikesi = 0 Then
            cmdTextSb.Append("  AND SK.torikesi = 0 ")
        End If

        '***********************************************************************
        ' 印字用紙対象外●過去請求書一覧画面
        '***********************************************************************
        If emType = EarthEnum.emSeikyuuSearchType.KakoSearchSeikyuusyo Then
            If keyRec.InjiYousi = 0 Then
                cmdTextSb.Append("  AND (SK.kaisyuu_seikyuusyo_yousi_hannyou_cd IS NULL OR SUBSTRING(SK.kaisyuu_seikyuusyo_yousi_hannyou_cd, 1, 1) <> '9') ")
            End If
        End If

        '***********************************************************************
        ' 請求書印刷日(呼出元画面別処理)●請求書一覧画面,過去請求書一覧画面
        '***********************************************************************
        If emType = EarthEnum.emSeikyuuSearchType.SearchSeikyuusyo Then '請求書一覧画面
            cmdTextSb.Append("  AND SK.seikyuusyo_insatu_date IS NULL ")
        ElseIf emType = EarthEnum.emSeikyuuSearchType.KakoSearchSeikyuusyo Then '過去請求書一覧画面
            cmdTextSb.Append("  AND SK.seikyuusyo_insatu_date IS NOT NULL ")
        End If

        Return cmdTextSb.ToString
    End Function
#End Region

#Region "ORDER BY句"
    ''' <summary>
    ''' 請求データ取得用の共通ORDER BYクエリを取得
    ''' </summary>
    ''' <returns>請求データ取得用の共通ORDER BYクエリを取得</returns>
    ''' <remarks></remarks>
    Private Function GetCmnSqlOrderBy() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetCmnSqlOrderBy")

        'SQL文の生成
        Dim cmdTextSb As New StringBuilder

        cmdTextSb.Append(" ORDER BY ")
        cmdTextSb.Append("   SK.seikyuusyo_hak_date ")
        cmdTextSb.Append("   , SK.seikyuusyo_no ")

        Return cmdTextSb.ToString
    End Function
#End Region

#Region "SQLパラメータ"

#Region "パラメータ定義"
    '請求書NO
    Private Const DBprmSeikyuusyoNo As String = "@SEIKYUU_NO"
    '請求書発行日From,To
    Private Const DBprmSeikyuusyoHakDateFrom As String = "@SEIKYUU_DATE_FROM"
    Private Const DBprmSeikyuusyoHakDateTo As String = "@SEIKYUU_DATE_TO"
    '請求先コード
    Private Const DBprmSeikyuuSakiCd As String = "@SEIKYUUSAKI_CD"
    '請求先枝番
    Private Const DBprmSeikyuuSakiBrc As String = "@SEIKYUUSAKI_BRC"
    '請求先区分
    Private Const DBprmSeikyuuSakiKbn As String = "@SEIKYUUSAKI_KBN"
    '請求先名カナ
    Private Const DBprmSeikyuuSakiMeiKana As String = "@SEIKYUUSAKIMEI_KANA"
    '請求締日
    Private Const DBprmSeikyuuSimeDate As String = "@SEIKYUU_SIME_DATE"
    '請求書式
    Private Const DBprmSeikyuuSyosiki As String = "@SEIKYUU_SYOSIKI"
    '明細件数From,To
    Private Const DBprmMeisaiKensuuFrom As String = "@MEISAI_KENSUU_FROM"
    Private Const DBprmMeisaiKensuuTo As String = "@MEISAI_KENSUU_TO"
    '伝票ユニークNO
    Private Const DBprmMeisaiDenUnqNo As String = "@MEISAI_DEN_UNQ_NO"
    '更新日時
    Private Const DBparamAddDateTime As String = "@ADD_DATETIME"
    '更新者ID
    Private Const DBparamAddLoginUserId As String = "@ADD_LOGINU_SER_ID"
#End Region

    ''' <summary>
    ''' 請求データ取得用の共通SQLパラメータを取得
    ''' </summary>
    ''' <param name="keyRec">請求データKEYレコードクラス</param>
    ''' <returns>請求データ取得用の共通SQLクエリ</returns>
    ''' <remarks></remarks>
    Private Function GetSeikyuuSqlCmnParams(ByVal keyRec As SeikyuuDataKeyRecord) As SqlParameter()
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSeikyuuSqlCmnParams", keyRec)

        '請求書発行日From,To
        Dim objSeikyuusyoHakDateFrom As Object = IIf(keyRec.SeikyuusyoHakDateFrom = DateTime.MinValue, DBNull.Value, keyRec.SeikyuusyoHakDateFrom)
        Dim objSeikyuusyoHakDateTo As Object = IIf(keyRec.SeikyuusyoHakDateTo = DateTime.MinValue, DBNull.Value, keyRec.SeikyuusyoHakDateTo)
        '請求先
        Dim objSeikyuuSakiKbn As Object = IIf(keyRec.SeikyuuSakiKbn = String.Empty, DBNull.Value, keyRec.SeikyuuSakiKbn)
        Dim objSeikyuuSakiCd As Object = IIf(keyRec.SeikyuuSakiCd = String.Empty, DBNull.Value, keyRec.SeikyuuSakiCd)
        Dim objSeikyuuSakiBrc As Object = IIf(keyRec.SeikyuuSakiBrc = String.Empty, DBNull.Value, keyRec.SeikyuuSakiBrc)
        '請求先名カナ
        Dim objSeikyuuSakiMeiKana As Object = IIf(keyRec.SeikyuuSakiMeiKana = String.Empty, DBNull.Value, keyRec.SeikyuuSakiMeiKana)
        '請求締日
        Dim objSeikyuuSimeDate As Object = IIf(keyRec.SeikyuuSimeDate = String.Empty, DBNull.Value, keyRec.SeikyuuSimeDate)
        '請求書式
        Dim objSeikyuuSyosiki As Object = IIf(keyRec.SeikyuuSyosiki = String.Empty, DBNull.Value, keyRec.SeikyuuSyosiki)
        '明細件数From,To
        Dim objMeisaiKensuuFrom As Object = IIf(keyRec.MeisaiKensuuFrom = Integer.MinValue, DBNull.Value, keyRec.MeisaiKensuuFrom)
        Dim objMeisaiKensuuTo As Object = IIf(keyRec.MeisaiKensuuTo = Integer.MinValue, DBNull.Value, keyRec.MeisaiKensuuTo)

        'パラメータへ設定
        Dim cmdParams() As SqlParameter = { _
                 SQLHelper.MakeParam(DBprmSeikyuusyoHakDateFrom, SqlDbType.DateTime, 16, objSeikyuusyoHakDateFrom), _
                 SQLHelper.MakeParam(DBprmSeikyuusyoHakDateTo, SqlDbType.DateTime, 16, objSeikyuusyoHakDateTo), _
                 SQLHelper.MakeParam(DBprmSeikyuuSakiKbn, SqlDbType.Char, 1, objSeikyuuSakiKbn), _
                 SQLHelper.MakeParam(DBprmSeikyuuSakiCd, SqlDbType.VarChar, 5, objSeikyuuSakiCd), _
                 SQLHelper.MakeParam(DBprmSeikyuuSakiBrc, SqlDbType.VarChar, 2, objSeikyuuSakiBrc), _
                 SQLHelper.MakeParam(DBprmSeikyuuSakiMeiKana, SqlDbType.VarChar, 40, objSeikyuuSakiMeiKana & Chr(37)), _
                 SQLHelper.MakeParam(DBprmSeikyuuSimeDate, SqlDbType.VarChar, 2, objSeikyuuSimeDate), _
                 SQLHelper.MakeParam(DBprmSeikyuuSyosiki, SqlDbType.VarChar, 10, objSeikyuuSyosiki), _
                 SQLHelper.MakeParam(DBprmMeisaiKensuuFrom, SqlDbType.Int, 4, objMeisaiKensuuFrom), _
                 SQLHelper.MakeParam(DBprmMeisaiKensuuTo, SqlDbType.Int, 4, objMeisaiKensuuTo) _
        }

        Return cmdParams
    End Function

#End Region

#End Region

End Class

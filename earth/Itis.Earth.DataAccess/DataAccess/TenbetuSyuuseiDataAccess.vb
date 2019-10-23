Imports System.text
Imports System.Data.SqlClient
Public Class TenbetuSyuuseiDataAccess
    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName
    Private cmnDtAcc As New CmnDataAccess

#Region "メンバ変数"
    Dim connStr As String = ConnectionManager.Instance.ConnectionString
#End Region

    ''' <summary>
    ''' 請求先情報を取得します(FC以外)
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <returns>請求先情報格納データテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetSeikyuuSaki(ByVal strKameitenCd As String) As TenbetuSyuuseiDataSet.SeikyuuSakiTableDataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSeikyuuSaki" _
                                            , strKameitenCd)

        Dim SyuuseiDataSet As New TenbetuSyuuseiDataSet
        Dim SeikyuuSakiTable As TenbetuSyuuseiDataSet.SeikyuuSakiTableDataTable
        Dim commandTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        commandTextSb.Append(" SELECT MKM.kbn ")
        commandTextSb.Append("       ,MDK.kbn_mei ")
        commandTextSb.Append("       ,MKM.kameiten_cd ")
        commandTextSb.Append("       ,MKM.kameiten_mei1 ")
        commandTextSb.Append("       ,MKR.keiretu_cd ")
        commandTextSb.Append("       ,MKR.keiretu_mei ")
        commandTextSb.Append("       ,MEG.eigyousyo_cd ")
        commandTextSb.Append("       ,MEG.eigyousyo_mei ")
        commandTextSb.Append("       ,MKM.tys_seikyuu_saki ")
        commandTextSb.Append("       ,MKM.hansokuhin_seikyuusaki ")
        commandTextSb.Append("   FROM m_kameiten               MKM ")
        commandTextSb.Append("   LEFT OUTER JOIN ")
        commandTextSb.Append("        m_data_kbn               MDK ")
        commandTextSb.Append("     ON MKM.kbn          = MDK.kbn ")
        commandTextSb.Append("   LEFT OUTER JOIN ")
        commandTextSb.Append("        m_keiretu                MKR ")
        commandTextSb.Append("     ON MKM.kbn = MKR.kbn ")
        commandTextSb.Append("    AND MKM.keiretu_cd   = MKR.keiretu_cd ")
        commandTextSb.Append("   LEFT OUTER JOIN ")
        commandTextSb.Append("        m_eigyousyo              MEG ")
        commandTextSb.Append("     ON MKM.eigyousyo_cd = MEG.eigyousyo_cd ")
        commandTextSb.Append("  WHERE MKM.kameiten_cd  = @KAMEITENCD ")

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
        SQLHelper.MakeParam("@KAMEITENCD", SqlDbType.VarChar, 5, strKameitenCd)}

        ' 検索実行
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            SyuuseiDataSet, SyuuseiDataSet.SeikyuuSakiTable.TableName, cmdParams)
        SeikyuuSakiTable = SyuuseiDataSet.SeikyuuSakiTable

        Return SeikyuuSakiTable
    End Function

    ''' <summary>
    ''' 請求先情報を取得します(FC)
    ''' </summary>
    ''' <param name="strEigyousyoCd">営業所コード</param>
    ''' <returns>請求先情報格納データテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetFcSeikyuusaki(ByVal strEigyousyoCd As String) As TenbetuSyuuseiDataSet.SeikyuuSakiTableDataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetFcSeikyuusaki" _
                                            , strEigyousyoCd)

        Dim SyuuseiDataSet As New TenbetuSyuuseiDataSet
        Dim SeikyuuSakiTable As TenbetuSyuuseiDataSet.SeikyuuSakiTableDataTable
        Dim commandTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        commandTextSb.Append(" SELECT eigyousyo_cd AS eigyousyo_cd ")
        commandTextSb.Append("       ,eigyousyo_mei AS eigyousyo_mei ")
        commandTextSb.Append("   FROM m_eigyousyo ")
        commandTextSb.Append("  WHERE eigyousyo_cd     = @EIGYOUSYOCD ")

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
        SQLHelper.MakeParam("@EIGYOUSYOCD", SqlDbType.VarChar, 5, strEigyousyoCd)}

        ' 検索実行
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            SyuuseiDataSet, SyuuseiDataSet.SeikyuuSakiTable.TableName, cmdParams)
        SeikyuuSakiTable = SyuuseiDataSet.SeikyuuSakiTable

        Return SeikyuuSakiTable
    End Function

    ''' <summary>
    ''' 登録料情報を取得します
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <returns>登録料情報格納データテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetTourokuRyou(ByVal strKameitenCd As String) As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTourokuRyou" _
                                            , strKameitenCd)

        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        cmdTextSb.Append(" SELECT TTS.mise_cd ")
        cmdTextSb.Append("       ,TTS.bunrui_cd ")
        cmdTextSb.Append("       ,TTS.uri_keijyou_flg ")
        cmdTextSb.Append("       ,TTS.seikyuu_umu ")
        cmdTextSb.Append("       ,TTS.syouhin_cd ")
        cmdTextSb.Append("       ,MSH.syouhin_mei ")
        cmdTextSb.Append("       ,TTS.uri_gaku ")
        cmdTextSb.Append("       ,MSZ.zeiritu ")
        cmdTextSb.Append("       ,TTS.syouhizei_gaku ")
        cmdTextSb.Append("       ,MSZ.zei_kbn ")
        cmdTextSb.Append("       ,TTS.koumuten_seikyuu_gaku ")
        cmdTextSb.Append("       ,TTS.seikyuusyo_hak_date ")
        cmdTextSb.Append("       ,TTS.uri_date ")
        cmdTextSb.Append("       ,TTS.denpyou_uri_date ")
        cmdTextSb.Append("       ,TTS.upd_login_user_id ")
        cmdTextSb.Append("       ,TTS.upd_datetime ")
        cmdTextSb.Append("   FROM t_tenbetu_syoki_seikyuu TTS ")
        cmdTextSb.Append("   LEFT OUTER JOIN ")
        cmdTextSb.Append("        m_syouhin               MSH ")
        cmdTextSb.Append("     ON TTS.syouhin_cd      = MSH.syouhin_cd ")
        cmdTextSb.Append("   LEFT OUTER JOIN ")
        cmdTextSb.Append("        m_syouhizei             MSZ ")
        cmdTextSb.Append("     ON TTS.zei_kbn         = MSZ.zei_kbn ")
        cmdTextSb.Append("  WHERE TTS.mise_cd         = @KAMEITENCD ")
        cmdTextSb.Append("    AND TTS.bunrui_cd       = '200' ")

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
        SQLHelper.MakeParam("@KAMEITENCD", SqlDbType.VarChar, 5, strKameitenCd)}

        'データ取得＆返却
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)

    End Function

    ''' <summary>
    ''' 販促品初期ツール料情報を取得します
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <returns>販促品初期ツール料情報格納データテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetToolRyou(ByVal strKameitenCd As String) As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetToolRyou" _
                                            , strKameitenCd)

        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        cmdTextSb.Append(" SELECT TTS.mise_cd ")
        cmdTextSb.Append("       ,TTS.bunrui_cd ")
        cmdTextSb.Append("       ,TTS.uri_keijyou_flg ")
        cmdTextSb.Append("       ,TTS.seikyuu_umu ")
        cmdTextSb.Append("       ,MSH.syouhin_cd ")
        cmdTextSb.Append("       ,MSH.syouhin_mei ")
        cmdTextSb.Append("       ,TTS.uri_gaku ")
        cmdTextSb.Append("       ,MSZ.zeiritu ")
        cmdTextSb.Append("       ,TTS.syouhizei_gaku ")
        cmdTextSb.Append("       ,MSZ.zei_kbn ")
        cmdTextSb.Append("       ,TTS.seikyuusyo_hak_date ")
        cmdTextSb.Append("       ,TTS.uri_date ")
        cmdTextSb.Append("       ,TTS.denpyou_uri_date ")
        cmdTextSb.Append("       ,TTS.upd_login_user_id ")
        cmdTextSb.Append("       ,TTS.upd_datetime ")
        cmdTextSb.Append("   FROM t_tenbetu_syoki_seikyuu TTS ")
        cmdTextSb.Append("   LEFT OUTER JOIN ")
        cmdTextSb.Append("        m_syouhin               MSH ")
        cmdTextSb.Append("     ON TTS.syouhin_cd      = MSH.syouhin_cd ")
        cmdTextSb.Append("   LEFT OUTER JOIN ")
        cmdTextSb.Append("        m_syouhizei             MSZ ")
        cmdTextSb.Append("     ON TTS.zei_kbn         = MSZ.zei_kbn ")
        cmdTextSb.Append("  WHERE TTS.mise_cd         = @KAMEITENCD ")
        cmdTextSb.Append("    AND TTS.bunrui_cd       = '210' ")

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
        SQLHelper.MakeParam("@KAMEITENCD", SqlDbType.VarChar, 5, strKameitenCd)}

        'データ取得＆返却
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)

    End Function

    ''' <summary>
    ''' 販促品情報を取得します
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="strBunruiCd">分類コード</param>
    ''' <param name="intUriFlg">売上計上FLG</param>
    ''' <returns>販促品情報格納データテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetHansokuHin(ByVal strKameitenCd As String _
                                , ByVal strBunruiCd As String _
                                , ByVal intUriFlg As Integer) As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetHansokuHin" _
                                                    , strKameitenCd _
                                                    , strBunruiCd _
                                                    , intUriFlg)

        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        cmdTextSb.Append(" SELECT SUMARY.* ")
        cmdTextSb.Append("       ,INDIVIDUAL.* ")
        cmdTextSb.Append("   FROM ( ")
        cmdTextSb.Append("         SELECT SUM(CAST(TTS.tanka AS BIGINT) * CAST(TTS.suu AS BIGINT)) AS sum_tanka ")
        cmdTextSb.Append("               ,SUM(ROUND((CAST(TTS.tanka AS BIGINT) * CAST(TTS.suu AS BIGINT) * MSZ.zeiritu),0,1)) AS sum_syouhi_zei ")
        cmdTextSb.Append("               ,SUM(CAST(TTS.tanka AS BIGINT) * CAST(TTS.suu AS BIGINT) + ROUND((CAST(TTS.tanka AS BIGINT) * CAST(TTS.suu AS BIGINT) * MSZ.zeiritu),0,1)) AS sum_zeikomi_gaku ")
        cmdTextSb.Append("               ,SUM(CAST(TTS.koumuten_seikyuu_tanka AS BIGINT) * CAST(TTS.suu AS BIGINT) + ROUND((CAST(TTS.koumuten_seikyuu_tanka AS BIGINT) * CAST(TTS.suu AS BIGINT)) * MSZ.zeiritu,0,1)) AS sum_koumuten_gaku ")
        cmdTextSb.Append("           FROM t_tenbetu_seikyuu       TTS ")
        cmdTextSb.Append("           LEFT OUTER JOIN ")
        cmdTextSb.Append("                m_syouhin               MSH ")
        cmdTextSb.Append("             ON TTS.syouhin_cd      = MSH.syouhin_cd ")
        cmdTextSb.Append("           LEFT OUTER JOIN ")
        cmdTextSb.Append("                m_syouhizei             MSZ ")
        cmdTextSb.Append("             ON TTS.zei_kbn         = MSZ.zei_kbn ")
        cmdTextSb.Append("          WHERE TTS.mise_cd         = @KAMEITENCD ")
        cmdTextSb.Append("            AND TTS.bunrui_cd       = @BUNRUICD ")
        If intUriFlg >= 0 Then
            cmdTextSb.Append("        AND TTS.uri_keijyou_flg      = @URIKEIJYOUFLG ")
        End If
        cmdTextSb.Append("          GROUP BY mise_cd,bunrui_cd ")
        cmdTextSb.Append("        ) SUMARY ")
        cmdTextSb.Append("       ,( ")
        cmdTextSb.Append("         SELECT TTS.mise_cd ")
        cmdTextSb.Append("               ,TTS.bunrui_cd ")
        cmdTextSb.Append("               ,TTS.hassou_date ")
        cmdTextSb.Append("               ,TTS.syouhin_cd ")
        cmdTextSb.Append("               ,MSH.syouhin_mei ")
        cmdTextSb.Append("               ,MSZ.zeiritu ")
        cmdTextSb.Append("               ,TTS.koumuten_seikyuu_tanka ")
        cmdTextSb.Append("               ,TTS.tanka ")
        cmdTextSb.Append("               ,TTS.suu ")
        cmdTextSb.Append("               ,TTS.syouhizei_gaku ")
        cmdTextSb.Append("               ,MSZ.zei_kbn ")
        cmdTextSb.Append("               ,TTS.seikyuusyo_hak_date ")
        cmdTextSb.Append("               ,TTS.uri_date ")
        cmdTextSb.Append("               ,TTS.denpyou_uri_date ")
        cmdTextSb.Append("               ,TTS.nyuuryoku_date ")
        cmdTextSb.Append("               ,TTS.nyuuryoku_date_no ")
        cmdTextSb.Append("               ,TTS.uri_keijyou_flg ")
        cmdTextSb.Append("               ,TTS.uri_keijyou_date ")
        cmdTextSb.Append("               ,TTS.upd_login_user_id ")
        cmdTextSb.Append("               ,TTS.upd_datetime ")
        cmdTextSb.Append("               ,MKM.tys_seikyuu_saki ")
        cmdTextSb.Append("               ,MKM.hansokuhin_seikyuusaki ")
        cmdTextSb.Append("               ,MKM.keiretu_cd ")
        cmdTextSb.Append("           FROM t_tenbetu_seikyuu       TTS ")
        cmdTextSb.Append("           LEFT OUTER JOIN ")
        cmdTextSb.Append("                m_syouhin               MSH ")
        cmdTextSb.Append("             ON TTS.syouhin_cd      = MSH.syouhin_cd ")
        cmdTextSb.Append("           LEFT OUTER JOIN ")
        cmdTextSb.Append("                m_syouhizei             MSZ ")
        cmdTextSb.Append("             ON TTS.zei_kbn         = MSZ.zei_kbn ")
        cmdTextSb.Append("           LEFT OUTER JOIN ")
        cmdTextSb.Append("                m_kameiten              MKM ")
        cmdTextSb.Append("             ON TTS.mise_cd         = MKM.kameiten_cd ")
        cmdTextSb.Append("          WHERE TTS.mise_cd         = @KAMEITENCD ")
        cmdTextSb.Append("            AND TTS.bunrui_cd       = @BUNRUICD ")
        If intUriFlg >= 0 Then
            cmdTextSb.Append("        AND TTS.uri_keijyou_flg      = @URIKEIJYOUFLG ")
        End If
        cmdTextSb.Append("        ) INDIVIDUAL ")

        ' パラメータへ設定
        If intUriFlg >= 0 Then
            cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam("@KAMEITENCD", SqlDbType.VarChar, 5, strKameitenCd), _
            SQLHelper.MakeParam("@BUNRUICD", SqlDbType.VarChar, 3, strBunruiCd), _
            SQLHelper.MakeParam("@URIKEIJYOUFLG", SqlDbType.VarChar, 4, intUriFlg)}
        Else
            cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam("@KAMEITENCD", SqlDbType.VarChar, 5, strKameitenCd), _
            SQLHelper.MakeParam("@BUNRUICD", SqlDbType.VarChar, 3, strBunruiCd)}
        End If

        'データ取得＆返却
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)

    End Function

    ''' <summary>
    ''' 商品の明細情報を取得します
    ''' </summary>
    ''' <param name="strSoukoCd">倉庫コード</param>
    ''' <returns>商品マスタ.標準価格格納データテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetSyouhinMeisai(ByVal strSyouhinCd As String _
                                    , ByVal strSoukoCd As String) As TenbetuSyuuseiDataSet.SyouhinMeisaiDataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSyouhinMeisai" _
                                                    , strSyouhinCd _
                                                    , strSoukoCd)

        Dim SyuuseiDataSet As New TenbetuSyuuseiDataSet
        Dim syouhinMeisaiTable As TenbetuSyuuseiDataSet.SyouhinMeisaiDataTable
        Dim commandTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        commandTextSb.Append(" SELECT MS.syouhin_cd ")
        commandTextSb.Append("       ,MS.syouhin_mei ")
        commandTextSb.Append("       ,MS.hyoujun_kkk ")
        commandTextSb.Append("       ,MS.souko_cd ")
        commandTextSb.Append("       ,MS.zei_kbn ")
        commandTextSb.Append("       ,MS.siire_kkk ")
        commandTextSb.Append("       ,MZ.zeiritu ")
        commandTextSb.Append("   FROM m_syouhin   MS ")
        commandTextSb.Append("       ,m_syouhizei MZ ")
        commandTextSb.Append("  WHERE MS.syouhin_cd = @SYOUHINCD ")
        commandTextSb.Append("    AND MS.souko_cd   = @SOUKOCD ")
        commandTextSb.Append("    AND MS.zei_kbn    = MZ.zei_kbn ")
        commandTextSb.Append("  ORDER BY ")
        commandTextSb.Append("        MS.syouhin_cd ")

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
        SQLHelper.MakeParam("@SYOUHINCD", SqlDbType.VarChar, 8, strSyouhinCd), _
        SQLHelper.MakeParam("@SOUKOCD", SqlDbType.VarChar, 3, strSoukoCd)}

        ' 検索実行
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            SyuuseiDataSet, SyuuseiDataSet.SyouhinMeisai.TableName, cmdParams)
        syouhinMeisaiTable = SyuuseiDataSet.SyouhinMeisai

        Return syouhinMeisaiTable
    End Function

    ''' <summary>
    ''' 更新対象の店別初期請求テーブルを取得
    ''' </summary>
    ''' <param name="strMiseCd">店コード</param>
    ''' <param name="strBunruiCd">分類コード</param>
    ''' <returns>更新対象の店別初期請求テーブル情報を格納したデータテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetUpdateTargetTenbetuSyoki(ByVal strMiseCd As String, ByVal strBunruiCd As String) As TenbetuSyokiSeikyuuDataSet.TenbetuSyokiSeikyuuDataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetUpdateTargetTenbetuSyoki" _
                                                    , strMiseCd _
                                                    , strBunruiCd)
        Dim TenbetuSyokiSeikyuuDataSet As New TenbetuSyokiSeikyuuDataSet
        Dim TenbetuSyokiSeikyuuTable As TenbetuSyokiSeikyuuDataSet.TenbetuSyokiSeikyuuDataTable
        Dim commandTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        commandTextSb.Append(" SELECT mise_cd ")
        commandTextSb.Append("       ,bunrui_cd ")
        commandTextSb.Append("       ,add_date ")
        commandTextSb.Append("       ,seikyuusyo_hak_date ")
        commandTextSb.Append("       ,uri_date ")
        commandTextSb.Append("       ,denpyou_uri_date ")
        commandTextSb.Append("       ,seikyuu_umu ")
        commandTextSb.Append("       ,uri_keijyou_flg ")
        commandTextSb.Append("       ,uri_keijyou_date ")
        commandTextSb.Append("       ,syouhin_cd ")
        commandTextSb.Append("       ,uri_gaku ")
        commandTextSb.Append("       ,zei_kbn ")
        commandTextSb.Append("       ,bikou ")
        commandTextSb.Append("       ,koumuten_seikyuu_gaku ")
        commandTextSb.Append("       ,add_login_user_id ")
        commandTextSb.Append("       ,add_datetime ")
        commandTextSb.Append("       ,upd_login_user_id ")
        commandTextSb.Append("       ,upd_datetime ")
        commandTextSb.Append("   FROM t_tenbetu_syoki_seikyuu WITH(UPDLOCK) ")
        commandTextSb.Append("  WHERE mise_cd    = @MISECD ")
        commandTextSb.Append("    AND bunrui_cd  = @BUNRUICD ")

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
        SQLHelper.MakeParam("@MISECD", SqlDbType.VarChar, 5, strMiseCd), _
        SQLHelper.MakeParam("@BUNRUICD", SqlDbType.VarChar, 3, strBunruiCd)}

        ' 検索実行
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            TenbetuSyokiSeikyuuDataSet, TenbetuSyokiSeikyuuDataSet.TenbetuSyokiSeikyuu.TableName, cmdParams)
        TenbetuSyokiSeikyuuTable = TenbetuSyokiSeikyuuDataSet.TenbetuSyokiSeikyuu

        Return TenbetuSyokiSeikyuuTable
    End Function

    ''' <summary>
    ''' 更新対象の店別請求テーブルを取得
    ''' </summary>
    ''' <param name="strMiseCd">店コード</param>
    ''' <param name="strBunruiCd">分類コード</param>
    ''' <param name="dtNyuuryokuDate">入力日</param>
    ''' <param name="intNyuuryokuDateNo">入力日NO</param>
    ''' <returns>更新対象の店別請求テーブル情報を格納したデータテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetUpdateTargetTenbetuSeikyuu(ByVal strMiseCd As String _
                                                , ByVal strBunruiCd As String _
                                                , ByVal dtNyuuryokuDate As DateTime _
                                                , ByVal intNyuuryokuDateNo As Integer) As TenbetuSeikyuuDataSet.TenbetuSeikyuuDataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetUpdateTargetTenbetuSeikyuu" _
                                                    , strMiseCd _
                                                    , strBunruiCd _
                                                    , dtNyuuryokuDate _
                                                    , intNyuuryokuDateNo)
        Dim TenbetuSeikyuuDataSet As New TenbetuSeikyuuDataSet
        Dim TenbetuSeikyuuTable As TenbetuSeikyuuDataSet.TenbetuSeikyuuDataTable
        Dim commandTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        commandTextSb.Append(" SELECT mise_cd ")
        commandTextSb.Append("       ,bunrui_cd ")
        commandTextSb.Append("       ,nyuuryoku_date ")
        commandTextSb.Append("       ,nyuuryoku_date_no ")
        commandTextSb.Append("       ,hassou_date ")
        commandTextSb.Append("       ,seikyuusyo_hak_date ")
        commandTextSb.Append("       ,uri_date ")
        commandTextSb.Append("       ,denpyou_uri_date ")
        commandTextSb.Append("       ,uri_keijyou_flg ")
        commandTextSb.Append("       ,uri_keijyou_date ")
        commandTextSb.Append("       ,syouhin_cd ")
        commandTextSb.Append("       ,tanka ")
        commandTextSb.Append("       ,suu ")
        commandTextSb.Append("       ,zei_kbn ")
        commandTextSb.Append("       ,koumuten_seikyuu_tanka ")
        commandTextSb.Append("       ,add_login_user_id ")
        commandTextSb.Append("       ,add_datetime ")
        commandTextSb.Append("       ,upd_login_user_id ")
        commandTextSb.Append("       ,upd_datetime ")
        commandTextSb.Append("   FROM t_tenbetu_seikyuu WITH(UPDLOCK) ")
        commandTextSb.Append("  WHERE mise_cd           = @MISECD ")
        commandTextSb.Append("    AND bunrui_cd         = @BUNRUICD ")
        commandTextSb.Append("    AND nyuuryoku_date    = @NYUURYOKUDATE ")
        commandTextSb.Append("    AND nyuuryoku_date_no = @NYUURYOKUDATENO ")

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
        SQLHelper.MakeParam("@MISECD", SqlDbType.VarChar, 5, strMiseCd), _
        SQLHelper.MakeParam("@BUNRUICD", SqlDbType.VarChar, 3, strBunruiCd), _
        SQLHelper.MakeParam("@NYUURYOKUDATE", SqlDbType.DateTime, 16, dtNyuuryokuDate), _
        SQLHelper.MakeParam("@NYUURYOKUDATENO", SqlDbType.Int, 4, intNyuuryokuDateNo)}

        '検索実行
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            TenbetuSeikyuuDataSet, TenbetuSeikyuuDataSet.TenbetuSeikyuu.TableName, cmdParams)
        TenbetuSeikyuuTable = TenbetuSeikyuuDataSet.TenbetuSeikyuu

        Return TenbetuSeikyuuTable

    End Function

    ''' <summary>
    ''' 更新対象の店別請求テーブルから入力日NOの最大値を取得
    ''' </summary>
    ''' <param name="strMiseCd">店コード</param>
    ''' <param name="strBunruiCd">分類コード</param>
    ''' <param name="dtNyuuryokuDate">入力日</param>
    ''' <returns>入力日NOの最大値</returns>
    ''' <remarks></remarks>
    Public Function GetUpdateTargetMaxNyuuryokuNo(ByVal strMiseCd As String _
                                                , ByVal strBunruiCd As String _
                                                , ByVal dtNyuuryokuDate As DateTime) As TenbetuSeikyuuDataSet.MaxNoDataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetUpdateTargetMaxNyuuryokuNo" _
                                                    , strMiseCd _
                                                    , strBunruiCd _
                                                    , dtNyuuryokuDate)
        Dim TenbetuSeikyuuDataSet As New TenbetuSeikyuuDataSet
        Dim MaxNoTable As TenbetuSeikyuuDataSet.MaxNoDataTable
        Dim commandTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        commandTextSb.Append(" SELECT isnull(MAX(nyuuryoku_date_no), 0) max_no ")
        commandTextSb.Append("   FROM t_tenbetu_seikyuu ")
        commandTextSb.Append("  WHERE mise_cd           = @MISECD ")
        commandTextSb.Append("    AND bunrui_cd         = @BUNRUICD ")
        commandTextSb.Append("    AND nyuuryoku_date    = @NYUURYOKUDATE ")

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
        SQLHelper.MakeParam("@MISECD", SqlDbType.VarChar, 5, strMiseCd), _
        SQLHelper.MakeParam("@BUNRUICD", SqlDbType.VarChar, 3, strBunruiCd), _
        SQLHelper.MakeParam("@NYUURYOKUDATE", SqlDbType.DateTime, 16, dtNyuuryokuDate)}

        '検索実行
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            TenbetuSeikyuuDataSet, TenbetuSeikyuuDataSet.MaxNo.TableName, cmdParams)
        MaxNoTable = TenbetuSeikyuuDataSet.MaxNo

        Return MaxNoTable
    End Function

#Region "販促品請求締め日取得"
    ''' <summary>
    ''' 加盟店マスタより販促品請求締め日を取得する
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <returns>販促品請求締め日</returns>
    ''' <remarks></remarks>
    Public Function GetHansokuhinSeikyuuSimeDateKameiten(ByVal strKameitenCd As String) As String

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetHansokuhinSeikyuuSimeDateKameiten", _
                            strKameitenCd)
        '未入力チェック
        If strKameitenCd = "" Then
            Return ""
        End If

        ' パラメータ
        Const strParamKameitenCd As String = "@KAMEITENCD"

        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append(" SELECT ")
        commandTextSb.Append("    k.hansokuhin_seikyuu_sime_date ")
        commandTextSb.Append(" FROM ")
        commandTextSb.Append("    m_kameiten k WITH (READCOMMITTED) ")
        commandTextSb.Append(" WHERE  ")
        commandTextSb.Append("    k.kameiten_cd = " & strParamKameitenCd)
        commandTextSb.Append(" AND  ")
        commandTextSb.Append("    k.torikesi = 0")

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamKameitenCd, SqlDbType.VarChar, 5, strKameitenCd)}

        ' データの取得
        Dim data As Object = Nothing

        ' 検索実行
        data = SQLHelper.ExecuteScalar(connStr, _
                                       CommandType.Text, _
                                       commandTextSb.ToString(), _
                                       commandParameters)

        ' 取得出来ない場合、空白を返却
        If data Is Nothing OrElse IsDBNull(data) Then
            Return ""
        End If

        Return data

    End Function

    ''' <summary>
    ''' 営業所マスタより販促品請求締め日を取得する
    ''' </summary>
    ''' <param name="strEigyousyoCd">営業所コード</param>
    ''' <returns>販促品請求締め日</returns>
    ''' <remarks></remarks>
    Public Function GetHansokuhinSeikyuuSimeDateEigyousyo(ByVal strEigyousyoCd As String) As String

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetHansokuhinSeikyuuSimeDateEigyousyo", _
                            strEigyousyoCd)
        '未入力チェック
        If strEigyousyoCd = "" Then
            Return ""
        End If

        ' パラメータ
        Const strParamKameitenCd As String = "@EIGYOUSYOCD"

        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append(" SELECT ")
        commandTextSb.Append("    E.hansokuhin_sime_date ")
        commandTextSb.Append(" FROM ")
        commandTextSb.Append("    m_eigyousyo E WITH (READCOMMITTED) ")
        commandTextSb.Append(" WHERE  ")
        commandTextSb.Append("    E.eigyousyo_cd = " & strParamKameitenCd)
        commandTextSb.Append(" AND  ")
        commandTextSb.Append("    E.torikesi = 0")

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamKameitenCd, SqlDbType.VarChar, 5, strEigyousyoCd)}

        ' データの取得
        Dim data As Object = Nothing

        ' 検索実行
        data = SQLHelper.ExecuteScalar(connStr, _
                                       CommandType.Text, _
                                       commandTextSb.ToString(), _
                                       commandParameters)

        ' 取得出来ない場合、空白を返却
        If data Is Nothing OrElse IsDBNull(data) Then
            Return ""
        End If

        Return data

    End Function
#End Region
End Class

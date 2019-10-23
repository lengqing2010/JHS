Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' 商品情報に関する処理を行うデータアクセスクラスです
''' </summary>
''' <remarks></remarks>
Public Class SyouhinDataAccess
    Inherits AbsDataAccess

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName
    Dim cmnDtAcc As New CmnDataAccess

#Region "商品の分類コード"
    ''' <summary>
    ''' 商品の分類コード
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum BunruiCdType
        ''' <summary>
        '''指定なし
        ''' </summary>
        ''' <remarks></remarks>
        SITEINASI = 999
        ''' <summary>
        '''100：調査料金（依頼画面.商品ｺｰﾄﾞ1）
        ''' </summary>
        ''' <remarks></remarks>
        SYOUHIN100 = 100
        ''' <summary>
        '''110：調査料金（依頼画面.商品ｺｰﾄﾞ2）
        ''' </summary>
        ''' <remarks></remarks>
        SYOUHIN110 = 110
        ''' <summary>
        '''115：調査料金（依頼画面.商品ｺｰﾄﾞ2の売上金額がマイナスのデータ）
        ''' </summary>
        ''' <remarks></remarks>
        SYOUHIN115 = 115
        ''' <summary>
        '''120：調査料金（依頼画面.商品ｺｰﾄﾞ3）
        ''' </summary>
        ''' <remarks></remarks>
        SYOUHIN120 = 120
        ''' <summary>
        '''130：工事料金（工事画面.改良工事）
        ''' </summary>
        ''' <remarks></remarks>
        SYOUHIN130 = 130
        ''' <summary>
        '''140：追加改良工事（工事画面.追加改良工事）
        ''' </summary>
        ''' <remarks></remarks>
        SYOUHIN140 = 140
        ''' <summary>
        '''150：調査報告書再発行手数料（報告書画面.調査報告書再発行）
        ''' </summary>
        ''' <remarks></remarks>
        SYOUHIN150 = 150
        ''' <summary>
        '''160：工事報告書再発行手数料（報告書画面.工事報告書再発行）
        ''' </summary>
        ''' <remarks></remarks>
        SYOUHIN160 = 160
        ''' <summary>
        '''170：保証書再発行手数料（保証画面.報告書再発行）
        ''' </summary>
        ''' <remarks></remarks>
        SYOUHIN170 = 170
        ''' <summary>
        '''180：解約払戻（保証画面.解約払戻）
        ''' </summary>
        ''' <remarks></remarks>
        SYOUHIN180 = 180
        ''' <summary>
        ''' 登録料
        ''' </summary>
        ''' <remarks></remarks>
        SYOUHIN200 = 200
        ''' <summary>
        ''' 販促品初期ツール料
        ''' </summary>
        ''' <remarks></remarks>
        SYOUHIN210 = 210
        ''' <summary>
        ''' FC以外販促品
        ''' </summary>
        ''' <remarks></remarks>
        SYOUHIN220 = 220
        ''' <summary>
        ''' FC販促品
        ''' </summary>
        ''' <remarks></remarks>
        SYOUHIN230 = 230
        ''' <summary>
        ''' FC月額割引
        ''' </summary>
        ''' <remarks></remarks>
        SYOUHIN240 = 240
        ''' <summary>
        ''' 商品2(分類コード:110,115)
        ''' </summary>
        ''' <remarks></remarks>
        SYOUHIN2 = 800
    End Enum
#End Region

    ''' <summary>
    ''' コネクションストリング
    ''' </summary>
    ''' <remarks></remarks>
    Private connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>
    ''' 検索工事種類
    ''' </summary>
    ''' <remarks></remarks>
    Private pIntMode As Integer

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="mode"></param>
    ''' <remarks></remarks>
    Public Sub New(Optional ByVal mode As BunruiCdType = BunruiCdType.SITEINASI)
        pIntMode = mode
    End Sub

#Region "価格設定情報取得"
    ''' <summary>
    ''' 商品価格設定マスタより商品情報を取得します
    ''' </summary>
    ''' <param name="kakakuSettei">商品価格設定情報</param>
    ''' <remarks>該当データなしの場合、レコードの商品コードを空白で設定</remarks>
    Public Sub GetKakakuSetteiInfo(ByRef kakakuSettei As KakakuSetteiRecord)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKakakuSetteiInfo", _
                                                    kakakuSettei)

        ' パラメータ
        Const strParamSyouhinKubun As String = "@SYOUHINKUBUN"
        Const strParamCyousaHouhou As String = "@CHOUSAHOUHOU"
        'Const strParamCyousaGaiyou As String = "@CHOUSAGAIYOU"
        Const strParamSyouhinCd As String = "@SYOUHINCD"

        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append(" SELECT syouhin_kbn ")
        commandTextSb.Append("  ,tys_gaiyou ")
        commandTextSb.Append("  ,tys_houhou_no ")
        commandTextSb.Append("  ,ISNULL(syouhin_cd,'')    AS syouhin_cd  ")
        commandTextSb.Append("  ,ISNULL(kkk_settei_basyo,0) AS kkk_settei_basyo ")
        commandTextSb.Append(" FROM m_syouhin_kakakusettei WITH (READCOMMITTED) ")
        commandTextSb.Append(" WHERE syouhin_kbn = " & strParamSyouhinKubun)
        commandTextSb.Append("   AND tys_houhou_no = " & strParamCyousaHouhou)
        commandTextSb.Append("   AND syouhin_cd = " & strParamSyouhinCd)
        'commandTextSb.Append("   AND tys_gaiyou = " & strParamCyousaGaiyou)
        commandTextSb.Append(" ORDER BY tys_gaiyou ")

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamSyouhinKubun, SqlDbType.Int, 0, kakakuSettei.SyouhinKbn), _
             SQLHelper.MakeParam(strParamCyousaHouhou, SqlDbType.Int, 0, kakakuSettei.TyousaHouhouNo), _
             SQLHelper.MakeParam(strParamSyouhinCd, SqlDbType.VarChar, 8, kakakuSettei.SyouhinCd)}

        ' データの取得
        Dim kakakuSetteiDataSet As New KakakuSetteiDataSet()

        ' 検索実行
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            kakakuSetteiDataSet, kakakuSetteiDataSet.SyouhinKakakuSetteiTable.TableName, commandParameters)

        Dim kakakuSetteiTable As KakakuSetteiDataSet.SyouhinKakakuSetteiTableDataTable = _
                    kakakuSetteiDataSet.SyouhinKakakuSetteiTable

        If kakakuSetteiTable.Count <> 0 Then
            ' 取得できた場合、行情報を取得し参照レコードに設定する
            Dim row As KakakuSetteiDataSet.SyouhinKakakuSetteiTableRow = kakakuSetteiTable(0)
            kakakuSettei.SyouhinCd = row.syouhin_cd
            kakakuSettei.KakakuSettei = row.kkk_settei_basyo
            kakakuSettei.TyousaGaiyou = row.tys_gaiyou
        End If

    End Sub
#End Region

#Region "商品情報取得(商品単体 参照渡し)"
    ''' <summary>
    ''' 商品情報、請求先を取得します
    ''' </summary>
    ''' <param name="syouhinCd">商品コード</param>
    ''' <param name="kameitenCd">加盟店コード</param>
    ''' <remarks></remarks>
    Public Function GetSyouhinInfo(ByVal syouhinCd As String, ByVal kameitenCd As String) As DataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSyouhinInfo", _
                                                    syouhinCd, kameitenCd)

        ' パラメータ
        Const strParamSyouhinCd As String = "@SYOUHIN_CD"
        Const strParamKameitenCd As String = "@KAMEITEN_CD"

        Dim sb As New StringBuilder()
        sb.AppendLine("SELECT")
        sb.AppendLine("    s.syouhin_cd")
        sb.AppendLine("    , ISNULL(s.souko_cd, '') AS souko_cd")
        sb.AppendLine("    , ISNULL(s.syouhin_mei, '') AS syouhin_mei")
        sb.AppendLine("    , ISNULL(s.zei_kbn, '') AS zei_kbn")
        sb.AppendLine("    , ISNULL(s.hyoujun_kkk, 0) AS hyoujun_kkk")
        sb.AppendLine("    , ISNULL(z.zeiritu, 0) AS zeiritu")
        sb.AppendLine("    , ISNULL(vsk.kameiten_cd, '') AS kameiten_cd")
        sb.AppendLine("    , ISNULL(vsk.seikyuu_saki_cd, '') AS seikyuu_saki_cd")
        sb.AppendLine("    , ISNULL(vsk.seikyuu_saki_kbn, '') AS seikyuu_saki_kbn")
        sb.AppendLine("    , ISNULL(vsk.seikyuu_saki_brc, '') AS seikyuu_saki_brc ")
        sb.AppendLine("FROM")
        sb.AppendLine("    jhs_sys.m_syouhin s WITH (READCOMMITTED) ")
        sb.AppendLine("    LEFT OUTER JOIN jhs_sys.m_syouhizei z WITH (READCOMMITTED) ")
        sb.AppendLine("        ON s.zei_kbn = z.zei_kbn ")
        sb.AppendLine("    LEFT OUTER JOIN jhs_sys.v_syouhin_seikyuusaki_kameiten vsk ")
        sb.AppendLine("        ON s.syouhin_cd = vsk.syouhin_cd ")
        sb.AppendLine("        AND vsk.kameiten_cd = " & strParamKameitenCd)
        sb.AppendLine("WHERE")
        sb.AppendLine("    s.syouhin_cd = " & strParamSyouhinCd)
        sb.AppendLine(" AND ")
        sb.AppendLine("    s.souko_cd = '100' ")
        'sb.AppendLine(" AND ")
        'sb.AppendLine("    s.torikesi = 0 ")
        sb.AppendLine("")

        ' パラメータへ設定
        Dim sqlParams() As SqlParameter = _
            {SQLHelper.MakeParam(strParamSyouhinCd, SqlDbType.VarChar, 8, syouhinCd), _
             SQLHelper.MakeParam(strParamKameitenCd, SqlDbType.VarChar, 5, kameitenCd)}

        'データ取得＆返却
        Return cmnDtAcc.getDataTable(sb.ToString, sqlParams)

    End Function
#End Region

    ''' <summary>
    ''' 商品情報取得
    ''' </summary>
    ''' <param name="strSyouhinCd">商品コード</param>
    ''' <param name="blnTorikesi">取消</param>
    ''' <returns>データテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetSyouhinInfo(ByVal strSyouhinCd As String, Optional ByVal blnTorikesi As Boolean = False) As DataTable
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSyouhinInfoRec" _
                                                    , strSyouhinCd)
        Dim cmdTextSb As New StringBuilder()

        cmdTextSb.Append(" SELECT ")
        cmdTextSb.Append("      MS.syouhin_cd  ")
        cmdTextSb.Append("    , MS.syouhin_mei ")
        cmdTextSb.Append("    , MS.torikesi  ")
        cmdTextSb.Append("    , MS.zei_kbn ")
        cmdTextSb.Append("    , MS.hyoujun_kkk ")
        cmdTextSb.Append("    , MS.souko_cd ")
        cmdTextSb.Append("    , MS.koj_type  ")
        cmdTextSb.Append("    , MS.tys_umu_kbn  ")
        cmdTextSb.Append("    , MZ.zeiritu  ")
        cmdTextSb.Append("    , MS.sds_jidou_set  ")
        cmdTextSb.Append(" FROM ")
        cmdTextSb.Append("      m_syouhin MS ")
        cmdTextSb.Append("           LEFT OUTER JOIN m_syouhizei MZ ")
        cmdTextSb.Append("             ON MS.zei_kbn= MZ.zei_kbn ")
        cmdTextSb.Append(" WHERE ")
        cmdTextSb.Append("      MS.syouhin_cd = @SYOUHINCD ")
        If blnTorikesi Then
            cmdTextSb.Append("  AND MS.torikesi = 0")
        End If

        ' パラメータへ設定
        Dim cmdParams() As SqlParameter = _
            {SQLHelper.MakeParam("@SYOUHINCD", SqlDbType.VarChar, 8, strSyouhinCd)}

        'データ取得＆返却
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)

    End Function

#Region "商品情報取得"
    ''' <summary>
    ''' 商品情報を取得します
    ''' </summary>
    ''' <param name="strSyouhinCd">商品コード</param>
    ''' <param name="strSyouhinNm">商品名</param>
    ''' <param name="kbnType">商品ｺｰﾄﾞの識別タイプ</param>
    ''' <param name="kameitenCd">加盟店コード(Optional)</param>
    ''' <returns>商品情報DataTable</returns>
    ''' <remarks></remarks>
    Public Function GetSyouhinInfo(ByVal strSyouhinCd As String, _
                                   ByVal strSyouhinNm As String, _
                                   ByRef kbnType As EarthEnum.EnumSyouhinKubun, _
                                   Optional ByVal kameitenCd As String = "") _
                                   As DataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSyouhin23Info", _
                                                    strSyouhinCd, _
                                                    strSyouhinNm, _
                                                    kbnType, _
                                                    kameitenCd)

        ' パラメータ
        Const strParamSyouhinCd As String = "@SYOUHINCD"
        Const strParamSyouhinNm As String = "@SYOUHINNM"
        Const strParamKameitenCd As String = "@KAMEITEN_CD"

        Dim sb As New StringBuilder()
        sb.AppendLine("SELECT")
        sb.AppendLine("    s.syouhin_cd")
        sb.AppendLine("    , s.syouhin_mei AS syouhin_mei")
        sb.AppendLine("    , s.syouhin_kbn3 AS syouhin_kbn3")
        sb.AppendLine("    , s.hyoujun_kkk AS hyoujun_kkk")
        sb.AppendLine("    , s.souko_cd AS souko_cd")
        sb.AppendLine("    , s.zei_kbn AS zei_kbn")
        sb.AppendLine("    , s.siire_kkk AS siire_kkk")
        sb.AppendLine("    , ISNULL(z.zeiritu, 0) AS zeiritu")
        sb.AppendLine("    , s.hosyou_umu AS hosyou_umu")
        If kameitenCd <> String.Empty Then
            sb.AppendLine("    , ISNULL(vsk.kameiten_cd, '') AS kameiten_cd")
            sb.AppendLine("    , ISNULL(vsk.seikyuu_saki_cd, '') AS seikyuu_saki_cd")
            sb.AppendLine("    , ISNULL(vsk.seikyuu_saki_kbn, '') AS seikyuu_saki_kbn")
            sb.AppendLine("    , ISNULL(vsk.seikyuu_saki_brc, '') AS seikyuu_saki_brc ")
        End If
        sb.AppendLine("FROM")
        sb.AppendLine("    jhs_sys.m_syouhin s WITH (READCOMMITTED) ")
        sb.AppendLine("    LEFT OUTER JOIN jhs_sys.m_syouhizei z WITH (READCOMMITTED) ")
        sb.AppendLine("        ON s.zei_kbn = z.zei_kbn ")
        If kameitenCd <> String.Empty Then
            sb.AppendLine("    LEFT OUTER JOIN jhs_sys.v_syouhin_seikyuusaki_kameiten vsk ")
            sb.AppendLine("        ON s.syouhin_cd = vsk.syouhin_cd ")
            sb.AppendLine("        AND vsk.kameiten_cd = " & strParamKameitenCd)
        End If
        sb.AppendLine(" WHERE 0 = 0 ")

        ' 取得する商品分類により条件を分ける
        Dim strSoukoCd As String = String.Empty
        Select Case kbnType
            Case EarthEnum.EnumSyouhinKubun.Syouhin2_110, EarthEnum.EnumSyouhinKubun.Syouhin2_115
                strSoukoCd = EarthConst.SOUKO_CD_SYOUHIN_2_110 + "' OR s.souko_cd = '" + EarthConst.SOUKO_CD_SYOUHIN_2_115
            Case EarthEnum.EnumSyouhinKubun.AllSyouhin, EarthEnum.EnumSyouhinKubun.AllSyouhinTorikesi
                strSoukoCd = String.Empty
            Case Else
                strSoukoCd = CInt(kbnType).ToString
        End Select
        If strSoukoCd <> String.Empty Then
            sb.AppendLine(" AND ( s.souko_cd = '" + strSoukoCd + "' ) ")
        Else
            '初期読込時の場合、商品取消データ(倉庫コード=0)も抽出条件に含める
            If kbnType = EarthEnum.EnumSyouhinKubun.AllSyouhinTorikesi Then
            Else
                '倉庫コードを指定しない場合は、倉庫コード'0'を除外
                sb.AppendLine(" AND  s.souko_cd <> '0'  ")
            End If

        End If

        ' 商品コードが条件に存在する場合
        If strSyouhinCd <> String.Empty Then
            sb.AppendLine(" AND s.syouhin_cd like " & strParamSyouhinCd)
        End If

        ' 商品名が条件に存在する場合
        If strSyouhinNm <> String.Empty Then
            sb.AppendLine(" AND s.syouhin_mei like " & strParamSyouhinNm)
        End If

        ' パラメータへ設定
        Dim sqlParams() As SqlParameter = {SQLHelper.MakeParam(strParamSyouhinCd, SqlDbType.VarChar, 9, strSyouhinCd & Chr(37)), _
                                           SQLHelper.MakeParam(strParamSyouhinNm, SqlDbType.VarChar, 40, strSyouhinNm & Chr(37)), _
                                           SQLHelper.MakeParam(strParamKameitenCd, SqlDbType.VarChar, 5, kameitenCd)}
        'データ取得＆返却
        Return cmnDtAcc.getDataTable(sb.ToString, sqlParams)

    End Function
#End Region

#Region "特定店商品２取得"
    ''' <summary>
    ''' 特定店商品２を取得します
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <returns>商品情報DataTable</returns>
    ''' <remarks></remarks>
    Public Function GetTokuteitenSyouhin2(ByVal strKameitenCd As String) _
                                     As SyouhinDataSet.SyouhinTableDataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTokuteitenSyouhin2", _
                                                    strKameitenCd)

        ' パラメータ
        Const strParamKameitenCd As String = "@KAMEITENCD"

        ' SQL生成
        Dim commandTextSb As New StringBuilder()
        commandTextSb.Append(" SELECT s.syouhin_cd ")
        commandTextSb.Append("   ,s.syouhin_mei AS syouhin_mei ")
        commandTextSb.Append(" FROM m_tokuteiten_syouhin2_settei tok WITH (READCOMMITTED) ")
        commandTextSb.Append(" INNER JOIN m_syouhin s WITH (READCOMMITTED) ")
        commandTextSb.Append(" ON tok.syouhin_cd = s.syouhin_cd ")
        commandTextSb.Append(" WHERE ( s.souko_cd = '110' OR s.souko_cd = '115' ) ")
        commandTextSb.Append(" AND tok.kameiten_cd = " & strParamKameitenCd)
        commandTextSb.Append(" AND s.torikesi = 0 ")
        commandTextSb.Append(" ORDER BY s.syouhin_cd ")

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = Nothing

        ' パラメータを設定
        commandParameters = New SqlParameter() {SQLHelper.MakeParam(strParamKameitenCd, SqlDbType.VarChar, 5, strKameitenCd)}

        ' データの取得
        Dim syouhinDataSet As New SyouhinDataSet()

        ' 検索実行(パラメータ有り)
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            syouhinDataSet, syouhinDataSet.SyouhinTable.TableName, commandParameters)

        Dim syouhinTable As SyouhinDataSet.SyouhinTableDataTable = _
                    syouhinDataSet.SyouhinTable

        Return syouhinTable

    End Function
#End Region

#Region "多棟割商品コード２取得"
    ''' <summary>
    ''' 多棟割引設定マスタより商品コードを取得します
    ''' </summary>
    ''' <param name="kameitenCd">加盟店コード</param>
    ''' <param name="touKbn">棟区分</param>
    ''' <returns>多棟割の商品コード２</returns>
    ''' <remarks>該当データなしの場合、戻り値を空白で返却</remarks>
    Public Function GetTatouwariSyouhinCd(ByVal kameitenCd As String, _
                                          ByVal touKbn As Integer) As String

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTatouwariSyouhinCd", _
                                                    kameitenCd, _
                                                    touKbn)

        ' パラメータ
        Const strParamKameitenCd As String = "@KAMEITENCD"
        Const strParamTouKubun As String = "@TOUKUBUN"

        Dim cmdTextSb As New StringBuilder()

        cmdTextSb.Append(" SELECT ")
        cmdTextSb.Append("      TS.syouhin_cd ")
        cmdTextSb.Append(" FROM ")
        cmdTextSb.Append("      m_tatouwaribiki_settei TS ")
        cmdTextSb.Append("           LEFT OUTER JOIN m_syouhin MS ")
        cmdTextSb.Append("             ON TS.syouhin_cd = MS.syouhin_cd ")
        cmdTextSb.Append(" WHERE ")
        cmdTextSb.Append("  (   TS.syouhin_cd = '00000' ")
        cmdTextSb.Append("      OR ")
        cmdTextSb.Append("      (MS.torikesi = 0 AND MS.souko_cd <> '0') ")
        cmdTextSb.Append("  ) ")
        cmdTextSb.Append("  AND TS.kameiten_cd = " & strParamKameitenCd)
        cmdTextSb.Append("  AND TS.toukubun = " & strParamTouKubun)

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamKameitenCd, SqlDbType.VarChar, 8, kameitenCd), _
             SQLHelper.MakeParam(strParamTouKubun, SqlDbType.Int, 0, touKbn)}

        ' データの取得
        Dim syouhinDataSet As New SyouhinDataSet()

        ' 検索実行
        SQLHelper.FillDataset(connStr, CommandType.Text, cmdTextSb.ToString(), _
            syouhinDataSet, syouhinDataSet.TatouwariTable.TableName, commandParameters)

        Dim tatouSetteiTable As SyouhinDataSet.TatouwariTableDataTable = _
                    syouhinDataSet.TatouwariTable

        If tatouSetteiTable.Count <> 0 Then

            ' 取得できた場合、行情報を取得
            Dim row As SyouhinDataSet.TatouwariTableRow = tatouSetteiTable(0)

            ' 取得値がNullの場合は空白を返す
            If row.syouhin_cd Is Nothing OrElse row.syouhin_cd = String.Empty Then
                Return EarthConst.SH_CD_TATOUWARI_ER
            Else
                Return row.syouhin_cd
            End If
        End If

        Return ""

    End Function
#End Region

#Region "商品コンボデータ作成"
    ''' <summary>
    ''' コンボボックス設定用の有効な商品レコードを全て取得します
    ''' </summary>
    ''' <param name="dt" ></param>
    ''' <param name="withSpaceRow" >先頭に空白行をセットする場合:true</param>
    ''' <param name="withCode" >（任意）ValueにKey項目を付加する場合:true " 1:aaaa "</param>
    ''' <remarks></remarks>
    Public Overrides Sub GetDropdownData(ByRef dt As DataTable, ByVal withSpaceRow As Boolean, Optional ByVal withCode As Boolean = True, Optional ByVal blnTorikesi As Boolean = True)
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetDropdownData", dt, withSpaceRow, withCode)

        Dim connStr As String = ConnectionManager.Instance.ConnectionString
        Dim commandTextSb As New StringBuilder()
        Dim row As SyouhinDataSet.SyouhinTableRow

        commandTextSb.Append(" SELECT ")
        commandTextSb.Append("    syouhin_cd ")
        commandTextSb.Append("    ,syouhin_mei ")
        commandTextSb.Append(" FROM ")
        commandTextSb.Append("    jhs_sys.m_syouhin WITH (READCOMMITTED) ")
        If pIntMode = BunruiCdType.SITEINASI Then '指定なし
        Else
            commandTextSb.Append(" WHERE ")
            If pIntMode = BunruiCdType.SYOUHIN2 Then '商品2を指定(110,115)
                commandTextSb.Append(String.Format(" souko_cd IN ('{0}','{1}') ", CStr(BunruiCdType.SYOUHIN110), CStr(BunruiCdType.SYOUHIN115)))
            Else '分類コードを単一指定
                commandTextSb.Append(String.Format(" souko_cd = '{0}' ", pIntMode.ToString))
            End If
        End If
        '取消の場合はDDLに含めない
        If blnTorikesi Then
            commandTextSb.Append(" AND torikesi = 0 ")
        End If
        commandTextSb.Append(" ORDER BY ")
        commandTextSb.Append("    syouhin_cd")

        ' データの取得
        Dim dsSyouhin As New SyouhinDataSet()
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            dsSyouhin, dsSyouhin.SyouhinTable.TableName)

        Dim syouhinDataTable As SyouhinDataSet.SyouhinTableDataTable = _
                    dsSyouhin.SyouhinTable

        If syouhinDataTable.Count <> 0 Then

            ' 空白行データの設定
            If withSpaceRow = True Then
                dt.Rows.Add(CreateRow("", "", dt))
            End If

            ' 取得データよりDataRowを作成しDataTableにセットする
            For Each row In syouhinDataTable
                If withCode = True Then
                    dt.Rows.Add(CreateRow(row.syouhin_cd + ":" + row.syouhin_mei, row.syouhin_cd, dt))
                Else
                    dt.Rows.Add(CreateRow(row.syouhin_mei, row.syouhin_cd, dt))
                End If
            Next

        End If

    End Sub
#End Region

#Region "保証商品有無"

    ''' <summary>
    ''' 商品情報取得
    ''' </summary>
    ''' <param name="strSyouhinCd">商品コード</param>
    ''' <returns>データテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetSyouhinHosyouUmuInfo(ByVal strSyouhinCd As String) As DataTable
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSyouhinHosyouUmuInfo" _
                                                    , strSyouhinCd)
        Dim cmdTextSb As New StringBuilder()

        cmdTextSb.Append(" SELECT ")
        cmdTextSb.Append("      ISNULL(MAX(MS.hosyou_umu), '') AS hosyou_umu ")
        cmdTextSb.Append(" FROM ")
        cmdTextSb.Append("      m_syouhin MS ")
        cmdTextSb.Append(" WHERE ")
        cmdTextSb.Append("      1=1 ")
        cmdTextSb.Append("  AND MS.syouhin_cd IN('" & strSyouhinCd.Replace(EarthConst.SEP_STRING, "','") & "') ")

        ' パラメータへ設定
        Dim cmdParams() As SqlParameter = {}

        'データ取得＆返却
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)
    End Function

#End Region

#Region "倉庫コード"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="intMode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSyouhinSoukoCd(ByVal intMode As Integer) As DataTable
        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSyouhinSoukoCd" _
                                                    , intMode)

        Dim cmdTextSb As New StringBuilder()

        cmdTextSb.AppendLine(" SELECT ")
        cmdTextSb.AppendLine("    syouhin_cd ")
        cmdTextSb.AppendLine("    ,souko_cd ")
        cmdTextSb.AppendLine(" FROM ")
        cmdTextSb.AppendLine("    jhs_sys.m_syouhin WITH (READCOMMITTED) ")
        cmdTextSb.AppendLine(" WHERE ")

        If intMode = BunruiCdType.SYOUHIN2 Then '商品2を指定(110,115)
            cmdTextSb.Append(String.Format(" souko_cd IN ('{0}','{1}') ", CStr(BunruiCdType.SYOUHIN110), CStr(BunruiCdType.SYOUHIN115)))
        Else '分類コードを単一指定
            cmdTextSb.Append(String.Format(" souko_cd = '{0}' ", pIntMode.ToString))
        End If
        '取消の場合はDDLに含めない
        If intMode = BunruiCdType.SYOUHIN100 Then
            cmdTextSb.Append(" AND torikesi = 0 ")
        End If

        cmdTextSb.AppendLine(" ORDER BY ")
        cmdTextSb.AppendLine("    syouhin_cd")

        'データ取得＆返却
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString)

    End Function
#End Region

End Class

Imports System
Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' 請求書締め日履歴データの取得クラス
''' </summary>
''' <remarks></remarks>
Public Class SeikyuuSimeDateRerekiDataAccess

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

#Region "メンバ変数"
    Dim cmnDtAcc As New CmnDataAccess
#End Region

#Region "請求書締め日履歴データの取得"
    ''' <summary>
    ''' 請求書締め日履歴照会画面/請求書締め日履歴データを取得します
    ''' </summary>
    ''' <param name="keyRec">検索KEYレコードクラス</param>
    ''' <returns>請求書締め日履歴テーブル</returns>
    ''' <remarks></remarks>
    Public Function GetSearchSeikyuuSimeDateRirekiTbl(ByVal keyRec As SeikyuuSimeDateRirekiKeyRecord) As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSearchSeikyuuSimeDateRirekiTbl", keyRec)

        'SQL文の生成
        Dim cmdTextSb As New StringBuilder
        'パラメータ
        Dim cmdParams() As SqlParameter = Me.GetSeikyuuSimeDateRirekiSqlCmnParams(keyRec)

        'SELECT句
        Dim strCmnSelect As String = Me.GetSearchSeikyuuSimeDateRirekiSqlSelect()
        'TABLE句
        Dim strCmnTable As String = Me.GetSearchSeikyuuSimeDateRirekiSqlTable()
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

        '****************
        ' ORDER BY句
        '****************
        cmdTextSb.Append(strCmnOrderBy)

        'データ取得＆返却
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)

    End Function

    ''' <summary>
    ''' 更新対象の請求書締め日履歴データを取得
    ''' </summary>
    ''' <param name="strSeikyuuSakiCd">請求先コード</param>
    ''' <param name="strSeikyuuSakiBrc">請求先枝番</param>
    ''' <param name="strSeikyuuSakiKbn">請求先区分</param>
    ''' <param name="dateSeikyuusyoHakNengetu">請求年月日</param>
    ''' <param name="strSeikyuuSimeDate">請求締め日</param>
    ''' <returns>データテーブル</returns>
    ''' <remarks>請求先・請求年月日をKEYにして取得</remarks>
    Public Function GetSeikyuuSimeDateRirekiSyuuseiList(ByVal strSeikyuuSakiCd As String, _
                                                        ByVal strSeikyuuSakiBrc As String, _
                                                        ByVal strSeikyuuSakiKbn As String, _
                                                        ByVal dateSeikyuusyoHakNengetu As DateTime, _
                                                        ByVal strSeikyuuSimeDate As String _
                                                        ) As DataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSeikyuuSimeDateRirekiSyuuseiRec", _
                                                    strSeikyuuSakiCd, _
                                                    strSeikyuuSakiBrc, _
                                                    strSeikyuuSakiKbn, _
                                                    dateSeikyuusyoHakNengetu, _
                                                    strSeikyuuSimeDate _
                                                    )

        'SQL文の生成
        Dim cmdTextSb As New StringBuilder
        ' パラメータ
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
        cmdTextSb.AppendLine(" WHERE ")
        cmdTextSb.AppendLine("      SR.seikyuu_saki_cd = " & DBprmSeikyuuSakiCd)
        cmdTextSb.AppendLine("  AND SR.seikyuu_saki_brc = " & DBprmSeikyuuSakiBrc)
        cmdTextSb.AppendLine("  AND SR.seikyuu_saki_kbn = " & DBprmSeikyuuSakiKbn)
        cmdTextSb.AppendLine("  AND CONVERT(VARCHAR, SR.seikyuusyo_hak_nengetu, 111) = " & DBprmSeikyuusyoHakNengetu)
        cmdTextSb.AppendLine("  AND SR.seikyuu_sime_date = " & DBprmSeikyuuSimeDate)
        cmdTextSb.AppendLine("  AND SR.torikesi = 0 ")

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
                SQLHelper.MakeParam(DBprmSeikyuuSakiCd, SqlDbType.VarChar, 5, strSeikyuuSakiCd), _
                SQLHelper.MakeParam(DBprmSeikyuuSakiBrc, SqlDbType.VarChar, 2, strSeikyuuSakiBrc), _
                SQLHelper.MakeParam(DBprmSeikyuuSakiKbn, SqlDbType.Char, 1, strSeikyuuSakiKbn), _
                SQLHelper.MakeParam(DBprmSeikyuusyoHakNengetu, SqlDbType.DateTime, 16, dateSeikyuusyoHakNengetu), _
                SQLHelper.MakeParam(DBprmSeikyuuSimeDate, SqlDbType.VarChar, 2, strSeikyuuSimeDate) _
        }

        'データ取得＆返却
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)
    End Function

#End Region

#Region "共通クエリ"

#Region "SELECT句"
    ''' <summary>
    ''' 請求書締め日履歴データ取得用の共通SELECTクエリを取得
    ''' </summary>
    ''' <returns>請求書締め日履歴データ取得用の共通SELECTクエリを取得</returns>
    ''' <remarks></remarks>
    Private Function GetCmnSqlSelect() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetCmnSqlSelect")

        'SQL文の生成
        Dim cmdTextSb As New StringBuilder

        '****************
        '* SELECT項目
        '****************
        cmdTextSb.AppendLine(" SELECT ")
        cmdTextSb.AppendLine("      SR.rireki_no ")
        cmdTextSb.AppendLine("    , SR.torikesi ")
        cmdTextSb.AppendLine("    , SR.seikyuu_saki_cd ")
        cmdTextSb.AppendLine("    , SR.seikyuu_saki_brc ")
        cmdTextSb.AppendLine("    , SR.seikyuu_saki_kbn ")
        cmdTextSb.AppendLine("    , SR.seikyuusyo_hak_nengetu ")
        cmdTextSb.AppendLine("    , SR.seikyuu_sime_date ")
        cmdTextSb.AppendLine("    , SR.zen_taisyou_flg ")
        cmdTextSb.AppendLine("    , SR.seikyuusyo_no ")
        cmdTextSb.AppendLine("    , ISNULL(SR.upd_login_user_id, SR.add_login_user_id) AS upd_login_user_id ")
        cmdTextSb.AppendLine("    , ISNULL(SR.upd_datetime, SR.add_datetime) AS upd_datetime ")
        cmdTextSb.AppendLine("    , CASE ")
        cmdTextSb.AppendLine("           WHEN DR.rireki_no IS NOT NULL THEN 1 ")
        cmdTextSb.AppendLine("           ELSE 0 ")
        cmdTextSb.AppendLine("      END AS max_rireki_no_flg ")

        Return cmdTextSb.ToString
    End Function

    ''' <summary>
    ''' 請求書締め日履歴データ取得用のSELECTクエリを取得
    ''' </summary>
    ''' <returns>請求書締め日履歴データ取得用のSELECTクエリを取得</returns>
    ''' <remarks></remarks>
    Private Function GetSearchSeikyuuSimeDateRirekiSqlSelect() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSearchSeikyuuSimeDateRirekiSqlSelect")

        'SQL文の生成
        Dim cmdTextSb As New StringBuilder

        'SELECT句
        Dim strCmnSelect As String = Me.GetCmnSqlSelect()

        '****************
        '* SELECT項目
        '****************
        cmdTextSb.Append(strCmnSelect)
        cmdTextSb.AppendLine("    , VIW.seikyuu_saki_mei ")
        cmdTextSb.AppendLine("    , VIW.seikyuu_saki_mei2 ")
        cmdTextSb.AppendLine("    , SK.konkai_goseikyuu_gaku ")
        cmdTextSb.AppendLine("    , DATEADD(DAY, + CAST(SR.seikyuu_sime_date As int) - 1, SR.seikyuusyo_hak_nengetu) AS seikyuusyo_hak_date ")
        cmdTextSb.AppendLine("    , ISNULL(SK.upd_login_user_id, SK.add_login_user_id) AS sk_upd_login_user_id ")
        cmdTextSb.AppendLine("    , ISNULL(SK.upd_datetime, SK.add_datetime) AS sk_upd_datetime ")

        Return cmdTextSb.ToString
    End Function

#End Region

#Region "TABLE句"
    ''' <summary>
    ''' 請求書締め日履歴データ取得用の共通TABLEクエリを取得
    ''' </summary>
    ''' <returns>請求書締め日履歴データ取得用の共通TABLEクエリを取得</returns>
    ''' <remarks></remarks>
    Private Function GetCmnSqlTable() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetCmnSqlTable")

        'SQL文の生成
        Dim cmdTextSb As New StringBuilder

        '****************
        '* TABLE項目(メイン)
        '****************
        cmdTextSb.AppendLine(" FROM ")
        cmdTextSb.AppendLine("      t_seikyuusyo_sime_date_rireki SR WITH (READCOMMITTED)")

        '***********************************************************************
        '* 請求書締め日履歴テーブル：履歴NOの最大値
        '***********************************************************************
        cmdTextSb.AppendLine("            LEFT OUTER JOIN  ")
        cmdTextSb.AppendLine("                (SELECT  ")
        cmdTextSb.AppendLine("                      MAX(SS.rireki_no) AS rireki_no  ")
        cmdTextSb.AppendLine("                    , SS.seikyuu_saki_cd  ")
        cmdTextSb.AppendLine("                    , SS.seikyuu_saki_brc  ")
        cmdTextSb.AppendLine("                    , SS.seikyuu_saki_kbn  ")
        cmdTextSb.AppendLine("                    , SS.seikyuusyo_hak_nengetu  ")
        cmdTextSb.AppendLine("                 FROM  ")
        cmdTextSb.AppendLine("                     (SELECT  ")
        cmdTextSb.AppendLine("                           *  ")
        cmdTextSb.AppendLine("                      FROM  ")
        cmdTextSb.AppendLine("                           t_seikyuusyo_sime_date_rireki  ")
        cmdTextSb.AppendLine("                      WHERE  ")
        cmdTextSb.AppendLine("                           torikesi = 0  ")
        cmdTextSb.AppendLine("                     ) SS  ")
        cmdTextSb.AppendLine("                 GROUP BY  ")
        cmdTextSb.AppendLine("                      SS.seikyuu_saki_cd  ")
        cmdTextSb.AppendLine("                    , SS.seikyuu_saki_brc  ")
        cmdTextSb.AppendLine("                    , SS.seikyuu_saki_kbn  ")
        cmdTextSb.AppendLine("                    , SS.seikyuusyo_hak_nengetu  ")
        cmdTextSb.AppendLine("                ) DR  ")
        cmdTextSb.AppendLine("              ON SR.rireki_no = DR.rireki_no ")
        cmdTextSb.AppendLine("             AND SR.seikyuu_saki_cd = DR.seikyuu_saki_cd ")
        cmdTextSb.AppendLine("             AND SR.seikyuu_saki_brc = DR.seikyuu_saki_brc  ")
        cmdTextSb.AppendLine("             AND SR.seikyuu_saki_kbn = DR.seikyuu_saki_kbn  ")
        cmdTextSb.AppendLine("             AND SR.seikyuusyo_hak_nengetu = DR.seikyuusyo_hak_nengetu  ")

        Return cmdTextSb.ToString
    End Function

    ''' <summary>
    ''' 請求書締め日履歴データ取得用のTABLEクエリを取得
    ''' </summary>
    ''' <returns>請求書締め日履歴データ取得用のTABLEクエリを取得</returns>
    ''' <remarks></remarks>
    Private Function GetSearchSeikyuuSimeDateRirekiSqlTable() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSearchSeikyuuSimeDateRirekiSqlTable")

        'SQL文の生成
        Dim cmdTextSb As New StringBuilder
        'SELECT句
        Dim strCmnTable As String = Me.GetCmnSqlTable()

        '****************
        '* TABLE項目(メイン)
        '****************
        cmdTextSb.Append(strCmnTable)

        '***********************************************************************
        ' 請求先情報取得ビュー：請求先名カナ(外部結合)
        '***********************************************************************
        cmdTextSb.AppendLine("           LEFT OUTER JOIN v_seikyuu_saki_info VIW WITH (READCOMMITTED)")
        cmdTextSb.AppendLine("             ON SR.seikyuu_saki_cd = VIW.seikyuu_saki_cd ")
        cmdTextSb.AppendLine("            AND SR.seikyuu_saki_brc = VIW.seikyuu_saki_brc ")
        cmdTextSb.AppendLine("            AND SR.seikyuu_saki_kbn = VIW.seikyuu_saki_kbn ")

        '***********************************************************************
        ' 請求鑑テーブル：今回ご請求金額(外部結合)
        '***********************************************************************
        cmdTextSb.AppendLine("           LEFT OUTER JOIN t_seikyuu_kagami SK ")
        cmdTextSb.AppendLine("             ON SR.seikyuusyo_no = SK.seikyuusyo_no ")

        Return cmdTextSb.ToString
    End Function

#End Region

#Region "WHERE句"
    ''' <summary>
    ''' 請求書締め日履歴データ取得用の共通WHEREクエリを取得
    ''' </summary>
    ''' <param name="keyRec">請求書締め日履歴データKEYレコードクラス</param>
    ''' <returns>請求書締め日履歴データ取得用の共通WHEREクエリを取得</returns>
    ''' <remarks></remarks>
    Private Function GetCmnSqlWhere(ByVal keyRec As SeikyuuSimeDateRirekiKeyRecord) As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetCmnSqlWhere", keyRec)

        'SQL文の生成
        Dim cmdTextSb As New StringBuilder

        cmdTextSb.AppendLine(" WHERE ")
        cmdTextSb.AppendLine("      1= 1 ")

        '***********************************************************************
        ' 請求先コード
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.SeikyuuSakiCd) Then
            cmdTextSb.AppendLine("  AND SR.seikyuu_saki_cd = " & DBprmSeikyuuSakiCd)
        End If

        '***********************************************************************
        ' 請求先枝番
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.SeikyuuSakiBrc) Then
            cmdTextSb.AppendLine("  AND SR.seikyuu_saki_brc = " & DBprmSeikyuuSakiBrc)
        End If

        '***********************************************************************
        ' 請求先区分
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.SeikyuuSakiKbn) Then
            cmdTextSb.AppendLine("  AND SR.seikyuu_saki_kbn = " & DBprmSeikyuuSakiKbn)
        End If

        '***********************************************************************
        ' 請求先名カナ
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.SeikyuuSakiMeiKana) Then
            cmdTextSb.AppendLine("  AND VIW.seikyuu_saki_kana like " & DBprmSeikyuuSakiMeiKana)
        End If

        '***********************************************************************
        ' 請求書発行日
        '***********************************************************************
        If keyRec.SeikyuusyoHakDateFrom <> DateTime.MinValue Or _
            keyRec.SeikyuusyoHakDateTo <> DateTime.MinValue Then

            cmdTextSb.Append("  AND")

            If keyRec.SeikyuusyoHakDateFrom <> DateTime.MinValue And _
                keyRec.SeikyuusyoHakDateTo <> DateTime.MinValue Then
                '両方指定ありはBETWEEN
                cmdTextSb.AppendLine(" CONVERT(VARCHAR, DATEADD(DAY, + CAST(SR.seikyuu_sime_date As int) - 1, SR.seikyuusyo_hak_nengetu), 111) ")
                cmdTextSb.AppendLine("      BETWEEN " & DBprmSeikyuusyoHakDateFrom & " AND " & DBprmSeikyuusyoHakDateTo)
            Else
                If keyRec.SeikyuusyoHakDateFrom <> DateTime.MinValue Then
                    'Fromのみ
                    cmdTextSb.AppendLine(" CONVERT(VARCHAR, DATEADD(DAY, + CAST(SR.seikyuu_sime_date As int) - 1, SR.seikyuusyo_hak_nengetu) ,111) >= " & DBprmSeikyuusyoHakDateFrom)
                Else
                    'Toのみ
                    cmdTextSb.AppendLine(" CONVERT(VARCHAR, DATEADD(DAY, + CAST(SR.seikyuu_sime_date As int) - 1, SR.seikyuusyo_hak_nengetu) ,111) <= " & DBprmSeikyuusyoHakDateTo)
                End If
            End If
        End If

        '***********************************************************************
        ' 請求書NO
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.SeikyuusyoNoFrom) Or _
            Not String.IsNullOrEmpty(keyRec.SeikyuusyoNoTo) Then
            cmdTextSb.Append("  AND")

            If Not (keyRec.SeikyuusyoNoFrom Is String.Empty) And _
                Not (keyRec.SeikyuusyoNoTo Is String.Empty) Then
                ' 両方指定有りはBETWEEN
                cmdTextSb.Append(" SR.seikyuusyo_no BETWEEN " & DBprmSeikyuusyoNoFrom)
                cmdTextSb.Append(" AND ")
                cmdTextSb.AppendLine(DBprmSeikyuusyoNoTo)
            Else
                If Not keyRec.SeikyuusyoNoFrom Is String.Empty Then
                    ' 番号Fromのみ
                    cmdTextSb.AppendLine(" SR.seikyuusyo_no >= " & DBprmSeikyuusyoNoFrom)
                Else
                    ' 番号Toのみ
                    cmdTextSb.AppendLine(" SR.seikyuusyo_no <= " & DBprmSeikyuusyoNoTo)
                End If
            End If
        End If

        '***********************************************************************
        ' 取消
        '***********************************************************************
        If keyRec.Torikesi = 0 Then
            cmdTextSb.AppendLine("  AND SR.torikesi = 0 ")
        End If

        '***********************************************************************
        '最新伝票のみ表示
        '***********************************************************************
        If keyRec.NewRirekiDisp = 0 Then
            cmdTextSb.AppendLine("  AND DR.rireki_no IS NOT NULL ")
        End If

        Return cmdTextSb.ToString
    End Function
#End Region

#Region "ORDER BY句"
    ''' <summary>
    ''' 請求書締め日履歴データ取得用の共通ORDER BYクエリを取得
    ''' </summary>
    ''' <returns>請求書締め日履歴データ取得用の共通ORDER BYクエリを取得</returns>
    ''' <remarks></remarks>
    Private Function GetCmnSqlOrderBy() As String
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetCmnSqlOrderBy")

        'SQL文の生成
        Dim cmdTextSb As New StringBuilder

        cmdTextSb.AppendLine(" ORDER BY ")
        cmdTextSb.AppendLine("      SR.seikyuu_saki_kbn ")
        cmdTextSb.AppendLine("    , SR.seikyuu_saki_cd ")
        cmdTextSb.AppendLine("    , SR.seikyuu_saki_brc ")
        cmdTextSb.AppendLine("    , SR.seikyuusyo_hak_nengetu ")
        cmdTextSb.AppendLine("    , SR.rireki_no ")

        Return cmdTextSb.ToString
    End Function
#End Region

#Region "SQLパラメータ"

#Region "パラメータ定義"
    '請求先コード
    Private Const DBprmSeikyuuSakiCd As String = "@SEIKYUUSAKI_CD"
    '請求先枝番
    Private Const DBprmSeikyuuSakiBrc As String = "@SEIKYUUSAKI_BRC"
    '請求先区分
    Private Const DBprmSeikyuuSakiKbn As String = "@SEIKYUUSAKI_KBN"
    '請求先名カナ
    Private Const DBprmSeikyuuSakiMeiKana As String = "@SEIKYUUSAKIMEI_KANA"
    '請求書発行日_FROM,TO
    Private Const DBprmSeikyuusyoHakDateFrom As String = "@SEIKYUUSYO_HAK_DATE_FROM"
    Private Const DBprmSeikyuusyoHakDateTo As String = "@SEIKYUUSYO_HAK_DATE_TO"
    '請求書NO_FROM,TO
    Private Const DBprmSeikyuusyoNoFrom As String = "@SEIKYUUSYO_NO_FROM"
    Private Const DBprmSeikyuusyoNoTo As String = "@SEIKYUUSYO_NO_TO"

#Region "更新用"
    '請求書発行年月
    Private Const DBprmSeikyuusyoHakNengetu As String = "@SEIKYUU_NENGETU"
    '請求締め日
    Private Const DBprmSeikyuuSimeDate As String = "@SEIKYUU_SIME_DATE"
    '更新日時
    Private Const DBprmAddDateTime As String = "@UPD_DATETIME"
    '更新者ID
    Private Const DBprmAddLoginUserId As String = "@UPD_LOGIN_USER_ID"
#End Region

#End Region

    ''' <summary>
    ''' 請求書締め日履歴データ取得用の共通SQLパラメータを取得
    ''' </summary>
    ''' <param name="keyRec">請求書締め日履歴データKEYレコードクラス</param>
    ''' <returns>請求書締め日履歴データ取得用の共通SQLクエリ</returns>
    ''' <remarks></remarks>
    Private Function GetSeikyuuSimeDateRirekiSqlCmnParams(ByVal keyRec As SeikyuuSimeDateRirekiKeyRecord) As SqlParameter()
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSeikyuuSimeDateRirekiSqlCmnParams", keyRec)

        '請求先
        Dim objSeikyuuSakiKbn As Object = IIf(keyRec.SeikyuuSakiKbn = String.Empty, DBNull.Value, keyRec.SeikyuuSakiKbn)
        Dim objSeikyuuSakiCd As Object = IIf(keyRec.SeikyuuSakiCd = String.Empty, DBNull.Value, keyRec.SeikyuuSakiCd)
        Dim objSeikyuuSakiBrc As Object = IIf(keyRec.SeikyuuSakiBrc = String.Empty, DBNull.Value, keyRec.SeikyuuSakiBrc)
        '請求先名カナ
        Dim objSeikyuuSakiMeiKana As Object = IIf(keyRec.SeikyuuSakiMeiKana = String.Empty, DBNull.Value, keyRec.SeikyuuSakiMeiKana)
        '請求書発行日_FROM,TO
        Dim objSeikyuusyoHakDateFrom As Object = IIf(keyRec.SeikyuusyoHakDateFrom = DateTime.MinValue, DBNull.Value, keyRec.SeikyuusyoHakDateFrom)
        Dim objSeikyuusyoHakDateTo As Object = IIf(keyRec.SeikyuusyoHakDateTo = DateTime.MinValue, DBNull.Value, keyRec.SeikyuusyoHakDateTo)
        '請求書NO_FROM,TO
        Dim objSeikyuusyoNoFrom As Object = IIf(keyRec.SeikyuusyoNoFrom = String.Empty, DBNull.Value, keyRec.SeikyuusyoNoFrom)
        Dim objSeikyuusyoNoTo As Object = IIf(keyRec.SeikyuusyoNoTo = String.Empty, DBNull.Value, keyRec.SeikyuusyoNoTo)

        'パラメータへ設定
        Dim cmdParams() As SqlParameter = { _
                SQLHelper.MakeParam(DBprmSeikyuuSakiCd, SqlDbType.VarChar, 5, objSeikyuuSakiCd), _
                SQLHelper.MakeParam(DBprmSeikyuuSakiBrc, SqlDbType.VarChar, 2, objSeikyuuSakiBrc), _
                SQLHelper.MakeParam(DBprmSeikyuuSakiKbn, SqlDbType.Char, 1, objSeikyuuSakiKbn), _
                SQLHelper.MakeParam(DBprmSeikyuuSakiMeiKana, SqlDbType.VarChar, 40, objSeikyuuSakiMeiKana & Chr(37)), _
                SQLHelper.MakeParam(DBprmSeikyuusyoHakDateFrom, SqlDbType.DateTime, 16, objSeikyuusyoHakDateFrom), _
                SQLHelper.MakeParam(DBprmSeikyuusyoHakDateTo, SqlDbType.DateTime, 16, objSeikyuusyoHakDateTo), _
                SQLHelper.MakeParam(DBprmSeikyuusyoNoFrom, SqlDbType.VarChar, 15, objSeikyuusyoNoFrom), _
                SQLHelper.MakeParam(DBprmSeikyuusyoNoTo, SqlDbType.VarChar, 15, objSeikyuusyoNoTo) _
        }

        Return cmdParams
    End Function

#End Region

#End Region

End Class

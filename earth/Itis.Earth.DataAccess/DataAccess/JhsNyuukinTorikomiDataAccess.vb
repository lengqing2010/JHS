Imports System.text
Imports System.Data.SqlClient

Public Class JhsNyuukinTorikomiDataAccess
    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName
    Private connStr As String = ConnectionManager.Instance.ConnectionString
    Private cmnDtAcc As New CmnDataAccess
    ''' <summary>
    ''' 重複チェック処理
    ''' </summary>
    ''' <param name="strEdiInfo">EDI情報作成日</param>
    ''' <returns>True：重複アリ／False：重複ナシ</returns>
    ''' <remarks>取り込みファイルのEDI情報作成日が、既に入金ファイル取り込みテーブルに登録されているかチェックする</remarks>
    Public Function chkTyoufuku(ByVal strEdiInfo As String) As Boolean
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".chkTyoufuku" _
                                                    , strEdiInfo)
        Dim blnResult As Boolean = False
        Dim tyoufukuChkTable As JhsNyuukinTorikomiDataSet.TyoufukuChkDataTable
        Dim tyoufukuChkRow As JhsNyuukinTorikomiDataSet.TyoufukuChkRow
        Dim torikomiDataSet As New JhsNyuukinTorikomiDataSet
        Dim commandTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        commandTextSb.Append(" SELECT ")
        commandTextSb.Append("        count(*) data_cnt")
        commandTextSb.Append("   FROM t_upload_kanri ")
        commandTextSb.Append("  WHERE edi_jouhou_sakusei_date = @EDIINFO ")
        'パラメータへ設定
        cmdParams = New SqlParameter() { _
                            SQLHelper.MakeParam("@EDIINFO", SqlDbType.VarChar, 40, strEdiInfo)}
        '検索実行
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            torikomiDataSet, torikomiDataSet.TyoufukuChk.TableName, cmdParams)
        tyoufukuChkTable = torikomiDataSet.TyoufukuChk
        tyoufukuChkRow = tyoufukuChkTable.Rows(0)
        If tyoufukuChkRow.data_cnt > 0 Then
            blnResult = True
        End If

        Return blnResult
    End Function

    ''' <summary>
    ''' 入金ファイル取込テーブルの情報を取得します
    ''' </summary>
    ''' <param name="keyRec">区分</param>
    ''' <returns>入金ファイル取込データテーブル</returns>
    ''' <remarks>検索条件をKEYにして取得</remarks>
    Public Function getNkFileTorikomiTable(ByVal keyRec As NyuukinFileTorikomiKeyRecord) As DataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getNkFileTorikomiTable", keyRec)

        Dim cmdTextSb As New StringBuilder()

        ' パラメータ
        Const strPrmNyuukinTorikomiNoFrom As String = "@NYUUKIN_TORIKOMI_NO_FROM"
        Const strPrmNyuukinTorikomiNoTo As String = "@NYUUKIN_TORIKOMI_NO_TO"
        Const strPrmTorikomiDenpyouNoFrom As String = "@TORIKOMI_DENPYOU_NO_FROM"
        Const strPrmTorikomiDenpyouNoTo As String = "@TORIKOMI_DENPYOU_NO_TO"
        Const strPrmNyuukinDateFrom As String = "@NYUUKIN_DATE_FROM"
        Const strPrmNyuukinDateTo As String = "@NYUUKIN_DATE_TO"
        Const strPrmEdiJouhouSakuseiDate As String = "@EDI_JOUHOU_SAKUSEI_DATE"
        Const strPrmSeikyuuSakiKbn As String = "@SEIKYUUSAKIKBN"
        Const strPrmSeikyuuSakiCd As String = "@SEIKYUUSAKICD"
        Const strPrmSeikyuuSakiBrc As String = "@SEIKYUUSAKIBRC"

        'SQL生成
        cmdTextSb.Append(" SELECT ")
        cmdTextSb.Append(" 	    nyuukin_torikomi_unique_no ")
        cmdTextSb.Append(" 	    ,edi_jouhou_sakusei_date ")
        cmdTextSb.Append(" 	    ,torikesi ")
        cmdTextSb.Append(" 	    ,nyuukin_date ")
        cmdTextSb.Append(" 	    ,torikomi_denpyou_no ")
        cmdTextSb.Append(" 	    ,seikyuu_saki_cd ")
        cmdTextSb.Append(" 	    ,seikyuu_saki_brc ")
        cmdTextSb.Append(" 	    ,seikyuu_saki_kbn ")
        cmdTextSb.Append(" 	    ,seikyuu_saki_mei ")
        cmdTextSb.Append(" 	    ,syougou_kouza_no ")
        cmdTextSb.Append(" 	    ,genkin ")
        cmdTextSb.Append(" 	    ,kogitte ")
        cmdTextSb.Append(" 	    ,furikomi ")
        cmdTextSb.Append(" 	    ,tegata ")
        cmdTextSb.Append(" 	    ,sousai ")
        cmdTextSb.Append(" 	    ,nebiki ")
        cmdTextSb.Append(" 	    ,sonota ")
        cmdTextSb.Append(" 	    ,kyouryoku_kaihi ")
        cmdTextSb.Append(" 	    ,kouza_furikae ")
        cmdTextSb.Append(" 	    ,furikomi_tesuuryou ")
        cmdTextSb.Append(" 	    ,tegata_kijitu ")
        cmdTextSb.Append(" 	    ,tegata_no ")
        cmdTextSb.Append(" 	    ,tekiyou_mei ")
        cmdTextSb.Append(" 	    ,add_login_user_id ")
        cmdTextSb.Append(" 	    ,add_datetime ")
        cmdTextSb.Append(" 	    ,upd_login_user_id ")
        cmdTextSb.Append(" 	    ,upd_datetime ")
        cmdTextSb.Append("  FROM t_nyuukin_file_torikomi ")
        cmdTextSb.Append("  WHERE 1 = 1 ")

        '***********************************************************************
        '入金取込ユニークNO
        '***********************************************************************
        If Not (keyRec.NyuukinTorikomiNoFrom = Integer.MinValue) Or Not (keyRec.NyuukinTorikomiNoTo = Integer.MinValue) Then

            cmdTextSb.Append(" AND ")

            If Not (keyRec.NyuukinTorikomiNoFrom = Integer.MinValue) And Not (keyRec.NyuukinTorikomiNoTo = Integer.MinValue) Then
                ' 両方指定有りはBETWEEN
                cmdTextSb.Append(" nyuukin_torikomi_unique_no BETWEEN " & strPrmNyuukinTorikomiNoFrom)
                cmdTextSb.Append(" AND ")
                cmdTextSb.Append(strPrmNyuukinTorikomiNoTo)
            Else
                If Not keyRec.NyuukinTorikomiNoFrom = Integer.MinValue Then
                    ' Fromのみ
                    cmdTextSb.Append(" nyuukin_torikomi_unique_no >= " & strPrmNyuukinTorikomiNoFrom)
                Else
                    ' Toのみ
                    cmdTextSb.Append(" nyuukin_torikomi_unique_no <= " & strPrmNyuukinTorikomiNoTo)
                End If
            End If

        End If

        '***********************************************************************
        '伝票番号
        '***********************************************************************
        If Not (keyRec.TorikomiDenpyouNoFrom Is String.Empty) Or Not (keyRec.TorikomiDenpyouNoTo Is String.Empty) Then

            cmdTextSb.Append(" AND ")

            If Not (keyRec.TorikomiDenpyouNoFrom Is String.Empty) And Not (keyRec.TorikomiDenpyouNoTo Is String.Empty) Then
                ' 両方指定有りはBETWEEN
                cmdTextSb.Append(" torikomi_denpyou_no BETWEEN " & strPrmTorikomiDenpyouNoFrom)
                cmdTextSb.Append(" AND ")
                cmdTextSb.Append(strPrmTorikomiDenpyouNoTo)
            Else
                If Not keyRec.TorikomiDenpyouNoFrom Is String.Empty Then
                    ' Fromのみ
                    cmdTextSb.Append(" torikomi_denpyou_no >= " & strPrmTorikomiDenpyouNoFrom)
                Else
                    ' Toのみ
                    cmdTextSb.Append(" torikomi_denpyou_no <= " & strPrmTorikomiDenpyouNoTo)
                End If
            End If
        End If

        '***********************************************************************
        ' 入金日
        '***********************************************************************
        '日付が一つでも設定されている場合、条件を作成
        If keyRec.NyuukinDateFrom <> DateTime.MinValue Or _
            keyRec.NyuukinDateTo <> DateTime.MinValue Then

            cmdTextSb.Append(" AND ")

            If keyRec.NyuukinDateFrom <> DateTime.MinValue And _
                keyRec.NyuukinDateTo <> DateTime.MinValue Then
                '両方指定ありはBETWEEN
                cmdTextSb.Append(" CONVERT(VARCHAR, nyuukin_date ,111) ")
                cmdTextSb.Append(" BETWEEN " & strPrmNyuukinDateFrom)
                cmdTextSb.Append(" AND " & strPrmNyuukinDateTo)
            Else
                If keyRec.NyuukinDateFrom <> DateTime.MinValue Then
                    'Fromのみ
                    cmdTextSb.Append(" CONVERT(VARCHAR, nyuukin_date ,111) >= " & strPrmNyuukinDateFrom)
                Else
                    'Toのみ
                    cmdTextSb.Append(" CONVERT(VARCHAR, nyuukin_date ,111) <= " & strPrmNyuukinDateTo)
                End If
            End If
        End If

        '***********************************************************************
        'EDI情報作成日
        '***********************************************************************
        If Not (keyRec.EdiJouhouSakuseiDate Is String.Empty) Then
            cmdTextSb.Append(" AND edi_jouhou_sakusei_date LIKE " & strPrmEdiJouhouSakuseiDate)
        End If

        '***********************************************************************
        ' 請求先区分
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.SeikyuuSakiKbn) Then
            cmdTextSb.Append(" AND seikyuu_saki_kbn = " & strPrmSeikyuuSakiKbn)
        End If

        '***********************************************************************
        ' 請求先コード
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.SeikyuuSakiCd) Then
            cmdTextSb.Append(" AND seikyuu_saki_cd = " & strPrmSeikyuuSakiCd)
        End If

        '***********************************************************************
        ' 請求先枝番
        '***********************************************************************
        If Not String.IsNullOrEmpty(keyRec.SeikyuuSakiBrc) Then
            cmdTextSb.Append(" AND seikyuu_saki_brc = " & strPrmSeikyuuSakiBrc)
        End If

        '***********************************************************************
        ' 取消
        '***********************************************************************
        If keyRec.Torikesi = 0 Then
            cmdTextSb.Append("  AND torikesi = 0 ")
        End If

        '***********************************************************************
        '表示順序の付与（入金日、入金取込ユニークNO）
        '***********************************************************************
        cmdTextSb.Append("  ORDER BY ")
        cmdTextSb.Append("      nyuukin_date")
        cmdTextSb.Append("      ,nyuukin_torikomi_unique_no ")

        'パラメータへ設定
        Dim cmdParams() As SqlParameter = _
            {SQLHelper.MakeParam(strPrmNyuukinTorikomiNoFrom, SqlDbType.Int, 4, keyRec.NyuukinTorikomiNoFrom), _
             SQLHelper.MakeParam(strPrmNyuukinTorikomiNoTo, SqlDbType.Int, 4, keyRec.NyuukinTorikomiNoTo), _
             SQLHelper.MakeParam(strPrmTorikomiDenpyouNoFrom, SqlDbType.Char, 6, keyRec.TorikomiDenpyouNoFrom), _
             SQLHelper.MakeParam(strPrmTorikomiDenpyouNoTo, SqlDbType.Char, 6, keyRec.TorikomiDenpyouNoTo), _
             SQLHelper.MakeParam(strPrmNyuukinDateFrom, SqlDbType.DateTime, 16, IIf(keyRec.NyuukinDateFrom = DateTime.MinValue, DBNull.Value, keyRec.NyuukinDateFrom)), _
             SQLHelper.MakeParam(strPrmNyuukinDateTo, SqlDbType.DateTime, 16, IIf(keyRec.NyuukinDateTo = DateTime.MinValue, DBNull.Value, keyRec.NyuukinDateTo)), _
             SQLHelper.MakeParam(strPrmEdiJouhouSakuseiDate, SqlDbType.VarChar, 40, IIf(keyRec.EdiJouhouSakuseiDate = String.Empty, DBNull.Value, keyRec.EdiJouhouSakuseiDate & Chr(37))), _
             SQLHelper.MakeParam(strPrmSeikyuuSakiKbn, SqlDbType.Char, 1, keyRec.SeikyuuSakiKbn), _
             SQLHelper.MakeParam(strPrmSeikyuuSakiCd, SqlDbType.VarChar, 5, keyRec.SeikyuuSakiCd), _
             SQLHelper.MakeParam(strPrmSeikyuuSakiBrc, SqlDbType.VarChar, 2, keyRec.SeikyuuSakiBrc)}

        'データ取得＆返却
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)
    End Function

    ''' <summary>
    ''' 入金ファイル取込テーブルの情報をPKで取得します
    ''' </summary>
    ''' <param name="strNyuukinTorikomiNo">入金取込NO</param>
    ''' <returns>入金ファイル取込データテーブル</returns>
    ''' <remarks>検索条件をKEYにして取得</remarks>
    Public Function getNkFileTorikomiTable(ByVal strNyuukinTorikomiNo As String) As DataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getNkFileTorikomiTable", strNyuukinTorikomiNo)

        Dim cmdTextSb As New StringBuilder()
        Dim NkFileTorikomiDataSet As New JhsNyuukinTorikomiDataSet
        Dim NkFileTorikomiTable As New DataTable

        If strNyuukinTorikomiNo = String.Empty Then
            Return NkFileTorikomiTable
            Exit Function
        End If

        ' パラメータ
        Const strPrmNyuukinTorikomiNo As String = "@NYUUKIN_TORIKOMI_NO"

        'SQL生成
        cmdTextSb.Append(" SELECT ")
        cmdTextSb.Append(" 	    nyuukin_torikomi_unique_no ")
        cmdTextSb.Append(" 	    ,edi_jouhou_sakusei_date ")
        cmdTextSb.Append(" 	    ,torikesi ")
        cmdTextSb.Append(" 	    ,nyuukin_date ")
        cmdTextSb.Append(" 	    ,torikomi_denpyou_no ")
        cmdTextSb.Append(" 	    ,seikyuu_saki_cd ")
        cmdTextSb.Append(" 	    ,seikyuu_saki_brc ")
        cmdTextSb.Append(" 	    ,seikyuu_saki_kbn ")
        cmdTextSb.Append(" 	    ,seikyuu_saki_mei ")
        cmdTextSb.Append(" 	    ,syougou_kouza_no ")
        cmdTextSb.Append(" 	    ,genkin ")
        cmdTextSb.Append(" 	    ,kogitte ")
        cmdTextSb.Append(" 	    ,furikomi ")
        cmdTextSb.Append(" 	    ,tegata ")
        cmdTextSb.Append(" 	    ,sousai ")
        cmdTextSb.Append(" 	    ,nebiki ")
        cmdTextSb.Append(" 	    ,sonota ")
        cmdTextSb.Append(" 	    ,kyouryoku_kaihi ")
        cmdTextSb.Append(" 	    ,kouza_furikae ")
        cmdTextSb.Append(" 	    ,furikomi_tesuuryou ")
        cmdTextSb.Append(" 	    ,tegata_kijitu ")
        cmdTextSb.Append(" 	    ,tegata_no ")
        cmdTextSb.Append(" 	    ,tekiyou_mei ")
        cmdTextSb.Append(" 	    ,add_login_user_id ")
        cmdTextSb.Append(" 	    ,add_datetime ")
        cmdTextSb.Append(" 	    ,upd_login_user_id ")
        cmdTextSb.Append(" 	    ,upd_datetime ")
        cmdTextSb.Append("  FROM t_nyuukin_file_torikomi ")
        cmdTextSb.Append("  WHERE nyuukin_torikomi_unique_no =  " & strPrmNyuukinTorikomiNo)

        'パラメータへ設定
        Dim cmdParams() As SqlParameter = _
            {SQLHelper.MakeParam(strPrmNyuukinTorikomiNo, SqlDbType.Int, 4, strNyuukinTorikomiNo)}

        'データ取得＆返却
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)

    End Function

    ''' <summary>
    ''' 入金ファイル取込テーブルの排他チェックを行います
    ''' </summary>
    ''' <param name="sql">SQL</param>
    ''' <param name="paramList">パラメータのリスト</param>
    ''' <returns>入金ファイル取込データ排他レコード（更新日付相違有りの場合取得されます）</returns>
    ''' <remarks></remarks>
    Public Function CheckHaita(ByVal sql As String, _
                               ByVal paramList As List(Of ParamRecord)) As JhsNyuukinTorikomiDataSet.NkFileHaitaTableDataTable

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".CheckHaita", _
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

        ' データの取得
        Dim NkDataSet As New JhsNyuukinTorikomiDataSet()

        SQLHelper.FillDataset(connStr, CommandType.Text, sql, _
            NkDataSet, NkDataSet.NkFileHaitaTable.TableName, cmdParams)

        Dim haitaTable As JhsNyuukinTorikomiDataSet.NkFileHaitaTableDataTable = NkDataSet.NkFileHaitaTable

        Return haitaTable

    End Function

End Class

Imports System.text
Imports System.Data.SqlClient
''' <summary>
''' 各種連携管理テーブルに関する処理を行うロジッククラスです
''' </summary>
''' <remarks></remarks>
Public Class RenkeiKanriDataAccess
    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName
    Dim connStr As String = ConnectionManager.Instance.ConnectionString

#Region "SQLパラメータ"
    Private Const paramKubun As String = "@KUBUN"
    Private Const paramHosyousyoNo As String = "@HOSYOUSYONO"
    Private Const paramBunruiCd As String = "@BUNRUICD"
    Private Const paramGamenHyoujiNo As String = "@GAMENHYOUJINO"
    Private Const paramMiseCd As String = "@MISECD"
    Private Const paramNyuuryokuDate As String = "@NYUURYOKUDATE"
    Private Const paramNyuuryokuDateNo As String = "@NYUURYOKUDATENO"
    Private Const paramUpdDatetimeRireki As String = "@UPDDATETIMERIREKI"
    Private Const paramUpdKoumoku As String = "@UPDKOUMOKU"
    Private Const paramRenkeiSijiCd As String = "@RENKEISIJICD"
    Private Const paramSousinJykyCd As String = "@SOUSINJYKYCD"
    Private Const paramUpdLoginUserId As String = "@UPDLOGINUSERID"
    Private Const paramUpdDateTime As String = "@UPDDATETIME"
#End Region

#Region "テンポラリーテーブル関連"
    Private tmpTeibetuRenkeiTable As String
    Private Const tmpTeibetuRenkeiKey As String = "TEMP_TEIBETU_RENKEI_WORK_PKC"

    ''' <summary>
    ''' テンポラリーテーブル名(邸別請求連携テーブル用)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TmpTableTeibetuRenkei() As String
        Get
            Return tmpTeibetuRenkeiTable
        End Get
        Set(ByVal value As String)
            tmpTeibetuRenkeiTable = value
        End Set
    End Property

#End Region

#Region "邸別請求連携"
    ''' <summary>
    ''' 更新対象の邸別請求連携管理テーブルの内容を確認します
    ''' </summary>
    ''' <param name="renkeiRec">邸別請求連携管理レコード</param>
    ''' <returns>送信状況コードと連携指示コードを格納したデータテーブル</returns>
    ''' <remarks>邸別請求連携管理レコードに登録／更新対象のKEYを設定してから当メソッドを使用する</remarks>
    Public Function SelectTeibetuSeikyuuRenkeiData(ByVal renkeiRec As TeibetuSeikyuuRenkeiRecord) As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SelectTeibetuSeikyuuRenkeiData", _
                                                    renkeiRec)
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlParameter
        Dim renkeiDataSet As New TeibetuRenkeiDataSet()
        Dim renkeiTable As TeibetuRenkeiDataSet.RenkeiTableDataTable

        ' 邸別請求連携テーブルの存在チェック
        cmdTextSb.Append(" SELECT sousin_jyky_cd ")
        cmdTextSb.Append("       ,renkei_siji_cd ")
        cmdTextSb.Append("   FROM t_teibetu_seikyuu_renkei WITH(UPDLOCK) ")
        cmdTextSb.Append("  WHERE kbn               = " & paramKubun)
        cmdTextSb.Append("    AND hosyousyo_no      = " & paramHosyousyoNo)
        cmdTextSb.Append("    AND SUBSTRING(bunrui_cd,1,2) = SUBSTRING(" & paramBunruiCd & ",1,2)")
        cmdTextSb.Append("    AND gamen_hyouji_no   = " & paramGamenHyoujiNo)

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam(paramKubun, SqlDbType.Char, 1, renkeiRec.Kbn), _
            SQLHelper.MakeParam(paramHosyousyoNo, SqlDbType.VarChar, 10, renkeiRec.HosyousyoNo), _
            SQLHelper.MakeParam(paramBunruiCd, SqlDbType.VarChar, 3, renkeiRec.BunruiCd), _
            SQLHelper.MakeParam(paramGamenHyoujiNo, SqlDbType.Int, 4, renkeiRec.GamenHyoujiNo)}

        ' データの取得
        SQLHelper.FillDataset(connStr, _
                              CommandType.Text, _
                              cmdTextSb.ToString(), _
                              renkeiDataSet, _
                              renkeiDataSet.RenkeiTable.TableName, _
                              cmdParams)
        renkeiTable = renkeiDataSet.RenkeiTable

        Return renkeiTable
    End Function

    ''' <summary>
    ''' 邸別請求連携テーブルへデータを登録します
    ''' </summary>
    ''' <param name="renkeiRec">邸別請求連携管理レコード</param>
    ''' <returns>登録結果</returns>
    ''' <remarks>邸別請求連携管理レコードへ登録内容を設定してから当メソッドを使用する</remarks>
    Public Function InsertTeibetuSeikyuuRenkeiData(ByVal renkeiRec As TeibetuSeikyuuRenkeiRecord) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".InsertTeibetuSeikyuuRenkeiData", _
                                                    renkeiRec)
        Dim intResult As Integer = 0
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        ' 登録用SQL設定
        cmdTextSb.Append(" INSERT INTO t_teibetu_seikyuu_renkei ( ")
        cmdTextSb.Append("      kbn ")
        cmdTextSb.Append("     ,hosyousyo_no ")
        cmdTextSb.Append("     ,bunrui_cd ")
        cmdTextSb.Append("     ,gamen_hyouji_no ")
        cmdTextSb.Append("     ,renkei_siji_cd ")
        cmdTextSb.Append("     ,sousin_jyky_cd ")
        cmdTextSb.Append("     ,upd_login_user_id ")
        cmdTextSb.Append("     ,upd_datetime ")
        cmdTextSb.Append(" ) VALUES ( ")
        cmdTextSb.Append("      " & paramKubun)
        cmdTextSb.Append("     ," & paramHosyousyoNo)
        cmdTextSb.Append("     ," & paramBunruiCd)
        cmdTextSb.Append("     ," & paramGamenHyoujiNo)
        cmdTextSb.Append("     ," & paramRenkeiSijiCd)
        cmdTextSb.Append("     ," & paramSousinJykyCd)
        cmdTextSb.Append("     ," & paramUpdLoginUserId)
        cmdTextSb.Append("     ," & paramUpdDateTime)
        cmdTextSb.Append(" )")

        cmdParams = New SqlParameter() { _
                SQLHelper.MakeParam(paramKubun, SqlDbType.Char, 1, renkeiRec.Kbn), _
                SQLHelper.MakeParam(paramHosyousyoNo, SqlDbType.VarChar, 10, renkeiRec.HosyousyoNo), _
                SQLHelper.MakeParam(paramBunruiCd, SqlDbType.VarChar, 3, renkeiRec.BunruiCd), _
                SQLHelper.MakeParam(paramGamenHyoujiNo, SqlDbType.Int, 4, renkeiRec.GamenHyoujiNo), _
                SQLHelper.MakeParam(paramRenkeiSijiCd, SqlDbType.Int, 4, renkeiRec.RenkeiSijiCd), _
                SQLHelper.MakeParam(paramSousinJykyCd, SqlDbType.Int, 4, renkeiRec.SousinJykyCd), _
                SQLHelper.MakeParam(paramUpdLoginUserId, SqlDbType.VarChar, 30, renkeiRec.UpdLoginUserId), _
                SQLHelper.MakeParam(paramUpdDateTime, SqlDbType.DateTime, 16, DateTime.Now)}

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    cmdTextSb.ToString(), _
                                    cmdParams)
        Return intResult
    End Function

    ''' <summary>
    ''' 邸別請求連携テーブルのデータを更新します
    ''' </summary>
    ''' <param name="renkeiRec">邸別請求連携管理レコード</param>
    ''' <param name="isAll">True:送信コード,連携指示コードも更新する</param>
    ''' <returns>更新結果</returns>
    ''' <remarks>邸別請求連携管理レコードへ更新対象を絞るKEYと更新内容を設定してから当メソッドを使用する</remarks>
    Public Function UpdateTeibetuSeikyuuRenkeiData(ByVal renkeiRec As TeibetuSeikyuuRenkeiRecord, _
                                                    ByVal isAll As Boolean) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".UpdateTeibetuSeikyuuRenkeiData", _
                                                    renkeiRec, _
                                                    isAll)
        Dim intResult As Integer = 0
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        ' 更新用SQL設定
        cmdTextSb.Append(" UPDATE t_teibetu_seikyuu_renkei ")
        cmdTextSb.Append("    SET ")
        cmdTextSb.Append("        bunrui_cd         = " & paramBunruiCd & ",")
        If isAll = True Then
            cmdTextSb.Append("    renkei_siji_cd    = " & paramRenkeiSijiCd & ",")
            cmdTextSb.Append("    sousin_jyky_cd    = " & paramSousinJykyCd & ",")
        End If
        cmdTextSb.Append("        upd_login_user_id = " & paramUpdLoginUserId & ",")
        cmdTextSb.Append("        upd_datetime      = " & paramUpdDateTime)
        cmdTextSb.Append("  WHERE kbn               = " & paramKubun)
        cmdTextSb.Append("    AND hosyousyo_no      = " & paramHosyousyoNo)
        cmdTextSb.Append("    AND SUBSTRING(bunrui_cd,1,2) = SUBSTRING(" & paramBunruiCd & ",1,2)")
        cmdTextSb.Append("    AND gamen_hyouji_no   = " & paramGamenHyoujiNo)

        cmdParams = New SqlParameter() { _
                SQLHelper.MakeParam(paramKubun, SqlDbType.Char, 1, renkeiRec.Kbn), _
                SQLHelper.MakeParam(paramHosyousyoNo, SqlDbType.VarChar, 10, renkeiRec.HosyousyoNo), _
                SQLHelper.MakeParam(paramBunruiCd, SqlDbType.VarChar, 3, renkeiRec.BunruiCd), _
                SQLHelper.MakeParam(paramGamenHyoujiNo, SqlDbType.Int, 4, renkeiRec.GamenHyoujiNo), _
                SQLHelper.MakeParam(paramRenkeiSijiCd, SqlDbType.Int, 4, renkeiRec.RenkeiSijiCd), _
                SQLHelper.MakeParam(paramSousinJykyCd, SqlDbType.Int, 4, renkeiRec.SousinJykyCd), _
                SQLHelper.MakeParam(paramUpdLoginUserId, SqlDbType.VarChar, 30, renkeiRec.UpdLoginUserId), _
                SQLHelper.MakeParam(paramUpdDateTime, SqlDbType.DateTime, 16, DateTime.Now)}

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    cmdTextSb.ToString(), _
                                    cmdParams)
        Return intResult
    End Function

    ''' <summary>
    ''' 邸別請求連携テーブル用テンポラリーテーブル作成用SQLを作成します(一括更新用)
    ''' </summary>
    ''' <returns>SQLクエリ文字列</returns>
    ''' <remarks></remarks>
    Public Function GetCreateSqlRenkeiTmpForTeibetu() As String
        Dim cmdTextSb As New StringBuilder()

        cmdTextSb.Append(" CREATE TABLE ")
        cmdTextSb.Append("     " & tmpTeibetuRenkeiTable)
        cmdTextSb.Append("         (kbn                    CHAR(1)         NOT NULL ")
        cmdTextSb.Append("         ,hosyousyo_no           VARCHAR(10)     NOT NULL ")
        cmdTextSb.Append("         ,bunrui_cd              VARCHAR(3)      NOT NULL ")
        cmdTextSb.Append("         ,gamen_hyouji_no        INT             NOT NULL ")
        cmdTextSb.Append("         ,renkei_siji_cd         INT             NOT NULL ")
        cmdTextSb.Append("         ,sousin_jyky_cd         INT             NOT NULL ")
        cmdTextSb.Append("         ,sousin_kanry_datetime  DATETIME ")
        cmdTextSb.Append("         ,proc_type				INT				NOT NULL ")
        cmdTextSb.Append("         CONSTRAINT " & tmpTeibetuRenkeiKey)
        cmdTextSb.Append("             PRIMARY KEY (kbn ")
        cmdTextSb.Append("                         ,hosyousyo_no ")
        cmdTextSb.Append("                         ,bunrui_cd ")
        cmdTextSb.Append("                         ,gamen_hyouji_no) ")
        cmdTextSb.Append("         ) ")

        Return cmdTextSb.ToString

    End Function

    ''' <summary>
    ''' テンポラリーテーブルから邸別請求連携テーブルへデータを登録します(一括更新用)
    ''' </summary>
    ''' <param name="cmdTextSb">SQLクエリ</param>
    ''' <param name="cmdParams">SQLパラメーター</param>
    ''' <returns>処理件数</returns>
    ''' <remarks></remarks>
    Public Function ExecRenkeiTeibetuLump(ByVal cmdTextSb As StringBuilder, ByVal cmdParams() As SqlParameter) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".ExecRenkeiTeibetuLump", _
                                                    cmdTextSb, _
                                                    cmdParams)
        Dim intResult As Integer

        cmdTextSb.Append(" INSERT INTO t_teibetu_seikyuu_renkei ")
        cmdTextSb.Append("     SELECT kbn ")
        cmdTextSb.Append("           ,hosyousyo_no ")
        cmdTextSb.Append("           ,bunrui_cd ")
        cmdTextSb.Append("           ,gamen_hyouji_no ")
        cmdTextSb.Append("           ,CASE proc_type ")
        cmdTextSb.Append("              WHEN 1 ")
        cmdTextSb.Append("                THEN '1' ")
        cmdTextSb.Append("              WHEN 0 ")
        cmdTextSb.Append("                THEN '2' ")
        cmdTextSb.Append("              WHEN 9 ")
        cmdTextSb.Append("                THEN '9' ")
        cmdTextSb.Append("              ELSE '1' ")
        cmdTextSb.Append("            END ")
        cmdTextSb.Append("           ,'0' ")
        cmdTextSb.Append("           ,sousin_kanry_datetime ")
        cmdTextSb.Append("           ," & paramUpdLoginUserId)
        cmdTextSb.Append("           ," & paramUpdDateTime)
        cmdTextSb.Append("       FROM " & tmpTeibetuRenkeiTable)
        cmdTextSb.Append("      WHERE renkei_siji_cd = -1 ")

        cmdTextSb.Append(" UPDATE t_teibetu_seikyuu_renkei ")
        cmdTextSb.Append("    SET renkei_siji_cd = (CASE TM.proc_type ")
        cmdTextSb.Append("                            WHEN 1 ")
        cmdTextSb.Append("                              THEN (CASE  ")
        cmdTextSb.Append("                                      WHEN TM.renkei_siji_cd = 1 AND TM.sousin_jyky_cd = 0 ")
        cmdTextSb.Append("                                        THEN '1' ")
        cmdTextSb.Append("                                      WHEN TM.renkei_siji_cd = 9 AND TM.sousin_jyky_cd = 0 ")
        cmdTextSb.Append("                                        THEN '2' ")
        cmdTextSb.Append("                                      ELSE '1' ")
        cmdTextSb.Append("                                    END) ")
        cmdTextSb.Append("                            WHEN 0 ")
        cmdTextSb.Append("                              THEN (CASE ")
        cmdTextSb.Append("                                      WHEN TM.renkei_siji_cd = 1 AND TM.sousin_jyky_cd = 0 ")
        cmdTextSb.Append("                                        THEN '1' ")
        cmdTextSb.Append("                                      ELSE '2' ")
        cmdTextSb.Append("                                    END) ")
        cmdTextSb.Append("                            WHEN 9 ")
        cmdTextSb.Append("                              THEN '9' ")
        cmdTextSb.Append("                            ELSE '1' ")
        cmdTextSb.Append("                          END ")
        cmdTextSb.Append("                         ) ")
        cmdTextSb.Append("        ,sousin_jyky_cd    = 0  ")
        cmdTextSb.Append("        ,bunrui_cd    = TM.bunrui_cd  ")
        cmdTextSb.Append("        ,upd_login_user_id = " & paramUpdLoginUserId)
        cmdTextSb.Append("        ,upd_datetime      = " & paramUpdDateTime)
        cmdTextSb.Append("   FROM t_teibetu_seikyuu_renkei TR ")
        cmdTextSb.Append("       ," & tmpTeibetuRenkeiTable & " TM ")
        cmdTextSb.Append("  WHERE TR.kbn              = TM.kbn ")
        cmdTextSb.Append("    AND TR.hosyousyo_no     = TM.hosyousyo_no ")
        cmdTextSb.Append("    AND SUBSTRING(TR.bunrui_cd,1,2) = SUBSTRING(TM.bunrui_cd,1,2) ")
        cmdTextSb.Append("    AND TR.gamen_hyouji_no  = TM.gamen_hyouji_no ")
        cmdTextSb.Append("    AND TM.renkei_siji_cd <> -1 ")

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    cmdTextSb.ToString(), _
                                    cmdParams)
        Return intResult

    End Function

#End Region

#Region "店別初期請求連携"
    ''' <summary>
    ''' 更新対象の店別初期請求連携管理テーブルの内容を確認します
    ''' </summary>
    ''' <param name="renkeiRec">店別初期請求連携管理レコード</param>
    ''' <returns>送信状況コードと連携指示コードを格納したデータテーブル</returns>
    ''' <remarks>店別初期請求連携管理レコードに登録／更新対象のKEYを設定してから当メソッドを使用する</remarks>
    Public Function SelectTenbetuSyokiSeikyuuRenkeiData(ByVal renkeiRec As TenbetuSyokiSeikyuuRenkeiRecord) As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SelectTenbetuSyokiSeikyuuRenkeiData", _
                                                    renkeiRec)
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlParameter
        Dim renkeiDataSet As New TenbetuSyokiRenkeiDataSet
        Dim renkeiTable As TenbetuSyokiRenkeiDataSet.RenkeiTableDataTable

        '店別初期請求連携テーブルの存在チェック
        cmdTextSb.Append(" SELECT sousin_jyky_cd ")
        cmdTextSb.Append("       ,renkei_siji_cd ")
        cmdTextSb.Append("   FROM t_tenbetu_syoki_seikyuu_renkei WITH(UPDLOCK) ")
        cmdTextSb.Append("  WHERE mise_cd           = " & paramMiseCd)
        cmdTextSb.Append("    AND bunrui_cd         = " & paramBunruiCd)

        cmdParams = New SqlParameter() { _
        SQLHelper.MakeParam(paramMiseCd, SqlDbType.VarChar, 5, renkeiRec.MiseCd), _
        SQLHelper.MakeParam(paramBunruiCd, SqlDbType.VarChar, 3, renkeiRec.BunruiCd)}

        '検索実行
        SQLHelper.FillDataset(connStr, _
                              CommandType.Text, _
                              cmdTextSb.ToString(), _
                              renkeiDataSet, _
                              renkeiDataSet.RenkeiTable.TableName, _
                              cmdParams)
        renkeiTable = renkeiDataSet.RenkeiTable

        Return renkeiTable
    End Function

    ''' <summary>
    ''' 店別初期請求連携テーブルへデータを登録します
    ''' </summary>
    ''' <param name="renkeiRec">店別初期請求連携管理レコード</param>
    ''' <returns>登録結果</returns>
    ''' <remarks>店別初期請求連携管理レコードへ登録内容を設定してから当メソッドを使用する</remarks>
    Public Function InsertTenbetuSyokiSeikyuuRenkeiData(ByVal renkeiRec As TenbetuSyokiSeikyuuRenkeiRecord) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".InsertTenbetuSyokiSeikyuuRenkeiData", _
                                                    renkeiRec)
        Dim intResult As Integer = 0
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        ' 登録用SQL設定
        cmdTextSb.Append(" INSERT INTO t_tenbetu_syoki_seikyuu_renkei ")
        cmdTextSb.Append("     ( mise_cd ")
        cmdTextSb.Append("      ,bunrui_cd ")
        cmdTextSb.Append("      ,renkei_siji_cd ")
        cmdTextSb.Append("      ,sousin_jyky_cd ")
        cmdTextSb.Append("      ,upd_login_user_id ")
        cmdTextSb.Append("      ,upd_datetime ")
        cmdTextSb.Append("     ) VALUES ( ")
        cmdTextSb.Append("      " & paramMiseCd)
        cmdTextSb.Append("     ," & paramBunruiCd)
        cmdTextSb.Append("     ," & paramRenkeiSijiCd)
        cmdTextSb.Append("     ," & paramSousinJykyCd)
        cmdTextSb.Append("     ," & paramUpdLoginUserId)
        cmdTextSb.Append("     ," & paramUpdDateTime)
        cmdTextSb.Append("     ) ")
        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
                SQLHelper.MakeParam(paramMiseCd, SqlDbType.VarChar, 5, renkeiRec.MiseCd), _
                SQLHelper.MakeParam(paramBunruiCd, SqlDbType.VarChar, 3, renkeiRec.BunruiCd), _
                SQLHelper.MakeParam(paramRenkeiSijiCd, SqlDbType.Int, 4, renkeiRec.RenkeiSijiCd), _
                SQLHelper.MakeParam(paramSousinJykyCd, SqlDbType.Int, 4, renkeiRec.SousinJykyCd), _
                SQLHelper.MakeParam(paramUpdLoginUserId, SqlDbType.VarChar, 30, renkeiRec.UpdLoginUserId), _
                SQLHelper.MakeParam(paramUpdDateTime, SqlDbType.DateTime, 16, DateTime.Now)}
        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    cmdTextSb.ToString(), _
                                    cmdParams)
        Return intResult
    End Function

    ''' <summary>
    ''' 店別初期請求連携テーブルのデータを更新します
    ''' </summary>
    ''' <param name="renkeiRec">店別初期請求連携管理レコード</param>
    ''' <param name="isAll">True:送信コード,連携指示コードも更新する</param>
    ''' <returns>更新結果</returns>
    ''' <remarks>店別初期請求連携管理レコードへ更新対象を絞るKEYと更新内容を設定してから当メソッドを使用する</remarks>
    Public Function UpdateTenbetuSyokiSeikyuuRenkeiData(ByVal renkeiRec As TenbetuSyokiSeikyuuRenkeiRecord, _
                                                 ByVal isAll As Boolean) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".UpdateTenbetuSyokiSeikyuuRenkeiData", _
                                                    renkeiRec, _
                                                    isAll)
        Dim intResult As Integer = 0
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        ' 更新用SQL設定
        cmdTextSb.Append(" UPDATE t_tenbetu_syoki_seikyuu_renkei ")
        cmdTextSb.Append("    SET ")
        If isAll = True Then
            cmdTextSb.Append("    renkei_siji_cd    = " & paramRenkeiSijiCd & ",")
            cmdTextSb.Append("    sousin_jyky_cd    = " & paramSousinJykyCd & ",")
        End If
        cmdTextSb.Append("        upd_login_user_id = " & paramUpdLoginUserId & ",")
        cmdTextSb.Append("        upd_datetime      = " & paramUpdDateTime)
        cmdTextSb.Append("  WHERE mise_cd           = " & paramMiseCd)
        cmdTextSb.Append("    AND bunrui_cd         = " & paramBunruiCd)

        cmdParams = New SqlParameter() { _
                SQLHelper.MakeParam(paramMiseCd, SqlDbType.Char, 5, renkeiRec.MiseCd), _
                SQLHelper.MakeParam(paramBunruiCd, SqlDbType.VarChar, 3, renkeiRec.BunruiCd), _
                SQLHelper.MakeParam(paramRenkeiSijiCd, SqlDbType.Int, 4, renkeiRec.RenkeiSijiCd), _
                SQLHelper.MakeParam(paramSousinJykyCd, SqlDbType.Int, 4, renkeiRec.SousinJykyCd), _
                SQLHelper.MakeParam(paramUpdLoginUserId, SqlDbType.VarChar, 30, renkeiRec.UpdLoginUserId), _
                SQLHelper.MakeParam(paramUpdDateTime, SqlDbType.DateTime, 16, DateTime.Now)}

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    cmdTextSb.ToString(), _
                                    cmdParams)
        Return intResult
    End Function
#End Region

#Region "店別請求連携"
    ''' <summary>
    ''' 更新対象の店別請求連携管理テーブルの内容を確認します
    ''' </summary>
    ''' <param name="renkeiRec">店別請求連携管理レコード</param>
    ''' <returns>送信状況コードと連携指示コードを格納したデータテーブル</returns>
    ''' <remarks>店別請求連携管理レコードに登録／更新対象のKEYを設定してから当メソッドを使用する</remarks>
    Public Function SelectTenbetuSeikyuuRenkeiData(ByVal renkeiRec As TenbetuSeikyuuRenkeiRecord) As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SelectTenbetuSeikyuuRenkeiData", _
                                                    renkeiRec)
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlParameter
        Dim renkeiDataSet As New TenbetuRenkeiDataSet
        Dim renkeiTable As TenbetuRenkeiDataSet.RenkeiTableDataTable

        '店別請求連携テーブルの存在チェック
        cmdTextSb.Append(" SELECT sousin_jyky_cd ")
        cmdTextSb.Append("       ,renkei_siji_cd ")
        cmdTextSb.Append("   FROM t_tenbetu_seikyuu_renkei WITH(UPDLOCK) ")
        cmdTextSb.Append("  WHERE mise_cd            = " & paramMiseCd)
        cmdTextSb.Append("    AND bunrui_cd          = " & paramBunruiCd)
        cmdTextSb.Append("    AND nyuuryoku_date     = " & paramNyuuryokuDate)
        cmdTextSb.Append("    AND nyuuryoku_date_no  = " & paramNyuuryokuDateNo)

        cmdParams = New SqlParameter() { _
                SQLHelper.MakeParam(paramMiseCd, SqlDbType.VarChar, 5, renkeiRec.MiseCd), _
                SQLHelper.MakeParam(paramBunruiCd, SqlDbType.VarChar, 3, renkeiRec.BunruiCd), _
                SQLHelper.MakeParam(paramNyuuryokuDate, SqlDbType.DateTime, 16, renkeiRec.NyuuryokuDate), _
                SQLHelper.MakeParam(paramNyuuryokuDateNo, SqlDbType.Int, 2, renkeiRec.NyuuryokuDateNo)}

        '検索実行
        SQLHelper.FillDataset(connStr, _
                              CommandType.Text, _
                              cmdTextSb.ToString(), _
                              renkeiDataSet, _
                              renkeiDataSet.RenkeiTable.TableName, _
                              cmdParams)
        renkeiTable = renkeiDataSet.RenkeiTable

        Return renkeiTable
    End Function

    ''' <summary>
    ''' 店別請求連携テーブルへデータを登録します
    ''' </summary>
    ''' <param name="renkeiRec">店別請求連携管理レコード</param>
    ''' <returns>登録結果</returns>
    ''' <remarks>店別請求連携管理レコードへ登録内容を設定してから当メソッドを使用する</remarks>
    Public Function InsertTenbetuSeikyuuRenkeiData(ByVal renkeiRec As TenbetuSeikyuuRenkeiRecord) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".InsertTenbetuSeikyuuRenkeiData", _
                                                    renkeiRec)
        Dim intResult As Integer = 0
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        ' 登録用SQL設定
        cmdTextSb.Append(" INSERT INTO t_tenbetu_seikyuu_renkei ")
        cmdTextSb.Append("     ( mise_cd ")
        cmdTextSb.Append("      ,bunrui_cd ")
        cmdTextSb.Append("      ,nyuuryoku_date ")
        cmdTextSb.Append("      ,nyuuryoku_date_no ")
        cmdTextSb.Append("      ,renkei_siji_cd ")
        cmdTextSb.Append("      ,sousin_jyky_cd ")
        cmdTextSb.Append("      ,upd_login_user_id ")
        cmdTextSb.Append("      ,upd_datetime ")
        cmdTextSb.Append("     ) VALUES ( ")
        cmdTextSb.Append("      " & paramMiseCd)
        cmdTextSb.Append("     ," & paramBunruiCd)
        cmdTextSb.Append("     ," & paramNyuuryokuDate)
        cmdTextSb.Append("     ," & paramNyuuryokuDateNo)
        cmdTextSb.Append("     ," & paramRenkeiSijiCd)
        cmdTextSb.Append("     ," & paramSousinJykyCd)
        cmdTextSb.Append("     ," & paramUpdLoginUserId)
        cmdTextSb.Append("     ," & paramUpdDateTime)
        cmdTextSb.Append("     ) ")
        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
                SQLHelper.MakeParam(paramMiseCd, SqlDbType.VarChar, 5, renkeiRec.MiseCd), _
                SQLHelper.MakeParam(paramBunruiCd, SqlDbType.VarChar, 3, renkeiRec.BunruiCd), _
                SQLHelper.MakeParam(paramNyuuryokuDate, SqlDbType.DateTime, 16, renkeiRec.NyuuryokuDate), _
                SQLHelper.MakeParam(paramNyuuryokuDateNo, SqlDbType.Int, 2, renkeiRec.NyuuryokuDateNo), _
                SQLHelper.MakeParam(paramRenkeiSijiCd, SqlDbType.Int, 4, renkeiRec.RenkeiSijiCd), _
                SQLHelper.MakeParam(paramSousinJykyCd, SqlDbType.Int, 4, renkeiRec.SousinJykyCd), _
                SQLHelper.MakeParam(paramUpdLoginUserId, SqlDbType.VarChar, 30, renkeiRec.UpdLoginUserId), _
                SQLHelper.MakeParam(paramUpdDateTime, SqlDbType.DateTime, 16, DateTime.Now)}
        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    cmdTextSb.ToString(), _
                                    cmdParams)
        Return intResult
    End Function

    ''' <summary>
    ''' 店別請求連携テーブルのデータを更新します
    ''' </summary>
    ''' <param name="renkeiRec">店別請求連携管理レコード</param>
    ''' <param name="isAll">True:送信コード,連携指示コードも更新する</param>
    ''' <returns>更新結果</returns>
    ''' <remarks>店別請求連携管理レコードへ更新対象を絞るKEYと更新内容を設定してから当メソッドを使用する</remarks>
    Public Function UpdateTenbetuSeikyuuRenkeiData(ByVal renkeiRec As TenbetuSeikyuuRenkeiRecord, _
                                                 ByVal isAll As Boolean) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".UpdateTenbetuSeikyuuRenkeiData", _
                                                    renkeiRec, _
                                                    isAll)
        Dim intResult As Integer = 0
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        ' 更新用SQL設定
        cmdTextSb.Append(" UPDATE t_tenbetu_seikyuu_renkei ")
        cmdTextSb.Append("    SET ")
        If isAll = True Then
            cmdTextSb.Append("    renkei_siji_cd    = " & paramRenkeiSijiCd & ",")
            cmdTextSb.Append("    sousin_jyky_cd    = " & paramSousinJykyCd & ",")
        End If
        cmdTextSb.Append("        upd_login_user_id = " & paramUpdLoginUserId & ",")
        cmdTextSb.Append("        upd_datetime      = " & paramUpdDateTime)
        cmdTextSb.Append("  WHERE mise_cd           = " & paramMiseCd)
        cmdTextSb.Append("    AND bunrui_cd         = " & paramBunruiCd)
        cmdTextSb.Append("    AND nyuuryoku_date     = " & paramNyuuryokuDate)
        cmdTextSb.Append("    AND nyuuryoku_date_no  = " & paramNyuuryokuDateNo)

        cmdParams = New SqlParameter() { _
                SQLHelper.MakeParam(paramMiseCd, SqlDbType.Char, 5, renkeiRec.MiseCd), _
                SQLHelper.MakeParam(paramBunruiCd, SqlDbType.VarChar, 3, renkeiRec.BunruiCd), _
                SQLHelper.MakeParam(paramNyuuryokuDate, SqlDbType.DateTime, 16, renkeiRec.NyuuryokuDate), _
                SQLHelper.MakeParam(paramNyuuryokuDateNo, SqlDbType.Int, 2, renkeiRec.NyuuryokuDateNo), _
                SQLHelper.MakeParam(paramRenkeiSijiCd, SqlDbType.Int, 4, renkeiRec.RenkeiSijiCd), _
                SQLHelper.MakeParam(paramSousinJykyCd, SqlDbType.Int, 4, renkeiRec.SousinJykyCd), _
                SQLHelper.MakeParam(paramUpdLoginUserId, SqlDbType.VarChar, 30, renkeiRec.UpdLoginUserId), _
                SQLHelper.MakeParam(paramUpdDateTime, SqlDbType.DateTime, 16, DateTime.Now)}

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    cmdTextSb.ToString(), _
                                    cmdParams)
        Return intResult
    End Function
#End Region

#Region "邸別入金連携"
    ''' <summary>
    ''' 更新対象の邸別入金連携管理テーブルの内容を確認します
    ''' </summary>
    ''' <param name="renkeiRec">邸別入金連携管理レコード</param>
    ''' <returns>送信状況コードと連携指示コードを格納したデータテーブル</returns>
    ''' <remarks>邸別入金連携管理レコードに登録／更新対象のKEYを設定してから当メソッドを使用する</remarks>
    Public Function SelectTeibetuNyuukinRenkeiData(ByVal renkeiRec As TeibetuNyuukinRenkeiRecord) As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SelectTeibetuNyuukinRenkeiData", _
                                            renkeiRec)
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlParameter
        Dim renkeiDataSet As New TeibetuNyuukinRenkeiDataSet()
        Dim renkeiTable As TeibetuNyuukinRenkeiDataSet.RenkeiTableDataTable

        ' 邸別入金連携テーブルの存在チェック
        cmdTextSb.Append(" SELECT sousin_jyky_cd ")
        cmdTextSb.Append("       ,renkei_siji_cd ")
        cmdTextSb.Append("   FROM t_teibetu_nyuukin_renkei WITH(UPDLOCK) ")
        cmdTextSb.Append("  WHERE kbn               = " & paramKubun)
        cmdTextSb.Append("    AND hosyousyo_no      = " & paramHosyousyoNo)
        cmdTextSb.Append("    AND SUBSTRING(bunrui_cd,1 ,2 ) = SUBSTRING(" & paramBunruiCd & ",1,2)")
        cmdTextSb.Append("    AND gamen_hyouji_no   = " & paramGamenHyoujiNo)

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam(paramKubun, SqlDbType.Char, 1, renkeiRec.Kbn), _
            SQLHelper.MakeParam(paramHosyousyoNo, SqlDbType.VarChar, 10, renkeiRec.HosyousyoNo), _
            SQLHelper.MakeParam(paramBunruiCd, SqlDbType.VarChar, 3, renkeiRec.BunruiCd), _
            SQLHelper.MakeParam(paramGamenHyoujiNo, SqlDbType.Int, 4, renkeiRec.GamenHyoujiNo)}

        ' データの取得
        SQLHelper.FillDataset(connStr, _
                              CommandType.Text, _
                              cmdTextSb.ToString(), _
                              renkeiDataSet, _
                              renkeiDataSet.RenkeiTable.TableName, _
                              cmdParams)
        renkeiTable = renkeiDataSet.RenkeiTable

        Return renkeiTable
    End Function

    ''' <summary>
    ''' 邸別入金連携テーブルへデータを登録します
    ''' </summary>
    ''' <param name="renkeiRec">邸別入金連携管理レコード</param>
    ''' <returns>登録結果</returns>
    ''' <remarks>邸別入金連携管理レコードへ登録内容を設定してから当メソッドを使用する</remarks>
    Public Function InsertTeibetuNyuukinRenkeiData(ByVal renkeiRec As TeibetuNyuukinRenkeiRecord) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".InsertTeibetuNyuukinRenkeiData", _
                                                    renkeiRec)
        Dim intResult As Integer = 0
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        ' 登録用SQL設定
        cmdTextSb.Append(" INSERT INTO t_teibetu_nyuukin_renkei ( ")
        cmdTextSb.Append("      kbn ")
        cmdTextSb.Append("     ,hosyousyo_no ")
        cmdTextSb.Append("     ,bunrui_cd ")
        cmdTextSb.Append("     ,gamen_hyouji_no ")
        cmdTextSb.Append("     ,renkei_siji_cd ")
        cmdTextSb.Append("     ,sousin_jyky_cd ")
        cmdTextSb.Append("     ,upd_login_user_id ")
        cmdTextSb.Append("     ,upd_datetime ")
        cmdTextSb.Append(" ) VALUES ( ")
        cmdTextSb.Append("      " & paramKubun)
        cmdTextSb.Append("     ," & paramHosyousyoNo)
        cmdTextSb.Append("     ," & paramBunruiCd)
        cmdTextSb.Append("     ," & paramGamenHyoujiNo)
        cmdTextSb.Append("     ," & paramRenkeiSijiCd)
        cmdTextSb.Append("     ," & paramSousinJykyCd)
        cmdTextSb.Append("     ," & paramUpdLoginUserId)
        cmdTextSb.Append("     ," & paramUpdDateTime)
        cmdTextSb.Append(" )")

        cmdParams = New SqlParameter() { _
                SQLHelper.MakeParam(paramKubun, SqlDbType.Char, 1, renkeiRec.Kbn), _
                SQLHelper.MakeParam(paramHosyousyoNo, SqlDbType.VarChar, 10, renkeiRec.HosyousyoNo), _
                SQLHelper.MakeParam(paramBunruiCd, SqlDbType.VarChar, 3, renkeiRec.BunruiCd), _
                SQLHelper.MakeParam(paramGamenHyoujiNo, SqlDbType.Int, 4, renkeiRec.GamenHyoujiNo), _
                SQLHelper.MakeParam(paramRenkeiSijiCd, SqlDbType.Int, 4, renkeiRec.RenkeiSijiCd), _
                SQLHelper.MakeParam(paramSousinJykyCd, SqlDbType.Int, 4, renkeiRec.SousinJykyCd), _
                SQLHelper.MakeParam(paramUpdLoginUserId, SqlDbType.VarChar, 30, renkeiRec.UpdLoginUserId), _
                SQLHelper.MakeParam(paramUpdDateTime, SqlDbType.DateTime, 16, DateTime.Now)}

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    cmdTextSb.ToString(), _
                                    cmdParams)
        Return intResult
    End Function

    ''' <summary>
    ''' 邸別入金連携テーブルのデータを更新します
    ''' </summary>
    ''' <param name="renkeiRec">邸別入金連携管理レコード</param>
    ''' <param name="isAll">True:送信コード,連携指示コードも更新する</param>
    ''' <returns>更新結果</returns>
    ''' <remarks>邸別入金連携管理レコードへ更新対象を絞るKEYと更新内容を設定してから当メソッドを使用する</remarks>
    Public Function UpdateTeibetuNyuukinRenkeiData(ByVal renkeiRec As TeibetuNyuukinRenkeiRecord, _
                                            ByVal isAll As Boolean) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".UpdateTeibetuNyuukinRenkeiData", _
                                                    renkeiRec, _
                                                    isAll)
        Dim intResult As Integer = 0
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        ' 更新用SQL設定
        cmdTextSb.Append(" UPDATE t_teibetu_nyuukin_renkei ")
        cmdTextSb.Append("    SET ")
        cmdTextSb.Append("        bunrui_cd         = " & paramBunruiCd & ",")
        If isAll = True Then
            cmdTextSb.Append("    renkei_siji_cd    = " & paramRenkeiSijiCd & ",")
            cmdTextSb.Append("    sousin_jyky_cd    = " & paramSousinJykyCd & ",")
        End If
        cmdTextSb.Append("        upd_login_user_id = " & paramUpdLoginUserId & ",")
        cmdTextSb.Append("        upd_datetime      = " & paramUpdDateTime)
        cmdTextSb.Append("  WHERE kbn               = " & paramKubun)
        cmdTextSb.Append("    AND hosyousyo_no      = " & paramHosyousyoNo)
        cmdTextSb.Append("    AND SUBSTRING(bunrui_cd,1,2) = SUBSTRING(" & paramBunruiCd & ",1,2)")
        cmdTextSb.Append("    AND gamen_hyouji_no   = " & paramGamenHyoujiNo)

        cmdParams = New SqlParameter() { _
                SQLHelper.MakeParam(paramKubun, SqlDbType.Char, 1, renkeiRec.Kbn), _
                SQLHelper.MakeParam(paramHosyousyoNo, SqlDbType.VarChar, 10, renkeiRec.HosyousyoNo), _
                SQLHelper.MakeParam(paramBunruiCd, SqlDbType.VarChar, 3, renkeiRec.BunruiCd), _
                SQLHelper.MakeParam(paramGamenHyoujiNo, SqlDbType.Int, 4, renkeiRec.GamenHyoujiNo), _
                SQLHelper.MakeParam(paramRenkeiSijiCd, SqlDbType.Int, 4, renkeiRec.RenkeiSijiCd), _
                SQLHelper.MakeParam(paramSousinJykyCd, SqlDbType.Int, 4, renkeiRec.SousinJykyCd), _
                SQLHelper.MakeParam(paramUpdLoginUserId, SqlDbType.VarChar, 30, renkeiRec.UpdLoginUserId), _
                SQLHelper.MakeParam(paramUpdDateTime, SqlDbType.DateTime, 16, DateTime.Now)}

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    cmdTextSb.ToString(), _
                                    cmdParams)
        Return intResult
    End Function
#End Region

#Region "邸別入金連携(旧バージョン－削除予定)"
    ''' <summary>
    ''' 更新対象の邸別入金連携管理テーブルの内容を確認します
    ''' </summary>
    ''' <param name="renkeiRec">邸別入金連携管理レコード</param>
    ''' <returns>送信状況コードと連携指示コードを格納したデータテーブル</returns>
    ''' <remarks>邸別入金連携管理レコードに登録／更新対象のKEYを設定してから当メソッドを使用する</remarks>
    Public Function SelectTeibetuNyuukinRenkeiDataOld(ByVal renkeiRec As TeibetuNyuukinRenkeiRecordOld) As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SelectTeibetuNyuukinRenkeiData", _
                                            renkeiRec)
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlParameter
        Dim renkeiDataSet As New TeibetuNyuukinRenkeiDataSet()
        Dim renkeiTable As TeibetuNyuukinRenkeiDataSet.RenkeiTableDataTable

        ' 邸別入金連携テーブルの存在チェック
        cmdTextSb.Append(" SELECT sousin_jyky_cd ")
        cmdTextSb.Append("       ,renkei_siji_cd ")
        cmdTextSb.Append("   FROM t_teibetu_nyuukin_renkei WITH(UPDLOCK) ")
        cmdTextSb.Append("  WHERE kbn               = " & paramKubun)
        cmdTextSb.Append("    AND hosyousyo_no      = " & paramHosyousyoNo)
        cmdTextSb.Append("    AND bunrui_cd         = " & paramBunruiCd)

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam(paramKubun, SqlDbType.Char, 1, renkeiRec.Kbn), _
            SQLHelper.MakeParam(paramHosyousyoNo, SqlDbType.VarChar, 10, renkeiRec.HosyousyoNo), _
            SQLHelper.MakeParam(paramBunruiCd, SqlDbType.VarChar, 3, renkeiRec.BunruiCd)}

        ' データの取得
        SQLHelper.FillDataset(connStr, _
                              CommandType.Text, _
                              cmdTextSb.ToString(), _
                              renkeiDataSet, _
                              renkeiDataSet.RenkeiTable.TableName, _
                              cmdParams)
        renkeiTable = renkeiDataSet.RenkeiTable

        Return renkeiTable
    End Function

    ''' <summary>
    ''' 邸別入金連携テーブルへデータを登録します
    ''' </summary>
    ''' <param name="renkeiRec">邸別入金連携管理レコード</param>
    ''' <returns>登録結果</returns>
    ''' <remarks>邸別入金連携管理レコードへ登録内容を設定してから当メソッドを使用する</remarks>
    Public Function InsertTeibetuNyuukinRenkeiDataOld(ByVal renkeiRec As TeibetuNyuukinRenkeiRecordOld) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".InsertTeibetuNyuukinRenkeiData", _
                                                    renkeiRec)
        Dim intResult As Integer = 0
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        ' 登録用SQL設定
        cmdTextSb.Append(" INSERT INTO t_teibetu_nyuukin_renkei ( ")
        cmdTextSb.Append("      kbn ")
        cmdTextSb.Append("     ,hosyousyo_no ")
        cmdTextSb.Append("     ,bunrui_cd ")
        cmdTextSb.Append("     ,renkei_siji_cd ")
        cmdTextSb.Append("     ,sousin_jyky_cd ")
        cmdTextSb.Append("     ,upd_login_user_id ")
        cmdTextSb.Append("     ,upd_datetime ")
        cmdTextSb.Append(" ) VALUES ( ")
        cmdTextSb.Append("      " & paramKubun)
        cmdTextSb.Append("     ," & paramHosyousyoNo)
        cmdTextSb.Append("     ," & paramBunruiCd)
        cmdTextSb.Append("     ," & paramRenkeiSijiCd)
        cmdTextSb.Append("     ," & paramSousinJykyCd)
        cmdTextSb.Append("     ," & paramUpdLoginUserId)
        cmdTextSb.Append("     ," & paramUpdDateTime)
        cmdTextSb.Append(" )")

        cmdParams = New SqlParameter() { _
                SQLHelper.MakeParam(paramKubun, SqlDbType.Char, 1, renkeiRec.Kbn), _
                SQLHelper.MakeParam(paramHosyousyoNo, SqlDbType.VarChar, 10, renkeiRec.HosyousyoNo), _
                SQLHelper.MakeParam(paramBunruiCd, SqlDbType.VarChar, 3, renkeiRec.BunruiCd), _
                SQLHelper.MakeParam(paramRenkeiSijiCd, SqlDbType.Int, 4, renkeiRec.RenkeiSijiCd), _
                SQLHelper.MakeParam(paramSousinJykyCd, SqlDbType.Int, 4, renkeiRec.SousinJykyCd), _
                SQLHelper.MakeParam(paramUpdLoginUserId, SqlDbType.VarChar, 30, renkeiRec.UpdLoginUserId), _
                SQLHelper.MakeParam(paramUpdDateTime, SqlDbType.DateTime, 16, DateTime.Now)}

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    cmdTextSb.ToString(), _
                                    cmdParams)
        Return intResult
    End Function

    ''' <summary>
    ''' 邸別入金連携テーブルのデータを更新します
    ''' </summary>
    ''' <param name="renkeiRec">邸別入金連携管理レコード</param>
    ''' <param name="isAll">True:送信コード,連携指示コードも更新する</param>
    ''' <returns>更新結果</returns>
    ''' <remarks>邸別入金連携管理レコードへ更新対象を絞るKEYと更新内容を設定してから当メソッドを使用する</remarks>
    Public Function UpdateTeibetuNyuukinRenkeiDataOld(ByVal renkeiRec As TeibetuNyuukinRenkeiRecordOld, _
                                            ByVal isAll As Boolean) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".UpdateTeibetuNyuukinRenkeiData", _
                                                    renkeiRec, _
                                                    isAll)
        Dim intResult As Integer = 0
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        ' 更新用SQL設定
        cmdTextSb.Append(" UPDATE t_teibetu_nyuukin_renkei ")
        cmdTextSb.Append("    SET ")
        If isAll = True Then
            cmdTextSb.Append("    renkei_siji_cd    = " & paramRenkeiSijiCd & ",")
            cmdTextSb.Append("    sousin_jyky_cd    = " & paramSousinJykyCd & ",")
        End If
        cmdTextSb.Append("        upd_login_user_id = " & paramUpdLoginUserId & ",")
        cmdTextSb.Append("        upd_datetime      = " & paramUpdDateTime)
        cmdTextSb.Append("  WHERE kbn               = " & paramKubun)
        cmdTextSb.Append("    AND hosyousyo_no      = " & paramHosyousyoNo)
        cmdTextSb.Append("    AND bunrui_cd         = " & paramBunruiCd)

        cmdParams = New SqlParameter() { _
                SQLHelper.MakeParam(paramKubun, SqlDbType.Char, 1, renkeiRec.Kbn), _
                SQLHelper.MakeParam(paramHosyousyoNo, SqlDbType.VarChar, 10, renkeiRec.HosyousyoNo), _
                SQLHelper.MakeParam(paramBunruiCd, SqlDbType.VarChar, 3, renkeiRec.BunruiCd), _
                SQLHelper.MakeParam(paramRenkeiSijiCd, SqlDbType.Int, 4, renkeiRec.RenkeiSijiCd), _
                SQLHelper.MakeParam(paramSousinJykyCd, SqlDbType.Int, 4, renkeiRec.SousinJykyCd), _
                SQLHelper.MakeParam(paramUpdLoginUserId, SqlDbType.VarChar, 30, renkeiRec.UpdLoginUserId), _
                SQLHelper.MakeParam(paramUpdDateTime, SqlDbType.DateTime, 16, DateTime.Now)}

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    cmdTextSb.ToString(), _
                                    cmdParams)
        Return intResult
    End Function
#End Region

#Region "地盤連携"
    ''' <summary>
    ''' 更新対象の地盤連携管理テーブルの内容を確認します
    ''' </summary>
    ''' <param name="renkeiRec">地盤連携管理レコード</param>
    ''' <returns>送信状況コードと連携指示コードを格納したデータテーブル</returns>
    ''' <remarks>地盤連携管理レコードに登録／更新対象のKEYを設定してから当メソッドを使用する</remarks>
    Public Function SelectJibanRenkeiData(ByVal renkeiRec As JibanRenkeiRecord) As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SelectJibanRenkeiData", _
                                                    renkeiRec)
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlParameter
        Dim renkeiDataSet As New JibanRenkeiDataSet()
        Dim renkeiTable As JibanRenkeiDataSet.RenkeiTableDataTable

        '地盤連携テーブルの存在チェック
        cmdTextSb.Append(" SELECT sousin_jyky_cd ")
        cmdTextSb.Append("       ,renkei_siji_cd ")
        cmdTextSb.Append("   FROM t_jiban_renkei WITH(UPDLOCK) ")
        cmdTextSb.Append("  WHERE kbn               = " & paramKubun)
        cmdTextSb.Append("    AND hosyousyo_no      = " & paramHosyousyoNo)

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam(paramKubun, SqlDbType.Char, 1, renkeiRec.Kbn), _
            SQLHelper.MakeParam(paramHosyousyoNo, SqlDbType.VarChar, 10, renkeiRec.HosyousyoNo)}

        ' データの取得
        SQLHelper.FillDataset(connStr, _
                              CommandType.Text, _
                              cmdTextSb.ToString(), _
                              renkeiDataSet, _
                              renkeiDataSet.RenkeiTable.TableName, _
                              cmdParams)
        renkeiTable = renkeiDataSet.RenkeiTable

        Return renkeiTable
    End Function

    ''' <summary>
    ''' 地盤連携テーブルへデータを登録します
    ''' </summary>
    ''' <param name="renkeiRec">地盤連携管理レコード</param>
    ''' <returns>登録結果</returns>
    ''' <remarks>地盤連携管理レコードへ登録内容を設定してから当メソッドを使用する</remarks>
    Public Function InsertJibanRenkeiData(ByVal renkeiRec As JibanRenkeiRecord) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".InsertJibanRenkeiData", _
                                                    renkeiRec)
        Dim intResult As Integer = 0
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter
        ' 登録用SQL設定
        cmdTextSb.Append(" INSERT INTO t_jiban_renkei ( ")
        cmdTextSb.Append("      kbn ")
        cmdTextSb.Append("     ,hosyousyo_no ")
        cmdTextSb.Append("     ,renkei_siji_cd ")
        cmdTextSb.Append("     ,sousin_jyky_cd ")
        cmdTextSb.Append("     ,upd_login_user_id ")
        cmdTextSb.Append("     ,upd_datetime ")
        cmdTextSb.Append(" ) VALUES ( ")
        cmdTextSb.Append("      " & paramKubun)
        cmdTextSb.Append("     ," & paramHosyousyoNo)
        cmdTextSb.Append("     ," & paramRenkeiSijiCd)
        cmdTextSb.Append("     ," & paramSousinJykyCd)
        cmdTextSb.Append("     ," & paramUpdLoginUserId)
        cmdTextSb.Append("     ," & paramUpdDateTime)
        cmdTextSb.Append(" )")

        cmdParams = New SqlParameter() { _
                SQLHelper.MakeParam(paramKubun, SqlDbType.Char, 1, renkeiRec.Kbn), _
                SQLHelper.MakeParam(paramHosyousyoNo, SqlDbType.VarChar, 10, renkeiRec.HosyousyoNo), _
                SQLHelper.MakeParam(paramRenkeiSijiCd, SqlDbType.Int, 4, renkeiRec.RenkeiSijiCd), _
                SQLHelper.MakeParam(paramSousinJykyCd, SqlDbType.Int, 4, renkeiRec.SousinJykyCd), _
                SQLHelper.MakeParam(paramUpdLoginUserId, SqlDbType.VarChar, 30, renkeiRec.UpdLoginUserId), _
                SQLHelper.MakeParam(paramUpdDateTime, SqlDbType.DateTime, 16, DateTime.Now)}

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    cmdTextSb.ToString(), _
                                    cmdParams)
        Return intResult
    End Function

    ''' <summary>
    ''' 地盤連携テーブルのデータを更新します
    ''' </summary>
    ''' <param name="renkeiRec">地盤連携管理レコード</param>
    ''' <param name="isAll">True:送信コード,連携指示コードも更新する</param>
    ''' <returns>更新結果</returns>
    ''' <remarks>地盤連携管理レコードへ更新対象を絞るKEYと更新内容を設定してから当メソッドを使用する</remarks>
    Public Function UpdateJibanRenkeiData(ByVal renkeiRec As JibanRenkeiRecord, _
                                            ByVal isAll As Boolean) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".UpdateJibanRenkeiData", _
                                                    renkeiRec, _
                                                    isAll)
        Dim intResult As Integer = 0
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        ' 更新用SQL設定
        cmdTextSb.Append(" UPDATE t_jiban_renkei ")
        cmdTextSb.Append("    SET ")
        If isAll = True Then
            cmdTextSb.Append("    renkei_siji_cd    = " & paramRenkeiSijiCd & ",")
            cmdTextSb.Append("    sousin_jyky_cd    = " & paramSousinJykyCd & ",")
        End If
        cmdTextSb.Append("        upd_login_user_id = " & paramUpdLoginUserId & ",")
        cmdTextSb.Append("        upd_datetime      = " & paramUpdDateTime)
        cmdTextSb.Append("  WHERE kbn               = " & paramKubun)
        cmdTextSb.Append("    AND hosyousyo_no      = " & paramHosyousyoNo)

        cmdParams = New SqlParameter() { _
                SQLHelper.MakeParam(paramKubun, SqlDbType.Char, 1, renkeiRec.Kbn), _
                SQLHelper.MakeParam(paramHosyousyoNo, SqlDbType.VarChar, 10, renkeiRec.HosyousyoNo), _
                SQLHelper.MakeParam(paramRenkeiSijiCd, SqlDbType.Int, 4, renkeiRec.RenkeiSijiCd), _
                SQLHelper.MakeParam(paramSousinJykyCd, SqlDbType.Int, 4, renkeiRec.SousinJykyCd), _
                SQLHelper.MakeParam(paramUpdLoginUserId, SqlDbType.VarChar, 30, renkeiRec.UpdLoginUserId), _
                SQLHelper.MakeParam(paramUpdDateTime, SqlDbType.DateTime, 16, DateTime.Now)}

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    cmdTextSb.ToString(), _
                                    cmdParams)
        Return intResult
    End Function
#End Region

#Region "更新履歴連携"

    ''' <summary>
    ''' 更新対象の更新履歴連携管理テーブルの内容を確認します
    ''' </summary>
    ''' <param name="renkeiRec">更新履歴連携管理レコード</param>
    ''' <returns>送信状況コードと連携指示コードを格納したデータテーブル</returns>
    ''' <remarks>更新履歴連携管理レコードに登録／更新対象のKEYを設定してから当メソッドを使用する</remarks>
    Public Function SelectKousinRirekiRenkeiData(ByVal renkeiRec As KousinRirekiRenkeiRecord) As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SelectKousinRirekiRenkeiData", _
                                                    renkeiRec)
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlParameter
        Dim renkeiDataSet As New KousinRirekiRenkeiDataSet()
        Dim renkeiTable As KousinRirekiRenkeiDataSet.RenkeiTableDataTable

        ' 更新履歴連携テーブルの存在チェック
        cmdTextSb.Append(" SELECT sousin_jyky_cd ")
        cmdTextSb.Append("       ,renkei_siji_cd ")
        cmdTextSb.Append("   FROM t_kousin_rireki_renkei WITH(UPDLOCK) ")
        cmdTextSb.Append("  WHERE upd_datetime_rireki = " & paramUpdDatetimeRireki)
        cmdTextSb.Append("    AND kbn                 = " & paramKubun)
        cmdTextSb.Append("    AND hosyousyo_no        = " & paramHosyousyoNo)
        cmdTextSb.Append("    AND upd_koumoku         = " & paramUpdKoumoku)

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam(paramUpdDatetimeRireki, SqlDbType.DateTime, 16, renkeiRec.UpdDatetime), _
            SQLHelper.MakeParam(paramKubun, SqlDbType.Char, 1, renkeiRec.Kbn), _
            SQLHelper.MakeParam(paramHosyousyoNo, SqlDbType.VarChar, 10, renkeiRec.HosyousyoNo), _
            SQLHelper.MakeParam(paramUpdKoumoku, SqlDbType.VarChar, 30, renkeiRec.UpdKoumoku)}

        ' データの取得
        SQLHelper.FillDataset(connStr, _
                              CommandType.Text, _
                              cmdTextSb.ToString(), _
                              renkeiDataSet, _
                              renkeiDataSet.RenkeiTable.TableName, _
                              cmdParams)
        renkeiTable = renkeiDataSet.RenkeiTable

        Return renkeiTable
    End Function

    ''' <summary>
    ''' 更新履歴連携テーブルへデータを登録します
    ''' </summary>
    ''' <param name="renkeiRec">更新履歴連携管理レコード</param>
    ''' <returns>登録結果</returns>
    ''' <remarks>更新履歴連携管理レコードへ登録内容を設定してから問うメソッドを使用する</remarks>
    Public Function InsertKousinRirekiRenkeiData(ByVal renkeiRec As KousinRirekiRenkeiRecord) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".InsertKousinRirekiRenkeiData", _
                                                    renkeiRec)
        Dim intResult As Integer = 0
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        ' 登録用SQL設定
        cmdTextSb.Append(" INSERT INTO t_kousin_rireki_renkei ( ")
        cmdTextSb.Append("      upd_datetime_rireki ")
        cmdTextSb.Append("     ,kbn ")
        cmdTextSb.Append("     ,hosyousyo_no ")
        cmdTextSb.Append("     ,upd_koumoku ")
        cmdTextSb.Append("     ,renkei_siji_cd ")
        cmdTextSb.Append("     ,sousin_jyky_cd ")
        cmdTextSb.Append("     ,upd_login_user_id ")
        cmdTextSb.Append("     ,upd_datetime ")
        cmdTextSb.Append(" ) VALUES ( ")
        cmdTextSb.Append("      " & paramUpdDatetimeRireki)
        cmdTextSb.Append("     ," & paramKubun)
        cmdTextSb.Append("     ," & paramHosyousyoNo)
        cmdTextSb.Append("     ," & paramUpdKoumoku)
        cmdTextSb.Append("     ," & paramRenkeiSijiCd)
        cmdTextSb.Append("     ," & paramSousinJykyCd)
        cmdTextSb.Append("     ," & paramUpdLoginUserId)
        cmdTextSb.Append("     ," & paramUpdDateTime)
        cmdTextSb.Append(" )")

        cmdParams = New SqlParameter() { _
                SQLHelper.MakeParam(paramUpdDatetimeRireki, SqlDbType.DateTime, 16, renkeiRec.UpdDatetime), _
                SQLHelper.MakeParam(paramKubun, SqlDbType.Char, 1, renkeiRec.Kbn), _
                SQLHelper.MakeParam(paramHosyousyoNo, SqlDbType.VarChar, 10, renkeiRec.HosyousyoNo), _
                SQLHelper.MakeParam(paramGamenHyoujiNo, SqlDbType.VarChar, 30, renkeiRec.UpdKoumoku), _
                SQLHelper.MakeParam(paramRenkeiSijiCd, SqlDbType.Int, 4, renkeiRec.RenkeiSijiCd), _
                SQLHelper.MakeParam(paramSousinJykyCd, SqlDbType.Int, 4, renkeiRec.SousinJykyCd), _
                SQLHelper.MakeParam(paramUpdLoginUserId, SqlDbType.VarChar, 30, renkeiRec.UpdLoginUserId), _
                SQLHelper.MakeParam(paramUpdDateTime, SqlDbType.DateTime, 16, DateTime.Now)}

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    cmdTextSb.ToString(), _
                                    cmdParams)
        Return intResult
    End Function

    ''' <summary>
    ''' 更新履歴連携テーブルのデータを更新します
    ''' </summary>
    ''' <param name="renkeiRec">更新履歴連携管理レコード</param>
    ''' <param name="isAll">True:送信コード,連携指示コードも更新する</param>
    ''' <returns>更新結果</returns>
    ''' <remarks>更新履歴連携管理レコードへ更新対象を絞るKEYと更新内容を設定してから当メソッドを使用する</remarks>
    Public Function UpdateKousinRirekiRenkeiData(ByVal renkeiRec As KousinRirekiRenkeiRecord, _
                                                ByVal isAll As Boolean) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".UpdateKousinRirekiRenkeiDate", _
                                                            renkeiRec, _
                                                            isAll)
        Dim intResult As Integer = 0
        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        ' 更新用SQL設定
        cmdTextSb.Append(" UPDATE t_kousin_rireki_renkei ")
        cmdTextSb.Append("    SET ")
        If isAll = True Then
            cmdTextSb.Append("    renkei_siji_cd    = " & paramRenkeiSijiCd & ",")
            cmdTextSb.Append("    sousin_jyky_cd    = " & paramSousinJykyCd & ",")
        End If
        cmdTextSb.Append("        upd_login_user_id = " & paramUpdLoginUserId & ",")
        cmdTextSb.Append("        upd_datetime      = " & paramUpdDateTime)
        cmdTextSb.Append("  WHERE upd_datetime_rireki = " & paramUpdDatetimeRireki)
        cmdTextSb.Append("    AND kbn                 = " & paramKubun)
        cmdTextSb.Append("    AND hosyousyo_no        = " & paramHosyousyoNo)
        cmdTextSb.Append("    AND upd_koumoku         = " & paramUpdKoumoku)

        cmdParams = New SqlParameter() { _
                SQLHelper.MakeParam(paramUpdDatetimeRireki, SqlDbType.DateTime, 16, renkeiRec.UpdDatetime), _
                SQLHelper.MakeParam(paramKubun, SqlDbType.Char, 1, renkeiRec.Kbn), _
                SQLHelper.MakeParam(paramHosyousyoNo, SqlDbType.VarChar, 10, renkeiRec.HosyousyoNo), _
                SQLHelper.MakeParam(paramGamenHyoujiNo, SqlDbType.VarChar, 30, renkeiRec.UpdKoumoku), _
                SQLHelper.MakeParam(paramRenkeiSijiCd, SqlDbType.Int, 4, renkeiRec.RenkeiSijiCd), _
                SQLHelper.MakeParam(paramSousinJykyCd, SqlDbType.Int, 4, renkeiRec.SousinJykyCd), _
                SQLHelper.MakeParam(paramUpdLoginUserId, SqlDbType.VarChar, 30, renkeiRec.UpdLoginUserId), _
                SQLHelper.MakeParam(paramUpdDateTime, SqlDbType.DateTime, 16, DateTime.Now)}

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    cmdTextSb.ToString(), _
                                    cmdParams)
        Return intResult
    End Function

#End Region

    ''' <summary>
    ''' 連携管理用SQLパラメーターを作成します(一括更新用)
    ''' </summary>
    ''' <param name="strLoginUserId">ログインユーザーID</param>
    ''' <returns>SQLパラメータ</returns>
    ''' <remarks></remarks>
    Public Function GetRenkeiCmdParams(ByVal strLoginUserId As String) As SqlParameter()
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetRenkeiCmdParams" _
                                                    , strLoginUserId)
        Dim cmdParams() As SqlParameter

        ' パラメータを作成
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam(paramUpdLoginUserId, SqlDbType.VarChar, 30, strLoginUserId), _
            SQLHelper.MakeParam(paramUpdDateTime, SqlDbType.DateTime, 16, DateTime.Now)}

        Return cmdParams

    End Function

#Region "更新ログインユーザと更新日時を条件に、一括で連携管理テーブルへ追加/更新する処理 (元テーブルへの追加/更新時のみ有効。削除されたレコードには対応不可)"

    ''' <summary>
    ''' 邸別請求連携管理テーブルにUPDATE/INSERT
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function setTeibetuSeikyuuRenkei(ByVal updDate As DateTime, _
                                            ByVal loginUserId As String) As Integer

        Dim intResult As Integer = 0
        Dim sb As New StringBuilder()
        Dim sqlParams() As SqlClient.SqlParameter

        'SQL文生成
        sb.AppendLine("UPDATE [jhs_sys].[t_teibetu_seikyuu_renkei] ")
        sb.AppendLine("SET  ")
        sb.AppendLine("    bunrui_cd = t.bunrui_cd ")
        sb.AppendLine("    , renkei_siji_cd = 2 ")
        sb.AppendLine("    , sousin_jyky_cd = 0 ")
        sb.AppendLine("    , sousin_kanry_datetime = NULL ")
        sb.AppendLine("    , upd_login_user_id = t.upd_login_user_id ")
        sb.AppendLine("    , upd_datetime = t.upd_datetime ")
        sb.AppendLine(" ")
        sb.AppendLine("FROM")
        sb.AppendLine("    jhs_sys.t_teibetu_seikyuu_renkei r ")
        sb.AppendLine("    INNER JOIN jhs_sys.t_teibetu_seikyuu t ")
        sb.AppendLine("        ON r.kbn = t.kbn ")
        sb.AppendLine("        AND r.hosyousyo_no = t.hosyousyo_no ")
        sb.AppendLine("        AND REPLACE (r.bunrui_cd, '115', '110') = REPLACE (t.bunrui_cd, '115', '110') ")
        sb.AppendLine("        AND r.gamen_hyouji_no = t.gamen_hyouji_no")
        sb.AppendLine("WHERE")
        sb.AppendLine("    ( ")
        sb.AppendLine("        t.upd_login_user_id IS NOT NULL ")
        sb.AppendLine("        AND t.upd_datetime IS NOT NULL ")
        sb.AppendLine("        AND t.upd_login_user_id = " & paramUpdLoginUserId & " ")
        sb.AppendLine("        AND t.upd_datetime = " & paramUpdDateTime & "")
        sb.AppendLine("    ) ")
        sb.AppendLine("    OR ( ")
        sb.AppendLine("        t.upd_login_user_id IS NULL ")
        sb.AppendLine("        AND t.upd_datetime IS NULL ")
        sb.AppendLine("        AND t.add_login_user_id = " & paramUpdLoginUserId & " ")
        sb.AppendLine("        AND t.add_datetime = " & paramUpdDateTime & "")
        sb.AppendLine("    ) ")
        sb.AppendLine("")

        sb.AppendLine("INSERT ")
        sb.AppendLine("INTO [jhs_sys].[t_teibetu_seikyuu_renkei] ( ")
        sb.AppendLine("    kbn")
        sb.AppendLine("    , hosyousyo_no")
        sb.AppendLine("    , bunrui_cd")
        sb.AppendLine("    , gamen_hyouji_no")
        sb.AppendLine("    , renkei_siji_cd")
        sb.AppendLine("    , sousin_jyky_cd")
        sb.AppendLine("    , sousin_kanry_datetime")
        sb.AppendLine("    , upd_login_user_id")
        sb.AppendLine("    , upd_datetime")
        sb.AppendLine(") ")
        sb.AppendLine("SELECT")
        sb.AppendLine("    t.kbn")
        sb.AppendLine("    , t.hosyousyo_no")
        sb.AppendLine("    , t.bunrui_cd")
        sb.AppendLine("    , t.gamen_hyouji_no")
        sb.AppendLine("    , 1")
        sb.AppendLine("    , 0")
        sb.AppendLine("    , NULL")
        sb.AppendLine("    , t.upd_login_user_id")
        sb.AppendLine("    , t.upd_datetime ")
        sb.AppendLine("FROM")
        sb.AppendLine("    jhs_sys.t_teibetu_seikyuu t ")
        sb.AppendLine("    LEFT OUTER JOIN jhs_sys.t_teibetu_seikyuu_renkei r ")
        sb.AppendLine("        ON t.kbn = r.kbn ")
        sb.AppendLine("        AND t.hosyousyo_no = r.hosyousyo_no ")
        sb.AppendLine("        AND REPLACE(t.bunrui_cd,'115','110') = REPLACE(r.bunrui_cd,'115','110') ")
        sb.AppendLine("        AND t.gamen_hyouji_no = r.gamen_hyouji_no ")
        sb.AppendLine("WHERE")
        sb.AppendLine("    r.kbn IS NULL ")
        sb.AppendLine("    AND ( ")
        sb.AppendLine("        ( ")
        sb.AppendLine("            t.upd_login_user_id IS NOT NULL ")
        sb.AppendLine("            AND t.upd_datetime IS NOT NULL ")
        sb.AppendLine("            AND t.upd_login_user_id = " & paramUpdLoginUserId & " ")
        sb.AppendLine("            AND t.upd_datetime = " & paramUpdDateTime & "")
        sb.AppendLine("        ) ")
        sb.AppendLine("        OR ( ")
        sb.AppendLine("            t.upd_login_user_id IS NULL ")
        sb.AppendLine("            AND t.upd_datetime IS NULL ")
        sb.AppendLine("            AND t.add_login_user_id = " & paramUpdLoginUserId & " ")
        sb.AppendLine("            AND t.add_datetime = " & paramUpdDateTime & "")
        sb.AppendLine("        )")
        sb.AppendLine("    ) ")
        sb.AppendLine("")


        ' パラメータへ設定
        sqlParams = New SqlParameter() { _
                                        SQLHelper.MakeParam(paramUpdLoginUserId, SqlDbType.VarChar, 30, loginUserId), _
                                        SQLHelper.MakeParam(paramUpdDateTime, SqlDbType.DateTime, 1, updDate) _
                                        }

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    sb.ToString(), _
                                    sqlParams)

        Return intResult

    End Function

    ''' <summary>
    ''' 店別請求連携管理テーブルにUPDATE/INSERT
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function setTenbetuSeikyuuRenkei(ByVal updDate As DateTime, _
                                            ByVal loginUserId As String) As Integer

        Dim intResult As Integer = 0
        Dim sb As New StringBuilder()
        Dim sqlParams() As SqlClient.SqlParameter

        'SQL文生成
        sb.AppendLine("UPDATE [jhs_sys].[t_tenbetu_seikyuu_renkei] ")
        sb.AppendLine("SET  ")
        sb.AppendLine("    renkei_siji_cd = 2 ")
        sb.AppendLine("    , sousin_jyky_cd = 0 ")
        sb.AppendLine("    , sousin_kanry_datetime = NULL ")
        sb.AppendLine("    , upd_login_user_id = t.upd_login_user_id ")
        sb.AppendLine("    , upd_datetime = t.upd_datetime ")
        sb.AppendLine(" ")
        sb.AppendLine("FROM")
        sb.AppendLine("    jhs_sys.t_tenbetu_seikyuu_renkei r ")
        sb.AppendLine("    INNER JOIN jhs_sys.t_tenbetu_seikyuu t ")
        sb.AppendLine("        ON r.mise_cd = t.mise_cd ")
        sb.AppendLine("        AND r.bunrui_cd = t.bunrui_cd ")
        sb.AppendLine("        AND r.nyuuryoku_date = t.nyuuryoku_date ")
        sb.AppendLine("        AND r.nyuuryoku_date_no = t.nyuuryoku_date_no")
        sb.AppendLine("WHERE")
        sb.AppendLine("    ( ")
        sb.AppendLine("        t.upd_login_user_id IS NOT NULL ")
        sb.AppendLine("        AND t.upd_datetime IS NOT NULL ")
        sb.AppendLine("        AND t.upd_login_user_id = " & paramUpdLoginUserId & " ")
        sb.AppendLine("        AND t.upd_datetime = " & paramUpdDateTime & "")
        sb.AppendLine("    ) ")
        sb.AppendLine("    OR ( ")
        sb.AppendLine("        t.upd_login_user_id IS NULL ")
        sb.AppendLine("        AND t.upd_datetime IS NULL ")
        sb.AppendLine("        AND t.add_login_user_id = " & paramUpdLoginUserId & " ")
        sb.AppendLine("        AND t.add_datetime = " & paramUpdDateTime & "")
        sb.AppendLine("    ) ")
        sb.AppendLine("")

        sb.AppendLine("INSERT ")
        sb.AppendLine("INTO [jhs_sys].[t_tenbetu_seikyuu_renkei] ( ")
        sb.AppendLine("    mise_cd")
        sb.AppendLine("    , bunrui_cd")
        sb.AppendLine("    , nyuuryoku_date")
        sb.AppendLine("    , nyuuryoku_date_no")
        sb.AppendLine("    , renkei_siji_cd")
        sb.AppendLine("    , sousin_jyky_cd")
        sb.AppendLine("    , sousin_kanry_datetime")
        sb.AppendLine("    , upd_login_user_id")
        sb.AppendLine("    , upd_datetime")
        sb.AppendLine(") ")
        sb.AppendLine("SELECT")
        sb.AppendLine("    t.mise_cd")
        sb.AppendLine("    , t.bunrui_cd")
        sb.AppendLine("    , t.nyuuryoku_date")
        sb.AppendLine("    , t.nyuuryoku_date_no")
        sb.AppendLine("    , 1")
        sb.AppendLine("    , 0")
        sb.AppendLine("    , NULL")
        sb.AppendLine("    , t.upd_login_user_id")
        sb.AppendLine("    , t.upd_datetime ")
        sb.AppendLine("FROM")
        sb.AppendLine("    jhs_sys.t_tenbetu_seikyuu t ")
        sb.AppendLine("    LEFT OUTER JOIN jhs_sys.t_tenbetu_seikyuu_renkei r ")
        sb.AppendLine("        ON t.mise_cd = r.mise_cd ")
        sb.AppendLine("        AND t.bunrui_cd = r.bunrui_cd ")
        sb.AppendLine("        AND t.nyuuryoku_date = r.nyuuryoku_date ")
        sb.AppendLine("        AND t.nyuuryoku_date_no = r.nyuuryoku_date_no ")
        sb.AppendLine("WHERE")
        sb.AppendLine("    r.mise_cd IS NULL ")
        sb.AppendLine("    AND ( ")
        sb.AppendLine("        ( ")
        sb.AppendLine("            t.upd_login_user_id IS NOT NULL ")
        sb.AppendLine("            AND t.upd_datetime IS NOT NULL ")
        sb.AppendLine("            AND t.upd_login_user_id = " & paramUpdLoginUserId & " ")
        sb.AppendLine("            AND t.upd_datetime = " & paramUpdDateTime & "")
        sb.AppendLine("        ) ")
        sb.AppendLine("        OR ( ")
        sb.AppendLine("            t.upd_login_user_id IS NULL ")
        sb.AppendLine("            AND t.upd_datetime IS NULL ")
        sb.AppendLine("            AND t.add_login_user_id = " & paramUpdLoginUserId & " ")
        sb.AppendLine("            AND t.add_datetime = " & paramUpdDateTime & "")
        sb.AppendLine("        )")
        sb.AppendLine("    ) ")
        sb.AppendLine("")


        ' パラメータへ設定
        sqlParams = New SqlParameter() { _
                                        SQLHelper.MakeParam(paramUpdLoginUserId, SqlDbType.VarChar, 30, loginUserId), _
                                        SQLHelper.MakeParam(paramUpdDateTime, SqlDbType.DateTime, 1, updDate) _
                                        }

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    sb.ToString(), _
                                    sqlParams)

        Return intResult

    End Function

    ''' <summary>
    ''' 店別初期請求連携管理テーブルにUPDATE/INSERT
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function setTenbetuSyokiSeikyuuRenkei(ByVal updDate As DateTime, _
                                                 ByVal loginUserId As String) As Integer

        Dim intResult As Integer = 0
        Dim sb As New StringBuilder()
        Dim sqlParams() As SqlClient.SqlParameter

        'SQL文生成
        sb.AppendLine("UPDATE [jhs_sys].[t_tenbetu_syoki_seikyuu_renkei] ")
        sb.AppendLine("SET  ")
        sb.AppendLine("    renkei_siji_cd = 2 ")
        sb.AppendLine("    , sousin_jyky_cd = 0 ")
        sb.AppendLine("    , sousin_kanry_datetime = NULL ")
        sb.AppendLine("    , upd_login_user_id = t.upd_login_user_id ")
        sb.AppendLine("    , upd_datetime = t.upd_datetime ")
        sb.AppendLine(" ")
        sb.AppendLine("FROM")
        sb.AppendLine("    jhs_sys.t_tenbetu_syoki_seikyuu_renkei r ")
        sb.AppendLine("    INNER JOIN jhs_sys.t_tenbetu_syoki_seikyuu t ")
        sb.AppendLine("        ON r.mise_cd = t.mise_cd ")
        sb.AppendLine("        AND r.bunrui_cd = t.bunrui_cd ")
        sb.AppendLine("WHERE")
        sb.AppendLine("    ( ")
        sb.AppendLine("        t.upd_login_user_id IS NOT NULL ")
        sb.AppendLine("        AND t.upd_datetime IS NOT NULL ")
        sb.AppendLine("        AND t.upd_login_user_id = " & paramUpdLoginUserId & " ")
        sb.AppendLine("        AND t.upd_datetime = " & paramUpdDateTime & "")
        sb.AppendLine("    ) ")
        sb.AppendLine("    OR ( ")
        sb.AppendLine("        t.upd_login_user_id IS NULL ")
        sb.AppendLine("        AND t.upd_datetime IS NULL ")
        sb.AppendLine("        AND t.add_login_user_id = " & paramUpdLoginUserId & " ")
        sb.AppendLine("        AND t.add_datetime = " & paramUpdDateTime & "")
        sb.AppendLine("    ) ")
        sb.AppendLine("")

        sb.AppendLine("INSERT ")
        sb.AppendLine("INTO [jhs_sys].[t_tenbetu_syoki_seikyuu_renkei] ( ")
        sb.AppendLine("    mise_cd")
        sb.AppendLine("    , bunrui_cd")
        sb.AppendLine("    , renkei_siji_cd")
        sb.AppendLine("    , sousin_jyky_cd")
        sb.AppendLine("    , sousin_kanry_datetime")
        sb.AppendLine("    , upd_login_user_id")
        sb.AppendLine("    , upd_datetime")
        sb.AppendLine(") ")
        sb.AppendLine("SELECT")
        sb.AppendLine("    t.mise_cd")
        sb.AppendLine("    , t.bunrui_cd")
        sb.AppendLine("    , 1")
        sb.AppendLine("    , 0")
        sb.AppendLine("    , NULL")
        sb.AppendLine("    , t.upd_login_user_id")
        sb.AppendLine("    , t.upd_datetime ")
        sb.AppendLine("FROM")
        sb.AppendLine("    jhs_sys.t_tenbetu_syoki_seikyuu t ")
        sb.AppendLine("    LEFT OUTER JOIN jhs_sys.t_tenbetu_syoki_seikyuu_renkei r ")
        sb.AppendLine("        ON t.mise_cd = r.mise_cd ")
        sb.AppendLine("        AND t.bunrui_cd = r.bunrui_cd ")
        sb.AppendLine("WHERE")
        sb.AppendLine("    r.mise_cd IS NULL ")
        sb.AppendLine("    AND ( ")
        sb.AppendLine("        ( ")
        sb.AppendLine("            t.upd_login_user_id IS NOT NULL ")
        sb.AppendLine("            AND t.upd_datetime IS NOT NULL ")
        sb.AppendLine("            AND t.upd_login_user_id = " & paramUpdLoginUserId & " ")
        sb.AppendLine("            AND t.upd_datetime = " & paramUpdDateTime & "")
        sb.AppendLine("        ) ")
        sb.AppendLine("        OR ( ")
        sb.AppendLine("            t.upd_login_user_id IS NULL ")
        sb.AppendLine("            AND t.upd_datetime IS NULL ")
        sb.AppendLine("            AND t.add_login_user_id = " & paramUpdLoginUserId & " ")
        sb.AppendLine("            AND t.add_datetime = " & paramUpdDateTime & "")
        sb.AppendLine("        )")
        sb.AppendLine("    ) ")
        sb.AppendLine("")


        ' パラメータへ設定
        sqlParams = New SqlParameter() { _
                                        SQLHelper.MakeParam(paramUpdLoginUserId, SqlDbType.VarChar, 30, loginUserId), _
                                        SQLHelper.MakeParam(paramUpdDateTime, SqlDbType.DateTime, 1, updDate) _
                                        }

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    sb.ToString(), _
                                    sqlParams)

        Return intResult

    End Function

#End Region

End Class

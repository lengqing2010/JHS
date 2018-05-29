Option Explicit On
Option Strict On
Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Imports System.Data
Imports System.Text

''' <summary>
''' 支店 月別計画値 ＣＳＶ取込DA
''' </summary>
''' <remarks></remarks>
''' <history>
''' <para>2012/11/23 P-44979 王莎莎 新規作成 </para>
''' </history>
Public Class SitenTukibetuKeikakuchiInputDA

    ''' <summary>
    ''' 画面一覧データ取得処理
    ''' </summary>
    ''' <param name="strKbn">区分</param>
    ''' <returns>アップロード管理テーブルのデータ</returns>
    ''' <remarks>アップロード管理テーブルのデータを取得する</remarks>
    ''' <history>
    ''' <para>2012/11/23 P-44979 王莎莎 新規作成 </para>
    ''' </history>
    Public Function SelTUploadKanri(ByVal strKbn As String) As Data.DataTable
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKbn)

        Dim dsInfo As New Data.DataSet

        Dim sqlBuffer As New System.Text.StringBuilder

        With sqlBuffer
            .AppendLine("SELECT")
            If strKbn = "1" Then
                .AppendLine("      TOP 100")
                .AppendLine("      CONVERT(varchar(100),syori_datetime,111) + ' ' + CONVERT(varchar(100),syori_datetime,108) AS syori_datetime")
                .AppendLine("	 , CONVERT(varchar(100),syori_datetime,112) + REPLACE(CONVERT(varchar(100),syori_datetime,114),':','') AS torikomi_datetime") '取込日時
                .AppendLine("    , nyuuryoku_file_mei")
                .AppendLine("    , CASE error_umu ")
                .AppendLine("        WHEN '1' THEN '有り' ")
                .AppendLine("        WHEN '0' THEN '無し' ")
                .AppendLine("        ELSE '' ")
                .AppendLine("        END AS error_umu")
                .AppendLine("    ,edi_jouhou_sakusei_date ")                'EDI情報作成日
            Else
                .AppendLine("    COUNT(syori_datetime)")
            End If
            .AppendLine("FROM")
            .AppendLine("    t_upload_kanri WITH(READCOMMITTED)")
            .AppendLine("WHERE")
            .AppendLine("    file_kbn = '1'")
            If strKbn = "1" Then
                .AppendLine("ORDER BY")
                .AppendLine("    syori_datetime DESC")
            End If
        End With

        FillDataset(ManagerDA.Connection, CommandType.Text, sqlBuffer.ToString, dsInfo, "tUploadKanri")

        Return dsInfo.Tables(0)

    End Function

    ''' <summary>
    ''' 部署ｺｰﾄﾞ存在チェック
    ''' </summary>
    ''' <param name="strBusyoCd">部署ｺｰﾄﾞ</param>
    ''' <returns>部署ｺｰﾄﾞ存在数</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2012/12/14 P-44979 王莎莎 新規作成 </para>
    ''' </history>
    Public Function SelMBusyoKanri(ByVal strBusyoCd As String) As Integer
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strBusyoCd)

        Dim dsInfo As New Data.DataSet

        Dim sqlBuffer As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With sqlBuffer
            .AppendLine("SELECT")
            .AppendLine("    busyo_cd")                          '部署ｺｰﾄﾞ
            .AppendLine("FROM")
            .AppendLine("    m_busyo_kanri WITH(READCOMMITTED)") '部署管理マスタ
            .AppendLine("WHERE")
            .AppendLine("    busyo_cd = @busyo_cd")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@busyo_cd", SqlDbType.VarChar, 4, strBusyoCd)) '部署コード

        FillDataset(ManagerDA.Connection, CommandType.Text, sqlBuffer.ToString, dsInfo, "busyoCdCount", paramList.ToArray)

        Return dsInfo.Tables(0).Rows.Count

    End Function

    ''' <summary>
    ''' 「登録CSVファイル」のデータが確認済みチェック
    ''' </summary>
    ''' <param name="strKeikakuNendo">計画_年度</param>
    ''' <param name="strBusyoCd">部署ｺｰﾄﾞ</param>
    ''' <returns>データ存在数</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2012/12/14 P-44979 王莎莎 新規作成 </para>
    ''' </history>
    Public Function SelTSitenbetuTukiKeikakuKanriCount(ByVal strKeikakuNendo As String, ByVal strBusyoCd As String) As Integer
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo, strBusyoCd)

        Dim dsInfo As New Data.DataSet

        Dim sqlBuffer As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With sqlBuffer
            .AppendLine("SELECT")
            .AppendLine("    keikaku_nendo")
            .AppendLine("FROM")
            .AppendLine("    t_sitenbetu_tuki_keikaku_kanri WITH(READCOMMITTED)") '支店別月別計画管理テーブル
            .AppendLine("WHERE")
            .AppendLine("    keikaku_nendo = @keikaku_nendo")
            .AppendLine("    AND busyo_cd = @busyo_cd")
            .AppendLine("    AND (kakutei_flg = '1'")
            .AppendLine("    OR keikaku_huhen_flg = '1')")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@keikaku_nendo", SqlDbType.Char, 4, strKeikakuNendo)) '計画_年度
        paramList.Add(MakeParam("@busyo_cd", SqlDbType.VarChar, 4, strBusyoCd)) '部署コード

        FillDataset(ManagerDA.Connection, CommandType.Text, sqlBuffer.ToString, dsInfo, "tSitenbetuTukiKeikakuKanriCount", paramList.ToArray)

        Return dsInfo.Tables(0).Rows.Count

    End Function

    ''' <summary>
    ''' 「支店別月別計画管理表_取込エラー情報テーブル」に挿入する。
    ''' </summary>
    ''' <param name="strSyoriDatetime">処理日時</param>
    ''' <param name="strEdiJouhouSakuseiDate">EDI情報作成日</param>
    ''' <param name="strGyouNo">行No</param>
    ''' <param name="strKbn">区分</param>
    ''' <param name="strUserId">ユーザーID</param>
    ''' <returns>TRUE：成功、FALSE：失敗</returns>
    ''' <remarks>「支店別月別計画管理表_取込エラー情報テーブル」データ挿入処理</remarks>
    ''' <history>
    ''' <para>2012/11/26 P-44979 王莎莎 新規作成 </para>
    ''' </history>
    Public Function InsTSitenTukibetuTorikomiError(ByVal strSyoriDatetime As String, _
                                                   ByVal strEdiJouhouSakuseiDate As String, _
                                                   ByVal strGyouNo As String, _
                                                   ByVal strKbn As String, _
                                                   ByVal strUserId As String) As Boolean
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strSyoriDatetime, strEdiJouhouSakuseiDate, strGyouNo, strKbn, strUserId)

        Dim sqlBuffer As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With sqlBuffer
            .AppendLine("INSERT INTO")
            .AppendLine("    t_siten_tukibetu_torikomi_error WITH(UPDLOCK)")
            .AppendLine("(")
            .AppendLine("      edi_jouhou_sakusei_date")
            .AppendLine("    , gyou_no")
            .AppendLine("    , syori_datetime")
            .AppendLine("    , error_naiyou")
            .AppendLine("    , add_login_user_id")
            .AppendLine("    , add_datetime")
            .AppendLine("    , upd_login_user_id")
            .AppendLine("    , upd_datetime")
            .AppendLine(")")
            .AppendLine("VALUES")
            .AppendLine("(")
            .AppendLine("      @edi_jouhou_sakusei_date")
            .AppendLine("    , @gyou_no")
            .AppendLine("    , @syori_datetime")
            If strKbn = "1" Then
                .AppendLine("    , '該当行の項目数が不正です。'")
            End If
            If strKbn = "2" Then
                .AppendLine("    , '取込対象のCSVで必須項目(計画年度、部署ｺｰﾄﾞ、営業区分)で未入力のものがあります。取込対象のCSV内容をご確認下さい。'")
            End If
            If strKbn = "3" Then
                .AppendLine("    , '取込部署コードが部署管理マスタに存在しません。'")
            End If
            If strKbn = "4" Then
                .AppendLine("    , '営業区分が営業、特販、ＦＣではありません。'")
            End If
            If strKbn = "5" Then
                .AppendLine("    , '営業区分が重複です。'")
            End If
            If strKbn = "6" Then
                .AppendLine("    , '取込対象のCSVに禁則文字が含まれております。取込対象のCSV内容をご確認下さい。'")
            End If
            If strKbn = "7" Then
                .AppendLine("    , '取込対象のCSVに半角数字以外が含まれております。EXCEL内容をご確認の上、CSV出力－取込を 再度実施 下さい。'")
            End If
            If strKbn = "8" Then
                .AppendLine("    , '取込対象のCSVで桁数オーバーの入力がございます。取込対象のCSV内容をご確認下さい。'")
            End If
            .AppendLine("    , @user_id")
            .AppendLine("    , GetDate()")
            .AppendLine("    , @user_id")
            .AppendLine("    , GetDate()")
            .AppendLine(")")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, strEdiJouhouSakuseiDate)) 'EDI情報作成日
        paramList.Add(MakeParam("@gyou_no", SqlDbType.VarChar, 12, strGyouNo)) '行NO
        paramList.Add(MakeParam("@syori_datetime", SqlDbType.DateTime, 20, strSyoriDatetime)) '処理日時
        paramList.Add(MakeParam("@user_id", SqlDbType.VarChar, 30, strUserId)) 'ユーザーID

        '挿入されたデータセットを DB へ書き込み
        If ExecuteNonQuery(ManagerDA.Connection, CommandType.Text, sqlBuffer.ToString(), paramList.ToArray) > 0 Then
            '終了処理
            sqlBuffer = Nothing
            Return True
        Else
            '終了処理
            sqlBuffer = Nothing
            Return False
        End If

    End Function

    ''' <summary>
    ''' 「支店別月別計画管理テーブル」に挿入する。
    ''' </summary>
    ''' <param name="drExcelData">CSVデータ</param>
    ''' <param name="strUserId">ユーザーID</param>
    ''' <returns>TRUE：成功、FALSE：失敗</returns>
    ''' <remarks>「支店別月別計画管理テーブル」データ挿入処理</remarks>
    ''' <history>
    ''' <para>2012/11/26 P-44979 王莎莎 新規作成 </para>
    ''' </history>
    Public Function InsTSitenbetuTukiKeikakuKanri(ByVal drExcelData As DataRow, ByVal strUserId As String) As Boolean
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, drExcelData, strUserId)

        Dim sqlBuffer As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With sqlBuffer
            .AppendLine("INSERT INTO")
            .AppendLine("    t_sitenbetu_tuki_keikaku_kanri WITH(UPDLOCK)") '支店別月別計画管理テーブル
            .AppendLine("(")
            .AppendLine("      [keikaku_nendo]")              '計画_年度
            .AppendLine("    , [busyo_cd]")                   '部署ｺｰﾄﾞ
            .AppendLine("    , [add_datetime]")               '登録日時
            .AppendLine("    , [siten_mei]")                  '支店名
            .AppendLine("    , [eigyou_cd]")                  '営業所
            .AppendLine("    , [eigyou_mei]")                 '営業所名
            .AppendLine("    , [eigyou_kbn]")                 '営業区分
            .AppendLine("    , [4gatu_keikaku_kensuu]")       '4月_計画件数
            .AppendLine("    , [4gatu_keikaku_kingaku]")      '4月_計画金額
            .AppendLine("    , [4gatu_keikaku_arari]")        '4月_計画粗利
            .AppendLine("    , [5gatu_keikaku_kensuu]")       '5月_計画件数
            .AppendLine("    , [5gatu_keikaku_kingaku]")      '5月_計画金額
            .AppendLine("    , [5gatu_keikaku_arari]")        '5月_計画粗利
            .AppendLine("    , [6gatu_keikaku_kensuu]")       '6月_計画件数
            .AppendLine("    , [6gatu_keikaku_kingaku]")      '6月_計画金額
            .AppendLine("    , [6gatu_keikaku_arari]")        '6月_計画粗利
            .AppendLine("    , [7gatu_keikaku_kensuu]")       '7月_計画件数
            .AppendLine("    , [7gatu_keikaku_kingaku]")      '7月_計画金額
            .AppendLine("    , [7gatu_keikaku_arari]")        '7月_計画粗利
            .AppendLine("    , [8gatu_keikaku_kensuu]")       '8月_計画件数
            .AppendLine("    , [8gatu_keikaku_kingaku]")      '8月_計画金額
            .AppendLine("    , [8gatu_keikaku_arari]")        '8月_計画粗利
            .AppendLine("    , [9gatu_keikaku_kensuu]")       '9月_計画件数
            .AppendLine("    , [9gatu_keikaku_kingaku]")      '9月_計画金額
            .AppendLine("    , [9gatu_keikaku_arari]")        '9月_計画粗利
            .AppendLine("    , [10gatu_keikaku_kensuu]")      '10月_計画件数
            .AppendLine("    , [10gatu_keikaku_kingaku]")     '10月_計画金額
            .AppendLine("    , [10gatu_keikaku_arari]")       '10月_計画粗利
            .AppendLine("    , [11gatu_keikaku_kensuu]")      '11月_計画件数
            .AppendLine("    , [11gatu_keikaku_kingaku]")     '11月_計画金額
            .AppendLine("    , [11gatu_keikaku_arari]")       '11月_計画粗利
            .AppendLine("    , [12gatu_keikaku_kensuu]")      '12月_計画件数
            .AppendLine("    , [12gatu_keikaku_kingaku]")     '12月_計画金額
            .AppendLine("    , [12gatu_keikaku_arari]")       '12月_計画粗利
            .AppendLine("    , [1gatu_keikaku_kensuu]")       '1月_計画件数
            .AppendLine("    , [1gatu_keikaku_kingaku]")      '1月_計画金額
            .AppendLine("    , [1gatu_keikaku_arari]")        '1月_計画粗利
            .AppendLine("    , [2gatu_keikaku_kensuu]")       '2月_計画件数
            .AppendLine("    , [2gatu_keikaku_kingaku]")      '2月_計画金額
            .AppendLine("    , [2gatu_keikaku_arari]")        '2月_計画粗利
            .AppendLine("    , [3gatu_keikaku_kensuu]")       '3月_計画件数
            .AppendLine("    , [3gatu_keikaku_kingaku]")      '3月_計画金額
            .AppendLine("    , [3gatu_keikaku_arari]")        '3月_計画粗利
            .AppendLine("    , [keikaku_henkou_flg]")         '計画変更FLG
            .AppendLine("    , [keikaku_settutei_kome]")      '計画設定時ｺﾒﾝﾄ
            .AppendLine("    , [kakutei_flg]")                '確定FLG
            .AppendLine("    , [keikaku_huhen_flg]")          '計画値不変FLG
            .AppendLine("    , [add_login_user_id]")          '登録ログインユーザーID
            .AppendLine("    , [upd_login_user_id]")          '更新ログインユーザーID
            .AppendLine("    , [upd_datetime]")               '更新日時
            .AppendLine(")")
            .AppendLine("VALUES")
            .AppendLine("(")
            .AppendLine("      @keikaku_nendo")
            .AppendLine("    , @busyo_cd")
            .AppendLine("    , GetDate()")
            .AppendLine("    , @siten_mei")
            .AppendLine("    , NULL")
            .AppendLine("    , NULL")
            .AppendLine("    , @eigyou_kbn")
            .AppendLine("    , @4gatu_keikaku_kensuu")
            .AppendLine("    , @4gatu_keikaku_kingaku")
            .AppendLine("    , @4gatu_keikaku_arari")
            .AppendLine("    , @5gatu_keikaku_kensuu")
            .AppendLine("    , @5gatu_keikaku_kingaku")
            .AppendLine("    , @5gatu_keikaku_arari")
            .AppendLine("    , @6gatu_keikaku_kensuu")
            .AppendLine("    , @6gatu_keikaku_kingaku")
            .AppendLine("    , @6gatu_keikaku_arari")
            .AppendLine("    , @7gatu_keikaku_kensuu")
            .AppendLine("    , @7gatu_keikaku_kingaku")
            .AppendLine("    , @7gatu_keikaku_arari")
            .AppendLine("    , @8gatu_keikaku_kensuu")
            .AppendLine("    , @8gatu_keikaku_kingaku")
            .AppendLine("    , @8gatu_keikaku_arari")
            .AppendLine("    , @9gatu_keikaku_kensuu")
            .AppendLine("    , @9gatu_keikaku_kingaku")
            .AppendLine("    , @9gatu_keikaku_arari")
            .AppendLine("    , @10gatu_keikaku_kensuu")
            .AppendLine("    , @10gatu_keikaku_kingaku")
            .AppendLine("    , @10gatu_keikaku_arari")
            .AppendLine("    , @11gatu_keikaku_kensuu")
            .AppendLine("    , @11gatu_keikaku_kingaku")
            .AppendLine("    , @11gatu_keikaku_arari")
            .AppendLine("    , @12gatu_keikaku_kensuu")
            .AppendLine("    , @12gatu_keikaku_kingaku")
            .AppendLine("    , @12gatu_keikaku_arari")
            .AppendLine("    , @1gatu_keikaku_kensuu")
            .AppendLine("    , @1gatu_keikaku_kingaku")
            .AppendLine("    , @1gatu_keikaku_arari")
            .AppendLine("    , @2gatu_keikaku_kensuu")
            .AppendLine("    , @2gatu_keikaku_kingaku")
            .AppendLine("    , @2gatu_keikaku_arari")
            .AppendLine("    , @3gatu_keikaku_kensuu")
            .AppendLine("    , @3gatu_keikaku_kingaku")
            .AppendLine("    , @3gatu_keikaku_arari")
            .AppendLine("    , NULL")
            .AppendLine("    , NULL")
            .AppendLine("    , NULL")
            .AppendLine("    , NULL")
            .AppendLine("    , @user_id")
            .AppendLine("    , @user_id")
            .AppendLine("    , GetDate()")
            .AppendLine(")")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@keikaku_nendo", SqlDbType.Char, 4, drExcelData.Item("keikaku_nendo").ToString.Trim)) '計画_年度
        paramList.Add(MakeParam("@busyo_cd", SqlDbType.VarChar, 4, drExcelData.Item("busyo_cd").ToString.Trim)) '部署ｺｰﾄﾞ
        paramList.Add(MakeParam("@siten_mei", SqlDbType.VarChar, 40, drExcelData.Item("siten_mei").ToString.Trim)) '支店名
        paramList.Add(MakeParam("@eigyou_kbn", SqlDbType.VarChar, 5, drExcelData.Item("eigyou_kbn").ToString.Trim)) '営業区分

        paramList.Add(MakeParam("@4gatu_keikaku_kensuu", SqlDbType.BigInt, 12, IIf(drExcelData.Item("4gatu_keikaku_kensuu").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("4gatu_keikaku_kensuu").ToString.Trim))) '4月_計画件数
        paramList.Add(MakeParam("@4gatu_keikaku_kingaku", SqlDbType.BigInt, 12, IIf(drExcelData.Item("4gatu_keikaku_kingaku").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("4gatu_keikaku_kingaku").ToString.Trim))) '4月_計画金額
        paramList.Add(MakeParam("@4gatu_keikaku_arari", SqlDbType.BigInt, 12, IIf(drExcelData.Item("4gatu_keikaku_arari").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("4gatu_keikaku_arari").ToString.Trim))) '4月_計画粗利

        paramList.Add(MakeParam("@5gatu_keikaku_kensuu", SqlDbType.BigInt, 12, IIf(drExcelData.Item("5gatu_keikaku_kensuu").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("5gatu_keikaku_kensuu").ToString.Trim))) '5月_計画件数
        paramList.Add(MakeParam("@5gatu_keikaku_kingaku", SqlDbType.BigInt, 12, IIf(drExcelData.Item("5gatu_keikaku_kingaku").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("5gatu_keikaku_kingaku").ToString.Trim))) '5月_計画金額
        paramList.Add(MakeParam("@5gatu_keikaku_arari", SqlDbType.BigInt, 12, IIf(drExcelData.Item("5gatu_keikaku_arari").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("5gatu_keikaku_arari").ToString.Trim))) '5月_計画粗利

        paramList.Add(MakeParam("@6gatu_keikaku_kensuu", SqlDbType.BigInt, 12, IIf(drExcelData.Item("6gatu_keikaku_kensuu").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("6gatu_keikaku_kensuu").ToString.Trim))) '6月_計画件数
        paramList.Add(MakeParam("@6gatu_keikaku_kingaku", SqlDbType.BigInt, 12, IIf(drExcelData.Item("6gatu_keikaku_kingaku").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("6gatu_keikaku_kingaku").ToString.Trim))) '6月_計画金額
        paramList.Add(MakeParam("@6gatu_keikaku_arari", SqlDbType.BigInt, 12, IIf(drExcelData.Item("6gatu_keikaku_arari").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("6gatu_keikaku_arari").ToString.Trim))) '6月_計画粗利

        paramList.Add(MakeParam("@7gatu_keikaku_kensuu", SqlDbType.BigInt, 12, IIf(drExcelData.Item("7gatu_keikaku_kensuu").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("7gatu_keikaku_kensuu").ToString.Trim))) '7月_計画件数
        paramList.Add(MakeParam("@7gatu_keikaku_kingaku", SqlDbType.BigInt, 12, IIf(drExcelData.Item("7gatu_keikaku_kingaku").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("7gatu_keikaku_kingaku").ToString.Trim))) '7月_計画金額
        paramList.Add(MakeParam("@7gatu_keikaku_arari", SqlDbType.BigInt, 12, IIf(drExcelData.Item("7gatu_keikaku_arari").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("7gatu_keikaku_arari").ToString.Trim))) '7月_計画粗利

        paramList.Add(MakeParam("@8gatu_keikaku_kensuu", SqlDbType.BigInt, 12, IIf(drExcelData.Item("8gatu_keikaku_kensuu").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("8gatu_keikaku_kensuu").ToString.Trim))) '8月_計画件数
        paramList.Add(MakeParam("@8gatu_keikaku_kingaku", SqlDbType.BigInt, 12, IIf(drExcelData.Item("8gatu_keikaku_kingaku").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("8gatu_keikaku_kingaku").ToString.Trim))) '8月_計画金額
        paramList.Add(MakeParam("@8gatu_keikaku_arari", SqlDbType.BigInt, 12, IIf(drExcelData.Item("8gatu_keikaku_arari").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("8gatu_keikaku_arari").ToString.Trim))) '8月_計画粗利

        paramList.Add(MakeParam("@9gatu_keikaku_kensuu", SqlDbType.BigInt, 12, IIf(drExcelData.Item("9gatu_keikaku_kensuu").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("9gatu_keikaku_kensuu").ToString.Trim))) '9月_計画件数
        paramList.Add(MakeParam("@9gatu_keikaku_kingaku", SqlDbType.BigInt, 12, IIf(drExcelData.Item("9gatu_keikaku_kingaku").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("9gatu_keikaku_kingaku").ToString.Trim))) '9月_計画金額
        paramList.Add(MakeParam("@9gatu_keikaku_arari", SqlDbType.BigInt, 12, IIf(drExcelData.Item("9gatu_keikaku_arari").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("9gatu_keikaku_arari").ToString.Trim))) '9月_計画粗利

        paramList.Add(MakeParam("@10gatu_keikaku_kensuu", SqlDbType.BigInt, 12, IIf(drExcelData.Item("10gatu_keikaku_kensuu").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("10gatu_keikaku_kensuu").ToString.Trim))) '10月_計画件数
        paramList.Add(MakeParam("@10gatu_keikaku_kingaku", SqlDbType.BigInt, 12, IIf(drExcelData.Item("10gatu_keikaku_kingaku").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("10gatu_keikaku_kingaku").ToString.Trim))) '10月_計画金額
        paramList.Add(MakeParam("@10gatu_keikaku_arari", SqlDbType.BigInt, 12, IIf(drExcelData.Item("10gatu_keikaku_arari").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("10gatu_keikaku_arari").ToString.Trim))) '10月_計画粗利

        paramList.Add(MakeParam("@11gatu_keikaku_kensuu", SqlDbType.BigInt, 12, IIf(drExcelData.Item("11gatu_keikaku_kensuu").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("11gatu_keikaku_kensuu").ToString.Trim))) '11月_計画件数
        paramList.Add(MakeParam("@11gatu_keikaku_kingaku", SqlDbType.BigInt, 12, IIf(drExcelData.Item("11gatu_keikaku_kingaku").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("11gatu_keikaku_kingaku").ToString.Trim))) '11月_計画金額
        paramList.Add(MakeParam("@11gatu_keikaku_arari", SqlDbType.BigInt, 12, IIf(drExcelData.Item("11gatu_keikaku_arari").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("11gatu_keikaku_arari").ToString.Trim))) '11月_計画粗利

        paramList.Add(MakeParam("@12gatu_keikaku_kensuu", SqlDbType.BigInt, 12, IIf(drExcelData.Item("12gatu_keikaku_kensuu").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("12gatu_keikaku_kensuu").ToString.Trim))) '12月_計画件数
        paramList.Add(MakeParam("@12gatu_keikaku_kingaku", SqlDbType.BigInt, 12, IIf(drExcelData.Item("12gatu_keikaku_kingaku").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("12gatu_keikaku_kingaku").ToString.Trim))) '12月_計画金額
        paramList.Add(MakeParam("@12gatu_keikaku_arari", SqlDbType.BigInt, 12, IIf(drExcelData.Item("12gatu_keikaku_arari").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("12gatu_keikaku_arari").ToString.Trim))) '12月_計画粗利

        paramList.Add(MakeParam("@1gatu_keikaku_kensuu", SqlDbType.BigInt, 12, IIf(drExcelData.Item("1gatu_keikaku_kensuu").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("1gatu_keikaku_kensuu").ToString.Trim))) '1月_計画件数
        paramList.Add(MakeParam("@1gatu_keikaku_kingaku", SqlDbType.BigInt, 12, IIf(drExcelData.Item("1gatu_keikaku_kingaku").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("1gatu_keikaku_kingaku").ToString.Trim))) '1月_計画金額
        paramList.Add(MakeParam("@1gatu_keikaku_arari", SqlDbType.BigInt, 12, IIf(drExcelData.Item("1gatu_keikaku_arari").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("1gatu_keikaku_arari").ToString.Trim))) '1月_計画粗利

        paramList.Add(MakeParam("@2gatu_keikaku_kensuu", SqlDbType.BigInt, 12, IIf(drExcelData.Item("2gatu_keikaku_kensuu").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("2gatu_keikaku_kensuu").ToString.Trim))) '2月_計画件数
        paramList.Add(MakeParam("@2gatu_keikaku_kingaku", SqlDbType.BigInt, 12, IIf(drExcelData.Item("2gatu_keikaku_kingaku").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("2gatu_keikaku_kingaku").ToString.Trim))) '2月_計画金額
        paramList.Add(MakeParam("@2gatu_keikaku_arari", SqlDbType.BigInt, 12, IIf(drExcelData.Item("2gatu_keikaku_arari").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("2gatu_keikaku_arari").ToString.Trim))) '2月_計画粗利

        paramList.Add(MakeParam("@3gatu_keikaku_kensuu", SqlDbType.BigInt, 12, IIf(drExcelData.Item("3gatu_keikaku_kensuu").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("3gatu_keikaku_kensuu").ToString.Trim))) '3月_計画件数
        paramList.Add(MakeParam("@3gatu_keikaku_kingaku", SqlDbType.BigInt, 12, IIf(drExcelData.Item("3gatu_keikaku_kingaku").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("3gatu_keikaku_kingaku").ToString.Trim))) '3月_計画金額
        paramList.Add(MakeParam("@3gatu_keikaku_arari", SqlDbType.BigInt, 12, IIf(drExcelData.Item("3gatu_keikaku_arari").ToString.Trim = String.Empty, DBNull.Value, drExcelData.Item("3gatu_keikaku_arari").ToString.Trim))) '3月_計画粗利

        paramList.Add(MakeParam("@user_id", SqlDbType.VarChar, 30, strUserId)) 'ユーザーID

        '挿入されたデータセットを DB へ書き込み
        If ExecuteNonQuery(ManagerDA.Connection, CommandType.Text, sqlBuffer.ToString(), paramList.ToArray) > 0 Then
            '終了処理
            sqlBuffer = Nothing
            Return True
        Else
            '終了処理
            sqlBuffer = Nothing
            Return False
        End If

    End Function

    ''' <summary>
    ''' 「アップロード管理テーブル」に挿入する。
    ''' </summary>
    ''' <param name="strSyoriDatetime">処理日時</param>
    ''' <param name="strEdiJouhouSakuseiDate">EDI情報作成日</param>
    ''' <param name="strErrorUmu">エラー有無</param>
    ''' <param name="strNyuuryokuFileMei">入力ファイル名</param>
    ''' <param name="strUserId">ユーザーID</param>
    ''' <returns>TRUE：成功、FALSE：失敗</returns>
    ''' <remarks>「アップロード管理テーブル」データ挿入処理</remarks>
    ''' <history>
    ''' <para>2012/11/26 P-44979 王莎莎 新規作成 </para>
    ''' </history>
    Public Function InsTUploadKanri(ByVal strSyoriDatetime As String, _
                                    ByVal strEdiJouhouSakuseiDate As String, _
                                    ByVal strErrorUmu As String, _
                                    ByVal strNyuuryokuFileMei As String, _
                                    ByVal strUserId As String) As Boolean
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strSyoriDatetime, strEdiJouhouSakuseiDate, strNyuuryokuFileMei, strUserId)

        Dim sqlBuffer As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With sqlBuffer
            .AppendLine("INSERT INTO")
            .AppendLine("    t_upload_kanri WITH(UPDLOCK)")
            .AppendLine("(")
            .AppendLine("      syori_datetime")
            .AppendLine("    , nyuuryoku_file_mei")
            .AppendLine("    , edi_jouhou_sakusei_date")
            .AppendLine("    , error_umu")
            .AppendLine("    , file_kbn")
            .AppendLine("    , add_login_user_id")
            .AppendLine("    , add_datetime")
            .AppendLine("    , upd_login_user_id")
            .AppendLine("    , upd_datetime")
            .AppendLine(")")
            .AppendLine("VALUES")
            .AppendLine("(")
            .AppendLine("      @syori_datetime")
            .AppendLine("    , @nyuuryoku_file_mei")
            .AppendLine("    , @edi_jouhou_sakusei_date")
            .AppendLine("    , @error_umu")
            .AppendLine("    , 1")
            .AppendLine("    , @user_id")
            .AppendLine("    , GetDate()")
            .AppendLine("    , @user_id")
            .AppendLine("    , GetDate()")
            .AppendLine(")")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@syori_datetime", SqlDbType.DateTime, 20, strSyoriDatetime)) '処理日時
        paramList.Add(MakeParam("@error_umu", SqlDbType.VarChar, 1, strErrorUmu)) 'エラー有無
        paramList.Add(MakeParam("@nyuuryoku_file_mei", SqlDbType.VarChar, 128, strNyuuryokuFileMei)) '入力ファイル名
        paramList.Add(MakeParam("@edi_jouhou_sakusei_date", SqlDbType.VarChar, 40, strEdiJouhouSakuseiDate)) 'EDI情報作成日
        paramList.Add(MakeParam("@user_id", SqlDbType.VarChar, 30, strUserId)) 'ユーザーID


        '挿入されたデータセットを DB へ書き込み
        If ExecuteNonQuery(ManagerDA.Connection, CommandType.Text, sqlBuffer.ToString(), paramList.ToArray) > 0 Then
            '終了処理
            sqlBuffer = Nothing
            Return True
        Else
            '終了処理
            sqlBuffer = Nothing
            Return False
        End If

    End Function

End Class

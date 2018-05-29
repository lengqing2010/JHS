Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Imports Itis.Earth.DataAccess
Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' 名称テーブル情報の取得用クラスです
''' </summary>
''' <remarks>本クラスで名称情報を取得する場合は事前に名称種別プロパティに情報を設定してください</remarks>
Public Class MeisyouDataAccess
    Inherits AbsDataAccess

    Public Enum MeisyouType
        ''' <summary>
        ''' 01：商品区分（地盤データ・商品価格設定マスタ）
        ''' </summary>
        ''' <remarks></remarks>
        SYOUHIN_KBN = 1
        ''' <summary>
        ''' 02：調査概要（地盤データ・商品価格設定マスタ）
        ''' </summary>
        ''' <remarks></remarks>
        TYOUSA_GAIYOU = 2
        ''' <summary>
        ''' 03：価格設定場所（地盤データ・商品価格設定マスタ）
        ''' </summary>
        ''' <remarks></remarks>
        KAKAKU_SETTEI = 3
        ''' <summary>
        ''' 04：保険申請区分（地盤データ・商品価格設定マスタ）
        ''' </summary>
        ''' <remarks></remarks>
        HOKEN_SINSEI = 4
        ''' <summary>
        ''' 05：入金確認条件名（加盟店マスタ・地盤データ）
        ''' </summary>
        ''' <remarks></remarks>
        NYUUKIN_KAKUNIN = 5
        ''' <summary>
        ''' 06：経由名（地盤データ）
        ''' </summary>
        ''' <remarks></remarks>
        KEIYU_MEI = 6
        ''' <summary>
        ''' 07：発注書FLG（加盟店マスタ）
        ''' </summary>
        ''' <remarks></remarks>
        HATTYUUSYO_FLG = 7
        ''' <summary>
        ''' 08：調査見積書FLG（加盟店マスタ）
        ''' </summary>
        ''' <remarks></remarks>
        TYS_MITUMORI_FLG = 8
        ''' <summary>
        ''' 09：加盟店備考（加盟店備考設定マスタ）
        ''' </summary>
        ''' <remarks></remarks>
        KAMEITEN_BIKO = 9
        ''' <summary>
        ''' 10：加盟店注意事項（加盟店注意事項設定マスタ）
        ''' </summary>
        ''' <remarks></remarks>
        KAMEITEN_TYUUIJIKOU = 10
        ''' <summary>
        ''' 11：手形比率名
        ''' </summary>
        ''' <remarks></remarks>
        TEGATA_HIRITU = 11
        ''' <summary>
        ''' 12：協力会費比率名
        ''' </summary>
        ''' <remarks></remarks>
        KYOURYOKU_KAIHI = 12

        ''' <summary>
        ''' 110：指定請求書有無
        ''' shitei_seikyuusyo_umu
        ''' </summary>
        ''' <remarks></remarks>
        SHITEI_SEIKYUUSYO_UMU = 110

    End Enum

    ''' <summary>
    ''' 名称種別（未指定時は全件取得）
    ''' </summary>
    ''' <remarks></remarks>
    Private meisyou_syubetu As String = "%"

    ' コネクションストリングを取得
    Private connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>
    ''' ｺｰﾄﾞを引数に名称を取得します
    ''' </summary>
    ''' <param name="type">取得する名称種別</param>
    ''' <param name="code">keyとなるコード</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getMeisyou(ByVal type As MeisyouType, _
                                ByVal code As Integer, _
                              ByRef name As String) As Boolean

        ' パラメータ
        Const paramCode As String = "@CODE"
        Const paramSyubetu As String = "@SYUBETU"

        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append("SELECT meisyou_syubetu,code,meisyou,hyouji_jyun ")
        commandTextSb.Append("  FROM m_meisyou ")
        commandTextSb.Append("  WHERE meisyou_syubetu like " & paramSyubetu)
        commandTextSb.Append("  AND   code     = " & paramCode)
        commandTextSb.Append("  ORDER BY hyouji_jyun ")

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(paramSyubetu, SqlDbType.Char, 2, type.ToString("00")), _
             SQLHelper.MakeParam(paramCode, SqlDbType.Int, 1, code)}

        ' データの取得
        Dim MeisyouDataSet As New MeisyouDataSet()

        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            MeisyouDataSet, MeisyouDataSet.MeisyouTable.TableName, commandParameters)

        Dim MeisyouTable As MeisyouDataSet.MeisyouTableDataTable = MeisyouDataSet.MeisyouTable

        If MeisyouTable.Count = 0 Then
            Debug.WriteLine("取得出来ませんでした")
            Return False
        Else
            Dim row As MeisyouDataSet.MeisyouTableRow = MeisyouTable(0)
            name = row.meisyou
        End If

        Return True

    End Function

    ''' <summary>
    ''' コンボボックス設定用の有効な名称テーブルレコードを全て取得します
    ''' </summary>
    ''' <param name="type">取得する名称種別</param>
    ''' <param name="dt">データテーブル</param>
    ''' <param name="withSpaceRow" >先頭に空白行をセットする場合:true</param>
    ''' <param name="withCode" >（任意）ValueにKey項目を付加する場合:true " 1:aaaa "</param>
    ''' <remarks></remarks>
    Public Overloads Sub GetDropdownData(ByVal type As MeisyouType, _
                                         ByRef dt As DataTable, _
                                         ByVal withSpaceRow As Boolean, _
                                         Optional ByVal withCode As Boolean = True)

        ' 商品種別の指定
        meisyou_syubetu = type.ToString("00")

        ' 共通のコンボデータ設定メソッドを使用
        GetDropdownData(dt, withSpaceRow, withCode)

    End Sub

    ''' <summary>
    ''' コンボボックス設定用の有効な名称テーブルレコードを全て取得します<br/>
    ''' 本メソッドを直接実行した場合、コード９以外の全てのレコードが取得されます
    ''' </summary>
    ''' <param name="dt" >データテーブル</param>
    ''' <param name="withSpaceRow" >先頭に空白行をセットする場合:true</param>
    ''' <param name="withCode" >（任意）ValueにKey項目を付加する場合:true " 1:aaaa "</param>
    ''' <remarks></remarks>
    Public Overrides Sub GetDropdownData(ByRef dt As DataTable, _
                                         ByVal withSpaceRow As Boolean, _
                                         Optional ByVal withCode As Boolean = True)

        Dim connStr As String = ConnectionManager.Instance.ConnectionString
        Dim commandTextSb As New StringBuilder()
        Dim row As MeisyouDataSet.MeisyouTableRow

        Const paramSyubetu As String = "@SYUBETU"

        commandTextSb.Append("SELECT code,meisyou ")
        commandTextSb.Append("  FROM m_meisyou ")
        commandTextSb.Append("  WHERE meisyou_syubetu = " & paramSyubetu)
        commandTextSb.Append("  AND   code     <> 9 ")
        commandTextSb.Append("  ORDER BY hyouji_jyun ")

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(paramSyubetu, SqlDbType.Char, 2, meisyou_syubetu)}

        ' データの取得
        Dim MeisyouDataSet As New MeisyouDataSet()
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            MeisyouDataSet, MeisyouDataSet.MeisyouTable.TableName, commandParameters)

        Dim MeisyouDataTable As MeisyouDataSet.MeisyouTableDataTable = _
                    MeisyouDataSet.MeisyouTable

        If MeisyouDataTable.Count <> 0 Then

            ' 空白行データの設定
            If withSpaceRow = True Then
                dt.Rows.Add(CreateRow("", "", dt))
            End If

            ' 取得データよりDataRowを作成しDataTableにセットする
            For Each row In MeisyouDataTable
                If withCode = True Then
                    dt.Rows.Add(CreateRow(row.code.ToString + ":" + row.meisyou, row.code, dt))
                Else
                    dt.Rows.Add(CreateRow(row.meisyou, row.code, dt))
                End If
            Next

        End If


    End Sub

End Class

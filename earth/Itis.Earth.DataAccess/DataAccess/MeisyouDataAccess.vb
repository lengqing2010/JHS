Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' 名称テーブル情報の取得用クラスです
''' </summary>
''' <remarks>本クラスで名称情報を取得する場合はインスタンス時に名称種別プロパティに情報を設定してください</remarks>
Public Class MeisyouDataAccess
    Inherits AbsDataAccess

    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ''' <summary>
    ''' コンストラクタ
    ''' <newpara>※名称種別を指定すること</newpara>
    ''' </summary>
    ''' <param name="intVal">EarthConst.emMeisyouTypeを使用すること</param>
    ''' <remarks>※EarthConst値を指定すること</remarks>
    Public Sub New(ByVal intVal As Integer)
        ' 除外データの有無判定
        Select Case intVal
            Case 1
            Case 2
                Me.strJogaiFlg = 1
            Case Else
                Me.strJogaiFlg = 0
        End Select

        ' 名称種別の設定(Stringで2桁にフォーマット)
        Me.strMeisyou_Syubetu = CStr(Format(intVal, "00"))
    End Sub

    ''' <summary>
    ''' 名称種別が以下に該当する場合、コード値が「9」のデータは除外する
    ''' [該当名称種別]
    ''' 01：商品区分（地盤データ・商品価格設定マスタ）
    ''' 02：調査概要（地盤データ・商品価格設定マスタ）
    ''' </summary>
    ''' <remarks></remarks>
    Private strJogaiFlg As Integer = 0 'デフォルト値(=0)


    'Public Enum MeisyouType
    '    ''' <summary>
    '    ''' 01：商品区分（地盤データ・商品価格設定マスタ）
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    SYOUHIN_KBN = 1
    '    ''' <summary>
    '    ''' 02：調査概要（地盤データ・商品価格設定マスタ）
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    TYOUSA_GAIYOU = 2
    '    ''' <summary>
    '    ''' 03：価格設定場所（地盤データ・商品価格設定マスタ）
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    KAKAKU_SETTEI = 3
    '    ''' <summary>
    '    ''' 04：保険申請区分（地盤データ・商品価格設定マスタ）
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    HOKEN_SINSEI = 4
    '    ''' <summary>
    '    ''' 05：入金確認条件名（加盟店マスタ・地盤データ）
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    NYUUKIN_KAKUNIN = 5
    '    ''' <summary>
    '    ''' 06：経由名（地盤データ）
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    KEIYU_MEI = 6
    '    ''' <summary>
    '    ''' 07：発注書FLG（加盟店マスタ）
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    HATTYUUSYO_FLG = 7
    '    ''' <summary>
    '    ''' 08：調査見積書FLG（加盟店マスタ）
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    TYS_MITUMORI_FLG = 8
    '    ''' <summary>
    '    ''' 09：加盟店備考（加盟店備考設定マスタ）
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    KAMEITEN_BIKO = 9
    '    ''' <summary>
    '    ''' 10：加盟店注意事項（加盟店注意事項設定マスタ）
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    KAMEITEN_TYUUIJIKOU = 10
    '    ''' <summary>
    '    ''' 11：手形比率名
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    TEGATA_HIRITU = 11
    '    ''' <summary>
    '    ''' 12：協力会費比率名
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    KYOURYOKU_KAIHI = 12

    'End Enum

    '''' <summary>
    '''' 名称種別（未指定時は全件取得）
    '''' </summary>
    '''' <remarks></remarks>
    'Private meisyou_syubetu As String = "%"
    Private strMeisyou_Syubetu As String = ""

    ' コネクションストリングを取得
    Private connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>
    ''' ｺｰﾄﾞを引数に名称を取得します
    ''' </summary>
    ''' <param name="type">取得する名称種別</param>
    ''' <param name="code">keyとなるコード</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getMeisyou(ByVal type As String, _
                                ByVal code As Integer, _
                              ByRef name As String) As Boolean

        ' パラメータ
        Const paramCode As String = "@CODE"
        Const paramSyubetu As String = "@SYUBETU"

        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append("SELECT meisyou_syubetu,code,meisyou,hyouji_jyun ")
        commandTextSb.Append("  FROM m_meisyou WITH (READCOMMITTED) ")
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
    Public Overloads Sub GetDropdownData(ByVal type As String, _
                                         ByRef dt As DataTable, _
                                         ByVal withSpaceRow As Boolean, _
                                         Optional ByVal withCode As Boolean = True)

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetDropdownData", _
                                            type, _
                                            dt, _
                                            withSpaceRow, _
                                            withCode)

        ' 商品種別の指定
        strMeisyou_Syubetu = type.ToString("00")

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
                                         Optional ByVal withCode As Boolean = True, _
                                         Optional ByVal blnTorikesi As Boolean = True)

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetDropdownData", dt, withSpaceRow, withCode)

        Dim connStr As String = ConnectionManager.Instance.ConnectionString
        Dim commandTextSb As New StringBuilder()
        Dim row As MeisyouDataSet.MeisyouTable2Row

        Const paramSyubetu As String = "@SYUBETU"

        commandTextSb.Append("SELECT code,meisyou ")
        commandTextSb.Append("  FROM m_meisyou WITH (READCOMMITTED) ")
        commandTextSb.Append("  WHERE meisyou_syubetu = " & paramSyubetu)
        If Me.strJogaiFlg <> 0 Then
            commandTextSb.Append("  AND   code     <> 9 ")
        End If
        commandTextSb.Append("  ORDER BY hyouji_jyun ")

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(paramSyubetu, SqlDbType.Char, 2, strMeisyou_Syubetu)}

        ' データの取得
        Dim MeisyouDataSet As New MeisyouDataSet()
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            MeisyouDataSet, MeisyouDataSet.MeisyouTable2.TableName, commandParameters)

        Dim MeisyouDataTable As MeisyouDataSet.MeisyouTable2DataTable = _
                    MeisyouDataSet.MeisyouTable2

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

    ''' <summary>
    ''' コンボボックス設定用の有効な名称テーブルレコードを全て取得します(※名称種別専用)<br/>
    ''' 本メソッドを直接実行した場合、コード９以外の全てのレコードが取得されます
    ''' </summary>
    ''' <param name="dt" >データテーブル</param>
    ''' <param name="withSpaceRow" >先頭に空白行をセットする場合:true</param>
    ''' <param name="withCode" >（任意）ValueにKey項目を付加する場合:true " 1:aaaa "</param>
    ''' <remarks></remarks>
    Public Overrides Sub GetMeisyouDropdownData(ByRef dt As DataTable, _
                                         ByVal withSpaceRow As Boolean, _
                                         Optional ByVal withCode As Boolean = True, _
                                         Optional ByVal blnTorikesi As Boolean = True)

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetMeisyouDropdownData", dt, withSpaceRow, withCode)

        Dim connStr As String = ConnectionManager.Instance.ConnectionString
        Dim commandTextSb As New StringBuilder()
        Dim row As MeisyouDataSet.MeisyouTable2Row

        Const paramSyubetu As String = "@SYUBETU"

        commandTextSb.Append("SELECT code,meisyou ")
        commandTextSb.Append("  FROM m_meisyou WITH (READCOMMITTED) ")
        commandTextSb.Append("  WHERE meisyou_syubetu = " & paramSyubetu)
        If Me.strJogaiFlg <> 0 Then
            commandTextSb.Append("  AND   code     <> 9 ")
        End If
        commandTextSb.Append("  ORDER BY hyouji_jyun ")

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(paramSyubetu, SqlDbType.Char, 2, strMeisyou_Syubetu)}

        ' データの取得
        Dim MeisyouDataSet As New MeisyouDataSet()
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            MeisyouDataSet, MeisyouDataSet.MeisyouTable2.TableName, commandParameters)

        Dim MeisyouDataTable As MeisyouDataSet.MeisyouTable2DataTable = _
                    MeisyouDataSet.MeisyouTable2

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

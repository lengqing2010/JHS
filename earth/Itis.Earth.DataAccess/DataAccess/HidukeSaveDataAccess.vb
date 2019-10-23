Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' 日付マスタに関係するデータアクセスクラスです
''' </summary>
''' <remarks></remarks>
Public Class HidukeSaveDataAccess

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ''' <summary>
    ''' コネクションストリング
    ''' </summary>
    ''' <remarks></remarks>
    Private connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>
    ''' 区分を引数に報告書発送日を取得します
    ''' </summary>
    ''' <param name="kubun">区分</param>
    ''' <returns>報告書発送日</returns>
    ''' <remarks></remarks>
    Public Function GetHoukokusyoHassouDate(ByVal kubun As String) As String

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetHoukokusyoHassouDate", _
                                    kubun)

        ' 戻り値
        Dim strHoukokusyoHassouDate As String = ""

        ' パラメータ
        Const strParamKubun As String = "@KUBUN"

        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append("SELECT kbn,hosyousyo_hak_date,hosyousyo_no_nengetu,hkks_hassou_date ")
        commandTextSb.Append("  FROM m_hiduke_save WITH (READCOMMITTED) ")
        commandTextSb.Append("  WHERE kbn = " & strParamKubun)

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamKubun, SqlDbType.Char, 1, kubun)}

        ' データ取得＆返却
        Dim cmnDtAcc As New CmnDataAccess
        Dim dTbl As New DataTable
        dTbl = cmnDtAcc.getDataTable(commandTextSb.ToString, commandParameters)
        If dTbl.Rows.Count > 0 Then
            If Not dTbl.Rows(0)("hkks_hassou_date") Is Nothing AndAlso Not dTbl.Rows(0)("hkks_hassou_date") Is DBNull.Value Then
                strHoukokusyoHassouDate = Format(dTbl.Rows(0)("hkks_hassou_date"), "yyyy/MM/dd")
            End If
        End If

        Return strHoukokusyoHassouDate

    End Function

    ''' <summary>
    ''' 区分を引数に保証書発行日を取得します
    ''' </summary>
    ''' <param name="kubun">区分</param>
    ''' <returns>保証書発行日</returns>
    ''' <remarks></remarks>
    Public Function GetHosyousyoHakkouDate(ByVal kubun As String) As String

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetHosyousyoHakkouDate", _
                                    kubun)

        ' 戻り値
        Dim strHosyousyoHakkouDate As String = ""

        ' パラメータ
        Const strParamKubun As String = "@KUBUN"

        Dim commandTextSb As New StringBuilder()

        commandTextSb.Append("SELECT kbn,hosyousyo_hak_date,hosyousyo_no_nengetu,hkks_hassou_date ")
        commandTextSb.Append("  FROM m_hiduke_save WITH (READCOMMITTED) ")
        commandTextSb.Append("  WHERE kbn = " & strParamKubun)

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamKubun, SqlDbType.Char, 1, kubun)}

        ' データ取得＆返却
        Dim cmnDtAcc As New CmnDataAccess
        Dim dTbl As New DataTable
        dTbl = cmnDtAcc.getDataTable(commandTextSb.ToString, commandParameters)
        If dTbl.Rows.Count > 0 Then
            If Not dTbl.Rows(0)("hosyousyo_hak_date") Is Nothing AndAlso Not dTbl.Rows(0)("hosyousyo_hak_date") Is DBNull.Value Then
                strHosyousyoHakkouDate = Format(dTbl.Rows(0)("hosyousyo_hak_date"), "yyyy/MM/dd")
            End If
        End If

        Return strHosyousyoHakkouDate

    End Function

    ''' <summary>
    ''' 日付マスタレコードを取得します
    ''' </summary>
    ''' <param name="kbn">区分</param>
    ''' <returns>日付マスタデータテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetHidukeSaveData(ByVal kbn As String) As HidukeSaveDataSet.HidukeSaveTableDataTable

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".CreateRow", _
                                            kbn)

        ' パラメータ
        Const strParamKbn As String = "@KBN"

        Dim commandText As String = " SELECT  " & _
                                    "     kbn, " & _
                                    "     hosyousyo_hak_date, " & _
                                    "     hosyousyo_no_nengetu, " & _
                                    "     hkks_hassou_date, " & _
                                    "     upd_login_user_id, " & _
                                    "     upd_datetime " & _
                                    " FROM  " & _
                                    "     m_hiduke_save (READCOMMITTED) " & _
                                    " WHERE " & _
                                    "     kbn = " & strParamKbn

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamKbn, SqlDbType.Char, 1, kbn)}

        ' データの取得
        Dim hidukeDataSet As New HidukeSaveDataSet

        SQLHelper.FillDataset(connStr, CommandType.Text, commandText, _
            hidukeDataSet, hidukeDataSet.HidukeSaveTable.TableName, commandParameters)

        Dim hidukeTable As HidukeSaveDataSet.HidukeSaveTableDataTable = hidukeDataSet.HidukeSaveTable

        Return hidukeTable

    End Function
End Class

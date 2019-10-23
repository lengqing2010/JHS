Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' 重複データの取得クラスです
''' </summary>
''' <remarks></remarks>
Public Class TyoufukuDataAccess

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ''' <summary>
    ''' 区分、検索条件を引数に重複レコードを取得します
    ''' </summary>
    ''' <param name="kubun">区分</param>
    ''' <param name="hosyousyoNo">表示中の保証書NO</param>
    ''' <param name="searchItem1">テーブル項目名１</param>
    ''' <param name="condition1">チェック対象データ１</param>
    ''' <param name="searchItem2">テーブル項目名２</param>
    ''' <param name="condition2">チェック対象データ２</param>
    ''' <returns></returns>
    ''' <remarks>単体の重複チェック時には項目名、データを１つ指定<br/>
    '''          一覧データ取得時は施主名、住所共に指定する</remarks>
    Public Function GetDataBy(ByVal kubun As String, _
                              ByVal hosyousyoNo As String, _
                              ByVal searchItem1 As String, _
                              ByVal condition1 As String, _
                              ByVal searchItem2 As String, _
                              ByVal condition2 As String) As TyoufukuDataSet.TyoufukuTableDataTable

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetDataBy", _
                                                    kubun, _
                                                    hosyousyoNo, _
                                                    searchItem1, _
                                                    condition1, _
                                                    searchItem2, _
                                                    condition2)

        Dim connStr As String = ConnectionManager.Instance.ConnectionString

        ' 日付範囲From（当月より６ヶ月前の１日）を取得
        Dim dateFrom As New DateTime(Date.Today.AddMonths(-5).Year, _
                                     Date.Today.AddMonths(-5).Month, _
                                     1, 0, 0, 0)
        ' 日付範囲To（当月末）を取得
        Dim dateTo As New DateTime(Date.Today.Year, _
                                   Date.Today.Month, _
                                   Date.DaysInMonth(Date.Today.Year, Date.Today.Month), _
                                   23, 59, 59)

        ' 保証書NOの検索条件を生成
        Dim strFrom As String = Format(dateFrom, "yyyyMM") & "0000"
        Dim strTo As String = Format(dateTo, "yyyyMM") & "9999"

        ' 施主名、住所共に検索する際の追加項目
        Dim strAll As String = ""
        Dim strCondition2 As String = ""

        If searchItem2.Trim() <> "" Then
            strAll = " OR " & searchItem2 & " = "
            strCondition2 = "@CONDITION2"
        End If

        ' パラメータ
        Dim arParams As String() = {"@KUBUN", _
                                    "@MYHOSYOUSYONO", _
                                    "@DATEFROM", _
                                    "@DATETO", _
                                    "@CONDITION1", _
                                    searchItem1, _
                                    strAll, _
                                    strCondition2}


        Dim commandText As String = "SELECT " & _
                                    "    h.haki_syubetu, " & _
                                    "    z.kbn, " & _
                                    "    z.hosyousyo_no, " & _
                                    "    z.sesyu_mei, " & _
                                    "    z.bukken_jyuusyo1, " & _
                                    "    z.bukken_jyuusyo2, " & _
                                    "    km.kameiten_mei1, " & _
                                    "    z.bikou " & _
                                    "FROM " & _
                                    "    t_jiban z WITH (READCOMMITTED) " & _
                                    "    LEFT JOIN m_data_haki h WITH (READCOMMITTED) ON (z.data_haki_syubetu = h.data_haki_no) " & _
                                    "    LEFT JOIN m_kameiten km WITH (READCOMMITTED) ON (z.kameiten_cd   = km.kameiten_cd) " & _
                                    "WHERE " & _
                                    "    z.kbn = {0}  " & _
                                    "AND z.hosyousyo_no <> {1}  " & _
                                    "AND z.hosyousyo_no BETWEEN {2} AND {3} " & _
                                    "AND ({5} = {4} {6}{7} ) " & _
                                    "ORDER BY z.hosyousyo_no "

        ' パラメータへデータを設定
        Dim commandParameters() As SqlParameter

        If searchItem2.Trim() <> "" Then
            commandParameters = New SqlParameter() _
            {SQLHelper.MakeParam(arParams(0), SqlDbType.Char, 1, kubun), _
             SQLHelper.MakeParam(arParams(1), SqlDbType.VarChar, 10, hosyousyoNo), _
             SQLHelper.MakeParam(arParams(2), SqlDbType.VarChar, 10, strFrom), _
             SQLHelper.MakeParam(arParams(3), SqlDbType.VarChar, 10, strTo), _
             SQLHelper.MakeParam(arParams(4), SqlDbType.VarChar, 30, condition1), _
             SQLHelper.MakeParam(arParams(7), SqlDbType.VarChar, 30, condition2)}
        Else
            commandParameters = New SqlParameter() _
            {SQLHelper.MakeParam(arParams(0), SqlDbType.Char, 1, kubun), _
             SQLHelper.MakeParam(arParams(1), SqlDbType.VarChar, 10, hosyousyoNo), _
             SQLHelper.MakeParam(arParams(2), SqlDbType.VarChar, 10, strFrom), _
             SQLHelper.MakeParam(arParams(3), SqlDbType.VarChar, 10, strTo), _
             SQLHelper.MakeParam(arParams(4), SqlDbType.VarChar, 30, condition1)}
        End If

        ' パラメータをSQLに反映
        commandText = String.Format(commandText, arParams)

        ' データの取得
        Dim tyoufukuDataSet As New TyoufukuDataSet()

        SQLHelper.FillDataset(connStr, CommandType.Text, commandText, _
            tyoufukuDataSet, tyoufukuDataSet.TyoufukuTable.TableName, commandParameters)

        Dim tyoufukuTable As TyoufukuDataSet.TyoufukuTableDataTable = tyoufukuDataSet.TyoufukuTable

        Return tyoufukuTable

    End Function

End Class

Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' 特定３系列の共通処理を行う親クラスです 
''' </summary>
''' <remarks></remarks>
Public Class KeiretuDataAccess
    Inherits AbsDataAccess

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

#Region "変数定義"
    ''' <summary>
    ''' コネクションストリング
    ''' </summary>
    ''' <remarks></remarks>
    Protected connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>
    ''' コマンドテキスト
    ''' </summary>
    ''' <remarks></remarks>
    Protected commandTextSb As New StringBuilder()

    ''' <summary>
    ''' データセット
    ''' </summary>
    ''' <remarks></remarks>
    Protected seikyuuKakakuDataSet As New SeikyuKingakuDataSet()

    ''' <summary>
    ''' データテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Protected dataTableName As String
#End Region

    ''' <summary>
    ''' 基本となるSQLの設定を行います
    ''' </summary>
    ''' <param name="strItem"></param>
    ''' <param name="intMode"></param>
    ''' <remarks></remarks>
    Protected Function SetBaseSQLData(ByVal intMode As Integer, _
                                      ByVal strItem As String) As Boolean

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SetBaseSQLData", _
                                            intMode, _
                                            strItem)

        ' 取得モードによる処理分岐
        Select Case intMode
            Case 1, 2, 6
                commandTextSb.Append(" SELECT ISNULL(kameiten_muke_kkk,0) AS kameiten_muke_kkk, ISNULL(kakeritu,0) AS kakeritu ")
                dataTableName = seikyuuKakakuDataSet.dt_KameitenMuke.TableName
            Case 3
                commandTextSb.Append(String.Format(" SELECT ISNULL({0},0) AS syutoku_kakaku ", strItem))
                dataTableName = seikyuuKakakuDataSet.dt_SyutokuKakaku.TableName
            Case 4, 5
                commandTextSb.Append(" SELECT ISNULL(kakeritu,0) AS kakeritu ")
                dataTableName = seikyuuKakakuDataSet.dt_Kakeritu.TableName
            Case Else
                Return False
        End Select

        Return True
    End Function

    ''' <summary>
    ''' SQLを実行し価格を設定します
    ''' </summary>
    ''' <param name="intMode">取得モード<BR/>
    ''' 1 (商品コード1の加盟店向価格)   <BR/>
    ''' 2 (解約払戻の加盟店向価格)      <BR/>
    ''' 3 (本部(TH)向価格)              <BR/>
    ''' 4 (実請求金額 / 掛率)           <BR/>
    ''' 5 (工務店請求金額 * 掛率)       <BR/>
    ''' 6 (加盟店向価格→実請求金額/掛率)</param>
    ''' <param name="intKingaku">TH請求用価格マスタのKEY,掛率計算時の金額<BR/>
    ''' 取得モード=1 (TH請求用価格マスタのKEY)<BR/>
    ''' 取得モード=4 (実請求金額)<BR/>
    ''' 取得モード=5 (工務店請求金額)</param>
    ''' <param name="inrReturnKingaku">取得金額（戻り値）</param>
    ''' <returns>True:取得OK,False:取得NG</returns>
    ''' <remarks></remarks>
    Protected Function GetKingaku(ByVal intMode As Integer, _
                                  ByVal intKingaku As Integer, _
                                  ByRef inrReturnKingaku As Integer) As Boolean

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKingaku", _
                                    intMode, _
                                    intKingaku, _
                                    inrReturnKingaku)

        ' 取得モードによる処理分岐
        Select Case intMode
            Case 1, 2
                Dim kameitenMukeTable As SeikyuKingakuDataSet.dt_KameitenMukeDataTable = _
                    seikyuuKakakuDataSet.dt_KameitenMuke

                If kameitenMukeTable.Count <> 0 Then
                    ' 取得できた場合、行情報を取得し参照レコードに設定する
                    Dim row As SeikyuKingakuDataSet.dt_KameitenMukeRow = kameitenMukeTable(0)
                    inrReturnKingaku = row.kameiten_muke_kkk
                Else
                    Return False
                End If
            Case 3
                Dim syutokuKakakuTable As SeikyuKingakuDataSet.dt_SyutokuKakakuDataTable = _
                    seikyuuKakakuDataSet.dt_SyutokuKakaku

                If syutokuKakakuTable.Count <> 0 Then
                    ' 取得できた場合、行情報を取得し参照レコードに設定する
                    Dim row As SeikyuKingakuDataSet.dt_SyutokuKakakuRow = syutokuKakakuTable(0)
                    inrReturnKingaku = row.syutoku_kakaku
                Else
                    Return False
                End If
            Case 4
                Dim kakerituTable As SeikyuKingakuDataSet.dt_KakerituDataTable = _
                    seikyuuKakakuDataSet.dt_Kakeritu

                If kakerituTable.Count <> 0 Then
                    ' 取得できた場合、行情報を取得し参照レコードに設定する
                    Dim row As SeikyuKingakuDataSet.dt_KakerituRow = kakerituTable(0)

                    If row.kakeritu <> 0 Then
                        inrReturnKingaku = Fix(intKingaku / row.kakeritu)
                    Else
                        inrReturnKingaku = 0
                    End If
                Else
                    Return False
                End If
            Case 5
                Dim kakerituTable As SeikyuKingakuDataSet.dt_KakerituDataTable = _
                    seikyuuKakakuDataSet.dt_Kakeritu

                If kakerituTable.Count <> 0 Then
                    ' 取得できた場合、行情報を取得し参照レコードに設定する
                    Dim row As SeikyuKingakuDataSet.dt_KakerituRow = kakerituTable(0)

                    inrReturnKingaku = Fix(intKingaku * row.kakeritu)
                Else
                    Return False
                End If
            Case 6
                Dim kameitenMukeTable As SeikyuKingakuDataSet.dt_KameitenMukeDataTable = _
                    seikyuuKakakuDataSet.dt_KameitenMuke

                If kameitenMukeTable.Count <> 0 Then
                    ' 取得できた場合、行情報を取得し参照レコードに設定する
                    Dim row As SeikyuKingakuDataSet.dt_KameitenMukeRow = kameitenMukeTable(0)

                    ' 検索結果の加盟店価格が0の場合
                    If row.kameiten_muke_kkk = 0 Then
                        If row.kakeritu <> 0 Then
                            inrReturnKingaku = Fix(intKingaku / row.kakeritu)
                        Else
                            inrReturnKingaku = 0
                        End If
                    Else
                        inrReturnKingaku = row.kameiten_muke_kkk
                    End If
                Else
                    Return False
                End If
            Case Else
                Return False
        End Select

        Return True
    End Function

    ''' <summary>
    ''' コンボボックス設定用の有効な区分レコードを全て取得します
    ''' </summary>
    ''' <param name="dt" ></param>
    ''' <param name="withSpaceRow" >先頭に空白行をセットする場合:true</param>
    ''' <param name="withCode" >（任意）ValueにKey項目を付加する場合:true " 1:aaaa "</param>
    ''' <remarks></remarks>
    Public Overrides Sub GetDropdownData(ByRef dt As DataTable, ByVal withSpaceRow As Boolean, Optional ByVal withCode As Boolean = True, Optional ByVal blnTorikesi As Boolean = True)
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetDropdownData", dt, withSpaceRow, withCode)

        Dim connStr As String = ConnectionManager.Instance.ConnectionString
        Dim commandTextSb As New StringBuilder()
        Dim row As KeiretuSearchDataSet.KeiretuTableRow

        commandTextSb.Append(" SELECT")
        commandTextSb.Append("    i.keiretu_cd")
        commandTextSb.Append("    ,k.keiretu_mei")
        commandTextSb.Append(" FROM")
        commandTextSb.Append("    jhs_sys.m_ikkatu_nyuukin_taisyou i")
        commandTextSb.Append(" LEFT JOIN (")
        commandTextSb.Append("			SELECT")
        commandTextSb.Append("				keiretu_cd")
        commandTextSb.Append("        		,keiretu_mei")
        commandTextSb.Append("        	FROM")
        commandTextSb.Append("        		jhs_sys.m_keiretu")
        commandTextSb.Append("        	GROUP BY")
        commandTextSb.Append("        		keiretu_cd,keiretu_mei")
        commandTextSb.Append("        	) k")
        commandTextSb.Append("           	ON (i.keiretu_cd = k.keiretu_cd)")
        commandTextSb.Append(" ORDER BY")
        commandTextSb.Append("    i.keiretu_cd")

        ' データの取得
        Dim keiretuDataSet As New KeiretuSearchDataSet()
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            keiretuDataSet, keiretuDataSet.KeiretuTable.TableName)

        Dim keiretuDataTable As KeiretuSearchDataSet.KeiretuTableDataTable = _
                    keiretuDataSet.KeiretuTable

        If keiretuDataTable.Count <> 0 Then

            ' 空白行データの設定
            If withSpaceRow = True Then
                dt.Rows.Add(CreateRow("", "", dt))
            End If

            ' 取得データよりDataRowを作成しDataTableにセットする
            For Each row In keiretuDataTable
                If withCode = True Then
                    dt.Rows.Add(CreateRow(row.keiretu_cd + ":" + row.keiretu_mei, row.keiretu_cd, dt))
                Else
                    dt.Rows.Add(CreateRow(row.keiretu_mei, row.keiretu_cd, dt))
                End If
            Next

        End If

    End Sub
End Class

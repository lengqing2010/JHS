Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' 承諾書金額の取得クラスです
''' </summary>
''' <remarks></remarks>
Public Class SyoudakusyoKingakuDataAccess

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    Dim connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>
    ''' 承諾書金額を取得します
    ''' </summary>
    ''' <param name="strTyousakaisyaCd">調査会社ｺｰﾄﾞ+事業所ｺｰﾄﾞ</param>
    ''' <param name="strKameitenCd">加盟店ｺｰﾄﾞ</param>
    ''' <param name="intDoujiIraiSuu">同時依頼棟数</param>
    ''' <param name="intSyoudakuKingaku">承諾書金額</param>
    ''' <returns>True:取得成功，False:取得失敗</returns>
    ''' <remarks>仕入価格マスタより価格を取得できない場合、<br/>
    '''          調査会社マスタの標準価格を取得する</remarks>
    Public Function GetSyoudakuKingaku(ByVal strTyousakaisyaCd As String, _
                                       ByVal strKameitenCd As String, _
                                       ByVal intDoujiIraiSuu As Integer, _
                                       ByRef intSyoudakuKingaku As Integer) As Boolean

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSyoudakuKingaku", _
                                                    strTyousakaisyaCd, _
                                                    strKameitenCd, _
                                                    intDoujiIraiSuu, _
                                                    intSyoudakuKingaku)
        ' パラメータ
        Const paramTyousakaisyaCd As String = "@TYOUSAGAIYOU"
        Const paramKameitenCd As String = "@KAMEITEN"

        Dim commandTextSb As New StringBuilder()
        Dim itemName As String = ""

        ' 同時依頼棟数による取得項目の設定
        Select Case intDoujiIraiSuu
            Case 1 To 3
                itemName = "siire_kkk1"
            Case 4 To 9
                itemName = "siire_kkk2"
            Case 10 To 19
                itemName = "siire_kkk3"
            Case Else
                Return False
        End Select

        commandTextSb.Append(String.Format("SELECT {0} AS siire_kkk FROM m_siire_kakaku WITH (READCOMMITTED) ", itemName))
        commandTextSb.Append("  WHERE RTRIM(tys_kaisya_cd) + RTRIM(jigyousyo_cd) = " & paramTyousakaisyaCd)
        commandTextSb.Append("  AND kameiten_cd = " & paramKameitenCd)

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(paramTyousakaisyaCd, SqlDbType.VarChar, 7, strTyousakaisyaCd), _
             SQLHelper.MakeParam(paramKameitenCd, SqlDbType.VarChar, 5, strKameitenCd)}

        ' データの取得
        Dim syoudakusyoKingakuDataSet As New SyoudakusyoKingakuDataSet()

        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            syoudakusyoKingakuDataSet, syoudakusyoKingakuDataSet.SyoudakusyoKingakuTable.TableName, commandParameters)

        Dim syoudakusyoTable As SyoudakusyoKingakuDataSet.SyoudakusyoKingakuTableDataTable = syoudakusyoKingakuDataSet.SyoudakusyoKingakuTable

        If syoudakusyoTable.Count = 0 Then

            'TBL_M調査会社．SS基準価格を取得
            If GetKijunKakaku(strTyousakaisyaCd, intSyoudakuKingaku) = False Then
                ' 取得に失敗した場合、Falseを返却
                Return False
            End If
        Else
            Dim row As SyoudakusyoKingakuDataSet.SyoudakusyoKingakuTableRow = syoudakusyoTable(0)

            If row.siire_kkk = Nothing Then

                'TBL_M調査会社．SS基準価格を取得
                If GetKijunKakaku(strTyousakaisyaCd, intSyoudakuKingaku) = False Then
                    ' 取得に失敗した場合、Falseを返却
                    Return False
                End If
            Else
                intSyoudakuKingaku = Integer.Parse(row.siire_kkk)
            End If

        End If

        Return True

    End Function

    ''' <summary>
    ''' 調査会社マスタよりSS基準価格を取得する
    ''' </summary>
    ''' <param name="strTyousakaisyaCd">調査会社ｺｰﾄﾞ+事業所ｺｰﾄﾞ</param>
    ''' <param name="intSyoudakuKingaku">承諾書金額</param>
    ''' <returns>True:取得成功，False:取得失敗</returns>
    ''' <remarks></remarks>
    Public Function GetKijunKakaku(ByVal strTyousakaisyaCd As String, _
                                   ByRef intSyoudakuKingaku As Integer) As Boolean

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKijunKakaku", _
                                                    strTyousakaisyaCd, _
                                                    intSyoudakuKingaku)

        ' パラメータ
        Const paramTyousaGaiyouCd As String = "@TYOUSAGAISYA"

        Dim commandTextSb As New StringBuilder()
        commandTextSb.Append(" SELECT ss_kijyun_kkk AS siire_kkk FROM m_tyousakaisya WITH (READCOMMITTED) ")
        commandTextSb.Append("  WHERE RTRIM(tys_kaisya_cd) + RTRIM(jigyousyo_cd) = " & paramTyousaGaiyouCd)
        commandTextSb.Append("  AND torikesi = 0 ")

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(paramTyousaGaiyouCd, SqlDbType.VarChar, 7, strTyousakaisyaCd)}

        ' データの取得
        Dim syoudakusyoKingakuDataSet As New SyoudakusyoKingakuDataSet()

        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            syoudakusyoKingakuDataSet, syoudakusyoKingakuDataSet.SyoudakusyoKingakuTable.TableName, commandParameters)

        Dim syoudakusyoTable As SyoudakusyoKingakuDataSet.SyoudakusyoKingakuTableDataTable = syoudakusyoKingakuDataSet.SyoudakusyoKingakuTable

        If syoudakusyoTable.Count = 0 Then
            ' 取得に失敗した場合、Falseを返却
            Return False
        Else
            Dim row As SyoudakusyoKingakuDataSet.SyoudakusyoKingakuTableRow = syoudakusyoTable(0)

            If row.siire_kkk = Nothing Then
                Return False
            Else
                intSyoudakuKingaku = Integer.Parse(row.siire_kkk)
            End If
        End If

        Return True
    End Function

End Class

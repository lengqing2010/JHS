Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' 販売価格(工務店請求金額・実請求税抜金額)
''' </summary>
''' <remarks></remarks>
Public Class HanbaiKakakuDataAccess

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    Dim cmnDtAcc As New CmnDataAccess
    Dim connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>
    ''' 販売価格マスタより販売価格情報を取得します
    ''' </summary>
    ''' <param name="intAitesakiSyubetu">相手先種別</param>
    ''' <param name="strAitesakiCd">相手先コード</param>
    ''' <param name="strSyouhinCd">商品コード</param>
    ''' <param name="intTysHouhouNo">調査方法NO</param>
    ''' <param name="intKmtnKingaku">工務店請求金額</param>
    ''' <param name="blnKmtnKingakuKahi">工務店請求金額変更FLG</param>
    ''' <param name="intJskuKingaku">実請求税抜き金額</param>
    ''' <param name="blnJskuKingakuKahi">実請求税抜金額</param>
    ''' <param name="blnTysHouhou">調査方法検索フラグ</param>
    ''' <param name="blnTorikesi">取消フラグ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetHanbaiKakaku(ByVal intAitesakiSyubetu As Integer, _
                               ByVal strAitesakiCd As String, _
                               ByVal strSyouhinCd As String, _
                               ByVal intTysHouhouNo As Integer, _
                               ByRef intKmtnKingaku As Integer, _
                               ByRef blnKmtnKingakuKahi As Boolean, _
                               ByRef intJskuKingaku As Integer, _
                               ByRef blnJskuKingakuKahi As Boolean, _
                               ByVal blnTysHouhou As Boolean, _
                               Optional ByVal blnTorikesi As Boolean = False) As Boolean

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetHanbaiKakaku", _
                                                    intAitesakiSyubetu, _
                                                    strAitesakiCd, _
                                                    strSyouhinCd, _
                                                    intTysHouhouNo, _
                                                    intKmtnKingaku, _
                                                    blnKmtnKingakuKahi, _
                                                    intJskuKingaku, _
                                                    blnJskuKingakuKahi, _
                                                    blnTorikesi)
        'パラメータ
        Const paramAitesakiSyubetu As String = "@AITESAKISYUBETU"
        Const paramAitesakiCd As String = "@AITESAKICD"
        Const paramSyouhinCd As String = "@SYOUHINCD"
        Const paramTysHouhouNo As String = "@TYSHOUHOUNO"
        Dim strKoumuKakaku As String = "koumuten_seikyuu_gaku"
        Dim strKoumuKakakuKahi As String = "koumuten_seikyuu_gaku_henkou_flg"
        Dim strJituKakaku As String = "jitu_seikyuu_gaku"
        Dim strJituKakakuKahi As String = "jitu_seikyuu_gaku_henkou_flg"

        Dim cmdTextSb As New StringBuilder()
        Dim dt As New DataTable

        cmdTextSb.AppendLine(" SELECT ")
        cmdTextSb.AppendLine("      MHK.aitesaki_syubetu ")
        cmdTextSb.AppendLine("    , MHK.aitesaki_cd ")
        cmdTextSb.AppendLine("    , MHK.syouhin_cd ")
        cmdTextSb.AppendLine("    , MHK.tys_houhou_no ")
        cmdTextSb.AppendLine("    , MHK.koumuten_seikyuu_gaku ")
        cmdTextSb.AppendLine("    , MHK.koumuten_seikyuu_gaku_henkou_flg ")
        cmdTextSb.AppendLine("    , MHK.jitu_seikyuu_gaku ")
        cmdTextSb.AppendLine("    , MHK.jitu_seikyuu_gaku_henkou_flg ")
        cmdTextSb.AppendLine("    , MHK.koukai_flg ")
        cmdTextSb.AppendLine(" FROM ")
        cmdTextSb.AppendLine("      m_hanbai_kakaku MHK ")
        cmdTextSb.AppendLine(" WHERE ")
        cmdTextSb.AppendLine("      MHK.aitesaki_syubetu = " & paramAitesakiSyubetu)
        cmdTextSb.AppendLine("  AND MHK.aitesaki_cd = " & paramAitesakiCd)
        cmdTextSb.AppendLine("  AND MHK.syouhin_cd = " & paramSyouhinCd)
        '商品M.調査有無区分と営業所による検索により、調査方法NOを切り分ける
        If blnTysHouhou Then
            cmdTextSb.AppendLine("  AND MHK.tys_houhou_no = " & paramTysHouhouNo)
        Else
            cmdTextSb.AppendLine("  AND MHK.tys_houhou_no = 0 ")
        End If
        '取消フラグ
        If blnTorikesi Then
            cmdTextSb.AppendLine("  AND MHK.torikesi = 0 ")
        End If

        ' パラメータへ設定
        Dim sqlParams() As SqlParameter = {SQLHelper.MakeParam(paramAitesakiSyubetu, SqlDbType.Int, 4, intAitesakiSyubetu), _
                                           SQLHelper.MakeParam(paramAitesakiCd, SqlDbType.VarChar, 5, strAitesakiCd), _
                                           SQLHelper.MakeParam(paramSyouhinCd, SqlDbType.VarChar, 8, strSyouhinCd), _
                                           SQLHelper.MakeParam(paramTysHouhouNo, SqlDbType.Int, 4, intTysHouhouNo)}

        'データ取得
        dt = cmnDtAcc.getDataTable(cmdTextSb.ToString, sqlParams)

        If dt.Rows.Count > 0 Then

            '●工務店請求金額
            If Not IsDBNull(dt.Rows(0)(strKoumuKakaku)) Then
                intKmtnKingaku = dt.Rows(0)(strKoumuKakaku).ToString
            Else
                intKmtnKingaku = 0
            End If
            If IsDBNull(dt.Rows(0)(strKoumuKakakuKahi)) OrElse dt.Rows(0)(strKoumuKakakuKahi).ToString = 0 Then
                '0orNullの場合、変更不可
                blnKmtnKingakuKahi = False
            Else
                '1以上の場合、変更可
                blnKmtnKingakuKahi = True
            End If

            '●実請求税抜金額
            If Not IsDBNull(dt.Rows(0)(strJituKakaku)) Then
                intJskuKingaku = dt.Rows(0)(strJituKakaku).ToString
            Else
                intJskuKingaku = 0
            End If
            If IsDBNull(dt.Rows(0)(strJituKakakuKahi)) OrElse dt.Rows(0)(strJituKakakuKahi).ToString = 0 Then
                '0orNullの場合、変更不可
                blnJskuKingakuKahi = False
            Else
                '1以上の場合、変更可
                blnJskuKingakuKahi = True
            End If

            Return True
        End If


        Return False
    End Function

End Class
Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' 原価（承諾書金額）を取得するクラスです
''' </summary>
''' <remarks></remarks>
Public Class GenkaDataAccess

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    Dim cmnDtAcc As New CmnDataAccess
    Dim connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>
    ''' 承諾書金額を取得します（原価マスタから）
    ''' </summary>
    ''' <param name="strTyousaKaisyaCd">調査会社ｺｰﾄﾞ+事業所ｺｰﾄﾞ</param>
    ''' <param name="intAitesakiSyubetu">相手先種別</param>
    ''' <param name="strAitesakiCd">相手先ｺｰﾄﾞ</param>
    ''' <param name="strSyouhinCd">商品コード</param>
    ''' <param name="intTysHouhouNo">調査方法</param>
    ''' <param name="intDoujiIraiSuu">同時依頼棟数</param>
    ''' <param name="intSyoudakuKingaku"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSyoudakuKingaku(ByVal strTyousaKaisyaCd As String, _
                                       ByVal intAitesakiSyubetu As Integer, _
                                       ByVal strAitesakiCd As String, _
                                       ByVal strSyouhinCd As String, _
                                       ByVal intTysHouhouNo As Integer, _
                                       ByVal intDoujiIraiSuu As Integer, _
                                       ByRef intSyoudakuKingaku As Integer, _
                                       ByRef blnHenkouFlg As Boolean, _
                                       Optional ByVal blnTorikesi As Boolean = False) As Boolean

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSyoudakuKingaku", _
                                                    strTyousaKaisyaCd, _
                                                    intAitesakiSyubetu, _
                                                    strAitesakiCd, _
                                                    strSyouhinCd, _
                                                    intTysHouhouNo, _
                                                    intDoujiIraiSuu, _
                                                    intSyoudakuKingaku, _
                                                    blnHenkouFlg)

        'パラメータ
        Const paramTyousakaisyaCd As String = "@TYOUSAKAISYA"
        Const paramAitesakiSyubetu As String = "@AITESAKISYUBETU"
        Const paramAitesakiCd As String = "@AITESAKICD"
        Const paramSyouhinCd As String = "@SYOUHINCD"
        Const paramTysHouhouNo As String = "@TYSHOUHOUNO"

        Dim cmdTextSb As New StringBuilder()
        Dim strTouKakaku As String = String.Empty
        Dim strHenkouFlg As String = String.Empty

        '同時依頼棟数による取得項目の設定
        Select Case intDoujiIraiSuu
            Case 1
                strTouKakaku = "tou_kkk1"
                strHenkouFlg = "tou_kkk_henkou_flg1"
            Case 2
                strTouKakaku = "tou_kkk2"
                strHenkouFlg = "tou_kkk_henkou_flg2"
            Case 3
                strTouKakaku = "tou_kkk3"
                strHenkouFlg = "tou_kkk_henkou_flg3"
            Case 4
                strTouKakaku = "tou_kkk4"
                strHenkouFlg = "tou_kkk_henkou_flg4"
            Case 5
                strTouKakaku = "tou_kkk5"
                strHenkouFlg = "tou_kkk_henkou_flg5"
            Case 6
                strTouKakaku = "tou_kkk6"
                strHenkouFlg = "tou_kkk_henkou_flg6"
            Case 7
                strTouKakaku = "tou_kkk7"
                strHenkouFlg = "tou_kkk_henkou_flg7"
            Case 8
                strTouKakaku = "tou_kkk8"
                strHenkouFlg = "tou_kkk_henkou_flg8"
            Case 9
                strTouKakaku = "tou_kkk9"
                strHenkouFlg = "tou_kkk_henkou_flg9"
            Case 10
                strTouKakaku = "tou_kkk10"
                strHenkouFlg = "tou_kkk_henkou_flg10"
            Case 11 To 19
                strTouKakaku = "tou_kkk11t19"
                strHenkouFlg = "tou_kkk_henkou_flg11t19"
            Case 20 To 29
                strTouKakaku = "tou_kkk20t29"
                strHenkouFlg = "tou_kkk_henkou_flg20t29"
            Case 30 To 39
                strTouKakaku = "tou_kkk30t39"
                strHenkouFlg = "tou_kkk_henkou_flg30t39"
            Case 40 To 49
                strTouKakaku = "tou_kkk40t49"
                strHenkouFlg = "tou_kkk_henkou_flg40t49"
            Case Is >= 50
                strTouKakaku = "tou_kkk50t"
                strHenkouFlg = "tou_kkk_henkou_flg50t"
            Case Else
                strTouKakaku = "tou_kkk1"
                strHenkouFlg = "tou_kkk_henkou_flg1"
        End Select


        Dim dt As DataTable

        cmdTextSb.AppendLine(" SELECT ")
        cmdTextSb.AppendLine("      MG.tys_kaisya_cd ")
        cmdTextSb.AppendLine("    , MG.jigyousyo_cd ")
        cmdTextSb.AppendLine("    , MG.aitesaki_syubetu ")
        cmdTextSb.AppendLine("    , MG.aitesaki_cd ")
        cmdTextSb.AppendLine("    , MG.syouhin_cd ")
        cmdTextSb.AppendLine("    , MG.tys_houhou_no ")
        cmdTextSb.AppendLine(String.Format("    , {0} ", strTouKakaku))
        cmdTextSb.AppendLine(String.Format("    , {0} ", strHenkouFlg))
        cmdTextSb.AppendLine(" FROM ")
        cmdTextSb.AppendLine("      m_genka MG ")
        cmdTextSb.AppendLine(" WHERE ")
        cmdTextSb.AppendLine("      MG.tys_kaisya_cd + MG.jigyousyo_cd = " & paramTyousakaisyaCd)
        cmdTextSb.AppendLine("  AND MG.aitesaki_syubetu = " & paramAitesakiSyubetu)
        cmdTextSb.AppendLine("  AND MG.aitesaki_cd = " & paramAitesakiCd)
        cmdTextSb.AppendLine("  AND MG.syouhin_cd = " & paramSyouhinCd)
        cmdTextSb.AppendLine("  AND MG.tys_houhou_no =" & paramTysHouhouNo)
        If blnTorikesi Then
            cmdTextSb.AppendLine("  AND MG.torikesi = '0' ")
        End If

        ' パラメータへ設定
        Dim sqlParams() As SqlParameter = {SQLHelper.MakeParam(paramTyousakaisyaCd, SqlDbType.VarChar, 7, strTyousaKaisyaCd), _
                                           SQLHelper.MakeParam(paramAitesakiSyubetu, SqlDbType.Int, 4, intAitesakiSyubetu), _
                                           SQLHelper.MakeParam(paramAitesakiCd, SqlDbType.VarChar, 5, strAitesakiCd), _
                                           SQLHelper.MakeParam(paramSyouhinCd, SqlDbType.VarChar, 8, strSyouhinCd), _
                                           SQLHelper.MakeParam(paramTysHouhouNo, SqlDbType.Int, 4, intTysHouhouNo)}

        'データ取得
        dt = cmnDtAcc.getDataTable(cmdTextSb.ToString, sqlParams)

        If dt.Rows.Count > 0 Then
            If Not IsDBNull(dt.Rows(0)(strTouKakaku)) Then
                '承諾書金額（棟価格）
                intSyoudakuKingaku = dt.Rows(0)(strTouKakaku).ToString
            Else
                '金額設定無し
                intSyoudakuKingaku = 0
            End If
            If IsDBNull(dt.Rows(0)(strHenkouFlg)) OrElse dt.Rows(0)(strHenkouFlg).ToString = 0 Then
                '0orNullの場合、承諾書金額変更不可
                blnHenkouFlg = False
            Else
                '1以上の場合、承諾書金額変更可
                blnHenkouFlg = True
            End If

            Return True
        End If

        Return False
    End Function


End Class

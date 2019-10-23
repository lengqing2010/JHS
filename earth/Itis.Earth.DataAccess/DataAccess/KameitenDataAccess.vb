Imports System.text
Imports System.Data.SqlClient

Public Class KameitenDataAccess

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ''' <summary>
    ''' コネクションストリング
    ''' </summary>
    ''' <remarks></remarks>
    Private connStr As String = ConnectionManager.Instance.ConnectionString

#Region "加盟店マスタ価格取得"
    ''' <summary>
    ''' 加盟店マスタより価格を取得します
    ''' </summary>
    ''' <param name="strKubun">区分</param>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <param name="intTatemonoYouto">建物用途NO</param>
    ''' <param name="isYoutoAdd">建物用途による加算対象(True:加算する)</param>
    ''' <param name="strItem">取得対象のカラム名</param>
    ''' <param name="blnDelete">True:取消データを取得対象から除外 False:取消データを対象</param>
    ''' <param name="kameitenKakaku">検索結果</param>
    ''' <returns>True:取得成功 False:取得失敗</returns>
    ''' <remarks></remarks>
    Public Function GetKameitenKakaku(ByVal strKubun As String, _
                                      ByVal strKameitenCd As String, _
                                      ByVal intTatemonoYouto As Integer, _
                                      ByVal isYoutoAdd As Boolean, _
                                      ByVal strItem As String, _
                                      ByVal blnDelete As Boolean, _
                                      ByRef kameitenKakaku As Decimal) As Boolean

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKameitenKakaku", _
                                                    strKubun, _
                                                    strKameitenCd, _
                                                    intTatemonoYouto, _
                                                    isYoutoAdd, _
                                                    strItem, _
                                                    blnDelete, _
                                                    kameitenKakaku)

        ' パラメータ
        Const strParamKameitenCd As String = "@KAMEITENCD"
        Const strParamKubun As String = "@KUBUN"

        Dim youto As String = ""
        Dim commandTextSb As New StringBuilder()

        ' 建物用途が2～9の場合、金額に加算額を加算する(但し商品コードがA1001,A1003,A1004の場合)
        If intTatemonoYouto >= 2 And intTatemonoYouto <= 9 And isYoutoAdd = True Then
            youto = " , ISNULL(kasangaku" & intTatemonoYouto.ToString() & ",0) AS tatemono_youto"
        Else
            youto = " , 0 AS tatemono_youto"
        End If

        commandTextSb.Append("SELECT kbn,K.kameiten_cd,ISNULL(")
        commandTextSb.Append(strItem & ",0) As kameiten_master_kakaku ")
        commandTextSb.Append(youto)
        commandTextSb.Append("  FROM m_kameiten K WITH (READCOMMITTED) ")
        ' 建物用途が2～9の場合、金額に加算額を加算する(但し商品コードがA1001,A1003,A1004の場合)
        If intTatemonoYouto >= 2 And intTatemonoYouto <= 9 And isYoutoAdd = True Then
            commandTextSb.Append(" LEFT OUTER JOIN m_tatemono_youto_kasangaku Y WITH (READCOMMITTED) ON K.kameiten_cd = Y.kameiten_cd ")
        End If
        commandTextSb.Append("  WHERE K.kameiten_cd = " & strParamKameitenCd)
        commandTextSb.Append("  AND   kbn = " & strParamKubun)

        If blnDelete = True Then
            commandTextSb.Append("  AND torikesi = 0 ")
        End If

        ' パラメータへ設定
        Dim commandParameters() As SqlParameter = _
            {SQLHelper.MakeParam(strParamKameitenCd, SqlDbType.Char, 5, strKameitenCd), _
             SQLHelper.MakeParam(strParamKubun, SqlDbType.Char, 1, strKubun)}

        ' データの取得
        Dim kameitenDataSet As New KameitenKakakuDataSet()

        ' 検索実行
        SQLHelper.FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), _
            kameitenDataSet, kameitenDataSet.KameitenMasterKakakuTable.TableName, commandParameters)

        Dim kameitenTable As KameitenKakakuDataSet.KameitenMasterKakakuTableDataTable = _
                    kameitenDataSet.KameitenMasterKakakuTable

        If kameitenTable.Count <> 0 Then
            ' 取得できた場合、行情報を取得し参照レコードに設定する
            Dim row As KameitenKakakuDataSet.KameitenMasterKakakuTableRow = kameitenTable(0)

            kameitenKakaku = row.kameiten_master_kakaku

            ' 価格が０以外の場合、建物用途を加算する
            If kameitenKakaku <> 0 Then
                kameitenKakaku = kameitenKakaku + row.tatemono_youto
            End If

            Return True
        End If

        Return False

    End Function
#End Region

    ''' <summary>
    ''' 加盟店マスタのデフォルト請求先情報取得処理
    ''' </summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getKameitenDefaultSeikyuuSakiInfo(ByVal strKameitenCd As String) As DataTable
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getKameitenSeikyuuSakiInfo", _
                                                    strKameitenCd)
        Dim cmdTextSb As New StringBuilder()
        cmdTextSb.Append(" SELECT")
        cmdTextSb.Append("      MK.kameiten_cd")
        cmdTextSb.Append("    , MK.kameiten_mei1")
        cmdTextSb.Append("    , MK.tenmei_kana1")
        cmdTextSb.Append("    , MK.tys_seikyuu_saki_cd")
        cmdTextSb.Append("    , MK.tys_seikyuu_saki_brc")
        cmdTextSb.Append("    , MK.tys_seikyuu_saki_kbn")
        cmdTextSb.Append("    ,")
        cmdTextSb.Append("     (SELECT")
        cmdTextSb.Append("           VSS.seikyuu_saki_mei")
        cmdTextSb.Append("      FROM")
        cmdTextSb.Append("           jhs_sys.v_seikyuu_saki_info VSS")
        cmdTextSb.Append("      WHERE")
        cmdTextSb.Append("           MK.tys_seikyuu_saki_cd = VSS.seikyuu_saki_cd")
        cmdTextSb.Append("       AND MK.tys_seikyuu_saki_brc = VSS.seikyuu_saki_brc")
        cmdTextSb.Append("       AND MK.tys_seikyuu_saki_kbn = VSS.seikyuu_saki_kbn")
        cmdTextSb.Append("     )")
        cmdTextSb.Append("      tys_seikyuu_saki_mei")
        cmdTextSb.Append("    ,")
        cmdTextSb.Append("     (SELECT")
        cmdTextSb.Append("           MSH.kameiten_cd")
        cmdTextSb.Append("      FROM")
        cmdTextSb.Append("           m_seikyuu_saki_henkou MSH")
        cmdTextSb.Append("      WHERE")
        cmdTextSb.Append("           MK.kameiten_cd = MSH.kameiten_cd")
        cmdTextSb.Append("       AND (substring(MSH.syouhin_kbn, 1, 1) = 't'")
        cmdTextSb.Append("        OR substring(MSH.syouhin_kbn, 1, 1) = 'T')")
        cmdTextSb.Append("     )")
        cmdTextSb.Append("      tys_henkou_umu")
        cmdTextSb.Append("    , MK.koj_seikyuu_saki_cd")
        cmdTextSb.Append("    , MK.koj_seikyuu_saki_brc")
        cmdTextSb.Append("    , MK.koj_seikyuu_saki_kbn")
        cmdTextSb.Append("    ,")
        cmdTextSb.Append("     (SELECT")
        cmdTextSb.Append("           VSS.seikyuu_saki_mei")
        cmdTextSb.Append("      FROM")
        cmdTextSb.Append("           jhs_sys.v_seikyuu_saki_info VSS")
        cmdTextSb.Append("      WHERE")
        cmdTextSb.Append("           MK.koj_seikyuu_saki_cd = VSS.seikyuu_saki_cd")
        cmdTextSb.Append("       AND MK.koj_seikyuu_saki_brc = VSS.seikyuu_saki_brc")
        cmdTextSb.Append("       AND MK.koj_seikyuu_saki_kbn = VSS.seikyuu_saki_kbn")
        cmdTextSb.Append("     )")
        cmdTextSb.Append("      koj_seikyuu_saki_mei")
        cmdTextSb.Append("    ,")
        cmdTextSb.Append("     (SELECT")
        cmdTextSb.Append("           MSH.kameiten_cd")
        cmdTextSb.Append("      FROM")
        cmdTextSb.Append("           m_seikyuu_saki_henkou MSH")
        cmdTextSb.Append("      WHERE")
        cmdTextSb.Append("           MK.kameiten_cd = MSH.kameiten_cd")
        cmdTextSb.Append("       AND (substring(MSH.syouhin_kbn, 1, 1) = 'k'")
        cmdTextSb.Append("        OR substring(MSH.syouhin_kbn, 1, 1) = 'K')")
        cmdTextSb.Append("     )")
        cmdTextSb.Append("      koj_henkou_umu")
        cmdTextSb.Append("    , MK.hansokuhin_seikyuu_saki_cd")
        cmdTextSb.Append("    , MK.hansokuhin_seikyuu_saki_brc")
        cmdTextSb.Append("    , MK.hansokuhin_seikyuu_saki_kbn")
        cmdTextSb.Append("    ,")
        cmdTextSb.Append("     (SELECT")
        cmdTextSb.Append("           VSS.seikyuu_saki_mei")
        cmdTextSb.Append("      FROM")
        cmdTextSb.Append("           jhs_sys.v_seikyuu_saki_info VSS")
        cmdTextSb.Append("      WHERE")
        cmdTextSb.Append("           MK.hansokuhin_seikyuu_saki_cd = VSS.seikyuu_saki_cd")
        cmdTextSb.Append("       AND MK.hansokuhin_seikyuu_saki_brc = VSS.seikyuu_saki_brc")
        cmdTextSb.Append("       AND MK.hansokuhin_seikyuu_saki_kbn = VSS.seikyuu_saki_kbn")
        cmdTextSb.Append("     )")
        cmdTextSb.Append("      hansokuhin_seikyuu_saki_mei")
        cmdTextSb.Append("    ,")
        cmdTextSb.Append("     (SELECT")
        cmdTextSb.Append("           MSH.kameiten_cd")
        cmdTextSb.Append("      FROM")
        cmdTextSb.Append("           m_seikyuu_saki_henkou MSH")
        cmdTextSb.Append("      WHERE")
        cmdTextSb.Append("           MK.kameiten_cd = MSH.kameiten_cd")
        cmdTextSb.Append("       AND (substring(MSH.syouhin_kbn, 1, 1) = 'h'")
        cmdTextSb.Append("        OR substring(MSH.syouhin_kbn, 1, 1) = 'H')")
        cmdTextSb.Append("     )")
        cmdTextSb.Append("      hansoku_henkou_umu")
        cmdTextSb.Append("    , MK.tatemono_seikyuu_saki_cd")
        cmdTextSb.Append("    , MK.tatemono_seikyuu_saki_brc")
        cmdTextSb.Append("    , MK.tatemono_seikyuu_saki_kbn")
        cmdTextSb.Append("    ,")
        cmdTextSb.Append("     (SELECT")
        cmdTextSb.Append("           VSS.seikyuu_saki_mei")
        cmdTextSb.Append("      FROM")
        cmdTextSb.Append("           jhs_sys.v_seikyuu_saki_info VSS")
        cmdTextSb.Append("      WHERE")
        cmdTextSb.Append("           MK.tatemono_seikyuu_saki_cd = VSS.seikyuu_saki_cd")
        cmdTextSb.Append("       AND MK.tatemono_seikyuu_saki_brc = VSS.seikyuu_saki_brc")
        cmdTextSb.Append("       AND MK.tatemono_seikyuu_saki_kbn = VSS.seikyuu_saki_kbn")
        cmdTextSb.Append("     )")
        cmdTextSb.Append("      tatemono_seikyuu_saki_mei")
        cmdTextSb.Append("    ,")
        cmdTextSb.Append("     (SELECT")
        cmdTextSb.Append("           MSH.kameiten_cd")
        cmdTextSb.Append("      FROM")
        cmdTextSb.Append("           m_seikyuu_saki_henkou MSH")
        cmdTextSb.Append("      WHERE")
        cmdTextSb.Append("           MK.kameiten_cd = MSH.kameiten_cd")
        cmdTextSb.Append("       AND (substring(MSH.syouhin_kbn, 1, 1) = 's'")
        cmdTextSb.Append("        OR substring(MSH.syouhin_kbn, 1, 1) = 'S')")
        cmdTextSb.Append("     )")
        cmdTextSb.Append("      tatemono_henkou_umu")
        cmdTextSb.Append(" FROM")
        cmdTextSb.Append("      jhs_sys.m_kameiten MK")
        cmdTextSb.Append(" WHERE")
        cmdTextSb.Append("      MK.kameiten_cd = @KAMEITENCD")

        ' パラメータ
        Dim cmdParams() As SqlClient.SqlParameter
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam("@KAMEITENCD", SqlDbType.Char, 5, strKameitenCd)}

        ' データの取得＆返却
        Dim cmnDtAcc As New CmnDataAccess
        Return cmnDtAcc.getDataTable(cmdTextSb.ToString, cmdParams)

    End Function

End Class

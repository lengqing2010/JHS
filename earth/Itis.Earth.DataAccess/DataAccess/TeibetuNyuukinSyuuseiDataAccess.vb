Imports System.text
Imports System.Data.SqlClient
Public Class TeibetuNyuukinSyuuseiDataAccess

#Region "メンバ変数"
    Dim connStr As String = ConnectionManager.Instance.ConnectionString
    Dim strLogic As New StringLogic
#End Region

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName
    ''' <summary>
    ''' 邸別入金テーブルを更新します
    ''' </summary>
    ''' <param name="nyuukinRec"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdTeibetuNyuukin(ByVal nyuukinRec As TeibetuNyuukinRecord) As Integer
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".UpdTeibetuNyuukin" _
                                                    , nyuukinRec)

        Dim cmdTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter
        Dim intResult As Integer
        Dim dtTimeNow As DateTime = Now


        cmdTextSb.Append(" UPDATE")
        cmdTextSb.Append("      t_teibetu_nyuukin")
        cmdTextSb.Append(" SET")
        cmdTextSb.Append("      bunrui_cd            = @BUNRUICD")
        cmdTextSb.Append("    , zeikomi_nyuukin_gaku = @ZEIKOMINYUUKINGAKU")
        cmdTextSb.Append("    , zeikomi_henkin_gaku  = @ZEIKOMIHENKINGAKU")
        cmdTextSb.Append("    , saisyuu_nyuukin_date = @SAISYUUNYUUKINDATE")
        cmdTextSb.Append("    , upd_login_user_id    = @UPDLOGINUSERID")
        cmdTextSb.Append("    , upd_datetime         = @UPDDATETIME")
        cmdTextSb.Append(" WHERE")
        cmdTextSb.Append("      kbn = @KBN")
        cmdTextSb.Append("   AND hosyousyo_no        = @HOSYOUSYONO")
        cmdTextSb.Append("   AND LEFT(bunrui_cd, 2) = LEFT(@BUNRUICD, 2)")
        cmdTextSb.Append("   AND gamen_hyouji_no     = @GAMENHYOUJINO")

        ' パラメータへ設定
        cmdParams = New SqlParameter() { _
            SQLHelper.MakeParam("@KBN", SqlDbType.Char, 1, nyuukinRec.Kbn), _
            SQLHelper.MakeParam("@HOSYOUSYONO", SqlDbType.VarChar, 10, nyuukinRec.HosyousyoNo), _
            SQLHelper.MakeParam("@BUNRUICD", SqlDbType.VarChar, 3, nyuukinRec.BunruiCd), _
            SQLHelper.MakeParam("@GAMENHYOUJINO", SqlDbType.Int, 4, nyuukinRec.GamenHyoujiNo), _
            SQLHelper.MakeParam("@ZEIKOMINYUUKINGAKU", SqlDbType.Int, 4, nyuukinRec.ZeikomiNyuukinGaku), _
            SQLHelper.MakeParam("@ZEIKOMIHENKINGAKU", SqlDbType.Int, 4, nyuukinRec.ZeikomiHenkinGaku), _
            SQLHelper.MakeParam("@SAISYUUNYUUKINDATE", SqlDbType.DateTime, 16, Date.Parse(dtTimeNow)), _
            SQLHelper.MakeParam("@UPDLOGINUSERID", SqlDbType.VarChar, 30, nyuukinRec.UpdLoginUserId), _
            SQLHelper.MakeParam("@UPDDATETIME", SqlDbType.DateTime, 16, DateTime.Now)}

        ' クエリ実行
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    cmdTextSb.ToString(), _
                                    cmdParams)
        Return intResult


    End Function

End Class

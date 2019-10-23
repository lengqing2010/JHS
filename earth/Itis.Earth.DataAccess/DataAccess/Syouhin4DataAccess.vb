Imports System
Imports System.text
Imports System.Data.SqlClient

''' <summary>
''' è§ïi4ÇÃìoò^ÉNÉâÉX
''' </summary>
''' <remarks></remarks>
Public Class Syouhin4DataAccess

    'EMABè·äQëŒâûèÓïÒäiî[èàóù
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

#Region "ÉÅÉìÉoïœêî"
    Dim connStr As String = ConnectionManager.Instance.ConnectionString
    Dim strLogic As New StringLogic
#End Region




#Region "ì@ï êøãÅÉfÅ[É^ìoò^"
    ''' <summary>
    ''' ì@ï êøãÅÉeÅ[ÉuÉãÇ÷ÉfÅ[É^Çìoò^ÇµÇ‹Ç∑
    ''' </summary>
    ''' <param name="teibetuRec">ì@ï êøãÅÉåÉRÅ[Éh</param>
    ''' <returns>ìoò^åãâ åèêî</returns>
    ''' <remarks></remarks>
    Public Function InsTeibetuSeikyuuData(ByVal teibetuRec As TeibetuSeikyuuRecord) As Integer

        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".InsTeibetuSeikyuuData", teibetuRec)

        Dim intResult As Integer = 0
        Dim commandTextSb As New StringBuilder()
        Dim cmdParams() As SqlClient.SqlParameter

        'ìoò^ópSQL
        commandTextSb.Append(" INSERT INTO t_teibetu_seikyuu ( ")
        commandTextSb.Append("     kbn, ")
        commandTextSb.Append("     hosyousyo_no, ")
        commandTextSb.Append("     bunrui_cd, ")
        commandTextSb.Append("     gamen_hyouji_no, ")
        commandTextSb.Append("     syouhin_cd, ")
        commandTextSb.Append("     uri_gaku, ")
        commandTextSb.Append("     siire_gaku, ")
        commandTextSb.Append("     zei_kbn, ")
        commandTextSb.Append("     seikyuusyo_hak_date, ")
        commandTextSb.Append("     uri_date, ")
        commandTextSb.Append("     denpyou_uri_date, ")
        commandTextSb.Append("     seikyuu_umu, ")
        commandTextSb.Append("     kakutei_kbn, ")
        commandTextSb.Append("     uri_keijyou_flg, ")
        commandTextSb.Append("     uri_keijyou_date, ")
        commandTextSb.Append("     bikou, ")
        commandTextSb.Append("     koumuten_seikyuu_gaku, ")
        commandTextSb.Append("     hattyuusyo_gaku, ")
        commandTextSb.Append("     hattyuusyo_kakunin_date, ")
        commandTextSb.Append("     tys_mitsyo_sakusei_date, ")
        commandTextSb.Append("     hattyuusyo_kakutei_flg, ")
        commandTextSb.Append("     ikkatu_nyuukin_flg, ")
        commandTextSb.Append("     seikyuu_saki_cd, ")
        commandTextSb.Append("     seikyuu_saki_brc, ")
        commandTextSb.Append("     seikyuu_saki_kbn, ")
        commandTextSb.Append("     tys_kaisya_cd, ")
        commandTextSb.Append("     tys_kaisya_jigyousyo_cd, ")
        commandTextSb.Append("     add_login_user_id, ")
        commandTextSb.Append("     add_datetime, ")
        commandTextSb.Append("     upd_login_user_id, ")
        commandTextSb.Append("     upd_datetime ")
        commandTextSb.Append(" ) SELECT ")
        commandTextSb.Append("     @KBN, ")
        commandTextSb.Append("     @HOSYOUSYONO, ")
        commandTextSb.Append("     @BUNRUICD, ")

        'commandTextSb.Append("     @GAMENHYOUJINO, ")
        commandTextSb.Append(" ( SELECT ")
        commandTextSb.Append("      MAX(gamen_hyouji_no) + 1 ")
        commandTextSb.Append("   FROM ")
        commandTextSb.Append("      t_teibetu_seikyuu ")
        commandTextSb.Append("   WHERE ")
        commandTextSb.Append("          kbn = @KBN ")
        commandTextSb.Append("      and hosyousyo_no = @HOSYOUSYONO ")
        commandTextSb.Append("      and bunrui_cd = @BUNRUICD ")
        commandTextSb.Append(" ), ")

        commandTextSb.Append("     @SYOUHINCD, ")
        commandTextSb.Append("     @URIGAKU, ")
        commandTextSb.Append("     @SIIREGAKU, ")
        commandTextSb.Append("     @ZEIKBN, ")
        commandTextSb.Append("     @SEIKYUUSYOHAKDATE, ")
        commandTextSb.Append("     @URIDATE, ")
        commandTextSb.Append("     @DENPYOUURIDATE, ")
        commandTextSb.Append("     @SEIKYUUUMU, ")
        commandTextSb.Append("     @KAKUTEIKBN, ")
        commandTextSb.Append("     @URIKEIJYOUFLG, ")
        commandTextSb.Append("     @URIKEIJYOUDATE, ")
        commandTextSb.Append("     @BIKOU, ")
        commandTextSb.Append("     @KOUMUTENSEIKYUUGAKU, ")
        commandTextSb.Append("     @HATTYUUSYOGAKU, ")
        commandTextSb.Append("     @HATTYUUSYOKAKUNINDATE, ")
        commandTextSb.Append("     @TYSMITSYOSAKUSEIDATE, ")
        commandTextSb.Append("     @HATTYUUSYOKAKUTEIFLG, ")
        commandTextSb.Append("     @IKKATUNYUUKUNFLG, ")
        commandTextSb.Append("     @SEIKYUUSAKICD, ")
        commandTextSb.Append("     @SEIKYUUSAKIBRC, ")
        commandTextSb.Append("     @SEIKYUUSAKIKBN, ")
        commandTextSb.Append("     @TYSKAISYACD, ")
        commandTextSb.Append("     @TYSKAISYAJIGYOUSYOCD, ")
        commandTextSb.Append("     @ADDLOGINUSERID, ")
        commandTextSb.Append("     @ADDDATETIME, ")
        commandTextSb.Append("     @UPDLOGINUSERID, ")
        commandTextSb.Append("     @UPDDATETIME ")

        cmdParams = New SqlParameter() { _
                SQLHelper.MakeParam("@KBN", SqlDbType.Char, 1, teibetuRec.Kbn), _
                SQLHelper.MakeParam("@HOSYOUSYONO", SqlDbType.VarChar, 10, teibetuRec.HosyousyoNo), _
                SQLHelper.MakeParam("@BUNRUICD", SqlDbType.VarChar, 3, teibetuRec.BunruiCd), _
                SQLHelper.MakeParam("@SYOUHINCD", SqlDbType.VarChar, 8, teibetuRec.SyouhinCd), _
                SQLHelper.MakeParam("@URIGAKU", SqlDbType.Int, 4, IIf(teibetuRec.UriGaku = Integer.MinValue, DBNull.Value, teibetuRec.UriGaku)), _
                SQLHelper.MakeParam("@SIIREGAKU", SqlDbType.Int, 4, IIf(teibetuRec.SiireGaku = Integer.MinValue, DBNull.Value, teibetuRec.SiireGaku)), _
                SQLHelper.MakeParam("@ZEIKBN", SqlDbType.VarChar, 1, teibetuRec.ZeiKbn), _
                SQLHelper.MakeParam("@SEIKYUUSYOHAKDATE", SqlDbType.DateTime, 16, IIf(teibetuRec.SeikyuusyoHakDate = DateTime.MinValue, DBNull.Value, teibetuRec.SeikyuusyoHakDate)), _
                SQLHelper.MakeParam("@URIDATE", SqlDbType.DateTime, 16, IIf(teibetuRec.UriDate = DateTime.MinValue, DBNull.Value, teibetuRec.UriDate)), _
                SQLHelper.MakeParam("@DENPYOUURIDATE", SqlDbType.DateTime, 16, IIf(teibetuRec.DenpyouUriDate = DateTime.MinValue, DBNull.Value, teibetuRec.DenpyouUriDate)), _
                SQLHelper.MakeParam("@SEIKYUUUMU", SqlDbType.Int, 4, IIf(teibetuRec.SeikyuuUmu = Integer.MinValue, DBNull.Value, teibetuRec.SeikyuuUmu)), _
                SQLHelper.MakeParam("@KAKUTEIKBN", SqlDbType.Int, 4, IIf(teibetuRec.KakuteiKbn = Integer.MinValue, DBNull.Value, teibetuRec.KakuteiKbn)), _
                SQLHelper.MakeParam("@URIKEIJYOUFLG", SqlDbType.Int, 4, IIf(teibetuRec.UriKeijyouFlg = Integer.MinValue, DBNull.Value, teibetuRec.UriKeijyouFlg)), _
                SQLHelper.MakeParam("@URIKEIJYOUDATE", SqlDbType.DateTime, 16, IIf(teibetuRec.UriKeijyouDate = DateTime.MinValue, DBNull.Value, teibetuRec.UriKeijyouDate)), _
                SQLHelper.MakeParam("@BIKOU", SqlDbType.VarChar, 40, teibetuRec.Bikou), _
                SQLHelper.MakeParam("@KOUMUTENSEIKYUUGAKU", SqlDbType.Int, 4, IIf(teibetuRec.KoumutenSeikyuuGaku = Integer.MinValue, DBNull.Value, teibetuRec.KoumutenSeikyuuGaku)), _
                SQLHelper.MakeParam("@HATTYUUSYOGAKU", SqlDbType.Int, 4, IIf(teibetuRec.HattyuusyoGaku = Integer.MinValue, DBNull.Value, teibetuRec.HattyuusyoGaku)), _
                SQLHelper.MakeParam("@HATTYUUSYOKAKUNINDATE", SqlDbType.DateTime, 16, IIf(teibetuRec.HattyuusyoKakuninDate = DateTime.MinValue, DBNull.Value, teibetuRec.HattyuusyoKakuninDate)), _
                SQLHelper.MakeParam("@TYSMITSYOSAKUSEIDATE", SqlDbType.DateTime, 16, IIf(teibetuRec.TysMitsyoSakuseiDate = DateTime.MinValue, DBNull.Value, teibetuRec.TysMitsyoSakuseiDate)), _
                SQLHelper.MakeParam("@HATTYUUSYOKAKUTEIFLG", SqlDbType.Int, 4, IIf(teibetuRec.HattyuusyoKakuteiFlg = Integer.MinValue, DBNull.Value, teibetuRec.HattyuusyoKakuteiFlg)), _
                SQLHelper.MakeParam("@IKKATUNYUUKUNFLG", SqlDbType.Int, 4, IIf(teibetuRec.IkkatuNyuukinFlg = Integer.MinValue, DBNull.Value, teibetuRec.IkkatuNyuukinFlg)), _
                SQLHelper.MakeParam("@SEIKYUUSAKICD", SqlDbType.VarChar, 5, teibetuRec.SeikyuuSakiCd), _
                SQLHelper.MakeParam("@SEIKYUUSAKIBRC", SqlDbType.VarChar, 2, teibetuRec.SeikyuuSakiBrc), _
                SQLHelper.MakeParam("@SEIKYUUSAKIKBN", SqlDbType.Char, 1, teibetuRec.SeikyuuSakiKbn), _
                SQLHelper.MakeParam("@TYSKAISYACD", SqlDbType.VarChar, 5, teibetuRec.TysKaisyaCd), _
                SQLHelper.MakeParam("@TYSKAISYAJIGYOUSYOCD", SqlDbType.VarChar, 2, teibetuRec.TysKaisyaJigyousyoCd), _
                SQLHelper.MakeParam("@ADDLOGINUSERID", SqlDbType.VarChar, 30, teibetuRec.AddLoginUserId), _
                SQLHelper.MakeParam("@ADDDATETIME", SqlDbType.DateTime, 16, IIf(teibetuRec.AddDatetime = DateTime.MinValue, DBNull.Value, teibetuRec.AddDatetime)), _
                SQLHelper.MakeParam("@UPDLOGINUSERID", SqlDbType.VarChar, 30, teibetuRec.UpdLoginUserId), _
                SQLHelper.MakeParam("@UPDDATETIME", SqlDbType.DateTime, 16, IIf(teibetuRec.UpdDatetime = DateTime.MinValue, DBNull.Value, teibetuRec.UpdDatetime))}


        ' ÉNÉGÉäé¿çs
        intResult = ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    commandTextSb.ToString(), _
                                    cmdParams)

        Return intResult
    End Function

#End Region

End Class

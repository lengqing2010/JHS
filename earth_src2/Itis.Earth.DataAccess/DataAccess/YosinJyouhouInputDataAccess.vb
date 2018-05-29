Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Imports System.text
Imports System.Data.SqlClient
''' <summary>与信管理情報を取得する</summary>
''' <remarks>与信管理情報照会を提供する</remarks>
''' <history>
''' <para>2009/07/16　呉営(大連情報システム部)　新規作成</para>
''' </history>
Public Class YosinJyouhouInputDataAccess
    ''' <summary>
    ''' コネクションストリング
    ''' </summary>
    ''' <remarks></remarks>
    Private connStr As String = ConnectionManager.Instance.ConnectionString

#Region "与信管理情報取得"
    ''' <summary>
    ''' 与信管理情報を取得する
    ''' </summary>
    ''' <param name="nayose_cd">名寄先コード</param>
    ''' <returns>与信管理情報データテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetYosinMeisai(ByVal nayose_cd As String, ByVal tyousa As Boolean, ByVal kouji As Boolean, ByVal sonota As Boolean, ByVal yotei As Boolean, ByVal jisseki As Boolean) As DataSet

        ' DataSetインスタンスの生成
        Dim dsYosinJyouhouInput As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder()

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        If (tyousa And jisseki) Or _
           (tyousa And yotei = False And jisseki = False) Or _
           (jisseki And tyousa = False And kouji = False And sonota = False) Then
            commandTextSb.AppendLine(" --調査(調査)実績 ")
            commandTextSb.AppendLine("  SELECT  m_todoufuken.todouhuken_cd,m_todoufuken.todouhuken_mei,  ")
            commandTextSb.AppendLine(" m_kameiten.kameiten_cd,m_kameiten.kameiten_mei1,t_jiban.kbn + t_jiban.hosyousyo_no AS hosyousyo_no,t_jiban.sesyu_mei,t_jiban.irai_date, ")
            commandTextSb.AppendLine(" '調査' as col1,'実績' as col2,ISNULL(t_teibetu_seikyuu.uri_date,'9999/12/31') AS uri_date,ISNULL(t_jiban.syoudakusyo_tys_date,t_jiban.tys_kibou_date) AS syoudakusyo_tys_date, ")
            commandTextSb.AppendLine(" FLOOR(uri_gaku+uri_gaku*m_syouhizei.zeiritu) as kin,m_syouhin.syouhin_mei ")
            commandTextSb.AppendLine(" FROM m_yosinkanri  WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" LEFT JOIN m_nayose WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" ON m_nayose.nayose_saki_cd=m_yosinkanri.nayose_saki_cd ")
            commandTextSb.AppendLine(" LEFT JOIN m_kameiten WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" ON m_kameiten.tys_seikyuu_saki=m_nayose.seikyuu_saki_cd ")
            commandTextSb.AppendLine(" LEFT JOIN t_jiban WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" ON m_kameiten.kameiten_cd=t_jiban.kameiten_cd ")
            commandTextSb.AppendLine(" LEFT JOIN t_teibetu_seikyuu WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" ON t_jiban.kbn=t_teibetu_seikyuu.kbn ")
            commandTextSb.AppendLine(" AND t_jiban.hosyousyo_no=t_teibetu_seikyuu.hosyousyo_no ")
            commandTextSb.AppendLine(" LEFT JOIN m_syouhin WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" on t_teibetu_seikyuu.syouhin_cd=m_syouhin.syouhin_cd ")
            commandTextSb.AppendLine(" AND t_teibetu_seikyuu.bunrui_cd=m_syouhin.souko_cd ")
            commandTextSb.AppendLine(" LEFT JOIN m_syouhizei WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" on m_syouhizei.zei_kbn=t_teibetu_seikyuu.zei_kbn ")
            commandTextSb.AppendLine(" LEFT JOIN m_todoufuken WITH (READCOMMITTED)  ")
            commandTextSb.AppendLine(" ON m_kameiten.todouhuken_cd=m_todoufuken.todouhuken_cd ")


            commandTextSb.AppendLine(" WHERE   ")
            commandTextSb.AppendLine(" m_yosinkanri.nayose_saki_cd=@nayose_saki_cd and  ")
            commandTextSb.AppendLine(" t_teibetu_seikyuu.bunrui_cd<='120' ")
            commandTextSb.AppendLine(" and  t_teibetu_seikyuu.seikyuu_umu=@umu ")
            commandTextSb.AppendLine(" and  t_jiban.data_haki_syubetu='0' ")
            commandTextSb.AppendLine(" and t_teibetu_seikyuu.uri_date>=convert(datetime,convert(varchar(4),datepart(yy,dateadd(m,1,m_yosinkanri.zengetsu_saiken_set_date)))+'/'+right('0'+convert(varchar(2),datepart(m,DATEADD(Month, 1, m_yosinkanri.zengetsu_saiken_set_date))),2)+'/01') ")
            commandTextSb.AppendLine("UNION ALL")
        End If

        If (tyousa And yotei) Or _
           (tyousa And yotei = False And jisseki = False) Or _
           (yotei And tyousa = False And kouji = False And sonota = False) Then
            commandTextSb.AppendLine(" --調査(調査)予定 ")
            commandTextSb.AppendLine("  SELECT  m_todoufuken.todouhuken_cd,m_todoufuken.todouhuken_mei,  ")
            commandTextSb.AppendLine(" m_kameiten.kameiten_cd,m_kameiten.kameiten_mei1,t_jiban.kbn + t_jiban.hosyousyo_no AS hosyousyo_no,t_jiban.sesyu_mei,t_jiban.irai_date, ")
            commandTextSb.AppendLine(" '調査' as col1,'予定' as col2,ISNULL(t_teibetu_seikyuu.uri_date,'9999/12/31') AS uri_date,ISNULL(t_jiban.syoudakusyo_tys_date,t_jiban.tys_kibou_date) AS syoudakusyo_tys_date, ")
            commandTextSb.AppendLine(" FLOOR(uri_gaku+uri_gaku*m_syouhizei.zeiritu) as kin,m_syouhin.syouhin_mei ")
            commandTextSb.AppendLine(" FROM m_yosinkanri  WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" LEFT JOIN m_nayose WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" ON m_nayose.nayose_saki_cd=m_yosinkanri.nayose_saki_cd ")
            commandTextSb.AppendLine(" LEFT JOIN m_kameiten WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" ON m_kameiten.tys_seikyuu_saki=m_nayose.seikyuu_saki_cd ")
            commandTextSb.AppendLine(" LEFT JOIN t_jiban WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" ON m_kameiten.kameiten_cd=t_jiban.kameiten_cd ")
            commandTextSb.AppendLine(" LEFT JOIN t_teibetu_seikyuu WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" ON t_jiban.kbn=t_teibetu_seikyuu.kbn ")
            commandTextSb.AppendLine(" AND t_jiban.hosyousyo_no=t_teibetu_seikyuu.hosyousyo_no ")
            commandTextSb.AppendLine(" LEFT JOIN m_syouhin WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" on t_teibetu_seikyuu.syouhin_cd=m_syouhin.syouhin_cd ")
            commandTextSb.AppendLine(" AND t_teibetu_seikyuu.bunrui_cd=m_syouhin.souko_cd ")
            commandTextSb.AppendLine(" LEFT JOIN m_syouhizei WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" on m_syouhizei.zei_kbn=t_teibetu_seikyuu.zei_kbn ")
            commandTextSb.AppendLine(" LEFT JOIN m_todoufuken WITH (READCOMMITTED)  ")
            commandTextSb.AppendLine(" ON m_kameiten.todouhuken_cd=m_todoufuken.todouhuken_cd ")

            commandTextSb.AppendLine(" WHERE   ")
            commandTextSb.AppendLine(" m_yosinkanri.nayose_saki_cd=@nayose_saki_cd and  ")
            commandTextSb.AppendLine(" t_teibetu_seikyuu.bunrui_cd<='120' ")
            commandTextSb.AppendLine(" and  t_teibetu_seikyuu.seikyuu_umu=@umu ")
            commandTextSb.AppendLine(" and t_teibetu_seikyuu.uri_date IS NULL ")
            commandTextSb.AppendLine(" and  t_jiban.data_haki_syubetu='0' ")
            commandTextSb.AppendLine(" and  ISNULL(t_jiban.syoudakusyo_tys_date,t_jiban.tys_kibou_date)>=DATEADD(day,-30, CONVERT(CHAR(10),getdate(),111))  ")
            commandTextSb.AppendLine(" and  ISNULL(t_jiban.syoudakusyo_tys_date,t_jiban.tys_kibou_date)<=DATEADD(day,30, CONVERT(CHAR(10),getdate(),111)) ")

            commandTextSb.AppendLine("UNION ALL")
        End If
        If (tyousa And jisseki) Or _
           (tyousa And yotei = False And jisseki = False) Or _
           (jisseki And tyousa = False And kouji = False And sonota = False) Then
            commandTextSb.AppendLine(" --調査(再発行・解約払い戻し)実績 ")
            commandTextSb.AppendLine("  SELECT  m_todoufuken.todouhuken_cd,m_todoufuken.todouhuken_mei,  ")
            commandTextSb.AppendLine(" m_kameiten.kameiten_cd,m_kameiten.kameiten_mei1,t_jiban.kbn + t_jiban.hosyousyo_no AS hosyousyo_no,t_jiban.sesyu_mei,t_jiban.irai_date, ")
            commandTextSb.AppendLine(" '調査' as col1,'実績' as col2,ISNULL(t_teibetu_seikyuu.uri_date,'9999/12/31') AS uri_date,ISNULL(t_jiban.syoudakusyo_tys_date,t_jiban.tys_kibou_date) AS syoudakusyo_tys_date, ")
            commandTextSb.AppendLine(" FLOOR(uri_gaku+uri_gaku*m_syouhizei.zeiritu) as kin,m_syouhin.syouhin_mei ")
            commandTextSb.AppendLine(" FROM m_yosinkanri  WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" LEFT JOIN m_nayose WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" ON m_nayose.nayose_saki_cd=m_yosinkanri.nayose_saki_cd ")
            commandTextSb.AppendLine(" LEFT JOIN m_kameiten WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" ON m_kameiten.tys_seikyuu_saki=m_nayose.seikyuu_saki_cd ")
            commandTextSb.AppendLine(" LEFT JOIN t_jiban WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" ON m_kameiten.kameiten_cd=t_jiban.kameiten_cd ")
            commandTextSb.AppendLine(" LEFT JOIN t_teibetu_seikyuu WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" ON t_jiban.kbn=t_teibetu_seikyuu.kbn ")
            commandTextSb.AppendLine(" AND t_jiban.hosyousyo_no=t_teibetu_seikyuu.hosyousyo_no ")
            commandTextSb.AppendLine(" LEFT JOIN m_syouhin WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" on t_teibetu_seikyuu.syouhin_cd=m_syouhin.syouhin_cd ")
            commandTextSb.AppendLine(" AND t_teibetu_seikyuu.bunrui_cd=m_syouhin.souko_cd ")
            commandTextSb.AppendLine(" LEFT JOIN m_syouhizei WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" on m_syouhizei.zei_kbn=t_teibetu_seikyuu.zei_kbn ")
            commandTextSb.AppendLine(" LEFT JOIN m_todoufuken WITH (READCOMMITTED)  ")
            commandTextSb.AppendLine(" ON m_kameiten.todouhuken_cd=m_todoufuken.todouhuken_cd ")

            commandTextSb.AppendLine(" WHERE   ")
            commandTextSb.AppendLine(" m_yosinkanri.nayose_saki_cd=@nayose_saki_cd and  ")
            commandTextSb.AppendLine(" t_teibetu_seikyuu.bunrui_cd IN ('150' ,'170' , '180') ")
            commandTextSb.AppendLine(" and  t_teibetu_seikyuu.seikyuu_umu=@umu ")
            commandTextSb.AppendLine(" and  t_jiban.data_haki_syubetu='0' ")
            commandTextSb.AppendLine(" and t_teibetu_seikyuu.uri_date>=convert(datetime,convert(varchar(4),datepart(yy,dateadd(m,1,m_yosinkanri.zengetsu_saiken_set_date)))+'/'+right('0'+convert(varchar(2),datepart(m,DATEADD(Month, 1, m_yosinkanri.zengetsu_saiken_set_date))),2)+'/01') ")
            commandTextSb.AppendLine("UNION ALL")
        End If
        If (kouji And jisseki) Or _
           (kouji And yotei = False And jisseki = False) Or _
           (jisseki And tyousa = False And kouji = False And sonota = False) Then
            commandTextSb.AppendLine(" --工事(工事)実績 ")
            commandTextSb.AppendLine("  SELECT  m_todoufuken.todouhuken_cd,m_todoufuken.todouhuken_mei,  ")
            commandTextSb.AppendLine(" m_kameiten.kameiten_cd,m_kameiten.kameiten_mei1,t_jiban.kbn + t_jiban.hosyousyo_no AS hosyousyo_no,t_jiban.sesyu_mei,t_jiban.irai_date, ")
            commandTextSb.AppendLine(" '工事' as col1,'実績' as col2,ISNULL(t_teibetu_seikyuu.uri_date,'9999/12/31') AS uri_date,t_jiban.kairy_koj_kanry_yotei_date AS syoudakusyo_tys_date, ")
            commandTextSb.AppendLine(" FLOOR(uri_gaku+uri_gaku*m_syouhizei.zeiritu) as kin,m_syouhin.syouhin_mei ")
            commandTextSb.AppendLine(" FROM m_yosinkanri  WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" LEFT JOIN m_nayose WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" ON m_nayose.nayose_saki_cd=m_yosinkanri.nayose_saki_cd ")
            commandTextSb.AppendLine(" LEFT JOIN m_kameiten WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" ON m_kameiten.koj_seikyuusaki=m_nayose.seikyuu_saki_cd ")

            commandTextSb.AppendLine(" LEFT JOIN t_jiban WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" ON m_kameiten.kameiten_cd=t_jiban.kameiten_cd ")
            commandTextSb.AppendLine(" LEFT JOIN t_teibetu_seikyuu WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" ON t_jiban.kbn=t_teibetu_seikyuu.kbn ")
            commandTextSb.AppendLine(" AND t_jiban.hosyousyo_no=t_teibetu_seikyuu.hosyousyo_no ")
            commandTextSb.AppendLine(" LEFT JOIN m_syouhin WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" on t_teibetu_seikyuu.syouhin_cd=m_syouhin.syouhin_cd ")
            commandTextSb.AppendLine(" AND t_teibetu_seikyuu.bunrui_cd=m_syouhin.souko_cd ")
            commandTextSb.AppendLine(" LEFT JOIN m_syouhizei WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" on m_syouhizei.zei_kbn=t_teibetu_seikyuu.zei_kbn ")
            commandTextSb.AppendLine(" LEFT JOIN m_todoufuken WITH (READCOMMITTED)  ")
            commandTextSb.AppendLine(" ON m_kameiten.todouhuken_cd=m_todoufuken.todouhuken_cd ")

            commandTextSb.AppendLine(" WHERE   ")
            commandTextSb.AppendLine(" m_yosinkanri.nayose_saki_cd=@nayose_saki_cd and  ")
            commandTextSb.AppendLine(" t_teibetu_seikyuu.bunrui_cd ='130' ")
            commandTextSb.AppendLine(" and  t_teibetu_seikyuu.seikyuu_umu=@umu ")
            commandTextSb.AppendLine(" and  t_jiban.data_haki_syubetu='0' ")
            commandTextSb.AppendLine(" and t_teibetu_seikyuu.uri_date>=convert(datetime,convert(varchar(4),datepart(yy,dateadd(m,1,m_yosinkanri.zengetsu_saiken_set_date)))+'/'+right('0'+convert(varchar(2),datepart(m,DATEADD(Month, 1, m_yosinkanri.zengetsu_saiken_set_date))),2)+'/01') ")
            commandTextSb.AppendLine(" AND (t_jiban.koj_gaisya_seikyuu_umu IS NULL OR t_jiban.koj_gaisya_seikyuu_umu=0 ) ")

            commandTextSb.AppendLine("UNION ALL")
        End If
        If (kouji And yotei) Or _
           (kouji And yotei = False And jisseki = False) Or _
           (yotei And tyousa = False And kouji = False And sonota = False) Then
            commandTextSb.AppendLine(" --工事(工事)予定 ")
            commandTextSb.AppendLine("  SELECT  m_todoufuken.todouhuken_cd,m_todoufuken.todouhuken_mei,  ")
            commandTextSb.AppendLine(" m_kameiten.kameiten_cd,m_kameiten.kameiten_mei1,t_jiban.kbn + t_jiban.hosyousyo_no AS hosyousyo_no,t_jiban.sesyu_mei,t_jiban.irai_date, ")
            commandTextSb.AppendLine(" '工事' as col1,'予定' as col2,ISNULL(t_teibetu_seikyuu.uri_date,'9999/12/31') AS uri_date,t_jiban.kairy_koj_kanry_yotei_date AS syoudakusyo_tys_date, ")
            commandTextSb.AppendLine(" FLOOR(uri_gaku+uri_gaku*m_syouhizei.zeiritu) as kin,m_syouhin.syouhin_mei ")
            commandTextSb.AppendLine(" FROM m_yosinkanri  WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" LEFT JOIN m_nayose WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" ON m_nayose.nayose_saki_cd=m_yosinkanri.nayose_saki_cd ")
            commandTextSb.AppendLine(" LEFT JOIN m_kameiten WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" ON m_kameiten.koj_seikyuusaki=m_nayose.seikyuu_saki_cd ")

            commandTextSb.AppendLine(" LEFT JOIN t_jiban WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" ON m_kameiten.kameiten_cd=t_jiban.kameiten_cd ")
            commandTextSb.AppendLine(" LEFT JOIN t_teibetu_seikyuu WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" ON t_jiban.kbn=t_teibetu_seikyuu.kbn ")
            commandTextSb.AppendLine(" AND t_jiban.hosyousyo_no=t_teibetu_seikyuu.hosyousyo_no ")
            commandTextSb.AppendLine(" LEFT JOIN m_syouhin WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" on t_teibetu_seikyuu.syouhin_cd=m_syouhin.syouhin_cd ")
            commandTextSb.AppendLine(" AND t_teibetu_seikyuu.bunrui_cd=m_syouhin.souko_cd ")
            commandTextSb.AppendLine(" LEFT JOIN m_syouhizei WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" on m_syouhizei.zei_kbn=t_teibetu_seikyuu.zei_kbn ")

            commandTextSb.AppendLine(" LEFT JOIN m_todoufuken WITH (READCOMMITTED)  ")
            commandTextSb.AppendLine(" ON m_kameiten.todouhuken_cd=m_todoufuken.todouhuken_cd ")
            commandTextSb.AppendLine(" WHERE   ")
            commandTextSb.AppendLine(" m_yosinkanri.nayose_saki_cd=@nayose_saki_cd and  ")
            commandTextSb.AppendLine(" t_teibetu_seikyuu.bunrui_cd='130' ")
            commandTextSb.AppendLine(" and  t_teibetu_seikyuu.seikyuu_umu=@umu ")
            commandTextSb.AppendLine(" and t_teibetu_seikyuu.uri_date IS NULL ")
            commandTextSb.AppendLine(" and  t_jiban.data_haki_syubetu='0' ")
            commandTextSb.AppendLine(" AND (t_jiban.koj_gaisya_seikyuu_umu IS NULL OR t_jiban.koj_gaisya_seikyuu_umu=0 ) ")
            commandTextSb.AppendLine(" and  t_jiban.kairy_koj_kanry_yotei_date>=DATEADD(day,-30, CONVERT(CHAR(10),getdate(),111))  ")
            commandTextSb.AppendLine(" and  t_jiban.kairy_koj_kanry_yotei_date<=DATEADD(day,30, CONVERT(CHAR(10),getdate(),111)) ")

            commandTextSb.AppendLine("UNION ALL")
        End If
        If (kouji And jisseki) Or _
           (kouji And yotei = False And jisseki = False) Or _
           (jisseki And tyousa = False And kouji = False And sonota = False) Then
            commandTextSb.AppendLine(" --工事(追加工事)実績 ")
            commandTextSb.AppendLine("  SELECT  m_todoufuken.todouhuken_cd,m_todoufuken.todouhuken_mei,  ")
            commandTextSb.AppendLine(" m_kameiten.kameiten_cd,m_kameiten.kameiten_mei1,t_jiban.kbn + t_jiban.hosyousyo_no AS hosyousyo_no,t_jiban.sesyu_mei,t_jiban.irai_date, ")
            commandTextSb.AppendLine(" '工事' as col1,'実績' as col2,ISNULL(t_teibetu_seikyuu.uri_date,'9999/12/31') AS uri_date,t_jiban.kairy_koj_kanry_yotei_date AS syoudakusyo_tys_date, ")
            commandTextSb.AppendLine(" FLOOR(uri_gaku+uri_gaku*m_syouhizei.zeiritu) as kin,m_syouhin.syouhin_mei ")
            commandTextSb.AppendLine(" FROM m_yosinkanri  WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" LEFT JOIN m_nayose WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" ON m_nayose.nayose_saki_cd=m_yosinkanri.nayose_saki_cd ")
            commandTextSb.AppendLine(" LEFT JOIN m_kameiten WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" ON m_kameiten.koj_seikyuusaki=m_nayose.seikyuu_saki_cd ")

            commandTextSb.AppendLine(" LEFT JOIN t_jiban WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" ON m_kameiten.kameiten_cd=t_jiban.kameiten_cd ")
            commandTextSb.AppendLine(" LEFT JOIN t_teibetu_seikyuu WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" ON t_jiban.kbn=t_teibetu_seikyuu.kbn ")
            commandTextSb.AppendLine(" AND t_jiban.hosyousyo_no=t_teibetu_seikyuu.hosyousyo_no ")
            commandTextSb.AppendLine(" LEFT JOIN m_syouhin WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" on t_teibetu_seikyuu.syouhin_cd=m_syouhin.syouhin_cd ")
            commandTextSb.AppendLine(" AND t_teibetu_seikyuu.bunrui_cd=m_syouhin.souko_cd ")
            commandTextSb.AppendLine(" LEFT JOIN m_syouhizei WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" on m_syouhizei.zei_kbn=t_teibetu_seikyuu.zei_kbn ")
            commandTextSb.AppendLine(" LEFT JOIN m_todoufuken WITH (READCOMMITTED)  ")
            commandTextSb.AppendLine(" ON m_kameiten.todouhuken_cd=m_todoufuken.todouhuken_cd ")

            commandTextSb.AppendLine(" WHERE   ")
            commandTextSb.AppendLine(" m_yosinkanri.nayose_saki_cd=@nayose_saki_cd and  ")
            commandTextSb.AppendLine(" t_teibetu_seikyuu.bunrui_cd='140' ")
            commandTextSb.AppendLine(" and  t_teibetu_seikyuu.seikyuu_umu=@umu ")
            commandTextSb.AppendLine(" and  t_jiban.data_haki_syubetu='0' ")
            commandTextSb.AppendLine(" and t_teibetu_seikyuu.uri_date>=convert(datetime,convert(varchar(4),datepart(yy,dateadd(m,1,m_yosinkanri.zengetsu_saiken_set_date)))+'/'+right('0'+convert(varchar(2),datepart(m,DATEADD(Month, 1, m_yosinkanri.zengetsu_saiken_set_date))),2)+'/01') ")
            commandTextSb.AppendLine(" AND (t_jiban.t_koj_kaisya_seikyuu_umu IS NULL OR t_jiban.t_koj_kaisya_seikyuu_umu=0 ) ")

            commandTextSb.AppendLine("UNION ALL")
        End If
        If (kouji And yotei) Or _
           (kouji And yotei = False And jisseki = False) Or _
           (yotei And tyousa = False And kouji = False And sonota = False) Then
            commandTextSb.AppendLine(" --工事(追加工事)予定 ")
            commandTextSb.AppendLine("  SELECT  m_todoufuken.todouhuken_cd,m_todoufuken.todouhuken_mei,  ")
            commandTextSb.AppendLine(" m_kameiten.kameiten_cd,m_kameiten.kameiten_mei1,t_jiban.kbn + t_jiban.hosyousyo_no AS hosyousyo_no,t_jiban.sesyu_mei,t_jiban.irai_date, ")
            commandTextSb.AppendLine(" '工事' as col1,'予定' as col2,ISNULL(t_teibetu_seikyuu.uri_date,'9999/12/31') AS uri_date,t_jiban.kairy_koj_kanry_yotei_date AS syoudakusyo_tys_date, ")
            commandTextSb.AppendLine(" FLOOR(uri_gaku+uri_gaku*m_syouhizei.zeiritu) as kin,m_syouhin.syouhin_mei ")
            commandTextSb.AppendLine(" FROM m_yosinkanri  WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" LEFT JOIN m_nayose WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" ON m_nayose.nayose_saki_cd=m_yosinkanri.nayose_saki_cd ")
            commandTextSb.AppendLine(" LEFT JOIN m_kameiten WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" ON m_kameiten.koj_seikyuusaki=m_nayose.seikyuu_saki_cd ")

            commandTextSb.AppendLine(" LEFT JOIN t_jiban WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" ON m_kameiten.kameiten_cd=t_jiban.kameiten_cd ")
            commandTextSb.AppendLine(" LEFT JOIN t_teibetu_seikyuu WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" ON t_jiban.kbn=t_teibetu_seikyuu.kbn ")
            commandTextSb.AppendLine(" AND t_jiban.hosyousyo_no=t_teibetu_seikyuu.hosyousyo_no ")
            commandTextSb.AppendLine(" LEFT JOIN m_syouhin WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" on t_teibetu_seikyuu.syouhin_cd=m_syouhin.syouhin_cd ")
            commandTextSb.AppendLine(" AND t_teibetu_seikyuu.bunrui_cd=m_syouhin.souko_cd ")
            commandTextSb.AppendLine(" LEFT JOIN m_syouhizei WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" on m_syouhizei.zei_kbn=t_teibetu_seikyuu.zei_kbn ")
            commandTextSb.AppendLine(" LEFT JOIN m_todoufuken WITH (READCOMMITTED)  ")
            commandTextSb.AppendLine(" ON m_kameiten.todouhuken_cd=m_todoufuken.todouhuken_cd ")

            commandTextSb.AppendLine(" WHERE   ")
            commandTextSb.AppendLine(" m_yosinkanri.nayose_saki_cd=@nayose_saki_cd and  ")
            commandTextSb.AppendLine(" t_teibetu_seikyuu.bunrui_cd='140' ")
            commandTextSb.AppendLine(" and  t_teibetu_seikyuu.seikyuu_umu=@umu ")
            commandTextSb.AppendLine(" and t_teibetu_seikyuu.uri_date IS NULL ")
            commandTextSb.AppendLine(" and  t_jiban.data_haki_syubetu='0' ")
            commandTextSb.AppendLine(" AND (t_jiban.t_koj_kaisya_seikyuu_umu IS NULL OR t_jiban.t_koj_kaisya_seikyuu_umu=0 ) ")
            commandTextSb.AppendLine(" and  t_jiban.t_koj_kanry_yotei_date>=DATEADD(day,-30, CONVERT(CHAR(10),getdate(),111))  ")
            commandTextSb.AppendLine(" and  t_jiban.t_koj_kanry_yotei_date<=DATEADD(day,30, CONVERT(CHAR(10),getdate(),111)) ")
            commandTextSb.AppendLine("UNION ALL")
        End If
        If (kouji And jisseki) Or _
           (kouji And yotei = False And jisseki = False) Or _
           (jisseki And tyousa = False And kouji = False And sonota = False) Then
            commandTextSb.AppendLine(" --工事(再発行・解約払い戻し)実績 ")
            commandTextSb.AppendLine("  SELECT  m_todoufuken.todouhuken_cd,m_todoufuken.todouhuken_mei,  ")
            commandTextSb.AppendLine(" m_kameiten.kameiten_cd,m_kameiten.kameiten_mei1,t_jiban.kbn + t_jiban.hosyousyo_no AS hosyousyo_no,t_jiban.sesyu_mei,t_jiban.irai_date, ")
            commandTextSb.AppendLine(" '工事' as col1,'実績' as col2,ISNULL(t_teibetu_seikyuu.uri_date,'9999/12/31') AS uri_date,t_jiban.kairy_koj_kanry_yotei_date AS syoudakusyo_tys_date, ")
            commandTextSb.AppendLine(" FLOOR(uri_gaku+uri_gaku*m_syouhizei.zeiritu) as kin,m_syouhin.syouhin_mei ")
            commandTextSb.AppendLine(" FROM m_yosinkanri  WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" LEFT JOIN m_nayose WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" ON m_nayose.nayose_saki_cd=m_yosinkanri.nayose_saki_cd ")
            commandTextSb.AppendLine(" LEFT JOIN m_kameiten WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" ON m_kameiten.koj_seikyuusaki=m_nayose.seikyuu_saki_cd ")

            commandTextSb.AppendLine(" LEFT JOIN t_jiban WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" ON m_kameiten.kameiten_cd=t_jiban.kameiten_cd ")
            commandTextSb.AppendLine(" LEFT JOIN t_teibetu_seikyuu WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" ON t_jiban.kbn=t_teibetu_seikyuu.kbn ")
            commandTextSb.AppendLine(" AND t_jiban.hosyousyo_no=t_teibetu_seikyuu.hosyousyo_no ")
            commandTextSb.AppendLine(" LEFT JOIN m_syouhin WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" on t_teibetu_seikyuu.syouhin_cd=m_syouhin.syouhin_cd ")
            commandTextSb.AppendLine(" AND t_teibetu_seikyuu.bunrui_cd=m_syouhin.souko_cd ")
            commandTextSb.AppendLine(" LEFT JOIN m_syouhizei WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" on m_syouhizei.zei_kbn=t_teibetu_seikyuu.zei_kbn ")
            commandTextSb.AppendLine(" LEFT JOIN m_todoufuken WITH (READCOMMITTED)  ")
            commandTextSb.AppendLine(" ON m_kameiten.todouhuken_cd=m_todoufuken.todouhuken_cd ")

            commandTextSb.AppendLine(" WHERE   ")
            commandTextSb.AppendLine(" m_yosinkanri.nayose_saki_cd=@nayose_saki_cd and  ")
            commandTextSb.AppendLine(" t_teibetu_seikyuu.bunrui_cd='160' ")
            commandTextSb.AppendLine(" and  t_teibetu_seikyuu.seikyuu_umu=@umu ")
            commandTextSb.AppendLine(" and  t_jiban.data_haki_syubetu='0' ")
            commandTextSb.AppendLine(" and t_teibetu_seikyuu.uri_date>=convert(datetime,convert(varchar(4),datepart(yy,dateadd(m,1,m_yosinkanri.zengetsu_saiken_set_date)))+'/'+right('0'+convert(varchar(2),datepart(m,DATEADD(Month, 1, m_yosinkanri.zengetsu_saiken_set_date))),2)+'/01') ")
            commandTextSb.AppendLine("UNION ALL")
        End If
        If (sonota And jisseki) Or _
           (sonota And yotei = False And jisseki = False) Or _
           (jisseki And tyousa = False And kouji = False And sonota = False) Then
            commandTextSb.AppendLine(" --その他(販促品)実績 ")
            commandTextSb.AppendLine("  SELECT  m_todoufuken.todouhuken_cd,m_todoufuken.todouhuken_mei,  ")
            commandTextSb.AppendLine(" m_kameiten.kameiten_cd,m_kameiten.kameiten_mei1,'' AS hosyousyo_no,'' as sesyu_mei,'' as irai_date, ")
            commandTextSb.AppendLine(" 'その他' as col1,'実績' as col2,ISNULL(t_tenbetu_syoki_seikyuu.uri_date,'9999/12/31') AS uri_date,'' AS syoudakusyo_tys_date, ")
            commandTextSb.AppendLine(" FLOOR(t_tenbetu_syoki_seikyuu.uri_gaku+t_tenbetu_syoki_seikyuu.uri_gaku*m_syouhizei.zeiritu) as kin,m_syouhin.syouhin_mei ")
            commandTextSb.AppendLine(" FROM m_yosinkanri  WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" LEFT JOIN m_nayose WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" ON m_nayose.nayose_saki_cd=m_yosinkanri.nayose_saki_cd ")
            commandTextSb.AppendLine(" LEFT JOIN m_kameiten WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" ON m_kameiten.hansokuhin_seikyuusaki=m_nayose.seikyuu_saki_cd ")

            commandTextSb.AppendLine(" LEFT JOIN t_tenbetu_syoki_seikyuu WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" ON m_kameiten.kameiten_cd=t_tenbetu_syoki_seikyuu.mise_cd ")
            commandTextSb.AppendLine(" LEFT JOIN m_syouhin WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" on t_tenbetu_syoki_seikyuu.syouhin_cd=m_syouhin.syouhin_cd ")
            commandTextSb.AppendLine(" AND t_tenbetu_syoki_seikyuu.bunrui_cd=m_syouhin.souko_cd ")
            commandTextSb.AppendLine(" LEFT JOIN m_syouhizei WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" on m_syouhizei.zei_kbn=t_tenbetu_syoki_seikyuu.zei_kbn ")
            commandTextSb.AppendLine(" LEFT JOIN m_todoufuken WITH (READCOMMITTED)  ")
            commandTextSb.AppendLine(" ON m_kameiten.todouhuken_cd=m_todoufuken.todouhuken_cd ")

            commandTextSb.AppendLine(" WHERE   ")
            commandTextSb.AppendLine(" m_yosinkanri.nayose_saki_cd=@nayose_saki_cd   ")
            commandTextSb.AppendLine(" and t_tenbetu_syoki_seikyuu.uri_date>=convert(datetime,convert(varchar(4),datepart(yy,dateadd(m,1,m_yosinkanri.zengetsu_saiken_set_date)))+'/'+right('0'+convert(varchar(2),datepart(m,DATEADD(Month, 1, m_yosinkanri.zengetsu_saiken_set_date))),2)+'/01') ")
            commandTextSb.AppendLine(" --その他(販促品)実績 ")
            commandTextSb.AppendLine("UNION ALL")
            commandTextSb.AppendLine("  SELECT  m_todoufuken.todouhuken_cd,m_todoufuken.todouhuken_mei,  ")
            commandTextSb.AppendLine(" m_kameiten.kameiten_cd,m_kameiten.kameiten_mei1,'' AS hosyousyo_no,'' as sesyu_mei,'' as irai_date, ")
            commandTextSb.AppendLine(" 'その他' as col1,'実績' as col2,ISNULL(t_tenbetu_seikyuu.uri_date,'9999/12/31') AS uri_date,'' AS syoudakusyo_tys_date, ")
            commandTextSb.AppendLine(" FLOOR(t_tenbetu_seikyuu.tanka*t_tenbetu_seikyuu.suu+t_tenbetu_seikyuu.tanka*t_tenbetu_seikyuu.suu*m_syouhizei.zeiritu) as kin,m_syouhin.syouhin_mei ")
            commandTextSb.AppendLine(" FROM m_yosinkanri  WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" LEFT JOIN m_nayose WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" ON m_nayose.nayose_saki_cd=m_yosinkanri.nayose_saki_cd ")
            commandTextSb.AppendLine(" LEFT JOIN m_kameiten WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" ON m_kameiten.hansokuhin_seikyuusaki=m_nayose.seikyuu_saki_cd ")

            commandTextSb.AppendLine(" LEFT JOIN t_tenbetu_seikyuu WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" ON m_kameiten.kameiten_cd=t_tenbetu_seikyuu.mise_cd ")
            commandTextSb.AppendLine(" LEFT JOIN m_syouhin WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" on t_tenbetu_seikyuu.syouhin_cd=m_syouhin.syouhin_cd ")
            commandTextSb.AppendLine(" AND t_tenbetu_seikyuu.bunrui_cd=m_syouhin.souko_cd ")
            commandTextSb.AppendLine(" LEFT JOIN m_syouhizei WITH (READCOMMITTED) ")
            commandTextSb.AppendLine(" on m_syouhizei.zei_kbn=t_tenbetu_seikyuu.zei_kbn ")
            commandTextSb.AppendLine(" LEFT JOIN m_todoufuken WITH (READCOMMITTED)  ")
            commandTextSb.AppendLine(" ON m_kameiten.todouhuken_cd=m_todoufuken.todouhuken_cd ")

            commandTextSb.AppendLine(" WHERE   ")
            commandTextSb.AppendLine(" m_yosinkanri.nayose_saki_cd=@nayose_saki_cd   ")
            commandTextSb.AppendLine(" and t_tenbetu_seikyuu.uri_date>=convert(datetime,convert(varchar(4),datepart(yy,dateadd(m,1,m_yosinkanri.zengetsu_saiken_set_date)))+'/'+right('0'+convert(varchar(2),datepart(m,DATEADD(Month, 1, m_yosinkanri.zengetsu_saiken_set_date))),2)+'/01') ")
            commandTextSb.AppendLine("UNION ALL")
        End If



        'パラメータの設定
        paramList.Add(MakeParam("@nayose_saki_cd", SqlDbType.VarChar, 5, nayose_cd))
        paramList.Add(MakeParam("@umu", SqlDbType.VarChar, 1, "1"))
        If commandTextSb.ToString() = "" Then
            ' 検索実行
            FillDataset(connStr, CommandType.Text, "SELECT m_kameiten.kameiten_cd FROM m_kameiten WHERE 1=2", dsYosinJyouhouInput, _
                        "dsYosinJyouhouInput", paramList.ToArray)

        Else
            ' 検索実行
            FillDataset(connStr, CommandType.Text, Left(commandTextSb.ToString(), Len(commandTextSb.ToString()) - 11), dsYosinJyouhouInput, _
                        "dsYosinJyouhouInput", paramList.ToArray)

        End If

        'ヘッダーデータテーブル戻り
        Return dsYosinJyouhouInput

    End Function
    ''' <summary>
    ''' 与信管理情報を取得する
    ''' </summary>
    ''' <param name="nayose_cd">名寄先コード</param>
    ''' <returns>与信管理情報データテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetYosinKanriInfo(ByVal nayose_cd As String) As YosinJyouhouInputDataSet.YosinKanriInfoTableDataTable

        ' DataSetインスタンスの生成
        Dim dsYosinJyouhouInput As New YosinJyouhouInputDataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder()

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        commandTextSb.AppendLine("SELECT ")
        commandTextSb.AppendLine("  MS.nayose_saki_name1 ") '名寄先名１
        commandTextSb.AppendLine("  ,MS.nayose_saki_name2 ") '名寄先名２
        commandTextSb.AppendLine("  ,MS.nayose_saki_kana1 ") '名寄先カナ１
        commandTextSb.AppendLine("  ,MS.nayose_saki_kana2 ") '名寄先カナ２
        commandTextSb.AppendLine("  ,isnull(MS.yosin_gendo_gaku,0) AS yosin_gendo_gaku ") '名寄先与信限度額
        commandTextSb.AppendLine("  ,MS.yosin_keikou_kaisiritsu ") '与信警告開始率
        commandTextSb.AppendLine("  ,MS.teikoku_hyouten ") '帝国評点
        commandTextSb.AppendLine("  ,MS.todouhuken_cd ") '都道府県コード
        commandTextSb.AppendLine("  ,MT.todouhuken_mei") '都道府県名
        commandTextSb.AppendLine("  ,MS.tyoku_koji_flg ") '直工事FLG
        commandTextSb.AppendLine("  ,MS.jyutyuu_kanri_flg ") '受注管理FLG
        commandTextSb.AppendLine("  ,MM.meisyou ") '警告状況名
        commandTextSb.AppendLine("  ,MS.zenjitsu_koji_flg ") '前日工事状況FLG
        commandTextSb.AppendLine("  ,isnull(MS.zengetsu_saiken_gaku,0) AS zengetsu_saiken_gaku ") '前月債権額
        commandTextSb.AppendLine("  ,MS.zengetsu_saiken_set_date ") '前月債権額設定年月
        commandTextSb.AppendLine("  ,isnull(MS.ruiseki_nyuukin_gaku,0) AS ruiseki_nyuukin_gaku ") '累積入金額
        commandTextSb.AppendLine("  ,MS.ruiseki_nyuukin_set_date ") '累積入金額設定日時
        commandTextSb.AppendLine("  ,isnull(MS.ruiseki_jyutyuu_gaku,0) AS ruiseki_jyutyuu_gaku ") '累積受注額
        commandTextSb.AppendLine("  ,getdate() AS ruiseki_jyutyuu_set_date ") '累積受注額設定年月日
        commandTextSb.AppendLine("  ,isnull(MS.ruiseki_kasiuri_gaku,0) AS ruiseki_kasiuri_gaku ") '累積瑕疵売上額
        commandTextSb.AppendLine("  ,MS.ruiseki_kasiuri_set_date ") '累積瑕疵売上額設定日
        commandTextSb.AppendLine("  ,isnull(MS.toujitsu_jyutyuu_gaku,0) AS toujitsu_jyutyuu_gaku ") '当日受注額
        commandTextSb.AppendLine("  ,isnull(MS.zengetsu_saiken_gaku,0) - isnull(MS.ruiseki_nyuukin_gaku,0) ")
        commandTextSb.AppendLine("  + isnull(MS.ruiseki_jyutyuu_gaku,0) + isnull(MS.ruiseki_kasiuri_gaku,0) ")
        commandTextSb.AppendLine("  + isnull(MS.toujitsu_jyutyuu_gaku,0) as saikaigaku ") '当月売掛金合計額
        commandTextSb.AppendLine("FROM ")
        commandTextSb.AppendLine("  m_yosinkanri MS WITH (READCOMMITTED) ")
        commandTextSb.AppendLine("LEFT JOIN m_todoufuken MT WITH (READCOMMITTED) ")
        commandTextSb.AppendLine("ON MS.todouhuken_cd = MT.todouhuken_cd ")
        commandTextSb.AppendLine("LEFT JOIN m_meisyou MM WITH (READCOMMITTED) ")
        commandTextSb.AppendLine("ON MS.keikoku_joukyou = MM.code ")
        commandTextSb.AppendLine("AND MM.meisyou_syubetu = @meisyou_syubetu ")
        commandTextSb.AppendLine("WHERE MS.nayose_saki_cd = @nayose_saki_cd ")

        'パラメータの設定
        paramList.Add(MakeParam("@nayose_saki_cd", SqlDbType.VarChar, 5, nayose_cd))
        paramList.Add(MakeParam("@meisyou_syubetu", SqlDbType.VarChar, 2, "52"))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsYosinJyouhouInput, _
                    dsYosinJyouhouInput.YosinKanriInfoTable.TableName, paramList.ToArray)

        'ヘッダーデータテーブル戻り
        Return dsYosinJyouhouInput.YosinKanriInfoTable

    End Function
    ''' <summary>
    ''' 入金予定情報を取得する
    ''' </summary>
    ''' <param name="nayose_cd">名寄先コード</param>
    ''' <returns>入金予定情報データテーブル</returns>
    ''' <remarks></remarks>
    Public Function GetNyuukinYoteiInfo(ByVal nayose_cd As String, _
                                        ByVal ruiseki_nyuukin_set_date As DateTime) As YosinJyouhouInputDataSet.NyuukinYoteiInfoTableDataTable

        ' DataSetインスタンスの生成
        Dim dsYosinJyouhouInput As New YosinJyouhouInputDataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder()

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        commandTextSb.AppendLine("SELECT ")
        commandTextSb.AppendLine("  MM.meisyou ") '入金予定種別
        commandTextSb.AppendLine("  ,isnull(MNNY.nyuukinyotei_gaku,0) AS nyuukinyotei_gaku ") '入金予定額
        commandTextSb.AppendLine("  ,MNNY.nyuukinyotei_date ") '入金予定日
        commandTextSb.AppendLine("  ,MNNY.bikou ") '備考
        commandTextSb.AppendLine("FROM ")
        commandTextSb.AppendLine("  m_nayose_nyuukin_yoteikanri MNNY WITH (READCOMMITTED) ")
        commandTextSb.AppendLine("LEFT JOIN m_meisyou MM WITH (READCOMMITTED) ")
        commandTextSb.AppendLine("ON MNNY.nyuukin_yotei_syubetsu = MM.code ")
        commandTextSb.AppendLine("AND MM.meisyou_syubetu = @meisyou_syubetu ")
        commandTextSb.AppendLine("WHERE MNNY.nayose_saki_cd = @nayose_saki_cd ")
        commandTextSb.AppendLine("AND MNNY.torikesi <> 1")
        commandTextSb.AppendLine("AND MNNY.nyuukinyotei_date > @ruiseki_nyuukin_set_date")
        commandTextSb.AppendLine("ORDER BY ")
        commandTextSb.AppendLine("     MNNY.nyuukinyotei_date")
        commandTextSb.AppendLine("     ,MM.hyouji_jyun")
        commandTextSb.AppendLine("     ,MNNY.nyuuryoku_no")

        'パラメータの設定
        paramList.Add(MakeParam("@nayose_saki_cd", SqlDbType.VarChar, 5, nayose_cd))
        paramList.Add(MakeParam("@ruiseki_nyuukin_set_date", SqlDbType.DateTime, 10, ruiseki_nyuukin_set_date))
        paramList.Add(MakeParam("@meisyou_syubetu", SqlDbType.VarChar, 2, "54"))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsYosinJyouhouInput, _
                    dsYosinJyouhouInput.NyuukinYoteiInfoTable.TableName, paramList.ToArray)

        'ヘッダーデータテーブル戻り
        Return dsYosinJyouhouInput.NyuukinYoteiInfoTable

    End Function
    Public Function GetNayoseInfo(ByVal kameiten_cd As String) As DataTable

        ' DataSetインスタンスの生成
        Dim dtNayoseInfo As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder()

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        commandTextSb.AppendLine("	SELECT 	")
        commandTextSb.AppendLine("	m_kameiten.kameiten_cd,m_kameiten.kameiten_mei1 AS kameiten_mei,	")
        commandTextSb.AppendLine("	m_yosinkanri1.nayose_saki_cd AS nayose_saki_cd1,m_yosinkanri1.nayose_saki_name1 AS nayose_saki_name1,	")
        commandTextSb.AppendLine("	m_yosinkanri2.nayose_saki_cd AS nayose_saki_cd2,m_yosinkanri2.nayose_saki_name1 AS nayose_saki_name2,	")
        commandTextSb.AppendLine("	m_yosinkanri3.nayose_saki_cd AS nayose_saki_cd3,m_yosinkanri3.nayose_saki_name1 AS nayose_saki_name3	")
        commandTextSb.AppendLine("	FROM m_kameiten WITH (READCOMMITTED)	")
        commandTextSb.AppendLine("	LEFT JOIN m_nayose AS m_nayose1 WITH (READCOMMITTED)	")
        commandTextSb.AppendLine("	ON m_kameiten.tys_seikyuu_saki=m_nayose1.seikyuu_saki_cd	")
        commandTextSb.AppendLine("	LEFT JOIN m_yosinkanri AS m_yosinkanri1 WITH (READCOMMITTED)	")
        commandTextSb.AppendLine("	ON m_nayose1.nayose_saki_cd=m_yosinkanri1.nayose_saki_cd	")
        commandTextSb.AppendLine("	LEFT JOIN m_nayose AS m_nayose2 WITH (READCOMMITTED)	")
        commandTextSb.AppendLine("	ON m_kameiten.koj_seikyuusaki=m_nayose2.seikyuu_saki_cd	")

        commandTextSb.AppendLine("	LEFT JOIN m_yosinkanri AS m_yosinkanri2 WITH (READCOMMITTED)	")
        commandTextSb.AppendLine("	ON m_nayose2.nayose_saki_cd=m_yosinkanri2.nayose_saki_cd	")
        commandTextSb.AppendLine("	LEFT JOIN m_nayose AS m_nayose3	 WITH (READCOMMITTED)")
        commandTextSb.AppendLine("	ON m_kameiten.hansokuhin_seikyuusaki=m_nayose3.seikyuu_saki_cd	")

        commandTextSb.AppendLine("	LEFT JOIN m_yosinkanri AS m_yosinkanri3 WITH (READCOMMITTED)	")
        commandTextSb.AppendLine("	ON m_nayose3.nayose_saki_cd=m_yosinkanri3.nayose_saki_cd	")
        commandTextSb.AppendLine("	WHERE (m_yosinkanri1.nayose_saki_cd IS NOT NULL		")
        commandTextSb.AppendLine("	OR  m_yosinkanri2.nayose_saki_cd IS NOT NULL		")
        commandTextSb.AppendLine("	OR m_yosinkanri3.nayose_saki_cd IS NOT NULL	)	")
        commandTextSb.AppendLine("	AND m_kameiten.kameiten_cd=@kameiten_cd")

        'パラメータの設定
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, kameiten_cd))


        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dtNayoseInfo, _
                    "dtNayoseInfo", paramList.ToArray)

        'ヘッダーデータテーブル戻り
        Return dtNayoseInfo.Tables("dtNayoseInfo")

    End Function
#End Region
End Class

Imports System.Data.SqlClient
Imports System.Text
Imports Itis.ApplicationBlocks.Data.SQLHelper
Public Class SyouhinMasterDataAccess
    Private connStr As String = ConnectionManager.Instance.ConnectionString

    Public Function SelZeiKBNInfo() As DataTable

        ' DataSetインスタンスの生成()
        Dim dsDataSet As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'SQL文
        commandTextSb.AppendLine(" SELECT   ")
        commandTextSb.AppendLine(" zei_kbn   ")
        commandTextSb.AppendLine(" ,zeiritu   ")
        commandTextSb.AppendLine(" FROM  ")
        commandTextSb.AppendLine(" m_syouhizei   WITH (READCOMMITTED)  ")

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    "dsDataSet")

        Return dsDataSet.Tables(0)


    End Function
    Public Function SelKakutyouInfo(ByVal strSyubetu As String, Optional ByVal strTable As String = "") As DataTable

        ' DataSetインスタンスの生成()
        Dim dsDataSet As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        'SQL文
        If strTable = "m_tyousahouhou" Then
            commandTextSb.AppendLine(" SELECT   ")
            commandTextSb.AppendLine(" tys_houhou_no AS code   ")
            commandTextSb.AppendLine(" ,tys_houhou_mei AS meisyou   ")
            commandTextSb.AppendLine(" FROM  ")
            commandTextSb.AppendLine(" m_tyousahouhou   WITH (READCOMMITTED)  ")

            commandTextSb.AppendLine(" ORDER BY tys_houhou_no ")
            'パラメータの設定
            paramList.Add(MakeParam("@meisyou_syubetu", SqlDbType.VarChar, 3, strSyubetu))
        Else
            commandTextSb.AppendLine(" SELECT   ")
            commandTextSb.AppendLine(" code   ")
            commandTextSb.AppendLine(" ,meisyou   ")
            commandTextSb.AppendLine(" FROM  ")
            commandTextSb.AppendLine(" m_kakutyou_meisyou   WITH (READCOMMITTED)  ")
            commandTextSb.AppendLine(" WHERE  meisyou_syubetu=@meisyou_syubetu  ")
            commandTextSb.AppendLine(" ORDER BY hyouji_jyun ")
            'パラメータの設定
            paramList.Add(MakeParam("@meisyou_syubetu", SqlDbType.VarChar, 3, strSyubetu))
        End If

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    "dsDataSet", paramList.ToArray)

        Return dsDataSet.Tables(0)


    End Function
    Public Function SelSyouhinInfo(ByVal strSyouhinCd As String) As SyouhinDataSet.m_syouhinDataTable

        ' DataSetインスタンスの生成()
        Dim dsDataSet As New SyouhinDataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        commandTextSb.AppendLine(" SELECT   ")
        commandTextSb.AppendLine(" syouhin_cd ")
        commandTextSb.AppendLine(" ,syouhin_mei ")
        commandTextSb.AppendLine(" ,torikesi ")
        commandTextSb.AppendLine(" ,tani ")
        commandTextSb.AppendLine(" ,seikyuusaki_kbn ")
        commandTextSb.AppendLine(" ,shri_you_syouhin_mei ")
        commandTextSb.AppendLine(" ,syouhin_kbn1 ")
        commandTextSb.AppendLine(" ,syouhin_kbn2 ")
        commandTextSb.AppendLine(" ,syouhin_kbn3 ")
        commandTextSb.AppendLine(" ,hosyou_umu ")
        commandTextSb.AppendLine(" ,zei_kbn ")
        commandTextSb.AppendLine(" ,zeikomi_kbn ")
        commandTextSb.AppendLine(" ,hyoujun_kkk ")
        commandTextSb.AppendLine(" ,syanai_genka ")
        commandTextSb.AppendLine(" ,souko_cd ")
        commandTextSb.AppendLine(" ,siire_kkk ")
        commandTextSb.AppendLine(" ,koj_type ")
        '2011/03/01 商品マスタの項目を追加する 付龍(大連) ↓
        commandTextSb.AppendLine(" ,tys_umu_kbn")
        commandTextSb.AppendLine(" ,syouhin_syubetu1")
        commandTextSb.AppendLine(" ,syouhin_syubetu2")
        commandTextSb.AppendLine(" ,syouhin_syubetu3")
        commandTextSb.AppendLine(" ,syouhin_syubetu4")
        commandTextSb.AppendLine(" ,syouhin_syubetu5")
        '2011/03/01 商品マスタの項目を追加する 付龍(大連) ↑

        '2017/12/11 商品マスタの項目を追加する 李(大連) ↓
        commandTextSb.AppendLine(" ,syouhin_syubetu6")
        commandTextSb.AppendLine(" ,syouhin_syubetu7")
        commandTextSb.AppendLine(" ,syouhin_syubetu8")
        commandTextSb.AppendLine(" ,syouhin_syubetu9")
        commandTextSb.AppendLine(" ,syouhin_syubetu10")
        commandTextSb.AppendLine(" ,syouhin_syubetu11")
        commandTextSb.AppendLine(" ,syouhin_syubetu12")
        commandTextSb.AppendLine(" ,syouhin_syubetu13")
        commandTextSb.AppendLine(" ,syouhin_syubetu14")
        commandTextSb.AppendLine(" ,syouhin_syubetu15")
        commandTextSb.AppendLine(" ,syouhin_syubetu16")
        commandTextSb.AppendLine(" ,syouhin_syubetu17")
        '2017/12/11 商品マスタの項目を追加する 李(大連) ↑

        '2013/11/06 李宇追加 ↓
        commandTextSb.AppendLine(" ,sds_jidou_set")
        '2013/11/06 李宇追加 ↑
        commandTextSb.AppendLine(" ,tys_syouhin_hyouji_kbn")
        commandTextSb.AppendLine(" ,upd_login_user_id ")
        commandTextSb.AppendLine(" ,ISNULL(CONVERT(varchar(19),upd_datetime,21),'') AS upd_datetime")
        commandTextSb.AppendLine(" FROM m_syouhin WITH (READCOMMITTED)  ")
        commandTextSb.AppendLine(" WHERE  syouhin_cd=@syouhin_cd  ")

        'パラメータの設定
        paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, strSyouhinCd))

        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                         dsDataSet.m_syouhin.TableName, paramList.ToArray)

        Return dsDataSet.m_syouhin

    End Function

    Public Function InsSyouhin(ByVal dtSyouhin As SyouhinDataSet.m_syouhinDataTable) As Integer

        ' DataSetインスタンスの生成()
        Dim dsDataSet As New SyouhinDataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        commandTextSb.AppendLine("INSERT INTO m_syouhin WITH (UPDLOCK) ")
        commandTextSb.AppendLine("(syouhin_cd")
        commandTextSb.AppendLine(",syouhin_mei")
        commandTextSb.AppendLine(",torikesi")
        commandTextSb.AppendLine(",tani")
        commandTextSb.AppendLine(",seikyuusaki_kbn")
        commandTextSb.AppendLine(",shri_you_syouhin_mei")
        commandTextSb.AppendLine(",syouhin_kbn1")
        commandTextSb.AppendLine(",syouhin_kbn2")
        commandTextSb.AppendLine(",syouhin_kbn3")
        commandTextSb.AppendLine(",hosyou_umu")
        commandTextSb.AppendLine(",zei_kbn")
        commandTextSb.AppendLine(",zeikomi_kbn")
        commandTextSb.AppendLine(",hyoujun_kkk")
        commandTextSb.AppendLine(",syanai_genka")
        commandTextSb.AppendLine(",souko_cd")
        commandTextSb.AppendLine(",siire_kkk")
        commandTextSb.AppendLine(",koj_type")
        '2011/03/01 商品マスタの項目を追加する 付龍(大連) ↓
        commandTextSb.AppendLine(",tys_umu_kbn")
        commandTextSb.AppendLine(",syouhin_syubetu1")
        commandTextSb.AppendLine(",syouhin_syubetu2")
        commandTextSb.AppendLine(",syouhin_syubetu3")
        commandTextSb.AppendLine(",syouhin_syubetu4")
        commandTextSb.AppendLine(",syouhin_syubetu5")
        '2011/03/01 商品マスタの項目を追加する 付龍(大連) ↑
        '2017/12/11 商品マスタの項目を追加する 李(大連) ↓
        commandTextSb.AppendLine(" ,syouhin_syubetu6")
        commandTextSb.AppendLine(" ,syouhin_syubetu7")
        commandTextSb.AppendLine(" ,syouhin_syubetu8")
        commandTextSb.AppendLine(" ,syouhin_syubetu9")
        commandTextSb.AppendLine(" ,syouhin_syubetu10")
        commandTextSb.AppendLine(" ,syouhin_syubetu11")
        commandTextSb.AppendLine(" ,syouhin_syubetu12")
        commandTextSb.AppendLine(" ,syouhin_syubetu13")
        commandTextSb.AppendLine(" ,syouhin_syubetu14")
        commandTextSb.AppendLine(" ,syouhin_syubetu15")
        commandTextSb.AppendLine(" ,syouhin_syubetu16")
        commandTextSb.AppendLine(" ,syouhin_syubetu17")
        '2017/12/11 商品マスタの項目を追加する 李(大連) ↑
        '2013/11/06 李宇追加 ↓
        commandTextSb.AppendLine(",sds_jidou_set")
        '2013/11/06 李宇追加 ↑
        commandTextSb.AppendLine(",tys_syouhin_hyouji_kbn")
        commandTextSb.AppendLine(",add_login_user_id")
        commandTextSb.AppendLine(",add_datetime)")
        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine(" @syouhin_cd")
        commandTextSb.AppendLine(",@syouhin_mei")
        commandTextSb.AppendLine(",@torikesi")
        commandTextSb.AppendLine(",@tani")
        commandTextSb.AppendLine(",@seikyuusaki_kbn")
        commandTextSb.AppendLine(",@shri_you_syouhin_mei")
        commandTextSb.AppendLine(",@syouhin_kbn1")
        commandTextSb.AppendLine(",@syouhin_kbn2")
        commandTextSb.AppendLine(",@syouhin_kbn3")
        commandTextSb.AppendLine(",@hosyou_umu")
        commandTextSb.AppendLine(",@zei_kbn")
        commandTextSb.AppendLine(",@zeikomi_kbn")
        commandTextSb.AppendLine(",@hyoujun_kkk")
        commandTextSb.AppendLine(",@syanai_genka")
        commandTextSb.AppendLine(",@souko_cd")
        commandTextSb.AppendLine(",@siire_kkk")
        commandTextSb.AppendLine(",@koj_type")
        '2011/03/01 商品マスタの項目を追加する 付龍(大連) ↓
        commandTextSb.AppendLine(",@tys_umu_kbn")
        commandTextSb.AppendLine(",@syouhin_syubetu1")
        commandTextSb.AppendLine(",@syouhin_syubetu2")
        commandTextSb.AppendLine(",@syouhin_syubetu3")
        commandTextSb.AppendLine(",@syouhin_syubetu4")
        commandTextSb.AppendLine(",@syouhin_syubetu5")
        '2011/03/01 商品マスタの項目を追加する 付龍(大連) ↑

        '2017/12/11 商品マスタの項目を追加する 李(大連) ↓
        commandTextSb.AppendLine(",@syouhin_syubetu6")
        commandTextSb.AppendLine(",@syouhin_syubetu7")
        commandTextSb.AppendLine(",@syouhin_syubetu8")
        commandTextSb.AppendLine(",@syouhin_syubetu9")
        commandTextSb.AppendLine(",@syouhin_syubetu10")
        commandTextSb.AppendLine(",@syouhin_syubetu11")
        commandTextSb.AppendLine(",@syouhin_syubetu12")
        commandTextSb.AppendLine(",@syouhin_syubetu13")
        commandTextSb.AppendLine(",@syouhin_syubetu14")
        commandTextSb.AppendLine(",@syouhin_syubetu15")
        commandTextSb.AppendLine(",@syouhin_syubetu16")
        commandTextSb.AppendLine(",@syouhin_syubetu17")
        '2017/12/11 商品マスタの項目を追加する 李(大連) ↑

        '2013/11/06 李宇追加 ↓
        commandTextSb.AppendLine(",@sds_jidou_set")
        commandTextSb.AppendLine(",@tys_syouhin_hyouji_kbn")
        '2013/11/06 李宇追加 ↑
        commandTextSb.AppendLine(",@add_login_user_id")
        commandTextSb.AppendLine(",GETDATE() ")

        'パラメータの設定()
        paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 255, dtSyouhin(0).syouhin_cd))

        paramList.Add(MakeParam("@syouhin_mei", SqlDbType.VarChar, 255, IIf(dtSyouhin(0).syouhin_mei = "", DBNull.Value, dtSyouhin(0).syouhin_mei)))
        paramList.Add(MakeParam("@torikesi", SqlDbType.VarChar, 255, dtSyouhin(0).torikesi))
        paramList.Add(MakeParam("@tani", SqlDbType.VarChar, 255, IIf(dtSyouhin(0).tani = "", DBNull.Value, dtSyouhin(0).tani)))
        paramList.Add(MakeParam("@seikyuusaki_kbn", SqlDbType.VarChar, 255, IIf(dtSyouhin(0).seikyuusaki_kbn = "", DBNull.Value, dtSyouhin(0).seikyuusaki_kbn)))
        paramList.Add(MakeParam("@shri_you_syouhin_mei", SqlDbType.VarChar, 255, IIf(dtSyouhin(0).shri_you_syouhin_mei = "", DBNull.Value, dtSyouhin(0).shri_you_syouhin_mei)))
        paramList.Add(MakeParam("@syouhin_kbn1", SqlDbType.VarChar, 255, IIf(dtSyouhin(0).syouhin_kbn1 = "", DBNull.Value, dtSyouhin(0).syouhin_kbn1)))
        paramList.Add(MakeParam("@syouhin_kbn2", SqlDbType.VarChar, 255, IIf(dtSyouhin(0).syouhin_kbn2 = "", DBNull.Value, dtSyouhin(0).syouhin_kbn2)))
        paramList.Add(MakeParam("@syouhin_kbn3", SqlDbType.VarChar, 255, IIf(dtSyouhin(0).syouhin_kbn3 = "", DBNull.Value, dtSyouhin(0).syouhin_kbn3)))
        paramList.Add(MakeParam("@hosyou_umu", SqlDbType.VarChar, 255, IIf(dtSyouhin(0).hosyou_umu = "", DBNull.Value, dtSyouhin(0).hosyou_umu)))
        paramList.Add(MakeParam("@zei_kbn", SqlDbType.VarChar, 255, IIf(dtSyouhin(0).zei_kbn = "", DBNull.Value, dtSyouhin(0).zei_kbn)))
        paramList.Add(MakeParam("@zeikomi_kbn", SqlDbType.VarChar, 255, IIf(dtSyouhin(0).zeikomi_kbn = "", DBNull.Value, dtSyouhin(0).zeikomi_kbn)))
        paramList.Add(MakeParam("@hyoujun_kkk", SqlDbType.VarChar, 255, IIf(dtSyouhin(0).hyoujun_kkk = "", DBNull.Value, dtSyouhin(0).hyoujun_kkk)))
        paramList.Add(MakeParam("@syanai_genka", SqlDbType.VarChar, 255, IIf(dtSyouhin(0).syanai_genka = "", DBNull.Value, dtSyouhin(0).syanai_genka)))
        paramList.Add(MakeParam("@souko_cd", SqlDbType.VarChar, 255, IIf(dtSyouhin(0).souko_cd = "", DBNull.Value, dtSyouhin(0).souko_cd)))
        paramList.Add(MakeParam("@siire_kkk", SqlDbType.VarChar, 255, IIf(dtSyouhin(0).souko_cd = "", DBNull.Value, dtSyouhin(0).siire_kkk)))
        paramList.Add(MakeParam("@koj_type", SqlDbType.VarChar, 255, IIf(dtSyouhin(0).koj_type = "", DBNull.Value, dtSyouhin(0).koj_type)))
        '2011/03/01 商品マスタの項目を追加する 付龍(大連) ↓
        paramList.Add(MakeParam("@tys_umu_kbn", SqlDbType.Int, 4, IIf(dtSyouhin(0).tys_umu_kbn = "", DBNull.Value, dtSyouhin(0).tys_umu_kbn)))
        paramList.Add(MakeParam("@syouhin_syubetu1", SqlDbType.VarChar, 10, IIf(dtSyouhin(0).syouhin_syubetu1 = "", DBNull.Value, dtSyouhin(0).syouhin_syubetu1)))
        paramList.Add(MakeParam("@syouhin_syubetu2", SqlDbType.VarChar, 10, IIf(dtSyouhin(0).syouhin_syubetu2 = "", DBNull.Value, dtSyouhin(0).syouhin_syubetu2)))
        paramList.Add(MakeParam("@syouhin_syubetu3", SqlDbType.VarChar, 10, IIf(dtSyouhin(0).syouhin_syubetu3 = "", DBNull.Value, dtSyouhin(0).syouhin_syubetu3)))
        paramList.Add(MakeParam("@syouhin_syubetu4", SqlDbType.VarChar, 10, IIf(dtSyouhin(0).syouhin_syubetu4 = "", DBNull.Value, dtSyouhin(0).syouhin_syubetu4)))
        paramList.Add(MakeParam("@syouhin_syubetu5", SqlDbType.VarChar, 10, IIf(dtSyouhin(0).syouhin_syubetu5 = "", DBNull.Value, dtSyouhin(0).syouhin_syubetu5)))
        '2011/03/01 商品マスタの項目を追加する 付龍(大連) ↑


        '2017/12/11 商品マスタの項目を追加する 李(大連) ↓
        paramList.Add(MakeParam("@syouhin_syubetu6", SqlDbType.VarChar, 10, IIf(dtSyouhin(0).syouhin_syubetu6 = "", DBNull.Value, dtSyouhin(0).syouhin_syubetu6)))
        paramList.Add(MakeParam("@syouhin_syubetu7", SqlDbType.VarChar, 10, IIf(dtSyouhin(0).syouhin_syubetu7 = "", DBNull.Value, dtSyouhin(0).syouhin_syubetu7)))
        paramList.Add(MakeParam("@syouhin_syubetu8", SqlDbType.VarChar, 10, IIf(dtSyouhin(0).syouhin_syubetu8 = "", DBNull.Value, dtSyouhin(0).syouhin_syubetu8)))
        paramList.Add(MakeParam("@syouhin_syubetu9", SqlDbType.VarChar, 10, IIf(dtSyouhin(0).syouhin_syubetu9 = "", DBNull.Value, dtSyouhin(0).syouhin_syubetu9)))
        paramList.Add(MakeParam("@syouhin_syubetu10", SqlDbType.VarChar, 10, IIf(dtSyouhin(0).syouhin_syubetu10 = "", DBNull.Value, dtSyouhin(0).syouhin_syubetu10)))
        paramList.Add(MakeParam("@syouhin_syubetu11", SqlDbType.VarChar, 10, IIf(dtSyouhin(0).syouhin_syubetu11 = "", DBNull.Value, dtSyouhin(0).syouhin_syubetu11)))
        paramList.Add(MakeParam("@syouhin_syubetu12", SqlDbType.VarChar, 10, IIf(dtSyouhin(0).syouhin_syubetu12 = "", DBNull.Value, dtSyouhin(0).syouhin_syubetu12)))
        paramList.Add(MakeParam("@syouhin_syubetu13", SqlDbType.VarChar, 10, IIf(dtSyouhin(0).syouhin_syubetu13 = "", DBNull.Value, dtSyouhin(0).syouhin_syubetu13)))
        paramList.Add(MakeParam("@syouhin_syubetu14", SqlDbType.VarChar, 10, IIf(dtSyouhin(0).syouhin_syubetu14 = "", DBNull.Value, dtSyouhin(0).syouhin_syubetu14)))
        paramList.Add(MakeParam("@syouhin_syubetu15", SqlDbType.VarChar, 10, IIf(dtSyouhin(0).syouhin_syubetu15 = "", DBNull.Value, dtSyouhin(0).syouhin_syubetu15)))
        paramList.Add(MakeParam("@syouhin_syubetu16", SqlDbType.VarChar, 10, IIf(dtSyouhin(0).syouhin_syubetu16 = "", DBNull.Value, dtSyouhin(0).syouhin_syubetu16)))
        paramList.Add(MakeParam("@syouhin_syubetu17", SqlDbType.VarChar, 10, IIf(dtSyouhin(0).syouhin_syubetu17 = "", DBNull.Value, dtSyouhin(0).syouhin_syubetu17)))
        '2017/12/11 商品マスタの項目を追加する 李(大連) ↑


        '2013/11/06 李宇追加 ↓
        paramList.Add(MakeParam("@sds_jidou_set", SqlDbType.Int, 10, IIf(dtSyouhin(0).sds_jidou_set = "", DBNull.Value, dtSyouhin(0).sds_jidou_set)))
        '2013/11/06 李宇追加 ↑
        paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 255, IIf(dtSyouhin(0).upd_login_user_id = "", DBNull.Value, dtSyouhin(0).upd_login_user_id)))

        paramList.Add(MakeParam("@tys_syouhin_hyouji_kbn", SqlDbType.VarChar, 1, IIf(dtSyouhin(0).tys_syouhin_hyouji_kbn = "", DBNull.Value, dtSyouhin(0).tys_syouhin_hyouji_kbn)))


        ' クエリ実行
        Return ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    commandTextSb.ToString(), _
                                    paramList.ToArray)


    End Function

    Public Function UpdSyouhin(ByVal dtSyouhin As SyouhinDataSet.m_syouhinDataTable) As Integer

        ' DataSetインスタンスの生成()
        Dim dsDataSet As New SyouhinDataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        commandTextSb.AppendLine("UPDATE m_syouhin WITH (UPDLOCK) ")
        commandTextSb.AppendLine(" SET syouhin_mei=@syouhin_mei")
        commandTextSb.AppendLine(",torikesi=@torikesi")
        commandTextSb.AppendLine(",tani=@tani")
        commandTextSb.AppendLine(",seikyuusaki_kbn=@seikyuusaki_kbn")
        commandTextSb.AppendLine(",shri_you_syouhin_mei=@shri_you_syouhin_mei")
        commandTextSb.AppendLine(",syouhin_kbn1=@syouhin_kbn1")
        commandTextSb.AppendLine(",syouhin_kbn2=@syouhin_kbn2")
        commandTextSb.AppendLine(",syouhin_kbn3=@syouhin_kbn3")
        commandTextSb.AppendLine(",hosyou_umu=@hosyou_umu")
        commandTextSb.AppendLine(",zei_kbn=@zei_kbn")
        commandTextSb.AppendLine(",zeikomi_kbn=@zeikomi_kbn")
        commandTextSb.AppendLine(",hyoujun_kkk=@hyoujun_kkk")
        commandTextSb.AppendLine(",syanai_genka=@syanai_genka")
        commandTextSb.AppendLine(",souko_cd=@souko_cd")
        commandTextSb.AppendLine(",siire_kkk=@siire_kkk")
        commandTextSb.AppendLine(",koj_type=@koj_type")
        '2011/03/01 商品マスタの項目を追加する 付龍(大連) ↓
        commandTextSb.AppendLine(",tys_umu_kbn=@tys_umu_kbn")
        commandTextSb.AppendLine(",syouhin_syubetu1=@syouhin_syubetu1")
        commandTextSb.AppendLine(",syouhin_syubetu2=@syouhin_syubetu2")
        commandTextSb.AppendLine(",syouhin_syubetu3=@syouhin_syubetu3")
        commandTextSb.AppendLine(",syouhin_syubetu4=@syouhin_syubetu4")
        commandTextSb.AppendLine(",syouhin_syubetu5=@syouhin_syubetu5")
        '2011/03/01 商品マスタの項目を追加する 付龍(大連) ↑

        '2017/12/11 商品マスタの項目を追加する 李(大連) ↓
        commandTextSb.AppendLine(",syouhin_syubetu6	=@syouhin_syubetu6")
        commandTextSb.AppendLine(",syouhin_syubetu7	=@syouhin_syubetu7")
        commandTextSb.AppendLine(",syouhin_syubetu8	=@syouhin_syubetu8")
        commandTextSb.AppendLine(",syouhin_syubetu9	=@syouhin_syubetu9")
        commandTextSb.AppendLine(",syouhin_syubetu10	=@syouhin_syubetu10")
        commandTextSb.AppendLine(",syouhin_syubetu11	=@syouhin_syubetu11")
        commandTextSb.AppendLine(",syouhin_syubetu12	=@syouhin_syubetu12")
        commandTextSb.AppendLine(",syouhin_syubetu13	=@syouhin_syubetu13")
        commandTextSb.AppendLine(",syouhin_syubetu14	=@syouhin_syubetu14")
        commandTextSb.AppendLine(",syouhin_syubetu15	=@syouhin_syubetu15")
        commandTextSb.AppendLine(",syouhin_syubetu16	=@syouhin_syubetu16")
        commandTextSb.AppendLine(",syouhin_syubetu17	=@syouhin_syubetu17")
        '2017/12/11 商品マスタの項目を追加する 李(大連) ↑

        '2013/11/06 李宇追加 ↓
        commandTextSb.AppendLine(",sds_jidou_set=@sds_jidou_set")
        '2013/11/06 李宇追加 ↑
        commandTextSb.AppendLine(",upd_login_user_id=@add_login_user_id")
        commandTextSb.AppendLine(",upd_datetime=GETDATE() ")

        commandTextSb.AppendLine(",tys_syouhin_hyouji_kbn=@tys_syouhin_hyouji_kbn ")

        commandTextSb.AppendLine(" WHERE syouhin_cd=@syouhin_cd")
        'パラメータの設定()
        paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 255, dtSyouhin(0).syouhin_cd))

        paramList.Add(MakeParam("@syouhin_mei", SqlDbType.VarChar, 255, IIf(dtSyouhin(0).syouhin_mei = "", DBNull.Value, dtSyouhin(0).syouhin_mei)))
        paramList.Add(MakeParam("@torikesi", SqlDbType.VarChar, 255, dtSyouhin(0).torikesi))
        paramList.Add(MakeParam("@tani", SqlDbType.VarChar, 255, IIf(dtSyouhin(0).tani = "", DBNull.Value, dtSyouhin(0).tani)))
        paramList.Add(MakeParam("@seikyuusaki_kbn", SqlDbType.VarChar, 255, IIf(dtSyouhin(0).seikyuusaki_kbn = "", DBNull.Value, dtSyouhin(0).seikyuusaki_kbn)))
        paramList.Add(MakeParam("@shri_you_syouhin_mei", SqlDbType.VarChar, 255, IIf(dtSyouhin(0).shri_you_syouhin_mei = "", DBNull.Value, dtSyouhin(0).shri_you_syouhin_mei)))
        paramList.Add(MakeParam("@syouhin_kbn1", SqlDbType.VarChar, 255, IIf(dtSyouhin(0).syouhin_kbn1 = "", DBNull.Value, dtSyouhin(0).syouhin_kbn1)))
        paramList.Add(MakeParam("@syouhin_kbn2", SqlDbType.VarChar, 255, IIf(dtSyouhin(0).syouhin_kbn2 = "", DBNull.Value, dtSyouhin(0).syouhin_kbn2)))
        paramList.Add(MakeParam("@syouhin_kbn3", SqlDbType.VarChar, 255, IIf(dtSyouhin(0).syouhin_kbn3 = "", DBNull.Value, dtSyouhin(0).syouhin_kbn3)))
        paramList.Add(MakeParam("@hosyou_umu", SqlDbType.VarChar, 255, IIf(dtSyouhin(0).hosyou_umu = "", DBNull.Value, dtSyouhin(0).hosyou_umu)))
        paramList.Add(MakeParam("@zei_kbn", SqlDbType.VarChar, 255, IIf(dtSyouhin(0).zei_kbn = "", DBNull.Value, dtSyouhin(0).zei_kbn)))
        paramList.Add(MakeParam("@zeikomi_kbn", SqlDbType.VarChar, 255, IIf(dtSyouhin(0).zeikomi_kbn = "", DBNull.Value, dtSyouhin(0).zeikomi_kbn)))
        paramList.Add(MakeParam("@hyoujun_kkk", SqlDbType.VarChar, 255, IIf(dtSyouhin(0).hyoujun_kkk = "", DBNull.Value, dtSyouhin(0).hyoujun_kkk)))
        paramList.Add(MakeParam("@syanai_genka", SqlDbType.VarChar, 255, IIf(dtSyouhin(0).syanai_genka = "", DBNull.Value, dtSyouhin(0).syanai_genka)))
        paramList.Add(MakeParam("@souko_cd", SqlDbType.VarChar, 255, IIf(dtSyouhin(0).souko_cd = "", DBNull.Value, dtSyouhin(0).souko_cd)))
        paramList.Add(MakeParam("@siire_kkk", SqlDbType.VarChar, 255, IIf(dtSyouhin(0).siire_kkk = "", DBNull.Value, dtSyouhin(0).siire_kkk)))
        paramList.Add(MakeParam("@koj_type", SqlDbType.VarChar, 255, IIf(dtSyouhin(0).koj_type = "", DBNull.Value, dtSyouhin(0).koj_type)))
        '2011/03/01 商品マスタの項目を追加する 付龍(大連) ↓
        paramList.Add(MakeParam("@tys_umu_kbn", SqlDbType.Int, 4, IIf(dtSyouhin(0).tys_umu_kbn = "", DBNull.Value, dtSyouhin(0).tys_umu_kbn)))
        paramList.Add(MakeParam("@syouhin_syubetu1", SqlDbType.VarChar, 10, IIf(dtSyouhin(0).syouhin_syubetu1 = "", DBNull.Value, dtSyouhin(0).syouhin_syubetu1)))
        paramList.Add(MakeParam("@syouhin_syubetu2", SqlDbType.VarChar, 10, IIf(dtSyouhin(0).syouhin_syubetu2 = "", DBNull.Value, dtSyouhin(0).syouhin_syubetu2)))
        paramList.Add(MakeParam("@syouhin_syubetu3", SqlDbType.VarChar, 10, IIf(dtSyouhin(0).syouhin_syubetu3 = "", DBNull.Value, dtSyouhin(0).syouhin_syubetu3)))
        paramList.Add(MakeParam("@syouhin_syubetu4", SqlDbType.VarChar, 10, IIf(dtSyouhin(0).syouhin_syubetu4 = "", DBNull.Value, dtSyouhin(0).syouhin_syubetu4)))
        paramList.Add(MakeParam("@syouhin_syubetu5", SqlDbType.VarChar, 10, IIf(dtSyouhin(0).syouhin_syubetu5 = "", DBNull.Value, dtSyouhin(0).syouhin_syubetu5)))
        '2011/03/01 商品マスタの項目を追加する 付龍(大連) ↑

        '2017/12/11 商品マスタの項目を追加する 李(大連) ↓
        paramList.Add(MakeParam("@syouhin_syubetu6", SqlDbType.VarChar, 10, IIf(dtSyouhin(0).syouhin_syubetu6 = "", DBNull.Value, dtSyouhin(0).syouhin_syubetu6)))
        paramList.Add(MakeParam("@syouhin_syubetu7", SqlDbType.VarChar, 10, IIf(dtSyouhin(0).syouhin_syubetu7 = "", DBNull.Value, dtSyouhin(0).syouhin_syubetu7)))
        paramList.Add(MakeParam("@syouhin_syubetu8", SqlDbType.VarChar, 10, IIf(dtSyouhin(0).syouhin_syubetu8 = "", DBNull.Value, dtSyouhin(0).syouhin_syubetu8)))
        paramList.Add(MakeParam("@syouhin_syubetu9", SqlDbType.VarChar, 10, IIf(dtSyouhin(0).syouhin_syubetu9 = "", DBNull.Value, dtSyouhin(0).syouhin_syubetu9)))
        paramList.Add(MakeParam("@syouhin_syubetu10", SqlDbType.VarChar, 10, IIf(dtSyouhin(0).syouhin_syubetu10 = "", DBNull.Value, dtSyouhin(0).syouhin_syubetu10)))
        paramList.Add(MakeParam("@syouhin_syubetu11", SqlDbType.VarChar, 10, IIf(dtSyouhin(0).syouhin_syubetu11 = "", DBNull.Value, dtSyouhin(0).syouhin_syubetu11)))
        paramList.Add(MakeParam("@syouhin_syubetu12", SqlDbType.VarChar, 10, IIf(dtSyouhin(0).syouhin_syubetu12 = "", DBNull.Value, dtSyouhin(0).syouhin_syubetu12)))
        paramList.Add(MakeParam("@syouhin_syubetu13", SqlDbType.VarChar, 10, IIf(dtSyouhin(0).syouhin_syubetu13 = "", DBNull.Value, dtSyouhin(0).syouhin_syubetu13)))
        paramList.Add(MakeParam("@syouhin_syubetu14", SqlDbType.VarChar, 10, IIf(dtSyouhin(0).syouhin_syubetu14 = "", DBNull.Value, dtSyouhin(0).syouhin_syubetu14)))
        paramList.Add(MakeParam("@syouhin_syubetu15", SqlDbType.VarChar, 10, IIf(dtSyouhin(0).syouhin_syubetu15 = "", DBNull.Value, dtSyouhin(0).syouhin_syubetu15)))
        paramList.Add(MakeParam("@syouhin_syubetu16", SqlDbType.VarChar, 10, IIf(dtSyouhin(0).syouhin_syubetu16 = "", DBNull.Value, dtSyouhin(0).syouhin_syubetu16)))
        paramList.Add(MakeParam("@syouhin_syubetu17", SqlDbType.VarChar, 10, IIf(dtSyouhin(0).syouhin_syubetu17 = "", DBNull.Value, dtSyouhin(0).syouhin_syubetu17)))
        '2017/12/11 商品マスタの項目を追加する 李(大連) ↑

        '2013/11/06 李宇追加 ↓
        paramList.Add(MakeParam("@sds_jidou_set", SqlDbType.Int, 10, IIf(dtSyouhin(0).sds_jidou_set = "", DBNull.Value, dtSyouhin(0).sds_jidou_set)))
        '2013/11/06 李宇追加 ↑
        paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 255, IIf(dtSyouhin(0).upd_login_user_id = "", DBNull.Value, dtSyouhin(0).upd_login_user_id)))
        paramList.Add(MakeParam("@tys_syouhin_hyouji_kbn", SqlDbType.VarChar, 1, IIf(dtSyouhin(0).tys_syouhin_hyouji_kbn = "", DBNull.Value, dtSyouhin(0).tys_syouhin_hyouji_kbn)))

        ' クエリ実行
        Return ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    commandTextSb.ToString(), _
                                    paramList.ToArray)


    End Function

    Public Function SelHaita(ByVal strSyouhinCd As String, ByVal strKousin As String) As DataTable

        ' DataSetインスタンスの生成()
        Dim dsDataSet As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        'SQL文
        commandTextSb.AppendLine(" SELECT   ")
        commandTextSb.AppendLine(" upd_login_user_id  ")
        commandTextSb.AppendLine(" ,upd_datetime  ")
        commandTextSb.AppendLine(" FROM  ")
        commandTextSb.AppendLine(" m_syouhin  WITH (READCOMMITTED)  ")
        commandTextSb.AppendLine(" WHERE    ")
        commandTextSb.AppendLine(" syouhin_cd=@strSyouhinCd  ")
        commandTextSb.AppendLine(" AND CONVERT(varchar(19),upd_datetime,21)<>@upd_datetime  ")
        commandTextSb.AppendLine(" AND upd_datetime IS NOT NULL  ")
        'パラメータの設定

        paramList.Add(MakeParam("@strSyouhinCd", SqlDbType.VarChar, 8, strSyouhinCd))
        paramList.Add(MakeParam("@upd_datetime", SqlDbType.VarChar, 19, strKousin))


        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    "dsDataSet", paramList.ToArray)

        Return dsDataSet.Tables(0)
    End Function
End Class

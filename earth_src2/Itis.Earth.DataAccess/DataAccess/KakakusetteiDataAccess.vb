Imports System.Data.SqlClient
Imports System.Text
Imports Itis.ApplicationBlocks.Data.SQLHelper
Public Class KakakusetteiDataAccess
    Private connStr As String = ConnectionManager.Instance.ConnectionString

    '============================2011/04/26 車龍 修正 開始↓=====================================
    'Public Function SelKakakuInfo(ByVal strKbn As String, ByVal strHouhou As String, ByVal strGaiyou As String) As DataTable
    Public Function SelKakakuInfo(ByVal strKbn As String, ByVal strHouhou As String, ByVal strGaiyou As String, Optional ByVal strSyouhinCd As String = "") As DataTable
        '========================2011/04/26 車龍 修正 終了↑=====================================

        ' DataSetインスタンスの生成()
        Dim dsDataSet As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        commandTextSb.AppendLine(" SELECT ")

        commandTextSb.AppendLine(" CONVERT(VARCHAR(10),m_syouhin_kakakusettei.syouhin_kbn)+':'+ISNULL(m_meisyou_01.meisyou,'')  ")
        commandTextSb.AppendLine(" ,CONVERT(VARCHAR(10),m_syouhin_kakakusettei.tys_houhou_no)+':'+ISNULL(m_tyousahouhou.tys_houhou_mei_ryaku,'')  ")
        commandTextSb.AppendLine(" ,CONVERT(VARCHAR(10),m_syouhin_kakakusettei.tys_gaiyou)+':'+ISNULL(m_meisyou_02.meisyou,'')  ")
        commandTextSb.AppendLine(" ,ISNULL(m_syouhin_kakakusettei.syouhin_cd,'')+':'+ISNULL(m_syouhin.syouhin_mei,'')  ")
        commandTextSb.AppendLine(" ,ISNULL(CONVERT(VARCHAR(10),m_syouhin_kakakusettei.kkk_settei_basyo),'')  ")
        commandTextSb.AppendLine(" ,ISNULL(CONVERT(VARCHAR(19),m_syouhin_kakakusettei.upd_datetime,21),'') AS upd_datetime ")
        commandTextSb.AppendLine(" FROM m_syouhin_kakakusettei WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" LEFT JOIN m_meisyou as m_meisyou_01 WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" ON m_syouhin_kakakusettei.syouhin_kbn=m_meisyou_01.code  ")
        commandTextSb.AppendLine(" AND m_meisyou_01.meisyou_syubetu='01'  ")
        commandTextSb.AppendLine(" LEFT JOIN m_tyousahouhou WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" ON m_tyousahouhou.tys_houhou_no=m_syouhin_kakakusettei.tys_houhou_no  ")
        commandTextSb.AppendLine(" LEFT JOIN m_meisyou as m_meisyou_02 WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" ON m_syouhin_kakakusettei.tys_gaiyou=m_meisyou_02.code  ")
        commandTextSb.AppendLine(" AND m_meisyou_02.meisyou_syubetu='02'  ")
        commandTextSb.AppendLine(" LEFT JOIN m_syouhin WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" ON m_syouhin_kakakusettei.syouhin_cd=m_syouhin.syouhin_cd  ")
        commandTextSb.AppendLine(" LEFT JOIN m_meisyou as m_meisyou_03 WITH (READCOMMITTED) ")
        commandTextSb.AppendLine(" ON m_syouhin_kakakusettei.kkk_settei_basyo=m_meisyou_03.code  ")
        commandTextSb.AppendLine(" AND m_meisyou_03.meisyou_syubetu='03'  ")
        commandTextSb.AppendLine(" WHERE syouhin_kbn IS NOT NULL  ")
        '============================2011/04/26 車龍 修正 開始↓=====================================
        'If strKbn.Trim = "" And strHouhou.Trim = "" And strGaiyou.Trim = "" Then
        If strKbn.Trim = "" And strHouhou.Trim = "" And strGaiyou.Trim = "" And strSyouhinCd.Trim.Equals(String.Empty) Then
            '========================2011/04/26 車龍 修正 終了↑=====================================
            commandTextSb.AppendLine(" AND syouhin_kbn IS NULL  ")
        Else
            If strKbn.Trim <> "" Then
                commandTextSb.AppendLine(" AND m_syouhin_kakakusettei.syouhin_kbn =@syouhin_kbn ")
                paramList.Add(MakeParam("@syouhin_kbn", SqlDbType.VarChar, 10, strKbn))
            End If
            If strHouhou.Trim <> "" Then
                commandTextSb.AppendLine(" AND m_syouhin_kakakusettei.tys_houhou_no =@tys_houhou_no ")
                paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.VarChar, 10, strHouhou))
            End If
            If strGaiyou.Trim <> "" Then
                commandTextSb.AppendLine(" AND m_syouhin_kakakusettei.tys_gaiyou =@tys_gaiyou ")
                paramList.Add(MakeParam("@tys_gaiyou", SqlDbType.VarChar, 10, strGaiyou))
            End If
            '============================2011/04/26 車龍 追加 開始↓=====================================
            '｢商品コード｣
            If Not strSyouhinCd.Trim.Equals(String.Empty) Then
                commandTextSb.AppendLine(" AND m_syouhin_kakakusettei.syouhin_cd =@syouhin_cd ")
                paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, strSyouhinCd))
            End If
            '============================2011/04/26 車龍 追加 終了↑=====================================
        End If
        commandTextSb.AppendLine(" ORDER BY m_syouhin_kakakusettei.syouhin_kbn,m_syouhin_kakakusettei.tys_houhou_no,m_syouhin_kakakusettei.tys_gaiyou")
        'パラメータの設定

        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                         "dsDataSet", paramList.ToArray)

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
            commandTextSb.AppendLine(" ,tys_houhou_mei_ryaku AS meisyou   ")
            commandTextSb.AppendLine(" ,tys_houhou_mei AS meisyou2   ")
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
            commandTextSb.AppendLine(" m_meisyou   WITH (READCOMMITTED)  ")
            commandTextSb.AppendLine(" WHERE  meisyou_syubetu=@meisyou_syubetu  ")
            commandTextSb.AppendLine(" ORDER BY hyouji_jyun ")
            'パラメータの設定
            paramList.Add(MakeParam("@meisyou_syubetu", SqlDbType.VarChar, 2, strSyubetu))
        End If

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    "dsDataSet", paramList.ToArray)

        Return dsDataSet.Tables(0)


    End Function
    Public Function SelJyuufuku(ByVal strKbn As String, ByVal strHouhou As String, ByVal strGaiyou As String) As DataTable

        ' DataSetインスタンスの生成()
        Dim dsDataSet As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        'SQL文
        commandTextSb.AppendLine(" SELECT   ")
        commandTextSb.AppendLine(" ISNULL(CONVERT(varchar(19),upd_datetime,21),'') AS upd_datetime ")
        commandTextSb.AppendLine(" FROM  ")
        commandTextSb.AppendLine(" m_syouhin_kakakusettei  WITH (READCOMMITTED)  ")
        commandTextSb.AppendLine(" WHERE  syouhin_kbn=@strKbn  ")
        commandTextSb.AppendLine(" AND tys_houhou_no=@strHouhou  ")
        commandTextSb.AppendLine(" AND tys_gaiyou=@strGaiyou  ")

        'パラメータの設定
        paramList.Add(MakeParam("@strKbn", SqlDbType.VarChar, 10, strKbn))
        paramList.Add(MakeParam("@strHouhou", SqlDbType.VarChar, 10, strHouhou))
        paramList.Add(MakeParam("@strGaiyou", SqlDbType.VarChar, 10, strGaiyou))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    "dsDataSet", paramList.ToArray)

        Return dsDataSet.Tables(0)
    End Function

    Public Function InsKakakusettei(ByVal strKbn As String, _
                               ByVal strHouhou As String, _
                               ByVal strGaiyou As String, _
                               ByVal strSyouhinCd As String, _
                               ByVal strSetteiBasyo As String, _
                               ByVal strUserId As String) As Integer



        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        commandTextSb.AppendLine("INSERT INTO m_syouhin_kakakusettei WITH (UPDLOCK) ")
        commandTextSb.AppendLine("(syouhin_kbn")
        commandTextSb.AppendLine(",tys_houhou_no")
        commandTextSb.AppendLine(",tys_gaiyou")
        commandTextSb.AppendLine(",syouhin_cd")
        commandTextSb.AppendLine(",kkk_settei_basyo")
        commandTextSb.AppendLine(",add_login_user_id")
        commandTextSb.AppendLine(",add_datetime)")

        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine(" @syouhin_kbn")
        commandTextSb.AppendLine(",@tys_houhou_no")
        commandTextSb.AppendLine(",@tys_gaiyou")
        commandTextSb.AppendLine(",@syouhin_cd")
        commandTextSb.AppendLine(",@kkk_settei_basyo")
        commandTextSb.AppendLine(",@add_login_user_id")
        commandTextSb.AppendLine(",GETDATE() ")


        'パラメータの設定()
        paramList.Add(MakeParam("@syouhin_kbn", SqlDbType.VarChar, 10, strKbn))
        paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.VarChar, 10, strHouhou))
        paramList.Add(MakeParam("@tys_gaiyou", SqlDbType.VarChar, 10, strGaiyou))
        paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, strSyouhinCd))
        paramList.Add(MakeParam("@kkk_settei_basyo", SqlDbType.VarChar, 10, strSetteiBasyo))

        paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, strUserId))
        ' クエリ実行
        Return ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    commandTextSb.ToString(), _
                                    paramList.ToArray)


    End Function
    Public Function SelHaita(ByVal strKbn As String, ByVal strHouhou As String, ByVal strGaiyou As String, ByVal strKousin As String) As DataTable

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
        commandTextSb.AppendLine(" m_syouhin_kakakusettei  WITH (READCOMMITTED)  ")
        commandTextSb.AppendLine(" WHERE  syouhin_kbn=@strKbn  ")
        commandTextSb.AppendLine(" AND tys_houhou_no=@strHouhou  ")
        commandTextSb.AppendLine(" AND tys_gaiyou=@strGaiyou  ")
        commandTextSb.AppendLine(" AND CONVERT(varchar(19),upd_datetime,21)<>@upd_datetime  ")
        commandTextSb.AppendLine(" AND upd_datetime IS NOT NULL  ")
        'パラメータの設定
        paramList.Add(MakeParam("@strKbn", SqlDbType.VarChar, 10, strKbn))
        paramList.Add(MakeParam("@strHouhou", SqlDbType.VarChar, 10, strHouhou))
        paramList.Add(MakeParam("@strGaiyou", SqlDbType.VarChar, 10, strGaiyou))
        paramList.Add(MakeParam("@upd_datetime", SqlDbType.VarChar, 19, strKousin))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    "dsDataSet", paramList.ToArray)

        Return dsDataSet.Tables(0)
    End Function

    Public Function UpdKakakusettei(ByVal strKbn As String, _
                               ByVal strHouhou As String, _
                               ByVal strGaiyou As String, _
                               ByVal strSyouhinCd As String, _
                               ByVal strSetteiBasyo As String, _
                               ByVal strUserId As String) As Integer

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        'SQL文
        commandTextSb.AppendLine(" UPDATE m_syouhin_kakakusettei  WITH (UPDLOCK)    ")
        commandTextSb.AppendLine(" SET syouhin_cd=@syouhin_cd ")
        commandTextSb.AppendLine(" ,kkk_settei_basyo=@kkk_settei_basyo ")
        commandTextSb.AppendLine(" ,upd_login_user_id=@add_login_user_id")
        commandTextSb.AppendLine(" ,upd_datetime=GETDATE()")
        commandTextSb.AppendLine(" WHERE  syouhin_kbn=@syouhin_kbn  ")
        commandTextSb.AppendLine(" AND tys_houhou_no=@tys_houhou_no  ")
        commandTextSb.AppendLine(" AND tys_gaiyou=@tys_gaiyou  ")
        'パラメータの設定()
        paramList.Add(MakeParam("@syouhin_kbn", SqlDbType.VarChar, 10, strKbn))
        paramList.Add(MakeParam("@tys_houhou_no", SqlDbType.VarChar, 10, strHouhou))
        paramList.Add(MakeParam("@tys_gaiyou", SqlDbType.VarChar, 10, strGaiyou))
        paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, strSyouhinCd))
        paramList.Add(MakeParam("@kkk_settei_basyo", SqlDbType.VarChar, 10, strSetteiBasyo))

        paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, strUserId))
        ' クエリ実行
        Return ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    commandTextSb.ToString(), _
                                    paramList.ToArray)



    End Function

    Public Function DelKakakusettei(ByVal strKbn As String, ByVal strHouhou As String, ByVal strGaiyou As String) As Integer

        ' DataSetインスタンスの生成()
        Dim dsDataSet As New SyouhinDataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        commandTextSb.AppendLine("DELETE FROM m_syouhin_kakakusettei WITH (UPDLOCK) ")
        commandTextSb.AppendLine(" WHERE  syouhin_kbn=@strKbn  ")
        commandTextSb.AppendLine(" AND tys_houhou_no=@strHouhou  ")
        commandTextSb.AppendLine(" AND tys_gaiyou=@strGaiyou  ")


        'パラメータの設定
        paramList.Add(MakeParam("@strKbn", SqlDbType.VarChar, 10, strKbn))
        paramList.Add(MakeParam("@strHouhou", SqlDbType.VarChar, 10, strHouhou))
        paramList.Add(MakeParam("@strGaiyou", SqlDbType.VarChar, 10, strGaiyou))

        ' クエリ実行
        Return ExecuteNonQuery(connStr, _
                                    CommandType.Text, _
                                    commandTextSb.ToString(), _
                                    paramList.ToArray)


    End Function

    '========================2011/04/26 車龍 追加 開始↓============================
    ''' <summary>
    ''' 重複チェック(商品区分、調査方法、商品ｺｰﾄﾞ)
    ''' </summary>
    ''' <param name="strKbn">商品区分</param>
    ''' <param name="strHouhou">調査方法</param>
    ''' <param name="strSyouhin">商品ｺｰﾄﾞ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2011/04/26　車龍(大連情報システム部)　新規作成</history>
    Public Function SelJyuufukuSyouhin(ByVal strKbn As String, ByVal strHouhou As String, ByVal strSyouhin As String) As DataTable

        ' DataSetインスタンスの生成()
        Dim dsDataSet As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        'SQL文
        commandTextSb.AppendLine(" SELECT   ")
        commandTextSb.AppendLine(" ISNULL(CONVERT(varchar(19),upd_datetime,21),'') AS upd_datetime ")
        commandTextSb.AppendLine(" FROM  ")
        commandTextSb.AppendLine(" m_syouhin_kakakusettei  WITH (READCOMMITTED)  ")
        commandTextSb.AppendLine(" WHERE  syouhin_kbn=@strKbn  ")
        commandTextSb.AppendLine(" AND tys_houhou_no=@strHouhou  ")
        commandTextSb.AppendLine(" AND syouhin_cd=@syouhin_cd  ")

        'パラメータの設定
        paramList.Add(MakeParam("@strKbn", SqlDbType.VarChar, 10, strKbn))
        paramList.Add(MakeParam("@strHouhou", SqlDbType.VarChar, 10, strHouhou))
        paramList.Add(MakeParam("@syouhin_cd", SqlDbType.VarChar, 8, strSyouhin))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    "dsDataSet", paramList.ToArray)

        Return dsDataSet.Tables(0)
    End Function
    '========================2011/04/26 車龍 追加 終了↑============================

End Class

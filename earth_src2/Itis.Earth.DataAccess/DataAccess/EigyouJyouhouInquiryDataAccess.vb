Imports System.Data.SqlClient
Imports System.Text
Imports Itis.ApplicationBlocks.Data.SQLHelper

''' <summary>加盟店営業情報を検索する</summary>
''' <remarks>加盟店営業情報検索機能を提供する</remarks>
''' <history>
''' <para>2009/07/16　高雅娟(大連情報システム部)　新規作成</para>
''' </history>
Public Class EigyouJyouhouInquiryDataAccess
    Private connStr As String = ConnectionManager.Instance.ConnectionString
    ''' <summary>
    ''' 組織レベルを取得する。
    ''' </summary>
    ''' <returns>組織レベルデータテーブル</returns>
    Public Function selSosikiLevel() As EigyouJyouhouDataSet.sosikiLabelDataTable

        Dim sqlSb As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        Dim dsSosikiLabel As New EigyouJyouhouDataSet

        sqlSb.AppendLine(" SELECT ")
        sqlSb.AppendLine(" code, ")
        sqlSb.AppendLine(" meisyou ")
        sqlSb.AppendLine(" FROM m_meisyou WITH (READCOMMITTED) ")
        sqlSb.AppendLine(" WHERE  meisyou_syubetu= @meisyou_syubetu ")

        'パラメータの設定
        paramList.Add(MakeParam("@meisyou_syubetu", SqlDbType.Int, 2, "50"))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, sqlSb.ToString(), dsSosikiLabel, _
                    dsSosikiLabel.sosikiLabel.TableName, paramList.ToArray)

        '終了処理
        sqlSb = Nothing

        Return dsSosikiLabel.sosikiLabel
    End Function
    ''' <summary>
    ''' 組織レベルを取得する。
    ''' </summary>
    ''' <returns>組織レベルデータテーブル</returns>
    Public Function selSosikiLevel2(ByVal strSosiki As String, ByVal strSosiki2 As String, ByVal strBusyoCd As String, ByVal strBusyoCd2 As String, ByVal strKbn As String) As EigyouJyouhouDataSet.sosikiLabelDataTable

        Dim sqlSb As New System.Text.StringBuilder
        Dim strS As String = ""
        Dim strB As String = ""

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        Dim dsSosikiLabel As New EigyouJyouhouDataSet

        sqlSb.AppendLine(" SELECT ")
        sqlSb.AppendLine(" code, ")
        sqlSb.AppendLine(" meisyou ")
        sqlSb.AppendLine(" FROM m_meisyou WITH (READCOMMITTED) ")
        sqlSb.AppendLine(" WHERE  meisyou_syubetu= @meisyou_syubetu ")
        sqlSb.AppendLine(" AND code IN  (")
        If strSosiki <> "" And strSosiki2 <> "" Then
            sqlSb.AppendLine(" SELECT substring(busyo_cd,1,2) as cd FROM   ")
            sqlSb.AppendLine(" (SELECT a6.busyo_cd, a6.sosiki_level,a6.busyo_mei, ")
            sqlSb.AppendLine(" ISNULL(a1.busyo_cd,'0000')  ")
            sqlSb.AppendLine(" +ISNULL(a2.busyo_cd,'0000')  ")
            sqlSb.AppendLine(" +ISNULL(a3.busyo_cd,'0000')  ")
            sqlSb.AppendLine(" +ISNULL(a4.busyo_cd,'0000')  ")
            sqlSb.AppendLine(" +ISNULL(a5.busyo_cd,'0000')  ")
            sqlSb.AppendLine(" +ISNULL(a6.busyo_cd,'0000') AS cd  ")
            sqlSb.AppendLine(" FROM m_busyo_kanri a6  WITH (READCOMMITTED)  ")
            sqlSb.AppendLine(" LEFT JOIN m_busyo_kanri a5  WITH (READCOMMITTED)  ")
            sqlSb.AppendLine(" ON a6.joui_soiki = a5.busyo_cd  ")
            sqlSb.AppendLine(" LEFT JOIN m_busyo_kanri a4  WITH (READCOMMITTED)  ")
            sqlSb.AppendLine(" ON a5.joui_soiki = a4.busyo_cd  ")
            sqlSb.AppendLine(" LEFT JOIN m_busyo_kanri a3  WITH (READCOMMITTED)  ")
            sqlSb.AppendLine(" ON a4.joui_soiki = a3.busyo_cd  ")
            sqlSb.AppendLine(" LEFT JOIN m_busyo_kanri a2  WITH (READCOMMITTED)  ")
            sqlSb.AppendLine(" ON a3.joui_soiki = a2.busyo_cd  ")
            sqlSb.AppendLine(" LEFT JOIN m_busyo_kanri a1  WITH (READCOMMITTED)  ")
            sqlSb.AppendLine(" ON a2.joui_soiki = a1.busyo_cd) AS CD  ")
            sqlSb.AppendLine(" WHERE ")
            sqlSb.AppendLine(" (SUBSTRING(cd,1,4)=@BusyoCd1 OR ")
            sqlSb.AppendLine(" SUBSTRING(cd,5,4)=@BusyoCd1  OR  ")
            sqlSb.AppendLine(" SUBSTRING(cd,9,4)=@BusyoCd1  OR  ")
            sqlSb.AppendLine(" SUBSTRING(cd,13,4)=@BusyoCd1  OR ")
            sqlSb.AppendLine(" SUBSTRING(cd,17,4)=@BusyoCd1  OR ")
            sqlSb.AppendLine(" SUBSTRING(cd,21,4)=@BusyoCd1  ) ")
            sqlSb.AppendLine(" AND sosiki_level in ( SELECT  ")
            sqlSb.AppendLine("  code ")
            sqlSb.AppendLine("  FROM m_meisyou WITH (READCOMMITTED)  ")
            sqlSb.AppendLine("  WHERE  meisyou_syubetu= @meisyou_syubetu ")
            sqlSb.AppendLine("  AND ( ")
            If strKbn <> "1" Then
                sqlSb.AppendLine("  code >=@strSosiki1 ")
                sqlSb.AppendLine("  OR  ")
                sqlSb.AppendLine("  code >= @strSosiki2)) ")
            Else
                sqlSb.AppendLine("  code =@strSosiki1 ")
                sqlSb.AppendLine("  OR  ")
                sqlSb.AppendLine("  code = @strSosiki2)) ")
            End If

            sqlSb.AppendLine(" UNION ALL ")
            sqlSb.AppendLine(" SELECT substring(busyo_cd,1,2) as cd FROM   ")
            sqlSb.AppendLine(" (SELECT a6.busyo_cd, a6.sosiki_level,a6.busyo_mei, ")
            sqlSb.AppendLine(" ISNULL(a1.busyo_cd,'0000')  ")
            sqlSb.AppendLine(" +ISNULL(a2.busyo_cd,'0000')  ")
            sqlSb.AppendLine(" +ISNULL(a3.busyo_cd,'0000')  ")
            sqlSb.AppendLine(" +ISNULL(a4.busyo_cd,'0000')  ")
            sqlSb.AppendLine(" +ISNULL(a5.busyo_cd,'0000')  ")
            sqlSb.AppendLine(" +ISNULL(a6.busyo_cd,'0000') AS cd  ")
            sqlSb.AppendLine(" FROM m_busyo_kanri a6  WITH (READCOMMITTED)  ")
            sqlSb.AppendLine(" LEFT JOIN m_busyo_kanri a5  WITH (READCOMMITTED)  ")
            sqlSb.AppendLine(" ON a6.joui_soiki = a5.busyo_cd  ")
            sqlSb.AppendLine(" LEFT JOIN m_busyo_kanri a4  WITH (READCOMMITTED)  ")
            sqlSb.AppendLine(" ON a5.joui_soiki = a4.busyo_cd  ")
            sqlSb.AppendLine(" LEFT JOIN m_busyo_kanri a3  WITH (READCOMMITTED)  ")
            sqlSb.AppendLine(" ON a4.joui_soiki = a3.busyo_cd  ")
            sqlSb.AppendLine(" LEFT JOIN m_busyo_kanri a2  WITH (READCOMMITTED)  ")
            sqlSb.AppendLine(" ON a3.joui_soiki = a2.busyo_cd  ")
            sqlSb.AppendLine(" LEFT JOIN m_busyo_kanri a1  WITH (READCOMMITTED)  ")
            sqlSb.AppendLine(" ON a2.joui_soiki = a1.busyo_cd) AS CD  ")
            sqlSb.AppendLine(" WHERE ")
            sqlSb.AppendLine(" (SUBSTRING(cd,1,4)=@BusyoCd2 OR ")
            sqlSb.AppendLine(" SUBSTRING(cd,5,4)=@BusyoCd2  OR  ")
            sqlSb.AppendLine(" SUBSTRING(cd,9,4)=@BusyoCd2  OR  ")
            sqlSb.AppendLine(" SUBSTRING(cd,13,4)=@BusyoCd2 OR ")
            sqlSb.AppendLine(" SUBSTRING(cd,17,4)=@BusyoCd2 OR ")
            sqlSb.AppendLine(" SUBSTRING(cd,21,4)=@BusyoCd2  ) ")
            sqlSb.AppendLine(" AND sosiki_level in ( SELECT  ")
            sqlSb.AppendLine("  code ")
            sqlSb.AppendLine("  FROM m_meisyou WITH (READCOMMITTED)  ")
            sqlSb.AppendLine("  WHERE  meisyou_syubetu= @meisyou_syubetu ")
            sqlSb.AppendLine("  AND ( ")
            If strKbn <> "1" Then
                sqlSb.AppendLine("  code >=@strSosiki1 ")
                sqlSb.AppendLine("  OR  ")
                sqlSb.AppendLine("  code >= @strSosiki2)) ")
            Else
                sqlSb.AppendLine("  code =@strSosiki1 ")
                sqlSb.AppendLine("  OR  ")
                sqlSb.AppendLine("  code = @strSosiki2)) ")
            End If
            
            sqlSb.AppendLine(" )")
            'パラメータの設定
            paramList.Add(MakeParam("@meisyou_syubetu", SqlDbType.Int, 2, "50"))
            paramList.Add(MakeParam("@strSosiki1", SqlDbType.Int, 4, strSosiki))
            paramList.Add(MakeParam("@strSosiki2", SqlDbType.Int, 4, strSosiki2))
            paramList.Add(MakeParam("@BusyoCd1", SqlDbType.VarChar, 4, strBusyoCd))
            paramList.Add(MakeParam("@BusyoCd2", SqlDbType.VarChar, 4, strBusyoCd2))
        Else
            If strSosiki = "" Then
                strS = strSosiki2
                strB = strBusyoCd2
            Else
                strS = strSosiki
                strB = strBusyoCd
            End If
            sqlSb.AppendLine(" SELECT substring(busyo_cd,1,2) as cd FROM   ")
            sqlSb.AppendLine(" (SELECT a6.busyo_cd, a6.sosiki_level,a6.busyo_mei, ")
            sqlSb.AppendLine(" ISNULL(a1.busyo_cd,'0000')  ")
            sqlSb.AppendLine(" +ISNULL(a2.busyo_cd,'0000')  ")
            sqlSb.AppendLine(" +ISNULL(a3.busyo_cd,'0000')  ")
            sqlSb.AppendLine(" +ISNULL(a4.busyo_cd,'0000')  ")
            sqlSb.AppendLine(" +ISNULL(a5.busyo_cd,'0000')  ")
            sqlSb.AppendLine(" +ISNULL(a6.busyo_cd,'0000') AS cd  ")
            sqlSb.AppendLine(" FROM m_busyo_kanri a6  WITH (READCOMMITTED)  ")
            sqlSb.AppendLine(" LEFT JOIN m_busyo_kanri a5  WITH (READCOMMITTED)  ")
            sqlSb.AppendLine(" ON a6.joui_soiki = a5.busyo_cd  ")
            sqlSb.AppendLine(" LEFT JOIN m_busyo_kanri a4  WITH (READCOMMITTED)  ")
            sqlSb.AppendLine(" ON a5.joui_soiki = a4.busyo_cd  ")
            sqlSb.AppendLine(" LEFT JOIN m_busyo_kanri a3  WITH (READCOMMITTED)  ")
            sqlSb.AppendLine(" ON a4.joui_soiki = a3.busyo_cd  ")
            sqlSb.AppendLine(" LEFT JOIN m_busyo_kanri a2  WITH (READCOMMITTED)  ")
            sqlSb.AppendLine(" ON a3.joui_soiki = a2.busyo_cd  ")
            sqlSb.AppendLine(" LEFT JOIN m_busyo_kanri a1  WITH (READCOMMITTED)  ")
            sqlSb.AppendLine(" ON a2.joui_soiki = a1.busyo_cd) AS CD  ")
            sqlSb.AppendLine(" WHERE ")
            sqlSb.AppendLine(" (SUBSTRING(cd,1,4)=@BusyoCd1 OR ")
            sqlSb.AppendLine(" SUBSTRING(cd,5,4)=@BusyoCd1  OR  ")
            sqlSb.AppendLine(" SUBSTRING(cd,9,4)=@BusyoCd1  OR  ")
            sqlSb.AppendLine(" SUBSTRING(cd,13,4)=@BusyoCd1  OR ")
            sqlSb.AppendLine(" SUBSTRING(cd,17,4)=@BusyoCd1  OR ")
            sqlSb.AppendLine(" SUBSTRING(cd,21,4)=@BusyoCd1  ) ")
            sqlSb.AppendLine(" AND sosiki_level in ( SELECT  ")
            sqlSb.AppendLine("  code ")
            sqlSb.AppendLine("  FROM m_meisyou WITH (READCOMMITTED)  ")
            sqlSb.AppendLine("  WHERE  meisyou_syubetu= @meisyou_syubetu ")
            sqlSb.AppendLine("  AND ( ")
            If strKbn <> "1" Then

                sqlSb.AppendLine("  code >= @strSosiki1)) ")
            Else

                sqlSb.AppendLine("  code = @strSosiki1)) ")
            End If
            sqlSb.AppendLine(" )")
            'パラメータの設定
            paramList.Add(MakeParam("@meisyou_syubetu", SqlDbType.Int, 2, "50"))
            paramList.Add(MakeParam("@strSosiki1", SqlDbType.Int, 4, strS))
            paramList.Add(MakeParam("@BusyoCd1", SqlDbType.VarChar, 4, strB))
        End If

        ' 検索実行
        FillDataset(connStr, CommandType.Text, sqlSb.ToString(), dsSosikiLabel, _
                    dsSosikiLabel.sosikiLabel.TableName, paramList.ToArray)

        '終了処理
        sqlSb = Nothing

        Return dsSosikiLabel.sosikiLabel
    End Function
    ''' <summary>
    ''' 部署コードと名称を取得する。
    ''' </summary>
    ''' <param name="strSosikiLevel">組織レベル</param>
    ''' <returns>部署情報データテーブル</returns>
    Public Function selBusyoCd(ByVal strSosikiLevel As String) As EigyouJyouhouDataSet.busyoCdDataTable

        Dim sqlSb As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        Dim dsBusyoCd As New EigyouJyouhouDataSet

        sqlSb.AppendLine(" SELECT ")
        sqlSb.AppendLine(" busyo_cd, ")
        sqlSb.AppendLine(" busyo_mei ")
        sqlSb.AppendLine(" FROM m_busyo_kanri WITH (READCOMMITTED) ")
        sqlSb.AppendLine(" WHERE  sosiki_level= @sosiki_level ")

        'パラメータの設定
        paramList.Add(MakeParam("@sosiki_level", SqlDbType.Int, 4, strSosikiLevel))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, sqlSb.ToString(), dsBusyoCd, _
                    dsBusyoCd.busyoCd.TableName, paramList.ToArray)

        '終了処理
        sqlSb = Nothing

        Return dsBusyoCd.busyoCd
    End Function
    ''' <summary>
    ''' 部署コードと名称を取得する。
    ''' </summary>
    ''' <param name="strSosikiLevel">組織レベル</param>
    ''' <returns>部署情報データテーブル</returns>
    Public Function selBusyoCd2(ByVal strSosikiLevel As String, ByVal strBusyoCd As String, ByVal strSansyouBusyoCd As String) As EigyouJyouhouDataSet.busyoCdDataTable

        Dim sqlSb As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        Dim dsBusyoCd As New EigyouJyouhouDataSet
        Dim strB As String = ""
        If strBusyoCd <> "" And strSansyouBusyoCd = "" Then
            strB = strBusyoCd
        End If
        If strBusyoCd = "" And strSansyouBusyoCd <> "" Then
            strB = strSansyouBusyoCd
        End If
        If strBusyoCd <> "" And strSansyouBusyoCd <> "" Then
            sqlSb.AppendLine(" SELECT  distinct busyo_cd,busyo_mei FROM")
            sqlSb.AppendLine(" (SELECT  busyo_cd,busyo_mei FROM     ")
            sqlSb.AppendLine(" (SELECT a6.busyo_cd, a6.sosiki_level,a6.busyo_mei, ")
            sqlSb.AppendLine(" ISNULL(a1.busyo_cd,'0000')  ")
            sqlSb.AppendLine(" +ISNULL(a2.busyo_cd,'0000')  ")
            sqlSb.AppendLine(" +ISNULL(a3.busyo_cd,'0000')  ")
            sqlSb.AppendLine(" +ISNULL(a4.busyo_cd,'0000')  ")
            sqlSb.AppendLine(" +ISNULL(a5.busyo_cd,'0000')  ")
            sqlSb.AppendLine(" +ISNULL(a6.busyo_cd,'0000') AS cd  ")
            sqlSb.AppendLine(" FROM m_busyo_kanri a6  WITH (READCOMMITTED)  ")
            sqlSb.AppendLine(" LEFT JOIN m_busyo_kanri a5  WITH (READCOMMITTED)  ")
            sqlSb.AppendLine(" ON a6.joui_soiki = a5.busyo_cd  ")
            sqlSb.AppendLine(" LEFT JOIN m_busyo_kanri a4  WITH (READCOMMITTED)  ")
            sqlSb.AppendLine(" ON a5.joui_soiki = a4.busyo_cd  ")
            sqlSb.AppendLine(" LEFT JOIN m_busyo_kanri a3  WITH (READCOMMITTED)  ")
            sqlSb.AppendLine(" ON a4.joui_soiki = a3.busyo_cd  ")
            sqlSb.AppendLine(" LEFT JOIN m_busyo_kanri a2  WITH (READCOMMITTED)  ")
            sqlSb.AppendLine(" ON a3.joui_soiki = a2.busyo_cd  ")
            sqlSb.AppendLine(" LEFT JOIN m_busyo_kanri a1  WITH (READCOMMITTED)  ")
            sqlSb.AppendLine(" ON a2.joui_soiki = a1.busyo_cd) AS CD  ")
            sqlSb.AppendLine(" WHERE ")
            sqlSb.AppendLine(" (SUBSTRING(cd,1,4)=@BusyoCd1 OR ")
            sqlSb.AppendLine(" SUBSTRING(cd,5,4)=@BusyoCd1  OR  ")
            sqlSb.AppendLine(" SUBSTRING(cd,9,4)=@BusyoCd1  OR  ")
            sqlSb.AppendLine(" SUBSTRING(cd,13,4)=@BusyoCd1  OR ")
            sqlSb.AppendLine(" SUBSTRING(cd,17,4)=@BusyoCd1  OR ")
            sqlSb.AppendLine(" SUBSTRING(cd,21,4)=@BusyoCd1  ) ")
            sqlSb.AppendLine(" AND sosiki_level=@sosiki_level ")
            sqlSb.AppendLine(" UNION ALL ")
            sqlSb.AppendLine(" SELECT busyo_cd,busyo_mei FROM     ")
            sqlSb.AppendLine(" (SELECT a6.busyo_cd, a6.sosiki_level,a6.busyo_mei, ")
            sqlSb.AppendLine(" ISNULL(a1.busyo_cd,'0000')  ")
            sqlSb.AppendLine(" +ISNULL(a2.busyo_cd,'0000')  ")
            sqlSb.AppendLine(" +ISNULL(a3.busyo_cd,'0000')  ")
            sqlSb.AppendLine(" +ISNULL(a4.busyo_cd,'0000')  ")
            sqlSb.AppendLine(" +ISNULL(a5.busyo_cd,'0000')  ")
            sqlSb.AppendLine(" +ISNULL(a6.busyo_cd,'0000') AS cd  ")
            sqlSb.AppendLine(" FROM m_busyo_kanri a6  WITH (READCOMMITTED)  ")
            sqlSb.AppendLine(" LEFT JOIN m_busyo_kanri a5  WITH (READCOMMITTED)  ")
            sqlSb.AppendLine(" ON a6.joui_soiki = a5.busyo_cd  ")
            sqlSb.AppendLine(" LEFT JOIN m_busyo_kanri a4  WITH (READCOMMITTED)  ")
            sqlSb.AppendLine(" ON a5.joui_soiki = a4.busyo_cd  ")
            sqlSb.AppendLine(" LEFT JOIN m_busyo_kanri a3  WITH (READCOMMITTED)  ")
            sqlSb.AppendLine(" ON a4.joui_soiki = a3.busyo_cd  ")
            sqlSb.AppendLine(" LEFT JOIN m_busyo_kanri a2  WITH (READCOMMITTED)  ")
            sqlSb.AppendLine(" ON a3.joui_soiki = a2.busyo_cd  ")
            sqlSb.AppendLine(" LEFT JOIN m_busyo_kanri a1  WITH (READCOMMITTED)  ")
            sqlSb.AppendLine(" ON a2.joui_soiki = a1.busyo_cd) AS CD  ")
            sqlSb.AppendLine(" WHERE ")
            sqlSb.AppendLine(" (SUBSTRING(cd,1,4)=@BusyoCd2 OR ")
            sqlSb.AppendLine(" SUBSTRING(cd,5,4)=@BusyoCd2  OR  ")
            sqlSb.AppendLine(" SUBSTRING(cd,9,4)=@BusyoCd2  OR  ")
            sqlSb.AppendLine(" SUBSTRING(cd,13,4)=@BusyoCd2  OR ")
            sqlSb.AppendLine(" SUBSTRING(cd,17,4)=@BusyoCd2  OR ")
            sqlSb.AppendLine(" SUBSTRING(cd,21,4)=@BusyoCd2  ) ")
            sqlSb.AppendLine(" AND sosiki_level=@sosiki_level ) as main")

            'パラメータの設定
            paramList.Add(MakeParam("@sosiki_level", SqlDbType.Int, 4, strSosikiLevel))
            paramList.Add(MakeParam("@BusyoCd1", SqlDbType.VarChar, 4, strBusyoCd))
            paramList.Add(MakeParam("@BusyoCd2", SqlDbType.VarChar, 4, strSansyouBusyoCd))
        Else
            sqlSb.AppendLine(" SELECT  busyo_cd,busyo_mei FROM     ")
            sqlSb.AppendLine(" (SELECT a6.busyo_cd, a6.sosiki_level,a6.busyo_mei, ")
            sqlSb.AppendLine(" ISNULL(a1.busyo_cd,'0000')  ")
            sqlSb.AppendLine(" +ISNULL(a2.busyo_cd,'0000')  ")
            sqlSb.AppendLine(" +ISNULL(a3.busyo_cd,'0000')  ")
            sqlSb.AppendLine(" +ISNULL(a4.busyo_cd,'0000')  ")
            sqlSb.AppendLine(" +ISNULL(a5.busyo_cd,'0000')  ")
            sqlSb.AppendLine(" +ISNULL(a6.busyo_cd,'0000') AS cd  ")
            sqlSb.AppendLine(" FROM m_busyo_kanri a6  WITH (READCOMMITTED)  ")
            sqlSb.AppendLine(" LEFT JOIN m_busyo_kanri a5  WITH (READCOMMITTED)  ")
            sqlSb.AppendLine(" ON a6.joui_soiki = a5.busyo_cd  ")
            sqlSb.AppendLine(" LEFT JOIN m_busyo_kanri a4  WITH (READCOMMITTED)  ")
            sqlSb.AppendLine(" ON a5.joui_soiki = a4.busyo_cd  ")
            sqlSb.AppendLine(" LEFT JOIN m_busyo_kanri a3  WITH (READCOMMITTED)  ")
            sqlSb.AppendLine(" ON a4.joui_soiki = a3.busyo_cd  ")
            sqlSb.AppendLine(" LEFT JOIN m_busyo_kanri a2  WITH (READCOMMITTED)  ")
            sqlSb.AppendLine(" ON a3.joui_soiki = a2.busyo_cd  ")
            sqlSb.AppendLine(" LEFT JOIN m_busyo_kanri a1  WITH (READCOMMITTED)  ")
            sqlSb.AppendLine(" ON a2.joui_soiki = a1.busyo_cd) AS CD  ")
            sqlSb.AppendLine(" WHERE ")
            sqlSb.AppendLine(" (SUBSTRING(cd,1,4)=@BusyoCd1 OR ")
            sqlSb.AppendLine(" SUBSTRING(cd,5,4)=@BusyoCd1  OR  ")
            sqlSb.AppendLine(" SUBSTRING(cd,9,4)=@BusyoCd1  OR  ")
            sqlSb.AppendLine(" SUBSTRING(cd,13,4)=@BusyoCd1  OR ")
            sqlSb.AppendLine(" SUBSTRING(cd,17,4)=@BusyoCd1  OR ")
            sqlSb.AppendLine(" SUBSTRING(cd,21,4)=@BusyoCd1  ) ")
            sqlSb.AppendLine(" AND sosiki_level=@sosiki_level ")

            'パラメータの設定
            paramList.Add(MakeParam("@sosiki_level", SqlDbType.Int, 4, strSosikiLevel))
            paramList.Add(MakeParam("@BusyoCd1", SqlDbType.VarChar, 4, strB))

        End If
        
        ' 検索実行
        FillDataset(connStr, CommandType.Text, sqlSb.ToString(), dsBusyoCd, _
                    dsBusyoCd.busyoCd.TableName, paramList.ToArray)

        '終了処理
        sqlSb = Nothing

        Return dsBusyoCd.busyoCd
    End Function

    ''' <summary>
    ''' ログインユーザーの営業マン区分を取得する。
    ''' </summary>
    ''' <param name="strUserId">ログインユーザーID</param>
    ''' <returns>ログインユーザーの営業マン区分データテーブル</returns>
    Public Function selEigyouManKbn(ByVal strUserId As String) As EigyouJyouhouDataSet.eigyouManKbnDataTable

        Dim sqlSb As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        Dim dsEigyouManKubun As New EigyouJyouhouDataSet

        sqlSb.AppendLine(" SELECT ")
        sqlSb.AppendLine(" m_jiban_ninsyou.eigyou_man_kbn,  ")
        sqlSb.AppendLine(" m_jiban_ninsyou.login_user_id, ")
        sqlSb.AppendLine(" m_jiban_ninsyou.busyo_cd, ")
        sqlSb.AppendLine(" m_jiban_ninsyou.t_sansyou_busyo_cd, ")
        sqlSb.AppendLine(" M1.sosiki_level AS sosiki_level, ")
        sqlSb.AppendLine(" ISNULL(M2.sosiki_level,'-1') AS sosiki_level2 ")
        sqlSb.AppendLine(" FROM m_jiban_ninsyou WITH (READCOMMITTED) ")
        sqlSb.AppendLine(" LEFT OUTER JOIN m_busyo_kanri AS M1")
        sqlSb.AppendLine(" ON  m_jiban_ninsyou.busyo_cd=M1.busyo_cd")
        sqlSb.AppendLine(" LEFT OUTER JOIN m_busyo_kanri AS M2")
        sqlSb.AppendLine(" ON  m_jiban_ninsyou.t_sansyou_busyo_cd=M2.busyo_cd ")
        sqlSb.AppendLine(" WHERE  m_jiban_ninsyou.login_user_id= @login_user_id ")

        'パラメータの設定
        paramList.Add(MakeParam("@login_user_id", SqlDbType.VarChar, 30, strUserId))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, sqlSb.ToString(), dsEigyouManKubun, _
                    dsEigyouManKubun.eigyouManKbn.TableName, paramList.ToArray)

        '終了処理
        sqlSb = Nothing

        Return dsEigyouManKubun.eigyouManKbn

    End Function

    ''' <summary>
    ''' 加盟店営業情報データ総数を取得する。
    ''' </summary>
    ''' <param name="dtParamEigyouInfo">検索条件テーブル</param>
    ''' <returns>加盟店営業情報データ総数</returns>
    Public Function selEigyouJyouhouCount(ByVal dtParamEigyouInfo As EigyouJyouhouDataSet.paramEigyouJyouhouDataTable, ByVal chkBusyoCd As Boolean) _
                        As Integer

        Dim commandTextSb As New System.Text.StringBuilder
        Dim isWhere As Boolean = False
        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        Dim dsEigyouJyouhou As New EigyouJyouhouDataSet
        With commandTextSb
            .AppendLine(" SELECT ")
            .AppendLine(" Count(MKK.kameiten_cd) AS kameiten_cd_count")
            .AppendLine(" FROM ( ")
            .AppendLine(" SELECT DISTINCT ")
            .AppendLine(" A.kameiten_cd")
            .AppendLine(" FROM m_kameiten A  WITH (READCOMMITTED) ")
            .AppendLine(" LEFT OUTER JOIN m_todoufuken B ")
            .AppendLine(" ON A.todouhuken_cd=B.todouhuken_cd  ")
            .AppendLine(" LEFT OUTER JOIN m_jhs_mailbox C ")
            .AppendLine(" ON C.PrimaryWindowsNTAccount=A.eigyou_tantousya_mei ")
            .AppendLine(" LEFT OUTER JOIN  ")
            .AppendLine(" ( ")
            .AppendLine(" SELECT A.kameiten_cd,B.keikoku_joukyou,C.meisyou ")
            .AppendLine(" FROM m_kameiten A ")
            .AppendLine(" LEFT OUTER JOIN m_yosinkanri B ")
            .AppendLine(" ON A.tys_seikyuu_saki=B.nayose_saki_cd ")
            .AppendLine(" LEFT OUTER JOIN (SELECT code,meisyou FROM m_meisyou WHERE meisyou_syubetu='52') C ")
            .AppendLine(" ON B.keikoku_joukyou=C.code ")
            .AppendLine(" )D ")
            .AppendLine(" ON D.kameiten_cd=A.kameiten_cd ")
            .AppendLine(" LEFT OUTER JOIN ")
            .AppendLine(" ( ")
            .AppendLine(" SELECT A.kameiten_cd,B.keikoku_joukyou,C.meisyou ")
            .AppendLine(" FROM m_kameiten A ")
            .AppendLine(" LEFT OUTER JOIN m_yosinkanri B ")
            .AppendLine(" ON A.koj_seikyuusaki=B.nayose_saki_cd ")

            .AppendLine(" LEFT OUTER JOIN (SELECT code,meisyou FROM m_meisyou WHERE meisyou_syubetu='52') C ")
            .AppendLine(" ON B.keikoku_joukyou=C.code ")
            .AppendLine(" )E  ")
            .AppendLine(" ON E.kameiten_cd=A.kameiten_cd ")
            .AppendLine(" LEFT OUTER JOIN ")
            .AppendLine(" ( ")
            .AppendLine(" SELECT A.kameiten_cd,B.keikoku_joukyou,C.meisyou ")
            .AppendLine(" FROM m_kameiten A ")
            .AppendLine(" LEFT OUTER JOIN m_yosinkanri B ")
            .AppendLine(" ON A.hansokuhin_seikyuusaki=B.nayose_saki_cd ")

            .AppendLine(" LEFT OUTER JOIN (SELECT code,meisyou FROM m_meisyou WHERE meisyou_syubetu='52') C ")
            .AppendLine(" ON B.keikoku_joukyou=C.code ")
            .AppendLine(" )F ")
            .AppendLine(" ON F.kameiten_cd=A.kameiten_cd ")
            .AppendLine(" LEFT OUTER JOIN m_busyo_kanri G ")
            .AppendLine(" ON G.busyo_cd=B.busyo_cd ")
            If dtParamEigyouInfo(0).tel <> "" Then
                .AppendLine(" LEFT OUTER JOIN m_kameiten_jyuusyo H ")
                .AppendLine(" ON H.kameiten_cd=A.kameiten_cd ")
            End If
            .AppendLine(" WHERE ")
            .AppendLine("       1 = 1 ")

            '区分
            If dtParamEigyouInfo(0).kbn <> String.Empty Then
                Dim arrKbn() As String = dtParamEigyouInfo(0).kbn.Split(",")
                .AppendLine(" AND A.kbn IN ( ")
                For i As Integer = 0 To arrKbn.Length - 1
                    If i = 0 Then
                        .AppendLine("     @kbn" & i & "   ")
                    Else
                        .AppendLine("     ,@kbn" & i & "   ")
                    End If
                    paramList.Add(MakeParam("@kbn" & i, SqlDbType.Char, 1, arrKbn(i)))
                Next
                .AppendLine("       ) ")
            End If

            '退会した加盟店
            If dtParamEigyouInfo(0).torikesi <> "1" Then
                .AppendLine(" AND A.torikesi = 0 ")
            End If
            ' 加盟店コード
            If dtParamEigyouInfo(0).kameitenCd <> String.Empty Then
                .AppendLine(" AND A.kameiten_cd = @kameiten_cd ")
                paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, dtParamEigyouInfo(0).kameitenCd))
            End If
            '加盟店名カナ
            If dtParamEigyouInfo(0).kameitenKana <> String.Empty Then
                .AppendLine(" AND (A.tenmei_kana1 LIKE @kameiten_kana1 ")
                .AppendLine("      OR A.tenmei_kana2 LIKE @kameiten_kana2) ")
                paramList.Add(MakeParam("@kameiten_kana1", SqlDbType.VarChar, 20, "%" & dtParamEigyouInfo(0).kameitenKana & "%"))
                paramList.Add(MakeParam("@kameiten_kana2", SqlDbType.VarChar, 20, "%" & dtParamEigyouInfo(0).kameitenKana & "%"))
            End If
            '系列コード
            If dtParamEigyouInfo(0).keiretuCd <> String.Empty Then
                .AppendLine(" AND A.keiretu_cd = @keiretu_cd ")
                paramList.Add(MakeParam("@keiretu_cd", SqlDbType.VarChar, 5, dtParamEigyouInfo(0).keiretuCd))
            End If
            '電話番号
            If dtParamEigyouInfo(0).tel <> String.Empty Then
                .AppendLine(" AND REPLACE(H.tel_no,'-','') = @tel_no ")
                paramList.Add(MakeParam("@tel_no", SqlDbType.VarChar, 16, dtParamEigyouInfo(0).tel))
            End If
            '都道府県
            If dtParamEigyouInfo(0).todouhukenCd <> String.Empty Then
                .AppendLine(" AND A.todouhuken_cd = @todouhuken_cd ")
                paramList.Add(MakeParam("@todouhuken_cd", SqlDbType.VarChar, 2, dtParamEigyouInfo(0).todouhukenCd))
            End If
            ' 組織レベル
            If dtParamEigyouInfo(0).sosikiLevel <> String.Empty Then


                If dtParamEigyouInfo(0).eigyouManKbn = "1" Then
                    .AppendLine(" AND G.sosiki_level = @sosiki_level2 ")
                    paramList.Add(MakeParam("@sosiki_level2", SqlDbType.VarChar, 2, dtParamEigyouInfo(0).sosikiLevel))

                Else
                    .AppendLine(" AND G.sosiki_level >= @sosiki_level2 ")
                    paramList.Add(MakeParam("@sosiki_level2", SqlDbType.VarChar, 2, dtParamEigyouInfo(0).sosikiLevel))

                End If
                'End If
            End If
            If dtParamEigyouInfo(0).eigyouManKbn = "1" Then
                .AppendLine(" AND A.eigyou_tantousya_mei = @strUserId ")
                paramList.Add(MakeParam("@strUserId", SqlDbType.VarChar, 30, dtParamEigyouInfo(0).loginUserId))
            End If

            '担当営業ID
            If dtParamEigyouInfo(0).tantouEigyouId <> String.Empty Then
                .AppendLine(" AND A.eigyou_tantousya_mei = @eigyou_tantousya_mei ")
                paramList.Add(MakeParam("@eigyou_tantousya_mei", SqlDbType.VarChar, 64, dtParamEigyouInfo(0).tantouEigyouId))
            End If




            If dtParamEigyouInfo(0).eigyouManKbn = "1" Then
                .AppendLine(" AND  B.busyo_cd IN ( ")
                .AppendLine(" @busyo_cd2 ")
                If dtParamEigyouInfo(0).sosikiLevel = "" Then
                    paramList.Add(MakeParam("@busyo_cd2", SqlDbType.VarChar, 4, dtParamEigyouInfo(0).busyo_cd))
                Else
                    paramList.Add(MakeParam("@busyo_cd2", SqlDbType.VarChar, 4, dtParamEigyouInfo(0).BusyoCd))
                End If
                .AppendLine(" ) ")
            Else
                If dtParamEigyouInfo(0).BusyoCd = "0000" Then
                    If chkBusyoCd Then
                        .AppendLine(" AND  B.busyo_cd IN ( ")
                        .AppendLine(" @busyo_cd2 ")
                        .AppendLine(" ) ")
                        paramList.Add(MakeParam("@busyo_cd2", SqlDbType.VarChar, 4, dtParamEigyouInfo(0).BusyoCd))
                    End If

                Else
                    If dtParamEigyouInfo(0).BusyoCd <> "" Then
                        .AppendLine(" AND  B.busyo_cd IN ( ")
                        .AppendLine("SELECT busyo_cd FROM  ")
                        .AppendLine("(SELECT a6.busyo_cd, ")
                        .AppendLine("ISNULL(a1.busyo_cd,'0000') ")
                        .AppendLine("+ISNULL(a2.busyo_cd,'0000') ")
                        .AppendLine("+ISNULL(a3.busyo_cd,'0000') ")
                        .AppendLine("+ISNULL(a4.busyo_cd,'0000') ")
                        .AppendLine("+ISNULL(a5.busyo_cd,'0000') ")
                        .AppendLine("+ISNULL(a6.busyo_cd,'0000') AS cd ")
                        .AppendLine("FROM m_busyo_kanri a6 ")
                        .AppendLine("LEFT JOIN m_busyo_kanri a5 ")
                        .AppendLine("ON a6.joui_soiki = a5.busyo_cd ")
                        .AppendLine("LEFT JOIN m_busyo_kanri a4 ")
                        .AppendLine("ON a5.joui_soiki = a4.busyo_cd ")
                        .AppendLine("LEFT JOIN m_busyo_kanri a3 ")
                        .AppendLine("ON a4.joui_soiki = a3.busyo_cd ")
                        .AppendLine("LEFT JOIN m_busyo_kanri a2 ")
                        .AppendLine("ON a3.joui_soiki = a2.busyo_cd ")
                        .AppendLine("LEFT JOIN m_busyo_kanri a1 ")
                        .AppendLine("ON a2.joui_soiki = a1.busyo_cd) AS TB ")

                        .AppendLine("WHERE   (")
                        'lis6　start
                        'If dtParamEigyouInfo(0).sosikiLevel = "" Then
                        '
                        'Else
                        If chkBusyoCd Then

                            .AppendLine("  B.busyo_cd = @busyo_cd ")
                            paramList.Add(MakeParam("@busyo_cd", SqlDbType.VarChar, 4, dtParamEigyouInfo(0).BusyoCd))

                        Else
                            .AppendLine("SUBSTRING(cd,1,4)=@busyo_cd2 OR  ")
                            .AppendLine("SUBSTRING(cd,5,4)=@busyo_cd2 OR  ")
                            .AppendLine("SUBSTRING(cd,9,4)=@busyo_cd2 OR  ")
                            .AppendLine("SUBSTRING(cd,13,4)=@busyo_cd2 OR  ")
                            .AppendLine("SUBSTRING(cd,17,4)=@busyo_cd2 OR  ")
                            .AppendLine("SUBSTRING(cd,21,4)=@busyo_cd2   ")
                            paramList.Add(MakeParam("@busyo_cd2", SqlDbType.VarChar, 4, dtParamEigyouInfo(0).BusyoCd))

                        End If

                        'End If


                        'lis6　end
                        .AppendLine(" ) ")
                        .AppendLine(" ) ")


                    Else
                        If dtParamEigyouInfo(0).busyo_cd = "0000" Or dtParamEigyouInfo(0).t_sansyou_busyo_cd = "0000" Then

                        Else
                            .AppendLine(" AND  B.busyo_cd IN ( ")
                            .AppendLine("SELECT busyo_cd FROM  ")
                            .AppendLine("(SELECT a6.busyo_cd, ")
                            .AppendLine("ISNULL(a1.busyo_cd,'0000') ")
                            .AppendLine("+ISNULL(a2.busyo_cd,'0000') ")
                            .AppendLine("+ISNULL(a3.busyo_cd,'0000') ")
                            .AppendLine("+ISNULL(a4.busyo_cd,'0000') ")
                            .AppendLine("+ISNULL(a5.busyo_cd,'0000') ")
                            .AppendLine("+ISNULL(a6.busyo_cd,'0000') AS cd ")
                            .AppendLine("FROM m_busyo_kanri a6 ")
                            .AppendLine("LEFT JOIN m_busyo_kanri a5 ")
                            .AppendLine("ON a6.joui_soiki = a5.busyo_cd ")
                            .AppendLine("LEFT JOIN m_busyo_kanri a4 ")
                            .AppendLine("ON a5.joui_soiki = a4.busyo_cd ")
                            .AppendLine("LEFT JOIN m_busyo_kanri a3 ")
                            .AppendLine("ON a4.joui_soiki = a3.busyo_cd ")
                            .AppendLine("LEFT JOIN m_busyo_kanri a2 ")
                            .AppendLine("ON a3.joui_soiki = a2.busyo_cd ")
                            .AppendLine("LEFT JOIN m_busyo_kanri a1 ")
                            .AppendLine("ON a2.joui_soiki = a1.busyo_cd) AS TB ")

                            .AppendLine("WHERE   (")
                            Dim intCHK As Integer = 0
                            If dtParamEigyouInfo(0).busyo_cd <> "" Then
                                .AppendLine("SUBSTRING(cd,1,4)=@busyo_cd2 OR  ")
                                .AppendLine("SUBSTRING(cd,5,4)=@busyo_cd2 OR  ")
                                .AppendLine("SUBSTRING(cd,9,4)=@busyo_cd2 OR  ")
                                .AppendLine("SUBSTRING(cd,13,4)=@busyo_cd2 OR  ")
                                .AppendLine("SUBSTRING(cd,17,4)=@busyo_cd2 OR  ")
                                .AppendLine("SUBSTRING(cd,21,4)=@busyo_cd2   ")
                                paramList.Add(MakeParam("@busyo_cd2", SqlDbType.VarChar, 4, dtParamEigyouInfo(0).busyo_cd))
                                intCHK = intCHK + 1
                            End If

                            If dtParamEigyouInfo(0).t_sansyou_busyo_cd <> "" Then
                                If intCHK = 1 Then
                                    .AppendLine(" OR ")
                                End If
                                .AppendLine(" SUBSTRING(cd,1,4)=@busyo_cd3 OR  ")
                                .AppendLine("SUBSTRING(cd,5,4)=@busyo_cd3 OR  ")
                                .AppendLine("SUBSTRING(cd,9,4)=@busyo_cd3 OR  ")
                                .AppendLine("SUBSTRING(cd,13,4)=@busyo_cd3 OR  ")
                                .AppendLine("SUBSTRING(cd,17,4)=@busyo_cd3 OR  ")
                                .AppendLine("SUBSTRING(cd,21,4)=@busyo_cd3   ")
                                paramList.Add(MakeParam("@busyo_cd3", SqlDbType.VarChar, 4, dtParamEigyouInfo(0).t_sansyou_busyo_cd))
                            End If
                            .AppendLine(" ) ")
                            .AppendLine(" ) ")
                        End If


                    End If

                End If
            End If

            .AppendLine(" ) AS MKK ")

        End With

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsEigyouJyouhou, _
                    dsEigyouJyouhou.eigyouJyouhouCount.TableName, paramList.ToArray)

        '終了処理
        commandTextSb = Nothing

        Return dsEigyouJyouhou.eigyouJyouhouCount(0).kameiten_cd_count

    End Function

    ''' <summary>
    ''' 加盟店営業情報を取得する。
    ''' </summary>
    ''' <param name="dtParamEigyouInfo">検索条件テーブル</param>
    ''' <returns>加盟店営業情報データテーブル</returns>
    Public Function selEigyouJyouhou(ByVal dtParamEigyouInfo As EigyouJyouhouDataSet.paramEigyouJyouhouDataTable, ByVal chkBusyoCd As Boolean) _
                    As EigyouJyouhouDataSet.eigyouJyouhouDataTable


        Dim commandTextSb As New System.Text.StringBuilder
        Dim isWhere As Boolean = False
        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        Dim dsEigyouJyouhou As New EigyouJyouhouDataSet
        With commandTextSb

            .AppendLine(" SELECT DISTINCT ")
            If dtParamEigyouInfo(0).kensakuCount = "100" Then
                .AppendLine("      TOP 100 ")
            End If
            .AppendLine(" A.kameiten_cd, ")
            .AppendLine(" A.kameiten_mei1 AS kameiten_mei, ")
            .AppendLine(" B.todouhuken_cd +':'+B.todouhuken_mei AS todouhuken_mei, ")
            .AppendLine(" C.DisplayName, ")
            .AppendLine(" D.keikoku_joukyou AS tyousa_cd, ")
            .AppendLine(" D.meisyou AS tyousa, ")
            .AppendLine(" E.keikoku_joukyou AS kouji_cd, ")
            .AppendLine(" E.meisyou AS kouji, ")
            .AppendLine(" F.keikoku_joukyou AS hansokuhin_cd, ")
            .AppendLine(" F.meisyou AS hansokuhin, ")
            .AppendLine(" G.busyo_mei ")
            '================2012/03/28 車龍 405721案件の対応 追加↓==================================
            .AppendLine(" ,A.torikesi as torikesi ")
            .AppendLine(" ,CASE ")
            .AppendLine("    WHEN A.torikesi = 0 THEN ")
            .AppendLine("        '0' ")
            .AppendLine("    ELSE ")
            .AppendLine("        CONVERT(VARCHAR(10),A.torikesi) + ':' + ISNULL(MKM.meisyou,'') ")
            .AppendLine("    END AS torikesi_txt ")

            '-----------------------------From 2013.03.11李宇追加----------------------------
            .AppendLine(" ,A.kameiten_mei2")                                             '加盟店名2
            .AppendLine(" ,A.keiretu_cd")                                                '系列コード
            .AppendLine(" ,A.eigyousyo_cd")                                              '営業所コード
            .AppendLine(" ,A.builder_no")                                                'ビルダーNO
            .AppendLine(" ,A.kbn")                                                       '区分
            If dtParamEigyouInfo(0).tel <> "" Then
                .AppendLine(" ,ISNULL(H.jyuusyo1,'') + ISNULL(H.jyuusyo2,'') AS jyuusyo1 ")  '住所
                .AppendLine(" ,H.daihyousya_mei")                                            '代表者名
                .AppendLine(" ,H.tel_no")                                                    '電話番号
            Else
                .AppendLine(" ,' ' AS jyuusyo1 ")  '住所
                .AppendLine(" ,' ' AS daihyousya_mei")                                            '代表者名
                .AppendLine(" ,' ' AS tel_no")                                                    '電話番号
            End If
            '-----------------------------To   2013.03.11李宇追加----------------------------

            '================2012/03/28 車龍 405721案件の対応 追加↑==================================
            .AppendLine(" FROM m_kameiten A  WITH (READCOMMITTED) ")
            '================2012/03/28 車龍 405721案件の対応 追加↓==================================
            .AppendLine(" LEFT OUTER JOIN m_kakutyou_meisyou AS MKM ") '--拡張名称マスタ
            .AppendLine(" ON A.torikesi = MKM.code ")
            .AppendLine(" AND MKM.meisyou_syubetu = 56 ")
            '================2012/03/28 車龍 405721案件の対応 追加↑==================================
            .AppendLine(" LEFT OUTER JOIN m_todoufuken B WITH (READCOMMITTED)  ")
            .AppendLine(" ON A.todouhuken_cd=B.todouhuken_cd  ")
            .AppendLine(" LEFT OUTER JOIN m_jhs_mailbox C WITH (READCOMMITTED)  ")
            .AppendLine(" ON C.PrimaryWindowsNTAccount=A.eigyou_tantousya_mei ")
            .AppendLine(" LEFT OUTER JOIN  ")
            .AppendLine(" ( ")
            .AppendLine(" SELECT A.kameiten_cd,B.keikoku_joukyou,C.meisyou ")
            .AppendLine(" FROM m_kameiten A WITH (READCOMMITTED)  ")
            .AppendLine(" LEFT OUTER JOIN m_yosinkanri B  WITH (READCOMMITTED)  ")
            .AppendLine(" ON A.tys_seikyuu_saki=B.nayose_saki_cd ")
            .AppendLine(" LEFT OUTER JOIN (SELECT code,meisyou FROM m_meisyou WHERE meisyou_syubetu='52') C ")
            .AppendLine(" ON B.keikoku_joukyou=C.code ")
            .AppendLine(" )D ")
            .AppendLine(" ON D.kameiten_cd=A.kameiten_cd ")
            .AppendLine(" LEFT OUTER JOIN ")
            .AppendLine(" ( ")
            .AppendLine(" SELECT A.kameiten_cd,B.keikoku_joukyou,C.meisyou ")
            .AppendLine(" FROM m_kameiten A  WITH (READCOMMITTED)  ")
            .AppendLine(" LEFT OUTER JOIN m_yosinkanri B  WITH (READCOMMITTED)  ")
            .AppendLine(" ON A.koj_seikyuusaki=B.nayose_saki_cd ")

            .AppendLine(" LEFT OUTER JOIN (SELECT code,meisyou FROM  m_meisyou WITH (READCOMMITTED) WHERE meisyou_syubetu='52') C ")
            .AppendLine(" ON B.keikoku_joukyou=C.code ")
            .AppendLine(" )E  ")
            .AppendLine(" ON E.kameiten_cd=A.kameiten_cd ")
            .AppendLine(" LEFT OUTER JOIN ")
            .AppendLine(" ( ")
            .AppendLine(" SELECT A.kameiten_cd,B.keikoku_joukyou,C.meisyou ")
            .AppendLine(" FROM m_kameiten A  WITH (READCOMMITTED)  ")
            .AppendLine(" LEFT OUTER JOIN m_yosinkanri B  WITH (READCOMMITTED)  ")
            .AppendLine(" ON A.hansokuhin_seikyuusaki=B.nayose_saki_cd ")

            .AppendLine(" LEFT OUTER JOIN (SELECT code,meisyou FROM m_meisyou WITH (READCOMMITTED)   WHERE meisyou_syubetu='52') C ")
            .AppendLine(" ON B.keikoku_joukyou=C.code ")
            .AppendLine(" )F ")
            .AppendLine(" ON F.kameiten_cd=A.kameiten_cd ")
            .AppendLine(" LEFT OUTER JOIN m_busyo_kanri G  WITH (READCOMMITTED)  ")
            .AppendLine(" ON G.busyo_cd=B.busyo_cd ")
            If dtParamEigyouInfo(0).tel <> "" Then
                .AppendLine(" LEFT OUTER JOIN m_kameiten_jyuusyo H  WITH (READCOMMITTED)  ")
                .AppendLine(" ON H.kameiten_cd=A.kameiten_cd ")
            End If
            .AppendLine(" WHERE ")
            .AppendLine("       1 = 1 ")

            '区分
            If dtParamEigyouInfo(0).kbn <> String.Empty Then
                Dim arrKbn() As String = dtParamEigyouInfo(0).kbn.Split(",")
                .AppendLine(" AND A.kbn IN ( ")
                For i As Integer = 0 To arrKbn.Length - 1
                    If i = 0 Then
                        .AppendLine("     @kbn" & i & "   ")
                    Else
                        .AppendLine("     ,@kbn" & i & "   ")
                    End If
                    paramList.Add(MakeParam("@kbn" & i, SqlDbType.Char, 1, arrKbn(i)))
                Next
                .AppendLine("       ) ")
            End If

            '退会した加盟店
            If dtParamEigyouInfo(0).torikesi <> "1" Then
                .AppendLine(" AND A.torikesi = 0 ")
            End If
            ' 加盟店コード
            If dtParamEigyouInfo(0).kameitenCd <> String.Empty Then
                .AppendLine(" AND A.kameiten_cd = @kameiten_cd ")
                paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, dtParamEigyouInfo(0).kameitenCd))
            End If
            '加盟店名カナ
            If dtParamEigyouInfo(0).kameitenKana <> String.Empty Then
                .AppendLine(" AND (A.tenmei_kana1 LIKE @kameiten_kana1 ")
                .AppendLine("      OR A.tenmei_kana2 LIKE @kameiten_kana2) ")
                paramList.Add(MakeParam("@kameiten_kana1", SqlDbType.VarChar, 20, "%" & dtParamEigyouInfo(0).kameitenKana & "%"))
                paramList.Add(MakeParam("@kameiten_kana2", SqlDbType.VarChar, 20, "%" & dtParamEigyouInfo(0).kameitenKana & "%"))
            End If
            '系列コード
            If dtParamEigyouInfo(0).keiretuCd <> String.Empty Then
                .AppendLine(" AND A.keiretu_cd = @keiretu_cd ")
                paramList.Add(MakeParam("@keiretu_cd", SqlDbType.VarChar, 5, dtParamEigyouInfo(0).keiretuCd))
            End If
            '電話番号
            If dtParamEigyouInfo(0).tel <> String.Empty Then
                .AppendLine(" AND REPLACE(H.tel_no,'-','') = @tel_no ")
                paramList.Add(MakeParam("@tel_no", SqlDbType.VarChar, 16, dtParamEigyouInfo(0).tel))
            End If
            '都道府県
            If dtParamEigyouInfo(0).todouhukenCd <> String.Empty Then
                .AppendLine(" AND A.todouhuken_cd = @todouhuken_cd ")
                paramList.Add(MakeParam("@todouhuken_cd", SqlDbType.VarChar, 2, dtParamEigyouInfo(0).todouhukenCd))
            End If
            ' 組織レベル
            If dtParamEigyouInfo(0).sosikiLevel <> String.Empty Then


                If dtParamEigyouInfo(0).eigyouManKbn = "1" Then
                    .AppendLine(" AND G.sosiki_level = @sosiki_level2 ")
                    paramList.Add(MakeParam("@sosiki_level2", SqlDbType.VarChar, 2, dtParamEigyouInfo(0).sosikiLevel))

                Else
                    .AppendLine(" AND G.sosiki_level >= @sosiki_level2 ")
                    paramList.Add(MakeParam("@sosiki_level2", SqlDbType.VarChar, 2, dtParamEigyouInfo(0).sosikiLevel))

                End If
                'End If
            End If
            If dtParamEigyouInfo(0).eigyouManKbn = "1" Then
                .AppendLine(" AND A.eigyou_tantousya_mei = @strUserId ")
                paramList.Add(MakeParam("@strUserId", SqlDbType.VarChar, 30, dtParamEigyouInfo(0).loginUserId))
            End If

            '担当営業ID
            If dtParamEigyouInfo(0).tantouEigyouId <> String.Empty Then
                .AppendLine(" AND A.eigyou_tantousya_mei = @eigyou_tantousya_mei ")
                paramList.Add(MakeParam("@eigyou_tantousya_mei", SqlDbType.VarChar, 64, dtParamEigyouInfo(0).tantouEigyouId))
            End If




            If dtParamEigyouInfo(0).eigyouManKbn = "1" Then
                .AppendLine(" AND  B.busyo_cd IN ( ")
                .AppendLine(" @busyo_cd2 ")
                If dtParamEigyouInfo(0).sosikiLevel = "" Then
                    paramList.Add(MakeParam("@busyo_cd2", SqlDbType.VarChar, 4, dtParamEigyouInfo(0).busyo_cd))
                Else
                    paramList.Add(MakeParam("@busyo_cd2", SqlDbType.VarChar, 4, dtParamEigyouInfo(0).BusyoCd))
                End If
                .AppendLine(" ) ")
            Else
                If dtParamEigyouInfo(0).BusyoCd = "0000" Then
                    If chkBusyoCd Then
                        .AppendLine(" AND  B.busyo_cd IN ( ")
                        .AppendLine(" @busyo_cd2 ")
                        .AppendLine(" ) ")
                        paramList.Add(MakeParam("@busyo_cd2", SqlDbType.VarChar, 4, dtParamEigyouInfo(0).BusyoCd))
                    End If

                Else
                    If dtParamEigyouInfo(0).BusyoCd <> "" Then
                        .AppendLine(" AND  B.busyo_cd IN ( ")
                        .AppendLine("SELECT busyo_cd FROM  ")
                        .AppendLine("(SELECT a6.busyo_cd, ")
                        .AppendLine("ISNULL(a1.busyo_cd,'0000') ")
                        .AppendLine("+ISNULL(a2.busyo_cd,'0000') ")
                        .AppendLine("+ISNULL(a3.busyo_cd,'0000') ")
                        .AppendLine("+ISNULL(a4.busyo_cd,'0000') ")
                        .AppendLine("+ISNULL(a5.busyo_cd,'0000') ")
                        .AppendLine("+ISNULL(a6.busyo_cd,'0000') AS cd ")
                        .AppendLine("FROM m_busyo_kanri a6 ")
                        .AppendLine("LEFT JOIN m_busyo_kanri a5 ")
                        .AppendLine("ON a6.joui_soiki = a5.busyo_cd ")
                        .AppendLine("LEFT JOIN m_busyo_kanri a4 ")
                        .AppendLine("ON a5.joui_soiki = a4.busyo_cd ")
                        .AppendLine("LEFT JOIN m_busyo_kanri a3 ")
                        .AppendLine("ON a4.joui_soiki = a3.busyo_cd ")
                        .AppendLine("LEFT JOIN m_busyo_kanri a2 ")
                        .AppendLine("ON a3.joui_soiki = a2.busyo_cd ")
                        .AppendLine("LEFT JOIN m_busyo_kanri a1 ")
                        .AppendLine("ON a2.joui_soiki = a1.busyo_cd) AS TB ")

                        .AppendLine("WHERE   (")
                        'lis6　start
                        If chkBusyoCd Then

                            .AppendLine("  B.busyo_cd = @busyo_cd ")
                            paramList.Add(MakeParam("@busyo_cd", SqlDbType.VarChar, 4, dtParamEigyouInfo(0).BusyoCd))

                        Else
                            .AppendLine("SUBSTRING(cd,1,4)=@busyo_cd2 OR  ")
                            .AppendLine("SUBSTRING(cd,5,4)=@busyo_cd2 OR  ")
                            .AppendLine("SUBSTRING(cd,9,4)=@busyo_cd2 OR  ")
                            .AppendLine("SUBSTRING(cd,13,4)=@busyo_cd2 OR  ")
                            .AppendLine("SUBSTRING(cd,17,4)=@busyo_cd2 OR  ")
                            .AppendLine("SUBSTRING(cd,21,4)=@busyo_cd2   ")
                            paramList.Add(MakeParam("@busyo_cd2", SqlDbType.VarChar, 4, dtParamEigyouInfo(0).BusyoCd))

                        End If

                        'lis6　end
                        .AppendLine(" ) ")
                        .AppendLine(" ) ")


                    Else
                        If dtParamEigyouInfo(0).busyo_cd = "0000" Or dtParamEigyouInfo(0).t_sansyou_busyo_cd = "0000" Then

                        Else
                            .AppendLine(" AND  B.busyo_cd IN ( ")
                            .AppendLine("SELECT busyo_cd FROM  ")
                            .AppendLine("(SELECT a6.busyo_cd, ")
                            .AppendLine("ISNULL(a1.busyo_cd,'0000') ")
                            .AppendLine("+ISNULL(a2.busyo_cd,'0000') ")
                            .AppendLine("+ISNULL(a3.busyo_cd,'0000') ")
                            .AppendLine("+ISNULL(a4.busyo_cd,'0000') ")
                            .AppendLine("+ISNULL(a5.busyo_cd,'0000') ")
                            .AppendLine("+ISNULL(a6.busyo_cd,'0000') AS cd ")
                            .AppendLine("FROM m_busyo_kanri a6 ")
                            .AppendLine("LEFT JOIN m_busyo_kanri a5 ")
                            .AppendLine("ON a6.joui_soiki = a5.busyo_cd ")
                            .AppendLine("LEFT JOIN m_busyo_kanri a4 ")
                            .AppendLine("ON a5.joui_soiki = a4.busyo_cd ")
                            .AppendLine("LEFT JOIN m_busyo_kanri a3 ")
                            .AppendLine("ON a4.joui_soiki = a3.busyo_cd ")
                            .AppendLine("LEFT JOIN m_busyo_kanri a2 ")
                            .AppendLine("ON a3.joui_soiki = a2.busyo_cd ")
                            .AppendLine("LEFT JOIN m_busyo_kanri a1 ")
                            .AppendLine("ON a2.joui_soiki = a1.busyo_cd) AS TB ")

                            .AppendLine("WHERE   (")
                            Dim intCHK As Integer = 0
                            If dtParamEigyouInfo(0).busyo_cd <> "" Then
                                .AppendLine("SUBSTRING(cd,1,4)=@busyo_cd2 OR  ")
                                .AppendLine("SUBSTRING(cd,5,4)=@busyo_cd2 OR  ")
                                .AppendLine("SUBSTRING(cd,9,4)=@busyo_cd2 OR  ")
                                .AppendLine("SUBSTRING(cd,13,4)=@busyo_cd2 OR  ")
                                .AppendLine("SUBSTRING(cd,17,4)=@busyo_cd2 OR  ")
                                .AppendLine("SUBSTRING(cd,21,4)=@busyo_cd2   ")
                                paramList.Add(MakeParam("@busyo_cd2", SqlDbType.VarChar, 4, dtParamEigyouInfo(0).busyo_cd))
                                intCHK = intCHK + 1
                            End If

                            If dtParamEigyouInfo(0).t_sansyou_busyo_cd <> "" Then
                                If intCHK = 1 Then
                                    .AppendLine(" OR ")
                                End If
                                .AppendLine(" SUBSTRING(cd,1,4)=@busyo_cd3 OR  ")
                                .AppendLine("SUBSTRING(cd,5,4)=@busyo_cd3 OR  ")
                                .AppendLine("SUBSTRING(cd,9,4)=@busyo_cd3 OR  ")
                                .AppendLine("SUBSTRING(cd,13,4)=@busyo_cd3 OR  ")
                                .AppendLine("SUBSTRING(cd,17,4)=@busyo_cd3 OR  ")
                                .AppendLine("SUBSTRING(cd,21,4)=@busyo_cd3   ")
                                paramList.Add(MakeParam("@busyo_cd3", SqlDbType.VarChar, 4, dtParamEigyouInfo(0).t_sansyou_busyo_cd))
                            End If
                            .AppendLine(" ) ")
                            .AppendLine(" ) ")
                        End If


                    End If

                End If
            End If
 
            .AppendLine(" ORDER BY  A.kameiten_cd ")

        End With

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsEigyouJyouhou, _
                    dsEigyouJyouhou.eigyouJyouhou.TableName, paramList.ToArray)

        '終了処理
        commandTextSb = Nothing

        Return dsEigyouJyouhou.eigyouJyouhou

    End Function

End Class

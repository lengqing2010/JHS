Imports System.Data.SqlClient
Imports System.Text
Imports Itis.ApplicationBlocks.Data.SQLHelper
Public Class CommonSearchDataAccess
    Private connStr As String = ConnectionManager.Instance.ConnectionString
    ''' <summary>ユーザーデータを取得する</summary>
    Public Function SelUserInfo(ByVal intRows As String, _
                                            ByVal strUserId As String, _
                                            ByVal strUserMei As String, _
                                            ByVal blnDelete As Boolean) As CommonSearchDataSet.BirudaTableDataTable
        ' DataSetインスタンスの生成()
        Dim dsCommonSearch As New CommonSearchDataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        commandTextSb.AppendLine(" SELECT ")
        If intRows <> "max" Then
            commandTextSb.AppendLine(" TOP " & intRows)
        End If

        commandTextSb.AppendLine("      m_jiban_ninsyou.login_user_id AS cd, ")
        commandTextSb.AppendLine("      m_jhs_mailbox.DisplayName AS mei ")
        commandTextSb.AppendLine(" FROM ")
        commandTextSb.AppendLine("      m_jiban_ninsyou  WITH(READCOMMITTED) ")
        commandTextSb.AppendLine("  INNER JOIN  m_jhs_mailbox   WITH(READCOMMITTED)")
        commandTextSb.AppendLine("  ON m_jiban_ninsyou.login_user_id=m_jhs_mailbox.PrimaryWindowsNTAccount")
        commandTextSb.AppendLine(" WHERE m_jiban_ninsyou.login_user_id IS NOT NULL ")
        If blnDelete = True Then
            commandTextSb.Append(" AND m_jiban_ninsyou.torikesi = @torikesi ")
            paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, "0"))
        Else
            commandTextSb.Append("  AND  0 = 0 ")
        End If
        If strUserId.Trim <> "" Then
            commandTextSb.AppendLine(" AND m_jiban_ninsyou.login_user_id LIKE @login_user_id ")
        End If
        If strUserMei.Trim <> "" Then
            commandTextSb.AppendLine(" AND m_jhs_mailbox.DisplayName  LIKE @DisplayName ")
        End If
        commandTextSb.Append(" ORDER BY m_jiban_ninsyou.login_user_id ")
        'パラメータの設定
        paramList.Add(MakeParam("@login_user_id", SqlDbType.VarChar, 31, strUserId & "%"))
        paramList.Add(MakeParam("@DisplayName", SqlDbType.VarChar, 130, "%" & strUserMei & "%"))
        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsCommonSearch, _
                    dsCommonSearch.BirudaTable.TableName, paramList.ToArray)

        Return dsCommonSearch.BirudaTable
    End Function
    ''' <summary>営業データを取得する</summary>
    Public Function SelEigyouInfo(ByVal strUserId As String) As CommonSearchDataSet.BirudaTableDataTable
        ' DataSetインスタンスの生成()
        Dim dsCommonSearch As New CommonSearchDataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine("      m_jiban_ninsyou.login_user_id AS cd, ")
        commandTextSb.AppendLine("      m_jhs_mailbox.DisplayName AS mei ")
        commandTextSb.AppendLine(" FROM ")
        commandTextSb.AppendLine("      m_jiban_ninsyou  WITH(READCOMMITTED) ")
        commandTextSb.AppendLine("  INNER JOIN  m_jhs_mailbox   WITH(READCOMMITTED)")
        commandTextSb.AppendLine("  ON m_jiban_ninsyou.login_user_id=m_jhs_mailbox.PrimaryWindowsNTAccount")
        commandTextSb.AppendLine(" WHERE m_jiban_ninsyou.login_user_id IS NOT NULL ")
        commandTextSb.Append(" AND m_jiban_ninsyou.torikesi = @torikesi ")

        commandTextSb.AppendLine(" AND m_jiban_ninsyou.login_user_id = @login_user_id ")
        
        'パラメータの設定
        paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, "0"))
        paramList.Add(MakeParam("@login_user_id", SqlDbType.VarChar, 64, strUserId))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsCommonSearch, _
                    dsCommonSearch.BirudaTable.TableName, paramList.ToArray)

        Return dsCommonSearch.BirudaTable
    End Function
    '''<summary>ユーザーレコード行数を取得する</summary>
    Public Function SelUserCount(ByVal strUserId As String, _
                                            ByVal strUserMei As String, _
                                            ByVal blnDelete As Boolean) As Integer
        ' DataSetインスタンスの生成()
        Dim dsCommonSearch As New DataSet
        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine("      COUNT(m_jiban_ninsyou.login_user_id) ")

        commandTextSb.AppendLine(" FROM ")
        commandTextSb.AppendLine("      m_jiban_ninsyou  WITH(READCOMMITTED) ")
        commandTextSb.AppendLine("  INNER JOIN  m_jhs_mailbox   WITH(READCOMMITTED)")
        commandTextSb.AppendLine("  ON m_jiban_ninsyou.login_user_id=m_jhs_mailbox.PrimaryWindowsNTAccount")
        commandTextSb.AppendLine(" WHERE m_jiban_ninsyou.login_user_id IS NOT NULL ")
        If blnDelete = True Then
            commandTextSb.Append(" AND m_jiban_ninsyou.torikesi = @torikesi ")
            paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, "0"))
        Else
            commandTextSb.Append("  AND  0 = 0 ")
        End If
        If strUserId.Trim <> "" Then
            commandTextSb.AppendLine(" AND m_jiban_ninsyou.login_user_id LIKE @login_user_id ")
        End If
        If strUserMei.Trim <> "" Then
            commandTextSb.AppendLine(" AND m_jhs_mailbox.DisplayName  LIKE @DisplayName ")
        End If

        'パラメータの設定
        paramList.Add(MakeParam("@login_user_id", SqlDbType.VarChar, 31, strUserId & "%"))
        paramList.Add(MakeParam("@DisplayName", SqlDbType.VarChar, 130, "%" & strUserMei & "%"))
        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsCommonSearch, "dsCommonSearch", paramList.ToArray)

        Return CInt(dsCommonSearch.Tables("dsCommonSearch").Rows(0).Item(0))
    End Function
    '''<summary>郵便番号レコード行数を取得する</summary>
    Public Function SelYuubinCount(ByVal strYuubinNo As String, _
                                        ByVal strYuubinMei As String) As Integer
        ' DataSetインスタンスの生成()
        Dim dsCommonSearch As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine("      COUNT(yuubin_no) ")
        commandTextSb.AppendLine(" FROM ")
        commandTextSb.AppendLine("    m_yuubin WITH(READCOMMITTED) LEFT JOIN m_todoufuken WITH(READCOMMITTED) ")
        commandTextSb.AppendLine("    ON m_yuubin.todoufuken_mei=m_todoufuken.todouhuken_mei ")
        commandTextSb.AppendLine(" WHERE yuubin_no IS NOT NULL ")

        If strYuubinNo.Trim <> "" Then
            commandTextSb.AppendLine(" AND yuubin_no LIKE @strYuubinNo ")
        End If
        If strYuubinMei.Trim <> "" Then
            commandTextSb.AppendLine(" AND isnull(todoufuken_mei,'')+isnull(sikutyouson_mei,'')+isnull(tiiki_mei,'') LIKE @strYuubinMei ")
        End If
        'パラメータの設定
        paramList.Add(MakeParam("@strYuubinNo", SqlDbType.VarChar, 8, strYuubinNo & "%"))
        paramList.Add(MakeParam("@strYuubinMei", SqlDbType.VarChar, 230, "%" & strYuubinMei & "%"))
        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsCommonSearch, "dsCommonSearch", paramList.ToArray)


        Return CInt(dsCommonSearch.Tables("dsCommonSearch").Rows(0).Item(0))
    End Function
    ''' <summary>郵便番号データを取得する</summary>
    Public Function SelYuubinInfo(ByVal intRows As String, _
                                        ByVal strYuubinNo As String, _
                                        ByVal strYuubinMei As String) As CommonSearchDataSet.BirudaTableDataTable
        ' DataSetインスタンスの生成()
        Dim dsCommonSearch As New CommonSearchDataSet
        strYuubinNo = Replace(strYuubinNo.Trim, "-", "")
        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        commandTextSb.AppendLine(" SELECT ")
        If intRows <> "max" Then
            commandTextSb.AppendLine(" TOP " & intRows)
        End If
        commandTextSb.AppendLine("      yuubin_no AS cd, ")
        commandTextSb.AppendLine("      (isnull(todoufuken_mei,'')+isnull(sikutyouson_mei,'')+isnull(tiiki_mei,'')+','+isnull(m_todoufuken.todouhuken_cd,'')) AS mei")
        commandTextSb.AppendLine(" FROM ")
        commandTextSb.AppendLine("    m_yuubin WITH(READCOMMITTED) LEFT JOIN m_todoufuken WITH(READCOMMITTED) ")
        commandTextSb.AppendLine("    ON m_yuubin.todoufuken_mei=m_todoufuken.todouhuken_mei ")

        commandTextSb.AppendLine(" WHERE yuubin_no IS NOT NULL ")

        If Not strYuubinNo Is Nothing Then
            commandTextSb.AppendLine(" AND yuubin_no LIKE @strYuubinNo ")
        End If
        If strYuubinMei.Trim <> "" Then
            commandTextSb.AppendLine(" AND isnull(todoufuken_mei,'')+isnull(sikutyouson_mei,'')+isnull(tiiki_mei,'') LIKE @strYuubinMei ")
        End If
        commandTextSb.Append(" ORDER BY yuubin_no ")
        'パラメータの設定
        paramList.Add(MakeParam("@strYuubinNo", SqlDbType.VarChar, 8, strYuubinNo & "%"))
        paramList.Add(MakeParam("@strYuubinMei", SqlDbType.VarChar, 230, "%" & strYuubinMei & "%"))
        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsCommonSearch, _
                    dsCommonSearch.BirudaTable.TableName, paramList.ToArray)

        Return dsCommonSearch.BirudaTable
    End Function
    '''<summary>仕様レコード行数を取得する</summary>
    Public Function SelSiyouCount(ByVal strSiyouNo As String, _
                                        ByVal strSiyouMei As String) As Integer
        ' DataSetインスタンスの生成()
        Dim dsCommonSearch As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine("      COUNT(ks_siyou_no) ")
        commandTextSb.AppendLine(" FROM ")
        commandTextSb.AppendLine("      m_ks_siyou WITH(READCOMMITTED)  ")
        commandTextSb.AppendLine(" WHERE ks_siyou_no IS NOT NULL ")

        If Not strSiyouNo Is Nothing Then
            commandTextSb.AppendLine(" AND ks_siyou_no LIKE @strSiyouNo ")
        End If
        If strSiyouMei.Trim <> "" Then
            commandTextSb.AppendLine(" AND ks_siyou LIKE @strSiyouMei ")
        End If
        'パラメータの設定
        paramList.Add(MakeParam("@strSiyouNo", SqlDbType.VarChar, 11, strSiyouNo & "%"))
        paramList.Add(MakeParam("@strSiyouMei", SqlDbType.VarChar, 82, "%" & strSiyouMei & "%"))
        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsCommonSearch, "dsCommonSearch", paramList.ToArray)


        Return CInt(dsCommonSearch.Tables("dsCommonSearch").Rows(0).Item(0))
    End Function
    ''' <summary>仕様データを取得する</summary>
    Public Function SelSiyouInfo(ByVal intRows As String, _
                                        ByVal strSiyouNo As String, _
                                        ByVal strSiyouMei As String) As CommonSearchDataSet.IntTableDataTable
        ' DataSetインスタンスの生成()
        Dim dsCommonSearch As New CommonSearchDataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        commandTextSb.AppendLine(" SELECT ")
        If intRows <> "max" Then
            commandTextSb.AppendLine(" TOP " & intRows)
        End If
        commandTextSb.AppendLine("      ks_siyou_no AS cd, ")
        commandTextSb.AppendLine("      ks_siyou AS mei")
        commandTextSb.AppendLine(" FROM ")
        commandTextSb.AppendLine("      m_ks_siyou WITH(READCOMMITTED)  ")
        commandTextSb.AppendLine(" WHERE ks_siyou_no IS NOT NULL ")

        If Not strSiyouNo Is Nothing Then
            commandTextSb.AppendLine(" AND ks_siyou_no LIKE @strSiyouNo ")
        End If
        If strSiyouMei.Trim <> "" Then
            commandTextSb.AppendLine(" AND ks_siyou LIKE @strSiyouMei ")
        End If
        commandTextSb.Append(" ORDER BY ks_siyou_no ")
        'パラメータの設定
        paramList.Add(MakeParam("@strSiyouNo", SqlDbType.VarChar, 11, strSiyouNo & "%"))
        paramList.Add(MakeParam("@strSiyouMei", SqlDbType.VarChar, 82, "%" & strSiyouMei & "%"))
        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsCommonSearch, _
                    dsCommonSearch.IntTable.TableName, paramList.ToArray)

        Return dsCommonSearch.IntTable
    End Function
    ''' <summary>工事会社データを取得する</summary>
    Public Function SelKojKaisyaKensakuInfo(ByVal intRows As String, _
                                        ByVal strCd As String, _
                                        ByVal strMei As String, _
                                        ByVal blnDelete As Boolean) As DataTable
        ' DataSetインスタンスの生成()
        Dim dsCommonSearch As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        commandTextSb.AppendLine(" SELECT ")
        If intRows <> "max" Then
            commandTextSb.AppendLine("      TOP " & intRows)
        End If

        commandTextSb.AppendLine("      tys_kaisya_cd+jigyousyo_cd AS cd, ")
        commandTextSb.AppendLine("      tys_kaisya_mei, ")
        commandTextSb.AppendLine("      tys_kaisya_mei_kana ")
        commandTextSb.AppendLine(" FROM ")
        commandTextSb.AppendLine("      m_tyousakaisya  WITH(READCOMMITTED)  ")
        commandTextSb.AppendLine(" WHERE tys_kaisya_cd IS NOT NULL ")
        If blnDelete = True Then
            commandTextSb.Append(" AND torikesi = @torikesi ")
            paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, "0"))
        Else
            commandTextSb.Append("  AND  0 = 0 ")
        End If
        If strCd.Trim <> "" Then
            commandTextSb.AppendLine(" AND tys_kaisya_cd+jigyousyo_cd LIKE @Cd ")
        End If
        If strMei.Trim <> "" Then
            commandTextSb.AppendLine(" AND tys_kaisya_mei_kana LIKE @Mei ")
        End If
        commandTextSb.Append(" ORDER BY tys_kaisya_cd,jigyousyo_cd ")
        'パラメータの設定
        paramList.Add(MakeParam("@Cd", SqlDbType.VarChar, 8, strCd & "%"))
        paramList.Add(MakeParam("@Mei", SqlDbType.VarChar, 22, "%" & strMei & "%"))
        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsCommonSearch, _
                    "dsCommonSearch", paramList.ToArray)

        Return dsCommonSearch.Tables("dsCommonSearch")
    End Function
    ''' <summary>工事会社レコード行数を取得する</summary>
    Public Function SelKojKaisyaKensakuCount(ByVal strCd As String, _
                                        ByVal strMei As String, _
                                        ByVal blnDelete As Boolean) As Integer
        ' DataSetインスタンスの生成()
        Dim dsCommonSearch As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine("   COUNT(tys_kaisya_cd) ")
        commandTextSb.AppendLine(" FROM ")
        commandTextSb.AppendLine("      m_tyousakaisya  WITH(READCOMMITTED)  ")
        commandTextSb.AppendLine(" WHERE tys_kaisya_cd IS NOT NULL ")
        If blnDelete = True Then
            commandTextSb.Append(" AND torikesi = @torikesi ")
            paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, "0"))
        Else
            commandTextSb.Append("  AND  0 = 0 ")
        End If
        If strCd.Trim <> "" Then
            commandTextSb.AppendLine(" AND tys_kaisya_cd+jigyousyo_cd LIKE @Cd ")
        End If
        If strMei.Trim <> "" Then
            commandTextSb.AppendLine(" AND tys_kaisya_mei_kana LIKE @Mei ")
        End If
        'パラメータの設定
        paramList.Add(MakeParam("@Cd", SqlDbType.VarChar, 8, strCd & "%"))
        paramList.Add(MakeParam("@Mei", SqlDbType.VarChar, 22, "%" & strMei & "%"))
        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsCommonSearch, "dsCommonSearch", paramList.ToArray)

        Return CInt(dsCommonSearch.Tables("dsCommonSearch").Rows(0).Item(0))
    End Function

    ''' <summary>営業所データを取得する</summary>
    Public Function SelEigyousyoInfo(ByVal intRows As String, _
                                        ByVal strEigyousyoCd As String, _
                                        ByVal strEigyousyoMei As String, _
                                        ByVal blnDelete As Boolean) As CommonSearchDataSet.EigyousyoTableDataTable
        ' DataSetインスタンスの生成()
        Dim dsCommonSearch As New CommonSearchDataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        commandTextSb.AppendLine(" SELECT ")
        If intRows <> "max" Then
            commandTextSb.AppendLine("      TOP " & intRows)
        End If

        commandTextSb.AppendLine("      eigyousyo_cd, ")
        commandTextSb.AppendLine("      eigyousyo_mei ")
        commandTextSb.AppendLine(" FROM ")
        commandTextSb.AppendLine("      m_eigyousyo  WITH(READCOMMITTED)  ")
        commandTextSb.AppendLine(" WHERE eigyousyo_cd IS NOT NULL ")
        If blnDelete = True Then
            commandTextSb.Append(" AND torikesi = @torikesi ")
            paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, "0"))
        Else
            commandTextSb.Append("  AND  0 = 0 ")
        End If
        If strEigyousyoCd.Trim <> "" Then
            commandTextSb.AppendLine(" AND eigyousyo_cd LIKE @EigyousyoCd ")
        End If
        If strEigyousyoMei.Trim <> "" Then
            commandTextSb.AppendLine(" AND eigyousyo_mei LIKE @EigyousyoMei ")
        End If
        commandTextSb.Append(" ORDER BY eigyousyo_cd ")
        'パラメータの設定
        paramList.Add(MakeParam("@EigyousyoCd", SqlDbType.VarChar, 6, strEigyousyoCd & "%"))
        paramList.Add(MakeParam("@EigyousyoMei", SqlDbType.VarChar, 42, "%" & strEigyousyoMei & "%"))
        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsCommonSearch, _
                    dsCommonSearch.EigyousyoTable.TableName, paramList.ToArray)

        Return dsCommonSearch.EigyousyoTable
    End Function
    '''<summary>営業所レコード行数を取得する</summary>
    Public Function SelEigyousyoCount(ByVal strEigyousyoCd As String, _
                                        ByVal strEigyousyoMei As String, _
                                        ByVal blnDelete As Boolean) As Integer
        ' DataSetインスタンスの生成()
        Dim dsCommonSearch As New DataSet
        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine("      COUNT(eigyousyo_cd) ")
        commandTextSb.AppendLine(" FROM ")
        commandTextSb.AppendLine("      m_eigyousyo  WITH(READCOMMITTED)  ")
        commandTextSb.AppendLine(" WHERE eigyousyo_cd IS NOT NULL ")
        If blnDelete = True Then
            commandTextSb.Append(" AND torikesi = @torikesi ")
            paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, "0"))
        Else
            commandTextSb.Append("  AND  0 = 0 ")
        End If
        If strEigyousyoCd.Trim <> "" Then
            commandTextSb.AppendLine(" AND eigyousyo_cd LIKE @EigyousyoCd ")
        End If
        If strEigyousyoMei.Trim <> "" Then
            commandTextSb.AppendLine(" AND eigyousyo_mei LIKE @EigyousyoMei ")
        End If

        'パラメータの設定
        paramList.Add(MakeParam("@EigyousyoCd", SqlDbType.VarChar, 6, strEigyousyoCd & "%"))
        paramList.Add(MakeParam("@EigyousyoMei", SqlDbType.VarChar, 42, "%" & strEigyousyoMei & "%"))
        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsCommonSearch, "dsCommonSearch", paramList.ToArray)

        Return CInt(dsCommonSearch.Tables("dsCommonSearch").Rows(0).Item(0))
    End Function
    ''' <summary>加盟店データを取得する</summary>
    Public Function SelKameitenKensakuInfo(ByVal intRows As String, _
                                      ByVal strKubun As String, _
                                      ByVal strKameitenCd As String, _
                                      ByVal strKameitenKana As String, _
                                      ByVal blnDelete As Boolean) As CommonSearchDataSet.KameitenSearchTableDataTable
        ' DataSetインスタンスの生成()
        Dim dsCommonSearch As New CommonSearchDataSet

        Dim commandTextSb As New StringBuilder()
        Dim kbnCount As Integer = 1
        Dim tmpKbn1 As String = ""
        Dim tmpKbn2 As String = ""
        Dim tmpKbn3 As String = ""
        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        commandTextSb.AppendLine(" SELECT ")
        If intRows <> "max" Then
            commandTextSb.AppendLine(" TOP " & intRows)
        End If
        commandTextSb.Append("  k.kameiten_cd, k.kameiten_mei1, t.todouhuken_mei, k.tenmei_kana1")
        '============2012/03/29 車龍 405721案件の対応 追加↓=========================
        commandTextSb.Append("  ,k.torikesi ")
        commandTextSb.Append("  ,ISNULL(MKM.meisyou,'') AS torikesi_txt ")
        '============2012/03/29 車龍 405721案件の対応 追加↑=========================
        commandTextSb.Append("  FROM m_kameiten  k  WITH(READCOMMITTED)")
        commandTextSb.Append("  LEFT OUTER JOIN m_todoufuken  t  WITH(READCOMMITTED) ON t.todouhuken_cd = k.todouhuken_cd ")
        '============2012/03/29 車龍 405721案件の対応 追加↓=========================
        commandTextSb.Append("  LEFT OUTER JOIN m_kakutyou_meisyou  MKM  WITH(READCOMMITTED) ON MKM.meisyou_syubetu = 56 AND k.torikesi = MKM.code ")
        '============2012/03/29 車龍 405721案件の対応 追加↑=========================
        commandTextSb.Append("  WHERE ")
        If blnDelete = True Then
            commandTextSb.Append(" k.torikesi = @torikesi ")
            paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, "0"))
        Else
            commandTextSb.Append(" 0 = 0 ")
        End If
        If strKubun <> "" Then
            commandTextSb.Append(" AND k.kbn IN ( ")
            Dim arrKbn As Array = strKubun.Split(",")
            For Each tmpKbn As String In arrKbn
                If kbnCount = 1 And arrKbn(0).ToString.Trim <> "" Then
                    commandTextSb.AppendLine(" @Kbn0 ")
                    paramList.Add(MakeParam("@Kbn0", SqlDbType.Char, 1, arrKbn(0)))
                End If
                If kbnCount = 2 Then
                    commandTextSb.AppendLine(" ,@Kbn1 ")
                    paramList.Add(MakeParam("@Kbn1", SqlDbType.Char, 1, arrKbn(1)))
                End If
                If kbnCount = 3 Then
                    commandTextSb.AppendLine(" ,@Kbn2 ")
                    paramList.Add(MakeParam("@Kbn2", SqlDbType.Char, 1, arrKbn(2)))
                End If
                kbnCount += 1
            Next
            commandTextSb.Append(" ) ")
        End If
        If strKameitenCd <> "" Then
            commandTextSb.Append(" AND k.kameiten_cd like @kameiten_cd")
            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 6, strKameitenCd & "%"))
        End If
        If strKameitenKana <> "" Then
            commandTextSb.Append(" AND (k.tenmei_kana1 Like @tenmei_kana1 ")
            commandTextSb.Append(" OR k.tenmei_kana2 Like @tenmei_kana2 )")
            paramList.Add(MakeParam("@tenmei_kana1", SqlDbType.VarChar, 22, "%" & strKameitenKana & "%"))
            paramList.Add(MakeParam("@tenmei_kana2", SqlDbType.VarChar, 22, "%" & strKameitenKana & "%"))
        End If
        If strKubun = "E" Then
            commandTextSb.Append(" ORDER BY k.kameiten_cd ")
        Else
            commandTextSb.Append(" ORDER BY k.tenmei_kana1 ")
        End If

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsCommonSearch, _
                    dsCommonSearch.KameitenSearchTable.TableName, paramList.ToArray)


        Return dsCommonSearch.KameitenSearchTable
    End Function
    '''<summary>加盟店レコード行数を取得する</summary>
    Public Function SelKameitenKensakuCount(ByVal strKubun As String, _
                                      ByVal strKameitenCd As String, _
                                      ByVal strKameitenKana As String, _
                                      ByVal blnDelete As Boolean) As Integer
        ' DataSetインスタンスの生成()
        Dim dsCommonSearch As New DataSet

        Dim commandTextSb As New StringBuilder()
        Dim kbnCount As Integer = 1
        Dim tmpKbn1 As String = ""
        Dim tmpKbn2 As String = ""
        Dim tmpKbn3 As String = ""
        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        commandTextSb.Append("SELECT COUNT(k.kameiten_cd) ")
        commandTextSb.Append("  FROM m_kameiten k WITH(READCOMMITTED) ")
        commandTextSb.Append("  LEFT OUTER JOIN m_todoufuken t WITH(READCOMMITTED) ON t.todouhuken_cd = k.todouhuken_cd ")
        commandTextSb.Append("  WHERE ")
        If blnDelete = True Then
            commandTextSb.Append(" k.torikesi = @torikesi ")
            paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, "0"))
        Else
            commandTextSb.Append(" 0 = 0 ")
        End If
        If strKubun <> "" Then
            commandTextSb.Append(" AND k.kbn IN ( ")
            Dim arrKbn As Array = strKubun.Split(",")
            For Each tmpKbn As String In arrKbn
                If kbnCount = 1 And arrKbn(0).ToString.Trim <> "" Then
                    commandTextSb.AppendLine(" @Kbn0 ")
                    paramList.Add(MakeParam("@Kbn0", SqlDbType.Char, 1, arrKbn(0)))
                End If
                If kbnCount = 2 Then
                    commandTextSb.AppendLine(" ,@Kbn1 ")
                    paramList.Add(MakeParam("@Kbn1", SqlDbType.Char, 1, arrKbn(1)))
                End If
                If kbnCount = 3 Then
                    commandTextSb.AppendLine(" ,@Kbn2 ")
                    paramList.Add(MakeParam("@Kbn2", SqlDbType.Char, 1, arrKbn(2)))
                End If
                kbnCount += 1
            Next
            commandTextSb.Append(" ) ")
        End If
        If strKameitenCd <> "" Then
            commandTextSb.Append(" AND k.kameiten_cd like @kameiten_cd")
            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 6, strKameitenCd & "%"))
        End If
        If strKameitenKana <> "" Then
            commandTextSb.Append(" AND (k.tenmei_kana1 Like @tenmei_kana1 ")
            commandTextSb.Append(" OR k.tenmei_kana2 Like @tenmei_kana2 )")
            paramList.Add(MakeParam("@tenmei_kana1", SqlDbType.VarChar, 22, "%" & strKameitenKana & "%"))
            paramList.Add(MakeParam("@tenmei_kana2", SqlDbType.VarChar, 22, "%" & strKameitenKana & "%"))
        End If


        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsCommonSearch, _
                    "dsCommonSearch", paramList.ToArray)


        Return CInt(dsCommonSearch.Tables("dsCommonSearch").Rows(0).Item(0))
    End Function
    ''' <summary>系列データを取得する</summary>
    Public Function SelKeiretuKensakuInfo(ByVal intRows As String, _
                                      ByVal strKubun As String, _
                                      ByVal strKeiretuCd As String, _
                                      ByVal strKeiretuMei As String, _
                                      ByVal blnDelete As Boolean) As CommonSearchDataSet.KeiretuTableDataTable
        ' DataSetインスタンスの生成()
        Dim dsCommonSearch As New CommonSearchDataSet

        Dim commandTextSb As New StringBuilder()
        Dim kbnCount As Integer = 1
        Dim tmpKbn1 As String = ""
        Dim tmpKbn2 As String = ""
        Dim tmpKbn3 As String = ""
        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        commandTextSb.AppendLine(" SELECT ")
        If intRows <> "max" Then
            commandTextSb.AppendLine(" TOP " & intRows)
        End If
        commandTextSb.Append("  kbn,")
        commandTextSb.Append("  keiretu_cd,keiretu_mei")
        commandTextSb.Append("  FROM m_keiretu  WITH(READCOMMITTED) ")
        commandTextSb.Append("  WHERE ")
        If blnDelete = True Then
            commandTextSb.Append(" torikesi = @torikesi ")
            paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, "0"))
        Else
            commandTextSb.Append(" 0 = 0 ")
        End If
        If strKubun <> "" Then
            commandTextSb.Append(" AND kbn IN ( ")
            Dim arrKbn As Array = strKubun.Split(",")
            For Each tmpKbn As String In arrKbn
                If kbnCount = 1 And arrKbn(0).ToString.Trim <> "" Then
                    commandTextSb.AppendLine(" @Kbn0 ")
                    paramList.Add(MakeParam("@Kbn0", SqlDbType.Char, 1, arrKbn(0)))
                End If
                If kbnCount = 2 Then
                    commandTextSb.AppendLine(" ,@Kbn1 ")
                    paramList.Add(MakeParam("@Kbn1", SqlDbType.Char, 1, arrKbn(1)))
                End If
                If kbnCount = 3 Then
                    commandTextSb.AppendLine(" ,@Kbn2 ")
                    paramList.Add(MakeParam("@Kbn2", SqlDbType.Char, 1, arrKbn(2)))
                End If
                kbnCount += 1
            Next
            commandTextSb.Append(" ) ")
        End If
        If strKeiretuCd <> "" Then
            commandTextSb.Append(" AND keiretu_cd LIKE @keiretu_cd")
            paramList.Add(MakeParam("@keiretu_cd", SqlDbType.VarChar, 6, strKeiretuCd & "%"))
        End If
        If strKeiretuMei <> "" Then
            commandTextSb.Append(" AND keiretu_mei Like @keiretu_mei ")
            paramList.Add(MakeParam("@keiretu_mei", SqlDbType.VarChar, 42, "%" & strKeiretuMei & "%"))
        End If
        commandTextSb.Append(" ORDER BY keiretu_cd ")


        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsCommonSearch, _
                    dsCommonSearch.KeiretuTable.TableName, paramList.ToArray)


        Return dsCommonSearch.KeiretuTable
    End Function
    '''<summary>系列レコード行数を取得する</summary>
    Public Function SelKeiretuKensakuCount(ByVal strKubun As String, _
                                      ByVal strKeiretuCd As String, _
                                      ByVal strKeiretuMei As String, _
                                      ByVal blnDelete As Boolean) As Integer
        ' DataSetインスタンスの生成()
        Dim dsCommonSearch As New DataSet

        Dim commandTextSb As New StringBuilder()
        Dim kbnCount As Integer = 1
        Dim tmpKbn1 As String = ""
        Dim tmpKbn2 As String = ""
        Dim tmpKbn3 As String = ""
        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.Append("  COUNT(keiretu_cd) ")
        commandTextSb.Append("  FROM m_keiretu  WITH(READCOMMITTED) ")
        commandTextSb.Append("  WHERE ")
        If blnDelete = True Then
            commandTextSb.Append(" torikesi = @torikesi ")
            paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, "0"))
        Else
            commandTextSb.Append(" 0 = 0 ")
        End If
        If strKubun <> "" Then
            commandTextSb.Append(" AND kbn IN ( ")
            Dim arrKbn As Array = strKubun.Split(",")
            For Each tmpKbn As String In arrKbn
                If kbnCount = 1 And arrKbn(0).ToString.Trim <> "" Then
                    commandTextSb.AppendLine(" @Kbn0 ")
                    paramList.Add(MakeParam("@Kbn0", SqlDbType.Char, 1, arrKbn(0)))
                End If
                If kbnCount = 2 Then
                    commandTextSb.AppendLine(" ,@Kbn1 ")
                    paramList.Add(MakeParam("@Kbn1", SqlDbType.Char, 1, arrKbn(1)))
                End If
                If kbnCount = 3 Then
                    commandTextSb.AppendLine(" ,@Kbn2 ")
                    paramList.Add(MakeParam("@Kbn2", SqlDbType.Char, 1, arrKbn(2)))
                End If
                kbnCount += 1
            Next
            commandTextSb.Append(" ) ")
        End If
        If strKeiretuCd <> "" Then
            commandTextSb.Append(" AND keiretu_cd LIKE @keiretu_cd")
            paramList.Add(MakeParam("@keiretu_cd", SqlDbType.VarChar, 6, strKeiretuCd & "%"))
        End If
        If strKeiretuMei <> "" Then
            commandTextSb.Append(" AND keiretu_mei Like @keiretu_mei ")
            paramList.Add(MakeParam("@keiretu_mei", SqlDbType.VarChar, 42, "%" & strKeiretuMei & "%"))
        End If

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsCommonSearch, _
                    "dsCommonSearch", paramList.ToArray)

        Return CInt(dsCommonSearch.Tables("dsCommonSearch").Rows(0).Item(0))
    End Function
    ''' <summary>商品データを取得する</summary>
    Public Function SelSyouhinInfo(ByVal intRows As String, _
                                         ByVal strSyouhinCd As String, _
                                         ByVal strSyouhinMei As String, _
                                         ByVal soukoCd As String, _
                                         Optional ByVal blnDelete As String = "") As CommonSearchDataSet.SyouhinTableDataTable
        ' DataSetインスタンスの生成()
        Dim dsCommonSearch As New CommonSearchDataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        Dim intCnt As Integer = 0
        'SQL文
        commandTextSb.AppendLine(" SELECT ")
        If intRows <> "max" Then
            commandTextSb.AppendLine(" TOP " & intRows)
        End If
        commandTextSb.AppendLine("      syouhin.syouhin_cd, ")
        commandTextSb.AppendLine("      syouhin.syouhin_mei ")
        commandTextSb.AppendLine(" FROM ")
        commandTextSb.AppendLine("		(SELECT syouhin_cd , ")
        commandTextSb.AppendLine("			syouhin_mei  ")
        commandTextSb.AppendLine("		FROM ")
        commandTextSb.AppendLine("			m_syouhin WITH(READCOMMITTED)")
        commandTextSb.AppendLine("	WHERE	syouhin_cd IS NOT NULL")
        If soukoCd <> "#" Then
            commandTextSb.AppendLine("	AND (")
            For intCnt = 0 To Split(soukoCd, ",").Length - 1
                If intCnt = 0 Then
                    commandTextSb.AppendLine("	souko_cd=@souko_cd" & intCnt)
                    paramList.Add(MakeParam("@souko_cd" & intCnt, SqlDbType.VarChar, 3, Split(soukoCd, ",")(0)))
                Else
                    commandTextSb.AppendLine(" OR	souko_cd=@souko_cd" & intCnt)
                    paramList.Add(MakeParam("@souko_cd" & intCnt, SqlDbType.VarChar, 3, Split(soukoCd, ",")(intCnt)))

                End If
            Next
            commandTextSb.AppendLine("	)")
        End If
        If blnDelete = "True" Then
            commandTextSb.Append(" AND torikesi = @torikesi ")
            paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, "0"))
        Else
            commandTextSb.Append(" AND 0 = 0 ")
        End If

        If soukoCd = "115" Then
            commandTextSb.AppendLine("		UNION ALL ")
            commandTextSb.AppendLine("		SELECT DISTINCT '00000' AS syouhin_cd , ")
            commandTextSb.AppendLine("			'自動設定なし' AS syouhin_mei  ")
            commandTextSb.AppendLine("		FROM ")
            commandTextSb.AppendLine("		m_syouhin WITH(READCOMMITTED)")
        End If
        commandTextSb.AppendLine("		) AS syouhin")
        commandTextSb.AppendLine(" WHERE syouhin.syouhin_cd IS NOT NULL ")
        

        If strSyouhinCd.Trim <> "" Then
            commandTextSb.AppendLine(" AND syouhin.syouhin_cd LIKE @SyouhinCd ")
        End If
        If strSyouhinMei.Trim <> "" Then
            commandTextSb.AppendLine(" AND syouhin.syouhin_mei LIKE @SyouhinMei ")
        End If

        commandTextSb.Append(" ORDER BY syouhin.syouhin_cd ")
        'パラメータの設定
        paramList.Add(MakeParam("@SyouhinCd", SqlDbType.VarChar, 9, strSyouhinCd & "%"))
        paramList.Add(MakeParam("@SyouhinMei", SqlDbType.VarChar, 42, "%" & strSyouhinMei & "%"))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsCommonSearch, _
                    dsCommonSearch.SyouhinTable.TableName, paramList.ToArray)

        Return dsCommonSearch.SyouhinTable
    End Function
    '''<summary>商品レコード行数を取得する</summary>
    Public Function SelSyouhinCount(ByVal strSyouhinCd As String, _
                                       ByVal strSyouhinMei As String, _
                                       ByVal soukoCd As String, _
                                       Optional ByVal blnDelete As String = "") As Integer
        ' DataSetインスタンスの生成()
        Dim dsCommonSearch As New DataSet
        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        Dim intCnt As Integer = 0
        'SQL文
        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine("      COUNT(syouhin.syouhin_cd) ")
        commandTextSb.AppendLine(" FROM ")
        commandTextSb.AppendLine("		(SELECT syouhin_cd , ")
        commandTextSb.AppendLine("			syouhin_mei  ")
        commandTextSb.AppendLine("		FROM ")
        commandTextSb.AppendLine("			m_syouhin WITH(READCOMMITTED)")
        commandTextSb.AppendLine("	WHERE	syouhin_cd IS NOT NULL")
        If soukoCd <> "#" Then
            commandTextSb.AppendLine(" AND	( ")
            For intCnt = 0 To Split(soukoCd, ",").Length - 1
                If intCnt = 0 Then
                    commandTextSb.AppendLine("	souko_cd=@souko_cd" & intCnt)
                    paramList.Add(MakeParam("@souko_cd" & intCnt, SqlDbType.VarChar, 3, Split(soukoCd, ",")(0)))
                Else
                    commandTextSb.AppendLine(" OR	souko_cd=@souko_cd" & intCnt)
                    paramList.Add(MakeParam("@souko_cd" & intCnt, SqlDbType.VarChar, 3, Split(soukoCd, ",")(intCnt)))

                End If
            Next
            commandTextSb.AppendLine("	) ")
        End If
        If blnDelete = "True" Then
            commandTextSb.Append(" AND torikesi = @torikesi ")
            paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, "0"))
        Else
            commandTextSb.Append(" AND 0 = 0 ")
        End If
        If soukoCd = "115" Then
            commandTextSb.AppendLine("		UNION ALL ")
            commandTextSb.AppendLine("		SELECT DISTINCT '00000' AS syouhin_cd , ")
            commandTextSb.AppendLine("			'自動設定なし' AS syouhin_mei  ")
            commandTextSb.AppendLine("		FROM ")
            commandTextSb.AppendLine("		m_syouhin WITH(READCOMMITTED)")
        End If
        commandTextSb.AppendLine("		) AS syouhin")

        commandTextSb.AppendLine(" WHERE syouhin.syouhin_cd IS NOT NULL ")
    
        If strSyouhinCd.Trim <> "" Then
            commandTextSb.AppendLine(" AND syouhin_cd LIKE @SyouhinCd ")
        End If
        If strSyouhinMei.Trim <> "" Then
            commandTextSb.AppendLine(" AND syouhin_mei LIKE @SyouhinMei ")
        End If

        'パラメータの設定
        paramList.Add(MakeParam("@SyouhinCd", SqlDbType.VarChar, 9, strSyouhinCd & "%"))
        paramList.Add(MakeParam("@SyouhinMei", SqlDbType.VarChar, 42, "%" & strSyouhinMei & "%"))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsCommonSearch, "dsCommonSearch", paramList.ToArray)

        Return CInt(dsCommonSearch.Tables("dsCommonSearch").Rows(0).Item(0))
    End Function

    '==================2011/05/11 車龍 多棟割引情報表示変更 追加 開始↓==========================
    ''' <summary>商品+標準化価格データを取得する</summary>
    Public Function SelSyouhinKakakuInfo(ByVal intRows As String, _
                                         ByVal strSyouhinCd As String, _
                                         ByVal strSyouhinMei As String, _
                                         ByVal soukoCd As String, _
                                         Optional ByVal blnDelete As String = "") As Data.DataTable
        ' DataSetインスタンスの生成()
        Dim dsCommonSearch As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        Dim intCnt As Integer = 0
        'SQL文
        commandTextSb.AppendLine(" SELECT ")
        If intRows <> "max" Then
            commandTextSb.AppendLine(" TOP " & intRows)
        End If
        commandTextSb.AppendLine("      syouhin.syouhin_cd, ")
        commandTextSb.AppendLine("      syouhin.syouhin_mei, ")
        commandTextSb.AppendLine("      syouhin.hyoujun_kkk ")
        commandTextSb.AppendLine(" FROM ")
        commandTextSb.AppendLine("		(SELECT syouhin_cd , ")
        commandTextSb.AppendLine("			syouhin_mei,  ")
        commandTextSb.AppendLine("          ISNULL(CONVERT(VARCHAR(10),hyoujun_kkk),'') AS hyoujun_kkk ")
        commandTextSb.AppendLine("		FROM ")
        commandTextSb.AppendLine("			m_syouhin WITH(READCOMMITTED)")
        commandTextSb.AppendLine("	WHERE	syouhin_cd IS NOT NULL")
        If soukoCd <> "#" Then
            commandTextSb.AppendLine("	AND (")
            For intCnt = 0 To Split(soukoCd, ",").Length - 1
                If intCnt = 0 Then
                    commandTextSb.AppendLine("	souko_cd=@souko_cd" & intCnt)
                    paramList.Add(MakeParam("@souko_cd" & intCnt, SqlDbType.VarChar, 3, Split(soukoCd, ",")(0)))
                Else
                    commandTextSb.AppendLine(" OR	souko_cd=@souko_cd" & intCnt)
                    paramList.Add(MakeParam("@souko_cd" & intCnt, SqlDbType.VarChar, 3, Split(soukoCd, ",")(intCnt)))

                End If
            Next
            commandTextSb.AppendLine("	)")
        End If
        If blnDelete = "True" Then
            commandTextSb.Append(" AND torikesi = @torikesi ")
            paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, "0"))
        Else
            commandTextSb.Append(" AND 0 = 0 ")
        End If

        If soukoCd = "115" Then
            commandTextSb.AppendLine("		UNION ALL ")
            commandTextSb.AppendLine("		SELECT DISTINCT '00000' AS syouhin_cd , ")
            commandTextSb.AppendLine("			'自動設定なし' AS syouhin_mei , ")
            commandTextSb.AppendLine("          '' AS hyoujun_kkk ")
            commandTextSb.AppendLine("		FROM ")
            commandTextSb.AppendLine("		m_syouhin WITH(READCOMMITTED)")
        End If
        commandTextSb.AppendLine("		) AS syouhin")
        commandTextSb.AppendLine(" WHERE syouhin.syouhin_cd IS NOT NULL ")


        If strSyouhinCd.Trim <> "" Then
            commandTextSb.AppendLine(" AND syouhin.syouhin_cd LIKE @SyouhinCd ")
        End If
        If strSyouhinMei.Trim <> "" Then
            commandTextSb.AppendLine(" AND syouhin.syouhin_mei LIKE @SyouhinMei ")
        End If

        commandTextSb.Append(" ORDER BY syouhin.syouhin_cd ")
        'パラメータの設定
        paramList.Add(MakeParam("@SyouhinCd", SqlDbType.VarChar, 9, strSyouhinCd & "%"))
        paramList.Add(MakeParam("@SyouhinMei", SqlDbType.VarChar, 42, "%" & strSyouhinMei & "%"))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsCommonSearch, _
                    "dtSyouhin", paramList.ToArray)

        Return dsCommonSearch.Tables(0)
    End Function
    '==================2011/05/11 車龍 多棟割引情報表示変更 追加 終了↑==========================

    ''' <summary>ビルダーデータを取得する</summary>
    Public Function SelBirudaInfo(ByVal intRows As String, _
                                        ByVal strBirudaCd As String, _
                                        ByVal strBirudaMei As String, _
                                        ByVal blnDelete As Boolean) As CommonSearchDataSet.BirudaTableDataTable
        ' DataSetインスタンスの生成()
        Dim dsCommonSearch As New CommonSearchDataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        commandTextSb.AppendLine(" SELECT ")
        If intRows <> "max" Then
            commandTextSb.AppendLine(" TOP " & intRows)
        End If
        commandTextSb.AppendLine("      kameiten_cd AS cd, ")
        commandTextSb.AppendLine("      kameiten_mei1 AS mei ")
        commandTextSb.AppendLine(" FROM ")
        commandTextSb.AppendLine("      m_kameiten WITH(READCOMMITTED) ")
        commandTextSb.AppendLine(" WHERE kameiten_cd IS NOT NULL ")
        If blnDelete = True Then
            commandTextSb.Append(" AND torikesi = @torikesi ")
            paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, "0"))
        Else
            commandTextSb.Append("  AND  0 = 0 ")
        End If
        If strBirudaCd.Trim <> "" Then
            commandTextSb.AppendLine(" AND kameiten_cd LIKE @kameiten_cd ")
        End If
        If strBirudaMei.Trim <> "" Then
            commandTextSb.AppendLine(" AND kameiten_mei1 LIKE @kameiten_mei1 ")
        End If
        commandTextSb.Append(" ORDER BY kameiten_cd ")
        'パラメータの設定
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 6, strBirudaCd & "%"))
        paramList.Add(MakeParam("@kameiten_mei1", SqlDbType.VarChar, 42, "%" & strBirudaMei & "%"))
        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsCommonSearch, _
                    dsCommonSearch.BirudaTable.TableName, paramList.ToArray)

        Return dsCommonSearch.BirudaTable
    End Function
    '''<summary>ビルダーレコード行数を取得する</summary>
    Public Function SelBirudaCount(ByVal strBirudaCd As String, _
                                        ByVal strBirudaMei As String, _
                                        ByVal blnDelete As Boolean) As Integer
        ' DataSetインスタンスの生成()
        Dim dsCommonSearch As New DataSet
        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine("      COUNT(kameiten_cd) ")
        commandTextSb.AppendLine(" FROM ")
        commandTextSb.AppendLine("      m_kameiten  WITH(READCOMMITTED)  ")
        commandTextSb.AppendLine(" WHERE kameiten_cd IS NOT NULL ")
        If blnDelete = True Then
            commandTextSb.Append(" AND torikesi = @torikesi ")
            paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, "0"))
        Else
            commandTextSb.Append("  AND  0 = 0 ")
        End If
        If strBirudaCd.Trim <> "" Then
            commandTextSb.AppendLine(" AND kameiten_cd LIKE @kameiten_cd ")
        End If
        If strBirudaMei.Trim <> "" Then
            commandTextSb.AppendLine(" AND kameiten_mei1 LIKE @kameiten_mei1 ")
        End If

        'パラメータの設定
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 6, strBirudaCd & "%"))
        paramList.Add(MakeParam("@kameiten_mei1", SqlDbType.VarChar, 42, "%" & strBirudaMei & "%"))
        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsCommonSearch, "dsCommonSearch", paramList.ToArray)

        Return CInt(dsCommonSearch.Tables("dsCommonSearch").Rows(0).Item(0))
    End Function
    ''' <summary>排他データを取得する</summary>
    Public Function SelHaita(ByVal strKameitenCd As String, ByVal strTableName As String) As String
        ' DataSetインスタンスの生成()
        Dim dsCommonSearch As New DataSet
        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine(" ISNULL(upd_login_user_id,''), ")
        commandTextSb.AppendLine(" ISNULL(upd_datetime,'') ")
        commandTextSb.AppendLine(" FROM ")
        commandTextSb.AppendLine(strTableName)
        commandTextSb.AppendLine("  WITH(READCOMMITTED) ")
        commandTextSb.AppendLine(" WHERE kameiten_cd = @kameiten_cd ")
        'パラメータの設定
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strKameitenCd))
        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsCommonSearch, _
                    "dtCommonSearch", paramList.ToArray)

        If dsCommonSearch.Tables(0).Rows.Count <> 0 Then
            Return dsCommonSearch.Tables(0).Rows(0).Item(0) & "," & dsCommonSearch.Tables(0).Rows(0).Item(1)
        Else
            Return ""
        End If

    End Function
    ''' <summary>権限データを取得する</summary>
    Public Function SelKengen(ByVal strAccountNo As String) As CommonSearchDataSet.AccountTableDataTable
        ' DataSetインスタンスの生成()
        Dim dsCommonSearch As New CommonSearchDataSet
        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)
        commandTextSb.AppendLine(" SELECT  ")
        commandTextSb.AppendLine(" irai_gyoumu_kengen,  ")
        commandTextSb.AppendLine(" kekka_gyoumu_kengen,  ")
        commandTextSb.AppendLine(" hosyou_gyoumu_kengen,  ")
        commandTextSb.AppendLine(" hkks_gyoumu_kengen,  ")
        commandTextSb.AppendLine(" koj_gyoumu_kengen,  ")
        commandTextSb.AppendLine(" keiri_gyoumu_kengen,  ")
        commandTextSb.AppendLine(" kaiseki_master_kanri_kengen,  ")
        commandTextSb.AppendLine(" eigyou_master_kanri_kengen,  ")
        commandTextSb.AppendLine(" kkk_master_kanri_kengen,  ")
        commandTextSb.AppendLine(" hansoku_uri_kengen,  ")
        commandTextSb.AppendLine(" data_haki_kengen,  ")
        commandTextSb.AppendLine(" system_kanrisya_kengen,  ")
        commandTextSb.AppendLine(" sinki_nyuuryoku_kengen,  ")
        commandTextSb.AppendLine(" hattyuusyo_kanri_kengen  ")
        commandTextSb.AppendLine(" ,tyousaka_kanrisya_kengen  ")
        commandTextSb.AppendLine(" FROM  ")
        commandTextSb.AppendLine(" m_account  WITH(READCOMMITTED)  ")

        commandTextSb.AppendLine(" WHERE account_no = @account_no ")
        'パラメータの設定
        paramList.Add(MakeParam("@account_no", SqlDbType.Int, 4, strAccountNo))
        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsCommonSearch, _
                    dsCommonSearch.AccountTable.TableName, paramList.ToArray)

        Return dsCommonSearch.AccountTable

    End Function
    ''' <summary>LOGの新規処理</summary>
    Public Function InsUrlLog(ByVal strUrl As String, ByVal strUerId As String) As Boolean
        '戻り値
        InsUrlLog = False

        Dim commandTextSb As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)


        commandTextSb.AppendLine(" INSERT INTO ")
        commandTextSb.AppendLine(" t_sansyou_rireki_kanri ")
        commandTextSb.AppendLine(" WITH(UPDLOCK) ")
        commandTextSb.AppendLine("  (login_user_id,")
        commandTextSb.AppendLine("  url, ")
        commandTextSb.AppendLine("  add_datetime) ")
        commandTextSb.AppendLine(" SELECT ")
        commandTextSb.AppendLine("  @login_user_id ,")
        commandTextSb.AppendLine("  @url, ")
        commandTextSb.AppendLine("  GETDATE() ")

        'パラメータの設定

        paramList.Add(MakeParam("@login_user_id", SqlDbType.VarChar, 30, strUerId))
        paramList.Add(MakeParam("@url", SqlDbType.VarChar, 550, strUrl))

        '更新されたデータセットを DB へ書き込み
        ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

        '終了処理
        commandTextSb = Nothing

        '戻り値をセット成功の場合
        InsUrlLog = True
    End Function
    '''<summary>共通データを取得する</summary>
    Public Function SelCommonInfo(ByVal strCd As String, _
                                        ByVal strTableName As String, Optional ByVal strKubun As String = "") As CommonSearchDataSet.BirudaTableDataTable
        ' DataSetインスタンスの生成()
        Dim dsCommonSearch As New CommonSearchDataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        commandTextSb.AppendLine(" SELECT ")
        Select Case strTableName
            Case "m_kameiten"
                commandTextSb.AppendLine("      kameiten_cd AS cd, ")
                commandTextSb.AppendLine("      kameiten_mei1 AS mei ")
                commandTextSb.AppendLine(" FROM ")
                commandTextSb.AppendLine("      m_kameiten WITH(READCOMMITTED) ")
                commandTextSb.AppendLine(" WHERE torikesi = @torikesi ")
                commandTextSb.AppendLine(" AND kameiten_cd = @kameiten_cd ")
                'パラメータの設定
                paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, "0"))
                paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strCd))
            Case "m_keiretu"
                commandTextSb.AppendLine("      keiretu_cd AS cd, ")
                commandTextSb.AppendLine("      keiretu_mei AS mei ")
                commandTextSb.AppendLine(" FROM ")
                commandTextSb.AppendLine("      m_keiretu WITH(READCOMMITTED) ")
                commandTextSb.AppendLine(" WHERE torikesi = @torikesi ")
                commandTextSb.AppendLine(" AND keiretu_cd = @keiretu_cd ")
                If strKubun <> "" Then
                    commandTextSb.AppendLine(" AND kbn = @kbn ")
                    paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, strKubun))
                End If

                'パラメータの設定
                paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, "0"))
                paramList.Add(MakeParam("@keiretu_cd", SqlDbType.VarChar, 5, strCd))
            Case "m_eigyousyo"
                commandTextSb.AppendLine("      eigyousyo_cd AS cd, ")
                commandTextSb.AppendLine("      eigyousyo_mei AS mei ")
                commandTextSb.AppendLine(" FROM ")
                commandTextSb.AppendLine("      m_eigyousyo WITH(READCOMMITTED) ")
                commandTextSb.AppendLine(" WHERE torikesi = @torikesi ")
                commandTextSb.AppendLine(" AND eigyousyo_cd = @eigyousyo_cd ")
                'パラメータの設定
                paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, "0"))
                paramList.Add(MakeParam("@eigyousyo_cd", SqlDbType.VarChar, 5, strCd))
            Case "Biruda"
                commandTextSb.AppendLine("      kameiten_cd AS cd, ")
                commandTextSb.AppendLine("      kameiten_mei1 AS mei ")
                commandTextSb.AppendLine(" FROM ")
                commandTextSb.AppendLine("      m_kameiten WITH(READCOMMITTED) ")
                commandTextSb.AppendLine(" WHERE torikesi = @torikesi ")
                commandTextSb.AppendLine(" AND kameiten_cd = @kameiten_cd ")
                'パラメータの設定
                paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, "0"))
                paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, strCd))


        End Select

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsCommonSearch, _
                    dsCommonSearch.BirudaTable.TableName, paramList.ToArray)

        Return dsCommonSearch.BirudaTable
    End Function
    '''<summary>加盟店種別データを取得する</summary>
    Public Function Selkameitensyubetu(ByVal intRows As String, _
                    ByVal code As String, ByVal mei As String) As CommonSearchDataSet.meisyouTableDataTable

        ' DataSetインスタンスの生成()
        Dim dsCommonSearch As New CommonSearchDataSet

        ' SQL文の生成
        Dim sql As New StringBuilder

        ' パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        ' 加盟店マスト情報のSql 
        sql.AppendLine("SELECT          ")
        If intRows <> "max" Then
            sql.AppendLine("      TOP " & intRows)
        End If

        sql.Append(" code as  cd")
        sql.Append(",meisyou as mei")
        sql.AppendLine(" FROM            ")
        sql.AppendLine("  m_meisyou WITH (READCOMMITTED)")

        sql.AppendLine("WHERE")
        sql.AppendLine(" meisyou_syubetu='09' ")

        If code.Trim <> "" Then
            sql.AppendLine(" AND code LIKE @code ")
        End If
        If mei.Trim <> "" Then
            sql.AppendLine(" AND meisyou LIKE @meisyou ")
        End If

        sql.Append(" ORDER BY code ")

        'パラメータの設定
        paramList.Add(MakeParam("@code", SqlDbType.VarChar, 5, code & "%"))
        paramList.Add(MakeParam("@meisyou", SqlDbType.VarChar, 82, "%" & mei & "%"))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, sql.ToString(), dsCommonSearch, _
                    dsCommonSearch.meisyouTable.TableName, paramList.ToArray)

        Return dsCommonSearch.meisyouTable

    End Function
    '''<summary>加盟店種別レコード行数を取得する</summary>
    Public Function SelkameitensyubetuCount(ByVal code As String, ByVal mei As String) As Integer

        ' DataSetインスタンスの生成()
        Dim dsCommonSearch As New CommonSearchDataSet

        ' SQL文の生成
        Dim sql As New StringBuilder

        ' パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        ' 加盟店マスト情報のSql 
        sql.AppendLine("SELECT          ")
        sql.Append("code as  cd")
        sql.Append(",meisyou as mei")
        sql.AppendLine(" FROM            ")
        sql.AppendLine("  m_meisyou WITH (READCOMMITTED)")

        sql.AppendLine("WHERE")
        sql.AppendLine(" meisyou_syubetu='09' ")

        If code.Trim <> "" Then
            sql.AppendLine(" AND code LIKE @code ")
        End If
        If mei.Trim <> "" Then
            sql.AppendLine(" AND meisyou LIKE @meisyou ")
        End If

        sql.Append(" ORDER BY code ")

        'パラメータの設定
        paramList.Add(MakeParam("@code", SqlDbType.VarChar, 5, code & "%"))
        paramList.Add(MakeParam("@meisyou", SqlDbType.VarChar, 82, "%" & mei & "%"))

        ' 検索実行
        FillDataset(connStr, CommandType.Text, sql.ToString(), dsCommonSearch, _
                    dsCommonSearch.meisyouTable.TableName, paramList.ToArray)

        Return dsCommonSearch.meisyouTable.Rows.Count

    End Function
    ''' <summary>調査会社データを取得する</summary>
    Public Function SelTyousaInfo(ByVal intRows As String, _
                                        ByVal strCd As String, _
                                        ByVal strMei As String, _
                                        ByVal strMei2 As String, _
                                        ByVal blnDelete As Boolean) As CommonSearchDataSet.tyousakaisyaTableDataTable
        ' DataSetインスタンスの生成()
        Dim dsCommonSearch As New CommonSearchDataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        commandTextSb.AppendLine(" SELECT ")
        If intRows <> "max" Then
            commandTextSb.AppendLine("      TOP " & intRows)
        End If

        commandTextSb.AppendLine("      tys_kaisya_cd, ")
        commandTextSb.AppendLine("      jigyousyo_cd, ")
        commandTextSb.AppendLine("      tys_kaisya_mei, ")
        commandTextSb.AppendLine("      jyuusyo1 ")
        commandTextSb.AppendLine(" FROM ")
        commandTextSb.AppendLine("      m_tyousakaisya  WITH(READCOMMITTED)  ")
        commandTextSb.AppendLine(" WHERE tys_kaisya_cd IS NOT NULL ")
        If blnDelete = True Then
            commandTextSb.Append(" AND torikesi = @torikesi ")
            paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, "0"))
        End If


        If strCd.Trim <> "" Then
            commandTextSb.AppendLine(" AND tys_kaisya_cd+jigyousyo_cd LIKE @strCd ")
        End If
        If strMei.Trim <> "" Then
            commandTextSb.AppendLine(" AND tys_kaisya_mei LIKE @strMei ")
        End If
        If strMei2.Trim <> "" Then
            commandTextSb.AppendLine(" AND tys_kaisya_mei_kana LIKE @strMei2 ")
        End If

        commandTextSb.Append(" ORDER BY tys_kaisya_cd+jigyousyo_cd ")
        'パラメータの設定
        paramList.Add(MakeParam("@strCd", SqlDbType.VarChar, 8, strCd & "%"))
        paramList.Add(MakeParam("@strMei", SqlDbType.VarChar, 42, "%" & strMei & "%"))
        paramList.Add(MakeParam("@strMei2", SqlDbType.VarChar, 22, "%" & strMei2 & "%"))
        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsCommonSearch, _
                    dsCommonSearch.tyousakaisyaTable.TableName, paramList.ToArray)

        Return dsCommonSearch.tyousakaisyaTable
    End Function
    ''' <summary>調査会社レコード行数を取得する</summary>
    Public Function SelTyousaCount(ByVal strCd As String, _
                                        ByVal strMei As String, _
                                        ByVal strMei2 As String, _
                                        ByVal blnDelete As Boolean) As Integer
        ' DataSetインスタンスの生成()
        Dim dsCommonSearch As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        commandTextSb.AppendLine(" SELECT ")

        commandTextSb.AppendLine(" count(tys_kaisya_cd) ")

        commandTextSb.AppendLine(" FROM ")
        commandTextSb.AppendLine("      m_tyousakaisya  WITH(READCOMMITTED)  ")
        commandTextSb.AppendLine(" WHERE tys_kaisya_cd IS NOT NULL ")
        If blnDelete = True Then
            commandTextSb.Append(" AND torikesi = @torikesi ")
            paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, "0"))
        End If


        If strCd.Trim <> "" Then
            commandTextSb.AppendLine(" AND tys_kaisya_cd+jigyousyo_cd LIKE @strCd ")
        End If
        If strMei.Trim <> "" Then
            commandTextSb.AppendLine(" AND tys_kaisya_mei LIKE @strMei ")
        End If
        If strMei2.Trim <> "" Then
            commandTextSb.AppendLine(" AND tys_kaisya_mei_kana LIKE @strMei2 ")
        End If

        'パラメータの設定
        paramList.Add(MakeParam("@strCd", SqlDbType.VarChar, 8, strCd & "%"))
        paramList.Add(MakeParam("@strMei", SqlDbType.VarChar, 42, "%" & strMei & "%"))
        paramList.Add(MakeParam("@strMei2", SqlDbType.VarChar, 22, "%" & strMei2 & "%"))
        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsCommonSearch, _
                    "dsCommonSearch", paramList.ToArray)

        Return CInt(dsCommonSearch.Tables("dsCommonSearch").Rows(0).Item(0))
    End Function

    ''' <summary>請求先データを取得する</summary>
    Public Function SelSeikyuuSakiInfo(ByVal intRows As String, _
                                        ByVal strKbn As String, _
                                        ByVal strCd As String, _
                                        ByVal strBrc As String, ByVal strMei3 As String, ByVal blnDelete As Boolean, Optional ByVal blnKana As Boolean = False) As CommonSearchDataSet.SeikyuuSakiTableDataTable
        ' DataSetインスタンスの生成()
        Dim dsCommonSearch As New CommonSearchDataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            If intRows <> "max" Then
                .AppendLine("      TOP " & intRows)
            End If
            .AppendLine("  v_seikyuu_saki_info.seikyuu_saki_kbn AS seikyuu_saki_kbn, ")
            .AppendLine("  RTRIM(v_seikyuu_saki_info.seikyuu_saki_cd) AS seikyuu_saki_cd, ")
            .AppendLine("  v_seikyuu_saki_info.seikyuu_saki_brc AS seikyuu_saki_brc, ")
            .AppendLine("  v_seikyuu_saki_info.seikyuu_saki_mei AS seikyuu_saki_mei, ")
            .AppendLine("  v_seikyuu_saki_info.torikesi AS torikesi, ")
            .AppendLine("  m_seikyuu_saki.seikyuu_sime_date AS seikyuu_sime_date ")
            .AppendLine("FROM ")
            .AppendLine("   v_seikyuu_saki_info WITH(READCOMMITTED)  ")
            .AppendLine("   INNER JOIN m_seikyuu_saki WITH(READCOMMITTED)  ")
            .AppendLine("   ON  v_seikyuu_saki_info.seikyuu_saki_cd=m_seikyuu_saki.seikyuu_saki_cd")
            .AppendLine("   AND  v_seikyuu_saki_info.seikyuu_saki_kbn=m_seikyuu_saki.seikyuu_saki_kbn")
            .AppendLine("   AND  v_seikyuu_saki_info.seikyuu_saki_brc=m_seikyuu_saki.seikyuu_saki_brc")
            .AppendLine(" WHERE ")
        End With

        If blnDelete = True Then
            commandTextSb.Append(" v_seikyuu_saki_info.torikesi = @torikesi ")
            paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 1, "0"))
        Else
            commandTextSb.Append("  0 = 0 ")
        End If

        If strCd.Trim <> "" Then
            commandTextSb.AppendLine(" AND v_seikyuu_saki_info.seikyuu_saki_cd LIKE @strCd ")
        End If
        If strBrc.Trim <> "" Then
            commandTextSb.AppendLine(" AND v_seikyuu_saki_info.seikyuu_saki_brc = @strBrc ")
        End If
        If strKbn.Trim <> "" Then
            commandTextSb.AppendLine(" AND v_seikyuu_saki_info.seikyuu_saki_kbn = @strKbn ")
        End If
        If strMei3.Trim <> "" Then
            If blnKana Then
                commandTextSb.AppendLine(" AND v_seikyuu_saki_info.seikyuu_saki_kana LIKE @strMei3 ")
            Else
                commandTextSb.AppendLine(" AND v_seikyuu_saki_info.seikyuu_saki_mei LIKE @strMei3 ")
            End If

        End If

        commandTextSb.Append(" ORDER BY v_seikyuu_saki_info.seikyuu_saki_cd,v_seikyuu_saki_info.seikyuu_saki_brc,v_seikyuu_saki_info.seikyuu_saki_kbn ")

        'パラメータの設定
        paramList.Add(MakeParam("@strKbn", SqlDbType.VarChar, 255, strKbn))
        paramList.Add(MakeParam("@strCd", SqlDbType.VarChar, 255, strCd & "%"))
        paramList.Add(MakeParam("@strBrc", SqlDbType.VarChar, 255, strBrc))
        paramList.Add(MakeParam("@strMei3", SqlDbType.VarChar, 255, strMei3 & "%"))
        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsCommonSearch, _
                    dsCommonSearch.SeikyuuSakiTable.TableName, paramList.ToArray)

        Return dsCommonSearch.SeikyuuSakiTable
    End Function
    ''' <summary>請求先レコード行数を取得する</summary>
    Public Function SelSeikyuuSakiCount(ByVal strKbn As String, _
                                        ByVal strCd As String, _
                                        ByVal strBrc As String, ByVal strMei3 As String, ByVal blnDelete As Boolean, Optional ByVal blnKana As Boolean = False) As Integer
        ' DataSetインスタンスの生成()
        Dim dsCommonSearch As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine("SELECT ")
            .AppendLine("  COUNT(v_seikyuu_saki_info.seikyuu_saki_cd) ")
            .AppendLine("FROM ")
            .AppendLine("   v_seikyuu_saki_info WITH(READCOMMITTED)  ")
            .AppendLine("   INNER JOIN m_seikyuu_saki WITH(READCOMMITTED)  ")
            .AppendLine("   ON  v_seikyuu_saki_info.seikyuu_saki_cd=m_seikyuu_saki.seikyuu_saki_cd")
            .AppendLine("   AND  v_seikyuu_saki_info.seikyuu_saki_kbn=m_seikyuu_saki.seikyuu_saki_kbn")
            .AppendLine("   AND  v_seikyuu_saki_info.seikyuu_saki_brc=m_seikyuu_saki.seikyuu_saki_brc")

            .AppendLine(" WHERE ")
        End With

        If blnDelete = True Then
            commandTextSb.Append(" v_seikyuu_saki_info.torikesi = @torikesi ")
            paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 1, "0"))
        Else
            commandTextSb.Append("  0 = 0 ")
        End If

        If strCd.Trim <> "" Then
            commandTextSb.AppendLine(" AND v_seikyuu_saki_info.seikyuu_saki_cd LIKE @strCd ")
        End If
        If strBrc.Trim <> "" Then
            commandTextSb.AppendLine(" AND v_seikyuu_saki_info.seikyuu_saki_brc = @strBrc ")
        End If
        If strKbn.Trim <> "" Then
            commandTextSb.AppendLine(" AND v_seikyuu_saki_info.seikyuu_saki_kbn = @strKbn ")
        End If
        If strMei3.Trim <> "" Then
            If blnKana Then
                commandTextSb.AppendLine(" AND v_seikyuu_saki_info.seikyuu_saki_kana LIKE @strMei3 ")
            Else
                commandTextSb.AppendLine(" AND v_seikyuu_saki_info.seikyuu_saki_mei LIKE @strMei3 ")
            End If

        End If

        'パラメータの設定
        paramList.Add(MakeParam("@strKbn", SqlDbType.VarChar, 255, strKbn))
        paramList.Add(MakeParam("@strCd", SqlDbType.VarChar, 255, strCd & "%"))
        paramList.Add(MakeParam("@strBrc", SqlDbType.VarChar, 255, strBrc))
        paramList.Add(MakeParam("@strMei3", SqlDbType.VarChar, 255, strMei3 & "%"))
        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsCommonSearch, _
                    "dsCommonSearch", paramList.ToArray)

        Return CInt(dsCommonSearch.Tables("dsCommonSearch").Rows(0).Item(0))
    End Function

    ''' <summary>調査会社データを取得する</summary>
    Public Function SelJigyousyoInfo(ByVal intRows As String, _
                                        ByVal strCd As String, _
                                        ByVal strMei As String, _
                                        ByVal strMei2 As String) As DataTable
        ' DataSetインスタンスの生成()
        Dim dsCommonSearch As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        commandTextSb.AppendLine(" SELECT ")
        If intRows <> "max" Then
            commandTextSb.AppendLine("      TOP " & intRows)
        End If

        commandTextSb.AppendLine("      skk_jigyousyo_cd, ")
        commandTextSb.AppendLine("      skk_jigyousyo_mei, ")
        commandTextSb.AppendLine("      skk_jigyousyo_ryakusyou ")
        commandTextSb.AppendLine(" FROM ")
        commandTextSb.AppendLine("      m_sinkaikei_jigyousyo  WITH(READCOMMITTED)  ")
        commandTextSb.AppendLine(" WHERE skk_jigyousyo_cd IS NOT NULL ")

        If strCd.Trim <> "" Then
            commandTextSb.AppendLine(" AND skk_jigyousyo_cd LIKE @skk_jigyousyo_cd ")
        End If
        If strMei.Trim <> "" Then
            commandTextSb.AppendLine(" AND skk_jigyousyo_mei LIKE @skk_jigyousyo_mei ")
        End If
        If strMei2.Trim <> "" Then
            commandTextSb.AppendLine(" AND skk_jigyousyo_ryakusyou LIKE @skk_jigyousyo_ryakusyou ")
        End If

        commandTextSb.Append(" ORDER BY skk_jigyousyo_cd ")
        'パラメータの設定
        paramList.Add(MakeParam("@skk_jigyousyo_cd", SqlDbType.VarChar, 11, strCd & "%"))
        paramList.Add(MakeParam("@skk_jigyousyo_mei", SqlDbType.VarChar, 42, "%" & strMei & "%"))
        paramList.Add(MakeParam("@skk_jigyousyo_ryakusyou", SqlDbType.VarChar, 22, "%" & strMei2 & "%"))
        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsCommonSearch, _
                    "dsCommonSearch", paramList.ToArray)

        Return dsCommonSearch.Tables(0)

    End Function
    ''' <summary>調査会社レコード行数を取得する</summary>
    Public Function SelJigyousyoCount(ByVal strCd As String, _
                                        ByVal strMei As String, _
                                        ByVal strMei2 As String) As Integer
        ' DataSetインスタンスの生成()
        Dim dsCommonSearch As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        commandTextSb.AppendLine(" SELECT ")

        commandTextSb.AppendLine(" count(skk_jigyousyo_cd) ")

        commandTextSb.AppendLine(" FROM ")
        commandTextSb.AppendLine("      m_sinkaikei_jigyousyo  WITH(READCOMMITTED)  ")
        commandTextSb.AppendLine(" WHERE skk_jigyousyo_cd IS NOT NULL ")
        If strCd.Trim <> "" Then
            commandTextSb.AppendLine(" AND skk_jigyousyo_cd LIKE @skk_jigyousyo_cd ")
        End If
        If strMei.Trim <> "" Then
            commandTextSb.AppendLine(" AND skk_jigyousyo_mei LIKE @skk_jigyousyo_mei ")
        End If
        If strMei2.Trim <> "" Then
            commandTextSb.AppendLine(" AND skk_jigyousyo_ryakusyou LIKE @skk_jigyousyo_ryakusyou ")
        End If
        'パラメータの設定
        paramList.Add(MakeParam("@skk_jigyousyo_cd", SqlDbType.VarChar, 11, strCd & "%"))
        paramList.Add(MakeParam("@skk_jigyousyo_mei", SqlDbType.VarChar, 42, "%" & strMei & "%"))
        paramList.Add(MakeParam("@skk_jigyousyo_ryakusyou", SqlDbType.VarChar, 22, "%" & strMei2 & "%"))
        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsCommonSearch, _
                    "dsCommonSearch", paramList.ToArray)

        Return CInt(dsCommonSearch.Tables("dsCommonSearch").Rows(0).Item(0))
    End Function
    ''' <summary>調査会社データを取得する</summary>
    Public Function SelSinkaikeiSiharaiSakiInfo(ByVal intRows As String, _
                                        ByVal strCd As String, _
                                        ByVal strCd2 As String, _
                                        ByVal strMei As String, _
                                        ByVal strMei2 As String) As DataTable
        ' DataSetインスタンスの生成()
        Dim dsCommonSearch As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        commandTextSb.AppendLine(" SELECT ")
        If intRows <> "max" Then
            commandTextSb.AppendLine("      TOP " & intRows)
        End If

        commandTextSb.AppendLine("      skk_jigyou_cd, ")
        commandTextSb.AppendLine("      skk_shri_saki_cd, ")
        commandTextSb.AppendLine("      shri_saki_mei_kanji ")
        commandTextSb.AppendLine(" FROM ")
        commandTextSb.AppendLine("      m_sinkaikei_siharai_saki  WITH(READCOMMITTED)  ")
        commandTextSb.AppendLine(" WHERE skk_jigyou_cd IS NOT NULL ")

        If strCd.Trim <> "" Then
            commandTextSb.AppendLine(" AND skk_jigyou_cd LIKE @skk_jigyou_cd ")
        End If
        If strCd2.Trim <> "" Then
            commandTextSb.AppendLine(" AND skk_shri_saki_cd LIKE @skk_shri_saki_cd ")
        End If
        If strMei.Trim <> "" Then
            commandTextSb.AppendLine(" AND shri_saki_mei LIKE @shri_saki_mei ")
        End If
        If strMei2.Trim <> "" Then
            commandTextSb.AppendLine(" AND shri_saki_mei_kanji LIKE @shri_saki_mei_kanji ")
        End If

        commandTextSb.Append(" ORDER BY skk_jigyou_cd ")
        'パラメータの設定
        paramList.Add(MakeParam("@skk_jigyou_cd", SqlDbType.VarChar, 11, strCd & "%"))
        paramList.Add(MakeParam("@skk_shri_saki_cd", SqlDbType.VarChar, 11, strCd2 & "%"))
        paramList.Add(MakeParam("@shri_saki_mei", SqlDbType.VarChar, 32, "%" & strMei & "%"))
        paramList.Add(MakeParam("@shri_saki_mei_kanji", SqlDbType.VarChar, 42, "%" & strMei2 & "%"))
        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsCommonSearch, _
                    "dsCommonSearch", paramList.ToArray)

        Return dsCommonSearch.Tables(0)

    End Function
    Public Function SelSinkaikeiSiharaiSakiCount(ByVal strCd As String, _
                                        ByVal strCd2 As String, _
                                        ByVal strMei As String, _
                                        ByVal strMei2 As String) As Integer
        ' DataSetインスタンスの生成()
        Dim dsCommonSearch As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        commandTextSb.AppendLine(" SELECT ")

        commandTextSb.AppendLine(" count(skk_jigyou_cd) ")

        commandTextSb.AppendLine(" FROM ")
        commandTextSb.AppendLine("      m_sinkaikei_siharai_saki  WITH(READCOMMITTED)  ")
        commandTextSb.AppendLine(" WHERE skk_jigyou_cd IS NOT NULL ")

        If strCd.Trim <> "" Then
            commandTextSb.AppendLine(" AND skk_jigyou_cd LIKE @skk_jigyou_cd ")
        End If
        If strCd2.Trim <> "" Then
            commandTextSb.AppendLine(" AND skk_shri_saki_cd LIKE @skk_shri_saki_cd ")
        End If
        If strMei.Trim <> "" Then
            commandTextSb.AppendLine(" AND shri_saki_mei LIKE @shri_saki_mei ")
        End If
        If strMei2.Trim <> "" Then
            commandTextSb.AppendLine(" AND shri_saki_mei_kanji LIKE @shri_saki_mei_kanji ")
        End If

        'パラメータの設定
        paramList.Add(MakeParam("@skk_jigyou_cd", SqlDbType.VarChar, 11, strCd & "%"))
        paramList.Add(MakeParam("@skk_shri_saki_cd", SqlDbType.VarChar, 11, strCd2 & "%"))
        paramList.Add(MakeParam("@shri_saki_mei", SqlDbType.VarChar, 32, "%" & strMei & "%"))
        paramList.Add(MakeParam("@shri_saki_mei_kanji", SqlDbType.VarChar, 42, "%" & strMei2 & "%"))
        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsCommonSearch, _
                    "dsCommonSearch", paramList.ToArray)

        Return CInt(dsCommonSearch.Tables("dsCommonSearch").Rows(0).Item(0))
    End Function

    ''' <summary>名寄先データを取得する</summary>
    Public Function SelNayoseSakiInfo(ByVal intRows As String, _
                                        ByVal strCd As String, _
                                        ByVal strMei As String, _
                                        ByVal strMei2 As String) As DataTable
        ' DataSetインスタンスの生成()
        Dim dsCommonSearch As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        commandTextSb.AppendLine(" SELECT ")
        If intRows <> "max" Then
            commandTextSb.AppendLine("      TOP " & intRows)
        End If

        commandTextSb.AppendLine("      nayose_saki_cd, ")
        commandTextSb.AppendLine("      nayose_saki_name1, ")
        commandTextSb.AppendLine("      nayose_saki_kana1 ")
        commandTextSb.AppendLine(" FROM ")
        commandTextSb.AppendLine("      m_yosinkanri  WITH(READCOMMITTED)  ")
        commandTextSb.AppendLine(" WHERE nayose_saki_cd IS NOT NULL ")

        If strCd.Trim <> "" Then
            commandTextSb.AppendLine(" AND nayose_saki_cd LIKE @nayose_saki_cd ")
        End If
        If strMei.Trim <> "" Then
            commandTextSb.AppendLine(" AND nayose_saki_name1 LIKE @nayose_saki_name1 ")
        End If
        If strMei2.Trim <> "" Then
            commandTextSb.AppendLine(" AND nayose_saki_kana1 LIKE @nayose_saki_kana1 ")
        End If

        commandTextSb.Append(" ORDER BY nayose_saki_cd ")
        'パラメータの設定
        paramList.Add(MakeParam("@nayose_saki_cd", SqlDbType.VarChar, 5, strCd & "%"))
        paramList.Add(MakeParam("@nayose_saki_name1", SqlDbType.VarChar, 40, "%" & strMei & "%"))
        paramList.Add(MakeParam("@nayose_saki_kana1", SqlDbType.VarChar, 255, "%" & strMei2 & "%"))
        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsCommonSearch, _
                    "dsCommonSearch", paramList.ToArray)

        Return dsCommonSearch.Tables(0)

    End Function
    Public Function SelNayoseSakiCount(ByVal strCd As String, _
                                        ByVal strMei As String, _
                                        ByVal strMei2 As String) As Integer
        ' DataSetインスタンスの生成()
        Dim dsCommonSearch As New DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        commandTextSb.AppendLine(" SELECT ")

        commandTextSb.AppendLine(" count(nayose_saki_cd) ")

        commandTextSb.AppendLine(" FROM ")
        commandTextSb.AppendLine("      m_yosinkanri  WITH(READCOMMITTED)  ")
        commandTextSb.AppendLine(" WHERE nayose_saki_cd IS NOT NULL ")

        If strCd.Trim <> "" Then
            commandTextSb.AppendLine(" AND nayose_saki_cd LIKE @nayose_saki_cd ")
        End If
        If strMei.Trim <> "" Then
            commandTextSb.AppendLine(" AND nayose_saki_name1 LIKE @nayose_saki_name1 ")
        End If
        If strMei2.Trim <> "" Then
            commandTextSb.AppendLine(" AND nayose_saki_kana1 LIKE @nayose_saki_kana1 ")
        End If

        'パラメータの設定
        paramList.Add(MakeParam("@nayose_saki_cd", SqlDbType.VarChar, 5, strCd & "%"))
        paramList.Add(MakeParam("@nayose_saki_name1", SqlDbType.VarChar, 40, "%" & strMei & "%"))
        paramList.Add(MakeParam("@nayose_saki_kana1", SqlDbType.VarChar, 255, "%" & strMei2 & "%"))
        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsCommonSearch, _
                    "dsCommonSearch", paramList.ToArray)

        Return CInt(dsCommonSearch.Tables("dsCommonSearch").Rows(0).Item(0))
    End Function

    ''' <summary>特別対応データを取得する</summary>
    Public Function SelTokubetuTaiouInfo(ByVal intRows As String, ByVal strCd As String, ByVal strMei As String _
                                            , ByVal blnDelete As Boolean) As Data.DataTable
        ' DataSetインスタンスの生成()
        Dim dsCommonSearch As New DataSet
        'SQL文の生成
        Dim commandTextsb As New StringBuilder
        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With commandTextsb
            .AppendLine(" SELECT ")
            If intRows <> "max" Then
                .AppendLine("      TOP " & intRows)
            End If
            .AppendLine("	tokubetu_taiou_cd ")
            .AppendLine("   ,tokubetu_taiou_meisyou ")
            .AppendLine("	,torikesi ")
            .AppendLine(" FROM ")
            .AppendLine("	m_tokubetu_taiou WITH(READCOMMITTED) ")
            .AppendLine(" WHERE 1=1 ")
            If blnDelete = True Then
                .AppendLine(" AND torikesi = @torikesi ")
                paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, "0"))
            End If
            If strCd.Trim <> "" Then
                .AppendLine(" AND tokubetu_taiou_cd LIKE @tokubetu_taiou_cd ")
                paramList.Add(MakeParam("@tokubetu_taiou_cd", SqlDbType.VarChar, 6, strCd & "%"))
            End If
            If strMei.Trim <> "" Then
                .AppendLine(" AND tokubetu_taiou_meisyou LIKE @tokubetu_taiou_meisyou ")
                paramList.Add(MakeParam("@tokubetu_taiou_meisyou", SqlDbType.VarChar, 42, "%" & strMei & "%"))
            End If
            .AppendLine(" ORDER BY ")
            .AppendLine("   tokubetu_taiou_cd ")
        End With

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextsb.ToString, dsCommonSearch, "tokubetutaiou", paramList.ToArray)

        Return dsCommonSearch.Tables(0)
    End Function

    ''' <summary>特別対応行数を取得する</summary>
    Public Function SelTokubetuTaiouCount(ByVal strCd As String, ByVal strMei As String, ByVal blnDelete As Boolean) As Integer
        ' DataSetインスタンスの生成()
        Dim dsCommonSearch As New DataSet
        'SQL文の生成
        Dim commandTextsb As New StringBuilder
        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With commandTextsb
            .AppendLine(" SELECT ")
            .AppendLine("	COUNT(tokubetu_taiou_cd) ")
            .AppendLine(" FROM ")
            .AppendLine("	m_tokubetu_taiou WITH(READCOMMITTED) ")
            .AppendLine(" WHERE 1=1 ")
            If blnDelete = True Then
                .AppendLine(" AND torikesi = @torikesi ")
                paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 4, "0"))
            End If
            If strCd.Trim <> "" Then
                .AppendLine(" AND tokubetu_taiou_cd LIKE @ttc ")
                paramList.Add(MakeParam("@ttc", SqlDbType.VarChar, 6, strCd & "%"))
            End If
            If strMei.Trim <> "" Then
                .AppendLine(" AND tokubetu_taiou_meisyou LIKE @ttm ")
                paramList.Add(MakeParam("@ttm", SqlDbType.VarChar, 42, "%" & strMei & "%"))
            End If
        End With

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextsb.ToString, dsCommonSearch, "tokubetutaiou", paramList.ToArray)

        Return CInt(dsCommonSearch.Tables(0).Rows(0).Item(0))
    End Function






    ''' <summary>コード取得元のマスタ取得する</summary>
    Public Function SelSAPSiireSaki(ByVal top As Integer, ByVal a1_ktokk As String, ByVal a1_lifnr As String, ByVal a1_a_zz_sort As String, ByVal sort As String) As DataSet
        ' DataSetインスタンスの生成()
        Dim dsCommonSearch As New DataSet
        'SQL文の生成
        Dim commandTextsb As New StringBuilder
        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With commandTextsb

            .AppendLine(" SELECT ")
            .AppendLine("	count(*) ")
            .AppendLine(" FROM ")
            .AppendLine("	m_sinkaikei_siire_saki WITH(READCOMMITTED) ")
            .AppendLine(" WHERE 1=1 ")

            If a1_ktokk.Trim <> "" Then
                .AppendLine(" AND a1_ktokk LIKE @a1_ktokk ")
                paramList.Add(MakeParam("@a1_ktokk", SqlDbType.VarChar, 6, a1_ktokk & "%"))
            End If

            If a1_lifnr.Trim <> "" Then
                .AppendLine(" AND a1_lifnr LIKE @a1_lifnr ")
                paramList.Add(MakeParam("@a1_lifnr", SqlDbType.VarChar, 12, a1_lifnr & "%"))
            End If

            If a1_a_zz_sort.Trim <> "" Then
                .AppendLine(" AND a1_a_zz_sort LIKE @a1_a_zz_sort ")
                paramList.Add(MakeParam("@a1_a_zz_sort", SqlDbType.VarChar, 72, "%" & a1_a_zz_sort & "%"))
            End If


            .AppendLine(" SELECT ")
            If top > 0 Then
                .AppendLine(" TOP " & top)
            End If

            .AppendLine("	a1_ktokk,a1_lifnr,a1_a_zz_sort ")
            .AppendLine(" FROM ")
            .AppendLine("	m_sinkaikei_siire_saki WITH(READCOMMITTED) ")
            .AppendLine(" WHERE 1=1 ")

            If a1_ktokk.Trim <> "" Then
                .AppendLine(" AND a1_ktokk LIKE @a1_ktokk1 ")
                paramList.Add(MakeParam("@a1_ktokk1", SqlDbType.VarChar, 6, a1_ktokk & "%"))
            End If

            If a1_lifnr.Trim <> "" Then
                .AppendLine(" AND a1_lifnr LIKE @a1_lifnr1 ")
                paramList.Add(MakeParam("@a1_lifnr1", SqlDbType.VarChar, 12, a1_lifnr & "%"))
            End If

            If a1_a_zz_sort.Trim <> "" Then
                .AppendLine(" AND a1_a_zz_sort LIKE @a1_a_zz_sort1 ")
                paramList.Add(MakeParam("@a1_a_zz_sort1", SqlDbType.VarChar, 72, "%" & a1_a_zz_sort & "%"))
            End If
            .AppendLine(" ORDER BY " & sort)

        End With

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextsb.ToString, dsCommonSearch, "tokubetutaiou", paramList.ToArray)

        Return dsCommonSearch
    End Function



    ''' <summary>勘定ｸﾞﾙｰﾌﾟ取得する</summary>
    Public Function SelDis_a1_ktokk() As DataTable
        ' DataSetインスタンスの生成()
        Dim dsCommonSearch As New DataSet
        'SQL文の生成
        Dim commandTextsb As New StringBuilder
        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        With commandTextsb

            .AppendLine(" SELECT ")
            .AppendLine("	distinct a1_ktokk ")
            .AppendLine(" FROM ")
            .AppendLine("	m_sinkaikei_siire_saki WITH(READCOMMITTED) ")

        End With

        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextsb.ToString, dsCommonSearch, "a1_ktokk", paramList.ToArray)

        Return dsCommonSearch.Tables(0)
    End Function




End Class

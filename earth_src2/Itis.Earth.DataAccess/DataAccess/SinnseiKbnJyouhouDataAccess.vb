Imports System.Data.SqlClient
Imports System.Text
Imports Itis.ApplicationBlocks.Data.SQLHelper

Public Class SinnseiKbnJyouhouDataAccess

    Private connStr As String = ConnectionManager.Instance.ConnectionString

    ''' <summary>加盟店マスト情報を更新する</summary>
    Public Function UpdSinnseiInfo(ByVal kameiten_cd As String _
                                        , ByVal shinsei_syoshiki As String _
                                        , ByVal shinsei_kbn_shinki As String _
                                        , ByVal shinsei_kbn_sonota As String _
                                        , ByVal shinsei_kbn_jig_shinki As String _
                                        , ByVal shinsei_kbn_jig_fudousan As String _
                                        , ByVal shinsei_kbn_jig_reform As String _
                                        , ByVal shinsei_kbn_jig_sonota As String _
                                        , ByVal shinsei_kbn_ser_jibantyousa As String _
                                        , ByVal shinsei_kbn_ser_tatemonokensa As String _
                                        , ByVal shinsei_kbn_jig_sonota_hosoku As String _
                                        , ByVal shinsei_kbn_ser_sonota As String _
                                        , ByVal shinsei_kbn_ser_sonota_hosoku As String _
                                        , ByVal upd_login_user_id As String) As Integer

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine(" UPDATE ")
            .AppendLine(" m_kameiten ")
            .AppendLine(" WITH(UPDLOCK) ")
            .AppendLine(" SET ")
            .AppendLine("shinsei_syoshiki=@shinsei_syoshiki")
            .AppendLine(",shinsei_kbn_shinki=@shinsei_kbn_shinki")
            .AppendLine(",shinsei_kbn_sonota=@shinsei_kbn_sonota")
            .AppendLine(",shinsei_kbn_jig_shinki=@shinsei_kbn_jig_shinki")
            .AppendLine(",shinsei_kbn_jig_fudousan=@shinsei_kbn_jig_fudousan")
            .AppendLine(",shinsei_kbn_jig_reform=@shinsei_kbn_jig_reform")
            .AppendLine(",shinsei_kbn_jig_sonota=@shinsei_kbn_jig_sonota")
            .AppendLine(",shinsei_kbn_ser_jibantyousa=@shinsei_kbn_ser_jibantyousa")
            .AppendLine(",shinsei_kbn_ser_tatemonokensa=@shinsei_kbn_ser_tatemonokensa")
            .AppendLine(",shinsei_kbn_jig_sonota_hosoku=@shinsei_kbn_jig_sonota_hosoku")
            .AppendLine(",shinsei_kbn_ser_sonota=@shinsei_kbn_ser_sonota")
            .AppendLine(",shinsei_kbn_ser_sonota_hosoku=@shinsei_kbn_ser_sonota_hosoku")

            commandTextSb.AppendLine(" ,upd_login_user_id=@upd_login_user_id ")
            commandTextSb.AppendLine(" ,upd_datetime=GETDATE() ")
            .AppendLine(" WHERE m_kameiten.kameiten_cd=@kameiten_cd ")
        End With

        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, kameiten_cd))
        paramList.Add(MakeParam("@shinsei_syoshiki", SqlDbType.VarChar, 10, shinsei_syoshiki))
        paramList.Add(MakeParam("@shinsei_kbn_shinki", SqlDbType.Int, 9, shinsei_kbn_shinki))
        paramList.Add(MakeParam("@shinsei_kbn_sonota", SqlDbType.Int, 9, shinsei_kbn_sonota))
        paramList.Add(MakeParam("@shinsei_kbn_jig_shinki", SqlDbType.Int, 9, shinsei_kbn_jig_shinki))
        paramList.Add(MakeParam("@shinsei_kbn_jig_fudousan", SqlDbType.Int, 9, shinsei_kbn_jig_fudousan))
        paramList.Add(MakeParam("@shinsei_kbn_jig_reform", SqlDbType.Int, 9, shinsei_kbn_jig_reform))
        paramList.Add(MakeParam("@shinsei_kbn_jig_sonota", SqlDbType.Int, 9, shinsei_kbn_jig_sonota))
        paramList.Add(MakeParam("@shinsei_kbn_ser_jibantyousa", SqlDbType.Int, 9, shinsei_kbn_ser_jibantyousa))
        paramList.Add(MakeParam("@shinsei_kbn_ser_tatemonokensa", SqlDbType.Int, 9, shinsei_kbn_ser_tatemonokensa))
        paramList.Add(MakeParam("@shinsei_kbn_jig_sonota_hosoku", SqlDbType.VarChar, 50, shinsei_kbn_jig_sonota_hosoku))
        paramList.Add(MakeParam("@shinsei_kbn_ser_sonota", SqlDbType.Int, 9, shinsei_kbn_ser_sonota))
        paramList.Add(MakeParam("@shinsei_kbn_ser_sonota_hosoku", SqlDbType.VarChar, 50, shinsei_kbn_ser_sonota_hosoku))
        paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, upd_login_user_id))

        '更新されたデータセットを DB へ書き込み
        ExecuteNonQuery(connStr, CommandType.Text, commandTextSb.ToString(), paramList.ToArray)

        '終了処理
        commandTextSb = Nothing

        '戻り値をセット成功の場合
        UpdSinnseiInfo = True

    End Function

    ''' <summary>加盟店マスト情報を取得する</summary>
    Public Function SelSinnseiInfo(ByVal kameiten_cd As String) As DataTable
        ' DataSetインスタンスの生成()
        Dim dsDataSet As New Data.DataSet

        'SQL文の生成
        Dim commandTextSb As New StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        With commandTextSb
            .AppendLine(" SELECT ")
            .AppendLine("shinsei_syoshiki")
            .AppendLine(",shinsei_kbn_shinki")
            .AppendLine(",shinsei_kbn_sonota")
            .AppendLine(",shinsei_kbn_jig_shinki")
            .AppendLine(",shinsei_kbn_jig_fudousan")
            .AppendLine(",shinsei_kbn_jig_reform")
            .AppendLine(",shinsei_kbn_jig_sonota")
            .AppendLine(",shinsei_kbn_ser_jibantyousa")
            .AppendLine(",shinsei_kbn_ser_tatemonokensa")
            .AppendLine(",shinsei_kbn_jig_sonota_hosoku")
            .AppendLine(",shinsei_kbn_ser_sonota")
            .AppendLine(",shinsei_kbn_ser_sonota_hosoku")

            .AppendLine(" From m_kameiten ")
            .AppendLine(" WHERE m_kameiten.kameiten_cd=@kameiten_cd ")
        End With

        'パラメータの設定
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, kameiten_cd))
        ' 検索実行
        FillDataset(connStr, CommandType.Text, commandTextSb.ToString(), dsDataSet, _
                    "dsDataSet", paramList.ToArray)

        Return dsDataSet.Tables(0)

    End Function

    ''' <summary>Param EMPTY TO NULL</summary>
    Function setParam(ByVal strObj As Object) As Object
        If strObj.ToString.Trim = "" Then
            Return DBNull.Value
        Else
            Return strObj.ToString
        End If
    End Function

End Class

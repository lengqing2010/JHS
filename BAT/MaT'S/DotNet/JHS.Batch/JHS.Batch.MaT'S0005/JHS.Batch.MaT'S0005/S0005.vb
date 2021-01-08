Option Explicit On
Option Strict On

Imports System.Data
Imports System.Text
Imports System.IO
Imports System.Transactions
Imports JHS.Batch.SqlExecutor
Imports JHS.Batch
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase

''' <summary>
''' 年度比率管理データを年度比率管理テーブルに格納する
''' </summary>
''' <remarks>JHS Earth ｼｽﾃﾑ の 売上ﾃﾞｰﾀ　と　仕入ﾃﾞｰﾀ　と 加盟店ﾏｽﾀ　から
'''          加盟店ごとの 売上比率、工事判定率、工事受注率、直工事率
'''          を　年度比率管理テーブル　に格納する　</remarks>
''' <history>
''' <para>2013/1/15 大連/楊双 新規作成 P-45026</para>
''' </history>
Public Class S0005

#Region "定数"
    'バッチID
    Private Const CON_BATCH_ID As String = "bat_set5"
#End Region

#Region "変数"
    '各Event/Methodの動作時における、"EMAB障害対応情報の格納処理"向け、自クラス名
    Private ReadOnly mMyNamePeriod As String = MyClass.GetType.FullName
    'DB接続ストリング
    Private mDBconnectionEarth As String
    Private mDBconnectionJHS As String
    'DB接続
    Private mConnectionEarth As SqlExecutor
    Private mConnectionJHS As SqlExecutor
    'ログメッセージ
    Private mLogMsg As New StringBuilder()
    '新規件数
    Private mInsCount As Integer = 0
#End Region

#Region "Main処理"
    ''' <summary>
    ''' Main処理
    ''' </summary>
    ''' <param name="argv"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2013/01/15 大連/楊双 新規作成 P-45026</para>
    ''' </history>
    Public Shared Function Main(ByVal argv As String()) As Integer
        Dim btProcess As S0005

        '初期化
        btProcess = New S0005()

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(btProcess.mMyNamePeriod & MyMethod.GetCurrentMethod.Name, argv)

        Try
            'DB接続ストリング
            btProcess.mDBconnectionEarth = Definition.GetConnectionStringEarth()
            btProcess.mDBconnectionJHS = Definition.GetConnectionStringJHS()

            'DB接続
            btProcess.mConnectionEarth = New SqlExecutor(btProcess.mDBconnectionEarth)
            btProcess.mConnectionJHS = New SqlExecutor(btProcess.mDBconnectionJHS)

            '主処理を呼び込む()
            Call btProcess.Main_Process()

            Return 0
        Catch ex As Exception
            Dim strErrorMsg As String = ""
            If ex.Data.Item("ERROR_LOG") IsNot Nothing Then
                strErrorMsg = Convert.ToString(ex.Data.Item("ERROR_LOG"))
                btProcess.mLogMsg.AppendLine(Format(Date.Now, "yyyy年MM月dd日 HH:mm:ss  ") & strErrorMsg)
            End If

            '異常を発生する場合、ログファイルに出力する
            btProcess.mLogMsg.AppendLine(ex.Message)

            btProcess.mLogMsg.AppendLine(Format(Date.Now, "yyyy年MM月dd日 HH:mm:ss  ") & "上記の処理をロールバックしました。")
            btProcess.mInsCount = 0
            Return 9
        Finally
            If btProcess.mConnectionEarth IsNot Nothing Then
                'DB接続のグルーズ
                btProcess.mConnectionEarth.Close()
                btProcess.mConnectionEarth.Dispose()
            End If

            If btProcess.mConnectionJHS IsNot Nothing Then
                'DB接続のグルーズ
                btProcess.mConnectionJHS.Close()
                btProcess.mConnectionJHS.Dispose()
            End If

            '新規件数をログファイルに出力する
            btProcess.mLogMsg.AppendLine(Format(Date.Now, "yyyy年MM月dd日 HH:mm:ss  ") & _
            "計画管理_年度比率テーブルに" & _
            Convert.ToString(btProcess.mInsCount) & _
            "件データが挿入されました。")

            'ログ出力
            Console.WriteLine(btProcess.mLogMsg.ToString())
        End Try
    End Function

#End Region

#Region "主処理"
    ''' <summary>
    ''' 主処理
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2013/01/15 大連/楊双 新規作成 P-45026</para>
    ''' </history>
    Private Sub Main_Process()
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(mMyNamePeriod & MyMethod.GetCurrentMethod.Name)

        Dim dtYear As Data.DataTable                  '計画年度
        Dim dtEarthData As DataTable            'Ｅａｒｔｈデータ
        Dim options As New Transactions.TransactionOptions
        Dim i As Integer
        Dim j As Integer

        Dim strYear As String
        Dim strBeginMonth As String
        Dim strEndMonth As String

        mLogMsg.AppendLine(Format(Date.Now, "yyyy年MM月dd日 HH:mm:ss  ") & _
                            "計画年度ＩＮＩファイル読取る処理を開始しました。")

        '計画年度ＩＮＩファイルを読込み、計画年度を取得する
        dtYear = Definition.GetKeikakuNendo("S0005")

        '正常に読取ることができなかった場合、終了する
        If dtYear Is Nothing OrElse dtYear.Rows.Count = 0 Then
            Exit Sub
        End If

        mLogMsg.AppendLine(Format(Date.Now, "yyyy年MM月dd日 HH:mm:ss  ") & _
                            "Ｅａｒｔｈの年度比率管理データの取得処理を開始しました。")

        'DB接続のオープン
        mConnectionEarth.Open()
        mConnectionJHS.Open(True)

        Try
            For i = 0 To dtYear.Rows.Count - 1

                strYear = Convert.ToString(dtYear.Rows(i)("Year"))
                strBeginMonth = Convert.ToString(dtYear.Rows(i)("BeginMonth"))
                strEndMonth = Convert.ToString(dtYear.Rows(i)("EndMonth"))

                'Ｅａｒｔｈの計画管理_年度比率データデータを取得する
                dtEarthData = SelEarthData(mConnectionEarth, strBeginMonth, strEndMonth)

                If dtEarthData IsNot Nothing Then
                    mLogMsg.AppendLine(Format(Date.Now, "yyyy年MM月dd日 HH:mm:ss  ") & _
                                        strYear & "年度にＥａｒｔｈのデータから" & _
                                        Convert.ToString(dtEarthData.Rows.Count) & _
                                        "件データが取得されました。")
                End If

                If dtEarthData.Rows.Count > 0 Then

                    '該当年度のデータを削除する
                    DelJHSData(mConnectionJHS, strYear)

                    'Ｅａｒｔｈの計画管理_年度比率データにより、ループする
                    For j = 0 To dtEarthData.Rows.Count - 1

                        mInsCount = mInsCount + InsJHSData(mConnectionJHS, dtEarthData.Rows(j), strYear)

                    Next

                End If

                'データを解放する
                If dtEarthData IsNot Nothing Then
                    dtEarthData.Dispose()
                    dtEarthData = Nothing
                End If
            Next

            '成功の場合
            mConnectionJHS.Commit()
        Catch ex As Exception
            '失敗の場合
            mConnectionJHS.Rollback()
            Throw ex
        End Try
    End Sub
#End Region

#Region "SQL文"
    ''' <summary>
    ''' Ｅａｒｔｈのデータを取得する
    ''' </summary>
    ''' <param name="objConnection">DB接続</param>
    ''' <param name="strBeginMonth">対象開始年度</param>
    ''' <param name="strEndMonth">対象終了年度</param>
    ''' <returns>年度比率管理データ</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2013/01/15 大連/楊双 新規作成 P-45026</para>
    ''' </history>
    Private Function SelEarthData(ByVal objConnection As SqlExecutor, _
                                  ByVal strBeginMonth As String, _
                                  ByVal strEndMonth As String) As Data.DataTable
        'SQLコメント
        Dim sqlBuffer As New System.Text.StringBuilder

        'バラメタ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        Dim strKikan As String
        strKikan = "between '" & strBeginMonth & "' and '" & strEndMonth & "'"

        '2013/10/23 李宇追加　↓
        '区分
        Dim strKubun As String
        strKubun = Definition.GetKubunName5

        '商品種別
        Dim strSyouhinSyubetu As String
        strSyouhinSyubetu = Definition.GetSyouhinSyubetuName5
        '2013/10/23 李宇追加　↑
        Try
            'SQL文	
            'With sqlBuffer
            '    .AppendLine("select  ")
            '    .AppendLine("  k.kameiten_cd 加盟店ｺｰﾄﾞ   ")
            '    .AppendLine("  ,k.kameiten_mei1 加盟店名   ")
            '    .AppendLine("  ,case when isnull(usum_eigyo.urigaku,0)=0 then ")
            '    .AppendLine("	   0  ")
            '    .AppendLine("   else  ")
            '    .AppendLine("	   case when convert(float,isnull(usum.urigaku,0))/isnull(usum_eigyo.urigaku,0) >= 1 then ")
            '    .AppendLine("	       100 ")
            '    .AppendLine("	   else  ")
            '    .AppendLine("	       round(convert(float,isnull(usum.urigaku,0))/isnull(usum_eigyo.urigaku,0)*100,1)  ")
            '    .AppendLine("	   end ")
            '    .AppendLine("   end 売上比率営業マン   ")
            '    .AppendLine("  ,case when isnull(kaiseki_cnt.cnt,0)=0 then ")
            '    .AppendLine("       0  ")
            '    .AppendLine("   else  ")
            '    .AppendLine("	   case when convert(float,isnull(koj_cnt.cnt,0))/isnull(kaiseki_cnt.cnt,0) >= 1 then ")
            '    .AppendLine("	       100 ")
            '    .AppendLine("	   else  ")
            '    .AppendLine("	       round(convert(float,isnull(koj_cnt.cnt,0))/isnull(kaiseki_cnt.cnt,0)*100,1)  ")
            '    .AppendLine("	   end ")
            '    .AppendLine("   end 工事判定率   ")
            '    .AppendLine("  ,case when isnull(koj_cnt.cnt,0)=0 then  ")
            '    .AppendLine("	   0  ")
            '    .AppendLine("   else  ")
            '    .AppendLine("	   case when convert(float,isnull(JHS_koj_cnt.cnt,0)+isnull(tyoku_koj_cnt.cnt,0))/isnull(koj_cnt.cnt,0) >= 1 then ")
            '    .AppendLine("	       100 ")
            '    .AppendLine("	   else  ")
            '    .AppendLine("	       round(convert(float,isnull(JHS_koj_cnt.cnt,0)+isnull(tyoku_koj_cnt.cnt,0))/isnull(koj_cnt.cnt,0)*100,1)  ")
            '    .AppendLine("	   end ")
            '    .AppendLine("   end 工事受注率   ")
            '    .AppendLine("  ,case when isnull(JHS_koj_cnt.cnt,0)+isnull(tyoku_koj_cnt.cnt,0)=0 then  ")
            '    .AppendLine("	   0  ")
            '    .AppendLine("   else  ")
            '    .AppendLine("	   case when convert(float,isnull(tyoku_koj_cnt.cnt,0))/(isnull(JHS_koj_cnt.cnt,0)+isnull(tyoku_koj_cnt.cnt,0)) >= 1 then ")
            '    .AppendLine("	       100 ")
            '    .AppendLine("	   else  ")
            '    .AppendLine("	       round(convert(float,isnull(tyoku_koj_cnt.cnt,0))/(isnull(JHS_koj_cnt.cnt,0)+isnull(tyoku_koj_cnt.cnt,0))*100,1)  ")
            '    .AppendLine("	   end ")
            '    .AppendLine("   end 直工事率   ")
            '    .AppendLine("from m_kameiten k with(readuncommitted)   ")
            '    '/*2011年度・対象商品の売上金額を加盟店ごとに集計*/
            '    .AppendLine("/*2011年度・対象商品の売上金額を加盟店ごとに集計*/   ")
            '    .AppendLine("left join (   ")
            '    .AppendLine("	select kameiten_cd,sum(uri_gaku) urigaku from t_uriage_data u with(readuncommitted)    ")
            '    .AppendLine("	inner join m_syouhin s with(readuncommitted) on u.syouhin_cd=s.syouhin_cd   ")
            '    .AppendLine("	where denpyou_uri_date " & strKikan & "  ")
            '    .AppendLine("	and isnull(s.syouhin_syubetu1,'')<>''    ")
            '    .AppendLine("	group by kameiten_cd   ")
            '    .AppendLine(") usum on k.kameiten_cd=usum.kameiten_cd   ")
            '    '/*2011年度・対象商品の売上金額を営業担当ごとに集計
            '    '（営業担当が同じ加盟店には同じ値がセットされる・「売上比率営業マン」の計算に利用）*/
            '    .AppendLine("/*2011年度・対象商品の売上金額を営業担当ごとに集計 ")
            '    .AppendLine("（営業担当が同じ加盟店には同じ値がセットされる・「売上比率営業マン」の計算に利用）*/   ")
            '    .AppendLine("left join (   ")
            '    .AppendLine("	select eigyou_tantousya_mei,sum(uri_gaku) urigaku from t_uriage_data u with(readuncommitted)    ")
            '    .AppendLine("	inner join m_syouhin s with(readuncommitted) on u.syouhin_cd=s.syouhin_cd   ")
            '    .AppendLine("	inner join m_kameiten k with(readuncommitted) on u.kameiten_cd=k.kameiten_cd   ")
            '    .AppendLine("	where denpyou_uri_date " & strKikan & "   ")
            '    .AppendLine("	and isnull(s.syouhin_syubetu1,'')<>''    ")
            '    .AppendLine("	group by eigyou_tantousya_mei   ")
            '    .AppendLine(") usum_eigyo on k.eigyou_tantousya_mei=usum_eigyo.eigyou_tantousya_mei   ")
            '    '/*2011年度・工事判定率計算利用商品の売上件数（特定判定物件を除く）を加盟店ごとに集計
            '    '（「工事判定率」の計算に利用）*/
            '    .AppendLine("/*2011年度・工事判定率計算利用商品の売上件数（特定判定物件を除く）を加盟店ごとに集計 ")
            '    .AppendLine("（「工事判定率」の計算に利用）*/   ")
            '    .AppendLine("left join (   ")
            '    .AppendLine("	select j.kameiten_cd,count(j.kbn) cnt from t_jiban j with(readuncommitted)    ")
            '    .AppendLine("	inner join t_teibetu_seikyuu t with(readuncommitted) on j.kbn=t.kbn and j.hosyousyo_no=t.hosyousyo_no   ")
            '    .AppendLine("	inner join m_syouhin s with(readuncommitted) on t.syouhin_cd=s.syouhin_cd    ")
            '    '2013/10/23 李宇修正　↓
            '    .AppendLine("	where isnull(s.syouhin_syubetu1,'') in (" & strSyouhinSyubetu & ")    ")
            '    '2013/10/23 李宇修正　↑
            '    .AppendLine("	and t.uri_date " & strKikan & "     ")
            '    .AppendLine("	and hantei_cd1 not in(97,113,1635)   ")
            '    .AppendLine("	group by j.kameiten_cd   ")
            '    .AppendLine(") kaiseki_cnt on k.kameiten_cd=kaiseki_cnt.kameiten_cd   ")
            '    '/*2011年度・工事判定率計算利用商品・工事判定結果FLG=1の売上件数を加盟店ごとに集計
            '    '（「工事判定率」、「工事受注率」の計算に利用）*/
            '    .AppendLine("/*2011年度・工事判定率計算利用商品・工事判定結果FLG=1の売上件数を加盟店ごとに集計 ")
            '    .AppendLine("（「工事判定率」、「工事受注率」の計算に利用）*/   ")
            '    .AppendLine("left join (   ")
            '    .AppendLine("	select j.kameiten_cd,count(j.kbn) cnt from t_jiban j with(readuncommitted)    ")
            '    .AppendLine("	inner join t_teibetu_seikyuu t with(readuncommitted) on j.kbn=t.kbn and j.hosyousyo_no=t.hosyousyo_no   ")
            '    .AppendLine("	inner join m_syouhin s with(readuncommitted) on t.syouhin_cd=s.syouhin_cd    ")
            '    '2013/10/23 李宇修正　↓
            '    .AppendLine("	where isnull(s.syouhin_syubetu1,'') in (" & strSyouhinSyubetu & ")    ")
            '    '2013/10/23 李宇修正　↑
            '    .AppendLine("	and t.uri_date " & strKikan & "     ")
            '    .AppendLine("	and j.koj_hantei_kekka_flg=1   ")
            '    .AppendLine("	group by j.kameiten_cd   ")
            '    .AppendLine(") koj_cnt on k.kameiten_cd=koj_cnt.kameiten_cd   ")
            '    '/*2011年度・工事対象商品（分類ｺｰﾄﾞ130）の売上件数を加盟店ごとに集計
            '    '（「工事受注率」、「直工事率」の計算に利用）*/
            '    .AppendLine("/*2011年度・工事対象商品（分類ｺｰﾄﾞ130）の売上件数を加盟店ごとに集計 ")
            '    .AppendLine("（「工事受注率」、「直工事率」の計算に利用）*/   ")
            '    .AppendLine("left join (   ")
            '    .AppendLine("	select j.kameiten_cd,count(j.kbn) cnt from t_jiban j with(readuncommitted)    ")
            '    .AppendLine("	inner join t_teibetu_seikyuu t with(readuncommitted) on j.kbn=t.kbn and j.hosyousyo_no=t.hosyousyo_no   ")
            '    .AppendLine("	inner join m_syouhin s with(readuncommitted) on t.syouhin_cd=s.syouhin_cd    ")
            '    .AppendLine("	where t.bunrui_cd='130'  ")
            '    .AppendLine("	and isnull(s.syouhin_syubetu1,'') = 'Ke2001'    ")
            '    .AppendLine("	and t.uri_date " & strKikan & "    ")
            '    .AppendLine("	group by j.kameiten_cd   ")
            '    .AppendLine(") JHS_koj_cnt on k.kameiten_cd=JHS_koj_cnt.kameiten_cd   ")
            '    '/*2011年度・工事対象商品（分類ｺｰﾄﾞ130）の売上件数を加盟店ごとに集計
            '    '（「工事受注率」、「直工事率」の計算に利用）*/
            '    .AppendLine("/*2011年度・工事対象商品（分類ｺｰﾄﾞ130）の売上件数を加盟店ごとに集計 ")
            '    .AppendLine("（「工事受注率」、「直工事率」の計算に利用）*/   ")
            '    .AppendLine("left join (   ")
            '    .AppendLine("	select j.kameiten_cd,count(j.kbn) cnt from t_jiban j with(readuncommitted)    ")
            '    .AppendLine("	inner join t_teibetu_seikyuu t with(readuncommitted) on j.kbn=t.kbn and j.hosyousyo_no=t.hosyousyo_no   ")
            '    .AppendLine("	inner join m_syouhin s with(readuncommitted) on t.syouhin_cd=s.syouhin_cd    ")
            '    .AppendLine("	where t.bunrui_cd='130'  ")
            '    .AppendLine("	and isnull(s.syouhin_syubetu1,'') = 'Ke2002'    ")
            '    .AppendLine("	and t.uri_date " & strKikan & " ")
            '    .AppendLine("	group by j.kameiten_cd   ")
            '    .AppendLine(") tyoku_koj_cnt on k.kameiten_cd=tyoku_koj_cnt.kameiten_cd   ")
            '    '2013/10/23 李宇修正　↓
            '    .AppendLine("where k.kbn in (" & strKubun & ") ")
            '    '2013/10/23 李宇修正　↑
            '    .AppendLine("/*系列'TAMA','REOH','LEOP','ACEH','0001','6100','6800'はE・G・Sしかないので今はこれでOK ")
            '    .AppendLine("（今後は以外の系列も追加になる可能性有なので注意）*/   ")
            'End With

            Dim strSql As String = String.Empty
            strSql = Definition.GetSqlString(Definition.SqlStringKbn.S0005_01)
            strSql = strSql.Replace("@strKikan", strKikan)
            strSql = strSql.Replace("@strKubun", strKubun)
            sqlBuffer.AppendLine(strSql)

            'Ｅａｒｔｈのデータ
            Return objConnection.ExecuteDataTable(CommandType.Text, sqlBuffer.ToString(), paramList.ToArray())

        Catch ex As Exception
            If IsNothing(ex.Data.Item("ERROR_LOG")) Then
                ex.Data.Add("ERROR_LOG", "Ｅａｒｔｈの年度比率管理データの取得処理が異常終了しました。")
            End If

            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' JHSのデータを削除する
    ''' </summary>
    ''' <param name="objConnection">DB接続</param>
    ''' <param name="strYear">対象年度</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2013/01/15 大連/楊双 新規作成 P-45026</para>
    ''' </history>
    Private Sub DelJHSData(ByVal objConnection As SqlExecutor, _
                           ByVal strYear As String)

        'SQLコメント
        Dim sqlBuffer As New System.Text.StringBuilder

        'バラメタ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        Try
            'SQL文	
            'With sqlBuffer
            '    .AppendLine(" delete from ")
            '    .AppendLine("     t_nendo_hiritu_kanri ")
            '    .AppendLine(" where keikaku_nendo = @year ")
            'End With

            Dim strSql As String = String.Empty
            strSql = Definition.GetSqlString(Definition.SqlStringKbn.S0005_02)
            sqlBuffer.AppendLine(strSql)

            'バラメタ
            paramList.Add(MakeParam("@year", SqlDbType.VarChar, 4, strYear))      '年度

            objConnection.ExecuteNonQuery(CommandType.Text, sqlBuffer.ToString(), paramList.ToArray())

        Catch ex As Exception
            If IsNothing(ex.Data.Item("ERROR_LOG")) Then
                ex.Data.Add("ERROR_LOG", strYear & "年度比率管理のデータのクリア処理が異常終了しました。")
            End If

            Throw ex
        End Try

    End Sub

    ''' <summary>
    ''' JHSのデータを登録する
    ''' </summary>
    ''' <param name="objConnection">DB接続</param>
    ''' <param name="drData">登録データ</param>
    ''' <param name="strYear">年度</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2013/01/15 大連/楊双 新規作成 P-45026</para>
    ''' </history>
    Private Function InsJHSData(ByVal objConnection As SqlExecutor, _
                                ByVal drData As DataRow, _
                                ByVal strYear As String) As Integer

        'SQLコメント
        Dim sqlBuffer As New System.Text.StringBuilder

        'バラメタ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        Try

            'SQL文	
            'With sqlBuffer
            '    .AppendLine(" INSERT INTO t_nendo_hiritu_kanri WITH(UPDLOCK) ( ")     '年度比率管理テーブル           
            '    .AppendLine("	keikaku_nendo ")                        '計画_年度
            '    .AppendLine("	,kameiten_cd ")                         '加盟店ｺｰﾄﾞ
            '    .AppendLine("	,kameiten_mei ")                        '加盟店名
            '    .AppendLine("	,uri_hiritu ")                          '売上比率
            '    .AppendLine("	,koj_hantei_ritu ")                     '工事判定率
            '    .AppendLine("	,koj_jyuchuu_ritu ")                    '工事受注率
            '    .AppendLine("	,tyoku_koj_ritu ")                      '直工事率
            '    .AppendLine("	,add_login_user_id ")                   '登録ログインユーザーID
            '    .AppendLine("	,add_datetime ")                        '登録日時
            '    .AppendLine("	,upd_login_user_id ")                   '更新ログインユーザーID
            '    .AppendLine("	,upd_datetime ")                        '更新日時
            '    .AppendLine(" ) ")
            '    .AppendLine(" VALUES ( ")
            '    .AppendLine("	@keikaku_nendo ")                       '計画_年度
            '    .AppendLine("	,@kameiten_cd ")                        '加盟店ｺｰﾄﾞ
            '    .AppendLine("	,@kameiten_mei ")                       '加盟店名
            '    .AppendLine("	,@uri_hiritu ")                         '売上比率
            '    .AppendLine("	,@koj_hantei_ritu ")                    '工事判定率
            '    .AppendLine("	,@koj_jyuchuu_ritu ")                   '工事受注率
            '    .AppendLine("	,@tyoku_koj_ritu ")                     '直工事率
            '    .AppendLine("	,@add_login_user_id ")                                               '登録ログインユーザーID
            '    .AppendLine("	,GETDATE()  ")                                                       '登録日時
            '    .AppendLine("	,null ")                                                             '更新ログインユーザーID
            '    .AppendLine("	,null  ")                                                            '更新日時
            '    .AppendLine(" ); ")

            'End With

            Dim strSql As String = String.Empty
            strSql = Definition.GetSqlString(Definition.SqlStringKbn.S0005_03)
            sqlBuffer.AppendLine(strSql)

            'バラメタ
            paramList.Add(MakeParam("@keikaku_nendo", SqlDbType.VarChar, 4, strYear))    '計画_年度
            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 8, drData.Item("加盟店ｺｰﾄﾞ").ToString)) '加盟店ｺｰﾄﾞ
            paramList.Add(MakeParam("@kameiten_mei", SqlDbType.VarChar, 40, drData.Item("加盟店名").ToString))   '加盟店名
            paramList.Add(MakeParam("@uri_hiritu", SqlDbType.VarChar, 5, drData.Item("売上比率営業マン").ToString))      '売上比率
            paramList.Add(MakeParam("@koj_hantei_ritu", SqlDbType.VarChar, 5, drData.Item("工事判定率").ToString))    '工事判定率
            paramList.Add(MakeParam("@koj_jyuchuu_ritu", SqlDbType.VarChar, 5, drData.Item("工事受注率").ToString)) '工事受注率
            paramList.Add(MakeParam("@tyoku_koj_ritu", SqlDbType.VarChar, 5, drData.Item("直工事率").ToString)) '直工事率
            paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, CON_BATCH_ID)) '登録ログインユーザーID

            Return objConnection.ExecuteNonQuery(CommandType.Text, sqlBuffer.ToString(), paramList.ToArray())


        Catch ex As Exception
            If IsNothing(ex.Data.Item("ERROR_LOG")) Then
                ex.Data.Add("ERROR_LOG", strYear & "年度比率管理のデータの挿入処理が異常終了しました。")
            End If

            Throw ex

        End Try

    End Function

#End Region

End Class


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
''' <remarks>JHS Earth ｼｽﾃﾑ の 加盟店マスタ　の内容　を
'''          計画管理_加盟店マスタ　用に加工して　登録する</remarks>
''' <history>
''' <para>2013/1/15 大連/楊双 新規作成 P-45026</para>
''' </history>
Public Class S0003

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
        Dim btProcess As S0003

        '初期化
        btProcess = New S0003()

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
            "計画管理_加盟店マスタテーブルに合計：" & _
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

        Dim strYear As String                   '計画年度
        Dim dtEarthData As DataTable            'Ｅａｒｔｈデータ
        Dim dtAspsfaData As DataTable            '報連相データ
        Dim options As New Transactions.TransactionOptions
        Dim j As Integer

        'バッチラン時間
        strYear = Convert.ToString(DateAdd(DateInterval.Month, -3, Now).Year)
        'strYear = Now.Year.ToString

        mLogMsg.AppendLine(Format(Date.Now, "yyyy年MM月dd日 HH:mm:ss  ") & _
                            "Ｅａｒｔｈの計画管理_加盟店データの取得処理を開始しました。")

        'DB接続のオープン
        mConnectionEarth.Open()
        mConnectionJHS.Open(True)

        Try
            'Ｅａｒｔｈの計画管理_加盟店データを取得する
            dtEarthData = SelEarthData(mConnectionEarth, strYear)

            If dtEarthData IsNot Nothing Then
                mLogMsg.AppendLine(Format(Date.Now, "yyyy年MM月dd日 HH:mm:ss  ") & _
                                    "Ｅａｒｔｈのデータから" & _
                                    Convert.ToString(dtEarthData.Rows.Count) & _
                                    "件データが取得されました。")
            End If

            '2013/10/10 李宇追加　↓
            mLogMsg.AppendLine(Format(Date.Now, "yyyy年MM月dd日 HH:mm:ss  ") & _
                              "報連相の計画管理_加盟店データの取得処理を開始しました。")

            '報連相のデータを取得する
            dtAspsfaData = SelASPSFAData()

            If dtAspsfaData IsNot Nothing Then
                mLogMsg.AppendLine(Format(Date.Now, "yyyy年MM月dd日 HH:mm:ss  ") & _
                                    "報連相のデータから" & _
                                    Convert.ToString(dtAspsfaData.Rows.Count) & _
                                    "件データが取得されました。")
            End If
            '2013/10/10 李宇追加　↑


            '該当年度のデータを削除する
            DelJHSData(mConnectionJHS, strYear)

            Dim strKameiten As String = String.Empty
            Dim dtKameiten As Data.DataTable
            Dim intCount As Integer = 0
            Dim i As Integer
            dtKameiten = SelKeikakuKameiten(mConnectionJHS, strYear)
            '計画値不変の加盟店の取得
            For i = 0 To dtKameiten.Rows.Count - 1
                strKameiten = strKameiten & "," & dtKameiten.Rows(i).Item(0).ToString
            Next

            Dim dtEigyouKbn As Data.DataTable
            Dim blnEigyouKbn As Boolean
            dtEigyouKbn = SelKeikakuyouMeisyou(mConnectionJHS)

            'Ｅａｒｔｈの計画管理_加盟店データにより、ループする
            For j = 0 To dtEarthData.Rows.Count - 1
                blnEigyouKbn = False
                For i = 0 To dtEigyouKbn.Rows.Count - 1
                    If dtEarthData.Rows(j).Item("営業区分").ToString = dtEigyouKbn.Rows(i).Item("meisyou").ToString Then
                        '営業区分のセット
                        dtEarthData.Columns("営業区分").ReadOnly = False
                        dtEarthData.Rows(j).Item("営業区分") = dtEigyouKbn.Rows(i).Item("code").ToString
                        blnEigyouKbn = True
                        Exit For
                    End If
                Next

                If strKameiten.IndexOf(dtEarthData.Rows(j).Item("加盟店ｺｰﾄﾞ").ToString) = -1 AndAlso blnEigyouKbn Then

                    dtEarthData.Columns("計画値0FLG").ReadOnly = False
                    '加盟店が計画値不変の加盟店じゃない場合、挿入する
                    SetJHSData(dtEarthData.Rows(j), strYear)
                    intCount = intCount + 1
                End If

            Next

            '2013/10/10 李宇修正　↓
            '新規件数をログファイルに出力する
            mLogMsg.AppendLine(Format(Date.Now, "yyyy年MM月dd日 HH:mm:ss  ") & _
            "計画管理_加盟店マスタテーブルに" & _
            Convert.ToString(intCount) & _
            "件Ｅａｒｔｈのデータが挿入されました。")
            '2013/10/10 李宇修正　↑

            '2013/10/10 李宇削除　↓
            'mLogMsg.AppendLine(Format(Date.Now, "yyyy年MM月dd日 HH:mm:ss  ") & _
            '    "報連相の計画管理_加盟店データの取得処理を開始しました。")

            ''報連相のデータを取得する
            'dtAspsfaData = SelASPSFAData()

            'If dtAspsfaData IsNot Nothing Then
            '    mLogMsg.AppendLine(Format(Date.Now, "yyyy年MM月dd日 HH:mm:ss  ") & _
            '                        "報連相のデータから" & _
            '                        Convert.ToString(dtAspsfaData.Rows.Count) & _
            '                        "件データが取得されました。")
            'End If
            '2013/10/10 李宇削除　↑

            intCount = 0
            For j = 0 To dtAspsfaData.Rows.Count - 1

                If SelKeikakuKameitenCount(mConnectionJHS, strYear, dtAspsfaData.Rows(j).Item("加盟店ｺｰﾄﾞ").ToString).Rows(0).Item(0).Equals(0) Then
                    'Ｅａｒｔｈ加盟店ﾏｽﾀ　にない 
                    '報連相システムの　加盟店ﾏｽﾀ　の情報を　計画管理用の加盟店ﾏｽﾀを作成する
                    dtAspsfaData.Rows(j).Item("登録日時") = SetDateTime(dtAspsfaData.Rows(j).Item("登録日時").ToString)
                    dtAspsfaData.Rows(j).Item("更新日時") = SetDateTime(dtAspsfaData.Rows(j).Item("更新日時").ToString)
                    SetJHSData(dtAspsfaData.Rows(j), strYear)
                    intCount = intCount + 1

                End If

            Next

            '2013/10/10 李宇修正　↓
            '新規件数をログファイルに出力する
            mLogMsg.AppendLine(Format(Date.Now, "yyyy年MM月dd日 HH:mm:ss  ") & _
            "計画管理_加盟店マスタテーブルに" & _
            Convert.ToString(intCount) & _
            "件報連相のデータが挿入されました。")
            '2013/10/10 李宇修正　↑

            'データを解放する
            If dtEarthData IsNot Nothing Then
                dtEarthData.Dispose()
                dtEarthData = Nothing
            End If

            If dtAspsfaData IsNot Nothing Then
                dtAspsfaData.Dispose()
                dtAspsfaData = Nothing
            End If

            '成功の場合
            mConnectionJHS.Commit()

        Catch ex As Exception
            '失敗の場合
            mConnectionJHS.Rollback()
            Throw ex
        End Try
        'End Using
    End Sub
#End Region

#Region "データ編集処理"

    ''' <summary>
    ''' データ編集処理
    ''' </summary>
    ''' <param name="drData">Ｅａｒｔｈのデータ</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2013/01/15 大連/楊双 新規作成 P-45026</para>
    ''' </history>
    Private Sub SetJHSData(ByVal drData As DataRow, ByVal strYear As String)

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(mMyNamePeriod & MyMethod.GetCurrentMethod.Name, drData)

        Dim i As Integer
        Dim dtKeikakuKensuu As Data.DataTable
        dtKeikakuKensuu = SelKeikakuKanri(mConnectionJHS, strYear, drData.Item("加盟店ｺｰﾄﾞ").ToString)

        Dim intKeikaku0Flg As Integer = 1
        For i = 0 To dtKeikakuKensuu.Rows.Count - 1
            '計画値0FLGのセット
            If Convert.ToInt32(dtKeikakuKensuu.Rows(i).Item(0).ToString) > 0 Then
                intKeikaku0Flg = 0
                Exit For
            End If
        Next

        '計画値0FLGのセット
        drData.Item("計画値0FLG") = intKeikaku0Flg.ToString

        '受注データワーク新規処理
        mInsCount = mInsCount + InsJHSData(mConnectionJHS, drData, strYear)

    End Sub

#End Region

#Region "SQL文"
    ''' <summary>
    ''' Ｅａｒｔｈのデータを取得する
    ''' </summary>
    ''' <param name="objConnection">DB接続</param>
    ''' <param name="strYear">対象年度</param>
    ''' <returns>計画管理_加盟店データ</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2013/01/15 大連/楊双 新規作成 P-45026</para>
    ''' </history>
    Private Function SelEarthData(ByVal objConnection As SqlExecutor, _
                                  ByVal strYear As String) As Data.DataTable
        'SQLコメント
        Dim sqlBuffer As New System.Text.StringBuilder

        'バラメタ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        '開始年度
        Dim strBeginYear As String
        Dim strEndYear As String
        strBeginYear = strYear & "0401"
        strEndYear = DateAdd(DateInterval.Year, 1, Convert.ToDateTime(strYear & "/03/31")).ToString("yyyyMMdd")
        '系列コード
        Dim strKeiretuCd As String
        strKeiretuCd = Definition.GetKeiretuCd

        '2013/10/23 李宇追加　↓
        '区分
        Dim strKubun As String
        strKubun = Definition.GetKubunName3
        '2013/10/23 李宇追加　↑
        Try
            'SQL文	
            'With sqlBuffer
            '    .AppendLine(" SELECT")
            '    .AppendLine(" 	k.kameiten_cd 加盟店ｺｰﾄﾞ ")
            '    .AppendLine(" 	,k.torikesi 取消 ")
            '    .AppendLine(" 	,k.hattyuu_teisi_flg 発注停止FLG ")
            '    .AppendLine(" 	,CASE WHEN k.kbn in ('A','C') THEN --区分が　A、C　の場合   「FC」 ")
            '    .AppendLine(" 		'FC' ")
            '    .AppendLine(" 	ELSE ")
            '    '2013/10/10 李宇削除　↓
            '    '.AppendLine(" 		CASE WHEN CONVERT(varchar(100), k.add_datetime, 112) <= @endYear AND CONVERT(varchar(100), k.add_datetime, 112) >= @beginYear  ")
            '    '.AppendLine(" 				  OR replace(KJ.add_nengetu,'/','')+'01' <= @endYear AND replace(KJ.add_nengetu,'/','')+'01' >= @beginYear THEN ")
            '    '.AppendLine(" 			'新規' ")
            '    '.AppendLine(" 		ELSE ")
            '    '.AppendLine(" 			case when k.keiretu_cd in ('TAMA','REOH','LEOP','ACEH','0001','6100','6800') then  ")
            '    '2013/10/10 李宇削除　↑
            '    .AppendLine(" 			case when k.keiretu_cd in (" & strKeiretuCd & ") then  ")
            '    .AppendLine(" 				'特販'  ")
            '    .AppendLine(" 			else  ")
            '    '2013/10/23 李宇修正　↓
            '    .AppendLine(" 				CASE WHEN k.kbn in (" & strKubun & ") THEN ")
            '    '2013/10/23 李宇修正　↓
            '    .AppendLine(" 					'営業'  ")
            '    .AppendLine(" 				END ")
            '    .AppendLine(" 			end  ")
            '    '2013/10/10 李宇削除　↓
            '    '.AppendLine(" 		END ")
            '    '2013/10/10 李宇削除　↑
            '    .AppendLine(" 	END 営業区分 ")
            '    .AppendLine(" 	,k.kameiten_mei1 加盟店名 ")
            '    .AppendLine(" 	,case when b1.sosiki_level=4 then b1.busyo_mei else b2.busyo_mei end 支店名/*直轄部署が支店ではないときは、上位部署を取得*/ ")
            '    .AppendLine(" 	,case when b1.sosiki_level=4 then b1.busyo_cd else b2.busyo_cd end 部署ｺｰﾄﾞ ")
            '    .AppendLine(" 	,k.eigyou_tantousya_mei 営業担当者  ")
            '    .AppendLine(" 	,mb.displayname 営業担当者名 ")
            '    .AppendLine(" 	,NULL 必要面談回数 ")
            '    .AppendLine(" 	,case when b1.sosiki_level=5 then b1.busyo_cd else null end 営業所_部署ｺｰﾄﾞ ")
            '    .AppendLine(" 	,case when b1.sosiki_level=5 then b1.busyo_mei else null end 営業所名/*直轄部署が支店ではないときは、nullをセット*/ ")
            '    .AppendLine(" 	,k.todouhuken_cd 都道府県ｺｰﾄﾞ ")
            '    .AppendLine(" 	,f.todouhuken_mei 都道府県名 ")
            '    .AppendLine(" 	,k.keiretu_cd 系列ｺｰﾄﾞ ")
            '    .AppendLine(" 	,ke.keiretu_mei 系列名 ")
            '    .AppendLine(" 	,k.eigyousyo_cd 営業所ｺｰﾄﾞ ")
            '    .AppendLine(" 	,k.nenkan_tousuu 年間棟数 ")
            '    .AppendLine(" 	,NULL 前年実績_年間棟数 ")
            '    .AppendLine(" 	,null 計画値0FLG ")
            '    .AppendLine(" 	,KJ.add_nengetu 加盟店登録年月 ")
            '    '2013/10/10 李宇修正　↓
            '    '.AppendLine(" 	,CASE WHEN CONVERT(varchar(100), k.add_datetime, 112) <= @endYear AND CONVERT(varchar(100), k.add_datetime, 112) >= @beginYear THEN ")
            '    '.AppendLine(" 		'1' ")
            '    '.AppendLine(" 	ELSE ")
            '    '.AppendLine(" 		'0'  ")
            '    '.AppendLine(" 	END 加盟店新規FLG ")
            '    .AppendLine("	,CASE WHEN KJ.add_nengetu IS NULL THEN")
            '    .AppendLine("		NULL")
            '    .AppendLine("	ELSE")
            '    .AppendLine("		CASE WHEN (REPLACE(KJ.add_nengetu,'/','') + '01') BETWEEN @beginYear AND @endYear THEN ")
            '    .AppendLine("			'1'  ")
            '    .AppendLine("		ELSE  ")
            '    .AppendLine("			'0'  ")
            '    .AppendLine("		END ")
            '    .AppendLine("	END 加盟店新規FLG  ")
            '    '2013/10/10 李宇修正　↑
            '    .AppendLine(" 	,isnull(tmax.naiyou,'') 業態 ")
            '    .AppendLine(" 	,null 分譲上位50社FLG ")
            '    .AppendLine(" 	,null 注文上位50社FLG ")
            '    .AppendLine(" 	,null 併売上位50社FLG ")
            '    .AppendLine(" 	,0 計画値不変FLG ")
            '    .AppendLine(" 	,k.add_login_user_id 登録ID ")
            '    .AppendLine(" 	,k.add_datetime 登録日時 ")
            '    .AppendLine(" 	,k.upd_login_user_id 更新ID ")
            '    .AppendLine(" 	,k.upd_datetime 更新日時 ")
            '    .AppendLine(" from m_kameiten k with(readuncommitted) ")
            '    .AppendLine(" LEFT JOIN m_kameiten_jyuusyo KJ  ")
            '    .AppendLine(" 	ON KJ.kameiten_cd = K.kameiten_cd ")
            '    .AppendLine(" 	AND KJ.jyuusyo_no = '1' ")
            '    .AppendLine(" LEFT JOIN m_todoufuken f  ")
            '    .AppendLine(" 	ON f.todouhuken_cd = k.todouhuken_cd --都道府県マスタ ")
            '    .AppendLine(" LEFT JOIN m_busyo_kanri b1  ")
            '    .AppendLine(" 	ON b1.busyo_cd = f.busyo_cd /*都道府県の直轄部署*/ ")
            '    .AppendLine(" LEFT JOIN m_busyo_kanri b2  ")
            '    .AppendLine(" 	ON b2.busyo_cd = b1.joui_soiki /*直轄部署の上位組織部署（直轄部署が支店ではない場合、支店が取得できる）*/ ")
            '    .AppendLine(" LEFT JOIN m_jhs_mailbox mb with(readuncommitted)  ")
            '    .AppendLine(" 	ON k.eigyou_tantousya_mei=mb.aliasname ")
            '    .AppendLine(" LEFT JOIN m_keiretu ke --系列マスタ ")
            '    .AppendLine(" 	ON ke.kbn = k.kbn ")
            '    .AppendLine(" 	AND ke.keiretu_cd = k.keiretu_cd  ")
            '    .AppendLine(" /*加盟店注意事項マスタの注意事項種別61の最大入力NOの内容取得。100以下・100超注文・100超分譲・HMが取得できる*/ ")
            '    .AppendLine(" LEFT JOIN ( ")
            '    .AppendLine(" 	 select t.kameiten_cd,naiyou from m_kameiten_tyuuijikou t with(readuncommitted)  ")
            '    .AppendLine(" 	 inner join ( ")
            '    .AppendLine(" 		 select kameiten_cd,max(nyuuryoku_no) maxno from m_kameiten_tyuuijikou with(readuncommitted)  ")
            '    .AppendLine(" 		 where tyuuijikou_syubetu='61' group by kameiten_cd ")
            '    .AppendLine(" 	 ) t61 on t.kameiten_cd=t61.kameiten_cd and t.nyuuryoku_no=t61.maxno ")
            '    .AppendLine(" ) tmax on k.kameiten_cd=tmax.kameiten_cd ")
            '    .AppendLine(" where ")
            '    .AppendLine(" 	    k.kbn in ('A','C') /*FCの場合*/ ")
            '    '2013/10/10 李宇削除　↓
            '    '.AppendLine(" 		OR CONVERT(varchar(100), k.add_datetime, 112) <= @endYear AND CONVERT(varchar(100), k.add_datetime, 112) >= @beginYear /*新規の場合*/ ")
            '    '.AppendLine(" 		OR replace(KJ.add_nengetu,'/','')+'01' <= @endYear AND replace(KJ.add_nengetu,'/','')+'01' >= @beginYear /*新規の場合*/ ")
            '    '2013/10/10 李宇削除　↑
            '    '.AppendLine(" 		OR k.keiretu_cd in ('TAMA','REOH','LEOP','ACEH','0001','6100','6800') /*特販の場合*/ ")
            '    .AppendLine(" 		OR k.keiretu_cd in (" & strKeiretuCd & ") /*特販の場合*/ ")
            '    .AppendLine(" 							/*系列'TAMA','REOH','LEOP','ACEH','0001','6100','6800'はE・G・Sしかないので今はこれでOK（今後は以外の系列も追加になる可能性有なので注意）*/ ")
            '    '2013/10/23 李宇修正　↓
            '    .AppendLine(" 		OR k.kbn in (" & strKubun & ") /*営業の場合*/ ")
            '    '2013/10/23 李宇修正　↑
            'End With

            Dim strSql As String = String.Empty
            strSql = Definition.GetSqlString(Definition.SqlStringKbn.S0003_01)
            strSql = strSql.Replace("@strKeiretuCd", strKeiretuCd)
            strSql = strSql.Replace("@strKubun", strKubun)
            sqlBuffer.AppendLine(strSql)

            'バラメタ
            paramList.Add(MakeParam("@beginYear", SqlDbType.VarChar, 8, strBeginYear))      '開始年
            paramList.Add(MakeParam("@endYear", SqlDbType.VarChar, 8, strEndYear))          '終了年

            'Ｅａｒｔｈのデータ
            Return objConnection.ExecuteDataTable(CommandType.Text, sqlBuffer.ToString(), paramList.ToArray())

        Catch ex As Exception
            If IsNothing(ex.Data.Item("ERROR_LOG")) Then
                ex.Data.Add("ERROR_LOG", "Ｅａｒｔｈのデータの取得処理が異常終了しました。")
            End If
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' 報連相システムのデータを取得する
    ''' </summary>
    ''' <returns>計画管理_加盟店データ</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2013/01/15 大連/楊双 新規作成 P-45026</para>
    ''' </history>
    Private Function SelASPSFAData() As Data.DataTable

        'SQLコメント
        Dim sqlBuffer As New System.Text.StringBuilder
        Dim ds As New Data.DataSet

        Try
            'SQL文	
            'With sqlBuffer
            '    .AppendLine("	SELECT ")
            '    .AppendLine("		SFUM.UCCRPCOD AS 加盟店ｺｰﾄﾞ ")
            '    .AppendLine("	,	SFUM.UCDELFLG AS 取消 ")
            '    .AppendLine("	,	9 AS 発注停止FLG ")
            '    '2013/10/10 李宇修正　↓
            '    .AppendLine("	,	CASE WHEN UCSTROT8 IS NOT NULL THEN ")
            '    .AppendLine("			CASE WHEN SUBSTR(UCSTROT8,-1) IN ('0','1','2','3','4','5','6','7','8','9') THEN")
            '    .AppendLine("               to_char(SUBSTR(UCSTROT8, -1))")
            '    .AppendLine("			ELSE")
            '    .AppendLine("               '0'")
            '    .AppendLine("           End")
            '    .AppendLine("		ELSE")
            '    .AppendLine("           '0'")
            '    .AppendLine("		END AS 営業区分 ")
            '    '.AppendLine("	,	2 AS 営業区分			--「2:新規」固定 ")
            '    '2013/10/10 李宇修正　↑
            '    .AppendLine("	,	SFUM.UCCRPNAM AS 加盟店名 ")
            '    .AppendLine("	,	CASE WHEN SUBSTR(SFOM.OGPSTCOD,1,2) = '04' THEN SFOM.OGPSTNAM ")
            '    .AppendLine("		     ELSE SFOM.SUB_OGPSTNAM END AS 支店名 ")
            '    .AppendLine("	,	CASE WHEN SUBSTR(SFOM.OGPSTCOD,1,2) = '04' THEN SFOM.OGPSTCOD ")
            '    .AppendLine("		     ELSE SFOM.SUB_OGPSTCOD END AS 部署ｺｰﾄﾞ ")
            '    .AppendLine("	,	SOM1.OEUSRNMR AS 営業担当者  ")
            '    .AppendLine("	,	SOM1.OEBASLID AS 営業担当者名 ")
            '    .AppendLine("	,	NULL AS 必要面談回数 ")
            '    .AppendLine("	,	CASE WHEN SUBSTR(SFOM.OGPSTCOD,1,2) = '05' THEN SFOM.OGPSTCOD ")
            '    .AppendLine("		     ELSE NULL END AS 営業所_部署ｺｰﾄﾞ ")
            '    .AppendLine("	,	CASE WHEN SUBSTR(SFOM.OGPSTCOD,1,2) = '05' THEN SFOM.OGPSTNAM ")
            '    .AppendLine("		     ELSE NULL END AS 営業所名 ")
            '    .AppendLine("	,	NULL AS 都道府県ｺｰﾄﾞ ")
            '    .AppendLine("	,	NULL AS 都道府県名 ")
            '    .AppendLine("	,	NULL AS 系列ｺｰﾄﾞ ")
            '    .AppendLine("	,	NULL AS 系列名 ")
            '    .AppendLine("	,	NULL AS 営業所ｺｰﾄﾞ ")
            '    .AppendLine("	,	NULL AS 年間棟数 ")
            '    .AppendLine("	,	NULL AS 前年実績_年間棟数 ")
            '    .AppendLine("	,	NULL AS 計画値0FLG ")
            '    .AppendLine("	,	NULL AS 加盟店登録年月 ")
            '    '2013/10/10 李宇修正　↓
            '    .AppendLine("	,	'1' AS 加盟店新規FLG ")
            '    '.AppendLine("	,	NULL AS 加盟店新規FLG ")
            '    '2013/10/10 李宇修正　↑
            '    .AppendLine("	,	NULL AS 業態 ")
            '    .AppendLine("	,	NULL AS 分譲上位50社FLG ")
            '    .AppendLine("	,	NULL AS 注文上位50社FLG ")
            '    .AppendLine("	,	NULL AS 併売上位50社FLG ")
            '    .AppendLine("	,	NULL AS 登録ID ")
            '    .AppendLine("	,	SFUM.UCMAKDAT AS 登録日時 ")
            '    .AppendLine("	,	NULL AS 更新ID ")
            '    .AppendLine("	,	SFUM.UCDBADAT AS 更新日時 ")
            '    .AppendLine("FROM SFAMT_USRCORP_MVR  SFUM				--報連相の加盟店ﾏｽﾀ ")
            '    .AppendLine("	LEFT JOIN ( ")
            '    .AppendLine("	    SELECT SFOM.OGPSTSEQ,						--部署No ")
            '    .AppendLine("	           SFOM.OGPSTCOD,						--部署コード ")
            '    .AppendLine("	           SFOM.OGPSTNAM,						--部署名 ")
            '    .AppendLine("	           SUB_SFOM.OGPSTCOD AS SUB_OGPSTCOD,	--上位組織の部署コード ")
            '    .AppendLine("	           SUB_SFOM.OGPSTNAM AS SUB_OGPSTNAM	--上位組織の部署名 ")
            '    .AppendLine("	    FROM SFAMT_OWNPOSGRP_MVR  SFOM			--報連相の部署ﾏｽﾀ ")
            '    .AppendLine("	    LEFT JOIN SFAMT_OWNPOSGRP_MVR  SUB_SFOM	--報連相の部署ﾏｽﾀ ")
            '    .AppendLine("	    ON SFOM.OGPHLSEQ = SUB_SFOM.OGPSTSEQ		--上位組織 ")
            '    .AppendLine("	)  SFOM ")
            '    .AppendLine("	ON SFUM.UCPSTSEQ = SFOM.OGPSTSEQ				--部署No ")
            '    .AppendLine("	LEFT JOIN ( ")
            '    .AppendLine("		SELECT MIN(OGEMPSEQ) OGEMPSEQ ")
            '    .AppendLine("			   ,OGCRPSEQ	 ")
            '    .AppendLine("		FROM SFAMT_OWNCSTCHG_MVR ")
            '    .AppendLine("		GROUP BY OGCRPSEQ ")
            '    .AppendLine("		) SOM				--報連相の加盟店_担当者ﾏｽﾀ  ")
            '    .AppendLine("	ON SFUM.UCCRPSEQ = SOM.OGCRPSEQ					--担当紐付けｺｰﾄﾞ  ")
            '    .AppendLine("	LEFT JOIN SFAMT_OWNEMP_MVR SOM1					--報連相の担当者ﾏｽﾀ  ")
            '    .AppendLine("	ON SOM.OGEMPSEQ = SOM1.OEEMPSEQ					--担当者ｺｰﾄﾞ ")
            '    .AppendLine("	WHERE length(nvl(SFUM.UCCRPCOD,'')) = 8	--8桁 で登録のあるコードを取得する ")
            '    '2013/10/10 李宇追加　↓
            '    .AppendLine("         AND SFUM.UCDELFLG = '0'")
            '    '2013/10/10 李宇追加　↑
            'End With

            Dim strSql As String = String.Empty
            strSql = Definition.GetSqlString(Definition.SqlStringKbn.S0003_02)
            sqlBuffer.AppendLine(strSql)

            '報連相のデータ
            ORCLHelper.FillDataset(Definition.GetConnectionStringASPSFA, CommandType.Text, sqlBuffer.ToString, ds, "dtKeikakuKameiten")
            Return ds.Tables("dtKeikakuKameiten")

        Catch ex As Exception
            If IsNothing(ex.Data.Item("ERROR_LOG")) Then
                ex.Data.Add("ERROR_LOG", "報連相のデータの取得処理が異常終了しました。")
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
            '    .AppendLine("     m_keikaku_kameiten ")
            '    .AppendLine(" where keikaku_nendo = @year ")
            '    .AppendLine(" and isnull(keikaku_huhen_flg,0) <> @keikaku_huhen_flg ")

            'End With

            Dim strSql As String = String.Empty
            strSql = Definition.GetSqlString(Definition.SqlStringKbn.S0003_03)
            sqlBuffer.AppendLine(strSql)

            'バラメタ
            paramList.Add(MakeParam("@year", SqlDbType.VarChar, 4, strYear))      '年度
            paramList.Add(MakeParam("@keikaku_huhen_flg", SqlDbType.Int, 1, 1))   '計画値不変FLG

            objConnection.ExecuteNonQuery(CommandType.Text, sqlBuffer.ToString(), paramList.ToArray())

        Catch ex As Exception
            If IsNothing(ex.Data.Item("ERROR_LOG")) Then
                ex.Data.Add("ERROR_LOG", strYear & "計画管理_加盟店マスタのデータのクリア処理が異常終了しました。")
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

        '加盟店のセット
        Dim strKemeiten As String = drData("加盟店名").ToString
        If Encoding.Default.GetByteCount(strKemeiten) > 40 Then
            strKemeiten = Encoding.Default.GetString(Encoding.Default.GetBytes(strKemeiten), 0, 40)
            If strKemeiten.EndsWith("・") Then
                strKemeiten = Encoding.Default.GetString(Encoding.Default.GetBytes(strKemeiten), 0, 39)
            End If

        End If

        Try

            'SQL文	
            'With sqlBuffer
            '    .AppendLine(" INSERT INTO m_keikaku_kameiten WITH(UPDLOCK)( ")          '計画管理_加盟店ﾏｽﾀ           
            '    .AppendLine("    keikaku_nendo ")                          '計画_年度
            '    .AppendLine("    ,kameiten_cd ")                           '加盟店ｺｰﾄﾞ
            '    .AppendLine("    ,torikesi ")                              '取消
            '    .AppendLine("    ,hattyuu_teisi_flg ")                     '発注停止FLG
            '    .AppendLine("    ,eigyou_kbn ")                            '営業区分
            '    .AppendLine("    ,kameiten_mei ")                          '加盟店名
            '    .AppendLine("    ,shiten_mei ")                            '支店名
            '    .AppendLine("    ,busyo_cd ")                              '部署ｺｰﾄﾞ
            '    .AppendLine("    ,eigyou_tantousya_id ")                   '営業担当者
            '    .AppendLine("    ,eigyou_tantousya_mei ")                  '営業担当者名
            '    .AppendLine("    ,hituyou_mendan_kaisuu ")                 '必要面談回数
            '    .AppendLine("    ,eigyousyo_busyo_cd ")                    '営業所_部署ｺｰﾄﾞ
            '    .AppendLine("    ,eigyousyo_mei ")                         '営業所名
            '    .AppendLine("    ,todouhuken_cd ")                         '都道府県ｺｰﾄﾞ
            '    .AppendLine("    ,todouhuken_mei ")                        '都道府県名
            '    .AppendLine("    ,keiretu_cd ")                            '系列ｺｰﾄﾞ
            '    .AppendLine("    ,keiretu_mei ")                           '系列名
            '    .AppendLine("    ,eigyousyo_cd ")                          '営業所ｺｰﾄﾞ
            '    .AppendLine("    ,keikaku_nenkan_tousuu ")                 '年間棟数
            '    .AppendLine("    ,zenjitu_nenkan_tousuu ")                 '前年実績_年間棟数
            '    .AppendLine("    ,keikaku0_flg ")                          '計画値0FLG
            '    .AppendLine("    ,kameiten_add_datetime ")                 '加盟店登録年月
            '    .AppendLine("    ,kameiten_sinki_flg ")                    '加盟店新規FLG
            '    .AppendLine("    ,gyoutai ")                               '業態
            '    .AppendLine("    ,bunjyou_50flg ")                         '分譲上位50社FLG
            '    .AppendLine("    ,tyuumon_50flg ")                         '注文上位50社FLG
            '    .AppendLine("    ,heibai_50flg ")                          '併売上位50社FLG
            '    .AppendLine("    ,keikaku_huhen_flg ")                     '計画値不変FLG
            '    .AppendLine("    ,add_login_user_id ")                     '登録ログインユーザーID
            '    .AppendLine("    ,add_datetime ")                          '登録日時
            '    .AppendLine("    ,upd_login_user_id ")                     '更新ログインユーザーID
            '    .AppendLine("    ,upd_datetime ")                          '更新日時
            '    .AppendLine(" ) ")
            '    .AppendLine(" VALUES ( ")
            '    .AppendLine("	@keikaku_nendo ")
            '    .AppendLine("	,@kameiten_cd ")
            '    .AppendLine("	,@torikesi ")
            '    .AppendLine("	,@hattyuu_teisi_flg ")
            '    .AppendLine("	,@eigyou_kbn ")
            '    .AppendLine("	,@kameiten_mei ")
            '    .AppendLine("	,@shiten_mei ")
            '    .AppendLine("	,@busyo_cd ")
            '    .AppendLine("	,@eigyou_tantousya_id ")
            '    .AppendLine("	,@eigyou_tantousya_mei ")
            '    .AppendLine("	,@hituyou_mendan_kaisuu ")
            '    .AppendLine("	,@eigyousyo_busyo_cd ")
            '    .AppendLine("	,@eigyousyo_mei ")
            '    .AppendLine("	,@todouhuken_cd ")
            '    .AppendLine("	,@todouhuken_mei ")
            '    .AppendLine("	,@keiretu_cd ")
            '    .AppendLine("	,@keiretu_mei ")
            '    .AppendLine("	,@eigyousyo_cd ")
            '    .AppendLine("	,@keikaku_nenkan_tousuu ")
            '    .AppendLine("	,@zenjitu_nenkan_tousuu ")
            '    .AppendLine("	,@keikaku0_flg ")
            '    .AppendLine("	,@kameiten_add_datetime ")
            '    .AppendLine("	,@kameiten_sinki_flg ")
            '    .AppendLine("	,@gyoutai ")
            '    .AppendLine("	,@bunjyou_50flg ")
            '    .AppendLine("	,@tyuumon_50flg ")
            '    .AppendLine("	,@heibai_50flg ")
            '    .AppendLine("	,@keikaku_huhen_flg ")
            '    .AppendLine("	,@add_login_user_id ")
            '    .AppendLine("	,@add_datetime ")
            '    .AppendLine("	,@upd_login_user_id ")
            '    .AppendLine("	,@upd_datetime ")
            '    .AppendLine(" ) ")
            'End With

            Dim strSql As String = String.Empty
            strSql = Definition.GetSqlString(Definition.SqlStringKbn.S0003_07)
            sqlBuffer.AppendLine(strSql)

            'バラメタ
            With paramList
                paramList.Add(MakeParam("@keikaku_nendo", SqlDbType.Char, 4, strYear))                              '計画_年度
                paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 16, drData("加盟店ｺｰﾄﾞ")))                '加盟店ｺｰﾄﾞ
                paramList.Add(MakeParam("@torikesi", SqlDbType.Int, 1, drData("取消")))
                paramList.Add(MakeParam("@hattyuu_teisi_flg", SqlDbType.VarChar, 10, drData("発注停止FLG")))        '発注停止FLG
                paramList.Add(MakeParam("@eigyou_kbn", SqlDbType.VarChar, 1, drData("営業区分")))                   '営業区分
                paramList.Add(MakeParam("@kameiten_mei", SqlDbType.VarChar, 40, strKemeiten))                       '加盟店名
                paramList.Add(MakeParam("@shiten_mei", SqlDbType.VarChar, 160, drData("支店名")))                    '支店名
                paramList.Add(MakeParam("@busyo_cd", SqlDbType.VarChar, 16, IIf(drData("部署ｺｰﾄﾞ").ToString.Length > 4, "0000", drData("部署ｺｰﾄﾞ"))))                     '部署ｺｰﾄﾞ
                paramList.Add(MakeParam("@eigyou_tantousya_id", SqlDbType.VarChar, 30, drData("営業担当者")))       '営業担当者
                paramList.Add(MakeParam("@eigyou_tantousya_mei", SqlDbType.VarChar, 100, drData("営業担当者名")))    '営業担当者名
                paramList.Add(MakeParam("@hituyou_mendan_kaisuu", SqlDbType.Int, 3, drData("必要面談回数")))
                paramList.Add(MakeParam("@eigyousyo_busyo_cd", SqlDbType.VarChar, 16, drData("営業所_部署ｺｰﾄﾞ")))    '営業所_部署ｺｰﾄﾞ
                paramList.Add(MakeParam("@eigyousyo_mei", SqlDbType.VarChar, 160, drData("営業所名")))               '営業所名
                paramList.Add(MakeParam("@todouhuken_cd", SqlDbType.VarChar, 2, drData("都道府県ｺｰﾄﾞ")))            '都道府県ｺｰﾄﾞ
                paramList.Add(MakeParam("@todouhuken_mei", SqlDbType.VarChar, 10, drData("都道府県名")))            '都道府県名
                paramList.Add(MakeParam("@keiretu_cd", SqlDbType.VarChar, 5, drData("系列ｺｰﾄﾞ")))                   '系列ｺｰﾄﾞ
                paramList.Add(MakeParam("@keiretu_mei", SqlDbType.VarChar, 40, drData("系列名")))                   '系列名
                paramList.Add(MakeParam("@eigyousyo_cd", SqlDbType.VarChar, 5, drData("営業所ｺｰﾄﾞ")))               '営業所ｺｰﾄﾞ
                'TODO
                'paramList.Add(MakeParam("@keikaku_nenkan_tousuu", SqlDbType.BigInt, 12, IIf(drData("年間棟数").ToString = String.Empty, DBNull.Value, drData("年間棟数"))))         '年間棟数
                paramList.Add(MakeParam("@keikaku_nenkan_tousuu", SqlDbType.VarChar, 5, drData("年間棟数")))         '年間棟数
                paramList.Add(MakeParam("@zenjitu_nenkan_tousuu", SqlDbType.BigInt, 12, drData("前年実績_年間棟数")))         '前年実績_年間棟数
                paramList.Add(MakeParam("@keikaku0_flg", SqlDbType.Int, 1, drData("計画値0FLG")))                  '計画値0FLG
                paramList.Add(MakeParam("@kameiten_add_datetime", SqlDbType.VarChar, 7, drData("加盟店登録年月")))         '加盟店登録年月
                paramList.Add(MakeParam("@kameiten_sinki_flg", SqlDbType.Int, 1, drData("加盟店新規FLG")))         '加盟店新規FLG
                paramList.Add(MakeParam("@gyoutai", SqlDbType.VarChar, 20, drData("業態")))                   '業態
                paramList.Add(MakeParam("@bunjyou_50flg", SqlDbType.Int, 2, drData("分譲上位50社FLG")))          '分譲上位50社FLG
                paramList.Add(MakeParam("@tyuumon_50flg", SqlDbType.Int, 2, drData("注文上位50社FLG")))          '注文上位50社FLG
                paramList.Add(MakeParam("@heibai_50flg", SqlDbType.Int, 2, drData("併売上位50社FLG")))          '併売上位50社FLG
                paramList.Add(MakeParam("@keikaku_huhen_flg", SqlDbType.Int, 1, "0"))          '計画値不変FLG
                paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, drData("登録ID")))          '登録ログインユーザーID
                paramList.Add(MakeParam("@add_datetime", SqlDbType.DateTime, 25, IIf(drData("登録日時").ToString = String.Empty, DBNull.Value, drData("登録日時"))))            '登録日時
                paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, drData("更新ID")))          '更新ログインユーザーID
                paramList.Add(MakeParam("@upd_datetime", SqlDbType.DateTime, 25, IIf(drData("更新日時").ToString = String.Empty, DBNull.Value, drData("更新日時"))))            '更新日時
            End With

            Return objConnection.ExecuteNonQuery(CommandType.Text, sqlBuffer.ToString(), paramList.ToArray())


        Catch ex As Exception
            If IsNothing(ex.Data.Item("ERROR_LOG")) Then
                ex.Data.Add("ERROR_LOG", "計画管理_加盟店マスタのデータの挿入処理が異常終了しました。")
            End If
            Throw ex

        End Try

    End Function

    ''' <summary>
    ''' 計画管理_加盟店のデータを取得する
    ''' </summary>
    ''' <param name="objConnection">DB接続</param>
    ''' <param name="strYear">対象年度</param>
    ''' <returns>計画管理_加盟店データ</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2013/01/15 大連/楊双 新規作成 P-45026</para>
    ''' </history>
    Private Function SelKeikakuKameiten(ByVal objConnection As SqlExecutor, _
                                        ByVal strYear As String) As Data.DataTable
        'SQLコメント
        Dim sqlBuffer As New System.Text.StringBuilder

        'バラメタ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        Try
            'SQL文	
            'With sqlBuffer
            '    .AppendLine(" SELECT  ")
            '    .AppendLine(" 	k.kameiten_cd ")
            '    .AppendLine(" FROM m_keikaku_kameiten k with(readuncommitted) ")
            '    .AppendLine(" WHERE  k.keikaku_nendo = @year ")
            '    .AppendLine(" AND k.keikaku_huhen_flg = @keikaku_huhen_flg ")
            'End With

            Dim strSql As String = String.Empty
            strSql = Definition.GetSqlString(Definition.SqlStringKbn.S0003_04)
            sqlBuffer.AppendLine(strSql)

            'バラメタ
            paramList.Add(MakeParam("@year", SqlDbType.VarChar, 4, strYear))            '年度
            paramList.Add(MakeParam("@keikaku_huhen_flg", SqlDbType.Int, 1, 1))   '計画値不変FLG

            'Ｅａｒｔｈのデータ
            Return objConnection.ExecuteDataTable(CommandType.Text, sqlBuffer.ToString(), paramList.ToArray())

        Catch ex As Exception
            If IsNothing(ex.Data.Item("ERROR_LOG")) Then
                ex.Data.Add("ERROR_LOG", "計画管理_加盟店のデータの取得処理が異常終了しました。")
            End If
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' 計画管理_加盟店データの存在チェック
    ''' </summary>
    ''' <param name="objConnection">DB接続</param>
    ''' <param name="strYear">対象年度</param>
    ''' <param name="strKameitenCd">加盟店ｺｰﾄﾞ</param>
    ''' <returns>計画管理_加盟店データ</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2013/01/15 大連/楊双 新規作成 P-45026</para>
    ''' </history>
    Private Function SelKeikakuKameitenCount(ByVal objConnection As SqlExecutor, _
                                        ByVal strYear As String, _
                                        ByVal strKameitenCd As String) As Data.DataTable
        'SQLコメント
        Dim sqlBuffer As New System.Text.StringBuilder

        'バラメタ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        Try
            'SQL文	
            'With sqlBuffer
            '    .AppendLine(" SELECT  ")
            '    .AppendLine(" 	count(k.kameiten_cd) ")
            '    .AppendLine(" FROM m_keikaku_kameiten k with(readuncommitted) ")
            '    .AppendLine(" WHERE  k.keikaku_nendo = @year ")
            '    .AppendLine(" AND k.kameiten_cd = @kameiten_cd ")
            'End With

            Dim strSql As String = String.Empty
            strSql = Definition.GetSqlString(Definition.SqlStringKbn.S0003_08)
            sqlBuffer.AppendLine(strSql)

            'バラメタ
            paramList.Add(MakeParam("@year", SqlDbType.VarChar, 4, strYear))            '年度
            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 8, strKameitenCd))   '加盟店ｺｰﾄﾞ

            'Ｅａｒｔｈのデータ
            Return objConnection.ExecuteDataTable(CommandType.Text, sqlBuffer.ToString(), paramList.ToArray())

        Catch ex As Exception
            If IsNothing(ex.Data.Item("ERROR_LOG")) Then
                ex.Data.Add("ERROR_LOG", "計画管理_加盟店のデータの取得処理が異常終了しました。")
            End If
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' 計画管理テーブルからデータ件数を取得する
    ''' </summary>
    ''' <param name="objConnection">DB接続</param>
    ''' <param name="strYear">対象年度</param>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <returns>計画管理テーブル</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2013/01/15 大連/楊双 新規作成 P-45026</para>
    ''' </history>
    Private Function SelKeikakuKanri(ByVal objConnection As SqlExecutor, _
                                        ByVal strYear As String, _
                                        ByVal strKameitenCd As String) As Data.DataTable
        'SQLコメント
        Dim sqlBuffer As New System.Text.StringBuilder

        'バラメタ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        Try
            'SQL文	
            'With sqlBuffer
            '    .AppendLine(" SELECT  ")
            '    .AppendLine(" 	isnull([4gatu_keikaku_kensuu],0) + isnull([5gatu_keikaku_kensuu],0) +isnull([6gatu_keikaku_kensuu],0) ")
            '    .AppendLine(" +	isnull([7gatu_keikaku_kensuu],0) + isnull([8gatu_keikaku_kensuu],0) +isnull([9gatu_keikaku_kensuu],0) ")
            '    .AppendLine(" +	isnull([10gatu_keikaku_kensuu],0) + isnull([11gatu_keikaku_kensuu],0) +isnull([12gatu_keikaku_kensuu],0) ")
            '    .AppendLine(" +	isnull([1gatu_keikaku_kensuu],0) + isnull([2gatu_keikaku_kensuu],0) +isnull([3gatu_keikaku_kensuu],0) ")
            '    .AppendLine(" as keikaku_kensuu ")
            '    .AppendLine(" FROM t_keikaku_kanri with(readuncommitted) ")
            '    .AppendLine(" WHERE keikaku_nendo = @keikaku_nendo ")
            '    .AppendLine(" AND kameiten_cd = @kameiten_cd ")
            'End With

            Dim strSql As String = String.Empty
            strSql = Definition.GetSqlString(Definition.SqlStringKbn.S0003_06)
            sqlBuffer.AppendLine(strSql)

            'バラメタ
            paramList.Add(MakeParam("@keikaku_nendo", SqlDbType.Char, 4, strYear))            '計画_年度
            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 8, strKameitenCd))     '加盟店ｺｰﾄﾞ

            'Ｅａｒｔｈのデータ
            Return objConnection.ExecuteDataTable(CommandType.Text, sqlBuffer.ToString(), paramList.ToArray())

        Catch ex As Exception
            If IsNothing(ex.Data.Item("ERROR_LOG")) Then
                ex.Data.Add("ERROR_LOG", "計画管理のデータの取得処理が異常終了しました。")
            End If
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' 計画用_名称マスタから営業区分を取得する
    ''' </summary>
    ''' <param name="objConnection">DB接続</param>
    ''' <returns>計画用_名称マスタ</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2013/01/15 大連/楊双 新規作成 P-45026</para>
    ''' </history>
    Private Function SelKeikakuyouMeisyou(ByVal objConnection As SqlExecutor) As Data.DataTable

        'SQLコメント
        Dim sqlBuffer As New System.Text.StringBuilder

        'バラメタ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        Try
            'SQL文	
            'With sqlBuffer
            '    .AppendLine(" SELECT  ")
            '    .AppendLine(" 	code ")
            '    .AppendLine("   ,meisyou ")
            '    .AppendLine(" FROM m_keikakuyou_meisyou with(readuncommitted) ")
            '    .AppendLine(" WHERE meisyou_syubetu = @meisyou_syubetu ")
            'End With

            Dim strSql As String = String.Empty
            strSql = Definition.GetSqlString(Definition.SqlStringKbn.S0003_05)
            sqlBuffer.AppendLine(strSql)

            'バラメタ
            paramList.Add(MakeParam("@meisyou_syubetu", SqlDbType.Char, 2, "05"))            '営業区分

            'Ｅａｒｔｈのデータ
            Return objConnection.ExecuteDataTable(CommandType.Text, sqlBuffer.ToString(), paramList.ToArray())

        Catch ex As Exception
            If IsNothing(ex.Data.Item("ERROR_LOG")) Then
                ex.Data.Add("ERROR_LOG", "計画用_名称マスタのデータの取得処理が異常終了しました。")
            End If
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' データ編集処理
    ''' </summary>
    ''' <param name="strDateTime">日時</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2013/01/15 大連/楊双 新規作成 P-45026</para>
    ''' </history>
    Private Function SetDateTime(ByVal strDateTime As String) As String

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(mMyNamePeriod & MyMethod.GetCurrentMethod.Name, strDateTime)

        If strDateTime <> String.Empty Then
            strDateTime = Left(strDateTime, 4) _
                            & "/" & Mid(strDateTime, 5, 2) _
                            & "/" & Mid(strDateTime, 7, 2) _
                            & " " & Mid(strDateTime, 9, 2) _
                            & ":" & Mid(strDateTime, 11, 2) _
                            & ":" & Mid(strDateTime, 13, 2)

        End If

        Return strDateTime

    End Function

#End Region

End Class


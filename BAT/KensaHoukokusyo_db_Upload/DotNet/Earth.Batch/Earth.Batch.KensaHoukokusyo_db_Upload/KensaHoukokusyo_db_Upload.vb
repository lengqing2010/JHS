Imports System.Data
Imports System.Text
Imports System.IO
Imports System.Transactions
Imports earth.Batch.SqlExecutor
Imports earth.Batch
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase

''' <summary>
''' 検査報告書アップロード
''' </summary>
''' <remarks></remarks>
''' <history>
''' <para>2015/12/04 高兵兵(大連) 新規作成</para>
''' </history>
Public Class KensaHoukokusyo_db_Upload

#Region "変数"
    '各Event/Methodの動作時における、"EMAB障害対応情報の格納処理"向け、自クラス名
    Private ReadOnly mMyNamePeriod As String = MyClass.GetType.FullName
    'DB接続ストリング
    Private mDBconnectionEarth As String
    Private mDBconnectionJhsfgm As String
    'DB接続
    Private mConnectionEarth As SqlExecutor
    Private mConnectionJhsfgm As SqlExecutor

    'ログメッセージ
    Private mLogMsg As New StringBuilder()
    '新規件数
    Private mInsCount As Integer = 0
    '失敗件数
    Private mFailureCount As Integer = 0
#End Region

#Region "Main処理"
    ''' <summary>
    ''' Main処理
    ''' </summary>
    ''' <param name="argv"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2015/12/04 高兵兵(大連) 新規作成</para>
    ''' </history>
    Public Shared Function Main(ByVal argv As String()) As Integer

        Dim btProcess As KensaHoukokusyo_db_Upload

        '初期化
        btProcess = New KensaHoukokusyo_db_Upload()

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(btProcess.mMyNamePeriod & MyMethod.GetCurrentMethod.Name, argv)

        Try
            'DB接続ストリング
            btProcess.mDBconnectionEarth = Definition.GetConnectionStringEarth()
            btProcess.mDBconnectionJhsfgm = Definition.GetConnectionStringJhsfgm()

            'DB接続
            btProcess.mConnectionEarth = New SqlExecutor(btProcess.mDBconnectionEarth)
            btProcess.mConnectionJhsfgm = New SqlExecutor(btProcess.mDBconnectionJhsfgm)

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

            btProcess.mLogMsg.AppendLine(Format(Date.Now, "yyyy年MM月dd日 HH:mm:ss  ") & "バッチ処理を異常に終了しました。")
            Return 9
        Finally
            If btProcess.mConnectionEarth IsNot Nothing Then
                'DB接続のグルーズ
                btProcess.mConnectionEarth.Close()
                btProcess.mConnectionEarth.Dispose()
            End If

            If btProcess.mConnectionJhsfgm IsNot Nothing Then
                'DB接続のグルーズ
                btProcess.mConnectionJhsfgm.Close()
                btProcess.mConnectionJhsfgm.Dispose()
            End If

            '新規件数をログファイルに出力する
            btProcess.mLogMsg.AppendLine(Format(Date.Now, "yyyy年MM月dd日 HH:mm:ss  ") & _
            "検査報告書アップロード結果：" & _
            Convert.ToString(btProcess.mInsCount) & "件が成功しました、" & _
            Convert.ToString(btProcess.mFailureCount) & "件が失敗しました。")

            'ログ出力
            Console.WriteLine(btProcess.mLogMsg.ToString())

            'Console.ReadLine()
        End Try

    End Function

#End Region

#Region "主処理"
    ''' <summary>
    ''' 主処理
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2015/12/04 高兵兵(大連) 新規作成</para>
    ''' </history>
    Private Sub Main_Process()
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(mMyNamePeriod & MyMethod.GetCurrentMethod.Name)

        mLogMsg.AppendLine(Format(Date.Now, "yyyy年MM月dd日 HH:mm:ss  ") & _
                            "バッチ処理を開始しました。")
        'DB接続のオープン
        mConnectionEarth.Open(True)
        mConnectionJhsfgm.Open()

        '検査報告書コピー元のパス
        Dim copyFromPath As String = Definition.GetKensaHoukokusyoCopyFromPath
        If Not Directory.Exists(copyFromPath) Then
            mLogMsg.AppendLine("検査報告書コピー元のパス「" & copyFromPath & "」が存在しません。")
            '異常終了
            Throw New Exception
        End If

        '検査報告書コピー先のパス
        Dim copyToPath As String = Definition.GetKensaHoukokusyoCopyToPath
        copyToPath = copyToPath & IIf(Right(copyToPath, 1).Equals("\"), String.Empty, "\").ToString
        If Not Directory.Exists(copyToPath) Then
            mLogMsg.AppendLine("検査報告書コピー先のパス「" & copyToPath & "」が存在しません。")
            '異常終了
            Throw New Exception
        End If

        '検査報告書管理テーブル
        Dim dtKensa As Data.DataTable = Me.CreateKensaHkksKanriTable()
        Dim drKensa As Data.DataRow

        '加盟店情報を取得する
        Dim dtkameiten As DataTable = Nothing
        '地盤情報を取得する
        Dim dtJjiban As DataTable = Nothing
        '受注情報を取得する
        Dim dtJyucyu As DataTable = Nothing
        '検査情報を取得する
        Dim dtKensaInfo As DataTable = Nothing

        Dim copyFromDir As New DirectoryInfo(copyFromPath)
        For Each pdfFile As FileInfo In copyFromDir.GetFiles("*.pdf")

            Try
                'ファイル名
                Dim pdfFileName As String = pdfFile.Name
                pdfFileName = pdfFileName.Substring(0, pdfFileName.Length() - 4)
                '区分
                Dim kbn As String = String.Empty
                '加盟店コード
                Dim kameitenCd As String = String.Empty
                '保証書No
                Dim hosyousyoNo As String = String.Empty

                'ファイル名チェック
                Dim nameErrFlg As Boolean = False
                Dim pdfName() As String = pdfFileName.Split(CChar("-"))
                If pdfName.Length <> 2 Then
                    nameErrFlg = True
                Else
                    kbn = Left(pdfName(0), 1)   '区分
                    kameitenCd = Mid(pdfName(0), 2) '加盟店コード
                    hosyousyoNo = Mid(pdfName(1), 2)    '保証書No
                    If kbn.Equals(String.Empty) OrElse kameitenCd.Equals(String.Empty) OrElse hosyousyoNo.Equals(String.Empty) Then
                        nameErrFlg = True
                    End If
                End If
                If nameErrFlg Then
                    Dim ex As New Exception
                    If IsNothing(ex.Data.Item("ERROR_LOG")) Then
                        ex.Data.Add("ERROR_LOG", "ファイル名が正しくない。")
                    End If
                    Throw ex
                End If

                '登録情報を整理する
                dtKensa.Clear()
                drKensa = dtKensa.NewRow
                '区分
                drKensa.Item("kbn") = kbn
                '保証書No
                drKensa.Item("hosyousyo_no") = hosyousyoNo
                '加盟店コード
                drKensa.Item("kameiten_cd") = kameitenCd
                '格納日
                drKensa.Item("kakunou_date") = Date.Now

                '加盟店情報を取得する
                dtkameiten = GetKameitenInfoFromEarth(mConnectionEarth, kbn, kameitenCd)
                If dtkameiten.Rows.Count > 0 Then
                    '加盟店名
                    drKensa.Item("kameiten_mei") = dtkameiten.Rows(0).Item("kameiten_mei1").ToString().Trim()
                    '取消	
                    drKensa.Item("torikesi") = "0"
                    '検査報告書発行部数	
                    drKensa.Item("kensa_hkks_busuu") = dtkameiten.Rows(0).Item("kensa_hkks_busuu").ToString().Trim()
                    '検査報告書送付先住所1	
                    drKensa.Item("kensa_hkks_jyuusyo1") = dtkameiten.Rows(0).Item("jyuusyo1").ToString().Trim()
                    '検査報告書送付先住所2	
                    drKensa.Item("kensa_hkks_jyuusyo2") = dtkameiten.Rows(0).Item("jyuusyo2").ToString().Trim()
                    '郵便番号
                    drKensa.Item("yuubin_no") = dtkameiten.Rows(0).Item("yuubin_no").ToString().Trim()
                    '電話番号	
                    drKensa.Item("tel_no") = dtkameiten.Rows(0).Item("tel_no").ToString().Trim()
                    '部署名	
                    drKensa.Item("busyo_mei") = dtkameiten.Rows(0).Item("busyo_mei").ToString().Trim()
                    '都道府県コード	
                    drKensa.Item("todouhuken_cd") = dtkameiten.Rows(0).Item("todouhuken_cd").ToString().Trim()
                    '都道府県名	
                    drKensa.Item("todouhuken_mei") = dtkameiten.Rows(0).Item("todouhuken_mei").ToString().Trim()
                Else
                    '検査報告書エラーフォルダへ移動する
                    Me.MoveErrFile(pdfFile, copyFromPath)
                    Dim ex As New Exception
                    If IsNothing(ex.Data.Item("ERROR_LOG")) Then
                        ex.Data.Add("ERROR_LOG", "加盟店情報がありません。")
                    End If
                    Throw ex
                End If

                '発送日	
                drKensa.Item("hassou_date") = DBNull.Value
                '発送日入力フラグ	
                drKensa.Item("hassou_date_in_flg") = "0"
                '送付担当者	
                drKensa.Item("souhu_tantousya") = DBNull.Value

                '地盤情報を取得する
                dtJjiban = GetJibanInfoFromEarth(mConnectionEarth, kbn, hosyousyoNo, kameitenCd)
                If dtJjiban.Rows.Count > 0 Then
                    '施主名
                    drKensa.Item("sesyu_mei") = dtJjiban.Rows(0).Item("sesyu_mei").ToString().Trim()
                    '物件住所1	
                    drKensa.Item("bukken_jyuusyo1") = dtJjiban.Rows(0).Item("bukken_jyuusyo1").ToString().Trim()
                    '物件住所2	
                    drKensa.Item("bukken_jyuusyo2") = dtJjiban.Rows(0).Item("bukken_jyuusyo2").ToString().Trim()
                    '物件住所3	
                    drKensa.Item("bukken_jyuusyo3") = dtJjiban.Rows(0).Item("bukken_jyuusyo3").ToString().Trim()
                Else
                    '検査報告書エラーフォルダへ移動する
                    Me.MoveErrFile(pdfFile, copyFromPath)
                    Dim ex As New Exception
                    If IsNothing(ex.Data.Item("ERROR_LOG")) Then
                        ex.Data.Add("ERROR_LOG", "地盤情報がありません。")
                    End If
                    Throw ex

                End If

                '受注情報を取得する
                dtJyucyu = GetJyutyuuInfoFromJhsfgm(mConnectionJhsfgm, kbn, hosyousyoNo)
                If dtJyucyu.Rows.Count > 0 Then
                    '建物構造	
                    drKensa.Item("tatemono_kouzou") = dtJyucyu.Rows(0).Item("tatemono_kouzou").ToString().Trim()
                    '建物階数	
                    drKensa.Item("tatemono_kaisu") = dtJyucyu.Rows(0).Item("tatemono_kaisu").ToString().Trim()
                    'FC名	
                    drKensa.Item("fc_nm") = dtJyucyu.Rows(0).Item("fc_nm").ToString().Trim()
                    '依頼担当者名	
                    drKensa.Item("kameiten_tanto") = dtJyucyu.Rows(0).Item("kameiten_tanto").ToString().Trim()
                    '建物加盟店コード	
                    drKensa.Item("tatemono_kameiten_cd") = dtJyucyu.Rows(0).Item("kameiten_cd").ToString().Trim()
                    '建物構造_概要用	
                    drKensa.Item("gaiyou_you") = dtJyucyu.Rows(0).Item("gaiyou_you").ToString().Trim()
                    '建物構造_指示書用	
                    drKensa.Item("shijisyo_you") = dtJyucyu.Rows(0).Item("shijisyo_you").ToString().Trim()
                Else
                    '検査報告書エラーフォルダへ移動する
                    Me.MoveErrFile(pdfFile, copyFromPath)
                    Dim ex As New Exception
                    If IsNothing(ex.Data.Item("ERROR_LOG")) Then
                        ex.Data.Add("ERROR_LOG", "受注情報がありません。")
                    End If
                    Throw ex
                End If

                '管理表出力フラグ	
                drKensa.Item("kanrihyou_out_flg") = "0"
                '管理表出力日	
                drKensa.Item("kanrihyou_out_date") = DBNull.Value
                '送付状出力フラグ	
                drKensa.Item("souhujyou_out_flg") = "0"
                '送付状出力日	
                drKensa.Item("souhujyou_out_date") = DBNull.Value
                '検査報告書出力フラグ	
                drKensa.Item("kensa_hkks_out_flg") = "0"
                '検査報告書出力日	
                drKensa.Item("kensa_hkks_out_date") = DBNull.Value
                '通しNo	
                drKensa.Item("tooshi_no") = DBNull.Value

                '検査情報を取得する
                dtKensaInfo = GetKensaInfoFromJhsfgm(mConnectionJhsfgm, kbn, hosyousyoNo)
                If dtKensaInfo.Rows.Count > 0 Then
                    '検査工程名1	
                    drKensa.Item("kensa_koutei_nm1") = dtKensaInfo.Rows(0).Item("kensa_koutei_nm").ToString().Trim()
                    '検査実施日1	
                    drKensa.Item("kensa_start_jissibi1") = dtKensaInfo.Rows(0).Item("kensa_start_jissibi").ToString().Trim()
                    '検査員名1	
                    drKensa.Item("kensa_in_nm1") = dtKensaInfo.Rows(0).Item("kensain_nm").ToString().Trim()
                    If dtKensaInfo.Rows.Count > 1 Then
                        '検査工程名2	
                        drKensa.Item("kensa_koutei_nm2") = dtKensaInfo.Rows(1).Item("kensa_koutei_nm").ToString().Trim()
                        '検査実施日2	
                        drKensa.Item("kensa_start_jissibi2") = dtKensaInfo.Rows(1).Item("kensa_start_jissibi").ToString().Trim()
                        '検査員名2	
                        drKensa.Item("kensa_in_nm2") = dtKensaInfo.Rows(1).Item("kensain_nm").ToString().Trim()
                    End If
                    If dtKensaInfo.Rows.Count > 2 Then
                        '検査工程名3	
                        drKensa.Item("kensa_koutei_nm3") = dtKensaInfo.Rows(2).Item("kensa_koutei_nm").ToString().Trim()
                        '検査実施日3	
                        drKensa.Item("kensa_start_jissibi3") = dtKensaInfo.Rows(2).Item("kensa_start_jissibi").ToString().Trim()
                        '検査員名3	
                        drKensa.Item("kensa_in_nm3") = dtKensaInfo.Rows(2).Item("kensain_nm").ToString().Trim()
                    End If
                    If dtKensaInfo.Rows.Count > 3 Then
                        '検査工程名4	
                        drKensa.Item("kensa_koutei_nm4") = dtKensaInfo.Rows(3).Item("kensa_koutei_nm").ToString().Trim()
                        '検査実施日4	
                        drKensa.Item("kensa_start_jissibi4") = dtKensaInfo.Rows(3).Item("kensa_start_jissibi").ToString().Trim()
                        '検査員名4	
                        drKensa.Item("kensa_in_nm4") = dtKensaInfo.Rows(3).Item("kensain_nm").ToString().Trim()
                    End If
                    If dtKensaInfo.Rows.Count > 4 Then
                        '検査工程名5	
                        drKensa.Item("kensa_koutei_nm5") = dtKensaInfo.Rows(4).Item("kensa_koutei_nm").ToString().Trim()
                        '検査実施日5	
                        drKensa.Item("kensa_start_jissibi5") = dtKensaInfo.Rows(4).Item("kensa_start_jissibi").ToString().Trim()
                        '検査員名5	
                        drKensa.Item("kensa_in_nm5") = dtKensaInfo.Rows(4).Item("kensain_nm").ToString().Trim()
                    End If
                    If dtKensaInfo.Rows.Count > 5 Then
                        '検査工程名6	
                        drKensa.Item("kensa_koutei_nm6") = dtKensaInfo.Rows(5).Item("kensa_koutei_nm").ToString().Trim()
                        '検査実施日6	
                        drKensa.Item("kensa_start_jissibi6") = dtKensaInfo.Rows(5).Item("kensa_start_jissibi").ToString().Trim()
                        '検査員名6	
                        drKensa.Item("kensa_in_nm6") = dtKensaInfo.Rows(5).Item("kensain_nm").ToString().Trim()
                    End If
                    If dtKensaInfo.Rows.Count > 6 Then
                        '検査工程名7	
                        drKensa.Item("kensa_koutei_nm7") = dtKensaInfo.Rows(6).Item("kensa_koutei_nm").ToString().Trim()
                        '検査実施日7	
                        drKensa.Item("kensa_start_jissibi7") = dtKensaInfo.Rows(6).Item("kensa_start_jissibi").ToString().Trim()
                        '検査員名7	
                        drKensa.Item("kensa_in_nm7") = dtKensaInfo.Rows(6).Item("kensain_nm").ToString().Trim()
                    End If
                    If dtKensaInfo.Rows.Count > 7 Then
                        '検査工程名8	
                        drKensa.Item("kensa_koutei_nm8") = dtKensaInfo.Rows(7).Item("kensa_koutei_nm").ToString().Trim()
                        '検査実施日8	
                        drKensa.Item("kensa_start_jissibi8") = dtKensaInfo.Rows(7).Item("kensa_start_jissibi").ToString().Trim()
                        '検査員名8	
                        drKensa.Item("kensa_in_nm8") = dtKensaInfo.Rows(7).Item("kensain_nm").ToString().Trim()
                    End If
                    If dtKensaInfo.Rows.Count > 8 Then
                        '検査工程名9	
                        drKensa.Item("kensa_koutei_nm9") = dtKensaInfo.Rows(8).Item("kensa_koutei_nm").ToString().Trim()
                        '検査実施日9	
                        drKensa.Item("kensa_start_jissibi9") = dtKensaInfo.Rows(8).Item("kensa_start_jissibi").ToString().Trim()
                        '検査員名9	
                        drKensa.Item("kensa_in_nm9") = dtKensaInfo.Rows(8).Item("kensain_nm").ToString().Trim()
                    End If
                    If dtKensaInfo.Rows.Count > 9 Then
                        '検査工程名10	
                        drKensa.Item("kensa_koutei_nm10") = dtKensaInfo.Rows(9).Item("kensa_koutei_nm").ToString().Trim()
                        '検査実施日10	
                        drKensa.Item("kensa_start_jissibi10") = dtKensaInfo.Rows(9).Item("kensa_start_jissibi").ToString().Trim()
                        '検査員名10
                        drKensa.Item("kensa_in_nm10") = dtKensaInfo.Rows(8).Item("kensain_nm").ToString().Trim()
                    End If
                End If

                '登録ログインユーザID	
                drKensa.Item("add_login_user_id") = "kensa_hkks_bat"
                '登録日時	
                drKensa.Item("add_datetime") = Date.Now
                '更新ログインユーザID	
                drKensa.Item("upd_login_user_id") = DBNull.Value
                '更新日時	
                drKensa.Item("upd_datetime") = DBNull.Value
                dtKensa.Rows.Add(drKensa)

                '「検査報告書管理テーブル」を登録する
                Me.InstKensaHkksKanri(mConnectionEarth, dtKensa)

                '検査報告書コピー先へ移動する
                Me.MoveFile(pdfFile, copyToPath)

                '成功の場合
                mConnectionEarth.Commit()

                '新規件数
                mInsCount = mInsCount + 1
            Catch ex As Exception
                '失敗の場合
                mConnectionEarth.Rollback()
                Dim logMsg As String = String.Empty
                If ex.Data.Item("ERROR_LOG") IsNot Nothing Then
                    logMsg = "(" & Convert.ToString(ex.Data.Item("ERROR_LOG")) & ")"
                End If

                mLogMsg.AppendLine(Format(Date.Now, "yyyy年MM月dd日 HH:mm:ss  ") & _
                                    "「" & pdfFile.Name & "」の処理は失敗しました。" & logMsg)

                '失敗件数
                mFailureCount = mFailureCount + 1
            Finally
                mConnectionEarth.Close()
                mConnectionEarth.Open(True)

                'データを解放する
                If dtkameiten IsNot Nothing Then
                    dtkameiten.Dispose()
                    dtkameiten = Nothing
                End If
                If dtJjiban IsNot Nothing Then
                    dtJjiban.Dispose()
                    dtJjiban = Nothing
                End If
                If dtJyucyu IsNot Nothing Then
                    dtJyucyu.Dispose()
                    dtJyucyu = Nothing
                End If
                If dtKensaInfo IsNot Nothing Then
                    dtKensaInfo.Dispose()
                    dtKensaInfo = Nothing
                End If
            End Try
        Next

        If dtKensa IsNot Nothing Then
            dtKensa.Dispose()
            dtKensa = Nothing
        End If

        mLogMsg.AppendLine(Format(Date.Now, "yyyy年MM月dd日 HH:mm:ss  ") & _
                            "バッチ処理を終了しました。")

    End Sub

    ''' <summary>
    ''' 検査報告書管理テーブルを作成する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2015/12/04 高兵兵(大連) 新規作成</para>
    ''' </history>
    Private Function CreateKensaHkksKanriTable() As Data.DataTable
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name)
        Dim dt As New Data.DataTable
        With dt.Columns
            .Add(New Data.DataColumn("kbn"))
            .Add(New Data.DataColumn("hosyousyo_no"))
            .Add(New Data.DataColumn("kameiten_cd"))
            .Add(New Data.DataColumn("kakunou_date"))
            .Add(New Data.DataColumn("kameiten_mei"))
            .Add(New Data.DataColumn("sesyu_mei"))
            .Add(New Data.DataColumn("torikesi"))
            .Add(New Data.DataColumn("kensa_hkks_busuu"))
            .Add(New Data.DataColumn("kensa_hkks_jyuusyo1"))
            .Add(New Data.DataColumn("kensa_hkks_jyuusyo2"))
            .Add(New Data.DataColumn("yuubin_no"))
            .Add(New Data.DataColumn("tel_no"))
            .Add(New Data.DataColumn("busyo_mei"))
            .Add(New Data.DataColumn("todouhuken_cd"))
            .Add(New Data.DataColumn("todouhuken_mei"))
            .Add(New Data.DataColumn("hassou_date"))
            .Add(New Data.DataColumn("hassou_date_in_flg"))
            .Add(New Data.DataColumn("souhu_tantousya"))
            .Add(New Data.DataColumn("bukken_jyuusyo1"))
            .Add(New Data.DataColumn("bukken_jyuusyo2"))
            .Add(New Data.DataColumn("bukken_jyuusyo3"))
            .Add(New Data.DataColumn("tatemono_kouzou"))
            .Add(New Data.DataColumn("tatemono_kaisu"))
            .Add(New Data.DataColumn("fc_nm"))
            .Add(New Data.DataColumn("kameiten_tanto"))
            .Add(New Data.DataColumn("tatemono_kameiten_cd"))
            .Add(New Data.DataColumn("kanrihyou_out_flg"))
            .Add(New Data.DataColumn("kanrihyou_out_date"))
            .Add(New Data.DataColumn("souhujyou_out_flg"))
            .Add(New Data.DataColumn("souhujyou_out_date"))
            .Add(New Data.DataColumn("kensa_hkks_out_flg"))
            .Add(New Data.DataColumn("kensa_hkks_out_date"))
            .Add(New Data.DataColumn("tooshi_no"))
            .Add(New Data.DataColumn("kensa_koutei_nm1"))
            .Add(New Data.DataColumn("kensa_koutei_nm2"))
            .Add(New Data.DataColumn("kensa_koutei_nm3"))
            .Add(New Data.DataColumn("kensa_koutei_nm4"))
            .Add(New Data.DataColumn("kensa_koutei_nm5"))
            .Add(New Data.DataColumn("kensa_koutei_nm6"))
            .Add(New Data.DataColumn("kensa_koutei_nm7"))
            .Add(New Data.DataColumn("kensa_koutei_nm8"))
            .Add(New Data.DataColumn("kensa_koutei_nm9"))
            .Add(New Data.DataColumn("kensa_koutei_nm10"))
            .Add(New Data.DataColumn("kensa_start_jissibi1"))
            .Add(New Data.DataColumn("kensa_start_jissibi2"))
            .Add(New Data.DataColumn("kensa_start_jissibi3"))
            .Add(New Data.DataColumn("kensa_start_jissibi4"))
            .Add(New Data.DataColumn("kensa_start_jissibi5"))
            .Add(New Data.DataColumn("kensa_start_jissibi6"))
            .Add(New Data.DataColumn("kensa_start_jissibi7"))
            .Add(New Data.DataColumn("kensa_start_jissibi8"))
            .Add(New Data.DataColumn("kensa_start_jissibi9"))
            .Add(New Data.DataColumn("kensa_start_jissibi10"))
            .Add(New Data.DataColumn("kensa_in_nm1"))
            .Add(New Data.DataColumn("kensa_in_nm2"))
            .Add(New Data.DataColumn("kensa_in_nm3"))
            .Add(New Data.DataColumn("kensa_in_nm4"))
            .Add(New Data.DataColumn("kensa_in_nm5"))
            .Add(New Data.DataColumn("kensa_in_nm6"))
            .Add(New Data.DataColumn("kensa_in_nm7"))
            .Add(New Data.DataColumn("kensa_in_nm8"))
            .Add(New Data.DataColumn("kensa_in_nm9"))
            .Add(New Data.DataColumn("kensa_in_nm10"))
            .Add(New Data.DataColumn("add_login_user_id"))
            .Add(New Data.DataColumn("add_datetime"))
            .Add(New Data.DataColumn("upd_login_user_id"))
            .Add(New Data.DataColumn("upd_datetime"))
            .Add(New Data.DataColumn("gaiyou_you"))
            .Add(New Data.DataColumn("shijisyo_you"))
        End With

        Return dt
    End Function

    Private Function InsObj(ByVal str As String) As Object
        If String.IsNullOrEmpty(str) Then
            Return DBNull.Value
        Else
            Return str
        End If
    End Function

    ''' <summary>
    ''' ファイルを移動する
    ''' </summary>
    ''' <param name="fileFrom">目標ファイル</param>
    ''' <param name="moveToPath">移動先フォルダのパス</param>
    ''' <param name="newFileName">新しいファイル名(指定しない場合、元ファイル名を使用する)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2015/12/04 高兵兵(大連) 新規作成</para>
    ''' </history>
    Private Function MoveFile(ByVal fileFrom As FileInfo, ByVal moveToPath As String, Optional ByVal newFileName As String = "") As Boolean
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                fileFrom, moveToPath, newFileName)
        Try
            fileFrom.CopyTo(moveToPath & IIf(newFileName.Equals(String.Empty), fileFrom.Name, newFileName).ToString, True)
            fileFrom.Delete()
            Return True
        Catch ex As Exception
            If IsNothing(ex.Data.Item("ERROR_LOG")) Then
                ex.Data.Add("ERROR_LOG", "ファイルの移動処理が異常終了しました。")
            End If
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' エラーフォルダへ移動する
    ''' </summary>
    ''' <param name="fileFrom">目標ファイル</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2015/12/04 高兵兵(大連) 新規作成</para>
    ''' </history>
    Private Function MoveErrFile(ByVal fileFrom As FileInfo, ByVal path As String) As Boolean
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                fileFrom, path)
        Try
            '検査報告書エラーフォルダ名を取得する
            Dim ErrorFolderName As String = Definition.GetKensaHoukokusyoCopyErrorFolderName

            'エラーフォルダの存在チェック
            If Not Directory.Exists(path & ErrorFolderName) Then
                'エラーフォルダがない場合、作成する
                Directory.CreateDirectory(path & ErrorFolderName)
            End If

            '移動
            fileFrom.CopyTo(path & ErrorFolderName & "\" & fileFrom.Name, True)
            fileFrom.Delete()
            Return True
        Catch ex As Exception
            If IsNothing(ex.Data.Item("ERROR_LOG")) Then
                ex.Data.Add("ERROR_LOG", "エラーフォルダへ移動処理が異常終了しました。")
            End If
            Throw ex
        End Try

    End Function

#End Region

#Region "DB処理"
    ''' <summary>
    ''' 加盟店情報を取得する
    ''' </summary>
    ''' <param name="objConnection">DB操作機能</param>
    ''' <param name="kbn">区分</param>
    ''' <param name="kameitenCd">加盟店コード</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2015/12/04 高兵兵(大連) 新規作成</para>
    ''' </history>
    Private Function GetKameitenInfoFromEarth(ByVal objConnection As SqlExecutor, ByVal kbn As String, ByVal kameitenCd As String) As Data.DataTable
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                objConnection, kbn, kameitenCd)
        'SQLコメント
        Dim sqlBuffer As New System.Text.StringBuilder

        'バラメタ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        Try
            'SQL文	
            With sqlBuffer
                .AppendLine("SELECT ")
                .AppendLine("	MK.kbn ") '--区分 ")
                .AppendLine("	,MK.kameiten_cd ") '--加盟店コード ")
                .AppendLine("	,MK.kameiten_mei1 ") '--加盟店名1 ")
                .AppendLine("	,ISNULL(MKTJ.kensa_hkks_busuu,0) AS kensa_hkks_busuu ") '--検査報告書部数 ")
                .AppendLine("	,MKTJ.kensa_hkks_busuu ") '--検査報告書部数 ")
                .AppendLine("	,MKJ.jyuusyo1 ") '--住所1 ")
                .AppendLine("	,MKJ.jyuusyo2 ") '--住所2 ")
                .AppendLine("	,MKJ.yuubin_no ") '--郵便番号 ")
                .AppendLine("	,MKJ.tel_no ") '--電話番号 ")
                .AppendLine("	,MKJ.busyo_mei ") '--部署名 ")
                .AppendLine("	,MK.todouhuken_cd ") '--都道府県コード ")
                .AppendLine("	,MT.todouhuken_mei ") '--都道府県名 ")
                .AppendLine("FROM ")
                .AppendLine("	m_kameiten AS MK ") '--加盟店マスタ ")
                .AppendLine("	LEFT JOIN ")
                .AppendLine("	m_kameiten_jyuusyo AS MKJ ") '--加盟店住所マスタ ")
                .AppendLine("		ON ")
                .AppendLine("		MKJ.kameiten_cd = MK.kameiten_cd ") '--加盟店コード ")
                .AppendLine("		AND ")
                .AppendLine("		MKJ.kensa_hkks_flg = '-1' ") '--検査報告書FLG ")
                .AppendLine("	LEFT JOIN ")
                .AppendLine("	m_kameiten_torihiki_jouhou AS MKTJ ") '--加盟店取引情報マスタ ")
                .AppendLine("		ON ")
                .AppendLine("		MKTJ.kameiten_cd = MK.kameiten_cd ") '--加盟店コード ")
                .AppendLine("	LEFT JOIN ")
                .AppendLine("	m_todoufuken AS MT ") '--都道府県マスタ ")
                .AppendLine("		ON ")
                .AppendLine("		MK.todouhuken_cd = MT.todouhuken_cd ") '--都道府県コード ")
                .AppendLine("WHERE ")
                .AppendLine("	MK.kbn = @kbn ") '--区分 ")
                .AppendLine("	AND ")
                .AppendLine("	MK.kameiten_cd = @kameiten_cd ") '--加盟店コード ")
            End With

            'バラメタ
            paramList.Add(MakeParam("@kbn", SqlDbType.VarChar, 1, kbn))
            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, kameitenCd))

            'Ｅａｒｔｈのデータ
            Return objConnection.ExecuteDataTable(CommandType.Text, sqlBuffer.ToString(), paramList.ToArray())


        Catch ex As Exception
            If IsNothing(ex.Data.Item("ERROR_LOG")) Then
                ex.Data.Add("ERROR_LOG", "加盟店情報の取得処理が異常終了しました。")
            End If
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' 地盤情報を取得する
    ''' </summary>
    ''' <param name="objConnection">DB操作機能</param>
    ''' <param name="kbn">区分</param>
    ''' <param name="hosyousyoNo">保証書NO</param>
    ''' <param name="kameitenCd">加盟店コード</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2015/12/04 高兵兵(大連) 新規作成</para>
    ''' </history>
    Private Function GetJibanInfoFromEarth(ByVal objConnection As SqlExecutor, ByVal kbn As String, ByVal hosyousyoNo As String, ByVal kameitenCd As String) As Data.DataTable
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                objConnection, kbn, hosyousyoNo, kameitenCd)
        'SQLコメント
        Dim sqlBuffer As New System.Text.StringBuilder

        'バラメタ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        Try
            'SQL文	
            With sqlBuffer
                .AppendLine("SELECT ")
                .AppendLine("	kbn ") '--区分 ")
                .AppendLine("	,hosyousyo_no ") '--保証書NO ")
                .AppendLine("	,sesyu_mei ") '--施主名 ")
                .AppendLine("	,bukken_jyuusyo1 ") '--物件住所1 ")
                .AppendLine("	,bukken_jyuusyo2 ") '--物件住所2 ")
                .AppendLine("	,bukken_jyuusyo3 ") '--物件住所3 ")
                .AppendLine("FROM ")
                .AppendLine("	t_jiban ") '--地盤テーブル ")
                .AppendLine("WHERE ")
                .AppendLine("	kbn = @kbn ") '--区分 ")
                .AppendLine("	AND ")
                .AppendLine("	hosyousyo_no = @hosyousyo_no ") '--保証書NO ")
                .AppendLine("	AND ")
                .AppendLine("	kameiten_cd = @kameiten_cd ") '--加盟店コード ")
            End With

            'バラメタ
            paramList.Add(MakeParam("@kbn", SqlDbType.VarChar, 1, kbn))
            paramList.Add(MakeParam("@hosyousyo_no", SqlDbType.VarChar, 10, hosyousyoNo))
            paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, kameitenCd))

            'Ｅａｒｔｈのデータ
            Return objConnection.ExecuteDataTable(CommandType.Text, sqlBuffer.ToString(), paramList.ToArray())

        Catch ex As Exception
            If IsNothing(ex.Data.Item("ERROR_LOG")) Then
                ex.Data.Add("ERROR_LOG", "地盤情報の取得処理が異常終了しました。")
            End If
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' 受注情報を取得する
    ''' </summary>
    ''' <param name="objConnection">DB操作機能</param>
    ''' <param name="kbn">区分</param>
    ''' <param name="hosyousyoNo">保証書NO</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2015/12/04 高兵兵(大連) 新規作成</para>
    ''' </history>
    Private Function GetJyutyuuInfoFromJhsfgm(ByVal objConnection As SqlExecutor, ByVal kbn As String, ByVal hosyousyoNo As String) As Data.DataTable
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                objConnection, kbn, hosyousyoNo)
        'SQLコメント
        Dim sqlBuffer As New System.Text.StringBuilder

        'バラメタ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        Try
            'SQL文	
            With sqlBuffer
                .AppendLine("SELECT ")
                .AppendLine("	TJ.kbn ") '--区分 ")
                .AppendLine("	,TJ.hosyousyo_no ") '--保証書NO ")
                .AppendLine("	,MTK.tatemono_kouzou ") '--建物構造 ")
                .AppendLine("	,TJ.tatemono_kaisu ") '--建物階数 ")
                .AppendLine("	,MF.fc_nm ") '--FC名 ")
                .AppendLine("	,TJ.kameiten_tanto ") '--依頼担当者名 ")
                .AppendLine("	,TJ.kameiten_cd ") '--建物加盟店コード ")
                .AppendLine("	,MTK.gaiyou_you ") '--建物構造_概要用 ")
                .AppendLine("	,MTK.shijisyo_you ") '--建物構造_指示書用 ")
                .AppendLine("FROM ")
                .AppendLine("	t_jyucyu AS TJ ")
                .AppendLine("	LEFT JOIN ")
                .AppendLine("	m_fc AS MF ")
                .AppendLine("		ON ")
                .AppendLine("		MF.fc_cd = TJ.fc_cd ")
                .AppendLine("	LEFT JOIN ")
                .AppendLine("	m_tatemono_kouzou AS MTK ")
                .AppendLine("		ON ")
                .AppendLine("		MTK.tatemono_kouzou_cd = TJ.tatemono_kouzou_cd ")
                .AppendLine("WHERE ")
                .AppendLine("	TJ.kbn = @kbn ") '--区分 ")
                .AppendLine("	and ")
                .AppendLine("	TJ.hosyousyo_no = @hosyousyo_no ") '--保証書NO ")
            End With

            'バラメタ
            paramList.Add(MakeParam("@kbn", SqlDbType.VarChar, 1, kbn))
            paramList.Add(MakeParam("@hosyousyo_no", SqlDbType.VarChar, 10, hosyousyoNo))

            'Jhsfgmのデータ
            Return objConnection.ExecuteDataTable(CommandType.Text, sqlBuffer.ToString(), paramList.ToArray())

        Catch ex As Exception
            If IsNothing(ex.Data.Item("ERROR_LOG")) Then
                ex.Data.Add("ERROR_LOG", "受注情報の取得処理が異常終了しました。")
            End If
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' 検査情報を取得する
    ''' </summary>
    ''' <param name="objConnection">DB操作機能</param>
    ''' <param name="kbn">区分</param>
    ''' <param name="hosyousyoNo">保証書NO</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2015/12/04 高兵兵(大連) 新規作成</para>
    ''' </history>
    Private Function GetKensaInfoFromJhsfgm(ByVal objConnection As SqlExecutor, ByVal kbn As String, ByVal hosyousyoNo As String) As Data.DataTable
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                objConnection, kbn, hosyousyoNo)
        'SQLコメント
        Dim sqlBuffer As New System.Text.StringBuilder

        'バラメタ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        Try
            'SQL文	
            With sqlBuffer
                .AppendLine("SELECT ")
                .AppendLine("	TK.kbn  ") '--区分 ")
                .AppendLine("	,TK.hosyousyo_no  ") '--保証書NO ")
                .AppendLine("	,TK.kensa_oiban  ") '-- ")
                .AppendLine("	,MKK.kensa_koutei_nm  ") '--検査工程名 ")
                .AppendLine("	,MKK.disp_no  ") '-- ")
                .AppendLine("	,TK.kensa_start_jissibi  ") '--検査実施日 ")  
                .AppendLine("	,MK.kensain_nm  ") '--検査員名 ")

                .AppendLine("FROM ")
                .AppendLine("	t_kensa AS TK ")
                .AppendLine("	LEFT JOIN ")
                .AppendLine("	m_kensa_koutei AS MKK ")
                .AppendLine("		ON ")
                .AppendLine("		MKK.kensa_koutei_cd = TK.kensa_koutei_cd ")
                .AppendLine("	LEFT JOIN ")
                .AppendLine("	m_kensain AS MK ")
                .AppendLine("		ON ")
                .AppendLine("		MK.fc_cd = TK.fc_cd_jissiin ")
                .AppendLine("		AND ")
                .AppendLine("		MK.kensain_cd = TK.kensa_jissiin_cd ")
                .AppendLine("WHERE ")
                .AppendLine("	TK.kbn = @kbn ")
                .AppendLine("	AND ")
                .AppendLine("	TK.hosyousyo_no = @hosyousyo_no ")
                .AppendLine("	AND ")
                .AppendLine("	TK.kensa_oiban = 0 ")
                .AppendLine("	AND ")
                .AppendLine("	TK.kensa_start_jissibi IS NOT NULL ")
                .AppendLine("	AND ")
                .AppendLine("	MK.kensain_cd IS NOT NULL ")
                .AppendLine("ORDER BY ")
                .AppendLine("	MKK.disp_no ")
            End With

            'バラメタ
            paramList.Add(MakeParam("@kbn", SqlDbType.VarChar, 1, kbn))
            paramList.Add(MakeParam("@hosyousyo_no", SqlDbType.VarChar, 10, hosyousyoNo))

            'Jhsfgmのデータ
            Return objConnection.ExecuteDataTable(CommandType.Text, sqlBuffer.ToString(), paramList.ToArray())

        Catch ex As Exception
            If IsNothing(ex.Data.Item("ERROR_LOG")) Then
                ex.Data.Add("ERROR_LOG", "検査情報の取得処理が異常終了しました。")
            End If
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' 検査報告書管理テーブルを追加
    ''' </summary>
    ''' <param name="objConnection">DB操作機能</param>
    ''' <param name="dtKensa">データ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2015/12/04 高兵兵(大連) 新規作成</para>
    ''' </history>
    Public Function InstKensaHkksKanri(ByVal objConnection As SqlExecutor, ByVal dtKensa As DataTable) As Boolean
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                objConnection, dtKensa)
        '戻り値
        Dim InsCount As Integer = 0

        'SQLコメント
        Dim commandTextSb As New System.Text.StringBuilder

        'パラメータ格納
        Dim paramList As New List(Of SqlClient.SqlParameter)

        'SQL文
        commandTextSb.AppendLine("INSERT INTO t_kensa_hkks_kanri WITH(UPDLOCK)")
        commandTextSb.AppendLine("	( ")
        commandTextSb.AppendLine("		kbn")
        commandTextSb.AppendLine("		,hosyousyo_no")
        commandTextSb.AppendLine("		,kameiten_cd")
        commandTextSb.AppendLine("		,kakunou_date")
        commandTextSb.AppendLine("		,kameiten_mei")
        commandTextSb.AppendLine("		,sesyu_mei")
        commandTextSb.AppendLine("		,torikesi")
        commandTextSb.AppendLine("		,kensa_hkks_busuu")
        commandTextSb.AppendLine("		,kensa_hkks_jyuusyo1")
        commandTextSb.AppendLine("		,kensa_hkks_jyuusyo2")
        commandTextSb.AppendLine("		,yuubin_no")
        commandTextSb.AppendLine("		,tel_no")
        commandTextSb.AppendLine("		,busyo_mei")
        commandTextSb.AppendLine("		,todouhuken_cd")
        commandTextSb.AppendLine("		,todouhuken_mei")
        commandTextSb.AppendLine("		,hassou_date")
        commandTextSb.AppendLine("		,hassou_date_in_flg")
        commandTextSb.AppendLine("		,souhu_tantousya")
        commandTextSb.AppendLine("		,bukken_jyuusyo1")
        commandTextSb.AppendLine("		,bukken_jyuusyo2")
        commandTextSb.AppendLine("		,bukken_jyuusyo3")
        commandTextSb.AppendLine("		,tatemono_kouzou")
        commandTextSb.AppendLine("		,tatemono_kaisu")
        commandTextSb.AppendLine("		,fc_nm")
        commandTextSb.AppendLine("		,kameiten_tanto")
        commandTextSb.AppendLine("		,tatemono_kameiten_cd")
        commandTextSb.AppendLine("		,kanrihyou_out_flg")
        commandTextSb.AppendLine("		,kanrihyou_out_date")
        commandTextSb.AppendLine("		,souhujyou_out_flg")
        commandTextSb.AppendLine("		,souhujyou_out_date")
        commandTextSb.AppendLine("		,kensa_hkks_out_flg")
        commandTextSb.AppendLine("		,kensa_hkks_out_date")
        commandTextSb.AppendLine("		,tooshi_no")
        commandTextSb.AppendLine("		,kensa_koutei_nm1")
        commandTextSb.AppendLine("		,kensa_koutei_nm2")
        commandTextSb.AppendLine("		,kensa_koutei_nm3")
        commandTextSb.AppendLine("		,kensa_koutei_nm4")
        commandTextSb.AppendLine("		,kensa_koutei_nm5")
        commandTextSb.AppendLine("		,kensa_koutei_nm6")
        commandTextSb.AppendLine("		,kensa_koutei_nm7")
        commandTextSb.AppendLine("		,kensa_koutei_nm8")
        commandTextSb.AppendLine("		,kensa_koutei_nm9")
        commandTextSb.AppendLine("		,kensa_koutei_nm10")
        commandTextSb.AppendLine("		,kensa_start_jissibi1")
        commandTextSb.AppendLine("		,kensa_start_jissibi2")
        commandTextSb.AppendLine("		,kensa_start_jissibi3")
        commandTextSb.AppendLine("		,kensa_start_jissibi4")
        commandTextSb.AppendLine("		,kensa_start_jissibi5")
        commandTextSb.AppendLine("		,kensa_start_jissibi6")
        commandTextSb.AppendLine("		,kensa_start_jissibi7")
        commandTextSb.AppendLine("		,kensa_start_jissibi8")
        commandTextSb.AppendLine("		,kensa_start_jissibi9")
        commandTextSb.AppendLine("		,kensa_start_jissibi10")
        commandTextSb.AppendLine("		,kensa_in_nm1")
        commandTextSb.AppendLine("		,kensa_in_nm2")
        commandTextSb.AppendLine("		,kensa_in_nm3")
        commandTextSb.AppendLine("		,kensa_in_nm4")
        commandTextSb.AppendLine("		,kensa_in_nm5")
        commandTextSb.AppendLine("		,kensa_in_nm6")
        commandTextSb.AppendLine("		,kensa_in_nm7")
        commandTextSb.AppendLine("		,kensa_in_nm8")
        commandTextSb.AppendLine("		,kensa_in_nm9")
        commandTextSb.AppendLine("		,kensa_in_nm10")
        commandTextSb.AppendLine("		,add_login_user_id")
        commandTextSb.AppendLine("		,add_datetime")
        commandTextSb.AppendLine("		,upd_login_user_id")
        commandTextSb.AppendLine("		,upd_datetime")
        commandTextSb.AppendLine("		,gaiyou_you")
        commandTextSb.AppendLine("		,shijisyo_you")
        commandTextSb.AppendLine(" )      ")
        commandTextSb.AppendLine(" VALUES ")
        commandTextSb.AppendLine(" (      ")
        commandTextSb.AppendLine("		@kbn")
        commandTextSb.AppendLine("		,@hosyousyo_no")
        commandTextSb.AppendLine("		,@kameiten_cd")
        commandTextSb.AppendLine("		,@kakunou_date")
        commandTextSb.AppendLine("		,@kameiten_mei")
        commandTextSb.AppendLine("		,@sesyu_mei")
        commandTextSb.AppendLine("		,@torikesi")
        commandTextSb.AppendLine("		,@kensa_hkks_busuu")
        commandTextSb.AppendLine("		,@kensa_hkks_jyuusyo1")
        commandTextSb.AppendLine("		,@kensa_hkks_jyuusyo2")
        commandTextSb.AppendLine("		,@yuubin_no")
        commandTextSb.AppendLine("		,@tel_no")
        commandTextSb.AppendLine("		,@busyo_mei")
        commandTextSb.AppendLine("		,@todouhuken_cd")
        commandTextSb.AppendLine("		,@todouhuken_mei")
        commandTextSb.AppendLine("		,@hassou_date")
        commandTextSb.AppendLine("		,@hassou_date_in_flg")
        commandTextSb.AppendLine("		,@souhu_tantousya")
        commandTextSb.AppendLine("		,@bukken_jyuusyo1")
        commandTextSb.AppendLine("		,@bukken_jyuusyo2")
        commandTextSb.AppendLine("		,@bukken_jyuusyo3")
        commandTextSb.AppendLine("		,@tatemono_kouzou")
        commandTextSb.AppendLine("		,@tatemono_kaisu")
        commandTextSb.AppendLine("		,@fc_nm")
        commandTextSb.AppendLine("		,@kameiten_tanto")
        commandTextSb.AppendLine("		,@tatemono_kameiten_cd")
        commandTextSb.AppendLine("		,@kanrihyou_out_flg")
        commandTextSb.AppendLine("		,@kanrihyou_out_date")
        commandTextSb.AppendLine("		,@souhujyou_out_flg")
        commandTextSb.AppendLine("		,@souhujyou_out_date")
        commandTextSb.AppendLine("		,@kensa_hkks_out_flg")
        commandTextSb.AppendLine("		,@kensa_hkks_out_date")
        commandTextSb.AppendLine("		,@tooshi_no")
        commandTextSb.AppendLine("		,@kensa_koutei_nm1")
        commandTextSb.AppendLine("		,@kensa_koutei_nm2")
        commandTextSb.AppendLine("		,@kensa_koutei_nm3")
        commandTextSb.AppendLine("		,@kensa_koutei_nm4")
        commandTextSb.AppendLine("		,@kensa_koutei_nm5")
        commandTextSb.AppendLine("		,@kensa_koutei_nm6")
        commandTextSb.AppendLine("		,@kensa_koutei_nm7")
        commandTextSb.AppendLine("		,@kensa_koutei_nm8")
        commandTextSb.AppendLine("		,@kensa_koutei_nm9")
        commandTextSb.AppendLine("		,@kensa_koutei_nm10")
        commandTextSb.AppendLine("		,@kensa_start_jissibi1")
        commandTextSb.AppendLine("		,@kensa_start_jissibi2")
        commandTextSb.AppendLine("		,@kensa_start_jissibi3")
        commandTextSb.AppendLine("		,@kensa_start_jissibi4")
        commandTextSb.AppendLine("		,@kensa_start_jissibi5")
        commandTextSb.AppendLine("		,@kensa_start_jissibi6")
        commandTextSb.AppendLine("		,@kensa_start_jissibi7")
        commandTextSb.AppendLine("		,@kensa_start_jissibi8")
        commandTextSb.AppendLine("		,@kensa_start_jissibi9")
        commandTextSb.AppendLine("		,@kensa_start_jissibi10")
        commandTextSb.AppendLine("		,@kensa_in_nm1")
        commandTextSb.AppendLine("		,@kensa_in_nm2")
        commandTextSb.AppendLine("		,@kensa_in_nm3")
        commandTextSb.AppendLine("		,@kensa_in_nm4")
        commandTextSb.AppendLine("		,@kensa_in_nm5")
        commandTextSb.AppendLine("		,@kensa_in_nm6")
        commandTextSb.AppendLine("		,@kensa_in_nm7")
        commandTextSb.AppendLine("		,@kensa_in_nm8")
        commandTextSb.AppendLine("		,@kensa_in_nm9")
        commandTextSb.AppendLine("		,@kensa_in_nm10")
        commandTextSb.AppendLine("		,@add_login_user_id")
        commandTextSb.AppendLine("		,@add_datetime")
        commandTextSb.AppendLine("		,@upd_login_user_id")
        commandTextSb.AppendLine("		,@upd_datetime")
        commandTextSb.AppendLine("		,@gaiyou_you")
        commandTextSb.AppendLine("		,@shijisyo_you")
        commandTextSb.AppendLine("   ) ")

        paramList.Add(MakeParam("@kbn", SqlDbType.Char, 1, InsObj(dtKensa.Rows(0).Item("kbn").ToString.Trim)))  '区分
        paramList.Add(MakeParam("@hosyousyo_no", SqlDbType.VarChar, 10, InsObj(dtKensa.Rows(0).Item("hosyousyo_no").ToString.Trim)))  '保証書NO
        paramList.Add(MakeParam("@kameiten_cd", SqlDbType.VarChar, 5, InsObj(dtKensa.Rows(0).Item("kameiten_cd").ToString.Trim)))  '加盟店コード
        paramList.Add(MakeParam("@kakunou_date", SqlDbType.DateTime, 30, InsObj(dtKensa.Rows(0).Item("kakunou_date").ToString.Trim)))  '格納日
        paramList.Add(MakeParam("@kameiten_mei", SqlDbType.VarChar, 40, InsObj(dtKensa.Rows(0).Item("kameiten_mei").ToString.Trim)))  '加盟店名
        paramList.Add(MakeParam("@sesyu_mei", SqlDbType.VarChar, 50, InsObj(dtKensa.Rows(0).Item("sesyu_mei").ToString.Trim))) '施主名
        paramList.Add(MakeParam("@torikesi", SqlDbType.VarChar, 1, InsObj(dtKensa.Rows(0).Item("torikesi").ToString.Trim)))  '取消	
        paramList.Add(MakeParam("@kensa_hkks_busuu", SqlDbType.Int, 1, InsObj(dtKensa.Rows(0).Item("kensa_hkks_busuu").ToString.Trim)))  '検査報告書発行部数	
        paramList.Add(MakeParam("@kensa_hkks_jyuusyo1", SqlDbType.VarChar, 40, InsObj(dtKensa.Rows(0).Item("kensa_hkks_jyuusyo1").ToString.Trim)))   '検査報告書送付先住所1	
        paramList.Add(MakeParam("@kensa_hkks_jyuusyo2", SqlDbType.VarChar, 30, InsObj(dtKensa.Rows(0).Item("kensa_hkks_jyuusyo2").ToString.Trim)))   '検査報告書送付先住所2	
        paramList.Add(MakeParam("@yuubin_no", SqlDbType.VarChar, 8, InsObj(dtKensa.Rows(0).Item("yuubin_no").ToString.Trim))) '郵便番号
        paramList.Add(MakeParam("@tel_no", SqlDbType.VarChar, 16, InsObj(dtKensa.Rows(0).Item("tel_no").ToString.Trim)))    '電話番号	
        paramList.Add(MakeParam("@busyo_mei", SqlDbType.VarChar, 50, InsObj(dtKensa.Rows(0).Item("busyo_mei").ToString.Trim))) '部署名	
        paramList.Add(MakeParam("@todouhuken_cd", SqlDbType.VarChar, 2, InsObj(dtKensa.Rows(0).Item("todouhuken_cd").ToString.Trim))) '都道府県コード	
        paramList.Add(MakeParam("@todouhuken_mei", SqlDbType.VarChar, 10, InsObj(dtKensa.Rows(0).Item("todouhuken_mei").ToString.Trim)))    '都道府県名	
        paramList.Add(MakeParam("@hassou_date", SqlDbType.DateTime, 30, InsObj(dtKensa.Rows(0).Item("hassou_date").ToString.Trim)))   '発送日	
        paramList.Add(MakeParam("@hassou_date_in_flg", SqlDbType.Int, 1, InsObj(dtKensa.Rows(0).Item("hassou_date_in_flg").ToString.Trim)))    '発送日入力フラグ	
        paramList.Add(MakeParam("@souhu_tantousya", SqlDbType.VarChar, 128, InsObj(dtKensa.Rows(0).Item("souhu_tantousya").ToString.Trim)))   '送付担当者	
        paramList.Add(MakeParam("@bukken_jyuusyo1", SqlDbType.VarChar, 32, InsObj(dtKensa.Rows(0).Item("bukken_jyuusyo1").ToString.Trim)))   '物件住所1	
        paramList.Add(MakeParam("@bukken_jyuusyo2", SqlDbType.VarChar, 32, InsObj(dtKensa.Rows(0).Item("bukken_jyuusyo2").ToString.Trim)))   '物件住所2
        paramList.Add(MakeParam("@bukken_jyuusyo3", SqlDbType.VarChar, 54, InsObj(dtKensa.Rows(0).Item("bukken_jyuusyo3").ToString.Trim)))   '物件住所3	
        paramList.Add(MakeParam("@tatemono_kouzou", SqlDbType.Char, 30, InsObj(dtKensa.Rows(0).Item("tatemono_kouzou").ToString.Trim)))   '建物構造	
        paramList.Add(MakeParam("@tatemono_kaisu", SqlDbType.VarChar, 20, InsObj(dtKensa.Rows(0).Item("tatemono_kaisu").ToString.Trim)))    '建物階数	
        paramList.Add(MakeParam("@fc_nm", SqlDbType.VarChar, 40, InsObj(dtKensa.Rows(0).Item("fc_nm").ToString.Trim))) 'FC名	
        paramList.Add(MakeParam("@kameiten_tanto", SqlDbType.VarChar, 40, InsObj(dtKensa.Rows(0).Item("kameiten_tanto").ToString.Trim)))    '依頼担当者名	
        paramList.Add(MakeParam("@tatemono_kameiten_cd", SqlDbType.Char, 10, InsObj(dtKensa.Rows(0).Item("tatemono_kameiten_cd").ToString.Trim)))  '建物加盟店コード	
        paramList.Add(MakeParam("@kanrihyou_out_flg", SqlDbType.Int, 1, InsObj(dtKensa.Rows(0).Item("kanrihyou_out_flg").ToString.Trim))) '管理表出力フラグ	
        paramList.Add(MakeParam("@kanrihyou_out_date", SqlDbType.DateTime, 30, InsObj(dtKensa.Rows(0).Item("kanrihyou_out_date").ToString.Trim)))    '管理表出力日	
        paramList.Add(MakeParam("@souhujyou_out_flg", SqlDbType.Int, 1, InsObj(dtKensa.Rows(0).Item("souhujyou_out_flg").ToString.Trim))) '送付状出力フラグ	
        paramList.Add(MakeParam("@souhujyou_out_date", SqlDbType.DateTime, 30, InsObj(dtKensa.Rows(0).Item("souhujyou_out_date").ToString.Trim)))    '送付状出力日	
        paramList.Add(MakeParam("@kensa_hkks_out_flg", SqlDbType.Int, 1, InsObj(dtKensa.Rows(0).Item("kensa_hkks_out_flg").ToString.Trim)))    '検査報告書出力フラグ	
        paramList.Add(MakeParam("@kensa_hkks_out_date", SqlDbType.DateTime, 30, InsObj(dtKensa.Rows(0).Item("kensa_hkks_out_date").ToString.Trim)))   '検査報告書出力日
        paramList.Add(MakeParam("@tooshi_no", SqlDbType.Int, 50, InsObj(dtKensa.Rows(0).Item("tooshi_no").ToString.Trim)))     '通しNo	
        paramList.Add(MakeParam("@kensa_koutei_nm1", SqlDbType.VarChar, 40, InsObj(dtKensa.Rows(0).Item("kensa_koutei_nm1").ToString.Trim)))  '検査工程名1	
        paramList.Add(MakeParam("@kensa_koutei_nm2", SqlDbType.VarChar, 40, InsObj(dtKensa.Rows(0).Item("kensa_koutei_nm2").ToString.Trim)))  '検査工程名2	
        paramList.Add(MakeParam("@kensa_koutei_nm3", SqlDbType.VarChar, 40, InsObj(dtKensa.Rows(0).Item("kensa_koutei_nm3").ToString.Trim)))  '検査工程名3	
        paramList.Add(MakeParam("@kensa_koutei_nm4", SqlDbType.VarChar, 40, InsObj(dtKensa.Rows(0).Item("kensa_koutei_nm4").ToString.Trim)))   '検査工程名4	
        paramList.Add(MakeParam("@kensa_koutei_nm5", SqlDbType.VarChar, 40, InsObj(dtKensa.Rows(0).Item("kensa_koutei_nm5").ToString.Trim)))   '検査工程名5
        paramList.Add(MakeParam("@kensa_koutei_nm6", SqlDbType.VarChar, 40, InsObj(dtKensa.Rows(0).Item("kensa_koutei_nm6").ToString.Trim)))  '検査工程名6
        paramList.Add(MakeParam("@kensa_koutei_nm7", SqlDbType.VarChar, 40, InsObj(dtKensa.Rows(0).Item("kensa_koutei_nm7").ToString.Trim)))  '検査工程名7
        paramList.Add(MakeParam("@kensa_koutei_nm8", SqlDbType.VarChar, 40, InsObj(dtKensa.Rows(0).Item("kensa_koutei_nm8").ToString.Trim)))  '検査工程8
        paramList.Add(MakeParam("@kensa_koutei_nm9", SqlDbType.VarChar, 40, InsObj(dtKensa.Rows(0).Item("kensa_koutei_nm9").ToString.Trim)))  '検査工程9
        paramList.Add(MakeParam("@kensa_koutei_nm10", SqlDbType.VarChar, 40, InsObj(dtKensa.Rows(0).Item("kensa_koutei_nm10").ToString.Trim)))  '検査工程名10
        paramList.Add(MakeParam("@kensa_start_jissibi1", SqlDbType.DateTime, 30, InsObj(dtKensa.Rows(0).Item("kensa_start_jissibi1").ToString.Trim)))  '検査実施日1
        paramList.Add(MakeParam("@kensa_start_jissibi2", SqlDbType.DateTime, 30, InsObj(dtKensa.Rows(0).Item("kensa_start_jissibi2").ToString.Trim)))  '検査実施日2
        paramList.Add(MakeParam("@kensa_start_jissibi3", SqlDbType.DateTime, 30, InsObj(dtKensa.Rows(0).Item("kensa_start_jissibi3").ToString.Trim)))  '検査実施日3
        paramList.Add(MakeParam("@kensa_start_jissibi4", SqlDbType.DateTime, 30, InsObj(dtKensa.Rows(0).Item("kensa_start_jissibi4").ToString.Trim)))  '検査実施日4
        paramList.Add(MakeParam("@kensa_start_jissibi5", SqlDbType.DateTime, 30, InsObj(dtKensa.Rows(0).Item("kensa_start_jissibi5").ToString.Trim)))  '検査実施日5
        paramList.Add(MakeParam("@kensa_start_jissibi6", SqlDbType.DateTime, 30, InsObj(dtKensa.Rows(0).Item("kensa_start_jissibi6").ToString.Trim)))  '検査実施日6
        paramList.Add(MakeParam("@kensa_start_jissibi7", SqlDbType.DateTime, 30, InsObj(dtKensa.Rows(0).Item("kensa_start_jissibi7").ToString.Trim)))  '検査実施日7
        paramList.Add(MakeParam("@kensa_start_jissibi8", SqlDbType.DateTime, 30, InsObj(dtKensa.Rows(0).Item("kensa_start_jissibi8").ToString.Trim)))  '検査実施日8
        paramList.Add(MakeParam("@kensa_start_jissibi9", SqlDbType.DateTime, 30, InsObj(dtKensa.Rows(0).Item("kensa_start_jissibi9").ToString.Trim)))  '検査実施日9
        paramList.Add(MakeParam("@kensa_start_jissibi10", SqlDbType.DateTime, 30, InsObj(dtKensa.Rows(0).Item("kensa_start_jissibi10").ToString.Trim)))  '検査実施日10
        paramList.Add(MakeParam("@kensa_in_nm1", SqlDbType.VarChar, 40, InsObj(dtKensa.Rows(0).Item("kensa_in_nm1").ToString.Trim)))  '検査員名1
        paramList.Add(MakeParam("@kensa_in_nm2", SqlDbType.VarChar, 40, InsObj(dtKensa.Rows(0).Item("kensa_in_nm2").ToString.Trim)))  '検査員名2
        paramList.Add(MakeParam("@kensa_in_nm3", SqlDbType.VarChar, 40, InsObj(dtKensa.Rows(0).Item("kensa_in_nm3").ToString.Trim)))  '検査員名3
        paramList.Add(MakeParam("@kensa_in_nm4", SqlDbType.VarChar, 40, InsObj(dtKensa.Rows(0).Item("kensa_in_nm4").ToString.Trim)))  '検査員名4
        paramList.Add(MakeParam("@kensa_in_nm5", SqlDbType.VarChar, 40, InsObj(dtKensa.Rows(0).Item("kensa_in_nm5").ToString.Trim)))  '検査員名5
        paramList.Add(MakeParam("@kensa_in_nm6", SqlDbType.VarChar, 40, InsObj(dtKensa.Rows(0).Item("kensa_in_nm6").ToString.Trim)))  '検査員名6
        paramList.Add(MakeParam("@kensa_in_nm7", SqlDbType.VarChar, 40, InsObj(dtKensa.Rows(0).Item("kensa_in_nm7").ToString.Trim)))  '検査員名7
        paramList.Add(MakeParam("@kensa_in_nm8", SqlDbType.VarChar, 40, InsObj(dtKensa.Rows(0).Item("kensa_in_nm8").ToString.Trim)))  '検査員名8
        paramList.Add(MakeParam("@kensa_in_nm9", SqlDbType.VarChar, 40, InsObj(dtKensa.Rows(0).Item("kensa_in_nm9").ToString.Trim)))  '検査員名9
        paramList.Add(MakeParam("@kensa_in_nm10", SqlDbType.VarChar, 40, InsObj(dtKensa.Rows(0).Item("kensa_in_nm10").ToString.Trim)))  '検査員名10
        paramList.Add(MakeParam("@add_login_user_id", SqlDbType.VarChar, 30, InsObj(dtKensa.Rows(0).Item("add_login_user_id").ToString.Trim))) '登録ログインユーザID	
        paramList.Add(MakeParam("@add_datetime", SqlDbType.DateTime, 30, InsObj(dtKensa.Rows(0).Item("add_datetime").ToString.Trim)))  '登録日時	
        paramList.Add(MakeParam("@upd_login_user_id", SqlDbType.VarChar, 30, InsObj(dtKensa.Rows(0).Item("upd_login_user_id").ToString.Trim))) '更新ログインユーザID	
        paramList.Add(MakeParam("@upd_datetime", SqlDbType.DateTime, 30, InsObj(dtKensa.Rows(0).Item("upd_datetime").ToString.Trim)))  '更新日時	
        paramList.Add(MakeParam("@gaiyou_you", SqlDbType.VarChar, 30, InsObj(dtKensa.Rows(0).Item("gaiyou_you").ToString.Trim))) '建物構造_概要用
        paramList.Add(MakeParam("@shijisyo_you", SqlDbType.VarChar, 10, InsObj(dtKensa.Rows(0).Item("shijisyo_you").ToString.Trim)))  '建物構造_指示書用	

        '更新されたデータセットを DB へ書き込み 
        Try
            InsCount = objConnection.ExecuteNonQuery(CommandType.Text, commandTextSb.ToString(), paramList.ToArray())
            If Not InsCount > 0 Then
                Throw New Exception
            End If
            Return True

        Catch ex As Exception
            If IsNothing(ex.Data.Item("ERROR_LOG")) Then
                ex.Data.Add("ERROR_LOG", "検査報告書管理テーブルの登録処理が異常終了しました。")
            End If
            Throw ex
        End Try

    End Function

#End Region

End Class

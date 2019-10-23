Imports System.Text
Imports System.IO
Imports System.Configuration
Imports System.Web
Imports Itis.Framework.Report

''' <summary>
''' FCW(帳票出力)処理管理クラス
''' </summary>
''' <remarks>添付ファイルサーバへdatファイルの作成、FCWサーバへRespons.Redirectの文字列の作成などを行う</remarks>
Public Class FcwManager

    'EMAB障害対応情報格納処理
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    Dim stringLogic As New StringLogic
    Dim messageLogic As New MessageLogic

    ''' <summary>
    ''' FCWの出力形式
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum FcwDriverType
        ''' <summary>CSM出力</summary>
        CSM
    End Enum

    ''' <summary>
    ''' ファイルの書き込み状態
    ''' </summary>
    ''' <remarks>
    ''' ファイル書き込み時の戻り値を定義します。
    ''' </remarks>
    Public Enum FileWriteStatus
        ''' <summary>正常に書き込みした状態を表します。</summary>
        OK
        ''' <summary>OS による I/O エラーなどのアクセス拒否のため、正常に書き込みできない状態を表します。</summary>
        UnauthorizedAccessException
        ''' <summary>引数が有効でないため、正常に書き込みできない状態を表します。</summary>
        ArgumentException
        ''' <summary>引数が null のため、正常に書き込みできない状態を表します。</summary>
        ArgumentNullException
        ''' <summary>ファイル / ディレクトリが存在しないため、正常に書き込みできない状態を表します。</summary>
        DirectoryNotFoundException
        ''' <summary>I/O エラーにより、正常に書き込みできない状態を表します。</summary>
        IOException
        ''' <summary>パス / ファイル名がシステム定義の最大長よりも長いため、正常に書き込みできない状態を表します。</summary>
        PathTooLongException
        ''' <summary>セキュリティ エラーにより、正常に書き込みできない状態を表します。</summary>
        SecurityException
    End Enum


    'FCWサーバへ渡すパラメータ変数定義
    '帳票サーバ定義
    Private ReadOnly paramReportServerUrl As String = ConfigurationManager _
            .AppSettings("ReportServerUrl")
    '帳票データパス
    Private ReadOnly paramDataFilePath As String _
            = ConfigurationManager.AppSettings("ReportTempFileServerName") _
            & ConfigurationManager.AppSettings("ReportTempFilePath")
    'FCWサーバへ引き渡すパラメータ
    Private ReadOnly paramFcwFormDownload As String = "yes"
    Private ReadOnly paramFcwNewSession As String = "yes"
    Private ReadOnly paramFcwEndSession As String = "yes"
    Private Const paramFcwNonOverlay As String = "3"

    Private Shared singleton As New FcwManager()
    ''' <summary>
    ''' コンストラクターです。（インスタンス化を禁止します。）
    ''' </summary>
    Private Sub New()
    End Sub

    ''' <summary>
    ''' インスタンスを取得します。
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property Instance() As FcwManager
        Get
            Return singleton
        End Get
    End Property


    ''' <summary>
    ''' FCWサーバへResponse.Redirectする文字列を作成する
    ''' </summary>
    ''' <param name="selectedFcwDriver">出力形式指定</param>
    ''' <param name="fcpFileName">FCWのフォームファイル名</param>
    ''' <param name="dataFileName">FCWのデータファイル名</param>
    ''' <returns>Response.Redirectの文字列</returns>
    ''' <remarks>引数の出力形式を元にFCWサーバへResponse.Redirectする文字列を作成する</remarks>
    ''' <history>
    ''' <para>2009/11/19 菊川(IC) 帳票サーバ切替対応(共通関数を使用するように変更)</para>
    ''' </history>
    Public Function CreateResponseRedirectString(ByVal selectedFcwDriver As Integer, _
                                                 ByVal fcpFileName As String, _
                                                 ByVal dataFileName As String) As String

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".CreateResponseRedirectString", _
                                            selectedFcwDriver, _
                                            fcpFileName, _
                                            dataFileName)

        Dim fcpFormFile As String = String.Format("JHSEarth\{0}", fcpFileName)
        Dim fcwDataFile As String = String.Format("JHSEarth\{0}", dataFileName)

        Dim reportPrintMode As FcwDriverType        '出力形式
        Dim reportUrl As String                     '帳票(Report)URLを格納

        '出力形式判断
        Select Case selectedFcwDriver
            Case FcwDriverType.CSM
                'CSM出力
                reportPrintMode = PrintMode.Preview
            Case Else
                'プレビュー
                reportPrintMode = PrintMode.Preview
        End Select

        '帳票(Report)URLを取得
        Dim Report As New ReportManager(fcpFormFile, fcwDataFile, PrintMode.Preview)
        reportUrl = Report.URL

        Return reportUrl

    End Function

    ''' <summary>
    ''' データファイル(.dat)の作成
    ''' </summary>
    ''' <returns>書き込み状態</returns>
    ''' <remarks>帳票定義体(フォームデータ)へオーバレイするデータファイル(.dat)を指定のフォルダに作成する。
    ''' 返り値の書き込み状態は、正常、または、StreamWriter の例外に相当する状態が返ります。
    ''' </remarks>
    Public Function CreateFcwDataFile(ByVal sender As Object, _
                                      ByVal dataFileName As String, _
                                      ByVal kbn As String, _
                                      ByVal hosyouno As String, _
                                      ByVal accountno As Integer, _
                                      ByVal stdate As String) As FileWriteStatus

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".CreateFcwDataFile", _
                                            dataFileName, _
                                            kbn, _
                                            hosyouno, _
                                            accountno, _
                                            stdate)

        Dim sb As StringBuilder = New StringBuilder()

        Dim fcwCyohyoContents As String = Me.GetFcwCyohyoContents(sender, kbn, hosyouno, accountno, stdate)
        Try
            Using datafile As FileStream = File.Create(paramDataFilePath & dataFileName)
                Using sw As StreamWriter = New StreamWriter(datafile, System.Text.Encoding.GetEncoding("shift_jis"))
                    'datファイルの作成
                    sw.Write(fcwCyohyoContents)
                End Using
            End Using
        Catch uaEx As UnauthorizedAccessException
            Return FileWriteStatus.UnauthorizedAccessException
        Catch anEx As ArgumentNullException
            Return FileWriteStatus.ArgumentNullException
        Catch aEx As ArgumentException
            Return FileWriteStatus.ArgumentException
        Catch dnfEx As DirectoryNotFoundException
            Return FileWriteStatus.DirectoryNotFoundException
        Catch ptlEx As PathTooLongException
            Return FileWriteStatus.PathTooLongException
        Catch ioEx As IOException
            Return FileWriteStatus.IOException
        Catch sEx As System.Security.SecurityException
            Return FileWriteStatus.SecurityException
        End Try
        Return FileWriteStatus.OK
    End Function

    ''' <summary>
    ''' datファイル出力データの作成
    ''' </summary>
    ''' <returns>datファイルへ出力するデータ</returns>
    ''' <remarks>文字列連結を行いdatファイルへ出力するデータを作成する</remarks>
    ''' <history>
    ''' <para>2009/11/19 菊川(IC) 帳票サーバ切替対応(VERSIONの変更)</para>
    ''' </history>
    Private Function GetFcwCyohyoContents(ByVal sender As Object, _
                                          ByVal kbn As String, _
                                          ByVal hosyouno As String, _
                                          ByVal accountno As Integer, _
                                          ByVal stdate As String) As String

        'メソッド名、引数の情報の退避
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetFcwCyohyoContents", _
                                            kbn, _
                                            hosyouno, _
                                            accountno)

        Dim contents As New StringBuilder()
        Dim logic As New Itis.Earth.BizLogic.Pdf_RenrakuLogic
        Dim ds As DataSet = logic.GetrenrakuSearchData(kbn, hosyouno, accountno)

        Dim dataRow As DataRow = ds.Tables(0).Rows(0)

        Dim yyyy As String
        Dim mm As String
        Dim dd As String
        Dim hiduke As String
        Dim meisyou As String
        Dim source() As Byte
        Dim encoded() As Byte
        Dim destEncoding As Encoding
        Dim jyusyo2 As String
        Dim jyusyo3 As String
        Dim jyusyol2() As Byte
        Dim jyusyol3() As Byte
        Dim encoded2() As Byte
        Dim encoded3() As Byte
        Dim tysyyyy As String
        Dim tysmm As String
        Dim tysdd As String
        Dim tysmei As String
        Dim tysmeil() As Byte
        Dim encoded4() As Byte
        Dim gaijiErrorTarget As String = String.Empty


        '日付作成
        hiduke = DateTime.Today
        yyyy = Left(hiduke, 4)
        mm = Mid(hiduke, 6, 2)
        dd = Mid(hiduke, 9, 2)

        '施主名バイト数取得
        meisyou = (dataRow.Item("sesyu_mei"))
        source = Encoding.Unicode.GetBytes(meisyou)
        destEncoding = Encoding.GetEncoding(932)
        encoded = Encoding.Convert(Encoding.Unicode, destEncoding, source)

        '住所バイト数取得
        If (dataRow.Item("bukken_jyuusyo2")) Is DBNull.Value = False Then
            jyusyo2 = (dataRow.Item("bukken_jyuusyo2"))
            jyusyol2 = Encoding.Unicode.GetBytes(jyusyo2)
            encoded2 = Encoding.Convert(Encoding.Unicode, destEncoding, jyusyol2)
        Else
            jyusyo2 = ""
            jyusyol2 = Encoding.Unicode.GetBytes(jyusyo2)
            encoded2 = Encoding.Convert(Encoding.Unicode, destEncoding, jyusyol2)
        End If

        If (dataRow.Item("bukken_jyuusyo3")) Is DBNull.Value = False Then
            jyusyo3 = (dataRow.Item("bukken_jyuusyo3"))
            jyusyol3 = Encoding.Unicode.GetBytes(jyusyo3)
            encoded3 = Encoding.Convert(Encoding.Unicode, destEncoding, jyusyol3)
        Else
            jyusyo3 = ""
            jyusyol3 = Encoding.Unicode.GetBytes(jyusyo2)
            encoded3 = Encoding.Convert(Encoding.Unicode, destEncoding, jyusyol2)
        End If

        '調査会社名バイト数取得
        tysmei = (dataRow.Item("tys_kaisya_mei"))
        tysmeil = Encoding.Unicode.GetBytes(tysmei)
        destEncoding = Encoding.GetEncoding(932)
        encoded4 = Encoding.Convert(Encoding.Unicode, destEncoding, tysmeil)

        'datファイル作成
        contents.Append("[Control Section]").Append(vbCrLf)
        contents.Append("VERSION=6.3").Append(vbCrLf)
        contents.Append(";").Append(vbCrLf)
        contents.Append("[Fixed Data Section]").Append(vbCrLf)
        '区分＋保証No
        contents.Append("No=")
        contents.Append(dataRow.Item("kbn"))
        contents.Append(dataRow.Item("hosyousyo_no")).Append(vbCrLf)

        '加盟店名
        If (dataRow.Item("kameiten_mei1")) Is DBNull.Value = False Then
            contents.Append("kameiten=" & (dataRow.Item("kameiten_mei1")) & " 御中")
        End If

        '営業所名
        If (dataRow.Item("eigyousyo_mei_inji_umu")) Is DBNull.Value = False Then
            If (dataRow.Item("eigyousyo_mei_inji_umu")) = 1 Then
                contents.Append("(" & (dataRow.Item("eigyousyo_mei")) & ")")
            End If
        End If

        contents.Append("／" & (dataRow.Item("kameiten_cd"))).Append(vbCrLf)

        '担当者名
        If (dataRow.Item("koj_tantou_flg")) Is DBNull.Value = True Then
            If (dataRow.Item("irai_tantousya_mei")) Is DBNull.Value = False Then
                If (dataRow.Item("irai_tantousya_mei")) <> "" Then
                    contents.Append("tantousyamei=")
                    contents.Append(dataRow.Item("irai_tantousya_mei") & "　様").Append(vbCrLf)
                End If
            Else
                contents.Append("tantousyamei=").Append(vbCrLf)
            End If
        Else
            If ((dataRow.Item("koj_tantou_flg")) = "1") And (dataRow.Item("koj_tantousya_mei")) Is DBNull.Value = True Then
                contents.Append("tantousyamei=工事担当者" & "　様").Append(vbCrLf)
            ElseIf (dataRow.Item("koj_tantou_flg")) = 1 And (dataRow.Item("koj_tantousya_mei")) Is DBNull.Value = False Then
                If (dataRow.Item("koj_tantousya_mei")) <> "" Then
                    contents.Append("tantousyamei=" & (dataRow.Item("koj_tantousya_mei")) & "　様").Append(vbCrLf)
                End If
            Else
                contents.Append("tantousyamei=").Append(vbCrLf)
            End If
        End If

        '発行日付
        contents.Append("hiduke=")
        contents.Append(yyyy & "年" & mm & "月" & dd & "日").Append(vbCrLf)

        '名称
        Select Case True
            Case encoded.Length <= 14
                contents.Append("meisyou_14=")
                contents.Append(dataRow.Item("sesyu_mei")).Append(vbCrLf)
            Case encoded.Length <= 16
                contents.Append("meisyou_16=")
                contents.Append(dataRow.Item("sesyu_mei")).Append(vbCrLf)
            Case encoded.Length <= 18
                contents.Append("meisyou_18=")
                contents.Append(dataRow.Item("sesyu_mei")).Append(vbCrLf)
            Case encoded.Length = 19
                contents.Append("meisyou_19=")
                contents.Append(dataRow.Item("sesyu_mei")).Append(vbCrLf)
            Case encoded.Length >= 20
                contents.Append("meisyou_long=")
                contents.Append(dataRow.Item("sesyu_mei")).Append(vbCrLf)
        End Select

        '調査場所
        If encoded2.Length + encoded3.Length <= 64 Then
            contents.Append("jyusyo1=")
            contents.Append(dataRow.Item("bukken_jyuusyo1")).Append(vbCrLf)
            Select Case True
                Case encoded2.Length = 0 AndAlso encoded3.Length = 0
                    contents.Append("jyusyo2=").Append(vbCrLf)
                Case encoded2.Length <> 0 AndAlso encoded3.Length = 0
                    contents.Append("jyusyo2=")
                    contents.Append(dataRow.Item("bukken_jyuusyo2")).Append(vbCrLf)
                Case encoded2.Length = 0 AndAlso encoded3.Length <> 0
                    contents.Append("jyusyo2=")
                    contents.Append(dataRow.Item("bukken_jyuusyo3")).Append(vbCrLf)
                Case encoded2.Length <> 0 AndAlso encoded3.Length <> 0
                    contents.Append("jyusyo2=")
                    contents.Append(dataRow.Item("bukken_jyuusyo2"))
                    contents.Append(dataRow.Item("bukken_jyuusyo3")).Append(vbCrLf)
                Case Else
                    contents.Append("jyusyo2=")
            End Select
        Else
            Select Case True
                Case encoded2.Length = 0
                    contents.Append("jyusyo1=")
                    contents.Append(dataRow.Item("bukken_jyuusyo1"))
                Case encoded2.Length <> 0
                    contents.Append("jyusyo1=")
                    contents.Append(dataRow.Item("bukken_jyuusyo1"))
                    contents.Append(dataRow.Item("bukken_jyuusyo2")).Append(vbCrLf)
            End Select
            If encoded3.Length <> 0 Then
                contents.Append("jyusyo2=")
                contents.Append(dataRow.Item("bukken_jyuusyo3")).Append(vbCrLf)
            Else
                contents.Append("jyusyo2=")
            End If
        End If

        '調査予定日(承諾書調査日)
        dataRow.Item("syoudakusyo_tys_date") = DBNull.Value
        If stdate <> "" Then
            If DateTime.Parse(stdate) > EarthConst.Instance.MAX_DATE Or DateTime.Parse(stdate) < EarthConst.Instance.MIN_DATE Then
            Else
                dataRow.Item("syoudakusyo_tys_date") = Date.Parse(stdate)
            End If
        End If

        If (dataRow.Item("syoudakusyo_tys_date")) Is DBNull.Value = False Then
            tysyyyy = Left((dataRow.Item("syoudakusyo_tys_date")), 4)
            tysmm = Mid((dataRow.Item("syoudakusyo_tys_date")), 6, 2)
            tysdd = Mid((dataRow.Item("syoudakusyo_tys_date")), 9, 2)
            'If (dataRow.Item("tys_kibou_jikan")) Is DBNull.Value = True Then
            If (dataRow.Item("tys_kibou_jikan")) & "" = "" Then
                contents.Append("tyousayoteibi=" & tysyyyy & "年" & tysmm & "月" & tysdd & "日").Append(vbCrLf)
            ElseIf (dataRow.Item("tys_kibou_jikan")) Is DBNull.Value = False Then
                contents.Append("tyousayoteibi=" & tysyyyy & "年" & tysmm & "月" & tysdd & "日").Append(vbCrLf)
                If (dataRow.Item("tys_kibou_jikan")) <> "" Then
                    contents.Append("tyousayoteijikan=(" & (dataRow.Item("tys_kibou_jikan")) & ")").Append(vbCrLf)
                End If
            End If
        End If

        '立会い者
        If (dataRow.Item("tatiai_umu")) Is DBNull.Value = False Then
            Select Case True
                Case (dataRow.Item("tatiai_umu")) = 0
                    contents.Append("check5=ﾚ").Append(vbCrLf)
                Case (dataRow.Item("tatiai_umu")) = 1
                    contents.Append("check1=ﾚ").Append(vbCrLf)
            End Select

            If (dataRow.Item("tatiaisya_cd")) Is DBNull.Value = False Then
                Select Case True
                    Case (dataRow.Item("tatiaisya_cd")) = 0

                    Case (dataRow.Item("tatiaisya_cd")) = 1
                        contents.Append("check2=ﾚ").Append(vbCrLf)
                    Case (dataRow.Item("tatiaisya_cd")) = 2
                        contents.Append("check3=ﾚ").Append(vbCrLf)
                    Case (dataRow.Item("tatiaisya_cd")) = 3
                        contents.Append("check2=ﾚ").Append(vbCrLf)
                        contents.Append("check3=ﾚ").Append(vbCrLf)
                    Case (dataRow.Item("tatiaisya_cd")) = 4
                        contents.Append("check4=ﾚ").Append(vbCrLf)
                    Case (dataRow.Item("tatiaisya_cd")) = 5
                        contents.Append("check2=ﾚ").Append(vbCrLf)
                        contents.Append("check4=ﾚ").Append(vbCrLf)
                    Case (dataRow.Item("tatiaisya_cd")) = 6
                        contents.Append("check3=ﾚ").Append(vbCrLf)
                        contents.Append("check4=ﾚ").Append(vbCrLf)
                    Case (dataRow.Item("tatiaisya_cd")) = 7
                        contents.Append("check2=ﾚ").Append(vbCrLf)
                        contents.Append("check3=ﾚ").Append(vbCrLf)
                        contents.Append("check4=ﾚ").Append(vbCrLf)
                End Select
            Else

            End If
        Else
            contents.Append("check5=ﾚ").Append(vbCrLf)
        End If

        '調査会社名
        If (dataRow.Item("tys_kaisya_mei")).Length <> 0 Then
            Select Case True
                Case encoded4.Length < 23
                    contents.Append("tyousagaisyamei_23=")
                    contents.Append(dataRow.Item("tys_kaisya_mei")).Append(vbCrLf)
                Case encoded4.Length < 25
                    contents.Append("tyousagaisyamei_25=")
                    contents.Append(dataRow.Item("tys_kaisya_mei")).Append(vbCrLf)
                Case encoded4.Length >= 25
                    contents.Append("tyousagaisyameilong=")
                    contents.Append(dataRow.Item("tys_kaisya_mei")).Append(vbCrLf)
            End Select
        End If

        '調査会社TEL,FAX
        If (dataRow.Item("tel_no")) Is DBNull.Value = False Then
            contents.Append("tyousagaisyatel=TEL:")
            contents.Append(dataRow.Item("tel_no")).Append(vbCrLf)
        Else
            contents.Append("tyousagaisyatel=").Append(vbCrLf)
        End If

        If (dataRow.Item("fax_no")) Is DBNull.Value = False Then
            contents.Append("tyousagaisyafax=FAX:")
            contents.Append(dataRow.Item("fax_no")).Append(vbCrLf)
        Else
            contents.Append("tyousagaisyafax=").Append(vbCrLf)
        End If

        '自社郵便番号
        contents.Append("yuubinbangou=〒")
        contents.Append(dataRow.Item("yuubin_no")).Append(vbCrLf)

        '自社住所
        If (dataRow.Item("jyuusyo1")) Is DBNull.Value = False Then
            contents.Append("jisyajusyo1=")
            contents.Append(dataRow.Item("jyuusyo1")).Append(vbCrLf)
        Else
            contents.Append("jisyajusyo1=").Append(vbCrLf)
        End If

        If (dataRow.Item("jyuusyo2")) Is DBNull.Value = False Then
            contents.Append("jisyajusyo2=")
            contents.Append(dataRow.Item("jyuusyo2")).Append(vbCrLf)
        Else
            contents.Append("jisyajusyo2=").Append(vbCrLf)
        End If

        '自社電話番号
        If (dataRow.Item("kaiseki_tel")) Is DBNull.Value = False Then
            contents.Append("jisyatel=")
            contents.Append(dataRow.Item("kaiseki_tel")).Append(vbCrLf)
        Else
            contents.Append("jisyatel=").Append(vbCrLf)
        End If

        '自社担当者名
        If (dataRow.Item("tantousya_mei")) Is DBNull.Value = False Then
            contents.Append("jisyatantousya=担当　")
            contents.Append(dataRow.Item("tantousya_mei")).Append(vbCrLf)
        Else
            contents.Append("jisyatantousya=").Append(vbCrLf)
        End If

        '######################
        '#####外字チェック#####
        '施主名外字チェック
        If dataRow.Item("sesyu_mei") Is DBNull.Value = False Then
            If stringLogic.GaijiExistCheck(dataRow.Item("sesyu_mei")) = False Then
                gaijiErrorTarget += "[施主名] "
            End If
        End If
        '加盟店名外字チェック
        If (dataRow.Item("kameiten_mei1")) Is DBNull.Value = False Then
            If stringLogic.GaijiExistCheck(dataRow.Item("kameiten_mei1")) = False Then
                gaijiErrorTarget += "[加盟店名] "
            End If
        End If
        '住所外字チェック
        If dataRow.Item("bukken_jyuusyo1") Is DBNull.Value = False Then
            If stringLogic.GaijiExistCheck(dataRow.Item("bukken_jyuusyo1")) = False Then
                gaijiErrorTarget += "[住所１] "
            End If
        End If
        If dataRow.Item("bukken_jyuusyo2") Is DBNull.Value = False Then
            If stringLogic.GaijiExistCheck(dataRow.Item("bukken_jyuusyo2")) = False Then
                gaijiErrorTarget += "[住所２] "
            End If
        End If
        If dataRow.Item("bukken_jyuusyo3") Is DBNull.Value = False Then
            If stringLogic.GaijiExistCheck(dataRow.Item("bukken_jyuusyo3")) = False Then
                gaijiErrorTarget += "[住所３] "
            End If
        End If
        '店担当者名外字チェック
        If (dataRow.Item("irai_tantousya_mei")) Is DBNull.Value = False Then
            If stringLogic.GaijiExistCheck(dataRow.Item("irai_tantousya_mei")) = False Then
                gaijiErrorTarget += "[店担当者名] "
            End If
        End If
        '自社担当者名外字チェック
        If (dataRow.Item("tantousya_mei")) Is DBNull.Value = False Then
            If stringLogic.GaijiExistCheck(dataRow.Item("tantousya_mei")) = False Then
                gaijiErrorTarget += "[自社担当者名] "
            End If
        End If
        '調査希望時間外字チェック
        If (dataRow.Item("tys_kibou_jikan")) Is DBNull.Value = False Then
            If stringLogic.GaijiExistCheck(dataRow.Item("tys_kibou_jikan")) = False Then
                gaijiErrorTarget += "[調査希望時間] "
            End If
        End If
        '調査会社名外字チェック
        If (dataRow.Item("tys_kaisya_mei")) Is DBNull.Value = False Then
            If stringLogic.GaijiExistCheck(dataRow.Item("tys_kaisya_mei")) = False Then
                gaijiErrorTarget += "[調査会社名] "
            End If
        End If

        '外字エラーメッセージ出力
        If gaijiErrorTarget <> String.Empty Then
            messageLogic.AlertMessage(sender, String.Format(Messages.MSG106W, gaijiErrorTarget), 1, "gaijicheckresult")
        End If
        '#####外字チェック#####
        '######################

        Return contents.ToString()
    End Function
End Class

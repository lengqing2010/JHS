Imports Itis.Earth.BizLogic
Imports Itis.Earth.DataAccess
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports System.Reflection
Imports System.Data

Partial Public Class SeikyusyoExcelOutput
    Inherits System.Web.UI.Page

#Region "プライベート変数"

    Private seikyusyoFcwLogic As New SeikyusyoFcwOutputLogic
    'ログインユーザーを取得する。
    Private ninsyou As New Ninsyou()

    'FCWのクラスを作成
    Private fcw As Itis.Earth.BizLogic.FcwUtility63
    Private kinouId As String = "SB_SEIKYUSYO"

    Private tantouName As String
    Private loginID As String
    Private headFlg As Boolean = False

    'Private Const APOST As Char = ","c
    Private Const APOST As Char = "'"c

    Enum PDFStatus As Integer

        OK = 0                              '正常
        IOException = 1                     'エラー(他のユーザがファイルを開いている)
        UnauthorizedAccessException = 2     'エラー(ファイルを作成するパスが不正)
        NoData = 3                          '対象のデータが取得できません。

    End Enum

#End Region

    Public Const XltFolder As String = "C:\JHS\earth\"
    Public XltFile As String = ""
    Public Const CsvFolder As String = "C:\JHS\earth\download\"
    Public CsvDataFile As String = ""
    Public csvData As String
    Public strErr As String = ""
    Public strKakutyoushi As String = ""
    Private sb_T As New StringBuilder

    'Public tt As String = "msgbox 'aaaa'"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'tt = "msgbox 'bbbb'"
        '基本認証
        If ninsyou.GetUserID() = "" Then
            Context.Items("strFailureMsg") = Messages.Instance.MSG2024E
            Server.Transfer("CommonErr.aspx")
        Else
            loginID = ninsyou.GetUserID()
        End If

        Me.tantouName = seikyusyoFcwLogic.GetLoginUserName(loginID)

        Dim strNoAddId As String = Request.QueryString("strNoAddId").ToString

        Dim strNo As String = strNoAddId.Split(",")(0)
        Dim ID1 As String = strNoAddId.Split(",")(1)
        Dim ID2 As String = strNoAddId.Split(",")(2)

        Me.MakeJavaScript()

        CsvDataFile = "ExcelOutput.csv"
        XltFile = "ExcelOutput.xlt"


        GetScriptValue(strNoAddId)

        If (Me.CreateExcelData(strNo) = True) Then
            Dim arr() As String = sb_T.ToString.Split(vbCrLf)
            For i As Integer = 0 To arr.Length - 1
                If arr(i) <> Chr(10) Then
                    csvData = csvData & arr(i).Replace(Chr(10), "") & "@@@"
                End If
            Next
            csvData = csvData.Replace("'", "&")
        Else
            'ClientScript.RegisterStartupScript(Me.GetType(), "CLOSE", "funBtnEnable();", True)
        End If

        If Not IsPostBack Then

        Else

        End If

    End Sub

    ''' <summary>
    ''' Excel出力
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2010/09/21 車龍(大連情報システム部)　新規作成</history>
    Private Function CreateExcelData(ByVal strNo As String) As Boolean

        'インスタンスの生成
        Dim seikyusyoDataTable As Data.DataTable        'データdt
        Dim seikyusyoNoDataTable As Data.DataTable        'データdt

        Dim errMsg As String = String.Empty

        Dim syainCd As String                           '社員コード
        Dim seikyusyoNo As String       '請求書NO

        syainCd = loginID

        seikyusyoNo = strNo

        fcw = New FcwUtility63(Page, syainCd, kinouId, ".fcx")

        '請求書データ取得
        seikyusyoNo = "'" & seikyusyoNo.Replace("$$$", "','") & "'"

        seikyusyoNoDataTable = seikyusyoFcwLogic.GetSeikyusyoNoData(seikyusyoNo)

        If seikyusyoNoDataTable.Rows.Count > 0 Then

            Dim i As Integer
            For i = 0 To seikyusyoNoDataTable.Rows.Count - 1

                seikyusyoNo = seikyusyoNoDataTable.Rows(i).Item("seikyuusyo_no").ToString

                seikyusyoDataTable = seikyusyoFcwLogic.GetSeikyusyoData(seikyusyoNo)

                Select Case seikyusyoDataTable.Rows(0).Item("yousi_flg").ToString

                    Case "0"

                        '全件請求書用紙を使用
                        sb_T.Append(Me.seikyusyo_ZK(seikyusyoDataTable, errMsg))

                    Case "1"

                        '物件別請求書用紙を使用
                        sb_T.Append(Me.seikyusyo_BK(seikyusyoDataTable, errMsg))

                    Case "2"

                        '全件請求書(店小計)用紙を使用
                        sb_T.Append(Me.seikyusyo_XK(seikyusyoDataTable, errMsg))

                    Case "3"

                        '全件・残高表示無し請求書用紙を使用
                        sb_T.Append(Me.seikyusyo_ZKZN(seikyusyoDataTable, errMsg))

                    Case Else

                        errMsg = Messages.Instance.MSG2030E

                End Select

            Next

        Else
            errMsg = Messages.Instance.MSG034E
        End If

        If Not errMsg.Equals(String.Empty) Then
            Return False
        Else
            Return True
        End If

    End Function

    Private Sub AddFeild_ZK(ByRef editDt As Data.DataTable)

        '[FIXED FEILD]
        editDt.Columns.Add("seikyuu_saki_cd_with_brc", GetType(String))
        editDt.Columns.Add("pages_total", GetType(String))
        editDt.Columns.Add("seikyuusyo_hak_date", GetType(String))
        editDt.Columns.Add("yuubin_no", GetType(String))
        editDt.Columns.Add("jyuusyo1", GetType(String))
        editDt.Columns.Add("jyuusyo2", GetType(String))
        editDt.Columns.Add("seikyuu_saki_mei", GetType(String))
        editDt.Columns.Add("seikyuu_saki_mei2", GetType(String))
        editDt.Columns.Add("tantousya_mei", GetType(String))
        editDt.Columns.Add("konkai_goseikyuu_gaku", GetType(String))
        editDt.Columns.Add("konkai_kaisyuu_yotei_date", GetType(String))
        editDt.Columns.Add("konkai_kaisyuu_yotei_date2", GetType(String))
        editDt.Columns.Add("konkai_kaisyuu_yotei_date3", GetType(String))
        editDt.Columns.Add("DisplayName", GetType(String))
        editDt.Columns.Add("furikomisaki1", GetType(String))
        editDt.Columns.Add("furikomisaki2", GetType(String))
        editDt.Columns.Add("furikomisaki3", GetType(String))
        editDt.Columns.Add("furikomisaki4", GetType(String))
        editDt.Columns.Add("furikomisaki5", GetType(String))
        editDt.Columns.Add("zenkai_goseikyuu_gaku", GetType(String))
        editDt.Columns.Add("gonyuukin_gaku", GetType(String))
        editDt.Columns.Add("sousai_gaku", GetType(String))
        editDt.Columns.Add("tyousei_gaku", GetType(String))
        editDt.Columns.Add("konkai_goseikyuu_gaku2", GetType(String))
        editDt.Columns.Add("konkai_kurikosi_gaku", GetType(String))
        '===============↓2014/02/17 407662_消費税増税対応_Earth 車龍 修正 開始↓===========================
        'editDt.Columns.Add("sum_uriage", GetType(String))
        'editDt.Columns.Add("sum_sotozei", GetType(String))
        'editDt.Columns.Add("sum_uriage_sotozei", GetType(String))
        'editDt.Columns.Add("total_uriage", GetType(String))
        'editDt.Columns.Add("total_sotozei", GetType(String))
        'editDt.Columns.Add("total_uriage_sotozei", GetType(String))

        '小計(税率=0%)
        editDt.Columns.Add("sum_uriage0", GetType(String))
        editDt.Columns.Add("sum_sotozei0", GetType(String))
        editDt.Columns.Add("sum_uriage_sotozei0", GetType(String))
        '小計(税率=5%)
        editDt.Columns.Add("sum_uriage5", GetType(String))
        editDt.Columns.Add("sum_sotozei5", GetType(String))
        editDt.Columns.Add("sum_uriage_sotozei5", GetType(String))
        '小計(税率=8%)
        editDt.Columns.Add("sum_uriage8", GetType(String))
        editDt.Columns.Add("sum_sotozei8", GetType(String))
        editDt.Columns.Add("sum_uriage_sotozei8", GetType(String))

        '合計(税率=0%)
        editDt.Columns.Add("total_uriage0", GetType(String))
        editDt.Columns.Add("total_sotozei0", GetType(String))
        editDt.Columns.Add("total_uriage_sotozei0", GetType(String))
        '合計(税率=5%)
        editDt.Columns.Add("total_uriage5", GetType(String))
        editDt.Columns.Add("total_sotozei5", GetType(String))
        editDt.Columns.Add("total_uriage_sotozei5", GetType(String))
        '合計(税率=8%)
        editDt.Columns.Add("total_uriage8", GetType(String))
        editDt.Columns.Add("total_sotozei8", GetType(String))
        editDt.Columns.Add("total_uriage_sotozei8", GetType(String))

        '総合計
        editDt.Columns.Add("total_uriage_all", GetType(String))
        editDt.Columns.Add("total_sotozei_all", GetType(String))
        editDt.Columns.Add("total_uriage_sotozei_all", GetType(String))
        '===============↑2014/02/17 407662_消費税増税対応_Earth 車龍 修正 開始↑===========================
        editDt.Columns.Add("total_cover_flg", GetType(String))

        '[TABLE FEILD]
        editDt.Columns.Add("denpyou_uri_date", GetType(String))
        editDt.Columns.Add("kbnwihtbangou", GetType(String))
        editDt.Columns.Add("hinmei", GetType(String))
        editDt.Columns.Add("bukenmei", GetType(String))
        editDt.Columns.Add("suu", GetType(String))
        editDt.Columns.Add("tanka", GetType(String))
        editDt.Columns.Add("uri_gaku", GetType(String))
        editDt.Columns.Add("sotozei_gaku", GetType(String))
        editDt.Columns.Add("uriage_sotosei", GetType(String))
        '===============↓2014/02/17 407662_消費税増税対応_Earth 車龍 修正 開始↓===========================
        editDt.Columns.Add("zeiritu", GetType(String)) '税率
        '===============↑2014/02/17 407662_消費税増税対応_Earth 車龍 修正 開始↑===========================

        '2018/12/05 李松涛 JHS0003_Earth請求書帳票の項目追加 Fontsize 対応↓
        editDt.Columns.Add("font_size", GetType(String))

    End Sub

    Private Sub AddFeild_XK(ByRef editDt As Data.DataTable)

        '[FIXED FEILD]
        editDt.Columns.Add("seikyuu_saki_cd_with_brc", GetType(String))
        editDt.Columns.Add("pages_total", GetType(String))
        editDt.Columns.Add("seikyuusyo_hak_date", GetType(String))
        editDt.Columns.Add("yuubin_no", GetType(String))
        editDt.Columns.Add("jyuusyo1", GetType(String))
        editDt.Columns.Add("jyuusyo2", GetType(String))
        editDt.Columns.Add("seikyuu_saki_mei", GetType(String))
        editDt.Columns.Add("seikyuu_saki_mei2", GetType(String))
        editDt.Columns.Add("tantousya_mei", GetType(String))
        editDt.Columns.Add("konkai_goseikyuu_gaku", GetType(String))
        editDt.Columns.Add("konkai_kaisyuu_yotei_date", GetType(String))
        editDt.Columns.Add("konkai_kaisyuu_yotei_date2", GetType(String))
        editDt.Columns.Add("konkai_kaisyuu_yotei_date3", GetType(String))
        editDt.Columns.Add("DisplayName", GetType(String))
        editDt.Columns.Add("furikomisaki1", GetType(String))
        editDt.Columns.Add("furikomisaki2", GetType(String))
        editDt.Columns.Add("furikomisaki3", GetType(String))
        editDt.Columns.Add("furikomisaki4", GetType(String))
        editDt.Columns.Add("furikomisaki5", GetType(String))
        editDt.Columns.Add("zenkai_goseikyuu_gaku", GetType(String))
        editDt.Columns.Add("gonyuukin_gaku", GetType(String))
        editDt.Columns.Add("sousai_gaku", GetType(String))
        editDt.Columns.Add("tyousei_gaku", GetType(String))
        editDt.Columns.Add("konkai_goseikyuu_gaku2", GetType(String))
        editDt.Columns.Add("konkai_kurikosi_gaku", GetType(String))
        editDt.Columns.Add("sum_uriage", GetType(String))
        editDt.Columns.Add("sum_sotozei", GetType(String))
        editDt.Columns.Add("sum_uriage_sotozei", GetType(String))
        '===============↓2014/02/17 407662_消費税増税対応_Earth 車龍 修正 開始↓===========================
        'editDt.Columns.Add("total_uriage", GetType(String))
        'editDt.Columns.Add("total_sotozei", GetType(String))
        'editDt.Columns.Add("total_uriage_sotozei", GetType(String))
        editDt.Columns.Add("total_uriage0", GetType(String))
        editDt.Columns.Add("total_sotozei0", GetType(String))
        editDt.Columns.Add("total_uriage_sotozei0", GetType(String))
        editDt.Columns.Add("total_uriage5", GetType(String))
        editDt.Columns.Add("total_sotozei5", GetType(String))
        editDt.Columns.Add("total_uriage_sotozei5", GetType(String))
        editDt.Columns.Add("total_uriage8", GetType(String))
        editDt.Columns.Add("total_sotozei8", GetType(String))
        editDt.Columns.Add("total_uriage_sotozei8", GetType(String))
        editDt.Columns.Add("total_uriage_all", GetType(String))
        editDt.Columns.Add("total_sotozei_all", GetType(String))
        editDt.Columns.Add("total_uriage_sotozei_all", GetType(String))
        '===============↑2014/02/17 407662_消費税増税対応_Earth 車龍 修正 終了↑===========================
        editDt.Columns.Add("TM1", GetType(String))
        editDt.Columns.Add("TM1_1", GetType(String))
        editDt.Columns.Add("TM1_2", GetType(String))
        editDt.Columns.Add("TM1_3", GetType(String))
        editDt.Columns.Add("TM1_4", GetType(String))
        editDt.Columns.Add("TM1_5", GetType(String))
        editDt.Columns.Add("TM1_6", GetType(String))
        editDt.Columns.Add("TM1_7", GetType(String))
        editDt.Columns.Add("TM1_8", GetType(String))
        editDt.Columns.Add("TM1_9", GetType(String))
        editDt.Columns.Add("TM1_10", GetType(String))
        editDt.Columns.Add("TM1_11", GetType(String))
        editDt.Columns.Add("TM1_12", GetType(String))
        editDt.Columns.Add("TM1_13", GetType(String))
        editDt.Columns.Add("TM1_14", GetType(String))
        editDt.Columns.Add("TM1_15", GetType(String))
        editDt.Columns.Add("TM1_16", GetType(String))
        editDt.Columns.Add("TM1_17", GetType(String))
        editDt.Columns.Add("TM1_18", GetType(String))
        editDt.Columns.Add("TM1_19", GetType(String))
        editDt.Columns.Add("TM1_20", GetType(String))
        editDt.Columns.Add("TM1_21", GetType(String))
        editDt.Columns.Add("TM1_22", GetType(String))
        editDt.Columns.Add("TM1_23", GetType(String))
        editDt.Columns.Add("TM1_24", GetType(String))
        editDt.Columns.Add("TM1_25", GetType(String))
        editDt.Columns.Add("TM2_1", GetType(String))
        editDt.Columns.Add("TM2_2", GetType(String))
        editDt.Columns.Add("TM2_3", GetType(String))
        editDt.Columns.Add("TM2_4", GetType(String))
        editDt.Columns.Add("TM2_5", GetType(String))
        editDt.Columns.Add("TM2_6", GetType(String))
        editDt.Columns.Add("TM2_7", GetType(String))
        editDt.Columns.Add("TM2_8", GetType(String))
        editDt.Columns.Add("TM2_9", GetType(String))
        editDt.Columns.Add("TM2_10", GetType(String))
        editDt.Columns.Add("TM2_11", GetType(String))
        editDt.Columns.Add("TM2_12", GetType(String))
        editDt.Columns.Add("TM2_13", GetType(String))
        editDt.Columns.Add("TM2_14", GetType(String))
        editDt.Columns.Add("TM2_15", GetType(String))
        editDt.Columns.Add("TM2_16", GetType(String))
        editDt.Columns.Add("TM2_17", GetType(String))
        editDt.Columns.Add("TM2_18", GetType(String))
        editDt.Columns.Add("TM2_19", GetType(String))
        editDt.Columns.Add("TM2_20", GetType(String))
        editDt.Columns.Add("TM2_21", GetType(String))
        editDt.Columns.Add("TM2_22", GetType(String))
        editDt.Columns.Add("TM2_23", GetType(String))
        editDt.Columns.Add("TM2_24", GetType(String))
        editDt.Columns.Add("TM2_25", GetType(String))
        editDt.Columns.Add("TM2_0", GetType(String))
        editDt.Columns.Add("total_cover_flg", GetType(String))

        '[TABLE FEILD]
        editDt.Columns.Add("denpyou_uri_date", GetType(String))
        editDt.Columns.Add("kbnwihtbangou", GetType(String))
        editDt.Columns.Add("hinmei", GetType(String))
        editDt.Columns.Add("bukenmei", GetType(String))
        editDt.Columns.Add("suu", GetType(String))
        editDt.Columns.Add("tanka", GetType(String))
        editDt.Columns.Add("uri_gaku", GetType(String))
        editDt.Columns.Add("sotozei_gaku", GetType(String))
        editDt.Columns.Add("uriage_sotosei", GetType(String))
        'editDt.Columns.Add("kbnwihtbangou_1", GetType(String))
        '===============↓2014/02/17 407662_消費税増税対応_Earth 車龍 修正 開始↓===========================
        editDt.Columns.Add("zeiritu", GetType(String)) '税率
        '===============↑2014/02/17 407662_消費税増税対応_Earth 車龍 修正 開始↑===========================
        '2018/12/05 李松涛 JHS0003_Earth請求書帳票の項目追加 Fontsize 対応↓
        editDt.Columns.Add("font_size", GetType(String))
    End Sub

    '全件請求書用紙を使用 
    Private Function seikyusyo_ZK(ByVal seikyusyoDataTable As Data.DataTable, ByRef errMsg As String) _
                                                                                            As StringBuilder

        '---検索したデータを編集する。---st
        Dim editDt As New DataTable
        Dim editDR As Data.DataRow = editDt.NewRow
        Dim CNT1 As Integer
        Dim sb As New StringBuilder

        '===============↓2014/02/17 407662_消費税増税対応_Earth 車龍 修正 開始↓===========================
        'Dim sum_uriage As Decimal = 0
        'Dim sum_sotozei As Decimal = 0
        'Dim sum_uriage_sotozei As Decimal = 0
        'Dim total_uriage As Decimal = 0
        'Dim total_sotozei As Decimal = 0
        'Dim total_uriage_sotozei As Decimal = 0

        '小計(税率=0%)
        Dim sum_uriage0 As Decimal = 0
        Dim sum_sotozei0 As Decimal = 0
        Dim sum_uriage_sotozei0 As Decimal = 0
        '小計(税率=5%)
        Dim sum_uriage5 As Decimal = 0
        Dim sum_sotozei5 As Decimal = 0
        Dim sum_uriage_sotozei5 As Decimal = 0
        '小計(税率=8%)
        Dim sum_uriage8 As Decimal = 0
        Dim sum_sotozei8 As Decimal = 0
        Dim sum_uriage_sotozei8 As Decimal = 0

        '合計(税率=0%)
        Dim total_uriage0 As Decimal = 0
        Dim total_sotozei0 As Decimal = 0
        Dim total_uriage_sotozei0 As Decimal = 0
        '合計(税率=5%)
        Dim total_uriage5 As Decimal = 0
        Dim total_sotozei5 As Decimal = 0
        Dim total_uriage_sotozei5 As Decimal = 0
        '合計(税率=8%)
        Dim total_uriage8 As Decimal = 0
        Dim total_sotozei8 As Decimal = 0
        Dim total_uriage_sotozei8 As Decimal = 0

        '総合計
        Dim total_uriage_all As Decimal = 0
        Dim total_sotozei_all As Decimal = 0
        Dim total_uriage_sotozei_all As Decimal = 0
        '===============↑2014/02/17 407662_消費税増税対応_Earth 車龍 修正 終了↑===========================

        Dim pages As Integer

        'add feild
        Call Me.AddFeild_ZK(editDt)

        '===============↓2014/02/17 407662_消費税増税対応_Earth 車龍 追加 開始↓===========================
        '各頁の最大行数
        Dim intPage1MaxRow As Integer
        Dim intPage2MaxRow As Integer

        intPage1MaxRow = 18 '初め頁
        intPage2MaxRow = 20 '後頁
        '===============↑2014/02/17 407662_消費税増税対応_Earth 車龍 追加 終了↑===========================

        Dim CNT_T As Integer = seikyusyoDataTable.Rows.Count

        '<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>
        '<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>
        '<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>

        '第一ページDAT作成

        If CNT_T > intPage1MaxRow Then
            '>1Page　の場合
            CNT1 = intPage1MaxRow

        Else
            '1Page　の場合
            CNT1 = CNT_T

        End If

        pages = Math.Ceiling((CNT_T - intPage1MaxRow) / intPage2MaxRow) + 1

        'データ作成
        Dim i As Integer
        For i = 0 To CNT1 - 1

            editDR = editDt.NewRow

            Call Me.editDR_data_settings(seikyusyoDataTable, _
                                        i, _
                                        editDR, _
                                        sum_uriage0, _
                                        sum_sotozei0, _
                                        sum_uriage_sotozei0, _
                                        sum_uriage5, _
                                        sum_sotozei5, _
                                        sum_uriage_sotozei5, _
                                        sum_uriage8, _
                                        sum_sotozei8, _
                                        sum_uriage_sotozei8, _
                                        pages, _
                                        errMsg, _
                                        1)

            editDt.Rows.Add(editDR)

        Next

        '===============↓2014/02/17 407662_消費税増税対応_Earth 車龍 修正 開始↓===========================
        'total_uriage += sum_uriage
        'total_sotozei += sum_sotozei
        'total_uriage_sotozei += sum_uriage_sotozei

        '合計(税率=0%)
        total_uriage0 += sum_uriage0
        total_sotozei0 += sum_sotozei0
        total_uriage_sotozei0 += sum_uriage_sotozei0
        '合計(税率=5%)
        total_uriage5 += sum_uriage5
        total_sotozei5 += sum_sotozei5
        total_uriage_sotozei5 += sum_uriage_sotozei5
        '合計(税率=8%)
        total_uriage8 += sum_uriage8
        total_sotozei8 += sum_sotozei8
        total_uriage_sotozei8 += sum_uriage_sotozei8

        '総合計
        total_uriage_all = total_uriage0 + total_uriage5 + total_uriage8
        total_sotozei_all = total_sotozei0 + total_sotozei5 + total_sotozei8
        total_uriage_sotozei_all = total_uriage_sotozei0 + total_uriage_sotozei5 + total_uriage_sotozei8
        '===============↑2014/02/17 407662_消費税増税対応_Earth 車龍 修正 終了↑===========================

        For i = 0 To CNT1 - 1

            '===============↓2014/02/17 407662_消費税増税対応_Earth 車龍 修正 開始↓===========================
            'editDt.Rows(i).Item("sum_uriage") = sum_uriage
            'editDt.Rows(i).Item("sum_sotozei") = sum_sotozei
            'editDt.Rows(i).Item("sum_uriage_sotozei") = sum_uriage_sotozei

            'editDt.Rows(i).Item("total_uriage") = total_uriage
            'editDt.Rows(i).Item("total_sotozei") = total_sotozei
            'editDt.Rows(i).Item("total_uriage_sotozei") = total_uriage_sotozei

            '小計(税率=0%)
            editDt.Rows(i).Item("sum_uriage0") = sum_uriage0
            editDt.Rows(i).Item("sum_sotozei0") = sum_sotozei0
            editDt.Rows(i).Item("sum_uriage_sotozei0") = sum_uriage_sotozei0
            '小計(税率=5%)
            editDt.Rows(i).Item("sum_uriage5") = sum_uriage5
            editDt.Rows(i).Item("sum_sotozei5") = sum_sotozei5
            editDt.Rows(i).Item("sum_uriage_sotozei5") = sum_uriage_sotozei5
            '小計(税率=8%)
            editDt.Rows(i).Item("sum_uriage8") = sum_uriage8
            editDt.Rows(i).Item("sum_sotozei8") = sum_sotozei8
            editDt.Rows(i).Item("sum_uriage_sotozei8") = sum_uriage_sotozei8

            '合計(税率=0%)
            editDt.Rows(i).Item("total_uriage0") = total_uriage0
            editDt.Rows(i).Item("total_sotozei0") = total_sotozei0
            editDt.Rows(i).Item("total_uriage_sotozei0") = total_uriage_sotozei0
            '合計(税率=5%)
            editDt.Rows(i).Item("total_uriage5") = total_uriage5
            editDt.Rows(i).Item("total_sotozei5") = total_sotozei5
            editDt.Rows(i).Item("total_uriage_sotozei5") = total_uriage_sotozei5
            '合計(税率=8%)
            editDt.Rows(i).Item("total_uriage8") = total_uriage8
            editDt.Rows(i).Item("total_sotozei8") = total_sotozei8
            editDt.Rows(i).Item("total_uriage_sotozei8") = total_uriage_sotozei8

            '総合計
            editDt.Rows(i).Item("total_uriage_all") = total_uriage_all
            editDt.Rows(i).Item("total_sotozei_all") = total_sotozei_all
            editDt.Rows(i).Item("total_uriage_sotozei_all") = total_uriage_sotozei_all
            '===============↑2014/02/17 407662_消費税増税対応_Earth 車龍 修正 終了↑===========================

            If CNT_T <= intPage1MaxRow Then
                editDt.Rows(i).Item("total_cover_flg") = ""
            Else
                editDt.Rows(i).Item("total_cover_flg") = "　"
            End If

        Next

        If Me.headFlg Then
        Else
            '[Head] 部作成
            sb.Append(fcw.CreateDatHeader(APOST.ToString))
            Me.headFlg = True
        End If

        sb.Append(vbCrLf)
        '[Form] 部作成
        sb.Append(fcw.CreateFormSection("PAGE=SB_SEIKYUSYO_ZK1"))

        '[FixedDataSection] 部作成
        sb.Append(fcw.CreateFixedDataSection(GetFixedDataSection_ZK(editDt)))

        '[TableDataSection] 部作成
        sb.Append(fcw.CreateTableDataSection(GetTableDataSection_ZK(editDt)))

        '<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>
        '<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>
        '<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>

        '第二ページ（＞＝２）DAT作成
        If CNT_T > intPage1MaxRow Then

            Dim sb2_Pages As Integer = Math.Ceiling((CNT_T - intPage1MaxRow) / intPage2MaxRow)
            Dim RowsInThisPage As Integer = 0

            For i = 0 To sb2_Pages - 1

                editDt.Rows.Clear()

                '===============↓2014/02/17 407662_消費税増税対応_Earth 車龍 修正 開始↓===========================
                'sum_uriage = 0
                'sum_sotozei = 0
                'sum_uriage_sotozei = 0

                '小計(税率=0%)
                sum_uriage0 = 0
                sum_sotozei0 = 0
                sum_uriage_sotozei0 = 0
                '小計(税率=5%)
                sum_uriage5 = 0
                sum_sotozei5 = 0
                sum_uriage_sotozei5 = 0
                '小計(税率=8%)
                sum_uriage8 = 0
                sum_sotozei8 = 0
                sum_uriage_sotozei8 = 0
                '===============↑2014/02/17 407662_消費税増税対応_Earth 車龍 修正 終了↑===========================

                'RowsInThisPage
                If intPage1MaxRow + (intPage2MaxRow * (i + 1)) <= CNT_T Then
                    RowsInThisPage = intPage2MaxRow
                Else
                    RowsInThisPage = CNT_T - (intPage1MaxRow + intPage2MaxRow * i)
                End If

                Dim j As Integer
                For j = 0 To RowsInThisPage - 1

                    editDR = editDt.NewRow

                    Call Me.editDR_data_settings(seikyusyoDataTable, _
                            intPage1MaxRow + i * intPage2MaxRow + j, _
                            editDR, _
                            sum_uriage0, _
                            sum_sotozei0, _
                            sum_uriage_sotozei0, _
                            sum_uriage5, _
                            sum_sotozei5, _
                            sum_uriage_sotozei5, _
                            sum_uriage8, _
                            sum_sotozei8, _
                            sum_uriage_sotozei8, _
                            pages, _
                            errMsg, _
                            i + 2)

                    editDt.Rows.Add(editDR)

                Next

                '===============↓2014/02/17 407662_消費税増税対応_Earth 車龍 修正 開始↓===========================
                'total_uriage += sum_uriage
                'total_sotozei += sum_sotozei
                'total_uriage_sotozei += sum_uriage_sotozei

                '合計(税率=0%)
                total_uriage0 += sum_uriage0
                total_sotozei0 += sum_sotozei0
                total_uriage_sotozei0 += sum_uriage_sotozei0
                '合計(税率=5%)
                total_uriage5 += sum_uriage5
                total_sotozei5 += sum_sotozei5
                total_uriage_sotozei5 += sum_uriage_sotozei5
                '合計(税率=8%)
                total_uriage8 += sum_uriage8
                total_sotozei8 += sum_sotozei8
                total_uriage_sotozei8 += sum_uriage_sotozei8

                '総合計
                total_uriage_all = total_uriage0 + total_uriage5 + total_uriage8
                total_sotozei_all = total_sotozei0 + total_sotozei5 + total_sotozei8
                total_uriage_sotozei_all = total_uriage_sotozei0 + total_uriage_sotozei5 + total_uriage_sotozei8
                '===============↓2014/02/17 407662_消費税増税対応_Earth 車龍 修正 開始↓===========================

                For j = 0 To RowsInThisPage - 1

                    '===============↓2014/02/17 407662_消費税増税対応_Earth 車龍 修正 開始↓===========================
                    'editDt.Rows(j)("total_uriage") = total_uriage
                    'editDt.Rows(j)("total_sotozei") = total_sotozei
                    'editDt.Rows(j)("total_uriage_sotozei") = total_uriage_sotozei

                    'editDt.Rows(j)("sum_uriage") = sum_uriage
                    'editDt.Rows(j)("sum_sotozei") = sum_sotozei
                    'editDt.Rows(j)("sum_uriage_sotozei") = sum_uriage_sotozei

                    '小計(税率=0%)
                    editDt.Rows(j)("sum_uriage0") = sum_uriage0
                    editDt.Rows(j)("sum_sotozei0") = sum_sotozei0
                    editDt.Rows(j)("sum_uriage_sotozei0") = sum_uriage_sotozei0
                    '小計(税率=5%)
                    editDt.Rows(j)("sum_uriage5") = sum_uriage5
                    editDt.Rows(j)("sum_sotozei5") = sum_sotozei5
                    editDt.Rows(j)("sum_uriage_sotozei5") = sum_uriage_sotozei5
                    '小計(税率=8%)
                    editDt.Rows(j)("sum_uriage8") = sum_uriage8
                    editDt.Rows(j)("sum_sotozei8") = sum_sotozei8
                    editDt.Rows(j)("sum_uriage_sotozei8") = sum_uriage_sotozei8

                    '合計(税率=0%)
                    editDt.Rows(j)("total_uriage0") = total_uriage0
                    editDt.Rows(j)("total_sotozei0") = total_sotozei0
                    editDt.Rows(j)("total_uriage_sotozei0") = total_uriage_sotozei0
                    '合計(税率=5%)
                    editDt.Rows(j)("total_uriage5") = total_uriage5
                    editDt.Rows(j)("total_sotozei5") = total_sotozei5
                    editDt.Rows(j)("total_uriage_sotozei5") = total_uriage_sotozei5
                    '合計(税率=8%)
                    editDt.Rows(j)("total_uriage8") = total_uriage8
                    editDt.Rows(j)("total_sotozei8") = total_sotozei8
                    editDt.Rows(j)("total_uriage_sotozei8") = total_uriage_sotozei8

                    '総合計
                    editDt.Rows(j)("total_uriage_all") = total_uriage_all
                    editDt.Rows(j)("total_sotozei_all") = total_sotozei_all
                    editDt.Rows(j)("total_uriage_sotozei_all") = total_uriage_sotozei_all
                    '===============↑2014/02/17 407662_消費税増税対応_Earth 車龍 修正 終了↑===========================

                    '最終ページ　合計を表示
                    If (intPage1MaxRow + i * intPage2MaxRow + RowsInThisPage) >= seikyusyoDataTable.Rows.Count Then
                        editDt.Rows(j)("total_cover_flg") = ""
                    Else
                        editDt.Rows(j)("total_cover_flg") = "　"
                    End If

                Next

                '>=2ページ
                sb.Append(vbCrLf)
                '[Form] 部作成
                sb.Append(fcw.CreateFormSection("PAGE=SB_SEIKYUSYO_ZK2"))

                '[FixedDataSection] 部作成
                sb.Append(fcw.CreateFixedDataSection(GetFixedDataSection_ZK(editDt)))

                '[TableDataSection] 部作成
                sb.Append(fcw.CreateTableDataSection(GetTableDataSection_ZK(editDt)))

            Next

        End If

        '<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>
        '<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>
        '<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>

        Return sb

    End Function

    '全件・残高表示無し請求書用紙を使用 
    Private Function seikyusyo_ZKZN(ByVal seikyusyoDataTable As Data.DataTable, ByRef errMsg As String) _
                                                                                            As StringBuilder

        '---検索したデータを編集する。---st
        Dim editDt As New DataTable
        Dim editDR As Data.DataRow = editDt.NewRow
        Dim CNT1 As Integer
        Dim sb As New StringBuilder

        '===============↓2014/02/17 407662_消費税増税対応_Earth 車龍 修正 開始↓===========================
        'Dim sum_uriage As Decimal = 0
        'Dim sum_sotozei As Decimal = 0
        'Dim sum_uriage_sotozei As Decimal = 0
        'Dim total_uriage As Decimal = 0
        'Dim total_sotozei As Decimal = 0
        'Dim total_uriage_sotozei As Decimal = 0

        '小計(税率=0%)
        Dim sum_uriage0 As Decimal = 0
        Dim sum_sotozei0 As Decimal = 0
        Dim sum_uriage_sotozei0 As Decimal = 0
        '小計(税率=5%)
        Dim sum_uriage5 As Decimal = 0
        Dim sum_sotozei5 As Decimal = 0
        Dim sum_uriage_sotozei5 As Decimal = 0
        '小計(税率=8%)
        Dim sum_uriage8 As Decimal = 0
        Dim sum_sotozei8 As Decimal = 0
        Dim sum_uriage_sotozei8 As Decimal = 0

        '合計(税率=0%)
        Dim total_uriage0 As Decimal = 0
        Dim total_sotozei0 As Decimal = 0
        Dim total_uriage_sotozei0 As Decimal = 0
        '合計(税率=5%)
        Dim total_uriage5 As Decimal = 0
        Dim total_sotozei5 As Decimal = 0
        Dim total_uriage_sotozei5 As Decimal = 0
        '合計(税率=8%)
        Dim total_uriage8 As Decimal = 0
        Dim total_sotozei8 As Decimal = 0
        Dim total_uriage_sotozei8 As Decimal = 0

        '総合計
        Dim total_uriage_all As Decimal = 0
        Dim total_sotozei_all As Decimal = 0
        Dim total_uriage_sotozei_all As Decimal = 0
        '===============↑2014/02/17 407662_消費税増税対応_Earth 車龍 修正 終了↑===========================

        Dim pages As Integer

        'add feild
        Call Me.AddFeild_ZK(editDt)

        '===============↓2014/02/17 407662_消費税増税対応_Earth 車龍 追加 開始↓===========================
        '各頁の最大行数
        Dim intPage1MaxRow As Integer
        Dim intPage2MaxRow As Integer

        intPage1MaxRow = 20 '初め頁
        intPage2MaxRow = 20 '後頁
        '===============↑2014/02/17 407662_消費税増税対応_Earth 車龍 追加 終了↑===========================

        Dim CNT_T As Integer = seikyusyoDataTable.Rows.Count

        '<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>
        '<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>
        '<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>

        '第一ページDAT作成

        If CNT_T > intPage1MaxRow Then
            '>1Page　の場合
            CNT1 = intPage1MaxRow

        Else
            '1Page　の場合
            CNT1 = CNT_T

        End If

        pages = Math.Ceiling((CNT_T - intPage1MaxRow) / intPage2MaxRow) + 1

        'データ作成
        Dim i As Integer
        For i = 0 To CNT1 - 1

            editDR = editDt.NewRow

            Call Me.editDR_data_settings(seikyusyoDataTable, _
                                        i, _
                                        editDR, _
                                        sum_uriage0, _
                                        sum_sotozei0, _
                                        sum_uriage_sotozei0, _
                                        sum_uriage5, _
                                        sum_sotozei5, _
                                        sum_uriage_sotozei5, _
                                        sum_uriage8, _
                                        sum_sotozei8, _
                                        sum_uriage_sotozei8, _
                                        pages, _
                                        errMsg, _
                                        1)

            editDt.Rows.Add(editDR)

        Next

        '===============↓2014/02/17 407662_消費税増税対応_Earth 車龍 修正 開始↓===========================
        'total_uriage += sum_uriage
        'total_sotozei += sum_sotozei
        'total_uriage_sotozei += sum_uriage_sotozei

        '合計(税率=0%)
        total_uriage0 += sum_uriage0
        total_sotozei0 += sum_sotozei0
        total_uriage_sotozei0 += sum_uriage_sotozei0
        '合計(税率=5%)
        total_uriage5 += sum_uriage5
        total_sotozei5 += sum_sotozei5
        total_uriage_sotozei5 += sum_uriage_sotozei5
        '合計(税率=8%)
        total_uriage8 += sum_uriage8
        total_sotozei8 += sum_sotozei8
        total_uriage_sotozei8 += sum_uriage_sotozei8

        '総合計
        total_uriage_all = total_uriage0 + total_uriage5 + total_uriage8
        total_sotozei_all = total_sotozei0 + total_sotozei5 + total_sotozei8
        total_uriage_sotozei_all = total_uriage_sotozei0 + total_uriage_sotozei5 + total_uriage_sotozei8
        '===============↑2014/02/17 407662_消費税増税対応_Earth 車龍 修正 終了↑===========================

        For i = 0 To CNT1 - 1

            '===============↓2014/02/17 407662_消費税増税対応_Earth 車龍 修正 開始↓===========================
            'editDt.Rows(i).Item("sum_uriage") = sum_uriage
            'editDt.Rows(i).Item("sum_sotozei") = sum_sotozei
            'editDt.Rows(i).Item("sum_uriage_sotozei") = sum_uriage_sotozei

            'editDt.Rows(i).Item("total_uriage") = total_uriage
            'editDt.Rows(i).Item("total_sotozei") = total_sotozei
            'editDt.Rows(i).Item("total_uriage_sotozei") = total_uriage_sotozei

            '小計(税率=0%)
            editDt.Rows(i).Item("sum_uriage0") = sum_uriage0
            editDt.Rows(i).Item("sum_sotozei0") = sum_sotozei0
            editDt.Rows(i).Item("sum_uriage_sotozei0") = sum_uriage_sotozei0
            '小計(税率=5%)
            editDt.Rows(i).Item("sum_uriage5") = sum_uriage5
            editDt.Rows(i).Item("sum_sotozei5") = sum_sotozei5
            editDt.Rows(i).Item("sum_uriage_sotozei5") = sum_uriage_sotozei5
            '小計(税率=8%)
            editDt.Rows(i).Item("sum_uriage8") = sum_uriage8
            editDt.Rows(i).Item("sum_sotozei8") = sum_sotozei8
            editDt.Rows(i).Item("sum_uriage_sotozei8") = sum_uriage_sotozei8

            '合計(税率=0%)
            editDt.Rows(i).Item("total_uriage0") = total_uriage0
            editDt.Rows(i).Item("total_sotozei0") = total_sotozei0
            editDt.Rows(i).Item("total_uriage_sotozei0") = total_uriage_sotozei0
            '合計(税率=5%)
            editDt.Rows(i).Item("total_uriage5") = total_uriage5
            editDt.Rows(i).Item("total_sotozei5") = total_sotozei5
            editDt.Rows(i).Item("total_uriage_sotozei5") = total_uriage_sotozei5
            '合計(税率=8%)
            editDt.Rows(i).Item("total_uriage8") = total_uriage8
            editDt.Rows(i).Item("total_sotozei8") = total_sotozei8
            editDt.Rows(i).Item("total_uriage_sotozei8") = total_uriage_sotozei8

            '総合計
            editDt.Rows(i).Item("total_uriage_all") = total_uriage_all
            editDt.Rows(i).Item("total_sotozei_all") = total_sotozei_all
            editDt.Rows(i).Item("total_uriage_sotozei_all") = total_uriage_sotozei_all
            '===============↑2014/02/17 407662_消費税増税対応_Earth 車龍 修正 終了↑===========================

            If CNT_T <= intPage1MaxRow Then
                editDt.Rows(i).Item("total_cover_flg") = ""
            Else
                editDt.Rows(i).Item("total_cover_flg") = "　"
            End If

        Next

        If Me.headFlg Then
        Else
            '[Head] 部作成
            sb.Append(fcw.CreateDatHeader(APOST.ToString))
            Me.headFlg = True
        End If

        sb.Append(vbCrLf)
        '[Form] 部作成
        sb.Append(fcw.CreateFormSection("PAGE=SB_SEIKYUSYO_ZKZN1"))

        '[FixedDataSection] 部作成
        sb.Append(fcw.CreateFixedDataSection(GetFixedDataSection_ZK(editDt)))

        '[TableDataSection] 部作成
        sb.Append(fcw.CreateTableDataSection(GetTableDataSection_ZK(editDt)))

        '<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>
        '<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>
        '<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>

        '第二ページ（＞＝２）DAT作成
        If CNT_T > intPage1MaxRow Then

            Dim sb2_Pages As Integer = Math.Ceiling((CNT_T - intPage1MaxRow) / intPage2MaxRow)
            Dim RowsInThisPage As Integer = 0

            For i = 0 To sb2_Pages - 1

                editDt.Rows.Clear()

                '===============↓2014/02/17 407662_消費税増税対応_Earth 車龍 修正 開始↓===========================
                'sum_uriage = 0
                'sum_sotozei = 0
                'sum_uriage_sotozei = 0

                '小計(税率=0%)
                sum_uriage0 = 0
                sum_sotozei0 = 0
                sum_uriage_sotozei0 = 0
                '小計(税率=5%)
                sum_uriage5 = 0
                sum_sotozei5 = 0
                sum_uriage_sotozei5 = 0
                '小計(税率=8%)
                sum_uriage8 = 0
                sum_sotozei8 = 0
                sum_uriage_sotozei8 = 0
                '===============↑2014/02/17 407662_消費税増税対応_Earth 車龍 修正 終了↑===========================

                'RowsInThisPage
                If intPage1MaxRow + (intPage2MaxRow * (i + 1)) <= CNT_T Then
                    RowsInThisPage = intPage2MaxRow
                Else
                    RowsInThisPage = CNT_T - (intPage1MaxRow + intPage2MaxRow * i)
                End If

                Dim j As Integer
                For j = 0 To RowsInThisPage - 1

                    editDR = editDt.NewRow

                    Call Me.editDR_data_settings(seikyusyoDataTable, _
                            intPage1MaxRow + i * intPage2MaxRow + j, _
                            editDR, _
                            sum_uriage0, _
                            sum_sotozei0, _
                            sum_uriage_sotozei0, _
                            sum_uriage5, _
                            sum_sotozei5, _
                            sum_uriage_sotozei5, _
                            sum_uriage8, _
                            sum_sotozei8, _
                            sum_uriage_sotozei8, _
                            pages, _
                            errMsg, _
                            i + 2)

                    editDt.Rows.Add(editDR)

                Next

                '===============↓2014/02/17 407662_消費税増税対応_Earth 車龍 修正 開始↓===========================
                'total_uriage += sum_uriage
                'total_sotozei += sum_sotozei
                'total_uriage_sotozei += sum_uriage_sotozei

                '合計(税率=0%)
                total_uriage0 += sum_uriage0
                total_sotozei0 += sum_sotozei0
                total_uriage_sotozei0 += sum_uriage_sotozei0
                '合計(税率=5%)
                total_uriage5 += sum_uriage5
                total_sotozei5 += sum_sotozei5
                total_uriage_sotozei5 += sum_uriage_sotozei5
                '合計(税率=8%)
                total_uriage8 += sum_uriage8
                total_sotozei8 += sum_sotozei8
                total_uriage_sotozei8 += sum_uriage_sotozei8

                '総合計
                total_uriage_all = total_uriage0 + total_uriage5 + total_uriage8
                total_sotozei_all = total_sotozei0 + total_sotozei5 + total_sotozei8
                total_uriage_sotozei_all = total_uriage_sotozei0 + total_uriage_sotozei5 + total_uriage_sotozei8
                '===============↑2014/02/17 407662_消費税増税対応_Earth 車龍 修正 開始↑===========================

                For j = 0 To RowsInThisPage - 1

                    '===============↓2014/02/17 407662_消費税増税対応_Earth 車龍 修正 開始↓===========================
                    'editDt.Rows(j)("total_uriage") = total_uriage
                    'editDt.Rows(j)("total_sotozei") = total_sotozei
                    'editDt.Rows(j)("total_uriage_sotozei") = total_uriage_sotozei

                    'editDt.Rows(j)("sum_uriage") = sum_uriage
                    'editDt.Rows(j)("sum_sotozei") = sum_sotozei
                    'editDt.Rows(j)("sum_uriage_sotozei") = sum_uriage_sotozei

                    '小計(税率=0%)
                    editDt.Rows(j)("sum_uriage0") = sum_uriage0
                    editDt.Rows(j)("sum_sotozei0") = sum_sotozei0
                    editDt.Rows(j)("sum_uriage_sotozei0") = sum_uriage_sotozei0
                    '小計(税率=5%)
                    editDt.Rows(j)("sum_uriage5") = sum_uriage5
                    editDt.Rows(j)("sum_sotozei5") = sum_sotozei5
                    editDt.Rows(j)("sum_uriage_sotozei5") = sum_uriage_sotozei5
                    '小計(税率=8%)
                    editDt.Rows(j)("sum_uriage8") = sum_uriage8
                    editDt.Rows(j)("sum_sotozei8") = sum_sotozei8
                    editDt.Rows(j)("sum_uriage_sotozei8") = sum_uriage_sotozei8

                    '合計(税率=0%)
                    editDt.Rows(j)("total_uriage0") = total_uriage0
                    editDt.Rows(j)("total_sotozei0") = total_sotozei0
                    editDt.Rows(j)("total_uriage_sotozei0") = total_uriage_sotozei0
                    '合計(税率=5%)
                    editDt.Rows(j)("total_uriage5") = total_uriage5
                    editDt.Rows(j)("total_sotozei5") = total_sotozei5
                    editDt.Rows(j)("total_uriage_sotozei5") = total_uriage_sotozei5
                    '合計(税率=8%)
                    editDt.Rows(j)("total_uriage8") = total_uriage8
                    editDt.Rows(j)("total_sotozei8") = total_sotozei8
                    editDt.Rows(j)("total_uriage_sotozei8") = total_uriage_sotozei8

                    '総合計
                    editDt.Rows(j)("total_uriage_all") = total_uriage_all
                    editDt.Rows(j)("total_sotozei_all") = total_sotozei_all
                    editDt.Rows(j)("total_uriage_sotozei_all") = total_uriage_sotozei_all
                    '===============↑2014/02/17 407662_消費税増税対応_Earth 車龍 修正 終了↑===========================

                    '最終ページ　合計を表示
                    If (intPage1MaxRow + i * intPage2MaxRow + RowsInThisPage) >= seikyusyoDataTable.Rows.Count Then
                        editDt.Rows(j)("total_cover_flg") = ""
                    Else
                        editDt.Rows(j)("total_cover_flg") = "　"
                    End If

                Next

                '>=2ページ
                sb.Append(vbCrLf)
                '[Form] 部作成
                sb.Append(fcw.CreateFormSection("PAGE=SB_SEIKYUSYO_ZKZN2"))

                '[FixedDataSection] 部作成
                sb.Append(fcw.CreateFixedDataSection(GetFixedDataSection_ZK(editDt)))

                '[TableDataSection] 部作成
                sb.Append(fcw.CreateTableDataSection(GetTableDataSection_ZK(editDt)))

            Next

        End If

        '<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>
        '<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>
        '<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>

        Return sb

    End Function

    ''' <summary>
    ''' Ｄａｔａ Ｒｅｃｏｒｄ
    ''' </summary>
    ''' <param name="seikyusyoDataTable"></param>
    ''' <param name="i"></param>
    ''' <param name="editDR"></param>
    ''' <param name="sum_uriage0"></param>
    ''' <param name="sum_sotozei0"></param>
    ''' <param name="sum_uriage_sotozei0"></param>
    ''' <param name="sum_uriage5"></param>
    ''' <param name="sum_sotozei5"></param>
    ''' <param name="sum_uriage_sotozei5"></param>
    ''' <param name="sum_uriage8"></param>
    ''' <param name="sum_sotozei8"></param>
    ''' <param name="sum_uriage_sotozei8"></param>
    ''' <param name="pages"></param>
    ''' <param name="errMsg"></param>
    ''' <param name="pageIndex"></param>
    ''' <param name="needSum"></param>
    ''' <remarks></remarks>
    Private Sub editDR_data_settings(ByVal seikyusyoDataTable As Data.DataTable, _
                                      ByVal i As Integer, _
                                      ByRef editDR As Data.DataRow, _
                                      ByRef sum_uriage0 As Decimal, _
                                      ByRef sum_sotozei0 As Decimal, _
                                      ByRef sum_uriage_sotozei0 As Decimal, _
                                      ByRef sum_uriage5 As Decimal, _
                                      ByRef sum_sotozei5 As Decimal, _
                                      ByRef sum_uriage_sotozei5 As Decimal, _
                                      ByRef sum_uriage8 As Decimal, _
                                      ByRef sum_sotozei8 As Decimal, _
                                      ByRef sum_uriage_sotozei8 As Decimal, _
                                      ByVal pages As Integer, _
                                      ByRef errMsg As String, _
                                      ByVal pageIndex As Integer, _
                              Optional ByVal needSum As Boolean = True)


        editDR("seikyuu_saki_cd_with_brc") = seikyusyoDataTable.Rows(i).Item("seikyuu")

        editDR("pages_total") = pageIndex.ToString & "/" & pages & "ページ"

        If pages = 1 Then
            editDR("pages_total") = "1ページ"
        End If

        If seikyusyoDataTable.Rows(i).Item("seikyuusyo_hak_date").Equals(DBNull.Value) Then
            editDR("seikyuusyo_hak_date") = seikyusyoDataTable.Rows(i).Item("seikyuusyo_hak_date")
        Else
            editDR("seikyuusyo_hak_date") = CDate(seikyusyoDataTable.Rows(i).Item("seikyuusyo_hak_date")).ToString("yyyyMMdd")
        End If

        'editDR("seikyuusyo_hak_date") = seikyusyoDataTable.Rows(i).Item("seikyuusyo_hak_date")

        editDR("yuubin_no") = "〒" & seikyusyoDataTable.Rows(i).Item("yuubin_no")
        editDR("jyuusyo1") = seikyusyoDataTable.Rows(i).Item("jyuusyo1")
        editDR("jyuusyo2") = seikyusyoDataTable.Rows(i).Item("jyuusyo2")
        editDR("seikyuu_saki_mei") = seikyusyoDataTable.Rows(i).Item("seikyuu_saki_mei")
        editDR("seikyuu_saki_mei2") = seikyusyoDataTable.Rows(i).Item("seikyuu_saki_mei2")
        editDR("tantousya_mei") = seikyusyoDataTable.Rows(i).Item("tantousya_mei")

        editDR("konkai_goseikyuu_gaku") = seikyusyoDataTable.Rows(i).Item("konkai_goseikyuu_gaku")


        '横山
        'editDR("DisplayName") = seikyusyoDataTable.Rows(i).Item("")
        editDR("DisplayName") = "担当 " & Me.tantouName

        If seikyusyoDataTable.Rows(i).Item("furikomi_flg").ToString = "0" Then
            '口座情報部分
            editDR("furikomisaki1") = ""
            editDR("furikomisaki2") = ""
            editDR("furikomisaki3") = ""
            editDR("furikomisaki4") = ""
            editDR("furikomisaki5") = ""
            If seikyusyoDataTable.Rows(i).Item("konkai_kaisyuu_yotei_date").Equals(DBNull.Value) Then
                editDR("konkai_kaisyuu_yotei_date") = seikyusyoDataTable.Rows(i).Item("konkai_kaisyuu_yotei_date")
            Else
                editDR("konkai_kaisyuu_yotei_date") = "上記御請求金額を" & CDate(seikyusyoDataTable.Rows(i).Item("konkai_kaisyuu_yotei_date")).ToString("yyyy年M月") & "12日に、"
                editDR("konkai_kaisyuu_yotei_date2") = "ご指定口座より自動引落させていただきます。"
                editDR("konkai_kaisyuu_yotei_date3") = "(土・日・祝の場合、翌営業日となります。)"
            End If
        ElseIf seikyusyoDataTable.Rows(i).Item("furikomi_flg").ToString = "1" Then
            '口座情報部分
            editDR("furikomisaki1") = "【振込先】"
            '2013/5/29 請求書の銀行支店コード増加に伴うEarth改修 -----------------------↓
            'editDR("furikomisaki2") = "三菱東京UFJ銀行(0005) 千代田支店(103)"
            'editDR("furikomisaki2") = "三菱東京UFJ銀行(0005) " & seikyusyoDataTable.Rows(i).Item("ginkou_siten_cd")
            editDR("furikomisaki2") = "三菱UFJ銀行(0005) " & seikyusyoDataTable.Rows(i).Item("ginkou_siten_cd")
            '2013/5/29 請求書の銀行支店コード増加に伴うEarth改修 -----------------------↑
            editDR("furikomisaki3") = "    普通預金 " & seikyusyoDataTable.Rows(i).Item("nyuukin_kouza_no")
            editDR("furikomisaki4") = ""
            editDR("furikomisaki5") = ""
            editDR("konkai_kaisyuu_yotei_date") = ""
            If seikyusyoDataTable.Rows(i).Item("konkai_kaisyuu_yotei_date").Equals(DBNull.Value) Then
                editDR("konkai_kaisyuu_yotei_date2") = seikyusyoDataTable.Rows(i).Item("konkai_kaisyuu_yotei_date")
            Else
                editDR("konkai_kaisyuu_yotei_date2") = "上記御請求金額を" & CDate(seikyusyoDataTable.Rows(i).Item("konkai_kaisyuu_yotei_date")).ToString("yyyy年M月d日") & "までにお振込み下さい。"
            End If
            editDR("konkai_kaisyuu_yotei_date3") = ""
        Else

            'errMsg = Messages.Instance.MSG2030E
            'Exit Sub

        End If


        '帳票上TBL部
        editDR("zenkai_goseikyuu_gaku") = seikyusyoDataTable.Rows(i).Item("kurikosi_gaku")
        editDR("gonyuukin_gaku") = seikyusyoDataTable.Rows(i).Item("gonyuukin_gaku")
        editDR("sousai_gaku") = seikyusyoDataTable.Rows(i).Item("sousai_gaku")
        editDR("tyousei_gaku") = seikyusyoDataTable.Rows(i).Item("tyousei_gaku")
        editDR("konkai_goseikyuu_gaku2") = seikyusyoDataTable.Rows(i).Item("konkai_goseikyuu_gaku")
        editDR("konkai_kurikosi_gaku") = seikyusyoDataTable.Rows(i).Item("konkai_kurikosi_gaku")

        'データ　TBL部
        If seikyusyoDataTable.Rows(i).Item("denpyou_uri_date").Equals(DBNull.Value) Then
            editDR("denpyou_uri_date") = seikyusyoDataTable.Rows(i).Item("denpyou_uri_date")
        Else
            editDR("denpyou_uri_date") = CDate(seikyusyoDataTable.Rows(i).Item("denpyou_uri_date")).ToString("yyyyMMdd")
        End If

        editDR("kbnwihtbangou") = seikyusyoDataTable.Rows(i).Item("bukken_no")


        '2018/12/05 李松涛 JHS0003_Earth請求書帳票の項目追加 ↓
        'editDR("hinmei") = seikyusyoDataTable.Rows(i).Item("hinmei")
        'If seikyusyoDataTable.Rows(i).Item("bukken_mei").ToString.Trim = "" Then
        '    editDR("bukenmei") = ""
        'Else
        '    editDR("bukenmei") = seikyusyoDataTable.Rows(i).Item("bukken_mei") & " 邸"
        'End If

        '商品名 依頼担当者名追加
        editDR("hinmei") = seikyusyoDataTable.Rows(i).Item("hinmei").ToString.Trim & GetSyouhinMeiByFlg(seikyusyoDataTable, i)
        '商品名 契約No追加
        If seikyusyoDataTable.Rows(i).Item("bukken_mei").ToString.Trim = "" Then
            editDR("bukenmei") = ""
        Else
            editDR("bukenmei") = seikyusyoDataTable.Rows(i).Item("bukken_mei") & " 邸"
        End If
        editDR("bukenmei") &= GetBukenMeiByFlg(seikyusyoDataTable, i)
        editDR("font_size") = GetFontSize(editDR("hinmei"))
        '2018/12/05 李松涛 JHS0003_Earth請求書帳票の項目追加 ↑


        editDR("suu") = seikyusyoDataTable.Rows(i).Item("suu")
        editDR("tanka") = seikyusyoDataTable.Rows(i).Item("tanka")
        editDR("uri_gaku") = seikyusyoDataTable.Rows(i).Item("uri_gaku")
        editDR("sotozei_gaku") = seikyusyoDataTable.Rows(i).Item("sotozei_gaku")
        editDR("uriage_sotosei") = seikyusyoDataTable.Rows(i).Item("zeikomi_gaku")

        '===============↓2014/02/17 407662_消費税増税対応_Earth 車龍 修正 開始↓===========================
        Dim strZeiritu As String = String.Empty
        strZeiritu = TrimNull(seikyusyoDataTable.Rows(i).Item("zeiritu"))

        If strZeiritu.Equals(String.Empty) Then
            editDR("zeiritu") = String.Empty
        Else
            strZeiritu = Math.Round((Convert.ToDecimal(strZeiritu) * 100), 0).ToString
            editDR("zeiritu") = strZeiritu & "%"
        End If
        '===============↑2014/02/17 407662_消費税増税対応_Earth 車龍 修正 終了↑===========================

        If needSum Then
            '全件のみ
            '===============↓2014/02/17 407662_消費税増税対応_Earth 車龍 修正 開始↓===========================
            ''小計１
            'If Not editDR("uri_gaku").Equals(DBNull.Value) Then
            '    sum_uriage += CDec(editDR("uri_gaku"))
            'End If

            ''小計２
            'If Not editDR("sotozei_gaku").Equals(DBNull.Value) Then
            '    sum_sotozei += CDec(editDR("sotozei_gaku"))
            'End If

            ''小計３
            'If Not editDR("uriage_sotosei").Equals(DBNull.Value) Then
            '    sum_uriage_sotozei += CDec(editDR("uriage_sotosei"))
            'End If

            Dim decUriGaku As Decimal = 0
            Dim decSotozeiGaku As Decimal = 0
            Dim decUriageSotosei As Decimal = 0

            If TrimNull(editDR("uri_gaku")).Equals(String.Empty) Then
                decUriGaku = 0
            Else
                decUriGaku = Convert.ToDecimal(editDR("uri_gaku"))
            End If

            If TrimNull(editDR("sotozei_gaku")).Equals(String.Empty) Then
                decSotozeiGaku = 0
            Else
                decSotozeiGaku = Convert.ToDecimal(editDR("sotozei_gaku"))
            End If

            If TrimNull(editDR("uriage_sotosei")).Equals(String.Empty) Then
                decUriageSotosei = 0
            Else
                decUriageSotosei = Convert.ToDecimal(editDR("uriage_sotosei"))
            End If

            Select Case strZeiritu
                Case "0"
                    sum_uriage0 += decUriGaku '小計_税別金額(税率=0%)
                    sum_sotozei0 += decSotozeiGaku '小計_消費税(税率=0%)
                    sum_uriage_sotozei0 += decUriageSotosei '小計_税込金額(税率=0%)

                    '消費税=0の場合、税率・消費税は非表示
                    editDR("zeiritu") = String.Empty
                    editDR("sotozei_gaku") = String.Empty

                Case "8" '元５⇒８
                    sum_uriage5 += decUriGaku '小計_税別金額(税率=5%)
                    sum_sotozei5 += decSotozeiGaku '小計_消費税(税率=5%)
                    sum_uriage_sotozei5 += decUriageSotosei '小計_税込金額(税率=5%)

                Case "10" '元８⇒１０
                    sum_uriage8 += decUriGaku '小計_税別金額(税率=8%)
                    sum_sotozei8 += decSotozeiGaku '小計_消費税(税率=8%)
                    sum_uriage_sotozei8 += decUriageSotosei '小計_税込金額(税率=8%)

            End Select
            '===============↑2014/02/17 407662_消費税増税対応_Earth 車龍 修正 終了↑===========================
        End If

    End Sub

    ''' <summary>
    ''' 物件別請求書用紙を使用
    ''' </summary>
    ''' <param name="seikyusyoDataTable"></param>
    ''' <param name="errMsg"></param>
    ''' <remarks></remarks>
    Private Function seikyusyo_BK(ByVal seikyusyoDataTable As Data.DataTable, ByRef errMsg As String) _
                                                                                            As StringBuilder

        '---検索したデータを編集する。---st
        Dim editDt As New DataTable
        Dim editDR As Data.DataRow = editDt.NewRow
        Dim sb As New StringBuilder
        Dim oldBukkenNo As String = ""               '元物件No
        Dim intTempRow As Integer               '総ページ数を計算用
        Dim intPageTatal As Integer             '総ページ数
        Dim strPageTotal As String              '総ページ数（表示用）
        Dim intDisplayTableRows As Integer      '一ページ内表示するデータ行数
        Dim intCurPageIndex As Integer          '該当ページ
        '===============↓2014/02/17 407662_消費税増税対応_Earth 車龍 修正 開始↓===========================
        'intDisplayTableRows = 25
        intDisplayTableRows = 17
        '===============↑2014/02/17 407662_消費税増税対応_Earth 車龍 修正 終了↑===========================

        editDt.Columns.Add("pages_total", GetType(String))
        editDt.Columns.Add("seikyuu_saki_cd_with_brc", GetType(String))
        editDt.Columns.Add("seikyuusyo_hak_date", GetType(String))
        editDt.Columns.Add("yuubin_no", GetType(String))
        editDt.Columns.Add("jyuusyo1", GetType(String))
        editDt.Columns.Add("jyuusyo2", GetType(String))
        editDt.Columns.Add("seikyuu_saki_mei", GetType(String))
        editDt.Columns.Add("seikyuu_saki_mei2", GetType(String))
        editDt.Columns.Add("tantousya_mei", GetType(String))
        editDt.Columns.Add("onkai_kaisyuu_yotei_date", GetType(String))         '入金予定日
        editDt.Columns.Add("onkai_kaisyuu_yotei_date2", GetType(String))        '入金予定日2
        editDt.Columns.Add("onkai_kaisyuu_yotei_date3", GetType(String))        '入金予定日3
        editDt.Columns.Add("DisplayName", GetType(String))                      '担当者名
        editDt.Columns.Add("furikomisaki1", GetType(String))
        editDt.Columns.Add("furikomisaki2", GetType(String))
        editDt.Columns.Add("furikomisaki3", GetType(String))
        editDt.Columns.Add("furikomisaki4", GetType(String))
        editDt.Columns.Add("furikomisaki5", GetType(String))
        editDt.Columns.Add("zenkai_goseikyuu_gaku", GetType(String))
        editDt.Columns.Add("gonyuukin_gaku", GetType(String))
        editDt.Columns.Add("sousai_gaku", GetType(String))
        editDt.Columns.Add("tyousei_gaku", GetType(String))
        editDt.Columns.Add("konkai_goseikyuu_gaku2", GetType(String))
        editDt.Columns.Add("konkai_kurikosi_gaku", GetType(String))
        editDt.Columns.Add("bukken_no", GetType(String))
        editDt.Columns.Add("bukken_mei", GetType(String))
        editDt.Columns.Add("hinmei", GetType(String))
        editDt.Columns.Add("suu", GetType(String))
        editDt.Columns.Add("tanka", GetType(String))
        editDt.Columns.Add("uri_gaku", GetType(String))
        editDt.Columns.Add("sotozei_gaku", GetType(String))
        editDt.Columns.Add("uri_sotozei", GetType(String))
        '===============↓2014/02/17 407662_消費税増税対応_Earth 車龍 追加 開始↓===========================
        editDt.Columns.Add("zeiritu", GetType(String)) '税率
        editDt.Columns.Add("total_uriage0", GetType(String)) '合計_税別金額(税率=0%)
        editDt.Columns.Add("total_sotozei0", GetType(String)) '合計_消費税(税率=0%)
        editDt.Columns.Add("total_uriage_sotozei0", GetType(String)) '合計_税込金額(税率=0%)
        editDt.Columns.Add("total_uriage5", GetType(String)) '合計_税別金額(税率=5%)
        editDt.Columns.Add("total_sotozei5", GetType(String)) '合計_消費税(税率=5%)
        editDt.Columns.Add("total_uriage_sotozei5", GetType(String)) '合計_税込金額(税率=5%)
        editDt.Columns.Add("total_uriage8", GetType(String)) '合計_税別金額(税率=8%)
        editDt.Columns.Add("total_sotozei8", GetType(String)) '合計_消費税(税率=8%)
        editDt.Columns.Add("total_uriage_sotozei8", GetType(String)) '合計_税込金額(税率=8%)
        '===============↑2014/02/17 407662_消費税増税対応_Earth 車龍 追加 終了↑===========================

        editDt.Columns.Add("font_size", GetType(String))

        '初期化
        intPageTatal = 1
        intCurPageIndex = 1
        oldBukkenNo = String.Empty

        'ページ数を取得する
        For i As Integer = 0 To seikyusyoDataTable.Rows.Count - 1

            '物件No変更する時
            If i <> 0 And TrimNull(seikyusyoDataTable.Rows(i).Item("bukken_no")) <> oldBukkenNo Then
                intPageTatal = intPageTatal + 1
                intTempRow = 0

                '物件No変更しない時
            Else

                'データ数＞一ページ表示するデータ数
                If intTempRow Mod intDisplayTableRows = 1 And intTempRow > intDisplayTableRows Then
                    intPageTatal = intPageTatal + 1
                End If
            End If
            intTempRow = intTempRow + 1
            oldBukkenNo = TrimNull(seikyusyoDataTable.Rows(i).Item("bukken_no"))
        Next

        '帳票に表示するページ数のを設定する。
        If intPageTatal > 1 Then
            strPageTotal = "/" & intPageTatal & "ページ"
        Else
            strPageTotal = "ページ"
        End If

        '---検索したデータを編集する。---ed
        'ヘッダー部作成
        If Me.headFlg Then
        Else
            '[Head] 部作成
            sb.AppendLine(fcw.CreateDatHeader(APOST.ToString))
            Me.headFlg = True
        End If

        '   [Form] 部作成
        sb.Append(vbCrLf)
        sb.Append(fcw.CreateFormSection("PAGE=SB_SEIKYUSYO_BK"))

        '===============↓2014/02/17 407662_消費税増税対応_Earth 車龍 追加 開始↓===========================
        Dim total_uriage0 As Decimal = 0 '合計_税別金額(税率=0%)
        Dim total_sotozei0 As Decimal = 0 '合計_消費税(税率=0%)
        Dim total_uriage_sotozei0 As Decimal = 0 '合計_税込金額(税率=0%)
        Dim total_uriage5 As Decimal = 0 '合計_税別金額(税率=5%)
        Dim total_sotozei5 As Decimal = 0 '合計_消費税(税率=5%)
        Dim total_uriage_sotozei5 As Decimal = 0 '合計_税込金額(税率=5%)
        Dim total_uriage8 As Decimal = 0 '合計_税別金額(税率=8%)
        Dim total_sotozei8 As Decimal = 0 '合計_消費税(税率=8%)
        Dim total_uriage_sotozei8 As Decimal = 0 '合計_税込金額(税率=8%)
        '===============↑2014/02/17 407662_消費税増税対応_Earth 車龍 追加 終了↑===========================

        'DATデータを作成

        '先頭行項目(繰越残高の取得する)
        For i As Integer = 0 To seikyusyoDataTable.Rows.Count - 1

            If i <> 0 And TrimNull(seikyusyoDataTable.Rows(i).Item("bukken_no")) <> oldBukkenNo Then

                If intCurPageIndex > 1 Then
                    '   [Form] 部作成
                    sb.Append(vbCrLf)
                    sb.AppendLine(fcw.CreateFormSection("PAGE=SB_SEIKYUSYO_BK"))
                End If

                intCurPageIndex = intCurPageIndex + 1

                '===============↓2014/02/17 407662_消費税増税対応_Earth 車龍 追加 開始↓===========================
                '各税率の合計を整理する
                For j As Integer = 0 To editDt.Rows.Count - 1
                    editDt.Rows(j).Item("total_uriage0") = total_uriage0 '合計_税別金額(税率=0%)
                    editDt.Rows(j).Item("total_sotozei0") = total_sotozei0 '合計_消費税(税率=0%)
                    editDt.Rows(j).Item("total_uriage_sotozei0") = total_uriage_sotozei0 '合計_税込金額(税率=0%)
                    editDt.Rows(j).Item("total_uriage5") = total_uriage5 '合計_税別金額(税率=5%)
                    editDt.Rows(j).Item("total_sotozei5") = total_sotozei5 '合計_消費税(税率=5%)
                    editDt.Rows(j).Item("total_uriage_sotozei5") = total_uriage_sotozei5 '合計_税込金額(税率=5%)
                    editDt.Rows(j).Item("total_uriage8") = total_uriage8 '合計_税別金額(税率=8%)
                    editDt.Rows(j).Item("total_sotozei8") = total_sotozei8 '合計_消費税(税率=8%)
                    editDt.Rows(j).Item("total_uriage_sotozei8") = total_uriage_sotozei8 '合計_税込金額(税率=8%)
                Next
                '===============↑2014/02/17 407662_消費税増税対応_Earth 車龍 追加 終了↑===========================

                '   [FixedDataSection] 部作成
                sb.Append(vbCrLf)
                sb.Append(fcw.CreateFixedDataSection(GetBukkenFixedDataSection(editDt)))
                '   [TableDataSection] 部作成
                sb.Append(fcw.CreateTableDataSection(GetBukkenTableDataSection(editDt)))

                For j As Integer = editDt.Rows.Count - 1 To 0 Step -1
                    editDt.Rows.RemoveAt(j)
                Next

                '===============↓2014/02/17 407662_消費税増税対応_Earth 車龍 追加 開始↓===========================
                '各税率の合計をクリアする
                total_uriage0 = 0 '合計_税別金額(税率=0%)
                total_sotozei0 = 0 '合計_消費税(税率=0%)
                total_uriage_sotozei0 = 0 '合計_税込金額(税率=0%)
                total_uriage5 = 0 '合計_税別金額(税率=5%)
                total_sotozei5 = 0 '合計_消費税(税率=5%)
                total_uriage_sotozei5 = 0 '合計_税込金額(税率=5%)
                total_uriage8 = 0 '合計_税別金額(税率=8%)
                total_sotozei8 = 0 '合計_消費税(税率=8%)
                total_uriage_sotozei8 = 0 '合計_税込金額(税率=8%)
                '===============↑2014/02/17 407662_消費税増税対応_Earth 車龍 追加 終了↑===========================

                editDR = editDt.NewRow
                'editDt.Clear()
            End If

            '総ページ数
            editDR("pages_total") = intCurPageIndex & strPageTotal

            '請求先コード-請求先枝番
            editDR("seikyuu_saki_cd_with_brc") = TrimNull(seikyusyoDataTable.Rows(i).Item("seikyuu"))

            '請求書発行日
            editDR("seikyuusyo_hak_date") = Format(seikyusyoDataTable.Rows(i).Item("seikyuusyo_hak_date"), "yyyy年M月d日")

            '郵便番号
            editDR("yuubin_no") = "〒" & TrimNull(seikyusyoDataTable.Rows(i).Item("yuubin_no"))
            If Trim(editDR("yuubin_no")) = "〒" Then
                editDR("yuubin_no") = ""
            End If

            '住所1
            editDR("jyuusyo1") = TrimNull(seikyusyoDataTable.Rows(i).Item("jyuusyo1"))

            '住所2
            editDR("jyuusyo2") = TrimNull(seikyusyoDataTable.Rows(i).Item("jyuusyo2"))

            '請求先名
            editDR("seikyuu_saki_mei") = TrimNull(seikyusyoDataTable.Rows(i).Item("seikyuu_saki_mei"))

            '請求先名2
            editDR("seikyuu_saki_mei2") = TrimNull(seikyusyoDataTable.Rows(i).Item("seikyuu_saki_mei2"))

            '担当者名

            'irai_tantousya_mei
            'If TrimNull(seikyusyoDataTable.Rows(i).Item("irai_tantousya_mei")) = "" Then
            '    editDR("tantousya_mei") = TrimNull(seikyusyoDataTable.Rows(i).Item("tantousya_mei"))
            'Else
            '    editDR("tantousya_mei") = TrimNull(seikyusyoDataTable.Rows(i).Item("irai_tantousya_mei")) & " "
            'End If

            'If TrimNull(seikyusyoDataTable.Rows(i).Item("tantousya_mei")) <> "" Then
            '    editDR("tantousya_mei") = TrimNull(seikyusyoDataTable.Rows(i).Item("tantousya_mei")) & " "
            'Else
            '    If TrimNull(seikyusyoDataTable.Rows(i).Item("irai_tantousya_mei")) = "" Then
            '        editDR("tantousya_mei") = "御担当者 "
            '    Else
            '        editDR("tantousya_mei") = TrimNull(seikyusyoDataTable.Rows(i).Item("irai_tantousya_mei")) & " "
            '    End If
            'End If

            editDR("tantousya_mei") = seikyusyoDataTable.Rows(i).Item("tantousya_mei_bk") & " "

            'ログインユーザー名
            editDR("DisplayName") = "担当　" & Me.tantouName


            '物件番号
            editDR("bukken_no") = TrimNull(seikyusyoDataTable.Rows(i).Item("bukken_no"))

            If TrimNull(seikyusyoDataTable.Rows(i).Item("bukken_mei")) = "" Then
                '物件名
                editDR("bukken_mei") = ""
            Else

                '物件名
                editDR("bukken_mei") = TrimNull(seikyusyoDataTable.Rows(i).Item("bukken_mei")) & " 邸"
            End If

            '2018/12/05 李松涛 JHS0003_Earth請求書帳票の項目追加 ↓
            'If seikyusyoDataTable.Rows(i).Item("keiyaku_no") <> "" Then
            '    editDR("bukken_mei") &= "(" & seikyusyoDataTable.Rows(i).Item("keiyaku_no") & ")"
            'End If
            '2018/12/05 李松涛 JHS0003_Earth請求書帳票の項目追加 ↑

            '請求書用紙一桁目値
            If TrimNull(seikyusyoDataTable.Rows(i).Item("furikomi_flg")) = "0" Then

                editDR("furikomisaki1") = ""
                editDR("furikomisaki2") = ""
                editDR("furikomisaki3") = ""
                editDR("furikomisaki4") = ""
                editDR("furikomisaki5") = ""

                '入金予定日
                editDR("onkai_kaisyuu_yotei_date") = "上記御請求金額を" & Format(seikyusyoDataTable.Rows(i).Item("konkai_kaisyuu_yotei_date"), "yyyy年M月") & "12日に、"
                editDR("onkai_kaisyuu_yotei_date2") = "ご指定口座より自動引落させていただきます。"
                editDR("onkai_kaisyuu_yotei_date3") = "(土・日・祝の場合、翌営業日となります。)"
            Else

                editDR("furikomisaki1") = "【振込先】"
                '2013/5/29 請求書の銀行支店コード増加に伴うEarth改修 -----------------------↓
                'editDR("furikomisaki2") = "三菱東京UFJ銀行(0005) 千代田支店(103)"
                'editDR("furikomisaki2") = "三菱東京UFJ銀行(0005) " & seikyusyoDataTable.Rows(i).Item("ginkou_siten_cd")
                editDR("furikomisaki2") = "三菱UFJ銀行(0005) " & seikyusyoDataTable.Rows(i).Item("ginkou_siten_cd")
                '2013/5/29 請求書の銀行支店コード増加に伴うEarth改修 -----------------------↑
                editDR("furikomisaki3") = "　　普通預金 " & TrimNull(seikyusyoDataTable.Rows(i).Item("nyuukin_kouza_no"))
                editDR("furikomisaki4") = ""
                editDR("furikomisaki5") = ""

                '入金予定日
                editDR("onkai_kaisyuu_yotei_date2") = "上記御請求金額を" & Format(seikyusyoDataTable.Rows(i).Item("konkai_kaisyuu_yotei_date"), "yyyy年M月d日") & "までにお振込み下さい。"
            End If

            '2018/12/05 李松涛 JHS0003_Earth請求書帳票の項目追加 ↓
            editDR("bukken_mei") &= GetBukenMeiByFlg(seikyusyoDataTable, i)
            '商品名
            editDR("hinmei") = TrimNull(seikyusyoDataTable.Rows(i).Item("hinmei"))
            editDR("font_size") = GetFontSize(editDR("hinmei"))
            '2018/12/05 李松涛 JHS0003_Earth請求書帳票の項目追加 ↑

            '数量
            editDR("suu") = TrimNull(seikyusyoDataTable.Rows(i).Item("suu"))

            '単価
            editDR("tanka") = TrimNull(seikyusyoDataTable.Rows(i).Item("tanka"))

            '税別金額
            editDR("uri_gaku") = TrimNull(seikyusyoDataTable.Rows(i).Item("uri_gaku"))

            '外税額
            editDR("sotozei_gaku") = TrimNull(seikyusyoDataTable.Rows(i).Item("sotozei_gaku"))

            '税込金額
            editDR("uri_sotozei") = TrimNull(seikyusyoDataTable.Rows(i).Item("zeikomi_gaku"))

            '===============↓2014/02/17 407662_消費税増税対応_Earth 車龍 追加 開始↓===========================
            '税率
            Dim strZeiritu As String = String.Empty
            strZeiritu = TrimNull(seikyusyoDataTable.Rows(i).Item("zeiritu"))

            If strZeiritu.Equals(String.Empty) Then
                editDR("zeiritu") = String.Empty
            Else
                strZeiritu = Math.Round((Convert.ToDecimal(strZeiritu) * 100), 0).ToString
                editDR("zeiritu") = strZeiritu & "%"

                Select Case strZeiritu
                    Case "0"
                        If Not editDR("uri_gaku").Equals(String.Empty) Then
                            total_uriage0 += Convert.ToDecimal(editDR("uri_gaku")) '合計_税別金額(税率=0%)
                        End If

                        If Not editDR("sotozei_gaku").Equals(String.Empty) Then
                            total_sotozei0 += Convert.ToDecimal(editDR("sotozei_gaku")) '合計_消費税(税率=0%)
                        End If

                        If Not editDR("uri_sotozei").Equals(String.Empty) Then
                            total_uriage_sotozei0 += Convert.ToDecimal(editDR("uri_sotozei")) '合計_税込金額(税率=0%)
                        End If

                        '消費税=0の場合、税率・消費税は非表示
                        editDR("zeiritu") = String.Empty
                        editDR("sotozei_gaku") = String.Empty

                    Case "8" '元５⇒８
                        If Not editDR("uri_gaku").Equals(String.Empty) Then
                            total_uriage5 += Convert.ToDecimal(editDR("uri_gaku")) '合計_税別金額(税率=5%)
                        End If

                        If Not editDR("sotozei_gaku").Equals(String.Empty) Then
                            total_sotozei5 += Convert.ToDecimal(editDR("sotozei_gaku")) '合計_消費税(税率=5%)
                        End If

                        If Not editDR("uri_sotozei").Equals(String.Empty) Then
                            total_uriage_sotozei5 += Convert.ToDecimal(editDR("uri_sotozei")) '合計_税込金額(税率=5%)
                        End If

                    Case "10" '元８⇒１０
                        If Not editDR("uri_gaku").Equals(String.Empty) Then
                            total_uriage8 += Convert.ToDecimal(editDR("uri_gaku")) '合計_税別金額(税率=8%)
                        End If

                        If Not editDR("sotozei_gaku").Equals(String.Empty) Then
                            total_sotozei8 += Convert.ToDecimal(editDR("sotozei_gaku")) '合計_消費税(税率=8%)
                        End If

                        If Not editDR("uri_sotozei").Equals(String.Empty) Then
                            total_uriage_sotozei8 += Convert.ToDecimal(editDR("uri_sotozei")) '合計_税込金額(税率=8%)
                        End If

                End Select
            End If
            '===============↑2014/02/17 407662_消費税増税対応_Earth 車龍 追加 終了↑===========================

            oldBukkenNo = TrimNull(seikyusyoDataTable.Rows(i).Item("bukken_no"))
            editDt.Rows.Add(editDR)
            editDR = editDt.NewRow

        Next
        '---検索したデータを編集する。---ed

        '===============↓2014/02/17 407662_消費税増税対応_Earth 車龍 追加 開始↓===========================
        '各税率の合計を整理する
        For j As Integer = 0 To editDt.Rows.Count - 1
            editDt.Rows(j).Item("total_uriage0") = total_uriage0 '合計_税別金額(税率=0%)
            editDt.Rows(j).Item("total_sotozei0") = total_sotozei0 '合計_消費税(税率=0%)
            editDt.Rows(j).Item("total_uriage_sotozei0") = total_uriage_sotozei0 '合計_税込金額(税率=0%)
            editDt.Rows(j).Item("total_uriage5") = total_uriage5 '合計_税別金額(税率=5%)
            editDt.Rows(j).Item("total_sotozei5") = total_sotozei5 '合計_消費税(税率=5%)
            editDt.Rows(j).Item("total_uriage_sotozei5") = total_uriage_sotozei5 '合計_税込金額(税率=5%)
            editDt.Rows(j).Item("total_uriage8") = total_uriage8 '合計_税別金額(税率=8%)
            editDt.Rows(j).Item("total_sotozei8") = total_sotozei8 '合計_消費税(税率=8%)
            editDt.Rows(j).Item("total_uriage_sotozei8") = total_uriage_sotozei8 '合計_税込金額(税率=8%)
        Next
        '===============↑2014/02/17 407662_消費税増税対応_Earth 車龍 追加 終了↑===========================

        '   [Form] 部作成
        sb.Append(vbCrLf)
        If intPageTatal > 1 Then
            sb.AppendLine(fcw.CreateFormSection("PAGE=SB_SEIKYUSYO_BK"))
        End If
        '   [FixedDataSection] 部作成
        sb.Append(fcw.CreateFixedDataSection(GetBukkenFixedDataSection(editDt)))

        '   [TableDataSection] 部作成
        sb.Append(fcw.CreateTableDataSection(GetBukkenTableDataSection(editDt)))

        'errMsg = fcw.GetErrMsg(fcw.WriteData(sb.ToString))

        Return sb

    End Function

    Private Sub SetTenMei(ByRef motoTable As Data.DataTable)

        Dim tempDt As Data.DataTable

        tempDt = motoTable

        Dim i As Integer
        For i = 0 To motoTable.Rows.Count - 1

            Select Case motoTable.Rows(i).Item("himoduke_table_type").ToString.Trim

                Case "1"
                    '[売上データテーブル.区分、番号で結合]
                    '売上データ -> 地盤テーブル -> 加盟店マスタ.加盟店正式名
                    tempDt = seikyusyoFcwLogic.GetHimodekeSyurui_1_tenmei( _
                                    motoTable.Rows(i).Item("uri_kbn").ToString, _
                                    motoTable.Rows(i).Item("uri_bangou").ToString _
                                                                )

                    If tempDt.Rows.Count = 0 Then
                        motoTable.Rows(i).Item("ten_cd") = "10001"
                        motoTable.Rows(i).Item("ten_mei") = " "
                    Else
                        motoTable.Rows(i).Item("ten_cd") = tempDt.Rows(0).Item("ten_cd")
                        motoTable.Rows(i).Item("ten_mei") = tempDt.Rows(0).Item("ten_mei")
                    End If

                    'motoTable.Rows(i).Item("ten_cd") = tempDt.Rows(0).Item("ten_cd")
                    'motoTable.Rows(i).Item("ten_mei") = tempDt.Rows(0).Item("ten_mei")

                Case "2"
                    '売上データ -> 店別請求テーブル.店コード -> 加盟店マスタ.加盟店正式名
                    '              加盟店マスタに上記店コードが存在しない場合
                    '売上データ -> 店別請求テーブル.店コード -> 営業所マスタ.請求先名 営業所名
                    Dim himodukeCd As String = motoTable.Rows(i).Item("himoduke_cd").ToString

                    If himodukeCd.IndexOf("$$$") > 0 Then
                        tempDt = seikyusyoFcwLogic.GetHimodekeSyurui_2_tenmei( _
                                        himodukeCd.Split("$$$")(0) _
                                        )
                        '$$$

                        If tempDt.Rows.Count = 0 Then
                            motoTable.Rows(i).Item("ten_cd") = "10001"
                            motoTable.Rows(i).Item("ten_mei") = " "
                        Else
                            motoTable.Rows(i).Item("ten_cd") = tempDt.Rows(0).Item("ten_cd")
                            motoTable.Rows(i).Item("ten_mei") = tempDt.Rows(0).Item("ten_mei")
                        End If

                        'motoTable.Rows(i).Item("ten_cd") = tempDt.Rows(0).Item("ten_cd")
                        'motoTable.Rows(i).Item("ten_mei") = tempDt.Rows(0).Item("ten_mei")

                    Else
                        motoTable.Rows(i).Item("ten_cd") = "10002"
                        motoTable.Rows(i).Item("ten_mei") = " "
                    End If


                Case "3"
                    '売上データ -> 店別初期請求テーブル.店コード -> 加盟店マスタ.加盟店正式名


                    Dim himodukeCd As String = motoTable.Rows(i).Item("himoduke_cd").ToString

                    If himodukeCd.IndexOf("$$$") > 0 Then
                        tempDt = seikyusyoFcwLogic.GetHimodekeSyurui_3_tenmei( _
                                        himodukeCd.Split("$$$")(0) _
                                        )
                        '$$$

                        If tempDt.Rows.Count = 0 Then
                            motoTable.Rows(i).Item("ten_cd") = "10001"
                            motoTable.Rows(i).Item("ten_mei") = " "
                        Else
                            motoTable.Rows(i).Item("ten_cd") = tempDt.Rows(0).Item("ten_cd")
                            motoTable.Rows(i).Item("ten_mei") = tempDt.Rows(0).Item("ten_mei")
                        End If

                        'motoTable.Rows(i).Item("ten_cd") = tempDt.Rows(0).Item("ten_cd")
                        'motoTable.Rows(i).Item("ten_mei") = tempDt.Rows(0).Item("ten_mei")
                    Else
                        motoTable.Rows(i).Item("ten_cd") = "10002"
                        motoTable.Rows(i).Item("ten_mei") = " "
                    End If


                    'Case "4"
                    '売上データ 

                    'DO NOTHING

                Case Else

                    'Need a errMsg show to the user

            End Select

        Next

    End Sub

    '全件請求書(店小計)用紙を使用
    Private Function seikyusyo_XK(ByVal seikyusyoDataTable As Data.DataTable, ByRef errMsg As String) _
                                                                                            As StringBuilder

        '---検索したデータを編集する。---st
        Dim editDt As New DataTable
        Dim editDR As Data.DataRow = editDt.NewRow
        Dim CNT1 As Integer
        Dim sb As New StringBuilder

        '===============↓2014/02/17 407662_消費税増税対応_Earth 車龍 修正 開始↓===========================
        'Dim sum_uriage As Decimal = 0
        'Dim sum_sotozei As Decimal = 0
        'Dim sum_uriage_sotozei As Decimal = 0
        'Dim total_uriage As Decimal = 0
        'Dim total_sotozei As Decimal = 0
        'Dim total_uriage_sotozei As Decimal = 0

        '小計(税率=0%)
        Dim sum_uriage0 As Decimal = 0
        Dim sum_sotozei0 As Decimal = 0
        Dim sum_uriage_sotozei0 As Decimal = 0
        '小計(税率=5%)
        Dim sum_uriage5 As Decimal = 0
        Dim sum_sotozei5 As Decimal = 0
        Dim sum_uriage_sotozei5 As Decimal = 0
        '小計(税率=8%)
        Dim sum_uriage8 As Decimal = 0
        Dim sum_sotozei8 As Decimal = 0
        Dim sum_uriage_sotozei8 As Decimal = 0

        '合計(税率=0%)
        Dim total_uriage0 As Decimal = 0
        Dim total_sotozei0 As Decimal = 0
        Dim total_uriage_sotozei0 As Decimal = 0
        '合計(税率=5%)
        Dim total_uriage5 As Decimal = 0
        Dim total_sotozei5 As Decimal = 0
        Dim total_uriage_sotozei5 As Decimal = 0
        '合計(税率=8%)
        Dim total_uriage8 As Decimal = 0
        Dim total_sotozei8 As Decimal = 0
        Dim total_uriage_sotozei8 As Decimal = 0

        '総合計
        Dim total_uriage_all As Decimal = 0
        Dim total_sotozei_all As Decimal = 0
        Dim total_uriage_sotozei_all As Decimal = 0
        '===============↑2014/02/17 407662_消費税増税対応_Earth 車龍 修正 終了↑===========================

        Dim pages As Integer

        '===============↓2014/02/17 407662_消費税増税対応_Earth 車龍 追加 開始↓===========================
        '各頁の最大行数
        Dim intPage1MaxRow As Integer
        Dim intPage2MaxRow As Integer

        intPage1MaxRow = 18 '初め頁
        intPage2MaxRow = 24 '後頁
        '===============↑2014/02/17 407662_消費税増税対応_Earth 車龍 追加 終了↑===========================

        'add feild
        Call Me.AddFeild_XK(editDt)

        'SET 店コード　店名
        Me.SetTenMei(seikyusyoDataTable)

        Dim dataView As DataView
        dataView = seikyusyoDataTable.DefaultView

        '===============↓2014/02/17 407662_消費税増税対応_Earth 車龍 修正 開始↓===========================
        'dataView.Sort = "ten_cd ASC,bukken_no ASC,syouhin_cd ASC,denpyou_uri_date ASC"
        dataView.Sort = "ten_cd ASC,bukken_no ASC,syouhin_cd ASC,denpyou_uri_date ASC,zeiritu ASC"
        '===============↑2014/02/17 407662_消費税増税対応_Earth 車龍 修正 開始↑===========================

        seikyusyoDataTable = dataView.ToTable

        Dim CNT_T As Integer = seikyusyoDataTable.Rows.Count

        '<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>
        '<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>
        '<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>
        Dim i As Integer
        Dim tenCd As String

        tenCd = seikyusyoDataTable.Rows(0).Item("ten_cd").ToString

        Dim tpRow As Data.DataRow

        tpRow = seikyusyoDataTable.NewRow

        Dim TMs As String = "TM1"
        Dim TMt As String = "TM1"
        Dim TMs2 As String = ""
        Dim TMt2 As String = ""

        '===============↓2014/02/17 407662_消費税増税対応_Earth 車龍 追加 開始↓===========================
        Dim decUriGaku As Decimal = 0 '税別金額
        Dim decSotozeiGaku As Decimal = 0 '消費税
        Dim decZeikomiGaku As Decimal = 0 '税込金額

        Dim strZeiritu As String = String.Empty '税率
        '===============↑2014/02/17 407662_消費税増税対応_Earth 車龍 追加 終了↑===========================

        'ADD 小計行
        For i = 0 To CNT_T * 2

            If i = seikyusyoDataTable.Rows.Count Then
                Exit For
            End If

            If seikyusyoDataTable.Rows(i).Item("ten_cd").ToString = tenCd Then

                '===============↓2014/02/17 407662_消費税増税対応_Earth 車龍 修正 開始↓===========================
                'If seikyusyoDataTable.Rows(i).Item("uri_gaku").Equals(DBNull.Value) Then
                '    sum_uriage += 0
                'Else
                '    sum_uriage += CDec(seikyusyoDataTable.Rows(i).Item("uri_gaku"))
                'End If

                'If seikyusyoDataTable.Rows(i).Item("sotozei_gaku").Equals(DBNull.Value) Then
                '    sum_sotozei += 0
                'Else
                '    sum_sotozei += CDec(seikyusyoDataTable.Rows(i).Item("sotozei_gaku"))
                'End If

                'If seikyusyoDataTable.Rows(i).Item("zeikomi_gaku").Equals(DBNull.Value) Then
                '    sum_uriage_sotozei += 0
                'Else
                '    sum_uriage_sotozei += CDec(seikyusyoDataTable.Rows(i).Item("zeikomi_gaku"))
                'End If

                With seikyusyoDataTable.Rows(i)

                    decUriGaku = IsNullTo0(.Item("uri_gaku")) ' IIf(.Item("uri_gaku").Equals(DBNull.Value), 0, CDec(.Item("uri_gaku")))
                    decSotozeiGaku = IsNullTo0(.Item("sotozei_gaku")) ' IIf(.Item("sotozei_gaku").Equals(DBNull.Value), 0, CDec(.Item("sotozei_gaku")))
                    decZeikomiGaku = IsNullTo0(.Item("zeikomi_gaku")) ' IIf(.Item("zeikomi_gaku").Equals(DBNull.Value), 0, CDec(.Item("zeikomi_gaku")))

                    strZeiritu = TrimNull(.Item("zeiritu"))
                    If Not strZeiritu.Equals(String.Empty) Then
                        strZeiritu = Math.Round((Convert.ToDecimal(strZeiritu) * 100), 0).ToString
                    End If

                    Select Case strZeiritu
                        Case "0"
                            sum_uriage0 += decUriGaku
                            sum_sotozei0 += decSotozeiGaku
                            sum_uriage_sotozei0 += decZeikomiGaku

                        Case "8" '元５⇒８
                            sum_uriage5 += decUriGaku
                            sum_sotozei5 += decSotozeiGaku
                            sum_uriage_sotozei5 += decZeikomiGaku

                        Case "10" '元８⇒１０
                            sum_uriage8 += decUriGaku
                            sum_sotozei8 += decSotozeiGaku
                            sum_uriage_sotozei8 += decZeikomiGaku

                    End Select

                End With
                '===============↑2014/02/17 407662_消費税増税対応_Earth 車龍 追加 終了↑===========================
            Else

                tpRow = seikyusyoDataTable.NewRow

                tenCd = seikyusyoDataTable.Rows(i).Item("ten_cd").ToString

                seikyusyoDataTable.Rows.InsertAt(tpRow, i)

                '===============↓2014/02/17 407662_消費税増税対応_Earth 車龍 修正 開始↓===========================
                'seikyusyoDataTable.Rows(i).Item("uri_gaku") = sum_uriage
                'seikyusyoDataTable.Rows(i).Item("sotozei_gaku") = sum_sotozei
                'seikyusyoDataTable.Rows(i).Item("zeikomi_gaku") = sum_uriage_sotozei

                'total_uriage += sum_uriage
                'total_sotozei += sum_sotozei
                'total_uriage_sotozei += sum_uriage_sotozei

                'sum_uriage = seikyusyoDataTable.Rows(i + 1).Item("uri_gaku")
                'sum_sotozei = seikyusyoDataTable.Rows(i + 1).Item("sotozei_gaku")
                'sum_uriage_sotozei = seikyusyoDataTable.Rows(i + 1).Item("zeikomi_gaku")

                seikyusyoDataTable.Rows(i).Item("uri_gaku") = sum_uriage0 + sum_uriage5 + sum_uriage8
                seikyusyoDataTable.Rows(i).Item("sotozei_gaku") = sum_sotozei0 + sum_sotozei5 + sum_sotozei8
                seikyusyoDataTable.Rows(i).Item("zeikomi_gaku") = sum_uriage_sotozei0 + sum_uriage_sotozei5 + sum_uriage_sotozei8

                '合計(税率=0%)
                total_uriage0 += sum_uriage0
                total_sotozei0 += sum_sotozei0
                total_uriage_sotozei0 += sum_uriage_sotozei0
                '合計(税率=5%)
                total_uriage5 += sum_uriage5
                total_sotozei5 += sum_sotozei5
                total_uriage_sotozei5 += sum_uriage_sotozei5
                '合計(税率=8%)
                total_uriage8 += sum_uriage8
                total_sotozei8 += sum_sotozei8
                total_uriage_sotozei8 += sum_uriage_sotozei8
                '総合計
                total_uriage_all = total_uriage0 + total_uriage5 + total_uriage8
                total_sotozei_all = total_sotozei0 + total_sotozei5 + total_sotozei8
                total_uriage_sotozei_all = total_uriage_sotozei0 + total_uriage_sotozei5 + total_uriage_sotozei8

                With seikyusyoDataTable.Rows(i + 1)

                    decUriGaku = IsNullTo0(.Item("uri_gaku")) ' IIf(.Item("uri_gaku").Equals(DBNull.Value), 0, CDec(.Item("uri_gaku")))
                    decSotozeiGaku = IsNullTo0(.Item("sotozei_gaku")) 'IIf(.Item("sotozei_gaku").Equals(DBNull.Value), 0, CDec(.Item("sotozei_gaku")))
                    decZeikomiGaku = IsNullTo0(.Item("zeikomi_gaku")) 'IIf(.Item("zeikomi_gaku").Equals(DBNull.Value), 0, CDec(.Item("zeikomi_gaku")))

                    strZeiritu = TrimNull(.Item("zeiritu"))
                    If Not strZeiritu.Equals(String.Empty) Then
                        strZeiritu = Math.Round((Convert.ToDecimal(strZeiritu) * 100), 0).ToString
                    End If

                    sum_uriage0 = 0
                    sum_sotozei0 = 0
                    sum_uriage_sotozei0 = 0
                    sum_uriage5 = 0
                    sum_sotozei5 = 0
                    sum_uriage_sotozei5 = 0
                    sum_uriage8 = 0
                    sum_sotozei8 = 0
                    sum_uriage_sotozei8 = 0

                    Select Case strZeiritu
                        Case "0"
                            sum_uriage0 = decUriGaku
                            sum_sotozei0 = decSotozeiGaku
                            sum_uriage_sotozei0 = decZeikomiGaku
                        Case "8" '元５⇒８
                            sum_uriage5 = decUriGaku
                            sum_sotozei5 = decSotozeiGaku
                            sum_uriage_sotozei5 = decZeikomiGaku
                        Case "10" '元８⇒１０
                            sum_uriage8 = decUriGaku
                            sum_sotozei8 = decSotozeiGaku
                            sum_uriage_sotozei8 = decZeikomiGaku

                    End Select

                End With
                '===============↑2014/02/17 407662_消費税増税対応_Earth 車龍 追加 終了↑===========================

                seikyusyoDataTable.Rows(i).Item("hinmei") = "　　　　　　　　　小　　　　計"
                seikyusyoDataTable.Rows(i).Item("yuubin_no") = seikyusyoDataTable.Rows(i - 1).Item("yuubin_no")
                seikyusyoDataTable.Rows(i).Item("jyuusyo1") = seikyusyoDataTable.Rows(i - 1).Item("jyuusyo1")
                seikyusyoDataTable.Rows(i).Item("jyuusyo2") = seikyusyoDataTable.Rows(i - 1).Item("jyuusyo2")

                TMs = TMs & ",TM1_" & (i + 1).ToString
                If TMs2 = "" Then
                    TMs2 = "TM2_" & i.ToString
                Else
                    TMs2 = TMs2 & ",TM2_" & i.ToString
                End If

                If i + 1 <= intPage1MaxRow Then
                    TMt = TMt & ",TM1_" & (i + 1).ToString
                Else
                    TMt = TMt & ",TM1_" & (((i + 1) - intPage1MaxRow) Mod intPage2MaxRow).ToString
                    TMt = TMt.Replace("_0", "_" & intPage2MaxRow.ToString)

                End If

                If i + 1 <= (intPage1MaxRow - 1) Then
                    If TMt2 = "" Then
                        TMt2 = "TM2_" & i.ToString
                    Else
                        TMt2 = TMt2 & ",TM2_" & i.ToString
                    End If
                Else
                    If TMt2 = "" Then
                        TMt2 = "TM2_" & ((i - (intPage1MaxRow - 1)) Mod intPage2MaxRow).ToString
                    Else
                        TMt2 = TMt2 & ",TM2_" & ((i - (intPage1MaxRow - 1)) Mod intPage2MaxRow).ToString
                    End If
                End If

                i += 1

            End If

        Next

        tpRow = seikyusyoDataTable.NewRow
        'ADD 小計行 補足
        seikyusyoDataTable.Rows.InsertAt(tpRow, i)

        '===============↓2014/02/17 407662_消費税増税対応_Earth 車龍 修正 開始↓===========================
        'seikyusyoDataTable.Rows(i).Item("uri_gaku") = sum_uriage
        'seikyusyoDataTable.Rows(i).Item("sotozei_gaku") = sum_sotozei
        'seikyusyoDataTable.Rows(i).Item("zeikomi_gaku") = sum_uriage_sotozei

        'total_uriage += sum_uriage
        'total_sotozei += sum_sotozei
        'total_uriage_sotozei += sum_uriage_sotozei

        'sum_uriage = 0
        'sum_sotozei = 0
        'sum_uriage_sotozei = 0

        seikyusyoDataTable.Rows(i).Item("uri_gaku") = sum_uriage0 + sum_uriage5 + sum_uriage8
        seikyusyoDataTable.Rows(i).Item("sotozei_gaku") = sum_sotozei0 + sum_sotozei5 + sum_sotozei8
        seikyusyoDataTable.Rows(i).Item("zeikomi_gaku") = sum_uriage_sotozei0 + sum_uriage_sotozei5 + sum_uriage_sotozei8

        '合計(税率=0%)
        total_uriage0 += sum_uriage0
        total_sotozei0 += sum_sotozei0
        total_uriage_sotozei0 += sum_uriage_sotozei0
        '合計(税率=5%)
        total_uriage5 += sum_uriage5
        total_sotozei5 += sum_sotozei5
        total_uriage_sotozei5 += sum_uriage_sotozei5
        '合計(税率=8%)
        total_uriage8 += sum_uriage8
        total_sotozei8 += sum_sotozei8
        total_uriage_sotozei8 += sum_uriage_sotozei8
        '総合計
        total_uriage_all = total_uriage0 + total_uriage5 + total_uriage8
        total_sotozei_all = total_sotozei0 + total_sotozei5 + total_sotozei8
        total_uriage_sotozei_all = total_uriage_sotozei0 + total_uriage_sotozei5 + total_uriage_sotozei8

        sum_uriage0 = 0
        sum_sotozei0 = 0
        sum_uriage_sotozei0 = 0
        sum_uriage5 = 0
        sum_sotozei5 = 0
        sum_uriage_sotozei5 = 0
        sum_uriage8 = 0
        sum_sotozei8 = 0
        sum_uriage_sotozei8 = 0
        '===============↑2014/02/17 407662_消費税増税対応_Earth 車龍 追加 終了↑===========================

        seikyusyoDataTable.Rows(i).Item("hinmei") = "　　　　　　　　　小　　　　計"
        seikyusyoDataTable.Rows(i).Item("yuubin_no") = seikyusyoDataTable.Rows(i - 1).Item("yuubin_no")
        seikyusyoDataTable.Rows(i).Item("jyuusyo1") = seikyusyoDataTable.Rows(i - 1).Item("jyuusyo1")
        seikyusyoDataTable.Rows(i).Item("jyuusyo2") = seikyusyoDataTable.Rows(i - 1).Item("jyuusyo2")
        TMs = TMs & ",TM1_" & (i + 1).ToString
        If TMs2 = "" Then
            TMs2 = "TM2_" & i.ToString
        Else
            TMs2 = TMs2 & ",TM2_" & i.ToString
        End If

        If i + 1 <= intPage1MaxRow Then

            TMt = TMt & ",TM1_" & (i + 1).ToString

        Else

            TMt = TMt & ",TM1_" & (((i + 1) - intPage1MaxRow) Mod intPage2MaxRow).ToString
            TMt = TMt.Replace("_0", "_" & intPage2MaxRow.ToString)

        End If

        If i + 1 <= (intPage1MaxRow - 1) Then
            '===============↓2014/02/17 407662_消費税増税対応_Earth 車龍 修正 開始↓===========================
            'TMt2 = TMt2 & ",TM2_" & i.ToString
            If TMt2 = "" Then
                TMt2 = "TM2_" & i.ToString
            Else
                TMt2 = TMt2 & ",TM2_" & i.ToString
            End If
            '===============↑2014/02/17 407662_消費税増税対応_Earth 車龍 修正 終了↑===========================
        Else
            '===============↓2014/02/17 407662_消費税増税対応_Earth 車龍 修正 開始↓===========================
            'TMt2 = TMt2 & ",TM2_" & ((i - 18) Mod 25).ToString
            If TMt2 = "" Then
                TMt2 = "TM2_" & ((i - (intPage1MaxRow - 1)) Mod intPage2MaxRow).ToString
            Else
                TMt2 = TMt2 & ",TM2_" & ((i - (intPage1MaxRow - 1)) Mod intPage2MaxRow).ToString
            End If
            '===============↑2014/02/17 407662_消費税増税対応_Earth 車龍 修正 終了↑===========================
            'TMt2 = TMt2.Replace("_0", "_23")
        End If


        CNT_T = seikyusyoDataTable.Rows.Count

        For i = 0 To CNT_T - 1

            If seikyusyoDataTable.Rows(i).Item("hinmei").ToString = "　　　　　　　　　小　　　　計" Then

                Dim headStrs As String = _
                "seikyuu,seikyuusyo_hak_date,seikyuu_saki_mei,seikyuu_saki_mei2,tantousya_mei,tantousya_mei"

                Dim headItems As String()
                headItems = headStrs.Split(",")

                For x As Integer = 0 To headItems.Length - 1

                    seikyusyoDataTable.Rows(i).Item(headItems(x)) _
                    = seikyusyoDataTable.Rows(0).Item(headItems(x))

                Next

            End If

        Next

        '第一ページDAT作成

        If CNT_T > intPage1MaxRow Then
            '>1Page　の場合
            CNT1 = intPage1MaxRow

        Else
            '1Page　の場合
            CNT1 = CNT_T

        End If

        pages = Math.Ceiling((CNT_T - intPage1MaxRow) / intPage2MaxRow) + 1

        'データ作成
        'Dim i As Integer
        For i = 0 To CNT1 - 1

            editDR = editDt.NewRow

            Call Me.editDR_data_settings(seikyusyoDataTable, _
                                        i, _
                                        editDR, _
                                        sum_uriage0, _
                                        sum_sotozei0, _
                                        sum_uriage_sotozei0, _
                                        sum_uriage5, _
                                        sum_sotozei5, _
                                        sum_uriage_sotozei5, _
                                        sum_uriage8, _
                                        sum_sotozei8, _
                                        sum_uriage_sotozei8, _
                                        pages, _
                                        errMsg, _
                                        1, _
                                        False)

            editDt.Rows.Add(editDR)

        Next

        'total_uriage += sum_uriage
        'total_sotozei += sum_sotozei
        'total_uriage_sotozei += sum_uriage_sotozei

        For i = 0 To CNT1 - 1

            'editDt.Rows(i).Item("sum_uriage") = sum_uriage
            'editDt.Rows(i).Item("sum_sotozei") = sum_sotozei
            'editDt.Rows(i).Item("sum_uriage_sotozei") = sum_uriage_sotozei

            editDt.Rows(i).Item("TM1") = seikyusyoDataTable.Rows(0).Item("ten_mei").ToString

            Dim j As Integer
            For j = 1 To TMs.Split(",").Length - 1

                If CInt(TMs.Split(",")(j).Substring(4)) > intPage1MaxRow Then
                    Exit For
                End If

                If CInt(TMs.Split(",")(j).Substring(4)) > CNT_T - 1 Then
                    editDt.Rows(i).Item(TMs.Split(",")(j)) = "　"
                Else

                    editDt.Rows(i).Item(TMs.Split(",")(j)) = _
                    seikyusyoDataTable.Rows(CInt(TMs.Split(",")(j).Substring(4))).Item("ten_mei")

                End If

            Next
            For j = 0 To TMs2.Split(",").Length - 1

                If CInt(TMs2.Split(",")(j).Substring(4)) > intPage1MaxRow Then
                    Exit For
                End If

                editDt.Rows(i).Item(TMs2.Split(",")(j)) = "　"

            Next

            '===============↓2014/02/17 407662_消費税増税対応_Earth 車龍 修正 開始↓===========================
            'editDt.Rows(i).Item("total_uriage") = total_uriage
            'editDt.Rows(i).Item("total_sotozei") = total_sotozei
            'editDt.Rows(i).Item("total_uriage_sotozei") = total_uriage_sotozei

            '合計(税率=0%)
            editDt.Rows(i).Item("total_uriage0") = total_uriage0
            editDt.Rows(i).Item("total_sotozei0") = total_sotozei0
            editDt.Rows(i).Item("total_uriage_sotozei0") = total_uriage_sotozei0
            '合計(税率=5%)
            editDt.Rows(i).Item("total_uriage5") = total_uriage5
            editDt.Rows(i).Item("total_sotozei5") = total_sotozei5
            editDt.Rows(i).Item("total_uriage_sotozei5") = total_uriage_sotozei5
            '合計(税率=8%)
            editDt.Rows(i).Item("total_uriage8") = total_uriage8
            editDt.Rows(i).Item("total_sotozei8") = total_sotozei8
            editDt.Rows(i).Item("total_uriage_sotozei8") = total_uriage_sotozei8
            '総合計
            editDt.Rows(i).Item("total_uriage_all") = total_uriage_all
            editDt.Rows(i).Item("total_sotozei_all") = total_sotozei_all
            editDt.Rows(i).Item("total_uriage_sotozei_all") = total_uriage_sotozei_all
            '===============↑2014/02/17 407662_消費税増税対応_Earth 車龍 修正 終了↑===========================

            If CNT_T <= intPage1MaxRow Then
                editDt.Rows(i).Item("total_cover_flg") = ""
            Else
                editDt.Rows(i).Item("total_cover_flg") = "　"
            End If

        Next

        Dim kk As Integer
        For kk = 0 To editDt.Rows.Count - 1

            If editDt.Rows(kk).Item("uri_gaku").Equals(DBNull.Value) Then
            Else
                editDt.Rows(kk).Item("uri_gaku") = _
                CDec(editDt.Rows(kk).Item("uri_gaku")).ToString("###,###")
            End If

            If editDt.Rows(kk).Item("sotozei_gaku").Equals(DBNull.Value) Then
            Else
                editDt.Rows(kk).Item("sotozei_gaku") = _
                CDec(editDt.Rows(kk).Item("sotozei_gaku")).ToString("###,###")
            End If

            If editDt.Rows(kk).Item("uriage_sotosei").Equals(DBNull.Value) Then
            Else
                editDt.Rows(kk).Item("uriage_sotosei") = _
               CDec(editDt.Rows(kk).Item("uriage_sotosei")).ToString("###,###")
            End If

            '===============↓2014/02/17 407662_消費税増税対応_Earth 車龍 追加 終了↓===========================
            If editDt.Rows(kk).Item("zeiritu").ToString.Equals("0%") Then
                editDt.Rows(kk).Item("zeiritu") = String.Empty
                editDt.Rows(kk).Item("sotozei_gaku") = String.Empty
            End If
            '===============↑2014/02/17 407662_消費税増税対応_Earth 車龍 追加 終了↑===========================

            If editDt.Rows(kk).Item("hinmei").ToString = "　　　　　　　　　小　　　　計" Then

                editDt.Rows(kk).Item("uri_gaku") = _
                editDt.Rows(kk).Item("uri_gaku").ToString.PadLeft(13, CChar(" ")).PadRight(26, CChar(" "))

                editDt.Rows(kk).Item("sotozei_gaku") = _
                editDt.Rows(kk).Item("sotozei_gaku").ToString.PadLeft(13, CChar(" ")).PadRight(26, CChar(" "))

                '===============↓2014/02/17 407662_消費税増税対応_Earth 車龍 修正 開始↓===========================
                'editDt.Rows(kk).Item("uriage_sotosei") = _
                'editDt.Rows(kk).Item("uriage_sotosei").ToString.PadLeft(13, CChar(" ")).PadRight(26, CChar(" "))
                editDt.Rows(kk).Item("uriage_sotosei") = _
                editDt.Rows(kk).Item("uriage_sotosei").ToString.PadLeft(15, CChar(" ")).PadRight(28, CChar(" "))
                '===============↑2014/02/17 407662_消費税増税対応_Earth 車龍 修正 終了↑===========================

            End If

        Next

        If Me.headFlg Then
        Else
            '[Head] 部作成
            sb.Append(fcw.CreateDatHeader(APOST.ToString))

            Me.headFlg = True
        End If

        sb.Append(vbCrLf)
        '[Form] 部作成
        sb.Append(fcw.CreateFormSection("PAGE=SB_SEIKYUSYO_XK1"))

        '[FixedDataSection] 部作成
        sb.Append(fcw.CreateFixedDataSection(GetFixedDataSection_XK(editDt)))

        '[TableDataSection] 部作成
        sb.Append(fcw.CreateTableDataSection(GetTableDataSection_XK(editDt)))

        '<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>
        '<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>
        '<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>

        '第二ページ（＞＝２）DAT作成
        If CNT_T > intPage1MaxRow Then

            Dim sb2_Pages As Integer = Math.Ceiling((CNT_T - intPage1MaxRow) / intPage2MaxRow)
            Dim RowsInThisPage As Integer = 0

            For i = 0 To sb2_Pages - 1

                editDt.Rows.Clear()

                'RowsInThisPage
                If intPage1MaxRow + (intPage2MaxRow * (i + 1)) <= CNT_T Then
                    RowsInThisPage = intPage2MaxRow
                Else
                    RowsInThisPage = CNT_T - (intPage1MaxRow + intPage2MaxRow * i)
                End If

                Dim j As Integer
                For j = 0 To RowsInThisPage - 1

                    editDR = editDt.NewRow

                    Call Me.editDR_data_settings(seikyusyoDataTable, _
                            intPage1MaxRow + i * intPage2MaxRow + j, _
                            editDR, _
                            sum_uriage0, _
                            sum_sotozei0, _
                            sum_uriage_sotozei0, _
                            sum_uriage5, _
                            sum_sotozei5, _
                            sum_uriage_sotozei5, _
                            sum_uriage8, _
                            sum_sotozei8, _
                            sum_uriage_sotozei8, _
                            pages, _
                            errMsg, _
                            i + 2, _
                            False)

                    editDt.Rows.Add(editDR)

                Next


                For j = 0 To RowsInThisPage - 1

                    Dim k As Integer
                    For k = 1 To TMs.Split(",").Length - 1

                        If CInt(TMs.Split(",")(k).Substring(4)) > intPage1MaxRow + i * intPage2MaxRow _
                        And CInt(TMs.Split(",")(k).Substring(4)) <= intPage1MaxRow + i * intPage2MaxRow + RowsInThisPage Then

                            If CInt(TMs.Split(",")(k).Substring(4)) > CNT_T - 1 Then

                                editDt.Rows(j).Item(TMt.Split(",")(k)) = "　"

                            Else

                                editDt.Rows(j).Item(TMt.Split(",")(k)) = _
                                seikyusyoDataTable.Rows(CInt(TMs.Split(",")(k).Substring(4))).Item("ten_mei")

                            End If

                        End If

                    Next

                    For k = 0 To TMs2.Split(",").Length - 1

                        If CInt(TMs2.Split(",")(k).Substring(4)) >= intPage1MaxRow + i * intPage2MaxRow _
                        And CInt(TMs2.Split(",")(k).Substring(4)) < intPage1MaxRow + i * intPage2MaxRow + RowsInThisPage Then

                            editDt.Rows(j).Item(TMt2.Split(",")(k)) = "　"

                        End If

                    Next

                    '===============↓2014/02/17 407662_消費税増税対応_Earth 車龍 修正 開始↓===========================
                    'editDt.Rows(j)("total_uriage") = total_uriage
                    'editDt.Rows(j)("total_sotozei") = total_sotozei
                    'editDt.Rows(j)("total_uriage_sotozei") = total_uriage_sotozei

                    '合計(税率=0%)
                    editDt.Rows(j)("total_uriage0") = total_uriage0
                    editDt.Rows(j)("total_sotozei0") = total_sotozei0
                    editDt.Rows(j)("total_uriage_sotozei0") = total_uriage_sotozei0
                    '合計(税率=5%)
                    editDt.Rows(j)("total_uriage5") = total_uriage5
                    editDt.Rows(j)("total_sotozei5") = total_sotozei5
                    editDt.Rows(j)("total_uriage_sotozei5") = total_uriage_sotozei5
                    '合計(税率=8%)
                    editDt.Rows(j)("total_uriage8") = total_uriage8
                    editDt.Rows(j)("total_sotozei8") = total_sotozei8
                    editDt.Rows(j)("total_uriage_sotozei8") = total_uriage_sotozei8
                    '総合計
                    editDt.Rows(j)("total_uriage_all") = total_uriage_all
                    editDt.Rows(j)("total_sotozei_all") = total_sotozei_all
                    editDt.Rows(j)("total_uriage_sotozei_all") = total_uriage_sotozei_all
                    '===============↑2014/02/17 407662_消費税増税対応_Earth 車龍 修正 終了↑===========================

                    '最終ページ　合計を表示
                    If (intPage1MaxRow + i * intPage2MaxRow + RowsInThisPage) >= seikyusyoDataTable.Rows.Count Then
                        editDt.Rows(j)("total_cover_flg") = ""
                    Else
                        editDt.Rows(j)("total_cover_flg") = "　"
                    End If

                Next

                Dim ii As Integer
                For ii = 0 To editDt.Rows.Count - 1

                    If editDt.Rows(ii).Item("uri_gaku").Equals(DBNull.Value) Then
                    Else
                        editDt.Rows(ii).Item("uri_gaku") = _
                        CDec(editDt.Rows(ii).Item("uri_gaku")).ToString("###,###")
                    End If

                    If editDt.Rows(ii).Item("sotozei_gaku").Equals(DBNull.Value) Then
                    Else
                        editDt.Rows(ii).Item("sotozei_gaku") = _
                        CDec(editDt.Rows(ii).Item("sotozei_gaku")).ToString("###,###")
                    End If

                    If editDt.Rows(ii).Item("uriage_sotosei").Equals(DBNull.Value) Then
                    Else
                        editDt.Rows(ii).Item("uriage_sotosei") = _
                       CDec(editDt.Rows(ii).Item("uriage_sotosei")).ToString("###,###")
                    End If

                    '===============↓2014/02/17 407662_消費税増税対応_Earth 車龍 追加 終了↓===========================
                    If editDt.Rows(ii).Item("zeiritu").ToString.Equals("0%") Then
                        editDt.Rows(ii).Item("zeiritu") = String.Empty
                        editDt.Rows(ii).Item("sotozei_gaku") = String.Empty
                    End If
                    '===============↑2014/02/17 407662_消費税増税対応_Earth 車龍 追加 終了↑===========================

                    If editDt.Rows(ii).Item("hinmei").ToString = "　　　　　　　　　小　　　　計" Then

                        editDt.Rows(ii).Item("uri_gaku") = _
                        editDt.Rows(ii).Item("uri_gaku").ToString.PadLeft(13, CChar(" ")).PadRight(26, CChar(" "))

                        editDt.Rows(ii).Item("sotozei_gaku") = _
                        editDt.Rows(ii).Item("sotozei_gaku").ToString.PadLeft(13, CChar(" ")).PadRight(26, CChar(" "))

                        '===============↓2014/02/17 407662_消費税増税対応_Earth 車龍 修正 開始↓===========================
                        'editDt.Rows(ii).Item("uriage_sotosei") = _
                        'editDt.Rows(ii).Item("uriage_sotosei").ToString.PadLeft(13, CChar(" ")).PadRight(26, CChar(" "))
                        editDt.Rows(ii).Item("uriage_sotosei") = _
                        editDt.Rows(ii).Item("uriage_sotosei").ToString.PadLeft(15, CChar(" ")).PadRight(28, CChar(" "))
                        '===============↑2014/02/17 407662_消費税増税対応_Earth 車龍 修正 終了↑===========================
                    End If

                Next

                '>=2ページ
                sb.Append(vbCrLf)
                '[Form] 部作成
                sb.Append(fcw.CreateFormSection("PAGE=SB_SEIKYUSYO_XK2"))

                '[FixedDataSection] 部作成
                sb.Append(fcw.CreateFixedDataSection(GetFixedDataSection_XK(editDt)))

                '[TableDataSection] 部作成
                sb.Append(fcw.CreateTableDataSection(GetTableDataSection_XK(editDt)))

            Next

        End If

        '<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>
        '<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>
        '<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>

        Return sb

    End Function

    ''' <summary>
    ''' GetFixedBukkenDataSectionのデータを取得
    ''' </summary>
    ''' <param name="data">データ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetBukkenFixedDataSection(ByVal data As DataTable) As String

        'EMAB障害対応情報の格納処理
        UnTrappedExceptionManager.AddMethodEntrance(MyClass.GetType.FullName & "." & MethodBase.GetCurrentMethod.Name)

        'データを取得
        Return fcw.GetFixedDataSection( _
                                                     "pages_total" & _
                                                     ",seikyuu_saki_cd_with_brc" & _
                                                     ",seikyuusyo_hak_date" & _
                                                     ",yuubin_no" & _
                                                     ",jyuusyo1" & _
                                                     ",jyuusyo2" & _
                                                     ",seikyuu_saki_mei" & _
                                                     ",seikyuu_saki_mei2" & _
                                                     ",tantousya_mei" & _
                                                     ",DisplayName" & _
                                                     ",bukken_no" & _
                                                     ",bukken_mei" & _
                                                     ",onkai_kaisyuu_yotei_date" & _
                                                     ",onkai_kaisyuu_yotei_date2" & _
                                                     ",onkai_kaisyuu_yotei_date3" & _
                                                     ",furikomisaki1" & _
                                                     ",furikomisaki2" & _
                                                     ",furikomisaki3" & _
                                                     ",furikomisaki4" & _
                                                     ",furikomisaki5" & _
                                                     ",total_uriage0" & _
                                                     ",total_sotozei0" & _
                                                     ",total_uriage_sotozei0" & _
                                                     ",total_uriage5" & _
                                                     ",total_sotozei5" & _
                                                     ",total_uriage_sotozei5" & _
                                                     ",total_uriage8" & _
                                                     ",total_sotozei8" & _
                                                     ",total_uriage_sotozei8", data)

    End Function

    ''' <summary>
    ''' GetBukkenTableDataSectionのデータを取得
    ''' </summary>
    ''' <param name="data">データ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetBukkenTableDataSection(ByVal data As DataTable) As String

        'EMAB障害対応情報の格納処理
        UnTrappedExceptionManager.AddMethodEntrance(MyClass.GetType.FullName & "." & MethodBase.GetCurrentMethod.Name)

        '共通CLASS
        Dim earthAction As New EarthAction

        'データを取得
        Return earthAction.JoinDataTable( _
                                                    data, _
                                                    APOST, _
                                                    "hinmei" & _
                                                    ",suu" & _
                                                    ",tanka" & _
                                                    ",uri_gaku" & _
                                                    ",sotozei_gaku" & _
                                                    ",uri_sotozei" & _
                                                    ",zeiritu" & _
                                                    ",font_size")

    End Function

    ''' <summary>
    ''' GetFixedDataSectionのデータを取得
    ''' </summary>
    ''' <param name="data">データ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetFixedDataSection_ZK(ByVal data As DataTable) As String

        'EMAB障害対応情報の格納処理
        UnTrappedExceptionManager.AddMethodEntrance(MyClass.GetType.FullName & "." & MethodBase.GetCurrentMethod.Name)

        'データを取得
        Return fcw.GetFixedDataSection( _
                                        "seikyuu_saki_cd_with_brc" & _
                                        ",pages_total" & _
                                        ",seikyuusyo_hak_date" & _
                                        ",yuubin_no" & _
                                        ",jyuusyo1" & _
                                        ",jyuusyo2" & _
                                        ",seikyuu_saki_mei" & _
                                        ",seikyuu_saki_mei2" & _
                                        ",tantousya_mei" & _
                                        ",konkai_goseikyuu_gaku" & _
                                        ",konkai_kaisyuu_yotei_date" & _
                                        ",konkai_kaisyuu_yotei_date2" & _
                                        ",konkai_kaisyuu_yotei_date3" & _
                                        ",DisplayName" & _
                                        ",furikomisaki1" & _
                                        ",furikomisaki2" & _
                                        ",furikomisaki3" & _
                                        ",furikomisaki4" & _
                                        ",furikomisaki5" & _
                                        ",zenkai_goseikyuu_gaku" & _
                                        ",gonyuukin_gaku" & _
                                        ",sousai_gaku" & _
                                        ",tyousei_gaku" & _
                                        ",konkai_goseikyuu_gaku2" & _
                                        ",konkai_kurikosi_gaku" & _
                                        ",sum_uriage0" & _
                                        ",sum_sotozei0" & _
                                        ",sum_uriage_sotozei0" & _
                                        ",sum_uriage5" & _
                                        ",sum_sotozei5" & _
                                        ",sum_uriage_sotozei5" & _
                                        ",sum_uriage8" & _
                                        ",sum_sotozei8" & _
                                        ",sum_uriage_sotozei8" & _
                                        ",total_uriage0" & _
                                        ",total_sotozei0" & _
                                        ",total_uriage_sotozei0" & _
                                        ",total_uriage5" & _
                                        ",total_sotozei5" & _
                                        ",total_uriage_sotozei5" & _
                                        ",total_uriage8" & _
                                        ",total_sotozei8" & _
                                        ",total_uriage_sotozei8" & _
                                        ",total_uriage_all" & _
                                        ",total_sotozei_all" & _
                                        ",total_uriage_sotozei_all" & _
                                        ",total_cover_flg", data)


    End Function

    ''' <summary>
    ''' GetFixedDataSectionのデータを取得
    ''' </summary>
    ''' <param name="data">データ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetFixedDataSection_XK(ByVal data As DataTable) As String

        'EMAB障害対応情報の格納処理
        UnTrappedExceptionManager.AddMethodEntrance(MyClass.GetType.FullName & "." & MethodBase.GetCurrentMethod.Name)

        'データを取得
        Return fcw.GetFixedDataSection( _
                                        "seikyuu_saki_cd_with_brc" & _
                                        ",pages_total" & _
                                        ",seikyuusyo_hak_date" & _
                                        ",yuubin_no" & _
                                        ",jyuusyo1" & _
                                        ",jyuusyo2" & _
                                        ",seikyuu_saki_mei" & _
                                        ",seikyuu_saki_mei2" & _
                                        ",tantousya_mei" & _
                                        ",konkai_goseikyuu_gaku" & _
                                        ",konkai_kaisyuu_yotei_date" & _
                                        ",konkai_kaisyuu_yotei_date2" & _
                                        ",konkai_kaisyuu_yotei_date3" & _
                                        ",DisplayName" & _
                                        ",furikomisaki1" & _
                                        ",furikomisaki2" & _
                                        ",furikomisaki3" & _
                                        ",furikomisaki4" & _
                                        ",furikomisaki5" & _
                                        ",zenkai_goseikyuu_gaku" & _
                                        ",gonyuukin_gaku" & _
                                        ",sousai_gaku" & _
                                        ",tyousei_gaku" & _
                                        ",konkai_goseikyuu_gaku2" & _
                                        ",konkai_kurikosi_gaku" & _
                                        ",sum_uriage" & _
                                        ",sum_sotozei" & _
                                        ",sum_uriage_sotozei" & _
                                        ",total_uriage0" & _
                                        ",total_sotozei0" & _
                                        ",total_uriage_sotozei0" & _
                                        ",total_uriage5" & _
                                        ",total_sotozei5" & _
                                        ",total_uriage_sotozei5" & _
                                        ",total_uriage8" & _
                                        ",total_sotozei8" & _
                                        ",total_uriage_sotozei8" & _
                                        ",total_uriage_all" & _
                                        ",total_sotozei_all" & _
                                        ",total_uriage_sotozei_all" & _
                                        ",TM1" & _
                                        ",TM1_1" & _
                                        ",TM1_2" & _
                                        ",TM1_3" & _
                                        ",TM1_4" & _
                                        ",TM1_5" & _
                                        ",TM1_6" & _
                                        ",TM1_7" & _
                                        ",TM1_8" & _
                                        ",TM1_9" & _
                                        ",TM1_10" & _
                                        ",TM1_11" & _
                                        ",TM1_12" & _
                                        ",TM1_13" & _
                                        ",TM1_14" & _
                                        ",TM1_15" & _
                                        ",TM1_16" & _
                                        ",TM1_17" & _
                                        ",TM1_18" & _
                                        ",TM1_19" & _
                                        ",TM1_20" & _
                                        ",TM1_21" & _
                                        ",TM1_22" & _
                                        ",TM1_23" & _
                                        ",TM1_24" & _
                                        ",TM1_25" & _
                                        ",TM2_1" & _
                                        ",TM2_2" & _
                                        ",TM2_3" & _
                                        ",TM2_4" & _
                                        ",TM2_5" & _
                                        ",TM2_6" & _
                                        ",TM2_7" & _
                                        ",TM2_8" & _
                                        ",TM2_9" & _
                                        ",TM2_10" & _
                                        ",TM2_11" & _
                                        ",TM2_12" & _
                                        ",TM2_13" & _
                                        ",TM2_14" & _
                                        ",TM2_15" & _
                                        ",TM2_16" & _
                                        ",TM2_17" & _
                                        ",TM2_18" & _
                                        ",TM2_19" & _
                                        ",TM2_20" & _
                                        ",TM2_21" & _
                                        ",TM2_22" & _
                                        ",TM2_23" & _
                                        ",TM2_24" & _
                                        ",TM2_25" & _
                                        ",TM2_0" & _
                                        ",total_cover_flg", data)


    End Function


    ''' <summary>
    ''' GetTableDataSectionのデータを取得
    ''' </summary>
    ''' <param name="data">データ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetTableDataSection_ZK(ByVal data As DataTable) As String

        'EMAB障害対応情報の格納処理
        UnTrappedExceptionManager.AddMethodEntrance(MyClass.GetType.FullName & "." & MethodBase.GetCurrentMethod.Name)

        '共通CLASS
        Dim earthAction As New EarthAction

        'データを取得
        Return earthAction.JoinDataTable( _
                                                    data, _
                                                    APOST, _
                                                    "denpyou_uri_date" & _
                                                    ",kbnwihtbangou" & _
                                                    ",hinmei" & _
                                                    ",bukenmei" & _
                                                    ",suu" & _
                                                    ",tanka" & _
                                                    ",uri_gaku" & _
                                                    ",sotozei_gaku" & _
                                                    ",uriage_sotosei" & _
                                                    ",zeiritu" & _
                                                    ",font_size")

    End Function

    ''' <summary>
    ''' GetTableDataSectionのデータを取得
    ''' </summary>
    ''' <param name="data">データ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetTableDataSection_XK(ByVal data As DataTable) As String

        'EMAB障害対応情報の格納処理
        UnTrappedExceptionManager.AddMethodEntrance(MyClass.GetType.FullName & "." & MethodBase.GetCurrentMethod.Name)

        '共通CLASS
        Dim earthAction As New EarthAction

        'データを取得
        Return earthAction.JoinDataTable( _
                                                    data, _
                                                    APOST, _
                                                    "denpyou_uri_date" & _
                                                    ",kbnwihtbangou" & _
                                                    ",hinmei" & _
                                                    ",bukenmei" & _
                                                    ",suu" & _
                                                    ",tanka" & _
                                                    ",uri_gaku" & _
                                                    ",sotozei_gaku" & _
                                                    ",uriage_sotosei" & _
                                                    ",zeiritu" & _
                                                    ",font_size")


    End Function

    ''' <summary>空白文字の削除処理</summary>
    Private Function TrimNull(ByVal objStr As Object) As String
        If IsDBNull(objStr) Then
            TrimNull = ""
        Else
            TrimNull = objStr.ToString.Trim
        End If
    End Function

    ''' <summary>
    ''' ->YYYY年MM月DD日と->YY年MM月DD日
    ''' </summary>
    ''' <param name="nowDate">日付</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function NengoChange(ByVal nowDate As Date, Optional ByVal strFlg As Boolean = False) As String
        If strFlg = False Then
            Return nowDate.Year() & "年" & Right("0" & nowDate.Month, 2) & "月" & Right("0" & nowDate.Day, 2) & "日"
        Else
            Return Right(nowDate.Year(), 2) & "/" & Right("0" & nowDate.Month, 2) & "/" & Right("0" & nowDate.Day, 2)
        End If
    End Function





    ''' <summary>
    '''　前画面データ格納
    ''' </summary>
    ''' <param name="strId"></param>
    ''' <remarks></remarks>
    ''' <history>2010/09/19 付龍(大連情報システム部)　新規作成</history>
    Protected Sub GetScriptValue(ByVal strId As String)

        Dim csScript As New StringBuilder
        With csScript
            .Append("<script language='javascript' type='text/javascript'>  " & vbCrLf)
            .Append("   document.getElementById('" & Me.hidSelName.ClientID & "').value='" & Split(strId, ",")(1) & "';" & vbCrLf)
            .Append("   document.getElementById('" & Me.hidDisableDiv.ClientID & "').value='" & Split(strId, ",")(2) & "';" & vbCrLf)
            '.Append("   form1.submit();" & vbCrLf)
            .Append("</script>" & vbCrLf)
        End With
        ClientScript.RegisterStartupScript(Me.GetType, "", csScript.ToString)

    End Sub

    Protected Sub MakeJavaScript()

        Dim csType As Type = Page.GetType()
        Dim csName As String = "setScript"
        Dim csScript As New StringBuilder
        With csScript
            .Append("<script language='javascript' type='text/javascript'>  " & vbCrLf)
            .Append("function funOutError(strERR)" & vbCrLf)
            .Append("{" & vbCrLf)
            .Append("if (strERR==''){" & vbCrLf)
            .Append("   alert('指定した条件でデータはありません。');}else{" & vbCrLf)
            .Append("   alert(strERR);" & vbCrLf)
            .Append("}" & vbCrLf)
            .Append("}" & vbCrLf)
            .Append("</script>")
        End With

        ClientScript.RegisterClientScriptBlock(csType, csName, csScript.ToString)
    End Sub
    ''' <summary>
    ''' 帳票商品名Fontsize設定
    ''' </summary>
    ''' <param name="txt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFontSize(ByVal txt As String) As String

        GetFontSize = 9

        If Len(txt) > 40 / 2 Then
            GetFontSize = 8
        End If

        If Len(txt) > 48 / 2 Then
            GetFontSize = 7.5
        End If

        If Len(txt) > 52 / 2 Then
            GetFontSize = 7
        End If

        If Len(txt) > 56 / 2 Then
            GetFontSize = 6
        End If

    End Function

    '1	：	今まで通り（今回追加の項目は出さない=特に変更なし）		
    '2	：	要望①の追加 		※商品名の後ろに依頼担当者名を追加
    '3	：	要望③の追加		※物件名の後ろに（契約No）を追加
    '4	：	要望①＋③の追加		※商品名の後ろに依頼担当者名を追加、物件名の後ろに（契約No）を追加
    Public Function GetSyouhinMeiByFlg(ByVal seikyusyoDataTable As DataTable, ByVal i As Integer) As String
        If seikyusyoDataTable.Rows(i).Item("hinmei") IsNot DBNull.Value AndAlso seikyusyoDataTable.Rows(i).Item("hinmei") = "　　　　　　　　　小　　　　計" Then Return ""
        If seikyusyoDataTable.Rows(i).Item("koumoku_hyouji_flg") = "2" OrElse seikyusyoDataTable.Rows(i).Item("koumoku_hyouji_flg") = "4" Then
            'Return "　" & seikyusyoDataTable.Rows(i).Item("irai_tantousya_mei") & " 様"
            If seikyusyoDataTable.Rows(i).Item("irai_tantousya_mei").ToString.Trim = "" OrElse TrimNull(seikyusyoDataTable.Rows(i).Item("bukken_no")) = "" Then
                Return ""
            Else
                Return "　" & seikyusyoDataTable.Rows(i).Item("irai_tantousya_mei") & " 様"
            End If
        Else
            Return ""
        End If
    End Function
    Public Function GetBukenMeiByFlg(ByVal seikyusyoDataTable As DataTable, ByVal i As Integer) As String
        If seikyusyoDataTable.Rows(i).Item("hinmei") IsNot DBNull.Value AndAlso seikyusyoDataTable.Rows(i).Item("hinmei") = "　　　　　　　　　小　　　　計" Then Return ""
        If seikyusyoDataTable.Rows(i).Item("koumoku_hyouji_flg") = "3" OrElse seikyusyoDataTable.Rows(i).Item("koumoku_hyouji_flg") = "4" Then
            If seikyusyoDataTable.Rows(i).Item("keiyaku_no").ToString.Trim = "" Then
                Return " "
            Else
                Return "　(" & seikyusyoDataTable.Rows(i).Item("keiyaku_no") & ")"
            End If

        Else
            Return ""
        End If
    End Function


    Public Function IsNullTo0(ByVal v As Object) As Decimal
        If v Is DBNull.Value Then
            Return 0
        Else
            Return CDec(v)
        End If

    End Function

End Class
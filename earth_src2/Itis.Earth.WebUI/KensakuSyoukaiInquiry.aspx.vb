Imports Itis.Earth.BizLogic
Imports Itis.Earth.DataAccess

Partial Public Class KensakuSyoukaiInquiry
    Inherits System.Web.UI.Page

    ''' <summary>加盟店情報を検索照会する</summary>
    ''' <remarks>加盟店検索照会機能を提供する</remarks>
    ''' <history>
    ''' <para>2009/07/15　付龍(大連情報システム部)　新規作成</para>
    ''' </history>
    Private inquiryLogic As New KensakuSyoukaiInquiryLogic
    Private commonCheck As New CommonCheck
    Protected scrollHeight As Integer = 0
    Private KihonJyouhouBL As New KakakuseikyuJyouhouLogic()
    Private earthAction As New EarthAction

    ''' <summary>ページロッド</summary>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '権限チェック
        Dim user_info As New LoginUserInfo
        Dim ninsyou As New Ninsyou()
        Dim jBn As New Jiban '地盤画面共通クラス
        ' ユーザー基本認証
        jBn.userAuth(user_info)
        If ninsyou.GetUserID() = String.Empty Then
            Context.Items("strFailureMsg") = Messages.Instance.MSG2024E
            Server.Transfer("CommonErr.aspx")
        End If
        If user_info Is Nothing Then
            Context.Items("strFailureMsg") = Messages.Instance.MSG2020E
            Server.Transfer("CommonErr.aspx")
        End If

        'Javascript作成
        MakeJavascript()

        If Not IsPostBack Then
            '参照履歴管理テーブルを登録する。
            commonCheck.SetURL(Me, ninsyou.GetUserID())
        Else
            Me.Common_drop1.SelectedValue = Me.Common_drop1.SelectedValue
            Me.Common_drop2.SelectedValue = Me.Common_drop2.SelectedValue
            Me.Common_drop3.SelectedValue = Me.Common_drop3.SelectedValue
            Me.Common_drop.SelectedText = Me.Common_drop.SelectedText
            If Me.tbxKeiretuCd.Text <> String.Empty Then
                Me.tbxKeiretuMei.Text = Me.hidKeiretuMei.Value
            Else
                Me.tbxKeiretuMei.Text = String.Empty
            End If
            CloseCover()
        End If

        Me.chkKubunAll.Attributes.Add("onClick", "fncSetKubunVal();")
        Me.tbxTourokuNengetuhi1.Attributes.Add("onBlur", "fncCheckNengetu(this);")
        Me.tbxTourokuNengetuhi2.Attributes.Add("onBlur", "fncCheckNengetu(this);")
        Me.btnKensaku.Attributes.Add("onClick", "if(fncNyuuryokuCheck('1')==true){fncSetKeiretuMei();fncShowModal();}else{return false}")
        Me.btnKihonJyouhouCsv.Attributes.Add("onClick", " return fncNyuuryokuCheck('2');")
        Me.btnJyusyoJyouhouCsv.Attributes.Add("onClick", " return fncNyuuryokuCheck('3');")
        Me.btnKameitenInfoIttukatuCsv.Attributes.Add("onClick", " return fncNyuuryokuCheck('4');")

        Me.tbxKameitenCd1.Attributes.Add("onBlur", "fncToUpper(this);")
        Me.tbxKameitenCd2.Attributes.Add("onBlur", "fncToUpper(this);")
        Me.tbxKameitenKana.Attributes.Add("onBlur", "fncTokomozi(this);")
        'Me.tbxEigyousyoCd1.Attributes.Add("onBlur", "fncToUpper(this);")
        'Me.tbxEigyousyoCd2.Attributes.Add("onBlur", "fncToUpper(this);")
        'Me.tbxKeiretuCd.Attributes.Add("onBlur", "fncToUpper(this);")

    End Sub

    ''' <summary>検索実行ボタンをクリック時</summary>
    Private Sub btnKensaku_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKensaku.Click

        '入力チェック
        Dim strObjId As String = String.Empty
        Dim strErrMessage As String = CheckInput(strObjId)
        If strErrMessage <> String.Empty Then
            ShowMessage(strErrMessage, strObjId)
            Exit Sub
        End If

        '検索データを取得する
        Dim dtParam As Data.DataTable = SetGamenData()
        Dim dtKameitenInfo As Data.DataTable = inquiryLogic.GetKameitenInfo(dtParam)
        Me.grdItiranLeft.DataSource = dtKameitenInfo
        Me.grdItiranLeft.DataBind()
        Me.grdItiranRight.DataSource = dtKameitenInfo
        Me.grdItiranRight.DataBind()

        Dim commonSearchLogic As New CommonSearchLogic
        '営業所コードFromを設置。
        If Me.tbxEigyousyoCd1.Text <> String.Empty Then
            Dim dtEigyousyoTable As Data.DataTable = commonSearchLogic.GetCommonInfo(dtParam.Rows(0).Item("eigyousyo_cd_from"), _
                                                                                     "m_eigyousyo")
            If dtEigyousyoTable.Rows.Count > 0 Then
                Me.tbxEigyousyoCd1.Text = dtEigyousyoTable.Rows(0).Item("cd").ToString
            End If
        End If
        '営業所コードToを設置。
        If Me.tbxEigyousyoCd2.Text <> String.Empty Then
            Dim dtEigyousyoTable As Data.DataTable = commonSearchLogic.GetCommonInfo(dtParam.Rows(0).Item("eigyousyo_cd_to"), _
                                                                                     "m_eigyousyo")
            If dtEigyousyoTable.Rows.Count > 0 Then
                Me.tbxEigyousyoCd2.Text = dtEigyousyoTable.Rows(0).Item("cd").ToString
            End If
        End If
        '系列コード、系列名を設置。
        If Me.tbxKeiretuCd.Text <> String.Empty Then
            Dim dtKeiretuTable As Data.DataTable = commonSearchLogic.GetCommonInfo(dtParam.Rows(0).Item("keiretu_cd"), _
                                                                                     "m_keiretu", _
                                                                                    dtParam.Rows(0).Item("kbn"))
            If dtKeiretuTable.Rows.Count > 0 Then
                Me.tbxKeiretuCd.Text = dtKeiretuTable.Rows(0).Item("cd").ToString
                Me.tbxKeiretuMei.Text = dtKeiretuTable.Rows(0).Item("mei").ToString
            Else
                Me.tbxKeiretuMei.Text = String.Empty
            End If
        Else
            Me.tbxKeiretuMei.Text = String.Empty
        End If

        '検索結果を設定する
        Dim intCount As Integer = inquiryLogic.GetKameitenInfoCount(dtParam)
        If Me.ddlSearchCount.SelectedValue = "max" Then
            Me.lblCount.Text = intCount
            Me.lblCount.ForeColor = Drawing.Color.Black
            scrollHeight = intCount * 22 + 1
        Else
            If intCount > Me.ddlSearchCount.SelectedValue Then
                Me.lblCount.Text = Me.ddlSearchCount.SelectedValue & " / " & intCount
                Me.lblCount.ForeColor = Drawing.Color.Red
                scrollHeight = 100 * 22 + 1
            Else
                Me.lblCount.Text = dtKameitenInfo.Rows.Count
                Me.lblCount.ForeColor = Drawing.Color.Black
                scrollHeight = dtKameitenInfo.Rows.Count * 22 + 1
            End If
        End If

        '検索データがない時を設定
        If intCount = 0 Then
            btnSortTorikesi1.Visible = False
            btnSortTorikesi2.Visible = False
            btnSortKubun1.Visible = False
            btnSortKubun2.Visible = False
            btnSortKameitenCd1.Visible = False
            btnSortKameitenCd2.Visible = False
            btnSortKameitenMei1.Visible = False
            btnSortKameitenMei2.Visible = False
            btnSortKameitenKana1.Visible = False
            btnSortKameitenKana2.Visible = False
            btnSortKameitenMei21.Visible = False
            btnSortKameitenMei22.Visible = False
            btnSortJyuusyo1.Visible = False
            btnSortJyuusyo2.Visible = False
            btnSortTourofukenMei1.Visible = False
            btnSortTourofukenMei2.Visible = False
            btnSortKeiretu1.Visible = False
            btnSortKeiretu2.Visible = False
            btnSortEigyousyoCd1.Visible = False
            btnSortEigyousyoCd2.Visible = False
            btnSortBuilderNo1.Visible = False
            btnSortBuilderNo2.Visible = False
            btnSortDaihyousyaMei1.Visible = False
            btnSortDaihyousyaMei2.Visible = False
            btnSortTelNo1.Visible = False
            btnSortTelNo2.Visible = False
            ShowMessage(Messages.Instance.MSG020E, String.Empty)
            Exit Sub
        Else
            btnSortTorikesi1.Visible = True
            btnSortTorikesi2.Visible = True
            btnSortKubun1.Visible = True
            btnSortKubun2.Visible = True
            btnSortKameitenCd1.Visible = True
            btnSortKameitenCd2.Visible = True
            btnSortKameitenMei1.Visible = True
            btnSortKameitenMei2.Visible = True
            btnSortKameitenKana1.Visible = True
            btnSortKameitenKana2.Visible = True
            btnSortKameitenMei21.Visible = True
            btnSortKameitenMei22.Visible = True
            btnSortJyuusyo1.Visible = True
            btnSortJyuusyo2.Visible = True
            btnSortTourofukenMei1.Visible = True
            btnSortTourofukenMei2.Visible = True
            btnSortKeiretu1.Visible = True
            btnSortKeiretu2.Visible = True
            btnSortEigyousyoCd1.Visible = True
            btnSortEigyousyoCd2.Visible = True
            btnSortBuilderNo1.Visible = True
            btnSortBuilderNo2.Visible = True
            btnSortDaihyousyaMei1.Visible = True
            btnSortDaihyousyaMei2.Visible = True
            btnSortTelNo1.Visible = True
            btnSortTelNo2.Visible = True
            btnSortTorikesi1.ForeColor = Drawing.Color.SkyBlue
            btnSortTorikesi2.ForeColor = Drawing.Color.SkyBlue
            btnSortKubun1.ForeColor = Drawing.Color.SkyBlue
            btnSortKubun2.ForeColor = Drawing.Color.SkyBlue
            btnSortKameitenCd1.ForeColor = Drawing.Color.IndianRed
            btnSortKameitenCd2.ForeColor = Drawing.Color.SkyBlue
            btnSortKameitenMei1.ForeColor = Drawing.Color.SkyBlue
            btnSortKameitenMei2.ForeColor = Drawing.Color.SkyBlue
            btnSortKameitenKana1.ForeColor = Drawing.Color.SkyBlue
            btnSortKameitenKana2.ForeColor = Drawing.Color.SkyBlue
            btnSortKameitenMei21.ForeColor = Drawing.Color.SkyBlue
            btnSortKameitenMei22.ForeColor = Drawing.Color.SkyBlue
            btnSortJyuusyo1.ForeColor = Drawing.Color.SkyBlue
            btnSortJyuusyo2.ForeColor = Drawing.Color.SkyBlue
            btnSortTourofukenMei1.ForeColor = Drawing.Color.SkyBlue
            btnSortTourofukenMei2.ForeColor = Drawing.Color.SkyBlue
            btnSortKeiretu1.ForeColor = Drawing.Color.SkyBlue
            btnSortKeiretu2.ForeColor = Drawing.Color.SkyBlue
            btnSortEigyousyoCd1.ForeColor = Drawing.Color.SkyBlue
            btnSortEigyousyoCd2.ForeColor = Drawing.Color.SkyBlue
            btnSortBuilderNo1.ForeColor = Drawing.Color.SkyBlue
            btnSortBuilderNo2.ForeColor = Drawing.Color.SkyBlue
            btnSortDaihyousyaMei1.ForeColor = Drawing.Color.SkyBlue
            btnSortDaihyousyaMei2.ForeColor = Drawing.Color.SkyBlue
            btnSortTelNo1.ForeColor = Drawing.Color.SkyBlue
            btnSortTelNo2.ForeColor = Drawing.Color.SkyBlue

            ViewState("dtKameitenInfo") = dtKameitenInfo
            ViewState("scrollHeight") = scrollHeight
        End If

    End Sub

    ''' <summary>画面に入力した値をデータテーブルに設定する</summary>
    ''' <returns>画面データを検索するパラメータデータテーブル</returns>
    Private Function SetGamenData() As KensakuSyoukaiInquiryDataSet.Param_KameitenInfoDataTable
        Dim dtParam As New KensakuSyoukaiInquiryDataSet.Param_KameitenInfoDataTable
        Dim row As KensakuSyoukaiInquiryDataSet.Param_KameitenInfoRow = dtParam.NewParam_KameitenInfoRow

        '区分を設定
        If Me.chkKubunAll.Checked Then
            row.kbn = String.Empty
        Else
            Dim strKbns As String = String.Empty
            If Me.Common_drop1.SelectedValue <> String.Empty Then
                strKbns = strKbns & Me.Common_drop1.SelectedValue & ","
            End If
            If Me.Common_drop2.SelectedValue <> String.Empty Then
                strKbns = strKbns & Me.Common_drop2.SelectedValue & ","
            End If
            If Me.Common_drop3.SelectedValue <> String.Empty Then
                strKbns = strKbns & Me.Common_drop3.SelectedValue & ","
            End If
            strKbns = strKbns.Substring(0, strKbns.Length - 1)
            row.kbn = strKbns
        End If

        '退会した加盟店を設定
        row.taikai = IIf(Me.chkTaikai.Checked, "1", String.Empty)

        '入力データを設定
        row.kameiten_mei = Me.tbxKameitenMei.Text
        If row.kameiten_mei.Replace("%", String.Empty) = String.Empty Then
            row.kameiten_mei = String.Empty
        End If
        row.kameiten_cd_from = Me.tbxKameitenCd1.Text
        row.kameiten_cd_to = Me.tbxKameitenCd2.Text
        row.kameiten_kana = Me.tbxKameitenKana.Text
        If row.kameiten_kana.Replace("%", String.Empty) = String.Empty Then
            row.kameiten_kana = String.Empty
        End If
        row.eigyousyo_cd_from = Me.tbxEigyousyoCd1.Text
        row.eigyousyo_cd_to = Me.tbxEigyousyoCd2.Text
        row.tel_no = Me.tbxDenwaBangou.Text.Replace("-", String.Empty)
        If row.tel_no.Replace("%", String.Empty) = String.Empty Then
            row.tel_no = String.Empty
        End If
        row.touroku_nengetu_from = Me.tbxTourokuNengetuhi1.Text
        row.touroku_nengetu_to = Me.tbxTourokuNengetuhi2.Text
        row.todouhuken_cd = Me.Common_drop.SelectedText
        row.keiretu_cd = Me.tbxKeiretuCd.Text

        '送付先選択を設定
        If Me.chkLitSoufusaki.SelectedIndex = -1 Then
            row.soufusaki_kbn = "1"
        Else
            row.jyuusyo_no = IIf(Me.chkLitSoufusaki.Items(0).Selected, "1", String.Empty)
            row.seikyuusyo_flg = IIf(Me.chkLitSoufusaki.Items(1).Selected, "1", String.Empty)
            row.hosyousyo_flg = IIf(Me.chkLitSoufusaki.Items(2).Selected, "1", String.Empty)
            row.kasi_hosyousyo_flg = IIf(Me.chkLitSoufusaki.Items(3).Selected, "1", String.Empty)
            row.teiki_kankou_flg = IIf(Me.chkLitSoufusaki.Items(4).Selected, "1", String.Empty)
            row.hkks_flg = IIf(Me.chkLitSoufusaki.Items(5).Selected, "1", String.Empty)
            row.koj_hkks_flg = IIf(Me.chkLitSoufusaki.Items(6).Selected, "1", String.Empty)
            row.kensa_hkks_flg = IIf(Me.chkLitSoufusaki.Items(7).Selected, "1", String.Empty)
        End If

        '検索上限件数を設定
        row.kensaku_count = Me.ddlSearchCount.SelectedValue

        dtParam.AddParam_KameitenInfoRow(row)

        Return dtParam
    End Function

    ''' <summary>基本情報CSV出力ボタンをクリック時</summary>
    Private Sub btnKihonJyouhouCsv_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKihonJyouhouCsv.Click

        '入力チェック
        Dim strObjId As String = String.Empty
        Dim strErrMessage As String = CheckInput(strObjId).ToString
        If strErrMessage <> String.Empty Then
            ShowMessage(strErrMessage, strObjId)
            Exit Sub
        End If

        '検索データを取得する
        Dim dtParam As Data.DataTable = SetGamenData()
        Dim dtKihonJyouhouCsvInfo As New Data.DataTable
        dtKihonJyouhouCsvInfo = inquiryLogic.GetKihonJyouhouCsvInfo(dtParam).Tables(0)
        Dim intCount As Integer = dtKihonJyouhouCsvInfo.Rows.Count
        If intCount = 0 Then
            ShowMessage(Messages.Instance.MSG020E, String.Empty)
            Exit Sub
        End If

        'CSVファイル名設定
        Dim strFileName As String = System.Configuration.ConfigurationManager.AppSettings("KameitenMasterCsv").ToString

        Response.Buffer = True
        Dim writer As New CsvWriter(Me.Response.OutputStream, Encoding.GetEncoding(932), ",", vbCrLf)

        'CSVファイルヘッダ設定
        writer.WriteLine(EarthConst.conKihonJyouhouCsvHeader)

        'CSVファイル内容設定
        For i As Integer = 0 To dtKihonJyouhouCsvInfo.Rows.Count - 1
            With dtKihonJyouhouCsvInfo.Rows(i)
                writer.WriteLine(.Item(0), .Item(1), .Item(2), .Item(3), .Item(4), .Item(5), .Item(6), .Item(7), .Item(8), .Item(9), _
                                 .Item(10), .Item(11), .Item(12), .Item(13), .Item(14), .Item(15), .Item(16), .Item(17), .Item(18), .Item(19), _
                                 .Item(20), .Item(21), .Item(22), .Item(23), .Item(24), .Item(25), .Item(26), .Item(27), .Item(28), .Item(29), _
                                 .Item(30), .Item(31), .Item(32), .Item(33), .Item(34), .Item(35), .Item(36), .Item(37), .Item(38), .Item(39), _
                                 .Item(40), .Item(41), .Item(42), .Item(43), .Item(44), .Item(45), .Item(46), .Item(47), .Item(48), .Item(49), _
                                 .Item(50), .Item(51), .Item(52), .Item(53), .Item(54), .Item(55), .Item(56), .Item(57), .Item(58), .Item(59), _
                                 .Item(60), .Item(61), .Item(62), .Item(63), .Item(64), .Item(65), .Item(66), .Item(67), .Item(68), .Item(69), _
                                 .Item(70), .Item(71), .Item(72), .Item(73), .Item(74), .Item(75), .Item(76), .Item(77), .Item(78), .Item(79), _
                                 .Item(80), .Item(81), .Item(82), .Item(83), .Item(84), .Item(85), .Item(86), .Item(87), .Item(88), .Item(89), _
                                 .Item(90), .Item(91), .Item(92), .Item(93), .Item(94), .Item(95), .Item(96), .Item(97), .Item(98), .Item(99), _
                                 .Item(100), .Item(101), .Item(102), .Item(103), .Item(104), .Item(105), .Item(106), .Item(107), .Item(108), .Item(109), _
                                 .Item(110), .Item(111), .Item(112), .Item(113), .Item(114), .Item(115), .Item(116), .Item(117), .Item(118), .Item(119), .Item(120))
            End With
        Next

        'CSVファイルダウンロード
        Response.Charset = "utf-8"
        Response.ContentType = "text/plain"
        Response.AddHeader("Content-Disposition", "attachment; filename=" & System.Web.HttpUtility.UrlEncode(strFileName))
        Response.End()
    End Sub

    ''' <summary>住所情報CSV出力ボタンをクリック時</summary>
    Private Sub btnJyusyoJyouhouCsv_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnJyusyoJyouhouCsv.Click

        '入力チェック
        Dim strObjId As String = String.Empty
        Dim strErrMessage As String = CheckInput(strObjId).ToString
        If strErrMessage <> String.Empty Then
            ShowMessage(strErrMessage, strObjId)
            Exit Sub
        End If

        '検索データを取得する
        Dim dtParam As Data.DataTable = SetGamenData()
        Dim dtJyusyoJyouhouCsvInfo As New Data.DataTable
        dtJyusyoJyouhouCsvInfo = inquiryLogic.GetJyusyoJyouhouCsvInfo(dtParam).Tables(0)

        Dim intCount As Integer = dtJyusyoJyouhouCsvInfo.Rows.Count
        If intCount = 0 Then
            ShowMessage(Messages.Instance.MSG020E, String.Empty)
            Exit Sub
        End If

        'CSVファイル名設定
        Dim strFileName As String = System.Configuration.ConfigurationManager.AppSettings("KameitenJyuusyoCsv").ToString

        Response.Buffer = True
        Dim writer As New CsvWriter(Me.Response.OutputStream, Encoding.GetEncoding(932), ",", vbCrLf)

        'CSVファイルヘッダ設定
        writer.WriteLine(EarthConst.conJyuusyoJyouhouCsvHeader)

        'CSVファイル内容設定
        For i As Integer = 0 To dtJyusyoJyouhouCsvInfo.Rows.Count - 1
            With dtJyusyoJyouhouCsvInfo.Rows(i)
                writer.WriteLine(.Item(0), .Item(1), .Item(2), .Item(3), .Item(4), .Item(5), .Item(6), .Item(7), .Item(8), .Item(9), _
                                 .Item(10), .Item(11), .Item(12), .Item(13), .Item(14), .Item(15), .Item(16), .Item(17), .Item(18), .Item(19), _
                                 .Item(20), .Item(21), .Item(22), .Item(23), .Item(24), .Item(25), .Item(26), .Item(27), .Item(28), .Item(29), _
                                 .Item(30), .Item(31), .Item(32), .Item(33), .Item(34), .Item(35), .Item(36), .Item(37), .Item(38), .Item(39), _
                                 .Item(40), .Item(41), .Item(42), .Item(43), .Item(44), .Item(45), .Item(46), .Item(47), .Item(48), .Item(49), _
                                 .Item(50), .Item(51), .Item(52), .Item(53), .Item(54), .Item(55), .Item(56), .Item(57), .Item(58), .Item(59), _
                                 .Item(60), .Item(61), .Item(62), .Item(63), .Item(64), .Item(65), .Item(66), .Item(67), .Item(68), .Item(69), _
                                 .Item(70), .Item(71), .Item(72), .Item(73), .Item(74), .Item(75), .Item(76), .Item(77), .Item(78), .Item(79), _
                                 .Item(80), .Item(81), .Item(82), .Item(83), .Item(84), .Item(85), .Item(86))
            End With
        Next

        'CSVファイルダウンロード
        Response.Charset = "shift-jis"
        Response.ContentType = "text/plain"
        Response.AddHeader("Content-Disposition", "attachment; filename=" & System.Web.HttpUtility.UrlEncode(strFileName))
        Response.End()
    End Sub

    ''' <summary>一括取込用情報CSV出力ボタンをクリック時</summary>
    Private Sub btnKameitenInfoIttukatuCsv_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKameitenInfoIttukatuCsv.Click

        '入力チェック
        Dim strObjId As String = String.Empty
        Dim strErrMessage As String = CheckInput(strObjId).ToString
        If strErrMessage <> String.Empty Then
            ShowMessage(strErrMessage, strObjId)
            Exit Sub
        End If

        '検索データを取得する
        Dim dtParam As Data.DataTable = SetGamenData()
        Dim dtKameitenJyuusyoCsv As New Data.DataTable
        dtKameitenJyuusyoCsv = inquiryLogic.GetKameitenJyuusyoCsvInfo(dtParam)

        Dim intCount As Integer = dtKameitenJyuusyoCsv.Rows.Count
        If intCount = 0 Then
            ShowMessage(Messages.Instance.MSG020E, String.Empty)
            Exit Sub
        End If

        'CSVファイル名設定
        Dim strFileName As String = System.Configuration.ConfigurationManager.AppSettings("KameitenInfoIttukatuCsv").ToString

        Response.Buffer = True
        Dim writer As New CsvWriter(Me.Response.OutputStream, Encoding.GetEncoding(932), ",", vbCrLf)

        'CSVファイルヘッダ設定
        writer.WriteLine(EarthConst.conKameitenInfoIttukatuCsvHeader)

        'CSVファイル内容設定
        For Each row As Data.DataRow In dtKameitenJyuusyoCsv.Rows
            Dim fuho_syoumeisyo_kaisi_nengetu As Object = row.Item("fuho_syoumeisyo_kaisi_nengetu")
            Dim nyuukin_kakunin_oboegaki As Object = row.Item("nyuukin_kakunin_oboegaki")
            If Not fuho_syoumeisyo_kaisi_nengetu Is DBNull.Value Then
                fuho_syoumeisyo_kaisi_nengetu = CStr(fuho_syoumeisyo_kaisi_nengetu)
            End If
            If Not nyuukin_kakunin_oboegaki Is DBNull.Value Then
                nyuukin_kakunin_oboegaki = CStr(nyuukin_kakunin_oboegaki)
            End If
            'row.Item("edi_jouhou_sakusei_dat"),
            writer.WriteLine(row.Item("edi_jouhou_sakusei_dat"), row.Item("kbn"), row.Item("kameiten_cd"), row.Item("torikesi"), row.Item("hattyuu_teisi_flg"), _
            row.Item("kameiten_mei1"), row.Item("tenmei_kana1"), row.Item("kameiten_mei2"), row.Item("tenmei_kana2"), _
            row.Item("builder_no"), row.Item("builder_mei"), row.Item("keiretu_cd"), row.Item("keiretu_mei"), _
            row.Item("eigyousyo_cd"), row.Item("eigyousyo_mei"), row.Item("kameiten_seisiki_mei"), row.Item("kameiten_seisiki_mei_kana"), _
            row.Item("todouhuken_cd"), row.Item("todouhuken_mei"), row.Item("nenkan_tousuu"), row.Item("fuho_syoumeisyo_flg"), fuho_syoumeisyo_kaisi_nengetu, _
            row.Item("eigyou_tantousya_mei"), row.Item("eigyou_tantousya_simei"), row.Item("kyuu_eigyou_tantousya_mei"), _
            row.Item("kyuu_eigyou_tantousya_simei"), row.Item("koj_uri_syubetsu"), row.Item("koj_uri_syubetsu_mei"), row.Item("jiosaki_flg"), row.Item("kaiyaku_haraimodosi_kkk"), row.Item("syouhin_cd1"), _
            row.Item("syouhin_mei1"), row.Item("syouhin_cd2"), row.Item("syouhin_mei2"), row.Item("syouhin_cd3"), _
            row.Item("syouhin_mei3"), row.Item("tys_seikyuu_saki_kbn"), row.Item("tys_seikyuu_saki_cd"), _
            row.Item("tys_seikyuu_saki_brc"), row.Item("tys_seikyuu_saki_mei"), row.Item("koj_seikyuu_saki_kbn"), _
            row.Item("koj_seikyuu_saki_cd"), row.Item("koj_seikyuu_saki_brc"), row.Item("koj_seikyuu_saki_mei"), _
            row.Item("hansokuhin_seikyuu_saki_kbn"), row.Item("hansokuhin_seikyuu_saki_cd"), row.Item("hansokuhin_seikyuu_saki_brc"), _
            row.Item("hansokuhin_seikyuu_saki_mei"), row.Item("tatemono_seikyuu_saki_kbn"), row.Item("tatemono_seikyuu_saki_cd"), _
            row.Item("tatemono_seikyuu_saki_brc"), row.Item("tatemono_seikyuu_saki_mei"), row.Item("seikyuu_saki_kbn5"), _
            row.Item("seikyuu_saki_cd5"), row.Item("seikyuu_saki_brc5"), row.Item("seikyuu_saki5_mei"), row.Item("seikyuu_saki_kbn6"), _
            row.Item("seikyuu_saki_cd6"), row.Item("seikyuu_saki_brc6"), row.Item("seikyuu_saki6_mei"), row.Item("seikyuu_saki_kbn7"), _
            row.Item("seikyuu_saki_cd7"), row.Item("seikyuu_saki_brc7"), row.Item("seikyuu_saki7_mei"), row.Item("hosyou_kikan"), row.Item("hosyousyo_hak_umu"), row.Item("nyuukin_kakunin_jyouken"), _
            nyuukin_kakunin_oboegaki, row.Item("tys_mitsyo_flg"), row.Item("hattyuusyo_flg"), row.Item("yuubin_no"), _
            row.Item("jyuusyo1"), row.Item("jyuusyo2"), row.Item("jyuusyo_cd"), row.Item("jyuusyo_mei"), row.Item("busyo_mei"), _
            row.Item("daihyousya_mei"), row.Item("tel_no"), row.Item("fax_no"), row.Item("mail_address"), _
            row.Item("bikou1"), row.Item("bikou2"), _
            row.Item("add_date"), row.Item("seikyuu_umu"), row.Item("syouhin_cd"), row.Item("syouhin_mei"), row.Item("uri_gaku"), row.Item("koumuten_seikyuu_gaku"), row.Item("seikyuusyo_hak_date"), row.Item("uri_date"), row.Item("bikou"), _
            row.Item("kameiten_upd_datetime"), row.Item("tatouwari_upd_datetime_1"), row.Item("tatouwari_upd_datetime_2"), row.Item("tatouwari_upd_datetime_3"), row.Item("kameiten_jyuusyo_upd_datetime"), _
            String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, _
             row.Item("ssgr_kkk"), _
             row.Item("kaiseki_hosyou_kkk"), _
             row.Item("koj_mitiraisyo_soufu_fuyou"), _
             row.Item("hikiwatasi_inji_umu"), _
             row.Item("hosyousyo_hassou_umu"), _
             row.Item("ekijyouka_tokuyaku_kakaku"), _
             row.Item("hosyousyo_hassou_umu_start_date"), _
             row.Item("taiou_syouhin_kbn"), _
             row.Item("taiou_syouhin_kbn_set_date"), _
             row.Item("campaign_waribiki_flg"), _
             row.Item("campaign_waribiki_set_date"), _
             row.Item("online_waribiki_flg"), _
             row.Item("b_str_yuuryou_wide_flg"))
        Next

        'CSVファイルダウンロード
        Response.Charset = "shift-jis"
        Response.ContentType = "text/plain"
        Response.AddHeader("Content-Disposition", "attachment; filename=" & System.Web.HttpUtility.UrlEncode(strFileName))
        Response.End()
    End Sub

    ''' <summary>入力チェック</summary>
    ''' <param name="strObjId">クライアントID</param>
    ''' <returns>エラーメッセージ</returns>
    Public Function CheckInput(ByRef strObjId As String) As String
        Dim csScript As New StringBuilder
        With csScript
            '加盟店名
            If Me.tbxKameitenMei.Text <> String.Empty Then
                .Append(commonCheck.CheckKinsoku(Me.tbxKameitenMei.Text, "加盟店名"))
                .Append(commonCheck.CheckByte(Me.tbxKameitenMei.Text, 40, "加盟店名", kbn.ZENKAKU))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxKameitenMei.ClientID
                End If
            End If
            '加盟店コード(From)
            If Me.tbxKameitenCd1.Text <> String.Empty Then
                .Append(commonCheck.ChkHankakuEisuuji(Me.tbxKameitenCd1.Text, "加盟店コード(From)"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxKameitenCd1.ClientID
                End If
            End If
            '加盟店コード(To)
            If Me.tbxKameitenCd2.Text <> String.Empty Then
                .Append(commonCheck.ChkHankakuEisuuji(Me.tbxKameitenCd2.Text, "加盟店コード(To)"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxKameitenCd2.ClientID
                End If
            End If
            '加盟店コード範囲
            If Me.tbxKameitenCd1.Text <> String.Empty And Me.tbxKameitenCd2.Text <> String.Empty Then
                If commonCheck.ChkHankakuEisuuji(Me.tbxKameitenCd1.Text, "加盟店コード(From)") = String.Empty _
                   And commonCheck.ChkHankakuEisuuji(Me.tbxKameitenCd2.Text, "加盟店コード(To)") = String.Empty Then
                    If Me.tbxKameitenCd1.Text > Me.tbxKameitenCd2.Text Then
                        .Append(String.Format(Messages.Instance.MSG2012E, "加盟店コード", "加盟店コード").ToString)
                        If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                            strObjId = Me.tbxKameitenCd1.ClientID
                        End If
                    End If
                End If
            End If
            If Me.tbxKameitenCd1.Text = String.Empty And Me.tbxKameitenCd2.Text <> String.Empty Then
                If commonCheck.ChkHankakuEisuuji(Me.tbxKameitenCd2.Text, "加盟店コード(To)") = String.Empty Then
                    .Append(String.Format(Messages.Instance.MSG2012E, "加盟店コード", "加盟店コード").ToString)
                    If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                        strObjId = Me.tbxKameitenCd1.ClientID
                    End If
                End If
            End If
            '加盟店名カナ
            If Me.tbxKameitenKana.Text.Replace("%", String.Empty) <> String.Empty Then
                .Append(commonCheck.CheckKatakana(Me.tbxKameitenKana.Text.Replace("%", String.Empty), "加盟店名カナ", True))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxKameitenKana.ClientID
                End If
            End If
            '営業所コード(From)
            If Me.tbxEigyousyoCd1.Text <> String.Empty Then
                .Append(commonCheck.ChkHankakuEisuuji(Me.tbxEigyousyoCd1.Text, "営業所コード(From)"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxEigyousyoCd1.ClientID
                End If
            End If
            '営業所コード(To)
            If Me.tbxEigyousyoCd2.Text <> String.Empty Then
                .Append(commonCheck.ChkHankakuEisuuji(Me.tbxEigyousyoCd2.Text, "営業所コード(To)"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxEigyousyoCd2.ClientID
                End If
            End If
            '営業所コード範囲
            If Me.tbxEigyousyoCd1.Text <> String.Empty And Me.tbxEigyousyoCd2.Text <> String.Empty Then
                If commonCheck.ChkHankakuEisuuji(Me.tbxEigyousyoCd1.Text, "営業所コード(From)") = String.Empty _
                   And commonCheck.ChkHankakuEisuuji(Me.tbxEigyousyoCd2.Text, "営業所コード(To)") = String.Empty Then
                    If Me.tbxEigyousyoCd1.Text > Me.tbxEigyousyoCd2.Text Then
                        .Append(String.Format(Messages.Instance.MSG2012E, "営業所コード", "営業所コード").ToString)
                        If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                            strObjId = Me.tbxEigyousyoCd1.ClientID
                        End If
                    End If
                End If
            End If
            If Me.tbxEigyousyoCd1.Text = String.Empty And Me.tbxEigyousyoCd2.Text <> String.Empty Then
                If commonCheck.ChkHankakuEisuuji(Me.tbxEigyousyoCd2.Text, "営業所コード(To)") = String.Empty Then
                    .Append(String.Format(Messages.Instance.MSG2012E, "営業所コード", "営業所コード").ToString)
                    If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                        strObjId = Me.tbxEigyousyoCd1.ClientID
                    End If
                End If
            End If
            '電話番号
            If Me.tbxDenwaBangou.Text.Replace("-", String.Empty).Replace("%", String.Empty) <> String.Empty Then
                .Append(commonCheck.CheckHankaku(Me.tbxDenwaBangou.Text.Replace("-", String.Empty).Replace("%", String.Empty), "電話番号"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxDenwaBangou.ClientID
                End If
            End If
            '登録年月(From)
            If Me.tbxTourokuNengetuhi1.Text <> String.Empty Then
                .Append(commonCheck.CheckYuukouHiduke(Me.tbxTourokuNengetuhi1.Text, "登録年月(From)"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxTourokuNengetuhi1.ClientID
                End If
            End If
            '登録年月(To)
            If Me.tbxTourokuNengetuhi2.Text <> String.Empty Then
                .Append(commonCheck.CheckYuukouHiduke(Me.tbxTourokuNengetuhi2.Text, "登録年月(To)"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxTourokuNengetuhi2.ClientID
                End If
            End If
            '登録年月範囲
            If Me.tbxTourokuNengetuhi1.Text <> String.Empty And Me.tbxTourokuNengetuhi2.Text <> String.Empty Then
                If commonCheck.CheckYuukouHiduke(Me.tbxTourokuNengetuhi1.Text, "登録年月(From)") = String.Empty _
                   And commonCheck.CheckYuukouHiduke(Me.tbxTourokuNengetuhi2.Text, "登録年月(To)") = String.Empty Then
                    .Append(commonCheck.CheckHidukeHani(Me.tbxTourokuNengetuhi1.Text, Me.tbxTourokuNengetuhi2.Text, "登録年月"))
                    If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                        strObjId = Me.tbxTourokuNengetuhi1.ClientID
                    End If
                End If
            End If
            If Me.tbxTourokuNengetuhi1.Text = String.Empty And Me.tbxTourokuNengetuhi2.Text <> String.Empty Then
                If commonCheck.CheckYuukouHiduke(Me.tbxTourokuNengetuhi2.Text, "登録年月(To)") = String.Empty Then
                    .Append(String.Format(Messages.Instance.MSG2012E, "登録年月", "登録年月").ToString)
                End If
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxTourokuNengetuhi1.ClientID
                End If
            End If
            '系列コード
            If Me.tbxKeiretuCd.Text <> String.Empty Then
                .Append(commonCheck.ChkHankakuEisuuji(Me.tbxKeiretuCd.Text, "系列コード"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxKeiretuCd.ClientID
                End If
            End If
        End With

        Return csScript.ToString
    End Function

    ''' <summary>画面タイトルに並び順をクリック時</summary>
    Private Sub btnSort_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSortTorikesi1.Click, _
                                                                                           btnSortTorikesi2.Click, _
                                                                                           btnSortKubun1.Click, _
                                                                                           btnSortKubun2.Click, _
                                                                                           btnSortKameitenCd1.Click, _
                                                                                           btnSortKameitenCd2.Click, _
                                                                                           btnSortKameitenMei1.Click, _
                                                                                           btnSortKameitenMei2.Click, _
                                                                                           btnSortKameitenKana1.Click, _
                                                                                           btnSortKameitenKana2.Click, _
                                                                                           btnSortKameitenMei21.Click, _
                                                                                           btnSortKameitenMei22.Click, _
                                                                                           btnSortJyuusyo1.Click, _
                                                                                           btnSortJyuusyo2.Click, _
                                                                                           btnSortTourofukenMei1.Click, _
                                                                                           btnSortTourofukenMei2.Click, _
                                                                                           btnSortKeiretu1.Click, _
                                                                                           btnSortKeiretu2.Click, _
                                                                                           btnSortEigyousyoCd1.Click, _
                                                                                           btnSortEigyousyoCd2.Click, _
                                                                                           btnSortBuilderNo1.Click, _
                                                                                           btnSortBuilderNo2.Click, _
                                                                                           btnSortDaihyousyaMei1.Click, _
                                                                                           btnSortDaihyousyaMei2.Click, _
                                                                                           btnSortTelNo1.Click, _
                                                                                           btnSortTelNo2.Click

        Dim strSort As String = String.Empty
        Dim strUpDown As String = String.Empty

        btnSortTorikesi1.ForeColor = Drawing.Color.SkyBlue
        btnSortTorikesi2.ForeColor = Drawing.Color.SkyBlue
        btnSortKubun1.ForeColor = Drawing.Color.SkyBlue
        btnSortKubun2.ForeColor = Drawing.Color.SkyBlue
        btnSortKameitenCd1.ForeColor = Drawing.Color.SkyBlue
        btnSortKameitenCd2.ForeColor = Drawing.Color.SkyBlue
        btnSortKameitenMei1.ForeColor = Drawing.Color.SkyBlue
        btnSortKameitenMei2.ForeColor = Drawing.Color.SkyBlue
        btnSortKameitenKana1.ForeColor = Drawing.Color.SkyBlue
        btnSortKameitenKana2.ForeColor = Drawing.Color.SkyBlue
        btnSortKameitenMei21.ForeColor = Drawing.Color.SkyBlue
        btnSortKameitenMei22.ForeColor = Drawing.Color.SkyBlue
        btnSortJyuusyo1.ForeColor = Drawing.Color.SkyBlue
        btnSortJyuusyo2.ForeColor = Drawing.Color.SkyBlue
        btnSortTourofukenMei1.ForeColor = Drawing.Color.SkyBlue
        btnSortTourofukenMei2.ForeColor = Drawing.Color.SkyBlue
        btnSortKeiretu1.ForeColor = Drawing.Color.SkyBlue
        btnSortKeiretu2.ForeColor = Drawing.Color.SkyBlue
        btnSortEigyousyoCd1.ForeColor = Drawing.Color.SkyBlue
        btnSortEigyousyoCd2.ForeColor = Drawing.Color.SkyBlue
        btnSortBuilderNo1.ForeColor = Drawing.Color.SkyBlue
        btnSortBuilderNo2.ForeColor = Drawing.Color.SkyBlue
        btnSortDaihyousyaMei1.ForeColor = Drawing.Color.SkyBlue
        btnSortDaihyousyaMei2.ForeColor = Drawing.Color.SkyBlue
        btnSortTelNo1.ForeColor = Drawing.Color.SkyBlue
        btnSortTelNo2.ForeColor = Drawing.Color.SkyBlue

        '画面にソート順をクリック時
        Select Case CType(sender, LinkButton).ID
            Case btnSortTorikesi1.ID
                strSort = "torikesi"
                strUpDown = "ASC"
                btnSortTorikesi1.ForeColor = Drawing.Color.IndianRed
            Case btnSortTorikesi2.ID
                strSort = "torikesi"
                strUpDown = "DESC"
                btnSortTorikesi2.ForeColor = Drawing.Color.IndianRed
            Case btnSortKubun1.ID
                strSort = "kbn"
                strUpDown = "ASC"
                btnSortKubun1.ForeColor = Drawing.Color.IndianRed
            Case btnSortKubun2.ID
                strSort = "kbn"
                strUpDown = "DESC"
                btnSortKubun2.ForeColor = Drawing.Color.IndianRed
            Case btnSortKameitenCd1.ID
                strSort = "kameiten_cd"
                strUpDown = "ASC"
                btnSortKameitenCd1.ForeColor = Drawing.Color.IndianRed
            Case btnSortKameitenCd2.ID
                strSort = "kameiten_cd"
                strUpDown = "DESC"
                btnSortKameitenCd2.ForeColor = Drawing.Color.IndianRed
            Case btnSortKameitenMei1.ID
                strSort = "kameiten_mei1"
                strUpDown = "ASC"
                btnSortKameitenMei1.ForeColor = Drawing.Color.IndianRed
            Case btnSortKameitenMei2.ID
                strSort = "kameiten_mei1"
                strUpDown = "DESC"
                btnSortKameitenMei2.ForeColor = Drawing.Color.IndianRed
            Case btnSortKameitenKana1.ID
                strSort = "tenmei_kana1"
                strUpDown = "ASC"
                btnSortKameitenKana1.ForeColor = Drawing.Color.IndianRed
            Case btnSortKameitenKana2.ID
                strSort = "tenmei_kana1"
                strUpDown = "DESC"
                btnSortKameitenKana2.ForeColor = Drawing.Color.IndianRed
            Case btnSortKameitenMei21.ID
                strSort = "kameiten_mei2"
                strUpDown = "ASC"
                btnSortKameitenMei21.ForeColor = Drawing.Color.IndianRed
            Case btnSortKameitenMei22.ID
                strSort = "kameiten_mei2"
                strUpDown = "DESC"
                btnSortKameitenMei22.ForeColor = Drawing.Color.IndianRed
            Case btnSortJyuusyo1.ID
                strSort = "jyuusyo1"
                strUpDown = "ASC"
                btnSortJyuusyo1.ForeColor = Drawing.Color.IndianRed
            Case btnSortJyuusyo2.ID
                strSort = "jyuusyo1"
                strUpDown = "DESC"
                btnSortJyuusyo2.ForeColor = Drawing.Color.IndianRed
            Case btnSortTourofukenMei1.ID
                strSort = "todouhuken_mei"
                strUpDown = "ASC"
                btnSortTourofukenMei1.ForeColor = Drawing.Color.IndianRed
            Case btnSortTourofukenMei2.ID
                strSort = "todouhuken_mei"
                strUpDown = "DESC"
                btnSortTourofukenMei2.ForeColor = Drawing.Color.IndianRed
            Case btnSortKeiretu1.ID
                strSort = "keiretu_cd"
                strUpDown = "ASC"
                btnSortKeiretu1.ForeColor = Drawing.Color.IndianRed
            Case btnSortKeiretu2.ID
                strSort = "keiretu_cd"
                strUpDown = "DESC"
                btnSortKeiretu2.ForeColor = Drawing.Color.IndianRed
            Case btnSortEigyousyoCd1.ID
                strSort = "eigyousyo_cd"
                strUpDown = "ASC"
                btnSortEigyousyoCd1.ForeColor = Drawing.Color.IndianRed
            Case btnSortEigyousyoCd2.ID
                strSort = "eigyousyo_cd"
                strUpDown = "DESC"
                btnSortEigyousyoCd2.ForeColor = Drawing.Color.IndianRed
            Case btnSortBuilderNo1.ID
                strSort = "builder_no"
                strUpDown = "ASC"
                btnSortBuilderNo1.ForeColor = Drawing.Color.IndianRed
            Case btnSortBuilderNo2.ID
                strSort = "builder_no"
                strUpDown = "DESC"
                btnSortBuilderNo2.ForeColor = Drawing.Color.IndianRed
            Case btnSortDaihyousyaMei1.ID
                strSort = "daihyousya_mei"
                strUpDown = "ASC"
                btnSortDaihyousyaMei1.ForeColor = Drawing.Color.IndianRed
            Case btnSortDaihyousyaMei2.ID
                strSort = "daihyousya_mei"
                strUpDown = "DESC"
                btnSortDaihyousyaMei2.ForeColor = Drawing.Color.IndianRed
            Case btnSortTelNo1.ID
                strSort = "tel_no"
                strUpDown = "ASC"
                btnSortTelNo1.ForeColor = Drawing.Color.IndianRed
            Case btnSortTelNo2.ID
                strSort = "tel_no"
                strUpDown = "DESC"
                btnSortTelNo2.ForeColor = Drawing.Color.IndianRed
        End Select

        '画面データのソート順を設定する
        Dim dvKameitenInfo As Data.DataView = CType(ViewState("dtKameitenInfo"), Data.DataTable).DefaultView
        dvKameitenInfo.Sort = strSort & " " & strUpDown

        Me.grdItiranLeft.DataSource = dvKameitenInfo
        Me.grdItiranLeft.DataBind()
        Me.grdItiranRight.DataSource = dvKameitenInfo
        Me.grdItiranRight.DataBind()

        '画面縦スクロールを設定する
        scrollHeight = ViewState("scrollHeight")

        '画面横スクロール位置を設定する
        SetScroll()

    End Sub

    ''' <summary>Javascript作成</summary>
    Private Sub MakeJavascript()
        Dim sbScript As New StringBuilder
        Dim strPraram(0) As String
        With sbScript
            .AppendLine("<script language='javascript' type='text/javascript'>")
            .AppendLine("   var objKbn1 = document.getElementById('" & Me.Common_drop1.DdlClientID & "')")
            .AppendLine("   var objKbn2 = document.getElementById('" & Me.Common_drop2.DdlClientID & "')")
            .AppendLine("   var objKbn3 = document.getElementById('" & Me.Common_drop3.DdlClientID & "')")
            .AppendLine("   var objKbnAll = document.getElementById('" & Me.chkKubunAll.ClientID & "')")
            .AppendLine("   function fncSetLineColor(obj,index){")
            .AppendLine("       document.getElementById('" & Me.hidSelectRowIndex.ClientID & "').value = index;")
            .AppendLine("       var obj1 = objEBI('" + Me.grdItiranRight.ClientID + "').childNodes[0].childNodes[index] ")
            .AppendLine("       setSelectedLineColor(obj,obj1);")
            .AppendLine("   }")
            .AppendLine("   function fncSetKubunVal(){")
            .AppendLine("       if(objKbnAll.checked == true){")
            .AppendLine("           objKbn1.selectedIndex = 0;")
            .AppendLine("           objKbn2.selectedIndex = 0;")
            .AppendLine("           objKbn3.selectedIndex = 0;")
            .AppendLine("           objKbn1.disabled = true;")
            .AppendLine("           objKbn2.disabled = true;")
            .AppendLine("           objKbn3.disabled = true;")
            .AppendLine("       }else{")
            .AppendLine("           objKbn1.disabled = false;")
            .AppendLine("           objKbn2.disabled = false;")
            .AppendLine("           objKbn3.disabled = false;")
            .AppendLine("       }")
            .AppendLine("   }")
            .AppendLine("   function funDisableButton(){")

            '--------------------From 2013.03.06 李宇追加--------------------
            .AppendLine("       var strKameitenCd = document.getElementById('hidKameitenCd').value;")
            .AppendLine("       document.getElementById('" & Me.hidSentakuKameitenCd.ClientID & "').value = strKameitenCd;")
            .AppendLine("       var strSentakuKbn = document.getElementById('hidKbnCd').value;")
            .AppendLine("       document.getElementById('" & Me.hidSentakuKbn.ClientID & "').value = strSentakuKbn;")
            '--------------------To   2013.03.06 李宇追加--------------------

            .AppendLine("       document.getElementById('" & Me.btnKihonJyouhou.ClientID & "').disabled = false;")
            .AppendLine("       document.getElementById('" & Me.btnTyuiJikou.ClientID & "').disabled = false;")
            .AppendLine("       document.getElementById('" & Me.btnBukkenJyouhou.ClientID & "').disabled = false;")
            .AppendLine("       document.getElementById('" & Me.btnYosinJyouhou.ClientID & "').disabled = false;")
            .AppendLine("       document.getElementById('" & Me.btnKakakuJyouhou.ClientID & "').disabled = false;")

            '--------------------From 2013.03.06 李宇追加--------------------
            .AppendLine("       document.getElementById('" & Me.btnSiharaiTyousa.ClientID & "').disabled = false;")
            .AppendLine("       document.getElementById('" & Me.btnSiharaiKouji.ClientID & "').disabled = false;")
            .AppendLine("       document.getElementById('" & Me.btnHoukakusyo.ClientID & "').disabled = false;")
            .AppendLine("       document.getElementById('" & Me.btnTorihukiJyoukenKakuninhyou.ClientID & "').disabled = false;")
            .AppendLine("       document.getElementById('" & Me.btnTyousaCard.ClientID & "').disabled = false;")
            '--------------------To   2013.03.06 李宇追加--------------------

            .AppendLine("   }")
            .AppendLine("   function fncGamenSenni(strGamenId){")
            .AppendLine("       var strKameitenCd = document.getElementById('hidKameitenCd').value;")
            .AppendLine("       if(strGamenId == '0')  window.open('KihonJyouhouInput.aspx')")
            .AppendLine("       if(strGamenId == '1')  window.open('KihonJyouhouInquiry.aspx?strKameitenCd='+strKameitenCd)")
            .AppendLine("       if(strGamenId == '2')  window.open('TyuiJyouhouInquiry.aspx?strKameitenCd='+strKameitenCd)")
            .AppendLine("       if(strGamenId == '3')  window.open('BukkenJyouhouInquiry.aspx?strKameitenCd='+strKameitenCd)")
            .AppendLine("       if(strGamenId == '4')  window.open('YosinJyouhouInquiry.aspx?strKameitenCd='+strKameitenCd)")
            .AppendLine("       if(strGamenId == '5')  window.open('HanbaiKakakuMasterSearchList.aspx?sendSearchTerms='+'1'+'$$$'+strKameitenCd)")
            .AppendLine("       if(strGamenId == '6')  window.open('KameitenMasterInput.aspx')")
            .AppendLine("   }")
            .AppendLine("   function fncClearWin(){")
            .AppendLine("       for(var i = 0;i < document.forms.length;i++){")
            .AppendLine("          c_form = document.forms[i];")
            .AppendLine("          for (var j = 0;j < c_form.elements.length;j++){")
            .AppendLine("              if(c_form.elements[j].type == 'text'){")
            .AppendLine("                  c_form.elements[j].value = '';")
            .AppendLine("              }")
            .AppendLine("              if(c_form.elements[j].type == 'checkbox'){")
            .AppendLine("                  c_form.elements[j].checked = false;")
            .AppendLine("              }")
            .AppendLine("          }")
            .AppendLine("       }")
            .AppendLine("       objKbn1.selectedIndex = 0;")
            .AppendLine("       objKbn1.disabled = true;")
            .AppendLine("       objKbn2.selectedIndex = 0;")
            .AppendLine("       objKbn2.disabled = true;")
            .AppendLine("       objKbn3.selectedIndex = 0;")
            .AppendLine("       objKbn3.disabled = true;")
            .AppendLine("       objKbnAll.checked = true;")
            .AppendLine("       document.getElementById('" & Me.chkLitSoufusaki.ClientID & "').childNodes[0].checked=true;")
            .AppendLine("       document.getElementById('" & CType(Me.Common_drop.FindControl("ddlCommonDrop"), DropDownList).ClientID & "').selectedIndex = 0;")
            .AppendLine("       document.getElementById('" & Me.ddlSearchCount.ClientID & "').selectedIndex = 0;")
            .AppendLine("   }")
            .AppendLine("   function fncScrollV(){")
            .AppendLine("       var divbodyleft=" & Me.divBodyLeft.ClientID & ";")
            .AppendLine("       var divbodyright=" & Me.divBodyRight.ClientID & ";")
            .AppendLine("       var divVscroll=" & Me.divHiddenMeisaiV.ClientID & ";")
            .AppendLine("       divbodyleft.scrollTop = divVscroll.scrollTop;")
            .AppendLine("       divbodyright.scrollTop = divVscroll.scrollTop;")
            .AppendLine("   }")
            .AppendLine("   function fncScrollH(){")
            .AppendLine("       var divheadright=" & Me.divHeadRight.ClientID & ";")
            .AppendLine("       var divbodyright=" & Me.divBodyRight.ClientID & ";")
            .AppendLine("       var divHscroll=" & Me.divHiddenMeisaiH.ClientID & ";")
            .AppendLine("       divheadright.scrollLeft = divHscroll.scrollLeft;")
            .AppendLine("       divbodyright.scrollLeft = divHscroll.scrollLeft;")
            .AppendLine("   }")
            .AppendLine("   function fncSetScroll(){")
            .AppendLine("       var divHscroll=" & Me.divHiddenMeisaiH.ClientID & ";")
            .AppendLine("       document.getElementById('" & Me.hidScroll.ClientID & "').value = divHscroll.scrollLeft;")
            .AppendLine("   }")
            .AppendLine("   function wheel(event){")
            .AppendLine("       var delta = 0;")
            .AppendLine("       if(!event)")
            .AppendLine("           event = window.event;")
            .AppendLine("       if (event.wheelDelta){")
            .AppendLine("           delta = event.wheelDelta/120;")
            .AppendLine("           if (window.opera)")
            .AppendLine("               delta = -delta;")
            .AppendLine("       } else if(event.detail){")
            .AppendLine("           delta = -event.detail/3;")
            .AppendLine("       }")
            .AppendLine("       if (delta)")
            .AppendLine("           handle(delta);")
            .AppendLine("   }")
            .AppendLine("   function handle(delta){")
            .AppendLine("      var divVscroll=" & Me.divHiddenMeisaiV.ClientID & ";")
            .AppendLine("      if (delta < 0){")
            .AppendLine("          divVscroll.scrollTop = divVscroll.scrollTop + 15;")
            .AppendLine("      }else{")
            .AppendLine("          divVscroll.scrollTop = divVscroll.scrollTop - 15;")
            .AppendLine("      }")
            .AppendLine("   }")
            .AppendLine("   function fncShowModal(){")
            .AppendLine("      var buyDiv=document.getElementById('" & Me.buySelName.ClientID & "');")
            .AppendLine("      var disable=document.getElementById('" & Me.disableDiv.ClientID & "');")
            .AppendLine("      if(buyDiv.style.display=='none')")
            .AppendLine("      {")
            .AppendLine("       buyDiv.style.display='';")
            .AppendLine("       disable.style.display='';")
            .AppendLine("       disable.focus();")
            .AppendLine("      }else{")
            .AppendLine("       buyDiv.style.display='none';")
            .AppendLine("       disable.style.display='none';")
            .AppendLine("      }")
            .AppendLine("   }")
            .AppendLine("   function fncClosecover(){")
            .AppendLine("       var buyDiv=document.getElementById('" & Me.buySelName.ClientID & "');")
            .AppendLine("       var disable=document.getElementById('" & Me.disableDiv.ClientID & "');")
            .AppendLine("       buyDiv.style.display='none';")
            .AppendLine("       disable.style.display='none';")
            .AppendLine("   }")
            '加盟店ポップアップ
            .AppendLine("   function fncKameitenSearch(strKbn){")
            .AppendLine("       if(objKbn1.selectedIndex==0 && objKbn2.selectedIndex==0 && objKbn3.selectedIndex==0 && !objKbnAll.checked){")
            .AppendLine("           alert('" & Messages.Instance.MSG006E & "');")
            .AppendLine("           objKbn1.focus();")
            .AppendLine("           return false;")
            .AppendLine("       }")
            .AppendLine("       var strkbn='加盟店'")
            .AppendLine("       var strClientID ")
            .AppendLine("       var blnTaikai ")
            .AppendLine("       var arrKubun = ''")
            .AppendLine("       if(objKbn1.selectedIndex!=0){")
            .AppendLine("           arrKubun = arrKubun + objKbn1.value + ',';")
            .AppendLine("       }")
            .AppendLine("       if(objKbn2.selectedIndex!=0){")
            .AppendLine("           arrKubun = arrKubun + objKbn2.value + ',';")
            .AppendLine("       }")
            .AppendLine("       if(objKbn3.selectedIndex!=0){")
            .AppendLine("           arrKubun = arrKubun + objKbn3.value + ',';")
            .AppendLine("       }")
            .AppendLine("       arrKubun = arrKubun.substring(0,arrKubun.length-1);")
            .AppendLine("       if(strKbn=='1'){")
            .AppendLine("           strClientID = '" & Me.tbxKameitenCd1.ClientID & "'")
            .AppendLine("       }else{")
            .AppendLine("           strClientID = '" & Me.tbxKameitenCd2.ClientID & "'")
            .AppendLine("       }")
            .AppendLine("       if(document.getElementById('" & Me.chkTaikai.ClientID & "').checked){")
            .AppendLine("           blnTaikai = 'False' ")
            .AppendLine("       }else{")
            .AppendLine("           blnTaikai = 'True' ")
            .AppendLine("       }")
            .AppendLine("       objSrchWin = window.open('search_common.aspx?Kbn='+escape(strkbn)+'&FormName=" & Me.Form.Name & _
                                "&objCd='+strClientID+'&objMei=&strCd='+escape(eval('document.all.'+strClientID).value)+")
            .AppendLine("       '&KensakuKubun='+arrKubun+'&blnDelete='+blnTaikai, ")
            .AppendLine("       'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');")
            .AppendLine("       return false;")
            .AppendLine("   }")
            '営業所ポップアップ
            .AppendLine("   function fncEigyousyoSearch(strKbn){")
            .AppendLine("       var strkbn='営業所'")
            .AppendLine("       var strClientID ")
            .AppendLine("       var blnTaikai ")
            .AppendLine("       if(strKbn=='1'){")
            .AppendLine("           strClientID = '" & Me.tbxEigyousyoCd1.ClientID & "'")
            .AppendLine("       }else{")
            .AppendLine("           strClientID = '" & Me.tbxEigyousyoCd2.ClientID & "'")
            .AppendLine("       }")
            .AppendLine("       if(document.getElementById('" & Me.chkTaikai.ClientID & "').checked){")
            .AppendLine("           blnTaikai = 'False' ")
            .AppendLine("       }else{")
            .AppendLine("           blnTaikai = 'True' ")
            .AppendLine("       }")
            .AppendLine("       objSrchWin = window.open('search_common.aspx?Kbn='+escape(strkbn)+'&FormName=" & Me.Form.Name & _
                                           "&objCd='+strClientID+'&objMei=&strCd='+escape(eval('document.all.'+strClientID).value)+")
            .AppendLine("       '&KensakuKubun=&blnDelete='+blnTaikai, ")
            .AppendLine("       'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');")
            .AppendLine("       return false;")
            .AppendLine("   }")
            '系列ポップアップ
            .AppendLine("   function fncKeiretuSearch(){")
            .AppendLine("       if(objKbn1.selectedIndex==0 && objKbn2.selectedIndex==0 && objKbn3.selectedIndex==0 && !objKbnAll.checked){")
            .AppendLine("           alert('" & Messages.Instance.MSG006E & "');")
            .AppendLine("           objKbn1.focus();")
            .AppendLine("           return false;")
            .AppendLine("       }")
            .AppendLine("       var strkbn='系列'")
            .AppendLine("       var blnTaikai ")
            .AppendLine("       var arrKubun = ''")
            .AppendLine("       if(objKbn1.selectedIndex!=0){")
            .AppendLine("           arrKubun = arrKubun + objKbn1.value + ',';")
            .AppendLine("       }")
            .AppendLine("       if(objKbn2.selectedIndex!=0){")
            .AppendLine("           arrKubun = arrKubun + objKbn2.value + ',';")
            .AppendLine("       }")
            .AppendLine("       if(objKbn3.selectedIndex!=0){")
            .AppendLine("           arrKubun = arrKubun + objKbn3.value + ',';")
            .AppendLine("       }")
            .AppendLine("       arrKubun = arrKubun.substring(0,arrKubun.length-1);")
            .AppendLine("       if(document.getElementById('" & Me.chkTaikai.ClientID & "').checked){")
            .AppendLine("           blnTaikai = 'False' ")
            .AppendLine("       }else{")
            .AppendLine("           blnTaikai = 'True' ")
            .AppendLine("       }")
            .AppendLine("       objSrchWin = window.open('search_common.aspx?Kbn='+escape(strkbn)+'&FormName=" & Me.Form.Name & _
                                           "&objCd=" & Me.tbxKeiretuCd.ClientID & "&objMei=" & Me.tbxKeiretuMei.ClientID & _
                                           "&strCd='+escape(eval('document.all.'+'" & Me.tbxKeiretuCd.ClientID & "').value)+")
            .AppendLine("       '&strMei=&KensakuKubun='+arrKubun+'&blnDelete='+blnTaikai, ")
            .AppendLine("       'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');")
            .AppendLine("       return false;")
            .AppendLine("   }")
            .AppendLine("   function fncSetKameitenCd(){")
            .AppendLine("       var strKameitenCd = event.srcElement.parentNode.parentNode.childNodes[3].innerText; ")
            .AppendLine("       document.getElementById('hidKameitenCd').value = strKameitenCd; ")
            '-------------------------------FROM 李宇2013.03.22追加----------------------
            .AppendLine("       var strKbnCd = event.srcElement.parentNode.parentNode.childNodes[2].innerText; ")
            .AppendLine("       document.getElementById('hidKbnCd').value = strKbnCd; ")
            '-------------------------------TO   李宇2013.03.22追加----------------------
            .AppendLine("   }")
            .AppendLine("   function fncSetKeiretuMei(){")
            .AppendLine("       document.getElementById('" & Me.hidKeiretuMei.ClientID & "').value = document.getElementById('" & Me.tbxKeiretuMei.ClientID & "').value;")
            .AppendLine("   }")
            '日付チェック(yyyy/mm)
            .AppendLine("   function fncCheckNengetu(obj){")
            .AppendLine("   	if (obj.value==''){return true;}")
            .AppendLine("   	var checkFlg = true;")
            .AppendLine("   	obj.value = obj.value.Trim();")
            .AppendLine("   	var val = obj.value;")
            .AppendLine("   	val = SetDateNoSign(val,'/');")
            .AppendLine("   	val = SetDateNoSign(val,'-');")
            .AppendLine("   	val = val+'01';")
            .AppendLine("   	if(val == '')return;")
            .AppendLine("   	val = removeSlash(val);")
            .AppendLine("   	val = val.replace(/\-/g, '');")
            .AppendLine("   	if(val.length == 6){")
            .AppendLine("   		if(val.substring(0, 2) > 70){")
            .AppendLine("   			val = '19' + val;")
            .AppendLine("   		}else{")
            .AppendLine("   			val = '20' + val;")
            .AppendLine("   		}")
            .AppendLine("   	}else if(val.length == 4){")
            .AppendLine("   		dd = new Date();")
            .AppendLine("   		year = dd.getFullYear();")
            .AppendLine("   		val = year + val;")
            .AppendLine("   	}")
            .AppendLine("   	if(val.length != 8){")
            .AppendLine("   		checkFlg = false;")
            .AppendLine("   	}else{  //8桁の場合")
            .AppendLine("   		val = addSlash(val);")
            .AppendLine("   		var arrD = val.split('/');")
            .AppendLine("   		if(checkDateVali(arrD[0],arrD[1],arrD[2]) == false){")
            .AppendLine("   			checkFlg = false; ")
            .AppendLine("   		}")
            .AppendLine("   	}")
            .AppendLine("   	if(!checkFlg){")
            .AppendLine("   		event.returnValue = false;")
            .AppendLine("           alert('" & String.Format(Messages.Instance.MSG2019E, "登録年月").ToString & "');")
            .AppendLine("           obj.focus();")
            .AppendLine("   		obj.select();")
            .AppendLine("   		return false;")
            .AppendLine("   	}else{")
            .AppendLine("   		obj.value = val.substring(0,7);")
            .AppendLine("   	}")
            .AppendLine("   }")
            .AppendLine("   function SetDateNoSign(value,sign){")
            .AppendLine("   	var arr;")
            .AppendLine("   	arr = value.split(sign);")
            .AppendLine("   	var i;")
            .AppendLine("   	for(i=0;i<=arr.length-1;i++){")
            .AppendLine("   		if(arr[i].length==1){")
            .AppendLine("   			arr[i] = '0' + arr[i];        ")
            .AppendLine("   		}")
            .AppendLine("   	}")
            .AppendLine("   	return arr.join('');")
            .AppendLine("   } ")
            '画面入力チェック
            .AppendLine("   function fncNyuuryokuCheck(strButtonFlg){")
            .AppendLine("       if(objKbn1.selectedIndex==0 && objKbn2.selectedIndex==0 && objKbn3.selectedIndex==0 && !objKbnAll.checked){")
            .AppendLine("           alert('" & Messages.Instance.MSG006E & "');")
            .AppendLine("           objKbn1.focus();")
            .AppendLine("           return false;")
            .AppendLine("       }")
            .AppendLine("       if(strButtonFlg=='1' && document.getElementById('" & Me.ddlSearchCount.ClientID & "').value=='max'){")
            .AppendLine("           if (confirm('" & Messages.Instance.MSG007C & "')){")
            .AppendLine("               document.forms[0].submit();")
            .AppendLine("           }else{")
            .AppendLine("               return false;")
            .AppendLine("           }")
            .AppendLine("       }")
            .AppendLine("       return true;")
            .AppendLine("   }")
            .AppendLine("   function fncClickRow()")
            .AppendLine("   {")
            .AppendLine("       var intSelectRowIndex = document.getElementById('" & Me.hidSelectRowIndex.ClientID & "').value;")
            .AppendLine("       objEBI('" + Me.grdItiranLeft.ClientID + "').childNodes[0].childNodes[intSelectRowIndex].childNodes[0].childNodes[0].click();")
            .AppendLine("   }")
            .AppendLine("</script>")
        End With
        Page.ClientScript.RegisterStartupScript(Page.GetType, "InputCheck", sbScript.ToString)
    End Sub

    ''' <summary>メッセージ表示</summary>
    ''' <param name="strMessage">メッセージ</param>
    ''' <param name="strObjId">クライアントID</param>
    Private Sub ShowMessage(ByVal strMessage As String, ByVal strObjId As String)
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("function window.onload() {")
            .AppendLine("   fncSetKubunVal();")
            .AppendLine("	alert('" & strMessage & "');")
            If strObjId <> String.Empty Then
                .AppendLine("   document.getElementById('" & strObjId & "').focus();")
                .AppendLine("   document.getElementById('" & strObjId & "').select();")
            End If
            .AppendLine("}")
        End With
        'ページ応答で、クライアント側のスクリプト ブロックを出力します
        ClientScript.RegisterStartupScript(Me.GetType(), "ShowMessage", csScript.ToString, True)
    End Sub

    ''' <summary>DIV表示</summary>
    Public Sub CloseCover()
        Dim csScript As New StringBuilder
        csScript.AppendFormat("fncClosecover();").ToString()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CloseCover", csScript.ToString, True)
    End Sub

    ''' <summary>横スクロール設定</summary>
    Public Sub SetScroll()
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("var divheadright=" & Me.divHeadRight.ClientID & ";")
            .AppendLine("var divbodyright=" & Me.divBodyRight.ClientID & ";")
            .AppendLine("var divHscroll=" & Me.divHiddenMeisaiH.ClientID & ";")
            .AppendLine("divheadright.scrollLeft = document.getElementById('" & Me.hidScroll.ClientID & "').value;")
            .AppendLine("divbodyright.scrollLeft = document.getElementById('" & Me.hidScroll.ClientID & "').value;")
            .AppendLine("divHscroll.scrollLeft = document.getElementById('" & Me.hidScroll.ClientID & "').value;")
        End With
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SetScroll", csScript.ToString, True)
    End Sub

    ''' <summary>
    ''' 選択した行をクリックする
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClickSelectRow()
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("setTimeout('fncClickRow();',100);")
        End With
        'ページ応答で、クライアント側のスクリプト ブロックを出力します
        ClientScript.RegisterStartupScript(Me.GetType(), "ClickRow", csScript.ToString, True)
    End Sub


    Private Sub grdItiranLeft_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdItiranLeft.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim lblTorikesi As Label
            lblTorikesi = CType(e.Row.FindControl("lblTorikesi"), Label)

            If lblTorikesi.Text.Trim.Equals("0") OrElse lblTorikesi.Text.Trim.Equals(String.Empty) Then
                '=============2012/04/20 車龍 405721の要望対応 追加↓==================
                lblTorikesi.Text = String.Empty
                '=============2012/04/20 車龍 405721の要望対応 追加↑==================
            Else
                lblTorikesi.ForeColor = Drawing.Color.Red
                e.Row.Cells(3).ForeColor = Drawing.Color.Red
            End If

        End If

    End Sub

    Private Sub grdItiranRight_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdItiranRight.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim rowIndex As Integer = e.Row.RowIndex

            Dim lblTorikesi As Label
            lblTorikesi = CType(Me.grdItiranLeft.Rows(rowIndex).FindControl("lblTorikesi"), Label)

            If lblTorikesi.Text.Trim.Equals("0") OrElse lblTorikesi.Text.Trim.Equals(String.Empty) Then
            Else
                e.Row.Cells(0).ForeColor = Drawing.Color.Red
            End If

        End If

    End Sub

    ''' <summary>
    ''' 支払条件（調査）ボタンを押下する時
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    ''' <history>李宇 大連情報システム部 2013.03.06</history>
    Private Sub btnSiharaiTyousa_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSiharaiTyousa.Click

        Dim strTys_seikyuu_saki_cd As String         '調査請求先コード tys_seikyuu_saki_cd
        Dim strTys_seikyuu_saki_brc As String        '調査請求先枝番 tys_seikyuu_saki_brc 
        Dim strTys_seikyuu_saki_kbn As String        '調査請求先区分 tys_seikyuu_saki_kbn

        '加盟店マスタでデータを取得する
        Dim dtTyousaJyouhou As Data.DataTable
        dtTyousaJyouhou = KihonJyouhouBL.GetKameiten(Me.hidSentakuKameitenCd.Value)
        If dtTyousaJyouhou.Rows.Count > 0 Then
            strTys_seikyuu_saki_cd = dtTyousaJyouhou.Rows(0).Item("tys_seikyuu_saki_cd").ToString
            strTys_seikyuu_saki_brc = dtTyousaJyouhou.Rows(0).Item("tys_seikyuu_saki_brc").ToString
            strTys_seikyuu_saki_kbn = dtTyousaJyouhou.Rows(0).Item("tys_seikyuu_saki_kbn").ToString
        Else
            strTys_seikyuu_saki_cd = String.Empty
            strTys_seikyuu_saki_brc = String.Empty
            strTys_seikyuu_saki_kbn = String.Empty
        End If

        '検索後、データがあったら、RadioButtonをクッリク前、データを削除された
        If strTys_seikyuu_saki_kbn.Trim = "" OrElse strTys_seikyuu_saki_cd.Trim = "" OrElse strTys_seikyuu_saki_brc.Trim = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "err_Allinput", "alert('請求先情報が設定されていません。\r\n請求先を入力して下さい。');", True)
            Exit Sub
        End If

        '請求先マスタからデータを取得する
        Dim SKU_datatable As Data.DataTable
        SKU_datatable = KihonJyouhouBL.GetSeikyusaki(strTys_seikyuu_saki_kbn, strTys_seikyuu_saki_cd, strTys_seikyuu_saki_brc, 0, False)

        If SKU_datatable.Rows.Count = 1 Then
            '請求先マスタメンテナンス画面を開く
            Dim strScript As String
            strScript = "objSrchWin = window.open('SeikyuuSakiMaster.aspx?sendSearchTerms='+escape('" & strTys_seikyuu_saki_cd & "')+'$$$'+escape('" & strTys_seikyuu_saki_brc & "')+'$$$'+escape('" & strTys_seikyuu_saki_kbn & "')+'$$$'+escape('1'), 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');"
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "err_seikyuInputPage", strScript, True)
        Else
            '上記以外
            'メッセージ「請求先が存在しません」を表示し、処理中断	
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "err_Syousai", "alert('請求先が存在しません。');", True)
        End If

        '画面縦スクロールを設定する
        scrollHeight = ViewState("scrollHeight")

        '画面横スクロール位置を設定する
        SetScroll()

        '選択した行をクリックする
        Call Me.ClickSelectRow()

    End Sub

    ''' <summary>
    ''' 支払条件（工事）ボタンを押下する時
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    ''' <history>李宇 大連情報システム部 2013.03.06</history>
    Private Sub btnSiharaiKouji_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSiharaiKouji.Click

        Dim strTys_seikyuu_saki_cd As String         '工事請求先コード koj_seikyuu_saki_cd
        Dim strTys_seikyuu_saki_brc As String        '工事請求先枝番   koj_seikyuu_saki_brc 
        Dim strTys_seikyuu_saki_kbn As String        '工事請求先区分   koj_seikyuu_saki_kbn

        '加盟店マスタでデータを取得する
        Dim dtTyousaJyouhou As Data.DataTable
        dtTyousaJyouhou = KihonJyouhouBL.GetKameiten(Me.hidSentakuKameitenCd.Value)
        If dtTyousaJyouhou.Rows.Count > 0 Then
            strTys_seikyuu_saki_cd = dtTyousaJyouhou.Rows(0).Item("koj_seikyuu_saki_cd").ToString
            strTys_seikyuu_saki_brc = dtTyousaJyouhou.Rows(0).Item("koj_seikyuu_saki_brc").ToString
            strTys_seikyuu_saki_kbn = dtTyousaJyouhou.Rows(0).Item("koj_seikyuu_saki_kbn").ToString
        Else
            strTys_seikyuu_saki_cd = String.Empty
            strTys_seikyuu_saki_brc = String.Empty
            strTys_seikyuu_saki_kbn = String.Empty
        End If

        '検索後、データがあったら、RadioButtonをクッリク前、データを削除された
        If strTys_seikyuu_saki_kbn.Trim = "" OrElse strTys_seikyuu_saki_cd.Trim = "" OrElse strTys_seikyuu_saki_brc.Trim = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "err_Allinput", "alert('請求先情報が設定されていません。\r\n請求先を入力して下さい。');", True)
            Exit Sub
        End If

        '請求先マスタからデータを取得する
        Dim SKU_datatable As Data.DataTable
        SKU_datatable = KihonJyouhouBL.GetSeikyusaki(strTys_seikyuu_saki_kbn, strTys_seikyuu_saki_cd, strTys_seikyuu_saki_brc, 0, False)

        If SKU_datatable.Rows.Count = 1 Then
            '請求先マスタメンテナンス画面を開く
            Dim strScript As String
            strScript = "objSrchWin = window.open('SeikyuuSakiMaster.aspx?sendSearchTerms='+escape('" & strTys_seikyuu_saki_cd & "')+'$$$'+escape('" & strTys_seikyuu_saki_brc & "')+'$$$'+escape('" & strTys_seikyuu_saki_kbn & "')+'$$$'+escape('2'), 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');"
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "err_seikyuInputPage", strScript, True)
        Else
            '上記以外
            'メッセージ「請求先が存在しません」を表示し、処理中断	
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "err_Syousai", "alert('請求先が存在しません。');", True)
        End If

        '画面縦スクロールを設定する
        scrollHeight = ViewState("scrollHeight")

        '画面横スクロール位置を設定する
        SetScroll()

        '選択した行をクリックする
        Call Me.ClickSelectRow()
    End Sub

    ''' <summary>
    ''' 報告書・オプションボタンを押下する時
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    ''' <history>李宇 大連情報システム部 2013.03.06</history>
    Private Sub btnHoukakusyo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHoukakusyo.Click


        '加盟店商品調査方法特別対応マスタ照会画面
        Dim strScript As String
        strScript = "objSrchWin = window.open('TokubetuTaiouMasterSearchList.aspx?sendSearchTerms=1$$$'+'" & Me.hidSentakuKameitenCd.Value & "');"
        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "err_seikyuInputPage", strScript, True)


        '相手先区分：　1-加盟店
        '相手先コード: 加盟店ｺｰﾄﾞ

        '画面縦スクロールを設定する
        scrollHeight = ViewState("scrollHeight")

        '画面横スクロール位置を設定する
        SetScroll()

        '選択した行をクリックする
        Call Me.ClickSelectRow()
    End Sub

    ''' <summary>
    ''' 取引条件確認表ボタンを押下する時
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    ''' <history>李宇 大連情報システム部 2013.03.26</history>
    Private Sub btnTorihukiJyoukenKakuninhyou_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTorihukiJyoukenKakuninhyou.Click

        Dim FilePath As String = earthAction.TorihukiJyoukenKakuninhyou(Me.hidSentakuKameitenCd.Value, Me.hidSentakuKbn.Value)

        Me.hidFile.Value = FilePath
        'ファイルパスの存在ことを判断する
        If IO.File.Exists(FilePath.Replace("file:", String.Empty).Replace("/", "\")) Then
            Call GetFile()
        Else
            ShowMessage(Messages.Instance.MSG2076E, String.Empty)
        End If

        '画面縦スクロールを設定する
        scrollHeight = ViewState("scrollHeight")

        '画面横スクロール位置を設定する
        SetScroll()

        '選択した行をクリックする
        Call Me.ClickSelectRow()
    End Sub

    ''' <summary>
    ''' 調査カードボタンを押下する時
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    ''' <history>李宇 大連情報システム部 2013.03.26</history>
    Private Sub btnTyousaCard_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTyousaCard.Click

        Dim FilePath As String = earthAction.TyousaCard(Me.hidSentakuKameitenCd.Value, Me.hidSentakuKbn.Value)

        Me.hidFile.Value = FilePath
        'ファイルパスの存在ことを判断する
        If IO.File.Exists(FilePath.Replace("file:", String.Empty).Replace("/", "\")) Then
            Call GetFile()
        Else
            ShowMessage(Messages.Instance.MSG2077E, String.Empty)
        End If

        '画面縦スクロールを設定する
        scrollHeight = ViewState("scrollHeight")

        '画面横スクロール位置を設定する
        SetScroll()

        '選択した行をクリックする
        Call Me.ClickSelectRow()
    End Sub

    ''' <summary>
    ''' ファイルパスを開ける
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>李宇 大連情報システム部 2013.03.26</history>
    Private Sub GetFile()

        Dim csType As Type = Page.GetType()
        Dim csName As String = "setScript"
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("<script type =""text/javascript"" >")
            .AppendLine("function OpenFile(){")
            .AppendLine("   document.getElementById('" & Me.file.ClientID & "').href = document.getElementById('" & Me.hidFile.ClientID & "').value;")
            .AppendLine("   document.getElementById('" & Me.file.ClientID & "').click();")
            .AppendLine("}")
            .AppendLine("setTimeout('OpenFile()',1000);")
            .AppendLine("</script>")
        End With
        ClientScript.RegisterClientScriptBlock(csType, csName, csScript.ToString)

    End Sub

End Class
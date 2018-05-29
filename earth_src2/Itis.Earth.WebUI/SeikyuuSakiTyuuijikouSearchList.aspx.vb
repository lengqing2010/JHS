Imports Itis.Earth.BizLogic
Imports Itis.Earth.DataAccess


Partial Public Class SeikyuuSakiTyuuijikouSearchList
    Inherits System.Web.UI.Page

    ''' <summary>原価マスタ</summary>
    ''' <remarks>原価マスタ用機能を提供する</remarks>
    ''' <history>
    ''' <para>2011/02/24　車龍(大連情報システム部)　新規作成</para>
    ''' </history>
    Private seikyuuSakiTyuuijikouLogic As New SeikyuuSakiTyuuijikouLogic
    Private commonCheck As New CommonCheck
    Protected scrollHeight As Integer = 0
    Protected setFlg As Boolean = False

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '権限チェック(経理業務権限)
        Dim commonChk As New CommonCheck
        Dim strUserID As String = ""
        Dim blnKengen As Boolean
        blnKengen = commonChk.CommonNinnsyou(strUserID, "keiri_gyoumu_kengen")

        '「新規登録」ボタンを設定する
        setFlg = Me.SetBtnTouroku()

        'JavaScriptを作成
        Call Me.MakeJavaScript()

        If Not IsPostBack Then
            '参照履歴管理テーブルを登録する。
            commonCheck.SetURL(Me, strUserID)

            If Not Request.QueryString("sendSearchTerms") Is Nothing Then
                '初期化
                Call Syokika(CStr(Request.QueryString("sendSearchTerms")))
            Else
                '初期化
                Call Syokika(String.Empty)
            End If
        Else

            'DIV非表示
            CloseCover()
        End If

        '画面縦スクロールを設定する
        scrollHeight = ViewState("scrollHeight")

        '「閉じる」ボタン
        Me.btnClose.Attributes.Add("onClick", "fncClose();return false;")
        '「クリア」ボタン
        Me.btnClear.Attributes.Add("onClick", "fncClear();return false;")
        '「検索実行」ボタン
        Me.btnKensakujiltukou.Attributes.Add("onClick", "if(!fncNyuuryokuCheck('kensaku')){return false;}else{fncShowModal();}")
        '「CSV出力」ボタン
        Me.btnCsvOutput.Attributes.Add("onClick", "fncShowModal();")
        '「検索」ボタン
        Me.btnSeikyuusakiSearch.Attributes.Add("onClick", "fncSeikyuusakiPopup();return false;")

        If setFlg.Equals(True) Then
            '｢新規登録｣
            Me.btnTouroku.Attributes.Add("onClick", "fncTourokuPopup('btn','','','','');return false;")
        End If
    End Sub

    ''' <summary>初期化</summary> 
    Private Sub Syokika(ByVal sendSearchTerms As String)

        '請求先区分
        Dim strSeikyuusakiKbn As String = String.Empty
        '請求先コード
        Dim strSeikyuusakiCd As String = String.Empty
        '請求先枝番
        Dim strSeikyuusakiBrc As String = String.Empty

        If Not sendSearchTerms.Equals(String.Empty) Then
            'パラメータ
            Dim arrSearchTerm() As String = Split(sendSearchTerms, "$$$")

            '請求先区分
            strSeikyuusakiKbn = arrSearchTerm(0).Trim
            '請求先区分を設定する
            Call Me.SetSeikyuusakiKbn(strSeikyuusakiKbn)

            '請求先コード
            If arrSearchTerm.Length > 1 Then
                strSeikyuusakiCd = arrSearchTerm(1).Trim
                '請求先コードを設定する
                Call Me.SetSeikyuusakiCd(strSeikyuusakiCd)
            Else
                Call Me.SetSeikyuusakiCd(String.Empty)
            End If

            '請求先枝番
            If arrSearchTerm.Length > 2 Then
                strSeikyuusakiBrc = arrSearchTerm(2).Trim
                '請求先枝番を設定する
                Call Me.SetSeikyuusakiBrc(strSeikyuusakiBrc)
            Else
                Call Me.SetSeikyuusakiBrc(String.Empty)
            End If
        Else
            '請求先区分を設定する
            Call Me.SetSeikyuusakiKbn(String.Empty)

            '請求先コードを設定する
            Call Me.SetSeikyuusakiCd(String.Empty)

            '請求先枝番を設定する
            Call Me.SetSeikyuusakiBrc(String.Empty)
        End If

        '請求先名を設定する
        Call Me.SetSeikyuusakiMei()

        '種別コードを設定する
        Call Me.SetSyubetuCd()

        '重要度を設定する
        Call Me.SetJyuuyoudo()

        '請求締め日を設定する
        Call Me.SetSeikyuuSimeDate()

        '請求書必着日を設定する
        Call Me.SetSeikyuusyoHittykDate()

        '取消は検索対象外
        Me.chkKensakuTaisyouGai.Checked = True

        'パラメータが3つ（請求先区分・コード・枝番）ある場合、
        If (Not strSeikyuusakiKbn.Trim.Equals(String.Empty)) AndAlso (Not strSeikyuusakiCd.Trim.Equals(String.Empty)) AndAlso (Not strSeikyuusakiBrc.Trim.Equals(String.Empty)) Then
            '検索して、明細データを表示する
            Call Me.KensakuSyori(False)
        Else
            '明細テーブルを設定する
            Me.grdMeisai.DataSource = Nothing
            Me.grdMeisai.DataBind()

            'ソート順ボタンを設定する
            Call Me.SetSortButton(False)
        End If

    End Sub

    ''' <summary>請求先区分を設定する</summary> 
    Private Sub SetSeikyuusakiKbn(ByVal strSeikyuusakiKbn As String)

        '空白行
        Me.ddlSeikyuusakiKbn.Items.Insert(0, New ListItem(String.Empty, String.Empty))
        '加盟店
        Me.ddlSeikyuusakiKbn.Items.Insert(1, New ListItem("加盟店", "0"))
        '調査会社
        Me.ddlSeikyuusakiKbn.Items.Insert(2, New ListItem("調査会社", "1"))

        '表示の値を設定する
        If Not strSeikyuusakiKbn.Trim.Equals(String.Empty) Then
            Try
                Me.ddlSeikyuusakiKbn.SelectedValue = strSeikyuusakiKbn
            Catch ex As Exception
                Me.ddlSeikyuusakiKbn.SelectedValue = String.Empty
            End Try
        Else
            Me.ddlSeikyuusakiKbn.SelectedValue = String.Empty
        End If

    End Sub

    ''' <summary>請求先コードを設定する</summary> 
    Private Sub SetSeikyuusakiCd(ByVal strSeikyuusakiCd As String)
        '表示の値を設定する
        If Not strSeikyuusakiCd.Trim.Equals(String.Empty) Then
            Me.tbxSeikyuusakiCd.Text = strSeikyuusakiCd.Trim
        Else
            Me.tbxSeikyuusakiCd.Text = String.Empty
        End If
    End Sub

    ''' <summary>請求先枝番を設定する</summary> 
    Private Sub SetSeikyuusakiBrc(ByVal strSeikyuusakiBrc As String)
        '表示の値を設定する
        If Not strSeikyuusakiBrc.Trim.Equals(String.Empty) Then
            Me.tbxSeikyuusakiBrc.Text = strSeikyuusakiBrc.Trim
        Else
            Me.tbxSeikyuusakiBrc.Text = String.Empty
        End If
    End Sub

    ''' <summary>請求先名を設定する</summary> 
    Private Sub SetSeikyuusakiMei()
        '請求先区分
        Dim strSeikyuusakiKbn As String = Me.ddlSeikyuusakiKbn.SelectedValue.Trim
        '請求先コード
        Dim strSeikyuusakiCd As String = Me.tbxSeikyuusakiCd.Text.Trim
        '請求先枝番
        Dim strSeikyuusakiBrc As String = Me.tbxSeikyuusakiBrc.Text.Trim

        '請求先(区分・コード・枝番）ある場合
        If (Not strSeikyuusakiKbn.Trim.Equals(String.Empty)) AndAlso (Not strSeikyuusakiCd.Trim.Equals(String.Empty)) AndAlso (Not strSeikyuusakiBrc.Trim.Equals(String.Empty)) Then
            '請求先名を取得する
            Dim dtSeikyuusakiMei As New Data.DataTable
            dtSeikyuusakiMei = seikyuuSakiTyuuijikouLogic.GetSeikyuusakiMei(strSeikyuusakiKbn, strSeikyuusakiCd, strSeikyuusakiBrc)
            '請求先名の表示値を設定する
            If dtSeikyuusakiMei.Rows.Count > 0 Then
                Me.tbxSeikyuusakiMei.Text = dtSeikyuusakiMei.Rows(0).Item(0).ToString.Trim
            Else
                Me.tbxSeikyuusakiMei.Text = String.Empty
            End If
        Else
            Me.tbxSeikyuusakiMei.Text = String.Empty
        End If

    End Sub

    ''' <summary>種別コードを設定する</summary> 
    Private Sub SetSyubetuCd()

        '種別情報を取得する
        Dim dtSyubetu As New Data.DataTable
        dtSyubetu = seikyuuSakiTyuuijikouLogic.GetSyubetu()

        'データをbound
        Me.ddlSyubetuCd.DataSource = dtSyubetu
        Me.ddlSyubetuCd.DataValueField = "code"
        Me.ddlSyubetuCd.DataTextField = "meisyou"
        Me.ddlSyubetuCd.DataBind()

        '空白行
        Me.ddlSyubetuCd.Items.Insert(0, New ListItem(String.Empty, String.Empty))

        'デフォルト
        Me.ddlSyubetuCd.SelectedValue = String.Empty

    End Sub

    ''' <summary>重要度を設定する</summary> 
    Private Sub SetJyuuyoudo()
        '空白行
        Me.ddlJyuuyoudo.Items.Insert(0, New ListItem(String.Empty, String.Empty))
        '高
        Me.ddlJyuuyoudo.Items.Insert(1, New ListItem("高", "2"))
        '中
        Me.ddlJyuuyoudo.Items.Insert(2, New ListItem("中", "1"))
        '低
        Me.ddlJyuuyoudo.Items.Insert(3, New ListItem("低", "0"))

        'デフォルト
        Me.ddlJyuuyoudo.SelectedValue = String.Empty

    End Sub

    ''' <summary>請求締め日を設定する</summary> 
    Private Sub SetSeikyuuSimeDate()
        '表示の値を設定する
        Me.tbxSeikyuuSimeDate.Text = String.Empty
    End Sub

    ''' <summary>請求書必着日を設定する</summary> 
    Private Sub SetSeikyuusyoHittykDate()
        '表示の値を設定する
        Me.tbxSeikyuusyoHittykDate.Text = String.Empty
    End Sub

    ''' <summary>「新規登録」ボタンを設定する</summary>
    Private Function SetBtnTouroku() As Boolean
        '地盤画面共通クラス
        Dim jBn As New Jiban
        Dim user_info As New LoginUserInfo
        jBn.userAuth(user_info)

        Dim blnEnable As Boolean

        '経理業務権限取得
        Dim dtAccountTable As CommonSearchDataSet.AccountTableDataTable
        dtAccountTable = commonCheck.CheckKengen(user_info.AccountNo)
        If dtAccountTable.Rows.Count = 0 Then
            blnEnable = False
        ElseIf dtAccountTable.Rows(0).Item("keiri_gyoumu_kengen") = -1 Then
            blnEnable = True
        Else
            blnEnable = False
        End If

        '「新規登録」ボタン
        Me.btnTouroku.Visible = blnEnable

        Return blnEnable

    End Function

    ''' <summary>検索結果を設定</summary>
    Private Sub SetKensakuCount(ByVal intCount As Integer)

        If intCount.Equals(0) Then
            Me.lblCount.Text = "0"
            Me.lblCount.ForeColor = Drawing.Color.Black
            scrollHeight = intCount * 32 + 1
        Else
            If Me.ddlKensaKensuu.SelectedValue = "max" Then
                Me.lblCount.Text = "1-" & CStr(intCount)
                Me.lblCount.ForeColor = Drawing.Color.Black
                scrollHeight = intCount * 32 + 1
            Else
                If intCount > Convert.ToInt64(Me.ddlKensaKensuu.SelectedValue) Then
                    Me.lblCount.Text = "1-" & CStr(Me.ddlKensaKensuu.SelectedValue) & "/" & CStr(intCount)
                    Me.lblCount.ForeColor = Drawing.Color.Red
                    scrollHeight = Me.ddlKensaKensuu.SelectedValue * 32 + 1
                Else
                    Me.lblCount.Text = "1-" & CStr(intCount)
                    Me.lblCount.ForeColor = Drawing.Color.Black
                    scrollHeight = intCount * 32 + 1
                End If
            End If
        End If

    End Sub

    ''' <summary>｢検索実行｣ボタンをクリック時</summary>
    Private Sub btnKensakujiltukou_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKensakujiltukou.Click
        '入力チェック
        Dim strObjId As String = String.Empty
        Dim strErrMessage As String = CheckInput(strObjId)
        If strErrMessage <> String.Empty Then
            ShowMessage(strErrMessage, strObjId)
            Exit Sub
        End If

        '検索処理を行う
        Call Me.KensakuSyori()

    End Sub

    ''' <summary>CSV出力ボタンをクリック時</summary>
    Private Sub btnCsvOutput_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCsvOutput.Click
        '入力チェック
        Dim strObjId As String = String.Empty
        Dim strErrMessage As String = CheckInput(strObjId)
        If strErrMessage <> String.Empty Then
            ShowMessage(strErrMessage, strObjId)
            Exit Sub
        End If

        'パラメタを作成する
        Dim param As New Dictionary(Of String, String)
        param = SetParameters()

        '請求先名を設定する
        Call Me.SetSeikyuusakiMei()

        'データの件数を取得する
        Dim intCount As Integer
        intCount = seikyuuSakiTyuuijikouLogic.GetSeikyuuSakiTyuuijikouCount(param)

        '検索結果を設定
        Call Me.SetKensakuCount(intCount)

        '検索の明細データを設定する
        Call Me.SetKensakuMeisai(intCount, param, False)

        '
        ClientScript.RegisterStartupScript(Me.GetType, "CsvOutput", "<script language=javascript>document.getElementById('" & Me.btnCsvSyori.ClientID & "').click();</script>")

    End Sub

    ''' <summary>CSV出力処理</summary>
    Private Sub btnCsvSyori_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCsvSyori.Click

        'パラメタを作成する
        Dim param As New Dictionary(Of String, String)
        param = SetParameters()

        'データを取得する
        Dim dtSeikyuuSakiTyuuijikouCSV As New Data.DataTable
        dtSeikyuuSakiTyuuijikouCSV = seikyuuSakiTyuuijikouLogic.GetSeikyuuSakiTyuuijikouCSV(param)

        'CSVファイル名設定
        Dim strFileName As String = System.Configuration.ConfigurationManager.AppSettings("SeikyuuSakiTyuuijikouMasterCsv").ToString

        Response.Buffer = True
        Dim writer As New CsvWriter(Me.Response.OutputStream, Encoding.GetEncoding(932), ",", vbCrLf)

        'CSVファイルヘッダ設定
        writer.WriteLine(EarthConst.conSeikyuuSakiTyuuijikouCsvHeader)

        'CSVファイル内容設定
        For i As Integer = 0 To dtSeikyuuSakiTyuuijikouCSV.Rows.Count - 1
            With dtSeikyuuSakiTyuuijikouCSV.Rows(i)
                writer.WriteLine(.Item(0), .Item(1), .Item(2), .Item(3), .Item(4), .Item(5), .Item(6), Me.CutMaxLength(.Item(7), 128), .Item(8), .Item(9), _
                                 .Item(10), .Item(11), Me.SetTimeType(.Item(12)), .Item(13), Me.SetTimeType(.Item(14)))
            End With
        Next

        'CSVファイルダウンロード
        Response.Charset = "utf-8"
        Response.ContentType = "text/plain"
        Response.AddHeader("Content-Disposition", "attachment; filename=" & HttpUtility.UrlEncode(strFileName))
        Response.End()

    End Sub

    ''' <summary>gridview</summary>
    Private Sub grdMeisai_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdMeisai.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            '詳細
            Dim lblSyousai As WebControls.Label = CType(e.Row.Cells(5).Controls(1), WebControls.Label)
            lblSyousai.Text = Me.CutMaxLength(lblSyousai.Text.Trim, 128)

            If setFlg.Equals(True) Then
                '請求先区分
                Dim strSeikyuusakiKbn As String
                strSeikyuusakiKbn = e.Row.Cells(9).Text.Trim
                '請求先コード
                Dim strSeikyuusakiCd As String
                strSeikyuusakiCd = e.Row.Cells(10).Text.Trim
                '請求先枝番
                Dim strSeikyuusakiBrc As String
                strSeikyuusakiBrc = e.Row.Cells(11).Text.Trim
                'NO
                Dim strNo As String
                strNo = CType(e.Row.Cells(2).Controls(1), WebControls.Label).Text.Trim

                e.Row.Attributes.Add("ondblclick", "fncTourokuPopup('row','" & strSeikyuusakiKbn & "','" & strSeikyuusakiCd & "','" & strSeikyuusakiBrc & "','" & strNo & "');")
            End If
            e.Row.Cells(9).Style.Add("display", "none")
            e.Row.Cells(10).Style.Add("display", "none")
            e.Row.Cells(11).Style.Add("display", "none")
        End If
    End Sub

    ''' <summary>検索処理</summary>
    Private Sub KensakuSyori(Optional ByVal blnShowMessageFlg As Boolean = True)

        'パラメタを作成する
        Dim param As New Dictionary(Of String, String)
        param = SetParameters()

        'データの件数を取得する
        Dim intCount As Integer
        intCount = seikyuuSakiTyuuijikouLogic.GetSeikyuuSakiTyuuijikouCount(param)

        '検索結果を設定
        Call Me.SetKensakuCount(intCount)

        '検索の明細データを設定する
        Call Me.SetKensakuMeisai(intCount, param, blnShowMessageFlg)

        '請求先名を設定する
        Call Me.SetSeikyuusakiMei()

    End Sub

    ''' <summary>検索の明細データを設定する</summary>
    Private Sub SetKensakuMeisai(ByVal intCount As Integer, ByVal Param As Dictionary(Of String, String), Optional ByVal blnShowMessageFlg As Boolean = True)
        If intCount.Equals(0) Then
            '0件の場合
            If blnShowMessageFlg.Equals(True) Then
                'エラーメッセージを表示する
                Call Me.ShowMessage(Messages.Instance.MSG020E, String.Empty)
            End If
            '明細をクリアする
            Me.grdMeisai.DataSource = Nothing
            Me.grdMeisai.DataBind()

            'ソート順ボタンを設定する
            Call Me.SetSortButton(False)

        Else
            '該当データが存在する場合

            '請求先注意事項情報を取得する
            Dim dtSeikyuuSakiTyuuijikouInfo As New SeikyuuSakiTyuuijikouDataSet.SeikyuuSakiTyuuijikouInfoTableDataTable
            dtSeikyuuSakiTyuuijikouInfo = seikyuuSakiTyuuijikouLogic.GetSeikyuuSakiTyuuijikouInfo(Param)

            ViewState("dtSeikyuuSakiTyuuijikouInfo") = dtSeikyuuSakiTyuuijikouInfo

            '明細をクリアする
            Me.grdMeisai.DataSource = dtSeikyuuSakiTyuuijikouInfo
            Me.grdMeisai.DataBind()

            ViewState("scrollHeight") = scrollHeight

            'ソート順ボタンを設定する
            Call Me.SetSortButton(True)

            'ソート順ボタン色を設定する
            Call Me.SetSortButtonColor()

        End If
    End Sub

    ''' <summary>入力チェック</summary>
    Private Function CheckInput(ByRef strObjId As String) As String
        Dim csScript As New StringBuilder
        With csScript
            '請求先コード(半角英数字チェック)
            If Me.tbxSeikyuusakiCd.Text <> String.Empty Then
                .Append(commonCheck.ChkHankakuEisuuji(Me.tbxSeikyuusakiCd.Text, "請求先コード"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxSeikyuusakiCd.ClientID
                End If
            End If
            '請求先枝番(半角英数字チェック)
            If Me.tbxSeikyuusakiBrc.Text <> String.Empty Then
                .Append(commonCheck.ChkHankakuEisuuji(Me.tbxSeikyuusakiBrc.Text, "請求先枝番"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxSeikyuusakiBrc.ClientID
                End If
            End If
            '請求締め日(半角数字チェック)
            If Me.tbxSeikyuuSimeDate.Text <> String.Empty Then
                .Append(commonCheck.CheckHankaku(Me.tbxSeikyuuSimeDate.Text, "請求締め日", "1"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxSeikyuuSimeDate.ClientID
                End If
            End If
            '請求書必着日(半角数字チェック)
            If Me.tbxSeikyuusyoHittykDate.Text <> String.Empty Then
                .Append(commonCheck.CheckHankaku(Me.tbxSeikyuusyoHittykDate.Text, "請求書必着日", "1"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxSeikyuusyoHittykDate.ClientID
                End If
            End If
        End With
        Return csScript.ToString
    End Function

    ''' <summary>年月日を設定する</summary>
    Private Function SetTimeType(ByVal objTime As Object) As String
        If (Not IsDBNull(objTime)) AndAlso (Not objTime.ToString.Trim.Equals(String.Empty)) Then
            Return CDate(objTime).ToString("yyyy/MM/dd HH:mm:ss")
        Else
            Return String.Empty
        End If
    End Function

    ''' <summary>最大長を切り取る</summary>
    Public Function CutMaxLength(ByVal strValue As String, ByVal intMaxByteCount As Integer) As String

        Dim hEncoding As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")

        Dim intLengthCount As Integer = 0
        For i As Integer = strValue.Length To 0 Step -1
            Dim btBytes As Byte() = hEncoding.GetBytes(Left(strValue, i))
            If btBytes.LongLength <= intMaxByteCount Then
                intLengthCount = i
                Exit For
            End If
        Next

        Return Left(strValue, intLengthCount)
    End Function

    ''' <summary>画面一覧ヘッダーに並び順をクリック時</summary>
    Private Sub btnSort_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSeikyuusakiCdUp.Click, _
                                                                                           btnSeikyuusakiCdDown.Click, _
                                                                                           btnSeikyuusakiMeiUp.Click, _
                                                                                           btnSeikyuusakiMeiDown.Click, _
                                                                                           btnTorikesiUp.Click, _
                                                                                           btnTorikesiDown.Click, _
                                                                                           btnSyubetuCdUp.Click, _
                                                                                           btnSyubetuCdDown.Click, _
                                                                                           btnSyousaiUp.Click, _
                                                                                           btnSyousaiDown.Click, _
                                                                                           btnJyuuyoudoUp.Click, _
                                                                                           btnJyuuyoudoDown.Click, _
                                                                                           btnSeikyuuSimeDateUp.Click, _
                                                                                           btnSeikyuuSimeDateDown.Click, _
                                                                                           btnSeikyuusyoHittykDateUp.Click, _
                                                                                           btnSeikyuusyoHittykDateDown.Click
        Dim strSort As String = String.Empty
        'ソート順ボタン色を設定する
        Call SetSortButtonColor()

        '画面にソート順をクリック時
        Select Case CType(sender, LinkButton).ID
            Case btnSeikyuusakiCdUp.ID                                      '請求先コード▲
                strSort = "seikyuu_saki_kbn ASC,seikyuu_saki_cd ASC,seikyuu_saki_brc ASC"
                btnSeikyuusakiCdUp.ForeColor = Drawing.Color.IndianRed
            Case btnSeikyuusakiCdDown.ID                                    '請求先コード▼
                strSort = "seikyuu_saki_kbn DESC,seikyuu_saki_cd DESC,seikyuu_saki_brc DESC"
                btnSeikyuusakiCdDown.ForeColor = Drawing.Color.IndianRed
            Case btnSeikyuusakiMeiUp.ID                                     '請求先名▲
                strSort = "seikyuu_saki_mei ASC"
                btnSeikyuusakiMeiUp.ForeColor = Drawing.Color.IndianRed
            Case btnSeikyuusakiMeiDown.ID                                   '請求先名▼
                strSort = "seikyuu_saki_mei DESC"
                btnSeikyuusakiMeiDown.ForeColor = Drawing.Color.IndianRed
            Case btnTorikesiUp.ID                                           '取消▲
                strSort = "torikesi ASC"
                btnTorikesiUp.ForeColor = Drawing.Color.IndianRed
            Case btnTorikesiDown.ID                                         '取消▼
                strSort = "torikesi DESC"
                btnTorikesiDown.ForeColor = Drawing.Color.IndianRed
            Case btnSyubetuCdUp.ID                                          '種別▲
                strSort = "syubetu_cd ASC"
                btnSyubetuCdUp.ForeColor = Drawing.Color.IndianRed
            Case btnSyubetuCdDown.ID                                        '種別▼
                strSort = "syubetu_cd DESC"
                btnSyubetuCdDown.ForeColor = Drawing.Color.IndianRed
            Case btnSyousaiUp.ID                                            '詳細▲
                strSort = "syousai ASC"
                btnSyousaiUp.ForeColor = Drawing.Color.IndianRed
            Case btnSyousaiDown.ID                                          '詳細▼
                strSort = "syousai DESC"
                btnSyousaiDown.ForeColor = Drawing.Color.IndianRed
            Case btnJyuuyoudoUp.ID                                          '重要度▲
                strSort = "jyuyodo ASC"
                btnJyuuyoudoUp.ForeColor = Drawing.Color.IndianRed
            Case btnJyuuyoudoDown.ID                                        '重要度▼
                strSort = "jyuyodo DESC"
                btnJyuuyoudoDown.ForeColor = Drawing.Color.IndianRed
            Case btnSeikyuuSimeDateUp.ID                                    '請求締め日▲
                strSort = "seikyuu_sime_date ASC"
                btnSeikyuuSimeDateUp.ForeColor = Drawing.Color.IndianRed
            Case btnSeikyuuSimeDateDown.ID                                  '請求締め日▼
                strSort = "seikyuu_sime_date DESC"
                btnSeikyuuSimeDateDown.ForeColor = Drawing.Color.IndianRed
            Case btnSeikyuusyoHittykDateUp.ID                               '請求書必着日▲
                strSort = "seikyuusyo_hittyk_date ASC"
                btnSeikyuusyoHittykDateUp.ForeColor = Drawing.Color.IndianRed
            Case btnSeikyuusyoHittykDateDown.ID                             '請求書必着日▼
                strSort = "seikyuusyo_hittyk_date DESC"
                btnSeikyuusyoHittykDateDown.ForeColor = Drawing.Color.IndianRed
        End Select

        '画面データのソート順を設定する
        Dim dvSeikyuuSakiTyuuijikouInfo As Data.DataView = CType(ViewState("dtSeikyuuSakiTyuuijikouInfo"), Data.DataTable).DefaultView
        dvSeikyuuSakiTyuuijikouInfo.Sort = strSort

        Me.grdMeisai.DataSource = dvSeikyuuSakiTyuuijikouInfo
        Me.grdMeisai.DataBind()

        '画面縦スクロールを設定する
        scrollHeight = ViewState("scrollHeight")

    End Sub

    ''' <summary>ソート順ボタンを設定する</summary>
    Private Sub SetSortButton(ByVal blnFlg As Boolean)

        Me.btnSeikyuusakiCdUp.Visible = blnFlg            '請求先コード▲
        Me.btnSeikyuusakiCdDown.Visible = blnFlg          '請求先コード▼
        Me.btnSeikyuusakiMeiUp.Visible = blnFlg           '請求先名▲
        Me.btnSeikyuusakiMeiDown.Visible = blnFlg         '請求先名▼
        Me.btnTorikesiUp.Visible = blnFlg                 '取消▲
        Me.btnTorikesiDown.Visible = blnFlg               '取消▼
        Me.btnSyubetuCdUp.Visible = blnFlg                '種別▲
        Me.btnSyubetuCdDown.Visible = blnFlg              '種別▼
        Me.btnSyousaiUp.Visible = blnFlg                  '詳細▲
        Me.btnSyousaiDown.Visible = blnFlg                '詳細▼
        Me.btnJyuuyoudoUp.Visible = blnFlg                '重要度▲
        Me.btnJyuuyoudoDown.Visible = blnFlg              '重要度▼
        Me.btnSeikyuuSimeDateUp.Visible = blnFlg          '請求締め日▲
        Me.btnSeikyuuSimeDateDown.Visible = blnFlg        '請求締め日▼
        Me.btnSeikyuusyoHittykDateUp.Visible = blnFlg     '請求書必着日▲
        Me.btnSeikyuusyoHittykDateDown.Visible = blnFlg   '請求書必着日▼

        Me.divTorikesi.Visible = blnFlg
        Me.divJyuuyoudo.Visible = blnFlg
        Me.divSeikyuuSimeDate.Visible = blnFlg
        Me.divSeikyuusyoHittykDate.Visible = blnFlg

    End Sub

    ''' <summary>ソート順ボタン色を設定する</summary>
    Private Sub SetSortButtonColor()

        Me.btnSeikyuusakiCdUp.ForeColor = Drawing.Color.SkyBlue            '請求先コード▲
        Me.btnSeikyuusakiCdDown.ForeColor = Drawing.Color.SkyBlue          '請求先コード▼
        Me.btnSeikyuusakiMeiUp.ForeColor = Drawing.Color.SkyBlue           '請求先名▲
        Me.btnSeikyuusakiMeiDown.ForeColor = Drawing.Color.SkyBlue         '請求先名▼
        Me.btnTorikesiUp.ForeColor = Drawing.Color.SkyBlue                 '取消▲
        Me.btnTorikesiDown.ForeColor = Drawing.Color.SkyBlue               '取消▼
        Me.btnSyubetuCdUp.ForeColor = Drawing.Color.SkyBlue                '種別▲
        Me.btnSyubetuCdDown.ForeColor = Drawing.Color.SkyBlue              '種別▼
        Me.btnSyousaiUp.ForeColor = Drawing.Color.SkyBlue                  '詳細▲
        Me.btnSyousaiDown.ForeColor = Drawing.Color.SkyBlue                '詳細▼
        Me.btnJyuuyoudoUp.ForeColor = Drawing.Color.SkyBlue                '重要度▲
        Me.btnJyuuyoudoDown.ForeColor = Drawing.Color.SkyBlue              '重要度▼
        Me.btnSeikyuuSimeDateUp.ForeColor = Drawing.Color.SkyBlue          '請求締め日▲
        Me.btnSeikyuuSimeDateDown.ForeColor = Drawing.Color.SkyBlue        '請求締め日▼
        Me.btnSeikyuusyoHittykDateUp.ForeColor = Drawing.Color.SkyBlue     '請求書必着日▲
        Me.btnSeikyuusyoHittykDateDown.ForeColor = Drawing.Color.SkyBlue   '請求書必着日▼

    End Sub

    ''' <summary>パラメータを作成する</summary>
    Public Function SetParameters() As Dictionary(Of String, String)
        'パラメータ
        Dim param As New Dictionary(Of String, String)
        '検索上限件数
        param.Add("KensakuKensuu", Me.ddlKensaKensuu.SelectedValue.Trim)
        '請求先区分
        param.Add("SeikyuusakiKbn", Me.ddlSeikyuusakiKbn.SelectedValue.Trim)
        '請求先コード
        param.Add("SeikyuusakiCd", Me.tbxSeikyuusakiCd.Text.Trim)
        '請求先枝番
        param.Add("SeikyuusakiBrc", Me.tbxSeikyuusakiBrc.Text.Trim)
        '種別コード
        param.Add("SyubetuCd", Me.ddlSyubetuCd.SelectedValue.Trim)
        '重要度
        param.Add("Jyuuyoudo", Me.ddlJyuuyoudo.SelectedValue.Trim)
        '請求締め日
        param.Add("SeikyuuSimeDate", Me.tbxSeikyuuSimeDate.Text.Trim)
        '請求書必着日
        param.Add("SeikyuusyoHittykDate", Me.tbxSeikyuusyoHittykDate.Text.Trim)
        '取消は検索対象外
        param.Add("KensakuTaisyouGai", IIf(Me.chkKensakuTaisyouGai.Checked, "0", String.Empty))

        Return param
    End Function

    ''' <summary>エラーメッセージ表示</summary>
    Private Sub ShowMessage(ByVal strMessage As String, ByVal strObjId As String)
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("function window.onload() {")
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

    ''' <summary>DIV非表示</summary>
    Public Sub CloseCover()
        Dim csScript As New StringBuilder
        csScript.AppendFormat("fncClosecover();").ToString()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CloseCover", csScript.ToString, True)
    End Sub

    ''' <summary>JavaScriptを作成</summary>
    Protected Sub MakeJavaScript()
        Dim sbScript As New StringBuilder
        With sbScript
            .AppendLine("<script type='text/javascript' language='javascript'>")
            '「Close」ボタンの処理
            .AppendLine("   function fncClose()")
            .AppendLine("   {")
            .AppendLine("       window.close();")
            .AppendLine("       return false;")
            .AppendLine("   }")

            '「クリア」ボタンの処理
            .AppendLine("   function fncClear()")
            .AppendLine("   {")
            '請求先区分
            .AppendLine("       document.getElementById('" & Me.ddlSeikyuusakiKbn.ClientID & "').selectedIndex=0;")
            '請求先コード
            .AppendLine("       document.getElementById('" & Me.tbxSeikyuusakiCd.ClientID & "').innerText='';")
            '請求先枝番
            .AppendLine("       document.getElementById('" & Me.tbxSeikyuusakiBrc.ClientID & "').innerText='';")
            '請求先名
            .AppendLine("       document.getElementById('" & Me.tbxSeikyuusakiMei.ClientID & "').innerText='';")
            '種別コード
            .AppendLine("       document.getElementById('" & Me.ddlSyubetuCd.ClientID & "').selectedIndex=0;")
            '重要度
            .AppendLine("       document.getElementById('" & Me.ddlJyuuyoudo.ClientID & "').selectedIndex=0;")
            '請求締め日
            .AppendLine("       document.getElementById('" & Me.tbxSeikyuuSimeDate.ClientID & "').innerText='';")
            '請求書必着日
            .AppendLine("       document.getElementById('" & Me.tbxSeikyuusyoHittykDate.ClientID & "').innerText='';")
            '検索上限件数
            .AppendLine("       document.getElementById('" & Me.ddlKensaKensuu.ClientID & "').selectedIndex=1;")
            '取消は検索対象外
            .AppendLine("       document.getElementById('" & Me.chkKensakuTaisyouGai.ClientID & "').checked=true;")
            '請求先区分
            .AppendLine("       document.getElementById('" & Me.ddlSeikyuusakiKbn.ClientID & "').focus();")
            .AppendLine("   }")

            '入力チェック
            .AppendLine("	function fncNyuuryokuCheck(strKbn) ")
            .AppendLine("	{ ")
            '請求先コード
            .AppendLine("		var strSeikyuusakiCd = Trim(document.getElementById('" & Me.tbxSeikyuusakiCd.ClientID & "').value); ")
            '種別コード
            .AppendLine("		var strSyubetuCd = Trim(document.getElementById('" & Me.ddlSyubetuCd.ClientID & "').value); ")
            '重要度
            .AppendLine("		var strJyuuyoudo = Trim(document.getElementById('" & Me.ddlJyuuyoudo.ClientID & "').value); ")
            '請求締め日
            .AppendLine("		var strSeikyuuSimeDate = Trim(document.getElementById('" & Me.tbxSeikyuuSimeDate.ClientID & "').value); ")
            '請求書必着日
            .AppendLine("		var strSeikyuusyoHittykDate = Trim(document.getElementById('" & Me.tbxSeikyuusyoHittykDate.ClientID & "').value); ")
            .AppendLine("       if (strKbn == 'kensaku'){")
            '検索条件いずれか必須：請求先ｺｰﾄﾞ（区分・枝番は除く）、種別、重要度、請求締め日、請求書必着日
            .AppendLine("		    if((strSeikyuusakiCd=='')&&(strSyubetuCd=='')&&(strJyuuyoudo=='')&&(strSeikyuuSimeDate=='')&&(strSeikyuusyoHittykDate=='')) ")
            .AppendLine("		    { ")
            .AppendLine("			    alert('" & Messages.Instance.MSG2064E & "'); ")
            .AppendLine("			    document.getElementById('" & Me.tbxSeikyuusakiCd.ClientID & "').focus(); ")
            .AppendLine("			    return false;			 ")
            .AppendLine("		    } ")
            '確認メッセージ表示
            .AppendLine("           if (document.all." & Me.ddlKensaKensuu.ClientID & ".value=='max'){")
            .AppendLine("               if(!confirm('" & Messages.Instance.MSG007C & "')){")
            .AppendLine("                   return false; ")
            .AppendLine("               }")
            .AppendLine("           }")
            .AppendLine("       }")
            .AppendLine("       return true;")
            .AppendLine("	} ")

            '請求先の検索popup
            .AppendLine("	function fncSeikyuusakiPopup() ")
            .AppendLine("	{ ")
            .AppendLine("		window.open('search_Seikyuusaki.aspx?blnDelete=False&Kbn='+escape('請求先')+'&FormName=" & Me.Page.Form.Name & " &objKbn=" & Me.ddlSeikyuusakiKbn.ClientID & "&objCd=" & Me.tbxSeikyuusakiCd.ClientID & "&objBrc=" & Me.tbxSeikyuusakiBrc.ClientID & "&objMei=" & Me.tbxSeikyuusakiMei.ClientID & "&strKbn='+escape(document.getElementById('" & Me.ddlSeikyuusakiKbn.ClientID & "').value)+'&strCd='+escape(document.getElementById('" & Me.tbxSeikyuusakiCd.ClientID & "').value)+'&strBrc='+escape(document.getElementById('" & Me.tbxSeikyuusakiBrc.ClientID & "').value),'SeikyuusakiPopup','menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes'); ")
            .AppendLine("	} ")

            '請求先注意情報登録画面をpopup
            .AppendLine("	function fncTourokuPopup(strFlg,strKbn,strCd,strBrc,strNo) ")
            .AppendLine("	{ ")
            .AppendLine("       var sendSearchTerms; ")
            .AppendLine("       if(strFlg=='btn') ")
            .AppendLine("       { ")
            .AppendLine("           strKbn = Trim(document.getElementById('" & Me.ddlSeikyuusakiKbn.ClientID & "').value);")
            .AppendLine("           strCd = Trim(document.getElementById('" & Me.tbxSeikyuusakiCd.ClientID & "').value);")
            .AppendLine("           strBrc = Trim(document.getElementById('" & Me.tbxSeikyuusakiBrc.ClientID & "').value);")
            .AppendLine("           sendSearchTerms = Trim(strKbn)+'$$$'+Trim(strCd)+'$$$'+Trim(strBrc); ")
            .AppendLine("       } ")
            .AppendLine("       else ")
            .AppendLine("       { ")
            .AppendLine("           sendSearchTerms = Trim(strKbn)+'$$$'+Trim(strCd)+'$$$'+Trim(strBrc)+'$$$'+Trim(strNo); ")
            .AppendLine("       } ")
            .AppendLine("		window.open('SeikyuuSakiTyuuijikouInput.aspx?sendSearchTerms='+escape(sendSearchTerms),'TourokuPopup','top=0,left=0,width=1000,height=400,menubar=yes,toolbar=yes,location=yes,status=yes,resizable=yes,scrollbars=yes'); ")
            .AppendLine(" ")
            .AppendLine(" ")
            .AppendLine("	} ")

            'スクロールを設定する
            .AppendLine("function wheel(event){")
            .AppendLine("   var delta = 0;")
            .AppendLine("   if(!event)")
            .AppendLine("       event = window.event;")
            .AppendLine("   if(event.wheelDelta){")
            .AppendLine("       delta = event.wheelDelta/120;")
            .AppendLine("       if(window.opera)")
            .AppendLine("           delta = -delta;")
            .AppendLine("       }else if(event.detail){")
            .AppendLine("           delta = -event.detail/3;")
            .AppendLine("       }")
            .AppendLine("   if(delta)")
            .AppendLine("       handle(delta);")
            .AppendLine("}")
            .AppendLine("function handle(delta){")
            .AppendLine("   var divVscroll=" & divHiddenMeisaiV.ClientID & ";")
            .AppendLine("   if (delta < 0){")
            .AppendLine("       divVscroll.scrollTop = divVscroll.scrollTop + 15;")
            .AppendLine("   }else{")
            .AppendLine("       divVscroll.scrollTop = divVscroll.scrollTop - 15;")
            .AppendLine("   }")
            .AppendLine("}")
            .AppendLine("function fncScrollV(){")
            .AppendLine("   var divbody=" & Me.divMeisai.ClientID & ";")
            .AppendLine("   var divVscroll=" & Me.divHiddenMeisaiV.ClientID & ";")
            .AppendLine("   divbody.scrollTop = divVscroll.scrollTop;")
            .AppendLine("}")
            .AppendLine("   function fncSetHidCSV(){")
            .AppendLine("       document.getElementById('" & Me.hidCSVFlg.ClientID & "').value='';")
            .AppendLine("   }")

            'DIV表示
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

            'DIV非表示
            .AppendLine("   function fncClosecover(){")
            .AppendLine("       var buyDiv=document.getElementById('" & Me.buySelName.ClientID & "');")
            .AppendLine("       var disable=document.getElementById('" & Me.disableDiv.ClientID & "');")
            .AppendLine("       buyDiv.style.display='none';")
            .AppendLine("       disable.style.display='none';")
            .AppendLine("   }")
            .AppendLine("	function LTrim(str)  ")
            .AppendLine("	{  ")
            .AppendLine("		var i; ")
            .AppendLine("		for(i=0;i<str.length;i++)  ")
            .AppendLine("		{  ")
            .AppendLine("			if(str.charAt(i)!=' '&&str.charAt(i)!='　')break;  ")
            .AppendLine("		}  ")
            .AppendLine("		str=str.substring(i,str.length);  ")
            .AppendLine("		return str;  ")
            .AppendLine("	}  ")
            .AppendLine("	function RTrim(str)  ")
            .AppendLine("	{  ")
            .AppendLine("		var i;  ")
            .AppendLine("		for(i=str.length-1;i>=0;i--)  ")
            .AppendLine("		{  ")
            .AppendLine("			if(str.charAt(i)!=' '&&str.charAt(i)!='　')break;  ")
            .AppendLine("		}  ")
            .AppendLine("		str=str.substring(0,i+1);  ")
            .AppendLine("		return str;  ")
            .AppendLine("	} ")
            .AppendLine("	function Trim(str)  ")
            .AppendLine("	{  ")
            .AppendLine("		return LTrim(RTrim(str));  ")
            .AppendLine("	}  ")
            .AppendLine("	 ")
            .AppendLine("</script>")
        End With
        Page.ClientScript.RegisterStartupScript(Page.GetType, "JS_" & Me.ClientID, sbScript.ToString)
    End Sub

End Class
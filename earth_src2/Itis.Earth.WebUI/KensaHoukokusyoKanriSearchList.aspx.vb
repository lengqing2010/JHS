Imports Itis.Earth.BizLogic
Imports Itis.Earth.DataAccess

''' <summary>検査報告書管理</summary>
''' <history>
''' <para>2015/12/07　高兵兵(大連)　新規作成</para>
''' </history>
Partial Public Class KensaHoukokusyoKanriSearchList
    Inherits System.Web.UI.Page

    Private KensaLogic As New KensaHoukokusyoKanriSearchListLogic
    Private commonCheck As New CommonCheck
    Protected scrollHeight As Integer = 0

    Public gridviewRightId As String = ""
    Public tblRightId As String = ""
    Public tblLeftId As String = ""

    ''' <summary>
    ''' ページロッド
    ''' </summary>
    ''' <param name="sender">system.Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2015/12/07　高兵兵(大連)　新規作成</para>
    ''' </history>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'javascript作成
        Call Me.MakeJavascript()

        If Not IsPostBack Then
            hidCsvFlg.Value = ""

            '検索結果
            Me.lblCount.Text = String.Empty

            'リンクボタンの表示を設定
            Call Me.SetUpDownHyouji(False)

            Me.tbxKakunouDateFrom.Focus()
        End If

        '｢閉じる｣ボタン処理
        Me.btnClose.Attributes.Add("onClick", "return fncClose();")
        '｢クリア｣ボタン処理
        Me.btnClear.Attributes.Add("onclick", "fncClear();return false;")
        '検索実行ボタン処理
        Me.btnKensakujiltukou.Attributes.Add("onclick", ";if(document.getElementById('" & Me.ddlKensakuJyouken.ClientID & "').value=='max'){if(!confirm('検索上限件数に「無制限」が選択されています。\n画面表示に時間が掛かる可能性がありますが、実行してよろしいですか？')){return false;}else{return true;}}else{return true;};")
        '選択物件一括セットセット
        Me.btnSetSend.Attributes.Add("onClick", "if(fncSetSend()==true){}else{return false}")
        '格納日From
        Me.tbxKakunouDateFrom.Attributes.Add("onBlur", "checkDate(this);")
        '格納日To
        Me.tbxKakunouDateTo.Attributes.Add("onBlur", "checkDate(this);")
        '発送日From
        Me.tbxSendDateFrom.Attributes.Add("onBlur", "checkDate(this);")
        '発送日To
        Me.tbxSendDateTo.Attributes.Add("onBlur", "checkDate(this);")
        '番号From
        tbxNoFrom.Attributes.Add("onBlur", "chkHankakuSuuji(this);")
        '番号To
        tbxNoTo.Attributes.Add("onBlur", "chkHankakuSuuji(this);")
        '一括セット発送日
        tbxSetSendDate.Attributes.Add("onBlur", "checkDate(this);")
        '画面縦スクロールを設定する
        scrollHeight = ViewState("scrollHeight")
        '加盟店名
        Me.tbxKameitenMeiFrom.Attributes.Add("readOnly", "true")
        Me.tbxKameitenMeiFrom.Attributes.Add("onkeydown", "return false;")
        If Me.tbxKameitenCdFrom.Text.Trim.Equals(String.Empty) Then
            Me.tbxKameitenMeiFrom.Text = String.Empty
        End If
    End Sub

    ''' <summary>
    ''' 検索実行ボタン処理
    ''' </summary>
    ''' <param name="sender">system.Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>   
    ''' <history>
    ''' <para>2015/12/07　高兵兵(大連)　新規作成</para>
    ''' </history> 
    Protected Sub btnKensakujiltukou_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKensakujiltukou.Click
        '入力チェック
        Dim strObjId As String = String.Empty
        Dim strErrMessage As String = CheckInput(strObjId, "0")
        If strErrMessage <> String.Empty Then
            ShowMessage(strErrMessage, strObjId)
            Exit Sub
        End If

        Call Kensakuzikkou()

    End Sub

    ''' <summary>
    ''' 選択物件一括セットセットボタン処理
    ''' </summary>
    ''' <param name="sender">system.Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2015/12/07　高兵兵(大連)　新規作成</para>
    ''' </history>
    Protected Sub btnSetSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetSend.Click

        Dim strReturn As String = ""
        Dim strErr As String = ""
        Dim strID As String = ""
        Dim strHenkou As String = ""
        Dim strDisplayName As String = ""
        Dim intCount As Int64 = 0
        '検査報告書管理テーブル
        Dim dtKensa As Data.DataTable = Me.CreateKensaHkksKanriTable()
        Dim drKensa As Data.DataRow
        intCount = Me.grdBodyLeft.Rows.Count
        dtKensa.Clear()
        '入力チェック
        Dim strObjId As String = String.Empty
        Dim strErrMessage As String = CheckInput(strObjId, "1")
        If strErrMessage <> String.Empty Then
            ShowMessage(strErrMessage, strObjId)
            Exit Sub
        End If
        If intCount > 0 Then
            For i As Int64 = 0 To intCount - 1
                If CType(Me.grdBodyLeft.Rows(i).Cells(0).FindControl("chkKensakuTaisyouGai"), CheckBox).Checked = True Then
                    drKensa = dtKensa.NewRow
                    '管理No
                    drKensa.Item("kanri_no") = CType(grdBodyLeft.Rows(i).Cells(1).FindControl("lblKanrino"), Label).Text
                    '区分
                    drKensa.Item("kbn") = CType(grdBodyLeft.Rows(i).Cells(2).FindControl("lblkbn"), Label).Text
                    '保証書NO
                    drKensa.Item("hosyousyo_no") = CType(grdBodyLeft.Rows(i).Cells(3).FindControl("lblHosyousyono"), Label).Text
                    '加盟店コード
                    drKensa.Item("kameiten_cd") = CType(grdBodyLeft.Rows(i).Cells(4).FindControl("lblkameitencd"), Label).Text
                    dtKensa.Rows.Add(drKensa)
                End If
            Next
        End If

        '更新処理
        If dtKensa.Rows.Count > 0 Then
            strReturn = KensaLogic.UpdKensahoukokusho(tbxSetSendDate.Text.Trim(), tbxTantousya.Text.Trim(), dtKensa)
        End If

        If strReturn = "True" Then
            '更新成功
            strErr = "alert('" & Replace(Messages.Instance.MSG018S, "@PARAM1", "検査報告書管理") & "');"
            Call Kensakuzikkou()
        ElseIf strReturn = "false" Then
            '更新失敗
            strErr = "alert('" & Replace(Messages.Instance.MSG019E, "@PARAM1", "検査報告書管理") & "');"
        End If
        'メッセージ表示
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strErr, True)

    End Sub

    ''' <summary>
    ''' 取消セットボタン処理
    ''' </summary>
    ''' <param name="sender">system.Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2015/12/07　高兵兵(大連)　新規作成</para>
    ''' </history>
    Protected Sub btnTorikesi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTorikesi.Click
        Dim strReturn As String = ""
        Dim strErr As String = ""
        Dim strID As String = ""
        Dim strHenkou As String = ""
        Dim strDisplayName As String = ""
        Dim intCount As Int64 = 0
        '検査報告書管理テーブル
        Dim dtKensa As Data.DataTable = Me.CreateKensaHkksKanriTable()
        Dim drKensa As Data.DataRow
        intCount = Me.grdBodyLeft.Rows.Count
        dtKensa.Clear()
        If intCount > 0 Then
            '登録情報を整理する
            For i As Int64 = 0 To intCount - 1
                If CType(Me.grdBodyLeft.Rows(i).Cells(0).FindControl("chkKensakuTaisyouGai"), CheckBox).Checked = True Then
                    drKensa = dtKensa.NewRow
                    '管理No
                    drKensa.Item("kanri_no") = CType(grdBodyLeft.Rows(i).Cells(1).FindControl("lblKanrino"), Label).Text
                    '区分
                    drKensa.Item("kbn") = CType(grdBodyLeft.Rows(i).Cells(2).FindControl("lblkbn"), Label).Text
                    '保証書NO
                    drKensa.Item("hosyousyo_no") = CType(grdBodyLeft.Rows(i).Cells(3).FindControl("lblHosyousyono"), Label).Text
                    '加盟店コード
                    drKensa.Item("kameiten_cd") = CType(grdBodyLeft.Rows(i).Cells(4).FindControl("lblkameitencd"), Label).Text
                    dtKensa.Rows.Add(drKensa)
                End If
            Next
        End If
        '更新処理
        If dtKensa.Rows.Count > 0 Then
            strReturn = KensaLogic.UpdKensahoukokushoTorikesi(dtKensa)
        Else
            strReturn = "3"
        End If
        If strReturn = "True" Then
            '更新成功
            strErr = "alert('" & Replace(Messages.Instance.MSG018S, "@PARAM1", "検査報告書管理") & "');"
            Call Kensakuzikkou()
        ElseIf strReturn = "false" Then
            '更新失敗
            strErr = "alert('" & Replace(Messages.Instance.MSG019E, "@PARAM1", "検査報告書管理") & "');"
        Else
            strErr = "alert('" & Replace(Messages.Instance.MSG038E, "@PARAM1", "対象") & "');"
            If intCount > 0 Then
                CType(Me.grdBodyLeft.Rows(0).Cells(0).FindControl("chkKensakuTaisyouGai"), CheckBox).Focus()
            End If
        End If
        'メッセージ表示
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strErr, True)
    End Sub

    ''' <summary>
    ''' <summary>CSV出力ボタンをクリック時</summary>
    ''' </summary>
    ''' <param name="sender">system.Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2015/12/07　高兵兵(大連)　新規作成</para>
    ''' </history>
    Private Sub btnCsvOutput_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCsvOutput.Click
        '入力チェック
        Dim strObjId As String = String.Empty
        Dim strErrMessage As String = CheckInput(strObjId, "0")
        If strErrMessage <> String.Empty Then
            ShowMessage(strErrMessage, strObjId)
            Exit Sub
        End If

        '検索件数
        Dim intCount As Long
        'CSV出力上限件数
        Dim intCsvMax As Integer = CInt(System.Configuration.ConfigurationManager.AppSettings("CsvDownMax"))

        '加盟店名を設定
        Call Me.SetkameitenCd(tbxKameitenCdFrom.Text)
        '検査報告書管理テーブルを取得
        Dim dtKensaHkksKanri As New DataTable
        dtKensaHkksKanri = KensaLogic.GetKensaHoukokusyoKanriSearch(Me.tbxKakunouDateFrom.Text.Trim, _
                                                              Me.tbxKakunouDateTo.Text.Trim, _
                                                              Me.tbxSendDateFrom.Text.Trim, _
                                                              Me.tbxSendDateTo.Text.Trim, _
                                                              Me.ddlKbn.SelectedValue.Trim, _
                                                              Me.tbxNoFrom.Text.Trim, _
                                                              Me.tbxNoTo.Text.Trim, _
                                                              Me.tbxKameitenCdFrom.Text.Trim, _
                                                              Me.ddlKensakuJyouken.SelectedValue.Trim, _
                                                              Me.chkKensakuTaisyouGai.Checked, _
                                                              Me.chkSendDateTaisyouGai.Checked)

        If dtKensaHkksKanri.Rows.Count > 0 Then

            ViewState("dtKensaHkksKanri") = dtKensaHkksKanri

            ViewState("dtKensaHkksKanri") = dtKensaHkksKanri
            Me.grdBodyLeft.DataSource = ViewState("dtKensaHkksKanri")
            Me.grdBodyLeft.DataBind()

            Me.grdBodyRight.DataSource = ViewState("dtKensaHkksKanri")
            Me.grdBodyRight.DataBind()
            'リンクボタンの表示を設定
            Call Me.SetUpDownHyouji(True)

            'リンクボタンの色を設定
            Call Me.setUpDownColor()

            ''検索結果を設定
            Dim intKenkasaKensuu As Integer = Me.SetKensakuResult(True)

            '検索結果を設定
            intCount = Me.SetKensakuResult(True)

        Else
            ViewState("dtKensaHkksKanri") = Nothing

            Me.grdBodyLeft.DataSource = Nothing
            Me.grdBodyLeft.DataBind()

            Me.grdBodyRight.DataSource = Nothing
            Me.grdBodyRight.DataBind()
            'リンクボタンの表示を設定
            Call Me.SetUpDownHyouji(False)

            '検索結果を設定
            intCount = Me.SetKensakuResult(False)
        End If

        ViewState("scrollHeight") = scrollHeight

        If intCount > intCsvMax Then
            strErrMessage = Messages.Instance.MSG051E.Replace("@PARAM1", intCsvMax)
            ShowMessage(strErrMessage, String.Empty)
        Else
            Me.hidCsvOut.Value = "1"
            ClientScript.RegisterStartupScript(Me.GetType, "", "<script language=javascript>document.getElementById('" & Me.btnCsv.ClientID & "').click();</script>")
        End If
    End Sub
    Private Sub btnCsv_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCsv.Click
        'CSV出力
        Call CsvOutPut()
    End Sub

    ''' <summary>画面タイトルに並び順をクリック時</summary>
    Private Sub btnSort_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKanrinoUp.Click, _
                                                                                            btnKanrinoDown.Click, _
                                                                                            btnKbnUp.Click, _
                                                                                            btnKbnDown.Click, _
                                                                                            btnHosyousyonoUp.Click, _
                                                                                            btnHosyousyonoDown.Click, _
                                                                                            btnKameitencdUp.Click, _
                                                                                            btnKameitencdDown.Click, _
                                                                                            btnKakunoudateUp.Click, _
                                                                                            btnKakunoudateDown.Click, _
                                                                                            btnKameitenmeiUp.Click, _
                                                                                            btnKameitenmeiDown.Click, _
                                                                                            btnSesyumeiUp.Click, _
                                                                                            btnSesyumeiDown.Click, _
                                                                                            btnTorikesiUp.Click, _
                                                                                            btnTorikesiDown.Click, _
                                                                                            btnKensahkksbusuuUp.Click, _
                                                                                            btnKensahkksbusuuDown.Click, _
                                                                                            btnKensahkksjyuusyo1Up.Click, _
                                                                                            btnKensahkksjyuusyo1Down.Click, _
                                                                                            btnKensahkksjyuusyo2Up.Click, _
                                                                                            btnKensahkksjyuusyo2Down.Click, _
                                                                                            btnYuubinnoUp.Click, _
                                                                                            btnYuubinnoDown.Click, _
                                                                                            btnTelnoUp.Click, _
                                                                                            btnTelnoDown.Click, _
                                                                                            btnBusyomeiUp.Click, _
                                                                                            btnBusyomeiDown.Click, _
                                                                                            btnTodouhukencdUp.Click, _
                                                                                            btnTodouhukencdDown.Click, _
                                                                                            btnTodouhukenmeiUp.Click, _
                                                                                            btnTodouhukenmeiDown.Click, _
                                                                                            btnHassoudateUp.Click, _
                                                                                            btnHassoudateDown.Click, _
                                                                                            btnHassoudateinflgUp.Click, _
                                                                                            btnHassoudateinflgDown.Click, _
                                                                                            btnSouhutantousyaUp.Click, _
                                                                                            btnSouhutantousyaDown.Click, _
                                                                                            btnBukkenjyuusyo1Up.Click, _
                                                                                            btnBukkenjyuusyo1Down.Click, _
                                                                                            btnBukkenjyuusyo2Up.Click, _
                                                                                            btnBukkenjyuusyo2Down.Click, _
                                                                                            btnBukkenjyuusyo3Up.Click, _
                                                                                            btnBukkenjyuusyo3Down.Click, _
                                                                                            btnTatemonokouzouUp.Click, _
                                                                                            btnTatemonokouzouDown.Click, _
                                                                                            btnTatemonokaisuUp.Click, _
                                                                                            btnTatemonokaisuDown.Click, _
                                                                                            btnFcnmUp.Click, _
                                                                                            btnFcnmDown.Click, _
                                                                                            btnKameitentantoUp.Click, _
                                                                                            btnKameitentantoDown.Click, _
                                                                                            btnTatemonokameitencdUp.Click, _
                                                                                            btnTatemonokameitencdDown.Click, _
                                                                                            btnKanrihyououtflgUp.Click, _
                                                                                            btnKanrihyououtflgDown.Click, _
                                                                                            btnKanrihyououtdateUp.Click, _
                                                                                            btnKanrihyououtdateDown.Click, _
                                                                                            btnSouhujyououtflgUp.Click, _
                                                                                            btnSouhujyououtflgDown.Click, _
                                                                                            btnSouhujyououtdateUp.Click, _
                                                                                            btnSouhujyououtdateDown.Click, _
                                                                                            btnKensahkksoutflgUp.Click, _
                                                                                            btnKensahkksoutflgDown.Click, _
                                                                                            btnKensahkksoutdateUp.Click, _
                                                                                            btnKensahkksoutdateDown.Click, _
                                                                                            btnTooshinoUp.Click, _
                                                                                            btnTooshinoDown.Click, _
                                                                                            btnKensakouteinm1Up.Click, _
                                                                                            btnKensakouteinm1Down.Click, _
                                                                                            btnKensakouteinm2Up.Click, _
                                                                                            btnKensakouteinm2Down.Click, _
                                                                                            btnKensakouteinm3Up.Click, _
                                                                                            btnKensakouteinm3Down.Click, _
                                                                                            btnKensakouteinm4Up.Click, _
                                                                                            btnKensakouteinm4Down.Click, _
                                                                                            btnKensakouteinm5Up.Click, _
                                                                                            btnKensakouteinm5Down.Click, _
                                                                                            btnKensakouteinm6Up.Click, _
                                                                                            btnKensakouteinm6Down.Click, _
                                                                                            btnKensakouteinm7Up.Click, _
                                                                                            btnKensakouteinm7Down.Click, _
                                                                                            btnKensakouteinm8Up.Click, _
                                                                                            btnKensakouteinm8Down.Click, _
                                                                                            btnKensakouteinm9Up.Click, _
                                                                                            btnKensakouteinm9Down.Click, _
                                                                                            btnKensakouteinm10Up.Click, _
                                                                                            btnKensakouteinm10Down.Click, _
                                                                                            btnKensastartjissibi1Up.Click, _
                                                                                            btnKensastartjissibi1Down.Click, _
                                                                                            btnKensastartjissibi2Up.Click, _
                                                                                            btnKensastartjissibi2Down.Click, _
                                                                                            btnKensastartjissibi3Up.Click, _
                                                                                            btnKensastartjissibi3Down.Click, _
                                                                                            btnKensastartjissibi4Up.Click, _
                                                                                            btnKensastartjissibi4Down.Click, _
                                                                                            btnKensastartjissibi5Up.Click, _
                                                                                            btnKensastartjissibi5Down.Click, _
                                                                                            btnKensastartjissibi6Up.Click, _
                                                                                            btnKensastartjissibi6Down.Click, _
                                                                                            btnKensastartjissibi7Up.Click, _
                                                                                            btnKensastartjissibi7Down.Click, _
                                                                                            btnKensastartjissibi8Up.Click, _
                                                                                            btnKensastartjissibi8Down.Click, _
                                                                                            btnKensastartjissibi9Up.Click, _
                                                                                            btnKensastartjissibi9Down.Click, _
                                                                                            btnKensastartjissibi10Up.Click, _
                                                                                            btnKensastartjissibi10Down.Click, _
                                                                                            btnKensainnm1Up.Click, _
                                                                                            btnKensainnm1Down.Click, _
                                                                                            btnKensainnm2Up.Click, _
                                                                                            btnKensainnm2Down.Click, _
                                                                                            btnKensainnm3Up.Click, _
                                                                                            btnKensainnm3Down.Click, _
                                                                                            btnKensainnm4Up.Click, _
                                                                                            btnKensainnm4Down.Click, _
                                                                                            btnKensainnm5Up.Click, _
                                                                                            btnKensainnm5Down.Click, _
                                                                                            btnKensainnm6Up.Click, _
                                                                                            btnKensainnm6Down.Click, _
                                                                                            btnKensainnm7Up.Click, _
                                                                                            btnKensainnm7Down.Click, _
                                                                                            btnKensainnm8Up.Click, _
                                                                                            btnKensainnm8Down.Click, _
                                                                                            btnKensainnm9Up.Click, _
                                                                                            btnKensainnm9Down.Click, _
                                                                                            btnKensainnm10Up.Click, _
                                                                                            btnKensainnm10Down.Click, _
                                                                                            btnAddloginuseridUp.Click, _
                                                                                            btnAddloginuseridDown.Click, _
                                                                                            btnAdddatetimeUp.Click, _
                                                                                            btnAdddatetimeDown.Click, _
                                                                                            btnUpdloginuseridUp.Click, _
                                                                                            btnUpdloginuseridDown.Click, _
                                                                                            btnUpddatetimeUp.Click, _
                                                                                            btnUpddatetimeDown.Click





        Dim strSort As String = String.Empty
        Dim strUpDown As String = String.Empty

        Call Me.setUpDownColor()

        '画面にソート順をクリック時
        Select Case CType(sender, LinkButton).ID
            Case btnKanrinoUp.ID
                strSort = "kanri_no"
                strUpDown = "ASC"
                btnKanrinoUp.ForeColor = Drawing.Color.IndianRed
            Case btnKanrinoDown.ID
                strSort = "kanri_no"
                strUpDown = "DESC"
                btnKanrinoDown.ForeColor = Drawing.Color.IndianRed
            Case btnKbnUp.ID
                strSort = "kbn"
                strUpDown = "ASC"
                btnKbnUp.ForeColor = Drawing.Color.IndianRed
            Case btnKbnDown.ID
                strSort = "kbn"
                strUpDown = "DESC"
                btnKbnDown.ForeColor = Drawing.Color.IndianRed
            Case btnHosyousyonoUp.ID
                strSort = "hosyousyo_no"
                strUpDown = "ASC"
                btnHosyousyonoUp.ForeColor = Drawing.Color.IndianRed
            Case btnHosyousyonoDown.ID
                strSort = "hosyousyo_no"
                strUpDown = "DESC"
                btnHosyousyonoDown.ForeColor = Drawing.Color.IndianRed
            Case btnKameitencdUp.ID
                strSort = "kameiten_cd"
                strUpDown = "ASC"
                btnKameitencdUp.ForeColor = Drawing.Color.IndianRed
            Case btnKameitencdDown.ID
                strSort = "kameiten_cd"
                strUpDown = "DESC"
                btnKameitencdDown.ForeColor = Drawing.Color.IndianRed
            Case btnKakunoudateUp.ID
                strSort = "kakunou_date"
                strUpDown = "ASC"
                btnKakunoudateUp.ForeColor = Drawing.Color.IndianRed
            Case btnKakunoudateDown.ID
                strSort = "kakunou_date"
                strUpDown = "DESC"
                btnKakunoudateDown.ForeColor = Drawing.Color.IndianRed
            Case btnKameitenmeiUp.ID
                strSort = "kameiten_mei"
                strUpDown = "ASC"
                btnKameitenmeiUp.ForeColor = Drawing.Color.IndianRed
            Case btnKameitenmeiDown.ID
                strSort = "kameiten_mei"
                strUpDown = "DESC"
                btnKameitenmeiDown.ForeColor = Drawing.Color.IndianRed
            Case btnSesyumeiUp.ID
                strSort = "sesyu_mei"
                strUpDown = "ASC"
                btnSesyumeiUp.ForeColor = Drawing.Color.IndianRed
            Case btnSesyumeiDown.ID
                strSort = "sesyu_mei"
                strUpDown = "DESC"
                btnSesyumeiDown.ForeColor = Drawing.Color.IndianRed
            Case btnTorikesiUp.ID
                strSort = "torikesi"
                strUpDown = "ASC"
                btnTorikesiUp.ForeColor = Drawing.Color.IndianRed
            Case btnTorikesiDown.ID
                strSort = "torikesi"
                strUpDown = "DESC"
                btnTorikesiDown.ForeColor = Drawing.Color.IndianRed
            Case btnKensahkksbusuuUp.ID
                strSort = "kensa_hkks_busuu"
                strUpDown = "ASC"
                btnKensahkksbusuuUp.ForeColor = Drawing.Color.IndianRed
            Case btnKensahkksbusuuDown.ID
                strSort = "kensa_hkks_busuu"
                strUpDown = "DESC"
                btnKensahkksbusuuDown.ForeColor = Drawing.Color.IndianRed
            Case btnKensahkksjyuusyo1Up.ID
                strSort = "kensa_hkks_jyuusyo1"
                strUpDown = "ASC"
                btnKensahkksjyuusyo1Up.ForeColor = Drawing.Color.IndianRed
            Case btnKensahkksjyuusyo1Down.ID
                strSort = "kensa_hkks_jyuusyo1"
                strUpDown = "DESC"
                btnKensahkksjyuusyo1Down.ForeColor = Drawing.Color.IndianRed
            Case btnKensahkksjyuusyo2Up.ID
                strSort = "kensa_hkks_jyuusyo2"
                strUpDown = "ASC"
                btnKensahkksjyuusyo2Up.ForeColor = Drawing.Color.IndianRed
            Case btnKensahkksjyuusyo2Down.ID
                strSort = "kensa_hkks_jyuusyo2"
                strUpDown = "DESC"
                btnKensahkksjyuusyo2Down.ForeColor = Drawing.Color.IndianRed
            Case btnYuubinnoUp.ID
                strSort = "yuubin_no"
                strUpDown = "ASC"
                btnYuubinnoUp.ForeColor = Drawing.Color.IndianRed
            Case btnYuubinnoDown.ID
                strSort = "yuubin_no"
                strUpDown = "DESC"
                btnYuubinnoDown.ForeColor = Drawing.Color.IndianRed
            Case btnTelnoUp.ID
                strSort = "tel_no"
                strUpDown = "ASC"
                btnTelnoUp.ForeColor = Drawing.Color.IndianRed
            Case btnTelnoDown.ID
                strSort = "tel_no"
                strUpDown = "DESC"
                btnTelnoDown.ForeColor = Drawing.Color.IndianRed
            Case btnBusyomeiUp.ID
                strSort = "busyo_mei"
                strUpDown = "ASC"
                btnBusyomeiUp.ForeColor = Drawing.Color.IndianRed
            Case btnBusyomeiDown.ID
                strSort = "busyo_mei"
                strUpDown = "DESC"
                btnBusyomeiDown.ForeColor = Drawing.Color.IndianRed
            Case btnTodouhukencdUp.ID
                strSort = "todouhuken_cd"
                strUpDown = "ASC"
                btnTodouhukencdUp.ForeColor = Drawing.Color.IndianRed
            Case btnTodouhukencdDown.ID
                strSort = "todouhuken_cd"
                strUpDown = "DESC"
                btnTodouhukencdDown.ForeColor = Drawing.Color.IndianRed
            Case btnTodouhukenmeiUp.ID
                strSort = "todouhuken_mei"
                strUpDown = "ASC"
                btnTodouhukenmeiUp.ForeColor = Drawing.Color.IndianRed
            Case btnTodouhukenmeiDown.ID
                strSort = "todouhuken_mei"
                strUpDown = "DESC"
                btnTodouhukenmeiDown.ForeColor = Drawing.Color.IndianRed
            Case btnHassoudateUp.ID
                strSort = "hassou_date"
                strUpDown = "ASC"
                btnHassoudateUp.ForeColor = Drawing.Color.IndianRed
            Case btnHassoudateDown.ID
                strSort = "hassou_date"
                strUpDown = "DESC"
                btnHassoudateDown.ForeColor = Drawing.Color.IndianRed
            Case btnHassoudateinflgUp.ID
                strSort = "hassou_date_in_flg"
                strUpDown = "ASC"
                btnHassoudateinflgUp.ForeColor = Drawing.Color.IndianRed
            Case btnHassoudateinflgDown.ID
                strSort = "hassou_date_in_flg"
                strUpDown = "DESC"
                btnHassoudateinflgDown.ForeColor = Drawing.Color.IndianRed
            Case btnSouhutantousyaUp.ID
                strSort = "souhu_tantousya"
                strUpDown = "ASC"
                btnSouhutantousyaUp.ForeColor = Drawing.Color.IndianRed
            Case btnSouhutantousyaDown.ID
                strSort = "souhu_tantousya"
                strUpDown = "DESC"
                btnSouhutantousyaDown.ForeColor = Drawing.Color.IndianRed
            Case btnBukkenjyuusyo1Up.ID
                strSort = "bukken_jyuusyo1"
                strUpDown = "ASC"
                btnBukkenjyuusyo1Up.ForeColor = Drawing.Color.IndianRed
            Case btnBukkenjyuusyo1Down.ID
                strSort = "bukken_jyuusyo1"
                strUpDown = "DESC"
                btnBukkenjyuusyo1Down.ForeColor = Drawing.Color.IndianRed
            Case btnBukkenjyuusyo2Up.ID
                strSort = "bukken_jyuusyo2"
                strUpDown = "ASC"
                btnBukkenjyuusyo2Up.ForeColor = Drawing.Color.IndianRed
            Case btnBukkenjyuusyo2Down.ID
                strSort = "bukken_jyuusyo2"
                strUpDown = "DESC"
                btnBukkenjyuusyo2Down.ForeColor = Drawing.Color.IndianRed
            Case btnBukkenjyuusyo3Up.ID
                strSort = "bukken_jyuusyo3"
                strUpDown = "ASC"
                btnBukkenjyuusyo3Up.ForeColor = Drawing.Color.IndianRed
            Case btnBukkenjyuusyo3Down.ID
                strSort = "bukken_jyuusyo3"
                strUpDown = "DESC"
                btnBukkenjyuusyo3Down.ForeColor = Drawing.Color.IndianRed
            Case btnTatemonokouzouUp.ID
                strSort = "tatemono_kouzou"
                strUpDown = "ASC"
                btnTatemonokouzouUp.ForeColor = Drawing.Color.IndianRed
            Case btnTatemonokouzouDown.ID
                strSort = "tatemono_kouzou"
                strUpDown = "DESC"
                btnTatemonokouzouDown.ForeColor = Drawing.Color.IndianRed
            Case btnTatemonokaisuUp.ID
                strSort = "tatemono_kaisu"
                strUpDown = "ASC"
                btnTatemonokaisuUp.ForeColor = Drawing.Color.IndianRed
            Case btnTatemonokaisuDown.ID
                strSort = "tatemono_kaisu"
                strUpDown = "DESC"
                btnTatemonokaisuDown.ForeColor = Drawing.Color.IndianRed
            Case btnFcnmUp.ID
                strSort = "fc_nm"
                strUpDown = "ASC"
                btnFcnmUp.ForeColor = Drawing.Color.IndianRed
            Case btnFcnmDown.ID
                strSort = "fc_nm"
                strUpDown = "DESC"
                btnFcnmDown.ForeColor = Drawing.Color.IndianRed
            Case btnKameitentantoUp.ID
                strSort = "kameiten_tanto"
                strUpDown = "ASC"
                btnKameitentantoUp.ForeColor = Drawing.Color.IndianRed
            Case btnKameitentantoDown.ID
                strSort = "kameiten_tanto"
                strUpDown = "DESC"
                btnKameitentantoDown.ForeColor = Drawing.Color.IndianRed
            Case btnTatemonokameitencdUp.ID
                strSort = "tatemono_kameiten_cd"
                strUpDown = "ASC"
                btnTatemonokameitencdUp.ForeColor = Drawing.Color.IndianRed
            Case btnTatemonokameitencdDown.ID
                strSort = "tatemono_kameiten_cd"
                strUpDown = "DESC"
                btnTatemonokameitencdDown.ForeColor = Drawing.Color.IndianRed
            Case btnKanrihyououtflgUp.ID
                strSort = "kanrihyou_out_flg"
                strUpDown = "ASC"
                btnKanrihyououtflgUp.ForeColor = Drawing.Color.IndianRed
            Case btnKanrihyououtflgDown.ID
                strSort = "kanrihyou_out_flg"
                strUpDown = "DESC"
                btnKanrihyououtflgDown.ForeColor = Drawing.Color.IndianRed
            Case btnKanrihyououtdateUp.ID
                strSort = "kanrihyou_out_date"
                strUpDown = "ASC"
                btnKanrihyououtdateUp.ForeColor = Drawing.Color.IndianRed
            Case btnKanrihyououtdateDown.ID
                strSort = "kanrihyou_out_date"
                strUpDown = "DESC"
                btnKanrihyououtdateDown.ForeColor = Drawing.Color.IndianRed
            Case btnSouhujyououtflgUp.ID
                strSort = "souhujyou_out_flg"
                strUpDown = "ASC"
                btnSouhujyououtflgUp.ForeColor = Drawing.Color.IndianRed
            Case btnSouhujyououtflgDown.ID
                strSort = "souhujyou_out_flg"
                strUpDown = "DESC"
                btnSouhujyououtflgDown.ForeColor = Drawing.Color.IndianRed
            Case btnSouhujyououtdateUp.ID
                strSort = "souhujyou_out_date"
                strUpDown = "ASC"
                btnSouhujyououtdateUp.ForeColor = Drawing.Color.IndianRed
            Case btnSouhujyououtdateDown.ID
                strSort = "souhujyou_out_date"
                strUpDown = "DESC"
                btnSouhujyououtdateDown.ForeColor = Drawing.Color.IndianRed
            Case btnKensahkksoutflgUp.ID
                strSort = "kensa_hkks_out_flg"
                strUpDown = "ASC"
                btnKensahkksoutflgUp.ForeColor = Drawing.Color.IndianRed
            Case btnKensahkksoutflgDown.ID
                strSort = "kensa_hkks_out_flg"
                strUpDown = "DESC"
                btnKensahkksoutflgDown.ForeColor = Drawing.Color.IndianRed
            Case btnKensahkksoutdateUp.ID
                strSort = "kensa_hkks_out_date"
                strUpDown = "ASC"
                btnKensahkksoutdateUp.ForeColor = Drawing.Color.IndianRed
            Case btnKensahkksoutdateDown.ID
                strSort = "kensa_hkks_out_date"
                strUpDown = "DESC"
                btnKensahkksoutdateDown.ForeColor = Drawing.Color.IndianRed
            Case btnTooshinoUp.ID
                strSort = "tooshi_no"
                strUpDown = "ASC"
                btnTooshinoUp.ForeColor = Drawing.Color.IndianRed
            Case btnTooshinoDown.ID
                strSort = "tooshi_no"
                strUpDown = "DESC"
                btnTooshinoDown.ForeColor = Drawing.Color.IndianRed
            Case btnKensakouteinm1Up.ID
                strSort = "kensa_koutei_nm1"
                strUpDown = "ASC"
                btnKensakouteinm1Up.ForeColor = Drawing.Color.IndianRed
            Case btnKensakouteinm1Down.ID
                strSort = "kensa_koutei_nm1"
                strUpDown = "DESC"
                btnKensakouteinm1Down.ForeColor = Drawing.Color.IndianRed
            Case btnKensakouteinm2Up.ID
                strSort = "kensa_koutei_nm2"
                strUpDown = "ASC"
                btnKensakouteinm2Up.ForeColor = Drawing.Color.IndianRed
            Case btnKensakouteinm2Down.ID
                strSort = "kensa_koutei_nm2"
                strUpDown = "DESC"
                btnKensakouteinm2Up.ForeColor = Drawing.Color.IndianRed
            Case btnKensakouteinm3Up.ID
                strSort = "kensa_koutei_nm3"
                strUpDown = "ASC"
                btnKensakouteinm3Up.ForeColor = Drawing.Color.IndianRed
            Case btnKensakouteinm3Down.ID
                strSort = "kensa_koutei_nm3"
                strUpDown = "DESC"
                btnKensakouteinm3Up.ForeColor = Drawing.Color.IndianRed
            Case btnKensakouteinm4Up.ID
                strSort = "kensa_koutei_nm4"
                strUpDown = "ASC"
                btnKensakouteinm4Up.ForeColor = Drawing.Color.IndianRed
            Case btnKensakouteinm4Down.ID
                strSort = "kensa_koutei_nm4"
                strUpDown = "DESC"
                btnKensakouteinm4Up.ForeColor = Drawing.Color.IndianRed
            Case btnKensakouteinm5Up.ID
                strSort = "kensa_koutei_nm5"
                strUpDown = "ASC"
                btnKensakouteinm5Up.ForeColor = Drawing.Color.IndianRed
            Case btnKensakouteinm5Down.ID
                strSort = "kensa_koutei_nm5"
                strUpDown = "DESC"
                btnKensakouteinm5Up.ForeColor = Drawing.Color.IndianRed
            Case btnKensakouteinm6Up.ID
                strSort = "kensa_koutei_nm6"
                strUpDown = "ASC"
                btnKensakouteinm6Up.ForeColor = Drawing.Color.IndianRed
            Case btnKensakouteinm6Down.ID
                strSort = "kensa_koutei_nm6"
                strUpDown = "DESC"
                btnKensakouteinm6Up.ForeColor = Drawing.Color.IndianRed
            Case btnKensakouteinm7Up.ID
                strSort = "kensa_koutei_nm7"
                strUpDown = "ASC"
                btnKensakouteinm7Up.ForeColor = Drawing.Color.IndianRed
            Case btnKensakouteinm7Down.ID
                strSort = "kensa_koutei_nm7"
                strUpDown = "DESC"
                btnKensakouteinm7Up.ForeColor = Drawing.Color.IndianRed
            Case btnKensakouteinm8Up.ID
                strSort = "kensa_koutei_nm8"
                strUpDown = "ASC"
                btnKensakouteinm8Up.ForeColor = Drawing.Color.IndianRed
            Case btnKensakouteinm8Down.ID
                strSort = "kensa_koutei_nm8"
                strUpDown = "DESC"
                btnKensakouteinm8Up.ForeColor = Drawing.Color.IndianRed
            Case btnKensakouteinm9Up.ID
                strSort = "kensa_koutei_nm9"
                strUpDown = "ASC"
                btnKensakouteinm9Up.ForeColor = Drawing.Color.IndianRed
            Case btnKensakouteinm9Down.ID
                strSort = "kensa_koutei_nm9"
                strUpDown = "DESC"
                btnKensakouteinm9Up.ForeColor = Drawing.Color.IndianRed
            Case btnKensakouteinm10Up.ID
                strSort = "kensa_koutei_nm10"
                strUpDown = "ASC"
                btnKensakouteinm10Up.ForeColor = Drawing.Color.IndianRed
            Case btnKensakouteinm10Down.ID
                strSort = "kensa_koutei_nm10"
                strUpDown = "DESC"
                btnKensakouteinm10Up.ForeColor = Drawing.Color.IndianRed
            Case btnKensastartjissibi1Up.ID
                strSort = "kensa_start_jissibi1"
                strUpDown = "ASC"
                btnKensastartjissibi1Up.ForeColor = Drawing.Color.IndianRed
            Case btnKensastartjissibi1Down.ID
                strSort = "kensa_start_jissibi1"
                strUpDown = "DESC"
                btnKensastartjissibi1Down.ForeColor = Drawing.Color.IndianRed
            Case btnKensastartjissibi2Up.ID
                strSort = "kensa_start_jissibi2"
                strUpDown = "ASC"
                btnKensastartjissibi2Up.ForeColor = Drawing.Color.IndianRed
            Case btnKensastartjissibi2Down.ID
                strSort = "kensa_start_jissibi2"
                strUpDown = "DESC"
                btnKensastartjissibi2Down.ForeColor = Drawing.Color.IndianRed
            Case btnKensastartjissibi3Up.ID
                strSort = "kensa_start_jissibi3"
                strUpDown = "ASC"
                btnKensastartjissibi3Up.ForeColor = Drawing.Color.IndianRed
            Case btnKensastartjissibi3Down.ID
                strSort = "kensa_start_jissibi3"
                strUpDown = "DESC"
                btnKensastartjissibi3Down.ForeColor = Drawing.Color.IndianRed
            Case btnKensastartjissibi4Up.ID
                strSort = "kensa_start_jissibi4"
                strUpDown = "ASC"
                btnKensastartjissibi4Up.ForeColor = Drawing.Color.IndianRed
            Case btnKensastartjissibi4Down.ID
                strSort = "kensa_start_jissibi4"
                strUpDown = "DESC"
                btnKensastartjissibi1Up.ForeColor = Drawing.Color.IndianRed
            Case btnKensastartjissibi4Down.ID
                strSort = "kensa_start_jissibi5"
                strUpDown = "ASC"
                btnKensastartjissibi5Up.ForeColor = Drawing.Color.IndianRed
            Case btnKensastartjissibi5Down.ID
                strSort = "kensa_start_jissibi5"
                strUpDown = "DESC"
                btnKensastartjissibi5Down.ForeColor = Drawing.Color.IndianRed
            Case btnKensastartjissibi6Up.ID
                strSort = "kensa_start_jissibi6"
                strUpDown = "ASC"
                btnKensastartjissibi6Up.ForeColor = Drawing.Color.IndianRed
            Case btnKensastartjissibi6Down.ID
                strSort = "kensa_start_jissibi6"
                strUpDown = "DESC"
                btnKensastartjissibi6Down.ForeColor = Drawing.Color.IndianRed
            Case btnKensastartjissibi7Up.ID
                strSort = "kensa_start_jissibi7"
                strUpDown = "ASC"
                btnKensastartjissibi7Up.ForeColor = Drawing.Color.IndianRed
            Case btnKensastartjissibi7Down.ID
                strSort = "kensa_start_jissibi7"
                strUpDown = "DESC"
                btnKensastartjissibi7Down.ForeColor = Drawing.Color.IndianRed
            Case btnKensastartjissibi8Up.ID
                strSort = "kensa_start_jissibi8"
                strUpDown = "ASC"
                btnKensastartjissibi8Up.ForeColor = Drawing.Color.IndianRed
            Case btnKensastartjissibi8Down.ID
                strSort = "kensa_start_jissibi8"
                strUpDown = "DESC"
                btnKensastartjissibi8Down.ForeColor = Drawing.Color.IndianRed
            Case btnKensastartjissibi9Up.ID
                strSort = "kensa_start_jissibi9"
                strUpDown = "ASC"
                btnKensastartjissibi9Up.ForeColor = Drawing.Color.IndianRed
            Case btnKensastartjissibi9Down.ID
                strSort = "kensa_start_jissibi9"
                strUpDown = "DESC"
                btnKensastartjissibi9Down.ForeColor = Drawing.Color.IndianRed
            Case btnKensastartjissibi10Up.ID
                strSort = "kensa_start_jissibi10"
                strUpDown = "ASC"
                btnKensastartjissibi10Up.ForeColor = Drawing.Color.IndianRed
            Case btnKensastartjissibi10Down.ID
                strSort = "kensa_start_jissibi10"
                strUpDown = "DESC"
                btnKensastartjissibi10Down.ForeColor = Drawing.Color.IndianRed
            Case btnKensainnm1Up.ID
                strSort = "kensa_in_nm1"
                strUpDown = "ASC"
                btnKensainnm1Up.ForeColor = Drawing.Color.IndianRed
            Case btnKensainnm1Down.ID
                strSort = "kensa_in_nm1"
                strUpDown = "DESC"
                btnKensainnm1Down.ForeColor = Drawing.Color.IndianRed
            Case btnKensainnm2Up.ID
                strSort = "kensa_in_nm2"
                strUpDown = "ASC"
                btnKensainnm2Up.ForeColor = Drawing.Color.IndianRed
            Case btnKensainnm2Down.ID
                strSort = "kensa_in_nm2"
                strUpDown = "DESC"
                btnKensainnm2Down.ForeColor = Drawing.Color.IndianRed
            Case btnKensainnm3Up.ID
                strSort = "kensa_in_nm3"
                strUpDown = "ASC"
                btnKensainnm3Up.ForeColor = Drawing.Color.IndianRed
            Case btnKensainnm3Down.ID
                strSort = "kensa_in_nm3"
                strUpDown = "DESC"
                btnKensainnm3Down.ForeColor = Drawing.Color.IndianRed
            Case btnKensainnm4Up.ID
                strSort = "kensa_in_nm4"
                strUpDown = "ASC"
                btnKensainnm1Up.ForeColor = Drawing.Color.IndianRed
            Case btnKensainnm4Down.ID
                strSort = "kensa_in_nm4"
                strUpDown = "DESC"
                btnKensainnm4Down.ForeColor = Drawing.Color.IndianRed
            Case btnKensainnm5Up.ID
                strSort = "kensa_in_nm5"
                strUpDown = "ASC"
                btnKensainnm5Up.ForeColor = Drawing.Color.IndianRed
            Case btnKensainnm5Down.ID
                strSort = "kensa_in_nm5"
                strUpDown = "DESC"
                btnKensainnm5Down.ForeColor = Drawing.Color.IndianRed
            Case btnKensainnm6Up.ID
                strSort = "kensa_in_nm6"
                strUpDown = "ASC"
                btnKensainnm6Up.ForeColor = Drawing.Color.IndianRed
            Case btnKensainnm6Down.ID
                strSort = "kensa_in_nm6"
                strUpDown = "DESC"
                btnKensainnm6Down.ForeColor = Drawing.Color.IndianRed
            Case btnKensainnm7Up.ID
                strSort = "kensa_in_nm7"
                strUpDown = "ASC"
                btnKensainnm7Up.ForeColor = Drawing.Color.IndianRed
            Case btnKensainnm7Down.ID
                strSort = "kensa_in_nm7"
                strUpDown = "DESC"
                btnKensainnm7Down.ForeColor = Drawing.Color.IndianRed
            Case btnKensainnm8Up.ID
                strSort = "kensa_in_nm8"
                strUpDown = "ASC"
                btnKensainnm8Up.ForeColor = Drawing.Color.IndianRed
            Case btnKensainnm8Down.ID
                strSort = "kensa_in_nm8"
                strUpDown = "DESC"
                btnKensainnm8Down.ForeColor = Drawing.Color.IndianRed
            Case btnKensainnm9Up.ID
                strSort = "kensa_in_nm9"
                strUpDown = "ASC"
                btnKensainnm9Up.ForeColor = Drawing.Color.IndianRed
            Case btnKensainnm9Down.ID
                strSort = "kensa_in_nm9"
                strUpDown = "DESC"
                btnKensainnm9Down.ForeColor = Drawing.Color.IndianRed
            Case btnKensainnm10Up.ID
                strSort = "kensa_in_nm10"
                strUpDown = "ASC"
                btnKensainnm10Up.ForeColor = Drawing.Color.IndianRed
            Case btnKensainnm10Down.ID
                strSort = "kensa_in_nm10"
                strUpDown = "DESC"
                btnKensainnm10Down.ForeColor = Drawing.Color.IndianRed
            Case btnAddloginuseridUp.ID
                strSort = "add_login_user_id"
                strUpDown = "ASC"
                btnAddloginuseridUp.ForeColor = Drawing.Color.IndianRed
            Case btnAddloginuseridDown.ID
                strSort = "add_login_user_id"
                strUpDown = "DESC"
                btnAddloginuseridDown.ForeColor = Drawing.Color.IndianRed
            Case btnAdddatetimeUp.ID
                strSort = "add_datetime"
                strUpDown = "ASC"
                btnAdddatetimeUp.ForeColor = Drawing.Color.IndianRed
            Case btnAdddatetimeDown.ID
                strSort = "add_datetime"
                strUpDown = "DESC"
                btnAdddatetimeDown.ForeColor = Drawing.Color.IndianRed
            Case btnUpdloginuseridUp.ID
                strSort = "upd_login_user_id"
                strUpDown = "ASC"
                btnUpdloginuseridUp.ForeColor = Drawing.Color.IndianRed
            Case btnUpdloginuseridDown.ID
                strSort = "upd_login_user_id"
                strUpDown = "DESC"
                btnUpdloginuseridDown.ForeColor = Drawing.Color.IndianRed
            Case btnUpddatetimeUp.ID
                strSort = "upd_datetime"
                strUpDown = "ASC"
                btnUpddatetimeUp.ForeColor = Drawing.Color.IndianRed
            Case btnUpddatetimeDown.ID
                strSort = "upd_datetime"
                strUpDown = "DESC"
                btnUpddatetimeDown.ForeColor = Drawing.Color.IndianRed
        End Select

        '画面データのソート順を設定する
        Dim dvKameitenInfo As Data.DataView = CType(ViewState("dtKensaHkksKanri"), Data.DataTable).DefaultView
        dvKameitenInfo.Sort = strSort & " " & strUpDown

        Me.grdBodyLeft.DataSource = dvKameitenInfo
        Me.grdBodyLeft.DataBind()
        Me.grdBodyRight.DataSource = dvKameitenInfo
        Me.grdBodyRight.DataBind()

        '画面縦スクロールを設定する
        scrollHeight = ViewState("scrollHeight")

        '画面横スクロール位置を設定する
        SetScroll()

    End Sub

    ''' <summary>
    ''' 各帳票出力指示画面へ
    ''' </summary>
    ''' <param name="sender">system.Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2015/12/07　高兵兵(大連)　新規作成</para>
    ''' </history>
    Private Sub btnGotoOutput_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGotoOutput.Click

        Server.Transfer("KensaHoukokusyoOutput.aspx")

    End Sub

    ''' <summary>
    ''' <summary>検索実行</summary>
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2015/12/07　高兵兵(大連)　新規作成</para>
    ''' </history>
    Private Sub Kensakuzikkou()

        '加盟店名を設定
        Call Me.SetkameitenCd(tbxKameitenCdFrom.Text)

        '検査報告書管理テーブルを取得
        Dim dtKensaHkksKanri As New DataTable
        dtKensaHkksKanri = KensaLogic.GetKensaHoukokusyoKanriSearch(Me.tbxKakunouDateFrom.Text.Trim, _
                                                              Me.tbxKakunouDateTo.Text.Trim, _
                                                              Me.tbxSendDateFrom.Text.Trim, _
                                                              Me.tbxSendDateTo.Text.Trim, _
                                                              Me.ddlKbn.SelectedValue.Trim, _
                                                              Me.tbxNoFrom.Text.Trim, _
                                                              Me.tbxNoTo.Text.Trim, _
                                                              Me.tbxKameitenCdFrom.Text.Trim, _
                                                              Me.ddlKensakuJyouken.SelectedValue.Trim, _
                                                              Me.chkKensakuTaisyouGai.Checked, _
                                                              Me.chkSendDateTaisyouGai.Checked)

        Me.chkAll.Checked = False

        If dtKensaHkksKanri.Rows.Count > 0 Then

            ViewState("dtKensaHkksKanri") = dtKensaHkksKanri
            Me.grdBodyLeft.DataSource = ViewState("dtKensaHkksKanri")
            Me.grdBodyLeft.DataBind()

            Me.grdBodyRight.DataSource = ViewState("dtKensaHkksKanri")
            Me.grdBodyRight.DataBind()
            'リンクボタンの表示を設定
            Call Me.SetUpDownHyouji(True)

            'リンクボタンの色を設定
            Call Me.setUpDownColor()

            ''検索結果を設定
            ViewState("intCount") = dtKensaHkksKanri.Rows.Count
            Dim intKenkasaKensuu As Integer = Me.SetKensakuResult(True)

        Else
            ViewState("dtKensaHkksKanri") = Nothing

            Me.grdBodyLeft.DataSource = Nothing
            Me.grdBodyLeft.DataBind()

            Me.grdBodyRight.DataSource = Nothing
            Me.grdBodyRight.DataBind()
            'リンクボタンの表示を設定
            Call Me.SetUpDownHyouji(False)

            '検索結果を設定
            Dim intKenkasaKensuu As Integer = Me.SetKensakuResult(False)
            'エラーメッセージを表示
            ShowMessage(Messages.Instance.MSG020E, String.Empty)
        End If

        ViewState("scrollHeight") = scrollHeight

    End Sub

    ''' <summary>
    ''' CSV出力
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2015/12/07　高兵兵(大連)　新規作成</para>
    ''' </history>
    Private Sub CsvOutPut()
        '検査報告書管理テーブルを取得
        Dim dtKensaHkksKanri As New DataTable
        dtKensaHkksKanri = KensaLogic.GetKensaHoukokusyoKanriSearch(Me.tbxKakunouDateFrom.Text.Trim, _
                                                              Me.tbxKakunouDateTo.Text.Trim, _
                                                              Me.tbxSendDateFrom.Text.Trim, _
                                                              Me.tbxSendDateTo.Text.Trim, _
                                                              Me.ddlKbn.SelectedValue.Trim, _
                                                              Me.tbxNoFrom.Text.Trim, _
                                                              Me.tbxNoTo.Text.Trim, _
                                                              Me.tbxKameitenCdFrom.Text.Trim, _
                                                              Me.ddlKensakuJyouken.SelectedValue.Trim, _
                                                              Me.chkKensakuTaisyouGai.Checked, _
                                                              Me.chkSendDateTaisyouGai.Checked)

        'CSVファイル名設定
        Dim strFileName As String = System.Configuration.ConfigurationManager.AppSettings("KensahoukokushoCsv").ToString

        Response.Buffer = True
        Dim writer As New CsvWriter(Me.Response.OutputStream, Encoding.GetEncoding(932), ",", vbCrLf)

        'CSVファイルヘッダ設定
        writer.WriteLine(EarthConst.conKensahoukokushoCsvHeader)

        'CSVファイル内容設定
        For i As Integer = 0 To dtKensaHkksKanri.Rows.Count - 1
            With dtKensaHkksKanri.Rows(i)
                writer.WriteLine(.Item("torikesi"), .Item("kanri_no"), .Item("kbn"), .Item("hosyousyo_no"), .Item("sesyu_mei"), _
                                 .Item("kameiten_cd"), .Item("kameiten_mei"), .Item("kakunou_date"), .Item("hassou_date"), _
                                 .Item("kensa_hkks_busuu"), .Item("kensa_hkks_jyuusyo1"), .Item("kensa_hkks_jyuusyo2"), _
                                 .Item("yuubin_no"), .Item("tel_no"), .Item("busyo_mei"), .Item("todouhuken_cd"), _
                                 .Item("todouhuken_mei"), .Item("hassou_date_in_flg"), .Item("souhu_tantousya"), _
                                 .Item("bukken_jyuusyo1"), .Item("bukken_jyuusyo2"), .Item("bukken_jyuusyo3"), _
                                 .Item("tatemono_kouzou"), .Item("tatemono_kaisu"), .Item("fc_nm"), .Item("kameiten_tanto"), _
                                 .Item("tatemono_kameiten_cd"), .Item("kanrihyou_out_flg"), .Item("kanrihyou_out_date"), _
                                 .Item("souhujyou_out_flg"), .Item("souhujyou_out_date"), .Item("kensa_hkks_out_flg"), _
                                 .Item("kensa_hkks_out_date"), .Item("tooshi_no"), .Item("kensa_koutei_nm1"), _
                                 .Item("kensa_koutei_nm2"), .Item("kensa_koutei_nm3"), .Item("kensa_koutei_nm4"), _
                                 .Item("kensa_koutei_nm5"), .Item("kensa_koutei_nm6"), .Item("kensa_koutei_nm7"), _
                                 .Item("kensa_koutei_nm8"), .Item("kensa_koutei_nm9"), .Item("kensa_koutei_nm10"), _
                                 .Item("kensa_start_jissibi1"), .Item("kensa_start_jissibi2"), .Item("kensa_start_jissibi3"), _
                                 .Item("kensa_start_jissibi4"), .Item("kensa_start_jissibi5"), .Item("kensa_start_jissibi6"), _
                                 .Item("kensa_start_jissibi7"), .Item("kensa_start_jissibi8"), .Item("kensa_start_jissibi9"), _
                                 .Item("kensa_start_jissibi10"), .Item("kensa_in_nm1"), .Item("kensa_in_nm2"), .Item("kensa_in_nm3"), _
                                 .Item("kensa_in_nm4"), .Item("kensa_in_nm5"), .Item("kensa_in_nm6"), .Item("kensa_in_nm7"), _
                                 .Item("kensa_in_nm8"), .Item("kensa_in_nm9"), .Item("kensa_in_nm10"), .Item("add_login_user_id"), _
                                 .Item("add_datetime"), .Item("upd_login_user_id"), .Item("upd_datetime"))
            End With
        Next

        'CSVファイルダウンロード
        Response.Charset = "utf-8"
        Response.ContentType = "text/plain"
        Response.AddHeader("Content-Disposition", "attachment; filename=" & HttpUtility.UrlEncode(strFileName))
        Response.End()

    End Sub

    ''' <summary>
    ''' すべて選択時
    ''' </summary>
    ''' <param name="sender">system.Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2015/12/07　高兵兵(大連)　新規作成</para>
    ''' </history>
    Private Sub chkAll_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAll.CheckedChanged
        Dim intCount As Int64 = 0
        intCount = Me.grdBodyLeft.Rows.Count
        If intCount > 0 Then
            If Me.chkAll.Checked = True Then
                Me.hidCsvFlg.Value = intCount.ToString
                For i As Int64 = 0 To intCount - 1
                    CType(Me.grdBodyLeft.Rows(i).Cells(0).FindControl("chkKensakuTaisyouGai"), CheckBox).Checked = True
                Next
            Else
                Me.hidCsvFlg.Value = 0
                For i As Int64 = 0 To intCount - 1
                    CType(Me.grdBodyLeft.Rows(i).Cells(0).FindControl("chkKensakuTaisyouGai"), CheckBox).Checked = False
                Next
            End If
        End If
    End Sub

    ''' <summary>
    ''' CHECKBOX
    ''' </summary>
    ''' <param name="sender">system.Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2015/12/07　高兵兵(大連)　新規作成</para>
    ''' </history>
    Private Sub grdBodyLeft_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdBodyLeft.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            CType(e.Row.FindControl("chkKensakuTaisyouGai"), CheckBox).Attributes.Add("onclick", "ChkCsvFlg(this," & e.Row.RowIndex & ");")
        End If
    End Sub

    ''' <summary>加盟店名を設定</summary>
    ''' <param name="strKameitenCd">加盟店コード</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2015/12/07　高兵兵(大連)　新規作成</para>
    ''' </history>
    Private Sub SetkameitenCd(ByVal strKameitenCd As String)

        If Not strKameitenCd.Trim.Equals(String.Empty) Then
            '加盟店名データを取得
            Dim dtKameitenMei As New Data.DataTable
            dtKameitenMei = KensaLogic.SelMkameiten(strKameitenCd)

            If dtKameitenMei.Rows.Count > 0 Then
                '加盟店名
                Me.tbxKameitenMeiFrom.Text = dtKameitenMei.Rows(0).Item(0).ToString.Trim
            Else
                Me.tbxKameitenMeiFrom.Text = String.Empty
            End If
        Else
            Me.tbxKameitenMeiFrom.Text = String.Empty
        End If


    End Sub

    ''' <summary>入力チェック</summary>
    ''' <param name="strObjId">クライアントID</param>
    ''' <returns>エラーメッセージ</returns>
    ''' <history>
    ''' <para>2015/12/07　高兵兵(大連)　新規作成</para>
    ''' </history>
    Public Function CheckInput(ByRef strObjId As String, ByRef strFlg As String) As String
        Dim csScript As New StringBuilder
        With csScript
            '格納日(From)
            If Me.tbxKakunouDateFrom.Text <> String.Empty Then
                .Append(commonCheck.CheckYuukouHiduke(Me.tbxKakunouDateFrom.Text, "格納日(From)"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxKakunouDateFrom.ClientID
                End If
            End If
            '格納日(To)
            If Me.tbxKakunouDateTo.Text <> String.Empty Then
                .Append(commonCheck.CheckYuukouHiduke(Me.tbxKakunouDateTo.Text, "格納日(To)"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxKakunouDateTo.ClientID
                End If
            End If
            '格納日範囲
            If Me.tbxKakunouDateFrom.Text <> String.Empty And Me.tbxKakunouDateTo.Text <> String.Empty Then
                If commonCheck.CheckYuukouHiduke(Me.tbxKakunouDateFrom.Text, "格納日(From)") = String.Empty _
                   And commonCheck.CheckYuukouHiduke(Me.tbxKakunouDateTo.Text, "格納日(To)") = String.Empty Then
                    .Append(commonCheck.CheckHidukeHani(Me.tbxKakunouDateFrom.Text, Me.tbxKakunouDateTo.Text, "格納日"))
                    If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                        strObjId = Me.tbxKakunouDateFrom.ClientID
                    End If
                End If
            End If
            If Me.tbxKakunouDateFrom.Text = String.Empty And Me.tbxKakunouDateTo.Text <> String.Empty Then
                If commonCheck.CheckYuukouHiduke(Me.tbxKakunouDateTo.Text, "格納日(To)") = String.Empty Then
                    .Append(String.Format(Messages.Instance.MSG2012E, "格納日", "格納日").ToString)
                End If
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxKakunouDateFrom.ClientID
                End If
            End If

            '発送日(From)
            If Me.tbxSendDateFrom.Text <> String.Empty Then
                .Append(commonCheck.CheckYuukouHiduke(Me.tbxSendDateFrom.Text, "発送日(From)"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxSendDateFrom.ClientID
                End If
            End If
            '発送日(To)
            If Me.tbxSendDateTo.Text <> String.Empty Then
                .Append(commonCheck.CheckYuukouHiduke(Me.tbxSendDateTo.Text, "発送日(To)"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxSendDateTo.ClientID
                End If
            End If
            '発送日範囲
            If Me.tbxSendDateFrom.Text <> String.Empty And Me.tbxSendDateTo.Text <> String.Empty Then
                If commonCheck.CheckYuukouHiduke(Me.tbxSendDateFrom.Text, "発送日(From)") = String.Empty _
                   And commonCheck.CheckYuukouHiduke(Me.tbxSendDateTo.Text, "発送日(To)") = String.Empty Then
                    .Append(commonCheck.CheckHidukeHani(Me.tbxSendDateFrom.Text, Me.tbxSendDateTo.Text, "発送日"))
                    If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                        strObjId = Me.tbxSendDateFrom.ClientID
                    End If
                End If
            End If
            If Me.tbxSendDateFrom.Text = String.Empty And Me.tbxSendDateTo.Text <> String.Empty Then
                If commonCheck.CheckYuukouHiduke(Me.tbxSendDateTo.Text, "発送日(To)") = String.Empty Then
                    .Append(String.Format(Messages.Instance.MSG2012E, "発送日", "発送日").ToString)
                End If
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxSendDateFrom.ClientID
                End If
            End If

            '加盟店コード
            If Me.tbxKameitenCdFrom.Text <> String.Empty Then
                .Append(commonCheck.ChkHankakuEisuuji(Me.tbxKameitenCdFrom.Text, "加盟店コード"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxKameitenCdFrom.ClientID
                End If
            End If

            '番号From
            If tbxNoFrom.Text <> "" Then
                .Append(commonCheck.CheckHankaku(Me.tbxNoFrom.Text, "番号(From)"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxNoFrom.ClientID
                End If
            End If
            '番号To
            If tbxNoTo.Text <> "" Then
                .Append(commonCheck.CheckHankaku(Me.tbxNoTo.Text, "番号(To)"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxNoTo.ClientID
                End If
            End If
            '番号範囲
            If Me.tbxNoFrom.Text <> String.Empty And Me.tbxNoTo.Text <> String.Empty Then
                If commonCheck.CheckHankaku(Me.tbxNoTo.Text, "番号(From)") = String.Empty _
                   And commonCheck.CheckHankaku(Me.tbxNoTo.Text, "番号(To)") = String.Empty Then
                    If tbxNoFrom.Text > tbxNoTo.Text And tbxNoTo.Text <> "" Then
                        .Append(String.Format(Messages.Instance.MSG2012E, "番号", "番号").ToString)
                    End If
                    If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                        strObjId = Me.tbxNoFrom.ClientID
                    End If
                End If
            End If
            If Me.tbxNoFrom.Text = String.Empty And Me.tbxNoTo.Text <> String.Empty Then
                If commonCheck.CheckHankaku(Me.tbxNoTo.Text, "番号(To)") = String.Empty Then
                    .Append(String.Format(Messages.Instance.MSG2012E, "番号", "番号").ToString)
                End If
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxNoFrom.ClientID
                End If
            End If

            If strFlg = "1" Then
                '一括セット発送日
                If Me.tbxSetSendDate.Text <> String.Empty Then
                    .Append(commonCheck.CheckYuukouHiduke(Me.tbxSetSendDate.Text, "一括セット発送日"))
                    If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                        strObjId = Me.tbxSetSendDate.ClientID
                    End If
                End If

                '送付担当者
                If Me.tbxTantousya.Text <> String.Empty Then
                    .Append(commonCheck.CheckKinsoku(Me.tbxTantousya.Text, "送付担当者"))
                    If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                        strObjId = Me.tbxTantousya.ClientID
                    End If
                End If
            End If

        End With

        Return csScript.ToString
    End Function

    ''' <summary>
    ''' <summary>Javascript作成</summary>
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2015/12/07　高兵兵(大連)　新規作成</para>
    ''' </history>
    Private Sub MakeJavascript()
        Dim sbScript As New StringBuilder
        Dim strPraram(0) As String
        With sbScript
            .AppendLine("<script language='javascript' type='text/javascript'>")

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
            .AppendLine("   var divbodyleft=" & Me.divBodyLeft.ClientID & ";")
            .AppendLine("   var divbodyright=" & Me.divBodyRight.ClientID & ";")
            .AppendLine("   var divVscroll=" & Me.divHiddenMeisaiV.ClientID & ";")
            .AppendLine("   divbodyleft.scrollTop = divVscroll.scrollTop;")
            .AppendLine("   divbodyright.scrollTop = divVscroll.scrollTop;")
            .AppendLine("}")
            .AppendLine("function fncScrollH(){")
            .AppendLine("   var divheadright=" & Me.divHeadRight.ClientID & ";")
            .AppendLine("   var divbodyright=" & Me.divBodyRight.ClientID & ";")
            .AppendLine("   var divHscroll=" & Me.divHiddenMeisaiH.ClientID & ";")
            .AppendLine("   divheadright.scrollLeft = divHscroll.scrollLeft;")
            .AppendLine("   divbodyright.scrollLeft = divHscroll.scrollLeft;")
            .AppendLine("}")
            .AppendLine("function fncSetScroll(){")
            .AppendLine("   var divHscroll=" & Me.divHiddenMeisaiH.ClientID & ";")
            .AppendLine("   document.getElementById('" & Me.hidScroll.ClientID & "').value = divHscroll.scrollLeft;")
            .AppendLine("}")
            .AppendLine("	 ")
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
            '閉じるボタン処理
            .AppendLine("function fncClose(){")
            .AppendLine("   window.close();")
            .AppendLine("   return false;")
            .AppendLine("}")
            '加盟店ポップアップ
            .AppendLine("   function fncKameitenSearch(strKbn){")
            .AppendLine("       var strkbn='加盟店'")
            .AppendLine("       var strClientID ")
            .AppendLine("       strClientID = '" & Me.tbxKameitenCdFrom.ClientID & "'")
            .AppendLine("       strClientMei = '" & Me.tbxKameitenMeiFrom.ClientID & "'")
            .AppendLine("       objSrchWin = window.open('search_common.aspx?Kbn='+escape(strkbn)+'&FormName=" & Me.Form.Name & _
                                "&objCd='+strClientID+'&objMei='+strClientMei+'&strMei='+escape(eval('document.all.'+strClientMei).value)+'&strCd='+escape(eval('document.all.'+strClientID).value),")
            .AppendLine("       'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');")
            .AppendLine("   }")
            '「クリア」ボタン
            .AppendLine("function fncClear()")
            .AppendLine("{")
            '格納日From
            .AppendLine("   var tbxKakunouDateFrom = document.getElementById('" & Me.tbxKakunouDateFrom.ClientID & "'); ")
            '格納日To
            .AppendLine("   var tbxKakunouDateTo = document.getElementById('" & Me.tbxKakunouDateTo.ClientID & "'); ")
            '発送日From
            .AppendLine("   var tbxSendDateFrom = document.getElementById('" & Me.tbxSendDateFrom.ClientID & "'); ")
            '発送日To
            .AppendLine("   var tbxSendDateTo = document.getElementById('" & Me.tbxSendDateTo.ClientID & "'); ")
            '区分
            .AppendLine("   var ddlKbn = document.getElementById('" & Me.ddlKbn.DdlClientID & "'); ")
            '番号From
            .AppendLine("   var tbxNoFrom = document.getElementById('" & Me.tbxNoFrom.ClientID & "'); ")
            '番号To
            .AppendLine("   var tbxNoTo = document.getElementById('" & Me.tbxNoTo.ClientID & "'); ")
            '加盟店CD
            .AppendLine("   var tbxKameitenCdFrom = document.getElementById('" & Me.tbxKameitenCdFrom.ClientID & "'); ")
            '加盟店
            .AppendLine("   var tbxKameitenMeiFrom = document.getElementById('" & Me.tbxKameitenMeiFrom.ClientID & "'); ")
            '検索上限件数
            .AppendLine("   var ddlKensakuJyouken = document.getElementById('" & Me.ddlKensakuJyouken.ClientID & "'); ")
            '取消は検索対象外
            .AppendLine("   var chkKensakuTaisyouGai = document.getElementById('" & Me.chkKensakuTaisyouGai.ClientID & "'); ")
            '発送日セット済みは対象外
            .AppendLine("   var chkSendDateTaisyouGai = document.getElementById('" & Me.chkSendDateTaisyouGai.ClientID & "'); ")
            '一括セット発送日
            .AppendLine("   var tbxSetSendDate = document.getElementById('" & Me.tbxSetSendDate.ClientID & "'); ")
            '送付担当者
            .AppendLine("   var tbxTantousya = document.getElementById('" & Me.tbxTantousya.ClientID & "'); ")

            .AppendLine("   tbxKakunouDateFrom.value = '';")
            .AppendLine("   tbxKakunouDateTo.value = '';")
            .AppendLine("   tbxSendDateFrom.value = '';")
            .AppendLine("   tbxSendDateTo.value = '';")
            .AppendLine("   ddlKbn.selectedIndex = '0';")
            .AppendLine("   tbxNoFrom.value = '';")
            .AppendLine("   tbxNoTo.value = '';")
            .AppendLine("   tbxKameitenCdFrom.value = '';")
            .AppendLine("   tbxKameitenMeiFrom.value = '';")
            .AppendLine("   ddlKensakuJyouken.selectedIndex = '1';")
            .AppendLine("   chkKensakuTaisyouGai.checked = true;")
            .AppendLine("   chkSendDateTaisyouGai.checked = true;")
            .AppendLine("   tbxSetSendDate.value = '';")
            .AppendLine("   tbxTantousya.value = '';")
            .AppendLine("   tbxTantousya.value = '';")
            .AppendLine("}")
            '対象CHECK
            .AppendLine("function ChkCsvFlg(e,rowNo)")
            .AppendLine("{")
            .AppendLine("   var hidCsvFlg ")
            .AppendLine("   hidCsvFlg=document.getElementById('" & Me.hidCsvFlg.ClientID & "');")
            .AppendLine("   var intCsvFlg=hidCsvFlg.value;")
            .AppendLine("   if(e.checked==true){")
            .AppendLine("       intCsvFlg++;")
            .AppendLine("   }else{")
            .AppendLine("       intCsvFlg--;")
            .AppendLine("   }")
            .AppendLine("   hidCsvFlg.value=intCsvFlg;")
            .AppendLine("}")
            '選択物件一括セットセット
            .AppendLine("function fncSetSend()	")
            .AppendLine("        {	")
            .AppendLine("            var gvtable = document.getElementById('" & Me.grdBodyLeft.ClientID & "');	")
            .AppendLine("            var counter = 0;	")
            .AppendLine("            var chkcounter = 0;	")
            .AppendLine("            var tbxSetSendDate = document.getElementById('" & Me.tbxSetSendDate.ClientID & "')")
            .AppendLine("            var tbxTantousya = document.getElementById('" & Me.tbxTantousya.ClientID & "')")
            .AppendLine("            if (tbxSetSendDate.value == '' || tbxTantousya.value == '' )	")
            .AppendLine("            {	")
            .AppendLine("                window.alert('" & Messages.Instance.MSG2080E & "');	")
            .AppendLine("                if (tbxSetSendDate.value == '')	")
            .AppendLine("                {	")
            .AppendLine("                    tbxSetSendDate.focus();	")
            .AppendLine("                }	")
            .AppendLine("                else	")
            .AppendLine("                {	")
            .AppendLine("                    tbxTantousya.focus();	")
            .AppendLine("                }	")
            .AppendLine("                return  false;	")
            .AppendLine("            }	")
            .AppendLine("            if (gvtable != null)	")
            .AppendLine("            {	")
            .AppendLine("                for (var i = 0; i < gvtable.rows.length; i++)	")
            .AppendLine("                {	")
            .AppendLine("                    var cbx = gvtable.rows(i).cells(0).children(0);	")
            .AppendLine("                    if(cbx!=null)	")
            .AppendLine("                    {	")
            .AppendLine("                       if (cbx.type == 'checkbox' && cbx.checked == true)	")
            .AppendLine("                          {	")
            .AppendLine("                            if (gvtable.rows(i).cells(1).innerText.Trim() =='取消') {  ")
            .AppendLine("                                 counter++; ")
            .AppendLine("                            } ")
            .AppendLine("                            chkcounter ++; ")
            .AppendLine("                          }	")
            .AppendLine("                    }	")
            .AppendLine("                }	")
            .AppendLine("            }	")
            .AppendLine("            if (counter > 0)	")
            .AppendLine("            {	")
            .AppendLine("                return confirm('" & Messages.Instance.MSG2081E & "');	")
            .AppendLine("            }	")
            .AppendLine("            else if (chkcounter == 0)	")
            .AppendLine("            {	")
            .AppendLine("                window.alert('" & Replace(Messages.Instance.MSG038E, "@PARAM1", "対象") & "');	")
            .AppendLine("                return  false;	")
            .AppendLine("            }	")
            .AppendLine("            else 	")
            .AppendLine("            {	")
            .AppendLine("                return true;	")
            .AppendLine("            }	")
            .AppendLine("        }	")

            .AppendLine("	 ")
            .AppendLine("</script>")
        End With
        Page.ClientScript.RegisterStartupScript(Page.GetType, "myJavaScript", sbScript.ToString)
    End Sub

    ''' <summary>
    ''' <summary>検索結果を設定</summary>
    ''' </summary>
    ''' <param name="blnFlg"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2015/12/07　高兵兵(大連)　新規作成</para>
    ''' </history>
    Private Function SetKensakuResult(ByVal blnFlg As Boolean) As Integer

        '件数を取得
        Dim intGenkaJyouhouCount As Integer = 0

        If blnFlg.Equals(True) Then

            intGenkaJyouhouCount = KensaLogic.GetKensaHoukokusyoKanriSearchCount(Me.tbxKakunouDateFrom.Text.Trim, _
                                                              Me.tbxKakunouDateTo.Text.Trim, _
                                                              Me.tbxSendDateFrom.Text.Trim, _
                                                              Me.tbxSendDateTo.Text.Trim, _
                                                              Me.ddlKbn.SelectedValue.Trim, _
                                                              Me.tbxNoFrom.Text.Trim, _
                                                              Me.tbxNoTo.Text.Trim, _
                                                              Me.tbxKameitenCdFrom.Text.Trim, _
                                                              Me.ddlKensakuJyouken.SelectedValue.Trim, _
                                                              Me.chkKensakuTaisyouGai.Checked, _
                                                              Me.chkSendDateTaisyouGai.Checked)

            If Me.ddlKensakuJyouken.SelectedValue.Trim.Equals("max") Then

                Me.lblCount.Text = CStr(intGenkaJyouhouCount)
                '黒色
                Me.lblCount.ForeColor = Drawing.Color.Black

                scrollHeight = intGenkaJyouhouCount * 25 + 1
            Else
                If intGenkaJyouhouCount > CInt(Me.ddlKensakuJyouken.SelectedValue) Then
                    Me.lblCount.Text = Me.ddlKensakuJyouken.SelectedValue.Trim & " / " & CStr(intGenkaJyouhouCount)
                    '赤色
                    Me.lblCount.ForeColor = Drawing.Color.Red

                    scrollHeight = Me.ddlKensakuJyouken.SelectedValue * 25 + 1
                Else
                    Me.lblCount.Text = CStr(intGenkaJyouhouCount)
                    '黒色
                    Me.lblCount.ForeColor = Drawing.Color.Black

                    scrollHeight = intGenkaJyouhouCount * 25 + 1
                End If
            End If
        Else
            Me.lblCount.Text = "0"
            '黒色
            Me.lblCount.ForeColor = Drawing.Color.Black

            scrollHeight = intGenkaJyouhouCount * 25 + 1

        End If

        Return intGenkaJyouhouCount

    End Function

    ''' <summary>
    ''' <summary>リンクボタンの色を設定</summary>
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2015/12/07　高兵兵(大連)　新規作成</para>
    ''' </history>
    Public Sub setUpDownColor()

        btnKanrinoUp.ForeColor = Drawing.Color.SkyBlue
        btnKanrinoDown.ForeColor = Drawing.Color.SkyBlue
        btnKbnUp.ForeColor = Drawing.Color.SkyBlue
        btnKbnDown.ForeColor = Drawing.Color.SkyBlue
        btnHosyousyonoUp.ForeColor = Drawing.Color.SkyBlue
        btnHosyousyonoDown.ForeColor = Drawing.Color.SkyBlue
        btnKameitencdUp.ForeColor = Drawing.Color.SkyBlue
        btnKameitencdDown.ForeColor = Drawing.Color.SkyBlue
        btnKakunoudateUp.ForeColor = Drawing.Color.SkyBlue
        btnKakunoudateDown.ForeColor = Drawing.Color.SkyBlue
        btnKameitenmeiUp.ForeColor = Drawing.Color.SkyBlue
        btnKameitenmeiDown.ForeColor = Drawing.Color.SkyBlue
        btnSesyumeiUp.ForeColor = Drawing.Color.SkyBlue
        btnSesyumeiDown.ForeColor = Drawing.Color.SkyBlue
        btnTorikesiUp.ForeColor = Drawing.Color.SkyBlue
        btnTorikesiDown.ForeColor = Drawing.Color.SkyBlue
        btnKensahkksbusuuUp.ForeColor = Drawing.Color.SkyBlue
        btnKensahkksbusuuDown.ForeColor = Drawing.Color.SkyBlue
        btnKensahkksjyuusyo1Up.ForeColor = Drawing.Color.SkyBlue
        btnKensahkksjyuusyo1Down.ForeColor = Drawing.Color.SkyBlue
        btnKensahkksjyuusyo2Up.ForeColor = Drawing.Color.SkyBlue
        btnKensahkksjyuusyo2Down.ForeColor = Drawing.Color.SkyBlue
        btnYuubinnoUp.ForeColor = Drawing.Color.SkyBlue
        btnYuubinnoDown.ForeColor = Drawing.Color.SkyBlue
        btnTelnoUp.ForeColor = Drawing.Color.SkyBlue
        btnTelnoDown.ForeColor = Drawing.Color.SkyBlue
        btnBusyomeiUp.ForeColor = Drawing.Color.SkyBlue
        btnBusyomeiDown.ForeColor = Drawing.Color.SkyBlue
        btnTodouhukencdUp.ForeColor = Drawing.Color.SkyBlue
        btnTodouhukencdDown.ForeColor = Drawing.Color.SkyBlue
        btnTodouhukenmeiUp.ForeColor = Drawing.Color.SkyBlue
        btnTodouhukenmeiDown.ForeColor = Drawing.Color.SkyBlue
        btnHassoudateUp.ForeColor = Drawing.Color.SkyBlue
        btnHassoudateDown.ForeColor = Drawing.Color.SkyBlue
        btnHassoudateinflgUp.ForeColor = Drawing.Color.SkyBlue
        btnHassoudateinflgDown.ForeColor = Drawing.Color.SkyBlue
        btnSouhutantousyaUp.ForeColor = Drawing.Color.SkyBlue
        btnSouhutantousyaDown.ForeColor = Drawing.Color.SkyBlue
        btnBukkenjyuusyo1Up.ForeColor = Drawing.Color.SkyBlue
        btnBukkenjyuusyo1Down.ForeColor = Drawing.Color.SkyBlue
        btnBukkenjyuusyo2Up.ForeColor = Drawing.Color.SkyBlue
        btnBukkenjyuusyo2Down.ForeColor = Drawing.Color.SkyBlue
        btnBukkenjyuusyo3Up.ForeColor = Drawing.Color.SkyBlue
        btnBukkenjyuusyo3Down.ForeColor = Drawing.Color.SkyBlue
        btnTatemonokouzouUp.ForeColor = Drawing.Color.SkyBlue
        btnTatemonokouzouDown.ForeColor = Drawing.Color.SkyBlue
        btnTatemonokaisuUp.ForeColor = Drawing.Color.SkyBlue
        btnTatemonokaisuDown.ForeColor = Drawing.Color.SkyBlue
        btnFcnmUp.ForeColor = Drawing.Color.SkyBlue
        btnFcnmDown.ForeColor = Drawing.Color.SkyBlue
        btnKameitentantoUp.ForeColor = Drawing.Color.SkyBlue
        btnKameitentantoDown.ForeColor = Drawing.Color.SkyBlue
        btnTatemonokameitencdUp.ForeColor = Drawing.Color.SkyBlue
        btnTatemonokameitencdDown.ForeColor = Drawing.Color.SkyBlue
        btnKanrihyououtflgUp.ForeColor = Drawing.Color.SkyBlue
        btnKanrihyououtflgDown.ForeColor = Drawing.Color.SkyBlue
        btnKanrihyououtdateUp.ForeColor = Drawing.Color.SkyBlue
        btnKanrihyououtdateDown.ForeColor = Drawing.Color.SkyBlue
        btnSouhujyououtflgUp.ForeColor = Drawing.Color.SkyBlue
        btnSouhujyououtflgDown.ForeColor = Drawing.Color.SkyBlue
        btnSouhujyououtdateUp.ForeColor = Drawing.Color.SkyBlue
        btnSouhujyououtdateDown.ForeColor = Drawing.Color.SkyBlue
        btnKensahkksoutflgUp.ForeColor = Drawing.Color.SkyBlue
        btnKensahkksoutflgDown.ForeColor = Drawing.Color.SkyBlue
        btnKensahkksoutdateUp.ForeColor = Drawing.Color.SkyBlue
        btnKensahkksoutdateDown.ForeColor = Drawing.Color.SkyBlue
        btnTooshinoUp.ForeColor = Drawing.Color.SkyBlue
        btnTooshinoDown.ForeColor = Drawing.Color.SkyBlue
        btnKensakouteinm1Up.ForeColor = Drawing.Color.SkyBlue
        btnKensakouteinm1Down.ForeColor = Drawing.Color.SkyBlue
        btnKensakouteinm2Up.ForeColor = Drawing.Color.SkyBlue
        btnKensakouteinm2Down.ForeColor = Drawing.Color.SkyBlue
        btnKensakouteinm3Up.ForeColor = Drawing.Color.SkyBlue
        btnKensakouteinm3Down.ForeColor = Drawing.Color.SkyBlue
        btnKensakouteinm4Up.ForeColor = Drawing.Color.SkyBlue
        btnKensakouteinm4Down.ForeColor = Drawing.Color.SkyBlue
        btnKensakouteinm5Up.ForeColor = Drawing.Color.SkyBlue
        btnKensakouteinm5Down.ForeColor = Drawing.Color.SkyBlue
        btnKensakouteinm6Up.ForeColor = Drawing.Color.SkyBlue
        btnKensakouteinm6Down.ForeColor = Drawing.Color.SkyBlue
        btnKensakouteinm7Up.ForeColor = Drawing.Color.SkyBlue
        btnKensakouteinm7Down.ForeColor = Drawing.Color.SkyBlue
        btnKensakouteinm8Up.ForeColor = Drawing.Color.SkyBlue
        btnKensakouteinm8Down.ForeColor = Drawing.Color.SkyBlue
        btnKensakouteinm9Up.ForeColor = Drawing.Color.SkyBlue
        btnKensakouteinm9Down.ForeColor = Drawing.Color.SkyBlue
        btnKensakouteinm10Up.ForeColor = Drawing.Color.SkyBlue
        btnKensakouteinm10Down.ForeColor = Drawing.Color.SkyBlue
        btnKensastartjissibi1Up.ForeColor = Drawing.Color.SkyBlue
        btnKensastartjissibi1Down.ForeColor = Drawing.Color.SkyBlue
        btnKensastartjissibi2Up.ForeColor = Drawing.Color.SkyBlue
        btnKensastartjissibi2Down.ForeColor = Drawing.Color.SkyBlue
        btnKensastartjissibi3Up.ForeColor = Drawing.Color.SkyBlue
        btnKensastartjissibi3Down.ForeColor = Drawing.Color.SkyBlue
        btnKensastartjissibi4Up.ForeColor = Drawing.Color.SkyBlue
        btnKensastartjissibi4Down.ForeColor = Drawing.Color.SkyBlue
        btnKensastartjissibi5Up.ForeColor = Drawing.Color.SkyBlue
        btnKensastartjissibi5Down.ForeColor = Drawing.Color.SkyBlue
        btnKensastartjissibi6Up.ForeColor = Drawing.Color.SkyBlue
        btnKensastartjissibi6Down.ForeColor = Drawing.Color.SkyBlue
        btnKensastartjissibi7Up.ForeColor = Drawing.Color.SkyBlue
        btnKensastartjissibi7Down.ForeColor = Drawing.Color.SkyBlue
        btnKensastartjissibi8Up.ForeColor = Drawing.Color.SkyBlue
        btnKensastartjissibi8Down.ForeColor = Drawing.Color.SkyBlue
        btnKensastartjissibi9Up.ForeColor = Drawing.Color.SkyBlue
        btnKensastartjissibi9Down.ForeColor = Drawing.Color.SkyBlue
        btnKensastartjissibi10Up.ForeColor = Drawing.Color.SkyBlue
        btnKensastartjissibi10Down.ForeColor = Drawing.Color.SkyBlue
        btnKensainnm1Up.ForeColor = Drawing.Color.SkyBlue
        btnKensainnm1Down.ForeColor = Drawing.Color.SkyBlue
        btnKensainnm2Up.ForeColor = Drawing.Color.SkyBlue
        btnKensainnm2Down.ForeColor = Drawing.Color.SkyBlue
        btnKensainnm3Up.ForeColor = Drawing.Color.SkyBlue
        btnKensainnm3Down.ForeColor = Drawing.Color.SkyBlue
        btnKensainnm4Up.ForeColor = Drawing.Color.SkyBlue
        btnKensainnm4Down.ForeColor = Drawing.Color.SkyBlue
        btnKensainnm5Up.ForeColor = Drawing.Color.SkyBlue
        btnKensainnm5Down.ForeColor = Drawing.Color.SkyBlue
        btnKensainnm6Up.ForeColor = Drawing.Color.SkyBlue
        btnKensainnm6Down.ForeColor = Drawing.Color.SkyBlue
        btnKensainnm7Up.ForeColor = Drawing.Color.SkyBlue
        btnKensainnm7Down.ForeColor = Drawing.Color.SkyBlue
        btnKensainnm8Up.ForeColor = Drawing.Color.SkyBlue
        btnKensainnm8Down.ForeColor = Drawing.Color.SkyBlue
        btnKensainnm9Up.ForeColor = Drawing.Color.SkyBlue
        btnKensainnm9Down.ForeColor = Drawing.Color.SkyBlue
        btnKensainnm10Up.ForeColor = Drawing.Color.SkyBlue
        btnKensainnm10Down.ForeColor = Drawing.Color.SkyBlue
        btnAddloginuseridUp.ForeColor = Drawing.Color.SkyBlue
        btnAddloginuseridDown.ForeColor = Drawing.Color.SkyBlue
        btnAdddatetimeUp.ForeColor = Drawing.Color.SkyBlue
        btnAdddatetimeDown.ForeColor = Drawing.Color.SkyBlue
        btnUpdloginuseridUp.ForeColor = Drawing.Color.SkyBlue
        btnUpddatetimeDown.ForeColor = Drawing.Color.SkyBlue
        btnUpdloginuseridUp.ForeColor = Drawing.Color.SkyBlue
        btnUpdloginuseridDown.ForeColor = Drawing.Color.SkyBlue
        btnUpddatetimeUp.ForeColor = Drawing.Color.SkyBlue
        btnUpddatetimeDown.ForeColor = Drawing.Color.SkyBlue

    End Sub

    ''' <summary>
    ''' <summary>リンクボタンの表示を設定</summary>
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2015/12/07　高兵兵(大連)　新規作成</para>
    ''' </history>
    Public Sub SetUpDownHyouji(ByVal blnHyoujiFlg As Boolean)

        If blnHyoujiFlg = True Then
            btnKanrinoUp.Visible = True
            btnKanrinoDown.Visible = True
            btnKbnUp.Visible = True
            btnKbnDown.Visible = True
            btnHosyousyonoUp.Visible = True
            btnHosyousyonoDown.Visible = True
            btnKameitencdUp.Visible = True
            btnKameitencdDown.Visible = True
            btnKakunoudateUp.Visible = True
            btnKakunoudateDown.Visible = True
            btnKameitenmeiUp.Visible = True
            btnKameitenmeiDown.Visible = True
            btnSesyumeiUp.Visible = True
            btnSesyumeiDown.Visible = True
            btnTorikesiUp.Visible = True
            btnTorikesiDown.Visible = True
            btnKensahkksbusuuUp.Visible = True
            btnKensahkksbusuuDown.Visible = True
            btnKensahkksjyuusyo1Up.Visible = True
            btnKensahkksjyuusyo1Down.Visible = True
            btnKensahkksjyuusyo2Up.Visible = True
            btnKensahkksjyuusyo2Down.Visible = True
            btnYuubinnoUp.Visible = True
            btnYuubinnoDown.Visible = True
            btnTelnoUp.Visible = True
            btnTelnoDown.Visible = True
            btnBusyomeiUp.Visible = True
            btnBusyomeiDown.Visible = True
            btnTodouhukencdUp.Visible = True
            btnTodouhukencdDown.Visible = True
            btnTodouhukenmeiUp.Visible = True
            btnTodouhukenmeiDown.Visible = True
            btnHassoudateUp.Visible = True
            btnHassoudateDown.Visible = True
            btnHassoudateinflgUp.Visible = True
            btnHassoudateinflgDown.Visible = True
            btnSouhutantousyaUp.Visible = True
            btnSouhutantousyaDown.Visible = True
            btnBukkenjyuusyo1Up.Visible = True
            btnBukkenjyuusyo1Down.Visible = True
            btnBukkenjyuusyo2Up.Visible = True
            btnBukkenjyuusyo2Down.Visible = True
            btnBukkenjyuusyo3Up.Visible = True
            btnBukkenjyuusyo3Down.Visible = True
            btnTatemonokouzouUp.Visible = True
            btnTatemonokouzouDown.Visible = True
            btnTatemonokaisuUp.Visible = True
            btnTatemonokaisuDown.Visible = True
            btnFcnmUp.Visible = True
            btnFcnmDown.Visible = True
            btnKameitentantoUp.Visible = True
            btnKameitentantoDown.Visible = True
            btnTatemonokameitencdUp.Visible = True
            btnTatemonokameitencdDown.Visible = True
            btnKanrihyououtflgUp.Visible = True
            btnKanrihyououtflgDown.Visible = True
            btnKanrihyououtdateUp.Visible = True
            btnKanrihyououtdateDown.Visible = True
            btnSouhujyououtflgUp.Visible = True
            btnSouhujyououtflgDown.Visible = True
            btnSouhujyououtdateUp.Visible = True
            btnSouhujyououtdateDown.Visible = True
            btnKensahkksoutflgUp.Visible = True
            btnKensahkksoutflgDown.Visible = True
            btnKensahkksoutdateUp.Visible = True
            btnKensahkksoutdateDown.Visible = True
            btnTooshinoUp.Visible = True
            btnTooshinoDown.Visible = True
            btnKensakouteinm1Up.Visible = True
            btnKensakouteinm1Down.Visible = True
            btnKensakouteinm2Up.Visible = True
            btnKensakouteinm2Down.Visible = True
            btnKensakouteinm3Up.Visible = True
            btnKensakouteinm3Down.Visible = True
            btnKensakouteinm4Up.Visible = True
            btnKensakouteinm4Down.Visible = True
            btnKensakouteinm5Up.Visible = True
            btnKensakouteinm5Down.Visible = True
            btnKensakouteinm6Up.Visible = True
            btnKensakouteinm6Down.Visible = True
            btnKensakouteinm7Up.Visible = True
            btnKensakouteinm7Down.Visible = True
            btnKensakouteinm8Up.Visible = True
            btnKensakouteinm8Down.Visible = True
            btnKensakouteinm9Up.Visible = True
            btnKensakouteinm9Down.Visible = True
            btnKensakouteinm10Up.Visible = True
            btnKensakouteinm10Down.Visible = True
            btnKensastartjissibi1Up.Visible = True
            btnKensastartjissibi1Down.Visible = True
            btnKensastartjissibi2Up.Visible = True
            btnKensastartjissibi2Down.Visible = True
            btnKensastartjissibi3Up.Visible = True
            btnKensastartjissibi3Down.Visible = True
            btnKensastartjissibi4Up.Visible = True
            btnKensastartjissibi4Down.Visible = True
            btnKensastartjissibi5Up.Visible = True
            btnKensastartjissibi5Down.Visible = True
            btnKensastartjissibi6Up.Visible = True
            btnKensastartjissibi6Down.Visible = True
            btnKensastartjissibi7Up.Visible = True
            btnKensastartjissibi7Down.Visible = True
            btnKensastartjissibi8Up.Visible = True
            btnKensastartjissibi8Down.Visible = True
            btnKensastartjissibi9Up.Visible = True
            btnKensastartjissibi9Down.Visible = True
            btnKensastartjissibi10Up.Visible = True
            btnKensastartjissibi10Down.Visible = True
            btnKensainnm1Up.Visible = True
            btnKensainnm1Down.Visible = True
            btnKensainnm2Up.Visible = True
            btnKensainnm2Down.Visible = True
            btnKensainnm3Up.Visible = True
            btnKensainnm3Down.Visible = True
            btnKensainnm4Up.Visible = True
            btnKensainnm4Down.Visible = True
            btnKensainnm5Up.Visible = True
            btnKensainnm5Down.Visible = True
            btnKensainnm6Up.Visible = True
            btnKensainnm6Down.Visible = True
            btnKensainnm7Up.Visible = True
            btnKensainnm7Down.Visible = True
            btnKensainnm8Up.Visible = True
            btnKensainnm8Down.Visible = True
            btnKensainnm9Up.Visible = True
            btnKensainnm9Down.Visible = True
            btnKensainnm10Up.Visible = True
            btnKensainnm10Down.Visible = True
            btnAddloginuseridUp.Visible = True
            btnAddloginuseridDown.Visible = True
            btnAdddatetimeUp.Visible = True
            btnAdddatetimeDown.Visible = True
            btnUpdloginuseridUp.Visible = True
            btnUpddatetimeDown.Visible = True
            btnUpdloginuseridUp.Visible = True
            btnUpdloginuseridDown.Visible = True
            btnUpddatetimeUp.Visible = True
            btnUpddatetimeDown.Visible = True
        Else
            btnKanrinoUp.Visible = False
            btnKanrinoDown.Visible = False
            btnKbnUp.Visible = False
            btnKbnDown.Visible = False
            btnHosyousyonoUp.Visible = False
            btnHosyousyonoDown.Visible = False
            btnKameitencdUp.Visible = False
            btnKameitencdDown.Visible = False
            btnKakunoudateUp.Visible = False
            btnKakunoudateDown.Visible = False
            btnKameitenmeiUp.Visible = False
            btnKameitenmeiDown.Visible = False
            btnSesyumeiUp.Visible = False
            btnSesyumeiDown.Visible = False
            btnTorikesiUp.Visible = False
            btnTorikesiDown.Visible = False
            btnKensahkksbusuuUp.Visible = False
            btnKensahkksbusuuDown.Visible = False
            btnKensahkksjyuusyo1Up.Visible = False
            btnKensahkksjyuusyo1Down.Visible = False
            btnKensahkksjyuusyo2Up.Visible = False
            btnKensahkksjyuusyo2Down.Visible = False
            btnYuubinnoUp.Visible = False
            btnYuubinnoDown.Visible = False
            btnTelnoUp.Visible = False
            btnTelnoDown.Visible = False
            btnBusyomeiUp.Visible = False
            btnBusyomeiDown.Visible = False
            btnTodouhukencdUp.Visible = False
            btnTodouhukencdDown.Visible = False
            btnTodouhukenmeiUp.Visible = False
            btnTodouhukenmeiDown.Visible = False
            btnHassoudateUp.Visible = False
            btnHassoudateDown.Visible = False
            btnHassoudateinflgUp.Visible = False
            btnHassoudateinflgDown.Visible = False
            btnSouhutantousyaUp.Visible = False
            btnSouhutantousyaDown.Visible = False
            btnBukkenjyuusyo1Up.Visible = False
            btnBukkenjyuusyo1Down.Visible = False
            btnBukkenjyuusyo2Up.Visible = False
            btnBukkenjyuusyo2Down.Visible = False
            btnBukkenjyuusyo3Up.Visible = False
            btnBukkenjyuusyo3Down.Visible = False
            btnTatemonokouzouUp.Visible = False
            btnTatemonokouzouDown.Visible = False
            btnTatemonokaisuUp.Visible = False
            btnTatemonokaisuDown.Visible = False
            btnFcnmUp.Visible = False
            btnFcnmDown.Visible = False
            btnKameitentantoUp.Visible = False
            btnKameitentantoDown.Visible = False
            btnTatemonokameitencdUp.Visible = False
            btnTatemonokameitencdDown.Visible = False
            btnKanrihyououtflgUp.Visible = False
            btnKanrihyououtflgDown.Visible = False
            btnKanrihyououtdateUp.Visible = False
            btnKanrihyououtdateDown.Visible = False
            btnSouhujyououtflgUp.Visible = False
            btnSouhujyououtflgDown.Visible = False
            btnSouhujyououtdateUp.Visible = False
            btnSouhujyououtdateDown.Visible = False
            btnKensahkksoutflgUp.Visible = False
            btnKensahkksoutflgDown.Visible = False
            btnKensahkksoutdateUp.Visible = False
            btnKensahkksoutdateDown.Visible = False
            btnTooshinoUp.Visible = False
            btnTooshinoDown.Visible = False
            btnKensakouteinm1Up.Visible = False
            btnKensakouteinm1Down.Visible = False
            btnKensakouteinm2Up.Visible = False
            btnKensakouteinm2Down.Visible = False
            btnKensakouteinm3Up.Visible = False
            btnKensakouteinm3Down.Visible = False
            btnKensakouteinm4Up.Visible = False
            btnKensakouteinm4Down.Visible = False
            btnKensakouteinm5Up.Visible = False
            btnKensakouteinm5Down.Visible = False
            btnKensakouteinm6Up.Visible = False
            btnKensakouteinm6Down.Visible = False
            btnKensakouteinm7Up.Visible = False
            btnKensakouteinm7Down.Visible = False
            btnKensakouteinm8Up.Visible = False
            btnKensakouteinm8Down.Visible = False
            btnKensakouteinm9Up.Visible = False
            btnKensakouteinm9Down.Visible = False
            btnKensakouteinm10Up.Visible = False
            btnKensakouteinm10Down.Visible = False
            btnKensastartjissibi1Up.Visible = False
            btnKensastartjissibi1Down.Visible = False
            btnKensastartjissibi2Up.Visible = False
            btnKensastartjissibi2Down.Visible = False
            btnKensastartjissibi3Up.Visible = False
            btnKensastartjissibi3Down.Visible = False
            btnKensastartjissibi4Up.Visible = False
            btnKensastartjissibi4Down.Visible = False
            btnKensastartjissibi5Up.Visible = False
            btnKensastartjissibi5Down.Visible = False
            btnKensastartjissibi6Up.Visible = False
            btnKensastartjissibi6Down.Visible = False
            btnKensastartjissibi7Up.Visible = False
            btnKensastartjissibi7Down.Visible = False
            btnKensastartjissibi8Up.Visible = False
            btnKensastartjissibi8Down.Visible = False
            btnKensastartjissibi9Up.Visible = False
            btnKensastartjissibi9Down.Visible = False
            btnKensastartjissibi10Up.Visible = False
            btnKensastartjissibi10Down.Visible = False
            btnKensainnm1Up.Visible = False
            btnKensainnm1Down.Visible = False
            btnKensainnm2Up.Visible = False
            btnKensainnm2Down.Visible = False
            btnKensainnm3Up.Visible = False
            btnKensainnm3Down.Visible = False
            btnKensainnm4Up.Visible = False
            btnKensainnm4Down.Visible = False
            btnKensainnm5Up.Visible = False
            btnKensainnm5Down.Visible = False
            btnKensainnm6Up.Visible = False
            btnKensainnm6Down.Visible = False
            btnKensainnm7Up.Visible = False
            btnKensainnm7Down.Visible = False
            btnKensainnm8Up.Visible = False
            btnKensainnm8Down.Visible = False
            btnKensainnm9Up.Visible = False
            btnKensainnm9Down.Visible = False
            btnKensainnm10Up.Visible = False
            btnKensainnm10Down.Visible = False
            btnAddloginuseridUp.Visible = False
            btnAddloginuseridDown.Visible = False
            btnAdddatetimeUp.Visible = False
            btnAdddatetimeDown.Visible = False
            btnUpdloginuseridUp.Visible = False
            btnUpddatetimeDown.Visible = False
            btnUpdloginuseridUp.Visible = False
            btnUpdloginuseridDown.Visible = False
            btnUpddatetimeUp.Visible = False
            btnUpddatetimeDown.Visible = False
        End If

    End Sub

    ''' <summary>
    ''' エラーメッセージ表示
    ''' </summary>
    ''' <param name="strMessage">メッセージ</param>
    ''' <param name="strObjId">クライアントID</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2015/12/07　高兵兵(大連)　新規作成</para>
    ''' </history>
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
    ''' 日付型変更処理
    ''' </summary>
    ''' <param name="s"></param>
    ''' <returns></returns>
    ''' <history>
    ''' <para>2015/12/07　高兵兵(大連)　新規作成</para>
    ''' </history>
    Public Function GetstrDate(ByVal s As Object) As String
        If s Is DBNull.Value OrElse s.ToString = String.Empty Then
            Return String.Empty
        Else
            Return CStr(s).Substring(0, 10)
        End If
    End Function

    ''' <summary>
    ''' 検査報告書管理テーブルを作成する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2015/12/07 高兵兵(大連) 新規作成</para>
    ''' </history>
    Private Function CreateKensaHkksKanriTable() As Data.DataTable
        'EMAB障害対応情報の格納処理
        Dim dt As New Data.DataTable
        With dt.Columns
            .Add(New Data.DataColumn("kanri_no"))
            .Add(New Data.DataColumn("kbn"))
            .Add(New Data.DataColumn("hosyousyo_no"))
            .Add(New Data.DataColumn("kameiten_cd"))
        End With

        Return dt
    End Function
End Class
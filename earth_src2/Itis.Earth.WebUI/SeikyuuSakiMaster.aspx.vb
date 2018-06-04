Imports Itis.Earth.BizLogic
Imports System.Data

Partial Public Class SeikyuuSakiMaster
    Inherits System.Web.UI.Page
    'ボタン
    Private blnBtn As Boolean
    'インスタンス生成
    Private CommonSearchLogic As New Itis.Earth.BizLogic.CommonSearchLogic
    '共通チェック
    Private commoncheck As New CommonCheck
    'インスタンス生成
    Private SeikyuuSakiMasterBL As New Itis.Earth.BizLogic.SeikyuuSakiMasterLogic

    Private Const SEP_STRING As String = "$$$"

    ''' <summary>
    ''' ページロード
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '権限チェック↓
        Dim strNinsyou As String = ""
        Dim commonChk As New CommonCheck
        Dim strUserID As String = ""
        '権限チェックおよび設定
        blnBtn = commonChk.CommonNinnsyou(strUserID, "keiri_gyoumu_kengen")
        ViewState("UserId") = strUserID

        If Not IsPostBack Then

            'DDLの初期設定
            SetDdlListInf()

            ''調査会社マスタ画面から、
            If Request("sendSearchTerms") <> "" Then
                Dim arrSearchTerm() As String = Split(Request("sendSearchTerms"), SEP_STRING)
                tbxSeikyuuSaki_Cd.Text = arrSearchTerm(0)                      '親画面からPOSTされた情報1 ：請求先コード
                tbxSeikyuuSaki_Brc.Text = arrSearchTerm(1)                     '親画面からPOSTされた情報1 ：請求先コード
                SetDropSelect(ddlSeikyuuSaki_Kbn, arrSearchTerm(2))             '親画面からPOSTされた情報1 ：請求先コード

                If arrSearchTerm.Length = 4 Then
                    '請求先種類のセット
                    If arrSearchTerm(3) = "1" Then
                        Me.lblTyousaKoujiKbn.Text = "※調査請求先"
                    ElseIf arrSearchTerm(3) = "2" Then
                        Me.lblTyousaKoujiKbn.Text = "※工事請求先"
                    End If
                End If

                '明細データ取得
                GetMeisaiData(tbxSeikyuuSaki_Cd.Text, tbxSeikyuuSaki_Brc.Text, ddlSeikyuuSaki_Kbn.SelectedValue, "btnSearch")

                Me.btnBack.Text = "閉じる"
                ViewState.Item("Flg") = "close"
            Else
                '修正ボタン
                btnSyuusei.Enabled = False
                '登録ボタン
                btnTouroku.Enabled = True
                Me.btnKensakuSeikyuuSaki.Enabled = True

                Me.btnBack.Text = "戻る"
                ViewState.Item("Flg") = "back"
            End If
        Else
            If hidConfirm.Value = "OK" Then
                '請求先登録雛形マスタ設定
                SetSeikyuuSakiHinagata()
            End If
        End If

        '回収境界額
        Me.tbxKaisyuuKyoukaigaku.Attributes.Add("onblur", "checkNumberAddFig(this);")
        Me.tbxKaisyuuKyoukaigaku.Attributes.Add("onfocus", "removeFig(this);")

        '請求先名
        tbxSeikyuuSakiMei.Attributes.Add("readonly", "true;")
        '請求先名
        tbxSeikyuuSaki_Mei.Attributes.Add("readonly", "true;")
        '新会計事業所
        tbxSkkJigyousyoMei.Attributes.Add("readonly", "true;")

        '名寄先名
        tbxNayoseMei.Attributes.Add("readonly", "true;")

        '明細クリア
        btnClearMeisai.Attributes.Add("onclick", "if(!confirm('クリアを行ないます。\nよろしいですか？')){return false;};disableButton1();")

        'disableButton
        btnSearchSeikyuuSaki.Attributes.Add("onclick", "disableButton1();")
        btnKihonJyouhouSet.Attributes.Add("onclick", "disableButton1();")
        btnSearch.Attributes.Add("onclick", "disableButton1();")
        btnClear.Attributes.Add("onclick", "disableButton1();")
        btnSyuusei.Attributes.Add("onclick", "disableButton1();")
        btnTouroku.Attributes.Add("onclick", "disableButton1();")
        btnKensakuSeikyuuSaki.Attributes.Add("onclick", "disableButton1();")
        btnSkkJigyousyo.Attributes.Add("onclick", "disableButton1();")
        btnKensakuYuubinNo.Attributes.Add("onclick", "disableButton1();")
        '20100925　名寄先コード、名寄先名　追加　馬艶軍↓↓↓
        btnKensakuNayose.Attributes.Add("onclick", "disableButton1();")
        '20100925　名寄先コード、名寄先名　追加　馬艶軍↑↑↑
        '郵便番号
        Me.tbxYuubinNo.Attributes.Add("onblur", "SetYuubinNo(this)")

        '==================2011/06/16 車龍 ｢注意事項｣ボタン追加の対応 追加 開始↓========================== 
        '｢注意情報｣ボタン
        Me.btnTyuuijikou.Attributes.Add("onClick", "fncTyuuijikouPopup();return false;")

        '==================2011/06/16 車龍 ｢注意事項｣ボタン追加の対応 追加 終了↑========================== 

        '===========2012/05/15 車龍 407553の対応 追加↓========================
        '「採番」ボタンをセットする
        Call Me.SetBtnSaiban()
        '統合会計得意先ｺｰﾄﾞ
        Me.tbxTougouKaikeiTokusakiCd.Attributes.Add("onPropertyChange", "fncSetBtnSaiban();")
        '===========2012/05/15 車龍 407553の対応 追加↑========================

        MakeScript()
        If Not blnBtn Then
            btnSyuusei.Enabled = False
            btnTouroku.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' 絞込編集ボタン押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim strErr As String = ""
        Dim strID As String = ""

        '請求先区分
        If strErr = "" Then
            '入力必須
            strErr = commoncheck.CheckHissuNyuuryoku(ddlSeikyuuSaki_Kbn.SelectedValue, "請求先区分")
            strID = ddlSeikyuuSaki_Kbn.ClientID
        End If

        '請求先コード
        If strErr = "" Then
            '入力必須
            strErr = commoncheck.CheckHissuNyuuryoku(tbxSeikyuuSaki_Cd.Text, "請求先コード")
            strID = tbxSeikyuuSaki_Cd.ClientID
        End If
        If strErr = "" Then
            '半角英数字
            strErr = commoncheck.ChkHankakuEisuuji(tbxSeikyuuSaki_Cd.Text, "請求先コード")
            strID = tbxSeikyuuSaki_Cd.ClientID
        End If

        '請求先枝番
        If strErr = "" Then
            '入力必須
            strErr = commoncheck.CheckHissuNyuuryoku(tbxSeikyuuSaki_Brc.Text, "請求先枝番")
            strID = tbxSeikyuuSaki_Brc.ClientID
        End If
        If strErr = "" Then
            '半角英数字
            strErr = commoncheck.ChkHankakuEisuuji(tbxSeikyuuSaki_Brc.Text, "請求先枝番")
            strID = tbxSeikyuuSaki_Brc.ClientID
        End If

        If strErr <> "" Then
            strErr = "alert('" & strErr & "');document.getElementById('" & strID & "').focus();"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strErr, True)
        Else
            '明細データ取得
            GetMeisaiData(tbxSeikyuuSaki_Cd.Text, tbxSeikyuuSaki_Brc.Text, ddlSeikyuuSaki_Kbn.SelectedValue, "btnSearch")
        End If
    End Sub

    ''' <summary>
    ''' 登録ボタン押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnTouroku_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTouroku.Click
        Dim strErr As String = ""
        Dim strID As String = ""
        strID = InputCheck(strErr)

        '新会計事業所コード存在チェック
        If strErr = "" And Trim(tbxSkkJigyousyoCd.Text) <> "" Then
            Dim dtTable As New Data.DataTable
            dtTable = SeikyuuSakiMasterBL.SelSinkaikeiJigyousyoInfo(Trim(tbxSkkJigyousyoCd.Text))
            If dtTable.Rows.Count <> 1 Then
                strErr = String.Format(Messages.Instance.MSG2036E, "新会計事業所マスタ").ToString
                strID = tbxSkkJigyousyoCd.ClientID
                tbxSkkJigyousyoMei.Text = ""
            End If
        End If

        '郵便番号存在チェック
        If strErr = "" And Trim(tbxYuubinNo.Text) <> "" Then
            Dim dtTable As New Data.DataTable
            dtTable = SeikyuuSakiMasterBL.SelYuubinInfo(Trim(tbxYuubinNo.Text.Replace("-", "")))
            If dtTable.Rows.Count <> 1 Then
                strErr = String.Format(Messages.Instance.MSG2036E, "郵便番号マスタ").ToString
                strID = tbxYuubinNo.ClientID
            End If
        End If

        'エラーがある時
        If strErr <> "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('" & strErr & "');document.getElementById('" & strID & "').focus();", True)
        Else
            '重複チェック
            Dim dtSeikyuuSakiDataSet As New Itis.Earth.DataAccess.SeikyuuSakiDataSet.m_seikyuu_sakiDataTable
            dtSeikyuuSakiDataSet = SeikyuuSakiMasterBL.SelSeikyuuSakiInfo(tbxSeikyuuSakiCd.Text, tbxSeikyuuSakiBrc.Text, ddlSeikyuuSakiKbn.SelectedValue)
            If dtSeikyuuSakiDataSet.Rows.Count <> 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('マスターに重複データが存在します。');document.getElementById('" & ddlSeikyuuSakiKbn.ClientID & "').focus();", True)
                Return
            End If

            '各マスタの存在チェック
            If Me.ddlSeikyuuSakiKbn.Text = "0" Or Me.ddlSeikyuuSakiKbn.Text = "1" Or Me.ddlSeikyuuSakiKbn.Text = "2" Then
                If SeikyuuSakiMasterBL.SelSonzaiChk(Me.ddlSeikyuuSakiKbn.Text, Me.tbxSeikyuuSakiCd.Text, Me.tbxSeikyuuSakiBrc.Text).Rows.Count < 1 Then
                    Select Case Me.ddlSeikyuuSakiKbn.Text
                        Case "0"
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('加盟店マスタに存在しないコードが設定されいています。');document.getElementById('" & tbxSeikyuuSakiCd.ClientID & "').focus();", True)
                            Return
                        Case "1"
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('調査会社マスタに存在しないコードが設定されいています。');document.getElementById('" & tbxSeikyuuSakiCd.ClientID & "').focus();", True)
                            Return
                        Case "2"
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('営業所マスタに存在しないコードが設定されいています。');document.getElementById('" & tbxSeikyuuSakiCd.ClientID & "').focus();", True)
                            Return
                        Case Else
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('不正な請求先区分が選択されています。');document.getElementById('" & tbxSeikyuuSakiCd.ClientID & "').focus();", True)
                            Return
                    End Select
                End If
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('不正な請求先区分が選択されています。');document.getElementById('" & ddlSeikyuuSakiKbn.ClientID & "').focus();", True)
                Return
            End If

            'データ登録
            If SeikyuuSakiMasterBL.InsSeikyuuSaki(SetMeisaiData) Then
                strErr = "alert('" & Replace(Messages.Instance.MSG018S, "@PARAM1", "請求先マスタ") & "');"
                '再取得
                GetMeisaiData(tbxSeikyuuSakiCd.Text, tbxSeikyuuSakiBrc.Text, ddlSeikyuuSakiKbn.SelectedValue, "btnTouroku")
            Else
                strErr = "alert('" & Replace(Messages.Instance.MSG019E, "@PARAM1", "請求先マスタ") & "');"
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strErr, True)
        End If
    End Sub

    ''' <summary>
    ''' 修正ボタン押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSyuusei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSyuusei.Click
        Dim strReturn As String = ""
        Dim strErr As String = ""
        Dim strID As String = ""

        'チェック
        strID = InputCheck(strErr)

        '新会計事業所コード存在チェック
        If strErr = "" And Trim(tbxSkkJigyousyoCd.Text) <> "" Then
            Dim dtTable As New Data.DataTable
            dtTable = SeikyuuSakiMasterBL.SelSinkaikeiJigyousyoInfo(Trim(tbxSkkJigyousyoCd.Text))
            If dtTable.Rows.Count <> 1 Then
                strErr = String.Format(Messages.Instance.MSG2036E, "新会計事業所マスタ").ToString
                strID = tbxSkkJigyousyoCd.ClientID
                tbxSkkJigyousyoMei.Text = ""
            End If
        End If

        '郵便番号存在チェック
        If strErr = "" And Trim(tbxYuubinNo.Text) <> "" Then
            Dim dtTable As New Data.DataTable
            dtTable = SeikyuuSakiMasterBL.SelYuubinInfo(Trim(tbxYuubinNo.Text.Replace("-", "")))
            If dtTable.Rows.Count <> 1 Then
                strErr = String.Format(Messages.Instance.MSG2036E, "郵便番号マスタ").ToString
                strID = tbxYuubinNo.ClientID
            End If
        End If

        'エラーがある時
        If strErr <> "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('" & strErr & "');document.getElementById('" & strID & "').focus();", True)
        Else
            '更新処理
            strReturn = SeikyuuSakiMasterBL.UpdSeikyuuSaki(SetMeisaiData)
            If strReturn = "0" Then
                '更新成功
                strErr = "alert('" & Replace(Messages.Instance.MSG018S, "@PARAM1", "請求先マスタ") & "');"
                '画面再描画処理
                GetMeisaiData(tbxSeikyuuSakiCd.Text, tbxSeikyuuSakiBrc.Text, ddlSeikyuuSakiKbn.SelectedValue, "btnSyuusei")
            ElseIf strReturn = "1" Then
                '更新失敗
                strErr = "alert('" & Replace(Messages.Instance.MSG019E, "@PARAM1", "請求先マスタ") & "');"
            ElseIf strReturn = "2" Then
                '存在チェック
                strErr = "alert('該当データが存在しません。既に削除されている可能性があります。');"
            Else
                'その他
                strErr = "alert('" & strReturn & "');"
            End If
            'メッセージ表示
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strErr, True)
        End If

    End Sub

    ''' <summary>
    ''' 郵便番号.検索ボタン押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnKensakuYuubinNo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKensakuYuubinNo.Click
        '住所
        Dim data As DataSet
        Dim jyuusyo As String
        Dim jyuusyoMei As String
        Dim jyuusyoNo As String

        '住所取得
        Dim csScript As New StringBuilder

        data = (SeikyuuSakiMasterBL.GetMailAddress(Me.tbxYuubinNo.Text.Replace("-", String.Empty).Trim))
        If data.Tables(0).Rows.Count = 1 Then

            jyuusyo = data.Tables(0).Rows(0).Item(0).ToString
            jyuusyoNo = jyuusyo.Split(",")(0)
            jyuusyoMei = GetJyusho(jyuusyo.Split(",")(1))
            If jyuusyoNo.Length > 3 Then
                jyuusyoNo = jyuusyoNo.Substring(0, 3) & "-" & jyuusyoNo.Substring(3, jyuusyoNo.Length - 3)
            End If

            csScript.AppendLine("if(document.getElementById('" & Me.tbxJyuusyo1.ClientID & "').value!='' || document.getElementById('" & Me.tbxJyuusyo2.ClientID & "').value!=''){" & vbCrLf)
            csScript.AppendLine("if (!confirm('既存データがありますが上書きしてよろしいですか。')){}else{ " & vbCrLf)

            csScript.AppendLine("document.getElementById('" & Me.tbxJyuusyo1.ClientID & "').value = '" & jyuusyoMei.Split(",")(0) & "';" & vbCrLf)
            csScript.AppendLine("document.getElementById('" & Me.tbxJyuusyo2.ClientID & "').value = '" & jyuusyoMei.Split(",")(1) & "';" & vbCrLf)
            csScript.AppendLine("document.getElementById('" & Me.tbxYuubinNo.ClientID & "').value = '" & jyuusyoNo & "';" & vbCrLf)

            csScript.AppendLine("}" & vbCrLf)

            csScript.AppendLine("}else{" & vbCrLf)
            csScript.AppendLine("document.getElementById('" & Me.tbxJyuusyo1.ClientID & "').value = '" & jyuusyoMei.Split(",")(0) & "';" & vbCrLf)
            csScript.AppendLine("document.getElementById('" & Me.tbxJyuusyo2.ClientID & "').value = '" & jyuusyoMei.Split(",")(1) & "';" & vbCrLf)
            csScript.AppendLine("document.getElementById('" & Me.tbxYuubinNo.ClientID & "').value = '" & jyuusyoNo & "';" & vbCrLf)

            csScript.AppendLine("}" & vbCrLf)
        Else
            csScript.AppendLine("fncOpenwindowYuubin('" & Me.tbxYuubinNo.ClientID & "','" & Me.tbxJyuusyo1.ClientID & "','" & Me.tbxJyuusyo2.ClientID & "');")
        End If
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "openWindowYuubin", csScript.ToString, True)
    End Sub

    ''' <summary>
    ''' 住所１、２取得
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetJyusho(ByVal value As String) As String
        Dim i As Integer
        If value.Length > 20 Then
            For i = 20 To value.Length
                If System.Text.Encoding.Default.GetBytes(Left(value, i)).Length >= 39 Then
                    Return value.Substring(0, i) & "," & value.Substring(i, value.Length - i)
                End If
            Next
        End If
        Return value & ","
    End Function

    ''' <summary>
    ''' 基本情報セット
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnKihonJyouhouSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKihonJyouhouSet.Click
        Dim strErr As String = ""
        Dim strID As String = ""
        'リストの選択チェック
        If ddlSeikyuuSakiTourokuHinagata.SelectedValue = "" Then
            strErr = "基本情報が未選択です。"
        End If

        If strErr <> "" Then
            strErr = "alert('" & strErr & "');document.getElementById('" & ddlSeikyuuSakiTourokuHinagata.ClientID & "').focus();"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strErr, True)
        Else
            '確認メッセージの表示
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "fncConfirm();", True)
        End If
    End Sub

    ''' <summary>
    ''' 請求先.検索ボタン押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSearchSeikyuuSaki_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearchSeikyuuSaki.Click

        Dim strScript As String = ""
        '請求先情報取得
        Dim dtSeikyuuSakiTable As New Itis.Earth.DataAccess.CommonSearchDataSet.SeikyuuSakiTableDataTable
        'dtSeikyuuSakiTable = CommonSearchLogic.GetSeikyuuSakiInfo("2", ddlSeikyuuSaki_Kbn.SelectedValue, tbxSeikyuuSaki_Cd.Text, tbxSeikyuuSaki_Brc.Text, "", False)
        dtSeikyuuSakiTable = SeikyuuSakiMasterBL.SelVSeikyuuSakiInfo(ddlSeikyuuSaki_Kbn.SelectedValue, tbxSeikyuuSaki_Cd.Text, tbxSeikyuuSaki_Brc.Text, False)

        '検索結果が1件だった場合
        If dtSeikyuuSakiTable.Rows.Count = 1 Then
            '請求先コード
            tbxSeikyuuSaki_Cd.Text = TrimNull(dtSeikyuuSakiTable.Item(0).seikyuu_saki_cd)
            '請求先枝番
            tbxSeikyuuSaki_Brc.Text = TrimNull(dtSeikyuuSakiTable.Item(0).seikyuu_saki_brc)
            '請求先区分
            SetDropSelect(ddlSeikyuuSaki_Kbn, TrimNull(dtSeikyuuSakiTable.Item(0).seikyuu_saki_kbn))
            '請求先名
            tbxSeikyuuSaki_Mei.Text = TrimNull(dtSeikyuuSakiTable.Item(0).seikyuu_saki_mei)
        Else
            '請求先名
            tbxSeikyuuSaki_Mei.Text = ""
            strScript = "objSrchWin = window.open('search_Seikyuusaki.aspx?blnDelete=False&Kbn='+escape('請求先')+'&FormName=" & _
                                             Me.Page.Form.Name & "&objKbn=" & _
                                             ddlSeikyuuSaki_Kbn.ClientID & _
                                             "&objCd=" & tbxSeikyuuSaki_Cd.ClientID & _
                                             "&objBrc=" & tbxSeikyuuSaki_Brc.ClientID & _
                                             "&objMei=" & tbxSeikyuuSaki_Mei.ClientID & _
                                             "&strKbn='+escape(eval('document.all.'+'" & _
                                             ddlSeikyuuSaki_Kbn.ClientID & "').value)+'&strCd='+escape(eval('document.all.'+'" & _
                                             tbxSeikyuuSaki_Cd.ClientID & "').value)+'&strBrc='+escape(eval('document.all.'+'" & _
                                             tbxSeikyuuSaki_Brc.ClientID & "').value), 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strScript, True)
        End If
    End Sub

    ''' <summary>
    ''' 請求先.検索ボタン押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnKensakuSeikyuuSaki_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKensakuSeikyuuSaki.Click

        Dim strScript As String = ""
        '請求先情報取得
        Dim dtSeikyuuSakiTable As New Itis.Earth.DataAccess.CommonSearchDataSet.SeikyuuSakiTableDataTable
        dtSeikyuuSakiTable = SeikyuuSakiMasterBL.SelVSeikyuuSakiInfo(ddlSeikyuuSakiKbn.SelectedValue, tbxSeikyuuSakiCd.Text, tbxSeikyuuSakiBrc.Text, False)

        '検索結果が1件だった場合
        If dtSeikyuuSakiTable.Rows.Count = 1 Then
            '請求先コード
            tbxSeikyuuSakiCd.Text = TrimNull(dtSeikyuuSakiTable.Item(0).seikyuu_saki_cd)
            '請求先枝番
            tbxSeikyuuSakiBrc.Text = TrimNull(dtSeikyuuSakiTable.Item(0).seikyuu_saki_brc)
            '請求先区分
            SetDropSelect(ddlSeikyuuSakiKbn, TrimNull(dtSeikyuuSakiTable.Item(0).seikyuu_saki_kbn))
            '請求先名
            tbxSeikyuuSakiMei.Text = TrimNull(dtSeikyuuSakiTable.Item(0).seikyuu_saki_mei)

            GetMeisaiData1(tbxSeikyuuSakiCd.Text, tbxSeikyuuSakiBrc.Text, ddlSeikyuuSakiKbn.SelectedValue)
        Else
            '請求先名
            tbxSeikyuuSakiMei.Text = ""
            strScript = "objSrchWin = window.open('search_Seikyuusaki.aspx?blnDelete=False&Kbn='+escape('請求先')+'&FormName=" & _
                                             Me.Page.Form.Name & "&objKbn=" & _
                                             ddlSeikyuuSakiKbn.ClientID & _
                                             "&objCd=" & tbxSeikyuuSakiCd.ClientID & _
                                             "&objBrc=" & tbxSeikyuuSakiBrc.ClientID & _
                                             "&objMei=" & tbxSeikyuuSakiMei.ClientID & _
                                             "&objBtn=" & btnOK.ClientID & _
                                             "&strKbn='+escape(eval('document.all.'+'" & _
                                             ddlSeikyuuSakiKbn.ClientID & "').value)+'&strCd='+escape(eval('document.all.'+'" & _
                                             tbxSeikyuuSakiCd.ClientID & "').value)+'&strBrc='+escape(eval('document.all.'+'" & _
                                             tbxSeikyuuSakiBrc.ClientID & "').value), 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strScript, True)
        End If
    End Sub

    ''' <summary>
    ''' 新会計事業所検索ボタンボタン押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSkkJigyousyo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSkkJigyousyo.Click
        Dim strScript As String = ""
        'データ取得SelSinkaikeiJigyousyoInfo
        Dim dtJigyousyoTable As New Data.DataTable
        dtJigyousyoTable = SeikyuuSakiMasterBL.SelSinkaikeiJigyousyoInfo(tbxSkkJigyousyoCd.Text)

        '検索結果が1件だった場合
        If dtJigyousyoTable.Rows.Count = 1 Then
            '新会計事業所コード
            tbxSkkJigyousyoCd.Text = TrimNull(dtJigyousyoTable.Rows(0).Item("skk_jigyousyo_cd"))
            '新会計支払先名 
            tbxSkkJigyousyoMei.Text = TrimNull(dtJigyousyoTable.Rows(0).Item("skk_jigyousyo_mei"))
        Else
            tbxSkkJigyousyoMei.Text = ""
            strScript = "objSrchWin = window.open('search_SinkaikeiJigyousyo.aspx?Kbn='+escape('新会計事業所')+'&SiharaiKbn='+escape('Siharai')+'&FormName=" & _
            Me.Page.Form.Name & "&objCd=" & _
                tbxSkkJigyousyoCd.ClientID & _
                    "&objMei=" & tbxSkkJigyousyoMei.ClientID & _
                    "&strCd='+escape(eval('document.all.'+'" & _
                    tbxSkkJigyousyoCd.ClientID & "').value)+'&strMei='+escape(eval('document.all.'+'" & _
                    tbxSkkJigyousyoMei.ClientID & "').value), 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strScript, True)
        End If
    End Sub

    ''' <summary>
    ''' 名寄先検索ボタンボタン押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <history>20100925　名寄先コード、名寄先名　追加　馬艶軍</history>
    Protected Sub btnKensakuNayose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKensakuNayose.Click
        Dim strScript As String = ""
        'データ取得
        Dim dtNayoseSakiTable As New Data.DataTable
        dtNayoseSakiTable = SeikyuuSakiMasterBL.SelNayoseSakiInfo(tbxNayoseCd.Text.Trim)

        '検索結果が1件だった場合
        If dtNayoseSakiTable.Rows.Count = 1 Then
            '名寄先コード
            tbxNayoseCd.Text = TrimNull(dtNayoseSakiTable.Rows(0).Item("nayose_saki_cd"))
            '名寄先名 
            tbxNayoseMei.Text = TrimNull(dtNayoseSakiTable.Rows(0).Item("nayose_saki_name1"))
        Else
            tbxNayoseMei.Text = ""
            strScript = "objSrchWin = window.open('search_NayoseSaki.aspx?Kbn='+escape('名寄先')+'&SiharaiKbn='+escape('Siharai')+'&FormName=" & _
            Me.Page.Form.Name & "&objCd=" & _
                tbxNayoseCd.ClientID & _
                    "&objMei=" & tbxNayoseMei.ClientID & _
                    "&strCd='+escape(eval('document.all.'+'" & _
                    tbxNayoseCd.ClientID & "').value)+'&strMei='+escape(eval('document.all.'+'" & _
                    tbxNayoseMei.ClientID & "').value), 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strScript, True)
        End If
    End Sub

    ''' <summary>
    ''' 明細クリア
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnClearMeisai_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearMeisai.Click
        MeisaiClear()
    End Sub

    ''' <summary>
    ''' クリア
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        '請求先区分
        SetDropSelect(ddlSeikyuuSaki_Kbn, "")
        '請求先コード
        tbxSeikyuuSaki_Cd.Text = ""
        '請求先
        tbxSeikyuuSaki_Brc.Text = ""
        '請求先名
        tbxSeikyuuSaki_Mei.Text = ""
        'MeisaiClear()
    End Sub

    ''' <summary>Javascript作成</summary>
    Private Sub MakeScript()
        Dim csType As Type = Page.GetType()
        Dim csName As String = "GetScript"
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("<script language='javascript' type='text/javascript'>")
            '基本情報セット
            .AppendLine("function fncConfirm()")
            .AppendLine("{")
            '
            .AppendLine("   var hidConfirm = document.getElementById('" & Me.hidConfirm.ClientID & "')")
            .AppendLine("   var hidSeikyuuKbn = document.getElementById('" & Me.hidSeikyuuKbn.ClientID & "')")
            .AppendLine("   var ddlSeikyuuSakiKbn = document.getElementById('" & Me.ddlSeikyuuSakiKbn.ClientID & "')")
            .AppendLine("   if(confirm('基本情報をセットしますか？')){")
            .AppendLine("       hidConfirm.value = 'OK';")
            .AppendLine("       hidSeikyuuKbn.value = ddlSeikyuuSakiKbn.value;")
            .AppendLine("       document.getElementById('" & Me.Form.Name & "').submit();")
            .AppendLine("   }")
            .AppendLine("}")

            '郵便の取得
            .AppendLine("function fncOpenwindowYuubin(id1,mei1,mei2)" & vbCrLf)
            .AppendLine("{" & vbCrLf)
            .AppendLine("var strkbn='郵便';" & vbCrLf)
            .AppendLine("objSrchWin = window.open('search_common.aspx?Kbn='+escape(strkbn)+'&FormName=" & _
            Me.Page.Form.Name & _
            "&objCd=" & _
            "'+escape(id1)+'" & _
            "&objMei=" & _
            "'+mei1+'" & _
            "&objMei2=" & _
            "'+mei2+'" & _
            "&strCd='+escape(eval('document.all.'+" & _
            " id1 +'" & "').value)" & _
            "+'&strMei='+escape(eval('document.all.'+" & _
            " mei1 " & ").innerText)" & _
            "+'&strMei2='+escape(eval('document.all.'+" & _
            " mei2 " & ").innerText)" & _
            ", 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');" & vbCrLf)
            .AppendLine("}" & vbCrLf)

            '郵便番号
            .AppendLine("function SetYuubinNo(e)")
            .AppendLine("{")
            .AppendLine("   var val;")
            .AppendLine("   var val2;")
            .AppendLine("   val = e.value;")
            .AppendLine("   arr = val.split('-');")
            .AppendLine("   val = arr.join('');")
            .AppendLine("   if (val.length>=3){")
            .AppendLine("       val2 = val.substring(0,3) + '-' + val.substring(3,val.length);")
            .AppendLine("   }else{")
            .AppendLine("       val2 =val;")
            .AppendLine("   }")
            .AppendLine("   e.value = val2.replace(/(^\s*)|(\s*$)/g,'');")
            .AppendLine("}")

            '基本情報セット
            .AppendLine("function fncDisable()")
            .AppendLine("{")
            .AppendLine("   var btnSearchSeikyuuSaki = document.getElementById('" & Me.btnSearchSeikyuuSaki.ClientID & "')")
            .AppendLine("   var btnSearch = document.getElementById('" & Me.btnSearch.ClientID & "')")
            .AppendLine("   var btnClear = document.getElementById('" & Me.btnClear.ClientID & "')")
            .AppendLine("   var btnSyuusei = document.getElementById('" & Me.btnSyuusei.ClientID & "')")
            .AppendLine("   var btnTouroku = document.getElementById('" & Me.btnTouroku.ClientID & "')")
            .AppendLine("   var btnClearMeisai = document.getElementById('" & Me.btnClearMeisai.ClientID & "')")
            .AppendLine("   var btnKensakuSeikyuuSaki = document.getElementById('" & Me.btnKensakuSeikyuuSaki.ClientID & "')")
            .AppendLine("   var btnSkkJigyousyo = document.getElementById('" & Me.btnSkkJigyousyo.ClientID & "')")
            .AppendLine("   var btnKihonJyouhouSet = document.getElementById('" & Me.btnKihonJyouhouSet.ClientID & "')")
            .AppendLine("   var btnKensakuYuubinNo = document.getElementById('" & Me.btnKensakuYuubinNo.ClientID & "')")
            '20100925　名寄先コード、名寄先名　追加　馬艶軍↓↓↓
            .AppendLine("   var btnKensakuNayose = document.getElementById('" & Me.btnKensakuNayose.ClientID & "')")
            '20100925　名寄先コード、名寄先名　追加　馬艶軍↑↑↑
            .AppendLine("   var my_array = new Array(10);")
            .AppendLine("   my_array[0] = btnSearchSeikyuuSaki;")
            .AppendLine("   my_array[1] = btnSearch;")
            .AppendLine("   my_array[2] = btnClear;")
            .AppendLine("   my_array[3] = btnSyuusei;")
            .AppendLine("   my_array[4] = btnTouroku;")
            .AppendLine("   my_array[5] = btnClearMeisai;")
            .AppendLine("   my_array[6] = btnKensakuSeikyuuSaki;")
            .AppendLine("   my_array[7] = btnSkkJigyousyo;")
            .AppendLine("   my_array[8] = btnKihonJyouhouSet;")
            .AppendLine("   my_array[9] = btnKensakuYuubinNo;")
            '20100925　名寄先コード、名寄先名　追加　馬艶軍↓↓↓
            .AppendLine("   my_array[10] = btnKensakuNayose;")
            '20100925　名寄先コード、名寄先名　追加　馬艶軍↑↑↑
            .AppendLine("   for (i = 0; i < 11; i++){")
            .AppendLine("       my_array[i].disabled = true;")
            .AppendLine("   }")
            .AppendLine("}")
            .AppendLine("function disableButton1()")
            .AppendLine("{")
            .AppendLine("   window.setTimeout('fncDisable()',0);")
            .AppendLine("   return true;")
            .AppendLine("}")

            '==================2011/06/16 車龍 ｢注意事項｣ボタン追加の対応 追加 開始↓========================== 
            '｢注意情報｣ボタン
            .AppendLine("function fncTyuuijikouPopup() ")
            .AppendLine("{ ")
            .AppendLine("	var blnTouroku = document.getElementById('" & Me.btnTouroku.ClientID & "').disabled; ")
            .AppendLine("	var blnSyuusei = document.getElementById('" & Me.btnSyuusei.ClientID & "').disabled; ")
            .AppendLine("	//修正モード時（【修正実行】活性時） ")
            .AppendLine("	if(blnSyuusei == false) ")
            .AppendLine("	{ ")
            .AppendLine("		var strKbn = document.getElementById('" & Me.ddlSeikyuuSakiKbn.ClientID & "').value; ")
            .AppendLine("		var strCd = document.getElementById('" & Me.tbxSeikyuuSakiCd.ClientID & "').value; ")
            .AppendLine("		var strBrc = document.getElementById('" & Me.tbxSeikyuuSakiBrc.ClientID & "').value; ")
            .AppendLine("		var sendSearchTerms = strKbn + '$$$' + strCd + '$$$' + strBrc; ")
            .AppendLine("		window.open('SeikyuuSakiTyuuijikouSearchList.aspx?sendSearchTerms='+sendSearchTerms,'TyuuijikouPopup','menubar=yes,toolbar=yes,location=yes,status=yes,resizable=yes,scrollbars=yes'); ")
            .AppendLine("		return true; ")
            .AppendLine("	} ")
            .AppendLine("	//新規モード時（【新規登録】活性時） ")
            .AppendLine("	if(blnTouroku == false) ")
            .AppendLine("	{ ")
            .AppendLine("		window.open('SeikyuuSakiTyuuijikouSearchList.aspx','TyuuijikouPopup','menubar=yes,toolbar=yes,location=yes,status=yes,resizable=yes,scrollbars=yes'); ")
            .AppendLine("		return true; ")
            .AppendLine("	} ")
            .AppendLine("	//修正と新規 非活性 ")
            .AppendLine("	if((blnSyuusei == true)&&(blnTouroku == true)) ")
            .AppendLine("	{ ")
            .AppendLine("		window.open('SeikyuuSakiTyuuijikouSearchList.aspx','TyuuijikouPopup','menubar=yes,toolbar=yes,location=yes,status=yes,resizable=yes,scrollbars=yes'); ")
            .AppendLine("		return true; ")
            .AppendLine("	} ")
            .AppendLine("   return false; ")
            .AppendLine("} ")
            '==================2011/06/16 車龍 ｢注意事項｣ボタン追加の対応 追加 終了↑========================== 

            '===========2012/05/15 車龍 407553の対応 追加↓=========================
            .AppendLine("function fncSetBtnSaiban() ")
            .AppendLine("{ ")
            .AppendLine("	var strTougouKaikeiTokusakiCd = document.getElementById('" & Me.tbxTougouKaikeiTokusakiCd.ClientID & "').value.Trim(); ")
            .AppendLine("	var btnSaiban = document.getElementById('" & Me.btnSaiban.ClientID & "'); ")
            .AppendLine("	if(strTougouKaikeiTokusakiCd == '') ")
            .AppendLine("	{ ")
            .AppendLine("		btnSaiban.disabled = false; ")
            .AppendLine("	} ")
            .AppendLine("	else ")
            .AppendLine("	{ ")
            .AppendLine("		btnSaiban.disabled = true; ")
            .AppendLine("	} ")
            .AppendLine("} ")
            '===========2012/05/15 車龍 407553の対応 追加↑=========================

            .AppendLine("</script>")
        End With
        ClientScript.RegisterClientScriptBlock(csType, csName, csScript.ToString)
    End Sub

    ''' <summary>
    ''' 登録と修正値を持ち
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SetMeisaiData() As Itis.Earth.DataAccess.SeikyuuSakiDataSet.m_seikyuu_sakiDataTable
        Dim dtSeikyuuSakiDataSet As New Itis.Earth.DataAccess.SeikyuuSakiDataSet.m_seikyuu_sakiDataTable

        dtSeikyuuSakiDataSet.Rows.Add(dtSeikyuuSakiDataSet.NewRow)
        '取消
        dtSeikyuuSakiDataSet.Item(0).torikesi = IIf(chkTorikesi.Checked, "1", "0")
        '請求先コード
        dtSeikyuuSakiDataSet.Item(0).seikyuu_saki_cd = tbxSeikyuuSakiCd.Text.ToUpper
        '請求先枝番
        dtSeikyuuSakiDataSet.Item(0).seikyuu_saki_brc = tbxSeikyuuSakiBrc.Text.ToUpper
        '請求先区分
        dtSeikyuuSakiDataSet.Item(0).seikyuu_saki_kbn = ddlSeikyuuSakiKbn.SelectedValue

        '20100925　名寄先コード、名寄先名　追加　馬艶軍↓↓↓
        '名寄先コード
        dtSeikyuuSakiDataSet.Item(0).nayose_saki_cd = tbxNayoseCd.Text.ToUpper
        '20100925　名寄先コード、名寄先名　追加　馬艶軍↑↑↑

        '20100712　仕様変更　追加　馬艶軍↓↓↓
        '郵便番号
        dtSeikyuuSakiDataSet.Item(0).skysy_soufu_yuubin_no = tbxYuubinNo.Text
        '住所１
        dtSeikyuuSakiDataSet.Item(0).skysy_soufu_jyuusyo1 = tbxJyuusyo1.Text
        '電話番号
        dtSeikyuuSakiDataSet.Item(0).skysy_soufu_tel_no = tbxTelNo.Text
        '住所２
        dtSeikyuuSakiDataSet.Item(0).skysy_soufu_jyuusyo2 = tbxJyuusyo2.Text
        'FAX番号
        dtSeikyuuSakiDataSet.Item(0).skysy_soufu_fax_no = tbxFaxNo.Text
        '20100712　仕様変更　追加　馬艶軍↑↑↑

        '新会計事業所コード
        dtSeikyuuSakiDataSet.Item(0).skk_jigyousyo_cd = tbxSkkJigyousyoCd.Text

        '旧請求先コード
        '==================2011/06/16 車龍 修正 開始↓==========================
        'dtSeikyuuSakiDataSet.Item(0).kyuu_seikyuu_saki_cd = tbxKyuuSeikyuuSakiCd.Text
        dtSeikyuuSakiDataSet.Item(0).kyuu_seikyuu_saki_cd = String.Empty
        '==================2011/06/16 車龍 修正 終了↑==========================

        '==================2011/06/16 車龍  追加 開始↓========================== 
        '決算時二度締めフラグ
        dtSeikyuuSakiDataSet.Item(0).kessanji_nidosime_flg = Me.ddlKessanjiNidosimeFlg.SelectedValue
        '==================2011/06/16 車龍  追加 終了↑==========================

        '担当者名
        dtSeikyuuSakiDataSet.Item(0).tantousya_mei = tbxTantousya.Text
        '請求書印字物件名フラグ
        dtSeikyuuSakiDataSet.Item(0).seikyuusyo_inji_bukken_mei_flg = ddlSeikyuuSyoInjiBukkenMei.SelectedValue
        '入金口座番号
        dtSeikyuuSakiDataSet.Item(0).nyuukin_kouza_no = tbxNyuukinKouzaNo.Text
        '請求締め日
        dtSeikyuuSakiDataSet.Item(0).seikyuu_sime_date = tbxSeikyuuSimeDate.Text
        '先方請求締め日
        dtSeikyuuSakiDataSet.Item(0).senpou_seikyuu_sime_date = tbxSenpouSeikyuuSimeDate.Text
        '直工事請求タイミングフラグ
        dtSeikyuuSakiDataSet.Item(0).tyk_koj_seikyuu_timing_flg = ddlTykKojSeikyuuTimingFlg.SelectedValue
        '相殺フラグ
        dtSeikyuuSakiDataSet.Item(0).sousai_flg = IIf(chkSousaiFlg.Checked, "1", "0")
        '回収予定月数
        dtSeikyuuSakiDataSet.Item(0).kaisyuu_yotei_gessuu = tbxKaisyuuYoteiGessuu.Text
        '回収予定日
        dtSeikyuuSakiDataSet.Item(0).kaisyuu_yotei_date = tbxKaisyuuYoteiDate.Text
        '請求書必着日
        dtSeikyuuSakiDataSet.Item(0).seikyuusyo_hittyk_date = tbxSeikyuusyoHittykDate.Text
        '回収1種別1
        dtSeikyuuSakiDataSet.Item(0).kaisyuu1_syubetu1 = ddlKaisyuu1Syubetu1.SelectedValue
        '回収1割合1
        dtSeikyuuSakiDataSet.Item(0).kaisyuu1_wariai1 = tbxKaisyuu1Wariai1.Text
        '回収1手形サイト月数
        dtSeikyuuSakiDataSet.Item(0).kaisyuu1_tegata_site_gessuu = tbxKaisyuu1TegataSiteGessuu.Text
        '回収1手形サイト日
        dtSeikyuuSakiDataSet.Item(0).kaisyuu1_tegata_site_date = tbxKaisyuu1TegataSiteDate.Text
        '回収1請求書用紙
        dtSeikyuuSakiDataSet.Item(0).kaisyuu1_seikyuusyo_yousi = ddlKaisyuu1SeikyuusyoYousi.SelectedValue
        '回収1種別2
        dtSeikyuuSakiDataSet.Item(0).kaisyuu1_syubetu2 = ddlKaisyuu1Syubetu2.SelectedValue
        '回収1割合2
        dtSeikyuuSakiDataSet.Item(0).kaisyuu1_wariai2 = tbxKaisyuu1Wariai2.Text
        '回収1種別3
        dtSeikyuuSakiDataSet.Item(0).kaisyuu1_syubetu3 = ddlKaisyuu1Syubetu3.SelectedValue
        '回収1割合3
        dtSeikyuuSakiDataSet.Item(0).kaisyuu1_wariai3 = tbxKaisyuu1Wariai3.Text
        '回収境界額
        dtSeikyuuSakiDataSet.Item(0).kaisyuu_kyoukaigaku = Replace(tbxKaisyuuKyoukaigaku.Text, ",", "")
        '回収2種別1
        dtSeikyuuSakiDataSet.Item(0).kaisyuu2_syubetu1 = ddlKaisyuu2Syubetu1.SelectedValue
        '回収2割合1
        dtSeikyuuSakiDataSet.Item(0).kaisyuu2_wariai1 = tbxKaisyuu2Wariai1.Text
        '回収2手形サイト月数
        dtSeikyuuSakiDataSet.Item(0).kaisyuu2_tegata_site_gessuu = tbxKaisyuu2TegataSiteGessuu.Text
        '回収2手形サイト日
        dtSeikyuuSakiDataSet.Item(0).kaisyuu2_tegata_site_date = tbxKaisyuu2TegataSiteDate.Text
        '回収2請求書用紙
        dtSeikyuuSakiDataSet.Item(0).kaisyuu2_seikyuusyo_yousi = ddlKaisyuu2SeikyuusyoYousi.SelectedValue
        '回収2種別2
        dtSeikyuuSakiDataSet.Item(0).kaisyuu2_syubetu2 = ddlKaisyuu2Syubetu2.SelectedValue
        '回収2割合2
        dtSeikyuuSakiDataSet.Item(0).kaisyuu2_wariai2 = tbxKaisyuu2Wariai2.Text
        '回収2種別3
        dtSeikyuuSakiDataSet.Item(0).kaisyuu2_syubetu3 = ddlKaisyuu2Syubetu3.SelectedValue
        '回収2割合3
        dtSeikyuuSakiDataSet.Item(0).kaisyuu2_wariai3 = tbxKaisyuu2Wariai3.Text

        '2013/5/29 請求書の銀行支店コード増加に伴うEarth改修 -----------------------↓
        '銀行支店コード
        dtSeikyuuSakiDataSet.Item(0).ginkou_siten_cd = Me.ddlGinkouSitenCd.SelectedValue
        '2013/5/29 請求書の銀行支店コード増加に伴うEarth改修 -----------------------↑

        '===========2012/05/14 車龍 407553の対応 追加↓=====================
        With dtSeikyuuSakiDataSet.Item(0)
            '統合会計得意先ｺｰﾄﾞ 
            .tougou_tokuisaki_cd = Me.tbxTougouKaikeiTokusakiCd.Text.Trim
            '口振ＯＫフラグ
            .koufuri_ok_flg = Me.ddlKutiburiOkFlg.SelectedValue.Trim
            '安全協力会費_円
            .anzen_kaihi_en = Me.tbxAnzenKyouryokuKaihi1.Text.Trim.Replace(",", String.Empty)
            '安全協力会費_％
            .anzen_kaihi_wari = Me.tbxAnzenKyouryokuKaihi2.Text.Trim
            '備考
            .bikou = Me.tbxBikou.Text.Trim
        End With
        dtSeikyuuSakiDataSet.Item(0).kyouryoku_kaihi_taisyou = Me.ddlKyouryokuKaihiJigou.SelectedValue

        '===========2012/05/14 車龍 407553の対応 追加↑=====================

        dtSeikyuuSakiDataSet.Item(0).upd_login_user_id = ViewState("UserId")
        dtSeikyuuSakiDataSet.Item(0).add_login_user_id = ViewState("UserId")
        dtSeikyuuSakiDataSet.Item(0).upd_datetime = hidUPDTime.Value

        Return dtSeikyuuSakiDataSet
    End Function

    ''' <summary>
    ''' 明細項目クリア
    ''' </summary>
    ''' <remarks></remarks>
    Sub MeisaiClear()
        '取消
        chkTorikesi.Checked = False
        '請求先区分
        SetDropSelect(ddlSeikyuuSakiKbn, "")
        '請求先コード
        tbxSeikyuuSakiCd.Text = ""
        '請求先枝番
        tbxSeikyuuSakiBrc.Text = ""
        '請求先名
        tbxSeikyuuSakiMei.Text = ""

        '20100925　名寄先コード、名寄先名　追加　馬艶軍↓↓↓
        '名寄先コード
        tbxNayoseCd.Text = ""
        '名寄先名
        tbxNayoseMei.Text = ""
        '20100925　名寄先コード、名寄先名　追加　馬艶軍↑↑↑

        '20100712　仕様変更　追加　馬艶軍↓↓↓
        '郵便番号
        tbxYuubinNo.Text = ""
        '住所１
        tbxJyuusyo1.Text = ""
        '電話番号
        tbxTelNo.Text = ""
        '住所２
        tbxJyuusyo2.Text = ""
        'FAX番号
        tbxFaxNo.Text = ""
        '20100712　仕様変更　追加　馬艶軍↑↑↑

        '請求先登録雛形
        SetDropSelect(ddlSeikyuuSakiTourokuHinagata, "")
        '担当者
        tbxTantousya.Text = ""
        '請求書印字物件名
        SetDropSelect(ddlSeikyuuSyoInjiBukkenMei, "")
        '新会計事業所
        tbxSkkJigyousyoCd.Text = ""
        '新会計事業所
        tbxSkkJigyousyoMei.Text = ""
        '請求締め日
        tbxSeikyuuSimeDate.Text = ""
        '先方請求締め日
        tbxSenpouSeikyuuSimeDate.Text = ""
        ' 請求書必着日
        tbxSeikyuusyoHittykDate.Text = ""

        '==================2011/06/16 車龍 削除 開始↓==========================
        ''旧請求先コード
        'tbxKyuuSeikyuuSakiCd.Text = ""
        '==================2011/06/16 車龍 削除 終了↑==========================

        '==================2011/06/16 車龍  追加 開始↓========================== 
        '決算時二度締めフラグ
        Me.ddlKessanjiNidosimeFlg.SelectedValue = "0"
        '==================2011/06/16 車龍  追加 終了↑==========================

        '回収予定月数
        tbxKaisyuuYoteiGessuu.Text = ""
        '回収予定日
        tbxKaisyuuYoteiDate.Text = ""
        '入金口座番号
        tbxNyuukinKouzaNo.Text = ""
        '相殺フラグ
        chkSousaiFlg.Checked = False
        '直工事請求タイミング
        SetDropSelect(ddlTykKojSeikyuuTimingFlg, "")
        '回収1手形サイト月数
        tbxKaisyuu1TegataSiteGessuu.Text = ""
        '回収1手形サイト日
        tbxKaisyuu1TegataSiteDate.Text = ""
        '回収1請求書用紙
        SetDropSelect(ddlKaisyuu1SeikyuusyoYousi, "")
        '回収1種別1
        SetDropSelect(ddlKaisyuu1Syubetu1, "")
        '回収1割合1
        tbxKaisyuu1Wariai1.Text = ""
        '回収1種別2
        SetDropSelect(ddlKaisyuu1Syubetu2, "")
        '回収1割合2
        tbxKaisyuu1Wariai2.Text = ""
        '回収1種別3
        SetDropSelect(ddlKaisyuu1Syubetu3, "")
        '回収1割合3
        tbxKaisyuu1Wariai3.Text = ""
        '回収境界額
        tbxKaisyuuKyoukaigaku.Text = ""
        '回収2手形サイト月数
        tbxKaisyuu2TegataSiteGessuu.Text = ""
        '回収2手形サイト日
        tbxKaisyuu2TegataSiteDate.Text = ""
        '回収2請求書用紙
        SetDropSelect(ddlKaisyuu2SeikyuusyoYousi, "")
        '回収2種別1
        SetDropSelect(ddlKaisyuu2Syubetu1, "")
        '回収2割合1
        tbxKaisyuu2Wariai1.Text = ""
        '回収2種別2
        SetDropSelect(ddlKaisyuu2Syubetu2, "")
        '回収2割合2
        tbxKaisyuu2Wariai2.Text = ""
        '回収2種別3
        SetDropSelect(ddlKaisyuu2Syubetu3, "")
        '回収2割合3
        tbxKaisyuu2Wariai3.Text = ""

        '2013/5/29 請求書の銀行支店コード増加に伴うEarth改修 -----------------------↓
        '銀行支店コード
        SetDropSelect(Me.ddlGinkouSitenCd, "804")
        '2013/5/29 請求書の銀行支店コード増加に伴うEarth改修 -----------------------↑

        '===========2012/05/14 車龍 407553の対応 追加↓=====================
        '統合会計得意先ｺｰﾄﾞ 
        Me.tbxTougouKaikeiTokusakiCd.Text = String.Empty
        '口振ＯＫフラグ
        SetDropSelect(ddlKutiburiOkFlg, String.Empty)
        '協力会費_適用事項
        Me.ddlKyouryokuKaihiJigou.SelectedIndex = 0
        '安全協力会費_円
        Me.tbxAnzenKyouryokuKaihi1.Text = String.Empty
        '安全協力会費_％
        Me.tbxAnzenKyouryokuKaihi2.Text = String.Empty
        '備考
        Me.tbxBikou.Text = String.Empty

        '「採番」ボタンをセットする
        Call Me.SetBtnSaiban()
        '===========2012/05/14 車龍 407553の対応 追加↑=====================

        hidUPDTime.Value = ""
        If Not blnBtn Then
            btnSyuusei.Enabled = False
            btnTouroku.Enabled = False
        Else
            btnSyuusei.Enabled = False
            btnTouroku.Enabled = True
        End If
        Me.btnKensakuSeikyuuSaki.Enabled = True
        ddlSeikyuuSakiKbn.Attributes.Remove("disabled")
        tbxSeikyuuSakiCd.Attributes.Remove("readonly")
        tbxSeikyuuSakiBrc.Attributes.Remove("readonly")
        UpdatePanelA.Update()
    End Sub

    ''' <summary>
    ''' 明細データを取得
    ''' </summary>
    ''' <param name="TyousaKaisya_Cd"></param>
    ''' <param name="btn"></param>
    ''' <remarks></remarks>
    Sub GetMeisaiData(ByVal TyousaKaisya_Cd As String, _
                      ByVal TyousaKaisyaCd As String, _
                      ByVal JigyousyoCd As String, _
                      Optional ByVal btn As String = "")

        Dim strErr As String = ""
        Dim dtSeikyuuSakiDataSet As New Itis.Earth.DataAccess.SeikyuuSakiDataSet.m_seikyuu_sakiDataTable
        dtSeikyuuSakiDataSet = SeikyuuSakiMasterBL.SelSeikyuuSakiInfo(TyousaKaisya_Cd, TyousaKaisyaCd, JigyousyoCd)

        If dtSeikyuuSakiDataSet.Rows.Count = 1 Then
            With dtSeikyuuSakiDataSet.Item(0)
                '取消
                chkTorikesi.Checked = IIf(.torikesi = "0", False, True)
                '請求先区分
                SetDropSelect(ddlSeikyuuSakiKbn, TrimNull(.seikyuu_saki_kbn))
                '請求先コード
                tbxSeikyuuSakiCd.Text = TrimNull(.seikyuu_saki_cd)
                '請求先枝番
                tbxSeikyuuSakiBrc.Text = TrimNull(.seikyuu_saki_brc)
                '請求先名
                tbxSeikyuuSakiMei.Text = TrimNull(.seikyuu_saki_mei)
                If btn = "btnSearch" Then
                    '請求先名
                    tbxSeikyuuSaki_Mei.Text = TrimNull(.seikyuu_saki_mei)

                    tbxSeikyuuSaki_Cd.Text = TrimNull(.seikyuu_saki_cd).ToUpper
                    tbxSeikyuuSaki_Brc.Text = TrimNull(.seikyuu_saki_brc).ToUpper
                End If

                '20100925　名寄先コード、名寄先名　追加　馬艶軍↓↓↓
                '名寄先コード
                tbxNayoseCd.Text = TrimNull(.nayose_saki_cd)
                '名寄先名
                'tbxNayoseMei.Text = TrimNull(.nayose_saki_name1)
                If tbxNayoseCd.Text <> String.Empty Then
                    Dim dtNayoseSakiTable As New Data.DataTable
                    dtNayoseSakiTable = SeikyuuSakiMasterBL.SelNayoseSakiInfo(tbxNayoseCd.Text.Trim)
                    If dtNayoseSakiTable.Rows.Count > 0 Then
                        tbxNayoseMei.Text = TrimNull(dtNayoseSakiTable.Rows(0).Item("nayose_saki_name1"))
                    Else
                        tbxNayoseMei.Text = "名寄先コード　未登録"
                    End If
                Else
                    tbxNayoseMei.Text = ""
                End If
                '20100925　名寄先コード、名寄先名　追加　馬艶軍↑↑↑

                '20100712　仕様変更　追加　馬艶軍↓↓↓
                '郵便番号
                tbxYuubinNo.Text = TrimNull(.skysy_soufu_yuubin_no)
                '住所１
                tbxJyuusyo1.Text = TrimNull(.skysy_soufu_jyuusyo1)
                '電話番号
                tbxTelNo.Text = TrimNull(.skysy_soufu_tel_no)
                '住所２
                tbxJyuusyo2.Text = TrimNull(.skysy_soufu_jyuusyo2)
                'FAX番号
                tbxFaxNo.Text = TrimNull(.skysy_soufu_fax_no)
                '20100712　仕様変更　追加　馬艶軍↑↑↑

                '担当者
                tbxTantousya.Text = TrimNull(.tantousya_mei)
                '請求書印字物件名
                SetDropSelect(ddlSeikyuuSyoInjiBukkenMei, TrimNull(.seikyuusyo_inji_bukken_mei_flg))
                '新会計事業所
                tbxSkkJigyousyoCd.Text = TrimNull(.skk_jigyousyo_cd)
                '新会計事業所
                tbxSkkJigyousyoMei.Text = TrimNull(.skk_jigyousyo_mei)
                '請求締め日
                tbxSeikyuuSimeDate.Text = TrimNull(.seikyuu_sime_date)
                '先方請求締め日
                tbxSenpouSeikyuuSimeDate.Text = TrimNull(.senpou_seikyuu_sime_date)
                ' 請求書必着日
                tbxSeikyuusyoHittykDate.Text = TrimNull(.seikyuusyo_hittyk_date)

                '==================2011/06/16 車龍 削除 開始↓==========================
                ''旧請求先コード
                'tbxKyuuSeikyuuSakiCd.Text = TrimNull(.kyuu_seikyuu_saki_cd)
                '==================2011/06/16 車龍 削除 終了↑==========================

                '==================2011/06/16 車龍  追加 開始↓========================== 
                '決算時二度締めフラグ
                Me.ddlKessanjiNidosimeFlg.SelectedValue = IIf(TrimNull(.kessanji_nidosime_flg).Equals("1"), "1", "0")
                '==================2011/06/16 車龍  追加 終了↑==========================

                '回収予定月数
                tbxKaisyuuYoteiGessuu.Text = TrimNull(.kaisyuu_yotei_gessuu)
                '回収予定日
                tbxKaisyuuYoteiDate.Text = TrimNull(.kaisyuu_yotei_date)
                '入金口座番号
                tbxNyuukinKouzaNo.Text = TrimNull(.nyuukin_kouza_no)
                '相殺フラグ
                chkSousaiFlg.Checked = IIf(.sousai_flg = "0", False, True)
                '直工事請求タイミング
                SetDropSelect(ddlTykKojSeikyuuTimingFlg, TrimNull(.tyk_koj_seikyuu_timing_flg))
                '回収1手形サイト月数
                tbxKaisyuu1TegataSiteGessuu.Text = TrimNull(.kaisyuu1_tegata_site_gessuu)
                '回収1手形サイト日
                tbxKaisyuu1TegataSiteDate.Text = TrimNull(.kaisyuu1_tegata_site_date)
                '回収1請求書用紙
                SetDropSelect(ddlKaisyuu1SeikyuusyoYousi, TrimNull(.kaisyuu1_seikyuusyo_yousi))
                '回収1種別1
                SetDropSelect(ddlKaisyuu1Syubetu1, TrimNull(.kaisyuu1_syubetu1))
                '回収1割合1
                tbxKaisyuu1Wariai1.Text = TrimNull(.kaisyuu1_wariai1)
                '回収1種別2
                SetDropSelect(ddlKaisyuu1Syubetu2, TrimNull(.kaisyuu1_syubetu2))
                '回収1割合2
                tbxKaisyuu1Wariai2.Text = TrimNull(.kaisyuu1_wariai2)
                '回収1種別3
                SetDropSelect(ddlKaisyuu1Syubetu3, TrimNull(.kaisyuu1_syubetu3))
                '回収1割合1
                tbxKaisyuu1Wariai3.Text = TrimNull(.kaisyuu1_wariai3)
                '回収境界額
                tbxKaisyuuKyoukaigaku.Text = AddComa(.kaisyuu_kyoukaigaku)

                '回収2手形サイト月数
                tbxKaisyuu2TegataSiteGessuu.Text = TrimNull(.kaisyuu2_tegata_site_gessuu)
                '回収2手形サイト日
                tbxKaisyuu2TegataSiteDate.Text = TrimNull(.kaisyuu2_tegata_site_date)
                '回収2請求書用紙
                SetDropSelect(ddlKaisyuu2SeikyuusyoYousi, TrimNull(.kaisyuu2_seikyuusyo_yousi))
                '回収2種別1
                SetDropSelect(ddlKaisyuu2Syubetu1, TrimNull(.kaisyuu2_syubetu1))
                '回収2割合1
                tbxKaisyuu2Wariai1.Text = TrimNull(.kaisyuu2_wariai1)
                '回収2種別2
                SetDropSelect(ddlKaisyuu2Syubetu2, TrimNull(.kaisyuu2_syubetu2))
                '回収2割合2
                tbxKaisyuu2Wariai2.Text = TrimNull(.kaisyuu2_wariai2)
                '回収2種別3
                SetDropSelect(ddlKaisyuu2Syubetu3, TrimNull(.kaisyuu2_syubetu3))
                '回収2割合1
                tbxKaisyuu2Wariai3.Text = TrimNull(.kaisyuu2_wariai3)

                '2013/5/29 請求書の銀行支店コード増加に伴うEarth改修 -----------------------↓
                '銀行支店コード
                SetDropSelect(ddlGinkouSitenCd, TrimNull(.ginkou_siten_cd))
                '2013/5/29 請求書の銀行支店コード増加に伴うEarth改修 -----------------------↑

                '===========2012/05/14 車龍 407553の対応 追加↓=====================
                '統合会計得意先ｺｰﾄﾞ 
                Me.tbxTougouKaikeiTokusakiCd.Text = TrimNull(.tougou_tokuisaki_cd)
                '口振ＯＫフラグ
                SetDropSelect(ddlKutiburiOkFlg, TrimNull(.koufuri_ok_flg))
                '協力会費_適用事項
                Me.ddlKyouryokuKaihiJigou.SelectedValue = TrimNull(dtSeikyuuSakiDataSet.Item(0).kyouryoku_kaihi_taisyou)
                '安全協力会費_円
                If TrimNull(.anzen_kaihi_en).Equals(String.Empty) Then
                    Me.tbxAnzenKyouryokuKaihi1.Text = String.Empty
                Else
                    Me.tbxAnzenKyouryokuKaihi1.Text = FormatNumber(TrimNull(.anzen_kaihi_en), 0)
                End If
                '安全協力会費_％
                If TrimNull(.anzen_kaihi_wari).Equals(String.Empty) Then
                    Me.tbxAnzenKyouryokuKaihi2.Text = String.Empty
                Else
                    Me.tbxAnzenKyouryokuKaihi2.Text = Convert.ToDecimal(.anzen_kaihi_wari).ToString("0.#####")
                End If
                '備考
                Me.tbxBikou.Text = TrimNull(.bikou)

                '「採番」ボタンをセットする
                Call Me.SetBtnSaiban()
                '===========2012/05/14 車龍 407553の対応 追加↑=====================

                hidUPDTime.Value = TrimNull(.upd_datetime)
                UpdatePanelA.Update()
            End With

            Me.hidConfirm.Value = ""

            If Not blnBtn Then
                btnSyuusei.Enabled = False
                btnTouroku.Enabled = False
            Else
                btnSyuusei.Enabled = True
                btnTouroku.Enabled = False
            End If

            tbxSeikyuuSakiCd.Attributes.Add("readonly", "true;")
            tbxSeikyuuSakiBrc.Attributes.Add("readonly", "true;")
            ddlSeikyuuSakiKbn.Attributes.Add("disabled", "true;")

            Me.btnKensakuSeikyuuSaki.Enabled = False
        Else
            MeisaiClear()
            Me.tbxSeikyuuSaki_Mei.Text = ""
            strErr = "alert('" & Messages.Instance.MSG2034E & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strErr, True)

        End If

    End Sub

    ''' <summary>
    ''' 入力チェック
    ''' </summary>
    ''' <param name="strErr">エラーメッセージ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function InputCheck(ByRef strErr As String) As String
        Dim strID As String = ""
        '請求先区分
        If strErr = "" Then
            '入力必須
            strErr = commoncheck.CheckHissuNyuuryoku(ddlSeikyuuSakiKbn.Text, "請求先区分")
            strID = ddlSeikyuuSakiKbn.ClientID
        End If

        '請求先コード
        If strErr = "" Then
            '入力必須
            strErr = commoncheck.CheckHissuNyuuryoku(tbxSeikyuuSakiCd.Text, "請求先コード")
            strID = tbxSeikyuuSakiCd.ClientID
        End If
        If strErr = "" And tbxSeikyuuSakiCd.Text <> "" Then
            '半角英数字
            strErr = commoncheck.ChkHankakuEisuuji(tbxSeikyuuSakiCd.Text, "請求先コード")
            strID = tbxSeikyuuSakiCd.ClientID
        End If

        '請求先枝番
        If strErr = "" Then
            '入力必須
            strErr = commoncheck.CheckHissuNyuuryoku(tbxSeikyuuSakiBrc.Text, "請求先枝番")
            strID = tbxSeikyuuSakiBrc.ClientID
        End If
        If strErr = "" And tbxSeikyuuSakiBrc.Text <> "" Then
            '半角英数字
            strErr = commoncheck.ChkHankakuEisuuji(tbxSeikyuuSakiBrc.Text, "請求先枝番")
            strID = tbxSeikyuuSakiBrc.ClientID
        End If
        If strErr = "" And (tbxNayoseCd.Text <> "" And tbxNayoseCd.Text.Length <> 5) Then
            strErr = String.Format(Messages.Instance.MSG2067E, "名寄先コード", "5").ToString
            strID = tbxNayoseCd.ClientID
        End If

        '20100925　名寄先コード、名寄先名　追加　馬艶軍↓↓↓
        '名寄先コード
        If strErr = "" And tbxNayoseCd.Text <> "" Then
            '半角英数字
            strErr = commoncheck.ChkHankakuEisuuji(tbxNayoseCd.Text, "名寄先コード")
            strID = tbxNayoseCd.ClientID
        End If
        '20100925　名寄先コード、名寄先名　追加　馬艶軍↑↑↑

        '=====================2012/03/31 車龍 要望の対応 削除↓===================================
        '入力コードと与信管理ﾏｽﾀ.名寄先ｺｰﾄﾞが一致するかチェック
        'If strErr = "" And tbxNayoseCd.Text <> "" Then
        '    If SeikyuuSakiMasterBL.SelNayoseSakiInfo(tbxNayoseCd.Text.Trim).Rows.Count = 0 Then
        '        strErr = String.Format(Messages.Instance.MSG2068E, "名寄先コード").ToString
        '        strID = tbxNayoseCd.ClientID
        '    End If
        'End If
        '=====================2012/03/31 車龍 要望の対応 削除↑===================================

        '20100712　仕様変更　追加　馬艶軍↓↓↓
        '郵便番号
        If strErr = "" And tbxYuubinNo.Text <> "" Then
            strErr = commoncheck.CheckHankakuHaifun(tbxYuubinNo.Text, "郵便番号", "1")
            If strErr <> "" Then
                strID = tbxYuubinNo.ClientID
            End If
        End If
        '住所１
        If strErr = "" And tbxJyuusyo1.Text <> "" Then
            strErr = commoncheck.CheckByte(tbxJyuusyo1.Text, 40, "住所１", kbn.ZENKAKU)
            If strErr <> "" Then
                strID = tbxJyuusyo1.ClientID
            End If
        End If
        If strErr = "" And tbxJyuusyo1.Text <> "" Then
            strErr = commoncheck.CheckKinsoku(tbxJyuusyo1.Text, "住所１")
            If strErr <> "" Then
                strID = tbxJyuusyo1.ClientID
            End If
        End If
        '電話番号
        If strErr = "" And tbxTelNo.Text <> "" Then
            strErr = commoncheck.CheckHankakuHaifun(tbxTelNo.Text, "電話番号", "1")
            If strErr <> "" Then
                strID = tbxTelNo.ClientID
            End If
        End If
        '住所２
        If strErr = "" And tbxJyuusyo2.Text <> "" Then
            strErr = commoncheck.CheckByte(tbxJyuusyo2.Text, 30, "住所２", kbn.ZENKAKU)
            If strErr <> "" Then
                strID = tbxJyuusyo2.ClientID
            End If
        End If
        If strErr = "" And tbxJyuusyo2.Text <> "" Then
            strErr = commoncheck.CheckKinsoku(tbxJyuusyo2.Text, "住所２")
            If strErr <> "" Then
                strID = tbxJyuusyo2.ClientID
            End If
        End If
        'FAX番号
        If strErr = "" And tbxFaxNo.Text <> "" Then
            strErr = commoncheck.CheckHankakuHaifun(tbxFaxNo.Text, "FAX番号", "1")
            If strErr <> "" Then
                strID = tbxFaxNo.ClientID
            End If
        End If
        '20100712　仕様変更　追加　馬艶軍↑↑↑

        '新会計事業所
        If strErr = "" And tbxSkkJigyousyoCd.Text <> "" Then
            '半角英数字
            strErr = commoncheck.ChkHankakuEisuuji(tbxSkkJigyousyoCd.Text, "新会計事業所コード")
            strID = tbxSkkJigyousyoCd.ClientID
        End If

        '担当者
        If strErr = "" And tbxTantousya.Text <> "" Then
            strErr = commoncheck.CheckByte(tbxTantousya.Text, 40, "担当者", kbn.ZENKAKU)
            If strErr <> "" Then
                strID = tbxTantousya.ClientID
            End If
        End If
        If strErr = "" And tbxTantousya.Text <> "" Then
            strErr = commoncheck.CheckKinsoku(tbxTantousya.Text, "担当者")
            If strErr <> "" Then
                strID = tbxTantousya.ClientID
            End If
        End If

        '請求締め日
        If strErr = "" And tbxSeikyuuSimeDate.Text <> "" Then
            strErr = commoncheck.CheckSime(tbxSeikyuuSimeDate.Text, "請求締め日", "31")
            If strErr <> "" Then
                strID = tbxSeikyuuSimeDate.ClientID
            End If
        End If

        '先方請求締め日
        If strErr = "" And tbxSenpouSeikyuuSimeDate.Text <> "" Then
            strErr = commoncheck.CheckSime(tbxSenpouSeikyuuSimeDate.Text, "先方請求締め日", "31")
            If strErr <> "" Then
                strID = tbxSenpouSeikyuuSimeDate.ClientID
            End If
        End If

        '請求書必着日
        If strErr = "" And tbxSeikyuusyoHittykDate.Text <> "" Then
            strErr = commoncheck.CheckSime(tbxSeikyuusyoHittykDate.Text, "請求書必着日", "31")
            If strErr <> "" Then
                strID = tbxSeikyuusyoHittykDate.ClientID
            End If
        End If

        '==================2011/06/16 車龍 削除 開始↓==========================
        ''旧請求先コード
        'If strErr = "" And tbxKyuuSeikyuuSakiCd.Text <> "" Then
        '    '半角英数字
        '    strErr = commoncheck.ChkHankakuEisuuji(tbxKyuuSeikyuuSakiCd.Text, "旧請求先コード")
        '    strID = tbxKyuuSeikyuuSakiCd.ClientID
        'End If
        '==================2011/06/16 車龍 削除 終了↑==========================

        '回収予定月数
        If strErr = "" And tbxKaisyuuYoteiGessuu.Text <> "" Then
            '2010/12/02 馬艶軍　修正　回収予定月数-数値範囲は0～12まで（整数のみ）　↓
            'strErr = commoncheck.CheckSime(tbxKaisyuuYoteiGessuu.Text, "回収予定月数", "12")
            strErr = commoncheck.CheckSime1(tbxKaisyuuYoteiGessuu.Text, "回収予定月数", "12")
            '2010/12/02 馬艶軍　修正　回収予定月数-数値範囲は0～12まで（整数のみ）　↓
            If strErr <> "" Then
                strID = tbxKaisyuuYoteiGessuu.ClientID
            End If
        End If

        '回収予定日
        If strErr = "" And tbxKaisyuuYoteiDate.Text <> "" Then
            strErr = commoncheck.CheckSime(tbxKaisyuuYoteiDate.Text, "回収予定日", "31")
            If strErr <> "" Then
                strID = tbxKaisyuuYoteiDate.ClientID
            End If
        End If

        '入金口座番号
        If strErr = "" And tbxNyuukinKouzaNo.Text <> "" Then
            strErr = commoncheck.ChkHankakuEisuuji(tbxNyuukinKouzaNo.Text, "入金口座番号")
            If strErr <> "" Then
                strID = tbxNyuukinKouzaNo.ClientID
            End If
        End If

        '回収1手形サイト月数
        If strErr = "" And tbxKaisyuu1TegataSiteGessuu.Text <> "" Then
            strErr = commoncheck.CheckSime(tbxKaisyuu1TegataSiteGessuu.Text, "回収1手形サイト月数", "12")
            If strErr <> "" Then
                strID = tbxKaisyuu1TegataSiteGessuu.ClientID
            End If
        End If

        '回収1手形サイト日
        If strErr = "" And tbxKaisyuu1TegataSiteDate.Text <> "" Then
            strErr = commoncheck.CheckSime(tbxKaisyuu1TegataSiteDate.Text, "回収1手形サイト日", "31")
            If strErr <> "" Then
                strID = tbxKaisyuu1TegataSiteDate.ClientID
            End If
        End If

        '回収境界額
        If strErr = "" And tbxKaisyuuKyoukaigaku.Text <> "" Then
            strErr = commoncheck.CheckNum(tbxKaisyuuKyoukaigaku.Text, "回収境界額", "1")
            If strErr <> "" Then
                strID = tbxKaisyuuKyoukaigaku.ClientID
            End If
        End If

        '回収2手形サイト月数
        If strErr = "" And tbxKaisyuu2TegataSiteGessuu.Text <> "" Then
            strErr = commoncheck.CheckSime(tbxKaisyuu2TegataSiteGessuu.Text, "回収2手形サイト月数", "12")
            If strErr <> "" Then
                strID = tbxKaisyuu2TegataSiteGessuu.ClientID
            End If
        End If

        '回収2手形サイト日
        If strErr = "" And tbxKaisyuu2TegataSiteDate.Text <> "" Then
            strErr = commoncheck.CheckSime(tbxKaisyuu2TegataSiteDate.Text, "回収2手形サイト日", "31")
            If strErr <> "" Then
                strID = tbxKaisyuu2TegataSiteDate.ClientID
            End If
        End If

        '回収1割合1
        If strErr = "" And tbxKaisyuu1Wariai1.Text <> "" Then
            strErr = commoncheck.CheckSuuti(tbxKaisyuu1Wariai1.Text, "回収1割合1", "100")
            If strErr <> "" Then
                strID = tbxKaisyuu1Wariai1.ClientID
            End If
        End If

        '回収1種別1
        If strErr = "" And tbxKaisyuu1Wariai1.Text <> "" Then
            If ddlKaisyuu1Syubetu1.SelectedValue.Trim = "" Then
                strErr = Messages.Instance.MSG2052E
            End If
            If strErr <> "" Then
                strID = ddlKaisyuu1Syubetu1.ClientID
            End If
        End If

        '回収1割合2
        If strErr = "" And tbxKaisyuu1Wariai2.Text <> "" Then
            strErr = commoncheck.CheckSuuti(tbxKaisyuu1Wariai2.Text, "回収1割合2", "100")
            If strErr <> "" Then
                strID = tbxKaisyuu1Wariai2.ClientID
            End If
        End If

        '回収1種別2
        If strErr = "" And tbxKaisyuu1Wariai2.Text <> "" Then
            If ddlKaisyuu1Syubetu2.SelectedValue.Trim = "" Then
                strErr = Messages.Instance.MSG2052E
            End If
            If strErr <> "" Then
                strID = ddlKaisyuu1Syubetu2.ClientID
            End If
        End If

        '回収1割合3
        If strErr = "" And tbxKaisyuu1Wariai3.Text <> "" Then
            strErr = commoncheck.CheckSuuti(tbxKaisyuu1Wariai3.Text, "回収1割合3", "100")
            If strErr <> "" Then
                strID = tbxKaisyuu1Wariai3.ClientID
            End If
        End If

        '回収1種別3
        If strErr = "" And tbxKaisyuu1Wariai3.Text <> "" Then
            If ddlKaisyuu1Syubetu3.SelectedValue.Trim = "" Then
                strErr = Messages.Instance.MSG2052E
            End If
            If strErr <> "" Then
                strID = ddlKaisyuu1Syubetu3.ClientID
            End If
        End If

        '割合チェック
        If strErr = "" Then
            strErr = commoncheck.CheckWariai(tbxKaisyuu1Wariai1.Text, tbxKaisyuu1Wariai2.Text, tbxKaisyuu1Wariai3.Text, "回収1", "100")
            If strErr <> "" Then
                strID = tbxKaisyuu1Wariai1.ClientID
            End If
        End If


        '回収2割合1
        If strErr = "" And tbxKaisyuu2Wariai1.Text <> "" Then
            strErr = commoncheck.CheckSuuti(tbxKaisyuu2Wariai1.Text, "回収2割合1", "100")
            If strErr <> "" Then
                strID = tbxKaisyuu2Wariai1.ClientID
            End If
        End If
        '回収2種別1
        If strErr = "" And tbxKaisyuu2Wariai1.Text <> "" Then
            If ddlKaisyuu2Syubetu1.SelectedValue.Trim = "" Then
                strErr = Messages.Instance.MSG2052E
            End If
            If strErr <> "" Then
                strID = ddlKaisyuu2Syubetu1.ClientID
            End If
        End If

        '回収2割合2
        If strErr = "" And tbxKaisyuu2Wariai2.Text <> "" Then
            strErr = commoncheck.CheckSuuti(tbxKaisyuu2Wariai2.Text, "回収2割合2", "100")
            If strErr <> "" Then
                strID = tbxKaisyuu2Wariai2.ClientID
            End If
        End If
        '回収2種別2
        If strErr = "" And tbxKaisyuu2Wariai2.Text <> "" Then
            If ddlKaisyuu2Syubetu2.SelectedValue.Trim = "" Then
                strErr = Messages.Instance.MSG2052E
            End If
            If strErr <> "" Then
                strID = ddlKaisyuu2Syubetu2.ClientID
            End If
        End If

        '回収2割合3
        If strErr = "" And tbxKaisyuu2Wariai3.Text <> "" Then
            strErr = commoncheck.CheckSuuti(tbxKaisyuu2Wariai3.Text, "回収2割合3", "100")
            If strErr <> "" Then
                strID = tbxKaisyuu2Wariai3.ClientID
            End If
        End If
        '回収2種別3
        If strErr = "" And tbxKaisyuu2Wariai3.Text <> "" Then
            If ddlKaisyuu2Syubetu3.SelectedValue.Trim = "" Then
                strErr = Messages.Instance.MSG2052E
            End If
            If strErr <> "" Then
                strID = ddlKaisyuu2Syubetu3.ClientID
            End If
        End If

        '割合チェック
        If strErr = "" Then
            strErr = commoncheck.CheckWariai(tbxKaisyuu2Wariai1.Text, tbxKaisyuu2Wariai2.Text, tbxKaisyuu2Wariai3.Text, "回収2", "100")
            If strErr <> "" Then
                strID = tbxKaisyuu2Wariai1.ClientID
            End If
        End If

        '============2012/05/15 車龍 407553の対応 追加↓==========================
        '統合会計得意先ｺｰﾄﾞ(半角数字)
        If (strErr = "") AndAlso (Not Me.tbxTougouKaikeiTokusakiCd.Text.Trim.Equals(String.Empty)) Then
            strErr = commoncheck.CheckHankaku(Me.tbxTougouKaikeiTokusakiCd.Text.Trim, "統合会計得意先ｺｰﾄﾞ", "1")
            If strErr <> "" Then
                strID = Me.tbxTougouKaikeiTokusakiCd.ClientID
            End If
        End If

        '安全協力会費(円、割合)(両方に登録はできない)
        If strErr = "" Then
            '円
            Dim strEn As String = Me.tbxAnzenKyouryokuKaihi1.Text.Trim
            '割合
            Dim strRitu As String = Me.tbxAnzenKyouryokuKaihi2.Text.Trim

            If (Not strEn.Equals(String.Empty)) AndAlso (Not strRitu.Equals(String.Empty)) Then
                strErr = Messages.Instance.MSG2072E
            End If

            If strErr <> "" Then
                strID = Me.tbxAnzenKyouryokuKaihi1.ClientID
            End If
        End If

        '安全協力会費_円(半角数字)
        If (strErr = "") AndAlso (Not Me.tbxAnzenKyouryokuKaihi1.Text.Trim.Equals(String.Empty)) Then
            strErr = commoncheck.CheckHankaku(Me.tbxAnzenKyouryokuKaihi1.Text.Trim.Replace(",", String.Empty), "安全協力会費_円", "1")
            If strErr <> "" Then
                strID = Me.tbxAnzenKyouryokuKaihi1.ClientID
            End If
        End If

        '安全協力会費_円(int範囲内)
        If (strErr = "") AndAlso (Not Me.tbxAnzenKyouryokuKaihi1.Text.Trim.Equals(String.Empty)) Then

            Dim intTemp = Convert.ToDouble(Me.tbxAnzenKyouryokuKaihi1.Text.Trim)
            If (intTemp < 0) OrElse (intTemp > 2147483647) Then
                strErr = String.Format(Messages.Instance.MSG2056E, "安全協力会費_円")
            End If

            If strErr <> "" Then
                strID = Me.tbxAnzenKyouryokuKaihi1.ClientID
            End If
        End If

        '安全協力会費_割合(decimal(6,5))
        If (strErr = "") AndAlso (Not Me.tbxAnzenKyouryokuKaihi2.Text.Trim.Equals(String.Empty)) Then
            strErr = commoncheck.CheckSyousuu(Me.tbxAnzenKyouryokuKaihi2.Text.Trim, "安全協力会費_割合", 1, 5)
            If strErr <> "" Then
                strID = Me.tbxAnzenKyouryokuKaihi2.ClientID
            End If
        End If

        '備考(バイト40)
        If (strErr = "") AndAlso (Not Me.tbxBikou.Text.Trim.Equals(String.Empty)) Then
            strErr = commoncheck.CheckByte(Me.tbxBikou.Text.Trim, 40, "備考", kbn.ZENKAKU)
            If strErr <> "" Then
                strID = Me.tbxBikou.ClientID
            End If
        End If

        '備考(禁則文字)
        If (strErr = "") AndAlso (Not Me.tbxBikou.Text.Trim.Equals(String.Empty)) Then
            strErr = commoncheck.CheckKinsoku(Me.tbxBikou.Text.Trim, "備考")
            If strErr <> "" Then
                strID = tbxBikou.ClientID
            End If
        End If
        '============2012/05/15 車龍 407553の対応 追加↑==========================



        Return strID

    End Function

    ''' <summary>
    ''' 請求先登録雛形マスタ設定
    ''' </summary>
    ''' <remarks></remarks>
    Sub SetSeikyuuSakiHinagata()
        Dim dtSeikyuuSakiHinagata As New Itis.Earth.DataAccess.SeikyuuSakiHinagataDataSet.m_seikyuu_saki_touroku_hinagataDataTable
        dtSeikyuuSakiHinagata = SeikyuuSakiMasterBL.SelSeikyuuSakiHinagataInfo(ddlSeikyuuSakiTourokuHinagata.SelectedValue)
        If dtSeikyuuSakiHinagata.Rows.Count > 0 Then
            'データが有る時
            '新会計事業所コード
            tbxSkkJigyousyoCd.Text = TrimNull(dtSeikyuuSakiHinagata.Item(0).skk_jigyousyo_cd)
            '担当者名
            tbxTantousya.Text = TrimNull(dtSeikyuuSakiHinagata.Item(0).tantousya_mei)
            '請求書印字物件名フラグ
            SetDropSelect(ddlSeikyuuSyoInjiBukkenMei, TrimNull(dtSeikyuuSakiHinagata.Item(0).seikyuusyo_inji_bukken_mei_flg))
            '入金口座番号
            tbxNyuukinKouzaNo.Text = TrimNull(dtSeikyuuSakiHinagata.Item(0).nyuukin_kouza_no)
            '請求締め日
            tbxSeikyuuSimeDate.Text = TrimNull(dtSeikyuuSakiHinagata.Item(0).seikyuu_sime_date)
            '先方請求締め日
            tbxSenpouSeikyuuSimeDate.Text = TrimNull(dtSeikyuuSakiHinagata.Item(0).senpou_seikyuu_sime_date)
            '相殺フラグ
            chkSousaiFlg.Checked = IIf(dtSeikyuuSakiHinagata.Item(0).sousai_flg = "0", False, True)

            '直工事請求タイミング
            SetDropSelect(ddlTykKojSeikyuuTimingFlg, TrimNull(dtSeikyuuSakiHinagata.Item(0).tyk_koj_seikyuu_timing_flg))

            '回収予定月数
            tbxKaisyuuYoteiGessuu.Text = TrimNull(dtSeikyuuSakiHinagata.Item(0).kaisyuu_yotei_gessuu)
            '回収予定日
            tbxKaisyuuYoteiDate.Text = TrimNull(dtSeikyuuSakiHinagata.Item(0).kaisyuu_yotei_date)
            '請求書必着日
            tbxSeikyuusyoHittykDate.Text = TrimNull(dtSeikyuuSakiHinagata.Item(0).seikyuusyo_hittyk_date)
            '回収1種別1
            SetDropSelect(ddlKaisyuu1Syubetu1, TrimNull(dtSeikyuuSakiHinagata.Item(0).kaisyuu1_syubetu1))
            '回収1割合1
            tbxKaisyuu1Wariai1.Text = TrimNull(dtSeikyuuSakiHinagata.Item(0).kaisyuu1_wariai1)
            '回収1手形サイト月数
            tbxKaisyuu1TegataSiteGessuu.Text = TrimNull(dtSeikyuuSakiHinagata.Item(0).kaisyuu1_tegata_site_gessuu)
            '回収1手形サイト日
            tbxKaisyuu1TegataSiteDate.Text = TrimNull(dtSeikyuuSakiHinagata.Item(0).kaisyuu1_tegata_site_date)
            '回収1請求書用紙
            SetDropSelect(ddlKaisyuu1SeikyuusyoYousi, TrimNull(dtSeikyuuSakiHinagata.Item(0).kaisyuu1_seikyuusyo_yousi))
            '回収1種別2
            SetDropSelect(ddlKaisyuu1Syubetu2, TrimNull(dtSeikyuuSakiHinagata.Item(0).kaisyuu1_syubetu2))
            '回収1割合2
            tbxKaisyuu1Wariai2.Text = TrimNull(dtSeikyuuSakiHinagata.Item(0).kaisyuu1_wariai2)
            '回収1種別3
            SetDropSelect(ddlKaisyuu1Syubetu3, TrimNull(dtSeikyuuSakiHinagata.Item(0).kaisyuu1_syubetu3))
            '回収1割合3
            tbxKaisyuu1Wariai3.Text = TrimNull(dtSeikyuuSakiHinagata.Item(0).kaisyuu1_wariai3)
            '回収境界額
            tbxKaisyuuKyoukaigaku.Text = AddComa(dtSeikyuuSakiHinagata.Item(0).kaisyuu_kyoukaigaku)
            '回収2種別1
            SetDropSelect(ddlKaisyuu2Syubetu1, TrimNull(dtSeikyuuSakiHinagata.Item(0).kaisyuu2_syubetu1))
            '回収2割合1
            tbxKaisyuu2Wariai1.Text = TrimNull(dtSeikyuuSakiHinagata.Item(0).kaisyuu2_wariai1)
            '回収2手形サイト月数
            tbxKaisyuu2TegataSiteGessuu.Text = TrimNull(dtSeikyuuSakiHinagata.Item(0).kaisyuu2_tegata_site_gessuu)
            '回収2手形サイト日
            tbxKaisyuu2TegataSiteDate.Text = TrimNull(dtSeikyuuSakiHinagata.Item(0).kaisyuu2_tegata_site_date)
            '回収2請求書用紙
            SetDropSelect(ddlKaisyuu2SeikyuusyoYousi, TrimNull(dtSeikyuuSakiHinagata.Item(0).kaisyuu2_seikyuusyo_yousi))
            '回収2種別2
            SetDropSelect(ddlKaisyuu2Syubetu2, TrimNull(dtSeikyuuSakiHinagata.Item(0).kaisyuu2_syubetu2))
            '回収2割合2
            tbxKaisyuu2Wariai2.Text = TrimNull(dtSeikyuuSakiHinagata.Item(0).kaisyuu2_wariai2)
            '回収2種別3
            SetDropSelect(ddlKaisyuu2Syubetu3, TrimNull(dtSeikyuuSakiHinagata.Item(0).kaisyuu2_syubetu3))
            '回収2割合3
            tbxKaisyuu2Wariai3.Text = TrimNull(dtSeikyuuSakiHinagata.Item(0).kaisyuu2_wariai3)

            '2013/5/29 請求書の銀行支店コード増加に伴うEarth改修 -----------------------↓
            '銀行支店コード
            SetDropSelect(Me.ddlGinkouSitenCd, TrimNull(dtSeikyuuSakiHinagata.Item(0).ginkou_siten_cd))
            '2013/5/29 請求書の銀行支店コード増加に伴うEarth改修 -----------------------↑

            If Me.hidSeikyuuKbn.Value <> "" Then
                SetDropSelect(Me.ddlSeikyuuSakiKbn, Me.hidSeikyuuKbn.Value)
            End If
            Me.hidSeikyuuKbn.Value = ""

            Me.hidConfirm.Value = ""
        Else
            If Me.hidSeikyuuKbn.Value <> "" Then
                SetDropSelect(Me.ddlSeikyuuSakiKbn, Me.hidSeikyuuKbn.Value)
            End If
            Me.hidSeikyuuKbn.Value = ""

            Me.hidConfirm.Value = ""

            '請求先登録雛形ddlSeikyuuSakiTourokuHinagata
            ddlSeikyuuSakiTourokuHinagata.Items.Clear()
            SetSeikyuuSakiTourokuHinagata(ddlSeikyuuSakiTourokuHinagata)

            'データが無い時
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('この選択した基本情報は削除されました');", True)
        End If
    End Sub

    ''' <summary>
    ''' DDLの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Sub SetDdlListInf()
        '※ドロップダウンリスト設定↓
        Dim ddlist As ListItem

        '請求先区分ddlSeikyuuSaki_Kbn
        SetKakutyouMeisyou(ddlSeikyuuSaki_Kbn, "1")
        'ddlist = New ListItem
        'ddlist.Text = ""
        'ddlist.Value = ""
        'ddlSeikyuuSaki_Kbn.Items.Add(ddlist)
        'ddlist = New ListItem
        'ddlist.Text = "加盟店"
        'ddlist.Value = "0"
        'ddlSeikyuuSaki_Kbn.Items.Add(ddlist)
        'ddlist = New ListItem
        'ddlist.Text = "調査会社"
        'ddlist.Value = "1"
        'ddlSeikyuuSaki_Kbn.Items.Add(ddlist)
        'ddlist = New ListItem
        'ddlist.Text = "営業所"
        'ddlist.Value = "2"
        'ddlSeikyuuSaki_Kbn.Items.Add(ddlist)

        '請求先区分ddlSeikyuuSakiKbn
        ddlist = New ListItem
        ddlist.Text = ""
        ddlist.Value = ""
        ddlSeikyuuSakiKbn.Items.Add(ddlist)
        ddlist = New ListItem
        ddlist.Text = "加盟店"
        ddlist.Value = "0"
        ddlSeikyuuSakiKbn.Items.Add(ddlist)
        ddlist = New ListItem
        ddlist.Text = "調査会社"
        ddlist.Value = "1"
        ddlSeikyuuSakiKbn.Items.Add(ddlist)
        ddlist = New ListItem
        ddlist.Text = "営業所"
        ddlist.Value = "2"
        ddlSeikyuuSakiKbn.Items.Add(ddlist)

        '請求先登録雛形ddlSeikyuuSakiTourokuHinagata
        SetSeikyuuSakiTourokuHinagata(ddlSeikyuuSakiTourokuHinagata)

        '請求書印字物件名ddlSeikyuuSyoInjiBukkenMei
        ddlist = New ListItem
        ddlist.Text = "施主名"
        ddlist.Value = "0"
        ddlSeikyuuSyoInjiBukkenMei.Items.Add(ddlist)
        ddlist = New ListItem
        ddlist.Text = "受注時物件名"
        ddlist.Value = "1"
        ddlSeikyuuSyoInjiBukkenMei.Items.Add(ddlist)

        '直工事請求タイミングddlTykKojSeikyuuTimingFlg
        ddlist = New ListItem
        ddlist.Text = "仕様確認時に売上で起算"
        ddlist.Value = "0"
        ddlTykKojSeikyuuTimingFlg.Items.Add(ddlist)
        ddlist = New ListItem
        ddlist.Text = "完工速報着日入力時に売上で起算"
        ddlist.Value = "1"
        ddlTykKojSeikyuuTimingFlg.Items.Add(ddlist)

        '回収1種別1ddlKaisyuu1Syubetu1
        SetKakutyouMeisyou(ddlKaisyuu1Syubetu1, "2")

        '回収1請求書用紙ddlKaisyuu1SeikyuusyoYousi
        SetKakutyouMeisyou(ddlKaisyuu1SeikyuusyoYousi, "3")

        '回収1種別2ddlKaisyuu1Syubetu2
        SetKakutyouMeisyou(ddlKaisyuu1Syubetu2, "2")

        '回収1種別3ddlKaisyuu1Syubetu3
        SetKakutyouMeisyou(ddlKaisyuu1Syubetu3, "2")

        '回収2種別1ddlKaisyuu2Syubetu1
        SetKakutyouMeisyou(ddlKaisyuu2Syubetu1, "2")

        '回収2請求書用紙ddlKaisyuu2SeikyuusyoYousi
        SetKakutyouMeisyou(ddlKaisyuu2SeikyuusyoYousi, "3")

        '回収2種別2ddlKaisyuu2Syubetu2
        SetKakutyouMeisyou(ddlKaisyuu2Syubetu2, "2")

        '回収2種別3ddlKaisyuu2Syubetu3
        SetKakutyouMeisyou(ddlKaisyuu2Syubetu3, "2")

        '===========2012/05/15 車龍 407553の対応 追加↓=========================
        '口振ＯＫフラグ
        Me.ddlKutiburiOkFlg.Items.Clear()
        '適用事項
        Me.ddlKyouryokuKaihiJigou.SelectedIndex = 0

        'データを取得する
        Dim dtList As New Data.DataTable
        dtList = SeikyuuSakiMasterBL.GetKutiburiOkFlg()

        'データをbound
        Me.ddlKutiburiOkFlg.DataValueField = "code"
        Me.ddlKutiburiOkFlg.DataTextField = "meisyou"
        Me.ddlKutiburiOkFlg.DataSource = dtList
        Me.ddlKutiburiOkFlg.DataBind()

        '先頭行
        Me.ddlKutiburiOkFlg.Items.Insert(0, New ListItem(String.Empty, String.Empty))
        '===========2012/05/15 車龍 407553の対応 追加↑=========================

        '2013/5/29 請求書の銀行支店コード増加に伴うEarth改修 -----------------------↓
        '銀行支店コード
        Me.ddlGinkouSitenCd.Items.Clear()

        'データを取得する
        Dim dtGinkouSitenCd As New Data.DataTable
        dtGinkouSitenCd = SeikyuuSakiMasterBL.GetGinkouSitenCd()

        'データをbound
        Me.ddlGinkouSitenCd.DataValueField = "code"
        Me.ddlGinkouSitenCd.DataTextField = "meisyou"
        Me.ddlGinkouSitenCd.DataSource = dtGinkouSitenCd
        Me.ddlGinkouSitenCd.DataBind()

        '先頭行
        Me.ddlGinkouSitenCd.Items.Insert(0, New ListItem(String.Empty, String.Empty))
        'デフォルト：804
        Me.ddlGinkouSitenCd.SelectedValue = "804"
        '2013/5/29 請求書の銀行支店コード増加に伴うEarth改修 -----------------------↓

    End Sub

    ''' <summary>
    ''' 請求先登録雛形マスタドロップダウンリスト設定
    ''' </summary>
    ''' <param name="ddl">ドロップダウンリスト</param>
    ''' <remarks></remarks>
    Sub SetSeikyuuSakiTourokuHinagata(ByVal ddl As DropDownList)
        Dim dtTable As New DataTable
        Dim intCount As Integer = 0
        dtTable = SeikyuuSakiMasterBL.SelSeikyuuSakiTourokuHinagataInfo()

        Dim ddlist As New ListItem
        ddlist.Text = ""
        ddlist.Value = ""
        ddl.Items.Add(ddlist)
        For intCount = 0 To dtTable.Rows.Count - 1
            ddlist = New ListItem
            ddlist.Text = TrimNull(dtTable.Rows(intCount).Item("seikyuu_saki_brc")) & "：" & TrimNull(dtTable.Rows(intCount).Item("hyouji_naiyou"))
            ddlist.Value = TrimNull(dtTable.Rows(intCount).Item("seikyuu_saki_brc"))
            ddl.Items.Add(ddlist)
        Next
    End Sub

    ''' <summary>
    ''' 拡張名称マスタm_kakutyou_meisyou
    ''' </summary>
    ''' <param name="ddl">ドロップダウンリスト</param>
    ''' <param name="strSyubetu">名称種別</param>
    ''' <remarks></remarks>
    Sub SetKakutyouMeisyou(ByVal ddl As DropDownList, ByVal strSyubetu As String)
        Dim dtTable As New DataTable
        Dim intCount As Integer = 0
        dtTable = SeikyuuSakiMasterBL.SelKakutyouInfo(strSyubetu)

        Dim ddlist As New ListItem
        ddlist.Text = ""
        ddlist.Value = ""
        ddl.Items.Add(ddlist)
        For intCount = 0 To dtTable.Rows.Count - 1
            ddlist = New ListItem
            ddlist.Text = TrimNull(dtTable.Rows(intCount).Item("meisyou"))
            ddlist.Value = TrimNull(dtTable.Rows(intCount).Item("code"))
            ddl.Items.Add(ddlist)
        Next
    End Sub

    ''' <summary>空白を削除</summary>
    Private Function TrimNull(ByVal objStr As Object) As String
        If IsDBNull(objStr) Then
            TrimNull = ""
        Else
            TrimNull = objStr.ToString.Trim
        End If
    End Function

    ''' <summary>DDL設定</summary>
    Sub SetDropSelect(ByVal ddl As DropDownList, ByVal strValue As String)

        If ddl.Items.FindByValue(strValue) IsNot Nothing Then
            ddl.SelectedValue = strValue
        Else
            ddl.SelectedIndex = 0
        End If
    End Sub

    ''' <summary>
    ''' 項目データにコマを追加
    ''' </summary>
    ''' <param name="kekka">金額</param>
    Protected Function AddComa(ByVal kekka As String) As String
        If TrimNull(kekka) = "" Then
            Return ""
        Else
            Return CInt(kekka).ToString("###,###,##0")
        End If

    End Function

    ''' <summary>
    ''' 明細部分請求先検索押下時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOK.Click
        '明細データ取得
        GetMeisaiData1(tbxSeikyuuSakiCd.Text, tbxSeikyuuSakiBrc.Text, ddlSeikyuuSakiKbn.SelectedValue)
    End Sub

    ''' <summary>
    ''' 明細データを取得
    ''' </summary>
    ''' <param name="TyousaKaisya_Cd"></param>
    ''' <param name="btn"></param>
    ''' <remarks></remarks>
    Sub GetMeisaiData1(ByVal TyousaKaisya_Cd As String, _
                      ByVal TyousaKaisyaCd As String, _
                      ByVal JigyousyoCd As String, _
                      Optional ByVal btn As String = "")

        Dim strErr As String = ""
        Dim dtSeikyuuSakiDataSet As New Itis.Earth.DataAccess.SeikyuuSakiDataSet.m_seikyuu_sakiDataTable
        dtSeikyuuSakiDataSet = SeikyuuSakiMasterBL.SelSeikyuuSakiInfo(TyousaKaisya_Cd, TyousaKaisyaCd, JigyousyoCd)

        If dtSeikyuuSakiDataSet.Rows.Count = 1 Then
            With dtSeikyuuSakiDataSet.Item(0)
                '取消
                chkTorikesi.Checked = IIf(.torikesi = "0", False, True)
                '請求先区分
                SetDropSelect(ddlSeikyuuSakiKbn, TrimNull(.seikyuu_saki_kbn))
                '請求先コード
                tbxSeikyuuSakiCd.Text = TrimNull(.seikyuu_saki_cd)
                '請求先枝番
                tbxSeikyuuSakiBrc.Text = TrimNull(.seikyuu_saki_brc)
                '請求先名
                tbxSeikyuuSakiMei.Text = TrimNull(.seikyuu_saki_mei)
                'If btn = "btnSearch" Then
                '    '請求先名
                '    tbxSeikyuuSaki_Mei.Text = TrimNull(.seikyuu_saki_mei)
                'End If

                '20100925　名寄先コード、名寄先名　追加　馬艶軍↓↓↓
                '名寄先コード
                tbxNayoseCd.Text = TrimNull(.nayose_saki_cd)
                '名寄先名
                'tbxNayoseMei.Text = TrimNull(.nayose_saki_name1)
                If tbxNayoseCd.Text <> String.Empty Then
                    Dim dtNayoseSakiTable As New Data.DataTable
                    dtNayoseSakiTable = SeikyuuSakiMasterBL.SelNayoseSakiInfo(tbxNayoseCd.Text.Trim)
                    If dtNayoseSakiTable.Rows.Count > 0 Then
                        tbxNayoseMei.Text = TrimNull(dtNayoseSakiTable.Rows(0).Item("nayose_saki_name1"))
                    Else
                        tbxNayoseMei.Text = "名寄先コード　未登録"
                    End If
                Else
                    tbxNayoseMei.Text = ""
                End If
                '20100925　名寄先コード、名寄先名　追加　馬艶軍↑↑↑

                '20100712　仕様変更　追加　馬艶軍↓↓↓
                '郵便番号
                tbxYuubinNo.Text = TrimNull(.skysy_soufu_yuubin_no)
                '住所１
                tbxJyuusyo1.Text = TrimNull(.skysy_soufu_jyuusyo1)
                '電話番号
                tbxTelNo.Text = TrimNull(.skysy_soufu_tel_no)
                '住所２
                tbxJyuusyo2.Text = TrimNull(.skysy_soufu_jyuusyo2)
                'FAX番号
                tbxFaxNo.Text = TrimNull(.skysy_soufu_fax_no)
                '20100712　仕様変更　追加　馬艶軍↑↑↑

                '担当者
                tbxTantousya.Text = TrimNull(.tantousya_mei)
                '請求書印字物件名
                SetDropSelect(ddlSeikyuuSyoInjiBukkenMei, TrimNull(.seikyuusyo_inji_bukken_mei_flg))
                '新会計事業所
                tbxSkkJigyousyoCd.Text = TrimNull(.skk_jigyousyo_cd)
                '新会計事業所
                tbxSkkJigyousyoMei.Text = TrimNull(.skk_jigyousyo_mei)
                '請求締め日
                tbxSeikyuuSimeDate.Text = TrimNull(.seikyuu_sime_date)
                '先方請求締め日
                tbxSenpouSeikyuuSimeDate.Text = TrimNull(.senpou_seikyuu_sime_date)
                ' 請求書必着日
                tbxSeikyuusyoHittykDate.Text = TrimNull(.seikyuusyo_hittyk_date)

                '==================2011/06/16 車龍 削除 開始↓==========================
                ''旧請求先コード
                'tbxKyuuSeikyuuSakiCd.Text = TrimNull(.kyuu_seikyuu_saki_cd)
                '==================2011/06/16 車龍 削除 終了↑==========================

                '==================2011/06/16 車龍  追加 開始↓========================== 
                '決算時二度締めフラグ
                Me.ddlKessanjiNidosimeFlg.SelectedValue = IIf(TrimNull(.kessanji_nidosime_flg).Equals("1"), "1", "0")
                '==================2011/06/16 車龍  追加 終了↑==========================

                '回収予定月数
                tbxKaisyuuYoteiGessuu.Text = TrimNull(.kaisyuu_yotei_gessuu)
                '回収予定日
                tbxKaisyuuYoteiDate.Text = TrimNull(.kaisyuu_yotei_date)
                '入金口座番号
                tbxNyuukinKouzaNo.Text = TrimNull(.nyuukin_kouza_no)
                '相殺フラグ
                chkSousaiFlg.Checked = IIf(.sousai_flg = "0", False, True)
                '直工事請求タイミング
                SetDropSelect(ddlTykKojSeikyuuTimingFlg, TrimNull(.tyk_koj_seikyuu_timing_flg))
                '回収1手形サイト月数
                tbxKaisyuu1TegataSiteGessuu.Text = TrimNull(.kaisyuu1_tegata_site_gessuu)
                '回収1手形サイト日
                tbxKaisyuu1TegataSiteDate.Text = TrimNull(.kaisyuu1_tegata_site_date)
                '回収1請求書用紙
                SetDropSelect(ddlKaisyuu1SeikyuusyoYousi, TrimNull(.kaisyuu1_seikyuusyo_yousi))
                '回収1種別1
                SetDropSelect(ddlKaisyuu1Syubetu1, TrimNull(.kaisyuu1_syubetu1))
                '回収1割合1
                tbxKaisyuu1Wariai1.Text = TrimNull(.kaisyuu1_wariai1)
                '回収1種別2
                SetDropSelect(ddlKaisyuu1Syubetu2, TrimNull(.kaisyuu1_syubetu2))
                '回収1割合2
                tbxKaisyuu1Wariai2.Text = TrimNull(.kaisyuu1_wariai2)
                '回収1種別3
                SetDropSelect(ddlKaisyuu1Syubetu3, TrimNull(.kaisyuu1_syubetu3))
                '回収1割合1
                tbxKaisyuu1Wariai3.Text = TrimNull(.kaisyuu1_wariai3)
                '回収境界額
                tbxKaisyuuKyoukaigaku.Text = AddComa(.kaisyuu_kyoukaigaku)

                '回収2手形サイト月数
                tbxKaisyuu2TegataSiteGessuu.Text = TrimNull(.kaisyuu2_tegata_site_gessuu)
                '回収2手形サイト日
                tbxKaisyuu2TegataSiteDate.Text = TrimNull(.kaisyuu2_tegata_site_date)
                '回収2請求書用紙
                SetDropSelect(ddlKaisyuu2SeikyuusyoYousi, TrimNull(.kaisyuu2_seikyuusyo_yousi))
                '回収2種別1
                SetDropSelect(ddlKaisyuu2Syubetu1, TrimNull(.kaisyuu2_syubetu1))
                '回収2割合1
                tbxKaisyuu2Wariai1.Text = TrimNull(.kaisyuu2_wariai1)
                '回収2種別2
                SetDropSelect(ddlKaisyuu2Syubetu2, TrimNull(.kaisyuu2_syubetu2))
                '回収2割合2
                tbxKaisyuu2Wariai2.Text = TrimNull(.kaisyuu2_wariai2)
                '回収2種別3
                SetDropSelect(ddlKaisyuu2Syubetu3, TrimNull(.kaisyuu2_syubetu3))
                '回収2割合1
                tbxKaisyuu2Wariai3.Text = TrimNull(.kaisyuu2_wariai3)

                '2013/5/29 請求書の銀行支店コード増加に伴うEarth改修 -----------------------↓
                '銀行支店コード
                SetDropSelect(ddlGinkouSitenCd, TrimNull(.ginkou_siten_cd))
                '2013/5/29 請求書の銀行支店コード増加に伴うEarth改修 -----------------------↑

                '===========2012/05/14 車龍 407553の対応 追加↓=====================
                '統合会計得意先ｺｰﾄﾞ 
                Me.tbxTougouKaikeiTokusakiCd.Text = TrimNull(.tougou_tokuisaki_cd)
                '口振ＯＫフラグ
                SetDropSelect(ddlKutiburiOkFlg, TrimNull(.koufuri_ok_flg))
                '協力会費_適用事項
                Me.ddlKyouryokuKaihiJigou.SelectedValue = dtSeikyuuSakiDataSet.Item(0).kyouryoku_kaihi_taisyou

                '安全協力会費_円
                If TrimNull(.anzen_kaihi_en).Equals(String.Empty) Then
                    Me.tbxAnzenKyouryokuKaihi1.Text = String.Empty
                Else
                    Me.tbxAnzenKyouryokuKaihi1.Text = FormatNumber(TrimNull(.anzen_kaihi_en), 0)
                End If
                '安全協力会費_％
                Me.tbxAnzenKyouryokuKaihi2.Text = TrimNull(.anzen_kaihi_wari)
                '備考
                Me.tbxBikou.Text = TrimNull(.bikou)

                '「採番」ボタンをセットする
                Call Me.SetBtnSaiban()
                '===========2012/05/14 車龍 407553の対応 追加↑=====================

                'hidUPDTime.Value = TrimNull(.upd_datetime)
                'UpdatePanelA.Update()
            End With

            'Me.hidConfirm.Value = ""

            If Not blnBtn Then
                btnSyuusei.Enabled = False
                btnTouroku.Enabled = False
            Else
                btnSyuusei.Enabled = False
                btnTouroku.Enabled = True
            End If

            'tbxSeikyuuSakiCd.Attributes.Add("readonly", "true;")
            'tbxSeikyuuSakiBrc.Attributes.Add("readonly", "true;")
            'ddlSeikyuuSakiKbn.Attributes.Add("disabled", "true;")

            'Me.btnKensakuSeikyuuSaki.Enabled = False
        Else
            MeisaiClear()
            Me.tbxSeikyuuSaki_Mei.Text = ""
            strErr = "alert('" & Messages.Instance.MSG2034E & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strErr, True)
        End If

    End Sub

    ''' <summary>
    ''' 戻るボタンの処理
    ''' </summary>
    ''' <param name="sender">system.Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        If ViewState.Item("Flg") = "close" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "aaaa", "window.close();", True)
        Else
            Server.Transfer("MasterMainteMenu.aspx")
        End If
    End Sub

    ''' <summary>
    ''' 「採番」ボタンをセットする
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2012/05/15 車龍 407553の対応 追加</history>
    Private Sub SetBtnSaiban()

        '統合会計得意先ｺｰﾄﾞ
        Dim strTougouKaikeiTokusakiCd As String
        strTougouKaikeiTokusakiCd = Me.tbxTougouKaikeiTokusakiCd.Text.Trim

        If Not strTougouKaikeiTokusakiCd.Equals(String.Empty) Then
            Me.btnSaiban.Attributes.Add("disabled", "true")
        Else
            Me.btnSaiban.Attributes.Remove("disabled")
        End If

    End Sub

    ''' <summary>
    ''' 「採番」ボタン押下時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>2012/05/15 車龍 407553の対応 追加</history>
    Private Sub btnSaiban_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaiban.Click

        '統合会計得意先ｺｰﾄﾞ
        Me.tbxTougouKaikeiTokusakiCd.Text = SeikyuuSakiMasterBL.GetMaxKutiburiOkFlg()

        '「採番」ボタンをセットする
        Call Me.SetBtnSaiban()

    End Sub
End Class
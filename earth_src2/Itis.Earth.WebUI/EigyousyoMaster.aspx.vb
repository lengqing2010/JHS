Imports Itis.Earth.BizLogic
Imports System.Data

Partial Public Class EigyousyoMaster
    Inherits System.Web.UI.Page

    'ボタン
    Private blnBtn As Boolean
    'インスタンス生成
    Private CommonSearchLogic As New Itis.Earth.BizLogic.CommonSearchLogic
    '共通チェック
    Private commoncheck As New CommonCheck
    'インスタンス生成
    Private EigyousyoMasterBL As New Itis.Earth.BizLogic.EigyousyoMasterLogic

    '詳細ボタン
    Private Const SEP_STRING As String = "$$$"
    Private Const SEARCH_SEIKYUU_SAKI As String = "SeikyuuSakiMaster.aspx"

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
        blnBtn = commonChk.CommonNinnsyou(strUserID, "eigyou_master_kanri_kengen")
        ViewState("UserId") = strUserID
        If Not IsPostBack Then
            'DDLの初期設定
            SetDdlListInf()

            '修正ボタン
            btnSyuusei.Enabled = False
            '登録ボタン
            btnTouroku.Enabled = True
            'FC店住所自動更新
            btnFcTenKousin.Enabled = False
            '請求先新規登録
            labSeikyuuSaki.Style.Add("display", "none")
            labHyouji.Style.Add("display", "none")

            '=========2012/04/10 車龍 405738 追加↓================================
            '固定チャージ
            Me.btnKoteiTyaaji.Enabled = False
            Me.lblKoteiTyaaji.Text = String.Empty
            '=========2012/04/10 車龍 405738 追加↑================================
        Else
            '請求先検索ボタン押下時
            If Me.hidConfirm.Value = "Hyouji" Then
                labSeikyuuSaki.Style.Add("display", "none")
                labHyouji.Style.Add("display", "none")

                Me.hidConfirm1.Value = "NO"
            End If
        End If

        '営業所名
        Me.tbxEigyousyo_Mei.Attributes.Add("readonly", "true;")
        '請求先
        Me.tbxSeikyuuSakiMei.Attributes.Add("readonly", "true;")
        '郵便番号
        Me.tbxYuubinNo.Attributes.Add("onblur", "SetYuubinNo(this)")

        '20100712　仕様変更　削除　馬艶軍↓↓↓
        ''請求書送付先郵便番号
        'Me.tbxSkysySoufuYuubinNo.Attributes.Add("onblur", "SetYuubinNo(this)")
        '20100712　仕様変更　削除　馬艶軍↑↑↑

        'DISABLE
        btnSearchEigyousyo.Attributes.Add("onclick", "disableButton1();")
        btnSearch.Attributes.Add("onclick", "disableButton1();")
        btnClear.Attributes.Add("onclick", "disableButton1();")
        btnFcTenKousin.Attributes.Add("onclick", "disableButton1();")
        btnKensakuYuubinNo.Attributes.Add("onclick", "disableButton1();")
        btnKensakuSeikyuuSaki.Attributes.Add("onclick", "disableButton1();")
        btnKensakuSeikyuuSyousai.Attributes.Add("onclick", "disableButton1();")
        '20100712　仕様変更　削除　馬艶軍↓↓↓
        'btnKensakuSeikyuuSyo.Attributes.Add("onclick", "disableButton1();")
        '20100712　仕様変更　削除　馬艶軍↑↑↑

        btnFC.Attributes.Add("onclick", "disableButton1();")
        btnOK.Attributes.Add("onclick", "disableButton1();")
        btnNO.Attributes.Add("onclick", "disableButton1();")

        '明細クリア
        btnClearMeisai.Attributes.Add("onclick", "if(!confirm('クリアを行ないます。\nよろしいですか？')){return false;};disableButton1();")

        '20100712　仕様変更　削除　馬艶軍↓↓↓
        ''請求先名・送付住所にコピー
        'btnKensakuSeikyuuSoufuCopy.Attributes.Add("onclick", "if(!confirm('送付住所に上書きコピーします。\nよろしいですか？')){return false;};disableButton1();")
        '20100712　仕様変更　削除　馬艶軍↑↑↑

        'JavaScript
        MakeScript()

        '半角カナ変換
        '20100712　仕様変更　削除　馬艶軍↓↓↓
        'tbxSeikyuuSakiShriSakiKana.Attributes.Add("onblur", "fncTokomozi(this)")
        '20100712　仕様変更　削除　馬艶軍↑↑↑

        tbxEigyousyoMeiKana.Attributes.Add("onblur", "fncTokomozi(this)")

        '登録ボタン
        Me.btnTouroku.Attributes.Add("onClick", "if(!fncCheck03()){return false;};disableButton1();")

        '修正ボタン
        Me.btnSyuusei.Attributes.Add("onClick", "if(!fncCheck03()){return false;};disableButton1();")

        ''調査会社コード
        'Me.tbxEigyousyoCd.Attributes.Add("onfocus", "fncFocus('" & Me.tbxEigyousyoCd.ClientID & "')")
        'Me.tbxEigyousyoCd.Attributes.Add("onblur", "fncblur('" & Me.tbxEigyousyoCd.ClientID & "')")
        ''請求先区分
        'Me.ddlSeikyuuSaki.Attributes.Add("onfocus", "fncFocus('" & Me.ddlSeikyuuSaki.ClientID & "')")
        'Me.ddlSeikyuuSaki.Attributes.Add("onblur", "fncblur('" & Me.ddlSeikyuuSaki.ClientID & "')")
        ''請求先コード
        'Me.tbxSeikyuuSakiCd.Attributes.Add("onfocus", "fncFocus('" & Me.tbxSeikyuuSakiCd.ClientID & "')")
        'Me.tbxSeikyuuSakiCd.Attributes.Add("onblur", "fncblur('" & Me.tbxSeikyuuSakiCd.ClientID & "')")
        ''請求先枝番
        'Me.tbxSeikyuuSakiBrc.Attributes.Add("onfocus", "fncFocus('" & Me.tbxSeikyuuSakiBrc.ClientID & "')")
        'Me.tbxSeikyuuSakiBrc.Attributes.Add("onblur", "fncblur('" & Me.tbxSeikyuuSakiBrc.ClientID & "')")

        '権限
        If Not blnBtn Then
            btnSyuusei.Enabled = False
            btnTouroku.Enabled = False
            btnFcTenKousin.Enabled = False
        End If

        '=========2012/04/10 車龍 405738 追加↓================================
        'CSV出力
        Me.btnCsv.Attributes.Add("onClick", "document.getElementById('" & Me.btnCsvOutput.ClientID & "').click();return false;")
        '調査会社検索
        Me.btnTyousaKaisyaCd.Attributes.Add("onclick", "disableButton1();")
        Me.btnTyousakaisya.Attributes.Add("onclick", "disableButton1();")

        '調査会社 住所情報コピー
        Me.btnTyousakaisyaCopy.Attributes.Add("onClick", "fncCopyTyousaKaisya();return false;")

        '調査会社名
        Me.tbxTyousaKaisyaMei.Attributes.Add("readonly", "true;")
        '調査会社名カナ
        Me.tbxTyousaKaisyaMeiKana.Attributes.Add("readonly", "true;")
        '(調査会社)代表者名
        Me.tbxDaihyousyaMei.Attributes.Add("readonly", "true;")
        '(調査会社)役職名
        Me.tbxYasyokuMei.Attributes.Add("readonly", "true;")
        '(調査会社)郵便番号
        Me.tbxTyousakaisyaYuubinNo.Attributes.Add("readonly", "true;")
        '(調査会社)住所1
        Me.tbxTyousakaisyaJyuusyo1.Attributes.Add("readonly", "true;")
        '(調査会社)電話番号
        Me.tbxTyousakaisyaTelNo.Attributes.Add("readonly", "true;")
        '(調査会社)住所2
        Me.tbxTyousakaisyaJyuusyo2.Attributes.Add("readonly", "true;")
        '(調査会社)FAX番号
        Me.tbxTyousakaisyaFaxNo.Attributes.Add("readonly", "true;")
        '(調査会社)JAPAN会区分
        Me.tbxJapanKaiKbn.Attributes.Add("readonly", "true;")
        '(調査会社)JAPAN会入会年月
        Me.tbxJapanKaiNyuukaiYM.Attributes.Add("readonly", "true;")
        '(調査会社)JAPAN会退会年月
        Me.tbxJapanKaiTaikaiYM.Attributes.Add("readonly", "true;")
        '(調査会社)宅地地盤調査主任資格有無フラグ
        Me.tbxTyousaSyuninSikaku.Attributes.Add("readonly", "true;")
        '(調査会社)ReportJHSトークン有無フラグ
        Me.tbxReportJHS.Attributes.Add("readonly", "true;")

        'ＦＣ 入会年月
        Me.tbxFcNyuukaiYM.Attributes.Add("onBlur", "fncCheckNengetu(this,'ＦＣ 入会年月');")
        'ＦＣ 退会年月
        Me.tbxFcTaikaiYM.Attributes.Add("onBlur", "fncCheckNengetu(this,'ＦＣ 退会年月');")
        '=========2012/04/10 車龍 405738 追加↑================================

    End Sub

    ''' <summary>
    ''' 選択編集ボタン押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim strErr As String = ""

        If strErr = "" Then
            '入力必須
            strErr = commoncheck.CheckHissuNyuuryoku(Me.tbxEigyousyo_Cd.Text, "営業所コード")
        End If
        If strErr = "" Then
            '半角英数字
            strErr = commoncheck.ChkHankakuEisuuji(tbxEigyousyo_Cd.Text, "営業所コード")
        End If
        If strErr <> "" Then
            strErr = "alert('" & strErr & "');document.getElementById('" & tbxEigyousyo_Cd.ClientID & "').focus();"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strErr, True)
        Else
            GetMeisaiData(tbxEigyousyo_Cd.Text, tbxEigyousyoCd.Text, "btnSearch")
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
        Dim strDisplayName As String = ""
        strID = InputCheck(strErr)

        '郵便番号存在チェック
        If strErr = "" And Trim(tbxYuubinNo.Text) <> "" Then
            Dim dtTable As New Data.DataTable
            dtTable = EigyousyoMasterBL.SelYuubinInfo(Trim(tbxYuubinNo.Text.Replace("-", "")))
            If dtTable.Rows.Count <> 1 Then
                strErr = String.Format(Messages.Instance.MSG2036E, "郵便番号マスタ").ToString
                strID = tbxYuubinNo.ClientID
            End If
        End If

        '調査会社存在チェック
        If strErr = "" And Trim(Me.tbxTyousaKaisyaCd.Text.Trim()) <> "" Then
            If EigyousyoMasterBL.GetTyousaKaisyaCount(Me.tbxTyousaKaisyaCd.Text.Trim()) <> 1 Then
                strErr = String.Format(Messages.Instance.MSG2036E, "調査会社マスタ").ToString
                strID = tbxTyousaKaisyaCd.ClientID
            End If
        End If

        '20100712　仕様変更　削除　馬艶軍↓↓↓
        ''請求書送付先郵便番号存在チェック
        'If strErr = "" And Trim(tbxSkysySoufuYuubinNo.Text) <> "" Then
        '    Dim dtTable As New Data.DataTable
        '    dtTable = EigyousyoMasterBL.SelYuubinInfo(Trim(tbxSkysySoufuYuubinNo.Text.Replace("-", "")))
        '    If dtTable.Rows.Count <> 1 Then
        '        strErr = String.Format(Messages.Instance.MSG2036E, "郵便番号マスタ").ToString
        '        strID = tbxSkysySoufuYuubinNo.ClientID
        '    End If
        'End If
        '20100712　仕様変更　削除　馬艶軍↑↑↑

        'エラーがある時
        If strErr <> "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('" & strErr & "');document.getElementById('" & strID & "').focus();", True)
        Else
            Dim dtEigyousyoTable As New Itis.Earth.DataAccess.EigyousyoDataSet.m_eigyousyoDataTable
            dtEigyousyoTable = EigyousyoMasterBL.SelEigyousyoInfo("", Me.tbxEigyousyoCd.Text, "btnTouroku")
            '重複チェック
            If dtEigyousyoTable.Rows.Count <> 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('マスターに重複データが存在します。');document.getElementById('" & tbxEigyousyoCd.ClientID & "').focus();", True)
                Return
            End If

            Dim strTrue As String = ""
            '【請求先新規登録】が表示されている場合
            If Me.hidConfirm1.Value = "OK" Then
                strTrue = "OK"
            End If

            'データ登録
            If EigyousyoMasterBL.InsEigyousyo(SetMeisaiData, strTrue) Then
                strErr = "alert('" & Replace(Messages.Instance.MSG018S, "@PARAM1", "営業所マスタ") & "');"
                '再取得
                GetMeisaiData("", tbxEigyousyoCd.Text, "btnTouroku")
            Else
                strErr = "alert('" & Replace(Messages.Instance.MSG019E, "@PARAM1", "営業所マスタ") & "');"
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
        Dim strDisplayName As String = ""
        'チェック
        strID = InputCheck(strErr)

        '郵便番号存在チェック
        If strErr = "" And Trim(tbxYuubinNo.Text) <> "" Then
            Dim dtTable As New Data.DataTable
            dtTable = EigyousyoMasterBL.SelYuubinInfo(Trim(tbxYuubinNo.Text.Replace("-", "")))
            If dtTable.Rows.Count <> 1 Then
                strErr = String.Format(Messages.Instance.MSG2036E, "郵便番号マスタ").ToString
                strID = tbxYuubinNo.ClientID
            End If
        End If

        '調査会社存在チェック
        If strErr = "" And Trim(Me.tbxTyousaKaisyaCd.Text.Trim()) <> "" Then
            If EigyousyoMasterBL.GetTyousaKaisyaCount(Me.tbxTyousaKaisyaCd.Text.Trim()) <> 1 Then
                strErr = String.Format(Messages.Instance.MSG2036E, "調査会社マスタ").ToString
                strID = tbxTyousaKaisyaCd.ClientID
            End If
        End If

        '20100712　仕様変更　削除　馬艶軍↓↓↓
        ''請求書送付先郵便番号存在チェック
        'If strErr = "" And Trim(tbxSkysySoufuYuubinNo.Text) <> "" Then
        '    Dim dtTable As New Data.DataTable
        '    dtTable = EigyousyoMasterBL.SelYuubinInfo(Trim(tbxSkysySoufuYuubinNo.Text.Replace("-", "")))
        '    If dtTable.Rows.Count <> 1 Then
        '        strErr = String.Format(Messages.Instance.MSG2036E, "郵便番号マスタ").ToString
        '        strID = tbxSkysySoufuYuubinNo.ClientID
        '    End If
        'End If
        '20100712　仕様変更　削除　馬艶軍↑↑↑

        'エラーがある時
        If strErr <> "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('" & strErr & "');document.getElementById('" & strID & "').focus();", True)
        Else
            Dim strTrue As String = ""
            '【請求先新規登録】が表示されている場合
            If Me.hidConfirm1.Value = "OK" Then
                strTrue = "OK"
            End If

            '更新処理
            strReturn = EigyousyoMasterBL.UpdEigyousyo(SetMeisaiData, strTrue)
            If strReturn = "0" Then
                '更新成功
                strErr = "alert('" & Replace(Messages.Instance.MSG018S, "@PARAM1", "営業所マスタ") & "');"
                '画面再描画処理
                GetMeisaiData("", tbxEigyousyoCd.Text, "btnSyuusei")
            ElseIf strReturn = "1" Then
                '更新失敗
                strErr = "alert('" & Replace(Messages.Instance.MSG019E, "@PARAM1", "営業所マスタ") & "');"
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
    ''' 請求先.検索ボタン押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnKensakuSeikyuuSaki_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKensakuSeikyuuSaki.Click

        '20100712　仕様変更　削除　馬艶軍↓↓↓
        ''活性化制御共通処理 請求先.検索ボタン押下時
        'SetKasseika()
        '20100712　仕様変更　削除　馬艶軍↑↑↑

        Dim strScript As String = ""

        '請求先情報取得
        Dim dtSeikyuuSakiTable As New Itis.Earth.DataAccess.CommonSearchDataSet.SeikyuuSakiTable1DataTable
        dtSeikyuuSakiTable = EigyousyoMasterBL.SelVSeikyuuSakiInfo(ddlSeikyuuSaki.SelectedValue, tbxSeikyuuSakiCd.Text, tbxSeikyuuSakiBrc.Text, False)

        '検索結果が1件だった場合
        If dtSeikyuuSakiTable.Rows.Count = 1 Then
            If dtSeikyuuSakiTable.Item(0).torikesi = "0" Then
                '請求先コード
                tbxSeikyuuSakiCd.Text = TrimNull(dtSeikyuuSakiTable.Item(0).seikyuu_saki_cd)
                hidSeikyuuSakiCd.Value = TrimNull(dtSeikyuuSakiTable.Item(0).seikyuu_saki_cd)
                '請求先枝番
                tbxSeikyuuSakiBrc.Text = TrimNull(dtSeikyuuSakiTable.Item(0).seikyuu_saki_brc)
                hidSeikyuuSakiBrc.Value = TrimNull(dtSeikyuuSakiTable.Item(0).seikyuu_saki_brc)
                '請求先区分
                SetDropSelect(ddlSeikyuuSaki, TrimNull(dtSeikyuuSakiTable.Item(0).seikyuu_saki_kbn))
                hidSeikyuuSakiKbn.Value = TrimNull(dtSeikyuuSakiTable.Item(0).seikyuu_saki_kbn)
                '請求先名
                tbxSeikyuuSakiMei.Text = TrimNull(dtSeikyuuSakiTable.Item(0).seikyuu_saki_mei)

                hidConfirm2.Value = ""
            Else
                '請求先名
                tbxSeikyuuSakiMei.Text = ""
                'メッセージ表示
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('指定した請求先は取消されています。');", True)
            End If
            'ElseIf dtSeikyuuSakiTable.Rows.Count = 0 Then
            '    '検索結果が0件だった場合
            '    '画面.請求先区分＝"2：営業所" かつ 画面.調査会社コード＝画面.請求先コード かつ 画面.事業所コード＝画面.請求先枝番の場合
            '    If (ddlSeikyuuSaki.SelectedValue = "2") And (tbxSeikyuuSakiCd.Text <> "") And (tbxSeikyuuSakiBrc.Text <> "") And (tbxEigyousyoCd.Text.ToUpper = tbxSeikyuuSakiCd.Text.ToUpper) Then
            '        'メッセージ表示
            '        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "fncConfirm();", True)
            '        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "confrim", "if(confirm('この内容の登録時に請求先マスタに登録しますか？')){ window.setTimeout('objEBI(\'" & Me.btnOK.ClientID & "\').click()',10);}else{ window.setTimeout('objEBI(\'" & Me.btnNO.ClientID & "\').click()',10);}; ", True)
            '    Else
            '        '請求先名
            '        tbxSeikyuuSakiMei.Text = ""
            '        strScript = "objSrchWin = window.open('search_Seikyuusaki.aspx?blnDelete=True&&Kbn='+escape('請求先')+'&FormName=" & _
            '                                     Me.Page.Form.Name & "&objKbn=" & _
            '                                     ddlSeikyuuSaki.ClientID & _
            '                                     "&objHidKbn=" & hidSeikyuuSakiKbn.ClientID & _
            '                                     "&objCd=" & tbxSeikyuuSakiCd.ClientID & _
            '                                     "&objHidCd=" & hidSeikyuuSakiCd.ClientID & _
            '                                     "&objBrc=" & tbxSeikyuuSakiBrc.ClientID & _
            '                                     "&hidConfirm2=" & hidConfirm2.ClientID & _
            '                                     "&objHidBrc=" & hidSeikyuuSakiBrc.ClientID & _
            '                                     "&objMei=" & tbxSeikyuuSakiMei.ClientID & _
            '                                     "&strKbn='+escape(eval('document.all.'+'" & _
            '                                     ddlSeikyuuSaki.ClientID & "').value)+'&strCd='+escape(eval('document.all.'+'" & _
            '                                     tbxSeikyuuSakiCd.ClientID & "').value)+'&strBrc='+escape(eval('document.all.'+'" & _
            '                                     tbxSeikyuuSakiBrc.ClientID & "').value), 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');"
            '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strScript, True)
            '    End If
        Else
            '請求先名
            tbxSeikyuuSakiMei.Text = ""
            strScript = "objSrchWin = window.open('search_Seikyuusaki.aspx?strFlg=1&blnDelete=True&Kbn='+escape('請求先')+'&FormName=" & _
                                             Me.Page.Form.Name & "&objKbn=" & _
                                             ddlSeikyuuSaki.ClientID & _
                                             "&objHidKbn=" & hidSeikyuuSakiKbn.ClientID & _
                                             "&objCd=" & tbxSeikyuuSakiCd.ClientID & _
                                             "&objHidCd=" & hidSeikyuuSakiCd.ClientID & _
                                             "&hidConfirm2=" & hidConfirm2.ClientID & _
                                             "&objBrc=" & tbxSeikyuuSakiBrc.ClientID & _
                                             "&objHidBrc=" & hidSeikyuuSakiBrc.ClientID & _
                                             "&objMei=" & tbxSeikyuuSakiMei.ClientID & _
                                             "&strKbn='+escape(eval('document.all.'+'" & _
                                             ddlSeikyuuSaki.ClientID & "').value)+'&strCd='+escape(eval('document.all.'+'" & _
                                             tbxSeikyuuSakiCd.ClientID & "').value)+'&strBrc='+escape(eval('document.all.'+'" & _
                                             tbxSeikyuuSakiBrc.ClientID & "').value), 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strScript, True)
        End If

    End Sub

    '20100712　仕様変更　削除　馬艶軍↓↓↓
    '''' <summary>
    '''' 活性化制御共通処理 請求先.検索ボタン押下時
    '''' </summary>
    '''' <remarks></remarks>
    'Sub SetKasseika()
    '    If ddlSeikyuuSaki.SelectedValue = "2" And tbxEigyousyoCd.Text.ToUpper = tbxSeikyuuSakiCd.Text.ToUpper Then
    '        tbxSeikyuuSakiShriSakiMei.Attributes.Remove("readonly")
    '        tbxSeikyuuSakiShriSakiKana.Attributes.Remove("readonly")
    '        tbxSkysySoufuYuubinNo.Attributes.Remove("readonly")
    '        tbxSkysySoufuJyuusyo1.Attributes.Remove("readonly")
    '        tbxSkysySoufuTelNo.Attributes.Remove("readonly")
    '        tbxSkysySoufuJyuusyo2.Attributes.Remove("readonly")
    '        tbxShriYouFaxNo.Attributes.Remove("readonly")
    '        btnKensakuSeikyuuSoufuCopy.Enabled = True
    '        btnKensakuSeikyuuSyo.Enabled = True
    '    Else
    '        tbxSeikyuuSakiShriSakiMei.Text = ""
    '        tbxSeikyuuSakiShriSakiKana.Text = ""
    '        tbxSkysySoufuYuubinNo.Text = ""
    '        tbxSkysySoufuJyuusyo1.Text = ""
    '        tbxSkysySoufuTelNo.Text = ""
    '        tbxSkysySoufuJyuusyo2.Text = ""
    '        tbxShriYouFaxNo.Text = ""

    '        tbxSeikyuuSakiShriSakiMei.Attributes.Add("readonly", "true;")
    '        tbxSeikyuuSakiShriSakiKana.Attributes.Add("readonly", "true;")
    '        tbxSkysySoufuYuubinNo.Attributes.Add("readonly", "true;")
    '        tbxSkysySoufuJyuusyo1.Attributes.Add("readonly", "true;")
    '        tbxSkysySoufuTelNo.Attributes.Add("readonly", "true;")
    '        tbxSkysySoufuJyuusyo2.Attributes.Add("readonly", "true;")
    '        tbxShriYouFaxNo.Attributes.Add("readonly", "true;")
    '        btnKensakuSeikyuuSoufuCopy.Enabled = False
    '        btnKensakuSeikyuuSyo.Enabled = False
    '    End If
    'End Sub
    '20100712　仕様変更　削除　馬艶軍↑↑↑

    ''' <summary>
    ''' 営業所検索ボタン押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSearchEigyousyo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchEigyousyo.Click
        Dim strScript As String = ""
        'データ取得
        Dim dtEigyousyoTable As New Itis.Earth.DataAccess.EigyousyoDataSet.m_eigyousyoDataTable
        dtEigyousyoTable = EigyousyoMasterBL.SelEigyousyoInfo(tbxEigyousyo_Cd.Text, "", "btnSearch")

        '検索結果が1件だった場合
        If dtEigyousyoTable.Rows.Count = 1 Then
            tbxEigyousyo_Cd.Text = dtEigyousyoTable.Item(0).eigyousyo_cd.ToString 
            tbxEigyousyo_Mei.Text = dtEigyousyoTable.Item(0).eigyousyo_mei
        Else
            tbxEigyousyo_Mei.Text = ""
            strScript = "objSrchWin = window.open('search_common.aspx?Kbn='+escape('営業所')+'&soukoCd='+escape('#')+'&FormName=" & _
            Me.Page.Form.Name & "&objCd=" & _
           tbxEigyousyo_Cd.ClientID & _
                    "&objMei=" & tbxEigyousyo_Mei.ClientID & _
                    "&strCd='+escape(eval('document.all.'+'" & _
                    tbxEigyousyo_Cd.ClientID & "').value)+'&strMei='+escape(eval('document.all.'+'" & _
                    tbxEigyousyo_Mei.ClientID & "').value), 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strScript, True)
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

        data = (EigyousyoMasterBL.GetMailAddress(Me.tbxYuubinNo.Text.Replace("-", String.Empty).Trim))
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

    '20100712　仕様変更　削除　馬艶軍↓↓↓
    '''' <summary>
    '''' 請求書送付先郵便番号.検索ボタン押下時処理
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <remarks></remarks>
    'Private Sub btnKensakuSeikyuuSyo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKensakuSeikyuuSyo.Click
    '    '住所
    '    Dim data As DataSet
    '    Dim jyuusyo As String
    '    Dim jyuusyoMei As String
    '    Dim jyuusyoNo As String

    '    '住所取得
    '    Dim csScript As New StringBuilder

    '    data = (EigyousyoMasterBL.GetMailAddress(Me.tbxSkysySoufuYuubinNo.Text.Replace("-", String.Empty).Trim))
    '    If data.Tables(0).Rows.Count = 1 Then

    '        jyuusyo = data.Tables(0).Rows(0).Item(0).ToString
    '        jyuusyoNo = jyuusyo.Split(",")(0)
    '        jyuusyoMei = GetJyusho(jyuusyo.Split(",")(1))
    '        If jyuusyoNo.Length > 3 Then
    '            jyuusyoNo = jyuusyoNo.Substring(0, 3) & "-" & jyuusyoNo.Substring(3, jyuusyoNo.Length - 3)
    '        End If

    '        csScript.AppendLine("if(document.getElementById('" & Me.tbxSkysySoufuJyuusyo1.ClientID & "').value!='' || document.getElementById('" & Me.tbxSkysySoufuJyuusyo2.ClientID & "').value!=''){" & vbCrLf)
    '        csScript.AppendLine("if (!confirm('既存データがありますが上書きしてよろしいですか。')){}else{ " & vbCrLf)

    '        csScript.AppendLine("document.getElementById('" & Me.tbxSkysySoufuJyuusyo1.ClientID & "').value = '" & jyuusyoMei.Split(",")(0) & "';" & vbCrLf)
    '        csScript.AppendLine("document.getElementById('" & Me.tbxSkysySoufuJyuusyo2.ClientID & "').value = '" & jyuusyoMei.Split(",")(1) & "';" & vbCrLf)
    '        csScript.AppendLine("document.getElementById('" & Me.tbxSkysySoufuYuubinNo.ClientID & "').value = '" & jyuusyoNo & "';" & vbCrLf)

    '        csScript.AppendLine("}" & vbCrLf)

    '        csScript.AppendLine("}else{" & vbCrLf)
    '        csScript.AppendLine("document.getElementById('" & Me.tbxSkysySoufuJyuusyo1.ClientID & "').value = '" & jyuusyoMei.Split(",")(0) & "';" & vbCrLf)
    '        csScript.AppendLine("document.getElementById('" & Me.tbxSkysySoufuJyuusyo2.ClientID & "').value = '" & jyuusyoMei.Split(",")(1) & "';" & vbCrLf)
    '        csScript.AppendLine("document.getElementById('" & Me.tbxSkysySoufuYuubinNo.ClientID & "').value = '" & jyuusyoNo & "';" & vbCrLf)

    '        csScript.AppendLine("}" & vbCrLf)
    '    Else
    '        csScript.AppendLine("fncOpenwindowYuubin('" & Me.tbxSkysySoufuYuubinNo.ClientID & "','" & Me.tbxSkysySoufuJyuusyo1.ClientID & "','" & Me.tbxSkysySoufuJyuusyo2.ClientID & "');")
    '    End If
    '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "openWindowYuubin", csScript.ToString, True)
    'End Sub
    '20100712　仕様変更　削除　馬艶軍↑↑↑

    '20100712　仕様変更　削除　馬艶軍↓↓↓
    '''' <summary>
    '''' 請求先名・送付住所にコピー
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <remarks></remarks>
    'Protected Sub btnKensakuSeikyuuSoufuCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKensakuSeikyuuSoufuCopy.Click
    '    'tbxSeikyuuSakiShriSakiMei.Text = tbxEigyousyoMei.Text
    '    'tbxSeikyuuSakiShriSakiKana.Text = tbxEigyousyoMeiKana.Text
    '    tbxSkysySoufuYuubinNo.Text = tbxYuubinNo.Text
    '    tbxSkysySoufuJyuusyo1.Text = tbxJyuusyo1.Text
    '    tbxSkysySoufuTelNo.Text = tbxTelNo.Text
    '    tbxSkysySoufuJyuusyo2.Text = tbxJyuusyo2.Text
    '    tbxShriYouFaxNo.Text = tbxFaxNo.Text
    'End Sub
    '20100712　仕様変更　削除　馬艶軍↑↑↑

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
        '営業所
        tbxEigyousyo_Cd.Text = ""
        '営業所名
        tbxEigyousyo_Mei.Text = ""
    End Sub

    ''' <summary>
    ''' 登録と修正値を持ち
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SetMeisaiData() As Itis.Earth.DataAccess.EigyousyoDataSet.m_eigyousyoDataTable
        Dim dtEigyousyoDataSet As New Itis.Earth.DataAccess.EigyousyoDataSet.m_eigyousyoDataTable

        dtEigyousyoDataSet.Rows.Add(dtEigyousyoDataSet.NewRow)
        '取消
        dtEigyousyoDataSet.Item(0).torikesi = IIf(chkTorikesi.Checked, "1", "0")
        '営業所コード
        dtEigyousyoDataSet.Item(0).eigyousyo_cd = tbxEigyousyoCd.Text.ToUpper
        '営業所名
        dtEigyousyoDataSet.Item(0).eigyousyo_mei = tbxEigyousyoMei.Text
        '営業所カナ
        dtEigyousyoDataSet.Item(0).eigyousyo_kana = tbxEigyousyoMeiKana.Text
        '営業所名印字有無
        dtEigyousyoDataSet.Item(0).eigyousyo_mei_inji_umu = Me.ddlEigyousyoMeiInjiUmu.SelectedValue
        '部署名
        dtEigyousyoDataSet.Item(0).busyo_mei = tbxBusyoMei.Text
        '郵便番号
        dtEigyousyoDataSet.Item(0).yuubin_no = tbxYuubinNo.Text
        '住所１
        dtEigyousyoDataSet.Item(0).jyuusyo1 = tbxJyuusyo1.Text
        '電話番号
        dtEigyousyoDataSet.Item(0).tel_no = tbxTelNo.Text
        '住所２
        dtEigyousyoDataSet.Item(0).jyuusyo2 = tbxJyuusyo2.Text
        'FAX番号
        dtEigyousyoDataSet.Item(0).fax_no = tbxFaxNo.Text
        
        '請求先区分
        dtEigyousyoDataSet.Item(0).seikyuu_saki_kbn = ddlSeikyuuSaki.SelectedValue
        '請求先コード
        dtEigyousyoDataSet.Item(0).seikyuu_saki_cd = tbxSeikyuuSakiCd.Text.ToUpper
        '請求先枝番
        dtEigyousyoDataSet.Item(0).seikyuu_saki_brc = tbxSeikyuuSakiBrc.Text.ToUpper

        '=========2012/04/10 車龍 405738 追加↓================================
        With dtEigyousyoDataSet.Item(0)
            '集計FC用ｺｰﾄﾞ
            .syuukei_fc_ten_cd = Me.tbxSyuukeiFcCd.Text.Trim
            'エリアコード
            .eria_cd = Me.ddlArea.SelectedValue.Trim
            'ブロックコード
            .block_cd = Me.ddlBlock.SelectedValue.Trim
            'FC店区分
            .fc_ten_kbn = Me.ddlFcTenKbn.SelectedValue.Trim
            'FC入会年月
            .fc_nyuukai_date = Me.tbxFcNyuukaiYM.Text.Trim
            'FC退会年月
            .fc_taikai_date = Me.tbxFcTaikaiYM.Text.Trim
            '(FC)調査会社コード
            .fc_tys_kaisya_cd = Left(Me.tbxTyousaKaisyaCd.Text, 4)
            '(FC)事業所コード
            .fc_jigyousyo_cd = Mid(Me.tbxTyousaKaisyaCd.Text.Trim, 5, 2)
        End With
        '=========2012/04/10 車龍 405738 追加↑================================

        '20100712　仕様変更　削除　馬艶軍↓↓↓
        ''請求先名
        'dtEigyousyoDataSet.Item(0).seikyuu_saki_mei = tbxSeikyuuSakiShriSakiMei.Text
        ''請求先名カナ
        'dtEigyousyoDataSet.Item(0).seikyuu_saki_kana = tbxSeikyuuSakiShriSakiKana.Text
        ''請求書送付先郵便番号
        'dtEigyousyoDataSet.Item(0).skysy_soufu_yuubin_no = tbxSkysySoufuYuubinNo.Text
        ''請求書送付先住所１
        'dtEigyousyoDataSet.Item(0).skysy_soufu_jyuusyo1 = tbxSkysySoufuJyuusyo1.Text
        ''請求書送付先電話番号
        'dtEigyousyoDataSet.Item(0).skysy_soufu_tel_no = tbxSkysySoufuTelNo.Text
        ''請求書送付先住所２
        'dtEigyousyoDataSet.Item(0).skysy_soufu_jyuusyo2 = tbxSkysySoufuJyuusyo2.Text
        ''請求書送付先FAX番号
        'dtEigyousyoDataSet.Item(0).skysy_soufu_fax_no = tbxShriYouFaxNo.Text
        '20100712　仕様変更　削除　馬艶軍↑↑↑

        '登録、更新ログインユーザーID
        dtEigyousyoDataSet.Item(0).upd_login_user_id = ViewState("UserId")
        dtEigyousyoDataSet.Item(0).add_login_user_id = ViewState("UserId")
        dtEigyousyoDataSet.Item(0).upd_datetime = hidUPDTime.Value

        Return dtEigyousyoDataSet
    End Function

    ''' <summary>
    ''' 入力チェック
    ''' </summary>
    ''' <param name="strErr">エラーメッセージ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function InputCheck(ByRef strErr As String) As String
        Dim strID As String = ""
        '営業所コード
        If strErr = "" Then
            strErr = commoncheck.CheckHissuNyuuryoku(tbxEigyousyoCd.Text, "営業所コード")
            If strErr <> "" Then
                strID = tbxEigyousyoCd.ClientID
            End If
        End If
        If strErr = "" And tbxEigyousyoCd.Text <> "" Then
            strErr = commoncheck.ChkHankakuEisuuji(tbxEigyousyoCd.Text, "営業所コード")
            If strErr <> "" Then
                strID = tbxEigyousyoCd.ClientID
            End If
        End If
        '営業所名印字有無
        If strErr = "" Then
            strErr = commoncheck.CheckHissuNyuuryoku(ddlEigyousyoMeiInjiUmu.Text, "営業所名印字有無コード")
            If strErr <> "" Then
                strID = ddlEigyousyoMeiInjiUmu.ClientID
            End If
        End If
        '営業所名
        If strErr = "" Then
            strErr = commoncheck.CheckHissuNyuuryoku(tbxEigyousyoMei.Text, "営業所名")
            If strErr <> "" Then
                strID = tbxEigyousyoMei.ClientID
            End If
        End If
        If strErr = "" And tbxEigyousyoMei.Text <> "" Then
            strErr = commoncheck.CheckByte(tbxEigyousyoMei.Text, 40, "営業所名", kbn.ZENKAKU)
            If strErr <> "" Then
                strID = tbxEigyousyoMei.ClientID
            End If
        End If
        If strErr = "" And tbxEigyousyoMei.Text <> "" Then
            strErr = commoncheck.CheckKinsoku(tbxEigyousyoMei.Text, "営業所名")
            If strErr <> "" Then
                strID = tbxEigyousyoMei.ClientID
            End If
        End If
        '営業所名カナ
        If strErr = "" And tbxEigyousyoMeiKana.Text <> "" Then
            strErr = commoncheck.CheckByte(tbxEigyousyoMeiKana.Text, 20, "営業所カナ", kbn.ZENKAKU)
            If strErr <> "" Then
                strID = tbxEigyousyoMeiKana.ClientID
            End If
        End If
        If strErr = "" And tbxEigyousyoMeiKana.Text <> "" Then
            strErr = commoncheck.CheckKinsoku(tbxEigyousyoMeiKana.Text, "営業所カナ")
            If strErr <> "" Then
                strID = tbxEigyousyoMeiKana.ClientID
            End If
        End If
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
        '部署名
        If strErr = "" And tbxBusyoMei.Text <> "" Then
            strErr = commoncheck.CheckByte(tbxBusyoMei.Text, 50, "部署名", kbn.ZENKAKU)
            If strErr <> "" Then
                strID = tbxBusyoMei.ClientID
            End If
        End If
        If strErr = "" And tbxBusyoMei.Text <> "" Then
            strErr = commoncheck.CheckKinsoku(tbxBusyoMei.Text, "部署名")
            If strErr <> "" Then
                strID = tbxBusyoMei.ClientID
            End If
        End If

        '請求先コード
        If strErr = "" And tbxSeikyuuSakiCd.Text <> "" Then
            strErr = commoncheck.ChkHankakuEisuuji(tbxSeikyuuSakiCd.Text, "請求先コード")
            If strErr <> "" Then
                strID = tbxSeikyuuSakiCd.ClientID
            End If
        End If
        '請求先枝番
        If strErr = "" And tbxSeikyuuSakiBrc.Text <> "" Then
            strErr = commoncheck.ChkHankakuEisuuji(tbxSeikyuuSakiBrc.Text, "請求先枝番")
            If strErr <> "" Then
                strID = tbxSeikyuuSakiBrc.ClientID
            End If
        End If

        '=========2012/04/10 車龍 405738 追加↓================================
        '集計FC用ｺｰﾄﾞ(半角英数字)
        If strErr = "" And Me.tbxSyuukeiFcCd.Text.Trim <> "" Then
            strErr = commoncheck.ChkHankakuEisuuji(Me.tbxSyuukeiFcCd.Text.Trim, "集計FC用コード")
            If strErr <> "" Then
                strID = Me.tbxSyuukeiFcCd.ClientID
            End If
        End If

        'ＦＣ 入会年月(有効日付チェック)
        If strErr = "" And tbxFcNyuukaiYM.Text.Trim <> "" Then
            strErr = commoncheck.CheckYuukouHiduke(tbxFcNyuukaiYM.Text.Trim, "ＦＣ 入会年月")
            If strErr <> "" Then
                strID = tbxFcNyuukaiYM.ClientID
            End If
        End If
        'ＦＣ 退会年月(有効日付チェック)
        If strErr = "" And tbxFcTaikaiYM.Text.Trim <> "" Then
            strErr = commoncheck.CheckYuukouHiduke(tbxFcTaikaiYM.Text.Trim, "ＦＣ 退会年月")
            If strErr <> "" Then
                strID = tbxFcTaikaiYM.ClientID
            End If
        End If

        '(FC)調査会社(FC店区分が加入の場合、入力チェックが必要)
        If (strErr = "") AndAlso (Me.ddlFcTenKbn.SelectedValue.Trim.Equals("1")) Then
            strErr = commoncheck.CheckHissuNyuuryoku(Me.tbxTyousaKaisyaCd.Text, "調査会社コード")
            If strErr <> "" Then
                strID = Me.tbxTyousaKaisyaCd.ClientID
            End If
        End If

        '(FC)調査会社(半角英数字)
        If (strErr = "") AndAlso (Not Me.tbxTyousaKaisyaCd.Text.Trim.Equals(String.Empty)) Then
            strErr = commoncheck.ChkHankakuEisuuji(Me.tbxTyousaKaisyaCd.Text.Trim, "調査会社コード")
            If strErr <> "" Then
                strID = Me.tbxTyousaKaisyaCd.ClientID
            End If
        End If

        '=========2012/04/10 車龍 405738 追加↑================================

        '20100712　仕様変更　削除　馬艶軍↓↓↓
        ''請求先名
        'If strErr = "" And tbxSeikyuuSakiShriSakiMei.Text <> "" Then
        '    strErr = commoncheck.CheckByte(tbxSeikyuuSakiShriSakiMei.Text, 80, "請求先名", kbn.ZENKAKU)
        '    If strErr <> "" Then
        '        strID = tbxSeikyuuSakiShriSakiMei.ClientID
        '    End If
        'End If
        'If strErr = "" And tbxSeikyuuSakiShriSakiMei.Text <> "" Then
        '    strErr = commoncheck.CheckKinsoku(tbxSeikyuuSakiShriSakiMei.Text, "請求先名")
        '    If strErr <> "" Then
        '        strID = tbxSeikyuuSakiShriSakiMei.ClientID
        '    End If
        'End If
        ''請求先名カナ
        'If strErr = "" And tbxSeikyuuSakiShriSakiKana.Text <> "" Then
        '    strErr = commoncheck.CheckByte(tbxSeikyuuSakiShriSakiKana.Text, 40, "請求先カナ", kbn.ZENKAKU)
        '    If strErr <> "" Then
        '        strID = tbxSeikyuuSakiShriSakiKana.ClientID
        '    End If
        'End If
        'If strErr = "" And tbxSeikyuuSakiShriSakiKana.Text <> "" Then
        '    strErr = commoncheck.CheckKinsoku(tbxSeikyuuSakiShriSakiKana.Text, "請求先カナ")
        '    If strErr <> "" Then
        '        strID = tbxSeikyuuSakiShriSakiKana.ClientID
        '    End If
        'End If
        ''請求書送付先郵便番号
        'If strErr = "" And tbxSkysySoufuYuubinNo.Text <> "" Then
        '    strErr = commoncheck.CheckHankakuHaifun(tbxSkysySoufuYuubinNo.Text, "請求書送付先郵便番号", "1")
        '    If strErr <> "" Then
        '        strID = tbxSkysySoufuYuubinNo.ClientID
        '    End If
        'End If
        ''請求書送付先住所１
        'If strErr = "" And tbxSkysySoufuJyuusyo1.Text <> "" Then
        '    strErr = commoncheck.CheckByte(tbxSkysySoufuJyuusyo1.Text, 40, "請求書送付先住所１", kbn.ZENKAKU)
        '    If strErr <> "" Then
        '        strID = tbxSkysySoufuJyuusyo1.ClientID
        '    End If
        'End If
        'If strErr = "" And tbxSkysySoufuJyuusyo1.Text <> "" Then
        '    strErr = commoncheck.CheckKinsoku(tbxSkysySoufuJyuusyo1.Text, "請求書送付先住所１")
        '    If strErr <> "" Then
        '        strID = tbxSkysySoufuJyuusyo1.ClientID
        '    End If
        'End If
        ''請求書送付先電話番号
        'If strErr = "" And tbxSkysySoufuTelNo.Text <> "" Then
        '    strErr = commoncheck.CheckHankakuHaifun(tbxSkysySoufuTelNo.Text, "請求書送付先電話番号", "1")
        '    If strErr <> "" Then
        '        strID = tbxSkysySoufuTelNo.ClientID
        '    End If
        'End If
        ''請求書送付先住所２
        'If strErr = "" And tbxSkysySoufuJyuusyo2.Text <> "" Then
        '    strErr = commoncheck.CheckByte(tbxSkysySoufuJyuusyo2.Text, 40, "請求書送付先住所２", kbn.ZENKAKU)
        '    If strErr <> "" Then
        '        strID = tbxSkysySoufuJyuusyo2.ClientID
        '    End If
        'End If
        'If strErr = "" And tbxSkysySoufuJyuusyo2.Text <> "" Then
        '    strErr = commoncheck.CheckKinsoku(tbxSkysySoufuJyuusyo2.Text, "請求書送付先住所２")
        '    If strErr <> "" Then
        '        strID = tbxSkysySoufuJyuusyo2.ClientID
        '    End If
        'End If
        ''請求書送付先FAX番号
        'If strErr = "" And tbxShriYouFaxNo.Text <> "" Then
        '    strErr = commoncheck.CheckHankakuHaifun(tbxShriYouFaxNo.Text, "請求書送付先FAX番号", "1")
        '    If strErr <> "" Then
        '        strID = tbxShriYouFaxNo.ClientID
        '    End If
        'End If
        '20100712　仕様変更　削除　馬艶軍↑↑↑

        Return strID

    End Function

    ''' <summary>
    ''' 明細データを取得
    ''' </summary>
    ''' <param name="Eigyousyo_Cd"></param>
    ''' <param name="btn"></param>
    ''' <remarks></remarks>
    Sub GetMeisaiData(ByVal Eigyousyo_Cd As String, _
                      ByVal EigyousyoCd As String, _
                      Optional ByVal btn As String = "")

        Dim strErr As String = ""
        Dim dtEigyousyoDataSet As New Itis.Earth.DataAccess.EigyousyoDataSet.m_eigyousyoDataTable
        dtEigyousyoDataSet = EigyousyoMasterBL.SelEigyousyoInfo(Eigyousyo_Cd, EigyousyoCd, btn)

        If dtEigyousyoDataSet.Rows.Count = 1 Then
            With dtEigyousyoDataSet.Item(0)
                '取消
                chkTorikesi.Checked = IIf(.torikesi = "0", False, True)
                '営業所コード
                tbxEigyousyoCd.Text = TrimNull(.eigyousyo_cd)
                '営業所名
                tbxEigyousyoMei.Text = TrimNull(.eigyousyo_mei)
                If btn = "btnSearch" Then
                    tbxEigyousyo_Mei.Text = TrimNull(.eigyousyo_mei)
                    tbxEigyousyo_Cd.Text = TrimNull(.eigyousyo_cd).ToUpper
                End If
                '調査会社名カナ
                tbxEigyousyoMeiKana.Text = TrimNull(.eigyousyo_kana)
                '営業所名印字有無
                SetDropSelect(ddlEigyousyoMeiInjiUmu, TrimNull(.eigyousyo_mei_inji_umu))

                '郵便番号
                tbxYuubinNo.Text = TrimNull(.yuubin_no)
                '住所１
                tbxJyuusyo1.Text = TrimNull(.jyuusyo1)
                '電話番号
                tbxTelNo.Text = TrimNull(.tel_no)
                '住所２
                tbxJyuusyo2.Text = TrimNull(.jyuusyo2)
                'FAX番号
                tbxFaxNo.Text = TrimNull(.fax_no)
                '部署名
                tbxBusyoMei.Text = TrimNull(.busyo_mei)

                '請求先区分
                SetDropSelect(ddlSeikyuuSaki, TrimNull(.seikyuu_saki_kbn))
                '請求先コード
                tbxSeikyuuSakiCd.Text = TrimNull(.seikyuu_saki_cd).ToUpper
                '請求先枝番
                tbxSeikyuuSakiBrc.Text = TrimNull(.seikyuu_saki_brc).ToUpper
                '請求先名
                tbxSeikyuuSakiMei.Text = TrimNull(.seikyuu_saki_mei1)
                '請求先コード
                hidSeikyuuSakiCd.Value = TrimNull(.seikyuu_saki_cd).ToUpper
                '請求先枝番
                hidSeikyuuSakiBrc.Value = TrimNull(.seikyuu_saki_brc).ToUpper
                '請求先区分
                hidSeikyuuSakiKbn.Value = TrimNull(.seikyuu_saki_kbn)
                '=========2012/04/10 車龍 405738 追加↓================================
                '集計FC用ｺｰﾄﾞ
                Me.tbxSyuukeiFcCd.Text = TrimNull(.syuukei_fc_ten_cd)
                'エリアコード
                Call Me.SetDropSelect(Me.ddlArea, TrimNull(.eria_cd))
                'ブロックコード
                Call Me.SetDropSelect(Me.ddlBlock, TrimNull(.block_cd))
                'FC店区分
                Call Me.SetDropSelect(Me.ddlFcTenKbn, TrimNull(.fc_ten_kbn))
                'FC入会年月
                Me.tbxFcNyuukaiYM.Text = TrimNull(.fc_nyuukai_date)
                'FC退会年月
                Me.tbxFcTaikaiYM.Text = TrimNull(.fc_taikai_date)
                '(FC)調査会社コード
                Me.tbxTyousaKaisyaCd.Text = TrimNull(.fc_tys_kaisya_cd) & TrimNull(.fc_jigyousyo_cd)

                '調査会社情報をセットする
                Call Me.SetTyousaKaisyaInfo()

                '固定チャージをセットする
                Call Me.SetKoteiTyaaji()
                '=========2012/04/10 車龍 405738 追加↑================================

                '20100712　仕様変更　削除　馬艶軍↓↓↓
                ''請求先名
                'tbxSeikyuuSakiShriSakiMei.Text = TrimNull(.seikyuu_saki_mei)
                ''請求先カナ
                'tbxSeikyuuSakiShriSakiKana.Text = TrimNull(.seikyuu_saki_kana)
                ''請求書送付先郵便番号
                'tbxSkysySoufuYuubinNo.Text = TrimNull(.skysy_soufu_yuubin_no)
                ''請求書送付先住所１
                'tbxSkysySoufuJyuusyo1.Text = TrimNull(.skysy_soufu_jyuusyo1)
                ''請求書送付先電話番号
                'tbxSkysySoufuTelNo.Text = TrimNull(.skysy_soufu_tel_no)
                ''請求書送付先住所２
                'tbxSkysySoufuJyuusyo2.Text = TrimNull(.skysy_soufu_jyuusyo2)
                ''請求書送付先FAX番号
                'tbxShriYouFaxNo.Text = TrimNull(.skysy_soufu_fax_no)
                '20100712　仕様変更　削除　馬艶軍↑↑↑

                hidUPDTime.Value = TrimNull(.upd_datetime)
                UpdatePanelA.Update()
            End With
            '活性化
            If Not blnBtn Then
                btnSyuusei.Enabled = False
                btnTouroku.Enabled = False
                btnFcTenKousin.Enabled = False
            Else
                btnSyuusei.Enabled = True
                btnTouroku.Enabled = False
                btnFcTenKousin.Enabled = True
            End If
            tbxEigyousyoCd.Attributes.Add("readonly", "true;")
        Else
            MeisaiClear()
            strErr = "alert('" & Messages.Instance.MSG2034E & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strErr, True)
            If btn <> "btnSearch" Then
                btnSyuusei.Enabled = False
                btnTouroku.Enabled = True
                btnFcTenKousin.Enabled = False
                tbxEigyousyoCd.Attributes.Remove("readonly")
            End If
            tbxEigyousyo_Mei.Text = ""
        End If

        labSeikyuuSaki.Style.Add("display", "none")
        labHyouji.Style.Add("display", "none")
        hidConfirm2.Value = ""

        '20100712　仕様変更　削除　馬艶軍↓↓↓
        'If btn = "btnSearch" Or btn = "btnSyuusei" Or btn = "btnTouroku" Then
        '    tbxSeikyuuSakiShriSakiMei.Attributes.Remove("readonly")
        '    tbxSeikyuuSakiShriSakiKana.Attributes.Remove("readonly")
        '    tbxSkysySoufuYuubinNo.Attributes.Remove("readonly")
        '    tbxSkysySoufuJyuusyo1.Attributes.Remove("readonly")
        '    tbxSkysySoufuTelNo.Attributes.Remove("readonly")
        '    tbxSkysySoufuJyuusyo2.Attributes.Remove("readonly")
        '    tbxShriYouFaxNo.Attributes.Remove("readonly")
        '    btnKensakuSeikyuuSoufuCopy.Enabled = True
        '    btnKensakuSeikyuuSyo.Enabled = True
        'End If
        '20100712　仕様変更　削除　馬艶軍↑↑↑

        hidSyousai.Value = ""
    End Sub

    ''' <summary>
    ''' 明細項目クリア
    ''' </summary>
    ''' <remarks></remarks>
    Sub MeisaiClear()
        '取消
        chkTorikesi.Checked = False
        '営業所コード
        tbxEigyousyoCd.Text = ""
        '営業所名
        tbxEigyousyoMei.Text = ""
        '営業所名カナ
        tbxEigyousyoMeiKana.Text = ""
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
        '部署名
        tbxBusyoMei.Text = ""

        '請求先
        tbxSeikyuuSakiCd.Text = ""
        '請求先枝番
        tbxSeikyuuSakiBrc.Text = ""
        '請求先名
        tbxSeikyuuSakiMei.Text = ""

        '=========2012/04/10 車龍 405738 追加↓================================
        '集計FC用ｺｰﾄﾞ
        Me.tbxSyuukeiFcCd.Text = String.Empty
        'エリアコード
        Me.ddlArea.SelectedIndex = 0
        'ブロックコード
        Me.ddlBlock.SelectedIndex = 0
        'FC店区分
        Me.ddlFcTenKbn.SelectedIndex = 0
        'FC入会年月
        Me.tbxFcNyuukaiYM.Text = String.Empty
        'FC退会年月
        Me.tbxFcTaikaiYM.Text = String.Empty
        '(FC)調査会社コード
        Me.tbxTyousaKaisyaCd.Text = String.Empty

        '調査会社情報をクリアする
        Call Me.ClearTyousakaisya()

        '固定チャージ
        Me.btnKoteiTyaaji.Enabled = False
        Me.lblKoteiTyaaji.Text = String.Empty
        '=========2012/04/10 車龍 405738 追加↑================================

        '20100712　仕様変更　削除　馬艶軍↓↓↓
        ''請求先支払先名
        'tbxSeikyuuSakiShriSakiMei.Text = ""
        ''請求先支払先名カナ
        'tbxSeikyuuSakiShriSakiKana.Text = ""
        ''請求書送付先郵便番号
        'tbxSkysySoufuYuubinNo.Text = ""
        ''請求書送付先住所１
        'tbxSkysySoufuJyuusyo1.Text = ""
        ''請求書送付先電話番号
        'tbxSkysySoufuTelNo.Text = ""
        ''請求書送付先住所２
        'tbxSkysySoufuJyuusyo2.Text = ""
        ''支払用FAX番号
        'tbxShriYouFaxNo.Text = ""
        '20100712　仕様変更　削除　馬艶軍↑↑↑

        '営業所名印字有無
        SetDropSelect(ddlEigyousyoMeiInjiUmu, "")
        '請求先
        SetDropSelect(ddlSeikyuuSaki, "")

        'HIDDEN設定
        Me.hidSeikyuuSakiCd.Value = ""
        Me.hidSeikyuuSakiBrc.Value = ""
        Me.hidSeikyuuSakiKbn.Value = ""

        Me.hidConfirm.Value = ""

        '請求先新規登録
        labSeikyuuSaki.Style.Add("display", "none")
        labHyouji.Style.Add("display", "none")

        '20100712　仕様変更　削除　馬艶軍↓↓↓
        'tbxSeikyuuSakiShriSakiMei.Attributes.Remove("readonly")
        'tbxSeikyuuSakiShriSakiKana.Attributes.Remove("readonly")
        'tbxSkysySoufuYuubinNo.Attributes.Remove("readonly")
        'tbxSkysySoufuJyuusyo1.Attributes.Remove("readonly")
        'tbxSkysySoufuTelNo.Attributes.Remove("readonly")
        'tbxSkysySoufuJyuusyo2.Attributes.Remove("readonly")
        'tbxShriYouFaxNo.Attributes.Remove("readonly")
        'btnKensakuSeikyuuSoufuCopy.Enabled = True
        'btnKensakuSeikyuuSyo.Enabled = True
        '20100712　仕様変更　削除　馬艶軍↑↑↑

        hidUPDTime.Value = ""
        '権限
        If Not blnBtn Then
            btnSyuusei.Enabled = False
            btnTouroku.Enabled = False
            btnFcTenKousin.Enabled = False
        Else
            btnSyuusei.Enabled = False
            btnTouroku.Enabled = True
            btnFcTenKousin.Enabled = False
        End If

        tbxEigyousyoCd.Attributes.Remove("readonly")
        UpdatePanelA.Update()
    End Sub


    ''' <summary>Javascript作成</summary>
    Private Sub MakeScript()
        Dim csType As Type = Page.GetType()
        Dim csName As String = "GetScript"
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("<script language='javascript' type='text/javascript'>")

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

            'Disable
            .AppendLine("function fncDisable()")
            .AppendLine("{")
            .AppendLine("   var btnSearchEigyousyo = document.getElementById('" & Me.btnSearchEigyousyo.ClientID & "')")
            .AppendLine("   var btnSearch = document.getElementById('" & Me.btnSearch.ClientID & "')")
            .AppendLine("   var btnClear = document.getElementById('" & Me.btnClear.ClientID & "')")
            .AppendLine("   var btnSyuusei = document.getElementById('" & Me.btnSyuusei.ClientID & "')")
            .AppendLine("   var btnTouroku = document.getElementById('" & Me.btnTouroku.ClientID & "')")
            .AppendLine("   var btnClearMeisai = document.getElementById('" & Me.btnClearMeisai.ClientID & "')")
            .AppendLine("   var btnFcTenKousin = document.getElementById('" & Me.btnFcTenKousin.ClientID & "')")
            .AppendLine("   var btnKensakuYuubinNo = document.getElementById('" & Me.btnKensakuYuubinNo.ClientID & "')")
            .AppendLine("   var btnKensakuSeikyuuSaki = document.getElementById('" & Me.btnKensakuSeikyuuSaki.ClientID & "')")
            .AppendLine("   var btnKensakuSeikyuuSyousai = document.getElementById('" & Me.btnKensakuSeikyuuSyousai.ClientID & "')")
            '20100712　仕様変更　削除　馬艶軍↓↓↓
            '.AppendLine("   var btnKensakuSeikyuuSoufuCopy = document.getElementById('" & Me.btnKensakuSeikyuuSoufuCopy.ClientID & "')")
            '.AppendLine("   var btnKensakuSeikyuuSyo = document.getElementById('" & Me.btnKensakuSeikyuuSyo.ClientID & "')")
            '20100712　仕様変更　削除　馬艶軍↑↑↑
            .AppendLine("   var btnFC = document.getElementById('" & Me.btnFC.ClientID & "')")
            .AppendLine("   var btnOK = document.getElementById('" & Me.btnOK.ClientID & "')")
            .AppendLine("   var btnNO = document.getElementById('" & Me.btnNO.ClientID & "')")
            .AppendLine("   var my_array = new Array(12);")
            .AppendLine("   my_array[0] = btnSearchEigyousyo;")
            .AppendLine("   my_array[1] = btnSearch;")
            .AppendLine("   my_array[2] = btnClear;")
            .AppendLine("   my_array[3] = btnSyuusei;")
            .AppendLine("   my_array[4] = btnTouroku;")
            .AppendLine("   my_array[5] = btnClearMeisai;")
            .AppendLine("   my_array[6] = btnFcTenKousin;")
            .AppendLine("   my_array[7] = btnKensakuYuubinNo;")
            .AppendLine("   my_array[8] = btnKensakuSeikyuuSaki;")
            .AppendLine("   my_array[9] = btnKensakuSeikyuuSyousai;")
            '20100712　仕様変更　削除　馬艶軍↓↓↓
            '.AppendLine("   my_array[10] = btnKensakuSeikyuuSoufuCopy;")
            '.AppendLine("   my_array[11] = btnKensakuSeikyuuSyo;")
            '20100712　仕様変更　削除　馬艶軍↑↑↑
            .AppendLine("   my_array[10] = btnFC;")
            .AppendLine("   my_array[11] = btnOK;")
            .AppendLine("   my_array[12] = btnNO;")
            .AppendLine("   for (i = 0; i < 13; i++){")
            .AppendLine("       my_array[i].disabled = true;")
            .AppendLine("   }")
            .AppendLine("}")

            .AppendLine("function disableButton1()")
            .AppendLine("{")
            .AppendLine("   window.setTimeout('fncDisable()',0);")
            .AppendLine("   return true;")
            .AppendLine("}")

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

            '請求先検索ボタン押下時
            .AppendLine("function fncConfirm()")
            .AppendLine("{")
            .AppendLine("   var hidConfirm = document.getElementById('" & Me.hidConfirm.ClientID & "')")
            .AppendLine("   if(confirm('この営業所登録時に請求先マスタに登録しますか？')){")
            .AppendLine("       hidConfirm.value = 'OK';")
            .AppendLine("   }else{")
            .AppendLine("       hidConfirm.value = 'NO';")
            .AppendLine("   }")
            .AppendLine("   document.getElementById('" & Me.Form.Name & "').submit();")
            .AppendLine("}")

            'その他チェックCHK03
            .AppendLine("function fncCheck03()")
            .AppendLine("{")

            '請求先検索ボタン関連
            .AppendLine("   var tbxSeikyuuSakiCd = document.getElementById('" & Me.tbxSeikyuuSakiCd.ClientID & "')")
            .AppendLine("   var tbxSeikyuuSakiBrc = document.getElementById('" & Me.tbxSeikyuuSakiBrc.ClientID & "')")
            .AppendLine("   var ddlSeikyuuSaki = document.getElementById('" & Me.ddlSeikyuuSaki.ClientID & "')")
            .AppendLine("   var hidSeikyuuSakiCd = document.getElementById('" & Me.hidSeikyuuSakiCd.ClientID & "')")
            .AppendLine("   var hidSeikyuuSakiBrc = document.getElementById('" & Me.hidSeikyuuSakiBrc.ClientID & "')")
            .AppendLine("   var hidSeikyuuSakiKbn = document.getElementById('" & Me.hidSeikyuuSakiKbn.ClientID & "')")

            '請求先
            '.AppendLine("   var hidConfirm2 = document.getElementById('" & Me.hidConfirm2.ClientID & "')")
            '.AppendLine("   if((ddlSeikyuuSaki.value!='')||(tbxSeikyuuSakiCd.value!='')||(tbxSeikyuuSakiBrc.value!='')){")
            '.AppendLine("       if(hidConfirm2.value=='検索'){")
            '.AppendLine("           alert('" & Replace(Messages.Instance.MSG030E, "@PARAM1", "請求先") & "');")
            '.AppendLine("           ddlSeikyuuSaki.focus();")
            '.AppendLine("           return false;")
            '.AppendLine("       }")
            '.AppendLine("   }")

            .AppendLine("   if((ddlSeikyuuSaki.value != hidSeikyuuSakiKbn.value)&&((ddlSeikyuuSaki.value!='')||(tbxSeikyuuSakiCd.value!='')||(tbxSeikyuuSakiBrc.value!=''))){")
            .AppendLine("       alert('" & Replace(Messages.Instance.MSG030E, "@PARAM1", "請求先区分") & "');")
            .AppendLine("       ddlSeikyuuSaki.focus();")
            .AppendLine("       return false;")

            .AppendLine("   }else if((tbxSeikyuuSakiCd.value != hidSeikyuuSakiCd.value)&&((ddlSeikyuuSaki.value!='')||(tbxSeikyuuSakiCd.value!='')||(tbxSeikyuuSakiBrc.value!=''))){")
            .AppendLine("       alert('" & Replace(Messages.Instance.MSG030E, "@PARAM1", "請求先コード") & "');")
            .AppendLine("       tbxSeikyuuSakiCd.focus();")
            .AppendLine("       return false;")

            .AppendLine("   }else if((tbxSeikyuuSakiBrc.value != hidSeikyuuSakiBrc.value)&&((ddlSeikyuuSaki.value!='')||(tbxSeikyuuSakiCd.value!='')||(tbxSeikyuuSakiBrc.value!=''))){")
            .AppendLine("       alert('" & Replace(Messages.Instance.MSG030E, "@PARAM1", "請求先枝番") & "');")
            .AppendLine("       tbxSeikyuuSakiBrc.focus();")
            .AppendLine("       return false;")

            .AppendLine("   }else{")
            .AppendLine("       return true;")
            .AppendLine("   }")
            .AppendLine("}")

            '「請求先新規登録」文言表示共通処理
            .AppendLine("function fncHyouji()")
            .AppendLine("{")
            .AppendLine("   var hidConfirm = document.getElementById('" & Me.hidConfirm.ClientID & "')")
            .AppendLine("   var labSeikyuuSaki = document.getElementById('" & Me.labSeikyuuSaki.ClientID & "')")
            .AppendLine("   var labHyouji = document.getElementById('" & Me.labHyouji.ClientID & "')")
            .AppendLine("   var hidConfirm1 = document.getElementById('" & Me.hidConfirm1.ClientID & "')")
            .AppendLine("   labSeikyuuSaki.style.visibility = 'hidden';")
            .AppendLine("   labHyouji.style.visibility = 'hidden';")
            .AppendLine("   hidConfirm.value = 'Hyouji';")
            .AppendLine("   hidConfirm1.value = 'NO';")
            .AppendLine("}")

            '「請求先新規登録」文言表示共通処理
            'フォーカス
            .AppendLine("function fncFocus()")
            .AppendLine("{")
            .AppendLine("   var tbxEigyousyoCd = document.getElementById('" & Me.tbxEigyousyoCd.ClientID & "')")
            .AppendLine("   var ddlSeikyuuSaki = document.getElementById('" & Me.ddlSeikyuuSaki.ClientID & "')")
            .AppendLine("   var tbxSeikyuuSakiCd = document.getElementById('" & Me.tbxSeikyuuSakiCd.ClientID & "')")
            .AppendLine("   var tbxSeikyuuSakiBrc = document.getElementById('" & Me.tbxSeikyuuSakiBrc.ClientID & "')")

            .AppendLine("   var hidChange1 = document.getElementById('" & Me.hidChange1.ClientID & "')")
            .AppendLine("   var hidChange3 = document.getElementById('" & Me.hidChange3.ClientID & "')")
            .AppendLine("   var hidChange4 = document.getElementById('" & Me.hidChange4.ClientID & "')")
            .AppendLine("   var hidChange5 = document.getElementById('" & Me.hidChange5.ClientID & "')")

            .AppendLine("   hidChange1.value = tbxEigyousyoCd.value;")
            .AppendLine("   hidChange3.value = ddlSeikyuuSaki.value;")
            .AppendLine("   hidChange4.value = tbxSeikyuuSakiCd.value;")
            .AppendLine("   hidChange5.value = tbxSeikyuuSakiBrc.value;")
            .AppendLine("}")
            'ロストフォーカス
            .AppendLine("function fncblur()")
            .AppendLine("{")
            .AppendLine("   var tbxEigyousyoCd = document.getElementById('" & Me.tbxEigyousyoCd.ClientID & "')")
            .AppendLine("   var ddlSeikyuuSaki = document.getElementById('" & Me.ddlSeikyuuSaki.ClientID & "')")
            .AppendLine("   var tbxSeikyuuSakiCd = document.getElementById('" & Me.tbxSeikyuuSakiCd.ClientID & "')")
            .AppendLine("   var tbxSeikyuuSakiBrc = document.getElementById('" & Me.tbxSeikyuuSakiBrc.ClientID & "')")

            .AppendLine("   var hidChange1 = document.getElementById('" & Me.hidChange1.ClientID & "')")
            .AppendLine("   var hidChange3 = document.getElementById('" & Me.hidChange3.ClientID & "')")
            .AppendLine("   var hidChange4 = document.getElementById('" & Me.hidChange4.ClientID & "')")
            .AppendLine("   var hidChange5 = document.getElementById('" & Me.hidChange5.ClientID & "')")

            .AppendLine("   var hidConfirm1 = document.getElementById('" & Me.hidConfirm1.ClientID & "')")
            .AppendLine("   var hidConfirm2 = document.getElementById('" & Me.hidConfirm2.ClientID & "')")

            .AppendLine("   if(hidConfirm1.value=='OK'){")
            .AppendLine("       if((tbxEigyousyoCd.value != hidChange1.value)||(ddlSeikyuuSaki.value != hidChange3.value)||(tbxSeikyuuSakiCd.value != hidChange4.value)||(tbxSeikyuuSakiBrc.value != hidChange5.value)){")
            .AppendLine("           fncHyouji();")
            .AppendLine("           hidConfirm2.value='検索';")
            .AppendLine("       }")
            .AppendLine("   }else{")
            .AppendLine("   }")
            .AppendLine("}")

            '請求先検索ボタン押下時
            .AppendLine("function fncKousin(obj)")
            .AppendLine("{")
            .AppendLine("   var hidKousin = document.getElementById('" & Me.hidKousin.ClientID & "')")
            .AppendLine("   if(confirm(obj + 'の住所自動更新処理を行います。宜しいですか？')){")
            .AppendLine("       hidKousin.value = 'OK';")
            .AppendLine("   }else{")
            .AppendLine("       hidKousin.value = 'NO';")
            .AppendLine("   }")
            .AppendLine("   document.getElementById('" & Me.Form.Name & "').submit();")
            .AppendLine("}")
            '=========2012/04/10 車龍 405738 追加↓================================
            '調査会社 住所情報コピーボタン押下時
            .AppendLine("function fncCopyTyousaKaisya()")
            .AppendLine("{")
            .AppendLine("   document.getElementById('" & Me.tbxYuubinNo.ClientID & "').value = document.getElementById('" & Me.tbxTyousakaisyaYuubinNo.ClientID & "').value;")
            .AppendLine("   document.getElementById('" & Me.tbxJyuusyo1.ClientID & "').value = document.getElementById('" & Me.tbxTyousakaisyaJyuusyo1.ClientID & "').value;")
            .AppendLine("   document.getElementById('" & Me.tbxJyuusyo2.ClientID & "').value = document.getElementById('" & Me.tbxTyousakaisyaJyuusyo2.ClientID & "').value;")
            .AppendLine("   document.getElementById('" & Me.tbxTelNo.ClientID & "').value = document.getElementById('" & Me.tbxTyousakaisyaTelNo.ClientID & "').value;")
            .AppendLine("   document.getElementById('" & Me.tbxFaxNo.ClientID & "').value = document.getElementById('" & Me.tbxTyousakaisyaFaxNo.ClientID & "').value;")
            .AppendLine("}")
            '日付チェック(yyyy/mm)
            .AppendLine("function fncCheckNengetu(obj,objId)")
            .AppendLine("{ ")
            .AppendLine("	if (obj.value==''){return true;}")
            .AppendLine("	var checkFlg = true;")
            .AppendLine("	obj.value = obj.value.Trim();")
            .AppendLine("	var val = obj.value;")
            .AppendLine("	val = SetDateNoSign(val,'/');")
            .AppendLine("	val = SetDateNoSign(val,'-');")
            .AppendLine("	val = val+'01';")
            .AppendLine("	if(val == '')return;")
            .AppendLine("	val = removeSlash(val);")
            .AppendLine("	val = val.replace(/\-/g, '');")
            .AppendLine("	if(val.length == 6){")
            .AppendLine("		if(val.substring(0, 2) > 70){")
            .AppendLine("			val = '19' + val;")
            .AppendLine("		}else{")
            .AppendLine("			val = '20' + val;")
            .AppendLine("		}")
            .AppendLine("	}else if(val.length == 4){")
            .AppendLine("		dd = new Date();")
            .AppendLine("		year = dd.getFullYear();")
            .AppendLine("		val = year + val;")
            .AppendLine("	}")
            .AppendLine("	if(val.length != 8){")
            .AppendLine("		checkFlg = false;")
            .AppendLine("	}else{  //8桁の場合")
            .AppendLine("		val = addSlash(val);")
            .AppendLine("		var arrD = val.split('/');")
            .AppendLine("		if(checkDateVali(arrD[0],arrD[1],arrD[2]) == false){")
            .AppendLine("			checkFlg = false; ")
            .AppendLine("		}")
            .AppendLine("	}")
            .AppendLine("	if(!checkFlg){")
            .AppendLine("		event.returnValue = false;")
            .AppendLine("        if (objId == 'ＦＣ 入会年月'){")
            .AppendLine("            alert('" & Replace(Messages.Instance.MSG014E, "@PARAM1", "ＦＣ 入会年月").ToString & "');")
            .AppendLine("        }else if(objId == 'ＦＣ 退会年月'){")
            .AppendLine("            alert('" & Replace(Messages.Instance.MSG014E, "@PARAM1", "ＦＣ 退会年月").ToString & "');")
            .AppendLine("        }")
            .AppendLine("        obj.focus();")
            .AppendLine("		obj.select();")
            .AppendLine("		return false;")
            .AppendLine("	}else{")
            .AppendLine("		obj.value = val.substring(0,7);")
            .AppendLine("	}")
            .AppendLine("}")
            '=========2012/04/10 車龍 405738 追加↑================================
            .AppendLine("</script>")
        End With
        ClientScript.RegisterClientScriptBlock(csType, csName, csScript.ToString)
    End Sub

    ''' <summary>
    ''' DDLの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Sub SetDdlListInf()
        '※ドロップダウンリスト設定↓
        Dim ddlist As ListItem

        '営業所名印字有無ddlEigyousyoMeiInjiUmu
        ddlist = New ListItem
        ddlist.Text = ""
        ddlist.Value = ""
        ddlEigyousyoMeiInjiUmu.Items.Add(ddlist)
        ddlist = New ListItem
        ddlist.Text = "無し"
        ddlist.Value = "0"
        ddlEigyousyoMeiInjiUmu.Items.Add(ddlist)
        ddlist = New ListItem
        ddlist.Text = "有り"
        ddlist.Value = "1"
        ddlEigyousyoMeiInjiUmu.Items.Add(ddlist)

        '請求先区分ddlSeikyuuSaki
        'SetKakutyou(ddlSeikyuuSaki, "1")
        ddlist = New ListItem
        ddlist.Text = ""
        ddlist.Value = ""
        ddlSeikyuuSaki.Items.Add(ddlist)
        ddlist = New ListItem
        ddlist.Text = "調査会社"
        ddlist.Value = "1"
        ddlSeikyuuSaki.Items.Add(ddlist)

        '=========2012/04/10 車龍 405738 追加↓================================
        Dim dtDDL As New Data.DataTable

        'エリア
        Me.ddlArea.Items.Clear()
        dtDDL = EigyousyoMasterBL.GetDdlList(60)
        Me.ddlArea.DataValueField = "code"
        Me.ddlArea.DataTextField = "meisyou"
        Me.ddlArea.DataSource = dtDDL
        Me.ddlArea.DataBind()
        '先頭行
        Me.ddlArea.Items.Insert(0, New ListItem(String.Empty, String.Empty))

        'ブロック
        Me.ddlBlock.Items.Clear()
        dtDDL = EigyousyoMasterBL.GetDdlList(61)
        Me.ddlBlock.DataValueField = "code"
        Me.ddlBlock.DataTextField = "meisyou"
        Me.ddlBlock.DataSource = dtDDL
        Me.ddlBlock.DataBind()
        '先頭行
        Me.ddlBlock.Items.Insert(0, New ListItem(String.Empty, String.Empty))

        'FC店区分
        Me.ddlFcTenKbn.Items.Clear()
        Me.ddlFcTenKbn.Items.Insert(0, New ListItem("未加入", "0"))
        Me.ddlFcTenKbn.Items.Insert(1, New ListItem("加入", "1"))
        Me.ddlFcTenKbn.Items.Insert(2, New ListItem("退会", "3"))

        '=========2012/04/10 車龍 405738 追加↑================================

    End Sub

    '20100712　仕様変更　削除　馬艶軍↓↓↓
    '''' <summary>
    '''' 請求先区分ドロップダウンリスト設定
    '''' </summary>
    '''' <param name="ddl">ドロップダウンリスト</param>
    '''' <param name="strSyubetu">名称種別</param>
    '''' <remarks></remarks>
    'Sub SetKakutyou(ByVal ddl As DropDownList, ByVal strSyubetu As String)
    '    Dim dtTable As New DataTable
    '    Dim intCount As Integer = 0
    '    dtTable = EigyousyoMasterBL.SelKakutyouInfo(strSyubetu)

    '    Dim ddlist As New ListItem
    '    ddlist.Text = ""
    '    ddlist.Value = ""
    '    ddl.Items.Add(ddlist)
    '    For intCount = 0 To dtTable.Rows.Count - 1
    '        ddlist = New ListItem
    '        ddlist.Text = TrimNull(dtTable.Rows(intCount).Item("code")) & "：" & TrimNull(dtTable.Rows(intCount).Item("meisyou"))
    '        ddlist.Value = TrimNull(dtTable.Rows(intCount).Item("code"))
    '        ddl.Items.Add(ddlist)
    '    Next
    'End Sub
    '20100712　仕様変更　削除　馬艶軍↑↑↑

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
    ''' 日付型変更処理
    ''' </summary>
    ''' <param name="ymd">年月</param>
    ''' <returns></returns>
    ''' <remarks>2010/01/12 馬艶軍（大連） 新規作成</remarks>
    Public Function toYYYYMM(ByVal ymd As Object) As Object

        If ymd.Equals(String.Empty) Or ymd.Equals(DBNull.Value) Then
            Return ymd
        Else
            Return CDate(ymd).ToString("yyyy/MM")
        End If

    End Function

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
    ''' 詳細ボタン押下時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnKensakuSeikyuuSyousai_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKensakuSeikyuuSyousai.Click
        Dim strScript As String = String.Empty
        Dim tmpScript As String = String.Empty
        '請求先区分<>""(空白) and 請求先コード<>""(空白) and 請求先枝番 <> ""(空白) の場合
        If Me.ddlSeikyuuSaki.SelectedValue <> "" And Me.tbxSeikyuuSakiCd.Text <> "" And Me.tbxSeikyuuSakiBrc.Text <> "" Then
            Dim dtTable As New Data.DataTable
            dtTable = EigyousyoMasterBL.SelSeikyuuSaki(tbxSeikyuuSakiCd.Text, tbxSeikyuuSakiBrc.Text, ddlSeikyuuSaki.SelectedValue, False)
            If dtTable.Rows.Count > 0 Then
                tmpScript = "callSearch('" & Me.tbxSeikyuuSakiCd.ClientID & SEP_STRING & _
                                            Me.tbxSeikyuuSakiBrc.ClientID & SEP_STRING & _
                                            Me.ddlSeikyuuSaki.ClientID & "','" & _
                                            SEARCH_SEIKYUU_SAKI & "','" _
                                       & Me.btnKensakuSeikyuuSyousai.ClientID & "');"

                ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)
            Else
                'メッセージ表示
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('該当するデータが存在しません。');", True)
            End If
        Else
            'メッセージ表示
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('請求先情報が設定されていません。\r\n請求先を入力して下さい。');", True)
        End If
    End Sub

    ''' <summary>
    ''' FC店更新処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnFcTenKousin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFcTenKousin.Click

        ''メッセージ表示
        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "fncKousin('" & Me.tbxEigyousyoMei.Text & "');", True)
        'メッセージ表示
        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "FC_Kousin", "if(confirm('" & Me.tbxEigyousyoMei.Text & "の住所自動更新処理を行います。宜しいですか？')){window.setTimeout('objEBI(\'" & Me.btnFC.ClientID & "\').click()',10);}; ", True)

    End Sub

    ''' <summary>
    ''' FC店更新処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnFC_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFC.Click

        Me.hidKousin.Value = ""

        Dim dtKameiten As New Data.DataTable
        '加盟店マスタを取得する
        dtKameiten = EigyousyoMasterBL.SelKameiten(Me.tbxEigyousyoCd.Text)
        If dtKameiten.Rows.Count > 0 Then
            '更新追加処理
            If EigyousyoMasterBL.SetKousinTuika(Me.tbxEigyousyoCd.Text, ViewState("UserId")) Then
                'メッセージ表示
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('FC店住所自動更新処理が完了しました。');", True)
            Else
                'メッセージ表示
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('FC店住所自動更新処理が失敗しました。');", True)
            End If
        Else
            'メッセージ表示
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('" & Me.tbxEigyousyoMei.Text & "のA区分の加盟店はありません。');", True)
        End If

    End Sub

    ''' <summary>
    ''' 請求先検索ボタンを押下、OKを選択時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Me.hidConfirm.Value = ""
        '該当するデータが存在する場合
        Dim dtSeikyuuSakiTouroku As New DataTable
        dtSeikyuuSakiTouroku = EigyousyoMasterBL.SelSeikyuuSakiTouroku(tbxSeikyuuSakiBrc.Text)
        If dtSeikyuuSakiTouroku.Rows.Count > 0 Then
            labSeikyuuSaki.Style.Add("display", "block")
            labHyouji.Style.Add("display", "block")
            labHyouji.Text = dtSeikyuuSakiTouroku.Rows(0).Item("hyouji_naiyou").ToString
        Else
            labSeikyuuSaki.Style.Add("display", "block")
        End If

        Me.hidConfirm1.Value = "OK"
        Me.hidConfirm2.Value = ""

        Me.tbxSeikyuuSakiMei.Text = ""

        hidSeikyuuSakiCd.Value = tbxSeikyuuSakiCd.Text
        hidSeikyuuSakiBrc.Value = tbxSeikyuuSakiBrc.Text
        hidSeikyuuSakiKbn.Value = ddlSeikyuuSaki.SelectedValue
    End Sub

    ''' <summary>
    ''' 請求先検索ボタンを押下、NOを選択時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnNO_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNO.Click
        Dim strScript As String = ""

        labSeikyuuSaki.Style.Add("display", "none")
        labHyouji.Style.Add("display", "none")
        Me.hidConfirm1.Value = "NO"
        Me.hidConfirm2.Value = "検索"

        '請求先名
        tbxSeikyuuSakiMei.Text = ""
        Me.hidConfirm.Value = ""
        strScript = "objSrchWin = window.open('search_Seikyuusaki.aspx?blnDelete=True&Kbn='+escape('請求先')+'&FormName=" & _
                                     Me.Page.Form.Name & "&objKbn=" & _
                                     ddlSeikyuuSaki.ClientID & _
                                     "&objHidKbn=" & hidSeikyuuSakiKbn.ClientID & _
                                     "&objCd=" & tbxSeikyuuSakiCd.ClientID & _
                                     "&objHidCd=" & hidSeikyuuSakiCd.ClientID & _
                                     "&hidConfirm2=" & hidConfirm2.ClientID & _
                                     "&objBrc=" & tbxSeikyuuSakiBrc.ClientID & _
                                     "&objHidBrc=" & hidSeikyuuSakiBrc.ClientID & _
                                     "&objMei=" & tbxSeikyuuSakiMei.ClientID & _
                                     "&strKbn='+escape(eval('document.all.'+'" & _
                                     ddlSeikyuuSaki.ClientID & "').value)+'&strCd='+escape(eval('document.all.'+'" & _
                                     tbxSeikyuuSakiCd.ClientID & "').value)+'&strBrc='+escape(eval('document.all.'+'" & _
                                     tbxSeikyuuSakiBrc.ClientID & "').value), 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');"
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strScript, True)
    End Sub

    ''' <summary>
    ''' 戻るボタンの処理
    ''' </summary>
    ''' <param name="sender">system.Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Server.Transfer("MasterMainteMenu.aspx")
    End Sub

    ''' <summary>
    ''' CSV出力
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>2012/04/10 405738 車龍</history>
    Private Sub btnCsvOutput_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCsvOutput.Click

        'CSVデータを取得する
        Dim dtCsv As New Data.DataTable
        dtCsv = EigyousyoMasterBL.GetEigyousyoCsv()

        'システム日付を取得する
        Dim strSystemDate As String
        strSystemDate = EigyousyoMasterBL.GetSystemDateYMD().Rows(0).Item("system_date")

        'CSVファイル名設定
        Dim strFileName As String
        strFileName = strSystemDate & "_Fc_Kameiten_Ichiran.csv"

        Response.Buffer = True
        Dim writer As New CsvWriter(Me.Response.OutputStream, Encoding.GetEncoding(932), ",", vbCrLf)

        'CSVファイルヘッダ設定
        writer.WriteLine(EarthConst.conEigyousyoCsvHeader)

        'CSVファイル内容設定
        For i As Integer = 0 To dtCsv.Rows.Count - 1
            With dtCsv.Rows(i)
                writer.WriteLine(.Item(0), .Item(1), .Item(2), .Item(3), .Item(4), .Item(5), .Item(6), .Item(7), .Item(8), .Item(9), _
                                 .Item(10), .Item(11), .Item(12), .Item(13), .Item(14), .Item(15), .Item(16), .Item(17), .Item(18), .Item(19), _
                                 .Item(20), .Item(21), .Item(22), .Item(23))
            End With
        Next


        'CSVファイルダウンロード
        Response.Charset = "utf-8"
        Response.ContentType = "text/plain"
        Response.AddHeader("Content-Disposition", "attachment; filename=" & HttpUtility.UrlEncode(strFileName))
        Response.End()

    End Sub

    ''' <summary>
    ''' 調査会社 選択ボタン押下時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>2012/04/10 405738 車龍</history>
    Private Sub btnTyousaKaisyaCd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTyousaKaisyaCd.Click

        '調査会社コード
        Dim strTyousaKaisyaCd As String
        strTyousaKaisyaCd = Me.tbxTyousaKaisyaCd.Text.Trim

        '調査会社情報をクリアする
        Call Me.ClearTyousakaisya()

        If Not strTyousaKaisyaCd.Equals(String.Empty) Then
            '調査会社コードが入力の場合

            If EigyousyoMasterBL.GetTyousaKaisyaCount(strTyousaKaisyaCd) = 1 Then
                '調査会社情報をセットする
                Call Me.SetTyousaKaisyaInfo()
            Else
                '調査会社検索画面を表示する
                Call Me.ShowTyousakaisyaKensaku()
            End If

        Else
            '調査会社コードが未入力の場合

            '調査会社検索画面を表示する
            Call Me.ShowTyousakaisyaKensaku()
        End If

    End Sub

    ''' <summary>
    ''' 調査会社検索画面を表示する
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2012/04/10 405738 車龍</history>
    Private Sub ShowTyousakaisyaKensaku()
        Dim strScript As String = ""

        strScript = "objSrchWin = window.open('search_tyousa.aspx?Kbn='+escape('調査')+'&soukoCd='+escape('#')+'&FormName=" & Me.Page.Form.Name & _
                "&objCd=" & Me.tbxTyousaKaisyaCd.ClientID & _
                "&objMei=" & Me.tbxTyousaKaisyaMei.ClientID & _
                "&strCd='+escape(eval('document.all.'+'" & Me.tbxTyousaKaisyaCd.ClientID & "').value)+" & _
                "'&strMei='+escape(eval('document.all.'+'" & Me.tbxTyousaKaisyaMei.ClientID & "').value)+" & _
                "'&btnSelectId=" & Me.btnTyousakaisya.ClientID & "', " & _
                "'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');"

        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ShowTyousakaisyaKensaku", strScript, True)

    End Sub

    ''' <summary>
    ''' 調査会社情報をクリアする
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2012/04/10 405738 車龍</history>
    Private Sub ClearTyousakaisya()

        '調査会社名
        Me.tbxTyousaKaisyaMei.Text = String.Empty
        '調査会社名カナ
        Me.tbxTyousaKaisyaMeiKana.Text = String.Empty
        '(調査会社)代表者名
        Me.tbxDaihyousyaMei.Text = String.Empty
        '(調査会社)役職名
        Me.tbxYasyokuMei.Text = String.Empty
        '(調査会社)郵便番号
        Me.tbxTyousakaisyaYuubinNo.Text = String.Empty
        '(調査会社)住所1
        Me.tbxTyousakaisyaJyuusyo1.Text = String.Empty
        '(調査会社)電話番号
        Me.tbxTyousakaisyaTelNo.Text = String.Empty
        '(調査会社)住所2
        Me.tbxTyousakaisyaJyuusyo2.Text = String.Empty
        '(調査会社)FAX番号
        Me.tbxTyousakaisyaFaxNo.Text = String.Empty
        '(調査会社)JAPAN会区分
        Me.tbxJapanKaiKbn.Text = String.Empty
        '(調査会社)JAPAN会入会年月
        Me.tbxJapanKaiNyuukaiYM.Text = String.Empty
        '(調査会社)JAPAN会退会年月
        Me.tbxJapanKaiTaikaiYM.Text = String.Empty
        '(調査会社)宅地地盤調査主任資格有無フラグ
        Me.tbxTyousaSyuninSikaku.Text = String.Empty
        '(調査会社)ReportJHSトークン有無フラグ
        Me.tbxReportJHS.Text = String.Empty

    End Sub

    ''' <summary>
    ''' 調査会社情報を取得する
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>2012/04/10 405738 車龍</history>
    Private Sub btnTyousakaisya_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTyousakaisya.Click

        '調査会社情報をセットする
        Call Me.SetTyousaKaisyaInfo()

    End Sub

    ''' <summary>
    ''' 調査会社情報をセットする
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2012/04/10 405738 車龍</history>
    Private Sub SetTyousaKaisyaInfo()

        '調査会社コード
        Dim strTyousaKaisyaCd As String = Me.tbxTyousaKaisyaCd.Text.Trim()

        If Not strTyousaKaisyaCd.Equals(String.Empty) Then

            '調査会社情報を取得する
            Dim dtTyousaKaisyaInfo As New Data.DataTable
            dtTyousaKaisyaInfo = EigyousyoMasterBL.GetTyousaKaisyaInfo(strTyousaKaisyaCd)

            If dtTyousaKaisyaInfo.Rows.Count > 0 Then
                With dtTyousaKaisyaInfo.Rows(0)
                    '調査会社コード
                    Me.tbxTyousaKaisyaCd.Text = .Item("tys_kaisya_cd").ToString.Trim() & .Item("jigyousyo_cd").ToString.Trim()
                    '調査会社名
                    Me.tbxTyousaKaisyaMei.Text = .Item("tys_kaisya_mei").ToString.Trim()
                    '調査会社名カナ
                    Me.tbxTyousaKaisyaMeiKana.Text = .Item("tys_kaisya_mei_kana").ToString.Trim()
                    '(調査会社)代表者名
                    Me.tbxDaihyousyaMei.Text = .Item("daihyousya_mei").ToString.Trim()
                    '(調査会社)役職名
                    Me.tbxYasyokuMei.Text = .Item("yakusyoku_mei").ToString.Trim()
                    '(調査会社)郵便番号
                    Me.tbxTyousakaisyaYuubinNo.Text = .Item("yuubin_no").ToString.Trim()
                    '(調査会社)住所1
                    Me.tbxTyousakaisyaJyuusyo1.Text = .Item("jyuusyo1").ToString.Trim()
                    '(調査会社)電話番号
                    Me.tbxTyousakaisyaTelNo.Text = .Item("tel_no").ToString.Trim()
                    '(調査会社)住所2
                    Me.tbxTyousakaisyaJyuusyo2.Text = .Item("jyuusyo2").ToString.Trim()
                    '(調査会社)FAX番号
                    Me.tbxTyousakaisyaFaxNo.Text = .Item("fax_no").ToString.Trim()
                    '(調査会社)JAPAN会区分
                    Select Case .Item("japan_kai_kbn").ToString.Trim()
                        Case "0"
                            Me.tbxJapanKaiKbn.Text = "未加入"
                        Case "1"
                            Me.tbxJapanKaiKbn.Text = "加入"
                        Case "3"
                            Me.tbxJapanKaiKbn.Text = "退会"
                        Case Else
                            Me.tbxJapanKaiKbn.Text = "未加入"
                    End Select

                    '(調査会社)JAPAN会入会年月
                    Me.tbxJapanKaiNyuukaiYM.Text = .Item("japan_kai_nyuukai_date").ToString.Trim()
                    '(調査会社)JAPAN会退会年月
                    Me.tbxJapanKaiTaikaiYM.Text = .Item("japan_kai_taikai_date").ToString.Trim()
                    '(調査会社)宅地地盤調査主任資格有無フラグ
                    Select Case .Item("tkt_jbn_tys_syunin_skk_flg").ToString.Trim()
                        Case "0"
                            Me.tbxTyousaSyuninSikaku.Text = "無し"
                        Case "1"
                            Me.tbxTyousaSyuninSikaku.Text = "有り"
                        Case Else
                            Me.tbxTyousaSyuninSikaku.Text = "無し"
                    End Select

                    '(調査会社)ReportJHSトークン有無フラグ
                    Select Case .Item("report_jhs_token_flg").ToString.Trim()
                        Case "0"
                            Me.tbxReportJHS.Text = "無し"
                        Case "1"
                            Me.tbxReportJHS.Text = "有り"
                        Case Else
                            Me.tbxReportJHS.Text = "無し"
                    End Select
                End With
            Else
                '調査会社情報をクリアする
                Call Me.ClearTyousakaisya()
            End If
        Else
            '調査会社情報をクリアする
            Call Me.ClearTyousakaisya()
        End If

    End Sub

    ''' <summary>
    ''' 固定チャージをセットする
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2012/04/10 405738 車龍</history>
    Private Sub SetKoteiTyaaji()

        '営業所コード
        Dim strEigyousyoCd As String = Me.tbxEigyousyoCd.Text.Trim

        If Left(strEigyousyoCd, 2).Equals("AF") AndAlso Me.ddlFcTenKbn.SelectedValue.Trim.Equals("1") Then
            '入力日を取得する
            Dim strNyuuryokuDate As String
            strNyuuryokuDate = EigyousyoMasterBL.GetKoteiTyaaji(strEigyousyoCd, True).Rows(0).Item("nyuuryoku_date").ToString.Trim

            If Not strNyuuryokuDate.Equals(String.Empty) Then
                '本月で請求済み
                Me.lblKoteiTyaaji.Text = Left(strNyuuryokuDate, 4) & "年" & Right(strNyuuryokuDate, 2) & "月　請求済み"
                Me.lblKoteiTyaaji.ForeColor = Drawing.Color.Red

                '「固定チャージ」
                Me.btnKoteiTyaaji.Enabled = False

            Else
                strNyuuryokuDate = EigyousyoMasterBL.GetKoteiTyaaji(strEigyousyoCd, False).Rows(0).Item("nyuuryoku_date").ToString.Trim
                If Not strNyuuryokuDate.Equals(String.Empty) Then
                    '本月で未請求
                    Me.lblKoteiTyaaji.Text = Left(strNyuuryokuDate, 4) & "年" & Right(strNyuuryokuDate, 2) & "月　請求済み"
                    Me.lblKoteiTyaaji.ForeColor = Drawing.Color.Blue
                Else
                    '過去に登録がない場合
                    Me.lblKoteiTyaaji.Text = "固定チャージ　請求履歴なし"
                    Me.lblKoteiTyaaji.ForeColor = Drawing.Color.Black
                End If

                '請求先存在チェック
                If EigyousyoMasterBL.SelSeikyuusakiCheck(strEigyousyoCd) Then
                    '「固定チャージ」
                    Me.btnKoteiTyaaji.Enabled = True
                Else
                    '「固定チャージ」
                    Me.btnKoteiTyaaji.Enabled = False
                End If

            End If
        Else
            '固定チャージ
            Me.btnKoteiTyaaji.Enabled = False
            Me.lblKoteiTyaaji.Text = String.Empty
        End If

    End Sub

    ''' <summary>
    ''' 固定チャージボタン押下時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>2012/04/10 405738 車龍</history>
    Private Sub btnKoteiTyaaji_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKoteiTyaaji.Click

        '営業所コード
        Dim strEigyousyoCd As String
        strEigyousyoCd = Me.tbxEigyousyoCd.Text.Trim

        'メッセージ
        Dim strMessage As String
        strMessage = EigyousyoMasterBL.SetKoteiTyaaji(strEigyousyoCd, CStr(ViewState("UserId")))

        '固定チャージ情報をセットする
        If strMessage.Trim.Equals(String.Empty) Then
            '成功の場合

            'メッセージ表示
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('" & String.Format(Messages.Instance.MSG2069E, "固定チャージ") & "');", True)

            '固定チャージをセットする
            Call Me.SetKoteiTyaaji()
        Else
            '失敗の場合

            'メッセージ表示
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('" & strMessage & "');", True)
        End If

    End Sub

End Class
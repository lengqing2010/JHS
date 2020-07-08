Imports Itis.Earth.BizLogic
Imports System.Data
''' <summary>
''' 調査会社マスタ
''' </summary>
''' <history>
''' <para>2010/05/15　馬艶軍(大連)　新規作成</para>
''' </history>
Partial Public Class TyousaKaisyaMaster
    Inherits System.Web.UI.Page
    'ボタン
    Private blnBtn As Boolean
    Private blnBtn1 As Boolean
    Private blnBtn2 As Boolean
    'インスタンス生成
    Private CommonSearchLogic As New Itis.Earth.BizLogic.CommonSearchLogic
    '共通チェック
    Private commoncheck As New CommonCheck
    'インスタンス生成
    Private TyousaKaisyaMasterBL As New Itis.Earth.BizLogic.TyousaKaisyaMasterLogic

    Private Const SEP_STRING As String = "$$$"
    '
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
        blnBtn = commonChk.CommonNinnsyou(strUserID, "irai_gyoumu_kengen,keiri_gyoumu_kengen")
        blnBtn1 = commonChk.CommonNinnsyou(strUserID, "irai_gyoumu_kengen")
        blnBtn2 = commonChk.CommonNinnsyou(strUserID, "keiri_gyoumu_kengen")
        ViewState("UserId") = strUserID
        '権限チェック↑
        If Not IsPostBack Then
            'DDLの初期設定
            SetDdlListInf()
            '新会計事業所コード
            Me.tbxSkkJigyousyoCd.Text = "YMP8"
            Me.hidSkkJigyousyoCd.Value = "YMP8"

            '修正ボタン
            btnSyuusei.Enabled = False
            '登録ボタン
            btnTouroku.Enabled = True
            '取消理由
            tbxTorikesiRiyuu.Attributes.Add("readonly", "true;")
            '請求先新規登録
            labSeikyuuSaki.Style.Add("display", "none")
            labHyouji.Style.Add("display", "none")

        Else
            '取消と取消理由の状態
            If Me.hidTorikesi.Value = "true" Then
                Me.chkTorikesi.Checked = True
                tbxTorikesiRiyuu.Attributes.Remove("readonly")
            Else
                Me.chkTorikesi.Checked = False
                tbxTorikesiRiyuu.Attributes.Add("readonly", "true;")
            End If

            '請求先検索ボタン押下時
            If Me.hidConfirm.Value = "Hyouji" Then
                labSeikyuuSaki.Style.Add("display", "none")
                labHyouji.Style.Add("display", "none")

                Me.hidConfirm1.Value = "NO"

            End If
        End If

        '取消チェックボックス
        Me.chkTorikesi.Attributes.Add("onClick", "fncSetTorikesiVal();")
        '郵便番号
        Me.tbxYuubinNo.Attributes.Add("onblur", "SetYuubinNo(this)")
        'SS基準価格
        Me.tbxSSKijyunKkk.Attributes.Add("onblur", "checkNumberAddFig(this);")
        Me.tbxSSKijyunKkk.Attributes.Add("onfocus", "removeFig(this);")
        '==============2012/04/12 車龍 405738 削除↓====================
        ''ＦＣ 入会年月
        'Me.tbxFCNyuukaiDate.Attributes.Add("onBlur", "fncCheckNengetu(this,'ＦＣ 入会年月');")
        ''ＦＣ 退会年月
        'Me.tbxFCTaikaiDate.Attributes.Add("onBlur", "fncCheckNengetu(this,'ＦＣ 退会年月');")
        '==============2012/04/12 車龍 405738 削除↑====================
        'ＪＡＰＡＮ会 入会年月
        Me.tbxJapanKaiNyuukaiDate.Attributes.Add("onBlur", "fncCheckNengetu(this,'ＪＡＰＡＮ会 入会年月');")
        'ＪＡＰＡＮ会 退会年月
        Me.tbxJapanKaiTaikaiDate.Attributes.Add("onBlur", "fncCheckNengetu(this,'ＪＡＰＡＮ会 退会年月');")
        '請求書送付先郵便番号
        Me.tbxSkysySoufuYuubinNo.Attributes.Add("onblur", "SetYuubinNo(this)")
        'ファクタリング開始年月
        Me.tbxFctringKaisiNengetu.Attributes.Add("onBlur", "fncCheckNengetu(this,'ファクタリング開始年月');")

        '調査会社コード
        Me.tbxTyousaKaisyaCd.Attributes.Add("onfocus", "fncFocus('" & Me.tbxTyousaKaisyaCd.ClientID & "')")
        Me.tbxTyousaKaisyaCd.Attributes.Add("onblur", "fncblur('1','" & Me.tbxTyousaKaisyaCd.ClientID & "')")
        '事業所コード
        Me.tbxJigyousyoCd.Attributes.Add("onfocus", "fncFocus('" & Me.tbxJigyousyoCd.ClientID & "')")
        Me.tbxJigyousyoCd.Attributes.Add("onblur", "fncblur('','" & Me.tbxJigyousyoCd.ClientID & "')")
        '請求先区分
        Me.ddlSeikyuuSaki.Attributes.Add("onfocus", "fncFocus('" & Me.ddlSeikyuuSaki.ClientID & "')")
        Me.ddlSeikyuuSaki.Attributes.Add("onblur", "fncblur('','" & Me.ddlSeikyuuSaki.ClientID & "')")
        '請求先コード
        Me.tbxSeikyuuSakiCd.Attributes.Add("onfocus", "fncFocus('" & Me.tbxSeikyuuSakiCd.ClientID & "')")
        Me.tbxSeikyuuSakiCd.Attributes.Add("onblur", "fncblur('','" & Me.tbxSeikyuuSakiCd.ClientID & "')")
        '請求先枝番
        Me.tbxSeikyuuSakiBrc.Attributes.Add("onfocus", "fncFocus('" & Me.tbxSeikyuuSakiBrc.ClientID & "')")
        Me.tbxSeikyuuSakiBrc.Attributes.Add("onblur", "fncblur('','" & Me.tbxSeikyuuSakiBrc.ClientID & "')")

        '調査会社名
        Me.tbxTyousaKaisya_Mei.Attributes.Add("readonly", "true;")
        'FC店
        Me.tbxFCTenMei.Attributes.Add("readonly", "true;")
        '工事報告書直送変更ログインユーザ
        Me.tbxKojHkksTyokusouUpdLoginUserId.Attributes.Add("readonly", "true;")
        '工事報告書直送変更日時
        Me.tbxKojHkksTyokusouUpdDatetime.Attributes.Add("readonly", "true;")
        '建物検査センター
        'Me.tbxKensakuKensaCenterMei.Attributes.Add("readonly", "true;")
        '請求先
        Me.tbxSeikyuuSakiMei.Attributes.Add("readonly", "true;")
        '新会計支払先
        Me.tbxSkkShriSakiMei.Attributes.Add("readonly", "true;")
        'SAP用仕入先名
        Me.tbxSiireSakiMei.Attributes.Add("readonly", "true;")
        '支払集計先事業所
        Me.tbxTysKaisyaCd.Attributes.Add("readonly", "true;")
        '支払集計先事業所名
        Me.tbxTysKaisyaMei.Attributes.Add("readonly", "true;")
        '支払明細集計先事業所
        Me.tbxTysMeisaiKaisyaCd.Attributes.Add("readonly", "true;")
        '支払明細集計先事業所名
        Me.tbxTysMeisaiKaisyaMei.Attributes.Add("readonly", "true;")

        '登録ボタン
        Me.btnTouroku.Attributes.Add("onClick", "if(!fncCheck01()){return false;}else{if(!fncCheck02()){return false;}else{if(!fncCheck03()){return false;}}};disableButton1();")

        '修正ボタン
        Me.btnSyuusei.Attributes.Add("onClick", "if(!fncCheck01()){return false;}else{if(!fncCheck02()){return false;}else{if(!fncCheck03()){return false;}}};disableButton1();")

        '明細クリア
        btnClearMeisai.Attributes.Add("onclick", "if(!confirm('クリアを行ないます。\nよろしいですか？')){return false;};disableButton1();")

        '請求先名・送付住所にコピー
        btnKensakuSeikyuuSoufuCopy.Attributes.Add("onclick", "if(!confirm('請求先名・送付住所に上書きコピーします。\nよろしいですか？')){return false;};disableButton1();")

        btnSearchTyousaKaisya.Attributes.Add("onclick", "disableButton1();")
        btnSearch.Attributes.Add("onclick", "disableButton1();")
        btnClear.Attributes.Add("onclick", "disableButton1();")
        btnKensakuYuubinNo.Attributes.Add("onclick", "disableButton1();")
        btnKensakuFCTen.Attributes.Add("onclick", "disableButton1();")
        'btnKensakuKensaCenter.Attributes.Add("onclick", "disableButton1();")

        '======================2011/06/27 車龍 修正 開始↓=================================
        'btnKensakuSeikyuuSaki.Attributes.Add("onclick", "disableButton1();")
        btnKensakuSeikyuuSaki.Attributes.Add("onclick", "if(!fncSeikyuusakiChangeCheck()){return false;}else{disableButton1();}")
        '======================2011/06/27 車龍 修正 終了↑=================================

        btnKensakuSeikyuuSyousai.Attributes.Add("onclick", "disableButton1();")
        btnKensakuSeikyuuSyo.Attributes.Add("onclick", "disableButton1();")
        btnKensakuSkkShriSaki.Attributes.Add("onclick", "disableButton1();")
        btnKensakuShriJigyousyo.Attributes.Add("onclick", "disableButton1();")
        btnKensakuShriMeisaiJigyousyo.Attributes.Add("onclick", "disableButton1();")

        btnOK.Attributes.Add("onclick", "disableButton1();")
        btnNO.Attributes.Add("onclick", "disableButton1();")

        tbxSeikyuuSakiShriSakiKana.Attributes.Add("onblur", "fncTokomozi(this)")
        tbxTyousaKaisyaMeiKana.Attributes.Add("onblur", "fncTokomozi(this)")

        'JavaScript
        MakeScript()

        If Not blnBtn Then
            btnSyuusei.Enabled = False
            btnTouroku.Enabled = False
        End If

        '新会計事業所コード・新会計支払先コードの権限設定
        If blnBtn1 And Not blnBtn2 Then
            tbxSkkJigyousyoCd.Enabled = False
            tbxSkkShriSakiCd.Enabled = False
            btnKensakuSkkShriSaki.Enabled = False
        End If

    End Sub

    ''' <summary>
    ''' 調査会社検索ボタン押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSearchTyousaKaisya_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchTyousaKaisya.Click
        Dim strScript As String = ""
        'データ取得
        Dim dtTyousaKaisyaTable As New Itis.Earth.DataAccess.CommonSearchDataSet.tyousakaisyaTableDataTable
        dtTyousaKaisyaTable = TyousaKaisyaMasterBL.SelTyousaKaisya(tbxTyousaKaisya_Cd.Text, "", False)

        '検索結果が1件だった場合
        If dtTyousaKaisyaTable.Rows.Count = 1 Then
            tbxTyousaKaisya_Cd.Text = dtTyousaKaisyaTable.Item(0).tys_kaisya_cd.ToString & _
                                 dtTyousaKaisyaTable.Item(0).jigyousyo_cd.ToString
            tbxTyousaKaisya_Mei.Text = dtTyousaKaisyaTable.Item(0).tys_kaisya_mei
        Else
            tbxTyousaKaisya_Mei.Text = ""
            strScript = "objSrchWin = window.open('search_tyousa.aspx?Kbn='+escape('調査')+'&soukoCd='+escape('#')+'&FormName=" & _
            Me.Page.Form.Name & "&objCd=" & _
           tbxTyousaKaisya_Cd.ClientID & _
                    "&objMei=" & tbxTyousaKaisya_Mei.ClientID & _
                    "&strCd='+escape(eval('document.all.'+'" & _
                    tbxTyousaKaisya_Cd.ClientID & "').value)+'&strMei='+escape(eval('document.all.'+'" & _
                    tbxTyousaKaisya_Mei.ClientID & "').value), 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strScript, True)
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

        If strErr = "" Then
            '入力必須
            strErr = commoncheck.CheckHissuNyuuryoku(tbxTyousaKaisya_Cd.Text, "調査会社コード")
        End If
        If strErr = "" Then
            '半角英数字
            strErr = commoncheck.ChkHankakuEisuuji(tbxTyousaKaisya_Cd.Text, "調査会社コード")
        End If
        If strErr <> "" Then
            strErr = "alert('" & strErr & "');document.getElementById('" & tbxTyousaKaisya_Cd.ClientID & "').focus();"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strErr, True)
        Else
            GetMeisaiData(tbxTyousaKaisya_Cd.Text, tbxTyousaKaisyaCd.Text, tbxJigyousyoCd.Text, "btnSearch")
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
        Dim strHenkou As String = ""
        Dim strDisplayName As String = ""
        strID = InputCheck(strErr)

        '20101112 馬艶軍 「画面の住所」の整合性チェックは外してしまってください。 削除　↓
        ''郵便番号存在チェック
        'If strErr = "" And Trim(tbxYuubinNo.Text) <> "" Then
        '    Dim dtTable As New Data.DataTable
        '    dtTable = TyousaKaisyaMasterBL.SelYuubinInfo(Trim(tbxYuubinNo.Text.Replace("-", "")))
        '    If dtTable.Rows.Count <> 1 Then
        '        strErr = String.Format(Messages.Instance.MSG2036E, "郵便番号マスタ").ToString
        '        strID = tbxYuubinNo.ClientID
        '    End If
        'End If

        ''請求書送付先郵便番号存在チェック
        'If strErr = "" And Trim(tbxSkysySoufuYuubinNo.Text) <> "" Then
        '    Dim dtTable As New Data.DataTable
        '    dtTable = TyousaKaisyaMasterBL.SelYuubinInfo(Trim(tbxSkysySoufuYuubinNo.Text.Replace("-", "")))
        '    If dtTable.Rows.Count <> 1 Then
        '        strErr = String.Format(Messages.Instance.MSG2036E, "郵便番号マスタ").ToString
        '        strID = tbxSkysySoufuYuubinNo.ClientID
        '    End If
        'End If
        '20101112 馬艶軍 「画面の住所」の整合性チェックは外してしまってください。 削除　↑

        '営業所存在チェック
        If strErr = "" And Trim(tbxFCTen.Text) <> "" Then
            Dim dtTable As New Data.DataTable
            dtTable = TyousaKaisyaMasterBL.SelEigyousyo(Trim(tbxFCTen.Text))
            If dtTable.Rows.Count <> 1 Then
                strErr = String.Format(Messages.Instance.MSG2036E, "営業所マスタ").ToString
                strID = tbxFCTen.ClientID
                tbxFCTenMei.Text = ""
            End If
        End If

        If strErr = "" Then
            '【請求先新規登録】が表示されている場合、調査会社名のチェックを行う
            If Me.hidConfirm1.Value = "OK" Then
                If TrimNull(Me.tbxTyousaKaisyaMei.Text) = "" Then
                    strID = Me.tbxTyousaKaisyaMei.ClientID
                    strErr = Messages.Instance.MSG033E
                End If
            End If
        End If
        'エラーがある時
        If strErr <> "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('" & strErr & "');document.getElementById('" & strID & "').focus();", True)
        Else
            Dim dtTyousaKaisyaTable As New Itis.Earth.DataAccess.TyousaKaisyaDataSet.m_tyousakaisyaDataTable
            dtTyousaKaisyaTable = TyousaKaisyaMasterBL.SelMTyousaKaisyaInfo("", tbxTyousaKaisyaCd.Text, tbxJigyousyoCd.Text, "btnTouroku")
            '重複チェック
            If dtTyousaKaisyaTable.Rows.Count <> 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('マスターに重複データが存在します。');document.getElementById('" & tbxTyousaKaisyaCd.ClientID & "').focus();", True)
                Return
            End If

            '工事報告書直送を変更した場合
            If TrimNull(ddlKojHkksTyokusouFlg.SelectedValue) = "" Then
                strHenkou = "NO"
            Else
                strHenkou = "YES"
                strDisplayName = TyousaKaisyaMasterBL.SelKoujiInfo(ViewState("UserId")).Rows(0).Item("DisplayName").ToString
                If strDisplayName = "" Then
                    strDisplayName = ViewState("UserId")
                End If
            End If

            Dim strTrue As String = ""
            '【請求先新規登録】が表示されている場合
            If Me.hidConfirm1.Value = "OK" Then
                strTrue = "OK"
            End If

            'データ登録
            If TyousaKaisyaMasterBL.InsTyousaKaisya(SetMeisaiData, strHenkou, strDisplayName, strTrue) Then
                strErr = "alert('" & Replace(Messages.Instance.MSG018S, "@PARAM1", "調査会社マスタ") & "');"
                '再取得
                GetMeisaiData("", tbxTyousaKaisyaCd.Text, tbxJigyousyoCd.Text, "btnTouroku")
            Else
                strErr = "alert('" & Replace(Messages.Instance.MSG019E, "@PARAM1", "調査会社マスタ") & "');"
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
        Dim strHenkou As String = ""
        Dim strDisplayName As String = ""
        'チェック
        strID = InputCheck(strErr)

        '20101112 馬艶軍 「画面の住所」の整合性チェックは外してしまってください。 削除　↓
        ''郵便番号存在チェック
        'If strErr = "" And Trim(tbxYuubinNo.Text) <> "" Then
        '    Dim dtTable As New Data.DataTable
        '    dtTable = TyousaKaisyaMasterBL.SelYuubinInfo(Trim(tbxYuubinNo.Text.Replace("-", "")))
        '    If dtTable.Rows.Count = 0 Then
        '        strErr = String.Format(Messages.Instance.MSG2036E, "郵便番号マスタ").ToString
        '        strID = tbxYuubinNo.ClientID
        '    End If
        'End If

        ''請求書送付先郵便番号存在チェック
        'If strErr = "" And Trim(tbxSkysySoufuYuubinNo.Text) <> "" Then
        '    Dim dtTable As New Data.DataTable
        '    dtTable = TyousaKaisyaMasterBL.SelYuubinInfo(Trim(tbxSkysySoufuYuubinNo.Text.Replace("-", "")))
        '    If dtTable.Rows.Count <> 1 Then
        '        strErr = String.Format(Messages.Instance.MSG2036E, "郵便番号マスタ").ToString
        '        strID = tbxSkysySoufuYuubinNo.ClientID
        '    End If
        'End If
        '20101112 馬艶軍 「画面の住所」の整合性チェックは外してしまってください。 削除　↑

        '営業所存在チェック
        If strErr = "" And Trim(tbxFCTen.Text) <> "" Then
            Dim dtTable As New Data.DataTable
            dtTable = TyousaKaisyaMasterBL.SelEigyousyo(Trim(tbxFCTen.Text))
            If dtTable.Rows.Count <> 1 Then
                strErr = String.Format(Messages.Instance.MSG2036E, "営業所マスタ").ToString
                strID = tbxFCTen.ClientID
                tbxFCTenMei.Text = ""
            End If
        End If

        If strErr = "" Then
            '【請求先新規登録】が表示されている場合、調査会社名のチェックを行う
            If Me.hidConfirm1.Value = "OK" Then
                If TrimNull(Me.tbxTyousaKaisyaMei.Text) = "" Then
                    strID = Me.tbxTyousaKaisyaMei.ClientID
                    strErr = Messages.Instance.MSG033E
                End If
            End If
        End If
        'エラーがある時
        If strErr <> "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('" & strErr & "');document.getElementById('" & strID & "').focus();", True)
        Else
            '工事報告書直送を変更した場合
            If hidKojHkksTyokusouFlg.Value = ddlKojHkksTyokusouFlg.SelectedValue Then
                strHenkou = "NO"
            Else
                strHenkou = "YES"
                strDisplayName = TyousaKaisyaMasterBL.SelKoujiInfo(ViewState("UserId")).Rows(0).Item("DisplayName").ToString
                If strDisplayName = "" Then
                    strDisplayName = ViewState("UserId")
                End If
            End If

            Dim strTrue As String = ""
            '【請求先新規登録】が表示されている場合
            If Me.hidConfirm1.Value = "OK" Then
                strTrue = "OK"
            End If

            '更新処理
            strReturn = TyousaKaisyaMasterBL.UpdTyousaKaisya(SetMeisaiData, strHenkou, strDisplayName, strTrue)
            If strReturn = "0" Then
                '更新成功
                strErr = "alert('" & Replace(Messages.Instance.MSG018S, "@PARAM1", "調査会社マスタ") & "');"
                '画面再描画処理
                GetMeisaiData("", tbxTyousaKaisyaCd.Text, tbxJigyousyoCd.Text, "btnSyuusei")
            ElseIf strReturn = "1" Then
                '更新失敗
                strErr = "alert('" & Replace(Messages.Instance.MSG019E, "@PARAM1", "調査会社マスタ") & "');"
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

        '活性化制御共通処理 請求先.検索ボタン押下時
        SetKasseika()

        Dim strScript As String = ""

        '請求先情報取得
        Dim dtSeikyuuSakiTable As New Itis.Earth.DataAccess.CommonSearchDataSet.SeikyuuSakiTable1DataTable
        'dtSeikyuuSakiTable = CommonSearchLogic.GetSeikyuuSakiInfo("2", ddlSeikyuuSaki.SelectedValue, tbxSeikyuuSakiCd.Text, tbxSeikyuuSakiBrc.Text, "", False)
        dtSeikyuuSakiTable = TyousaKaisyaMasterBL.SelVSeikyuuSakiInfo(ddlSeikyuuSaki.SelectedValue, tbxSeikyuuSakiCd.Text, tbxSeikyuuSakiBrc.Text, False)

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
        ElseIf dtSeikyuuSakiTable.Rows.Count = 0 Then
            '検索結果が0件だった場合
            '画面.請求先区分＝"1：調査会社" かつ 画面.調査会社コード＝画面.請求先コード かつ 画面.事業所コード＝画面.請求先枝番の場合
            If (ddlSeikyuuSaki.SelectedValue = "1") And (tbxSeikyuuSakiCd.Text <> "") And (tbxSeikyuuSakiBrc.Text <> "") And (tbxTyousaKaisyaCd.Text.ToUpper = tbxSeikyuuSakiCd.Text.ToUpper) And (tbxJigyousyoCd.Text.ToUpper = tbxSeikyuuSakiBrc.Text.ToUpper) Then
                'メッセージ表示
                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "fncConfirm();", True)
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "confrim", "if(confirm('この内容の登録時に請求先マスタに登録しますか？')){ window.setTimeout('objEBI(\'" & Me.btnOK.ClientID & "\').click()',10);}else{ window.setTimeout('objEBI(\'" & Me.btnNO.ClientID & "\').click()',10);}; ", True)
            Else
                '請求先名
                tbxSeikyuuSakiMei.Text = ""
                strScript = "objSrchWin = window.open('search_Seikyuusaki.aspx?blnDelete=True&&Kbn='+escape('請求先')+'&FormName=" & _
                                             Me.Page.Form.Name & "&objKbn=" & _
                                             ddlSeikyuuSaki.ClientID & _
                                             "&objHidKbn=" & hidSeikyuuSakiKbn.ClientID & _
                                             "&objCd=" & tbxSeikyuuSakiCd.ClientID & _
                                             "&objHidCd=" & hidSeikyuuSakiCd.ClientID & _
                                             "&objBrc=" & tbxSeikyuuSakiBrc.ClientID & _
                                             "&hidConfirm2=" & hidConfirm2.ClientID & _
                                             "&objHidBrc=" & hidSeikyuuSakiBrc.ClientID & _
                                             "&objMei=" & tbxSeikyuuSakiMei.ClientID & _
                                             "&strKbn='+escape(eval('document.all.'+'" & _
                                             ddlSeikyuuSaki.ClientID & "').value)+'&strCd='+escape(eval('document.all.'+'" & _
                                             tbxSeikyuuSakiCd.ClientID & "').value)+'&strBrc='+escape(eval('document.all.'+'" & _
                                             tbxSeikyuuSakiBrc.ClientID & "').value), 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strScript, True)
            End If
        Else
            '請求先名
            tbxSeikyuuSakiMei.Text = ""
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

        data = (TyousaKaisyaMasterBL.GetMailAddress(Me.tbxYuubinNo.Text.Replace("-", String.Empty).Trim))
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
    ''' 請求書送付先郵便番号.検索ボタン押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnKensakuSeikyuuSyo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKensakuSeikyuuSyo.Click
        '住所
        Dim data As DataSet
        Dim jyuusyo As String
        Dim jyuusyoMei As String
        Dim jyuusyoNo As String

        '住所取得
        Dim csScript As New StringBuilder

        data = (TyousaKaisyaMasterBL.GetMailAddress(Me.tbxSkysySoufuYuubinNo.Text.Replace("-", String.Empty).Trim))
        If data.Tables(0).Rows.Count = 1 Then

            jyuusyo = data.Tables(0).Rows(0).Item(0).ToString
            jyuusyoNo = jyuusyo.Split(",")(0)
            jyuusyoMei = GetJyusho(jyuusyo.Split(",")(1))
            If jyuusyoNo.Length > 3 Then
                jyuusyoNo = jyuusyoNo.Substring(0, 3) & "-" & jyuusyoNo.Substring(3, jyuusyoNo.Length - 3)
            End If

            csScript.AppendLine("if(document.getElementById('" & Me.tbxSkysySoufuJyuusyo1.ClientID & "').value!='' || document.getElementById('" & Me.tbxSkysySoufuJyuusyo2.ClientID & "').value!=''){" & vbCrLf)
            csScript.AppendLine("if (!confirm('既存データがありますが上書きしてよろしいですか。')){}else{ " & vbCrLf)

            csScript.AppendLine("document.getElementById('" & Me.tbxSkysySoufuJyuusyo1.ClientID & "').value = '" & jyuusyoMei.Split(",")(0) & "';" & vbCrLf)
            csScript.AppendLine("document.getElementById('" & Me.tbxSkysySoufuJyuusyo2.ClientID & "').value = '" & jyuusyoMei.Split(",")(1) & "';" & vbCrLf)
            csScript.AppendLine("document.getElementById('" & Me.tbxSkysySoufuYuubinNo.ClientID & "').value = '" & jyuusyoNo & "';" & vbCrLf)

            csScript.AppendLine("}" & vbCrLf)

            csScript.AppendLine("}else{" & vbCrLf)
            csScript.AppendLine("document.getElementById('" & Me.tbxSkysySoufuJyuusyo1.ClientID & "').value = '" & jyuusyoMei.Split(",")(0) & "';" & vbCrLf)
            csScript.AppendLine("document.getElementById('" & Me.tbxSkysySoufuJyuusyo2.ClientID & "').value = '" & jyuusyoMei.Split(",")(1) & "';" & vbCrLf)
            csScript.AppendLine("document.getElementById('" & Me.tbxSkysySoufuYuubinNo.ClientID & "').value = '" & jyuusyoNo & "';" & vbCrLf)

            csScript.AppendLine("}" & vbCrLf)
        Else
            csScript.AppendLine("fncOpenwindowYuubin('" & Me.tbxSkysySoufuYuubinNo.ClientID & "','" & Me.tbxSkysySoufuJyuusyo1.ClientID & "','" & Me.tbxSkysySoufuJyuusyo2.ClientID & "');")
        End If
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "openWindowYuubin", csScript.ToString, True)
    End Sub

    ''' <summary>
    ''' FC店検索ボタン押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnKensakuFCTen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKensakuFCTen.Click
        Dim strScript As String = ""
        'データ取得
        Dim dtKeiretuTable As New Itis.Earth.DataAccess.CommonSearchDataSet.EigyousyoTableDataTable
        dtKeiretuTable = TyousaKaisyaMasterBL.SelEigyousyo(tbxFCTen.Text)

        '検索結果が1件だった場合
        If dtKeiretuTable.Rows.Count = 1 Then
            tbxFCTen.Text = TrimNull(dtKeiretuTable.Item(0).eigyousyo_cd)
            tbxFCTenMei.Text = TrimNull(dtKeiretuTable.Item(0).eigyousyo_mei)
        Else
            tbxFCTenMei.Text = ""
            strScript = "objSrchWin = window.open('search_common.aspx?Kbn='+escape('営業所')+'&FormName=" & Form.Name & "&objCd=" & Me.tbxFCTen.ClientID & "&objMei=" & Me.tbxFCTenMei.ClientID & "&strCd='+escape(eval('document.all.'+'" & Me.tbxFCTen.ClientID & "').value)+'&strMei='+escape(eval('document.all.'+'" & Me.tbxFCTenMei.ClientID & "').value)+'&KensakuKubun=A&blnDelete=True', 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strScript, True)
        End If
    End Sub

    '''' <summary>
    '''' 建物検査センター検索ボタン押下時処理
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <remarks></remarks>
    'Protected Sub btnKensakuKensaCenter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKensakuKensaCenter.Click
    '    Dim strScript As String = ""
    '    'データ取得
    '    Dim dtFcTable As New DataTable
    '    dtFcTable = TyousaKaisyaMasterBL.SelMfcInfo(tbxKensakuKensaCenter.Text)

    '    '検索結果が1件だった場合
    '    If dtFcTable.Rows.Count = 1 Then
    '        tbxKensakuKensaCenter.Text = TrimNull(dtFcTable.Rows(0).Item("fc_cd"))
    '        hidKensakuKensaCenter.Value = TrimNull(dtFcTable.Rows(0).Item("fc_cd"))
    '        tbxKensakuKensaCenterMei.Text = TrimNull(dtFcTable.Rows(0).Item("fc_nm"))
    '    Else
    '        tbxKensakuKensaCenterMei.Text = ""
    '        strScript = "alert('該当の検査センターはありません。');"
    '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strScript, True)
    '    End If
    'End Sub

    ''' <summary>
    ''' 新会計支払先.検索ボタン押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnKensakuSkkShriSaki_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKensakuSkkShriSaki.Click
        Dim strScript As String = ""
        'データ取得
        Dim dtSkkShriSakiTable As New Data.DataTable
        dtSkkShriSakiTable = TyousaKaisyaMasterBL.SelSKK(tbxSkkJigyousyoCd.Text, tbxSkkShriSakiCd.Text)

        '検索結果が1件だった場合
        If dtSkkShriSakiTable.Rows.Count = 1 Then
            '新会計事業所コード
            tbxSkkJigyousyoCd.Text = TrimNull(dtSkkShriSakiTable.Rows(0).Item("skk_jigyou_cd"))
            hidSkkJigyousyoCd.Value = TrimNull(dtSkkShriSakiTable.Rows(0).Item("skk_jigyou_cd"))
            '新会計支払先コード
            tbxSkkShriSakiCd.Text = TrimNull(dtSkkShriSakiTable.Rows(0).Item("skk_shri_saki_cd"))
            hidSkkShriSakiCd.Value = TrimNull(dtSkkShriSakiTable.Rows(0).Item("skk_shri_saki_cd"))
            '新会計支払先名 
            tbxSkkShriSakiMei.Text = TrimNull(dtSkkShriSakiTable.Rows(0).Item("shri_saki_mei_kanji"))
        Else
            tbxSkkShriSakiMei.Text = ""
            strScript = "objSrchWin = window.open('search_SinkaikeiSiharaiSaki.aspx?Kbn='+escape('新会計支払先')+'&SiharaiKbn='+escape('Siharai')+'&FormName=" & _
            Me.Page.Form.Name & "&objCd=" & _
           tbxSkkJigyousyoCd.ClientID & _
           "&objCd2=" & tbxSkkShriSakiCd.ClientID & _
           "&objHidCd2=" & hidSkkShriSakiCd.ClientID & _
                    "&objMei=" & tbxSkkShriSakiMei.ClientID & _
                    "&strCd='+escape(eval('document.all.'+'" & _
                    tbxSkkJigyousyoCd.ClientID & "').value)+'&strCd2='+escape(eval('document.all.'+'" & _
                    tbxSkkShriSakiCd.ClientID & "').value)+'&strMei='+escape(eval('document.all.'+'" & _
                    tbxSkkShriSakiMei.ClientID & "').value), 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strScript, True)
        End If
    End Sub




    Protected Sub btnSiireSakiKensaku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim ds As DataSet = CommonSearchLogic.SelSAPSiireSaki(0, "", Me.tbxSiireSaki.Text, "", "a1_ktokk asc")

        If ds.Tables(1).Rows.Count = 1 Then

            Me.tbxSiireSaki.Text = ds.Tables(1).Rows(0).Item(1).ToString
            Me.tbxSiireSakiMei.Text = ds.Tables(1).Rows(0).Item(2).ToString




        Else
            Dim strScript As String = "window.open('search_SAPSiireSaki.aspx', 'searchWindow2', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes')"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strScript, True)

        End If


    End Sub




    ''' <summary>
    ''' 支払集計先事業所検索ボタン押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnKensakuShriJigyousyo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKensakuShriJigyousyo.Click
        Dim strScript As String = ""
        'データ取得
        Dim dtTyousaKaisyaTable As New Itis.Earth.DataAccess.CommonSearchDataSet.tyousakaisyaTableDataTable
        'dtTyousaKaisyaTable = CommonSearchLogic.GetTyousaInfo("2", tbxTysKaisyaCd.Text & tbxShriJigyousyoCd.Text, "", "")
        dtTyousaKaisyaTable = TyousaKaisyaMasterBL.SelTyousaKaisya(tbxTysKaisyaCd.Text, tbxShriJigyousyoCd.Text, True)

        '検索結果が1件だった場合
        If dtTyousaKaisyaTable.Rows.Count = 1 Then
            tbxShriJigyousyoCd.Text = TrimNull(dtTyousaKaisyaTable.Item(0).jigyousyo_cd)
            hidShriJigyousyoCd.Value = TrimNull(dtTyousaKaisyaTable.Item(0).jigyousyo_cd)
            tbxTysKaisyaMei.Text = TrimNull(dtTyousaKaisyaTable.Item(0).tys_kaisya_mei)
        Else
            tbxTysKaisyaMei.Text = ""
            strScript = "objSrchWin = window.open('search_SiharaiTyousa.aspx?Kbn='+escape('支払集計先事業所')+'&SiharaiKbn='+escape('Siharai')+'&FormName=" & _
            Me.Page.Form.Name & "&objCd=" & _
           tbxTysKaisyaCd.ClientID & _
           "&objCd2=" & tbxShriJigyousyoCd.ClientID & _
           "&objHidCd2=" & hidShriJigyousyoCd.ClientID & _
                    "&objMei=" & tbxTysKaisyaMei.ClientID & _
                    "&strCd='+escape(eval('document.all.'+'" & _
                    tbxTysKaisyaCd.ClientID & "').value)+'&strCd2='+escape(eval('document.all.'+'" & _
                    tbxShriJigyousyoCd.ClientID & "').value)+'&strMei='+escape(eval('document.all.'+'" & _
                    tbxTysKaisyaMei.ClientID & "').value), 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strScript, True)
        End If
    End Sub

    ''' <summary>
    ''' 支払明細集計先事業所検索ボタン押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnKensakuShriMeisaiJigyousyo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKensakuShriMeisaiJigyousyo.Click
        Dim strScript As String = ""
        'データ取得
        Dim dtTyousaKaisyaTable As New Itis.Earth.DataAccess.CommonSearchDataSet.tyousakaisyaTableDataTable
        'dtTyousaKaisyaTable = CommonSearchLogic.GetTyousaInfo("2", tbxTysMeisaiKaisyaCd.Text & tbxShriMeisaiJigyousyoCd.Text, "", "")
        dtTyousaKaisyaTable = TyousaKaisyaMasterBL.SelTyousaKaisya(tbxTysMeisaiKaisyaCd.Text, tbxShriMeisaiJigyousyoCd.Text, True)

        '検索結果が1件だった場合
        If dtTyousaKaisyaTable.Rows.Count = 1 Then
            tbxShriMeisaiJigyousyoCd.Text = TrimNull(dtTyousaKaisyaTable.Item(0).jigyousyo_cd)
            hidShriMeisaiJigyousyoCd.Value = TrimNull(dtTyousaKaisyaTable.Item(0).jigyousyo_cd)
            tbxTysMeisaiKaisyaMei.Text = TrimNull(dtTyousaKaisyaTable.Item(0).tys_kaisya_mei)
        Else
            tbxTysMeisaiKaisyaMei.Text = ""
            strScript = "objSrchWin = window.open('search_SiharaiTyousa.aspx?Kbn='+escape('支払明細集計先事業所')+'&SiharaiKbn='+escape('Siharai')+'&FormName=" & _
            Me.Page.Form.Name & "&objCd=" & _
           tbxTysMeisaiKaisyaCd.ClientID & _
           "&objCd2=" & tbxShriMeisaiJigyousyoCd.ClientID & _
           "&objHidCd2=" & hidShriMeisaiJigyousyoCd.ClientID & _
                    "&objMei=" & tbxTysMeisaiKaisyaMei.ClientID & _
                    "&strCd='+escape(eval('document.all.'+'" & _
                    tbxTysMeisaiKaisyaCd.ClientID & "').value)+'&strCd2='+escape(eval('document.all.'+'" & _
                    tbxShriMeisaiJigyousyoCd.ClientID & "').value)+'&strMei='+escape(eval('document.all.'+'" & _
                    tbxTysMeisaiKaisyaMei.ClientID & "').value), 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strScript, True)
        End If
    End Sub

    ''' <summary>Javascript作成</summary>
    Private Sub MakeScript()
        Dim csType As Type = Page.GetType()
        Dim csName As String = "GetScript"
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("<script language='javascript' type='text/javascript'>")
            '取消
            .AppendLine("function fncSetTorikesiVal()")
            .AppendLine("{")
            .AppendLine("   var objTorikesi = document.getElementById('" & Me.chkTorikesi.ClientID & "')")
            .AppendLine("   var objTorikesiRiyuu = document.getElementById('" & Me.tbxTorikesiRiyuu.ClientID & "')")
            .AppendLine("   var hidTorikesi = document.getElementById('" & Me.hidTorikesi.ClientID & "')")
            .AppendLine("   if(objTorikesi.checked == true){")
            .AppendLine("       objTorikesiRiyuu.readOnly = false;")
            .AppendLine("       hidTorikesi.value = 'true';")
            .AppendLine("   }else{")
            .AppendLine("       objTorikesiRiyuu.value='';")
            .AppendLine("       objTorikesiRiyuu.readOnly = true;")
            .AppendLine("       hidTorikesi.value = 'false';")
            .AppendLine("   }")
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
            '日付チェック(yyyy/mm)
            .AppendLine("   function fncCheckNengetu(obj,objId){")
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
            .AppendLine("           if (objId == 'ＦＣ 入会年月'){")
            .AppendLine("               alert('" & Replace(Messages.Instance.MSG2017E, "@PARAM1", "ＦＣ 入会年月").ToString & "');")
            .AppendLine("           }else if(objId == 'ＦＣ 退会年月'){")
            .AppendLine("               alert('" & Replace(Messages.Instance.MSG2017E, "@PARAM1", "ＦＣ 退会年月").ToString & "');")
            .AppendLine("           }else if(objId == 'ＪＡＰＡＮ会 入会年月'){")
            .AppendLine("               alert('" & Replace(Messages.Instance.MSG2017E, "@PARAM1", "ＪＡＰＡＮ会 入会年月").ToString & "');")
            .AppendLine("           }else if(objId == 'ＪＡＰＡＮ会 退会年月'){")
            .AppendLine("               alert('" & Replace(Messages.Instance.MSG2017E, "@PARAM1", "ＪＡＰＡＮ会 退会年月").ToString & "');")
            .AppendLine("           }else if(objId == 'ファクタリング開始年月'){")
            .AppendLine("               alert('" & Replace(Messages.Instance.MSG2017E, "@PARAM1", "ファクタリング開始年月").ToString & "');")
            .AppendLine("           }")
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
            '値変更時に、「支払集計先会社コード」と「支払明細集計先会社コード」に同じ値をコピーする
            .AppendLine("function fncSetCopy()")
            .AppendLine("{")
            .AppendLine("   var objTyousaKaisyaCd = document.getElementById('" & Me.tbxTyousaKaisyaCd.ClientID & "')")
            .AppendLine("   var objTysKaisyaCd = document.getElementById('" & Me.tbxTysKaisyaCd.ClientID & "')")
            .AppendLine("   var objTysMeisaiKaisyaCd = document.getElementById('" & Me.tbxTysMeisaiKaisyaCd.ClientID & "')")
            .AppendLine("   objTysKaisyaCd.value = objTyousaKaisyaCd.value;")
            .AppendLine("   objTysMeisaiKaisyaCd.value = objTyousaKaisyaCd.value;")
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
            '建物検査センターボタン関連
            '.AppendLine("   var tbxKensakuKensaCenter = document.getElementById('" & Me.tbxKensakuKensaCenter.ClientID & "')")
            .AppendLine("   var hidKensakuKensaCenter = document.getElementById('" & Me.hidKensakuKensaCenter.ClientID & "')")
            .AppendLine("   var tbxSkkShriSakiCd = document.getElementById('" & Me.tbxSkkShriSakiCd.ClientID & "')")
            .AppendLine("   var hidSkkShriSakiCd = document.getElementById('" & Me.hidSkkShriSakiCd.ClientID & "')")
            '支払集計先事業所検索関連
            .AppendLine("   var tbxShriJigyousyoCd = document.getElementById('" & Me.tbxShriJigyousyoCd.ClientID & "')")
            .AppendLine("   var hidShriJigyousyoCd = document.getElementById('" & Me.hidShriJigyousyoCd.ClientID & "')")
            '支払明細集計先事業所検索関連
            .AppendLine("   var tbxShriMeisaiJigyousyoCd = document.getElementById('" & Me.tbxShriMeisaiJigyousyoCd.ClientID & "')")
            .AppendLine("   var hidShriMeisaiJigyousyoCd = document.getElementById('" & Me.hidShriMeisaiJigyousyoCd.ClientID & "')")

            .AppendLine("   var tbxSkkShriSakiMei = document.getElementById('" & Me.tbxSkkShriSakiMei.ClientID & "')")
            .AppendLine("   var tbxTysKaisyaMei = document.getElementById('" & Me.tbxTysKaisyaMei.ClientID & "')")
            .AppendLine("   var tbxTysMeisaiKaisyaMei = document.getElementById('" & Me.tbxTysMeisaiKaisyaMei.ClientID & "')")
            '.AppendLine("   var tbxKensakuKensaCenterMei = document.getElementById('" & Me.tbxKensakuKensaCenterMei.ClientID & "')")

            '請求先
            .AppendLine("   var hidConfirm2 = document.getElementById('" & Me.hidConfirm2.ClientID & "')")
            .AppendLine("   if((ddlSeikyuuSaki.value!='')||(tbxSeikyuuSakiCd.value!='')||(tbxSeikyuuSakiBrc.value!='')){")
            .AppendLine("   if(hidConfirm2.value=='検索'){")
            .AppendLine("       alert('" & Replace(Messages.Instance.MSG030E, "@PARAM1", "請求先") & "');")
            .AppendLine("       ddlSeikyuuSaki.focus();")
            .AppendLine("       return false;")
            .AppendLine("   }")
            .AppendLine("   }")

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

            .AppendLine("   }else if((tbxSkkShriSakiCd.value != hidSkkShriSakiCd.value)&&(tbxSkkShriSakiCd.value !='')){")
            .AppendLine("       alert('" & Replace(Messages.Instance.MSG030E, "@PARAM1", "新会計支払先コード") & "');")
            .AppendLine("       tbxSkkShriSakiMei.value='';")
            .AppendLine("       tbxSkkShriSakiCd.focus();")
            .AppendLine("       return false;")
            '支払集計先事業所
            .AppendLine("   }else if((tbxShriJigyousyoCd.value != hidShriJigyousyoCd.value)&&(tbxShriJigyousyoCd.value != '')){")
            .AppendLine("       alert('" & Replace(Messages.Instance.MSG030E, "@PARAM1", "支払集計先事業所コード") & "');")
            .AppendLine("       tbxTysKaisyaMei.value='';")
            .AppendLine("       tbxShriJigyousyoCd.focus();")
            .AppendLine("       return false;")
            '支払明細集計先事業所
            .AppendLine("   }else if((tbxShriMeisaiJigyousyoCd.value != hidShriMeisaiJigyousyoCd.value)&&(tbxShriMeisaiJigyousyoCd.value != '')){")
            .AppendLine("       alert('" & Replace(Messages.Instance.MSG030E, "@PARAM1", "支払明細集計先事業所コード") & "');")
            .AppendLine("       tbxTysMeisaiKaisyaMei.value='';")
            .AppendLine("       tbxShriMeisaiJigyousyoCd.focus();")
            .AppendLine("       return false;")
            '建物検査センター
            '.AppendLine("   }else if((tbxKensakuKensaCenter.value != hidKensakuKensaCenter.value)&&(tbxKensakuKensaCenter.value != '')){")
            '.AppendLine("       alert('" & Replace(Messages.Instance.MSG030E, "@PARAM1", "建物検査センターコード") & "');")
            '.AppendLine("       tbxKensakuKensaCenterMei.value='';")
            '.AppendLine("       tbxKensakuKensaCenter.focus();")
            '.AppendLine("       return false;")
            .AppendLine("   }else{")
            .AppendLine("       return true;")
            .AppendLine("   }")
            .AppendLine("}")

            'その他チェックCHK01
            .AppendLine("function fncCheck01()")
            .AppendLine("{")
            .AppendLine("   var ddlTktJbnTysSyuninSkkFlg = document.getElementById('" & Me.ddlTktJbnTysSyuninSkkFlg.ClientID & "')")
            .AppendLine("   var ddlTyousaGyoumu = document.getElementById('" & Me.ddlTyousaGyoumu.ClientID & "')")
            .AppendLine("   if((ddlTktJbnTysSyuninSkkFlg.value == '0')&&(ddlTyousaGyoumu.value == '1')){")
            .AppendLine("       alert('" & Messages.Instance.MSG031E & "');")
            .AppendLine("       ddlTyousaGyoumu.focus();")
            .AppendLine("       return false;")
            .AppendLine("   }else{")
            .AppendLine("       return true;")
            .AppendLine("   }")
            .AppendLine("}")

            'その他チェックCHK02
            .AppendLine("function fncCheck02()")
            .AppendLine("{")
            .AppendLine("   var chkTorikesi = document.getElementById('" & Me.chkTorikesi.ClientID & "')")
            .AppendLine("   var tbxTorikesiRiyuu = document.getElementById('" & Me.tbxTorikesiRiyuu.ClientID & "')")
            .AppendLine("   if((chkTorikesi.checked == true)&&(tbxTorikesiRiyuu.value == '')){")
            .AppendLine("       alert('" & Messages.Instance.MSG032E & "');")
            .AppendLine("       tbxTorikesiRiyuu.focus();")
            .AppendLine("       return false;")
            .AppendLine("   }else{")
            .AppendLine("       return true;")
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

            '請求先検索ボタン押下時
            .AppendLine("function fncConfirm()")
            .AppendLine("{")
            .AppendLine("   var hidConfirm = document.getElementById('" & Me.hidConfirm.ClientID & "')")
            .AppendLine("   if(confirm('この調査会社登録時に請求先マスタに登録しますか？')){")
            .AppendLine("       hidConfirm.value = 'OK';")
            .AppendLine("   }else{")
            .AppendLine("       hidConfirm.value = 'NO';")
            .AppendLine("   }")
            .AppendLine("   document.getElementById('" & Me.Form.Name & "').submit();")
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

            ''コピー
            '.AppendLine("function fncFocus(obj)")
            '.AppendLine("{")
            '.AppendLine("   var tbxTyousaKaisyaCd = document.getElementById(obj)")
            '.AppendLine("   var hidChange = document.getElementById('" & Me.hidChange.ClientID & "')")
            '.AppendLine("   hidChange.value = tbxTyousaKaisyaCd.value;")
            '.AppendLine("}")
            ''コピー
            '.AppendLine("function fncblur(kbn,obj)")
            '.AppendLine("{")
            '.AppendLine("   var tbxTyousaKaisyaCd = document.getElementById(obj)")
            '.AppendLine("   var hidChange = document.getElementById('" & Me.hidChange.ClientID & "')")
            '.AppendLine("   if(tbxTyousaKaisyaCd.value != hidChange.value){")
            '.AppendLine("       if(kbn=='1'){")
            '.AppendLine("           fncSetCopy();")
            '.AppendLine("           fncHyouji();")
            '.AppendLine("       }else{")
            '.AppendLine("           fncHyouji();")
            '.AppendLine("       }")
            '.AppendLine("   }")
            '.AppendLine("}")

            'コピー
            .AppendLine("function fncFocus()")
            .AppendLine("{")
            .AppendLine("   var tbxTyousaKaisyaCd = document.getElementById('" & Me.tbxTyousaKaisyaCd.ClientID & "')")
            .AppendLine("   var tbxJigyousyoCd = document.getElementById('" & Me.tbxJigyousyoCd.ClientID & "')")
            .AppendLine("   var ddlSeikyuuSaki = document.getElementById('" & Me.ddlSeikyuuSaki.ClientID & "')")
            .AppendLine("   var tbxSeikyuuSakiCd = document.getElementById('" & Me.tbxSeikyuuSakiCd.ClientID & "')")
            .AppendLine("   var tbxSeikyuuSakiBrc = document.getElementById('" & Me.tbxSeikyuuSakiBrc.ClientID & "')")

            .AppendLine("   var hidChange1 = document.getElementById('" & Me.hidChange1.ClientID & "')")
            .AppendLine("   var hidChange2 = document.getElementById('" & Me.hidChange2.ClientID & "')")
            .AppendLine("   var hidChange3 = document.getElementById('" & Me.hidChange3.ClientID & "')")
            .AppendLine("   var hidChange4 = document.getElementById('" & Me.hidChange4.ClientID & "')")
            .AppendLine("   var hidChange5 = document.getElementById('" & Me.hidChange5.ClientID & "')")
            .AppendLine("   hidChange1.value = tbxTyousaKaisyaCd.value;")
            .AppendLine("   hidChange2.value = tbxJigyousyoCd.value;")
            .AppendLine("   hidChange3.value = ddlSeikyuuSaki.value;")
            .AppendLine("   hidChange4.value = tbxSeikyuuSakiCd.value;")
            .AppendLine("   hidChange5.value = tbxSeikyuuSakiBrc.value;")
            .AppendLine("}")
            'コピー
            .AppendLine("function fncblur(kbn)")
            .AppendLine("{")
            .AppendLine("   var tbxTyousaKaisyaCd = document.getElementById('" & Me.tbxTyousaKaisyaCd.ClientID & "')")
            .AppendLine("   var tbxJigyousyoCd = document.getElementById('" & Me.tbxJigyousyoCd.ClientID & "')")
            .AppendLine("   var ddlSeikyuuSaki = document.getElementById('" & Me.ddlSeikyuuSaki.ClientID & "')")
            .AppendLine("   var tbxSeikyuuSakiCd = document.getElementById('" & Me.tbxSeikyuuSakiCd.ClientID & "')")
            .AppendLine("   var tbxSeikyuuSakiBrc = document.getElementById('" & Me.tbxSeikyuuSakiBrc.ClientID & "')")

            .AppendLine("   var hidChange1 = document.getElementById('" & Me.hidChange1.ClientID & "')")
            .AppendLine("   var hidChange2 = document.getElementById('" & Me.hidChange2.ClientID & "')")
            .AppendLine("   var hidChange3 = document.getElementById('" & Me.hidChange3.ClientID & "')")
            .AppendLine("   var hidChange4 = document.getElementById('" & Me.hidChange4.ClientID & "')")
            .AppendLine("   var hidChange5 = document.getElementById('" & Me.hidChange5.ClientID & "')")

            .AppendLine("   var hidConfirm1 = document.getElementById('" & Me.hidConfirm1.ClientID & "')")

            .AppendLine("   var hidConfirm2 = document.getElementById('" & Me.hidConfirm2.ClientID & "')")

            .AppendLine("   if(hidConfirm1.value=='OK'){")
            .AppendLine("   if((tbxTyousaKaisyaCd.value != hidChange1.value)||(tbxJigyousyoCd.value != hidChange2.value)||(ddlSeikyuuSaki.value != hidChange3.value)||(tbxSeikyuuSakiCd.value != hidChange4.value)||(tbxSeikyuuSakiBrc.value != hidChange5.value)){")
            .AppendLine("       if(kbn=='1'){")
            .AppendLine("           fncSetCopy();")
            .AppendLine("           fncHyouji();")
            .AppendLine("       }else{")
            .AppendLine("           fncHyouji();")
            .AppendLine("       }")
            .AppendLine("   hidConfirm2.value='検索';")
            .AppendLine("   }")
            .AppendLine("  }else{")
            .AppendLine("       if(kbn=='1'){")
            .AppendLine("           fncSetCopy();")
            .AppendLine("       }")
            .AppendLine("   }")
            .AppendLine("}")

            '基本情報セット
            .AppendLine("function fncDisable()")
            .AppendLine("{")
            .AppendLine("   var btnSearchTyousaKaisya = document.getElementById('" & Me.btnSearchTyousaKaisya.ClientID & "')")
            .AppendLine("   var btnSearch = document.getElementById('" & Me.btnSearch.ClientID & "')")
            .AppendLine("   var btnClear = document.getElementById('" & Me.btnClear.ClientID & "')")
            .AppendLine("   var btnSyuusei = document.getElementById('" & Me.btnSyuusei.ClientID & "')")
            .AppendLine("   var btnTouroku = document.getElementById('" & Me.btnTouroku.ClientID & "')")
            .AppendLine("   var btnClearMeisai = document.getElementById('" & Me.btnClearMeisai.ClientID & "')")
            .AppendLine("   var btnKensakuYuubinNo = document.getElementById('" & Me.btnKensakuYuubinNo.ClientID & "')")
            .AppendLine("   var btnKensakuFCTen = document.getElementById('" & Me.btnKensakuFCTen.ClientID & "')")
            ' .AppendLine("   var btnKensakuKensaCenter = document.getElementById('" & Me.btnKensakuKensaCenter.ClientID & "')")
            .AppendLine("   var btnKensakuSeikyuuSaki = document.getElementById('" & Me.btnKensakuSeikyuuSaki.ClientID & "')")
            .AppendLine("   var btnKensakuSeikyuuSyousai = document.getElementById('" & Me.btnKensakuSeikyuuSyousai.ClientID & "')")
            .AppendLine("   var btnKensakuSeikyuuSoufuCopy = document.getElementById('" & Me.btnKensakuSeikyuuSoufuCopy.ClientID & "')")
            .AppendLine("   var btnKensakuSeikyuuSyo = document.getElementById('" & Me.btnKensakuSeikyuuSyo.ClientID & "')")
            .AppendLine("   var btnKensakuSkkShriSaki = document.getElementById('" & Me.btnKensakuSkkShriSaki.ClientID & "')")
            .AppendLine("   var btnKensakuShriJigyousyo = document.getElementById('" & Me.btnKensakuShriJigyousyo.ClientID & "')")
            .AppendLine("   var btnKensakuShriMeisaiJigyousyo = document.getElementById('" & Me.btnKensakuShriMeisaiJigyousyo.ClientID & "')")

            .AppendLine("   var btnOK = document.getElementById('" & Me.btnOK.ClientID & "')")
            .AppendLine("   var btnNO = document.getElementById('" & Me.btnNO.ClientID & "')")

            .AppendLine("   var my_array = new Array(17);")
            .AppendLine("   my_array[0] = btnSearchTyousaKaisya;")
            .AppendLine("   my_array[1] = btnSearch;")
            .AppendLine("   my_array[2] = btnClear;")
            .AppendLine("   my_array[3] = btnSyuusei;")
            .AppendLine("   my_array[4] = btnTouroku;")
            .AppendLine("   my_array[5] = btnClearMeisai;")
            .AppendLine("   my_array[6] = btnKensakuYuubinNo;")
            .AppendLine("   my_array[7] = btnKensakuFCTen;")
            '.AppendLine("   my_array[8] = btnKensakuKensaCenter;")
            .AppendLine("   my_array[9] = btnKensakuSeikyuuSaki;")
            .AppendLine("   my_array[10] = btnKensakuSeikyuuSyousai;")
            .AppendLine("   my_array[11] = btnKensakuSeikyuuSoufuCopy;")
            .AppendLine("   my_array[12] = btnKensakuSeikyuuSyo;")
            .AppendLine("   my_array[13] = btnKensakuSkkShriSaki;")
            .AppendLine("   my_array[14] = btnKensakuShriJigyousyo;")
            .AppendLine("   my_array[15] = btnKensakuShriMeisaiJigyousyo;")

            .AppendLine("   my_array[16] = btnOK;")
            .AppendLine("   my_array[17] = btnNO;")

            .AppendLine("   for (i = 0; i < 18; i++){")
            .AppendLine("       if(i != 8){my_array[i].disabled = true;}")
            .AppendLine("   }")
            .AppendLine("}")

            .AppendLine("function disableButton1()")
            .AppendLine("{")
            .AppendLine("   window.setTimeout('fncDisable()',0);")
            .AppendLine("   return true;")
            .AppendLine("}")

            '======================2011/06/27 車龍 追加 開始↓=================================
            .AppendLine("function fncSeikyuusakiChangeCheck() ")
            .AppendLine("{ ")
            .AppendLine("   var tbxSeikyuuSakiCd = document.getElementById('" & Me.tbxSeikyuuSakiCd.ClientID & "'); ")
            .AppendLine("   var tbxSeikyuuSakiBrc = document.getElementById('" & Me.tbxSeikyuuSakiBrc.ClientID & "'); ")
            .AppendLine("   var ddlSeikyuuSaki = document.getElementById('" & Me.ddlSeikyuuSaki.ClientID & "'); ")
            .AppendLine("   var hidSeikyuuSakiCd = document.getElementById('" & Me.hidSeikyuuSakiCd.ClientID & "'); ")
            .AppendLine("   var hidSeikyuuSakiBrc = document.getElementById('" & Me.hidSeikyuuSakiBrc.ClientID & "'); ")
            .AppendLine("   var hidSeikyuuSakiKbn = document.getElementById('" & Me.hidSeikyuuSakiKbn.ClientID & "'); ")
            .AppendLine("   if((tbxSeikyuuSakiCd.value!=hidSeikyuuSakiCd.value)||(tbxSeikyuuSakiBrc.value!=hidSeikyuuSakiBrc.value)||(ddlSeikyuuSaki.value!=hidSeikyuuSakiKbn.value)) ")
            .AppendLine("   { ")
            .AppendLine("       if(confirm('請求先が変更されています。\r\n請求先情報をクリアしますがよろしいですか？')) ")
            .AppendLine("       { ")
            .AppendLine("           document.getElementById('" & Me.tbxSeikyuuSakiShriSakiMei.ClientID & "').value = ''; ")
            .AppendLine("           document.getElementById('" & Me.tbxSeikyuuSakiShriSakiKana.ClientID & "').value = ''; ")
            .AppendLine("           document.getElementById('" & Me.tbxSkysySoufuYuubinNo.ClientID & "').value = ''; ")
            .AppendLine("           document.getElementById('" & Me.tbxSkysySoufuJyuusyo1.ClientID & "').value = ''; ")
            .AppendLine("           document.getElementById('" & Me.tbxSkysySoufuTelNo.ClientID & "').value = ''; ")
            .AppendLine("           document.getElementById('" & Me.tbxSkysySoufuJyuusyo2.ClientID & "').value = ''; ")
            .AppendLine("           document.getElementById('" & Me.tbxShriYouFaxNo.ClientID & "').value = ''; ")
            .AppendLine("           return true; ")
            .AppendLine("       } ")
            .AppendLine("       else ")
            .AppendLine("       { ")
            .AppendLine("           tbxSeikyuuSakiCd.value = hidSeikyuuSakiCd.value; ")
            .AppendLine("           tbxSeikyuuSakiBrc.value = hidSeikyuuSakiBrc.value ")
            .AppendLine("           ddlSeikyuuSaki.value = hidSeikyuuSakiKbn.value ")
            .AppendLine("           return false; ")
            .AppendLine("       } ")
            .AppendLine("   } ")
            .AppendLine("   else ")
            .AppendLine("   { ")
            .AppendLine("       return true; ")
            .AppendLine("   } ")
            .AppendLine("} ")
            '======================2011/06/27 車龍 追加 終了↑=================================
            .AppendLine("</script>")
        End With
        ClientScript.RegisterClientScriptBlock(csType, csName, csScript.ToString)
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
        Dim dtTyousaKaisyaDataSet As New Itis.Earth.DataAccess.TyousaKaisyaDataSet.m_tyousakaisyaDataTable
        dtTyousaKaisyaDataSet = TyousaKaisyaMasterBL.SelMTyousaKaisyaInfo(TyousaKaisya_Cd, TyousaKaisyaCd, JigyousyoCd, btn)

        If dtTyousaKaisyaDataSet.Rows.Count = 1 Then
            With dtTyousaKaisyaDataSet.Item(0)
                '取消
                chkTorikesi.Checked = IIf(.torikesi = "0", False, True)
                '調査会社コード
                tbxTyousaKaisyaCd.Text = TrimNull(.tys_kaisya_cd)
                '事務所コード
                tbxJigyousyoCd.Text = TrimNull(.jigyousyo_cd)

                '取消理由
                tbxTorikesiRiyuu.Text = TrimNull(.torikesi_riyuu)
                If chkTorikesi.Checked = True Then
                    Me.tbxTorikesiRiyuu.Attributes.Remove("readonly")
                    Me.hidTorikesi.Value = "true"
                Else
                    Me.tbxTorikesiRiyuu.Attributes.Add("readonly", "true;")
                    Me.hidTorikesi.Value = "false"
                End If

                '調査会社名
                tbxTyousaKaisyaMei.Text = TrimNull(.tys_kaisya_mei)
                If btn = "btnSearch" Then
                    tbxTyousaKaisya_Mei.Text = TrimNull(.tys_kaisya_mei)
                    tbxTyousaKaisya_Cd.Text = TrimNull(.tys_kaisya_cd).ToUpper & TrimNull(.jigyousyo_cd)
                End If

                '調査会社名カナ
                tbxTyousaKaisyaMeiKana.Text = TrimNull(.tys_kaisya_mei_kana)
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
                'SS基準価格
                tbxSSKijyunKkk.Text = AddComa(.ss_kijyun_kkk)
                '調査業務
                SetDropSelect(ddlTyousaGyoumu, TrimNull(.tys_kaisya_flg))
                '工事業務
                SetDropSelect(ddlKoujiGyoumu, TrimNull(.koj_kaisya_flg))
                'FC店
                tbxFCTen.Text = TrimNull(.fc_ten_cd)
                tbxFCTenMei.Text = TrimNull(.keiretu_mei)
                '==============2012/04/12 車龍 405738 削除↓====================
                ''ＦＣ店区分
                'SetDropSelect(ddlFCTenKbn, TrimNull(.fc_ten_kbn))
                ''ＦＣ 入会年月
                'tbxFCNyuukaiDate.Text = toYYYYMM(.fc_nyuukai_date)
                ''ＦＣ 退会年月
                'tbxFCTaikaiDate.Text = toYYYYMM(.fc_taikai_date)
                '==============2012/04/12 車龍 405738 削除↑====================
                'ＪＡＰＡＮ会区分
                SetDropSelect(ddlJapanKbn, TrimNull(.japan_kai_kbn))
                'ＪＡＰＡＮ会 入会年月
                tbxJapanKaiNyuukaiDate.Text = toYYYYMM(.japan_kai_nyuukai_date)
                'ＪＡＰＡＮ会 退会年月
                tbxJapanKaiTaikaiDate.Text = toYYYYMM(.japan_kai_taikai_date)

                btxZenjyuhinHosoku.Text = TrimNull(.zenjyuhin_hosoku)
                '宅地地盤調査主任資格
                SetDropSelect(ddlTktJbnTysSyuninSkkFlg, TrimNull(.tkt_jbn_tys_syunin_skk_flg))
                'Ｒ－ＪＨＳトークン
                SetDropSelect(ddlRJhsTokenFlg, TrimNull(.report_jhs_token_flg))
                '工事報告書直送
                SetDropSelect(ddlKojHkksTyokusouFlg, TrimNull(.koj_hkks_tyokusou_flg))
                hidKojHkksTyokusouFlg.Value = TrimNull(.koj_hkks_tyokusou_flg)

                '工事報告書直送変更ログインユーザ
                tbxKojHkksTyokusouUpdLoginUserId.Text = TrimNull(.koj_hkks_tyokusou_upd_login_user_id)
                '工事報告書直送変更日時
                tbxKojHkksTyokusouUpdDatetime.Text = toYYYYMMDDHH(.koj_hkks_tyokusou_upd_datetime)

                '建物検査センター
                'tbxKensakuKensaCenter.Text = TrimNull(.kensa_center_cd)
                hidKensakuKensaCenter.Value = TrimNull(.kensa_center_cd)

                '建物検査センター名
                'tbxKensakuKensaCenterMei.Text = TrimNull(.fc_nm)

                '請求先区分
                SetDropSelect(ddlSeikyuuSaki, TrimNull(.seikyuu_saki_kbn))
                '請求先コード
                tbxSeikyuuSakiCd.Text = TrimNull(.seikyuu_saki_cd).ToUpper
                '請求先枝番
                tbxSeikyuuSakiBrc.Text = TrimNull(.seikyuu_saki_brc).ToUpper
                '請求先名
                tbxSeikyuuSakiMei.Text = TrimNull(.seikyuu_saki_mei)
                '請求先コード
                hidSeikyuuSakiCd.Value = TrimNull(.seikyuu_saki_cd).ToUpper
                '請求先枝番
                hidSeikyuuSakiBrc.Value = TrimNull(.seikyuu_saki_brc).ToUpper
                '請求先区分
                hidSeikyuuSakiKbn.Value = TrimNull(.seikyuu_saki_kbn)

                '請求先支払先名
                tbxSeikyuuSakiShriSakiMei.Text = TrimNull(.seikyuu_saki_shri_saki_mei)
                '請求先支払先名カナ
                tbxSeikyuuSakiShriSakiKana.Text = TrimNull(.seikyuu_saki_shri_saki_kana)
                '請求書送付先郵便番号
                tbxSkysySoufuYuubinNo.Text = TrimNull(.skysy_soufu_yuubin_no)
                '請求書送付先住所１
                tbxSkysySoufuJyuusyo1.Text = TrimNull(.skysy_soufu_jyuusyo1)
                '請求書送付先電話番号
                tbxSkysySoufuTelNo.Text = TrimNull(.skysy_soufu_tel_no)
                '請求書送付先住所２
                tbxSkysySoufuJyuusyo2.Text = TrimNull(.skysy_soufu_jyuusyo2)
                '支払用FAX番号
                tbxShriYouFaxNo.Text = TrimNull(.shri_you_fax_no)

                '新会計支払先事業所コード
                tbxSkkJigyousyoCd.Text = TrimNull(.skk_jigyousyo_cd)
                hidSkkJigyousyoCd.Value = TrimNull(.skk_jigyousyo_cd)

                'SAP用仕入
                Me.tbxSiireSaki.Text = TrimNull(.a1_lifnr)
                Me.tbxSiireSakiMei.Text = TrimNull(.a1_a_zz_sort)


                '新会計支払先コード
                tbxSkkShriSakiCd.Text = TrimNull(.skk_shri_saki_cd)
                hidSkkShriSakiCd.Value = TrimNull(.skk_shri_saki_cd)

                '新会計支払先名
                tbxSkkShriSakiMei.Text = TrimNull(.shri_saki_mei_kanji)
                '支払締め日
                tbxShriSimeDate.Text = TrimNull(.shri_sime_date)
                '支払予定月数
                tbxShriYoteiGessuu.Text = TrimNull(.shri_yotei_gessuu)
                'ファクタリング開始年月
                tbxFctringKaisiNengetu.Text = toYYYYMM(.fctring_kaisi_nengetu)
                '支払集計先事業所
                tbxTysKaisyaCd.Text = TrimNull(.tys_kaisya_cd)
                '支払集計先事業所
                tbxShriJigyousyoCd.Text = TrimNull(.shri_jigyousyo_cd)
                hidShriJigyousyoCd.Value = TrimNull(.shri_jigyousyo_cd)

                '支払集計先事業所名
                tbxTysKaisyaMei.Text = TrimNull(.shri_kaisya_mei)
                '支払明細集計先事業所
                tbxTysMeisaiKaisyaCd.Text = TrimNull(.tys_kaisya_cd)
                '支払明細集計先事業所コード
                tbxShriMeisaiJigyousyoCd.Text = TrimNull(.shri_meisai_jigyousyo_cd)
                hidShriMeisaiJigyousyoCd.Value = TrimNull(.shri_meisai_jigyousyo_cd)

                '支払明細集計先事業所名
                tbxTysMeisaiKaisyaMei.Text = TrimNull(.shri_meisai_kaisya_mei)

                '============2012/04/12 車龍 405721 追加↓==========================
                '代表者名
                Me.tbxDaihyousyaMei.Text = TrimNull(.daihyousya_mei)
                '役職名
                Me.tbxYasyokuMei.Text = TrimNull(.yakusyoku_mei)
                '============2012/04/12 車龍 405721 追加↑==========================

                '2013/11/04 李宇追加 ↓
                'SDS保持情報
                SetDropSelect(Me.ddlSdsJyouhou, TrimNull(.sds_hoji_info))
                'SDS台数情報
                Me.tbxSdsKiki.Text = TrimNull(.sds_daisuu_info)
                '2013/11/04 李宇追加 ↑

                If TrimNull(.jituzai_flg) = "1" Then
                    ddlJituzaiFlg.SelectedIndex = 1
                Else
                    ddlJituzaiFlg.SelectedIndex = 0
                End If

                hidUPDTime.Value = TrimNull(.upd_datetime)
                UpdatePanelA.Update()
            End With
            If Not blnBtn Then
                btnSyuusei.Enabled = False
                btnTouroku.Enabled = False
            Else
                btnSyuusei.Enabled = True
                btnTouroku.Enabled = False
            End If
            '新会計事業所コード・新会計支払先コードの権限設定
            If blnBtn1 And Not blnBtn2 Then
                tbxSkkJigyousyoCd.Enabled = False
                tbxSkkShriSakiCd.Enabled = False
                btnKensakuSkkShriSaki.Enabled = False
            Else
                tbxSkkJigyousyoCd.Enabled = True
                tbxSkkShriSakiCd.Enabled = True
                btnKensakuSkkShriSaki.Enabled = True
            End If
            tbxTyousaKaisyaCd.Attributes.Add("readonly", "true;")
            tbxJigyousyoCd.Attributes.Add("readonly", "true;")
        Else
            MeisaiClear()
            strErr = "alert('" & Messages.Instance.MSG2034E & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strErr, True)
            If btn <> "btnSearch" Then
                btnSyuusei.Enabled = False
                btnTouroku.Enabled = True
                tbxTyousaKaisyaCd.Attributes.Remove("readonly")
                tbxJigyousyoCd.Attributes.Remove("readonly")
            End If
            tbxTyousaKaisya_Mei.Text = ""
        End If

        labSeikyuuSaki.Style.Add("display", "none")
        labHyouji.Style.Add("display", "none")
        hidConfirm2.Value = ""

        If btn = "btnSearch" Or btn = "btnSyuusei" Or btn = "btnTouroku" Then
            tbxSeikyuuSakiShriSakiMei.Attributes.Remove("readonly")
            tbxSeikyuuSakiShriSakiKana.Attributes.Remove("readonly")
            tbxSkysySoufuYuubinNo.Attributes.Remove("readonly")
            tbxSkysySoufuJyuusyo1.Attributes.Remove("readonly")
            tbxSkysySoufuTelNo.Attributes.Remove("readonly")
            tbxSkysySoufuJyuusyo2.Attributes.Remove("readonly")
            tbxShriYouFaxNo.Attributes.Remove("readonly")
            btnKensakuSeikyuuSoufuCopy.Enabled = True
            btnKensakuSeikyuuSyo.Enabled = True
        End If

        hidSyousai.Value = ""
    End Sub

    ''' <summary>
    ''' 登録と修正値を持ち
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SetMeisaiData() As Itis.Earth.DataAccess.TyousaKaisyaDataSet.m_tyousakaisyaDataTable
        Dim dtTyousaKaisyaDataSet As New Itis.Earth.DataAccess.TyousaKaisyaDataSet.m_tyousakaisyaDataTable

        dtTyousaKaisyaDataSet.Rows.Add(dtTyousaKaisyaDataSet.NewRow)
        '取消
        dtTyousaKaisyaDataSet.Item(0).torikesi = IIf(chkTorikesi.Checked, "1", "0")
        '調査会社コード
        dtTyousaKaisyaDataSet.Item(0).tys_kaisya_cd = tbxTyousaKaisyaCd.Text.ToUpper
        '事務所コード
        dtTyousaKaisyaDataSet.Item(0).jigyousyo_cd = tbxJigyousyoCd.Text.ToUpper
        '取消理由
        dtTyousaKaisyaDataSet.Item(0).torikesi_riyuu = tbxTorikesiRiyuu.Text
        '調査会社名
        dtTyousaKaisyaDataSet.Item(0).tys_kaisya_mei = tbxTyousaKaisyaMei.Text
        '調査会社名カナ
        dtTyousaKaisyaDataSet.Item(0).tys_kaisya_mei_kana = tbxTyousaKaisyaMeiKana.Text
        '郵便番号
        dtTyousaKaisyaDataSet.Item(0).yuubin_no = tbxYuubinNo.Text
        '住所１
        dtTyousaKaisyaDataSet.Item(0).jyuusyo1 = tbxJyuusyo1.Text
        '電話番号
        dtTyousaKaisyaDataSet.Item(0).tel_no = tbxTelNo.Text
        '住所２
        dtTyousaKaisyaDataSet.Item(0).jyuusyo2 = tbxJyuusyo2.Text
        'FAX番号
        dtTyousaKaisyaDataSet.Item(0).fax_no = tbxFaxNo.Text
        'SS基準価格
        dtTyousaKaisyaDataSet.Item(0).ss_kijyun_kkk = Replace(tbxSSKijyunKkk.Text, ",", "")
        '調査業務
        dtTyousaKaisyaDataSet.Item(0).tys_kaisya_flg = ddlTyousaGyoumu.SelectedValue
        '工事業務
        dtTyousaKaisyaDataSet.Item(0).koj_kaisya_flg = ddlKoujiGyoumu.SelectedValue
        'FC店
        dtTyousaKaisyaDataSet.Item(0).fc_ten_cd = tbxFCTen.Text
        dtTyousaKaisyaDataSet.Item(0).keiretu_mei = tbxFCTenMei.Text
        '==============2012/04/12 車龍 405738 削除↓====================
        ''ＦＣ店区分
        'dtTyousaKaisyaDataSet.Item(0).fc_ten_kbn = ddlFCTenKbn.SelectedValue
        ''ＦＣ 入会年月
        'dtTyousaKaisyaDataSet.Item(0).fc_nyuukai_date = tbxFCNyuukaiDate.Text
        ''ＦＣ 退会年月
        'dtTyousaKaisyaDataSet.Item(0).fc_taikai_date = tbxFCTaikaiDate.Text
        '==============2012/04/12 車龍 405738 削除↑====================
        'ＪＡＰＡＮ会区分
        dtTyousaKaisyaDataSet.Item(0).japan_kai_kbn = ddlJapanKbn.SelectedValue
        'ＪＡＰＡＮ会 入会年月
        dtTyousaKaisyaDataSet.Item(0).japan_kai_nyuukai_date = tbxJapanKaiNyuukaiDate.Text
        'ＪＡＰＡＮ会 退会年月
        dtTyousaKaisyaDataSet.Item(0).japan_kai_taikai_date = tbxJapanKaiTaikaiDate.Text

        dtTyousaKaisyaDataSet.Item(0).zenjyuhin_hosoku = btxZenjyuhinHosoku.Text

        '宅地地盤調査主任資格
        dtTyousaKaisyaDataSet.Item(0).tkt_jbn_tys_syunin_skk_flg = ddlTktJbnTysSyuninSkkFlg.SelectedValue
        'Ｒ－ＪＨＳトークン
        dtTyousaKaisyaDataSet.Item(0).report_jhs_token_flg = ddlRJhsTokenFlg.SelectedValue

        '工事報告書直送
        dtTyousaKaisyaDataSet.Item(0).koj_hkks_tyokusou_flg = ddlKojHkksTyokusouFlg.SelectedValue
        '工事報告書直送変更ログインユーザ
        dtTyousaKaisyaDataSet.Item(0).koj_hkks_tyokusou_upd_login_user_id = tbxKojHkksTyokusouUpdLoginUserId.Text
        '工事報告書直送変更日時
        dtTyousaKaisyaDataSet.Item(0).koj_hkks_tyokusou_upd_datetime = tbxKojHkksTyokusouUpdDatetime.Text

        '建物検査センター
        'dtTyousaKaisyaDataSet.Item(0).kensa_center_cd = tbxKensakuKensaCenter.Text
        ''建物検査センター名
        'dtTyousaKaisyaDataSet.Item(0).fc_nm = tbxKensakuKensaCenterMei.Text
        '請求先区分
        dtTyousaKaisyaDataSet.Item(0).seikyuu_saki_kbn = ddlSeikyuuSaki.SelectedValue
        '請求先コード
        dtTyousaKaisyaDataSet.Item(0).seikyuu_saki_cd = tbxSeikyuuSakiCd.Text.ToUpper
        '請求先枝番
        dtTyousaKaisyaDataSet.Item(0).seikyuu_saki_brc = tbxSeikyuuSakiBrc.Text.ToUpper
        '請求先名
        dtTyousaKaisyaDataSet.Item(0).seikyuu_saki_mei = tbxSeikyuuSakiMei.Text
        '請求先支払先名
        dtTyousaKaisyaDataSet.Item(0).seikyuu_saki_shri_saki_mei = tbxSeikyuuSakiShriSakiMei.Text
        '請求先支払先名カナ
        dtTyousaKaisyaDataSet.Item(0).seikyuu_saki_shri_saki_kana = tbxSeikyuuSakiShriSakiKana.Text
        '請求書送付先郵便番号
        dtTyousaKaisyaDataSet.Item(0).skysy_soufu_yuubin_no = tbxSkysySoufuYuubinNo.Text
        '請求書送付先住所１
        dtTyousaKaisyaDataSet.Item(0).skysy_soufu_jyuusyo1 = tbxSkysySoufuJyuusyo1.Text
        '請求書送付先電話番号
        dtTyousaKaisyaDataSet.Item(0).skysy_soufu_tel_no = tbxSkysySoufuTelNo.Text
        '請求書送付先住所２
        dtTyousaKaisyaDataSet.Item(0).skysy_soufu_jyuusyo2 = tbxSkysySoufuJyuusyo2.Text
        '支払用FAX番号
        dtTyousaKaisyaDataSet.Item(0).shri_you_fax_no = tbxShriYouFaxNo.Text
        '※新会計支払先
        dtTyousaKaisyaDataSet.Item(0).skk_jigyousyo_cd = tbxSkkJigyousyoCd.Text
        '新会計支払先コード
        dtTyousaKaisyaDataSet.Item(0).skk_shri_saki_cd = tbxSkkShriSakiCd.Text
        '新会計支払先名
        dtTyousaKaisyaDataSet.Item(0).shri_saki_mei_kanji = tbxSkkShriSakiMei.Text
        '支払締め日
        dtTyousaKaisyaDataSet.Item(0).shri_sime_date = tbxShriSimeDate.Text
        '支払予定月数
        dtTyousaKaisyaDataSet.Item(0).shri_yotei_gessuu = tbxShriYoteiGessuu.Text
        'ファクタリング開始年月
        dtTyousaKaisyaDataSet.Item(0).fctring_kaisi_nengetu = tbxFctringKaisiNengetu.Text
        '※支払集計先事業所コード
        dtTyousaKaisyaDataSet.Item(0).shri_jigyousyo_cd = tbxShriJigyousyoCd.Text
        '支払明細集計先事業所コード
        dtTyousaKaisyaDataSet.Item(0).shri_meisai_jigyousyo_cd = tbxShriMeisaiJigyousyoCd.Text

        'SAP用仕入先
        dtTyousaKaisyaDataSet.Item(0).a1_lifnr = tbxSiireSaki.Text
        dtTyousaKaisyaDataSet.Item(0).a1_a_zz_sort = tbxSiireSakiMei.Text


        ''支払明細集計先事業所名
        'dtTyousaKaisyaDataSet.Item(0).tys_kaisya_mei = tbxTysMeisaiKaisyaMei.Text
        '============2012/04/12 車龍 405721 追加↓==========================
        With dtTyousaKaisyaDataSet.Item(0)
            '代表者名
            .daihyousya_mei = Me.tbxDaihyousyaMei.Text.Trim
            '役職名
            .yakusyoku_mei = Me.tbxYasyokuMei.Text.Trim
        End With
        '============2012/04/12 車龍 405721 追加↑==========================

        '2013/11/04 李宇追加 ↓
        With dtTyousaKaisyaDataSet.Item(0)
            'SDS保持情報
            .sds_hoji_info = Me.ddlSdsJyouhou.SelectedValue
            'SDS台数情報
            .sds_daisuu_info = Me.tbxSdsKiki.Text.Trim
        End With
        '2013/11/04 李宇追加 ↑
        If ddlJituzaiFlg.SelectedIndex = 1 Then
            dtTyousaKaisyaDataSet.Rows(0).Item("jituzai_flg") = "1"
        Else
            dtTyousaKaisyaDataSet.Rows(0).Item("jituzai_flg") = DBNull.Value
        End If


        dtTyousaKaisyaDataSet.Item(0).upd_login_user_id = ViewState("UserId")
        dtTyousaKaisyaDataSet.Item(0).add_login_user_id = ViewState("UserId")
        dtTyousaKaisyaDataSet.Item(0).upd_datetime = hidUPDTime.Value

        Return dtTyousaKaisyaDataSet
    End Function

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
        '調査会社
        tbxTyousaKaisya_Cd.Text = ""
        '調査会社名
        tbxTyousaKaisya_Mei.Text = ""
        'MeisaiClear()
    End Sub

    ''' <summary>
    ''' 請求先名・送付住所にコピー
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnKensakuSeikyuuSoufuCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKensakuSeikyuuSoufuCopy.Click
        tbxSeikyuuSakiShriSakiMei.Text = tbxTyousaKaisyaMei.Text
        tbxSeikyuuSakiShriSakiKana.Text = tbxTyousaKaisyaMeiKana.Text
        tbxSkysySoufuYuubinNo.Text = tbxYuubinNo.Text
        tbxSkysySoufuJyuusyo1.Text = tbxJyuusyo1.Text
        tbxSkysySoufuTelNo.Text = tbxTelNo.Text
        tbxSkysySoufuJyuusyo2.Text = tbxJyuusyo2.Text
        tbxShriYouFaxNo.Text = tbxFaxNo.Text
    End Sub

    ''' <summary>
    ''' 明細項目クリア
    ''' </summary>
    ''' <remarks></remarks>
    Sub MeisaiClear()
        '取消
        chkTorikesi.Checked = False
        '調査会社コード
        tbxTyousaKaisyaCd.Text = ""
        '事務所コード
        tbxJigyousyoCd.Text = ""
        '取消理由
        tbxTorikesiRiyuu.Text = ""
        tbxTorikesiRiyuu.Attributes.Add("readonly", "true;")
        '調査会社名
        tbxTyousaKaisyaMei.Text = ""
        '調査会社名カナ
        tbxTyousaKaisyaMeiKana.Text = ""
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
        'SS基準価格
        tbxSSKijyunKkk.Text = ""
        'FC店
        tbxFCTen.Text = ""
        'FC店名
        tbxFCTenMei.Text = ""
        '==============2012/04/12 車龍 405738 削除↓====================
        ''ＦＣ 入会年月
        'tbxFCNyuukaiDate.Text = ""
        ''ＦＣ 退会年月
        'tbxFCTaikaiDate.Text = ""
        '==============2012/04/12 車龍 405738 削除↑====================
        'ＪＡＰＡＮ会 入会年月
        tbxJapanKaiNyuukaiDate.Text = ""
        'ＪＡＰＡＮ会 退会年月
        tbxJapanKaiTaikaiDate.Text = ""

        btxZenjyuhinHosoku.Text = ""
        '工事報告書直送変更ログインユーザ
        tbxKojHkksTyokusouUpdLoginUserId.Text = ""
        '工事報告書直送変更日時
        tbxKojHkksTyokusouUpdDatetime.Text = ""
        '建物検査センター
        'tbxKensakuKensaCenter.Text = ""
        ''建物検査センター名
        'tbxKensakuKensaCenterMei.Text = ""
        '請求先
        tbxSeikyuuSakiCd.Text = ""
        '請求先枝番
        tbxSeikyuuSakiBrc.Text = ""
        '請求先名
        tbxSeikyuuSakiMei.Text = ""
        '請求先支払先名
        tbxSeikyuuSakiShriSakiMei.Text = ""
        '請求先支払先名カナ
        tbxSeikyuuSakiShriSakiKana.Text = ""
        '請求書送付先郵便番号
        tbxSkysySoufuYuubinNo.Text = ""
        '請求書送付先住所１
        tbxSkysySoufuJyuusyo1.Text = ""
        '請求書送付先電話番号
        tbxSkysySoufuTelNo.Text = ""
        '請求書送付先住所２
        tbxSkysySoufuJyuusyo2.Text = ""
        '支払用FAX番号
        tbxShriYouFaxNo.Text = ""
        '新会計支払先
        tbxSkkJigyousyoCd.Text = "YMP8"
        '新会計支払先
        tbxSkkShriSakiCd.Text = ""
        '新会計支払先
        tbxSkkShriSakiMei.Text = ""
        '支払締め日
        tbxShriSimeDate.Text = ""
        '支払予定月数
        tbxShriYoteiGessuu.Text = ""
        'ファクタリング開始年月
        tbxFctringKaisiNengetu.Text = ""
        '支払集計先事業所
        tbxTysKaisyaCd.Text = ""
        '支払集計先事業所
        tbxShriJigyousyoCd.Text = ""
        '支払集計先事業所
        tbxTysKaisyaMei.Text = ""
        '支払明細集計先事業所
        tbxTysMeisaiKaisyaCd.Text = ""
        '支払明細集計先事業所
        tbxShriMeisaiJigyousyoCd.Text = ""
        '支払明細集計先事業所
        tbxTysMeisaiKaisyaMei.Text = ""

        '調査業務
        SetDropSelect(ddlTyousaGyoumu, "")
        '工事業務
        SetDropSelect(ddlKoujiGyoumu, "")
        '==============2012/04/12 車龍 405738 削除↓====================
        ''ＦＣ店区分
        'SetDropSelect(ddlFCTenKbn, "")
        '==============2012/04/12 車龍 405738 削除↑====================
        'ＪＡＰＡＮ会区分
        SetDropSelect(ddlJapanKbn, "")
        '宅地地盤調査主任資格
        SetDropSelect(ddlTktJbnTysSyuninSkkFlg, "")
        'Ｒ－ＪＨＳトークン
        '=================2011/07/14 車龍 修正 開始↓=========================
        'SetDropSelect(ddlRJhsTokenFlg, "")
        SetDropSelect(ddlRJhsTokenFlg, "1")
        '=================2011/07/14 車龍 修正 開始↓=========================
        '工事報告書直送
        SetDropSelect(ddlKojHkksTyokusouFlg, "")
        '請求先
        SetDropSelect(ddlSeikyuuSaki, "")
        '============2012/04/12 車龍 405721 追加↓==========================
        '代表者名
        Me.tbxDaihyousyaMei.Text = String.Empty
        '役職名
        Me.tbxYasyokuMei.Text = String.Empty
        '============2012/04/12 車龍 405721 追加↑==========================

        Me.tbxSiireSaki.Text = ""
        Me.tbxSiireSakiMei.Text = ""


        '2013/11/04 李宇追加 ↓
        'SDS保持情報
        SetDropSelect(Me.ddlSdsJyouhou, "")
        'SDS機器台数
        Me.tbxSdsKiki.Text = String.Empty
        '2013/11/04 李宇追加↑

        ddlJituzaiFlg.SelectedIndex = 1

        'HIDDEN設定
        Me.hidSeikyuuSakiCd.Value = ""
        Me.hidSeikyuuSakiBrc.Value = ""
        Me.hidSeikyuuSakiKbn.Value = ""
        Me.hidKensakuKensaCenter.Value = ""
        Me.hidSkkJigyousyoCd.Value = ""
        Me.hidSkkShriSakiCd.Value = ""
        Me.hidShriJigyousyoCd.Value = ""
        Me.hidShriMeisaiJigyousyoCd.Value = ""
        Me.hidConfirm.Value = ""
        Me.hidTorikesi.Value = ""

        '請求先新規登録
        labSeikyuuSaki.Style.Add("display", "none")
        labHyouji.Style.Add("display", "none")

        tbxSeikyuuSakiShriSakiMei.Attributes.Remove("readonly")
        tbxSeikyuuSakiShriSakiKana.Attributes.Remove("readonly")
        tbxSkysySoufuYuubinNo.Attributes.Remove("readonly")
        tbxSkysySoufuJyuusyo1.Attributes.Remove("readonly")
        tbxSkysySoufuTelNo.Attributes.Remove("readonly")
        tbxSkysySoufuJyuusyo2.Attributes.Remove("readonly")
        tbxShriYouFaxNo.Attributes.Remove("readonly")
        btnKensakuSeikyuuSoufuCopy.Enabled = True
        btnKensakuSeikyuuSyo.Enabled = True

        hidUPDTime.Value = ""
        If Not blnBtn Then
            btnSyuusei.Enabled = False
            btnTouroku.Enabled = False
        Else
            btnSyuusei.Enabled = False
            btnTouroku.Enabled = True
        End If
        '新会計事業所コード・新会計支払先コードの権限設定
        If blnBtn1 And Not blnBtn2 Then
            tbxSkkJigyousyoCd.Enabled = False
            tbxSkkShriSakiCd.Enabled = False
            btnKensakuSkkShriSaki.Enabled = False
        Else
            tbxSkkJigyousyoCd.Enabled = True
            tbxSkkShriSakiCd.Enabled = True
            btnKensakuSkkShriSaki.Enabled = True
        End If


        tbxTyousaKaisyaCd.Attributes.Remove("readonly")
        tbxJigyousyoCd.Attributes.Remove("readonly")
        UpdatePanelA.Update()
    End Sub

    ''' <summary>
    ''' 入力チェック
    ''' </summary>
    ''' <param name="strErr">エラーメッセージ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function InputCheck(ByRef strErr As String) As String
        Dim strID As String = ""
        '調査会社コード
        If strErr = "" Then
            strErr = commoncheck.CheckHissuNyuuryoku(tbxTyousaKaisyaCd.Text, "調査会社コード")
            If strErr <> "" Then
                strID = tbxTyousaKaisyaCd.ClientID
            End If
        End If
        If strErr = "" And tbxTyousaKaisyaCd.Text <> "" Then
            strErr = commoncheck.ChkHankakuEisuuji(tbxTyousaKaisyaCd.Text, "調査会社コード")
            If strErr <> "" Then
                strID = tbxTyousaKaisyaCd.ClientID
            End If
        End If
        '事務所コード
        If strErr = "" Then
            strErr = commoncheck.CheckHissuNyuuryoku(tbxJigyousyoCd.Text, "事務所コード")
            If strErr <> "" Then
                strID = tbxJigyousyoCd.ClientID
            End If
        End If
        If strErr = "" And tbxJigyousyoCd.Text <> "" Then
            strErr = commoncheck.ChkHankakuEisuuji(tbxJigyousyoCd.Text, "事務所コード")
            If strErr <> "" Then
                strID = tbxJigyousyoCd.ClientID
            End If
        End If
        '取消理由
        If strErr = "" And tbxTorikesiRiyuu.Text <> "" Then
            '禁則文字
            strErr = commoncheck.CheckKinsoku(tbxTorikesiRiyuu.Text, "取消理由")
            If strErr <> "" Then
                strID = tbxTorikesiRiyuu.ClientID
            End If
        End If
        '調査会社名
        If strErr = "" And tbxTyousaKaisyaMei.Text <> "" Then
            strErr = commoncheck.CheckByte(tbxTyousaKaisyaMei.Text, 40, "調査会社名", kbn.ZENKAKU)
            If strErr <> "" Then
                strID = tbxTyousaKaisyaMei.ClientID
            End If
        End If
        If strErr = "" And tbxTyousaKaisyaMei.Text <> "" Then
            strErr = commoncheck.CheckKinsoku(tbxTyousaKaisyaMei.Text, "調査会社名")
            If strErr <> "" Then
                strID = tbxTyousaKaisyaMei.ClientID
            End If
        End If
        '調査会社名カナ
        If strErr = "" And tbxTyousaKaisyaMeiKana.Text <> "" Then
            strErr = commoncheck.CheckByte(tbxTyousaKaisyaMeiKana.Text, 20, "調査会社名カナ", kbn.ZENKAKU)
            If strErr <> "" Then
                strID = tbxTyousaKaisyaMeiKana.ClientID
            End If
        End If
        If strErr = "" And tbxTyousaKaisyaMeiKana.Text <> "" Then
            strErr = commoncheck.CheckKinsoku(tbxTyousaKaisyaMeiKana.Text, "調査会社名カナ")
            If strErr <> "" Then
                strID = tbxTyousaKaisyaMeiKana.ClientID
            End If
        End If
        '============2012/04/12 車龍 405721 追加↓==========================
        '代表者名
        If strErr = "" And Me.tbxDaihyousyaMei.Text.Trim <> "" Then
            strErr = commoncheck.CheckByte(Me.tbxDaihyousyaMei.Text.Trim, 20, "代表者名", kbn.ZENKAKU)
            If strErr <> "" Then
                strID = Me.tbxDaihyousyaMei.ClientID
            End If
        End If
        If strErr = "" And Me.tbxDaihyousyaMei.Text <> "" Then
            strErr = commoncheck.CheckKinsoku(Me.tbxDaihyousyaMei.Text.Trim, "代表者名")
            If strErr <> "" Then
                strID = Me.tbxDaihyousyaMei.ClientID
            End If
        End If

        '役職名
        If strErr = "" And Me.tbxYasyokuMei.Text.Trim <> "" Then
            strErr = commoncheck.CheckByte(Me.tbxYasyokuMei.Text.Trim, 20, "役職名", kbn.ZENKAKU)
            If strErr <> "" Then
                strID = Me.tbxYasyokuMei.ClientID
            End If
        End If
        If strErr = "" And Me.tbxYasyokuMei.Text <> "" Then
            strErr = commoncheck.CheckKinsoku(Me.tbxYasyokuMei.Text.Trim, "役職名")
            If strErr <> "" Then
                strID = Me.tbxYasyokuMei.ClientID
            End If
        End If
        '============2012/04/12 車龍 405721 追加↑==========================
        '郵便番号
        If strErr = "" And tbxYuubinNo.Text <> "" Then
            strErr = commoncheck.CheckHankakuHaifun(tbxYuubinNo.Text, "郵便番号", "1")
            If strErr <> "" Then
                strID = tbxYuubinNo.ClientID
            End If
        End If

        '2013/11/05 李宇追加 ↓
        'SDS機器台数
        If strErr = "" And Me.tbxSdsKiki.Text <> "" Then
            strErr = commoncheck.CheckHankaku(Me.tbxSdsKiki.Text, "SDS機器台数", "1")
            If strErr <> "" Then
                strErr = String.Format(Messages.Instance.MSG2006E, "SDS機器台数")
                strID = Me.tbxSdsKiki.ClientID
            End If
        End If
        '2013/11/05 李宇追加 ↑

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
        'SS基準価格
        If strErr = "" And tbxSSKijyunKkk.Text <> "" Then
            strErr = commoncheck.CheckNum(tbxSSKijyunKkk.Text, "SS基準価格", "1")
            If strErr <> "" Then
                strID = tbxSSKijyunKkk.ClientID
            End If
        End If
        'FC店コード
        If strErr = "" And tbxFCTen.Text <> "" Then
            strErr = commoncheck.ChkHankakuEisuuji(tbxFCTen.Text, "FC店コード")
            If strErr <> "" Then
                strID = tbxFCTen.ClientID
            End If
        End If
        '==============2012/04/12 車龍 405738 削除↓====================
        ''ＦＣ 入会年月
        'If strErr = "" And tbxFCNyuukaiDate.Text <> "" Then
        '    strErr = commoncheck.CheckYuukouHiduke(tbxFCNyuukaiDate.Text, "ＦＣ 入会年月")
        '    If strErr <> "" Then
        '        strID = tbxFCNyuukaiDate.ClientID
        '    End If
        'End If
        ''ＦＣ 退会年月
        'If strErr = "" And tbxFCTaikaiDate.Text <> "" Then
        '    strErr = commoncheck.CheckYuukouHiduke(tbxFCTaikaiDate.Text, "ＦＣ 退会年月")
        '    If strErr <> "" Then
        '        strID = tbxFCTaikaiDate.ClientID
        '    End If
        'End If
        '==============2012/04/12 車龍 405738 削除↑====================
        'ＪＡＰＡＮ会 入会年月
        If strErr = "" And tbxJapanKaiNyuukaiDate.Text <> "" Then
            strErr = commoncheck.CheckYuukouHiduke(tbxJapanKaiNyuukaiDate.Text, "ＪＡＰＡＮ会 入会年月")
            If strErr <> "" Then
                strID = tbxJapanKaiNyuukaiDate.ClientID
            End If
        End If
        'ＪＡＰＡＮ会 退会年月
        If strErr = "" And tbxJapanKaiTaikaiDate.Text <> "" Then
            strErr = commoncheck.CheckYuukouHiduke(tbxJapanKaiTaikaiDate.Text, "ＪＡＰＡＮ会 退会年月")
            If strErr <> "" Then
                strID = tbxJapanKaiTaikaiDate.ClientID
            End If
        End If

        If strErr = "" And btxZenjyuhinHosoku.Text <> "" Then
            strErr = commoncheck.CheckByte(btxZenjyuhinHosoku.Text, 80, "全住品区分補足", kbn.ZENKAKU)
            If strErr <> "" Then
                strID = btxZenjyuhinHosoku.ClientID
            End If
        End If

        '建物検査センターコード
        'If strErr = "" And tbxKensakuKensaCenter.Text <> "" Then
        '    strErr = commoncheck.ChkHankakuEisuuji(tbxKensakuKensaCenter.Text, "建物検査センターコード")
        '    If strErr <> "" Then
        '        strID = tbxKensakuKensaCenter.ClientID
        '    End If
        'End If
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
        '請求先支払先名
        If strErr = "" And tbxSeikyuuSakiShriSakiMei.Text <> "" Then
            strErr = commoncheck.CheckByte(tbxSeikyuuSakiShriSakiMei.Text, 80, "請求先支払先名", kbn.ZENKAKU)
            If strErr <> "" Then
                strID = tbxSeikyuuSakiShriSakiMei.ClientID
            End If
        End If
        If strErr = "" And tbxSeikyuuSakiShriSakiMei.Text <> "" Then
            strErr = commoncheck.CheckKinsoku(tbxSeikyuuSakiShriSakiMei.Text, "請求先支払先名")
            If strErr <> "" Then
                strID = tbxSeikyuuSakiShriSakiMei.ClientID
            End If
        End If
        '請求先支払先名カナ
        If strErr = "" And tbxSeikyuuSakiShriSakiKana.Text <> "" Then
            strErr = commoncheck.CheckByte(tbxSeikyuuSakiShriSakiKana.Text, 40, "請求先支払先名カナ", kbn.ZENKAKU)
            If strErr <> "" Then
                strID = tbxSeikyuuSakiShriSakiKana.ClientID
            End If
        End If
        If strErr = "" And tbxSeikyuuSakiShriSakiKana.Text <> "" Then
            strErr = commoncheck.CheckKinsoku(tbxSeikyuuSakiShriSakiKana.Text, "請求先支払先名カナ")
            If strErr <> "" Then
                strID = tbxSeikyuuSakiShriSakiKana.ClientID
            End If
        End If
        '請求書送付先郵便番号
        If strErr = "" And tbxSkysySoufuYuubinNo.Text <> "" Then
            strErr = commoncheck.CheckHankakuHaifun(tbxSkysySoufuYuubinNo.Text, "請求書送付先郵便番号", "1")
            If strErr <> "" Then
                strID = tbxSkysySoufuYuubinNo.ClientID
            End If
        End If
        '請求書送付先住所１
        If strErr = "" And tbxSkysySoufuJyuusyo1.Text <> "" Then
            strErr = commoncheck.CheckByte(tbxSkysySoufuJyuusyo1.Text, 40, "請求書送付先住所１", kbn.ZENKAKU)
            If strErr <> "" Then
                strID = tbxSkysySoufuJyuusyo1.ClientID
            End If
        End If
        If strErr = "" And tbxSkysySoufuJyuusyo1.Text <> "" Then
            strErr = commoncheck.CheckKinsoku(tbxSkysySoufuJyuusyo1.Text, "請求書送付先住所１")
            If strErr <> "" Then
                strID = tbxSkysySoufuJyuusyo1.ClientID
            End If
        End If
        '請求書送付先電話番号
        If strErr = "" And tbxSkysySoufuTelNo.Text <> "" Then
            strErr = commoncheck.CheckHankakuHaifun(tbxSkysySoufuTelNo.Text, "請求書送付先電話番号", "1")
            If strErr <> "" Then
                strID = tbxSkysySoufuTelNo.ClientID
            End If
        End If
        '請求書送付先住所２
        If strErr = "" And tbxSkysySoufuJyuusyo2.Text <> "" Then
            strErr = commoncheck.CheckByte(tbxSkysySoufuJyuusyo2.Text, 40, "請求書送付先住所２", kbn.ZENKAKU)
            If strErr <> "" Then
                strID = tbxSkysySoufuJyuusyo2.ClientID
            End If
        End If
        If strErr = "" And tbxSkysySoufuJyuusyo2.Text <> "" Then
            strErr = commoncheck.CheckKinsoku(tbxSkysySoufuJyuusyo2.Text, "請求書送付先住所２")
            If strErr <> "" Then
                strID = tbxSkysySoufuJyuusyo2.ClientID
            End If
        End If
        '支払用FAX番号
        If strErr = "" And tbxShriYouFaxNo.Text <> "" Then
            strErr = commoncheck.CheckHankakuHaifun(tbxShriYouFaxNo.Text, "支払用FAX番号", "1")
            If strErr <> "" Then
                strID = tbxShriYouFaxNo.ClientID
            End If
        End If
        '新会計事業所コード
        If strErr = "" And tbxSkkJigyousyoCd.Text <> "" Then
            strErr = commoncheck.ChkHankakuEisuuji(tbxSkkJigyousyoCd.Text, "新会計事業所コード")
            If strErr <> "" Then
                strID = tbxSkkJigyousyoCd.ClientID
            End If
        End If
        '新会計支払先コード
        If strErr = "" And tbxSkkShriSakiCd.Text <> "" Then
            strErr = commoncheck.ChkHankakuEisuuji(tbxSkkShriSakiCd.Text, "新会計支払先コード")
            If strErr <> "" Then
                strID = tbxSkkShriSakiCd.ClientID
            End If
        End If
        '支払締め日
        If strErr = "" And tbxShriSimeDate.Text <> "" Then
            strErr = commoncheck.CheckSime(tbxShriSimeDate.Text, "支払締め日", "31")
            If strErr <> "" Then
                strID = tbxShriSimeDate.ClientID
            End If
        End If
        '支払予定月数
        If strErr = "" And tbxShriYoteiGessuu.Text <> "" Then
            strErr = commoncheck.CheckSime(tbxShriYoteiGessuu.Text, "支払予定月数", "12")
            If strErr <> "" Then
                strID = tbxShriYoteiGessuu.ClientID
            End If
        End If
        'ファクタリング開始年月
        If strErr = "" And tbxFctringKaisiNengetu.Text <> "" Then
            strErr = commoncheck.CheckYuukouHiduke(tbxFctringKaisiNengetu.Text, "ファクタリング開始年月")
            If strErr <> "" Then
                strID = tbxFctringKaisiNengetu.ClientID
            End If
        End If
        '支払集計先事業所
        If strErr = "" And tbxShriJigyousyoCd.Text <> "" Then
            strErr = commoncheck.ChkHankakuEisuuji(tbxShriJigyousyoCd.Text, "支払集計先事業所コード")
            If strErr <> "" Then
                strID = tbxShriJigyousyoCd.ClientID
            End If
        End If
        '支払明細集計先事業所
        If strErr = "" And tbxShriMeisaiJigyousyoCd.Text <> "" Then
            strErr = commoncheck.ChkHankakuEisuuji(tbxShriMeisaiJigyousyoCd.Text, "支払明細集計先事業所コード")
            If strErr <> "" Then
                strID = tbxShriMeisaiJigyousyoCd.ClientID
            End If
        End If
        Return strID

    End Function

    ''' <summary>
    ''' DDLの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Sub SetDdlListInf()
        '※ドロップダウンリスト設定↓
        Dim ddlist As ListItem

        '調査業務ddlTyousaGyoumu
        ddlist = New ListItem
        ddlist.Text = "行わない"
        ddlist.Value = "0"
        ddlTyousaGyoumu.Items.Add(ddlist)
        ddlist = New ListItem
        ddlist.Text = "行う"
        ddlist.Value = "1"
        ddlTyousaGyoumu.Items.Add(ddlist)

        '工事業務ddlKoujiGyoumu
        ddlist = New ListItem
        ddlist.Text = "行わない"
        ddlist.Value = "0"
        ddlKoujiGyoumu.Items.Add(ddlist)
        ddlist = New ListItem
        ddlist.Text = "行う"
        ddlist.Value = "1"
        ddlKoujiGyoumu.Items.Add(ddlist)

        '==============2012/04/12 車龍 405738 削除↓====================
        ''FC店区分ddlFCTenKbn
        'ddlist = New ListItem
        'ddlist.Text = "未加入"
        'ddlist.Value = "0"
        'ddlFCTenKbn.Items.Add(ddlist)
        'ddlist = New ListItem
        'ddlist.Text = "加入"
        'ddlist.Value = "1"
        'ddlFCTenKbn.Items.Add(ddlist)
        'ddlist = New ListItem
        'ddlist.Text = "退会"
        'ddlist.Value = "3"
        'ddlFCTenKbn.Items.Add(ddlist)
        '==============2012/04/12 車龍 405738 削除↑====================

        'JAPAN会区分ddlJapanKbn
        ddlist = New ListItem
        ddlist.Text = "未加入"
        ddlist.Value = "0"
        ddlJapanKbn.Items.Add(ddlist)

        ddlist = New ListItem
        ddlist.Text = "加入（JHS取引有）"
        ddlist.Value = "1"
        ddlJapanKbn.Items.Add(ddlist)

        ddlist = New ListItem
        ddlist.Text = "加入（全住品のみ）"
        ddlist.Value = "2"
        ddlJapanKbn.Items.Add(ddlist)

        ddlist = New ListItem
        ddlist.Text = "退会"
        ddlist.Value = "3"
        ddlJapanKbn.Items.Add(ddlist)

        ddlist = New ListItem
        ddlist.Text = "対象外"
        ddlist.Value = "4"
        ddlJapanKbn.Items.Add(ddlist)

        '宅地地盤調査主任資格ddlTktJbnTysSyuninSkkFlg
        ddlist = New ListItem
        ddlist.Text = "無し"
        ddlist.Value = "0"
        ddlTktJbnTysSyuninSkkFlg.Items.Add(ddlist)
        ddlist = New ListItem
        ddlist.Text = "有り"
        ddlist.Value = "1"
        ddlTktJbnTysSyuninSkkFlg.Items.Add(ddlist)

        'Ｒ－ＪＨＳトークンddlRJhsTokenFlg
        ddlist = New ListItem
        ddlist.Text = "無し"
        ddlist.Value = "0"
        ddlRJhsTokenFlg.Items.Add(ddlist)
        ddlist = New ListItem
        ddlist.Text = "有り"
        ddlist.Value = "1"
        ddlRJhsTokenFlg.Items.Add(ddlist)
        '=================2011/07/14 車龍 追加 開始↓=========================
        ddlRJhsTokenFlg.SelectedIndex = 1
        '=================2011/07/14 車龍 追加 開始↓=========================

        '工事報告書直送ddlKojHkksTyokusouFlg
        ddlist = New ListItem
        ddlist.Text = ""
        ddlist.Value = ""
        ddlKojHkksTyokusouFlg.Items.Add(ddlist)
        ddlist = New ListItem
        ddlist.Text = "可"
        ddlist.Value = "1"
        ddlKojHkksTyokusouFlg.Items.Add(ddlist)
        ddlist = New ListItem
        ddlist.Text = "不可"
        ddlist.Value = "0"
        ddlKojHkksTyokusouFlg.Items.Add(ddlist)

        '請求先区分ddlSeikyuuSaki
        SetKakutyou(ddlSeikyuuSaki, "1")

    End Sub

    ''' <summary>
    ''' 請求先区分ドロップダウンリスト設定
    ''' </summary>
    ''' <param name="ddl">ドロップダウンリスト</param>
    ''' <param name="strSyubetu">名称種別</param>
    ''' <remarks></remarks>
    Sub SetKakutyou(ByVal ddl As DropDownList, ByVal strSyubetu As String)
        Dim dtTable As New DataTable
        Dim intCount As Integer = 0
        dtTable = TyousaKaisyaMasterBL.SelKakutyouInfo(strSyubetu)

        Dim ddlist As New ListItem
        ddlist.Text = ""
        ddlist.Value = ""
        ddl.Items.Add(ddlist)
        For intCount = 0 To dtTable.Rows.Count - 1
            ddlist = New ListItem
            ddlist.Text = TrimNull(dtTable.Rows(intCount).Item("code")) & "：" & TrimNull(dtTable.Rows(intCount).Item("meisyou"))
            ddlist.Value = TrimNull(dtTable.Rows(intCount).Item("code"))
            ddl.Items.Add(ddlist)
        Next
    End Sub

    ''' <summary>
    ''' 活性化制御共通処理 請求先.検索ボタン押下時
    ''' </summary>
    ''' <remarks></remarks>
    Sub SetKasseika()
        If ddlSeikyuuSaki.SelectedValue = "1" And tbxTyousaKaisyaCd.Text.ToUpper = tbxSeikyuuSakiCd.Text.ToUpper And tbxJigyousyoCd.Text.ToUpper = tbxSeikyuuSakiBrc.Text.ToUpper Then
            tbxSeikyuuSakiShriSakiMei.Attributes.Remove("readonly")
            tbxSeikyuuSakiShriSakiKana.Attributes.Remove("readonly")
            tbxSkysySoufuYuubinNo.Attributes.Remove("readonly")
            tbxSkysySoufuJyuusyo1.Attributes.Remove("readonly")
            tbxSkysySoufuTelNo.Attributes.Remove("readonly")
            tbxSkysySoufuJyuusyo2.Attributes.Remove("readonly")
            tbxShriYouFaxNo.Attributes.Remove("readonly")
            btnKensakuSeikyuuSoufuCopy.Enabled = True
            btnKensakuSeikyuuSyo.Enabled = True
        Else
            tbxSeikyuuSakiShriSakiMei.Text = ""
            tbxSeikyuuSakiShriSakiKana.Text = ""
            tbxSkysySoufuYuubinNo.Text = ""
            tbxSkysySoufuJyuusyo1.Text = ""
            tbxSkysySoufuTelNo.Text = ""
            tbxSkysySoufuJyuusyo2.Text = ""
            tbxShriYouFaxNo.Text = ""

            tbxSeikyuuSakiShriSakiMei.Attributes.Add("readonly", "true;")
            tbxSeikyuuSakiShriSakiKana.Attributes.Add("readonly", "true;")
            tbxSkysySoufuYuubinNo.Attributes.Add("readonly", "true;")
            tbxSkysySoufuJyuusyo1.Attributes.Add("readonly", "true;")
            tbxSkysySoufuTelNo.Attributes.Add("readonly", "true;")
            tbxSkysySoufuJyuusyo2.Attributes.Add("readonly", "true;")
            tbxShriYouFaxNo.Attributes.Add("readonly", "true;")
            btnKensakuSeikyuuSoufuCopy.Enabled = False
            btnKensakuSeikyuuSyo.Enabled = False
        End If
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
    ''' 日付型変更処理
    ''' </summary>
    ''' <param name="ymd">年月</param>
    ''' <returns></returns>
    ''' <remarks>2010/01/12 馬艶軍（大連） 新規作成</remarks>
    Public Function toYYYYMMDDHH(ByVal ymd As Object) As Object

        If ymd.Equals(String.Empty) Or ymd.Equals(DBNull.Value) Then
            Return ymd
        Else
            Return CDate(ymd).ToString("yyyy/MM/dd hh:mm:ss")
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
            dtTable = TyousaKaisyaMasterBL.SelSeikyuuSaki(tbxSeikyuuSakiCd.Text, tbxSeikyuuSakiBrc.Text, ddlSeikyuuSaki.SelectedValue, True)
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
    ''' 請求先検索ボタンを押下、OKを選択時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Me.hidConfirm.Value = ""
        '該当するデータが存在する場合
        Dim dtSeikyuuSakiTouroku As New DataTable
        dtSeikyuuSakiTouroku = TyousaKaisyaMasterBL.SelSeikyuuSakiTouroku(tbxSeikyuuSakiBrc.Text)
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
        Me.hidConfirm1.Value = "ON"
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


End Class
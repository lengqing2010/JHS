Imports Itis.Earth.BizLogic
Imports Itis.Earth.DataAccess
Partial Public Class HanbaiKakakuMasterSearchList
    Inherits System.Web.UI.Page

    ''' <summary>販売価格マスタ照会</summary>
    ''' <remarks>販売価格マスタ照会機能を提供する</remarks>
    ''' <history>
    ''' <para>2011/03/01　呉営(大連情報システム部)　新規作成</para>
    ''' </history>
    Private hanbaiKakakuSearchListLogic As New HanbaiKakakuMasterLogic
    Private commonCheck As New CommonCheck
    Protected scrollHeight As Integer = 0
    '=======================2011/05/30 車龍 仕様変更 追加 開始↓==========================
    Protected setFlg As Boolean = False
    '=======================2011/05/30 車龍 仕様変更 追加 終了↑==========================

    ''' <summary>ページロッド</summary>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim strUserID As String = ""
        '権限チェック
        Dim jBn As New Jiban '地盤画面共通クラス
        Dim user_info As New LoginUserInfo
        jBn.userAuth(user_info)
        If user_info Is Nothing Then
            Context.Items("strFailureMsg") = Messages.Instance.MSG2024E
            Context.Server.Transfer("./CommonErr.aspx")
        End If

        'javascript作成
        MakeJavascript()

        If Not IsPostBack Then
            '参照履歴管理テーブルを登録する。
            commonCheck.SetURL(Me, strUserID)

            If Not Request("sendSearchTerms") Is Nothing Then
                '初期化
                Call SetInitData(CStr(Request("sendSearchTerms")))
            Else
                '初期化
                Call SetInitData(String.Empty)
            End If
        Else
            '検索条件(相手先コード)を設定する
            If Me.ddlAiteSakiSyubetu.SelectedValue <> "0" AndAlso Me.ddlAiteSakiSyubetu.SelectedValue <> String.Empty Then
                Me.divAitesaki.Attributes.Add("style", "display:block;")
            Else
                Me.divAitesaki.Attributes.Add("style", "display:none;")
            End If

            If Me.ddlAiteSakiSyubetu.SelectedValue = "1" Then
                '未設定＝チェック
                If Not Me.chkMiSetei.Checked Then

                    '「系列・営業所・指定無しも検索対象」チェックボックス表示
                    Me.divSitei.Style.Add("display", "block")

                    '「系列・営業所・指定無しも検索対象」チェックボックス＝チェック
                    If Me.chkSiteiNasiTaisyou.Checked Then

                        '相手先コードTOテキストボックス＋検索ボタン+「CSV出力」ボタン非表示
                        Me.divAitesakiTo.Style.Add("display", "none")
                        Me.tbxAiteSakiCdTo.Text = String.Empty
                        Me.tbxAiteSakiMeiTo.Text = String.Empty
                        Me.divCsvOutput.Style.Add("display", "none")

                    Else

                        '相手先コードTOテキストボックス＋検索ボタン+「CSV出力」ボタン表示
                        Me.divAitesakiTo.Style.Add("display", "block")
                        Me.divCsvOutput.Style.Add("display", "block")

                    End If
                Else

                    '「系列・営業所・指定無しも検索対象」チェックボックス非表示
                    Me.divSitei.Style.Add("display", "none")
                    Me.chkSiteiNasiTaisyou.Checked = False

                    '相手先コードTOテキストボックス＋検索ボタン+「CSV出力」ボタン表示
                    Me.divAitesakiTo.Style.Add("display", "block")
                    Me.divCsvOutput.Style.Add("display", "block")
                End If
            Else

                '「系列・営業所・指定無しも検索対象」チェックボックス非表示
                Me.divSitei.Style.Add("display", "none")
                Me.chkSiteiNasiTaisyou.Checked = False

                '相手先コードTOテキストボックス＋検索ボタン+「CSV出力」ボタン表示
                Me.divAitesakiTo.Style.Add("display", "block")
                Me.divCsvOutput.Style.Add("display", "block")

            End If

            '検索実行ボタン活用性を設定する
            If Me.chkMiSetei.Checked Then
                Me.btnKensaku.Enabled = False
            Else
                Me.btnKensaku.Enabled = True
            End If
            'CSV出力ボタンを押下する場合
            If Me.hidCsvOut.Value = "1" Then
                'CSV出力
                Call CsvOutPut()
            End If
        End If

        '=======================2011/05/30 車龍 仕様変更 修正 開始↓==========================
        setFlg = GetEigyouKengen(user_info)
        'CSV取込ボタンを設定する
        'Me.btnCsvUpload.Enabled = GetEigyouKengen(user_info)
        Me.btnCsvUpload.Enabled = setFlg

        '｢新規登録｣ボタンを設定する
        Me.btnTouroku.Enabled = setFlg
        '=======================2011/05/30 車龍 仕様変更 修正 終了↑==========================
        '相手先コード(FROM)がフォーカスを失う場合、大文字に変換する
        Me.tbxAiteSakiCdFrom.Attributes.Add("onBlur", "fncToUpper(this);")
        '相手先コード(TO)がフォーカスを失う場合、大文字に変換する
        Me.tbxAiteSakiCdTo.Attributes.Add("onBlur", "fncToUpper(this);")
        '相手先名(FROM)が入力不可を設定する
        Me.tbxAiteSakiMeiFrom.Attributes.Add("ReadOnly", "True")
        '相手先名(To)が入力不可を設定する
        Me.tbxAiteSakiMeiTo.Attributes.Add("ReadOnly", "True")
        '検索ボタンを押下する場合、必須チェック
        Me.btnKensaku.Attributes.Add("onClick", "if(fncNyuuryokuCheck('1')){fncShowModal();}else{return false;}")
        '検索ボタンを押下する場合、必須チェック
        Me.btnCsvOutput.Attributes.Add("onClick", "if(fncNyuuryokuCheck('2')){fncShowModal();}else{return false;}")
        '相手先種別が変更する場合、相手先コード検索条件を設定する
        Me.ddlAiteSakiSyubetu.Attributes.Add("onChange", "return fncSetAitesaki();")
        'クリアボタン処理
        Me.btnClear.Attributes.Add("onClick", "return fncClear();")
        '「閉じる」ボタン処理
        Me.btnClose.Attributes.Add("onClick", "return fncClose();")
        '「未設定も含む」チェックボックスを変更する場合、「検索実行」ボタン活用性を設定する
        Me.chkMiSetei.Attributes.Add("onClick", "return fncChange();")
        '「CSV取込」ボタンを押下する場合、販売価格取込ポップアップ画面を起動する
        Me.btnCsvUpload.Attributes.Add("onClick", "return fncCsvUpload();")
        '「系列・営業所・指定無しも検索対象」チェックボックスを変更する場合
        Me.chkSiteiNasiTaisyou.Attributes.Add("onClick", "return fncChangeAite();")
        '画面縦スクロールを設定する
        scrollHeight = ViewState("scrollHeight")

        '=======================2011/05/18 車龍 仕様変更 追加 開始↓==========================
        '｢新規登録｣ボタン
        If setFlg Then
            Me.btnTouroku.Attributes.Add("onClick", "fncPopupKobetuSettei('','','','');return false;")
        End If
        '=======================2011/05/18 車龍 仕様変更 追加 終了↑==========================

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

        Dim intCount As Integer     '検索件数
        '検索条件を設定する
        Dim dtParam As Data.DataTable = SetHanbaiKakaku()
        '検索データを取得する
        Dim dtHanbaiKakakuInfo As New Data.DataTable

        If Not Me.chkSiteiNasiTaisyou.Checked Then
            '検索件数を取得する
            intCount = hanbaiKakakuSearchListLogic.GetHanbaiKakakuInfoCount(dtParam)
            '検索データを取得する
            dtHanbaiKakakuInfo = hanbaiKakakuSearchListLogic.GetHanbaiKakakuInfo(dtParam)
        Else
            '検索件数を取得する
            intCount = hanbaiKakakuSearchListLogic.GetHanbaiKakakuSeiteNasiInfoCount(dtParam)
            '検索データを取得する
            dtHanbaiKakakuInfo = hanbaiKakakuSearchListLogic.GetHanbaiKakakuSeiteNasiInfo(dtParam)
        End If

        '検索結果を設定する
        Me.grdBodyLeft.DataSource = dtHanbaiKakakuInfo
        Me.grdBodyLeft.DataBind()
        Me.grdBodyRight.DataSource = dtHanbaiKakakuInfo
        Me.grdBodyRight.DataBind()

        '相手先名を設定する
        Call SetAitesakiMei(Me.ddlAiteSakiSyubetu.SelectedValue, _
                    dtParam.Rows(0).Item("aitesaki_cd_from"), _
                    dtParam.Rows(0).Item("aitesaki_cd_to"), _
                    dtParam.Rows(0).Item("torikesi_aitesaki"))
        
        '検索結果件数を設定する
        Call SetKensakuCount(intCount)

        If intCount = 0 Then
            'ソート順ボタンを設定する
            Call SetSortButton(False)
            ShowMessage(Messages.Instance.MSG020E, String.Empty)
            Exit Sub
        Else
            'ソート順ボタンを設定する
            Call SetSortButton(True)
            'ソート順ボタン色を設定する
            Call SetSortButtonColor()
            '数字列を設定する
            Call SetKingaku()
            ViewState("dtHanbaiKakakuInfo") = dtHanbaiKakakuInfo
            ViewState("scrollHeight") = scrollHeight
        End If

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

        Dim intCount As Long     '検索件数
        '検索条件を設定する
        Dim dtParam As Data.DataTable = SetHanbaiKakaku()
        'CSV出力上限件数
        Dim intCsvMax As Integer = CInt(System.Configuration.ConfigurationManager.AppSettings("CsvDownMax"))

        If Me.chkMiSetei.Checked Then
            '検索結果をクリアする
            Me.grdBodyLeft.DataSource = Nothing
            Me.grdBodyLeft.DataBind()
            Me.grdBodyRight.DataSource = Nothing
            Me.grdBodyRight.DataBind()
            '相手先名を設定する
            Call SetAitesakiMei(Me.ddlAiteSakiSyubetu.SelectedValue, _
                        dtParam.Rows(0).Item("aitesaki_cd_from"), _
                        dtParam.Rows(0).Item("aitesaki_cd_to"), _
                        dtParam.Rows(0).Item("torikesi_aitesaki"))
            '検索結果件数を設定する
            Call SetKensakuCount(0)
            'ソート順ボタンを設定する
            Call SetSortButton(False)
            '未設定も含む販売価格CSVデータ件数を設定する
            intCount = hanbaiKakakuSearchListLogic.GetMiSeteiHanbaiKakakuCSVCount(dtParam)
        Else
            '検索データを取得する
            Dim dtHanbaiKakakuInfo As Data.DataTable = hanbaiKakakuSearchListLogic.GetHanbaiKakakuInfo(dtParam)
            '検索結果を設定する
            Me.grdBodyLeft.DataSource = dtHanbaiKakakuInfo
            Me.grdBodyLeft.DataBind()
            Me.grdBodyRight.DataSource = dtHanbaiKakakuInfo
            Me.grdBodyRight.DataBind()

            '相手先名を設定する
            Call SetAitesakiMei(Me.ddlAiteSakiSyubetu.SelectedValue, _
                        dtParam.Rows(0).Item("aitesaki_cd_from"), _
                        dtParam.Rows(0).Item("aitesaki_cd_to"), _
                        dtParam.Rows(0).Item("torikesi_aitesaki"))

            '検索件数を取得する
            intCount = hanbaiKakakuSearchListLogic.GetHanbaiKakakuInfoCount(dtParam)

            '検索結果件数を設定する
            Call SetKensakuCount(intCount)

            If intCount = 0 Then
                'ソート順ボタンを設定する
                Call SetSortButton(False)
            Else
                'ソート順ボタンを設定する
                Call SetSortButton(True)
                'ソート順ボタン色を設定する
                Call SetSortButtonColor()
                '数字列を設定する
                Call SetKingaku()
                ViewState("dtHanbaiKakakuInfo") = dtHanbaiKakakuInfo
                ViewState("scrollHeight") = scrollHeight
            End If
        End If

        If intCount > intCsvMax Then
            strErrMessage = Messages.Instance.MSG051E.Replace("@PARAM1", intCsvMax)
            ShowMessage(strErrMessage, "")
        Else
            Me.hidCsvOut.Value = "1"
            ClientScript.RegisterStartupScript(Me.GetType, "", "<script language=javascript>document.forms[0].submit();</script>")
        End If

    End Sub
    ''' <summary>画面一覧ヘッダーに並び順をクリック時</summary>
    Private Sub btnSort_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAitesakiSyubetuUp.Click, _
                                                                                           btnAitesakiSyubetuDown.Click, _
                                                                                           btnAitesakiCdUp.Click, _
                                                                                           btnAitesakiCdDown.Click, _
                                                                                           btnAitesakiMeiUp.Click, _
                                                                                           btnAitesakiMeiDown.Click, _
                                                                                           btnSyouhinCdUp.Click, _
                                                                                           btnSyouhinCdDown.Click, _
                                                                                           btntSyouhinMeiUp.Click, _
                                                                                           btntSyouhinMeiDown.Click, _
                                                                                           btnTysHouhouUp.Click, _
                                                                                           btnTysHouhouDown.Click, _
                                                                                           btnTorikesiUp.Click, _
                                                                                           btnTorikesiDown.Click, _
                                                                                           btnKoumutenSeikyuuGakuUp.Click, _
                                                                                           btnKoumutenSeikyuuGakuDown.Click, _
                                                                                           btnKoumutenSeikyuuGakuHenkouFlgUp.Click, _
                                                                                           btnKoumutenSeikyuuGakuHenkouFlgDown.Click, _
                                                                                           btnJituSeikyuuGakuUp.Click, _
                                                                                           btnJituSeikyuuGakuDown.Click, _
                                                                                           btnJituSeikyuuGakuHenkouFlgUp.Click, _
                                                                                           btnJituSeikyuuGakuHenkouFlgDown.Click, _
                                                                                           btnKoukaiFlgUp.Click, _
                                                                                           btnKoukaiFlgDown.Click

        Dim strSort As String = String.Empty
        Dim strUpDown As String = String.Empty

        'ソート順ボタン色を設定する
        Call SetSortButtonColor()

        '画面にソート順をクリック時
        Select Case CType(sender, LinkButton).ID
            Case btnAitesakiSyubetuUp.ID                '相手先種別▲
                strSort = "aitesaki"
                strUpDown = "ASC"
                btnAitesakiSyubetuUp.ForeColor = Drawing.Color.IndianRed
            Case btnAitesakiSyubetuDown.ID              '相手先種別▼
                strSort = "aitesaki"
                strUpDown = "DESC"
                btnAitesakiSyubetuDown.ForeColor = Drawing.Color.IndianRed
            Case btnAitesakiCdUp.ID                     '相手先コード▲
                strSort = "aitesaki_cd"
                strUpDown = "ASC"
                btnAitesakiCdUp.ForeColor = Drawing.Color.IndianRed
            Case btnAitesakiCdDown.ID                   '相手先コード▼
                strSort = "aitesaki_cd"
                strUpDown = "DESC"
                btnAitesakiCdDown.ForeColor = Drawing.Color.IndianRed
            Case btnAitesakiMeiUp.ID                    '相手先名▲
                strSort = "aitesaki_mei"
                strUpDown = "ASC"
                btnAitesakiMeiUp.ForeColor = Drawing.Color.IndianRed
            Case btnAitesakiMeiDown.ID                  '相手先名▼
                strSort = "aitesaki_mei"
                strUpDown = "DESC"
                btnAitesakiMeiDown.ForeColor = Drawing.Color.IndianRed
            Case btnSyouhinCdUp.ID                      '商品コード▲
                strSort = "syouhin_cd"
                strUpDown = "ASC"
                btnSyouhinCdUp.ForeColor = Drawing.Color.IndianRed
            Case btnSyouhinCdDown.ID                    '商品コード▼
                strSort = "syouhin_cd"
                strUpDown = "DESC"
                btnSyouhinCdDown.ForeColor = Drawing.Color.IndianRed
            Case btntSyouhinMeiUp.ID                    '商品名▲
                strSort = "syouhin_mei"
                strUpDown = "ASC"
                btntSyouhinMeiUp.ForeColor = Drawing.Color.IndianRed
            Case btntSyouhinMeiDown.ID                  '商品名▼
                strSort = "syouhin_mei"
                strUpDown = "DESC"
                btntSyouhinMeiDown.ForeColor = Drawing.Color.IndianRed
            Case btnTysHouhouUp.ID                      '調査方法▲
                strSort = "tys_houhou_no"
                strUpDown = "ASC"
                btnTysHouhouUp.ForeColor = Drawing.Color.IndianRed
            Case btnTysHouhouDown.ID                    '調査方法▼
                strSort = "tys_houhou_no"
                strUpDown = "DESC"
                btnTysHouhouDown.ForeColor = Drawing.Color.IndianRed
            Case btnTorikesiUp.ID                       '取消▲
                strSort = "torikesi"
                strUpDown = "ASC"
                btnTorikesiUp.ForeColor = Drawing.Color.IndianRed
            Case btnTorikesiDown.ID                     '取消▼
                strSort = "torikesi"
                strUpDown = "DESC"
                btnTorikesiDown.ForeColor = Drawing.Color.IndianRed
            Case btnKoumutenSeikyuuGakuUp.ID            '工務店請求金額▲
                strSort = "koumuten_seikyuu_gaku"
                strUpDown = "ASC"
                btnKoumutenSeikyuuGakuUp.ForeColor = Drawing.Color.IndianRed
            Case btnKoumutenSeikyuuGakuDown.ID          '工務店請求金額▼
                strSort = "koumuten_seikyuu_gaku"
                strUpDown = "DESC"
                btnKoumutenSeikyuuGakuDown.ForeColor = Drawing.Color.IndianRed
            Case btnKoumutenSeikyuuGakuHenkouFlgUp.ID   '工務店請求金額FLG▲
                strSort = "koumuten_seikyuu_gaku_henkou_flg"
                strUpDown = "ASC"
                btnKoumutenSeikyuuGakuHenkouFlgUp.ForeColor = Drawing.Color.IndianRed
            Case btnKoumutenSeikyuuGakuHenkouFlgDown.ID '工務店請求金額FLG▼
                strSort = "koumuten_seikyuu_gaku_henkou_flg"
                strUpDown = "DESC"
                btnKoumutenSeikyuuGakuHenkouFlgDown.ForeColor = Drawing.Color.IndianRed
            Case btnJituSeikyuuGakuUp.ID                '実請求金額▲
                strSort = "jitu_seikyuu_gaku"
                strUpDown = "ASC"
                btnJituSeikyuuGakuUp.ForeColor = Drawing.Color.IndianRed
            Case btnJituSeikyuuGakuDown.ID              '実請求金額▼
                strSort = "jitu_seikyuu_gaku"
                strUpDown = "DESC"
                btnJituSeikyuuGakuDown.ForeColor = Drawing.Color.IndianRed
            Case btnJituSeikyuuGakuHenkouFlgUp.ID       '実請求金額FLG▲
                strSort = "jitu_seikyuu_gaku_henkou_flg"
                strUpDown = "ASC"
                btnJituSeikyuuGakuHenkouFlgUp.ForeColor = Drawing.Color.IndianRed
            Case btnJituSeikyuuGakuHenkouFlgDown.ID     '実請求金額FLG▼
                strSort = "jitu_seikyuu_gaku_henkou_flg"
                strUpDown = "DESC"
                btnJituSeikyuuGakuHenkouFlgDown.ForeColor = Drawing.Color.IndianRed
            Case btnKoukaiFlgUp.ID                      '公開フラグ▲
                strSort = "koukai_flg"
                strUpDown = "ASC"
                btnKoukaiFlgUp.ForeColor = Drawing.Color.IndianRed
            Case btnKoukaiFlgDown.ID                    '公開フラグ▼
                strSort = "koukai_flg"
                strUpDown = "DESC"
                btnKoukaiFlgDown.ForeColor = Drawing.Color.IndianRed
        End Select

        '画面データのソート順を設定する
        Dim dvHanbaiKakakuInfo As Data.DataView = CType(ViewState("dtHanbaiKakakuInfo"), Data.DataTable).DefaultView
        dvHanbaiKakakuInfo.Sort = strSort & " " & strUpDown

        Me.grdBodyLeft.DataSource = dvHanbaiKakakuInfo
        Me.grdBodyLeft.DataBind()
        Me.grdBodyRight.DataSource = dvHanbaiKakakuInfo
        Me.grdBodyRight.DataBind()

        '数字列を設定する
        Call SetKingaku()

        '画面縦スクロールを設定する
        scrollHeight = ViewState("scrollHeight")
        '画面横スクロール位置を設定する
        SetScroll()

    End Sub


    ''' <summary>初期データをセット</summary>
    ''' <param name="sendSearchTerms">パラメータ</param>
    Protected Sub SetInitData(ByVal sendSearchTerms As String)

        If sendSearchTerms <> String.Empty Then
            Dim arrSearchTerm() As String = Split(sendSearchTerms, "$$$")
            '相手先種別を設定する
            SetAitesakiSyubetu(arrSearchTerm(0))
            If Me.ddlAiteSakiSyubetu.SelectedValue <> String.Empty AndAlso Me.ddlAiteSakiSyubetu.SelectedValue <> "0" Then
                '相手先コードFROMを設定する
                If arrSearchTerm.Length > 1 Then
                    Me.tbxAiteSakiCdFrom.Text = arrSearchTerm(1)
                    Call SetAitesakiMei(Me.ddlAiteSakiSyubetu.SelectedValue, arrSearchTerm(1), String.Empty, "1")
                Else
                    Me.tbxAiteSakiCdFrom.Text = String.Empty
                End If
                '相手先コードTOを設定する
                Me.tbxAiteSakiCdTo.Text = String.Empty
                '相手先名TOを設定する
                Me.tbxAiteSakiMeiTo.Text = String.Empty
                Me.divAitesaki.Attributes.Add("style", "display:block;")
            Else
                '相手先コードFROMを設定する
                Me.tbxAiteSakiCdFrom.Text = String.Empty
                '相手先名FROMを設定する
                Me.tbxAiteSakiMeiFrom.Text = String.Empty
                '相手先コードTOを設定する
                Me.tbxAiteSakiCdTo.Text = String.Empty
                '相手先名TOを設定する
                Me.tbxAiteSakiMeiTo.Text = String.Empty
                Me.divAitesaki.Attributes.Add("style", "display:none;")
            End If
            '商品コードを設定する
            If arrSearchTerm.Length > 2 Then
                Call SetSyouhin(arrSearchTerm(2))
            Else
                Call SetSyouhin(String.Empty)
            End If
            '調査方法NOを設定する
            If arrSearchTerm.Length > 3 Then
                Call SetTyousaHouhou(arrSearchTerm(3))
            Else
                Call SetTyousaHouhou(String.Empty)
            End If
        Else
            '相手先種別を設定する
            Call SetAitesakiSyubetu(String.Empty)
            '相手先コードFROMを設定する
            Me.tbxAiteSakiCdFrom.Text = String.Empty
            '相手先名FROMを設定する
            Me.tbxAiteSakiMeiFrom.Text = String.Empty
            '相手先コードTOを設定する
            Me.tbxAiteSakiCdTo.Text = String.Empty
            '相手先名TOを設定する
            Me.tbxAiteSakiMeiTo.Text = String.Empty
            Me.divAitesaki.Attributes.Add("style", "display:none;")
            '商品コードを設定する
            Call SetSyouhin(String.Empty)
            '調査方法NOを設定する
            Call SetTyousaHouhou(String.Empty)
        End If
        '一覧データを設定する
        Me.grdBodyLeft.DataSource = Nothing
        Me.grdBodyLeft.DataBind()
        Me.grdBodyRight.DataSource = Nothing
        Me.grdBodyRight.DataBind()

    End Sub
    ''' <summary> 相手先種別リストを設定する</summary>
    ''' <param name="strAitesakiSyubetu">相手先種別</param>
    Sub SetAitesakiSyubetu(ByVal strAitesakiSyubetu As String)

        Dim dtAiteSakiSyubetu As Data.DataTable
        '相手先種別データを取得する
        dtAiteSakiSyubetu = hanbaiKakakuSearchListLogic.GetAiteSakiSyubetu()
        'データを設定する
        Me.ddlAiteSakiSyubetu.DataTextField = "aitesaki_syubetu"
        Me.ddlAiteSakiSyubetu.DataValueField = "code"
        Me.ddlAiteSakiSyubetu.DataSource = dtAiteSakiSyubetu
        Me.ddlAiteSakiSyubetu.DataBind()
        Me.ddlAiteSakiSyubetu.Items.Insert(0, New ListItem("", ""))
        If strAitesakiSyubetu = "1" Then
            Me.divSitei.Style.Add("display", "block")
            Me.chkSiteiNasiTaisyou.Checked = True
            Me.tbxAiteSakiCdTo.Text = String.Empty
            Me.tbxAiteSakiMeiTo.Text = String.Empty
            Me.divAitesakiTo.Style.Add("display", "none")
            Me.divCsvOutput.Style.Add("display", "none")
        End If
        '選択される値を設定する
        If strAitesakiSyubetu <> String.Empty Then
            Dim intcount
            For intcount = 0 To Me.ddlAiteSakiSyubetu.Items.Count - 1
                If Me.ddlAiteSakiSyubetu.Items(intcount).Value = strAitesakiSyubetu Then
                    Me.ddlAiteSakiSyubetu.SelectedIndex = intcount
                    Exit For
                End If
            Next
        End If

    End Sub
    ''' <summary> 商品コードリストを設定する</summary>
    ''' <param name="strSyouhinCd">商品コード</param>
    Sub SetSyouhin(ByVal strSyouhinCd As String)

        Dim dtSyouhin As Data.DataTable
        '相手先種別データを取得する
        dtSyouhin = hanbaiKakakuSearchListLogic.GetSyouhin()
        'データを設定する
        Me.ddlSyouhinCd.DataTextField = "syouhin"
        Me.ddlSyouhinCd.DataValueField = "syouhin_cd"
        Me.ddlSyouhinCd.DataSource = dtSyouhin
        Me.ddlSyouhinCd.DataBind()
        Me.ddlSyouhinCd.Items.Insert(0, New ListItem("", ""))
        '選択される値を設定する
        If strSyouhinCd <> String.Empty Then
            Dim intcount
            For intcount = 0 To Me.ddlSyouhinCd.Items.Count - 1
                If Me.ddlSyouhinCd.Items(intcount).Value = strSyouhinCd Then
                    Me.ddlSyouhinCd.SelectedIndex = intcount
                    Exit For
                End If
            Next
        End If

    End Sub
    ''' <summary> 調査方法リストを設定する</summary>
    ''' <param name="strTysHouhouNo">調査方法NO</param>
    Sub SetTyousaHouhou(ByVal strTysHouhouNo As String)

        Dim dtTyousahouhou As Data.DataTable
        '相手先種別データを取得する
        dtTyousahouhou = hanbaiKakakuSearchListLogic.GetTyousaHouhou()
        'データを設定する
        Me.ddlTyousaHouhou.DataTextField = "tys_houhou"
        Me.ddlTyousaHouhou.DataValueField = "tys_houhou_no"
        Me.ddlTyousaHouhou.DataSource = dtTyousahouhou
        Me.ddlTyousaHouhou.DataBind()
        Me.ddlTyousaHouhou.Items.Insert(0, New ListItem("", ""))
        '選択される値を設定する
        If strTysHouhouNo <> String.Empty Then
            Dim intcount
            For intcount = 0 To Me.ddlTyousaHouhou.Items.Count - 1
                If Me.ddlTyousaHouhou.Items(intcount).Value = strTysHouhouNo Then
                    Me.ddlTyousaHouhou.SelectedIndex = intcount
                    Exit For
                End If
            Next
        End If

    End Sub
    ''' <summary>入力チェック</summary>
    ''' <param name="strObjId">クライアントID</param>
    ''' <returns>エラーメッセージ</returns>
    Public Function CheckInput(ByRef strObjId As String) As String
        Dim csScript As New StringBuilder
        With csScript
            '相手先コード(From)(英数字チェック)
            If Me.tbxAiteSakiCdFrom.Text <> String.Empty Then
                .Append(commonCheck.ChkHankakuEisuuji(Me.tbxAiteSakiCdFrom.Text, "相手先コード(From)"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxAiteSakiCdFrom.ClientID
                End If
            End If
            '相手先コード(To)(英数字チェック)
            If Me.tbxAiteSakiCdTo.Text <> String.Empty Then
                .Append(commonCheck.ChkHankakuEisuuji(Me.tbxAiteSakiCdTo.Text, "相手先コード(To)"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxAiteSakiCdTo.ClientID
                End If
            End If
        End With
        Return csScript.ToString
    End Function
    ''' <summary>画面に入力した値をデータテーブルに設定する</summary>
    ''' <returns>画面データを検索するパラメータデータテーブル</returns>
    Private Function SetHanbaiKakaku() As HanbaiKakakuMasterDataSet.Param_HanbaiKakakuInfoDataTable

        Dim dtParam As New HanbaiKakakuMasterDataSet.Param_HanbaiKakakuInfoDataTable
        Dim row As HanbaiKakakuMasterDataSet.Param_HanbaiKakakuInfoRow = dtParam.NewParam_HanbaiKakakuInfoRow

        '相手先種別を設定する
        row.aitesaki_syubetu = Me.ddlAiteSakiSyubetu.SelectedValue
        '相手先コードFROMを設定する
        row.aitesaki_cd_from = Me.tbxAiteSakiCdFrom.Text
        '相手先コードTOを設定する
        row.aitesaki_cd_to = Me.tbxAiteSakiCdTo.Text
        '商品コードを設定する
        row.syouhin_cd = Me.ddlSyouhinCd.SelectedValue
        '調査方法を設定する
        row.tys_houhou_no = Me.ddlTyousaHouhou.SelectedValue
        '取消を設定する
        row.torikesi = IIf(Me.chkKensakuTaisyouGai.Checked, "1", String.Empty)
        '取消相手先を設定する
        row.torikesi_aitesaki = IIf(Me.chkAitesakiTaisyouGai.Checked, "1", String.Empty)
        '=======================2011/05/18 車龍 仕様変更 追加 開始↓==========================
        '\0は対象外
        row.kingaku_0_taisyou_gai = IIf(Me.chk0TaisyouGai.Checked, "1", String.Empty)
        '=======================2011/05/18 車龍 仕様変更 追加 終了↑==========================
        '検索上限件数を設定する
        row.kensaku_count = Me.ddlKensakuCount.SelectedValue

        dtParam.AddParam_HanbaiKakakuInfoRow(row)
        Return dtParam

    End Function
    ''' <summary>相手先名を設定する</summary>
    ''' <param name="strAitesakiSyubetu">相手先種別</param>
    ''' <param name="strAitesakiCdFrom">相手先コードFROM</param>
    ''' <param name="strAitesakiCdTo">相手先コードTO</param>
    ''' <param name="strTorikesiAitesaki">相手先取消区分</param>
    Private Sub SetAitesakiMei(ByVal strAitesakiSyubetu As String, _
                               ByVal strAitesakiCdFrom As String, _
                               ByVal strAitesakiCdTo As String, _
                               ByVal strTorikesiAitesaki As String)

        Dim commonSearchLogic As New CommonSearchLogic
        '相手先コードFromを設定する
        If Me.tbxAiteSakiCdFrom.Text <> String.Empty Then
            Dim dtAiteSakiTable As Data.DataTable = hanbaiKakakuSearchListLogic.GetAiteSaki(strAitesakiSyubetu, _
                                                                                            strTorikesiAitesaki, _
                                                                                            strAitesakiCdFrom)
            If dtAiteSakiTable.Rows.Count > 0 Then
                Me.tbxAiteSakiCdFrom.Text = dtAiteSakiTable.Rows(0).Item("cd").ToString
                Me.tbxAiteSakiMeiFrom.Text = dtAiteSakiTable.Rows(0).Item("mei").ToString
            Else
                Me.tbxAiteSakiMeiFrom.Text = String.Empty
            End If
        Else
            Me.tbxAiteSakiMeiFrom.Text = String.Empty
        End If

        '相手先コードToを設定する
        If Me.tbxAiteSakiCdTo.Text <> String.Empty Then

            Dim dtAiteSakiTable As Data.DataTable = hanbaiKakakuSearchListLogic.GetAiteSaki(strAitesakiSyubetu, _
                                                                                            strTorikesiAitesaki, _
                                                                                            strAitesakiCdTo)
            If dtAiteSakiTable.Rows.Count > 0 Then
                Me.tbxAiteSakiCdTo.Text = dtAiteSakiTable.Rows(0).Item("cd").ToString
                Me.tbxAiteSakiMeiTo.Text = dtAiteSakiTable.Rows(0).Item("mei").ToString
            Else
                Me.tbxAiteSakiMeiTo.Text = String.Empty
            End If
        Else
            Me.tbxAiteSakiMeiTo.Text = String.Empty
        End If
    End Sub
    ''' <summary>検索結果件数を設定する</summary>
    ''' <param name="intCount">検索結果件数</param>
    Private Sub SetKensakuCount(ByVal intCount As Integer)

        If Me.ddlKensakuCount.SelectedValue = "max" Then
            Me.lblCount.Text = intCount
            Me.lblCount.ForeColor = Drawing.Color.Black
            scrollHeight = intCount * 32 + 1
        Else
            If intCount > Me.ddlKensakuCount.SelectedValue Then
                Me.lblCount.Text = Me.ddlKensakuCount.SelectedValue & " / " & intCount
                Me.lblCount.ForeColor = Drawing.Color.Red
                scrollHeight = Me.ddlKensakuCount.SelectedValue * 32 + 1
            Else
                Me.lblCount.Text = intCount
                Me.lblCount.ForeColor = Drawing.Color.Black
                scrollHeight = intCount * 32 + 1
            End If
        End If

    End Sub
    ''' <summary>ソート順ボタンを設定する</summary>
    ''' <param name="blnFlg">表示区分</param>
    Private Sub SetSortButton(ByVal blnFlg As Boolean)

        Me.btnAitesakiSyubetuUp.Visible = blnFlg                 '相手先種別▲
        Me.btnAitesakiSyubetuDown.Visible = blnFlg               '相手先種別▼
        Me.btnAitesakiCdUp.Visible = blnFlg                      '相手先コード▲
        Me.btnAitesakiCdDown.Visible = blnFlg                    '相手先コード▼
        Me.btnAitesakiMeiUp.Visible = blnFlg                     '相手先名▲
        Me.btnAitesakiMeiDown.Visible = blnFlg                   '相手先名▼
        Me.btnSyouhinCdUp.Visible = blnFlg                       '商品コード▲
        Me.btnSyouhinCdDown.Visible = blnFlg                     '商品コード▼
        Me.btntSyouhinMeiUp.Visible = blnFlg                     '商品名▲
        Me.btntSyouhinMeiDown.Visible = blnFlg                   '商品名▼
        Me.btnTysHouhouUp.Visible = blnFlg                       '調査方法▲
        Me.btnTysHouhouDown.Visible = blnFlg                     '調査方法▼
        Me.btnTorikesiUp.Visible = blnFlg                        '取消▲
        Me.btnTorikesiDown.Visible = blnFlg                      '取消▼
        Me.btnKoumutenSeikyuuGakuUp.Visible = blnFlg             '工務店請求金額▲
        Me.btnKoumutenSeikyuuGakuDown.Visible = blnFlg           '工務店請求金額▼
        Me.btnKoumutenSeikyuuGakuHenkouFlgUp.Visible = blnFlg    '工務店請求金額FLG▲
        Me.btnKoumutenSeikyuuGakuHenkouFlgDown.Visible = blnFlg  '工務店請求金額FLG▼
        Me.btnJituSeikyuuGakuUp.Visible = blnFlg                 '実請求金額▲
        Me.btnJituSeikyuuGakuDown.Visible = blnFlg               '実請求金額▼
        Me.btnJituSeikyuuGakuHenkouFlgUp.Visible = blnFlg        '実請求金額FLG▲
        Me.btnJituSeikyuuGakuHenkouFlgDown.Visible = blnFlg      '実請求金額FLG▼
        Me.btnKoukaiFlgUp.Visible = blnFlg                       '公開フラグ▲
        Me.btnKoukaiFlgDown.Visible = blnFlg                     '公開フラグ▼

    End Sub
    ''' <summary>ソート順ボタン色を設定する</summary>
    Private Sub SetSortButtonColor()

        Me.btnAitesakiSyubetuUp.ForeColor = Drawing.Color.SkyBlue                   '相手先種別▲
        Me.btnAitesakiSyubetuDown.ForeColor = Drawing.Color.SkyBlue                 '相手先種別▼
        Me.btnAitesakiCdUp.ForeColor = Drawing.Color.SkyBlue                        '相手先コード▲
        Me.btnAitesakiCdDown.ForeColor = Drawing.Color.SkyBlue                      '相手先コード▼
        Me.btnAitesakiMeiUp.ForeColor = Drawing.Color.SkyBlue                       '相手先名▲
        Me.btnAitesakiMeiDown.ForeColor = Drawing.Color.SkyBlue                     '相手先名▼
        Me.btnSyouhinCdUp.ForeColor = Drawing.Color.SkyBlue                         '商品コード▲
        Me.btnSyouhinCdDown.ForeColor = Drawing.Color.SkyBlue                       '商品コード▼
        Me.btntSyouhinMeiUp.ForeColor = Drawing.Color.SkyBlue                       '商品名▲
        Me.btntSyouhinMeiDown.ForeColor = Drawing.Color.SkyBlue                     '商品名▼
        Me.btnTysHouhouUp.ForeColor = Drawing.Color.SkyBlue                         '調査方法▲
        Me.btnTysHouhouDown.ForeColor = Drawing.Color.SkyBlue                       '調査方法▼
        Me.btnTorikesiUp.ForeColor = Drawing.Color.SkyBlue                          '取消▲
        Me.btnTorikesiDown.ForeColor = Drawing.Color.SkyBlue                        '取消▼
        Me.btnKoumutenSeikyuuGakuUp.ForeColor = Drawing.Color.SkyBlue               '工務店請求金額▲
        Me.btnKoumutenSeikyuuGakuDown.ForeColor = Drawing.Color.SkyBlue             '工務店請求金額▼
        Me.btnKoumutenSeikyuuGakuHenkouFlgUp.ForeColor = Drawing.Color.SkyBlue      '工務店請求金額FLG▲
        Me.btnKoumutenSeikyuuGakuHenkouFlgDown.ForeColor = Drawing.Color.SkyBlue    '工務店請求金額FLG▼
        Me.btnJituSeikyuuGakuUp.ForeColor = Drawing.Color.SkyBlue                   '実請求金額▲
        Me.btnJituSeikyuuGakuDown.ForeColor = Drawing.Color.SkyBlue                 '実請求金額▼
        Me.btnJituSeikyuuGakuHenkouFlgUp.ForeColor = Drawing.Color.SkyBlue          '実請求金額FLG▲
        Me.btnJituSeikyuuGakuHenkouFlgDown.ForeColor = Drawing.Color.SkyBlue        '実請求金額FLG▼
        Me.btnKoukaiFlgUp.ForeColor = Drawing.Color.SkyBlue                         '公開フラグ▲
        Me.btnKoukaiFlgDown.ForeColor = Drawing.Color.SkyBlue                       '公開フラグ▼

    End Sub
    '''' <summary>CSV出力</summary>
    Private Sub CsvOutPut()
        '検索条件を設定する
        Dim dtParam As Data.DataTable = SetHanbaiKakaku()
        Dim dtHanbaiKakakuCsvInfo As HanbaiKakakuMasterDataSet.HanbaiKakakuCSVInfoTableDataTable
        If Me.chkMiSetei.Checked = True Then
            '未設定も含む販売価格CSVデータを取得する
            dtHanbaiKakakuCsvInfo = hanbaiKakakuSearchListLogic.GetMiSeteiHanbaiKakakuCSVInfo(dtParam)
        Else
            '販売価格CSVデータを取得する
            dtHanbaiKakakuCsvInfo = hanbaiKakakuSearchListLogic.GetHanbaiKakakuCSVInfo(dtParam)
        End If

        'CSVファイル名設定
        Dim strFileName As String = System.Configuration.ConfigurationManager.AppSettings("HanbaiKakakuMasterCsv").ToString

        Response.Buffer = True
        Dim writer As New CsvWriter(Me.Response.OutputStream, Encoding.GetEncoding(932), ",", vbCrLf)

        'CSVファイルヘッダ設定
        writer.WriteLine(EarthConst.conHanbaiKakakuCsvHeader)

        'CSVファイル内容設定
        For Each row As HanbaiKakakuMasterDataSet.HanbaiKakakuCSVInfoTableRow In dtHanbaiKakakuCsvInfo
            writer.WriteLine(row.edi_jouhou_sakusei_date, row.aitesaki_syubetu, row.aitesaki_cd, _
                row.aitesaki_mei, row.syouhin_cd, row.syouhin_mei, row.tys_houhou_no, _
                row.tys_houhou_mei, row.torikesi, row.koumuten_seikyuu_gaku, row.koumuten_seikyuu_gaku_henkou_flg, _
                row.jitu_seikyuu_gaku, row.jitu_seikyuu_gaku_henkou_flg, row.koukai_flg)
        Next

        'CSVファイルダウンロード
        Response.Charset = "utf-8"
        Response.ContentType = "text/plain"
        Response.AddHeader("Content-Disposition", "attachment; filename=" & HttpUtility.UrlEncode(strFileName))
        Response.End()

    End Sub
    ''' <summary>Javascript作成</summary>
    Private Sub MakeJavascript()
        Dim sbScript As New StringBuilder
        Dim strPraram(0) As String
        With sbScript
            .AppendLine("<script language='javascript' type='text/javascript'>")
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
            '検索実行、CSV出力ボタンを押下する場合、入力チェック
            .AppendLine("function fncNyuuryokuCheck(strKbn){")
            'CSV出力区分を設定する
            .AppendLine("   fncSetCsvOut();")
            '相手先種別が必須入力チェック
            .AppendLine("   if (document.all." & Me.ddlAiteSakiSyubetu.ClientID & ".selectedIndex=='0'){")
            .AppendLine("       alert('" & Messages.Instance.MSG013E.Replace("@PARAM1", "相手先種別") & "');")
            .AppendLine("       document.all." & Me.ddlAiteSakiSyubetu.ClientID & ".focus();")
            .AppendLine("       return false; ")
            .AppendLine("   }")
            '「相手先コードFROM」、「相手先コードTO」のいづれかは入力必須
            .AppendLine("   if (document.all." & Me.ddlAiteSakiSyubetu.ClientID & ".value!='0'){")
            .AppendLine("       if(fncNyuuryokuHissu(document.all." & Me.tbxAiteSakiCdFrom.ClientID & ".value)==false && fncNyuuryokuHissu(document.all." & Me.tbxAiteSakiCdTo.ClientID & ".value)==false){")
            .AppendLine("           alert('" & Messages.Instance.MSG041E.Replace("@PARAM1", "相手先コードFROM").Replace("@PARAM2", "相手先コードTO") & "');")
            .AppendLine("           document.all." & Me.tbxAiteSakiCdFrom.ClientID & ".focus();")
            .AppendLine("           return false; ")
            .AppendLine("       }")
            .AppendLine("   }")
            '確認メッセージ表示
            .AppendLine("   if(strKbn=='1'){")
            .AppendLine("       if (document.all." & Me.ddlKensakuCount.ClientID & ".value=='max'){")
            .AppendLine("          if(confirm('" & Messages.Instance.MSG007C & "')){")
            .AppendLine("               document.forms[0].submit();")
            .AppendLine("           }else{")
            .AppendLine("              return false; ")
            .AppendLine("         }")
            .AppendLine("       }")
            .AppendLine("   }")
            .AppendLine("return true;")
            .AppendLine("}")
            '入力必須チェック:未入力、スペースのみならエラー表示
            .AppendLine("function fncNyuuryokuHissu(strValue) {")
            .AppendLine("   var wkflg = 0;")
            .AppendLine("   var wkdata = strValue;")
            .AppendLine("   for (i = 0; i < wkdata.length; i++) {")
            .AppendLine("       if (wkdata.charAt(i) != " & """" & " " & """" & ") {")
            .AppendLine("           if (wkdata.charAt(i) != " & """" & "  " & """" & ") {")
            .AppendLine("               wkflg = 1;")
            .AppendLine("           }")
            .AppendLine("       }")
            .AppendLine("   }")
            .AppendLine("   if (wkflg == 0) {")
            .AppendLine("       return false;")
            .AppendLine("   }")
            .AppendLine("   return true;")
            .AppendLine("}")
            'CSV出力区分を設定する
            .AppendLine("function fncSetCsvOut(){")
            .AppendLine("   document.all." & Me.hidCsvOut.ClientID & ".value='';")
            .AppendLine("}")
            '相手先検索表示を設定する
            .AppendLine("function fncSetAitesaki(){")
            .AppendLine("   if(document.all." & Me.ddlAiteSakiSyubetu.ClientID & ".value == '0'||document.all." & Me.ddlAiteSakiSyubetu.ClientID & ".value == ''){")
            .AppendLine("       document.all." & Me.tbxAiteSakiCdFrom.ClientID & ".value = '';")
            .AppendLine("       document.all." & Me.tbxAiteSakiMeiFrom.ClientID & ".value = '';")
            .AppendLine("       document.all." & Me.tbxAiteSakiCdTo.ClientID & ".value = '';")
            .AppendLine("       document.all." & Me.tbxAiteSakiMeiTo.ClientID & ".value = '';")
            .AppendLine("       document.all." & Me.divAitesaki.ClientID & ".style.display = 'none';")
            .AppendLine("       document.all." & Me.divSitei.ClientID & ".style.display = 'none';")
            .AppendLine("       document.all." & Me.chkSiteiNasiTaisyou.ClientID & ".checked = false;")
            .AppendLine("       document.all." & Me.divCsvOutput.ClientID & ".style.display = 'block';")
            .AppendLine("   }else if(document.all." & Me.ddlAiteSakiSyubetu.ClientID & ".value == '1'){")
            .AppendLine("       document.all." & Me.divAitesaki.ClientID & ".style.display = 'block';")
            .AppendLine("       document.all." & Me.tbxAiteSakiCdFrom.ClientID & ".value = '';")
            .AppendLine("       document.all." & Me.tbxAiteSakiMeiFrom.ClientID & ".value = '';")
            .AppendLine("       document.all." & Me.tbxAiteSakiCdTo.ClientID & ".value = '';")
            .AppendLine("       document.all." & Me.tbxAiteSakiMeiTo.ClientID & ".value = '';")
            .AppendLine("       document.all." & Me.divAitesakiTo.ClientID & ".style.display = 'block';")
            .AppendLine("       if(!document.all." & Me.chkMiSetei.ClientID & ".checked){")
            .AppendLine("           document.all." & Me.divSitei.ClientID & ".style.display = 'block';")
            .AppendLine("       }")
            .AppendLine("   }else{")
            .AppendLine("       document.all." & Me.divAitesaki.ClientID & ".style.display = 'block';")
            .AppendLine("       document.all." & Me.tbxAiteSakiCdFrom.ClientID & ".value = '';")
            .AppendLine("       document.all." & Me.tbxAiteSakiMeiFrom.ClientID & ".value = '';")
            .AppendLine("       document.all." & Me.tbxAiteSakiCdTo.ClientID & ".value = '';")
            .AppendLine("       document.all." & Me.tbxAiteSakiMeiTo.ClientID & ".value = '';")
            .AppendLine("       document.all." & Me.divSitei.ClientID & ".style.display = 'none';")
            .AppendLine("       document.all." & Me.chkSiteiNasiTaisyou.ClientID & ".checked = false;")
            .AppendLine("       document.all." & Me.divCsvOutput.ClientID & ".style.display = 'block';")
            .AppendLine("       document.all." & Me.divAitesakiTo.ClientID & ".style.display = 'block';")
            .AppendLine("   }")
            .AppendLine("}")
            '相手先検索を押下する場合、ポップアップを起動する
            .AppendLine("function fncAiteSakiSearch(strAiteSakiKbn){")
            '相手先種別が「1:加盟店」の場合、加盟店ポップアップを起動する
            .AppendLine("   if(document.all." & Me.ddlAiteSakiSyubetu.ClientID & ".value == '1'){")
            .AppendLine("       var strkbn='加盟店';")
            .AppendLine("       var strClientCdID; ")
            .AppendLine("       var strClientMeiID; ")
            .AppendLine("       var blnTorikesi; ")
            .AppendLine("       if(strAiteSakiKbn == '1'){")
            .AppendLine("             strClientCdID = '" & Me.tbxAiteSakiCdFrom.ClientID & "';")
            .AppendLine("             strClientMeiID = '" & Me.tbxAiteSakiMeiFrom.ClientID & "';")
            .AppendLine("       }else{")
            .AppendLine("            strClientCdID = '" & Me.tbxAiteSakiCdTo.ClientID & "';")
            .AppendLine("             strClientMeiID = '" & Me.tbxAiteSakiMeiTo.ClientID & "';")
            .AppendLine("       }")
            .AppendLine("       if(document.all." & Me.chkAitesakiTaisyouGai.ClientID & ".checked){")
            .AppendLine("            blnTorikesi = 'True';")
            .AppendLine("       }else{")
            .AppendLine("            blnTorikesi = 'False';")
            .AppendLine("       }")
            .AppendLine("       objSrchWin = window.open('search_common.aspx?Kbn='+escape(strkbn)+'&FormName=" & Me.Form.Name & _
                                "&objCd='+strClientCdID+'&objMei='+strClientMeiID+'&strCd='+escape(eval('document.all.'+strClientCdID).value)+'&strMei='+escape(eval('document.all.'+strClientMeiID).value)+")
            .AppendLine("       '&blnDelete='+blnTorikesi, ")
            .AppendLine("       'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');")
            .AppendLine("       return false;")
            .AppendLine("   }else if(document.all." & Me.ddlAiteSakiSyubetu.ClientID & ".value == '5'){")
            '相手先種別が「5:営業所」の場合、営業所ポップアップを起動する
            .AppendLine("       var strkbn='営業所';")
            .AppendLine("       var strClientCdID; ")
            .AppendLine("       var strClientMeiID; ")
            .AppendLine("       var blnTorikesi; ")
            .AppendLine("       if(strAiteSakiKbn == '1'){")
            .AppendLine("            strClientCdID = '" & Me.tbxAiteSakiCdFrom.ClientID & "';")
            .AppendLine("            strClientMeiID = '" & Me.tbxAiteSakiMeiFrom.ClientID & "';")
            .AppendLine("       }else{")
            .AppendLine("           strClientCdID = '" & Me.tbxAiteSakiCdTo.ClientID & "';")
            .AppendLine("           strClientMeiID = '" & Me.tbxAiteSakiMeiTo.ClientID & "';")
            .AppendLine("       }")
            .AppendLine("       if(document.all." & Me.chkAitesakiTaisyouGai.ClientID & ".checked){")
            .AppendLine("           blnTorikesi = 'True';")
            .AppendLine("       }else{")
            .AppendLine("           blnTorikesi = 'False';")
            .AppendLine("       }")
            .AppendLine("       objSrchWin = window.open('search_common.aspx?Kbn='+escape(strkbn)+'&FormName=" & Me.Form.Name & _
                                "&objCd='+strClientCdID+'&objMei='+strClientMeiID+'&strCd='+escape(eval('document.all.'+strClientCdID).value)+'&strMei='+escape(eval('document.all.'+strClientMeiID).value)+")
            .AppendLine("       '&blnDelete='+blnTorikesi, ")
            .AppendLine("       'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');")
            .AppendLine("       return false;")
            .AppendLine("   }else if(document.all." & Me.ddlAiteSakiSyubetu.ClientID & ".value == '7'){")
            '相手先種別が「7:系列」の場合、系列ポップアップを起動する
            .AppendLine("       var strkbn='系列';")
            .AppendLine("       var strClientCdID; ")
            .AppendLine("       var strClientMeiID; ")
            .AppendLine("       var blnTorikesi; ")
            .AppendLine("       if(strAiteSakiKbn == '1'){")
            .AppendLine("           strClientCdID = '" & Me.tbxAiteSakiCdFrom.ClientID & "';")
            .AppendLine("           strClientMeiID = '" & Me.tbxAiteSakiMeiFrom.ClientID & "';")
            .AppendLine("       }else{")
            .AppendLine("           strClientCdID = '" & Me.tbxAiteSakiCdTo.ClientID & "';")
            .AppendLine("           strClientMeiID = '" & Me.tbxAiteSakiMeiTo.ClientID & "';")
            .AppendLine("       }")
            .AppendLine("       if(document.all." & Me.chkAitesakiTaisyouGai.ClientID & ".checked){")
            .AppendLine("           blnTorikesi = 'True';")
            .AppendLine("       }else{")
            .AppendLine("           blnTorikesi = 'False';")
            .AppendLine("       }")
            .AppendLine("       objSrchWin = window.open('search_common.aspx?Kbn='+escape(strkbn)+'&FormName=" & Me.Form.Name & _
                                "&objCd='+strClientCdID+'&objMei='+strClientMeiID+'&strCd='+escape(eval('document.all.'+strClientCdID).value)+'&strMei='+escape(eval('document.all.'+strClientMeiID).value)+")
            .AppendLine("       '&blnDelete='+blnTorikesi, ")
            .AppendLine("       'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');")
            .AppendLine("       return false;")
            .AppendLine("   }")
            .AppendLine("}")
            'クリアボタン処理
            .AppendLine("function fncClear(){")
            .AppendLine("   document.all." & Me.ddlAiteSakiSyubetu.ClientID & ".value = ''")
            .AppendLine("   document.all." & Me.tbxAiteSakiCdFrom.ClientID & ".value = '';")
            .AppendLine("   document.all." & Me.tbxAiteSakiMeiFrom.ClientID & ".value = '';")
            .AppendLine("   document.all." & Me.tbxAiteSakiCdTo.ClientID & ".value = '';")
            .AppendLine("   document.all." & Me.tbxAiteSakiMeiTo.ClientID & ".value = '';")
            .AppendLine("   document.all." & Me.divAitesaki.ClientID & ".style.display = 'none';")
            .AppendLine("   document.all." & Me.ddlSyouhinCd.ClientID & ".value = '';")
            .AppendLine("   document.all." & Me.ddlTyousaHouhou.ClientID & ".value = '';")
            .AppendLine("   document.all." & Me.ddlKensakuCount.ClientID & ".value = '100';")
            .AppendLine("   document.all." & Me.btnKensaku.ClientID & ".disabled = false;")
            .AppendLine("   document.all." & Me.chkKensakuTaisyouGai.ClientID & ".checked = true;")
            .AppendLine("   document.all." & Me.chkAitesakiTaisyouGai.ClientID & ".checked = true;")
            .AppendLine("   document.all." & Me.chkMiSetei.ClientID & ".checked = false;")
            .AppendLine("   document.all." & Me.divSitei.ClientID & ".style.display = 'none';")
            .AppendLine("   document.all." & Me.chkSiteiNasiTaisyou.ClientID & ".checked = false;")
            .AppendLine("   document.all." & Me.divCsvOutput.ClientID & ".style.display = 'block';")
            '=======================2011/05/18 車龍 仕様変更 追加 開始↓==========================
            '\0は対象外
            .AppendLine("   document.all." & Me.chk0TaisyouGai.ClientID & ".checked = false;")
            '=======================2011/05/18 車龍 仕様変更 追加 終了↑==========================
            .AppendLine("   return false;")
            .AppendLine("}")
            'クリアボタン処理
            .AppendLine("function fncClose(){")
            .AppendLine("   window.close();")
            .AppendLine("   return false;")
            .AppendLine("}")
            '「未設定も含む」チェックボックス処理
            .AppendLine("function fncChange(){")
            .AppendLine("   if(document.all." & Me.chkMiSetei.ClientID & ".checked){")
            .AppendLine("       document.all." & Me.btnKensaku.ClientID & ".disabled = true;")
            .AppendLine("       document.all." & Me.divSitei.ClientID & ".style.display = 'none';")
            .AppendLine("       document.all." & Me.chkSiteiNasiTaisyou.ClientID & ".checked = false;")
            .AppendLine("       document.all." & Me.divCsvOutput.ClientID & ".style.display = 'block';")
            .AppendLine("       document.all." & Me.divAitesakiTo.ClientID & ".style.display = 'block';")
            .AppendLine("   }else{")
            .AppendLine("       document.all." & Me.btnKensaku.ClientID & ".disabled = false;")
            .AppendLine("       document.all." & Me.divAitesakiTo.ClientID & ".style.display = 'block';")
            .AppendLine("       if(document.all." & Me.ddlAiteSakiSyubetu.ClientID & ".value == '1'){")
            .AppendLine("           document.all." & Me.divSitei.ClientID & ".style.display = 'block';")
            .AppendLine("       }")
            .AppendLine("   }")
            .AppendLine("}")
            '「系列・営業所・指定無しも検索対象」チェックボックス処理
            .AppendLine("function fncChangeAite(){")
            .AppendLine("   if(document.all." & Me.chkSiteiNasiTaisyou.ClientID & ".checked){")
            .AppendLine("       document.all." & Me.divAitesakiTo.ClientID & ".style.display = 'none';")
            .AppendLine("       document.all." & Me.tbxAiteSakiCdTo.ClientID & ".value = '';")
            .AppendLine("       document.all." & Me.tbxAiteSakiMeiTo.ClientID & ".value = '';")
            .AppendLine("       document.all." & Me.divCsvOutput.ClientID & ".style.display = 'none';")
            .AppendLine("   }else{")
            .AppendLine("       document.all." & Me.divAitesakiTo.ClientID & ".style.display = 'block';")
            .AppendLine("       document.all." & Me.divCsvOutput.ClientID & ".style.display = 'block';")
            .AppendLine("       document.all." & Me.btnCsvOutput.ClientID & ".display = true;")
            .AppendLine("   }")
            .AppendLine("}")
            'DIV表示
            .AppendLine("function fncShowModal(){")
            .AppendLine("   var buyDiv=document.getElementById('" & Me.buySelName.ClientID & "');")
            .AppendLine("   var disable=document.getElementById('" & Me.disableDiv.ClientID & "');")
            .AppendLine("   if(buyDiv.style.display=='none')")
            .AppendLine("   {")
            .AppendLine("       buyDiv.style.display='';")
            .AppendLine("       disable.style.display='';")
            .AppendLine("       disable.focus();")
            .AppendLine("   }else{")
            .AppendLine("       buyDiv.style.display='none';")
            .AppendLine("       disable.style.display='none';")
            .AppendLine("   }")
            .AppendLine("}")
            '販売価格マスタ取込ポップアップ画面を起動する
            .AppendLine("function fncCsvUpload(){")
            .AppendLine("   window.open('HanbaiKakakuMasterInput.aspx', 'hanbaiKakakuWindow')")
            .AppendLine("   return false;")
            .AppendLine("}")

            .AppendLine("</script>")
        End With
        Page.ClientScript.RegisterStartupScript(Page.GetType, "myJavaScript", sbScript.ToString)
    End Sub
    ''' <summary>エラーメッセージ表示</summary>
    ''' <param name="strMessage">エラーメッセージ</param>
    ''' <param name="strObjId">クライアントID</param>
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
    ''' <summary>数字列設定</summary>
    Public Sub SetKingaku()

        Dim rowCount As Integer

        For rowCount = 0 To Me.grdBodyRight.Rows.Count - 1
            '工務店請求金額を設定する
            If Me.grdBodyRight.Rows(rowCount).Cells(1).Text.ToString.Trim.Equals("&nbsp;") Then
                Me.grdBodyRight.Rows(rowCount).Cells(1).Text = ""
            Else
                Me.grdBodyRight.Rows(rowCount).Cells(1).Text = FormatNumber(Me.grdBodyRight.Rows(rowCount).Cells(1).Text, 0)
            End If
            '実請求金額を設定する
            If Me.grdBodyRight.Rows(rowCount).Cells(3).Text.ToString.Trim.Equals("&nbsp;") Then
                Me.grdBodyRight.Rows(rowCount).Cells(3).Text = ""
            Else
                Me.grdBodyRight.Rows(rowCount).Cells(3).Text = FormatNumber(Me.grdBodyRight.Rows(rowCount).Cells(3).Text, 0)
            End If
        Next

    End Sub
    ''' <summary>営業所マスタ権限チェック</summary>
    ''' <returns>営業所マスタ権限区分</returns>
    Public Function GetEigyouKengen(ByVal user_info As LoginUserInfo) As Boolean

        Dim dtAccountTable As CommonSearchDataSet.AccountTableDataTable
        '営業所マスタ権限取得
        dtAccountTable = commonCheck.CheckKengen(user_info.AccountNo)
        If dtAccountTable.Rows.Count = 0 Then
            Return False
        ElseIf dtAccountTable.Rows(0).Item("eigyou_master_kanri_kengen") = -1 Then
            Return True
        Else
            Return False
        End If

    End Function

    Private Sub grdBodyLeft_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdBodyLeft.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(2).Style.Add("word-break", "break-all")
            e.Row.Cells(4).Style.Add("word-break", "break-all")
            e.Row.Cells(5).Style.Add("word-break", "break-all")
            If setFlg Then
                '相手先種別
                Dim strAiteSakiSyubetu As String
                strAiteSakiSyubetu = e.Row.Cells(0).Text.Split("：")(0).Trim
                '相手先コード
                Dim strAiteSakiCd As String
                If strAiteSakiSyubetu.Equals("0") Then
                    strAiteSakiCd = "ALL"
                Else
                    strAiteSakiCd = e.Row.Cells(1).Text.Trim
                End If
                '商品コード
                Dim strSyouhinCd As String
                strSyouhinCd = e.Row.Cells(3).Text.Trim
                '調査方法
                Dim strTyousaHouhouNo As String
                strTyousaHouhouNo = CType(e.Row.Cells(5).Controls(1), WebControls.Label).Text.Split("：")(0).Trim
                e.Row.Attributes.Add("ondblclick", "fncPopupKobetuSettei('" & strAiteSakiSyubetu & "','" & strAiteSakiCd & "','" & strSyouhinCd & "','" & strTyousaHouhouNo & "');")
            End If
        End If

    End Sub

    Private Sub grdBodyRight_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdBodyRight.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If setFlg Then
                Dim RowIndex As Integer = e.Row.RowIndex
                '相手先種別
                Dim strAiteSakiSyubetu As String
                strAiteSakiSyubetu = Me.grdBodyLeft.Rows(RowIndex).Cells(0).Text.Split("：")(0).Trim
                '相手先コード
                Dim strAiteSakiCd As String
                If strAiteSakiSyubetu.Equals("0") Then
                    strAiteSakiCd = "ALL"
                Else
                    strAiteSakiCd = Me.grdBodyLeft.Rows(RowIndex).Cells(1).Text.Trim
                End If
                '商品コード
                Dim strSyouhinCd As String
                strSyouhinCd = Me.grdBodyLeft.Rows(RowIndex).Cells(3).Text.Trim
                '調査方法
                Dim strTyousaHouhouNo As String
                strTyousaHouhouNo = CType(Me.grdBodyLeft.Rows(RowIndex).Cells(5).Controls(1), WebControls.Label).Text.Split("：")(0).Trim
                e.Row.Attributes.Add("ondblclick", "fncPopupKobetuSettei('" & strAiteSakiSyubetu & "','" & strAiteSakiCd & "','" & strSyouhinCd & "','" & strTyousaHouhouNo & "');")
            End If
        End If
    End Sub

End Class
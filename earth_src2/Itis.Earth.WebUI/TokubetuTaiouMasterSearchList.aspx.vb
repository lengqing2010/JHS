Imports Itis.Earth.BizLogic

Partial Public Class TokubetuTaiouMasterSearchList
    Inherits System.Web.UI.Page

    ''' <summary>加盟店商品調査方法特別対応マスタ照会</summary>
    ''' <remarks>加盟店商品調査方法特別対応マスタ照会用機能を提供する</remarks>
    ''' <history>
    ''' <para>2011/03/03　ジン登閣(大連情報システム部)　新規作成</para>
    ''' </history>
    Private tokubetuTaiouMasterLogic As New TokubetuTaiouMasterLogic
    Private commonCheck As New CommonCheck
    Private commonSearchLogic As New CommonSearchLogic
    Protected scrollHeight As Integer = 0

    ''' <summary>ページロッド</summary>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '権限チェック
        Dim commonChk As New CommonCheck
        Dim strUserID As String = ""
        Dim blnEigyouKengen As Boolean
        blnEigyouKengen = commonChk.CommonNinnsyou(strUserID, "eigyou_master_kanri_kengen,kaiseki_master_kanri_kengen")

        'JavaScriptを作成
        MakeJavaScript()

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
            '検索条件(相手先コード)を設定する
            If Me.ddlAitesakiSyubetu.SelectedValue <> "0" AndAlso Me.ddlAitesakiSyubetu.SelectedValue <> String.Empty Then
                Me.divAitesakiSyubetu.Attributes.Add("style", "display:block;")
            Else
                Me.divAitesakiSyubetu.Attributes.Add("style", "display:none;")
            End If

            If Me.ddlAitesakiSyubetu.SelectedValue = "1" Then
                '相手先が加盟店の場合

                '未設定＝チェック
                If Not Me.chkMisetteimo.Checked Then

                    '「系列・営業所・指定無しも検索対象」チェックボックス表示
                    Me.divSitei.Style.Add("display", "block")

                    '「系列・営業所・指定無しも検索対象」チェックボックス＝チェック
                    If Me.chkSiteiNasiTaisyou.Checked Then

                        '相手先コードTOテキストボックス＋検索ボタン+「CSV出力」ボタン非表示
                        Me.divAitesakiTo.Style.Add("display", "none")
                        Me.tbxKameitenCdTo.Text = String.Empty
                        Me.tbxKameitenMeiTo.Text = String.Empty
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
                '相手先が加盟店以外の場合

                '「系列・営業所・指定無しも検索対象」チェックボックス非表示
                Me.divSitei.Style.Add("display", "none")
                Me.chkSiteiNasiTaisyou.Checked = False

                '相手先コードTOテキストボックス＋検索ボタン+「CSV出力」ボタン表示
                Me.divAitesakiTo.Style.Add("display", "block")
                Me.divCsvOutput.Style.Add("display", "block")

            End If

            '検索ボタンの処理
            Me.btnKensaku.Enabled = True
            If Me.chkMisetteimo.Checked = True Then
                Me.btnKensaku.Enabled = False
            End If

            'CSVファイルダウンロード
            If Me.hidCSVFlg.Value = "1" Then
                MakeCSVFile()
            End If

            'DIV非表示
            CloseCover()
        End If

        'CSV取込ボタンを設定する
        If blnEigyouKengen Then
            Me.btnCSVInput.Enabled = True
        Else
            Me.btnCSVInput.Enabled = False
        End If

        '相手先状態設置
        If ddlAitesakiSyubetu.SelectedValue.Trim.Equals("0") OrElse String.IsNullOrEmpty(ddlAitesakiSyubetu.SelectedValue.Trim) Then
            Me.tbxKameitenCdFrom.Text = String.Empty
            Me.tbxKameitenMeiFrom.Text = String.Empty
            Me.tbxKameitenCdTo.Text = String.Empty
            Me.tbxKameitenMeiTo.Text = String.Empty
            Me.divAitesakiSyubetu.Attributes.Add("style", "display:none;")
        Else
            Me.divAitesakiSyubetu.Attributes.Add("style", "display:block;")
        End If

        '特別対応名称は表示のみ
        Me.tbxTokubetuTaiouMei.Attributes.Add("readonly", "true")
        '加盟店名は表示のみ
        Me.tbxKameitenMeiFrom.Attributes.Add("readonly", "true")
        '加盟店名は表示のみ
        Me.tbxKameitenMeiTo.Attributes.Add("readonly", "true")
        '｢閉じる｣ボタン
        Me.btnClose.Attributes.Add("onclick", "fncClose();return false;")
        '「検索（特別対応）」ボタン
        Me.btnSearch.Attributes.Add("onclick", "fncSetTokubetuKaiou();return false;")
        ''「検索（加盟店コードFrom）ボタン」
        'Me.btnKameitenSearchFrom.Attributes.Add("onClick", "fncSetKameiten('" & Me.tbxKameitenCdFrom.ClientID & "',1,'" & Me.tbxKameitenMeiFrom.ClientID & "');return false;")
        ''「検索（加盟店コードTo）ボタン」
        'Me.btnKameitenSearchTo.Attributes.Add("onClick", "fncSetKameiten('" & Me.tbxKameitenCdTo.ClientID & "',2,'" & Me.tbxKameitenMeiTo.ClientID & "');return false;")
        '相手先FROMの｢検索｣ボタン
        Me.btnKameitenSearchFrom.Attributes.Add("onClick", "fncAiteSakiSearch('1');return false;")
        '相手先TOの｢検索｣ボタン
        Me.btnKameitenSearchTo.Attributes.Add("onClick", "fncAiteSakiSearch('2');return false;")

        '「クリア」ボタン
        Me.btnClear.Attributes.Add("onClick", "fncClear();return false;")
        '加盟店コード入力かどうかチェック
        Me.btnKensaku.Attributes.Add("onClick", "fncSetHidCSV();if(! fncKameitenCdChk('kensaku')){return false;}else{fncShowModal();}")
        Me.btnCSVOutput.Attributes.Add("onClick", "fncSetHidCSV();if(! fncKameitenCdChk('csv')){return false;}else{fncShowModal();}")
        'CSV取込画面をポップアップする
        Me.btnCSVInput.Attributes.Add("onclick", "fncShowModal();fncSetHidCSV();fncCSVTorikomi();fncClosecover();return false;")
        '「未設定も含む」チェックボックス
        Me.chkMisetteimo.Attributes.Add("onClick", "fncSetbtnKensaku()")
        '「系列・営業所・指定無しも検索対象」チェックボックスを変更する場合
        Me.chkSiteiNasiTaisyou.Attributes.Add("onClick", "return fncChangeAite();")
        Call SetButton()
        '相手先種別が変更する場合、相手先コード検索条件を設定する
        Me.ddlAitesakiSyubetu.Attributes.Add("onChange", "return fncSetAitesaki();")

        '画面縦スクロールを設定する
        scrollHeight = ViewState("scrollHeight")

    End Sub

    ''' <summary>初期化</summary> 
    Private Sub Syokika(ByVal sendSearchTerms As String)

        If Not sendSearchTerms.Equals(String.Empty) Then
            Dim arrSearchTerm() As String = Split(sendSearchTerms, "$$$")
            '相手先種別を設定
            Call setAitesakiSyubetu(arrSearchTerm(0))

            '============2012/05/23 車龍 407553の対応 追加↓=========================
            If Me.ddlAitesakiSyubetu.SelectedValue.Trim.Equals("1") Then
                '「系列・営業所・指定無しも検索対象」チェックボックス表示
                Me.divSitei.Style.Add("display", "block")
            End If
            '============2012/05/23 車龍 407553の対応 追加↑=========================


            '-------------------From 2013.03.09李宇追加------------------------
            '「系列・営業所・指定無しも検索対象」チェック = チェック
            If Me.chkSiteiNasiTaisyou.Checked Then
                '相手先コードTOテキストボックス＋検索ボタン+「CSV出力」ボタン非表示
                Me.divAitesakiTo.Style.Add("display", "none")
                Me.tbxKameitenCdTo.Text = String.Empty
                Me.tbxKameitenMeiTo.Text = String.Empty
                Me.divCsvOutput.Style.Add("display", "none")
            End If
            '-------------------To   2013.03.09李宇追加------------------------

            '加盟店コードFrom
            Me.tbxKameitenCdFrom.Text = arrSearchTerm(1)

            '商品コードを設定
            If arrSearchTerm.Length > 2 Then
                Call SetSyouhinCd(arrSearchTerm(2))
            Else
                Call SetSyouhinCd(String.Empty)
            End If

            '調査方法
            If arrSearchTerm.Length > 3 Then
                Call SetTyousaHouhou(arrSearchTerm(3))
            Else
                Call SetTyousaHouhou(String.Empty)
            End If

            '特別対応コード
            If arrSearchTerm.Length > 4 Then
                Me.tbxTokubetuTaiouCd.Text = arrSearchTerm(4)
            Else
                Me.tbxTokubetuTaiouCd.Text = String.Empty
            End If

            '加盟店名と特別対応名を設定する
            'Call Me.SetGamenData()
            Call Me.SetGamenData(Me.ddlAitesakiSyubetu.SelectedValue.Trim, Me.tbxKameitenCdFrom.Text.Trim, Me.tbxKameitenCdTo.Text.Trim, IIf(Me.chkTorikesi.Checked, "1", String.Empty))
        Else
            '相手先種別を設定
            Call setAitesakiSyubetu(String.Empty)
            '加盟店コードFrom
            Me.tbxKameitenCdFrom.Text = String.Empty
            '加盟店名From
            Me.tbxKameitenMeiFrom.Text = String.Empty
            '加盟店コードTo
            Me.tbxKameitenCdTo.Text = String.Empty
            '加盟店名To
            Me.tbxKameitenMeiTo.Text = String.Empty
            '商品コードを設定
            Call SetSyouhinCd(String.Empty)
            '調査方法
            Call SetTyousaHouhou(String.Empty)
            '特別対応コード
            Me.tbxTokubetuTaiouCd.Text = String.Empty
            '特別対応名
            Me.tbxTokubetuTaiouMei.Text = String.Empty
        End If

        '取消は検索対象外
        Me.chkTorikesi.Checked = True
        '未設定も含む
        Me.chkMisetteimo.Checked = False

        'リンクボタンの表示を設定
        Call Me.SetUpDownHyouji(False)

        '検索明細
        Me.grdMeisaiLeft.DataSource = Nothing
        Me.grdMeisaiLeft.DataBind()
        Me.grdMeisaiRight.DataSource = Nothing
        Me.grdMeisaiRight.DataBind()
    End Sub

    ''' <summary>相手先種別を設定</summary>
    Private Sub setAitesakiSyubetu(ByVal strAitesakiCd As String)
        Dim dtAitesakiSyubetu As New Data.DataTable
        dtAitesakiSyubetu = tokubetuTaiouMasterLogic.GetAitesakiSyubetuList()
        Me.ddlAitesakiSyubetu.DataValueField = "value"
        Me.ddlAitesakiSyubetu.DataTextField = "name"
        Me.ddlAitesakiSyubetu.DataSource = dtAitesakiSyubetu
        Me.ddlAitesakiSyubetu.DataBind()

        '商品コードの先頭行は空欄をセットする
        Me.ddlAitesakiSyubetu.Items.Insert(0, New ListItem(String.Empty, String.Empty))

        If String.IsNullOrEmpty(strAitesakiCd) Then
            Me.ddlAitesakiSyubetu.SelectedValue = String.Empty
        Else
            Try
                Me.ddlAitesakiSyubetu.SelectedValue = strAitesakiCd
            Catch ex As Exception
                Me.ddlAitesakiSyubetu.SelectedValue = String.Empty
            End Try
        End If
    End Sub

    ''' <summary>商品コードを設定</summary>
    Private Sub SetSyouhinCd(ByVal strSyouhinCd As String)
        '商品コードデータを取得
        Dim dtSyouhinCd As New Data.DataTable
        dtSyouhinCd = tokubetuTaiouMasterLogic.GetSyouhinCd()

        '商品コード
        Me.ddlSyouhinCd.DataValueField = "syouhin_cd"
        Me.ddlSyouhinCd.DataTextField = "syouhin_mei"
        Me.ddlSyouhinCd.DataSource = dtSyouhinCd
        Me.ddlSyouhinCd.DataBind()

        '商品コードの先頭行は空欄をセットする
        Me.ddlSyouhinCd.Items.Insert(0, New ListItem(String.Empty, String.Empty))

        'プリンタのデフォルト表示
        If strSyouhinCd.Equals(String.Empty) Then
            Me.ddlSyouhinCd.SelectedValue = String.Empty
        Else
            Try
                Me.ddlSyouhinCd.SelectedValue = strSyouhinCd
            Catch ex As Exception
                Me.ddlSyouhinCd.SelectedValue = String.Empty
            End Try
        End If

    End Sub

    ''' <summary>調査方法を設定</summary>
    Private Sub SetTyousaHouhou(ByVal strTyousaHouhou As String)
        '調査方法データを取得
        Dim dtTyousaHouhou As New Data.DataTable
        dtTyousaHouhou = tokubetuTaiouMasterLogic.GetTyousaHouhou()

        '調査方法
        Me.ddlTyousaHouhou.DataValueField = "tys_houhou_no"
        Me.ddlTyousaHouhou.DataTextField = "tys_houhou_mei"
        Me.ddlTyousaHouhou.DataSource = dtTyousaHouhou
        Me.ddlTyousaHouhou.DataBind()

        '調査方法の先頭行は空欄をセットする
        Me.ddlTyousaHouhou.Items.Insert(0, New ListItem(String.Empty, String.Empty))

        'プリンタのデフォルト表示
        If strTyousaHouhou.Equals(String.Empty) Then
            Me.ddlTyousaHouhou.SelectedValue = String.Empty
        Else
            Try
                Me.ddlTyousaHouhou.SelectedValue = strTyousaHouhou
            Catch ex As Exception
                Me.ddlTyousaHouhou.SelectedValue = String.Empty
            End Try
        End If

    End Sub

    ''' <summary>リンクボタンの色を設定</summary>
    Public Sub setUpDownColor()
        Me.btnSortAitesakiSyubetuUp.ForeColor = Drawing.Color.SkyBlue
        Me.btnSortAitesakiSyubetuDown.ForeColor = Drawing.Color.SkyBlue
        Me.btnSortAitesakiCdUp.ForeColor = Drawing.Color.SkyBlue
        Me.btnSortAitesakiCdDown.ForeColor = Drawing.Color.SkyBlue
        Me.btnSortAitesakiMeiUp.ForeColor = Drawing.Color.SkyBlue
        Me.btnSortAitesakiMeiDown.ForeColor = Drawing.Color.SkyBlue
        Me.btnSortSyouhinCdUp.ForeColor = Drawing.Color.SkyBlue
        Me.btnSortSyouhinCdDown.ForeColor = Drawing.Color.SkyBlue
        Me.btnSortSyouhinMeiUp.ForeColor = Drawing.Color.SkyBlue
        Me.btnSortSyouhinMeiDown.ForeColor = Drawing.Color.SkyBlue
        Me.btnSortTyousaUp.ForeColor = Drawing.Color.SkyBlue
        Me.btnSortTyousaDown.ForeColor = Drawing.Color.SkyBlue
        'Me.btnSortTokubetuCdUp.ForeColor = Drawing.Color.SkyBlue
        'Me.btnSortTokubetuCdDown.ForeColor = Drawing.Color.SkyBlue
        Me.btnSortTokubetuMeiUp.ForeColor = Drawing.Color.SkyBlue
        Me.btnSortTokubetuMeiDown.ForeColor = Drawing.Color.SkyBlue
        Me.btnSortTorikesiUp.ForeColor = Drawing.Color.SkyBlue
        Me.btnSortTorikesiDown.ForeColor = Drawing.Color.SkyBlue
        Me.btnSortKingakuAddScdUp.ForeColor = Drawing.Color.SkyBlue
        Me.btnSortKingakuAddScdDown.ForeColor = Drawing.Color.SkyBlue
        Me.btnSortSyokiTiUp.ForeColor = Drawing.Color.SkyBlue
        Me.btnSortSyokiTiDown.ForeColor = Drawing.Color.SkyBlue
        Me.btnSortRequestAddKingakuUp.ForeColor = Drawing.Color.SkyBlue
        Me.btnSortRequestAddKingakuDown.ForeColor = Drawing.Color.SkyBlue
        Me.btnSortKoumuAddKingakuUp.ForeColor = Drawing.Color.SkyBlue
        Me.btnSortKoumuAddKingakuDown.ForeColor = Drawing.Color.SkyBlue
    End Sub

    ''' <summary>リンクボタンの表示を設定</summary>
    Public Sub SetUpDownHyouji(ByVal blnHyoujiFlg As Boolean)
        If blnHyoujiFlg = True Then
            Me.btnSortAitesakiSyubetuUp.Visible = True
            Me.btnSortAitesakiSyubetuDown.Visible = True
            Me.btnSortAitesakiCdUp.Visible = True
            Me.btnSortAitesakiCdDown.Visible = True
            Me.btnSortAitesakiMeiUp.Visible = True
            Me.btnSortAitesakiMeiDown.Visible = True
            Me.btnSortSyouhinCdUp.Visible = True
            Me.btnSortSyouhinCdDown.Visible = True
            Me.btnSortSyouhinMeiUp.Visible = True
            Me.btnSortSyouhinMeiDown.Visible = True
            Me.btnSortTyousaUp.Visible = True
            Me.btnSortTyousaDown.Visible = True
            'Me.btnSortTokubetuCdUp.Visible = True
            'Me.btnSortTokubetuCdDown.Visible = True
            Me.btnSortTokubetuMeiUp.Visible = True
            Me.btnSortTokubetuMeiDown.Visible = True
            Me.btnSortTorikesiUp.Visible = True
            Me.btnSortTorikesiDown.Visible = True
            Me.btnSortKingakuAddScdUp.Visible = True
            Me.btnSortKingakuAddScdDown.Visible = True
            Me.btnSortSyokiTiUp.Visible = True
            Me.btnSortSyokiTiDown.Visible = True
            Me.btnSortRequestAddKingakuUp.Visible = True
            Me.btnSortRequestAddKingakuDown.Visible = True
            Me.btnSortKoumuAddKingakuUp.Visible = True
            Me.btnSortKoumuAddKingakuDown.Visible = True
        Else
            Me.btnSortAitesakiSyubetuUp.Visible = False
            Me.btnSortAitesakiSyubetuDown.Visible = False
            Me.btnSortAitesakiCdUp.Visible = False
            Me.btnSortAitesakiCdDown.Visible = False
            Me.btnSortAitesakiMeiUp.Visible = False
            Me.btnSortAitesakiMeiDown.Visible = False
            Me.btnSortSyouhinCdUp.Visible = False
            Me.btnSortSyouhinCdDown.Visible = False
            Me.btnSortSyouhinMeiUp.Visible = False
            Me.btnSortSyouhinMeiDown.Visible = False
            Me.btnSortTyousaUp.Visible = False
            Me.btnSortTyousaDown.Visible = False
            'Me.btnSortTokubetuCdUp.Visible = False
            'Me.btnSortTokubetuCdDown.Visible = False
            Me.btnSortTokubetuMeiUp.Visible = False
            Me.btnSortTokubetuMeiDown.Visible = False
            Me.btnSortTorikesiUp.Visible = False
            Me.btnSortTorikesiDown.Visible = False
            Me.btnSortKingakuAddScdUp.Visible = False
            Me.btnSortKingakuAddScdDown.Visible = False
            Me.btnSortSyokiTiUp.Visible = False
            Me.btnSortSyokiTiDown.Visible = False
            Me.btnSortRequestAddKingakuUp.Visible = False
            Me.btnSortRequestAddKingakuDown.Visible = False
            Me.btnSortKoumuAddKingakuUp.Visible = False
            Me.btnSortKoumuAddKingakuDown.Visible = False
        End If
    End Sub

    ''' <summary>検索明細項目のソート処理</summary> 
    Private Sub btnSort_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSortAitesakiSyubetuUp.Click _
                                                                                            , btnSortAitesakiSyubetuDown.Click _
                                                                                            , btnSortAitesakiCdUp.Click _
                                                                                            , btnSortAitesakiCdDown.Click _
                                                                                            , btnSortAitesakiMeiUp.Click _
                                                                                            , btnSortAitesakiMeiDown.Click _
                                                                                            , btnSortSyouhinCdUp.Click _
                                                                                            , btnSortSyouhinCdDown.Click _
                                                                                            , btnSortSyouhinMeiUp.Click _
                                                                                            , btnSortSyouhinMeiDown.Click _
                                                                                            , btnSortTyousaUp.Click _
                                                                                            , btnSortTyousaDown.Click _
                                                                                            , btnSortTokubetuMeiUp.Click _
                                                                                            , btnSortTokubetuMeiDown.Click _
                                                                                            , btnSortTorikesiUp.Click _
                                                                                            , btnSortTorikesiDown.Click _
                                                                                            , btnSortKingakuAddScdUp.Click _
                                                                                            , btnSortKingakuAddScdDown.Click _
                                                                                            , btnSortSyokiTiUp.Click _
                                                                                            , btnSortSyokiTiDown.Click _
                                                                                            , btnSortRequestAddKingakuUp.Click _
                                                                                            , btnSortRequestAddKingakuDown.Click _
                                                                                            , btnSortKoumuAddKingakuUp.Click _
                                                                                            , btnSortKoumuAddKingakuDown.Click
        Dim strSort As String = String.Empty
        Dim strUpDown As String = String.Empty

        'リンクボタンの色を設定
        Call Me.setUpDownColor()

        '画面にソート順をクリック時
        Select Case CType(sender, LinkButton).ID
            Case btnSortAitesakiSyubetuUp.ID     '--相手先種別で昇順ソート
                strSort = "aitesaki_syubetu"
                strUpDown = "ASC"
                btnSortAitesakiSyubetuUp.ForeColor = Drawing.Color.IndianRed
            Case btnSortAitesakiSyubetuDown.ID   '--相手先種別で降順ソート
                strSort = "aitesaki_syubetu"
                strUpDown = "DESC"
                btnSortAitesakiSyubetuDown.ForeColor = Drawing.Color.IndianRed
            Case btnSortAitesakiCdUp.ID    '--相手先コードで昇順ソート
                strSort = "aitesaki_cd"
                strUpDown = "ASC"
                btnSortAitesakiCdUp.ForeColor = Drawing.Color.IndianRed
            Case btnSortAitesakiCdDown.ID  '--相手先コードで降順ソート
                strSort = "aitesaki_cd"
                strUpDown = "DESC"
                btnSortAitesakiCdDown.ForeColor = Drawing.Color.IndianRed
            Case btnSortAitesakiMeiUp.ID    '--相手先名で昇順ソート
                strSort = "aitesaki_name"
                strUpDown = "ASC"
                btnSortAitesakiMeiUp.ForeColor = Drawing.Color.IndianRed
            Case btnSortAitesakiMeiDown.ID  '--相手先名で降順ソート
                strSort = "aitesaki_name"
                strUpDown = "DESC"
                btnSortAitesakiMeiDown.ForeColor = Drawing.Color.IndianRed
            Case btnSortSyouhinCdUp.ID  '--商品コードで昇順ソート
                strSort = "syouhin_cd"
                strUpDown = "ASC"
                btnSortSyouhinCdUp.ForeColor = Drawing.Color.IndianRed
            Case btnSortSyouhinCdDown.ID    '--商品コードで降順ソート
                strSort = "syouhin_cd"
                strUpDown = "DESC"
                btnSortSyouhinCdDown.ForeColor = Drawing.Color.IndianRed
            Case btnSortSyouhinMeiUp.ID '--商品名称で昇順ソート
                strSort = "syouhin_mei"
                strUpDown = "ASC"
                btnSortSyouhinMeiUp.ForeColor = Drawing.Color.IndianRed
            Case btnSortSyouhinMeiDown.ID   '--商品名称で降順ソート
                strSort = "syouhin_mei"
                strUpDown = "DESC"
                btnSortSyouhinMeiDown.ForeColor = Drawing.Color.IndianRed
            Case btnSortTyousaUp.ID '--調査コードで昇順ソート
                strSort = "tys_houhou_no"
                strUpDown = "ASC"
                btnSortTyousaUp.ForeColor = Drawing.Color.IndianRed
            Case btnSortTyousaDown.ID   '--調査コードで降順ソート
                strSort = "tys_houhou_no"
                strUpDown = "DESC"
                btnSortTyousaDown.ForeColor = Drawing.Color.IndianRed
                'Case btnSortTokubetuCdUp.ID '--特別対応コードで昇順ソート
                '    strSort = "tokubetu_taiou_cd"
                '    strUpDown = "ASC"
                '    btnSortTokubetuCdUp.ForeColor = Drawing.Color.IndianRed
                'Case btnSortTokubetuCdDown.ID   '--特別対応コードで降順ソート
                '    strSort = "tokubetu_taiou_cd"
                '    strUpDown = "DESC"
                '    btnSortTokubetuCdDown.ForeColor = Drawing.Color.IndianRed
            Case btnSortTokubetuMeiUp.ID    '--特別対応名称で昇順ソート
                strSort = "tokubetu_taiou_meisyou"
                strUpDown = "ASC"
                btnSortTokubetuMeiUp.ForeColor = Drawing.Color.IndianRed
            Case btnSortTokubetuMeiDown.ID  '--特別対応名称で降順ソート
                strSort = "tokubetu_taiou_meisyou"
                strUpDown = "DESC"
                btnSortTokubetuMeiDown.ForeColor = Drawing.Color.IndianRed
            Case btnSortTorikesiUp.ID   '--取消で昇順ソート
                strSort = "torikesi"
                strUpDown = "ASC"
                btnSortTorikesiUp.ForeColor = Drawing.Color.IndianRed
            Case btnSortTorikesiDown.ID '--取消で降順ソート
                strSort = "torikesi"
                strUpDown = "DESC"
                btnSortTorikesiDown.ForeColor = Drawing.Color.IndianRed
            Case btnSortKingakuAddScdUp.ID
                strSort = "kasan_syouhin_cd"
                strUpDown = "ASC"
                btnSortKingakuAddScdUp.ForeColor = Drawing.Color.IndianRed
            Case btnSortKingakuAddScdDown.ID
                strSort = "kasan_syouhin_cd"
                strUpDown = "DESC"
                btnSortKingakuAddScdDown.ForeColor = Drawing.Color.IndianRed
            Case btnSortSyokiTiUp.ID
                strSort = "syokiti"
                strUpDown = "ASC"
                btnSortSyokiTiUp.ForeColor = Drawing.Color.IndianRed
            Case btnSortSyokiTiDown.ID
                strSort = "syokiti"
                strUpDown = "DESC"
                btnSortSyokiTiDown.ForeColor = Drawing.Color.IndianRed
            Case btnSortRequestAddKingakuUp.ID
                strSort = "uri_kasan_gaku"
                strUpDown = "ASC"
                btnSortRequestAddKingakuUp.ForeColor = Drawing.Color.IndianRed
            Case btnSortRequestAddKingakuDown.ID
                strSort = "uri_kasan_gaku"
                strUpDown = "DESC"
                btnSortRequestAddKingakuDown.ForeColor = Drawing.Color.IndianRed
            Case btnSortKoumuAddKingakuUp.ID
                strSort = "koumuten_kasan_gaku"
                strUpDown = "ASC"
                btnSortKoumuAddKingakuUp.ForeColor = Drawing.Color.IndianRed
            Case btnSortKoumuAddKingakuDown.ID
                strSort = "koumuten_kasan_gaku"
                strUpDown = "DESC"
                btnSortKoumuAddKingakuDown.ForeColor = Drawing.Color.IndianRed

        End Select

        '画面縦スクロールを設定する
        scrollHeight = ViewState("scrollHeight")

        '画面データのソート順を設定する
        Dim dvKameitenInfo As Data.DataView = CType(ViewState("dtTokubetuTaiou"), Data.DataTable).DefaultView
        dvKameitenInfo.Sort = strSort & " " & strUpDown

        Me.grdMeisaiLeft.DataSource = dvKameitenInfo
        Me.grdMeisaiLeft.DataBind()
        Me.grdMeisaiRight.DataSource = dvKameitenInfo
        Me.grdMeisaiRight.DataBind()

        '明細の背景色を設定する
        SetMeisaiBackColor()

    End Sub

    ''' <summary>「検索実行」ボタンを押下時</summary>
    Protected Sub benKensaku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKensaku.Click

        '入力チェック
        Dim strObjId As String = String.Empty
        Dim strErrMessage As String = CheckInput(strObjId)
        If strErrMessage <> String.Empty Then
            ShowMessage(strErrMessage, strObjId)
            Exit Sub
        End If

        Dim dtTokubetuTaiou As Data.DataTable 'データ明細
        Dim intCount As Integer    '検索件数
        '検索条件を設定する
        Dim dtParam As Dictionary(Of String, String) = SetKensakuJyouken()

        If Me.chkSiteiNasiTaisyou.Checked Then
            '「系列・営業所・指定無しも対象チェックボックス」=未チェックの場合

            '検索データを取得する
            dtTokubetuTaiou = tokubetuTaiouMasterLogic.GetTokubetuTaiouNasiInfo(dtParam)
            '検索件数を取得する
            intCount = tokubetuTaiouMasterLogic.GetTokubetuTaiouNasiCount(dtParam)
        Else
            '「系列・営業所・指定無しも対象チェックボックス」=チェックの場合（指定加盟店の系列・営業所と指定なしも対象）

            '検索データを取得する
            dtTokubetuTaiou = tokubetuTaiouMasterLogic.GetTokubetuTaiouJyouhou(dtParam)
            '検索件数を取得する
            intCount = tokubetuTaiouMasterLogic.GetTokubetuTaiouCount(dtParam)
        End If

        '検索結果を設定する
        Me.grdMeisaiLeft.DataSource = dtTokubetuTaiou
        Me.grdMeisaiLeft.DataBind()
        Me.grdMeisaiRight.DataSource = dtTokubetuTaiou
        Me.grdMeisaiRight.DataBind()

        '明細の背景色を設定する
        SetMeisaiBackColor()

        '画面データを設定する
        'Call SetGamenData()
        Call Me.SetGamenData(Me.ddlAitesakiSyubetu.SelectedValue.Trim, Me.tbxKameitenCdFrom.Text.Trim, Me.tbxKameitenCdTo.Text.Trim, IIf(Me.chkTorikesi.Checked, "1", String.Empty))
        '検索結果件数を設定する
        Call SetKensakuKekka(intCount)

        '報告書様式を設定する
        Call SetHoukokusyo(dtTokubetuTaiou)

        If intCount = 0 Then
            'ソート順ボタンを設定する
            Call SetUpDownHyouji(False)
            ShowMessage(Messages.Instance.MSG020E, String.Empty)
        Else
            'ソート順ボタンを設定する
            Call SetUpDownHyouji(True)
            'ソート順ボタン色を設定する
            Call setUpDownColor()
            ViewState("dtTokubetuTaiou") = dtTokubetuTaiou

        End If
        ViewState("scrollHeight") = scrollHeight
    End Sub

    '''<summary>「CSV出力」ボタンを押下する</summary>
    Protected Sub btnCSVOutput_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCSVOutput.Click

        '入力チェック
        Dim strObjId As String = String.Empty
        Dim strErrMessage As String = CheckInput(strObjId)
        If strErrMessage <> String.Empty Then
            ShowMessage(strErrMessage, strObjId)
            Exit Sub
        End If

        '検索件数
        Dim intCount As Long
        '    '検索条件を設定する
        Dim dtParam As Dictionary(Of String, String) = SetKensakuJyouken()

        '最大CSV出力数
        Dim intCsvMax As Integer = CInt(System.Configuration.ConfigurationManager.AppSettings("CsvDownMax"))

        '画面データを設定する
        'Call SetGamenData()
        Call Me.SetGamenData(Me.ddlAitesakiSyubetu.SelectedValue.Trim, Me.tbxKameitenCdFrom.Text.Trim, Me.tbxKameitenCdTo.Text.Trim, IIf(Me.chkTorikesi.Checked, "1", String.Empty))

        If Me.chkMisetteimo.Checked Then
            '検索結果をクリアする
            Me.grdMeisaiLeft.DataSource = Nothing
            Me.grdMeisaiLeft.DataBind()
            Me.grdMeisaiRight.DataSource = Nothing
            Me.grdMeisaiRight.DataBind()

            '検索結果件数を設定する
            Call SetKensakuKekka(0)

            'リンクボタンの表示を設定
            Call Me.SetUpDownHyouji(False)

            '未設定も含む加盟店商品調査方法特別対応CSVデータ件数を設定する
            intCount = tokubetuTaiouMasterLogic.GetTokubetuTaiouCSVCount(dtParam)
        Else
            '検索データを取得する
            Dim dtTokubetuTaiou As Data.DataTable = tokubetuTaiouMasterLogic.GetTokubetuTaiouJyouhou(dtParam)

            '検索データを取得する
            Me.grdMeisaiLeft.DataSource = dtTokubetuTaiou
            Me.grdMeisaiLeft.DataBind()
            Me.grdMeisaiRight.DataSource = dtTokubetuTaiou
            Me.grdMeisaiRight.DataBind()

            '明細の背景色を設定する
            SetMeisaiBackColor()

            '検索件数を取得する
            intCount = tokubetuTaiouMasterLogic.GetTokubetuTaiouCount(dtParam)

            '検索結果件数を設定する
            Call SetKensakuKekka(intCount)

            If intCount = 0 Then
                'ソート順ボタンを設定する
                Call SetUpDownHyouji(False)
            Else
                'ソート順ボタンを設定する
                Call SetUpDownHyouji(True)
                'ソート順ボタン色を設定する
                Call setUpDownColor()
                ViewState("dtTokubetuTaiou") = dtTokubetuTaiou
            End If
        End If

        ViewState("scrollHeight") = scrollHeight

        If intCount > intCsvMax Then
            ShowMessage(Messages.Instance.MSG051E.Replace("@PARAM1", CStr(intCsvMax)), String.Empty)
        Else
            Me.hidCSVFlg.Value = "1"
            ClientScript.RegisterStartupScript(Me.GetType, "", "<script language=javascript>document.forms[0].submit();</script>")
        End If
    End Sub

    '''' <summary>画面データを設定する</summary>
    'Private Sub SetGamenData()
    '    '加盟店コードFromによって、名を設定
    '    If Me.tbxKameitenCdFrom.Text.ToString.Trim <> String.Empty Then

    '        Dim dtKameitenMeiFrom As New Data.DataTable
    '        dtKameitenMeiFrom = commonSearchLogic.GetKameitenKensakuInfo("1", "", "" & Me.tbxKameitenCdFrom.Text.Trim & "", "", False)

    '        If dtKameitenMeiFrom.Rows.Count > 0 Then
    '            Me.tbxKameitenMeiFrom.Text = dtKameitenMeiFrom.Rows(0).Item("kameiten_mei1")
    '        Else
    '            Me.tbxKameitenMeiFrom.Text = String.Empty
    '        End If
    '    Else
    '        Me.tbxKameitenMeiFrom.Text = String.Empty
    '    End If
    '    '加盟店コードToによって、名を設定
    '    If Me.tbxKameitenCdTo.Text.ToString.Trim <> String.Empty Then

    '        Dim dtKameitenMeiTo As New Data.DataTable
    '        dtKameitenMeiTo = commonSearchLogic.GetKameitenKensakuInfo("1", "", "" & Me.tbxKameitenCdTo.Text.Trim & "", "", False)

    '        If dtKameitenMeiTo.Rows.Count > 0 Then
    '            Me.tbxKameitenMeiTo.Text = dtKameitenMeiTo.Rows(0).Item("kameiten_mei1")
    '        Else
    '            Me.tbxKameitenMeiTo.Text = String.Empty
    '        End If
    '    Else
    '        Me.tbxKameitenMeiTo.Text = String.Empty
    '    End If
    '    '特別対応コードによって、名称を設定
    '    If Me.tbxTokubetuTaiouCd.Text.ToString.Trim <> String.Empty Then

    '        Dim dtTokubetuMei As New Data.DataTable
    '        dtTokubetuMei = commonSearchLogic.GetTokubetuKaiouInfo("1", "" & Me.tbxTokubetuTaiouCd.Text.Trim & "", "", False)

    '        If dtTokubetuMei.Rows.Count > 0 Then
    '            Me.tbxTokubetuTaiouMei.Text = dtTokubetuMei.Rows(0).Item("tokubetu_taiou_meisyou")
    '        Else
    '            Me.tbxTokubetuTaiouMei.Text = String.Empty
    '        End If
    '    Else
    '        Me.tbxTokubetuTaiouMei.Text = String.Empty
    '    End If
    'End Sub

    ''' <summary>画面データを設定する</summary>
    Private Sub SetGamenData(ByVal strAitesakiSyobetu As String, ByVal strAitesakiFromCd As String, ByVal strAitesakiToCd As String, ByVal strTorikesiAitesaki As String)

        If Me.ddlAitesakiSyubetu.SelectedValue.Trim.Equals(String.Empty) OrElse Me.ddlAitesakiSyubetu.SelectedValue.Trim.Equals("0") OrElse Me.ddlAitesakiSyubetu.SelectedValue.Trim.Equals("3") Then
            '相手先コードFROM
            Me.tbxKameitenCdFrom.Text = String.Empty
            '相手先名FROM
            Me.tbxKameitenMeiFrom.Text = String.Empty
            '相手先コードTO
            Me.tbxKameitenCdTo.Text = String.Empty
            '相手先名TO
            Me.tbxKameitenMeiTo.Text = String.Empty

            Me.divAitesakiSyubetu.Attributes.Add("style", "display:none;")
        Else

            If Not strAitesakiFromCd.Trim.Equals(String.Empty) Then
                '相手先コードFROM
                Me.tbxKameitenCdFrom.Text = strAitesakiFromCd

                '相手先名を取得
                Dim dtAitesakiMei As New Data.DataTable
                dtAitesakiMei = tokubetuTaiouMasterLogic.GetAitesakiMei(CInt(strAitesakiSyobetu), strAitesakiFromCd, strTorikesiAitesaki)

                '相手先名FROM
                If dtAitesakiMei.Rows.Count > 0 Then
                    Me.tbxKameitenMeiFrom.Text = dtAitesakiMei.Rows(0).Item("aitesaki_mei").ToString.Trim
                Else
                    'Me.tbxAiteSakiCdFrom.Text = String.Empty
                    Me.tbxKameitenMeiFrom.Text = String.Empty
                End If
            Else
                Me.tbxKameitenMeiFrom.Text = String.Empty
            End If


            If Not strAitesakiToCd.Trim.Equals(String.Empty) Then
                '相手先コードTO
                Me.tbxKameitenCdTo.Text = strAitesakiToCd

                '相手先名を取得
                Dim dtAitesakiMei As New Data.DataTable
                dtAitesakiMei = tokubetuTaiouMasterLogic.GetAitesakiMei(CInt(strAitesakiSyobetu), strAitesakiToCd, strTorikesiAitesaki)

                '相手先名TO
                If dtAitesakiMei.Rows.Count > 0 Then
                    Me.tbxKameitenMeiTo.Text = dtAitesakiMei.Rows(0).Item("aitesaki_mei").ToString.Trim
                Else
                    'Me.tbxAiteSakiCdTo.Text = String.Empty
                    Me.tbxKameitenMeiTo.Text = String.Empty
                End If
            Else
                Me.tbxKameitenMeiTo.Text = String.Empty

            End If

            Me.divAitesakiSyubetu.Attributes.Add("style", "display:block;")
        End If

        '特別対応コードによって、名称を設定
        If Me.tbxTokubetuTaiouCd.Text.ToString.Trim <> String.Empty Then

            Dim dtTokubetuMei As New Data.DataTable
            dtTokubetuMei = commonSearchLogic.GetTokubetuKaiouInfo("1", "" & Me.tbxTokubetuTaiouCd.Text.Trim & "", "", False)

            If dtTokubetuMei.Rows.Count > 0 Then
                Me.tbxTokubetuTaiouMei.Text = dtTokubetuMei.Rows(0).Item("tokubetu_taiou_meisyou")
            Else
                Me.tbxTokubetuTaiouMei.Text = String.Empty
            End If
        Else
            Me.tbxTokubetuTaiouMei.Text = String.Empty
        End If

    End Sub

    '''<summary>CSVファイルを作成</summary>
    Private Sub MakeCSVFile()

        '検索条件を設定する
        Dim dtParamList As Dictionary(Of String, String) = SetKensakuJyouken()
        Dim dtTokubetuTaiouCSV As New Data.DataTable

        If Me.chkMisetteimo.Checked = True Then

            dtTokubetuTaiouCSV = tokubetuTaiouMasterLogic.GetTokubetuTaiouCSV(dtParamList)

        Else

            dtTokubetuTaiouCSV = tokubetuTaiouMasterLogic.GetTokubetuTaiouJyouhouCSV(dtParamList)

        End If

        'CSVファイル名設定
        Dim strFileName As String = System.Configuration.ConfigurationManager.AppSettings("TokubetuTaiouMasterCsv").ToString

        Response.Buffer = True
        Dim writer As New CsvWriter(Me.Response.OutputStream, Encoding.GetEncoding(932), ",", vbCrLf)

        'CSVファイルヘッダ設定
        writer.WriteLine(EarthConst.conTokubetuTaiouCsvHeader)

        'CSVファイル内容設定
        For Each row As Data.DataRow In dtTokubetuTaiouCSV.Rows
            writer.WriteLine(row.Item(0), row.Item(1), row.Item(2), row.Item(3), row.Item(4), row.Item(5), _
            row.Item(6), row.Item(7), row.Item(8), row.Item(9), row.Item(10), row.Item(11), row.Item(12), row.Item(13), row.Item(14))
        Next

        'CSVファイルダウンロード
        Response.Charset = "utf-8"
        Response.ContentType = "text/plain"
        Response.AddHeader("Content-Disposition", "attachment; filename=" & HttpUtility.UrlEncode(strFileName))
        Response.End()

    End Sub

    '''<summary>画面に検索条件を設定</summary>
    Public Function SetKensakuJyouken() As Dictionary(Of String, String)
        Dim dtParamlist As New Dictionary(Of String, String)
        dtParamlist.Add("kensuu", CStr(Me.ddlKensuu.SelectedValue))                             '検索上限件数
        dtParamlist.Add("aitesakiSyubetu", CStr(Me.ddlAitesakiSyubetu.SelectedValue.Trim))      '相手先種別
        dtParamlist.Add("aitesakiCdFrom", CStr(Me.tbxKameitenCdFrom.Text.Trim))                 '相手先コードFrom
        dtParamlist.Add("aitesakiCdTo", CStr(Me.tbxKameitenCdTo.Text.Trim))                     '相手先コードTo
        dtParamlist.Add("syouhin_cd", CStr(Me.ddlSyouhinCd.SelectedValue))                      '商品コード
        dtParamlist.Add("tys_houhou_no", CStr(Me.ddlTyousaHouhou.SelectedValue))                '調査方法No
        dtParamlist.Add("tokubetu_taiou_cd", CStr(Me.tbxTokubetuTaiouCd.Text.Trim))             '特別対応コード
        If Me.chkTorikesi.Checked = True Then
            dtParamlist.Add("torikesiFlg", 1)   '取消は検索対象外
        Else
            dtParamlist.Add("torikesiFlg", 0)   '取消も含む
        End If

        dtParamlist.Add("aitesakiTorikesiFlg", IIf(Me.chkAitesakiTaisyouGai.Checked, "1", "0")) '取消相手先は対象外
        dtParamlist.Add("kingaku0TorikesiFlg", IIf(Me.chk0TaisyouGai.Checked, "1", "0"))        '\0は対象外

        '------------------From 2013.03.09    李宇追加する-----------------
        dtParamlist.Add("Syokiti1Nomi", IIf(Me.chkSyokiti.Checked, "1", "0"))        '初期値1のみ
        '------------------To   2013.03.09    李宇追加する-----------------

        Return dtParamlist
    End Function

    ''' <summary>入力チェック</summary>
    ''' <param name="strObjId">クライアントID</param>
    ''' <returns>エラーメッセージ</returns>
    Public Function CheckInput(ByRef strObjId As String) As String
        Dim csScript As New StringBuilder
        With csScript
            '加盟店コード(From)
            If Me.tbxKameitenCdFrom.Text <> String.Empty Then
                .Append(commonCheck.ChkHankakuEisuuji(Me.tbxKameitenCdFrom.Text, "加盟店コード(From)"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxKameitenCdFrom.ClientID
                End If
            End If
            '加盟店コード(To)
            If Me.tbxKameitenCdTo.Text <> String.Empty Then
                .Append(commonCheck.ChkHankakuEisuuji(Me.tbxKameitenCdTo.Text, "加盟店コード(To)"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxKameitenCdTo.ClientID
                End If
            End If
            '加盟店コード範囲
            If Me.tbxKameitenCdFrom.Text <> String.Empty And Me.tbxKameitenCdTo.Text <> String.Empty Then
                If commonCheck.ChkHankakuEisuuji(Me.tbxKameitenCdFrom.Text, "加盟店コード(From)") = String.Empty _
                   And commonCheck.ChkHankakuEisuuji(Me.tbxKameitenCdTo.Text, "加盟店コード(To)") = String.Empty Then
                    If Me.tbxKameitenCdFrom.Text > Me.tbxKameitenCdTo.Text Then
                        .Append(String.Format(Messages.Instance.MSG2012E, "加盟店コード", "加盟店コード").ToString)
                        If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                            strObjId = Me.tbxKameitenCdFrom.ClientID
                        End If
                    End If
                End If
            End If
            '特別対応コード
            If Me.tbxTokubetuTaiouCd.Text <> String.Empty Then
                .Append(commonCheck.CheckHankaku(Me.tbxTokubetuTaiouCd.Text, "特別対応コード"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxTokubetuTaiouCd.ClientID
                End If
            End If
        End With

        Return csScript.ToString

    End Function

    ''' <summary>検索結果を設定</summary>
    Private Sub SetKensakuKekka(ByVal intCount As Integer)
        If Me.ddlKensuu.SelectedValue = "max" Then
            Me.lblCount.Text = CStr(intCount)
            Me.lblCount.ForeColor = Drawing.Color.Black
            scrollHeight = intCount * 43 + 1
        Else
            If intCount > Convert.ToInt64(Me.ddlKensuu.SelectedValue) Then
                Me.lblCount.Text = CStr(Me.ddlKensuu.SelectedValue) & "/" & CStr(intCount)
                Me.lblCount.ForeColor = Drawing.Color.Red
                scrollHeight = Me.ddlKensuu.SelectedValue * 43 + 1
            Else
                Me.lblCount.Text = CStr(intCount)
                Me.lblCount.ForeColor = Drawing.Color.Black
                scrollHeight = intCount * 43 + 1
            End If
        End If
    End Sub

    ''' <summary>JavaScriptを作成</summary>
    Protected Sub MakeJavaScript()
        Dim sbScript As New StringBuilder
        With sbScript
            .AppendLine("<script type='text/javascript' language='javascript'>")
            '特別対応検索画面をポップアップする
            .AppendLine("   function fncSetTokubetuKaiou(){")
            .AppendLine("       var objCd = '" & Me.tbxTokubetuTaiouCd.ClientID & "';")
            .AppendLine("       var objMei = '" & Me.tbxTokubetuTaiouMei.ClientID & "';")
            .AppendLine("       var strCd = document.getElementById('" & Me.tbxTokubetuTaiouCd.ClientID & "').value;")
            .AppendLine("       var FormName = '" & Me.Form.Name & "';")
            .AppendLine("       window.open('search_tokubetu_taiou.aspx?blnDelete=False&FormName='+FormName+'&objCd='+objCd+'&objMei='+objMei+'&strCd='+strCd,'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');")
            .AppendLine("   }")
            'CSV取込画面をポップアップする
            .AppendLine("   function fncCSVTorikomi(){")
            .AppendLine("       window.open('TokubetuTaiouMasterInput.aspx', 'CSVTorikomi','menubar=yes,toolbar=yes,location=yes,status=yes,resizable=yes,scrollbars=yes');")
            .AppendLine("       return false;")
            .AppendLine("   }")
            '加盟店検索画面をポップアップする
            '.AppendLine("   function fncSetKameiten(objCd,strCdKbn,objMei){")
            '.AppendLine("       var strCdFrom = document.getElementById('" & Me.tbxKameitenCdFrom.ClientID & "').value;")
            '.AppendLine("       var strCdTo = document.getElementById('" & Me.tbxKameitenCdTo.ClientID & "').value;")
            '.AppendLine("       var FormName = '" & Me.Form.Name & "';")
            '.AppendLine("       if(strCdKbn == 1){")
            '.AppendLine("           window.open('search_common.aspx?blnDelete=False&FormName='+FormName+'&objCd='+objCd+'&strCd='+strCdFrom+'&objMei='+objMei+'&Kbn='+escape('加盟店'),'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');")
            '.AppendLine("       }")
            '.AppendLine("       else{")
            '.AppendLine("           window.open('search_common.aspx?blnDelete=False&FormName='+FormName+'&objCd='+objCd+'&strCd='+strCdTo+'&objMei='+objMei+'&Kbn='+escape('加盟店'),'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');")
            '.AppendLine("       }")
            '.AppendLine("   }")

            '相手先検索を押下する場合、ポップアップを起動する
            .AppendLine("function fncAiteSakiSearch(strAiteSakiKbn){")
            '相手先種別が「1:加盟店」の場合、加盟店ポップアップを起動する
            .AppendLine("   if(document.all." & Me.ddlAitesakiSyubetu.ClientID & ".value == '1'){")
            .AppendLine("       var strkbn='加盟店';")
            .AppendLine("       var strClientCdID; ")
            .AppendLine("       var strClientMeiID; ")
            .AppendLine("       var blnTorikesi; ")
            .AppendLine("       if(strAiteSakiKbn == '1'){")
            .AppendLine("           strClientCdID = '" & Me.tbxKameitenCdFrom.ClientID & "';")
            .AppendLine("           strClientMeiID = '" & Me.tbxKameitenMeiFrom.ClientID & "';")
            .AppendLine("       }else{")
            .AppendLine("           strClientCdID = '" & Me.tbxKameitenCdTo.ClientID & "';")
            .AppendLine("           strClientMeiID = '" & Me.tbxKameitenMeiTo.ClientID & "';")
            .AppendLine("       }")
            .AppendLine("       if(document.all." & Me.chkTorikesi.ClientID & ".checked){")
            .AppendLine("           blnTorikesi = 'True';")
            .AppendLine("       }else{")
            .AppendLine("           blnTorikesi = 'False';")
            .AppendLine("       }")
            .AppendLine("       objSrchWin = window.open('search_common.aspx?Kbn='+escape(strkbn)+'&FormName=" & Me.Form.Name & _
                                "&objCd='+strClientCdID+'&objMei='+strClientMeiID+'&strCd='+escape(eval('document.all.'+strClientCdID).value)+'&strMei='+escape(eval('document.all.'+strClientMeiID).value)+")
            .AppendLine("       '&blnDelete='+blnTorikesi, ")
            .AppendLine("       'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');")
            .AppendLine("       return false;")
            .AppendLine("   }else if(document.all." & Me.ddlAitesakiSyubetu.ClientID & ".value == '5'){")
            '相手先種別が「5:営業所」の場合、営業所ポップアップを起動する
            .AppendLine("       var strkbn='営業所';")
            .AppendLine("       var strClientCdID; ")
            .AppendLine("       var strClientMeiID; ")
            .AppendLine("       var blnTorikesi; ")
            .AppendLine("       if(strAiteSakiKbn == '1'){")
            .AppendLine("           strClientCdID = '" & Me.tbxKameitenCdFrom.ClientID & "';")
            .AppendLine("           strClientMeiID = '" & Me.tbxKameitenMeiFrom.ClientID & "';")
            .AppendLine("       }else{")
            .AppendLine("           strClientCdID = '" & Me.tbxKameitenCdTo.ClientID & "';")
            .AppendLine("           strClientMeiID = '" & Me.tbxKameitenMeiTo.ClientID & "';")
            .AppendLine("       }")
            .AppendLine("       if(document.all." & Me.chkTorikesi.ClientID & ".checked){")
            .AppendLine("           blnTorikesi = 'True';")
            .AppendLine("       }else{")
            .AppendLine("           blnTorikesi = 'False';")
            .AppendLine("       }")
            .AppendLine("       objSrchWin = window.open('search_common.aspx?Kbn='+escape(strkbn)+'&FormName=" & Me.Form.Name & _
                                "&objCd='+strClientCdID+'&objMei='+strClientMeiID+'&strCd='+escape(eval('document.all.'+strClientCdID).value)+'&strMei='+escape(eval('document.all.'+strClientMeiID).value)+")
            .AppendLine("       '&blnDelete='+blnTorikesi, ")
            .AppendLine("       'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');")
            .AppendLine("       return false;")
            .AppendLine("   }else if(document.all." & Me.ddlAitesakiSyubetu.ClientID & ".value == '7'){")
            '相手先種別が「7:系列」の場合、系列ポップアップを起動する
            .AppendLine("       var strkbn='系列';")
            .AppendLine("       var strClientCdID; ")
            .AppendLine("       var strClientMeiID; ")
            .AppendLine("       var blnTorikesi; ")
            .AppendLine("       if(strAiteSakiKbn == '1'){")
            .AppendLine("           strClientCdID = '" & Me.tbxKameitenCdFrom.ClientID & "';")
            .AppendLine("           strClientMeiID = '" & Me.tbxKameitenMeiFrom.ClientID & "';")
            .AppendLine("       }else{")
            .AppendLine("           strClientCdID = '" & Me.tbxKameitenCdTo.ClientID & "';")
            .AppendLine("           strClientMeiID = '" & Me.tbxKameitenMeiTo.ClientID & "';")
            .AppendLine("       }")
            .AppendLine("       if(document.all." & Me.chkTorikesi.ClientID & ".checked){")
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

            '「クリア」ボタンの処理
            .AppendLine("   function fncClear(){")
            .AppendLine("       document.getElementById('" & Me.tbxKameitenCdFrom.ClientID & "').value='';")
            .AppendLine("       document.getElementById('" & Me.tbxKameitenMeiFrom.ClientID & "').value='';")
            .AppendLine("       document.getElementById('" & Me.tbxKameitenCdTo.ClientID & "').value='';")
            .AppendLine("       document.getElementById('" & Me.tbxKameitenMeiTo.ClientID & "').value='';")
            .AppendLine("       document.getElementById('" & Me.ddlSyouhinCd.ClientID & "').selectedIndex=0;")
            .AppendLine("       document.getElementById('" & Me.ddlTyousaHouhou.ClientID & "').selectedIndex=0;")
            .AppendLine("       document.getElementById('" & Me.tbxTokubetuTaiouCd.ClientID & "').value='';")
            .AppendLine("       document.getElementById('" & Me.tbxTokubetuTaiouMei.ClientID & "').value='';")
            .AppendLine("       document.getElementById('" & Me.ddlKensuu.ClientID & "').selectedIndex=1;")
            .AppendLine("       document.getElementById('" & Me.chkTorikesi.ClientID & "').checked=true;")
            .AppendLine("       document.all." & Me.chkAitesakiTaisyouGai.ClientID & ".checked = true;")
            .AppendLine("       document.getElementById('" & Me.chkMisetteimo.ClientID & "').checked=false;")
            .AppendLine("       document.all." & Me.divSitei.ClientID & ".style.display = 'none';")
            .AppendLine("       document.getElementById('" & Me.btnKensaku.ClientID & "').disabled=false;")
            .AppendLine("       document.all." & Me.divCsvOutput.ClientID & ".style.display = 'block';")

            .AppendLine("       document.all." & Me.chk0TaisyouGai.ClientID & ".checked = false;")            '相手先種別
            .AppendLine("       document.all." & Me.ddlAitesakiSyubetu.ClientID & ".value = '';")
            '相手先コードを表示しない
            .AppendLine("       document.all." & Me.divAitesakiSyubetu.ClientID & ".style.display = 'none';")
            .AppendLine("   }")
            '加盟店コード入力かどうかチェック
            .AppendLine("   function fncKameitenCdChk(strKbn){")
            .AppendLine("       var KameitenCdFrom = document.getElementById('" & Me.tbxKameitenCdFrom.ClientID & "');")
            .AppendLine("       var KameitenCdTo = document.getElementById('" & Me.tbxKameitenCdTo.ClientID & "');")
            .AppendLine("       var chkMisettei = document.getElementById('" & Me.chkMisetteimo.ClientID & "');")
            .AppendLine("       if(document.all." & Me.ddlAitesakiSyubetu.ClientID & ".value != '' && document.all." & Me.ddlAitesakiSyubetu.ClientID & ".value != '0' && KameitenCdFrom.value == '' && KameitenCdTo.value == ''){")
            .AppendLine("           alert('" & Messages.Instance.MSG041E.Replace("@PARAM1", "加盟店コードFROM").Replace("@PARAM2", "加盟店コードTO") & "');")
            .AppendLine("           KameitenCdFrom.focus();")
            .AppendLine("           return false;")
            .AppendLine("       }")
            '確認メッセージ表示
            .AppendLine("       if (strKbn == 'kensaku'){")
            .AppendLine("           if (document.all." & Me.ddlKensuu.ClientID & ".value=='max'){")
            .AppendLine("               if(!confirm('" & Messages.Instance.MSG007C & "')){")
            .AppendLine("                   return false; ")
            .AppendLine("               }")
            .AppendLine("           }")
            .AppendLine("       }")
            '相手先種別が必須入力チェック
            .AppendLine("   if (document.all." & Me.ddlAitesakiSyubetu.ClientID & ".selectedIndex=='0'){")
            .AppendLine("       alert('" & Messages.Instance.MSG013E.Replace("@PARAM1", "相手先種別") & "');")
            .AppendLine("       document.all." & Me.ddlAitesakiSyubetu.ClientID & ".focus();")
            .AppendLine("       return false; ")
            .AppendLine("   }")
            .AppendLine("       return true;")
            .AppendLine("   }")
            '「未設定も含む」チェックボックス
            .AppendLine("   function fncSetbtnKensaku(){")
            .AppendLine("   if(document.all." & Me.chkMisetteimo.ClientID & ".checked){")
            .AppendLine("       document.all." & Me.btnKensaku.ClientID & ".disabled = true;")
            .AppendLine("       document.all." & Me.divSitei.ClientID & ".style.display = 'none';")
            .AppendLine("       document.all." & Me.chkSiteiNasiTaisyou.ClientID & ".checked = false;")
            .AppendLine("       document.all." & Me.divCsvOutput.ClientID & ".style.display = 'block';")
            .AppendLine("       document.all." & Me.divAitesakiTo.ClientID & ".style.display = 'block';")
            .AppendLine("   }else{")
            .AppendLine("       document.all." & Me.btnKensaku.ClientID & ".disabled = false;")
            .AppendLine("       document.all." & Me.divAitesakiTo.ClientID & ".style.display = 'block';")
            .AppendLine("       if(document.all." & Me.ddlAitesakiSyubetu.ClientID & ".value == '1'){")
            .AppendLine("           document.all." & Me.divSitei.ClientID & ".style.display = 'block';")
            .AppendLine("       }")
            .AppendLine("   }")
            .AppendLine("    }")
            '「系列・営業所・指定無しも検索対象」チェックボックス処理
            .AppendLine("function fncChangeAite(){")
            .AppendLine("   if(document.all." & Me.chkSiteiNasiTaisyou.ClientID & ".checked){")
            .AppendLine("       document.all." & Me.divAitesakiTo.ClientID & ".style.display = 'none';")
            .AppendLine("       document.all." & Me.tbxKameitenCdTo.ClientID & ".value = '';")
            .AppendLine("       document.all." & Me.tbxKameitenMeiTo.ClientID & ".value = '';")
            .AppendLine("       document.all." & Me.divCsvOutput.ClientID & ".style.display = 'none';")
            .AppendLine("   }else{")
            .AppendLine("       document.all." & Me.divAitesakiTo.ClientID & ".style.display = 'block';")
            .AppendLine("       document.all." & Me.divCsvOutput.ClientID & ".style.display = 'block';")
            .AppendLine("       document.all." & Me.btnCSVOutput.ClientID & ".display = true;")
            .AppendLine("   }")
            .AppendLine("}")
            '「Close」ボタンの処理
            .AppendLine("   function fncClose(){")
            .AppendLine("       window.close();")
            .AppendLine("       return false;")
            .AppendLine("   }")
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
            '.AppendLine("function fncScrollV(){")
            '.AppendLine("   var divbody=" & Me.divMeisai.ClientID & ";")
            '.AppendLine("   var divVscroll=" & Me.divHiddenMeisaiV.ClientID & ";")
            '.AppendLine("   divbody.scrollTop = divVscroll.scrollTop;")
            '.AppendLine("}")
            .AppendLine("function fncScrollV(){")
            .AppendLine("   var divbodyleft=" & Me.divMeisaiLeft.ClientID & ";")
            .AppendLine("   var divbodyright=" & Me.divMeisaiRight.ClientID & ";")
            .AppendLine("   var divVscroll=" & Me.divHiddenMeisaiV.ClientID & ";")
            .AppendLine("   divbodyleft.scrollTop = divVscroll.scrollTop;")
            .AppendLine("   divbodyright.scrollTop = divVscroll.scrollTop;")
            .AppendLine("}")
            .AppendLine("function fncScrollH(){")
            .AppendLine("   var divheadright=" & Me.divHeadRight.ClientID & ";")
            .AppendLine("   var divbodyright=" & Me.divMeisaiRight.ClientID & ";")
            .AppendLine("   var divHscroll=" & Me.divHiddenMeisaiH.ClientID & ";")
            .AppendLine("   divheadright.scrollLeft = divHscroll.scrollLeft;")
            .AppendLine("   divbodyright.scrollLeft = divHscroll.scrollLeft;")
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

            '相手先検索表示を設定する
            .AppendLine("function fncSetAitesaki(){")
            .AppendLine("   if(document.all." & Me.ddlAitesakiSyubetu.ClientID & ".value == '0'||document.all." & Me.ddlAitesakiSyubetu.ClientID & ".value == ''){")
            .AppendLine("       document.all." & Me.tbxKameitenCdFrom.ClientID & ".value = '';")
            .AppendLine("       document.all." & Me.tbxKameitenMeiFrom.ClientID & ".value = '';")
            .AppendLine("       document.all." & Me.tbxKameitenCdTo.ClientID & ".value = '';")
            .AppendLine("       document.all." & Me.tbxKameitenMeiTo.ClientID & ".value = '';")
            .AppendLine("       document.all." & Me.divAitesakiSyubetu.ClientID & ".style.display = 'none';")
            .AppendLine("       document.all." & Me.divSitei.ClientID & ".style.display = 'none';")
            .AppendLine("       document.all." & Me.chkSiteiNasiTaisyou.ClientID & ".checked = false;")
            .AppendLine("       document.all." & Me.divCsvOutput.ClientID & ".style.display = 'block';")
            .AppendLine("   }else if(document.all." & Me.ddlAitesakiSyubetu.ClientID & ".value == '1'){")
            .AppendLine("       document.all." & Me.divAitesakiSyubetu.ClientID & ".style.display = 'block';")
            .AppendLine("       document.all." & Me.tbxKameitenCdFrom.ClientID & ".value = '';")
            .AppendLine("       document.all." & Me.tbxKameitenMeiFrom.ClientID & ".value = '';")
            .AppendLine("       document.all." & Me.tbxKameitenCdTo.ClientID & ".value = '';")
            .AppendLine("       document.all." & Me.tbxKameitenMeiTo.ClientID & ".value = '';")
            .AppendLine("       document.all." & Me.divAitesakiTo.ClientID & ".style.display = 'block';")
            .AppendLine("       if(!document.all." & Me.chkMisetteimo.ClientID & ".checked){")
            .AppendLine("           document.all." & Me.divSitei.ClientID & ".style.display = 'block';")
            .AppendLine("           document.all." & Me.chkSiteiNasiTaisyou.ClientID & ".checked = 'true';")
            .AppendLine("           document.all." & Me.divAitesakiTo.ClientID & ".style.display = 'none';")
            .AppendLine("           document.all." & Me.divCsvOutput.ClientID & ".style.display = 'none';")
            .AppendLine("       }")
            .AppendLine("   }else{")
            .AppendLine("       document.all." & Me.divAitesakiSyubetu.ClientID & ".style.display = 'block';")
            .AppendLine("       document.all." & Me.tbxKameitenCdFrom.ClientID & ".value = '';")
            .AppendLine("       document.all." & Me.tbxKameitenMeiFrom.ClientID & ".value = '';")
            .AppendLine("       document.all." & Me.tbxKameitenCdTo.ClientID & ".value = '';")
            .AppendLine("       document.all." & Me.tbxKameitenMeiTo.ClientID & ".value = '';")
            .AppendLine("       document.all." & Me.divSitei.ClientID & ".style.display = 'none';")
            .AppendLine("       document.all." & Me.chkSiteiNasiTaisyou.ClientID & ".checked = false;")
            .AppendLine("       document.all." & Me.divCsvOutput.ClientID & ".style.display = 'block';")
            .AppendLine("       document.all." & Me.divAitesakiTo.ClientID & ".style.display = 'block';")
            .AppendLine("   }")
            .AppendLine("}")

            .AppendLine("</script>")
        End With
        Page.ClientScript.RegisterStartupScript(Page.GetType, "JS_" & Me.ClientID, sbScript.ToString)
    End Sub

    ''' <summary>メッセージ表示</summary>
    ''' <param name="strMessage">メッセージ</param>
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

    ''' <summary>DIV非表示</summary>
    Public Sub CloseCover()
        Dim csScript As New StringBuilder
        csScript.AppendFormat("fncClosecover();").ToString()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CloseCover", csScript.ToString, True)
    End Sub

    'hiddenの値を設定する
    Protected Sub SetButton()
        Me.btnSortAitesakiSyubetuDown.Attributes.Add("onClick", "fncShowModal();fncSetHidCSV()")        '--相手先種別▼
        Me.btnSortAitesakiSyubetuUp.Attributes.Add("onclick", "fncShowModal();fncSetHidCSV()")          '--相手先種別▲
        Me.btnSortAitesakiCdDown.Attributes.Add("onclick", "fncShowModal();fncSetHidCSV()")             '--相手先コード▼
        Me.btnSortAitesakiCdUp.Attributes.Add("onclick", "fncShowModal();fncSetHidCSV()")               '--相手先コード▲
        Me.btnSortAitesakiMeiDown.Attributes.Add("onclick", "fncShowModal();fncSetHidCSV()")            '--相手先名称▼
        Me.btnSortAitesakiMeiUp.Attributes.Add("onclick", "fncShowModal();fncSetHidCSV()")              '--相手先名称▲
        Me.btnSortSyouhinCdUp.Attributes.Add("onclick", "fncShowModal();fncSetHidCSV()")                '--商品コード▼
        Me.btnSortSyouhinCdDown.Attributes.Add("onclick", "fncShowModal();fncSetHidCSV()")              '--商品コード▲
        Me.btnSortSyouhinMeiUp.Attributes.Add("onclick", "fncShowModal();fncSetHidCSV()")               '--商品名▼
        Me.btnSortSyouhinMeiDown.Attributes.Add("onclick", "fncShowModal();fncSetHidCSV()")             '--商品名▲
        Me.btnSortTyousaUp.Attributes.Add("onclick", "fncShowModal();fncSetHidCSV()")                   '--調査方法▼
        Me.btnSortTyousaDown.Attributes.Add("onclick", "fncShowModal();fncSetHidCSV()")                 '--調査方法▲
        'Me.btnSortTokubetuCdUp.Attributes.Add("onclick", "fncShowModal();fncSetHidCSV()")               '--特別対応コード▼
        'Me.btnSortTokubetuCdDown.Attributes.Add("onclick", "fncShowModal();fncSetHidCSV()")             '--特別対応コード▲
        Me.btnSortTokubetuMeiUp.Attributes.Add("onclick", "fncShowModal();fncSetHidCSV()")              '--特別対応名称▼
        Me.btnSortTokubetuMeiDown.Attributes.Add("onclick", "fncShowModal();fncSetHidCSV()")            '--特別対応名称▲
        Me.btnSortTorikesiUp.Attributes.Add("onclick", "fncShowModal();fncSetHidCSV()")                 '--取消▼
        Me.btnSortTorikesiDown.Attributes.Add("onclick", "fncShowModal();fncSetHidCSV()")               '--取消▲
        Me.btnSortKingakuAddScdDown.Attributes.Add("onclick", "fncShowModal();fncSetHidCSV()")          '--金額加算商品コード▼
        Me.btnSortKingakuAddScdUp.Attributes.Add("onclick", "fncShowModal();fncSetHidCSV()")            '--金額加算商品コード▲
        Me.btnSortSyokiTiDown.Attributes.Add("onclick", "fncShowModal();fncSetHidCSV()")                '--初期値▼
        Me.btnSortSyokiTiUp.Attributes.Add("onclick", "fncShowModal();fncSetHidCSV()")                  '--初期値▲
        Me.btnSortRequestAddKingakuDown.Attributes.Add("onclick", "fncShowModal();fncSetHidCSV()")      '--実請求加算金額▼
        Me.btnSortRequestAddKingakuUp.Attributes.Add("onclick", "fncShowModal();fncSetHidCSV()")        '--実請求加算金額▲
        Me.btnSortKoumuAddKingakuDown.Attributes.Add("onclick", "fncShowModal();fncSetHidCSV()")        '--工務店請求加算金額▼
        Me.btnSortKoumuAddKingakuUp.Attributes.Add("onclick", "fncShowModal();fncSetHidCSV()")          '--工務店請求加算金額▲
    End Sub

    Private Sub grdBodyRight_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdMeisaiRight.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim str1 As String = CType(e.Row.FindControl("uri_kasan_gaku"), Label).Text
            Dim str2 As String = CType(e.Row.FindControl("koumuten_kasan_gaku"), Label).Text

            If String.IsNullOrEmpty(str1) Then
                CType(e.Row.FindControl("uri_kasan_gaku"), Label).Text = String.Empty
            Else
                CType(e.Row.FindControl("uri_kasan_gaku"), Label).Text = FormatNumber(str1.Trim, 0)
            End If

            If String.IsNullOrEmpty(str2) Then
                CType(e.Row.FindControl("koumuten_kasan_gaku"), Label).Text = String.Empty
            Else
                CType(e.Row.FindControl("koumuten_kasan_gaku"), Label).Text = FormatNumber(str2.Trim, 0)
            End If
        End If
    End Sub

    ''' <summary>
    ''' 画面の報告書様式を設定する
    ''' </summary>
    ''' <param name="dtTokubetuTaiou"></param>
    ''' <history>
    '''     2013.05.20 楊双 新規作成
    ''' </history>
    Private Sub SetHoukokusyo(ByVal dtTokubetuTaiou As Data.DataTable)

        '「系列・営業所・指定無しも対象チェックボックス」=チェックの場合
        If chkSiteiNasiTaisyou.Checked = True AndAlso dtTokubetuTaiou.Rows.Count > 0 Then

            '報告書様式を表示する
            Me.tbHoukokusyo.Style.Add("display", "block")

            Dim strHoukokusyo As String = String.Empty  '報告書様式の表示内容
            Dim strTokubetuMei As String = String.Empty '特別対応の名称
            '旧様式
            Dim strKyuuStyle As String = tokubetuTaiouMasterLogic.GetStyleMeisyou()
            For i As Integer = 0 To dtTokubetuTaiou.Rows.Count - 1
                '初期値１の件数を探す
                If dtTokubetuTaiou.Rows(i).Item("syokiti").ToString.Equals("1") Then
                    If dtTokubetuTaiou.Rows(i).Item("tokubetu_taiou_cd").ToString <> String.Empty AndAlso _
                       dtTokubetuTaiou.Rows(i).Item("tokubetu_taiou_cd").ToString < 10 Then
                        strKyuuStyle = String.Empty
                        Exit For
                    End If
                End If
            Next

            '旧様式のセット
            If strKyuuStyle <> String.Empty Then
                '報告書様式の表示内容
                strHoukokusyo = strKyuuStyle
            End If

            For i As Integer = 0 To dtTokubetuTaiou.Rows.Count - 1
                '初期値１の件数を探す
                If dtTokubetuTaiou.Rows(i).Item("syokiti").ToString.Equals("1") Then

                    If strKyuuStyle = String.Empty OrElse _
                       strKyuuStyle <> String.Empty AndAlso _
                       dtTokubetuTaiou.Rows(i).Item("tokubetu_taiou_cd").ToString <> String.Empty AndAlso _
                       dtTokubetuTaiou.Rows(i).Item("tokubetu_taiou_cd").ToString >= 10 Then
                        '特別対応の名称
                        strTokubetuMei = dtTokubetuTaiou.Rows(i).Item("tokubetu_taiou_meisyou").ToString
                    Else
                        '特別対応の名称
                        strTokubetuMei = String.Empty
                    End If

                    '特別対応の名称の存在かどうか、判断する
                    If strHoukokusyo.IndexOf(strTokubetuMei) >= 0 Then

                        '報告書様式の表示内容、既に存在の時
                        strHoukokusyo = strHoukokusyo
                    ElseIf strHoukokusyo.IndexOf(strTokubetuMei) = -1 Then

                        '報告書様式の表示内容に追加する
                        If strHoukokusyo = String.Empty Then
                            strHoukokusyo = strTokubetuMei
                        Else
                            strHoukokusyo = strHoukokusyo & "、" & strTokubetuMei
                        End If
                    End If
                End If
            Next

            '報告書様式の表示
            Me.lblHoukokusyo.Text = strHoukokusyo
            Me.lblHoukokusyo.ToolTip = strHoukokusyo

        Else
            '報告書様式を表示しない
            Me.tbHoukokusyo.Style.Add("display", "none")
            Me.lblHoukokusyo.Text = String.Empty

        End If

    End Sub

    ''' <summary>
    ''' 明細の背景色を設定する
    ''' </summary>
    ''' <history>
    '''     2013.06.07 楊双 新規作成
    ''' </history>
    Private Sub SetMeisaiBackColor()

        Dim currentRow As Integer
        Dim oldKey As String = String.Empty
        Dim currentKey As String = String.Empty
        Dim color As String = String.Empty

        For currentRow = 0 To grdMeisaiLeft.Rows.Count - 1
            '該当行のキー
            currentKey = CType(grdMeisaiLeft.Rows(currentRow).FindControl("aitesaki_syubetu_layout"), Label).Text.ToString & _
                        CType(grdMeisaiLeft.Rows(currentRow).FindControl("aitesaki_cd"), Label).Text.ToString & _
                        CType(grdMeisaiLeft.Rows(currentRow).FindControl("syouhin_cd"), Label).Text.ToString & _
                        CType(grdMeisaiLeft.Rows(currentRow).FindControl("tys_houhou"), Label).Text.ToString
            If currentRow = 0 Then
                '初期値のセット
                oldKey = currentKey
            End If

            'キー項目変更の場合
            If oldKey <> currentKey Then
                '背景色
                If color = "#CCFFFF" Then
                    color = String.Empty
                Else
                    color = "#CCFFFF"
                End If
            End If
            grdMeisaiLeft.Rows(currentRow).Style.Add("background-color", color)
            grdMeisaiRight.Rows(currentRow).Style.Add("background-color", color)
            oldKey = currentKey
        Next

    End Sub

End Class
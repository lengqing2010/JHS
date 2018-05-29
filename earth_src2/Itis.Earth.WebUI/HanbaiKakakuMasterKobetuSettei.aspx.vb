Imports Itis.Earth.BizLogic
Imports Itis.Earth.DataAccess

Partial Public Class HanbaiKakakuMasterKobetuSettei
    Inherits System.Web.UI.Page

    Private hanbaiKakakuSearchListLogic As New HanbaiKakakuMasterLogic
    Private commonCheck As New CommonCheck

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '権限チェック
        Dim commonChk As New CommonCheck
        Dim strUserID As String = ""
        Dim blnEigyouKengen As Boolean
        blnEigyouKengen = commonChk.CommonNinnsyou(strUserID, "eigyou_master_kanri_kengen")
        If Not blnEigyouKengen Then
            'エラー画面へ遷移して、エラーメッセージを表示する
            Context.Items("strFailureMsg") = Messages.Instance.MSG2020E
            Server.Transfer("CommonErr.aspx")
        End If

        'javascript作成
        MakeJavascript()

        If Not IsPostBack Then

            If Not Request("sendSearchTerms") Is Nothing Then
                '初期化
                Call SetInitData(CStr(Request("sendSearchTerms")))
                Me.hdnAiteSakiSyubetu.Value = Me.ddlAiteSakiSyubetu.SelectedValue.Trim
                Me.hdnAiteSakiCd.Value = Me.tbxAiteSakiCd.Text.Trim
            Else
                '初期化
                Call SetInitData(String.Empty)
            End If

        Else
            Me.hdnKensakuFlg.Value = "1"
            '相手先種別
            Call Me.SetAitesakiSyubetu(Me.ddlAiteSakiSyubetu.SelectedValue.Trim)
            Me.hdnAiteSakiSyubetu.Value = Me.ddlAiteSakiSyubetu.SelectedValue.Trim
            '相手先コード
            Call Me.SetAitesakiCd(Me.tbxAiteSakiCd.Text.Trim)
            Me.hdnAiteSakiCd.Value = Me.tbxAiteSakiCd.Text.Trim
            '商品コード
            Call Me.SetSyouhin(Me.ddlSyouhinCd.SelectedValue.Trim)
            '調査方法
            Call Me.SetTyousaHouhou(Me.ddlTyousaHouhou.SelectedValue.Trim)

            '更新処理を行う
            If CStr(Me.hdnDbFlg.Value).Equals("1") Then
                Call Me.DbSyori("update")
                Me.hdnDbFlg.Value = String.Empty
            End If

        End If
        '｢閉じる｣ボタン
        Me.btnClose.Attributes.Add("onClick", "fncCloseWindow();")
        '相手先種別が変更する場合、相手先コード検索条件を設定する
        Me.ddlAiteSakiSyubetu.Attributes.Add("onChange", "fncSetAitesaki();return false;")
        '相手先コードがフォーカスを失う場合、大文字に変換する
        Me.tbxAiteSakiCd.Attributes.Add("onBlur", "fncToUpper(this);")
        Me.tbxAiteSakiCd.Attributes.Add("onChange", "fncAitesakiCdChange();")
        '｢検索｣ボタン
        Me.btnAiteSakiCd.Attributes.Add("onClick", "fncAiteSakiSearch();return false;")
        '｢登録｣ボタン
        Me.btnTouroku.Attributes.Add("onClick", "if(!fncNyuuryokuCheck()){return false;}")

    End Sub

    ''' <summary>初期データをセット</summary>
    ''' <param name="sendSearchTerms">パラメータ</param>
    Private Sub SetInitData(ByVal sendSearchTerms As String)

        Me.hdnKensakuFlg.Value = "1"

        '工務店請求金額変更フラグ
        Me.ddlKoumutenSeikyuuKingakuFlg.Items.Insert(0, New ListItem("変更不可", "0"))
        Me.ddlKoumutenSeikyuuKingakuFlg.Items.Insert(1, New ListItem("変更可", "1"))

        '実請求金額変更フラグ
        Me.ddlJituSeikyuuKingakuFlg.Items.Insert(0, New ListItem("変更不可", "0"))
        Me.ddlJituSeikyuuKingakuFlg.Items.Insert(1, New ListItem("変更可", "1"))

        If sendSearchTerms <> String.Empty Then
            Dim arrSearchTerm() As String = Split(sendSearchTerms, "$$$")
            '相手先種別
            Dim strAiteSakiSyubetu As String
            '相手先コード
            Dim strAiteSakiCd As String
            '商品コード
            Dim strSyouhinCd As String
            '調査方法
            Dim strTyousaHouhouNo As String

            '相手先種別
            strAiteSakiSyubetu = arrSearchTerm(0).Trim
            Me.hdnAiteSakiSyubetu.Value = strAiteSakiSyubetu
            Call Me.SetAitesakiSyubetu(strAiteSakiSyubetu)

            '相手先コード
            If arrSearchTerm.Length > 1 Then
                strAiteSakiCd = arrSearchTerm(1).Trim
            Else
                strAiteSakiCd = String.Empty
            End If
            Me.hdnAiteSakiCd.Value = strAiteSakiCd
            Call Me.SetAitesakiCd(strAiteSakiCd)

            '商品コード
            If arrSearchTerm.Length > 2 Then
                strSyouhinCd = arrSearchTerm(2).Trim
            Else
                strSyouhinCd = String.Empty
            End If
            Call Me.SetSyouhin(strSyouhinCd)

            '調査方法
            If arrSearchTerm.Length > 3 Then
                strTyousaHouhouNo = arrSearchTerm(3).Trim
            Else
                strTyousaHouhouNo = String.Empty
            End If
            Call Me.SetTyousaHouhou(strTyousaHouhouNo)

            If arrSearchTerm.Length > 3 Then
                '検索を行う
                Call Me.KensakuSyori(strAiteSakiSyubetu, strAiteSakiCd, strSyouhinCd, strTyousaHouhouNo)
            Else
                '取消
                Me.chkTorikesi.Checked = False

                '公開
                Me.chkKoukai.Checked = False

                '工務店請求金額
                Me.tbxKoumutenSeikyuuKingaku.Text = "0"

                '実請求金額
                Me.tbxJituSeikyuuKingaku.Text = "0"

                '工務店請求金額変更フラグ
                Me.ddlKoumutenSeikyuuKingakuFlg.SelectedValue = "0"

                '工務店請求金額変更フラグ
                Me.ddlJituSeikyuuKingakuFlg.SelectedValue = "0"
            End If
        Else
            '相手先種別
            Call Me.SetAitesakiSyubetu(String.Empty)
            Me.hdnAiteSakiSyubetu.Value = String.Empty
            '相手先コード
            Call Me.SetAitesakiCd(String.Empty)
            Me.hdnAiteSakiCd.Value = String.Empty
            '商品コード
            Call Me.SetSyouhin(String.Empty)

            '調査方法
            Call Me.SetTyousaHouhou(String.Empty)

            '取消
            Me.chkTorikesi.Checked = False

            '公開
            Me.chkKoukai.Checked = False

            '工務店請求金額
            Me.tbxKoumutenSeikyuuKingaku.Text = "0"

            '実請求金額
            Me.tbxJituSeikyuuKingaku.Text = "0"

            '工務店請求金額変更フラグ
            Me.ddlKoumutenSeikyuuKingakuFlg.SelectedValue = "0"

            '工務店請求金額変更フラグ
            Me.ddlJituSeikyuuKingakuFlg.SelectedValue = "0"
        End If

    End Sub

    ''' <summary> 相手先種別リストを設定する</summary>
    ''' <param name="strAitesakiSyubetu">相手先種別</param>
    Private Sub SetAitesakiSyubetu(ByVal strAitesakiSyubetu As String)

        Dim dtAiteSakiSyubetu As Data.DataTable
        '相手先種別データを取得する
        dtAiteSakiSyubetu = hanbaiKakakuSearchListLogic.GetAiteSakiSyubetu()
        'データを設定する
        Me.ddlAiteSakiSyubetu.DataTextField = "aitesaki_syubetu"
        Me.ddlAiteSakiSyubetu.DataValueField = "code"
        Me.ddlAiteSakiSyubetu.DataSource = dtAiteSakiSyubetu
        Me.ddlAiteSakiSyubetu.DataBind()

        '相手先種別の先頭行は空欄をセットする
        Me.ddlAiteSakiSyubetu.Items.Insert(0, New ListItem(String.Empty, String.Empty))

        'プリンタのデフォルト表示
        If strAitesakiSyubetu.Equals(String.Empty) Then
            Me.ddlAiteSakiSyubetu.SelectedValue = String.Empty
        Else
            Try
                Me.ddlAiteSakiSyubetu.SelectedValue = strAitesakiSyubetu
            Catch ex As Exception
                Me.ddlAiteSakiSyubetu.SelectedValue = String.Empty
            End Try

        End If

    End Sub

    ''' <summary>相手先コードを設定</summary>
    Private Sub SetAitesakiCd(ByVal strAitesakiCd As String)

        Dim strAiteSakiSyubetu As String = Me.ddlAiteSakiSyubetu.SelectedValue.Trim

        Select Case strAiteSakiSyubetu
            Case String.Empty
                '｢相手先コード｣
                Me.tbxAiteSakiCd.Text = String.Empty
                Me.tbxAiteSakiCd.Enabled = False
                '「検索」ボタン
                Me.btnAiteSakiCd.Enabled = False
                '｢相手先名｣
                Me.tbxAiteSakiMei.Text = String.Empty
            Case "0"
                '｢相手先コード｣
                Me.tbxAiteSakiCd.Text = "ALL"
                Me.tbxAiteSakiCd.Enabled = False
                '「検索」ボタン
                Me.btnAiteSakiCd.Enabled = False
                '｢相手先名｣
                Me.tbxAiteSakiMei.Text = "相手先なし"
            Case Else
                '｢相手先コード｣
                Me.tbxAiteSakiCd.Enabled = True
                '「検索」ボタン
                Me.btnAiteSakiCd.Enabled = True
                If Not strAitesakiCd.Trim.Equals(String.Empty) Then
                    '｢相手先コード｣
                    Me.tbxAiteSakiCd.Text = strAitesakiCd.Trim

                    '相手先名を取得
                    Dim dtAitesakiMei As New Data.DataTable
                    dtAitesakiMei = hanbaiKakakuSearchListLogic.GetAiteSaki(strAiteSakiSyubetu.Trim, String.Empty, strAitesakiCd.Trim)

                    '相手先名をセット
                    If dtAitesakiMei.Rows.Count > 0 Then
                        Me.tbxAiteSakiMei.Text = dtAitesakiMei.Rows(0).Item("mei").ToString
                    Else
                        Me.tbxAiteSakiMei.Text = String.Empty
                    End If
                Else
                    Me.tbxAiteSakiMei.Text = String.Empty
                End If
        End Select

    End Sub

    ''' <summary> 商品コードリストを設定する</summary>
    ''' <param name="strSyouhinCd">商品コード</param>
    Private Sub SetSyouhin(ByVal strSyouhinCd As String)

        Dim dtSyouhin As Data.DataTable
        '相手先種別データを取得する
        dtSyouhin = hanbaiKakakuSearchListLogic.GetSyouhin()
        'データを設定する
        Me.ddlSyouhinCd.DataTextField = "syouhin"
        Me.ddlSyouhinCd.DataValueField = "syouhin_cd"
        Me.ddlSyouhinCd.DataSource = dtSyouhin
        Me.ddlSyouhinCd.DataBind()

        ''商品コードの先頭行は空欄をセットする
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

    ''' <summary> 調査方法リストを設定する</summary>
    ''' <param name="strTysHouhouNo">調査方法NO</param>
    Private Sub SetTyousaHouhou(ByVal strTysHouhouNo As String)

        Dim dtTyousahouhou As Data.DataTable
        '相手先種別データを取得する
        dtTyousahouhou = hanbaiKakakuSearchListLogic.GetTyousaHouhou()
        'データを設定する
        Me.ddlTyousaHouhou.DataTextField = "tys_houhou"
        Me.ddlTyousaHouhou.DataValueField = "tys_houhou_no"
        Me.ddlTyousaHouhou.DataSource = dtTyousahouhou
        Me.ddlTyousaHouhou.DataBind()

        '調査方法の先頭行は空欄をセットする
        Me.ddlTyousaHouhou.Items.Insert(0, New ListItem(String.Empty, String.Empty))

        'プリンタのデフォルト表示
        If strTysHouhouNo.Equals(String.Empty) Then
            Me.ddlTyousaHouhou.SelectedValue = String.Empty
        Else
            Try
                Me.ddlTyousaHouhou.SelectedValue = strTysHouhouNo
            Catch ex As Exception
                Me.ddlTyousaHouhou.SelectedValue = String.Empty
            End Try
        End If

    End Sub

    ''' <summary>入力チェック</summary>
    ''' <param name="strObjId">クライアントID</param>
    ''' <returns>エラーメッセージ</returns>
    Private Function CheckInput(ByRef strObjId As String) As String
        Dim csScript As New StringBuilder
        With csScript
            '相手先コード(From)(英数字チェック)
            If Me.tbxAiteSakiCd.Text <> String.Empty Then
                .Append(commonCheck.ChkHankakuEisuuji(Me.tbxAiteSakiCd.Text, "相手先コード(From)"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxAiteSakiCd.ClientID
                End If
            End If
        End With
        Return csScript.ToString
    End Function

    ''' <summary>検索処理</summary>
    ''' <param name="strAiteSakiSyubetu">相手先種別</param>
    ''' <param name="strAiteSakiCd">相手先コード</param>
    ''' <param name="strSyouhinCd">商品コード</param>
    ''' <param name="strTyousaHouhouNo">調査方法</param>
    ''' <remarks></remarks>
    Private Sub KensakuSyori(ByVal strAiteSakiSyubetu As String, ByVal strAiteSakiCd As String, ByVal strSyouhinCd As String, ByVal strTyousaHouhouNo As String)

        Dim intValue As Integer
        Dim blnKensaku As Boolean = True
        Try
            intValue = CInt(strAiteSakiSyubetu)
        Catch ex As Exception
            blnKensaku = False
        End Try
        Try
            intValue = CInt(strAiteSakiSyubetu)
        Catch ex As Exception
            blnKensaku = False
        End Try

        If blnKensaku = True Then

            'データを取得する
            Dim dtHanbaiKakaku As New Data.DataTable
            dtHanbaiKakaku = hanbaiKakakuSearchListLogic.GetHanbaiKakakuKobeituSettei(strAiteSakiSyubetu, strAiteSakiCd, strSyouhinCd, strTyousaHouhouNo)

            If dtHanbaiKakaku.Rows.Count > 0 Then
                With dtHanbaiKakaku.Rows(0)
                    '相手先名
                    'Me.tbxAiteSakiMei.Text = .Item("aitesaki_mei").ToString.Trim

                    '取消
                    If .Item("torikesi").ToString.Trim.Equals("0") Then
                        Me.chkTorikesi.Checked = False
                    Else
                        Me.chkTorikesi.Checked = True
                    End If

                    '公開
                    If .Item("koukai_flg").ToString.Trim.Equals("0") Then
                        Me.chkKoukai.Checked = False
                    Else
                        Me.chkKoukai.Checked = True
                    End If

                    '工務店請求金額
                    If Not .Item("koumuten_seikyuu_gaku").ToString.Trim.Equals(String.Empty) Then
                        Me.tbxKoumutenSeikyuuKingaku.Text = FormatNumber(.Item("koumuten_seikyuu_gaku").ToString.Trim, 0)
                    Else
                        Me.tbxKoumutenSeikyuuKingaku.Text = "0"
                    End If

                    '実請求金額
                    If Not .Item("jitu_seikyuu_gaku").ToString.Trim.Equals(String.Empty) Then
                        Me.tbxJituSeikyuuKingaku.Text = FormatNumber(.Item("jitu_seikyuu_gaku").ToString.Trim, 0)
                    Else
                        Me.tbxJituSeikyuuKingaku.Text = "0"
                    End If

                    '工務店請求金額変更フラグ
                    If .Item("koumuten_seikyuu_gaku_henkou_flg").ToString.Trim.Equals("0") Then
                        Me.ddlKoumutenSeikyuuKingakuFlg.SelectedValue = "0"
                    Else
                        Me.ddlKoumutenSeikyuuKingakuFlg.SelectedValue = "1"
                    End If

                    '工務店請求金額変更フラグ
                    If .Item("jitu_seikyuu_gaku_henkou_flg").ToString.Trim.Equals("0") Then
                        Me.ddlJituSeikyuuKingakuFlg.SelectedValue = "0"
                    Else
                        Me.ddlJituSeikyuuKingakuFlg.SelectedValue = "1"
                    End If
                End With
            Else
                '取消
                Me.chkTorikesi.Checked = False
                '公開
                Me.chkKoukai.Checked = False
                '工務店請求金額
                Me.tbxKoumutenSeikyuuKingaku.Text = "0"
                '実請求金額
                Me.tbxJituSeikyuuKingaku.Text = "0"
                '工務店請求金額変更フラグ
                Me.ddlKoumutenSeikyuuKingakuFlg.SelectedValue = "0"
                '工務店請求金額変更フラグ
                Me.ddlJituSeikyuuKingakuFlg.SelectedValue = "0"
            End If
        Else
            '取消
            Me.chkTorikesi.Checked = False
            '公開
            Me.chkKoukai.Checked = False
            '工務店請求金額
            Me.tbxKoumutenSeikyuuKingaku.Text = "0"
            '実請求金額
            Me.tbxJituSeikyuuKingaku.Text = "0"
            '工務店請求金額変更フラグ
            Me.ddlKoumutenSeikyuuKingakuFlg.SelectedValue = "0"
            '工務店請求金額変更フラグ
            Me.ddlJituSeikyuuKingakuFlg.SelectedValue = "0"
        End If
    End Sub

    ''' <summary>登録ボタン押下時処理</summary>
    Private Sub btnTouroku_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTouroku.Click

        '入力チェック
        Dim strObjId As String = String.Empty
        Dim strErrMessage As String = CheckInput(strObjId)
        If strErrMessage <> String.Empty Then
            ShowMessage(strErrMessage)
            Exit Sub
        End If

        '相手先種別
        Dim strAiteSakiSyubetu As String
        strAiteSakiSyubetu = Me.ddlAiteSakiSyubetu.SelectedValue.Trim
        '相手先コード
        Dim strAiteSakiCd As String
        If strAiteSakiSyubetu.Equals("0") Then
            strAiteSakiCd = "ALL"
        Else
            strAiteSakiCd = Me.tbxAiteSakiCd.Text.Trim
        End If

        '商品コード
        Dim strSyouhinCd As String
        strSyouhinCd = Me.ddlSyouhinCd.SelectedValue.Trim
        '調査方法
        Dim strTyousaHouhouNo As String
        strTyousaHouhouNo = Me.ddlTyousaHouhou.SelectedValue.Trim

        '存在チェツク
        Dim dtHanbaiKakaku As New Data.DataTable
        dtHanbaiKakaku = hanbaiKakakuSearchListLogic.CheckSonzai(strAiteSakiSyubetu, strAiteSakiCd, strSyouhinCd, strTyousaHouhouNo)

        If dtHanbaiKakaku.Rows.Count > 0 Then
            '更新
            Call Me.ShowKakuninMessage(Messages.Instance.MSG2057E)
        Else
            '登録
            Call Me.DbSyori("insert")
        End If

    End Sub

    ''' <summary>DB処理</summary>
    Private Sub DbSyori(ByVal strKbn As String)

        'ユーザーID
        Dim ninsyou As New Ninsyou()
        Dim strUserId As String
        strUserId = ninsyou.GetUserID()

        '相手先種別
        Dim strAiteSakiSyubetu As String
        strAiteSakiSyubetu = Me.ddlAiteSakiSyubetu.SelectedValue.Trim
        '相手先コード
        Dim strAiteSakiCd As String
        If strAiteSakiSyubetu.Equals("0") Then
            strAiteSakiCd = "ALL"
        Else
            strAiteSakiCd = Me.tbxAiteSakiCd.Text.Trim
        End If

        '商品コード
        Dim strSyouhinCd As String
        strSyouhinCd = Me.ddlSyouhinCd.SelectedValue.Trim
        '調査方法
        Dim strTyousaHouhouNo As String
        strTyousaHouhouNo = Me.ddlTyousaHouhou.SelectedValue.Trim

        'パラメータ
        Dim dtHanbaiKakakuOk As New Data.DataTable
        Call Me.SetHanbaiKakaku(dtHanbaiKakakuOk)
        Dim dr As Data.DataRow
        dr = dtHanbaiKakakuOk.NewRow
        dr.Item("aitesaki_syubetu") = strAiteSakiSyubetu
        dr.Item("aitesaki_cd") = strAiteSakiCd
        dr.Item("syouhin_cd") = strSyouhinCd
        dr.Item("tys_houhou_no") = strTyousaHouhouNo
        If Me.chkTorikesi.Checked Then
            dr.Item("torikesi") = "1"
        Else
            dr.Item("torikesi") = "0"
        End If
        Dim strKoumutenSeikyuuKingaku As String = Me.tbxKoumutenSeikyuuKingaku.Text.Trim.Replace(",", String.Empty)
        dr.Item("koumuten_seikyuu_gaku") = IIf(strKoumutenSeikyuuKingaku.Equals(String.Empty), "0", strKoumutenSeikyuuKingaku)
        dr.Item("koumuten_seikyuu_gaku_henkou_flg") = Me.ddlKoumutenSeikyuuKingakuFlg.SelectedValue.Trim
        Dim strJituSeikyuuKingaku As String = Me.tbxJituSeikyuuKingaku.Text.Trim.Replace(",", String.Empty)
        dr.Item("jitu_seikyuu_gaku") = IIf(strJituSeikyuuKingaku.Equals(String.Empty), "0", strJituSeikyuuKingaku)
        dr.Item("jitu_seikyuu_gaku_henkou_flg") = Me.ddlJituSeikyuuKingakuFlg.SelectedValue.Trim
        If Me.chkKoukai.Checked Then
            dr.Item("koukai_flg") = "1"
        Else
            dr.Item("koukai_flg") = "0"
        End If

        Select Case strKbn
            Case "insert"
                dr.Item("ins_upd_flg") = "0"
            Case "update"
                dr.Item("ins_upd_flg") = "1"
        End Select

        dtHanbaiKakakuOk.Rows.Add(dr)

        If hanbaiKakakuSearchListLogic.SetHanbaiKakakuKobeituSettei(dtHanbaiKakakuOk, strUserId).Equals(False) Then

            Call Me.ShowMessage(Messages.Instance.MSG2058E)

        Else

            '当画面を閉じる
            ClientScript.RegisterStartupScript(Me.GetType(), "ShowMessage", "fncCloseWindow();", True)
        End If

    End Sub

    ''' <summary>Javascript作成</summary>
    Private Sub MakeJavascript()
        Dim sbScript As New StringBuilder
        Dim strPraram(0) As String
        With sbScript
            .AppendLine("<script language='javascript' type='text/javascript'>")
            '相手先変更時
            .AppendLine("function fncSetAitesaki()")
            .AppendLine("{")
            '.AppendLine("   alert('hdnKensakuFlg:'+document.getElementById('" & Me.hdnKensakuFlg.ClientID & "').value);")
            .AppendLine("   if(document.getElementById('" & Me.ddlAiteSakiSyubetu.ClientID & "').value != document.getElementById('" & Me.hdnAiteSakiSyubetu.ClientID & "').value)")
            .AppendLine("   {")
            .AppendLine("       document.getElementById('" & Me.hdnKensakuFlg.ClientID & "').value = '0';")
            .AppendLine("       document.getElementById('" & Me.hdnAiteSakiSyubetu.ClientID & "').value = document.getElementById('" & Me.ddlAiteSakiSyubetu.ClientID & "').value;")
            .AppendLine("   }")
            .AppendLine("	switch(document.all." & Me.ddlAiteSakiSyubetu.ClientID & ".value) ")
            .AppendLine("	{ ")
            .AppendLine("		case '': ")
            .AppendLine("           document.all." & Me.tbxAiteSakiCd.ClientID & ".value = '';")
            .AppendLine("           document.all." & Me.tbxAiteSakiCd.ClientID & ".disabled = true;")
            .AppendLine("           document.all." & Me.btnAiteSakiCd.ClientID & ".disabled = true;")
            .AppendLine("           document.all." & Me.tbxAiteSakiMei.ClientID & ".value = '';")
            .AppendLine("			break; ")
            .AppendLine("		case '0': ")
            .AppendLine("           document.all." & Me.tbxAiteSakiCd.ClientID & ".value = 'ALL';")
            .AppendLine("           document.all." & Me.tbxAiteSakiCd.ClientID & ".disabled = true;")
            .AppendLine("           document.all." & Me.btnAiteSakiCd.ClientID & ".disabled = true;")
            .AppendLine("           document.all." & Me.tbxAiteSakiMei.ClientID & ".value = '';")
            .AppendLine("			break; ")
            .AppendLine("		default: ")
            .AppendLine("           document.all." & Me.tbxAiteSakiCd.ClientID & ".value = '';")
            .AppendLine("           document.all." & Me.tbxAiteSakiCd.ClientID & ".disabled = false;")
            .AppendLine("           document.all." & Me.btnAiteSakiCd.ClientID & ".disabled = false;")
            .AppendLine("           document.all." & Me.tbxAiteSakiMei.ClientID & ".value = '';")
            .AppendLine("	} ")
            '.AppendLine("   alert('hdnKensakuFlg:'+document.getElementById('" & Me.hdnKensakuFlg.ClientID & "').value);")
            .AppendLine("}")
            '相手先検索を押下する場合、ポップアップを起動する
            .AppendLine("function fncAiteSakiSearch(){")
            .AppendLine("       var objRetrun = '" & Me.hdnKensakuFlg.ClientID & "';")
            .AppendLine("       var hdnId = '" & Me.hdnAiteSakiCd.ClientID & "';")
            '相手先種別が「1:加盟店」の場合、加盟店ポップアップを起動する
            .AppendLine("   if(document.all." & Me.ddlAiteSakiSyubetu.ClientID & ".value == '1'){")
            .AppendLine("       var strkbn='加盟店';")
            .AppendLine("       var strClientCdID; ")
            .AppendLine("       var strClientMeiID; ")
            .AppendLine("       var blnTorikesi; ")
            .AppendLine("       strClientCdID = '" & Me.tbxAiteSakiCd.ClientID & "';")
            .AppendLine("       strClientMeiID = '" & Me.tbxAiteSakiMei.ClientID & "';")
            .AppendLine("       blnTorikesi = 'False';")
            .AppendLine("       objSrchWin = window.open('search_common.aspx?Kbn='+escape(strkbn)+'&FormName=" & Me.Form.Name & _
                                "&objCd='+strClientCdID+'&objMei='+strClientMeiID+'&strCd='+escape(eval('document.all.'+strClientCdID).value)+'&strMei='+escape(eval('document.all.'+strClientMeiID).value)+")
            .AppendLine("       '&blnDelete='+blnTorikesi+'&objRetrun='+objRetrun+'&hdnId='+hdnId, ")
            .AppendLine("       'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');")
            .AppendLine("       return false;")
            .AppendLine("   }else if(document.all." & Me.ddlAiteSakiSyubetu.ClientID & ".value == '5'){")
            '相手先種別が「5:営業所」の場合、営業所ポップアップを起動する
            .AppendLine("       var strkbn='営業所';")
            .AppendLine("       var strClientCdID; ")
            .AppendLine("       var strClientMeiID; ")
            .AppendLine("       var blnTorikesi; ")
            .AppendLine("       strClientCdID = '" & Me.tbxAiteSakiCd.ClientID & "';")
            .AppendLine("       strClientMeiID = '" & Me.tbxAiteSakiMei.ClientID & "';")
            .AppendLine("       blnTorikesi = 'False';")
            .AppendLine("       objSrchWin = window.open('search_common.aspx?Kbn='+escape(strkbn)+'&FormName=" & Me.Form.Name & _
                                "&objCd='+strClientCdID+'&objMei='+strClientMeiID+'&strCd='+escape(eval('document.all.'+strClientCdID).value)+'&strMei='+escape(eval('document.all.'+strClientMeiID).value)+")
            .AppendLine("       '&blnDelete='+blnTorikesi+'&objRetrun='+objRetrun+'&hdnId='+hdnId, ")
            .AppendLine("       'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');")
            .AppendLine("       return false;")
            .AppendLine("   }else if(document.all." & Me.ddlAiteSakiSyubetu.ClientID & ".value == '7'){")
            '相手先種別が「7:系列」の場合、系列ポップアップを起動する
            .AppendLine("       var strkbn='系列';")
            .AppendLine("       var strClientCdID; ")
            .AppendLine("       var strClientMeiID; ")
            .AppendLine("       var blnTorikesi; ")
            .AppendLine("       strClientCdID = '" & Me.tbxAiteSakiCd.ClientID & "';")
            .AppendLine("       strClientMeiID = '" & Me.tbxAiteSakiMei.ClientID & "';")
            .AppendLine("       blnTorikesi = 'False';")
            .AppendLine("       objSrchWin = window.open('search_common.aspx?Kbn='+escape(strkbn)+'&FormName=" & Me.Form.Name & _
                                "&objCd='+strClientCdID+'&objMei='+strClientMeiID+'&strCd='+escape(eval('document.all.'+strClientCdID).value)+'&strMei='+escape(eval('document.all.'+strClientMeiID).value)+")
            .AppendLine("       '&blnDelete='+blnTorikesi+'&objRetrun='+objRetrun+'&hdnId='+hdnId, ")
            .AppendLine("       'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');")
            .AppendLine("       return false;")
            .AppendLine("   }")
            .AppendLine("}")

            .AppendLine("function fncAitesakiCdChange()")
            .AppendLine("{")
            '.AppendLine("   alert('hdnAiteSakiCd:'+document.getElementById('" & Me.hdnAiteSakiCd.ClientID & "').value);")
            '.AppendLine("   alert('tbxAiteSakiCd:'+document.getElementById('" & Me.tbxAiteSakiCd.ClientID & "').value);")
            '.AppendLine("   alert('hdnKensakuFlg1:'+document.getElementById('" & Me.hdnKensakuFlg.ClientID & "').value);")

            .AppendLine("   if(document.all." & Me.tbxAiteSakiCd.ClientID & ".value != document.getElementById('" & Me.hdnAiteSakiCd.ClientID & "').value)")
            .AppendLine("   {")
            .AppendLine("       document.getElementById('" & Me.hdnKensakuFlg.ClientID & "').value = '0';")
            .AppendLine("       document.getElementById('" & Me.hdnAiteSakiCd.ClientID & "').value = document.all." & Me.tbxAiteSakiCd.ClientID & ".value;")
            .AppendLine("   }")
            '.AppendLine("   alert('hdnKensakuFlg2:'+document.getElementById('" & Me.hdnKensakuFlg.ClientID & "').value);")
            .AppendLine("}")

            .AppendLine("function fncCloseWindow()")
            .AppendLine("{")
            .AppendLine("   window.close();")
            .AppendLine("}")

            .AppendLine("function fncNyuuryokuCheck(strKbn)")
            .AppendLine("{")
            '相手先種別
            .AppendLine("   if(document.all." & Me.ddlAiteSakiSyubetu.ClientID & ".value == '')")
            .AppendLine("   {")
            .AppendLine("       alert('" & Messages.Instance.MSG013E.Replace("@PARAM1", "相手先種別") & "');")
            .AppendLine("       document.all." & Me.ddlAiteSakiSyubetu.ClientID & ".focus();")
            .AppendLine("       return false; ")
            .AppendLine("   }")
            '相手先コード
            .AppendLine("   if(document.all." & Me.ddlAiteSakiSyubetu.ClientID & ".value != '0')")
            .AppendLine("   {")
            .AppendLine("       if(Trim(document.all." & Me.tbxAiteSakiCd.ClientID & ".value) == '')")
            .AppendLine("       {")
            .AppendLine("           alert('" & Messages.Instance.MSG013E.Replace("@PARAM1", "相手先コード") & "');")
            .AppendLine("           document.all." & Me.tbxAiteSakiCd.ClientID & ".focus();")
            .AppendLine("           return false; ")
            .AppendLine("       }")
            .AppendLine("   }")
            '商品
            .AppendLine("   if(document.all." & Me.ddlSyouhinCd.ClientID & ".value == '')")
            .AppendLine("   {")
            .AppendLine("       alert('" & Messages.Instance.MSG013E.Replace("@PARAM1", "商品コード") & "');")
            .AppendLine("       document.all." & Me.ddlSyouhinCd.ClientID & ".focus();")
            .AppendLine("       return false; ")
            .AppendLine("   }")
            '調査方法
            .AppendLine("   if(document.all." & Me.ddlTyousaHouhou.ClientID & ".value == '')")
            .AppendLine("   {")
            .AppendLine("       alert('" & Messages.Instance.MSG013E.Replace("@PARAM1", "調査方法") & "');")
            .AppendLine("       document.all." & Me.ddlTyousaHouhou.ClientID & ".focus();")
            .AppendLine("       return false; ")
            .AppendLine("   }")
            '相手先（区分・コード・枝番のいずれか）が変更
            .AppendLine("   if(document.all." & Me.ddlAiteSakiSyubetu.ClientID & ".value != '0')")
            .AppendLine("   {")
            '.AppendLine("   alert('hdnKensakuFlg登録:'+document.getElementById('" & Me.hdnKensakuFlg.ClientID & "').value);")
            .AppendLine("       if(document.getElementById('" & Me.hdnKensakuFlg.ClientID & "').value == '0')")
            .AppendLine("       {")
            .AppendLine("           alert('" & Messages.Instance.MSG2054E & "');")
            .AppendLine("           document.all." & Me.ddlAiteSakiSyubetu.ClientID & ".focus();")
            .AppendLine("           return false; ")
            .AppendLine("       }")
            .AppendLine("   }")

            .AppendLine("   var strMenoy;")
            '工務店請求金額
            .AppendLine("   if(!fncCheckMenoy(document.all." & Me.tbxKoumutenSeikyuuKingaku.ClientID & ".value))")
            .AppendLine("   {")
            .AppendLine("       alert('" & String.Format(Messages.Instance.MSG2055E, "工務店請求金額") & "');")
            .AppendLine("       document.all." & Me.tbxKoumutenSeikyuuKingaku.ClientID & ".focus();")
            .AppendLine("       return false; ")
            .AppendLine("   }")

            .AppendLine("   strMenoy = Trim(document.all." & Me.tbxKoumutenSeikyuuKingaku.ClientID & ".value).replace(/,/g,'');")
            .AppendLine("   if((strMenoy<0)||(strMenoy>2147483647))")
            .AppendLine("   {")
            .AppendLine("       alert('" & String.Format(Messages.Instance.MSG2056E, "工務店請求金額") & "');")
            .AppendLine("       document.all." & Me.tbxKoumutenSeikyuuKingaku.ClientID & ".focus();")
            .AppendLine("       return false; ")
            .AppendLine("   }")
            '実請求金額
            .AppendLine("   if(!fncCheckMenoy(document.all." & Me.tbxJituSeikyuuKingaku.ClientID & ".value))")
            .AppendLine("   {")
            .AppendLine("       alert('" & String.Format(Messages.Instance.MSG2055E, "実請求金額") & "');")
            .AppendLine("       document.all." & Me.tbxJituSeikyuuKingaku.ClientID & ".focus();")
            .AppendLine("       return false; ")
            .AppendLine("   }")

            .AppendLine("   strMenoy = Trim(document.all." & Me.tbxJituSeikyuuKingaku.ClientID & ".value).replace(/,/g,'');")
            .AppendLine("   if((strMenoy<0)||(strMenoy>2147483647))")
            .AppendLine("   {")
            .AppendLine("       alert('" & String.Format(Messages.Instance.MSG2056E, "実請求金額") & "');")
            .AppendLine("       document.all." & Me.tbxJituSeikyuuKingaku.ClientID & ".focus();")
            .AppendLine("       return false; ")
            .AppendLine("   }")
            .AppendLine("   return true; ")
            .AppendLine("}")
            .AppendLine("   //DB処理確認 ")
            .AppendLine("   function fncDbSyoriKakunin(strMessage) ")
            .AppendLine("   { ")
            .AppendLine("       if(confirm(strMessage)) ")
            .AppendLine("       { ")
            .AppendLine("           document.getElementById('" & Me.hdnDbFlg.ClientID & "').value = '1'; ")
            .AppendLine("           document.forms[0].submit(); ")
            .AppendLine("       } ")
            .AppendLine("       else ")
            .AppendLine("       { ")
            .AppendLine("           return false; ")
            .AppendLine("       } ")
            .AppendLine("   } ")



            '金額check
            .AppendLine("function fncCheckMenoy(strMenoy) ")
            .AppendLine("{ ")
            .AppendLine("	strMenoy = Trim(strMenoy); ")
            .AppendLine("	strMenoy = strMenoy.replace(/,/g,''); ")
            .AppendLine("	if(strMenoy != '') ")
            .AppendLine("	{ ")
            .AppendLine("		if(isNaN(strMenoy) || (strMenoy.indexOf('+') != -1) || (strMenoy.indexOf('-') != -1) || (strMenoy.indexOf('.') != -1))  ")
            .AppendLine("		{ ")
            .AppendLine("			return false; ")
            .AppendLine("		} ")
            .AppendLine("	} ")
            .AppendLine("	return true; ")
            .AppendLine("} ")


            .AppendLine("//半角英数字チェック ")
            .AppendLine("function chkHankakuEisuuji(strInputString) ")
            .AppendLine("{ ")
            .AppendLine("	if(strInputString.match(/[^a-z\^A-Z\^0-9]/)!=null) ")
            .AppendLine("	{ ")
            .AppendLine("		return false;  ")
            .AppendLine("	} ")
            .AppendLine("	else{ ")
            .AppendLine("		return true;  ")
            .AppendLine("	} ")
            .AppendLine("} ")
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

        Page.ClientScript.RegisterStartupScript(Page.GetType, "myJavaScript", sbScript.ToString)

    End Sub




    ''' <summary>販売価格テーブルを作成する</summary>
    ''' <param name="dtHanbaiKakakuOk">販売価格テーブル</param>
    Public Sub SetHanbaiKakaku(ByRef dtHanbaiKakakuOk As Data.DataTable)

        Dim dc As Data.DataColumn
        dc = New Data.DataColumn
        dc.ColumnName = "ins_upd_flg"       '追加更新FLG(0:追加; 1:更新)
        dtHanbaiKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "aitesaki_syubetu"  '相手先種別
        dtHanbaiKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "aitesaki_cd"       '相手先コード
        dtHanbaiKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "syouhin_cd"        '商品コード
        dtHanbaiKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tys_houhou_no"     '調査方法NO
        dtHanbaiKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "torikesi"          '取消
        dtHanbaiKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "koumuten_seikyuu_gaku"  '工務店請求金額
        dtHanbaiKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "koumuten_seikyuu_gaku_henkou_flg"   '工務店請求金額変更FLG
        dtHanbaiKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "jitu_seikyuu_gaku"  '実請求金額
        dtHanbaiKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "jitu_seikyuu_gaku_henkou_flg"   '実請求金額変更FLG
        dtHanbaiKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "koukai_flg"        '公開フラグ
        dtHanbaiKakakuOk.Columns.Add(dc)

    End Sub

    ''' <summary>メッセージ表示</summary>
    ''' <param name="strMessage">メッセージ</param>
    Private Sub ShowMessage(ByVal strMessage As String)
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("function window.onload() {")
            .AppendLine("	alert('" & strMessage & "');")
            .AppendLine("}")
        End With
        'ページ応答で、クライアント側のスクリプト ブロックを出力します
        ClientScript.RegisterStartupScript(Me.GetType(), "ShowMessage", csScript.ToString, True)
    End Sub

    ''' <summary>
    ''' DB処理確認メッセージ表示
    ''' </summary>
    ''' <param name="strMessage">メッセージ</param>
    ''' <history>2011/04/11 車龍(大連情報システム部)　新規作成</history>
    Private Sub ShowKakuninMessage(ByVal strMessage As String)

        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("fncDbSyoriKakunin('" & strMessage & "'); ")
        End With
        'ページ応答で、クライアント側のスクリプト ブロックを出力します
        ClientScript.RegisterStartupScript(Me.GetType(), "ShowTourokuMessage", csScript.ToString, True)
    End Sub
End Class
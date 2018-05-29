Imports Itis.Earth.BizLogic
Imports Itis.Earth.DataAccess

Partial Public Class KoujiKakakuMasterKobetuSettei
    Inherits System.Web.UI.Page

    Private hanbaiKakakuSearchListLogic As New HanbaiKakakuMasterLogic
    Private commonCheck As New CommonCheck

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '権限チェック
        Dim commonChk As New CommonCheck
        Dim strUserID As String = ""
        Dim blnEigyouKengen As Boolean
        blnEigyouKengen = commonChk.CommonNinnsyou(strUserID, "koj_gyoumu_kengen")
        If Not blnEigyouKengen Then
            'エラー画面へ遷移して、エラーメッセージを表示する
            Context.Items("strFailureMsg") = Messages.Instance.MSG2020E
            Server.Transfer("CommonErr.aspx")
        End If

        'javascript作成
        MakeJavascript()

        If Not IsPostBack Then
            '参照履歴管理テーブルを登録する。
            commonCheck.SetURL(Me, strUserID)
            If Not Request("sendSearchTerms") Is Nothing Then
                '初期化
                Call SetInitData(CStr(Request("sendSearchTerms")))
                Me.hdnAiteSakiSyubetu.Value = Me.ddlAiteSakiSyubetu.SelectedValue.Trim
                Me.hdnAiteSakiCd.Value = Me.tbxAiteSakiCd.Text.Trim
                hdnKojKaisyaCd.Value = Me.tbxKojKaisyaCd.Text.Trim
            Else
                '初期化
                Call SetInitData(String.Empty)
            End If

        Else
            Me.hdnKensakuFlg.Value = "1"
            Me.hdnKensakuFlg2.Value = "1"
            '相手先種別
            Call Me.SetAitesakiSyubetu(Me.ddlAiteSakiSyubetu.SelectedValue.Trim)
            Me.hdnAiteSakiSyubetu.Value = Me.ddlAiteSakiSyubetu.SelectedValue.Trim
            '相手先コード
            Call Me.SetAitesakiCd(Me.tbxAiteSakiCd.Text.Trim)
            Me.hdnAiteSakiCd.Value = Me.tbxAiteSakiCd.Text.Trim
            '商品コード
            Call Me.SetSyouhin(Me.ddlSyouhinCd.SelectedValue.Trim)
            '工事会社
            Call Me.SetKojKaisya(Me.tbxKojKaisyaCd.Text.Trim)
            hdnKojKaisyaCd.Value = Me.tbxKojKaisyaCd.Text.Trim
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

        Me.tbxKojKaisyaCd.Attributes.Add("onChange", "fncKojKaisyaCdChange();")
        Me.tbxKojKaisyaCd.Attributes.Add("onBlur", "fncToUpper(this);")
        '｢検索｣ボタン
        Me.btnAiteSakiCd.Attributes.Add("onClick", "fncAiteSakiSearch();return false;")
        '｢登録｣ボタン
        Me.btnTouroku.Attributes.Add("onClick", "if(!fncNyuuryokuCheck()){return false;}")

    End Sub

    ''' <summary>初期データをセット</summary>
    ''' <param name="sendSearchTerms">パラメータ</param>
    Private Sub SetInitData(ByVal sendSearchTerms As String)

        Me.hdnKensakuFlg.Value = "1"
        Me.hdnKensakuFlg2.Value = "1"
        '工事会社請求有無
        Me.ddlKojGaisyaSeikyuuUmu.Items.Insert(0, New ListItem("無", "0"))
        Me.ddlKojGaisyaSeikyuuUmu.Items.Insert(1, New ListItem("有", "1"))

        '請求有無

        Me.ddlSeikyuuUmu.Items.Insert(0, New ListItem("無", "0"))
        Me.ddlSeikyuuUmu.Items.Insert(1, New ListItem("有", "1"))

        If sendSearchTerms <> String.Empty Then
            Dim arrSearchTerm() As String = Split(sendSearchTerms, "$$$")
            '相手先種別
            Dim strAiteSakiSyubetu As String
            '相手先コード
            Dim strAiteSakiCd As String
            '商品コード
            Dim strSyouhinCd As String
            '工事会社コード
            Dim strKojKaisyaCd As String

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

            '工事会社
            If arrSearchTerm.Length > 3 Then
                strKojKaisyaCd = arrSearchTerm(3).Trim
            Else
                strKojKaisyaCd = String.Empty
            End If
            hdnKojKaisyaCd.Value = strKojKaisyaCd
            Call Me.SetKojKaisya(strKojKaisyaCd)

            If arrSearchTerm.Length > 3 Then
                '検索を行う
                Call Me.KensakuSyori(strAiteSakiSyubetu, strAiteSakiCd, strSyouhinCd, strKojKaisyaCd)
            Else
                '取消
                Me.chkTorikesi.Checked = False
                '売上金額
                Me.tbxUriGaku.Text = "0"
                '工事会社請求有無
                Me.ddlKojGaisyaSeikyuuUmu.SelectedValue = "0"
                '請求有無
                Me.ddlSeikyuuUmu.SelectedValue = "0"
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
            Call Me.SetKojKaisya(String.Empty)
            hdnKojKaisyaCd.Value = String.Empty
            '取消
            Me.chkTorikesi.Checked = False
            '売上金額
            Me.tbxUriGaku.Text = "0"
            '工事会社請求有無
            Me.ddlKojGaisyaSeikyuuUmu.SelectedValue = "0"
            '請求有無
            Me.ddlSeikyuuUmu.SelectedValue = "0"
        End If

    End Sub
    ''' <summary> 工事会社を設定する</summary>
    ''' <param name="strSetKojKaisyaCd">工事会社コード</param>
    Sub SetKojKaisya(ByVal strSetKojKaisyaCd As String)
        Dim KojKakuLogic As New KojKakakuMasterLogic
        Dim dtSearchTable As New DataTable

        If strSetKojKaisyaCd = "ALLAL" Then
            Me.tbxKojKaisyaMei.Text = "指定無し"
        ElseIf strSetKojKaisyaCd = "" Then
            Me.tbxKojKaisyaMei.Text = ""
        Else
            dtSearchTable = KojKakuLogic.GetKojKaisyaKensaku(strSetKojKaisyaCd)
            If dtSearchTable.Rows.Count > 0 Then
                Me.tbxKojKaisyaMei.Text = dtSearchTable.Rows(0).Item("tys_kaisya_mei")
            End If
        End If

        Me.tbxKojKaisyaCd.Text = strSetKojKaisyaCd
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
    Sub SetSyouhin(ByVal strSyouhinCd As String)

        Dim dtSyouhin As Data.DataTable
        '相手先種別データを取得する
        Dim KojKakakuSearchListLogic As New KojKakakuMasterLogic
        dtSyouhin = KojKakakuSearchListLogic.GetSyouhin()
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
    ''' <summary>入力チェック</summary>
    ''' <param name="strObjId">クライアントID</param>
    ''' <returns>エラーメッセージ</returns>
    Private Function CheckInput(ByRef strObjId As String) As String
        Dim csScript As New StringBuilder
        With csScript
            '相手先コード(From)(英数字チェック)
            If Me.tbxAiteSakiCd.Text <> String.Empty Then
                .Append(commonCheck.ChkHankakuEisuuji(Me.tbxAiteSakiCd.Text, "相手先コード"))
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
    ''' <param name="strKojKaisyaCd">工事会社コード</param>
    ''' <remarks></remarks>
    Private Sub KensakuSyori(ByVal strAiteSakiSyubetu As String, ByVal strAiteSakiCd As String, ByVal strSyouhinCd As String, ByVal strKojKaisyaCd As String)

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
            Dim KojKakakuSearchListLogic As New KojKakakuMasterLogic
            Dim dtKojKakaku As New Data.DataTable
            dtKojKakaku = KojKakakuSearchListLogic.GetHanbaiKakakuKobeituSettei(strAiteSakiSyubetu, strAiteSakiCd, strSyouhinCd, strKojKaisyaCd)

            If dtKojKakaku.Rows.Count > 0 Then
                With dtKojKakaku.Rows(0)

                    '取消
                    If .Item("torikesi").ToString.Trim.Equals("0") Then
                        Me.chkTorikesi.Checked = False
                    Else
                        Me.chkTorikesi.Checked = True
                    End If

                    '売上金額
                    If Not .Item("uri_gaku").ToString.Trim.Equals(String.Empty) Then
                        Me.tbxUriGaku.Text = FormatNumber(.Item("uri_gaku").ToString.Trim, 0)
                    Else
                        Me.tbxUriGaku.Text = "0"
                    End If

                    '工事会社請求有無
                    If .Item("kojumu").ToString.Trim.Equals("1") Then
                        Me.ddlKojGaisyaSeikyuuUmu.SelectedValue = "1"
                    Else
                        Me.ddlKojGaisyaSeikyuuUmu.SelectedValue = "0"
                    End If

                    '請求有無
                    If .Item("seikyuumu").ToString.Trim.Equals("1") Then
                        Me.ddlSeikyuuUmu.SelectedValue = "1"
                    ElseIf .Item("seikyuumu").ToString.Trim.Equals("0") Then
                        Me.ddlSeikyuuUmu.SelectedValue = "0"
                    Else
                        Me.ddlSeikyuuUmu.SelectedValue = ""
                    End If
                End With
            Else
                '取消
                Me.chkTorikesi.Checked = False
                '工務店請求金額
                Me.tbxUriGaku.Text = "0"
                '売上金額
                Me.tbxUriGaku.Text = "0"
                '工事会社請求有無
                Me.ddlKojGaisyaSeikyuuUmu.SelectedValue = "0"
                '請求有無
                Me.ddlSeikyuuUmu.SelectedValue = "0"
            End If
        Else
            '取消
            Me.chkTorikesi.Checked = False
            '工務店請求金額
            Me.tbxUriGaku.Text = "0"
            '売上金額
            Me.tbxUriGaku.Text = "0"
            '工事会社請求有無
            Me.ddlKojGaisyaSeikyuuUmu.SelectedValue = "0"
            '請求有無
            Me.ddlSeikyuuUmu.SelectedValue = "0"
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
        Dim strKojKaisyaCd As String
        strKojKaisyaCd = Me.tbxKojKaisyaCd.Text.Trim
        Dim KojKakakuSearchListLogic As New KojKakakuMasterLogic
        '存在チェツク
        Dim dtKojKakaku As New Data.DataTable
        dtKojKakaku = KojKakakuSearchListLogic.CheckSonzai(strAiteSakiSyubetu, strAiteSakiCd, strSyouhinCd, strKojKaisyaCd)

        If dtKojKakaku.Rows.Count > 0 Then
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
        Dim strKojkaisyaCd As String
        strKojkaisyaCd = Me.tbxKojKaisyaCd.Text

        'パラメータ
        Dim dtKojKakakuOk As New Data.DataTable
        Call Me.SetKojKakaku(dtKojKakakuOk)
        Dim dr As Data.DataRow
        dr = dtKojKakakuOk.NewRow
        dr.Item("aitesaki_syubetu") = strAiteSakiSyubetu
        dr.Item("aitesaki_cd") = strAiteSakiCd
        dr.Item("syouhin_cd") = strSyouhinCd
        dr.Item("koj_gaisya_cd") = Left(Right("       " & strKojkaisyaCd.Trim, 7), 5).Trim
        dr.Item("koj_gaisya_jigyousyo_cd") = Right(strKojkaisyaCd.Trim, 2).Trim
        If Me.chkTorikesi.Checked Then
            dr.Item("torikesi") = "1"
        Else
            dr.Item("torikesi") = "0"
        End If
        Dim strUriGaku As String = Me.tbxUriGaku.Text.Trim.Replace(",", String.Empty)
        dr.Item("uri_gaku") = IIf(strUriGaku.Equals(String.Empty), "0", strUriGaku)
        dr.Item("seikyuu_umu") = Me.ddlSeikyuuUmu.SelectedValue.Trim
        dr.Item("koj_gaisya_seikyuu_umu") = Me.ddlKojGaisyaSeikyuuUmu.SelectedValue.Trim
   

        Select Case strKbn
            Case "insert"
                dr.Item("ins_upd_flg") = "0"
            Case "update"
                dr.Item("ins_upd_flg") = "1"
        End Select

        dtKojKakakuOk.Rows.Add(dr)
        Dim KojKakakuSearchListLogic As New KojKakakuMasterLogic
        If KojKakakuSearchListLogic.SetKojKakakuKobeituSettei(dtKojKakakuOk, strUserId).Equals(False) Then

            Call Me.ShowMessage(Messages.Instance.MSG2058E)

        Else

            '当画面を閉じる
            ClientScript.RegisterStartupScript(Me.GetType(), "ShowMessage", "fncCloseWindow();", True)
        End If

    End Sub
    ''' <summary>工事価格テーブルを作成する</summary>
    ''' <param name="dtKojKakakuOk">工事価格テーブル</param>
    Public Sub SetKojKakaku(ByRef dtKojKakakuOk As Data.DataTable)

        Dim dc As Data.DataColumn
        dc = New Data.DataColumn
        dc.ColumnName = "ins_upd_flg"       '追加更新FLG(0:追加; 1:更新)
        dtKojKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "aitesaki_syubetu"  '相手先種別
        dtKojKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "aitesaki_cd"       '相手先コード
        dtKojKakakuOk.Columns.Add(dc)

        dc = New Data.DataColumn
        dc.ColumnName = "syouhin_cd"        '商品コード
        dtKojKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "torikesi"          '取消
        dtKojKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "koj_gaisya_cd"  '工事会社コード
        dtKojKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "koj_gaisya_jigyousyo_cd"  '工事会社コード
        dtKojKakakuOk.Columns.Add(dc)

        dc = New Data.DataColumn
        dc.ColumnName = "koj_gaisya_seikyuu_umu"   '工事会社請求有無
        dtKojKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "seikyuu_umu"  '請求有無
        dtKojKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "uri_gaku"   '売上金額
        dtKojKakakuOk.Columns.Add(dc)

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
            .AppendLine("           document.all." & Me.tbxAiteSakiMei.ClientID & ".value = '相手先なし';")
            .AppendLine("			break; ")
            .AppendLine("		default: ")
            .AppendLine("           document.all." & Me.tbxAiteSakiCd.ClientID & ".value = '';")
            .AppendLine("           document.all." & Me.tbxAiteSakiCd.ClientID & ".disabled = false;")
            .AppendLine("           document.all." & Me.btnAiteSakiCd.ClientID & ".disabled = false;")
            .AppendLine("           document.all." & Me.tbxAiteSakiMei.ClientID & ".value = '';")
            .AppendLine("	} ")
            '.AppendLine("   alert('hdnKensakuFlg:'+document.getElementById('" & Me.hdnKensakuFlg.ClientID & "').value);")
            .AppendLine("}")
            .AppendLine("function fncKojKaisyaSearch(){")
            .AppendLine("       var objRetrun = '" & Me.hdnKensakuFlg2.ClientID & "';")
            .AppendLine("       var hdnId = '" & Me.hdnKojKaisyaCd.ClientID & "';")
            .AppendLine(" if (document.all." & Me.tbxKojKaisyaCd.ClientID & ".value=='ALLAL'){")
            .AppendLine("       document.getElementById('" & Me.hdnKensakuFlg2.ClientID & "').value = '1';")
            .AppendLine(" document.all." & Me.tbxKojKaisyaMei.ClientID & ".value='指定無し'}else{")
            .AppendLine("       objSrchWin = window.open('search_common.aspx?blnDelete=True&Kbn='+escape('工事会社')+'&FormName=" & _
            Me.Page.Form.Name & "&objCd=" & _
              tbxKojKaisyaCd.ClientID & _
                       "&objMei=" & tbxKojKaisyaMei.ClientID & _
                       "&strCd='+escape(eval('document.all.'+'" & _
                       tbxKojKaisyaCd.ClientID & "').value)+'&strMei='+escape(eval('document.all.'+'" & _
                       tbxKojKaisyaMei.ClientID & "').value)+'&objRetrun='+objRetrun+'&hdnId='+hdnId, 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');")
            .AppendLine("}")
            .AppendLine("       return false;")
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

            .AppendLine("   if(document.all." & Me.tbxAiteSakiCd.ClientID & ".value != document.getElementById('" & Me.hdnAiteSakiCd.ClientID & "').value)")
            .AppendLine("   {")
            .AppendLine("       document.getElementById('" & Me.hdnKensakuFlg.ClientID & "').value = '0';")
            .AppendLine("       document.getElementById('" & Me.hdnAiteSakiCd.ClientID & "').value = document.all." & Me.tbxAiteSakiCd.ClientID & ".value;")
            .AppendLine("   }")
            .AppendLine("}")
            .AppendLine("function fncKojKaisyaCdChange()")
            .AppendLine("{")

            .AppendLine("   if(document.all." & Me.tbxKojKaisyaCd.ClientID & ".value != document.getElementById('" & Me.hdnKojKaisyaCd.ClientID & "').value)")
            .AppendLine("   {")
            .AppendLine("       document.getElementById('" & Me.hdnKensakuFlg2.ClientID & "').value = '0';")
            .AppendLine("       document.getElementById('" & Me.hdnKojKaisyaCd.ClientID & "').value = document.all." & Me.tbxKojKaisyaCd.ClientID & ".value;")
            .AppendLine("   }")
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
            .AppendLine("       if(chkHankakuEisuuji(document.all." & Me.tbxAiteSakiCd.ClientID & ".value) == false)")
            .AppendLine("       {")
            .AppendLine("           alert('" & String.Format(Messages.Instance.MSG2005E, "相手先コード").ToString & "');")
            .AppendLine("           document.all." & Me.tbxAiteSakiCd.ClientID & ".focus();")
            .AppendLine("           return false; ")
            .AppendLine("       }")

            .AppendLine("   }")
            '商品
            .AppendLine("   if(document.all." & Me.ddlSyouhinCd.ClientID & ".value == '')")
            .AppendLine("   {")
            .AppendLine("       alert('" & Messages.Instance.MSG013E.Replace("@PARAM1", "商品") & "');")
            .AppendLine("       document.all." & Me.ddlSyouhinCd.ClientID & ".focus();")
            .AppendLine("       return false; ")
            .AppendLine("   }")
            '工事会社
            .AppendLine("   if(document.all." & Me.tbxKojKaisyaCd.ClientID & ".value == '')")
            .AppendLine("   {")
            .AppendLine("       alert('" & Messages.Instance.MSG013E.Replace("@PARAM1", "工事会社") & "');")
            .AppendLine("       document.all." & Me.tbxKojKaisyaCd.ClientID & ".focus();")
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
            .AppendLine("       if(document.getElementById('" & Me.hdnKensakuFlg2.ClientID & "').value == '0')")
            .AppendLine("       {")
            .AppendLine("           alert('" & Replace(Messages.Instance.MSG2054E, "相手先", "工事会社") & "');")
            .AppendLine("           document.all." & Me.tbxKojKaisyaCd.ClientID & ".focus();")
            .AppendLine("           return false; ")
            .AppendLine("       }")
            .AppendLine("   var strMenoy;")
            '工務店請求金額
            .AppendLine("   if(!fncCheckMenoy(document.all." & Me.tbxUriGaku.ClientID & ".value))")
            .AppendLine("   {")
            .AppendLine("       alert('" & String.Format(Messages.Instance.MSG2055E, "売上金額") & "');")
            .AppendLine("       document.all." & Me.tbxUriGaku.ClientID & ".focus();")
            .AppendLine("       return false; ")
            .AppendLine("   }")

            .AppendLine("   strMenoy = Trim(document.all." & Me.tbxUriGaku.ClientID & ".value).replace(/,/g,'');")
            .AppendLine("   if((strMenoy<0)||(strMenoy>2147483647))")
            .AppendLine("   {")
            .AppendLine("       alert('" & String.Format(Messages.Instance.MSG2056E, "売上金額") & "');")
            .AppendLine("       document.all." & Me.tbxUriGaku.ClientID & ".focus();")
            .AppendLine("       return false; ")
            .AppendLine("   }")
            '====================↓2015/11/17 追加↓====================
            .AppendLine("   strMenoy = (strMenoy == '')?'0':strMenoy;")
            .AppendLine("   var strSeikyuuUmu = document.all." & Me.ddlSeikyuuUmu.ClientID & ".value;")
            '売上金額が0円ではない場合　且つ　請求有無　「無」　選択時
            .AppendLine("   if((strMenoy != 0) && (strSeikyuuUmu == '0'))")
            .AppendLine("   {")
            .AppendLine("       alert('" & Messages.Instance.MSG2078E & "');")
            .AppendLine("       document.all." & Me.ddlSeikyuuUmu.ClientID & ".focus();")
            .AppendLine("       return false; ")
            .AppendLine("   }")
            '売上金額が0円の場合　且つ　請求有無　「有」　選択時
            .AppendLine("   if((strMenoy == 0) && (strSeikyuuUmu == '1'))")
            .AppendLine("   {")
            .AppendLine("       alert('" & Messages.Instance.MSG2079E & "');")
            .AppendLine("       document.all." & Me.ddlSeikyuuUmu.ClientID & ".focus();")
            .AppendLine("       return false; ")
            .AppendLine("   }")
            '====================↑2015/11/17 追加↑====================
          
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
    ''' <history>2011/09/15 高(大連情報システム部)　新規作成</history>
    Private Sub ShowKakuninMessage(ByVal strMessage As String)

        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("fncDbSyoriKakunin('" & strMessage & "'); ")
        End With
        'ページ応答で、クライアント側のスクリプト ブロックを出力します
        ClientScript.RegisterStartupScript(Me.GetType(), "ShowTourokuMessage", csScript.ToString, True)
    End Sub

End Class
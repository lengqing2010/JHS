Imports Itis.Earth.BizLogic
Imports Itis.Earth.DataAccess

Partial Public Class SeikyuuSakiTyuuijikouInput
    Inherits System.Web.UI.Page

    Private seikyuuSakiTyuuijikouLogic As New SeikyuuSakiTyuuijikouLogic
    Private commonCheck As New CommonCheck

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '権限チェック
        Dim commonChk As New CommonCheck
        Dim strUserID As String = ""
        Dim blnKeiriKengen As Boolean
        blnKeiriKengen = commonChk.CommonNinnsyou(strUserID, "keiri_gyoumu_kengen")
        If Not blnKeiriKengen Then
            'エラー画面へ遷移して、エラーメッセージを表示する
            Context.Items("strFailureMsg") = Messages.Instance.MSG2020E
            Server.Transfer("CommonErr.aspx")
        End If

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



        End If
        '「閉じる」ボタン
        Me.btnClose.Attributes.Add("onClick", "fncClose();return false;")
        '「検索」ボタン
        Me.btnSeikyuusakiSearch.Attributes.Add("onClick", "fncSeikyuusakiPopup();return false;")
        '請求先区分
        Me.ddlSeikyuusakiKbn.Attributes.Add("onChange", "fncSeikyuusakiChange('kbn');")
        '請求先コード
        Me.tbxSeikyuusakiCd.Attributes.Add("onChange", "fncSeikyuusakiChange('cd');")
        '請求先枝番
        Me.tbxSeikyuusakiBrc.Attributes.Add("onChange", "fncSeikyuusakiChange('brc');")
        '｢新規登録｣ボタン
        Me.btnTouroku.Attributes.Add("onClick", "if(!fncNyuuryokuCheck()){return false;}")

    End Sub

    ''' <summary>初期化</summary> 
    Private Sub Syokika(ByVal sendSearchTerms As String)

        '請求先区分
        Dim strSeikyuusakiKbn As String = String.Empty
        '請求先コード
        Dim strSeikyuusakiCd As String = String.Empty
        '請求先枝番
        Dim strSeikyuusakiBrc As String = String.Empty
        '入力№
        Dim strNo As String = String.Empty

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

            '入力№
            If arrSearchTerm.Length > 3 Then
                Try
                    Dim intNo As Integer
                    intNo = CInt(arrSearchTerm(3).Trim)
                    strNo = arrSearchTerm(3).Trim
                Catch ex As Exception
                    strNo = String.Empty
                End Try
                '請求先枝番を設定する
                If (Not strSeikyuusakiKbn.Trim.Equals(String.Empty)) AndAlso (Not strSeikyuusakiCd.Trim.Equals(String.Empty)) AndAlso (Not strSeikyuusakiBrc.Trim.Equals(String.Empty)) Then
                    Call Me.SetNo(strNo)
                Else
                    Call Me.SetNo(String.Empty)
                End If
            Else
                Call Me.SetNo(String.Empty)
            End If

        Else
            '請求先区分を設定する
            Call Me.SetSeikyuusakiKbn(String.Empty)

            '請求先コードを設定する
            Call Me.SetSeikyuusakiCd(String.Empty)

            '請求先枝番を設定する
            Call Me.SetSeikyuusakiBrc(String.Empty)

            '入力№
            Call Me.SetNo(String.Empty)
        End If

        ''請求先名を設定する
        'Call Me.SetSeikyuusakiMei()

        ''種別コードを設定する
        'Call Me.SetSyubetuCd(String.Empty)

        ''重要度を設定する
        'Call Me.SetJyuuyoudo(String.Empty)

        ''請求締め日を設定する
        'Call Me.SetSeikyuuSimeDate()

        ''請求書必着日を設定する
        'Call Me.SetSeikyuusyoHittykDate()

        '請求先(区分・コード・枝番)ある場合
        If (Not strSeikyuusakiKbn.Trim.Equals(String.Empty)) AndAlso (Not strSeikyuusakiCd.Trim.Equals(String.Empty)) AndAlso (Not strSeikyuusakiBrc.Trim.Equals(String.Empty)) Then
            '請求先を設定する
            Call Me.SetSeikyuusaki(strSeikyuusakiKbn.Trim, strSeikyuusakiCd.Trim, strSeikyuusakiBrc.Trim)

            '請求先区分
            Me.hdnSeikyuusakiKbn.Value = strSeikyuusakiKbn.Trim
            '請求先コード
            Me.hdnSeikyuusakiCd.Value = strSeikyuusakiCd.Trim
            '請求先枝番
            Me.hdnSeikyuusakiBrc.Value = strSeikyuusakiBrc.Trim
            '検索フラグ
            Me.hdnSearchFlg.Value = "1"

            '入力№ある場合
            If Not strNo.Trim.Equals(String.Empty) Then
                '請求先注意事項を設定する
                Call Me.SetSeikyuusakiTyuuijikou(strSeikyuusakiKbn.Trim, strSeikyuusakiCd.Trim, strSeikyuusakiBrc.Trim, strNo.Trim)

                '請求先(区分・コード・枝番・検索ボタン)は非活性にする
                Me.ddlSeikyuusakiKbn.Enabled = False
                Me.tbxSeikyuusakiCd.Enabled = False
                Me.tbxSeikyuusakiBrc.Enabled = False
                Me.btnSeikyuusakiSearch.Enabled = False
            Else
                '重要度
                Call Me.SetJyuuyoudo(String.Empty)
                '取消
                Me.chkTorikesi.Checked = False
                '種別コード
                Call Me.SetSyubetuCd(String.Empty)
                '詳細
                Me.tbxSyousai.Text = String.Empty
            End If
        Else
            '請求先名
            Me.tbxSeikyuusakiMei.Text = String.Empty
            '請求締め日
            Me.lblSeikyuuSimeDate.Text = String.Empty
            '請求書必着日
            Me.lblSeikyuusyoHittykDate.Text = String.Empty
            '重要度
            Call Me.SetJyuuyoudo(String.Empty)
            '取消
            Me.chkTorikesi.Checked = False
            '種別コード
            Call Me.SetSyubetuCd(String.Empty)
            '詳細
            Me.tbxSyousai.Text = String.Empty
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

    ''' <summary>入力№を設定する</summary> 
    Private Sub SetNo(ByVal strNo As String)
        '表示の値を設定する
        If Not strNo.Trim.Equals(String.Empty) Then
            Me.lblNo.Text = strNo.Trim
        Else
            Me.lblNo.Text = String.Empty
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
    Private Sub SetSyubetuCd(ByVal strSyubetu As String)

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

        '表示の値を設定する
        If Not strSyubetu.Trim.Equals(String.Empty) Then
            Try
                Me.ddlSyubetuCd.SelectedValue = strSyubetu
            Catch ex As Exception
                Me.ddlSyubetuCd.SelectedValue = String.Empty
            End Try
        Else
            Me.ddlSyubetuCd.SelectedValue = String.Empty
        End If

    End Sub

    ''' <summary>重要度を設定する</summary> 
    Private Sub SetJyuuyoudo(ByVal strJyuuyoudo As String)
        ''高
        'Me.ddlJyuuyoudo.Items.Insert(0, New ListItem("高", "2"))
        ''中
        'Me.ddlJyuuyoudo.Items.Insert(1, New ListItem("中", "1"))
        ''低
        'Me.ddlJyuuyoudo.Items.Insert(2, New ListItem("低", "0"))

        '表示の値を設定する
        If Not strJyuuyoudo.Trim.Equals(String.Empty) Then
            Try
                Me.ddlJyuuyoudo.SelectedValue = strJyuuyoudo
            Catch ex As Exception
                Me.ddlJyuuyoudo.SelectedValue = "0"
            End Try
        Else
            Me.ddlJyuuyoudo.SelectedValue = "0"
        End If

    End Sub

    ''' <summary>請求締め日を設定する</summary> 
    Private Sub SetSeikyuuSimeDate(ByVal strSeikyuuSimeDate As String)
        '表示の値を設定する

        '表示の値を設定する
        If Not strSeikyuuSimeDate.Trim.Equals(String.Empty) Then
            Me.lblSeikyuuSimeDate.Text = strSeikyuuSimeDate.Trim
        Else
            Me.lblSeikyuuSimeDate.Text = String.Empty
        End If


        Me.lblSeikyuuSimeDate.Text = String.Empty
    End Sub

    ''' <summary>請求書必着日を設定する</summary> 
    Private Sub SetSeikyuusyoHittykDate(ByVal strSeikyuusyoHittykDate As String)
        '表示の値を設定する 

        '表示の値を設定する
        If Not strSeikyuusyoHittykDate.Trim.Equals(String.Empty) Then
            Me.lblSeikyuusyoHittykDate.Text = strSeikyuusyoHittykDate.Trim
        Else
            Me.lblSeikyuusyoHittykDate.Text = String.Empty
        End If


        Me.lblSeikyuusyoHittykDate.Text = String.Empty
    End Sub

    ''' <summary>請求先を設定する</summary> 
    Private Sub SetSeikyuusaki(ByVal strSeikyuusakiKbn As String, ByVal strSeikyuusakiCd As String, ByVal strSeikyuusakiBrc As String)
        '請求先情報を取得する
        Dim dtSeikyuusaki As New Data.DataTable
        dtSeikyuusaki = seikyuuSakiTyuuijikouLogic.GetSeikyuusakiInfo(strSeikyuusakiKbn.Trim, strSeikyuusakiCd.Trim, strSeikyuusakiBrc.Trim)

        If dtSeikyuusaki.Rows.Count > 0 Then
            '請求先名
            Me.tbxSeikyuusakiMei.Text = dtSeikyuusaki.Rows(0).Item("seikyuu_saki_mei").ToString.Trim
            '請求締め日
            Me.lblSeikyuuSimeDate.Text = dtSeikyuusaki.Rows(0).Item("seikyuu_sime_date").ToString.Trim
            '請求書必着日
            Me.lblSeikyuusyoHittykDate.Text = dtSeikyuusaki.Rows(0).Item("seikyuusyo_hittyk_date").ToString.Trim
        Else
            '請求先名
            Me.tbxSeikyuusakiMei.Text = String.Empty
            '請求締め日
            Me.lblSeikyuuSimeDate.Text = String.Empty
            '請求書必着日
            Me.lblSeikyuusyoHittykDate.Text = String.Empty
        End If

    End Sub

    ''' <summary>請求先注意事項を設定する</summary> 
    Private Sub SetSeikyuusakiTyuuijikou(ByVal strSeikyuusakiKbn As String, ByVal strSeikyuusakiCd As String, ByVal strSeikyuusakiBrc As String, ByVal strNo As String)
        'DBの時間を取得する
        ViewState("DbTime") = CDate(seikyuuSakiTyuuijikouLogic.GetDbTime().Rows(0).Item(0))

        '請求先注意事項を取得する
        Dim dtSeikyuusakiTyuuijikou As New Data.DataTable
        dtSeikyuusakiTyuuijikou = seikyuuSakiTyuuijikouLogic.GetSeikyuusakiTyuuijikou(strSeikyuusakiKbn.Trim, strSeikyuusakiCd.Trim, strSeikyuusakiBrc.Trim, strNo.Trim)

        If dtSeikyuusakiTyuuijikou.Rows.Count > 0 Then
            '重要度
            Call Me.SetJyuuyoudo(dtSeikyuusakiTyuuijikou.Rows(0).Item("jyuyodo").ToString.Trim)
            '取消
            Me.chkTorikesi.Checked = IIf(dtSeikyuusakiTyuuijikou.Rows(0).Item("torikesi").ToString.Trim.Equals("1"), True, False)
            '種別コード
            Call Me.SetSyubetuCd(dtSeikyuusakiTyuuijikou.Rows(0).Item("syubetu_cd").ToString.Trim)
            '詳細
            Me.tbxSyousai.Text = Me.CutMaxLength(dtSeikyuusakiTyuuijikou.Rows(0).Item("syousai").ToString.Trim, 128)

        Else
            '重要度
            Call Me.SetJyuuyoudo(String.Empty)
            '取消
            Me.chkTorikesi.Checked = False
            '種別コード
            Call Me.SetSyubetuCd(String.Empty)
            '詳細
            Me.tbxSyousai.Text = String.Empty
        End If

    End Sub

    ''' <summary>請求先を検索した後、請求先情報と請求先注意事項を取得する</summary> 
    Private Sub btnDisplay_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDisplay.Click

        '請求先区分
        Dim strSeikyuusakiKbn As String = Me.ddlSeikyuusakiKbn.SelectedValue.Trim
        '請求先コード
        Dim strSeikyuusakiCd As String = Me.tbxSeikyuusakiCd.Text.Trim
        '請求先枝番
        Dim strSeikyuusakiBrc As String = Me.tbxSeikyuusakiBrc.Text.Trim
        '入力№
        Dim strNo As String = Me.lblNo.Text.Trim

        '請求先(区分・コード・枝番)ある場合
        If (Not strSeikyuusakiKbn.Trim.Equals(String.Empty)) AndAlso (Not strSeikyuusakiCd.Trim.Equals(String.Empty)) AndAlso (Not strSeikyuusakiBrc.Trim.Equals(String.Empty)) Then
            '請求先を設定する
            Call Me.SetSeikyuusaki(strSeikyuusakiKbn.Trim, strSeikyuusakiCd.Trim, strSeikyuusakiBrc.Trim)

            '請求先区分
            Me.hdnSeikyuusakiKbn.Value = strSeikyuusakiKbn.Trim
            '請求先コード
            Me.hdnSeikyuusakiCd.Value = strSeikyuusakiCd.Trim
            '請求先枝番
            Me.hdnSeikyuusakiBrc.Value = strSeikyuusakiBrc.Trim
            '検索フラグ
            Me.hdnSearchFlg.Value = "1"

            '入力№ある場合
            If Not strNo.Trim.Equals(String.Empty) Then
                '請求先注意事項を設定する
                Call Me.SetSeikyuusakiTyuuijikou(strSeikyuusakiKbn.Trim, strSeikyuusakiCd.Trim, strSeikyuusakiBrc.Trim, strNo.Trim)

                '請求先(区分・コード・枝番・検索ボタン)は非活性にする
                Me.ddlSeikyuusakiKbn.Enabled = False
                Me.tbxSeikyuusakiCd.Enabled = False
                Me.tbxSeikyuusakiBrc.Enabled = False
                Me.btnSeikyuusakiSearch.Enabled = False
            Else
                '重要度
                Call Me.SetJyuuyoudo(String.Empty)
                '取消
                Me.chkTorikesi.Checked = False
                '種別コード
                Call Me.SetSyubetuCd(String.Empty)
                '詳細
                Me.tbxSyousai.Text = String.Empty
            End If
        Else
            '請求先名
            Me.tbxSeikyuusakiMei.Text = String.Empty
            '請求締め日
            Me.lblSeikyuuSimeDate.Text = String.Empty
            '請求書必着日
            Me.lblSeikyuusyoHittykDate.Text = String.Empty
            '重要度
            Call Me.SetJyuuyoudo(String.Empty)
            '取消
            Me.chkTorikesi.Checked = False
            '種別コード
            Call Me.SetSyubetuCd(String.Empty)
            '詳細
            Me.tbxSyousai.Text = String.Empty
        End If

    End Sub

    ''' <summary>｢新規登録｣ボタンを押下時</summary> 
    Private Sub btnTouroku_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTouroku.Click

        '入力チェック
        Dim strObjId As String = String.Empty
        Dim strErrMessage As String = CheckInput(strObjId)
        If strErrMessage <> String.Empty Then
            ShowMessage(strErrMessage, strObjId)
            Exit Sub
        End If

        '請求先区分
        Dim strSeikyuusakiKbn As String = Me.ddlSeikyuusakiKbn.SelectedValue.Trim
        '請求先コード
        Dim strSeikyuusakiCd As String = Me.tbxSeikyuusakiCd.Text.Trim
        '請求先枝番
        Dim strSeikyuusakiBrc As String = Me.tbxSeikyuusakiBrc.Text.Trim
        '入力№
        Dim strNo As String = Me.lblNo.Text.Trim
        '取消
        Dim strTorikesi As String = IIf(Me.chkTorikesi.Checked, "1", "0")
        '種別コード
        Dim strSyubetuCd As String = Me.ddlSyubetuCd.SelectedValue.Trim
        '詳細
        Dim strSyousai As String = Me.tbxSyousai.Text
        '重要度
        Dim strJyuuyoudo As String = Me.ddlJyuuyoudo.SelectedValue.Trim
        'ユーザーID
        Dim ninsyou As New Ninsyou()
        Dim strUserId As String = ninsyou.GetUserID()

        '請求先存在チェック
        If Not seikyuuSakiTyuuijikouLogic.GetSeikyuusakiCheck(strSeikyuusakiKbn, strSeikyuusakiCd, strSeikyuusakiBrc) Then
            ShowMessage(Messages.Instance.MSG2063E, String.Empty)
        End If

        '入力№が空白の場合
        If strNo.Equals(String.Empty) Then
            '入力№の最大+1で登録処理を行う
            If Not seikyuuSakiTyuuijikouLogic.InsSeikyuusakiTyuuijikou(strSeikyuusakiKbn, strSeikyuusakiCd, strSeikyuusakiBrc, strNo, strTorikesi, strSyubetuCd, strSyousai, strJyuuyoudo, strUserId) Then
                ShowMessage(Messages.Instance.MSG2058E, String.Empty)
                Exit Sub
            Else
                '当画面を閉じる
                ClientScript.RegisterStartupScript(Me.GetType(), "WindowClose", "fncClose();", True)
            End If
        Else
            Dim dtSeikyuusakiTyuuijikouCheck As New Data.DataTable
            dtSeikyuusakiTyuuijikouCheck = seikyuuSakiTyuuijikouLogic.GetSeikyuusakiTyuuijikouCheck(strSeikyuusakiKbn, strSeikyuusakiCd, strSeikyuusakiBrc, strNo)
            If dtSeikyuusakiTyuuijikouCheck.Rows.Count > 0 Then
                'DB処理確認メッセージ表示()
                Call Me.ShowKakuninMessage(Messages.Instance.MSG2057E)
            Else
                ShowMessage(Messages.Instance.MSG2062E, String.Empty)
                Exit Sub
            End If
        End If

    End Sub

    ''' <summary>更新処理</summary>
    Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        '請求先区分
        Dim strSeikyuusakiKbn As String = Me.ddlSeikyuusakiKbn.SelectedValue.Trim
        '請求先コード
        Dim strSeikyuusakiCd As String = Me.tbxSeikyuusakiCd.Text.Trim
        '請求先枝番
        Dim strSeikyuusakiBrc As String = Me.tbxSeikyuusakiBrc.Text.Trim
        '入力№
        Dim strNo As String = Me.lblNo.Text.Trim
        '取消
        Dim strTorikesi As String = IIf(Me.chkTorikesi.Checked, "1", "0")
        '種別コード
        Dim strSyubetuCd As String = Me.ddlSyubetuCd.SelectedValue.Trim
        '詳細
        Dim strSyousai As String = Me.tbxSyousai.Text
        '重要度
        Dim strJyuuyoudo As String = Me.ddlJyuuyoudo.SelectedValue.Trim
        'ユーザーID
        Dim ninsyou As New Ninsyou()
        Dim strUserId As String = ninsyou.GetUserID()

        '排他チェックを行う
        Dim dtSeikyuusakiTyuuijikouCheck As New Data.DataTable
        dtSeikyuusakiTyuuijikouCheck = seikyuuSakiTyuuijikouLogic.GetSeikyuusakiTyuuijikouCheck(strSeikyuusakiKbn, strSeikyuusakiCd, strSeikyuusakiBrc, strNo)
        If dtSeikyuusakiTyuuijikouCheck.Rows.Count > 0 Then
            With dtSeikyuusakiTyuuijikouCheck.Rows(0)
                '登録時間
                If Not IsDBNull(.Item("add_datetime")) Then
                    If DateTime.Compare(CDate(.Item("add_datetime")), CDate(ViewState("DbTime"))) > 0 Then
                        ShowMessage(Messages.Instance.MSG2061E, String.Empty)
                        Exit Sub
                    End If
                End If
                '更新時間
                If Not IsDBNull(.Item("upd_datetime")) Then
                    If DateTime.Compare(CDate(.Item("upd_datetime")), CDate(ViewState("DbTime"))) > 0 Then
                        ShowMessage(Messages.Instance.MSG2061E, String.Empty)
                        Exit Sub
                    End If
                End If
            End With
        Else
            ShowMessage(Messages.Instance.MSG2062E, String.Empty)
            Exit Sub
        End If

        '更新処理を行う
        If Not seikyuuSakiTyuuijikouLogic.UpdSeikyuusakiTyuuijikou(strSeikyuusakiKbn, strSeikyuusakiCd, strSeikyuusakiBrc, strNo, strTorikesi, strSyubetuCd, strSyousai, strJyuuyoudo, strUserId) Then
            ShowMessage(Messages.Instance.MSG2058E, String.Empty)
            Exit Sub
        Else
            '当画面を閉じる
            ClientScript.RegisterStartupScript(Me.GetType(), "WindowClose", "fncClose();", True)
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
                    If Me.tbxSeikyuusakiCd.Enabled Then
                        strObjId = Me.tbxSeikyuusakiCd.ClientID
                    Else
                        strObjId = String.Empty
                    End If
                End If
            End If

            '請求先枝番(半角英数字チェック)
            If Me.tbxSeikyuusakiBrc.Text <> String.Empty Then
                .Append(commonCheck.ChkHankakuEisuuji(Me.tbxSeikyuusakiBrc.Text, "請求先枝番"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    If Me.tbxSeikyuusakiBrc.Enabled Then
                        strObjId = Me.tbxSeikyuusakiBrc.ClientID
                    Else
                        strObjId = String.Empty
                    End If
                End If
            End If

            '詳細が128バイトを超える場合
            If Not Me.tbxSyousai.Text.Equals(String.Empty) Then
                Dim hEncoding As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")
                Dim btBytes As Byte() = hEncoding.GetBytes(Me.tbxSyousai.Text)
                If btBytes.LongLength > 128 Then
                    .Append(Messages.Instance.MSG2060E)
                    If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                        strObjId = Me.tbxSyousai.ClientID
                    End If
                End If
            End If

            '詳細は禁止文字(改行コードを含める)がある場合
            If Me.tbxSyousai.Text <> String.Empty Then
                .Append(commonCheck.CheckKinsoku(Me.tbxSyousai.Text, "詳細"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxSyousai.ClientID
                End If
            End If

        End With
        Return csScript.ToString
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

    ''' <summary>DB処理確認メッセージ表示</summary>
    Private Sub ShowKakuninMessage(ByVal strMessage As String)
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("fncDbSyoriKakunin('" & strMessage & "'); ")
        End With
        'ページ応答で、クライアント側のスクリプト ブロックを出力します
        ClientScript.RegisterStartupScript(Me.GetType(), "ShowTourokuMessage", csScript.ToString, True)
    End Sub

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

            '請求先の検索popup
            .AppendLine("	function fncSeikyuusakiPopup() ")
            .AppendLine("	{ ")
            .AppendLine("		window.open('search_Seikyuusaki.aspx?blnDelete=False&Kbn='+escape('請求先')+'&FormName=" & Me.Page.Form.Name & " &objKbn=" & Me.ddlSeikyuusakiKbn.ClientID & "&objCd=" & Me.tbxSeikyuusakiCd.ClientID & "&objBrc=" & Me.tbxSeikyuusakiBrc.ClientID & "&objMei=" & Me.tbxSeikyuusakiMei.ClientID & "&strKbn='+escape(document.getElementById('" & Me.ddlSeikyuusakiKbn.ClientID & "').value)+'&strCd='+escape(document.getElementById('" & Me.tbxSeikyuusakiCd.ClientID & "').value)+'&strBrc='+escape(document.getElementById('" & Me.tbxSeikyuusakiBrc.ClientID & "').value)+'&objBtn=" & Me.btnDisplay.ClientID & "','SeikyuusakiPopup','menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes'); ")
            .AppendLine("	} ")

            '請求先の変更
            .AppendLine("   function fncSeikyuusakiChange(strKbn) ")
            .AppendLine("   { ")
            .AppendLine("       var objGamen; ")
            .AppendLine("       var objHidden; ")
            .AppendLine("       switch(strKbn) ")
            .AppendLine("	    { ")
            .AppendLine("           case 'kbn':")
            .AppendLine("               objGamen = document.getElementById('" & Me.ddlSeikyuusakiKbn.ClientID & "');")
            .AppendLine("               objHidden = document.getElementById('" & Me.hdnSeikyuusakiKbn.ClientID & "');")
            .AppendLine("               break; ")
            .AppendLine("           case 'cd':")
            .AppendLine("               objGamen = document.getElementById('" & Me.tbxSeikyuusakiCd.ClientID & "');")
            .AppendLine("               objHidden = document.getElementById('" & Me.hdnSeikyuusakiCd.ClientID & "');")
            .AppendLine("               break; ")
            .AppendLine("           case 'brc':")
            .AppendLine("               objGamen = document.getElementById('" & Me.tbxSeikyuusakiBrc.ClientID & "');")
            .AppendLine("               objHidden = document.getElementById('" & Me.hdnSeikyuusakiBrc.ClientID & "');")
            .AppendLine("               break; ")
            .AppendLine("		    default: ")
            .AppendLine("               document.getElementById('" & Me.hdnSearchFlg.ClientID & "').value = '0'; ")
            .AppendLine("               return false; ")
            .AppendLine("       } ")
            .AppendLine("       if(objGamen.value != objHidden.value) ")
            .AppendLine("       { ")
            .AppendLine("           document.getElementById('" & Me.hdnSearchFlg.ClientID & "').value = '0'; ")
            .AppendLine("       } ")
            .AppendLine("   } ")

            '｢登録｣ボタンを押下時、入力チェック
            .AppendLine("   function fncNyuuryokuCheck() ")
            .AppendLine("   { ")
            '請求先区分が未入力の場合
            .AppendLine("       var strKbn = Trim(document.getElementById('" & Me.ddlSeikyuusakiKbn.ClientID & "').value); ")
            .AppendLine("       if(strKbn == '') ")
            .AppendLine("       { ")
            .AppendLine("           alert('" & Messages.Instance.MSG013E.Replace("@PARAM1", "請求先区分") & "'); ")
            .AppendLine("           if(document.getElementById('" & Me.ddlSeikyuusakiKbn.ClientID & "').disabled == false) ")
            .AppendLine("           { ")
            .AppendLine("               document.getElementById('" & Me.ddlSeikyuusakiKbn.ClientID & "').focus(); ")
            .AppendLine("           } ")
            .AppendLine("           return false; ")
            .AppendLine("       } ")
            '請求先コードが未入力の場合
            .AppendLine("       var strCd = Trim(document.getElementById('" & Me.tbxSeikyuusakiCd.ClientID & "').value); ")
            .AppendLine("       if(strCd == '') ")
            .AppendLine("       { ")
            .AppendLine("           alert('" & Messages.Instance.MSG013E.Replace("@PARAM1", "請求先コード") & "'); ")
            .AppendLine("           if(document.getElementById('" & Me.tbxSeikyuusakiCd.ClientID & "').disabled == false) ")
            .AppendLine("           { ")
            .AppendLine("               document.getElementById('" & Me.tbxSeikyuusakiCd.ClientID & "').focus(); ")
            .AppendLine("           } ")
            .AppendLine("           return false; ")
            .AppendLine("       } ")
            '請求先コードが未入力の場合
            .AppendLine("       var strBrc = Trim(document.getElementById('" & Me.tbxSeikyuusakiBrc.ClientID & "').value); ")
            .AppendLine("       if(strBrc == '') ")
            .AppendLine("       { ")
            .AppendLine("           alert('" & Messages.Instance.MSG013E.Replace("@PARAM1", "請求先枝番") & "'); ")
            .AppendLine("           if(document.getElementById('" & Me.tbxSeikyuusakiBrc.ClientID & "').disabled == false) ")
            .AppendLine("           { ")
            .AppendLine("               document.getElementById('" & Me.tbxSeikyuusakiBrc.ClientID & "').focus(); ")
            .AppendLine("           } ")
            .AppendLine("           return false; ")
            .AppendLine("       } ")
            '重要度が未入力の場合
            .AppendLine("       var strJyuuyoudo = Trim(document.getElementById('" & Me.ddlJyuuyoudo.ClientID & "').value); ")
            .AppendLine("       if(strJyuuyoudo == '') ")
            .AppendLine("       { ")
            .AppendLine("           alert('" & Messages.Instance.MSG013E.Replace("@PARAM1", "重要度") & "'); ")
            .AppendLine("           document.getElementById('" & Me.ddlJyuuyoudo.ClientID & "').focus(); ")
            .AppendLine("           return false; ")
            .AppendLine("       } ")
            '種別コードが未入力の場合
            .AppendLine("       var strSyubetu = Trim(document.getElementById('" & Me.ddlSyubetuCd.ClientID & "').value); ")
            .AppendLine("       if(strSyubetu == '') ")
            .AppendLine("       { ")
            .AppendLine("           alert('" & Messages.Instance.MSG013E.Replace("@PARAM1", "種別コード") & "'); ")
            .AppendLine("           document.getElementById('" & Me.ddlSyubetuCd.ClientID & "').focus(); ")
            .AppendLine("           return false; ")
            .AppendLine("       } ")
            '請求先区分・コード・枝番のいずれかが変更されて検索ボタンが押されているかのチェック
            .AppendLine("       if(document.getElementById('" & Me.hdnSearchFlg.ClientID & "').value != '1') ")
            .AppendLine("       { ")
            .AppendLine("           alert('" & Messages.Instance.MSG2059E & "'); ")
            .AppendLine("           document.getElementById('" & Me.ddlSeikyuusakiKbn.ClientID & "').focus(); ")
            .AppendLine("           return false; ")
            .AppendLine("       } ")
            .AppendLine("       return true; ")
            .AppendLine("   } ")

            'DB処理確認
            .AppendLine("   function fncDbSyoriKakunin(strMessage) ")
            .AppendLine("   { ")
            .AppendLine("       if(confirm(strMessage)) ")
            .AppendLine("       { ")
            .AppendLine("           document.getElementById('" & Me.btnUpdate.ClientID & "').click(); ")
            .AppendLine("       } ")
            .AppendLine("       else ")
            .AppendLine("       { ")
            .AppendLine("           return false; ")
            .AppendLine("       } ")
            .AppendLine("   } ")

            .AppendLine("	function LTrim(str)   ")
            .AppendLine("	{   ")
            .AppendLine("		var i;  ")
            .AppendLine("		for(i=0;i<str.length;i++)   ")
            .AppendLine("		{   ")
            .AppendLine("			if(str.charAt(i)!=' '&&str.charAt(i)!='　')break;   ")
            .AppendLine("		}   ")
            .AppendLine("		str=str.substring(i,str.length);   ")
            .AppendLine("		return str;   ")
            .AppendLine("	}   ")
            .AppendLine("	function RTrim(str)   ")
            .AppendLine("	{   ")
            .AppendLine("		var i;   ")
            .AppendLine("		for(i=str.length-1;i>=0;i--)   ")
            .AppendLine("		{   ")
            .AppendLine("			if(str.charAt(i)!=' '&&str.charAt(i)!='　')break;   ")
            .AppendLine("		}   ")
            .AppendLine("		str=str.substring(0,i+1);   ")
            .AppendLine("		return str;   ")
            .AppendLine("	}  ")
            .AppendLine("	function Trim(str)   ")
            .AppendLine("	{   ")
            .AppendLine("		return LTrim(RTrim(str));   ")
            .AppendLine("	}   ")
            .AppendLine("	  ")
            .AppendLine("	function left(mainStr,lngLen)  ")
            .AppendLine("	{   ")
            .AppendLine("		if (lngLen>0)  ")
            .AppendLine("		{ ")
            .AppendLine("			return mainStr.substring(0,lngLen); ")
            .AppendLine("		}   ")
            .AppendLine("		else ")
            .AppendLine("		{ ")
            .AppendLine("			return null; ")
            .AppendLine("		}   ")
            .AppendLine("	}   ")
            .AppendLine("	function right(mainStr,lngLen)  ")
            .AppendLine("	{    ")
            .AppendLine("		if (mainStr.length-lngLen>=0 && mainStr.length>=0 && mainStr.length-lngLen<=mainStr.length)  ")
            .AppendLine("		{   ")
            .AppendLine("			return mainStr.substring(mainStr.length-lngLen,mainStr.length); ")
            .AppendLine("		}   ")
            .AppendLine("		else ")
            .AppendLine("		{ ")
            .AppendLine("			return null; ")
            .AppendLine("		}   ")
            .AppendLine("	}   ")
            .AppendLine("	function mid(mainStr,starnum,endnum) ")
            .AppendLine("	{   ")
            .AppendLine("		if (mainStr.length>=0) ")
            .AppendLine("		{   ")
            .AppendLine("			return mainStr.substr(starnum,endnum); ")
            .AppendLine("		} ")
            .AppendLine("		else ")
            .AppendLine("		{ ")
            .AppendLine("			return null; ")
            .AppendLine("		}   ")
            .AppendLine("	} ")
            .AppendLine("	 ")
            .AppendLine("</script>")
        End With
        Page.ClientScript.RegisterStartupScript(Page.GetType, "JS_" & Me.ClientID, sbScript.ToString)
    End Sub


End Class
Imports Itis.Earth.DataAccess
Imports Itis.Earth.BizLogic
Imports System.Web.UI.Page

Partial Public Class kameiten_tourokuryou
    Inherits System.Web.UI.UserControl

#Region "共通変数"
    Private KihonJyouhouInquiryBc As New Itis.Earth.BizLogic.kihonjyouhou.KihonJyouhouInquiryLogic
    Public msgAndFocus As New Itis.Earth.BizLogic.kihonjyouhou.MessageAndFocus
    Private Const SEIKYUUSIMEDATE As Integer = 0
    Private Const HANSOHUHINSEIKYUUSIMEDATE As Integer = 1

    Private Const HANKAKU As Integer = 1
    Private Const ZENKAKU As Integer = 2
    Private Const YM As String = "yyyy/MM"
    Private Const YMD As String = "yyyy/MM/dd"
    Private Const SIGNDAY As String = "d"
    Private Const SIGNMONTH As String = "m"

#End Region

#Region "プロパティ"
    Public _kameiten_cd As String
    Public _keiretuCd As String
    Public _Kbn As String
    Public _MiseCode As String
    Private _upd_login_user_id As String
    Public Property Upd_login_user_id() As String
        Get
            Return _upd_login_user_id
        End Get

        Set(ByVal value As String)
            _upd_login_user_id = value
        End Set

    End Property
    ''' <summary>
    ''' 加盟店コード
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MiseCode() As String
        Get
            Return _MiseCode
        End Get
        Set(ByVal value As String)
            _MiseCode = value
        End Set
    End Property
    ''' <summary>
    ''' 加盟店コード
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Kbn() As String
        Get
            Return _Kbn
        End Get
        Set(ByVal value As String)
            _Kbn = value
        End Set
    End Property

    ''' <summary>
    ''' 加盟店コード
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property kameiten_cd() As String
        Get
            Return _kameiten_cd
        End Get
        Set(ByVal value As String)
            _kameiten_cd = value
        End Set
    End Property
    ''' <summary>
    ''' 系列
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property keiretuCd() As String
        Get
            Return _keiretuCd
        End Get
        Set(ByVal value As String)
            _keiretuCd = value
        End Set
    End Property

    '権限
    Private _kenngenn As Boolean
    Public Property Kenngenn() As Boolean
        Get
            Return _kenngenn
        End Get
        Set(ByVal value As Boolean)
            _kenngenn = value
        End Set
    End Property
#End Region

#Region "画面"


    '編集項目非活性、活性設定対応　20180905　李松涛　対応　↓
    'salesforce項目_編集非活性フラグ 取得
    Private Function Iskassei(ByVal KameitenCd As String, ByVal kbn As String) As Boolean

        If kbn.Trim <> "" Then
            If ViewState("Iskassei") Is Nothing Then
                If kbn = "" Then
                    ViewState("Iskassei") = ""
                Else
                    ViewState("Iskassei") = (New Salesforce).GetSalesforceHikasseiFlgByKbn(kbn)
                End If

            End If
        Else

            If ViewState("Iskassei") Is Nothing Then
                ViewState("Iskassei") = (New Salesforce).GetSalesforceHikasseiFlg(KameitenCd)
            End If

        End If
        Return ViewState("Iskassei").ToString <> "1"
    End Function

    '編集項目非活性、活性設定する
    Public Sub SetKassei()

        ViewState("Iskassei") = Nothing
        Dim kbn As String = ""
        Dim itKassei As Boolean = Iskassei(_kameiten_cd, "")

        tbxAddDate.ReadOnly = Not itKassei
        tbxAddDate.CssClass = GetCss(itKassei, tbxAddDate.CssClass)


        If Not itKassei Then
            CommonKassei.SetDropdownListReadonly(ddlSeikyuuUmu)
        End If

        tbxSyouhinCd.ReadOnly = Not itKassei
        tbxSyouhinCd.CssClass = GetCss(itKassei, tbxSyouhinCd.CssClass)

        btnKansaku.Enabled = itKassei
        btnKansaku.CssClass = GetCss(itKassei, btnKansaku.CssClass)

        tbxZeinuki.ReadOnly = Not itKassei
        tbxZeinuki.CssClass = GetCss(itKassei, tbxZeinuki.CssClass)

        tbxSeikyuDate.ReadOnly = Not itKassei
        tbxSeikyuDate.CssClass = GetCss(itKassei, tbxSeikyuDate.CssClass)

        tbxUriDate.ReadOnly = Not itKassei
        tbxUriDate.CssClass = GetCss(itKassei, tbxUriDate.CssClass)

        tbxBikou.ReadOnly = Not itKassei
        tbxBikou.CssClass = GetCss(itKassei, tbxBikou.CssClass)
    End Sub

    Public Function GetCss(ByVal itKassei As Boolean, ByVal css As String)
        If itKassei Then
            Return Microsoft.VisualBasic.Strings.Replace(css, "readOnly", "", 1, -1, CompareMethod.Text)
        Else
            Return css & " readOnly"
        End If
    End Function

    ''' <summary>
    ''' 画面初期化
    ''' </summary>
    ''' <param name="sender">System.Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            PageInit()
        Else
            If hidLastFocus.Value <> String.Empty Then
                If hidLastFocus.Value = "1" Then
                    tbxAddDate1_TextChanged(sender, e)
                ElseIf hidLastFocus.Value = "2" Then
                    tbxSyouhinCd_TextChanged(sender, e)
                ElseIf hidLastFocus.Value = "3" Then
                    tbxZeinuki_TextChanged(sender, e)
                ElseIf hidLastFocus.Value = "4" Then
                    tbxKoumutenSeikyuuGaku_TextChanged(sender, e)
                ElseIf hidLastFocus.Value = "5" Then
                    tbxSeikyuDate_TextChanged(sender, e)
                ElseIf hidLastFocus.Value = "6" Then
                    tbxAddDate1_TextChanged1(sender, e)
                ElseIf hidLastFocus.Value = "7" Then
                    tbxSyouhinCd1_TextChanged(sender, e)
                ElseIf hidLastFocus.Value = "8" Then
                    tbxZeinuki1_TextChanged(sender, e)
                ElseIf hidLastFocus.Value = "9" Then
                    tbxSeikyuDate1_TextChanged(sender, e)
                Else
                    hidLastFocus.Value = String.Empty
                End If


            End If

            'Dim otherPageFunction As New Itis.Earth.BizLogic.kihonjyouhou.OtherPageFunction
            'otherPageFunction.DoFunction(Parent.Page, "closecover")

        End If
        SetKassei()
    End Sub

    ''' <summary>
    ''' 登録日変更時
    ''' </summary>
    ''' <param name="sender">System.Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub tbxAddDate1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Me.hidLastFocus.Value <> String.Empty Then
            If Me.hidLastFocus.Value <> "1" Then
                Exit Sub
            End If
        End If

        '請求書発行日、売上年月日　　　　 未入力の場合、自動設定　　　　　
        Dim dateValue As String
        dateValue = chkDate(Me.tbxAddDate.Text)
        If dateValue <> "false" Then
            Me.tbxAddDate.Text = dateValue
        Else
            ShowMsg("日付以外が入力されています。", Me.tbxAddDate)
            Me.hidLastFocus.Value = "1"
            Exit Sub
        End If
        ''商品ｺｰﾄﾞの入力チェック
        If Me.tbxSyouhinCd.Text <> String.Empty Then

            '請求書発行日、売上年月日を設定する
            SetDate(False)

            '請求を有にする
            Me.ddlSeikyuuUmu.SelectedIndex = 1

            '税抜金額を入力可能にする
            UnLockItemTextbox(tbxZeinuki)
            '請求書発行日を入力可能にする
            UnLockItemTextbox(tbxSeikyuDate)
        End If

        Me.hidLastFocus.Value = String.Empty

        msgAndFocus.setFocus(Me.Page, Me.ddlSeikyuuUmu)

    End Sub

    ''' <summary>
    ''' 販促品初期ツール料
    ''' 配送日
    ''' </summary>
    ''' <param name="sender">System.Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub tbxAddDate1_TextChanged1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Me.hidLastFocus.Value <> String.Empty Then
            If Me.hidLastFocus.Value <> "6" Then
                Exit Sub
            End If
        End If
        '請求書発行日、売上年月日　　　　 未入力の場合、自動設定　　　　　

        Dim dateValue As String
        dateValue = chkDate(Me.tbxAddDate1.Text)
        If dateValue <> "false" Then
            Me.tbxAddDate1.Text = dateValue
        Else
            Me.hidLastFocus.Value = "6"
            ShowMsg("日付以外が入力されています。", Me.tbxAddDate1)
            Exit Sub
        End If


        ''商品ｺｰﾄﾞの入力チェック
        If Me.tbxSyouhinCd1.Text <> String.Empty Then

            '請求書発行日、売上年月日を設定する
            SetDate1(False)

            '請求を有にする
            Me.ddlSeikyuuUmu1.SelectedIndex = 1

            '税抜金額を入力可能にする
            UnLockItemTextbox(tbxZeinuki1)
            '請求書発行日を入力可能にする
            UnLockItemTextbox(tbxSeikyuDate1)

        End If


        Me.hidLastFocus.Value = String.Empty

        msgAndFocus.setFocus(Me.Page, Me.ddlSeikyuuUmu1)
    End Sub

    ''' <summary>
    ''' 請求有無
    ''' </summary>
    ''' <param name="sender">System.Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    Private Sub ddlSeikyuuUmu_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSeikyuuUmu.SelectedIndexChanged
        '請求入力項目を再設定する
        OperateSeikyuData()

        msgAndFocus.setFocus(Me.Page, Me.tbxSyouhinCd)
    End Sub

    ''' <summary>
    '''  販促品初期ツール料
    ''' 請求有無
    ''' </summary>
    ''' <param name="sender">System.Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    Private Sub ddlSeikyuuUmu1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSeikyuuUmu1.SelectedIndexChanged
        '請求入力項目を再設定する
        OperateSeikyuData1()
        msgAndFocus.setFocus(Me.Page, Me.tbxSyouhinCd1)

    End Sub

    ''' <summary>
    ''' 登録料
    ''' 商品コード変更
    ''' </summary>
    ''' <param name="sender">System.Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    Private Sub tbxSyouhinCd_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbxSyouhinCd.TextChanged
        If Me.hidLastFocus.Value <> String.Empty Then
            If Me.hidLastFocus.Value <> "2" Then

                Exit Sub
            End If
        End If

        '商品コードの変更により各項目を再設定する
        If Not UpdShouhinCd() Then
            Me.hidLastFocus.Value = "2"
            Exit Sub
        End If




        If Me.hidLastFocus.Value <> "2" Then
            msgAndFocus.setFocus(Me.Page, Me.tbxBikou)
        End If
        Me.hidLastFocus.Value = String.Empty

    End Sub

    ''' <summary>
    '''  販促品初期ツール料
    ''' 商品コードを変更
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub tbxSyouhinCd1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbxSyouhinCd1.TextChanged
        If Me.hidLastFocus.Value <> String.Empty Then
            If Me.hidLastFocus.Value <> "7" Then
                Exit Sub
            End If
        End If
        '商品コードの変更により各項目を再設定する
        If Not UpdShouhinCd1() Then
            Me.hidLastFocus.Value = "7"
            Exit Sub
        End If

        If Me.hidLastFocus.Value <> "7" Then
            msgAndFocus.setFocus(Me.Page, Me.tbxBikou1)
        End If
        Me.hidLastFocus.Value = String.Empty
    End Sub

    ''' <summary>
    ''' 登録料
    ''' 実請求変更時
    ''' </summary>
    ''' <param name="sender">System.Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub tbxZeinuki_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If Me.hidLastFocus.Value <> String.Empty Then
            If Me.hidLastFocus.Value <> "3" Then
                Exit Sub
            End If
        End If

        If tbxZeinuki.Text <> String.Empty Then
            If Not Microsoft.VisualBasic.IsNumeric(tbxZeinuki.Text) Then
                ShowMsg(Messages.Instance.MSG2006E, Me.tbxZeinuki, "実請求税抜価格")
                hidLastFocus.Value = "3"
                Exit Sub
            End If
        End If

        '税抜き
        If Not Zeinuki() Then
        End If

        tbxZeinuki.Text = SetKingaku(tbxZeinuki.Text, False)

        Me.hidLastFocus.Value = String.Empty

        msgAndFocus.setFocus(Me.Page, Me.tbxBikou)
    End Sub

    ''' <summary>
    '''  販促品初期ツール料
    ''' 実請求変更時
    ''' </summary>
    ''' <param name="sender">System.Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    Private Sub tbxZeinuki1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbxZeinuki1.TextChanged

        If Me.hidLastFocus.Value <> String.Empty Then
            If Me.hidLastFocus.Value <> "8" Then
                Exit Sub
            End If
        End If

        If tbxZeinuki1.Text <> String.Empty Then
            If Not Microsoft.VisualBasic.IsNumeric(tbxZeinuki1.Text) Then
                Me.hidLastFocus.Value = "8"
                ShowMsg(Messages.Instance.MSG2006E, Me.tbxZeinuki1, "実請求税抜価格")
                Exit Sub
            End If
        End If


        '税抜き
        If Not Zeinuki1() Then

        End If
        tbxZeinuki1.Text = SetKingaku(tbxZeinuki1.Text, False)



        Me.hidLastFocus.Value = String.Empty
        msgAndFocus.setFocus(Me.Page, Me.tbxBikou1)
    End Sub

    ''' <summary>
    ''' 工務店請求設定
    ''' </summary>
    ''' <param name="sender">System.Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    ''' 
    Protected Sub tbxKoumutenSeikyuuGaku_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbxKoumutenSeikyuuGaku.TextChanged

        If Me.hidLastFocus.Value <> String.Empty Then
            If Me.hidLastFocus.Value <> "4" Then
                Exit Sub
            End If
        End If

        If tbxKoumutenSeikyuuGaku.Text <> String.Empty Then
            If Not Microsoft.VisualBasic.IsNumeric(tbxKoumutenSeikyuuGaku.Text) Then
                Me.hidLastFocus.Value = "4"
                ShowMsg(Messages.Instance.MSG2006E, Me.tbxKoumutenSeikyuuGaku, "工務店請求税抜金額")
                Exit Sub
            End If
        End If


        '工務店請求
        If Not SetKoumutenSeikyuu() Then
            'tbxKoumutenSeikyuuGaku.Text = SetKingaku(tbxKoumutenSeikyuuGaku.Text)

            'Exit Sub
        End If

        tbxKoumutenSeikyuuGaku.Text = SetKingaku(tbxKoumutenSeikyuuGaku.Text, False)


        Me.hidLastFocus.Value = String.Empty


        msgAndFocus.setFocus(Me.Page, Me.tbxBikou)
    End Sub

    ''' <summary>
    ''' 登録ボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnTouroku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTouroku.Click

        'チェック
        If Not chkInputValue() Then
            Exit Sub
        End If

        '登録チェック
        Dim msg As String
        msg = KihonJyouhouInquiryBc.ChkTourokuryouTouroku(_kameiten_cd, "200", Me.hidUpdTime.Value)
        If msg <> String.Empty Then
            ShowMsg(msg, btnTouroku)
            Exit Sub
        End If

        Dim otherPageFunction As New Itis.Earth.BizLogic.kihonjyouhou.OtherPageFunction
        If Not otherPageFunction.DoFunction(Parent.Page, "Haitakameiten") Then
            Exit Sub
        End If

        If _Kbn = "A" Then
            '登録チェック
            msg = KihonJyouhouInquiryBc.ChkTourokuryouTouroku(_kameiten_cd, "210", Me.hidUpdTime1.Value)
            If msg <> String.Empty Then
                ShowMsg(msg, btnTouroku)
                Exit Sub
            End If

        End If

        '更新のデータを作成
        Dim insdata As New KameitenjyushoDataSet.t_tenbetu_syoki_seikyuuDataTable
        Dim dr As KameitenjyushoDataSet.t_tenbetu_syoki_seikyuuRow

        '更新のデータを作成
        Dim insdata2 As New KameitenjyushoDataSet.t_tenbetu_syoki_seikyuuDataTable
        Dim dr2 As KameitenjyushoDataSet.t_tenbetu_syoki_seikyuuRow

        dr = insdata.NewRow
        dr.mise_cd = _kameiten_cd
        dr.add_date = Me.tbxAddDate.Text
        dr.bunrui_cd = "200"
        dr.seikyuusyo_hak_date = Me.tbxSeikyuDate.Text
        dr.uri_date = Me.tbxUriDate.Text
        dr.seikyuu_umu = Me.ddlSeikyuuUmu.SelectedValue
        dr.uri_keijyou_date = Me.tbxUriDate.Text
        dr.syouhin_cd = Me.tbxSyouhinCd.Text
        dr.uri_gaku = Me.tbxZeinuki.Text.Replace(",", "")
        dr.zei_kbn = Me.hidZeikbnTxt.Value
        dr.bikou = Me.tbxBikou.Text
        dr.koumuten_seikyuu_gaku = Me.tbxKoumutenSeikyuuGaku.Text.Replace(",", "")
        dr.add_login_user_id = _upd_login_user_id
        dr.upd_login_user_id = _upd_login_user_id
        dr.syouhizei_gaku = Me.lblSyouhizei.Text.Replace(",", "")
        insdata.Rows.Add(dr)

        '区分が"A"(FC)の場合、初期販促品の入力チェックを行なう
        If _Kbn = "A" Then
            dr2 = insdata2.NewRow
            dr2.mise_cd = _kameiten_cd
            dr2.add_date = Me.tbxAddDate1.Text
            dr2.bunrui_cd = "210"
            dr2.seikyuusyo_hak_date = Me.tbxSeikyuDate1.Text
            dr2.uri_date = Me.tbxUriDate1.Text
            dr2.seikyuu_umu = Me.ddlSeikyuuUmu1.SelectedValue
            dr2.uri_keijyou_date = Me.tbxUriDate1.Text
            dr2.syouhin_cd = Me.tbxSyouhinCd1.Text
            dr2.uri_gaku = Me.tbxZeinuki1.Text.Replace(",", "")
            dr2.zei_kbn = Me.hidZeikbnTxt1.Value
            dr2.bikou = Me.tbxBikou1.Text
            dr2.koumuten_seikyuu_gaku = 0
            dr2.add_login_user_id = _upd_login_user_id
            dr2.upd_login_user_id = _upd_login_user_id
            dr2.syouhizei_gaku = Me.lblSyouhizei1.Text.Replace(",", "")
            insdata2.Rows.Add(dr2)
        End If

        Dim updBln As Boolean
        '登録
        If _Kbn = "A" Then
            updBln = KihonJyouhouInquiryBc.SetTenbetuSyokiSeikyuu(_kameiten_cd, insdata, insdata2)
        Else
            updBln = KihonJyouhouInquiryBc.SetTenbetuSyokiSeikyuu(_kameiten_cd, insdata, "200")
        End If


        If Not updBln Then
            Dim mei As String = "登録料"
            If _Kbn = "A" Then
                mei = mei & "および販促品初期ツール料"
            End If
            ShowMsg(Messages.Instance.MSG019E, Me.btnTouroku, mei)
            Exit Sub
        End If

        If _Kbn = "A" Then
            '登録料/初期販促品の登録
            ShowMsg(Messages.Instance.MSG2018E, Me.btnTouroku, "登録料および販促品初期ツール料")

        Else
            '登録料の登録
            ShowMsg(Messages.Instance.MSG2018E, Me.btnTouroku, "登録料")
        End If

        PageInit()

    End Sub

    ''' <summary>
    ''' 請求書発行日
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub tbxSeikyuDate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim dateValue As String
        dateValue = chkDate(Me.tbxSeikyuDate.Text)
        If dateValue <> "false" Then
            Me.tbxSeikyuDate.Text = dateValue
            msgAndFocus.setFocus(Me.Page, Me.tbxBikou)
        Else
            Me.hidLastFocus.Value = "5"
            ShowMsg("日付以外が入力されています。", Me.tbxSeikyuDate)
        End If
        Me.hidLastFocus.Value = String.Empty

    End Sub

    ''' <summary>
    ''' 実請求税抜価格
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub tbxUriDate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim dateValue As String
        dateValue = chkDate(Me.tbxUriDate.Text)
        If dateValue <> "false" Then
            Me.tbxUriDate.Text = dateValue
            msgAndFocus.setFocus(Me.Page, Me.tbxBikou1)
        Else
            ShowMsg("日付以外が入力されています。", Me.tbxUriDate)
        End If
        Me.hidLastFocus.Value = String.Empty
    End Sub

    ''' <summary>
    ''' 販促品初期ツール料
    ''' 実請求税抜価格
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub tbxUriDate1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim dateValue As String
        dateValue = chkDate(Me.tbxUriDate1.Text)
        If dateValue <> "false" Then
            Me.tbxUriDate1.Text = dateValue
        Else
            ShowMsg("日付以外が入力されています。", Me.tbxUriDate1)
        End If
    End Sub

    ''' <summary>
    ''' 販促品初期ツール料
    ''' 請求書発行日
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub tbxSeikyuDate1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim dateValue As String
        dateValue = chkDate(Me.tbxSeikyuDate1.Text)
        If dateValue <> "false" Then
            Me.tbxSeikyuDate1.Text = dateValue
        Else
            Me.hidLastFocus.Value = "9"
            ShowMsg("日付以外が入力されています。", Me.tbxSeikyuDate1)
        End If
        Me.hidLastFocus.Value = String.Empty
    End Sub

    ''' <summary>
    ''' LBTN登録料
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub lbtnTouroku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnTouroku.Click
        '明細を表示
        If meisaiTbody.Style.Item("display") = "none" Then
            meisaiTbody.Style.Item("display") = "inline"
            btnTouroku.Style.Item("display") = "inline"
        Else
            meisaiTbody.Style.Item("display") = "none"
            btnTouroku.Style.Item("display") = "none"
        End If

    End Sub
#End Region

#Region "処理"

    ''' <summary>
    ''' 画面初期化
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub PageInit()

        Setkenngen()

        BindJavaScriptEvent()

        SetGamenValue(True)

    End Sub

    ''' <summary>
    ''' Javascript Event Bind
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BindJavaScriptEvent()

        '年月
        Me.tbxAddDate.Attributes.Add("onfocus", "return setOnfocusNengetu(this)")
        Me.tbxAddDate1.Attributes.Add("onfocus", "return setOnfocusNengetu(this)")
        Me.tbxSeikyuDate.Attributes.Add("onfocus", "return setOnfocusNengetu(this)")
        Me.tbxSeikyuDate1.Attributes.Add("onfocus", "return setOnfocusNengetu(this)")


        'IE FIREROX 登録日変更されない
        Me.tbxAddDate.Attributes.Add("onblur", "LostFocusPostBack(this,1)")
        Me.tbxAddDate1.Attributes.Add("onblur", "LostFocusPostBack(this,6)")
        Me.tbxSeikyuDate.Attributes.Add("onblur", "LostFocusPostBack(this,5)")
        Me.tbxSeikyuDate1.Attributes.Add("onblur", "LostFocusPostBack(this,9)")


        Me.tbxSyouhinCd.Attributes.Add("onblur", "fncToUpper(this);LostFocusPostBack(this,2)")

        Me.tbxZeinuki.Attributes.Add("onblur", "LostFocusPostBack(this,3)")
        Me.tbxKoumutenSeikyuuGaku.Attributes.Add("onblur", "LostFocusPostBack(this,4)")
        Me.tbxZeinuki1.Attributes.Add("onblur", "LostFocusPostBack(this,8)")

        Me.tbxSyouhinCd1.Attributes.Add("onblur", "fncToUpper(this);LostFocusPostBack(this,7)")

        '金額項目
        Me.tbxKoumutenSeikyuuGaku.Attributes.Add("onfocus", "return SetKingaku(this)")
        Me.tbxZeinuki1.Attributes.Add("onfocus", "return SetKingaku(this)")
        Me.tbxZeinuki.Attributes.Add("onfocus", "return SetKingaku(this)")

        Me.tbxBikou.Attributes.Add("onfocus", "return GetFocusOperate(this)")
        Me.tbxBikou1.Attributes.Add("onfocus", "return GetFocusOperate(this)")

        Me.tbxSyouhinCd.Attributes.Add("onfocus", "return GetFocusOperate(this)")
        Me.tbxSyouhinCd1.Attributes.Add("onfocus", "return GetFocusOperate(this)")

        'popup
        Me.btnKansaku.Attributes.Add("onclick", "syainCdHenkouKbn=true;fncOpenwindowSyouhin(200);return false;")
        Me.btnKansaku1.Attributes.Add("onclick", "syainCdHenkouKbn=true;fncOpenwindowSyouhin(210);return false;")

        Me.ddlSeikyuuUmu.Attributes.Add("onchange", "ShowModal();")
        Me.ddlSeikyuuUmu1.Attributes.Add("onchange", "ShowModal();")


    End Sub

    ''' <summary>
    ''' 画面の値を設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetGamenValue(Optional ByVal blnFirst As Boolean = False)

        Dim kameitenTableDataTable As KameitenDataSet.m_kameitenTableDataTable
        Dim tenbetuSyokiseikyu As KameitenjyushoDataSet.t_tenbetu_syoki_seikyuuDataTable

        '加盟店情報取得
        kameitenTableDataTable = KihonJyouhouInquiryBc.GetkameitenInfo(_kameiten_cd)

        '登録料のデータを取得
        tenbetuSyokiseikyu = KihonJyouhouInquiryBc.GetTenbetuSyokiSeikyu(_kameiten_cd, "200")

        Me.ddlSeikyuuUmu.SelectedIndex = 1
        Me.ddlSeikyuuUmu1.SelectedIndex = 1

        If Not kameitenTableDataTable(0).Istys_seikyuu_sakiNull Then
            '馬艶軍  2010/08/17　調査請求先⇒調査請求先コードを変更する ↓
            'If kameitenTableDataTable(0).tys_seikyuu_saki = _MiseCode Then
            If kameitenTableDataTable(0).tys_seikyuu_saki_cd = _MiseCode Then

                lblSeikyusaki.Text = "直接請求"
                hidSeikyusaki.Value = "直"
            Else
                lblSeikyusaki.Text = "他請求"
                hidSeikyusaki.Value = "他"
            End If
        Else
            lblSeikyusaki.Text = "他請求"
            hidSeikyusaki.Value = "他"
        End If

        If _Kbn = "A" Then
            lblSeikyusaki.Text = "ＦＣ請求"
        End If



        'データある時
        If tenbetuSyokiseikyu.Rows.Count > 0 Then

            '登録日
            If Not tenbetuSyokiseikyu(0).Isadd_dateNull Then
                Me.tbxAddDate.Text = Convert.ToDateTime(tenbetuSyokiseikyu(0).add_date).ToString(YMD)
            End If

            '請求有無
            If Not tenbetuSyokiseikyu(0).Isseikyuu_umuNull Then
                If tenbetuSyokiseikyu(0).seikyuu_umu = 1 Then
                    Me.ddlSeikyuuUmu.SelectedIndex = 1
                ElseIf tenbetuSyokiseikyu(0).seikyuu_umu = 0 Then
                    Me.ddlSeikyuuUmu.SelectedIndex = 0
                Else
                    Me.ddlSeikyuuUmu.SelectedIndex = 1
                End If
            Else
                Me.ddlSeikyuuUmu.SelectedIndex = 1
            End If

            '商品CD　名
            If Not tenbetuSyokiseikyu(0).Issyouhin_cdNull Then
                Me.tbxSyouhinCd.Text = tenbetuSyokiseikyu(0).syouhin_cd
                Dim syouhinDataTable As KameitenjyushoDataSet.m_syouhinDataTable
                syouhinDataTable = KihonJyouhouInquiryBc.GetSyouhin(tenbetuSyokiseikyu(0).syouhin_cd)
                If syouhinDataTable.Rows.Count > 0 Then
                    If Not syouhinDataTable(0).Issyouhin_meiNull Then
                        Me.lblSyouhinMei.Text = syouhinDataTable(0).syouhin_mei
                        Me.tbxSyouhinCd.Text = syouhinDataTable(0).syouhin_cd

                    End If

                    If Not syouhinDataTable(0).Ishyoujun_kkkNull Then
                        Me.hidHyoujunkakaku.Value = syouhinDataTable(0).hyoujun_kkk
                    Else
                        Me.hidHyoujunkakaku.Value = "0"
                    End If

                    If Not syouhinDataTable(0).Iszei_kbnNull Then
                        Me.hidZeikbn.Value = syouhinDataTable(0).zei_kbn
                    Else
                        Me.hidZeikbn.Value = ""
                    End If


                End If

            End If

            '実請求税抜金額
            If Not tenbetuSyokiseikyu(0).Isuri_gakuNull Then
                Me.tbxZeinuki.Text = SetKingaku(tenbetuSyokiseikyu(0).uri_gaku, False)
            End If

            '消費税
            If blnFirst Then
                If Not tenbetuSyokiseikyu(0).Iszei_kbnNull Then
                    Me.hidZeikbnTxt.Value = tenbetuSyokiseikyu(0).zei_kbn

                End If
                If tenbetuSyokiseikyu(0).syouhizei_gaku = "0" Then
                    '消費税にをセットする

                    Me.lblSyouhizei.Text = 0
                Else
                    '消費税区分から税率を求める
                    Me.lblSyouhizei.Text = SetKingaku(tenbetuSyokiseikyu(0).syouhizei_gaku)

                    '消費税区分から税率を求める
                End If
                Me.lblZeikomi.Text = SetKingaku((SetVal(SetDouble(tbxZeinuki.Text.Replace(",", ""))) + SetVal(lblSyouhizei.Text)))

            Else

                If Not tenbetuSyokiseikyu(0).Iszei_kbnNull Then
                    Me.hidZeikbnTxt.Value = tenbetuSyokiseikyu(0).zei_kbn
                    Dim syouhizei As String
                    syouhizei = (KihonJyouhouInquiryBc.GetKakaku(Me.hidZeikbnTxt.Value))
                    If syouhizei = "0" Then
                        '消費税にをセットする
                        Me.lblSyouhizei.Text = SetKingaku(syouhizei)
                    Else
                        '消費税区分から税率を求める
                        Me.lblSyouhizei.Text = SetKingaku((Fix(SetVal(SetDouble(Me.lblSyouhizei.Text.Replace(",", "")) * SetVal(syouhizei * 100) / 100))))
                        '消費税区分から税率を求める
                    End If
                End If
                '税込金額を算出する
                Me.lblZeikomi.Text = SetKingaku((SetVal(SetDouble(tbxZeinuki.Text.Replace(",", ""))) + SetVal(lblSyouhizei.Text)))
            End If


            '工務店請求
            If Not tenbetuSyokiseikyu(0).Iskoumuten_seikyuu_gakuNull Then
                Me.tbxKoumutenSeikyuuGaku.Text = SetKingaku((tenbetuSyokiseikyu(0).koumuten_seikyuu_gaku), False)
            End If

            '請求書発行日
            If Not tenbetuSyokiseikyu(0).Isseikyuusyo_hak_dateNull Then
                Me.tbxSeikyuDate.Text = Convert.ToDateTime(tenbetuSyokiseikyu(0).seikyuusyo_hak_date).ToString(YMD)
            End If

            '売上年月日
            If Not tenbetuSyokiseikyu(0).Isuri_dateNull Then
                Me.tbxUriDate.Text = Convert.ToDateTime(tenbetuSyokiseikyu(0).uri_date).ToString(YMD)
            End If

            If Not tenbetuSyokiseikyu(0).IsbikouNull Then
                Me.tbxBikou.Text = tenbetuSyokiseikyu(0).bikou
            End If

            'If Not kameitenTableDataTable(0).Ishansokuhin_seikyuusakiNull Then
            '    If kameitenTableDataTable(0).hansokuhin_seikyuusaki = _MiseCode Then
            '        lblSeikyusaki.Text = "直接請求"
            '    Else
            '        lblSeikyusaki.Text = "他請求"
            '    End If
            'Else

            'End If

            If Not kameitenTableDataTable(0).Isupd_datetimeNull Then
                Me.hidUpdTime.Value = Convert.ToDateTime(tenbetuSyokiseikyu(0).upd_datetime).ToString("yyyy/MM/dd HH:mm:ss")
            End If
            If Not blnFirst Then
                SetKingaku()
            End If
            If tenbetuSyokiseikyu(0).Isuri_keijyou_flgNull OrElse tenbetuSyokiseikyu(0).uri_keijyou_flg <> "1" Then
                Me.lblUriageKeijou.Text = ""

                If tenbetuSyokiseikyu(0).Isseikyuu_umuNull OrElse tenbetuSyokiseikyu(0).seikyuu_umu <> 1 Then

                    LockItemTextbox(Me.tbxZeinuki)
                    LockItemTextbox(Me.tbxSeikyuDate)
                    LockItemTextbox(Me.tbxKoumutenSeikyuuGaku)


                Else
                    '工務店請求金額の状態
                    If (hidSeikyusaki.Value = "直" Or _keiretuCd <> "THTH" Or _
                                _keiretuCd <> "0001" Or _keiretuCd <> "NF03") Then

                        UnLockItemTextbox(tbxKoumutenSeikyuuGaku)
                    Else
                        LockItemTextbox(tbxKoumutenSeikyuuGaku)
                    End If

                End If

            Else

                Me.lblUriageKeijou.Text = "売上処理済"
                LockItemTextbox(Me.tbxAddDate)
                LockItemdrp(Me.ddlSeikyuuUmu)
                LockItemTextbox(Me.tbxSyouhinCd)
                LockItemTextbox(Me.tbxZeinuki)
                LockItemTextbox(Me.tbxKoumutenSeikyuuGaku)
                LockItemTextbox(Me.tbxSeikyuDate)

                Me.btnKansaku.Enabled = False

            End If

        Else

            If (hidSeikyusaki.Value = "他" AndAlso _keiretuCd <> "THTH" AndAlso _
            _keiretuCd <> "0001" AndAlso _keiretuCd <> "NF03") Then
                LockItemTextbox(tbxKoumutenSeikyuuGaku)
            Else
                UnLockItemTextbox(tbxKoumutenSeikyuuGaku)
            End If

            ClearArea()

        End If




        If _Kbn = "A" Then



            '販促品初期ツール料
            tenbetuSyokiseikyu = KihonJyouhouInquiryBc.GetTenbetuSyokiSeikyu(_kameiten_cd, "210")
            If tenbetuSyokiseikyu.Rows.Count > 0 Then


                If Not tenbetuSyokiseikyu(0).Isadd_dateNull Then
                    Me.tbxAddDate1.Text = Convert.ToDateTime(tenbetuSyokiseikyu(0).add_date).ToString(YMD)
                End If

                '請求有無
                If Not tenbetuSyokiseikyu(0).Isseikyuu_umuNull Then
                    If tenbetuSyokiseikyu(0).seikyuu_umu = 1 Then
                        Me.ddlSeikyuuUmu1.SelectedIndex = 1
                    ElseIf tenbetuSyokiseikyu(0).seikyuu_umu = 0 Then
                        Me.ddlSeikyuuUmu1.SelectedIndex = 0
                    Else
                        Me.ddlSeikyuuUmu1.SelectedIndex = 1
                    End If
                Else
                    Me.ddlSeikyuuUmu1.SelectedIndex = 1
                End If

                '商品CD　名
                If Not tenbetuSyokiseikyu(0).Issyouhin_cdNull Then

                    Me.tbxSyouhinCd1.Text = tenbetuSyokiseikyu(0).syouhin_cd
                    Dim syouhinDataTable As KameitenjyushoDataSet.m_syouhinDataTable

                    syouhinDataTable = KihonJyouhouInquiryBc.GetSyouhin(tenbetuSyokiseikyu(0).syouhin_cd)
                    If syouhinDataTable.Rows.Count > 0 Then
                        If Not syouhinDataTable(0).Issyouhin_meiNull Then
                            Me.lblSyouhinMei1.Text = syouhinDataTable(0).syouhin_mei
                            Me.tbxSyouhinCd1.Text = syouhinDataTable(0).syouhin_cd
                        End If

                        If Not syouhinDataTable(0).Ishyoujun_kkkNull Then
                            Me.hidHyoujunkakaku1.Value = syouhinDataTable(0).hyoujun_kkk
                        Else
                            Me.hidHyoujunkakaku1.Value = "0"
                        End If

                        If Not syouhinDataTable(0).Iszei_kbnNull Then
                            Me.hidZeikbn1.Value = syouhinDataTable(0).zei_kbn
                        Else
                            Me.hidZeikbn1.Value = ""
                        End If
                    End If
                End If

                '実請求税抜金額
                If Not tenbetuSyokiseikyu(0).Isuri_gakuNull Then
                    Me.tbxZeinuki1.Text = SetKingaku(tenbetuSyokiseikyu(0).uri_gaku, False)
                End If

                If blnFirst Then
                    If Not tenbetuSyokiseikyu(0).Iszei_kbnNull Then
                        Me.hidZeikbnTxt1.Value = tenbetuSyokiseikyu(0).zei_kbn

                    End If
                    If tenbetuSyokiseikyu(0).syouhizei_gaku = "0" Then
                        '消費税にをセットする

                        Me.lblSyouhizei1.Text = 0
                    Else
                        '消費税区分から税率を求める
                        Me.lblSyouhizei1.Text = SetKingaku(tenbetuSyokiseikyu(0).syouhizei_gaku)

                        '消費税区分から税率を求める
                    End If
                    Me.lblZeikomi1.Text = SetKingaku((SetVal(SetDouble(tbxZeinuki1.Text.Replace(",", ""))) + SetVal(lblSyouhizei1.Text)))

                Else
                    '消費税
                    If Not tenbetuSyokiseikyu(0).Iszei_kbnNull Then

                        Me.hidZeikbnTxt1.Value = tenbetuSyokiseikyu(0).zei_kbn
                        Dim syouhizei As String
                        syouhizei = (KihonJyouhouInquiryBc.GetKakaku(Me.hidZeikbnTxt1.Value))
                        If syouhizei = "0" Then
                            '消費税にをセットする
                            Me.lblSyouhizei1.Text = SetKingaku(syouhizei)

                        Else
                            '消費税区分から税率を求める
                            Me.lblSyouhizei1.Text = SetKingaku((Fix(SetVal(SetDouble(Me.lblSyouhizei1.Text.Replace(",", ""))) * SetVal(syouhizei * 100) / 100)))
                            '消費税区分から税率を求める
                        End If
                    End If

                    '税込金額を算出する
                    Me.lblZeikomi1.Text = SetKingaku((SetVal(SetDouble(tbxZeinuki1.Text.Replace(",", ""))) + SetVal(lblSyouhizei1.Text)))
                End If


                '請求書発行日
                If Not tenbetuSyokiseikyu(0).Isseikyuusyo_hak_dateNull Then
                    Me.tbxSeikyuDate1.Text = Convert.ToDateTime(tenbetuSyokiseikyu(0).seikyuusyo_hak_date).ToString(YMD)
                End If

                '売上年月日
                If Not tenbetuSyokiseikyu(0).Isuri_dateNull Then
                    Me.tbxUriDate1.Text = Convert.ToDateTime(tenbetuSyokiseikyu(0).uri_date).ToString(YMD)
                End If

                If Not tenbetuSyokiseikyu(0).IsbikouNull Then
                    Me.tbxBikou1.Text = tenbetuSyokiseikyu(0).bikou
                End If

                If Not kameitenTableDataTable(0).Isupd_datetimeNull Then
                    Me.hidUpdTime1.Value = Convert.ToDateTime(tenbetuSyokiseikyu(0).upd_datetime).ToString("yyyy/MM/dd HH:mm:ss")
                End If

                If Not blnFirst Then
                    SetKingaku1()
                End If
                If tenbetuSyokiseikyu(0).Isuri_keijyou_flgNull OrElse tenbetuSyokiseikyu(0).uri_keijyou_flg <> "1" Then

                    Me.lblUriageKeijou1.Text = ""
                    If tenbetuSyokiseikyu(0).Isseikyuu_umuNull OrElse tenbetuSyokiseikyu(0).seikyuu_umu <> 1 Then
                        LockItemTextbox(Me.tbxZeinuki1)
                        LockItemTextbox(Me.tbxSeikyuDate1)

                    Else

                    End If

                Else
                    Me.lblUriageKeijou1.Text = "売上処理済"
                    LockItemTextbox(Me.tbxAddDate1)
                    LockItemdrp(Me.ddlSeikyuuUmu1)
                    LockItemTextbox(Me.tbxSyouhinCd1)
                    LockItemTextbox(Me.tbxZeinuki1)
                    LockItemTextbox(Me.tbxSeikyuDate1)
                    Me.btnKansaku1.Enabled = False
                End If

            Else
                ClearArea1()
            End If

        Else
            '項目を入力不可にする
            LockItemTextbox(Me.tbxAddDate1)
            LockItemdrp(Me.ddlSeikyuuUmu1)
            LockItemTextbox(Me.tbxSyouhinCd1)
            LockItemTextbox(Me.tbxZeinuki1)
            LockItemTextbox(Me.tbxSeikyuDate1)
            LockItemTextbox(Me.tbxBikou1)
            Me.btnKansaku1.Enabled = False
        End If

        hidAutoKoumuFlg.Value = "False"
        hidAutoJituFlg.Value = "False"
    End Sub

    ''' <summary>
    ''' 登録権限の設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Setkenngen()

        '登録権限
        If _kenngenn = True Then
            Me.btnTouroku.Enabled = True
        Else
            Me.btnTouroku.Enabled = False
        End If

    End Sub

    ''' <summary>
    ''' 入力チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function chkInputValue() As Boolean

        '登録料の入力チェック
        '必須入力チェック
        '登録日
        Dim commonCheck As New CommonCheck

        If commonCheck.CheckYuukouHiduke(Me.tbxAddDate.Text, "登録日") <> String.Empty Then
            msgAndFocus.AppendMsgAndCtrl(Me.tbxAddDate, commonCheck.CheckYuukouHiduke(Me.tbxAddDate.Text, "登録日"))

        End If

        If Me.tbxAddDate.Text = String.Empty Then
            msgAndFocus.AppendMsgAndCtrl(Me.tbxAddDate, Messages.Instance.MSG013E, "登録日")
        End If


        If Me.tbxSyouhinCd.Text = String.Empty Then
            msgAndFocus.AppendMsgAndCtrl(Me.tbxSyouhinCd, Messages.Instance.MSG013E, "商品コード")
        End If


        '日付チェック
        '登録日
        If Me.tbxAddDate.Text <> String.Empty Then
            If IsDate(Me.tbxAddDate.Text) = False Then
                msgAndFocus.AppendMsgAndCtrl(Me.tbxAddDate, "登録日は日付以外が入力されています。\r\n")
            End If
        End If

        '請求書発行日
        If Me.tbxSeikyuDate.Text <> String.Empty Then
            If IsDate(Me.tbxSeikyuDate.Text) = False Then
                msgAndFocus.AppendMsgAndCtrl(Me.tbxSeikyuDate, "請求書発行日は日付以外が入力されています。\r\n")
            End If
        End If



        '桁数チェック
        '税抜金額
        If Me.tbxZeinuki.Text <> String.Empty Then
            If Me.tbxZeinuki.Text.Replace(",", "").Length > 8 Then
                msgAndFocus.AppendMsgAndCtrl(Me.tbxZeinuki, Messages.Instance.MSG2021E, "実請求税抜金額")
            End If
        End If

        If Me.tbxKoumutenSeikyuuGaku.Text <> String.Empty Then
            If Me.tbxKoumutenSeikyuuGaku.Text.Replace(",", "").Length > 8 Then
                msgAndFocus.AppendMsgAndCtrl(Me.tbxKoumutenSeikyuuGaku, Messages.Instance.MSG2021E, "工務店請求税抜金額")
            End If
        End If


        '備考
        If Not chkKetaSuu(Me.tbxBikou, Me.tbxBikou.Text, "備考", 30, 2) Then
            '終了処理
            'Return False
        End If

        Dim chkobj As New CommonCheck


        If chkobj.CheckKinsoku(Me.tbxBikou.Text, "備考") <> String.Empty Then
            msgAndFocus.AppendMsgAndCtrl(Me.tbxBikou, chkobj.CheckKinsoku(Me.tbxBikou.Text, "備考"))
            '終了処理
        End If


        If _Kbn = "A" Then
            '登録料の入力チェック
            '必須入力チェック
            '登録日
            If commonCheck.CheckYuukouHiduke(Me.tbxAddDate1.Text, "配送日") <> String.Empty Then
                msgAndFocus.AppendMsgAndCtrl(Me.tbxAddDate1, commonCheck.CheckYuukouHiduke(Me.tbxAddDate1.Text, "配送日"))

            End If

            If Me.tbxAddDate1.Text = String.Empty Then
                msgAndFocus.AppendMsgAndCtrl(Me.tbxAddDate1, Messages.Instance.MSG013E, "配送日")
            End If

            If Me.tbxSyouhinCd1.Text = String.Empty Then
                msgAndFocus.AppendMsgAndCtrl(Me.tbxSyouhinCd1, Messages.Instance.MSG013E, "商品コード")
            End If

            '日付チェック
            '配送日
            If Me.tbxAddDate1.Text <> String.Empty Then
                If IsDate(Me.tbxAddDate1.Text) = False Then
                    msgAndFocus.AppendMsgAndCtrl(Me.tbxAddDate1, "配送日は日付以外が入力されています。\r\n")
                End If
            End If

            '請求書発行日
            If Me.tbxSeikyuDate1.Text <> String.Empty Then
                If IsDate(Me.tbxSeikyuDate1.Text) = False Then
                    msgAndFocus.AppendMsgAndCtrl(Me.tbxSeikyuDate1, "請求書発行日は日付以外が入力されています。\r\n")
                End If
            End If

            '桁数チェック
            '税抜金額
            If Me.tbxZeinuki1.Text <> String.Empty Then
                If Me.tbxZeinuki1.Text.Replace(",", "").Length > 8 Then
                    msgAndFocus.AppendMsgAndCtrl(Me.tbxZeinuki1, Messages.Instance.MSG2021E, "実請求税抜金額")
                End If
            End If

            '備考
            If Not chkKetaSuu(Me.tbxBikou1, Me.tbxBikou1.Text, "備考", 30, 2) Then

            End If

            If chkobj.CheckKinsoku(Me.tbxBikou1.Text, "備考") <> String.Empty Then
                msgAndFocus.AppendMsgAndCtrl(Me.tbxBikou1, chkobj.CheckKinsoku(Me.tbxBikou1.Text, "備考"))
                '終了処理
            End If

        End If
        ''メッセージ表示
        If msgAndFocus.Message <> String.Empty Then
            ShowMsg(msgAndFocus.Message, msgAndFocus.focusCtrl)
            Return False
        Else
            Return True
        End If
    End Function

    ''' <summary>
    ''' 請求入力項目を再設定する
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub OperateSeikyuData()

        hidAutoKoumuFlg.Value = "False"
        hidAutoJituFlg.Value = "False"

        If Me.ddlSeikyuuUmu.SelectedValue = "1" Then

            '商品コードの未入力チェック
            If Me.tbxSyouhinCd.Text.Trim = String.Empty Then
                Exit Sub
            End If

            If KihonJyouhouInquiryBc.GetkeiretuCd(_kameiten_cd) <> _keiretuCd Then
                Me.btnTouroku.Enabled = False
                ShowMsg("他のユーザーに更新されています、画面をもう一度取り込んでください。", Me.btnTouroku)
                Exit Sub
            End If

            Dim syouhinDataTable As KameitenjyushoDataSet.m_syouhinDataTable
            syouhinDataTable = KihonJyouhouInquiryBc.GetSyouhin(Me.tbxSyouhinCd.Text.Trim)

            If Not syouhinDataTable(0).Iszei_kbnNull Then
                Me.hidZeikbn.Value = syouhinDataTable(0).zei_kbn
            Else
                Me.hidZeikbn.Value = ""
            End If


            ' 税抜金額を入力可能にする
            UnLockItemTextbox(tbxZeinuki)

            '登録料で、直接請求接請求また系列の場合工務店請求金額を入力可能にする
            If (hidSeikyusaki.Value = "直" Or _keiretuCd = "THTH" Or _
                        _keiretuCd = "0001" Or _keiretuCd = "NF03") Then

                UnLockItemTextbox(tbxKoumutenSeikyuuGaku)
            End If

            '請求書発行日を入力可能にする
            UnLockItemTextbox(tbxSeikyuDate)


            koumuten = SetDouble(hidHyoujunkakaku.Value)
            jitu = SetDouble(hidHyoujunkakaku.Value)

            If GetMiseSeikyuu("A", Me.hidSeikyusaki.Value, _keiretuCd, _
                                            Me.tbxSyouhinCd.Text) = "True" Then

                Me.tbxZeinuki.Text = SetKingaku(jitu, False)
                Me.tbxKoumutenSeikyuuGaku.Text = SetKingaku(koumuten, False)
            End If

            ' 税区分を取得する
            Me.hidZeikbnTxt.Value = Me.hidZeikbn.Value

            '金額を再計算する
            SetKingaku()


            '登録日の入力チェック

            If Me.tbxAddDate.Text <> String.Empty Then

                '請求書発行日、売上年月日を設定する
                SetDate(True)

            End If

            Me.tbxSyouhinCd.Focus()

        Else
            '登録料
            Dim syouhinDataTable As KameitenjyushoDataSet.m_syouhinDataTable

            If _Kbn = "A" Then

                'FC店
                '商品コード="J0099"をセット
                Me.tbxSyouhinCd.Text = "J0099"
            Else
                'FC店以外
                '商品コード="C0099"をセット
                Me.tbxSyouhinCd.Text = "C0099"

            End If

            syouhinDataTable = KihonJyouhouInquiryBc.GetSyouhin(Me.tbxSyouhinCd.Text)
            If syouhinDataTable.Rows.Count > 0 Then

                If Not syouhinDataTable(0).Issyouhin_meiNull Then
                    Me.lblSyouhinMei.Text = syouhinDataTable(0).syouhin_mei
                    Me.tbxSyouhinCd.Text = syouhinDataTable(0).syouhin_cd
                End If

                '標準価格
                If Not syouhinDataTable(0).Ishyoujun_kkkNull Then
                    tbxZeinuki.Text = SetKingaku(syouhinDataTable(0).hyoujun_kkk, False)
                Else
                    tbxZeinuki.Text = String.Empty
                End If

                '税区分
                If Not syouhinDataTable(0).Iszei_kbnNull Then
                    Me.hidZeikbnTxt.Value = syouhinDataTable(0).zei_kbn
                Else
                    hidZeikbnTxt.Value = String.Empty
                End If

            Else
                tbxZeinuki.Text = String.Empty
                Me.hidZeikbn.Value = String.Empty
            End If

            '金額を算出する
            SetKingaku()

            '税抜金額を入力不可にする
            LockItemTextbox(tbxZeinuki)

            '工務店請求金額を設定する
            tbxKoumutenSeikyuuGaku.Text = tbxZeinuki.Text

            '工務店請求金額を入力不可にする
            LockItemTextbox(tbxKoumutenSeikyuuGaku)

            '請求書発行日を初期化する
            tbxSeikyuDate.Text = String.Empty

            '売上年月日の更新(システム日付)
            Me.tbxUriDate.Text = Convert.ToDateTime(KihonJyouhouInquiryBc.GetSysDate).ToString(YMD)

            '請求書発行日を入力不可にする
            LockItemTextbox(tbxSeikyuDate)

            '備考にフォーカスをセットする
            msgAndFocus.setFocus(Me.Page, tbxBikou)

        End If

    End Sub

    ''' <summary>
    ''' 請求入力項目を再設定する
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub OperateSeikyuData1()

        If Me.ddlSeikyuuUmu1.SelectedValue = "1" Then

            '商品コードの未入力チェック
            If Me.tbxSyouhinCd1.Text.Trim = String.Empty Then
                Exit Sub
            End If
            Dim syouhinDataTable As KameitenjyushoDataSet.m_syouhinDataTable
            syouhinDataTable = KihonJyouhouInquiryBc.GetSyouhin(Me.tbxSyouhinCd1.Text.Trim)

            If Not syouhinDataTable(0).Iszei_kbnNull Then
                Me.hidZeikbn1.Value = syouhinDataTable(0).zei_kbn
            Else
                Me.hidZeikbn1.Value = ""
            End If

            ' 税抜金額を入力可能にする
            UnLockItemTextbox(tbxZeinuki1)
            '請求書発行日を入力可能にする
            UnLockItemTextbox(tbxSeikyuDate1)
            '初期販促の場合、実請求金額に標準価格を設定
            tbxZeinuki1.Text = SetKingaku(hidHyoujunkakaku1.Value, False)

            ' 税区分を取得する
            Me.hidZeikbnTxt1.Value = Me.hidZeikbn1.Value

            '金額を再計算する
            SetKingaku1()


            '登録日の入力チェック

            If Me.tbxAddDate1.Text <> String.Empty Then

                '請求書発行日、売上年月日を設定する
                SetDate1(True)

            End If

            Me.tbxSyouhinCd1.Focus()

        Else
            '登録料
            Dim syouhinDataTable As KameitenjyushoDataSet.m_syouhinDataTable

            Me.tbxSyouhinCd1.Text = "K0099"
            syouhinDataTable = KihonJyouhouInquiryBc.GetSyouhin("K0099")
            If Not syouhinDataTable(0).Issyouhin_meiNull Then
                Me.lblSyouhinMei1.Text = syouhinDataTable(0).syouhin_mei
                Me.tbxSyouhinCd1.Text = syouhinDataTable(0).syouhin_cd
            End If


            '標準価格
            If Not syouhinDataTable(0).Ishyoujun_kkkNull Then
                tbxZeinuki1.Text = SetKingaku(syouhinDataTable(0).hyoujun_kkk, False)
            Else
                tbxZeinuki1.Text = String.Empty
            End If

            '税区分
            If Not syouhinDataTable(0).Iszei_kbnNull Then
                Me.hidZeikbnTxt1.Value = syouhinDataTable(0).zei_kbn
            Else
                hidZeikbnTxt1.Value = String.Empty
            End If

            '金額を算出する
            SetKingaku1()

            '税抜金額を入力不可にする
            LockItemTextbox(tbxZeinuki1)


            '請求書発行日を初期化する
            tbxSeikyuDate1.Text = String.Empty

            '売上年月日の更新(システム日付)
            Me.tbxUriDate1.Text = Convert.ToDateTime(KihonJyouhouInquiryBc.GetSysDate).ToString(YMD)

            '請求書発行日を入力不可にする
            LockItemTextbox(tbxSeikyuDate1)

            '備考にフォーカスをセットする
            Me.tbxBikou1.Focus()


        End If

    End Sub

    ''' <summary>
    '''  商品コードの変更により各項目を再設定する
    ''' </summary>
    ''' <remarks></remarks>
    Private Function UpdShouhinCd() As Boolean

        hidAutoKoumuFlg.Value = "False"
        hidAutoJituFlg.Value = "False"

        '未入力チェック
        If Me.tbxSyouhinCd.Text = String.Empty Then
            Me.lblSyouhinMei.Text = String.Empty
            Return True
        End If

        If KihonJyouhouInquiryBc.GetkeiretuCd(_kameiten_cd) <> _keiretuCd Then
            Me.btnTouroku.Enabled = False
            ShowMsg("他のユーザーに更新されています、画面をもう一度取り込んでください。", Me.btnTouroku)
            Return False
        End If

        Dim syouhin As KameitenjyushoDataSet.m_syouhinDataTable
        syouhin = KihonJyouhouInquiryBc.GetSyouhin(Me.tbxSyouhinCd.Text, "200")

        If syouhin.Rows.Count > 0 Then
            '税区分を取得する
            If Not syouhin(0).Iszei_kbnNull Then
                Me.hidZeikbn.Value = syouhin(0).zei_kbn
            End If
            '商品名
            Me.lblSyouhinMei.Text = syouhin(0).syouhin_mei
            Me.tbxSyouhinCd.Text = syouhin(0).syouhin_cd
            If Not syouhin(0).Ishyoujun_kkkNull Then
                Me.hidHyoujunkakaku.Value = syouhin(0).hyoujun_kkk
            Else
                Me.hidHyoujunkakaku.Value = "0"
            End If

            If Not syouhin(0).Iszei_kbnNull Then
                Me.hidZeikbnTxt.Value = syouhin(0).zei_kbn
            Else
                Me.hidZeikbnTxt.Value = ""
            End If

        Else
            Me.hidLastFocus.Value = "2"
            ShowMsg(Messages.Instance.MSG2008E, Me.tbxSyouhinCd, "商品コード")
            Me.tbxSyouhinCd.Text = String.Empty
            Me.lblSyouhinMei.Text = String.Empty
            Return False
        End If
        Me.hidLastFocus.Value = String.Empty

        '請求有無の判定
        If Me.ddlSeikyuuUmu.SelectedValue = "1" Then
            '請求有り
            '登録料の場合、自動設定
            koumuten = hidHyoujunkakaku.Value
            jitu = hidHyoujunkakaku.Value

            If GetMiseSeikyuu("A", Me.hidSeikyusaki.Value, _keiretuCd, _
                                            Me.tbxSyouhinCd.Text) = "True" Then
                Me.tbxZeinuki.Text = SetKingaku(jitu, False)
                Me.tbxKoumutenSeikyuuGaku.Text = SetKingaku(koumuten, False)
            End If

            '金額を再計算する
            Call SetKingaku()

            '登録日の入力チェック
            If Me.tbxAddDate.Text <> String.Empty Then
                '請求書発行日、売上年月日を設定する
                SetDate(False)
            End If
        End If
        Return True
    End Function

    ''' <summary>
    '''  商品コードの変更により各項目を再設定する
    ''' </summary>
    ''' <remarks></remarks>
    Private Function UpdShouhinCd1() As Boolean
        '未入力チェック
        If Me.tbxSyouhinCd1.Text = String.Empty Then
            Me.lblSyouhinMei1.Text = String.Empty
            Return True
        End If

        Dim syouhin As KameitenjyushoDataSet.m_syouhinDataTable
        syouhin = KihonJyouhouInquiryBc.GetSyouhin(Me.tbxSyouhinCd1.Text, "210")

        If syouhin.Rows.Count > 0 Then
            '税区分を取得する
            If Not syouhin(0).Iszei_kbnNull Then
                Me.hidZeikbn1.Value = syouhin(0).zei_kbn
            End If
            '商品名
            Me.lblSyouhinMei1.Text = syouhin(0).syouhin_mei
            Me.tbxSyouhinCd1.Text = syouhin(0).syouhin_cd
            If Not syouhin(0).Ishyoujun_kkkNull Then
                Me.hidHyoujunkakaku1.Value = syouhin(0).hyoujun_kkk
            Else
                Me.hidHyoujunkakaku1.Value = "0"
            End If

            If Not syouhin(0).Iszei_kbnNull Then
                Me.hidZeikbnTxt1.Value = syouhin(0).zei_kbn
            Else
                Me.hidZeikbnTxt1.Value = ""
            End If
        Else
            Me.hidLastFocus.Value = "7"
            ShowMsg(Messages.Instance.MSG2008E, Me.tbxSyouhinCd1, "商品コード")
            Me.tbxSyouhinCd1.Text = String.Empty
            Me.lblSyouhinMei1.Text = String.Empty
            Return False
        End If
        Me.hidLastFocus.Value = String.Empty

        '請求有無の判定
        If Me.ddlSeikyuuUmu1.SelectedValue = "1" Then

            '初期販促の場合、実請求金額に標準価格を設定
            tbxZeinuki1.Text = SetKingaku(hidHyoujunkakaku1.Value, False)

            '金額を再計算する
            Call SetKingaku1()

            '登録日の入力チェック
            If Me.tbxAddDate1.Text <> String.Empty Then

                '請求書発行日、売上年月日を設定する
                SetDate1(False)

            End If
        End If
        Return True
    End Function

    ''' <summary>
    ''' 登録料
    ''' 実請求
    ''' </summary>
    ''' <remarks></remarks>
    Private Function Zeinuki() As Boolean

        koumuten = 0
        jitu = 0
        '商品ｺｰﾄﾞが未入力の場合は処理なし

        If Me.tbxSyouhinCd.Text.Trim = String.Empty Then
            Return False
        End If

        If KihonJyouhouInquiryBc.GetkeiretuCd(_kameiten_cd) <> _keiretuCd Then
            Me.btnTouroku.Enabled = False
            ShowMsg("他のユーザーに更新されています、画面をもう一度取り込んでください。", Me.btnTouroku)
            Return False
        End If

        '実請求金額がlong型の制限を越えていた場合は
        '工務店請求自動設定は行なわない（消費税・税込金額計算のみ）
        If SetDouble(tbxZeinuki.Text.Trim) > SetDouble("2147483647") Then
            '金額を再計算する
            SetKingaku()
            Return False
        End If

        If hidSeikyusaki.Value = "直" Or hidAutoJituFlg.Value = "False" Then

            '直接請求接請求もしくは実請求自動設定未実施の場合、自動設定
            jitu = IIf(Me.tbxZeinuki.Text = String.Empty, 0, SetDouble(SetKingaku(Me.tbxZeinuki.Text, False)))

            If GetMiseSeikyuu("B", Me.hidSeikyusaki.Value, _keiretuCd, _
                                           Me.tbxSyouhinCd.Text) = "True" Then

                '取得した金額を工務店請求金額に設定
                Me.tbxKoumutenSeikyuuGaku.Text = SetKingaku(koumuten, False)
                '工務店請求金額自動設定フラグＯＮ
                hidAutoKoumuFlg.Value = "True"
            End If
        End If

        '金額を再計算する
        Call SetKingaku()
        Return True
    End Function

    ''' <summary>
    ''' 販促品初期ツール料
    ''' 実請求
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private Function Zeinuki1() As Boolean

        koumuten = 0
        jitu = 0

        '金額を再計算する
        Call SetKingaku1()
        Return True
    End Function

    ''' <summary>
    ''' 工務店請求設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetKoumutenSeikyuu() As Boolean

        koumuten = 0
        jitu = 0

        '商品ｺｰﾄﾞが未入力の場合は処理なし
        If Me.tbxSyouhinCd.Text.Trim = String.Empty Then
            Return False
        End If

        If KihonJyouhouInquiryBc.GetkeiretuCd(_kameiten_cd) <> _keiretuCd Then
            Me.btnTouroku.Enabled = False
            ShowMsg("他のユーザーに更新されています、画面をもう一度取り込んでください。", Me.btnTouroku)
            Return False
        End If


        '工務店請求金額がlong型の制限を越えていた場合処理を中断（エラー回避）
        If SetDouble(Me.tbxKoumutenSeikyuuGaku.Text.Trim) > SetDouble("2147483647") Then
            Return False
        End If

        If hidSeikyusaki.Value = "直" Or hidAutoJituFlg.Value = "False" Then
            '直接請求接請求もしくは工務店請求自動設定未実施の場合、自動設定

            koumuten = IIf(Me.tbxKoumutenSeikyuuGaku.Text = String.Empty, 0, SetDouble(SetKingaku(Me.tbxKoumutenSeikyuuGaku.Text, False)))
            If GetMiseSeikyuu("C", Me.hidSeikyusaki.Value, _keiretuCd, _
                                                   Me.tbxSyouhinCd.Text) = "True" Then

                '取得した金額を実請求金額に設定
                tbxZeinuki.Text = SetKingaku(jitu, False)
                '実請求金額自動設定フラグＯＮ
                hidAutoJituFlg.Value = "True"
            End If
        End If

        '金額を再計算する
        Call SetKingaku()
        Return True
    End Function

    ''' <summary>
    ''' 請求書発行日、売上年月日を設定する
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetDate(ByVal bln As Boolean)

        Dim simeDate As String

        '請求書発行日の設定(未入力の場合)
        If tbxSeikyuDate.Text.Trim = String.Empty Then

            simeDate = KihonJyouhouInquiryBc.GetSimeDate(_kameiten_cd, SEIKYUUSIMEDATE)
            Dim datenow As Date
            datenow = Convert.ToDateTime(KihonJyouhouInquiryBc.GetSysDate)

            If simeDate = String.Empty Then
                simeDate = Format(DateSerial(datenow.Year.ToString, datenow.Month + 1, 0), "dd")
            End If

            '請求書発行日を求める
            simeDate = GetHkkouYMD(simeDate)

            ''請求書発行日を表示する
            tbxSeikyuDate.Text = simeDate
        End If

        '売上年月日未入力の場合売上年月日の設定
        If bln = True OrElse tbxUriDate.Text.Trim = String.Empty Then
            tbxUriDate.Text = Convert.ToDateTime(KihonJyouhouInquiryBc.GetSysDate).ToString(YMD)
        End If

    End Sub

    ''' <summary>
    ''' 販促品初期ツール料
    ''' 請求書発行日、売上年月日を設定する
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetDate1(ByVal bln As Boolean)

        Dim simeDate As String

        '請求書発行日の設定(未入力の場合)
        If tbxSeikyuDate1.Text.Trim = String.Empty Then

            simeDate = KihonJyouhouInquiryBc.GetSimeDate(_kameiten_cd, HANSOHUHINSEIKYUUSIMEDATE)

            Dim datenow As Date
            datenow = Convert.ToDateTime(KihonJyouhouInquiryBc.GetSysDate)

            If simeDate = String.Empty Then
                simeDate = Format(DateSerial(datenow.Year.ToString, datenow.Month + 1, 0), "dd")
            End If

            '請求書発行日を求める
            simeDate = GetHkkouYMD(simeDate)

            ''請求書発行日を表示する
            tbxSeikyuDate1.Text = simeDate
        End If

        '売上年月日未入力の場合売上年月日の設定
        If bln = True OrElse tbxUriDate1.Text.Trim = String.Empty Then
            tbxUriDate1.Text = Convert.ToDateTime(KihonJyouhouInquiryBc.GetSysDate).ToString(YMD)
        End If

    End Sub

    ''' <summary>
    '''請求書発行日を求める
    ''' </summary>
    ''' <param name="dayString">請求日</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetHkkouYMD(ByVal dayString As String) As String
        Dim strEditYMD As String
        Dim strDD As String
        Dim sysDateYMD As Date

        sysDateYMD = Convert.ToDateTime(KihonJyouhouInquiryBc.GetSysDate)

        strDD = Right$("00" & dayString, 2)

        strEditYMD = sysDateYMD.ToString(YM) & "/" & strDD

        If IsDate(strEditYMD) = False Then

            strEditYMD = Format$( _
                             DateAdd(SIGNDAY, _
                                    -1, _
                                    DateAdd(SIGNMONTH, 1, CDate(Format$(sysDateYMD, YM) & "/01"))), YMD)
        End If

        '求めた請求書発行日＜システム日付の場合、翌月の締日を再設定する
        If YMDCheck(CDate(strEditYMD), CDate(KihonJyouhouInquiryBc.GetSysDate)) = -1 Then

            strEditYMD = Format$(DateAdd(SIGNMONTH, 1, CDate(strEditYMD)), YM) & "/" & strDD

            If IsDate(strEditYMD) = False Then
                strEditYMD = Format$( _
                                   DateAdd(SIGNDAY, _
                                          -1, _
                                          DateAdd(SIGNMONTH _
                                                 , 2 _
                                                 , CDate(Format$(sysDateYMD, YM) & "/01"))), YMD)
            End If
        End If
        Return strEditYMD

    End Function




    Private koumuten As Long
    Private jitu As Long
    ''' <summary>
    ''' LOCK
    ''' </summary>
    ''' <param name="control"></param>
    ''' <remarks></remarks>
    Private Sub LockItemTextbox(ByVal control As TextBox)
        control.Attributes.Add("readonly", "true")
        'control.Enabled = False
        control.Style.Add("background-color", "silver")
    End Sub

    ''' <summary>
    ''' UNLOCK
    ''' 
    ''' </summary>
    ''' <param name="control"></param>
    ''' <remarks></remarks>
    Private Sub UnLockItemTextbox(ByVal control As TextBox)
        '   control.Enabled = True
        control.Attributes.Remove("readonly")
        control.Style.Item("background-color") = ("white")
    End Sub

    ''' <summary>
    ''' LOCKDRP
    ''' </summary>
    ''' <param name="control"></param>
    ''' <remarks></remarks>
    Private Sub LockItemdrp(ByVal control As DropDownList)
        ' control.Enabled = False
        'control.Attributes.Add("disabled", "true")
        'control.Style.Add("background-color", "silver")




        'control.Attributes.Item("onfocus") = "this.defaultIndex=this.selectedIndex;"
        'control.Attributes.Item("onchange") = "this.selectedIndex=this.defaultIndex;"

        'control.CssClass = "readOnly"
        'control.Style.Item("background-color") = "#D0D0D0"
        CommonKassei.SetDropdownListReadonly(control)

    End Sub

    ''' <summary>
    ''' UNLOCKDRP
    ''' </summary>
    ''' <param name="control"></param>
    ''' <remarks></remarks>
    Private Sub UnLockItemdrp(ByVal control As DropDownList)
        '   control.Enabled = True
        'control.Attributes.Remove("disabled")
        'control.Style.Item("background-color") = ("white")

        CommonKassei.SetDropdownListNotReadonly(control)

    End Sub

    ''' <summary>
    ''' 金額を算出する
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetKingaku()

        Dim blnComErrFlg As Boolean

        '初期化
        blnComErrFlg = True

        '未入力チェック
        If Me.tbxZeinuki.Text = String.Empty Then
            Me.lblSyouhizei.Text = String.Empty
            Me.lblZeikomi.Text = String.Empty
            Exit Sub
        End If

        Dim syouhizei As String
        syouhizei = (KihonJyouhouInquiryBc.GetKakaku(Me.hidZeikbnTxt.Value))
        If syouhizei = "0" Then
            '消費税にをセットする
            Me.lblSyouhizei.Text = syouhizei

        Else
            '消費税区分から税率を求める
            Me.lblSyouhizei.Text = SetKingaku(Fix(SetVal(SetDouble(Me.tbxZeinuki.Text.Replace(",", ""))) * SetVal(syouhizei * 100) / 100))
            '消費税区分から税率を求める

        End If

        '税込金額を算出する
        Me.lblZeikomi.Text = SetKingaku(SetVal(SetDouble(tbxZeinuki.Text.Replace(",", "") + SetVal(lblSyouhizei.Text))))


    End Sub

    ''' <summary>
    ''' 金額を算出する
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetKingaku1()

        Dim blnComErrFlg As Boolean

        '初期化
        blnComErrFlg = True

        '未入力チェック
        If Me.tbxZeinuki1.Text = String.Empty Then
            Me.lblSyouhizei1.Text = String.Empty
            Me.lblZeikomi1.Text = String.Empty
            Exit Sub
        End If

        Dim syouhizei As String
        syouhizei = (KihonJyouhouInquiryBc.GetKakaku(Me.hidZeikbnTxt1.Value))
        If syouhizei = "0" Then
            '消費税にをセットする
            Me.lblSyouhizei1.Text = SetKingaku(syouhizei)

        Else
            '消費税区分から税率を求める
            Me.lblSyouhizei1.Text = SetKingaku(Fix(SetVal(SetDouble(Me.tbxZeinuki1.Text.Replace(",", ""))) * SetVal(syouhizei * 100) / 100))
            '消費税区分から税率を求める

        End If

        '税込金額を算出する
        Me.lblZeikomi1.Text = SetKingaku(SetVal(SetDouble(tbxZeinuki1.Text.Replace(",", ""))) + SetVal(lblSyouhizei1.Text))


    End Sub

    ''' <summary>
    ''' 見せ請求を取得
    ''' </summary>
    ''' <param name="ptn"></param>
    ''' <param name="seikyuuSaki"></param>
    ''' <param name="keiretu"></param>
    ''' <param name="shouhin"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMiseSeikyuu(ByVal ptn As Object, ByVal seikyuuSaki As String, _
                                       ByVal keiretu As Object, ByVal shouhin As Object) As String

        Dim table As String           '取得対象テーブル名
        Dim key As String           '取得用キー
        Dim item As String           '取得項目名
        GetMiseSeikyuu = "True"
        '初期化

        '請求先= 直接請求接請求の場合、相互に同じ金額を設定にセット
        If seikyuuSaki = "直" Then
            Select Case ptn
                Case "A"
                    '商品コード・請求有無変更時は工務店請求額と実請求額に同額を設定（そのまま）
                Case "B"
                    '実請求金額変更時は工務店請求金額に実請求金額を設定
                    koumuten = jitu
                Case Else
                    '工務店請求金額変更時は実請求金額に工務店請求金額を設定
                    jitu = koumuten
            End Select

            Exit Function

        End If

        '/////↓以下他請求時処理↓/////

        '系列ｺｰﾄﾞにより分岐
        Select Case keiretu
            'アイフルホーム
            Case "0001"
                table = "m_honbu_seikyuu"
                item = "honbumuke_kkk"
                key = "jhs_syouhin_cd"
                'ＴＨ友の会請求
            Case "THTH"
                table = "m_th_seikyuuyou_kakaku"
                item = "th_muke_kkk"
                key = "syouhin_cd"
                'ワンダーホーム
            Case "NF03"
                table = "m_wh_seikyuuyou_kakaku"
                item = "honbumuke_kkk"
                key = "syouhin_cd"
                '上記以外
            Case Else
                Select Case ptn
                    Case "A"
                        '他請求・３系列以外の請求有無・商品コード変更時
                        koumuten = 0
                    Case "B"
                        '他請求・３系列以外の実請求金額変更時(設定なし）
                        GetMiseSeikyuu = "False"
                    Case Else
                        '他請求・３系列以外の工務店請求金額変更時
                        '（他請求・３系列は工務店請求金額入力不可なので通常有り得ない）
                        koumuten = 0
                        GetMiseSeikyuu = "False"
                End Select
                Exit Function
        End Select

        '/////↓以下他請求で３系列時の処理↓/////

        '請求有無・商品コード変更時、工務店請求金額=0の場合は実請求にを設定
        If (ptn = "A") And koumuten = 0 Then
            jitu = 0
            Exit Function
        End If

        Dim value As String
        value = KihonJyouhouInquiryBc.GelKakaku(item, table, key, shouhin, ptn, jitu, koumuten)

        If value.ToString = String.Empty Then
            Me.btnTouroku.Enabled = False
            ShowMsg2(Messages.Instance.MSG2013E, Me.btnTouroku)
            GetMiseSeikyuu = "False"
        Else
            Select Case ptn
                Case "B"
                    '他請求・３系列以外の請求有無・商品コード変更時
                    koumuten = value
                Case "A"
                    '他請求・３系列以外の実請求金額変更時(設定なし）
                    jitu = value
                Case Else
                    '他請求・３系列以外の工務店請求金額変更時
                    '（他請求・３系列は工務店請求金額入力不可なので通常有り得ない）
                    jitu = value
            End Select
        End If
    End Function

    ''' <summary>
    ''' 金額表示
    ''' </summary>
    ''' <param name="uservalue">value</param>
    ''' <param name="flg">flg</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetKingaku(ByVal uservalue As String, Optional ByVal flg As Boolean = True)

        Try
            Dim value As String
            value = uservalue
            value = value.Replace(",", "")

            value = Int(value)

            If flg = False Then
                If value.Length > 8 Then
                    value = value.Substring(0, 8)
                End If
            End If


            If value = String.Empty Then
                Return "0"
            End If

            Dim invalue As Integer
            invalue = Convert.ToInt32(value)

            Return invalue.ToString("###,###,##0")

        Catch ex As Exception
            Return uservalue
        End Try


    End Function

    ''' <summary>
    ''' MsgBox表示
    ''' </summary>
    ''' <param name="msg"></param>
    ''' <param name="param1"></param>
    ''' <param name="param2"></param>
    ''' <param name="param3"></param>
    ''' <param name="param4"></param>
    ''' <remarks></remarks>
    Public Sub ShowMsg(ByVal msg As String, _
                                ByVal control As System.Web.UI.Control, _
                                Optional ByVal param1 As String = "", _
                                Optional ByVal param2 As String = "", _
                                Optional ByVal param3 As String = "", _
                                Optional ByVal param4 As String = "")

        Dim csScript As New StringBuilder


        Dim pPage As Page = Parent.Page
        Dim pType As Type = pPage.GetType
        Dim methodInfo As System.Reflection.MethodInfo = pType.GetMethod("ShowMsg")

        msg = msg.Replace("@PARAM1", param1) _
                          .Replace("@PARAM2", param2) _
                          .Replace("@PARAM3", param3) _
                          .Replace("@PARAM4", param4)

        If Not methodInfo Is Nothing Then
            methodInfo.Invoke(pPage, New Object() {control.ClientID, csScript.AppendFormat( _
                                                                "" & msg & "", _
                                                                param1, param2, param3, param4).ToString _
                                                                })
        End If


    End Sub

    ''' <summary>
    ''' MsgBox表示
    ''' </summary>
    ''' <param name="msg"></param>
    ''' <param name="param1"></param>
    ''' <param name="param2"></param>
    ''' <param name="param3"></param>
    ''' <param name="param4"></param>
    ''' <remarks></remarks>
    Public Sub ShowMsg2(ByVal msg As String, _
                                ByVal control As System.Web.UI.Control, _
                                Optional ByVal param1 As String = "", _
                                Optional ByVal param2 As String = "", _
                                Optional ByVal param3 As String = "", _
                                Optional ByVal param4 As String = "")

        Dim csScript As New StringBuilder


        Dim pPage As Page = Parent.Page
        Dim pType As Type = pPage.GetType
        Dim methodInfo As System.Reflection.MethodInfo = pType.GetMethod("ShowMsg2")

        msg = msg.Replace("@PARAM1", param1) _
                          .Replace("@PARAM2", param2) _
                          .Replace("@PARAM3", param3) _
                          .Replace("@PARAM4", param4)

        If Not methodInfo Is Nothing Then
            methodInfo.Invoke(pPage, New Object() {control.ClientID, csScript.AppendFormat( _
                                                                "" & msg & "", _
                                                                param1, param2, param3, param4).ToString _
                                                                })
        End If


    End Sub

    ''' <summary>
    ''' 項目桁数チェック処理
    ''' </summary>
    ''' <param name="rvntData"></param>
    ''' <param name="rstrItemName"></param>
    ''' <param name="rlngMax"></param>
    ''' <param name="rlngType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function chkKetaSuu(ByVal control As System.Web.UI.Control, _
                                            ByVal rvntData As String, _
                                            ByVal rstrItemName As String, _
                                            ByVal rlngMax As Long, _
                                            ByVal rlngType As Long) As Boolean

        '値のCheckを行なう
        If rvntData = String.Empty Then
            Return True
        End If

        '文字数チェック
        If System.Text.Encoding.Default.GetBytes(rvntData).Length() > rlngMax Then
            Dim csScript As New StringBuilder

            'MsgBox 表示
            If rlngType = HANKAKU Then
                '半角　{0}に登録できる文字数は、半角{1}文字以内です。
                msgAndFocus.AppendMsgAndCtrl(control, Messages.Instance.MSG2003E, rstrItemName, rlngMax)
            Else

                '全角　{0}に登録できる文字数は、全角{1}文字以内です。
                msgAndFocus.AppendMsgAndCtrl(control, Messages.Instance.MSG2002E, rstrItemName, Int(rlngMax / 2).ToString)
            End If
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 日符チェック
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function chkDate(ByVal value As String) As String

        Dim strBuf As String = String.Empty        '作業BUF

        chkDate = "false"

        '入力チェック
        If value = String.Empty Then
            Return String.Empty
        End If

        'If value = "00000000" Then
        '    '特別の場合です。
        '    Return "00000000"
        'End If

        'If Len(Trim(value)) = "0" Then
        '    '特別の場合です。
        '    Return "0"

        'End If

        '引数が数字のみか確認
        If IsNumeric(value) Then
            '文字カウント
            Select Case Len(value)
                Case 4   'MMDD
                    strBuf = Mid(Format(Convert.ToDateTime(KihonJyouhouInquiryBc.GetSysDate), "yyyy"), 1, 4)
                    strBuf = strBuf & "/"
                    strBuf = strBuf & Mid(value, 1, 2)
                    strBuf = strBuf & "/"
                    strBuf = strBuf & Mid(value, 3, 2)
                Case 6   'YYMMDD

                    If Mid(value, 1, 2) > "70" Then
                        strBuf = "19"
                    Else
                        strBuf = "20"
                    End If

                    strBuf = strBuf & Mid(value, 1, 2)
                    strBuf = strBuf & "/"
                    strBuf = strBuf & Mid(value, 3, 2)
                    strBuf = strBuf & "/"
                    strBuf = strBuf & Mid(value, 5, 2)

                Case 8   'YYYYMMDD

                    strBuf = strBuf & Mid(value, 1, 4)
                    strBuf = strBuf & "/"
                    strBuf = strBuf & Mid(value, 5, 2)
                    strBuf = strBuf & "/"
                    strBuf = strBuf & Mid(value, 7, 2)

                Case Else
                    'エラー(入力された日付に誤りがある。)

                    Exit Function
            End Select

            '日付方に変換できる確認
            If Not IsDate(strBuf) Then
                'エラー(入力された日付に誤りがある。)
                Return "false"

            End If


            '"/"を含む入力
        ElseIf IsDate(value) Then
            strBuf = Format(CDate(value), YMD)
        Else
            'Date変換不能(入力された日付に誤りがある。)
            Return "false"
        End If



        Return strBuf

    End Function

    ''' <summary>
    ''' クリア
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearArea()
        Me.lblUriageKeijou.Text = String.Empty
        Me.tbxAddDate.Text = String.Empty
        Me.tbxSyouhinCd.Text = String.Empty
        Me.tbxZeinuki.Text = String.Empty
        Me.lblSyouhizei.Text = String.Empty
        Me.lblZeikomi.Text = String.Empty
        Me.tbxKoumutenSeikyuuGaku.Text = String.Empty
        Me.tbxSeikyuDate.Text = String.Empty
        Me.tbxUriDate.Text = String.Empty
        Me.tbxBikou.Text = String.Empty
    End Sub
    ''' <summary>
    ''' クリア１
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearArea1()
        Me.lblUriageKeijou1.Text = String.Empty
        Me.tbxAddDate1.Text = String.Empty
        Me.tbxSyouhinCd1.Text = String.Empty
        Me.tbxZeinuki1.Text = String.Empty
        Me.lblSyouhizei1.Text = String.Empty
        Me.lblZeikomi1.Text = String.Empty
        Me.tbxSeikyuDate1.Text = String.Empty
        Me.tbxUriDate1.Text = String.Empty
        Me.tbxBikou1.Text = String.Empty
    End Sub
    ''' <summary>
    ''' doubleを設定
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetDouble(ByVal value As String) As Double
        If value.Trim = String.Empty Then
            Return 0
        End If
        Return Convert.ToDouble(value)
    End Function

    ''' <summary>
    ''' VBのval同じ
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetVal(ByVal value As String) As Double
        If value.Trim = String.Empty Then
            Return 0
        End If
        Return Convert.ToDouble(value.Replace(",", ""))
    End Function


    ''' <summary>
    ''' 日check
    '''
    ''' </summary>
    ''' <param name="d1">date1</param>
    ''' <param name="d2">date2</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function YMDCheck(ByVal d1 As Date, ByVal d2 As Date) As Integer

        If d1.ToString(YMD) > d2.ToString(YMD) Then
            Return 1
        ElseIf d1.ToString(YMD) = d2.ToString(YMD) Then
            Return 0
        Else
            Return -1
        End If

    End Function


#End Region


    Protected Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub



End Class


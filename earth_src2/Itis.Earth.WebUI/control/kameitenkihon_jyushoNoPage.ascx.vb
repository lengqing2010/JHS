Imports Itis.Earth.DataAccess
Imports Itis.Earth.BizLogic

''' <summary>
''' 加盟店基本情報＿住所１～４
''' </summary>
''' <remarks></remarks>
Partial Public Class kameitenkihon_jyushoNoPage1
    Inherits System.Web.UI.UserControl

#Region "共通変数"
    ''' <summary> クラスのインスタンス生成 </summary>
    Private KihonJyouhouInquiryBc As New Itis.Earth.BizLogic.kihonjyouhou.KihonJyouhouInquiryLogic
    Private msgAndFocus As New Itis.Earth.BizLogic.kihonjyouhou.MessageAndFocus
    Private Const HANKAKU As Integer = 1
    Private Const ZENKAKU As Integer = 2
#End Region

#Region "プロパティ"
    '加盟店コード
    Private _kameiten_cd As String
    '住所
    Private _jyusho As KameitenjyushoDataSet.m_kameiten_jyuusyoDataTable
    '住所No
    Private _jyushoNo As Integer
    '権限
    Private _kenngenn As Boolean

    ''' <summary>
    ''' 住所情報
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property jyusho() As KameitenjyushoDataSet.m_kameiten_jyuusyoDataTable
        Get
            Return _jyusho
        End Get
        Set(ByVal value As KameitenjyushoDataSet.m_kameiten_jyuusyoDataTable)
            _jyusho = value
        End Set
    End Property

    ''' <summary>
    ''' 住所NO
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property jyushoNo() As Integer
        Get
            Return _jyushoNo
        End Get

        Set(ByVal value As Integer)
            _jyushoNo = value
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
    ''' 権限
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Kenngenn() As Boolean
        Get
            Return _kenngenn
        End Get

        Set(ByVal value As Boolean)
            _kenngenn = value
        End Set

    End Property

    Private _upd_login_user_id As String
    Public Property Upd_login_user_id() As String
        Get
            Return _upd_login_user_id
        End Get

        Set(ByVal value As String)
            _upd_login_user_id = value
        End Set

    End Property

    Private _copy_button_flg As Boolean
    Public Property Copy_button_flg() As Boolean
        Get
            Return _copy_button_flg
        End Get

        Set(ByVal value As Boolean)
            _copy_button_flg = value
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

        '保証期間
        If Not itKassei Then

            For Each c As Control In meisaiTbody.Controls

                Try
                    CType(c, TextBox).ReadOnly = Not itKassei
                    CType(c, TextBox).CssClass = IIf(itKassei, "", "readOnly")
                Catch ex1 As Exception
                    Try
                        CType(c, Button).Enabled = itKassei
                        CType(c, Button).CssClass = IIf(itKassei, "", "readOnly")
                    Catch ex2 As Exception
                        Try
                            CType(c, DropDownList).Enabled = itKassei
                            CType(c, DropDownList).CssClass = IIf(itKassei, "", "readOnly")
                        Catch ex As Exception
                        End Try

                    End Try
                End Try
            Next

            Me.ddlTodoufuken.Enabled = False
            Me.btnTouroku.Enabled = False
            Me.btnSakujyo.Enabled = False
            Me.btnCopy.Enabled = False



        End If
    End Sub

    ''' <summary>
    ''' 画面初期化
    ''' </summary>
    ''' <param name="sender">System.Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            '画面の表示
            PageInit()
            MakeJavaScript()

        End If
        SetKassei()
    End Sub

    ''' <summary>
    ''' 登録ボタン
    ''' </summary>
    ''' <param name="sender">System.Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub btnTouroku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTouroku.Click

        '　入力チェック
        If Not chkInputValue() Then
            Exit Sub
        End If

        '　他の端末で更新チェック
        If Not ChkOtherUserKousin() Then
            Exit Sub
        End If

        '　画面のデータを取得
        Dim kameitenjyushoDataSet As KameitenjyushoDataSet
        kameitenjyushoDataSet = GetGamenData()

        '更新
        If KihonJyouhouInquiryBc.SetKameitenJyushoInfo(_kameiten_cd, _jyushoNo, kameitenjyushoDataSet) Then
            '画面を更新　（住所１～４）
            Dim otherPageFunction As New Itis.Earth.BizLogic.kihonjyouhou.OtherPageFunction
            otherPageFunction.DoFunction(Parent.Page, "GetKousinData")

            Dim maxDate As String
            maxDate = KihonJyouhouInquiryBc.GetMaxUpdDate(_kameiten_cd).Split(",")(0)
            '更新日
            '  Me.hidUpdTime.Value = KihonJyouhouInquiryBc.GetMaxUpdDate(_kameiten_cd)

            CType(Me.Parent.FindControl("Kameitenkihon_jyusho1").FindControl("hidUpdTime"), HiddenField).Value = maxDate
            CType(Me.Parent.FindControl("Kameitenkihon_jyushoNoPage1").FindControl("hidUpdTime"), HiddenField).Value = maxDate
            CType(Me.Parent.FindControl("Kameitenkihon_jyushoNoPage2").FindControl("hidUpdTime"), HiddenField).Value = maxDate
            CType(Me.Parent.FindControl("Kameitenkihon_jyushoNoPage3").FindControl("hidUpdTime"), HiddenField).Value = maxDate
            CType(Me.Parent.FindControl("Kameitenkihon_jyushoNoPage4").FindControl("hidUpdTime"), HiddenField).Value = maxDate


            '完了メッセージを表示する。
            ShowMsg(Messages.Instance.MSG018S, btnTouroku, "住所" & _jyushoNo - 1)
        Else
            ShowMsg(Messages.Instance.MSG019E, btnTouroku, "住所" & _jyushoNo - 1)
        End If



    End Sub

    ''' <summary>
    ''' 削除ボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSakujyo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        '　他の端末で更新チェック
        If Not ChkOtherUserKousin() Then
            Exit Sub
        End If

        Dim delFlg As Integer
        delFlg = KihonJyouhouInquiryBc.DelKameitenjyousyo(_kameiten_cd, _jyushoNo, _upd_login_user_id)
        If delFlg = 1 Then

            '　画面のデータを取得
            Dim kameitenjyushoDataSet As KameitenjyushoDataSet
            kameitenjyushoDataSet = GetGamenData()

            '画面を更新　（住所１～４）
            Dim otherPageFunction As New Itis.Earth.BizLogic.kihonjyouhou.OtherPageFunction
            otherPageFunction.DoFunction(Parent.Page, "GetKousinData")

            '更新日

            Dim maxDate As String
            maxDate = KihonJyouhouInquiryBc.GetMaxUpdDate(_kameiten_cd)
            CType(Me.Parent.FindControl("Kameitenkihon_jyusho1").FindControl("hidUpdTime"), HiddenField).Value = maxDate
            CType(Me.Parent.FindControl("Kameitenkihon_jyushoNoPage1").FindControl("hidUpdTime"), HiddenField).Value = maxDate
            CType(Me.Parent.FindControl("Kameitenkihon_jyushoNoPage2").FindControl("hidUpdTime"), HiddenField).Value = maxDate
            CType(Me.Parent.FindControl("Kameitenkihon_jyushoNoPage3").FindControl("hidUpdTime"), HiddenField).Value = maxDate
            CType(Me.Parent.FindControl("Kameitenkihon_jyushoNoPage4").FindControl("hidUpdTime"), HiddenField).Value = maxDate

            '完了メッセージを表示する。
            ShowMsg(Messages.Instance.MSG018S, btnTouroku, "住所" & _jyushoNo - 1)
        ElseIf delFlg = -1 Then
            ShowMsg(Messages.Instance.MSG019E, btnTouroku, "住所" & _jyushoNo - 1)
        Else
            ShowMsg(Messages.Instance.MSG2009E, Me.btnSakujyo, )
        End If

    End Sub

    ''' <summary>
    ''' 住所ﾘﾝｸ
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub lbtnJyosho_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtnJyosho.Click

        '明細を表示
        If meisaiTbody.Style.Item("display") = "none" Then
            meisaiTbody.Style.Item("display") = "inline"
            btnTouroku.Style.Item("display") = "inline"
            btnSakujyo.Style.Item("display") = "inline"
            If Me._copy_button_flg Then
                Me.btnCopy.Style.Item("display") = "inline"
            End If

            lblSign.Style.Item("display") = "none"
        Else

            meisaiTbody.Style.Item("display") = "none"
            btnTouroku.Style.Item("display") = "none"
            btnSakujyo.Style.Item("display") = "none"

            If Me._copy_button_flg Then
                Me.btnCopy.Style.Item("display") = "none"
            End If

            lblSign.Style.Item("display") = "inline"
        End If
    End Sub

    '''' <summary>
    '''' 郵便NO.
    '''' </summary>
    '''' <param name="sender">System.Object</param>
    '''' <param name="e">System.EventArgs</param>
    '''' <remarks></remarks>
    'Protected Sub tbxYuubinNo1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbxYuubinNo1.TextChanged

    '    '住所
    '    Dim jyuusyo As String
    '    Dim jyuusyoMei As String
    '    Dim jyuusyoNo As String
    '    Dim msgAndFocus As New Itis.Earth.BizLogic.kihonjyouhou.MessageAndFocus
    '    If Me.tbxYuubinNo1.Text = String.Empty Then

    '        msgAndFocus.setFocus(Me.Page, Me.tbxJyuusyo1)
    '        Exit Sub
    '    End If
    '    '住所取得
    '    jyuusyo = (KihonJyouhouInquiryBc.GetMailAddress(Me.tbxYuubinNo1.Text.Replace("-", String.Empty)))

    '    If jyuusyo = "," Then
    '        Exit Sub
    '    End If

    '    jyuusyoMei = GetJyusho(jyuusyo.Split(",")(1))
    '    jyuusyoNo = jyuusyo.Split(",")(0)
    '    '画面.住所1＝未入力、かつ、画面.住所2＝未入力の場合																			
    '    '画面.住所1←取得した住所情報（先頭から40バイトまで）																		
    '    '画面.住所2←取得した住所情報（先頭から41バイト目以降）																		
    '    '先頭から40バイトで分けた時に全角文字が切れてしまう場合は、																	
    '    '先頭から39バイトまでを住所1に設定し、それ以降を住所2に設定する																	
    '    If Me.tbxJyuusyo1.Text.Trim = String.Empty And Me.tbxJyuusyo2.Text.Trim = String.Empty Then
    '        Me.tbxJyuusyo1.Text = jyuusyoMei.Split(",")(0)
    '        Me.tbxJyuusyo2.Text = jyuusyoMei.Split(",")(1)
    '    End If

    '    If jyuusyoNo.Length > 3 Then
    '        jyuusyoNo = jyuusyoNo.Substring(0, 3) & "-" & jyuusyoNo.Substring(3, jyuusyoNo.Length - 3)
    '    End If

    '    Me.tbxYuubinNo1.Text = jyuusyoNo.Trim


    '    msgAndFocus.setFocus(Me.Page, Me.tbxJyuusyo1)
    'End Sub

#End Region

#Region "処理"

    ''' <summary>
    ''' 画面の表示
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub PageInit()

        '登録権限設定
        Setkenngen()
        '画面の値を設定
        SetGamenValue()
        'DropDownListのJavaScriptを追加
        AddDropDownCtrl()


    End Sub

    ''' <summary>
    ''' 登録権限設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Setkenngen()
        '登録権限
        If _kenngenn Then
            Me.btnTouroku.Enabled = True
            Me.btnSakujyo.Enabled = True
        Else
            Me.btnTouroku.Enabled = False
            Me.btnSakujyo.Enabled = False
        End If

    End Sub

    ''' <summary>
    ''' 画面の値を設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetGamenValue()

        '加盟店住所情報
        Dim data As KameitenjyushoDataSet.m_kameiten_jyuusyoRow()
        lblSign.Text = String.Empty

        '加盟店住所情報を取得する。
        data = _jyusho.Select("jyuusyo_no=" & _jyushoNo.ToString)

        If _jyusho.Select("jyuusyo_no=" & (_jyushoNo - 1).ToString).Length < 1 Then
            Me.btnTouroku.Enabled = False
        End If

        lbtnJyosho.Text = "送付先住所" & (_jyushoNo - 1).ToString

        '「営業所情報コピー」ボタンを表示
        Me.btnCopy.Visible = _copy_button_flg

        If data.Length = 1 Then

            If _kenngenn Then
                Me.btnSakujyo.Enabled = True
            Else
                Me.btnSakujyo.Enabled = False
            End If


            '郵便NO
            If Not data(0).Isyuubin_noNull Then
                tbxYuubinNo1.Text = data(0).yuubin_no
            End If

            '住所１
            If Not data(0).Isjyuusyo1Null Then
                tbxJyuusyo1.Text = data(0).jyuusyo1
            End If

            '住所２
            If Not data(0).Isjyuusyo2Null Then
                tbxJyuusyo2.Text = data(0).jyuusyo2
            End If

            '住所２
            If Not data(0).Isbusyo_meiNull Then
                tbxBusyoMei1.Text = data(0).busyo_mei
            End If

            '郵便No
            If Not (data(0).Isyuubin_noNull) Then
                tbxYuubinNo1.Text = data(0).yuubin_no
            End If

            '部署名
            If Not (data(0).Isbusyo_meiNull) Then
                tbxBusyoMei1.Text = data(0).busyo_mei
            End If

            '代表者名
            If Not (data(0).Isdaihyousya_meiNull) Then
                tbxDaihyousyaMei1.Text = data(0).daihyousya_mei
            End If

            'Tel
            If Not (data(0).Istel_noNull) Then
                tbxTelNo1.Text = data(0).tel_no
            End If

            'FAX
            If Not (data(0).Isfax_noNull) Then
                tbxFaxNo1.Text = data(0).fax_no
            End If

            '備考１
            If Not (data(0).Isbikou1Null) Then
                tbxBikou11.Text = data(0).bikou1
            End If
            '都道府県
            If Not (data(0).Istodouhuken_cdNull) Then
                ddlTodoufuken.SelectedValue = data(0).todouhuken_cd
            End If
            If Not (data(0).Isseikyuusyo_flgNull) Then
                If data(0).seikyuusyo_flg = -1 Then
                    ddlSeikyuusyo.SelectedIndex = 1
                    lblSign.Text += "請求書" & "&nbsp;&nbsp;"
                Else
                    ddlSeikyuusyo.SelectedIndex = 0
                End If
            End If

            If Not (data(0).Ishosyousyo_flgNull) Then
                If data(0).hosyousyo_flg = -1 Then
                    ddlHosyousyo.SelectedIndex = 1
                    lblSign.Text += "保証書" & "&nbsp;&nbsp;"
                Else
                    ddlHosyousyo.SelectedIndex = 0
                End If
            End If

            If Not (data(0).Ishkks_flgNull) Then
                If data(0).hkks_flg = -1 Then
                    ddlhkks.SelectedIndex = 1
                    lblSign.Text += "報告書" & "&nbsp;&nbsp;"
                Else
                    ddlhkks.SelectedIndex = 0
                End If
            End If

            If Not (data(0).Isteiki_kankou_flgNull) Then
                If data(0).teiki_kankou_flg = -1 Then
                    ddlTeikiKankou.SelectedIndex = 1
                    lblSign.Text += "定期刊行" & "&nbsp;&nbsp;"
                Else
                    ddlTeikiKankou.SelectedIndex = 0
                End If
            End If

            If Not (data(0).Iskasi_hosyousyo_flgNull) Then
                If data(0).kasi_hosyousyo_flg = -1 Then
                    ddlKasihosyousyo.SelectedIndex = 1
                    lblSign.Text += "瑕疵保証書印字" & "&nbsp;&nbsp;"
                Else
                    ddlKasihosyousyo.SelectedIndex = 0
                End If
            End If

            If Not (data(0).Iskoj_hkks_flgNull) Then
                If data(0).koj_hkks_flg = -1 Then
                    ddlKojhkks.SelectedIndex = 1
                    lblSign.Text += "工事報告書" & "&nbsp;&nbsp;"
                Else
                    ddlKojhkks.SelectedIndex = 0
                End If
            End If

            If Not (data(0).Iskensa_hkks_flgNull) Then
                If data(0).kensa_hkks_flg = -1 Then
                    ddlkensahkks.SelectedIndex = 1
                    lblSign.Text += "検査報告書" & "&nbsp;&nbsp;"
                Else
                    ddlkensahkks.SelectedIndex = 0
                End If
            End If



        Else
            Me.btnSakujyo.Enabled = False
            '郵便NO
            tbxYuubinNo1.Text = String.Empty
            '住所１
            tbxJyuusyo1.Text = String.Empty
            '住所２
            tbxJyuusyo2.Text = String.Empty
            '住所２
            tbxBusyoMei1.Text = String.Empty
            '郵便No
            tbxYuubinNo1.Text = String.Empty
            '部署名
            tbxBusyoMei1.Text = String.Empty
            '代表者名
            tbxDaihyousyaMei1.Text = String.Empty
            'Tel
            tbxTelNo1.Text = String.Empty
            'FAX
            tbxFaxNo1.Text = String.Empty
            '備考１
            tbxBikou11.Text = String.Empty
            ddlSeikyuusyo.SelectedIndex = 0
            ddlHosyousyo.SelectedIndex = 0
            ddlhkks.SelectedIndex = 0
            ddlTeikiKankou.SelectedIndex = 0
            ddlKasihosyousyo.SelectedIndex = 0
            ddlKojhkks.SelectedIndex = 0
            ddlkensahkks.SelectedIndex = 0
            Me.hidUpdTime.Value = String.Empty
            lblSign.Text = String.Empty
            '都道府県
            ddlTodoufuken.SelectedValue = ""
        End If

        '更新日

        Me.hidUpdTime.Value = KihonJyouhouInquiryBc.GetMaxUpdDate(_kameiten_cd)

        '請求LABLE
        If data.Length = 1 Then
            lblSign.Text = "登録あり&nbsp;&nbsp;" + lblSign.Text
        Else
            lblSign.Text = "登録なし&nbsp;&nbsp;"
        End If


    End Sub

    ''' <summary>
    ''' 入力チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function chkInputValue() As Boolean

        '未入力チェック
        chkMinyuuryoku()
        '半角桁数チェック
        chkHankaku()
        '全角桁数チェック
        chkZenkaku()

        '禁則チェック
        checkKinsoku()

        ''メッセージ表示
        If msgAndFocus.Message <> String.Empty Then
            ShowMsg(msgAndFocus.Message, msgAndFocus.focusCtrl)
            Return False
        Else
            Return True
        End If
    End Function

    ''' <summary>
    ''' 半角チェック
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub chkHankaku()
        '･･････入力桁数チェック(半角)
        '郵便番号
        chkKetaSuu(Me.tbxYuubinNo1, Me.tbxYuubinNo1.Text, "郵便番号", 8, 1)
        '電話番号
        chkKetaSuu(Me.tbxTelNo1, Me.tbxTelNo1.Text, "電話番号", 16, 1)
        'FAX番号
        chkKetaSuu(Me.tbxFaxNo1, Me.tbxFaxNo1.Text, "FAX番号", 16, 1)
    End Sub

    ''' <summary>
    ''' 全角チェック
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub chkZenkaku()

        '入力桁数チェック(全角)
        '住所1
        chkKetaSuu(Me.tbxJyuusyo1, Me.tbxJyuusyo1.Text, "住所1", 40, 2)

        '住所2
        chkKetaSuu(Me.tbxJyuusyo2, Me.tbxJyuusyo2.Text, "住所2", 30, 2)

        '部署名
        chkKetaSuu(Me.tbxBusyoMei1, Me.tbxBusyoMei1.Text, "部署名", 50, 2)

        '備考1
        chkKetaSuu(Me.tbxBikou11, Me.tbxBikou11.Text, "備考", 30, 2)

        '代表者名
        chkKetaSuu(Me.tbxDaihyousyaMei1, Me.tbxDaihyousyaMei1.Text, "代表者", 20, 2)
    End Sub

    ''' <summary>
    ''' 未入力チェック
    ''' 加盟店住所１　必須入力
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function chkMinyuuryoku() As Boolean

        '･････････加盟店住所１
        If Trim(tbxJyuusyo1.Text = String.Empty) Then

            '住所1は必須入力です
            msgAndFocus.AppendMsgAndCtrl(tbxJyuusyo1, Messages.Instance.MSG013E, "住所1")

        End If

        Return True

    End Function

    ''' <summary>
    ''' 項目桁数チェック処理
    ''' </summary>
    ''' <param name="data">data</param>
    ''' <param name="itemName">itemName</param>
    ''' <param name="max">max</param>
    ''' <param name="type">type</param>
    ''' <remarks></remarks>
    Public Sub chkKetaSuu(ByVal control As System.Web.UI.Control, _
                                            ByVal data As String, _
                                            ByVal itemName As String, _
                                            ByVal max As Long, _
                                            ByVal type As Long)

        '値のCheckを行なう
        If data = String.Empty Then
            Exit Sub
        End If

        '文字数チェック
        If System.Text.Encoding.Default.GetBytes(data).Length() > max Then
            Dim csScript As New StringBuilder

            'MsgBox 表示
            If type = HANKAKU Then

                '半角　{0}に登録できる文字数は、半角{1}文字以内です。
                msgAndFocus.AppendMsgAndCtrl(control, Messages.Instance.MSG2003E, itemName, max)
            Else

                '全角　{0}に登録できる文字数は、全角{1}文字以内です。
                msgAndFocus.AppendMsgAndCtrl(control, Messages.Instance.MSG2002E, itemName, Int(max / 2))
            End If
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' MsgBox表示
    ''' </summary>
    ''' <param name="msg">msg</param>
    ''' <param name="control">WebControl</param>
    ''' <param name="param1">param</param>
    ''' <param name="param2">param</param>
    ''' <param name="param3">param</param>
    ''' <param name="param4">param</param>
    ''' <remarks></remarks>
    Public Sub ShowMsg(ByVal msg As String, _
                                ByVal control As System.Web.UI.Control, _
                                Optional ByVal param1 As String = "", _
                                Optional ByVal param2 As String = "", _
                                Optional ByVal param3 As String = "", _
                                Optional ByVal param4 As String = "")

        Dim csScript As New StringBuilder


        Dim pPage As Page = Me.Parent.Page
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
    ''' 画面のデータを取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetGamenData() As KameitenjyushoDataSet

        Dim kameitenjyushoDataSet As New KameitenjyushoDataSet
        Dim dr As KameitenjyushoDataSet.m_kameiten_jyuusyoRow

        dr = kameitenjyushoDataSet.m_kameiten_jyuusyo.NewRow
        dr.kameiten_cd = _kameiten_cd
        dr.jyuusyo_no = _jyushoNo
        dr.jyuusyo1 = tbxJyuusyo1.Text
        dr.jyuusyo2 = tbxJyuusyo2.Text
        dr.yuubin_no = tbxYuubinNo1.Text
        dr.tel_no = tbxTelNo1.Text
        dr.fax_no = Me.tbxFaxNo1.Text
        dr.busyo_mei = tbxBusyoMei1.Text
        dr.daihyousya_mei = tbxDaihyousyaMei1.Text
        dr.bikou1 = tbxBikou11.Text

        dr.hkks_flg = 0
        If ddlhkks.SelectedIndex = 1 Then
            dr.hkks_flg = -1
        End If

        dr.hosyousyo_flg = 0
        If ddlHosyousyo.SelectedIndex = 1 Then
            dr.hosyousyo_flg = -1
        End If

        dr.kasi_hosyousyo_flg = 0
        If ddlKasihosyousyo.SelectedIndex = 1 Then
            dr.kasi_hosyousyo_flg = -1
        End If

        dr.kensa_hkks_flg = 0
        If ddlkensahkks.SelectedIndex = 1 Then
            dr.kensa_hkks_flg = -1
        End If

        dr.koj_hkks_flg = 0
        If ddlKojhkks.SelectedIndex = 1 Then
            dr.koj_hkks_flg = -1
        End If

        dr.seikyuusyo_flg = 0
        If ddlSeikyuusyo.SelectedIndex = 1 Then
            dr.seikyuusyo_flg = -1
        End If

        dr.teiki_kankou_flg = 0
        If ddlTeikiKankou.SelectedIndex = 1 Then
            dr.teiki_kankou_flg = -1
        End If
        dr.upd_login_user_id = _upd_login_user_id
        dr.add_login_user_id = _upd_login_user_id
        dr.todouhuken_cd = ddlTodoufuken.SelectedValue
        kameitenjyushoDataSet.m_kameiten_jyuusyo.Rows.Add(dr)

        Return kameitenjyushoDataSet
    End Function

    ''' <summary>
    ''' 他の端末で更新チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ChkOtherUserKousin()
        '他の端末で更新チェック
        Dim msg As String
        msg = KihonJyouhouInquiryBc.ChkJyushoTouroku(_kameiten_cd, Me.hidUpdTime.Value)
        If msg <> String.Empty Then
            ShowMsg(msg, btnTouroku)
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' DropDownListのJavaScriptを追加
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddDropDownCtrl()

        Dim csScript As New StringBuilder

        'ArrayはClientIdを追加
        csScript.AppendFormat("arrdrp1.push('" & ddlhkks.ClientID & "');")
        csScript.AppendFormat("arrdrp2.push('" & ddlHosyousyo.ClientID & "');")
        csScript.AppendFormat("arrdrp3.push('" & ddlKasihosyousyo.ClientID & "');")
        csScript.AppendFormat("arrdrp4.push('" & ddlkensahkks.ClientID & "');")
        csScript.AppendFormat("arrdrp5.push('" & ddlKojhkks.ClientID & "');")
        csScript.AppendFormat("arrdrp6.push('" & ddlSeikyuusyo.ClientID & "');")
        csScript.AppendFormat("arrdrp7.push('" & ddlTeikiKankou.ClientID & "');")

        'onchange追加
        ddlhkks.Attributes.Add("onchange", "chksyo(this,1)")
        ddlHosyousyo.Attributes.Add("onchange", "chksyo(this,2)")
        ddlKasihosyousyo.Attributes.Add("onchange", "chksyo(this,3)")
        ddlkensahkks.Attributes.Add("onchange", "chksyo(this,4)")
        ddlKojhkks.Attributes.Add("onchange", "chksyo(this,5)")
        ddlSeikyuusyo.Attributes.Add("onchange", "chksyo(this,6)")
        ddlTeikiKankou.Attributes.Add("onchange", "chksyo(this,7)")

        ' Me.btnKensaku.Attributes.Add("onclick", "return chkJyuusyoNoPage('" & Me.tbxJyuusyo1.ClientID & "','" & Me.tbxJyuusyo2.ClientID & "');")

        Me.tbxYuubinNo1.Attributes.Add("onblur", "SetYoubin(this)")
        'Me.tbxYuubinNo1.Attributes.Add("onchange", "ShowModal()")


        Me.btnCopy.Attributes.Add("onclick", "return copyCheck()")

        ScriptManager.RegisterStartupScript(Me, _
                                        Me.GetType(), _
                                        "dropdownlistid" & _jyushoNo, _
                                        csScript.ToString, _
                                        True)

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
    ''' 禁則チェック
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub checkKinsoku()
        Dim chkobj As New CommonCheck

        '住所1
        If chkobj.checkKinsoku(Me.tbxJyuusyo1.Text, "住所1") <> String.Empty Then

            msgAndFocus.AppendMsgAndCtrl(Me.tbxJyuusyo1, chkobj.checkKinsoku(Me.tbxJyuusyo1.Text, "住所1"))
            '終了処理

        End If

        '住所2
        If chkobj.checkKinsoku(Me.tbxJyuusyo2.Text, "住所2") <> String.Empty Then

            msgAndFocus.AppendMsgAndCtrl(Me.tbxJyuusyo2, chkobj.CheckKinsoku(Me.tbxJyuusyo2.Text, "住所2"))

        End If

        '部署名
        If chkobj.checkKinsoku(Me.tbxBusyoMei1.Text, "部署名") <> String.Empty Then
            msgAndFocus.AppendMsgAndCtrl(Me.tbxBusyoMei1, chkobj.checkKinsoku(Me.tbxBusyoMei1.Text, "部署名"))
        End If

        '備考1
        If chkobj.checkKinsoku(Me.tbxBikou11.Text, "備考") <> String.Empty Then
            msgAndFocus.AppendMsgAndCtrl(Me.tbxBikou11, chkobj.checkKinsoku(Me.tbxBikou11.Text, "備考"))

        End If

        '代表者名
        If chkobj.checkKinsoku(Me.tbxDaihyousyaMei1.Text, "代表者") <> String.Empty Then
            msgAndFocus.AppendMsgAndCtrl(Me.tbxDaihyousyaMei1, chkobj.checkKinsoku(Me.tbxDaihyousyaMei1.Text, "代表者"))
        End If


        '郵便番号
        If chkobj.checkKinsoku(Me.tbxYuubinNo1.Text, "郵便番号") <> String.Empty Then
            msgAndFocus.AppendMsgAndCtrl(Me.tbxYuubinNo1, chkobj.checkKinsoku(Me.tbxYuubinNo1.Text, "郵便番号"))
        End If

        '電話番号
        If chkobj.checkKinsoku(Me.tbxTelNo1.Text, "電話番号") <> String.Empty Then
            msgAndFocus.AppendMsgAndCtrl(Me.tbxTelNo1, chkobj.checkKinsoku(Me.tbxTelNo1.Text, "電話番号"))
        End If

        'FAX番号
        If chkobj.checkKinsoku(Me.tbxFaxNo1.Text, "FAX番号") <> String.Empty Then
            msgAndFocus.AppendMsgAndCtrl(Me.tbxFaxNo1, chkobj.checkKinsoku(Me.tbxFaxNo1.Text, "FAX番号"))
        End If

    End Sub

#End Region

    Protected Sub btnCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy.Click

        '加盟店情報取得
        Dim kameitenTableDataTable As New Itis.Earth.DataAccess.KameitenDataSet.m_kameitenTableDataTable
        Dim kihonJyouhouInquiryLogic As New BizLogic.kihonjyouhou.KihonJyouhouInquiryLogic
        kameitenTableDataTable = kihonJyouhouInquiryLogic.GetkameitenInfo(_kameiten_cd)

        If kameitenTableDataTable.Rows.Count = 0 Then
            Dim msg As String
            msg = Messages.Instance.MSG020E
            If msg <> String.Empty Then
                ShowMsg(msg, Me.btnCopy)
                Exit Sub
            End If
        End If

        Dim eigyousyoCd As String = String.Empty

        If Not kameitenTableDataTable(0).Iseigyousyo_cdNull Then
            eigyousyoCd = kameitenTableDataTable(0).eigyousyo_cd
        End If


        Dim eigyousyoTableDataTable As KameitenDataSet.eigyousyoTableDataTable

        eigyousyoTableDataTable = kihonJyouhouInquiryLogic.GetEigyousyo(eigyousyoCd)

        If eigyousyoTableDataTable.Rows.Count > 0 Then
            '郵便No.
            Me.tbxYuubinNo1.Text = eigyousyoTableDataTable(0).yuubin_no
            '住所１
            Me.tbxJyuusyo1.Text = eigyousyoTableDataTable(0).jyuusyo1
            '住所２
            Me.tbxJyuusyo2.Text = eigyousyoTableDataTable(0).jyuusyo2

            Me.tbxTelNo1.Text = eigyousyoTableDataTable(0).tel_no

            Me.tbxFaxNo1.Text = eigyousyoTableDataTable(0).fax_no

            Me.tbxBusyoMei1.Text = eigyousyoTableDataTable(0).busyo_mei


        End If


    End Sub

    ''' <summary>
    ''' JAVASCRIPT
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub MakeJavaScript()
        Dim csType As Type = Page.GetType()
        Dim csName As String = "chkJyuushoGamenChangeNoPage"
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("<script language='javascript' type='text/javascript'>  " & vbCrLf)
            .AppendLine("function chkJyuushoGamenChange()" & vbCrLf)
            .AppendLine("{" & vbCrLf)

            .AppendLine("if(document.getElementById('" & Me.tbxYuubinNo1.ClientID & "').value!=''){" & vbCrLf)
            .AppendLine("return false;}" & vbCrLf)

            .AppendLine("if(document.getElementById('" & Me.tbxJyuusyo1.ClientID & "').value!=''){" & vbCrLf)
            .AppendLine("return false;}" & vbCrLf)

            .AppendLine("if(document.getElementById('" & Me.tbxJyuusyo2.ClientID & "').value!=''){" & vbCrLf)
            .AppendLine("return false;}" & vbCrLf)

            .AppendLine("if(document.getElementById('" & Me.tbxFaxNo1.ClientID & "').value!=''){" & vbCrLf)
            .AppendLine("return false;}" & vbCrLf)

            .AppendLine("if(document.getElementById('" & Me.tbxTelNo1.ClientID & "').value!=''){" & vbCrLf)
            .AppendLine("return false;}" & vbCrLf)

            .AppendLine("if(document.getElementById('" & Me.tbxBusyoMei1.ClientID & "').value!=''){" & vbCrLf)
            .AppendLine("return false;}" & vbCrLf)

            .AppendLine("return true;}" & vbCrLf)

            .AppendLine("function chkJyuusyoNoPage(id1,id2)" & vbCrLf)

            .AppendLine("{" & vbCrLf)

            .AppendLine("if(document.getElementById(id1).value!='' || document.getElementById(id2).value!=''){" & vbCrLf)
            .AppendLine("if (!confirm('既存データがありますが上書きしてよろしいですか。')){return false;}else{ " & vbCrLf)
            '.AppendLine("fncOpenwindowYuubin('" & Me.tbxYuubinNo1.ClientID & _
            '                                                        "','" & Me.tbxJyuusyo1.ClientID & "','" & _
            '                                                         Me.tbxJyuusyo2.ClientID & "');return false;")
            .AppendLine("return true;}" & vbCrLf)
            .AppendLine("}" & vbCrLf)

            '.AppendLine("fncOpenwindowYuubin('" & Me.tbxYuubinNo1.ClientID & _
            '                                                        "','" & Me.tbxJyuusyo1.ClientID & "','" & _
            '                                                         Me.tbxJyuusyo2.ClientID & "');return false;")
            .AppendLine("return true;}" & vbCrLf)

            .AppendLine("</script>" & vbCrLf)




        End With

        'ScriptManager.RegisterStartupScript(Me, _
        '                              Me.GetType(), _
        '                              "chkJyuushoGamenChange", _
        '                              csScript.ToString, _
        '                              True)

        Me.Page.ClientScript.RegisterClientScriptBlock(csType, csName, csScript.ToString)
    End Sub

    Protected Sub btnKensaku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKensaku.Click
        '住所
        Dim data As DataSet
        Dim jyuusyo As String
        Dim jyuusyoMei As String
        Dim jyuusyoNo As String
        'If Me.tbxYuubinNo1.Text = String.Empty Then
        '    Exit Sub
        'End If
        '住所取得
        Dim csScript As New StringBuilder

        data = (KihonJyouhouInquiryBc.GetMailAddress(Me.tbxYuubinNo1.Text.Replace("-", String.Empty).Trim))
        If data.Tables(0).Rows.Count = 1 Then


            jyuusyo = data.Tables(0).Rows(0).Item(0).ToString
            jyuusyoNo = jyuusyo.Split(",")(0)

            jyuusyoMei = GetJyusho(jyuusyo.Split(",")(1))

            'Me.tbxJyuusyo1.Text = jyuusyoMei.Split(",")(0)
            'Me.tbxJyuusyo2.Text = jyuusyoMei.Split(",")(1)

            If jyuusyoNo.Length > 3 Then
                jyuusyoNo = jyuusyoNo.Substring(0, 3) & "-" & jyuusyoNo.Substring(3, jyuusyoNo.Length - 3)
            End If
            'Me.tbxYuubinNo1.Text = jyuusyoNo

            csScript.AppendLine("if(document.getElementById('" & Me.tbxJyuusyo1.ClientID & "').value!='' || document.getElementById('" & Me.tbxJyuusyo2.ClientID & "').value!='' || document.getElementById('" & CType(ddlTodoufuken.FindControl("ddlCommonDrop"), DropDownList).ClientID & "').value!=''){" & vbCrLf)
            csScript.AppendLine("if (!confirm('既存データがありますが上書きしてよろしいですか。')){}else{ " & vbCrLf)

            csScript.AppendLine("document.getElementById('" & Me.tbxJyuusyo1.ClientID & "').value = '" & jyuusyoMei.Split(",")(0) & "';" & vbCrLf)
            csScript.AppendLine("document.getElementById('" & Me.tbxJyuusyo2.ClientID & "').value = '" & jyuusyoMei.Split(",")(1) & "';" & vbCrLf)
            csScript.AppendLine("document.getElementById('" & Me.tbxYuubinNo1.ClientID & "').value = '" & jyuusyoNo & "';" & vbCrLf)
            csScript.AppendLine("document.getElementById('" & CType(ddlTodoufuken.FindControl("ddlCommonDrop"), DropDownList).ClientID & "').value = '" & data.Tables(0).Rows(0).Item(1).ToString & "';" & vbCrLf)

            csScript.AppendLine("}" & vbCrLf)

            csScript.AppendLine("}else{" & vbCrLf)
            csScript.AppendLine("document.getElementById('" & Me.tbxJyuusyo1.ClientID & "').value = '" & jyuusyoMei.Split(",")(0) & "';" & vbCrLf)
            csScript.AppendLine("document.getElementById('" & Me.tbxJyuusyo2.ClientID & "').value = '" & jyuusyoMei.Split(",")(1) & "';" & vbCrLf)
            csScript.AppendLine("document.getElementById('" & Me.tbxYuubinNo1.ClientID & "').value = '" & jyuusyoNo & "';" & vbCrLf)
            csScript.AppendLine("document.getElementById('" & CType(ddlTodoufuken.FindControl("ddlCommonDrop"), DropDownList).ClientID & "').value = '" & data.Tables(0).Rows(0).Item(1).ToString & "';" & vbCrLf)

            csScript.AppendLine("}" & vbCrLf)
        Else


            csScript.AppendLine("document.getElementById('" & CType(ddlTodoufuken.FindControl("ddlCommonDrop"), DropDownList).ClientID & "').value = '';" & vbCrLf)



            csScript.AppendLine("fncOpenwindowYuubin('" & Me.tbxYuubinNo1.ClientID & _
                                                                    "','" & Me.tbxJyuusyo1.ClientID & "','" & _
                                                                     Me.tbxJyuusyo2.ClientID & "','" & CType(ddlTodoufuken.FindControl("ddlCommonDrop"), DropDownList).ClientID & "');")



        End If


        ScriptManager.RegisterStartupScript(Me, _
                                        Me.GetType(), _
                                        "openWindowYuubin", _
                                        csScript.ToString, _
                                        True)


    End Sub
End Class
Imports Itis.Earth.DataAccess
Imports Itis.Earth.BizLogic
''' <summary>
''' 加盟店基本情報＿住所
''' </summary>
''' <remarks></remarks>
Partial Public Class kameitenkihon_jyusho_user
    Inherits System.Web.UI.UserControl

#Region "共通変数"

    ''' <summary> クラスのインスタンス生成 </summary>
    Private kihonJyouhouInquiryBc As New Itis.Earth.BizLogic.kihonjyouhou.KihonJyouhouInquiryLogic
    '半角
    Private Const HANKAKU As Integer = 1
    '全角
    Private Const ZENKAKU As Integer = 2
    'メッセージとFOCUS
    Public msgAndFocus As New Itis.Earth.BizLogic.kihonjyouhou.MessageAndFocus

#End Region

#Region "プロパティ"

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

    '住所情報
    Private _jyusho As KameitenjyushoDataSet.m_kameiten_jyuusyoDataTable
    Public Property jyusho() As KameitenjyushoDataSet.m_kameiten_jyuusyoDataTable
        Get
            'ロジック変更のタイミングで開発者が任意にインクリメンタルします

            Return _jyusho
        End Get

        Set(ByVal value As KameitenjyushoDataSet.m_kameiten_jyuusyoDataTable)
            _jyusho = value
        End Set

    End Property

    '加盟店コード
    Private _kameiten_cd As String
    Public Property kameiten_cd() As String
        Get
            'ロジック変更のタイミングで開発者が任意にインクリメンタルします

            Return _kameiten_cd
        End Get

        Set(ByVal value As String)
            _kameiten_cd = value
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

            For Each c As Control In meisaiTbody5.Controls

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
            '画面初期化
            PageInit()
        
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

        '入力チェック
        If Not chkInputValue() Then
            Exit Sub
        End If

        '登録年月の自動表示
        If Me.tbxAddNengetu.Text = String.Empty Then
            'システム日付を表示する
            Me.tbxAddNengetu.Text = Convert.ToDateTime(kihonJyouhouInquiryBc.GetSysDate).ToString("yyyy/MM")
        End If

        '他の端末で更新チェック
        If Not ChkOtherUserKousin() Then
            Exit Sub
        End If



        ' 住所情報登録
        If SetKameitenJyushoInfo() Then

            Dim maxDate As String
            maxDate = kihonJyouhouInquiryBc.GetMaxUpdDate(_kameiten_cd).Split(",")(0)
            '更新日
            Me.hidUpdTime.Value = maxDate

            '画面を更新　（住所１～４）
            Dim otherPageFunction As New Itis.Earth.BizLogic.kihonjyouhou.OtherPageFunction
            otherPageFunction.DoFunction(Parent.Page, "GetKousinData")

            ShowMsg(Messages.Instance.MSG018S, Me.btnTouroku, "住所情報")
        Else
            ShowMsg(Messages.Instance.MSG019E, Me.btnTouroku, "住所情報")
        End If



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

        data = (kihonJyouhouInquiryBc.GetMailAddress(Me.tbxYuubinNo1.Text.Replace("-", String.Empty).Trim))
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


#End Region

#Region "処理"

    ''' <summary>
    ''' 画面初期化
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub PageInit()

        '登録権限設定
        Setkenngen()

        'Javascript Event Bind
        BindJavaScriptEvent()

        '画面の値を設定
        SetGamenValue()

    End Sub

    ''' <summary>
    ''' 登録権限の設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Setkenngen()

        '登録権限
        If _kenngenn Then
            Me.btnTouroku.Enabled = True

        Else
            Me.btnTouroku.Enabled = False
        End If

    End Sub

    ''' <summary>
    ''' Javascript Event Bind
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BindJavaScriptEvent()
        '  登録年月
        Me.tbxAddNengetu.Attributes.Add("onfocus", "setOnfocusNengetu(this)")
        Me.tbxAddNengetu.Attributes.Add("onblur", "chkNengetu(this)")
        'Me.btnKensaku.Attributes.Add("onclick", "fncOpenwindowYuubin('" & Me.tbxYuubinNo1.ClientID & _
        '"','" & Me.tbxJyuusyo1.ClientID & "','" & _
        ' Me.tbxJyuusyo2.ClientID & "');return false;")


        '  Me.btnKensaku.Attributes.Add("onclick", "return chkJyuusyoNoPage('" & Me.tbxJyuusyo1.ClientID & "','" & Me.tbxJyuusyo2.ClientID & "');")

        Me.tbxYuubinNo1.Attributes.Add("onblur", "SetYoubin(this)")
        'Me.tbxYuubinNo1.Attributes.Add("onchange", "ShowModal()")

        'Me.btnTouroku.Attributes.Add("onclick", "ShowModal();")
    End Sub

    ''' <summary>
    ''' 画面の値を設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetGamenValue()

        '加盟店住所情報を取得する。
        Dim data As KameitenjyushoDataSet.m_kameiten_jyuusyoRow()
        data = _jyusho.Select("jyuusyo_no=1")

        'データがある時、画面を表示
        If data.Length = 1 Then

            '登録年月
            If Not data(0).Isadd_nengetuNull Then
                Me.tbxAddNengetu.Text = data(0).add_nengetu
            End If

            '郵便NO
            If Not data(0).Isyuubin_noNull Then
                Me.tbxYuubinNo1.Text = data(0).yuubin_no
            End If

            '住所１
            If Not data(0).Isjyuusyo1Null Then
                Me.tbxJyuusyo1.Text = data(0).jyuusyo1
            End If

            '住所２
            If Not data(0).Isjyuusyo2Null Then
                Me.tbxJyuusyo2.Text = data(0).jyuusyo2
            End If

            '住所２
            If Not data(0).Isbusyo_meiNull Then
                Me.tbxBusyoMei1.Text = data(0).busyo_mei
            End If

            '郵便No
            If Not (data(0).Isyuubin_noNull) Then
                Me.tbxYuubinNo1.Text = data(0).yuubin_no
            End If

            '部署名
            If Not (data(0).Isbusyo_meiNull) Then

                Me.tbxBusyoMei1.Text = data(0).busyo_mei
            End If

            '代表者名
            If Not (data(0).Isdaihyousya_meiNull) Then
                Me.tbxDaihyousyaMei1.Text = data(0).daihyousya_mei
            End If

            'Tel
            If Not (data(0).Istel_noNull) Then
                Me.tbxTelNo1.Text = data(0).tel_no
            End If

            'FAX
            If Not (data(0).Isfax_noNull) Then
                Me.tbxFaxNo1.Text = data(0).fax_no
            End If

            'Address
            If Not (data(0).Ismail_addressNull) Then
                Me.tbxMailAddress.Text = data(0).mail_address
            End If

            '備考１
            If Not (data(0).Isbikou1Null) Then
                Me.tbxBikou11.Text = data(0).bikou1
            End If

            '備考２
            If Not (data(0).Isbikou2Null) Then
                Me.tbxBikou21.Text = data(0).bikou2
            End If
            '都道府県
            If Not (data(0).Istodouhuken_cdNull) Then
                ddlTodoufuken.SelectedValue = data(0).todouhuken_cd
            End If
        End If
        '更新日

        Me.hidUpdTime.Value = kihonJyouhouInquiryBc.GetMaxUpdDate(_kameiten_cd)

    End Sub

    ''' <summary>
    ''' 住所情報登録
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetKameitenJyushoInfo() As Boolean

        '画面のデータを取得
        Dim kameitenjyushoDataSet As New KameitenjyushoDataSet
        Dim dr As KameitenjyushoDataSet.m_kameiten_jyuusyoRow

        '値の取得
        dr = kameitenjyushoDataSet.m_kameiten_jyuusyo.NewRow
        dr.kameiten_cd = _kameiten_cd
        dr.jyuusyo_no = 1
        dr.jyuusyo1 = Me.tbxJyuusyo1.Text
        dr.jyuusyo2 = Me.tbxJyuusyo2.Text
        dr.yuubin_no = Me.tbxYuubinNo1.Text
        dr.tel_no = Me.tbxTelNo1.Text
        dr.fax_no = Me.tbxFaxNo1.Text
        dr.busyo_mei = Me.tbxBusyoMei1.Text
        dr.daihyousya_mei = Me.tbxDaihyousyaMei1.Text
        dr.add_nengetu = Me.tbxAddNengetu.Text
        dr.bikou1 = Me.tbxBikou11.Text
        dr.bikou2 = Me.tbxBikou21.Text
        dr.mail_address = Me.tbxMailAddress.Text
        dr.upd_login_user_id = _upd_login_user_id
        dr.add_login_user_id = _upd_login_user_id
        dr.todouhuken_cd = ddlTodoufuken.SelectedValue
        kameitenjyushoDataSet.m_kameiten_jyuusyo.Rows.Add(dr)

        '登録
        Return kihonJyouhouInquiryBc.SetKameitenJyushoInfo(_kameiten_cd, 1, kameitenjyushoDataSet)

    End Function

    ''' <summary>
    ''' 他の端末で更新チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ChkOtherUserKousin() As Boolean

        '他の端末で更新チェック
        Dim msg As String
        msg = kihonJyouhouInquiryBc.ChkJyushoTouroku(_kameiten_cd, Me.hidUpdTime.Value)
        If msg <> String.Empty Then
            ShowMsg(msg, btnTouroku)
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 入力チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function chkInputValue() As Boolean

        If Me.tbxAddNengetu.Text <> String.Empty Then
            Dim commonCheck As New CommonCheck
            If commonCheck.CheckYuukouHiduke(Me.tbxAddNengetu.Text & "/01", "登録年月") <> String.Empty Then
                msgAndFocus.AppendMsgAndCtrl(Me.tbxAddNengetu, commonCheck.CheckYuukouHiduke(Me.tbxAddNengetu.Text & "/01", "登録年月"))
            End If
        End If


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
        '登録年月
        chkKetaSuu(Me.tbxAddNengetu, Me.tbxAddNengetu.Text, "登録年月", 7, 1)
        '郵便番号
        chkKetaSuu(Me.tbxYuubinNo1, Me.tbxYuubinNo1.Text, "郵便番号", 8, 1)
        '電話番号
        chkKetaSuu(Me.tbxTelNo1, Me.tbxTelNo1.Text, "電話番号", 16, 1)
        'FAX番号
        chkKetaSuu(Me.tbxFaxNo1, Me.tbxFaxNo1.Text, "FAX番号", 16, 1)
        '申込担当者
        chkKetaSuu(Me.tbxMailAddress, Me.tbxMailAddress.Text, "申込担当者", 64, 1)
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
        chkKetaSuu(Me.tbxBikou11, Me.tbxBikou11.Text, "備考1", 30, 2)

        '備考2
        chkKetaSuu(Me.tbxBikou21, Me.tbxBikou21.Text, "備考2", 30, 2)

        '代表者名
        chkKetaSuu(Me.tbxDaihyousyaMei1, Me.tbxDaihyousyaMei1.Text, "代表者", 20, 2)


    End Sub

    ''' <summary>
    ''' 未入力チェック
    ''' 加盟店住所１　必須入力
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub chkMinyuuryoku()
        '･････････加盟店住所１
        If Trim(Me.tbxJyuusyo1.Text = String.Empty) Then
            '住所1は必須入力です
            msgAndFocus.AppendMsgAndCtrl(Me.tbxJyuusyo1, Messages.Instance.MSG013E, "住所1")
        End If
    End Sub

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
    ''' 禁則チェック
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub checkKinsoku()

        'チェックObject
        Dim chkobj As New CommonCheck

        '住所1
        If chkobj.checkKinsoku(Me.tbxJyuusyo1.Text, "住所1") <> String.Empty Then
            msgAndFocus.AppendMsgAndCtrl(Me.tbxJyuusyo1, chkobj.checkKinsoku(Me.tbxJyuusyo1.Text, "住所1"))
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
        If chkobj.checkKinsoku(Me.tbxBikou11.Text, "備考1") <> String.Empty Then
            msgAndFocus.AppendMsgAndCtrl(Me.tbxBikou11, chkobj.checkKinsoku(Me.tbxBikou11.Text, "備考1"))
        End If

        '備考2
        If chkobj.checkKinsoku(Me.tbxBikou21.Text, "備考2") <> String.Empty Then
            msgAndFocus.AppendMsgAndCtrl(Me.tbxBikou21, chkobj.checkKinsoku(Me.tbxBikou21.Text, "備考2"))
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

        '申込担当者
        If chkobj.checkKinsoku(Me.tbxMailAddress.Text, "申込担当者") <> String.Empty Then
            msgAndFocus.AppendMsgAndCtrl(Me.tbxMailAddress, chkobj.checkKinsoku(Me.tbxMailAddress.Text, "申込担当者"))
        End If

    End Sub


    '''' <summary>
    '''' JAVASCRIPT
    '''' </summary>
    '''' <remarks></remarks>
    'Protected Sub MakeJavaScript()
    '    Dim csType As Type = Page.GetType()
    '    Dim csName As String = "chkJyuushoGamenChange1"
    '    Dim csScript As New StringBuilder
    '    With csScript
    '        .AppendLine("<script language='javascript' type='text/javascript'>  " & vbCrLf)

    '        .AppendLine("function chkJyuusyo()" & vbCrLf)
    '        .AppendLine("{" & vbCrLf)

    '        .AppendLine("if(document.getElementById('" & Me.tbxJyuusyo1.ClientID & "').value!='' || document.getElementById('" & Me.tbxJyuusyo2.ClientID & "').value!=''){" & vbCrLf)
    '        .AppendLine("if (!confirm('既存データがありますが上書きしてよろしいですか。')){return false;}else{ " & vbCrLf)
    '        '.AppendLine("fncOpenwindowYuubin('" & Me.tbxYuubinNo1.ClientID & _
    '        '                                                        "','" & Me.tbxJyuusyo1.ClientID & "','" & _
    '        '                                                         Me.tbxJyuusyo2.ClientID & "');return false;")
    '        .AppendLine("return true;}" & vbCrLf)
    '        .AppendLine("}" & vbCrLf)

    '        '.AppendLine("fncOpenwindowYuubin('" & Me.tbxYuubinNo1.ClientID & _
    '        '                                                        "','" & Me.tbxJyuusyo1.ClientID & "','" & _
    '        '                                                         Me.tbxJyuusyo2.ClientID & "');return false;")
    '        .AppendLine("return true;}" & vbCrLf)
    '        .AppendLine("</script>" & vbCrLf)




    '    End With

    '    Me.Page.ClientScript.RegisterClientScriptBlock(csType, csName, csScript.ToString)
    'End Sub

#End Region


  
End Class
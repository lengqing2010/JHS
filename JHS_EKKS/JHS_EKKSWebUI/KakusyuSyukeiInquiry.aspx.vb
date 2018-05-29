Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Lixil.JHS_EKKS.BizLogic
Imports Lixil.JHS_EKKS.Utilities
Imports Lixil.JHS_EKKS.Utilities.CommonMessage
Imports System.Data

''' <summary>
''' 各種集計
''' </summary>
''' <remarks>各種集計</remarks>
Partial Class KakusyuSyukeiInquiry
    Inherits System.Web.UI.Page

#Region "プライベート変数"

    Private kakusyuSyukeiInquiryBC As New KakusyuSyukeiInquiryBC
    Public CommonConstBC As Lixil.JHS_EKKS.BizLogic.CommonConstBC
    Private common As New Common


#End Region

#Region "定数"

    ''' <summary>
    ''' POP画面の区分
    ''' </summary>
    ''' <remarks></remarks>
    Private Enum popKbn As Integer

        Shiten = 1    '支店
        Kameiten = 2  '都道府県
        Eigyou = 3    '営業所
        Keiretu = 4   '系列
        User = 5      '営業マン
        TourokuJigyousya = 6 '登録事業者

    End Enum

#End Region

#Region "イベント"

    ''' <summary>
    ''' 画面の初期表示
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    ''' <history>2012/12/04　李宇(大連情報システム部)　新規作成</history>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)

        Dim CommonCheck As New CommonCheck
        CommonCheck.CommonNinsyou(String.Empty, Master.loginUserInfo, kegen.UserIdOnly)

        'JavaScript
        Call MakeJavaScript()

        If Not IsPostBack Then

            '初期表示時、画面のセットする
            Call SetSyokiSeltuto()
            '明細部を表示しない
            Me.divHead.Visible = False
        End If

        '支店 　'Siten'
        Me.tbxSiten.Attributes.Add("onfocusout", "fncToUpper($ID('" & Me.tbxSiten.ClientID & "'));fncTextBoxTrue();return false;")
        '都道府県　'Todouhuken'
        Me.tbxKameiten.Attributes.Add("onfocusout", "fncToUpper($ID('" & Me.tbxKameiten.ClientID & "'));fncTextBoxTrue();return false;")
        '営業所　'Eigyousyo'
        Me.tbxEigyou.Attributes.Add("onfocusout", "fncToUpper($ID('" & Me.tbxEigyou.ClientID & "'));fncTextBoxTrue();return false;")
        '系列名　'Keiretu'
        Me.tbxKeiretu.Attributes.Add("onfocusout", "fncToUpper($ID('" & Me.tbxKeiretu.ClientID & "'));fncTextBoxTrue();return false;")
        '営業マン　'EigyouMan'
        Me.tbxUser.Attributes.Add("onfocusout", "fncToUpper($ID('" & Me.tbxUser.ClientID & "'));fncTextBoxTrue();return false;")
        '登録事業者　'Tourokusya'
        Me.tbxTourokuJgousya.Attributes.Add("onfocusout", "fncToUpper($ID('" & Me.tbxTourokuJgousya.ClientID & "'));fncTextBoxTrue();return false;")

        Me.btnPopup.Attributes.Add("onClick", "fncTextBoxTrue();return false;")

        '画面のJS EVENT設定
        Call SetJsEvent()

    End Sub

    ''' <summary>
    ''' 年度ボタンを押下する時の処理
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <history>2012/12/05　李宇(大連情報システム部)　新規作成</history>
    Protected Sub btnNendo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNendo.Click

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)

        '年度選択リストボックス活性化
        Me.ddlNendo.Enabled = True

        '期間の選択を非活性
        Me.ddlKikanFrom.Enabled = False
        Me.ddlKikanTo.Enabled = False

        '月次の選択を非活性
        Me.ddlTukinamiFrom.Enabled = False
        Me.ddlTukinamiTo.Enabled = False
        Me.ddlTukinamiTo2.Enabled = False

    End Sub

    ''' <summary>
    ''' 期間ボタンを押下する時の処理
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <history>2012/12/05　李宇(大連情報システム部)　新規作成</history>
    Protected Sub btnKikan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKikan.Click

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)

        '年度選択リストボックス非活性
        Me.ddlNendo.Enabled = False

        '期間の選択を活性化
        Me.ddlKikanFrom.Enabled = True
        Me.ddlKikanTo.Enabled = True

        '月次の選択を非活性
        Me.ddlTukinamiFrom.Enabled = False
        Me.ddlTukinamiTo.Enabled = False
        Me.ddlTukinamiTo2.Enabled = False

    End Sub

    ''' <summary>
    ''' 月次ボタンを押下する時の処理
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <history>2012/12/05　李宇(大連情報システム部)　新規作成</history>
    Protected Sub btnTukinami_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTukinami.Click

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)

        '年度選択リストボックス非活性
        Me.ddlNendo.Enabled = False

        '期間の選択を非活性
        Me.ddlKikanFrom.Enabled = False
        Me.ddlKikanTo.Enabled = False

        '月次の選択を活性化
        Me.ddlTukinamiFrom.Enabled = True
        Me.ddlTukinamiTo.Enabled = True
        Me.ddlTukinamiTo2.Enabled = True

    End Sub

    ''' <summary>
    ''' 支店の検索ボタン押下時の処理
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <history>2012/12/04　李宇(大連情報システム部)　新規作成</history>
    Protected Sub btnSiten_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSiten.Click

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)

        If Me.tbxSiten.Text.Equals("ALL") Then
            Call SetClear(popKbn.Shiten)
            Me.hidModouru.Value = "Siten"
        Else
            If Me.tbxSiten.Text.Equals(String.Empty) Then
                Call ShoPop(popKbn.Shiten)
            Else
                Dim SitenSearchBC As New SitenSearchBC
                Dim dtSiten As Data.DataTable = SitenSearchBC.GetBusyoKanri("0", Me.tbxSiten.Text, False)
                If dtSiten.Rows.Count = 1 Then
                    Me.tbxSiten.Text = dtSiten.Rows(0).Item(0).ToString
                    Me.hidSitenCd.Value = dtSiten.Rows(0).Item(1).ToString
                    Me.hidModouru.Value = "Siten"
                    Call SetClear(popKbn.Shiten)
                Else
                    Call ShoPop(popKbn.Shiten)
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' 営業マンの検索ボタン押下時の処理
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <history>2012/12/04　李宇(大連情報システム部)　新規作成</history>
    Protected Sub btnUser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUser.Click

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)

        If Me.tbxUser.Text.Equals("ALL") Then
            Call SetClear(popKbn.User)
            Me.hidModouru.Value = "EigyouMan"
        Else
            If Me.tbxUser.Text.Equals(String.Empty) Then
                Call ShoPop(popKbn.User)
            Else
                Dim UserSearchBC As New EigyouManSearchBC
                Dim dtUserInfo As Data.DataTable = UserSearchBC.GetUserInfo("0", "", Me.tbxUser.Text, False)
                If dtUserInfo.Rows.Count = 1 Then
                    Me.tbxUser.Text = dtUserInfo.Rows(0).Item(1).ToString
                    Me.hidUserCd.Value = dtUserInfo.Rows(0).Item(0).ToString
                    Me.hidModouru.Value = "EigyouMan"
                    Call SetClear(popKbn.User)
                Else
                    Call ShoPop(popKbn.User)
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' 都道府県の検索ボタン押下時の処理
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <history>2012/12/04　李宇(大連情報システム部)　新規作成</history>
    Protected Sub btnKameiten_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKameiten.Click

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)

        If Me.tbxKameiten.Text.Equals("ALL") Then
            Call SetClear(popKbn.Kameiten)
            Me.hidModouru.Value = "Todouhuken"
        Else
            If Me.tbxKameiten.Text.Equals(String.Empty) Then
                Call ShoPop(popKbn.Kameiten)
            Else
                Dim TodouhukenBC As New TodoufukenSearchBC
                Dim Todouhuken As Data.DataTable = TodouhukenBC.GetTodoufukenMei("0", Me.tbxKameiten.Text)
                If Todouhuken.Rows.Count = 1 Then
                    Me.tbxKameiten.Text = Todouhuken.Rows(0).Item(1).ToString
                    Me.hidKameitenCd.Value = Todouhuken.Rows(0).Item(0).ToString
                    Me.hidModouru.Value = "Todouhuken"
                    Call SetClear(popKbn.Kameiten)
                Else
                    Call ShoPop(popKbn.Kameiten)
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' 系列名の検索ボタン押下時の処理
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <history>2012/12/04　李宇(大連情報システム部)　新規作成</history>
    Protected Sub btnKeiretu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKeiretu.Click

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)
        If Me.tbxKeiretu.Text.Equals("ALL") Then
            Call SetClear(popKbn.Keiretu)
            Me.hidModouru.Value = "Keiretu"
        Else
            If Me.tbxKeiretu.Text.Equals(String.Empty) Then
                Call ShoPop(popKbn.Keiretu)
            Else
                Dim KeiretuSearchBC As New KeiretuSearchBC
                Dim dtKeiretu As Data.DataTable = KeiretuSearchBC.GetKiretuJyouhou("0", "", Me.tbxKeiretu.Text, False)
                If dtKeiretu.Rows.Count = 1 Then
                    Me.tbxKeiretu.Text = dtKeiretu.Rows(0).Item(1).ToString
                    Me.hidKeiretuMei.Value = dtKeiretu.Rows(0).Item(0).ToString
                    Me.hidModouru.Value = "Keiretu"
                    Call SetClear(popKbn.Keiretu)
                Else
                    Call ShoPop(popKbn.Keiretu)
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' 営業所の検索ボタン押下時の処理
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <history>2012/12/04　李宇(大連情報システム部)　新規作成</history>
    Protected Sub btnEigyou_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEigyou.Click

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)

        If Me.tbxEigyou.Text.Equals("ALL") Then
            Call SetClear(popKbn.Eigyou)
            Me.hidModouru.Value = "Eigyousyo"
        Else
            If Me.tbxEigyou.Text.Equals(String.Empty) Then
                Call ShoPop(popKbn.Eigyou)
            Else
                Dim EigyousyoSearchBC As New EigyousyoSearchBC
                Dim dtEigyousyo As Data.DataTable = EigyousyoSearchBC.GetEigyousyoMei("0", Me.tbxEigyou.Text, False)
                If dtEigyousyo.Rows.Count = 1 Then
                    Me.tbxEigyou.Text = dtEigyousyo.Rows(0).Item(0).ToString
                    Me.hidEigyouCd.Value = dtEigyousyo.Rows(0).Item(1).ToString
                    Me.hidModouru.Value = "Eigyousyo"
                    Call SetClear(popKbn.Eigyou)
                Else
                    Call ShoPop(popKbn.Eigyou)
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' 登録事業者の検索ボタン押下時の処理
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <history>2012/12/07　李宇(大連情報システム部)　新規作成</history>
    Protected Sub btnTourokuJgousya_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTourokuJgousya.Click

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)

        If Me.tbxTourokuJgousya.Text.Equals("ALL") Then
            Call SetClear(popKbn.TourokuJigyousya)
            Me.hidModouru.Value = "Tourokusya"
        Else
            If Me.tbxTourokuJgousya.Text.Equals(String.Empty) Then
                Call ShoPop(popKbn.TourokuJigyousya)
            Else
                Dim TourokuJgousyaBC As New TourokuJigyousyaSearchBC
                Dim dtTourokuJgousya As Data.DataTable = TourokuJgousyaBC.GetTourokuJigyousya("0", "", Me.tbxTourokuJgousya.Text, False)
                If dtTourokuJgousya.Rows.Count = 1 Then
                    Me.tbxEigyou.Text = dtTourokuJgousya.Rows(0).Item(1).ToString
                    Me.hidTourokuJgousya.Value = dtTourokuJgousya.Rows(0).Item(0).ToString
                    Me.hidModouru.Value = "Tourokusya"
                    Call SetClear(popKbn.TourokuJigyousya)
                Else
                    Call ShoPop(popKbn.TourokuJigyousya)
                End If
            End If
        End If
    End Sub

    '''' <summary>
    '''' 結果表示ボタン押下時の処理
    '''' </summary>
    '''' <param name="sender">Object</param>
    '''' <param name="e">System.EventArgs</param>
    '''' <history>
    '''' <para>2013/01/10　李宇(大連情報システム部)　新規作成</para>
    '''' </history>
    'Protected Sub btnKeltukaHyouji_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKeltukaHyouji.Click

    '    'EMAB障害対応情報の格納処理
    '    EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
    '                           MyMethod.GetCurrentMethod.Name, sender, e)

    '    '年度LISTを選択しない時
    '    If ((Me.ddlNendo.Enabled = True) AndAlso (Me.ddlNendo.SelectedValue = 0)) OrElse _
    '       ((Me.ddlKikanFrom.Enabled = True) AndAlso (Me.ddlKikanFrom.SelectedValue = 0)) OrElse _
    '       ((Me.ddlTukinamiFrom.Enabled = True) AndAlso (Me.ddlTukinamiFrom.SelectedValue = 0)) Then
    '        'メッセージを表示する
    '        common.SetShowMessage(Me, MSG001E, "年度")
    '        '明細部を表示しない
    '        Me.divHead.Visible = False

    '        If Me.ddlNendo.Enabled = True Then
    '            Me.ddlNendo.Focus()
    '        ElseIf Me.ddlKikanFrom.Enabled = True Then
    '            Me.ddlKikanFrom.Focus()
    '        ElseIf Me.ddlTukinamiFrom.Enabled = True Then
    '            Me.ddlTukinamiFrom.Focus()
    '        End If

    '        '月別表示２だけ選べるように
    '    ElseIf (Me.ddlTukinamiFrom.Enabled = True) AndAlso (Me.ddlTukinamiTo.SelectedItem.Value = "0") Then
    '        common.SetShowMessage(Me, MSG048E)
    '        Me.ddlTukinamiTo.Focus()

    '        'FCチェックボックスと支店名の関係
    '    ElseIf (Me.chkFC.Checked = True) AndAlso (Me.tbxSiten.Text = String.Empty) Then

    '        Me.tbxSiten.Focus()

    '        'メッセージを表示する
    '        common.SetShowMessage(Me, MSG001E, "支店名")
    '        '明細部を表示しない
    '        Me.divHead.Visible = False

    '        '集計 内容選択一つも選択しない時
    '    ElseIf Me.tbxSiten.Text = String.Empty AndAlso _
    '           Me.tbxKameiten.Text = String.Empty AndAlso _
    '           Me.tbxEigyou.Text = String.Empty AndAlso _
    '           Me.tbxKeiretu.Text = String.Empty AndAlso _
    '           Me.tbxUser.Text = String.Empty AndAlso _
    '           Me.tbxTourokuJgousya.Text = String.Empty Then

    '        'メッセージを表示する
    '        common.SetShowMessage(Me, MSG058E)
    '        '明細部を表示しない
    '        Me.divHead.Visible = False

    '        '営業区分一つも選択しない時
    '    ElseIf Me.chkEigyou.Checked = False AndAlso _
    '            Me.chkFC.Checked = False AndAlso _
    '            Me.chkSinki.Checked = False AndAlso _
    '            Me.chkTokuhan.Checked = False Then
    '        'メッセージを表示する
    '        common.SetShowMessage(Me, MSG071E)

    '        '検索ボタンの押下判断する
    '    ElseIf Not Me.tbxSiten.Text.Equals(String.Empty) OrElse _
    '           Not Me.tbxKameiten.Text.Equals(String.Empty) OrElse _
    '           Not Me.tbxEigyou.Text.Equals(String.Empty) OrElse _
    '           Not Me.tbxKeiretu.Text.Equals(String.Empty) OrElse _
    '           Not Me.tbxUser.Text.Equals(String.Empty) OrElse _
    '           Not Me.tbxTourokuJgousya.Text.Equals(String.Empty) Then

    '        If CheckInput() = True OrElse _
    '           Me.tbxSiten.Text.Equals("ALL") OrElse _
    '           Me.tbxKameiten.Text.Equals("ALL") OrElse _
    '           Me.tbxEigyou.Text.Equals("ALL") OrElse _
    '           Me.tbxKeiretu.Text.Equals("ALL") OrElse _
    '           Me.tbxUser.Text.Equals("ALL") OrElse _
    '           Me.tbxTourokuJgousya.Text.Equals("ALL") Then

    '            '画面で選択したの項目
    '            If Not Me.tbxSiten.Text.Equals(String.Empty) Then
    '                Me.lblSyukeiSentaku.Text = "支店"
    '            ElseIf Not Me.tbxKameiten.Text.Equals(String.Empty) Then
    '                Me.lblSyukeiSentaku.Text = "都道府県"
    '            ElseIf Not Me.tbxEigyou.Text.Equals(String.Empty) Then
    '                Me.lblSyukeiSentaku.Text = "営業所"
    '            ElseIf Not Me.tbxKeiretu.Text.Equals(String.Empty) Then
    '                Me.lblSyukeiSentaku.Text = "系列名"
    '            ElseIf Not Me.tbxUser.Text.Equals(String.Empty) Then
    '                Me.lblSyukeiSentaku.Text = "営業マン"
    '            ElseIf Not Me.tbxTourokuJgousya.Text.Equals(String.Empty) Then
    '                Me.lblSyukeiSentaku.Text = "登録事業者"
    '            End If

    '            Dim dtMeisaiDate As Data.DataTable
    '            '支店
    '            Dim strSiten As String
    '            If Me.tbxSiten.Text.Equals("ALL") Then
    '                strSiten = Me.tbxSiten.Text
    '            Else
    '                strSiten = Me.hidSitenCd.Value
    '            End If
    '            '都道府県
    '            Dim strKameitenCd As String
    '            If Me.tbxKameiten.Text.Equals("ALL") Then
    '                strKameitenCd = Me.tbxKameiten.Text
    '            Else
    '                strKameitenCd = Me.hidKameitenCd.Value
    '            End If
    '            '営業所
    '            Dim strEigyouCd As String
    '            If Me.tbxEigyou.Text.Equals("ALL") Then
    '                strEigyouCd = Me.tbxEigyou.Text
    '            Else
    '                strEigyouCd = Me.hidEigyouCd.Value
    '            End If
    '            '系列名
    '            Dim strKeiretuMei As String
    '            If Me.tbxKeiretu.Text.Equals("ALL") Then
    '                strKeiretuMei = Me.tbxKeiretu.Text
    '            Else
    '                strKeiretuMei = Me.hidKeiretuMei.Value
    '            End If
    '            '営業マン
    '            Dim strUserCd As String
    '            If Me.tbxUser.Text.Equals("ALL") Then
    '                strUserCd = Me.tbxUser.Text
    '            Else
    '                strUserCd = Me.hidUserCd.Value
    '            End If
    '            '登録事業者
    '            Dim strTourokuJgousya As String
    '            If Me.tbxTourokuJgousya.Text.Equals("ALL") Then
    '                strTourokuJgousya = Me.tbxTourokuJgousya.Text
    '            Else
    '                strTourokuJgousya = Me.hidTourokuJgousya.Value
    '            End If

    '            '営業区分
    '            Dim strEigyouKbn As String = String.Empty '営業
    '            If Me.chkEigyou.Checked = True Then
    '                strEigyouKbn = strEigyouKbn & ",1"
    '            End If
    '            If Me.chkTokuhan.Checked = True Then '特販
    '                strEigyouKbn = strEigyouKbn & ",3"
    '            End If
    '            If Me.chkSinki.Checked = True Then '新規
    '                strEigyouKbn = strEigyouKbn & ",2"
    '            End If
    '            If Me.chkFC.Checked = True Then 'FC
    '                strEigyouKbn = strEigyouKbn & ",4"
    '            End If

    '            'データを取得する
    '            If (Me.ddlNendo.Enabled = True) Then

    '                'FCのチェックボックスをチェックしない
    '                If Me.chkFC.Checked = False Then

    '                    '年間:画面のデータを取得する
    '                    dtMeisaiDate = kakusyuSyukeiInquiryBC.GetKakusyuSyukeiData(strSiten, _
    '                                   strKameitenCd, strEigyouCd, strKeiretuMei, strUserCd, _
    '                                   strTourokuJgousya, Me.ddlNendo.SelectedValue, 1, 12, strEigyouKbn)

    '                ElseIf (Me.chkFC.Checked = True) AndAlso _
    '                       (Me.chkEigyou.Checked = False AndAlso Me.chkSinki.Checked = False AndAlso Me.chkTokuhan.Checked = False) Then

    '                    'FCのチェックボックスをチェックする
    '                    '年間:画面のデータを取得する
    '                    dtMeisaiDate = kakusyuSyukeiInquiryBC.GetKakusyuSyukeiFCData(strSiten, _
    '                                                          Me.ddlNendo.SelectedValue, 1, 12)
    '                Else
    '                    '集計
    '                    dtMeisaiDate = kakusyuSyukeiInquiryBC.GetKakusyuSyukeiSubeteData(strSiten, _
    '                                   Me.ddlNendo.SelectedValue, 1, 12, strEigyouKbn)

    '                End If

    '            ElseIf (Me.ddlKikanTo.Enabled = True AndAlso Me.ddlKikanFrom.Enabled = True) Then

    '                Dim intBegin As Integer
    '                Dim intEnd As Integer
    '                If Me.ddlKikanTo.SelectedItem.Text = "上期" Then
    '                    intBegin = 4
    '                    intEnd = 9
    '                ElseIf Me.ddlKikanTo.SelectedItem.Text = "四半期(4,5,6月)" Then
    '                    intBegin = 4
    '                    intEnd = 6
    '                ElseIf Me.ddlKikanTo.SelectedItem.Text = "四半期(7,8,9月)" Then
    '                    intBegin = 7
    '                    intEnd = 9
    '                ElseIf Me.ddlKikanTo.SelectedItem.Text = "四半期(10,11,12月)" Then
    '                    intBegin = 10
    '                    intEnd = 12
    '                ElseIf Me.ddlKikanTo.SelectedItem.Text = "四半期(1,2,3月)" Then
    '                    intBegin = 1
    '                    intEnd = 3
    '                ElseIf Me.ddlKikanTo.SelectedItem.Text = "下期" Then
    '                    intBegin = 10
    '                    intEnd = 15
    '                End If

    '                'FCのチェックボックスをチェックしない
    '                If Me.chkFC.Checked = False Then

    '                    '期間:画面のデータを取得する
    '                    dtMeisaiDate = kakusyuSyukeiInquiryBC.GetKakusyuSyukeiData(strSiten, _
    '                                   strKameitenCd, strEigyouCd, strKeiretuMei, strUserCd, _
    '                                   strTourokuJgousya, Me.ddlKikanFrom.SelectedValue, intBegin, intEnd, strEigyouKbn)

    '                ElseIf (Me.chkFC.Checked = True) AndAlso _
    '                       (Me.chkEigyou.Checked = False AndAlso Me.chkSinki.Checked = False AndAlso Me.chkTokuhan.Checked = False) Then

    '                    'FCのチェックボックスをチェックする
    '                    '期間:画面のデータを取得する
    '                    dtMeisaiDate = kakusyuSyukeiInquiryBC.GetKakusyuSyukeiFCData(strSiten, _
    '                                    Me.ddlKikanFrom.SelectedValue, intBegin, intEnd)

    '                Else
    '                    '集計
    '                    dtMeisaiDate = kakusyuSyukeiInquiryBC.GetKakusyuSyukeiSubeteData(strSiten, _
    '                                   Me.ddlKikanFrom.SelectedValue, intBegin, intEnd, strEigyouKbn)

    '                End If

    '            ElseIf (Me.ddlTukinamiFrom.Enabled = True AndAlso Me.ddlTukinamiTo.Enabled = True AndAlso Me.ddlTukinamiTo2.Enabled = True) Then

    '                Dim strTukinamiTo As String
    '                If Me.ddlTukinamiTo.SelectedValue < 4 Then
    '                    strTukinamiTo = Me.ddlTukinamiTo.SelectedValue + 12
    '                Else
    '                    strTukinamiTo = Me.ddlTukinamiTo.SelectedValue
    '                End If

    '                Dim strTukinamiTo2 As String
    '                If Me.ddlTukinamiTo2.SelectedItem IsNot Nothing Then
    '                    If Me.ddlTukinamiTo2.SelectedItem.Text.Equals("2月") Then
    '                        strTukinamiTo2 = 14
    '                    ElseIf Me.ddlTukinamiTo2.SelectedItem.Text.Equals("3月") Then
    '                        strTukinamiTo2 = 15
    '                    Else
    '                        strTukinamiTo2 = Me.ddlTukinamiTo2.SelectedValue
    '                    End If
    '                Else
    '                    strTukinamiTo2 = 15
    '                End If


    '                'FCのチェックボックスをチェックしない
    '                If Me.chkFC.Checked = False Then

    '                    '月次:画面のデータを取得する
    '                    dtMeisaiDate = kakusyuSyukeiInquiryBC.GetKakusyuSyukeiData(strSiten, _
    '                                   strKameitenCd, strEigyouCd, strKeiretuMei, strUserCd, _
    '                                   strTourokuJgousya, Me.ddlTukinamiFrom.SelectedValue, _
    '                                   strTukinamiTo, strTukinamiTo2, strEigyouKbn)

    '                ElseIf (Me.chkFC.Checked = True) AndAlso _
    '                       (Me.chkEigyou.Checked = False AndAlso Me.chkSinki.Checked = False AndAlso Me.chkTokuhan.Checked = False) Then

    '                    'FCのチェックボックスをチェックする
    '                    '月次:画面のデータを取得する
    '                    dtMeisaiDate = kakusyuSyukeiInquiryBC.GetKakusyuSyukeiFCData(strSiten, _
    '                                   Me.ddlTukinamiFrom.SelectedValue, strTukinamiTo, strTukinamiTo2)

    '                Else
    '                    '集計
    '                    dtMeisaiDate = kakusyuSyukeiInquiryBC.GetKakusyuSyukeiSubeteData(strSiten, _
    '                                   Me.ddlTukinamiFrom.SelectedValue, strTukinamiTo, strTukinamiTo2, strEigyouKbn)
    '                End If

    '            Else
    '                dtMeisaiDate = Nothing
    '            End If

    '            If dtMeisaiDate.Rows.Count = 0 Then
    '                'メッセージを表示する
    '                common.SetShowMessage(Me, MSG067E)
    '                '明細部を表示しない
    '                Me.divHead.Visible = False
    '            Else

    '                '明細部を表示する
    '                Me.divHead.Visible = True
    '                '画面で
    '                Call BoundGridView(dtMeisaiDate)
    '            End If

    '        End If

    '    End If

    '    If Me.tbxSiten.Text.Equals("ALL") Then
    '        Me.hidModouru.Value = "Siten"
    '    ElseIf Me.tbxKameiten.Text.Equals("ALL") Then
    '        Me.hidModouru.Value = "Todouhuken"
    '    ElseIf Me.tbxEigyou.Text.Equals("ALL") Then
    '        Me.hidModouru.Value = "Eigyousyo"
    '    ElseIf Me.tbxKeiretu.Text.Equals("ALL") Then
    '        Me.hidModouru.Value = "Keiretu"
    '    ElseIf Me.tbxUser.Text.Equals("ALL") Then
    '        Me.hidModouru.Value = "EigyouMan"
    '    ElseIf Me.tbxTourokuJgousya.Text.Equals("ALL") Then
    '        Me.hidModouru.Value = "Tourokusya"
    '    End If

    'End Sub

    ''' <summary>
    ''' 明細部のLEFT部分のRowDataBound
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">System.Web.UI.WebControls.GridViewRowEventArgs</param>
    ''' <remarks></remarks>
    ''' <history>2013/01/10　李宇(大連情報システム部)　新規作成</history>
    Protected Sub grdBodyLeft_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdBodyLeft.RowDataBound

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim title As String = CType(e.Row.FindControl("hidTitle"), HiddenField).Value

            Dim dtTemp As Data.DataTable
            Dim drValue As Data.DataRow
            Dim drTemp() As Data.DataRow
            drTemp = CType(ViewState("dtSource"), Data.DataTable).Select("busyo_cd='" & title & "'")

            dtTemp = CType(ViewState("dtSource"), Data.DataTable).Clone
            For Each drValue In drTemp
                dtTemp.ImportRow(drValue)
            Next

            CType(e.Row.FindControl("SyouhinInfo1"), CommonControl_KakusyuSyukeiInquiryInfo).DataSource = dtTemp
        End If

    End Sub

    ''' <summary>
    ''' 明細部のRight部分のRowDataBound
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">System.Web.UI.WebControls.GridViewRowEventArgs</param>
    ''' <remarks></remarks>
    ''' <history>2013/01/10　李宇(大連情報システム部)　新規作成</history>
    Protected Sub grdBodyRight_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdBodyRight.RowDataBound

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim title As String = CType(e.Row.FindControl("hidTitle"), HiddenField).Value

            Dim dtTemp As Data.DataTable
            Dim drValue As Data.DataRow
            Dim drTemp() As Data.DataRow
            drTemp = CType(ViewState("dtSource"), Data.DataTable).Select("busyo_cd='" & title & "'")

            dtTemp = CType(ViewState("dtSource"), Data.DataTable).Clone
            For Each drValue In drTemp
                dtTemp.ImportRow(drValue)
            Next

            CType(e.Row.FindControl("SyouhinData1"), CommonControl_KakusyuSyukeiInquiryData).DataSource = dtTemp
        End If

    End Sub

    ''' <summary>
    ''' 月別表示FROMのchange事件
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <history>2013/01/11　李宇(大連情報システム部)　新規作成</history>
    Protected Sub ddlTukinamiTo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTukinamiTo.SelectedIndexChanged

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)
        '月別表示１の値
        Dim selValue As String
        selValue = Me.ddlTukinamiTo.SelectedValue
        If Me.ddlTukinamiTo.SelectedItem.Value = "0" Then
            '月次ToDropdownListを取得する
            Dim dtTukinamiTo As Data.DataTable = kakusyuSyukeiInquiryBC.GetTukinamiListData()
            If dtTukinamiTo.Rows.Count > 0 Then
                Me.ddlTukinamiTo2.DataSource = dtTukinamiTo
                Me.ddlTukinamiTo2.DataValueField = "code"
                Me.ddlTukinamiTo2.DataTextField = "meisyou"
                Me.ddlTukinamiTo2.DataBind()
            End If

            Me.ddlTukinamiTo2.Items.Insert(0, New ListItem("", "0"))
            Me.ddlTukinamiTo2.SelectedIndex = 0

        ElseIf Me.ddlTukinamiTo.SelectedItem.Value = "1" Then
            Me.ddlTukinamiTo2.Items.Clear()
            Me.ddlTukinamiTo2.Items.Insert(0, New ListItem("", "0"))
            Me.ddlTukinamiTo2.Items.Insert(1, New ListItem("2月", "1"))
            Me.ddlTukinamiTo2.Items.Insert(2, New ListItem("3月", "2"))
            Me.ddlTukinamiTo2.SelectedValue = 1

        ElseIf Me.ddlTukinamiTo.SelectedItem.Value = "2" Then
            Me.ddlTukinamiTo2.Items.Clear()
            Me.ddlTukinamiTo2.Items.Insert(0, New ListItem("", "0"))
            Me.ddlTukinamiTo2.Items.Insert(1, New ListItem("3月", "1"))
            Me.ddlTukinamiTo2.SelectedValue = 1

        ElseIf Me.ddlTukinamiTo.SelectedItem.Value = "3" Then
            Me.ddlTukinamiTo2.Items.Clear()

        Else
            '月別表示２をクリア
            Me.ddlTukinamiTo2.Items.Clear()
            Dim index As Integer = 1
            Me.ddlTukinamiTo2.Items.Insert(0, New ListItem("", "0"))
            For i As Integer = CType(selValue, Integer) + 1 To 15
                Dim j As Integer
                If i > 12 Then
                    j = i - 12
                Else
                    j = i
                End If

                Me.ddlTukinamiTo2.Items.Insert(index, New ListItem(j.ToString & "月", i))
                index = index + 1
                Me.ddlTukinamiTo2.SelectedValue = CType(selValue, Integer) + 1
            Next

        End If

    End Sub

    ''' <summary>
    ''' 月別表示TOのchange事件
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <history>2013/01/11　李宇(大連情報システム部)　新規作成</history>
    Protected Sub ddlTukinamiTo2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTukinamiTo2.SelectedIndexChanged

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)

        '月別表示２だけ選べるように
        If Me.ddlTukinamiTo.SelectedItem.Value = "0" AndAlso Me.ddlTukinamiTo2.SelectedItem.Value <> "0" Then
            common.SetShowMessage(Me, MSG048E)
            Me.ddlTukinamiTo.Focus()
        End If

    End Sub

#End Region

#Region "メソッド"

    ''' <summary>
    ''' 画面の他項目をクリアする
    ''' </summary>
    ''' <param name="popKbn">画面項目の区分</param>
    ''' <remarks></remarks>
    ''' <history>2013/01/17　李宇(大連情報システム部)　新規作成</history>
    Private Sub SetClear(ByVal popKbn As KakusyuSyukeiInquiry.popKbn)

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        Select Case popKbn

            '支店
            Case KakusyuSyukeiInquiry.popKbn.Shiten
                Me.tbxKameiten.Text = String.Empty
                Me.tbxEigyou.Text = String.Empty
                Me.tbxKeiretu.Text = String.Empty
                Me.tbxTourokuJgousya.Text = String.Empty
                Me.tbxUser.Text = String.Empty

                '営業マン
            Case KakusyuSyukeiInquiry.popKbn.User
                Me.tbxKameiten.Text = String.Empty
                Me.tbxEigyou.Text = String.Empty
                Me.tbxKeiretu.Text = String.Empty
                Me.tbxTourokuJgousya.Text = String.Empty
                Me.tbxSiten.Text = String.Empty

                '登録事業者
            Case KakusyuSyukeiInquiry.popKbn.TourokuJigyousya
                Me.tbxKameiten.Text = String.Empty
                Me.tbxEigyou.Text = String.Empty
                Me.tbxKeiretu.Text = String.Empty
                Me.tbxUser.Text = String.Empty
                Me.tbxSiten.Text = String.Empty

                '営業所
            Case KakusyuSyukeiInquiry.popKbn.Eigyou
                Me.tbxKameiten.Text = String.Empty
                Me.tbxTourokuJgousya.Text = String.Empty
                Me.tbxKeiretu.Text = String.Empty
                Me.tbxUser.Text = String.Empty
                Me.tbxSiten.Text = String.Empty

                '系列名
            Case KakusyuSyukeiInquiry.popKbn.Keiretu
                Me.tbxKameiten.Text = String.Empty
                Me.tbxEigyou.Text = String.Empty
                Me.tbxTourokuJgousya.Text = String.Empty
                Me.tbxUser.Text = String.Empty
                Me.tbxSiten.Text = String.Empty

                '都道府県
            Case KakusyuSyukeiInquiry.popKbn.Kameiten
                Me.tbxTourokuJgousya.Text = String.Empty
                Me.tbxEigyou.Text = String.Empty
                Me.tbxKeiretu.Text = String.Empty
                Me.tbxUser.Text = String.Empty
                Me.tbxSiten.Text = String.Empty

        End Select

    End Sub

    ''' <summary>
    ''' 明細部分のBound
    ''' </summary>
    ''' <param name="dtSource">Data.DataTable</param>
    ''' <remarks></remarks>
    ''' <history>2013/01/10　李宇(大連情報システム部)　新規作成</history>
    Private Sub BoundGridView(ByVal dtSource As Data.DataTable)

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        Dim dt As New Data.DataTable
        Dim dc As Data.DataColumn
        Dim dr As Data.DataRow

        dc = New Data.DataColumn("title")
        dt.Columns.Add(dc)

        Dim strTitle As String = String.Empty

        For i As Integer = 0 To dtSource.Rows.Count - 1
            If Not strTitle.Equals(dtSource.Rows(i).Item("busyo_cd").ToString) Then
                strTitle = dtSource.Rows(i).Item("busyo_cd").ToString
                dr = dt.NewRow
                dr.Item("title") = strTitle
                dt.Rows.Add(dr)
            End If
        Next

        ViewState("dtSource") = dtSource

        Me.grdBodyLeft.DataSource = dt
        Me.grdBodyLeft.DataBind()
        Me.grdBodyRight.DataSource = dt
        Me.grdBodyRight.DataBind()

    End Sub


    ''' <summary>
    ''' 画面の集計 内容選択部分、検索ボタンを押下するかどうかについて、判断する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2013/01/10　李宇(大連情報システム部)　新規作成</para>
    ''' </history>
    Public Function CheckInput() As Boolean

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        '支店
        If Not Me.tbxSiten.Text.Equals(String.Empty) Then
            Dim intSitenDataCount As Integer
            intSitenDataCount = ShowPop(False, popKbn.Shiten)
            If intSitenDataCount = 0 Then
                If Not Me.tbxSiten.Text.Equals("ALL") Then
                    common.SetShowMessage(Me, MSG022E, "支店")
                End If
                Return False

            ElseIf intSitenDataCount > 1 Then
                common.SetShowMessage(Me, MSG062E, "支店")
                Return False

            Else
                Return True
            End If

            '都道府県
        ElseIf Not Me.tbxKameiten.Text.Equals(String.Empty) Then
            Dim intTodouhukenDataCount As Integer
            intTodouhukenDataCount = ShowPop(False, popKbn.Kameiten)
            If intTodouhukenDataCount = 0 Then
                If Not Me.tbxKameiten.Text.Equals("ALL") Then
                    common.SetShowMessage(Me, MSG022E, "都道府県")
                End If
                Return False

            ElseIf intTodouhukenDataCount > 1 Then
                common.SetShowMessage(Me, MSG062E, "都道府県")
                Return False

            Else
                Return True
            End If

            '営業所
        ElseIf Not Me.tbxEigyou.Text.Equals(String.Empty) Then
            Dim intEigyouDataCount As Integer
            intEigyouDataCount = ShowPop(False, popKbn.Eigyou)
            If intEigyouDataCount = 0 Then
                If Not Me.tbxEigyou.Text.Equals("ALL") Then
                    common.SetShowMessage(Me, MSG022E, "営業所")
                End If
                Return False

            ElseIf intEigyouDataCount > 1 Then
                common.SetShowMessage(Me, MSG062E, "営業所")
                Return False

            Else
                Return True
            End If

            '系列名
        ElseIf Not Me.tbxKeiretu.Text.Equals(String.Empty) Then
            Dim intKeiretuDataCount As Integer
            intKeiretuDataCount = ShowPop(False, popKbn.Keiretu)
            If intKeiretuDataCount = 0 Then
                If Not Me.tbxKeiretu.Text.Equals("ALL") Then
                    common.SetShowMessage(Me, MSG022E, "系列名")
                End If
                Return False

            ElseIf intKeiretuDataCount > 1 Then
                common.SetShowMessage(Me, MSG062E, "系列名")
                Return False

            Else
                Return True
            End If

            '営業マン
        ElseIf Not Me.tbxUser.Text.Equals(String.Empty) Then
            Dim intUserDataCount As Integer
            intUserDataCount = ShowPop(False, popKbn.User)
            If intUserDataCount = 0 Then
                If Not Me.tbxUser.Text.Equals("ALL") Then
                    common.SetShowMessage(Me, MSG022E, "営業マン")
                End If
                Return False

            ElseIf intUserDataCount > 1 Then
                common.SetShowMessage(Me, MSG062E, "営業マン")
                Return False

            Else
                Return True
            End If

            '登録事業者
        ElseIf Not Me.tbxTourokuJgousya.Text.Equals(String.Empty) Then
            Dim intTourokuJgousyaDataCount As Integer
            intTourokuJgousyaDataCount = ShowPop(False, popKbn.TourokuJigyousya)
            If intTourokuJgousyaDataCount = 0 Then
                If Not Me.tbxTourokuJgousya.Text.Equals("ALL") Then
                    common.SetShowMessage(Me, MSG022E, "登録事業者")
                End If
                Return False

            ElseIf intTourokuJgousyaDataCount > 1 Then
                common.SetShowMessage(Me, MSG062E, "登録事業者")
                Return False

            Else
                Return True
            End If
        End If

    End Function

    ''' <summary>
    ''' 初期表示時画面のセットする
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2012/12/05　李宇(大連情報システム部)　新規作成</history>
    Private Sub SetSyokiSeltuto()

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        '年度,期間From,月次FromDropdownListを取得する
        Call GetNendoListDate()

        '期間ToDropdownListを取得する
        Call GetKikanToListDate()

        '月次ToDropdownListを取得する
        Call GetTukinamiToListDate()

        '年度選択リストボックス活性化
        Me.ddlNendo.Enabled = True

        '期間の選択を活性化
        Me.ddlKikanFrom.Enabled = False
        Me.ddlKikanTo.Enabled = False

        '月次の選択を活性化
        Me.ddlTukinamiFrom.Enabled = False
        Me.ddlTukinamiTo.Enabled = False
        Me.ddlTukinamiTo2.Enabled = False

        '営業,特販,新規checkBoxを選択しています
        Me.chkEigyou.Checked = True
        Me.chkTokuhan.Checked = True
        Me.chkSinki.Checked = True
        'focusにセットする
        Me.tbxSiten.Focus()

    End Sub

    ''' <summary>
    ''' 月別セットする
    ''' </summary>
    ''' <history>2012/11/30　李宇(大連情報システム部)　新規作成</history>
    Public Sub GetTukinamiToListDate()

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        '月次FromDropdownListをクリアする
        Me.ddlTukinamiTo.Items.Clear()
        Me.ddlTukinamiTo2.Items.Clear()

        '月次ToDropdownListを取得する
        Dim dtTukinamiTo As Data.DataTable = kakusyuSyukeiInquiryBC.GetTukinamiListData()
        If dtTukinamiTo.Rows.Count > 0 Then
            Me.ddlTukinamiTo.DataSource = dtTukinamiTo
            Me.ddlTukinamiTo.DataValueField = "code"
            Me.ddlTukinamiTo.DataTextField = "meisyou"
            Me.ddlTukinamiTo.DataBind()

            Me.ddlTukinamiTo2.DataSource = dtTukinamiTo
            Me.ddlTukinamiTo2.DataValueField = "code"
            Me.ddlTukinamiTo2.DataTextField = "meisyou"
            Me.ddlTukinamiTo2.DataBind()
        End If

        Dim objCommonBC As New CommonBC
        Dim strSysTuki As Integer = Convert.ToDateTime(objCommonBC.SelSystemDate.Rows(0).Item(0).ToString).Month

        '月別表示は初期を未選択を設定
        Me.ddlTukinamiTo.Items.Insert(0, New ListItem("", "0"))
        Me.ddlTukinamiTo2.Items.Insert(0, New ListItem("", "0"))
        Me.ddlTukinamiTo.SelectedIndex = 1

        'ddlBeginTuki初期設定4月なので、システム月　5月　の場合のみ
        If (strSysTuki = 4) OrElse (strSysTuki = 5) Then
            'ddlEndTuki初期設定を空白
            Me.ddlTukinamiTo2.SelectedIndex = 0
        Else
            Me.ddlTukinamiTo2.SelectedValue = IIf(strSysTuki < 4, (strSysTuki + 12 - 1).ToString, (strSysTuki - 1).ToString)
        End If

    End Sub

    ''' <summary>
    ''' 共通POPUP
    ''' </summary>
    ''' <param name="popKbn">集計 内容選択の区分</param>
    ''' <history>
    ''' <para>2013/01/10　李宇(大連情報システム部)　新規作成</para>
    ''' </history>
    Private Function ShowPop(ByVal blnPop As Boolean, ByVal popKbn As KakusyuSyukeiInquiry.popKbn) As Integer

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, blnPop)

        Dim csScript As New StringBuilder
        Select Case popKbn
            '支店
            Case KakusyuSyukeiInquiry.popKbn.Shiten

                Dim SitenSearchBC As New SitenSearchBC
                Dim dtSiten As Data.DataTable
                dtSiten = SitenSearchBC.GetBusyoKanri("0", Me.tbxSiten.Text, False, False)

                ShowPop = dtSiten.Rows.Count
                If ShowPop = 1 Then
                    Me.tbxSiten.Text = dtSiten.Rows(0).Item("busyo_mei").ToString
                    Me.hidSitenCd.Value = dtSiten.Rows(0).Item("busyo_cd").ToString
                    Me.hidModouru.Value = "Siten"
                Else
                    'Me.hidModouru.Value = String.Empty
                    '複数の場合
                    If ShowPop > 1 Then
                        If Me.hidSitenCd.Value.ToString.Trim <> String.Empty Then
                            For i As Integer = 0 To dtSiten.Rows.Count - 1
                                If Me.hidSitenCd.Value.ToString.Trim = dtSiten.Rows(i).Item("busyo_cd").ToString() Then
                                    ShowPop = 1
                                    Exit For
                                End If
                            Next
                        End If
                    End If
                End If

                '営業マン
            Case KakusyuSyukeiInquiry.popKbn.User

                Dim EigyouManSearchBC As New EigyouManSearchBC
                Dim dtUserInfo As New Data.DataTable
                dtUserInfo = EigyouManSearchBC.GetUserInfo("0", "", Me.tbxUser.Text, False, False)

                ShowPop = dtUserInfo.Rows.Count
                If ShowPop = 1 Then
                    Me.tbxUser.Text = dtUserInfo.Rows(0).Item("DisplayName").ToString
                    Me.hidUserCd.Value = dtUserInfo.Rows(0).Item("login_user_id").ToString
                    Me.hidModouru.Value = "EigyouMan"
                Else
                    'Me.hidModouru.Value = String.Empty
                    '複数の場合
                    If ShowPop > 1 Then
                        If Me.hidUserCd.Value.ToString.Trim <> String.Empty Then
                            For i As Integer = 0 To dtUserInfo.Rows.Count - 1
                                If Me.hidUserCd.Value.ToString.Trim = dtUserInfo.Rows(i).Item("login_user_id").ToString() Then
                                    ShowPop = 1
                                    Exit For
                                End If
                            Next
                        End If
                    End If
                End If

                '登録事業者
            Case KakusyuSyukeiInquiry.popKbn.TourokuJigyousya
                Dim TourokuJigyousyaSearchBC As New TourokuJigyousyaSearchBC
                Dim dtTourokuJigyousya As New Data.DataTable
                dtTourokuJigyousya = TourokuJigyousyaSearchBC.GetTourokuJigyousya("0", "", Me.tbxTourokuJgousya.Text, False, False)

                ShowPop = dtTourokuJigyousya.Rows.Count
                If ShowPop = 1 Then
                    Me.tbxTourokuJgousya.Text = dtTourokuJigyousya.Rows(0).Item("kameiten_mei1").ToString
                    Me.hidTourokuJgousya.Value = dtTourokuJigyousya.Rows(0).Item("kameiten_cd").ToString
                    Me.hidModouru.Value = "Tourokusya"
                Else
                    'Me.hidModouru.Value = String.Empty
                    '複数の場合
                    If ShowPop > 1 Then
                        If Me.hidTourokuJgousya.Value.ToString.Trim <> String.Empty Then
                            For i As Integer = 0 To dtTourokuJigyousya.Rows.Count - 1
                                If Me.hidTourokuJgousya.Value.ToString.Trim = dtTourokuJigyousya.Rows(i).Item("kameiten_cd").ToString() Then
                                    ShowPop = 1
                                    Exit For
                                End If
                            Next
                        End If
                    End If
                End If

                '営業所
            Case KakusyuSyukeiInquiry.popKbn.Eigyou
                Dim EigyousyoSearchBC As New EigyousyoSearchBC
                Dim dtEigyousyo As New Data.DataTable
                dtEigyousyo = EigyousyoSearchBC.GetEigyousyoMei("0", Me.tbxEigyou.Text, False, False)

                ShowPop = dtEigyousyo.Rows.Count
                If ShowPop = 1 Then
                    Me.tbxEigyou.Text = dtEigyousyo.Rows(0).Item("busyo_mei").ToString
                    Me.hidEigyouCd.Value = dtEigyousyo.Rows(0).Item("busyo_cd").ToString
                    Me.hidModouru.Value = "Eigyousyo"
                Else
                    'Me.hidModouru.Value = String.Empty
                    '複数の場合
                    If ShowPop > 1 Then
                        If Me.hidEigyouCd.Value.ToString.Trim <> String.Empty Then
                            For i As Integer = 0 To dtEigyousyo.Rows.Count - 1
                                If Me.hidEigyouCd.Value.ToString.Trim = dtEigyousyo.Rows(i).Item("busyo_cd").ToString() Then
                                    ShowPop = 1
                                    Exit For
                                End If
                            Next
                        End If
                    End If
                End If

                '系列名
            Case KakusyuSyukeiInquiry.popKbn.Keiretu
                Dim KeiretuSearchBC As New KeiretuSearchBC
                Dim dtKeiretu As New Data.DataTable
                dtKeiretu = KeiretuSearchBC.GetKiretuJyouhou("0", "", Me.tbxKeiretu.Text, False, False)

                ShowPop = dtKeiretu.Rows.Count
                If ShowPop = 1 Then
                    Me.tbxKeiretu.Text = dtKeiretu.Rows(0).Item("keiretu_mei").ToString
                    Me.hidKeiretuMei.Value = dtKeiretu.Rows(0).Item("keiretu_cd").ToString
                    Me.hidModouru.Value = "Keiretu"
                Else
                    'Me.hidModouru.Value = String.Empty
                    '複数の場合
                    If ShowPop > 1 Then
                        If Me.hidKeiretuMei.Value.ToString.Trim <> String.Empty Then
                            For i As Integer = 0 To dtKeiretu.Rows.Count - 1
                                If Me.hidKeiretuMei.Value.ToString.Trim = dtKeiretu.Rows(i).Item("keiretu_cd").ToString() Then
                                    ShowPop = 1
                                    Exit For
                                End If
                            Next
                        End If
                    End If
                End If

                '都道府県
            Case KakusyuSyukeiInquiry.popKbn.Kameiten
                Dim TodoufukenSearchBC As New TodoufukenSearchBC
                Dim dtTodoufuken As New Data.DataTable
                dtTodoufuken = TodoufukenSearchBC.GetTodoufukenMei("0", Me.tbxKameiten.Text, False)

                ShowPop = dtTodoufuken.Rows.Count
                If ShowPop = 1 Then
                    Me.tbxKameiten.Text = dtTodoufuken.Rows(0).Item("todouhuken_mei").ToString
                    Me.hidKameitenCd.Value = dtTodoufuken.Rows(0).Item("todouhuken_cd").ToString
                    Me.hidModouru.Value = "Todouhuken"
                Else
                    'Me.hidModouru.Value = String.Empty
                    '複数の場合
                    If ShowPop > 1 Then
                        If Me.hidKameitenCd.Value.ToString.Trim <> String.Empty Then
                            For i As Integer = 0 To dtTodoufuken.Rows.Count - 1
                                If Me.hidKameitenCd.Value.ToString.Trim = dtTodoufuken.Rows(i).Item("todouhuken_cd").ToString() Then
                                    ShowPop = 1
                                    Exit For
                                End If
                            Next
                        End If
                    End If
                End If
        End Select

    End Function

    ''' <summary>
    ''' 共通POPUP
    ''' </summary>
    ''' <param name="popKbn">POPUPの区分</param>
    ''' <history>2012/12/04　李宇(大連情報システム部)　新規作成</history>
    Private Sub ShoPop(ByVal popKbn As popKbn)

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, popKbn)

        Dim csScript As New StringBuilder
        Select Case popKbn
            Case KakusyuSyukeiInquiry.popKbn.Shiten
                csScript.AppendLine("window.open('./PopupSearch/SitenSearch.aspx?formName=" & Me.Form.ClientID & "&strSitenMei='+ escape($ID('" & Me.tbxSiten.ClientID & "').value)+'&field=" & Me.tbxSiten.ClientID & "'+'&fieldCd=" & Me.hidSitenCd.ClientID & "'+'&fieldBtn=" & Me.btnPopup.ClientID & "'+'&fieldHid=" & Me.hidModouru.ClientID & "', 'SitenSearch', 'menubar=no,toolbar=no,location=no,status=no,scrollbars=no,resizable=no,width=700,height=500,top=30,left=0');")
            Case KakusyuSyukeiInquiry.popKbn.Kameiten
                csScript.AppendLine("window.open('./PopupSearch/TodoufukenSearch.aspx?formName=" & Me.Form.ClientID & "&strTodouhukenMei='+ escape($ID('" & Me.tbxKameiten.ClientID & "').value)+'&field=" & Me.tbxKameiten.ClientID & "'+'&fieldCd=" & Me.hidKameitenCd.ClientID & "'+'&fieldBtn=" & Me.btnPopup.ClientID & "'+'&fieldHid=" & Me.hidModouru.ClientID & "', 'TodouhukenSearch', 'menubar=no,toolbar=no,location=no,status=no,scrollbars=no,resizable=no,width=700,height=500,top=30,left=0');")
            Case KakusyuSyukeiInquiry.popKbn.Eigyou
                csScript.AppendLine("window.open('./PopupSearch/EigyousyoSearch.aspx?formName=" & Me.Form.ClientID & "&strEigyousyoMei='+ escape($ID('" & Me.tbxEigyou.ClientID & "').value)+'&field=" & Me.tbxEigyou.ClientID & "'+'&fieldCd=" & Me.hidEigyouCd.ClientID & "'+'&fieldBtn=" & Me.btnPopup.ClientID & "'+'&fieldHid=" & Me.hidModouru.ClientID & "', 'EigyousyoSearch', 'menubar=no,toolbar=no,location=no,status=no,scrollbars=no,resizable=no,width=700,height=500,top=30,left=0');")
            Case KakusyuSyukeiInquiry.popKbn.Keiretu
                csScript.AppendLine("window.open('./PopupSearch/KeiretuSearch.aspx?formName=" & Me.Form.ClientID & "&strKeiretuCd='+ escape($ID('" & Me.tbxKeiretu.ClientID & "').value) +'&field=" & Me.tbxKeiretu.ClientID & "'+'&fieldMei=" & Me.hidKeiretuMei.ClientID & "'+'&fieldBtn=" & Me.btnPopup.ClientID & "'+'&fieldHid=" & Me.hidModouru.ClientID & "', 'KeiretuSearch', 'menubar=no,toolbar=no,location=no,status=no,scrollbars=no,resizable=no,width=700,height=500,top=30,left=0');")
            Case KakusyuSyukeiInquiry.popKbn.User
                csScript.AppendLine("window.open('./PopupSearch/EigyouManSearch.aspx?formName=" & Me.Form.ClientID & "&strEigyouManMei='+ escape($ID('" & Me.tbxUser.ClientID & "').value)+'&field=" & Me.tbxUser.ClientID & "'+'&fieldCd=" & Me.hidUserCd.ClientID & "'+'&fieldBtn=" & Me.btnPopup.ClientID & "'+'&fieldHid=" & Me.hidModouru.ClientID & "', 'EigyouManSearch', 'menubar=no,toolbar=no,location=no,status=no,scrollbars=no,resizable=no,width=700,height=500,top=30,left=0');")
            Case KakusyuSyukeiInquiry.popKbn.TourokuJigyousya
                csScript.AppendLine("window.open('./PopupSearch/TourokuJigyousyaSearch.aspx?formName=" & Me.Form.ClientID & "&strTourokuJigyousya='+ escape($ID('" & Me.tbxTourokuJgousya.ClientID & "').value)+'&field=" & Me.tbxTourokuJgousya.ClientID & "'+'&fieldCd=" & Me.hidTourokuJgousya.ClientID & "'+'&fieldBtn=" & Me.btnPopup.ClientID & "'+'&fieldHid=" & Me.hidModouru.ClientID & "', 'TourokuJigyousya', 'menubar=no,toolbar=no,location=no,status=no,scrollbars=no,resizable=no,width=850,height=500,top=30,left=0');")
        End Select

        'ページ応答で、クライアント側のスクリプト ブロックを出力します
        ClientScript.RegisterStartupScript(Me.GetType(), popKbn.ToString, csScript.ToString, True)

    End Sub

    ''' <summary>
    ''' 年度DropdownListを取得する,期間FromDropdownListを取得する,月次FromDropdownListを取得する
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2012/12/05　李宇(大連情報システム部)　新規作成</history>
    Private Sub GetNendoListDate()

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        '年度DropdownListをクリアする
        Me.ddlNendo.Items.Clear()
        '期間FromDropdownListをクリアする
        Me.ddlKikanFrom.Items.Clear()
        '月次FromDropdownListをクリアする
        Me.ddlTukinamiFrom.Items.Clear()

        Dim CommonBC As New CommonBC
        Dim objCommon As New Common
        Dim dtNendo As Data.DataTable = CommonBC.GetKeikakuNendoData()

        'システム年度を取得する
        Dim strSysNen As String = objCommon.GetSystemYear()

        If dtNendo.Rows.Count > 0 Then

            '年度LIST取得
            Me.ddlNendo.DataSource = dtNendo
            Me.ddlNendo.DataValueField = "code"
            Me.ddlNendo.DataTextField = "meisyou"
            Me.ddlNendo.DataBind()
            Me.ddlNendo.Items.Insert(0, New ListItem("", "0"))
            '初期表示はシステム年
            Me.ddlNendo.SelectedValue = strSysNen

            '期間の年度LIST取得
            Me.ddlKikanFrom.DataSource = dtNendo
            Me.ddlKikanFrom.DataValueField = "code"
            Me.ddlKikanFrom.DataTextField = "meisyou"
            Me.ddlKikanFrom.DataBind()
            Me.ddlKikanFrom.Items.Insert(0, New ListItem("", "0"))
            '初期表示はシステム年
            Me.ddlKikanFrom.SelectedValue = strSysNen

            '月次のの年度LIST取得
            Me.ddlTukinamiFrom.DataSource = dtNendo
            Me.ddlTukinamiFrom.DataValueField = "code"
            Me.ddlTukinamiFrom.DataTextField = "meisyou"
            Me.ddlTukinamiFrom.DataBind()
            Me.ddlTukinamiFrom.Items.Insert(0, New ListItem("", "0"))
            '初期表示はシステム年
            Me.ddlTukinamiFrom.SelectedValue = strSysNen

        End If

    End Sub

    ''' <summary>
    ''' 期間ToDropdownListを取得する
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2012/12/05　李宇(大連情報システム部)　新規作成</history>
    Private Sub GetKikanToListDate()

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        '期間ToDropdownListをクリアする
        Me.ddlKikanTo.Items.Clear()

        Me.ddlKikanTo.Items.Insert(0, New ListItem("上期", "0"))
        Me.ddlKikanTo.Items.Insert(1, New ListItem("下期", "1"))
        Me.ddlKikanTo.Items.Insert(2, New ListItem("四半期(4,5,6月)", "2"))
        Me.ddlKikanTo.Items.Insert(3, New ListItem("四半期(7,8,9月)", "3"))
        Me.ddlKikanTo.Items.Insert(4, New ListItem("四半期(10,11,12月)", "4"))
        Me.ddlKikanTo.Items.Insert(5, New ListItem("四半期(1,2,3月)", "5"))

    End Sub

    ''' <summary>
    '''  JS作成
    ''' </summary>
    ''' <history>2012/12/07　李宇(大連情報システム部)　新規作成</history>
    Protected Sub MakeJavaScript()

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                                MyMethod.GetCurrentMethod.Name)

        Dim csType As Type = Page.GetType()
        Dim csName As String = "setScript"
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("<script language='javascript' type='text/javascript'>")
            .AppendLine("function window.onload()")
            .AppendLine("{")
            .AppendLine("   //alert('1');")
            .AppendLine("   window.name ='" & CommonConstBC.uriageYojituKanri & "';")
            .AppendLine("   setMenuBgColor();")
            .AppendLine("   fncTextBoxTrue();")
            .AppendLine("}")
            .AppendLine("function fncTextBoxTrue()")
            .AppendLine("{")
            .AppendLine("//alert($ID('" & Me.hidModouru.ClientID & "').value);")
            .AppendLine("   switch($ID('" & Me.hidModouru.ClientID & "').value) ")
            .AppendLine("   {")
            '支店TextboxをClear
            .AppendLine("   case 'Siten':")
            .AppendLine("      if($ID('" & Me.tbxSiten.ClientID & "').value == '')")
            .AppendLine("      {")
            .AppendLine("         fncSetDisabled(false);")
            '支店のHiddenをクリアする
            .AppendLine("         $ID('" & Me.hidSitenCd.ClientID & "').value = '';")
            .AppendLine("         $ID('" & Me.hidModouru.ClientID & "').value = '';")
            .AppendLine("      }")
            .AppendLine("      else")
            .AppendLine("      {")
            .AppendLine("         fncSetDisabled(true);")
            .AppendLine("         $ID('" & Me.tbxSiten.ClientID & "').disabled = false;")
            .AppendLine("         $ID('" & Me.btnSiten.ClientID & "').disabled = false;")
            .AppendLine("      }")
            .AppendLine("         break;")

            '都道府県TextboxをClear
            .AppendLine("   case 'Todouhuken':")
            .AppendLine("      if($ID('" & Me.tbxKameiten.ClientID & "').value == '')")
            .AppendLine("      {")
            .AppendLine("         fncSetDisabled(false);")
            '支店のHiddenをクリアする
            .AppendLine("         $ID('" & Me.hidKameitenCd.ClientID & "').value = '';")
            .AppendLine("         $ID('" & Me.hidModouru.ClientID & "').value = '';")
            .AppendLine("      }")
            .AppendLine("      else")
            .AppendLine("      {")
            .AppendLine("         fncSetDisabled(true);")
            '都道府県
            .AppendLine("         $ID('" & Me.tbxKameiten.ClientID & "').disabled = false;")
            .AppendLine("         $ID('" & Me.btnKameiten.ClientID & "').disabled = false;")
            .AppendLine("      }")
            .AppendLine("         break;")

            '営業所TextboxをClear
            .AppendLine("   case 'Eigyousyo':")
            .AppendLine("      if($ID('" & Me.tbxEigyou.ClientID & "').value == '')")
            .AppendLine("      {")
            .AppendLine("         fncSetDisabled(false);")
            '営業所のHiddenをクリアする
            .AppendLine("         $ID('" & Me.hidEigyouCd.ClientID & "').value = '';")
            .AppendLine("         $ID('" & Me.hidModouru.ClientID & "').value = '';")
            .AppendLine("      }")
            .AppendLine("      else")
            .AppendLine("      {")
            .AppendLine("         fncSetDisabled(true);")
            '営業所
            .AppendLine("         $ID('" & Me.tbxEigyou.ClientID & "').disabled = false;")
            .AppendLine("         $ID('" & Me.btnEigyou.ClientID & "').disabled = false;")
            .AppendLine("      }")
            .AppendLine("         break;")

            '系列名TextboxをClear
            .AppendLine("   case 'Keiretu':")
            .AppendLine("      if($ID('" & Me.tbxKeiretu.ClientID & "').value == '')")
            .AppendLine("      {")
            .AppendLine("         fncSetDisabled(false);")
            .AppendLine("         $ID('" & Me.hidKeiretuMei.ClientID & "').value = '';")
            .AppendLine("         $ID('" & Me.hidModouru.ClientID & "').value = '';")
            .AppendLine("      }")
            .AppendLine("      else")
            .AppendLine("      {")
            .AppendLine("         fncSetDisabled(true);")
            '系列名
            .AppendLine("         $ID('" & Me.tbxKeiretu.ClientID & "').disabled = false;")
            .AppendLine("         $ID('" & Me.btnKeiretu.ClientID & "').disabled = false;")
            .AppendLine("      }")
            .AppendLine("         break;")

            '営業マンTextboxをClear
            .AppendLine("   case 'EigyouMan':")
            .AppendLine("      if($ID('" & Me.tbxUser.ClientID & "').value == '')")
            .AppendLine("      {")
            .AppendLine("         fncSetDisabled(false);")
            .AppendLine("         $ID('" & Me.hidUserCd.ClientID & "').value = '';")
            .AppendLine("         $ID('" & Me.hidModouru.ClientID & "').value = '';")
            .AppendLine("      }")
            .AppendLine("      else")
            .AppendLine("      {")
            .AppendLine("         fncSetDisabled(true);")
            '営業マン
            .AppendLine("         $ID('" & Me.tbxUser.ClientID & "').disabled = false;")
            .AppendLine("         $ID('" & Me.btnUser.ClientID & "').disabled = false;")
            .AppendLine("      }")
            .AppendLine("         break;")

            '登録事業者TextboxをClear
            .AppendLine("   case 'Tourokusya':")
            .AppendLine("      if($ID('" & Me.tbxTourokuJgousya.ClientID & "').value == '')")
            .AppendLine("      {")
            .AppendLine("         fncSetDisabled(false);")
            .AppendLine("         $ID('" & Me.hidTourokuJgousya.ClientID & "').value = '';")
            .AppendLine("         $ID('" & Me.hidModouru.ClientID & "').value = '';")
            .AppendLine("      }")
            .AppendLine("      else")
            .AppendLine("      {")
            .AppendLine("         fncSetDisabled(true);")
            '登録事業者
            .AppendLine("         $ID('" & Me.tbxTourokuJgousya.ClientID & "').disabled = false;")
            .AppendLine("         $ID('" & Me.btnTourokuJgousya.ClientID & "').disabled = false;")
            .AppendLine("      }")
            .AppendLine("         break;")
            .AppendLine("        default:")
            .AppendLine("           fncSetDisabled(false);")
            .AppendLine("   }")
            .AppendLine("  return false;")
            .AppendLine("}")

            '小さいは大きいに変更する
            .AppendLine("function fncToUpper(strTextBoxValue)")
            .AppendLine("   {")
            .AppendLine("      var str;")
            .AppendLine("      str = strTextBoxValue.value.toUpperCase();")
            .AppendLine("      strTextBoxValue.value = str")
            .AppendLine("   }")

            .AppendLine("function fncSetDisabled(flg)")
            .AppendLine("   {")
            '支店名
            .AppendLine("         $ID('" & Me.tbxSiten.ClientID & "').disabled = flg;")
            .AppendLine("         $ID('" & Me.btnSiten.ClientID & "').disabled = flg;")
            '都道府県
            .AppendLine("         $ID('" & Me.tbxKameiten.ClientID & "').disabled = flg;")
            .AppendLine("         $ID('" & Me.btnKameiten.ClientID & "').disabled = flg;")
            '営業所
            .AppendLine("         $ID('" & Me.tbxEigyou.ClientID & "').disabled = flg;")
            .AppendLine("         $ID('" & Me.btnEigyou.ClientID & "').disabled = flg;")
            '系列名
            .AppendLine("         $ID('" & Me.tbxKeiretu.ClientID & "').disabled = flg;")
            .AppendLine("         $ID('" & Me.btnKeiretu.ClientID & "').disabled = flg;")
            '営業マン
            .AppendLine("         $ID('" & Me.tbxUser.ClientID & "').disabled = flg;")
            .AppendLine("         $ID('" & Me.btnUser.ClientID & "').disabled = flg;")
            '登録事業者
            .AppendLine("         $ID('" & Me.tbxTourokuJgousya.ClientID & "').disabled = flg;")
            .AppendLine("         $ID('" & Me.btnTourokuJgousya.ClientID & "').disabled = flg;")
            .AppendLine("   }")

            .AppendLine("</script>")
        End With

        ClientScript.RegisterClientScriptBlock(csType, csName, csScript.ToString)

    End Sub

    ''' <summary>
    ''' 画面のJS EVENT設定
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2013/03/26 李宇 新規作成 </para>	
    ''' </history>	
    Private Sub SetJsEvent()
        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        '全社の計画値保存ボタン押下時
        Me.btnAllSave.OnClick = "BtnAllSave_Click()"

    End Sub

    ''' <summary>
    ''' 結果表示ボタン押下時の処理
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2013/03/26 李宇 新規作成 </para>	
    ''' </history>	
    Public Function BtnAllSave_Click() As Boolean

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        '年度LISTを選択しない時
        If ((Me.ddlNendo.Enabled = True) AndAlso (Me.ddlNendo.SelectedValue = 0)) OrElse _
           ((Me.ddlKikanFrom.Enabled = True) AndAlso (Me.ddlKikanFrom.SelectedValue = 0)) OrElse _
           ((Me.ddlTukinamiFrom.Enabled = True) AndAlso (Me.ddlTukinamiFrom.SelectedValue = 0)) Then
            'メッセージを表示する
            common.SetShowMessage(Me, MSG001E, "年度")
            '明細部を表示しない
            Me.divHead.Visible = False

            If Me.ddlNendo.Enabled = True Then
                Me.ddlNendo.Focus()
            ElseIf Me.ddlKikanFrom.Enabled = True Then
                Me.ddlKikanFrom.Focus()
            ElseIf Me.ddlTukinamiFrom.Enabled = True Then
                Me.ddlTukinamiFrom.Focus()
            End If

            '月別表示２だけ選べるように
        ElseIf (Me.ddlTukinamiFrom.Enabled = True) AndAlso (Me.ddlTukinamiTo.SelectedItem.Value = "0") Then
            common.SetShowMessage(Me, MSG048E)
            Me.ddlTukinamiTo.Focus()

            'FCチェックボックスと支店名の関係
        ElseIf (Me.chkFC.Checked = True) AndAlso (Me.tbxSiten.Text = String.Empty) Then

            Me.tbxSiten.Focus()

            'メッセージを表示する
            common.SetShowMessage(Me, MSG001E, "支店名")
            '明細部を表示しない
            Me.divHead.Visible = False

            '集計 内容選択一つも選択しない時
        ElseIf Me.tbxSiten.Text = String.Empty AndAlso _
               Me.tbxKameiten.Text = String.Empty AndAlso _
               Me.tbxEigyou.Text = String.Empty AndAlso _
               Me.tbxKeiretu.Text = String.Empty AndAlso _
               Me.tbxUser.Text = String.Empty AndAlso _
               Me.tbxTourokuJgousya.Text = String.Empty Then

            'メッセージを表示する
            common.SetShowMessage(Me, MSG058E)
            '明細部を表示しない
            Me.divHead.Visible = False

            '営業区分一つも選択しない時
        ElseIf Me.chkEigyou.Checked = False AndAlso _
                Me.chkFC.Checked = False AndAlso _
                Me.chkSinki.Checked = False AndAlso _
                Me.chkTokuhan.Checked = False Then
            'メッセージを表示する
            common.SetShowMessage(Me, MSG071E)

            '検索ボタンの押下判断する
        ElseIf Not Me.tbxSiten.Text.Equals(String.Empty) OrElse _
               Not Me.tbxKameiten.Text.Equals(String.Empty) OrElse _
               Not Me.tbxEigyou.Text.Equals(String.Empty) OrElse _
               Not Me.tbxKeiretu.Text.Equals(String.Empty) OrElse _
               Not Me.tbxUser.Text.Equals(String.Empty) OrElse _
               Not Me.tbxTourokuJgousya.Text.Equals(String.Empty) Then

            If CheckInput() = True OrElse _
               Me.tbxSiten.Text.Equals("ALL") OrElse _
               Me.tbxKameiten.Text.Equals("ALL") OrElse _
               Me.tbxEigyou.Text.Equals("ALL") OrElse _
               Me.tbxKeiretu.Text.Equals("ALL") OrElse _
               Me.tbxUser.Text.Equals("ALL") OrElse _
               Me.tbxTourokuJgousya.Text.Equals("ALL") Then

                '画面で選択したの項目
                If Not Me.tbxSiten.Text.Equals(String.Empty) Then
                    Me.lblSyukeiSentaku.Text = "支店"
                ElseIf Not Me.tbxKameiten.Text.Equals(String.Empty) Then
                    Me.lblSyukeiSentaku.Text = "都道府県"
                ElseIf Not Me.tbxEigyou.Text.Equals(String.Empty) Then
                    Me.lblSyukeiSentaku.Text = "営業所"
                ElseIf Not Me.tbxKeiretu.Text.Equals(String.Empty) Then
                    Me.lblSyukeiSentaku.Text = "系列名"
                ElseIf Not Me.tbxUser.Text.Equals(String.Empty) Then
                    Me.lblSyukeiSentaku.Text = "営業マン"
                ElseIf Not Me.tbxTourokuJgousya.Text.Equals(String.Empty) Then
                    Me.lblSyukeiSentaku.Text = "登録事業者"
                End If

                Dim dtMeisaiDate As Data.DataTable
                '支店
                Dim strSiten As String
                If Me.tbxSiten.Text.Equals("ALL") Then
                    strSiten = Me.tbxSiten.Text
                Else
                    strSiten = Me.hidSitenCd.Value
                End If
                '都道府県
                Dim strKameitenCd As String
                If Me.tbxKameiten.Text.Equals("ALL") Then
                    strKameitenCd = Me.tbxKameiten.Text
                Else
                    strKameitenCd = Me.hidKameitenCd.Value
                End If
                '営業所
                Dim strEigyouCd As String
                If Me.tbxEigyou.Text.Equals("ALL") Then
                    strEigyouCd = Me.tbxEigyou.Text
                Else
                    strEigyouCd = Me.hidEigyouCd.Value
                End If
                '系列名
                Dim strKeiretuMei As String
                If Me.tbxKeiretu.Text.Equals("ALL") Then
                    strKeiretuMei = Me.tbxKeiretu.Text
                Else
                    strKeiretuMei = Me.hidKeiretuMei.Value
                End If
                '営業マン
                Dim strUserCd As String
                If Me.tbxUser.Text.Equals("ALL") Then
                    strUserCd = Me.tbxUser.Text
                Else
                    strUserCd = Me.hidUserCd.Value
                End If
                '登録事業者
                Dim strTourokuJgousya As String
                If Me.tbxTourokuJgousya.Text.Equals("ALL") Then
                    strTourokuJgousya = Me.tbxTourokuJgousya.Text
                Else
                    strTourokuJgousya = Me.hidTourokuJgousya.Value
                End If

                '営業区分
                Dim strEigyouKbn As String = String.Empty '営業
                If Me.chkEigyou.Checked = True Then
                    strEigyouKbn = strEigyouKbn & ",1"
                End If
                If Me.chkTokuhan.Checked = True Then '特販
                    strEigyouKbn = strEigyouKbn & ",3"
                End If
                If Me.chkSinki.Checked = True Then '新規
                    strEigyouKbn = strEigyouKbn & ",2"
                End If
                If Me.chkFC.Checked = True Then 'FC
                    strEigyouKbn = strEigyouKbn & ",4"
                End If

                'データを取得する
                If (Me.ddlNendo.Enabled = True) Then

                    'FCのチェックボックスをチェックしない
                    If Me.chkFC.Checked = False Then

                        '年間:画面のデータを取得する
                        dtMeisaiDate = kakusyuSyukeiInquiryBC.GetKakusyuSyukeiData(strSiten, _
                                       strKameitenCd, strEigyouCd, strKeiretuMei, strUserCd, _
                                       strTourokuJgousya, Me.ddlNendo.SelectedValue, 1, 12, strEigyouKbn)

                    ElseIf (Me.chkFC.Checked = True) AndAlso _
                           (Me.chkEigyou.Checked = False AndAlso Me.chkSinki.Checked = False AndAlso Me.chkTokuhan.Checked = False) Then

                        'FCのチェックボックスをチェックする
                        '年間:画面のデータを取得する
                        dtMeisaiDate = kakusyuSyukeiInquiryBC.GetKakusyuSyukeiFCData(strSiten, _
                                                              Me.ddlNendo.SelectedValue, 1, 12)
                    Else
                        '集計
                        dtMeisaiDate = kakusyuSyukeiInquiryBC.GetKakusyuSyukeiSubeteData(strSiten, _
                                       Me.ddlNendo.SelectedValue, 1, 12, strEigyouKbn)

                    End If

                ElseIf (Me.ddlKikanTo.Enabled = True AndAlso Me.ddlKikanFrom.Enabled = True) Then

                    Dim intBegin As Integer
                    Dim intEnd As Integer
                    If Me.ddlKikanTo.SelectedItem.Text = "上期" Then
                        intBegin = 4
                        intEnd = 9
                    ElseIf Me.ddlKikanTo.SelectedItem.Text = "四半期(4,5,6月)" Then
                        intBegin = 4
                        intEnd = 6
                    ElseIf Me.ddlKikanTo.SelectedItem.Text = "四半期(7,8,9月)" Then
                        intBegin = 7
                        intEnd = 9
                    ElseIf Me.ddlKikanTo.SelectedItem.Text = "四半期(10,11,12月)" Then
                        intBegin = 10
                        intEnd = 12
                    ElseIf Me.ddlKikanTo.SelectedItem.Text = "四半期(1,2,3月)" Then
                        intBegin = 1
                        intEnd = 3
                    ElseIf Me.ddlKikanTo.SelectedItem.Text = "下期" Then
                        intBegin = 10
                        intEnd = 15
                    End If

                    'FCのチェックボックスをチェックしない
                    If Me.chkFC.Checked = False Then

                        '期間:画面のデータを取得する
                        dtMeisaiDate = kakusyuSyukeiInquiryBC.GetKakusyuSyukeiData(strSiten, _
                                       strKameitenCd, strEigyouCd, strKeiretuMei, strUserCd, _
                                       strTourokuJgousya, Me.ddlKikanFrom.SelectedValue, intBegin, intEnd, strEigyouKbn)

                    ElseIf (Me.chkFC.Checked = True) AndAlso _
                           (Me.chkEigyou.Checked = False AndAlso Me.chkSinki.Checked = False AndAlso Me.chkTokuhan.Checked = False) Then

                        'FCのチェックボックスをチェックする
                        '期間:画面のデータを取得する
                        dtMeisaiDate = kakusyuSyukeiInquiryBC.GetKakusyuSyukeiFCData(strSiten, _
                                        Me.ddlKikanFrom.SelectedValue, intBegin, intEnd)

                    Else
                        '集計
                        dtMeisaiDate = kakusyuSyukeiInquiryBC.GetKakusyuSyukeiSubeteData(strSiten, _
                                       Me.ddlKikanFrom.SelectedValue, intBegin, intEnd, strEigyouKbn)

                    End If

                ElseIf (Me.ddlTukinamiFrom.Enabled = True AndAlso Me.ddlTukinamiTo.Enabled = True AndAlso Me.ddlTukinamiTo2.Enabled = True) Then

                    Dim strTukinamiTo As String
                    If Me.ddlTukinamiTo.SelectedValue < 4 Then
                        strTukinamiTo = Me.ddlTukinamiTo.SelectedValue + 12
                    Else
                        strTukinamiTo = Me.ddlTukinamiTo.SelectedValue
                    End If

                    Dim strTukinamiTo2 As String
                    If Me.ddlTukinamiTo2.SelectedItem IsNot Nothing Then
                        If Me.ddlTukinamiTo2.SelectedItem.Text.Equals("2月") Then
                            strTukinamiTo2 = 14
                        ElseIf Me.ddlTukinamiTo2.SelectedItem.Text.Equals("3月") Then
                            strTukinamiTo2 = 15
                        Else
                            strTukinamiTo2 = Me.ddlTukinamiTo2.SelectedValue
                        End If
                    Else
                        strTukinamiTo2 = 15
                    End If


                    'FCのチェックボックスをチェックしない
                    If Me.chkFC.Checked = False Then

                        '月次:画面のデータを取得する
                        dtMeisaiDate = kakusyuSyukeiInquiryBC.GetKakusyuSyukeiData(strSiten, _
                                       strKameitenCd, strEigyouCd, strKeiretuMei, strUserCd, _
                                       strTourokuJgousya, Me.ddlTukinamiFrom.SelectedValue, _
                                       strTukinamiTo, strTukinamiTo2, strEigyouKbn)

                    ElseIf (Me.chkFC.Checked = True) AndAlso _
                           (Me.chkEigyou.Checked = False AndAlso Me.chkSinki.Checked = False AndAlso Me.chkTokuhan.Checked = False) Then

                        'FCのチェックボックスをチェックする
                        '月次:画面のデータを取得する
                        dtMeisaiDate = kakusyuSyukeiInquiryBC.GetKakusyuSyukeiFCData(strSiten, _
                                       Me.ddlTukinamiFrom.SelectedValue, strTukinamiTo, strTukinamiTo2)

                    Else
                        '集計
                        dtMeisaiDate = kakusyuSyukeiInquiryBC.GetKakusyuSyukeiSubeteData(strSiten, _
                                       Me.ddlTukinamiFrom.SelectedValue, strTukinamiTo, strTukinamiTo2, strEigyouKbn)
                    End If

                Else
                    dtMeisaiDate = Nothing
                End If

                If dtMeisaiDate.Rows.Count = 0 Then
                    'メッセージを表示する
                    common.SetShowMessage(Me, MSG067E)
                    '明細部を表示しない
                    Me.divHead.Visible = False
                Else

                    '明細部を表示する
                    Me.divHead.Visible = True
                    '画面で
                    Call BoundGridView(dtMeisaiDate)
                End If

            End If

        End If

        If Me.tbxSiten.Text.Equals("ALL") Then
            Me.hidModouru.Value = "Siten"
        ElseIf Me.tbxKameiten.Text.Equals("ALL") Then
            Me.hidModouru.Value = "Todouhuken"
        ElseIf Me.tbxEigyou.Text.Equals("ALL") Then
            Me.hidModouru.Value = "Eigyousyo"
        ElseIf Me.tbxKeiretu.Text.Equals("ALL") Then
            Me.hidModouru.Value = "Keiretu"
        ElseIf Me.tbxUser.Text.Equals("ALL") Then
            Me.hidModouru.Value = "EigyouMan"
        ElseIf Me.tbxTourokuJgousya.Text.Equals("ALL") Then
            Me.hidModouru.Value = "Tourokusya"
        End If

    End Function

#End Region

End Class

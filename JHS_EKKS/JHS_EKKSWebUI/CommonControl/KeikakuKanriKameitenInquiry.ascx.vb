Imports Lixil.JHS_EKKS.BizLogic
Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Messages = Lixil.JHS_EKKS.Utilities.CommonMessage
Imports System.Collections.Generic

Partial Class CommonControl_KeikakuKanriKameitenInquiry
    Inherits System.Web.UI.UserControl

    '加盟店コード
    Public strKameitenCd As String
    '年度
    Public strNendo As String
    '区分
    Public strKbn As String
    '区分
    Public strKbnMei As String

    Public WriteOnly Property GetKameitenCd() As String
        Set(ByVal KameitenCd As String)
            strKameitenCd = KameitenCd
        End Set
    End Property
    Public WriteOnly Property GetNendo() As String
        Set(ByVal Nendo As String)
            strNendo = Nendo
        End Set
    End Property
    Private CommonCheckFuc As New CommonCheck()
    Private bc As New Lixil.JHS_EKKS.BizLogic.KeikakuKameitenJyouhouInquiryBC

    ''' <summary>
    ''' 初期表示
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    ''' <history>2013/09/09　李宇(大連情報システム部)　新規作成</history>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)

        If Not IsPostBack Then

            'ドロップダウンリストのデータをセットする
            Call SetDdlDate()
            '画面のデータをセットする
            Call SetDate()

        End If

        'MakeJavaScript
        Call Me.MakeJs(Me.Parent.Page)

        '加盟店検索(統一法人ｺｰﾄﾞ)
        Me.btnKensakuTouitu.Attributes.Add("onClick", "fncShowKameitenPopup('1');return false;")
        Me.lblTouitu.Attributes.Add("readOnly", "true")
        '加盟店検索(法人ｺｰﾄﾞ)
        Me.btnKensakuHoujin.Attributes.Add("onClick", "fncShowKameitenPopup('2');return false;")
        Me.lblHoujin.Attributes.Add("readOnly", "true")
        '加盟店検索(法計画名寄コード)
        Me.btnKensakuYoriCd.Attributes.Add("onClick", "fncShowKameitenPopup('3');return false;")
        Me.lblYoriCd.Attributes.Add("readOnly", "true")
        Me.lblKeikakuEigyouSya.Attributes.Add("readOnly", "true")
        Me.lblSyozoku.Attributes.Add("readOnly", "true")

        'SDS開始年月_YYYY/MMチェック
        Me.tbxSDS.Attributes.Add("onBlur", "fncCheckNengetu(this);")

    End Sub

    ''' <summary>
    ''' [計画管理用_加盟店情報]クッリク時
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    ''' <history>2013/09/09　李宇(大連情報システム部)　新規作成</history>
    Protected Sub lnkTitle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkTitle.Click

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)

        '画面テータの設定
        Call Me.SetDate()

        If meisaiTbody1.Style.Item("display") = "none" Then
            '表示する
            meisaiTbody1.Style.Item("display") = ""
            btnTouroku.Style.Item("display") = ""
        Else
            '表示しない
            meisaiTbody1.Style.Item("display") = "none"
            btnTouroku.Style.Item("display") = "none"
        End If

    End Sub

    ''' <summary>
    ''' 「登録」ボタンをクリック
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    ''' <history>2013/09/10　李宇(大連情報システム部)　新規作成</history>
    Protected Sub btnTouroku_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTouroku.Click

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)
        Dim msg As String

        '入力チェック
        '計画用_加盟店名_禁止文字チェック
        If Not CommonCheckFuc.kinsiStrCheck(Me.tbxKeikakuyoKameitenMei.Text) Then
            Call Me.SetMessage(String.Format(Messages.MSG033E, "計画用_加盟店名"), Me.tbxKeikakuyoKameitenMei.ClientID)
            Return
        End If

        '計画用_加盟店名_バイト数チェック
        msg = CommonCheckFuc.CheckByte(Me.tbxKeikakuyoKameitenMei.Text, 80, "計画用_加盟店名", kbn.ZENKAKU)
        If Not msg.Equals(String.Empty) Then
            Call Me.SetMessage(String.Format(Messages.MSG073E, "計画用_加盟店名"), Me.tbxKeikakuyoKameitenMei.ClientID)
            Return
        End If

        '統一法人ｺｰﾄﾞ_半角英数チェック
        msg = CommonCheckFuc.ChkHankakuEisuuji(Me.tbxTouitu.Text, "統一法人ｺｰﾄﾞ")
        If Not msg.Equals(String.Empty) Then
            Call Me.SetMessage(msg, Me.tbxTouitu.ClientID)
            Return
        End If

        '法人ｺｰﾄﾞ_半角英数チェック
        msg = CommonCheckFuc.ChkHankakuEisuuji(Me.tbxHoujin.Text, "法人ｺｰﾄﾞ")
        If Not msg.Equals(String.Empty) Then
            Call Me.SetMessage(msg, Me.tbxHoujin.ClientID)
            Return
        End If

        '計画名寄ｺｰﾄﾞ_半角英数チェック
        msg = CommonCheckFuc.ChkHankakuEisuuji(Me.tbxYoriCd.Text, "計画名寄ｺｰﾄﾞ")
        If Not msg.Equals(String.Empty) Then
            Call Me.SetMessage(msg, Me.tbxYoriCd.ClientID)
            Return
        End If

        '計画用_年間棟数_半角数チェック
        If Not Me.tbxNenKan.Text.Equals(String.Empty) Then
            msg = CommonCheckFuc.CheckHankaku(Me.tbxNenKan.Text, "計画用_年間棟数")
            If Not msg.Equals(String.Empty) Then
                Call Me.SetMessage(msg, Me.tbxNenKan.ClientID)
                Return
            End If
        End If

        '計画用_年間棟数_バイト数チェック
        msg = CommonCheckFuc.CheckByte(Me.tbxNenKan.Text, 5, "計画用_年間棟数", kbn.HANKAKU)
        If Not msg.Equals(String.Empty) Then
            Call Me.SetMessage(String.Format(Messages.MSG073E, "計画用_年間棟数"), Me.tbxNenKan.ClientID)
            Return
        End If

        '計画時_営業担当者_半角英数チェック
        msg = CommonCheckFuc.ChkHankakuEisuuji(Me.tbxEigyouSya.Text, "計画時_営業担当者")
        If Not msg.Equals(String.Empty) Then
            Call Me.SetMessage(msg, Me.tbxEigyouSya.ClientID)
            Return
        End If

        'IDと名称が一致しない項目のチェック
        '①DBにより、一致の判斷
        '営業担当者名の取得
        Dim strTantousyaMei As String = String.Empty
        If Not (Me.tbxEigyouSya.Text.Equals(String.Empty)) Then
            Dim dtTantousyaMei As Data.DataTable = bc.GetEigyouTantousyaMei(Me.tbxEigyouSya.Text)
            If dtTantousyaMei.Rows.Count > 0 Then
                strTantousyaMei = dtTantousyaMei.Rows(0).Item(0).ToString
                If Not (Me.lblKeikakuEigyouSya.Text.Equals(String.Empty)) Then
                    If Not (Me.lblKeikakuEigyouSya.Text.Equals(strTantousyaMei)) Then
                        Call Me.SetMessage(Messages.MSG082E, Me.tbxEigyouSya.ClientID)
                        Return
                    End If
                End If
            Else
                strTantousyaMei = String.Empty
                Me.lblKeikakuEigyouSya.Text = String.Empty
                Call Me.SetMessage(Messages.MSG081E, Me.tbxEigyouSya.ClientID)
                Return
            End If
        End If

        '加盟店属性6_禁止文字チェック
        If Not CommonCheckFuc.kinsiStrCheck(Me.tbxSokusei6.Text) Then
            Call Me.SetMessage(String.Format(Messages.MSG033E, "加盟店属性6"), Me.tbxSokusei6.ClientID)
            Return
        End If

        '加盟店属性6_バイト数チェック
        msg = CommonCheckFuc.CheckByte(Me.tbxSokusei6.Text, 40, "加盟店属性6", kbn.ZENKAKU)
        If Not msg.Equals(String.Empty) Then
            Call Me.SetMessage(String.Format(Messages.MSG073E, "加盟店属性6"), Me.tbxSokusei6.ClientID)
            Return
        End If

        '他の端末で更新されたチェック
        If Not Me.CheckUpdTime() Then
            Return
        End If

        '更新者
        Dim strKousinsya As String = Me.GetLogin()

        '画面の加盟店ｺｰﾄﾞが計画管理_加盟店情報マスタ.加盟店ｺｰﾄﾞに存在するかどうかを判断する
        Dim bloSonzai As Data.DataTable
        bloSonzai = bc.GetCount(strKameitenCd)

        '計画用_管轄支店
        Dim strSitenn As String = Me.ddlKamkatuSiten.SelectedItem.Text
        Dim strMei As String
        If Not strSitenn.Equals(String.Empty) Then
            strMei = Split(strSitenn, "：")(1)
        Else
            strMei = String.Empty
        End If

        If bloSonzai.Rows(0).Item(0) = 1 Then
            '更新処理
            Dim bloKousin As Boolean
            bloKousin = bc.GetUpdMKeikakuKameitenInfo(strKameitenCd, strKbn, strKbnMei, _
                                                      Me.tbxKeikakuyoKameitenMei.Text, Me.ddlGyoutai.SelectedValue, _
                                                      Me.tbxTouitu.Text, Me.tbxHoujin.Text, Me.tbxYoriCd.Text, Me.tbxNenKan.Text, _
                                                      Me.tbxEigyouSya.Text, strTantousyaMei, Me.ddlKeikauEigyouKbn.SelectedValue, _
                                                      Me.ddlKamkatuSiten.SelectedValue, strMei, Me.tbxSDS.Text, Me.ddlSokusei1.SelectedValue, Me.ddlSokusei2.SelectedValue, _
                                                      Me.ddlSokusei3.SelectedValue, Me.ddlSokusei4.SelectedValue, Me.ddlSokusei5.SelectedValue, Me.tbxSokusei6.Text, strKousinsya)

            If bloKousin = True Then
                '更新日の取得
                Call Me.SetDate()
                '成功
                Call Me.SetMessage(String.Format(Messages.MSG077E, "計画管理_加盟店情報　更新"), String.Empty)
            Else
                '失敗
                Call Me.SetMessage(String.Format(Messages.MSG078E, "計画管理_加盟店情報　更新"), String.Empty)
            End If
        Else
            '追加処理
            Dim bloTuika As Boolean
            bloTuika = bc.GetInsMKeikakuKameitenInfo(strKameitenCd, strKbn, strKbnMei, _
                                                     Me.tbxKeikakuyoKameitenMei.Text, Me.ddlGyoutai.SelectedValue, _
                                                     Me.tbxTouitu.Text, Me.tbxHoujin.Text, Me.tbxYoriCd.Text, Me.tbxNenKan.Text, _
                                                     Me.tbxEigyouSya.Text, strTantousyaMei, Me.ddlKeikauEigyouKbn.SelectedValue, _
                                                     Me.ddlKamkatuSiten.SelectedValue, strMei, Me.tbxSDS.Text, Me.ddlSokusei1.SelectedValue, Me.ddlSokusei2.SelectedValue, _
                                                     Me.ddlSokusei3.SelectedValue, Me.ddlSokusei4.SelectedValue, Me.ddlSokusei5.SelectedValue, Me.tbxSokusei6.Text, strKousinsya)
            If bloTuika = True Then
                '更新日の取得
                Call Me.SetDate()
                '成功
                Call Me.SetMessage(String.Format(Messages.MSG077E, "計画管理_加盟店情報　登録"), String.Empty)
            Else
                '失敗
                Call Me.SetMessage(String.Format(Messages.MSG078E, "計画管理_加盟店情報　登録"), String.Empty)
            End If

        End If

    End Sub

    ''' <summary>
    ''' 「計画時_営業担当者」の検索ボタンをクリック
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    ''' <history>2013/09/10　李宇(大連情報システム部)　新規作成</history>
    Protected Sub btnKensakuEigyouSya_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKensakuEigyouSya.Click

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)

        Dim objPage As Page
        objPage = Me.Parent.Page
        Dim csScript As New StringBuilder
        csScript.AppendLine("window.open('PopupSearch/EigyouManSearch.aspx?formName=aspnetForm&strEigyouManCd='+escape($ID('" & Me.tbxEigyouSya.ClientID & "').value)+'&strEigyouManMei=&field=" & Me.lblKeikakuEigyouSya.ClientID & "'+'&fieldCd=" & Me.tbxEigyouSya.ClientID & "', 'EigyouManSearch', 'menubar=no,toolbar=no,location=no,status=no,scrollbars=no,resizable=no,width=700,height=500,top=30,left=0');")
        'ページ応答で、クライアント側のスクリプト ブロックを出力します
        objPage.ClientScript.RegisterStartupScript(objPage.GetType, "EigyouManSearch", csScript.ToString, True)

    End Sub

    ''' <summary>
    ''' 画面のデータをセットする
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2013/09/09　李宇(大連情報システム部)　新規作成</history>
    Protected Sub SetDate()

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        '加盟店更新日取得
        Call Me.GetKousinhi()

        '加盟店情報を取得する
        Dim dtKameitenJyohuo As Data.DataTable
        dtKameitenJyohuo = bc.GetKameitenJyouhou(strKameitenCd, strNendo)
        If dtKameitenJyohuo.Rows.Count > 0 Then
            With dtKameitenJyohuo.Rows(0)
                '計画用_加盟店名
                Me.tbxKeikakuyoKameitenMei.Text = .Item("keikakuyou_kameitenmei").ToString
                '統一法人ｺｰﾄﾞ
                Me.tbxTouitu.Text = .Item("touitsu_houjin_cd").ToString
                Me.lblTouitu.Text = .Item("touitsuHoujinMei").ToString
                '法人ｺｰﾄﾞ
                Me.tbxHoujin.Text = .Item("houjin_cd").ToString
                Me.lblHoujin.Text = .Item("houjinMei").ToString
                '計画名寄コード
                Me.tbxYoriCd.Text = .Item("keikaku_nayose_cd").ToString
                Me.lblYoriCd.Text = .Item("keikakuNayoseMei").ToString
                '計画用_年間棟数
                Me.tbxNenKan.Text = .Item("keikakuyou_nenkan_tousuu").ToString
                '計画時_営業担当者
                Me.tbxEigyouSya.Text = .Item("keikakuji_eigyou_tantousya_id").ToString
                'Me.lblKeikakuEigyouSya.Text = .Item("keikakuji_eigyou_tantousya_mei").ToString
                Me.lblKeikakuEigyouSya.Text = .Item("DisplayName").ToString
                '所属
                Me.lblSyozoku.Text = .Item("syozoku").ToString
                'SDS開始年月
                Me.tbxSDS.Text = .Item("sds_kaisi_nengetu").ToString
                '加盟店属性6
                Me.tbxSokusei6.Text = .Item("kameiten_zokusei_6").ToString

                '業態
                Me.ddlGyoutai.SelectedValue = .Item("gyoutai").ToString
                '加盟店属性1
                Me.ddlSokusei1.SelectedValue = .Item("kameiten_zokusei_1").ToString
                '加盟店属性2
                Me.ddlSokusei2.SelectedValue = .Item("kameiten_zokusei_2").ToString
                '加盟店属性3
                Me.ddlSokusei3.SelectedValue = .Item("kameiten_zokusei_3").ToString
                '加盟店属性4
                Me.ddlSokusei4.SelectedValue = .Item("kameiten_zokusei_4").ToString
                '加盟店属性5
                Me.ddlSokusei5.SelectedValue = .Item("kameiten_zokusei_5").ToString
                '計画用_管轄支店
                Me.ddlKamkatuSiten.SelectedValue = .Item("keikakuyou_kannkatsu_siten").ToString
                ViewState("strKeikakuSitenMei") = .Item("keikakuyou_kannkatsu_siten_mei").ToString

                '計画用_営業区分
                Me.ddlKeikauEigyouKbn.SelectedValue = .Item("keikakuyou_eigyou_kbn").ToString

            End With

        End If

    End Sub

    ''' <summary>
    ''' 更新日を取得する
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2013/09/11　李宇(大連情報システム部)　新規作成</history>
    Protected Sub GetKousinhi()

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        '加盟店更新日取得
        Dim dtMaxDate As New Data.DataTable
        dtMaxDate = bc.GetKameitenMaxUpdTime(strKameitenCd)

        If dtMaxDate.Rows.Count > 0 Then
            If dtMaxDate.Rows(0).Item("maxtime").ToString.Equals(String.Empty) Then
                Me.hidMaxDate.Value = String.Empty
            Else
                Me.hidMaxDate.Value = Convert.ToDateTime(dtMaxDate.Rows(0).Item("maxtime")).ToString("yyyy/MM/dd HH:mm:ss")
            End If
        Else
            Me.hidMaxDate.Value = String.Empty
        End If

    End Sub

    ''' <summary>
    ''' 更新者を取得する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/10　李宇(大連情報システム部)　新規作成</history>
    Protected Function GetLogin() As String

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        Dim CommonCheck As New CommonCheck
        Dim LoginUserInfoList As New LoginUserInfoList
        Dim UserId As String = ""

        CommonCheck.CommonNinsyou(UserId, LoginUserInfoList, kegen.UserIdOnly)
        Return UserId

    End Function

    ''' <summary>
    ''' ドロップダウンリストのデータをセットする
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2013/09/09　李宇(大連情報システム部)　新規作成</history>
    Protected Sub SetDdlDate()

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        Dim dtDdlData As Data.DataTable
        Dim KeikakuKanriKameitenKensakuSyoukaiInquiryBC As New Lixil.JHS_EKKS.BizLogic.KeikakuKanriKameitenKensakuSyoukaiInquiryBC

        '業態
        Me.ddlGyoutai.Items.Clear()
        dtDdlData = KeikakuKanriKameitenKensakuSyoukaiInquiryBC.GetMeisyouInfo("20")
        Me.ddlGyoutai.DataValueField = "code"
        Me.ddlGyoutai.DataTextField = "meisyou"
        Me.ddlGyoutai.DataSource = dtDdlData
        Me.ddlGyoutai.DataBind()
        Me.ddlGyoutai.Items.Insert(0, New ListItem(String.Empty, String.Empty))

        '計画用_管轄支店
        Me.ddlKamkatuSiten.Items.Clear()
        dtDdlData = KeikakuKanriKameitenKensakuSyoukaiInquiryBC.GetSitenInfo()

        Me.ddlKamkatuSiten.DataValueField = "busyo_cd"
        Me.ddlKamkatuSiten.DataTextField = "busyo_mei"
        Me.ddlKamkatuSiten.DataSource = dtDdlData
        Me.ddlKamkatuSiten.DataBind()
        Me.ddlKamkatuSiten.Items.Insert(0, New ListItem(String.Empty, String.Empty))

        '計画用_営業区分
        Me.ddlKeikauEigyouKbn.Items.Clear()
        dtDdlData = KeikakuKanriKameitenKensakuSyoukaiInquiryBC.GetMeisyouInfo("05")
        Me.ddlKeikauEigyouKbn.DataValueField = "code"
        Me.ddlKeikauEigyouKbn.DataTextField = "meisyou"
        Me.ddlKeikauEigyouKbn.DataSource = dtDdlData
        Me.ddlKeikauEigyouKbn.DataBind()
        Me.ddlKeikauEigyouKbn.Items.Insert(0, New ListItem(String.Empty, String.Empty))

        '加盟店属性1
        Me.ddlSokusei1.Items.Clear()
        dtDdlData = KeikakuKanriKameitenKensakuSyoukaiInquiryBC.GetMeisyouInfo("21")
        Me.ddlSokusei1.DataValueField = "code"
        Me.ddlSokusei1.DataTextField = "meisyou"
        Me.ddlSokusei1.DataSource = dtDdlData
        Me.ddlSokusei1.DataBind()
        Me.ddlSokusei1.Items.Insert(0, New ListItem(String.Empty, String.Empty))

        '加盟店属性2
        Me.ddlSokusei2.Items.Clear()
        dtDdlData = KeikakuKanriKameitenKensakuSyoukaiInquiryBC.GetMeisyouInfo("22")
        Me.ddlSokusei2.DataValueField = "code"
        Me.ddlSokusei2.DataTextField = "meisyou"
        Me.ddlSokusei2.DataSource = dtDdlData
        Me.ddlSokusei2.DataBind()
        Me.ddlSokusei2.Items.Insert(0, New ListItem(String.Empty, String.Empty))

        '加盟店属性3
        Me.ddlSokusei3.Items.Clear()
        dtDdlData = KeikakuKanriKameitenKensakuSyoukaiInquiryBC.GetMeisyouInfo("23")
        Me.ddlSokusei3.DataValueField = "code"
        Me.ddlSokusei3.DataTextField = "meisyou"
        Me.ddlSokusei3.DataSource = dtDdlData
        Me.ddlSokusei3.DataBind()
        Me.ddlSokusei3.Items.Insert(0, New ListItem(String.Empty, String.Empty))

        '加盟店属性4
        Me.ddlSokusei4.Items.Clear()
        dtDdlData = KeikakuKanriKameitenKensakuSyoukaiInquiryBC.GetKakutyouMeisyouInfo("21")
        Me.ddlSokusei4.DataValueField = "code"
        Me.ddlSokusei4.DataTextField = "meisyou"
        Me.ddlSokusei4.DataSource = dtDdlData
        Me.ddlSokusei4.DataBind()
        Me.ddlSokusei4.Items.Insert(0, New ListItem(String.Empty, String.Empty))

        '加盟店属性5
        Me.ddlSokusei5.Items.Clear()
        dtDdlData = KeikakuKanriKameitenKensakuSyoukaiInquiryBC.GetKakutyouMeisyouInfo("22")
        Me.ddlSokusei5.DataValueField = "code"
        Me.ddlSokusei5.DataTextField = "meisyou"
        Me.ddlSokusei5.DataSource = dtDdlData
        Me.ddlSokusei5.DataBind()
        Me.ddlSokusei5.Items.Insert(0, New ListItem(String.Empty, String.Empty))

    End Sub

    ''' <summary>
    ''' 他の端末で更新されたチェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckUpdTime() As Boolean

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        '加盟店備考更新日取得
        Dim strMaxUpdTime As String
        Dim strUserId As String
        Dim dtMaxDate As New Data.DataTable
        dtMaxDate = bc.GetKameitenMaxUpdTime(strKameitenCd)

        If dtMaxDate.Rows.Count > 0 Then
            If dtMaxDate.Rows(0).Item("maxtime").ToString.Equals(String.Empty) Then
                strMaxUpdTime = String.Empty
                strUserId = String.Empty
            Else
                strMaxUpdTime = Convert.ToDateTime(dtMaxDate.Rows(0).Item("maxtime")).ToString("yyyy/MM/dd HH:mm:ss")
                strUserId = dtMaxDate.Rows(0).Item("theuser").ToString()
            End If
        Else
            strMaxUpdTime = String.Empty
            strUserId = String.Empty
        End If

        '他の端末で更新されたチェック
        If Me.hidMaxDate.Value <> String.Empty AndAlso strMaxUpdTime <> String.Empty Then
            If Me.hidMaxDate.Value < strMaxUpdTime Then
                Call Me.SetMessage(String.Format(Messages.MSG079E, strUserId, strMaxUpdTime), "")
                Return False
            End If
        Else
            If Me.hidMaxDate.Value = String.Empty AndAlso strMaxUpdTime <> String.Empty Then
                Call Me.SetMessage(String.Format(Messages.MSG079E, strUserId, strMaxUpdTime), "")
                Return False
            End If
        End If

        Return True

    End Function

    ''' <summary>
    ''' エラーメッセージを設定する
    ''' </summary>
    ''' <history>2013/09/10　李宇(大連情報システム部)　新規作成</history>
    Private Sub SetMessage(ByVal strErrorMessage As String, ByVal strErrorItemId As String)

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, strErrorMessage, strErrorItemId)

        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("<script language='javascript' type='text/javascript'>  ")
            .AppendLine("   strMessage='" & strErrorMessage & "';")
            .AppendLine("   strItemId='" & strErrorItemId & "';")
            .AppendLine("</script>")
        End With

        'ページ応答で、クライアント側のスクリプト ブロックを出力します
        Me.Parent.Page.ClientScript.RegisterStartupScript(Me.GetType, "SetErrorMessage", csScript.ToString)

    End Sub

    ''' <summary>
    ''' MakeJavaScript
    ''' </summary>
    ''' <param name="objPage">Page</param>
    ''' <remarks></remarks>
    ''' <history>2013/09/09　李宇(大連情報システム部)　新規作成</history>
    Private Sub MakeJs(ByVal objPage As Page)

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        Dim csName As String = "setScript"
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("<script language='javascript' type='text/javascript'>  ")
            '加盟店検索popup
            .AppendLine("   function fncShowKameitenPopup(kbn)")
            .AppendLine("   {")
            .AppendLine("       var strCdValue = '';")
            .AppendLine("       var strCdId = '';")
            .AppendLine("       var strMeiId = '';")
            .AppendLine("       if(kbn == '1')")
            .AppendLine("       {")
            .AppendLine("           strCdValue = $ID('" & Me.tbxTouitu.ClientID & "').value;")
            .AppendLine("           strCdId = '" & Me.tbxTouitu.ClientID & "';")
            .AppendLine("           strMeiId = '" & Me.lblTouitu.ClientID & "';")
            .AppendLine("       }")
            .AppendLine("       else")
            .AppendLine("           if(kbn == '2')")
            .AppendLine("           {")
            .AppendLine("               strCdValue = $ID('" & Me.tbxHoujin.ClientID & "').value;")
            .AppendLine("               strCdId = '" & Me.tbxHoujin.ClientID & "';")
            .AppendLine("               strMeiId = '" & Me.lblHoujin.ClientID & "';")
            .AppendLine("           }")
            .AppendLine("           else")
            .AppendLine("           {")
            .AppendLine("               strCdValue = $ID('" & Me.tbxYoriCd.ClientID & "').value;")
            .AppendLine("               strCdId = '" & Me.tbxYoriCd.ClientID & "';")
            .AppendLine("               strMeiId = '" & Me.lblYoriCd.ClientID & "';")
            .AppendLine("           }")
            .AppendLine("       var strYear = '" & strNendo & "';")
            .AppendLine("       window.open('PopupSearch/KeikakuKanriKameitenSearch.aspx?formName=aspnetForm&strKameitenCdValue='+ escape(strCdValue)+'&strKameitenCdId='+ strCdId +'&strKameitenMeiId=' + strMeiId +'&strYear='+ strYear, 'KeikakuKanriKameitenSearch', 'menubar=no,toolbar=no,location=no,status=no,scrollbars=no,resizable=no,width=700,height=500,top=30,left=0');")
            .AppendLine("       return false;")
            .AppendLine("   }")

            '日付チェック(yyyy/mm)
            .AppendLine("   function fncCheckNengetu(obj){")
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
            .AppendLine("           alert('" & String.Format(Messages.MSG008E, "SDS開始年月", "YYYY/MM").ToString & "');")
            .AppendLine("           obj.focus();")
            .AppendLine("   		obj.select();")
            .AppendLine("   		return false;")
            .AppendLine("   	}else{")
            .AppendLine("   		obj.value = val.substring(0,7);")
            .AppendLine("   	}")
            .AppendLine("   }")
            .AppendLine("</script>  ")
        End With

        objPage.ClientScript.RegisterClientScriptBlock(objPage.GetType, csName, csScript.ToString)

    End Sub

End Class

Imports Itis.Earth.BizLogic
Imports System.Transactions
Imports System.Data

Partial Public Class TyousaMitumorisyoSakuseiInquiry
    Inherits System.Web.UI.Page

    'インスタンス生成
    Private tyousaMitumorisyoSakuseiInquiryBC As New TyousaMitumorisyoSakuseiInquiryLogic
    Private fcw As Itis.Earth.BizLogic.FcwUtility
    Private kinouId As String = "TyousaMitsumorisyo"
    Private Const APOST As Char = ","c
    Private headFlg As Boolean = False
    Private Const SEP_STRING As String = "$$$"

    Enum PDFStatus As Integer

        OK = 0                              '正常
        IOException = 1                     'エラー(他のユーザがファイルを開いている)
        UnauthorizedAccessException = 2     'エラー(ファイルを作成するパスが不正)
        NoData = 3                          '対象のデータが取得できません。

    End Enum

    ''' <summary>
    ''' 初期表示
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    ''' <history>2013/11/12 李宇(大連情報システム部) 新規作成</history>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'MakeJavaScript
        Call MakeJavaScript()

        If Not IsPostBack Then

            If Request("sendSearchTerms") <> "" Then
                Dim arrSearchTerm() As String = Split(Request("sendSearchTerms"), SEP_STRING)
                '区分
                Me.lblKbn.Text = arrSearchTerm(0)
                '物件番号
                Me.lblBukkenNo.Text = arrSearchTerm(1)
            End If

            '初期画面表示
            Call SetGamenHyouji()

        End If

        '「見積書作成」
        Me.btnMitumorisyoSakusei.Attributes.Add("onclick", "if(!fncHiltusuNyuryokuCheck()){return false;};")
        '「閉める」ボタン
        Me.btnCloseWin.Attributes.Add("onclick", "window.close();")
        '「クリア」ボタン
        Me.clearWin.Attributes.Add("onclick", "fncClear();return false;")
    End Sub

    ''' <summary>
    ''' 「当日」ボタンを押下する
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnToday_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnToday.Click

        Me.tbxSakuseiDate.Text = TyousaMitumorisyoSakuseiInquiryBC.GetSysTime().ToString("yyyy/MM/dd")

    End Sub

    ''' <summary>
    ''' 「見積書選択」ボタンを押下する
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    ''' <history>2013/11/12 李宇(大連情報システム部) 新規作成</history>
    Private Sub btnMitumorisyoSakusei_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMitumorisyoSakusei.Click

        Dim user_info As New BizLogic.LoginUserInfo
        Dim ninsyou As New BizLogic.Ninsyou()
        Dim jBn As New Jiban '地盤画面共通クラス
        jBn.userAuth(user_info) 'ユーザー基本認証

        If user_info Is Nothing Then
            Call ShowMessage(Messages.Instance.MSG2024E)
            Exit Sub
        Else
            ViewState("UserId") = user_info.LoginUserId '担当者ID
            ViewState("UserName") = Split(user_info.Name(), "(")(0) '担当者名
        End If

        '登録者IDが担当者紐付け承認印管理マスタの担当者IDに存在しない場合
        Dim dtTantousya As Data.DataTable
        dtTantousya = TyousaMitumorisyoSakuseiInquiryBC.GetSonzaiHandan(ViewState("UserId").ToString)
        If dtTantousya.Rows(0).Item(0).ToString = "0" Then
            Call ShowMessage("担当者印が存在しないため見積作成できません\r\n管理者へご連絡下さい")
            Exit Sub
        End If

        '調査見積書作成管理テーブルに登録する
        Dim strKbn As String '区分
        strKbn = Me.lblKbn.Text

        Dim strHosyousyoNo As String '保証書NO
        strHosyousyoNo = Me.lblBukkenNo.Text

        Dim inSyouhizei As Integer '消費税選択
        If rdoZeikomi.Checked = True Then
            inSyouhizei = 1 '税込み
        Else
            inSyouhizei = 0 '税抜き
        End If

        Dim inMooru As Integer 'モール展開
        If rdoSuru.Checked = True Then
            inMooru = 1 'する
        Else
            inMooru = 0 'しない
        End If

        Dim inHyoujiJyuusyo As Integer '表示住所_管理No
        inHyoujiJyuusyo = Me.ddlHyoujiJyuusyo.SelectedValue

        Dim strSyouninSyaId As String '承認者ID
        strSyouninSyaId = Me.ddlSyouniSya.SelectedItem.Value

        Dim strSyouninSyaMei As String '承認者名
        strSyouninSyaMei = Me.ddlSyouniSya.SelectedItem.Text

        Dim strSakuseiDate As String '調査見積書作成日
        strSakuseiDate = Convert.ToDateTime(Me.tbxSakuseiDate.Text.Trim).ToString("yyyy/MM/dd")
        Dim strIraiTantousyaMei As String '調査見積書_依頼担当者
        strIraiTantousyaMei = Me.tbxIraiTantousya.Text.Trim

        'Dim dtOne As Data.DataTable
        'dtOne = TyousaMitumorisyoSakuseiInquiryBC.GetKihonInfoOne(Me.lblKbn.Text, _
        '                                                          Me.lblBukkenNo.Text)
        'Dim dtTwo As Data.DataTable
        'dtTwo = TyousaMitumorisyoSakuseiInquiryBC.GetKihonInfoTwo(Me.lblKbn.Text, _
        '                                                          Me.lblBukkenNo.Text)
        ''該当データがない場合
        'If (dtOne.Rows.Count = 0) OrElse (dtTwo.Rows.Count = 0) Then
        '    Call Me.ShowMessage(Messages.Instance.MSG020E)
        '    Return
        'End If

        Using scope As New TransactionScope(TransactionScopeOption.RequiresNew)
            '見積書の存在を判斷する
            Dim dtMitumoriCnt As Data.DataTable
            dtMitumoriCnt = TyousaMitumorisyoSakuseiInquiryBC.GetMitumoriCount(Me.lblKbn.Text, _
                                                                               Me.lblBukkenNo.Text)
            '見積作成回数
            Dim inMitumori As Integer
            inMitumori = Convert.ToInt32(Me.lblMitumorisyoSakuseiKaisuu.Text) + 1
            If dtMitumoriCnt.Rows(0).Item(0).ToString = "1" Then
                '存在するUPDATE_回数、
                TyousaMitumorisyoSakuseiInquiryBC.GetUpdMitumoriKaisu(Me.lblKbn.Text, _
                                                                      Me.lblBukkenNo.Text, _
                                                                      inMitumori, _
                                                                      inSyouhizei, _
                                                                      inMooru, _
                                                                      inHyoujiJyuusyo, _
                                                                      ViewState("UserId").ToString, _
                                                                      ViewState("UserName").ToString, _
                                                                      strSyouninSyaId, _
                                                                      strSyouninSyaMei, _
                                                                      strSakuseiDate, _
                                                                      strIraiTantousyaMei)
            Else
                '存在しなしINSERT
                TyousaMitumorisyoSakuseiInquiryBC.GetInsMitumoriKaisu(Me.lblKbn.Text, _
                                                                      Me.lblBukkenNo.Text, _
                                                                      inMitumori, _
                                                                      inSyouhizei, _
                                                                      inMooru, _
                                                                      inHyoujiJyuusyo, _
                                                                      ViewState("UserId").ToString, _
                                                                      ViewState("UserName").ToString, _
                                                                      strSyouninSyaId, _
                                                                      strSyouninSyaMei, _
                                                                      strSakuseiDate, _
                                                                      strIraiTantousyaMei)
            End If

            '帳票のデータを作成する
            Dim strMessage As String
            strMessage = Me.CreateFcwTyouhyouData()
            If strMessage.Equals(String.Empty) Then

                '見積書作成回数
                Call SetMitumorisakuseiKisu()
                '「モール展開」のセットを判斷する
                Call SetMoruHandan()

                scope.Complete()

            Else
                scope.Dispose()

                'メッセージ
                Context.Items("strFailureMsg") = strMessage
                Server.Transfer("CommonErr.aspx")
            End If
        End Using

    End Sub

    ''' <summary>
    ''' 初期画面表示をセットする
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2013/11/13 李宇(大連情報システム部) 新規作成</history>
    Private Sub SetGamenHyouji()

        '見積書作成回数
        Call SetMitumorisakuseiKisu()
        '表示住所 選択
        Call SetHyoujiJyuusyo()
        '承認者 選択
        Call SetSyounisya()
        '「モール展開」のセットを判斷する
        Call SetMoruHandan()
    End Sub

    ''' <summary>
    ''' 見積書作成回数
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2013/11/15 李宇(大連情報システム部) 新規作成</history>
    Private Sub SetMitumorisakuseiKisu()

        '見積書の存在を判斷する
        Dim dtMitumoriCnt As Data.DataTable
        dtMitumoriCnt = TyousaMitumorisyoSakuseiInquiryBC.GetMitumoriCount(Me.lblKbn.Text, _
                                                                           Me.lblBukkenNo.Text)

        '見積書作成回数
        Dim dtMitumorisyoKaisuu As Data.DataTable
        dtMitumorisyoKaisuu = TyousaMitumorisyoSakuseiInquiryBC.GetSakuseiKaisuu(Me.lblKbn.Text, _
                                                                                 Me.lblBukkenNo.Text)
        '見積書を作成されった
        If dtMitumoriCnt.Rows(0).Item(0).ToString = "1" Then
            If dtMitumorisyoKaisuu.Rows.Count > 0 Then
                Me.lblMitumorisyoSakuseiKaisuu.Text = dtMitumorisyoKaisuu.Rows(0).Item(0).ToString
            End If
        Else
            Me.lblMitumorisyoSakuseiKaisuu.Text = "0"
        End If
    End Sub

    ''' <summary>
    ''' 「表示住所 選択」をセットする
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2013/11/12 李宇(大連情報システム部) 新規作成</history>
    Private Sub SetHyoujiJyuusyo()

        '「表示住所 選択」を取得する
        Dim dtJyuusyo As Data.DataTable
        dtJyuusyo = TyousaMitumorisyoSakuseiInquiryBC.GetJyuusyoInfo()

        If dtJyuusyo.Rows.Count > 0 Then
            Me.ddlHyoujiJyuusyo.DataValueField = "kanri_no"
            Me.ddlHyoujiJyuusyo.DataTextField = "shiten_mei"
            Me.ddlHyoujiJyuusyo.DataSource = dtJyuusyo
            Me.ddlHyoujiJyuusyo.DataBind()
        End If

        '先頭行は空欄にセットする
        Me.ddlHyoujiJyuusyo.Items.Insert(0, New ListItem(String.Empty, "0"))
    End Sub

    ''' <summary>
    ''' 「承認者 選択」をセットする
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2013/11/12 李宇(大連情報システム部) 新規作成</history>
    Private Sub SetSyounisya()

        '「承認者」を取得する
        Dim dtSyouninsya As Data.DataTable
        dtSyouninsya = TyousaMitumorisyoSakuseiInquiryBC.GetSyouninSyaInfo()

        If dtSyouninsya.Rows.Count > 0 Then
            Me.ddlSyouniSya.DataValueField = "syouninsya_id"
            Me.ddlSyouniSya.DataTextField = "syouninsya_mei"
            Me.ddlSyouniSya.DataSource = dtSyouninsya
            Me.ddlSyouniSya.DataBind()
        End If

        '先頭行は空欄にセットする
        Me.ddlSyouniSya.Items.Insert(0, New ListItem(String.Empty, "0"))
    End Sub

    ''' <summary>
    ''' 「モール展開」のセットを判斷する
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2013/12/02 李宇(大連情報システム部) 新規作成</history>
    Private Sub SetMoruHandan()

        Me.hidMoru.Value = Me.rdoSuru.Enabled

        '「モール展開」のセットを判斷する
        '作成回数>"1"　AND　"する"を選択される
        Dim dtMoruHandan As Data.DataTable
        dtMoruHandan = TyousaMitumorisyoSakuseiInquiryBC.GetMoruHandan(Me.lblKbn.Text, Me.lblBukkenNo.Text)
        If dtMoruHandan.Rows.Count > 0 Then
            'dtMoruHandan.Rows(0).Item("mit_sakusei_kaisuu").ToString >= "1" AndAlso _
            If dtMoruHandan.Rows(0).Item("mooru_tenkai_flg").ToString = "1" Then
                Me.rdoSuru.Enabled = False
                Me.rdoSinai.Enabled = False

                Me.rdoSuru.Checked = True
                Me.rdoSinai.Checked = False
            Else
                Me.rdoSuru.Enabled = True
                Me.rdoSinai.Enabled = True
            End If
        End If
    End Sub

    ''' <summary>
    ''' 帳票のデータを作成する
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2013/11/18 李宇(大連情報システム部) 新規作成</history>
    Private Function CreateFcwTyouhyouData() As String

        'インスタンスの生成
        Dim sb As New StringBuilder
        Dim editDt_One As New DataTable
        Dim editDt_Two As New DataTable
        Dim editDt_Three As New DataTable
        Dim editDR_One As Data.DataRow = editDt_One.NewRow
        Dim editDR_Two As Data.DataRow = editDt_Two.NewRow
        Dim editDR_Three As Data.DataRow = editDt_Three.NewRow
        Dim sb_T As New StringBuilder

        Dim errMsg As String = String.Empty
        Dim seikyusyoNo As String       '請求書NO

        seikyusyoNo = Request("seino")
        fcw = New FcwUtility(Page, ViewState("UserId"), kinouId, ".fcx")

        'add feild
        Call Me.AddFeild_One(editDt_One)
        'add feild
        Call Me.AddFeild_Two(editDt_Two)
        'add feild
        Call Me.AddFeild_Three(editDt_Three)

        If Me.headFlg Then
        Else
            '[Head] 部作成
            sb.Append(fcw.CreateDatHeader(APOST.ToString))
            Me.headFlg = True
        End If

        '---------------(407662_消費税増税対応_Earth) 李宇追加 2014/02/17↓
        Dim strKbnZeinu As String = "1" '税抜
        Dim strZeirituZeinu As String = String.Empty  '税抜

        Dim strKbnZeikomi As String = "2" '税込
        Dim strZeirituZeikomi As String = String.Empty '税込

        Dim dtZeiritu As Data.DataTable
        If rdoZeikomi.Checked = True Then
            '税込
            dtZeiritu = TyousaMitumorisyoSakuseiInquiryBC.GetZeiritu(strKbnZeikomi)
            If dtZeiritu.Rows.Count > 0 Then
                strZeirituZeikomi = dtZeiritu.Rows(0).Item(0).ToString
            End If
        Else
            '税抜
            dtZeiritu = TyousaMitumorisyoSakuseiInquiryBC.GetZeiritu(strKbnZeinu)
            If dtZeiritu.Rows.Count > 0 Then
                strZeirituZeinu = dtZeiritu.Rows(0).Item(0).ToString
            End If
        End If
        '---------------(407662_消費税増税対応_Earth) 李宇追加 2014/02/17↑

        '"税込"
        Dim hyoudai As String = String.Empty
        Dim syoukei As String = String.Empty
        Dim syouhizei As String = String.Empty
        If rdoZeikomi.Checked = True Then
            hyoudai = "御見積合計金額(税込)"
            '---------------(407662_消費税増税対応_Earth) 李宇修正 2014/02/18↓
            'syoukei = "小" & Space(7) & "計"
            syoukei = "小計" & Space(1) & "(" & strZeirituZeikomi & ")"
            '---------------(407662_消費税増税対応_Earth) 李宇修正 2014/02/18↑
            syouhizei = Space(1)
            '"税抜"
        ElseIf rdoZeinu.Checked = True Then
            '2014.01.10 李宇修正(問題発見一覧表(407646).xlsのNo.12)
            'hyoudai = "御見積合計金額(税抜)"
            hyoudai = "御見積合計金額(税込)"
            '2014.01.10 李宇修正(問題発見一覧表(407646).xlsのNo.12)
            syoukei = Space(1) & "小計" & Space(1) & "(税抜)"
            '---------------(407662_消費税増税対応_Earth) 李宇修正 2014/02/18↓
            'syouhizei = Space(1) & "消" & Space(1) & "費" & Space(1) & "税"
            syouhizei = "消費税" & Space(1) & "(" & strZeirituZeinu & ")"
            '---------------(407662_消費税増税対応_Earth) 李宇修正 2014/02/18↑
        End If

        '担当者紐付け承認印管理マスタから【承認印】を取得する
        Dim strTantouIn As String
        Dim dtTantouIn As Data.DataTable
        dtTantouIn = TyousaMitumorisyoSakuseiInquiryBC.GetTantouIn(Me.lblKbn.Text, _
                                                                   Me.lblBukkenNo.Text)
        If dtTantouIn.Rows.Count > 0 Then
            strTantouIn = dtTantouIn.Rows(0).Item(0).ToString
        Else
            strTantouIn = String.Empty
        End If

        '承認者紐付け承認印管理マスタから【承認印】を取得する
        Dim strSyouninIn As String
        Dim dtSyouninIn As Data.DataTable
        dtSyouninIn = TyousaMitumorisyoSakuseiInquiryBC.GetSyouninIn(Me.lblKbn.Text, _
                                                                     Me.lblBukkenNo.Text)
        If dtSyouninIn.Rows.Count > 0 Then
            strSyouninIn = dtSyouninIn.Rows(0).Item(0).ToString
        Else
            strSyouninIn = String.Empty
        End If

        '[Fixed Data Section]ONE
        Dim dtOne As Data.DataTable
        dtOne = TyousaMitumorisyoSakuseiInquiryBC.GetKihonInfoOne(Me.lblKbn.Text, _
                                                                  Me.lblBukkenNo.Text)

        '---------------(407662_消費税増税対応_Earth) 李宇削除 2014/03/25↓
        'If dtOne.Rows.Count = 0 Then
        '    'データがない場合、エラー
        '    errMsg = fcw.GetErrMsg(PDFStatus.NoData)
        '    If Not errMsg.Equals(String.Empty) Then
        '        Return errMsg
        '    End If
        'End If
        '---------------(407662_消費税増税対応_Earth) 李宇削除 2014/03/25↑

        '[Fixed Data Section]Two
        Dim dtTwo As Data.DataTable
        dtTwo = TyousaMitumorisyoSakuseiInquiryBC.GetKihonInfoTwo(Me.lblKbn.Text, _
                                                                  Me.lblBukkenNo.Text)

        '---------------(407662_消費税増税対応_Earth) 李宇削除 2014/03/25↓
        'If dtTwo.Rows.Count = 0 Then
        '    'データがない場合、エラー
        '    errMsg = fcw.GetErrMsg(PDFStatus.NoData)
        '    If Not errMsg.Equals(String.Empty) Then
        '        Return errMsg
        '    End If
        'End If
        '---------------(407662_消費税増税対応_Earth) 李宇削除 2014/03/25↑


        '---------------(407662_消費税増税対応_Earth) 李宇追加 2014/02/18↓
        'webConfigで、文言を取得する
        Dim strBungen1 As String = System.Configuration.ConfigurationManager.AppSettings("BunGen1").ToString
        Dim strBungen2 As String = System.Configuration.ConfigurationManager.AppSettings("BunGen2").ToString
        Dim strBungen3 As String = System.Configuration.ConfigurationManager.AppSettings("BunGen3").ToString
        '---------------(407662_消費税増税対応_Earth) 李宇追加 2014/02/18↑

        '---------------(407662_消費税増税対応_Earth) 李宇修正 2014/03/25↓
        '[Fixed Data Section]_editDt_Oneに追加する
        editDR_One = editDt_One.NewRow
        editDR_Two = editDt_Two.NewRow

        If dtOne.Rows.Count > 0 Then
            Dim strHituke As String = String.Empty
            strHituke = Left(Replace(dtOne.Rows(0).Item("hituke"), "/", ""), 4) & "年" & _
                        Mid(Replace(dtOne.Rows(0).Item("hituke"), "/", ""), 5, 2) & "月" & _
                        Right(Replace(dtOne.Rows(0).Item("hituke"), "/", ""), 2) & "日"  '調査見積書作成日

            editDR_One.Item("hituke") = strHituke '調査見積書作成日
            editDR_One.Item("yuubin_no") = dtOne.Rows(0).Item("yuubin_no").ToString '郵便番号
            editDR_One.Item("jyuusyo_1") = dtOne.Rows(0).Item("jyuusyo1").ToString '住所1
            editDR_One.Item("jyuusyo_2") = dtOne.Rows(0).Item("jyuusyo2").ToString '住所2
            editDR_One.Item("tel_fax") = dtOne.Rows(0).Item("tel_fax").ToString 'Fax
            editDR_One.Item("sousinsya") = dtOne.Rows(0).Item("sousinsya").ToString '調査見積書作成者

            editDR_Two.Item("sakuseihi") = strHituke '調査見積書作成日
            editDR_Two.Item("yuubin_no") = dtOne.Rows(0).Item("yuubin_no").ToString '郵便番号
            editDR_Two.Item("jyuusyo_1") = dtOne.Rows(0).Item("jyuusyo1").ToString '住所1
            editDR_Two.Item("jyuusyo_2") = dtOne.Rows(0).Item("jyuusyo2").ToString '住所2
            editDR_Two.Item("tel_fax") = dtOne.Rows(0).Item("tel_fax").ToString 'Fax

            '==================2015/09/17 案件「430011」の対応 追加↓====================================
            Dim strHosoku As String = dtOne.Rows(0).Item("hosoku").ToString
            editDR_One.Item("hosoku") = strHosoku '補足
            editDR_Two.Item("hosoku") = strHosoku '補足
            '==================2015/09/17 案件「430011」の対応 追加↑====================================
        Else
            editDR_One.Item("hituke") = "" '調査見積書作成日
            editDR_One.Item("yuubin_no") = "" '郵便番号
            editDR_One.Item("jyuusyo_1") = "" '住所1
            editDR_One.Item("jyuusyo_2") = "" '住所2
            editDR_One.Item("tel_fax") = "" 'Fax
            editDR_One.Item("sousinsya") = "" '調査見積書作成者

            editDR_Two.Item("sakuseihi") = ""  '調査見積書作成日
            editDR_Two.Item("yuubin_no") = "" '郵便番号
            editDR_Two.Item("jyuusyo_1") = "" '住所1
            editDR_Two.Item("jyuusyo_2") = "" '住所2
            editDR_Two.Item("tel_fax") = "" 'Fax

            '==================2015/09/17 案件「430011」の対応 追加↓====================================
            editDR_One.Item("hosoku") = "" '補足
            editDR_Two.Item("hosoku") = "" '補足
            '==================2015/09/17 案件「430011」の対応 追加↑====================================
        End If

        If dtTwo.Rows.Count > 0 Then

            editDR_One.Item("made") = dtTwo.Rows(0).Item("made").ToString '加盟店名
            'editDR_One.Item("attn") = dtTwo.Rows(0).Item("attn").ToString '依頼担当者

            editDR_Two.Item("kameiten_mei") = dtTwo.Rows(0).Item("made").ToString & Space(2) & "御中" '加盟店名
            editDR_Two.Item("bukken_no") = dtTwo.Rows(0).Item("bukken_no").ToString '区分＋物件No(=保証書No)
            editDR_Two.Item("bukken_mei") = dtTwo.Rows(0).Item("bukken_mei").ToString & Space(2) & "様邸" '施主名
            editDR_Two.Item("bukken_jyuusyo") = dtTwo.Rows(0).Item("bukken_jyuusyo").ToString '物件住所1+物件住所2+物件住所3
            editDR_Two.Item("syouninsya") = strSyouninIn '承認者印を取得する
            editDR_Two.Item("tantousya") = strTantouIn '担当者印を取得する
            editDR_Two.Item("hyoudai") = hyoudai
            editDR_Two.Item("syoukeiMei") = syoukei
            editDR_Two.Item("syouhizeiMei") = syouhizei
            editDR_Two.Item("bungen1") = strBungen1
            editDR_Two.Item("bungen2") = strBungen2
            editDR_Two.Item("bungen3") = strBungen3

        Else
            editDR_One.Item("made") = "" '加盟店名
            'editDR_One.Item("attn") = "" '依頼担当者

            editDR_Two.Item("kameiten_mei") = "" & "御中" '加盟店名
            editDR_Two.Item("bukken_no") = "" '区分＋物件No(=保証書No)
            editDR_Two.Item("bukken_mei") = "" '施主名
            editDR_Two.Item("bukken_jyuusyo") = "" '物件住所1+物件住所2+物件住所3
            editDR_Two.Item("syouninsya") = strSyouninIn '承認者印を取得する
            editDR_Two.Item("tantousya") = strTantouIn '担当者印を取得する
            editDR_Two.Item("hyoudai") = hyoudai
            editDR_Two.Item("syoukeiMei") = syoukei
            editDR_Two.Item("syouhizeiMei") = syouhizei
            editDR_Two.Item("bungen1") = strBungen1
            editDR_Two.Item("bungen2") = strBungen2
            editDR_Two.Item("bungen3") = strBungen3

        End If

        '依頼担当者
        Dim dtIraiTantousya As Data.DataTable
        dtIraiTantousya = tyousaMitumorisyoSakuseiInquiryBC.GetIraiTantousya(Me.lblKbn.Text, Me.lblBukkenNo.Text)

        If (dtIraiTantousya.Rows.Count > 0) AndAlso (Not dtIraiTantousya.Rows(0).Item("tys_mit_irai_tantousya_mei").ToString.Trim.Equals(String.Empty)) Then
            editDR_One.Item("attn") = dtIraiTantousya.Rows(0).Item("tys_mit_irai_tantousya_mei").ToString.Trim
        Else
            If (dtTwo.Rows.Count > 0) AndAlso (Not dtTwo.Rows(0).Item("attn").ToString.Trim.Equals(String.Empty)) Then
                editDR_One.Item("attn") = dtTwo.Rows(0).Item("attn").ToString.Trim
            Else
                editDR_One.Item("attn") = "御担当者"
            End If
        End If

        '行を追加する
        editDt_One.Rows.Add(editDR_One)
        editDt_Two.Rows.Add(editDR_Two)


        ''[Fixed Data Section]_editDt_Oneに追加する
        'If dtOne.Rows.Count > 0 AndAlso dtTwo.Rows.Count > 0 Then
        '    editDR_One = editDt_One.NewRow
        '    editDR_One.Item("hituke") = dtOne.Rows(0).Item("hituke") '調査見積書作成日
        '    editDR_One.Item("made") = dtTwo.Rows(0).Item("made").ToString '加盟店名
        '    editDR_One.Item("attn") = dtTwo.Rows(0).Item("attn").ToString '依頼担当者
        '    editDR_One.Item("yuubin_no") = dtOne.Rows(0).Item("yuubin_no").ToString '郵便番号
        '    editDR_One.Item("jyuusyo_1") = dtOne.Rows(0).Item("jyuusyo1").ToString '住所1
        '    editDR_One.Item("jyuusyo_2") = dtOne.Rows(0).Item("jyuusyo2").ToString '住所2
        '    editDR_One.Item("tel_fax") = dtOne.Rows(0).Item("tel_fax").ToString 'Fax
        '    editDR_One.Item("sousinsya") = dtOne.Rows(0).Item("sousinsya").ToString '調査見積書作成者

        '    editDt_One.Rows.Add(editDR_One)
        'End If

        ''[Fixed Data Section]_editDt_Twoに追加する
        'If dtOne.Rows.Count > 0 AndAlso dtTwo.Rows.Count > 0 Then
        '    editDR_Two = editDt_Two.NewRow
        '    editDR_Two.Item("sakuseihi") = Left(Replace(dtOne.Rows(0).Item("hituke"), "/", ""), 4) & "年" & _
        '                                   Mid(Replace(dtOne.Rows(0).Item("hituke"), "/", ""), 5, 2) & "月" & _
        '                                   Right(Replace(dtOne.Rows(0).Item("hituke"), "/", ""), 2) & "日"  '調査見積書作成日
        '    editDR_Two.Item("kameiten_mei") = dtTwo.Rows(0).Item("made").ToString & Space(2) & "御中" '加盟店名
        '    editDR_Two.Item("bukken_no") = dtTwo.Rows(0).Item("bukken_no").ToString '区分＋物件No(=保証書No)
        '    editDR_Two.Item("bukken_mei") = dtTwo.Rows(0).Item("bukken_mei").ToString '施主名
        '    editDR_Two.Item("bukken_jyuusyo") = dtTwo.Rows(0).Item("bukken_jyuusyo").ToString '物件住所1+物件住所2+物件住所3
        '    editDR_Two.Item("yuubin_no") = dtOne.Rows(0).Item("yuubin_no").ToString '郵便番号
        '    editDR_Two.Item("jyuusyo_1") = dtOne.Rows(0).Item("jyuusyo1").ToString '住所1
        '    editDR_Two.Item("jyuusyo_2") = dtOne.Rows(0).Item("jyuusyo2").ToString '住所2
        '    editDR_Two.Item("tel_fax") = dtOne.Rows(0).Item("tel_fax").ToString 'Fax
        '    editDR_Two.Item("syouninsya") = strSyouninIn '承認者印を取得する
        '    editDR_Two.Item("tantousya") = strTantouIn '担当者印を取得する
        '    editDR_Two.Item("hyoudai") = hyoudai
        '    editDR_Two.Item("syoukeiMei") = syoukei
        '    editDR_Two.Item("syouhizeiMei") = syouhizei

        '    '---------------(407662_消費税増税対応_Earth) 李宇追加 2014/02/18↓
        '    editDR_Two.Item("bungen1") = strBungen1
        '    editDR_Two.Item("bungen2") = strBungen2
        '    editDR_Two.Item("bungen3") = strBungen3
        '    '---------------(407662_消費税増税対応_Earth) 李宇追加 2014/02/18↑

        '    editDt_Two.Rows.Add(editDR_Two)
        'End If
        '---------------(407662_消費税増税対応_Earth) 李宇修正 2014/03/25↑

        '御見積書のデータ
        Dim dtTyouhyouDate As Data.DataTable
        '"税込"
        If rdoZeikomi.Checked = True Then
            dtTyouhyouDate = TyousaMitumorisyoSakuseiInquiryBC.GetTyouhyouDate(Me.lblKbn.Text, _
                                                                               Me.lblBukkenNo.Text, _
                                                                               "税込")
            '"税抜"
        Else
            dtTyouhyouDate = TyousaMitumorisyoSakuseiInquiryBC.GetTyouhyouDate(Me.lblKbn.Text, _
                                                                               Me.lblBukkenNo.Text, _
                                                                               "税抜")
        End If

        '---------------(407662_消費税増税対応_Earth) 李宇削除 2014/03/25↓
        'If dtTyouhyouDate.Rows.Count = 0 Then
        '    'データがない場合、エラー
        '    errMsg = fcw.GetErrMsg(PDFStatus.NoData)
        '    If Not errMsg.Equals(String.Empty) Then
        '        Return errMsg
        '    End If
        'End If
        '---------------(407662_消費税増税対応_Earth) 李宇削除 2014/03/25↑

        If dtTyouhyouDate.Rows.Count > 0 Then
            For i As Integer = 0 To dtTyouhyouDate.Rows.Count - 1
                editDR_Three = editDt_Three.NewRow
                editDR_Three.Item("syouhin_mei") = dtTyouhyouDate.Rows(i).Item("syouhin_mei") '商品名
                editDR_Three.Item("suuryou") = dtTyouhyouDate.Rows(i).Item("suuryou").ToString '数量
                editDR_Three.Item("tanka") = dtTyouhyouDate.Rows(i).Item("tanka").ToString '単価
                editDR_Three.Item("kingaku") = dtTyouhyouDate.Rows(i).Item("kingaku").ToString '金額
                editDR_Three.Item("bikou") = dtTyouhyouDate.Rows(i).Item("bikou").ToString '備考
                editDR_Three.Item("syouhizei") = dtTyouhyouDate.Rows(i).Item("syouhizei").ToString '消費税
                editDt_Three.Rows.Add(editDR_Three)
            Next

        Else
            '---------------(407662_消費税増税対応_Earth) 李宇追加 2014/03/25↓
            editDR_Three = editDt_Three.NewRow
            editDR_Three.Item("syouhin_mei") = "" '商品名
            editDR_Three.Item("suuryou") = "" '数量
            editDR_Three.Item("tanka") = "" '単価
            editDR_Three.Item("kingaku") = "" '金額
            editDR_Three.Item("bikou") = "" '備考
            editDR_Three.Item("syouhizei") = "" '消費税
            editDt_Three.Rows.Add(editDR_Three)
            '---------------(407662_消費税増税対応_Earth) 李宇追加 2014/03/25↑
        End If

        '第Ⅰページ
        sb.Append(vbCrLf)
        '[Form] 部作成
        sb.Append(fcw.CreateFormSection("PAGE=PageOne"))
        '[FixedDataSection] 部作成
        sb.Append(fcw.CreateFixedDataSection(GetFixedDataSection_One(editDt_One)))

        '第Ⅱページ
        sb.Append(vbCrLf)
        '[Form] 部作成
        sb.Append(fcw.CreateFormSection("PAGE=PageTwo"))
        '[FixedDataSection] 部作成
        sb.Append(fcw.CreateFixedDataSection(GetFixedDataSection_Two(editDt_Two)))
        '[TableDataSection](部作成)
        sb.Append(fcw.CreateTableDataSection(GetTableDataSection(editDt_Three)))

        'DATファイル作成
        errMsg = fcw.GetErrMsg(fcw.WriteData(sb.ToString))

        ' 請求先元帳帳票データPDF　出力
        If Not errMsg.Equals(String.Empty) Then
            'エラーがある場合
            Return errMsg
        Else
            'エラーがない場合、帳票をOPEN
            Call Me.PopupFcw(fcw.GetUrl(Me.lblKbn.Text, _
                                        Me.lblBukkenNo.Text, _
                                        (Convert.ToInt32(Me.lblMitumorisyoSakuseiKaisuu.Text) + 1).ToString))

            Return String.Empty
        End If

    End Function

    ''' <summary>
    ''' 第ⅠページFixedDataSection
    ''' </summary>
    ''' <param name="editDt_One"></param>
    ''' <remarks></remarks>
    ''' <history>2013/11/19 李宇(大連情報システム部) 新規作成</history>
    Private Sub AddFeild_One(ByRef editDt_One As Data.DataTable)

        '[FIXED FEILD]
        editDt_One.Columns.Add("hituke", GetType(String)) '調査見積書作成日
        editDt_One.Columns.Add("made", GetType(String)) '加盟店名
        editDt_One.Columns.Add("attn", GetType(String)) '依頼担当者
        editDt_One.Columns.Add("yuubin_no", GetType(String)) '郵便番号
        editDt_One.Columns.Add("jyuusyo_1", GetType(String)) '住所1
        editDt_One.Columns.Add("jyuusyo_2", GetType(String)) '住所2
        editDt_One.Columns.Add("tel_fax", GetType(String)) 'Fax
        editDt_One.Columns.Add("sousinsya", GetType(String)) '調査見積書作成者

        '==================2015/09/17 案件「430011」の対応 追加↓====================================
        editDt_One.Columns.Add("hosoku", GetType(String)) '補足
        '==================2015/09/17 案件「430011」の対応 追加↑====================================

    End Sub

    ''' <summary>
    ''' 第ⅠページGetFixedDataSectionのデータを取得
    ''' </summary>
    ''' <param name="data">データ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/11/18 李宇(大連情報システム部) 新規作成</history>
    Private Function GetFixedDataSection_One(ByVal data As DataTable) As String

        'データを取得
        Return fcw.GetFixedDataSection( _
                                        "hituke" & _
                                        ",made" & _
                                        ",attn" & _
                                        ",yuubin_no" & _
                                        ",jyuusyo_1" & _
                                        ",jyuusyo_2" & _
                                        ",tel_fax" & _
                                        ",sousinsya" & _
                                        ",hosoku", data)

    End Function

    ''' <summary>
    ''' 第ⅡページFixedDataSection
    ''' </summary>
    ''' <param name="editDt_Two"></param>
    ''' <remarks></remarks>
    ''' <history>2013/11/19 李宇(大連情報システム部) 新規作成</history>
    Private Sub AddFeild_Two(ByRef editDt_Two As Data.DataTable)

        '[FIXED FEILD]
        editDt_Two.Columns.Add("sakuseihi", GetType(String)) '調査見積書作成日
        editDt_Two.Columns.Add("kameiten_mei", GetType(String)) '加盟店名
        editDt_Two.Columns.Add("bukken_no", GetType(String)) '区分＋物件No(=保証書No)
        editDt_Two.Columns.Add("bukken_mei", GetType(String)) '施主名
        editDt_Two.Columns.Add("bukken_jyuusyo", GetType(String)) '物件住所1+物件住所2+物件住所3
        editDt_Two.Columns.Add("yuubin_no", GetType(String)) '郵便番号
        editDt_Two.Columns.Add("jyuusyo_1", GetType(String)) '住所1
        editDt_Two.Columns.Add("jyuusyo_2", GetType(String)) '住所2
        editDt_Two.Columns.Add("tel_fax", GetType(String)) 'FAX
        editDt_Two.Columns.Add("syouninsya", GetType(String)) '承認者印
        editDt_Two.Columns.Add("tantousya", GetType(String)) '担当者印
        editDt_Two.Columns.Add("hyoudai", GetType(String)) '表題
        editDt_Two.Columns.Add("syoukeiMei", GetType(String)) '小計
        editDt_Two.Columns.Add("syouhizeiMei", GetType(String)) '消費税
        '---------------(407662_消費税増税対応_Earth) 李宇追加 2014/02/18↓
        editDt_Two.Columns.Add("bungen1", GetType(String)) '文言
        editDt_Two.Columns.Add("bungen2", GetType(String)) '文言
        editDt_Two.Columns.Add("bungen3", GetType(String)) '文言
        '---------------(407662_消費税増税対応_Earth) 李宇追加 2014/02/18↑

        '==================2015/09/17 案件「430011」の対応 追加↓====================================
        editDt_Two.Columns.Add("hosoku", GetType(String)) '補足
        '==================2015/09/17 案件「430011」の対応 追加↑====================================

    End Sub

    ''' <summary>
    ''' 第ⅡページGetFixedDataSectionのデータを取得
    ''' </summary>
    ''' <param name="data">データ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/11/18 李宇(大連情報システム部) 新規作成</history>
    Private Function GetFixedDataSection_Two(ByVal data As DataTable) As String

        'データを取得
        Return fcw.GetFixedDataSection( _
                                        "sakuseihi" & _
                                        ",kameiten_mei" & _
                                        ",bukken_no" & _
                                        ",bukken_mei" & _
                                        ",bukken_jyuusyo" & _
                                        ",yuubin_no" & _
                                        ",jyuusyo_1" & _
                                        ",jyuusyo_2" & _
                                        ",tel_fax" & _
                                        ",syouninsya" & _
                                        ",tantousya" & _
                                        ",hyoudai" & _
                                        ",syoukeiMei" & _
                                        ",syouhizeiMei" & _
                                        ",bungen1" & _
                                        ",bungen2" & _
                                        ",bungen3" & _
                                        ",hosoku", data)

    End Function

    ''' <summary>
    ''' 第Ⅱページ[TableDataSection]
    ''' </summary>
    ''' <param name="editDt_Three"></param>
    ''' <remarks></remarks>
    ''' <history>2013/11/19 李宇(大連情報システム部) 新規作成</history>
    Private Sub AddFeild_Three(ByRef editDt_Three As Data.DataTable)

        '[FIXED FEILD]
        editDt_Three.Columns.Add("syouhin_mei", GetType(String)) '商品名
        editDt_Three.Columns.Add("suuryou", GetType(String)) '数量
        editDt_Three.Columns.Add("tanka", GetType(String)) '単価
        editDt_Three.Columns.Add("kingaku", GetType(String)) '金額
        editDt_Three.Columns.Add("bikou", GetType(String)) '備考
        editDt_Three.Columns.Add("syouhizei", GetType(String)) '消費税

    End Sub

    ''' <summary>
    ''' 第ⅡページGetTableDataSectionのデータを取得
    ''' </summary>
    ''' <param name="data">データ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/11/18 李宇(大連情報システム部) 新規作成</history>
    Private Function GetTableDataSection(ByVal data As DataTable) As String

        '共通CLASS
        Dim earthAction As New EarthAction

        'データを取得
        Return earthAction.JoinDataTable(data, _
                                         APOST, _
                                         "syouhin_mei" & _
                                         ",suuryou" & _
                                         ",tanka" & _
                                         ",kingaku" & _
                                         ",bikou" & _
                                         ",syouhizei")

    End Function

    ''' <summary>
    ''' MakeJavaScript
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2013/11/12 李宇(大連情報システム部) 新規作成</history>
    Private Sub MakeJavaScript()

        Dim csType As Type = Page.GetType()
        Dim csName As String = "setScript"
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("<script type =""text/javascript"" >")
            .AppendLine("function fncHiltusuNyuryokuCheck()")
            .AppendLine("{")
            '「見積書作成日」
            .AppendLine("   var tbxDate = document.getElementById('" & Me.tbxSakuseiDate.ClientID & "');")
            '   必須チェック
            .AppendLine("   if(tbxDate.value.Trim() == '')")
            .AppendLine("       {")
            .AppendLine("           alert('「見積書作成日」は必須です。\r\n');")
            .AppendLine("           tbxDate.focus();")
            .AppendLine("           tbxDate.select();")
            .AppendLine("           return false;")
            .AppendLine("       }")
            '   日付チェック
            .AppendLine("   if(!chkDate(tbxDate.value.Trim()))")
            .AppendLine("       {")
            .AppendLine("           alert('" & Messages.Instance.MSG2017E & "');")
            .AppendLine("           tbxDate.focus();")
            .AppendLine("           tbxDate.select();")
            .AppendLine("           return false;")
            .AppendLine("       }")
            '「依頼担当者 入力」
            .AppendLine("   var tbxIraiTantousya = document.getElementById('" & Me.tbxIraiTantousya.ClientID & "');")
            '   桁数チェック
            .AppendLine("   if(!chkSiteinaiByte(tbxIraiTantousya.value.Trim(),20))")
            .AppendLine("       {")
            .AppendLine("           alert('" & String.Format(Messages.Instance.MSG2002E, "「依頼担当者」", 10) & "');")
            .AppendLine("           tbxIraiTantousya.focus();")
            .AppendLine("           tbxIraiTantousya.select();")
            .AppendLine("           return false;")
            .AppendLine("       }")


            .AppendLine("   var ddlHyoujiJyuusyo = document.getElementById('" & Me.ddlHyoujiJyuusyo.ClientID & "'); ")
            .AppendLine("   var ddlSyouniSya = document.getElementById('" & Me.ddlSyouniSya.ClientID & "'); ")
            '「表示住所 選択」_選択必須
            .AppendLine("   if(ddlHyoujiJyuusyo.value == '0')")
            .AppendLine("       {")
            .AppendLine("           alert('「表示住所 選択」は必須です\r\n表示住所を選択してください');")
            .AppendLine("           ddlHyoujiJyuusyo.focus();")
            .AppendLine("           return false;")
            .AppendLine("       }")
            '「承認者 選択」_選択必須
            .AppendLine("   if(ddlSyouniSya.value == '0')")
            .AppendLine("       {")
            .AppendLine("           alert('「承認者 選択」は必須です\r\n承認者を選択してください');")
            .AppendLine("           ddlSyouniSya.focus();")
            .AppendLine("           return false;")
            .AppendLine("       }")
            .AppendLine("   return true;")
            .AppendLine("}")

            '「クリア」ボタン
            .AppendLine("function fncClear()")
            .AppendLine("{")
            '表示住所
            .AppendLine("   var ddlHyoujiJyuusyo = document.getElementById('" & Me.ddlHyoujiJyuusyo.ClientID & "'); ")
            '承認者
            .AppendLine("   var ddlSyouniSya = document.getElementById('" & Me.ddlSyouniSya.ClientID & "'); ")
            'モール展開 選択
            .AppendLine("   var rdoSuru = document.getElementById('" & Me.rdoSuru.ClientID & "'); ")
            '消費税表示 選択
            .AppendLine("   var rdoZeinu = document.getElementById('" & Me.rdoZeinu.ClientID & "'); ")
            '見積書作成日
            .AppendLine("   var tbxSakuseiDate = document.getElementById('" & Me.tbxSakuseiDate.ClientID & "'); ")
            '依頼担当者
            .AppendLine("   var tbxIraiTantousya = document.getElementById('" & Me.tbxIraiTantousya.ClientID & "'); ")

            .AppendLine("   ddlHyoujiJyuusyo.value = '0';")
            .AppendLine("   ddlSyouniSya.value = '0';")
            .AppendLine("   rdoSuru.checked = true;")
            .AppendLine("   rdoZeinu.checked = true;")
            .AppendLine("   tbxSakuseiDate.value = '';")
            .AppendLine("   tbxIraiTantousya.value = '';")
            .AppendLine("}")
            .AppendLine("</script>")

        End With
        ClientScript.RegisterClientScriptBlock(csType, csName, csScript.ToString)

    End Sub

    ''' <summary>
    ''' 帳票をOpen
    ''' </summary>
    ''' <param name="strUrl">帳票のURL</param>
    ''' <remarks></remarks>
    ''' <history>2013/11/28 李宇(大連情報システム部) 新規作成</history>
    Private Sub PopupFcw(ByVal strUrl As String)

        Dim csType As Type = Page.GetType()
        Dim csName As String = "tyouhyouOpenScript"
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("<script type =""text/javascript"" >")
            .AppendLine("function window.onload(){")
            'モール展開
            .AppendLine("   var moru = document.getElementById('" & Me.rdoSuru.ClientID & "').checked;")
            .AppendLine("   var moruEnable = document.getElementById('" & Me.hidMoru.ClientID & "').value;")
            '区分
            .AppendLine("   var kbn = document.getElementById('" & Me.lblKbn.ClientID & "').innerText;")
            '保証書NO
            .AppendLine("   var bukkenNo = document.getElementById('" & Me.lblBukkenNo.ClientID & "').innerText;")
            '作成回数
            .AppendLine("   var mitumorisyoSakuseiKaisuu = document.getElementById('" & Me.lblMitumorisyoSakuseiKaisuu.ClientID & "').innerText;")
            .AppendLine("   window.open('tyouhyouOpen.aspx?fcwUrl=' + escape('" & strUrl & "') + '$$$' + escape(moru) + '$$$' + escape(moruEnable) + '$$$' + escape(kbn) + '$$$' + escape(bukkenNo) + '$$$' + escape(mitumorisyoSakuseiKaisuu),'PopupFcw');")
            .AppendLine("}")
            .AppendLine("</script>")
        End With
        ClientScript.RegisterClientScriptBlock(csType, csName, csScript.ToString)

    End Sub

    ''' <summary>
    ''' ShowMessage
    ''' </summary>
    ''' <param name="strMessage">メッセージ内容</param>
    ''' <remarks></remarks>
    ''' <history>2013/11/15 李宇(大連情報システム部) 新規作成</history>
    Private Sub ShowMessage(ByVal strMessage As String)

        Dim csType As Type = Page.GetType()
        Dim csName As String = "ShowMessage"
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("<script type =""text/javascript"" >")
            .AppendLine("function window.onload(){")
            .AppendLine("   alert('" & strMessage & "');")
            .AppendLine("}")
            .AppendLine("</script>")
        End With
        ClientScript.RegisterClientScriptBlock(csType, csName, csScript.ToString)

    End Sub
End Class
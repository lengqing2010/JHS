Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Lixil.JHS_EKKS.BizLogic
Imports Lixil.JHS_EKKS.Utilities
Imports Lixil.JHS_EKKS.Utilities.CommonMessage
Imports System.Data
Imports Messages = Lixil.JHS_EKKS.Utilities.CommonMessage

''' <summary>
''' 計画_加盟店情報照会
''' </summary>
''' <remarks></remarks>
Partial Class KeikakuKameitenJyouhouInquiry
    Inherits System.Web.UI.Page

#Region "プライベート変数"

    Private keikakuKameitenJyouhouInquiryBC As New KeikakuKameitenJyouhouInquiryBC
    'Public CommonConstBC As Lixil.JHS_EKKS.BizLogic.CommonConstBC
    'Private common As New Common
    Protected CommonConstBC As CommonConstBC
    Private commonBC As New CommonBC

#End Region

#Region "イベント"

    ''' <summary>
    ''' 初期情報
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    ''' <history>2013/09/06　李宇(大連情報システム部)　新規作成</history>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)

        Dim CommonCheck As New CommonCheck
        Dim strUserID As String = ""
        CommonCheck.CommonNinsyou(strUserID, Master.loginUserInfo, kegen.UserIdOnly)
        Dim ninsyouBC As New NinsyouBC
        If ninsyouBC.GetUserInfo(strUserID).Items.Count = 0 Then
            Context.Items("strFailureMsg") = Messages.MSG023E
            Server.Transfer("./CommonErr.aspx")
        End If

        Call Me.MakeJavaScript()

        If Not IsPostBack Then
            'Dim common As New Common
            'common.SetURL(Me, strUserID, "計画_加盟店情報照会")

            Dim arrUrl() As String = Me.Request.Url.OriginalString.Split("/")
            arrUrl(arrUrl.Length - 1) = "KeikakuKameitenJyouhouInquiry.aspx"
            commonBC.SetUserInfo(String.Join("/", arrUrl), strUserID, "計画_加盟店情報照会")

            '加盟店コード
            ViewState("KameitenCd") = Context.Items("KameitenCd")
            '計画_年度
            ViewState("KeikakuNendo") = Context.Items("KeikakuNendo")

            '前画面の検索条件
            ViewState("Parameter") = Context.Items("Parameter")

            '初期表示時、基本情報データ取得
            Call SetKihonJyouSyutoku(ViewState("KameitenCd").ToString, ViewState("KeikakuNendo").ToString)

            '備考情報の加盟店コードを設定する
            Me.KeikaikuKanriKameitenBikouInquiry1.KameitenCd = ViewState("KameitenCd").ToString
        End If
        '計画年度
        Me.lblKeikakuNendoValue.Text = ViewState("KeikakuNendo").ToString & "年度"
        Me.KeikakuKanriKameitenInquiry.strKameitenCd = ViewState("KameitenCd")
        Me.KeikakuKanriKameitenInquiry.strNendo = ViewState("KeikakuNendo")
        Me.KeikakuKanriKameitenInquiry.strKbn = Me.tbxKbn.Text
        Me.KeikakuKanriKameitenInquiry.strKbnMei = Me.lblKbn.Text
        Me.lblEigyouSya.Attributes.Add("readOnly", "true")
        Me.lblSyozoku.Attributes.Add("readOnly", "true")
        Me.lblKmeitenMei.Attributes.Add("readOnly", "true")

    End Sub

    ''' <summary>
    ''' 「戻る」ボタンをクリックする
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    ''' <history>2013/09/06　李宇(大連情報システム部)　新規作成</history>
    Protected Sub btnModoru_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModoru.Click

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, sender, e)

        '前画面の検索条件
        Context.Items("Parameter") = ViewState("Parameter")

        Server.Transfer("KeikakuKanriKameitenKensakuSyoukaiInquiry.aspx")
    End Sub

#End Region

#Region "メソッド"

    ''' <summary>
    ''' 基本情報を設定する
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2013/09/06　李宇(大連情報システム部)　新規作成</history>
    Private Sub SetKihonJyouSyutoku(ByVal strKameitenCd As String, ByVal strKeikauNendo As String)

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name, strKameitenCd, strKeikauNendo)

        '計画_基本情報
        Dim dtKeikakuKihonJyouhou As Data.DataTable
        dtKeikakuKihonJyouhou = keikakuKameitenJyouhouInquiryBC.GetKihonJyouSyutoku(strKameitenCd, strKeikauNendo)

        If dtKeikakuKihonJyouhou.Rows.Count > 0 Then
            With dtKeikakuKihonJyouhou.Rows(0)

                '最新更新者
                If .Item("upd_login_user_id").ToString.Equals(String.Empty) Then
                    Me.lblSaisinKousisyaValue.Text = .Item("add_login_user_id").ToString
                Else
                    Me.lblSaisinKousisyaValue.Text = .Item("upd_login_user_id").ToString
                End If

                '最新更新日時
                If .Item("upd_datetime").ToString.Equals(String.Empty) Then
                    Me.lblSaisinKousinNitijiValue.Text = .Item("add_datetime").ToString
                Else
                    Me.lblSaisinKousinNitijiValue.Text = .Item("upd_datetime").ToString
                End If

                '取消:名称(取消の内容取得)
                If (.Item("torikesi").ToString.Equals("0")) OrElse (.Item("torikesi").ToString.Equals(String.Empty)) Then
                    Me.lblTorikesi.Text = String.Empty
                Else
                    Me.lblTorikesi.Text = .Item("torikesi").ToString & "：" & .Item("torikesiMei").ToString
                End If

                '発注停止FLG:名称(発注停止FLGの内容取得)
                If (.Item("hattyuu_teisi_flg").ToString.Equals(String.Empty)) OrElse (.Item("flgMei").ToString.Equals(String.Empty)) Then
                    Me.lblHaltutyuuTeisiFlg.Text = String.Empty
                Else
                    Me.lblHaltutyuuTeisiFlg.Text = .Item("hattyuu_teisi_flg").ToString & "：" & .Item("flgMei").ToString
                End If

                '区分
                Me.tbxKbn.Text = .Item("kbn").ToString
                '区分名
                Me.lblKbn.Text = .Item("kbn_mei").ToString
                '加盟店ｺｰﾄﾞ
                Me.tbxKameitenCd.Text = .Item("kameiten_cd").ToString
                '加盟店名
                Me.lblKmeitenMei.Text = .Item("kameiten_mei").ToString
                '営業区分
                Me.tbxEigyouKbn.Text = .Item("eigyou_kbn").ToString
                '名称(営業区分の内容取得)
                Me.lblEigyouKbn.Text = .Item("eigyouKbnMei").ToString
                '営業担当者
                Me.tbxEigyouSya.Text = .Item("eigyou_tantousya_id").ToString
                '営業担当者名
                Me.lblEigyouSya.Text = .Item("eigyou_tantousya_mei").ToString
                '支店名
                Me.lblKankatuSitemei.Text = .Item("shiten_mei").ToString

                '都道府県ｺｰﾄﾞ:都道府県名
                If (.Item("todouhuken_cd").ToString.Equals(String.Empty)) OrElse (.Item("todouhuken_mei").ToString.Equals(String.Empty)) Then
                    Me.lblTodouhuke.Text = String.Empty
                Else
                    Me.lblTodouhuke.Text = .Item("todouhuken_cd").ToString & "：" & .Item("todouhuken_mei").ToString
                End If

                '営業所ｺｰﾄﾞ
                Me.tbxEigyousyoCd.Text = .Item("eigyousyo_cd").ToString
                '営業所名
                Me.lblEigyousyoCd.Text = .Item("eigyousyo_mei").ToString
                '系列ｺｰﾄﾞ
                Me.tbxKeiretuCd.Text = .Item("keiretu_cd").ToString
                '系列名
                Me.lblKeiretuCd.Text = .Item("keiretu_mei").ToString
                '所属
                Me.lblSyozoku.Text = .Item("syozoku").ToString
                '年間棟数
                Me.lblNenken.Text = .Item("keikaku_nenkan_tousuu").ToString
                '計画値0FLG  
                Me.lblKeikakuTi.Text = .Item("keikaku0_flg").ToString
            End With

        End If

    End Sub

    ''' <summary>
    ''' JavaScriptを作成する
    ''' </summary>
    ''' <history>2013/09/02　車龍(大連情報システム部)　新規作成</history>
    Private Sub MakeJavaScript()

        'EMAB障害対応情報の格納処理
        EMAB.AddMethodEntrance(Request.ApplicationPath & "." & MyClass.GetType.BaseType.FullName & "." & _
                               MyMethod.GetCurrentMethod.Name)

        Dim csType As Type = Page.GetType()
        Dim csName As String = "setAllPageScript"
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("<script language='javascript' type='text/javascript'>  ")


            .AppendLine("   function fncSaveAllPageScroll(){")
            .AppendLine("       var divMeisai=$ID('divAll');")
            .AppendLine("       document.getElementById('" & Me.hidAllPageScroll.ClientID & "').value = divMeisai.scrollTop;")
            .AppendLine("   }")
            .AppendLine("   function fncSetAllPageScroll(){")
            .AppendLine("       var divMeisai=$ID('divAll');")
            .AppendLine("       if(divMeisai != null)")
            .AppendLine("       {")
            .AppendLine("           var hidScroll=$ID('" & Me.hidAllPageScroll.ClientID & "');")
            .AppendLine("           if(hidScroll.value != '')")
            .AppendLine("           {")
            .AppendLine("               divMeisai.scrollTop = hidScroll.value;")
            .AppendLine("           }")
            .AppendLine("       }")
            .AppendLine("   }")



            .AppendLine("</script>  ")
        End With

        Me.ClientScript.RegisterClientScriptBlock(csType, csName, csScript.ToString)
    End Sub





#End Region

End Class

Partial Public Class KeiriMenu
    Inherits System.Web.UI.Page

    'ログインユーザレコード
    Private userinfo As New LoginUserInfo
    Private masterAjaxSM As New ScriptManager

    ''' <summary>
    ''' ページロード
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim jBn As New Jiban '地盤画面共通クラス
        Dim jSM As New JibanSessionManager

        'マスターページ情報を取得（ScriptManager用）
        Dim myMaster As EarthMasterPage = Page.Master
        masterAjaxSM = myMaster.AjaxScriptManager1

        ' ユーザー基本認証
        jBn.UserAuth(userinfo)

        '認証結果によって画面表示を切替える
        If userinfo Is Nothing Then
            'ログイン情報が無い場合
            Response.Redirect(UrlConst.MAIN)
            Me.BtnStyle()
            Exit Sub
        End If

        If IsPostBack = False Then

            '●権限のチェック
            '経理業務権限
            If userinfo.KeiriGyoumuKengen = 0 Then
                Response.Redirect(UrlConst.MAIN)
                Me.BtnStyle()
                Exit Sub
            End If
            'フォーカス設定
            Me.BtnTeibetuSyuusei.Focus()
        End If

        '邸別データ修正ボタン
        BtnTeibetuSyuusei.Attributes("onclick") = _
            "callModalBukken('" & UrlConst.POPUP_BUKKEN_SITEI & "','" & UrlConst.TEIBETU_SYUUSEI & "','teibetu',event.shiftKey==false,'','');return false;"
        '店別データ修正ボタン
        BtnTenbetuSyuusei.Attributes("onclick") = _
            "callModalMise('" & UrlConst.POPUP_MISE_SITEI & "','" & UrlConst.TENBETU_SYUUSEI & "','tenbetu',event.shiftKey==false,'','1','');return false;"
        '邸別入金修正ボタン
        BtnTeibetuNyuukinSyuusei.Attributes("onclick") = _
            "callModalBukken('" & UrlConst.POPUP_BUKKEN_SITEI & "','" & UrlConst.TEIBETU_NYUUKIN_SYUUSEI & "','teinyuu',event.shiftKey==false,'','');return false;"

    End Sub

#Region "ボタン押下時処理"

    ''' <summary>売上計上／月額割引作成押下時</summary>
    Private Sub BtnUriageSiireSakusei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnUriageSiireSakusei.Click
        Response.Redirect(UrlConst.URIAGE_SIIRE_SAKUSEI)
    End Sub

    ''' <summary>売上伝票照会／請求日変更ボタン押下時</summary>
    Private Sub BtnSearchUriageData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearchUriageData.Click
        Response.Redirect(UrlConst.SEARCH_URIAGE_DATA)
    End Sub

    ''' <summary>月次データ一括修正ボタン押下時</summary>
    Private Sub BtnGetujiIkkatuSyuusei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnGetujiIkkatuSyuusei.Click
        Response.Redirect(UrlConst.GETUJI_IKKATU_SYUUSEI)
    End Sub

    ''' <summary>仕入伝票照会ボタン押下時</summary>
    Private Sub BtnSearchSiireData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearchSiireData.Click
        Response.Redirect(UrlConst.SEARCH_SIIRE_DATA)
    End Sub

    ''' <summary>入金伝票照会ボタン押下時</summary>
    Private Sub BtnSearchNyuukinData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearchNyuukinData.Click
        Response.Redirect(UrlConst.SEARCH_NYUUKIN_DATA)
    End Sub

    ''' <summary>入金取込処理ボタン押下時</summary>
    Private Sub BtnNyuukinSyori_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNyuukinSyori.Click
        Response.Redirect(UrlConst.NYUUKIN_SYORI)
    End Sub

    ''' <summary>請求書データ作成／出力ボタン押下時</summary>
    Private Sub BtnSeikyuusyoDataSakusei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSeikyuusyoDataSakusei.Click
        Response.Redirect(UrlConst.SEIKYUUSYO_DATA_SAKUSEI)
    End Sub

    ''' <summary>支払伝票照会ボタン押下時</summary>
    Private Sub BtnSearchSiharaiData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearchSiharaiData.Click
        Response.Redirect(UrlConst.SEARCH_SIHARAI_DATA)
    End Sub

    ''' <summary>入金取込データ照会／登録／修正ボタン押下時</summary>
    Private Sub BtnSearchNyuukinTorikomi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearchNyuukinTorikomi.Click
        Response.Redirect(UrlConst.SEARCH_NYUUKIN_TORIKOMI)
    End Sub

    ''' <summary>請求先元帳ボタン押下時</summary>
    Private Sub BtnSeikyuuSakiMototyou_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSeikyuuSakiMototyou.Click
        Response.Redirect(UrlConst.SEIKYUU_SAKI_MOTOTYOU)
    End Sub

    ''' <summary>汎用売上データ照会／登録／修正ボタン押下時</summary>
    Private Sub BtnSearchHannyouUriage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearchHannyouUriage.Click
        Response.Redirect(UrlConst.SEARCH_HANNYOU_URIAGE)
    End Sub

    ''' <summary>請求年月日一括変更ボタン押下時</summary>
    Private Sub BtnSeikyuuDateIkkatuHenkou_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSeikyuuDateIkkatuHenkou.Click
        Response.Redirect(UrlConst.SEIKYUU_DATE_IKKATU_HENKOU)
    End Sub

    ''' <summary>支払先元帳ボタン押下時</summary>
    Private Sub BtnSiharaiSakiMototyou_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSiharaiSakiMototyou.Click
        Response.Redirect(UrlConst.SIHARAI_SAKI_MOTOTYOU)
    End Sub

    ''' <summary>汎用仕入データ照会／登録／修正ボタン押下時</summary>
    Private Sub BtnSearchHannyouSiire_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearchHannyouSiire.Click
        Response.Redirect(UrlConst.SEARCH_HANNYOU_SIIRE)
    End Sub

    ''' <summary>邸別請求先・仕入先一括変更ボタン押下時</summary>
    Private Sub BtnTeibetuSiireIkkatuHenkou_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnTeibetuSiireIkkatuHenkou.Click
        Response.Redirect(UrlConst.SEIKYUU_SIIRE_SAKI_HENKOU)
    End Sub

    ''' <summary>各種マスタ／データ出力ボタン押下時</summary>
    Private Sub BtnKakusyuDataSyuturyoku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnKakusyuDataSyuturyoku.Click
        Response.Redirect(UrlConst.EARTH2_KAKUSYU_DATA_SYUTURYOKU_MENU)
    End Sub

    ''' <summary>戻るボタン押下時</summary>
    Private Sub BtnModoru_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnModoru.Click
        Response.Redirect(UrlConst.MAIN)
    End Sub

#End Region


    ''' <summary>
    ''' ボタン非表示処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BtnStyle()

        Me.BtnTeibetuSyuusei.Attributes("style") = "display:none"
        Me.BtnTeibetuSyuusei.Attributes.Remove("onclick")
        Me.BtnUriageSiireSakusei.Attributes("style") = "display:none"
        Me.BtnSearchUriageData.Attributes("style") = "display:none"
        Me.BtnTenbetuSyuusei.Attributes("style") = "display:none"
        Me.BtnTenbetuSyuusei.Attributes.Remove("onclick")
        Me.BtnGetujiIkkatuSyuusei.Attributes("style") = "display:none"
        Me.BtnSearchSiireData.Attributes("style") = "display:none"
        Me.BtnTeibetuNyuukinSyuusei.Attributes("style") = "display:none"
        Me.BtnTeibetuNyuukinSyuusei.Attributes.Remove("onclick")
        Me.BtnSearchNyuukinData.Attributes("style") = "display:none"
        Me.BtnNyuukinSyori.Attributes("style") = "display:none"
        Me.BtnSeikyuusyoDataSakusei.Attributes("style") = "display:none"
        Me.BtnSearchSiharaiData.Attributes("style") = "display:none"
        Me.BtnSearchNyuukinTorikomi.Attributes("style") = "display:none"
        Me.BtnSeikyuuSakiMototyou.Attributes("style") = "display:none"
        Me.BtnSearchHannyouUriage.Attributes("style") = "display:none"
        Me.BtnSeikyuuDateIkkatuHenkou.Attributes("style") = "display:none"
        Me.BtnSiharaiSakiMototyou.Attributes("style") = "display:none"
        Me.BtnSearchHannyouSiire.Attributes("style") = "display:none"
        Me.BtnTeibetuSiireIkkatuHenkou.Attributes("style") = "display:none"
        Me.BtnKakusyuDataSyuturyoku.Attributes("style") = "display:none"

    End Sub
End Class
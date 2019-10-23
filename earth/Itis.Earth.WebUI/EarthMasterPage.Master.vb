
Partial Public Class EarthMasterPage
    Inherits System.Web.UI.MasterPage

    Dim userInfo As New LoginUserInfo

    ''' <summary>
    ''' ContentPlaceHolder
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>CPH1へのアクセス用</remarks>
    Public Property MasterContentPlaceHolder() As System.Web.UI.WebControls.ContentPlaceHolder
        Get
            Return CPH1
        End Get
        Set(ByVal value As System.Web.UI.WebControls.ContentPlaceHolder)
            CPH1 = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim jBn As New Jiban '地盤画面共通クラス

        ' ユーザー基本認証
        jBn.UserAuth(userInfo)

        If Not IsPostBack Then

            '業務メニュー
            LinkMain.HRef = UrlConst.MAIN
            LinkSinkiJutyuu.HRef = "javascript:void(0)"
            LinkSinkiJutyuu.Attributes("onclick") = "dispSubMenu(this,'" & UlSinkiJutyuuList.ClientID & "','" & IframeSinkiJutyuuList.ClientID & "');return false;"
            LinkMousikomiNyuuryoku.HRef = UrlConst.MOUSIKOMI_INPUT
            LinkJutyuuTouroku.HRef = UrlConst.IRAI_STEP_1 & "?st=" & "" & EarthConst.MODE_NEW
            LinkMousikomiKensaku.HRef = UrlConst.SEARCH_MOUSIKOMI
            LinkMousikomiKensakuOW.HRef = UrlConst.SEARCH_MOUSIKOMI
            LinkFcMousikomiKensaku.HRef = UrlConst.SEARCH_FC_MOUSIKOMI
            LinkFcMousikomiKensakuOW.HRef = UrlConst.SEARCH_FC_MOUSIKOMI

            LinkMenuBukkenKensaku.HRef = "javascript:void(0)"
            LinkMenuBukkenKensaku.Attributes("onclick") = "dispSubMenu(this,'" & UlBukkenKensakuList.ClientID & "','" & IframeBukkenKensakuList.ClientID & "');return false;"
            LinkBukkenKensaku.HRef = UrlConst.SEARCH_BUKKEN
            LinkBukkenKensakuOW.HRef = UrlConst.SEARCH_BUKKEN
            LinkHinsituHosyousyoJyoukyouKensaku.HRef = UrlConst.SEARCH_HINSITU_HOSYOUSYO_JYOUKYOU
            LinkHinsituHosyousyoJyoukyouKensakuOW.HRef = UrlConst.SEARCH_HINSITU_HOSYOUSYO_JYOUKYOU

            '業務メニュー / 修正・経理
            LinkKeiriHansokuhin.HRef = "javascript:void(0)"
            LinkKeiriHansokuhin.Attributes("onclick") = "dispSubMenu(this,'" & UlKeiriLinksList.ClientID & "','" & IframeKeiriLinksList.ClientID & "');return false;"
            LinkTeibetuSyuusei.HRef = "javascript:void(0)"
            LinkTeibetuSyuusei.Attributes("onclick") = "callModalBukken('" & UrlConst.POPUP_BUKKEN_SITEI & "','" & UrlConst.TEIBETU_SYUUSEI & "','teibetu',event.shiftKey==false,'','');return false;"
            LinkTeibetuSyuuseiOW.HRef = "javascript:void(0)"
            LinkTeibetuSyuuseiOW.Attributes("onclick") = "callModalBukken('" & UrlConst.POPUP_BUKKEN_SITEI & "','" & UrlConst.TEIBETU_SYUUSEI & "','teibetu',false,'','');return false;"
            LinkTeibetuNyuukinSyuusei.HRef = "javascript:void(0)"
            LinkTeibetuNyuukinSyuusei.Attributes("onclick") = "callModalBukken('" & UrlConst.POPUP_BUKKEN_SITEI & "','" & UrlConst.TEIBETU_NYUUKIN_SYUUSEI & "','teinyuu',event.shiftKey==false,'','');return false;"
            LinkTeibetuNyuukinSyuuseiOW.HRef = "javascript:void(0)"
            LinkTeibetuNyuukinSyuuseiOW.Attributes("onclick") = "callModalBukken('" & UrlConst.POPUP_BUKKEN_SITEI & "','" & UrlConst.TEIBETU_NYUUKIN_SYUUSEI & "','teinyuu',false,'','');return false;"
            ' [ tenmd ] 1:店別データ修正 2:販促品請求 3:売上処理済販促品参照
            ' [ isfc  ] 0:FC以外 1:FC
            LinkTenbetuSyuusei.HRef = "javascript:void(0)"
            LinkTenbetuSyuusei.Attributes("onclick") = "callModalMise('" & UrlConst.POPUP_MISE_SITEI & "','" & UrlConst.TENBETU_SYUUSEI & "','tenbetu',event.shiftKey==false,'','1','');return false;"
            LinkTenbetuSyuuseiOW.HRef = "javascript:void(0)"
            LinkTenbetuSyuuseiOW.Attributes("onclick") = "callModalMise('" & UrlConst.POPUP_MISE_SITEI & "','" & UrlConst.TENBETU_SYUUSEI & "','tenbetu',false,'','1','');return false;"
            LinkHansokuhinSeikyuu.HRef = "javascript:void(0)"
            LinkHansokuhinSeikyuu.Attributes("onclick") = "callModalMise('" & UrlConst.POPUP_MISE_SITEI & "','" & UrlConst.TENBETU_SYUUSEI & "','hansoku',event.shiftKey==false,'','2','');return false;"
            LinkHansokuhinSeikyuuOW.HRef = "javascript:void(0)"
            LinkHansokuhinSeikyuuOW.Attributes("onclick") = "callModalMise('" & UrlConst.POPUP_MISE_SITEI & "','" & UrlConst.TENBETU_SYUUSEI & "','hansoku',false,'','2','');return false;"
            LinkUriageSiireSakusei.HRef = UrlConst.URIAGE_SIIRE_SAKUSEI
            LinkUriageSiireSakuseiOW.HRef = UrlConst.URIAGE_SIIRE_SAKUSEI
            LinkGetujiIkkatuSyuusei.HRef = UrlConst.GETUJI_IKKATU_SYUUSEI
            LinkGetujiIkkatuSyuuseiOW.HRef = UrlConst.GETUJI_IKKATU_SYUUSEI
            LinkNyuukinSyori.HRef = UrlConst.NYUUKIN_SYORI
            LinkNyuukinSyoriOW.HRef = UrlConst.NYUUKIN_SYORI
            '経理 伝票系
            LinkSearchUriageData.HRef = UrlConst.SEARCH_URIAGE_DATA
            LinkSearchUriageDataOW.HRef = UrlConst.SEARCH_URIAGE_DATA
            LinkSearchSiireData.HRef = UrlConst.SEARCH_SIIRE_DATA
            LinkSearchSiireDataOW.HRef = UrlConst.SEARCH_SIIRE_DATA
            LinkSearchNyuukinData.HRef = UrlConst.SEARCH_NYUUKIN_DATA
            LinkSearchNyuukinDataOW.HRef = UrlConst.SEARCH_NYUUKIN_DATA
            LinkSearchNyuukinTorikomi.HRef = UrlConst.SEARCH_NYUUKIN_TORIKOMI
            LinkSearchNyuukinTorikomiOW.HRef = UrlConst.SEARCH_NYUUKIN_TORIKOMI
            LinkSearchHannyouUriage.HRef = UrlConst.SEARCH_HANNYOU_URIAGE
            LinkSearchHannyouUriageOW.HRef = UrlConst.SEARCH_HANNYOU_URIAGE
            LinkSearchHannyouSiire.HRef = UrlConst.SEARCH_HANNYOU_SIIRE
            LinkSearchHannyouSiireOW.HRef = UrlConst.SEARCH_HANNYOU_SIIRE
            LinkSearchSiharaiData.HRef = UrlConst.SEARCH_SIHARAI_DATA
            LinkSearchSiharaiDataOW.HRef = UrlConst.SEARCH_SIHARAI_DATA
            LinkSeikyuusyoDataSakusei.HRef = UrlConst.SEIKYUUSYO_DATA_SAKUSEI
            LinkSeikyuusyoDataSakuseiOW.HRef = UrlConst.SEIKYUUSYO_DATA_SAKUSEI
            LinkSeikyuuSakiMototyou.HRef = UrlConst.SEIKYUU_SAKI_MOTOTYOU
            LinkSeikyuuSakiMototyouOW.HRef = UrlConst.SEIKYUU_SAKI_MOTOTYOU
            LinkSiharaiSakiMototyou.HRef = UrlConst.SIHARAI_SAKI_MOTOTYOU
            LinkSiharaiSakiMototyouOW.HRef = UrlConst.SIHARAI_SAKI_MOTOTYOU
            LinkSeikyuuDateIkkatuHenkou.HRef = UrlConst.SEIKYUU_DATE_IKKATU_HENKOU
            LinkSeikyuuDateIkkatuHenkouOW.HRef = UrlConst.SEIKYUU_DATE_IKKATU_HENKOU
            LinkKakusyuDataSyuturyoku.HRef = UrlConst.EARTH2_KAKUSYU_DATA_SYUTURYOKU_MENU
            LinkKakusyuDataSyuturyokuOW.HRef = UrlConst.EARTH2_KAKUSYU_DATA_SYUTURYOKU_MENU
            LinkSeikyuuSiireHenkou.HRef = UrlConst.SEIKYUU_SIIRE_SAKI_HENKOU
            LinkSeikyuuSiireHenkouOW.HRef = UrlConst.SEIKYUU_SIIRE_SAKI_HENKOU
            '登録・照会メニュー
            LinkKameitenSyoukaiTouroku.HRef = UrlConst.EARTH2_KAMEITEN_SYOUKAI_TOUROKU
            LinkKameitenSyoukaiTourokuOW.HRef = UrlConst.EARTH2_KAMEITEN_SYOUKAI_TOUROKU
            '営業向けメニュー
            LinkEigyouMenu.HRef = "javascript:void(0)"
            LinkEigyouMenu.Attributes("onclick") = "dispSubMenu(this,'" & UlEigyouMenuList.ClientID & "','" & IframeEigyouMenuList.ClientID & "');return false;"
            LinkKameitenSyoukai.HRef = UrlConst.EARTH2_KAMEITEN_SYOUKAI
            LinkKameitenSyoukaiOW.HRef = UrlConst.EARTH2_KAMEITEN_SYOUKAI
            LinkBukkenSyoukai.HRef = UrlConst.EARTH2_BUKKEN_SYOUKAI
            LinkBukkenSyoukaiOW.HRef = UrlConst.EARTH2_BUKKEN_SYOUKAI

            ' ユーザー権限によりリンク状態、ログイン者情報を切り替え
            checkAuthKengen()

        End If


    End Sub

    ''' <summary>
    ''' ユーザー権限によりリンク状態、ログイン者情報を切り替え
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub checkAuthKengen()

        If userInfo IsNot Nothing Then

            'ログイン者情報をセット
            TdBusyoName.InnerText = userInfo.Department
            TdUserName.InnerText = userInfo.DisplayName

            'アカウントNOが無い場合、該当リンクを削除
            If userInfo.AccountNo = 0 Then
                '物件検索、経理・販促品、加盟店照会・登録、マスタメンテナンス
                LinkMenuBukkenKensaku.HRef = String.Empty
                LinkMenuBukkenKensaku.Attributes.Remove("onclick")
                LinkBukkenKensaku.HRef = String.Empty
                LinkBukkenKensakuOW.HRef = String.Empty
                LinkHinsituHosyousyoJyoukyouKensaku.HRef = String.Empty
                LinkHinsituHosyousyoJyoukyouKensakuOW.HRef = String.Empty
                LinkSinkiJutyuu.HRef = String.Empty
                LinkSinkiJutyuu.Attributes.Remove("onclick")
                LinkKeiriHansokuhin.HRef = String.Empty
                LinkKeiriHansokuhin.Attributes.Remove("onclick")
                LinkKameitenSyoukaiTouroku.HRef = String.Empty
                LinkKameitenSyoukaiTourokuOW.HRef = String.Empty
            End If

            '新規入力権限が無い場合、受注のリンクを削除
            If userInfo.SinkiNyuuryokuKengen = 0 Then
                LinkSinkiJutyuu.HRef = String.Empty
                LinkSinkiJutyuu.Attributes.Remove("onclick")
                LinkMousikomiNyuuryoku.HRef = String.Empty
                LinkJutyuuTouroku.HRef = String.Empty
                LinkMousikomiKensaku.HRef = String.Empty
                LinkMousikomiKensakuOW.HRef = String.Empty
                LinkMousikomiKensakuOW.Attributes.Remove("onclick")
            End If

            '営業所マスタ管理権限、販促売上権限が何れも無い場合、販促品請求のリンクを削除
            If userInfo.EigyouMasterKanriKengen = 0 And userInfo.HansokuUriageKengen = 0 Then
                LinkHansokuhinSeikyuu.HRef = String.Empty
                LinkHansokuhinSeikyuu.Attributes.Remove("onclick")
                LinkHansokuhinSeikyuuOW.HRef = String.Empty
                LinkHansokuhinSeikyuuOW.Attributes.Remove("onclick")
            End If

            '経理業務権限、発注書管理権限が何れも無い場合、邸別データ修正のリンクを削除
            If userInfo.KeiriGyoumuKengen = 0 And userInfo.HattyuusyoKanriKengen = 0 Then
                LinkTeibetuSyuusei.HRef = String.Empty
                LinkTeibetuSyuusei.Attributes.Remove("onclick")
                LinkTeibetuSyuuseiOW.HRef = String.Empty
                LinkTeibetuSyuuseiOW.Attributes.Remove("onclick")
            End If

            '経理業務権限が無い場合、該当リンクを削除
            If userInfo.KeiriGyoumuKengen = 0 Then
                LinkTeibetuNyuukinSyuusei.HRef = String.Empty
                LinkTeibetuNyuukinSyuusei.Attributes.Remove("onclick")
                LinkTeibetuNyuukinSyuuseiOW.HRef = String.Empty
                LinkTeibetuNyuukinSyuuseiOW.Attributes.Remove("onclick")
                LinkTenbetuSyuusei.HRef = String.Empty
                LinkTenbetuSyuusei.Attributes.Remove("onclick")
                LinkTenbetuSyuuseiOW.HRef = String.Empty
                LinkTenbetuSyuuseiOW.Attributes.Remove("onclick")
                LinkUriageSiireSakusei.HRef = String.Empty
                LinkUriageSiireSakuseiOW.HRef = String.Empty
                LinkGetujiIkkatuSyuusei.HRef = String.Empty
                LinkGetujiIkkatuSyuuseiOW.HRef = String.Empty
                LinkNyuukinSyori.HRef = String.Empty
                LinkNyuukinSyoriOW.HRef = String.Empty
                LinkSearchUriageData.HRef = String.Empty
                LinkSearchUriageDataOW.HRef = String.Empty
                LinkSearchSiireData.HRef = String.Empty
                LinkSearchSiireDataOW.HRef = String.Empty
                LinkSearchNyuukinData.HRef = String.Empty
                LinkSearchNyuukinDataOW.HRef = String.Empty
                LinkSearchNyuukinTorikomi.HRef = String.Empty
                LinkSearchNyuukinTorikomiOW.HRef = String.Empty
                LinkSearchHannyouUriage.HRef = String.Empty
                LinkSearchHannyouUriageOW.HRef = String.Empty
                LinkSearchHannyouSiire.HRef = String.Empty
                LinkSearchHannyouSiireOW.HRef = String.Empty
                LinkSearchSiharaiData.HRef = String.Empty
                LinkSearchSiharaiDataOW.HRef = String.Empty
                LinkSeikyuusyoDataSakusei.HRef = String.Empty
                LinkSeikyuusyoDataSakuseiOW.HRef = String.Empty
                LinkSeikyuuSakiMototyou.HRef = String.Empty
                LinkSeikyuuSakiMototyouOW.HRef = String.Empty
                LinkSiharaiSakiMototyou.HRef = String.Empty
                LinkSiharaiSakiMototyouOW.HRef = String.Empty
                LinkSeikyuuDateIkkatuHenkou.HRef = String.Empty
                LinkSeikyuuDateIkkatuHenkouOW.HRef = String.Empty
                LinkSeikyuuSiireHenkou.HRef = String.Empty
                LinkSeikyuuSiireHenkouOW.HRef = String.Empty
                LinkKakusyuDataSyuturyoku.HRef = String.Empty
                LinkSeikyuuSiireHenkouOW.HRef = String.Empty
            End If

        Else
            LinkMain.HRef = String.Empty
            ' 地盤認証マスタ情報取得不可の場合、地盤のメニュー使用不可
            LinkSinkiJutyuu.HRef = String.Empty
            LinkSinkiJutyuu.Attributes.Remove("onclick")
            LinkMousikomiNyuuryoku.HRef = String.Empty
            LinkJutyuuTouroku.HRef = String.Empty
            LinkMousikomiKensaku.HRef = String.Empty
            LinkMousikomiKensakuOW.HRef = String.Empty
            LinkMousikomiKensakuOW.Attributes.Remove("onclick")
            LinkMenuBukkenKensaku.HRef = String.Empty
            LinkMenuBukkenKensaku.Attributes.Remove("onclick")
            LinkBukkenKensaku.HRef = String.Empty
            LinkHinsituHosyousyoJyoukyouKensaku.HRef = String.Empty
            LinkHinsituHosyousyoJyoukyouKensakuOW.HRef = String.Empty
            LinkBukkenKensakuOW.HRef = String.Empty
            LinkKeiriHansokuhin.HRef = String.Empty
            LinkKeiriHansokuhin.Attributes.Remove("onclick")

            LinkKameitenSyoukaiTouroku.HRef = String.Empty
            LinkKameitenSyoukaiTourokuOW.HRef = String.Empty
            LinkEigyouMenu.HRef = String.Empty
            LinkKameitenSyoukai.HRef = String.Empty
            LinkKameitenSyoukaiOW.HRef = String.Empty
            LinkBukkenSyoukai.HRef = String.Empty
            LinkBukkenSyoukaiOW.HRef = String.Empty
        End If

    End Sub

End Class
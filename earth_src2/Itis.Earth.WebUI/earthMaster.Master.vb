Imports Itis.Earth.BizLogic

Partial Public Class earthMaster
    Inherits System.Web.UI.MasterPage

    Dim user_info As New LoginUserInfo

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim jBn As New Jiban '地盤画面共通クラス

        ' ユーザー基本認証
        jBn.userAuth(user_info)

        If Not IsPostBack Then
            '業務メニュー
            linkMain.HRef = UrlConst.MAIN
            linkIrai.HRef = UrlConst.IRAI_STEP_1 & "?st=" & "" & EarthConst.modeNew
            linkBukkenKensaku.HRef = UrlConst.SEARCH_BUKKEN
            linkBukkenKensakuOW.HRef = UrlConst.SEARCH_BUKKEN
            '業務メニュー / 修正・経理
            linkKeiriHansokuhin.HRef = "javascript:void(0)"
            linkKeiriHansokuhin.Attributes("onclick") = "dispSubMenu(this);return false;"
            linkTeibetuSyuusei.HRef = "javascript:void(0)"
            linkTeibetuSyuusei.Attributes("onclick") = "callModalBukken('" & UrlConst.POPUP_BUKKEN_SITEI & "','" & UrlConst.TEIBETU_SYUUSEI & "','teibetu',event.shiftKey==false,'','');return false;"
            linkTeibetuSyuuseiOW.HRef = "javascript:void(0)"
            linkTeibetuSyuuseiOW.Attributes("onclick") = "callModalBukken('" & UrlConst.POPUP_BUKKEN_SITEI & "','" & UrlConst.TEIBETU_SYUUSEI & "','teibetu',false,'','');return false;"
            linkTeibetuNyuukinSyuusei.HRef = "javascript:void(0)"
            linkTeibetuNyuukinSyuusei.Attributes("onclick") = "callModalBukken('" & UrlConst.POPUP_BUKKEN_SITEI & "','" & UrlConst.TEIBETU_NYUUKIN_SYUUSEI & "','teinyuu',event.shiftKey==false,'','');return false;"
            linkTeibetuNyuukinSyuuseiOW.HRef = "javascript:void(0)"
            linkTeibetuNyuukinSyuuseiOW.Attributes("onclick") = "callModalBukken('" & UrlConst.POPUP_BUKKEN_SITEI & "','" & UrlConst.TEIBETU_NYUUKIN_SYUUSEI & "','teinyuu',false,'','');return false;"

            '2009/12/1  追加 gaoy4 st

            '営業メニュー
            linkEigyouMenu.HRef = "javascript:void(0)"
            linkEigyouMenu.Attributes("onclick") = "dispSubMenu2(this);return false;"
            '2009/12/1  追加 gaoy4 ed

            ' [ tenmd ] 1:店別データ修正 2:販促品請求 3:売上処理済販促品参照
            ' [ isfc  ] 0:FC以外 1:FC
            linkTenbetuSyuusei.HRef = "javascript:void(0)"
            linkTenbetuSyuusei.Attributes("onclick") = "callModalMise('" & UrlConst.POPUP_MISE_SITEI & "','" & UrlConst.TENBETU_SYUUSEI & "','tenbetu',event.shiftKey==false,'','1','');return false;"
            linkTenbetuSyuuseiOW.HRef = "javascript:void(0)"
            linkTenbetuSyuuseiOW.Attributes("onclick") = "callModalMise('" & UrlConst.POPUP_MISE_SITEI & "','" & UrlConst.TENBETU_SYUUSEI & "','tenbetu',false,'','1','');return false;"
            linkHansokuhinSeikyuu.HRef = "javascript:void(0)"
            linkHansokuhinSeikyuu.Attributes("onclick") = "callModalMise('" & UrlConst.POPUP_MISE_SITEI & "','" & UrlConst.TENBETU_SYUUSEI & "','hansoku',event.shiftKey==false,'','2','');return false;"
            linkHansokuhinSeikyuuOW.HRef = "javascript:void(0)"
            linkHansokuhinSeikyuuOW.Attributes("onclick") = "callModalMise('" & UrlConst.POPUP_MISE_SITEI & "','" & UrlConst.TENBETU_SYUUSEI & "','hansoku',false,'','2','');return false;"

            '2009/12/1  追加 gaoy4 st
            ' 1:加盟店照会 2:物件照会 
            'linkKameitenSyoukai.HRef = "javascript:void(0)"
            'linkKameitenSyoukai.Attributes("onclick") = "callModalMise('" & UrlConst.POPUP_EIGYOUJYOUHOU & "','" & UrlConst.POPUP_EIGYOUJYOUHOU & "','kameitenS',event.shiftKey==false,'','1','');return false;"
            'linkKameitenSyoukaiOW.HRef = "javascript:void(0)"
            'linkKameitenSyoukaiOW.Attributes("onclick") = "callModalMise('" & UrlConst.POPUP_EIGYOUJYOUHOU & "','" & UrlConst.POPUP_EIGYOUJYOUHOU & "','kameitenS',false,'','1','');return false;"
            'linkBukenSyoukai.HRef = "javascript:void(0)"
            'linkBukenSyoukai.Attributes("onclick") = "callModalMise('" & UrlConst.POPUP_BUKKENJYOUHOU & "','" & UrlConst.POPUP_BUKKENJYOUHOU & "','tenbetu',event.shiftKey==false,'','2','');return false;"
            'linkBukenSyoukaiOW.HRef = "javascript:void(0)"
            'linkBukenSyoukaiOW.Attributes("onclick") = "callModalMise('" & UrlConst.POPUP_BUKKENJYOUHOU & "','" & UrlConst.POPUP_BUKKENJYOUHOU & "','tenbetu',false,'','2','');return false;"
            linkKameitenSyoukai.HRef = UrlConst.POPUP_EIGYOUJYOUHOU
            linkKameitenSyoukaiOW.HRef = UrlConst.POPUP_EIGYOUJYOUHOU
            linkBukenSyoukai.HRef = UrlConst.POPUP_BUKKENJYOUHOU
            linkBukenSyoukaiOW.HRef = UrlConst.POPUP_BUKKENJYOUHOU
            '2009/12/1  追加 gaoy4 ed

            linkUriageSiireSakusei.HRef = UrlConst.URIAGE_SIIRE_SAKUSEI
            linkUriageSiireSakuseiOW.HRef = UrlConst.URIAGE_SIIRE_SAKUSEI
            linkGetujiIkkatuSyuusei.HRef = UrlConst.GETUJI_IKKATU_SYUUSEI
            linkGetujiIkkatuSyuuseiOW.HRef = UrlConst.GETUJI_IKKATU_SYUUSEI
            linkNyuukinSyori.HRef = UrlConst.NYUUKIN_SYORI
            linkNyuukinSyoriOW.HRef = UrlConst.NYUUKIN_SYORI
            '照会メニュー
            '2009/12/1  削除 gaoy4 st
            'linkKameitenBukkenSyoukai.HRef = UrlConst.KAMEITEN_BUKKEN_SYOUKAI
            'linkKameitenBukkenSyoukaiOW.HRef = UrlConst.KAMEITEN_BUKKEN_SYOUKAI
            '2009/12/1  削除 gaoy4 ed
            linkKameitenSyoukaiTouroku.HRef = UrlConst.KAMEITEN_SYOUKAI_TOUROKU
            linkKameitenSyoukaiTourokuOW.HRef = UrlConst.KAMEITEN_SYOUKAI_TOUROKU

            checkAuthKengen()

        End If


    End Sub

    ''' <summary>
    ''' ユーザー権限によりリンク状態、ログイン者情報を切り替え
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub checkAuthKengen()

        If user_info IsNot Nothing Then

            busyo.InnerText = user_info.Department
            user_id.InnerText = user_info.Name

            'アカウントNOが無い場合、該当リンクを削除
            If user_info.AccountNo = 0 Then
                '物件検索、経理・販促品、加盟店照会・登録、マスタメンテナンス
                linkBukkenKensaku.HRef = String.Empty
                linkBukkenKensakuOW.HRef = String.Empty
                linkKeiriHansokuhin.HRef = String.Empty
                linkKeiriHansokuhin.Attributes.Remove("onclick")
                'gaoy4 st
                'linkEigyouMenu.HRef = String.Empty
                'linkEigyouMenu.Attributes.Remove("onclick")
                'gaoy4 ed
                linkKameitenSyoukaiTouroku.HRef = String.Empty
                linkKameitenSyoukaiTourokuOW.HRef = String.Empty
            End If

            '新規入力権限が無い場合、受注のリンクを削除
            If user_info.SinkiNyuuryokuKengen = 0 Then
                linkIrai.HRef = ""
            End If
            '営業所マスタ管理権限、販促売上権限が何れも無い場合、販促品請求のリンクを削除
            If user_info.EigyouMasterKanriKengen = 0 And user_info.HansokuUriageKengen = 0 Then
                linkHansokuhinSeikyuu.HRef = String.Empty
                linkHansokuhinSeikyuu.Attributes.Remove("onclick")
                linkHansokuhinSeikyuuOW.HRef = String.Empty
                linkHansokuhinSeikyuuOW.Attributes.Remove("onclick")
            End If

            '経理業務権限、発注書管理権限が何れも無い場合、邸別データ修正のリンクを削除
            If user_info.KeiriGyoumuKengen = 0 And user_info.HattyuusyoKanriKengen = 0 Then
                linkTeibetuSyuusei.HRef = String.Empty
                linkTeibetuSyuusei.Attributes.Remove("onclick")
                linkTeibetuSyuuseiOW.HRef = String.Empty
                linkTeibetuSyuuseiOW.Attributes.Remove("onclick")
            End If
            '2009/12/1  追加 gaoy4 st


            '2009/12/1  追加 gaoy4 ed

            '経理業務権限が無い場合、該当リンクを削除
            If user_info.KeiriGyoumuKengen = 0 Then
                linkTeibetuNyuukinSyuusei.HRef = String.Empty
                linkTeibetuNyuukinSyuusei.Attributes.Remove("onclick")
                linkTeibetuNyuukinSyuuseiOW.HRef = String.Empty
                linkTeibetuNyuukinSyuuseiOW.Attributes.Remove("onclick")
                linkTenbetuSyuusei.HRef = String.Empty
                linkTenbetuSyuuseiOW.HRef = String.Empty
                linkHansokuhinSeikyuu.HRef = String.Empty
                linkHansokuhinSeikyuuOW.HRef = String.Empty
                linkUriageSiireSakusei.HRef = String.Empty
                linkUriageSiireSakuseiOW.HRef = String.Empty
                linkGetujiIkkatuSyuusei.HRef = String.Empty
                linkGetujiIkkatuSyuuseiOW.HRef = String.Empty
                linkNyuukinSyori.HRef = String.Empty
                linkNyuukinSyoriOW.HRef = String.Empty
            End If

        Else
            'linkMain.HRef = String.Empty
            ' 地盤認証マスタ情報取得不可の場合、地盤のメニュー使用不可
            linkIrai.HRef = String.Empty
            linkBukkenKensaku.HRef = String.Empty
            linkBukkenKensakuOW.HRef = String.Empty
            linkKeiriHansokuhin.HRef = String.Empty
            linkKeiriHansokuhin.Attributes.Remove("onclick")

            '2009/12/1  追加 gaoy4 st
            linkEigyouMenu.HRef = String.Empty
            linkEigyouMenu.Attributes.Remove("onclick")
            '2009/12/1  追加 gaoy4 ed

            '2009/12/1  削除 gaoy4 st
            'linkKameitenBukkenSyoukai.HRef = String.Empty
            'linkKameitenBukkenSyoukaiOW.HRef = String.Empty
            '2009/12/1  削除 gaoy4 ed
            linkKameitenSyoukaiTouroku.HRef = String.Empty
            linkKameitenSyoukaiTourokuOW.HRef = String.Empty
        End If

    End Sub

End Class

Partial Public Class main
    Inherits System.Web.UI.Page

    Dim userInfo As New LoginUserInfo

    ''' <summary>
    ''' ページロード
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim jBn As New Jiban '地盤画面共通クラス

        ' ユーザー基本認証
        jBn.UserAuth(userInfo)

        '業務メニュー
        LinkSinkiJutyuu.HRef = "javascript:void(0)"
        LinkSinkiJutyuu.Attributes("onclick") = "changeDisplay('" & UlSinkiJutyuuList.ClientID & "'); return false;"
        LinkMousikomiNyuuryoku.HRef = UrlConst.MOUSIKOMI_INPUT
        LinkJutyuuTouroku.HRef = UrlConst.IRAI_STEP_1 & "?st=" & "" & EarthConst.MODE_NEW
        LinkMousikomiKensaku.HRef = UrlConst.SEARCH_MOUSIKOMI
        LinkMousikomiKensakuOW.HRef = UrlConst.SEARCH_MOUSIKOMI
        LinkFcMousikomiKensaku.HRef = UrlConst.SEARCH_FC_MOUSIKOMI
        LinkFcMousikomiKensakuOW.HRef = UrlConst.SEARCH_FC_MOUSIKOMI

        LinkMenuBukkenKensaku.HRef = "javascript:void(0)"
        LinkMenuBukkenKensaku.Attributes("onclick") = "changeDisplay('" & UlBukkenKensakuList.ClientID & "'); return false;"
        LinkBukkenKensaku.HRef = UrlConst.SEARCH_BUKKEN
        LinkBukkenKensakuOW.HRef = UrlConst.SEARCH_BUKKEN
        LinkHinsituHosyousyoJyoukyouKensaku.HRef = UrlConst.SEARCH_HINSITU_HOSYOUSYO_JYOUKYOU
        LinkHinsituHosyousyoJyoukyouKensakuOW.HRef = UrlConst.SEARCH_HINSITU_HOSYOUSYO_JYOUKYOU

        LinkSearchKousinRireki.HRef = UrlConst.SEARCH_KOUSIN_RIREKI
        LinkSearchKousinRirekiOW.HRef = UrlConst.SEARCH_KOUSIN_RIREKI
        LinkTyousaMitumoriYouDataSyuturyoku.HRef = UrlConst.EARTH2_TYOUSA_MITUMORI_YOU_DATA_SYUTURYOKU
        LinkTyousaMitumoriYouDataSyuturyokuOW.HRef = UrlConst.EARTH2_TYOUSA_MITUMORI_YOU_DATA_SYUTURYOKU
        '業務メニュー / 経理メニュー
        LinkKeiriMenu.HRef = UrlConst.KEIRI_MENU
        LinkKeiriMenuOW.HRef = UrlConst.KEIRI_MENU
        '業務メニュー / 販促品請求
        ' [ tenmd ] 1:店別データ修正 2:販促品請求 3:売上処理済販促品参照
        ' [ isfc  ] 0:FC以外 1:FC
        LinkHansokuhinSeikyuu.HRef = "javascript:void(0)"
        LinkHansokuhinSeikyuu.Attributes("onclick") = _
                    "callModalMise('" & UrlConst.POPUP_MISE_SITEI & "','" & UrlConst.TENBETU_SYUUSEI & "','hansoku',event.shiftKey==false,'','2','');return false;"
        LinkHansokuhinSeikyuuOW.HRef = "javascript:void(0)"
        LinkHansokuhinSeikyuuOW.Attributes("onclick") = _
                    "callModalMise('" & UrlConst.POPUP_MISE_SITEI & "','" & UrlConst.TENBETU_SYUUSEI & "','hansoku',false,'','2','');return false;"
        '物件ダイレクト
        LinkBukkenDirect.HRef = UrlConst.POPUP_GAMEN_KIDOU
        '登録・照会メニュー
        LinkKameitenSyoukaiTouroku.HRef = UrlConst.EARTH2_KAMEITEN_SYOUKAI_TOUROKU
        LinkKameitenSyoukaiTourokuOW.HRef = UrlConst.EARTH2_KAMEITEN_SYOUKAI_TOUROKU
        LinkKameitenTyuuijouhouDirect.HRef = UrlConst.EARTH2_KAMEITEN_TYUUIJOUHOU_DIRECT
        LinkKameitenTyuuijouhouDirectOW.HRef = UrlConst.EARTH2_KAMEITEN_TYUUIJOUHOU_DIRECT
        '営業向けメニュー
        LinkKameitenSyoukai.HRef = UrlConst.EARTH2_KAMEITEN_SYOUKAI
        LinkKameitenSyoukaiOW.HRef = UrlConst.EARTH2_KAMEITEN_SYOUKAI
        LinkBukkenSyoukai.HRef = UrlConst.EARTH2_BUKKEN_SYOUKAI
        LinkBukkenSyoukaiOW.HRef = UrlConst.EARTH2_BUKKEN_SYOUKAI
        '管理メニュー
        LinkHidukeMaster.HRef = UrlConst.MASTER_HIDUKE
        LinkMasterMaintenance.HRef = UrlConst.EARTH2_MASTER_MAINTENANCE

        ' ユーザー権限によりリンク状態、ログイン者情報を切り替え
        checkAuthKengen()

        'お知らせ表示
        setInfo()

    End Sub

    ''' <summary>
    ''' お知らせ情報を取得し、画面表示コントロールを作成する
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setInfo()
        Dim today As Date = DateAndTime.Today
        Dim JibanLogic As New JibanLogic
        Dim dataArray As New List(Of OsiraseRecord)
        Dim lineCount As New Integer
        Dim limitCounter As New Integer   '表示上限件数カウンタ

        'お知らせ情報を取得
        dataArray = JibanLogic.GetOsiraseRecords()

        'カウンタ初期化
        limitCounter = 0

        lineCount = dataArray.Count

        'データが取得できた場合に処理実行
        If lineCount > 0 Then
            For Each recData As OsiraseRecord In dataArray
                'お知らせ表示テーブル生成

                Dim objTr1 As New HtmlTableRow
                Dim objTr2 As New HtmlTableRow
                Dim objTr3 As New HtmlTableRow

                Dim objTdNew As New HtmlTableCell
                Dim objImgNew As New HtmlImage
                Dim objTdDate As New HtmlTableCell
                Dim objTdBusyoMei As New HtmlTableCell
                Dim objTdNaiyou As New HtmlTableCell

                Dim objTdBlank1 As New HtmlTableCell
                Dim objAncLink As New HtmlAnchor

                Dim objTdBlank2 As New HtmlTableCell

                '******** 1行目 ********
                '一週間前以降の日付の場合、「NEW」アイコンを付与
                If recData.Nengappi.Date >= Date.Now.AddDays(-7) Then
                    objImgNew.Src = "images/new.gif"
                    objImgNew.Alt = "new"
                    objTdNew.Controls.Add(objImgNew)
                End If

                objTdDate.InnerHtml = "【" & recData.Nengappi.Date & "】"
                objTdBusyoMei.InnerHtml = "&nbsp;&nbsp;" & recData.NyuuryokuBusyo & "&nbsp;&nbsp;" & recData.NyuuryokuMei
                objTdBusyoMei.Style("width") = "400px"

                '******** 2行目 ********
                objTdBlank1.ColSpan = 2
                objAncLink.Target = "_blunk"
                objAncLink.HRef = Trim(recData.LinkSaki)
                objAncLink.InnerHtml = recData.HyoujiNaiyou
                objTdNaiyou.Controls.Add(objAncLink)
                objTdNaiyou.Style("width") = "400px"

                '******** 3行目 ********
                objTdBlank2.Style("height") = "15px"
                objTdBlank2.ColSpan = 3

                '各、行コントロールに格納
                objTr1.Controls.Add(objTdNew)
                objTr1.Controls.Add(objTdDate)
                objTr1.Controls.Add(objTdBusyoMei)

                objTr2.Controls.Add(objTdBlank1)
                objTr2.Controls.Add(objTdNaiyou)

                objTr3.Controls.Add(objTdBlank2)

                'tbodyに挿入
                osiraseTbody.Controls.Add(objTr1)
                osiraseTbody.Controls.Add(objTr2)
                osiraseTbody.Controls.Add(objTr3)

                'カウントアップ
                limitCounter += 1

                '上限件数チェック
                If limitCounter >= EarthConst.OSIRASE_LIMIT_COUNT Then
                    Exit For
                End If
            Next

        End If

    End Sub

    ''' <summary>
    ''' ユーザー権限によりリンク状態、ログイン者情報を切り替え
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub checkAuthKengen()

        If userInfo IsNot Nothing Then

            'アカウントNOが無い場合、該当リンクを削除
            If userInfo.AccountNo = 0 Then
                '物件検索、経理・販促品、物件ダイレクト、更新履歴、加盟店照会・登録、加盟店注意情報ダイレクト、マスタメンテナンス、更新履歴照会
                LinkMenuBukkenKensaku.HRef = String.Empty
                LinkMenuBukkenKensaku.Attributes.Remove("onclick")
                LinkBukkenKensaku.HRef = String.Empty
                LinkBukkenKensakuOW.HRef = String.Empty
                LinkHinsituHosyousyoJyoukyouKensaku.HRef = String.Empty
                LinkHinsituHosyousyoJyoukyouKensakuOW.HRef = String.Empty
                LinkSinkiJutyuu.HRef = String.Empty
                LinkSinkiJutyuu.Attributes.Remove("onclick")
                LinkKeiriMenu.HRef = String.Empty
                LinkKeiriMenuOW.HRef = String.Empty
                LinkBukkenDirect.HRef = String.Empty
                LinkSearchKousinRireki.HRef = String.Empty
                LinkSearchKousinRirekiOW.HRef = String.Empty
                LinkKameitenSyoukaiTouroku.HRef = String.Empty
                LinkKameitenSyoukaiTourokuOW.HRef = String.Empty
                LinkKameitenTyuuijouhouDirect.HRef = String.Empty
                LinkKameitenTyuuijouhouDirectOW.HRef = String.Empty
                LinkMasterMaintenance.HRef = String.Empty
                LinkSearchKousinRireki.HRef = String.Empty
                LinkSearchKousinRirekiOW.HRef = String.Empty
            End If

            '新規入力権限が無い場合、受注・見積書作成用データ出力のリンクを削除
            If userInfo.SinkiNyuuryokuKengen = 0 Then
                LinkSinkiJutyuu.HRef = String.Empty
                LinkSinkiJutyuu.Attributes.Remove("onclick")
                LinkMousikomiNyuuryoku.HRef = String.Empty
                LinkJutyuuTouroku.HRef = String.Empty
                LinkMousikomiKensaku.HRef = String.Empty
                LinkMousikomiKensakuOW.HRef = String.Empty
                LinkFcMousikomiKensaku.HRef = String.Empty
                LinkFcMousikomiKensakuOW.HRef = String.Empty
                LinkMousikomiKensakuOW.Attributes.Remove("onclick")
                LinkTyousaMitumoriYouDataSyuturyoku.HRef = String.Empty
                LinkTyousaMitumoriYouDataSyuturyokuOW.HRef = String.Empty
                LinkTyousaMitumoriYouDataSyuturyoku.HRef = String.Empty
                LinkTyousaMitumoriYouDataSyuturyokuOW.HRef = String.Empty
            End If

            '営業所マスタ管理権限、販促売上権限が何れも無い場合、販促品請求のリンクを削除
            If userInfo.EigyouMasterKanriKengen = 0 And userInfo.HansokuUriageKengen = 0 Then
                LinkHansokuhinSeikyuu.HRef = String.Empty
                LinkHansokuhinSeikyuu.Attributes.Remove("onclick")
                LinkHansokuhinSeikyuuOW.HRef = String.Empty
                LinkHansokuhinSeikyuuOW.Attributes.Remove("onclick")
            End If

            '新規入力権限、保証書権限、報告書権限が何れも無い場合、日付マスタ更新のリンクを削除
            If userInfo.SinkiNyuuryokuKengen = 0 And userInfo.HosyouGyoumuKengen = 0 And userInfo.HoukokusyoGyoumuKengen = 0 Then
                LinkHidukeMaster.HRef = String.Empty
                LinkHidukeMaster.Attributes.Remove("onclick")
            End If

            '経理業務権限が無い場合、該当リンクを削除
            If userInfo.KeiriGyoumuKengen = 0 Then
                LinkKeiriMenu.HRef = String.Empty
                LinkKeiriMenuOW.HRef = String.Empty
            End If

        Else
            ' 地盤認証マスタ情報取得不可の場合、地盤のメニュー使用不可
            LinkSinkiJutyuu.HRef = String.Empty
            LinkSinkiJutyuu.Attributes.Remove("onclick")
            LinkMousikomiNyuuryoku.HRef = String.Empty
            LinkJutyuuTouroku.HRef = String.Empty
            LinkMousikomiKensaku.HRef = String.Empty
            LinkMousikomiKensakuOW.HRef = String.Empty
            LinkMousikomiKensakuOW.Attributes.Remove("onclick")
            LinkFcMousikomiKensaku.HRef = String.Empty
            LinkFcMousikomiKensakuOW.HRef = String.Empty
            LinkFcMousikomiKensakuOW.Attributes.Remove("onclick")

            LinkMenuBukkenKensaku.HRef = String.Empty
            LinkMenuBukkenKensaku.Attributes.Remove("onclick")
            LinkBukkenKensaku.HRef = String.Empty
            LinkBukkenKensakuOW.HRef = String.Empty
            LinkHinsituHosyousyoJyoukyouKensaku.HRef = String.Empty
            LinkHinsituHosyousyoJyoukyouKensakuOW.HRef = String.Empty

            LinkKeiriMenu.HRef = String.Empty
            LinkKeiriMenuOW.HRef = String.Empty
            LinkBukkenDirect.HRef = String.Empty

            LinkKameitenSyoukaiTouroku.HRef = String.Empty
            LinkKameitenSyoukaiTourokuOW.HRef = String.Empty
            LinkKameitenTyuuijouhouDirect.HRef = String.Empty
            LinkKameitenTyuuijouhouDirectOW.HRef = String.Empty

            LinkKameitenSyoukai.HRef = String.Empty
            LinkKameitenSyoukaiOW.HRef = String.Empty
            LinkBukkenSyoukai.HRef = String.Empty
            LinkBukkenSyoukaiOW.HRef = String.Empty

            LinkHidukeMaster.HRef = String.Empty
            LinkHidukeMaster.Attributes.Remove("onclick")
            LinkMasterMaintenance.HRef = String.Empty

            '更新履歴照会
            LinkSearchKousinRireki.HRef = String.Empty
            LinkSearchKousinRirekiOW.HRef = String.Empty
            '販促品請求
            LinkHansokuhinSeikyuu.HRef = String.Empty
            LinkHansokuhinSeikyuu.Attributes.Remove("onclick")
            LinkHansokuhinSeikyuuOW.HRef = String.Empty
            LinkHansokuhinSeikyuuOW.Attributes.Remove("onclick")
            '調査見積書作成
            LinkTyousaMitumoriYouDataSyuturyoku.HRef = String.Empty
            LinkTyousaMitumoriYouDataSyuturyokuOW.HRef = String.Empty

        End If

    End Sub

End Class
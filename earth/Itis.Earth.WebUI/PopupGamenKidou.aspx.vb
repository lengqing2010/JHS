
Partial Public Class PopupGamenKidou
    Inherits System.Web.UI.Page

    Dim userInfo As New LoginUserInfo

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim jBn As New Jiban '地盤画面共通クラス

        ' ユーザー基本認証
        jBn.UserAuth(userInfo)

        If IsPostBack = False Then

            ' コンボ設定ヘルパークラスを生成
            Dim helper As New DropDownHelper
            ' 区分コンボにデータをバインドする
            helper.SetDropDownList(SelectKubun, DropDownHelper.DropDownType.Kubun)

            '文字列受け取り
            If Request("kbn") IsNot Nothing Then
                SelectKubun.SelectedValue = Request("kbn")
            End If
            If Request("no") IsNot Nothing Then
                TextNo.Value = Request("no")
                '存在チェック
                ButtonCheckIsJibanData_ServerClick(sender, e)
            End If

            If userInfo IsNot Nothing Then

                SelectKubun.Attributes("onchange") = "checkJibanData(this);"
                TextNo.Attributes("onblur") = "checkNumber(this);"
                TextNo.Attributes("onkeydown") = "if(event.keyCode==13){return false;}else{};"
                TextNo.Attributes("onkeyup") = "if(event.keyCode==16 || event.keyCode==9){return false;}else{checkJibanData(this);};"
                TextNo.Attributes("onmouseup") = "checkJibanData(this);"
                TextNo.Attributes("onchange") = "checkJibanData(this);"

                Dim tmpScript As String = "funcGamenKidou('{0}',{1});"
                ButtonJutyuu.Attributes("onclick") = String.Format(tmpScript, UrlConst.IRAI_KAKUNIN, 1)
                ButtonHoukokusyo.Attributes("onclick") = String.Format(tmpScript, UrlConst.HOUKOKUSYO, 1)
                ButtonKairyouKouji.Attributes("onclick") = String.Format(tmpScript, UrlConst.KAIRYOU_KOUJI, 1)
                ButtonHosyou.Attributes("onclick") = String.Format(tmpScript, UrlConst.HOSYOU, 1)
                ButtonTeibetuSyuusei.Attributes("onclick") = String.Format(tmpScript, UrlConst.TEIBETU_SYUUSEI, 1)
                ButtonTeibetuNyuukinSyuusei.Attributes("onclick") = String.Format(tmpScript, UrlConst.TEIBETU_NYUUKIN_SYUUSEI, 1)
                ButtonPopupBukkenRireki.Attributes("onclick") = String.Format(tmpScript, UrlConst.POPUP_BUKKEN_RIREKI, 1)
                ButtonPopupTokubetuTaiou.Attributes("onclick") = String.Format(tmpScript, UrlConst.POPUP_TOKUBETU_TAIOU, 1)
                ButtonSearchMousikomi.Attributes("onclick") = String.Format(tmpScript, UrlConst.SEARCH_MOUSIKOMI, 1)
                ButtonSearchFcMousikomi.Attributes("onclick") = String.Format(tmpScript, UrlConst.SEARCH_FC_MOUSIKOMI, 1)

                Dim tmpDummyScript As String = "javascript:void(0)"
                Dim tmpAncScript As String = "funcGamenKidou('{0}',{1});return false;"
                AncJutyuuOW.HRef = tmpDummyScript
                AncJutyuuOW.Attributes("onclick") = String.Format(tmpAncScript, UrlConst.IRAI_KAKUNIN, 0)
                AncHoukokusyoOW.HRef = tmpDummyScript
                AncHoukokusyoOW.Attributes("onclick") = String.Format(tmpAncScript, UrlConst.HOUKOKUSYO, 0)
                AncKairyouKoujiOW.HRef = tmpDummyScript
                AncKairyouKoujiOW.Attributes("onclick") = String.Format(tmpAncScript, UrlConst.KAIRYOU_KOUJI, 0)
                AncHosyouOW.HRef = tmpDummyScript
                AncHosyouOW.Attributes("onclick") = String.Format(tmpAncScript, UrlConst.HOSYOU, 0)
                AncTeibetuSyuuseiOW.HRef = tmpDummyScript
                AncTeibetuSyuuseiOW.Attributes("onclick") = String.Format(tmpAncScript, UrlConst.TEIBETU_SYUUSEI, 0)
                AncTeibetuNyuukinSyuuseiOW.HRef = tmpDummyScript
                AncTeibetuNyuukinSyuuseiOW.Attributes("onclick") = String.Format(tmpAncScript, UrlConst.TEIBETU_NYUUKIN_SYUUSEI, 0)
                AncButtonPopupBukkenRirekiOW.HRef = tmpDummyScript
                AncButtonPopupBukkenRirekiOW.Attributes("onclick") = String.Format(tmpAncScript, UrlConst.POPUP_BUKKEN_RIREKI, 0)
                AncButtonPopupTokubetuTaiouOW.HRef = tmpDummyScript
                AncButtonPopupTokubetuTaiouOW.Attributes("onclick") = String.Format(tmpAncScript, UrlConst.POPUP_TOKUBETU_TAIOU, 0)
                AncButtonSearchMousikomiOW.HRef = tmpDummyScript
                AncButtonSearchMousikomiOW.Attributes("onclick") = String.Format(tmpAncScript, UrlConst.SEARCH_MOUSIKOMI, 0)
                AncButtonSearchFcMousikomiOW.HRef = tmpDummyScript
                AncButtonSearchFcMousikomiOW.Attributes("onclick") = String.Format(tmpAncScript, UrlConst.SEARCH_FC_MOUSIKOMI, 0)


                '権限別ボタン無効化処理
                'アカウントNOが無い場合、該当ボタンを設定しない
                If userInfo.AccountNo = 0 Then
                    '物件検索、経理・販促品、加盟店照会・登録、マスタメンテナンス
                    ButtonJutyuu.Disabled = True
                    ButtonHoukokusyo.Disabled = True
                    ButtonKairyouKouji.Disabled = True
                    ButtonHosyou.Disabled = True
                    ButtonTeibetuSyuusei.Disabled = True
                    ButtonTeibetuNyuukinSyuusei.Disabled = True
                    ButtonPopupBukkenRireki.Disabled = True
                    ButtonPopupTokubetuTaiou.Disabled = True
                    ButtonSearchMousikomi.Disabled = True
                    ButtonSearchFcMousikomi.Disabled = True
                    removeAncLink(AncJutyuuOW)
                    removeAncLink(AncHoukokusyoOW)
                    removeAncLink(AncKairyouKoujiOW)
                    removeAncLink(AncHosyouOW)
                    removeAncLink(AncTeibetuSyuuseiOW)
                    removeAncLink(AncTeibetuNyuukinSyuuseiOW)
                    removeAncLink(AncButtonPopupBukkenRirekiOW)
                    removeAncLink(AncButtonPopupTokubetuTaiouOW)
                    removeAncLink(AncButtonSearchMousikomiOW)
                    removeAncLink(AncButtonSearchFcMousikomiOW)
                End If

                '新規入力権限が無い場合、受注のリンクを削除
                If userInfo.SinkiNyuuryokuKengen = 0 Then

                End If

                '営業所マスタ管理権限、販促売上権限が何れも無い場合、販促品請求のリンクを削除
                If userInfo.EigyouMasterKanriKengen = 0 And userInfo.HansokuUriageKengen = 0 Then

                End If

                '経理業務権限、発注書管理権限が何れも無い場合、邸別データ修正のリンクを削除
                If userInfo.KeiriGyoumuKengen = 0 And userInfo.HattyuusyoKanriKengen = 0 Then
                    ButtonTeibetuSyuusei.Disabled = True
                    removeAncLink(AncTeibetuSyuuseiOW)
                End If

                '経理業務権限が無い場合、該当リンクを削除
                If userInfo.KeiriGyoumuKengen = 0 Then
                    ButtonTeibetuNyuukinSyuusei.Disabled = True
                    removeAncLink(AncTeibetuNyuukinSyuuseiOW)
                End If

                '新規入力権限、保証書権限、報告書権限が何れも無い場合、日付マスタ更新のリンクを削除
                If userInfo.SinkiNyuuryokuKengen = 0 And userInfo.HosyouGyoumuKengen = 0 And userInfo.HoukokusyoGyoumuKengen = 0 Then

                End If


            Else
                ' 地盤認証マスタ情報取得不可の場合、地盤のメニュー使用不可
                ButtonJutyuu.Disabled = True
                ButtonHoukokusyo.Disabled = True
                ButtonKairyouKouji.Disabled = True
                ButtonHosyou.Disabled = True
                ButtonTeibetuSyuusei.Disabled = True
                ButtonTeibetuNyuukinSyuusei.Disabled = True
                ButtonPopupBukkenRireki.Disabled = True
                ButtonPopupTokubetuTaiou.Disabled = True
                ButtonSearchMousikomi.Disabled = True
                ButtonSearchFcMousikomi.Disabled = True
                removeAncLink(AncJutyuuOW)
                removeAncLink(AncHoukokusyoOW)
                removeAncLink(AncKairyouKoujiOW)
                removeAncLink(AncHosyouOW)
                removeAncLink(AncTeibetuSyuuseiOW)
                removeAncLink(AncTeibetuNyuukinSyuuseiOW)
                removeAncLink(AncButtonPopupBukkenRirekiOW)
                removeAncLink(AncButtonPopupTokubetuTaiouOW)
                removeAncLink(AncButtonSearchMousikomiOW)
                removeAncLink(AncButtonSearchFcMousikomiOW)
            End If

            'フォーカス
            If SelectKubun.SelectedValue = String.Empty Then
                '区分未指定の場合、区分にフォーカス
                SetFocus(SelectKubun)
            Else
                '区分設定済みの場合、受注ボタンにフォーカス
                SetFocus(ButtonJutyuu)
            End If

        End If

    End Sub

    ''' <summary>
    ''' アンカーコントロールのリンク先情報等をクリア
    ''' </summary>
    ''' <param name="ctrlAnc"></param>
    ''' <remarks></remarks>
    Private Sub removeAncLink(ByVal ctrlAnc As HtmlAnchor)
        ctrlAnc.HRef = String.Empty
        ctrlAnc.Attributes.Remove("onclick")
    End Sub

    ''' <summary>
    ''' 地盤データの存在チェック
    ''' </summary>
    ''' <param name="kbn"></param>
    ''' <param name="no"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsJibanData(ByVal kbn As String, ByVal no As String) As Boolean
        ' 地盤データの存在チェック
        Dim logic As New JibanLogic
        Return logic.ExistsJibanData(kbn, no)
    End Function

    ''' <summary>
    ''' 地盤データの存在チェック実行ボタン押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ButtonCheckIsJibanData_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCheckIsJibanData.ServerClick
        If IsJibanData(SelectKubun.SelectedValue, TextNo.Value) Then
            HiddenIsJibanData.Value = SelectKubun.SelectedValue & TextNo.Value
            HiddenIsJibanDataOld.Value = HiddenIsJibanData.Value
        Else
            HiddenIsJibanData.Value = Boolean.FalseString
        End If
    End Sub
End Class
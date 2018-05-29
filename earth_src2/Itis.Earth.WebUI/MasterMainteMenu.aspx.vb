Imports Itis.Earth.BizLogic

Partial Public Class MasterMainteMenu
    Inherits System.Web.UI.Page

    ''' <summary>マスタメンテナンス</summary>
    ''' <remarks>マスタメンテナンスを提供する</remarks>
    ''' <history>
    ''' <para>2009/07/15　付龍(大連情報システム部)　新規作成</para>
    ''' </history>
    Private commonCheck As New CommonCheck

    ''' <summary>ページロッド</summary>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '権限チェック
        Dim user_info As New LoginUserInfo
        Dim ninsyou As New Ninsyou()
        Dim jBn As New Jiban '地盤画面共通クラス
        ' ユーザー基本認証
        jBn.userAuth(user_info)
        If ninsyou.GetUserID() = String.Empty Then
            Context.Items("strFailureMsg") = Messages.Instance.MSG2024E
            Server.Transfer("CommonErr.aspx")
        End If
        If user_info Is Nothing Then
            Me.btnWaribikiMaster.Enabled = False
            Me.btnKeiretuMaster.Enabled = False
            btnEigyousyoMaster.Enabled = False
            btnTyousaKaisyaMaster.Enabled = False
            btnSiireMaster.Enabled = False
            btnSeikyuuSakiMaster.Enabled = False
            btnSyouhinMaster.Enabled = False
            btnSyouhinKakakusetteiMaster.Enabled = False
            btnWaribikiMaster.Enabled = False
            btnTokuteitenSyouhin2SetteiMaster.Enabled = False
            btnSeikyuuSakiHenkouMaster.Enabled = False

            btnKairyKojSyubetuMaster.Enabled = False
            btnHanteiKojiSyubetuMaster.Enabled = False
            btnTantousyaMaster.Enabled = False
            btnUserKanri.Enabled = False

            btnKakakuMaster.Enabled = False
            '=========================2011/04/25 車龍 追加 開始↓===============================
            '｢加盟店商品調査方法特別対応マスタ｣ボタン
            Me.btnTokubetuTaiou.Enabled = False
            '=========================2011/04/25 車龍 追加 終了↑===============================
            btnKoujiKakakuMaster.Enabled = False
        Else
            If user_info.Account Is Nothing Then
                Me.btnWaribikiMaster.Enabled = False
                Me.btnKeiretuMaster.Enabled = False
                btnEigyousyoMaster.Enabled = False
                btnTyousaKaisyaMaster.Enabled = False
                btnSiireMaster.Enabled = False
                btnSeikyuuSakiMaster.Enabled = False
                btnSyouhinMaster.Enabled = False
                btnSyouhinKakakusetteiMaster.Enabled = False
                btnWaribikiMaster.Enabled = False
                btnTokuteitenSyouhin2SetteiMaster.Enabled = False
                btnSeikyuuSakiHenkouMaster.Enabled = False

                btnKairyKojSyubetuMaster.Enabled = False
                btnHanteiKojiSyubetuMaster.Enabled = False
                btnTantousyaMaster.Enabled = False
                btnUserKanri.Enabled = False

                btnKakakuMaster.Enabled = False
                '=========================2011/04/25 車龍 追加 開始↓===============================
                '｢加盟店商品調査方法特別対応マスタ｣ボタン
                Me.btnTokubetuTaiou.Enabled = False
                '=========================2011/04/25 車龍 追加 終了↑===============================
                btnKoujiKakakuMaster.Enabled = False
            Else
                Me.btnWaribikiMaster.Enabled = True
                Me.btnKeiretuMaster.Enabled = True
                btnEigyousyoMaster.Enabled = True
                btnTyousaKaisyaMaster.Enabled = True
                btnSiireMaster.Enabled = True
                btnSeikyuuSakiMaster.Enabled = True
                btnSyouhinMaster.Enabled = True
                btnSyouhinKakakusetteiMaster.Enabled = True
                btnWaribikiMaster.Enabled = True
                btnTokuteitenSyouhin2SetteiMaster.Enabled = True
                btnSeikyuuSakiHenkouMaster.Enabled = True

                btnKairyKojSyubetuMaster.Enabled = True
                btnHanteiKojiSyubetuMaster.Enabled = True
                btnTantousyaMaster.Enabled = True
                btnUserKanri.Enabled = True

                btnKakakuMaster.Enabled = True
                '=========================2011/04/25 車龍 追加 開始↓===============================
                '｢加盟店商品調査方法特別対応マスタ｣ボタン
                Me.btnTokubetuTaiou.Enabled = True
                '=========================2011/04/25 車龍 追加 終了↑===============================
                btnKoujiKakakuMaster.Enabled = True
            End If
        End If
        

        If Not IsPostBack Then
            '参照履歴管理テーブルを登録する。
            commonCheck.SetURL(Me, ninsyou.GetUserID())
        End If
        Me.Page.Form.Attributes.Add("onkeydown", "if(event.keyCode==13){return false;}")

        '=========================2011/04/25 車龍 追加 開始↓===============================
        '｢販売価格マスタ｣ボタン
        Me.btnKakakuMaster.Attributes.Add("onClick", "ShowPopup('HanbaiKakakuMasterSearchList.aspx');return false;")
        '｢原価マスタ｣ボタン
        Me.btnSiireMaster.Attributes.Add("onClick", "ShowPopup('GenkaMasterSearchList.aspx');return false;")
        '｢加盟店商品調査方法特別対応マスタ｣ボタン
        Me.btnTokubetuTaiou.Attributes.Add("onClick", "ShowPopup('TokubetuTaiouMasterSearchList.aspx');return false;")
        '=========================2011/04/25 車龍 追加 終了↑===============================

        Me.btnKoujiKakakuMaster.Attributes.Add("onClick", "ShowPopup('KoujiKakakuMasterSearchList.aspx');return false;")

    End Sub

    ''' <summary>ユーザー管理ボタンをクリック時</summary>
    Private Sub btnUserKanri_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUserKanri.Click
        Server.Transfer("KanrisyaMenuInquiryInput.aspx")
    End Sub

    ''' <summary>多棟割引マスタボタンをクリック時</summary>
    Private Sub btnWaribikiMaster_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnWaribikiMaster.Click
        Server.Transfer("WaribikiMasterSearch.aspx")
    End Sub

    ''' <summary>戻るボタンをクリック時</summary>
    Private Sub btnModoru_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModoru.Click
        Response.Redirect(UrlConst.MAIN)
    End Sub

    Protected Sub btnKeiretuMaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKeiretuMaster.Click
        Server.Transfer("KeiretuMaster.aspx")
    End Sub

    Protected Sub btnSyouhinMaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSyouhinMaster.Click
        Server.Transfer("SyouhinMaster.aspx")
    End Sub



    Protected Sub btnEigyousyoMaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEigyousyoMaster.Click
        Server.Transfer("EigyousyoMaster.aspx")
    End Sub

    Protected Sub btnTyousaKaisyaMaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTyousaKaisyaMaster.Click
        Server.Transfer("TyousaKaisyaMaster.aspx")
    End Sub

    '=========================2011/04/25 車龍 追加 削除 開始↓===============================
    'Protected Sub btnSiireMaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSiireMaster.Click
    '    'Server.Transfer("SiireMaster.aspx")
    '    Server.Transfer("GenkaMasterSearchList.aspx")
    'End Sub
    '=========================2011/04/25 車龍 追加 削除 終了↑===============================

    Protected Sub btnSeikyuuSakiMaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeikyuuSakiMaster.Click
        Server.Transfer("SeikyuuSakiMaster.aspx")
    End Sub

    Protected Sub btnSyouhinKakakusetteiMaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSyouhinKakakusetteiMaster.Click
        Server.Transfer("SyouhinKakakusetteiMaster.aspx")
    End Sub

    Protected Sub btnTokuteitenSyouhin2SetteiMaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTokuteitenSyouhin2SetteiMaster.Click
        Server.Transfer("Syouhin2Master.aspx")
    End Sub

    Protected Sub btnSeikyuuSakiHenkouMaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeikyuuSakiHenkouMaster.Click
        Server.Transfer("SeikyuuSakiHenkouMaster.aspx")
    End Sub

    Protected Sub btnKairyKojSyubetuMaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKairyKojSyubetuMaster.Click
        Server.Transfer("KairyKojSyubetuMaster.aspx")
    End Sub

    Protected Sub btnHanteiKojiSyubetuMaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHanteiKojiSyubetuMaster.Click
        Server.Transfer("HanteiKojiSyubetuMaster.aspx")
    End Sub

    Protected Sub btnTantousyaMaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTantousyaMaster.Click
        Server.Transfer("TantouSyaMaster.aspx")
    End Sub


    '=========================2011/04/25 車龍 追加 削除 開始↓===============================
    '''' <summary>
    '''' 価格マスタボタンを押下すると
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <remarks></remarks>
    'Private Sub btnKakakuMaster_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKakakuMaster.Click
    '    Server.Transfer("HanbaiKakakuMasterSearchList.aspx")
    'End Sub
    '=========================2011/04/25 車龍 削除 終了↑===============================

End Class
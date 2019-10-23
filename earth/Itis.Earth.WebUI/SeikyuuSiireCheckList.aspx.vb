
Partial Public Class SeikyuuSiireCheckList
    Inherits System.Web.UI.Page

    Dim user_info As New LoginUserInfo
    Dim masterAjaxSM As New ScriptManager

    ''' <summary>
    ''' ページロード
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim jBn As New Jiban '地盤画面共通クラス

        'マスターページ情報を取得（ScriptManager用）
        Dim myMaster As EarthMasterPage = Page.Master
        masterAjaxSM = myMaster.AjaxScriptManager1

        ' ユーザー基本認証
        jBn.UserAuth(user_info)

        '認証結果によって画面表示を切替える
        If user_info IsNot Nothing Then

        Else
            Response.Redirect(UrlConst.MAIN)
        End If

        If IsPostBack = False Then

            '****************************************************************************
            ' ドロップダウンリスト設定
            '****************************************************************************
            ' 区分コンボにデータをバインドする
            Dim helper As New DropDownHelper
            Dim kbn_logic As New KubunLogic
            'helper.SetDropDownList(cboKubun_1, DropDownHelper.DropDownType.Kubun)
            'helper.SetDropDownList(cboKubun_2, DropDownHelper.DropDownType.Kubun)
            'helper.SetDropDownList(cboKubun_3, DropDownHelper.DropDownType.Kubun)

            '****************************************************************************
            ' 画面項目の動きをセッティング（初期値、イベントハンドラ等）
            '****************************************************************************
            'setDispAction()

            'フォーカス設定
            'Me.cmbHosyousho_hak_jyky.Focus()

        Else


        End If

    End Sub

End Class
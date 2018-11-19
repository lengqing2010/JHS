Imports Itis.Earth.BizLogic
Imports Itis.Earth.DataAccess

Partial Public Class torihikiJyouhou
    Inherits System.Web.UI.UserControl

    Private TorihikiBL As New TorihikiJyouhouLogic()
    Private CommonMsgAndFocusBL As New kihonjyouhou.MessageAndFocus()
    Private CommonCheckFuc As New CommonCheck()
    Private CommonLG As New CommonLogic()

    Private _kameiten_cd As String
    ''' <summary>
    ''' 加盟店コード
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property kameiten_cd() As String
        Get
            Return _kameiten_cd
        End Get
        Set(ByVal value As String)
            _kameiten_cd = value
        End Set
    End Property

    Private _upd_login_user_id As String
    ''' <summary>
    ''' 更新者ID
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Upd_login_user_id() As String
        Get
            Return _upd_login_user_id
        End Get

        Set(ByVal value As String)
            _upd_login_user_id = value
        End Set

    End Property

    Private _kenngenn As Boolean
    ''' <summary>
    ''' 取引権限
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Kenngenn() As Boolean
        Get
            Return _kenngenn
        End Get

        Set(ByVal value As Boolean)
            _kenngenn = value
        End Set

    End Property

    Private _kenngennGM As Boolean
    ''' <summary>
    ''' 業務取引権限
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property KenngennGM() As Boolean
        Get
            Return _kenngennGM
        End Get

        Set(ByVal value As Boolean)
            _kenngennGM = value
        End Set

    End Property

    Private _kenngennKR As Boolean
    ''' <summary>
    ''' 経理取引権限
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property KenngennKR() As Boolean
        Get
            Return _kenngennKR
        End Get

        Set(ByVal value As Boolean)
            _kenngennKR = value
        End Set

    End Property



    '編集項目非活性、活性設定対応　20180905　李松涛　対応　↓
    'salesforce項目_編集非活性フラグ 取得
    Private Function Iskassei(ByVal KameitenCd As String, ByVal kbn As String) As Boolean

        If kbn.Trim <> "" Then
            If ViewState("Iskassei") Is Nothing Then
                If kbn = "" Then
                    ViewState("Iskassei") = ""
                Else
                    ViewState("Iskassei") = (New Salesforce).GetSalesforceHikasseiFlgByKbn(kbn)
                End If

            End If
        Else

            If ViewState("Iskassei") Is Nothing Then
                ViewState("Iskassei") = (New Salesforce).GetSalesforceHikasseiFlg(KameitenCd)
            End If

        End If
        Return ViewState("Iskassei").ToString <> "1"
    End Function

    '編集項目非活性、活性設定する
    Public Sub SetKassei()

        ViewState("Iskassei") = Nothing
        Dim kbn As String = ""
        Dim itKassei As Boolean = Iskassei(_kameiten_cd, "")

        '保証期間
        tbxHosyouKigen.ReadOnly = Not itKassei
        tbxHosyouKigen.CssClass = IIf(itKassei, "", "readOnly")

        '保証書発行<br/>タイミング
        ddlHosyousyoHakkou.Enabled = itKassei
        ddlHosyousyoHakkou.CssClass = IIf(itKassei, "", "readOnly")

        '自動発行_先方確認者
        tbx_hosyousyo_hak_kakuninsya.ReadOnly = Not itKassei
        tbx_hosyousyo_hak_kakuninsya.CssClass = IIf(itKassei, "", "readOnly")

        '自動発行_確認日
        tbx_hosyousyo_hak_kakunin_date.ReadOnly = Not itKassei
        tbx_hosyousyo_hak_kakunin_date.CssClass = IIf(itKassei, "", "readOnly")

        '保証書引渡日印字有無
        ddl_hikiwatasi_inji_umu.Enabled = itKassei
        ddl_hikiwatasi_inji_umu.CssClass = IIf(itKassei, "", "readOnly")


        '保証期間_先方確認者
        tbx_hosyou_kikan_kakuninsya.ReadOnly = Not itKassei
        tbx_hosyou_kikan_kakuninsya.CssClass = IIf(itKassei, "", "readOnly")

        '保証期<br />間適用開始日
        tbx_hosyou_kikan_start_date.ReadOnly = Not itKassei
        tbx_hosyou_kikan_start_date.CssClass = IIf(itKassei, "", "readOnly")

        '保証書発送有無
        ddl_hosyousyo_hassou_umu.Enabled = itKassei
        ddl_hosyousyo_hassou_umu.CssClass = IIf(itKassei, "", "readOnly")
        'If Not itKassei Then
        '    CommonKassei.SetDropdownListReadonly(ddl_hosyousyo_hassou_umu)
        'End If

        '保証書発行有無
        '適用開始日
        tbx_hosyousyo_hassou_umu_start_date.ReadOnly = Not itKassei
        tbx_hosyousyo_hassou_umu_start_date.CssClass = IIf(itKassei, "", "readOnly")


        'サポート調査<br />保証付保FAX先方確認者
        tbx_fuho_fax_kakuninsya.ReadOnly = Not itKassei
        tbx_fuho_fax_kakuninsya.CssClass = IIf(itKassei, "", "readOnly")

        ' サポート調査<br />保証付保FAX確認日
        tbx_fuho_fax_kakunin_date.ReadOnly = Not itKassei
        tbx_fuho_fax_kakunin_date.CssClass = IIf(itKassei, "", "readOnly")

        'サポート調査<br />保証付保FAX送付有無
        ddl_fuho_fax_umu.Enabled = itKassei
        ddl_fuho_fax_umu.CssClass = IIf(itKassei, "", "readOnly")




        '報告書発行部数
        tbxTysHks.ReadOnly = Not itKassei
        tbxTysHks.CssClass = IIf(itKassei, "", "readOnly")
        tbxKjHks.ReadOnly = Not itKassei
        tbxKjHks.CssClass = IIf(itKassei, "", "readOnly")





        ddlHattyusyoFlg.Enabled = itKassei
        ddlHattyusyoFlg.CssClass = IIf(itKassei, "", "readOnly")
        ddl_shitei_seikyuusyo_umu.Enabled = itKassei
        ddl_shitei_seikyuusyo_umu.CssClass = IIf(itKassei, "", "readOnly")








    End Sub


    ''' <summary>
    ''' 画面ロード
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            ListInit6869(Me.ddlEkijoukaTokuyakuKannri, "69", True, True)

            '権限チェック
            Call kengenCheck()
            'タイトル　リンク設定
            Call titleTextRefSet()
            '親画面から値を取得
            Call GetValueByMainPage()
            '画面リスト実装
            Call GamenListSettei()
            '画面表示項目設定
            Call gameihHyouji()
            '画面表示項目のプロプテ設定
            Call PageItemAddAttributes()

        End If

        SetKassei()

    End Sub

    ''' <summary>
    ''' 機能ヘーダーテキストリンク設定
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub titleTextRefSet()

        Me.titleText_torihiki.HRef = "javascript:changeDisplay('" & Me.meisaiTbody_torihiki.ClientID & "');changeDisplay('" & Me.titleInfobarTorihiki.ClientID & "');"
        Me.titleText_gyoumu.HRef = "javascript:changeDisplay('" & Me.meisaiTbody_gyoumu.ClientID & "');changeDisplay('" & Me.titleInfobarTHgyoumu.ClientID & "');"
        Me.titleText_keiri.HRef = "javascript:changeDisplay('" & Me.meisaiTbody_keiri.ClientID & "');changeDisplay('" & Me.titleInfobarTHkeiri.ClientID & "');"

    End Sub

    ''' <summary>
    ''' メインから取得した値を画面以内に引き渡す
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub GetValueByMainPage()

        ViewState.Item("kameiten_cd") = _kameiten_cd
        ViewState.Item("user_id") = _upd_login_user_id

    End Sub

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub kengenCheck()

        Me.btnTouroku.Enabled = _kenngenn
        Me.btnTouroku_gyoumu.Enabled = _kenngennGM
        Me.btnTouroku_keiri.Enabled = _kenngennKR

    End Sub

    ''' <summary>
    ''' 発注書有無：整合性設定
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub HtsChange(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlHattyusyo.SelectedIndexChanged

        If Me.ddlHattyusyo.SelectedValue = 0 Then

            Me.ddlHtsTys.SelectedValue = 0
            Me.ddlHtsKs.SelectedValue = 0
            Me.ddlHtsKj.SelectedValue = 0

            Me.ddlHtsTys.Enabled = False
            Me.ddlHtsKs.Enabled = False
            Me.ddlHtsKj.Enabled = False

        ElseIf Me.ddlHattyusyo.SelectedValue = 1 Then

            Me.ddlHtsTys.Enabled = True
            Me.ddlHtsKs.Enabled = True
            Me.ddlHtsKj.Enabled = True

        End If

    End Sub

    ''' <summary>
    ''' 画面項目の事件設定
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub PageItemAddAttributes()

        Me.tbxNyukinKakuninKakusyo.Attributes.Add("onblur", "checkDate(this);")
        Me.tbxProKakaninbi.Attributes.Add("onblur", "checkDate(this);")


        Me.tbxSinToyoKaisiBi.Attributes.Add("onfocus", "return setOnfocusNengetu(this)")
        Me.tbxSinToyoKaisiBi.Attributes.Add("onblur", "LostFocusPostBack(this,1)")

        Me.tbx_hosyousyo_hak_kakunin_date.Attributes.Add("onfocus", "return setOnfocusNengetu(this)")
        Me.tbx_hosyousyo_hak_kakunin_date.Attributes.Add("onblur", "LostFocusPostBack(this,1)")

        Me.tbx_hosyou_kikan_start_date.Attributes.Add("onfocus", "return setOnfocusNengetu(this)")
        Me.tbx_hosyou_kikan_start_date.Attributes.Add("onblur", "LostFocusPostBack(this,1)")

        '保証書発送有無_適用開始日
        Me.tbx_hosyousyo_hassou_umu_start_date.Attributes.Add("onfocus", "return setOnfocusNengetu(this)")
        Me.tbx_hosyousyo_hassou_umu_start_date.Attributes.Add("onblur", "LostFocusPostBack(this,1)")

        Me.tbx_fuho_fax_kakunin_date.Attributes.Add("onfocus", "return setOnfocusNengetu(this)")
        Me.tbx_fuho_fax_kakunin_date.Attributes.Add("onblur", "LostFocusPostBack(this,1)")

    End Sub

    ''' <summary>
    ''' 画面表示項目設定
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <remarks></remarks>
    Protected Sub gameihHyouji(Optional ByVal sender As String = "Page_Load")

        Dim dtKameiten As New DataTable
        Dim dtTorihiki As New DataTable

        dtKameiten = TorihikiBL.GetKameitenData(ViewState.Item("kameiten_cd"))
        dtTorihiki = TorihikiBL.GetTorihikiData(ViewState.Item("kameiten_cd"))

        '取引情報
        Me.tbxHosyouKigen.Text = CommonLG.getDisplayString(dtKameiten.Rows(0).Item("hosyou_kikan"))
        Me.ddlHosyousyoHakkou.SelectedValue = ddlItemSet_umu(CommonLG.getDisplayString(dtKameiten.Rows(0).Item("hosyousyo_hak_umu")))

        If CommonLG.getDisplayString(dtKameiten.Rows(0).Item("nyuukin_kakunin_jyouken")) <> "" Then
            Me.ddlNyukinKakunin.SelectedValue = CommonLG.getDisplayString(dtKameiten.Rows(0).Item("nyuukin_kakunin_jyouken"))
        End If

        If CommonLG.getDisplayString(dtKameiten.Rows(0).Item("nyuukin_kakunin_oboegaki")) <> "" Then
            Me.tbxNyukinKakuninKakusyo.Text = CommonLG.getDisplayString(dtKameiten.Rows(0).Item("nyuukin_kakunin_oboegaki")).Substring(0, 10)
        End If

        Me.ddlKoujiKaisyaSeikyu.SelectedValue = ddlItemSet_umu(CommonLG.getDisplayString(dtKameiten.Rows(0).Item("koj_gaisya_seikyuu_umu")))
        Me.ddlKoujiTantouFlg.SelectedValue = ddlItemSet_umu(CommonLG.getDisplayString(dtKameiten.Rows(0).Item("koj_tantou_flg")))

        If CommonLG.getDisplayString(dtKameiten.Rows(0).Item("tys_mitsyo_flg")) <> "" Then
            Me.ddlTysMitumorisyoFlg.SelectedValue = CommonLG.getDisplayString(dtKameiten.Rows(0).Item("tys_mitsyo_flg"))
        End If

        If CommonLG.getDisplayString(dtKameiten.Rows(0).Item("hattyuusyo_flg")) <> "" Then
            Me.ddlHattyusyoFlg.SelectedValue = CommonLG.getDisplayString(dtKameiten.Rows(0).Item("hattyuusyo_flg"))
        End If

        Me.tbxMitumorisyoFileNm.Text = CommonLG.getDisplayString(dtKameiten.Rows(0).Item("mitsyo_file_mei"))

        '==================2017/01/01 李松涛 追加 液状化特約管理 新特約切替日↓==========================
        Me.ddlEkijoukaTokuyakuKannri.SelectedValue = NullToEmpty(dtKameiten.Rows(0).Item("ekijyouka_tokuyaku_kanri"))
        If dtKameiten.Rows(0).Item("shintokuyaku_kirikaedate") Is DBNull.Value Then
            Me.tbxSinToyoKaisiBi.Text = ""
        Else
            Me.tbxSinToyoKaisiBi.Text = CDate(dtKameiten.Rows(0).Item("shintokuyaku_kirikaedate").ToString).ToString("yyyy/MM/dd")
        End If
        '==================2017/01/01 李松涛 追加 液状化特約管理 新特約切替日↑==========================


        Me.tbx_hosyousyo_hak_kakuninsya.Text = CommonLG.getDisplayString(dtKameiten.Rows(0).Item("hosyousyo_hak_kakuninsya"))
        If dtKameiten.Rows(0).Item("hosyousyo_hak_kakunin_date") Is DBNull.Value Then
            Me.tbx_hosyousyo_hak_kakunin_date.Text = ""
        Else
            Me.tbx_hosyousyo_hak_kakunin_date.Text = CDate(dtKameiten.Rows(0).Item("hosyousyo_hak_kakunin_date").ToString).ToString("yyyy/MM/dd")
        End If
        Me.ddl_hikiwatasi_inji_umu.SelectedValue = NullToEmpty(dtKameiten.Rows(0).Item("hikiwatasi_inji_umu"))



        Me.tbx_hosyou_kikan_kakuninsya.Text = CommonLG.getDisplayString(dtKameiten.Rows(0).Item("hosyou_kikan_kakuninsya"))

        If dtKameiten.Rows(0).Item("hosyou_kikan_start_date") Is DBNull.Value Then
            Me.tbx_hosyou_kikan_start_date.Text = ""
        Else
            Me.tbx_hosyou_kikan_start_date.Text = CDate(dtKameiten.Rows(0).Item("hosyou_kikan_start_date").ToString).ToString("yyyy/MM/dd")
        End If

        Me.ddl_hosyousyo_hassou_umu.SelectedValue = NullToEmpty(dtKameiten.Rows(0).Item("hosyousyo_hassou_umu"))
        '保証書発送有無_適用開始日
        If dtKameiten.Rows(0).Item("hosyousyo_hassou_umu_start_date") Is DBNull.Value Then
            Me.tbx_hosyousyo_hassou_umu_start_date.Text = ""
        Else
            Me.tbx_hosyousyo_hassou_umu_start_date.Text = CDate(dtKameiten.Rows(0).Item("hosyousyo_hassou_umu_start_date").ToString).ToString("yyyy/MM/dd")
        End If

        Me.tbx_fuho_fax_kakuninsya.Text = CommonLG.getDisplayString(dtKameiten.Rows(0).Item("fuho_fax_kakuninsya"))
        If dtKameiten.Rows(0).Item("fuho_fax_kakunin_date") Is DBNull.Value Then
            Me.tbx_fuho_fax_kakunin_date.Text = ""
        Else
            Me.tbx_fuho_fax_kakunin_date.Text = CDate(dtKameiten.Rows(0).Item("fuho_fax_kakunin_date").ToString).ToString("yyyy/MM/dd")
        End If
        Me.ddl_fuho_fax_umu.SelectedValue = NullToEmpty(dtKameiten.Rows(0).Item("fuho_fax_umu"))





        'WEB申込採番判別FLG
        Dim strWebMoushikomiSaibanHanbetuFlg As String = CommonLG.getDisplayString(dtKameiten.Rows(0).Item("web_moushikomi_saiban_hanbetu_flg"))
        If strWebMoushikomiSaibanHanbetuFlg = "1" Then
            Me.ddlWebMoushikomiSaibanHanbetuFlg.SelectedIndex = 1
        Else
            Me.ddlWebMoushikomiSaibanHanbetuFlg.SelectedIndex = 0
        End If
        '発注書未着連携対象外FLG
        Dim strHattyuusyoMichakuRenkeiTaisyougaiFlg As String = CommonLG.getDisplayString(dtKameiten.Rows(0).Item("hattyuusyo_michaku_renkei_taisyougai_flg"))
        If strHattyuusyoMichakuRenkeiTaisyougaiFlg = "1" Then
            Me.ddlHattyuusyoMichakuRenkeiTaisyougaiFlg.SelectedIndex = 1
        Else
            Me.ddlHattyuusyoMichakuRenkeiTaisyougaiFlg.SelectedIndex = 0
        End If

        'Me.ddl_shitei_seikyuusyo_umu.SelectedValue = CommonLG.getDisplayString(dtKameiten.Rows(0).Item("shitei_seikyuusyo_umu"))
        'Me.ddl_shiroari_kensa_hyouji.SelectedValue = CommonLG.getDisplayString(dtKameiten.Rows(0).Item("shiroari_kensa_hyouji"))

        If dtKameiten.Rows(0).Item("shitei_seikyuusyo_umu") Is DBNull.Value OrElse dtKameiten.Rows(0).Item("shitei_seikyuusyo_umu") = "" Then
        Else
            Me.ddl_shitei_seikyuusyo_umu.SelectedValue = dtKameiten.Rows(0).Item("shitei_seikyuusyo_umu")
        End If

        If dtKameiten.Rows(0).Item("shiroari_kensa_hyouji") Is DBNull.Value OrElse dtKameiten.Rows(0).Item("shiroari_kensa_hyouji") = "" Then
        Else
            Me.ddl_shiroari_kensa_hyouji.SelectedValue = dtKameiten.Rows(0).Item("shiroari_kensa_hyouji")
        End If


        If dtTorihiki.Rows.Count > 0 Then

            If dtTorihiki.Rows(0).Item("upd_datetime").ToString <> "" Then
                ViewState.Item("gameiDate") = dtTorihiki.Rows(0).Item("upd_datetime")
            End If

            '取引情報＿業務
            Me.ddlTysMitumori.SelectedValue = ddlItemSet_umu(CommonLG.getDisplayString(dtTorihiki.Rows(0).Item("tys_mitsyo_flg")))
            Me.ddlKisoDanmenzu.SelectedValue = ddlItemSet_umu(CommonLG.getDisplayString(dtTorihiki.Rows(0).Item("ks_danmenzu_flg")))
            Me.ddlTatouwaribikiKbn.SelectedValue = CommonLG.getDisplayString(dtTorihiki.Rows(0).Item("tatou_waribiki_flg"))
            Me.tbxTatouwaibikiBikou.Text = CommonLG.getDisplayString(dtTorihiki.Rows(0).Item("tatou_waribiki_bikou"))
            Me.ddlTokkaSinsei.SelectedValue = CommonLG.getDisplayString(dtTorihiki.Rows(0).Item("tokka_sinsei_flg"))
            Me.ddlZandoSyobunhi.SelectedValue = ddlItemSet_umu(CommonLG.getDisplayString(dtTorihiki.Rows(0).Item("zando_syobunhi_umu")))
            Me.ddlKyusuisyadai.SelectedValue = ddlItemSet_umu(CommonLG.getDisplayString(dtTorihiki.Rows(0).Item("kyuusuisyadai_umu")))
            Me.ddlJinawahari.SelectedValue = ddlItemSet_umu(CommonLG.getDisplayString(dtTorihiki.Rows(0).Item("jinawa_taiou_umu")))
            Me.ddlKuisindasi.SelectedValue = ddlItemSet_umu(CommonLG.getDisplayString(dtTorihiki.Rows(0).Item("kousin_taiou_umu")))
            Me.tbxHeikinhisuu.Text = CommonLG.getDisplayString(dtTorihiki.Rows(0).Item("tys_kojkan_heikin_nissuu"))
            Me.tbxHyoujunKiso.Text = CommonLG.getDisplayString(dtTorihiki.Rows(0).Item("hyoujun_ks"))
            Me.ddlJHSigaiKouji.SelectedValue = ddlItemSet_umu(CommonLG.getDisplayString(dtTorihiki.Rows(0).Item("js_igai_koj_flg")))

            Me.ddlTysDoufu.SelectedValue = ddlItemSet_umu(CommonLG.getDisplayString(dtTorihiki.Rows(0).Item("tys_hkks_douhuu_umu")))
            Me.ddlKjDoufu.SelectedValue = ddlItemSet_umu(CommonLG.getDisplayString(dtTorihiki.Rows(0).Item("koj_hkks_douhuu_umu")))
            Me.ddlKsDoufu.SelectedValue = ddlItemSet_umu(CommonLG.getDisplayString(dtTorihiki.Rows(0).Item("kensa_hkks_douhuu_umu")))

            Me.tbxKjHks.Text = CommonLG.getDisplayString(dtTorihiki.Rows(0).Item("koj_hkks_busuu"))
            Me.tbxKsHks.Text = CommonLG.getDisplayString(dtTorihiki.Rows(0).Item("kensa_hkks_busuu"))
            Me.tbxTysHks.Text = CommonLG.getDisplayString(dtTorihiki.Rows(0).Item("tys_hkks_busuu"))

            Me.tbxNkHssHakou.Text = CommonLG.getDisplayString(dtTorihiki.Rows(0).Item("nyuukin_mae_hosyousyo_hak"))
            Me.ddlHikiwataFile.SelectedValue = ddlItemSet_umu(CommonLG.getDisplayString(dtTorihiki.Rows(0).Item("hikiwatasi_file_umu")))

            '取引情報＿経理
            Me.tbxKaisyuSimeibi.Text = CommonLG.getDisplayString(dtTorihiki.Rows(0).Item("sime_date"))
            Me.tbxSeikyuhityakubi.Text = CommonLG.getDisplayString(dtTorihiki.Rows(0).Item("seikyuusyo_hittyk_date"))
            Me.tbxSiharayibi.Text = CommonLG.getDisplayString(dtTorihiki.Rows(0).Item("siharai_yotei_tuki"))
            Me.tbxGetugou.Text = CommonLG.getDisplayString(dtTorihiki.Rows(0).Item("siharai_yotei_date"))
            Me.tbxGenkin.Text = CommonLG.getDisplayString(dtTorihiki.Rows(0).Item("siharai_genkin_wariai"))
            Me.ddlSiharaiHouhou.SelectedValue = CommonLG.getDisplayString(dtTorihiki.Rows(0).Item("siharai_houhou_flg"))
            Me.tbxTegata.Text = CommonLG.getDisplayString(dtTorihiki.Rows(0).Item("siharai_tegata_wariai"))
            Me.tbxSiharaiSaito.Text = CommonLG.getDisplayString(dtTorihiki.Rows(0).Item("siharai_site"))
            Me.tbxTyoufuTegata.Text = CommonLG.getDisplayString(dtTorihiki.Rows(0).Item("tegata_hiritu"))
            Me.ddlHtsTys.SelectedValue = ddlItemSet_umu(CommonLG.getDisplayString(dtTorihiki.Rows(0).Item("tys_hattyuusyo_umu")))
            Me.ddlHtsKj.SelectedValue = ddlItemSet_umu(CommonLG.getDisplayString(dtTorihiki.Rows(0).Item("koj_hattyuusyo_umu")))
            Me.ddlHtsKs.SelectedValue = ddlItemSet_umu(CommonLG.getDisplayString(dtTorihiki.Rows(0).Item("kensa_hattyuusyo_umu")))

            Me.ddlSenpoSiteiSks.SelectedValue = ddlItemSet_umu(CommonLG.getDisplayString(dtTorihiki.Rows(0).Item("senpou_sitei_seikyuusyo")))
            Me.tbxProKakaninbi.Text = CommonLG.getDisplayString(dtTorihiki.Rows(0).Item("flow_kakunin_date"))
            Me.ddlKyouryokuKaihi.SelectedValue = ddlItemSet_umu(CommonLG.getDisplayString(dtTorihiki.Rows(0).Item("kyouryoku_kaihi_umu")))

            Me.tbxTyoufuKyouryoukuKh.Text = CommonLG.getDisplayString(dtTorihiki.Rows(0).Item("kyouryoku_kaihi_hiritu"))


        End If

        If ddlHtsTys.SelectedValue = 0 And ddlHtsKj.SelectedValue = 0 And ddlHtsKs.SelectedValue = 0 Then

            Me.ddlHattyusyo.SelectedValue = 0

            ddlHtsTys.Enabled = False
            ddlHtsKj.Enabled = False
            ddlHtsKs.Enabled = False

        Else
            Me.ddlHattyusyo.SelectedValue = 1
        End If

    End Sub

    ''' <summary>
    ''' リスト項目からデータ取得
    ''' </summary>
    ''' <param name="dataInTable"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function ddlItemSet_umu(ByVal dataInTable As String) As String

        If dataInTable <> "" Then
            Return dataInTable
        Else
            Return "0"
        End If

    End Function

    ''' <summary>
    ''' 画面トッロプダウンリスト実装
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub GamenListSettei()

        ListInit(Me.ddlNyukinKakunin, MeisyouDataAccess.MeisyouType.NYUUKIN_KAKUNIN, True, True)
        ListInit(Me.ddlTysMitumorisyoFlg, MeisyouDataAccess.MeisyouType.TYS_MITUMORI_FLG, True, True)
        ListInit(Me.ddlHattyusyoFlg, MeisyouDataAccess.MeisyouType.HATTYUUSYO_FLG, True, True)
        ListInit(Me.ddlTegataHiritu, MeisyouDataAccess.MeisyouType.TEGATA_HIRITU, False, True)
        ListInit(Me.ddlKyouryokuKaihiHiritu, MeisyouDataAccess.MeisyouType.KYOURYOKU_KAIHI, False, True)

        '20180129李追加
        ListInit6869(Me.ddl_shitei_seikyuusyo_umu, MeisyouDataAccess.MeisyouType.SHITEI_SEIKYUUSYO_UMU, True, True)
        Me.ddl_shitei_seikyuusyo_umu.Items(0).Text = ""
        Me.ddl_shitei_seikyuusyo_umu.Items(0).Value = ""

    End Sub

    ''' <summary>
    ''' トッロプダウンリストデータ設定
    ''' </summary>
    ''' <param name="ddl">トッロプダウンリストコントロール</param>
    ''' <param name="type">リストタイプ</param>
    ''' <param name="withSpc">空白行</param>
    ''' <param name="withCd">コード</param>
    ''' <remarks></remarks>
    Protected Sub ListInit(ByRef ddl As DropDownList, ByVal type As MeisyouDataAccess.MeisyouType, ByVal withSpc As Boolean, Optional ByVal withCd As Boolean = True)

        Dim dtTemp As New DataTable

        ' DataTableへのカラム設定
        dtTemp.Columns.Add(New DataColumn("CmbTextField", GetType(String)))
        dtTemp.Columns.Add(New DataColumn("CmbValueField", GetType(String)))

        TorihikiBL.GetListData(dtTemp, type.GetHashCode.ToString().PadLeft(2, "0"), withSpc, withCd)

        ddl.DataSource = dtTemp

        ddl.DataTextField = "CmbTextField"
        ddl.DataValueField = "CmbValueField"

        ddl.DataBind()

    End Sub

    Protected Sub ListInit6869(ByRef ddl As DropDownList, ByVal type As MeisyouDataAccess.MeisyouType, ByVal withSpc As Boolean, Optional ByVal withCd As Boolean = True)

        Dim dtTemp As New DataTable

        ' DataTableへのカラム設定
        dtTemp.Columns.Add(New DataColumn("CmbTextField", GetType(String)))
        dtTemp.Columns.Add(New DataColumn("CmbValueField", GetType(String)))

        TorihikiBL.GetListData6869(dtTemp, type.GetHashCode.ToString().PadLeft(2, "0"), withSpc, withCd)

        ddl.DataSource = dtTemp

        ddl.DataTextField = "CmbTextField"
        ddl.DataValueField = "CmbValueField"

        ddl.DataBind()

    End Sub

    ''' <summary>
    ''' 登録ボタンをクリック時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnTouroku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTouroku.Click
        If PageItemInputCheck("torihiki") Then

            Dim kameitenCd As String
            Dim updKekka As String
            Dim csScript As New StringBuilder

            kameitenCd = ViewState.Item("kameiten_cd")

            Dim otherPageFunction As New Itis.Earth.BizLogic.kihonjyouhou.OtherPageFunction
            If Not otherPageFunction.DoFunction(Parent.Page, "Haitakameiten") Then
                Exit Sub
            End If

            updKekka = TorihikiBL.UpdKameiten(kameitenCd, makeAkameitenTable)

            '最新更新時間---更新
            If InStr(updKekka, Messages.Instance.MSG018S.ToString.Substring(7)) Then
                otherPageFunction.DoFunction(Parent.Page, "SetKyoutuuKousin")
            End If

            ScriptManager.RegisterStartupScript(Me.Parent.Page, Me.Parent.Page.GetType(), "err", csScript.Append("alert('" & updKekka & "');").ToString, True)

        End If
        
    End Sub

    ''' <summary>
    ''' 業務部分：登録ボタンをクリック時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnTouroku_gyoumu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTouroku_gyoumu.Click
        Call TourokuSyori(CType(sender, Button).ID.ToString)
    End Sub

    ''' <summary>
    ''' 経理部分：登録ボタンをクリック時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnTouroku_keiri_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTouroku_keiri.Click
        Call TourokuSyori(CType(sender, Button).ID.ToString)
    End Sub


    ''' <summary>
    ''' 業務と経理の登録処理
    ''' </summary>
    ''' <param name="sender">ボタン区分</param>
    ''' <remarks></remarks>
    Protected Sub TourokuSyori(ByVal sender As String)
        Dim strBFlg As String = ""
        Call getBFlg(sender, strBFlg)
        Call torihikiTouroku(strBFlg)
    End Sub

    ''' <summary>
    ''' ボタン判断(業務と経理)
    ''' </summary>
    ''' <param name="sender">ボタン区分</param>
    ''' <param name="btnId">部分区分</param>
    ''' <remarks></remarks>
    Protected Sub getBFlg(ByVal sender As String, ByRef btnId As String)

        btnId = sender.Substring(sender.IndexOf("_") + 1, sender.Length - sender.IndexOf("_") - 1)

    End Sub

    ''' <summary>
    ''' 取引(業務と経理)情報の登録
    ''' </summary>
    ''' <param name="bubunFlg">部分区分</param>
    ''' <remarks></remarks>
    Protected Sub torihikiTouroku(ByVal bubunFlg As String)

        If PageItemInputCheck(bubunFlg) Then

            Dim kameitenCd As String
            Dim gameiDate As DateTime
            Dim tourokuKekka As String
            Dim csScript As New StringBuilder

            kameitenCd = ViewState.Item("kameiten_cd")

            gameiDate = ViewState.Item("gameiDate")

            tourokuKekka = TorihikiBL.TorihikiTouroku(kameitenCd, gameiDate, makeATorihikiTable(bubunFlg), bubunFlg)

            ViewState.Item("gameiDate") = gameiDate

            ScriptManager.RegisterStartupScript(Me.Parent.Page, Me.Parent.Page.GetType(), "err", csScript.Append("alert('" & tourokuKekka & "');").ToString, True)

        End If

    End Sub

    ''' <summary>
    ''' 画面項目入力チェック
    ''' </summary>
    ''' <param name="btnKbn">ボタン区分</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function PageItemInputCheck(ByVal btnKbn As String) As Boolean

        Dim csScript As New StringBuilder
        Dim MsgTotal As String
        Dim CltID As String

        If btnKbn = "torihiki" Then

            TbxItemInputCheck(Me.tbxHosyouKigen, "保証期間", "半角数字", , 1)
            TbxItemInputCheck(Me.tbxHosyouKigen, "保証期間", "桁数", 2)

            TbxItemInputCheck(Me.tbxNyukinKakuninKakusyo, "入金確認覚書", "日付")

            TbxItemInputCheck(Me.tbxMitumorisyoFileNm, "見積書ﾌｧｲﾙ名", "桁数", 24, , kbn.ZENKAKU)
            TbxItemInputCheck(Me.tbxMitumorisyoFileNm, "見積書ﾌｧｲﾙ名", "禁止文字")

            TbxItemInputCheck(Me.tbxSinToyoKaisiBi, "新特約切替日", "日付")

            TbxItemInputCheck(Me.tbx_hosyousyo_hak_kakunin_date, "保証書自動発行_確認日", "日付")
            TbxItemInputCheck(Me.tbx_hosyou_kikan_start_date, "保証期間適用開始日", "日付")

            TbxItemInputCheck(Me.tbx_hosyousyo_hassou_umu_start_date, "保証書発送有無適用開始日", "日付")

            TbxItemInputCheck(Me.tbx_fuho_fax_kakunin_date, "サポート調査保証付保FAX確認日", "日付")


        ElseIf btnKbn = "gyoumu" Then

            TbxItemInputCheck(Me.tbxTatouwaibikiBikou, "多棟割引備考", "桁数", 40, , kbn.ZENKAKU)
            TbxItemInputCheck(Me.tbxTatouwaibikiBikou, "多棟割引備考", "禁止文字")

            TbxItemInputCheck(Me.tbxHeikinhisuu, "工事間平均日数", "半角数字", , 1)
            TbxItemInputCheck(Me.tbxHeikinhisuu, "工事間平均日数", "桁数", 3)

            TbxItemInputCheck(Me.tbxHyoujunKiso, "標準基礎", "桁数", 40, , kbn.ZENKAKU)
            TbxItemInputCheck(Me.tbxHyoujunKiso, "標準基礎", "禁止文字")

            TbxItemInputCheck(Me.tbxTysHks, "調査報告書部数", "半角数字", , 1)
            TbxItemInputCheck(Me.tbxTysHks, "調査報告書部数", "桁数", 2)
            TbxItemInputCheck(Me.tbxKjHks, "工事報告書部数", "半角数字", , 1)
            TbxItemInputCheck(Me.tbxKjHks, "工事報告書部数", "桁数", 2)
            TbxItemInputCheck(Me.tbxKsHks, "検査報告書部数", "半角数字", , 1)
            TbxItemInputCheck(Me.tbxKsHks, "検査報告書部数", "桁数", 2)


            TbxItemInputCheck(Me.tbxNkHssHakou, "入金前保証書発行", "桁数", 40, , kbn.ZENKAKU)
            TbxItemInputCheck(Me.tbxNkHssHakou, "入金前保証書発行", "禁止文字")

        ElseIf btnKbn = "keiri" Then

            TbxItemInputCheck(Me.tbxKaisyuSimeibi, "回収締め日", "半角数字", , 1)
            TbxItemInputCheck(Me.tbxKaisyuSimeibi, "回収締め日", "桁数", 2)

            TbxItemInputCheck(Me.tbxSeikyuhityakubi, "請求書必着日", "半角数字", , 1)
            TbxItemInputCheck(Me.tbxSeikyuhityakubi, "請求書必着日", "桁数", 2)

            TbxItemInputCheck(Me.tbxSiharayibi, "支払予定月", "半角数字", , 1)
            TbxItemInputCheck(Me.tbxSiharayibi, "支払予定月", "桁数", 2)

            TbxItemInputCheck(Me.tbxGetugou, "支払予定日", "半角数字", , 1)
            TbxItemInputCheck(Me.tbxGetugou, "支払予定日", "桁数", 2)

            TbxItemInputCheck(Me.tbxGenkin, "支払現金割合", "半角数字", , 1)
            TbxItemInputCheck(Me.tbxGenkin, "支払現金割合", "桁数", 3)

            TbxItemInputCheck(Me.tbxTegata, "支払手形割合", "半角数字", , 1)
            TbxItemInputCheck(Me.tbxTegata, "支払手形割合", "桁数", 3)

            TbxItemInputCheck(Me.tbxSiharaiSaito, "支払ｻｲﾄ", "半角数字", , 1)
            TbxItemInputCheck(Me.tbxSiharaiSaito, "支払ｻｲﾄ", "桁数", 2)

            TbxItemInputCheck(Me.tbxTyoufuTegata, "手形比率内容", "桁数", 128, , kbn.ZENKAKU)
            TbxItemInputCheck(Me.tbxTyoufuTegata, "手形比率内容", "禁止文字")

            TbxItemInputCheck(Me.tbxProKakaninbi, "ﾌﾛｰ確認日", "日付")

            TbxItemInputCheck(Me.tbxTyoufuKyouryoukuKh, "協力会費比率内容", "桁数", 128, , kbn.ZENKAKU)
            TbxItemInputCheck(Me.tbxTyoufuKyouryoukuKh, "協力会費比率内容", "禁止文字")

            TbxItemInputCheck(Me.ddlHattyusyo, "発注書有無", "整合性")

        End If

        MsgTotal = CommonMsgAndFocusBL.Message

        If MsgTotal <> "" Then

            CltID = CommonMsgAndFocusBL.focusCtrl.ClientID

            If CommonMsgAndFocusBL.focusCtrl.GetType.ToString = GetType(DropDownList).ToString Then
                ScriptManager.RegisterStartupScript(Me.Parent.Page, Me.Parent.Page.GetType(), "err", csScript.Append("alert('" & MsgTotal & "');objEBI('" & CltID & "').focus();").ToString, True)
            ElseIf CommonMsgAndFocusBL.focusCtrl.GetType.ToString = GetType(TextBox).ToString Then
                ScriptManager.RegisterStartupScript(Me.Parent.Page, Me.Parent.Page.GetType(), "err", csScript.Append("alert('" & MsgTotal & "');objEBI('" & CltID & "').select();").ToString, True)
            End If

            Return False
        Else
            Return True
        End If

    End Function

    ''' <summary>
    ''' 画面項目入力チェック
    ''' </summary>
    ''' <param name="control">項目コントロール</param>
    ''' <param name="MsgParam">項目名</param>
    ''' <param name="kbn">チェック区分</param>
    ''' <param name="len">長さ</param>
    ''' <param name="Flg">整数フラグ</param>
    ''' <param name="kakaFlg">半角フラグ</param>
    ''' <remarks></remarks>
    Protected Sub TbxItemInputCheck(ByVal control As System.Web.UI.Control, ByVal MsgParam As String, ByVal kbn As String, Optional ByVal len As Int64 = 0, Optional ByVal Flg As Int16 = 0, Optional ByVal kakaFlg As WebUI.kbn = kbn.HANKAKU)

        Dim checkKekka As String = ""

        If kbn = "整合性" Then
            checkKekka = SeigouseiCheck()
        Else
            If CType(control, TextBox).Text.ToString.Trim <> "" Then

                Select Case kbn
                    Case "半角数字"
                        If Flg = 1 Then
                            checkKekka = CommonCheckFuc.CheckHankaku(KingakuFormat(CType(control, TextBox).Text.ToString), MsgParam, "1")
                        Else
                            checkKekka = CommonCheckFuc.CheckHankaku(KingakuFormat(CType(control, TextBox).Text.ToString), MsgParam)
                        End If
                    Case "半角英数字"
                        checkKekka = CommonCheckFuc.ChkHankakuEisuuji(KingakuFormat(CType(control, TextBox).Text.ToString), MsgParam)
                    Case "桁数"
                        checkKekka = CommonCheckFuc.CheckByte(KingakuFormat(CType(control, TextBox).Text.ToString), len, MsgParam, kakaFlg)
                    Case "禁止文字"
                        checkKekka = CommonCheckFuc.CheckKinsoku(KingakuFormat(CType(control, TextBox).Text.ToString), MsgParam)
                    Case "日付"
                        checkKekka = CommonCheckFuc.CheckYuukouHiduke(CType(control, TextBox).Text, MsgParam)
                End Select

            End If
        End If

        If checkKekka <> "" Then
            CommonMsgAndFocusBL.Append(checkKekka)
            CommonMsgAndFocusBL.AppendFocusCtrl(control)
        End If

    End Sub

    ''' <summary>
    ''' 整合性チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function SeigouseiCheck() As String

        If Me.ddlHattyusyo.SelectedValue = 1 Then

            If Me.ddlHtsTys.SelectedValue = 0 And Me.ddlHtsKj.SelectedValue = 0 And Me.ddlHtsKs.SelectedValue = 0 Then
                Return Messages.Instance.MSG2014E
            End If

        End If

        Return ""

    End Function

    ''' <summary>
    ''' 金額データFormat
    ''' </summary>
    ''' <param name="target">処理するデータ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function KingakuFormat(ByVal target As String) As String
        target = target.Trim
        If InStr(target, ",") > 0 Then
            target = target.Replace(",", "")
        End If
        Return target
    End Function

    ''' <summary>
    ''' 画面加盟店データは仮テーブルに作成する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function makeAkameitenTable() As KameitenDataSet.m_kameitenTableDataTable

        Dim tempTable As New KameitenDataSet.m_kameitenTableDataTable
        Dim i As Integer
        i = 0

        tempTable.Rows.Add(tempTable.NewRow)
        tempTable.Rows(i).Item("kameiten_cd") = ViewState.Item("kameiten_cd")

        If Me.tbxHosyouKigen.Text <> "" Then
            tempTable.Rows(i).Item("hosyou_kikan") = Me.tbxHosyouKigen.Text
        End If

        If Me.ddlHosyousyoHakkou.SelectedIndex <> 0 Then
            tempTable.Rows(i).Item("hosyousyo_hak_umu") = Me.ddlHosyousyoHakkou.SelectedValue
        End If

        If Me.ddlNyukinKakunin.SelectedIndex <> 0 Then
            tempTable.Rows(i).Item("nyuukin_kakunin_jyouken") = Me.ddlNyukinKakunin.SelectedValue
        End If

        If Me.tbxNyukinKakuninKakusyo.Text <> "" Then
            tempTable.Rows(i).Item("nyuukin_kakunin_oboegaki") = Me.tbxNyukinKakuninKakusyo.Text
        End If

        If Me.ddlKoujiKaisyaSeikyu.SelectedValue <> 0 Then
            tempTable.Rows(i).Item("koj_gaisya_seikyuu_umu") = Me.ddlKoujiKaisyaSeikyu.SelectedValue
        End If

        If Me.ddlKoujiTantouFlg.SelectedValue <> 0 Then
            tempTable.Rows(i).Item("koj_tantou_flg") = Me.ddlKoujiTantouFlg.SelectedValue
        End If

        If Me.ddlTysMitumorisyoFlg.SelectedIndex <> 0 Then
            tempTable.Rows(i).Item("tys_mitsyo_flg") = Me.ddlTysMitumorisyoFlg.SelectedValue
        End If

        If Me.ddlHattyusyoFlg.SelectedIndex <> 0 Then
            tempTable.Rows(i).Item("hattyuusyo_flg") = Me.ddlHattyusyoFlg.SelectedValue
        End If

        If Me.tbxMitumorisyoFileNm.Text <> "" Then
            tempTable.Rows(i).Item("mitsyo_file_mei") = Me.tbxMitumorisyoFileNm.Text
        End If

        tempTable.Rows(i).Item("upd_login_user_id") = ViewState.Item("user_id")

        '==================2017/01/01 李松涛 追加 液状化特約管理 新特約切替日↓==========================
        tempTable.Rows(i).Item("ekijyouka_tokuyaku_kanri") = Me.ddlEkijoukaTokuyakuKannri.Items(ddlEkijoukaTokuyakuKannri.SelectedIndex).Value
        tempTable.Rows(i).Item("shintokuyaku_kirikaedate") = Me.tbxSinToyoKaisiBi.Text
        '==================2017/01/01 李松涛 追加 液状化特約管理 新特約切替日↑==========================


        tempTable.Rows(i).Item("hosyousyo_hak_kakuninsya") = Me.tbx_hosyousyo_hak_kakuninsya.Text
        tempTable.Rows(i).Item("hosyousyo_hak_kakunin_date") = Me.tbx_hosyousyo_hak_kakunin_date.Text
        tempTable.Rows(i).Item("hikiwatasi_inji_umu") = Me.ddl_hikiwatasi_inji_umu.Items(ddl_hikiwatasi_inji_umu.SelectedIndex).Value

        tempTable.Rows(i).Item("hosyou_kikan_kakuninsya") = Me.tbx_hosyou_kikan_kakuninsya.Text
        tempTable.Rows(i).Item("hosyou_kikan_start_date") = Me.tbx_hosyou_kikan_start_date.Text
        tempTable.Rows(i).Item("hosyousyo_hassou_umu") = Me.ddl_hosyousyo_hassou_umu.Items(ddl_hosyousyo_hassou_umu.SelectedIndex).Value
        tempTable.Rows(i).Item("hosyousyo_hassou_umu_start_date") = Me.tbx_hosyousyo_hassou_umu_start_date.Text

        tempTable.Rows(i).Item("fuho_fax_kakuninsya") = Me.tbx_fuho_fax_kakuninsya.Text
        tempTable.Rows(i).Item("fuho_fax_kakunin_date") = Me.tbx_fuho_fax_kakunin_date.Text
        tempTable.Rows(i).Item("fuho_fax_umu") = Me.ddl_fuho_fax_umu.Items(ddl_fuho_fax_umu.SelectedIndex).Value




        'WEB申込_採番判別FLG
        If Me.ddlWebMoushikomiSaibanHanbetuFlg.SelectedIndex = 0 Then
            tempTable.Rows(i).Item("web_moushikomi_saiban_hanbetu_flg") = DBNull.Value
        Else
            tempTable.Rows(i).Item("web_moushikomi_saiban_hanbetu_flg") = "1"
        End If

        '発注書未着連携対象外FLG
        If Me.ddlHattyuusyoMichakuRenkeiTaisyougaiFlg.SelectedIndex = 0 Then
            tempTable.Rows(i).Item("hattyuusyo_michaku_renkei_taisyougai_flg") = DBNull.Value
        Else
            tempTable.Rows(i).Item("hattyuusyo_michaku_renkei_taisyougai_flg") = "1"
        End If

        'If tempTable.Rows(i).Item("shitei_seikyuusyo_umu") Is DBNull.Value OrElse tempTable.Rows(i).Item("shitei_seikyuusyo_umu") = "" Then
        'Else

        'End If

        'If tempTable.Rows(i).Item("shiroari_kensa_hyouji") Is DBNull.Value OrElse tempTable.Rows(i).Item("shiroari_kensa_hyouji") = "" Then
        'Else

        'End If
        tempTable.Rows(i).Item("shitei_seikyuusyo_umu") = Me.ddl_shitei_seikyuusyo_umu.SelectedValue
        tempTable.Rows(i).Item("shiroari_kensa_hyouji") = Me.ddl_shiroari_kensa_hyouji.SelectedValue

        Return tempTable

    End Function

    ''' <summary>
    ''' 画面加盟店データは仮テーブルに作成する
    ''' </summary>
    ''' <param name="bkn">部分区分</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function makeATorihikiTable(ByVal bkn As String) As KameitenTorihikiJyouhouDataSet.m_kameiten_torihiki_jouhouDataTable

        Dim tempTable As New KameitenTorihikiJyouhouDataSet.m_kameiten_torihiki_jouhouDataTable
        Dim i As Integer
        i = 0

        tempTable.Rows.Add(tempTable.NewRow)
        tempTable.Rows(i).Item("kameiten_cd") = ViewState.Item("kameiten_cd")

        If bkn = "gyoumu" Then
            '取引＿業務

            If Me.ddlTysMitumori.SelectedIndex <> 0 Then
                tempTable.Rows(i).Item("tys_mitsyo_flg") = Me.ddlTysMitumori.SelectedValue
            End If

            If Me.ddlKisoDanmenzu.SelectedIndex <> 0 Then
                tempTable.Rows(i).Item("ks_danmenzu_flg") = Me.ddlKisoDanmenzu.SelectedValue
            End If

            If Me.ddlTatouwaribikiKbn.SelectedIndex <> 0 Then
                tempTable.Rows(i).Item("tatou_waribiki_flg") = Me.ddlTatouwaribikiKbn.SelectedValue
            End If

            If Me.tbxTatouwaibikiBikou.Text <> "" Then
                tempTable.Rows(i).Item("tatou_waribiki_bikou") = Me.tbxTatouwaibikiBikou.Text
            End If

            If Me.ddlTokkaSinsei.SelectedIndex <> 0 Then
                tempTable.Rows(i).Item("tokka_sinsei_flg") = Me.ddlTokkaSinsei.SelectedValue
            End If

            If Me.ddlZandoSyobunhi.SelectedIndex <> 0 Then
                tempTable.Rows(i).Item("zando_syobunhi_umu") = Me.ddlZandoSyobunhi.SelectedValue
            End If
            If Me.ddlKyusuisyadai.SelectedIndex <> 0 Then
                tempTable.Rows(i).Item("kyuusuisyadai_umu") = Me.ddlKyusuisyadai.SelectedValue
            End If
            If Me.ddlJinawahari.SelectedIndex <> 0 Then
                tempTable.Rows(i).Item("jinawa_taiou_umu") = Me.ddlJinawahari.SelectedValue
            End If
            If Me.ddlKuisindasi.SelectedIndex <> 0 Then
                tempTable.Rows(i).Item("kousin_taiou_umu") = Me.ddlKuisindasi.SelectedValue
            End If
            If Me.tbxHeikinhisuu.Text <> "" Then
                tempTable.Rows(i).Item("tys_kojkan_heikin_nissuu") = Me.tbxHeikinhisuu.Text
            End If
            If Me.tbxHyoujunKiso.Text <> "" Then
                tempTable.Rows(i).Item("hyoujun_ks") = Me.tbxHyoujunKiso.Text
            End If
            If Me.ddlJHSigaiKouji.SelectedIndex <> 0 Then
                tempTable.Rows(i).Item("js_igai_koj_flg") = Me.ddlJHSigaiKouji.SelectedValue
            End If
            If Me.ddlTysDoufu.SelectedIndex <> 0 Then
                tempTable.Rows(i).Item("tys_hkks_douhuu_umu") = Me.ddlTysDoufu.SelectedValue
            End If
            If Me.ddlKjDoufu.SelectedIndex <> 0 Then
                tempTable.Rows(i).Item("koj_hkks_douhuu_umu") = Me.ddlKjDoufu.SelectedValue
            End If
            If Me.ddlKsDoufu.SelectedIndex <> 0 Then
                tempTable.Rows(i).Item("kensa_hkks_douhuu_umu") = Me.ddlKsDoufu.SelectedValue
            End If
            If Me.tbxKjHks.Text <> "" Then
                tempTable.Rows(i).Item("koj_hkks_busuu") = Me.tbxKjHks.Text
            End If
            If Me.tbxKsHks.Text <> "" Then
                tempTable.Rows(i).Item("kensa_hkks_busuu") = Me.tbxKsHks.Text
            End If
            If Me.tbxTysHks.Text <> "" Then
                tempTable.Rows(i).Item("tys_hkks_busuu") = Me.tbxTysHks.Text
            End If
            If Me.tbxNkHssHakou.Text <> "" Then
                tempTable.Rows(i).Item("nyuukin_mae_hosyousyo_hak") = Me.tbxNkHssHakou.Text
            End If
            If Me.ddlHikiwataFile.SelectedIndex <> 0 Then
                tempTable.Rows(i).Item("hikiwatasi_file_umu") = Me.ddlHikiwataFile.SelectedValue
            End If

        ElseIf bkn = "keiri" Then
            '取引＿経理
            If Me.tbxKaisyuSimeibi.Text <> "" Then
                tempTable.Rows(i).Item("sime_date") = Me.tbxKaisyuSimeibi.Text
            End If
            If Me.tbxSeikyuhityakubi.Text <> "" Then
                tempTable.Rows(i).Item("seikyuusyo_hittyk_date") = Me.tbxSeikyuhityakubi.Text
            End If
            If Me.tbxSiharayibi.Text <> "" Then
                tempTable.Rows(i).Item("siharai_yotei_tuki") = Me.tbxSiharayibi.Text
            End If
            If Me.tbxGetugou.Text <> "" Then
                tempTable.Rows(i).Item("siharai_yotei_date") = Me.tbxGetugou.Text
            End If
            If Me.tbxGenkin.Text <> "" Then
                tempTable.Rows(i).Item("siharai_genkin_wariai") = Me.tbxGenkin.Text
            End If
            If Me.ddlSiharaiHouhou.SelectedIndex <> 0 Then
                tempTable.Rows(i).Item("siharai_houhou_flg") = Me.ddlSiharaiHouhou.SelectedValue
            End If
            If Me.tbxTegata.Text <> "" Then
                tempTable.Rows(i).Item("siharai_tegata_wariai") = Me.tbxTegata.Text
            End If
            If Me.tbxSiharaiSaito.Text <> "" Then
                tempTable.Rows(i).Item("siharai_site") = Me.tbxSiharaiSaito.Text
            End If
            If Me.tbxTyoufuTegata.Text <> "" Then
                tempTable.Rows(i).Item("tegata_hiritu") = Me.tbxTyoufuTegata.Text
            End If
            If Me.ddlHtsTys.SelectedIndex <> 0 Then
                tempTable.Rows(i).Item("tys_hattyuusyo_umu") = Me.ddlHtsTys.SelectedValue
            End If
            If Me.ddlHtsKj.SelectedIndex <> 0 Then
                tempTable.Rows(i).Item("koj_hattyuusyo_umu") = Me.ddlHtsKj.SelectedValue
            End If
            If Me.ddlHtsKs.SelectedIndex <> 0 Then
                tempTable.Rows(i).Item("kensa_hattyuusyo_umu") = Me.ddlHtsKs.SelectedValue
            End If
            If Me.ddlSenpoSiteiSks.SelectedIndex <> 0 Then
                tempTable.Rows(i).Item("senpou_sitei_seikyuusyo") = Me.ddlSenpoSiteiSks.SelectedValue
            End If
            If Me.tbxProKakaninbi.Text <> "" Then
                tempTable.Rows(i).Item("flow_kakunin_date") = Me.tbxProKakaninbi.Text
            End If
            If Me.ddlKyouryokuKaihi.SelectedIndex <> 0 Then
                tempTable.Rows(i).Item("kyouryoku_kaihi_umu") = Me.ddlKyouryokuKaihi.SelectedValue
            End If
            If Me.tbxTyoufuKyouryoukuKh.Text <> "" Then
                tempTable.Rows(i).Item("kyouryoku_kaihi_hiritu") = Me.tbxTyoufuKyouryoukuKh.Text
            End If

        End If

        tempTable.Rows(i).Item("upd_login_user_id") = ViewState.Item("user_id")

        tempTable.Rows(i).Item("add_login_user_id") = ViewState.Item("user_id")

            Return tempTable

    End Function

    Protected Sub tbxSinToyoKaisiBi_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbxSinToyoKaisiBi.TextChanged
        '請求書発行日、売上年月日　　　　 未入力の場合、自動設定　　　　　
        Dim dateValue As String
        dateValue = chkDate(Me.tbxSinToyoKaisiBi.Text)
        If dateValue <> "false" Then
            Me.tbxSinToyoKaisiBi.Text = dateValue
        Else
            ShowMsg("日付以外が入力されています。", Me.tbxSinToyoKaisiBi)
            Exit Sub
        End If
    End Sub

    '保証書自動発行_確認日
    Protected Sub tbx_hosyousyo_hak_kakunin_date_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbx_hosyousyo_hak_kakunin_date.TextChanged

        Dim dateValue As String
        dateValue = chkDate(Me.tbx_hosyousyo_hak_kakunin_date.Text)
        If dateValue <> "false" Then
            Me.tbx_hosyousyo_hak_kakunin_date.Text = dateValue
        Else
            ShowMsg("日付以外が入力されています。", Me.tbx_hosyousyo_hak_kakunin_date)
            Exit Sub
        End If
    End Sub

    '保証期間適用開始日
    Protected Sub tbx_hosyou_kikan_start_date_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbx_hosyou_kikan_start_date.TextChanged

        Dim dateValue As String
        dateValue = chkDate(Me.tbx_hosyou_kikan_start_date.Text)
        If dateValue <> "false" Then
            Me.tbx_hosyou_kikan_start_date.Text = dateValue
        Else
            ShowMsg("日付以外が入力されています。", Me.tbx_hosyou_kikan_start_date)
            Exit Sub
        End If
    End Sub


    '保証書発送有無_適用開始日
    Protected Sub tbx_hosyousyo_hassou_umu_start_date_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbx_hosyousyo_hassou_umu_start_date.TextChanged

        Dim dateValue As String
        dateValue = chkDate(Me.tbx_hosyousyo_hassou_umu_start_date.Text)
        If dateValue <> "false" Then
            Me.tbx_hosyousyo_hassou_umu_start_date.Text = dateValue
        Else
            ShowMsg("日付以外が入力されています。", Me.tbx_hosyousyo_hassou_umu_start_date)
            Exit Sub
        End If
    End Sub


    'サポート調査保証付保FAX確認日
    Protected Sub tbx_fuho_fax_kakunin_date_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbx_fuho_fax_kakunin_date.TextChanged

        Dim dateValue As String
        dateValue = chkDate(Me.tbx_fuho_fax_kakunin_date.Text)
        If dateValue <> "false" Then
            Me.tbx_fuho_fax_kakunin_date.Text = dateValue
        Else
            ShowMsg("日付以外が入力されています。", Me.tbx_fuho_fax_kakunin_date)
            Exit Sub
        End If
    End Sub




    Private KihonJyouhouInquiryBc As New Itis.Earth.BizLogic.kihonjyouhou.KihonJyouhouInquiryLogic
    Private Const YMD As String = "yyyy/MM/dd"
    ''' <summary>
    ''' 日符チェック
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function chkDate(ByVal value As String) As String

        Dim strBuf As String = String.Empty        '作業BUF

        chkDate = "false"

        '入力チェック
        If value = String.Empty Then
            Return String.Empty
        End If

        'If value = "00000000" Then
        '    '特別の場合です。
        '    Return "00000000"
        'End If

        'If Len(Trim(value)) = "0" Then
        '    '特別の場合です。
        '    Return "0"

        'End If

        '引数が数字のみか確認
        If IsNumeric(value) Then
            '文字カウント
            Select Case Len(value)
                Case 4   'MMDD
                    strBuf = Mid(Format(Convert.ToDateTime(KihonJyouhouInquiryBc.GetSysDate), "yyyy"), 1, 4)
                    strBuf = strBuf & "/"
                    strBuf = strBuf & Mid(value, 1, 2)
                    strBuf = strBuf & "/"
                    strBuf = strBuf & Mid(value, 3, 2)
                Case 6   'YYMMDD

                    If Mid(value, 1, 2) > "70" Then
                        strBuf = "19"
                    Else
                        strBuf = "20"
                    End If

                    strBuf = strBuf & Mid(value, 1, 2)
                    strBuf = strBuf & "/"
                    strBuf = strBuf & Mid(value, 3, 2)
                    strBuf = strBuf & "/"
                    strBuf = strBuf & Mid(value, 5, 2)

                Case 8   'YYYYMMDD

                    strBuf = strBuf & Mid(value, 1, 4)
                    strBuf = strBuf & "/"
                    strBuf = strBuf & Mid(value, 5, 2)
                    strBuf = strBuf & "/"
                    strBuf = strBuf & Mid(value, 7, 2)

                Case Else
                    'エラー(入力された日付に誤りがある。)

                    Exit Function
            End Select

            '日付方に変換できる確認
            If Not IsDate(strBuf) Then
                'エラー(入力された日付に誤りがある。)
                Return "false"

            End If


            '"/"を含む入力
        ElseIf IsDate(value) Then
            strBuf = Format(CDate(value), YMD)
        Else
            'Date変換不能(入力された日付に誤りがある。)
            Return "false"
        End If



        Return strBuf

    End Function

    ''' <summary>
    ''' MsgBox表示
    ''' </summary>
    ''' <param name="msg"></param>
    ''' <param name="param1"></param>
    ''' <param name="param2"></param>
    ''' <param name="param3"></param>
    ''' <param name="param4"></param>
    ''' <remarks></remarks>
    Public Sub ShowMsg(ByVal msg As String, _
                                ByVal control As System.Web.UI.Control, _
                                Optional ByVal param1 As String = "", _
                                Optional ByVal param2 As String = "", _
                                Optional ByVal param3 As String = "", _
                                Optional ByVal param4 As String = "")

        Dim csScript As New StringBuilder


        Dim pPage As Page = Parent.Page
        Dim pType As Type = pPage.GetType
        Dim methodInfo As System.Reflection.MethodInfo = pType.GetMethod("ShowMsg")

        msg = msg.Replace("@PARAM1", param1) _
                          .Replace("@PARAM2", param2) _
                          .Replace("@PARAM3", param3) _
                          .Replace("@PARAM4", param4)

        If Not methodInfo Is Nothing Then
            methodInfo.Invoke(pPage, New Object() {control.ClientID, csScript.AppendFormat( _
                                                                "" & msg & "", _
                                                                param1, param2, param3, param4).ToString _
                                                                })
        End If


    End Sub

    Private Function NullToEmpty(ByVal v As Object) As Object

        If v Is DBNull.Value Then
            Return String.Empty
        Else
            Return v.ToString()
        End If
    End Function
End Class
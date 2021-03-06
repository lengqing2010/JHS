Imports Itis.Earth.BizLogic
Imports Itis.Earth.DataAccess
Partial Public Class YosinJyouhouInput
    Inherits System.Web.UI.Page

    Private YosinJyouhouInputLogic As New YosinJyouhouInputLogic

    ''' <summary>
    ''' ページロード
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'ユーザーチェック
        Dim Ninsyou As New Ninsyou()
        If Ninsyou.GetUserID() = "" Then
            Context.Items("strFailureMsg") = Messages.Instance.MSG2024E
            Server.Transfer("CommonErr.aspx")
        End If
        Me.Page.Form.Attributes.Add("onkeydown", "if(event.keyCode==13){return false;}")
        '権限チェック
        Dim user_info As New LoginUserInfo
        Dim jBn As New Jiban
        'ユーザー基本認証
        jBn.userAuth(user_info)
        If user_info Is Nothing Then
            'Context.Items("strFailureMsg") = Messages.Instance.MSG2020E
            'Server.Transfer("CommonErr.aspx")
        End If

        If (IsPostBack = False) Then
            '名寄先コード
            If Context.Items("nayose_cd") IsNot Nothing Then
                ViewState("nayose_cd") = Context.Items("nayose_cd")
            Else
                ViewState("nayose_cd") = Request.QueryString("strNayoseCd")
            End If
            If Context.Items("strKameitenCd") IsNot Nothing Then
                ViewState("strKameitenCd") = Context.Items("strKameitenCd")
            Else
                ViewState("strKameitenCd") = Request.QueryString("strKameitenCd")
            End If
            If Context.Items("modoru") IsNot Nothing Then
                ViewState("modoru") = Context.Items("modoru")
            Else
                ViewState("modoru") = Request.QueryString("modoru")
            End If
            If ViewState("modoru") = "YosinJyouhouDirectInquiry.aspx" Then
                btnModoru.Text = "閉じる"
                btnModoru.Attributes.Add("onclick", "window.close();")
            End If


            Dim commonChk As New CommonCheck
            commonChk.SetURL(Me, Ninsyou.GetUserID())

            If ViewState("nayose_cd") <> String.Empty Then

                Dim dtYosinKanriInfo As New YosinJyouhouInputDataSet.YosinKanriInfoTableDataTable
                dtYosinKanriInfo = YosinJyouhouInputLogic.GetYosinKanriInfo(ViewState("nayose_cd"))

                If dtYosinKanriInfo.Rows.Count = 1 Then

                    '名寄先コード
                    Me.tbxNayoseSakiCd.Text = ViewState("nayose_cd")
                    Me.tbxNayoseSakiCd.ReadOnly = True
                    '名寄先名１
                    Me.tbxNayoseSakiName1.Text = dtYosinKanriInfo.Rows(0).Item("nayose_saki_name1").ToString
                    ViewState("nayose_mei") = dtYosinKanriInfo.Rows(0).Item("nayose_saki_name1").ToString
                    Me.tbxNayoseSakiName1.ReadOnly = True
                    '名寄先カナ１
                    Me.tbxNayoseSakiKana1.Text = dtYosinKanriInfo.Rows(0).Item("nayose_saki_kana1").ToString
                    Me.tbxNayoseSakiKana1.ReadOnly = True
                    '名寄先名２
                    Me.tbxNayoseSakiName2.Text = dtYosinKanriInfo.Rows(0).Item("nayose_saki_name2").ToString
                    Me.tbxNayoseSakiName2.ReadOnly = True
                    '名寄先カナ２
                    Me.tbxNayoseSakiKana2.Text = dtYosinKanriInfo.Rows(0).Item("nayose_saki_kana2").ToString
                    Me.tbxNayoseSakiKana2.ReadOnly = True
                    '与信限度額
                    Me.tbxYosinGendoGaku.Text = addFigure(dtYosinKanriInfo.Rows(0).Item("yosin_gendo_gaku"))
                    Me.tbxYosinGendoGaku.ReadOnly = True
                    '与信警告開始率
                    If dtYosinKanriInfo.Rows(0).Item("yosin_keikou_kaisiritsu") IsNot DBNull.Value Then
                        Me.tbxYosinKeikouKaisiritsu.Text = dtYosinKanriInfo.Rows(0).Item("yosin_keikou_kaisiritsu") * 100
                    End If
                    Me.tbxYosinKeikouKaisiritsu.ReadOnly = True
                    '帝国評点
                    If dtYosinKanriInfo.Rows(0).Item("teikoku_hyouten") IsNot DBNull.Value Then
                        Me.tbxTeikokuHyouten.Text = dtYosinKanriInfo.Rows(0).Item("teikoku_hyouten")
                    End If
                    Me.tbxTeikokuHyouten.ReadOnly = True
                    '都道府県コード
                    If dtYosinKanriInfo.Rows(0).Item("todouhuken_cd") IsNot DBNull.Value Then
                        Me.tbxTodouhukenCd.Text = dtYosinKanriInfo.Rows(0).Item("todouhuken_cd").ToString
                    End If
                    Me.tbxTodouhukenCd.ReadOnly = True
                    '都道府県名
                    If dtYosinKanriInfo.Rows(0).Item("todouhuken_mei") IsNot DBNull.Value Then
                        Me.lblTodouhukenMei.Text = dtYosinKanriInfo.Rows(0).Item("todouhuken_mei").ToString
                    End If
                    '直工事FLG
                    If dtYosinKanriInfo.Rows(0).Item("tyoku_koji_flg") IsNot DBNull.Value Then
                        If dtYosinKanriInfo.Rows(0).Item("tyoku_koji_flg").ToString = "1" Then
                            Me.tbxTyokuKojiFlg.Text = "1"
                            Me.lblTyokuKojiFlg.Text = "直工事"
                        ElseIf dtYosinKanriInfo.Rows(0).Item("tyoku_koji_flg").ToString = "0" Then
                            Me.tbxTyokuKojiFlg.Text = "0"
                            Me.lblTyokuKojiFlg.Text = "なし"
                        End If
                    End If
                    Me.tbxTyokuKojiFlg.ReadOnly = True
                    '受注管理FLG
                    If dtYosinKanriInfo.Rows(0).Item("jyutyuu_kanri_flg") Is DBNull.Value Then
                        Me.lblJyutyuuKanriFlg.Text = "受注停止対象"
                    ElseIf dtYosinKanriInfo.Rows(0).Item("jyutyuu_kanri_flg").ToString = "1" Then
                        Me.tbxJyutyuuKanriFlg.Text = "1"
                        Me.lblJyutyuuKanriFlg.Text = "受注停止なし"
                    End If
                    Me.tbxJyutyuuKanriFlg.ReadOnly = True
                    '警告状況 
                    If dtYosinKanriInfo.Rows(0).Item("meisyou") IsNot DBNull.Value Then
                        Me.tbxKeikokuJoukyou.Text = dtYosinKanriInfo.Rows(0).Item("meisyou")
                    End If
                    Me.tbxKeikokuJoukyou.ReadOnly = True
                    '前日工事状況FLG 
                    If dtYosinKanriInfo.Rows(0).Item("zenjitsu_koji_flg") IsNot DBNull.Value Then
                        If dtYosinKanriInfo.Rows(0).Item("zenjitsu_koji_flg").ToString = "0" Then
                            Me.tbxZenjitsuKojiFlg.Text = "0"
                            Me.lblZenjitsuKojiFlg.Text = "工事無し"
                        ElseIf dtYosinKanriInfo.Rows(0).Item("zenjitsu_koji_flg").ToString = "1" Then
                            Me.tbxZenjitsuKojiFlg.Text = "1"
                            Me.lblZenjitsuKojiFlg.Text = "直工事のみ"
                        ElseIf dtYosinKanriInfo.Rows(0).Item("zenjitsu_koji_flg").ToString = "2" Then
                            Me.tbxZenjitsuKojiFlg.Text = "2"
                            Me.lblZenjitsuKojiFlg.Text = "JHS工事のみ"
                        ElseIf dtYosinKanriInfo.Rows(0).Item("zenjitsu_koji_flg").ToString = "3" Then
                            Me.tbxZenjitsuKojiFlg.Text = "3"
                            Me.lblZenjitsuKojiFlg.Text = "直&JHS工事あり"
                        End If
                    End If
                    Me.tbxZenjitsuKojiFlg.ReadOnly = True
                    '前月残高
                    Me.tbxZengetsuSaikenGaku.Text = addFigure(dtYosinKanriInfo.Rows(0).Item("zengetsu_saiken_gaku"))
                    Me.tbxZengetsuSaikenGaku.ReadOnly = True
                    '前月残高設定年月
                    Dim strZengetsuSaikenSetDate As String = String.Empty
                    If dtYosinKanriInfo.Rows(0).Item("zengetsu_saiken_set_date") IsNot DBNull.Value Then
                        Me.tbxZengetsuSaikenSetDate.Text = Left(dtYosinKanriInfo.Rows(0).Item("zengetsu_saiken_set_date").ToString, 10)
                        strZengetsuSaikenSetDate = Left(Me.tbxZengetsuSaikenSetDate.Text, 7)
                        If IsDate(strZengetsuSaikenSetDate) = False Then
                            Context.Items("strFailureMsg") = Messages.Instance.MSG2030E
                            Context.Items("strUrl") = "YosinJyouhouInquiry.aspx?strKameitenCd=" & ViewState("strKameitenCd")
                            Server.Transfer("CommonErr.aspx")
                        End If
                    End If
                    Me.tbxZengetsuSaikenSetDate.ReadOnly = True
                    '当月入金額
                    Me.tbxRuisekiNyuukinGaku.Text = addFigure(dtYosinKanriInfo.Rows(0).Item("ruiseki_nyuukin_gaku"))
                    Me.tbxRuisekiNyuukinGaku.ReadOnly = True
                    '当月入金額設定日FROM
                    Me.tbxRuisekiNyuukinSetDateFrom.Text = setKingakuNengetuFrom(strZengetsuSaikenSetDate)
                    Me.tbxRuisekiNyuukinSetDateFrom.ReadOnly = True
                    '当月入金額設定日TO
                    Me.tbxRuisekiNyuukinSetDateTo.Text = setKingakuNengetuTo(dtYosinKanriInfo.Rows(0).Item("ruiseki_nyuukin_set_date"))
                    Me.tbxRuisekiNyuukinSetDateTo.ReadOnly = True
                    '当月売上額(地盤)->調査・工事・その他
                    Me.tbxRuisekiJyutyuuGaku.Text = addFigure(CDbl(dtYosinKanriInfo.Rows(0).Item("ruiseki_jyutyuu_gaku")) + CDbl(dtYosinKanriInfo.Rows(0).Item("toujitsu_jyutyuu_gaku")))
                    Me.tbxRuisekiJyutyuuGaku.ReadOnly = True
                    '当月売上額設定日(地盤)FROM
                    Me.tbxRuisekiJyutyuuSetDateFrom.Text = setKingakuNengetuFrom(strZengetsuSaikenSetDate)
                    Me.tbxRuisekiJyutyuuSetDateFrom.ReadOnly = True
                    '当月売上額設定日(地盤)TO
                    Me.tbxRuisekiJyutyuuSetDateTo.Text = setKingakuNengetuTo(dtYosinKanriInfo.Rows(0).Item("ruiseki_jyutyuu_set_date"))
                    Me.tbxRuisekiJyutyuuSetDateTo.ReadOnly = True
                    '当月売上額(建物)
                    Me.tbxRuisekiKasiuriGaku.Text = addFigure(dtYosinKanriInfo.Rows(0).Item("ruiseki_kasiuri_gaku"))
                    Me.tbxRuisekiKasiuriGaku.ReadOnly = True
                    '当月売上額設定日(建物)FROM
                    Me.tbxRuisekiKasiuriSetDateFrom.Text = setKingakuNengetuFrom(strZengetsuSaikenSetDate)
                    Me.tbxRuisekiKasiuriSetDateFrom.ReadOnly = True
                    '当月売上額設定日(建物)TO
                    Me.tbxRuisekiKasiuriSetDateTo.Text = setKingakuNengetuTo(dtYosinKanriInfo.Rows(0).Item("ruiseki_kasiuri_set_date"))
                    Me.tbxRuisekiKasiuriSetDateTo.ReadOnly = True
                    ''当日受注額(地盤)
                    'Me.tbxToujitsuJyutyuuGaku.Text = addFigure(dtYosinKanriInfo.Rows(0).Item("toujitsu_jyutyuu_gaku"))
                    'Me.tbxToujitsuJyutyuuGaku.ReadOnly = True
                    '与信消化率
                    If dtYosinKanriInfo.Rows(0).Item("yosin_gendo_gaku") = 0 Then
                        Me.tbxYosinSyokaritu.Text = 0
                    Else
                        Me.tbxYosinSyokaritu.Text = Format((dtYosinKanriInfo.Rows(0).Item("saikaigaku") / dtYosinKanriInfo.Rows(0).Item("yosin_gendo_gaku")) * 100, 0)
                    End If
                    Me.tbxYosinSyokaritu.ReadOnly = True
                    '当月売掛金合計額
                    Me.tbxSaikengaku.Text = addFigure(dtYosinKanriInfo.Rows(0).Item("saikaigaku"))
                    Me.tbxSaikengaku.ReadOnly = True
                    '与信残額
                    Me.tbxYosinZangaku.Text = addFigure(dtYosinKanriInfo.Rows(0).Item("yosin_gendo_gaku") - dtYosinKanriInfo.Rows(0).Item("saikaigaku"))
                    Me.tbxYosinZangaku.ReadOnly = True
                    '入金予定情報
                    Dim dtNyuukinYoteiInfo As YosinJyouhouInputDataSet.NyuukinYoteiInfoTableDataTable
                    If dtYosinKanriInfo.Rows(0).Item("ruiseki_nyuukin_set_date").ToString = String.Empty Then
                        dtNyuukinYoteiInfo = YosinJyouhouInputLogic.GetNyuukinYoteiInfo(ViewState("nayose_cd"), Convert.ToDateTime("1900/01/01"))
                    Else
                        dtNyuukinYoteiInfo = YosinJyouhouInputLogic.GetNyuukinYoteiInfo(ViewState("nayose_cd"), Convert.ToDateTime(dtYosinKanriInfo.Rows(0).Item("ruiseki_nyuukin_set_date").ToString))
                    End If
                    If dtNyuukinYoteiInfo.Rows.Count = 0 Then
                        Me.nyuukinYoteiTbody.Attributes.Add("style", "display:none;")
                        Me.lblSign.Text = "設定無し"
                        Me.nyuukinYoteiTr.Visible = False
                    Else
                        Me.lblSign.Text = "設定有り"
                        For row As Integer = 0 To dtNyuukinYoteiInfo.Rows.Count - 1
                            '金額2
                            dtNyuukinYoteiInfo.Rows(row).Item("nyuukinyotei_gaku") = addFigure(dtNyuukinYoteiInfo.Rows(row).Item("nyuukinyotei_gaku"))
                            '予定日
                            If Not String.IsNullOrEmpty(dtNyuukinYoteiInfo.Rows(row).Item("nyuukinyotei_gaku")) Then
                                dtNyuukinYoteiInfo.Rows(row).Item("nyuukinyotei_date") = Left(dtNyuukinYoteiInfo.Rows(row).Item("nyuukinyotei_date"), 10)
                            End If
                        Next
                    End If
                    grdNyuukinYotei.DataSource = dtNyuukinYoteiInfo
                    grdNyuukinYotei.DataBind()
                    nyuukinYoteiLink.HRef = "javascript:changeDisplay('" & nyuukinYoteiTbody.ClientID & "');changeDisplay('" & nyuukinYoteiTitleInfobar.ClientID & "');"
                Else
                    Context.Items("strFailureMsg") = Messages.Instance.MSG020E
                    If ViewState("modoru") = "YosinJyouhouInquiry.aspx" Then
                        Context.Items("strUrl") = "YosinJyouhouInquiry.aspx?strKameitenCd=" & ViewState("strKameitenCd")
                    End If


                    Server.Transfer("CommonErr.aspx")
                End If
            Else
                Context.Items("strFailureMsg") = Messages.Instance.MSG020E
                If ViewState("modoru") = "YosinJyouhouInquiry.aspx" Then
                    Context.Items("strUrl") = "YosinJyouhouInquiry.aspx?strKameitenCd=" & ViewState("strKameitenCd")
                End If
                Server.Transfer("CommonErr.aspx")
            End If
        End If
    End Sub
    ''' <summary>
    ''' 金額の年月日FROMを設定
    ''' </summary>
    ''' <param name="strZengetsuSaikenSetDate">前月債権額設定年月</param>
    ''' <remarks></remarks>
    Function setKingakuNengetuFrom(ByVal strZengetsuSaikenSetDate As String) As String

        If strZengetsuSaikenSetDate = String.Empty Then
            Return String.Empty
        Else
            Return CDate(strZengetsuSaikenSetDate).AddMonths(1).ToString("yyyy/MM") & "/01"
        End If

    End Function
    ''' <summary>
    ''' 金額の年月日TOを設定
    ''' </summary>
    ''' <param name="KingakuNengetuTo">金額の年月日TO</param>
    ''' <remarks></remarks>
    Function setKingakuNengetuTo(ByVal KingakuNengetuTo As Object) As String

        If IsDBNull(KingakuNengetuTo) Then
            Return String.Empty
        Else
            Return Left(KingakuNengetuTo, 10)
        End If

    End Function
    ''' <summary>
    ''' カンマを付与
    ''' </summary>
    ''' <param name="kingaku">金額</param>
    ''' <remarks></remarks>
    Function addFigure(ByVal kingaku As String) As String

        Return Format(Convert.ToInt64(kingaku), "#,0")

    End Function
    ''' <summary>
    ''' 戻るボタン押下時の処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnModoru_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModoru.Click

        '加盟店与信情報画面へ遷移する
        If ViewState("modoru") = "YosinJyouhouDirectList.aspx" Then

            Server.Transfer("YosinJyouhouDirectList.aspx?strKameitenCd=" & ViewState("strKameitenCd") & "")
        ElseIf ViewState("modoru") = "YosinJyouhouInquiry.aspx" Then

            Server.Transfer("YosinJyouhouInquiry.aspx?strKameitenCd=" & ViewState("strKameitenCd") & "")

        End If

    End Sub

   

    Protected Sub btnMeisai_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMeisai.Click
        Context.Items("nayose_cd") = ViewState("nayose_cd")
        Context.Items("strKameitenCd") = ViewState("strKameitenCd")
        Context.Items("nayose_mei") = ViewState("nayose_mei")
        Context.Items("modoru") = ViewState("modoru")
        Server.Transfer("YosinTougetuInput.aspx")
    End Sub
End Class
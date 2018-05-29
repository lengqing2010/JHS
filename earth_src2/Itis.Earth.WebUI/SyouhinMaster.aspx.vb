Imports Itis.Earth.BizLogic
Partial Public Class SyouhinMaster
    Inherits System.Web.UI.Page
    Private blnBtn As Boolean
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strNinsyou As String = ""
        Dim commonChk As New CommonCheck
        Dim strUserID As String = ""
        '権限チェックおよび設定
        blnBtn = commonChk.CommonNinnsyou(strUserID, "keiri_gyoumu_kengen,kaiseki_master_kanri_kengen")
        ViewState("UserId") = strUserID
        If Not IsPostBack Then
            Dim SyouhinSearchLogic As New Itis.Earth.BizLogic.SyouhinMasterLogic
            Dim dtTable As New DataTable
            Dim intCount As Integer = 0
            dtTable = SyouhinSearchLogic.SelZeiKBNInfo
            Dim ddlist As New ListItem

            ddlist.Text = ""
            ddlist.Value = ""
            ddlZeiKBN.Items.Add(ddlist)
            For intCount = 0 To dtTable.Rows.Count - 1
                ddlist = New ListItem
                If TrimNull(dtTable.Rows(intCount).Item("zeiritu")) <> "" Then
                    ddlist.Text = dtTable.Rows(intCount).Item("zei_kbn") & ":" & dtTable.Rows(intCount).Item("zeiritu")
                Else
                    ddlist.Text = dtTable.Rows(intCount).Item("zei_kbn")
                End If

                ddlist.Value = dtTable.Rows(intCount).Item("zei_kbn")
                ddlZeiKBN.Items.Add(ddlist)
            Next
            '税込区分
            ddlist = New ListItem
            ddlist.Text = ""
            ddlist.Value = ""
            ddlZeikomi.Items.Add(ddlist)
            ddlist = New ListItem
            ddlist.Text = "税抜価格"
            ddlist.Value = "0"
            ddlZeikomi.Items.Add(ddlist)
            ddlist = New ListItem
            ddlist.Text = "税込価格"
            ddlist.Value = "1"
            ddlZeikomi.Items.Add(ddlist)
            '保証有無
            ddlist = New ListItem
            ddlist.Text = ""
            ddlist.Value = ""
            ddlHosyou.Items.Add(ddlist)
            ddlist = New ListItem
            ddlist.Text = "無"
            ddlist.Value = "0"
            ddlHosyou.Items.Add(ddlist)
            ddlist = New ListItem
            ddlist.Text = "有"
            ddlist.Value = "1"
            ddlHosyou.Items.Add(ddlist)
            '2011/03/01 商品マスタの項目を追加する 付龍(大連) ↓
            '調査有無
            ddlist = New ListItem
            ddlist.Text = ""
            ddlist.Value = ""
            ddlSyousa.Items.Add(ddlist)
            ddlist = New ListItem
            ddlist.Text = "無"
            ddlist.Value = "0"
            ddlSyousa.Items.Add(ddlist)
            ddlist = New ListItem
            ddlist.Text = "有"
            ddlist.Value = "1"
            ddlSyousa.Items.Add(ddlist)
            '2011/03/01 商品マスタの項目を追加する 付龍(大連) ↑

            SetKakutyou(ddlSyouhinKBN1, "41")

            SetKakutyou(ddlSyouhinKBN2, "42")
            SetKakutyou(ddlSyouhinKBN3, "43")
            SetKakutyou(ddlKouji, "44")

            SetKakutyou(ddlSoukoCd, "70")

            '2011/03/01 商品マスタの項目を追加する 付龍(大連) ↓
            SetKakutyou(ddlSyouhinSyubetu1, "51")
            SetKakutyou(ddlSyouhinSyubetu2, "52")
            SetKakutyou(ddlSyouhinSyubetu3, "53")
            SetKakutyou(ddlSyouhinSyubetu4, "54")
            SetKakutyou(ddlSyouhinSyubetu5, "55")
            '2011/03/01 商品マスタの項目を追加する 付龍(大連) ↑

            '2017/12/11 商品マスタの項目を追加する 李(大連) ↓
            SetKakutyou(ddlSyouhinSyubetu6, "58")
            SetKakutyou(ddlSyouhinSyubetu7, "59")
            SetKakutyou(ddlSyouhinSyubetu8, "80")
            SetKakutyou(ddlSyouhinSyubetu9, "81")
            SetKakutyou(ddlSyouhinSyubetu10, "82")
            SetKakutyou(ddlSyouhinSyubetu11, "83")
            SetKakutyou(ddlSyouhinSyubetu12, "84")
            SetKakutyou(ddlSyouhinSyubetu13, "85")
            SetKakutyou(ddlSyouhinSyubetu14, "86")
            SetKakutyou(ddlSyouhinSyubetu15, "87")
            SetKakutyou(ddlSyouhinSyubetu16, "88")
            SetKakutyou(ddlSyouhinSyubetu17, "89")
            '2017/12/11 商品マスタの項目を追加する 李(大連) ↑

            btnSyuusei.Enabled = False
            btnTouroku.Enabled = True
            '2017/12/11 商品マスタの項目を追加する 李(大連) ↓

            Dim dtMeisyo As DataTable = SyouhinSearchLogic.SelKakutyouInfo("57")

            For i As Integer = 0 To dtMeisyo.Rows.Count - 1
                Dim lbl As Label = CType(UpdatePanelA.FindControl("lblSyouhinMeisyo" & dtMeisyo.Rows(i).Item("code").ToString), Label)
                If dtMeisyo.Rows(i).Item("code") IsNot DBNull.Value Then
                    lbl.Text = dtMeisyo.Rows(i).Item("meisyou").ToString
                End If
            Next
            '2017/12/11 商品マスタの項目を追加する 李(大連) ↑


        End If
        tbxSyouhin_mei.Attributes.Add("readonly", "true;")
        tbxHyoujun.Attributes.Add("onblur", "fncCheckMainasu(this);checkNumberAddFig(this);")
        tbxSiire.Attributes.Add("onblur", "checkNumberAddFig(this);")
        tbxSyanai.Attributes.Add("onblur", "checkNumberAddFig(this);")

        tbxHyoujun.Attributes.Add("onfocus", "removeFig(this);")
        tbxSiire.Attributes.Add("onfocus", "removeFig(this);")
        tbxSyanai.Attributes.Add("onfocus", "removeFig(this);")
        btnClearMeisai.Attributes.Add("onclick", "if (!confirm('クリアを行ないます。\nよろしいですか？')){return false;};")
        MakeScript()
        If Not blnBtn Then
            btnSyuusei.Enabled = False
            btnTouroku.Enabled = False
        End If
    End Sub
    ''' <summary>Javascript作成</summary>
    Private Sub MakeScript()
        Dim csType As Type = Page.GetType()
        Dim csName As String = "GetScript"
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("<script language='javascript' type='text/javascript'>")
            .AppendLine("function  fncCheckMainasu(OBJ)")
            .AppendLine("{")
            .AppendLine("if (!isNaN(parseFloat(OBJ.value))){")
            .AppendLine("if (parseFloat(OBJ.value)<0){")
            .AppendLine("alert('" & String.Format(Messages.Instance.MSG091E, "標準価格", "金額") & "');")
            .AppendLine("OBJ.focus();")
            .AppendLine("}")
            .AppendLine("}")
            .AppendLine("}")

            .AppendLine("</script>")
        End With
        ClientScript.RegisterClientScriptBlock(csType, csName, csScript.ToString)
    End Sub
    Sub SetKakutyou(ByVal ddl As DropDownList, ByVal strSyubetu As String)
        Dim SyouhinSearchLogic As New Itis.Earth.BizLogic.SyouhinMasterLogic
        Dim dtTable As New DataTable
        Dim intCount As Integer = 0
        dtTable = SyouhinSearchLogic.SelKakutyouInfo(strSyubetu)

        Dim ddlist As New ListItem
        ddlist.Text = ""
        ddlist.Value = ""
        ddl.Items.Add(ddlist)
        For intCount = 0 To dtTable.Rows.Count - 1
            ddlist = New ListItem
            If strSyubetu = "70" Then
                If TrimNull(dtTable.Rows(intCount).Item("meisyou")) <> "" Then
                    ddlist.Text = TrimNull(dtTable.Rows(intCount).Item("code")) & ":" & TrimNull(dtTable.Rows(intCount).Item("meisyou"))
                Else
                    ddlist.Text = TrimNull(dtTable.Rows(intCount).Item("code"))
                End If

            Else
                ddlist.Text = TrimNull(dtTable.Rows(intCount).Item("meisyou"))

            End If
            ddlist.Value = TrimNull(dtTable.Rows(intCount).Item("code"))
            ddl.Items.Add(ddlist)
        Next
    End Sub
    ''' <summary>空白を削除</summary>
    Private Function TrimNull(ByVal objStr As Object) As String
        If IsDBNull(objStr) Then
            TrimNull = ""
        Else
            TrimNull = objStr.ToString.Trim
        End If
    End Function
    Protected Sub btnSearchSyouhin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchSyouhin.Click
        Dim CommonSearchLogic As New Itis.Earth.BizLogic.SyouhinMasterLogic
        Dim strScript As String = ""
        Dim dtSyouhinTable As New Itis.Earth.DataAccess.SyouhinDataSet.m_syouhinDataTable
        dtSyouhinTable = CommonSearchLogic.GetSyouhinInfo(tbxSyouhin_cd.Text)

        If dtSyouhinTable.Rows.Count = 1 Then
            tbxSyouhin_cd.Text = dtSyouhinTable.Item(0).syouhin_cd
            tbxSyouhin_mei.Text = dtSyouhinTable.Item(0).syouhin_mei
        Else
            tbxSyouhin_mei.Text = ""
            strScript = "objSrchWin = window.open('search_common.aspx?Kbn='+escape('商品')+'&soukoCd='+escape('#')+'&FormName=" & _
            Me.Page.Form.Name & "&objCd=" & _
           tbxSyouhin_cd.ClientID & _
                    "&objMei=" & tbxSyouhin_mei.ClientID & _
                    "&strCd='+escape(eval('document.all.'+'" & _
                    tbxSyouhin_cd.ClientID & "').value)+'&strMei='+escape(eval('document.all.'+'" & _
                    tbxSyouhin_mei.ClientID & "').value), 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strScript, True)

        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim commoncheck As New CommonCheck
        Dim strErr As String = ""

        If strErr = "" Then
            strErr = commoncheck.CheckHissuNyuuryoku(tbxSyouhin_cd.Text, "商品コード")
        End If
        If strErr = "" Then
            strErr = commoncheck.ChkHankakuEisuuji(tbxSyouhin_cd.Text, "商品コード")
        End If
        If strErr <> "" Then
            strErr = "alert('" & strErr & "');document.getElementById('" & tbxSyouhin_cd.ClientID & "').focus();"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strErr, True)

        Else
            GetMeisaiData(tbxSyouhin_cd.Text, "btnSearch")
        End If
    End Sub
    Sub GetMeisaiData(ByVal SyouhinCd As String, Optional ByVal btn As String = "")
        Dim strErr As String = ""
        Dim dtSyouhinDataSet As New Itis.Earth.DataAccess.SyouhinDataSet.m_syouhinDataTable
        Dim SyouhinLogic As New Itis.Earth.BizLogic.SyouhinMasterLogic
        dtSyouhinDataSet = SyouhinLogic.SelSyouhinInfo(SyouhinCd)
        If dtSyouhinDataSet.Rows.Count = 1 Then
            With dtSyouhinDataSet.Item(0)
                '取消
                chkTorikesi.Checked = IIf(.torikesi = "0", False, True)
                '商品コード
                tbxSyouhin_cd.Text = .syouhin_cd
                tbxSyouhinCd.Text = .syouhin_cd
                '商品名
                tbxSyouhinMei.Text = TrimNull(.syouhin_mei)
                If btn = "btnSearch" Then
                    tbxSyouhin_mei.Text = tbxSyouhinMei.Text
                End If

                '単位
                tbxTanni.Text = TrimNull(.tani)
                '支払用商品名
                tbxShiharaiSyouhin.Text = TrimNull(.shri_you_syouhin_mei)
                '税区分
                SetDropSelect(ddlZeiKBN, TrimNull(.zei_kbn))
                '商品区分１
                SetDropSelect(ddlSyouhinKBN1, TrimNull(.syouhin_kbn1))
                '税込区分
                SetDropSelect(ddlZeikomi, TrimNull(.zeikomi_kbn))
                '商品区分２
                SetDropSelect(ddlSyouhinKBN2, TrimNull(.syouhin_kbn2))
                '工事タイプ
                SetDropSelect(ddlKouji, TrimNull(.koj_type))
                '商品区分３
                SetDropSelect(ddlSyouhinKBN3, TrimNull(.syouhin_kbn3))
                '保証有無
                SetDropSelect(ddlHosyou, TrimNull(.hosyou_umu))
                '倉庫コード
                SetDropSelect(ddlSoukoCd, TrimNull(.souko_cd))

                '2011/03/01 商品マスタの項目を追加する 付龍(大連) ↓
                SetDropSelect(ddlSyousa, TrimNull(.tys_umu_kbn))
                SetDropSelect(ddlSyouhinSyubetu1, TrimNull(.syouhin_syubetu1))
                SetDropSelect(ddlSyouhinSyubetu2, TrimNull(.syouhin_syubetu2))
                SetDropSelect(ddlSyouhinSyubetu3, TrimNull(.syouhin_syubetu3))
                SetDropSelect(ddlSyouhinSyubetu4, TrimNull(.syouhin_syubetu4))
                SetDropSelect(ddlSyouhinSyubetu5, TrimNull(.syouhin_syubetu5))
                '2011/03/01 商品マスタの項目を追加する 付龍(大連) ↑

                '2017/12/11 商品マスタの項目を追加する 李(大連) ↓
                SetDropSelect(ddlSyouhinSyubetu6, TrimNull(.syouhin_syubetu6))
                SetDropSelect(ddlSyouhinSyubetu7, TrimNull(.syouhin_syubetu7))
                SetDropSelect(ddlSyouhinSyubetu8, TrimNull(.syouhin_syubetu8))
                SetDropSelect(ddlSyouhinSyubetu9, TrimNull(.syouhin_syubetu9))
                SetDropSelect(ddlSyouhinSyubetu10, TrimNull(.syouhin_syubetu10))
                SetDropSelect(ddlSyouhinSyubetu11, TrimNull(.syouhin_syubetu11))
                SetDropSelect(ddlSyouhinSyubetu12, TrimNull(.syouhin_syubetu12))
                SetDropSelect(ddlSyouhinSyubetu13, TrimNull(.syouhin_syubetu13))
                SetDropSelect(ddlSyouhinSyubetu14, TrimNull(.syouhin_syubetu14))
                SetDropSelect(ddlSyouhinSyubetu15, TrimNull(.syouhin_syubetu15))
                SetDropSelect(ddlSyouhinSyubetu16, TrimNull(.syouhin_syubetu16))
                SetDropSelect(ddlSyouhinSyubetu17, TrimNull(.syouhin_syubetu17))
                '2017/12/11 商品マスタの項目を追加する 李(大連) ↑

                '標準価格
                tbxHyoujun.Text = AddComa(.hyoujun_kkk)
                '仕入価格
                tbxSiire.Text = AddComa(.siire_kkk)
                '社内原価
                tbxSyanai.Text = AddComa(.syanai_genka)

                '2013/11/06 李宇追加 ↓
                'SDS自動設定
                SetDropSelect(Me.ddlSdsSeltutei, TrimNull(.sds_jidou_set))
                '2013/11/06 李宇追加 ↑

                hidUPDTime.Value = TrimNull(.upd_datetime)
                UpdatePanelA.Update()
            End With
            If Not blnBtn Then
                btnSyuusei.Enabled = False
                btnTouroku.Enabled = False
            Else
                btnSyuusei.Enabled = True
                btnTouroku.Enabled = False
            End If

            tbxSyouhinCd.Attributes.Add("readonly", "true;")
        Else
            MeisaiClear()
            strErr = "alert('" & Messages.Instance.MSG2034E & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strErr, True)
            If btn <> "btnSearch" Then
                btnSyuusei.Enabled = False
                btnTouroku.Enabled = True
                tbxSyouhinCd.Attributes.Remove("readonly")
            End If
            tbxSyouhin_mei.Text = ""
            'tbxSyouhin_mei.Text = ""
        End If
    End Sub
    ''' <summary>
    ''' 項目データにコマを追加
    ''' </summary>
    ''' <param name="kekka">金額</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function AddComa(ByVal kekka As String) As String
        If TrimNull(kekka) = "" Then
            Return ""
        Else
            Return CInt(kekka).ToString("###,###,##0")
        End If

    End Function
    Sub SetDropSelect(ByVal ddl As DropDownList, ByVal strValue As String)

        If ddl.Items.FindByValue(strValue) IsNot Nothing Then
            ddl.SelectedValue = strValue
        Else
            ddl.SelectedIndex = 0
        End If
    End Sub

    Private Sub btnTouroku_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTouroku.Click
        Dim strErr As String = ""
        Dim strID As String = ""
        strID = InputCheck(strErr)
        If strErr <> "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('" & strErr & "');document.getElementById('" & strID & "').focus();", True)
        Else
            Dim dtSyouhinDataSet As New Itis.Earth.DataAccess.SyouhinDataSet.m_syouhinDataTable
            Dim SyouhinLogic As New Itis.Earth.BizLogic.SyouhinMasterLogic
            dtSyouhinDataSet = SyouhinLogic.SelSyouhinInfo(tbxSyouhinCd.Text)
            If dtSyouhinDataSet.Rows.Count <> 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('マスターに重複データが存在します。');document.getElementById('" & tbxSyouhinCd.ClientID & "').focus();", True)
                Return
            End If
           
            If SyouhinLogic.InsSyouhin(SetMeisaiData) Then
                strErr = "alert('" & Replace(Messages.Instance.MSG018S, "@PARAM1", "商品マスタ") & "');"

                GetMeisaiData(tbxSyouhinCd.Text)
            Else
                strErr = "alert('" & Replace(Messages.Instance.MSG019E, "@PARAM1", "商品マスタ") & "');"
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strErr, True)
        End If
    End Sub
    Function SetMeisaiData() As Itis.Earth.DataAccess.SyouhinDataSet.m_syouhinDataTable
        Dim dtSyouhinDataSet As New Itis.Earth.DataAccess.SyouhinDataSet.m_syouhinDataTable

        dtSyouhinDataSet.Rows.Add(dtSyouhinDataSet.NewRow)
        dtSyouhinDataSet.Item(0).torikesi = IIf(chkTorikesi.Checked, "1", "0")
        dtSyouhinDataSet.Item(0).syouhin_cd = tbxSyouhinCd.Text
        dtSyouhinDataSet.Item(0).syouhin_mei = tbxSyouhinMei.Text
        dtSyouhinDataSet.Item(0).tani = tbxTanni.Text
        dtSyouhinDataSet.Item(0).shri_you_syouhin_mei = tbxShiharaiSyouhin.Text
        dtSyouhinDataSet.Item(0).zei_kbn = ddlZeiKBN.SelectedValue
        dtSyouhinDataSet.Item(0).syouhin_kbn1 = ddlSyouhinKBN1.SelectedValue
        dtSyouhinDataSet.Item(0).zeikomi_kbn = ddlZeikomi.SelectedValue
        dtSyouhinDataSet.Item(0).syouhin_kbn2 = ddlSyouhinKBN2.SelectedValue
        dtSyouhinDataSet.Item(0).koj_type = ddlKouji.SelectedValue
        dtSyouhinDataSet.Item(0).syouhin_kbn3 = ddlSyouhinKBN3.SelectedValue
        dtSyouhinDataSet.Item(0).hosyou_umu = ddlHosyou.SelectedValue
        dtSyouhinDataSet.Item(0).souko_cd = ddlSoukoCd.SelectedValue
        '2011/03/01 商品マスタの項目を追加する 付龍(大連) ↓
        dtSyouhinDataSet.Item(0).tys_umu_kbn = ddlSyousa.SelectedValue
        dtSyouhinDataSet.Item(0).syouhin_syubetu1 = ddlSyouhinSyubetu1.SelectedValue
        dtSyouhinDataSet.Item(0).syouhin_syubetu2 = ddlSyouhinSyubetu2.SelectedValue
        dtSyouhinDataSet.Item(0).syouhin_syubetu3 = ddlSyouhinSyubetu3.SelectedValue
        dtSyouhinDataSet.Item(0).syouhin_syubetu4 = ddlSyouhinSyubetu4.SelectedValue
        dtSyouhinDataSet.Item(0).syouhin_syubetu5 = ddlSyouhinSyubetu5.SelectedValue
        '2011/03/01 商品マスタの項目を追加する 付龍(大連) ↑

        '2017/12/11 商品マスタの項目を追加する 李(大連) ↓
        dtSyouhinDataSet.Item(0).syouhin_syubetu6 = ddlSyouhinSyubetu6.SelectedValue
        dtSyouhinDataSet.Item(0).syouhin_syubetu7 = ddlSyouhinSyubetu7.SelectedValue
        dtSyouhinDataSet.Item(0).syouhin_syubetu8 = ddlSyouhinSyubetu8.SelectedValue
        dtSyouhinDataSet.Item(0).syouhin_syubetu9 = ddlSyouhinSyubetu9.SelectedValue
        dtSyouhinDataSet.Item(0).syouhin_syubetu10 = ddlSyouhinSyubetu10.SelectedValue
        dtSyouhinDataSet.Item(0).syouhin_syubetu11 = ddlSyouhinSyubetu11.SelectedValue
        dtSyouhinDataSet.Item(0).syouhin_syubetu12 = ddlSyouhinSyubetu12.SelectedValue
        dtSyouhinDataSet.Item(0).syouhin_syubetu13 = ddlSyouhinSyubetu13.SelectedValue
        dtSyouhinDataSet.Item(0).syouhin_syubetu14 = ddlSyouhinSyubetu14.SelectedValue
        dtSyouhinDataSet.Item(0).syouhin_syubetu15 = ddlSyouhinSyubetu15.SelectedValue
        dtSyouhinDataSet.Item(0).syouhin_syubetu16 = ddlSyouhinSyubetu16.SelectedValue
        dtSyouhinDataSet.Item(0).syouhin_syubetu17 = ddlSyouhinSyubetu17.SelectedValue
        '2017/12/11 商品マスタの項目を追加する 李(大連) ↑

        dtSyouhinDataSet.Item(0).hyoujun_kkk = Replace(tbxHyoujun.Text, ",", "")
        dtSyouhinDataSet.Item(0).siire_kkk = Replace(tbxSiire.Text, ",", "")
        dtSyouhinDataSet.Item(0).syanai_genka = Replace(tbxSyanai.Text, ",", "")
        dtSyouhinDataSet.Item(0).upd_login_user_id = ViewState("UserId")
        '2013/11/06 李宇追加 ↓
        'SDS自動設定
        dtSyouhinDataSet.Item(0).sds_jidou_set = ddlSdsSeltutei.SelectedValue
        '2013/11/06 李宇追加 ↑
        dtSyouhinDataSet.Item(0).upd_datetime = hidUPDTime.Value

        Return dtSyouhinDataSet
    End Function
    Function InputCheck(ByRef strErr As String) As String
        Dim commoncheck As New CommonCheck

        Dim strID As String = ""
        '商品コード
        If strErr = "" Then
            strErr = commoncheck.CheckHissuNyuuryoku(tbxSyouhinCd.Text, "商品コード")
            If strErr <> "" Then
                strID = tbxSyouhinCd.ClientID
            End If
        End If

        If strErr = "" Then
            strErr = commoncheck.ChkHankakuEisuuji(tbxSyouhinCd.Text, "商品コード")
            If strErr <> "" Then
                strID = tbxSyouhinCd.ClientID
            End If
        End If
        '商品名
        If strErr = "" Then
            strErr = commoncheck.CheckHissuNyuuryoku(tbxSyouhinMei.Text, "商品名")
            If strErr <> "" Then
                strID = tbxSyouhinMei.ClientID
            End If
        End If

        If strErr = "" Then
            strErr = commoncheck.CheckByte(tbxSyouhinMei.Text, 40, "商品名", kbn.ZENKAKU)
            If strErr <> "" Then
                strID = tbxSyouhinMei.ClientID
            End If
        End If
        If strErr = "" Then
            strErr = commoncheck.CheckKinsoku(tbxSyouhinMei.Text, "商品名")
            If strErr <> "" Then
                strID = tbxSyouhinMei.ClientID
            End If
        End If
        '単位
        If strErr = "" And tbxTanni.Text <> "" Then
            strErr = commoncheck.CheckByte(tbxTanni.Text, 4, "単位", kbn.ZENKAKU)
            If strErr <> "" Then
                strID = tbxTanni.ClientID
            End If
        End If
        If strErr = "" And tbxTanni.Text <> "" Then
            strErr = commoncheck.CheckKinsoku(tbxTanni.Text, "単位")
            If strErr <> "" Then
                strID = tbxTanni.ClientID
            End If
        End If
        '支払用商品名
        If strErr = "" And tbxShiharaiSyouhin.Text <> "" Then
            strErr = commoncheck.CheckByte(tbxShiharaiSyouhin.Text, 40, "支払用商品名", kbn.ZENKAKU)
            If strErr <> "" Then
                strID = tbxShiharaiSyouhin.ClientID
            End If
        End If
        If strErr = "" And tbxShiharaiSyouhin.Text <> "" Then
            strErr = commoncheck.CheckKinsoku(tbxShiharaiSyouhin.Text, "支払用商品名")
            If strErr <> "" Then
                strID = tbxShiharaiSyouhin.ClientID
            End If
        End If
        '商品区分3
        If strErr = "" Then
            strErr = commoncheck.CheckHissuNyuuryoku(ddlSyouhinKBN3.SelectedValue, "商品区分3")
            If strErr <> "" Then
                strID = ddlSyouhinKBN3.ClientID
            End If
        End If
        '保証有無
        If strErr = "" Then
            strErr = commoncheck.CheckHissuNyuuryoku(ddlHosyou.SelectedValue, "保証有無")
            If strErr <> "" Then
                strID = ddlHosyou.ClientID
            End If
        End If
        '標準価格
        If strErr = "" And tbxHyoujun.Text <> "" Then
            strErr = commoncheck.CheckNum(tbxHyoujun.Text, "標準価格")
            If strErr <> "" Then
                strID = tbxHyoujun.ClientID
            End If
        End If

        '仕入価格
        If strErr = "" And tbxSiire.Text <> "" Then
            strErr = commoncheck.CheckNum(tbxSiire.Text, "仕入価格", "1")
            If strErr <> "" Then
                strID = tbxSiire.ClientID
            End If
        End If


        '社内原価
        If strErr = "" And tbxSyanai.Text <> "" Then
            strErr = commoncheck.CheckNum(tbxSyanai.Text, "社内原価", "1")
            If strErr <> "" Then
                strID = tbxSyanai.ClientID
            End If
        End If


        Return strID

    End Function

    Protected Sub btnSyuusei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSyuusei.Click
        Dim SyouhinLogic As New Itis.Earth.BizLogic.SyouhinMasterLogic

        Dim strReturn As String = ""
        Dim strErr As String = ""
        Dim strID As String = ""
        strID = InputCheck(strErr)
        If strErr <> "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('" & strErr & "');document.getElementById('" & strID & "').focus();", True)
        Else
            strReturn = SyouhinLogic.UpdSyouhin(SetMeisaiData)
            If strReturn = "0" Then
                strErr = "alert('" & Replace(Messages.Instance.MSG018S, "@PARAM1", "商品マスタ") & "');"
                GetMeisaiData(tbxSyouhinCd.Text)
            ElseIf strReturn = "1" Then
                strErr = "alert('" & Replace(Messages.Instance.MSG019E, "@PARAM1", "商品マスタ") & "');"
            ElseIf strReturn = "2" Then
                strErr = "alert('" & Messages.Instance.MSG2049E & "');"
            Else
                strErr = "alert('" & strReturn & "');"
            End If

            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strErr, True)
        End If

    End Sub

    Protected Sub btnClearMeisai_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearMeisai.Click
        MeisaiClear()
    End Sub
    Sub MeisaiClear()
        '取消
        chkTorikesi.Checked = False
        '商品コード
        tbxSyouhinCd.Text = ""
        '商品名
        tbxSyouhinMei.Text = ""

        '単位
        tbxTanni.Text = ""
        '支払用商品名
        tbxShiharaiSyouhin.Text = ""
        '税区分
        SetDropSelect(ddlZeiKBN, "")
        '商品区分１
        SetDropSelect(ddlSyouhinKBN1, "")
        '税込区分
        SetDropSelect(ddlZeikomi, "")
        '商品区分２
        SetDropSelect(ddlSyouhinKBN2, "")
        '工事タイプ
        SetDropSelect(ddlKouji, "")
        '商品区分３
        SetDropSelect(ddlSyouhinKBN3, "")
        '保証有無
        SetDropSelect(ddlHosyou, "")
        '倉庫コード
        SetDropSelect(ddlSoukoCd, "")
        '2011/03/01 商品マスタの項目を追加する 付龍(大連) ↓
        SetDropSelect(ddlSyousa, "")
        SetDropSelect(ddlSyouhinSyubetu1, "")
        SetDropSelect(ddlSyouhinSyubetu2, "")
        SetDropSelect(ddlSyouhinSyubetu3, "")
        SetDropSelect(ddlSyouhinSyubetu4, "")
        SetDropSelect(ddlSyouhinSyubetu5, "")
        '2011/03/01 商品マスタの項目を追加する 付龍(大連) ↑

        '2017/12/11 商品マスタの項目を追加する 李(大連) ↓
        SetDropSelect(ddlSyouhinSyubetu6, "")
        SetDropSelect(ddlSyouhinSyubetu7, "")
        SetDropSelect(ddlSyouhinSyubetu8, "")
        SetDropSelect(ddlSyouhinSyubetu9, "")
        SetDropSelect(ddlSyouhinSyubetu10, "")
        SetDropSelect(ddlSyouhinSyubetu11, "")
        SetDropSelect(ddlSyouhinSyubetu12, "")
        SetDropSelect(ddlSyouhinSyubetu13, "")
        SetDropSelect(ddlSyouhinSyubetu14, "")
        SetDropSelect(ddlSyouhinSyubetu15, "")
        SetDropSelect(ddlSyouhinSyubetu16, "")
        SetDropSelect(ddlSyouhinSyubetu17, "")
        '2017/12/11 商品マスタの項目を追加する 李(大連) ↑

        '標準価格
        tbxHyoujun.Text = ""
        '仕入価格
        tbxSiire.Text = ""
        '社内原価
        tbxSyanai.Text = ""
        hidUPDTime.Value = ""
        '2013/11/06 李宇追加 ↓
        SetDropSelect(ddlSdsSeltutei, "")
        '2013/11/06 李宇追加 ↑
        If Not blnBtn Then
            btnSyuusei.Enabled = False
            btnTouroku.Enabled = False
        Else
            btnSyuusei.Enabled = False
            btnTouroku.Enabled = True
        End If

        tbxSyouhinCd.Attributes.Remove("readonly")
        UpdatePanelA.Update()
    End Sub

    Protected Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        '商品コード
        tbxSyouhin_cd.Text = ""
        '商品名
        tbxSyouhin_mei.Text = ""

    End Sub
    Protected Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Server.Transfer("MasterMainteMenu.aspx")
    End Sub
End Class
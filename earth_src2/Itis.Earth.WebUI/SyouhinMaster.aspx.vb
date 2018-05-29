Imports Itis.Earth.BizLogic
Partial Public Class SyouhinMaster
    Inherits System.Web.UI.Page
    Private blnBtn As Boolean
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strNinsyou As String = ""
        Dim commonChk As New CommonCheck
        Dim strUserID As String = ""
        '�����`�F�b�N����ѐݒ�
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
            '�ō��敪
            ddlist = New ListItem
            ddlist.Text = ""
            ddlist.Value = ""
            ddlZeikomi.Items.Add(ddlist)
            ddlist = New ListItem
            ddlist.Text = "�Ŕ����i"
            ddlist.Value = "0"
            ddlZeikomi.Items.Add(ddlist)
            ddlist = New ListItem
            ddlist.Text = "�ō����i"
            ddlist.Value = "1"
            ddlZeikomi.Items.Add(ddlist)
            '�ۏؗL��
            ddlist = New ListItem
            ddlist.Text = ""
            ddlist.Value = ""
            ddlHosyou.Items.Add(ddlist)
            ddlist = New ListItem
            ddlist.Text = "��"
            ddlist.Value = "0"
            ddlHosyou.Items.Add(ddlist)
            ddlist = New ListItem
            ddlist.Text = "�L"
            ddlist.Value = "1"
            ddlHosyou.Items.Add(ddlist)
            '2011/03/01 ���i�}�X�^�̍��ڂ�ǉ����� �t��(��A) ��
            '�����L��
            ddlist = New ListItem
            ddlist.Text = ""
            ddlist.Value = ""
            ddlSyousa.Items.Add(ddlist)
            ddlist = New ListItem
            ddlist.Text = "��"
            ddlist.Value = "0"
            ddlSyousa.Items.Add(ddlist)
            ddlist = New ListItem
            ddlist.Text = "�L"
            ddlist.Value = "1"
            ddlSyousa.Items.Add(ddlist)
            '2011/03/01 ���i�}�X�^�̍��ڂ�ǉ����� �t��(��A) ��

            SetKakutyou(ddlSyouhinKBN1, "41")

            SetKakutyou(ddlSyouhinKBN2, "42")
            SetKakutyou(ddlSyouhinKBN3, "43")
            SetKakutyou(ddlKouji, "44")

            SetKakutyou(ddlSoukoCd, "70")

            '2011/03/01 ���i�}�X�^�̍��ڂ�ǉ����� �t��(��A) ��
            SetKakutyou(ddlSyouhinSyubetu1, "51")
            SetKakutyou(ddlSyouhinSyubetu2, "52")
            SetKakutyou(ddlSyouhinSyubetu3, "53")
            SetKakutyou(ddlSyouhinSyubetu4, "54")
            SetKakutyou(ddlSyouhinSyubetu5, "55")
            '2011/03/01 ���i�}�X�^�̍��ڂ�ǉ����� �t��(��A) ��

            '2017/12/11 ���i�}�X�^�̍��ڂ�ǉ����� ��(��A) ��
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
            '2017/12/11 ���i�}�X�^�̍��ڂ�ǉ����� ��(��A) ��

            btnSyuusei.Enabled = False
            btnTouroku.Enabled = True
            '2017/12/11 ���i�}�X�^�̍��ڂ�ǉ����� ��(��A) ��

            Dim dtMeisyo As DataTable = SyouhinSearchLogic.SelKakutyouInfo("57")

            For i As Integer = 0 To dtMeisyo.Rows.Count - 1
                Dim lbl As Label = CType(UpdatePanelA.FindControl("lblSyouhinMeisyo" & dtMeisyo.Rows(i).Item("code").ToString), Label)
                If dtMeisyo.Rows(i).Item("code") IsNot DBNull.Value Then
                    lbl.Text = dtMeisyo.Rows(i).Item("meisyou").ToString
                End If
            Next
            '2017/12/11 ���i�}�X�^�̍��ڂ�ǉ����� ��(��A) ��


        End If
        tbxSyouhin_mei.Attributes.Add("readonly", "true;")
        tbxHyoujun.Attributes.Add("onblur", "fncCheckMainasu(this);checkNumberAddFig(this);")
        tbxSiire.Attributes.Add("onblur", "checkNumberAddFig(this);")
        tbxSyanai.Attributes.Add("onblur", "checkNumberAddFig(this);")

        tbxHyoujun.Attributes.Add("onfocus", "removeFig(this);")
        tbxSiire.Attributes.Add("onfocus", "removeFig(this);")
        tbxSyanai.Attributes.Add("onfocus", "removeFig(this);")
        btnClearMeisai.Attributes.Add("onclick", "if (!confirm('�N���A���s�Ȃ��܂��B\n��낵���ł����H')){return false;};")
        MakeScript()
        If Not blnBtn Then
            btnSyuusei.Enabled = False
            btnTouroku.Enabled = False
        End If
    End Sub
    ''' <summary>Javascript�쐬</summary>
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
            .AppendLine("alert('" & String.Format(Messages.Instance.MSG091E, "�W�����i", "���z") & "');")
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
    ''' <summary>�󔒂��폜</summary>
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
            strScript = "objSrchWin = window.open('search_common.aspx?Kbn='+escape('���i')+'&soukoCd='+escape('#')+'&FormName=" & _
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
            strErr = commoncheck.CheckHissuNyuuryoku(tbxSyouhin_cd.Text, "���i�R�[�h")
        End If
        If strErr = "" Then
            strErr = commoncheck.ChkHankakuEisuuji(tbxSyouhin_cd.Text, "���i�R�[�h")
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
                '���
                chkTorikesi.Checked = IIf(.torikesi = "0", False, True)
                '���i�R�[�h
                tbxSyouhin_cd.Text = .syouhin_cd
                tbxSyouhinCd.Text = .syouhin_cd
                '���i��
                tbxSyouhinMei.Text = TrimNull(.syouhin_mei)
                If btn = "btnSearch" Then
                    tbxSyouhin_mei.Text = tbxSyouhinMei.Text
                End If

                '�P��
                tbxTanni.Text = TrimNull(.tani)
                '�x���p���i��
                tbxShiharaiSyouhin.Text = TrimNull(.shri_you_syouhin_mei)
                '�ŋ敪
                SetDropSelect(ddlZeiKBN, TrimNull(.zei_kbn))
                '���i�敪�P
                SetDropSelect(ddlSyouhinKBN1, TrimNull(.syouhin_kbn1))
                '�ō��敪
                SetDropSelect(ddlZeikomi, TrimNull(.zeikomi_kbn))
                '���i�敪�Q
                SetDropSelect(ddlSyouhinKBN2, TrimNull(.syouhin_kbn2))
                '�H���^�C�v
                SetDropSelect(ddlKouji, TrimNull(.koj_type))
                '���i�敪�R
                SetDropSelect(ddlSyouhinKBN3, TrimNull(.syouhin_kbn3))
                '�ۏؗL��
                SetDropSelect(ddlHosyou, TrimNull(.hosyou_umu))
                '�q�ɃR�[�h
                SetDropSelect(ddlSoukoCd, TrimNull(.souko_cd))

                '2011/03/01 ���i�}�X�^�̍��ڂ�ǉ����� �t��(��A) ��
                SetDropSelect(ddlSyousa, TrimNull(.tys_umu_kbn))
                SetDropSelect(ddlSyouhinSyubetu1, TrimNull(.syouhin_syubetu1))
                SetDropSelect(ddlSyouhinSyubetu2, TrimNull(.syouhin_syubetu2))
                SetDropSelect(ddlSyouhinSyubetu3, TrimNull(.syouhin_syubetu3))
                SetDropSelect(ddlSyouhinSyubetu4, TrimNull(.syouhin_syubetu4))
                SetDropSelect(ddlSyouhinSyubetu5, TrimNull(.syouhin_syubetu5))
                '2011/03/01 ���i�}�X�^�̍��ڂ�ǉ����� �t��(��A) ��

                '2017/12/11 ���i�}�X�^�̍��ڂ�ǉ����� ��(��A) ��
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
                '2017/12/11 ���i�}�X�^�̍��ڂ�ǉ����� ��(��A) ��

                '�W�����i
                tbxHyoujun.Text = AddComa(.hyoujun_kkk)
                '�d�����i
                tbxSiire.Text = AddComa(.siire_kkk)
                '�Г�����
                tbxSyanai.Text = AddComa(.syanai_genka)

                '2013/11/06 ���F�ǉ� ��
                'SDS�����ݒ�
                SetDropSelect(Me.ddlSdsSeltutei, TrimNull(.sds_jidou_set))
                '2013/11/06 ���F�ǉ� ��

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
    ''' ���ڃf�[�^�ɃR�}��ǉ�
    ''' </summary>
    ''' <param name="kekka">���z</param>
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
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('�}�X�^�[�ɏd���f�[�^�����݂��܂��B');document.getElementById('" & tbxSyouhinCd.ClientID & "').focus();", True)
                Return
            End If
           
            If SyouhinLogic.InsSyouhin(SetMeisaiData) Then
                strErr = "alert('" & Replace(Messages.Instance.MSG018S, "@PARAM1", "���i�}�X�^") & "');"

                GetMeisaiData(tbxSyouhinCd.Text)
            Else
                strErr = "alert('" & Replace(Messages.Instance.MSG019E, "@PARAM1", "���i�}�X�^") & "');"
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
        '2011/03/01 ���i�}�X�^�̍��ڂ�ǉ����� �t��(��A) ��
        dtSyouhinDataSet.Item(0).tys_umu_kbn = ddlSyousa.SelectedValue
        dtSyouhinDataSet.Item(0).syouhin_syubetu1 = ddlSyouhinSyubetu1.SelectedValue
        dtSyouhinDataSet.Item(0).syouhin_syubetu2 = ddlSyouhinSyubetu2.SelectedValue
        dtSyouhinDataSet.Item(0).syouhin_syubetu3 = ddlSyouhinSyubetu3.SelectedValue
        dtSyouhinDataSet.Item(0).syouhin_syubetu4 = ddlSyouhinSyubetu4.SelectedValue
        dtSyouhinDataSet.Item(0).syouhin_syubetu5 = ddlSyouhinSyubetu5.SelectedValue
        '2011/03/01 ���i�}�X�^�̍��ڂ�ǉ����� �t��(��A) ��

        '2017/12/11 ���i�}�X�^�̍��ڂ�ǉ����� ��(��A) ��
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
        '2017/12/11 ���i�}�X�^�̍��ڂ�ǉ����� ��(��A) ��

        dtSyouhinDataSet.Item(0).hyoujun_kkk = Replace(tbxHyoujun.Text, ",", "")
        dtSyouhinDataSet.Item(0).siire_kkk = Replace(tbxSiire.Text, ",", "")
        dtSyouhinDataSet.Item(0).syanai_genka = Replace(tbxSyanai.Text, ",", "")
        dtSyouhinDataSet.Item(0).upd_login_user_id = ViewState("UserId")
        '2013/11/06 ���F�ǉ� ��
        'SDS�����ݒ�
        dtSyouhinDataSet.Item(0).sds_jidou_set = ddlSdsSeltutei.SelectedValue
        '2013/11/06 ���F�ǉ� ��
        dtSyouhinDataSet.Item(0).upd_datetime = hidUPDTime.Value

        Return dtSyouhinDataSet
    End Function
    Function InputCheck(ByRef strErr As String) As String
        Dim commoncheck As New CommonCheck

        Dim strID As String = ""
        '���i�R�[�h
        If strErr = "" Then
            strErr = commoncheck.CheckHissuNyuuryoku(tbxSyouhinCd.Text, "���i�R�[�h")
            If strErr <> "" Then
                strID = tbxSyouhinCd.ClientID
            End If
        End If

        If strErr = "" Then
            strErr = commoncheck.ChkHankakuEisuuji(tbxSyouhinCd.Text, "���i�R�[�h")
            If strErr <> "" Then
                strID = tbxSyouhinCd.ClientID
            End If
        End If
        '���i��
        If strErr = "" Then
            strErr = commoncheck.CheckHissuNyuuryoku(tbxSyouhinMei.Text, "���i��")
            If strErr <> "" Then
                strID = tbxSyouhinMei.ClientID
            End If
        End If

        If strErr = "" Then
            strErr = commoncheck.CheckByte(tbxSyouhinMei.Text, 40, "���i��", kbn.ZENKAKU)
            If strErr <> "" Then
                strID = tbxSyouhinMei.ClientID
            End If
        End If
        If strErr = "" Then
            strErr = commoncheck.CheckKinsoku(tbxSyouhinMei.Text, "���i��")
            If strErr <> "" Then
                strID = tbxSyouhinMei.ClientID
            End If
        End If
        '�P��
        If strErr = "" And tbxTanni.Text <> "" Then
            strErr = commoncheck.CheckByte(tbxTanni.Text, 4, "�P��", kbn.ZENKAKU)
            If strErr <> "" Then
                strID = tbxTanni.ClientID
            End If
        End If
        If strErr = "" And tbxTanni.Text <> "" Then
            strErr = commoncheck.CheckKinsoku(tbxTanni.Text, "�P��")
            If strErr <> "" Then
                strID = tbxTanni.ClientID
            End If
        End If
        '�x���p���i��
        If strErr = "" And tbxShiharaiSyouhin.Text <> "" Then
            strErr = commoncheck.CheckByte(tbxShiharaiSyouhin.Text, 40, "�x���p���i��", kbn.ZENKAKU)
            If strErr <> "" Then
                strID = tbxShiharaiSyouhin.ClientID
            End If
        End If
        If strErr = "" And tbxShiharaiSyouhin.Text <> "" Then
            strErr = commoncheck.CheckKinsoku(tbxShiharaiSyouhin.Text, "�x���p���i��")
            If strErr <> "" Then
                strID = tbxShiharaiSyouhin.ClientID
            End If
        End If
        '���i�敪3
        If strErr = "" Then
            strErr = commoncheck.CheckHissuNyuuryoku(ddlSyouhinKBN3.SelectedValue, "���i�敪3")
            If strErr <> "" Then
                strID = ddlSyouhinKBN3.ClientID
            End If
        End If
        '�ۏؗL��
        If strErr = "" Then
            strErr = commoncheck.CheckHissuNyuuryoku(ddlHosyou.SelectedValue, "�ۏؗL��")
            If strErr <> "" Then
                strID = ddlHosyou.ClientID
            End If
        End If
        '�W�����i
        If strErr = "" And tbxHyoujun.Text <> "" Then
            strErr = commoncheck.CheckNum(tbxHyoujun.Text, "�W�����i")
            If strErr <> "" Then
                strID = tbxHyoujun.ClientID
            End If
        End If

        '�d�����i
        If strErr = "" And tbxSiire.Text <> "" Then
            strErr = commoncheck.CheckNum(tbxSiire.Text, "�d�����i", "1")
            If strErr <> "" Then
                strID = tbxSiire.ClientID
            End If
        End If


        '�Г�����
        If strErr = "" And tbxSyanai.Text <> "" Then
            strErr = commoncheck.CheckNum(tbxSyanai.Text, "�Г�����", "1")
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
                strErr = "alert('" & Replace(Messages.Instance.MSG018S, "@PARAM1", "���i�}�X�^") & "');"
                GetMeisaiData(tbxSyouhinCd.Text)
            ElseIf strReturn = "1" Then
                strErr = "alert('" & Replace(Messages.Instance.MSG019E, "@PARAM1", "���i�}�X�^") & "');"
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
        '���
        chkTorikesi.Checked = False
        '���i�R�[�h
        tbxSyouhinCd.Text = ""
        '���i��
        tbxSyouhinMei.Text = ""

        '�P��
        tbxTanni.Text = ""
        '�x���p���i��
        tbxShiharaiSyouhin.Text = ""
        '�ŋ敪
        SetDropSelect(ddlZeiKBN, "")
        '���i�敪�P
        SetDropSelect(ddlSyouhinKBN1, "")
        '�ō��敪
        SetDropSelect(ddlZeikomi, "")
        '���i�敪�Q
        SetDropSelect(ddlSyouhinKBN2, "")
        '�H���^�C�v
        SetDropSelect(ddlKouji, "")
        '���i�敪�R
        SetDropSelect(ddlSyouhinKBN3, "")
        '�ۏؗL��
        SetDropSelect(ddlHosyou, "")
        '�q�ɃR�[�h
        SetDropSelect(ddlSoukoCd, "")
        '2011/03/01 ���i�}�X�^�̍��ڂ�ǉ����� �t��(��A) ��
        SetDropSelect(ddlSyousa, "")
        SetDropSelect(ddlSyouhinSyubetu1, "")
        SetDropSelect(ddlSyouhinSyubetu2, "")
        SetDropSelect(ddlSyouhinSyubetu3, "")
        SetDropSelect(ddlSyouhinSyubetu4, "")
        SetDropSelect(ddlSyouhinSyubetu5, "")
        '2011/03/01 ���i�}�X�^�̍��ڂ�ǉ����� �t��(��A) ��

        '2017/12/11 ���i�}�X�^�̍��ڂ�ǉ����� ��(��A) ��
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
        '2017/12/11 ���i�}�X�^�̍��ڂ�ǉ����� ��(��A) ��

        '�W�����i
        tbxHyoujun.Text = ""
        '�d�����i
        tbxSiire.Text = ""
        '�Г�����
        tbxSyanai.Text = ""
        hidUPDTime.Value = ""
        '2013/11/06 ���F�ǉ� ��
        SetDropSelect(ddlSdsSeltutei, "")
        '2013/11/06 ���F�ǉ� ��
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
        '���i�R�[�h
        tbxSyouhin_cd.Text = ""
        '���i��
        tbxSyouhin_mei.Text = ""

    End Sub
    Protected Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Server.Transfer("MasterMainteMenu.aspx")
    End Sub
End Class
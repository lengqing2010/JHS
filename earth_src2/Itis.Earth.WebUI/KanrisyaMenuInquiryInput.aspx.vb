Imports Itis.Earth.BizLogic
Imports Itis.Earth.DataAccess
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports System.Reflection

Partial Public Class KanrisyaMenuInquiryInput
    Inherits System.Web.UI.Page

    ''' <summary>���[�U�[�Ǘ������Ɖ�o�^����</summary>
    ''' <remarks>���[�U�[�Ǘ������Ɖ�o�^�@�\��񋟂���</remarks>
    ''' <history>
    ''' <para>2009/07/17�@����N(��A���V�X�e����)�@�V�K�쐬</para>
    ''' </history>
    Dim user_info As New LoginUserInfo

    '���[�U�[�Ǘ��Ɖ�o�^
    Private KanrisyaMenuInquiryInputBL As New KanrisyaMenuInquiryInputLogic
    '�c�Ə�񌟍�BL
    Private EigyouJyouhouInquiryBL As New EigyouJyouhouInquiryLogic

    Private commonCheck As New CommonCheck

    ''' <summary>�y�[�W���b�h</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '���O�C�����[�U�[���擾����B
        Dim ninsyou As New Ninsyou()
        Dim jBn As New Jiban '�n�Չ�ʋ��ʃN���X

        '���O�C�����[�U�[ID���擾����B
        ViewState("userId") = ninsyou.GetUserID()
        ' ���[�U�[��{�F��
        jBn.userAuth(user_info)

        If ViewState("userId") = "" Then
            Context.Items("strFailureMsg") = Messages.Instance.MSG2024E '�i"�Y�����[�U�[������܂���B"�j
            Server.Transfer("CommonErr.aspx")
        End If

        'Javascript
        MakeJavascript()
        If Not IsPostBack Then
            '�Q�Ɨ����Ǘ��e�[�u����o�^����B
            commonCheck.SetURL(Me, ViewState("userId"))
            '�Ɩ��敪��ݒ肷��B
            setGyoumuKubun()
            '�g�D���x����ݒ肷��B
            setSosikiLabel()
        Else
            CloseCover()
            '�{�^���敪(����:0;�o�^:1)
            If Me.hidbtnKbn.Value = "0" Then
                '��ʕ\�����ڂ�ݒ肷��B
                setKoumoku()
                Me.hidbtnKbn.Value = ""
            End If
        End If

        '�o�^�{�^���̎g�p���f
        setTourokuButton()
        Me.btnSearch.Attributes.Add("onclick", "return clearCheck();")
        Me.btnTouroku.Attributes.Add("onClick", "fncShowModal();")

    End Sub

    ''' <summary>�Ɩ��敪��ݒ肷��</summary>
    Private Sub setGyoumuKubun()
        'EMAB��Q�Ή����̊i�[����
        UnTrappedExceptionManager.AddMethodEntrance(MyClass.GetType.FullName & "." & MethodBase.GetCurrentMethod.Name)

        Dim dtGyoumuKubun As KanrisyaMenuInquiryInputDataSet.gyoumuKubunDataTable

        dtGyoumuKubun = KanrisyaMenuInquiryInputBL.GetGyoumuKubunInfo()
        Me.ddlGyoumuCode.Items.Clear()
        Me.ddlGyoumuCode.Items.Insert(0, New ListItem(String.Empty, String.Empty))

        For i As Integer = 0 To dtGyoumuKubun.Rows.Count - 1
            Dim ddlist As New ListItem
            ddlist = New ListItem
            ddlist.Text = dtGyoumuKubun.Rows(i).Item(0).ToString & "�F" & dtGyoumuKubun.Rows(i).Item(1).ToString
            ddlist.Value = dtGyoumuKubun.Rows(i).Item(0).ToString
            ddlGyoumuCode.Items.Add(ddlist)
        Next

    End Sub

    ''' <summary>�g�D���x����ݒ肷��</summary>
    Private Sub setSosikiLabel()
        'EMAB��Q�Ή����̊i�[����
        UnTrappedExceptionManager.AddMethodEntrance(MyClass.GetType.FullName & "." & MethodBase.GetCurrentMethod.Name)

        Dim dtSosikiLevel As EigyouJyouhouDataSet.sosikiLabelDataTable

        dtSosikiLevel = EigyouJyouhouInquiryBL.GetSosikiLabelInfo()
        Me.ddlLevel.Items.Clear()
        Me.ddlLevel.Items.Insert(0, New ListItem(String.Empty, String.Empty))

        For i As Integer = 0 To dtSosikiLevel.Rows.Count - 1
            Dim ddlist As New ListItem
            ddlist = New ListItem
            ddlist.Text = dtSosikiLevel.Rows(i).Item(0).ToString & "�F" & dtSosikiLevel.Rows(i).Item(1).ToString
            ddlist.Value = dtSosikiLevel.Rows(i).Item(0).ToString
            ddlLevel.Items.Add(ddlist)
        Next

    End Sub

    ''' <summary>�g�D���x���͕ύX���̏���</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ddlLevel_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlLevel.SelectedIndexChanged
        If Me.ddlLevel.SelectedIndex = 0 Then
            Me.ddlBusyo.Items.Clear()
        Else
            setBusyo(Me.ddlLevel.SelectedValue.ToString)
        End If
    End Sub

    ''' <summary>�����R�[�h��ݒ肷��B</summary>
    ''' <param name="strSosikiCd">�I�����ꂽ�g�D���x���R�[�h</param>
    Private Sub setBusyo(ByVal strSosikiCd As String)

        'EMAB��Q�Ή����̊i�[����
        UnTrappedExceptionManager.AddMethodEntrance(MyClass.GetType.FullName & "." & MethodBase.GetCurrentMethod.Name)

        Dim dtBusyoCd As EigyouJyouhouDataSet.busyoCdDataTable

        dtBusyoCd = EigyouJyouhouInquiryBL.GetbusyoCdInfo(strSosikiCd)
        Me.ddlBusyo.Items.Clear()
        Me.ddlBusyo.Items.Insert(0, New ListItem(String.Empty, String.Empty))

        For i As Integer = 0 To dtBusyoCd.Rows.Count - 1
            Dim ddlist As New ListItem
            ddlist = New ListItem
            ddlist.Text = dtBusyoCd.Rows(i).Item(0).ToString & "�F" & dtBusyoCd.Rows(i).Item(1).ToString
            ddlist.Value = dtBusyoCd.Rows(i).Item(0).ToString
            ddlBusyo.Items.Add(ddlist)
        Next

    End Sub

    ''' <summary>�o�^�{�^���̉p�s�p��ݒ肷��B</summary>
    Private Sub setTourokuButton()

        Dim dtUserKengenflg As KanrisyaMenuInquiryInputDataSet.userKengenKanriFlgDataTable
        Dim strFlg As String
        dtUserKengenflg = KanrisyaMenuInquiryInputBL.GetUserKengenKanriFlg(ViewState("userId"))
        If dtUserKengenflg.Rows.Count > 0 Then
            strFlg = dtUserKengenflg.Rows(0).Item(0).ToString

            If strFlg = "1" Then
                '����������B
                Me.btnTouroku.Visible = True
            Else
                '�������Ȃ��B
                Me.btnTouroku.Visible = False
            End If
        Else
            '�������Ȃ��B
            Me.btnTouroku.Visible = False
        End If

    End Sub

    ''' <summary>Javascript</summary>
    Private Sub MakeJavascript()
        Dim sbScript As New StringBuilder
        Dim strPraram(0) As String
        With sbScript
            .AppendLine("<script language='javascript' type='text/javascript'>")
            '���[�U�[ID����������B
            .AppendLine("   function fncUserSearch(){")
            .AppendLine("       var strkbn='���[�U�[';")
            .AppendLine("objSrchWin = window.open('search_common.aspx?show=True&Kbn='+escape(strkbn)+'&FormName=" & Form.Name & "&objCd=" & Me.tbxUserCd.ClientID & "&objMei=" & Me.tbxSiMei.ClientID & "&objBtnKbn=" & Me.hidbtnKbn.ClientID & "&strCd='+escape(eval('document.all.'+'" & Me.tbxUserCd.ClientID & "').value)+'&strMei='+escape(eval('document.all.'+'" & Me.tbxSiMei.ClientID & "').value)+'&blnDelete=True&submit=true', 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');" & vbCrLf)
            .AppendLine("   }")

            .AppendLine("   function fncShowModal(){")
            .AppendLine("      var buyDiv=document.getElementById('" & Me.buySelName.ClientID & "');")
            .AppendLine("      var disable=document.getElementById('" & Me.disableDiv.ClientID & "');")
            .AppendLine("      if(buyDiv.style.display=='none')")
            .AppendLine("      {")
            .AppendLine("       buyDiv.style.display='';")
            .AppendLine("       disable.style.display='';")
            .AppendLine("       disable.focus();")
            .AppendLine("      }else{")
            .AppendLine("       buyDiv.style.display='none';")
            .AppendLine("       disable.style.display='none';")
            .AppendLine("      }")
            .AppendLine("   }")
            .AppendLine("   function fncClosecover(){")
            .AppendLine("       var buyDiv=document.getElementById('" & Me.buySelName.ClientID & "');")
            .AppendLine("       var disable=document.getElementById('" & Me.disableDiv.ClientID & "');")
            .AppendLine("       buyDiv.style.display='none';")
            .AppendLine("       disable.style.display='none';")
            .AppendLine("   }")
            .AppendLine("</script>")
        End With
        Page.ClientScript.RegisterStartupScript(Page.GetType, "InputCheck", sbScript.ToString)

    End Sub

    ''' <summary>��ʊe���ڂ�ݒ肷��B</summary>
    Private Sub setKoumoku()
        Dim csScript As New StringBuilder
        Dim strUserId As String
        strUserId = Me.tbxUserCd.Text.ToString

        '���������ύX�����擾����B
        Dim dtDate As KanrisyaMenuInquiryInputDataSet.syozokuHenkouDateDataTable
        dtDate = KanrisyaMenuInquiryInputBL.GetSyozokuHenkouDateInfo(strUserId)
        '���[�U�[�̌��������擾����B
        Dim dtUserInfo As KanrisyaMenuInquiryInputDataSet.kanrisyaJyouhouDataTable
        '�g�D���x���f�[�^�e�[�u��
        Dim dtSosikiLevel As KanrisyaMenuInquiryInputDataSet.sansyouLevelDataTable

        If dtDate.Rows.Count > 0 Then
            dtUserInfo = KanrisyaMenuInquiryInputBL.GetUserInfo(strUserId, dtDate.Rows(0).Item(1).ToString)
            If dtUserInfo.Rows.Count > 0 Then

                Me.lblLogonId.Text = dtUserInfo.Rows(0).Item("login_user_id").ToString
                '����
                Me.tbxSiMei.Text = dtUserInfo.Rows(0).Item("DisplayName").ToString
                '������
                If dtUserInfo.Rows(0).Item("busyo_mei").ToString <> "" Then
                    Me.lblSyozokuBusyo.Text = dtUserInfo.Rows(0).Item("busyo_mei").ToString
                Else
                    Me.lblSyozokuBusyo.Text = "&nbsp;"
                End If
                '�g�D��
                If dtUserInfo.Rows(0).Item("sosikiMei").ToString <> "" Then
                    Me.lblSosikiLevel.Text = dtUserInfo.Rows(0).Item("sosikiMei").ToString
                Else
                    Me.lblYakusyoku.Text = "&nbsp;"
                End If
                '��E
                If dtUserInfo.Rows(0).Item("yakusyoku").ToString <> "" Then
                    Me.lblYakusyoku.Text = dtUserInfo.Rows(0).Item("yakusyoku").ToString
                Else
                    Me.lblYakusyoku.Text = "&nbsp;"
                End If
                '�Q�ƌ����Ǘ��t���O
                If dtUserInfo.Rows(0).Item("sansyou_kengen_kanri_flg").ToString = "1" Then
                    Me.lblSansyouKengen.Text = "����"
                Else
                    Me.lblSansyouKengen.Text = "�Ȃ�"
                End If
                '�V�l�敪
                Me.ddlSinjinKubun.SelectedValue = dtUserInfo.Rows(0).Item("eigyou_man_kbn").ToString
                '�Ɩ��敪
                If dtUserInfo.Rows(0).Item("gyoumu_kbn").ToString <> "" Then
                    Me.ddlGyoumuCode.SelectedValue = dtUserInfo.Rows(0).Item("gyoumu_kbn").ToString
                Else
                    Me.ddlGyoumuCode.SelectedIndex = 0
                End If
                '�g�D���x���A�����R�[�h
                If dtUserInfo.Rows(0).Item("t_sansyou_busyo_cd").ToString <> "" Then
                    dtSosikiLevel = KanrisyaMenuInquiryInputBL.GetLevelInfo(dtUserInfo.Rows(0).Item("t_sansyou_busyo_cd").ToString)
                    If dtSosikiLevel.Rows.Count > 0 Then
                        Me.ddlLevel.SelectedValue = dtSosikiLevel.Rows(0).Item(0).ToString
                        setBusyo(dtSosikiLevel.Rows(0).Item(0).ToString)
                        Me.ddlBusyo.SelectedValue = dtUserInfo.Rows(0).Item("t_sansyou_busyo_cd").ToString
                    Else
                        Me.ddlLevel.SelectedIndex = 0
                        Me.ddlBusyo.Items.Clear()
                    End If
                Else
                    Me.ddlLevel.SelectedIndex = 0
                    Me.ddlBusyo.Items.Clear()
                End If
                '�˗��Ɩ�
                If dtUserInfo.Rows(0).Item("irai_gyoumu_kengen").ToString = "-1" Then
                    Me.lblIraiGyoumu.Text = "��"
                Else
                    Me.lblIraiGyoumu.Text = "&nbsp;"
                End If
                '�V�K����
                If dtUserInfo.Rows(0).Item("sinki_nyuuryoku_kengen").ToString = "-1" Then
                    Me.lblSinkiNyuryoku.Text = "��"
                Else
                    Me.lblSinkiNyuryoku.Text = "&nbsp;"
                End If
                '�ް��j��
                If dtUserInfo.Rows(0).Item("data_haki_kengen").ToString = "-1" Then
                    Me.lblDataHaki.Text = "��"
                Else
                    Me.lblDataHaki.Text = "&nbsp;"
                End If
                '���ʋƖ�
                If dtUserInfo.Rows(0).Item("kekka_gyoumu_kengen").ToString = "-1" Then
                    Me.lblKekkagyoumu.Text = "��"
                Else
                    Me.lblKekkagyoumu.Text = "&nbsp;"
                End If
                '�ۏ؋Ɩ�
                If dtUserInfo.Rows(0).Item("hosyou_gyoumu_kengen").ToString = "-1" Then
                    Me.lblHosyouGyoumu.Text = "��"
                Else
                    Me.lblHosyouGyoumu.Text = "&nbsp;"
                End If
                '�񍐏��Ɩ�
                If dtUserInfo.Rows(0).Item("hkks_gyoumu_kengen").ToString = "-1" Then
                    Me.lblHokosyoGyoumu.Text = "��"
                Else
                    Me.lblHokosyoGyoumu.Text = "&nbsp;"
                End If
                '�H���Ɩ�
                If dtUserInfo.Rows(0).Item("koj_gyoumu_kengen").ToString = "-1" Then
                    Me.lblKouji.Text = "��"
                Else
                    Me.lblKouji.Text = "&nbsp;"
                End If
                '�o���Ɩ�
                If dtUserInfo.Rows(0).Item("keiri_gyoumu_kengen").ToString = "-1" Then
                    Me.lblKeiriGyoumu.Text = "��"
                Else
                    Me.lblKeiriGyoumu.Text = "&nbsp;"
                End If
                '�̑�����
                If dtUserInfo.Rows(0).Item("hansoku_uri_kengen").ToString = "-1" Then
                    Me.lblHansokuUriage.Text = "��"
                Else
                    Me.lblHansokuUriage.Text = "&nbsp;"
                End If
                '�������Ǘ�
                If dtUserInfo.Rows(0).Item("hattyuusyo_kanri_kengen").ToString = "-1" Then
                    Me.lblHattyusyoKanri.Text = "��"
                Else
                    Me.lblHattyusyoKanri.Text = "&nbsp;"
                End If
                '���Ͻ�
                If dtUserInfo.Rows(0).Item("kaiseki_master_kanri_kengen").ToString = "-1" Then
                    Me.lblKaisekiMaster.Text = "��"
                Else
                    Me.lblKaisekiMaster.Text = "&nbsp;"
                End If
                '�c��Ͻ�
                If dtUserInfo.Rows(0).Item("eigyou_master_kanri_kengen").ToString = "-1" Then
                    Me.lblEigyouMaster.Text = "��"
                Else
                    Me.lblEigyouMaster.Text = "&nbsp;"
                End If
                '���iϽ�
                If dtUserInfo.Rows(0).Item("kkk_master_kanri_kengen").ToString = "-1" Then
                    Me.lblKakakuMaster.Text = "��"
                Else
                    Me.lblKakakuMaster.Text = "&nbsp;"
                End If
                '���ъǗ���
                If dtUserInfo.Rows(0).Item("system_kanrisya_kengen").ToString = "-1" Then
                    Me.lblSystemKanrisya.Text = "��"
                Else
                    Me.lblSystemKanrisya.Text = "&nbsp;"
                End If
                hidHaita.Value = Replace(dtUserInfo.Rows(0).Item("upd_datetime"), "1900/01/01 0:00:00", "")
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", csScript.Append("alert('" & Messages.Instance.MSG020E & "');").ToString, True)
            End If
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", csScript.Append("alert('" & Messages.Instance.MSG020E & "');").ToString, True)
        End If

    End Sub

    ''' <summary>�o�^�{�^����������</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnTouroku_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTouroku.Click

        '���̓`�F�b�N
        Dim strErrMessage As String = CheckInput.ToString
        If strErrMessage <> String.Empty Then
            ShowMessage(strErrMessage)
            Exit Sub
        End If

        If Me.ddlLevel.SelectedValue <> String.Empty Then
            If Me.ddlBusyo.SelectedValue = String.Empty Then
                ShowMessage("�����R�[�h��I�����Ă��������B")
                Me.ddlBusyo.Focus()
                Exit Sub
            End If
        End If

        '�{�^���敪
        Me.hidbtnKbn.Value = "1"

        '��ʏC�����ڂ��p�����[�^�ɐݒ�B
        Dim dtParam As New KanrisyaMenuInquiryInputDataSet.updJibanNinsyouBusyoDataTable
        Dim drParam As KanrisyaMenuInquiryInputDataSet.updJibanNinsyouBusyoRow
        drParam = dtParam.NewupdJibanNinsyouBusyoRow

        With drParam
            .gyoumu_kbn = Me.ddlGyoumuCode.SelectedItem.Value
            .eigyou_man_kbn = Me.ddlSinjinKubun.SelectedValue
            .sosiki_level = Me.ddlLevel.SelectedValue
            .busyo_cd = Me.ddlBusyo.SelectedValue
            .upd_login_user_id = ViewState("userId")
            .user_id = Me.tbxUserCd.Text
            .renkei_siji_cd = 2
            .sousin_jyky_cd = 0
        End With

        dtParam.AddupdJibanNinsyouBusyoRow(drParam)

        '�n�ՔF�؁A�����Ǘ��}�X�^���X�V����B
        Dim strReturn As String
        strReturn = KanrisyaMenuInquiryInputBL.SetUpdJibanNinsyou(dtParam, hidHaita.Value)

        Dim csScript As New StringBuilder
        If strReturn = "H" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", csScript.Append("alert('" & Messages.Instance.MSG2023E & "');").ToString, True)
        ElseIf strReturn = "1" Then
            csScript.Append("alert('" & Messages.Instance.MSG018S.Replace("@PARAM1", "���[�U�[�Ǘ��Ɖ�o�^") & "');window.document.forms[0].submit();")
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", csScript.ToString, True)
            setKoumoku()
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", csScript.Append("alert('" & String.Format(Messages.Instance.MSG003E, Split(strReturn, ",")(0), Split(strReturn, ",")(1)) & "');").ToString, True)
        End If

    End Sub

    ''' <summary>���̓`�F�b�N</summary>
    ''' <returns>StringBuilder</returns>
    Public Function CheckInput() As StringBuilder
        Dim csScript As New StringBuilder
        With csScript
            '�K�{���̓`�F�b�N
            .Append(commonCheck.CheckHissuNyuuryoku(Me.tbxUserCd.Text, "���[�U�[�h�c"))
            Me.tbxUserCd.Focus()
            '���p�p���`�F�b�N
            If Me.tbxUserCd.Text <> String.Empty Then
                .Append(commonCheck.ChkHankakuEisuuji(Me.tbxUserCd.Text, "���[�U�[�h�c"))
                Me.tbxUserCd.Focus()
            End If
        End With
        Return csScript

    End Function

    ''' <summary> ���b�Z�[�W���|�b�v�A�b�v����B</summary>
    ''' <param name="strMessage">���b�Z�[�W</param>
    Private Sub ShowMessage(ByVal strMessage As String)
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("function window.onload() {")
            .AppendLine("	alert('" & strMessage & "');")
            .AppendLine("}")
        End With
        '�y�[�W�����ŁA�N���C�A���g���̃X�N���v�g �u���b�N���o�͂��܂�
        ClientScript.RegisterStartupScript(Me.GetType(), "ShowMessageFocus", csScript.ToString, True)
    End Sub

    ''' <summary>�߂�{�^�����N���b�N��</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click

        Server.Transfer("MasterMainteMenu.aspx")

    End Sub

    ''' <summary>DIV�\��</summary>
    Public Sub CloseCover()
        Dim csScript As New StringBuilder
        csScript.AppendFormat("fncClosecover();").ToString()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CloseCover", csScript.ToString, True)
    End Sub

End Class
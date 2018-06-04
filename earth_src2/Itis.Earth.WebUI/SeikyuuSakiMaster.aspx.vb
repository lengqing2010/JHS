Imports Itis.Earth.BizLogic
Imports System.Data

Partial Public Class SeikyuuSakiMaster
    Inherits System.Web.UI.Page
    '�{�^��
    Private blnBtn As Boolean
    '�C���X�^���X����
    Private CommonSearchLogic As New Itis.Earth.BizLogic.CommonSearchLogic
    '���ʃ`�F�b�N
    Private commoncheck As New CommonCheck
    '�C���X�^���X����
    Private SeikyuuSakiMasterBL As New Itis.Earth.BizLogic.SeikyuuSakiMasterLogic

    Private Const SEP_STRING As String = "$$$"

    ''' <summary>
    ''' �y�[�W���[�h
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '�����`�F�b�N��
        Dim strNinsyou As String = ""
        Dim commonChk As New CommonCheck
        Dim strUserID As String = ""
        '�����`�F�b�N����ѐݒ�
        blnBtn = commonChk.CommonNinnsyou(strUserID, "keiri_gyoumu_kengen")
        ViewState("UserId") = strUserID

        If Not IsPostBack Then

            'DDL�̏����ݒ�
            SetDdlListInf()

            ''������Ѓ}�X�^��ʂ���A
            If Request("sendSearchTerms") <> "" Then
                Dim arrSearchTerm() As String = Split(Request("sendSearchTerms"), SEP_STRING)
                tbxSeikyuuSaki_Cd.Text = arrSearchTerm(0)                      '�e��ʂ���POST���ꂽ���1 �F������R�[�h
                tbxSeikyuuSaki_Brc.Text = arrSearchTerm(1)                     '�e��ʂ���POST���ꂽ���1 �F������R�[�h
                SetDropSelect(ddlSeikyuuSaki_Kbn, arrSearchTerm(2))             '�e��ʂ���POST���ꂽ���1 �F������R�[�h

                If arrSearchTerm.Length = 4 Then
                    '�������ނ̃Z�b�g
                    If arrSearchTerm(3) = "1" Then
                        Me.lblTyousaKoujiKbn.Text = "������������"
                    ElseIf arrSearchTerm(3) = "2" Then
                        Me.lblTyousaKoujiKbn.Text = "���H��������"
                    End If
                End If

                '���׃f�[�^�擾
                GetMeisaiData(tbxSeikyuuSaki_Cd.Text, tbxSeikyuuSaki_Brc.Text, ddlSeikyuuSaki_Kbn.SelectedValue, "btnSearch")

                Me.btnBack.Text = "����"
                ViewState.Item("Flg") = "close"
            Else
                '�C���{�^��
                btnSyuusei.Enabled = False
                '�o�^�{�^��
                btnTouroku.Enabled = True
                Me.btnKensakuSeikyuuSaki.Enabled = True

                Me.btnBack.Text = "�߂�"
                ViewState.Item("Flg") = "back"
            End If
        Else
            If hidConfirm.Value = "OK" Then
                '������o�^���`�}�X�^�ݒ�
                SetSeikyuuSakiHinagata()
            End If
        End If

        '������E�z
        Me.tbxKaisyuuKyoukaigaku.Attributes.Add("onblur", "checkNumberAddFig(this);")
        Me.tbxKaisyuuKyoukaigaku.Attributes.Add("onfocus", "removeFig(this);")

        '�����於
        tbxSeikyuuSakiMei.Attributes.Add("readonly", "true;")
        '�����於
        tbxSeikyuuSaki_Mei.Attributes.Add("readonly", "true;")
        '�V��v���Ə�
        tbxSkkJigyousyoMei.Attributes.Add("readonly", "true;")

        '����於
        tbxNayoseMei.Attributes.Add("readonly", "true;")

        '���׃N���A
        btnClearMeisai.Attributes.Add("onclick", "if(!confirm('�N���A���s�Ȃ��܂��B\n��낵���ł����H')){return false;};disableButton1();")

        'disableButton
        btnSearchSeikyuuSaki.Attributes.Add("onclick", "disableButton1();")
        btnKihonJyouhouSet.Attributes.Add("onclick", "disableButton1();")
        btnSearch.Attributes.Add("onclick", "disableButton1();")
        btnClear.Attributes.Add("onclick", "disableButton1();")
        btnSyuusei.Attributes.Add("onclick", "disableButton1();")
        btnTouroku.Attributes.Add("onclick", "disableButton1();")
        btnKensakuSeikyuuSaki.Attributes.Add("onclick", "disableButton1();")
        btnSkkJigyousyo.Attributes.Add("onclick", "disableButton1();")
        btnKensakuYuubinNo.Attributes.Add("onclick", "disableButton1();")
        '20100925�@�����R�[�h�A����於�@�ǉ��@�n���R������
        btnKensakuNayose.Attributes.Add("onclick", "disableButton1();")
        '20100925�@�����R�[�h�A����於�@�ǉ��@�n���R������
        '�X�֔ԍ�
        Me.tbxYuubinNo.Attributes.Add("onblur", "SetYuubinNo(this)")

        '==================2011/06/16 �ԗ� ����ӎ�����{�^���ǉ��̑Ή� �ǉ� �J�n��========================== 
        '����ӏ��{�^��
        Me.btnTyuuijikou.Attributes.Add("onClick", "fncTyuuijikouPopup();return false;")

        '==================2011/06/16 �ԗ� ����ӎ�����{�^���ǉ��̑Ή� �ǉ� �I����========================== 

        '===========2012/05/15 �ԗ� 407553�̑Ή� �ǉ���========================
        '�u�̔ԁv�{�^�����Z�b�g����
        Call Me.SetBtnSaiban()
        '������v���Ӑ溰��
        Me.tbxTougouKaikeiTokusakiCd.Attributes.Add("onPropertyChange", "fncSetBtnSaiban();")
        '===========2012/05/15 �ԗ� 407553�̑Ή� �ǉ���========================

        MakeScript()
        If Not blnBtn Then
            btnSyuusei.Enabled = False
            btnTouroku.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' �i���ҏW�{�^������������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim strErr As String = ""
        Dim strID As String = ""

        '������敪
        If strErr = "" Then
            '���͕K�{
            strErr = commoncheck.CheckHissuNyuuryoku(ddlSeikyuuSaki_Kbn.SelectedValue, "������敪")
            strID = ddlSeikyuuSaki_Kbn.ClientID
        End If

        '������R�[�h
        If strErr = "" Then
            '���͕K�{
            strErr = commoncheck.CheckHissuNyuuryoku(tbxSeikyuuSaki_Cd.Text, "������R�[�h")
            strID = tbxSeikyuuSaki_Cd.ClientID
        End If
        If strErr = "" Then
            '���p�p����
            strErr = commoncheck.ChkHankakuEisuuji(tbxSeikyuuSaki_Cd.Text, "������R�[�h")
            strID = tbxSeikyuuSaki_Cd.ClientID
        End If

        '������}��
        If strErr = "" Then
            '���͕K�{
            strErr = commoncheck.CheckHissuNyuuryoku(tbxSeikyuuSaki_Brc.Text, "������}��")
            strID = tbxSeikyuuSaki_Brc.ClientID
        End If
        If strErr = "" Then
            '���p�p����
            strErr = commoncheck.ChkHankakuEisuuji(tbxSeikyuuSaki_Brc.Text, "������}��")
            strID = tbxSeikyuuSaki_Brc.ClientID
        End If

        If strErr <> "" Then
            strErr = "alert('" & strErr & "');document.getElementById('" & strID & "').focus();"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strErr, True)
        Else
            '���׃f�[�^�擾
            GetMeisaiData(tbxSeikyuuSaki_Cd.Text, tbxSeikyuuSaki_Brc.Text, ddlSeikyuuSaki_Kbn.SelectedValue, "btnSearch")
        End If
    End Sub

    ''' <summary>
    ''' �o�^�{�^������������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnTouroku_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTouroku.Click
        Dim strErr As String = ""
        Dim strID As String = ""
        strID = InputCheck(strErr)

        '�V��v���Ə��R�[�h���݃`�F�b�N
        If strErr = "" And Trim(tbxSkkJigyousyoCd.Text) <> "" Then
            Dim dtTable As New Data.DataTable
            dtTable = SeikyuuSakiMasterBL.SelSinkaikeiJigyousyoInfo(Trim(tbxSkkJigyousyoCd.Text))
            If dtTable.Rows.Count <> 1 Then
                strErr = String.Format(Messages.Instance.MSG2036E, "�V��v���Ə��}�X�^").ToString
                strID = tbxSkkJigyousyoCd.ClientID
                tbxSkkJigyousyoMei.Text = ""
            End If
        End If

        '�X�֔ԍ����݃`�F�b�N
        If strErr = "" And Trim(tbxYuubinNo.Text) <> "" Then
            Dim dtTable As New Data.DataTable
            dtTable = SeikyuuSakiMasterBL.SelYuubinInfo(Trim(tbxYuubinNo.Text.Replace("-", "")))
            If dtTable.Rows.Count <> 1 Then
                strErr = String.Format(Messages.Instance.MSG2036E, "�X�֔ԍ��}�X�^").ToString
                strID = tbxYuubinNo.ClientID
            End If
        End If

        '�G���[�����鎞
        If strErr <> "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('" & strErr & "');document.getElementById('" & strID & "').focus();", True)
        Else
            '�d���`�F�b�N
            Dim dtSeikyuuSakiDataSet As New Itis.Earth.DataAccess.SeikyuuSakiDataSet.m_seikyuu_sakiDataTable
            dtSeikyuuSakiDataSet = SeikyuuSakiMasterBL.SelSeikyuuSakiInfo(tbxSeikyuuSakiCd.Text, tbxSeikyuuSakiBrc.Text, ddlSeikyuuSakiKbn.SelectedValue)
            If dtSeikyuuSakiDataSet.Rows.Count <> 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('�}�X�^�[�ɏd���f�[�^�����݂��܂��B');document.getElementById('" & ddlSeikyuuSakiKbn.ClientID & "').focus();", True)
                Return
            End If

            '�e�}�X�^�̑��݃`�F�b�N
            If Me.ddlSeikyuuSakiKbn.Text = "0" Or Me.ddlSeikyuuSakiKbn.Text = "1" Or Me.ddlSeikyuuSakiKbn.Text = "2" Then
                If SeikyuuSakiMasterBL.SelSonzaiChk(Me.ddlSeikyuuSakiKbn.Text, Me.tbxSeikyuuSakiCd.Text, Me.tbxSeikyuuSakiBrc.Text).Rows.Count < 1 Then
                    Select Case Me.ddlSeikyuuSakiKbn.Text
                        Case "0"
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('�����X�}�X�^�ɑ��݂��Ȃ��R�[�h���ݒ肳�ꂢ�Ă��܂��B');document.getElementById('" & tbxSeikyuuSakiCd.ClientID & "').focus();", True)
                            Return
                        Case "1"
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('������Ѓ}�X�^�ɑ��݂��Ȃ��R�[�h���ݒ肳�ꂢ�Ă��܂��B');document.getElementById('" & tbxSeikyuuSakiCd.ClientID & "').focus();", True)
                            Return
                        Case "2"
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('�c�Ə��}�X�^�ɑ��݂��Ȃ��R�[�h���ݒ肳�ꂢ�Ă��܂��B');document.getElementById('" & tbxSeikyuuSakiCd.ClientID & "').focus();", True)
                            Return
                        Case Else
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('�s���Ȑ�����敪���I������Ă��܂��B');document.getElementById('" & tbxSeikyuuSakiCd.ClientID & "').focus();", True)
                            Return
                    End Select
                End If
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('�s���Ȑ�����敪���I������Ă��܂��B');document.getElementById('" & ddlSeikyuuSakiKbn.ClientID & "').focus();", True)
                Return
            End If

            '�f�[�^�o�^
            If SeikyuuSakiMasterBL.InsSeikyuuSaki(SetMeisaiData) Then
                strErr = "alert('" & Replace(Messages.Instance.MSG018S, "@PARAM1", "������}�X�^") & "');"
                '�Ď擾
                GetMeisaiData(tbxSeikyuuSakiCd.Text, tbxSeikyuuSakiBrc.Text, ddlSeikyuuSakiKbn.SelectedValue, "btnTouroku")
            Else
                strErr = "alert('" & Replace(Messages.Instance.MSG019E, "@PARAM1", "������}�X�^") & "');"
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strErr, True)
        End If
    End Sub

    ''' <summary>
    ''' �C���{�^������������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSyuusei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSyuusei.Click
        Dim strReturn As String = ""
        Dim strErr As String = ""
        Dim strID As String = ""

        '�`�F�b�N
        strID = InputCheck(strErr)

        '�V��v���Ə��R�[�h���݃`�F�b�N
        If strErr = "" And Trim(tbxSkkJigyousyoCd.Text) <> "" Then
            Dim dtTable As New Data.DataTable
            dtTable = SeikyuuSakiMasterBL.SelSinkaikeiJigyousyoInfo(Trim(tbxSkkJigyousyoCd.Text))
            If dtTable.Rows.Count <> 1 Then
                strErr = String.Format(Messages.Instance.MSG2036E, "�V��v���Ə��}�X�^").ToString
                strID = tbxSkkJigyousyoCd.ClientID
                tbxSkkJigyousyoMei.Text = ""
            End If
        End If

        '�X�֔ԍ����݃`�F�b�N
        If strErr = "" And Trim(tbxYuubinNo.Text) <> "" Then
            Dim dtTable As New Data.DataTable
            dtTable = SeikyuuSakiMasterBL.SelYuubinInfo(Trim(tbxYuubinNo.Text.Replace("-", "")))
            If dtTable.Rows.Count <> 1 Then
                strErr = String.Format(Messages.Instance.MSG2036E, "�X�֔ԍ��}�X�^").ToString
                strID = tbxYuubinNo.ClientID
            End If
        End If

        '�G���[�����鎞
        If strErr <> "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('" & strErr & "');document.getElementById('" & strID & "').focus();", True)
        Else
            '�X�V����
            strReturn = SeikyuuSakiMasterBL.UpdSeikyuuSaki(SetMeisaiData)
            If strReturn = "0" Then
                '�X�V����
                strErr = "alert('" & Replace(Messages.Instance.MSG018S, "@PARAM1", "������}�X�^") & "');"
                '��ʍĕ`�揈��
                GetMeisaiData(tbxSeikyuuSakiCd.Text, tbxSeikyuuSakiBrc.Text, ddlSeikyuuSakiKbn.SelectedValue, "btnSyuusei")
            ElseIf strReturn = "1" Then
                '�X�V���s
                strErr = "alert('" & Replace(Messages.Instance.MSG019E, "@PARAM1", "������}�X�^") & "');"
            ElseIf strReturn = "2" Then
                '���݃`�F�b�N
                strErr = "alert('�Y���f�[�^�����݂��܂���B���ɍ폜����Ă���\��������܂��B');"
            Else
                '���̑�
                strErr = "alert('" & strReturn & "');"
            End If
            '���b�Z�[�W�\��
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strErr, True)
        End If

    End Sub

    ''' <summary>
    ''' �X�֔ԍ�.�����{�^������������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnKensakuYuubinNo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKensakuYuubinNo.Click
        '�Z��
        Dim data As DataSet
        Dim jyuusyo As String
        Dim jyuusyoMei As String
        Dim jyuusyoNo As String

        '�Z���擾
        Dim csScript As New StringBuilder

        data = (SeikyuuSakiMasterBL.GetMailAddress(Me.tbxYuubinNo.Text.Replace("-", String.Empty).Trim))
        If data.Tables(0).Rows.Count = 1 Then

            jyuusyo = data.Tables(0).Rows(0).Item(0).ToString
            jyuusyoNo = jyuusyo.Split(",")(0)
            jyuusyoMei = GetJyusho(jyuusyo.Split(",")(1))
            If jyuusyoNo.Length > 3 Then
                jyuusyoNo = jyuusyoNo.Substring(0, 3) & "-" & jyuusyoNo.Substring(3, jyuusyoNo.Length - 3)
            End If

            csScript.AppendLine("if(document.getElementById('" & Me.tbxJyuusyo1.ClientID & "').value!='' || document.getElementById('" & Me.tbxJyuusyo2.ClientID & "').value!=''){" & vbCrLf)
            csScript.AppendLine("if (!confirm('�����f�[�^������܂����㏑�����Ă�낵���ł����B')){}else{ " & vbCrLf)

            csScript.AppendLine("document.getElementById('" & Me.tbxJyuusyo1.ClientID & "').value = '" & jyuusyoMei.Split(",")(0) & "';" & vbCrLf)
            csScript.AppendLine("document.getElementById('" & Me.tbxJyuusyo2.ClientID & "').value = '" & jyuusyoMei.Split(",")(1) & "';" & vbCrLf)
            csScript.AppendLine("document.getElementById('" & Me.tbxYuubinNo.ClientID & "').value = '" & jyuusyoNo & "';" & vbCrLf)

            csScript.AppendLine("}" & vbCrLf)

            csScript.AppendLine("}else{" & vbCrLf)
            csScript.AppendLine("document.getElementById('" & Me.tbxJyuusyo1.ClientID & "').value = '" & jyuusyoMei.Split(",")(0) & "';" & vbCrLf)
            csScript.AppendLine("document.getElementById('" & Me.tbxJyuusyo2.ClientID & "').value = '" & jyuusyoMei.Split(",")(1) & "';" & vbCrLf)
            csScript.AppendLine("document.getElementById('" & Me.tbxYuubinNo.ClientID & "').value = '" & jyuusyoNo & "';" & vbCrLf)

            csScript.AppendLine("}" & vbCrLf)
        Else
            csScript.AppendLine("fncOpenwindowYuubin('" & Me.tbxYuubinNo.ClientID & "','" & Me.tbxJyuusyo1.ClientID & "','" & Me.tbxJyuusyo2.ClientID & "');")
        End If
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "openWindowYuubin", csScript.ToString, True)
    End Sub

    ''' <summary>
    ''' �Z���P�A�Q�擾
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetJyusho(ByVal value As String) As String
        Dim i As Integer
        If value.Length > 20 Then
            For i = 20 To value.Length
                If System.Text.Encoding.Default.GetBytes(Left(value, i)).Length >= 39 Then
                    Return value.Substring(0, i) & "," & value.Substring(i, value.Length - i)
                End If
            Next
        End If
        Return value & ","
    End Function

    ''' <summary>
    ''' ��{���Z�b�g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnKihonJyouhouSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKihonJyouhouSet.Click
        Dim strErr As String = ""
        Dim strID As String = ""
        '���X�g�̑I���`�F�b�N
        If ddlSeikyuuSakiTourokuHinagata.SelectedValue = "" Then
            strErr = "��{��񂪖��I���ł��B"
        End If

        If strErr <> "" Then
            strErr = "alert('" & strErr & "');document.getElementById('" & ddlSeikyuuSakiTourokuHinagata.ClientID & "').focus();"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strErr, True)
        Else
            '�m�F���b�Z�[�W�̕\��
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "fncConfirm();", True)
        End If
    End Sub

    ''' <summary>
    ''' ������.�����{�^������������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSearchSeikyuuSaki_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearchSeikyuuSaki.Click

        Dim strScript As String = ""
        '��������擾
        Dim dtSeikyuuSakiTable As New Itis.Earth.DataAccess.CommonSearchDataSet.SeikyuuSakiTableDataTable
        'dtSeikyuuSakiTable = CommonSearchLogic.GetSeikyuuSakiInfo("2", ddlSeikyuuSaki_Kbn.SelectedValue, tbxSeikyuuSaki_Cd.Text, tbxSeikyuuSaki_Brc.Text, "", False)
        dtSeikyuuSakiTable = SeikyuuSakiMasterBL.SelVSeikyuuSakiInfo(ddlSeikyuuSaki_Kbn.SelectedValue, tbxSeikyuuSaki_Cd.Text, tbxSeikyuuSaki_Brc.Text, False)

        '�������ʂ�1���������ꍇ
        If dtSeikyuuSakiTable.Rows.Count = 1 Then
            '������R�[�h
            tbxSeikyuuSaki_Cd.Text = TrimNull(dtSeikyuuSakiTable.Item(0).seikyuu_saki_cd)
            '������}��
            tbxSeikyuuSaki_Brc.Text = TrimNull(dtSeikyuuSakiTable.Item(0).seikyuu_saki_brc)
            '������敪
            SetDropSelect(ddlSeikyuuSaki_Kbn, TrimNull(dtSeikyuuSakiTable.Item(0).seikyuu_saki_kbn))
            '�����於
            tbxSeikyuuSaki_Mei.Text = TrimNull(dtSeikyuuSakiTable.Item(0).seikyuu_saki_mei)
        Else
            '�����於
            tbxSeikyuuSaki_Mei.Text = ""
            strScript = "objSrchWin = window.open('search_Seikyuusaki.aspx?blnDelete=False&Kbn='+escape('������')+'&FormName=" & _
                                             Me.Page.Form.Name & "&objKbn=" & _
                                             ddlSeikyuuSaki_Kbn.ClientID & _
                                             "&objCd=" & tbxSeikyuuSaki_Cd.ClientID & _
                                             "&objBrc=" & tbxSeikyuuSaki_Brc.ClientID & _
                                             "&objMei=" & tbxSeikyuuSaki_Mei.ClientID & _
                                             "&strKbn='+escape(eval('document.all.'+'" & _
                                             ddlSeikyuuSaki_Kbn.ClientID & "').value)+'&strCd='+escape(eval('document.all.'+'" & _
                                             tbxSeikyuuSaki_Cd.ClientID & "').value)+'&strBrc='+escape(eval('document.all.'+'" & _
                                             tbxSeikyuuSaki_Brc.ClientID & "').value), 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strScript, True)
        End If
    End Sub

    ''' <summary>
    ''' ������.�����{�^������������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnKensakuSeikyuuSaki_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKensakuSeikyuuSaki.Click

        Dim strScript As String = ""
        '��������擾
        Dim dtSeikyuuSakiTable As New Itis.Earth.DataAccess.CommonSearchDataSet.SeikyuuSakiTableDataTable
        dtSeikyuuSakiTable = SeikyuuSakiMasterBL.SelVSeikyuuSakiInfo(ddlSeikyuuSakiKbn.SelectedValue, tbxSeikyuuSakiCd.Text, tbxSeikyuuSakiBrc.Text, False)

        '�������ʂ�1���������ꍇ
        If dtSeikyuuSakiTable.Rows.Count = 1 Then
            '������R�[�h
            tbxSeikyuuSakiCd.Text = TrimNull(dtSeikyuuSakiTable.Item(0).seikyuu_saki_cd)
            '������}��
            tbxSeikyuuSakiBrc.Text = TrimNull(dtSeikyuuSakiTable.Item(0).seikyuu_saki_brc)
            '������敪
            SetDropSelect(ddlSeikyuuSakiKbn, TrimNull(dtSeikyuuSakiTable.Item(0).seikyuu_saki_kbn))
            '�����於
            tbxSeikyuuSakiMei.Text = TrimNull(dtSeikyuuSakiTable.Item(0).seikyuu_saki_mei)

            GetMeisaiData1(tbxSeikyuuSakiCd.Text, tbxSeikyuuSakiBrc.Text, ddlSeikyuuSakiKbn.SelectedValue)
        Else
            '�����於
            tbxSeikyuuSakiMei.Text = ""
            strScript = "objSrchWin = window.open('search_Seikyuusaki.aspx?blnDelete=False&Kbn='+escape('������')+'&FormName=" & _
                                             Me.Page.Form.Name & "&objKbn=" & _
                                             ddlSeikyuuSakiKbn.ClientID & _
                                             "&objCd=" & tbxSeikyuuSakiCd.ClientID & _
                                             "&objBrc=" & tbxSeikyuuSakiBrc.ClientID & _
                                             "&objMei=" & tbxSeikyuuSakiMei.ClientID & _
                                             "&objBtn=" & btnOK.ClientID & _
                                             "&strKbn='+escape(eval('document.all.'+'" & _
                                             ddlSeikyuuSakiKbn.ClientID & "').value)+'&strCd='+escape(eval('document.all.'+'" & _
                                             tbxSeikyuuSakiCd.ClientID & "').value)+'&strBrc='+escape(eval('document.all.'+'" & _
                                             tbxSeikyuuSakiBrc.ClientID & "').value), 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strScript, True)
        End If
    End Sub

    ''' <summary>
    ''' �V��v���Ə������{�^���{�^������������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSkkJigyousyo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSkkJigyousyo.Click
        Dim strScript As String = ""
        '�f�[�^�擾SelSinkaikeiJigyousyoInfo
        Dim dtJigyousyoTable As New Data.DataTable
        dtJigyousyoTable = SeikyuuSakiMasterBL.SelSinkaikeiJigyousyoInfo(tbxSkkJigyousyoCd.Text)

        '�������ʂ�1���������ꍇ
        If dtJigyousyoTable.Rows.Count = 1 Then
            '�V��v���Ə��R�[�h
            tbxSkkJigyousyoCd.Text = TrimNull(dtJigyousyoTable.Rows(0).Item("skk_jigyousyo_cd"))
            '�V��v�x���於 
            tbxSkkJigyousyoMei.Text = TrimNull(dtJigyousyoTable.Rows(0).Item("skk_jigyousyo_mei"))
        Else
            tbxSkkJigyousyoMei.Text = ""
            strScript = "objSrchWin = window.open('search_SinkaikeiJigyousyo.aspx?Kbn='+escape('�V��v���Ə�')+'&SiharaiKbn='+escape('Siharai')+'&FormName=" & _
            Me.Page.Form.Name & "&objCd=" & _
                tbxSkkJigyousyoCd.ClientID & _
                    "&objMei=" & tbxSkkJigyousyoMei.ClientID & _
                    "&strCd='+escape(eval('document.all.'+'" & _
                    tbxSkkJigyousyoCd.ClientID & "').value)+'&strMei='+escape(eval('document.all.'+'" & _
                    tbxSkkJigyousyoMei.ClientID & "').value), 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strScript, True)
        End If
    End Sub

    ''' <summary>
    ''' ����挟���{�^���{�^������������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <history>20100925�@�����R�[�h�A����於�@�ǉ��@�n���R</history>
    Protected Sub btnKensakuNayose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKensakuNayose.Click
        Dim strScript As String = ""
        '�f�[�^�擾
        Dim dtNayoseSakiTable As New Data.DataTable
        dtNayoseSakiTable = SeikyuuSakiMasterBL.SelNayoseSakiInfo(tbxNayoseCd.Text.Trim)

        '�������ʂ�1���������ꍇ
        If dtNayoseSakiTable.Rows.Count = 1 Then
            '�����R�[�h
            tbxNayoseCd.Text = TrimNull(dtNayoseSakiTable.Rows(0).Item("nayose_saki_cd"))
            '����於 
            tbxNayoseMei.Text = TrimNull(dtNayoseSakiTable.Rows(0).Item("nayose_saki_name1"))
        Else
            tbxNayoseMei.Text = ""
            strScript = "objSrchWin = window.open('search_NayoseSaki.aspx?Kbn='+escape('�����')+'&SiharaiKbn='+escape('Siharai')+'&FormName=" & _
            Me.Page.Form.Name & "&objCd=" & _
                tbxNayoseCd.ClientID & _
                    "&objMei=" & tbxNayoseMei.ClientID & _
                    "&strCd='+escape(eval('document.all.'+'" & _
                    tbxNayoseCd.ClientID & "').value)+'&strMei='+escape(eval('document.all.'+'" & _
                    tbxNayoseMei.ClientID & "').value), 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strScript, True)
        End If
    End Sub

    ''' <summary>
    ''' ���׃N���A
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnClearMeisai_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearMeisai.Click
        MeisaiClear()
    End Sub

    ''' <summary>
    ''' �N���A
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        '������敪
        SetDropSelect(ddlSeikyuuSaki_Kbn, "")
        '������R�[�h
        tbxSeikyuuSaki_Cd.Text = ""
        '������
        tbxSeikyuuSaki_Brc.Text = ""
        '�����於
        tbxSeikyuuSaki_Mei.Text = ""
        'MeisaiClear()
    End Sub

    ''' <summary>Javascript�쐬</summary>
    Private Sub MakeScript()
        Dim csType As Type = Page.GetType()
        Dim csName As String = "GetScript"
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("<script language='javascript' type='text/javascript'>")
            '��{���Z�b�g
            .AppendLine("function fncConfirm()")
            .AppendLine("{")
            '
            .AppendLine("   var hidConfirm = document.getElementById('" & Me.hidConfirm.ClientID & "')")
            .AppendLine("   var hidSeikyuuKbn = document.getElementById('" & Me.hidSeikyuuKbn.ClientID & "')")
            .AppendLine("   var ddlSeikyuuSakiKbn = document.getElementById('" & Me.ddlSeikyuuSakiKbn.ClientID & "')")
            .AppendLine("   if(confirm('��{�����Z�b�g���܂����H')){")
            .AppendLine("       hidConfirm.value = 'OK';")
            .AppendLine("       hidSeikyuuKbn.value = ddlSeikyuuSakiKbn.value;")
            .AppendLine("       document.getElementById('" & Me.Form.Name & "').submit();")
            .AppendLine("   }")
            .AppendLine("}")

            '�X�ւ̎擾
            .AppendLine("function fncOpenwindowYuubin(id1,mei1,mei2)" & vbCrLf)
            .AppendLine("{" & vbCrLf)
            .AppendLine("var strkbn='�X��';" & vbCrLf)
            .AppendLine("objSrchWin = window.open('search_common.aspx?Kbn='+escape(strkbn)+'&FormName=" & _
            Me.Page.Form.Name & _
            "&objCd=" & _
            "'+escape(id1)+'" & _
            "&objMei=" & _
            "'+mei1+'" & _
            "&objMei2=" & _
            "'+mei2+'" & _
            "&strCd='+escape(eval('document.all.'+" & _
            " id1 +'" & "').value)" & _
            "+'&strMei='+escape(eval('document.all.'+" & _
            " mei1 " & ").innerText)" & _
            "+'&strMei2='+escape(eval('document.all.'+" & _
            " mei2 " & ").innerText)" & _
            ", 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');" & vbCrLf)
            .AppendLine("}" & vbCrLf)

            '�X�֔ԍ�
            .AppendLine("function SetYuubinNo(e)")
            .AppendLine("{")
            .AppendLine("   var val;")
            .AppendLine("   var val2;")
            .AppendLine("   val = e.value;")
            .AppendLine("   arr = val.split('-');")
            .AppendLine("   val = arr.join('');")
            .AppendLine("   if (val.length>=3){")
            .AppendLine("       val2 = val.substring(0,3) + '-' + val.substring(3,val.length);")
            .AppendLine("   }else{")
            .AppendLine("       val2 =val;")
            .AppendLine("   }")
            .AppendLine("   e.value = val2.replace(/(^\s*)|(\s*$)/g,'');")
            .AppendLine("}")

            '��{���Z�b�g
            .AppendLine("function fncDisable()")
            .AppendLine("{")
            .AppendLine("   var btnSearchSeikyuuSaki = document.getElementById('" & Me.btnSearchSeikyuuSaki.ClientID & "')")
            .AppendLine("   var btnSearch = document.getElementById('" & Me.btnSearch.ClientID & "')")
            .AppendLine("   var btnClear = document.getElementById('" & Me.btnClear.ClientID & "')")
            .AppendLine("   var btnSyuusei = document.getElementById('" & Me.btnSyuusei.ClientID & "')")
            .AppendLine("   var btnTouroku = document.getElementById('" & Me.btnTouroku.ClientID & "')")
            .AppendLine("   var btnClearMeisai = document.getElementById('" & Me.btnClearMeisai.ClientID & "')")
            .AppendLine("   var btnKensakuSeikyuuSaki = document.getElementById('" & Me.btnKensakuSeikyuuSaki.ClientID & "')")
            .AppendLine("   var btnSkkJigyousyo = document.getElementById('" & Me.btnSkkJigyousyo.ClientID & "')")
            .AppendLine("   var btnKihonJyouhouSet = document.getElementById('" & Me.btnKihonJyouhouSet.ClientID & "')")
            .AppendLine("   var btnKensakuYuubinNo = document.getElementById('" & Me.btnKensakuYuubinNo.ClientID & "')")
            '20100925�@�����R�[�h�A����於�@�ǉ��@�n���R������
            .AppendLine("   var btnKensakuNayose = document.getElementById('" & Me.btnKensakuNayose.ClientID & "')")
            '20100925�@�����R�[�h�A����於�@�ǉ��@�n���R������
            .AppendLine("   var my_array = new Array(10);")
            .AppendLine("   my_array[0] = btnSearchSeikyuuSaki;")
            .AppendLine("   my_array[1] = btnSearch;")
            .AppendLine("   my_array[2] = btnClear;")
            .AppendLine("   my_array[3] = btnSyuusei;")
            .AppendLine("   my_array[4] = btnTouroku;")
            .AppendLine("   my_array[5] = btnClearMeisai;")
            .AppendLine("   my_array[6] = btnKensakuSeikyuuSaki;")
            .AppendLine("   my_array[7] = btnSkkJigyousyo;")
            .AppendLine("   my_array[8] = btnKihonJyouhouSet;")
            .AppendLine("   my_array[9] = btnKensakuYuubinNo;")
            '20100925�@�����R�[�h�A����於�@�ǉ��@�n���R������
            .AppendLine("   my_array[10] = btnKensakuNayose;")
            '20100925�@�����R�[�h�A����於�@�ǉ��@�n���R������
            .AppendLine("   for (i = 0; i < 11; i++){")
            .AppendLine("       my_array[i].disabled = true;")
            .AppendLine("   }")
            .AppendLine("}")
            .AppendLine("function disableButton1()")
            .AppendLine("{")
            .AppendLine("   window.setTimeout('fncDisable()',0);")
            .AppendLine("   return true;")
            .AppendLine("}")

            '==================2011/06/16 �ԗ� ����ӎ�����{�^���ǉ��̑Ή� �ǉ� �J�n��========================== 
            '����ӏ��{�^��
            .AppendLine("function fncTyuuijikouPopup() ")
            .AppendLine("{ ")
            .AppendLine("	var blnTouroku = document.getElementById('" & Me.btnTouroku.ClientID & "').disabled; ")
            .AppendLine("	var blnSyuusei = document.getElementById('" & Me.btnSyuusei.ClientID & "').disabled; ")
            .AppendLine("	//�C�����[�h���i�y�C�����s�z�������j ")
            .AppendLine("	if(blnSyuusei == false) ")
            .AppendLine("	{ ")
            .AppendLine("		var strKbn = document.getElementById('" & Me.ddlSeikyuuSakiKbn.ClientID & "').value; ")
            .AppendLine("		var strCd = document.getElementById('" & Me.tbxSeikyuuSakiCd.ClientID & "').value; ")
            .AppendLine("		var strBrc = document.getElementById('" & Me.tbxSeikyuuSakiBrc.ClientID & "').value; ")
            .AppendLine("		var sendSearchTerms = strKbn + '$$$' + strCd + '$$$' + strBrc; ")
            .AppendLine("		window.open('SeikyuuSakiTyuuijikouSearchList.aspx?sendSearchTerms='+sendSearchTerms,'TyuuijikouPopup','menubar=yes,toolbar=yes,location=yes,status=yes,resizable=yes,scrollbars=yes'); ")
            .AppendLine("		return true; ")
            .AppendLine("	} ")
            .AppendLine("	//�V�K���[�h���i�y�V�K�o�^�z�������j ")
            .AppendLine("	if(blnTouroku == false) ")
            .AppendLine("	{ ")
            .AppendLine("		window.open('SeikyuuSakiTyuuijikouSearchList.aspx','TyuuijikouPopup','menubar=yes,toolbar=yes,location=yes,status=yes,resizable=yes,scrollbars=yes'); ")
            .AppendLine("		return true; ")
            .AppendLine("	} ")
            .AppendLine("	//�C���ƐV�K �񊈐� ")
            .AppendLine("	if((blnSyuusei == true)&&(blnTouroku == true)) ")
            .AppendLine("	{ ")
            .AppendLine("		window.open('SeikyuuSakiTyuuijikouSearchList.aspx','TyuuijikouPopup','menubar=yes,toolbar=yes,location=yes,status=yes,resizable=yes,scrollbars=yes'); ")
            .AppendLine("		return true; ")
            .AppendLine("	} ")
            .AppendLine("   return false; ")
            .AppendLine("} ")
            '==================2011/06/16 �ԗ� ����ӎ�����{�^���ǉ��̑Ή� �ǉ� �I����========================== 

            '===========2012/05/15 �ԗ� 407553�̑Ή� �ǉ���=========================
            .AppendLine("function fncSetBtnSaiban() ")
            .AppendLine("{ ")
            .AppendLine("	var strTougouKaikeiTokusakiCd = document.getElementById('" & Me.tbxTougouKaikeiTokusakiCd.ClientID & "').value.Trim(); ")
            .AppendLine("	var btnSaiban = document.getElementById('" & Me.btnSaiban.ClientID & "'); ")
            .AppendLine("	if(strTougouKaikeiTokusakiCd == '') ")
            .AppendLine("	{ ")
            .AppendLine("		btnSaiban.disabled = false; ")
            .AppendLine("	} ")
            .AppendLine("	else ")
            .AppendLine("	{ ")
            .AppendLine("		btnSaiban.disabled = true; ")
            .AppendLine("	} ")
            .AppendLine("} ")
            '===========2012/05/15 �ԗ� 407553�̑Ή� �ǉ���=========================

            .AppendLine("</script>")
        End With
        ClientScript.RegisterClientScriptBlock(csType, csName, csScript.ToString)
    End Sub

    ''' <summary>
    ''' �o�^�ƏC���l������
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SetMeisaiData() As Itis.Earth.DataAccess.SeikyuuSakiDataSet.m_seikyuu_sakiDataTable
        Dim dtSeikyuuSakiDataSet As New Itis.Earth.DataAccess.SeikyuuSakiDataSet.m_seikyuu_sakiDataTable

        dtSeikyuuSakiDataSet.Rows.Add(dtSeikyuuSakiDataSet.NewRow)
        '���
        dtSeikyuuSakiDataSet.Item(0).torikesi = IIf(chkTorikesi.Checked, "1", "0")
        '������R�[�h
        dtSeikyuuSakiDataSet.Item(0).seikyuu_saki_cd = tbxSeikyuuSakiCd.Text.ToUpper
        '������}��
        dtSeikyuuSakiDataSet.Item(0).seikyuu_saki_brc = tbxSeikyuuSakiBrc.Text.ToUpper
        '������敪
        dtSeikyuuSakiDataSet.Item(0).seikyuu_saki_kbn = ddlSeikyuuSakiKbn.SelectedValue

        '20100925�@�����R�[�h�A����於�@�ǉ��@�n���R������
        '�����R�[�h
        dtSeikyuuSakiDataSet.Item(0).nayose_saki_cd = tbxNayoseCd.Text.ToUpper
        '20100925�@�����R�[�h�A����於�@�ǉ��@�n���R������

        '20100712�@�d�l�ύX�@�ǉ��@�n���R������
        '�X�֔ԍ�
        dtSeikyuuSakiDataSet.Item(0).skysy_soufu_yuubin_no = tbxYuubinNo.Text
        '�Z���P
        dtSeikyuuSakiDataSet.Item(0).skysy_soufu_jyuusyo1 = tbxJyuusyo1.Text
        '�d�b�ԍ�
        dtSeikyuuSakiDataSet.Item(0).skysy_soufu_tel_no = tbxTelNo.Text
        '�Z���Q
        dtSeikyuuSakiDataSet.Item(0).skysy_soufu_jyuusyo2 = tbxJyuusyo2.Text
        'FAX�ԍ�
        dtSeikyuuSakiDataSet.Item(0).skysy_soufu_fax_no = tbxFaxNo.Text
        '20100712�@�d�l�ύX�@�ǉ��@�n���R������

        '�V��v���Ə��R�[�h
        dtSeikyuuSakiDataSet.Item(0).skk_jigyousyo_cd = tbxSkkJigyousyoCd.Text

        '��������R�[�h
        '==================2011/06/16 �ԗ� �C�� �J�n��==========================
        'dtSeikyuuSakiDataSet.Item(0).kyuu_seikyuu_saki_cd = tbxKyuuSeikyuuSakiCd.Text
        dtSeikyuuSakiDataSet.Item(0).kyuu_seikyuu_saki_cd = String.Empty
        '==================2011/06/16 �ԗ� �C�� �I����==========================

        '==================2011/06/16 �ԗ�  �ǉ� �J�n��========================== 
        '���Z����x���߃t���O
        dtSeikyuuSakiDataSet.Item(0).kessanji_nidosime_flg = Me.ddlKessanjiNidosimeFlg.SelectedValue
        '==================2011/06/16 �ԗ�  �ǉ� �I����==========================

        '�S���Җ�
        dtSeikyuuSakiDataSet.Item(0).tantousya_mei = tbxTantousya.Text
        '�������󎚕������t���O
        dtSeikyuuSakiDataSet.Item(0).seikyuusyo_inji_bukken_mei_flg = ddlSeikyuuSyoInjiBukkenMei.SelectedValue
        '���������ԍ�
        dtSeikyuuSakiDataSet.Item(0).nyuukin_kouza_no = tbxNyuukinKouzaNo.Text
        '�������ߓ�
        dtSeikyuuSakiDataSet.Item(0).seikyuu_sime_date = tbxSeikyuuSimeDate.Text
        '����������ߓ�
        dtSeikyuuSakiDataSet.Item(0).senpou_seikyuu_sime_date = tbxSenpouSeikyuuSimeDate.Text
        '���H�������^�C�~���O�t���O
        dtSeikyuuSakiDataSet.Item(0).tyk_koj_seikyuu_timing_flg = ddlTykKojSeikyuuTimingFlg.SelectedValue
        '���E�t���O
        dtSeikyuuSakiDataSet.Item(0).sousai_flg = IIf(chkSousaiFlg.Checked, "1", "0")
        '����\�茎��
        dtSeikyuuSakiDataSet.Item(0).kaisyuu_yotei_gessuu = tbxKaisyuuYoteiGessuu.Text
        '����\���
        dtSeikyuuSakiDataSet.Item(0).kaisyuu_yotei_date = tbxKaisyuuYoteiDate.Text
        '�������K����
        dtSeikyuuSakiDataSet.Item(0).seikyuusyo_hittyk_date = tbxSeikyuusyoHittykDate.Text
        '���1���1
        dtSeikyuuSakiDataSet.Item(0).kaisyuu1_syubetu1 = ddlKaisyuu1Syubetu1.SelectedValue
        '���1����1
        dtSeikyuuSakiDataSet.Item(0).kaisyuu1_wariai1 = tbxKaisyuu1Wariai1.Text
        '���1��`�T�C�g����
        dtSeikyuuSakiDataSet.Item(0).kaisyuu1_tegata_site_gessuu = tbxKaisyuu1TegataSiteGessuu.Text
        '���1��`�T�C�g��
        dtSeikyuuSakiDataSet.Item(0).kaisyuu1_tegata_site_date = tbxKaisyuu1TegataSiteDate.Text
        '���1�������p��
        dtSeikyuuSakiDataSet.Item(0).kaisyuu1_seikyuusyo_yousi = ddlKaisyuu1SeikyuusyoYousi.SelectedValue
        '���1���2
        dtSeikyuuSakiDataSet.Item(0).kaisyuu1_syubetu2 = ddlKaisyuu1Syubetu2.SelectedValue
        '���1����2
        dtSeikyuuSakiDataSet.Item(0).kaisyuu1_wariai2 = tbxKaisyuu1Wariai2.Text
        '���1���3
        dtSeikyuuSakiDataSet.Item(0).kaisyuu1_syubetu3 = ddlKaisyuu1Syubetu3.SelectedValue
        '���1����3
        dtSeikyuuSakiDataSet.Item(0).kaisyuu1_wariai3 = tbxKaisyuu1Wariai3.Text
        '������E�z
        dtSeikyuuSakiDataSet.Item(0).kaisyuu_kyoukaigaku = Replace(tbxKaisyuuKyoukaigaku.Text, ",", "")
        '���2���1
        dtSeikyuuSakiDataSet.Item(0).kaisyuu2_syubetu1 = ddlKaisyuu2Syubetu1.SelectedValue
        '���2����1
        dtSeikyuuSakiDataSet.Item(0).kaisyuu2_wariai1 = tbxKaisyuu2Wariai1.Text
        '���2��`�T�C�g����
        dtSeikyuuSakiDataSet.Item(0).kaisyuu2_tegata_site_gessuu = tbxKaisyuu2TegataSiteGessuu.Text
        '���2��`�T�C�g��
        dtSeikyuuSakiDataSet.Item(0).kaisyuu2_tegata_site_date = tbxKaisyuu2TegataSiteDate.Text
        '���2�������p��
        dtSeikyuuSakiDataSet.Item(0).kaisyuu2_seikyuusyo_yousi = ddlKaisyuu2SeikyuusyoYousi.SelectedValue
        '���2���2
        dtSeikyuuSakiDataSet.Item(0).kaisyuu2_syubetu2 = ddlKaisyuu2Syubetu2.SelectedValue
        '���2����2
        dtSeikyuuSakiDataSet.Item(0).kaisyuu2_wariai2 = tbxKaisyuu2Wariai2.Text
        '���2���3
        dtSeikyuuSakiDataSet.Item(0).kaisyuu2_syubetu3 = ddlKaisyuu2Syubetu3.SelectedValue
        '���2����3
        dtSeikyuuSakiDataSet.Item(0).kaisyuu2_wariai3 = tbxKaisyuu2Wariai3.Text

        '2013/5/29 �������̋�s�x�X�R�[�h�����ɔ���Earth���C -----------------------��
        '��s�x�X�R�[�h
        dtSeikyuuSakiDataSet.Item(0).ginkou_siten_cd = Me.ddlGinkouSitenCd.SelectedValue
        '2013/5/29 �������̋�s�x�X�R�[�h�����ɔ���Earth���C -----------------------��

        '===========2012/05/14 �ԗ� 407553�̑Ή� �ǉ���=====================
        With dtSeikyuuSakiDataSet.Item(0)
            '������v���Ӑ溰�� 
            .tougou_tokuisaki_cd = Me.tbxTougouKaikeiTokusakiCd.Text.Trim
            '���U�n�j�t���O
            .koufuri_ok_flg = Me.ddlKutiburiOkFlg.SelectedValue.Trim
            '���S���͉��_�~
            .anzen_kaihi_en = Me.tbxAnzenKyouryokuKaihi1.Text.Trim.Replace(",", String.Empty)
            '���S���͉��_��
            .anzen_kaihi_wari = Me.tbxAnzenKyouryokuKaihi2.Text.Trim
            '���l
            .bikou = Me.tbxBikou.Text.Trim
        End With
        dtSeikyuuSakiDataSet.Item(0).kyouryoku_kaihi_taisyou = Me.ddlKyouryokuKaihiJigou.SelectedValue

        '===========2012/05/14 �ԗ� 407553�̑Ή� �ǉ���=====================

        dtSeikyuuSakiDataSet.Item(0).upd_login_user_id = ViewState("UserId")
        dtSeikyuuSakiDataSet.Item(0).add_login_user_id = ViewState("UserId")
        dtSeikyuuSakiDataSet.Item(0).upd_datetime = hidUPDTime.Value

        Return dtSeikyuuSakiDataSet
    End Function

    ''' <summary>
    ''' ���׍��ڃN���A
    ''' </summary>
    ''' <remarks></remarks>
    Sub MeisaiClear()
        '���
        chkTorikesi.Checked = False
        '������敪
        SetDropSelect(ddlSeikyuuSakiKbn, "")
        '������R�[�h
        tbxSeikyuuSakiCd.Text = ""
        '������}��
        tbxSeikyuuSakiBrc.Text = ""
        '�����於
        tbxSeikyuuSakiMei.Text = ""

        '20100925�@�����R�[�h�A����於�@�ǉ��@�n���R������
        '�����R�[�h
        tbxNayoseCd.Text = ""
        '����於
        tbxNayoseMei.Text = ""
        '20100925�@�����R�[�h�A����於�@�ǉ��@�n���R������

        '20100712�@�d�l�ύX�@�ǉ��@�n���R������
        '�X�֔ԍ�
        tbxYuubinNo.Text = ""
        '�Z���P
        tbxJyuusyo1.Text = ""
        '�d�b�ԍ�
        tbxTelNo.Text = ""
        '�Z���Q
        tbxJyuusyo2.Text = ""
        'FAX�ԍ�
        tbxFaxNo.Text = ""
        '20100712�@�d�l�ύX�@�ǉ��@�n���R������

        '������o�^���`
        SetDropSelect(ddlSeikyuuSakiTourokuHinagata, "")
        '�S����
        tbxTantousya.Text = ""
        '�������󎚕�����
        SetDropSelect(ddlSeikyuuSyoInjiBukkenMei, "")
        '�V��v���Ə�
        tbxSkkJigyousyoCd.Text = ""
        '�V��v���Ə�
        tbxSkkJigyousyoMei.Text = ""
        '�������ߓ�
        tbxSeikyuuSimeDate.Text = ""
        '����������ߓ�
        tbxSenpouSeikyuuSimeDate.Text = ""
        ' �������K����
        tbxSeikyuusyoHittykDate.Text = ""

        '==================2011/06/16 �ԗ� �폜 �J�n��==========================
        ''��������R�[�h
        'tbxKyuuSeikyuuSakiCd.Text = ""
        '==================2011/06/16 �ԗ� �폜 �I����==========================

        '==================2011/06/16 �ԗ�  �ǉ� �J�n��========================== 
        '���Z����x���߃t���O
        Me.ddlKessanjiNidosimeFlg.SelectedValue = "0"
        '==================2011/06/16 �ԗ�  �ǉ� �I����==========================

        '����\�茎��
        tbxKaisyuuYoteiGessuu.Text = ""
        '����\���
        tbxKaisyuuYoteiDate.Text = ""
        '���������ԍ�
        tbxNyuukinKouzaNo.Text = ""
        '���E�t���O
        chkSousaiFlg.Checked = False
        '���H�������^�C�~���O
        SetDropSelect(ddlTykKojSeikyuuTimingFlg, "")
        '���1��`�T�C�g����
        tbxKaisyuu1TegataSiteGessuu.Text = ""
        '���1��`�T�C�g��
        tbxKaisyuu1TegataSiteDate.Text = ""
        '���1�������p��
        SetDropSelect(ddlKaisyuu1SeikyuusyoYousi, "")
        '���1���1
        SetDropSelect(ddlKaisyuu1Syubetu1, "")
        '���1����1
        tbxKaisyuu1Wariai1.Text = ""
        '���1���2
        SetDropSelect(ddlKaisyuu1Syubetu2, "")
        '���1����2
        tbxKaisyuu1Wariai2.Text = ""
        '���1���3
        SetDropSelect(ddlKaisyuu1Syubetu3, "")
        '���1����3
        tbxKaisyuu1Wariai3.Text = ""
        '������E�z
        tbxKaisyuuKyoukaigaku.Text = ""
        '���2��`�T�C�g����
        tbxKaisyuu2TegataSiteGessuu.Text = ""
        '���2��`�T�C�g��
        tbxKaisyuu2TegataSiteDate.Text = ""
        '���2�������p��
        SetDropSelect(ddlKaisyuu2SeikyuusyoYousi, "")
        '���2���1
        SetDropSelect(ddlKaisyuu2Syubetu1, "")
        '���2����1
        tbxKaisyuu2Wariai1.Text = ""
        '���2���2
        SetDropSelect(ddlKaisyuu2Syubetu2, "")
        '���2����2
        tbxKaisyuu2Wariai2.Text = ""
        '���2���3
        SetDropSelect(ddlKaisyuu2Syubetu3, "")
        '���2����3
        tbxKaisyuu2Wariai3.Text = ""

        '2013/5/29 �������̋�s�x�X�R�[�h�����ɔ���Earth���C -----------------------��
        '��s�x�X�R�[�h
        SetDropSelect(Me.ddlGinkouSitenCd, "804")
        '2013/5/29 �������̋�s�x�X�R�[�h�����ɔ���Earth���C -----------------------��

        '===========2012/05/14 �ԗ� 407553�̑Ή� �ǉ���=====================
        '������v���Ӑ溰�� 
        Me.tbxTougouKaikeiTokusakiCd.Text = String.Empty
        '���U�n�j�t���O
        SetDropSelect(ddlKutiburiOkFlg, String.Empty)
        '���͉��_�K�p����
        Me.ddlKyouryokuKaihiJigou.SelectedIndex = 0
        '���S���͉��_�~
        Me.tbxAnzenKyouryokuKaihi1.Text = String.Empty
        '���S���͉��_��
        Me.tbxAnzenKyouryokuKaihi2.Text = String.Empty
        '���l
        Me.tbxBikou.Text = String.Empty

        '�u�̔ԁv�{�^�����Z�b�g����
        Call Me.SetBtnSaiban()
        '===========2012/05/14 �ԗ� 407553�̑Ή� �ǉ���=====================

        hidUPDTime.Value = ""
        If Not blnBtn Then
            btnSyuusei.Enabled = False
            btnTouroku.Enabled = False
        Else
            btnSyuusei.Enabled = False
            btnTouroku.Enabled = True
        End If
        Me.btnKensakuSeikyuuSaki.Enabled = True
        ddlSeikyuuSakiKbn.Attributes.Remove("disabled")
        tbxSeikyuuSakiCd.Attributes.Remove("readonly")
        tbxSeikyuuSakiBrc.Attributes.Remove("readonly")
        UpdatePanelA.Update()
    End Sub

    ''' <summary>
    ''' ���׃f�[�^���擾
    ''' </summary>
    ''' <param name="TyousaKaisya_Cd"></param>
    ''' <param name="btn"></param>
    ''' <remarks></remarks>
    Sub GetMeisaiData(ByVal TyousaKaisya_Cd As String, _
                      ByVal TyousaKaisyaCd As String, _
                      ByVal JigyousyoCd As String, _
                      Optional ByVal btn As String = "")

        Dim strErr As String = ""
        Dim dtSeikyuuSakiDataSet As New Itis.Earth.DataAccess.SeikyuuSakiDataSet.m_seikyuu_sakiDataTable
        dtSeikyuuSakiDataSet = SeikyuuSakiMasterBL.SelSeikyuuSakiInfo(TyousaKaisya_Cd, TyousaKaisyaCd, JigyousyoCd)

        If dtSeikyuuSakiDataSet.Rows.Count = 1 Then
            With dtSeikyuuSakiDataSet.Item(0)
                '���
                chkTorikesi.Checked = IIf(.torikesi = "0", False, True)
                '������敪
                SetDropSelect(ddlSeikyuuSakiKbn, TrimNull(.seikyuu_saki_kbn))
                '������R�[�h
                tbxSeikyuuSakiCd.Text = TrimNull(.seikyuu_saki_cd)
                '������}��
                tbxSeikyuuSakiBrc.Text = TrimNull(.seikyuu_saki_brc)
                '�����於
                tbxSeikyuuSakiMei.Text = TrimNull(.seikyuu_saki_mei)
                If btn = "btnSearch" Then
                    '�����於
                    tbxSeikyuuSaki_Mei.Text = TrimNull(.seikyuu_saki_mei)

                    tbxSeikyuuSaki_Cd.Text = TrimNull(.seikyuu_saki_cd).ToUpper
                    tbxSeikyuuSaki_Brc.Text = TrimNull(.seikyuu_saki_brc).ToUpper
                End If

                '20100925�@�����R�[�h�A����於�@�ǉ��@�n���R������
                '�����R�[�h
                tbxNayoseCd.Text = TrimNull(.nayose_saki_cd)
                '����於
                'tbxNayoseMei.Text = TrimNull(.nayose_saki_name1)
                If tbxNayoseCd.Text <> String.Empty Then
                    Dim dtNayoseSakiTable As New Data.DataTable
                    dtNayoseSakiTable = SeikyuuSakiMasterBL.SelNayoseSakiInfo(tbxNayoseCd.Text.Trim)
                    If dtNayoseSakiTable.Rows.Count > 0 Then
                        tbxNayoseMei.Text = TrimNull(dtNayoseSakiTable.Rows(0).Item("nayose_saki_name1"))
                    Else
                        tbxNayoseMei.Text = "�����R�[�h�@���o�^"
                    End If
                Else
                    tbxNayoseMei.Text = ""
                End If
                '20100925�@�����R�[�h�A����於�@�ǉ��@�n���R������

                '20100712�@�d�l�ύX�@�ǉ��@�n���R������
                '�X�֔ԍ�
                tbxYuubinNo.Text = TrimNull(.skysy_soufu_yuubin_no)
                '�Z���P
                tbxJyuusyo1.Text = TrimNull(.skysy_soufu_jyuusyo1)
                '�d�b�ԍ�
                tbxTelNo.Text = TrimNull(.skysy_soufu_tel_no)
                '�Z���Q
                tbxJyuusyo2.Text = TrimNull(.skysy_soufu_jyuusyo2)
                'FAX�ԍ�
                tbxFaxNo.Text = TrimNull(.skysy_soufu_fax_no)
                '20100712�@�d�l�ύX�@�ǉ��@�n���R������

                '�S����
                tbxTantousya.Text = TrimNull(.tantousya_mei)
                '�������󎚕�����
                SetDropSelect(ddlSeikyuuSyoInjiBukkenMei, TrimNull(.seikyuusyo_inji_bukken_mei_flg))
                '�V��v���Ə�
                tbxSkkJigyousyoCd.Text = TrimNull(.skk_jigyousyo_cd)
                '�V��v���Ə�
                tbxSkkJigyousyoMei.Text = TrimNull(.skk_jigyousyo_mei)
                '�������ߓ�
                tbxSeikyuuSimeDate.Text = TrimNull(.seikyuu_sime_date)
                '����������ߓ�
                tbxSenpouSeikyuuSimeDate.Text = TrimNull(.senpou_seikyuu_sime_date)
                ' �������K����
                tbxSeikyuusyoHittykDate.Text = TrimNull(.seikyuusyo_hittyk_date)

                '==================2011/06/16 �ԗ� �폜 �J�n��==========================
                ''��������R�[�h
                'tbxKyuuSeikyuuSakiCd.Text = TrimNull(.kyuu_seikyuu_saki_cd)
                '==================2011/06/16 �ԗ� �폜 �I����==========================

                '==================2011/06/16 �ԗ�  �ǉ� �J�n��========================== 
                '���Z����x���߃t���O
                Me.ddlKessanjiNidosimeFlg.SelectedValue = IIf(TrimNull(.kessanji_nidosime_flg).Equals("1"), "1", "0")
                '==================2011/06/16 �ԗ�  �ǉ� �I����==========================

                '����\�茎��
                tbxKaisyuuYoteiGessuu.Text = TrimNull(.kaisyuu_yotei_gessuu)
                '����\���
                tbxKaisyuuYoteiDate.Text = TrimNull(.kaisyuu_yotei_date)
                '���������ԍ�
                tbxNyuukinKouzaNo.Text = TrimNull(.nyuukin_kouza_no)
                '���E�t���O
                chkSousaiFlg.Checked = IIf(.sousai_flg = "0", False, True)
                '���H�������^�C�~���O
                SetDropSelect(ddlTykKojSeikyuuTimingFlg, TrimNull(.tyk_koj_seikyuu_timing_flg))
                '���1��`�T�C�g����
                tbxKaisyuu1TegataSiteGessuu.Text = TrimNull(.kaisyuu1_tegata_site_gessuu)
                '���1��`�T�C�g��
                tbxKaisyuu1TegataSiteDate.Text = TrimNull(.kaisyuu1_tegata_site_date)
                '���1�������p��
                SetDropSelect(ddlKaisyuu1SeikyuusyoYousi, TrimNull(.kaisyuu1_seikyuusyo_yousi))
                '���1���1
                SetDropSelect(ddlKaisyuu1Syubetu1, TrimNull(.kaisyuu1_syubetu1))
                '���1����1
                tbxKaisyuu1Wariai1.Text = TrimNull(.kaisyuu1_wariai1)
                '���1���2
                SetDropSelect(ddlKaisyuu1Syubetu2, TrimNull(.kaisyuu1_syubetu2))
                '���1����2
                tbxKaisyuu1Wariai2.Text = TrimNull(.kaisyuu1_wariai2)
                '���1���3
                SetDropSelect(ddlKaisyuu1Syubetu3, TrimNull(.kaisyuu1_syubetu3))
                '���1����1
                tbxKaisyuu1Wariai3.Text = TrimNull(.kaisyuu1_wariai3)
                '������E�z
                tbxKaisyuuKyoukaigaku.Text = AddComa(.kaisyuu_kyoukaigaku)

                '���2��`�T�C�g����
                tbxKaisyuu2TegataSiteGessuu.Text = TrimNull(.kaisyuu2_tegata_site_gessuu)
                '���2��`�T�C�g��
                tbxKaisyuu2TegataSiteDate.Text = TrimNull(.kaisyuu2_tegata_site_date)
                '���2�������p��
                SetDropSelect(ddlKaisyuu2SeikyuusyoYousi, TrimNull(.kaisyuu2_seikyuusyo_yousi))
                '���2���1
                SetDropSelect(ddlKaisyuu2Syubetu1, TrimNull(.kaisyuu2_syubetu1))
                '���2����1
                tbxKaisyuu2Wariai1.Text = TrimNull(.kaisyuu2_wariai1)
                '���2���2
                SetDropSelect(ddlKaisyuu2Syubetu2, TrimNull(.kaisyuu2_syubetu2))
                '���2����2
                tbxKaisyuu2Wariai2.Text = TrimNull(.kaisyuu2_wariai2)
                '���2���3
                SetDropSelect(ddlKaisyuu2Syubetu3, TrimNull(.kaisyuu2_syubetu3))
                '���2����1
                tbxKaisyuu2Wariai3.Text = TrimNull(.kaisyuu2_wariai3)

                '2013/5/29 �������̋�s�x�X�R�[�h�����ɔ���Earth���C -----------------------��
                '��s�x�X�R�[�h
                SetDropSelect(ddlGinkouSitenCd, TrimNull(.ginkou_siten_cd))
                '2013/5/29 �������̋�s�x�X�R�[�h�����ɔ���Earth���C -----------------------��

                '===========2012/05/14 �ԗ� 407553�̑Ή� �ǉ���=====================
                '������v���Ӑ溰�� 
                Me.tbxTougouKaikeiTokusakiCd.Text = TrimNull(.tougou_tokuisaki_cd)
                '���U�n�j�t���O
                SetDropSelect(ddlKutiburiOkFlg, TrimNull(.koufuri_ok_flg))
                '���͉��_�K�p����
                Me.ddlKyouryokuKaihiJigou.SelectedValue = TrimNull(dtSeikyuuSakiDataSet.Item(0).kyouryoku_kaihi_taisyou)
                '���S���͉��_�~
                If TrimNull(.anzen_kaihi_en).Equals(String.Empty) Then
                    Me.tbxAnzenKyouryokuKaihi1.Text = String.Empty
                Else
                    Me.tbxAnzenKyouryokuKaihi1.Text = FormatNumber(TrimNull(.anzen_kaihi_en), 0)
                End If
                '���S���͉��_��
                If TrimNull(.anzen_kaihi_wari).Equals(String.Empty) Then
                    Me.tbxAnzenKyouryokuKaihi2.Text = String.Empty
                Else
                    Me.tbxAnzenKyouryokuKaihi2.Text = Convert.ToDecimal(.anzen_kaihi_wari).ToString("0.#####")
                End If
                '���l
                Me.tbxBikou.Text = TrimNull(.bikou)

                '�u�̔ԁv�{�^�����Z�b�g����
                Call Me.SetBtnSaiban()
                '===========2012/05/14 �ԗ� 407553�̑Ή� �ǉ���=====================

                hidUPDTime.Value = TrimNull(.upd_datetime)
                UpdatePanelA.Update()
            End With

            Me.hidConfirm.Value = ""

            If Not blnBtn Then
                btnSyuusei.Enabled = False
                btnTouroku.Enabled = False
            Else
                btnSyuusei.Enabled = True
                btnTouroku.Enabled = False
            End If

            tbxSeikyuuSakiCd.Attributes.Add("readonly", "true;")
            tbxSeikyuuSakiBrc.Attributes.Add("readonly", "true;")
            ddlSeikyuuSakiKbn.Attributes.Add("disabled", "true;")

            Me.btnKensakuSeikyuuSaki.Enabled = False
        Else
            MeisaiClear()
            Me.tbxSeikyuuSaki_Mei.Text = ""
            strErr = "alert('" & Messages.Instance.MSG2034E & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strErr, True)

        End If

    End Sub

    ''' <summary>
    ''' ���̓`�F�b�N
    ''' </summary>
    ''' <param name="strErr">�G���[���b�Z�[�W</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function InputCheck(ByRef strErr As String) As String
        Dim strID As String = ""
        '������敪
        If strErr = "" Then
            '���͕K�{
            strErr = commoncheck.CheckHissuNyuuryoku(ddlSeikyuuSakiKbn.Text, "������敪")
            strID = ddlSeikyuuSakiKbn.ClientID
        End If

        '������R�[�h
        If strErr = "" Then
            '���͕K�{
            strErr = commoncheck.CheckHissuNyuuryoku(tbxSeikyuuSakiCd.Text, "������R�[�h")
            strID = tbxSeikyuuSakiCd.ClientID
        End If
        If strErr = "" And tbxSeikyuuSakiCd.Text <> "" Then
            '���p�p����
            strErr = commoncheck.ChkHankakuEisuuji(tbxSeikyuuSakiCd.Text, "������R�[�h")
            strID = tbxSeikyuuSakiCd.ClientID
        End If

        '������}��
        If strErr = "" Then
            '���͕K�{
            strErr = commoncheck.CheckHissuNyuuryoku(tbxSeikyuuSakiBrc.Text, "������}��")
            strID = tbxSeikyuuSakiBrc.ClientID
        End If
        If strErr = "" And tbxSeikyuuSakiBrc.Text <> "" Then
            '���p�p����
            strErr = commoncheck.ChkHankakuEisuuji(tbxSeikyuuSakiBrc.Text, "������}��")
            strID = tbxSeikyuuSakiBrc.ClientID
        End If
        If strErr = "" And (tbxNayoseCd.Text <> "" And tbxNayoseCd.Text.Length <> 5) Then
            strErr = String.Format(Messages.Instance.MSG2067E, "�����R�[�h", "5").ToString
            strID = tbxNayoseCd.ClientID
        End If

        '20100925�@�����R�[�h�A����於�@�ǉ��@�n���R������
        '�����R�[�h
        If strErr = "" And tbxNayoseCd.Text <> "" Then
            '���p�p����
            strErr = commoncheck.ChkHankakuEisuuji(tbxNayoseCd.Text, "�����R�[�h")
            strID = tbxNayoseCd.ClientID
        End If
        '20100925�@�����R�[�h�A����於�@�ǉ��@�n���R������

        '=====================2012/03/31 �ԗ� �v�]�̑Ή� �폜��===================================
        '���̓R�[�h�Ɨ^�M�Ǘ�Ͻ�.����溰�ނ���v���邩�`�F�b�N
        'If strErr = "" And tbxNayoseCd.Text <> "" Then
        '    If SeikyuuSakiMasterBL.SelNayoseSakiInfo(tbxNayoseCd.Text.Trim).Rows.Count = 0 Then
        '        strErr = String.Format(Messages.Instance.MSG2068E, "�����R�[�h").ToString
        '        strID = tbxNayoseCd.ClientID
        '    End If
        'End If
        '=====================2012/03/31 �ԗ� �v�]�̑Ή� �폜��===================================

        '20100712�@�d�l�ύX�@�ǉ��@�n���R������
        '�X�֔ԍ�
        If strErr = "" And tbxYuubinNo.Text <> "" Then
            strErr = commoncheck.CheckHankakuHaifun(tbxYuubinNo.Text, "�X�֔ԍ�", "1")
            If strErr <> "" Then
                strID = tbxYuubinNo.ClientID
            End If
        End If
        '�Z���P
        If strErr = "" And tbxJyuusyo1.Text <> "" Then
            strErr = commoncheck.CheckByte(tbxJyuusyo1.Text, 40, "�Z���P", kbn.ZENKAKU)
            If strErr <> "" Then
                strID = tbxJyuusyo1.ClientID
            End If
        End If
        If strErr = "" And tbxJyuusyo1.Text <> "" Then
            strErr = commoncheck.CheckKinsoku(tbxJyuusyo1.Text, "�Z���P")
            If strErr <> "" Then
                strID = tbxJyuusyo1.ClientID
            End If
        End If
        '�d�b�ԍ�
        If strErr = "" And tbxTelNo.Text <> "" Then
            strErr = commoncheck.CheckHankakuHaifun(tbxTelNo.Text, "�d�b�ԍ�", "1")
            If strErr <> "" Then
                strID = tbxTelNo.ClientID
            End If
        End If
        '�Z���Q
        If strErr = "" And tbxJyuusyo2.Text <> "" Then
            strErr = commoncheck.CheckByte(tbxJyuusyo2.Text, 30, "�Z���Q", kbn.ZENKAKU)
            If strErr <> "" Then
                strID = tbxJyuusyo2.ClientID
            End If
        End If
        If strErr = "" And tbxJyuusyo2.Text <> "" Then
            strErr = commoncheck.CheckKinsoku(tbxJyuusyo2.Text, "�Z���Q")
            If strErr <> "" Then
                strID = tbxJyuusyo2.ClientID
            End If
        End If
        'FAX�ԍ�
        If strErr = "" And tbxFaxNo.Text <> "" Then
            strErr = commoncheck.CheckHankakuHaifun(tbxFaxNo.Text, "FAX�ԍ�", "1")
            If strErr <> "" Then
                strID = tbxFaxNo.ClientID
            End If
        End If
        '20100712�@�d�l�ύX�@�ǉ��@�n���R������

        '�V��v���Ə�
        If strErr = "" And tbxSkkJigyousyoCd.Text <> "" Then
            '���p�p����
            strErr = commoncheck.ChkHankakuEisuuji(tbxSkkJigyousyoCd.Text, "�V��v���Ə��R�[�h")
            strID = tbxSkkJigyousyoCd.ClientID
        End If

        '�S����
        If strErr = "" And tbxTantousya.Text <> "" Then
            strErr = commoncheck.CheckByte(tbxTantousya.Text, 40, "�S����", kbn.ZENKAKU)
            If strErr <> "" Then
                strID = tbxTantousya.ClientID
            End If
        End If
        If strErr = "" And tbxTantousya.Text <> "" Then
            strErr = commoncheck.CheckKinsoku(tbxTantousya.Text, "�S����")
            If strErr <> "" Then
                strID = tbxTantousya.ClientID
            End If
        End If

        '�������ߓ�
        If strErr = "" And tbxSeikyuuSimeDate.Text <> "" Then
            strErr = commoncheck.CheckSime(tbxSeikyuuSimeDate.Text, "�������ߓ�", "31")
            If strErr <> "" Then
                strID = tbxSeikyuuSimeDate.ClientID
            End If
        End If

        '����������ߓ�
        If strErr = "" And tbxSenpouSeikyuuSimeDate.Text <> "" Then
            strErr = commoncheck.CheckSime(tbxSenpouSeikyuuSimeDate.Text, "����������ߓ�", "31")
            If strErr <> "" Then
                strID = tbxSenpouSeikyuuSimeDate.ClientID
            End If
        End If

        '�������K����
        If strErr = "" And tbxSeikyuusyoHittykDate.Text <> "" Then
            strErr = commoncheck.CheckSime(tbxSeikyuusyoHittykDate.Text, "�������K����", "31")
            If strErr <> "" Then
                strID = tbxSeikyuusyoHittykDate.ClientID
            End If
        End If

        '==================2011/06/16 �ԗ� �폜 �J�n��==========================
        ''��������R�[�h
        'If strErr = "" And tbxKyuuSeikyuuSakiCd.Text <> "" Then
        '    '���p�p����
        '    strErr = commoncheck.ChkHankakuEisuuji(tbxKyuuSeikyuuSakiCd.Text, "��������R�[�h")
        '    strID = tbxKyuuSeikyuuSakiCd.ClientID
        'End If
        '==================2011/06/16 �ԗ� �폜 �I����==========================

        '����\�茎��
        If strErr = "" And tbxKaisyuuYoteiGessuu.Text <> "" Then
            '2010/12/02 �n���R�@�C���@����\�茎��-���l�͈͂�0�`12�܂Łi�����̂݁j�@��
            'strErr = commoncheck.CheckSime(tbxKaisyuuYoteiGessuu.Text, "����\�茎��", "12")
            strErr = commoncheck.CheckSime1(tbxKaisyuuYoteiGessuu.Text, "����\�茎��", "12")
            '2010/12/02 �n���R�@�C���@����\�茎��-���l�͈͂�0�`12�܂Łi�����̂݁j�@��
            If strErr <> "" Then
                strID = tbxKaisyuuYoteiGessuu.ClientID
            End If
        End If

        '����\���
        If strErr = "" And tbxKaisyuuYoteiDate.Text <> "" Then
            strErr = commoncheck.CheckSime(tbxKaisyuuYoteiDate.Text, "����\���", "31")
            If strErr <> "" Then
                strID = tbxKaisyuuYoteiDate.ClientID
            End If
        End If

        '���������ԍ�
        If strErr = "" And tbxNyuukinKouzaNo.Text <> "" Then
            strErr = commoncheck.ChkHankakuEisuuji(tbxNyuukinKouzaNo.Text, "���������ԍ�")
            If strErr <> "" Then
                strID = tbxNyuukinKouzaNo.ClientID
            End If
        End If

        '���1��`�T�C�g����
        If strErr = "" And tbxKaisyuu1TegataSiteGessuu.Text <> "" Then
            strErr = commoncheck.CheckSime(tbxKaisyuu1TegataSiteGessuu.Text, "���1��`�T�C�g����", "12")
            If strErr <> "" Then
                strID = tbxKaisyuu1TegataSiteGessuu.ClientID
            End If
        End If

        '���1��`�T�C�g��
        If strErr = "" And tbxKaisyuu1TegataSiteDate.Text <> "" Then
            strErr = commoncheck.CheckSime(tbxKaisyuu1TegataSiteDate.Text, "���1��`�T�C�g��", "31")
            If strErr <> "" Then
                strID = tbxKaisyuu1TegataSiteDate.ClientID
            End If
        End If

        '������E�z
        If strErr = "" And tbxKaisyuuKyoukaigaku.Text <> "" Then
            strErr = commoncheck.CheckNum(tbxKaisyuuKyoukaigaku.Text, "������E�z", "1")
            If strErr <> "" Then
                strID = tbxKaisyuuKyoukaigaku.ClientID
            End If
        End If

        '���2��`�T�C�g����
        If strErr = "" And tbxKaisyuu2TegataSiteGessuu.Text <> "" Then
            strErr = commoncheck.CheckSime(tbxKaisyuu2TegataSiteGessuu.Text, "���2��`�T�C�g����", "12")
            If strErr <> "" Then
                strID = tbxKaisyuu2TegataSiteGessuu.ClientID
            End If
        End If

        '���2��`�T�C�g��
        If strErr = "" And tbxKaisyuu2TegataSiteDate.Text <> "" Then
            strErr = commoncheck.CheckSime(tbxKaisyuu2TegataSiteDate.Text, "���2��`�T�C�g��", "31")
            If strErr <> "" Then
                strID = tbxKaisyuu2TegataSiteDate.ClientID
            End If
        End If

        '���1����1
        If strErr = "" And tbxKaisyuu1Wariai1.Text <> "" Then
            strErr = commoncheck.CheckSuuti(tbxKaisyuu1Wariai1.Text, "���1����1", "100")
            If strErr <> "" Then
                strID = tbxKaisyuu1Wariai1.ClientID
            End If
        End If

        '���1���1
        If strErr = "" And tbxKaisyuu1Wariai1.Text <> "" Then
            If ddlKaisyuu1Syubetu1.SelectedValue.Trim = "" Then
                strErr = Messages.Instance.MSG2052E
            End If
            If strErr <> "" Then
                strID = ddlKaisyuu1Syubetu1.ClientID
            End If
        End If

        '���1����2
        If strErr = "" And tbxKaisyuu1Wariai2.Text <> "" Then
            strErr = commoncheck.CheckSuuti(tbxKaisyuu1Wariai2.Text, "���1����2", "100")
            If strErr <> "" Then
                strID = tbxKaisyuu1Wariai2.ClientID
            End If
        End If

        '���1���2
        If strErr = "" And tbxKaisyuu1Wariai2.Text <> "" Then
            If ddlKaisyuu1Syubetu2.SelectedValue.Trim = "" Then
                strErr = Messages.Instance.MSG2052E
            End If
            If strErr <> "" Then
                strID = ddlKaisyuu1Syubetu2.ClientID
            End If
        End If

        '���1����3
        If strErr = "" And tbxKaisyuu1Wariai3.Text <> "" Then
            strErr = commoncheck.CheckSuuti(tbxKaisyuu1Wariai3.Text, "���1����3", "100")
            If strErr <> "" Then
                strID = tbxKaisyuu1Wariai3.ClientID
            End If
        End If

        '���1���3
        If strErr = "" And tbxKaisyuu1Wariai3.Text <> "" Then
            If ddlKaisyuu1Syubetu3.SelectedValue.Trim = "" Then
                strErr = Messages.Instance.MSG2052E
            End If
            If strErr <> "" Then
                strID = ddlKaisyuu1Syubetu3.ClientID
            End If
        End If

        '�����`�F�b�N
        If strErr = "" Then
            strErr = commoncheck.CheckWariai(tbxKaisyuu1Wariai1.Text, tbxKaisyuu1Wariai2.Text, tbxKaisyuu1Wariai3.Text, "���1", "100")
            If strErr <> "" Then
                strID = tbxKaisyuu1Wariai1.ClientID
            End If
        End If


        '���2����1
        If strErr = "" And tbxKaisyuu2Wariai1.Text <> "" Then
            strErr = commoncheck.CheckSuuti(tbxKaisyuu2Wariai1.Text, "���2����1", "100")
            If strErr <> "" Then
                strID = tbxKaisyuu2Wariai1.ClientID
            End If
        End If
        '���2���1
        If strErr = "" And tbxKaisyuu2Wariai1.Text <> "" Then
            If ddlKaisyuu2Syubetu1.SelectedValue.Trim = "" Then
                strErr = Messages.Instance.MSG2052E
            End If
            If strErr <> "" Then
                strID = ddlKaisyuu2Syubetu1.ClientID
            End If
        End If

        '���2����2
        If strErr = "" And tbxKaisyuu2Wariai2.Text <> "" Then
            strErr = commoncheck.CheckSuuti(tbxKaisyuu2Wariai2.Text, "���2����2", "100")
            If strErr <> "" Then
                strID = tbxKaisyuu2Wariai2.ClientID
            End If
        End If
        '���2���2
        If strErr = "" And tbxKaisyuu2Wariai2.Text <> "" Then
            If ddlKaisyuu2Syubetu2.SelectedValue.Trim = "" Then
                strErr = Messages.Instance.MSG2052E
            End If
            If strErr <> "" Then
                strID = ddlKaisyuu2Syubetu2.ClientID
            End If
        End If

        '���2����3
        If strErr = "" And tbxKaisyuu2Wariai3.Text <> "" Then
            strErr = commoncheck.CheckSuuti(tbxKaisyuu2Wariai3.Text, "���2����3", "100")
            If strErr <> "" Then
                strID = tbxKaisyuu2Wariai3.ClientID
            End If
        End If
        '���2���3
        If strErr = "" And tbxKaisyuu2Wariai3.Text <> "" Then
            If ddlKaisyuu2Syubetu3.SelectedValue.Trim = "" Then
                strErr = Messages.Instance.MSG2052E
            End If
            If strErr <> "" Then
                strID = ddlKaisyuu2Syubetu3.ClientID
            End If
        End If

        '�����`�F�b�N
        If strErr = "" Then
            strErr = commoncheck.CheckWariai(tbxKaisyuu2Wariai1.Text, tbxKaisyuu2Wariai2.Text, tbxKaisyuu2Wariai3.Text, "���2", "100")
            If strErr <> "" Then
                strID = tbxKaisyuu2Wariai1.ClientID
            End If
        End If

        '============2012/05/15 �ԗ� 407553�̑Ή� �ǉ���==========================
        '������v���Ӑ溰��(���p����)
        If (strErr = "") AndAlso (Not Me.tbxTougouKaikeiTokusakiCd.Text.Trim.Equals(String.Empty)) Then
            strErr = commoncheck.CheckHankaku(Me.tbxTougouKaikeiTokusakiCd.Text.Trim, "������v���Ӑ溰��", "1")
            If strErr <> "" Then
                strID = Me.tbxTougouKaikeiTokusakiCd.ClientID
            End If
        End If

        '���S���͉��(�~�A����)(�����ɓo�^�͂ł��Ȃ�)
        If strErr = "" Then
            '�~
            Dim strEn As String = Me.tbxAnzenKyouryokuKaihi1.Text.Trim
            '����
            Dim strRitu As String = Me.tbxAnzenKyouryokuKaihi2.Text.Trim

            If (Not strEn.Equals(String.Empty)) AndAlso (Not strRitu.Equals(String.Empty)) Then
                strErr = Messages.Instance.MSG2072E
            End If

            If strErr <> "" Then
                strID = Me.tbxAnzenKyouryokuKaihi1.ClientID
            End If
        End If

        '���S���͉��_�~(���p����)
        If (strErr = "") AndAlso (Not Me.tbxAnzenKyouryokuKaihi1.Text.Trim.Equals(String.Empty)) Then
            strErr = commoncheck.CheckHankaku(Me.tbxAnzenKyouryokuKaihi1.Text.Trim.Replace(",", String.Empty), "���S���͉��_�~", "1")
            If strErr <> "" Then
                strID = Me.tbxAnzenKyouryokuKaihi1.ClientID
            End If
        End If

        '���S���͉��_�~(int�͈͓�)
        If (strErr = "") AndAlso (Not Me.tbxAnzenKyouryokuKaihi1.Text.Trim.Equals(String.Empty)) Then

            Dim intTemp = Convert.ToDouble(Me.tbxAnzenKyouryokuKaihi1.Text.Trim)
            If (intTemp < 0) OrElse (intTemp > 2147483647) Then
                strErr = String.Format(Messages.Instance.MSG2056E, "���S���͉��_�~")
            End If

            If strErr <> "" Then
                strID = Me.tbxAnzenKyouryokuKaihi1.ClientID
            End If
        End If

        '���S���͉��_����(decimal(6,5))
        If (strErr = "") AndAlso (Not Me.tbxAnzenKyouryokuKaihi2.Text.Trim.Equals(String.Empty)) Then
            strErr = commoncheck.CheckSyousuu(Me.tbxAnzenKyouryokuKaihi2.Text.Trim, "���S���͉��_����", 1, 5)
            If strErr <> "" Then
                strID = Me.tbxAnzenKyouryokuKaihi2.ClientID
            End If
        End If

        '���l(�o�C�g40)
        If (strErr = "") AndAlso (Not Me.tbxBikou.Text.Trim.Equals(String.Empty)) Then
            strErr = commoncheck.CheckByte(Me.tbxBikou.Text.Trim, 40, "���l", kbn.ZENKAKU)
            If strErr <> "" Then
                strID = Me.tbxBikou.ClientID
            End If
        End If

        '���l(�֑�����)
        If (strErr = "") AndAlso (Not Me.tbxBikou.Text.Trim.Equals(String.Empty)) Then
            strErr = commoncheck.CheckKinsoku(Me.tbxBikou.Text.Trim, "���l")
            If strErr <> "" Then
                strID = tbxBikou.ClientID
            End If
        End If
        '============2012/05/15 �ԗ� 407553�̑Ή� �ǉ���==========================



        Return strID

    End Function

    ''' <summary>
    ''' ������o�^���`�}�X�^�ݒ�
    ''' </summary>
    ''' <remarks></remarks>
    Sub SetSeikyuuSakiHinagata()
        Dim dtSeikyuuSakiHinagata As New Itis.Earth.DataAccess.SeikyuuSakiHinagataDataSet.m_seikyuu_saki_touroku_hinagataDataTable
        dtSeikyuuSakiHinagata = SeikyuuSakiMasterBL.SelSeikyuuSakiHinagataInfo(ddlSeikyuuSakiTourokuHinagata.SelectedValue)
        If dtSeikyuuSakiHinagata.Rows.Count > 0 Then
            '�f�[�^���L�鎞
            '�V��v���Ə��R�[�h
            tbxSkkJigyousyoCd.Text = TrimNull(dtSeikyuuSakiHinagata.Item(0).skk_jigyousyo_cd)
            '�S���Җ�
            tbxTantousya.Text = TrimNull(dtSeikyuuSakiHinagata.Item(0).tantousya_mei)
            '�������󎚕������t���O
            SetDropSelect(ddlSeikyuuSyoInjiBukkenMei, TrimNull(dtSeikyuuSakiHinagata.Item(0).seikyuusyo_inji_bukken_mei_flg))
            '���������ԍ�
            tbxNyuukinKouzaNo.Text = TrimNull(dtSeikyuuSakiHinagata.Item(0).nyuukin_kouza_no)
            '�������ߓ�
            tbxSeikyuuSimeDate.Text = TrimNull(dtSeikyuuSakiHinagata.Item(0).seikyuu_sime_date)
            '����������ߓ�
            tbxSenpouSeikyuuSimeDate.Text = TrimNull(dtSeikyuuSakiHinagata.Item(0).senpou_seikyuu_sime_date)
            '���E�t���O
            chkSousaiFlg.Checked = IIf(dtSeikyuuSakiHinagata.Item(0).sousai_flg = "0", False, True)

            '���H�������^�C�~���O
            SetDropSelect(ddlTykKojSeikyuuTimingFlg, TrimNull(dtSeikyuuSakiHinagata.Item(0).tyk_koj_seikyuu_timing_flg))

            '����\�茎��
            tbxKaisyuuYoteiGessuu.Text = TrimNull(dtSeikyuuSakiHinagata.Item(0).kaisyuu_yotei_gessuu)
            '����\���
            tbxKaisyuuYoteiDate.Text = TrimNull(dtSeikyuuSakiHinagata.Item(0).kaisyuu_yotei_date)
            '�������K����
            tbxSeikyuusyoHittykDate.Text = TrimNull(dtSeikyuuSakiHinagata.Item(0).seikyuusyo_hittyk_date)
            '���1���1
            SetDropSelect(ddlKaisyuu1Syubetu1, TrimNull(dtSeikyuuSakiHinagata.Item(0).kaisyuu1_syubetu1))
            '���1����1
            tbxKaisyuu1Wariai1.Text = TrimNull(dtSeikyuuSakiHinagata.Item(0).kaisyuu1_wariai1)
            '���1��`�T�C�g����
            tbxKaisyuu1TegataSiteGessuu.Text = TrimNull(dtSeikyuuSakiHinagata.Item(0).kaisyuu1_tegata_site_gessuu)
            '���1��`�T�C�g��
            tbxKaisyuu1TegataSiteDate.Text = TrimNull(dtSeikyuuSakiHinagata.Item(0).kaisyuu1_tegata_site_date)
            '���1�������p��
            SetDropSelect(ddlKaisyuu1SeikyuusyoYousi, TrimNull(dtSeikyuuSakiHinagata.Item(0).kaisyuu1_seikyuusyo_yousi))
            '���1���2
            SetDropSelect(ddlKaisyuu1Syubetu2, TrimNull(dtSeikyuuSakiHinagata.Item(0).kaisyuu1_syubetu2))
            '���1����2
            tbxKaisyuu1Wariai2.Text = TrimNull(dtSeikyuuSakiHinagata.Item(0).kaisyuu1_wariai2)
            '���1���3
            SetDropSelect(ddlKaisyuu1Syubetu3, TrimNull(dtSeikyuuSakiHinagata.Item(0).kaisyuu1_syubetu3))
            '���1����3
            tbxKaisyuu1Wariai3.Text = TrimNull(dtSeikyuuSakiHinagata.Item(0).kaisyuu1_wariai3)
            '������E�z
            tbxKaisyuuKyoukaigaku.Text = AddComa(dtSeikyuuSakiHinagata.Item(0).kaisyuu_kyoukaigaku)
            '���2���1
            SetDropSelect(ddlKaisyuu2Syubetu1, TrimNull(dtSeikyuuSakiHinagata.Item(0).kaisyuu2_syubetu1))
            '���2����1
            tbxKaisyuu2Wariai1.Text = TrimNull(dtSeikyuuSakiHinagata.Item(0).kaisyuu2_wariai1)
            '���2��`�T�C�g����
            tbxKaisyuu2TegataSiteGessuu.Text = TrimNull(dtSeikyuuSakiHinagata.Item(0).kaisyuu2_tegata_site_gessuu)
            '���2��`�T�C�g��
            tbxKaisyuu2TegataSiteDate.Text = TrimNull(dtSeikyuuSakiHinagata.Item(0).kaisyuu2_tegata_site_date)
            '���2�������p��
            SetDropSelect(ddlKaisyuu2SeikyuusyoYousi, TrimNull(dtSeikyuuSakiHinagata.Item(0).kaisyuu2_seikyuusyo_yousi))
            '���2���2
            SetDropSelect(ddlKaisyuu2Syubetu2, TrimNull(dtSeikyuuSakiHinagata.Item(0).kaisyuu2_syubetu2))
            '���2����2
            tbxKaisyuu2Wariai2.Text = TrimNull(dtSeikyuuSakiHinagata.Item(0).kaisyuu2_wariai2)
            '���2���3
            SetDropSelect(ddlKaisyuu2Syubetu3, TrimNull(dtSeikyuuSakiHinagata.Item(0).kaisyuu2_syubetu3))
            '���2����3
            tbxKaisyuu2Wariai3.Text = TrimNull(dtSeikyuuSakiHinagata.Item(0).kaisyuu2_wariai3)

            '2013/5/29 �������̋�s�x�X�R�[�h�����ɔ���Earth���C -----------------------��
            '��s�x�X�R�[�h
            SetDropSelect(Me.ddlGinkouSitenCd, TrimNull(dtSeikyuuSakiHinagata.Item(0).ginkou_siten_cd))
            '2013/5/29 �������̋�s�x�X�R�[�h�����ɔ���Earth���C -----------------------��

            If Me.hidSeikyuuKbn.Value <> "" Then
                SetDropSelect(Me.ddlSeikyuuSakiKbn, Me.hidSeikyuuKbn.Value)
            End If
            Me.hidSeikyuuKbn.Value = ""

            Me.hidConfirm.Value = ""
        Else
            If Me.hidSeikyuuKbn.Value <> "" Then
                SetDropSelect(Me.ddlSeikyuuSakiKbn, Me.hidSeikyuuKbn.Value)
            End If
            Me.hidSeikyuuKbn.Value = ""

            Me.hidConfirm.Value = ""

            '������o�^���`ddlSeikyuuSakiTourokuHinagata
            ddlSeikyuuSakiTourokuHinagata.Items.Clear()
            SetSeikyuuSakiTourokuHinagata(ddlSeikyuuSakiTourokuHinagata)

            '�f�[�^��������
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('���̑I��������{���͍폜����܂���');", True)
        End If
    End Sub

    ''' <summary>
    ''' DDL�̏����ݒ�
    ''' </summary>
    ''' <remarks></remarks>
    Sub SetDdlListInf()
        '���h���b�v�_�E�����X�g�ݒ聫
        Dim ddlist As ListItem

        '������敪ddlSeikyuuSaki_Kbn
        SetKakutyouMeisyou(ddlSeikyuuSaki_Kbn, "1")
        'ddlist = New ListItem
        'ddlist.Text = ""
        'ddlist.Value = ""
        'ddlSeikyuuSaki_Kbn.Items.Add(ddlist)
        'ddlist = New ListItem
        'ddlist.Text = "�����X"
        'ddlist.Value = "0"
        'ddlSeikyuuSaki_Kbn.Items.Add(ddlist)
        'ddlist = New ListItem
        'ddlist.Text = "�������"
        'ddlist.Value = "1"
        'ddlSeikyuuSaki_Kbn.Items.Add(ddlist)
        'ddlist = New ListItem
        'ddlist.Text = "�c�Ə�"
        'ddlist.Value = "2"
        'ddlSeikyuuSaki_Kbn.Items.Add(ddlist)

        '������敪ddlSeikyuuSakiKbn
        ddlist = New ListItem
        ddlist.Text = ""
        ddlist.Value = ""
        ddlSeikyuuSakiKbn.Items.Add(ddlist)
        ddlist = New ListItem
        ddlist.Text = "�����X"
        ddlist.Value = "0"
        ddlSeikyuuSakiKbn.Items.Add(ddlist)
        ddlist = New ListItem
        ddlist.Text = "�������"
        ddlist.Value = "1"
        ddlSeikyuuSakiKbn.Items.Add(ddlist)
        ddlist = New ListItem
        ddlist.Text = "�c�Ə�"
        ddlist.Value = "2"
        ddlSeikyuuSakiKbn.Items.Add(ddlist)

        '������o�^���`ddlSeikyuuSakiTourokuHinagata
        SetSeikyuuSakiTourokuHinagata(ddlSeikyuuSakiTourokuHinagata)

        '�������󎚕�����ddlSeikyuuSyoInjiBukkenMei
        ddlist = New ListItem
        ddlist.Text = "�{�喼"
        ddlist.Value = "0"
        ddlSeikyuuSyoInjiBukkenMei.Items.Add(ddlist)
        ddlist = New ListItem
        ddlist.Text = "�󒍎�������"
        ddlist.Value = "1"
        ddlSeikyuuSyoInjiBukkenMei.Items.Add(ddlist)

        '���H�������^�C�~���OddlTykKojSeikyuuTimingFlg
        ddlist = New ListItem
        ddlist.Text = "�d�l�m�F���ɔ���ŋN�Z"
        ddlist.Value = "0"
        ddlTykKojSeikyuuTimingFlg.Items.Add(ddlist)
        ddlist = New ListItem
        ddlist.Text = "���H���񒅓����͎��ɔ���ŋN�Z"
        ddlist.Value = "1"
        ddlTykKojSeikyuuTimingFlg.Items.Add(ddlist)

        '���1���1ddlKaisyuu1Syubetu1
        SetKakutyouMeisyou(ddlKaisyuu1Syubetu1, "2")

        '���1�������p��ddlKaisyuu1SeikyuusyoYousi
        SetKakutyouMeisyou(ddlKaisyuu1SeikyuusyoYousi, "3")

        '���1���2ddlKaisyuu1Syubetu2
        SetKakutyouMeisyou(ddlKaisyuu1Syubetu2, "2")

        '���1���3ddlKaisyuu1Syubetu3
        SetKakutyouMeisyou(ddlKaisyuu1Syubetu3, "2")

        '���2���1ddlKaisyuu2Syubetu1
        SetKakutyouMeisyou(ddlKaisyuu2Syubetu1, "2")

        '���2�������p��ddlKaisyuu2SeikyuusyoYousi
        SetKakutyouMeisyou(ddlKaisyuu2SeikyuusyoYousi, "3")

        '���2���2ddlKaisyuu2Syubetu2
        SetKakutyouMeisyou(ddlKaisyuu2Syubetu2, "2")

        '���2���3ddlKaisyuu2Syubetu3
        SetKakutyouMeisyou(ddlKaisyuu2Syubetu3, "2")

        '===========2012/05/15 �ԗ� 407553�̑Ή� �ǉ���=========================
        '���U�n�j�t���O
        Me.ddlKutiburiOkFlg.Items.Clear()
        '�K�p����
        Me.ddlKyouryokuKaihiJigou.SelectedIndex = 0

        '�f�[�^���擾����
        Dim dtList As New Data.DataTable
        dtList = SeikyuuSakiMasterBL.GetKutiburiOkFlg()

        '�f�[�^��bound
        Me.ddlKutiburiOkFlg.DataValueField = "code"
        Me.ddlKutiburiOkFlg.DataTextField = "meisyou"
        Me.ddlKutiburiOkFlg.DataSource = dtList
        Me.ddlKutiburiOkFlg.DataBind()

        '�擪�s
        Me.ddlKutiburiOkFlg.Items.Insert(0, New ListItem(String.Empty, String.Empty))
        '===========2012/05/15 �ԗ� 407553�̑Ή� �ǉ���=========================

        '2013/5/29 �������̋�s�x�X�R�[�h�����ɔ���Earth���C -----------------------��
        '��s�x�X�R�[�h
        Me.ddlGinkouSitenCd.Items.Clear()

        '�f�[�^���擾����
        Dim dtGinkouSitenCd As New Data.DataTable
        dtGinkouSitenCd = SeikyuuSakiMasterBL.GetGinkouSitenCd()

        '�f�[�^��bound
        Me.ddlGinkouSitenCd.DataValueField = "code"
        Me.ddlGinkouSitenCd.DataTextField = "meisyou"
        Me.ddlGinkouSitenCd.DataSource = dtGinkouSitenCd
        Me.ddlGinkouSitenCd.DataBind()

        '�擪�s
        Me.ddlGinkouSitenCd.Items.Insert(0, New ListItem(String.Empty, String.Empty))
        '�f�t�H���g�F804
        Me.ddlGinkouSitenCd.SelectedValue = "804"
        '2013/5/29 �������̋�s�x�X�R�[�h�����ɔ���Earth���C -----------------------��

    End Sub

    ''' <summary>
    ''' ������o�^���`�}�X�^�h���b�v�_�E�����X�g�ݒ�
    ''' </summary>
    ''' <param name="ddl">�h���b�v�_�E�����X�g</param>
    ''' <remarks></remarks>
    Sub SetSeikyuuSakiTourokuHinagata(ByVal ddl As DropDownList)
        Dim dtTable As New DataTable
        Dim intCount As Integer = 0
        dtTable = SeikyuuSakiMasterBL.SelSeikyuuSakiTourokuHinagataInfo()

        Dim ddlist As New ListItem
        ddlist.Text = ""
        ddlist.Value = ""
        ddl.Items.Add(ddlist)
        For intCount = 0 To dtTable.Rows.Count - 1
            ddlist = New ListItem
            ddlist.Text = TrimNull(dtTable.Rows(intCount).Item("seikyuu_saki_brc")) & "�F" & TrimNull(dtTable.Rows(intCount).Item("hyouji_naiyou"))
            ddlist.Value = TrimNull(dtTable.Rows(intCount).Item("seikyuu_saki_brc"))
            ddl.Items.Add(ddlist)
        Next
    End Sub

    ''' <summary>
    ''' �g�����̃}�X�^m_kakutyou_meisyou
    ''' </summary>
    ''' <param name="ddl">�h���b�v�_�E�����X�g</param>
    ''' <param name="strSyubetu">���̎��</param>
    ''' <remarks></remarks>
    Sub SetKakutyouMeisyou(ByVal ddl As DropDownList, ByVal strSyubetu As String)
        Dim dtTable As New DataTable
        Dim intCount As Integer = 0
        dtTable = SeikyuuSakiMasterBL.SelKakutyouInfo(strSyubetu)

        Dim ddlist As New ListItem
        ddlist.Text = ""
        ddlist.Value = ""
        ddl.Items.Add(ddlist)
        For intCount = 0 To dtTable.Rows.Count - 1
            ddlist = New ListItem
            ddlist.Text = TrimNull(dtTable.Rows(intCount).Item("meisyou"))
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

    ''' <summary>DDL�ݒ�</summary>
    Sub SetDropSelect(ByVal ddl As DropDownList, ByVal strValue As String)

        If ddl.Items.FindByValue(strValue) IsNot Nothing Then
            ddl.SelectedValue = strValue
        Else
            ddl.SelectedIndex = 0
        End If
    End Sub

    ''' <summary>
    ''' ���ڃf�[�^�ɃR�}��ǉ�
    ''' </summary>
    ''' <param name="kekka">���z</param>
    Protected Function AddComa(ByVal kekka As String) As String
        If TrimNull(kekka) = "" Then
            Return ""
        Else
            Return CInt(kekka).ToString("###,###,##0")
        End If

    End Function

    ''' <summary>
    ''' ���ו��������挟��������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOK.Click
        '���׃f�[�^�擾
        GetMeisaiData1(tbxSeikyuuSakiCd.Text, tbxSeikyuuSakiBrc.Text, ddlSeikyuuSakiKbn.SelectedValue)
    End Sub

    ''' <summary>
    ''' ���׃f�[�^���擾
    ''' </summary>
    ''' <param name="TyousaKaisya_Cd"></param>
    ''' <param name="btn"></param>
    ''' <remarks></remarks>
    Sub GetMeisaiData1(ByVal TyousaKaisya_Cd As String, _
                      ByVal TyousaKaisyaCd As String, _
                      ByVal JigyousyoCd As String, _
                      Optional ByVal btn As String = "")

        Dim strErr As String = ""
        Dim dtSeikyuuSakiDataSet As New Itis.Earth.DataAccess.SeikyuuSakiDataSet.m_seikyuu_sakiDataTable
        dtSeikyuuSakiDataSet = SeikyuuSakiMasterBL.SelSeikyuuSakiInfo(TyousaKaisya_Cd, TyousaKaisyaCd, JigyousyoCd)

        If dtSeikyuuSakiDataSet.Rows.Count = 1 Then
            With dtSeikyuuSakiDataSet.Item(0)
                '���
                chkTorikesi.Checked = IIf(.torikesi = "0", False, True)
                '������敪
                SetDropSelect(ddlSeikyuuSakiKbn, TrimNull(.seikyuu_saki_kbn))
                '������R�[�h
                tbxSeikyuuSakiCd.Text = TrimNull(.seikyuu_saki_cd)
                '������}��
                tbxSeikyuuSakiBrc.Text = TrimNull(.seikyuu_saki_brc)
                '�����於
                tbxSeikyuuSakiMei.Text = TrimNull(.seikyuu_saki_mei)
                'If btn = "btnSearch" Then
                '    '�����於
                '    tbxSeikyuuSaki_Mei.Text = TrimNull(.seikyuu_saki_mei)
                'End If

                '20100925�@�����R�[�h�A����於�@�ǉ��@�n���R������
                '�����R�[�h
                tbxNayoseCd.Text = TrimNull(.nayose_saki_cd)
                '����於
                'tbxNayoseMei.Text = TrimNull(.nayose_saki_name1)
                If tbxNayoseCd.Text <> String.Empty Then
                    Dim dtNayoseSakiTable As New Data.DataTable
                    dtNayoseSakiTable = SeikyuuSakiMasterBL.SelNayoseSakiInfo(tbxNayoseCd.Text.Trim)
                    If dtNayoseSakiTable.Rows.Count > 0 Then
                        tbxNayoseMei.Text = TrimNull(dtNayoseSakiTable.Rows(0).Item("nayose_saki_name1"))
                    Else
                        tbxNayoseMei.Text = "�����R�[�h�@���o�^"
                    End If
                Else
                    tbxNayoseMei.Text = ""
                End If
                '20100925�@�����R�[�h�A����於�@�ǉ��@�n���R������

                '20100712�@�d�l�ύX�@�ǉ��@�n���R������
                '�X�֔ԍ�
                tbxYuubinNo.Text = TrimNull(.skysy_soufu_yuubin_no)
                '�Z���P
                tbxJyuusyo1.Text = TrimNull(.skysy_soufu_jyuusyo1)
                '�d�b�ԍ�
                tbxTelNo.Text = TrimNull(.skysy_soufu_tel_no)
                '�Z���Q
                tbxJyuusyo2.Text = TrimNull(.skysy_soufu_jyuusyo2)
                'FAX�ԍ�
                tbxFaxNo.Text = TrimNull(.skysy_soufu_fax_no)
                '20100712�@�d�l�ύX�@�ǉ��@�n���R������

                '�S����
                tbxTantousya.Text = TrimNull(.tantousya_mei)
                '�������󎚕�����
                SetDropSelect(ddlSeikyuuSyoInjiBukkenMei, TrimNull(.seikyuusyo_inji_bukken_mei_flg))
                '�V��v���Ə�
                tbxSkkJigyousyoCd.Text = TrimNull(.skk_jigyousyo_cd)
                '�V��v���Ə�
                tbxSkkJigyousyoMei.Text = TrimNull(.skk_jigyousyo_mei)
                '�������ߓ�
                tbxSeikyuuSimeDate.Text = TrimNull(.seikyuu_sime_date)
                '����������ߓ�
                tbxSenpouSeikyuuSimeDate.Text = TrimNull(.senpou_seikyuu_sime_date)
                ' �������K����
                tbxSeikyuusyoHittykDate.Text = TrimNull(.seikyuusyo_hittyk_date)

                '==================2011/06/16 �ԗ� �폜 �J�n��==========================
                ''��������R�[�h
                'tbxKyuuSeikyuuSakiCd.Text = TrimNull(.kyuu_seikyuu_saki_cd)
                '==================2011/06/16 �ԗ� �폜 �I����==========================

                '==================2011/06/16 �ԗ�  �ǉ� �J�n��========================== 
                '���Z����x���߃t���O
                Me.ddlKessanjiNidosimeFlg.SelectedValue = IIf(TrimNull(.kessanji_nidosime_flg).Equals("1"), "1", "0")
                '==================2011/06/16 �ԗ�  �ǉ� �I����==========================

                '����\�茎��
                tbxKaisyuuYoteiGessuu.Text = TrimNull(.kaisyuu_yotei_gessuu)
                '����\���
                tbxKaisyuuYoteiDate.Text = TrimNull(.kaisyuu_yotei_date)
                '���������ԍ�
                tbxNyuukinKouzaNo.Text = TrimNull(.nyuukin_kouza_no)
                '���E�t���O
                chkSousaiFlg.Checked = IIf(.sousai_flg = "0", False, True)
                '���H�������^�C�~���O
                SetDropSelect(ddlTykKojSeikyuuTimingFlg, TrimNull(.tyk_koj_seikyuu_timing_flg))
                '���1��`�T�C�g����
                tbxKaisyuu1TegataSiteGessuu.Text = TrimNull(.kaisyuu1_tegata_site_gessuu)
                '���1��`�T�C�g��
                tbxKaisyuu1TegataSiteDate.Text = TrimNull(.kaisyuu1_tegata_site_date)
                '���1�������p��
                SetDropSelect(ddlKaisyuu1SeikyuusyoYousi, TrimNull(.kaisyuu1_seikyuusyo_yousi))
                '���1���1
                SetDropSelect(ddlKaisyuu1Syubetu1, TrimNull(.kaisyuu1_syubetu1))
                '���1����1
                tbxKaisyuu1Wariai1.Text = TrimNull(.kaisyuu1_wariai1)
                '���1���2
                SetDropSelect(ddlKaisyuu1Syubetu2, TrimNull(.kaisyuu1_syubetu2))
                '���1����2
                tbxKaisyuu1Wariai2.Text = TrimNull(.kaisyuu1_wariai2)
                '���1���3
                SetDropSelect(ddlKaisyuu1Syubetu3, TrimNull(.kaisyuu1_syubetu3))
                '���1����1
                tbxKaisyuu1Wariai3.Text = TrimNull(.kaisyuu1_wariai3)
                '������E�z
                tbxKaisyuuKyoukaigaku.Text = AddComa(.kaisyuu_kyoukaigaku)

                '���2��`�T�C�g����
                tbxKaisyuu2TegataSiteGessuu.Text = TrimNull(.kaisyuu2_tegata_site_gessuu)
                '���2��`�T�C�g��
                tbxKaisyuu2TegataSiteDate.Text = TrimNull(.kaisyuu2_tegata_site_date)
                '���2�������p��
                SetDropSelect(ddlKaisyuu2SeikyuusyoYousi, TrimNull(.kaisyuu2_seikyuusyo_yousi))
                '���2���1
                SetDropSelect(ddlKaisyuu2Syubetu1, TrimNull(.kaisyuu2_syubetu1))
                '���2����1
                tbxKaisyuu2Wariai1.Text = TrimNull(.kaisyuu2_wariai1)
                '���2���2
                SetDropSelect(ddlKaisyuu2Syubetu2, TrimNull(.kaisyuu2_syubetu2))
                '���2����2
                tbxKaisyuu2Wariai2.Text = TrimNull(.kaisyuu2_wariai2)
                '���2���3
                SetDropSelect(ddlKaisyuu2Syubetu3, TrimNull(.kaisyuu2_syubetu3))
                '���2����1
                tbxKaisyuu2Wariai3.Text = TrimNull(.kaisyuu2_wariai3)

                '2013/5/29 �������̋�s�x�X�R�[�h�����ɔ���Earth���C -----------------------��
                '��s�x�X�R�[�h
                SetDropSelect(ddlGinkouSitenCd, TrimNull(.ginkou_siten_cd))
                '2013/5/29 �������̋�s�x�X�R�[�h�����ɔ���Earth���C -----------------------��

                '===========2012/05/14 �ԗ� 407553�̑Ή� �ǉ���=====================
                '������v���Ӑ溰�� 
                Me.tbxTougouKaikeiTokusakiCd.Text = TrimNull(.tougou_tokuisaki_cd)
                '���U�n�j�t���O
                SetDropSelect(ddlKutiburiOkFlg, TrimNull(.koufuri_ok_flg))
                '���͉��_�K�p����
                Me.ddlKyouryokuKaihiJigou.SelectedValue = dtSeikyuuSakiDataSet.Item(0).kyouryoku_kaihi_taisyou

                '���S���͉��_�~
                If TrimNull(.anzen_kaihi_en).Equals(String.Empty) Then
                    Me.tbxAnzenKyouryokuKaihi1.Text = String.Empty
                Else
                    Me.tbxAnzenKyouryokuKaihi1.Text = FormatNumber(TrimNull(.anzen_kaihi_en), 0)
                End If
                '���S���͉��_��
                Me.tbxAnzenKyouryokuKaihi2.Text = TrimNull(.anzen_kaihi_wari)
                '���l
                Me.tbxBikou.Text = TrimNull(.bikou)

                '�u�̔ԁv�{�^�����Z�b�g����
                Call Me.SetBtnSaiban()
                '===========2012/05/14 �ԗ� 407553�̑Ή� �ǉ���=====================

                'hidUPDTime.Value = TrimNull(.upd_datetime)
                'UpdatePanelA.Update()
            End With

            'Me.hidConfirm.Value = ""

            If Not blnBtn Then
                btnSyuusei.Enabled = False
                btnTouroku.Enabled = False
            Else
                btnSyuusei.Enabled = False
                btnTouroku.Enabled = True
            End If

            'tbxSeikyuuSakiCd.Attributes.Add("readonly", "true;")
            'tbxSeikyuuSakiBrc.Attributes.Add("readonly", "true;")
            'ddlSeikyuuSakiKbn.Attributes.Add("disabled", "true;")

            'Me.btnKensakuSeikyuuSaki.Enabled = False
        Else
            MeisaiClear()
            Me.tbxSeikyuuSaki_Mei.Text = ""
            strErr = "alert('" & Messages.Instance.MSG2034E & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strErr, True)
        End If

    End Sub

    ''' <summary>
    ''' �߂�{�^���̏���
    ''' </summary>
    ''' <param name="sender">system.Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        If ViewState.Item("Flg") = "close" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "aaaa", "window.close();", True)
        Else
            Server.Transfer("MasterMainteMenu.aspx")
        End If
    End Sub

    ''' <summary>
    ''' �u�̔ԁv�{�^�����Z�b�g����
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2012/05/15 �ԗ� 407553�̑Ή� �ǉ�</history>
    Private Sub SetBtnSaiban()

        '������v���Ӑ溰��
        Dim strTougouKaikeiTokusakiCd As String
        strTougouKaikeiTokusakiCd = Me.tbxTougouKaikeiTokusakiCd.Text.Trim

        If Not strTougouKaikeiTokusakiCd.Equals(String.Empty) Then
            Me.btnSaiban.Attributes.Add("disabled", "true")
        Else
            Me.btnSaiban.Attributes.Remove("disabled")
        End If

    End Sub

    ''' <summary>
    ''' �u�̔ԁv�{�^��������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>2012/05/15 �ԗ� 407553�̑Ή� �ǉ�</history>
    Private Sub btnSaiban_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaiban.Click

        '������v���Ӑ溰��
        Me.tbxTougouKaikeiTokusakiCd.Text = SeikyuuSakiMasterBL.GetMaxKutiburiOkFlg()

        '�u�̔ԁv�{�^�����Z�b�g����
        Call Me.SetBtnSaiban()

    End Sub
End Class
Imports Itis.Earth.BizLogic
Imports Itis.Earth.DataAccess

Partial Public Class KihonJyouhouInput
    Inherits System.Web.UI.Page

    '�ҏW���ڔ񊈐��A�����ݒ�Ή��@20180905�@�������@�Ή��@��
    'salesforce����_�ҏW�񊈐��t���O �擾
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

    '�ҏW���ڔ񊈐��A�����ݒ肷��
    Public Sub SetKassei()

        ViewState("Iskassei") = Nothing
        Dim kbn As String

        If tbxKyoutuKubun.Visible AndAlso tbxKyoutuKubun.Text <> "" Then
            kbn = tbxKyoutuKubun.Text
        Else
            If comdrp.Visible Then
                kbn = Me.comdrp.SelectedValue
            Else
                kbn = Me.common_drop1.SelectedValue
            End If
        End If


        Dim itKassei As Boolean = Iskassei(tbxKyoutuKameitenCd.Text, kbn)


        tbxKyoutuKameitenMei2.ReadOnly = Not itKassei
        tbxKyoutuKameitenMei2.CssClass = GetCss(itKassei, tbxKyoutuKameitenMei2.CssClass)


        tbxKyoutukakeMei2.ReadOnly = Not itKassei
        tbxKyoutukakeMei2.CssClass = GetCss(itKassei, tbxKyoutukakeMei2.CssClass)
        tbxKeiretuCd.ReadOnly = Not itKassei
        tbxKeiretuCd.CssClass = GetCss(itKassei, tbxKeiretuCd.CssClass)
        btnKeiretuCd.Enabled = itKassei


        tbxKyoutuKameitenMei2.Text = ""
        tbxKyoutukakeMei2.Text = ""
        tbxKeiretuCd.Text = ""
        tbxKeiretuMei.Text = ""

    End Sub

    Public Function GetCss(ByVal itKassei As Boolean, ByVal css As String)
        If itKassei Then
            Return Microsoft.VisualBasic.Strings.Replace(css, "readOnly", "", 1, -1, CompareMethod.Text)
        Else
            Return css & " readOnly"
        End If
    End Function

    '�ҏW���ڔ񊈐��A�����ݒ�Ή��@20180905�@�������@�Ή��@��

    ''' <summary>�����X����V�K�o�^����</summary>
    ''' <remarks>�����X���V�K�o�^��񋟂���</remarks>
    ''' <history>
    ''' <para>2009/07/15�@�t��(��A���V�X�e����)�@�V�K�쐬</para>
    ''' </history>
    Private kihonJyouhouInputLogic As New KihonJyouhouInputLogic
    Private commonCheck As New CommonCheck

    ''' <summary>�y�[�W���b�h</summary>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '�ҏW���ڔ񊈐��A�����ݒ�Ή��@20180905�@�������@�Ή��@��
        comdrp.OnChange = "SetKassei()"
        common_drop1.OnChange = "SetKassei()"
        '�ҏW���ڔ񊈐��A�����ݒ�Ή��@20180905�@�������@�Ή��@��

        '�����`�F�b�N
        Dim user_info As New LoginUserInfo
        Dim ninsyou As New Ninsyou()
        Dim jBn As New Jiban '�n�Չ�ʋ��ʃN���X
        ' ���[�U�[��{�F��
        jBn.userAuth(user_info)
        If ninsyou.GetUserID() = String.Empty Then
            Context.Items("strFailureMsg") = Messages.Instance.MSG2024E
            Server.Transfer("CommonErr.aspx")
        End If
        If user_info Is Nothing Then
            Context.Items("strFailureMsg") = Messages.Instance.MSG2020E
            Server.Transfer("CommonErr.aspx")
        Else
            If user_info.EigyouMasterKanriKengen <> -1 Then
                Context.Items("strFailureMsg") = Messages.Instance.MSG2020E
                Server.Transfer("CommonErr.aspx")
            End If
        End If

        WindowOnload()

        If Not IsPostBack Then
            '�Q�Ɨ����Ǘ��e�[�u����o�^����B
            CommonCheck.SetURL(Me, ninsyou.GetUserID())
            CType(Me.common_drop1.FindControl("ddlCommonDrop"), DropDownList).Focus()

            '==============2012/03/21 �ԗ� 405721�Č��̑Ή� �ǉ���=============================
            '�u����vddl���Z�b�g����
            Call Me.SetTorikesiDDL()
            '==============2012/03/21 �ԗ� 405721�Č��̑Ή� �ǉ���=============================
            '�ҏW���ڔ񊈐��A�����ݒ�Ή��@20180905�@�������@�Ή��@��
            SetKassei()
            '�ҏW���ڔ񊈐��A�����ݒ�Ή��@20180905�@�������@�Ή��@��
        Else
            If hidTourokuFlg.Value = "1" Then
                hidTourokuFlg.Value = String.Empty
                InsKameitenInfo()
            End If
        End If
        '==================2012/03/27 �ԗ� 405721�Č��̑Ή� �ǉ���=========================
        Me.ddlTorikesi.Attributes.Add("onChange", "fncSetTorikesi();")
        '�F���Z�b�g����
        Call Me.SetColor()
        '==================2012/03/27 �ԗ� 405721�Č��̑Ή� �ǉ���=========================

        Me.btnKameitenSearch.Attributes.Add("onclick", "return fncNyuuryokuCheck('" & common_drop1.DdlClientID & "');")

        'Javascript�쐬
        MakeJavascript()

        btnHansokuSina.Attributes.Add("onclick", "return fncHansokuSina();")
        tbxBirudaNo.Attributes.Add("onBlur", "fncToUpper(this);")
        'tbxKeiretuCd.Attributes.Add("onBlur", "fncToUpper(this);")
        'tbxEigyousyoCd.Attributes.Add("onBlur", "fncToUpper(this);")
        tbxThKasiCd.Attributes.Add("onBlur", "fncToUpper(this);")
        tbxKyoutukakeMei1.Attributes.Add("onBlur", "fncTokomozi(this);")
        tbxKyoutukakeMei2.Attributes.Add("onBlur", "fncTokomozi(this);")


    End Sub

    ''' <summary>���ړ��̓{�^�����N���b�N��</summary>
    Protected Sub btnTyokusetuNyuuryoku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTyokusetuNyuuryoku.Click
        comdrp.Visible = True
        tbxKyoutuKubun.Visible = False
        tbxKubunMei.Visible = False
        tbxKyoutuKameitenCd.Text = String.Empty
        tbxKyoutuKameitenCd.ReadOnly = False
        tbxKyoutuKameitenCd.CssClass = "codeNumber"
        tbxKyoutuKameitenCd.TabIndex = 0
        common_drop1.SelectedValue = String.Empty
        common_drop1.Enabled = False
        common_drop1.CssClass = "readOnly"
        tbxKameitenCd.Text = String.Empty
        tbxKameitenCd.Enabled = False
        tbxKameitenCd.CssClass = "readOnly"
        tbxBirudaNo.Text = String.Empty
        tbxBirudaMei.Text = String.Empty
        btnKameitenSearch.Enabled = False
        btnTyokusetuNyuuryoku.Visible = False
        btnTyokusetuNyuuryokuTyuusi.Visible = True
        btnTouroku.Enabled = True
        btnTouroku.Attributes.Add("onclick", "if(fncNyuuryokuCheck('" & comdrp.DdlClientID & "')==true){}else{return false}")
    End Sub

    ''' <summary>���ړ��͒��~�{�^�����N���b�N��</summary>
    Private Sub btnTyokusetuNyuuryokuTyuusi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTyokusetuNyuuryokuTyuusi.Click
        comdrp.SelectedValue = String.Empty
        comdrp.Visible = False
        tbxKyoutuKubun.Visible = True
        tbxKubunMei.Visible = True
        tbxKyoutuKubun.Text = String.Empty
        tbxKubunMei.Text = String.Empty
        tbxKyoutuKameitenCd.Text = String.Empty
        tbxKyoutuKameitenCd.ReadOnly = True
        tbxKyoutuKameitenCd.CssClass = "readOnly"
        tbxKyoutuKameitenCd.TabIndex = -1
        common_drop1.Enabled = True
        common_drop1.CssClass = String.Empty
        tbxKameitenCd.Enabled = True
        tbxKameitenCd.CssClass = "codeNumber"
        btnKameitenSearch.Enabled = True
        btnTyokusetuNyuuryoku.Visible = True
        btnTyokusetuNyuuryokuTyuusi.Visible = False
        btnTouroku.Enabled = False
        btnTouroku.Attributes.Add("onclick", "")
    End Sub

    ''' <summary>�����{�^�����N���b�N��</summary>
    Private Sub btnKameitenSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKameitenSearch.Click

        SetKassei()

        Dim csScript As New StringBuilder
        Dim commonCheck As New CommonCheck

        '���̓`�F�b�N
        With csScript
            If Me.tbxKameitenCd.Text <> String.Empty Then
                .Append(commonCheck.CheckHankaku(Me.tbxKameitenCd.Text, "�����X�R�[�h"))
                If csScript.ToString = String.Empty AndAlso Me.tbxKameitenCd.Text.Length <> 5 Then
                    .Append(String.Format(Messages.Instance.MSG2004E, "�����X�R�[�h", "5").ToString)
                End If
            End If
        End With
        If csScript.ToString <> String.Empty Then
            ShowMsg(csScript.ToString, Me.tbxKameitenCd.ClientID)
            Exit Sub
        End If

        '�����X�R�[�h�`�F�b�N
        If tbxKameitenCd.Text = String.Empty Then
            '===========��2012/11/19 �ԗ� 407597�̑Ή� �C����==================
            'Dim dtKameitenCd As KihonJyouhouInputDataSet.KameitenCdTableDataTable = kihonJyouhouInputLogic.GetMaxKameitenCd(common_drop1.SelectedValue)
            ''�����X�R�[�h�̍ő�l�����݂̏ꍇ
            'If dtKameitenCd.Rows.Count <> 0 AndAlso dtKameitenCd(0).kameiten_cd.ToString <> String.Empty Then
            '    '�����X�R�[�h�̍ő�l=99999�̏ꍇ
            '    If dtKameitenCd(0).kameiten_cd.ToString = "99999" Then
            '        tbxKyoutuKubun.Text = String.Empty
            '        tbxKubunMei.Text = String.Empty
            '        tbxKyoutuKameitenCd.Text = String.Empty
            '        tbxBirudaNo.Text = String.Empty
            '        btnTouroku.Enabled = False
            '        ShowMsg(Messages.Instance.MSG2025E, tbxKameitenCd.ClientID)
            '        Exit Sub
            '    End If

            '    Dim intKameitenCd As String = Right("00000" & (CInt(dtKameitenCd(0).kameiten_cd) + 1).ToString, 5)

            '    '�ݒ肵�������X�R�[�h�𑶍݃`�F�b�N
            '    Dim dtKameitenCd1 As KihonJyouhouInputDataSet.KameitenCdTableDataTable
            '    dtKameitenCd1 = kihonJyouhouInputLogic.GetKameitenCd(String.Empty, intKameitenCd)

            '    Do While dtKameitenCd1.Rows.Count <> 0
            '        intKameitenCd = Right("00000" & (CInt(intKameitenCd) + 1).ToString, 5)
            '        If intKameitenCd = "99999" Then
            '            tbxKyoutuKubun.Text = String.Empty
            '            tbxKubunMei.Text = String.Empty
            '            tbxKyoutuKameitenCd.Text = String.Empty
            '            tbxBirudaNo.Text = String.Empty
            '            btnTouroku.Enabled = False
            '            ShowMsg(Messages.Instance.MSG2025E, tbxKameitenCd.ClientID)
            '            Exit Sub
            '        End If
            '        dtKameitenCd1 = kihonJyouhouInputLogic.GetKameitenCd(String.Empty, intKameitenCd)
            '    Loop
            '    tbxKyoutuKameitenCd.Text = intKameitenCd
            'Else
            '    tbxKyoutuKubun.Text = String.Empty
            '    tbxKubunMei.Text = String.Empty
            '    tbxKyoutuKameitenCd.Text = String.Empty
            '    tbxBirudaNo.Text = String.Empty
            '    btnTouroku.Enabled = False
            '    ShowMsg(String.Format(Messages.Instance.MSG2026E, common_drop1.SelectedValue), tbxKameitenCd.ClientID)
            '    Exit Sub
            'End If

            '�Ώۋ敪�������X�R�[�h�̍ő�l�������X�̔Ԑݒ�͈̔͂���擾����
            Dim dtMaxKameitencd As New Data.DataTable
            dtMaxKameitencd = kihonJyouhouInputLogic.GetMaxKameitenCd1(common_drop1.SelectedValue)
            '�����X�R�[�h�̍ő�l
            Dim strkameitenCdMax As String = dtMaxKameitencd.Rows(0).Item("kameiten_cd_max").ToString.Trim
            '�����X�̔�_From
            Dim strKameitenSaibanFrom As String = dtMaxKameitencd.Rows(0).Item("kameiten_saiban_from").ToString.Trim
            '�����X�̔�_To
            Dim strKameitenSaibanTo As String = dtMaxKameitencd.Rows(0).Item("kameiten_saiban_to").ToString.Trim

            Dim intNewKameitenCd As Integer

            If strkameitenCdMax.Equals(String.Empty) Then
                '�����X�R�[�h�̍ő�l���Ȃ��ꍇ

                If strKameitenSaibanFrom.Equals(String.Empty) Then
                    '�����X�̔�_From���Ȃ��ꍇ
                    intNewKameitenCd = 1
                Else
                    '�����X�̔�_From������ꍇ
                    intNewKameitenCd = Convert.ToInt32(strKameitenSaibanFrom)
                End If
            Else
                '�����X�R�[�h�̍ő�l������ꍇ
                If Not commonCheck.CheckHankaku(strkameitenCdMax, String.Empty, "1").Equals(String.Empty) Then
                    '���p�����ł͂Ȃ��ꍇ

                    intNewKameitenCd = Convert.ToInt32(strKameitenSaibanFrom)
                Else
                    '���p�����̏ꍇ

                    If (Not strKameitenSaibanFrom.Equals(String.Empty)) AndAlso (Not strKameitenSaibanTo.Equals(String.Empty)) Then
                        '�����X�̔�_From�A�����X�̔�_To ������ꍇ

                        If (Convert.ToInt32(strKameitenSaibanFrom) <= Convert.ToInt32(strkameitenCdMax)) _
                            AndAlso (Convert.ToInt32(strkameitenCdMax) <= Convert.ToInt32(strKameitenSaibanTo)) Then
                            '�����X�̔�_From <= �ő�l <= �����X�̔�_To �̏ꍇ

                            'MAX(�����X�}�X�^.�����X�R�[�h)+1
                            intNewKameitenCd = Convert.ToInt32(strkameitenCdMax) + 1
                        Else
                            '�擾�����ő�l���@�敪�}�X�^.�����X�̔�_From�@And�@�敪�}�X�^.�����X�̔�_To�@�ɑ��݂��Ȃ��ꍇ

                            '�敪�}�X�^.�����X�̔�_From�@�̒l��o�^��������X�R�[�h�ɃZ�b�g����
                            intNewKameitenCd = Convert.ToInt32(strKameitenSaibanFrom)
                        End If
                    ElseIf (strKameitenSaibanFrom.Equals(String.Empty)) AndAlso (strKameitenSaibanTo.Equals(String.Empty)) Then
                        '�����X�̔�_From�A�����X�̔�_To ���Ȃ��ꍇ

                        'MAX(�����X�}�X�^.�����X�R�[�h)+1
                        intNewKameitenCd = Convert.ToInt32(strkameitenCdMax) + 1

                    ElseIf (Not strKameitenSaibanFrom.Equals(String.Empty)) AndAlso (strKameitenSaibanTo.Equals(String.Empty)) Then
                        '�����X�̔�_From������A�����X�̔�_To���Ȃ��ꍇ
                        If Convert.ToInt32(strkameitenCdMax) >= Convert.ToInt32(strKameitenSaibanFrom) Then

                            'MAX(�����X�}�X�^.�����X�R�[�h)+1
                            intNewKameitenCd = Convert.ToInt32(strkameitenCdMax) + 1
                        Else
                            intNewKameitenCd = Convert.ToInt32(strKameitenSaibanFrom)
                        End If

                    ElseIf (strKameitenSaibanFrom.Equals(String.Empty)) AndAlso (Not strKameitenSaibanTo.Equals(String.Empty)) Then
                        '�����X�̔�_From���Ȃ��A�����X�̔�_To������ꍇ

                        'MAX(�����X�}�X�^.�����X�R�[�h)+1
                        intNewKameitenCd = Convert.ToInt32(strkameitenCdMax) + 1
                    End If
                End If
            End If

            Dim blnErrorFlg As Boolean
            If strKameitenSaibanTo.Equals(String.Empty) Then
                blnErrorFlg = intNewKameitenCd >= 99999
            Else
                blnErrorFlg = intNewKameitenCd > Convert.ToInt32(strKameitenSaibanTo)
            End If

            If blnErrorFlg Then
                tbxKyoutuKubun.Text = String.Empty
                tbxKubunMei.Text = String.Empty
                tbxKyoutuKameitenCd.Text = String.Empty
                tbxBirudaNo.Text = String.Empty
                btnTouroku.Enabled = False

                '�G���[��\������
                ShowMsg(Messages.Instance.MSG2074E, tbxKameitenCd.ClientID)
                '�����I��
                Exit Sub
            End If

            tbxKyoutuKameitenCd.Text = Right(StrDup(5, "0") & intNewKameitenCd.ToString, 5)
            '===========��2012/11/19 �ԗ� 407597�̑Ή� �C����==================
            tbxKyoutuKubun.Text = common_drop1.SelectedValue
            tbxKubunMei.Text = common_drop1.SelectedText.Split("�F")(1)
            tbxBirudaNo.Text = tbxKyoutuKameitenCd.Text
            btnTouroku.Enabled = True
        Else
            Dim i As Integer = 5
            Dim bln As Boolean = False
            For i = tbxKameitenCd.Text.Length To 1 Step -1
                Dim j As Integer = Mid(tbxKameitenCd.Text, i, 1)
                If j = 0 Then
                    bln = True
                Else
                    Exit For
                End If
            Next
            Dim strCd As String = Mid(tbxKameitenCd.Text, 1, i) & Mid("99999", 1, 5 - i)

            If bln = True Then
                Dim dtKameitenCd As KihonJyouhouInputDataSet.KameitenCdTableDataTable
                dtKameitenCd = kihonJyouhouInputLogic.GetMaxKameitenCd(common_drop1.SelectedValue, tbxKameitenCd.Text, strCd)
                If dtKameitenCd.Rows.Count <> 0 AndAlso dtKameitenCd(0).kameiten_cd.ToString <> String.Empty Then
                    If dtKameitenCd(0).kameiten_cd < strCd Then
                        tbxKyoutuKameitenCd.Text = Right("00000" & (dtKameitenCd(0).kameiten_cd + 1).ToString, 5)
                    Else
                        tbxKyoutuKubun.Text = String.Empty
                        tbxKubunMei.Text = String.Empty
                        tbxKyoutuKameitenCd.Text = String.Empty
                        tbxBirudaNo.Text = String.Empty
                        btnTouroku.Enabled = False
                        ShowMsg(Messages.Instance.MSG2025E, tbxKameitenCd.ClientID)
                        Exit Sub
                    End If
                Else
                    tbxKyoutuKameitenCd.Text = tbxKameitenCd.Text
                End If
                tbxKyoutuKubun.Text = common_drop1.SelectedValue
                tbxKubunMei.Text = common_drop1.SelectedText.Split("�F")(1)
                tbxBirudaNo.Text = tbxKyoutuKameitenCd.Text
                btnTouroku.Enabled = True
            Else
                Dim dtKameitenCd As KihonJyouhouInputDataSet.KameitenCdTableDataTable
                dtKameitenCd = kihonJyouhouInputLogic.GetKameitenCd(common_drop1.SelectedValue, tbxKameitenCd.Text)
                '���͂��������X�R�[�h�����݂��Ȃ��ꍇ
                If dtKameitenCd.Rows.Count = 0 Then
                    tbxKyoutuKameitenCd.Text = tbxKameitenCd.Text
                Else
                    '���͂��������X�R�[�h=99999�̏ꍇ
                    If tbxKameitenCd.Text = "99999" Then
                        tbxKyoutuKubun.Text = String.Empty
                        tbxKubunMei.Text = String.Empty
                        tbxKyoutuKameitenCd.Text = String.Empty
                        tbxBirudaNo.Text = String.Empty
                        btnTouroku.Enabled = False
                        ShowMsg(Messages.Instance.MSG2025E, tbxKameitenCd.ClientID)
                        Exit Sub
                    End If
                    dtKameitenCd = kihonJyouhouInputLogic.GetKameitenCd(common_drop1.SelectedValue, Right("00000" & (tbxKameitenCd.Text + 1).ToString, 5))
                    '���͂��������X�R�[�h+1�����݂��Ȃ��ꍇ
                    If dtKameitenCd.Rows.Count = 0 Then
                        tbxKyoutuKameitenCd.Text = Right("00000" & (tbxKameitenCd.Text + 1).ToString, 5)
                    Else
                        '�G���[
                        tbxKyoutuKubun.Text = String.Empty
                        tbxKubunMei.Text = String.Empty
                        tbxKyoutuKameitenCd.Text = String.Empty
                        tbxBirudaNo.Text = String.Empty
                        btnTouroku.Enabled = False
                        ShowMsg(Messages.Instance.MSG2027E, tbxKameitenCd.ClientID)
                        Exit Sub
                    End If
                End If
                tbxKyoutuKubun.Text = common_drop1.SelectedValue
                tbxKubunMei.Text = common_drop1.SelectedText.Split("�F")(1)
                tbxBirudaNo.Text = tbxKyoutuKameitenCd.Text
                btnTouroku.Enabled = True
            End If
        End If

    End Sub

    ''' <summary>�o�^�{�^�����N���b�N��</summary>
    Private Sub btnTouroku_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTouroku.Click
        Dim commonSearch As New CommonSearchLogic
        '���̓`�F�b�N
        Dim strObjId As String = String.Empty
        Dim strErr As String = CheckInput(strObjId)
        If strErr <> String.Empty Then
            ShowMsg(strErr, strObjId)
            Exit Sub
        End If

        '�ݒ肵�������X�R�[�h�𑶍݃`�F�b�N
        Dim dtKameitenCd As KihonJyouhouInputDataSet.KameitenCdTableDataTable
        dtKameitenCd = KihonJyouhouInputLogic.GetKameitenCd(String.Empty, tbxKyoutuKameitenCd.Text)
        If dtKameitenCd.Rows.Count <> 0 Then
            ShowMsg(String.Format(Messages.Instance.MSG2029E, "�����X�R�[�h", "�����X�R�[�h").ToString, btnTouroku.ClientID)
            Exit Sub
        End If

        Dim strKbn As String
        '�敪�ݒ�
        If comdrp.Visible Then
            strKbn = comdrp.SelectedValue
        Else
            strKbn = common_drop1.SelectedValue
        End If
        '�n��R�[�h��ݒu�B
        If Me.tbxKeiretuCd.Text <> String.Empty Then
            Dim dtKeiretuTable As Data.DataTable = commonSearch.GetCommonInfo(tbxKeiretuCd.Text, "m_keiretu", strKbn)
            If dtKeiretuTable.Rows.Count > 0 Then
                Me.tbxKeiretuCd.Text = dtKeiretuTable.Rows(0).Item("cd").ToString
                Me.tbxKeiretuMei.Text = dtKeiretuTable.Rows(0).Item("mei").ToString
            End If
        End If
        '�c�Ə��R�[�h��ݒu�B
        If Me.tbxEigyousyoCd.Text <> String.Empty Then
            Dim dtEigyousyoTable As Data.DataTable = commonSearch.GetCommonInfo(tbxEigyousyoCd.Text, "m_eigyousyo")
            If dtEigyousyoTable.Rows.Count > 0 Then
                Me.tbxEigyousyoCd.Text = dtEigyousyoTable.Rows(0).Item("cd").ToString
                Me.tbxEigyousyoMei.Text = dtEigyousyoTable.Rows(0).Item("mei").ToString
            End If
        End If

        '�o�^�m�F���b�Z�[�W
        ShowConfirm(btnTouroku.ClientID)

    End Sub

    ''' <summary>�����X�}�X�^�e�[�u���ɓo�^����</summary>
    ''' <returns>����</returns>
    Private Function InsKameitenInfo() As Boolean
        Dim ninsyou As New Ninsyou()
        Dim strKbn As String
        '�敪�ݒ�
        If btnTyokusetuNyuuryoku.Visible = True Then
            strKbn = tbxKyoutuKubun.Text
        Else
            strKbn = comdrp.SelectedValue
        End If

        '��ʂɓ��͂����f�[�^��ݒ肷��
        Dim dtParamKameitenInfo As New KihonJyouhouInputDataSet.Param_KameitenInfoDataTable
        Dim row As KihonJyouhouInputDataSet.Param_KameitenInfoRow = dtParamKameitenInfo.NewParam_KameitenInfoRow
        row.kameiten_cd = tbxKyoutuKameitenCd.Text
        row.kbn = strKbn
        '==============2012/03/21 �ԗ� 405721�Č��̑Ή� �C����=============================
        'row.torikesi = tbxTorikesi.Text
        row.torikesi = Me.ddlTorikesi.SelectedValue.Trim
        '==============2012/03/21 �ԗ� 405721�Č��̑Ή� �C����=============================
        row.kameiten_mei1 = tbxKyoutuKameitenMei1.Text
        row.tenmei_kana1 = CommonCheck.SetTokomozi(tbxKyoutukakeMei1.Text)
        row.kameiten_mei2 = IIf(tbxKyoutuKameitenMei2.Text = String.Empty, String.Empty, tbxKyoutuKameitenMei2.Text)
        row.tenmei_kana2 = IIf(tbxKyoutukakeMei2.Text = String.Empty, String.Empty, CommonCheck.SetTokomozi(tbxKyoutukakeMei2.Text))
        row.builder_no = IIf(tbxBirudaNo.Text = String.Empty, String.Empty, tbxBirudaNo.Text)
        row.keiretu_cd = IIf(tbxKeiretuCd.Text = String.Empty, String.Empty, tbxKeiretuCd.Text)
        row.eigyousyo_cd = IIf(tbxEigyousyoCd.Text = String.Empty, String.Empty, tbxEigyousyoCd.Text)
        row.th_kasi_cd = IIf(tbxThKasiCd.Text = String.Empty, String.Empty, tbxThKasiCd.Text)
        row.add_login_user_id = ninsyou.GetUserID()
        dtParamKameitenInfo.AddParam_KameitenInfoRow(row)

        '�����X�}�X�^�e�[�u���ɓo�^����
        If KihonJyouhouInputLogic.SetKameitenInfo(dtParamKameitenInfo) Then
            Server.Transfer("KihonJyouhouInquiry.aspx?strKameitenCd=" & dtParamKameitenInfo(0).kameiten_cd & "")
        Else
            ShowMsg(Replace(Messages.Instance.MSG019E, "@PARAM1", "�����X�V�K�o�^"), btnTouroku.ClientID)
        End If

    End Function

    ''' <summary>���̓`�F�b�N</summary>
    ''' <param name="strObjId">�N���C�A���gID</param>
    ''' <returns>�G���[���b�Z�[�W</returns>
    Public Function CheckInput(ByRef strObjId As String) As String
        Dim commonSearch As New CommonSearchLogic
        Dim csScript As New StringBuilder
        Dim strKbn As String
        Dim jBn As New Jiban

        '�敪�ݒ�
        If comdrp.Visible Then
            strKbn = comdrp.SelectedValue
        Else
            strKbn = common_drop1.SelectedValue
        End If

        With csScript
            '==============2012/03/21 �ԗ� 405721�Č��̑Ή� �폜��=============================
            ''���
            'If Me.tbxTorikesi.Text <> String.Empty Then
            '    .Append(commonCheck.CheckHankaku(Me.tbxTorikesi.Text, "���"))
            '    If csScript.ToString <> String.Empty And strObjId = String.Empty Then
            '        strObjId = Me.tbxTorikesi.ClientID
            '    End If
            'Else
            '    .Append(commonCheck.CheckHissuNyuuryoku(Me.tbxTorikesi.Text, "���"))
            '    If csScript.ToString <> String.Empty And strObjId = String.Empty Then
            '        strObjId = Me.tbxTorikesi.ClientID
            '    End If
            'End If
            '==============2012/03/21 �ԗ� 405721�Č��̑Ή� �폜��=============================
            '�����X�R�[�h
            If Me.tbxKyoutuKameitenCd.Text <> String.Empty Then
                .Append(CommonCheck.CheckHankaku(Me.tbxKyoutuKameitenCd.Text, "�����X�R�[�h"))
                If csScript.ToString = String.Empty AndAlso Me.tbxKyoutuKameitenCd.Text.Length <> 5 Then
                    .Append(String.Format(Messages.Instance.MSG2004E, "�����X�R�[�h", "5").ToString)
                End If
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxKyoutuKameitenCd.ClientID
                End If
            Else
                .Append(CommonCheck.CheckHissuNyuuryoku(Me.tbxKyoutuKameitenCd.Text, "�����X�R�[�h"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxKyoutuKameitenCd.ClientID
                End If
            End If
            '�����X���P
            If Me.tbxKyoutuKameitenMei1.Text <> String.Empty Then
                .Append(CommonCheck.CheckKinsoku(Me.tbxKyoutuKameitenMei1.Text, "�����X���P"))
                .Append(CommonCheck.CheckByte(Me.tbxKyoutuKameitenMei1.Text, 40, "�����X���P", kbn.ZENKAKU))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxKyoutuKameitenMei1.ClientID
                End If
            Else
                .Append(CommonCheck.CheckHissuNyuuryoku(Me.tbxKyoutuKameitenMei1.Text, "�����X���P"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxKyoutuKameitenMei1.ClientID
                End If
            End If
            '�X�J�i���P
            If Me.tbxKyoutukakeMei1.Text <> String.Empty Then
                .Append(CommonCheck.CheckKatakana(CommonCheck.SetTokomozi(Me.tbxKyoutukakeMei1.Text), "�X�J�i���P"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxKyoutukakeMei1.ClientID
                End If
            Else
                .Append(CommonCheck.CheckHissuNyuuryoku(Me.tbxKyoutukakeMei1.Text, "�X�J�i���P"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxKyoutukakeMei1.ClientID
                End If
            End If
            '�����X���Q
            If Me.tbxKyoutuKameitenMei2.Text <> String.Empty Then
                .Append(CommonCheck.CheckKinsoku(Me.tbxKyoutuKameitenMei2.Text, "�����X���Q"))
                .Append(CommonCheck.CheckByte(Me.tbxKyoutuKameitenMei2.Text, 40, "�����X���Q", kbn.ZENKAKU))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxKyoutuKameitenMei2.ClientID
                End If
            End If
            '�X�J�i���Q
            If Me.tbxKyoutukakeMei2.Text <> String.Empty Then
                .Append(CommonCheck.CheckKatakana(CommonCheck.SetTokomozi(Me.tbxKyoutukakeMei2.Text), "�X�J�i���Q"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxKyoutukakeMei2.ClientID
                End If
            End If
            '�r���_�|NO
            If Me.tbxBirudaNo.Text <> String.Empty Then
                .Append(CommonCheck.ChkHankakuEisuuji(Me.tbxBirudaNo.Text, "�r���_�|NO"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxBirudaNo.ClientID
                End If
            End If
            '�n��R�[�h
            If Me.tbxKeiretuCd.Text <> String.Empty Then
                .Append(CommonCheck.ChkHankakuEisuuji(Me.tbxKeiretuCd.Text, "�n��R�[�h"))
                If CommonCheck.ChkHankakuEisuuji(Me.tbxKeiretuCd.Text, "�n��R�[�h") = String.Empty Then
                    If commonSearch.GetCommonInfo(tbxKeiretuCd.Text, "m_keiretu", strKbn).Rows.Count = 0 Then
                        .Append(String.Format(Messages.Instance.MSG2008E, "�n��R�[�h").ToString)
                    End If
                End If
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxKeiretuCd.ClientID
                End If
            End If
            '�c�Ə��R�[�h
            If Me.tbxEigyousyoCd.Text <> String.Empty Then
                .Append(CommonCheck.ChkHankakuEisuuji(Me.tbxEigyousyoCd.Text, "�c�Ə��R�[�h"))
                If CommonCheck.ChkHankakuEisuuji(Me.tbxEigyousyoCd.Text, "�c�Ə��R�[�h") = String.Empty Then
                    If commonSearch.GetCommonInfo(tbxEigyousyoCd.Text, "m_eigyousyo").Rows.Count = 0 Then
                        .Append(String.Format(Messages.Instance.MSG2008E, "�c�Ə��R�[�h").ToString)
                    End If
                End If
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxEigyousyoCd.ClientID
                End If
            End If
            'TH���r����
            If Me.tbxThKasiCd.Text <> String.Empty Then
                .Append(CommonCheck.CheckKinsoku(Me.tbxThKasiCd.Text, "TH���r����"))
                If CommonCheck.CheckKinsoku(Me.tbxThKasiCd.Text, "TH���r����") = String.Empty Then
                    If Not jBn.byteCheckSJIS(Me.tbxThKasiCd.Text, 7) Then
                        .Append(String.Format(Messages.Instance.MSG2028E, "TH���r����", "7"))
                    End If
                End If
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxThKasiCd.ClientID
                End If
            End If
        End With

        Return csScript.ToString
    End Function

    ''' <summary>Javascript�쐬</summary>
    Private Sub MakeJavascript()
        Dim sbScript As New StringBuilder
        Dim strPraram(0) As String
        With sbScript
            .AppendLine("<script language='javascript' type='text/javascript'>")
            '�r���_�[�|�b�v�A�b�v
            .AppendLine("   function fncKameitenSearch(){")
            .AppendLine("       var strkbn='�r���_�['")
            .AppendLine("       objSrchWin = window.open('search_common.aspx?Kbn='+escape(strkbn)+'&FormName=" & Me.Form.Name & _
                                           "&objCd=" & Me.tbxBirudaNo.ClientID & "&objMei=" & Me.tbxBirudaMei.ClientID & _
                                           "&strCd='+escape(eval('document.all.'+'" & Me.tbxBirudaNo.ClientID & "').value)+")
            .AppendLine("       '&strMei=&KensakuKubun=&blnDelete=True', ")
            .AppendLine("       'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');")
            .AppendLine("       return false;")
            .AppendLine("   }")
            .AppendLine("   function fncHansokuSina(){")
            .AppendLine("       var objKbn1 = document.getElementById('" & Me.common_drop1.DdlClientID & "')")
            .AppendLine("       var objKbn2 = document.getElementById('" & Me.comdrp.DdlClientID & "')")
            .AppendLine("       var objKameitenCd = document.getElementById('" & Me.tbxKyoutuKameitenCd.ClientID & "')")
            .AppendLine("       try{if(objKbn1.disabled==false && objKbn1.selectedIndex==0){")
            .AppendLine("           alert('" & Messages.Instance.MSG006E & "');")
            .AppendLine("           objKbn1.focus();")
            .AppendLine("           return false;")
            .AppendLine("       }")
            .AppendLine("       if(objKbn2.disabled==false && objKbn2.selectedIndex==0){")
            .AppendLine("           alert('" & Messages.Instance.MSG006E & "');")
            .AppendLine("           objKbn2.focus();")
            .AppendLine("           return false;")
            .AppendLine("       }}catch(e){}")
            .AppendLine("       if(objKameitenCd.value==''){")
            .AppendLine("           alert('" & Messages.Instance.MSG013E.Replace("@PARAM1", "�����X�R�[�h") & "');")
            .AppendLine("           return false;")
            .AppendLine("       }")
            .AppendLine("       if(!chkHankakuSuuji(objKameitenCd.value)){")
            .AppendLine("           alert('" & String.Format(Messages.Instance.MSG2006E, "�����X�R�[�h") & "');")
            .AppendLine("           objKameitenCd.focus();")
            .AppendLine("           objKameitenCd.select();")
            .AppendLine("           return false;")
            .AppendLine("       }")
            .AppendLine("       if(objKameitenCd.value.length!=5){")
            .AppendLine("           alert('" & String.Format(Messages.Instance.MSG2004E, "�����X�R�[�h", "5") & "');")
            .AppendLine("           objKameitenCd.focus();")
            .AppendLine("           objKameitenCd.select();")
            .AppendLine("           return false;")
            .AppendLine("       }")
            .AppendLine("       var arrKubun ")
            .AppendLine("       if(objKbn1.disabled==true){")
            .AppendLine("           arrKubun = objKbn2.value;")
            .AppendLine("       }else{")
            .AppendLine("           arrKubun = objKbn1.value;")
            .AppendLine("       }")
            .AppendLine("       var flg ")
            .AppendLine("       if(arrKubun=='A'){")
            .AppendLine("           flg = '1';")
            .AppendLine("       }else{")
            .AppendLine("           flg = '0';")
            .AppendLine("       }")
            .AppendLine("       return funcMove('" & UrlConst.TENBETU_SYUUSEI & "','2',flg,objKameitenCd.value);")
            .AppendLine("   }")
            '�n��|�b�v�A�b�v
            .AppendLine("   function fncKeiretuSearch(){")
            .AppendLine("       var objKbn1 = document.getElementById('" & Me.common_drop1.DdlClientID & "')")
            .AppendLine("       var objKbn2 = document.getElementById('" & Me.comdrp.DdlClientID & "')")
            .AppendLine("       try{if(objKbn1.disabled==false && objKbn1.selectedIndex==0){")
            .AppendLine("           alert('" & Messages.Instance.MSG006E & "');")
            .AppendLine("           objKbn1.focus();")
            .AppendLine("           return false;")
            .AppendLine("       }")
            .AppendLine("       if(objKbn2.disabled==false && objKbn2.selectedIndex==0){")
            .AppendLine("           alert('" & Messages.Instance.MSG006E & "');")
            .AppendLine("           objKbn2.focus();")
            .AppendLine("           return false;")
            .AppendLine("       }}catch(e){}")
            .AppendLine("       var strkbn='�n��'")
            .AppendLine("       var arrKubun ")
            .AppendLine("       if(objKbn1.disabled==true){")
            .AppendLine("           arrKubun = objKbn2.value;")
            .AppendLine("       }else{")
            .AppendLine("           arrKubun = objKbn1.value;")
            .AppendLine("       }")
            .AppendLine("       objSrchWin = window.open('search_common.aspx?Kbn='+escape(strkbn)+'&FormName=" & Me.Form.Name & _
                                           "&objCd=" & Me.tbxKeiretuCd.ClientID & "&objMei=" & Me.tbxKeiretuMei.ClientID & _
                                           "&strCd='+escape(eval('document.all.'+'" & Me.tbxKeiretuCd.ClientID & "').value)+")
            .AppendLine("       '&strMei=&KensakuKubun='+arrKubun+'&blnDelete=True', ")
            .AppendLine("       'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');")
            .AppendLine("       return false;")
            .AppendLine("   }")
            '�c�Ə��|�b�v�A�b�v
            .AppendLine("   function fncEigyousyoSearch(){")
            .AppendLine("       var strkbn='�c�Ə�'")
            .AppendLine("       objSrchWin = window.open('search_common.aspx?Kbn='+escape(strkbn)+'&FormName=" & Me.Form.Name & _
                                           "&objCd=" & Me.tbxEigyousyoCd.ClientID & "&objMei=" & Me.tbxEigyousyoMei.ClientID & _
                                           "&strCd='+escape(eval('document.all.'+'" & Me.tbxEigyousyoCd.ClientID & "').value)+")
            .AppendLine("       '&strMei=&KensakuKubun=&blnDelete=True', ")
            .AppendLine("       'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');")
            .AppendLine("       return false;")
            .AppendLine("   }")
            .AppendLine("   function fncNyuuryokuCheck(strClientID){")
            .AppendLine("       var objKbn = document.getElementById(strClientID)")
            .AppendLine("       if(objKbn.selectedIndex==0){")
            .AppendLine("           alert('" & Messages.Instance.MSG004E & "');")
            .AppendLine("           objKbn.focus();")
            .AppendLine("           return false;")
            .AppendLine("       }")
            .AppendLine("       return true;")
            .AppendLine("   }")
            '==============2012/03/21 �ԗ� 405721�Č��̑Ή� �ǉ���=============================
            '�����ύX���鎞
            .AppendLine("function fncSetTorikesi() ")
            .AppendLine("{ ")
            .AppendLine("	var strTorikesi = document.getElementById('" & Me.ddlTorikesi.ClientID & "').value; ")
            .AppendLine("	if(strTorikesi =='0') ")
            .AppendLine("	{ ")
            .AppendLine("		fncSetColor(''); ")
            .AppendLine("	} ")
            .AppendLine("	else ")
            .AppendLine("	{ ")
            .AppendLine("		fncSetColor('red'); ")
            .AppendLine("	} ")
            .AppendLine("} ")
            '�F���Z�b�g����
            .AppendLine("function fncSetColor(strColor) ")
            .AppendLine("{ ")
            .AppendLine("	document.getElementById('" & Me.tbxKyoutuKubun.ClientID & "').style.color = strColor; ")
            .AppendLine("	document.getElementById('" & Me.tbxKubunMei.ClientID & "').style.color = strColor; ")
            .AppendLine("	document.getElementById('" & Me.ddlTorikesi.ClientID & "').style.color = strColor; ")
            .AppendLine("	document.getElementById('" & Me.tbxKyoutuKameitenCd.ClientID & "').style.color = strColor; ")
            .AppendLine("	document.getElementById('" & Me.tbxKyoutuKameitenMei1.ClientID & "').style.color = strColor; ")
            .AppendLine("	document.getElementById('" & Me.tbxKyoutukakeMei1.ClientID & "').style.color = strColor; ")
            .AppendLine("	document.getElementById('" & Me.tbxKyoutuKameitenMei2.ClientID & "').style.color = strColor; ")
            .AppendLine("	document.getElementById('" & Me.tbxKyoutukakeMei2.ClientID & "').style.color = strColor; ")
            .AppendLine("	document.getElementById('" & Me.tbxBirudaNo.ClientID & "').style.color = strColor; ")
            .AppendLine("	document.getElementById('" & Me.tbxBirudaMei.ClientID & "').style.color = strColor; ")
            .AppendLine("	document.getElementById('" & Me.tbxKeiretuCd.ClientID & "').style.color = strColor; ")
            .AppendLine("	document.getElementById('" & Me.tbxKeiretuMei.ClientID & "').style.color = strColor; ")
            .AppendLine("	document.getElementById('" & Me.tbxEigyousyoCd.ClientID & "').style.color = strColor; ")
            .AppendLine("	document.getElementById('" & Me.tbxEigyousyoMei.ClientID & "').style.color = strColor; ")
            .AppendLine("	document.getElementById('" & Me.tbxThKasiCd.ClientID & "').style.color = strColor; ")
            .AppendLine("} ")
            '==============2012/03/21 �ԗ� 405721�Č��̑Ή� �ǉ���=============================
            .AppendLine("</script>")
        End With
        Page.ClientScript.RegisterStartupScript(Page.GetType, "InputCheck", sbScript.ToString)
    End Sub

    ''' <summary>�o�^�m�F���b�Z�[�W</summary>
    ''' <param name="strObjId">�N���C�A���gID</param>
    Private Sub ShowConfirm(ByVal strObjId As String)
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("   document.getElementById('" & hidTourokuFlg.ClientID & "').value = '1';")
            .AppendLine("   document.forms[0].submit();")
        End With
        '�y�[�W�����ŁA�N���C�A���g���̃X�N���v�g �u���b�N���o�͂��܂�
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ShowConfirm", csScript.ToString, True)
    End Sub

    ''' <summary>���b�Z�[�W�\��</summary>
    ''' <param name="strMessage">���b�Z�[�W</param>
    ''' <param name="strID">�N���C�A���gID</param>
    Private Sub ShowMsg(ByVal strMessage As String, ByVal strID As String)
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("alert('" & strMessage & "');")
            .AppendLine("if(document.getElementById('" & strID & "').type != 'submit'){")
            .AppendLine("    document.getElementById('" & strID & "').focus();")
            .AppendLine("    document.getElementById('" & strID & "').select();")
            .AppendLine("}")
        End With
        '�y�[�W�����ŁA�N���C�A���g���̃X�N���v�g �u���b�N���o�͂��܂�
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ShowMsg", csScript.ToString, True)
    End Sub

    Private Sub WindowOnload()
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("function window.onload(){")
            .AppendLine("   document.getElementById('" & Me.hidTourokuFlg.ClientID & "').value='';")
            .AppendLine("}")
        End With
        '�y�[�W�����ŁA�N���C�A���g���̃X�N���v�g �u���b�N���o�͂��܂�
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "WindowOnload", csScript.ToString, True)
    End Sub

    ''' <summary>
    ''' �u����vddl���Z�b�g����
    ''' </summary>
    ''' <history>2012/03/27 �ԗ� 405721�Č��̑Ή� �ǉ�</history>
    Private Sub SetTorikesiDDL()

        '�f�[�^���擾����
        Dim dtTorikesi As New Data.DataTable
        dtTorikesi = KihonJyouhouInputLogic.GetTorikesiList()

        With Me.ddlTorikesi
            'ddl��Bound����
            .DataValueField = "code"
            .DataTextField = "meisyou"
            .DataSource = dtTorikesi
            .DataBind()

            '�擪�s
            '=============2012/04/20 �ԗ� 405721�̗v�]�Ή� �C����==================
            '.Items.Insert(0, New ListItem("0", "0"))
            .Items.Insert(0, New ListItem(String.Empty, "0"))
            '=============2012/04/20 �ԗ� 405721�̗v�]�Ή� �C����==================
        End With

    End Sub

    ''' <summary>
    ''' �F��ύX����
    ''' </summary>
    ''' <history>2012/03/27 �ԗ� 405721�Č��̑Ή� �ǉ�</history>
    Private Sub SetColor()

        Dim strTorikesi As String
        strTorikesi = Me.ddlTorikesi.SelectedValue.Trim

        If strTorikesi.Equals("0") OrElse strTorikesi.Equals(String.Empty) Then
            Me.tbxKyoutuKubun.ForeColor = Drawing.Color.Black
            Me.tbxKubunMei.ForeColor = Drawing.Color.Black
            Me.ddlTorikesi.ForeColor = Drawing.Color.Black
            Me.tbxKyoutuKameitenCd.ForeColor = Drawing.Color.Black
            Me.tbxKyoutuKameitenMei1.ForeColor = Drawing.Color.Black
            Me.tbxKyoutukakeMei1.ForeColor = Drawing.Color.Black
            Me.tbxKyoutuKameitenMei2.ForeColor = Drawing.Color.Black
            Me.tbxKyoutukakeMei2.ForeColor = Drawing.Color.Black
            Me.tbxBirudaNo.ForeColor = Drawing.Color.Black
            Me.tbxBirudaMei.ForeColor = Drawing.Color.Black
            Me.tbxKeiretuCd.ForeColor = Drawing.Color.Black
            Me.tbxKeiretuMei.ForeColor = Drawing.Color.Black
            Me.tbxEigyousyoCd.ForeColor = Drawing.Color.Black
            Me.tbxEigyousyoMei.ForeColor = Drawing.Color.Black
            Me.tbxThKasiCd.ForeColor = Drawing.Color.Black
        Else
            Me.tbxKyoutuKubun.ForeColor = Drawing.Color.Red
            Me.tbxKubunMei.ForeColor = Drawing.Color.Red
            Me.ddlTorikesi.ForeColor = Drawing.Color.Red
            Me.tbxKyoutuKameitenCd.ForeColor = Drawing.Color.Red
            Me.tbxKyoutuKameitenMei1.ForeColor = Drawing.Color.Red
            Me.tbxKyoutukakeMei1.ForeColor = Drawing.Color.Red
            Me.tbxKyoutuKameitenMei2.ForeColor = Drawing.Color.Red
            Me.tbxKyoutukakeMei2.ForeColor = Drawing.Color.Red
            Me.tbxBirudaNo.ForeColor = Drawing.Color.Red
            Me.tbxBirudaMei.ForeColor = Drawing.Color.Red
            Me.tbxKeiretuCd.ForeColor = Drawing.Color.Red
            Me.tbxKeiretuMei.ForeColor = Drawing.Color.Red
            Me.tbxEigyousyoCd.ForeColor = Drawing.Color.Red
            Me.tbxEigyousyoMei.ForeColor = Drawing.Color.Red
            Me.tbxThKasiCd.ForeColor = Drawing.Color.Red
        End If

    End Sub

End Class
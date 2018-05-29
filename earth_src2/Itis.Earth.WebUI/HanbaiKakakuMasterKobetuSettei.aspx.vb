Imports Itis.Earth.BizLogic
Imports Itis.Earth.DataAccess

Partial Public Class HanbaiKakakuMasterKobetuSettei
    Inherits System.Web.UI.Page

    Private hanbaiKakakuSearchListLogic As New HanbaiKakakuMasterLogic
    Private commonCheck As New CommonCheck

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '�����`�F�b�N
        Dim commonChk As New CommonCheck
        Dim strUserID As String = ""
        Dim blnEigyouKengen As Boolean
        blnEigyouKengen = commonChk.CommonNinnsyou(strUserID, "eigyou_master_kanri_kengen")
        If Not blnEigyouKengen Then
            '�G���[��ʂ֑J�ڂ��āA�G���[���b�Z�[�W��\������
            Context.Items("strFailureMsg") = Messages.Instance.MSG2020E
            Server.Transfer("CommonErr.aspx")
        End If

        'javascript�쐬
        MakeJavascript()

        If Not IsPostBack Then

            If Not Request("sendSearchTerms") Is Nothing Then
                '������
                Call SetInitData(CStr(Request("sendSearchTerms")))
                Me.hdnAiteSakiSyubetu.Value = Me.ddlAiteSakiSyubetu.SelectedValue.Trim
                Me.hdnAiteSakiCd.Value = Me.tbxAiteSakiCd.Text.Trim
            Else
                '������
                Call SetInitData(String.Empty)
            End If

        Else
            Me.hdnKensakuFlg.Value = "1"
            '�������
            Call Me.SetAitesakiSyubetu(Me.ddlAiteSakiSyubetu.SelectedValue.Trim)
            Me.hdnAiteSakiSyubetu.Value = Me.ddlAiteSakiSyubetu.SelectedValue.Trim
            '�����R�[�h
            Call Me.SetAitesakiCd(Me.tbxAiteSakiCd.Text.Trim)
            Me.hdnAiteSakiCd.Value = Me.tbxAiteSakiCd.Text.Trim
            '���i�R�[�h
            Call Me.SetSyouhin(Me.ddlSyouhinCd.SelectedValue.Trim)
            '�������@
            Call Me.SetTyousaHouhou(Me.ddlTyousaHouhou.SelectedValue.Trim)

            '�X�V�������s��
            If CStr(Me.hdnDbFlg.Value).Equals("1") Then
                Call Me.DbSyori("update")
                Me.hdnDbFlg.Value = String.Empty
            End If

        End If
        '����飃{�^��
        Me.btnClose.Attributes.Add("onClick", "fncCloseWindow();")
        '������ʂ��ύX����ꍇ�A�����R�[�h����������ݒ肷��
        Me.ddlAiteSakiSyubetu.Attributes.Add("onChange", "fncSetAitesaki();return false;")
        '�����R�[�h���t�H�[�J�X�������ꍇ�A�啶���ɕϊ�����
        Me.tbxAiteSakiCd.Attributes.Add("onBlur", "fncToUpper(this);")
        Me.tbxAiteSakiCd.Attributes.Add("onChange", "fncAitesakiCdChange();")
        '�������{�^��
        Me.btnAiteSakiCd.Attributes.Add("onClick", "fncAiteSakiSearch();return false;")
        '��o�^��{�^��
        Me.btnTouroku.Attributes.Add("onClick", "if(!fncNyuuryokuCheck()){return false;}")

    End Sub

    ''' <summary>�����f�[�^���Z�b�g</summary>
    ''' <param name="sendSearchTerms">�p�����[�^</param>
    Private Sub SetInitData(ByVal sendSearchTerms As String)

        Me.hdnKensakuFlg.Value = "1"

        '�H���X�������z�ύX�t���O
        Me.ddlKoumutenSeikyuuKingakuFlg.Items.Insert(0, New ListItem("�ύX�s��", "0"))
        Me.ddlKoumutenSeikyuuKingakuFlg.Items.Insert(1, New ListItem("�ύX��", "1"))

        '���������z�ύX�t���O
        Me.ddlJituSeikyuuKingakuFlg.Items.Insert(0, New ListItem("�ύX�s��", "0"))
        Me.ddlJituSeikyuuKingakuFlg.Items.Insert(1, New ListItem("�ύX��", "1"))

        If sendSearchTerms <> String.Empty Then
            Dim arrSearchTerm() As String = Split(sendSearchTerms, "$$$")
            '�������
            Dim strAiteSakiSyubetu As String
            '�����R�[�h
            Dim strAiteSakiCd As String
            '���i�R�[�h
            Dim strSyouhinCd As String
            '�������@
            Dim strTyousaHouhouNo As String

            '�������
            strAiteSakiSyubetu = arrSearchTerm(0).Trim
            Me.hdnAiteSakiSyubetu.Value = strAiteSakiSyubetu
            Call Me.SetAitesakiSyubetu(strAiteSakiSyubetu)

            '�����R�[�h
            If arrSearchTerm.Length > 1 Then
                strAiteSakiCd = arrSearchTerm(1).Trim
            Else
                strAiteSakiCd = String.Empty
            End If
            Me.hdnAiteSakiCd.Value = strAiteSakiCd
            Call Me.SetAitesakiCd(strAiteSakiCd)

            '���i�R�[�h
            If arrSearchTerm.Length > 2 Then
                strSyouhinCd = arrSearchTerm(2).Trim
            Else
                strSyouhinCd = String.Empty
            End If
            Call Me.SetSyouhin(strSyouhinCd)

            '�������@
            If arrSearchTerm.Length > 3 Then
                strTyousaHouhouNo = arrSearchTerm(3).Trim
            Else
                strTyousaHouhouNo = String.Empty
            End If
            Call Me.SetTyousaHouhou(strTyousaHouhouNo)

            If arrSearchTerm.Length > 3 Then
                '�������s��
                Call Me.KensakuSyori(strAiteSakiSyubetu, strAiteSakiCd, strSyouhinCd, strTyousaHouhouNo)
            Else
                '���
                Me.chkTorikesi.Checked = False

                '���J
                Me.chkKoukai.Checked = False

                '�H���X�������z
                Me.tbxKoumutenSeikyuuKingaku.Text = "0"

                '���������z
                Me.tbxJituSeikyuuKingaku.Text = "0"

                '�H���X�������z�ύX�t���O
                Me.ddlKoumutenSeikyuuKingakuFlg.SelectedValue = "0"

                '�H���X�������z�ύX�t���O
                Me.ddlJituSeikyuuKingakuFlg.SelectedValue = "0"
            End If
        Else
            '�������
            Call Me.SetAitesakiSyubetu(String.Empty)
            Me.hdnAiteSakiSyubetu.Value = String.Empty
            '�����R�[�h
            Call Me.SetAitesakiCd(String.Empty)
            Me.hdnAiteSakiCd.Value = String.Empty
            '���i�R�[�h
            Call Me.SetSyouhin(String.Empty)

            '�������@
            Call Me.SetTyousaHouhou(String.Empty)

            '���
            Me.chkTorikesi.Checked = False

            '���J
            Me.chkKoukai.Checked = False

            '�H���X�������z
            Me.tbxKoumutenSeikyuuKingaku.Text = "0"

            '���������z
            Me.tbxJituSeikyuuKingaku.Text = "0"

            '�H���X�������z�ύX�t���O
            Me.ddlKoumutenSeikyuuKingakuFlg.SelectedValue = "0"

            '�H���X�������z�ύX�t���O
            Me.ddlJituSeikyuuKingakuFlg.SelectedValue = "0"
        End If

    End Sub

    ''' <summary> ������ʃ��X�g��ݒ肷��</summary>
    ''' <param name="strAitesakiSyubetu">�������</param>
    Private Sub SetAitesakiSyubetu(ByVal strAitesakiSyubetu As String)

        Dim dtAiteSakiSyubetu As Data.DataTable
        '������ʃf�[�^���擾����
        dtAiteSakiSyubetu = hanbaiKakakuSearchListLogic.GetAiteSakiSyubetu()
        '�f�[�^��ݒ肷��
        Me.ddlAiteSakiSyubetu.DataTextField = "aitesaki_syubetu"
        Me.ddlAiteSakiSyubetu.DataValueField = "code"
        Me.ddlAiteSakiSyubetu.DataSource = dtAiteSakiSyubetu
        Me.ddlAiteSakiSyubetu.DataBind()

        '������ʂ̐擪�s�͋󗓂��Z�b�g����
        Me.ddlAiteSakiSyubetu.Items.Insert(0, New ListItem(String.Empty, String.Empty))

        '�v�����^�̃f�t�H���g�\��
        If strAitesakiSyubetu.Equals(String.Empty) Then
            Me.ddlAiteSakiSyubetu.SelectedValue = String.Empty
        Else
            Try
                Me.ddlAiteSakiSyubetu.SelectedValue = strAitesakiSyubetu
            Catch ex As Exception
                Me.ddlAiteSakiSyubetu.SelectedValue = String.Empty
            End Try

        End If

    End Sub

    ''' <summary>�����R�[�h��ݒ�</summary>
    Private Sub SetAitesakiCd(ByVal strAitesakiCd As String)

        Dim strAiteSakiSyubetu As String = Me.ddlAiteSakiSyubetu.SelectedValue.Trim

        Select Case strAiteSakiSyubetu
            Case String.Empty
                '������R�[�h�
                Me.tbxAiteSakiCd.Text = String.Empty
                Me.tbxAiteSakiCd.Enabled = False
                '�u�����v�{�^��
                Me.btnAiteSakiCd.Enabled = False
                '�����於�
                Me.tbxAiteSakiMei.Text = String.Empty
            Case "0"
                '������R�[�h�
                Me.tbxAiteSakiCd.Text = "ALL"
                Me.tbxAiteSakiCd.Enabled = False
                '�u�����v�{�^��
                Me.btnAiteSakiCd.Enabled = False
                '�����於�
                Me.tbxAiteSakiMei.Text = "�����Ȃ�"
            Case Else
                '������R�[�h�
                Me.tbxAiteSakiCd.Enabled = True
                '�u�����v�{�^��
                Me.btnAiteSakiCd.Enabled = True
                If Not strAitesakiCd.Trim.Equals(String.Empty) Then
                    '������R�[�h�
                    Me.tbxAiteSakiCd.Text = strAitesakiCd.Trim

                    '����於���擾
                    Dim dtAitesakiMei As New Data.DataTable
                    dtAitesakiMei = hanbaiKakakuSearchListLogic.GetAiteSaki(strAiteSakiSyubetu.Trim, String.Empty, strAitesakiCd.Trim)

                    '����於���Z�b�g
                    If dtAitesakiMei.Rows.Count > 0 Then
                        Me.tbxAiteSakiMei.Text = dtAitesakiMei.Rows(0).Item("mei").ToString
                    Else
                        Me.tbxAiteSakiMei.Text = String.Empty
                    End If
                Else
                    Me.tbxAiteSakiMei.Text = String.Empty
                End If
        End Select

    End Sub

    ''' <summary> ���i�R�[�h���X�g��ݒ肷��</summary>
    ''' <param name="strSyouhinCd">���i�R�[�h</param>
    Private Sub SetSyouhin(ByVal strSyouhinCd As String)

        Dim dtSyouhin As Data.DataTable
        '������ʃf�[�^���擾����
        dtSyouhin = hanbaiKakakuSearchListLogic.GetSyouhin()
        '�f�[�^��ݒ肷��
        Me.ddlSyouhinCd.DataTextField = "syouhin"
        Me.ddlSyouhinCd.DataValueField = "syouhin_cd"
        Me.ddlSyouhinCd.DataSource = dtSyouhin
        Me.ddlSyouhinCd.DataBind()

        ''���i�R�[�h�̐擪�s�͋󗓂��Z�b�g����
        Me.ddlSyouhinCd.Items.Insert(0, New ListItem(String.Empty, String.Empty))

        '�v�����^�̃f�t�H���g�\��
        If strSyouhinCd.Equals(String.Empty) Then
            Me.ddlSyouhinCd.SelectedValue = String.Empty
        Else
            Try
                Me.ddlSyouhinCd.SelectedValue = strSyouhinCd
            Catch ex As Exception
                Me.ddlSyouhinCd.SelectedValue = String.Empty
            End Try
        End If

    End Sub

    ''' <summary> �������@���X�g��ݒ肷��</summary>
    ''' <param name="strTysHouhouNo">�������@NO</param>
    Private Sub SetTyousaHouhou(ByVal strTysHouhouNo As String)

        Dim dtTyousahouhou As Data.DataTable
        '������ʃf�[�^���擾����
        dtTyousahouhou = hanbaiKakakuSearchListLogic.GetTyousaHouhou()
        '�f�[�^��ݒ肷��
        Me.ddlTyousaHouhou.DataTextField = "tys_houhou"
        Me.ddlTyousaHouhou.DataValueField = "tys_houhou_no"
        Me.ddlTyousaHouhou.DataSource = dtTyousahouhou
        Me.ddlTyousaHouhou.DataBind()

        '�������@�̐擪�s�͋󗓂��Z�b�g����
        Me.ddlTyousaHouhou.Items.Insert(0, New ListItem(String.Empty, String.Empty))

        '�v�����^�̃f�t�H���g�\��
        If strTysHouhouNo.Equals(String.Empty) Then
            Me.ddlTyousaHouhou.SelectedValue = String.Empty
        Else
            Try
                Me.ddlTyousaHouhou.SelectedValue = strTysHouhouNo
            Catch ex As Exception
                Me.ddlTyousaHouhou.SelectedValue = String.Empty
            End Try
        End If

    End Sub

    ''' <summary>���̓`�F�b�N</summary>
    ''' <param name="strObjId">�N���C�A���gID</param>
    ''' <returns>�G���[���b�Z�[�W</returns>
    Private Function CheckInput(ByRef strObjId As String) As String
        Dim csScript As New StringBuilder
        With csScript
            '�����R�[�h(From)(�p�����`�F�b�N)
            If Me.tbxAiteSakiCd.Text <> String.Empty Then
                .Append(commonCheck.ChkHankakuEisuuji(Me.tbxAiteSakiCd.Text, "�����R�[�h(From)"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxAiteSakiCd.ClientID
                End If
            End If
        End With
        Return csScript.ToString
    End Function

    ''' <summary>��������</summary>
    ''' <param name="strAiteSakiSyubetu">�������</param>
    ''' <param name="strAiteSakiCd">�����R�[�h</param>
    ''' <param name="strSyouhinCd">���i�R�[�h</param>
    ''' <param name="strTyousaHouhouNo">�������@</param>
    ''' <remarks></remarks>
    Private Sub KensakuSyori(ByVal strAiteSakiSyubetu As String, ByVal strAiteSakiCd As String, ByVal strSyouhinCd As String, ByVal strTyousaHouhouNo As String)

        Dim intValue As Integer
        Dim blnKensaku As Boolean = True
        Try
            intValue = CInt(strAiteSakiSyubetu)
        Catch ex As Exception
            blnKensaku = False
        End Try
        Try
            intValue = CInt(strAiteSakiSyubetu)
        Catch ex As Exception
            blnKensaku = False
        End Try

        If blnKensaku = True Then

            '�f�[�^���擾����
            Dim dtHanbaiKakaku As New Data.DataTable
            dtHanbaiKakaku = hanbaiKakakuSearchListLogic.GetHanbaiKakakuKobeituSettei(strAiteSakiSyubetu, strAiteSakiCd, strSyouhinCd, strTyousaHouhouNo)

            If dtHanbaiKakaku.Rows.Count > 0 Then
                With dtHanbaiKakaku.Rows(0)
                    '����於
                    'Me.tbxAiteSakiMei.Text = .Item("aitesaki_mei").ToString.Trim

                    '���
                    If .Item("torikesi").ToString.Trim.Equals("0") Then
                        Me.chkTorikesi.Checked = False
                    Else
                        Me.chkTorikesi.Checked = True
                    End If

                    '���J
                    If .Item("koukai_flg").ToString.Trim.Equals("0") Then
                        Me.chkKoukai.Checked = False
                    Else
                        Me.chkKoukai.Checked = True
                    End If

                    '�H���X�������z
                    If Not .Item("koumuten_seikyuu_gaku").ToString.Trim.Equals(String.Empty) Then
                        Me.tbxKoumutenSeikyuuKingaku.Text = FormatNumber(.Item("koumuten_seikyuu_gaku").ToString.Trim, 0)
                    Else
                        Me.tbxKoumutenSeikyuuKingaku.Text = "0"
                    End If

                    '���������z
                    If Not .Item("jitu_seikyuu_gaku").ToString.Trim.Equals(String.Empty) Then
                        Me.tbxJituSeikyuuKingaku.Text = FormatNumber(.Item("jitu_seikyuu_gaku").ToString.Trim, 0)
                    Else
                        Me.tbxJituSeikyuuKingaku.Text = "0"
                    End If

                    '�H���X�������z�ύX�t���O
                    If .Item("koumuten_seikyuu_gaku_henkou_flg").ToString.Trim.Equals("0") Then
                        Me.ddlKoumutenSeikyuuKingakuFlg.SelectedValue = "0"
                    Else
                        Me.ddlKoumutenSeikyuuKingakuFlg.SelectedValue = "1"
                    End If

                    '�H���X�������z�ύX�t���O
                    If .Item("jitu_seikyuu_gaku_henkou_flg").ToString.Trim.Equals("0") Then
                        Me.ddlJituSeikyuuKingakuFlg.SelectedValue = "0"
                    Else
                        Me.ddlJituSeikyuuKingakuFlg.SelectedValue = "1"
                    End If
                End With
            Else
                '���
                Me.chkTorikesi.Checked = False
                '���J
                Me.chkKoukai.Checked = False
                '�H���X�������z
                Me.tbxKoumutenSeikyuuKingaku.Text = "0"
                '���������z
                Me.tbxJituSeikyuuKingaku.Text = "0"
                '�H���X�������z�ύX�t���O
                Me.ddlKoumutenSeikyuuKingakuFlg.SelectedValue = "0"
                '�H���X�������z�ύX�t���O
                Me.ddlJituSeikyuuKingakuFlg.SelectedValue = "0"
            End If
        Else
            '���
            Me.chkTorikesi.Checked = False
            '���J
            Me.chkKoukai.Checked = False
            '�H���X�������z
            Me.tbxKoumutenSeikyuuKingaku.Text = "0"
            '���������z
            Me.tbxJituSeikyuuKingaku.Text = "0"
            '�H���X�������z�ύX�t���O
            Me.ddlKoumutenSeikyuuKingakuFlg.SelectedValue = "0"
            '�H���X�������z�ύX�t���O
            Me.ddlJituSeikyuuKingakuFlg.SelectedValue = "0"
        End If
    End Sub

    ''' <summary>�o�^�{�^������������</summary>
    Private Sub btnTouroku_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTouroku.Click

        '���̓`�F�b�N
        Dim strObjId As String = String.Empty
        Dim strErrMessage As String = CheckInput(strObjId)
        If strErrMessage <> String.Empty Then
            ShowMessage(strErrMessage)
            Exit Sub
        End If

        '�������
        Dim strAiteSakiSyubetu As String
        strAiteSakiSyubetu = Me.ddlAiteSakiSyubetu.SelectedValue.Trim
        '�����R�[�h
        Dim strAiteSakiCd As String
        If strAiteSakiSyubetu.Equals("0") Then
            strAiteSakiCd = "ALL"
        Else
            strAiteSakiCd = Me.tbxAiteSakiCd.Text.Trim
        End If

        '���i�R�[�h
        Dim strSyouhinCd As String
        strSyouhinCd = Me.ddlSyouhinCd.SelectedValue.Trim
        '�������@
        Dim strTyousaHouhouNo As String
        strTyousaHouhouNo = Me.ddlTyousaHouhou.SelectedValue.Trim

        '���݃`�F�c�N
        Dim dtHanbaiKakaku As New Data.DataTable
        dtHanbaiKakaku = hanbaiKakakuSearchListLogic.CheckSonzai(strAiteSakiSyubetu, strAiteSakiCd, strSyouhinCd, strTyousaHouhouNo)

        If dtHanbaiKakaku.Rows.Count > 0 Then
            '�X�V
            Call Me.ShowKakuninMessage(Messages.Instance.MSG2057E)
        Else
            '�o�^
            Call Me.DbSyori("insert")
        End If

    End Sub

    ''' <summary>DB����</summary>
    Private Sub DbSyori(ByVal strKbn As String)

        '���[�U�[ID
        Dim ninsyou As New Ninsyou()
        Dim strUserId As String
        strUserId = ninsyou.GetUserID()

        '�������
        Dim strAiteSakiSyubetu As String
        strAiteSakiSyubetu = Me.ddlAiteSakiSyubetu.SelectedValue.Trim
        '�����R�[�h
        Dim strAiteSakiCd As String
        If strAiteSakiSyubetu.Equals("0") Then
            strAiteSakiCd = "ALL"
        Else
            strAiteSakiCd = Me.tbxAiteSakiCd.Text.Trim
        End If

        '���i�R�[�h
        Dim strSyouhinCd As String
        strSyouhinCd = Me.ddlSyouhinCd.SelectedValue.Trim
        '�������@
        Dim strTyousaHouhouNo As String
        strTyousaHouhouNo = Me.ddlTyousaHouhou.SelectedValue.Trim

        '�p�����[�^
        Dim dtHanbaiKakakuOk As New Data.DataTable
        Call Me.SetHanbaiKakaku(dtHanbaiKakakuOk)
        Dim dr As Data.DataRow
        dr = dtHanbaiKakakuOk.NewRow
        dr.Item("aitesaki_syubetu") = strAiteSakiSyubetu
        dr.Item("aitesaki_cd") = strAiteSakiCd
        dr.Item("syouhin_cd") = strSyouhinCd
        dr.Item("tys_houhou_no") = strTyousaHouhouNo
        If Me.chkTorikesi.Checked Then
            dr.Item("torikesi") = "1"
        Else
            dr.Item("torikesi") = "0"
        End If
        Dim strKoumutenSeikyuuKingaku As String = Me.tbxKoumutenSeikyuuKingaku.Text.Trim.Replace(",", String.Empty)
        dr.Item("koumuten_seikyuu_gaku") = IIf(strKoumutenSeikyuuKingaku.Equals(String.Empty), "0", strKoumutenSeikyuuKingaku)
        dr.Item("koumuten_seikyuu_gaku_henkou_flg") = Me.ddlKoumutenSeikyuuKingakuFlg.SelectedValue.Trim
        Dim strJituSeikyuuKingaku As String = Me.tbxJituSeikyuuKingaku.Text.Trim.Replace(",", String.Empty)
        dr.Item("jitu_seikyuu_gaku") = IIf(strJituSeikyuuKingaku.Equals(String.Empty), "0", strJituSeikyuuKingaku)
        dr.Item("jitu_seikyuu_gaku_henkou_flg") = Me.ddlJituSeikyuuKingakuFlg.SelectedValue.Trim
        If Me.chkKoukai.Checked Then
            dr.Item("koukai_flg") = "1"
        Else
            dr.Item("koukai_flg") = "0"
        End If

        Select Case strKbn
            Case "insert"
                dr.Item("ins_upd_flg") = "0"
            Case "update"
                dr.Item("ins_upd_flg") = "1"
        End Select

        dtHanbaiKakakuOk.Rows.Add(dr)

        If hanbaiKakakuSearchListLogic.SetHanbaiKakakuKobeituSettei(dtHanbaiKakakuOk, strUserId).Equals(False) Then

            Call Me.ShowMessage(Messages.Instance.MSG2058E)

        Else

            '����ʂ����
            ClientScript.RegisterStartupScript(Me.GetType(), "ShowMessage", "fncCloseWindow();", True)
        End If

    End Sub

    ''' <summary>Javascript�쐬</summary>
    Private Sub MakeJavascript()
        Dim sbScript As New StringBuilder
        Dim strPraram(0) As String
        With sbScript
            .AppendLine("<script language='javascript' type='text/javascript'>")
            '�����ύX��
            .AppendLine("function fncSetAitesaki()")
            .AppendLine("{")
            '.AppendLine("   alert('hdnKensakuFlg:'+document.getElementById('" & Me.hdnKensakuFlg.ClientID & "').value);")
            .AppendLine("   if(document.getElementById('" & Me.ddlAiteSakiSyubetu.ClientID & "').value != document.getElementById('" & Me.hdnAiteSakiSyubetu.ClientID & "').value)")
            .AppendLine("   {")
            .AppendLine("       document.getElementById('" & Me.hdnKensakuFlg.ClientID & "').value = '0';")
            .AppendLine("       document.getElementById('" & Me.hdnAiteSakiSyubetu.ClientID & "').value = document.getElementById('" & Me.ddlAiteSakiSyubetu.ClientID & "').value;")
            .AppendLine("   }")
            .AppendLine("	switch(document.all." & Me.ddlAiteSakiSyubetu.ClientID & ".value) ")
            .AppendLine("	{ ")
            .AppendLine("		case '': ")
            .AppendLine("           document.all." & Me.tbxAiteSakiCd.ClientID & ".value = '';")
            .AppendLine("           document.all." & Me.tbxAiteSakiCd.ClientID & ".disabled = true;")
            .AppendLine("           document.all." & Me.btnAiteSakiCd.ClientID & ".disabled = true;")
            .AppendLine("           document.all." & Me.tbxAiteSakiMei.ClientID & ".value = '';")
            .AppendLine("			break; ")
            .AppendLine("		case '0': ")
            .AppendLine("           document.all." & Me.tbxAiteSakiCd.ClientID & ".value = 'ALL';")
            .AppendLine("           document.all." & Me.tbxAiteSakiCd.ClientID & ".disabled = true;")
            .AppendLine("           document.all." & Me.btnAiteSakiCd.ClientID & ".disabled = true;")
            .AppendLine("           document.all." & Me.tbxAiteSakiMei.ClientID & ".value = '';")
            .AppendLine("			break; ")
            .AppendLine("		default: ")
            .AppendLine("           document.all." & Me.tbxAiteSakiCd.ClientID & ".value = '';")
            .AppendLine("           document.all." & Me.tbxAiteSakiCd.ClientID & ".disabled = false;")
            .AppendLine("           document.all." & Me.btnAiteSakiCd.ClientID & ".disabled = false;")
            .AppendLine("           document.all." & Me.tbxAiteSakiMei.ClientID & ".value = '';")
            .AppendLine("	} ")
            '.AppendLine("   alert('hdnKensakuFlg:'+document.getElementById('" & Me.hdnKensakuFlg.ClientID & "').value);")
            .AppendLine("}")
            '����挟������������ꍇ�A�|�b�v�A�b�v���N������
            .AppendLine("function fncAiteSakiSearch(){")
            .AppendLine("       var objRetrun = '" & Me.hdnKensakuFlg.ClientID & "';")
            .AppendLine("       var hdnId = '" & Me.hdnAiteSakiCd.ClientID & "';")
            '������ʂ��u1:�����X�v�̏ꍇ�A�����X�|�b�v�A�b�v���N������
            .AppendLine("   if(document.all." & Me.ddlAiteSakiSyubetu.ClientID & ".value == '1'){")
            .AppendLine("       var strkbn='�����X';")
            .AppendLine("       var strClientCdID; ")
            .AppendLine("       var strClientMeiID; ")
            .AppendLine("       var blnTorikesi; ")
            .AppendLine("       strClientCdID = '" & Me.tbxAiteSakiCd.ClientID & "';")
            .AppendLine("       strClientMeiID = '" & Me.tbxAiteSakiMei.ClientID & "';")
            .AppendLine("       blnTorikesi = 'False';")
            .AppendLine("       objSrchWin = window.open('search_common.aspx?Kbn='+escape(strkbn)+'&FormName=" & Me.Form.Name & _
                                "&objCd='+strClientCdID+'&objMei='+strClientMeiID+'&strCd='+escape(eval('document.all.'+strClientCdID).value)+'&strMei='+escape(eval('document.all.'+strClientMeiID).value)+")
            .AppendLine("       '&blnDelete='+blnTorikesi+'&objRetrun='+objRetrun+'&hdnId='+hdnId, ")
            .AppendLine("       'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');")
            .AppendLine("       return false;")
            .AppendLine("   }else if(document.all." & Me.ddlAiteSakiSyubetu.ClientID & ".value == '5'){")
            '������ʂ��u5:�c�Ə��v�̏ꍇ�A�c�Ə��|�b�v�A�b�v���N������
            .AppendLine("       var strkbn='�c�Ə�';")
            .AppendLine("       var strClientCdID; ")
            .AppendLine("       var strClientMeiID; ")
            .AppendLine("       var blnTorikesi; ")
            .AppendLine("       strClientCdID = '" & Me.tbxAiteSakiCd.ClientID & "';")
            .AppendLine("       strClientMeiID = '" & Me.tbxAiteSakiMei.ClientID & "';")
            .AppendLine("       blnTorikesi = 'False';")
            .AppendLine("       objSrchWin = window.open('search_common.aspx?Kbn='+escape(strkbn)+'&FormName=" & Me.Form.Name & _
                                "&objCd='+strClientCdID+'&objMei='+strClientMeiID+'&strCd='+escape(eval('document.all.'+strClientCdID).value)+'&strMei='+escape(eval('document.all.'+strClientMeiID).value)+")
            .AppendLine("       '&blnDelete='+blnTorikesi+'&objRetrun='+objRetrun+'&hdnId='+hdnId, ")
            .AppendLine("       'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');")
            .AppendLine("       return false;")
            .AppendLine("   }else if(document.all." & Me.ddlAiteSakiSyubetu.ClientID & ".value == '7'){")
            '������ʂ��u7:�n��v�̏ꍇ�A�n��|�b�v�A�b�v���N������
            .AppendLine("       var strkbn='�n��';")
            .AppendLine("       var strClientCdID; ")
            .AppendLine("       var strClientMeiID; ")
            .AppendLine("       var blnTorikesi; ")
            .AppendLine("       strClientCdID = '" & Me.tbxAiteSakiCd.ClientID & "';")
            .AppendLine("       strClientMeiID = '" & Me.tbxAiteSakiMei.ClientID & "';")
            .AppendLine("       blnTorikesi = 'False';")
            .AppendLine("       objSrchWin = window.open('search_common.aspx?Kbn='+escape(strkbn)+'&FormName=" & Me.Form.Name & _
                                "&objCd='+strClientCdID+'&objMei='+strClientMeiID+'&strCd='+escape(eval('document.all.'+strClientCdID).value)+'&strMei='+escape(eval('document.all.'+strClientMeiID).value)+")
            .AppendLine("       '&blnDelete='+blnTorikesi+'&objRetrun='+objRetrun+'&hdnId='+hdnId, ")
            .AppendLine("       'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');")
            .AppendLine("       return false;")
            .AppendLine("   }")
            .AppendLine("}")

            .AppendLine("function fncAitesakiCdChange()")
            .AppendLine("{")
            '.AppendLine("   alert('hdnAiteSakiCd:'+document.getElementById('" & Me.hdnAiteSakiCd.ClientID & "').value);")
            '.AppendLine("   alert('tbxAiteSakiCd:'+document.getElementById('" & Me.tbxAiteSakiCd.ClientID & "').value);")
            '.AppendLine("   alert('hdnKensakuFlg1:'+document.getElementById('" & Me.hdnKensakuFlg.ClientID & "').value);")

            .AppendLine("   if(document.all." & Me.tbxAiteSakiCd.ClientID & ".value != document.getElementById('" & Me.hdnAiteSakiCd.ClientID & "').value)")
            .AppendLine("   {")
            .AppendLine("       document.getElementById('" & Me.hdnKensakuFlg.ClientID & "').value = '0';")
            .AppendLine("       document.getElementById('" & Me.hdnAiteSakiCd.ClientID & "').value = document.all." & Me.tbxAiteSakiCd.ClientID & ".value;")
            .AppendLine("   }")
            '.AppendLine("   alert('hdnKensakuFlg2:'+document.getElementById('" & Me.hdnKensakuFlg.ClientID & "').value);")
            .AppendLine("}")

            .AppendLine("function fncCloseWindow()")
            .AppendLine("{")
            .AppendLine("   window.close();")
            .AppendLine("}")

            .AppendLine("function fncNyuuryokuCheck(strKbn)")
            .AppendLine("{")
            '�������
            .AppendLine("   if(document.all." & Me.ddlAiteSakiSyubetu.ClientID & ".value == '')")
            .AppendLine("   {")
            .AppendLine("       alert('" & Messages.Instance.MSG013E.Replace("@PARAM1", "�������") & "');")
            .AppendLine("       document.all." & Me.ddlAiteSakiSyubetu.ClientID & ".focus();")
            .AppendLine("       return false; ")
            .AppendLine("   }")
            '�����R�[�h
            .AppendLine("   if(document.all." & Me.ddlAiteSakiSyubetu.ClientID & ".value != '0')")
            .AppendLine("   {")
            .AppendLine("       if(Trim(document.all." & Me.tbxAiteSakiCd.ClientID & ".value) == '')")
            .AppendLine("       {")
            .AppendLine("           alert('" & Messages.Instance.MSG013E.Replace("@PARAM1", "�����R�[�h") & "');")
            .AppendLine("           document.all." & Me.tbxAiteSakiCd.ClientID & ".focus();")
            .AppendLine("           return false; ")
            .AppendLine("       }")
            .AppendLine("   }")
            '���i
            .AppendLine("   if(document.all." & Me.ddlSyouhinCd.ClientID & ".value == '')")
            .AppendLine("   {")
            .AppendLine("       alert('" & Messages.Instance.MSG013E.Replace("@PARAM1", "���i�R�[�h") & "');")
            .AppendLine("       document.all." & Me.ddlSyouhinCd.ClientID & ".focus();")
            .AppendLine("       return false; ")
            .AppendLine("   }")
            '�������@
            .AppendLine("   if(document.all." & Me.ddlTyousaHouhou.ClientID & ".value == '')")
            .AppendLine("   {")
            .AppendLine("       alert('" & Messages.Instance.MSG013E.Replace("@PARAM1", "�������@") & "');")
            .AppendLine("       document.all." & Me.ddlTyousaHouhou.ClientID & ".focus();")
            .AppendLine("       return false; ")
            .AppendLine("   }")
            '�����i�敪�E�R�[�h�E�}�Ԃ̂����ꂩ�j���ύX
            .AppendLine("   if(document.all." & Me.ddlAiteSakiSyubetu.ClientID & ".value != '0')")
            .AppendLine("   {")
            '.AppendLine("   alert('hdnKensakuFlg�o�^:'+document.getElementById('" & Me.hdnKensakuFlg.ClientID & "').value);")
            .AppendLine("       if(document.getElementById('" & Me.hdnKensakuFlg.ClientID & "').value == '0')")
            .AppendLine("       {")
            .AppendLine("           alert('" & Messages.Instance.MSG2054E & "');")
            .AppendLine("           document.all." & Me.ddlAiteSakiSyubetu.ClientID & ".focus();")
            .AppendLine("           return false; ")
            .AppendLine("       }")
            .AppendLine("   }")

            .AppendLine("   var strMenoy;")
            '�H���X�������z
            .AppendLine("   if(!fncCheckMenoy(document.all." & Me.tbxKoumutenSeikyuuKingaku.ClientID & ".value))")
            .AppendLine("   {")
            .AppendLine("       alert('" & String.Format(Messages.Instance.MSG2055E, "�H���X�������z") & "');")
            .AppendLine("       document.all." & Me.tbxKoumutenSeikyuuKingaku.ClientID & ".focus();")
            .AppendLine("       return false; ")
            .AppendLine("   }")

            .AppendLine("   strMenoy = Trim(document.all." & Me.tbxKoumutenSeikyuuKingaku.ClientID & ".value).replace(/,/g,'');")
            .AppendLine("   if((strMenoy<0)||(strMenoy>2147483647))")
            .AppendLine("   {")
            .AppendLine("       alert('" & String.Format(Messages.Instance.MSG2056E, "�H���X�������z") & "');")
            .AppendLine("       document.all." & Me.tbxKoumutenSeikyuuKingaku.ClientID & ".focus();")
            .AppendLine("       return false; ")
            .AppendLine("   }")
            '���������z
            .AppendLine("   if(!fncCheckMenoy(document.all." & Me.tbxJituSeikyuuKingaku.ClientID & ".value))")
            .AppendLine("   {")
            .AppendLine("       alert('" & String.Format(Messages.Instance.MSG2055E, "���������z") & "');")
            .AppendLine("       document.all." & Me.tbxJituSeikyuuKingaku.ClientID & ".focus();")
            .AppendLine("       return false; ")
            .AppendLine("   }")

            .AppendLine("   strMenoy = Trim(document.all." & Me.tbxJituSeikyuuKingaku.ClientID & ".value).replace(/,/g,'');")
            .AppendLine("   if((strMenoy<0)||(strMenoy>2147483647))")
            .AppendLine("   {")
            .AppendLine("       alert('" & String.Format(Messages.Instance.MSG2056E, "���������z") & "');")
            .AppendLine("       document.all." & Me.tbxJituSeikyuuKingaku.ClientID & ".focus();")
            .AppendLine("       return false; ")
            .AppendLine("   }")
            .AppendLine("   return true; ")
            .AppendLine("}")
            .AppendLine("   //DB�����m�F ")
            .AppendLine("   function fncDbSyoriKakunin(strMessage) ")
            .AppendLine("   { ")
            .AppendLine("       if(confirm(strMessage)) ")
            .AppendLine("       { ")
            .AppendLine("           document.getElementById('" & Me.hdnDbFlg.ClientID & "').value = '1'; ")
            .AppendLine("           document.forms[0].submit(); ")
            .AppendLine("       } ")
            .AppendLine("       else ")
            .AppendLine("       { ")
            .AppendLine("           return false; ")
            .AppendLine("       } ")
            .AppendLine("   } ")



            '���zcheck
            .AppendLine("function fncCheckMenoy(strMenoy) ")
            .AppendLine("{ ")
            .AppendLine("	strMenoy = Trim(strMenoy); ")
            .AppendLine("	strMenoy = strMenoy.replace(/,/g,''); ")
            .AppendLine("	if(strMenoy != '') ")
            .AppendLine("	{ ")
            .AppendLine("		if(isNaN(strMenoy) || (strMenoy.indexOf('+') != -1) || (strMenoy.indexOf('-') != -1) || (strMenoy.indexOf('.') != -1))  ")
            .AppendLine("		{ ")
            .AppendLine("			return false; ")
            .AppendLine("		} ")
            .AppendLine("	} ")
            .AppendLine("	return true; ")
            .AppendLine("} ")


            .AppendLine("//���p�p�����`�F�b�N ")
            .AppendLine("function chkHankakuEisuuji(strInputString) ")
            .AppendLine("{ ")
            .AppendLine("	if(strInputString.match(/[^a-z\^A-Z\^0-9]/)!=null) ")
            .AppendLine("	{ ")
            .AppendLine("		return false;  ")
            .AppendLine("	} ")
            .AppendLine("	else{ ")
            .AppendLine("		return true;  ")
            .AppendLine("	} ")
            .AppendLine("} ")
            .AppendLine("	function LTrim(str)  ")
            .AppendLine("	{  ")
            .AppendLine("		var i; ")
            .AppendLine("		for(i=0;i<str.length;i++)  ")
            .AppendLine("		{  ")
            .AppendLine("			if(str.charAt(i)!=' '&&str.charAt(i)!='�@')break;  ")
            .AppendLine("		}  ")
            .AppendLine("		str=str.substring(i,str.length);  ")
            .AppendLine("		return str;  ")
            .AppendLine("	}  ")
            .AppendLine("	function RTrim(str)  ")
            .AppendLine("	{  ")
            .AppendLine("		var i;  ")
            .AppendLine("		for(i=str.length-1;i>=0;i--)  ")
            .AppendLine("		{  ")
            .AppendLine("			if(str.charAt(i)!=' '&&str.charAt(i)!='�@')break;  ")
            .AppendLine("		}  ")
            .AppendLine("		str=str.substring(0,i+1);  ")
            .AppendLine("		return str;  ")
            .AppendLine("	} ")
            .AppendLine("	function Trim(str)  ")
            .AppendLine("	{  ")
            .AppendLine("		return LTrim(RTrim(str));  ")
            .AppendLine("	}  ")
            .AppendLine("	 ")
            .AppendLine("</script>")
        End With

        Page.ClientScript.RegisterStartupScript(Page.GetType, "myJavaScript", sbScript.ToString)

    End Sub




    ''' <summary>�̔����i�e�[�u�����쐬����</summary>
    ''' <param name="dtHanbaiKakakuOk">�̔����i�e�[�u��</param>
    Public Sub SetHanbaiKakaku(ByRef dtHanbaiKakakuOk As Data.DataTable)

        Dim dc As Data.DataColumn
        dc = New Data.DataColumn
        dc.ColumnName = "ins_upd_flg"       '�ǉ��X�VFLG(0:�ǉ�; 1:�X�V)
        dtHanbaiKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "aitesaki_syubetu"  '�������
        dtHanbaiKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "aitesaki_cd"       '�����R�[�h
        dtHanbaiKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "syouhin_cd"        '���i�R�[�h
        dtHanbaiKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tys_houhou_no"     '�������@NO
        dtHanbaiKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "torikesi"          '���
        dtHanbaiKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "koumuten_seikyuu_gaku"  '�H���X�������z
        dtHanbaiKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "koumuten_seikyuu_gaku_henkou_flg"   '�H���X�������z�ύXFLG
        dtHanbaiKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "jitu_seikyuu_gaku"  '���������z
        dtHanbaiKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "jitu_seikyuu_gaku_henkou_flg"   '���������z�ύXFLG
        dtHanbaiKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "koukai_flg"        '���J�t���O
        dtHanbaiKakakuOk.Columns.Add(dc)

    End Sub

    ''' <summary>���b�Z�[�W�\��</summary>
    ''' <param name="strMessage">���b�Z�[�W</param>
    Private Sub ShowMessage(ByVal strMessage As String)
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("function window.onload() {")
            .AppendLine("	alert('" & strMessage & "');")
            .AppendLine("}")
        End With
        '�y�[�W�����ŁA�N���C�A���g���̃X�N���v�g �u���b�N���o�͂��܂�
        ClientScript.RegisterStartupScript(Me.GetType(), "ShowMessage", csScript.ToString, True)
    End Sub

    ''' <summary>
    ''' DB�����m�F���b�Z�[�W�\��
    ''' </summary>
    ''' <param name="strMessage">���b�Z�[�W</param>
    ''' <history>2011/04/11 �ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Private Sub ShowKakuninMessage(ByVal strMessage As String)

        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("fncDbSyoriKakunin('" & strMessage & "'); ")
        End With
        '�y�[�W�����ŁA�N���C�A���g���̃X�N���v�g �u���b�N���o�͂��܂�
        ClientScript.RegisterStartupScript(Me.GetType(), "ShowTourokuMessage", csScript.ToString, True)
    End Sub
End Class
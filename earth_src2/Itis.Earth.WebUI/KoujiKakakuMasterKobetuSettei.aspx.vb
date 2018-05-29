Imports Itis.Earth.BizLogic
Imports Itis.Earth.DataAccess

Partial Public Class KoujiKakakuMasterKobetuSettei
    Inherits System.Web.UI.Page

    Private hanbaiKakakuSearchListLogic As New HanbaiKakakuMasterLogic
    Private commonCheck As New CommonCheck

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '�����`�F�b�N
        Dim commonChk As New CommonCheck
        Dim strUserID As String = ""
        Dim blnEigyouKengen As Boolean
        blnEigyouKengen = commonChk.CommonNinnsyou(strUserID, "koj_gyoumu_kengen")
        If Not blnEigyouKengen Then
            '�G���[��ʂ֑J�ڂ��āA�G���[���b�Z�[�W��\������
            Context.Items("strFailureMsg") = Messages.Instance.MSG2020E
            Server.Transfer("CommonErr.aspx")
        End If

        'javascript�쐬
        MakeJavascript()

        If Not IsPostBack Then
            '�Q�Ɨ����Ǘ��e�[�u����o�^����B
            commonCheck.SetURL(Me, strUserID)
            If Not Request("sendSearchTerms") Is Nothing Then
                '������
                Call SetInitData(CStr(Request("sendSearchTerms")))
                Me.hdnAiteSakiSyubetu.Value = Me.ddlAiteSakiSyubetu.SelectedValue.Trim
                Me.hdnAiteSakiCd.Value = Me.tbxAiteSakiCd.Text.Trim
                hdnKojKaisyaCd.Value = Me.tbxKojKaisyaCd.Text.Trim
            Else
                '������
                Call SetInitData(String.Empty)
            End If

        Else
            Me.hdnKensakuFlg.Value = "1"
            Me.hdnKensakuFlg2.Value = "1"
            '�������
            Call Me.SetAitesakiSyubetu(Me.ddlAiteSakiSyubetu.SelectedValue.Trim)
            Me.hdnAiteSakiSyubetu.Value = Me.ddlAiteSakiSyubetu.SelectedValue.Trim
            '�����R�[�h
            Call Me.SetAitesakiCd(Me.tbxAiteSakiCd.Text.Trim)
            Me.hdnAiteSakiCd.Value = Me.tbxAiteSakiCd.Text.Trim
            '���i�R�[�h
            Call Me.SetSyouhin(Me.ddlSyouhinCd.SelectedValue.Trim)
            '�H�����
            Call Me.SetKojKaisya(Me.tbxKojKaisyaCd.Text.Trim)
            hdnKojKaisyaCd.Value = Me.tbxKojKaisyaCd.Text.Trim
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

        Me.tbxKojKaisyaCd.Attributes.Add("onChange", "fncKojKaisyaCdChange();")
        Me.tbxKojKaisyaCd.Attributes.Add("onBlur", "fncToUpper(this);")
        '�������{�^��
        Me.btnAiteSakiCd.Attributes.Add("onClick", "fncAiteSakiSearch();return false;")
        '��o�^��{�^��
        Me.btnTouroku.Attributes.Add("onClick", "if(!fncNyuuryokuCheck()){return false;}")

    End Sub

    ''' <summary>�����f�[�^���Z�b�g</summary>
    ''' <param name="sendSearchTerms">�p�����[�^</param>
    Private Sub SetInitData(ByVal sendSearchTerms As String)

        Me.hdnKensakuFlg.Value = "1"
        Me.hdnKensakuFlg2.Value = "1"
        '�H����А����L��
        Me.ddlKojGaisyaSeikyuuUmu.Items.Insert(0, New ListItem("��", "0"))
        Me.ddlKojGaisyaSeikyuuUmu.Items.Insert(1, New ListItem("�L", "1"))

        '�����L��

        Me.ddlSeikyuuUmu.Items.Insert(0, New ListItem("��", "0"))
        Me.ddlSeikyuuUmu.Items.Insert(1, New ListItem("�L", "1"))

        If sendSearchTerms <> String.Empty Then
            Dim arrSearchTerm() As String = Split(sendSearchTerms, "$$$")
            '�������
            Dim strAiteSakiSyubetu As String
            '�����R�[�h
            Dim strAiteSakiCd As String
            '���i�R�[�h
            Dim strSyouhinCd As String
            '�H����ЃR�[�h
            Dim strKojKaisyaCd As String

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

            '�H�����
            If arrSearchTerm.Length > 3 Then
                strKojKaisyaCd = arrSearchTerm(3).Trim
            Else
                strKojKaisyaCd = String.Empty
            End If
            hdnKojKaisyaCd.Value = strKojKaisyaCd
            Call Me.SetKojKaisya(strKojKaisyaCd)

            If arrSearchTerm.Length > 3 Then
                '�������s��
                Call Me.KensakuSyori(strAiteSakiSyubetu, strAiteSakiCd, strSyouhinCd, strKojKaisyaCd)
            Else
                '���
                Me.chkTorikesi.Checked = False
                '������z
                Me.tbxUriGaku.Text = "0"
                '�H����А����L��
                Me.ddlKojGaisyaSeikyuuUmu.SelectedValue = "0"
                '�����L��
                Me.ddlSeikyuuUmu.SelectedValue = "0"
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
            Call Me.SetKojKaisya(String.Empty)
            hdnKojKaisyaCd.Value = String.Empty
            '���
            Me.chkTorikesi.Checked = False
            '������z
            Me.tbxUriGaku.Text = "0"
            '�H����А����L��
            Me.ddlKojGaisyaSeikyuuUmu.SelectedValue = "0"
            '�����L��
            Me.ddlSeikyuuUmu.SelectedValue = "0"
        End If

    End Sub
    ''' <summary> �H����Ђ�ݒ肷��</summary>
    ''' <param name="strSetKojKaisyaCd">�H����ЃR�[�h</param>
    Sub SetKojKaisya(ByVal strSetKojKaisyaCd As String)
        Dim KojKakuLogic As New KojKakakuMasterLogic
        Dim dtSearchTable As New DataTable

        If strSetKojKaisyaCd = "ALLAL" Then
            Me.tbxKojKaisyaMei.Text = "�w�薳��"
        ElseIf strSetKojKaisyaCd = "" Then
            Me.tbxKojKaisyaMei.Text = ""
        Else
            dtSearchTable = KojKakuLogic.GetKojKaisyaKensaku(strSetKojKaisyaCd)
            If dtSearchTable.Rows.Count > 0 Then
                Me.tbxKojKaisyaMei.Text = dtSearchTable.Rows(0).Item("tys_kaisya_mei")
            End If
        End If

        Me.tbxKojKaisyaCd.Text = strSetKojKaisyaCd
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
    Sub SetSyouhin(ByVal strSyouhinCd As String)

        Dim dtSyouhin As Data.DataTable
        '������ʃf�[�^���擾����
        Dim KojKakakuSearchListLogic As New KojKakakuMasterLogic
        dtSyouhin = KojKakakuSearchListLogic.GetSyouhin()
        '�f�[�^��ݒ肷��
        Me.ddlSyouhinCd.DataTextField = "syouhin"
        Me.ddlSyouhinCd.DataValueField = "syouhin_cd"
        Me.ddlSyouhinCd.DataSource = dtSyouhin
        Me.ddlSyouhinCd.DataBind()
        Me.ddlSyouhinCd.Items.Insert(0, New ListItem("", ""))
        '�I�������l��ݒ肷��
        If strSyouhinCd <> String.Empty Then
            Dim intcount
            For intcount = 0 To Me.ddlSyouhinCd.Items.Count - 1
                If Me.ddlSyouhinCd.Items(intcount).Value = strSyouhinCd Then
                    Me.ddlSyouhinCd.SelectedIndex = intcount
                    Exit For
                End If
            Next
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
                .Append(commonCheck.ChkHankakuEisuuji(Me.tbxAiteSakiCd.Text, "�����R�[�h"))
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
    ''' <param name="strKojKaisyaCd">�H����ЃR�[�h</param>
    ''' <remarks></remarks>
    Private Sub KensakuSyori(ByVal strAiteSakiSyubetu As String, ByVal strAiteSakiCd As String, ByVal strSyouhinCd As String, ByVal strKojKaisyaCd As String)

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
            Dim KojKakakuSearchListLogic As New KojKakakuMasterLogic
            Dim dtKojKakaku As New Data.DataTable
            dtKojKakaku = KojKakakuSearchListLogic.GetHanbaiKakakuKobeituSettei(strAiteSakiSyubetu, strAiteSakiCd, strSyouhinCd, strKojKaisyaCd)

            If dtKojKakaku.Rows.Count > 0 Then
                With dtKojKakaku.Rows(0)

                    '���
                    If .Item("torikesi").ToString.Trim.Equals("0") Then
                        Me.chkTorikesi.Checked = False
                    Else
                        Me.chkTorikesi.Checked = True
                    End If

                    '������z
                    If Not .Item("uri_gaku").ToString.Trim.Equals(String.Empty) Then
                        Me.tbxUriGaku.Text = FormatNumber(.Item("uri_gaku").ToString.Trim, 0)
                    Else
                        Me.tbxUriGaku.Text = "0"
                    End If

                    '�H����А����L��
                    If .Item("kojumu").ToString.Trim.Equals("1") Then
                        Me.ddlKojGaisyaSeikyuuUmu.SelectedValue = "1"
                    Else
                        Me.ddlKojGaisyaSeikyuuUmu.SelectedValue = "0"
                    End If

                    '�����L��
                    If .Item("seikyuumu").ToString.Trim.Equals("1") Then
                        Me.ddlSeikyuuUmu.SelectedValue = "1"
                    ElseIf .Item("seikyuumu").ToString.Trim.Equals("0") Then
                        Me.ddlSeikyuuUmu.SelectedValue = "0"
                    Else
                        Me.ddlSeikyuuUmu.SelectedValue = ""
                    End If
                End With
            Else
                '���
                Me.chkTorikesi.Checked = False
                '�H���X�������z
                Me.tbxUriGaku.Text = "0"
                '������z
                Me.tbxUriGaku.Text = "0"
                '�H����А����L��
                Me.ddlKojGaisyaSeikyuuUmu.SelectedValue = "0"
                '�����L��
                Me.ddlSeikyuuUmu.SelectedValue = "0"
            End If
        Else
            '���
            Me.chkTorikesi.Checked = False
            '�H���X�������z
            Me.tbxUriGaku.Text = "0"
            '������z
            Me.tbxUriGaku.Text = "0"
            '�H����А����L��
            Me.ddlKojGaisyaSeikyuuUmu.SelectedValue = "0"
            '�����L��
            Me.ddlSeikyuuUmu.SelectedValue = "0"
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
        Dim strKojKaisyaCd As String
        strKojKaisyaCd = Me.tbxKojKaisyaCd.Text.Trim
        Dim KojKakakuSearchListLogic As New KojKakakuMasterLogic
        '���݃`�F�c�N
        Dim dtKojKakaku As New Data.DataTable
        dtKojKakaku = KojKakakuSearchListLogic.CheckSonzai(strAiteSakiSyubetu, strAiteSakiCd, strSyouhinCd, strKojKaisyaCd)

        If dtKojKakaku.Rows.Count > 0 Then
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
        Dim strKojkaisyaCd As String
        strKojkaisyaCd = Me.tbxKojKaisyaCd.Text

        '�p�����[�^
        Dim dtKojKakakuOk As New Data.DataTable
        Call Me.SetKojKakaku(dtKojKakakuOk)
        Dim dr As Data.DataRow
        dr = dtKojKakakuOk.NewRow
        dr.Item("aitesaki_syubetu") = strAiteSakiSyubetu
        dr.Item("aitesaki_cd") = strAiteSakiCd
        dr.Item("syouhin_cd") = strSyouhinCd
        dr.Item("koj_gaisya_cd") = Left(Right("       " & strKojkaisyaCd.Trim, 7), 5).Trim
        dr.Item("koj_gaisya_jigyousyo_cd") = Right(strKojkaisyaCd.Trim, 2).Trim
        If Me.chkTorikesi.Checked Then
            dr.Item("torikesi") = "1"
        Else
            dr.Item("torikesi") = "0"
        End If
        Dim strUriGaku As String = Me.tbxUriGaku.Text.Trim.Replace(",", String.Empty)
        dr.Item("uri_gaku") = IIf(strUriGaku.Equals(String.Empty), "0", strUriGaku)
        dr.Item("seikyuu_umu") = Me.ddlSeikyuuUmu.SelectedValue.Trim
        dr.Item("koj_gaisya_seikyuu_umu") = Me.ddlKojGaisyaSeikyuuUmu.SelectedValue.Trim
   

        Select Case strKbn
            Case "insert"
                dr.Item("ins_upd_flg") = "0"
            Case "update"
                dr.Item("ins_upd_flg") = "1"
        End Select

        dtKojKakakuOk.Rows.Add(dr)
        Dim KojKakakuSearchListLogic As New KojKakakuMasterLogic
        If KojKakakuSearchListLogic.SetKojKakakuKobeituSettei(dtKojKakakuOk, strUserId).Equals(False) Then

            Call Me.ShowMessage(Messages.Instance.MSG2058E)

        Else

            '����ʂ����
            ClientScript.RegisterStartupScript(Me.GetType(), "ShowMessage", "fncCloseWindow();", True)
        End If

    End Sub
    ''' <summary>�H�����i�e�[�u�����쐬����</summary>
    ''' <param name="dtKojKakakuOk">�H�����i�e�[�u��</param>
    Public Sub SetKojKakaku(ByRef dtKojKakakuOk As Data.DataTable)

        Dim dc As Data.DataColumn
        dc = New Data.DataColumn
        dc.ColumnName = "ins_upd_flg"       '�ǉ��X�VFLG(0:�ǉ�; 1:�X�V)
        dtKojKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "aitesaki_syubetu"  '�������
        dtKojKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "aitesaki_cd"       '�����R�[�h
        dtKojKakakuOk.Columns.Add(dc)

        dc = New Data.DataColumn
        dc.ColumnName = "syouhin_cd"        '���i�R�[�h
        dtKojKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "torikesi"          '���
        dtKojKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "koj_gaisya_cd"  '�H����ЃR�[�h
        dtKojKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "koj_gaisya_jigyousyo_cd"  '�H����ЃR�[�h
        dtKojKakakuOk.Columns.Add(dc)

        dc = New Data.DataColumn
        dc.ColumnName = "koj_gaisya_seikyuu_umu"   '�H����А����L��
        dtKojKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "seikyuu_umu"  '�����L��
        dtKojKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "uri_gaku"   '������z
        dtKojKakakuOk.Columns.Add(dc)

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
            .AppendLine("           document.all." & Me.tbxAiteSakiMei.ClientID & ".value = '�����Ȃ�';")
            .AppendLine("			break; ")
            .AppendLine("		default: ")
            .AppendLine("           document.all." & Me.tbxAiteSakiCd.ClientID & ".value = '';")
            .AppendLine("           document.all." & Me.tbxAiteSakiCd.ClientID & ".disabled = false;")
            .AppendLine("           document.all." & Me.btnAiteSakiCd.ClientID & ".disabled = false;")
            .AppendLine("           document.all." & Me.tbxAiteSakiMei.ClientID & ".value = '';")
            .AppendLine("	} ")
            '.AppendLine("   alert('hdnKensakuFlg:'+document.getElementById('" & Me.hdnKensakuFlg.ClientID & "').value);")
            .AppendLine("}")
            .AppendLine("function fncKojKaisyaSearch(){")
            .AppendLine("       var objRetrun = '" & Me.hdnKensakuFlg2.ClientID & "';")
            .AppendLine("       var hdnId = '" & Me.hdnKojKaisyaCd.ClientID & "';")
            .AppendLine(" if (document.all." & Me.tbxKojKaisyaCd.ClientID & ".value=='ALLAL'){")
            .AppendLine("       document.getElementById('" & Me.hdnKensakuFlg2.ClientID & "').value = '1';")
            .AppendLine(" document.all." & Me.tbxKojKaisyaMei.ClientID & ".value='�w�薳��'}else{")
            .AppendLine("       objSrchWin = window.open('search_common.aspx?blnDelete=True&Kbn='+escape('�H�����')+'&FormName=" & _
            Me.Page.Form.Name & "&objCd=" & _
              tbxKojKaisyaCd.ClientID & _
                       "&objMei=" & tbxKojKaisyaMei.ClientID & _
                       "&strCd='+escape(eval('document.all.'+'" & _
                       tbxKojKaisyaCd.ClientID & "').value)+'&strMei='+escape(eval('document.all.'+'" & _
                       tbxKojKaisyaMei.ClientID & "').value)+'&objRetrun='+objRetrun+'&hdnId='+hdnId, 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');")
            .AppendLine("}")
            .AppendLine("       return false;")
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

            .AppendLine("   if(document.all." & Me.tbxAiteSakiCd.ClientID & ".value != document.getElementById('" & Me.hdnAiteSakiCd.ClientID & "').value)")
            .AppendLine("   {")
            .AppendLine("       document.getElementById('" & Me.hdnKensakuFlg.ClientID & "').value = '0';")
            .AppendLine("       document.getElementById('" & Me.hdnAiteSakiCd.ClientID & "').value = document.all." & Me.tbxAiteSakiCd.ClientID & ".value;")
            .AppendLine("   }")
            .AppendLine("}")
            .AppendLine("function fncKojKaisyaCdChange()")
            .AppendLine("{")

            .AppendLine("   if(document.all." & Me.tbxKojKaisyaCd.ClientID & ".value != document.getElementById('" & Me.hdnKojKaisyaCd.ClientID & "').value)")
            .AppendLine("   {")
            .AppendLine("       document.getElementById('" & Me.hdnKensakuFlg2.ClientID & "').value = '0';")
            .AppendLine("       document.getElementById('" & Me.hdnKojKaisyaCd.ClientID & "').value = document.all." & Me.tbxKojKaisyaCd.ClientID & ".value;")
            .AppendLine("   }")
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
            .AppendLine("       if(chkHankakuEisuuji(document.all." & Me.tbxAiteSakiCd.ClientID & ".value) == false)")
            .AppendLine("       {")
            .AppendLine("           alert('" & String.Format(Messages.Instance.MSG2005E, "�����R�[�h").ToString & "');")
            .AppendLine("           document.all." & Me.tbxAiteSakiCd.ClientID & ".focus();")
            .AppendLine("           return false; ")
            .AppendLine("       }")

            .AppendLine("   }")
            '���i
            .AppendLine("   if(document.all." & Me.ddlSyouhinCd.ClientID & ".value == '')")
            .AppendLine("   {")
            .AppendLine("       alert('" & Messages.Instance.MSG013E.Replace("@PARAM1", "���i") & "');")
            .AppendLine("       document.all." & Me.ddlSyouhinCd.ClientID & ".focus();")
            .AppendLine("       return false; ")
            .AppendLine("   }")
            '�H�����
            .AppendLine("   if(document.all." & Me.tbxKojKaisyaCd.ClientID & ".value == '')")
            .AppendLine("   {")
            .AppendLine("       alert('" & Messages.Instance.MSG013E.Replace("@PARAM1", "�H�����") & "');")
            .AppendLine("       document.all." & Me.tbxKojKaisyaCd.ClientID & ".focus();")
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
            .AppendLine("       if(document.getElementById('" & Me.hdnKensakuFlg2.ClientID & "').value == '0')")
            .AppendLine("       {")
            .AppendLine("           alert('" & Replace(Messages.Instance.MSG2054E, "�����", "�H�����") & "');")
            .AppendLine("           document.all." & Me.tbxKojKaisyaCd.ClientID & ".focus();")
            .AppendLine("           return false; ")
            .AppendLine("       }")
            .AppendLine("   var strMenoy;")
            '�H���X�������z
            .AppendLine("   if(!fncCheckMenoy(document.all." & Me.tbxUriGaku.ClientID & ".value))")
            .AppendLine("   {")
            .AppendLine("       alert('" & String.Format(Messages.Instance.MSG2055E, "������z") & "');")
            .AppendLine("       document.all." & Me.tbxUriGaku.ClientID & ".focus();")
            .AppendLine("       return false; ")
            .AppendLine("   }")

            .AppendLine("   strMenoy = Trim(document.all." & Me.tbxUriGaku.ClientID & ".value).replace(/,/g,'');")
            .AppendLine("   if((strMenoy<0)||(strMenoy>2147483647))")
            .AppendLine("   {")
            .AppendLine("       alert('" & String.Format(Messages.Instance.MSG2056E, "������z") & "');")
            .AppendLine("       document.all." & Me.tbxUriGaku.ClientID & ".focus();")
            .AppendLine("       return false; ")
            .AppendLine("   }")
            '====================��2015/11/17 �ǉ���====================
            .AppendLine("   strMenoy = (strMenoy == '')?'0':strMenoy;")
            .AppendLine("   var strSeikyuuUmu = document.all." & Me.ddlSeikyuuUmu.ClientID & ".value;")
            '������z��0�~�ł͂Ȃ��ꍇ�@���@�����L���@�u���v�@�I����
            .AppendLine("   if((strMenoy != 0) && (strSeikyuuUmu == '0'))")
            .AppendLine("   {")
            .AppendLine("       alert('" & Messages.Instance.MSG2078E & "');")
            .AppendLine("       document.all." & Me.ddlSeikyuuUmu.ClientID & ".focus();")
            .AppendLine("       return false; ")
            .AppendLine("   }")
            '������z��0�~�̏ꍇ�@���@�����L���@�u�L�v�@�I����
            .AppendLine("   if((strMenoy == 0) && (strSeikyuuUmu == '1'))")
            .AppendLine("   {")
            .AppendLine("       alert('" & Messages.Instance.MSG2079E & "');")
            .AppendLine("       document.all." & Me.ddlSeikyuuUmu.ClientID & ".focus();")
            .AppendLine("       return false; ")
            .AppendLine("   }")
            '====================��2015/11/17 �ǉ���====================
          
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
    ''' <history>2011/09/15 ��(��A���V�X�e����)�@�V�K�쐬</history>
    Private Sub ShowKakuninMessage(ByVal strMessage As String)

        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("fncDbSyoriKakunin('" & strMessage & "'); ")
        End With
        '�y�[�W�����ŁA�N���C�A���g���̃X�N���v�g �u���b�N���o�͂��܂�
        ClientScript.RegisterStartupScript(Me.GetType(), "ShowTourokuMessage", csScript.ToString, True)
    End Sub

End Class
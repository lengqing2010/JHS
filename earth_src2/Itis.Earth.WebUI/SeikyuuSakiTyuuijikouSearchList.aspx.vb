Imports Itis.Earth.BizLogic
Imports Itis.Earth.DataAccess


Partial Public Class SeikyuuSakiTyuuijikouSearchList
    Inherits System.Web.UI.Page

    ''' <summary>�����}�X�^</summary>
    ''' <remarks>�����}�X�^�p�@�\��񋟂���</remarks>
    ''' <history>
    ''' <para>2011/02/24�@�ԗ�(��A���V�X�e����)�@�V�K�쐬</para>
    ''' </history>
    Private seikyuuSakiTyuuijikouLogic As New SeikyuuSakiTyuuijikouLogic
    Private commonCheck As New CommonCheck
    Protected scrollHeight As Integer = 0
    Protected setFlg As Boolean = False

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '�����`�F�b�N(�o���Ɩ�����)
        Dim commonChk As New CommonCheck
        Dim strUserID As String = ""
        Dim blnKengen As Boolean
        blnKengen = commonChk.CommonNinnsyou(strUserID, "keiri_gyoumu_kengen")

        '�u�V�K�o�^�v�{�^����ݒ肷��
        setFlg = Me.SetBtnTouroku()

        'JavaScript���쐬
        Call Me.MakeJavaScript()

        If Not IsPostBack Then
            '�Q�Ɨ����Ǘ��e�[�u����o�^����B
            commonCheck.SetURL(Me, strUserID)

            If Not Request.QueryString("sendSearchTerms") Is Nothing Then
                '������
                Call Syokika(CStr(Request.QueryString("sendSearchTerms")))
            Else
                '������
                Call Syokika(String.Empty)
            End If
        Else

            'DIV��\��
            CloseCover()
        End If

        '��ʏc�X�N���[����ݒ肷��
        scrollHeight = ViewState("scrollHeight")

        '�u����v�{�^��
        Me.btnClose.Attributes.Add("onClick", "fncClose();return false;")
        '�u�N���A�v�{�^��
        Me.btnClear.Attributes.Add("onClick", "fncClear();return false;")
        '�u�������s�v�{�^��
        Me.btnKensakujiltukou.Attributes.Add("onClick", "if(!fncNyuuryokuCheck('kensaku')){return false;}else{fncShowModal();}")
        '�uCSV�o�́v�{�^��
        Me.btnCsvOutput.Attributes.Add("onClick", "fncShowModal();")
        '�u�����v�{�^��
        Me.btnSeikyuusakiSearch.Attributes.Add("onClick", "fncSeikyuusakiPopup();return false;")

        If setFlg.Equals(True) Then
            '��V�K�o�^�
            Me.btnTouroku.Attributes.Add("onClick", "fncTourokuPopup('btn','','','','');return false;")
        End If
    End Sub

    ''' <summary>������</summary> 
    Private Sub Syokika(ByVal sendSearchTerms As String)

        '������敪
        Dim strSeikyuusakiKbn As String = String.Empty
        '������R�[�h
        Dim strSeikyuusakiCd As String = String.Empty
        '������}��
        Dim strSeikyuusakiBrc As String = String.Empty

        If Not sendSearchTerms.Equals(String.Empty) Then
            '�p�����[�^
            Dim arrSearchTerm() As String = Split(sendSearchTerms, "$$$")

            '������敪
            strSeikyuusakiKbn = arrSearchTerm(0).Trim
            '������敪��ݒ肷��
            Call Me.SetSeikyuusakiKbn(strSeikyuusakiKbn)

            '������R�[�h
            If arrSearchTerm.Length > 1 Then
                strSeikyuusakiCd = arrSearchTerm(1).Trim
                '������R�[�h��ݒ肷��
                Call Me.SetSeikyuusakiCd(strSeikyuusakiCd)
            Else
                Call Me.SetSeikyuusakiCd(String.Empty)
            End If

            '������}��
            If arrSearchTerm.Length > 2 Then
                strSeikyuusakiBrc = arrSearchTerm(2).Trim
                '������}�Ԃ�ݒ肷��
                Call Me.SetSeikyuusakiBrc(strSeikyuusakiBrc)
            Else
                Call Me.SetSeikyuusakiBrc(String.Empty)
            End If
        Else
            '������敪��ݒ肷��
            Call Me.SetSeikyuusakiKbn(String.Empty)

            '������R�[�h��ݒ肷��
            Call Me.SetSeikyuusakiCd(String.Empty)

            '������}�Ԃ�ݒ肷��
            Call Me.SetSeikyuusakiBrc(String.Empty)
        End If

        '�����於��ݒ肷��
        Call Me.SetSeikyuusakiMei()

        '��ʃR�[�h��ݒ肷��
        Call Me.SetSyubetuCd()

        '�d�v�x��ݒ肷��
        Call Me.SetJyuuyoudo()

        '�������ߓ���ݒ肷��
        Call Me.SetSeikyuuSimeDate()

        '�������K������ݒ肷��
        Call Me.SetSeikyuusyoHittykDate()

        '����͌����ΏۊO
        Me.chkKensakuTaisyouGai.Checked = True

        '�p�����[�^��3�i������敪�E�R�[�h�E�}�ԁj����ꍇ�A
        If (Not strSeikyuusakiKbn.Trim.Equals(String.Empty)) AndAlso (Not strSeikyuusakiCd.Trim.Equals(String.Empty)) AndAlso (Not strSeikyuusakiBrc.Trim.Equals(String.Empty)) Then
            '�������āA���׃f�[�^��\������
            Call Me.KensakuSyori(False)
        Else
            '���׃e�[�u����ݒ肷��
            Me.grdMeisai.DataSource = Nothing
            Me.grdMeisai.DataBind()

            '�\�[�g���{�^����ݒ肷��
            Call Me.SetSortButton(False)
        End If

    End Sub

    ''' <summary>������敪��ݒ肷��</summary> 
    Private Sub SetSeikyuusakiKbn(ByVal strSeikyuusakiKbn As String)

        '�󔒍s
        Me.ddlSeikyuusakiKbn.Items.Insert(0, New ListItem(String.Empty, String.Empty))
        '�����X
        Me.ddlSeikyuusakiKbn.Items.Insert(1, New ListItem("�����X", "0"))
        '�������
        Me.ddlSeikyuusakiKbn.Items.Insert(2, New ListItem("�������", "1"))

        '�\���̒l��ݒ肷��
        If Not strSeikyuusakiKbn.Trim.Equals(String.Empty) Then
            Try
                Me.ddlSeikyuusakiKbn.SelectedValue = strSeikyuusakiKbn
            Catch ex As Exception
                Me.ddlSeikyuusakiKbn.SelectedValue = String.Empty
            End Try
        Else
            Me.ddlSeikyuusakiKbn.SelectedValue = String.Empty
        End If

    End Sub

    ''' <summary>������R�[�h��ݒ肷��</summary> 
    Private Sub SetSeikyuusakiCd(ByVal strSeikyuusakiCd As String)
        '�\���̒l��ݒ肷��
        If Not strSeikyuusakiCd.Trim.Equals(String.Empty) Then
            Me.tbxSeikyuusakiCd.Text = strSeikyuusakiCd.Trim
        Else
            Me.tbxSeikyuusakiCd.Text = String.Empty
        End If
    End Sub

    ''' <summary>������}�Ԃ�ݒ肷��</summary> 
    Private Sub SetSeikyuusakiBrc(ByVal strSeikyuusakiBrc As String)
        '�\���̒l��ݒ肷��
        If Not strSeikyuusakiBrc.Trim.Equals(String.Empty) Then
            Me.tbxSeikyuusakiBrc.Text = strSeikyuusakiBrc.Trim
        Else
            Me.tbxSeikyuusakiBrc.Text = String.Empty
        End If
    End Sub

    ''' <summary>�����於��ݒ肷��</summary> 
    Private Sub SetSeikyuusakiMei()
        '������敪
        Dim strSeikyuusakiKbn As String = Me.ddlSeikyuusakiKbn.SelectedValue.Trim
        '������R�[�h
        Dim strSeikyuusakiCd As String = Me.tbxSeikyuusakiCd.Text.Trim
        '������}��
        Dim strSeikyuusakiBrc As String = Me.tbxSeikyuusakiBrc.Text.Trim

        '������(�敪�E�R�[�h�E�}�ԁj����ꍇ
        If (Not strSeikyuusakiKbn.Trim.Equals(String.Empty)) AndAlso (Not strSeikyuusakiCd.Trim.Equals(String.Empty)) AndAlso (Not strSeikyuusakiBrc.Trim.Equals(String.Empty)) Then
            '�����於���擾����
            Dim dtSeikyuusakiMei As New Data.DataTable
            dtSeikyuusakiMei = seikyuuSakiTyuuijikouLogic.GetSeikyuusakiMei(strSeikyuusakiKbn, strSeikyuusakiCd, strSeikyuusakiBrc)
            '�����於�̕\���l��ݒ肷��
            If dtSeikyuusakiMei.Rows.Count > 0 Then
                Me.tbxSeikyuusakiMei.Text = dtSeikyuusakiMei.Rows(0).Item(0).ToString.Trim
            Else
                Me.tbxSeikyuusakiMei.Text = String.Empty
            End If
        Else
            Me.tbxSeikyuusakiMei.Text = String.Empty
        End If

    End Sub

    ''' <summary>��ʃR�[�h��ݒ肷��</summary> 
    Private Sub SetSyubetuCd()

        '��ʏ����擾����
        Dim dtSyubetu As New Data.DataTable
        dtSyubetu = seikyuuSakiTyuuijikouLogic.GetSyubetu()

        '�f�[�^��bound
        Me.ddlSyubetuCd.DataSource = dtSyubetu
        Me.ddlSyubetuCd.DataValueField = "code"
        Me.ddlSyubetuCd.DataTextField = "meisyou"
        Me.ddlSyubetuCd.DataBind()

        '�󔒍s
        Me.ddlSyubetuCd.Items.Insert(0, New ListItem(String.Empty, String.Empty))

        '�f�t�H���g
        Me.ddlSyubetuCd.SelectedValue = String.Empty

    End Sub

    ''' <summary>�d�v�x��ݒ肷��</summary> 
    Private Sub SetJyuuyoudo()
        '�󔒍s
        Me.ddlJyuuyoudo.Items.Insert(0, New ListItem(String.Empty, String.Empty))
        '��
        Me.ddlJyuuyoudo.Items.Insert(1, New ListItem("��", "2"))
        '��
        Me.ddlJyuuyoudo.Items.Insert(2, New ListItem("��", "1"))
        '��
        Me.ddlJyuuyoudo.Items.Insert(3, New ListItem("��", "0"))

        '�f�t�H���g
        Me.ddlJyuuyoudo.SelectedValue = String.Empty

    End Sub

    ''' <summary>�������ߓ���ݒ肷��</summary> 
    Private Sub SetSeikyuuSimeDate()
        '�\���̒l��ݒ肷��
        Me.tbxSeikyuuSimeDate.Text = String.Empty
    End Sub

    ''' <summary>�������K������ݒ肷��</summary> 
    Private Sub SetSeikyuusyoHittykDate()
        '�\���̒l��ݒ肷��
        Me.tbxSeikyuusyoHittykDate.Text = String.Empty
    End Sub

    ''' <summary>�u�V�K�o�^�v�{�^����ݒ肷��</summary>
    Private Function SetBtnTouroku() As Boolean
        '�n�Չ�ʋ��ʃN���X
        Dim jBn As New Jiban
        Dim user_info As New LoginUserInfo
        jBn.userAuth(user_info)

        Dim blnEnable As Boolean

        '�o���Ɩ������擾
        Dim dtAccountTable As CommonSearchDataSet.AccountTableDataTable
        dtAccountTable = commonCheck.CheckKengen(user_info.AccountNo)
        If dtAccountTable.Rows.Count = 0 Then
            blnEnable = False
        ElseIf dtAccountTable.Rows(0).Item("keiri_gyoumu_kengen") = -1 Then
            blnEnable = True
        Else
            blnEnable = False
        End If

        '�u�V�K�o�^�v�{�^��
        Me.btnTouroku.Visible = blnEnable

        Return blnEnable

    End Function

    ''' <summary>�������ʂ�ݒ�</summary>
    Private Sub SetKensakuCount(ByVal intCount As Integer)

        If intCount.Equals(0) Then
            Me.lblCount.Text = "0"
            Me.lblCount.ForeColor = Drawing.Color.Black
            scrollHeight = intCount * 32 + 1
        Else
            If Me.ddlKensaKensuu.SelectedValue = "max" Then
                Me.lblCount.Text = "1-" & CStr(intCount)
                Me.lblCount.ForeColor = Drawing.Color.Black
                scrollHeight = intCount * 32 + 1
            Else
                If intCount > Convert.ToInt64(Me.ddlKensaKensuu.SelectedValue) Then
                    Me.lblCount.Text = "1-" & CStr(Me.ddlKensaKensuu.SelectedValue) & "/" & CStr(intCount)
                    Me.lblCount.ForeColor = Drawing.Color.Red
                    scrollHeight = Me.ddlKensaKensuu.SelectedValue * 32 + 1
                Else
                    Me.lblCount.Text = "1-" & CStr(intCount)
                    Me.lblCount.ForeColor = Drawing.Color.Black
                    scrollHeight = intCount * 32 + 1
                End If
            End If
        End If

    End Sub

    ''' <summary>��������s��{�^�����N���b�N��</summary>
    Private Sub btnKensakujiltukou_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKensakujiltukou.Click
        '���̓`�F�b�N
        Dim strObjId As String = String.Empty
        Dim strErrMessage As String = CheckInput(strObjId)
        If strErrMessage <> String.Empty Then
            ShowMessage(strErrMessage, strObjId)
            Exit Sub
        End If

        '�����������s��
        Call Me.KensakuSyori()

    End Sub

    ''' <summary>CSV�o�̓{�^�����N���b�N��</summary>
    Private Sub btnCsvOutput_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCsvOutput.Click
        '���̓`�F�b�N
        Dim strObjId As String = String.Empty
        Dim strErrMessage As String = CheckInput(strObjId)
        If strErrMessage <> String.Empty Then
            ShowMessage(strErrMessage, strObjId)
            Exit Sub
        End If

        '�p�����^���쐬����
        Dim param As New Dictionary(Of String, String)
        param = SetParameters()

        '�����於��ݒ肷��
        Call Me.SetSeikyuusakiMei()

        '�f�[�^�̌������擾����
        Dim intCount As Integer
        intCount = seikyuuSakiTyuuijikouLogic.GetSeikyuuSakiTyuuijikouCount(param)

        '�������ʂ�ݒ�
        Call Me.SetKensakuCount(intCount)

        '�����̖��׃f�[�^��ݒ肷��
        Call Me.SetKensakuMeisai(intCount, param, False)

        '
        ClientScript.RegisterStartupScript(Me.GetType, "CsvOutput", "<script language=javascript>document.getElementById('" & Me.btnCsvSyori.ClientID & "').click();</script>")

    End Sub

    ''' <summary>CSV�o�͏���</summary>
    Private Sub btnCsvSyori_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCsvSyori.Click

        '�p�����^���쐬����
        Dim param As New Dictionary(Of String, String)
        param = SetParameters()

        '�f�[�^���擾����
        Dim dtSeikyuuSakiTyuuijikouCSV As New Data.DataTable
        dtSeikyuuSakiTyuuijikouCSV = seikyuuSakiTyuuijikouLogic.GetSeikyuuSakiTyuuijikouCSV(param)

        'CSV�t�@�C�����ݒ�
        Dim strFileName As String = System.Configuration.ConfigurationManager.AppSettings("SeikyuuSakiTyuuijikouMasterCsv").ToString

        Response.Buffer = True
        Dim writer As New CsvWriter(Me.Response.OutputStream, Encoding.GetEncoding(932), ",", vbCrLf)

        'CSV�t�@�C���w�b�_�ݒ�
        writer.WriteLine(EarthConst.conSeikyuuSakiTyuuijikouCsvHeader)

        'CSV�t�@�C�����e�ݒ�
        For i As Integer = 0 To dtSeikyuuSakiTyuuijikouCSV.Rows.Count - 1
            With dtSeikyuuSakiTyuuijikouCSV.Rows(i)
                writer.WriteLine(.Item(0), .Item(1), .Item(2), .Item(3), .Item(4), .Item(5), .Item(6), Me.CutMaxLength(.Item(7), 128), .Item(8), .Item(9), _
                                 .Item(10), .Item(11), Me.SetTimeType(.Item(12)), .Item(13), Me.SetTimeType(.Item(14)))
            End With
        Next

        'CSV�t�@�C���_�E�����[�h
        Response.Charset = "utf-8"
        Response.ContentType = "text/plain"
        Response.AddHeader("Content-Disposition", "attachment; filename=" & HttpUtility.UrlEncode(strFileName))
        Response.End()

    End Sub

    ''' <summary>gridview</summary>
    Private Sub grdMeisai_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdMeisai.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            '�ڍ�
            Dim lblSyousai As WebControls.Label = CType(e.Row.Cells(5).Controls(1), WebControls.Label)
            lblSyousai.Text = Me.CutMaxLength(lblSyousai.Text.Trim, 128)

            If setFlg.Equals(True) Then
                '������敪
                Dim strSeikyuusakiKbn As String
                strSeikyuusakiKbn = e.Row.Cells(9).Text.Trim
                '������R�[�h
                Dim strSeikyuusakiCd As String
                strSeikyuusakiCd = e.Row.Cells(10).Text.Trim
                '������}��
                Dim strSeikyuusakiBrc As String
                strSeikyuusakiBrc = e.Row.Cells(11).Text.Trim
                'NO
                Dim strNo As String
                strNo = CType(e.Row.Cells(2).Controls(1), WebControls.Label).Text.Trim

                e.Row.Attributes.Add("ondblclick", "fncTourokuPopup('row','" & strSeikyuusakiKbn & "','" & strSeikyuusakiCd & "','" & strSeikyuusakiBrc & "','" & strNo & "');")
            End If
            e.Row.Cells(9).Style.Add("display", "none")
            e.Row.Cells(10).Style.Add("display", "none")
            e.Row.Cells(11).Style.Add("display", "none")
        End If
    End Sub

    ''' <summary>��������</summary>
    Private Sub KensakuSyori(Optional ByVal blnShowMessageFlg As Boolean = True)

        '�p�����^���쐬����
        Dim param As New Dictionary(Of String, String)
        param = SetParameters()

        '�f�[�^�̌������擾����
        Dim intCount As Integer
        intCount = seikyuuSakiTyuuijikouLogic.GetSeikyuuSakiTyuuijikouCount(param)

        '�������ʂ�ݒ�
        Call Me.SetKensakuCount(intCount)

        '�����̖��׃f�[�^��ݒ肷��
        Call Me.SetKensakuMeisai(intCount, param, blnShowMessageFlg)

        '�����於��ݒ肷��
        Call Me.SetSeikyuusakiMei()

    End Sub

    ''' <summary>�����̖��׃f�[�^��ݒ肷��</summary>
    Private Sub SetKensakuMeisai(ByVal intCount As Integer, ByVal Param As Dictionary(Of String, String), Optional ByVal blnShowMessageFlg As Boolean = True)
        If intCount.Equals(0) Then
            '0���̏ꍇ
            If blnShowMessageFlg.Equals(True) Then
                '�G���[���b�Z�[�W��\������
                Call Me.ShowMessage(Messages.Instance.MSG020E, String.Empty)
            End If
            '���ׂ��N���A����
            Me.grdMeisai.DataSource = Nothing
            Me.grdMeisai.DataBind()

            '�\�[�g���{�^����ݒ肷��
            Call Me.SetSortButton(False)

        Else
            '�Y���f�[�^�����݂���ꍇ

            '�����撍�ӎ��������擾����
            Dim dtSeikyuuSakiTyuuijikouInfo As New SeikyuuSakiTyuuijikouDataSet.SeikyuuSakiTyuuijikouInfoTableDataTable
            dtSeikyuuSakiTyuuijikouInfo = seikyuuSakiTyuuijikouLogic.GetSeikyuuSakiTyuuijikouInfo(Param)

            ViewState("dtSeikyuuSakiTyuuijikouInfo") = dtSeikyuuSakiTyuuijikouInfo

            '���ׂ��N���A����
            Me.grdMeisai.DataSource = dtSeikyuuSakiTyuuijikouInfo
            Me.grdMeisai.DataBind()

            ViewState("scrollHeight") = scrollHeight

            '�\�[�g���{�^����ݒ肷��
            Call Me.SetSortButton(True)

            '�\�[�g���{�^���F��ݒ肷��
            Call Me.SetSortButtonColor()

        End If
    End Sub

    ''' <summary>���̓`�F�b�N</summary>
    Private Function CheckInput(ByRef strObjId As String) As String
        Dim csScript As New StringBuilder
        With csScript
            '������R�[�h(���p�p�����`�F�b�N)
            If Me.tbxSeikyuusakiCd.Text <> String.Empty Then
                .Append(commonCheck.ChkHankakuEisuuji(Me.tbxSeikyuusakiCd.Text, "������R�[�h"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxSeikyuusakiCd.ClientID
                End If
            End If
            '������}��(���p�p�����`�F�b�N)
            If Me.tbxSeikyuusakiBrc.Text <> String.Empty Then
                .Append(commonCheck.ChkHankakuEisuuji(Me.tbxSeikyuusakiBrc.Text, "������}��"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxSeikyuusakiBrc.ClientID
                End If
            End If
            '�������ߓ�(���p�����`�F�b�N)
            If Me.tbxSeikyuuSimeDate.Text <> String.Empty Then
                .Append(commonCheck.CheckHankaku(Me.tbxSeikyuuSimeDate.Text, "�������ߓ�", "1"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxSeikyuuSimeDate.ClientID
                End If
            End If
            '�������K����(���p�����`�F�b�N)
            If Me.tbxSeikyuusyoHittykDate.Text <> String.Empty Then
                .Append(commonCheck.CheckHankaku(Me.tbxSeikyuusyoHittykDate.Text, "�������K����", "1"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxSeikyuusyoHittykDate.ClientID
                End If
            End If
        End With
        Return csScript.ToString
    End Function

    ''' <summary>�N������ݒ肷��</summary>
    Private Function SetTimeType(ByVal objTime As Object) As String
        If (Not IsDBNull(objTime)) AndAlso (Not objTime.ToString.Trim.Equals(String.Empty)) Then
            Return CDate(objTime).ToString("yyyy/MM/dd HH:mm:ss")
        Else
            Return String.Empty
        End If
    End Function

    ''' <summary>�ő咷��؂���</summary>
    Public Function CutMaxLength(ByVal strValue As String, ByVal intMaxByteCount As Integer) As String

        Dim hEncoding As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")

        Dim intLengthCount As Integer = 0
        For i As Integer = strValue.Length To 0 Step -1
            Dim btBytes As Byte() = hEncoding.GetBytes(Left(strValue, i))
            If btBytes.LongLength <= intMaxByteCount Then
                intLengthCount = i
                Exit For
            End If
        Next

        Return Left(strValue, intLengthCount)
    End Function

    ''' <summary>��ʈꗗ�w�b�_�[�ɕ��я����N���b�N��</summary>
    Private Sub btnSort_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSeikyuusakiCdUp.Click, _
                                                                                           btnSeikyuusakiCdDown.Click, _
                                                                                           btnSeikyuusakiMeiUp.Click, _
                                                                                           btnSeikyuusakiMeiDown.Click, _
                                                                                           btnTorikesiUp.Click, _
                                                                                           btnTorikesiDown.Click, _
                                                                                           btnSyubetuCdUp.Click, _
                                                                                           btnSyubetuCdDown.Click, _
                                                                                           btnSyousaiUp.Click, _
                                                                                           btnSyousaiDown.Click, _
                                                                                           btnJyuuyoudoUp.Click, _
                                                                                           btnJyuuyoudoDown.Click, _
                                                                                           btnSeikyuuSimeDateUp.Click, _
                                                                                           btnSeikyuuSimeDateDown.Click, _
                                                                                           btnSeikyuusyoHittykDateUp.Click, _
                                                                                           btnSeikyuusyoHittykDateDown.Click
        Dim strSort As String = String.Empty
        '�\�[�g���{�^���F��ݒ肷��
        Call SetSortButtonColor()

        '��ʂɃ\�[�g�����N���b�N��
        Select Case CType(sender, LinkButton).ID
            Case btnSeikyuusakiCdUp.ID                                      '������R�[�h��
                strSort = "seikyuu_saki_kbn ASC,seikyuu_saki_cd ASC,seikyuu_saki_brc ASC"
                btnSeikyuusakiCdUp.ForeColor = Drawing.Color.IndianRed
            Case btnSeikyuusakiCdDown.ID                                    '������R�[�h��
                strSort = "seikyuu_saki_kbn DESC,seikyuu_saki_cd DESC,seikyuu_saki_brc DESC"
                btnSeikyuusakiCdDown.ForeColor = Drawing.Color.IndianRed
            Case btnSeikyuusakiMeiUp.ID                                     '�����於��
                strSort = "seikyuu_saki_mei ASC"
                btnSeikyuusakiMeiUp.ForeColor = Drawing.Color.IndianRed
            Case btnSeikyuusakiMeiDown.ID                                   '�����於��
                strSort = "seikyuu_saki_mei DESC"
                btnSeikyuusakiMeiDown.ForeColor = Drawing.Color.IndianRed
            Case btnTorikesiUp.ID                                           '�����
                strSort = "torikesi ASC"
                btnTorikesiUp.ForeColor = Drawing.Color.IndianRed
            Case btnTorikesiDown.ID                                         '�����
                strSort = "torikesi DESC"
                btnTorikesiDown.ForeColor = Drawing.Color.IndianRed
            Case btnSyubetuCdUp.ID                                          '��ʁ�
                strSort = "syubetu_cd ASC"
                btnSyubetuCdUp.ForeColor = Drawing.Color.IndianRed
            Case btnSyubetuCdDown.ID                                        '��ʁ�
                strSort = "syubetu_cd DESC"
                btnSyubetuCdDown.ForeColor = Drawing.Color.IndianRed
            Case btnSyousaiUp.ID                                            '�ڍׁ�
                strSort = "syousai ASC"
                btnSyousaiUp.ForeColor = Drawing.Color.IndianRed
            Case btnSyousaiDown.ID                                          '�ڍׁ�
                strSort = "syousai DESC"
                btnSyousaiDown.ForeColor = Drawing.Color.IndianRed
            Case btnJyuuyoudoUp.ID                                          '�d�v�x��
                strSort = "jyuyodo ASC"
                btnJyuuyoudoUp.ForeColor = Drawing.Color.IndianRed
            Case btnJyuuyoudoDown.ID                                        '�d�v�x��
                strSort = "jyuyodo DESC"
                btnJyuuyoudoDown.ForeColor = Drawing.Color.IndianRed
            Case btnSeikyuuSimeDateUp.ID                                    '�������ߓ���
                strSort = "seikyuu_sime_date ASC"
                btnSeikyuuSimeDateUp.ForeColor = Drawing.Color.IndianRed
            Case btnSeikyuuSimeDateDown.ID                                  '�������ߓ���
                strSort = "seikyuu_sime_date DESC"
                btnSeikyuuSimeDateDown.ForeColor = Drawing.Color.IndianRed
            Case btnSeikyuusyoHittykDateUp.ID                               '�������K������
                strSort = "seikyuusyo_hittyk_date ASC"
                btnSeikyuusyoHittykDateUp.ForeColor = Drawing.Color.IndianRed
            Case btnSeikyuusyoHittykDateDown.ID                             '�������K������
                strSort = "seikyuusyo_hittyk_date DESC"
                btnSeikyuusyoHittykDateDown.ForeColor = Drawing.Color.IndianRed
        End Select

        '��ʃf�[�^�̃\�[�g����ݒ肷��
        Dim dvSeikyuuSakiTyuuijikouInfo As Data.DataView = CType(ViewState("dtSeikyuuSakiTyuuijikouInfo"), Data.DataTable).DefaultView
        dvSeikyuuSakiTyuuijikouInfo.Sort = strSort

        Me.grdMeisai.DataSource = dvSeikyuuSakiTyuuijikouInfo
        Me.grdMeisai.DataBind()

        '��ʏc�X�N���[����ݒ肷��
        scrollHeight = ViewState("scrollHeight")

    End Sub

    ''' <summary>�\�[�g���{�^����ݒ肷��</summary>
    Private Sub SetSortButton(ByVal blnFlg As Boolean)

        Me.btnSeikyuusakiCdUp.Visible = blnFlg            '������R�[�h��
        Me.btnSeikyuusakiCdDown.Visible = blnFlg          '������R�[�h��
        Me.btnSeikyuusakiMeiUp.Visible = blnFlg           '�����於��
        Me.btnSeikyuusakiMeiDown.Visible = blnFlg         '�����於��
        Me.btnTorikesiUp.Visible = blnFlg                 '�����
        Me.btnTorikesiDown.Visible = blnFlg               '�����
        Me.btnSyubetuCdUp.Visible = blnFlg                '��ʁ�
        Me.btnSyubetuCdDown.Visible = blnFlg              '��ʁ�
        Me.btnSyousaiUp.Visible = blnFlg                  '�ڍׁ�
        Me.btnSyousaiDown.Visible = blnFlg                '�ڍׁ�
        Me.btnJyuuyoudoUp.Visible = blnFlg                '�d�v�x��
        Me.btnJyuuyoudoDown.Visible = blnFlg              '�d�v�x��
        Me.btnSeikyuuSimeDateUp.Visible = blnFlg          '�������ߓ���
        Me.btnSeikyuuSimeDateDown.Visible = blnFlg        '�������ߓ���
        Me.btnSeikyuusyoHittykDateUp.Visible = blnFlg     '�������K������
        Me.btnSeikyuusyoHittykDateDown.Visible = blnFlg   '�������K������

        Me.divTorikesi.Visible = blnFlg
        Me.divJyuuyoudo.Visible = blnFlg
        Me.divSeikyuuSimeDate.Visible = blnFlg
        Me.divSeikyuusyoHittykDate.Visible = blnFlg

    End Sub

    ''' <summary>�\�[�g���{�^���F��ݒ肷��</summary>
    Private Sub SetSortButtonColor()

        Me.btnSeikyuusakiCdUp.ForeColor = Drawing.Color.SkyBlue            '������R�[�h��
        Me.btnSeikyuusakiCdDown.ForeColor = Drawing.Color.SkyBlue          '������R�[�h��
        Me.btnSeikyuusakiMeiUp.ForeColor = Drawing.Color.SkyBlue           '�����於��
        Me.btnSeikyuusakiMeiDown.ForeColor = Drawing.Color.SkyBlue         '�����於��
        Me.btnTorikesiUp.ForeColor = Drawing.Color.SkyBlue                 '�����
        Me.btnTorikesiDown.ForeColor = Drawing.Color.SkyBlue               '�����
        Me.btnSyubetuCdUp.ForeColor = Drawing.Color.SkyBlue                '��ʁ�
        Me.btnSyubetuCdDown.ForeColor = Drawing.Color.SkyBlue              '��ʁ�
        Me.btnSyousaiUp.ForeColor = Drawing.Color.SkyBlue                  '�ڍׁ�
        Me.btnSyousaiDown.ForeColor = Drawing.Color.SkyBlue                '�ڍׁ�
        Me.btnJyuuyoudoUp.ForeColor = Drawing.Color.SkyBlue                '�d�v�x��
        Me.btnJyuuyoudoDown.ForeColor = Drawing.Color.SkyBlue              '�d�v�x��
        Me.btnSeikyuuSimeDateUp.ForeColor = Drawing.Color.SkyBlue          '�������ߓ���
        Me.btnSeikyuuSimeDateDown.ForeColor = Drawing.Color.SkyBlue        '�������ߓ���
        Me.btnSeikyuusyoHittykDateUp.ForeColor = Drawing.Color.SkyBlue     '�������K������
        Me.btnSeikyuusyoHittykDateDown.ForeColor = Drawing.Color.SkyBlue   '�������K������

    End Sub

    ''' <summary>�p�����[�^���쐬����</summary>
    Public Function SetParameters() As Dictionary(Of String, String)
        '�p�����[�^
        Dim param As New Dictionary(Of String, String)
        '�����������
        param.Add("KensakuKensuu", Me.ddlKensaKensuu.SelectedValue.Trim)
        '������敪
        param.Add("SeikyuusakiKbn", Me.ddlSeikyuusakiKbn.SelectedValue.Trim)
        '������R�[�h
        param.Add("SeikyuusakiCd", Me.tbxSeikyuusakiCd.Text.Trim)
        '������}��
        param.Add("SeikyuusakiBrc", Me.tbxSeikyuusakiBrc.Text.Trim)
        '��ʃR�[�h
        param.Add("SyubetuCd", Me.ddlSyubetuCd.SelectedValue.Trim)
        '�d�v�x
        param.Add("Jyuuyoudo", Me.ddlJyuuyoudo.SelectedValue.Trim)
        '�������ߓ�
        param.Add("SeikyuuSimeDate", Me.tbxSeikyuuSimeDate.Text.Trim)
        '�������K����
        param.Add("SeikyuusyoHittykDate", Me.tbxSeikyuusyoHittykDate.Text.Trim)
        '����͌����ΏۊO
        param.Add("KensakuTaisyouGai", IIf(Me.chkKensakuTaisyouGai.Checked, "0", String.Empty))

        Return param
    End Function

    ''' <summary>�G���[���b�Z�[�W�\��</summary>
    Private Sub ShowMessage(ByVal strMessage As String, ByVal strObjId As String)
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("function window.onload() {")
            .AppendLine("	alert('" & strMessage & "');")
            If strObjId <> String.Empty Then
                .AppendLine("   document.getElementById('" & strObjId & "').focus();")
                .AppendLine("   document.getElementById('" & strObjId & "').select();")
            End If
            .AppendLine("}")
        End With
        '�y�[�W�����ŁA�N���C�A���g���̃X�N���v�g �u���b�N���o�͂��܂�
        ClientScript.RegisterStartupScript(Me.GetType(), "ShowMessage", csScript.ToString, True)
    End Sub

    ''' <summary>DIV��\��</summary>
    Public Sub CloseCover()
        Dim csScript As New StringBuilder
        csScript.AppendFormat("fncClosecover();").ToString()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CloseCover", csScript.ToString, True)
    End Sub

    ''' <summary>JavaScript���쐬</summary>
    Protected Sub MakeJavaScript()
        Dim sbScript As New StringBuilder
        With sbScript
            .AppendLine("<script type='text/javascript' language='javascript'>")
            '�uClose�v�{�^���̏���
            .AppendLine("   function fncClose()")
            .AppendLine("   {")
            .AppendLine("       window.close();")
            .AppendLine("       return false;")
            .AppendLine("   }")

            '�u�N���A�v�{�^���̏���
            .AppendLine("   function fncClear()")
            .AppendLine("   {")
            '������敪
            .AppendLine("       document.getElementById('" & Me.ddlSeikyuusakiKbn.ClientID & "').selectedIndex=0;")
            '������R�[�h
            .AppendLine("       document.getElementById('" & Me.tbxSeikyuusakiCd.ClientID & "').innerText='';")
            '������}��
            .AppendLine("       document.getElementById('" & Me.tbxSeikyuusakiBrc.ClientID & "').innerText='';")
            '�����於
            .AppendLine("       document.getElementById('" & Me.tbxSeikyuusakiMei.ClientID & "').innerText='';")
            '��ʃR�[�h
            .AppendLine("       document.getElementById('" & Me.ddlSyubetuCd.ClientID & "').selectedIndex=0;")
            '�d�v�x
            .AppendLine("       document.getElementById('" & Me.ddlJyuuyoudo.ClientID & "').selectedIndex=0;")
            '�������ߓ�
            .AppendLine("       document.getElementById('" & Me.tbxSeikyuuSimeDate.ClientID & "').innerText='';")
            '�������K����
            .AppendLine("       document.getElementById('" & Me.tbxSeikyuusyoHittykDate.ClientID & "').innerText='';")
            '�����������
            .AppendLine("       document.getElementById('" & Me.ddlKensaKensuu.ClientID & "').selectedIndex=1;")
            '����͌����ΏۊO
            .AppendLine("       document.getElementById('" & Me.chkKensakuTaisyouGai.ClientID & "').checked=true;")
            '������敪
            .AppendLine("       document.getElementById('" & Me.ddlSeikyuusakiKbn.ClientID & "').focus();")
            .AppendLine("   }")

            '���̓`�F�b�N
            .AppendLine("	function fncNyuuryokuCheck(strKbn) ")
            .AppendLine("	{ ")
            '������R�[�h
            .AppendLine("		var strSeikyuusakiCd = Trim(document.getElementById('" & Me.tbxSeikyuusakiCd.ClientID & "').value); ")
            '��ʃR�[�h
            .AppendLine("		var strSyubetuCd = Trim(document.getElementById('" & Me.ddlSyubetuCd.ClientID & "').value); ")
            '�d�v�x
            .AppendLine("		var strJyuuyoudo = Trim(document.getElementById('" & Me.ddlJyuuyoudo.ClientID & "').value); ")
            '�������ߓ�
            .AppendLine("		var strSeikyuuSimeDate = Trim(document.getElementById('" & Me.tbxSeikyuuSimeDate.ClientID & "').value); ")
            '�������K����
            .AppendLine("		var strSeikyuusyoHittykDate = Trim(document.getElementById('" & Me.tbxSeikyuusyoHittykDate.ClientID & "').value); ")
            .AppendLine("       if (strKbn == 'kensaku'){")
            '�������������ꂩ�K�{�F�����溰�ށi�敪�E�}�Ԃ͏����j�A��ʁA�d�v�x�A�������ߓ��A�������K����
            .AppendLine("		    if((strSeikyuusakiCd=='')&&(strSyubetuCd=='')&&(strJyuuyoudo=='')&&(strSeikyuuSimeDate=='')&&(strSeikyuusyoHittykDate=='')) ")
            .AppendLine("		    { ")
            .AppendLine("			    alert('" & Messages.Instance.MSG2064E & "'); ")
            .AppendLine("			    document.getElementById('" & Me.tbxSeikyuusakiCd.ClientID & "').focus(); ")
            .AppendLine("			    return false;			 ")
            .AppendLine("		    } ")
            '�m�F���b�Z�[�W�\��
            .AppendLine("           if (document.all." & Me.ddlKensaKensuu.ClientID & ".value=='max'){")
            .AppendLine("               if(!confirm('" & Messages.Instance.MSG007C & "')){")
            .AppendLine("                   return false; ")
            .AppendLine("               }")
            .AppendLine("           }")
            .AppendLine("       }")
            .AppendLine("       return true;")
            .AppendLine("	} ")

            '������̌���popup
            .AppendLine("	function fncSeikyuusakiPopup() ")
            .AppendLine("	{ ")
            .AppendLine("		window.open('search_Seikyuusaki.aspx?blnDelete=False&Kbn='+escape('������')+'&FormName=" & Me.Page.Form.Name & " &objKbn=" & Me.ddlSeikyuusakiKbn.ClientID & "&objCd=" & Me.tbxSeikyuusakiCd.ClientID & "&objBrc=" & Me.tbxSeikyuusakiBrc.ClientID & "&objMei=" & Me.tbxSeikyuusakiMei.ClientID & "&strKbn='+escape(document.getElementById('" & Me.ddlSeikyuusakiKbn.ClientID & "').value)+'&strCd='+escape(document.getElementById('" & Me.tbxSeikyuusakiCd.ClientID & "').value)+'&strBrc='+escape(document.getElementById('" & Me.tbxSeikyuusakiBrc.ClientID & "').value),'SeikyuusakiPopup','menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes'); ")
            .AppendLine("	} ")

            '�����撍�ӏ��o�^��ʂ�popup
            .AppendLine("	function fncTourokuPopup(strFlg,strKbn,strCd,strBrc,strNo) ")
            .AppendLine("	{ ")
            .AppendLine("       var sendSearchTerms; ")
            .AppendLine("       if(strFlg=='btn') ")
            .AppendLine("       { ")
            .AppendLine("           strKbn = Trim(document.getElementById('" & Me.ddlSeikyuusakiKbn.ClientID & "').value);")
            .AppendLine("           strCd = Trim(document.getElementById('" & Me.tbxSeikyuusakiCd.ClientID & "').value);")
            .AppendLine("           strBrc = Trim(document.getElementById('" & Me.tbxSeikyuusakiBrc.ClientID & "').value);")
            .AppendLine("           sendSearchTerms = Trim(strKbn)+'$$$'+Trim(strCd)+'$$$'+Trim(strBrc); ")
            .AppendLine("       } ")
            .AppendLine("       else ")
            .AppendLine("       { ")
            .AppendLine("           sendSearchTerms = Trim(strKbn)+'$$$'+Trim(strCd)+'$$$'+Trim(strBrc)+'$$$'+Trim(strNo); ")
            .AppendLine("       } ")
            .AppendLine("		window.open('SeikyuuSakiTyuuijikouInput.aspx?sendSearchTerms='+escape(sendSearchTerms),'TourokuPopup','top=0,left=0,width=1000,height=400,menubar=yes,toolbar=yes,location=yes,status=yes,resizable=yes,scrollbars=yes'); ")
            .AppendLine(" ")
            .AppendLine(" ")
            .AppendLine("	} ")

            '�X�N���[����ݒ肷��
            .AppendLine("function wheel(event){")
            .AppendLine("   var delta = 0;")
            .AppendLine("   if(!event)")
            .AppendLine("       event = window.event;")
            .AppendLine("   if(event.wheelDelta){")
            .AppendLine("       delta = event.wheelDelta/120;")
            .AppendLine("       if(window.opera)")
            .AppendLine("           delta = -delta;")
            .AppendLine("       }else if(event.detail){")
            .AppendLine("           delta = -event.detail/3;")
            .AppendLine("       }")
            .AppendLine("   if(delta)")
            .AppendLine("       handle(delta);")
            .AppendLine("}")
            .AppendLine("function handle(delta){")
            .AppendLine("   var divVscroll=" & divHiddenMeisaiV.ClientID & ";")
            .AppendLine("   if (delta < 0){")
            .AppendLine("       divVscroll.scrollTop = divVscroll.scrollTop + 15;")
            .AppendLine("   }else{")
            .AppendLine("       divVscroll.scrollTop = divVscroll.scrollTop - 15;")
            .AppendLine("   }")
            .AppendLine("}")
            .AppendLine("function fncScrollV(){")
            .AppendLine("   var divbody=" & Me.divMeisai.ClientID & ";")
            .AppendLine("   var divVscroll=" & Me.divHiddenMeisaiV.ClientID & ";")
            .AppendLine("   divbody.scrollTop = divVscroll.scrollTop;")
            .AppendLine("}")
            .AppendLine("   function fncSetHidCSV(){")
            .AppendLine("       document.getElementById('" & Me.hidCSVFlg.ClientID & "').value='';")
            .AppendLine("   }")

            'DIV�\��
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

            'DIV��\��
            .AppendLine("   function fncClosecover(){")
            .AppendLine("       var buyDiv=document.getElementById('" & Me.buySelName.ClientID & "');")
            .AppendLine("       var disable=document.getElementById('" & Me.disableDiv.ClientID & "');")
            .AppendLine("       buyDiv.style.display='none';")
            .AppendLine("       disable.style.display='none';")
            .AppendLine("   }")
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
        Page.ClientScript.RegisterStartupScript(Page.GetType, "JS_" & Me.ClientID, sbScript.ToString)
    End Sub

End Class
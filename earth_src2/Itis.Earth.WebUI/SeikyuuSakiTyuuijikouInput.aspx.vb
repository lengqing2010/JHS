Imports Itis.Earth.BizLogic
Imports Itis.Earth.DataAccess

Partial Public Class SeikyuuSakiTyuuijikouInput
    Inherits System.Web.UI.Page

    Private seikyuuSakiTyuuijikouLogic As New SeikyuuSakiTyuuijikouLogic
    Private commonCheck As New CommonCheck

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '�����`�F�b�N
        Dim commonChk As New CommonCheck
        Dim strUserID As String = ""
        Dim blnKeiriKengen As Boolean
        blnKeiriKengen = commonChk.CommonNinnsyou(strUserID, "keiri_gyoumu_kengen")
        If Not blnKeiriKengen Then
            '�G���[��ʂ֑J�ڂ��āA�G���[���b�Z�[�W��\������
            Context.Items("strFailureMsg") = Messages.Instance.MSG2020E
            Server.Transfer("CommonErr.aspx")
        End If

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



        End If
        '�u����v�{�^��
        Me.btnClose.Attributes.Add("onClick", "fncClose();return false;")
        '�u�����v�{�^��
        Me.btnSeikyuusakiSearch.Attributes.Add("onClick", "fncSeikyuusakiPopup();return false;")
        '������敪
        Me.ddlSeikyuusakiKbn.Attributes.Add("onChange", "fncSeikyuusakiChange('kbn');")
        '������R�[�h
        Me.tbxSeikyuusakiCd.Attributes.Add("onChange", "fncSeikyuusakiChange('cd');")
        '������}��
        Me.tbxSeikyuusakiBrc.Attributes.Add("onChange", "fncSeikyuusakiChange('brc');")
        '��V�K�o�^��{�^��
        Me.btnTouroku.Attributes.Add("onClick", "if(!fncNyuuryokuCheck()){return false;}")

    End Sub

    ''' <summary>������</summary> 
    Private Sub Syokika(ByVal sendSearchTerms As String)

        '������敪
        Dim strSeikyuusakiKbn As String = String.Empty
        '������R�[�h
        Dim strSeikyuusakiCd As String = String.Empty
        '������}��
        Dim strSeikyuusakiBrc As String = String.Empty
        '���͇�
        Dim strNo As String = String.Empty

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

            '���͇�
            If arrSearchTerm.Length > 3 Then
                Try
                    Dim intNo As Integer
                    intNo = CInt(arrSearchTerm(3).Trim)
                    strNo = arrSearchTerm(3).Trim
                Catch ex As Exception
                    strNo = String.Empty
                End Try
                '������}�Ԃ�ݒ肷��
                If (Not strSeikyuusakiKbn.Trim.Equals(String.Empty)) AndAlso (Not strSeikyuusakiCd.Trim.Equals(String.Empty)) AndAlso (Not strSeikyuusakiBrc.Trim.Equals(String.Empty)) Then
                    Call Me.SetNo(strNo)
                Else
                    Call Me.SetNo(String.Empty)
                End If
            Else
                Call Me.SetNo(String.Empty)
            End If

        Else
            '������敪��ݒ肷��
            Call Me.SetSeikyuusakiKbn(String.Empty)

            '������R�[�h��ݒ肷��
            Call Me.SetSeikyuusakiCd(String.Empty)

            '������}�Ԃ�ݒ肷��
            Call Me.SetSeikyuusakiBrc(String.Empty)

            '���͇�
            Call Me.SetNo(String.Empty)
        End If

        ''�����於��ݒ肷��
        'Call Me.SetSeikyuusakiMei()

        ''��ʃR�[�h��ݒ肷��
        'Call Me.SetSyubetuCd(String.Empty)

        ''�d�v�x��ݒ肷��
        'Call Me.SetJyuuyoudo(String.Empty)

        ''�������ߓ���ݒ肷��
        'Call Me.SetSeikyuuSimeDate()

        ''�������K������ݒ肷��
        'Call Me.SetSeikyuusyoHittykDate()

        '������(�敪�E�R�[�h�E�}��)����ꍇ
        If (Not strSeikyuusakiKbn.Trim.Equals(String.Empty)) AndAlso (Not strSeikyuusakiCd.Trim.Equals(String.Empty)) AndAlso (Not strSeikyuusakiBrc.Trim.Equals(String.Empty)) Then
            '�������ݒ肷��
            Call Me.SetSeikyuusaki(strSeikyuusakiKbn.Trim, strSeikyuusakiCd.Trim, strSeikyuusakiBrc.Trim)

            '������敪
            Me.hdnSeikyuusakiKbn.Value = strSeikyuusakiKbn.Trim
            '������R�[�h
            Me.hdnSeikyuusakiCd.Value = strSeikyuusakiCd.Trim
            '������}��
            Me.hdnSeikyuusakiBrc.Value = strSeikyuusakiBrc.Trim
            '�����t���O
            Me.hdnSearchFlg.Value = "1"

            '���͇�����ꍇ
            If Not strNo.Trim.Equals(String.Empty) Then
                '�����撍�ӎ�����ݒ肷��
                Call Me.SetSeikyuusakiTyuuijikou(strSeikyuusakiKbn.Trim, strSeikyuusakiCd.Trim, strSeikyuusakiBrc.Trim, strNo.Trim)

                '������(�敪�E�R�[�h�E�}�ԁE�����{�^��)�͔񊈐��ɂ���
                Me.ddlSeikyuusakiKbn.Enabled = False
                Me.tbxSeikyuusakiCd.Enabled = False
                Me.tbxSeikyuusakiBrc.Enabled = False
                Me.btnSeikyuusakiSearch.Enabled = False
            Else
                '�d�v�x
                Call Me.SetJyuuyoudo(String.Empty)
                '���
                Me.chkTorikesi.Checked = False
                '��ʃR�[�h
                Call Me.SetSyubetuCd(String.Empty)
                '�ڍ�
                Me.tbxSyousai.Text = String.Empty
            End If
        Else
            '�����於
            Me.tbxSeikyuusakiMei.Text = String.Empty
            '�������ߓ�
            Me.lblSeikyuuSimeDate.Text = String.Empty
            '�������K����
            Me.lblSeikyuusyoHittykDate.Text = String.Empty
            '�d�v�x
            Call Me.SetJyuuyoudo(String.Empty)
            '���
            Me.chkTorikesi.Checked = False
            '��ʃR�[�h
            Call Me.SetSyubetuCd(String.Empty)
            '�ڍ�
            Me.tbxSyousai.Text = String.Empty
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

    ''' <summary>���͇���ݒ肷��</summary> 
    Private Sub SetNo(ByVal strNo As String)
        '�\���̒l��ݒ肷��
        If Not strNo.Trim.Equals(String.Empty) Then
            Me.lblNo.Text = strNo.Trim
        Else
            Me.lblNo.Text = String.Empty
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
    Private Sub SetSyubetuCd(ByVal strSyubetu As String)

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

        '�\���̒l��ݒ肷��
        If Not strSyubetu.Trim.Equals(String.Empty) Then
            Try
                Me.ddlSyubetuCd.SelectedValue = strSyubetu
            Catch ex As Exception
                Me.ddlSyubetuCd.SelectedValue = String.Empty
            End Try
        Else
            Me.ddlSyubetuCd.SelectedValue = String.Empty
        End If

    End Sub

    ''' <summary>�d�v�x��ݒ肷��</summary> 
    Private Sub SetJyuuyoudo(ByVal strJyuuyoudo As String)
        ''��
        'Me.ddlJyuuyoudo.Items.Insert(0, New ListItem("��", "2"))
        ''��
        'Me.ddlJyuuyoudo.Items.Insert(1, New ListItem("��", "1"))
        ''��
        'Me.ddlJyuuyoudo.Items.Insert(2, New ListItem("��", "0"))

        '�\���̒l��ݒ肷��
        If Not strJyuuyoudo.Trim.Equals(String.Empty) Then
            Try
                Me.ddlJyuuyoudo.SelectedValue = strJyuuyoudo
            Catch ex As Exception
                Me.ddlJyuuyoudo.SelectedValue = "0"
            End Try
        Else
            Me.ddlJyuuyoudo.SelectedValue = "0"
        End If

    End Sub

    ''' <summary>�������ߓ���ݒ肷��</summary> 
    Private Sub SetSeikyuuSimeDate(ByVal strSeikyuuSimeDate As String)
        '�\���̒l��ݒ肷��

        '�\���̒l��ݒ肷��
        If Not strSeikyuuSimeDate.Trim.Equals(String.Empty) Then
            Me.lblSeikyuuSimeDate.Text = strSeikyuuSimeDate.Trim
        Else
            Me.lblSeikyuuSimeDate.Text = String.Empty
        End If


        Me.lblSeikyuuSimeDate.Text = String.Empty
    End Sub

    ''' <summary>�������K������ݒ肷��</summary> 
    Private Sub SetSeikyuusyoHittykDate(ByVal strSeikyuusyoHittykDate As String)
        '�\���̒l��ݒ肷�� 

        '�\���̒l��ݒ肷��
        If Not strSeikyuusyoHittykDate.Trim.Equals(String.Empty) Then
            Me.lblSeikyuusyoHittykDate.Text = strSeikyuusyoHittykDate.Trim
        Else
            Me.lblSeikyuusyoHittykDate.Text = String.Empty
        End If


        Me.lblSeikyuusyoHittykDate.Text = String.Empty
    End Sub

    ''' <summary>�������ݒ肷��</summary> 
    Private Sub SetSeikyuusaki(ByVal strSeikyuusakiKbn As String, ByVal strSeikyuusakiCd As String, ByVal strSeikyuusakiBrc As String)
        '����������擾����
        Dim dtSeikyuusaki As New Data.DataTable
        dtSeikyuusaki = seikyuuSakiTyuuijikouLogic.GetSeikyuusakiInfo(strSeikyuusakiKbn.Trim, strSeikyuusakiCd.Trim, strSeikyuusakiBrc.Trim)

        If dtSeikyuusaki.Rows.Count > 0 Then
            '�����於
            Me.tbxSeikyuusakiMei.Text = dtSeikyuusaki.Rows(0).Item("seikyuu_saki_mei").ToString.Trim
            '�������ߓ�
            Me.lblSeikyuuSimeDate.Text = dtSeikyuusaki.Rows(0).Item("seikyuu_sime_date").ToString.Trim
            '�������K����
            Me.lblSeikyuusyoHittykDate.Text = dtSeikyuusaki.Rows(0).Item("seikyuusyo_hittyk_date").ToString.Trim
        Else
            '�����於
            Me.tbxSeikyuusakiMei.Text = String.Empty
            '�������ߓ�
            Me.lblSeikyuuSimeDate.Text = String.Empty
            '�������K����
            Me.lblSeikyuusyoHittykDate.Text = String.Empty
        End If

    End Sub

    ''' <summary>�����撍�ӎ�����ݒ肷��</summary> 
    Private Sub SetSeikyuusakiTyuuijikou(ByVal strSeikyuusakiKbn As String, ByVal strSeikyuusakiCd As String, ByVal strSeikyuusakiBrc As String, ByVal strNo As String)
        'DB�̎��Ԃ��擾����
        ViewState("DbTime") = CDate(seikyuuSakiTyuuijikouLogic.GetDbTime().Rows(0).Item(0))

        '�����撍�ӎ������擾����
        Dim dtSeikyuusakiTyuuijikou As New Data.DataTable
        dtSeikyuusakiTyuuijikou = seikyuuSakiTyuuijikouLogic.GetSeikyuusakiTyuuijikou(strSeikyuusakiKbn.Trim, strSeikyuusakiCd.Trim, strSeikyuusakiBrc.Trim, strNo.Trim)

        If dtSeikyuusakiTyuuijikou.Rows.Count > 0 Then
            '�d�v�x
            Call Me.SetJyuuyoudo(dtSeikyuusakiTyuuijikou.Rows(0).Item("jyuyodo").ToString.Trim)
            '���
            Me.chkTorikesi.Checked = IIf(dtSeikyuusakiTyuuijikou.Rows(0).Item("torikesi").ToString.Trim.Equals("1"), True, False)
            '��ʃR�[�h
            Call Me.SetSyubetuCd(dtSeikyuusakiTyuuijikou.Rows(0).Item("syubetu_cd").ToString.Trim)
            '�ڍ�
            Me.tbxSyousai.Text = Me.CutMaxLength(dtSeikyuusakiTyuuijikou.Rows(0).Item("syousai").ToString.Trim, 128)

        Else
            '�d�v�x
            Call Me.SetJyuuyoudo(String.Empty)
            '���
            Me.chkTorikesi.Checked = False
            '��ʃR�[�h
            Call Me.SetSyubetuCd(String.Empty)
            '�ڍ�
            Me.tbxSyousai.Text = String.Empty
        End If

    End Sub

    ''' <summary>�����������������A��������Ɛ����撍�ӎ������擾����</summary> 
    Private Sub btnDisplay_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDisplay.Click

        '������敪
        Dim strSeikyuusakiKbn As String = Me.ddlSeikyuusakiKbn.SelectedValue.Trim
        '������R�[�h
        Dim strSeikyuusakiCd As String = Me.tbxSeikyuusakiCd.Text.Trim
        '������}��
        Dim strSeikyuusakiBrc As String = Me.tbxSeikyuusakiBrc.Text.Trim
        '���͇�
        Dim strNo As String = Me.lblNo.Text.Trim

        '������(�敪�E�R�[�h�E�}��)����ꍇ
        If (Not strSeikyuusakiKbn.Trim.Equals(String.Empty)) AndAlso (Not strSeikyuusakiCd.Trim.Equals(String.Empty)) AndAlso (Not strSeikyuusakiBrc.Trim.Equals(String.Empty)) Then
            '�������ݒ肷��
            Call Me.SetSeikyuusaki(strSeikyuusakiKbn.Trim, strSeikyuusakiCd.Trim, strSeikyuusakiBrc.Trim)

            '������敪
            Me.hdnSeikyuusakiKbn.Value = strSeikyuusakiKbn.Trim
            '������R�[�h
            Me.hdnSeikyuusakiCd.Value = strSeikyuusakiCd.Trim
            '������}��
            Me.hdnSeikyuusakiBrc.Value = strSeikyuusakiBrc.Trim
            '�����t���O
            Me.hdnSearchFlg.Value = "1"

            '���͇�����ꍇ
            If Not strNo.Trim.Equals(String.Empty) Then
                '�����撍�ӎ�����ݒ肷��
                Call Me.SetSeikyuusakiTyuuijikou(strSeikyuusakiKbn.Trim, strSeikyuusakiCd.Trim, strSeikyuusakiBrc.Trim, strNo.Trim)

                '������(�敪�E�R�[�h�E�}�ԁE�����{�^��)�͔񊈐��ɂ���
                Me.ddlSeikyuusakiKbn.Enabled = False
                Me.tbxSeikyuusakiCd.Enabled = False
                Me.tbxSeikyuusakiBrc.Enabled = False
                Me.btnSeikyuusakiSearch.Enabled = False
            Else
                '�d�v�x
                Call Me.SetJyuuyoudo(String.Empty)
                '���
                Me.chkTorikesi.Checked = False
                '��ʃR�[�h
                Call Me.SetSyubetuCd(String.Empty)
                '�ڍ�
                Me.tbxSyousai.Text = String.Empty
            End If
        Else
            '�����於
            Me.tbxSeikyuusakiMei.Text = String.Empty
            '�������ߓ�
            Me.lblSeikyuuSimeDate.Text = String.Empty
            '�������K����
            Me.lblSeikyuusyoHittykDate.Text = String.Empty
            '�d�v�x
            Call Me.SetJyuuyoudo(String.Empty)
            '���
            Me.chkTorikesi.Checked = False
            '��ʃR�[�h
            Call Me.SetSyubetuCd(String.Empty)
            '�ڍ�
            Me.tbxSyousai.Text = String.Empty
        End If

    End Sub

    ''' <summary>��V�K�o�^��{�^����������</summary> 
    Private Sub btnTouroku_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTouroku.Click

        '���̓`�F�b�N
        Dim strObjId As String = String.Empty
        Dim strErrMessage As String = CheckInput(strObjId)
        If strErrMessage <> String.Empty Then
            ShowMessage(strErrMessage, strObjId)
            Exit Sub
        End If

        '������敪
        Dim strSeikyuusakiKbn As String = Me.ddlSeikyuusakiKbn.SelectedValue.Trim
        '������R�[�h
        Dim strSeikyuusakiCd As String = Me.tbxSeikyuusakiCd.Text.Trim
        '������}��
        Dim strSeikyuusakiBrc As String = Me.tbxSeikyuusakiBrc.Text.Trim
        '���͇�
        Dim strNo As String = Me.lblNo.Text.Trim
        '���
        Dim strTorikesi As String = IIf(Me.chkTorikesi.Checked, "1", "0")
        '��ʃR�[�h
        Dim strSyubetuCd As String = Me.ddlSyubetuCd.SelectedValue.Trim
        '�ڍ�
        Dim strSyousai As String = Me.tbxSyousai.Text
        '�d�v�x
        Dim strJyuuyoudo As String = Me.ddlJyuuyoudo.SelectedValue.Trim
        '���[�U�[ID
        Dim ninsyou As New Ninsyou()
        Dim strUserId As String = ninsyou.GetUserID()

        '�����摶�݃`�F�b�N
        If Not seikyuuSakiTyuuijikouLogic.GetSeikyuusakiCheck(strSeikyuusakiKbn, strSeikyuusakiCd, strSeikyuusakiBrc) Then
            ShowMessage(Messages.Instance.MSG2063E, String.Empty)
        End If

        '���͇����󔒂̏ꍇ
        If strNo.Equals(String.Empty) Then
            '���͇��̍ő�+1�œo�^�������s��
            If Not seikyuuSakiTyuuijikouLogic.InsSeikyuusakiTyuuijikou(strSeikyuusakiKbn, strSeikyuusakiCd, strSeikyuusakiBrc, strNo, strTorikesi, strSyubetuCd, strSyousai, strJyuuyoudo, strUserId) Then
                ShowMessage(Messages.Instance.MSG2058E, String.Empty)
                Exit Sub
            Else
                '����ʂ����
                ClientScript.RegisterStartupScript(Me.GetType(), "WindowClose", "fncClose();", True)
            End If
        Else
            Dim dtSeikyuusakiTyuuijikouCheck As New Data.DataTable
            dtSeikyuusakiTyuuijikouCheck = seikyuuSakiTyuuijikouLogic.GetSeikyuusakiTyuuijikouCheck(strSeikyuusakiKbn, strSeikyuusakiCd, strSeikyuusakiBrc, strNo)
            If dtSeikyuusakiTyuuijikouCheck.Rows.Count > 0 Then
                'DB�����m�F���b�Z�[�W�\��()
                Call Me.ShowKakuninMessage(Messages.Instance.MSG2057E)
            Else
                ShowMessage(Messages.Instance.MSG2062E, String.Empty)
                Exit Sub
            End If
        End If

    End Sub

    ''' <summary>�X�V����</summary>
    Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        '������敪
        Dim strSeikyuusakiKbn As String = Me.ddlSeikyuusakiKbn.SelectedValue.Trim
        '������R�[�h
        Dim strSeikyuusakiCd As String = Me.tbxSeikyuusakiCd.Text.Trim
        '������}��
        Dim strSeikyuusakiBrc As String = Me.tbxSeikyuusakiBrc.Text.Trim
        '���͇�
        Dim strNo As String = Me.lblNo.Text.Trim
        '���
        Dim strTorikesi As String = IIf(Me.chkTorikesi.Checked, "1", "0")
        '��ʃR�[�h
        Dim strSyubetuCd As String = Me.ddlSyubetuCd.SelectedValue.Trim
        '�ڍ�
        Dim strSyousai As String = Me.tbxSyousai.Text
        '�d�v�x
        Dim strJyuuyoudo As String = Me.ddlJyuuyoudo.SelectedValue.Trim
        '���[�U�[ID
        Dim ninsyou As New Ninsyou()
        Dim strUserId As String = ninsyou.GetUserID()

        '�r���`�F�b�N���s��
        Dim dtSeikyuusakiTyuuijikouCheck As New Data.DataTable
        dtSeikyuusakiTyuuijikouCheck = seikyuuSakiTyuuijikouLogic.GetSeikyuusakiTyuuijikouCheck(strSeikyuusakiKbn, strSeikyuusakiCd, strSeikyuusakiBrc, strNo)
        If dtSeikyuusakiTyuuijikouCheck.Rows.Count > 0 Then
            With dtSeikyuusakiTyuuijikouCheck.Rows(0)
                '�o�^����
                If Not IsDBNull(.Item("add_datetime")) Then
                    If DateTime.Compare(CDate(.Item("add_datetime")), CDate(ViewState("DbTime"))) > 0 Then
                        ShowMessage(Messages.Instance.MSG2061E, String.Empty)
                        Exit Sub
                    End If
                End If
                '�X�V����
                If Not IsDBNull(.Item("upd_datetime")) Then
                    If DateTime.Compare(CDate(.Item("upd_datetime")), CDate(ViewState("DbTime"))) > 0 Then
                        ShowMessage(Messages.Instance.MSG2061E, String.Empty)
                        Exit Sub
                    End If
                End If
            End With
        Else
            ShowMessage(Messages.Instance.MSG2062E, String.Empty)
            Exit Sub
        End If

        '�X�V�������s��
        If Not seikyuuSakiTyuuijikouLogic.UpdSeikyuusakiTyuuijikou(strSeikyuusakiKbn, strSeikyuusakiCd, strSeikyuusakiBrc, strNo, strTorikesi, strSyubetuCd, strSyousai, strJyuuyoudo, strUserId) Then
            ShowMessage(Messages.Instance.MSG2058E, String.Empty)
            Exit Sub
        Else
            '����ʂ����
            ClientScript.RegisterStartupScript(Me.GetType(), "WindowClose", "fncClose();", True)
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
                    If Me.tbxSeikyuusakiCd.Enabled Then
                        strObjId = Me.tbxSeikyuusakiCd.ClientID
                    Else
                        strObjId = String.Empty
                    End If
                End If
            End If

            '������}��(���p�p�����`�F�b�N)
            If Me.tbxSeikyuusakiBrc.Text <> String.Empty Then
                .Append(commonCheck.ChkHankakuEisuuji(Me.tbxSeikyuusakiBrc.Text, "������}��"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    If Me.tbxSeikyuusakiBrc.Enabled Then
                        strObjId = Me.tbxSeikyuusakiBrc.ClientID
                    Else
                        strObjId = String.Empty
                    End If
                End If
            End If

            '�ڍׂ�128�o�C�g�𒴂���ꍇ
            If Not Me.tbxSyousai.Text.Equals(String.Empty) Then
                Dim hEncoding As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")
                Dim btBytes As Byte() = hEncoding.GetBytes(Me.tbxSyousai.Text)
                If btBytes.LongLength > 128 Then
                    .Append(Messages.Instance.MSG2060E)
                    If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                        strObjId = Me.tbxSyousai.ClientID
                    End If
                End If
            End If

            '�ڍׂ͋֎~����(���s�R�[�h���܂߂�)������ꍇ
            If Me.tbxSyousai.Text <> String.Empty Then
                .Append(commonCheck.CheckKinsoku(Me.tbxSyousai.Text, "�ڍ�"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = Me.tbxSyousai.ClientID
                End If
            End If

        End With
        Return csScript.ToString
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

    ''' <summary>DB�����m�F���b�Z�[�W�\��</summary>
    Private Sub ShowKakuninMessage(ByVal strMessage As String)
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("fncDbSyoriKakunin('" & strMessage & "'); ")
        End With
        '�y�[�W�����ŁA�N���C�A���g���̃X�N���v�g �u���b�N���o�͂��܂�
        ClientScript.RegisterStartupScript(Me.GetType(), "ShowTourokuMessage", csScript.ToString, True)
    End Sub

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

            '������̌���popup
            .AppendLine("	function fncSeikyuusakiPopup() ")
            .AppendLine("	{ ")
            .AppendLine("		window.open('search_Seikyuusaki.aspx?blnDelete=False&Kbn='+escape('������')+'&FormName=" & Me.Page.Form.Name & " &objKbn=" & Me.ddlSeikyuusakiKbn.ClientID & "&objCd=" & Me.tbxSeikyuusakiCd.ClientID & "&objBrc=" & Me.tbxSeikyuusakiBrc.ClientID & "&objMei=" & Me.tbxSeikyuusakiMei.ClientID & "&strKbn='+escape(document.getElementById('" & Me.ddlSeikyuusakiKbn.ClientID & "').value)+'&strCd='+escape(document.getElementById('" & Me.tbxSeikyuusakiCd.ClientID & "').value)+'&strBrc='+escape(document.getElementById('" & Me.tbxSeikyuusakiBrc.ClientID & "').value)+'&objBtn=" & Me.btnDisplay.ClientID & "','SeikyuusakiPopup','menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes'); ")
            .AppendLine("	} ")

            '������̕ύX
            .AppendLine("   function fncSeikyuusakiChange(strKbn) ")
            .AppendLine("   { ")
            .AppendLine("       var objGamen; ")
            .AppendLine("       var objHidden; ")
            .AppendLine("       switch(strKbn) ")
            .AppendLine("	    { ")
            .AppendLine("           case 'kbn':")
            .AppendLine("               objGamen = document.getElementById('" & Me.ddlSeikyuusakiKbn.ClientID & "');")
            .AppendLine("               objHidden = document.getElementById('" & Me.hdnSeikyuusakiKbn.ClientID & "');")
            .AppendLine("               break; ")
            .AppendLine("           case 'cd':")
            .AppendLine("               objGamen = document.getElementById('" & Me.tbxSeikyuusakiCd.ClientID & "');")
            .AppendLine("               objHidden = document.getElementById('" & Me.hdnSeikyuusakiCd.ClientID & "');")
            .AppendLine("               break; ")
            .AppendLine("           case 'brc':")
            .AppendLine("               objGamen = document.getElementById('" & Me.tbxSeikyuusakiBrc.ClientID & "');")
            .AppendLine("               objHidden = document.getElementById('" & Me.hdnSeikyuusakiBrc.ClientID & "');")
            .AppendLine("               break; ")
            .AppendLine("		    default: ")
            .AppendLine("               document.getElementById('" & Me.hdnSearchFlg.ClientID & "').value = '0'; ")
            .AppendLine("               return false; ")
            .AppendLine("       } ")
            .AppendLine("       if(objGamen.value != objHidden.value) ")
            .AppendLine("       { ")
            .AppendLine("           document.getElementById('" & Me.hdnSearchFlg.ClientID & "').value = '0'; ")
            .AppendLine("       } ")
            .AppendLine("   } ")

            '��o�^��{�^�����������A���̓`�F�b�N
            .AppendLine("   function fncNyuuryokuCheck() ")
            .AppendLine("   { ")
            '������敪�������͂̏ꍇ
            .AppendLine("       var strKbn = Trim(document.getElementById('" & Me.ddlSeikyuusakiKbn.ClientID & "').value); ")
            .AppendLine("       if(strKbn == '') ")
            .AppendLine("       { ")
            .AppendLine("           alert('" & Messages.Instance.MSG013E.Replace("@PARAM1", "������敪") & "'); ")
            .AppendLine("           if(document.getElementById('" & Me.ddlSeikyuusakiKbn.ClientID & "').disabled == false) ")
            .AppendLine("           { ")
            .AppendLine("               document.getElementById('" & Me.ddlSeikyuusakiKbn.ClientID & "').focus(); ")
            .AppendLine("           } ")
            .AppendLine("           return false; ")
            .AppendLine("       } ")
            '������R�[�h�������͂̏ꍇ
            .AppendLine("       var strCd = Trim(document.getElementById('" & Me.tbxSeikyuusakiCd.ClientID & "').value); ")
            .AppendLine("       if(strCd == '') ")
            .AppendLine("       { ")
            .AppendLine("           alert('" & Messages.Instance.MSG013E.Replace("@PARAM1", "������R�[�h") & "'); ")
            .AppendLine("           if(document.getElementById('" & Me.tbxSeikyuusakiCd.ClientID & "').disabled == false) ")
            .AppendLine("           { ")
            .AppendLine("               document.getElementById('" & Me.tbxSeikyuusakiCd.ClientID & "').focus(); ")
            .AppendLine("           } ")
            .AppendLine("           return false; ")
            .AppendLine("       } ")
            '������R�[�h�������͂̏ꍇ
            .AppendLine("       var strBrc = Trim(document.getElementById('" & Me.tbxSeikyuusakiBrc.ClientID & "').value); ")
            .AppendLine("       if(strBrc == '') ")
            .AppendLine("       { ")
            .AppendLine("           alert('" & Messages.Instance.MSG013E.Replace("@PARAM1", "������}��") & "'); ")
            .AppendLine("           if(document.getElementById('" & Me.tbxSeikyuusakiBrc.ClientID & "').disabled == false) ")
            .AppendLine("           { ")
            .AppendLine("               document.getElementById('" & Me.tbxSeikyuusakiBrc.ClientID & "').focus(); ")
            .AppendLine("           } ")
            .AppendLine("           return false; ")
            .AppendLine("       } ")
            '�d�v�x�������͂̏ꍇ
            .AppendLine("       var strJyuuyoudo = Trim(document.getElementById('" & Me.ddlJyuuyoudo.ClientID & "').value); ")
            .AppendLine("       if(strJyuuyoudo == '') ")
            .AppendLine("       { ")
            .AppendLine("           alert('" & Messages.Instance.MSG013E.Replace("@PARAM1", "�d�v�x") & "'); ")
            .AppendLine("           document.getElementById('" & Me.ddlJyuuyoudo.ClientID & "').focus(); ")
            .AppendLine("           return false; ")
            .AppendLine("       } ")
            '��ʃR�[�h�������͂̏ꍇ
            .AppendLine("       var strSyubetu = Trim(document.getElementById('" & Me.ddlSyubetuCd.ClientID & "').value); ")
            .AppendLine("       if(strSyubetu == '') ")
            .AppendLine("       { ")
            .AppendLine("           alert('" & Messages.Instance.MSG013E.Replace("@PARAM1", "��ʃR�[�h") & "'); ")
            .AppendLine("           document.getElementById('" & Me.ddlSyubetuCd.ClientID & "').focus(); ")
            .AppendLine("           return false; ")
            .AppendLine("       } ")
            '������敪�E�R�[�h�E�}�Ԃ̂����ꂩ���ύX����Č����{�^����������Ă��邩�̃`�F�b�N
            .AppendLine("       if(document.getElementById('" & Me.hdnSearchFlg.ClientID & "').value != '1') ")
            .AppendLine("       { ")
            .AppendLine("           alert('" & Messages.Instance.MSG2059E & "'); ")
            .AppendLine("           document.getElementById('" & Me.ddlSeikyuusakiKbn.ClientID & "').focus(); ")
            .AppendLine("           return false; ")
            .AppendLine("       } ")
            .AppendLine("       return true; ")
            .AppendLine("   } ")

            'DB�����m�F
            .AppendLine("   function fncDbSyoriKakunin(strMessage) ")
            .AppendLine("   { ")
            .AppendLine("       if(confirm(strMessage)) ")
            .AppendLine("       { ")
            .AppendLine("           document.getElementById('" & Me.btnUpdate.ClientID & "').click(); ")
            .AppendLine("       } ")
            .AppendLine("       else ")
            .AppendLine("       { ")
            .AppendLine("           return false; ")
            .AppendLine("       } ")
            .AppendLine("   } ")

            .AppendLine("	function LTrim(str)   ")
            .AppendLine("	{   ")
            .AppendLine("		var i;  ")
            .AppendLine("		for(i=0;i<str.length;i++)   ")
            .AppendLine("		{   ")
            .AppendLine("			if(str.charAt(i)!=' '&&str.charAt(i)!='�@')break;   ")
            .AppendLine("		}   ")
            .AppendLine("		str=str.substring(i,str.length);   ")
            .AppendLine("		return str;   ")
            .AppendLine("	}   ")
            .AppendLine("	function RTrim(str)   ")
            .AppendLine("	{   ")
            .AppendLine("		var i;   ")
            .AppendLine("		for(i=str.length-1;i>=0;i--)   ")
            .AppendLine("		{   ")
            .AppendLine("			if(str.charAt(i)!=' '&&str.charAt(i)!='�@')break;   ")
            .AppendLine("		}   ")
            .AppendLine("		str=str.substring(0,i+1);   ")
            .AppendLine("		return str;   ")
            .AppendLine("	}  ")
            .AppendLine("	function Trim(str)   ")
            .AppendLine("	{   ")
            .AppendLine("		return LTrim(RTrim(str));   ")
            .AppendLine("	}   ")
            .AppendLine("	  ")
            .AppendLine("	function left(mainStr,lngLen)  ")
            .AppendLine("	{   ")
            .AppendLine("		if (lngLen>0)  ")
            .AppendLine("		{ ")
            .AppendLine("			return mainStr.substring(0,lngLen); ")
            .AppendLine("		}   ")
            .AppendLine("		else ")
            .AppendLine("		{ ")
            .AppendLine("			return null; ")
            .AppendLine("		}   ")
            .AppendLine("	}   ")
            .AppendLine("	function right(mainStr,lngLen)  ")
            .AppendLine("	{    ")
            .AppendLine("		if (mainStr.length-lngLen>=0 && mainStr.length>=0 && mainStr.length-lngLen<=mainStr.length)  ")
            .AppendLine("		{   ")
            .AppendLine("			return mainStr.substring(mainStr.length-lngLen,mainStr.length); ")
            .AppendLine("		}   ")
            .AppendLine("		else ")
            .AppendLine("		{ ")
            .AppendLine("			return null; ")
            .AppendLine("		}   ")
            .AppendLine("	}   ")
            .AppendLine("	function mid(mainStr,starnum,endnum) ")
            .AppendLine("	{   ")
            .AppendLine("		if (mainStr.length>=0) ")
            .AppendLine("		{   ")
            .AppendLine("			return mainStr.substr(starnum,endnum); ")
            .AppendLine("		} ")
            .AppendLine("		else ")
            .AppendLine("		{ ")
            .AppendLine("			return null; ")
            .AppendLine("		}   ")
            .AppendLine("	} ")
            .AppendLine("	 ")
            .AppendLine("</script>")
        End With
        Page.ClientScript.RegisterStartupScript(Page.GetType, "JS_" & Me.ClientID, sbScript.ToString)
    End Sub


End Class
Imports Itis.Earth.DataAccess
Imports Itis.Earth.BizLogic
''' <summary>
''' �����X��{���Q�Z��
''' </summary>
''' <remarks></remarks>
Partial Public Class kameitenkihon_jyusho_user
    Inherits System.Web.UI.UserControl

#Region "���ʕϐ�"

    ''' <summary> �N���X�̃C���X�^���X���� </summary>
    Private kihonJyouhouInquiryBc As New Itis.Earth.BizLogic.kihonjyouhou.KihonJyouhouInquiryLogic
    '���p
    Private Const HANKAKU As Integer = 1
    '�S�p
    Private Const ZENKAKU As Integer = 2
    '���b�Z�[�W��FOCUS
    Public msgAndFocus As New Itis.Earth.BizLogic.kihonjyouhou.MessageAndFocus

#End Region

#Region "�v���p�e�B"

    '����
    Private _kenngenn As Boolean
    Public Property Kenngenn() As Boolean
        Get
            Return _kenngenn
        End Get
        Set(ByVal value As Boolean)
            _kenngenn = value
        End Set
    End Property

    '�Z�����
    Private _jyusho As KameitenjyushoDataSet.m_kameiten_jyuusyoDataTable
    Public Property jyusho() As KameitenjyushoDataSet.m_kameiten_jyuusyoDataTable
        Get
            '���W�b�N�ύX�̃^�C�~���O�ŊJ���҂��C�ӂɃC���N�������^�����܂�

            Return _jyusho
        End Get

        Set(ByVal value As KameitenjyushoDataSet.m_kameiten_jyuusyoDataTable)
            _jyusho = value
        End Set

    End Property

    '�����X�R�[�h
    Private _kameiten_cd As String
    Public Property kameiten_cd() As String
        Get
            '���W�b�N�ύX�̃^�C�~���O�ŊJ���҂��C�ӂɃC���N�������^�����܂�

            Return _kameiten_cd
        End Get

        Set(ByVal value As String)
            _kameiten_cd = value
        End Set

    End Property

    Private _upd_login_user_id As String
    Public Property Upd_login_user_id() As String
        Get
            Return _upd_login_user_id
        End Get

        Set(ByVal value As String)
            _upd_login_user_id = value
        End Set

    End Property




#End Region

#Region "���"



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
        Dim kbn As String = ""
        Dim itKassei As Boolean = Iskassei(_kameiten_cd, "")

        '�ۏ؊���
        If Not itKassei Then

            For Each c As Control In meisaiTbody5.Controls

                Try
                    CType(c, TextBox).ReadOnly = Not itKassei
                    CType(c, TextBox).CssClass = IIf(itKassei, "", "readOnly")
                Catch ex1 As Exception
                    Try
                        CType(c, Button).Enabled = itKassei
                        CType(c, Button).CssClass = IIf(itKassei, "", "readOnly")
                    Catch ex2 As Exception
                        Try
                            CType(c, DropDownList).Enabled = itKassei
                            CType(c, DropDownList).CssClass = IIf(itKassei, "", "readOnly")
                        Catch ex As Exception
                        End Try

                    End Try
                End Try
            Next

            Me.ddlTodoufuken.Enabled = False
            Me.btnTouroku.Enabled = False

        End If
    End Sub

    ''' <summary>
    ''' ��ʏ�����
    ''' </summary>
    ''' <param name="sender">System.Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            '��ʏ�����
            PageInit()
        
        End If

        SetKassei()

    End Sub

    ''' <summary>
    ''' �o�^�{�^��
    ''' </summary>
    ''' <param name="sender">System.Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub btnTouroku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTouroku.Click

        '���̓`�F�b�N
        If Not chkInputValue() Then
            Exit Sub
        End If

        '�o�^�N���̎����\��
        If Me.tbxAddNengetu.Text = String.Empty Then
            '�V�X�e�����t��\������
            Me.tbxAddNengetu.Text = Convert.ToDateTime(kihonJyouhouInquiryBc.GetSysDate).ToString("yyyy/MM")
        End If

        '���̒[���ōX�V�`�F�b�N
        If Not ChkOtherUserKousin() Then
            Exit Sub
        End If



        ' �Z�����o�^
        If SetKameitenJyushoInfo() Then

            Dim maxDate As String
            maxDate = kihonJyouhouInquiryBc.GetMaxUpdDate(_kameiten_cd).Split(",")(0)
            '�X�V��
            Me.hidUpdTime.Value = maxDate

            '��ʂ��X�V�@�i�Z���P�`�S�j
            Dim otherPageFunction As New Itis.Earth.BizLogic.kihonjyouhou.OtherPageFunction
            otherPageFunction.DoFunction(Parent.Page, "GetKousinData")

            ShowMsg(Messages.Instance.MSG018S, Me.btnTouroku, "�Z�����")
        Else
            ShowMsg(Messages.Instance.MSG019E, Me.btnTouroku, "�Z�����")
        End If



    End Sub
    Protected Sub btnKensaku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKensaku.Click

        '�Z��
        Dim data As DataSet
        Dim jyuusyo As String
        Dim jyuusyoMei As String
        Dim jyuusyoNo As String
        'If Me.tbxYuubinNo1.Text = String.Empty Then
        '    Exit Sub
        'End If
        '�Z���擾
        Dim csScript As New StringBuilder

        data = (kihonJyouhouInquiryBc.GetMailAddress(Me.tbxYuubinNo1.Text.Replace("-", String.Empty).Trim))
        If data.Tables(0).Rows.Count = 1 Then


            jyuusyo = data.Tables(0).Rows(0).Item(0).ToString
            jyuusyoNo = jyuusyo.Split(",")(0)

            jyuusyoMei = GetJyusho(jyuusyo.Split(",")(1))

            'Me.tbxJyuusyo1.Text = jyuusyoMei.Split(",")(0)
            'Me.tbxJyuusyo2.Text = jyuusyoMei.Split(",")(1)

            If jyuusyoNo.Length > 3 Then
                jyuusyoNo = jyuusyoNo.Substring(0, 3) & "-" & jyuusyoNo.Substring(3, jyuusyoNo.Length - 3)
            End If
            'Me.tbxYuubinNo1.Text = jyuusyoNo

            csScript.AppendLine("if(document.getElementById('" & Me.tbxJyuusyo1.ClientID & "').value!='' || document.getElementById('" & Me.tbxJyuusyo2.ClientID & "').value!='' || document.getElementById('" & CType(ddlTodoufuken.FindControl("ddlCommonDrop"), DropDownList).ClientID & "').value!=''){" & vbCrLf)
            csScript.AppendLine("if (!confirm('�����f�[�^������܂����㏑�����Ă�낵���ł����B')){}else{ " & vbCrLf)

            csScript.AppendLine("document.getElementById('" & Me.tbxJyuusyo1.ClientID & "').value = '" & jyuusyoMei.Split(",")(0) & "';" & vbCrLf)
            csScript.AppendLine("document.getElementById('" & Me.tbxJyuusyo2.ClientID & "').value = '" & jyuusyoMei.Split(",")(1) & "';" & vbCrLf)
            csScript.AppendLine("document.getElementById('" & Me.tbxYuubinNo1.ClientID & "').value = '" & jyuusyoNo & "';" & vbCrLf)
            csScript.AppendLine("document.getElementById('" & CType(ddlTodoufuken.FindControl("ddlCommonDrop"), DropDownList).ClientID & "').value = '" & data.Tables(0).Rows(0).Item(1).ToString & "';" & vbCrLf)
            csScript.AppendLine("}" & vbCrLf)

            csScript.AppendLine("}else{" & vbCrLf)
            csScript.AppendLine("document.getElementById('" & Me.tbxJyuusyo1.ClientID & "').value = '" & jyuusyoMei.Split(",")(0) & "';" & vbCrLf)
            csScript.AppendLine("document.getElementById('" & Me.tbxJyuusyo2.ClientID & "').value = '" & jyuusyoMei.Split(",")(1) & "';" & vbCrLf)
            csScript.AppendLine("document.getElementById('" & Me.tbxYuubinNo1.ClientID & "').value = '" & jyuusyoNo & "';" & vbCrLf)
            csScript.AppendLine("document.getElementById('" & CType(ddlTodoufuken.FindControl("ddlCommonDrop"), DropDownList).ClientID & "').value = '" & data.Tables(0).Rows(0).Item(1).ToString & "';" & vbCrLf)
            csScript.AppendLine("}" & vbCrLf)
        Else


            csScript.AppendLine("document.getElementById('" & CType(ddlTodoufuken.FindControl("ddlCommonDrop"), DropDownList).ClientID & "').value = '';" & vbCrLf)

            csScript.AppendLine("fncOpenwindowYuubin('" & Me.tbxYuubinNo1.ClientID & _
                                                                    "','" & Me.tbxJyuusyo1.ClientID & "','" & _
                                                                     Me.tbxJyuusyo2.ClientID & "','" & CType(ddlTodoufuken.FindControl("ddlCommonDrop"), DropDownList).ClientID & "');")



        End If


        ScriptManager.RegisterStartupScript(Me, _
                                        Me.GetType(), _
                                        "openWindowYuubin", _
                                        csScript.ToString, _
                                        True)


    End Sub


#End Region

#Region "����"

    ''' <summary>
    ''' ��ʏ�����
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub PageInit()

        '�o�^�����ݒ�
        Setkenngen()

        'Javascript Event Bind
        BindJavaScriptEvent()

        '��ʂ̒l��ݒ�
        SetGamenValue()

    End Sub

    ''' <summary>
    ''' �o�^�����̐ݒ�
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Setkenngen()

        '�o�^����
        If _kenngenn Then
            Me.btnTouroku.Enabled = True

        Else
            Me.btnTouroku.Enabled = False
        End If

    End Sub

    ''' <summary>
    ''' Javascript Event Bind
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BindJavaScriptEvent()
        '  �o�^�N��
        Me.tbxAddNengetu.Attributes.Add("onfocus", "setOnfocusNengetu(this)")
        Me.tbxAddNengetu.Attributes.Add("onblur", "chkNengetu(this)")
        'Me.btnKensaku.Attributes.Add("onclick", "fncOpenwindowYuubin('" & Me.tbxYuubinNo1.ClientID & _
        '"','" & Me.tbxJyuusyo1.ClientID & "','" & _
        ' Me.tbxJyuusyo2.ClientID & "');return false;")


        '  Me.btnKensaku.Attributes.Add("onclick", "return chkJyuusyoNoPage('" & Me.tbxJyuusyo1.ClientID & "','" & Me.tbxJyuusyo2.ClientID & "');")

        Me.tbxYuubinNo1.Attributes.Add("onblur", "SetYoubin(this)")
        'Me.tbxYuubinNo1.Attributes.Add("onchange", "ShowModal()")

        'Me.btnTouroku.Attributes.Add("onclick", "ShowModal();")
    End Sub

    ''' <summary>
    ''' ��ʂ̒l��ݒ�
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetGamenValue()

        '�����X�Z�������擾����B
        Dim data As KameitenjyushoDataSet.m_kameiten_jyuusyoRow()
        data = _jyusho.Select("jyuusyo_no=1")

        '�f�[�^�����鎞�A��ʂ�\��
        If data.Length = 1 Then

            '�o�^�N��
            If Not data(0).Isadd_nengetuNull Then
                Me.tbxAddNengetu.Text = data(0).add_nengetu
            End If

            '�X��NO
            If Not data(0).Isyuubin_noNull Then
                Me.tbxYuubinNo1.Text = data(0).yuubin_no
            End If

            '�Z���P
            If Not data(0).Isjyuusyo1Null Then
                Me.tbxJyuusyo1.Text = data(0).jyuusyo1
            End If

            '�Z���Q
            If Not data(0).Isjyuusyo2Null Then
                Me.tbxJyuusyo2.Text = data(0).jyuusyo2
            End If

            '�Z���Q
            If Not data(0).Isbusyo_meiNull Then
                Me.tbxBusyoMei1.Text = data(0).busyo_mei
            End If

            '�X��No
            If Not (data(0).Isyuubin_noNull) Then
                Me.tbxYuubinNo1.Text = data(0).yuubin_no
            End If

            '������
            If Not (data(0).Isbusyo_meiNull) Then

                Me.tbxBusyoMei1.Text = data(0).busyo_mei
            End If

            '��\�Җ�
            If Not (data(0).Isdaihyousya_meiNull) Then
                Me.tbxDaihyousyaMei1.Text = data(0).daihyousya_mei
            End If

            'Tel
            If Not (data(0).Istel_noNull) Then
                Me.tbxTelNo1.Text = data(0).tel_no
            End If

            'FAX
            If Not (data(0).Isfax_noNull) Then
                Me.tbxFaxNo1.Text = data(0).fax_no
            End If

            'Address
            If Not (data(0).Ismail_addressNull) Then
                Me.tbxMailAddress.Text = data(0).mail_address
            End If

            '���l�P
            If Not (data(0).Isbikou1Null) Then
                Me.tbxBikou11.Text = data(0).bikou1
            End If

            '���l�Q
            If Not (data(0).Isbikou2Null) Then
                Me.tbxBikou21.Text = data(0).bikou2
            End If
            '�s���{��
            If Not (data(0).Istodouhuken_cdNull) Then
                ddlTodoufuken.SelectedValue = data(0).todouhuken_cd
            End If
        End If
        '�X�V��

        Me.hidUpdTime.Value = kihonJyouhouInquiryBc.GetMaxUpdDate(_kameiten_cd)

    End Sub

    ''' <summary>
    ''' �Z�����o�^
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetKameitenJyushoInfo() As Boolean

        '��ʂ̃f�[�^���擾
        Dim kameitenjyushoDataSet As New KameitenjyushoDataSet
        Dim dr As KameitenjyushoDataSet.m_kameiten_jyuusyoRow

        '�l�̎擾
        dr = kameitenjyushoDataSet.m_kameiten_jyuusyo.NewRow
        dr.kameiten_cd = _kameiten_cd
        dr.jyuusyo_no = 1
        dr.jyuusyo1 = Me.tbxJyuusyo1.Text
        dr.jyuusyo2 = Me.tbxJyuusyo2.Text
        dr.yuubin_no = Me.tbxYuubinNo1.Text
        dr.tel_no = Me.tbxTelNo1.Text
        dr.fax_no = Me.tbxFaxNo1.Text
        dr.busyo_mei = Me.tbxBusyoMei1.Text
        dr.daihyousya_mei = Me.tbxDaihyousyaMei1.Text
        dr.add_nengetu = Me.tbxAddNengetu.Text
        dr.bikou1 = Me.tbxBikou11.Text
        dr.bikou2 = Me.tbxBikou21.Text
        dr.mail_address = Me.tbxMailAddress.Text
        dr.upd_login_user_id = _upd_login_user_id
        dr.add_login_user_id = _upd_login_user_id
        dr.todouhuken_cd = ddlTodoufuken.SelectedValue
        kameitenjyushoDataSet.m_kameiten_jyuusyo.Rows.Add(dr)

        '�o�^
        Return kihonJyouhouInquiryBc.SetKameitenJyushoInfo(_kameiten_cd, 1, kameitenjyushoDataSet)

    End Function

    ''' <summary>
    ''' ���̒[���ōX�V�`�F�b�N
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ChkOtherUserKousin() As Boolean

        '���̒[���ōX�V�`�F�b�N
        Dim msg As String
        msg = kihonJyouhouInquiryBc.ChkJyushoTouroku(_kameiten_cd, Me.hidUpdTime.Value)
        If msg <> String.Empty Then
            ShowMsg(msg, btnTouroku)
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' ���̓`�F�b�N
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function chkInputValue() As Boolean

        If Me.tbxAddNengetu.Text <> String.Empty Then
            Dim commonCheck As New CommonCheck
            If commonCheck.CheckYuukouHiduke(Me.tbxAddNengetu.Text & "/01", "�o�^�N��") <> String.Empty Then
                msgAndFocus.AppendMsgAndCtrl(Me.tbxAddNengetu, commonCheck.CheckYuukouHiduke(Me.tbxAddNengetu.Text & "/01", "�o�^�N��"))
            End If
        End If


        '�����̓`�F�b�N
        chkMinyuuryoku()


        '���p�����`�F�b�N
        chkHankaku()
        '�S�p�����`�F�b�N
        chkZenkaku()
        '�֑��`�F�b�N
        checkKinsoku()




        ''���b�Z�[�W�\��
        If msgAndFocus.Message <> String.Empty Then
            ShowMsg(msgAndFocus.Message, msgAndFocus.focusCtrl)
            Return False
        Else
            Return True
        End If

    End Function

    ''' <summary>
    ''' ���p�`�F�b�N
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub chkHankaku()
        '���������͌����`�F�b�N(���p)
        '�o�^�N��
        chkKetaSuu(Me.tbxAddNengetu, Me.tbxAddNengetu.Text, "�o�^�N��", 7, 1)
        '�X�֔ԍ�
        chkKetaSuu(Me.tbxYuubinNo1, Me.tbxYuubinNo1.Text, "�X�֔ԍ�", 8, 1)
        '�d�b�ԍ�
        chkKetaSuu(Me.tbxTelNo1, Me.tbxTelNo1.Text, "�d�b�ԍ�", 16, 1)
        'FAX�ԍ�
        chkKetaSuu(Me.tbxFaxNo1, Me.tbxFaxNo1.Text, "FAX�ԍ�", 16, 1)
        '�\���S����
        chkKetaSuu(Me.tbxMailAddress, Me.tbxMailAddress.Text, "�\���S����", 64, 1)
    End Sub

    ''' <summary>
    ''' �S�p�`�F�b�N
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub chkZenkaku()

        '���͌����`�F�b�N(�S�p)
        '�Z��1
        chkKetaSuu(Me.tbxJyuusyo1, Me.tbxJyuusyo1.Text, "�Z��1", 40, 2)

        '�Z��2
        chkKetaSuu(Me.tbxJyuusyo2, Me.tbxJyuusyo2.Text, "�Z��2", 30, 2)

        '������
        chkKetaSuu(Me.tbxBusyoMei1, Me.tbxBusyoMei1.Text, "������", 50, 2)

        '���l1
        chkKetaSuu(Me.tbxBikou11, Me.tbxBikou11.Text, "���l1", 30, 2)

        '���l2
        chkKetaSuu(Me.tbxBikou21, Me.tbxBikou21.Text, "���l2", 30, 2)

        '��\�Җ�
        chkKetaSuu(Me.tbxDaihyousyaMei1, Me.tbxDaihyousyaMei1.Text, "��\��", 20, 2)


    End Sub

    ''' <summary>
    ''' �����̓`�F�b�N
    ''' �����X�Z���P�@�K�{����
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub chkMinyuuryoku()
        '��������������X�Z���P
        If Trim(Me.tbxJyuusyo1.Text = String.Empty) Then
            '�Z��1�͕K�{���͂ł�
            msgAndFocus.AppendMsgAndCtrl(Me.tbxJyuusyo1, Messages.Instance.MSG013E, "�Z��1")
        End If
    End Sub

    ''' <summary>
    ''' ���ڌ����`�F�b�N����
    ''' </summary>
    ''' <param name="data">data</param>
    ''' <param name="itemName">itemName</param>
    ''' <param name="max">max</param>
    ''' <param name="type">type</param>
    ''' <remarks></remarks>
    Public Sub chkKetaSuu(ByVal control As System.Web.UI.Control, _
                                            ByVal data As String, _
                                            ByVal itemName As String, _
                                            ByVal max As Long, _
                                            ByVal type As Long)

        '�l��Check���s�Ȃ�
        If data = String.Empty Then
            Exit Sub
        End If

        '�������`�F�b�N
        If System.Text.Encoding.Default.GetBytes(data).Length() > max Then
            Dim csScript As New StringBuilder

            'MsgBox �\��
            If type = HANKAKU Then

                '���p�@{0}�ɓo�^�ł��镶�����́A���p{1}�����ȓ��ł��B
                msgAndFocus.AppendMsgAndCtrl(control, Messages.Instance.MSG2003E, itemName, max)
            Else

                '�S�p�@{0}�ɓo�^�ł��镶�����́A�S�p{1}�����ȓ��ł��B
                msgAndFocus.AppendMsgAndCtrl(control, Messages.Instance.MSG2002E, itemName, Int(max / 2))
            End If
            Exit Sub
        End If

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
    ''' MsgBox�\��
    ''' </summary>
    ''' <param name="msg">msg</param>
    ''' <param name="control">WebControl</param>
    ''' <param name="param1">param</param>
    ''' <param name="param2">param</param>
    ''' <param name="param3">param</param>
    ''' <param name="param4">param</param>
    ''' <remarks></remarks>
    Public Sub ShowMsg(ByVal msg As String, _
                                ByVal control As System.Web.UI.Control, _
                                Optional ByVal param1 As String = "", _
                                Optional ByVal param2 As String = "", _
                                Optional ByVal param3 As String = "", _
                                Optional ByVal param4 As String = "")

        Dim csScript As New StringBuilder


        Dim pPage As Page = Me.Parent.Page
        Dim pType As Type = pPage.GetType
        Dim methodInfo As System.Reflection.MethodInfo = pType.GetMethod("ShowMsg")

        msg = msg.Replace("@PARAM1", param1) _
                          .Replace("@PARAM2", param2) _
                          .Replace("@PARAM3", param3) _
                          .Replace("@PARAM4", param4)

        If Not methodInfo Is Nothing Then
            methodInfo.Invoke(pPage, New Object() {control.ClientID, csScript.AppendFormat( _
                                                                "" & msg & "", _
                                                                param1, param2, param3, param4).ToString _
                                                                })
        End If


    End Sub

    ''' <summary>
    ''' �֑��`�F�b�N
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub checkKinsoku()

        '�`�F�b�NObject
        Dim chkobj As New CommonCheck

        '�Z��1
        If chkobj.checkKinsoku(Me.tbxJyuusyo1.Text, "�Z��1") <> String.Empty Then
            msgAndFocus.AppendMsgAndCtrl(Me.tbxJyuusyo1, chkobj.checkKinsoku(Me.tbxJyuusyo1.Text, "�Z��1"))
        End If

        '�Z��2
        If chkobj.checkKinsoku(Me.tbxJyuusyo2.Text, "�Z��2") <> String.Empty Then
            msgAndFocus.AppendMsgAndCtrl(Me.tbxJyuusyo2, chkobj.CheckKinsoku(Me.tbxJyuusyo2.Text, "�Z��2"))
        End If

        '������
        If chkobj.checkKinsoku(Me.tbxBusyoMei1.Text, "������") <> String.Empty Then
            msgAndFocus.AppendMsgAndCtrl(Me.tbxBusyoMei1, chkobj.checkKinsoku(Me.tbxBusyoMei1.Text, "������"))
        End If

        '���l1
        If chkobj.checkKinsoku(Me.tbxBikou11.Text, "���l1") <> String.Empty Then
            msgAndFocus.AppendMsgAndCtrl(Me.tbxBikou11, chkobj.checkKinsoku(Me.tbxBikou11.Text, "���l1"))
        End If

        '���l2
        If chkobj.checkKinsoku(Me.tbxBikou21.Text, "���l2") <> String.Empty Then
            msgAndFocus.AppendMsgAndCtrl(Me.tbxBikou21, chkobj.checkKinsoku(Me.tbxBikou21.Text, "���l2"))
        End If

        '��\�Җ�
        If chkobj.checkKinsoku(Me.tbxDaihyousyaMei1.Text, "��\��") <> String.Empty Then
            msgAndFocus.AppendMsgAndCtrl(Me.tbxDaihyousyaMei1, chkobj.checkKinsoku(Me.tbxDaihyousyaMei1.Text, "��\��"))
        End If

        '�X�֔ԍ�
        If chkobj.checkKinsoku(Me.tbxYuubinNo1.Text, "�X�֔ԍ�") <> String.Empty Then
            msgAndFocus.AppendMsgAndCtrl(Me.tbxYuubinNo1, chkobj.checkKinsoku(Me.tbxYuubinNo1.Text, "�X�֔ԍ�"))
        End If

        '�d�b�ԍ�
        If chkobj.checkKinsoku(Me.tbxTelNo1.Text, "�d�b�ԍ�") <> String.Empty Then
            msgAndFocus.AppendMsgAndCtrl(Me.tbxTelNo1, chkobj.checkKinsoku(Me.tbxTelNo1.Text, "�d�b�ԍ�"))
        End If

        'FAX�ԍ�
        If chkobj.checkKinsoku(Me.tbxFaxNo1.Text, "FAX�ԍ�") <> String.Empty Then
            msgAndFocus.AppendMsgAndCtrl(Me.tbxFaxNo1, chkobj.checkKinsoku(Me.tbxFaxNo1.Text, "FAX�ԍ�"))
        End If

        '�\���S����
        If chkobj.checkKinsoku(Me.tbxMailAddress.Text, "�\���S����") <> String.Empty Then
            msgAndFocus.AppendMsgAndCtrl(Me.tbxMailAddress, chkobj.checkKinsoku(Me.tbxMailAddress.Text, "�\���S����"))
        End If

    End Sub


    '''' <summary>
    '''' JAVASCRIPT
    '''' </summary>
    '''' <remarks></remarks>
    'Protected Sub MakeJavaScript()
    '    Dim csType As Type = Page.GetType()
    '    Dim csName As String = "chkJyuushoGamenChange1"
    '    Dim csScript As New StringBuilder
    '    With csScript
    '        .AppendLine("<script language='javascript' type='text/javascript'>  " & vbCrLf)

    '        .AppendLine("function chkJyuusyo()" & vbCrLf)
    '        .AppendLine("{" & vbCrLf)

    '        .AppendLine("if(document.getElementById('" & Me.tbxJyuusyo1.ClientID & "').value!='' || document.getElementById('" & Me.tbxJyuusyo2.ClientID & "').value!=''){" & vbCrLf)
    '        .AppendLine("if (!confirm('�����f�[�^������܂����㏑�����Ă�낵���ł����B')){return false;}else{ " & vbCrLf)
    '        '.AppendLine("fncOpenwindowYuubin('" & Me.tbxYuubinNo1.ClientID & _
    '        '                                                        "','" & Me.tbxJyuusyo1.ClientID & "','" & _
    '        '                                                         Me.tbxJyuusyo2.ClientID & "');return false;")
    '        .AppendLine("return true;}" & vbCrLf)
    '        .AppendLine("}" & vbCrLf)

    '        '.AppendLine("fncOpenwindowYuubin('" & Me.tbxYuubinNo1.ClientID & _
    '        '                                                        "','" & Me.tbxJyuusyo1.ClientID & "','" & _
    '        '                                                         Me.tbxJyuusyo2.ClientID & "');return false;")
    '        .AppendLine("return true;}" & vbCrLf)
    '        .AppendLine("</script>" & vbCrLf)




    '    End With

    '    Me.Page.ClientScript.RegisterClientScriptBlock(csType, csName, csScript.ToString)
    'End Sub

#End Region


  
End Class